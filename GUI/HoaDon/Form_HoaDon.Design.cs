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
            thongKeButton = new Button();
            revenueLabel = new Label();
            countInvoicesLabel = new Label();
            lamMoiButton = new Button();
            xoaButton = new Button();
            suaButton = new Button();
            themButton = new Button();
            timeSearchGroupBox = new GroupBox();
            label2 = new Label();
            dateTimePicker3 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            khoangThoiGianRadioButton = new RadioButton();
            ngayRadioButton = new RadioButton();
            groupBox1 = new GroupBox();
            textBox2 = new TextBox();
            label1 = new Label();
            textBox1 = new TextBox();
            customerSearchGroupBox = new GroupBox();
            customerSearchTextBox = new TextBox();
            employeeSearchGroupBox = new GroupBox();
            employeeSearchTextBox = new TextBox();
            idSearchGroupBox = new GroupBox();
            idSearchTextBox = new TextBox();
            roleLabel = new Label();
            hoaDonDataGridView = new DataGridView();
            maHoaDonColumn = new DataGridViewTextBoxColumn();
            ngayLapColumn = new DataGridViewTextBoxColumn();
            nhanVienColumn = new DataGridViewTextBoxColumn();
            khachHangColumn = new DataGridViewTextBoxColumn();
            thanhTienColumn = new DataGridViewTextBoxColumn();
            danhSachGroupBox = new GroupBox();
            invoiceSearchGroupBox.SuspendLayout();
            timeSearchGroupBox.SuspendLayout();
            groupBox1.SuspendLayout();
            customerSearchGroupBox.SuspendLayout();
            employeeSearchGroupBox.SuspendLayout();
            idSearchGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)hoaDonDataGridView).BeginInit();
            danhSachGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // invoiceSearchGroupBox
            // 
            invoiceSearchGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            invoiceSearchGroupBox.BackColor = Color.WhiteSmoke;
            invoiceSearchGroupBox.Controls.Add(thongKeButton);
            invoiceSearchGroupBox.Controls.Add(revenueLabel);
            invoiceSearchGroupBox.Controls.Add(countInvoicesLabel);
            invoiceSearchGroupBox.Controls.Add(lamMoiButton);
            invoiceSearchGroupBox.Controls.Add(xoaButton);
            invoiceSearchGroupBox.Controls.Add(suaButton);
            invoiceSearchGroupBox.Controls.Add(themButton);
            invoiceSearchGroupBox.Controls.Add(timeSearchGroupBox);
            invoiceSearchGroupBox.Controls.Add(groupBox1);
            invoiceSearchGroupBox.Controls.Add(customerSearchGroupBox);
            invoiceSearchGroupBox.Controls.Add(employeeSearchGroupBox);
            invoiceSearchGroupBox.Controls.Add(idSearchGroupBox);
            invoiceSearchGroupBox.FlatStyle = FlatStyle.Flat;
            invoiceSearchGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            invoiceSearchGroupBox.Location = new Point(20, 50);
            invoiceSearchGroupBox.Margin = new Padding(3, 4, 3, 4);
            invoiceSearchGroupBox.Name = "invoiceSearchGroupBox";
            invoiceSearchGroupBox.Padding = new Padding(3, 4, 3, 4);
            invoiceSearchGroupBox.Size = new Size(1140, 320);
            invoiceSearchGroupBox.TabIndex = 0;
            invoiceSearchGroupBox.TabStop = false;
            invoiceSearchGroupBox.Text = "Tìm kiếm hóa đơn";
            // 
            // thongKeButton
            // 
            thongKeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            thongKeButton.BackColor = Color.FromArgb(45, 190, 97);
            thongKeButton.FlatStyle = FlatStyle.Flat;
            thongKeButton.ForeColor = Color.White;
            thongKeButton.Location = new Point(980, 260);
            thongKeButton.Margin = new Padding(3, 4, 3, 4);
            thongKeButton.Name = "thongKeButton";
            thongKeButton.Size = new Size(137, 40);
            thongKeButton.TabIndex = 23;
            thongKeButton.Text = "Thống kê";
            thongKeButton.UseVisualStyleBackColor = false;
            // 
            // revenueLabel
            // 
            revenueLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            revenueLabel.AutoSize = true;
            revenueLabel.Location = new Point(800, 220);
            revenueLabel.Name = "revenueLabel";
            revenueLabel.Size = new Size(152, 23);
            revenueLabel.TabIndex = 22;
            revenueLabel.Text = "Tổng doanh thu: 0";
            // 
            // countInvoicesLabel
            // 
            countInvoicesLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            countInvoicesLabel.AutoSize = true;
            countInvoicesLabel.Location = new Point(800, 190);
            countInvoicesLabel.Name = "countInvoicesLabel";
            countInvoicesLabel.Size = new Size(116, 23);
            countInvoicesLabel.TabIndex = 21;
            countInvoicesLabel.Text = "Số hóa đơn: 0";
            // 
            // lamMoiButton
            // 
            lamMoiButton.Location = new Point(580, 260);
            lamMoiButton.Margin = new Padding(3, 4, 3, 4);
            lamMoiButton.Name = "lamMoiButton";
            lamMoiButton.Size = new Size(137, 40);
            lamMoiButton.TabIndex = 20;
            lamMoiButton.Text = "Làm mới";
            lamMoiButton.UseVisualStyleBackColor = false;
            lamMoiButton.BackColor = Color.FromArgb(0, 120, 215);
            lamMoiButton.ForeColor = Color.White;
            lamMoiButton.FlatStyle = FlatStyle.Flat;
            // 
            // xoaButton
            // 
            xoaButton.Location = new Point(420, 260);
            xoaButton.Margin = new Padding(3, 4, 3, 4);
            xoaButton.Name = "xoaButton";
            xoaButton.Size = new Size(137, 40);
            xoaButton.TabIndex = 19;
            xoaButton.Text = "Xóa";
            xoaButton.UseVisualStyleBackColor = false;
            xoaButton.BackColor = Color.FromArgb(255, 77, 77);
            xoaButton.ForeColor = Color.White;
            xoaButton.FlatStyle = FlatStyle.Flat;
            // 
            // suaButton
            // 
            suaButton.Location = new Point(260, 260);
            suaButton.Margin = new Padding(3, 4, 3, 4);
            suaButton.Name = "suaButton";
            suaButton.Size = new Size(137, 40);
            suaButton.TabIndex = 18;
            suaButton.Text = "Sửa";
            suaButton.UseVisualStyleBackColor = false;
            suaButton.BackColor = Color.FromArgb(0, 120, 215);
            suaButton.ForeColor = Color.White;
            suaButton.FlatStyle = FlatStyle.Flat;
            // 
            // themButton
            // 
            themButton.Location = new Point(100, 260);
            themButton.Margin = new Padding(3, 4, 3, 4);
            themButton.Name = "themButton";
            themButton.Size = new Size(137, 40);
            themButton.TabIndex = 17;
            themButton.Text = "Thêm";
            themButton.UseVisualStyleBackColor = false;
            themButton.BackColor = Color.FromArgb(0, 120, 215);
            themButton.ForeColor = Color.White;
            themButton.FlatStyle = FlatStyle.Flat;
            // 
            // timeSearchGroupBox
            // 
            timeSearchGroupBox.Controls.Add(label2);
            timeSearchGroupBox.Controls.Add(dateTimePicker3);
            timeSearchGroupBox.Controls.Add(dateTimePicker2);
            timeSearchGroupBox.Controls.Add(dateTimePicker1);
            timeSearchGroupBox.Controls.Add(khoangThoiGianRadioButton);
            timeSearchGroupBox.Controls.Add(ngayRadioButton);
            timeSearchGroupBox.Location = new Point(20, 120);
            timeSearchGroupBox.Name = "timeSearchGroupBox";
            timeSearchGroupBox.Size = new Size(750, 120);
            timeSearchGroupBox.TabIndex = 4;
            timeSearchGroupBox.TabStop = false;
            timeSearchGroupBox.Text = "Thời gian";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(467, 80);
            label2.Name = "label2";
            label2.Size = new Size(17, 23);
            label2.TabIndex = 7;
            label2.Text = "-";
            // 
            // dateTimePicker3
            // 
            dateTimePicker3.Location = new Point(490, 74);
            dateTimePicker3.Name = "dateTimePicker3";
            dateTimePicker3.Size = new Size(250, 30);
            dateTimePicker3.TabIndex = 6;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(200, 74);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(250, 30);
            dateTimePicker2.TabIndex = 5;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(200, 34);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 30);
            dateTimePicker1.TabIndex = 4;
            // 
            // khoangThoiGianRadioButton
            // 
            khoangThoiGianRadioButton.AutoSize = true;
            khoangThoiGianRadioButton.Location = new Point(14, 78);
            khoangThoiGianRadioButton.Name = "khoangThoiGianRadioButton";
            khoangThoiGianRadioButton.Size = new Size(163, 27);
            khoangThoiGianRadioButton.TabIndex = 1;
            khoangThoiGianRadioButton.TabStop = true;
            khoangThoiGianRadioButton.Text = "Khoảng thời gian";
            khoangThoiGianRadioButton.UseVisualStyleBackColor = true;
            // 
            // ngayRadioButton
            // 
            ngayRadioButton.AutoSize = true;
            ngayRadioButton.Location = new Point(14, 34);
            ngayRadioButton.Name = "ngayRadioButton";
            ngayRadioButton.Size = new Size(71, 27);
            ngayRadioButton.TabIndex = 0;
            ngayRadioButton.TabStop = true;
            ngayRadioButton.Text = "Ngày";
            ngayRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(800, 30);
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
            // 
            // customerSearchGroupBox
            // 
            customerSearchGroupBox.Controls.Add(customerSearchTextBox);
            customerSearchGroupBox.Location = new Point(540, 30);
            customerSearchGroupBox.Name = "customerSearchGroupBox";
            customerSearchGroupBox.Size = new Size(240, 68);
            customerSearchGroupBox.TabIndex = 2;
            customerSearchGroupBox.TabStop = false;
            customerSearchGroupBox.Text = "Khách hàng";
            // 
            // customerSearchTextBox
            // 
            customerSearchTextBox.Location = new Point(14, 27);
            customerSearchTextBox.Name = "customerSearchTextBox";
            customerSearchTextBox.Size = new Size(210, 30);
            customerSearchTextBox.TabIndex = 0;
            // 
            // employeeSearchGroupBox
            // 
            employeeSearchGroupBox.Controls.Add(employeeSearchTextBox);
            employeeSearchGroupBox.Location = new Point(280, 30);
            employeeSearchGroupBox.Name = "employeeSearchGroupBox";
            employeeSearchGroupBox.Size = new Size(240, 68);
            employeeSearchGroupBox.TabIndex = 1;
            employeeSearchGroupBox.TabStop = false;
            employeeSearchGroupBox.Text = "Nhân viên";
            // 
            // employeeSearchTextBox
            // 
            employeeSearchTextBox.Location = new Point(14, 27);
            employeeSearchTextBox.Name = "employeeSearchTextBox";
            employeeSearchTextBox.Size = new Size(210, 30);
            employeeSearchTextBox.TabIndex = 0;
            // 
            // idSearchGroupBox
            // 
            idSearchGroupBox.Controls.Add(idSearchTextBox);
            idSearchGroupBox.Location = new Point(20, 30);
            idSearchGroupBox.Name = "idSearchGroupBox";
            idSearchGroupBox.Size = new Size(240, 68);
            idSearchGroupBox.TabIndex = 0;
            idSearchGroupBox.TabStop = false;
            idSearchGroupBox.Text = "Mã hóa đơn";
            // 
            // idSearchTextBox
            // 
            idSearchTextBox.Location = new Point(14, 27);
            idSearchTextBox.Name = "idSearchTextBox";
            idSearchTextBox.Size = new Size(210, 30);
            idSearchTextBox.TabIndex = 0;
            // 
            // roleLabel
            // 
            roleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            roleLabel.AutoSize = true;
            roleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            roleLabel.Location = new Point(1020, 20);
            roleLabel.Name = "roleLabel";
            roleLabel.Size = new Size(122, 23);
            roleLabel.TabIndex = 4;
            roleLabel.Text = "Quyền: admin";
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
            hoaDonDataGridView.Columns.AddRange(new DataGridViewColumn[] { maHoaDonColumn, ngayLapColumn, nhanVienColumn, khachHangColumn, thanhTienColumn });
            hoaDonDataGridView.Dock = DockStyle.Fill;
            hoaDonDataGridView.Location = new Point(3, 27);
            hoaDonDataGridView.Margin = new Padding(3, 4, 3, 4);
            hoaDonDataGridView.MultiSelect = false;
            hoaDonDataGridView.Name = "hoaDonDataGridView";
            hoaDonDataGridView.ReadOnly = true;
            hoaDonDataGridView.RowHeadersVisible = false;
            hoaDonDataGridView.RowHeadersWidth = 51;
            hoaDonDataGridView.RowTemplate.Height = 30;
            hoaDonDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            hoaDonDataGridView.Size = new Size(1134, 418);
            hoaDonDataGridView.TabIndex = 0;
            // 
            // maHoaDonColumn
            // 
            maHoaDonColumn.DataPropertyName = "MaHoaDon";
            maHoaDonColumn.HeaderText = "Mã HĐ";
            maHoaDonColumn.MinimumWidth = 100;
            maHoaDonColumn.Name = "maHoaDonColumn";
            maHoaDonColumn.ReadOnly = true;
            // 
            // ngayLapColumn
            // 
            ngayLapColumn.DataPropertyName = "NgayLap";
            ngayLapColumn.HeaderText = "Ngày lập";
            ngayLapColumn.MinimumWidth = 6;
            ngayLapColumn.Name = "ngayLapColumn";
            ngayLapColumn.ReadOnly = true;
            // 
            // nhanVienColumn
            // 
            nhanVienColumn.DataPropertyName = "NhanVien";
            nhanVienColumn.HeaderText = "Nhân viên";
            nhanVienColumn.MinimumWidth = 6;
            nhanVienColumn.Name = "nhanVienColumn";
            nhanVienColumn.ReadOnly = true;
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
            // danhSachGroupBox
            // 
            danhSachGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            danhSachGroupBox.Controls.Add(hoaDonDataGridView);
            danhSachGroupBox.FlatStyle = FlatStyle.Flat;
            danhSachGroupBox.Font = new Font("Segoe UI", 10F);
            danhSachGroupBox.Location = new Point(20, 380);
            danhSachGroupBox.Margin = new Padding(3, 4, 3, 4);
            danhSachGroupBox.Name = "danhSachGroupBox";
            danhSachGroupBox.Padding = new Padding(3, 4, 3, 4);
            danhSachGroupBox.Size = new Size(1140, 450);
            danhSachGroupBox.TabIndex = 5;
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
            Controls.Add(roleLabel);
            Controls.Add(invoiceSearchGroupBox);
            Name = "Form_HoaDon";
            Text = "Quản lý hóa đơn";
            invoiceSearchGroupBox.ResumeLayout(false);
            invoiceSearchGroupBox.PerformLayout();
            timeSearchGroupBox.ResumeLayout(false);
            timeSearchGroupBox.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            customerSearchGroupBox.ResumeLayout(false);
            customerSearchGroupBox.PerformLayout();
            employeeSearchGroupBox.ResumeLayout(false);
            employeeSearchGroupBox.PerformLayout();
            idSearchGroupBox.ResumeLayout(false);
            idSearchGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)hoaDonDataGridView).EndInit();
            danhSachGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox? invoiceSearchGroupBox;
        private GroupBox? groupBox1;
        private TextBox? textBox2;
        private Label? label1;
        private TextBox? textBox1;
        private GroupBox? customerSearchGroupBox;
        private TextBox? customerSearchTextBox;
        private GroupBox? employeeSearchGroupBox;
        private TextBox? employeeSearchTextBox;
        private GroupBox? idSearchGroupBox;
        private TextBox? idSearchTextBox;
        private Label? roleLabel;
        private GroupBox? timeSearchGroupBox;
        private RadioButton? khoangThoiGianRadioButton;
        private RadioButton? ngayRadioButton;
        private Label? label2;
        private DateTimePicker? dateTimePicker3;
        private DateTimePicker? dateTimePicker2;
        private DateTimePicker? dateTimePicker1;
        private Button? lamMoiButton;
        private Button? xoaButton;
        private Button? suaButton;
        private Button? themButton;
        private Label? countInvoicesLabel;
        private Label? revenueLabel;
        private Button? thongKeButton;
        private DataGridView? hoaDonDataGridView;
        private GroupBox? danhSachGroupBox;
        private DataGridViewTextBoxColumn? maHoaDonColumn;
        private DataGridViewTextBoxColumn? ngayLapColumn;
        private DataGridViewTextBoxColumn? nhanVienColumn;
        private DataGridViewTextBoxColumn? khachHangColumn;
        private DataGridViewTextBoxColumn? thanhTienColumn;
    }
}