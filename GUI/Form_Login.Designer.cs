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
            panel6 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            panel3 = new Panel();
            panel8 = new Panel();
            Exit_btn = new Button();
            Login_btn = new Button();
            panel7 = new Panel();
            taiKhoan_txb = new TextBox();
            taiKhoan_lbl = new Label();
            panel5 = new Panel();
            matKhau_txb = new TextBox();
            matKhau_lbl = new Label();
            panel1.SuspendLayout();
            panel6.SuspendLayout();
            panel3.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(2, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1434, 898);
            panel1.TabIndex = 0;
            // 
            // panel6
            // 
            panel6.Controls.Add(label1);
            panel6.Location = new Point(1244, 106);
            panel6.Name = "panel6";
            panel6.Size = new Size(526, 172);
            panel6.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(173, 48);
            label1.Name = "label1";
            label1.Size = new Size(172, 41);
            label1.TabIndex = 0;
            label1.Text = "Đăng nhập";
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.graceway_gourmet_dairy_2048x13652;
            panel2.Location = new Point(3, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(855, 898);
            panel2.TabIndex = 0;
            panel2.Paint += panel2_Paint;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(131, 187, 62);
            panel3.Controls.Add(panel8);
            panel3.Controls.Add(panel7);
            panel3.Controls.Add(panel5);
            panel3.Location = new Point(855, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(592, 908);
            panel3.TabIndex = 1;
            // 
            // panel8
            // 
            panel8.Controls.Add(Exit_btn);
            panel8.Controls.Add(Login_btn);
            panel8.Location = new Point(3, 544);
            panel8.Name = "panel8";
            panel8.Size = new Size(526, 89);
            panel8.TabIndex = 4;
            // 
            // Exit_btn
            // 
            Exit_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Exit_btn.Location = new Point(355, 16);
            Exit_btn.Name = "Exit_btn";
            Exit_btn.Size = new Size(123, 51);
            Exit_btn.TabIndex = 1;
            Exit_btn.Text = "Thoát";
            Exit_btn.UseVisualStyleBackColor = true;
            // 
            // Login_btn
            // 
            Login_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Login_btn.Location = new Point(190, 16);
            Login_btn.Name = "Login_btn";
            Login_btn.Size = new Size(123, 51);
            Login_btn.TabIndex = 0;
            Login_btn.Text = "Đăng nhập";
            Login_btn.UseVisualStyleBackColor = true;
            Login_btn.Click += Login_btn_Click;
            // 
            // panel7
            // 
            panel7.Controls.Add(taiKhoan_txb);
            panel7.Controls.Add(taiKhoan_lbl);
            panel7.Location = new Point(4, 182);
            panel7.Name = "panel7";
            panel7.Size = new Size(526, 172);
            panel7.TabIndex = 3;
            // 
            // taiKhoan_txb
            // 
            taiKhoan_txb.Location = new Point(20, 69);
            taiKhoan_txb.Name = "taiKhoan_txb";
            taiKhoan_txb.Size = new Size(473, 27);
            taiKhoan_txb.TabIndex = 1;
            // 
            // taiKhoan_lbl
            // 
            taiKhoan_lbl.AutoSize = true;
            taiKhoan_lbl.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            taiKhoan_lbl.Location = new Point(20, 18);
            taiKhoan_lbl.Name = "taiKhoan_lbl";
            taiKhoan_lbl.Size = new Size(117, 31);
            taiKhoan_lbl.TabIndex = 0;
            taiKhoan_lbl.Text = "Tài khoản";
            // 
            // panel5
            // 
            panel5.Controls.Add(matKhau_txb);
            panel5.Controls.Add(matKhau_lbl);
            panel5.Location = new Point(0, 368);
            panel5.Name = "panel5";
            panel5.Size = new Size(526, 172);
            panel5.TabIndex = 1;
            // 
            // matKhau_txb
            // 
            matKhau_txb.Location = new Point(24, 67);
            matKhau_txb.Name = "matKhau_txb";
            matKhau_txb.Size = new Size(473, 27);
            matKhau_txb.TabIndex = 3;
            matKhau_txb.UseSystemPasswordChar = true;
            matKhau_txb.TextChanged += textBox1_TextChanged;
            // 
            // matKhau_lbl
            // 
            matKhau_lbl.AutoSize = true;
            matKhau_lbl.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            matKhau_lbl.Location = new Point(24, 16);
            matKhau_lbl.Name = "matKhau_lbl";
            matKhau_lbl.Size = new Size(116, 31);
            matKhau_lbl.TabIndex = 2;
            matKhau_lbl.Text = "Mật khẩu";
            // 
            // Form_Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1440, 900);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_Login";
            Load += Form_Login_Load;
            panel1.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel3.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel6;
        private Label label1;
        private Panel panel5;
        private Panel panel7;
        private TextBox taiKhoan_txb;
        private Label taiKhoan_lbl;
        private TextBox matKhau_txb;
        private Label matKhau_lbl;
        private Panel panel8;
        private Button Login_btn;
        private Button Exit_btn;
    }
}