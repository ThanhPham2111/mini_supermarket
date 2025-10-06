namespace mini_supermarket.GUI.NhaCungCap
{
    partial class Form_NhaCungCap
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox customerInfoGroupBox;
        private System.Windows.Forms.Label maNhaCungCapLabel;
        private System.Windows.Forms.TextBox maNhaCungCapTextBox;
        private System.Windows.Forms.Label hoTenLabel;
        private System.Windows.Forms.TextBox hoTenTextBox;
        private System.Windows.Forms.Label soDienThoaiLabel;
        private System.Windows.Forms.TextBox soDienThoaiTextBox;
        // Add components
        // diaChi component
        private System.Windows.Forms.Label diaChiLabel;
        private System.Windows.Forms.TextBox diaChiTextBox;
        // email component
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox emailTextBox;
   
        // end add component
        private System.Windows.Forms.Button themButton;
        private System.Windows.Forms.Button suaButton;
        private System.Windows.Forms.Button xoaButton;
        private System.Windows.Forms.Button lamMoiButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.ComboBox statusFilterComboBox;
        private System.Windows.Forms.GroupBox danhSachGroupBox;
        private System.Windows.Forms.DataGridView nhaCungCapDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn maNhaCungCapColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaChiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoTenColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDienThoaiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThaiColumn;
        // add columns
        private System.Windows.Forms.DataGridViewTextBoxColumn emailColumn;
    
        // end add columns
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
            customerInfoGroupBox = new System.Windows.Forms.GroupBox();
            statusFilterComboBox = new System.Windows.Forms.ComboBox();
            searchTextBox = new System.Windows.Forms.TextBox();
            searchButton = new System.Windows.Forms.Button();
            lamMoiButton = new System.Windows.Forms.Button();
            xoaButton = new System.Windows.Forms.Button();
            suaButton = new System.Windows.Forms.Button();
            themButton = new System.Windows.Forms.Button();
            soDienThoaiTextBox = new System.Windows.Forms.TextBox();
            soDienThoaiLabel = new System.Windows.Forms.Label();
            hoTenTextBox = new System.Windows.Forms.TextBox();
            hoTenLabel = new System.Windows.Forms.Label();
            maNhaCungCapTextBox = new System.Windows.Forms.TextBox();
            maNhaCungCapLabel = new System.Windows.Forms.Label();
            danhSachGroupBox = new System.Windows.Forms.GroupBox();
            nhaCungCapDataGridView = new System.Windows.Forms.DataGridView();
            maNhaCungCapColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            diaChiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            hoTenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            soDienThoaiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            trangThaiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            roleLabel = new System.Windows.Forms.Label();

            // add init
            diaChiLabel = new System.Windows.Forms.Label();
            diaChiTextBox = new System.Windows.Forms.TextBox();
            emailLabel = new System.Windows.Forms.Label();
            emailTextBox = new System.Windows.Forms.TextBox();
            diaChiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            emailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            // end add init

            customerInfoGroupBox.SuspendLayout();
            danhSachGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nhaCungCapDataGridView).BeginInit();
            SuspendLayout();

            // customerInfoGroupBox
            customerInfoGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            // add component in GroupBox
            customerInfoGroupBox.Controls.Add(emailTextBox);
            customerInfoGroupBox.Controls.Add(emailLabel);
            customerInfoGroupBox.Controls.Add(diaChiTextBox);
            customerInfoGroupBox.Controls.Add(diaChiLabel);
            // end add component in GroupBox
            customerInfoGroupBox.Controls.Add(statusFilterComboBox);
            customerInfoGroupBox.Controls.Add(searchTextBox);
            customerInfoGroupBox.Controls.Add(searchButton);
            customerInfoGroupBox.Controls.Add(lamMoiButton);
            customerInfoGroupBox.Controls.Add(xoaButton);
            customerInfoGroupBox.Controls.Add(suaButton);
            customerInfoGroupBox.Controls.Add(themButton);
            customerInfoGroupBox.Controls.Add(soDienThoaiTextBox);
            customerInfoGroupBox.Controls.Add(soDienThoaiLabel);
            customerInfoGroupBox.Controls.Add(hoTenTextBox);
            customerInfoGroupBox.Controls.Add(hoTenLabel);
            customerInfoGroupBox.Controls.Add(maNhaCungCapTextBox);
            customerInfoGroupBox.Controls.Add(maNhaCungCapLabel);
            customerInfoGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            customerInfoGroupBox.Location = new System.Drawing.Point(20, 50);
            customerInfoGroupBox.Name = "customerInfoGroupBox";
            customerInfoGroupBox.Size = new System.Drawing.Size(1140, 240);
            customerInfoGroupBox.TabIndex = 0;
            customerInfoGroupBox.TabStop = false;
            customerInfoGroupBox.Text = "Thông tin nhà cung cấp";
            customerInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

           

            // emailTextBox
            emailTextBox.Location = new System.Drawing.Point(650, 64);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new System.Drawing.Size(250, 28);
            emailTextBox.TabIndex = 7;
            // emailTextBox.PlaceholderText = "Email";

            // emailLabel
            emailLabel.AutoSize = true;
            emailLabel.Location = new System.Drawing.Point(550, 64);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new System.Drawing.Size(53, 15);
            emailLabel.TabIndex = 6;
            emailLabel.Text = "Email";

            // diaChiTextBox
            diaChiTextBox.Location = new System.Drawing.Point(150, 64);
            diaChiTextBox.Name = "diaChiTextBox";
            diaChiTextBox.Size = new System.Drawing.Size(250, 28);
            diaChiTextBox.TabIndex = 5;
            // diaChiTextBox.PlaceholderText = "Địa chỉ";

            // diaChiLabel
            diaChiLabel.AutoSize = true;
            diaChiLabel.Location = new System.Drawing.Point(20, 64);
            diaChiLabel.Name = "diaChiLabel";
            diaChiLabel.Size = new System.Drawing.Size(54, 15);
            diaChiLabel.TabIndex = 4;
            diaChiLabel.Text = "Địa chỉ";

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
            themButton.Text = "Thêm";
            themButton.UseVisualStyleBackColor = true;
            themButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            themButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            themButton.ForeColor = System.Drawing.Color.White;

            // soDienThoaiTextBox
            soDienThoaiTextBox.Location = new System.Drawing.Point(150, 110);
            soDienThoaiTextBox.Name = "soDienThoaiTextBox";
            soDienThoaiTextBox.Size = new System.Drawing.Size(250, 28);
            soDienThoaiTextBox.TabIndex = 12;

            // soDienThoaiLabel
            soDienThoaiLabel.AutoSize = true;
            soDienThoaiLabel.Location = new System.Drawing.Point(20, 110);
            soDienThoaiLabel.Name = "soDienThoaiLabel";
            soDienThoaiLabel.Size = new System.Drawing.Size(83, 15);
            soDienThoaiLabel.TabIndex = 11;
            soDienThoaiLabel.Text = "Số điện thoại";

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
            hoTenLabel.Text = "Tên công ty";

            // maNhaCungCapTextBox
            maNhaCungCapTextBox.Location = new System.Drawing.Point(150, 20);
            maNhaCungCapTextBox.Name = "maNhaCungCapTextBox";
            maNhaCungCapTextBox.ReadOnly = true;
            maNhaCungCapTextBox.Size = new System.Drawing.Size(250, 28);
            maNhaCungCapTextBox.TabIndex = 1;

            // maNhaCungCapLabel
            maNhaCungCapLabel.AutoSize = true;
            maNhaCungCapLabel.Location = new System.Drawing.Point(20, 24);
            maNhaCungCapLabel.Name = "maNhaCungCapLabel";
            maNhaCungCapLabel.Size = new System.Drawing.Size(46, 15);
            maNhaCungCapLabel.TabIndex = 0;
            maNhaCungCapLabel.Text = "Mã nhà cung cấp";

            // danhSachGroupBox
            danhSachGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            danhSachGroupBox.Controls.Add(nhaCungCapDataGridView);
            danhSachGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            danhSachGroupBox.Location = new System.Drawing.Point(20, 300);
            danhSachGroupBox.Name = "danhSachGroupBox";
            danhSachGroupBox.Size = new System.Drawing.Size(1140, 440);
            danhSachGroupBox.TabIndex = 1;
            danhSachGroupBox.TabStop = false;
            danhSachGroupBox.Text = "Danh sách nhà cung cấp";
            danhSachGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // nhaCungCapDataGridView
            nhaCungCapDataGridView.AllowUserToAddRows = false;
            nhaCungCapDataGridView.AllowUserToDeleteRows = false;
            nhaCungCapDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            nhaCungCapDataGridView.BackgroundColor = System.Drawing.Color.White;
            nhaCungCapDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nhaCungCapDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            nhaCungCapDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                maNhaCungCapColumn,
                hoTenColumn,
                diaChiColumn,
                emailColumn,
                soDienThoaiColumn,
                trangThaiColumn
            });
            nhaCungCapDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            nhaCungCapDataGridView.Location = new System.Drawing.Point(3, 19);
            nhaCungCapDataGridView.MultiSelect = false;
            nhaCungCapDataGridView.Name = "nhaCungCapDataGridView";
            nhaCungCapDataGridView.ReadOnly = true;
            nhaCungCapDataGridView.RowHeadersVisible = false;
            nhaCungCapDataGridView.RowTemplate.Height = 30;
            nhaCungCapDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            nhaCungCapDataGridView.Size = new System.Drawing.Size(1134, 418);
            nhaCungCapDataGridView.TabIndex = 0;
            nhaCungCapDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // maNhaCungCapColumn
            maNhaCungCapColumn.DataPropertyName = "MaNhaCungCap";
            maNhaCungCapColumn.HeaderText = "Mã nhà cung cấp";
            maNhaCungCapColumn.MinimumWidth = 100;
            maNhaCungCapColumn.Name = "maNhaCungCapColumn";
            maNhaCungCapColumn.ReadOnly = true;

            // diaChiColumn
            diaChiColumn.DataPropertyName = "DiaChi";
            diaChiColumn.HeaderText = "Địa chỉ";
            diaChiColumn.Name = "diaChiColumn";
            diaChiColumn.ReadOnly = true;

            // hoTenColumn
            hoTenColumn.DataPropertyName = "TenNhaCungCap";
            hoTenColumn.HeaderText = "Họ tên";
            hoTenColumn.Name = "hoTenColumn";
            hoTenColumn.ReadOnly = true;

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

            // add css new column
            // emailColumn
            emailColumn.DataPropertyName = "Email";
            emailColumn.HeaderText = "Email";
            emailColumn.Name = "emailColumn";
            emailColumn.ReadOnly = true;

           

            // roleLabel
            roleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            roleLabel.AutoSize = true;
            roleLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            roleLabel.Location = new System.Drawing.Point(1020, 20);
            roleLabel.Name = "roleLabel";
            roleLabel.Size = new System.Drawing.Size(87, 15);
            roleLabel.TabIndex = 2;
            roleLabel.Text = "Quyền: admin";

            // Form_NhaCungCap
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            ClientSize = new System.Drawing.Size(1180, 760);
            Controls.Add(roleLabel);
            Controls.Add(danhSachGroupBox);
            Controls.Add(customerInfoGroupBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "Form_NhaCungCap";
            Text = "Quản lý nhà cung cấp";
            customerInfoGroupBox.ResumeLayout(false);
            customerInfoGroupBox.PerformLayout();
            danhSachGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nhaCungCapDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}