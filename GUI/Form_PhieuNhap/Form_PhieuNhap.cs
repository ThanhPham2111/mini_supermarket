using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.PhieuNhap
{
    public partial class Form_PhieuNhap : Form
    {
        private Panel mainPanel;
        private DataGridView dgvPhieuNhap;
        private TextBox txtSearch;
        private ComboBox cboTimePeriod, cboSupplier;
        private Button btnAddImport, btnClear, btnImportExcel;

        public Form_PhieuNhap()
        {
            InitializeComponent();
            LoadData(); // Load data khi khởi tạo form
        }

        private void InitializeComponent()
        {
            this.Text = "Chi tiết phiếu nhập";
            this.Size = new Size(1170, 750);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(248, 249, 250);

            InitializeMainPanel();
            InitializeTitleSection();
            InitializeSearchSection();
            InitializeFilterSection();
            InitializeDataGridView();
        }

        private void InitializeMainPanel()
        {
            mainPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1170, 750),
                BackColor = Color.White,
                Padding = new Padding(30)
            };
            this.Controls.Add(mainPanel);
        }

      private void InitializeTitleSection()
        {
            // Title Panel - Tăng chiều cao để chứa 3 nút
            Panel titlePanel = new Panel
            {
                Location = new Point(30, 20),
                Size = new Size(1110, 155),
                BackColor = Color.White
            };
            mainPanel.Controls.Add(titlePanel);

            // Title Label
            Label titleLabel = new Label
            {
                Text = "Danh sách phiếu nhập",
                Location = new Point(0, 25),
                Size = new Size(500, 50),
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                AutoSize = false
            };
            titlePanel.Controls.Add(titleLabel);

            // Button Panel
            Panel buttonPanel = new Panel
            {
                Location = new Point(930, 10),
                Size = new Size(180, 85),
                BackColor = Color.White
            };
            titlePanel.Controls.Add(buttonPanel);

            // Add Import Button
            btnAddImport = CreateButton(
                "➕ Thêm",
                new Point(0, 0),
                new Size(180, 40),
                Color.FromArgb(25, 135, 84),
                Color.FromArgb(20, 108, 67),
                11
            );
            btnAddImport.Click += BtnAddImport_Click;
            buttonPanel.Controls.Add(btnAddImport);

            // Clear Button
            btnClear = CreateButton(
                "🔄 Làm mới",
                new Point(0, 45),
                new Size(180, 40),
                Color.FromArgb(13, 202, 240),
                Color.FromArgb(10, 162, 192),
                11
            );
            btnClear.Click += BtnClear_Click;
            buttonPanel.Controls.Add(btnClear);

            // Import Excel Button
            btnImportExcel = CreateButton(
                "📥 Nhập Excel",
                new Point(0, 90),
                new Size(180, 40),
                Color.FromArgb(255, 193, 7),
                Color.FromArgb(255, 173, 0),
                11
            );
            btnImportExcel.Click += BtnImportExcel_Click;
            buttonPanel.Controls.Add(btnImportExcel);

            // Tăng height của buttonPanel để chứa thêm nút
            buttonPanel.Size = new Size(180, 135);
        }

        private void BtnAddImport_Click(object sender, EventArgs e)
        {
            Form_ChiTietPhieuNhap formChiTiet = new Form_ChiTietPhieuNhap();
            DialogResult result = formChiTiet.ShowDialog();
            
            // Nếu thêm thành công, reload lại data
            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboTimePeriod.SelectedIndex = 0;
            cboSupplier.SelectedIndex = 0;
            LoadData();
        }

        private void BtnImportExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openDialog = new OpenFileDialog())
                {
                    openDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|CSV Files (*.csv)|*.csv";
                    openDialog.Title = "Chọn file Excel để nhập";
                    openDialog.Multiselect = false;

                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        string extension = Path.GetExtension(openDialog.FileName).ToLower();
                        
                        if (extension == ".csv")
                        {
                            ImportFromCSV(openDialog.FileName);
                        }
                        else if (extension == ".xls" || extension == ".xlsx")
                        {
                            ImportFromExcel(openDialog.FileName);
                        }
                        else
                        {
                            MessageBox.Show("Định dạng file không được hỗ trợ!", "Lỗi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nhập file: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportFromCSV(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
                
                if (lines.Length < 7) // Kiểm tra file có đủ dữ liệu không
                {
                    MessageBox.Show("File CSV không đúng định dạng!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse thông tin phiếu nhập
                string maPhieuNhap = "";
                DateTime? ngayNhap = null;
                string nhaCungCap = "";
                List<(string tenSP, string donVi, int soLuong, decimal donGia, decimal thanhTien)> sanPhamList = new List<(string, string, int, decimal, decimal)>();

                int currentLine = 0;
                
                // Đọc header
                while (currentLine < lines.Length)
                {
                    string line = lines[currentLine];
                    
                    if (line.StartsWith("Mã phiếu nhập:"))
                    {
                        var parts = line.Split(',');
                        if (parts.Length > 1)
                            maPhieuNhap = parts[1].Trim();
                    }
                    else if (line.StartsWith("Ngày nhập:"))
                    {
                        var parts = line.Split(',');
                        if (parts.Length > 1 && DateTime.TryParse(parts[1].Trim(), out DateTime date))
                            ngayNhap = date;
                    }
                    else if (line.StartsWith("Nhà cung cấp:"))
                    {
                        var parts = line.Split(',');
                        if (parts.Length > 1)
                            nhaCungCap = parts[1].Trim();
                    }
                    else if (line.StartsWith("STT,Sản phẩm,Đơn vị,Số lượng,Đơn giá nhập,Thành tiền"))
                    {
                        // Bắt đầu đọc dữ liệu sản phẩm
                        currentLine++;
                        break;
                    }
                    
                    currentLine++;
                }

                // Đọc dữ liệu sản phẩm
                while (currentLine < lines.Length)
                {
                    string line = lines[currentLine];
                    
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("Tổng tiền:"))
                        break;
                    
                    var parts = line.Split(',');
                    if (parts.Length >= 6)
                    {
                        try
                        {
                            // parts[0] = STT
                            string tenSP = parts[1].Trim('"', ' ');
                            string donVi = parts[2].Trim();
                            int soLuong = int.Parse(parts[3].Trim());
                            decimal donGia = decimal.Parse(parts[4].Trim());
                            decimal thanhTien = decimal.Parse(parts[5].Trim());
                            
                            sanPhamList.Add((tenSP, donVi, soLuong, donGia, thanhTien));
                        }
                        catch
                        {
                            // Bỏ qua dòng lỗi
                        }
                    }
                    
                    currentLine++;
                }

                // Hiển thị dialog xác nhận
                if (sanPhamList.Count > 0)
                {
                    string message = $"Đã đọc được:\n" +
                                   $"- Nhà cung cấp: {nhaCungCap}\n" +
                                   $"- Số lượng sản phẩm: {sanPhamList.Count}\n\n" +
                                   $"Bạn có muốn nhập phiếu nhập này vào hệ thống?";
                    
                    if (MessageBox.Show(message, "Xác nhận nhập dữ liệu", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SaveImportedData(nhaCungCap, ngayNhap ?? DateTime.Now, sanPhamList);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu sản phẩm trong file!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file CSV: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportFromExcel(string filePath)
        {
            try
            {
                // Đọc file HTML/XLS
                var htmlContent = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                
                // Parse HTML để lấy dữ liệu
                List<(string tenSP, string donVi, int soLuong, decimal donGia, decimal thanhTien)> sanPhamList = new List<(string, string, int, decimal, decimal)>();
                string nhaCungCap = "";
                DateTime? ngayNhap = null;

                // Tìm thông tin cơ bản
                var lines = htmlContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                
                bool foundTable = false;
                bool inDataSection = false;
                
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    
                    // Tìm nhà cung cấp
                    if (line.Contains("Nhà cung cấp:") && i + 1 < lines.Length)
                    {
                        string nextLine = lines[i + 1].Trim();
                        if (nextLine.Contains("<td>"))
                        {
                            nhaCungCap = ExtractTextFromHtml(nextLine);
                        }
                    }
                    
                    // Tìm ngày nhập
                    if (line.Contains("Ngày nhập:") && i + 1 < lines.Length)
                    {
                        string nextLine = lines[i + 1].Trim();
                        if (nextLine.Contains("<td>"))
                        {
                            string dateStr = ExtractTextFromHtml(nextLine);
                            if (DateTime.TryParse(dateStr, out DateTime date))
                                ngayNhap = date;
                        }
                    }
                    
                    // Tìm bảng sản phẩm
                    if (line.Contains("<th>STT</th>"))
                    {
                        foundTable = true;
                        continue;
                    }
                    
                    if (foundTable && line.Contains("<tbody>"))
                    {
                        inDataSection = true;
                        continue;
                    }
                    
                    if (inDataSection && line.Contains("</tbody>"))
                    {
                        break;
                    }
                    
                    // Đọc dữ liệu sản phẩm
                    if (inDataSection && line.Contains("<tr>"))
                    {
                        try
                        {
                            List<string> rowData = new List<string>();
                            int j = i + 1;
                            
                            while (j < lines.Length && !lines[j].Trim().Contains("</tr>"))
                            {
                                if (lines[j].Trim().Contains("<td"))
                                {
                                    rowData.Add(ExtractTextFromHtml(lines[j].Trim()));
                                }
                                j++;
                            }
                            
                            if (rowData.Count >= 6)
                            {
                                // rowData[0] = STT
                                string tenSP = rowData[1];
                                string donVi = rowData[2];
                                
                                if (int.TryParse(rowData[3].Replace(",", ""), out int soLuong) &&
                                    decimal.TryParse(rowData[4].Replace(",", ""), out decimal donGia) &&
                                    decimal.TryParse(rowData[5].Replace(",", "").Replace(" đ", ""), out decimal thanhTien))
                                {
                                    sanPhamList.Add((tenSP, donVi, soLuong, donGia, thanhTien));
                                }
                            }
                            
                            i = j;
                        }
                        catch
                        {
                            // Bỏ qua dòng lỗi
                        }
                    }
                }

                // Hiển thị dialog xác nhận
                if (sanPhamList.Count > 0)
                {
                    string message = $"Đã đọc được:\n" +
                                   $"- Nhà cung cấp: {nhaCungCap}\n" +
                                   $"- Số lượng sản phẩm: {sanPhamList.Count}\n\n" +
                                   $"Bạn có muốn nhập phiếu nhập này vào hệ thống?";
                    
                    if (MessageBox.Show(message, "Xác nhận nhập dữ liệu", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SaveImportedData(nhaCungCap, ngayNhap ?? DateTime.Now, sanPhamList);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu sản phẩm trong file!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file Excel: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ExtractTextFromHtml(string html)
        {
            // Loại bỏ các tag HTML
            string text = System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", string.Empty);
            // Decode HTML entities
            text = System.Net.WebUtility.HtmlDecode(text);
            return text.Trim();
        }

        private void SaveImportedData(string tenNhaCungCap, DateTime ngayNhap, 
            List<(string tenSP, string donVi, int soLuong, decimal donGia, decimal thanhTien)> sanPhamList)
        {
            try
            {
                var nhaCungCapBUS = new NhaCungCap_BUS();
                var sanPhamBUS = new SanPham_BUS();
                var phieuNhapBUS = new PhieuNhap_BUS();

                // Tìm nhà cung cấp
                var nhaCungCapList = nhaCungCapBUS.GetAll();
                var nhaCungCap = nhaCungCapList.FirstOrDefault(ncc => ncc.TenNhaCungCap == tenNhaCungCap);
                
                if (nhaCungCap == null)
                {
                    MessageBox.Show($"Không tìm thấy nhà cung cấp '{tenNhaCungCap}' trong hệ thống!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tìm sản phẩm
                var allSanPham = sanPhamBUS.GetAll();
                List<ChiTietPhieuNhapDTO> chiTietList = new List<ChiTietPhieuNhapDTO>();
                decimal tongTien = 0;

                foreach (var sp in sanPhamList)
                {
                    var sanPham = allSanPham.FirstOrDefault(s => s.TenSanPham == sp.tenSP);
                    
                    if (sanPham != null)
                    {
                        chiTietList.Add(new ChiTietPhieuNhapDTO
                        {
                            MaSanPham = sanPham.MaSanPham,
                            SoLuong = sp.soLuong,
                            DonGiaNhap = sp.donGia,
                            ThanhTien = sp.thanhTien
                        });
                        
                        tongTien += sp.thanhTien;
                    }
                    else
                    {
                        MessageBox.Show($"Sản phẩm '{sp.tenSP}' không tồn tại trong hệ thống!\nSẽ bỏ qua sản phẩm này.", 
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (chiTietList.Count == 0)
                {
                    MessageBox.Show("Không có sản phẩm hợp lệ để nhập!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo phiếu nhập mới
                PhieuNhapDTO phieuNhap = new PhieuNhapDTO
                {
                    MaNhaCungCap = nhaCungCap.MaNhaCungCap,
                    NgayNhap = ngayNhap,
                    TongTien = tongTien,
                    ChiTietPhieuNhaps = chiTietList
                };

                // Lưu vào database
                var result = phieuNhapBUS.AddPhieuNhap(phieuNhap);

                if (result != null && result.MaPhieuNhap > 0)
                {
                    MessageBox.Show($"Nhập phiếu nhập thành công!\n" +
                                  $"- Mã phiếu: PN{result.MaPhieuNhap:D3}\n" +
                                  $"- Số sản phẩm: {chiTietList.Count}\n" +
                                  $"- Tổng tiền: {tongTien:N0} đ", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadData(); // Reload danh sách
                }
                else
                {
                    MessageBox.Show("Lỗi khi lưu phiếu nhập vào database!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                dgvPhieuNhap.Rows.Clear();
                
                var phieuNhapBUS = new PhieuNhap_BUS();
                var nhaCungCapBUS = new NhaCungCap_BUS();
                
                var phieuNhapList = phieuNhapBUS.GetPhieuNhap();
                var nhaCungCapList = nhaCungCapBUS.GetAll();
                
                foreach (var phieuNhap in phieuNhapList)
                {
                    // Tìm tên nhà cung cấp
                    var nhaCungCap = nhaCungCapList.FirstOrDefault(ncc => ncc.MaNhaCungCap == phieuNhap.MaNhaCungCap);
                    string tenNhaCungCap = nhaCungCap?.TenNhaCungCap ?? "N/A";
                    
                    dgvPhieuNhap.Rows.Add(
                        $"PN{phieuNhap.MaPhieuNhap:D3}",
                        phieuNhap.NgayNhap?.ToString("dd/MM/yyyy") ?? "N/A",
                        tenNhaCungCap,
                        phieuNhap.TongTien ?? 0
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformSearch()
        {
            try
            {
                dgvPhieuNhap.Rows.Clear();
                
                var phieuNhapBUS = new PhieuNhap_BUS();
                var nhaCungCapBUS = new NhaCungCap_BUS();
                
                var phieuNhapList = phieuNhapBUS.GetPhieuNhap();
                var nhaCungCapList = nhaCungCapBUS.GetAll();
                
                // Lấy từ khóa tìm kiếm
                string searchText = txtSearch.Text.Trim().ToLower();
                
                // Lọc dữ liệu
                var filteredList = phieuNhapList.Where(pn =>
                {
                    // Tìm theo mã phiếu
                    if ($"pn{pn.MaPhieuNhap:d3}".Contains(searchText))
                        return true;
                    
                    // Tìm theo nhà cung cấp
                    var nhaCungCap = nhaCungCapList.FirstOrDefault(ncc => ncc.MaNhaCungCap == pn.MaNhaCungCap);
                    if (nhaCungCap != null && nhaCungCap.TenNhaCungCap.ToLower().Contains(searchText))
                        return true;
                    
                    // Tìm theo ngày
                    if (pn.NgayNhap.HasValue && pn.NgayNhap.Value.ToString("dd/MM/yyyy").Contains(searchText))
                        return true;
                    
                    return false;
                }).ToList();
                
                // Hiển thị kết quả
                foreach (var phieuNhap in filteredList)
                {
                    var nhaCungCap = nhaCungCapList.FirstOrDefault(ncc => ncc.MaNhaCungCap == phieuNhap.MaNhaCungCap);
                    string tenNhaCungCap = nhaCungCap?.TenNhaCungCap ?? "N/A";
                    
                    dgvPhieuNhap.Rows.Add(
                        $"PN{phieuNhap.MaPhieuNhap:D3}",
                        phieuNhap.NgayNhap?.ToString("dd/MM/yyyy") ?? "N/A",
                        tenNhaCungCap,
                        phieuNhap.TongTien ?? 0
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhaCungCapFilter()
        {
            try
            {
                var nhaCungCapBUS = new NhaCungCap_BUS();
                var nhaCungCapList = nhaCungCapBUS.GetAll();
                
                cboSupplier.Items.Clear();
                cboSupplier.Items.Add("🏢 Nhà cung cấp");
                cboSupplier.Items.Add("Tất cả");
                
                foreach (var ncc in nhaCungCapList)
                {
                    cboSupplier.Items.Add(ncc.TenNhaCungCap);
                }
                
                cboSupplier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải nhà cung cấp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            try
            {
                // Kiểm tra null
                if (cboTimePeriod == null || cboSupplier == null || dgvPhieuNhap == null)
                    return;
                
                dgvPhieuNhap.Rows.Clear();
                
                var phieuNhapBUS = new PhieuNhap_BUS();
                var nhaCungCapBUS = new NhaCungCap_BUS();
                
                var phieuNhapList = phieuNhapBUS.GetPhieuNhap();
                var nhaCungCapList = nhaCungCapBUS.GetAll();
                
                // Filter theo thời gian
                if (cboTimePeriod.SelectedIndex > 0)
                {
                    DateTime today = DateTime.Now.Date;
                    
                    phieuNhapList = cboTimePeriod.SelectedIndex switch
                    {
                        1 => phieuNhapList.Where(pn => pn.NgayNhap.HasValue && pn.NgayNhap.Value.Date == today).ToList(), // Hôm nay
                        2 => phieuNhapList.Where(pn => pn.NgayNhap.HasValue && 
                                                       pn.NgayNhap.Value.Date >= today.AddDays(-(int)today.DayOfWeek) &&
                                                       pn.NgayNhap.Value.Date <= today).ToList(), // Tuần này
                        3 => phieuNhapList.Where(pn => pn.NgayNhap.HasValue && 
                                                       pn.NgayNhap.Value.Month == today.Month &&
                                                       pn.NgayNhap.Value.Year == today.Year).ToList(), // Tháng này
                        _ => phieuNhapList.ToList() // Tất cả
                    };
                }
                
                // Filter theo nhà cung cấp
                if (cboSupplier.SelectedIndex > 1) // Skip "🏢 Nhà cung cấp" và "Tất cả"
                {
                    string selectedSupplier = cboSupplier.SelectedItem?.ToString() ?? "";
                    var ncc = nhaCungCapList.FirstOrDefault(n => n.TenNhaCungCap == selectedSupplier);
                    
                    if (ncc != null)
                    {
                        phieuNhapList = phieuNhapList.Where(pn => pn.MaNhaCungCap == ncc.MaNhaCungCap).ToList();
                    }
                }
                
                // Hiển thị kết quả
                foreach (var phieuNhap in phieuNhapList)
                {
                    var nhaCungCap = nhaCungCapList.FirstOrDefault(ncc => ncc.MaNhaCungCap == phieuNhap.MaNhaCungCap);
                    string tenNhaCungCap = nhaCungCap?.TenNhaCungCap ?? "N/A";
                    
                    dgvPhieuNhap.Rows.Add(
                        $"PN{phieuNhap.MaPhieuNhap:D3}",
                        phieuNhap.NgayNhap?.ToString("dd/MM/yyyy") ?? "N/A",
                        tenNhaCungCap,
                        phieuNhap.TongTien ?? 0
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Button CreateButton(string text, Point location, Size size, Color bgColor, Color hoverColor, float fontSize)
        {
            Button btn = new Button
            {
                Text = text,
                Location = location,
                Size = size,
                BackColor = bgColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", fontSize, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = hoverColor;
            
            return btn;
        }

        private void InitializeSearchSection()
        {
            // Search Panel - Dời xuống để không chồng lên titlePanel
            Panel searchPanel = new Panel
            {
                Location = new Point(30, 190),
                Size = new Size(1110, 52),
                BackColor = Color.White
            };
            mainPanel.Controls.Add(searchPanel);

            // Search Icon
            Label searchIcon = new Label
            {
                Text = "🔍",
                Location = new Point(18, 14),
                Size = new Size(30, 25),
                Font = new Font("Segoe UI", 13),
                ForeColor = Color.FromArgb(108, 117, 125)
            };
            searchPanel.Controls.Add(searchIcon);

            // Search TextBox
            txtSearch = new TextBox
            {
                Location = new Point(55, 14),
                Size = new Size(1035, 28),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.None,
                PlaceholderText = "Tìm kiếm theo mã phiếu, nhà cung cấp...",
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            txtSearch.TextChanged += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                    LoadData();
                else
                    PerformSearch();
            };
            searchPanel.Controls.Add(txtSearch);

            // Border for search panel
            searchPanel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(206, 212, 218), 2))
                {
                    Rectangle rect = new Rectangle(0, 0, searchPanel.Width - 1, searchPanel.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };
        }

        private void InitializeFilterSection()
        {
            Panel filterPanel = new Panel
            {
                Location = new Point(30, 255),
                Size = new Size(1110, 50),
                BackColor = Color.White
            };
            mainPanel.Controls.Add(filterPanel);

            // Time Period ComboBox
            cboTimePeriod = CreateComboBox(
                new Point(0, 10),
                new Size(240, 32),
                new[] { "📅 Thời gian", "Hôm nay", "Tuần này", "Tháng này", "Tất cả" }
            );
            cboTimePeriod.SelectedIndexChanged += (s, e) => ApplyFilters();
            filterPanel.Controls.Add(cboTimePeriod);

            // Supplier ComboBox
            cboSupplier = CreateComboBox(
                new Point(260, 10),
                new Size(350, 32),
                new[] { "🏢 Nhà cung cấp", "Tất cả" }
            );
            cboSupplier.SelectedIndexChanged += (s, e) => ApplyFilters();
            filterPanel.Controls.Add(cboSupplier);
            
            // Load nhà cung cấp vào ComboBox
            LoadNhaCungCapFilter();
        }

        private ComboBox CreateComboBox(Point location, Size size, string[] items)
        {
            ComboBox combo = new ComboBox
            {
                Location = location,
                Size = size,
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            combo.Items.AddRange(items);
            combo.SelectedIndex = 0;
            return combo;
        }

        private void InitializeDataGridView()
        {
            dgvPhieuNhap = new DataGridView
            {
                Location = new Point(30, 320),
                Size = new Size(1110, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ColumnHeadersHeight = 52,
                RowTemplate = { Height = 48 },
                Font = new Font("Segoe UI", 10),
                GridColor = Color.FromArgb(222, 226, 230),
                EnableHeadersVisualStyles = false,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                MultiSelect = false,
                RowHeadersVisible = false
            };

            ConfigureDataGridViewStyle();
            AddDataGridViewColumns();
            AddDataGridViewEvents();

            mainPanel.Controls.Add(dgvPhieuNhap);
        }

        private void ConfigureDataGridViewStyle()
        {
            // Column Headers Style
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 243, 245);
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 58, 64);
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Padding = new Padding(8);

            // Row Style
            dgvPhieuNhap.RowsDefaultCellStyle.BackColor = Color.White;
            dgvPhieuNhap.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvPhieuNhap.DefaultCellStyle.SelectionBackColor = Color.FromArgb(207, 226, 255);
            dgvPhieuNhap.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgvPhieuNhap.DefaultCellStyle.Padding = new Padding(10, 6, 10, 6);
            dgvPhieuNhap.DefaultCellStyle.ForeColor = Color.FromArgb(73, 80, 87);
            dgvPhieuNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void AddDataGridViewColumns()
        {
            // Mã phiếu
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPhieu",
                HeaderText = "Mã phiếu",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(13, 110, 253),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Ngày nhập
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayNhap",
                HeaderText = "Ngày nhập",
                Width = 170,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Nhà cung cấp
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NhaCungCap",
                HeaderText = "Nhà cung cấp",
                Width = 500,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(15, 6, 10, 6)
                }
            });

            // Tổng tiền
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TongTien",
                HeaderText = "Tổng tiền (VNĐ)",
                Width = 200,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(220, 53, 69),
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Padding = new Padding(10, 6, 20, 6)
                }
            });
        }

        private void AddDataGridViewEvents()
        {
            // Hover effect
            dgvPhieuNhap.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    dgvPhieuNhap.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(233, 236, 239);
                }
            };

            dgvPhieuNhap.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    dgvPhieuNhap.Rows[e.RowIndex].DefaultCellStyle.BackColor = 
                        e.RowIndex % 2 == 0 ? Color.White : Color.FromArgb(248, 249, 250);
                }
            };

            // Double click to view details
            dgvPhieuNhap.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    string maPhieuStr = dgvPhieuNhap.Rows[e.RowIndex].Cells["MaPhieu"].Value?.ToString() ?? "";
                    
                    // Parse mã phiếu nhập (ví dụ: "PN001" -> 1)
                    if (maPhieuStr.StartsWith("PN") && int.TryParse(maPhieuStr.Substring(2), out int maPhieuNhap))
                    {
                        Form_XemChiTietPhieuNhap formChiTiet = new Form_XemChiTietPhieuNhap(maPhieuNhap);
                        formChiTiet.ShowDialog();
                    }
                }
            };

            // Click event để thêm cursor pointer
            dgvPhieuNhap.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    dgvPhieuNhap.Cursor = Cursors.Hand;
                }
            };
        }
    }
}
