namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    partial class ThemDonViDialog
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
            this.maDonViLabel = new System.Windows.Forms.Label();
            this.maDonViTextBox = new System.Windows.Forms.TextBox();
            this.tenDonViLabel = new System.Windows.Forms.Label();
            this.tenDonViTextBox = new System.Windows.Forms.TextBox();
            this.moTaLabel = new System.Windows.Forms.Label();
            this.moTaTextBox = new System.Windows.Forms.TextBox();
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
            this.headerLabel.Text = "Thêm đơn vị";
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
            // maDonViLabel
            // 
            this.maDonViLabel.AutoSize = true;
            this.maDonViLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maDonViLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.maDonViLabel.Location = new System.Drawing.Point(40, 90);
            this.maDonViLabel.Name = "maDonViLabel";
            this.maDonViLabel.Size = new System.Drawing.Size(77, 19);
            this.maDonViLabel.TabIndex = 2;
            this.maDonViLabel.Text = "Mã đơn vị";
            // 
            // maDonViTextBox
            // 
            this.maDonViTextBox.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.maDonViTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maDonViTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maDonViTextBox.Location = new System.Drawing.Point(150, 86);
            this.maDonViTextBox.Name = "maDonViTextBox";
            this.maDonViTextBox.ReadOnly = true;
            this.maDonViTextBox.Size = new System.Drawing.Size(150, 25);
            this.maDonViTextBox.TabIndex = 3;
            this.maDonViTextBox.TabStop = false;
            // 
            // tenDonViLabel
            // 
            this.tenDonViLabel.AutoSize = true;
            this.tenDonViLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tenDonViLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.tenDonViLabel.Location = new System.Drawing.Point(40, 140);
            this.tenDonViLabel.Name = "tenDonViLabel";
            this.tenDonViLabel.Size = new System.Drawing.Size(82, 19);
            this.tenDonViLabel.TabIndex = 4;
            this.tenDonViLabel.Text = "Tên đơn vị";
            // 
            // tenDonViTextBox
            // 
            this.tenDonViTextBox.BackColor = System.Drawing.Color.White;
            this.tenDonViTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tenDonViTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tenDonViTextBox.Location = new System.Drawing.Point(40, 168);
            this.tenDonViTextBox.MaxLength = 100;
            this.tenDonViTextBox.Name = "tenDonViTextBox";
            this.tenDonViTextBox.Size = new System.Drawing.Size(440, 25);
            this.tenDonViTextBox.TabIndex = 5;
            // 
            // moTaLabel
            // 
            this.moTaLabel.AutoSize = true;
            this.moTaLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.moTaLabel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.moTaLabel.Location = new System.Drawing.Point(40, 210);
            this.moTaLabel.Name = "moTaLabel";
            this.moTaLabel.Size = new System.Drawing.Size(50, 19);
            this.moTaLabel.TabIndex = 6;
            this.moTaLabel.Text = "Mô tả";
            // 
            // moTaTextBox
            // 
            this.moTaTextBox.BackColor = System.Drawing.Color.White;
            this.moTaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moTaTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.moTaTextBox.Location = new System.Drawing.Point(40, 238);
            this.moTaTextBox.MaxLength = 255;
            this.moTaTextBox.Multiline = true;
            this.moTaTextBox.Name = "moTaTextBox";
            this.moTaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.moTaTextBox.Size = new System.Drawing.Size(440, 120);
            this.moTaTextBox.TabIndex = 7;
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
            this.closeButton.Location = new System.Drawing.Point(328, 380);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 36);
            this.closeButton.TabIndex = 8;
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
            this.confirmButton.Location = new System.Drawing.Point(444, 380);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(120, 36);
            this.confirmButton.TabIndex = 9;
            this.confirmButton.Text = "Xác nhận";
            // 
            // ThemDonViDialog
            // 
            this.AcceptButton = this.confirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(600, 440);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.moTaTextBox);
            this.Controls.Add(this.moTaLabel);
            this.Controls.Add(this.tenDonViTextBox);
            this.Controls.Add(this.tenDonViLabel);
            this.Controls.Add(this.maDonViTextBox);
            this.Controls.Add(this.maDonViLabel);
            this.Controls.Add(this.headerSeparator);
            this.Controls.Add(this.headerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemDonViDialog";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm đơn vị";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Panel headerSeparator;
        private System.Windows.Forms.Label maDonViLabel;
        private System.Windows.Forms.TextBox maDonViTextBox;
        private System.Windows.Forms.Label tenDonViLabel;
        private System.Windows.Forms.TextBox tenDonViTextBox;
        private System.Windows.Forms.Label moTaLabel;
        private System.Windows.Forms.TextBox moTaTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button confirmButton;
    }
}
