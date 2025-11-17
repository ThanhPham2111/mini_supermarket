namespace mini_supermarket.GUI.Form_BanHang
{
    partial class Dialog_ChonKhachHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelChonKH = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearchKH = new System.Windows.Forms.TextBox();
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnChon = new System.Windows.Forms.Button();
            this.panelChonKH.SuspendLayout();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChonKH
            // 
            this.panelChonKH.BackColor = System.Drawing.Color.White;
            this.panelChonKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChonKH.Controls.Add(this.buttonPanel);
            this.panelChonKH.Controls.Add(this.dgvKhachHang);
            this.panelChonKH.Controls.Add(this.searchPanel);
            this.panelChonKH.Controls.Add(this.lblTitle);
            this.panelChonKH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChonKH.Location = new System.Drawing.Point(0, 0);
            this.panelChonKH.Name = "panelChonKH";
            this.panelChonKH.Padding = new System.Windows.Forms.Padding(20);
            this.panelChonKH.Size = new System.Drawing.Size(1100, 750);
            this.panelChonKH.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblTitle.Location = new System.Drawing.Point(23, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Danh sách Khách hàng";
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.txtSearchKH);
            this.searchPanel.Controls.Add(this.lblSearch);
            this.searchPanel.Location = new System.Drawing.Point(23, 68);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(1040, 50);
            this.searchPanel.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.lblSearch.Location = new System.Drawing.Point(3, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(80, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // txtSearchKH
            // 
            this.txtSearchKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchKH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearchKH.Location = new System.Drawing.Point(93, 13);
            this.txtSearchKH.Name = "txtSearchKH";
            this.txtSearchKH.Size = new System.Drawing.Size(940, 27);
            this.txtSearchKH.TabIndex = 1;
            this.txtSearchKH.TextChanged += new System.EventHandler(this.TxtSearchKH_TextChanged);
            // 
            // dgvKhachHang
            // 
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.AllowUserToDeleteRows = false;
            this.dgvKhachHang.AutoGenerateColumns = false;
            this.dgvKhachHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvKhachHang.ColumnHeadersHeight = 35;
            this.dgvKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvKhachHang.Location = new System.Drawing.Point(23, 128);
            this.dgvKhachHang.MultiSelect = false;
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.RowHeadersVisible = false;
            this.dgvKhachHang.RowTemplate.Height = 30;
            this.dgvKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.Size = new System.Drawing.Size(1040, 520);
            this.dgvKhachHang.TabIndex = 2;
            this.dgvKhachHang.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvKhachHang_CellDoubleClick);
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.White;
            this.buttonPanel.Controls.Add(this.btnHuy);
            this.buttonPanel.Controls.Add(this.btnChon);
            this.buttonPanel.Location = new System.Drawing.Point(23, 658);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(1040, 60);
            this.buttonPanel.TabIndex = 3;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(830, 12);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 40);
            this.btnHuy.TabIndex = 0;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // btnChon
            // 
            this.btnChon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnChon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChon.FlatAppearance.BorderSize = 0;
            this.btnChon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnChon.ForeColor = System.Drawing.Color.White;
            this.btnChon.Location = new System.Drawing.Point(940, 12);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(100, 40);
            this.btnChon.TabIndex = 1;
            this.btnChon.Text = "Chọn";
            this.btnChon.UseVisualStyleBackColor = false;
            this.btnChon.Click += new System.EventHandler(this.BtnChon_Click);
            // 
            // Dialog_ChonKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1100, 750);
            this.Controls.Add(this.panelChonKH);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_ChonKhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn Khách Hàng";
            this.panelChonKH.ResumeLayout(false);
            this.panelChonKH.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

            // Setup DataGridView columns
            SetupDataGridViewColumns();
            
            // Setup DataGridView styles
            SetupDataGridViewStyles();
        }

        private void SetupDataGridViewColumns()
        {
            this.dgvKhachHang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                HeaderText = "Mã KH",
                DataPropertyName = "MaKhachHang",
                Width = 100,
                ReadOnly = true
            });
            this.dgvKhachHang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                HeaderText = "Tên khách hàng",
                DataPropertyName = "TenKhachHang",
                Width = 250,
                ReadOnly = true
            });
            this.dgvKhachHang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                HeaderText = "Số điện thoại",
                DataPropertyName = "SoDienThoai",
                Width = 150,
                ReadOnly = true
            });
            this.dgvKhachHang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                HeaderText = "Địa chỉ",
                DataPropertyName = "DiaChi",
                Width = 250,
                ReadOnly = true
            });
            this.dgvKhachHang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                HeaderText = "Điểm tích lũy",
                DataPropertyName = "DiemTichLuy",
                Width = 140,
                ReadOnly = true
            });
            this.dgvKhachHang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                Width = 150,
                ReadOnly = true
            });
        }

        private void SetupDataGridViewStyles()
        {
            // Alternating rows
            this.dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            
            // Default cell style
            this.dgvKhachHang.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvKhachHang.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.dgvKhachHang.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(238)))), ((int)(((byte)(144)))));
            this.dgvKhachHang.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            
            // Column headers
            this.dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvKhachHang.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            
            // Other settings
            this.dgvKhachHang.EnableHeadersVisualStyles = false;
            this.dgvKhachHang.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvKhachHang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
        }

        private System.Windows.Forms.Panel panelChonKH;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearchKH;
        private System.Windows.Forms.DataGridView dgvKhachHang;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnChon;
    }
}

