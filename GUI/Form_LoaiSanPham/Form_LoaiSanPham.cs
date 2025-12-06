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
            ApplyPermissions();
            ApplyLoaiFilters();
        }

        private void ApplyPermissions()
        {
            // Áp dụng quyền cho các button Loại
            bool canAdd = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them);
            bool canEdit = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
            bool canDelete = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa);

            addLoaiButton.Enabled = canAdd;
            editLoaiButton.Enabled = canEdit && loaiDataGridView.SelectedRows.Count > 0;
            deleteLoaiButton.Enabled = canDelete && loaiDataGridView.SelectedRows.Count > 0;
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

            if (mainTabControl.SelectedTab is { } selected)
            {
                selected.Controls.Clear();
                selected.Controls.Add(form);
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
                var thuongHieuForm = new Form_ThuongHieu();
                ShowEmbeddedFormInCurrentTab(thuongHieuForm);
                return;
            }

            if (mainTabControl.SelectedTab == tabDonVi)
            {
                var donViForm = new Form_DonVi();
                ShowEmbeddedFormInCurrentTab(donViForm);
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

            var maLoaiColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(LoaiDTO.MaLoai),
                HeaderText = "Mã loại",
                Name = "MaLoaiColumn",
                Width = 120
            };

            var tenLoaiColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(LoaiDTO.TenLoai),
                HeaderText = "Tên loại",
                Name = "TenLoaiColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var moTaColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(LoaiDTO.MoTa),
                HeaderText = "Mô tả",
                Name = "MoTaColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var trangThaiColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(LoaiDTO.TrangThai),
                HeaderText = "Trạng thái",
                Name = "TrangThaiColumn",
                Width = 150
            };

            loaiDataGridView.Columns.AddRange(maLoaiColumn, tenLoaiColumn, moTaColumn, trangThaiColumn);
            loaiDataGridView.DataSource = _loaiBindingSource;
        }

        private void ApplyLoaiFilters()
        {
            string keyword = searchTextBox.Text?.Trim() ?? string.Empty;
            string? statusFilter = GetSelectedStatus(loaiStatusFilterComboBox);

            IList<LoaiDTO> loaiList = string.IsNullOrEmpty(keyword)
                ? _loaiBus.GetLoaiList(statusFilter)
                : _loaiBus.SearchLoai(keyword, statusFilter);

            _loaiBindingSource.DataSource = loaiList;
            ResetLoaiSelection();
            UpdateLoaiButtonsState();
        }

        private void UpdateLoaiButtonsState()
        {
            bool hasSelection = loaiDataGridView.SelectedRows.Count > 0;
            bool canEdit = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
            bool canDelete = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa);

            editLoaiButton.Enabled = hasSelection && canEdit;
            deleteLoaiButton.Enabled = hasSelection && canDelete;
        }

        private static string? GetSelectedStatus(ComboBox comboBox)
        {
            if (comboBox.SelectedItem is not string option)
            {
                return null;
            }

            return string.Equals(option, TrangThaiConstants.ComboBoxOptions[0], StringComparison.CurrentCultureIgnoreCase)
                ? null
                : option;
        }

        private void refreshLoaiButton_Click(object sender, EventArgs e)
        {
            if (loaiStatusFilterComboBox.SelectedIndex != 0)
            {
                loaiStatusFilterComboBox.SelectedIndex = 0;
            }

            if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = string.Empty;
            }

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
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.CreatedLoai == null)
            {
                return;
            }

            ApplyLoaiFilters();
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
                MessageBox.Show(this,
                    "Vui lòng chọn loại để sửa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var loaiSnapshot = new LoaiDTO(selected.MaLoai, selected.TenLoai, selected.MoTa, selected.TrangThai);
            using var dialog = new SuaLoaiDialog(loaiSnapshot);
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.UpdatedLoai == null)
            {
                return;
            }

            ApplyLoaiFilters();
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
                MessageBox.Show(this,
                    "Vui lòng chọn loại để cập nhật trạng thái.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (string.Equals(selected.TrangThai, TrangThaiConstants.NgungHoatDong, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show(this,
                    $"Loại \"{selected.TenLoai}\" đã ở trạng thái \"{TrangThaiConstants.NgungHoatDong}\".",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                this,
                $"Bạn có chắc muốn chuyển loại \"{selected.TenLoai}\" (Mã {selected.MaLoai}) sang trạng thái \"{TrangThaiConstants.NgungHoatDong}\"?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _loaiBus.DeleteLoai(selected.MaLoai);
                ApplyLoaiFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"Không thể cập nhật trạng thái loại.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ResetLoaiSelection()
        {
            try
            {
                if (_loaiBindingSource.Position != -1)
                {
                    _loaiBindingSource.Position = -1;
                }
            }
            catch
            {
            }

            loaiDataGridView.ClearSelection();

            if (loaiDataGridView.CurrentCell != null)
            {
                try
                {
                    loaiDataGridView.CurrentCell = null;
                }
                catch
                {
                }
            }

            UpdateLoaiButtonsState();
        }

        private void loaiDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateLoaiButtonsState();
        }
    }
}
