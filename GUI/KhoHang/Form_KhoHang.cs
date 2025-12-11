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
using System.ComponentModel;

namespace mini_supermarket.GUI.KhoHang
{
    public partial class Form_KhoHang : Form
    {
        private KhoHangBUS khoHangBUS = new KhoHangBUS();
        private BindingList<TonKhoDTO>? bindingListProducts = null;
        private BindingSource bindingSourceProducts = new BindingSource();
        private BindingSource bindingSourceLoai = new BindingSource();
        private BindingSource bindingSourceThuongHieu = new BindingSource();
        private BindingSource bindingSourceTrangThai = new BindingSource();
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
            var comboListLoai = new BindingList<KeyValuePair<int, string>>();
            comboListLoai.Add(new KeyValuePair<int, string>(-1, "Tất cả loại"));
            foreach (var item in listLoai)
            {
                comboListLoai.Add(new KeyValuePair<int, string>(item.MaLoai, item.TenLoai));
            }
            bindingSourceLoai.DataSource = comboListLoai;
            cboLoaiSP.DataSource = bindingSourceLoai;
            cboLoaiSP.DisplayMember = "Value";
            cboLoaiSP.ValueMember = "Key";

            // Load Thương hiệu
            var listThuongHieu = khoHangBUS.LayDanhSachThuongHieu();
            var comboListThuongHieu = new BindingList<KeyValuePair<int, string>>();
            comboListThuongHieu.Add(new KeyValuePair<int, string>(-1, "Tất cả thương hiệu"));
            foreach (var item in listThuongHieu)
            {
                comboListThuongHieu.Add(new KeyValuePair<int, string>(item.MaThuongHieu, item.TenThuongHieu));
            }
            bindingSourceThuongHieu.DataSource = comboListThuongHieu;
            cboThuongHieu.DataSource = bindingSourceThuongHieu;
            cboThuongHieu.DisplayMember = "Value";
            cboThuongHieu.ValueMember = "Key";

