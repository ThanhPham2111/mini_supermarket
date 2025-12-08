using System;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class TaiKhoan_BUS
    {
        public const string StatusActive = "Hoạt động";
        public const string StatusInactive = "Khóa";

        private static readonly string[] DefaultStatuses =
        {
            StatusActive,
            StatusInactive
        };

        private readonly TaiKhoan_DAO _taiKhoanDao = new();

        public IReadOnlyList<string> GetDefaultStatuses() => DefaultStatuses;

        public IList<TaiKhoanDTO> GetAll()
        {
            return _taiKhoanDao.GetTaiKhoan();
        }

        public IList<TaiKhoanDTO> GetTaiKhoan(string? trangThaiFilter = null)
        {
            return _taiKhoanDao.GetTaiKhoan(trangThaiFilter);
        }

        public IList<PhanQuyenDTO> GetAllPhanQuyen()
        {
            return _taiKhoanDao.GetAllPhanQuyen();
        }

        public TaiKhoanDTO? Authenticate(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không được để trống.", nameof(tenDangNhap));
            }

            if (string.IsNullOrWhiteSpace(matKhau))
            {
                throw new ArgumentException("Mật khẩu không được để trống.", nameof(matKhau));
            }

            return _taiKhoanDao.Authenticate(tenDangNhap.Trim(), matKhau.Trim());
        }

        public bool IsPhoneMatchedWithUsername(string tenDangNhap, string soDienThoai)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(soDienThoai))
                return false;

            var tk = _taiKhoanDao.GetByUsernameAndPhone(tenDangNhap.Trim(), soDienThoai.Trim());
            return tk != null;
        }

        public bool UpdatePasswordWithPhone(string tenDangNhap, string soDienThoai, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(soDienThoai))
                return false;

            ValidateNewPassword(newPassword);

            var taiKhoan = _taiKhoanDao.GetByUsernameAndPhone(tenDangNhap.Trim(), soDienThoai.Trim());
            if (taiKhoan == null)
                return false;

            _taiKhoanDao.UpdatePassword(taiKhoan.MaTaiKhoan, newPassword.Trim());
            return true;
        }

        public int GetNextMaTaiKhoan()
        {
            int maxId = _taiKhoanDao.GetMaxMaTaiKhoan();
            return maxId + 1;
        }

        public TaiKhoanDTO AddTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            if (taiKhoan == null)
            {
                throw new ArgumentNullException(nameof(taiKhoan));
            }

            ValidateTaiKhoan(taiKhoan, isUpdate: false);

            if (_taiKhoanDao.IsTenDangNhapExists(taiKhoan.TenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập đã tồn tại.", nameof(taiKhoan.TenDangNhap));
            }

            int newId = _taiKhoanDao.InsertTaiKhoan(taiKhoan);
            taiKhoan.MaTaiKhoan = newId;
            return taiKhoan;
        }

        public void UpdateTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            if (taiKhoan == null)
            {
                throw new ArgumentNullException(nameof(taiKhoan));
            }

            ValidateTaiKhoan(taiKhoan, isUpdate: true);

            if (_taiKhoanDao.IsTenDangNhapExists(taiKhoan.TenDangNhap, taiKhoan.MaTaiKhoan))
            {
                throw new ArgumentException("Tên đăng nhập đã tồn tại.", nameof(taiKhoan.TenDangNhap));
            }

            _taiKhoanDao.UpdateTaiKhoan(taiKhoan);
        }

        public void DeleteTaiKhoan(int maTaiKhoan)
        {
            if (maTaiKhoan <= 0)
            {
                throw new ArgumentException("Mã tài khoản không hợp lệ.", nameof(maTaiKhoan));
            }

            _taiKhoanDao.DeleteTaiKhoan(maTaiKhoan);
        }

        public bool ResetPasswordToDefault(string tenDangNhap, string soDienThoai, string defaultPassword = "123456")
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(soDienThoai))
                return false;

            var taiKhoan = _taiKhoanDao.GetByUsernameAndPhone(tenDangNhap.Trim(), soDienThoai.Trim());
            if (taiKhoan == null)
                return false;

            _taiKhoanDao.UpdatePassword(taiKhoan.MaTaiKhoan, defaultPassword);
            return true;
        }

        private static void ValidateNewPassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Mật khẩu không được để trống.", nameof(newPassword));
            }

            if (newPassword.Trim().Length < 6)
            {
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự.", nameof(newPassword));
            }
        }

        private static void ValidateTaiKhoan(TaiKhoanDTO taiKhoan, bool isUpdate)
        {
            if (isUpdate && taiKhoan.MaTaiKhoan <= 0)
            {
                throw new ArgumentException("Mã tài khoản không hợp lệ.", nameof(taiKhoan.MaTaiKhoan));
            }

            taiKhoan.TenDangNhap = taiKhoan.TenDangNhap?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không được để trống.", nameof(taiKhoan.TenDangNhap));
            }

            if (taiKhoan.TenDangNhap.Length < 3)
            {
                throw new ArgumentException("Tên đăng nhập phải có ít nhất 3 ký tự.", nameof(taiKhoan.TenDangNhap));
            }

            taiKhoan.MatKhau = taiKhoan.MatKhau?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(taiKhoan.MatKhau))
            {
                throw new ArgumentException("Mật khẩu không được để trống.", nameof(taiKhoan.MatKhau));
            }

            if (taiKhoan.MatKhau.Length < 6)
            {
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự.", nameof(taiKhoan.MatKhau));
            }

            if (taiKhoan.MaNhanVien <= 0)
            {
                throw new ArgumentException("Mã nhân viên không hợp lệ.", nameof(taiKhoan.MaNhanVien));
            }

            if (taiKhoan.MaQuyen <= 0)
            {
                throw new ArgumentException("Mã quyền không hợp lệ.", nameof(taiKhoan.MaQuyen));
            }

            if (string.IsNullOrWhiteSpace(taiKhoan.TrangThai))
            {
                taiKhoan.TrangThai = StatusActive;
            }
            else if (!DefaultStatuses.Contains(taiKhoan.TrangThai))
            {
                throw new ArgumentException("Trạng thái không hợp lệ.", nameof(taiKhoan.TrangThai));
            }
        }
    }
}

