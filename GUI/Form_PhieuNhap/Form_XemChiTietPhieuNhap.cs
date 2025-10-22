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
        private readonly Color primaryColor = Color.FromArgb(33, 150, 243);
        private readonly Color successColor = Color.FromArgb(76, 175, 80);
        private readonly Color backgroundColor = Color.FromArgb(250, 251, 252);
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
            InitializeProductSection();
            InitializeTotalSection();
            InitializeExportExcelButton();
            InitializeCloseButton();
        }

        private void InitializeMainPanel()
        {
            mainPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1100, 750),
                BackColor = cardColor,
                Padding = new Padding(0),
                AutoScroll = true
            };
            this.Controls.Add(mainPanel);
        }

        private void InitializeHeader()
        {
            headerPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1100, 80),
                BackColor = cardColor
            };
            mainPanel.Controls.Add(headerPanel);

            // Icon and Title
            Label lblIcon = new Label
            {
                Text = "📋",
                Location = new Point(35, 20),
                Size = new Size(40, 40),
                Font = new Font("Segoe UI", 20),
                BackColor = Color.Transparent
            };
            headerPanel.Controls.Add(lblIcon);

            Label lblTitle = new Label
            {
                Text = "Chi tiết phiếu nhập",
                Location = new Point(80, 20),
                Size = new Size(450, 40),
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                BackColor = Color.Transparent,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            headerPanel.Controls.Add(lblTitle);

            // Bottom border
            Panel line = new Panel
            {
                Location = new Point(0, 79),
                Size = new Size(1100, 1),
                BackColor = borderColor
            };
            headerPanel.Controls.Add(line);
        }

        private void InitializeInfoSection()
        {
            infoSectionPanel = new Panel
            {
                Location = new Point(35, 100),
                Size = new Size(1030, 130),
                BackColor = backgroundColor,
                Padding = new Padding(20)
            };
            mainPanel.Controls.Add(infoSectionPanel);

            // Section title
            Label lblSectionIcon = new Label
            {
                Text = "ℹ️",
                Location = new Point(0, 0),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 14),
                BackColor = Color.Transparent
            };
            infoSectionPanel.Controls.Add(lblSectionIcon);

            Label lblSection = new Label
            {
                Text = "Thông tin phiếu nhập",
                Location = new Point(35, 0),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                BackColor = Color.Transparent
            };
            infoSectionPanel.Controls.Add(lblSection);

            int startY = 50;
            int labelW = 150;
            int col1X = 0;
            int col2X = 500;

            // Mã phiếu nhập
            CreateStyledLabel("Mã phiếu nhập:", col1X, startY, labelW, infoSectionPanel);
            lblMaPhieuNhap = CreateStyledValueLabel(col1X + labelW, startY, 200, infoSectionPanel);

            // Ngày nhập
            CreateStyledLabel("Ngày nhập:", col2X, startY, labelW, infoSectionPanel);
            lblNgayNhap = CreateStyledValueLabel(col2X + labelW, startY, 330, infoSectionPanel);

            // Nhà cung cấp - Tăng width của label để hiển thị đầy đủ
            CreateStyledLabel("Nhà cung cấp:", col1X, startY + 40, labelW, infoSectionPanel);
            lblNhaCungCap = CreateStyledValueLabel(col1X + labelW, startY + 40, 830, infoSectionPanel);
        }

        private void InitializeProductSection()
        {
            productSectionPanel = new Panel
            {
                Location = new Point(35, 250),
                Size = new Size(1030, 395),
                BackColor = backgroundColor,
                Padding = new Padding(20)
            };
            mainPanel.Controls.Add(productSectionPanel);

            // Section title
            Label lblSectionIcon = new Label
            {
                Text = "📦",
                Location = new Point(0, 0),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 14),
                BackColor = Color.Transparent
            };
            productSectionPanel.Controls.Add(lblSectionIcon);

            Label lblSection = new Label
            {
                Text = "Danh sách sản phẩm",
                Location = new Point(35, 0),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                BackColor = Color.Transparent
            };
            productSectionPanel.Controls.Add(lblSection);

            // DataGridView
            dgvProducts = new DataGridView
            {
                Location = new Point(0, 45),
                Size = new Size(990, 330),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10)
            };

            // Columns
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenSanPham",
                HeaderText = "Sản phẩm",
                FillWeight = 35,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Padding = new Padding(10, 5, 10, 5),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonVi",
                HeaderText = "Đơn vị",
                FillWeight = 12,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "Số lượng",
                FillWeight = 13,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Đơn giá nhập",
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Padding = new Padding(10, 5, 10, 5)
                }
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ThanhTien",
                HeaderText = "Thành tiền",
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Padding = new Padding(10, 5, 10, 5),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = primaryColor
                }
            });

            // Style
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.ColumnHeadersHeight = 40;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.RowTemplate.Height = 38;
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 250);

            productSectionPanel.Controls.Add(dgvProducts);
        }

        private void InitializeTotalSection()
        {
            // Panel chứa tổng tiền - Thu nhỏ để chứa nút
            Panel totalPanel = new Panel
            {
                Location = new Point(35, 655),
                Size = new Size(580, 50),
                BackColor = Color.FromArgb(232, 245, 233),
                Padding = new Padding(10)
            };
            mainPanel.Controls.Add(totalPanel);

            // Label "Tổng tiền:"
            Label lblTongTienText = new Label
            {
                Text = "Tổng tiền:",
                Location = new Point(250, 10),
                Size = new Size(130, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.Transparent
            };
            totalPanel.Controls.Add(lblTongTienText);

            // Label hiển thị tổng tiền
            lblTongTien = new Label
            {
                Text = "0 đ",
                Location = new Point(390, 10),
                Size = new Size(180, 30),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = successColor,
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.Transparent
            };
            totalPanel.Controls.Add(lblTongTien);
        }

        private void InitializeExportExcelButton()
        {
            btnExportExcel = new Button
            {
                Text = "📊 Xuất Excel",
                Location = new Point(625, 655),
                Size = new Size(200, 50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnExportExcel.FlatAppearance.BorderSize = 0;
            btnExportExcel.MouseEnter += (s, e) => btnExportExcel.BackColor = Color.FromArgb(33, 136, 56);
            btnExportExcel.MouseLeave += (s, e) => btnExportExcel.BackColor = Color.FromArgb(40, 167, 69);
            btnExportExcel.Click += BtnExportExcel_Click;

            mainPanel.Controls.Add(btnExportExcel);
            btnExportExcel.BringToFront();
        }

        private void InitializeCloseButton()
        {
            btnClose = new Button
            {
                Text = "Đóng",
                Location = new Point(835, 655),
                Size = new Size(230, 50),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.MouseEnter += (s, e) => btnClose.BackColor = Color.FromArgb(130, 130, 130);
            btnClose.MouseLeave += (s, e) => btnClose.BackColor = Color.FromArgb(158, 158, 158);
            btnClose.Click += (s, e) => this.Close();

            mainPanel.Controls.Add(btnClose);
            btnClose.BringToFront();
        }

        private Label CreateStyledLabel(string text, int x, int y, int width, Panel parent)
        {
            Label label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 35),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = textSecondaryColor,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };
            parent.Controls.Add(label);
            return label;
        }

        private Label CreateStyledValueLabel(int x, int y, int width, Panel parent)
        {
            Label label = new Label
            {
                Text = "",
                Location = new Point(x, y),
                Size = new Size(width, 35),
                Font = new Font("Segoe UI", 11),
                ForeColor = textPrimaryColor,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 10, 0)
            };
            parent.Controls.Add(label);
            return label;
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
