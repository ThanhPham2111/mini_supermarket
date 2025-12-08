using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mini_supermarket.GUI.SideBar;
using mini_supermarket.BUS;
using mini_supermarket.Common;

namespace mini_supermarket.GUI
{
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
            // Keep login window centered and non-resizable
            this.ClientSize = new System.Drawing.Size(1440, 900);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(150, 0, 0, 0);
            LoadBackgroundImage();
            
            // Cho phép đăng nhập bằng Enter
            taiKhoan_txb.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) matKhau_txb.Focus(); };
            matKhau_txb.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) Login_btn_Click(sender, e); };
            // Tự động focus vào ô tài khoản khi form load
            this.Shown += (s, args) => taiKhoan_txb.Focus();
        }

        private void LoadBackgroundImage()
        {
            try
            {
                var imagePath = TryFindImagePath("Screenshot 2025-11-24 133431.png");
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    panel2.BackgroundImage = Image.FromFile(imagePath);
                }
            }
            catch
            {
                // Ignore image load errors - form will display without background
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Login_btn_Click(object sender, EventArgs e)
        {
            string tenDangNhap = taiKhoan_txb.Text.Trim();
            string matKhau = matKhau_txb.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                taiKhoan_txb.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                matKhau_txb.Focus();
                return;
            }

            try
            {
                var taiKhoanBus = new TaiKhoan_BUS();
                var taiKhoan = taiKhoanBus.Authenticate(tenDangNhap, matKhau);

                if (taiKhoan == null)
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    matKhau_txb.Clear();
                    taiKhoan_txb.Focus();
                    return;
                }

                // Lấy thông tin nhân viên
                var nhanVienBus = new NhanVien_BUS();
                var nhanVien = nhanVienBus.GetNhanVienByID(taiKhoan.MaNhanVien);

                // Lưu session
                SessionManager.SetCurrentUser(taiKhoan, nhanVien);

                // Mở form sidebar
                this.Hide();
                using (Form_Sidebar sidebarForm = new Form_Sidebar())
                {
                    sidebarForm.ShowDialog();
                }
                
                // Clear session khi đóng sidebar (đăng xuất)
                SessionManager.ClearSession();
                
                // Clear các trường nhập liệu và hiển thị lại form login
                taiKhoan_txb.Clear();
                matKhau_txb.Clear();
                this.Show();
                taiKhoan_txb.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void taiKhoan_lbl_Click(object sender, EventArgs e)
        {

        }

        private void taiKhoan_txb_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            // Xác nhận thoát
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận thoát", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void quenMatKhau_btn_Click(object sender, EventArgs e)
        {
            ShowForgotPasswordDialog();
        }

        private void ShowForgotPasswordDialog()
        {
            using var dialog = new Form
            {
                Text = "Quên mật khẩu",
                Size = new Size(420, 260),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblUser = new Label { Text = "Tài khoản:", Location = new Point(20, 30), AutoSize = true };
            var txtUser = new TextBox { Location = new Point(140, 28), Width = 240 };

            var lblPhone = new Label { Text = "Số điện thoại:", Location = new Point(20, 80), AutoSize = true };
            var txtPhone = new TextBox { Location = new Point(140, 78), Width = 240 };

            var btnOk = new Button { Text = "Tiếp tục", Location = new Point(80, 140), Size = new Size(100, 40) };
            var btnCancel = new Button { Text = "Hủy", Location = new Point(220, 140), Size = new Size(100, 40) };

            btnOk.Click += (s, e) =>
            {
                string tenDangNhap = txtUser.Text.Trim();
                string soDienThoai = txtPhone.Text.Trim();

                if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(soDienThoai))
                {
                    MessageBox.Show(dialog, "Vui lòng nhập đầy đủ tài khoản và số điện thoại.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var taiKhoanBus = new TaiKhoan_BUS();
                    bool matched = taiKhoanBus.IsPhoneMatchedWithUsername(tenDangNhap, soDienThoai);

                    if (!matched)
                    {
                        MessageBox.Show(dialog, "Không tìm thấy tài khoản hoặc số điện thoại không khớp.", "Không thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Mở dialog nhập mật khẩu mới
                    using var pwdDialog = new DialogUpdatePassword();
                    if (pwdDialog.ShowDialog(dialog) == DialogResult.OK)
                    {
                        bool updated = taiKhoanBus.UpdatePasswordWithPhone(tenDangNhap, soDienThoai, pwdDialog.NewPassword);
                        if (updated)
                        {
                            MessageBox.Show(dialog, "Đặt lại mật khẩu thành công.", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dialog.DialogResult = DialogResult.OK;
                            dialog.Close();
                            matKhau_txb.Text = pwdDialog.NewPassword;
                            matKhau_txb.Focus();
                            matKhau_txb.SelectAll();
                        }
                        else
                        {
                            MessageBox.Show(dialog, "Không thể cập nhật mật khẩu.", "Không thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(dialog, $"Lỗi khi khôi phục mật khẩu: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (s, e) =>
            {
                dialog.DialogResult = DialogResult.Cancel;
                dialog.Close();
            };

            dialog.Controls.Add(lblUser);
            dialog.Controls.Add(txtUser);
            dialog.Controls.Add(lblPhone);
            dialog.Controls.Add(txtPhone);
            dialog.Controls.Add(btnOk);
            dialog.Controls.Add(btnCancel);

            dialog.AcceptButton = btnOk;
            dialog.CancelButton = btnCancel;

            dialog.ShowDialog(this);
        }
    }

    internal class DialogUpdatePassword : Form
    {
        private readonly TextBox _txtNewPassword = new() { UseSystemPasswordChar = true, Width = 240 };
        private readonly TextBox _txtConfirmPassword = new() { UseSystemPasswordChar = true, Width = 240 };
        private readonly Button _btnOk = new() { Text = "Xác nhận", Width = 100, Height = 36 };
        private readonly Button _btnCancel = new() { Text = "Hủy", Width = 100, Height = 36 };

        public string NewPassword { get; private set; } = string.Empty;

        public DialogUpdatePassword()
        {
            Text = "Đặt mật khẩu mới";
            Size = new Size(450, 260);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblNew = new Label { Text = "Mật khẩu mới:", Location = new Point(20, 30), AutoSize = true };
            _txtNewPassword.Location = new Point(160, 28);

            var lblConfirm = new Label { Text = "Nhập lại mật khẩu:", Location = new Point(20, 80), AutoSize = true };
            _txtConfirmPassword.Location = new Point(160, 78);

            _btnOk.Location = new Point(90, 140);
            _btnCancel.Location = new Point(230, 140);

            _btnOk.Click += BtnOk_Click;
            _btnCancel.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.Add(lblNew);
            Controls.Add(_txtNewPassword);
            Controls.Add(lblConfirm);
            Controls.Add(_txtConfirmPassword);
            Controls.Add(_btnOk);
            Controls.Add(_btnCancel);

            AcceptButton = _btnOk;
            CancelButton = _btnCancel;
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            var newPass = _txtNewPassword.Text.Trim();
            var confirm = _txtConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPass) || string.IsNullOrWhiteSpace(confirm))
            {
                MessageBox.Show(this, "Vui lòng nhập đầy đủ mật khẩu mới.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass.Length < 6)
            {
                MessageBox.Show(this, "Mật khẩu phải có ít nhất 6 ký tự.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show(this, "Mật khẩu xác nhận không khớp.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NewPassword = newPass;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
