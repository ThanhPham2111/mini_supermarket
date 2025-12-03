namespace mini_supermarket.GUI.TaiKhoan
{
    partial class Form_TaiKhoan
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox taiKhoanInfoGroupBox;
        private System.Windows.Forms.Label maTaiKhoanLabel;
        private System.Windows.Forms.TextBox maTaiKhoanTextBox;
        private System.Windows.Forms.Label tenDangNhapLabel;
        private System.Windows.Forms.TextBox tenDangNhapTextBox;
        private System.Windows.Forms.Label matKhauLabel;
        private System.Windows.Forms.TextBox matKhauTextBox;
        private System.Windows.Forms.Label nhanVienLabel;
        private System.Windows.Forms.TextBox nhanVienTextBox;
        private System.Windows.Forms.Label quyenLabel;
        private System.Windows.Forms.TextBox quyenTextBox;
        private System.Windows.Forms.Button themButton;
        private System.Windows.Forms.Button suaButton;
        private System.Windows.Forms.Button xoaButton;
        private System.Windows.Forms.Button lamMoiButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.ComboBox statusFilterComboBox;
        private System.Windows.Forms.GroupBox danhSachGroupBox;
        private System.Windows.Forms.DataGridView taiKhoanDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn maTaiKhoanColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenDangNhapColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenNhanVienColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenQuyenColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThaiColumn;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            taiKhoanInfoGroupBox = new System.Windows.Forms.GroupBox();
            statusFilterComboBox = new System.Windows.Forms.ComboBox();
            searchTextBox = new System.Windows.Forms.TextBox();
            searchButton = new System.Windows.Forms.Button();
            lamMoiButton = new System.Windows.Forms.Button();
            xoaButton = new System.Windows.Forms.Button();
            suaButton = new System.Windows.Forms.Button();
            themButton = new System.Windows.Forms.Button();
            quyenTextBox = new System.Windows.Forms.TextBox();
            quyenLabel = new System.Windows.Forms.Label();
            nhanVienTextBox = new System.Windows.Forms.TextBox();
            nhanVienLabel = new System.Windows.Forms.Label();
            matKhauTextBox = new System.Windows.Forms.TextBox();
            matKhauLabel = new System.Windows.Forms.Label();
            tenDangNhapTextBox = new System.Windows.Forms.TextBox();
            tenDangNhapLabel = new System.Windows.Forms.Label();
            maTaiKhoanTextBox = new System.Windows.Forms.TextBox();
            maTaiKhoanLabel = new System.Windows.Forms.Label();
            danhSachGroupBox = new System.Windows.Forms.GroupBox();
            taiKhoanDataGridView = new System.Windows.Forms.DataGridView();
            maTaiKhoanColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            tenDangNhapColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            tenNhanVienColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            tenQuyenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            trangThaiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            taiKhoanInfoGroupBox.SuspendLayout();
            danhSachGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)taiKhoanDataGridView).BeginInit();
            SuspendLayout();

            // taiKhoanInfoGroupBox
            taiKhoanInfoGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            taiKhoanInfoGroupBox.Controls.Add(statusFilterComboBox);
            taiKhoanInfoGroupBox.Controls.Add(searchTextBox);
            taiKhoanInfoGroupBox.Controls.Add(searchButton);
            taiKhoanInfoGroupBox.Controls.Add(lamMoiButton);
            taiKhoanInfoGroupBox.Controls.Add(xoaButton);
            taiKhoanInfoGroupBox.Controls.Add(suaButton);
            taiKhoanInfoGroupBox.Controls.Add(themButton);
            taiKhoanInfoGroupBox.Controls.Add(quyenTextBox);
            taiKhoanInfoGroupBox.Controls.Add(quyenLabel);
            taiKhoanInfoGroupBox.Controls.Add(nhanVienTextBox);
            taiKhoanInfoGroupBox.Controls.Add(nhanVienLabel);
            taiKhoanInfoGroupBox.Controls.Add(matKhauTextBox);
            taiKhoanInfoGroupBox.Controls.Add(matKhauLabel);
            taiKhoanInfoGroupBox.Controls.Add(tenDangNhapTextBox);
            taiKhoanInfoGroupBox.Controls.Add(tenDangNhapLabel);
            taiKhoanInfoGroupBox.Controls.Add(maTaiKhoanTextBox);
            taiKhoanInfoGroupBox.Controls.Add(maTaiKhoanLabel);
            taiKhoanInfoGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            taiKhoanInfoGroupBox.Location = new System.Drawing.Point(20, 20);
            taiKhoanInfoGroupBox.Name = "taiKhoanInfoGroupBox";
            taiKhoanInfoGroupBox.Size = new System.Drawing.Size(1140, 240);
            taiKhoanInfoGroupBox.TabIndex = 0;
            taiKhoanInfoGroupBox.TabStop = false;
            taiKhoanInfoGroupBox.Text = "Thông tin tài khoản";
            taiKhoanInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // statusFilterComboBox
            statusFilterComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            statusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            statusFilterComboBox.FormattingEnabled = true;
            statusFilterComboBox.Location = new System.Drawing.Point(980, 210);
            statusFilterComboBox.Name = "statusFilterComboBox";
            statusFilterComboBox.Size = new System.Drawing.Size(150, 28);
            statusFilterComboBox.TabIndex = 21;

            // searchTextBox
            searchTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            searchTextBox.Location = new System.Drawing.Point(150, 210);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Tìm kiếm theo mã, tên đăng nhập hoặc tên nhân viên";
            searchTextBox.Size = new System.Drawing.Size(824, 28);
            searchTextBox.TabIndex = 20;

            // searchButton
            searchButton.Location = new System.Drawing.Point(20, 210);
            searchButton.Name = "searchButton";
            searchButton.Size = new System.Drawing.Size(120, 30);
            searchButton.TabIndex = 19;
            searchButton.Text = "Tìm kiếm";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            searchButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            searchButton.ForeColor = System.Drawing.Color.White;

            // lamMoiButton
            lamMoiButton.Location = new System.Drawing.Point(860, 170);
            lamMoiButton.Name = "lamMoiButton";
            lamMoiButton.Size = new System.Drawing.Size(120, 30);
            lamMoiButton.TabIndex = 18;
            lamMoiButton.Text = "Làm mới";
            lamMoiButton.UseVisualStyleBackColor = true;
            lamMoiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            lamMoiButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            lamMoiButton.ForeColor = System.Drawing.Color.White;

            // xoaButton
            xoaButton.Location = new System.Drawing.Point(720, 170);
            xoaButton.Name = "xoaButton";
            xoaButton.Size = new System.Drawing.Size(120, 30);
            xoaButton.TabIndex = 17;
            xoaButton.Text = "Xóa";
            xoaButton.UseVisualStyleBackColor = true;
            xoaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            xoaButton.BackColor = System.Drawing.Color.FromArgb(255, 77, 77);
            xoaButton.ForeColor = System.Drawing.Color.White;

            // suaButton
            suaButton.Location = new System.Drawing.Point(580, 170);
            suaButton.Name = "suaButton";
            suaButton.Size = new System.Drawing.Size(120, 30);
            suaButton.TabIndex = 16;
            suaButton.Text = "Sửa";
            suaButton.UseVisualStyleBackColor = true;
            suaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            suaButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            suaButton.ForeColor = System.Drawing.Color.White;

            // themButton
            themButton.Location = new System.Drawing.Point(440, 170);
            themButton.Name = "themButton";
            themButton.Size = new System.Drawing.Size(120, 30);
            themButton.TabIndex = 15;
            themButton.Text = "Thêm";
            themButton.UseVisualStyleBackColor = true;
            themButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            themButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            themButton.ForeColor = System.Drawing.Color.White;

            // quyenTextBox
            quyenTextBox.Location = new System.Drawing.Point(550, 126);
            quyenTextBox.Name = "quyenTextBox";
            quyenTextBox.ReadOnly = true;
            quyenTextBox.Size = new System.Drawing.Size(250, 28);
            quyenTextBox.TabIndex = 14;

            // quyenLabel
            quyenLabel.AutoSize = true;
            quyenLabel.Location = new System.Drawing.Point(480, 130);
            quyenLabel.Name = "quyenLabel";
            quyenLabel.Size = new System.Drawing.Size(48, 19);
            quyenLabel.TabIndex = 12;
            quyenLabel.Text = "Quyền";

            // nhanVienTextBox
            nhanVienTextBox.Location = new System.Drawing.Point(150, 126);
            nhanVienTextBox.Name = "nhanVienTextBox";
            nhanVienTextBox.ReadOnly = true;
            nhanVienTextBox.Size = new System.Drawing.Size(250, 28);
            nhanVienTextBox.TabIndex = 11;

            // nhanVienLabel
            nhanVienLabel.AutoSize = true;
            nhanVienLabel.Location = new System.Drawing.Point(20, 130);
            nhanVienLabel.Name = "nhanVienLabel";
            nhanVienLabel.Size = new System.Drawing.Size(82, 19);
            nhanVienLabel.TabIndex = 9;
            nhanVienLabel.Text = "Nhân viên";

            // matKhauTextBox
            matKhauTextBox.Location = new System.Drawing.Point(550, 86);
            matKhauTextBox.Name = "matKhauTextBox";
            // matKhauTextBox.PasswordChar = '*';
            matKhauTextBox.Size = new System.Drawing.Size(250, 28);
            matKhauTextBox.TabIndex = 8;

            // matKhauLabel
            matKhauLabel.AutoSize = true;
            matKhauLabel.Location = new System.Drawing.Point(460, 90);
            matKhauLabel.Name = "matKhauLabel";
            matKhauLabel.Size = new System.Drawing.Size(73, 19);
            matKhauLabel.TabIndex = 7;
            matKhauLabel.Text = "Mật khẩu";

            // tenDangNhapTextBox
            tenDangNhapTextBox.Location = new System.Drawing.Point(150, 86);
            tenDangNhapTextBox.Name = "tenDangNhapTextBox";
            tenDangNhapTextBox.Size = new System.Drawing.Size(250, 28);
            tenDangNhapTextBox.TabIndex = 6;

            // tenDangNhapLabel
            tenDangNhapLabel.AutoSize = true;
            tenDangNhapLabel.Location = new System.Drawing.Point(20, 90);
            tenDangNhapLabel.Name = "tenDangNhapLabel";
            tenDangNhapLabel.Size = new System.Drawing.Size(102, 19);
            tenDangNhapLabel.TabIndex = 5;
            tenDangNhapLabel.Text = "Tên đăng nhập";

            // maTaiKhoanTextBox
            maTaiKhoanTextBox.Location = new System.Drawing.Point(150, 46);
            maTaiKhoanTextBox.Name = "maTaiKhoanTextBox";
            maTaiKhoanTextBox.ReadOnly = true;
            maTaiKhoanTextBox.Size = new System.Drawing.Size(250, 28);
            maTaiKhoanTextBox.TabIndex = 4;

            // maTaiKhoanLabel
            maTaiKhoanLabel.AutoSize = true;
            maTaiKhoanLabel.Location = new System.Drawing.Point(20, 50);
            maTaiKhoanLabel.Name = "maTaiKhoanLabel";
            maTaiKhoanLabel.Size = new System.Drawing.Size(97, 19);
            maTaiKhoanLabel.TabIndex = 3;
            maTaiKhoanLabel.Text = "Mã tài khoản";

            // danhSachGroupBox
            danhSachGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            danhSachGroupBox.Controls.Add(taiKhoanDataGridView);
            danhSachGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            danhSachGroupBox.Location = new System.Drawing.Point(20, 270);
            danhSachGroupBox.Name = "danhSachGroupBox";
            danhSachGroupBox.Size = new System.Drawing.Size(1140, 470);
            danhSachGroupBox.TabIndex = 1;
            danhSachGroupBox.TabStop = false;
            danhSachGroupBox.Text = "Danh sách tài khoản";
            danhSachGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // taiKhoanDataGridView
            taiKhoanDataGridView.AllowUserToAddRows = false;
            taiKhoanDataGridView.AllowUserToDeleteRows = false;
            taiKhoanDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            taiKhoanDataGridView.BackgroundColor = System.Drawing.Color.White;
            taiKhoanDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            taiKhoanDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            taiKhoanDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                maTaiKhoanColumn,
                tenDangNhapColumn,
                tenNhanVienColumn,
                tenQuyenColumn,
                trangThaiColumn
            });
            taiKhoanDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            taiKhoanDataGridView.Location = new System.Drawing.Point(3, 19);
            taiKhoanDataGridView.MultiSelect = false;
            taiKhoanDataGridView.Name = "taiKhoanDataGridView";
            taiKhoanDataGridView.ReadOnly = true;
            taiKhoanDataGridView.RowHeadersVisible = false;
            taiKhoanDataGridView.RowTemplate.Height = 28;
            taiKhoanDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            taiKhoanDataGridView.Size = new System.Drawing.Size(1134, 448);
            taiKhoanDataGridView.TabIndex = 0;
            taiKhoanDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // maTaiKhoanColumn
            maTaiKhoanColumn.DataPropertyName = "MaTaiKhoan";
            maTaiKhoanColumn.HeaderText = "Mã TK";
            maTaiKhoanColumn.MinimumWidth = 100;
            maTaiKhoanColumn.Name = "maTaiKhoanColumn";
            maTaiKhoanColumn.ReadOnly = true;

            // tenDangNhapColumn
            tenDangNhapColumn.DataPropertyName = "TenDangNhap";
            tenDangNhapColumn.HeaderText = "Tên đăng nhập";
            tenDangNhapColumn.Name = "tenDangNhapColumn";
            tenDangNhapColumn.ReadOnly = true;

            // tenNhanVienColumn
            tenNhanVienColumn.HeaderText = "Nhân viên";
            tenNhanVienColumn.Name = "tenNhanVienColumn";
            tenNhanVienColumn.ReadOnly = true;

            // tenQuyenColumn
            tenQuyenColumn.HeaderText = "Quyền";
            tenQuyenColumn.Name = "tenQuyenColumn";
            tenQuyenColumn.ReadOnly = true;

            // trangThaiColumn
            trangThaiColumn.DataPropertyName = "TrangThai";
            trangThaiColumn.HeaderText = "Trạng thái";
            trangThaiColumn.Name = "trangThaiColumn";
            trangThaiColumn.ReadOnly = true;

            // Form_TaiKhoan
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            ClientSize = new System.Drawing.Size(1180, 760);
            Controls.Add(danhSachGroupBox);
            Controls.Add(taiKhoanInfoGroupBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "Form_TaiKhoan";
            Text = "Quản lý tài khoản";
            taiKhoanInfoGroupBox.ResumeLayout(false);
            taiKhoanInfoGroupBox.PerformLayout();
            danhSachGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)taiKhoanDataGridView).EndInit();
            ResumeLayout(false);
        }
    }
}

