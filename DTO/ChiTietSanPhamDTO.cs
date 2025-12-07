namespace mini_supermarket.DTO
{
    public class ChiTietSanPhamDTO
    {
        private int _maChiTietSanPham;
        private int _maSanPham;
        private string? _thuocTinh;
        private decimal? _giaNhap;
        private string? _trangThai;

        public ChiTietSanPhamDTO()
        {
        }

        public ChiTietSanPhamDTO(
            int maChiTietSanPham,
            int maSanPham,
            string? thuocTinh,
            decimal? giaNhap,
            string? trangThai)
        {
            _maChiTietSanPham = maChiTietSanPham;
            _maSanPham = maSanPham;
            _thuocTinh = thuocTinh;
            _giaNhap = giaNhap;
            _trangThai = trangThai;
        }

        public int MaChiTietSanPham
        {
            get { return _maChiTietSanPham; }
            set { _maChiTietSanPham = value; }
        }

        public int MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }

        public string? ThuocTinh
        {
            get { return _thuocTinh; }
            set { _thuocTinh = value; }
        }

        public decimal? GiaNhap
        {
            get { return _giaNhap; }
            set { _giaNhap = value; }
        }

        public string? TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }
    }
}
