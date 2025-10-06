using System;
using System.Drawing;
using System.Windows.Forms;
using mini_supermarket.GUI.Form_BanHang;
using mini_supermarket.GUI.KhachHang;
using mini_supermarket.GUI.NhanVien;
using mini_supermarket.GUI.FormKhoHang;
using FormSanPham = mini_supermarket.GUI.Form_SanPham.Form_SanPham;

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

        private void navKhachHangButton_Click(object sender, EventArgs e)
        {
            ShowKhachHang();
        }

        private void navKhoHangButton_Click(object sender,EventArgs e)
        {
            ShowKhoHang();
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
            const string welcomeMessage = "Chuc mung ban den voi Mini Supermarket.";
            ShowPlaceholder(welcomeMessage, navTrangChuButton);
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
                _activeButton.BackColor = Color.FromArgb(52, 58, 64);
            }

            _activeButton = button;
            _activeButton.BackColor = Color.FromArgb(73, 80, 87);
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
