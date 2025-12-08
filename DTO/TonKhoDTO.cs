using System;

namespace mini_supermarket.DTO
{
    public class TonKhoDTO
    {
        private int _maSanPham;
        private string _tenSanPham = string.Empty;
        private string? _tenDonVi;
        private string? _tenLoai;
        private string? _tenThuongHieu;
        private int? _soLuong;
        private string? _trangThai;
        private int? _maLoai;
        private int? _maThuongHieu;
        private decimal? _giaBan;
        private DateTime? _hsd;
        private decimal? _giaNhap;

        public int MaSanPham
        {
            get => _maSanPham;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Mã sản phârm phải lớn hơn 0.", nameof(MaSanPham));
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

        public string? TenThuongHieu
        {
            get => _tenThuongHieu;
            set => _tenThuongHieu = value;
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

        public string? TrangThai
        {
            get => _trangThai;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Trạng thái không được trùng.", nameof(TrangThai));
                _trangThai = value;
            }
        }

        public int? MaLoai
        {
            get => _maLoai;
            set
            {
                if (value.HasValue && value <= 0)
                    throw new ArgumentException("Mã loại phải lớn 0.", nameof(MaLoai));
                _maLoai = value;
            }
        }

        public int? MaThuongHieu
        {
            get => _maThuongHieu;
            set
            {
                if (value.HasValue && value <= 0)
                    throw new ArgumentException("Mã thương hiệu phải lớn 0.", nameof(MaThuongHieu));
                _maThuongHieu = value;
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

        public DateTime? Hsd
        {
            get => _hsd;
            set => _hsd = value;
        }

        public decimal? GiaNhap
        {
            get => _giaNhap;
            set
            {
                if (value.HasValue && value < 0)
                    throw new ArgumentException("Giá nhập không được âm.", nameof(GiaNhap));
                _giaNhap = value;
            }
        }
    }
}