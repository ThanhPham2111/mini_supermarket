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

            vaiTroComboBox.Items.Clear();
            vaiTroComboBox.Items.AddRange(_roles.Cast<object>().ToArray());

            trangThaiComboBox.Items.Clear();
            trangThaiComboBox.Items.AddRange(_statuses.Cast<object>().ToArray());

            gioiTinhNamRadioButton.Checked = false;
            gioiTinhNuRadioButton.Checked = false;

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

            hoTenTextBox.Text = _workingNhanVien.TenNhanVien ?? string.Empty;
            soDienThoaiTextBox.Text = _workingNhanVien.SoDienThoai ?? string.Empty;
            vaiTroComboBox.SelectedItem = !string.IsNullOrEmpty(_workingNhanVien.VaiTro) && _roles.Contains(_workingNhanVien.VaiTro)
                ? _workingNhanVien.VaiTro
                : null;

            trangThaiComboBox.SelectedItem = !string.IsNullOrEmpty(_workingNhanVien.TrangThai) && _statuses.Contains(_workingNhanVien.TrangThai)
                ? _workingNhanVien.TrangThai
                : (_statuses.Contains(NhanVien_BUS.StatusWorking) ? NhanVien_BUS.StatusWorking : _statuses.FirstOrDefault());

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

        private void okButton_Click(object? sender, EventArgs e)
        {
            string hoTen = hoTenTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show(this, "Vui long nhap ho ten.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hoTenTextBox.Focus();
                return;
            }

            if (ngaySinhDateTimePicker.Value.Date > DateTime.Today)
            {
                MessageBox.Show(this, "Ngay sinh khong hop le.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ngaySinhDateTimePicker.Focus();
                return;
            }

            string soDienThoai = soDienThoaiTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                MessageBox.Show(this, "So dien thoai khong duoc de trong.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                soDienThoaiTextBox.Focus();
                return;
            }

            if (soDienThoai.Length != 10 || !soDienThoai.All(char.IsDigit))
            {
                MessageBox.Show(this, "So dien thoai phai gom 10 chu so.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                soDienThoaiTextBox.Focus();
                return;
            }

            if (vaiTroComboBox.SelectedItem is not string vaiTro || string.IsNullOrWhiteSpace(vaiTro))
            {
                MessageBox.Show(this, "Vui long chon vai tro.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                vaiTroComboBox.Focus();
                return;
            }

            if (trangThaiComboBox.SelectedItem is not string trangThai || string.IsNullOrWhiteSpace(trangThai))
            {
                MessageBox.Show(this, "Vui long chon trang thai.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                trangThaiComboBox.Focus();
                return;
            }

            _workingNhanVien.TenNhanVien = hoTen;
            _workingNhanVien.NgaySinh = ngaySinhDateTimePicker.Value.Date;
            _workingNhanVien.GioiTinh = gioiTinhNamRadioButton.Checked
                ? "Nam"
                : gioiTinhNuRadioButton.Checked ? "Nu" : null;
            _workingNhanVien.VaiTro = vaiTro;
            _workingNhanVien.SoDienThoai = soDienThoai;
            _workingNhanVien.TrangThai = trangThai;

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


