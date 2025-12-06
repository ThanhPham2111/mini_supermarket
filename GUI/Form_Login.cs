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
                
                // Clear session khi đóng
                SessionManager.ClearSession();
                this.Close();
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
    }
}
