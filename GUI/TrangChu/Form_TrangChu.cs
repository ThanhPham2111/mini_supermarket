using mini_supermarket.BUS;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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
            LoadKPIData();
            LoadDoanhThu7Ngay();
            LoadTop5BanChay();
            LoadSanPhamSapHetHan();
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

                int rowHeight = 30;
                int yPosition = 45;

                // Tiêu đề
                Label lblTitle = new Label
                {
                    Text = "Doanh Thu 7 Ngày Qua",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Location = new Point(10, 10),
                    Size = new Size(570, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panelDoanhThu7Ngay.Controls.Add(lblTitle);

                // Tìm giá trị lớn nhất để tính tỷ lệ
                decimal maxValue = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["TongDoanhThu"] != DBNull.Value)
                    {
                        decimal value = Convert.ToDecimal(row["TongDoanhThu"]);
                        if (value > maxValue) maxValue = value;
                    }
                }

                // Hiển thị từng ngày
                foreach (DataRow row in dt.Rows)
                {
                    DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                    decimal tongDoanhThu = row["TongDoanhThu"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongDoanhThu"]);
                    
                    // Label ngày
                    Label lblNgay = new Label
                    {
                        Text = ngay.ToString("dd/MM"),
                        Font = new Font("Segoe UI", 8.5F),
                        Location = new Point(15, yPosition + 5),
                        Size = new Size(40, 18),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelDoanhThu7Ngay.Controls.Add(lblNgay);

                    // Thanh biểu đồ
                    int maxBarWidth = 290;
                    int barWidth = maxValue > 0 ? (int)((tongDoanhThu / maxValue) * maxBarWidth) : 0;
                    if (barWidth > 0)
                    {
                        Panel barPanel = new Panel
                        {
                            BackColor = Color.FromArgb(0, 120, 215),
                            Location = new Point(65, yPosition + 2),
                            Size = new Size(barWidth, 20),
                            BorderStyle = BorderStyle.None
                        };
                        panelDoanhThu7Ngay.Controls.Add(barPanel);
                    }

                    // Label giá trị
                    Label lblValue = new Label
                    {
                        Text = tongDoanhThu.ToString("N0") + " đ",
                        Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                        Location = new Point(365, yPosition + 5),
                        Size = new Size(210, 18),
                        TextAlign = ContentAlignment.MiddleRight
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

                int rowHeight = 30;
                int yPosition = 45;

                // Tiêu đề
                Label lblTitle = new Label
                {
                    Text = "Top 5 Sản Phẩm Bán Chạy (30 Ngày)",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Location = new Point(10, 10),
                    Size = new Size(570, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panelTop5BanChay.Controls.Add(lblTitle);

                // Tìm giá trị lớn nhất
                int maxValue = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int value = Convert.ToInt32(row["TongSoLuong"]);
                    if (value > maxValue) maxValue = value;
                }

                // Hiển thị từng sản phẩm
                foreach (DataRow row in dt.Rows)
                {
                    string tenSanPham = row["TenSanPham"].ToString() ?? "";
                    int tongSoLuong = Convert.ToInt32(row["TongSoLuong"]);
                    
                    // Rút ngắn tên sản phẩm nếu quá dài
                    string displayName = tenSanPham;
                    if (tenSanPham.Length > 30)
                        displayName = tenSanPham.Substring(0, 27) + "...";

                    // Label tên sản phẩm
                    Label lblTenSP = new Label
                    {
                        Text = displayName,
                        Font = new Font("Segoe UI", 8.5F),
                        Location = new Point(15, yPosition + 5),
                        Size = new Size(265, 18),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelTop5BanChay.Controls.Add(lblTenSP);

                    // Thanh biểu đồ
                    int maxBarWidth = 165;
                    int barWidth = maxValue > 0 ? (int)((double)tongSoLuong / maxValue * maxBarWidth) : 0;
                    if (barWidth > 0)
                    {
                        Panel barPanel = new Panel
                        {
                            BackColor = Color.FromArgb(16, 137, 62),
                            Location = new Point(290, yPosition + 2),
                            Size = new Size(barWidth, 20),
                            BorderStyle = BorderStyle.None
                        };
                        panelTop5BanChay.Controls.Add(barPanel);
                    }

                    // Label giá trị
                    Label lblValue = new Label
                    {
                        Text = tongSoLuong.ToString("N0"),
                        Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                        Location = new Point(465, yPosition + 5),
                        Size = new Size(110, 18),
                        TextAlign = ContentAlignment.MiddleRight
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

                // Định dạng cột
                if (dgvSanPhamSapHetHan.Columns.Contains("TenSanPham"))
                {
                    dgvSanPhamSapHetHan.Columns["TenSanPham"]!.HeaderText = "Tên Sản Phẩm";
                    dgvSanPhamSapHetHan.Columns["TenSanPham"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvSanPhamSapHetHan.Columns.Contains("HSD"))
                {
                    dgvSanPhamSapHetHan.Columns["HSD"]!.HeaderText = "Hạn Sử Dụng";
                    dgvSanPhamSapHetHan.Columns["HSD"]!.Width = 150;
                    dgvSanPhamSapHetHan.Columns["HSD"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                // Đánh dấu hàng gần hết hạn nhất bằng màu
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Form_TrangChu_Load(sender, e);
        }
    }
}
