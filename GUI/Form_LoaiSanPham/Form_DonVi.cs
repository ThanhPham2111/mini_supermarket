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
        private readonly DonVi_BUS _bus = new();
        private readonly BindingSource _binding = new();

        public Form_DonVi()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeStatusFilter();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyFilters();
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

            var maColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(DonViDTO.MaDonVi),
                HeaderText = "Mã đơn vị",
                Name = "MaDonViColumn",
                Width = 140
            };

            var tenColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(DonViDTO.TenDonVi),
                HeaderText = "Tên đơn vị",
                Name = "TenDonViColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var moTaColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(DonViDTO.MoTa),
                HeaderText = "Mô tả",
                Name = "MoTaColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var trangThaiColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(DonViDTO.TrangThai),
                HeaderText = "Trạng thái",
                Name = "TrangThaiColumn",
                Width = 150
            };

            donViDataGridView.Columns.AddRange(maColumn, tenColumn, moTaColumn, trangThaiColumn);
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
            ResetSelection();
        }

        private string? GetSelectedStatus()
        {
            if (statusFilterComboBox.SelectedItem is not string option)
            {
                return null;
            }

            return string.Equals(option, TrangThaiConstants.ComboBoxOptions[0], StringComparison.CurrentCultureIgnoreCase)
                ? null
                : option;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (statusFilterComboBox.SelectedIndex != 0)
            {
                statusFilterComboBox.SelectedIndex = 0;
            }

            if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = string.Empty;
            }

            ApplyFilters();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using var dialog = new ThemDonViDialog();
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.CreatedDonVi == null)
            {
                return;
            }

            ApplyFilters();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (donViDataGridView.CurrentRow?.DataBoundItem is not DonViDTO selected)
            {
                MessageBox.Show(this,
                    "Vui lòng chọn đơn vị để sửa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var snapshot = new DonViDTO(selected.MaDonVi, selected.TenDonVi, selected.MoTa, selected.TrangThai);
            using var dialog = new SuaDonViDialog(snapshot);
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.UpdatedDonVi == null)
            {
                return;
            }

            ApplyFilters();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (donViDataGridView.CurrentRow?.DataBoundItem is not DonViDTO selected)
            {
                MessageBox.Show(this,
                    "Vui lòng chọn đơn vị để cập nhật trạng thái.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (string.Equals(selected.TrangThai, TrangThaiConstants.NgungHoatDong, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show(this,
                    $"Đơn vị \"{selected.TenDonVi}\" đã ở trạng thái \"{TrangThaiConstants.NgungHoatDong}\".",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                this,
                $"Bạn có chắc muốn chuyển đơn vị \"{selected.TenDonVi}\" (Mã {selected.MaDonVi}) sang trạng thái \"{TrangThaiConstants.NgungHoatDong}\"?",
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
                _bus.DeleteDonVi(selected.MaDonVi);
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Không thể cập nhật trạng thái đơn vị.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ResetSelection()
        {
            try
            {
                if (_binding.Position != -1)
                {
                    _binding.Position = -1;
                }
            }
            catch
            {
            }

            donViDataGridView.ClearSelection();

            if (donViDataGridView.CurrentCell != null)
            {
                try
                {
                    donViDataGridView.CurrentCell = null;
                }
                catch
                {
                }
            }
        }
    }
}
