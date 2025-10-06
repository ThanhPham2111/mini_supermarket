using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.NhanVien
{
    public partial class Form_NhanVien : Form
    {
        private const string StatusAll = "Tất cả";

        private readonly NhanVien_BUS _nhanVienBus = new();
        private readonly BindingSource _bindingSource = new();
        private readonly List<string> _roles;
        private readonly List<string> _statuses;
        private IList<NhanVienDTO> _currentNhanVien = Array.Empty<NhanVienDTO>();

        public Form_NhanVien()
        {
            InitializeComponent();
            Load += Form_NhanVien_Load;

            _roles = _nhanVienBus.GetDefaultRoles().ToList();
            _statuses = _nhanVienBus.GetDefaultStatuses().ToList();
        }

        private void Form_NhanVien_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add(StatusAll);
            foreach (var status in _statuses)
            {
                statusFilterComboBox.Items.Add(status);
            }
            statusFilterComboBox.SelectedIndex = 0;
            statusFilterComboBox.SelectedIndexChanged += statusFilterComboBox_SelectedIndexChanged;

            chucVuComboBox.Items.Clear();
            foreach (var role in _roles)
            {
                chucVuComboBox.Items.Add(role);
            }
            chucVuComboBox.SelectedIndex = -1;

            nhanVienDataGridView.AutoGenerateColumns = false;
            nhanVienDataGridView.DataSource = _bindingSource;
            nhanVienDataGridView.SelectionChanged += nhanVienDataGridView_SelectionChanged;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(themButton, "Thêm nhân viên mới");
            toolTip.SetToolTip(suaButton, "Sửa thông tin nhân viên đã chọn");
            toolTip.SetToolTip(xoaButton, "Khóa nhân viên đã chọn"); // Updated: "Khóa" instead of "Xóa"
            toolTip.SetToolTip(lamMoiButton, "Làm mới danh sách");
            toolTip.SetToolTip(searchButton, "Tìm kiếm nhân viên");

            themButton.Click += themButton_Click;
            suaButton.Click += suaButton_Click;
            xoaButton.Click += xoaButton_Click;
            lamMoiButton.Click += lamMoiButton_Click;
            searchButton.Click += (_, _) => ApplySearchFilter();

            searchTextBox.TextChanged += searchTextBox_TextChanged;

            themButton.Enabled = true;
            lamMoiButton.Enabled = true;
            suaButton.Enabled = false;
            xoaButton.Enabled = false;

            SetInputFieldsEnabled(false);

            LoadNhanVienData();
        }

        private void nhanVienDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            if (nhanVienDataGridView.SelectedRows.Count > 0)
            {
                var selectedNhanVien = (NhanVienDTO)nhanVienDataGridView.SelectedRows[0].DataBoundItem;
                maNhanVienTextBox.Text = selectedNhanVien.MaNhanVien.ToString();
                hoTenTextBox.Text = selectedNhanVien.TenNhanVien ?? string.Empty;
                ngaySinhDateTimePicker.Value = selectedNhanVien.NgaySinh ?? DateTime.Today;
                gioiTinhNamRadioButton.Checked = selectedNhanVien.GioiTinh == "Nam";
                gioiTinhNuRadioButton.Checked = selectedNhanVien.GioiTinh == "Nữ";
                if (!string.IsNullOrEmpty(selectedNhanVien.VaiTro) && chucVuComboBox.Items.Contains(selectedNhanVien.VaiTro))
                {
                    chucVuComboBox.SelectedItem = selectedNhanVien.VaiTro;
                }
                else
                {
                    chucVuComboBox.SelectedIndex = -1;
                }
                soDienThoaiTextBox.Text = selectedNhanVien.SoDienThoai ?? string.Empty;

                suaButton.Enabled = true;
                xoaButton.Enabled = true;

                SetInputFieldsEnabled(false);
            }
            else
            {
                maNhanVienTextBox.Text = string.Empty;
                hoTenTextBox.Text = string.Empty;
                ngaySinhDateTimePicker.Value = DateTime.Today;
                gioiTinhNamRadioButton.Checked = false;
                gioiTinhNuRadioButton.Checked = false;
                chucVuComboBox.SelectedIndex = -1;
                soDienThoaiTextBox.Text = string.Empty;

                suaButton.Enabled = false;
                xoaButton.Enabled = false;

                SetInputFieldsEnabled(false);
            }
        }

        private void themButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new Form_NhanVienDialog(_roles, _statuses);
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                var createdNhanVien = _nhanVienBus.AddNhanVien(dialog.NhanVien);
                LoadNhanVienData();
                SelectNhanVienRow(createdNhanVien.MaNhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể thêm nhân viên.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void suaButton_Click(object? sender, EventArgs e)
        {
            var selectedNhanVien = GetSelectedNhanVien();
            if (selectedNhanVien == null)
            {
                return;
            }

            using var dialog = new Form_NhanVienDialog(_roles, _statuses, selectedNhanVien);
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                _nhanVienBus.UpdateNhanVien(dialog.NhanVien);
                LoadNhanVienData();
                SelectNhanVienRow(dialog.NhanVien.MaNhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể cập nhật nhân viên.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xoaButton_Click(object? sender, EventArgs e)
        {
            var selectedNhanVien = GetSelectedNhanVien();
            if (selectedNhanVien == null)
            {
                return;
            }

            DialogResult confirm = MessageBox.Show(this,
                $"Bạn có chắc muốn khóa nhân viên '{selectedNhanVien.TenNhanVien}'? Trạng thái sẽ được chuyển thành 'Đã nghỉ'.",
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
                selectedNhanVien.TrangThai = NhanVien_BUS.StatusInactive; // Set TrangThai to "Đã nghỉ"
                _nhanVienBus.UpdateNhanVien(selectedNhanVien);
                LoadNhanVienData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể khoá nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            LoadNhanVienData();
        }

        private NhanVienDTO? GetSelectedNhanVien()
        {
            if (nhanVienDataGridView.SelectedRows.Count == 0)
            {
                return null;
            }

            return nhanVienDataGridView.SelectedRows[0].DataBoundItem as NhanVienDTO;
        }

        private void SelectNhanVienRow(int maNhanVien)
        {
            if (maNhanVien <= 0 || nhanVienDataGridView.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in nhanVienDataGridView.Rows)
            {
                if (row.DataBoundItem is NhanVienDTO nhanVien && nhanVien.MaNhanVien == maNhanVien)
                {
                    row.Selected = true;
                    try
                    {
                        nhanVienDataGridView.FirstDisplayedScrollingRowIndex = row.Index;
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
            maNhanVienTextBox.Enabled = enabled;
            hoTenTextBox.Enabled = enabled;
            ngaySinhDateTimePicker.Enabled = enabled;
            gioiTinhNamRadioButton.Enabled = enabled;
            gioiTinhNuRadioButton.Enabled = enabled;
            chucVuComboBox.Enabled = enabled;
            soDienThoaiTextBox.Enabled = enabled;
        }

        private void statusFilterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyStatusFilter();
        }

        private void searchTextBox_TextChanged(object? sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void LoadNhanVienData()
        {
            try
            {
                _currentNhanVien = _nhanVienBus.GetNhanVien();
                ApplyStatusFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể tải danh sách nhân viên.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStatusFilter()
        {
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(selectedStatus) || string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase))
            {
                _bindingSource.DataSource = _currentNhanVien;
            }
            else
            {
                var filtered = new List<NhanVienDTO>();
                foreach (var nhanVien in _currentNhanVien)
                {
                    if (string.Equals(nhanVien.TrangThai, selectedStatus, StringComparison.OrdinalIgnoreCase))
                    {
                        filtered.Add(nhanVien);
                    }
                }
                _bindingSource.DataSource = filtered;
            }

            if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
            {
                ApplySearchFilter();
            }
            else
            {
                nhanVienDataGridView.ClearSelection();
            }
        }

        private void ApplySearchFilter()
        {
            string searchText = searchTextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            var filtered = new List<NhanVienDTO>();

            foreach (var nhanVien in _currentNhanVien)
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                    nhanVien.MaNhanVien.ToString().ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ||
                    (nhanVien.TenNhanVien?.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ?? false) ||
                    (nhanVien.SoDienThoai?.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ?? false);

                bool matchesStatus = string.IsNullOrWhiteSpace(selectedStatus) ||
                    string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(nhanVien.TrangThai, selectedStatus, StringComparison.OrdinalIgnoreCase);

                if (matchesSearch && matchesStatus)
                {
                    filtered.Add(nhanVien);
                }
            }

            _bindingSource.DataSource = filtered;
            nhanVienDataGridView.ClearSelection();
        }
    }
}