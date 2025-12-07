using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.TaiKhoan
{
    public partial class Form_TaiKhoanDialog : Form
    {
        private readonly TaiKhoan_BUS _taiKhoanBus;
        private readonly NhanVien_BUS _nhanVienBus;
        private readonly bool _isEdit;
        private readonly TaiKhoanDTO _workingTaiKhoan;
        private readonly IList<NhanVienDTO> _nhanVienList;
        private readonly IList<PhanQuyenDTO> _quyenList;

        public TaiKhoanDTO TaiKhoan => _workingTaiKhoan;

        public Form_TaiKhoanDialog(TaiKhoan_BUS taiKhoanBus, NhanVien_BUS nhanVienBus, TaiKhoanDTO? existingTaiKhoan = null)
        {
            if (taiKhoanBus == null)
            {
                throw new ArgumentNullException(nameof(taiKhoanBus));
            }

            if (nhanVienBus == null)
            {
                throw new ArgumentNullException(nameof(nhanVienBus));
            }

            InitializeComponent();
            tenDangNhapTextBox.MaxLength = 50;
            matKhauTextBox.MaxLength = 50;

            _taiKhoanBus = taiKhoanBus;
            _nhanVienBus = nhanVienBus;
            _nhanVienList = _nhanVienBus.GetAll().Where(nv => nv.TrangThai == NhanVien_BUS.StatusWorking).ToList();
            _quyenList = _taiKhoanBus.GetAllPhanQuyen().ToList();

            _isEdit = existingTaiKhoan != null;
            _workingTaiKhoan = existingTaiKhoan != null ? CloneTaiKhoan(existingTaiKhoan) : new TaiKhoanDTO();

            Load += Form_TaiKhoanDialog_Load;
            okButton.Click += okButton_Click;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
        }

        private void tenDangNhapTextBox_TextChanged(object? sender, EventArgs e)
        {
            tenDangNhapErrorLabel.Text = string.Empty;
            tenDangNhapErrorIcon.Visible = false;
        }

        private void matKhauTextBox_TextChanged(object? sender, EventArgs e)
        {
            matKhauErrorLabel.Text = string.Empty;
            matKhauErrorIcon.Visible = false;
        }

        private void nhanVienComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            nhanVienErrorLabel.Text = string.Empty;
            nhanVienErrorIcon.Visible = false;
        }

        private void quyenComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            quyenErrorLabel.Text = string.Empty;
            quyenErrorIcon.Visible = false;
        }

        private void trangThaiComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            trangThaiErrorLabel.Text = string.Empty;
            trangThaiErrorIcon.Visible = false;
        }

        private void Form_TaiKhoanDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            // Load nhân viên
            nhanVienComboBox.DisplayMember = "TenNhanVien";
            nhanVienComboBox.ValueMember = "MaNhanVien";
            nhanVienComboBox.DataSource = _nhanVienList;

            // Load quyền
            quyenComboBox.DisplayMember = "TenQuyen";
            quyenComboBox.ValueMember = "MaQuyen";
            quyenComboBox.DataSource = _quyenList;

            // Load trạng thái
            trangThaiComboBox.Items.Clear();
            foreach (var status in _taiKhoanBus.GetDefaultStatuses())
            {
                trangThaiComboBox.Items.Add(status);
            }

            if (_isEdit)
            {
                Text = "Sửa tài khoản";
                maTaiKhoanValueLabel.Text = _workingTaiKhoan.MaTaiKhoan.ToString();

                tenDangNhapTextBox.Text = _workingTaiKhoan.TenDangNhap ?? string.Empty;
                matKhauTextBox.Text = _workingTaiKhoan.MatKhau ?? string.Empty;

                if (_nhanVienList.Any(nv => nv.MaNhanVien == _workingTaiKhoan.MaNhanVien))
                {
                    nhanVienComboBox.SelectedValue = _workingTaiKhoan.MaNhanVien;
                }
                // Khóa ComboBox nhân viên khi sửa
                nhanVienComboBox.Enabled = false;

                if (_quyenList.Any(q => q.MaQuyen == _workingTaiKhoan.MaQuyen))
                {
                    quyenComboBox.SelectedValue = _workingTaiKhoan.MaQuyen;
                }

                trangThaiComboBox.SelectedItem = !string.IsNullOrEmpty(_workingTaiKhoan.TrangThai) && _taiKhoanBus.GetDefaultStatuses().Contains(_workingTaiKhoan.TrangThai)
                    ? _workingTaiKhoan.TrangThai
                    : (_taiKhoanBus.GetDefaultStatuses().Contains(TaiKhoan_BUS.StatusActive) ? TaiKhoan_BUS.StatusActive : _taiKhoanBus.GetDefaultStatuses().FirstOrDefault());
            }
            else
            {
                Text = "Thêm tài khoản";
                int nextId = _taiKhoanBus.GetNextMaTaiKhoan();
                maTaiKhoanValueLabel.Text = nextId.ToString();
                _workingTaiKhoan.MaTaiKhoan = nextId;
                _workingTaiKhoan.TrangThai = TaiKhoan_BUS.StatusActive;
                trangThaiComboBox.SelectedItem = TaiKhoan_BUS.StatusActive;
            }
        }

        private void ClearAllErrors()
        {
            tenDangNhapErrorLabel.Text = string.Empty;
            tenDangNhapErrorIcon.Visible = false;
            matKhauErrorLabel.Text = string.Empty;
            matKhauErrorIcon.Visible = false;
            nhanVienErrorLabel.Text = string.Empty;
            nhanVienErrorIcon.Visible = false;
            quyenErrorLabel.Text = string.Empty;
            quyenErrorIcon.Visible = false;
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

            string tenDangNhap = tenDangNhapTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                ShowError(tenDangNhapErrorLabel, tenDangNhapErrorIcon, "Vui lòng nhập tên đăng nhập.");
                tenDangNhapTextBox.Focus();
                hasError = true;
            }
            else if (tenDangNhap.Length < 3)
            {
                ShowError(tenDangNhapErrorLabel, tenDangNhapErrorIcon, "Tên đăng nhập phải có ít nhất 3 ký tự.");
                tenDangNhapTextBox.Focus();
                hasError = true;
            }
            else if (tenDangNhap.Length > 50) // Dù có MaxLength rồi nhưng vẫn nên kiểm tra
            {
                ShowError(tenDangNhapErrorLabel, tenDangNhapErrorIcon, "Tên đăng nhập không được quá 50 ký tự.");
                tenDangNhapTextBox.Focus();
                hasError = true;
            }
            // matkhau validation
            string matKhau = matKhauTextBox.Text;
            if (string.IsNullOrWhiteSpace(matKhau))
            {
                ShowError(matKhauErrorLabel, matKhauErrorIcon, "Mật khẩu không được để trống.");
                matKhauTextBox.Focus();
                hasError = true;
            }
            else if (matKhau.Length < 6)
            {
                ShowError(matKhauErrorLabel, matKhauErrorIcon, "Mật khẩu phải có ít nhất 6 ký tự.");
                matKhauTextBox.Focus();
                hasError = true;
            }
            else if (matKhau.Length > 50)
            {
                ShowError(matKhauErrorLabel, matKhauErrorIcon, "Mật khẩu không được quá 50 ký tự.");
                matKhauTextBox.Focus();
                hasError = true;
            }

            int maNhanVien = 0;
            if (nhanVienComboBox.SelectedValue is int nv && nv > 0)
            {
                maNhanVien = nv;
            }
            else
            {
                ShowError(nhanVienErrorLabel, nhanVienErrorIcon, "Vui lòng chọn nhân viên.");
                nhanVienComboBox.Focus();
                hasError = true;
            }

            int maQuyen = 0;
            if (quyenComboBox.SelectedValue is int q && q > 0)
            {
                maQuyen = q;
            }
            else
            {
                ShowError(quyenErrorLabel, quyenErrorIcon, "Vui lòng chọn quyền.");
                quyenComboBox.Focus();
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
                trangThaiComboBox.Focus();
                hasError = true;
            }

            if (hasError)
            {
                return;
            }

            _workingTaiKhoan.TenDangNhap = tenDangNhap;
            _workingTaiKhoan.MatKhau = matKhau;
            // Chỉ cập nhật MaNhanVien nếu không phải đang edit (vì ComboBox đã bị khóa)
            if (!_isEdit)
            {
                _workingTaiKhoan.MaNhanVien = maNhanVien;
            }
            _workingTaiKhoan.MaQuyen = maQuyen;
            _workingTaiKhoan.TrangThai = trangThai;

            DialogResult = DialogResult.OK;
            Close();
        }

        private static TaiKhoanDTO CloneTaiKhoan(TaiKhoanDTO source)
        {
            return new TaiKhoanDTO
            {
                MaTaiKhoan = source.MaTaiKhoan,
                TenDangNhap = source.TenDangNhap,
                MatKhau = source.MatKhau,
                MaNhanVien = source.MaNhanVien,
                MaQuyen = source.MaQuyen,
                TrangThai = source.TrangThai
            };
        }
    }
}

