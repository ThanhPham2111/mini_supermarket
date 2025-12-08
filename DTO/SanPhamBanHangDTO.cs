using System;

namespace mini_supermarket.DTO
{
    public class SanPhamBanHangDTO
    {
        private int _maSanPham;
        private string _tenSanPham = string.Empty;
        private decimal? _giaBan;
        private int? _soLuong;
        private string _khuyenMai = string.Empty;
        private decimal _phanTramGiam;
        private DateTime? _hsd;

        public SanPhamBanHangDTO()
        {
        }

        public SanPhamBanHangDTO(
            int maSanPham,
            string tenSanPham,
            decimal? giaBan,
            int? soLuong,
            string khuyenMai,
            decimal phanTramGiam,
            DateTime? hsd)
        {
            _maSanPham = maSanPham;
            _tenSanPham = tenSanPham;
            _giaBan = giaBan;
            _soLuong = soLuong;
            _khuyenMai = khuyenMai;
            _phanTramGiam = phanTramGiam;
            _hsd = hsd;
        }

        public int MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }

        public string TenSanPham
        {
            get { return _tenSanPham; }
            set { _tenSanPham = value; }
        }

        public decimal? GiaBan
        {
            get { return _giaBan; }
            set { _giaBan = value; }
        }

        public int? SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; }
        }

        public string KhuyenMai
        {
            get { return _khuyenMai; }
            set { _khuyenMai = value; }
        }

        public decimal PhanTramGiam
        {
            get { return _phanTramGiam; }
            set { _phanTramGiam = value; }
        }

        public DateTime? Hsd
        {
            get { return _hsd; }
            set { _hsd = value; }
        }
    }
}
