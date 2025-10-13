using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
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

            donViDataGridView.Columns.AddRange(new DataGridViewColumn[] { maColumn, tenColumn, moTaColumn });
            donViDataGridView.DataSource = _binding;
        }

        private void LoadData()
        {
            IList<DonViDTO> list = _bus.GetDonViList();
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


