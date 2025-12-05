using System;
using System.Drawing;
using System.Windows.Forms;

namespace mini_supermarket.GUI.QuanLy
{
    public class Dialog_ThemVaiTro : Form
    {
        private TextBox txtTenVaiTro;
        private TextBox txtMoTa;
        private Button btnOK;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblTenVaiTro;
        private Label lblMoTa;

        public string TenVaiTro => txtTenVaiTro.Text.Trim();
        public string MoTa => txtMoTa.Text.Trim();

        public Dialog_ThemVaiTro()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form settings
            this.Text = "Thêm vai trò mới";
            this.Size = new Size(450, 280);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Padding = new Padding(20);

            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "Thêm vai trò mới";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 20);

            // Label Tên vai trò
            lblTenVaiTro = new Label();
            lblTenVaiTro.Text = "Tên vai trò:";
            lblTenVaiTro.Font = new Font("Segoe UI", 10F);
            lblTenVaiTro.ForeColor = Color.FromArgb(52, 58, 64);
            lblTenVaiTro.AutoSize = true;
            lblTenVaiTro.Location = new Point(20, 60);

            // TextBox Tên vai trò
            txtTenVaiTro = new TextBox();
            txtTenVaiTro.Font = new Font("Segoe UI", 10F);
            txtTenVaiTro.Location = new Point(20, 85);
            txtTenVaiTro.Size = new Size(390, 30);
            txtTenVaiTro.BorderStyle = BorderStyle.FixedSingle;

            // Label Mô tả
            lblMoTa = new Label();
            lblMoTa.Text = "Mô tả (tùy chọn):";
            lblMoTa.Font = new Font("Segoe UI", 10F);
            lblMoTa.ForeColor = Color.FromArgb(52, 58, 64);
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(20, 125);

            // TextBox Mô tả
            txtMoTa = new TextBox();
            txtMoTa.Font = new Font("Segoe UI", 10F);
            txtMoTa.Location = new Point(20, 150);
            txtMoTa.Size = new Size(390, 30);
            txtMoTa.BorderStyle = BorderStyle.FixedSingle;

            // Button OK
            btnOK = new Button();
            btnOK.Text = "➕ Thêm";
            btnOK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnOK.Size = new Size(120, 40);
            btnOK.Location = new Point(170, 195);
            btnOK.BackColor = Color.FromArgb(16, 137, 62);
            btnOK.ForeColor = Color.White;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.Cursor = Cursors.Hand;
            btnOK.Click += BtnOK_Click;

            // Button Cancel
            btnCancel = new Button();
            btnCancel.Text = "Hủy";
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.Size = new Size(120, 40);
            btnCancel.Location = new Point(295, 195);
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblTenVaiTro);
            this.Controls.Add(txtTenVaiTro);
            this.Controls.Add(lblMoTa);
            this.Controls.Add(txtMoTa);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            // Set accept/cancel buttons
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenVaiTro.Text))
            {
                MessageBox.Show("Vui lòng nhập tên vai trò!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenVaiTro.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
