using System;

namespace mini_supermarket.DTO
{
    public class SanPhamKhoDTO
    {
        private int _maSanPham;
        private string _tenSanPham = string.Empty;
        private int? _soLuong;
        private decimal? _giaBan;
        private string? _tenDonVi;
        private string? _tenLoai;

        public int MaSanPham
        {
            get => _maSanPham;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Mã sản phẩm phải lớn 0.", nameof(MaSanPham));
                _maSanPham = value;
            }
        }

        public string TenSanPham
        {
            get => _tenSanPham;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên sản phẩm không được trùng.", nameof(TenSanPham));
                _tenSanPham = value;
            }
        }

        public int? SoLuong
        {
            get => _soLuong;
            set
            {
                if (value.HasValue && value < 0)
                    throw new ArgumentException("Số lượng không được âm.", nameof(SoLuong));
                _soLuong = value;
            }
        }

        public decimal? GiaBan
        {
            get => _giaBan;
            set
            {
                if (value.HasValue && value < 0)
                    throw new ArgumentException("Giá bán không được âm.", nameof(GiaBan));
                _giaBan = value;
            }
        }

        public string? TenDonVi
        {
            get => _tenDonVi;
            set => _tenDonVi = value;
        }

        public string? TenLoai
        {
            get => _tenLoai;
            set => _tenLoai = value;
        }
    }
}