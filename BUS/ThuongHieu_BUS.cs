using System;
using System.Collections.Generic;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class ThuongHieu_BUS
    {
        private readonly ThuongHieu_DAO _dao = new();

        public IList<ThuongHieuDTO> GetThuongHieuList()
        {
            return _dao.GetAll();
        }

        public IList<ThuongHieuDTO> Search(string? keyword)
        {
            var all = _dao.GetAll();
            keyword = keyword?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(keyword))
            {
                return all;
            }
            int.TryParse(keyword, out var id);
            var result = new List<ThuongHieuDTO>();
            foreach (var item in all)
            {
                if ((id > 0 && item.MaThuongHieu == id) ||
                    (!string.IsNullOrEmpty(item.TenThuongHieu) && item.TenThuongHieu.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}


