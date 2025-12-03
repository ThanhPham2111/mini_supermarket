using System;
using System.Drawing;
using System.Windows.Forms;
using mini_supermarket.BUS;

namespace mini_supermarket.GUI.QuanLy
{
    public partial class UC_QuyDoiDiem : UserControl
    {
        private QuyDoiDiem_BUS _quyDoiDiemBus;

        // Controls
        private Label lblTitle;
        private Label lblSoDiem;
        private Label lblSoTien;
        private Label lblMoTa;
        private NumericUpDown nudSoDiem;
        private NumericUpDown nudSoTien;
        private Button btnXacNhan;
        private Label lblThongTinHienTai;
        private Label lblGiaTriMotDiem;
        private Label lblViDu;

        public UC_QuyDoiDiem()
        {
            _quyDoiDiemBus = new QuyDoiDiem_BUS();
            
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
            this.Padding = new Padding(40, 40, 40, 40);
            this.AutoScroll = true;

            int startY = 40;
            int spacing = 25; // Khoảng cách giữa các control

            // Title
            lblTitle = new Label
            {
                Text = "Cấu hình quy đổi điểm khách hàng",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, startY),
                ForeColor = Color.FromArgb(33, 37, 41)
            };
            this.Controls.Add(lblTitle);

            startY += lblTitle.Height + spacing + 10;

            // Mô tả
            lblMoTa = new Label
            {
                Text = "Cấu hình quy đổi điểm để tính giảm giá khi khách hàng sử dụng điểm.\n" +
                       "Ví dụ: 100 điểm = 100 đồng (khách hàng dùng 100 điểm sẽ được giảm 100 đồng).\n" +
                       "Lưu ý: Điểm tích lũy vẫn tính theo số lượng sản phẩm (1 sản phẩm = 1 điểm).",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(40, startY),
                ForeColor = Color.FromArgb(108, 117, 125),
                MaximumSize = new Size(900, 0)
            };
            this.Controls.Add(lblMoTa);

            startY += lblMoTa.Height + spacing + 20;

            // Label và NumericUpDown cho Số điểm
            lblSoDiem = new Label
            {
                Text = "Số điểm:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, startY)
            };
            this.Controls.Add(lblSoDiem);

            nudSoDiem = new NumericUpDown
            {
                Location = new Point(230, startY - 2),
                Size = new Size(280, 35),
                Font = new Font("Segoe UI", 12),
                Minimum = 1,
                Maximum = 1000000,
                Value = 100,
                DecimalPlaces = 0
            };
            this.Controls.Add(nudSoDiem);

            startY += nudSoDiem.Height + spacing + 15;

            // Label và NumericUpDown cho Số tiền
            lblSoTien = new Label
            {
                Text = "Số tiền tương ứng (đồng):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, startY)
            };
            this.Controls.Add(lblSoTien);

            nudSoTien = new NumericUpDown
            {
                Location = new Point(310, startY - 2),
                Size = new Size(250, 35),
                Font = new Font("Segoe UI", 12),
                Minimum = 1,
                Maximum = 1000000000,
                Value = 100,
                DecimalPlaces = 0
            };
            this.Controls.Add(nudSoTien);

            startY += nudSoTien.Height + spacing + 25;

            // Button Xác nhận
            btnXacNhan = new Button
            {
                Text = "Xác nhận",
                Size = new Size(180, 50),
                Location = new Point(40, startY),
                BackColor = Color.FromArgb(16, 137, 62),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.Click += BtnXacNhan_Click;
            this.Controls.Add(btnXacNhan);

            startY += btnXacNhan.Height + spacing + 30;

            // Label thông tin hiện tại
            lblThongTinHienTai = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 11),
                AutoSize = true,
                Location = new Point(40, startY),
                ForeColor = Color.FromArgb(33, 37, 41)
            };
            this.Controls.Add(lblThongTinHienTai);

            startY += 30;

            // Label giá trị 1 điểm
            lblGiaTriMotDiem = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, startY),
                ForeColor = Color.FromArgb(16, 137, 62)
            };
            this.Controls.Add(lblGiaTriMotDiem);

            startY += 30;

            // Label ví dụ
            lblViDu = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(40, startY),
                ForeColor = Color.FromArgb(108, 117, 125),
                MaximumSize = new Size(900, 0)
            };
            this.Controls.Add(lblViDu);

            // Cập nhật thông tin khi thay đổi giá trị
            nudSoDiem.ValueChanged += (s, e) => UpdateThongTin();
            nudSoTien.ValueChanged += (s, e) => UpdateThongTin();
        }

        private void LoadData()
        {
            try
            {
                var cauHinh = _quyDoiDiemBus.GetCauHinh();
                if (cauHinh != null)
                {
                    nudSoDiem.Value = cauHinh.SoDiem;
                    nudSoTien.Value = cauHinh.SoTienTuongUng;
                }
                else
                {
                    // Giá trị mặc định: 100 điểm = 100 đồng
                    nudSoDiem.Value = 100;
                    nudSoTien.Value = 100;
                }
                UpdateThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateThongTin()
        {
            try
            {
                int soDiem = (int)nudSoDiem.Value;
                decimal soTien = nudSoTien.Value;
                decimal giaTriMotDiem = soTien / soDiem;

                // Cập nhật thông tin hiện tại
                var cauHinhHienTai = _quyDoiDiemBus.GetCauHinh();
                if (cauHinhHienTai != null)
                {
                    lblThongTinHienTai.Text = $"Cấu hình hiện tại: {cauHinhHienTai.SoDiem} điểm = {cauHinhHienTai.SoTienTuongUng:N0} đồng";
                }
                else
                {
                    lblThongTinHienTai.Text = "Chưa có cấu hình. Sẽ tạo mới khi xác nhận.";
                }

                // Hiển thị giá trị 1 điểm
                lblGiaTriMotDiem.Text = $"Giá trị 1 điểm = {giaTriMotDiem:N2} đồng";

                // Hiển thị ví dụ
                if (soDiem > 0 && soTien > 0)
                {
                    int viDuDiemSuDung = 100;
                    decimal viDuGiamGia = (viDuDiemSuDung / (decimal)soDiem) * soTien;
                    lblViDu.Text = $"Ví dụ: Khách hàng dùng {viDuDiemSuDung} điểm sẽ được giảm {viDuGiamGia:N0} đồng";
                }
                else
                {
                    lblViDu.Text = "";
                }
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi khi cập nhật thông tin
            }
        }

        private void BtnXacNhan_Click(object? sender, EventArgs e)
        {
            try
            {
                int soDiem = (int)nudSoDiem.Value;
                decimal soTien = nudSoTien.Value;

                // Xác nhận
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn cập nhật cấu hình quy đổi điểm?\n\n" +
                    $"Cấu hình mới: {soDiem} điểm = {soTien:N0} đồng\n" +
                    $"Giá trị 1 điểm = {soTien / soDiem:N2} đồng\n\n" +
                    $"Lưu ý: Cấu hình này sẽ được áp dụng cho tất cả các giao dịch sau.",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                // Lấy mã nhân viên (tạm thời dùng 1, sau này lấy từ session)
                int maNhanVien = 1;

                // Cập nhật cấu hình
                _quyDoiDiemBus.UpdateCauHinh(soDiem, soTien, maNhanVien);

                MessageBox.Show(
                    $"Cập nhật cấu hình quy đổi điểm thành công!\n\n" +
                    $"Cấu hình mới: {soDiem} điểm = {soTien:N0} đồng\n" +
                    $"Giá trị 1 điểm = {soTien / soDiem:N2} đồng",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Reload lại dữ liệu
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

