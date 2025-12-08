using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.NhanVien
{
    public partial class Form_NhanVienDialog : Form
    {
        private readonly List<string> _roles;
        private readonly List<string> _statuses;
        private readonly bool _isEdit;
        private readonly NhanVienDTO _workingNhanVien;

        public NhanVienDTO NhanVien => _workingNhanVien;

        public Form_NhanVienDialog(IEnumerable<string> roles, IEnumerable<string> statuses, NhanVienDTO? existingNhanVien = null)
        {
            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }

            if (statuses == null)
            {
                throw new ArgumentNullException(nameof(statuses));
            }

            InitializeComponent();

            _roles = roles.Where(r => !string.IsNullOrWhiteSpace(r)).Distinct().ToList();
            _statuses = statuses.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            _isEdit = existingNhanVien != null;
            _workingNhanVien = existingNhanVien != null ? CloneNhanVien(existingNhanVien) : new NhanVienDTO();

            Load += Form_NhanVienDialog_Load;
            okButton.Click += okButton_Click;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
        }

        private void Form_NhanVienDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            try
            {
                // Event handlers để clear error khi người dùng nhập/chọn
                if (hoTenErrorLabel != null && hoTenErrorIcon != null)
                {
                    hoTenTextBox.TextChanged += (s, e) => { hoTenErrorLabel.Text = string.Empty; hoTenErrorIcon.Visible = false; };
                }
                if (soDienThoaiErrorLabel != null && soDienThoaiErrorIcon != null)
                {
                    soDienThoaiTextBox.TextChanged += (s, e) => { soDienThoaiErrorLabel.Text = string.Empty; soDienThoaiErrorIcon.Visible = false; };
                }
                if (vaiTroErrorLabel != null && vaiTroErrorIcon != null)
                {
                    vaiTroComboBox.SelectedIndexChanged += (s, e) => { vaiTroErrorLabel.Text = string.Empty; vaiTroErrorIcon.Visible = false; };
                }
                if (trangThaiErrorLabel != null && trangThaiErrorIcon != null)
                {
                    trangThaiComboBox.SelectedIndexChanged += (s, e) => { trangThaiErrorLabel.Text = string.Empty; trangThaiErrorIcon.Visible = false; };
                }

                vaiTroComboBox.Items.Clear();
                if (_roles != null && _roles.Count > 0)
                {
                    vaiTroComboBox.Items.AddRange(_roles.Cast<object>().ToArray());
                }

                trangThaiComboBox.Items.Clear();
                if (_statuses != null && _statuses.Count > 0)
                {
                    trangThaiComboBox.Items.AddRange(_statuses.Cast<object>().ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Lỗi khi khởi tạo form: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            gioiTinhNamRadioButton.Checked = false;
            gioiTinhNuRadioButton.Checked = false;

            try
            {
                if (_isEdit)
                {
                    Text = "Sửa nhân viên";
                    maNhanVienValueLabel.Text = _workingNhanVien.MaNhanVien.ToString();
                }
                else
                {
                    Text = "Thêm nhân viên";
                    // Lấy mã nhân viên cao nhất + 1
                    var bus = new NhanVien_BUS();
                    int nextId = bus.GetNextMaNhanVien();

                    maNhanVienValueLabel.Text = nextId.ToString();
                    _workingNhanVien.MaNhanVien = nextId;
                    _workingNhanVien.TrangThai = NhanVien_BUS.StatusWorking;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Lỗi khi lấy mã nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            hoTenTextBox.Text = _workingNhanVien.TenNhanVien ?? string.Empty;
            soDienThoaiTextBox.Text = _workingNhanVien.SoDienThoai ?? string.Empty;
            
            if (!string.IsNullOrEmpty(_workingNhanVien.VaiTro) && _roles != null && _roles.Contains(_workingNhanVien.VaiTro))
            {
                vaiTroComboBox.SelectedItem = _workingNhanVien.VaiTro;
            }
            else
            {
                vaiTroComboBox.SelectedIndex = -1;
            }

            if (!string.IsNullOrEmpty(_workingNhanVien.TrangThai) && _statuses != null && _statuses.Contains(_workingNhanVien.TrangThai))
            {
                trangThaiComboBox.SelectedItem = _workingNhanVien.TrangThai;
            }
            else if (_statuses != null && _statuses.Contains(NhanVien_BUS.StatusWorking))
            {
                trangThaiComboBox.SelectedItem = NhanVien_BUS.StatusWorking;
            }
            else if (_statuses != null && _statuses.Count > 0)
            {
                trangThaiComboBox.SelectedIndex = 0;
            }

            if (_workingNhanVien.NgaySinh.HasValue)
            {
                ngaySinhDateTimePicker.Value = _workingNhanVien.NgaySinh.Value.Date;
            }
            else
            {
                ngaySinhDateTimePicker.Value = DateTime.Today.AddYears(-18);
            }

            // format hiển thị số
            ngaySinhDateTimePicker.Format = DateTimePickerFormat.Custom;
            ngaySinhDateTimePicker.CustomFormat = "dd/MM/yyyy";
            ngaySinhDateTimePicker.ShowUpDown = true;

            switch (_workingNhanVien.GioiTinh)
            {
                case "Nam":
                    gioiTinhNamRadioButton.Checked = true;
                    break;
                case "Nu":
                case "N?":
                case "N??_":
                    gioiTinhNuRadioButton.Checked = true;
                    break;
            }
        }

        private void ClearAllErrors()
        {
            if (hoTenErrorLabel != null) hoTenErrorLabel.Text = string.Empty;
            if (hoTenErrorIcon != null) hoTenErrorIcon.Visible = false;
            if (soDienThoaiErrorLabel != null) soDienThoaiErrorLabel.Text = string.Empty;
            if (soDienThoaiErrorIcon != null) soDienThoaiErrorIcon.Visible = false;
            if (vaiTroErrorLabel != null) vaiTroErrorLabel.Text = string.Empty;
            if (vaiTroErrorIcon != null) vaiTroErrorIcon.Visible = false;
            if (trangThaiErrorLabel != null) trangThaiErrorLabel.Text = string.Empty;
            if (trangThaiErrorIcon != null) trangThaiErrorIcon.Visible = false;
        }

        private void ShowError(Label errorLabel, Label errorIcon, string message)
        {
            if (errorLabel != null)
            {
                errorLabel.Text = message;
                errorLabel.ForeColor = System.Drawing.Color.Red;
                errorLabel.Visible = true;
            }
            if (errorIcon != null)
            {
                errorIcon.Visible = true;
            }
        }

        private void okButton_Click(object? sender, EventArgs e)
        {
            ClearAllErrors();
            bool hasError = false;

            string hoTen = hoTenTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                ShowError(hoTenErrorLabel, hoTenErrorIcon, "Không được để trống");
                hoTenTextBox.Focus();
                hasError = true;
            }

            if (ngaySinhDateTimePicker.Value.Date > DateTime.Today)
            {
                MessageBox.Show(this, "Ngày sinh không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!hasError) ngaySinhDateTimePicker.Focus();
                hasError = true;
            }

            string soDienThoai = soDienThoaiTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Không được để trống");
                if (!hasError) soDienThoaiTextBox.Focus();
                hasError = true;
            }
            else if (soDienThoai.Length > 10 || !soDienThoai.All(char.IsDigit))
            {
                ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Phải 10 chữ số");
                if (!hasError) soDienThoaiTextBox.Focus();
                hasError = true;
            }

            string? vaiTro = null;
            if (vaiTroComboBox.SelectedItem is string vt && !string.IsNullOrWhiteSpace(vt))
            {
                vaiTro = vt;
            }
            else
            {
                ShowError(vaiTroErrorLabel, vaiTroErrorIcon, "Vui lòng chọn vai trò");
                if (!hasError) vaiTroComboBox.Focus();
                hasError = true;
            }

            string? trangThai = null;
            if (trangThaiComboBox.SelectedItem is string tt && !string.IsNullOrWhiteSpace(tt))
            {
                trangThai = tt;
            }
            else
            {
                ShowError(trangThaiErrorLabel, trangThaiErrorIcon, "Vui lòng chọn trạng thái");
                if (!hasError) trangThaiComboBox.Focus();
                hasError = true;
            }

            if (hasError)
                return;

            _workingNhanVien.TenNhanVien = hoTen;
            _workingNhanVien.NgaySinh = ngaySinhDateTimePicker.Value.Date;
            _workingNhanVien.GioiTinh = gioiTinhNamRadioButton.Checked
                ? "Nam"
                : gioiTinhNuRadioButton.Checked ? "Nữ" : null;
            _workingNhanVien.VaiTro = vaiTro!;
            _workingNhanVien.SoDienThoai = soDienThoai;
            _workingNhanVien.TrangThai = trangThai!;

            DialogResult = DialogResult.OK;
            Close();
        }

        private static NhanVienDTO CloneNhanVien(NhanVienDTO source)
        {
            return new NhanVienDTO
            {
                MaNhanVien = source.MaNhanVien,
                TenNhanVien = source.TenNhanVien,
                GioiTinh = source.GioiTinh,
                NgaySinh = source.NgaySinh,
                SoDienThoai = source.SoDienThoai,
                VaiTro = source.VaiTro,
                TrangThai = source.TrangThai
            };
        }
    }
}


