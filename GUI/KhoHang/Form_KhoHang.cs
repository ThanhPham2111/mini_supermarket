#nullable enable

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
using System.Collections.Generic;

namespace mini_supermarket.GUI.KhoHang
{
    public partial class Form_KhoHang : Form
    {
        private KhoHangBUS khoHangBUS = new KhoHangBUS();
        private IList<TonKhoDTO>? dtProducts = null;
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

            // Cho phép sắp xếp cột và hàng
            dgvKhoHang.AllowUserToOrderColumns = true;
            foreach (DataGridViewColumn column in dgvKhoHang.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void LoadComboBoxes()
        {
            // Load Loại sản phẩm
            var listLoai = khoHangBUS.LayDanhSachLoai();
            var comboListLoai = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(-1, "Tất cả loại") };
            foreach (var item in listLoai)
            {
                comboListLoai.Add(new KeyValuePair<int, string>(item.MaLoai, item.TenLoai));
            }
            cboLoaiSP.DataSource = comboListLoai;
            cboLoaiSP.DisplayMember = "Value";
            cboLoaiSP.ValueMember = "Key";

            // Load Thương hiệu
            var listThuongHieu = khoHangBUS.LayDanhSachThuongHieu();
            var comboListThuongHieu = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(-1, "Tất cả thương hiệu") };
            foreach (var item in listThuongHieu)
            {
                comboListThuongHieu.Add(new KeyValuePair<int, string>(item.MaThuongHieu, item.TenThuongHieu));
            }
            cboThuongHieu.DataSource = comboListThuongHieu;
            cboThuongHieu.DisplayMember = "Value";
            cboThuongHieu.ValueMember = "Key";

