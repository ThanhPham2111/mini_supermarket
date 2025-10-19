using System;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class NhaCungCap_BUS
    {
        public const string StatusActive = "Đang hợp tác";
        public const string StatusInactive = "Ngừng hợp tác";

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

        public IList<NhaCungCapDTO> GetNhaCungCap(string? trangThaiFilter = null)
        {
            return _nhaCungCapDao.GetNhaCungCap(trangThaiFilter);
        }

        public bool IsValidStatus(string? status)
        {
            return !string.IsNullOrWhiteSpace(status) && StatusLookup.Contains(status);
        }
    }
}
