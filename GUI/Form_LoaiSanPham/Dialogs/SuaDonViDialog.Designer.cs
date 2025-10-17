namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    partial class SuaDonViDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.headerSeparator = new System.Windows.Forms.Panel();
            this.maLabel = new System.Windows.Forms.Label();
            this.maTextBox = new System.Windows.Forms.TextBox();
            this.tenLabel = new System.Windows.Forms.Label();
            this.tenTextBox = new System.Windows.Forms.TextBox();
            this.moTaLabel = new System.Windows.Forms.Label();
            this.moTaTextBox = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.headerLabel.Size = new System.Drawing.Size(600, 60);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Cập nhật đơn vị";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // headerSeparator
            // 
            this.headerSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerSeparator.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.headerSeparator.Location = new System.Drawing.Point(24, 60);
            this.headerSeparator.Name = "headerSeparator";
            this.headerSeparator.Size = new System.Drawing.Size(552, 2);
            this.headerSeparator.TabIndex = 1;
            // 
            // maLabel
            // 
            this.maLabel.AutoSize = true;
            this.maLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.maLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.maLabel.Location = new System.Drawing.Point(40, 90);
            this.maLabel.Name = "maLabel";
            this.maLabel.Size = new System.Drawing.Size(70, 19);
            this.maLabel.TabIndex = 2;
            this.maLabel.Text = "Mã đơn vị";
            // 
            // maTextBox
            // 
            this.maTextBox.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.maTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.maTextBox.Location = new System.Drawing.Point(150, 86);
            this.maTextBox.Name = "maTextBox";
            this.maTextBox.ReadOnly = true;
            this.maTextBox.Size = new System.Drawing.Size(150, 25);
            this.maTextBox.TabIndex = 3;
            this.maTextBox.TabStop = false;
            // 
            // tenLabel
            // 
            this.tenLabel.AutoSize = true;
            this.tenLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tenLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.tenLabel.Location = new System.Drawing.Point(40, 140);
            this.tenLabel.Name = "tenLabel";
            this.tenLabel.Size = new System.Drawing.Size(69, 19);
            this.tenLabel.TabIndex = 4;
            this.tenLabel.Text = "Tên đơn vị";
            // 
            // tenTextBox
            // 
            this.tenTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tenTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tenTextBox.Location = new System.Drawing.Point(40, 168);
            this.tenTextBox.MaxLength = 100;
            this.tenTextBox.Name = "tenTextBox";
            this.tenTextBox.Size = new System.Drawing.Size(500, 25);
            this.tenTextBox.TabIndex = 5;
            // 
            // moTaLabel
            // 
            this.moTaLabel.AutoSize = true;
            this.moTaLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.moTaLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.moTaLabel.Location = new System.Drawing.Point(40, 210);
            this.moTaLabel.Name = "moTaLabel";
            this.moTaLabel.Size = new System.Drawing.Size(52, 19);
            this.moTaLabel.TabIndex = 6;
            this.moTaLabel.Text = "Mô tả";
            // 
            // moTaTextBox
            // 
            this.moTaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moTaTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.moTaTextBox.Location = new System.Drawing.Point(40, 238);
            this.moTaTextBox.MaxLength = 255;
            this.moTaTextBox.Multiline = true;
            this.moTaTextBox.Name = "moTaTextBox";
            this.moTaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.moTaTextBox.Size = new System.Drawing.Size(500, 120);
            this.moTaTextBox.TabIndex = 7;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.statusLabel.Location = new System.Drawing.Point(40, 370);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(76, 19);
            this.statusLabel.TabIndex = 8;
            this.statusLabel.Text = "Trạng thái";
            // 
            // statusComboBox
            // 
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.statusComboBox.Location = new System.Drawing.Point(150, 366);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(220, 25);
            this.statusComboBox.TabIndex = 9;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.closeButton.Location = new System.Drawing.Point(338, 410);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(110, 36);
            this.closeButton.TabIndex = 10;
            this.closeButton.Text = "Đóng";
            this.closeButton.UseVisualStyleBackColor = false;
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.confirmButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.confirmButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 140, 255);
            this.confirmButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirmButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.confirmButton.ForeColor = System.Drawing.Color.White;
            this.confirmButton.Location = new System.Drawing.Point(460, 410);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(120, 36);
            this.confirmButton.TabIndex = 11;
            this.confirmButton.Text = "Xác nhận";
            this.confirmButton.UseVisualStyleBackColor = false;
            // 
            // SuaDonViDialog
            // 
            this.AcceptButton = this.confirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(600, 470);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.moTaTextBox);
            this.Controls.Add(this.moTaLabel);
            this.Controls.Add(this.tenTextBox);
            this.Controls.Add(this.tenLabel);
            this.Controls.Add(this.maTextBox);
            this.Controls.Add(this.maLabel);
            this.Controls.Add(this.headerSeparator);
            this.Controls.Add(this.headerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SuaDonViDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật đơn vị";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Panel headerSeparator;
        private System.Windows.Forms.Label maLabel;
        private System.Windows.Forms.TextBox maTextBox;
        private System.Windows.Forms.Label tenLabel;
        private System.Windows.Forms.TextBox tenTextBox;
        private System.Windows.Forms.Label moTaLabel;
        private System.Windows.Forms.TextBox moTaTextBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button confirmButton;
    }
}

