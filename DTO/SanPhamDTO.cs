using System;

namespace mini_supermarket.DTO
{
    public class SanPhamDTO
    {
        private int _maSanPham;
        private string _tenSanPham = string.Empty;
        private int _maDonVi;
        private string? _tenDonVi;
        private int _maThuongHieu;
        private int _maLoai;
        private string? _moTa;
        private decimal? _giaBan;
        private string? _hinhAnh;
        private string? _xuatXu;
        private DateTime? _hsd;
        private string? _trangThai;
        private string? _tenLoai;
        private string? _tenThuongHieu;
        private int? _soLuong;
        private decimal? _phanTramGiam;
        private string? _khuyenMai;

        public SanPhamDTO()
        {
        }

        public SanPhamDTO(
            int maSanPham,
            string tenSanPham,
            int maDonVi,
            string? tenDonVi,
            int maThuongHieu,
            int maLoai,
            string? moTa,
            decimal? giaBan,
            string? hinhAnh,
            string? xuatXu,
            DateTime? hsd,
            string? trangThai,
            string? tenLoai,
            string? tenThuongHieu,
            int? soLuong,
            decimal? phanTramGiam,
            string? khuyenMai)
        {
            _maSanPham = maSanPham;
            _tenSanPham = tenSanPham;
            _maDonVi = maDonVi;
            _tenDonVi = tenDonVi;
            _maThuongHieu = maThuongHieu;
            _maLoai = maLoai;
            _moTa = moTa;
            _giaBan = giaBan;
            _hinhAnh = hinhAnh;
            _xuatXu = xuatXu;
            _hsd = hsd;
            _trangThai = trangThai;
            _tenLoai = tenLoai;
            _tenThuongHieu = tenThuongHieu;
            _soLuong = soLuong;
            _phanTramGiam = phanTramGiam;
            _khuyenMai = khuyenMai;
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

        public int MaDonVi
        {
            get { return _maDonVi; }
            set { _maDonVi = value; }
        }

        public string? TenDonVi
        {
            get { return _tenDonVi; }
            set { _tenDonVi = value; }
        }

        public int MaThuongHieu
        {
            get { return _maThuongHieu; }
            set { _maThuongHieu = value; }
        }

        public int MaLoai
        {
            get { return _maLoai; }
            set { _maLoai = value; }
        }

        public string? MoTa
        {
            get { return _moTa; }
            set { _moTa = value; }
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

        public string? XuatXu
        {
            get { return _xuatXu; }
            set { _xuatXu = value; }
        }

        public DateTime? Hsd
        {
            get { return _hsd; }
            set { _hsd = value; }
        }

        public string? TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }

        public string? TenLoai
        {
            get { return _tenLoai; }
            set { _tenLoai = value; }
        }

        public string? TenThuongHieu
        {
            get { return _tenThuongHieu; }
            set { _tenThuongHieu = value; }
        }

        public int? SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; }
        }

        public decimal? PhanTramGiam
        {
            get { return _phanTramGiam; }
            set { _phanTramGiam = value; }
        }

        public string? KhuyenMai
        {
            get { return _khuyenMai; }
            set { _khuyenMai = value; }
        }
    }
}
