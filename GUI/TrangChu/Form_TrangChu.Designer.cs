// Below is a redesigned, cleaner, more balanced WinForms layout for Form_TrangChu.
// This version reduces duplicate settings, fixes inconsistent padding/margins,
// improves proportional layout, and unifies styling.

using System;
using System.Windows.Forms;
using System.Drawing;

namespace mini_supermarket.GUI.TrangChu
{
    partial class Form_TrangChu
    {
        private System.ComponentModel.IContainer components = null;

        // Khai báo các controls
        private Panel panelMain;
        private Panel panelHeader;
        private Label lblHeader;
        private Button btnRefresh;
        
        private FlowLayoutPanel panelKPI;
        private Panel panelDoanhThu;
        private Label lblDoanhThuTitle;
        private Label lblDoanhThuValue;
        
        private Panel panelSoHoaDon;
        private Label lblSoHoaDonTitle;
        private Label lblSoHoaDonValue;
        
        private Panel panelHangHet;
        private Label lblHangHetTitle;
        private Label lblHangHetValue;
        
        private TableLayoutPanel panelCharts;
        private Panel panelDoanhThu7Ngay;
        private Panel panelTop5BanChay;
        
        private Panel panelDataGrid;
        private Label lblSapHetHanTitle;
        private DataGridView dgvSanPhamSapHetHan;

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
            this.panelHeader = new Panel();
            this.lblHeader = new Label();
            this.btnRefresh = new Button();
            this.panelKPI = new FlowLayoutPanel();
            this.panelCharts = new TableLayoutPanel();
            this.panelDoanhThu7Ngay = new Panel();
            this.panelTop5BanChay = new Panel();
            this.panelDataGrid = new Panel();
            this.lblSapHetHanTitle = new Label();
            this.dgvSanPhamSapHetHan = new DataGridView();

            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamSapHetHan)).BeginInit();

            // 
            // panelMain
            // 
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = Color.WhiteSmoke;
            this.panelMain.Padding = new Padding(10);
            this.panelMain.AutoScroll = true;
            this.panelMain.Controls.Add(this.panelDataGrid);
            this.panelMain.Controls.Add(this.panelCharts);
            this.panelMain.Controls.Add(this.panelKPI);
            this.panelMain.Controls.Add(this.panelHeader);

            // 
            // panelHeader
            // 
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 60;
            this.panelHeader.Padding = new Padding(15, 10, 15, 10);
            this.panelHeader.BackColor = Color.White;
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblHeader);

            // 
            // lblHeader
            // 
            this.lblHeader.Text = "TRANG CHỦ";
            this.lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblHeader.ForeColor = Color.FromArgb(0, 120, 215);
            this.lblHeader.Dock = DockStyle.Left;
            this.lblHeader.Width = 250;
            this.lblHeader.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // btnRefresh
            // 
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.BackColor = Color.FromArgb(0, 120, 215);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Width = 110;
            this.btnRefresh.Height = 35;
            this.btnRefresh.Dock = DockStyle.Right;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // 
            // panelKPI
            // 
            this.panelKPI.Dock = DockStyle.Top;
            this.panelKPI.Height = 110;
            this.panelKPI.FlowDirection = FlowDirection.LeftToRight;
            this.panelKPI.WrapContents = false;
            this.panelKPI.BackColor = Color.WhiteSmoke;
            this.panelKPI.Padding = new Padding(0, 10, 0, 10);
            
            // Tạo KPI panels
            this.panelDoanhThu = CreateKpiPanel("DOANH THU HÔM NAY", out this.lblDoanhThuTitle, out this.lblDoanhThuValue, Color.FromArgb(0, 120, 215));
            this.panelSoHoaDon = CreateKpiPanel("SỐ HÓA ĐƠN HÔM NAY", out this.lblSoHoaDonTitle, out this.lblSoHoaDonValue, Color.FromArgb(16, 137, 62));
            this.panelHangHet = CreateKpiPanel("SỐ HÀNG ĐÃ HẾT", out this.lblHangHetTitle, out this.lblHangHetValue, Color.FromArgb(220, 53, 69));
            
            this.lblDoanhThuValue.Text = "0 đ";
            this.lblSoHoaDonValue.Text = "0";
            this.lblHangHetValue.Text = "0";
            
            this.panelKPI.Controls.Add(this.panelDoanhThu);
            this.panelKPI.Controls.Add(this.panelSoHoaDon);
            this.panelKPI.Controls.Add(this.panelHangHet);

            // 
            // panelCharts
            // 
            this.panelCharts.Dock = DockStyle.Top;
            this.panelCharts.Height = 270;
            this.panelCharts.ColumnCount = 2;
            this.panelCharts.RowCount = 1;
            this.panelCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.panelCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.panelCharts.Controls.Add(this.panelDoanhThu7Ngay, 0, 0);
            this.panelCharts.Controls.Add(this.panelTop5BanChay, 1, 0);
            this.panelCharts.BackColor = Color.WhiteSmoke;
            this.panelCharts.Padding = new Padding(0, 10, 0, 10);

            // 
            // panelDoanhThu7Ngay
            // 
            this.panelDoanhThu7Ngay.Dock = DockStyle.Fill;
            this.panelDoanhThu7Ngay.BackColor = Color.White;
            this.panelDoanhThu7Ngay.BorderStyle = BorderStyle.FixedSingle;
            this.panelDoanhThu7Ngay.AutoScroll = true;
            this.panelDoanhThu7Ngay.Margin = new Padding(0, 0, 5, 0);

            // 
            // panelTop5BanChay
            // 
            this.panelTop5BanChay.Dock = DockStyle.Fill;
            this.panelTop5BanChay.BackColor = Color.White;
            this.panelTop5BanChay.BorderStyle = BorderStyle.FixedSingle;
            this.panelTop5BanChay.AutoScroll = true;
            this.panelTop5BanChay.Margin = new Padding(5, 0, 0, 0);

            // 
            // panelDataGrid
            // 
            this.panelDataGrid.Dock = DockStyle.Top;
            this.panelDataGrid.Height = 280;
            this.panelDataGrid.BackColor = Color.White;
            this.panelDataGrid.Padding = new Padding(15);
            this.panelDataGrid.Controls.Add(this.dgvSanPhamSapHetHan);
            this.panelDataGrid.Controls.Add(this.lblSapHetHanTitle);

            // 
            // lblSapHetHanTitle
            // 
            this.lblSapHetHanTitle.Text = "Sản Phẩm Sắp Hết Hạn (7 Ngày)";
            this.lblSapHetHanTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSapHetHanTitle.Dock = DockStyle.Top;
            this.lblSapHetHanTitle.Height = 30;
            this.lblSapHetHanTitle.ForeColor = Color.FromArgb(33, 37, 41);

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

            // 
            // Form_TrangChu
            // 
            this.ClientSize = new Size(1200, 800);
            this.Controls.Add(this.panelMain);
            this.Text = "Trang Chủ";
            this.BackColor = Color.WhiteSmoke;
            this.Load += new EventHandler(this.Form_TrangChu_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamSapHetHan)).EndInit();
            this.ResumeLayout(false);
        }

        private Panel CreateKpiPanel(string title, out Label titleLbl, out Label valueLbl, Color valueColor)
        {
            Panel panel = new Panel();
            panel.Width = 380;
            panel.Height = 90;
            panel.BackColor = Color.White;
            panel.Margin = new Padding(0, 0, 10, 0);
            panel.Padding = new Padding(12);

            titleLbl = new Label();
            titleLbl.Text = title;
            titleLbl.Dock = DockStyle.Top;
            titleLbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            titleLbl.ForeColor = Color.Gray;
            titleLbl.Height = 22;
            titleLbl.TextAlign = ContentAlignment.MiddleLeft;

            valueLbl = new Label();
            valueLbl.Text = "0";
            valueLbl.Dock = DockStyle.Fill;
            valueLbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            valueLbl.ForeColor = valueColor;
            valueLbl.TextAlign = ContentAlignment.MiddleLeft;

            panel.Controls.Add(valueLbl);
            panel.Controls.Add(titleLbl);
            
            return panel;
        }
    }
}
