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

        private static readonly HashSet<string> StatusLookup = new(DefaultStatuses, StringComparer.OrdinalIgnoreCase);

        private readonly NhaCungCap_DAO _nhaCungCapDao = new();

        public IReadOnlyList<string> GetAvailableStatuses()
        {
            var statuses = new List<string>(DefaultStatuses);
            return statuses;
        }

        public IList<NhaCungCapDTO> GetAll()
        {
            return _nhaCungCapDao.GetNhaCungCap();
        }
        public IReadOnlyList<string> GetDefaultStatuses() => DefaultStatuses;

        public int GetNextMaNhaCungCap()
        {
            int maxId = _nhaCungCapDao.GetMaxMaNhaCungCap();
            return maxId + 1;
        }

        public IList<NhaCungCapDTO> GetNhaCungCap(string? trangThaiFilter = null)
        {
            return _nhaCungCapDao.GetNhaCungCap(trangThaiFilter);
        }

        public bool IsValidStatus(string? status)
        {
            return !string.IsNullOrWhiteSpace(status) && StatusLookup.Contains(status);
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
                throw new ArgumentException("Tên nhà cung cấp không được để trống.", nameof(nhaCungCap.TenNhaCungCap));
            }

            nhaCungCap.DiaChi = nhaCungCap.DiaChi?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nhaCungCap.DiaChi))
            {
                throw new ArgumentException("Địa chỉ không được để trống.", nameof(nhaCungCap.DiaChi));
            }

            nhaCungCap.SoDienThoai = nhaCungCap.SoDienThoai?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nhaCungCap.SoDienThoai))
            {
                throw new ArgumentException("Số điện thoại không được để trống.", nameof(nhaCungCap.SoDienThoai));
            }

            if (nhaCungCap.SoDienThoai.Length != 10 || !nhaCungCap.SoDienThoai.All(char.IsDigit))
            {
                throw new ArgumentException("Số điện thoại phải có 10 số.", nameof(nhaCungCap.SoDienThoai));
            }

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
