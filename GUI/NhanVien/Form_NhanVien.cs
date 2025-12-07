using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;
using ClosedXML.Excel;

namespace mini_supermarket.GUI.NhanVien
{
    public partial class Form_NhanVien : Form
    {
        private const string StatusAll = "Tất cả";
        private const string FunctionPath = "Form_NhanVien";

        private readonly NhanVien_BUS _nhanVienBus = new();
        private readonly BindingSource _bindingSource = new();
        private readonly PermissionService _permissionService = new();

        private readonly List<string> _roles;
        private readonly List<string> _statuses;
        private IList<NhanVienDTO> _allNhanVien = new List<NhanVienDTO>();

        public Form_NhanVien()
        {
            InitializeComponent();
            Load += Form_NhanVien_Load;

            try
            {
                _roles = _nhanVienBus.GetDefaultRoles().ToList();
                _statuses = _nhanVienBus.GetDefaultStatuses().ToList();
            }
            catch
            {
                _roles = new List<string> { "Admin", "Quản lý", "Thu ngân", "Thủ kho" };
                _statuses = new List<string> { NhanVien_BUS.StatusWorking, NhanVien_BUS.StatusInactive };
            }
        }

        private void Form_NhanVien_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // ComboBox trạng thái
            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add(StatusAll);
            statusFilterComboBox.Items.AddRange(_statuses.ToArray());
            statusFilterComboBox.SelectedIndex = 0;
            statusFilterComboBox.SelectedIndexChanged += (_, _) => ApplyFilters();

            // ComboBox chức vụ (dùng trong dialog, không cần ở form chính)
            chucVuComboBox.Items.Clear();
            chucVuComboBox.Items.AddRange(_roles.ToArray());

            // DataGridView
            nhanVienDataGridView.AutoGenerateColumns = false;
            nhanVienDataGridView.DataSource = _bindingSource;
            nhanVienDataGridView.SelectionChanged += (_, _) => UpdateSelectionUI();

            // Buttons
            themButton.Click += ThemButton_Click;
            suaButton.Click += SuaButton_Click;
            xoaButton.Click += XoaButton_Click;
            lamMoiButton.Click += (_, _) => RefreshAll();
            exportExcelButton.Click += ExportExcelButton_Click;
            importExcelButton.Click += ImportExcelButton_Click;

            // Tìm kiếm
            searchTextBox.TextChanged += (_, _) => ApplyFilters();

            ApplyPermissions();
            SetInputFieldsEnabled(false);
            LoadNhanVienData();
        }

        #region Phân quyền & UI

