using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace mini_supermarket.GUI.TrangChu
{
    partial class Form_TrangChu
    {
        private System.ComponentModel.IContainer components = null;

        // --- KHAI BÁO CONTROLS ---
        private Panel panelMain;
        private Button btnRefresh;

        // Phần KPI
        private TableLayoutPanel panelKPI;
        private Panel panelDoanhThu;
        private Label lblDoanhThuTitle;
        private Label lblDoanhThuValue;

        private Panel panelSoHoaDon;
        private Label lblSoHoaDonTitle;
        private Label lblSoHoaDonValue;

        private Panel panelHangHet;
        private Label lblHangHetTitle;
        private Label lblHangHetValue;

        // Phần Biểu đồ
        private TableLayoutPanel panelCharts;
        private Panel panelDoanhThu7Ngay;
        private Panel panelTop5BanChay;

        // Phần Bảng hết hạn
        private Panel panelDataGrid;
        private Panel panelDataGridContainer;
        private Panel panelDataGridHeader;
        private Label lblSapHetHanTitle;
        private DataGridView dgvSanPhamSapHetHan;

        // Phần Khách hàng (Sửa lại tên biến đúng như cũ: panelKhachHang)
        private Panel panelKhachHang;
        private Panel panelKhachHangContent; // Panel con bên trong để làm đẹp
        private Label lblKhachHangTitle;
        private DataGridView dgvKhachHangThanThiet;

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
            this.panelMain = new Panel();
            this.btnRefresh = new Button();
            this.panelKPI = new TableLayoutPanel();
            this.panelCharts = new TableLayoutPanel();
            this.panelDoanhThu7Ngay = new Panel();
            this.panelTop5BanChay = new Panel();

            this.panelDataGrid = new Panel();
            this.panelDataGridContainer = new Panel();
            this.panelDataGridHeader = new Panel();
            this.lblSapHetHanTitle = new Label();
            this.dgvSanPhamSapHetHan = new DataGridView();

            // Khởi tạo Panel Khách hàng
            this.panelKhachHang = new Panel();
            this.panelKhachHangContent = new Panel();
            this.lblKhachHangTitle = new Label();
            this.dgvKhachHangThanThiet = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamSapHetHan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHangThanThiet)).BeginInit();
            this.SuspendLayout();

            // --- CẤU HÌNH FORM ---
            this.ClientSize = new Size(1200, 800);
            this.Text = "Trang Chủ - Dashboard";
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1000, 600);
            this.Load += new EventHandler(this.Form_TrangChu_Load);

            // --- PANEL MAIN ---
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = Color.Transparent;
            this.panelMain.Padding = new Padding(20);
            this.panelMain.AutoScroll = true;

            // --- NÚT REFRESH (Đã sửa lỗi thiếu tham số 40) ---
            this.btnRefresh.Size = new Size(40, 40);
            this.btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.BackColor = Color.White;
            this.btnRefresh.ForeColor = Color.FromArgb(0, 120, 215);
            this.btnRefresh.Font = new Font("Segoe MDL2 Assets", 14F, FontStyle.Regular, GraphicsUnit.Point);
            this.btnRefresh.Text = "\uE72C";
            this.btnRefresh.Cursor = Cursors.Hand;
            // SỬA LỖI Ở ĐÂY: Thêm tham số 40 cuối cùng
            this.btnRefresh.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 40, 40, 40, 40));
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // --- PHẦN KPI ---
            this.panelKPI.Dock = DockStyle.Top;
            this.panelKPI.Height = 120;
            this.panelKPI.ColumnCount = 3;
            this.panelKPI.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.panelKPI.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.panelKPI.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.panelKPI.Padding = new Padding(0, 0, 0, 20);

            this.panelDoanhThu = CreateKpiPanel("DOANH THU HÔM NAY", out this.lblDoanhThuTitle, out this.lblDoanhThuValue, Color.FromArgb(0, 120, 215));
            this.panelSoHoaDon = CreateKpiPanel("SỐ HÓA ĐƠN", out this.lblSoHoaDonTitle, out this.lblSoHoaDonValue, Color.FromArgb(16, 137, 62));
            this.panelHangHet = CreateKpiPanel("HÀNG ĐÃ HẾT", out this.lblHangHetTitle, out this.lblHangHetValue, Color.FromArgb(209, 52, 56));

            this.panelKPI.Controls.Add(this.panelDoanhThu, 0, 0);
            this.panelKPI.Controls.Add(this.panelSoHoaDon, 1, 0);
            this.panelKPI.Controls.Add(this.panelHangHet, 2, 0);

            // --- PHẦN CHARTS ---
            this.panelCharts.Dock = DockStyle.Top;
            this.panelCharts.Height = 300;
            this.panelCharts.ColumnCount = 2;
            this.panelCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            this.panelCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            this.panelCharts.Padding = new Padding(0, 0, 0, 20);

            this.panelDoanhThu7Ngay.Dock = DockStyle.Fill;
            this.panelDoanhThu7Ngay.BackColor = Color.White;
            this.panelDoanhThu7Ngay.Margin = new Padding(0, 0, 10, 0);
            this.panelDoanhThu7Ngay.Paint += Panel_Paint_Border;

            this.panelTop5BanChay.Dock = DockStyle.Fill;
            this.panelTop5BanChay.BackColor = Color.White;
            this.panelTop5BanChay.Margin = new Padding(10, 0, 0, 0);
            this.panelTop5BanChay.Paint += Panel_Paint_Border;

            this.panelCharts.Controls.Add(this.panelDoanhThu7Ngay, 0, 0);
            this.panelCharts.Controls.Add(this.panelTop5BanChay, 1, 0);

            // --- PHẦN DATA GRID ---
            this.panelDataGrid.Dock = DockStyle.Top;
            this.panelDataGrid.Height = 350;
            this.panelDataGrid.Padding = new Padding(0, 0, 0, 20);

            this.panelDataGridContainer.Dock = DockStyle.Fill;
            this.panelDataGridContainer.BackColor = Color.White;
            this.panelDataGridContainer.Padding = new Padding(1);
            this.panelDataGridContainer.Paint += Panel_Paint_Border;

            this.panelDataGridHeader.Dock = DockStyle.Top;
            this.panelDataGridHeader.Height = 50;
            this.panelDataGridHeader.BackColor = Color.White;
            this.panelDataGridHeader.Padding = new Padding(20, 0, 0, 0);

            this.lblSapHetHanTitle.Text = "Sản Phẩm Sắp Hết Hạn (7 Ngày)";
            this.lblSapHetHanTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            this.lblSapHetHanTitle.Dock = DockStyle.Fill;
            this.lblSapHetHanTitle.ForeColor = Color.FromArgb(64, 64, 64);
            this.lblSapHetHanTitle.TextAlign = ContentAlignment.MiddleLeft;

            this.panelDataGridHeader.Controls.Add(this.lblSapHetHanTitle);

            StyleDataGridView(this.dgvSanPhamSapHetHan);
            this.dgvSanPhamSapHetHan.Dock = DockStyle.Fill;

            this.panelDataGridContainer.Controls.Add(this.dgvSanPhamSapHetHan);
            this.panelDataGridContainer.Controls.Add(this.panelDataGridHeader);
            this.panelDataGrid.Controls.Add(this.panelDataGridContainer);

            // --- PHẦN KHÁCH HÀNG (Sửa lại đúng tên biến cũ) ---
            this.panelKhachHang.Dock = DockStyle.Top;
            this.panelKhachHang.Height = 250;
            this.panelKhachHang.Padding = new Padding(0, 0, 0, 20);

            this.panelKhachHangContent.Dock = DockStyle.Fill;
            this.panelKhachHangContent.BackColor = Color.White;
            this.panelKhachHangContent.Padding = new Padding(1);
            this.panelKhachHangContent.Paint += Panel_Paint_Border;

            this.lblKhachHangTitle.Text = "Khách Hàng Mua Nhiều Nhất";
            this.lblKhachHangTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            this.lblKhachHangTitle.Dock = DockStyle.Top;
            this.lblKhachHangTitle.Height = 50;
            this.lblKhachHangTitle.Padding = new Padding(20, 0, 0, 0);
            this.lblKhachHangTitle.TextAlign = ContentAlignment.MiddleLeft;

            StyleDataGridView(this.dgvKhachHangThanThiet);
            this.dgvKhachHangThanThiet.Dock = DockStyle.Fill;

            this.panelKhachHangContent.Controls.Add(this.dgvKhachHangThanThiet);
            this.panelKhachHangContent.Controls.Add(this.lblKhachHangTitle);

            // Add panel con vào panel cha (panelKhachHang)
            this.panelKhachHang.Controls.Add(this.panelKhachHangContent);


            // --- THÊM VÀO PANEL MAIN ---
            // Thứ tự add: Cái nào dưới cùng thì add trước (vì dùng Dock Top + BringToFront logic)
            this.panelMain.Controls.Add(this.panelKhachHang);
            this.panelMain.Controls.Add(this.panelDataGrid);
            this.panelMain.Controls.Add(this.panelCharts);
            this.panelMain.Controls.Add(this.panelKPI);
            this.panelMain.Controls.Add(this.btnRefresh);

            this.panelKPI.BringToFront();
            this.panelCharts.BringToFront();
            this.panelDataGrid.BringToFront();
            this.panelKhachHang.BringToFront(); // Đảm bảo đúng tên
            this.btnRefresh.BringToFront();

            this.Controls.Add(this.panelMain);

            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamSapHetHan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHangThanThiet)).EndInit();
            this.ResumeLayout(false);
        }

        // --- HÀM TẠO KPI PANEL ---
        private Panel CreateKpiPanel(string title, out Label titleLbl, out Label valueLbl, Color accentColor)
        {
            Panel container = new Panel();
            container.Dock = DockStyle.Fill;
            container.Padding = new Padding(5);

            Panel content = new Panel();
            content.Dock = DockStyle.Fill;
            content.BackColor = Color.White;
            content.Paint += Panel_Paint_Border;

            Panel accentBar = new Panel();
            accentBar.Width = 5;
            accentBar.Dock = DockStyle.Left;
            accentBar.BackColor = accentColor;

            Panel textPanel = new Panel();
            textPanel.Dock = DockStyle.Fill;
            textPanel.Padding = new Padding(15, 10, 10, 10);

            titleLbl = new Label();
            titleLbl.Text = title;
            titleLbl.Dock = DockStyle.Top;
            titleLbl.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            titleLbl.ForeColor = Color.Gray;
            titleLbl.Height = 25;

            valueLbl = new Label();
            valueLbl.Text = "0";
            valueLbl.Dock = DockStyle.Fill;
            valueLbl.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            valueLbl.ForeColor = Color.FromArgb(33, 37, 41);
            valueLbl.TextAlign = ContentAlignment.MiddleLeft;

            textPanel.Controls.Add(valueLbl);
            textPanel.Controls.Add(titleLbl);

            content.Controls.Add(textPanel);
            content.Controls.Add(accentBar);
            container.Controls.Add(content);

            return container;
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.Font = new Font("Segoe UI", 10F);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgv.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
            dgv.RowTemplate.Height = 40;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ScrollBars = ScrollBars.Both;
        }

        private void Panel_Paint_Border(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
                int radius = 8;
                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    GraphicsPath path = new GraphicsPath();
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseFigure();
                    g.DrawPath(pen, path);
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
    }
}