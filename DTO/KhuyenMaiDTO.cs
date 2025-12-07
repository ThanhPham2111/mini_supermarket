using System;

namespace mini_supermarket.DTO
{
    public class KhuyenMaiDTO
    {
        private int _maKhuyenMai;
        private int _maSanPham;
        private string? _tenKhuyenMai;
        private decimal? _phanTramGiamGia;
        private DateTime? _ngayBatDau;
        private DateTime? _ngayKetThuc;
        private string? _moTa;

        public int MaKhuyenMai
        {
            get { return _maKhuyenMai; }
            set { _maKhuyenMai = value; }
        }

        public int MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }

        public string? TenKhuyenMai
        {
            get { return _tenKhuyenMai; }
            set { _tenKhuyenMai = value; }
        }

        public decimal? PhanTramGiamGia
        {
            get { return _phanTramGiamGia; }
            set { _phanTramGiamGia = value; }
        }

        public DateTime? NgayBatDau
        {
            get { return _ngayBatDau; }
            set { _ngayBatDau = value; }
        }

        public DateTime? NgayKetThuc
        {
            get { return _ngayKetThuc; }
            set { _ngayKetThuc = value; }
        }

        public string? MoTa
        {
            get { return _moTa; }
            set { _moTa = value; }
        }

        public KhuyenMaiDTO()
        {
        }

        public KhuyenMaiDTO(
            int maKhuyenMai,
            int maSanPham,
            string? tenKhuyenMai,
            decimal? phanTramGiamGia,
            DateTime? ngayBatDau,
            DateTime? ngayKetThuc,
            string? moTa)
        {
            _maKhuyenMai = maKhuyenMai;
            _maSanPham = maSanPham;
            _tenKhuyenMai = tenKhuyenMai;
            _phanTramGiamGia = phanTramGiamGia;
            _ngayBatDau = ngayBatDau;
            _ngayKetThuc = ngayKetThuc;
            _moTa = moTa;
        }
    }
}
