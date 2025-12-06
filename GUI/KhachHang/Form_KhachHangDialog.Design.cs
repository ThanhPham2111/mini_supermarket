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
            // hoTenErrorIcon
            // 
            this.hoTenErrorIcon = new System.Windows.Forms.Label();
            this.hoTenErrorIcon.AutoSize = true;
            this.hoTenErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.hoTenErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.hoTenErrorIcon.Location = new System.Drawing.Point(308, 20);
            this.hoTenErrorIcon.Name = "hoTenErrorIcon";
            this.hoTenErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.hoTenErrorIcon.TabIndex = 16;
            this.hoTenErrorIcon.Text = "✕";
            this.hoTenErrorIcon.Visible = false;
            // 
            // hoTenErrorLabel
            // 
            this.hoTenErrorLabel = new System.Windows.Forms.Label();
            this.hoTenErrorLabel.AutoSize = true;
            this.hoTenErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.hoTenErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.hoTenErrorLabel.Location = new System.Drawing.Point(100, 38);
            this.hoTenErrorLabel.Name = "hoTenErrorLabel";
            this.hoTenErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.hoTenErrorLabel.TabIndex = 17;
            // 
            // soDienThoaiLabel
            // 
            this.soDienThoaiLabel.AutoSize = true;
            this.soDienThoaiLabel.Location = new System.Drawing.Point(12, 50 + 5);
            this.soDienThoaiLabel.Name = "soDienThoaiLabel";
            this.soDienThoaiLabel.Size = new System.Drawing.Size(82, 13);
            this.soDienThoaiLabel.TabIndex = 2;
            this.soDienThoaiLabel.Text = "Số Điện Thoại:";

            // 
            // soDienThoaiTextBox
            // 
            this.soDienThoaiTextBox.Location = new System.Drawing.Point(100, 47 + 5);
            this.soDienThoaiTextBox.Name = "soDienThoaiTextBox";
            this.soDienThoaiTextBox.Size = new System.Drawing.Size(200, 20);
            this.soDienThoaiTextBox.TabIndex = 3;
            // 
            // soDienThoaiErrorIcon
            // 
            this.soDienThoaiErrorIcon = new System.Windows.Forms.Label();
            this.soDienThoaiErrorIcon.AutoSize = true;
            this.soDienThoaiErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.soDienThoaiErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.soDienThoaiErrorIcon.Location = new System.Drawing.Point(308, 50 + 5);
            this.soDienThoaiErrorIcon.Name = "soDienThoaiErrorIcon";
            this.soDienThoaiErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.soDienThoaiErrorIcon.TabIndex = 18;
            this.soDienThoaiErrorIcon.Text = "✕";
            this.soDienThoaiErrorIcon.Visible = false;
            // 
            // soDienThoaiErrorLabel
            // 
            this.soDienThoaiErrorLabel = new System.Windows.Forms.Label();
            this.soDienThoaiErrorLabel.AutoSize = true;
            this.soDienThoaiErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.soDienThoaiErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.soDienThoaiErrorLabel.Location = new System.Drawing.Point(100, 68 + 5);
            this.soDienThoaiErrorLabel.Name = "soDienThoaiErrorLabel";
            this.soDienThoaiErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.soDienThoaiErrorLabel.TabIndex = 19;
            // 
            // diaChiLabel
            // 
            this.diaChiLabel.AutoSize = true;
            this.diaChiLabel.Location = new System.Drawing.Point(12, 80 + 10);
            this.diaChiLabel.Name = "diaChiLabel";
            this.diaChiLabel.Size = new System.Drawing.Size(62, 13);
            this.diaChiLabel.TabIndex = 4;
            this.diaChiLabel.Text = "Địa chỉ:";

            // 
            // diaChiTextBox
            // 
            this.diaChiTextBox.Location = new System.Drawing.Point(100, 77 + 10);
            this.diaChiTextBox.Name = "diaChiTextBox";
            this.diaChiTextBox.Size = new System.Drawing.Size(200, 20);
            this.diaChiTextBox.TabIndex = 5;
            // 
            // diaChiErrorIcon
            // 
            this.diaChiErrorIcon = new System.Windows.Forms.Label();
            this.diaChiErrorIcon.AutoSize = true;
            this.diaChiErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.diaChiErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.diaChiErrorIcon.Location = new System.Drawing.Point(308, 80 + 10);
            this.diaChiErrorIcon.Name = "diaChiErrorIcon";
            this.diaChiErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.diaChiErrorIcon.TabIndex = 20;
            this.diaChiErrorIcon.Text = "✕";
            this.diaChiErrorIcon.Visible = false;
            // 
            // diaChiErrorLabel
            // 
            this.diaChiErrorLabel = new System.Windows.Forms.Label();
            this.diaChiErrorLabel.AutoSize = true;
            this.diaChiErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.diaChiErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.diaChiErrorLabel.Location = new System.Drawing.Point(100, 98 + 10);
            this.diaChiErrorLabel.Name = "diaChiErrorLabel";
            this.diaChiErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.diaChiErrorLabel.TabIndex = 21;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(12, 110 + 15);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(62, 13);
            this.emailLabel.TabIndex = 6;
            this.emailLabel.Text = "Email:";

            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(100, 107 + 15);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(200, 20);
            this.emailTextBox.TabIndex = 7;
            // 
            // emailErrorIcon
            // 
            this.emailErrorIcon = new System.Windows.Forms.Label();
            this.emailErrorIcon.AutoSize = true;
            this.emailErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.emailErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.emailErrorIcon.Location = new System.Drawing.Point(308, 110 + 15);
            this.emailErrorIcon.Name = "emailErrorIcon";
            this.emailErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.emailErrorIcon.TabIndex = 22;
            this.emailErrorIcon.Text = "✕";
            this.emailErrorIcon.Visible = false;
            // 
            // emailErrorLabel
            // 
            this.emailErrorLabel = new System.Windows.Forms.Label();
            this.emailErrorLabel.AutoSize = true;
            this.emailErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.emailErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.emailErrorLabel.Location = new System.Drawing.Point(100, 128 + 15);
            this.emailErrorLabel.Name = "emailErrorLabel";
            this.emailErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.emailErrorLabel.TabIndex = 23;
            // 
            // diemTichLuyLabel
            // 
            this.diemTichLuyLabel.AutoSize = true;
            this.diemTichLuyLabel.Location = new System.Drawing.Point(12, 140 + 20);
            this.diemTichLuyLabel.Name = "diemTichLuyLabel";
            this.diemTichLuyLabel.Size = new System.Drawing.Size(62, 13);
            this.diemTichLuyLabel.TabIndex = 8;
            this.diemTichLuyLabel.Text = "Điểm tích lũy:";

            // 
            // diemTichLuyTextBox
            // 
            this.diemTichLuyTextBox.Location = new System.Drawing.Point(100, 137 + 20);
            this.diemTichLuyTextBox.Name = "diemTichLuyTextBox";
            this.diemTichLuyTextBox.Size = new System.Drawing.Size(200, 20);
            this.diemTichLuyTextBox.TabIndex = 9;
            // 
            // diemTichLuyErrorIcon
            // 
            this.diemTichLuyErrorIcon = new System.Windows.Forms.Label();
            this.diemTichLuyErrorIcon.AutoSize = true;
            this.diemTichLuyErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.diemTichLuyErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.diemTichLuyErrorIcon.Location = new System.Drawing.Point(308, 140 + 20);
            this.diemTichLuyErrorIcon.Name = "diemTichLuyErrorIcon";
            this.diemTichLuyErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.diemTichLuyErrorIcon.TabIndex = 24;
            this.diemTichLuyErrorIcon.Text = "✕";
            this.diemTichLuyErrorIcon.Visible = false;
            // 
            // diemTichLuyErrorLabel
            // 
            this.diemTichLuyErrorLabel = new System.Windows.Forms.Label();
            this.diemTichLuyErrorLabel.AutoSize = true;
            this.diemTichLuyErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.diemTichLuyErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.diemTichLuyErrorLabel.Location = new System.Drawing.Point(100, 158 + 20);
            this.diemTichLuyErrorLabel.Name = "diemTichLuyErrorLabel";
            this.diemTichLuyErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.diemTichLuyErrorLabel.TabIndex = 25;
            // 
            // trangThaiLabel
            // 
            this.trangThaiLabel.AutoSize = true;
            this.trangThaiLabel.Location = new System.Drawing.Point(12, 170 + 25);
            this.trangThaiLabel.Name = "trangThaiLabel";
            this.trangThaiLabel.Size = new System.Drawing.Size(62, 13);
            this.trangThaiLabel.TabIndex = 10;
            this.trangThaiLabel.Text = "Trạng Thái:";

            // 
            // trangThaiComboBox
            // 
            this.trangThaiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trangThaiComboBox.Location = new System.Drawing.Point(100, 167 + 25);
            this.trangThaiComboBox.Name = "trangThaiComboBox";
            this.trangThaiComboBox.Size = new System.Drawing.Size(200, 21);
            this.trangThaiComboBox.TabIndex = 11;
            // 
            // trangThaiErrorIcon
            // 
            this.trangThaiErrorIcon = new System.Windows.Forms.Label();
            this.trangThaiErrorIcon.AutoSize = true;
            this.trangThaiErrorIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.trangThaiErrorIcon.ForeColor = System.Drawing.Color.Red;
            this.trangThaiErrorIcon.Location = new System.Drawing.Point(308, 170 + 25);
            this.trangThaiErrorIcon.Name = "trangThaiErrorIcon";
            this.trangThaiErrorIcon.Size = new System.Drawing.Size(13, 17);
            this.trangThaiErrorIcon.TabIndex = 24;
            this.trangThaiErrorIcon.Text = "✕";
            this.trangThaiErrorIcon.Visible = false;
            // 
            // trangThaiErrorLabel
            // 
            this.trangThaiErrorLabel = new System.Windows.Forms.Label();
            this.trangThaiErrorLabel.AutoSize = true;
            this.trangThaiErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.trangThaiErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.trangThaiErrorLabel.Location = new System.Drawing.Point(100, 189 + 25);
            this.trangThaiErrorLabel.Name = "trangThaiErrorLabel";
            this.trangThaiErrorLabel.Size = new System.Drawing.Size(0, 10);
            this.trangThaiErrorLabel.TabIndex = 25;
            // 
            // maKhachHangLabel
            // 
            this.maKhachHangLabel.AutoSize = true;
            this.maKhachHangLabel.Location = new System.Drawing.Point(12, 200 + 25);
            this.maKhachHangLabel.Name = "maKhachHangLabel";
            this.maKhachHangLabel.Size = new System.Drawing.Size(89, 13);
            this.maKhachHangLabel.TabIndex = 12;
            this.maKhachHangLabel.Text = "Mã Khách Hàng:";

            // 
            // maKhachHangValueLabel
            // 
            this.maKhachHangValueLabel.AutoSize = true;
            this.maKhachHangValueLabel.Location = new System.Drawing.Point(100, 200 + 25);
            this.maKhachHangValueLabel.Name = "maKhachHangValueLabel";
            this.maKhachHangValueLabel.Size = new System.Drawing.Size(50, 13);
            this.maKhachHangValueLabel.TabIndex = 13;
            this.maKhachHangValueLabel.Text = "(Tự động)";

            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(144, 220 + 25);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 14;
            this.okButton.Text = "Đồng Ý";
            this.okButton.UseVisualStyleBackColor = true;

            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(225, 220 + 25);
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
            this.ClientSize = new System.Drawing.Size(340, 280);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.maKhachHangValueLabel);
            this.Controls.Add(this.maKhachHangLabel);
            this.Controls.Add(this.trangThaiErrorIcon);
            this.Controls.Add(this.trangThaiErrorLabel);
            this.Controls.Add(this.trangThaiComboBox);
            this.Controls.Add(this.trangThaiLabel);
            this.Controls.Add(this.soDienThoaiErrorIcon);
            this.Controls.Add(this.soDienThoaiErrorLabel);
            this.Controls.Add(this.soDienThoaiTextBox);
            this.Controls.Add(this.soDienThoaiLabel);
            this.Controls.Add(this.hoTenErrorIcon);
            this.Controls.Add(this.hoTenErrorLabel);
            this.Controls.Add(this.hoTenTextBox);
            this.Controls.Add(this.hoTenLabel);

            // add new component in group box
            this.Controls.Add(this.diaChiErrorIcon);
            this.Controls.Add(this.diaChiErrorLabel);
            this.Controls.Add(this.diaChiTextBox);
            this.Controls.Add(this.diaChiLabel);
            this.Controls.Add(this.emailErrorIcon);
            this.Controls.Add(this.emailErrorLabel);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.diemTichLuyTextBox);
            this.Controls.Add(this.diemTichLuyLabel);
            this.Controls.Add(this.diemTichLuyErrorIcon);
            this.Controls.Add(this.diemTichLuyErrorLabel);
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
        private System.Windows.Forms.Label? hoTenErrorIcon;
        private System.Windows.Forms.Label? hoTenErrorLabel;
        private System.Windows.Forms.Label? soDienThoaiLabel;
        private System.Windows.Forms.TextBox? soDienThoaiTextBox;
        private System.Windows.Forms.Label? soDienThoaiErrorIcon;
        private System.Windows.Forms.Label? soDienThoaiErrorLabel;
        private System.Windows.Forms.Label? trangThaiLabel;
        private System.Windows.Forms.ComboBox? trangThaiComboBox;
        private System.Windows.Forms.Label? trangThaiErrorIcon;
        private System.Windows.Forms.Label? trangThaiErrorLabel;
        private System.Windows.Forms.Label? maKhachHangLabel;
        private System.Windows.Forms.Label? maKhachHangValueLabel;
        private System.Windows.Forms.Button? okButton;
        private System.Windows.Forms.Button? cancelButton;
        private System.Windows.Forms.GroupBox? gioiTinhGroupBox;

        // Add components
        // diaChi component
        private System.Windows.Forms.Label? diaChiLabel;
        private System.Windows.Forms.TextBox? diaChiTextBox;
        private System.Windows.Forms.Label? diaChiErrorIcon;
        private System.Windows.Forms.Label? diaChiErrorLabel;
        // email component
        private System.Windows.Forms.Label? emailLabel;
        private System.Windows.Forms.TextBox? emailTextBox;
        private System.Windows.Forms.Label? emailErrorIcon;
        private System.Windows.Forms.Label? emailErrorLabel;
        // diemTichLuy component
        private System.Windows.Forms.Label? diemTichLuyLabel;
        private System.Windows.Forms.TextBox? diemTichLuyTextBox;
        private System.Windows.Forms.Label? diemTichLuyErrorIcon;
        private System.Windows.Forms.Label? diemTichLuyErrorLabel;
        // end add component
        // add columns
        private System.Windows.Forms.DataGridViewTextBoxColumn? diaChiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn? emailColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn? diemTichLuyColumn;
        // end add columns
    }
}