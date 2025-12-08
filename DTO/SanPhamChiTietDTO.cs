namespace mini_supermarket.DTO
{
    public class SanPhamChiTietDTO
    {
        private int _maSanPham;
        private string _tenSanPham = string.Empty;
        private decimal? _giaBan;
        private string? _hinhAnh;
        private int? _soLuong;
        private string _khuyenMai = string.Empty;
        private decimal _phanTramGiam;

        public SanPhamChiTietDTO()
        {
        }

        public SanPhamChiTietDTO(
            int maSanPham,
            string tenSanPham,
            decimal? giaBan,
            string? hinhAnh,
            int? soLuong,
            string khuyenMai,
            decimal phanTramGiam)
        {
            _maSanPham = maSanPham;
            _tenSanPham = tenSanPham;
            _giaBan = giaBan;
            _hinhAnh = hinhAnh;
            _soLuong = soLuong;
            _khuyenMai = khuyenMai;
            _phanTramGiam = phanTramGiam;
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

        public string? HinhAnh
        {
            get { return _hinhAnh; }
            set { _hinhAnh = value; }
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
    }
}
