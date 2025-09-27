using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_SanPham
{
    public partial class Form_SanPham : Form
    {
        private const string StatusAll = "Tat ca";

        private readonly SanPham_BUS _sanPhamBus = new();
        private readonly BindingSource _bindingSource = new();
        private IList<SanPhamDTO> _currentSanPham = Array.Empty<SanPhamDTO>();

        public Form_SanPham()
        {
            InitializeComponent();
            Load += Form_SanPham_Load;
        }

        private void Form_SanPham_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            sanPhamDataGridView.AutoGenerateColumns = false;
            sanPhamDataGridView.DataSource = _bindingSource;
            sanPhamDataGridView.SelectionChanged += SanPhamDataGridView_SelectionChanged;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(xemChiTietButton, "Xem chi tiet san pham da chon");
            toolTip.SetToolTip(themButton, "Them san pham moi");
            toolTip.SetToolTip(suaButton, "Sua thong tin san pham da chon");
            toolTip.SetToolTip(xoaButton, "Xoa san pham da chon");
            toolTip.SetToolTip(lamMoiButton, "Lam moi danh sach");
            toolTip.SetToolTip(searchButton, "Tim kiem san pham");


            searchButton.Click += (_, _) => ApplyFilters();
            searchTextBox.TextChanged += (_, _) => ApplyFilters();
            lamMoiButton.Click += lamMoiButton_Click;
            xemChiTietButton.Click += xemChiTietButton_Click;

            InitializeStatusFilter();
            statusFilterComboBox.SelectedIndexChanged += statusFilterComboBox_SelectedIndexChanged;

            themButton.Enabled = true;
            suaButton.Enabled = false;
            xoaButton.Enabled = false;
            xemChiTietButton.Enabled = false;

            LoadSanPhamData();
        }

        private void InitializeStatusFilter()
        {
            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add(StatusAll);

            foreach (var status in _sanPhamBus.GetAvailableStatuses())
            {
                if (!string.IsNullOrWhiteSpace(status))
                {
                    statusFilterComboBox.Items.Add(status);
                }
            }

            statusFilterComboBox.SelectedIndex = 0;
        }

        private void LoadSanPhamData()
        {
            try
            {
                _currentSanPham = _sanPhamBus.GetSanPham();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Khong the tai danh sach san pham.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void statusFilterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void lamMoiButton_Click(object? sender, EventArgs e)
        {
            bool statusChanged = statusFilterComboBox.SelectedIndex != 0;
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                searchTextBox.Clear();
            }

            if (statusChanged)
            {
                statusFilterComboBox.SelectedIndex = 0;
            }

            LoadSanPhamData();
        }

        private void ApplyFilters()
        {
            string searchText = searchTextBox.Text.Trim();
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            IEnumerable<SanPhamDTO> filtered = _currentSanPham;

            if (!string.IsNullOrWhiteSpace(selectedStatus) && !selectedStatus.Equals(StatusAll, StringComparison.CurrentCultureIgnoreCase))
            {
                filtered = filtered.Where(sp => string.Equals(sp.TrangThai?.Trim(), selectedStatus.Trim(), StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(sp => MatchesSearch(sp, searchText));
            }

            _bindingSource.DataSource = filtered.ToList();
            sanPhamDataGridView.ClearSelection();
            UpdateActionButtonsState();
        }

        private void SanPhamDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateActionButtonsState();
        }

        private void UpdateActionButtonsState()
        {
            bool hasSelection = sanPhamDataGridView.SelectedRows.Count > 0;
            xemChiTietButton.Enabled = hasSelection;
            suaButton.Enabled = hasSelection;
            xoaButton.Enabled = hasSelection;
        }

        private void xemChiTietButton_Click(object? sender, EventArgs e)
        {
            var selectedSanPham = GetSelectedSanPham();
            if (selectedSanPham == null)
            {
                return;
            }

            using var dialog = new Form_SanPhamDialog(selectedSanPham);
            dialog.ShowDialog(this);
        }

        private SanPhamDTO? GetSelectedSanPham()
        {
            if (sanPhamDataGridView.SelectedRows.Count == 0)
            {
                return null;
            }

            return sanPhamDataGridView.SelectedRows[0].DataBoundItem as SanPhamDTO;
        }

        private static bool MatchesSearch(SanPhamDTO sanPham, string searchText)
        {
            var comparison = StringComparison.CurrentCultureIgnoreCase;

            if (sanPham.MaSanPham.ToString().Contains(searchText, comparison))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(sanPham.TenSanPham) && sanPham.TenSanPham.Contains(searchText, comparison))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(sanPham.DonVi) && sanPham.DonVi.Contains(searchText, comparison))
            {
                return true;
            }

            if (sanPham.GiaBan.HasValue && sanPham.GiaBan.Value.ToString("N0", CultureInfo.CurrentCulture).Contains(searchText, comparison))
            {
                return true;
            }

            if (sanPham.MaLoai.ToString().Contains(searchText, comparison))
            {
                return true;
            }

            if (sanPham.Hsd.HasValue && sanPham.Hsd.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture).Contains(searchText, comparison))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(sanPham.TrangThai) && sanPham.TrangThai.Contains(searchText, comparison))
            {
                return true;
            }

            return false;
        }
    }
}
