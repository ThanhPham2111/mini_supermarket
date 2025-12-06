// Below is a redesigned, cleaner, more balanced WinForms layout for Form_TrangChu.
// This version reduces duplicate settings, fixes inconsistent padding/margins,
// improves proportional layout, and unifies styling.

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting; // Thêm cho Chart

namespace mini_supermarket.GUI.TrangChu
{
    partial class Form_TrangChu
    {
        private System.ComponentModel.IContainer components = null;

        // Khai báo các controls (thêm Chart, ToolTip, và top khách hàng)
        private TableLayoutPanel panelMain; // Thay Panel bằng TableLayoutPanel cho responsive
        private Button btnRefresh; // floating icon
        private ToolTip toolTip1; // Thêm ToolTip

        private FlowLayoutPanel panelKPI;
        private Panel panelDoanhThu;
        private Label lblDoanhThuTitle;
        private Label lblDoanhThuValue;
        private Label lblDoanhThuIcon;

        private Panel panelSoHoaDon;
        private Label lblSoHoaDonTitle;
        private Label lblSoHoaDonValue;
        private Label lblSoHoaDonIcon;

        private Panel panelHangHet;
        private Label lblHangHetTitle;
        private Label lblHangHetValue;
        private Label lblHangHetIcon;

        private TableLayoutPanel panelCharts;
        private Chart chartDoanhThu7Ngay;
        private Chart chartTop5BanChay;

        private Panel panelDataGrid;
        private Panel panelDataGridHetHan;
        private Panel panelDataGridHeader;
        private Label lblSapHetHanTitle;
        private DataGridView dgvSanPhamSapHetHan;

        private Panel panelDataGridHetHanHeader;
        private Label lblDaHetHanTitle;
        private DataGridView dgvSanPhamDaHetHan;

        private Panel panelTopKhachHang;
        private Panel panelTopKhachHangHeader;
        private Label lblTopKhachHangTitle;
        private DataGridView dgvTopKhachHang;

        private Panel panelRefreshBar; // Panel chứa nút refresh

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
            this.components = new System.ComponentModel.Container();

