using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;
using ClosedXML.Excel;
using System.Data;
using System.ComponentModel;

namespace mini_supermarket.GUI.KhachHang
{
    public partial class Form_KhachHang : Form
    {
        private const string StatusAll = "Tất cả";
        private const string FunctionPath = "Form_KhachHang";

        private readonly KhachHang_BUS _khachHangBus = new();
        private readonly BindingSource _bindingSource = new();
        private readonly PermissionService _permissionService = new();
        // private readonly List<string> _roles;
        private readonly List<string> _statuses;
        private BindingList<KhachHangDTO> _currentKhachHang = new();

        public Form_KhachHang()
        {
            InitializeComponent();
            Load += Form_KhachHang_Load;

            // _roles = _khachHangBus.GetDefaultRoles().ToList();
            _statuses = _khachHangBus.GetDefaultStatuses().ToList();
            LoadKhachHangData();
        }

        private void Form_KhachHang_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add(StatusAll);
            foreach (var status in _statuses)
            {
                statusFilterComboBox.Items.Add(status);
            }
            statusFilterComboBox.SelectedIndex = 0;
            statusFilterComboBox.SelectedIndexChanged += statusFilterComboBox_SelectedIndexChanged;

            // chucVuComboBox.Items.Clear();
            // foreach (var role in _roles)
            // {
            //     chucVuComboBox.Items.Add(role);
            // }
            // chucVuComboBox.SelectedIndex = -1;

            khachHangDataGridView.AutoGenerateColumns = false;
            khachHangDataGridView.DataSource = _bindingSource;
            khachHangDataGridView.SelectionChanged += khachHangDataGridView_SelectionChanged;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(themButton, "Thêm khách hàng mới");
            toolTip.SetToolTip(suaButton, "Sửa thông tin khách hàng đã chọn");
            toolTip.SetToolTip(xoaButton, "Khóa khách hàng đã chọn"); // Updated: "Khóa" instead of "Xóa"
            toolTip.SetToolTip(lamMoiButton, "Làm mới danh sách");
            toolTip.SetToolTip(searchButton, "Tìm kiếm khách hàng");

            themButton.Click += themButton_Click;
            suaButton.Click += suaButton_Click;
            xoaButton.Click += xoaButton_Click;
            lamMoiButton.Click += lamMoiButton_Click;
            searchButton.Click += (_, _) => ApplySearchFilter();

            searchTextBox.TextChanged += searchTextBox_TextChanged;

            importExcelButton.Click += ImportExcelButton_Click;
            exportExcelButton.Click += ExportExcelButton_Click;

            ApplyPermissions();

            SetInputFieldsEnabled(false);

            // LoadKhachHangData();
            _bindingSource.DataSource = _currentKhachHang;
        }

