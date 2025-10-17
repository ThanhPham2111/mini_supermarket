namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    partial class SuaThuongHieuDialog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.headerSeparator = new System.Windows.Forms.Panel();
            this.maThuongHieuLabel = new System.Windows.Forms.Label();
            this.maThuongHieuTextBox = new System.Windows.Forms.TextBox();
            this.tenThuongHieuLabel = new System.Windows.Forms.Label();
            this.tenThuongHieuTextBox = new System.Windows.Forms.TextBox();
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
            this.headerLabel.Size = new System.Drawing.Size(540, 60);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Cập nhật thương hiệu";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // headerSeparator
            // 
            this.headerSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerSeparator.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.headerSeparator.Location = new System.Drawing.Point(24, 60);
            this.headerSeparator.Name = "headerSeparator";
            this.headerSeparator.Size = new System.Drawing.Size(492, 2);
            this.headerSeparator.TabIndex = 1;
            // 
            // maThuongHieuLabel
            // 
            this.maThuongHieuLabel.AutoSize = true;
            this.maThuongHieuLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maThuongHieuLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.maThuongHieuLabel.Location = new System.Drawing.Point(40, 90);
            this.maThuongHieuLabel.Name = "maThuongHieuLabel";
            this.maThuongHieuLabel.Size = new System.Drawing.Size(111, 19);
            this.maThuongHieuLabel.TabIndex = 2;
            this.maThuongHieuLabel.Text = "Mã thương hiệu";
            // 
            // maThuongHieuTextBox
            // 
            this.maThuongHieuTextBox.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.maThuongHieuTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maThuongHieuTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maThuongHieuTextBox.Location = new System.Drawing.Point(190, 86);
            this.maThuongHieuTextBox.Name = "maThuongHieuTextBox";
            this.maThuongHieuTextBox.ReadOnly = true;
            this.maThuongHieuTextBox.Size = new System.Drawing.Size(150, 25);
            this.maThuongHieuTextBox.TabIndex = 3;
            this.maThuongHieuTextBox.TabStop = false;
            // 
            // tenThuongHieuLabel
            // 
            this.tenThuongHieuLabel.AutoSize = true;
            this.tenThuongHieuLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tenThuongHieuLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.tenThuongHieuLabel.Location = new System.Drawing.Point(40, 140);
            this.tenThuongHieuLabel.Name = "tenThuongHieuLabel";
            this.tenThuongHieuLabel.Size = new System.Drawing.Size(117, 19);
            this.tenThuongHieuLabel.TabIndex = 4;
            this.tenThuongHieuLabel.Text = "Tên thương hiệu";
            // 
            // tenThuongHieuTextBox
            // 
            this.tenThuongHieuTextBox.BackColor = System.Drawing.Color.White;
            this.tenThuongHieuTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tenThuongHieuTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tenThuongHieuTextBox.Location = new System.Drawing.Point(40, 168);
            this.tenThuongHieuTextBox.MaxLength = 255;
            this.tenThuongHieuTextBox.Name = "tenThuongHieuTextBox";
            this.tenThuongHieuTextBox.Size = new System.Drawing.Size(440, 25);
            this.tenThuongHieuTextBox.TabIndex = 5;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.closeButton.ForeColor = System.Drawing.Color.Black;
            this.closeButton.Location = new System.Drawing.Point(272, 250);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 36);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Đóng";
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.confirmButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.confirmButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 140, 255);
            this.confirmButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirmButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.confirmButton.ForeColor = System.Drawing.Color.White;
            this.confirmButton.Location = new System.Drawing.Point(388, 250);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(120, 36);
            this.confirmButton.TabIndex = 7;
            this.confirmButton.Text = "Xác nhận";
            // 
            // SuaThuongHieuDialog
            // 
            this.AcceptButton = this.confirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(540, 320);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tenThuongHieuTextBox);
            this.Controls.Add(this.tenThuongHieuLabel);
            this.Controls.Add(this.maThuongHieuTextBox);
            this.Controls.Add(this.maThuongHieuLabel);
            this.Controls.Add(this.headerSeparator);
            this.Controls.Add(this.headerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SuaThuongHieuDialog";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật thương hiệu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Panel headerSeparator;
        private System.Windows.Forms.Label maThuongHieuLabel;
        private System.Windows.Forms.TextBox maThuongHieuTextBox;
        private System.Windows.Forms.Label tenThuongHieuLabel;
        private System.Windows.Forms.TextBox tenThuongHieuTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button confirmButton;
    }
}
