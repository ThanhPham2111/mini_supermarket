namespace mini_supermarket.GUI.KhachHang
{
    partial class Form_KhachHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox khachHangInfoGroupBox;
        private System.Windows.Forms.Label maKhachHangLabel;
        private System.Windows.Forms.TextBox maKhachHangTextBox;
        private System.Windows.Forms.Label hoTenLabel;
        private System.Windows.Forms.TextBox hoTenTextBox;
        private System.Windows.Forms.Label soDienThoaiLabel;
        private System.Windows.Forms.TextBox soDienThoaiTextBox;
        private System.Windows.Forms.Label diaChiLabel;
        private System.Windows.Forms.TextBox diaChiTextBox;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label diemTichLuyLabel;
        private System.Windows.Forms.TextBox diemTichLuyTextBox;
        private System.Windows.Forms.Button themButton;
        private System.Windows.Forms.Button suaButton;
        private System.Windows.Forms.Button xoaButton;
        private System.Windows.Forms.Button lamMoiButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.ComboBox statusFilterComboBox;
        private System.Windows.Forms.GroupBox danhSachGroupBox;
        private System.Windows.Forms.DataGridView khachHangDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn maKhachHangColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoTenColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDienThoaiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaChiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemTichLuyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThaiColumn;
        private System.Windows.Forms.Button importExcelButton;
        private System.Windows.Forms.Button exportExcelButton;

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
            khachHangInfoGroupBox = new System.Windows.Forms.GroupBox();
            statusFilterComboBox = new System.Windows.Forms.ComboBox();
            searchTextBox = new System.Windows.Forms.TextBox();
            searchButton = new System.Windows.Forms.Button();
            importExcelButton = new System.Windows.Forms.Button();
            exportExcelButton = new System.Windows.Forms.Button();
            lamMoiButton = new System.Windows.Forms.Button();
            xoaButton = new System.Windows.Forms.Button();
            suaButton = new System.Windows.Forms.Button();
            themButton = new System.Windows.Forms.Button();
            diemTichLuyTextBox = new System.Windows.Forms.TextBox();
            diemTichLuyLabel = new System.Windows.Forms.Label();
            emailTextBox = new System.Windows.Forms.TextBox();
            emailLabel = new System.Windows.Forms.Label();
            diaChiTextBox = new System.Windows.Forms.TextBox();
            diaChiLabel = new System.Windows.Forms.Label();
            soDienThoaiTextBox = new System.Windows.Forms.TextBox();
            soDienThoaiLabel = new System.Windows.Forms.Label();
            hoTenTextBox = new System.Windows.Forms.TextBox();
            hoTenLabel = new System.Windows.Forms.Label();
            maKhachHangTextBox = new System.Windows.Forms.TextBox();
            maKhachHangLabel = new System.Windows.Forms.Label();
            danhSachGroupBox = new System.Windows.Forms.GroupBox();
            khachHangDataGridView = new System.Windows.Forms.DataGridView();
            maKhachHangColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            hoTenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            soDienThoaiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            diaChiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            emailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            diemTichLuyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            trangThaiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // Excel orange
            var excelOrange = System.Drawing.Color.FromArgb(253, 126, 20);

            // exportExcelButton
            exportExcelButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            exportExcelButton.Location = new System.Drawing.Point(920, 15);
            exportExcelButton.Name = "exportExcelButton";
            exportExcelButton.Size = new System.Drawing.Size(110, 30);
            exportExcelButton.TabIndex = 20;
            exportExcelButton.Text = "Export Excel";
            exportExcelButton.UseVisualStyleBackColor = false;
            exportExcelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            exportExcelButton.BackColor = excelOrange;
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
            importExcelButton.BackColor = System.Drawing.Color.FromArgb(33, 115, 70);
            importExcelButton.ForeColor = System.Drawing.Color.White;

            khachHangInfoGroupBox.SuspendLayout();
            danhSachGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)khachHangDataGridView).BeginInit();
            SuspendLayout();

            // khachHangInfoGroupBox
            khachHangInfoGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            khachHangInfoGroupBox.Controls.Add(statusFilterComboBox);
            khachHangInfoGroupBox.Controls.Add(searchTextBox);
            khachHangInfoGroupBox.Controls.Add(searchButton);
            khachHangInfoGroupBox.Controls.Add(lamMoiButton);
            khachHangInfoGroupBox.Controls.Add(xoaButton);
            khachHangInfoGroupBox.Controls.Add(suaButton);
            khachHangInfoGroupBox.Controls.Add(themButton);
            khachHangInfoGroupBox.Controls.Add(diemTichLuyTextBox);
            khachHangInfoGroupBox.Controls.Add(diemTichLuyLabel);
            khachHangInfoGroupBox.Controls.Add(emailTextBox);
            khachHangInfoGroupBox.Controls.Add(emailLabel);
            khachHangInfoGroupBox.Controls.Add(diaChiTextBox);
            khachHangInfoGroupBox.Controls.Add(diaChiLabel);
            khachHangInfoGroupBox.Controls.Add(soDienThoaiTextBox);
            khachHangInfoGroupBox.Controls.Add(soDienThoaiLabel);
            khachHangInfoGroupBox.Controls.Add(hoTenTextBox);
            khachHangInfoGroupBox.Controls.Add(hoTenLabel);
            khachHangInfoGroupBox.Controls.Add(maKhachHangTextBox);
            khachHangInfoGroupBox.Controls.Add(maKhachHangLabel);
            khachHangInfoGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            khachHangInfoGroupBox.Location = new System.Drawing.Point(20, 50);
            khachHangInfoGroupBox.Name = "khachHangInfoGroupBox";
            khachHangInfoGroupBox.Size = new System.Drawing.Size(1140, 240);
            khachHangInfoGroupBox.TabIndex = 0;
            khachHangInfoGroupBox.TabStop = false;
            khachHangInfoGroupBox.Text = "Thông tin khách hàng";
            khachHangInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            Controls.Add(exportExcelButton);
            Controls.Add(importExcelButton);

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
            xoaButton.Text = "Khóa";
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
            themButton.Text = "➕ Thêm";
            themButton.UseVisualStyleBackColor = true;
            themButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            themButton.BackColor = System.Drawing.Color.FromArgb(16, 137, 62);
            themButton.ForeColor = System.Drawing.Color.White;

            // diemTichLuyTextBox
            diemTichLuyTextBox.Location = new System.Drawing.Point(650, 110);
            diemTichLuyTextBox.Name = "diemTichLuyTextBox";
            diemTichLuyTextBox.ReadOnly = true;
            diemTichLuyTextBox.Size = new System.Drawing.Size(250, 28);
            diemTichLuyTextBox.TabIndex = 12;

            // diemTichLuyLabel
            diemTichLuyLabel.AutoSize = true;
            diemTichLuyLabel.Location = new System.Drawing.Point(550, 114);
            diemTichLuyLabel.Name = "diemTichLuyLabel";
            diemTichLuyLabel.Size = new System.Drawing.Size(84, 15);
            diemTichLuyLabel.TabIndex = 11;
            diemTichLuyLabel.Text = "Điểm tích lũy";

            // emailTextBox
            emailTextBox.Location = new System.Drawing.Point(650, 64);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.ReadOnly = true;
            emailTextBox.Size = new System.Drawing.Size(250, 28);
            emailTextBox.TabIndex = 7;

            // emailLabel
            emailLabel.AutoSize = true;
            emailLabel.Location = new System.Drawing.Point(550, 64);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new System.Drawing.Size(53, 15);
            emailLabel.TabIndex = 6;
            emailLabel.Text = "Email";

            // diaChiTextBox
            diaChiTextBox.Location = new System.Drawing.Point(150, 110);
            diaChiTextBox.Name = "diaChiTextBox";
            diaChiTextBox.ReadOnly = true;
            diaChiTextBox.Size = new System.Drawing.Size(250, 28);
            diaChiTextBox.TabIndex = 5;

            // diaChiLabel
            diaChiLabel.AutoSize = true;
            diaChiLabel.Location = new System.Drawing.Point(20, 110);
            diaChiLabel.Name = "diaChiLabel";
            diaChiLabel.Size = new System.Drawing.Size(54, 15);
            diaChiLabel.TabIndex = 4;
            diaChiLabel.Text = "Địa chỉ";

            // soDienThoaiTextBox
            soDienThoaiTextBox.Location = new System.Drawing.Point(150, 64);
            soDienThoaiTextBox.Name = "soDienThoaiTextBox";
            soDienThoaiTextBox.ReadOnly = true;
            soDienThoaiTextBox.Size = new System.Drawing.Size(250, 28);
            soDienThoaiTextBox.TabIndex = 3;

            // soDienThoaiLabel
            soDienThoaiLabel.AutoSize = true;
            soDienThoaiLabel.Location = new System.Drawing.Point(20, 64);
            soDienThoaiLabel.Name = "soDienThoaiLabel";
            soDienThoaiLabel.Size = new System.Drawing.Size(83, 15);
            soDienThoaiLabel.TabIndex = 2;
            soDienThoaiLabel.Text = "Số điện thoại";

            // hoTenTextBox
            hoTenTextBox.Location = new System.Drawing.Point(650, 20);
            hoTenTextBox.Name = "hoTenTextBox";
            hoTenTextBox.ReadOnly = true;
            hoTenTextBox.Size = new System.Drawing.Size(250, 28);
            hoTenTextBox.TabIndex = 1;

            // hoTenLabel
            hoTenLabel.AutoSize = true;
            hoTenLabel.Location = new System.Drawing.Point(550, 24);
            hoTenLabel.Name = "hoTenLabel";
            hoTenLabel.Size = new System.Drawing.Size(46, 15);
            hoTenLabel.TabIndex = 1;
            hoTenLabel.Text = "Họ tên";

            // maKhachHangTextBox
            maKhachHangTextBox.Location = new System.Drawing.Point(150, 20);
            maKhachHangTextBox.Name = "maKhachHangTextBox";
            maKhachHangTextBox.ReadOnly = true;
            maKhachHangTextBox.Size = new System.Drawing.Size(250, 28);
            maKhachHangTextBox.TabIndex = 1;

            // maKhachHangLabel
            maKhachHangLabel.AutoSize = true;
            maKhachHangLabel.Location = new System.Drawing.Point(20, 24);
            maKhachHangLabel.Name = "maKhachHangLabel";
            maKhachHangLabel.Size = new System.Drawing.Size(46, 15);
            maKhachHangLabel.TabIndex = 0;
            maKhachHangLabel.Text = "Mã KH";

            // danhSachGroupBox
            danhSachGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            danhSachGroupBox.Controls.Add(khachHangDataGridView);
            danhSachGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            danhSachGroupBox.Location = new System.Drawing.Point(20, 300);
            danhSachGroupBox.Name = "danhSachGroupBox";
            danhSachGroupBox.Size = new System.Drawing.Size(1140, 440);
            danhSachGroupBox.TabIndex = 1;
            danhSachGroupBox.TabStop = false;
            danhSachGroupBox.Text = "Danh sách khách hàng";
            danhSachGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // khachHangDataGridView
            khachHangDataGridView.AllowUserToAddRows = false;
            khachHangDataGridView.AllowUserToDeleteRows = false;
            khachHangDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            khachHangDataGridView.BackgroundColor = System.Drawing.Color.White;
            khachHangDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            khachHangDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            khachHangDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                maKhachHangColumn,
                hoTenColumn,
                soDienThoaiColumn,
                diaChiColumn,
                emailColumn,
                diemTichLuyColumn,
                trangThaiColumn
            });
            khachHangDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            khachHangDataGridView.Location = new System.Drawing.Point(3, 19);
            khachHangDataGridView.MultiSelect = false;
            khachHangDataGridView.Name = "khachHangDataGridView";
            khachHangDataGridView.ReadOnly = true;
            khachHangDataGridView.RowHeadersVisible = false;
            khachHangDataGridView.RowTemplate.Height = 30;
            khachHangDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            khachHangDataGridView.Size = new System.Drawing.Size(1134, 418);
            khachHangDataGridView.TabIndex = 0;
            khachHangDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // maKhachHangColumn
            maKhachHangColumn.DataPropertyName = "MaKhachHang";
            maKhachHangColumn.HeaderText = "Mã KH";
            maKhachHangColumn.MinimumWidth = 80;
            maKhachHangColumn.Name = "maKhachHangColumn";
            maKhachHangColumn.ReadOnly = true;

            // hoTenColumn
            hoTenColumn.DataPropertyName = "TenKhachHang";
            hoTenColumn.HeaderText = "Họ tên";
            hoTenColumn.Name = "hoTenColumn";
            hoTenColumn.ReadOnly = true;

            // soDienThoaiColumn
            soDienThoaiColumn.DataPropertyName = "SoDienThoai";
            soDienThoaiColumn.HeaderText = "SĐT";
            soDienThoaiColumn.Name = "soDienThoaiColumn";
            soDienThoaiColumn.ReadOnly = true;

            // diaChiColumn
            diaChiColumn.DataPropertyName = "DiaChi";
            diaChiColumn.HeaderText = "Địa chỉ";
            diaChiColumn.Name = "diaChiColumn";
            diaChiColumn.ReadOnly = true;

            // emailColumn
            emailColumn.DataPropertyName = "Email";
            emailColumn.HeaderText = "Email";
            emailColumn.Name = "emailColumn";
            emailColumn.ReadOnly = true;

            // diemTichLuyColumn
            diemTichLuyColumn.DataPropertyName = "DiemTichLuy";
            diemTichLuyColumn.HeaderText = "Điểm tích lũy";
            diemTichLuyColumn.Name = "diemTichLuyColumn";
            diemTichLuyColumn.ReadOnly = true;

            // trangThaiColumn
            trangThaiColumn.DataPropertyName = "TrangThai";
            trangThaiColumn.HeaderText = "Trạng thái";
            trangThaiColumn.Name = "trangThaiColumn";
            trangThaiColumn.ReadOnly = true;

            // Form_KhachHang
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            ClientSize = new System.Drawing.Size(1180, 760);
            Controls.Add(danhSachGroupBox);
            Controls.Add(khachHangInfoGroupBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "Form_KhachHang";
            Text = "Quản lý khách hàng";
            khachHangInfoGroupBox.ResumeLayout(false);
            khachHangInfoGroupBox.PerformLayout();
            danhSachGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)khachHangDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

