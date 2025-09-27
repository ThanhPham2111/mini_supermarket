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
        private const string StatusAll = "Tất cả";

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
            sanPhamDataGridView.CellClick += sanPhamDataGridView_CellClick;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(xemChiTietButton, "Xem chi tiết sản phẩm đã chọn");
            toolTip.SetToolTip(themButton, "Thêm sản phẩm mới");
            toolTip.SetToolTip(suaButton, "Sửa thông tin sản phẩm đã chọn");
            toolTip.SetToolTip(xoaButton, "Khóa sản phẩm đã chọn");
            toolTip.SetToolTip(lamMoiButton, "Làm mới danh sách");
            toolTip.SetToolTip(searchButton, "Tìm kiếm sản phẩm");

            searchButton.Click += (_, _) => ApplyFilters();
            searchTextBox.TextChanged += (_, _) => ApplyFilters();
            lamMoiButton.Click += lamMoiButton_Click;

            InitializeStatusFilter();
            statusFilterComboBox.SelectedIndexChanged += statusFilterComboBox_SelectedIndexChanged;

            themButton.Enabled = true;
            lamMoiButton.Enabled = true;
            suaButton.Enabled = false;
            xoaButton.Enabled = false;
            xemChiTietButton.Enabled = false;

            LoadSanPhamData();
        }

        private void sanPhamDataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure it's a data row, not a header
            {
                sanPhamDataGridView.ClearSelection();
                sanPhamDataGridView.Rows[e.RowIndex].Selected = true;
                // Enable buttons based on selection
                suaButton.Enabled = true;
                xoaButton.Enabled = true;
                xemChiTietButton.Enabled = true;
            }
            else
            {
                // Clicking header or outside data rows, clear selection
                sanPhamDataGridView.ClearSelection();
                suaButton.Enabled = false;
                xoaButton.Enabled = false;
                xemChiTietButton.Enabled = false;
            }
        }

        private void InitializeStatusFilter()
        {
            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add(StatusAll);
            statusFilterComboBox.Items.Add("Còn hàng");
            statusFilterComboBox.Items.Add("Hết hàng");
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
                MessageBox.Show(this, $"Không thể tải danh sách sản phẩm.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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