using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.HoaDon.Dialog
{
    public partial class Dialog_XemLyDoHuy : Form
    {
        private Panel mainPanel;
        private Label lblTitle;
        private Label lblMaHoaDon;
        private Label lblNgayTao;
        private Label lblNgayHuy;
        private Label lblNhanVienHuy;
        private Label lblKhachHang;
        private Label lblLyDoHuy;
        private Label lblChiTietTitle;
        private DataGridView dgvChiTiet;
        private Label lblDiemHoanLai;
        private Button btnClose;

        public Dialog_XemLyDoHuy(
            HoaDonDTO hoaDon,
            List<ChiTietHoaDonDTO> chiTietList,
            string? lyDoHuy,
            DateTime? ngayHuy,
            string? tenNhanVienHuy,
            int? diemHoanLai,
            int? diemTichLuyBiTru = null,
            int? diemSuDungTraLai = null)
        {
            InitializeComponent(hoaDon, chiTietList, lyDoHuy, ngayHuy, tenNhanVienHuy, diemHoanLai, diemTichLuyBiTru, diemSuDungTraLai);
        }

        private void InitializeComponent(
            HoaDonDTO hoaDon,
            List<ChiTietHoaDonDTO> chiTietList,
            string? lyDoHuy,
            DateTime? ngayHuy,
            string? tenNhanVienHuy,
            int? diemHoanLai,
            int? diemTichLuyBiTru = null,
            int? diemSuDungTraLai = null)
        {
            this.Text = $"Thông tin hủy hóa đơn HD{hoaDon.MaHoaDon:D3}";
            this.Size = new Size(900, 650);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Padding = new Padding(20);

            // Main Panel
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };
            this.Controls.Add(mainPanel);

            int yPos = 10;

            // Title
            lblTitle = new Label
            {
                Text = $"THÔNG TIN HỦY HÓA ĐƠN HD{hoaDon.MaHoaDon:D3}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, yPos),
                ForeColor = Color.FromArgb(220, 53, 69)
            };
            mainPanel.Controls.Add(lblTitle);
            yPos += 40;

            // Mã hóa đơn
            var lblMaHoaDonLabel = new Label
            {
                Text = "Mã hóa đơn:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, yPos),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            mainPanel.Controls.Add(lblMaHoaDonLabel);

            lblMaHoaDon = new Label
            {
                Text = $"HD{hoaDon.MaHoaDon:D3}",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(150, yPos),
                ForeColor = Color.Black
            };
            mainPanel.Controls.Add(lblMaHoaDon);
            yPos += 30;

            // Ngày tạo hóa đơn
            var lblNgayTaoLabel = new Label
            {
                Text = "Ngày tạo hóa đơn:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, yPos),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            mainPanel.Controls.Add(lblNgayTaoLabel);

            lblNgayTao = new Label
            {
                Text = hoaDon.NgayLap?.ToString("dd/MM/yyyy HH:mm:ss") ?? "N/A",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(180, yPos),
                ForeColor = Color.Black
            };
            mainPanel.Controls.Add(lblNgayTao);
            yPos += 30;

            // Ngày hủy
            var lblNgayHuyLabel = new Label
            {
                Text = "Ngày hủy:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, yPos),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            mainPanel.Controls.Add(lblNgayHuyLabel);

            lblNgayHuy = new Label
            {
                Text = ngayHuy?.ToString("dd/MM/yyyy HH:mm:ss") ?? "N/A",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(150, yPos),
                ForeColor = Color.FromArgb(220, 53, 69)
            };
            mainPanel.Controls.Add(lblNgayHuy);
            yPos += 30;

            // Nhân viên hủy
            var lblNhanVienHuyLabel = new Label
            {
                Text = "Nhân viên hủy:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, yPos),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            mainPanel.Controls.Add(lblNhanVienHuyLabel);

            lblNhanVienHuy = new Label
            {
                Text = tenNhanVienHuy ?? "N/A",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(150, yPos),
                ForeColor = Color.Black
            };
            mainPanel.Controls.Add(lblNhanVienHuy);
            yPos += 30;

            // Khách hàng
            var lblKhachHangLabel = new Label
            {
                Text = "Khách hàng:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, yPos),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            mainPanel.Controls.Add(lblKhachHangLabel);

            lblKhachHang = new Label
            {
                Text = hoaDon.KhachHang ?? "Khách lẻ",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(150, yPos),
                ForeColor = Color.Black
            };
            mainPanel.Controls.Add(lblKhachHang);
            yPos += 30;

            // Điểm hoàn lại (chỉ hiển thị nếu có khách hàng)
            if (hoaDon.MaKhachHang.HasValue && diemHoanLai.HasValue)
            {
                var lblDiemHoanLaiLabel = new Label
                {
                    Text = "Điểm hoàn lại:",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(10, yPos),
                    ForeColor = Color.FromArgb(73, 80, 87)
                };
                mainPanel.Controls.Add(lblDiemHoanLaiLabel);

                string diemText = "";
                Color diemColor = Color.Black;
                
                if (diemTichLuyBiTru.HasValue && diemSuDungTraLai.HasValue)
                {
                    // Hiển thị chi tiết
                    diemText = $"Trừ {diemTichLuyBiTru.Value:N0} điểm tích lũy";
                    if (diemSuDungTraLai.Value > 0)
                    {
                        diemText += $", trả lại {diemSuDungTraLai.Value:N0} điểm đã sử dụng";
                    }
                    diemText += $"\nTổng thay đổi: ";
                    
                    if (diemHoanLai.Value > 0)
                    {
                        diemText += $"+{diemHoanLai.Value:N0} điểm";
                        diemColor = Color.FromArgb(40, 167, 69);
                    }
                    else if (diemHoanLai.Value < 0)
                    {
                        diemText += $"{diemHoanLai.Value:N0} điểm";
                        diemColor = Color.FromArgb(220, 53, 69);
                    }
                    else
                    {
                        diemText += "0 điểm";
                        diemColor = Color.FromArgb(108, 117, 125);
                    }
                }
                else
                {
                    // Hiển thị tổng
                    if (diemHoanLai.Value > 0)
                    {
                        diemText = $"+{diemHoanLai.Value:N0} điểm (được cộng lại)";
                        diemColor = Color.FromArgb(40, 167, 69);
                    }
                    else if (diemHoanLai.Value < 0)
                    {
                        diemText = $"{diemHoanLai.Value:N0} điểm (bị trừ)";
                        diemColor = Color.FromArgb(220, 53, 69);
                    }
                    else
                    {
                        diemText = "0 điểm";
                        diemColor = Color.FromArgb(108, 117, 125);
                    }
                }

                lblDiemHoanLai = new Label
                {
                    Text = diemText,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    AutoSize = false,
                    Location = new Point(150, yPos),
                    Size = new Size(700, diemTichLuyBiTru.HasValue ? 50 : 25),
                    ForeColor = diemColor
                };
                mainPanel.Controls.Add(lblDiemHoanLai);
                yPos += (diemTichLuyBiTru.HasValue ? 60 : 35);
            }

            // Lý do hủy
            var lblLyDoHuyLabel = new Label
            {
                Text = "Lý do hủy:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, yPos),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            mainPanel.Controls.Add(lblLyDoHuyLabel);
            yPos += 25;

            lblLyDoHuy = new Label
            {
                Text = lyDoHuy ?? "Không có lý do",
                Font = new Font("Segoe UI", 10),
                AutoSize = false,
                Location = new Point(10, yPos),
                Size = new Size(850, 60),
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(248, 249, 250)
            };
            mainPanel.Controls.Add(lblLyDoHuy);
            yPos += 80;

            // Chi tiết hóa đơn
            lblChiTietTitle = new Label
            {
                Text = "Chi tiết hóa đơn:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, yPos),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            mainPanel.Controls.Add(lblChiTietTitle);
            yPos += 30;

            // DataGridView cho chi tiết
            dgvChiTiet = new DataGridView
            {
                Location = new Point(10, yPos),
                Size = new Size(850, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersVisible = false,
                RowHeadersVisible = false
            };

            // Thêm cột
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Mã SP",
                DataPropertyName = "MaSanPham",
                Width = 80
            });

            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "TenSanPham",
                Width = 250
            });

            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Số lượng",
                DataPropertyName = "SoLuong",
                Width = 100
            });

            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Giá bán",
                DataPropertyName = "GiaBan",
                Width = 150,
                DefaultCellStyle = { Format = "N0" }
            });

            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Thành tiền",
                DataPropertyName = "ThanhTien",
                Width = 150,
                DefaultCellStyle = { Format = "N0" }
            });

            // Bind data
            dgvChiTiet.DataSource = chiTietList.Select(ct => new
            {
                ct.MaSanPham,
                ct.TenSanPham,
                ct.SoLuong,
                ct.GiaBan,
                ThanhTien = ct.GiaBan * ct.SoLuong
            }).ToList();

            mainPanel.Controls.Add(dgvChiTiet);
            yPos += 220;

            // Button Close
            btnClose = new Button
            {
                Text = "Đóng",
                Size = new Size(120, 40),
                Location = new Point(740, yPos),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            mainPanel.Controls.Add(btnClose);

            this.AcceptButton = btnClose;
        }
    }
}

