using System;
using System.Collections.Generic;
using mini_supermarket.Common;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class LoaiSanPham_BUS
    {
        private readonly Loai_DAO _loaiDao = new();

        public IList<LoaiDTO> GetLoaiList(string? trangThai = null)
        {
            return _loaiDao.GetAll(trangThai);
        }

        public LoaiDTO AddLoai(string tenLoai, string? moTa)
        {
            if (string.IsNullOrWhiteSpace(tenLoai))
            {
                throw new ArgumentException("Tên loại không hợp lệ.", nameof(tenLoai));
            }

            return _loaiDao.Create(tenLoai.Trim(), string.IsNullOrWhiteSpace(moTa) ? null : moTa.Trim());
        }

        public LoaiDTO UpdateLoai(int maLoai, string tenLoai, string? moTa)
        {
            if (maLoai <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maLoai), "Mã loại không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(tenLoai))
            {
                throw new ArgumentException("Tên loại không hợp lệ.", nameof(tenLoai));
            }

            return _loaiDao.Update(maLoai, tenLoai.Trim(), string.IsNullOrWhiteSpace(moTa) ? null : moTa.Trim());
        }

        public void DeleteLoai(int maLoai)
        {
            if (maLoai <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maLoai), "Mã loại không hợp lệ.");
            }

            bool deleted = _loaiDao.Delete(maLoai);
            if (!deleted)
            {
                throw new InvalidOperationException("Không tìm thấy loại cần cập nhật trạng thái.");
            }
        }

        public int GetNextMaLoai()
        {
            return _loaiDao.GetNextIdentityValue();
        }

        public IList<LoaiDTO> SearchLoai(string? keyword, string? trangThai = null)
        {
            var all = _loaiDao.GetAll(trangThai);
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
