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
            bool emptyAll =
                string.IsNullOrWhiteSpace(hoTenTextBox.Text) &&
                string.IsNullOrWhiteSpace(soDienThoaiTextBox.Text) &&
                string.IsNullOrWhiteSpace(diaChiTextBox.Text) &&
                string.IsNullOrWhiteSpace(emailTextBox.Text);

            // ✅ Nếu tất cả đều trống → popup cảnh báo
            if (emptyAll)
            {
                MessageBox.Show(this,
                    "Vui lòng nhập đầy đủ thông tin trước khi xác nhận.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hoTenTextBox.Focus();
                return;
            }

            // ✅ Nếu có nhập nhưng thiếu trường
            if (!ValidateAll())
                return;

            // ✅ Lưu dữ liệu
            _workingNhaCungCap.TenNhaCungCap = hoTenTextBox.Text.Trim();
            _workingNhaCungCap.SoDienThoai = soDienThoaiTextBox.Text.Trim();
            _workingNhaCungCap.DiaChi = diaChiTextBox.Text.Trim();
            _workingNhaCungCap.Email = emailTextBox.Text.Trim();
            _workingNhaCungCap.TrangThai = (string)trangThaiComboBox.SelectedItem!;

            DialogResult = DialogResult.OK;
            Close();
        }

        // ==========================
        // ✅ VALIDATE TỔNG
        // ==========================
        private bool ValidateAll()
        {
            bool okHoTen = ValidateHoTen();
            bool okPhone = ValidateSoDienThoai();
            bool okAddr = ValidateDiaChi();
            bool okEmail = ValidateEmail();

            return okHoTen && okPhone && okAddr && okEmail;
        }

        // ==========================
        // ✅ SET ERROR HELPER
        // ==========================
        private void SetError(TextBox tb, Label lbl, string? msg)
        {
            lbl.Text = msg ?? "";
            errorProvider1.SetError(tb, msg == null ? "" : "❌");
        }

        // ==========================
        // ✅ VALIDATION
        // ==========================
        private bool ValidateHoTen()
        {
            string t = hoTenTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                SetError(hoTenTextBox, hoTenErrorLabel, "Không được để trống");
                return false;
            }
            SetError(hoTenTextBox, hoTenErrorLabel, null);
            return true;
        }

        private bool ValidateSoDienThoai()
        {
            string t = soDienThoaiTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                SetError(soDienThoaiTextBox, soDienThoaiErrorLabel, "Không được để trống");
                return false;
            }
            if (t.Length != 10 || !t.All(char.IsDigit))
            {
                SetError(soDienThoaiTextBox, soDienThoaiErrorLabel, "Phải 10 chữ số");
                return false;
            }
            SetError(soDienThoaiTextBox, soDienThoaiErrorLabel, null);
            return true;
        }
        private bool ValidateEmail()
        {
            string v = emailTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(v))
            {
                SetError(emailTextBox, emailErrorLabel, "Không được để trống");
                return false;
            }

            if (!Regex.IsMatch(v, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                SetError(emailTextBox, emailErrorLabel, "Email không hợp lệ");
                return false;
            }

            SetError(emailTextBox, emailErrorLabel, null);
            return true;
        }



        private bool ValidateDiaChi()
        {
            string t = diaChiTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                SetError(diaChiTextBox, diaChiErrorLabel, "Không được để trống");
                return false;
            }
            SetError(diaChiTextBox, diaChiErrorLabel, null);
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
