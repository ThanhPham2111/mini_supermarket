using System;
using System.Drawing;
using System.Windows.Forms;

namespace mini_supermarket.GUI.FormKhoHang
{
    partial class Form_KhoHang
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox khoHangInfoGroupBox;
        private ComboBox sanPhamComboBox;
        private TextBox maSanPhamTextBox;
        private NumericUpDown soLuongNumericUpDown;
        private ComboBox trangThaiComboBox;
        private Label maSanPhamLabel;
        private Label soLuongLabel;
        private Label trangThaiLabel;
        private Button themButton;
        private Button suaButton;
        private Button xoaButton;
        private Button lamMoiButton;
        private ComboBox statusFilterComboBox;
        private Label statusLabel;
        private TextBox searchTextBox;
        private Label searchLabel;
        private DataGridView khoHangDataGridView;
        private DataGridViewTextBoxColumn MaSanPhamColumn;
        private DataGridViewTextBoxColumn TenSanPhamColumn;
        private DataGridViewTextBoxColumn SoLuongColumn;
        private DataGridViewTextBoxColumn TrangThaiColumn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            khoHangInfoGroupBox = new GroupBox();
            maSanPhamLabel = new Label();
            maSanPhamTextBox = new TextBox();
            soLuongLabel = new Label();
            soLuongNumericUpDown = new NumericUpDown();
            trangThaiLabel = new Label();
            trangThaiComboBox = new ComboBox();
            themButton = new Button();
            suaButton = new Button();
            xoaButton = new Button();
            lamMoiButton = new Button();
            sanPhamComboBox = new ComboBox();
            statusFilterComboBox = new ComboBox();
            statusLabel = new Label();
            searchTextBox = new TextBox();
            searchLabel = new Label();
            khoHangDataGridView = new DataGridView();
            MaSanPhamColumn = new DataGridViewTextBoxColumn();
            TenSanPhamColumn = new DataGridViewTextBoxColumn();
            SoLuongColumn = new DataGridViewTextBoxColumn();
            TrangThaiColumn = new DataGridViewTextBoxColumn();
            khoHangInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)soLuongNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)khoHangDataGridView).BeginInit();
            SuspendLayout();
            // 
            // khoHangInfoGroupBox
            // 
            khoHangInfoGroupBox.Controls.Add(maSanPhamLabel);
            khoHangInfoGroupBox.Controls.Add(maSanPhamTextBox);
            khoHangInfoGroupBox.Controls.Add(soLuongLabel);
            khoHangInfoGroupBox.Controls.Add(soLuongNumericUpDown);
            khoHangInfoGroupBox.Controls.Add(trangThaiLabel);
            khoHangInfoGroupBox.Controls.Add(trangThaiComboBox);
            khoHangInfoGroupBox.Controls.Add(themButton);
            khoHangInfoGroupBox.Controls.Add(suaButton);
            khoHangInfoGroupBox.Controls.Add(xoaButton);
            khoHangInfoGroupBox.Controls.Add(lamMoiButton);
            khoHangInfoGroupBox.Controls.Add(sanPhamComboBox);
            khoHangInfoGroupBox.Font = new Font("Segoe UI", 10F);
            khoHangInfoGroupBox.Location = new Point(20, 20);
            khoHangInfoGroupBox.Name = "khoHangInfoGroupBox";
            khoHangInfoGroupBox.Size = new Size(1140, 160);
            khoHangInfoGroupBox.TabIndex = 0;
            khoHangInfoGroupBox.TabStop = false;
            khoHangInfoGroupBox.Text = "Thông tin kho hàng";
            khoHangInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            // 
            // maSanPhamLabel
            // 
            maSanPhamLabel.Location = new Point(20, 30);
            maSanPhamLabel.Name = "maSanPhamLabel";
            maSanPhamLabel.Size = new Size(120, 23);
            maSanPhamLabel.TabIndex = 0;
            maSanPhamLabel.Text = "Mã SP";
            // 
            // maSanPhamTextBox
            // 
            maSanPhamTextBox.Location = new Point(150, 27);
            maSanPhamTextBox.Multiline = true;
            maSanPhamTextBox.Name = "maSanPhamTextBox";
            maSanPhamTextBox.ReadOnly = true;
            maSanPhamTextBox.Size = new Size(200, 30);
            maSanPhamTextBox.TabIndex = 1;
            // 
            // soLuongLabel
            // 
            soLuongLabel.Location = new Point(420, 30);
            soLuongLabel.Name = "soLuongLabel";
            soLuongLabel.Size = new Size(88, 23);
            soLuongLabel.TabIndex = 2;
            soLuongLabel.Text = "Số lượng";
            // 
            // soLuongNumericUpDown
            // 
            soLuongNumericUpDown.Location = new Point(510, 27);
            soLuongNumericUpDown.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            soLuongNumericUpDown.Name = "soLuongNumericUpDown";
            soLuongNumericUpDown.Size = new Size(120, 30);
            soLuongNumericUpDown.TabIndex = 3;
            // 
            // trangThaiLabel
            // 
            trangThaiLabel.Location = new Point(650, 30);
            trangThaiLabel.Name = "trangThaiLabel";
            trangThaiLabel.Size = new Size(90, 23);
            trangThaiLabel.TabIndex = 4;
            trangThaiLabel.Text = "Trạng thái";
            // 
            // trangThaiComboBox
            // 
            trangThaiComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            trangThaiComboBox.Location = new Point(740, 27);
            trangThaiComboBox.Name = "trangThaiComboBox";
            trangThaiComboBox.Size = new Size(150, 31);
            trangThaiComboBox.TabIndex = 5;
            trangThaiComboBox.SelectedIndexChanged += trangThaiComboBox_SelectedIndexChanged;
            // 
            // themButton
            // 
            themButton.Location = new Point(20, 100);
            themButton.Name = "themButton";
            themButton.Size = new Size(120, 30);
            themButton.TabIndex = 6;
            themButton.Text = "Thêm";
            themButton.UseVisualStyleBackColor = true;
            themButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            themButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215); // Xanh dương
            themButton.ForeColor = System.Drawing.Color.White; // Chữ trắng
            // 
            // suaButton
            // 
            suaButton.Location = new Point(160, 100);
            suaButton.Name = "suaButton";
            suaButton.Size = new Size(120, 30);
            suaButton.TabIndex = 7;
            suaButton.Text = "Sửa";
            suaButton.UseVisualStyleBackColor = true;
            suaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            suaButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215); // Xanh dương
            suaButton.ForeColor = System.Drawing.Color.White; // Chữ trắng
            // 
            // xoaButton
            // 
            xoaButton.Location = new Point(300, 100);
            xoaButton.Name = "xoaButton";
            xoaButton.Size = new Size(120, 30);
            xoaButton.TabIndex = 8;
            xoaButton.Text = "Xóa";
            xoaButton.UseVisualStyleBackColor = true;
            xoaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            xoaButton.BackColor = System.Drawing.Color.FromArgb(255, 77, 77); // Đỏ
            xoaButton.ForeColor = System.Drawing.Color.White; // Chữ trắng
            // 
            // lamMoiButton
            // 
            lamMoiButton.Location = new Point(440, 100);
            lamMoiButton.Name = "lamMoiButton";
            lamMoiButton.Size = new Size(120, 30);
            lamMoiButton.TabIndex = 9;
            lamMoiButton.Text = "Làm mới";
            lamMoiButton.UseVisualStyleBackColor = true;
            lamMoiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            lamMoiButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215); // Xanh dương
            lamMoiButton.ForeColor = System.Drawing.Color.White; // Chữ trắng
            // 
            // sanPhamComboBox
            // 
            sanPhamComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            sanPhamComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            sanPhamComboBox.Location = new Point(150, 60);
            sanPhamComboBox.Name = "sanPhamComboBox";
            sanPhamComboBox.Size = new Size(250, 31);
            sanPhamComboBox.TabIndex = 10;
            sanPhamComboBox.Leave += sanPhamComboBox_Leave;
            // 
            // statusFilterComboBox
            // 
            statusFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            statusFilterComboBox.Location = new Point(980, 30);
            statusFilterComboBox.Name = "statusFilterComboBox";
            statusFilterComboBox.Size = new Size(180, 29);
            statusFilterComboBox.TabIndex = 1;
            // 
            // statusLabel
            // 
            statusLabel.Location = new Point(940, 30);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(50, 23);
            statusLabel.TabIndex = 2;
            statusLabel.Text = "Trạng thái";
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(110, 187);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(300, 29);
            searchTextBox.TabIndex = 3;
            // 
            // searchLabel
            // 
            searchLabel.Location = new Point(20, 190);
            searchLabel.Name = "searchLabel";
            searchLabel.Size = new Size(80, 23);
            searchLabel.TabIndex = 4;
            searchLabel.Text = "Tìm kiếm";
            // 
            // khoHangDataGridView
            // 
            khoHangDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            khoHangDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            khoHangDataGridView.ColumnHeadersHeight = 29;
            khoHangDataGridView.Columns.AddRange(new DataGridViewColumn[] { MaSanPhamColumn, TenSanPhamColumn, SoLuongColumn, TrangThaiColumn });
            khoHangDataGridView.Location = new Point(20, 230);
            khoHangDataGridView.MultiSelect = false;
            khoHangDataGridView.Name = "khoHangDataGridView";
            khoHangDataGridView.ReadOnly = true;
            khoHangDataGridView.RowHeadersVisible = false;
            khoHangDataGridView.RowHeadersWidth = 51;
            khoHangDataGridView.RowTemplate.Height = 30;
            khoHangDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            khoHangDataGridView.Size = new Size(1140, 500);
            khoHangDataGridView.TabIndex = 5;
            // 
            // MaSanPhamColumn
            // 
            MaSanPhamColumn.DataPropertyName = "MaSanPham";
            MaSanPhamColumn.HeaderText = "Mã SP";
            MaSanPhamColumn.MinimumWidth = 6;
            MaSanPhamColumn.Name = "MaSanPhamColumn";
            MaSanPhamColumn.ReadOnly = true;
            // 
            // TenSanPhamColumn
            // 
            TenSanPhamColumn.DataPropertyName = "TenSanPham";
            TenSanPhamColumn.HeaderText = "Tên SP";
            TenSanPhamColumn.MinimumWidth = 6;
            TenSanPhamColumn.Name = "TenSanPhamColumn";
            TenSanPhamColumn.ReadOnly = true;
            // 
            // SoLuongColumn
            // 
            SoLuongColumn.DataPropertyName = "SoLuong";
            SoLuongColumn.HeaderText = "Số lượng";
            SoLuongColumn.MinimumWidth = 6;
            SoLuongColumn.Name = "SoLuongColumn";
            SoLuongColumn.ReadOnly = true;
            // 
            // TrangThaiColumn
            // 
            TrangThaiColumn.DataPropertyName = "TrangThai";
            TrangThaiColumn.HeaderText = "Trạng thái";
            TrangThaiColumn.MinimumWidth = 6;
            TrangThaiColumn.Name = "TrangThaiColumn";
            TrangThaiColumn.ReadOnly = true;
            // 
            // Form_KhoHang
            // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1180, 760);
            Controls.Add(khoHangInfoGroupBox);
            Controls.Add(statusFilterComboBox);
            Controls.Add(statusLabel);
            Controls.Add(searchTextBox);
            Controls.Add(searchLabel);
            Controls.Add(khoHangDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form_KhoHang";
            Text = "Quản lý kho hàng";
            khoHangInfoGroupBox.ResumeLayout(false);
            khoHangInfoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)soLuongNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)khoHangDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
