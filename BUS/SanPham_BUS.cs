using System;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class SanPham_BUS
    {
        public const string StatusConHang = "Còn hàng";
        public const string StatusHetHang = "Hết hàng";

        private static readonly string[] DefaultStatuses =
        {
            StatusConHang,
            StatusHetHang
        };

        private readonly SanPham_DAO _sanPhamDao = new();

        public IReadOnlyList<string> GetAvailableStatuses()
        {
            var statuses = new List<string>(DefaultStatuses);
            var existing = new HashSet<string>(statuses, StringComparer.CurrentCultureIgnoreCase);

            foreach (var status in _sanPhamDao.GetDistinctTrangThai())
            {
                if (!string.IsNullOrWhiteSpace(status) && existing.Add(status))
                {
                    statuses.Add(status);
                }
            }

            return statuses;
        }

        public IList<SanPhamDTO> GetSanPham(string? trangThaiFilter = null)
        {
            return _sanPhamDao.GetSanPham(trangThaiFilter);
        }
    }
}