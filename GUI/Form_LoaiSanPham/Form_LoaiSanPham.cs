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
        private readonly LoaiSanPham_BUS _loaiBus = new();
        private readonly BindingSource _loaiBindingSource = new();
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
            ApplyLoaiFilters();
        }

        private void InitializeLoaiStatusFilter()
        {
            loaiStatusFilterComboBox.Items.Clear();
            foreach (var option in TrangThaiConstants.ComboBoxOptions)
            {
                loaiStatusFilterComboBox.Items.Add(option);
            }

            loaiStatusFilterComboBox.SelectedIndex = 0;
            loaiStatusFilterComboBox.SelectedIndexChanged += loaiStatusFilterComboBox_SelectedIndexChanged;
        }

        private void loaiStatusFilterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyLoaiFilters();
        }

        private void ShowEmbeddedFormInCurrentTab(Form form)
        {
            if (_activeEmbeddedForm != null)
            {
                _activeEmbeddedForm.Close();
                _activeEmbeddedForm.Dispose();
                _activeEmbeddedForm = null;
            }

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            var selected = mainTabControl.SelectedTab;
            if (selected != null)
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
                CloseEmbedded();
                ApplyLoaiFilters();
                return;
            }

            if (mainTabControl.SelectedTab == tabThuongHieu)
            {
                ShowEmbeddedFormInCurrentTab(new Form_ThuongHieu());
                return;
            }

            if (mainTabControl.SelectedTab == tabDonVi)
            {
                ShowEmbeddedFormInCurrentTab(new Form_DonVi());
            }
        }

        private void CloseEmbedded()
        {
            if (_activeEmbeddedForm == null)
            {
                return;
            }

            _activeEmbeddedForm.Close();
            _activeEmbeddedForm.Dispose();
            _activeEmbeddedForm = null;
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

            loaiDataGridView.Columns.AddRange(new DataGridViewColumn[] { maLoaiColumn, tenLoaiColumn, moTaColumn, trangThaiColumn });
            loaiDataGridView.DataSource = _loaiBindingSource;
        }

        private void LoadLoaiData()
        {
            ApplyLoaiFilters();
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
        }

        private static string? GetSelectedStatus(ComboBox comboBox)
        {
            if (comboBox.SelectedItem is not string option)
            {
                return null;
            }

            if (string.Equals(option, TrangThaiConstants.ComboBoxOptions[0], StringComparison.CurrentCultureIgnoreCase))
            {
                return null;
            }

            return option;
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
            using var dialog = new ThemLoaiDialog();
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.CreatedLoai == null)
            {
                return;
            }

            ApplyLoaiFilters();
        }

        private void editLoaiButton_Click(object sender, EventArgs e)
        {
            if (loaiDataGridView.CurrentRow?.DataBoundItem is not LoaiDTO selected)
            {
                MessageBox.Show("Vui lòng chọn loại để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (loaiDataGridView.CurrentRow?.DataBoundItem is not LoaiDTO selected)
            {
                MessageBox.Show("Vui lòng chọn loại để cập nhật trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
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
                // ignore when binding source rejects position reset
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
                    // ignore if the grid rejects clearing the current cell
                }
            }
        }
    }
}