            // Load Trạng thái
            var comboListTrangThai = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("", "Tất cả trạng thái"),
                new KeyValuePair<string, string>("Còn hàng", "Còn hàng"),
                new KeyValuePair<string, string>("Hết hàng", "Hết hàng")
            };
            cboTrangThai.DataSource = comboListTrangThai;
            cboTrangThai.DisplayMember = "Value";
            cboTrangThai.ValueMember = "Key";
        }

        private void LoadDataGridView()
        {
            dtProducts = khoHangBUS.LayDanhSachTonKho();

            if (dtProducts != null)
            {
                // Không cần CaseSensitive cho IList
            }

            dgvKhoHang.DataSource = dtProducts;
            SetupColumnHeaders();
        }

        private void SetupColumnHeaders()
        {
            if (dgvKhoHang.Columns["MaLoai"] != null) dgvKhoHang.Columns["MaLoai"].Visible = false;
            if (dgvKhoHang.Columns["MaThuongHieu"] != null) dgvKhoHang.Columns["MaThuongHieu"].Visible = false;
            if (dgvKhoHang.Columns["MaSanPham"] != null) dgvKhoHang.Columns["MaSanPham"].HeaderText = "Mã sản phẩm";
            if (dgvKhoHang.Columns["TenSanPham"] != null) dgvKhoHang.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            if (dgvKhoHang.Columns["TenDonVi"] != null) dgvKhoHang.Columns["TenDonVi"].HeaderText = "Đơn vị";
            if (dgvKhoHang.Columns["TenLoai"] != null) dgvKhoHang.Columns["TenLoai"].HeaderText = "Loại";
            if (dgvKhoHang.Columns["TenThuongHieu"] != null) dgvKhoHang.Columns["TenThuongHieu"].HeaderText = "Thương hiệu";
            if (dgvKhoHang.Columns["SoLuong"] != null) dgvKhoHang.Columns["SoLuong"].HeaderText = "Số lượng";
            if (dgvKhoHang.Columns["TrangThai"] != null) dgvKhoHang.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvKhoHang.Columns["GiaBan"] != null) 
            {
                dgvKhoHang.Columns["GiaBan"].HeaderText = "Giá bán";
                dgvKhoHang.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                dgvKhoHang.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvKhoHang.Columns["Hsd"] != null) dgvKhoHang.Columns["Hsd"].HeaderText = "Hạn sử dụng";
            if (dgvKhoHang.Columns["GiaNhap"] != null) 
            {
                dgvKhoHang.Columns["GiaNhap"].HeaderText = "Giá nhập";
                dgvKhoHang.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                dgvKhoHang.Columns["GiaNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            foreach (DataGridViewColumn column in dgvKhoHang.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Set lại alignment cho cột giá (căn giữa)
            if (dgvKhoHang.Columns["GiaBan"] != null) 
                dgvKhoHang.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvKhoHang.Columns["GiaNhap"] != null) 
                dgvKhoHang.Columns["GiaNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Sắp xếp lại thứ tự cột
            if (dgvKhoHang.Columns.Contains("TenThuongHieu")) dgvKhoHang.Columns["TenThuongHieu"].DisplayIndex = 4;
            if (dgvKhoHang.Columns.Contains("Hsd")) dgvKhoHang.Columns["Hsd"].DisplayIndex = 5;
            if (dgvKhoHang.Columns.Contains("SoLuong")) dgvKhoHang.Columns["SoLuong"].DisplayIndex = 6;
        }

        // Highlight cảnh báo hàng tồn kho thấp
        private void dgvKhoHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKhoHang.Rows.Count) return;
            if (dgvKhoHang.Rows[e.RowIndex].DataBoundItem == null) return;
            TonKhoDTO item = (TonKhoDTO)dgvKhoHang.Rows[e.RowIndex].DataBoundItem;
            int soLuong = item.SoLuong ?? 0;
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

            var filtered = dtProducts.AsEnumerable();

            string tuKhoa = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                filtered = filtered.Where(item => item.TenSanPham.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) || item.MaSanPham.ToString().Contains(tuKhoa));
            }

            if (cboLoaiSP.SelectedValue != null && (int)cboLoaiSP.SelectedValue != -1)
            {
                filtered = filtered.Where(item => item.MaLoai == (int)cboLoaiSP.SelectedValue);
            }

            if (cboThuongHieu.SelectedValue != null && (int)cboThuongHieu.SelectedValue != -1)
            {
                filtered = filtered.Where(item => item.MaThuongHieu == (int)cboThuongHieu.SelectedValue);
            }

            if (cboTrangThai.SelectedValue != null && !string.IsNullOrEmpty(cboTrangThai.SelectedValue.ToString()))
            {
                filtered = filtered.Where(item => item.TrangThai == cboTrangThai.SelectedValue.ToString());
            }

            dgvKhoHang.DataSource = filtered.ToList();
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

            TonKhoDTO item = (TonKhoDTO)dgvKhoHang.SelectedRows[0].DataBoundItem;

            int maSanPham = item.MaSanPham;
            string tenSanPham = item.TenSanPham;
            int soLuong = item.SoLuong ?? 0;

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

        // Nút Xuất Excel (xuất danh sách hiện đang hiển thị)
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvKhoHang.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var list = (IList<TonKhoDTO>)dgvKhoHang.DataSource;

            if (list == null || list.Count == 0)
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
                        worksheet.Cells["A1"].LoadFromCollection(list, true);
                        worksheet.Cells.AutoFitColumns();
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(excelFile);
                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Hỏi có muốn mở file không
                        DialogResult result = MessageBox.Show("Bạn có muốn mở file Excel vừa xuất không?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(excelFile.FullName) { UseShellExecute = true });
                        }
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

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var errors = new List<string>();
            var updates = new List<string>();
            bool hasUpdates = false;

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(ofd.FileName)))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        MessageBox.Show("File Excel không có worksheet hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Tìm cột theo header
                    int colMaSP = -1, colSoLuong = -1;
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        var header = worksheet.Cells[1, col].Value?.ToString()?.Trim();
                        if (header == "Mã sản phẩm") colMaSP = col;
                        else if (header == "Số lượng") colSoLuong = col;
                    }

                    if (colMaSP == -1 || colSoLuong == -1)
                    {
                        MessageBox.Show("File Excel thiếu cột 'Mã sản phẩm' hoặc 'Số lượng'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int rowCount = worksheet.Dimension.End.Row;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        // Đọc Mã SP
                        string maSpText = worksheet.Cells[row, colMaSP].Value?.ToString()?.Trim() ?? "";
                        // Đọc Số lượng
                        string soLuongText = worksheet.Cells[row, colSoLuong].Value?.ToString()?.Trim() ?? "";

                        // Nếu cả hai đều trống, bỏ qua dòng mà không báo lỗi
                        if (string.IsNullOrEmpty(maSpText) && string.IsNullOrEmpty(soLuongText))
                        {
                            continue;
                        }

                        // Nếu mã SP trống nhưng số lượng có, báo lỗi
                        if (string.IsNullOrEmpty(maSpText) && !string.IsNullOrEmpty(soLuongText))
                        {
                            errors.Add($"Dòng {row}: Mã sản phẩm trống nhưng có số lượng.");
                            continue;
                        }

                        // Nếu mã SP có nhưng số lượng trống, bỏ qua
                        if (!string.IsNullOrEmpty(maSpText) && string.IsNullOrEmpty(soLuongText))
                        {
                            continue;
                        }

                        // Validate Mã SP
                        if (!int.TryParse(maSpText, out int maSp))
                        {
                            errors.Add($"Dòng {row}: Mã sản phẩm không phải là số nguyên ('{maSpText}').");
                            continue;
                        }

                        // Validate Số lượng
                        if (!int.TryParse(soLuongText, out int soLuongMoi))
                        {
                            errors.Add($"Dòng {row}: Số lượng không phải là số nguyên ('{soLuongText}').");
                            continue;
                        }
                        if (soLuongMoi < 0)
                        {
                            errors.Add($"Dòng {row}: Số lượng không được âm ({soLuongMoi}).");
                            continue;
                        }
                        if (soLuongMoi == 0)
                        {
                            errors.Add($"Dòng {row}: Số lượng phải lớn hơn 0 ({soLuongMoi}).");
                            continue;
                        }

                        // Validation: Kiểm tra sản phẩm tồn tại
                        var khoHienTai = khoHangBUS.GetByMaSanPham(maSp);
                        if (khoHienTai == null)
                        {
                            errors.Add($"Dòng {row}: Sản phẩm mã {maSp} không tồn tại.");
                            continue;
                        }

                        // Cập nhật kho (cập nhật số lượng trực tiếp từ file)
                        try
                        {
                            // Tạo DTO cho kho hàng
                            const int NGUONG_CANH_BAO = 10;
                            const int NGUONG_TIEM_CAN = 5;
                            string trangThaiMoi = soLuongMoi == 0 ? "Hết hàng" :
                                                 soLuongMoi <= NGUONG_TIEM_CAN ? "Cảnh báo - Tiệm cận" :
                                                 soLuongMoi <= NGUONG_CANH_BAO ? "Cảnh báo - Sắp hết hàng" :
                                                 "Còn hàng";

                            KhoHangDTO khoHangCapNhat = new KhoHangDTO
                            {
                                MaSanPham = maSp,
                                SoLuong = soLuongMoi,
                                TrangThai = trangThaiMoi
                            };

                            // Tạo DTO cho lịch sử
                            LichSuThayDoiKhoDTO lichSu = new LichSuThayDoiKhoDTO
                            {
                                MaSanPham = maSp,
                                SoLuongCu = khoHienTai.SoLuong ?? 0,
                                SoLuongMoi = soLuongMoi,
                                ChenhLech = soLuongMoi - (khoHienTai.SoLuong ?? 0),
                                LoaiThayDoi = "Cập nhật từ Excel",
                                LyDo = "Nhập từ file Excel mẫu",
                                GhiChu = $"Cập nhật số lượng từ Excel: {soLuongMoi}",
                                MaNhanVien = 1, // TODO: Lấy từ session
                                NgayThayDoi = DateTime.Now
                            };

                            khoHangBUS.CapNhatKhoVaGhiLog(khoHangCapNhat, lichSu);
                            updates.Add($"Sản phẩm {maSp}: cập nhật số lượng thành {soLuongMoi}");
                            hasUpdates = true;
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Dòng {row}: Lỗi cập nhật sản phẩm {maSp}: {ex.Message}");
                        }
                    }
                }

                // Hiển thị kết quả
                string message = "";
                if (errors.Any())
                {
                    message += "Có lỗi:\n" + string.Join("\n", errors) + "\n\n";
                }
                if (updates.Any())
                {
                    message += "Cập nhật thành công:\n" + string.Join("\n", updates);
                }
                if (!errors.Any() && !updates.Any())
                {
                    message = "Không có dữ liệu hợp lệ để cập nhật.";
                }

                MessageBox.Show(message, hasUpdates ? "Kết quả nhập Excel" : "Thông báo", MessageBoxButtons.OK, hasUpdates ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (hasUpdates)
                {
                    LoadDataGridView(); // Reload dữ liệu sau khi cập nhật
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nhập file Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatFileMau_Click(object sender, EventArgs e)
        {
            // Lấy danh sách tất cả sản phẩm từ BUS
            var allProducts = khoHangBUS.LayDanhSachTonKho();

            if (allProducts == null || allProducts.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm nào trong kho để xuất mẫu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"MauNhapKho_{DateTime.Now:yyyyMMdd}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("MauNhapKho");
                        
                        // Header
                        worksheet.Cells[1, 1].Value = "Mã sản phẩm";
                        worksheet.Cells[1, 2].Value = "Tên sản phẩm";
                        worksheet.Cells[1, 3].Value = "Số lượng"; // Để trống
                        
                        // Dữ liệu: Chỉ điền Mã và Tên, Số lượng để trống
                        for (int i = 0; i < allProducts.Count; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = allProducts[i].MaSanPham;
                            worksheet.Cells[i + 2, 2].Value = allProducts[i].TenSanPham;
                            // Cột 3 (Số lượng) để trống
                        }
                        
                        worksheet.Cells.AutoFitColumns();
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(excelFile);
                        MessageBox.Show("Xuất file mẫu Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Hỏi có muốn mở file không
                        DialogResult result = MessageBox.Show("Bạn có muốn mở file mẫu Excel vừa xuất không?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(excelFile.FullName) { UseShellExecute = true });
                        }
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
        private void dgvKhoHang_CellMouseEnter(object? sender, DataGridViewCellEventArgs e)
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
                    if (dtProducts != null && e.RowIndex < dtProducts.Count)
                    {
                        string tenDayDu = dtProducts[e.RowIndex].TenSanPham;
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

        private void dgvKhoHang_CellMouseLeave(object? sender, DataGridViewCellEventArgs e)
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

            TonKhoDTO item = (TonKhoDTO)dgvKhoHang.SelectedRows[0].DataBoundItem;
            int maSanPham = item.MaSanPham;
            string tenSanPham = item.TenSanPham;

            Form_LichSuKhoHang formLichSu = new Form_LichSuKhoHang(maSanPham, tenSanPham);
            formLichSu.ShowDialog();
        }
    }
}

