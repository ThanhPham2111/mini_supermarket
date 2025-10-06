using System;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class NhaCungCap_BUS
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
        public int GetNextMaNhaCungCap()
        {
            int maxId = _nhaCungCapDao.GetMaxMaNhaCungCap();
            return maxId + 1;
        }


        private readonly NhaCungCap_DAO _nhaCungCapDao = new();

        public IReadOnlyList<string> GetDefaultStatuses() => DefaultStatuses;

        public IReadOnlyList<string> GetDefaultRoles() => DefaultRoles;

        public IList<NhaCungCapDTO> GetNhaCungCap(string? trangThaiFilter = null)
        {
            return _nhaCungCapDao.GetNhaCungCap(trangThaiFilter);
        }

        public NhaCungCapDTO AddNhaCungCap(NhaCungCapDTO nhaCungCap)
        {
            if (nhaCungCap == null)
            {
                throw new ArgumentNullException(nameof(nhaCungCap));
            }

            ValidateNhaCungCap(nhaCungCap, isUpdate: false);

            int newId = _nhaCungCapDao.InsertNhaCungCap(nhaCungCap);
            nhaCungCap.MaNhaCungCap = newId;
            return nhaCungCap;
        }

        public void UpdateNhaCungCap(NhaCungCapDTO nhaCungCap)
        {
            if (nhaCungCap == null)
            {
                throw new ArgumentNullException(nameof(nhaCungCap));
            }

            ValidateNhaCungCap(nhaCungCap, isUpdate: true);
            _nhaCungCapDao.UpdateNhaCungCap(nhaCungCap);
        }

        public void DeleteNhaCungCap(int maNhaCungCap)
        {
            if (maNhaCungCap <= 0)
            {
                throw new ArgumentException("Mã nhà cung cấp không hợp lệ.", nameof(maNhaCungCap));
            }

            _nhaCungCapDao.DeleteNhaCungCap(maNhaCungCap);
        }

        private static void ValidateNhaCungCap(NhaCungCapDTO nhaCungCap, bool isUpdate)
        {
            if (isUpdate && nhaCungCap.MaNhaCungCap <= 0)
            {
                throw new ArgumentException("Mã nhà cung cấp không hợp lệ.", nameof(nhaCungCap.MaNhaCungCap));
            }

            nhaCungCap.TenNhaCungCap = nhaCungCap.TenNhaCungCap?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nhaCungCap.TenNhaCungCap))
            {
                throw new ArgumentException("Tên khách hàng không được để trống.", nameof(nhaCungCap.TenNhaCungCap));
            }

            // if (nhaCungCap.NgaySinh.HasValue && nhaCungCap.NgaySinh.Value.Date > DateTime.Today)
            // {
            //     throw new ArgumentException("Ngày sinh không hợp lệ.", nameof(nhaCungCap.NgaySinh));
            // }

            if (!string.IsNullOrWhiteSpace(nhaCungCap.SoDienThoai))
            {
                nhaCungCap.SoDienThoai = nhaCungCap.SoDienThoai.Trim();
                if (nhaCungCap.SoDienThoai.Length > 15)
                {
                    throw new ArgumentException("Số điện thoại không được dài hơn 15 ký tự.", nameof(nhaCungCap.SoDienThoai));
                }
            }

            // if (!string.IsNullOrWhiteSpace(nhaCungCap.VaiTro) && !DefaultRoles.Contains(nhaCungCap.VaiTro))
            // {
            //     throw new ArgumentException("Chức vụ không hợp lệ.", nameof(nhaCungCap.VaiTro));
            // }

            if (string.IsNullOrWhiteSpace(nhaCungCap.TrangThai))
            {
                nhaCungCap.TrangThai = StatusActive;
            }
            else if (!DefaultStatuses.Contains(nhaCungCap.TrangThai))
            {
                throw new ArgumentException("Trạng thái không hợp lệ.", nameof(nhaCungCap.TrangThai));
            }
        }
    }
}