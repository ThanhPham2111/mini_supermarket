namespace mini_supermarket.GUI.NhanVien
{
    partial class Form_NhanVien
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox employeeInfoGroupBox;
        private System.Windows.Forms.Label maNhanVienLabel;
        private System.Windows.Forms.TextBox maNhanVienTextBox;
        private System.Windows.Forms.Label hoTenLabel;
        private System.Windows.Forms.TextBox hoTenTextBox;
        private System.Windows.Forms.Label ngaySinhLabel;
        private System.Windows.Forms.DateTimePicker ngaySinhDateTimePicker;
        private System.Windows.Forms.Label gioiTinhLabel;
        private System.Windows.Forms.RadioButton gioiTinhNamRadioButton;
        private System.Windows.Forms.RadioButton gioiTinhNuRadioButton;
        private System.Windows.Forms.Label chucVuLabel;
        private System.Windows.Forms.ComboBox chucVuComboBox;
        private System.Windows.Forms.Label soDienThoaiLabel;
        private System.Windows.Forms.TextBox soDienThoaiTextBox;
        private System.Windows.Forms.Button themButton;
        private System.Windows.Forms.Button suaButton;
        private System.Windows.Forms.Button xoaButton;
        private System.Windows.Forms.Button lamMoiButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.ComboBox statusFilterComboBox;
        private System.Windows.Forms.GroupBox danhSachGroupBox;
        private System.Windows.Forms.DataGridView nhanVienDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn maNhanVienColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chucVuColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoTenColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaySinhColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gioiTinhColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDienThoaiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThaiColumn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private System.Windows.Forms.Button importExcelButton;
        private System.Windows.Forms.Button exportExcelButton;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            employeeInfoGroupBox = new System.Windows.Forms.GroupBox();
            statusFilterComboBox = new System.Windows.Forms.ComboBox();
            searchTextBox = new System.Windows.Forms.TextBox();
            searchButton = new System.Windows.Forms.Button();
            lamMoiButton = new System.Windows.Forms.Button();
            xoaButton = new System.Windows.Forms.Button();
            suaButton = new System.Windows.Forms.Button();
            themButton = new System.Windows.Forms.Button();
            importExcelButton = new System.Windows.Forms.Button();
            exportExcelButton = new System.Windows.Forms.Button();
            soDienThoaiTextBox = new System.Windows.Forms.TextBox();
            soDienThoaiLabel = new System.Windows.Forms.Label();
            chucVuComboBox = new System.Windows.Forms.ComboBox();
            chucVuLabel = new System.Windows.Forms.Label();
            gioiTinhNuRadioButton = new System.Windows.Forms.RadioButton();
            gioiTinhNamRadioButton = new System.Windows.Forms.RadioButton();
            gioiTinhLabel = new System.Windows.Forms.Label();
            ngaySinhDateTimePicker = new System.Windows.Forms.DateTimePicker();
            ngaySinhLabel = new System.Windows.Forms.Label();
            hoTenTextBox = new System.Windows.Forms.TextBox();
            hoTenLabel = new System.Windows.Forms.Label();
            maNhanVienTextBox = new System.Windows.Forms.TextBox();
            maNhanVienLabel = new System.Windows.Forms.Label();
            danhSachGroupBox = new System.Windows.Forms.GroupBox();
            nhanVienDataGridView = new System.Windows.Forms.DataGridView();
            maNhanVienColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            chucVuColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            hoTenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ngaySinhColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            gioiTinhColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            soDienThoaiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            trangThaiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            employeeInfoGroupBox.SuspendLayout();
            danhSachGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nhanVienDataGridView).BeginInit();
            SuspendLayout();

            // employeeInfoGroupBox
            employeeInfoGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            employeeInfoGroupBox.Controls.Add(statusFilterComboBox);
            employeeInfoGroupBox.Controls.Add(searchTextBox);
            employeeInfoGroupBox.Controls.Add(searchButton);
            employeeInfoGroupBox.Controls.Add(lamMoiButton);
            employeeInfoGroupBox.Controls.Add(xoaButton);
            employeeInfoGroupBox.Controls.Add(suaButton);
            employeeInfoGroupBox.Controls.Add(themButton);
            employeeInfoGroupBox.Controls.Add(soDienThoaiTextBox);
            employeeInfoGroupBox.Controls.Add(soDienThoaiLabel);
            employeeInfoGroupBox.Controls.Add(chucVuComboBox);
            employeeInfoGroupBox.Controls.Add(chucVuLabel);
            employeeInfoGroupBox.Controls.Add(gioiTinhNuRadioButton);
            employeeInfoGroupBox.Controls.Add(gioiTinhNamRadioButton);
            employeeInfoGroupBox.Controls.Add(gioiTinhLabel);
            employeeInfoGroupBox.Controls.Add(ngaySinhDateTimePicker);
            employeeInfoGroupBox.Controls.Add(ngaySinhLabel);
            employeeInfoGroupBox.Controls.Add(hoTenTextBox);
            employeeInfoGroupBox.Controls.Add(hoTenLabel);
            employeeInfoGroupBox.Controls.Add(maNhanVienTextBox);
            employeeInfoGroupBox.Controls.Add(maNhanVienLabel);
            employeeInfoGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            employeeInfoGroupBox.Location = new System.Drawing.Point(20, 50);
            employeeInfoGroupBox.Name = "employeeInfoGroupBox";
            employeeInfoGroupBox.Size = new System.Drawing.Size(1140, 240);
            employeeInfoGroupBox.TabIndex = 0;
            employeeInfoGroupBox.TabStop = false;
            employeeInfoGroupBox.Text = "Thông tin nhân viên";
            employeeInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Excel green
            var excelGreen = System.Drawing.Color.FromArgb(33, 115, 70);

            // exportExcelButton
            exportExcelButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            exportExcelButton.Location = new System.Drawing.Point(920, 15);
            exportExcelButton.Name = "exportExcelButton";
            exportExcelButton.Size = new System.Drawing.Size(110, 30);
            exportExcelButton.TabIndex = 20;
            exportExcelButton.Text = "Export Excel";
            exportExcelButton.UseVisualStyleBackColor = false;
            exportExcelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            exportExcelButton.BackColor = excelGreen;
            exportExcelButton.ForeColor = System.Drawing.Color.White;

            // importExcelButton
            importExcelButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            importExcelButton.Location = new System.Drawing.Point(1050, 15);
            importExcelButton.Name = "importExcelButton";
            importExcelButton.Size = new System.Drawing.Size(110, 30);
            importExcelButton.TabIndex = 21;
            importExcelButton.Text = "Import Excel";
            importExcelButton.UseVisualStyleBackColor = false;
            importExcelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            importExcelButton.BackColor = excelGreen;
            importExcelButton.ForeColor = System.Drawing.Color.White;

            // statusFilterComboBox
            statusFilterComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            statusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            statusFilterComboBox.FormattingEnabled = true;
            statusFilterComboBox.Location = new System.Drawing.Point(980, 200);
            statusFilterComboBox.Name = "statusFilterComboBox";
            statusFilterComboBox.Size = new System.Drawing.Size(150, 28);
            statusFilterComboBox.TabIndex = 19;

            // searchTextBox
            searchTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            searchTextBox.Location = new System.Drawing.Point(150, 200);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Tìm kiếm theo mã, tên hoặc số điện thoại";
            searchTextBox.Size = new System.Drawing.Size(824, 28);
            searchTextBox.TabIndex = 18;

            // searchButton
            searchButton.Location = new System.Drawing.Point(20, 200);
            searchButton.Name = "searchButton";
            searchButton.Size = new System.Drawing.Size(120, 30);
            searchButton.TabIndex = 17;
            searchButton.Text = "Tìm kiếm";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            searchButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            searchButton.ForeColor = System.Drawing.Color.White;

            // lamMoiButton
            lamMoiButton.Location = new System.Drawing.Point(860, 150);
            lamMoiButton.Name = "lamMoiButton";
            lamMoiButton.Size = new System.Drawing.Size(120, 30);
            lamMoiButton.TabIndex = 16;
            lamMoiButton.Text = "Làm mới";
            lamMoiButton.UseVisualStyleBackColor = true;
            lamMoiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            lamMoiButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            lamMoiButton.ForeColor = System.Drawing.Color.White;

            // xoaButton
            xoaButton.Location = new System.Drawing.Point(720, 150);
            xoaButton.Name = "xoaButton";
            xoaButton.Size = new System.Drawing.Size(120, 30);
            xoaButton.TabIndex = 15;
            xoaButton.Text = "Xóa";
            xoaButton.UseVisualStyleBackColor = true;
            xoaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            xoaButton.BackColor = System.Drawing.Color.FromArgb(255, 77, 77);
            xoaButton.ForeColor = System.Drawing.Color.White;

            // suaButton
            suaButton.Location = new System.Drawing.Point(580, 150);
            suaButton.Name = "suaButton";
            suaButton.Size = new System.Drawing.Size(120, 30);
            suaButton.TabIndex = 14;
            suaButton.Text = "Sửa";
            suaButton.UseVisualStyleBackColor = true;
            suaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            suaButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            suaButton.ForeColor = System.Drawing.Color.White;

            // themButton
            themButton.Location = new System.Drawing.Point(440, 150);
            themButton.Name = "themButton";
            themButton.Size = new System.Drawing.Size(120, 30);
            themButton.TabIndex = 13;
            themButton.Text = "Thêm";
            themButton.UseVisualStyleBackColor = true;
            themButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            themButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            themButton.ForeColor = System.Drawing.Color.White;

            // soDienThoaiTextBox
            soDienThoaiTextBox.Location = new System.Drawing.Point(650, 100);
            soDienThoaiTextBox.Name = "soDienThoaiTextBox";
            soDienThoaiTextBox.Size = new System.Drawing.Size(250, 28);
            soDienThoaiTextBox.TabIndex = 12;

            // soDienThoaiLabel
            soDienThoaiLabel.AutoSize = true;
            soDienThoaiLabel.Location = new System.Drawing.Point(550, 104);
            soDienThoaiLabel.Name = "soDienThoaiLabel";
            soDienThoaiLabel.Size = new System.Drawing.Size(83, 15);
            soDienThoaiLabel.TabIndex = 11;
            soDienThoaiLabel.Text = "Số điện thoại";

            // chucVuComboBox
            chucVuComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            chucVuComboBox.FormattingEnabled = true;
            chucVuComboBox.Location = new System.Drawing.Point(150, 100);
            chucVuComboBox.Name = "chucVuComboBox";
            chucVuComboBox.Size = new System.Drawing.Size(250, 28);
            chucVuComboBox.TabIndex = 10;

            // chucVuLabel
            chucVuLabel.AutoSize = true;
            chucVuLabel.Location = new System.Drawing.Point(20, 104);
            chucVuLabel.Name = "chucVuLabel";
            chucVuLabel.Size = new System.Drawing.Size(54, 15);
            chucVuLabel.TabIndex = 9;
            chucVuLabel.Text = "Chức vụ";

            // gioiTinhNuRadioButton
            gioiTinhNuRadioButton.AutoSize = true;
            gioiTinhNuRadioButton.Location = new System.Drawing.Point(750, 60);
            gioiTinhNuRadioButton.Name = "gioiTinhNuRadioButton";
            gioiTinhNuRadioButton.Size = new System.Drawing.Size(41, 19);
            gioiTinhNuRadioButton.TabIndex = 8;
            gioiTinhNuRadioButton.TabStop = true;
            gioiTinhNuRadioButton.Text = "Nữ";
            gioiTinhNuRadioButton.UseVisualStyleBackColor = true;

            // gioiTinhNamRadioButton
            gioiTinhNamRadioButton.AutoSize = true;
            gioiTinhNamRadioButton.Location = new System.Drawing.Point(650, 60);
            gioiTinhNamRadioButton.Name = "gioiTinhNamRadioButton";
            gioiTinhNamRadioButton.Size = new System.Drawing.Size(53, 19);
            gioiTinhNamRadioButton.TabIndex = 7;
            gioiTinhNamRadioButton.TabStop = true;
            gioiTinhNamRadioButton.Text = "Nam";
            gioiTinhNamRadioButton.UseVisualStyleBackColor = true;

            // gioiTinhLabel
            gioiTinhLabel.AutoSize = true;
            gioiTinhLabel.Location = new System.Drawing.Point(550, 64);
            gioiTinhLabel.Name = "gioiTinhLabel";
            gioiTinhLabel.Size = new System.Drawing.Size(53, 15);
            gioiTinhLabel.TabIndex = 6;
            gioiTinhLabel.Text = "Giới tính";

            // ngaySinhDateTimePicker
            ngaySinhDateTimePicker.CustomFormat = "dd/MM/yyyy";
            ngaySinhDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            ngaySinhDateTimePicker.Location = new System.Drawing.Point(150, 60);
            ngaySinhDateTimePicker.Name = "ngaySinhDateTimePicker";
            ngaySinhDateTimePicker.Size = new System.Drawing.Size(250, 28);
            ngaySinhDateTimePicker.TabIndex = 5;

            // ngaySinhLabel
            ngaySinhLabel.AutoSize = true;
            ngaySinhLabel.Location = new System.Drawing.Point(20, 64);
            ngaySinhLabel.Name = "ngaySinhLabel";
            ngaySinhLabel.Size = new System.Drawing.Size(65, 15);
            ngaySinhLabel.TabIndex = 4;
            ngaySinhLabel.Text = "Ngày sinh";

            // hoTenTextBox
            hoTenTextBox.Location = new System.Drawing.Point(650, 20);
            hoTenTextBox.Name = "hoTenTextBox";
            hoTenTextBox.Size = new System.Drawing.Size(250, 28);
            hoTenTextBox.TabIndex = 3;

            // hoTenLabel
            hoTenLabel.AutoSize = true;
            hoTenLabel.Location = new System.Drawing.Point(550, 24);
            hoTenLabel.Name = "hoTenLabel";
            hoTenLabel.Size = new System.Drawing.Size(46, 15);
            hoTenLabel.TabIndex = 2;
            hoTenLabel.Text = "Họ tên";

            // maNhanVienTextBox
            maNhanVienTextBox.Location = new System.Drawing.Point(150, 20);
            maNhanVienTextBox.Name = "maNhanVienTextBox";
            maNhanVienTextBox.ReadOnly = true;
            maNhanVienTextBox.Size = new System.Drawing.Size(250, 28);
            maNhanVienTextBox.TabIndex = 1;

            // maNhanVienLabel
            maNhanVienLabel.AutoSize = true;
            maNhanVienLabel.Location = new System.Drawing.Point(20, 24);
            maNhanVienLabel.Name = "maNhanVienLabel";
            maNhanVienLabel.Size = new System.Drawing.Size(46, 15);
            maNhanVienLabel.TabIndex = 0;
            maNhanVienLabel.Text = "Mã NV";

            // danhSachGroupBox
            danhSachGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            danhSachGroupBox.Controls.Add(nhanVienDataGridView);
            danhSachGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            danhSachGroupBox.Location = new System.Drawing.Point(20, 300);
            danhSachGroupBox.Name = "danhSachGroupBox";
            danhSachGroupBox.Size = new System.Drawing.Size(1140, 440);
            danhSachGroupBox.TabIndex = 1;
            danhSachGroupBox.TabStop = false;
            danhSachGroupBox.Text = "Danh sách nhân viên";
            danhSachGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // nhanVienDataGridView
            nhanVienDataGridView.AllowUserToAddRows = false;
            nhanVienDataGridView.AllowUserToDeleteRows = false;
            nhanVienDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            nhanVienDataGridView.BackgroundColor = System.Drawing.Color.White;
            nhanVienDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nhanVienDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            nhanVienDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                maNhanVienColumn,
                chucVuColumn,
                hoTenColumn,
                ngaySinhColumn,
                gioiTinhColumn,
                soDienThoaiColumn,
                trangThaiColumn
            });
            nhanVienDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            nhanVienDataGridView.Location = new System.Drawing.Point(3, 19);
            nhanVienDataGridView.MultiSelect = false;
            nhanVienDataGridView.Name = "nhanVienDataGridView";
            nhanVienDataGridView.ReadOnly = true;
            nhanVienDataGridView.RowHeadersVisible = false;
            nhanVienDataGridView.RowTemplate.Height = 30;
            nhanVienDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            nhanVienDataGridView.Size = new System.Drawing.Size(1134, 418);
            nhanVienDataGridView.TabIndex = 0;
            nhanVienDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // maNhanVienColumn
            maNhanVienColumn.DataPropertyName = "MaNhanVien";
            maNhanVienColumn.HeaderText = "Mã NV";
            maNhanVienColumn.MinimumWidth = 100;
            maNhanVienColumn.Name = "maNhanVienColumn";
            maNhanVienColumn.ReadOnly = true;

            // chucVuColumn
            chucVuColumn.DataPropertyName = "VaiTro";
            chucVuColumn.HeaderText = "Chức vụ";
            chucVuColumn.Name = "chucVuColumn";
            chucVuColumn.ReadOnly = true;

            // hoTenColumn
            hoTenColumn.DataPropertyName = "TenNhanVien";
            hoTenColumn.HeaderText = "Họ tên";
            hoTenColumn.Name = "hoTenColumn";
            hoTenColumn.ReadOnly = true;

            // ngaySinhColumn
            ngaySinhColumn.DataPropertyName = "NgaySinh";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            dataGridViewCellStyle1.NullValue = null;
            ngaySinhColumn.DefaultCellStyle = dataGridViewCellStyle1;
            ngaySinhColumn.HeaderText = "Ngày sinh";
            ngaySinhColumn.Name = "ngaySinhColumn";
            ngaySinhColumn.ReadOnly = true;

            // gioiTinhColumn
            gioiTinhColumn.DataPropertyName = "GioiTinh";
            gioiTinhColumn.HeaderText = "Giới tính";
            gioiTinhColumn.Name = "gioiTinhColumn";
            gioiTinhColumn.ReadOnly = true;

            // soDienThoaiColumn
            soDienThoaiColumn.DataPropertyName = "SoDienThoai";
            soDienThoaiColumn.HeaderText = "SĐT";
            soDienThoaiColumn.Name = "soDienThoaiColumn";
            soDienThoaiColumn.ReadOnly = true;

            // trangThaiColumn
            trangThaiColumn.DataPropertyName = "TrangThai";
            trangThaiColumn.HeaderText = "Trạng thái";
            trangThaiColumn.Name = "trangThaiColumn";
            trangThaiColumn.ReadOnly = true;

            // Form_NhanVien
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            ClientSize = new System.Drawing.Size(1180, 760);
            Controls.Add(exportExcelButton);
            Controls.Add(importExcelButton);
            Controls.Add(danhSachGroupBox);
            Controls.Add(employeeInfoGroupBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "Form_NhanVien";
            Text = "Quản lý nhân viên";
            employeeInfoGroupBox.ResumeLayout(false);
            employeeInfoGroupBox.PerformLayout();
            danhSachGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nhanVienDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}