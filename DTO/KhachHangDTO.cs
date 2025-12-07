namespace mini_supermarket.DTO
{
    public class KhachHangDTO
    {
        // Instance Fields
        private int _maKhachHang;
        private string? _tenKhachHang;
        private string? _soDienThoai;
        private string? _diaChi;
        private string? _email;
        private int? _diemTichLuy;
        private string? _trangThai;

        // Properties
        public int MaKhachHang 
        { 
            get => _maKhachHang; 
            set => _maKhachHang = value; 
        }

        public string? TenKhachHang 
        { 
            get => _tenKhachHang; 
            set => _tenKhachHang = value; 
        }

        public string? SoDienThoai 
        { 
            get => _soDienThoai; 
            set => _soDienThoai = value; 
        }

        public string? DiaChi 
        { 
            get => _diaChi; 
            set => _diaChi = value; 
        }

        public string? Email 
        { 
            get => _email; 
            set => _email = value; 
        }

        public int? DiemTichLuy 
        { 
            get => _diemTichLuy; 
            set => _diemTichLuy = value; 
        }

        public string? TrangThai 
        { 
            get => _trangThai; 
            set => _trangThai = value; 
        }

        // Constructors
        public KhachHangDTO()
        {
        }

        public KhachHangDTO(
            int maKhachHang,
            string? tenKhachHang,
            string? soDienThoai,
            string? diaChi,
            string? email,
            int? diemTichLuy,
            string? trangThai)
        {
            _maKhachHang = maKhachHang;
            _tenKhachHang = tenKhachHang;
            _soDienThoai = soDienThoai;
            _diaChi = diaChi;
            _email = email;
            _diemTichLuy = diemTichLuy;
            _trangThai = trangThai;
        }
    }
}
