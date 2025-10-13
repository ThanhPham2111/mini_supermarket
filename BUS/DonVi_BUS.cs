using System;
using System.Collections.Generic;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class DonVi_BUS
    {
        private readonly DonVi_DAO _dao = new();

        public IList<DonViDTO> GetDonViList()
        {
            return _dao.GetAll();
        }

        public IList<DonViDTO> Search(string? keyword)
        {
            var all = _dao.GetAll();
            keyword = keyword?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(keyword))
            {
                return all;
            }
            int.TryParse(keyword, out var id);
            var result = new List<DonViDTO>();
            foreach (var item in all)
            {
                if ((id > 0 && item.MaDonVi == id) ||
                    (!string.IsNullOrEmpty(item.TenDonVi) && item.TenDonVi.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}


