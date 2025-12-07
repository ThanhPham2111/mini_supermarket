namespace mini_supermarket.GUI.Form_SanPham
{
    partial class Form_SanPham
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button xemChiTietButton;
        private System.Windows.Forms.Button themButton;
        private System.Windows.Forms.Button suaButton;
        private System.Windows.Forms.Button xoaButton;
        private System.Windows.Forms.Button khoaButton;
        private System.Windows.Forms.Button lamMoiButton;
        private System.Windows.Forms.Button exportExcelButton;
        private System.Windows.Forms.ComboBox statusFilterComboBox;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.GroupBox danhSachGroupBox;
        private System.Windows.Forms.DataGridView sanPhamDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn maSanPhamColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenSanPhamColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn giaBanColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loaiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hsdColumn;
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle giaBanCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle hsdCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            searchButton = new System.Windows.Forms.Button();
            searchTextBox = new System.Windows.Forms.TextBox();
            xemChiTietButton = new System.Windows.Forms.Button();
            themButton = new System.Windows.Forms.Button();
            suaButton = new System.Windows.Forms.Button();
            xoaButton = new System.Windows.Forms.Button();
            khoaButton = new System.Windows.Forms.Button();
            lamMoiButton = new System.Windows.Forms.Button();
            exportExcelButton = new System.Windows.Forms.Button();
            statusFilterComboBox = new System.Windows.Forms.ComboBox();
            searchLabel = new System.Windows.Forms.Label();
            statusLabel = new System.Windows.Forms.Label();
            danhSachGroupBox = new System.Windows.Forms.GroupBox();
            sanPhamDataGridView = new System.Windows.Forms.DataGridView();
            maSanPhamColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            tenSanPhamColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            donViColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            giaBanColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            loaiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            hsdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            trangThaiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            danhSachGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sanPhamDataGridView).BeginInit();
            SuspendLayout();

            // searchButton
            searchButton.Location = new System.Drawing.Point(20, 20);
            searchButton.Name = "searchButton";
            searchButton.Size = new System.Drawing.Size(110, 35);
            searchButton.TabIndex = 0;
            searchButton.Text = "Tìm kiếm";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            searchButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            searchButton.ForeColor = System.Drawing.Color.White;

            // searchTextBox
            searchTextBox.Location = new System.Drawing.Point(140, 20);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Tìm kiếm theo mã, tên hoặc đơn vị";
            searchTextBox.Size = new System.Drawing.Size(550, 35);
            searchTextBox.TabIndex = 1;

            // xemChiTietButton
            xemChiTietButton.Location = new System.Drawing.Point(20, 65);
            xemChiTietButton.Name = "xemChiTietButton";
            xemChiTietButton.Size = new System.Drawing.Size(110, 35);
            xemChiTietButton.TabIndex = 2;
            xemChiTietButton.Text = "Xem chi tiết";
            xemChiTietButton.UseVisualStyleBackColor = true;
            xemChiTietButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            xemChiTietButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            xemChiTietButton.ForeColor = System.Drawing.Color.White;

            // themButton
            themButton.Location = new System.Drawing.Point(170, 65);
            themButton.Name = "themButton";
            themButton.Size = new System.Drawing.Size(110, 35);
            themButton.TabIndex = 3;
            themButton.Text = "➕ Thêm";
            themButton.UseVisualStyleBackColor = true;
            themButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            themButton.BackColor = System.Drawing.Color.FromArgb(16, 137, 62);
            themButton.ForeColor = System.Drawing.Color.White;

            // suaButton
            suaButton.Location = new System.Drawing.Point(300, 65);
            suaButton.Name = "suaButton";
            suaButton.Size = new System.Drawing.Size(110, 35);
            suaButton.TabIndex = 4;
            suaButton.Text = "Sửa";
            suaButton.UseVisualStyleBackColor = true;
            suaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            suaButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            suaButton.ForeColor = System.Drawing.Color.White;

            // xoaButton
            // xoaButton.Location = new System.Drawing.Point(430, 65);
            // xoaButton.Name = "xoaButton";
            // xoaButton.Size = new System.Drawing.Size(110, 35);
            // xoaButton.TabIndex = 5;
            // xoaButton.Text = "Khóa";
            // xoaButton.UseVisualStyleBackColor = true;
            // xoaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            // xoaButton.BackColor = System.Drawing.Color.FromArgb(255, 77, 77);
            // xoaButton.ForeColor = System.Drawing.Color.White;
            //  xoaButton.Visible = false;

            // khoaButton
          
            // lamMoiButton
            lamMoiButton.Location = new System.Drawing.Point(430, 65);
            lamMoiButton.Name = "lamMoiButton";
            lamMoiButton.Size = new System.Drawing.Size(110, 35);
            lamMoiButton.TabIndex = 5;
            lamMoiButton.Text = "Làm mới";
            lamMoiButton.UseVisualStyleBackColor = true;
            lamMoiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            lamMoiButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            lamMoiButton.ForeColor = System.Drawing.Color.White;

            // exportExcelButton
            exportExcelButton.Location = new System.Drawing.Point(560, 65);
            exportExcelButton.Name = "exportExcelButton";
            exportExcelButton.Size = new System.Drawing.Size(110, 35);
            exportExcelButton.TabIndex = 6;
            exportExcelButton.Text = "📊 Export Excel";
            exportExcelButton.UseVisualStyleBackColor = true;
            exportExcelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            exportExcelButton.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            exportExcelButton.ForeColor = System.Drawing.Color.White;

            // statusFilterComboBox
            statusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            statusFilterComboBox.FormattingEnabled = true;
            statusFilterComboBox.Location = new System.Drawing.Point(700, 20);
            statusFilterComboBox.Name = "statusFilterComboBox";
            statusFilterComboBox.Size = new System.Drawing.Size(170, 35);
            statusFilterComboBox.TabIndex = 7;

            // searchLabel
            searchLabel.AutoSize = true;
            searchLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            searchLabel.Location = new System.Drawing.Point(140, 0);
            searchLabel.Name = "searchLabel";
            searchLabel.Size = new System.Drawing.Size(63, 23);
            searchLabel.TabIndex = 8;
            searchLabel.Text = "Từ khóa";

            // statusLabel
            statusLabel.AutoSize = true;
            statusLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            statusLabel.Location = new System.Drawing.Point(700, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(74, 23);
            statusLabel.TabIndex = 9;
            statusLabel.Text = "Trạng thái";

            // danhSachGroupBox
            danhSachGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            danhSachGroupBox.Controls.Add(sanPhamDataGridView);
            danhSachGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            danhSachGroupBox.Location = new System.Drawing.Point(20, 110);
            danhSachGroupBox.Name = "danhSachGroupBox";
            danhSachGroupBox.Size = new System.Drawing.Size(1140, 630);
            danhSachGroupBox.TabIndex = 1;
            danhSachGroupBox.TabStop = false;
            danhSachGroupBox.Text = "Danh sách sản phẩm";
            danhSachGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // sanPhamDataGridView
            sanPhamDataGridView.AllowUserToAddRows = false;
            sanPhamDataGridView.AllowUserToDeleteRows = false;
            sanPhamDataGridView.AllowUserToResizeRows = false;
            sanPhamDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            sanPhamDataGridView.BackgroundColor = System.Drawing.Color.White;
            sanPhamDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            sanPhamDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            sanPhamDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { maSanPhamColumn, tenSanPhamColumn, donViColumn, giaBanColumn, loaiColumn, hsdColumn, trangThaiColumn });
            sanPhamDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            sanPhamDataGridView.Location = new System.Drawing.Point(3, 19);
            sanPhamDataGridView.MultiSelect = false;
            sanPhamDataGridView.Name = "sanPhamDataGridView";
            sanPhamDataGridView.ReadOnly = true;
            sanPhamDataGridView.RowHeadersVisible = false;
            sanPhamDataGridView.RowTemplate.Height = 30;
            sanPhamDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            sanPhamDataGridView.Size = new System.Drawing.Size(1134, 608);
            sanPhamDataGridView.TabIndex = 0;
            sanPhamDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // maSanPhamColumn
            maSanPhamColumn.DataPropertyName = "MaSanPham";
            maSanPhamColumn.HeaderText = "Mã sản phẩm";
            maSanPhamColumn.MinimumWidth = 100;
            maSanPhamColumn.Name = "maSanPhamColumn";
            maSanPhamColumn.ReadOnly = true;

            // tenSanPhamColumn
            tenSanPhamColumn.DataPropertyName = "TenSanPham";
            tenSanPhamColumn.HeaderText = "Tên sản phẩm";
            tenSanPhamColumn.MinimumWidth = 200;
            tenSanPhamColumn.Name = "tenSanPhamColumn";
            tenSanPhamColumn.ReadOnly = true;

            // donViColumn
            donViColumn.DataPropertyName = "TenDonVi";
            donViColumn.HeaderText = "Đơn vị";
            donViColumn.MinimumWidth = 100;
            donViColumn.Name = "donViColumn";
            donViColumn.ReadOnly = true;

            // giaBanColumn
            giaBanColumn.DataPropertyName = "GiaBan";
            giaBanCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            giaBanCellStyle.Format = "N0";
            giaBanCellStyle.NullValue = null;
            giaBanColumn.DefaultCellStyle = giaBanCellStyle;
            giaBanColumn.HeaderText = "Giá bán";
            giaBanColumn.MinimumWidth = 120;
            giaBanColumn.Name = "giaBanColumn";
            giaBanColumn.ReadOnly = true;

            // loaiColumn
            loaiColumn.DataPropertyName = "TenLoai";
            loaiColumn.HeaderText = "Tên loại";
            loaiColumn.MinimumWidth = 100;
            loaiColumn.Name = "loaiColumn";
            loaiColumn.ReadOnly = true;

            // hsdColumn
            hsdColumn.DataPropertyName = "Hsd";
            hsdCellStyle.Format = "dd/MM/yyyy";
            hsdColumn.DefaultCellStyle = hsdCellStyle;
            hsdColumn.HeaderText = "HSD";
            hsdColumn.MinimumWidth = 120;
            hsdColumn.Name = "hsdColumn";
            hsdColumn.ReadOnly = true;

            // trangThaiColumn
            trangThaiColumn.DataPropertyName = "TrangThai";
            trangThaiColumn.HeaderText = "Trạng thái";
            trangThaiColumn.MinimumWidth = 120;
            trangThaiColumn.Name = "trangThaiColumn";
            trangThaiColumn.ReadOnly = true;

            // Form_SanPham
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            ClientSize = new System.Drawing.Size(1180, 760);
            Controls.Add(statusLabel);
            Controls.Add(searchLabel);
            Controls.Add(statusFilterComboBox);
            Controls.Add(exportExcelButton);
            Controls.Add(lamMoiButton);
            // Controls.Add(khoaButton); // Ẩn nút khóa
            // Controls.Add(xoaButton);
            Controls.Add(suaButton);
            Controls.Add(themButton);
            Controls.Add(xemChiTietButton);
            Controls.Add(searchTextBox);
            Controls.Add(searchButton);
            Controls.Add(danhSachGroupBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "Form_SanPham";
            Text = "Quản lý sản phẩm";
            danhSachGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)sanPhamDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
