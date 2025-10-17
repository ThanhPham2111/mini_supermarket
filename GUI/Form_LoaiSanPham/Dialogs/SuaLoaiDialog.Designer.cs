namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    partial class SuaLoaiDialog
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
            this.maLoaiLabel = new System.Windows.Forms.Label();
            this.maLoaiTextBox = new System.Windows.Forms.TextBox();
            this.tenLoaiLabel = new System.Windows.Forms.Label();
            this.tenLoaiTextBox = new System.Windows.Forms.TextBox();
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
            this.headerLabel.Text = "Cập nhật loại sản phẩm";
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
            // maLoaiLabel
            // 
            this.maLoaiLabel.AutoSize = true;
            this.maLoaiLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.maLoaiLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.maLoaiLabel.Location = new System.Drawing.Point(40, 90);
            this.maLoaiLabel.Name = "maLoaiLabel";
            this.maLoaiLabel.Size = new System.Drawing.Size(63, 19);
            this.maLoaiLabel.TabIndex = 2;
            this.maLoaiLabel.Text = "Mã loại";
            // 
            // maLoaiTextBox
            // 
            this.maLoaiTextBox.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.maLoaiTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maLoaiTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.maLoaiTextBox.Location = new System.Drawing.Point(150, 86);
            this.maLoaiTextBox.Name = "maLoaiTextBox";
            this.maLoaiTextBox.ReadOnly = true;
            this.maLoaiTextBox.Size = new System.Drawing.Size(150, 25);
            this.maLoaiTextBox.TabIndex = 3;
            this.maLoaiTextBox.TabStop = false;
            // 
            // tenLoaiLabel
            // 
            this.tenLoaiLabel.AutoSize = true;
            this.tenLoaiLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tenLoaiLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.tenLoaiLabel.Location = new System.Drawing.Point(40, 140);
            this.tenLoaiLabel.Name = "tenLoaiLabel";
            this.tenLoaiLabel.Size = new System.Drawing.Size(62, 19);
            this.tenLoaiLabel.TabIndex = 4;
            this.tenLoaiLabel.Text = "Tên loại";
            // 
            // tenLoaiTextBox
            // 
            this.tenLoaiTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tenLoaiTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tenLoaiTextBox.Location = new System.Drawing.Point(40, 168);
            this.tenLoaiTextBox.MaxLength = 255;
            this.tenLoaiTextBox.Name = "tenLoaiTextBox";
            this.tenLoaiTextBox.Size = new System.Drawing.Size(460, 25);
            this.tenLoaiTextBox.TabIndex = 5;
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
            this.moTaTextBox.MaxLength = 500;
            this.moTaTextBox.Multiline = true;
            this.moTaTextBox.Name = "moTaTextBox";
            this.moTaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.moTaTextBox.Size = new System.Drawing.Size(460, 120);
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
            this.statusComboBox.Size = new System.Drawing.Size(200, 25);
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
            // SuaLoaiDialog
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
            this.Controls.Add(this.tenLoaiTextBox);
            this.Controls.Add(this.tenLoaiLabel);
            this.Controls.Add(this.maLoaiTextBox);
            this.Controls.Add(this.maLoaiLabel);
            this.Controls.Add(this.headerSeparator);
            this.Controls.Add(this.headerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SuaLoaiDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật loại sản phẩm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Panel headerSeparator;
        private System.Windows.Forms.Label maLoaiLabel;
        private System.Windows.Forms.TextBox maLoaiTextBox;
        private System.Windows.Forms.Label tenLoaiLabel;
        private System.Windows.Forms.TextBox tenLoaiTextBox;
        private System.Windows.Forms.Label moTaLabel;
        private System.Windows.Forms.TextBox moTaTextBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button confirmButton;
    }
}

