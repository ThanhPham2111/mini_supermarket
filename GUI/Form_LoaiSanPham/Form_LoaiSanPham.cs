using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;
using mini_supermarket.GUI.Form_LoaiSanPham.Dialogs;

namespace mini_supermarket.GUI.Form_LoaiSanPham
{
    public partial class Form_LoaiSanPham : Form
    {
        private const string FunctionPath = "Form_LoaiSanPham";
        private readonly LoaiSanPham_BUS _loaiBus = new();
        private readonly BindingSource _loaiBindingSource = new();
        private readonly PermissionService _permissionService = new();
        private Form? _activeEmbeddedForm;

        public Form_LoaiSanPham()
        {
            InitializeComponent();
            InitializeLoaiGrid();
            InitializeLoaiStatusFilter();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _permissionService.ReloadPermissions();
            ApplyPermissions();
            ApplyLoaiFilters();
        }

       private void ApplyPermissions()
{
    _permissionService.ReloadPermissions();

    // DÙNG HELPER MỚI – CHỈ 1 DÒNG LÀ XONG!
    FormPermissionHelper.ApplyCRUDPermissions(
        _permissionService,
        FunctionPath,
        addButton: addLoaiButton,
        editButton: editLoaiButton,
        deleteButton: deleteLoaiButton
    );

    // Sau khi bật nút theo quyền, thì UpdateLoaiButtonsState() sẽ tắt nếu chưa chọn dòng
    UpdateLoaiButtonsState();
}

        private void InitializeLoaiStatusFilter()
        {
            loaiStatusFilterComboBox.Items.Clear();
            foreach (var option in TrangThaiConstants.ComboBoxOptions)
            {
                loaiStatusFilterComboBox.Items.Add(option);
            }
            loaiStatusFilterComboBox.SelectedIndex = 0;
            loaiStatusFilterComboBox.SelectedIndexChanged += (_, _) => ApplyLoaiFilters();
        }

        private void ShowEmbeddedFormInCurrentTab(Form form)
        {
            _activeEmbeddedForm?.Close();
            _activeEmbeddedForm?.Dispose();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            if (mainTabControl.SelectedTab is { } selectedTab)
            {
                selectedTab.Controls.Clear();
                selectedTab.Controls.Add(form);
                form.Show();
                _activeEmbeddedForm = form;
            }
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab == tabLoai)
            {
                tabLoai.Controls.Clear();
                tabLoai.Controls.Add(loaiDataGridView);
                tabLoai.Controls.Add(listHeaderLabel);
                tabLoai.Controls.Add(buttonsFlowPanel);
                tabLoai.Controls.Add(searchContainerPanel);

                _activeEmbeddedForm?.Close();
                _activeEmbeddedForm?.Dispose();
                _activeEmbeddedForm = null;

                ApplyLoaiFilters();
                return;
            }

            if (mainTabControl.SelectedTab == tabThuongHieu)
            {
                var form = new Form_ThuongHieu();
                ShowEmbeddedFormInCurrentTab(form);
            }
            else if (mainTabControl.SelectedTab == tabDonVi)
            {
                var form = new Form_DonVi();
                ShowEmbeddedFormInCurrentTab(form);
            }
        }

        private void InitializeLoaiGrid()
        {
            loaiDataGridView.AutoGenerateColumns = false;
            loaiDataGridView.AllowUserToAddRows = false;
            loaiDataGridView.AllowUserToDeleteRows = false;
            loaiDataGridView.ReadOnly = true;
            loaiDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            loaiDataGridView.RowHeadersVisible = false;
            loaiDataGridView.Columns.Clear();

            loaiDataGridView.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(LoaiDTO.MaLoai),
                    HeaderText = "Mã loại",
                    Name = "MaLoaiColumn",
                    Width = 120
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(LoaiDTO.TenLoai),
                    HeaderText = "Tên loại",
                    Name = "TenLoaiColumn",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(LoaiDTO.MoTa),
                    HeaderText = "Mô tả",
                    Name = "MoTaColumn",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(LoaiDTO.TrangThai),
                    HeaderText = "Trạng thái",
                    Name = "TrangThaiColumn",
                    Width = 150
                }
            );

            loaiDataGridView.DataSource = _loaiBindingSource;
        }

        private void ApplyLoaiFilters()
        {
            string keyword = searchTextBox.Text?.Trim() ?? string.Empty;
            string? statusFilter = GetSelectedStatus(loaiStatusFilterComboBox);

            IList<LoaiDTO> list = string.IsNullOrEmpty(keyword)
                ? _loaiBus.GetLoaiList(statusFilter)
                : _loaiBus.SearchLoai(keyword, statusFilter);

            _loaiBindingSource.DataSource = list;

            // Reset selection để tránh lỗi khi dữ liệu thay đổi
            loaiDataGridView.ClearSelection();
            UpdateLoaiButtonsState(); // Rất quan trọng!
        }

       private void UpdateLoaiButtonsState()
{
    // SỬA TẠI ĐÂY: Dùng CurrentRow thay vì SelectedRows
    // → Vì ClearSelection() làm SelectedRows = 0, nhưng CurrentRow vẫn còn!
    bool hasSelection = loaiDataGridView.CurrentRow != null;

    bool canEdit   = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
    bool canDelete = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa);

    editLoaiButton.Enabled   = canEdit   && hasSelection;
    deleteLoaiButton.Enabled = canDelete && hasSelection;
}

        private static string? GetSelectedStatus(ComboBox comboBox)
        {
            if (comboBox.SelectedItem is not string option) return null;

            return string.Equals(option, TrangThaiConstants.ComboBoxOptions[0], StringComparison.OrdinalIgnoreCase)
                ? null
                : option;
        }

        private void refreshLoaiButton_Click(object sender, EventArgs e)
        {
            loaiStatusFilterComboBox.SelectedIndex = 0;
            searchTextBox.Clear();
            ApplyLoaiFilters();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyLoaiFilters();
        }

        private void addLoaiButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them))
            {
                MessageBox.Show("Bạn không có quyền thêm loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new ThemLoaiDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CreatedLoai != null)
            {
                ApplyLoaiFilters();
            }
        }

        private void editLoaiButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua))
            {
                MessageBox.Show("Bạn không có quyền sửa loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (loaiDataGridView.CurrentRow?.DataBoundItem is not LoaiDTO selected)
            {
                MessageBox.Show("Vui lòng chọn một loại sản phẩm để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var snapshot = new LoaiDTO(selected.MaLoai, selected.TenLoai, selected.MoTa, selected.TrangThai);
            using var dialog = new SuaLoaiDialog(snapshot);

            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.UpdatedLoai != null)
            {
                ApplyLoaiFilters();
            }
        }

        private void deleteLoaiButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa))
            {
                MessageBox.Show("Bạn không có quyền khóa loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (loaiDataGridView.CurrentRow?.DataBoundItem is not LoaiDTO selected)
            {
                MessageBox.Show("Vui lòng chọn một loại sản phẩm để khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.Equals(selected.TrangThai, TrangThaiConstants.NgungHoatDong, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Loại \"{selected.TenLoai}\" đã bị khóa rồi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn khóa loại sản phẩm \"{selected.TenLoai}\" (Mã: {selected.MaLoai}) không?",
                "Xác nhận khóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _loaiBus.DeleteLoai(selected.MaLoai);
                ApplyLoaiFilters();
                MessageBox.Show("Khóa loại sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khóa loại sản phẩm:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loaiDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateLoaiButtonsState();
        }
    }
}