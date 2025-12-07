namespace mini_supermarket.GUI.Form_SanPham
{
    partial class Form_SanPhamDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Label maSanPhamLabel;
        private System.Windows.Forms.TextBox maSanPhamTextBox;
        private System.Windows.Forms.Label tenSanPhamLabel;
        private System.Windows.Forms.TextBox tenSanPhamTextBox;
        private System.Windows.Forms.Label donViLabel;
        private System.Windows.Forms.ComboBox donViComboBox;
        private System.Windows.Forms.Label maThuongHieuLabel;
        private System.Windows.Forms.TextBox maThuongHieuTextBox;
        private System.Windows.Forms.Label maLoaiLabel;
        private System.Windows.Forms.ComboBox maLoaiComboBox;
        private System.Windows.Forms.Label giaBanLabel;
        private System.Windows.Forms.TextBox giaBanTextBox;
        private System.Windows.Forms.Label xuatXuLabel;
        private System.Windows.Forms.TextBox xuatXuTextBox;
        private System.Windows.Forms.Label hsdLabel;
        private System.Windows.Forms.TextBox hsdTextBox;
            private System.Windows.Forms.Label nhaCungCapLabel;
            private System.Windows.Forms.ComboBox nhaCungCapComboBox;
        private System.Windows.Forms.Label moTaLabel;
        private System.Windows.Forms.TextBox moTaTextBox;
        private System.Windows.Forms.PictureBox productPictureBox;
        private System.Windows.Forms.Label hinhAnhLabel;
        private System.Windows.Forms.Button closeButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            headerPanel = new System.Windows.Forms.Panel();
            titleLabel = new System.Windows.Forms.Label();
            contentPanel = new System.Windows.Forms.Panel();
            moTaTextBox = new System.Windows.Forms.TextBox();
            moTaLabel = new System.Windows.Forms.Label();
                nhaCungCapComboBox = new System.Windows.Forms.ComboBox();
                nhaCungCapLabel = new System.Windows.Forms.Label();
            hsdTextBox = new System.Windows.Forms.TextBox();
            hsdLabel = new System.Windows.Forms.Label();
            xuatXuTextBox = new System.Windows.Forms.TextBox();
            xuatXuLabel = new System.Windows.Forms.Label();
            giaBanTextBox = new System.Windows.Forms.TextBox();
            giaBanLabel = new System.Windows.Forms.Label();
            maLoaiComboBox = new System.Windows.Forms.ComboBox();
            maLoaiLabel = new System.Windows.Forms.Label();
            maThuongHieuTextBox = new System.Windows.Forms.TextBox();
            maThuongHieuLabel = new System.Windows.Forms.Label();
            donViComboBox = new System.Windows.Forms.ComboBox();
            donViLabel = new System.Windows.Forms.Label();
            tenSanPhamTextBox = new System.Windows.Forms.TextBox();
            tenSanPhamLabel = new System.Windows.Forms.Label();
            maSanPhamTextBox = new System.Windows.Forms.TextBox();
            maSanPhamLabel = new System.Windows.Forms.Label();
            productPictureBox = new System.Windows.Forms.PictureBox();
            hinhAnhLabel = new System.Windows.Forms.Label();
            closeButton = new System.Windows.Forms.Button();
            headerPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productPictureBox).BeginInit();
            SuspendLayout();

            // headerPanel
            headerPanel.BackColor = System.Drawing.Color.FromArgb(0, 120, 215); // Gradient-like blue
            headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            headerPanel.Location = new System.Drawing.Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            headerPanel.Size = new System.Drawing.Size(800, 70);
            headerPanel.TabIndex = 0;
            headerPanel.Controls.Add(titleLabel);

            // titleLabel
            titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            titleLabel.ForeColor = System.Drawing.Color.White;
            titleLabel.Location = new System.Drawing.Point(20, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(760, 40);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Xem chi tiết sản phẩm";
            titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // contentPanel
            contentPanel.BackColor = System.Drawing.Color.White;
            contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            contentPanel.Location = new System.Drawing.Point(0, 70);
            contentPanel.Name = "contentPanel";
            contentPanel.Padding = new System.Windows.Forms.Padding(20);
            contentPanel.Size = new System.Drawing.Size(800, 530);
            contentPanel.TabIndex = 1;
            contentPanel.Controls.Add(closeButton);
            contentPanel.Controls.Add(productPictureBox);
            contentPanel.Controls.Add(hinhAnhLabel);
            contentPanel.Controls.Add(moTaTextBox);
            contentPanel.Controls.Add(moTaLabel);
                contentPanel.Controls.Add(nhaCungCapComboBox);
                contentPanel.Controls.Add(nhaCungCapLabel);
            contentPanel.Controls.Add(hsdTextBox);
            contentPanel.Controls.Add(hsdLabel);
            contentPanel.Controls.Add(xuatXuTextBox);
            contentPanel.Controls.Add(xuatXuLabel);
            contentPanel.Controls.Add(giaBanTextBox);
            contentPanel.Controls.Add(giaBanLabel);
            contentPanel.Controls.Add(maLoaiComboBox);
            contentPanel.Controls.Add(maLoaiLabel);
            contentPanel.Controls.Add(maThuongHieuTextBox);
            contentPanel.Controls.Add(maThuongHieuLabel);
            contentPanel.Controls.Add(donViComboBox);
            contentPanel.Controls.Add(donViLabel);
            contentPanel.Controls.Add(tenSanPhamTextBox);
            contentPanel.Controls.Add(tenSanPhamLabel);
            contentPanel.Controls.Add(maSanPhamTextBox);
            contentPanel.Controls.Add(maSanPhamLabel);

            // maSanPhamLabel
            maSanPhamLabel.AutoSize = true;
            maSanPhamLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            maSanPhamLabel.Location = new System.Drawing.Point(20, 20);
            maSanPhamLabel.Name = "maSanPhamLabel";
            maSanPhamLabel.Size = new System.Drawing.Size(100, 20);
            maSanPhamLabel.TabIndex = 0;
            maSanPhamLabel.Text = "Mã sản phẩm";

            // maSanPhamTextBox
            maSanPhamTextBox.Location = new System.Drawing.Point(150, 18);
            maSanPhamTextBox.Name = "maSanPhamTextBox";
            maSanPhamTextBox.ReadOnly = true;
            maSanPhamTextBox.Size = new System.Drawing.Size(250, 25);
            maSanPhamTextBox.TabIndex = 1;

            // tenSanPhamLabel
            tenSanPhamLabel.AutoSize = true;
            tenSanPhamLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tenSanPhamLabel.Location = new System.Drawing.Point(20, 60);
            tenSanPhamLabel.Name = "tenSanPhamLabel";
            tenSanPhamLabel.Size = new System.Drawing.Size(105, 20);
            tenSanPhamLabel.TabIndex = 2;
            tenSanPhamLabel.Text = "Tên sản phẩm";

            // tenSanPhamTextBox
            tenSanPhamTextBox.Location = new System.Drawing.Point(150, 58);
            tenSanPhamTextBox.Name = "tenSanPhamTextBox";
            tenSanPhamTextBox.ReadOnly = true;
            tenSanPhamTextBox.Size = new System.Drawing.Size(250, 25);
            tenSanPhamTextBox.TabIndex = 3;

            // donViLabel
            donViLabel.AutoSize = true;
            donViLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            donViLabel.Location = new System.Drawing.Point(20, 100);
            donViLabel.Name = "donViLabel";
            donViLabel.Size = new System.Drawing.Size(55, 20);
            donViLabel.TabIndex = 4;
            donViLabel.Text = "Đơn vị";

            // donViComboBox
            donViComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            donViComboBox.Enabled = false;
            donViComboBox.Location = new System.Drawing.Point(150, 98);
            donViComboBox.Name = "donViComboBox";
            donViComboBox.Size = new System.Drawing.Size(250, 25);
            donViComboBox.TabIndex = 5;

            // maThuongHieuLabel
            maThuongHieuLabel.AutoSize = true;
            maThuongHieuLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            maThuongHieuLabel.Location = new System.Drawing.Point(20, 140);
            maThuongHieuLabel.Name = "maThuongHieuLabel";
            maThuongHieuLabel.Size = new System.Drawing.Size(115, 20);
            maThuongHieuLabel.TabIndex = 6;
            maThuongHieuLabel.Text = "Tên thương hiệu";

            // maThuongHieuTextBox
            maThuongHieuTextBox.Location = new System.Drawing.Point(150, 138);
            maThuongHieuTextBox.Name = "maThuongHieuTextBox";
            maThuongHieuTextBox.ReadOnly = true;
            maThuongHieuTextBox.Size = new System.Drawing.Size(250, 25);
            maThuongHieuTextBox.TabIndex = 7;

            // maLoaiLabel
            maLoaiLabel.AutoSize = true;
            maLoaiLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            maLoaiLabel.Location = new System.Drawing.Point(20, 180);
            maLoaiLabel.Name = "maLoaiLabel";
            maLoaiLabel.Size = new System.Drawing.Size(60, 20);
            maLoaiLabel.TabIndex = 10;
            maLoaiLabel.Text = "Tên loại";

            // maLoaiComboBox
            maLoaiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            maLoaiComboBox.Enabled = false;
            maLoaiComboBox.Location = new System.Drawing.Point(150, 178);
            maLoaiComboBox.Name = "maLoaiComboBox";
            maLoaiComboBox.Size = new System.Drawing.Size(250, 25);
            maLoaiComboBox.TabIndex = 9;

            // giaBanLabel
            giaBanLabel.AutoSize = true;
            giaBanLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            giaBanLabel.Location = new System.Drawing.Point(20, 220);
            giaBanLabel.Name = "giaBanLabel";
            giaBanLabel.Size = new System.Drawing.Size(60, 20);
            giaBanLabel.TabIndex = 12;
            giaBanLabel.Text = "Giá bán";

            // giaBanTextBox
            giaBanTextBox.Location = new System.Drawing.Point(150, 218);
            giaBanTextBox.Name = "giaBanTextBox";
            giaBanTextBox.ReadOnly = true;
            giaBanTextBox.Size = new System.Drawing.Size(250, 25);
            giaBanTextBox.TabIndex = 11;

            // xuatXuLabel
            xuatXuLabel.AutoSize = true;
            xuatXuLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            xuatXuLabel.Location = new System.Drawing.Point(20, 260);
            xuatXuLabel.Name = "xuatXuLabel";
            xuatXuLabel.Size = new System.Drawing.Size(60, 20);
            xuatXuLabel.TabIndex = 14;
            xuatXuLabel.Text = "Xuất xứ";

            // xuatXuTextBox
            xuatXuTextBox.Location = new System.Drawing.Point(150, 258);
            xuatXuTextBox.Name = "xuatXuTextBox";
            xuatXuTextBox.ReadOnly = true;
            xuatXuTextBox.Size = new System.Drawing.Size(250, 25);
            xuatXuTextBox.TabIndex = 13;

            // hsdLabel
            hsdLabel.AutoSize = true;
            hsdLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            hsdLabel.Location = new System.Drawing.Point(20, 300);
            hsdLabel.Name = "hsdLabel";
            hsdLabel.Size = new System.Drawing.Size(40, 20);
            hsdLabel.TabIndex = 16;
            hsdLabel.Text = "HSD";

            // hsdTextBox
            hsdTextBox.Location = new System.Drawing.Point(150, 298);
            hsdTextBox.Name = "hsdTextBox";
            hsdTextBox.ReadOnly = true;
            hsdTextBox.Size = new System.Drawing.Size(250, 25);
            hsdTextBox.TabIndex = 15;

                // nhaCungCapLabel
                nhaCungCapLabel.AutoSize = true;
                nhaCungCapLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                nhaCungCapLabel.Location = new System.Drawing.Point(20, 340);
                nhaCungCapLabel.Name = "nhaCungCapLabel";
                nhaCungCapLabel.Size = new System.Drawing.Size(100, 20);
                nhaCungCapLabel.TabIndex = 18;
                nhaCungCapLabel.Text = "Nhà cung cấp";

                // nhaCungCapComboBox
                nhaCungCapComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                nhaCungCapComboBox.Enabled = false;
                nhaCungCapComboBox.Location = new System.Drawing.Point(150, 338);
                nhaCungCapComboBox.Name = "nhaCungCapComboBox";
                nhaCungCapComboBox.Size = new System.Drawing.Size(250, 25);
                nhaCungCapComboBox.TabIndex = 17;

            // moTaLabel
            moTaLabel.AutoSize = true;
            moTaLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            moTaLabel.Location = new System.Drawing.Point(20, 380);
            moTaLabel.Name = "moTaLabel";
            moTaLabel.Size = new System.Drawing.Size(50, 20);
            moTaLabel.TabIndex = 20;
            moTaLabel.Text = "Mô tả";

            // moTaTextBox
            moTaTextBox.Location = new System.Drawing.Point(150, 378);
            moTaTextBox.Multiline = true;
            moTaTextBox.Name = "moTaTextBox";
            moTaTextBox.ReadOnly = true;
            moTaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            moTaTextBox.Size = new System.Drawing.Size(250, 100);
            moTaTextBox.TabIndex = 19;

            // hinhAnhLabel
            hinhAnhLabel.AutoSize = true;
            hinhAnhLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            hinhAnhLabel.Location = new System.Drawing.Point(450, 20);
            hinhAnhLabel.Name = "hinhAnhLabel";
            hinhAnhLabel.Size = new System.Drawing.Size(70, 20);
            hinhAnhLabel.TabIndex = 22;
            hinhAnhLabel.Text = "Hình ảnh";

            // productPictureBox
            productPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            productPictureBox.Location = new System.Drawing.Point(450, 50);
            productPictureBox.Name = "productPictureBox";
            productPictureBox.Size = new System.Drawing.Size(320, 380); // Reduced height from 420 to 380
            productPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            productPictureBox.TabIndex = 23;
            productPictureBox.TabStop = false;

            // closeButton
            closeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            closeButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            closeButton.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            closeButton.ForeColor = System.Drawing.Color.White;
            closeButton.Location = new System.Drawing.Point(670, 450); // Adjusted position to avoid overlap
            closeButton.Name = "closeButton";
            closeButton.Size = new System.Drawing.Size(100, 35);
            closeButton.TabIndex = 24;
            closeButton.Text = "Đóng";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += new System.EventHandler(closeButton_Click);

            // Form_SanPhamDialog
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.WhiteSmoke;
            ClientSize = new System.Drawing.Size(800, 600);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_SanPhamDialog";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Chi tiết sản phẩm";
            headerPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)productPictureBox).EndInit();
            ResumeLayout(false);
        }
    }
}