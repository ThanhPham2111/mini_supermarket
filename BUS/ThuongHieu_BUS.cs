using System;
using System.Collections.Generic;
using mini_supermarket.Common;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class ThuongHieu_BUS
    {
        private readonly ThuongHieu_DAO _dao = new();

        public IList<ThuongHieuDTO> GetThuongHieuList(string? trangThai = null)
        {
            return _dao.GetAll(trangThai);
        }

        public ThuongHieuDTO AddThuongHieu(string tenThuongHieu, string? trangThai = null)
        {
            if (string.IsNullOrWhiteSpace(tenThuongHieu))
            {
                throw new ArgumentException("Tên thương hiệu không hợp lệ.", nameof(tenThuongHieu));
            }

            string resolvedTrangThai = string.IsNullOrWhiteSpace(trangThai)
                ? TrangThaiConstants.HoatDong
                : trangThai.Trim();

            return _dao.Create(tenThuongHieu.Trim(), resolvedTrangThai);
        }

        public ThuongHieuDTO UpdateThuongHieu(int maThuongHieu, string tenThuongHieu, string? trangThai = null)
        {
            if (maThuongHieu <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maThuongHieu), "Mã thương hiệu không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(tenThuongHieu))
            {
                throw new ArgumentException("Tên thương hiệu không hợp lệ.", nameof(tenThuongHieu));
            }

            string resolvedTrangThai = string.IsNullOrWhiteSpace(trangThai)
                ? TrangThaiConstants.HoatDong
                : trangThai.Trim();

            return _dao.Update(maThuongHieu, tenThuongHieu.Trim(), resolvedTrangThai);
        }

        public void DeleteThuongHieu(int maThuongHieu)
        {
            if (maThuongHieu <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maThuongHieu), "Mã thương hiệu không hợp lệ.");
            }

            bool deleted = _dao.Delete(maThuongHieu);
            if (!deleted)
            {
                throw new InvalidOperationException("Không tìm thấy thương hiệu cần cập nhật trạng thái.");
            }
        }

        public int GetNextMaThuongHieu()
        {
            return _dao.GetNextIdentityValue();
        }

        public IList<ThuongHieuDTO> Search(string? keyword, string? trangThai = null)
        {
            var all = _dao.GetAll(trangThai);
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
