using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.NhaCungCap
{
    public partial class Form_NhaCungCapDialog : Form
    {
        // private readonly List<string> _roles;
        private readonly List<string> _statuses;
        private readonly bool _isEdit;
        private readonly NhaCungCapDTO _workingNhaCungCap;

        public NhaCungCapDTO NhaCungCap => _workingNhaCungCap;

        public Form_NhaCungCapDialog(IEnumerable<string> statuses, NhaCungCapDTO? existingNhaCungCap = null)
        {
            // if (roles == null)
            // {
            //     throw new ArgumentNullException(nameof(roles));
            // }

            if (statuses == null)
            {
                throw new ArgumentNullException(nameof(statuses));
            }

            InitializeComponent();

            // _roles = roles.Where(r => !string.IsNullOrWhiteSpace(r)).Distinct().ToList();
            _statuses = statuses.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            _isEdit = existingNhaCungCap != null;
            _workingNhaCungCap = existingNhaCungCap != null ? CloneNhaCungCap(existingNhaCungCap) : new NhaCungCapDTO();

            Load += Form_NhaCungCapDialog_Load;
            okButton.Click += okButton_Click;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
        }

        private void Form_NhaCungCapDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            // vaiTroComboBox.Items.Clear();
            // vaiTroComboBox.Items.AddRange(_roles.Cast<object>().ToArray());

            trangThaiComboBox.Items.Clear();
            trangThaiComboBox.Items.AddRange(_statuses.Cast<object>().ToArray());

            // gioiTinhNamRadioButton.Checked = false;
            // gioiTinhNuRadioButton.Checked = false;

            if (_isEdit)
            {
                Text = "Sửa nhà cung cấp";
                maNhaCungCapValueLabel.Text = _workingNhaCungCap.MaNhaCungCap.ToString();
            }
            else
            {
                Text = "Thêm nhà cung cấp";
                // Lấy mã khách hàng cao nhất + 1
                var bus = new NhaCungCap_BUS();
                int nextId = bus.GetNextMaNhaCungCap();

                maNhaCungCapValueLabel.Text = nextId.ToString();
                _workingNhaCungCap.MaNhaCungCap = nextId;
                _workingNhaCungCap.TrangThai = NhaCungCap_BUS.StatusActive;
            }

            hoTenTextBox.Text = _workingNhaCungCap.TenNhaCungCap ?? string.Empty;
            soDienThoaiTextBox.Text = _workingNhaCungCap.SoDienThoai ?? string.Empty;
            // vaiTroComboBox.SelectedItem = !string.IsNullOrEmpty(_workingNhaCungCap.VaiTro) && _roles.Contains(_workingNhaCungCap.VaiTro)
            //     ? _workingNhaCungCap.VaiTro
            //     : null;

            trangThaiComboBox.SelectedItem = !string.IsNullOrEmpty(_workingNhaCungCap.TrangThai) && _statuses.Contains(_workingNhaCungCap.TrangThai)
                ? _workingNhaCungCap.TrangThai
                : (_statuses.Contains(NhaCungCap_BUS.StatusActive) ? NhaCungCap_BUS.StatusActive : _statuses.FirstOrDefault());

            // if (_workingNhaCungCap.NgaySinh.HasValue)
            // {
            //     ngaySinhDateTimePicker.Value = _workingNhaCungCap.NgaySinh.Value.Date;
            // }
            // else
            // {
            //     ngaySinhDateTimePicker.Value = DateTime.Today.AddYears(-18);
            // }

            // format hiển thị số
            // ngaySinhDateTimePicker.Format = DateTimePickerFormat.Custom;
            // ngaySinhDateTimePicker.CustomFormat = "dd/MM/yyyy";
            // ngaySinhDateTimePicker.ShowUpDown = true;

            // switch (_workingNhaCungCap.GioiTinh)
            // {
            //     case "Nam":
            //         gioiTinhNamRadioButton.Checked = true;
            //         break;
            //     case "Nữ":
            //         gioiTinhNuRadioButton.Checked = true;
            //         break;
            // }
            diaChiTextBox.Text = _workingNhaCungCap.DiaChi ?? string.Empty;
            emailTextBox.Text = _workingNhaCungCap.Email ?? string.Empty;
           

        }

        private void okButton_Click(object? sender, EventArgs e)
        {
            string hoTen = hoTenTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show(this, "Vui lòng nhập họ tên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hoTenTextBox.Focus();
                return;
            }

            // if (ngaySinhDateTimePicker.Value.Date > DateTime.Today)
            // {
            //     MessageBox.Show(this, "Ngày sinh không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     ngaySinhDateTimePicker.Focus();
            //     return;
            // }

            if (trangThaiComboBox.SelectedItem is not string trangThai || string.IsNullOrWhiteSpace(trangThai))
            {
                MessageBox.Show(this, "Vui lòng chọn trạng thái.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                trangThaiComboBox.Focus();
                return;
            }

            _workingNhaCungCap.TenNhaCungCap = hoTen;
            // _workingNhaCungCap.NgaySinh = ngaySinhDateTimePicker.Value.Date;
            // _workingNhaCungCap.GioiTinh = gioiTinhNamRadioButton.Checked
            //     ? "Nam"
            //     : gioiTinhNuRadioButton.Checked ? "Nữ" : null;
            // _workingNhaCungCap.VaiTro = vaiTroComboBox.SelectedItem as string;
            _workingNhaCungCap.SoDienThoai = string.IsNullOrWhiteSpace(soDienThoaiTextBox.Text)
                ? null
                : soDienThoaiTextBox.Text.Trim();
            _workingNhaCungCap.TrangThai = trangThai;

            // add
            _workingNhaCungCap.DiaChi = string.IsNullOrWhiteSpace(diaChiTextBox.Text)
            ? null
            : diaChiTextBox.Text.Trim();

            _workingNhaCungCap.Email = string.IsNullOrWhiteSpace(emailTextBox.Text)
            ? null
            : emailTextBox.Text.Trim();


            DialogResult = DialogResult.OK;
            Close();
        }

        private static NhaCungCapDTO CloneNhaCungCap(NhaCungCapDTO source)
        {
            return new NhaCungCapDTO
            {
                MaNhaCungCap = source.MaNhaCungCap,
                TenNhaCungCap = source.TenNhaCungCap,
                DiaChi = source.DiaChi,
                Email = source.Email,
                SoDienThoai = source.SoDienThoai,
                TrangThai = source.TrangThai
            };
        }
    }
}