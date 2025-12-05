namespace mini_supermarket.GUI.Form_SanPham
{
    partial class Form_SanPhamUpdate
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox productPictureBox;
        private System.Windows.Forms.Button chonAnhButton;
        private System.Windows.Forms.TextBox hinhAnhTextBox;
        private System.Windows.Forms.Label hinhAnhLabel;
        private System.Windows.Forms.TextBox moTaTextBox;
        private System.Windows.Forms.Label moTaLabel;
        private System.Windows.Forms.ComboBox trangThaiComboBox;
        private System.Windows.Forms.Label trangThaiLabel;
        private System.Windows.Forms.DateTimePicker hsdDateTimePicker;
        private System.Windows.Forms.Label hsdLabel;
        private System.Windows.Forms.TextBox xuatXuTextBox;
        private System.Windows.Forms.Label xuatXuLabel;
        private System.Windows.Forms.TextBox giaBanTextBox;
        private System.Windows.Forms.Label giaBanLabel;
        private System.Windows.Forms.ComboBox thuongHieuComboBox;
        private System.Windows.Forms.Label thuongHieuLabel;
        private System.Windows.Forms.ComboBox loaiComboBox;
        private System.Windows.Forms.Label loaiLabel;
        private System.Windows.Forms.ComboBox donViComboBox;
        private System.Windows.Forms.Label donViLabel;
        private System.Windows.Forms.TextBox tenSanPhamTextBox;
        private System.Windows.Forms.Label tenSanPhamLabel;
        private System.Windows.Forms.TextBox maSanPhamTextBox;
        private System.Windows.Forms.Label maSanPhamLabel;

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
            cancelButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            productPictureBox = new System.Windows.Forms.PictureBox();
            chonAnhButton = new System.Windows.Forms.Button();
            hinhAnhTextBox = new System.Windows.Forms.TextBox();
            hinhAnhLabel = new System.Windows.Forms.Label();
            moTaTextBox = new System.Windows.Forms.TextBox();
            moTaLabel = new System.Windows.Forms.Label();
            trangThaiComboBox = new System.Windows.Forms.ComboBox();
            trangThaiLabel = new System.Windows.Forms.Label();
            hsdDateTimePicker = new System.Windows.Forms.DateTimePicker();
            hsdLabel = new System.Windows.Forms.Label();
            xuatXuTextBox = new System.Windows.Forms.TextBox();
            xuatXuLabel = new System.Windows.Forms.Label();
            giaBanTextBox = new System.Windows.Forms.TextBox();
            giaBanLabel = new System.Windows.Forms.Label();
            thuongHieuComboBox = new System.Windows.Forms.ComboBox();
            thuongHieuLabel = new System.Windows.Forms.Label();
            loaiComboBox = new System.Windows.Forms.ComboBox();
            loaiLabel = new System.Windows.Forms.Label();
            donViComboBox = new System.Windows.Forms.ComboBox();
            donViLabel = new System.Windows.Forms.Label();
            tenSanPhamTextBox = new System.Windows.Forms.TextBox();
            tenSanPhamLabel = new System.Windows.Forms.Label();
            maSanPhamTextBox = new System.Windows.Forms.TextBox();
            maSanPhamLabel = new System.Windows.Forms.Label();
            headerPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productPictureBox).BeginInit();
            SuspendLayout();

            // headerPanel
            headerPanel.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            headerPanel.Location = new System.Drawing.Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            headerPanel.Size = new System.Drawing.Size(820, 70);
            headerPanel.TabIndex = 0;
            headerPanel.Controls.Add(titleLabel);

            // titleLabel
            titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            titleLabel.ForeColor = System.Drawing.Color.White;
            titleLabel.Location = new System.Drawing.Point(20, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(780, 40);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Sửa sản phẩm";

            // contentPanel
            contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            contentPanel.Location = new System.Drawing.Point(0, 70);
            contentPanel.Name = "contentPanel";
            contentPanel.Padding = new System.Windows.Forms.Padding(20);
            contentPanel.Size = new System.Drawing.Size(820, 530);
            contentPanel.TabIndex = 1;
            contentPanel.Controls.Add(cancelButton);
            contentPanel.Controls.Add(saveButton);
            contentPanel.Controls.Add(productPictureBox);
            contentPanel.Controls.Add(chonAnhButton);
            contentPanel.Controls.Add(hinhAnhTextBox);
            contentPanel.Controls.Add(hinhAnhLabel);
            contentPanel.Controls.Add(moTaTextBox);
            contentPanel.Controls.Add(moTaLabel);
            contentPanel.Controls.Add(trangThaiComboBox);
            contentPanel.Controls.Add(trangThaiLabel);
            contentPanel.Controls.Add(hsdDateTimePicker);
            contentPanel.Controls.Add(hsdLabel);
            contentPanel.Controls.Add(xuatXuTextBox);
            contentPanel.Controls.Add(xuatXuLabel);
            contentPanel.Controls.Add(giaBanTextBox);
            contentPanel.Controls.Add(giaBanLabel);
            contentPanel.Controls.Add(thuongHieuComboBox);
            contentPanel.Controls.Add(thuongHieuLabel);
            contentPanel.Controls.Add(loaiComboBox);
            contentPanel.Controls.Add(loaiLabel);
            contentPanel.Controls.Add(donViComboBox);
            contentPanel.Controls.Add(donViLabel);
            contentPanel.Controls.Add(tenSanPhamTextBox);
            contentPanel.Controls.Add(tenSanPhamLabel);
            contentPanel.Controls.Add(maSanPhamTextBox);
            contentPanel.Controls.Add(maSanPhamLabel);

            // cancelButton
            cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            cancelButton.BackColor = System.Drawing.Color.LightGray;
            cancelButton.FlatAppearance.BorderSize = 0;
            cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cancelButton.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cancelButton.Location = new System.Drawing.Point(690, 470);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(100, 35);
            cancelButton.TabIndex = 22;
            cancelButton.Text = "Hủy";
            cancelButton.UseVisualStyleBackColor = false;

            // saveButton
            saveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            saveButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            saveButton.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            saveButton.ForeColor = System.Drawing.Color.White;
            saveButton.Location = new System.Drawing.Point(580, 470);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(100, 35);
            saveButton.TabIndex = 21;
            saveButton.Text = "Lưu";
            saveButton.UseVisualStyleBackColor = false;

            // productPictureBox
            productPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            productPictureBox.Location = new System.Drawing.Point(430, 140);
            productPictureBox.Name = "productPictureBox";
            productPictureBox.Size = new System.Drawing.Size(360, 260);
            productPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            productPictureBox.TabIndex = 20;
            productPictureBox.TabStop = false;

            // chonAnhButton
            chonAnhButton.Location = new System.Drawing.Point(710, 100);
            chonAnhButton.Name = "chonAnhButton";
            chonAnhButton.Size = new System.Drawing.Size(80, 27);
            chonAnhButton.TabIndex = 19;
            chonAnhButton.Text = "Chọn...";
            chonAnhButton.UseVisualStyleBackColor = true;

            // hinhAnhTextBox
            hinhAnhTextBox.Location = new System.Drawing.Point(430, 100);
            hinhAnhTextBox.Name = "hinhAnhTextBox";
            hinhAnhTextBox.ReadOnly = true;
            hinhAnhTextBox.Size = new System.Drawing.Size(270, 25);
            hinhAnhTextBox.TabIndex = 18;

            // hinhAnhLabel
            hinhAnhLabel.AutoSize = true;
            hinhAnhLabel.Location = new System.Drawing.Point(430, 75);
            hinhAnhLabel.Name = "hinhAnhLabel";
            hinhAnhLabel.Size = new System.Drawing.Size(65, 17);
            hinhAnhLabel.TabIndex = 17;
            hinhAnhLabel.Text = "Hình ảnh";

            // moTaTextBox
            moTaTextBox.Location = new System.Drawing.Point(150, 380);
            moTaTextBox.Multiline = true;
            moTaTextBox.Name = "moTaTextBox";
            moTaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            moTaTextBox.Size = new System.Drawing.Size(250, 80);
            moTaTextBox.TabIndex = 16;

            // moTaLabel
            moTaLabel.AutoSize = true;
            moTaLabel.Location = new System.Drawing.Point(20, 383);
            moTaLabel.Name = "moTaLabel";
            moTaLabel.Size = new System.Drawing.Size(38, 17);
            moTaLabel.TabIndex = 15;
            moTaLabel.Text = "Mô tả";

            // trangThaiComboBox
            trangThaiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            trangThaiComboBox.Enabled = false;
            trangThaiComboBox.Location = new System.Drawing.Point(150, 340);
            trangThaiComboBox.Name = "trangThaiComboBox";
            trangThaiComboBox.Size = new System.Drawing.Size(250, 25);
            trangThaiComboBox.TabIndex = 14;

            // trangThaiLabel
            trangThaiLabel.AutoSize = true;
            trangThaiLabel.Location = new System.Drawing.Point(20, 343);
            trangThaiLabel.Name = "trangThaiLabel";
            trangThaiLabel.Size = new System.Drawing.Size(67, 17);
            trangThaiLabel.TabIndex = 13;
            trangThaiLabel.Text = "Trạng thái";

            // hsdDateTimePicker
            hsdDateTimePicker.Location = new System.Drawing.Point(150, 300);
            hsdDateTimePicker.Name = "hsdDateTimePicker";
            hsdDateTimePicker.Size = new System.Drawing.Size(250, 25);
            hsdDateTimePicker.TabIndex = 12;

            // hsdLabel
            hsdLabel.AutoSize = true;
            hsdLabel.Location = new System.Drawing.Point(20, 303);
            hsdLabel.Name = "hsdLabel";
            hsdLabel.Size = new System.Drawing.Size(33, 17);
            hsdLabel.TabIndex = 11;
            hsdLabel.Text = "HSD";

            // xuatXuTextBox
            xuatXuTextBox.Location = new System.Drawing.Point(150, 260);
            xuatXuTextBox.Name = "xuatXuTextBox";
            xuatXuTextBox.Size = new System.Drawing.Size(250, 25);
            xuatXuTextBox.TabIndex = 10;

            // xuatXuLabel
            xuatXuLabel.AutoSize = true;
            xuatXuLabel.Location = new System.Drawing.Point(20, 263);
            xuatXuLabel.Name = "xuatXuLabel";
            xuatXuLabel.Size = new System.Drawing.Size(52, 17);
            xuatXuLabel.TabIndex = 9;
            xuatXuLabel.Text = "Xuất xứ";

            // giaBanTextBox
            giaBanTextBox.Location = new System.Drawing.Point(150, 220);
            giaBanTextBox.Name = "giaBanTextBox";
            giaBanTextBox.Size = new System.Drawing.Size(250, 25);
            giaBanTextBox.TabIndex = 8;

            // giaBanLabel
            giaBanLabel.AutoSize = true;
            giaBanLabel.Location = new System.Drawing.Point(20, 223);
            giaBanLabel.Name = "giaBanLabel";
            giaBanLabel.Size = new System.Drawing.Size(52, 17);
            giaBanLabel.TabIndex = 7;
            giaBanLabel.Text = "Giá bán";

            // thuongHieuComboBox
            thuongHieuComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            thuongHieuComboBox.Location = new System.Drawing.Point(150, 180);
            thuongHieuComboBox.Name = "thuongHieuComboBox";
            thuongHieuComboBox.Size = new System.Drawing.Size(250, 25);
            thuongHieuComboBox.TabIndex = 6;

            // thuongHieuLabel
            thuongHieuLabel.AutoSize = true;
            thuongHieuLabel.Location = new System.Drawing.Point(20, 183);
            thuongHieuLabel.Name = "thuongHieuLabel";
            thuongHieuLabel.Size = new System.Drawing.Size(83, 17);
            thuongHieuLabel.TabIndex = 5;
            thuongHieuLabel.Text = "Thương hiệu";

            // loaiComboBox
            loaiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            loaiComboBox.Location = new System.Drawing.Point(150, 140);
            loaiComboBox.Name = "loaiComboBox";
            loaiComboBox.Size = new System.Drawing.Size(250, 25);
            loaiComboBox.TabIndex = 4;

            // loaiLabel
            loaiLabel.AutoSize = true;
            loaiLabel.Location = new System.Drawing.Point(20, 143);
            loaiLabel.Name = "loaiLabel";
            loaiLabel.Size = new System.Drawing.Size(33, 17);
            loaiLabel.TabIndex = 3;
            loaiLabel.Text = "Loại";

            // donViComboBox
            donViComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            donViComboBox.Location = new System.Drawing.Point(150, 100);
            donViComboBox.Name = "donViComboBox";
            donViComboBox.Size = new System.Drawing.Size(250, 25);
            donViComboBox.TabIndex = 2;

            // donViLabel
            donViLabel.AutoSize = true;
            donViLabel.Location = new System.Drawing.Point(20, 103);
            donViLabel.Name = "donViLabel";
            donViLabel.Size = new System.Drawing.Size(46, 17);
            donViLabel.TabIndex = 1;
            donViLabel.Text = "Đơn vị";

            // tenSanPhamTextBox
            tenSanPhamTextBox.Location = new System.Drawing.Point(150, 60);
            tenSanPhamTextBox.Name = "tenSanPhamTextBox";
            tenSanPhamTextBox.Size = new System.Drawing.Size(250, 25);
            tenSanPhamTextBox.TabIndex = 0;

            // tenSanPhamLabel
            tenSanPhamLabel.AutoSize = true;
            tenSanPhamLabel.Location = new System.Drawing.Point(20, 63);
            tenSanPhamLabel.Name = "tenSanPhamLabel";
            tenSanPhamLabel.Size = new System.Drawing.Size(90, 17);
            tenSanPhamLabel.TabIndex = 0;
            tenSanPhamLabel.Text = "Tên sản phẩm";

            // maSanPhamTextBox
            maSanPhamTextBox.Location = new System.Drawing.Point(150, 20);
            maSanPhamTextBox.Name = "maSanPhamTextBox";
            maSanPhamTextBox.Size = new System.Drawing.Size(250, 25);
            maSanPhamTextBox.TabIndex = 23;
            maSanPhamTextBox.ReadOnly = true;

            // maSanPhamLabel
            maSanPhamLabel.AutoSize = true;
            maSanPhamLabel.Location = new System.Drawing.Point(20, 23);
            maSanPhamLabel.Name = "maSanPhamLabel";
            maSanPhamLabel.Size = new System.Drawing.Size(87, 17);
            maSanPhamLabel.TabIndex = 24;
            maSanPhamLabel.Text = "Mã sản phẩm";

            // Form_SanPhamUpdate
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(820, 600);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_SanPhamUpdate";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Sửa sản phẩm";
            headerPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)productPictureBox).EndInit();
            ResumeLayout(false);
        }
    }
}

