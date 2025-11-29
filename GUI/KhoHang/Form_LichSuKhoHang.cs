using System;
using System.Data;
using System.Windows.Forms;
using mini_supermarket.BUS;

namespace mini_supermarket.GUI.KhoHang
{
    public partial class Form_LichSuKhoHang : Form
    {
        private KhoHangBUS khoHangBUS = new KhoHangBUS();
        private int maSanPham;

        public Form_LichSuKhoHang(int maSanPham)
        {
            InitializeComponent();
            this.maSanPham = maSanPham;
        }

        private void Form_LichSuKhoHang_Load(object sender, EventArgs e)
        {
            LoadLichSu();
        }

        private void LoadLichSu()
        {
            try
            {
                DataTable dtLichSu = khoHangBUS.LayLichSuThayDoi(maSanPham);
                dgvLichSu.DataSource = dtLichSu;

                // Định dạng cột
                dgvLichSu.Columns["MaLichSu"].HeaderText = "Mã Lịch Sử";
                dgvLichSu.Columns["SoLuongCu"].HeaderText = "Số Lượng Cũ";
                dgvLichSu.Columns["SoLuongMoi"].HeaderText = "Số Lượng Mới";
                dgvLichSu.Columns["ChenhLech"].HeaderText = "Chênh Lệch";
                dgvLichSu.Columns["LoaiThayDoi"].HeaderText = "Loại Thay Đổi";
                dgvLichSu.Columns["LyDo"].HeaderText = "Lý Do";
                dgvLichSu.Columns["GhiChu"].HeaderText = "Ghi Chú";
                dgvLichSu.Columns["TenNhanVien"].HeaderText = "Nhân Viên";
                dgvLichSu.Columns["NgayThayDoi"].HeaderText = "Ngày Thay Đổi";

                dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}