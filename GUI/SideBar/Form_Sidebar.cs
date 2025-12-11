using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
using Form_Login = mini_supermarket.GUI.Form_Login;
using mini_supermarket.Common;
using mini_supermarket.BUS;
using FormSanPham = mini_supermarket.GUI.Form_SanPham.Form_SanPham;
using FormLoaiSanPham = mini_supermarket.GUI.Form_LoaiSanPham.Form_LoaiSanPham;

namespace mini_supermarket.GUI.SideBar
{
    public partial class Form_Sidebar : Form
    {
        private Button? _activeButton;
        private Form? _activeForm;
        private PermissionService? _permissionService;
        private Dictionary<Button, string> _buttonPathMapping = new(); // Mapping button -> DuongDan

        public Form_Sidebar()
        {
            InitializeComponent();
            InitializeLayout();
            InitializeAssets();
            InitializePermissions();
            
            // Load event để đảm bảo permissions được load sau khi form đã sẵn sàng
            Load += Form_Sidebar_Load;
        }

        private void Form_Sidebar_Load(object? sender, EventArgs e)
        {
            // Reload permissions khi form load (đảm bảo session đã được set)
            if (_permissionService != null)
            {
                _permissionService.ReloadPermissions();
            }
            ApplyPermissions();
            ShowTrangChu();
        }

        private void InitializePermissions()
        {
            _permissionService = new PermissionService();
            
            // Mapping giữa button và DuongDan (tên form/chức năng)
            // Lưu ý: DuongDan trong DB có prefix "Form_" (Form_TrangChu, Form_BanHang, etc.)
            _buttonPathMapping = new Dictionary<Button, string>
            {
                { navTrangChuButton, "Form_TrangChu" },
                { navBanHangButton, "Form_BanHang" },
                { navHoaDonButton, "Form_HoaDon" },
                { navPhieuNhapButton, "Form_PhieuNhap" },
                { navSanPhamButton, "Form_SanPham" },
                { navKhoHangButton, "Form_KhoHang" },
                { navLoaiSanPhamButton, "Form_LoaiSanPham" },
                { navKhuyenMaiButton, "Form_KhuyenMai" },
                { navKhachHangButton, "Form_KhachHang" },
                { navNhaCungCapButton, "Form_NhaCungCap" },
                { navNhanVienButton, "Form_NhanVien" },
                { navTaiKhoanButton, "Form_TaiKhoan" },
                { navQuanLyButton, "Form_QuanLy" }
            };
        }

