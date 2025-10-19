using System;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class NhanVien_BUS
    {
        public const string StatusWorking = "Đang làm";
        public const string StatusInactive = "Đã nghỉ";

        private static readonly string[] DefaultStatuses =
        {
            StatusWorking,
            StatusInactive
        };

        private static readonly string[] DefaultRoles =
        {
            "Quản lý",
            "Nhân viên bán hàng",
            "Thu ngân"
        };
        public int GetNextMaNhanVien()
        {
            int maxId = _nhanVienDao.GetMaxMaNhanVien();
            return maxId + 1;
        }


        private readonly NhanVien_DAO _nhanVienDao = new();

        public IReadOnlyList<string> GetDefaultStatuses() => DefaultStatuses;

        public IReadOnlyList<string> GetDefaultRoles() => DefaultRoles;

        public IList<NhanVienDTO> GetAll()
        {
            return _nhanVienDao.GetNhanVien();
        }

        public IList<NhanVienDTO> GetNhanVien(string? trangThaiFilter = null)
        {
            return _nhanVienDao.GetNhanVien(trangThaiFilter);
        }

        public NhanVienDTO AddNhanVien(NhanVienDTO nhanVien)
        {
            if (nhanVien == null)
            {
                throw new ArgumentNullException(nameof(nhanVien));
            }

            ValidateNhanVien(nhanVien, isUpdate: false);

            int newId = _nhanVienDao.InsertNhanVien(nhanVien);
            nhanVien.MaNhanVien = newId;
            return nhanVien;
        }

        public void UpdateNhanVien(NhanVienDTO nhanVien)
        {
            if (nhanVien == null)
            {
                throw new ArgumentNullException(nameof(nhanVien));
            }

            ValidateNhanVien(nhanVien, isUpdate: true);
            _nhanVienDao.UpdateNhanVien(nhanVien);
        }

        public void DeleteNhanVien(int maNhanVien)
        {
            if (maNhanVien <= 0)
            {
                throw new ArgumentException("Mã nhân viên không hợp lệ.", nameof(maNhanVien));
            }

            _nhanVienDao.DeleteNhanVien(maNhanVien);
        }

        private static void ValidateNhanVien(NhanVienDTO nhanVien, bool isUpdate)
        {
            if (isUpdate && nhanVien.MaNhanVien <= 0)
            {
                throw new ArgumentException("Mã nhân viên không hợp lệ.", nameof(nhanVien.MaNhanVien));
            }

            nhanVien.TenNhanVien = nhanVien.TenNhanVien?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nhanVien.TenNhanVien))
            {
                throw new ArgumentException("Tên nhân viên không được để trống.", nameof(nhanVien.TenNhanVien));
            }

            if (nhanVien.NgaySinh.HasValue && nhanVien.NgaySinh.Value.Date > DateTime.Today)
            {
                throw new ArgumentException("Ngày sinh không hợp lệ.", nameof(nhanVien.NgaySinh));
            }

            nhanVien.SoDienThoai = nhanVien.SoDienThoai?.Trim();
            if (string.IsNullOrWhiteSpace(nhanVien.SoDienThoai))
            {
                throw new ArgumentException("Số điện thoại không được trống.", nameof(nhanVien.SoDienThoai));
            }

            if (nhanVien.SoDienThoai.Length != 10 || !nhanVien.SoDienThoai.All(char.IsDigit))
            {
                throw new ArgumentException("Số điện thoại phải có 10 số.", nameof(nhanVien.SoDienThoai));
            }

            nhanVien.VaiTro = nhanVien.VaiTro?.Trim();
            if (string.IsNullOrWhiteSpace(nhanVien.VaiTro))
            {
                throw new ArgumentException("Vai trò không được để trống.", nameof(nhanVien.VaiTro));
            }

            if (!DefaultRoles.Contains(nhanVien.VaiTro))
            {
                throw new ArgumentException("Chức vụ không hợp lệ", nameof(nhanVien.VaiTro));
            }

            if (string.IsNullOrWhiteSpace(nhanVien.TrangThai))
            {
                nhanVien.TrangThai = StatusWorking;
            }
            else if (!DefaultStatuses.Contains(nhanVien.TrangThai))
            {
                throw new ArgumentException("Trạng thái không hợp lệ.", nameof(nhanVien.TrangThai));
            }
        }
    }
}

