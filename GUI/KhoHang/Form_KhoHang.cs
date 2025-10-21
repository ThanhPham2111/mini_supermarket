using mini_supermarket.BUS;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace mini_supermarket.GUI.KhoHang
{
    public partial class Form_KhoHang : Form
    {
        private KhoHangBUS khoHangBUS = new KhoHangBUS();
        private DataTable? dtProducts = null;

        public Form_KhoHang()
        {
            InitializeComponent();
        }

        private void Form_KhoHang_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadDataGridView();
        }

        private void LoadComboBoxes()
        {
            cboLoaiSP.DataSource = khoHangBUS.LayDanhSachLoai();
            cboLoaiSP.DisplayMember = "TenLoai";
            cboLoaiSP.ValueMember = "MaLoai";
            cboLoaiSP.SelectedIndex = -1;

            cboThuongHieu.DataSource = khoHangBUS.LayDanhSachThuongHieu();
            cboThuongHieu.DisplayMember = "TenThuongHieu";
            cboThuongHieu.ValueMember = "MaThuongHieu";
            cboThuongHieu.SelectedIndex = -1;
        }

        private void LoadDataGridView()
        {
            dtProducts = khoHangBUS.LayDanhSachTonKho();
            dgvKhoHang.DataSource = dtProducts;
        }

        private void ApplyFilters()
        {
            if (dtProducts == null) return;

            DataView dv = dtProducts.DefaultView;
            string filter = "";

            // Lọc theo từ khóa
            string tuKhoa = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                // Thay đổi để tìm kiếm trên các cột có thật
                filter += string.Format("TenSanPham LIKE '%{0}%' OR MaSP LIKE '%{0}%'", tuKhoa);
            }

            // Lọc theo Loại sản phẩm
            if (cboLoaiSP.SelectedValue != null)
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += string.Format("MaLoai = {0}", cboLoaiSP.SelectedValue);
            }

            // Lọc theo Thương hiệu
            if (cboThuongHieu.SelectedValue != null)
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += string.Format("MaThuongHieu = {0}", cboThuongHieu.SelectedValue);
            }

            dv.RowFilter = filter;
            dgvKhoHang.DataSource = dv.ToTable();
        }

        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cboThuongHieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLoaiSP.SelectedIndex = -1;
            cboThuongHieu.SelectedIndex = -1;
            ApplyFilters();
        }

        private void dgvKhoHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int selectedMaSP = Convert.ToInt32(dgvKhoHang.Rows[e.RowIndex].Cells["MaSP"].Value);
                string selectedTenSP = dgvKhoHang.Rows[e.RowIndex].Cells["TenSanPham"].Value.ToString() ?? "Sản phẩm không tên";

                Form_ChiTietLichSuSP detailForm = new Form_ChiTietLichSuSP(selectedMaSP, selectedTenSP);
                detailForm.ShowDialog();
            }
        }
    }
}

