namespace mini_supermarket.GUI
{
    partial class Form_Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel6 = new Panel();
            label1 = new Label();
            panel7 = new Panel();
            Exit_btn = new Button();
            matKhau_txb = new TextBox();
            Login_btn = new Button();
            taiKhoan_txb = new TextBox();
            matKhau_lbl = new Label();
            taiKhoan_lbl = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(2, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1434, 898);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(3, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1439, 898);
            panel2.TabIndex = 0;
            panel2.Paint += panel2_Paint;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(180, 30, 30, 30);
            panel3.Controls.Add(panel6);
            panel3.Controls.Add(panel7);
            panel3.Location = new Point(363, 285);
            panel3.Name = "panel3";
            panel3.Size = new Size(714, 329);
            panel3.TabIndex = 1;
            panel3.Paint += panel3_Paint;
            // 
            // panel6
            // 
            panel6.BackColor = Color.Transparent;
            panel6.Controls.Add(label1);
            panel6.Location = new Point(99, 16);
            panel6.Name = "panel6";
            panel6.Size = new Size(508, 66);
            panel6.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(166, 10);
            label1.Name = "label1";
            label1.Size = new Size(172, 41);
            label1.TabIndex = 0;
            label1.Text = "Đăng nhập";
            // 
            // panel7
            // 
            panel7.BackColor = Color.Transparent;
            panel7.Controls.Add(Exit_btn);
            panel7.Controls.Add(matKhau_txb);
            panel7.Controls.Add(Login_btn);
            panel7.Controls.Add(taiKhoan_txb);
            panel7.Controls.Add(matKhau_lbl);
            panel7.Controls.Add(taiKhoan_lbl);
            panel7.Location = new Point(21, 98);
            panel7.Name = "panel7";
            panel7.Size = new Size(670, 219);
            panel7.TabIndex = 3;
            // 
            // Exit_btn
            // 
            Exit_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Exit_btn.Location = new Point(365, 145);
            Exit_btn.Name = "Exit_btn";
            Exit_btn.Size = new Size(123, 51);
            Exit_btn.TabIndex = 1;
            Exit_btn.Text = "Thoát";
            Exit_btn.UseVisualStyleBackColor = true;
            Exit_btn.Click += Exit_btn_Click;
            // 
            // matKhau_txb
            // 
            matKhau_txb.Location = new Point(153, 84);
            matKhau_txb.Name = "matKhau_txb";
            matKhau_txb.Size = new Size(473, 27);
            matKhau_txb.TabIndex = 3;
            matKhau_txb.UseSystemPasswordChar = true;
            matKhau_txb.TextChanged += textBox1_TextChanged;
            // 
            // Login_btn
            // 
            Login_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Login_btn.Location = new Point(173, 145);
            Login_btn.Name = "Login_btn";
            Login_btn.Size = new Size(123, 51);
            Login_btn.TabIndex = 0;
            Login_btn.Text = "Đăng nhập";
            Login_btn.UseVisualStyleBackColor = true;
            Login_btn.Click += Login_btn_Click;
            // 
            // taiKhoan_txb
            // 
            taiKhoan_txb.Location = new Point(153, 19);
            taiKhoan_txb.Name = "taiKhoan_txb";
            taiKhoan_txb.Size = new Size(473, 27);
            taiKhoan_txb.TabIndex = 1;
            taiKhoan_txb.TextChanged += taiKhoan_txb_TextChanged;
            // 
            // matKhau_lbl
            // 
            matKhau_lbl.AutoSize = true;
            matKhau_lbl.BackColor = Color.Transparent;
            matKhau_lbl.Font = new Font("Segoe UI", 13.8F);
            matKhau_lbl.ForeColor = Color.White;
            matKhau_lbl.Location = new Point(24, 78);
            matKhau_lbl.Name = "matKhau_lbl";
            matKhau_lbl.Size = new Size(115, 31);
            matKhau_lbl.TabIndex = 2;
            matKhau_lbl.Text = "Mật khẩu:";
            // 
            // taiKhoan_lbl
            // 
            taiKhoan_lbl.AutoSize = true;
            taiKhoan_lbl.BackColor = Color.Transparent;
            taiKhoan_lbl.Font = new Font("Segoe UI", 13.8F);
            taiKhoan_lbl.ForeColor = Color.White;
            taiKhoan_lbl.Location = new Point(20, 18);
            taiKhoan_lbl.Name = "taiKhoan_lbl";
            taiKhoan_lbl.Size = new Size(114, 31);
            taiKhoan_lbl.TabIndex = 0;
            taiKhoan_lbl.Text = "Tài khoản:";
            taiKhoan_lbl.Click += taiKhoan_lbl_Click;
            // 
            // Form_Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1440, 900);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_Login";
            Load += Form_Login_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel6;
        private Label label1;
        private Panel panel7;
        private TextBox taiKhoan_txb;
        private Label taiKhoan_lbl;
        private TextBox matKhau_txb;
        private Label matKhau_lbl;
        private Button Login_btn;
        private Button Exit_btn;
    }
}