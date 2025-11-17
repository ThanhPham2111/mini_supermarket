using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_BanHang
{
    public partial class Dialog_ChonKhachHang : Form
    {
        private readonly KhachHang_BUS _khachHangBus = new();
        private IList<KhachHangDTO>? _allKhachHang;
        private KhachHangDTO? _selectedKhachHang;

        public KhachHangDTO? SelectedKhachHang => _selectedKhachHang;

        public Dialog_ChonKhachHang()
        {
            InitializeComponent();
            LoadKhachHang();
        }

        private void LoadKhachHang()
        {
            try
            {
                _allKhachHang = _khachHangBus.GetKhachHang(KhachHang_BUS.StatusActive);
                FilterAndDisplayKhachHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khách hàng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterAndDisplayKhachHang()
        {
            try
            {
                if (_allKhachHang == null || dgvKhachHang == null)
                    return;

                string searchText = txtSearchKH?.Text?.Trim() ?? "";
                IList<KhachHangDTO> filteredList;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    filteredList = _allKhachHang;
                }
                else
                {
                    string searchLower = searchText.ToLower();
                    filteredList = _allKhachHang.Where(kh =>
                        (kh.TenKhachHang ?? "").ToLower().Contains(searchLower) ||
                        (kh.SoDienThoai ?? "").Contains(searchText) ||
                        (kh.MaKhachHang.ToString()).Contains(searchText) ||
                        (kh.DiaChi ?? "").ToLower().Contains(searchLower)
                    ).ToList();
                }

                dgvKhachHang.DataSource = filteredList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc danh sách khách hàng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSearchKH_TextChanged(object? sender, EventArgs e)
        {
            FilterAndDisplayKhachHang();
        }

        private void DgvKhachHang_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectCurrentRow();
            }
        }

        private void BtnChon_Click(object? sender, EventArgs e)
        {
            SelectCurrentRow();
        }

        private void SelectCurrentRow()
        {
            if (dgvKhachHang?.SelectedRows.Count > 0)
            {
                var selectedRow = dgvKhachHang.SelectedRows[0];
                if (selectedRow.DataBoundItem is KhachHangDTO kh)
                {
                    _selectedKhachHang = kh;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnHuy_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
