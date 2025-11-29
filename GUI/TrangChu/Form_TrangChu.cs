using mini_supermarket.BUS;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace mini_supermarket.GUI.TrangChu
{
    public partial class Form_TrangChu : Form
    {
        private TrangChuBUS trangChuBUS = new TrangChuBUS();

        public Form_TrangChu()
        {
            InitializeComponent();
        }

        private void Form_TrangChu_Load(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            LoadKPIData();
            Console.WriteLine($"LoadKPIData: {sw.ElapsedMilliseconds} ms");
            sw.Restart();
            LoadDoanhThu7Ngay();
            Console.WriteLine($"LoadDoanhThu7Ngay: {sw.ElapsedMilliseconds} ms");
            sw.Restart();
            LoadTop5BanChay();
            Console.WriteLine($"LoadTop5BanChay: {sw.ElapsedMilliseconds} ms");
            sw.Restart();
            LoadSanPhamSapHetHan();
            Console.WriteLine($"LoadSanPhamSapHetHan: {sw.ElapsedMilliseconds} ms");
            sw.Restart();
            LoadSanPhamSapHetHang();
            Console.WriteLine($"LoadSanPhamSapHetHang: {sw.ElapsedMilliseconds} ms");
            sw.Restart();
            LoadKhachHangMuaNhieuNhat();
            Console.WriteLine($"LoadKhachHangMuaNhieuNhat: {sw.ElapsedMilliseconds} ms");
            sw.Stop();

            panelMain_Resize(sender, e);
        }

        private void panelMain_Resize(object? sender, EventArgs e)
        {
            if (btnRefresh != null && panelMain != null)
            {
                int x = this.ClientSize.Width - btnRefresh.Width - 20; // Cách phải 20px
                int y = 5; // Cách trên 5px
                btnRefresh.Location = new Point(x, y);
                btnRefresh.BringToFront();
            }
        }

        private void LoadKPIData()
        {
            try
            {
                // Doanh thu hôm nay
                decimal doanhThu = trangChuBUS.GetDoanhThuHomNay();
                lblDoanhThuValue.Text = doanhThu.ToString("N0") + " đ";

                // Số hóa đơn hôm nay
                int soHoaDon = trangChuBUS.GetSoHoaDonHomNay();
                lblSoHoaDonValue.Text = soHoaDon.ToString("N0");

                // Số hàng hết
                int soHangHet = trangChuBUS.GetSoLuongSanPhamHetHang();
                lblHangHetValue.Text = soHangHet.ToString("N0");
                lblHangHetValue.ForeColor = soHangHet > 0 ? Color.DarkRed : Color.Black;
                lblHangHetValue.Font = new Font(lblHangHetValue.Font, soHangHet > 0 ? FontStyle.Bold : FontStyle.Regular);

                // Debug console
                int soHangSapHet = trangChuBUS.GetSoLuongSanPhamSapHetHang();
                int soHangTiemCan = trangChuBUS.GetSoLuongSanPhamTiemCan();
                Console.WriteLine($"=== KPI DASHBOARD ===");
                Console.WriteLine($"Doanh thu hôm nay: {doanhThu:N0} đ");
                Console.WriteLine($"Hóa đơn hôm nay: {soHoaDon}");
                Console.WriteLine($"Sản phẩm hết hàng: {soHangHet}");
                Console.WriteLine($"Sản phẩm sắp hết (1-10): {soHangSapHet}");
                Console.WriteLine($"Sản phẩm tần cận (1-5): {soHangTiemCan}");
                Console.WriteLine($"====================");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu KPI: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDoanhThu7Ngay()
        {
            try
            {
                DataTable dt = trangChuBUS.GetDoanhThu7Ngay();
                panelDoanhThu7Ngay.Controls.Clear();

                int rowHeight = 28;
                int yPosition = 45;

                Label lblTitle = new Label
                {
                    Text = "Doanh Thu 7 Ngày Qua",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Location = new Point(10, 10),
                    Size = new Size(panelDoanhThu7Ngay.Width - 20, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panelDoanhThu7Ngay.Controls.Add(lblTitle);

                decimal maxValue = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["TongDoanhThu"] != DBNull.Value)
                    {
                        decimal value = Convert.ToDecimal(row["TongDoanhThu"]);
                        if (value > maxValue) maxValue = value;
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                    decimal tongDoanhThu = row["TongDoanhThu"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongDoanhThu"]);

                    Label lblNgay = new Label
                    {
                        Text = ngay.ToString("dd/MM"),
                        Font = new Font("Segoe UI", 8.5F),
                        AutoSize = true,
                        Location = new Point(15, yPosition + 5),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelDoanhThu7Ngay.Controls.Add(lblNgay);

                    int maxBarWidth = panelDoanhThu7Ngay.Width - 100; // cho padding và lblValue
                    int barWidth = maxValue > 0 ? (int)((tongDoanhThu / maxValue) * maxBarWidth) : 0;

                    Panel barPanel = new Panel
                    {
                        BackColor = Color.FromArgb(0, 120, 215),
                        Location = new Point(lblNgay.Right + 5, yPosition + 2),
                        Size = new Size(barWidth, 20)
                    };
                    panelDoanhThu7Ngay.Controls.Add(barPanel);

                    Label lblValue = new Label
                    {
                        Text = tongDoanhThu.ToString("N0") + " đ",
                        Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                        AutoSize = true,
                        Location = new Point(barPanel.Right + 5, yPosition + 5),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelDoanhThu7Ngay.Controls.Add(lblValue);

                    yPosition += rowHeight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải doanh thu 7 ngày: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTop5BanChay()
        {
            try
            {
                DataTable dt = trangChuBUS.GetTop5BanChay();
                panelTop5BanChay.Controls.Clear();

                int rowHeight = 28;
                int yPosition = 45;

                Label lblTitle = new Label
                {
                    Text = "Top 5 Sản Phẩm Bán Chạy (30 Ngày)",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Location = new Point(10, 10),
                    Size = new Size(panelTop5BanChay.Width - 20, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panelTop5BanChay.Controls.Add(lblTitle);

                int maxValue = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int value = Convert.ToInt32(row["TongSoLuong"]);
                    if (value > maxValue) maxValue = value;
                }

                foreach (DataRow row in dt.Rows)
                {
                    string tenSanPham = row["TenSanPham"].ToString() ?? "";
                    int tongSoLuong = Convert.ToInt32(row["TongSoLuong"]);
                    string displayName = tenSanPham.Length > 30 ? tenSanPham.Substring(0, 27) + "..." : tenSanPham;

                    Label lblTenSP = new Label
                    {
                        Text = displayName,
                        Font = new Font("Segoe UI", 8.5F),
                        AutoSize = true,
                        Location = new Point(15, yPosition + 5),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelTop5BanChay.Controls.Add(lblTenSP);

                    int maxBarWidth = panelTop5BanChay.Width - 200; // cho padding và lblValue
                    int barWidth = maxValue > 0 ? (int)((double)tongSoLuong / maxValue * maxBarWidth) : 0;

                    Panel barPanel = new Panel
                    {
                        BackColor = Color.FromArgb(16, 137, 62),
                        Location = new Point(lblTenSP.Right + 5, yPosition + 2),
                        Size = new Size(barWidth, 20)
                    };
                    panelTop5BanChay.Controls.Add(barPanel);

                    Label lblValue = new Label
                    {
                        Text = tongSoLuong.ToString("N0"),
                        Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                        AutoSize = true,
                        Location = new Point(barPanel.Right + 5, yPosition + 5),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelTop5BanChay.Controls.Add(lblValue);

                    yPosition += rowHeight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải top 5 bán chạy: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSanPhamSapHetHan()
        {
            try
            {
                DataTable dt = trangChuBUS.GetSanPhamSapHetHan();
                dgvSanPhamSapHetHan.DataSource = dt;

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
                }

                foreach (DataGridViewRow row in dgvSanPhamSapHetHan.Rows)
                {
                    if (row.Cells["HSD"].Value != DBNull.Value)
                    {
                        DateTime hsd = Convert.ToDateTime(row.Cells["HSD"].Value);
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
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm sắp hết hạn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSanPhamSapHetHang()
        {
            try
            {
                DataTable dt = trangChuBUS.GetSanPhamSapHetHang();
                if (dt == null || dt.Rows.Count == 0) return;

                string msg = $"⚠️ CÓ {dt.Rows.Count} SẢN PHẨM SẮP HẾT HÀNG\n\nDanh sách sản phẩm cảnh báo (tồn kho <=10):\n";
                foreach (DataRow row in dt.Rows)
                {
                    string tenSP = row["TenSanPham"].ToString() ?? "";
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    msg += $"• {tenSP}: {soLuong} cái\n";
                }
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tải danh sách sắp hết hàng: " + ex.Message);
            }
        }

        private void LoadKhachHangMuaNhieuNhat()
        {
            try
            {
                DataTable dt = trangChuBUS.GetKhachHangMuaNhieuNhat();
                panelKhachHang.Controls.Clear();

                int rowHeight = 28;
                int yPosition = 45;

                Label lblTitle = new Label
                {
                    Text = "Top Khách Hàng Mua Nhiều Nhất",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Location = new Point(10, 10),
                    Size = new Size(panelKhachHang.Width - 20, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panelKhachHang.Controls.Add(lblTitle);

                foreach (DataRow row in dt.Rows)
                {
                    string tenKhachHang = row["TenKhachHang"].ToString() ?? "";
                    int tongSoLuong = Convert.ToInt32(row["TongSoLuong"]);

                    Label lblTenKH = new Label
                    {
                        Text = tenKhachHang,
                        Font = new Font("Segoe UI", 8.5F),
                        AutoSize = true,
                        Location = new Point(15, yPosition + 5),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelKhachHang.Controls.Add(lblTenKH);

                    int maxBarWidth = panelKhachHang.Width - 200;
                    int barWidth = tongSoLuong > 0 ? (int)((double)tongSoLuong / 100 * maxBarWidth) : 0;

                    Panel barPanel = new Panel
                    {
                        BackColor = Color.FromArgb(16, 137, 62),
                        Location = new Point(lblTenKH.Right + 5, yPosition + 2),
                        Size = new Size(barWidth, 20)
                    };
                    panelKhachHang.Controls.Add(barPanel);

                    Label lblValue = new Label
                    {
                        Text = tongSoLuong.ToString("N0"),
                        Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                        AutoSize = true,
                        Location = new Point(barPanel.Right + 5, yPosition + 5),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelKhachHang.Controls.Add(lblValue);

                    yPosition += rowHeight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khách hàng mua nhiều nhất: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Form_TrangChu_Load(sender, e);
        }

        // Hàm mở Form Quản lý
        public void ShowQuanLy()
        {
            var formQuanLy = new mini_supermarket.GUI.QuanLy.Form_QuanLy();
            formQuanLy.ShowDialog();
        }
    }
}
