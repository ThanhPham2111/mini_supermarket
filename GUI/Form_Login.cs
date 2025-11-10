using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            
            // Set tab order for better UX
            taiKhoan_txb.TabIndex = 0;
            matKhau_txb.TabIndex = 1;
            Login_btn.TabIndex = 2;
            
            // Enable Enter key to trigger login

            this.AcceptButton = Login_btn;
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            // Focus on username textbox when form loads
            taiKhoan_txb.Focus();
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

    }
}
