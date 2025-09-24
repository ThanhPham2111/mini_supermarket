namespace mini_supermarket.DTO
{
    public class NhaCungCapDTO
    {
        public int MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; } = string.Empty;
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? TrangThai { get; set; }

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
            MaNhaCungCap = maNhaCungCap;
            TenNhaCungCap = tenNhaCungCap;
            DiaChi = diaChi;
            SoDienThoai = soDienThoai;
            Email = email;
            TrangThai = trangThai;
        }
    }
}
