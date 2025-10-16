using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mini_supermarket.BUS;
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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadLoaiData();
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
                HeaderText = "Ma loai",
                Name = "MaLoaiColumn",
                Width = 120
            };

            var tenLoaiColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(LoaiDTO.TenLoai),
                HeaderText = "Ten loai",
                Name = "TenLoaiColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var moTaColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(LoaiDTO.MoTa),
                HeaderText = "Mo ta",
                Name = "MoTaColumn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            loaiDataGridView.Columns.AddRange(new DataGridViewColumn[] { maLoaiColumn, tenLoaiColumn, moTaColumn });
            loaiDataGridView.DataSource = _loaiBindingSource;
        }

        private void LoadLoaiData()
        {
            IList<LoaiDTO> loaiList = _loaiBus.GetLoaiList();
            _loaiBindingSource.DataSource = loaiList;
            ResetLoaiSelection();
        }

        private void refreshLoaiButton_Click(object sender, EventArgs e)
        {
            LoadLoaiData();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            var keyword = searchTextBox.Text?.Trim() ?? string.Empty;
            IList<LoaiDTO> filtered = _loaiBus.SearchLoai(keyword);
            _loaiBindingSource.DataSource = filtered;
            ResetLoaiSelection();
        }

        private void addLoaiButton_Click(object sender, EventArgs e)
        {
            using var dialog = new ThemLoaiDialog();
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.CreatedLoai == null)
            {
                return;
            }

            LoadLoaiData();
        }

        private void editLoaiButton_Click(object sender, EventArgs e)
        {
            if (loaiDataGridView.CurrentRow?.DataBoundItem is not LoaiDTO selected)
            {
                MessageBox.Show("Vui long chon loai de sua.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var loaiSnapshot = new LoaiDTO(selected.MaLoai, selected.TenLoai, selected.MoTa);
            using var dialog = new SuaLoaiDialog(loaiSnapshot);
            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.UpdatedLoai == null)
            {
                return;
            }

            LoadLoaiData();
        }

        private void deleteLoaiButton_Click(object sender, EventArgs e)
        {
            if (loaiDataGridView.CurrentRow?.DataBoundItem is not LoaiDTO selected)
            {
                MessageBox.Show("Vui long chon loai de xoa.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Ban co chac muon xoa loai \"{selected.TenLoai}\" (Ma {selected.MaLoai})?",
                "Xac nhan xoa",
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
                LoadLoaiData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Khong the xoa loai.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Loi",
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
