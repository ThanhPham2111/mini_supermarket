using mini_supermarket.BUS;
using System;
using System.Data;
using System.Windows.Forms;

namespace mini_supermarket.GUI.KhoHang
{
    public partial class Form_ChiTietLichSuSP : Form
    {
        // Biến để lưu mã và tên sản phẩm được truyền từ form Kho Hàng
        private int maSanPham;
        private string tenSanPham;
        private KhoHangBUS khoHangBUS = new KhoHangBUS();

        public Form_ChiTietLichSuSP(int maSanPham, string tenSanPham)
        {
            InitializeComponent();
            this.maSanPham = maSanPham;
            this.tenSanPham = tenSanPham;
        }

        private void Form_ChiTietLichSuSP_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin sản phẩm lên các label
            lblTenSP.Text = this.tenSanPham;
            lblMaSP.Text = $"Mã sản phẩm: {this.maSanPham}";
            this.Text = $"Lịch Sử Giao Dịch - {this.tenSanPham}"; // Đặt tiêu đề cho cửa sổ

            // Tải dữ liệu lịch sử
            LoadHistoryData();
        }

        private void LoadHistoryData()
        {
            DataTable dt = khoHangBUS.LayLichSuNhapXuat(this.maSanPham);
            dgvLichSu.DataSource = dt;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            // Đóng form khi nhấn nút
            this.Close();
        }
    }
}
