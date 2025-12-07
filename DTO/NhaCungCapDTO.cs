namespace mini_supermarket.DTO
{
    public class NhaCungCapDTO
    {


        
        // int là value type -> bắt buộc có giá trị
        private int _maNhaCungCap;
        // biến luôn có giá trị -> không bao giờ là null
        private string _tenNhaCungCap = string.Empty;
        // có thể null
        private string? _diaChi;
        private string? _soDienThoai;
        private string? _email;
        private string? _trangThai;




         public NhaCungCapDTO()
        {
        }

        public NhaCungCapDTO(
            int maNhaCungCap,
            string tenNhaCungCap,
            string? diaChi,
            string? soDienThoai,
            string? email,
            string? trangThai)
        {
            _maNhaCungCap = maNhaCungCap;
            _tenNhaCungCap = tenNhaCungCap;
            _diaChi = diaChi;
            _soDienThoai = soDienThoai;
            _email = email;
            _trangThai = trangThai;
        }

        public int MaNhaCungCap
        {
            get { return _maNhaCungCap; }
            set { _maNhaCungCap = value; }
        }

        public string TenNhaCungCap
        {
            get { return _tenNhaCungCap; }
            set { _tenNhaCungCap = value; }
        }

        public string? DiaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; }
        }

        public string? SoDienThoai
        {
            get { return _soDienThoai; }
            set { _soDienThoai = value; }
        }

        public string? Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string? TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }

       
    }
}
