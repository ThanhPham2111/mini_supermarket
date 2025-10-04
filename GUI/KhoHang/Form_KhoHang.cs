using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.FormKhoHang
{
    public partial class Form_KhoHang : Form
    {
        private readonly KhoHang_BUS _khoHangBus = new();
        private IList<SanPhamDTO> _allProducts = new List<SanPhamDTO>();

        public Form_KhoHang()
        {
            InitializeComponent();
            InitializeEvents();
            LoadAllProducts();
            LoadTrangThaiComboBoxes();
            LoadKhoHangData();
        }

        private void InitializeEvents()
        {
            themButton.Click += ThemButton_Click;
            suaButton.Click += SuaButton_Click;
            xoaButton.Click += XoaButton_Click;
            lamMoiButton.Click += LamMoiButton_Click;
            statusFilterComboBox.SelectedIndexChanged += StatusFilterComboBox_SelectedIndexChanged;
            khoHangDataGridView.SelectionChanged += KhoHangDataGridView_SelectionChanged;
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            sanPhamComboBox.SelectedIndexChanged += SanPhamComboBox_SelectedIndexChanged;
        }

        private void LoadAllProducts()
        {
            _allProducts = _khoHangBus.GetAllProducts();
            sanPhamComboBox.Items.Clear();
            foreach (var sp in _allProducts)
                sanPhamComboBox.Items.Add($"{sp.MaSanPham} - {sp.TenSanPham}");
        }

        private void LoadTrangThaiComboBoxes()
        {
            var statuses = _khoHangBus.GetAvailableStatuses();
            trangThaiComboBox.Items.Clear();
            trangThaiComboBox.Items.AddRange(statuses.ToArray());
            if (trangThaiComboBox.Items.Count > 0)
                trangThaiComboBox.SelectedIndex = 0;

            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add("Tất cả");
            statusFilterComboBox.Items.AddRange(statuses.ToArray());
            statusFilterComboBox.SelectedIndex = 0;
        }

        private void LoadKhoHangData(string? filter = null)
        {
            if (filter == "Tất cả") filter = null;
            var data = _khoHangBus.GetKhoHang(filter);

            if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                string keyword = searchTextBox.Text.Trim().ToLower();
                data = data.Where(k => k.MaSanPham.ToString().Contains(keyword) ||
                                       (_allProducts.FirstOrDefault(p => p.MaSanPham == k.MaSanPham)?.TenSanPham.ToLower().Contains(keyword) ?? false))
                           .ToList();
            }

            var displayData = data.Select(k =>
            {
                var sp = _allProducts.FirstOrDefault(p => p.MaSanPham == k.MaSanPham);
                return new
                {
                    k.MaSanPham,
                    TenSanPham = sp?.TenSanPham ?? "",
                    k.SoLuong,
                    k.TrangThai
                };
            }).ToList();

            khoHangDataGridView.DataSource = new BindingSource { DataSource = displayData };
        }

        private void SanPhamComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sanPhamComboBox.SelectedIndex >= 0)
            {
                var sp = _allProducts[sanPhamComboBox.SelectedIndex];
                maSanPhamTextBox.Text = sp.MaSanPham.ToString();
            }
        }

        private void KhoHangDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            if (khoHangDataGridView.CurrentRow == null) return;
            var row = khoHangDataGridView.CurrentRow;
            if (row.DataBoundItem != null)
            {
                int maSP = (int)row.Cells["MaSanPhamColumn"].Value;
                var sp = _allProducts.FirstOrDefault(p => p.MaSanPham == maSP);
                if (sp != null)
                {
                    maSanPhamTextBox.Text = sp.MaSanPham.ToString();
                    sanPhamComboBox.SelectedIndex = _allProducts.IndexOf(sp);
                    soLuongNumericUpDown.Value = Convert.ToDecimal(row.Cells["SoLuongColumn"].Value);
                    trangThaiComboBox.SelectedItem = row.Cells["TrangThaiColumn"].Value?.ToString();
                }
            }
        }

        private void ThemButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (sanPhamComboBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Chọn sản phẩm để thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maSP = _allProducts[sanPhamComboBox.SelectedIndex].MaSanPham;
                int soLuong = (int)soLuongNumericUpDown.Value;

                var kho = new KhoHangDTO
                {
                    MaSanPham = maSP,
                    SoLuong = soLuong,
                    TrangThai = _khoHangBus.GetStatusByQuantity(soLuong)
                };

                if (MessageBox.Show($"Bạn có chắc muốn thêm sản phẩm {sanPhamComboBox.Text} vào kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _khoHangBus.AddKhoHang(kho);
                    LoadKhoHangData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuaButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (sanPhamComboBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Chọn sản phẩm để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maSP = _allProducts[sanPhamComboBox.SelectedIndex].MaSanPham;
                int soLuong = (int)soLuongNumericUpDown.Value;

                var kho = new KhoHangDTO
                {
                    MaSanPham = maSP,
                    SoLuong = soLuong,
                    TrangThai = _khoHangBus.GetStatusByQuantity(soLuong)
                };

                if (MessageBox.Show($"Bạn có chắc muốn cập nhật sản phẩm {sanPhamComboBox.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _khoHangBus.UpdateKhoHang(kho);
                    LoadKhoHangData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (sanPhamComboBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Chọn sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maSP = _allProducts[sanPhamComboBox.SelectedIndex].MaSanPham;

                if (MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm {sanPhamComboBox.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _khoHangBus.DeleteKhoHang(maSP);
                    LoadKhoHangData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LamMoiButton_Click(object? sender, EventArgs e)
        {
            sanPhamComboBox.SelectedIndex = -1;
            maSanPhamTextBox.Clear();
            soLuongNumericUpDown.Value = 0;
            if (trangThaiComboBox.Items.Count > 0)
                trangThaiComboBox.SelectedIndex = 0;
            searchTextBox.Clear();
        }

        private SanPhamDTO? FindProductFromInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            input = input.Trim();

            // Nếu nhập toàn số → tìm theo MaSanPham
            if (int.TryParse(input, out int maSanPham))
            {
                return _allProducts.FirstOrDefault(p => p.MaSanPham == maSanPham);
            }

            // Nếu nhập chữ → tìm theo TenSanPham
            return _allProducts.FirstOrDefault(p =>
                p.TenSanPham.Equals(input, StringComparison.OrdinalIgnoreCase));
        }


        private void sanPhamComboBox_Leave(object sender, EventArgs e)
        {
            var input = sanPhamComboBox.Text;
            var product = FindProductFromInput(input);

            if (product != null)
            {
                // Hiển thị lại theo chuẩn "Mã - Tên"
                sanPhamComboBox.Text = $"{product.MaSanPham} - {product.TenSanPham}";
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void StatusFilterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadKhoHangData(statusFilterComboBox.SelectedItem?.ToString());
        }

        private void SearchTextBox_TextChanged(object? sender, EventArgs e)
        {
            LoadKhoHangData(statusFilterComboBox.SelectedItem?.ToString());
        }

        private void maSanPhamLabel_Click(object sender, EventArgs e) { }
        private void trangThaiLabel_Click(object sender, EventArgs e) { }

        private void maSanPhamTextBox_TextChanged(object sender, EventArgs e)
        {

        }

    }
}