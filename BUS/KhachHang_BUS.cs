using System;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class KhachHang_BUS
    {
        public const string StatusActive = "Hoạt động";
        public const string StatusInactive = "Khóa";

        private static readonly string[] DefaultStatuses =
        {
            StatusActive,
            StatusInactive
        };

        private static readonly string[] DefaultRoles =
        {
            "Quản lý",
            "Nhân viên bán hàng",
            "Thu ngân"
        };
        public int GetNextMaKhachHang()
        {
            int maxId = _khachHangDao.GetMaxMaKhachHang();
            return maxId + 1;
        }


        private readonly KhachHang_DAO _khachHangDao = new();

        public IReadOnlyList<string> GetDefaultStatuses() => DefaultStatuses;

        public IReadOnlyList<string> GetDefaultRoles() => DefaultRoles;

        public IList<KhachHangDTO> GetKhachHang(string? trangThaiFilter = null)
        {
            return _khachHangDao.GetKhachHang(trangThaiFilter);
        }

        public KhachHangDTO AddKhachHang(KhachHangDTO khachHang)
        {
            if (khachHang == null)
            {
                throw new ArgumentNullException(nameof(khachHang));
            }

            ValidateKhachHang(khachHang, isUpdate: false);

            int newId = _khachHangDao.InsertKhachHang(khachHang);
            khachHang.MaKhachHang = newId;
            return khachHang;
        }

        public void UpdateKhachHang(KhachHangDTO khachHang)
        {
            if (khachHang == null)
            {
                throw new ArgumentNullException(nameof(khachHang));
            }

            ValidateKhachHang(khachHang, isUpdate: true);
            _khachHangDao.UpdateKhachHang(khachHang);
        }

        public void DeleteKhachHang(int maKhachHang)
        {
            if (maKhachHang <= 0)
            {
                throw new ArgumentException("Mã khách hàng không hợp lệ.", nameof(maKhachHang));
            }

            _khachHangDao.DeleteKhachHang(maKhachHang);
        }

        private static void ValidateKhachHang(KhachHangDTO khachHang, bool isUpdate)
        {
            if (isUpdate && khachHang.MaKhachHang <= 0)
            {
                throw new ArgumentException("Mã khách hàng không hợp lệ.", nameof(khachHang.MaKhachHang));
            }

            khachHang.TenKhachHang = khachHang.TenKhachHang?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(khachHang.TenKhachHang))
            {
                throw new ArgumentException("Tên khách hàng không được để trống.", nameof(khachHang.TenKhachHang));
            }

            // if (khachHang.NgaySinh.HasValue && khachHang.NgaySinh.Value.Date > DateTime.Today)
            // {
            //     throw new ArgumentException("Ngày sinh không hợp lệ.", nameof(khachHang.NgaySinh));
            // }

            if (!string.IsNullOrWhiteSpace(khachHang.SoDienThoai))
            {
                khachHang.SoDienThoai = khachHang.SoDienThoai.Trim();
                if (khachHang.SoDienThoai.Length > 15)
                {
                    throw new ArgumentException("Số điện thoại không được dài hơn 15 ký tự.", nameof(khachHang.SoDienThoai));
                }
            }

            // if (!string.IsNullOrWhiteSpace(khachHang.VaiTro) && !DefaultRoles.Contains(khachHang.VaiTro))
            // {
            //     throw new ArgumentException("Chức vụ không hợp lệ.", nameof(khachHang.VaiTro));
            // }

            if (string.IsNullOrWhiteSpace(khachHang.TrangThai))
            {
                khachHang.TrangThai = StatusActive;
            }
            else if (!DefaultStatuses.Contains(khachHang.TrangThai))
            {
                throw new ArgumentException("Trạng thái không hợp lệ.", nameof(khachHang.TrangThai));
            }
        }

        public void UpdateDiemTichLuy(int maKhachHang, int diemMoi)
        {
            if (maKhachHang <= 0)
            {
                throw new ArgumentException("Mã khách hàng không hợp lệ.", nameof(maKhachHang));
            }

            if (diemMoi < 0)
            {
                throw new ArgumentException("Điểm tích lũy không được âm.", nameof(diemMoi));
            }

            _khachHangDao.UpdateDiemTichLuy(maKhachHang, diemMoi);
        }
    }
}