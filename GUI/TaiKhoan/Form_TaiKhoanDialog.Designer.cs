namespace mini_supermarket.GUI.TaiKhoan
{
    partial class Form_TaiKhoanDialog
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
            this.tenDangNhapLabel = new System.Windows.Forms.Label();
            this.tenDangNhapTextBox = new System.Windows.Forms.TextBox();
            this.tenDangNhapErrorLabel = new System.Windows.Forms.Label();
            this.tenDangNhapErrorIcon = new System.Windows.Forms.Label();
            this.matKhauLabel = new System.Windows.Forms.Label();
            this.matKhauTextBox = new System.Windows.Forms.TextBox();
            this.matKhauErrorLabel = new System.Windows.Forms.Label();
            this.matKhauErrorIcon = new System.Windows.Forms.Label();
            this.nhanVienLabel = new System.Windows.Forms.Label();
            this.nhanVienComboBox = new System.Windows.Forms.ComboBox();
            this.nhanVienErrorLabel = new System.Windows.Forms.Label();
            this.nhanVienErrorIcon = new System.Windows.Forms.Label();
            this.quyenLabel = new System.Windows.Forms.Label();
            this.quyenComboBox = new System.Windows.Forms.ComboBox();
            this.quyenErrorLabel = new System.Windows.Forms.Label();
            this.quyenErrorIcon = new System.Windows.Forms.Label();
            this.trangThaiLabel = new System.Windows.Forms.Label();
            this.trangThaiComboBox = new System.Windows.Forms.ComboBox();
            this.trangThaiErrorLabel = new System.Windows.Forms.Label();
            this.trangThaiErrorIcon = new System.Windows.Forms.Label();
            this.maTaiKhoanLabel = new System.Windows.Forms.Label();
            this.maTaiKhoanValueLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tenDangNhapLabel
            // 
            this.tenDangNhapLabel.AutoSize = true;
            this.tenDangNhapLabel.Location = new System.Drawing.Point(12, 20);
            this.tenDangNhapLabel.Name = "tenDangNhapLabel";
            this.tenDangNhapLabel.Size = new System.Drawing.Size(87, 13);
            this.tenDangNhapLabel.TabIndex = 0;
            this.tenDangNhapLabel.Text = "Tên đăng nhập:";
            // 
            // tenDangNhapTextBox
            // 
            this.tenDangNhapTextBox.Location = new System.Drawing.Point(105, 17);
            this.tenDangNhapTextBox.Name = "tenDangNhapTextBox";
            this.tenDangNhapTextBox.Size = new System.Drawing.Size(280, 20);
            this.tenDangNhapTextBox.TabIndex = 1;
            this.tenDangNhapTextBox.TextChanged += new System.EventHandler(this.tenDangNhapTextBox_TextChanged);
            // 
            // tenDangNhapErrorIcon
            // 
            this.tenDangNhapErrorIcon.AutoSize = true;
            this.tenDangNhapErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.tenDangNhapErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.tenDangNhapErrorIcon.Location = new System.Drawing.Point(388, 20);
            this.tenDangNhapErrorIcon.Name = "tenDangNhapErrorIcon";
            this.tenDangNhapErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.tenDangNhapErrorIcon.TabIndex = 19;
            this.tenDangNhapErrorIcon.Text = "✕";
            this.tenDangNhapErrorIcon.Visible = false;
            // 
            // tenDangNhapErrorLabel
            // 
            this.tenDangNhapErrorLabel.AutoSize = true;
            this.tenDangNhapErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.tenDangNhapErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.tenDangNhapErrorLabel.Location = new System.Drawing.Point(105, 38);
            this.tenDangNhapErrorLabel.Name = "tenDangNhapErrorLabel";
            this.tenDangNhapErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.tenDangNhapErrorLabel.TabIndex = 14;
            // 
            // matKhauLabel
            // 
            this.matKhauLabel.AutoSize = true;
            this.matKhauLabel.Location = new System.Drawing.Point(12, 60);
            this.matKhauLabel.Name = "matKhauLabel";
            this.matKhauLabel.Size = new System.Drawing.Size(56, 13);
            this.matKhauLabel.TabIndex = 2;
            this.matKhauLabel.Text = "Mật khẩu:";
            // 
            // matKhauTextBox
            // 
            this.matKhauTextBox.Location = new System.Drawing.Point(105, 57);
            this.matKhauTextBox.Name = "matKhauTextBox";
            // this.matKhauTextBox.PasswordChar = '*';
            this.matKhauTextBox.Size = new System.Drawing.Size(280, 20);
            this.matKhauTextBox.TabIndex = 3;
            this.matKhauTextBox.TextChanged += new System.EventHandler(this.matKhauTextBox_TextChanged);
            // 
            // matKhauErrorIcon
            // 
            this.matKhauErrorIcon.AutoSize = true;
            this.matKhauErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.matKhauErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.matKhauErrorIcon.Location = new System.Drawing.Point(388, 60);
            this.matKhauErrorIcon.Name = "matKhauErrorIcon";
            this.matKhauErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.matKhauErrorIcon.TabIndex = 20;
            this.matKhauErrorIcon.Text = "✕";
            this.matKhauErrorIcon.Visible = false;
            // 
            // matKhauErrorLabel
            // 
            this.matKhauErrorLabel.AutoSize = true;
            this.matKhauErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.matKhauErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.matKhauErrorLabel.Location = new System.Drawing.Point(105, 78);
            this.matKhauErrorLabel.Name = "matKhauErrorLabel";
            this.matKhauErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.matKhauErrorLabel.TabIndex = 15;
            // 
            // nhanVienLabel
            // 
            this.nhanVienLabel.AutoSize = true;
            this.nhanVienLabel.Location = new System.Drawing.Point(12, 100);
            this.nhanVienLabel.Name = "nhanVienLabel";
            this.nhanVienLabel.Size = new System.Drawing.Size(63, 13);
            this.nhanVienLabel.TabIndex = 4;
            this.nhanVienLabel.Text = "Nhân viên:";
            // 
            // nhanVienComboBox
            // 
            this.nhanVienComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nhanVienComboBox.FormattingEnabled = true;
            this.nhanVienComboBox.Location = new System.Drawing.Point(105, 97);
            this.nhanVienComboBox.Name = "nhanVienComboBox";
            this.nhanVienComboBox.Size = new System.Drawing.Size(280, 21);
            this.nhanVienComboBox.TabIndex = 5;
            this.nhanVienComboBox.SelectedIndexChanged += new System.EventHandler(this.nhanVienComboBox_SelectedIndexChanged);
            // 
            // nhanVienErrorIcon
            // 
            this.nhanVienErrorIcon.AutoSize = true;
            this.nhanVienErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.nhanVienErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.nhanVienErrorIcon.Location = new System.Drawing.Point(388, 100);
            this.nhanVienErrorIcon.Name = "nhanVienErrorIcon";
            this.nhanVienErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.nhanVienErrorIcon.TabIndex = 21;
            this.nhanVienErrorIcon.Text = "✕";
            this.nhanVienErrorIcon.Visible = false;
            // 
            // nhanVienErrorLabel
            // 
            this.nhanVienErrorLabel.AutoSize = true;
            this.nhanVienErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.nhanVienErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.nhanVienErrorLabel.Location = new System.Drawing.Point(105, 119);
            this.nhanVienErrorLabel.Name = "nhanVienErrorLabel";
            this.nhanVienErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.nhanVienErrorLabel.TabIndex = 16;
            // 
            // quyenLabel
            // 
            this.quyenLabel.AutoSize = true;
            this.quyenLabel.Location = new System.Drawing.Point(12, 140);
            this.quyenLabel.Name = "quyenLabel";
            this.quyenLabel.Size = new System.Drawing.Size(42, 13);
            this.quyenLabel.TabIndex = 6;
            this.quyenLabel.Text = "Quyền:";
            // 
            // quyenComboBox
            // 
            this.quyenComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quyenComboBox.FormattingEnabled = true;
            this.quyenComboBox.Location = new System.Drawing.Point(105, 137);
            this.quyenComboBox.Name = "quyenComboBox";
            this.quyenComboBox.Size = new System.Drawing.Size(280, 21);
            this.quyenComboBox.TabIndex = 7;
            this.quyenComboBox.SelectedIndexChanged += new System.EventHandler(this.quyenComboBox_SelectedIndexChanged);
            // 
            // quyenErrorIcon
            // 
            this.quyenErrorIcon.AutoSize = true;
            this.quyenErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.quyenErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.quyenErrorIcon.Location = new System.Drawing.Point(388, 140);
            this.quyenErrorIcon.Name = "quyenErrorIcon";
            this.quyenErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.quyenErrorIcon.TabIndex = 22;
            this.quyenErrorIcon.Text = "✕";
            this.quyenErrorIcon.Visible = false;
            // 
            // quyenErrorLabel
            // 
            this.quyenErrorLabel.AutoSize = true;
            this.quyenErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.quyenErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.quyenErrorLabel.Location = new System.Drawing.Point(105, 159);
            this.quyenErrorLabel.Name = "quyenErrorLabel";
            this.quyenErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.quyenErrorLabel.TabIndex = 17;
            // 
            // trangThaiLabel
            // 
            this.trangThaiLabel.AutoSize = true;
            this.trangThaiLabel.Location = new System.Drawing.Point(12, 180);
            this.trangThaiLabel.Name = "trangThaiLabel";
            this.trangThaiLabel.Size = new System.Drawing.Size(59, 13);
            this.trangThaiLabel.TabIndex = 8;
            this.trangThaiLabel.Text = "Trạng thái:";
            // 
            // trangThaiComboBox
            // 
            this.trangThaiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trangThaiComboBox.FormattingEnabled = true;
            this.trangThaiComboBox.Location = new System.Drawing.Point(105, 177);
            this.trangThaiComboBox.Name = "trangThaiComboBox";
            this.trangThaiComboBox.Size = new System.Drawing.Size(280, 21);
            this.trangThaiComboBox.TabIndex = 9;
            this.trangThaiComboBox.SelectedIndexChanged += new System.EventHandler(this.trangThaiComboBox_SelectedIndexChanged);
            // 
            // trangThaiErrorIcon
            // 
            this.trangThaiErrorIcon.AutoSize = true;
            this.trangThaiErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.trangThaiErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.trangThaiErrorIcon.Location = new System.Drawing.Point(388, 180);
            this.trangThaiErrorIcon.Name = "trangThaiErrorIcon";
            this.trangThaiErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.trangThaiErrorIcon.TabIndex = 23;
            this.trangThaiErrorIcon.Text = "✕";
            this.trangThaiErrorIcon.Visible = false;
            // 
            // trangThaiErrorLabel
            // 
            this.trangThaiErrorLabel.AutoSize = true;
            this.trangThaiErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.trangThaiErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.trangThaiErrorLabel.Location = new System.Drawing.Point(105, 199);
            this.trangThaiErrorLabel.Name = "trangThaiErrorLabel";
            this.trangThaiErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.trangThaiErrorLabel.TabIndex = 18;
            // 
            // maTaiKhoanLabel
            // 
            this.maTaiKhoanLabel.AutoSize = true;
            this.maTaiKhoanLabel.Location = new System.Drawing.Point(12, 220);
            this.maTaiKhoanLabel.Name = "maTaiKhoanLabel";
            this.maTaiKhoanLabel.Size = new System.Drawing.Size(79, 13);
            this.maTaiKhoanLabel.TabIndex = 10;
            this.maTaiKhoanLabel.Text = "Mã tài khoản:";
            // 
            // maTaiKhoanValueLabel
            // 
            this.maTaiKhoanValueLabel.AutoSize = true;
            this.maTaiKhoanValueLabel.Location = new System.Drawing.Point(105, 220);
            this.maTaiKhoanValueLabel.Name = "maTaiKhoanValueLabel";
            this.maTaiKhoanValueLabel.Size = new System.Drawing.Size(55, 13);
            this.maTaiKhoanValueLabel.TabIndex = 11;
            this.maTaiKhoanValueLabel.Text = "(Tự động)";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(170, 250);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "Đồng Ý";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(251, 250);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Hủy";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // Form_TaiKhoanDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 290);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.maTaiKhoanValueLabel);
            this.Controls.Add(this.maTaiKhoanLabel);
            this.Controls.Add(this.trangThaiErrorIcon);
            this.Controls.Add(this.trangThaiErrorLabel);
            this.Controls.Add(this.trangThaiComboBox);
            this.Controls.Add(this.trangThaiLabel);
            this.Controls.Add(this.quyenErrorIcon);
            this.Controls.Add(this.quyenErrorLabel);
            this.Controls.Add(this.quyenComboBox);
            this.Controls.Add(this.quyenLabel);
            this.Controls.Add(this.nhanVienErrorIcon);
            this.Controls.Add(this.nhanVienErrorLabel);
            this.Controls.Add(this.nhanVienComboBox);
            this.Controls.Add(this.nhanVienLabel);
            this.Controls.Add(this.matKhauErrorIcon);
            this.Controls.Add(this.matKhauErrorLabel);
            this.Controls.Add(this.matKhauTextBox);
            this.Controls.Add(this.matKhauLabel);
            this.Controls.Add(this.tenDangNhapErrorIcon);
            this.Controls.Add(this.tenDangNhapErrorLabel);
            this.Controls.Add(this.tenDangNhapTextBox);
            this.Controls.Add(this.tenDangNhapLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_TaiKhoanDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm tài khoản";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tenDangNhapLabel;
        private System.Windows.Forms.TextBox tenDangNhapTextBox;
        private System.Windows.Forms.Label tenDangNhapErrorLabel;
        private System.Windows.Forms.Label tenDangNhapErrorIcon;
        private System.Windows.Forms.Label matKhauLabel;
        private System.Windows.Forms.TextBox matKhauTextBox;
        private System.Windows.Forms.Label matKhauErrorLabel;
        private System.Windows.Forms.Label matKhauErrorIcon;
        private System.Windows.Forms.Label nhanVienLabel;
        private System.Windows.Forms.ComboBox nhanVienComboBox;
        private System.Windows.Forms.Label nhanVienErrorLabel;
        private System.Windows.Forms.Label nhanVienErrorIcon;
        private System.Windows.Forms.Label quyenLabel;
        private System.Windows.Forms.ComboBox quyenComboBox;
        private System.Windows.Forms.Label quyenErrorLabel;
        private System.Windows.Forms.Label quyenErrorIcon;
        private System.Windows.Forms.Label trangThaiLabel;
        private System.Windows.Forms.ComboBox trangThaiComboBox;
        private System.Windows.Forms.Label trangThaiErrorLabel;
        private System.Windows.Forms.Label trangThaiErrorIcon;
        private System.Windows.Forms.Label maTaiKhoanLabel;
        private System.Windows.Forms.Label maTaiKhoanValueLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}

