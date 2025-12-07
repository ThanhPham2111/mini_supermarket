
namespace mini_supermarket.GUI.HoaDon
{
    partial class Form_HoaDonDialog : Form
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
            maHoaDonLabel = new Label();
            ngayLabel = new Label();
            nhanVienLabel = new Label();
            khachHangLabel = new Label();
            titleDialogLabel = new Label();
            ngayText = new Label();
            maHoaDonText = new Label();
            nhanVienText = new Label();
            khachHangText = new Label();
            exportFileButton = new Button();
            danhSachGroupBox = new GroupBox();
            dataGridView1 = new DataGridView();
            tenSanPhamColumn = new DataGridViewTextBoxColumn();
            donViColumn = new DataGridViewTextBoxColumn();
            soLuongColumn = new DataGridViewTextBoxColumn();
            giaBanColumn = new DataGridViewTextBoxColumn();
            thanhTienColumn = new DataGridViewTextBoxColumn();
            danhSachGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // maHoaDonLabel
            // 
            maHoaDonLabel.AutoSize = true;
            maHoaDonLabel.Font = new Font("Segoe UI", 10F);
            maHoaDonLabel.Location = new Point(35, 113);
            maHoaDonLabel.Name = "maHoaDonLabel";
            maHoaDonLabel.Size = new Size(107, 23);
            maHoaDonLabel.TabIndex = 0;
            maHoaDonLabel.Text = "Mã hóa đơn:";
            // maHoaDonLabel.Click += maHoaDonLabel_Click;
            // 
            // ngayLabel
            // 
            ngayLabel.AutoSize = true;
            ngayLabel.Font = new Font("Segoe UI", 10F);
            ngayLabel.Location = new Point(290, 59);
            ngayLabel.Name = "ngayLabel";
            ngayLabel.Size = new Size(87, 23);
            ngayLabel.TabIndex = 1;
            ngayLabel.Text = "Ngày, giờ:";
            // ngayLabel.Click += ngayLabel_Click;
            // 
            // nhanVienLabel
            // 
            nhanVienLabel.AutoSize = true;
            nhanVienLabel.Font = new Font("Segoe UI", 10F);
            nhanVienLabel.Location = new Point(35, 156);
            nhanVienLabel.Name = "nhanVienLabel";
            nhanVienLabel.Size = new Size(92, 23);
            nhanVienLabel.TabIndex = 2;
            nhanVienLabel.Text = "Nhân viên:";
            // nhanVienLabel.Click += this.nhanVienLabel_Click;
            // 
            // khachHangLabel
            // 
            khachHangLabel.AutoSize = true;
            khachHangLabel.Font = new Font("Segoe UI", 10F);
            khachHangLabel.Location = new Point(35, 201);
            khachHangLabel.Name = "khachHangLabel";
            khachHangLabel.Size = new Size(105, 23);
            khachHangLabel.TabIndex = 3;
            khachHangLabel.Text = "Khách hàng:";
            // khachHangLabel.Click += khachHangLabel_Click;
            // 
            // titleDialogLabel
            // 
            titleDialogLabel.AutoSize = true;
            titleDialogLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleDialogLabel.Location = new Point(250, 13);
            titleDialogLabel.Name = "titleDialogLabel";
            titleDialogLabel.Size = new Size(374, 46);
            titleDialogLabel.TabIndex = 4;
            titleDialogLabel.Text = "HÓA ĐƠN BÁN HÀNG";
            // 
            // ngayText
            // 
            ngayText.AutoSize = true;
            ngayText.Font = new Font("Segoe UI", 10F);
            ngayText.Location = new Point(390, 59);
            ngayText.Name = "ngayText";
            ngayText.Size = new Size(141, 23);
            ngayText.TabIndex = 5;
            ngayText.Text = "22/11/2025 13:50";
            // 
            // maHoaDonText
            // 
            maHoaDonText.AutoSize = true;
            maHoaDonText.Font = new Font("Segoe UI", 10F);
            maHoaDonText.Location = new Point(163, 113);
            maHoaDonText.Name = "maHoaDonText";
            maHoaDonText.Size = new Size(19, 23);
            maHoaDonText.TabIndex = 6;
            maHoaDonText.Text = "1";
            // 
            // nhanVienText
            // 
            nhanVienText.AutoSize = true;
            nhanVienText.Font = new Font("Segoe UI", 10F);
            nhanVienText.Location = new Point(163, 156);
            nhanVienText.Name = "nhanVienText";
            nhanVienText.Size = new Size(120, 23);
            nhanVienText.TabIndex = 7;
            nhanVienText.Text = "Nguyễn Văn A";
            // 
            // khachHangText
            // 
            khachHangText.AutoSize = true;
            khachHangText.Font = new Font("Segoe UI", 10F);
            khachHangText.Location = new Point(163, 201);
            khachHangText.Name = "khachHangText";
            khachHangText.Size = new Size(92, 23);
            khachHangText.TabIndex = 8;
            khachHangText.Text = "Trần Văn B";
            // 
            // exportFileButton
            // 
            exportFileButton.BackColor = Color.FromArgb(33, 115, 70);
            exportFileButton.FlatStyle = FlatStyle.Flat;
            exportFileButton.ForeColor = Color.White;
            exportFileButton.Location = new Point(725, 96);
            exportFileButton.Margin = new Padding(3, 4, 3, 4);
            exportFileButton.Name = "exportFileButton";
            exportFileButton.Size = new Size(137, 40);
            exportFileButton.TabIndex = 28;
            exportFileButton.Text = "Export Excel";
            exportFileButton.UseVisualStyleBackColor = false;
            exportFileButton.Click += exportFileButton_Click;
            // 
            // danhSachGroupBox
            // 
            danhSachGroupBox.Controls.Add(dataGridView1);
            danhSachGroupBox.Location = new Point(33, 245);
            danhSachGroupBox.Name = "danhSachGroupBox";
            danhSachGroupBox.Size = new Size(829, 249);
            danhSachGroupBox.TabIndex = 29;
            danhSachGroupBox.TabStop = false;
            danhSachGroupBox.Text = "Danh sách sản phẩm";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { tenSanPhamColumn, donViColumn, soLuongColumn, giaBanColumn, thanhTienColumn });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 26);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(823, 220);
            dataGridView1.TabIndex = 0;
            // 
            // tenSanPhamColumn
            // 
            tenSanPhamColumn.DataPropertyName = "TenSanPham";
            tenSanPhamColumn.HeaderText = "Tên sản phẩm";
            tenSanPhamColumn.MinimumWidth = 6;
            tenSanPhamColumn.Name = "tenSanPhamColumn";
            // 
            // donViColumn
            // 
            donViColumn.DataPropertyName = "DonVi";
            donViColumn.HeaderText = "Đơn vị";
            donViColumn.MinimumWidth = 6;
            donViColumn.Name = "donViColumn";
            // 
            // soLuongColumn
            // 
            soLuongColumn.DataPropertyName = "SoLuong";
            soLuongColumn.HeaderText = "Số lượng";
            soLuongColumn.MinimumWidth = 6;
            soLuongColumn.Name = "soLuongColumn";
            // 
            // giaBanColumn
            // 
            giaBanColumn.DataPropertyName = "GiaBan";
            giaBanColumn.HeaderText = "Giá bán";
            giaBanColumn.MinimumWidth = 6;
            giaBanColumn.Name = "giaBanColumn";
            // 
            // thanhTienColumn
            // 
            thanhTienColumn.DataPropertyName = "ThanhTien";
            thanhTienColumn.HeaderText = "Thành tiền";
            thanhTienColumn.MinimumWidth = 6;
            thanhTienColumn.Name = "thanhTienColumn";
            // 
            // Form_HoaDonDialog
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 518);
            Controls.Add(danhSachGroupBox);
            Controls.Add(exportFileButton);
            Controls.Add(khachHangText);
            Controls.Add(nhanVienText);
            Controls.Add(maHoaDonText);
            Controls.Add(ngayText);
            Controls.Add(titleDialogLabel);
            Controls.Add(khachHangLabel);
            Controls.Add(nhanVienLabel);
            Controls.Add(ngayLabel);
            Controls.Add(maHoaDonLabel);
            Font = new Font("Segoe UI", 10F);
            Name = "Form_HoaDonDialog";
            Text = "Form_HoaDonDialog";
            StartPosition = FormStartPosition.CenterParent;
            // Load += Form_HoaDonDialog_Load;
            danhSachGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label maHoaDonLabel;
        private Label ngayLabel;
        private Label nhanVienLabel;
        private Label khachHangLabel;
        private Label titleDialogLabel;
        private Label ngayText;
        private Label maHoaDonText;
        private Label nhanVienText;
        private Label khachHangText;
        private Button exportFileButton;
        private GroupBox danhSachGroupBox;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn tenSanPhamColumn;
        private DataGridViewTextBoxColumn donViColumn;
        private DataGridViewTextBoxColumn soLuongColumn;
        private DataGridViewTextBoxColumn giaBanColumn;
        private DataGridViewTextBoxColumn thanhTienColumn;
    }
}