        private void ApplyPermissions()
        {
            bool canAdd = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them);
            bool canEdit = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
            bool canDelete = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa);

            themButton.Enabled = canAdd;
            lamMoiButton.Enabled = true;
            suaButton.Enabled = canEdit && khachHangDataGridView.SelectedRows.Count > 0;
            xoaButton.Enabled = canDelete && khachHangDataGridView.SelectedRows.Count > 0;
        }

        private void UpdateButtonsState()
        {
            bool hasSelection = khachHangDataGridView.SelectedRows.Count > 0;
            bool canEdit = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
            bool canDelete = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa);

            suaButton.Enabled = hasSelection && canEdit;
            xoaButton.Enabled = hasSelection && canDelete;
        }

        private void khachHangDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            if (khachHangDataGridView.SelectedRows.Count > 0)
            {
                var selectedKhachHang = (KhachHangDTO)khachHangDataGridView.SelectedRows[0].DataBoundItem;
                maKhachHangTextBox.Text = selectedKhachHang.MaKhachHang.ToString();
                hoTenTextBox.Text = selectedKhachHang.TenKhachHang ?? string.Empty;
                // ngaySinhDateTimePicker.Value = selectedKhachHang.NgaySinh ?? DateTime.Today;
                // gioiTinhNamRadioButton.Checked = selectedKhachHang.GioiTinh == "Nam";
                // gioiTinhNuRadioButton.Checked = selectedKhachHang.GioiTinh == "Nữ";
                // if (!string.IsNullOrEmpty(selectedKhachHang.VaiTro) && chucVuComboBox.Items.Contains(selectedKhachHang.VaiTro))
                // {
                //     chucVuComboBox.SelectedItem = selectedKhachHang.VaiTro;
                // }
                // else
                // {
                //     chucVuComboBox.SelectedIndex = -1;
                // }
                soDienThoaiTextBox.Text = selectedKhachHang.SoDienThoai ?? string.Empty;
                diaChiTextBox.Text = selectedKhachHang.DiaChi ?? string.Empty;
                emailTextBox.Text = selectedKhachHang.Email ?? string.Empty;
                diemTichLuyTextBox.Text = selectedKhachHang.DiemTichLuy.ToString() ?? "0";

                UpdateButtonsState();
                SetInputFieldsEnabled(false);
            }
            else
            {
                maKhachHangTextBox.Text = string.Empty;
                hoTenTextBox.Text = string.Empty;
                // ngaySinhDateTimePicker.Value = DateTime.Today;
                // gioiTinhNamRadioButton.Checked = false;
                // gioiTinhNuRadioButton.Checked = false;
                // chucVuComboBox.SelectedIndex = -1;
                soDienThoaiTextBox.Text = string.Empty;
                diaChiTextBox.Text = string.Empty;
                emailTextBox.Text = string.Empty;
                diemTichLuyTextBox.Text = string.Empty; 

                UpdateButtonsState();

                SetInputFieldsEnabled(false);
            }
        }

        private void themButton_Click(object? sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them))
            {
                MessageBox.Show("Bạn không có quyền thêm khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new Form_KhachHangDialog(_statuses);
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                var createdKhachHang = _khachHangBus.AddKhachHang(dialog.KhachHang);
                // Thêm khách hàng mới vào BindingList để tự động cập nhật DataGridView
                _currentKhachHang.Add(createdKhachHang);
                SelectKhachHangRow(createdKhachHang.MaKhachHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể thêm khách hàng.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void suaButton_Click(object? sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua))
            {
                MessageBox.Show("Bạn không có quyền sửa khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedKhachHang = GetSelectedKhachHang();
            if (selectedKhachHang == null)
            {
                return;
            }

            using var dialog = new Form_KhachHangDialog(_statuses, selectedKhachHang);
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                _khachHangBus.UpdateKhachHang(dialog.KhachHang);
                // Cập nhật khách hàng trong BindingList để tự động cập nhật DataGridView
                var index = _currentKhachHang.ToList().FindIndex(kh => kh.MaKhachHang == dialog.KhachHang.MaKhachHang);
                if (index >= 0)
                {
                    _currentKhachHang[index] = dialog.KhachHang;
                }
                SelectKhachHangRow(dialog.KhachHang.MaKhachHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể cập nhật khách hàng.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xoaButton_Click(object? sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa))
            {
                MessageBox.Show("Bạn không có quyền khóa khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedKhachHang = GetSelectedKhachHang();
            if (selectedKhachHang == null)
            {
                return;
            }

            DialogResult confirm = MessageBox.Show(this,
                $"Bạn có chắc muốn khóa khách hàng '{selectedKhachHang.TenKhachHang}'? Trạng thái sẽ được chuyển thành 'Khóa'.",
                "Xác nhận khóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                selectedKhachHang.TrangThai = KhachHang_BUS.StatusInactive; // Set TrangThai to "Khóa"
                _khachHangBus.UpdateKhachHang(selectedKhachHang);
                // Cập nhật trạng thái trong BindingList để tự động cập nhật DataGridView
                var index = _currentKhachHang.ToList().FindIndex(kh => kh.MaKhachHang == selectedKhachHang.MaKhachHang);
                if (index >= 0)
                {
                    _currentKhachHang[index].TrangThai = KhachHang_BUS.StatusInactive;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể khóa khách hàng.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lamMoiButton_Click(object? sender, EventArgs e)
        {
            searchTextBox.Text = string.Empty;

            if (statusFilterComboBox.SelectedIndex != 0)
            {
                statusFilterComboBox.SelectedIndex = 0;
            }
            else
            {
                ApplyStatusFilter();
            }

            LoadKhachHangData();
        }

        private KhachHangDTO? GetSelectedKhachHang()
        {
            if (khachHangDataGridView.SelectedRows.Count == 0)
            {
                return null;
            }

            return khachHangDataGridView.SelectedRows[0].DataBoundItem as KhachHangDTO;
        }

        private void SelectKhachHangRow(int maKhachHang)
        {
            if (maKhachHang <= 0 || khachHangDataGridView.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in khachHangDataGridView.Rows)
            {
                if (row.DataBoundItem is KhachHangDTO khachHang && khachHang.MaKhachHang == maKhachHang)
                {
                    row.Selected = true;
                    try
                    {
                        khachHangDataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                    catch
                    {
                        // Ignore if cannot set scroll index
                    }

                    return;
                }
            }
        }

        private void SetInputFieldsEnabled(bool enabled)
        {
            maKhachHangTextBox.Enabled = enabled;
            hoTenTextBox.Enabled = enabled;
            // ngaySinhDateTimePicker.Enabled = enabled;
            // gioiTinhNamRadioButton.Enabled = enabled;
            // gioiTinhNuRadioButton.Enabled = enabled;
            // chucVuComboBox.Enabled = enabled;
            diaChiTextBox.Enabled = enabled;
            emailTextBox.Enabled = enabled;
            diemTichLuyTextBox.Enabled = enabled;
            soDienThoaiTextBox.Enabled = enabled;
        }

        private void statusFilterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyStatusFilter();
        }

        private void searchTextBox_TextChanged(object? sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void LoadKhachHangData()
        {
            try
            {
                _currentKhachHang = new BindingList<KhachHangDTO>(_khachHangBus.GetKhachHang());
                ApplyStatusFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể tải danh sách khách hàng.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStatusFilter()
        {
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(selectedStatus) || string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase))
            {
                _bindingSource.DataSource = _currentKhachHang;
            }
            else
            {
                var filtered = new List<KhachHangDTO>();
                foreach (var khachHang in _currentKhachHang)
                {
                    if (string.Equals(khachHang.TrangThai, selectedStatus, StringComparison.OrdinalIgnoreCase))
                    {
                        filtered.Add(khachHang);
                    }
                }
                _bindingSource.DataSource = filtered;
                
            }

            if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
            {
                ApplySearchFilter();
            }
            else
            {
                khachHangDataGridView.ClearSelection();
            }
        }

        private void ApplySearchFilter()
        {
            string searchText = searchTextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            var filtered = new List<KhachHangDTO>();

            foreach (var khachHang in _currentKhachHang)
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                    khachHang.MaKhachHang.ToString().ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ||
                    (khachHang.TenKhachHang?.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ?? false) ||
                    (khachHang.SoDienThoai?.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ?? false);

                bool matchesStatus = string.IsNullOrWhiteSpace(selectedStatus) ||
                    string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(khachHang.TrangThai, selectedStatus, StringComparison.OrdinalIgnoreCase);

                if (matchesSearch && matchesStatus)
                {
                    filtered.Add(khachHang);
                }
            }

            _bindingSource.DataSource = filtered;
            khachHangDataGridView.ClearSelection();
            UpdateButtonsState();
        }
        private void ExportExcelButton_Click(object? sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Lưu Excel"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            DataTable dt = new DataTable();

            // Lấy header
            foreach (DataGridViewColumn col in khachHangDataGridView.Columns)
                dt.Columns.Add(col.HeaderText);

            // Lấy dữ liệu
            foreach (DataGridViewRow row in khachHangDataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                var data = new object[row.Cells.Count];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    data[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(data);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Khách Hàng");
                wb.SaveAs(sfd.FileName);
            }

            MessageBox.Show("✅ Xuất Excel thành công!");
        }

        private void ImportExcelButton_Click(object? sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Chọn file Excel"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            DataTable dt = new DataTable();
            List<KhachHangDTO> khachHangList = new List<KhachHangDTO>();

            using (XLWorkbook wb = new XLWorkbook(ofd.FileName))
            {
                var ws = wb.Worksheet(1);
                var rows = ws.RowsUsed().Skip(1); // Skip header row
                foreach(var row in rows)
                {
                    try
                    {
                        var maKhachHangStr = row.Cell(1).Value.ToString().Trim();
                        var hoTenStr = row.Cell(2).Value.ToString().Trim();
                        var soDienThoaiStr = row.Cell(3).Value.ToString().Trim();
                        var diaChiStr = row.Cell(4).Value.ToString().Trim();
                        var emailStr = row.Cell(5).Value.ToString().Trim();
                        var diemTichLuyStr = row.Cell(6).Value.ToString().Trim();
                        var trangThaiStr = row.Cell(7).Value.ToString().Trim();
                        if (string.IsNullOrEmpty(maKhachHangStr) || maKhachHangStr == "0")
                        {
                            continue;
                        }
                        if (!int.TryParse(maKhachHangStr, out int maKhachHang) || maKhachHang <= 0)
                        {
                            Console.WriteLine($"Không thể parse MaKhachHang hoặc <= 0: '{maKhachHangStr}'");
                            continue;
                        }
                        // Kiểm tra xem MaKhachHang đã tồn tại chưa
                        if (_currentKhachHang.Any(h => h.MaKhachHang == maKhachHang))
                        {
                            Console.WriteLine($"MaKhachHang {maKhachHang} đã tồn tại, bỏ qua");
                            continue;
                        }
                        // Kiểm tra xem MaKhachHang có bị trùng trong file không
                        if (khachHangList.Any(h => h.MaKhachHang == maKhachHang))
                        {
                            Console.WriteLine($"MaKhachHang {maKhachHang} bị trùng trong file, bỏ qua");
                            continue;
                        }
                        var kh_tmp = new KhachHangDTO
                        {
                            MaKhachHang = maKhachHang,
                            TenKhachHang = hoTenStr,
                            SoDienThoai = soDienThoaiStr,
                            DiaChi = diaChiStr,
                            Email = emailStr,
                            DiemTichLuy = int.TryParse(diemTichLuyStr, out int diem) ? diem : 0,
                            TrangThai = trangThaiStr
                        };
                        khachHangList.Add(kh_tmp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi xử lý dòng: {ex.Message}");
                        continue;
                    }
                }
            }

            // Thêm các khách hàng mới vào database và BindingList
            int successCount = 0;
            foreach (var kh in khachHangList)
            {
                try
                {
                    var createdKh = _khachHangBus.AddKhachHang(kh);
                    // Thêm vào BindingList để tự động cập nhật DataGridView
                    _currentKhachHang.Add(createdKh);
                    successCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi thêm khách hàng {kh.MaKhachHang}: {ex.Message}");
                }
            }

            MessageBox.Show($"✅ Nhập Excel thành công! Đã thêm {successCount} khách hàng mới.");
        }
    }
}