using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;
using mini_supermarket.GUI.Form_LoaiSanPham;

namespace mini_supermarket.GUI.Form_LoaiSanPham
{
    public partial class Form_LoaiSanPham : Form
    {
        private readonly LoaiSanPham_BUS _loaiBus = new();
        private readonly BindingSource _loaiBindingSource = new();

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

        private Form? _activeEmbeddedForm;

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
            // 0: Loại (giữ nguyên layout hiện tại)
            if (mainTabControl.SelectedTab == tabLoai)
            {
                // khôi phục layout loại
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
                return;
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

            loaiDataGridView.Columns.AddRange(new DataGridViewColumn[] { maLoaiColumn, tenLoaiColumn, moTaColumn });
            loaiDataGridView.DataSource = _loaiBindingSource;
        }

        private void LoadLoaiData()
        {
            IList<LoaiDTO> loaiList = _loaiBus.GetLoaiList();
            _loaiBindingSource.DataSource = loaiList;
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
        }

        private void addLoaiButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thêm loại - sẽ được triển khai.", "Thông báo");
        }

        private void editLoaiButton_Click(object sender, EventArgs e)
        {
            if (loaiDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một loại để sửa.");
                return;
            }
            MessageBox.Show("Sửa loại - sẽ được triển khai.", "Thông báo");
        }

        private void deleteLoaiButton_Click(object sender, EventArgs e)
        {
            if (loaiDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một loại để xóa.");
                return;
            }
            MessageBox.Show("Xóa loại - sẽ được triển khai.", "Thông báo");
        }
    }
}


