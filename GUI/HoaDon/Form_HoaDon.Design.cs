

namespace mini_supermarket.GUI.HoaDon
{
    partial class Form_HoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            invoiceSearchGroupBox = new GroupBox();
            xuatFileButton = new Button();
            themFileButton = new Button();
            timKiemButton = new Button();
            groupBox2 = new GroupBox();
            searchTextBox = new TextBox();
            searchComboBox = new ComboBox();
            xemChiTietButton = new Button();
            lamMoiButton = new Button();
            huyButton = new Button();
            timeSearchGroupBox = new GroupBox();
            label2 = new Label();
            dateTimePicker3 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            groupBox1 = new GroupBox();
            textBox2 = new TextBox();
            label1 = new Label();
            textBox1 = new TextBox();
            hoaDonDataGridView = new DataGridView();
            maHoaDonColumn = new DataGridViewTextBoxColumn();
            ngayLapColumn = new DataGridViewTextBoxColumn();
            nhanVienColumn = new DataGridViewTextBoxColumn();
            khachHangColumn = new DataGridViewTextBoxColumn();
            thanhTienColumn = new DataGridViewTextBoxColumn();
            trangThaiColumn = new DataGridViewTextBoxColumn();
            danhSachGroupBox = new GroupBox();
            trangThaiGroupBox = new GroupBox();
            trangThaiComboBox = new ComboBox();
            trangThaiLabel = new Label();
            invoiceSearchGroupBox.SuspendLayout();
            groupBox2.SuspendLayout();
            timeSearchGroupBox.SuspendLayout();
            groupBox1.SuspendLayout();
            trangThaiGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)hoaDonDataGridView).BeginInit();
            danhSachGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // invoiceSearchGroupBox
            // 
            invoiceSearchGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            invoiceSearchGroupBox.BackColor = Color.WhiteSmoke;
            // invoiceSearchGroupBox.Controls.Add(timKiemButton);
            invoiceSearchGroupBox.Controls.Add(huyButton);
            invoiceSearchGroupBox.Controls.Add(groupBox2);
            invoiceSearchGroupBox.Controls.Add(xemChiTietButton);
            // invoiceSearchGroupBox.Controls.Add(revenueLabel);
            // invoiceSearchGroupBox.Controls.Add(countInvoicesLabel);
            invoiceSearchGroupBox.Controls.Add(lamMoiButton);
            invoiceSearchGroupBox.Controls.Add(timeSearchGroupBox);
            invoiceSearchGroupBox.Controls.Add(groupBox1);
            invoiceSearchGroupBox.Controls.Add(trangThaiGroupBox);
            invoiceSearchGroupBox.FlatStyle = FlatStyle.Flat;
            invoiceSearchGroupBox.Font = new Font("Segoe UI", 10F);
            invoiceSearchGroupBox.Location = new Point(20, 60);
            invoiceSearchGroupBox.Margin = new Padding(3, 4, 3, 4);
            invoiceSearchGroupBox.Name = "invoiceSearchGroupBox";
            invoiceSearchGroupBox.Padding = new Padding(3, 4, 3, 4);
            invoiceSearchGroupBox.Size = new Size(1140, 220);
            invoiceSearchGroupBox.TabIndex = 0;
            invoiceSearchGroupBox.TabStop = false;
            invoiceSearchGroupBox.Text = "Tìm kiếm hóa đơn";
            // 
            // xuatFileButton
            // 
            xuatFileButton.BackColor = Color.FromArgb(253, 126, 20);
            xuatFileButton.FlatStyle = FlatStyle.Flat;
            xuatFileButton.ForeColor = Color.White;
            xuatFileButton.Location = new Point(1040, 20);
            xuatFileButton.Margin = new Padding(3, 4, 3, 4);
            xuatFileButton.Name = "xuatFileButton";
            xuatFileButton.Size = new Size(137, 40);
            xuatFileButton.TabIndex = 27;
            xuatFileButton.Text = "Export Excel";
            xuatFileButton.UseVisualStyleBackColor = false;
            xuatFileButton.Click += exportFileButton_Click;
            // 
            // themFileButton
            // 
            themFileButton.BackColor = Color.FromArgb(33, 115, 70);
            themFileButton.FlatStyle = FlatStyle.Flat;
            themFileButton.ForeColor = Color.White;
            themFileButton.Location = new Point(900, 20);
            themFileButton.Margin = new Padding(3, 4, 3, 4);
            themFileButton.Name = "themFileButton";
            themFileButton.Size = new Size(137, 40);
            themFileButton.TabIndex = 27;
            themFileButton.Text = "Import Excel";
            themFileButton.UseVisualStyleBackColor = false;
            themFileButton.Click += importFileButton_Click;
            // 
            // huyButton
            // 
            huyButton.BackColor = Color.FromArgb(255, 77, 77);
            huyButton.FlatStyle = FlatStyle.Flat;
            huyButton.ForeColor = Color.White;
            huyButton.Location = new Point(1000, 69);
            huyButton.Margin = new Padding(3, 4, 3, 4);
            huyButton.Name = "huyButton";
            huyButton.Size = new Size(137, 40);
            huyButton.TabIndex = 24;
            huyButton.Text = "Hủy";
            huyButton.UseVisualStyleBackColor = false;
            huyButton.Click += huyButton_Click;
            // 
            // timKiemButton
            // 
            timKiemButton.BackColor = Color.FromArgb(0, 120, 215);
            timKiemButton.FlatStyle = FlatStyle.Flat;
            timKiemButton.ForeColor = Color.White;
            timKiemButton.Location = new Point(1000, 69);
            timKiemButton.Margin = new Padding(3, 4, 3, 4);
            timKiemButton.Name = "timKiemButton";
            timKiemButton.Size = new Size(137, 40);
            timKiemButton.TabIndex = 26;
            timKiemButton.Text = "Tìm kiếm";
            timKiemButton.UseVisualStyleBackColor = false;
            timKiemButton.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(searchTextBox);
            groupBox2.Controls.Add(searchComboBox);
            groupBox2.Location = new Point(20, 40);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(600, 68);
            groupBox2.TabIndex = 25;
            groupBox2.TabStop = false;
            groupBox2.Text = "Mã hóa đơn, nhân viên, khách hàng";
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(160, 27);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(430, 30);
            searchTextBox.TabIndex = 28;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // searchComboBox
            // 
            searchComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            searchComboBox.FormattingEnabled = true;
            searchComboBox.Items.AddRange(new object[] { "Mã hóa đơn", "Nhân viên", "Khách hàng" });
            searchComboBox.Location = new Point(14, 27);
            searchComboBox.Name = "searchComboBox";
            searchComboBox.Size = new Size(140, 31);
            searchComboBox.TabIndex = 27;
            searchComboBox.SelectedIndexChanged += searchComboBox_SelectedIndexChanged;
            // 
            // xemChiTietButton
            // 
            xemChiTietButton.BackColor = Color.FromArgb(0, 120, 215);
            xemChiTietButton.FlatStyle = FlatStyle.Flat;
            xemChiTietButton.ForeColor = Color.White;
            xemChiTietButton.Location = new Point(1000, 117);
            xemChiTietButton.Margin = new Padding(3, 4, 3, 4);
            xemChiTietButton.Name = "xemChiTietButton";
            xemChiTietButton.Size = new Size(137, 40);
            xemChiTietButton.TabIndex = 24;
            xemChiTietButton.Text = "Xem chi tiết";
            xemChiTietButton.UseVisualStyleBackColor = false;
            xemChiTietButton.Click += xemChiTietButton_Click;
            // 
            // revenueLabel
            // 
            // revenueLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // revenueLabel.AutoSize = true;
            // revenueLabel.Location = new Point(20, 233);
            // revenueLabel.Name = "revenueLabel";
            // revenueLabel.Size = new Size(152, 23);
            // revenueLabel.TabIndex = 22;
            // revenueLabel.Text = "Tổng doanh thu: 0";
            // 
            // countInvoicesLabel
            // 
            // countInvoicesLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // countInvoicesLabel.AutoSize = true;
            // countInvoicesLabel.Location = new Point(20, 210);
            // countInvoicesLabel.Name = "countInvoicesLabel";
            // countInvoicesLabel.Size = new Size(116, 23);
            // countInvoicesLabel.TabIndex = 21;
            // countInvoicesLabel.Text = "Số hóa đơn: 0";
            // 
            // lamMoiButton
            // 
            lamMoiButton.BackColor = Color.FromArgb(0, 120, 215);
            lamMoiButton.FlatStyle = FlatStyle.Flat;
            lamMoiButton.ForeColor = Color.White;
            lamMoiButton.Location = new Point(1000, 165);
            lamMoiButton.Margin = new Padding(3, 4, 3, 4);
            lamMoiButton.Name = "lamMoiButton";
            lamMoiButton.Size = new Size(137, 40);
            lamMoiButton.TabIndex = 20;
            lamMoiButton.Text = "Làm mới";
            lamMoiButton.UseVisualStyleBackColor = false;
            lamMoiButton.Click += lamMoiButton_Click;
            // 
            // timeSearchGroupBox
            // 
            timeSearchGroupBox.Controls.Add(label2);
            timeSearchGroupBox.Controls.Add(dateTimePicker3);
            timeSearchGroupBox.Controls.Add(dateTimePicker2);
            timeSearchGroupBox.Location = new Point(20, 120);
            timeSearchGroupBox.Name = "timeSearchGroupBox";
            timeSearchGroupBox.Size = new Size(600, 70);
            timeSearchGroupBox.TabIndex = 4;
            timeSearchGroupBox.TabStop = false;
            timeSearchGroupBox.Text = "Thời gian";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(286, 34);
            label2.Name = "label2";
            label2.Size = new Size(17, 23);
            label2.TabIndex = 7;
            label2.Text = "-";
            // 
            // dateTimePicker3
            // 
            dateTimePicker3.Location = new Point(320, 29);
            dateTimePicker3.Name = "dateTimePicker3";
            dateTimePicker3.Size = new Size(270, 30);
            dateTimePicker3.TabIndex = 6;
            dateTimePicker3.CustomFormat = "dd/MM/yyyy";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.ValueChanged += dateTimePicker3_ValueChanged;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(14, 29);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(253, 30);
            dateTimePicker2.TabIndex = 5;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(620, 40);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(320, 68);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thành tiền";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(170, 27);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(140, 30);
            textBox2.TabIndex = 3;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(150, 30);
            label1.Name = "label1";
            label1.Size = new Size(17, 23);
            label1.TabIndex = 2;
            label1.Text = "-";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(14, 27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(130, 30);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // roleLabel
            // 
            // roleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // roleLabel.AutoSize = true;
            // roleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            // roleLabel.Location = new Point(1020, 20);
            // roleLabel.Name = "roleLabel";
            // roleLabel.Size = new Size(122, 23);
            // roleLabel.TabIndex = 4;
            // roleLabel.Text = "Quyền: admin";
            // 
            // trangThaiGroupBox
            // 
            trangThaiGroupBox.Controls.Add(trangThaiComboBox);
            // trangThaiGroupBox.Controls.Add(trangThaiLabel);
            trangThaiGroupBox.Font = new Font("Segoe UI", 9F);
            trangThaiGroupBox.Location = new Point(640, 120);
            trangThaiGroupBox.Name = "trangThaiGroupBox";
            trangThaiGroupBox.Size = new Size(200, 70);
            trangThaiGroupBox.TabIndex = 7;
            trangThaiGroupBox.TabStop = false;
            trangThaiGroupBox.Text = "Trạng thái";
            // 
            // trangThaiLabel
            // 
            trangThaiLabel.AutoSize = true;
            trangThaiLabel.Location = new Point(10, 25);
            trangThaiLabel.Name = "trangThaiLabel";
            trangThaiLabel.Size = new Size(75, 20);
            trangThaiLabel.TabIndex = 0;
            trangThaiLabel.Text = "Trạng thái:";
            // 
            // trangThaiComboBox
            // 
            trangThaiComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            trangThaiComboBox.FormattingEnabled = true;
            trangThaiComboBox.Items.AddRange(new object[] { "Tất cả", "Đã xuất", "Đã hủy" });
            trangThaiComboBox.Location = new Point(10, 28);
            trangThaiComboBox.Name = "trangThaiComboBox";
            trangThaiComboBox.Size = new Size(180, 28);
            trangThaiComboBox.TabIndex = 0;
            // 
            // hoaDonDataGridView
            // 
            hoaDonDataGridView.AllowUserToAddRows = false;
            hoaDonDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(240, 240, 240);
            hoaDonDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            hoaDonDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            hoaDonDataGridView.BackgroundColor = Color.White;
            hoaDonDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            hoaDonDataGridView.Columns.AddRange(new DataGridViewColumn[] { maHoaDonColumn, ngayLapColumn, nhanVienColumn, khachHangColumn, thanhTienColumn, trangThaiColumn });
            hoaDonDataGridView.Dock = DockStyle.Fill;
            hoaDonDataGridView.Location = new Point(3, 26);
            hoaDonDataGridView.Margin = new Padding(3, 4, 3, 4);
            hoaDonDataGridView.MultiSelect = false;
            hoaDonDataGridView.Name = "hoaDonDataGridView";
            hoaDonDataGridView.ReadOnly = true;
            hoaDonDataGridView.RowHeadersVisible = false;
            hoaDonDataGridView.RowHeadersWidth = 51;
            hoaDonDataGridView.RowTemplate.Height = 30;
            hoaDonDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            hoaDonDataGridView.Size = new Size(1134, 438);
            hoaDonDataGridView.TabIndex = 0;
            hoaDonDataGridView.SelectionChanged += hoaDonDataGridView_SelectionChanged;
            // 
            // maHoaDonColumn
            // 
            maHoaDonColumn.DataPropertyName = "MaHoaDon";
            maHoaDonColumn.HeaderText = "Mã HĐ";
            maHoaDonColumn.MinimumWidth = 100;
            maHoaDonColumn.Name = "maHoaDonColumn";
            maHoaDonColumn.ReadOnly = true;
            maHoaDonColumn.Resizable = DataGridViewTriState.True;
            // 
            // ngayLapColumn
            // 
            ngayLapColumn.DataPropertyName = "NgayLap";
            ngayLapColumn.HeaderText = "Ngày lập";
            ngayLapColumn.MinimumWidth = 6;
            ngayLapColumn.Name = "ngayLapColumn";
            ngayLapColumn.ReadOnly = true;
            ngayLapColumn.Resizable = DataGridViewTriState.True;
            // 
            // nhanVienColumn
            // 
            nhanVienColumn.DataPropertyName = "NhanVien";
            nhanVienColumn.HeaderText = "Nhân viên";
            nhanVienColumn.MinimumWidth = 6;
            nhanVienColumn.Name = "nhanVienColumn";
            nhanVienColumn.ReadOnly = true;
            nhanVienColumn.Resizable = DataGridViewTriState.True;
            // 
            // khachHangColumn
            // 
            khachHangColumn.DataPropertyName = "KhachHang";
            khachHangColumn.HeaderText = "Khách hàng";
            khachHangColumn.MinimumWidth = 6;
            khachHangColumn.Name = "khachHangColumn";
            khachHangColumn.ReadOnly = true;
            // 
            // thanhTienColumn
            // 
            thanhTienColumn.DataPropertyName = "ThanhTien";
            thanhTienColumn.HeaderText = "Thành tiền";
            thanhTienColumn.MinimumWidth = 6;
            thanhTienColumn.Name = "thanhTienColumn";
            thanhTienColumn.ReadOnly = true;
            // 
            // trangThaiColumn
            // 
            trangThaiColumn.DataPropertyName = "TrangThai";
            trangThaiColumn.HeaderText = "Trạng thái";
            trangThaiColumn.MinimumWidth = 6;
            trangThaiColumn.Name = "trangThaiColumn";
            trangThaiColumn.ReadOnly = true;
            // 
            // danhSachGroupBox
            // 
            danhSachGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            danhSachGroupBox.Controls.Add(hoaDonDataGridView);
            danhSachGroupBox.FlatStyle = FlatStyle.Flat;
            danhSachGroupBox.Font = new Font("Segoe UI", 10F);
            danhSachGroupBox.Location = new Point(20, 300);
            danhSachGroupBox.Name = "danhSachGroupBox";
            danhSachGroupBox.Size = new System.Drawing.Size(1140, 400);
            danhSachGroupBox.TabIndex = 1;
            danhSachGroupBox.TabStop = false;
            danhSachGroupBox.Text = "Danh sách hóa đơn";
            // 
            // Form_HoaDon
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1180, 850);
            Controls.Add(danhSachGroupBox);
            // Controls.Add(themFileButton);
            Controls.Add(xuatFileButton);
            // Controls.Add(roleLabel);
            Controls.Add(invoiceSearchGroupBox);
            MinimumSize = new Size(1198, 897);
            Name = "Form_HoaDon";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý hóa đơn";
            Load += Form_HoaDon_Load;
            invoiceSearchGroupBox.ResumeLayout(false);
            invoiceSearchGroupBox.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            timeSearchGroupBox.ResumeLayout(false);
            timeSearchGroupBox.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            trangThaiGroupBox.ResumeLayout(false);
            trangThaiGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)hoaDonDataGridView).EndInit();
            danhSachGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox invoiceSearchGroupBox;
        private GroupBox groupBox1;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox1;
        private GroupBox timeSearchGroupBox;
        private Label label2;
        private DateTimePicker dateTimePicker3;
        private DateTimePicker dateTimePicker2;
        private Button lamMoiButton;
        private DataGridView hoaDonDataGridView;
        private GroupBox danhSachGroupBox;
        private DataGridViewTextBoxColumn maHoaDonColumn;
        private DataGridViewTextBoxColumn ngayLapColumn;
        private DataGridViewTextBoxColumn nhanVienColumn;
        private DataGridViewTextBoxColumn khachHangColumn;
        private DataGridViewTextBoxColumn thanhTienColumn;
        private DataGridViewTextBoxColumn trangThaiColumn;
        private Button xemChiTietButton;
        private GroupBox groupBox2;
        private TextBox searchTextBox;
        private ComboBox searchComboBox;
        private Button timKiemButton;
        private Button huyButton;
        private Button xuatFileButton;
        private Button themFileButton;
        private GroupBox trangThaiGroupBox;
        private ComboBox trangThaiComboBox;
        private Label trangThaiLabel;
    }
}