        private void ApplyPermissions()
        {
            bool canAdd = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them);
            bool canEdit = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
            bool canDelete = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa);

            themButton.Enabled = canAdd;
            suaButton.Enabled = canEdit && nhanVienDataGridView.SelectedRows.Count > 0;
            xoaButton.Enabled = canDelete && nhanVienDataGridView.SelectedRows.Count > 0;
        }

        private void UpdateSelectionUI()
        {
            if (nhanVienDataGridView.SelectedRows.Count == 0)
            {
                ClearInputFields();
                SetInputFieldsEnabled(false);
                ApplyPermissions();
                return;
            }

            var nv = nhanVienDataGridView.SelectedRows[0].DataBoundItem as NhanVienDTO;
            if (nv == null) return;

            maNhanVienTextBox.Text = nv.MaNhanVien.ToString();
            hoTenTextBox.Text = nv.TenNhanVien ?? "";
            ngaySinhDateTimePicker.Value = nv.NgaySinh ?? DateTime.Today;
            gioiTinhNamRadioButton.Checked = nv.GioiTinh == "Nam";
            gioiTinhNuRadioButton.Checked = nv.GioiTinh == "Nữ";
            chucVuComboBox.Text = nv.VaiTro ?? "";
            soDienThoaiTextBox.Text = nv.SoDienThoai ?? "";

            SetInputFieldsEnabled(false);
            ApplyPermissions();
        }

        private void SetInputFieldsEnabled(bool enabled)
        {
            hoTenTextBox.Enabled = enabled;
            ngaySinhDateTimePicker.Enabled = enabled;
            gioiTinhNamRadioButton.Enabled = enabled;
            gioiTinhNuRadioButton.Enabled = enabled;
            chucVuComboBox.Enabled = enabled;
            soDienThoaiTextBox.Enabled = enabled;
        }

        private void ClearInputFields()
        {
            maNhanVienTextBox.Clear();
            hoTenTextBox.Clear();
            ngaySinhDateTimePicker.Value = DateTime.Today;
            gioiTinhNamRadioButton.Checked = false;
            gioiTinhNuRadioButton.Checked = false;
            chucVuComboBox.SelectedIndex = -1;
            soDienThoaiTextBox.Clear();
        }

        #endregion

        #region CRUD

        private void ThemButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them))
            {
                MessageBox.Show("Bạn không có quyền thêm nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new Form_NhanVienDialog(_roles, _statuses);
            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var nv = _nhanVienBus.AddNhanVien(dialog.NhanVien);
                LoadNhanVienData();
                SelectRowById(nv.MaNhanVien);
                MessageBox.Show("Thêm nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Thêm thất bại!\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuaButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua))
            {
                MessageBox.Show("Bạn không có quyền sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = GetSelectedNhanVien();
            if (selected == null) return;

            using var dialog = new Form_NhanVienDialog(_roles, _statuses, selected);
            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                _nhanVienBus.UpdateNhanVien(dialog.NhanVien);
                LoadNhanVienData();
                SelectRowById(dialog.NhanVien.MaNhanVien);
                MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cập nhật thất bại!\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa))
            {
                MessageBox.Show("Bạn không có quyền khóa nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = GetSelectedNhanVien();
            if (selected == null) return;

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn khóa nhân viên \"{selected.TenNhanVien}\"?",
                "Xác nhận khóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                selected.TrangThai = NhanVien_BUS.StatusInactive;
                _nhanVienBus.UpdateNhanVien(selected);
                LoadNhanVienData();
                MessageBox.Show("Đã khóa nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Khóa thất bại!\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private NhanVienDTO GetSelectedNhanVien()
        {
            return nhanVienDataGridView.SelectedRows.Count > 0
                ? nhanVienDataGridView.SelectedRows[0].DataBoundItem as NhanVienDTO
                : null;
        }

        private void SelectRowById(int maNhanVien)
        {
            foreach (DataGridViewRow row in nhanVienDataGridView.Rows)
            {
                if (row.DataBoundItem is NhanVienDTO nv && nv.MaNhanVien == maNhanVien)
                {
                    row.Selected = true;
                    nhanVienDataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        #endregion

        #region Load & Filter

        private void LoadNhanVienData()
        {
            try
            {
                _allNhanVien = _nhanVienBus.GetNhanVien();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không tải được danh sách nhân viên!\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshAll()
        {
            searchTextBox.Clear();
            statusFilterComboBox.SelectedIndex = 0;
            LoadNhanVienData();
        }

        private void ApplyFilters()
        {
            var keyword = searchTextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));
            var status = statusFilterComboBox.SelectedItem?.ToString();

            var filtered = _allNhanVien.Where(nv =>
            {
                bool matchKeyword = string.IsNullOrEmpty(keyword) ||
                    nv.MaNhanVien.ToString().Contains(keyword) ||
                    (nv.TenNhanVien?.ToLower().Contains(keyword) ?? false) ||
                    (nv.SoDienThoai?.ToLower().Contains(keyword) ?? false);

                bool matchStatus = status == StatusAll ||
                    string.Equals(nv.TrangThai, status, StringComparison.OrdinalIgnoreCase);

                return matchKeyword && matchStatus;
            }).ToList();

            _bindingSource.DataSource = filtered.Any() ? filtered : null;
            nhanVienDataGridView.ClearSelection();
            UpdateSelectionUI();
        }

        #endregion

        #region Export Excel (Đẹp + Tiếng Việt chuẩn)

        private void ExportExcelButton_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Xuất danh sách nhân viên",
                FileName = $"NhanVien_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Danh sách nhân viên");

                // Header
                string[] headers = { "Mã NV", "Họ tên", "Ngày sinh", "Giới tính", "SĐT", "Vai trò", "Trạng thái" };
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = ws.Cell(1, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromArgb(211, 211, 211);
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                var data = _bindingSource.DataSource as IEnumerable<NhanVienDTO> ?? new List<NhanVienDTO>();
                int row = 2;
                foreach (var nv in data)
                {
                    ws.Cell(row, 1).Value = nv.MaNhanVien;
                    ws.Cell(row, 2).Value = nv.TenNhanVien;
                    ws.Cell(row, 3).Value = nv.NgaySinh.HasValue ? nv.NgaySinh.Value.ToString("dd/MM/yyyy") : "";
                    ws.Cell(row, 4).Value = nv.GioiTinh ?? "";
                    ws.Cell(row, 5).Value = nv.SoDienThoai;
                    ws.Cell(row, 6).Value = nv.VaiTro;
                    ws.Cell(row, 7).Value = nv.TrangThai;
                    row++;
                }

                // Format bảng
                var range = ws.Range(1, 1, row - 1, headers.Length);
                range.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                range.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                ws.Columns(1, headers.Length).AdjustToContents();
                ws.Rows(1, row - 1).AdjustToContents();
                ws.SheetView.FreezeRows(1);

                wb.SaveAs(sfd.FileName);
                MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất file:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Import Excel

        private void ImportExcelButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Chọn file Excel nhân viên"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            var importList = new List<NhanVienDTO>();
            var readErrors = new List<string>();

            try
            {
                using var wb = new XLWorkbook(ofd.FileName);
                var ws = wb.Worksheets.First();
                var rows = ws.RowsUsed().Skip(1); // Bỏ header
                int rowNum = 2;

                foreach (var row in rows)
                {
                    try
                    {
                        // Đọc dữ liệu từ Excel
                        var maNhanVienStr = row.Cell(1).GetString().Trim();
                        var tenNhanVienStr = row.Cell(2).GetString().Trim();
                        var ngaySinhStr = row.Cell(3).GetString().Trim();
                        var gioiTinhStr = row.Cell(4).GetString().Trim();
                        var soDienThoaiStr = row.Cell(5).GetString().Trim();
                        var vaiTroStr = row.Cell(6).GetString().Trim();
                        var trangThaiStr = row.Cell(7).GetString().Trim();

                        // Bỏ qua dòng trống
                        if (string.IsNullOrEmpty(maNhanVienStr) && string.IsNullOrEmpty(tenNhanVienStr) &&
                            string.IsNullOrEmpty(soDienThoaiStr) && string.IsNullOrEmpty(vaiTroStr))
                        {
                            rowNum++;
                            continue;
                        }

                        // Parse mã nhân viên
                        if (!int.TryParse(maNhanVienStr, out int maNhanVien) || maNhanVien <= 0)
                        {
                            rowNum++;
                            continue;
                        }

                        // Kiểm tra đã tồn tại - bỏ qua không báo lỗi
                        if (_allNhanVien.Any(nv => nv.MaNhanVien == maNhanVien))
                        {
                            rowNum++;
                            continue;
                        }

                        // Tạo DTO
                        var nv = new NhanVienDTO
                        {
                            MaNhanVien = maNhanVien,
                            TenNhanVien = tenNhanVienStr,
                            NgaySinh = TryParseDate(ngaySinhStr),
                            GioiTinh = NormalizeGender(gioiTinhStr),
                            SoDienThoai = soDienThoaiStr,
                            VaiTro = vaiTroStr,
                            TrangThai = string.IsNullOrWhiteSpace(trangThaiStr)
                                ? NhanVien_BUS.StatusWorking
                                : trangThaiStr
                        };

                        importList.Add(nv);
                    }
                    catch (Exception ex)
                    {
                        readErrors.Add($"Dòng {rowNum}: {ex.Message}");
                    }
                    rowNum++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đọc file Excel:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (importList.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hợp lệ để nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Import vào DB
            int success = 0;
            var saveErrors = new List<string>();

            foreach (var nv in importList)
            {
                try
                {
                    // AddNhanVien sẽ tự validate
                    _nhanVienBus.AddNhanVien(nv);
                    success++;
                }
                catch (Exception ex)
                {
                    saveErrors.Add($"Mã NV {nv.MaNhanVien} ({nv.TenNhanVien}): {ex.Message}");
                }
            }

            LoadNhanVienData();

            // Hiển thị kết quả
            string result = $"✅ Nhập Excel thành công!\n\n";
            result += $"• Đã thêm: {success} nhân viên mới\n";
            if (saveErrors.Count > 0)
            {
                result += $"• Lỗi: {saveErrors.Count} nhân viên\n";
                result += "\nChi tiết lỗi:\n" + string.Join("\n", saveErrors.Take(10));
                if (saveErrors.Count > 10)
                {
                    result += $"\n... và {saveErrors.Count - 10} lỗi khác.";
                }
                MessageBox.Show(result, "Kết quả nhập Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(result, "Kết quả nhập Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DateTime? TryParseDate(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            var formats = new[] { "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "ddMMyyyy" };
            if (DateTime.TryParseExact(input.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                return dt;
            if (DateTime.TryParse(input.Trim(), out DateTime dt2))
                return dt2;
            return null;
        }

        private string? NormalizeGender(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            var lower = input.Trim().ToLower();
            if (lower.Contains("nam") || lower == "1") return "Nam";
            if (lower.Contains("nữ") || lower.Contains("nu") || lower == "0") return "Nữ";
            return null;
        }

        #endregion
    }
}