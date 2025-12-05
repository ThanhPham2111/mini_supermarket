using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;
using System.Text.RegularExpressions;

namespace mini_supermarket.GUI.NhaCungCap
{
    public partial class Form_NhaCungCapDialog : Form
    {
        private readonly List<string> _statuses;
        private readonly bool _isEdit;
        private readonly NhaCungCapDTO _workingNhaCungCap;

        public NhaCungCapDTO NhaCungCap => _workingNhaCungCap;

        public Form_NhaCungCapDialog(IEnumerable<string> statuses, NhaCungCapDTO? existingNhaCungCap = null)
        {
            if (statuses == null)
                throw new ArgumentNullException(nameof(statuses));

            InitializeComponent();
        //  cho phép Enter = OK
        this.AcceptButton = okButton;
        
            _statuses = statuses.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            _isEdit = existingNhaCungCap != null;
            _workingNhaCungCap = existingNhaCungCap != null
                ? CloneNhaCungCap(existingNhaCungCap)
                : new NhaCungCapDTO();

            Load += Form_NhaCungCapDialog_Load;
            okButton.Click += okButton_Click;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
        }

        private void Form_NhaCungCapDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
                return;

            // Event handlers để clear error khi người dùng nhập/chọn
            hoTenTextBox.TextChanged += (s, e) => { hoTenErrorLabel.Text = string.Empty; hoTenErrorIcon.Visible = false; };
            soDienThoaiTextBox.TextChanged += (s, e) => { soDienThoaiErrorLabel.Text = string.Empty; soDienThoaiErrorIcon.Visible = false; };
            diaChiTextBox.TextChanged += (s, e) => { diaChiErrorLabel.Text = string.Empty; diaChiErrorIcon.Visible = false; };
            emailTextBox.TextChanged += (s, e) => { emailErrorLabel.Text = string.Empty; emailErrorIcon.Visible = false; };
            trangThaiComboBox.SelectedIndexChanged += (s, e) => { trangThaiErrorLabel.Text = string.Empty; trangThaiErrorIcon.Visible = false; };

            trangThaiComboBox.Items.Clear();
            trangThaiComboBox.Items.AddRange(_statuses.Cast<object>().ToArray());

            if (_isEdit)
            {
                Text = "Sửa nhà cung cấp";
                maNhaCungCapValueLabel.Text = _workingNhaCungCap.MaNhaCungCap.ToString();
            }
            else
            {
                Text = "Thêm nhà cung cấp";

                var bus = new NhaCungCap_BUS();
                int nextId = bus.GetNextMaNhaCungCap();

                maNhaCungCapValueLabel.Text = nextId.ToString();
                _workingNhaCungCap.MaNhaCungCap = nextId;
                _workingNhaCungCap.TrangThai = NhaCungCap_BUS.StatusActive;
            }

            hoTenTextBox.Text = _workingNhaCungCap.TenNhaCungCap ?? string.Empty;
            soDienThoaiTextBox.Text = _workingNhaCungCap.SoDienThoai ?? string.Empty;
            diaChiTextBox.Text = _workingNhaCungCap.DiaChi ?? string.Empty;
            emailTextBox.Text = _workingNhaCungCap.Email ?? string.Empty;

            trangThaiComboBox.SelectedItem =
                !string.IsNullOrEmpty(_workingNhaCungCap.TrangThai) &&
                _statuses.Contains(_workingNhaCungCap.TrangThai)
                    ? _workingNhaCungCap.TrangThai
                    : (_statuses.Contains(NhaCungCap_BUS.StatusActive)
                        ? NhaCungCap_BUS.StatusActive
                        : _statuses.FirstOrDefault());
        }

        // ==========================
        // ✅ NÚT OK
        // ==========================
        private void okButton_Click(object? sender, EventArgs e)
        {
            ClearAllErrors();
            bool hasError = false;

            // Validate từng field
            if (!ValidateHoTen())
            {
                hoTenTextBox.Focus();
                hasError = true;
            }

            if (!ValidateSoDienThoai())
            {
                if (!hasError) soDienThoaiTextBox.Focus();
                hasError = true;
            }

            if (!ValidateDiaChi())
            {
                if (!hasError) diaChiTextBox.Focus();
                hasError = true;
            }

            if (!ValidateEmail())
            {
                if (!hasError) emailTextBox.Focus();
                hasError = true;
            }

            string? trangThai = null;
            if (trangThaiComboBox.SelectedItem is string t && !string.IsNullOrWhiteSpace(t))
            {
                trangThai = t;
            }
            else
            {
                ShowError(trangThaiErrorLabel, trangThaiErrorIcon, "Vui lòng chọn trạng thái.");
                if (!hasError) trangThaiComboBox.Focus();
                hasError = true;
            }

            if (hasError)
                return;

            // ✅ Lưu dữ liệu
            _workingNhaCungCap.TenNhaCungCap = hoTenTextBox.Text.Trim();
            _workingNhaCungCap.SoDienThoai = soDienThoaiTextBox.Text.Trim();
            _workingNhaCungCap.DiaChi = diaChiTextBox.Text.Trim();
            _workingNhaCungCap.Email = emailTextBox.Text.Trim();
            _workingNhaCungCap.TrangThai = trangThai!;

            DialogResult = DialogResult.OK;
            Close();
        }


        // ==========================
        // ✅ SET ERROR HELPER
        // ==========================
        private void ShowError(Label errorLabel, Label errorIcon, string message)
        {
            errorLabel.Text = message;
            errorLabel.ForeColor = System.Drawing.Color.Red;
            errorLabel.Visible = true;
            errorIcon.Visible = true;
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

        // ==========================
        // ✅ VALIDATION
        // ==========================
        private bool ValidateHoTen()
        {
            string t = hoTenTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                ShowError(hoTenErrorLabel, hoTenErrorIcon, "Không được để trống");
                return false;
            }
            hoTenErrorLabel.Text = string.Empty;
            hoTenErrorIcon.Visible = false;
            return true;
        }

        private bool ValidateSoDienThoai()
        {
            string t = soDienThoaiTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Không được để trống");
                return false;
            }
            if (t.Length != 10 || !t.All(char.IsDigit))
            {
                ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Phải 10 chữ số");
                return false;
            }
            soDienThoaiErrorLabel.Text = string.Empty;
            soDienThoaiErrorIcon.Visible = false;
            return true;
        }

        private bool ValidateEmail()
        {
            string v = emailTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(v))
            {
                ShowError(emailErrorLabel, emailErrorIcon, "Không được để trống");
                return false;
            }

            if (!Regex.IsMatch(v, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                ShowError(emailErrorLabel, emailErrorIcon, "Email không hợp lệ");
                return false;
            }

            emailErrorLabel.Text = string.Empty;
            emailErrorIcon.Visible = false;
            return true;
        }

        private bool ValidateDiaChi()
        {
            string t = diaChiTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                ShowError(diaChiErrorLabel, diaChiErrorIcon, "Không được để trống");
                return false;
            }
            diaChiErrorLabel.Text = string.Empty;
            diaChiErrorIcon.Visible = false;
            return true;
        }

      

        // ==========================
        // ✅ Clone DTO
        // ==========================
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
