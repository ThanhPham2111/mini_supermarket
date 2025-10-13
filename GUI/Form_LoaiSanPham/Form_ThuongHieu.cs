using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_LoaiSanPham
{
    public partial class Form_ThuongHieu : Form
    {
        private readonly ThuongHieu_BUS _bus = new();
        private readonly BindingSource _binding = new();

        public Form_ThuongHieu()
        {
            InitializeComponent();
            InitializeGrid();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
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

            thuongHieuDataGridView.Columns.AddRange(new DataGridViewColumn[] { maColumn, tenColumn });
            thuongHieuDataGridView.DataSource = _binding;
        }

        private void LoadData()
        {
            IList<ThuongHieuDTO> list = _bus.GetThuongHieuList();
            _binding.DataSource = list;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            var keyword = searchTextBox.Text?.Trim() ?? string.Empty;
            _binding.DataSource = _bus.Search(keyword);
        }
    }
}


