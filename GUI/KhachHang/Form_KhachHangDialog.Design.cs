namespace mini_supermarket.GUI.KhachHang
{
    partial class Form_KhachHangDialog
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
            this.hoTenLabel = new System.Windows.Forms.Label();
            this.hoTenTextBox = new System.Windows.Forms.TextBox();
            this.soDienThoaiLabel = new System.Windows.Forms.Label();
            this.soDienThoaiTextBox = new System.Windows.Forms.TextBox();
            this.trangThaiLabel = new System.Windows.Forms.Label();
            this.trangThaiComboBox = new System.Windows.Forms.ComboBox();
            this.maKhachHangLabel = new System.Windows.Forms.Label();
            this.maKhachHangValueLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.gioiTinhGroupBox = new System.Windows.Forms.GroupBox();
            this.gioiTinhGroupBox.SuspendLayout();
            this.SuspendLayout();

            // add init
            this.diaChiLabel = new System.Windows.Forms.Label();
            this.diaChiTextBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.diemTichLuyLabel = new System.Windows.Forms.Label();
            this.diemTichLuyTextBox = new System.Windows.Forms.TextBox();
            this.diaChiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemTichLuyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            // end add init

            // 
            // hoTenLabel
            // 
            this.hoTenLabel.AutoSize = true;
            this.hoTenLabel.Location = new System.Drawing.Point(12, 20);
            this.hoTenLabel.Name = "hoTenLabel";
            this.hoTenLabel.Size = new System.Drawing.Size(50, 13);
            this.hoTenLabel.TabIndex = 0;
            this.hoTenLabel.Text = "Họ Tên:";

            // 
            // hoTenTextBox
            // 
            this.hoTenTextBox.Location = new System.Drawing.Point(100, 17);
            this.hoTenTextBox.Name = "hoTenTextBox";
            this.hoTenTextBox.Size = new System.Drawing.Size(200, 20);
            this.hoTenTextBox.TabIndex = 1;

            // 
            // soDienThoaiLabel
            // 
            this.soDienThoaiLabel.AutoSize = true;
            this.soDienThoaiLabel.Location = new System.Drawing.Point(12, 50);
            this.soDienThoaiLabel.Name = "soDienThoaiLabel";
            this.soDienThoaiLabel.Size = new System.Drawing.Size(82, 13);
            this.soDienThoaiLabel.TabIndex = 2;
            this.soDienThoaiLabel.Text = "Số Điện Thoại:";

            // 
            // soDienThoaiTextBox
            // 
            this.soDienThoaiTextBox.Location = new System.Drawing.Point(100, 47);
            this.soDienThoaiTextBox.Name = "soDienThoaiTextBox";
            this.soDienThoaiTextBox.Size = new System.Drawing.Size(200, 20);
            this.soDienThoaiTextBox.TabIndex = 3;

            // 
            // diaChiLabel
            // 
            this.diaChiLabel.AutoSize = true;
            this.diaChiLabel.Location = new System.Drawing.Point(12, 80);
            this.diaChiLabel.Name = "diaChiLabel";
            this.diaChiLabel.Size = new System.Drawing.Size(62, 13);
            this.diaChiLabel.TabIndex = 4;
            this.diaChiLabel.Text = "Địa chỉ:";

            // 
            // diaChiTextBox
            // 
            this.diaChiTextBox.Location = new System.Drawing.Point(100, 77);
            this.diaChiTextBox.Name = "diaChiTextBox";
            this.diaChiTextBox.Size = new System.Drawing.Size(200, 20);
            this.diaChiTextBox.TabIndex = 5;

            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(12, 110);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(62, 13);
            this.emailLabel.TabIndex = 6;
            this.emailLabel.Text = "Email:";

            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(100, 107);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(200, 20);
            this.emailTextBox.TabIndex = 7;

            // 
            // diemTichLuyLabel
            // 
            this.diemTichLuyLabel.AutoSize = true;
            this.diemTichLuyLabel.Location = new System.Drawing.Point(12, 140);
            this.diemTichLuyLabel.Name = "diemTichLuyLabel";
            this.diemTichLuyLabel.Size = new System.Drawing.Size(62, 13);
            this.diemTichLuyLabel.TabIndex = 8;
            this.diemTichLuyLabel.Text = "Điểm tích lũy:";

            // 
            // diemTichLuyTextBox
            // 
            this.diemTichLuyTextBox.Location = new System.Drawing.Point(100, 137);
            this.diemTichLuyTextBox.Name = "diemTichLuyTextBox";
            this.diemTichLuyTextBox.Size = new System.Drawing.Size(200, 20);
            this.diemTichLuyTextBox.TabIndex = 9;

            // 
            // trangThaiLabel
            // 
            this.trangThaiLabel.AutoSize = true;
            this.trangThaiLabel.Location = new System.Drawing.Point(12, 170);
            this.trangThaiLabel.Name = "trangThaiLabel";
            this.trangThaiLabel.Size = new System.Drawing.Size(62, 13);
            this.trangThaiLabel.TabIndex = 10;
            this.trangThaiLabel.Text = "Trạng Thái:";

            // 
            // trangThaiComboBox
            // 
            this.trangThaiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trangThaiComboBox.Location = new System.Drawing.Point(100, 167);
            this.trangThaiComboBox.Name = "trangThaiComboBox";
            this.trangThaiComboBox.Size = new System.Drawing.Size(200, 21);
            this.trangThaiComboBox.TabIndex = 11;

            // 
            // maKhachHangLabel
            // 
            this.maKhachHangLabel.AutoSize = true;
            this.maKhachHangLabel.Location = new System.Drawing.Point(12, 200);
            this.maKhachHangLabel.Name = "maKhachHangLabel";
            this.maKhachHangLabel.Size = new System.Drawing.Size(89, 13);
            this.maKhachHangLabel.TabIndex = 12;
            this.maKhachHangLabel.Text = "Mã Khách Hàng:";

            // 
            // maKhachHangValueLabel
            // 
            this.maKhachHangValueLabel.AutoSize = true;
            this.maKhachHangValueLabel.Location = new System.Drawing.Point(100, 200);
            this.maKhachHangValueLabel.Name = "maKhachHangValueLabel";
            this.maKhachHangValueLabel.Size = new System.Drawing.Size(50, 13);
            this.maKhachHangValueLabel.TabIndex = 13;
            this.maKhachHangValueLabel.Text = "(Tự động)";

            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(144, 220);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 14;
            this.okButton.Text = "Đồng Ý";
            this.okButton.UseVisualStyleBackColor = true;

            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(225, 220);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Hủy";
            this.cancelButton.UseVisualStyleBackColor = true;

            // 
            // Form_KhachHangDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 260);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.maKhachHangValueLabel);
            this.Controls.Add(this.maKhachHangLabel);
            this.Controls.Add(this.trangThaiComboBox);
            this.Controls.Add(this.trangThaiLabel);
            this.Controls.Add(this.soDienThoaiTextBox);
            this.Controls.Add(this.soDienThoaiLabel);
            this.Controls.Add(this.hoTenTextBox);
            this.Controls.Add(this.hoTenLabel);

            // add new component in group box
            this.Controls.Add(this.diaChiTextBox);
            this.Controls.Add(this.diaChiLabel);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.diemTichLuyTextBox);
            this.Controls.Add(this.diemTichLuyLabel);
            // end add

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_KhachHangDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm khách hàng";
            this.gioiTinhGroupBox.ResumeLayout(false);
            this.gioiTinhGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label? hoTenLabel;
        private System.Windows.Forms.TextBox? hoTenTextBox;
        private System.Windows.Forms.Label? soDienThoaiLabel;
        private System.Windows.Forms.TextBox? soDienThoaiTextBox;
        private System.Windows.Forms.Label? trangThaiLabel;
        private System.Windows.Forms.ComboBox? trangThaiComboBox;
        private System.Windows.Forms.Label? maKhachHangLabel;
        private System.Windows.Forms.Label? maKhachHangValueLabel;
        private System.Windows.Forms.Button? okButton;
        private System.Windows.Forms.Button? cancelButton;
        private System.Windows.Forms.GroupBox? gioiTinhGroupBox;

        // Add components
        // diaChi component
        private System.Windows.Forms.Label? diaChiLabel;
        private System.Windows.Forms.TextBox? diaChiTextBox;
        // email component
        private System.Windows.Forms.Label? emailLabel;
        private System.Windows.Forms.TextBox? emailTextBox;
        // diemTichLuy component
        private System.Windows.Forms.Label? diemTichLuyLabel;
        private System.Windows.Forms.TextBox? diemTichLuyTextBox;
        // end add component
        // add columns
        private System.Windows.Forms.DataGridViewTextBoxColumn? diaChiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn? emailColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn? diemTichLuyColumn;
        // end add columns
    }
}