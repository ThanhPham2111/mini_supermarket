using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;
using mini_supermarket.GUI.Form_LoaiSanPham.Dialogs;

namespace mini_supermarket.GUI.Form_LoaiSanPham
{
    public partial class Form_DonVi : Form
    {
        private const string FunctionPath = "Form_DonVi";
        private readonly DonVi_BUS _bus = new();
        private readonly BindingSource _binding = new();
        private readonly PermissionService _permissionService = new();

        public Form_DonVi()
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
            bool hasSelection = donViDataGridView.CurrentRow != null;

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
            donViDataGridView.AutoGenerateColumns = false;
            donViDataGridView.AllowUserToAddRows = false;
            donViDataGridView.AllowUserToDeleteRows = false;
            donViDataGridView.ReadOnly = true;
            donViDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            donViDataGridView.RowHeadersVisible = false;
            donViDataGridView.Columns.Clear();

            donViDataGridView.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(DonViDTO.MaDonVi), HeaderText = "Mã đơn vị", Width = 140 },
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(DonViDTO.TenDonVi), HeaderText = "Tên đơn vị", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill },
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(DonViDTO.MoTa), HeaderText = "Mô tả", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill },
                new DataGridViewTextBoxColumn { DataPropertyName = nameof(DonViDTO.TrangThai), HeaderText = "Trạng thái", Width = 150 }
            );

            donViDataGridView.DataSource = _binding;
        }

        private void ApplyFilters()
        {
            string keyword = searchTextBox.Text?.Trim() ?? string.Empty;
            string? status = GetSelectedStatus();

            IList<DonViDTO> list = string.IsNullOrEmpty(keyword)
                ? _bus.GetDonViList(status)
                : _bus.Search(keyword, status);

            _binding.DataSource = list;

            donViDataGridView.ClearSelection();

            if (_binding.Count > 0)
            {
                _binding.Position = 0;
                var firstRow = donViDataGridView.Rows[0];
                firstRow.Selected = true;
                donViDataGridView.CurrentCell = firstRow.Cells[0];
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
                MessageBox.Show("Bạn không có quyền thêm đơn vị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new ThemDonViDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CreatedDonVi != null)
                ApplyFilters();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua))
            {
                MessageBox.Show("Bạn không có quyền sửa đơn vị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (donViDataGridView.CurrentRow?.DataBoundItem is not DonViDTO selected)
            {
                MessageBox.Show("Vui lòng chọn đơn vị để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var snapshot = new DonViDTO(selected.MaDonVi, selected.TenDonVi, selected.MoTa, selected.TrangThai);
            using var dialog = new SuaDonViDialog(snapshot);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.UpdatedDonVi != null)
                ApplyFilters();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa))
            {
                MessageBox.Show("Bạn không có quyền khóa đơn vị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (donViDataGridView.CurrentRow?.DataBoundItem is not DonViDTO selected)
            {
                MessageBox.Show("Vui lòng chọn đơn vị để khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.Equals(selected.TrangThai, TrangThaiConstants.NgungHoatDong, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show($"Đơn vị \"{selected.TenDonVi}\" đã bị khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(
                    $"Bạn có chắc muốn khóa đơn vị \"{selected.TenDonVi}\" (Mã {selected.MaDonVi}) không?",
                    "Xác nhận khóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                _bus.DeleteDonVi(selected.MaDonVi);
                ApplyFilters();
                MessageBox.Show("Khóa đơn vị thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void donViDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }
    }
}