
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
        private readonly NhaCungCapDTO _nhaCungCap;
        private readonly NhaCungCap_BUS _bus = new(); // C# 12: top-level object creation

        public NhaCungCapDTO NhaCungCap => _nhaCungCap;

        public Form_NhaCungCapDialog(IEnumerable<string> statuses, NhaCungCapDTO? existing = null)
        {
            if (statuses == null) throw new ArgumentNullException(nameof(statuses));

            InitializeComponent();
            _statuses = statuses.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            _isEdit = existing != null;
            _nhaCungCap = existing != null ? Clone(existing) : new NhaCungCapDTO();

            // Thiết lập form
            AcceptButton = okButton;
            Load += Form_Load;
            okButton.Click += OkButton_Click;
            cancelButton.Click += (_, _) => CloseWithResult(DialogResult.Cancel);

            // MaxLength
            hoTenTextBox.MaxLength = 50;
            diaChiTextBox.MaxLength = 70;
            emailTextBox.MaxLength = 50;
            soDienThoaiTextBox.MaxLength = 11;

            // Auto clear error khi nhập
            hoTenTextBox.TextChanged += (_, _) => ClearError(hoTenErrorLabel, hoTenErrorIcon);
            soDienThoaiTextBox.TextChanged += (_, _) => ClearError(soDienThoaiErrorLabel, soDienThoaiErrorIcon);
            diaChiTextBox.TextChanged += (_, _) => ClearError(diaChiErrorLabel, diaChiErrorIcon);
            emailTextBox.TextChanged += (_, _) => ClearError(emailErrorLabel, emailErrorIcon);
            trangThaiComboBox.SelectedIndexChanged += (_, _) => ClearError(trangThaiErrorLabel, trangThaiErrorIcon);
        }

        private void Form_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            trangThaiComboBox.Items.AddRange(_statuses.Cast<object>().ToArray());

            if (_isEdit)
            {
                Text = "Sửa nhà cung cấp";
                maNhaCungCapValueLabel.Text = _nhaCungCap.MaNhaCungCap.ToString();
            }
            else
            {
                Text = "Thêm nhà cung cấp";
                int nextId = _bus.GetNextMaNhaCungCap();
                _nhaCungCap.MaNhaCungCap = nextId;
                maNhaCungCapValueLabel.Text = nextId.ToString();
                _nhaCungCap.TrangThai = NhaCungCap_BUS.StatusActive;
            }

            // Bind dữ liệu
            hoTenTextBox.Text = _nhaCungCap.TenNhaCungCap;
            diaChiTextBox.Text = _nhaCungCap.DiaChi;
            soDienThoaiTextBox.Text = _nhaCungCap.SoDienThoai;
            emailTextBox.Text = _nhaCungCap.Email;

            trangThaiComboBox.SelectedItem = _nhaCungCap.TrangThai is { } status && _statuses.Contains(status)
                ? status
                : NhaCungCap_BUS.StatusActive;
        }

        private void OkButton_Click(object? sender, EventArgs e)
        {
            ClearAllErrors();

            if (!ValidateAll()) return;

            // Lưu dữ liệu
            _nhaCungCap.TenNhaCungCap = hoTenTextBox.Text.Trim();
            _nhaCungCap.DiaChi = diaChiTextBox.Text.Trim();
            _nhaCungCap.SoDienThoai = soDienThoaiTextBox.Text.Trim();
            _nhaCungCap.Email = emailTextBox.Text.Trim();
            _nhaCungCap.TrangThai = (string)trangThaiComboBox.SelectedItem!;

            CloseWithResult(DialogResult.OK);
        }

        private bool ValidateAll()
        {
            var errors = new List<bool>();

            errors.Add(ValidateHoTen());
            errors.Add(ValidateSoDienThoai());
            errors.Add(ValidateDiaChi());
            errors.Add(ValidateEmail());
            errors.Add(ValidateTrangThai());

            return errors.All(x => x);
        }

        private bool ValidateHoTen()
        {
            string ten = hoTenTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(ten))
                return ShowError(hoTenErrorLabel, hoTenErrorIcon, "Tên nhà cung cấp không được để trống");

            if (ten.Length is < 5 or > 50)
                return ShowError(hoTenErrorLabel, hoTenErrorIcon, "Tên phải từ 5-50 ký tự");

            int? excludeId = _isEdit ? _nhaCungCap.MaNhaCungCap : null;
            if (_bus.IsTenNhaCungCapExists(ten, excludeId))
                return ShowError(hoTenErrorLabel, hoTenErrorIcon, "Tên nhà cung cấp đã tồn tại");

            return true;
        }

        private bool ValidateSoDienThoai()
        {
            string input = soDienThoaiTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input))
                return ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Số điện thoại không được để trống");

            string normalized = Regex.Replace(input, @"[\s\-\(\)\+\.]", "");
            if (!Regex.IsMatch(normalized, @"^0[235789]\d{8,9}$"))
                return ShowError(soDienThoaiErrorLabel, soDienThoaiErrorIcon, "Số điện thoại không hợp lệ (VD: 0901234567)");

            return true;
        }

        private bool ValidateEmail()
        {
            string email = emailTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
                return ShowError(emailErrorLabel, emailErrorIcon, "Email không được để trống");

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                return ShowError(emailErrorLabel, emailErrorIcon, "Email không đúng định dạng");

            return true;
        }

        private bool ValidateDiaChi()
        {
            string dc = diaChiTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(dc))
                return ShowError(diaChiErrorLabel, diaChiErrorIcon, "Địa chỉ không được để trống");

            if (dc.Length > 70)
                return ShowError(diaChiErrorLabel, diaChiErrorIcon, "Địa chỉ không được quá 70 ký tự");

            return true;
        }

        private bool ValidateTrangThai()
        {
            if (trangThaiComboBox.SelectedItem is not string status || string.IsNullOrWhiteSpace(status))
                return ShowError(trangThaiErrorLabel, trangThaiErrorIcon, "Vui lòng chọn trạng thái");

            return true;
        }

        // Helper gọn nhất
        private bool ShowError(Label lbl, Control icon, string msg)
        {
            lbl.Text = msg;
            lbl.Visible = icon.Visible = true;
            lbl.ForeColor = System.Drawing.Color.Red;
            return false;
        }

        private void ClearError(Label lbl, Control icon)
        {
            lbl.Visible = icon.Visible = false;
            lbl.Text = string.Empty;
        }

        private void ClearAllErrors()
        {
            ClearError(hoTenErrorLabel, hoTenErrorIcon);
            ClearError(soDienThoaiErrorLabel, soDienThoaiErrorIcon);
            ClearError(diaChiErrorLabel, diaChiErrorIcon);
            ClearError(emailErrorLabel, emailErrorIcon);
            ClearError(trangThaiErrorLabel, trangThaiErrorIcon);
        }

        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private static NhaCungCapDTO Clone(NhaCungCapDTO src) => new()
        {
            MaNhaCungCap = src.MaNhaCungCap,
            TenNhaCungCap = src.TenNhaCungCap,
            DiaChi = src.DiaChi,
            Email = src.Email,
            SoDienThoai = src.SoDienThoai,
            TrangThai = src.TrangThai
        };
    }
}