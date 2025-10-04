using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.KhachHang
{
    public partial class Form_KhachHangDialog : Form
    {
        // private readonly List<string> _roles;
        private readonly List<string> _statuses;
        private readonly bool _isEdit;
        private readonly KhachHangDTO _workingKhachHang;

        public KhachHangDTO KhachHang => _workingKhachHang;

        public Form_KhachHangDialog(IEnumerable<string> statuses, KhachHangDTO? existingKhachHang = null)
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

            _isEdit = existingKhachHang != null;
            _workingKhachHang = existingKhachHang != null ? CloneKhachHang(existingKhachHang) : new KhachHangDTO();

            Load += Form_KhachHangDialog_Load;
            okButton.Click += okButton_Click;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
        }

        private void Form_KhachHangDialog_Load(object? sender, EventArgs e)
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
                Text = "Sửa khách hàng";
                maKhachHangValueLabel.Text = _workingKhachHang.MaKhachHang.ToString();
            }
            else
            {
                Text = "Thêm khách hàng";
                // Lấy mã khách hàng cao nhất + 1
                var bus = new KhachHang_BUS();
                int nextId = bus.GetNextMaKhachHang();

                maKhachHangValueLabel.Text = nextId.ToString();
                _workingKhachHang.MaKhachHang = nextId;
                _workingKhachHang.TrangThai = KhachHang_BUS.StatusActive;
            }

            hoTenTextBox.Text = _workingKhachHang.TenKhachHang ?? string.Empty;
            soDienThoaiTextBox.Text = _workingKhachHang.SoDienThoai ?? string.Empty;
            // vaiTroComboBox.SelectedItem = !string.IsNullOrEmpty(_workingKhachHang.VaiTro) && _roles.Contains(_workingKhachHang.VaiTro)
            //     ? _workingKhachHang.VaiTro
            //     : null;

            trangThaiComboBox.SelectedItem = !string.IsNullOrEmpty(_workingKhachHang.TrangThai) && _statuses.Contains(_workingKhachHang.TrangThai)
                ? _workingKhachHang.TrangThai
                : (_statuses.Contains(KhachHang_BUS.StatusActive) ? KhachHang_BUS.StatusActive : _statuses.FirstOrDefault());

            diaChiTextBox.Text = _workingKhachHang.DiaChi ?? string.Empty;
            emailTextBox.Text = _workingKhachHang.Email ?? string.Empty;
            diemTichLuyTextBox.Text = _workingKhachHang.DiemTichLuy.ToString();

        }

        private void okButton_Click(object? sender, EventArgs e)
        {
            string hoTen = hoTenTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show(this, "Vui lòng nhập họ tên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hoTenTextBox.SelectAll();
                hoTenTextBox.Focus();
                return;
            }

            if (trangThaiComboBox.SelectedItem is not string trangThai || string.IsNullOrWhiteSpace(trangThai))
            {
                MessageBox.Show(this, "Vui lòng chọn trạng thái.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                trangThaiComboBox.SelectAll();
                trangThaiComboBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(soDienThoaiTextBox.Text))
            {
                MessageBox.Show(this, "Vui lòng nhập số điện thoại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                soDienThoaiTextBox.SelectAll();
                soDienThoaiTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(diaChiTextBox.Text))
            {
                MessageBox.Show(this, "Vui lòng nhập địa chỉ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                diaChiTextBox.SelectAll();
                diaChiTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                MessageBox.Show(this, "Vui lòng nhập email.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailTextBox.SelectAll();
                emailTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(diemTichLuyTextBox.Text))
            {
                MessageBox.Show(this, "Vui lòng nhập điểm tích lũy.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                diemTichLuyTextBox.SelectAll();
                diemTichLuyTextBox.Focus();
                return;
            }

            // Check định dạng số điện thoại
            if (!Validation_Component.IsValidNumberPhone(soDienThoaiTextBox.Text))
            {
                MessageBox.Show(this, "Nhập sai định dạng số điện thoại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                soDienThoaiTextBox.SelectAll();
                soDienThoaiTextBox.Focus();
                return;
            }

            // Check định dạng Email
            if (!Validation_Component.IsValidEmail(emailTextBox.Text))
            {
                MessageBox.Show(this, "Nhập sai định dạng email.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailTextBox.SelectAll();
                emailTextBox.Focus();
                return;
            }

            // Check định dạng điểm tích lũy
            if (!Validation_Component.IsValidNumber(diemTichLuyTextBox.Text))
            {
                MessageBox.Show(this, "Nhập sai định dạng điểm tích lũy.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                diemTichLuyTextBox.SelectAll();
                diemTichLuyTextBox.Focus();
                return;
            }

            _workingKhachHang.TenKhachHang = hoTen;

            _workingKhachHang.SoDienThoai = string.IsNullOrWhiteSpace(soDienThoaiTextBox.Text)
                ? null
                : soDienThoaiTextBox.Text.Trim();
            _workingKhachHang.TrangThai = trangThai;

            // add
            _workingKhachHang.DiaChi = string.IsNullOrWhiteSpace(diaChiTextBox.Text)
            ? null
            : diaChiTextBox.Text.Trim();

            _workingKhachHang.Email = string.IsNullOrWhiteSpace(emailTextBox.Text)
            ? null
            : emailTextBox.Text.Trim();

            _workingKhachHang.DiemTichLuy = string.IsNullOrWhiteSpace(diemTichLuyTextBox.Text)
            ? 0
            : int.Parse(diemTichLuyTextBox.Text);
            // end

            DialogResult = DialogResult.OK;
            Close();
            if(_isEdit)
                MessageBox.Show(this, "Sửa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(this, "Thêm khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static KhachHangDTO CloneKhachHang(KhachHangDTO source)
        {
            return new KhachHangDTO
            {
                MaKhachHang = source.MaKhachHang,
                TenKhachHang = source.TenKhachHang,
                DiaChi = source.DiaChi,
                Email = source.Email,
                SoDienThoai = source.SoDienThoai,
                DiemTichLuy = source.DiemTichLuy,
                TrangThai = source.TrangThai
            };
        }
    }
}