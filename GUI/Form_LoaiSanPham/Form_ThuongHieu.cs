using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;
using mini_supermarket.GUI.Form_LoaiSanPham.Dialogs;

namespace mini_supermarket.GUI.Form_LoaiSanPham
{
    public partial class Form_ThuongHieu : Form
    {
        private const string FunctionPath = "Form_ThuongHieu";
        private readonly ThuongHieu_BUS _bus = new();
        private readonly BindingSource _binding = new();
        private readonly PermissionService _permissionService = new();

        public Form_ThuongHieu()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeStatusFilter();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _permissionService.ReloadPermissions();
            ApplyPermissions();
            ApplyFilters();
        }

        private void ApplyPermissions()
        {
            FormPermissionHelper.ApplyCRUDPermissions(
                _permissionService,
                FunctionPath,
                addButton: addButton,
                editButton: editButton,
                deleteButton: deleteButton
            );

            UpdateButtonsState(); // BẮT BUỘC GỌI ĐỂ NÚT SỬA/XÓA SÁNG NGAY
        }

        private void UpdateButtonsState()
        {
            bool hasSelection = thuongHieuDataGridView.CurrentRow != null;

            bool canEdit   = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
            bool canDelete = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa);

            editButton.Enabled   = hasSelection && canEdit;
            deleteButton.Enabled = hasSelection && canDelete;
        }

        private void InitializeStatusFilter()
        {
            statusFilterComboBox.Items.Clear();
            foreach (var option in TrangThaiConstants.ComboBoxOptions)
            {
                statusFilterComboBox.Items.Add(option);
            }
            statusFilterComboBox.SelectedIndex = 0;
            statusFilterComboBox.SelectedIndexChanged += (_, _) => ApplyFilters();
        }

        private void InitializeGrid()
        {
            thuongHieuDataGridView.AutoGenerateColumns = false;
            thuongHieuDataGridView.AllowUserToAddRows = false;
            thuongHieuDataGridView.AllowUserToDeleteRows = false;
            thuongHieuDataGridView.ReadOnly = true;
            thuongHieuDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            thuongHieuDataGridView.RowHeadersVisible = false;
            thuongHieuDataGridView.Columns.Clear();

            var maColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ThuongHieuDTO.MaThuongHieu),
                HeaderText = "Mã thương hiệu",
                Name = "MaThuongHieuColumn",
                Width = 160
            };

            var tenColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ThuongHieuDTO.TenThuongHieu),
                HeaderText = "Tên thương hiệu",
                Name = "TenThuongHieuColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var trangThaiColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ThuongHieuDTO.TrangThai),
                HeaderText = "Trạng thái",
                Name = "TrangThaiColumn",
                Width = 150
            };

            thuongHieuDataGridView.Columns.AddRange(maColumn, tenColumn, trangThaiColumn);
            thuongHieuDataGridView.DataSource = _binding;
        }

        private void ApplyFilters()
        {
            string keyword = searchTextBox.Text?.Trim() ?? string.Empty;
            string? status = GetSelectedStatus();

            IList<ThuongHieuDTO> list = string.IsNullOrEmpty(keyword)
                ? _bus.GetThuongHieuList(status)
                : _bus.Search(keyword, status);

            _binding.DataSource = list;

            // CHỈ ClearSelection – KHÔNG ĐƯỢC ĐỤNG CurrentCell = null
            thuongHieuDataGridView.ClearSelection();

            // Tự động chọn dòng đầu tiên nếu có dữ liệu → nút Sửa/Xóa sáng ngay!
            if (_binding.Count > 0)
            {
                _binding.Position = 0;
                var firstRow = thuongHieuDataGridView.Rows[0];
                firstRow.Selected = true;
                thuongHieuDataGridView.CurrentCell = firstRow.Cells[0];
            }

            UpdateButtonsState();
        }

        private string? GetSelectedStatus()
        {
            if (statusFilterComboBox.SelectedItem is not string option) return null;
            return string.Equals(option, TrangThaiConstants.ComboBoxOptions[0], StringComparison.CurrentCultureIgnoreCase)
                ? null : option;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            statusFilterComboBox.SelectedIndex = 0;
            searchTextBox.Clear();
            ApplyFilters();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them))
            {
                MessageBox.Show("Bạn không có quyền thêm thương hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new ThemThuongHieuDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CreatedThuongHieu != null)
                ApplyFilters();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua))
            {
                MessageBox.Show("Bạn không có quyền sửa thương hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (thuongHieuDataGridView.CurrentRow?.DataBoundItem is not ThuongHieuDTO selected)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var snapshot = new ThuongHieuDTO(selected.MaThuongHieu, selected.TenThuongHieu, selected.TrangThai);
            using var dialog = new SuaThuongHieuDialog(snapshot);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.UpdatedThuongHieu != null)
                ApplyFilters();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa))
            {
                MessageBox.Show("Bạn không có quyền khóa thương hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (thuongHieuDataGridView.CurrentRow?.DataBoundItem is not ThuongHieuDTO selected)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu để khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.Equals(selected.TrangThai, TrangThaiConstants.NgungHoatDong, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show($"Thương hiệu \"{selected.TenThuongHieu}\" đã bị khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(
                    $"Bạn có chắc muốn khóa thương hiệu \"{selected.TenThuongHieu}\" (Mã {selected.MaThuongHieu}) không?",
                    "Xác nhận khóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                _bus.DeleteThuongHieu(selected.MaThuongHieu);
                ApplyFilters();
                MessageBox.Show("Khóa thương hiệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void thuongHieuDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }
    }
}