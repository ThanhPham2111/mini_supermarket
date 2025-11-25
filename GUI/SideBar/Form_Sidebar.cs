using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using mini_supermarket.GUI.Form_BanHang;
using mini_supermarket.GUI.KhachHang;
using mini_supermarket.GUI.KhuyenMai;
using mini_supermarket.GUI.NhanVien;
using mini_supermarket.GUI.PhieuNhap;
using mini_supermarket.GUI.NhaCungCap;
using mini_supermarket.GUI.HoaDon;
using mini_supermarket.GUI.KhoHang;
using mini_supermarket.GUI.TaiKhoan;
using mini_supermarket.GUI.TrangChu;
using mini_supermarket.GUI.QuanLy;
using FormSanPham = mini_supermarket.GUI.Form_SanPham.Form_SanPham;
using FormLoaiSanPham = mini_supermarket.GUI.Form_LoaiSanPham.Form_LoaiSanPham;

namespace mini_supermarket.GUI.SideBar
{
    public partial class Form_Sidebar : Form
    {
        private Button? _activeButton;
        private Form? _activeForm;

        public Form_Sidebar()
        {
            InitializeComponent();
            InitializeLayout();
            InitializeAssets();
            ShowTrangChu();
        }

        private void InitializeLayout()
        {
            ClientSize = new Size(1440, 900);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeAssets()
        {
            try
            {
                var logoPath = TryFindImagePath("icons8-market-96.png");
                if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
                {
                    logoPictureBox.WaitOnLoad = true;
                    logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    logoPictureBox.Image = Image.FromFile(logoPath);
                }
                // Ensure docking order: picture (top) -> title (top) -> username (fill)
                logoPanel.Controls.SetChildIndex(logoPictureBox, 0);
                logoPanel.Controls.SetChildIndex(logoLabel, 1);
                logoPanel.Controls.SetChildIndex(userNameLabel, 2);
                logoPictureBox.BringToFront();
                logoPictureBox.Visible = true;
            }
            catch
            {
                // ignore image load errors
            }
        }

        private static string? TryFindImagePath(string fileName)
        {
            var current = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 6 && current != null; i++)
            {
                var candidate = Path.Combine(current, "img", fileName);
                if (File.Exists(candidate))
                {
                    return candidate;
                }
                var parent = Directory.GetParent(current);
                current = parent?.FullName;
            }
            return null;
        }

        private void navTrangChuButton_Click(object sender, EventArgs e)
        {
            ShowTrangChu();
        }

        private void navBanHangButton_Click(object sender, EventArgs e)
        {
            ShowBanHang();
        }

        private void navNhanVienButton_Click(object sender, EventArgs e)
        {
            ShowNhanVien();
        }

        private void navSanPhamButton_Click(object sender, EventArgs e)
        {
            ShowSanPham();
        }
        private void navLoaiSanPhamButton_Click(object sender, EventArgs e)
        {
            ShowLoaiSanPham();
        }

        private void navKhachHangButton_Click(object sender, EventArgs e)
        {
            ShowKhachHang();
        }
        private void navNhaCungCapButton_Click(object sender, EventArgs e)
        {
            ShowNhaCungCap();
        }

        private void navHoaDonButton_Click(object sender, EventArgs e)
        {
            ShowHoaDon();
        }

        private void navPhieuNhapButton_Click(object sender, EventArgs e)
        {
            ShowPhieuNhap();
        }
        private void navTaiKhoanButton_Click(object sender, EventArgs e)
        {
            ShowTaiKhoan();
        }
        private void navKhuyenMaiButton_Click(object sender, EventArgs e)
        {
            ShowKhuyenMai();
        }

        private void NavPlaceholderButton_Click(object sender, EventArgs e)
        {
            if (sender is not Button button)
            {
                return;
            }

            var message = $"Chuc nang {button.Text.ToLowerInvariant()} dang duoc phat trien.";
            ShowPlaceholder(message, button);
        }

        private void ShowTrangChu()
        {
            SetActiveButton(navTrangChuButton);
            mainTitleLabel.Text = navTrangChuButton.Text;

            CloseActiveForm();

            var trangChuForm = new Form_TrangChu
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = trangChuForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(trangChuForm);
            trangChuForm.Show();
        }

        private void ShowBanHang()
        {
            SetActiveButton(navBanHangButton);
            mainTitleLabel.Text = navBanHangButton.Text;

            CloseActiveForm();

            var banHangForm = new Form_banHang
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = banHangForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(banHangForm);
            banHangForm.Show();
        }
        private void ShowKhoHang()
        {
            SetActiveButton(navKhoHangButton);
            mainTitleLabel.Text = navKhoHangButton.Text;

            CloseActiveForm();

            var khoHangForm = new Form_KhoHang
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = khoHangForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(khoHangForm);
            khoHangForm.Show();
        }

