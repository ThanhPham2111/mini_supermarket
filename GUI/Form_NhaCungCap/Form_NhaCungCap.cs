using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.NhaCungCap

{
    public partial class Form_NhaCungCap : Form
    {
        private const string StatusAll = "Tất cả";

        private readonly NhaCungCap_BUS _nhaCungCapBus = new();
        private readonly BindingSource _bindingSource = new();
        // private readonly List<string> _roles;
        private readonly List<string> _statuses;
        private IList<NhaCungCapDTO> _currentNhaCungCap = Array.Empty<NhaCungCapDTO>();

        public Form_NhaCungCap()
        {
            InitializeComponent();
            Load += Form_NhaCungCap_Load;

            // _roles = _nhaCungCapBus.GetDefaultRoles().ToList();
            _statuses = _nhaCungCapBus.GetDefaultStatuses().ToList();

        }

        private void Form_NhaCungCap_Load(object? sender, EventArgs e)
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

            // chucVuComboBox.Items.Clear();
            // foreach (var role in _roles)
            // {
            //     chucVuComboBox.Items.Add(role);
            // }
            // chucVuComboBox.SelectedIndex = -1;

            nhaCungCapDataGridView.AutoGenerateColumns = false;
            nhaCungCapDataGridView.DataSource = _bindingSource;
            nhaCungCapDataGridView.SelectionChanged += nhaCungCapDataGridView_SelectionChanged;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(themButton, "Thêm nhà cung cấp mới");
            toolTip.SetToolTip(suaButton, "Sửa thông tin nhà cung cấp đã chọn");
            toolTip.SetToolTip(xoaButton, "Khóa nhà cung cấp đã chọn"); // Updated: "Khóa" instead of "Xóa"
            toolTip.SetToolTip(lamMoiButton, "Làm mới danh sách");
            toolTip.SetToolTip(searchButton, "Tìm kiếm nhà cung cấp");

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

            LoadNhaCungCapData();
        }

        private void nhaCungCapDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            if (nhaCungCapDataGridView.SelectedRows.Count > 0)
            {
                var selectedNhaCungCap = (NhaCungCapDTO)nhaCungCapDataGridView.SelectedRows[0].DataBoundItem;
                maNhaCungCapTextBox.Text = selectedNhaCungCap.MaNhaCungCap.ToString();
                hoTenTextBox.Text = selectedNhaCungCap.TenNhaCungCap ?? string.Empty;
                // ngaySinhDateTimePicker.Value = selectedNhaCungCap.NgaySinh ?? DateTime.Today;
                // gioiTinhNamRadioButton.Checked = selectedNhaCungCap.GioiTinh == "Nam";
                // gioiTinhNuRadioButton.Checked = selectedNhaCungCap.GioiTinh == "Nữ";
                // if (!string.IsNullOrEmpty(selectedNhaCungCap.VaiTro) && chucVuComboBox.Items.Contains(selectedNhaCungCap.VaiTro))
                // {
                //     chucVuComboBox.SelectedItem = selectedNhaCungCap.VaiTro;
                // }
                // else
                // {
                //     chucVuComboBox.SelectedIndex = -1;
                // }
                soDienThoaiTextBox.Text = selectedNhaCungCap.SoDienThoai ?? string.Empty;
                diaChiTextBox.Text = selectedNhaCungCap.DiaChi ?? string.Empty;
                emailTextBox.Text = selectedNhaCungCap.Email ?? string.Empty;


                suaButton.Enabled = true;
                xoaButton.Enabled = true;

                SetInputFieldsEnabled(false);
            }
            else
            {
                maNhaCungCapTextBox.Text = string.Empty;
                hoTenTextBox.Text = string.Empty;
                // ngaySinhDateTimePicker.Value = DateTime.Today;
                // gioiTinhNamRadioButton.Checked = false;
                // gioiTinhNuRadioButton.Checked = false;
                // chucVuComboBox.SelectedIndex = -1;
                soDienThoaiTextBox.Text = string.Empty;
                diaChiTextBox.Text = string.Empty;
                emailTextBox.Text = string.Empty;


                suaButton.Enabled = false;
                xoaButton.Enabled = false;

                SetInputFieldsEnabled(false);
            }
        }

        private void themButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new Form_NhaCungCapDialog(_statuses);
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                var createdNhaCungCap = _nhaCungCapBus.AddNhaCungCap(dialog.NhaCungCap);
                LoadNhaCungCapData();
                SelectNhaCungCapRow(createdNhaCungCap.MaNhaCungCap);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể thêm nhà cung cấp.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void suaButton_Click(object? sender, EventArgs e)
        {
            var selectedNhaCungCap = GetSelectedNhaCungCap();
            if (selectedNhaCungCap == null)
            {
                return;
            }

            using var dialog = new Form_NhaCungCapDialog(_statuses, selectedNhaCungCap);
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                _nhaCungCapBus.UpdateNhaCungCap(dialog.NhaCungCap);
                LoadNhaCungCapData();
                SelectNhaCungCapRow(dialog.NhaCungCap.MaNhaCungCap);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể cập nhật nhà cung cấp.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xoaButton_Click(object? sender, EventArgs e)
        {
            var selectedNhaCungCap = GetSelectedNhaCungCap();
            if (selectedNhaCungCap == null)
            {
                return;
            }

            DialogResult confirm = MessageBox.Show(this,
                $"Bạn có chắc muốn khóa nhà cung cấp '{selectedNhaCungCap.TenNhaCungCap}'? Trạng thái sẽ được chuyển thành 'Khóa'.",
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
                selectedNhaCungCap.TrangThai = NhaCungCap_BUS.StatusInactive; // Set TrangThai to "Khóa"
                _nhaCungCapBus.UpdateNhaCungCap(selectedNhaCungCap);
                LoadNhaCungCapData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể khóa nhà cung cấp.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            LoadNhaCungCapData();
        }

        private NhaCungCapDTO? GetSelectedNhaCungCap()
        {
            if (nhaCungCapDataGridView.SelectedRows.Count == 0)
            {
                return null;
            }

            return nhaCungCapDataGridView.SelectedRows[0].DataBoundItem as NhaCungCapDTO;
        }

        private void SelectNhaCungCapRow(int maNhaCungCap)
        {
            if (maNhaCungCap <= 0 || nhaCungCapDataGridView.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in nhaCungCapDataGridView.Rows)
            {
                if (row.DataBoundItem is NhaCungCapDTO nhaCungCap && nhaCungCap.MaNhaCungCap == maNhaCungCap)
                {
                    row.Selected = true;
                    try
                    {
                        nhaCungCapDataGridView.FirstDisplayedScrollingRowIndex = row.Index;
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
            maNhaCungCapTextBox.Enabled = enabled;
            hoTenTextBox.Enabled = enabled;
            // ngaySinhDateTimePicker.Enabled = enabled;
            // gioiTinhNamRadioButton.Enabled = enabled;
            // gioiTinhNuRadioButton.Enabled = enabled;
            // chucVuComboBox.Enabled = enabled;
            diaChiTextBox.Enabled = enabled;
            emailTextBox.Enabled = enabled;

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

        private void LoadNhaCungCapData()
        {
            try
            {
                _currentNhaCungCap = _nhaCungCapBus.GetNhaCungCap();
                ApplyStatusFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể tải danh sách nhà cung cấp.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStatusFilter()
        {
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(selectedStatus) || string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase))
            {
                _bindingSource.DataSource = _currentNhaCungCap;
            }
            else
            {
                var filtered = new List<NhaCungCapDTO>();
                foreach (var nhaCungCap in _currentNhaCungCap)
                {
                    if (string.Equals(nhaCungCap.TrangThai, selectedStatus, StringComparison.OrdinalIgnoreCase))
                    {
                        filtered.Add(nhaCungCap);
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
                nhaCungCapDataGridView.ClearSelection();
            }
        }

        private void ApplySearchFilter()
        {
            string searchText = searchTextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            var filtered = new List<NhaCungCapDTO>();

            foreach (var nhaCungCap in _currentNhaCungCap)
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                    nhaCungCap.MaNhaCungCap.ToString().ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ||
                    (nhaCungCap.TenNhaCungCap?.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ?? false) ||
                    (nhaCungCap.SoDienThoai?.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(searchText) ?? false);

                bool matchesStatus = string.IsNullOrWhiteSpace(selectedStatus) ||
                    string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(nhaCungCap.TrangThai, selectedStatus, StringComparison.OrdinalIgnoreCase);

                if (matchesSearch && matchesStatus)
                {
                    filtered.Add(nhaCungCap);
                }
            }

            _bindingSource.DataSource = filtered;
            nhaCungCapDataGridView.ClearSelection();
        }
    }
}