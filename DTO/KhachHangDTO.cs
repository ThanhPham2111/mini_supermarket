namespace mini_supermarket.DTO
{
    public class KhachHangDTO
    {
        public int MaKhachHang { get; set; }
        public string? TenKhachHang { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
        public int? DiemTichLuy { get; set; }
        public string? TrangThai { get; set; }

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
            MaKhachHang = maKhachHang;
            TenKhachHang = tenKhachHang;
            SoDienThoai = soDienThoai;
            DiaChi = diaChi;
            Email = email;
            DiemTichLuy = diemTichLuy;
            TrangThai = trangThai;
        }
    }
}
