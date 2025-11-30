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
            this.Hide();
            using (Form_Sidebar sidebarForm = new Form_Sidebar())
            {
                sidebarForm.ShowDialog();
            }
            this.Close();
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