        private void ApplyPermissions()
        {
            if (!SessionManager.IsLoggedIn || _permissionService == null)
            {
                // Nếu chưa đăng nhập, ẩn tất cả trừ Trang Chủ
                foreach (var button in _buttonPathMapping.Keys)
                {
                    if (button != navTrangChuButton)
                    {
                        button.Visible = false;
                    }
                }
                userNameLabel.Text = "Chưa đăng nhập";
                return;
            }

            // Đảm bảo permissions được reload
            _permissionService.ReloadPermissions();

            // Hiển thị tên user
            if (SessionManager.CurrentNhanVien != null)
            {
                userNameLabel.Text = SessionManager.CurrentNhanVien.TenNhanVien;
            }
            else if (SessionManager.CurrentUser != null)
            {
                userNameLabel.Text = SessionManager.CurrentUser.TenDangNhap;
            }

            // Admin có quyền tất cả
            if (SessionManager.CurrentMaQuyen == 1)
            {
                foreach (var button in _buttonPathMapping.Keys)
                {
                    button.Visible = true;
                }
                return;
            }

            // Kiểm tra quyền cho từng button
            try
            {
                var phanQuyenBus = new PhanQuyen_BUS();
                var allChucNangs = phanQuyenBus.GetAllChucNang();

                // Debug: Kiểm tra xem có chức năng nào không
                if (allChucNangs == null || allChucNangs.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy chức năng nào trong database. Vui lòng chạy script insert_xin.sql (phần 23-24)", 
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Ẩn tất cả trừ Trang Chủ
                    foreach (var button in _buttonPathMapping.Keys)
                    {
                        if (button != navTrangChuButton)
                        {
                            button.Visible = false;
                        }
                    }
                    return;
                }

                // Đếm số button được hiển thị (để debug)
                int visibleCount = 0;

                foreach (var kvp in _buttonPathMapping)
                {
                    var button = kvp.Key;
                    var duongDan = kvp.Value;

                    // Trang chủ luôn hiển thị
                    if (button == navTrangChuButton)
                    {
                        button.Visible = true;
                        visibleCount++;
                        continue;
                    }

                    // Tìm chức năng tương ứng
                    var chucNang = allChucNangs.FirstOrDefault(cn => 
                        cn.DuongDan != null && cn.DuongDan.Equals(duongDan, StringComparison.OrdinalIgnoreCase));

                    if (chucNang != null)
                    {
                        // Kiểm tra quyền xem
                        bool hasViewPermission = _permissionService.HasViewPermission(chucNang.MaChucNang);
                        button.Visible = hasViewPermission;
                        if (hasViewPermission)
                        {
                            visibleCount++;
                        }
                    }
                    else
                    {
                        // Nếu không tìm thấy trong DB, mặc định ẩn
                        button.Visible = false;
                    }
                }

                // Debug: Nếu chỉ có Trang Chủ được hiển thị, có thể là chưa có phân quyền
                if (visibleCount == 1 && SessionManager.CurrentMaQuyen != 1)
                {
                    // Không hiển thị message box vì có thể user chưa được phân quyền
                    // Chỉ log để debug
                    System.Diagnostics.Debug.WriteLine($"User {SessionManager.CurrentMaQuyen} chỉ có quyền Trang Chủ. Có thể chưa được phân quyền trong Tbl_PhanQuyenChiTiet.");
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, hiển thị tất cả (để tránh lock user)
                MessageBox.Show($"Lỗi khi kiểm tra quyền: {ex.Message}\n\nStack trace: {ex.StackTrace}", 
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                foreach (var button in _buttonPathMapping.Keys)
                {
                    button.Visible = true;
                }
            }
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

            // Create logout icon
            CreateLogoutIcon();
        }

        private void CreateLogoutIcon()
        {
            try
            {
                var iconPath = TryFindImagePath("icons8-power-24.png");
                if (!string.IsNullOrEmpty(iconPath) && File.Exists(iconPath))
                {
                    logoutButton.Image = Image.FromFile(iconPath);
                    logoutButton.ImageAlign = ContentAlignment.MiddleLeft;
                }
            }
            catch
            {
                // If icon loading fails, button will work without icon
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
            if (!CheckPermission("Form_BanHang", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowBanHang();
        }

        private void navNhanVienButton_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("Form_NhanVien", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowNhanVien();
        }

        private void navSanPhamButton_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("Form_SanPham", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowSanPham();
        }
        private void navLoaiSanPhamButton_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("Form_LoaiSanPham", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowLoaiSanPham();
        }

        private void navKhachHangButton_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("Form_KhachHang", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowKhachHang();
        }
        private void navNhaCungCapButton_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("Form_NhaCungCap", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowNhaCungCap();
        }

        private void navHoaDonButton_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("Form_HoaDon", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowHoaDon();
        }

        private void navPhieuNhapButton_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("Form_PhieuNhap", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowPhieuNhap();
        }
        private void navTaiKhoanButton_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("Form_TaiKhoan", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowTaiKhoan();
        }
        private void navKhuyenMaiButton_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("Form_KhuyenMai", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
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



    //tao form vao panel contentHostPanel

        private void ShowTrangChu() //tao form vao panel contentHostPanel
        {
            SetActiveButton(navTrangChuButton);
            mainTitleLabel.Text = navTrangChuButton.Text;

            CloseActiveForm();

            var trangChuForm = new Form_TrangChu //tao form trang chu
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None, //khong co border
                Dock = DockStyle.Fill //fill toi day
            };

            _activeForm = trangChuForm; //set form trang chu la form hien tai
            contentHostPanel.Controls.Clear(); //xoa form hien tai
            contentHostPanel.Controls.Add(trangChuForm); //them form trang chu vao panel contentHostPanel
            trangChuForm.Show(); //hien thi form trang chu
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
            if (!CheckPermission("Form_KhoHang", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
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
            if (!CheckPermission("Form_QuanLy", PermissionService.LoaiQuyen_Xem))
            {
                return;
            }
            ShowQuanLy();
        }

        private bool CheckPermission(string duongDan, int maLoaiQuyen)
        {
            if (_permissionService == null)
            {
                return false;
            }

            // Admin luôn có quyền
            if (SessionManager.CurrentMaQuyen == 1)
            {
                return true;
            }

            bool hasPermission = _permissionService.HasPermissionByPath(duongDan, maLoaiQuyen);
            if (!hasPermission)
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return hasPermission;
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

        private void logoutButton_Click(object sender, EventArgs e)
        {
            // Xác nhận đăng xuất
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                // Clear session
                SessionManager.ClearSession();
                
                // Đóng form sidebar - điều này sẽ quay lại form login vì ShowDialog() sẽ kết thúc
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
