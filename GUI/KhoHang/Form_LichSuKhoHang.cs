using System;
using System.Data;
using System.Windows.Forms;
using mini_supermarket.BUS;
using System.Collections.Generic;

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
                var listLichSu = khoHangBUS.LayLichSuThayDoi(maSanPham);
                dgvLichSu.DataSource = listLichSu;

                // Định dạng cột
                if (dgvLichSu.Columns["MaLichSu"] != null) dgvLichSu.Columns["MaLichSu"].HeaderText = "Mã Lịch Sử";
                if (dgvLichSu.Columns["SoLuongCu"] != null) dgvLichSu.Columns["SoLuongCu"].HeaderText = "Số Lượng Cũ";
                if (dgvLichSu.Columns["SoLuongMoi"] != null) dgvLichSu.Columns["SoLuongMoi"].HeaderText = "Số Lượng Mới";
                if (dgvLichSu.Columns["ChenhLech"] != null) dgvLichSu.Columns["ChenhLech"].HeaderText = "Chênh Lệch";
                if (dgvLichSu.Columns["LoaiThayDoi"] != null) dgvLichSu.Columns["LoaiThayDoi"].HeaderText = "Loại Thay Đổi";
                if (dgvLichSu.Columns["LyDo"] != null) dgvLichSu.Columns["LyDo"].HeaderText = "Lý Do";
                if (dgvLichSu.Columns["GhiChu"] != null) dgvLichSu.Columns["GhiChu"].HeaderText = "Ghi Chú";
                if (dgvLichSu.Columns["TenNhanVien"] != null) dgvLichSu.Columns["TenNhanVien"].HeaderText = "Nhân Viên";
                if (dgvLichSu.Columns["NgayThayDoi"] != null) dgvLichSu.Columns["NgayThayDoi"].HeaderText = "Ngày Thay Đổi";

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