        private void navKhoHangButton_Click(object sender, EventArgs e)
        {
            ShowKhoHang();
        }

        private void ShowNhanVien()
        {
            SetActiveButton(navNhanVienButton);
            mainTitleLabel.Text = navNhanVienButton.Text;

            CloseActiveForm();

            var nhanVienForm = new Form_NhanVien
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = nhanVienForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(nhanVienForm);
            nhanVienForm.Show();
        }

        private void ShowSanPham()
        {
            SetActiveButton(navSanPhamButton);
            mainTitleLabel.Text = navSanPhamButton.Text;

            CloseActiveForm();

            var sanPhamForm = new FormSanPham
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = sanPhamForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(sanPhamForm);
            sanPhamForm.Show();
        }

        private void ShowLoaiSanPham()
        {
            SetActiveButton(navLoaiSanPhamButton);
            mainTitleLabel.Text = navLoaiSanPhamButton.Text;

            CloseActiveForm();

            var form = new FormLoaiSanPham
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = form;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(form);
            form.Show();
        }

        private void ShowKhuyenMai()
        {
            SetActiveButton(navKhuyenMaiButton);
            mainTitleLabel.Text = navKhuyenMaiButton.Text;

            CloseActiveForm();

            var khuyenMaiForm = new Form_KhuyenMai
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = khuyenMaiForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(khuyenMaiForm);
            khuyenMaiForm.Show();
        }

        private void ShowKhachHang()
        {
            SetActiveButton(navKhachHangButton);
            mainTitleLabel.Text = navKhachHangButton.Text;

            CloseActiveForm();

            var khachHangForm = new Form_KhachHang
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = khachHangForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(khachHangForm);
            khachHangForm.Show();
        }
        private void ShowNhaCungCap()
        {
            SetActiveButton(navNhaCungCapButton);
            mainTitleLabel.Text = navNhaCungCapButton.Text;

            CloseActiveForm();

            var nhaCungCapForm = new Form_NhaCungCap
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = nhaCungCapForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(nhaCungCapForm);
            nhaCungCapForm.Show();
        }

        private void ShowHoaDon()
        {
            SetActiveButton(navHoaDonButton);
            mainTitleLabel.Text = navHoaDonButton.Text;

            CloseActiveForm();

            var hoaDonForm = new Form_HoaDon
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = hoaDonForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(hoaDonForm);
            hoaDonForm.Show();
        }

        private void ShowPhieuNhap()
        {
            SetActiveButton(navPhieuNhapButton);
            mainTitleLabel.Text = navPhieuNhapButton.Text;

            CloseActiveForm();

            var phieuNhapForm = new Form_PhieuNhap
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = phieuNhapForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(phieuNhapForm);
            phieuNhapForm.Show();
        }

        private void ShowTaiKhoan()
        {
            SetActiveButton(navTaiKhoanButton);
            mainTitleLabel.Text = navTaiKhoanButton.Text;

            CloseActiveForm();

            var taiKhoanForm = new Form_TaiKhoan
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = taiKhoanForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(taiKhoanForm); // Missing line - add form to panel
            taiKhoanForm.Show();
        }

        private void navQuanLyButton_Click(object sender, EventArgs e)
        {
            ShowQuanLy();
        }

        private void ShowQuanLy()
        {
            SetActiveButton(navQuanLyButton);
            mainTitleLabel.Text = navQuanLyButton.Text;

            CloseActiveForm();

            var quanLyForm = new Form_QuanLy
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = quanLyForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(quanLyForm);
            quanLyForm.Show();
        }

        private void ShowPlaceholder(string message, Button sourceButton)
        {
            SetActiveButton(sourceButton);
            mainTitleLabel.Text = sourceButton.Text;

            CloseActiveForm();
            contentHostPanel.Controls.Clear();

            var placeholderPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var messageLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = message,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(52, 58, 64),
                Padding = new Padding(16),
                TextAlign = ContentAlignment.MiddleCenter
            };

            placeholderPanel.Controls.Add(messageLabel);
            contentHostPanel.Controls.Add(placeholderPanel);
        }

        private void SetActiveButton(Button button)
        {
            if (_activeButton != null)
            {
                _activeButton.BackColor = Color.White;
                _activeButton.ForeColor = Color.FromArgb(33, 37, 41);
            }

            _activeButton = button;
            _activeButton.BackColor = Color.FromArgb(21, 128, 61); // green-700
            _activeButton.ForeColor = Color.White;
        }

        private void CloseActiveForm()
        {
            if (_activeForm == null)
            {
                return;
            }

            if (contentHostPanel.Controls.Contains(_activeForm))
            {
                contentHostPanel.Controls.Remove(_activeForm);
            }

            _activeForm.Close();
            _activeForm.Dispose();
            _activeForm = null;
        }
    }
}
