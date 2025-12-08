namespace mini_supermarket.GUI.KhoHang {
    partial class Form_KhoHang {
        private System.ComponentModel.IContainer components = null;
        
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        
        #region Windows Form Designer generated code
        
        private void InitializeComponent() {
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxGrid = new System.Windows.Forms.GroupBox();
            this.dgvKhoHang = new System.Windows.Forms.DataGridView();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.tblFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoaiSP = new System.Windows.Forms.ComboBox();
            this.lblThuongHieu = new System.Windows.Forms.Label();
            this.cboThuongHieu = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.headerActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXemLichSu = new System.Windows.Forms.Button();
            this.btnNhapExcel = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnXuatFileMau = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.groupBoxGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhoHang)).BeginInit();
            this.panelFilters.SuspendLayout();
            this.tblFilters.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.headerActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMain.Controls.Add(this.groupBoxGrid);
            this.panelMain.Controls.Add(this.panelFilters);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(10);
            this.panelMain.Size = new System.Drawing.Size(1200, 720);
            this.panelMain.TabIndex = 0;
            // 
            // groupBoxGrid
            // 
            this.groupBoxGrid.Controls.Add(this.dgvKhoHang);
            this.groupBoxGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGrid.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxGrid.Location = new System.Drawing.Point(10, 154);
            this.groupBoxGrid.Name = "groupBoxGrid";
            this.groupBoxGrid.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxGrid.Size = new System.Drawing.Size(1180, 556);
            this.groupBoxGrid.TabIndex = 2;
            this.groupBoxGrid.TabStop = false;
            this.groupBoxGrid.Text = "Danh sách tồn kho";
            // 
            // dgvKhoHang
            // 
            this.dgvKhoHang.AllowUserToAddRows = false;
            this.dgvKhoHang.AllowUserToDeleteRows = false;
            this.dgvKhoHang.AllowUserToResizeRows = false;
            this.dgvKhoHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhoHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhoHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKhoHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhoHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhoHang.Location = new System.Drawing.Point(8, 24);
            this.dgvKhoHang.MultiSelect = false;
            this.dgvKhoHang.Name = "dgvKhoHang";
            this.dgvKhoHang.ReadOnly = true;
            this.dgvKhoHang.RowHeadersVisible = false;
            this.dgvKhoHang.RowTemplate.Height = 30;
            this.dgvKhoHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhoHang.Size = new System.Drawing.Size(1164, 524);
            this.dgvKhoHang.TabIndex = 7;
            this.dgvKhoHang.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhoHang_CellDoubleClick);
            this.dgvKhoHang.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvKhoHang_CellFormatting);
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.White;
            this.panelFilters.Controls.Add(this.tblFilters);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(10, 64);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(5);
            this.panelFilters.Size = new System.Drawing.Size(1180, 90);
            this.panelFilters.TabIndex = 1;
            // 
            // tblFilters
            // 
            this.tblFilters.ColumnCount = 4;
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilters.Controls.Add(this.lblLoai, 0, 0);
            this.tblFilters.Controls.Add(this.cboLoaiSP, 1, 0);
            this.tblFilters.Controls.Add(this.lblThuongHieu, 2, 0);
            this.tblFilters.Controls.Add(this.cboThuongHieu, 3, 0);
            this.tblFilters.Controls.Add(this.lblTrangThai, 0, 1);
            this.tblFilters.Controls.Add(this.cboTrangThai, 1, 1);
            this.tblFilters.Controls.Add(this.lblTimKiem, 2, 1);
            this.tblFilters.Controls.Add(this.txtTimKiem, 3, 1);
            this.tblFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFilters.Location = new System.Drawing.Point(5, 5);
            this.tblFilters.Name = "tblFilters";
            this.tblFilters.RowCount = 2;
            this.tblFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilters.Size = new System.Drawing.Size(1170, 80);
            this.tblFilters.TabIndex = 0;
            // 
            // lblLoai
            // 
            this.lblLoai.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLoai.AutoSize = true;
            this.lblLoai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLoai.Location = new System.Drawing.Point(3, 10);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(103, 20);
            this.lblLoai.TabIndex = 0;
            this.lblLoai.Text = "Loại sản phẩm";
            // 
            // cboLoaiSP
            // 
            this.cboLoaiSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboLoaiSP.FormattingEnabled = true;
            this.cboLoaiSP.Location = new System.Drawing.Point(123, 6);
            this.cboLoaiSP.Name = "cboLoaiSP";
            this.cboLoaiSP.Size = new System.Drawing.Size(459, 28);
            this.cboLoaiSP.TabIndex = 1;
            this.cboLoaiSP.SelectedIndexChanged += new System.EventHandler(this.cboLoaiSP_SelectedIndexChanged);
            // 
            // lblThuongHieu
            // 
            this.lblThuongHieu.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblThuongHieu.AutoSize = true;
            this.lblThuongHieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblThuongHieu.Location = new System.Drawing.Point(588, 10);
            this.lblThuongHieu.Name = "lblThuongHieu";
            this.lblThuongHieu.Size = new System.Drawing.Size(91, 20);
            this.lblThuongHieu.TabIndex = 2;
            this.lblThuongHieu.Text = "Thương hiệu";
            // 
            // cboThuongHieu
            // 
            this.cboThuongHieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboThuongHieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThuongHieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboThuongHieu.FormattingEnabled = true;
            this.cboThuongHieu.Location = new System.Drawing.Point(708, 6);
            this.cboThuongHieu.Name = "cboThuongHieu";
            this.cboThuongHieu.Size = new System.Drawing.Size(459, 28);
            this.cboThuongHieu.TabIndex = 3;
            this.cboThuongHieu.SelectedIndexChanged += new System.EventHandler(this.cboThuongHieu_SelectedIndexChanged);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTrangThai.Location = new System.Drawing.Point(3, 50);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(75, 20);
            this.lblTrangThai.TabIndex = 4;
            this.lblTrangThai.Text = "Trạng thái";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(123, 46);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(459, 28);
            this.cboTrangThai.TabIndex = 5;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.cboTrangThai_SelectedIndexChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTimKiem.Location = new System.Drawing.Point(588, 50);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(70, 20);
            this.lblTimKiem.TabIndex = 6;
            this.lblTimKiem.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTimKiem.Location = new System.Drawing.Point(708, 46);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm kiếm theo mã hoặc tên sản phẩm";
            this.txtTimKiem.Size = new System.Drawing.Size(459, 27);
            this.txtTimKiem.TabIndex = 7;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.headerActions);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(10, 10);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1180, 54);
            this.panelHeader.TabIndex = 0;
            // 
            // headerActions
            // 
            this.headerActions.Controls.Add(this.btnLamMoi);
            this.headerActions.Controls.Add(this.btnSua);
            this.headerActions.Controls.Add(this.btnXemLichSu);
            // this.headerActions.Controls.Add(this.btnNhapExcel);
            this.headerActions.Controls.Add(this.btnXuatExcel);
            // this.headerActions.Controls.Add(this.btnXuatFileMau);
            this.headerActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerActions.Location = new System.Drawing.Point(0, 0);
            this.headerActions.Name = "headerActions";
            this.headerActions.Padding = new System.Windows.Forms.Padding(5);
            this.headerActions.Size = new System.Drawing.Size(1180, 54);
            this.headerActions.TabIndex = 0;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(8, 8);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 35);
            this.btnLamMoi.TabIndex = 0;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(124, 8);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(110, 35);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Điều chỉnh";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXemLichSu
            // 
            this.btnXemLichSu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnXemLichSu.FlatAppearance.BorderSize = 0;
            this.btnXemLichSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemLichSu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnXemLichSu.ForeColor = System.Drawing.Color.White;
            this.btnXemLichSu.Location = new System.Drawing.Point(240, 8);
            this.btnXemLichSu.Name = "btnXemLichSu";
            this.btnXemLichSu.Size = new System.Drawing.Size(110, 35);
            this.btnXemLichSu.TabIndex = 5;
            this.btnXemLichSu.Text = "Xem lịch sử";
            this.btnXemLichSu.UseVisualStyleBackColor = false;
            this.btnXemLichSu.Click += new System.EventHandler(this.btnXemLichSu_Click);
            // 
            // btnNhapExcel
            // 
            this.btnNhapExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNhapExcel.FlatAppearance.BorderSize = 0;
            this.btnNhapExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNhapExcel.ForeColor = System.Drawing.Color.White;
            this.btnNhapExcel.Location = new System.Drawing.Point(356, 8);
            this.btnNhapExcel.Name = "btnNhapExcel";
            this.btnNhapExcel.Size = new System.Drawing.Size(110, 35);
            this.btnNhapExcel.TabIndex = 3;
            this.btnNhapExcel.Text = "Nhập Excel";
            this.btnNhapExcel.UseVisualStyleBackColor = false;
            this.btnNhapExcel.Click += new System.EventHandler(this.btnNhapExcel_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(126)))), ((int)(((byte)(20)))));
            this.btnXuatExcel.FlatAppearance.BorderSize = 0;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(356, 8);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(110, 35);
            this.btnXuatExcel.TabIndex = 2;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXuatFileMau
            // 
            this.btnXuatFileMau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXuatFileMau.FlatAppearance.BorderSize = 0;
            this.btnXuatFileMau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatFileMau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnXuatFileMau.ForeColor = System.Drawing.Color.White;
            this.btnXuatFileMau.Location = new System.Drawing.Point(588, 8);
            this.btnXuatFileMau.Name = "btnXuatFileMau";
            this.btnXuatFileMau.Size = new System.Drawing.Size(140, 35);
            this.btnXuatFileMau.TabIndex = 4;
            this.btnXuatFileMau.Text = "Tải file mẫu";
            this.btnXuatFileMau.UseVisualStyleBackColor = false;
            this.btnXuatFileMau.Click += new System.EventHandler(this.btnXuatFileMau_Click);
            // 
            // Form_KhoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.panelMain);
            this.Name = "Form_KhoHang";
            this.Text = "Quản lý Kho hàng";
            this.Load += new System.EventHandler(this.Form_KhoHang_Load);
            this.panelMain.ResumeLayout(false);
            this.groupBoxGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhoHang)).EndInit();
            this.panelFilters.ResumeLayout(false);
            this.tblFilters.ResumeLayout(false);
            this.tblFilters.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.headerActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        
        #endregion
        
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.TableLayoutPanel tblFilters;
        private System.Windows.Forms.FlowLayoutPanel headerActions;
        private System.Windows.Forms.GroupBox groupBoxGrid;
        
        private System.Windows.Forms.ComboBox cboLoaiSP;
        private System.Windows.Forms.ComboBox cboThuongHieu;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvKhoHang;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnNhapExcel;
        private System.Windows.Forms.Button btnXuatFileMau;
        private System.Windows.Forms.Button btnXemLichSu;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.Label lblThuongHieu;
        private System.Windows.Forms.Label lblTrangThai;
    }
}
