using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_SanPham
{
    public partial class Form_SanPham : Form
    {
        private const string StatusAll = "Tất cả";
        private const string FunctionPath = "Form_SanPham";

        private readonly SanPham_BUS _sanPhamBus = new();
        private readonly BindingSource _bindingSource = new();
        private readonly PermissionService _permissionService = new();
        private IList<SanPhamDTO> _currentSanPham = Array.Empty<SanPhamDTO>();

        public Form_SanPham()
        {
            InitializeComponent();
            Load += Form_SanPham_Load;
        }

        private void Form_SanPham_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            sanPhamDataGridView.AutoGenerateColumns = false;
            sanPhamDataGridView.DataSource = _bindingSource;
            sanPhamDataGridView.SelectionChanged += SanPhamDataGridView_SelectionChanged;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(xemChiTietButton, "Xem chi tiết sản phẩm đã chọn");
            toolTip.SetToolTip(themButton, "Thêm sản phẩm mới");
            toolTip.SetToolTip(suaButton, "Sửa thông tin sản phẩm đã chọn");
            toolTip.SetToolTip(lamMoiButton, "Làm mới danh sách");
            toolTip.SetToolTip(searchButton, "Tìm kiếm sản phẩm");

            searchButton.Click += (_, _) => ApplyFilters();
            searchTextBox.TextChanged += (_, _) => ApplyFilters();
            themButton.Click += themButton_Click;
            suaButton.Click += suaButton_Click;
            lamMoiButton.Click += lamMoiButton_Click;
            xemChiTietButton.Click += xemChiTietButton_Click;

            InitializeStatusFilter();
            statusFilterComboBox.SelectedIndexChanged += statusFilterComboBox_SelectedIndexChanged;

            // Áp dụng quyền cho các button
            ApplyPermissions();

            LoadSanPhamData();
        }

        private void ApplyPermissions()
        {
            // Kiểm tra quyền Thêm
            bool canAdd = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them);
            themButton.Enabled = canAdd;

            // Kiểm tra quyền Sửa (sẽ được cập nhật khi có selection)
            // Kiểm tra quyền Xem (cho button xem chi tiết)
            bool canView = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xem);
            xemChiTietButton.Enabled = false; // Sẽ được cập nhật khi có selection
        }

        private void InitializeStatusFilter()
        {
            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add(StatusAll);

            foreach (var status in _sanPhamBus.GetAvailableStatuses())
            {
                if (!string.IsNullOrWhiteSpace(status))
                {
                    statusFilterComboBox.Items.Add(status);
                }
            }

            statusFilterComboBox.SelectedIndex = 0;
        }

        private void LoadSanPhamData(int? selectMaSanPham = null)
        {
            try
            {
                _currentSanPham = _sanPhamBus.GetSanPham();
                ApplyFilters();

                if (selectMaSanPham.HasValue)
                {
                    SelectSanPhamRow(selectMaSanPham.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể tải danh sách sản phẩm .{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void statusFilterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void lamMoiButton_Click(object? sender, EventArgs e)
        {
            bool statusChanged = statusFilterComboBox.SelectedIndex != 0;
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                searchTextBox.Clear();
            }

            if (statusChanged)
            {
                statusFilterComboBox.SelectedIndex = 0;
            }

            LoadSanPhamData();
        }

        private void ApplyFilters()
        {
            string searchText = searchTextBox.Text.Trim();
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            IEnumerable<SanPhamDTO> filtered = _currentSanPham;

            if (!string.IsNullOrWhiteSpace(selectedStatus) && !selectedStatus.Equals(StatusAll, StringComparison.CurrentCultureIgnoreCase))
            {
                filtered = filtered.Where(sp => string.Equals(sp.TrangThai?.Trim(), selectedStatus.Trim(), StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(sp => MatchesSearch(sp, searchText));
            }

            _bindingSource.DataSource = filtered.ToList();
            sanPhamDataGridView.ClearSelection();
            UpdateActionButtonsState();
        }

        private void SanPhamDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateActionButtonsState();
        }

        private void UpdateActionButtonsState()
        {
            bool hasSelection = sanPhamDataGridView.SelectedRows.Count > 0;
            
            // Kiểm tra quyền Xem
            bool canView = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xem);
            xemChiTietButton.Enabled = hasSelection && canView;
            
            // Kiểm tra quyền Sửa
            bool canUpdate = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
            suaButton.Enabled = hasSelection && canUpdate;
        }

        private void xemChiTietButton_Click(object? sender, EventArgs e)
        {
            // Kiểm tra quyền Xem
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xem))
            {
                MessageBox.Show("Bạn không có quyền xem chi tiết sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedSanPham = GetSelectedSanPham();
            if (selectedSanPham == null)
            {
                return;
            }

            using var dialog = new Form_SanPhamDialog(selectedSanPham);
            dialog.ShowDialog(this);
        }

        private SanPhamDTO? GetSelectedSanPham()
        {
            if (sanPhamDataGridView.SelectedRows.Count == 0)
            {
                return null;
            }

            return sanPhamDataGridView.SelectedRows[0].DataBoundItem as SanPhamDTO;
        }

        private void SelectSanPhamRow(int maSanPham)
        {
            if (maSanPham <= 0 || sanPhamDataGridView.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in sanPhamDataGridView.Rows)
            {
                if (row.DataBoundItem is SanPhamDTO sanPham && sanPham.MaSanPham == maSanPham)
                {
                    row.Selected = true;
                    try
                    {
                        sanPhamDataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                    catch
                    {
                        // Ignore scrolling issues
                    }

                    return;
                }
            }
        }

        private void themButton_Click(object? sender, EventArgs e)
        {
            // Kiểm tra quyền Thêm
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them))
            {
                MessageBox.Show("Bạn không có quyền thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new Form_SanPhamCreateDialog();
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.CreatedSanPham == null)
            {
                return;
            }

            if (statusFilterComboBox.SelectedIndex != 0)
            {
                statusFilterComboBox.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                searchTextBox.Clear();
            }

            LoadSanPhamData(dialog.CreatedSanPham.MaSanPham);
        }

        private void suaButton_Click(object? sender, EventArgs e)
        {
            // Kiểm tra quyền Sửa
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua))
            {
                MessageBox.Show("Bạn không có quyền sửa sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = GetSelectedSanPham();
            if (selected == null)
            {
                return;
            }

            using var dialog = new Form_SanPhamUpdate(selected);
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.UpdatedSanPham == null)
            {
                return;
            }

            if (statusFilterComboBox.SelectedIndex != 0)
            {
                statusFilterComboBox.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                searchTextBox.Clear();
            }

            LoadSanPhamData(dialog.UpdatedSanPham.MaSanPham);
        }
        private static bool MatchesSearch(SanPhamDTO sanPham, string searchText)
        {
            var comparison = StringComparison.CurrentCultureIgnoreCase;

            if (sanPham.MaSanPham.ToString().Contains(searchText, comparison))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(sanPham.TenSanPham) && sanPham.TenSanPham.Contains(searchText, comparison))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(sanPham.TenDonVi) && sanPham.TenDonVi.Contains(searchText, comparison))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(sanPham.TenLoai) && sanPham.TenLoai.Contains(searchText, comparison))
            {
                return true;
            }

            if (sanPham.MaDonVi != 0 && sanPham.MaDonVi.ToString().Contains(searchText, comparison))
            {
                return true;
            }

            if (sanPham.GiaBan.HasValue && sanPham.GiaBan.Value.ToString("N0", CultureInfo.CurrentCulture).Contains(searchText, comparison))
            {
                return true;
            }

            if (sanPham.MaLoai.ToString().Contains(searchText, comparison))
            {
                return true;
            }

            if (sanPham.Hsd.HasValue && sanPham.Hsd.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture).Contains(searchText, comparison))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(sanPham.TrangThai) && sanPham.TrangThai.Contains(searchText, comparison))
            {
                return true;
            }

            return false;
        }

    }
}






