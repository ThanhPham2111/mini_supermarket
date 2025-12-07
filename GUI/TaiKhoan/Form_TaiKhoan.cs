using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.TaiKhoan
{
    public partial class Form_TaiKhoan : Form
    {
        private const string StatusAll = "Tất cả";

        private readonly TaiKhoan_BUS _taiKhoanBus = new();
        private readonly NhanVien_BUS _nhanVienBus = new();
        private readonly BindingSource _bindingSource = new();
        private readonly List<string> _statuses;
        private IList<TaiKhoanDTO> _currentTaiKhoan = Array.Empty<TaiKhoanDTO>();
        private Dictionary<int, string> _nhanVienMap = new();
        private Dictionary<int, string> _quyenMap = new();

        public Form_TaiKhoan()
        {
            InitializeComponent();
            Load += Form_TaiKhoan_Load;

            _statuses = _taiKhoanBus.GetDefaultStatuses().ToList();
        }

        private void Form_TaiKhoan_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            LoadNhanVienMap();
            LoadQuyenMap();

            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add(StatusAll);
            foreach (var status in _statuses)
            {
                statusFilterComboBox.Items.Add(status);
            }
            statusFilterComboBox.SelectedIndex = 0;
            statusFilterComboBox.SelectedIndexChanged += statusFilterComboBox_SelectedIndexChanged;

            taiKhoanDataGridView.AutoGenerateColumns = false;
            taiKhoanDataGridView.DataSource = _bindingSource;
            taiKhoanDataGridView.SelectionChanged += taiKhoanDataGridView_SelectionChanged;
            taiKhoanDataGridView.DataBindingComplete += taiKhoanDataGridView_DataBindingComplete;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(themButton, "Thêm tài khoản mới");
            toolTip.SetToolTip(suaButton, "Sửa thông tin tài khoản đã chọn");
            toolTip.SetToolTip(xoaButton, "Khóa tài khoản đã chọn");
            toolTip.SetToolTip(lamMoiButton, "Làm mới danh sách");
            toolTip.SetToolTip(searchButton, "Tìm kiếm tài khoản");
            toolTip.SetToolTip(searchTextBox, "Tìm kiếm theo mã, tên đăng nhập hoặc tên nhân viên");

            themButton.Click += themButton_Click;
            suaButton.Click += suaButton_Click;
            xoaButton.Click += xoaButton_Click;
            lamMoiButton.Click += lamMoiButton_Click;
            searchButton.Click += (_, _) => ApplySearchFilter();

            searchTextBox.TextChanged += searchTextBox_TextChanged;

            // Xử lý con trỏ chuột cho tất cả các field ReadOnly để đồng nhất
            SetReadOnlyFieldCursor(maTaiKhoanTextBox);
            SetReadOnlyFieldCursor(nhanVienTextBox);
            SetReadOnlyFieldCursor(quyenTextBox);
            SetReadOnlyFieldCursor(trangThaiTextBox);

            themButton.Enabled = true;
            lamMoiButton.Enabled = true;
            suaButton.Enabled = false;
            xoaButton.Enabled = false;

            SetInputFieldsEnabled(false);

            LoadTaiKhoanData();
        }

        private void LoadNhanVienMap()
        {
            var nhanVienList = _nhanVienBus.GetAll();
            _nhanVienMap = nhanVienList.ToDictionary(nv => nv.MaNhanVien, nv => nv.TenNhanVien ?? $"NV{nv.MaNhanVien}");
        }

        private void LoadQuyenMap()
        {
            var quyenList = _taiKhoanBus.GetAllPhanQuyen();
            _quyenMap = quyenList.ToDictionary(q => q.MaQuyen, q => q.TenQuyen);
        }

        private void taiKhoanDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            if (taiKhoanDataGridView.SelectedRows.Count > 0)
            {
                var selectedTaiKhoan = (TaiKhoanDTO)taiKhoanDataGridView.SelectedRows[0].DataBoundItem;
                maTaiKhoanTextBox.Text = selectedTaiKhoan.MaTaiKhoan.ToString();
                tenDangNhapTextBox.Text = selectedTaiKhoan.TenDangNhap ?? string.Empty;
                matKhauTextBox.Text = selectedTaiKhoan.MatKhau ?? string.Empty;
                trangThaiTextBox.Text = selectedTaiKhoan.TrangThai ?? string.Empty;
                
                if (_nhanVienMap.TryGetValue(selectedTaiKhoan.MaNhanVien, out var tenNhanVien))
                {
                    nhanVienTextBox.Text = tenNhanVien;
                }
                else
                {
                    nhanVienTextBox.Text = string.Empty;
                }

                if (_quyenMap.TryGetValue(selectedTaiKhoan.MaQuyen, out var tenQuyen))
                {
                    quyenTextBox.Text = tenQuyen;
                }
                else
                {
                    quyenTextBox.Text = string.Empty;
                }

                suaButton.Enabled = true;
                xoaButton.Enabled = true;

                SetInputFieldsEnabled(false);
            }
            else
            {
                maTaiKhoanTextBox.Text = string.Empty;
                tenDangNhapTextBox.Text = string.Empty;
                matKhauTextBox.Text = string.Empty;
                trangThaiTextBox.Text = string.Empty;
                nhanVienTextBox.Text = string.Empty;
                quyenTextBox.Text = string.Empty;

                suaButton.Enabled = false;
                xoaButton.Enabled = false;

                SetInputFieldsEnabled(false);
            }
        }

        private void themButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new Form_TaiKhoanDialog(_taiKhoanBus, _nhanVienBus);
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                var createdTaiKhoan = _taiKhoanBus.AddTaiKhoan(dialog.TaiKhoan);
                LoadTaiKhoanData();
                SelectTaiKhoanRow(createdTaiKhoan.MaTaiKhoan);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể thêm tài khoản.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void suaButton_Click(object? sender, EventArgs e)
        {
            var selectedTaiKhoan = GetSelectedTaiKhoan();
            if (selectedTaiKhoan == null)
            {
                return;
            }

            using var dialog = new Form_TaiKhoanDialog(_taiKhoanBus, _nhanVienBus, selectedTaiKhoan);
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                _taiKhoanBus.UpdateTaiKhoan(dialog.TaiKhoan);
                LoadTaiKhoanData();
                SelectTaiKhoanRow(dialog.TaiKhoan.MaTaiKhoan);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể cập nhật tài khoản.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xoaButton_Click(object? sender, EventArgs e)
        {
            var selectedTaiKhoan = GetSelectedTaiKhoan();
            if (selectedTaiKhoan == null)
            {
                return;
            }

            if (string.Equals(selectedTaiKhoan.TrangThai, TaiKhoan_BUS.StatusInactive, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(this,
                    $"Tài khoản '{selectedTaiKhoan.TenDangNhap}' đã ở trạng thái '{TaiKhoan_BUS.StatusInactive}'.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(this,
                $"Bạn có chắc muốn khóa tài khoản '{selectedTaiKhoan.TenDangNhap}'? Trạng thái sẽ được chuyển thành '{TaiKhoan_BUS.StatusInactive}'.",
                "Xác nhận khóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                selectedTaiKhoan.TrangThai = TaiKhoan_BUS.StatusInactive;
                _taiKhoanBus.UpdateTaiKhoan(selectedTaiKhoan);
                LoadTaiKhoanData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể khóa tài khoản.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lamMoiButton_Click(object? sender, EventArgs e)
        {
            searchTextBox.Text = string.Empty;

            if (statusFilterComboBox.SelectedIndex != 0)
            {
                statusFilterComboBox.SelectedIndex = 0;
            }
            else
            {
                ApplyStatusFilter();
            }

            LoadNhanVienMap();
            LoadQuyenMap();
            LoadTaiKhoanData();
        }

        private TaiKhoanDTO? GetSelectedTaiKhoan()
        {
            if (taiKhoanDataGridView.SelectedRows.Count == 0)
            {
                return null;
            }

            return taiKhoanDataGridView.SelectedRows[0].DataBoundItem as TaiKhoanDTO;
        }

        private void SelectTaiKhoanRow(int maTaiKhoan)
        {
            if (maTaiKhoan <= 0 || taiKhoanDataGridView.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in taiKhoanDataGridView.Rows)
            {
                if (row.DataBoundItem is TaiKhoanDTO taiKhoan && taiKhoan.MaTaiKhoan == maTaiKhoan)
                {
                    row.Selected = true;
                    try
                    {
                        taiKhoanDataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                    catch
                    {
                        // Ignore if cannot set scroll index
                    }

                    return;
                }
            }
        }

        private void SetInputFieldsEnabled(bool enabled)
        {
            // Các field ReadOnly luôn giữ Enabled = false để đồng nhất hành vi
            maTaiKhoanTextBox.Enabled = false;
            nhanVienTextBox.Enabled = false;
            quyenTextBox.Enabled = false;
            trangThaiTextBox.Enabled = false;
            
            // Chỉ các field có thể chỉnh sửa mới thay đổi Enabled
            tenDangNhapTextBox.Enabled = enabled;
            matKhauTextBox.Enabled = enabled;
        }

        private void SetReadOnlyFieldCursor(TextBox textBox)
        {
            textBox.MouseEnter += (s, e) => { Cursor = Cursors.Default; };
            textBox.MouseMove += (s, e) => { Cursor = Cursors.Default; };
            textBox.GotFocus += (s, e) => { textBox.Parent.Focus(); }; // Chuyển focus ra khỏi field
        }

        private void statusFilterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyStatusFilter();
        }

        private void searchTextBox_TextChanged(object? sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void LoadTaiKhoanData()
        {
            try
            {
                _currentTaiKhoan = _taiKhoanBus.GetTaiKhoan();
                ApplyStatusFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể tải danh sách tài khoản.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStatusFilter()
        {
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(selectedStatus) || string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase))
            {
                _bindingSource.DataSource = _currentTaiKhoan;
            }
            else
            {
                var filtered = new List<TaiKhoanDTO>();
                foreach (var taiKhoan in _currentTaiKhoan)
                {
                    if (string.Equals(taiKhoan.TrangThai, selectedStatus, StringComparison.OrdinalIgnoreCase))
                    {
                        filtered.Add(taiKhoan);
                    }
                }
                _bindingSource.DataSource = filtered;
            }

            // Update display values for grid
            foreach (DataGridViewRow row in taiKhoanDataGridView.Rows)
            {
                if (row.DataBoundItem is TaiKhoanDTO tk)
                {
                    row.Cells["tenNhanVienColumn"].Value = _nhanVienMap.TryGetValue(tk.MaNhanVien, out var ten) ? ten : "";
                    row.Cells["tenQuyenColumn"].Value = _quyenMap.TryGetValue(tk.MaQuyen, out var q) ? q : "";
                }
            }

            if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
            {
                ApplySearchFilter();
            }
            else
            {
                taiKhoanDataGridView.ClearSelection();
            }
        }

        private void ApplySearchFilter()
        {
            string searchText = searchTextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            var filtered = new List<TaiKhoanDTO>();

            foreach (var taiKhoan in _currentTaiKhoan)
            {
                string tenNhanVien = _nhanVienMap.TryGetValue(taiKhoan.MaNhanVien, out var ten) ? ten : string.Empty;
                string tenQuyen = _quyenMap.TryGetValue(taiKhoan.MaQuyen, out var q) ? q : string.Empty;

                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                    taiKhoan.MaTaiKhoan.ToString().ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ||
                    (taiKhoan.TenDangNhap?.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ?? false) ||
                    tenNhanVien.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ||
                    tenQuyen.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText);

                bool matchesStatus = string.IsNullOrWhiteSpace(selectedStatus) ||
                    string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(taiKhoan.TrangThai, selectedStatus, StringComparison.OrdinalIgnoreCase);

                if (matchesSearch && matchesStatus)
                {
                    filtered.Add(taiKhoan);
                }
            }

            _bindingSource.DataSource = filtered;
            
            // Update display values for grid
            foreach (DataGridViewRow row in taiKhoanDataGridView.Rows)
            {
                if (row.DataBoundItem is TaiKhoanDTO tk)
                {
                    row.Cells["tenNhanVienColumn"].Value = _nhanVienMap.TryGetValue(tk.MaNhanVien, out var ten) ? ten : "";
                    row.Cells["tenQuyenColumn"].Value = _quyenMap.TryGetValue(tk.MaQuyen, out var q) ? q : "";
                }
            }
            
            taiKhoanDataGridView.ClearSelection();
        }

        private void taiKhoanDataGridView_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in taiKhoanDataGridView.Rows)
            {
                if (row.DataBoundItem is TaiKhoanDTO tk)
                {
                    row.Cells["tenNhanVienColumn"].Value = _nhanVienMap.TryGetValue(tk.MaNhanVien, out var ten) ? ten : "";
                    row.Cells["tenQuyenColumn"].Value = _quyenMap.TryGetValue(tk.MaQuyen, out var q) ? q : "";
                }
            }
        }
    }
}

