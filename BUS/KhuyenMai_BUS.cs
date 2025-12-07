using System;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class KhuyenMai_BUS
    {
        private readonly KhuyenMai_DAO _dao = new();

        public IList<KhuyenMaiDTO> GetKhuyenMai(int? maSanPhamFilter = null)
        {
            return _dao.GetKhuyenMai(maSanPhamFilter);
        }

        public int GetNextMaKhuyenMai()
        {
            int max = _dao.GetMaxMaKhuyenMai();
            return max + 1;
        }

        public KhuyenMaiDTO AddKhuyenMai(KhuyenMaiDTO kh)
        {
            if (kh == null) throw new ArgumentNullException(nameof(kh));

            Validate(kh, isUpdate: false);
            int newId = _dao.InsertKhuyenMai(kh);
            kh.MaKhuyenMai = newId;
            return kh;
        }

        public void UpdateKhuyenMai(KhuyenMaiDTO kh)
        {
            if (kh == null) throw new ArgumentNullException(nameof(kh));
            Validate(kh, isUpdate: true);
            _dao.UpdateKhuyenMai(kh);
        }

        public void DeleteKhuyenMai(int maKhuyenMai)
        {
            if (maKhuyenMai <= 0) throw new ArgumentException("Mã khuyến mãi không hợp lệ.", nameof(maKhuyenMai));
            _dao.DeleteKhuyenMai(maKhuyenMai);
        }

        private void Validate(KhuyenMaiDTO kh, bool isUpdate)
        {
            if (isUpdate && kh.MaKhuyenMai <= 0)
            {
                throw new ArgumentException("Mã khuyến mãi không hợp lệ.", nameof(kh.MaKhuyenMai));
            }

            if (kh.MaSanPham <= 0)
            {
                throw new ArgumentException("Mã sản phẩm phải > 0.", nameof(kh.MaSanPham));
            }

            // Ensure a product has at most one promotion
            int? excludeId = isUpdate ? kh.MaKhuyenMai : null;
            if (_dao.ExistsKhuyenMaiForProduct(kh.MaSanPham, excludeId))
            {
                throw new InvalidOperationException("Mỗi sản phẩm chỉ được áp một khuyến mãi.");
            }

            if (!string.IsNullOrWhiteSpace(kh.TenKhuyenMai))
            {
                kh.TenKhuyenMai = kh.TenKhuyenMai.Trim();
                if (kh.TenKhuyenMai.Length > 255)
                {
                    throw new ArgumentException("Tên khuyến mãi không được dài hơn 255 ký tự.", nameof(kh.TenKhuyenMai));
                }
            }

            if (kh.PhanTramGiamGia.HasValue)
            {
                if (kh.PhanTramGiamGia < 0 || kh.PhanTramGiamGia > 100)
                {
                    throw new ArgumentException("Phần trăm giảm giá phải trong khoảng 0 - 100.", nameof(kh.PhanTramGiamGia));
                }
            }

            if (kh.NgayBatDau.HasValue && kh.NgayKetThuc.HasValue)
            {
                if (kh.NgayBatDau > kh.NgayKetThuc)
                {
                    throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", nameof(kh.NgayBatDau));
                }
            }

            if (!string.IsNullOrWhiteSpace(kh.MoTa))
            {
                kh.MoTa = kh.MoTa.Trim();
            }
        }
    }
}
