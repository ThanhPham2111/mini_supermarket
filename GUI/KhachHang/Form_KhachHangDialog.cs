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
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            okButton.Click += okButton_Click;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private void Form_KhachHangDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            // Event handlers để clear error khi người dùng nhập/chọn
            hoTenTextBox.TextChanged += (s, e) => { hoTenErrorLabel.Text = string.Empty; hoTenErrorIcon.Visible = false; };
            soDienThoaiTextBox.TextChanged += (s, e) => { soDienThoaiErrorLabel.Text = string.Empty; soDienThoaiErrorIcon.Visible = false; };
            diaChiTextBox.TextChanged += (s, e) => { diaChiErrorLabel.Text = string.Empty; diaChiErrorIcon.Visible = false; };
            emailTextBox.TextChanged += (s, e) => { emailErrorLabel.Text = string.Empty; emailErrorIcon.Visible = false; };
            trangThaiComboBox.SelectedIndexChanged += (s, e) => { trangThaiErrorLabel.Text = string.Empty; trangThaiErrorIcon.Visible = false; };

            // vaiTroComboBox.Items.Clear();
            // vaiTroComboBox.Items.AddRange(_roles.Cast<object>().ToArray());

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            trangThaiComboBox.Items.Clear();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            trangThaiComboBox.Items.AddRange(_statuses.Cast<object>().ToArray());

            // gioiTinhNamRadioButton.Checked = false;
            // gioiTinhNuRadioButton.Checked = false;

            if (_isEdit)
            {
                Text = "Sửa khách hàng";
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                maKhachHangValueLabel.Text = _workingKhachHang.MaKhachHang.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            else
            {
                Text = "Thêm khách hàng";
                // Lấy mã khách hàng cao nhất + 1
                var bus = new KhachHang_BUS();
                int nextId = bus.GetNextMaKhachHang();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                maKhachHangValueLabel.Text = nextId.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                _workingKhachHang.MaKhachHang = nextId;
                _workingKhachHang.TrangThai = KhachHang_BUS.StatusActive;
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            hoTenTextBox.Text = _workingKhachHang.TenKhachHang ?? string.Empty;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            soDienThoaiTextBox.Text = _workingKhachHang.SoDienThoai ?? string.Empty;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            // vaiTroComboBox.SelectedItem = !string.IsNullOrEmpty(_workingKhachHang.VaiTro) && _roles.Contains(_workingKhachHang.VaiTro)
            //     ? _workingKhachHang.VaiTro
            //     : null;

            trangThaiComboBox.SelectedItem = !string.IsNullOrEmpty(_workingKhachHang.TrangThai) && _statuses.Contains(_workingKhachHang.TrangThai)
                ? _workingKhachHang.TrangThai
                : (_statuses.Contains(KhachHang_BUS.StatusActive) ? KhachHang_BUS.StatusActive : _statuses.FirstOrDefault());

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            diaChiTextBox.Text = _workingKhachHang.DiaChi ?? string.Empty;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            emailTextBox.Text = _workingKhachHang.Email ?? string.Empty;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            diemTichLuyTextBox.Text = _workingKhachHang.DiemTichLuy.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        }

        private void ClearAllErrors()
        {
            hoTenErrorLabel.Text = string.Empty;
            hoTenErrorIcon.Visible = false;
            soDienThoaiErrorLabel.Text = string.Empty;
            soDienThoaiErrorIcon.Visible = false;
            diaChiErrorLabel.Text = string.Empty;
            diaChiErrorIcon.Visible = false;
            emailErrorLabel.Text = string.Empty;
            emailErrorIcon.Visible = false;
            trangThaiErrorLabel.Text = string.Empty;
            trangThaiErrorIcon.Visible = false;
        }

        private void ShowError(Label errorLabel, Label errorIcon, string message)
        {
            errorLabel.Text = message;
            errorLabel.ForeColor = System.Drawing.Color.Red;
            errorLabel.Visible = true;
            errorIcon.Visible = true;
        }

        private void okButton_Click(object? sender, EventArgs e)
        {
            ClearAllErrors();
            bool hasError = false;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string hoTen = hoTenTextBox.Text.Trim();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                ShowError(hoTenErrorLabel, hoTenErrorIcon, "Không được để trống");
                hoTenTextBox.Focus();
                hasError = true;
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string soDienThoai = soDienThoaiTextBox.Text.Trim();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Không được để trống");
                if (!hasError) soDienThoaiTextBox.Focus();
                hasError = true;
            }
            else if (!Validation_Component.IsValidNumberPhone(soDienThoai))
            {
                ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Số điện thoại không hợp lệ");
                if (!hasError) soDienThoaiTextBox.Focus();
                hasError = true;
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string diaChi = diaChiTextBox.Text.Trim();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (string.IsNullOrWhiteSpace(diaChi))
            {
                ShowError(diaChiErrorLabel, diaChiErrorIcon, "Không được để trống");
                if (!hasError) diaChiTextBox.Focus();
                hasError = true;
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string email = emailTextBox.Text.Trim();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            // Email không bắt buộc khi tạo, nhưng nếu có nhập thì phải hợp lệ
            if (!string.IsNullOrWhiteSpace(email) && !Validation_Component.IsValidEmail(email))
            {
                ShowError(emailErrorLabel, emailErrorIcon, "Email không hợp lệ");
                if (!hasError) emailTextBox.Focus();
                hasError = true;
            }

            string? trangThai = null;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
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
#pragma warning restore CS8602 // Dereference of a possibly null reference.

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (string.IsNullOrWhiteSpace(diemTichLuyTextBox.Text))
            {
                MessageBox.Show(this, "Vui lòng nhập điểm tích lũy.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!hasError) diemTichLuyTextBox.Focus();
                hasError = true;
            }
            else if (!Validation_Component.IsValidNumber(diemTichLuyTextBox.Text))
            {
                MessageBox.Show(this, "Nhập sai định dạng điểm tích lũy.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!hasError) diemTichLuyTextBox.Focus();
                hasError = true;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (hasError)
                return;

            _workingKhachHang.TenKhachHang = hoTen;
            _workingKhachHang.SoDienThoai = soDienThoai;
            _workingKhachHang.DiaChi = diaChi;
            _workingKhachHang.Email = string.IsNullOrWhiteSpace(email) ? null : email;
            _workingKhachHang.TrangThai = trangThai!;
            _workingKhachHang.DiemTichLuy = string.IsNullOrWhiteSpace(diemTichLuyTextBox.Text) ? 0 : int.Parse(diemTichLuyTextBox.Text);

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