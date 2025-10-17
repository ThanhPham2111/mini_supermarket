using System;
using System.Collections.Generic;
using mini_supermarket.Common;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class DonVi_BUS
    {
        private readonly DonVi_DAO _dao = new();

        public IList<DonViDTO> GetDonViList(string? trangThai = null)
        {
            return _dao.GetAll(trangThai);
        }

        public DonViDTO AddDonVi(string tenDonVi, string? moTa, string? trangThai = null)
        {
            if (string.IsNullOrWhiteSpace(tenDonVi))
            {
                throw new ArgumentException("Tên đơn vị không hợp lệ.", nameof(tenDonVi));
            }

            string resolvedTrangThai = string.IsNullOrWhiteSpace(trangThai)
                ? TrangThaiConstants.HoatDong
                : trangThai.Trim();

            return _dao.Create(
                tenDonVi.Trim(),
                string.IsNullOrWhiteSpace(moTa) ? null : moTa.Trim(),
                resolvedTrangThai);
        }

        public DonViDTO UpdateDonVi(int maDonVi, string tenDonVi, string? moTa, string? trangThai = null)
        {
            if (maDonVi <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maDonVi), "Mã đơn vị không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(tenDonVi))
            {
                throw new ArgumentException("Tên đơn vị không hợp lệ.", nameof(tenDonVi));
            }

            string resolvedTrangThai = string.IsNullOrWhiteSpace(trangThai)
                ? TrangThaiConstants.HoatDong
                : trangThai.Trim();

            return _dao.Update(
                maDonVi,
                tenDonVi.Trim(),
                string.IsNullOrWhiteSpace(moTa) ? null : moTa.Trim(),
                resolvedTrangThai);
        }

        public void DeleteDonVi(int maDonVi)
        {
            if (maDonVi <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maDonVi), "Mã đơn vị không hợp lệ.");
            }

            bool deleted = _dao.Delete(maDonVi);
            if (!deleted)
            {
                throw new InvalidOperationException("Không tìm thấy đơn vị cần cập nhật trạng thái.");
            }
        }

        public int GetNextMaDonVi()
        {
            return _dao.GetNextIdentityValue();
        }

        public IList<DonViDTO> Search(string? keyword, string? trangThai = null)
        {
            var all = _dao.GetAll(trangThai);
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
