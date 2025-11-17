using mini_supermarket.BUS;
using mini_supermarket.GUI.Form_SanPham;
using mini_supermarket.DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;

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
            DataTable dtLoai = khoHangBUS.LayDanhSachLoai();
            DataRow drLoai = dtLoai.NewRow();
            drLoai["MaLoai"] = -1;
            drLoai["TenLoai"] = "Tất cả loại";
            dtLoai.Rows.InsertAt(drLoai, 0);
            cboLoaiSP.DataSource = dtLoai;
            cboLoaiSP.DisplayMember = "TenLoai";
            cboLoaiSP.ValueMember = "MaLoai";

            DataTable dtThuongHieu = khoHangBUS.LayDanhSachThuongHieu();
            DataRow drTH = dtThuongHieu.NewRow();
            drTH["MaThuongHieu"] = -1;
            drTH["TenThuongHieu"] = "Tất cả thương hiệu";
            dtThuongHieu.Rows.InsertAt(drTH, 0);
            cboThuongHieu.DataSource = dtThuongHieu;
            cboThuongHieu.DisplayMember = "TenThuongHieu";
            cboThuongHieu.ValueMember = "MaThuongHieu";
        }

        private void LoadDataGridView()
        {
            dtProducts = khoHangBUS.LayDanhSachTonKho();

            if (dtProducts != null)
            {
                dtProducts.CaseSensitive = false;
            }

            dgvKhoHang.DataSource = dtProducts;
            SetupColumnHeaders();
        }

        private void SetupColumnHeaders()
        {
            if (dgvKhoHang.Columns["MaLoai"] != null) dgvKhoHang.Columns["MaLoai"].Visible = false;
            if (dgvKhoHang.Columns["MaThuongHieu"] != null) dgvKhoHang.Columns["MaThuongHieu"].Visible = false;
            if (dgvKhoHang.Columns["MaSP"] != null) dgvKhoHang.Columns["MaSP"].HeaderText = "Mã SP";
            if (dgvKhoHang.Columns["TenSanPham"] != null) dgvKhoHang.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            if (dgvKhoHang.Columns["TenDonVi"] != null) dgvKhoHang.Columns["TenDonVi"].HeaderText = "Đơn Vị";
            if (dgvKhoHang.Columns["TenLoai"] != null) dgvKhoHang.Columns["TenLoai"].HeaderText = "Loại";
            if (dgvKhoHang.Columns["TenThuongHieu"] != null) dgvKhoHang.Columns["TenThuongHieu"].HeaderText = "Thương Hiệu";
            if (dgvKhoHang.Columns["SoLuong"] != null) dgvKhoHang.Columns["SoLuong"].HeaderText = "Số Lượng Tồn";
        }

        private void ApplyFilters()
        {
            if (dtProducts == null) return;

            DataView dv = dtProducts.DefaultView;
            string filter = "1=1";

            string tuKhoa = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                filter += string.Format(" AND (TenSanPham LIKE '%{0}%' OR CONVERT(MaSP, 'System.String') LIKE '%{0}%')", tuKhoa);
            }

            if (cboLoaiSP.SelectedValue != null && (int)cboLoaiSP.SelectedValue != -1)
            {
                filter += string.Format(" AND MaLoai = {0}", cboLoaiSP.SelectedValue);
            }

            if (cboThuongHieu.SelectedValue != null && (int)cboThuongHieu.SelectedValue != -1)
            {
                filter += string.Format(" AND MaThuongHieu = {0}", cboThuongHieu.SelectedValue);
            }

            dv.RowFilter = filter;
            // Gán lại DataSource bằng DataTable đã lọc
            dgvKhoHang.DataSource = dv.ToTable();
        }

        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e) { ApplyFilters(); }
        private void cboThuongHieu_SelectedIndexChanged(object sender, EventArgs e) { ApplyFilters(); }
        private void txtTimKiem_TextChanged(object sender, EventArgs e) { ApplyFilters(); }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLoaiSP.SelectedValue = -1;
            cboThuongHieu.SelectedValue = -1;
            ApplyFilters();
        }

        // Nút Xuất Excel
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvKhoHang.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dt = (DataTable)dgvKhoHang.DataSource;

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"DanhSachTonKho_{DateTime.Now:yyyyMMdd}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DanhSachTonKho");
                        worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                        worksheet.Cells.AutoFitColumns();
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(excelFile);
                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi lưu file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Sự kiện Double-Click: Mở Form Chi Tiết Sản Phẩm
        private void dgvKhoHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataRowView drv = (DataRowView)dgvKhoHang.Rows[e.RowIndex].DataBoundItem;
                DataRow row = drv.Row;

                SanPhamDTO sanPham = new SanPhamDTO
                {
                    MaSanPham = Convert.ToInt32(row["MaSP"]),
                    TenSanPham = row["TenSanPham"].ToString() ?? "",
                    TenDonVi = row["TenDonVi"].ToString(),
                    TenLoai = row["TenLoai"].ToString(),
                    TenThuongHieu = row["TenThuongHieu"].ToString(),
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    MaThuongHieu = Convert.ToInt32(row["MaThuongHieu"])
                };

                Form_SanPhamDialog detailForm = new Form_SanPhamDialog(sanPham);
                detailForm.ShowDialog();
            }
        }
    }
}

