using mini_supermarket.BUS;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace mini_supermarket.GUI.TrangChu
{
    public partial class Form_TrangChu : Form
    {
        private TrangChuBUS trangChuBUS = new TrangChuBUS();

        public Form_TrangChu()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form_TrangChu_Load(object sender, EventArgs e)
        {
            LoadKPIData();
            LoadDoanhThu7Ngay();
            LoadTop5BanChay();
            LoadSanPhamSapHetHan();
            LoadSanPhamDaHetHan();
            LoadTopKhachHangMuaNhieu();
            panelMain_Resize(sender, e);
        }

        private void LoadKPIData()
        {
            try
            {
                decimal doanhThu = trangChuBUS.GetDoanhThuHomNay();
                lblDoanhThuValue.Text = doanhThu.ToString("N0") + " đ";

                int soHoaDon = trangChuBUS.GetSoHoaDonHomNay();
                lblSoHoaDonValue.Text = soHoaDon.ToString("N0");

                int soHangHet = trangChuBUS.GetSoLuongSanPhamHetHang();
                lblHangHetValue.Text = soHangHet.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi KPI: " + ex.Message);
            }
        }

        private void LoadDoanhThu7Ngay()
        {
            try
            {
                var list = trangChuBUS.GetDoanhThu7Ngay();
                chartDoanhThu7Ngay.Series.Clear();
                chartDoanhThu7Ngay.ChartAreas.Clear();

                ChartArea area = new ChartArea("DoanhThuArea");
                area.AxisX.Title = "Ngày";
                area.AxisY.Title = "Doanh Thu (đ)";
                chartDoanhThu7Ngay.ChartAreas.Add(area);

                Series series = new Series("DoanhThu");
                series.ChartType = SeriesChartType.Bar;
                series.Color = Color.FromArgb(0, 120, 215);

                foreach (var item in list)
                {
                    series.Points.AddXY(item.Ngay.ToString("dd/MM"), (double)item.TongDoanhThu);
                }

                chartDoanhThu7Ngay.Series.Add(series);
                chartDoanhThu7Ngay.Titles.Clear();
                chartDoanhThu7Ngay.Titles.Add("Doanh Thu 7 Ngày Qua");
                chartDoanhThu7Ngay.Titles[0].Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi doanh thu: " + ex.Message);
            }
        }

        private void LoadTop5BanChay()
        {
            try
            {
                var list = trangChuBUS.GetTop5BanChay();
                chartTop5BanChay.Series.Clear();
                chartTop5BanChay.ChartAreas.Clear();

                ChartArea area = new ChartArea("Top5Area");
                area.AxisX.Title = "Sản Phẩm";
                area.AxisY.Title = "Số Lượng";
                chartTop5BanChay.ChartAreas.Add(area);

                Series series = new Series("Top5");
                series.ChartType = SeriesChartType.Bar;
                series.Color = Color.FromArgb(16, 137, 62);

                foreach (var item in list)
                {
                    string tenSanPham = item.TenSanPham;
                    if (tenSanPham.Length > 15) tenSanPham = tenSanPham.Substring(0, 12) + "...";
                    series.Points.AddXY(tenSanPham, item.TongSoLuong);
                }

                chartTop5BanChay.Series.Add(series);
                chartTop5BanChay.Titles.Clear();
                chartTop5BanChay.Titles.Add("Top 5 Sản Phẩm Bán Chạy (30 Ngày)");
                chartTop5BanChay.Titles[0].Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi top 5: " + ex.Message);
            }
        }

        private void LoadSanPhamSapHetHan()
        {
            try
            {
                var list = trangChuBUS.GetSanPhamSapHetHan();
                dgvSanPhamSapHetHan.DataSource = list;

                if (dgvSanPhamSapHetHan.Columns.Contains("TenSanPham"))
                {
                    dgvSanPhamSapHetHan.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
                    dgvSanPhamSapHetHan.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvSanPhamSapHetHan.Columns.Contains("HSD"))
                {
                    dgvSanPhamSapHetHan.Columns["HSD"].HeaderText = "Hạn Sử Dụng";
                    dgvSanPhamSapHetHan.Columns["HSD"].Width = 150;
                    dgvSanPhamSapHetHan.Columns["HSD"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvSanPhamSapHetHan.Columns["HSD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Căn giữa tất cả cột
                foreach (DataGridViewColumn column in dgvSanPhamSapHetHan.Columns)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                foreach (DataGridViewRow row in dgvSanPhamSapHetHan.Rows)
                {
                    if (row.Cells["HSD"].Value != null && row.Cells["HSD"].Value is DateTime hsd)
                    {
                        int soNgayConLai = (hsd - DateTime.Now).Days;

                        if (soNgayConLai <= 3)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(139, 0, 0);
                        }
                        else if (soNgayConLai <= 5)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 205);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sản phẩm hết hạn: " + ex.Message);
            }
        }

        private void LoadSanPhamDaHetHan()
        {
            try
            {
                var list = trangChuBUS.GetSanPhamDaHetHan();
                dgvSanPhamDaHetHan.DataSource = list;

                if (dgvSanPhamDaHetHan.Columns.Contains("TenSanPham"))
                {
                    dgvSanPhamDaHetHan.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
                    dgvSanPhamDaHetHan.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvSanPhamDaHetHan.Columns.Contains("HSD"))
                {
                    dgvSanPhamDaHetHan.Columns["HSD"].HeaderText = "Hạn Sử Dụng";
                    dgvSanPhamDaHetHan.Columns["HSD"].Width = 150;
                    dgvSanPhamDaHetHan.Columns["HSD"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvSanPhamDaHetHan.Columns["HSD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Căn giữa tất cả cột
                foreach (DataGridViewColumn column in dgvSanPhamDaHetHan.Columns)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Tô màu đỏ cho tất cả hàng vì đã hết hạn
                foreach (DataGridViewRow row in dgvSanPhamDaHetHan.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(139, 0, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sản phẩm đã hết hạn: " + ex.Message);
            }
        }

        private void LoadTopKhachHangMuaNhieu()
        {
            try
            {
                var list = trangChuBUS.GetKhachHangMuaNhieuNhat();
                dgvTopKhachHang.DataSource = list;

                if (dgvTopKhachHang.Columns.Contains("TenKhachHang"))
                {
                    dgvTopKhachHang.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
                    dgvTopKhachHang.Columns["TenKhachHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvTopKhachHang.Columns.Contains("TongSoLuong"))
                {
                    dgvTopKhachHang.Columns["TongSoLuong"].HeaderText = "Tổng Số Lượng";
                    dgvTopKhachHang.Columns["TongSoLuong"].Width = 150;
                    dgvTopKhachHang.Columns["TongSoLuong"].DefaultCellStyle.Format = "N0";
                    dgvTopKhachHang.Columns["TongSoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Căn giữa tất cả cột
                foreach (DataGridViewColumn column in dgvTopKhachHang.Columns)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khách hàng: " + ex.Message);
            }
        }

        private void panelMain_Resize(object? sender, EventArgs e)
        {
            if (btnRefresh != null && panelMain != null)
            {
                int x = Math.Max(10, panelMain.ClientSize.Width - btnRefresh.Width - 10);
                int y = 12;
                btnRefresh.Location = new Point(x, y);
                btnRefresh.BringToFront();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            btnRefresh.Text = "\uE895";
            await Task.Delay(500);
            Form_TrangChu_Load(sender, e);
            btnRefresh.Enabled = true;
            btnRefresh.Text = "\uE72C";
        }
    }
}