namespace mini_supermarket.DTO
{
    public class ChiTietPhieuNhapDTO
    {
        private int _maChiTietPhieuNhap;
        private int _maSanPham;
        private int _maPhieuNhap;
        private int _soLuong;
        private decimal _donGiaNhap;
        private decimal _thanhTien;

        public int MaChiTietPhieuNhap
        {
            get { return _maChiTietPhieuNhap; }
            set { _maChiTietPhieuNhap = value; }
        }

        public int MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }

        public int MaPhieuNhap
        {
            get { return _maPhieuNhap; }
            set { _maPhieuNhap = value; }
        }

        public int SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; }
        }

        public decimal DonGiaNhap
        {
            get { return _donGiaNhap; }
            set { _donGiaNhap = value; }
        }

        public decimal ThanhTien
        {
            get { return _thanhTien; }
            set { _thanhTien = value; }
        }

        public ChiTietPhieuNhapDTO()
        {
        }

        public ChiTietPhieuNhapDTO(
            int maChiTietPhieuNhap,
            int maSanPham,
            int maPhieuNhap,
            int soLuong,
            decimal donGiaNhap,
            decimal thanhTien)
        {
            _maChiTietPhieuNhap = maChiTietPhieuNhap;
            _maSanPham = maSanPham;
            _maPhieuNhap = maPhieuNhap;
            _soLuong = soLuong;
            _donGiaNhap = donGiaNhap;
            _thanhTien = thanhTien;
        }
    }
}