            // Load Trạng thái
            var comboListTrangThai = new BindingList<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("", "Tất cả trạng thái"),
                new KeyValuePair<string, string>("Còn hàng", "Còn hàng"),
                new KeyValuePair<string, string>("Hết hàng", "Hết hàng")
            };
            bindingSourceTrangThai.DataSource = comboListTrangThai;
            cboTrangThai.DataSource = bindingSourceTrangThai;
            cboTrangThai.DisplayMember = "Value";
            cboTrangThai.ValueMember = "Key";
        }

        private void LoadDataGridView()
        {
            var dtProducts = khoHangBUS.LayDanhSachTonKho();
            bindingListProducts = new BindingList<TonKhoDTO>(dtProducts.ToList());
            bindingSourceProducts.DataSource = bindingListProducts;
            dgvKhoHang.DataSource = bindingSourceProducts;
            
            SetupColumnHeaders();
            ThemCotTrangThaiBan();
            
            // Sắp xếp lại thứ tự cột
            SapXepThuTuCot();
            
            // Refresh để đảm bảo cột button hiển thị đúng
            dgvKhoHang.Refresh();
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
            if (dgvKhoHang.Columns["TrangThai"] != null) dgvKhoHang.Columns["TrangThai"].HeaderText = "Trạng thái kho";
            // Ẩn cột Giá bán
            if (dgvKhoHang.Columns["GiaBan"] != null) 
            {
                dgvKhoHang.Columns["GiaBan"].Visible = false;
            }
            if (dgvKhoHang.Columns["Hsd"] != null) 
            {
                dgvKhoHang.Columns["Hsd"].HeaderText = "Hạn sử dụng";
                dgvKhoHang.Columns["Hsd"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvKhoHang.Columns["Hsd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // Ẩn cột Giá nhập
            if (dgvKhoHang.Columns["GiaNhap"] != null) 
            {
                dgvKhoHang.Columns["GiaNhap"].Visible = false;
            }
            foreach (DataGridViewColumn column in dgvKhoHang.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void ThemCotTrangThaiBan()
        {
            // Xóa cột cũ nếu có
            if (dgvKhoHang.Columns.Contains("colTrangThaiBan"))
            {
                dgvKhoHang.Columns.Remove("colTrangThaiBan");
            }

            // Thêm cột button
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = "colTrangThaiBan";
            btnColumn.HeaderText = "Trạng thái bán";
            btnColumn.Width = 120;
            btnColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            btnColumn.FlatStyle = FlatStyle.Flat;
            
            dgvKhoHang.Columns.Add(btnColumn);
            
            // Đặt vị trí cột ở ngoài cùng bên phải (cuối cùng)
            btnColumn.DisplayIndex = dgvKhoHang.Columns.Count - 1;
            
            // Đảm bảo cột này luôn ở cuối
            btnColumn.Frozen = false;

            // Đăng ký sự kiện click
            dgvKhoHang.CellClick -= dgvKhoHang_CellClick; // Xóa sự kiện cũ nếu có
            dgvKhoHang.CellClick += dgvKhoHang_CellClick;
            dgvKhoHang.CellFormatting -= dgvKhoHang_CellFormatting_TrangThaiBan; // Xóa sự kiện cũ nếu có
            dgvKhoHang.CellFormatting += dgvKhoHang_CellFormatting_TrangThaiBan;
        }

        private void dgvKhoHang_CellFormatting_TrangThaiBan(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format màu cho button trạng thái bán
            if (dgvKhoHang.Columns[e.ColumnIndex].Name == "colTrangThaiBan" && e.RowIndex >= 0)
            {
                var item = dgvKhoHang.Rows[e.RowIndex].DataBoundItem as TonKhoDTO;
                if (item != null)
                {
                    int maSanPham = item.MaSanPham;
                    var trangThaiDieuKien = khoHangBUS.GetTrangThaiDieuKienBan(maSanPham);
                    
                    if (trangThaiDieuKien == KhoHangBUS.TRANG_THAI_DIEU_KIEN_BAN || string.IsNullOrEmpty(trangThaiDieuKien))
                    {
                        e.Value = "✓ Bán";
                        dgvKhoHang.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(40, 167, 69);
                        dgvKhoHang.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Value = "✗ Không bán";
                        dgvKhoHang.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(220, 53, 69);
                        dgvKhoHang.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    }
                }
            }
        }

        private void dgvKhoHang_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Xử lý click vào button trạng thái bán
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && 
                dgvKhoHang.Columns[e.ColumnIndex].Name == "colTrangThaiBan")
            {
                var item = dgvKhoHang.Rows[e.RowIndex].DataBoundItem as TonKhoDTO;
                if (item != null)
                {
                    int maSanPham = item.MaSanPham;
                    string tenSanPham = item.TenSanPham;
                    var trangThaiHienTai = khoHangBUS.GetTrangThaiDieuKienBan(maSanPham);
                    
                    // Toggle trạng thái
                    string trangThaiMoi;
                    string action;
                    if (trangThaiHienTai == KhoHangBUS.TRANG_THAI_DIEU_KIEN_KHONG_BAN)
                    {
                        trangThaiMoi = KhoHangBUS.TRANG_THAI_DIEU_KIEN_BAN;
                        action = "mở lại bán";
                    }
                    else
                    {
                        trangThaiMoi = KhoHangBUS.TRANG_THAI_DIEU_KIEN_KHONG_BAN;
                        action = "ngừng bán";
                    }

                    // Kiểm tra: Nếu số lượng bằng 0 và muốn chuyển sang "Bán", không cho phép
                    if (item.SoLuong == 0 && trangThaiMoi == KhoHangBUS.TRANG_THAI_DIEU_KIEN_BAN)
                    {
                        MessageBox.Show("Không thể chuyển trạng thái sang 'Bán' vì sản phẩm đã hết hàng.", "Không cho phép", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Xác nhận
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc muốn {action} sản phẩm:\n\n{tenSanPham}?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            bool success = khoHangBUS.CapNhatTrangThaiDieuKienBan(maSanPham, trangThaiMoi);
                            if (success)
                            {
                                // Refresh lại dòng hiện tại
                                dgvKhoHang.InvalidateRow(e.RowIndex);
                                
                                string msg = trangThaiMoi == KhoHangBUS.TRANG_THAI_DIEU_KIEN_BAN
                                    ? "Đã mở lại bán sản phẩm!"
                                    : "Đã ngừng bán sản phẩm!";
                                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // Highlight cảnh báo hàng tồn kho thấp và sản phẩm gần hết hạn/đã hết hạn
        private void dgvKhoHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKhoHang.Rows.Count) return;
            if (dgvKhoHang.Rows[e.RowIndex].DataBoundItem == null) return;
            TonKhoDTO item = (TonKhoDTO)dgvKhoHang.Rows[e.RowIndex].DataBoundItem;
            int soLuong = item.SoLuong ?? 0;
            
            // Không tô màu cột button
            if (dgvKhoHang.Columns[e.ColumnIndex].Name == "colTrangThaiBan")
                return;

            // Kiểm tra HSD: gần hết hạn (trong 7 ngày) hoặc đã hết hạn
            bool isNearExpiryOrExpired = false;
            if (item.Hsd.HasValue)
            {
                DateTime ngayHienTai = DateTime.Now.Date;
                DateTime hsd = item.Hsd.Value.Date;
                int soNgayConLai = (hsd - ngayHienTai).Days;
                
                // Đã hết hạn hoặc gần hết hạn (trong 7 ngày)
                if (soNgayConLai <= 7)
                {
                    isNearExpiryOrExpired = true;
                }
            }

            // Ưu tiên tô màu xám nhẹ cho sản phẩm gần hết hạn/đã hết hạn
            if (isNearExpiryOrExpired)
            {
            dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(210, 180, 140); // Nâu nhạt (Tan)
dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(90, 60, 30);    // Chữ nâu đậm nhẹ



            }
            else
            {
                // Tô màu theo số lượng như cũ
                if (soLuong == 0)
                {
                    dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else if (soLuong < NGUONG_CANH_BAO)
                {
                    dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 220);
                    dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkOrange;
                }
                else
                {
                    // Reset về màu mặc định cho các hàng khác
                    dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void ApplyFilters()
        {
            if (bindingListProducts == null) return;

            var filtered = bindingListProducts.AsEnumerable();

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

            var filteredList = new BindingList<TonKhoDTO>(filtered.ToList());
            bindingSourceProducts.DataSource = filteredList;
            
            // Sắp xếp lại thứ tự cột sau khi filter
            SapXepThuTuCot();
        }

        private void SapXepThuTuCot()
        {
            // Sắp xếp lại thứ tự cột - đảm bảo "Trạng thái bán" luôn ở ngoài cùng bên phải sau "Số lượng" và "Trạng thái kho"
            int displayIndex = 0;
            if (dgvKhoHang.Columns.Contains("MaSanPham")) dgvKhoHang.Columns["MaSanPham"].DisplayIndex = displayIndex++;
            if (dgvKhoHang.Columns.Contains("TenSanPham")) dgvKhoHang.Columns["TenSanPham"].DisplayIndex = displayIndex++;
            if (dgvKhoHang.Columns.Contains("TenDonVi")) dgvKhoHang.Columns["TenDonVi"].DisplayIndex = displayIndex++;
            if (dgvKhoHang.Columns.Contains("TenLoai")) dgvKhoHang.Columns["TenLoai"].DisplayIndex = displayIndex++;
            if (dgvKhoHang.Columns.Contains("TenThuongHieu")) dgvKhoHang.Columns["TenThuongHieu"].DisplayIndex = displayIndex++;
            if (dgvKhoHang.Columns.Contains("SoLuong")) dgvKhoHang.Columns["SoLuong"].DisplayIndex = displayIndex++;
            if (dgvKhoHang.Columns.Contains("TrangThai")) dgvKhoHang.Columns["TrangThai"].DisplayIndex = displayIndex++;
            if (dgvKhoHang.Columns.Contains("Hsd")) dgvKhoHang.Columns["Hsd"].DisplayIndex = displayIndex++;
            // Cột "colTrangThaiBan" phải ở cuối cùng (ngoài cùng bên phải)
            if (dgvKhoHang.Columns.Contains("colTrangThaiBan")) 
            {
                dgvKhoHang.Columns["colTrangThaiBan"].DisplayIndex = displayIndex;
            }
        }

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
            if (bindingSourceProducts.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var list = bindingSourceProducts.DataSource as BindingList<TonKhoDTO>;

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
                    XuatExcelTheoDataGridView(list, saveFileDialog.FileName);
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi có muốn mở file không
                    DialogResult result = MessageBox.Show("Bạn có muốn mở file Excel vừa xuất không?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi lưu file Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Xuất Excel theo đúng các cột hiển thị trong DataGridView
        /// </summary>
        private void XuatExcelTheoDataGridView(BindingList<TonKhoDTO> data, string filePath)
        {
            if (data == null || data.Count == 0)
                throw new ArgumentException("Không có dữ liệu để xuất.");

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TonKho");

                // Lấy danh sách các cột hiển thị (không bao gồm cột ẩn và cột button)
                var visibleColumns = dgvKhoHang.Columns
                    .Cast<DataGridViewColumn>()
                    .Where(col => col.Visible && !(col is DataGridViewButtonColumn))
                    .OrderBy(col => col.DisplayIndex)
                    .ToList();

                // Viết header
                int colIndex = 1;
                foreach (var column in visibleColumns)
                {
                    worksheet.Cells[1, colIndex].Value = column.HeaderText;
                    worksheet.Cells[1, colIndex].Style.Font.Bold = true;
                    colIndex++;
                }

                // Viết dữ liệu
                int rowIndex = 2;
                foreach (var item in data)
                {
                    colIndex = 1;
                    foreach (var column in visibleColumns)
                    {
                        object? value = null;
                        
                        // Lấy giá trị theo tên cột
                        switch (column.Name)
                        {
                            case "MaSanPham":
                                value = item.MaSanPham;
                                break;
                            case "TenSanPham":
                                value = item.TenSanPham;
                                break;
                            case "TenDonVi":
                                value = item.TenDonVi;
                                break;
                            case "TenLoai":
                                value = item.TenLoai;
                                break;
                            case "TenThuongHieu":
                                value = item.TenThuongHieu;
                                break;
                            case "SoLuong":
                                value = item.SoLuong;
                                break;
                            case "TrangThai":
                                value = item.TrangThai;
                                break;
                            case "Hsd":
                                value = item.Hsd?.ToString("dd/MM/yyyy");
                                break;
                        }

                        worksheet.Cells[rowIndex, colIndex].Value = value;
                        colIndex++;
                    }
                    rowIndex++;
                }

                worksheet.Cells.AutoFitColumns();
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);
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

            // TODO: Lấy MaNhanVien từ session/login thực tế
            int maNhanVien = 1;

            try
            {
                var (hasUpdates, errors, updates) = khoHangBUS.NhapKhoTuExcel(ofd.FileName, maNhanVien);

                // Hiển thị kết quả
                string message = "";
                if (errors.Any())
                {
                    message += "Có lỗi trong quá trình nhập:\n" + string.Join("\n", errors) + "\n\n";
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
                    khoHangBUS.XuatFileMauNhapKho(saveFileDialog.FileName);
                    MessageBox.Show("Xuất file mẫu Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi có muốn mở file không
                    DialogResult result = MessageBox.Show("Bạn có muốn mở file mẫu Excel vừa xuất không?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi lưu file mẫu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (bindingListProducts != null && e.RowIndex < bindingListProducts.Count)
                    {
                        string tenDayDu = bindingListProducts[e.RowIndex].TenSanPham;
                        if (!string.IsNullOrEmpty(tenDayDu) && tenDayDu != tenSanPham)
                        {
                            toolTipTenSP.SetToolTip(dgvKhoHang, $"→ {tenDayDu}");
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

        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e) 
        { 
            ApplyFilters(); 
        }
        
        private void cboThuongHieu_SelectedIndexChanged(object sender, EventArgs e) 
        { 
            ApplyFilters(); 
        }
        
        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e) 
        { 
            ApplyFilters(); 
        }
        
        private void txtTimKiem_TextChanged(object sender, EventArgs e) 
        { 
            ApplyFilters(); 
        }
    }
}

