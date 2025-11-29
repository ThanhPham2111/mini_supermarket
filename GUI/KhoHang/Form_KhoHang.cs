using mini_supermarket.BUS;
using mini_supermarket.GUI.Form_SanPham;
using mini_supermarket.DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace mini_supermarket.GUI.KhoHang
{
    public partial class Form_KhoHang : Form
    {
        private KhoHangBUS khoHangBUS = new KhoHangBUS();
        private DataTable? dtProducts = null;
        private const int NGUONG_CANH_BAO = 10; // Ngưỡng cảnh báo hàng sắp hết
        private ToolTip toolTipTenSP = new ToolTip(); // ToolTip để hiển thị tên sản phẩm đầy đủ

        public Form_KhoHang()
        {
            InitializeComponent();
        }

        private void Form_KhoHang_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadDataGridView();
            
            // Đăng ký sự kiện ToolTip
            dgvKhoHang.CellMouseEnter += dgvKhoHang_CellMouseEnter;
            dgvKhoHang.CellMouseLeave += dgvKhoHang_CellMouseLeave;
        }

        private void LoadComboBoxes()
        {
            // Load Loại sản phẩm
            DataTable dtLoai = khoHangBUS.LayDanhSachLoai();
            DataRow drLoai = dtLoai.NewRow();
            drLoai["MaLoai"] = -1;
            drLoai["TenLoai"] = "Tất cả loại";
            dtLoai.Rows.InsertAt(drLoai, 0);
            cboLoaiSP.DataSource = dtLoai;
            cboLoaiSP.DisplayMember = "TenLoai";
            cboLoaiSP.ValueMember = "MaLoai";

            // Load Thương hiệu
            DataTable dtThuongHieu = khoHangBUS.LayDanhSachThuongHieu();
            DataRow drTH = dtThuongHieu.NewRow();
            drTH["MaThuongHieu"] = -1;
            drTH["TenThuongHieu"] = "Tất cả thương hiệu";
            dtThuongHieu.Rows.InsertAt(drTH, 0);
            cboThuongHieu.DataSource = dtThuongHieu;
            cboThuongHieu.DisplayMember = "TenThuongHieu";
            cboThuongHieu.ValueMember = "MaThuongHieu";

            // Load Trạng thái
            DataTable dtTrangThai = new DataTable();
            dtTrangThai.Columns.Add("Value", typeof(string));
            dtTrangThai.Columns.Add("Display", typeof(string));
            dtTrangThai.Rows.Add("", "Tất cả trạng thái");
            dtTrangThai.Rows.Add("Còn hàng", "Còn hàng");
            dtTrangThai.Rows.Add("Hết hàng", "Hết hàng");
            cboTrangThai.DataSource = dtTrangThai;
            cboTrangThai.DisplayMember = "Display";
            cboTrangThai.ValueMember = "Value";
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
            if (dgvKhoHang.Columns["Hsd"] != null) dgvKhoHang.Columns["Hsd"].HeaderText = "Hạn Sử Dụng";
            if (dgvKhoHang.Columns["SoLuong"] != null) dgvKhoHang.Columns["SoLuong"].HeaderText = "Số Lượng Tồn";
            if (dgvKhoHang.Columns["TrangThai"] != null) dgvKhoHang.Columns["TrangThai"].HeaderText = "Trạng Thái";
            if (dgvKhoHang.Columns["GiaNhap"] != null) dgvKhoHang.Columns["GiaNhap"].HeaderText = "Giá nhập";
            if (dgvKhoHang.Columns["GiaBan"] != null) dgvKhoHang.Columns["GiaBan"].HeaderText = "Giá bán";

            // Căn giữa nội dung các cột
            foreach (DataGridViewColumn column in dgvKhoHang.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Sắp xếp lại thứ tự cột
            dgvKhoHang.Columns["TenThuongHieu"].DisplayIndex = 4;
            dgvKhoHang.Columns["Hsd"].DisplayIndex = 5;
            dgvKhoHang.Columns["SoLuong"].DisplayIndex = 6;
        }

        // Highlight cảnh báo hàng tồn kho thấp
        private void dgvKhoHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKhoHang.Rows.Count) return;
            if (dgvKhoHang.Rows[e.RowIndex].DataBoundItem == null) return;
            DataRowView drv = (DataRowView)dgvKhoHang.Rows[e.RowIndex].DataBoundItem;
            if (drv.Row["SoLuong"] == DBNull.Value) return;
            int soLuong = Convert.ToInt32(drv.Row["SoLuong"]);
            if (soLuong == 0)
            {
                dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 220, 220);
                dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
            }
            else if (soLuong < NGUONG_CANH_BAO)
            {
                dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 220);
                dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.DarkOrange;
            }
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

            if (cboTrangThai.SelectedValue != null && !string.IsNullOrEmpty(cboTrangThai.SelectedValue.ToString()))
            {
                filter += string.Format(" AND TrangThai = '{0}'", cboTrangThai.SelectedValue.ToString());
            }

            dv.RowFilter = filter;
            dgvKhoHang.DataSource = dv.ToTable();
        }

        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e) { ApplyFilters(); }
        private void cboThuongHieu_SelectedIndexChanged(object sender, EventArgs e) { ApplyFilters(); }
        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e) { ApplyFilters(); }
        private void txtTimKiem_TextChanged(object sender, EventArgs e) { ApplyFilters(); }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLoaiSP.SelectedValue = -1;
            cboThuongHieu.SelectedValue = -1;
            cboTrangThai.SelectedValue = "";
            LoadDataGridView();
        }

        // Nút Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhoHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần điều chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)dgvKhoHang.SelectedRows[0].DataBoundItem;
            DataRow row = drv.Row;

            int maSanPham = Convert.ToInt32(row["MaSP"]);
            string tenSanPham = row["TenSanPham"].ToString() ?? "";
            int soLuong = Convert.ToInt32(row["SoLuong"]);

            // TODO: Lấy MaNhanVien từ session/login thực tế
            // Hiện tại dùng giá trị mặc định 1
            int maNhanVien = 1;

            Form_SuaKho formSua = new Form_SuaKho(maSanPham, tenSanPham, soLuong, maNhanVien);
            formSua.ShowDialog();

            if (formSua.IsUpdated)
            {
                LoadDataGridView(); // Reload lại dữ liệu
            }
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
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TonKho");
                        worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                        worksheet.Cells.AutoFitColumns();
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(excelFile);
                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu file Excel.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Chọn file Excel nhập kho"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                // TODO: Lấy MaNhanVien từ session/login thực tế
                int maNhanVien = 1; 

                khoHangBUS.NhapKhoTuExcel(ofd.FileName, maNhanVien);

                MessageBox.Show("Nhập kho từ file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView(); // Tải lại dữ liệu để hiển thị thay đổi
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi khi nhập liệu từ Excel. Vui lòng kiểm tra file và thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatFileMau_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = "MauNhapKhoHang.xlsx"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        var ws = package.Workbook.Worksheets.Add("MauKhoHang");
                        // Header
                        string[] headers = { "Mã SP", "Tên SP", "Loại", "Thương hiệu", "Đơn vị", "Số lượng", "Giá nhập", "Giá bán" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            ws.Cells[1, i + 1].Value = headers[i];
                            ws.Cells[1, i + 1].Style.Font.Bold = true;
                            ws.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                            ws.Cells[1, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        // Dòng mẫu
                        ws.Cells[2, 1].Value = "1";
                        ws.Cells[2, 2].Value = "Sữa tươi Vinamilk 1L";
                        ws.Cells[2, 3].Value = "Đồ uống";
                        ws.Cells[2, 4].Value = "Vinamilk";
                        ws.Cells[2, 5].Value = "Hộp";
                        ws.Cells[2, 6].Value = "100";
                        ws.Cells[2, 7].Value = "30000";
                        ws.Cells[2, 8].Value = "32000";
                        ws.Cells[2, 1, 2, headers.Length].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        ws.Cells.AutoFitColumns();
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(excelFile);
                        MessageBox.Show("Xuất file mẫu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu file mẫu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvKhoHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Không thực hiện hành động nào
        }

        /// <summary>
        /// Hiển thị ToolTip với tên sản phẩm đầy đủ khi di chuột vào cột TenSanPham
        /// </summary>
        private void dgvKhoHang_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                DataGridViewColumn column = dgvKhoHang.Columns[e.ColumnIndex];
                if (column.Name != "TenSanPham")
                    return;

                DataGridViewCell cell = dgvKhoHang.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value == null)
                    return;

                string tenSanPham = cell.Value.ToString() ?? "";

                if (tenSanPham.EndsWith("..."))
                {
                    if (dtProducts != null && e.RowIndex < dtProducts.Rows.Count)
                    {
                        string tenDayDu = dtProducts.Rows[e.RowIndex]["TenSanPham"]?.ToString() ?? "";
                        if (!string.IsNullOrEmpty(tenDayDu) && tenDayDu != tenSanPham)
                        {
                            toolTipTenSP.SetToolTip(dgvKhoHang, $"📦 {tenDayDu}");
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Lỗi không quan trọng, có thể bỏ qua
            }
        }

        private void dgvKhoHang_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            toolTipTenSP.SetToolTip(dgvKhoHang, "");
        }

        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            if (dgvKhoHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xem lịch sử!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)dgvKhoHang.SelectedRows[0].DataBoundItem;
            int maSanPham = Convert.ToInt32(drv["MaSP"]);

            Form_LichSuKhoHang formLichSu = new Form_LichSuKhoHang(maSanPham);
            formLichSu.ShowDialog();
        }
    }
}

