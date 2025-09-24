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
        private System.Windows.Forms.Label roleLabel;

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
            employeeInfoGroupBox = new System.Windows.Forms.GroupBox();
            statusFilterComboBox = new System.Windows.Forms.ComboBox();
            searchTextBox = new System.Windows.Forms.TextBox();
            searchButton = new System.Windows.Forms.Button();
            lamMoiButton = new System.Windows.Forms.Button();
            xoaButton = new System.Windows.Forms.Button();
            suaButton = new System.Windows.Forms.Button();
            themButton = new System.Windows.Forms.Button();
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
            roleLabel = new System.Windows.Forms.Label();
            employeeInfoGroupBox.SuspendLayout();
            danhSachGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nhanVienDataGridView).BeginInit();
            SuspendLayout();
            // 
            // employeeInfoGroupBox
            // 
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
            employeeInfoGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            employeeInfoGroupBox.Location = new System.Drawing.Point(16, 48);
            employeeInfoGroupBox.Name = "employeeInfoGroupBox";
            employeeInfoGroupBox.Size = new System.Drawing.Size(1148, 232);
            employeeInfoGroupBox.TabIndex = 0;
            employeeInfoGroupBox.TabStop = false;
            employeeInfoGroupBox.Text = "Thông tin nhân viên";
            // 
            // statusFilterComboBox
            // 
            statusFilterComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            statusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            statusFilterComboBox.FormattingEnabled = true;
            statusFilterComboBox.Location = new System.Drawing.Point(987, 210);
            statusFilterComboBox.Name = "statusFilterComboBox";
            statusFilterComboBox.Size = new System.Drawing.Size(140, 23);
            statusFilterComboBox.TabIndex = 19;
            // 
            // searchTextBox
            // 
            searchTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            searchTextBox.Location = new System.Drawing.Point(120, 210);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Tìm kiếm nhân viên theo tên, mã,...";
            searchTextBox.Size = new System.Drawing.Size(861, 23);
            searchTextBox.TabIndex = 18;
            // 
            // searchButton
            // 
            searchButton.Location = new System.Drawing.Point(20, 208);
            searchButton.Name = "searchButton";
            searchButton.Size = new System.Drawing.Size(90, 28);
            searchButton.TabIndex = 17;
            searchButton.Text = "Tìm kiếm";
            searchButton.UseVisualStyleBackColor = true;
            // 
            // lamMoiButton
            // 
            lamMoiButton.Enabled = false;
            lamMoiButton.Location = new System.Drawing.Point(438, 168);
            lamMoiButton.Name = "lamMoiButton";
            lamMoiButton.Size = new System.Drawing.Size(90, 32);
            lamMoiButton.TabIndex = 16;
            lamMoiButton.Text = "Làm mới";
            lamMoiButton.UseVisualStyleBackColor = true;
            // 
            // xoaButton
            // 
            xoaButton.Enabled = false;
            xoaButton.Location = new System.Drawing.Point(332, 168);
            xoaButton.Name = "xoaButton";
            xoaButton.Size = new System.Drawing.Size(90, 32);
            xoaButton.TabIndex = 15;
            xoaButton.Text = "Xóa";
            xoaButton.UseVisualStyleBackColor = true;
            // 
            // suaButton
            // 
            suaButton.Enabled = false;
            suaButton.Location = new System.Drawing.Point(226, 168);
            suaButton.Name = "suaButton";
            suaButton.Size = new System.Drawing.Size(90, 32);
            suaButton.TabIndex = 14;
            suaButton.Text = "Sửa";
            suaButton.UseVisualStyleBackColor = true;
            // 
            // themButton
            // 
            themButton.Enabled = false;
            themButton.Location = new System.Drawing.Point(120, 168);
            themButton.Name = "themButton";
            themButton.Size = new System.Drawing.Size(90, 32);
            themButton.TabIndex = 13;
            themButton.Text = "Thêm";
            themButton.UseVisualStyleBackColor = true;
            // 
            // soDienThoaiTextBox
            // 
            soDienThoaiTextBox.Location = new System.Drawing.Point(480, 120);
            soDienThoaiTextBox.Name = "soDienThoaiTextBox";
            soDienThoaiTextBox.Size = new System.Drawing.Size(280, 23);
            soDienThoaiTextBox.TabIndex = 12;
            // 
            // soDienThoaiLabel
            // 
            soDienThoaiLabel.AutoSize = true;
            soDienThoaiLabel.Location = new System.Drawing.Point(360, 124);
            soDienThoaiLabel.Name = "soDienThoaiLabel";
            soDienThoaiLabel.Size = new System.Drawing.Size(83, 15);
            soDienThoaiLabel.TabIndex = 11;
            soDienThoaiLabel.Text = "Số điện thoại";
            // 
            // chucVuComboBox
            // 
            chucVuComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            chucVuComboBox.FormattingEnabled = true;
            chucVuComboBox.Location = new System.Drawing.Point(120, 120);
            chucVuComboBox.Name = "chucVuComboBox";
            chucVuComboBox.Size = new System.Drawing.Size(200, 23);
            chucVuComboBox.TabIndex = 10;
            // 
            // chucVuLabel
            // 
            chucVuLabel.AutoSize = true;
            chucVuLabel.Location = new System.Drawing.Point(20, 124);
            chucVuLabel.Name = "chucVuLabel";
            chucVuLabel.Size = new System.Drawing.Size(54, 15);
            chucVuLabel.TabIndex = 9;
            chucVuLabel.Text = "Chức vụ";
            // 
            // gioiTinhNuRadioButton
            // 
            gioiTinhNuRadioButton.AutoSize = true;
            gioiTinhNuRadioButton.Location = new System.Drawing.Point(520, 78);
            gioiTinhNuRadioButton.Name = "gioiTinhNuRadioButton";
            gioiTinhNuRadioButton.Size = new System.Drawing.Size(41, 19);
            gioiTinhNuRadioButton.TabIndex = 8;
            gioiTinhNuRadioButton.TabStop = true;
            gioiTinhNuRadioButton.Text = "Nữ";
            gioiTinhNuRadioButton.UseVisualStyleBackColor = true;
            // 
            // gioiTinhNamRadioButton
            // 
            gioiTinhNamRadioButton.AutoSize = true;
            gioiTinhNamRadioButton.Location = new System.Drawing.Point(440, 78);
            gioiTinhNamRadioButton.Name = "gioiTinhNamRadioButton";
            gioiTinhNamRadioButton.Size = new System.Drawing.Size(53, 19);
            gioiTinhNamRadioButton.TabIndex = 7;
            gioiTinhNamRadioButton.TabStop = true;
            gioiTinhNamRadioButton.Text = "Nam";
            gioiTinhNamRadioButton.UseVisualStyleBackColor = true;
            // 
            // gioiTinhLabel
            // 
            gioiTinhLabel.AutoSize = true;
            gioiTinhLabel.Location = new System.Drawing.Point(360, 80);
            gioiTinhLabel.Name = "gioiTinhLabel";
            gioiTinhLabel.Size = new System.Drawing.Size(53, 15);
            gioiTinhLabel.TabIndex = 6;
            gioiTinhLabel.Text = "Giới tính";
            // 
            // ngaySinhDateTimePicker
            // 
            ngaySinhDateTimePicker.CustomFormat = "dd/MM/yyyy";
            ngaySinhDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            ngaySinhDateTimePicker.Location = new System.Drawing.Point(120, 76);
            ngaySinhDateTimePicker.Name = "ngaySinhDateTimePicker";
            ngaySinhDateTimePicker.Size = new System.Drawing.Size(200, 23);
            ngaySinhDateTimePicker.TabIndex = 5;
            // 
            // ngaySinhLabel
            // 
            ngaySinhLabel.AutoSize = true;
            ngaySinhLabel.Location = new System.Drawing.Point(20, 80);
            ngaySinhLabel.Name = "ngaySinhLabel";
            ngaySinhLabel.Size = new System.Drawing.Size(65, 15);
            ngaySinhLabel.TabIndex = 4;
            ngaySinhLabel.Text = "Ngày sinh";
            // 
            // hoTenTextBox
            // 
            hoTenTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            hoTenTextBox.Location = new System.Drawing.Point(440, 32);
            hoTenTextBox.Name = "hoTenTextBox";
            hoTenTextBox.Size = new System.Drawing.Size(320, 23);
            hoTenTextBox.TabIndex = 3;
            // 
            // hoTenLabel
            // 
            hoTenLabel.AutoSize = true;
            hoTenLabel.Location = new System.Drawing.Point(360, 36);
            hoTenLabel.Name = "hoTenLabel";
            hoTenLabel.Size = new System.Drawing.Size(46, 15);
            hoTenLabel.TabIndex = 2;
            hoTenLabel.Text = "Họ tên";
            // 
            // maNhanVienTextBox
            // 
            maNhanVienTextBox.Location = new System.Drawing.Point(120, 32);
            maNhanVienTextBox.Name = "maNhanVienTextBox";
            maNhanVienTextBox.ReadOnly = true;
            maNhanVienTextBox.Size = new System.Drawing.Size(200, 23);
            maNhanVienTextBox.TabIndex = 1;
            // 
            // maNhanVienLabel
            // 
            maNhanVienLabel.AutoSize = true;
            maNhanVienLabel.Location = new System.Drawing.Point(20, 36);
            maNhanVienLabel.Name = "maNhanVienLabel";
            maNhanVienLabel.Size = new System.Drawing.Size(46, 15);
            maNhanVienLabel.TabIndex = 0;
            maNhanVienLabel.Text = "Mã NV";
            // 
            // danhSachGroupBox
            // 
            danhSachGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            danhSachGroupBox.Controls.Add(nhanVienDataGridView);
            danhSachGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            danhSachGroupBox.Location = new System.Drawing.Point(16, 296);
            danhSachGroupBox.Name = "danhSachGroupBox";
            danhSachGroupBox.Size = new System.Drawing.Size(1148, 440);
            danhSachGroupBox.TabIndex = 1;
            danhSachGroupBox.TabStop = false;
            danhSachGroupBox.Text = "Danh sách nhân viên";
            // 
            // nhanVienDataGridView
            // 
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
            nhanVienDataGridView.RowTemplate.Height = 28;
            nhanVienDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            nhanVienDataGridView.Size = new System.Drawing.Size(1142, 418);
            nhanVienDataGridView.TabIndex = 0;
            // 
            // maNhanVienColumn
            // 
            maNhanVienColumn.DataPropertyName = "MaNhanVien";
            maNhanVienColumn.HeaderText = "Mã NV";
            maNhanVienColumn.MinimumWidth = 80;
            maNhanVienColumn.Name = "maNhanVienColumn";
            maNhanVienColumn.ReadOnly = true;
            // 
            // chucVuColumn
            // 
            chucVuColumn.DataPropertyName = "VaiTro";
            chucVuColumn.HeaderText = "Chức vụ";
            chucVuColumn.Name = "chucVuColumn";
            chucVuColumn.ReadOnly = true;
            // 
            // hoTenColumn
            // 
            hoTenColumn.DataPropertyName = "TenNhanVien";
            hoTenColumn.HeaderText = "Họ tên";
            hoTenColumn.Name = "hoTenColumn";
            hoTenColumn.ReadOnly = true;
            // 
            // ngaySinhColumn
            // 
            ngaySinhColumn.DataPropertyName = "NgaySinh";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            dataGridViewCellStyle1.NullValue = null;
            ngaySinhColumn.DefaultCellStyle = dataGridViewCellStyle1;
            ngaySinhColumn.HeaderText = "Ngày sinh";
            ngaySinhColumn.Name = "ngaySinhColumn";
            ngaySinhColumn.ReadOnly = true;
            // 
            // gioiTinhColumn
            // 
            gioiTinhColumn.DataPropertyName = "GioiTinh";
            gioiTinhColumn.HeaderText = "Giới tính";
            gioiTinhColumn.Name = "gioiTinhColumn";
            gioiTinhColumn.ReadOnly = true;
            // 
            // soDienThoaiColumn
            // 
            soDienThoaiColumn.DataPropertyName = "SoDienThoai";
            soDienThoaiColumn.HeaderText = "SĐT";
            soDienThoaiColumn.Name = "soDienThoaiColumn";
            soDienThoaiColumn.ReadOnly = true;
            // 
            // trangThaiColumn
            // 
            trangThaiColumn.DataPropertyName = "TrangThai";
            trangThaiColumn.HeaderText = "Trạng thái";
            trangThaiColumn.Name = "trangThaiColumn";
            trangThaiColumn.ReadOnly = true;
            // 
            // roleLabel
            // 
            roleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            roleLabel.AutoSize = true;
            roleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            roleLabel.Location = new System.Drawing.Point(1030, 20);
            roleLabel.Name = "roleLabel";
            roleLabel.Size = new System.Drawing.Size(87, 15);
            roleLabel.TabIndex = 2;
            roleLabel.Text = "Quyền: admin";
            // 
            // Form_NhanVien
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1180, 760);
            Controls.Add(roleLabel);
            Controls.Add(danhSachGroupBox);
            Controls.Add(employeeInfoGroupBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "Form_NhanVien";
            Text = "Nhân Viên";
            employeeInfoGroupBox.ResumeLayout(false);
            employeeInfoGroupBox.PerformLayout();
            danhSachGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nhanVienDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}


