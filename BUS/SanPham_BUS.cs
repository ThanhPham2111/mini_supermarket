using System;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.Common;
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

        private static readonly HashSet<string> StatusLookup = new(DefaultStatuses, StringComparer.OrdinalIgnoreCase);

        private readonly SanPham_DAO _sanPhamDao = new();
        private readonly DonVi_DAO _donViDao = new();
        private readonly Loai_DAO _loaiDao = new();
        private readonly ThuongHieu_DAO _thuongHieuDao = new();

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

        public IList<SanPhamDTO> GetAll()
        {
            return _sanPhamDao.GetSanPham();
        }

        public IList<SanPhamDTO> GetSanPham(string? trangThaiFilter = null)
        {
            return _sanPhamDao.GetSanPham(trangThaiFilter);
        }

        public IList<DonViDTO> GetDonViList(string? trangThai = TrangThaiConstants.HoatDong)
        {
            return _donViDao.GetAll(trangThai);
        }

        public IList<LoaiDTO> GetLoaiList(string? trangThai = TrangThaiConstants.HoatDong)
        {
            return _loaiDao.GetAll(trangThai);
        }

        public IList<ThuongHieuDTO> GetThuongHieuList(string? trangThai = TrangThaiConstants.HoatDong)
        {
            return _thuongHieuDao.GetAll(trangThai);
        }

        public SanPhamDTO AddSanPham(SanPhamDTO sanPham)
        {
            if (sanPham == null)
            {
                throw new ArgumentNullException(nameof(sanPham));
            }

            ValidateSanPham(sanPham);
            int newId = _sanPhamDao.InsertSanPham(sanPham);
            sanPham.MaSanPham = newId;
            return sanPham;
        }

        public void UpdateSanPham(SanPhamDTO sanPham)
        {
            if (sanPham == null)
            {
                throw new ArgumentNullException(nameof(sanPham));
            }

            if (sanPham.MaSanPham <= 0)
            {
                throw new ArgumentException("Mã sản phẩm không hợp lệ.", nameof(sanPham.MaSanPham));
            }

            ValidateSanPham(sanPham);
            _sanPhamDao.UpdateSanPham(sanPham);
        }

        private static void ValidateSanPham(SanPhamDTO sanPham)
        {
            sanPham.TenSanPham = sanPham.TenSanPham?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(sanPham.TenSanPham))
            {
                throw new ArgumentException("Tên sản phẩm không được để trống.", nameof(sanPham.TenSanPham));
            }

            if (sanPham.MaDonVi <= 0)
            {
                throw new ArgumentException("Vui lòng chọn đơn vị hợp lệ.", nameof(sanPham.MaDonVi));
            }

            if (sanPham.MaLoai <= 0)
            {
                throw new ArgumentException("Vui lòng chọn loại hợp lệ.", nameof(sanPham.MaLoai));
            }

            if (sanPham.MaThuongHieu <= 0)
            {
                throw new ArgumentException("Vui lòng chọn thương hiệu hợp lệ.", nameof(sanPham.MaThuongHieu));
            }

            if (sanPham.GiaBan.HasValue && sanPham.GiaBan.Value < 0)
            {
                throw new ArgumentException("Giá bán không được âm.", nameof(sanPham.GiaBan));
            }

            sanPham.MoTa = string.IsNullOrWhiteSpace(sanPham.MoTa) ? null : sanPham.MoTa.Trim();
            sanPham.XuatXu = string.IsNullOrWhiteSpace(sanPham.XuatXu) ? null : sanPham.XuatXu.Trim();

            sanPham.TrangThai = string.IsNullOrWhiteSpace(sanPham.TrangThai)
                ? StatusConHang
                : sanPham.TrangThai.Trim();

            if (!StatusLookup.Contains(sanPham.TrangThai))
            {
                throw new ArgumentException("Trạng thái không hợp lệ.", nameof(sanPham.TrangThai));
            }
        }
    }
}
