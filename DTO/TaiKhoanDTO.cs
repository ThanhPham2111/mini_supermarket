namespace mini_supermarket.DTO
{
    public class TaiKhoanDTO
    {
        private int _maTaiKhoan;
        private string _tenDangNhap = string.Empty;
        private string _matKhau = string.Empty;
        private int _maNhanVien;
        private int _maQuyen;
        private string? _trangThai;

        public TaiKhoanDTO()
        {
        }

        public TaiKhoanDTO(
            int maTaiKhoan,
            string tenDangNhap,
            string matKhau,
            int maNhanVien,
            int maQuyen,
            string? trangThai)
        {
            _maTaiKhoan = maTaiKhoan;
            _tenDangNhap = tenDangNhap;
            _matKhau = matKhau;
            _maNhanVien = maNhanVien;
            _maQuyen = maQuyen;
            _trangThai = trangThai;
        }

        public int MaTaiKhoan
        {
            get { return _maTaiKhoan; }
            set { _maTaiKhoan = value; }
        }

        public string TenDangNhap
        {
            get { return _tenDangNhap; }
            set { _tenDangNhap = value; }
        }

        public string MatKhau
        {
            get { return _matKhau; }
            set { _matKhau = value; }
        }

        public int MaNhanVien
        {
            get { return _maNhanVien; }
            set { _maNhanVien = value; }
        }

        public int MaQuyen
        {
            get { return _maQuyen; }
            set { _maQuyen = value; }
        }

        public string? TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }
    }
}