            this.panelMain = new TableLayoutPanel();
            this.btnRefresh = new Button();
            this.toolTip1 = new ToolTip(this.components);
            this.panelKPI = new FlowLayoutPanel();
            this.panelCharts = new TableLayoutPanel();
            this.chartDoanhThu7Ngay = new Chart();
            this.chartTop5BanChay = new Chart();
            this.panelDataGrid = new Panel();
            this.panelDataGridHetHan = new Panel();
            this.panelDataGridHeader = new Panel();
            this.lblSapHetHanTitle = new Label();
            this.dgvSanPhamSapHetHan = new DataGridView();
            this.panelDataGridHetHanHeader = new Panel();
            this.lblDaHetHanTitle = new Label();
            this.dgvSanPhamDaHetHan = new DataGridView();
            this.panelTopKhachHang = new Panel();
            this.panelTopKhachHangHeader = new Panel();
            this.lblTopKhachHangTitle = new Label();
            this.dgvTopKhachHang = new DataGridView();
            this.panelRefreshBar = new Panel();

            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamSapHetHan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu7Ngay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTop5BanChay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamDaHetHan)).BeginInit();

            // 
            // panelMain (TableLayoutPanel cho responsive)
            // 
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = Color.WhiteSmoke;
            this.panelMain.Padding = new Padding(10);
            this.panelMain.AutoScroll = true;
            this.panelMain.RowCount = 7;
            this.panelMain.ColumnCount = 1;
            this.panelMain.RowStyles.Clear();
            this.panelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 48)); // Refresh bar
            this.panelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 110)); // KPI
            this.panelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 320)); // Charts
            this.panelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 250)); // DataGrid sản phẩm sắp hết hạn
            this.panelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 250)); // DataGrid sản phẩm đã hết hạn
            this.panelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 200)); // Top khách hàng
            this.panelMain.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // cảnh báo
            this.panelMain.Controls.Add(this.panelRefreshBar, 0, 0); // refresh bar lên đầu
            this.panelMain.Controls.Add(this.panelKPI, 0, 1);
            this.panelMain.Controls.Add(this.panelCharts, 0, 2);
            this.panelMain.Controls.Add(this.panelDataGrid, 0, 3);
            this.panelMain.Controls.Add(this.panelDataGridHetHan, 0, 4);
            this.panelMain.Controls.Add(this.panelTopKhachHang, 0, 5);
            this.panelMain.Resize += new EventHandler(this.panelMain_Resize);

            // 
            // btnRefresh (floating icon, với loading feedback)
            // 
            this.btnRefresh.Dock = DockStyle.Right;
            this.btnRefresh.Size = new Size(40, 32);
            this.btnRefresh.TextAlign = ContentAlignment.MiddleCenter;
            this.btnRefresh.Location = new Point(panelRefreshBar.Width - 52, 8);
            this.btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.BackColor = Color.Transparent;
            this.btnRefresh.ForeColor = Color.FromArgb(0, 120, 215);
            this.btnRefresh.Font = new Font("Segoe MDL2 Assets", 14F, FontStyle.Regular, GraphicsUnit.Point);
            this.btnRefresh.Text = "\uE72C";
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
            this.toolTip1.SetToolTip(this.btnRefresh, "Làm mới dữ liệu");

            // 
            // panelRefreshBar
            // 
            this.panelRefreshBar.Height = 48;
            this.panelRefreshBar.Dock = DockStyle.Top;
            this.panelRefreshBar.BackColor = Color.WhiteSmoke;
            this.panelRefreshBar.Padding = new Padding(0, 8, 12, 8);
            this.panelRefreshBar.Margin = new Padding(0);
            this.panelRefreshBar.Controls.Add(this.btnRefresh);

            // 
            // panelKPI
            // 
            this.panelKPI.Dock = DockStyle.Fill;
            this.panelKPI.Height = 110;
            this.panelKPI.FlowDirection = FlowDirection.LeftToRight;
            this.panelKPI.WrapContents = false;
            this.panelKPI.BackColor = Color.WhiteSmoke;
            this.panelKPI.Padding = new Padding(0, 10, 0, 10);

            // Tạo KPI panels với icon
            this.panelDoanhThu = CreateKpiPanel("DOANH THU HÔM NAY", out this.lblDoanhThuTitle, out this.lblDoanhThuValue, out this.lblDoanhThuIcon, Color.FromArgb(0, 120, 215), "\uE8D2");
            this.panelSoHoaDon = CreateKpiPanel("SỐ HÓA ĐƠN HÔM NAY", out this.lblSoHoaDonTitle, out this.lblSoHoaDonValue, out this.lblSoHoaDonIcon, Color.FromArgb(16, 137, 62), "\uE7C3");
            this.panelHangHet = CreateKpiPanel("SỐ HÀNG ĐÃ HẾT", out this.lblHangHetTitle, out this.lblHangHetValue, out this.lblHangHetIcon, Color.FromArgb(220, 53, 69), "\uEA6A");

            this.lblDoanhThuValue.Text = "0 đ";
            this.lblSoHoaDonValue.Text = "0";
            this.lblHangHetValue.Text = "0";

            this.panelKPI.Controls.Add(this.panelDoanhThu);
            this.panelKPI.Controls.Add(this.panelSoHoaDon);
            this.panelKPI.Controls.Add(this.panelHangHet);

            // 
            // panelCharts
            // 
            this.panelCharts.Dock = DockStyle.Fill;
            this.panelCharts.Height = 320;
            this.panelCharts.ColumnCount = 2;
            this.panelCharts.RowCount = 1;
            this.panelCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.panelCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.panelCharts.Controls.Add(this.chartDoanhThu7Ngay, 0, 0);
            this.panelCharts.Controls.Add(this.chartTop5BanChay, 1, 0);
            this.panelCharts.BackColor = Color.WhiteSmoke;
            this.panelCharts.Padding = new Padding(0, 10, 0, 10);

            // 
            // chartDoanhThu7Ngay
            // 
            this.chartDoanhThu7Ngay.Dock = DockStyle.Fill;
            this.chartDoanhThu7Ngay.BackColor = Color.White;
            this.chartDoanhThu7Ngay.BorderlineColor = Color.LightGray;
            this.chartDoanhThu7Ngay.BorderlineWidth = 1;

            // 
            // chartTop5BanChay
            // 
            this.chartTop5BanChay.Dock = DockStyle.Fill;
            this.chartTop5BanChay.BackColor = Color.White;
            this.chartTop5BanChay.BorderlineColor = Color.LightGray;
            this.chartTop5BanChay.BorderlineWidth = 1;

            // 
            // panelDataGrid
            // 
            this.panelDataGrid.Dock = DockStyle.Fill;
            this.panelDataGrid.Height = 250;
            this.panelDataGrid.BackColor = Color.White;
            this.panelDataGrid.Padding = new Padding(15);
            this.panelDataGrid.AutoScroll = true;
            this.panelDataGrid.Controls.Add(this.dgvSanPhamSapHetHan);
            this.panelDataGrid.Controls.Add(this.panelDataGridHeader);

            // 
            // panelDataGridHeader
            // 
            this.panelDataGridHeader.Dock = DockStyle.Top;
            this.panelDataGridHeader.Height = 35;
            this.panelDataGridHeader.BackColor = Color.White;
            this.panelDataGridHeader.Padding = new Padding(0);
            this.panelDataGridHeader.Controls.Add(this.lblSapHetHanTitle);

            // 
            // lblSapHetHanTitle
            // 
            this.lblSapHetHanTitle.Text = "Sản Phẩm Sắp Hết Hạn (7 Ngày)";
            this.lblSapHetHanTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblSapHetHanTitle.Dock = DockStyle.Left;
            this.lblSapHetHanTitle.Width = 350;
            this.lblSapHetHanTitle.ForeColor = Color.FromArgb(33, 37, 41);
            this.lblSapHetHanTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // dgvSanPhamSapHetHan
            // 
            this.dgvSanPhamSapHetHan.Dock = DockStyle.Fill;
            this.dgvSanPhamSapHetHan.BackgroundColor = Color.White;
            this.dgvSanPhamSapHetHan.BorderStyle = BorderStyle.None;
            this.dgvSanPhamSapHetHan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPhamSapHetHan.ReadOnly = true;
            this.dgvSanPhamSapHetHan.RowHeadersVisible = false;
            this.dgvSanPhamSapHetHan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPhamSapHetHan.AllowUserToAddRows = false;
            this.dgvSanPhamSapHetHan.AllowUserToDeleteRows = false;
            this.dgvSanPhamSapHetHan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            this.dgvSanPhamSapHetHan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            this.dgvSanPhamSapHetHan.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            this.dgvSanPhamSapHetHan.GridColor = Color.LightGray;
            this.dgvSanPhamSapHetHan.ScrollBars = ScrollBars.Vertical;

            // 
            // panelDataGridHetHan
            // 
            this.panelDataGridHetHan.Dock = DockStyle.Fill;
            this.panelDataGridHetHan.Height = 250;
            this.panelDataGridHetHan.BackColor = Color.White;
            this.panelDataGridHetHan.Padding = new Padding(15);
            this.panelDataGridHetHan.AutoScroll = true;
            this.panelDataGridHetHan.Controls.Add(this.dgvSanPhamDaHetHan);
            this.panelDataGridHetHan.Controls.Add(this.panelDataGridHetHanHeader);

            // 
            // panelDataGridHetHanHeader
            // 
            this.panelDataGridHetHanHeader.Dock = DockStyle.Top;
            this.panelDataGridHetHanHeader.Height = 35;
            this.panelDataGridHetHanHeader.BackColor = Color.White;
            this.panelDataGridHetHanHeader.Padding = new Padding(0);
            this.panelDataGridHetHanHeader.Controls.Add(this.lblDaHetHanTitle);

            // 
            // lblDaHetHanTitle
            // 
            this.lblDaHetHanTitle.Text = "Các Sản Phẩm Đã Hết Hạn";
            this.lblDaHetHanTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblDaHetHanTitle.Dock = DockStyle.Left;
            this.lblDaHetHanTitle.Width = 350;
            this.lblDaHetHanTitle.ForeColor = Color.FromArgb(33, 37, 41);
            this.lblDaHetHanTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // dgvSanPhamDaHetHan
            // 
            this.dgvSanPhamDaHetHan.Dock = DockStyle.Fill;
            this.dgvSanPhamDaHetHan.BackgroundColor = Color.White;
            this.dgvSanPhamDaHetHan.BorderStyle = BorderStyle.None;
            this.dgvSanPhamDaHetHan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPhamDaHetHan.ReadOnly = true;
            this.dgvSanPhamDaHetHan.RowHeadersVisible = false;
            this.dgvSanPhamDaHetHan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPhamDaHetHan.AllowUserToAddRows = false;
            this.dgvSanPhamDaHetHan.AllowUserToDeleteRows = false;
            this.dgvSanPhamDaHetHan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            this.dgvSanPhamDaHetHan.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            this.dgvSanPhamDaHetHan.GridColor = Color.LightGray;
            this.dgvSanPhamDaHetHan.ScrollBars = ScrollBars.Vertical;

            // 
            // panelTopKhachHang
            // 
            this.panelTopKhachHang.Dock = DockStyle.Fill;
            this.panelTopKhachHang.Height = 200;
            this.panelTopKhachHang.BackColor = Color.White;
            this.panelTopKhachHang.Padding = new Padding(15);
            this.panelTopKhachHang.AutoScroll = true;
            this.panelTopKhachHang.Controls.Add(this.dgvTopKhachHang);
            this.panelTopKhachHang.Controls.Add(this.panelTopKhachHangHeader);

            // 
            // panelTopKhachHangHeader
            // 
            this.panelTopKhachHangHeader.Dock = DockStyle.Top;
            this.panelTopKhachHangHeader.Height = 30;
            this.panelTopKhachHangHeader.BackColor = Color.White;
            this.panelTopKhachHangHeader.Padding = new Padding(0);
            this.panelTopKhachHangHeader.Controls.Add(this.lblTopKhachHangTitle);

            // 
            // lblTopKhachHangTitle
            // 
            this.lblTopKhachHangTitle.Text = "Top 5 Khách Hàng Mua Nhiều Nhất";
            this.lblTopKhachHangTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTopKhachHangTitle.Dock = DockStyle.Left;
            this.lblTopKhachHangTitle.Width = 300;
            this.lblTopKhachHangTitle.ForeColor = Color.FromArgb(33, 37, 41);
            this.lblTopKhachHangTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // dgvTopKhachHang
            // 
            this.dgvTopKhachHang.Dock = DockStyle.Fill;
            this.dgvTopKhachHang.BackgroundColor = Color.White;
            this.dgvTopKhachHang.BorderStyle = BorderStyle.None;
            this.dgvTopKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopKhachHang.ReadOnly = true;
            this.dgvTopKhachHang.RowHeadersVisible = false;
            this.dgvTopKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopKhachHang.AllowUserToAddRows = false;
            this.dgvTopKhachHang.AllowUserToDeleteRows = false;
            this.dgvTopKhachHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            this.dgvTopKhachHang.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            this.dgvTopKhachHang.GridColor = Color.LightGray;
            this.dgvTopKhachHang.ScrollBars = ScrollBars.Vertical;

            // 
            // Form_TrangChu
            // 
            this.ClientSize = new Size(1200, 800);
            this.MinimumSize = new Size(800, 600);
            this.Controls.Add(this.panelMain);
            this.Text = "Trang Chủ";
            this.BackColor = Color.WhiteSmoke;
            this.Load += new EventHandler(this.Form_TrangChu_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamSapHetHan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu7Ngay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTop5BanChay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamDaHetHan)).EndInit();
            this.ResumeLayout(false);
        }

        private Panel CreateKpiPanel(string title, out Label titleLbl, out Label valueLbl, out Label iconLbl, Color valueColor, string iconText)
        {
            Panel panel = new Panel();
            panel.Width = 380;
            panel.Height = 100;
            panel.BackColor = Color.White;
            panel.Margin = new Padding(5); // Adjust margin to ensure spacing
            panel.Padding = new Padding(12);
            panel.BorderStyle = BorderStyle.None; // Remove border completely

            iconLbl = new Label();
            iconLbl.Text = iconText;
            iconLbl.Font = new Font("Segoe MDL2 Assets", 24F);
            iconLbl.ForeColor = valueColor;
            iconLbl.Location = new Point(12, 15);
            iconLbl.Size = new Size(40, 40);
            panel.Controls.Add(iconLbl);

            titleLbl = new Label();
            titleLbl.Text = title;
            titleLbl.Location = new Point(60, 12);
            titleLbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            titleLbl.ForeColor = Color.Gray;
            titleLbl.Size = new Size(250, 22);
            panel.Controls.Add(titleLbl);

            valueLbl = new Label();
            valueLbl.Text = "0";
            valueLbl.Location = new Point(60, 40);
            valueLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            valueLbl.ForeColor = valueColor;
            valueLbl.Size = new Size(250, 45);
            panel.Controls.Add(valueLbl);

            return panel;
        }
    }
}