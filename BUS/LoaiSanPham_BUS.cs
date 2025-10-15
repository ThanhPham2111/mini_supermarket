using System;
using System.Collections.Generic;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class LoaiSanPham_BUS
    {
        private readonly Loai_DAO _loaiDao = new();

        public IList<LoaiDTO> GetLoaiList()
        {
            return _loaiDao.GetAll();
        }

        public LoaiDTO AddLoai(string tenLoai, string? moTa)
        {
            if (string.IsNullOrWhiteSpace(tenLoai))
            {
                throw new ArgumentException("Tên loại không hợp lệ.", nameof(tenLoai));
            }

            return _loaiDao.Create(tenLoai.Trim(), string.IsNullOrWhiteSpace(moTa) ? null : moTa.Trim());
        }

        public int GetNextMaLoai()
        {
            return _loaiDao.GetNextIdentityValue();
        }

        public IList<LoaiDTO> SearchLoai(string? keyword)
        {
            var all = _loaiDao.GetAll();
            keyword = keyword?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(keyword))
            {
                return all;
            }

            int.TryParse(keyword, out var id);
            var filtered = new List<LoaiDTO>();
            foreach (var item in all)
            {
                if ((id > 0 && item.MaLoai == id) ||
                    (!string.IsNullOrEmpty(item.TenLoai) && item.TenLoai.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0))
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
    }
}


