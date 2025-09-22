namespace mini_supermarket.DTO
{
    public class TaiKhoanDTO
    {
        public int MaTaiKhoan { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public int? MaNhanVien { get; set; }
        public int? MaQuyen { get; set; }
        public string? TrangThai { get; set; }

        public TaiKhoanDTO()
        {
        }

        public TaiKhoanDTO(
            int maTaiKhoan,
            string? tenDangNhap,
            string? matKhau,
            int? maNhanVien,
            int? maQuyen,
            string? trangThai)
        {
            MaTaiKhoan = maTaiKhoan;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            MaNhanVien = maNhanVien;
            MaQuyen = maQuyen;
            TrangThai = trangThai;
        }
    }
}
