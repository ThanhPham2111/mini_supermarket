using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.PhieuNhap
{
    public partial class Form_XemChiTietPhieuNhap : Form
    {
        // WinAPI for shadow effect
        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private Panel mainPanel = null!;
        private Panel headerPanel = null!;
        private Panel infoSectionPanel = null!;
        private Panel productSectionPanel = null!;
        private DataGridView dgvProducts = null!;
        private Label lblMaPhieuNhap = null!;
        private Label lblNgayNhap = null!;
        private Label lblNhaCungCap = null!;
        private Label lblTongTien = null!;
        private Button btnClose = null!;
        private Button btnExportExcel = null!;

        private PhieuNhapDTO? phieuNhap;
        private int maPhieuNhap;

        // Modern color scheme
        private readonly Color primaryColor = Color.FromArgb(0, 120, 215);      // Standard Blue
        private readonly Color successColor = Color.FromArgb(16, 137, 62);      // Standard Green
        private readonly Color backgroundColor = Color.WhiteSmoke;              // Light Gray
        private readonly Color cardColor = Color.White;
        private readonly Color borderColor = Color.FromArgb(224, 224, 224);
        private readonly Color textPrimaryColor = Color.FromArgb(33, 33, 33);
        private readonly Color textSecondaryColor = Color.FromArgb(117, 117, 117);

        public Form_XemChiTietPhieuNhap(int maPhieuNhap)
        {
            this.maPhieuNhap = maPhieuNhap;
            InitializeComponent();
            LoadPhieuNhapData();
        }

        private void LoadPhieuNhapData()
        {
            try
            {
                var phieuNhapBUS = new PhieuNhap_BUS();
                var nhaCungCapBUS = new NhaCungCap_BUS();
                var sanPhamBUS = new SanPham_BUS();

                // Lấy thông tin phiếu nhập với chi tiết
                phieuNhap = phieuNhapBUS.GetPhieuNhapById(maPhieuNhap);

                if (phieuNhap == null)
                {
                    MessageBox.Show("Không tìm thấy phiếu nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Hiển thị thông tin chung
                lblMaPhieuNhap.Text = $"PN{phieuNhap.MaPhieuNhap:D3}";
                lblNgayNhap.Text = phieuNhap.NgayNhap?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";

                // Lấy tên nhà cung cấp
                var nhaCungCapList = nhaCungCapBUS.GetAll();
                var nhaCungCap = nhaCungCapList.FirstOrDefault(ncc => ncc.MaNhaCungCap == phieuNhap.MaNhaCungCap);
                lblNhaCungCap.Text = nhaCungCap?.TenNhaCungCap ?? "N/A";

                // Hiển thị chi tiết sản phẩm
                dgvProducts.Rows.Clear();
                decimal tongTien = 0;

                var sanPhamList = sanPhamBUS.GetAll();

                if (phieuNhap.ChiTietPhieuNhaps != null && phieuNhap.ChiTietPhieuNhaps.Count > 0)
                {
                    foreach (var chiTiet in phieuNhap.ChiTietPhieuNhaps)
                    {
                        var sanPham = sanPhamList.FirstOrDefault(sp => sp.MaSanPham == chiTiet.MaSanPham);
                        string tenSanPham = sanPham?.TenSanPham ?? "N/A";
                        string tenDonVi = sanPham?.TenDonVi ?? "N/A";

                        dgvProducts.Rows.Add(
                            tenSanPham,
                            tenDonVi,
                            chiTiet.SoLuong,
                            chiTiet.DonGiaNhap,
                            chiTiet.ThanhTien
                        );

                        tongTien += chiTiet.ThanhTien;
                    }
                }
                else
                {
                    // Nếu không có chi tiết, hiển thị thông báo
                    MessageBox.Show("Phiếu nhập này chưa có sản phẩm nào!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                lblTongTien.Text = tongTien.ToString("N0") + " đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Chi tiết phiếu nhập";
            this.Size = new Size(1100, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = backgroundColor;
            this.MaximizeBox = false;

            InitializeMainPanel();
            InitializeHeader();
            InitializeInfoSection();
            InitializeFooterSection(); // Replaces Total, Export, Close
            InitializeProductSection(); // Fill remaining
        }

        private void InitializeMainPanel()
        {
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(20)
            };
            this.Controls.Add(mainPanel);
        }

        private void InitializeHeader()
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = cardColor,
                Padding = new Padding(15, 10, 15, 10)
            };
            mainPanel.Controls.Add(headerPanel);

            Label lblTitle = new Label
            {
                Text = "CHI TIẾT PHIẾU NHẬP",
                Dock = DockStyle.Left,
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = primaryColor,
                TextAlign = ContentAlignment.MiddleLeft
            };
            headerPanel.Controls.Add(lblTitle);
        }

        private void InitializeInfoSection()
        {
            infoSectionPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = cardColor,
                Padding = new Padding(20),
                Margin = new Padding(0, 20, 0, 20)
            };
            mainPanel.Controls.Add(infoSectionPanel);
            infoSectionPanel.BringToFront();

            TableLayoutPanel tblInfo = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 2,
                Padding = new Padding(0)
            };
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            
            infoSectionPanel.Controls.Add(tblInfo);

            // Helper to add label/value pairs
            void AddPair(string label, Label valueLabel, int row, int col)
            {
                Label lbl = new Label { Text = label, Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = textSecondaryColor };
                valueLabel.Anchor = AnchorStyles.Left;
                valueLabel.AutoSize = true;
                valueLabel.Font = new Font("Segoe UI", 11);
                
                tblInfo.Controls.Add(lbl, col, row);
                tblInfo.Controls.Add(valueLabel, col + 1, row);
            }

            lblMaPhieuNhap = new Label();
            lblNgayNhap = new Label();
            lblNhaCungCap = new Label();

            AddPair("Mã phiếu:", lblMaPhieuNhap, 0, 0);
            AddPair("Ngày nhập:", lblNgayNhap, 0, 2);
            AddPair("Nhà cung cấp:", lblNhaCungCap, 1, 0);
            tblInfo.SetColumnSpan(lblNhaCungCap, 3);
        }

        private void InitializeProductSection()
        {
            productSectionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = cardColor,
                Padding = new Padding(20),
                Margin = new Padding(0, 20, 0, 0)
            };
            mainPanel.Controls.Add(productSectionPanel);
            productSectionPanel.BringToFront();

            // Label tiêu đề
            Label lblProductTitle = new Label
            {
                Text = "Danh sách sản phẩm",
                Dock = DockStyle.Top,
                Height = 35,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Panel chứa DataGridView với padding top
            Panel dgvContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 40, 0, 0) // Tạo khoảng cách phía trên
            };

            dgvProducts = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10),
                GridColor = Color.FromArgb(220, 220, 220),
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            };

            // Header Style - màu xanh đậm nổi bật
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvProducts.ColumnHeadersHeight = 45;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProducts.EnableHeadersVisualStyles = false;
            
            // Row Style
            dgvProducts.RowTemplate.Height = 40;
            dgvProducts.RowsDefaultCellStyle.BackColor = Color.White;
            dgvProducts.RowsDefaultCellStyle.Padding = new Padding(5);
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(207, 226, 255);
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Columns với header rõ ràng
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "TenSanPham",
                HeaderText = "TÊN SẢN PHẨM", 
                FillWeight = 40,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                }
            });
            
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "DonVi",
                HeaderText = "ĐƠN VỊ", 
                FillWeight = 12,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Alignment = DataGridViewContentAlignment.MiddleCenter 
                }
            });
            
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "SoLuong",
                HeaderText = "SỐ LƯỢNG", 
                FillWeight = 12,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                }
            });
            
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "DonGia",
                HeaderText = "ĐƠN GIÁ (VNĐ)", 
                FillWeight = 18,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "N0", 
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Padding = new Padding(0, 0, 10, 0)
                }
            });
            
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "ThanhTien",
                HeaderText = "THÀNH TIỀN (VNĐ)", 
                FillWeight = 18,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "N0", 
                    Alignment = DataGridViewContentAlignment.MiddleRight, 
                    Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                    ForeColor = Color.FromArgb(220, 53, 69),
                    Padding = new Padding(0, 0, 10, 0)
                }
            });

            // Thêm dgvProducts vào container
            dgvContainer.Controls.Add(dgvProducts);
            
            // Thêm container (Fill) trước
            productSectionPanel.Controls.Add(dgvContainer);
            
            // Sau đó thêm label (Top) và BringToFront
            productSectionPanel.Controls.Add(lblProductTitle);
            lblProductTitle.BringToFront();
        }

        private void InitializeFooterSection()
        {
            Panel footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = cardColor,
                Padding = new Padding(20, 10, 20, 10)
            };
            mainPanel.Controls.Add(footerPanel);
            footerPanel.BringToFront();

            // Total Label
            Label lblTotalText = new Label
            {
                Text = "Tổng tiền:",
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Left
            };
            
            lblTongTien = new Label
            {
                Text = "0 đ",
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = successColor,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Left,
                Padding = new Padding(10, 0, 0, 0)
            };

            Panel totalContainer = new Panel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                Padding = new Padding(0, 15, 0, 0)
            };
            totalContainer.Controls.Add(lblTongTien);
            totalContainer.Controls.Add(lblTotalText);
            footerPanel.Controls.Add(totalContainer);

            // Buttons
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                Width = 300,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 10, 0, 0)
            };
            footerPanel.Controls.Add(buttonPanel);

            btnClose = new Button
            {
                Text = "Đóng",
                Size = new Size(120, 45),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(241, 243, 245),
                ForeColor = textPrimaryColor,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(5, 0, 0, 0)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            btnExportExcel = new Button
            {
                Text = "📊 Xuất Excel",
                Size = new Size(150, 45),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(253, 126, 20),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(5, 0, 0, 0)
            };
            btnExportExcel.FlatAppearance.BorderSize = 0;
            btnExportExcel.MouseEnter += (s, e) => btnExportExcel.BackColor = Color.FromArgb(220, 110, 18);
            btnExportExcel.MouseLeave += (s, e) => btnExportExcel.BackColor = Color.FromArgb(253, 126, 20);
            btnExportExcel.Click += BtnExportExcel_Click;

            buttonPanel.Controls.Add(btnClose);
            buttonPanel.Controls.Add(btnExportExcel);
        }

        private void BtnExportExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                if (phieuNhap == null)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo SaveFileDialog
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel Files (*.xls)|*.xls|CSV Files (*.csv)|*.csv";
                    saveDialog.DefaultExt = "xls";
                    saveDialog.FileName = $"PhieuNhap_{lblMaPhieuNhap.Text}_{DateTime.Now:yyyyMMdd_HHmmss}";
                    saveDialog.Title = "Xuất phiếu nhập";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        string extension = Path.GetExtension(saveDialog.FileName).ToLower();
                        
                        if (extension == ".csv")
                        {
                            ExportToCSV(saveDialog.FileName);
                        }
                        else if (extension == ".xls")
                        {
                            ExportToExcel(saveDialog.FileName);
                        }
                        else
                        {
                            ExportToExcel(saveDialog.FileName);
                        }

                        MessageBox.Show("Xuất file thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Mở file sau khi xuất
                        if (MessageBox.Show("Bạn có muốn mở file vừa xuất?", "Xác nhận", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = saveDialog.FileName,
                                UseShellExecute = true
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCSV(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                // Header thông tin phiếu nhập
                writer.WriteLine("CHI TIẾT PHIẾU NHẬP");
                writer.WriteLine();
                writer.WriteLine($"Mã phiếu nhập:,{lblMaPhieuNhap.Text}");
                writer.WriteLine($"Ngày nhập:,{lblNgayNhap.Text}");
                writer.WriteLine($"Nhà cung cấp:,{lblNhaCungCap.Text}");
                writer.WriteLine();
                
                // Header bảng sản phẩm
                writer.WriteLine("STT,Sản phẩm,Đơn vị,Số lượng,Đơn giá nhập,Thành tiền");
                
                // Dữ liệu sản phẩm
                int stt = 1;
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.IsNewRow) continue;
                    
                    writer.WriteLine($"{stt}," +
                        $"\"{row.Cells["TenSanPham"].Value}\"," +
                        $"{row.Cells["DonVi"].Value}," +
                        $"{row.Cells["SoLuong"].Value}," +
                        $"{row.Cells["DonGia"].Value}," +
                        $"{row.Cells["ThanhTien"].Value}");
                    stt++;
                }
                
                writer.WriteLine();
                writer.WriteLine($",,,,Tổng tiền:,{lblTongTien.Text}");
            }
        }

        private void ExportToExcel(string filePath)
        {
            // Tạo file Excel bằng HTML format với Excel XML
            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                // HTML header cho Excel
                writer.WriteLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
                writer.WriteLine(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
                writer.WriteLine(" xmlns=\"http://www.w3.org/TR/REC-html40\">");
                writer.WriteLine("<head>");
                writer.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>");
                writer.WriteLine("<!--[if gte mso 9]>");
                writer.WriteLine("<xml>");
                writer.WriteLine("<x:ExcelWorkbook>");
                writer.WriteLine("<x:ExcelWorksheets>");
                writer.WriteLine("<x:ExcelWorksheet>");
                writer.WriteLine("<x:Name>Phiếu Nhập</x:Name>");
                writer.WriteLine("<x:WorksheetOptions>");
                writer.WriteLine("<x:Print>");
                writer.WriteLine("<x:ValidPrinterInfo/>");
                writer.WriteLine("</x:Print>");
                writer.WriteLine("</x:WorksheetOptions>");
                writer.WriteLine("</x:ExcelWorksheet>");
                writer.WriteLine("</x:ExcelWorksheets>");
                writer.WriteLine("</x:ExcelWorkbook>");
                writer.WriteLine("</xml>");
                writer.WriteLine("<![endif]-->");
                writer.WriteLine("<style>");
                writer.WriteLine("table { border-collapse: collapse; width: 100%; }");
                writer.WriteLine("th { background-color: #2196F3; color: white; font-weight: bold; padding: 10px; text-align: center; border: 1px solid #ddd; }");
                writer.WriteLine("td { padding: 8px; border: 1px solid #ddd; }");
                writer.WriteLine(".title { font-size: 18pt; font-weight: bold; text-align: center; padding: 20px; }");
                writer.WriteLine(".info { font-weight: bold; background-color: #f5f5f5; }");
                writer.WriteLine(".number { text-align: right; }");
                writer.WriteLine(".center { text-align: center; }");
                writer.WriteLine(".total { background-color: #E8F5E9; font-weight: bold; font-size: 14pt; color: #4CAF50; }");
                writer.WriteLine("</style>");
                writer.WriteLine("</head>");
                writer.WriteLine("<body>");
                
                // Title
                writer.WriteLine("<div class='title'>CHI TIẾT PHIẾU NHẬP</div>");
                writer.WriteLine("<br/>");
                
                // Thông tin phiếu nhập
                writer.WriteLine("<table style='width: 50%; border: none;'>");
                writer.WriteLine("<tr>");
                writer.WriteLine("<td class='info' style='width: 30%;'>Mã phiếu nhập:</td>");
                writer.WriteLine($"<td>{System.Security.SecurityElement.Escape(lblMaPhieuNhap.Text)}</td>");
                writer.WriteLine("</tr>");
                writer.WriteLine("<tr>");
                writer.WriteLine("<td class='info'>Ngày nhập:</td>");
                writer.WriteLine($"<td>{System.Security.SecurityElement.Escape(lblNgayNhap.Text)}</td>");
                writer.WriteLine("</tr>");
                writer.WriteLine("<tr>");
                writer.WriteLine("<td class='info'>Nhà cung cấp:</td>");
                writer.WriteLine($"<td>{System.Security.SecurityElement.Escape(lblNhaCungCap.Text)}</td>");
                writer.WriteLine("</tr>");
                writer.WriteLine("</table>");
                writer.WriteLine("<br/>");
                
                // Bảng sản phẩm
                writer.WriteLine("<table>");
                writer.WriteLine("<thead>");
                writer.WriteLine("<tr>");
                writer.WriteLine("<th>STT</th>");
                writer.WriteLine("<th>Sản phẩm</th>");
                writer.WriteLine("<th>Đơn vị</th>");
                writer.WriteLine("<th>Số lượng</th>");
                writer.WriteLine("<th>Đơn giá nhập</th>");
                writer.WriteLine("<th>Thành tiền</th>");
                writer.WriteLine("</tr>");
                writer.WriteLine("</thead>");
                writer.WriteLine("<tbody>");
                
                // Dữ liệu
                int stt = 1;
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.IsNewRow) continue;
                    
                    writer.WriteLine("<tr>");
                    writer.WriteLine($"<td class='center'>{stt}</td>");
                    writer.WriteLine($"<td>{System.Security.SecurityElement.Escape(row.Cells["TenSanPham"].Value?.ToString() ?? "")}</td>");
                    writer.WriteLine($"<td class='center'>{System.Security.SecurityElement.Escape(row.Cells["DonVi"].Value?.ToString() ?? "")}</td>");
                    writer.WriteLine($"<td class='center'>{row.Cells["SoLuong"].Value}</td>");
                    writer.WriteLine($"<td class='number'>{Convert.ToDecimal(row.Cells["DonGia"].Value):N0}</td>");
                    writer.WriteLine($"<td class='number'>{Convert.ToDecimal(row.Cells["ThanhTien"].Value):N0}</td>");
                    writer.WriteLine("</tr>");
                    stt++;
                }
                
                // Tổng tiền
                writer.WriteLine("<tr>");
                writer.WriteLine("<td colspan='5' class='total' style='text-align: right;'>Tổng tiền:</td>");
                writer.WriteLine($"<td class='total number'>{lblTongTien.Text}</td>");
                writer.WriteLine("</tr>");
                
                writer.WriteLine("</tbody>");
                writer.WriteLine("</table>");
                
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }
        }
    }
}
