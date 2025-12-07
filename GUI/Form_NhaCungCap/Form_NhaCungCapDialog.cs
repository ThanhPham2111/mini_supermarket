using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

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
            // set max length
            hoTenTextBox.MaxLength = 50;
            diaChiTextBox.MaxLength = 70;
            emailTextBox.MaxLength = 50;
            soDienThoaiTextBox.MaxLength = 11;


            // Cho phép nhấn Enter = OK
            this.AcceptButton = okButton;

            _statuses = statuses.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            _isEdit = existingNhaCungCap != null;
            _workingNhaCungCap = existingNhaCungCap != null
                ? CloneNhaCungCap(existingNhaCungCap)
                : new NhaCungCapDTO();

            Load += Form_NhaCungCapDialog_Load;
            okButton.Click += okButton_Click;
            cancelButton.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        }

        private void Form_NhaCungCapDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            // Xóa lỗi khi người dùng bắt đầu nhập
            hoTenTextBox.TextChanged += (_, _) => ClearError(hoTenErrorLabel, hoTenErrorIcon);
            soDienThoaiTextBox.TextChanged += (_, _) => ClearError(soDienThoaiErrorLabel, soDienThoaiErrorIcon);
            diaChiTextBox.TextChanged += (_, _) => ClearError(diaChiErrorLabel, diaChiErrorIcon);
            emailTextBox.TextChanged += (_, _) => ClearError(emailErrorLabel, emailErrorIcon);
            trangThaiComboBox.SelectedIndexChanged += (_, _) => ClearError(trangThaiErrorLabel, trangThaiErrorIcon);

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

            // Gán dữ liệu
            hoTenTextBox.Text = _workingNhaCungCap.TenNhaCungCap ?? string.Empty;
            soDienThoaiTextBox.Text = _workingNhaCungCap.SoDienThoai ?? string.Empty;
            diaChiTextBox.Text = _workingNhaCungCap.DiaChi ?? string.Empty;
            emailTextBox.Text = _workingNhaCungCap.Email ?? string.Empty;

            // Chọn trạng thái mặc định
            trangThaiComboBox.SelectedItem = !string.IsNullOrWhiteSpace(_workingNhaCungCap.TrangThai) &&
                                            _statuses.Contains(_workingNhaCungCap.TrangThai)
                ? _workingNhaCungCap.TrangThai
                : (_statuses.Contains(NhaCungCap_BUS.StatusActive)
                    ? NhaCungCap_BUS.StatusActive
                    : _statuses.FirstOrDefault());
        }

        // ==========================
        // NÚT OK
        // ==========================
        private void okButton_Click(object? sender, EventArgs e)
        {
            ClearAllErrors();
            bool hasError = false;

            if (!ValidateHoTen())
            {
                ShowError(hoTenErrorLabel, hoTenErrorIcon, "Tên nhà cung cấp phải có ít nhất 5 ký tự");
                hoTenTextBox.Focus();
                hasError = true;
            }

            if (!ValidateSoDienThoai())
            {
                ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Số điện thoại không hợp lệ");
                if (!hasError) soDienThoaiTextBox.Focus();
                hasError = true;
            }

            if (!ValidateDiaChi())
            {
                ShowError(diaChiErrorLabel, diaChiErrorIcon, "Địa chỉ không được để trống");
                if (!hasError) diaChiTextBox.Focus();
                hasError = true;
            }

            if (!ValidateEmail())
            {
                ShowError(emailErrorLabel, emailErrorIcon, "Email không hợp lệ");
                if (!hasError) emailTextBox.Focus();
                hasError = true;
            }

            if (trangThaiComboBox.SelectedItem is not string selectedStatus || string.IsNullOrWhiteSpace(selectedStatus))
            {
                ShowError(trangThaiErrorLabel, trangThaiErrorIcon, "Vui lòng chọn trạng thái");
                if (!hasError) trangThaiComboBox.Focus();
                hasError = true;
            }

            if (hasError) return;

            // Lưu dữ liệu vào DTO
            _workingNhaCungCap.TenNhaCungCap = hoTenTextBox.Text.Trim();
            _workingNhaCungCap.SoDienThoai = soDienThoaiTextBox.Text.Trim();
            _workingNhaCungCap.DiaChi = diaChiTextBox.Text.Trim();
            _workingNhaCungCap.Email = emailTextBox.Text.Trim();
            _workingNhaCungCap.TrangThai = trangThaiComboBox.SelectedItem as string ?? string.Empty;

            DialogResult = DialogResult.OK;
            Close();
        }

        // ==========================
        // HELPER: Error
        // ==========================
        private void ShowError(Label errorLabel, Control errorIcon, string message)
        {
            errorLabel.Text = message;
            errorLabel.ForeColor = System.Drawing.Color.Red;
            errorLabel.Visible = true;
            errorIcon.Visible = true;
        }

        private void ClearError(Label errorLabel, Control errorIcon)
        {
            errorLabel.Text = string.Empty;
            errorLabel.Visible = false;
            errorIcon.Visible = false;
        }

        private void ClearAllErrors()
        {
            ClearError(hoTenErrorLabel, hoTenErrorIcon);
            ClearError(soDienThoaiErrorLabel, soDienThoaiErrorIcon);
            ClearError(diaChiErrorLabel, diaChiErrorIcon);
            ClearError(emailErrorLabel, emailErrorIcon);
            ClearError(trangThaiErrorLabel, trangThaiErrorIcon);
        }

        // ==========================
        // VALIDATION
        // ==========================
        private bool ValidateHoTen()
        {
            string t = hoTenTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                ShowError(hoTenErrorLabel, hoTenErrorIcon, "Tên nhà cung cấp không được để trống");
                return false;
            }
           if (t.Length < 5)
             {
                ShowError(hoTenErrorLabel, hoTenErrorIcon, "Tên nhà cung cấp phải có ít nhất 5 ký tự và dưới 51 ký tự");
                return false;
            }
            if(t.Length > 50)
            {
                ShowError(hoTenErrorLabel, hoTenErrorIcon, "Tên nhà cung cấp không được vượt quá 50 ký tự");
                return false;
            }
            return true;
        }

        private bool ValidateSoDienThoai()
        {
            string input = soDienThoaiTextBox.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Không được để trống");
                return false;
            }

            // Chuẩn hóa: loại bỏ dấu cách, gạch ngang, ngoặc, dấu cộng...
            string normalized = Regex.Replace(input, @"[\s\-\(\)\+\.]", "");

            // Regex chuẩn số điện thoại Việt Nam (10 hoặc 11 số, bắt đầu bằng 0 + đầu số hợp lệ)
            const string pattern = @"^0[235789]\d{8,9}$";

            if (!Regex.IsMatch(normalized, pattern))
            {
                ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Số điện thoại không hợp lệ");
                return false;
            }

            return true;
        }

        private bool ValidateEmail()
        {
            string email = emailTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                ShowError(emailErrorLabel, emailErrorIcon, "Email không được để trống");
                return false;
            }

            // Regex email đơn giản nhưng đủ tốt
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                ShowError(emailErrorLabel, emailErrorIcon, "Email không đúng định dạng");
                return false;
            }

            return true;
        }

      private bool ValidateDiaChi()
        {
            string t = diaChiTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(t))
            {
                ShowError(diaChiErrorLabel, diaChiErrorIcon, "Địa chỉ không được để trống");
                return false;
            }

            if (t.Length > 70)
            {
                ShowError(diaChiErrorLabel, diaChiErrorIcon, "Địa chỉ không được vượt quá 70 ký tự");
                return false;
            }

            return true;
        }

        // ==========================
        // Clone DTO
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