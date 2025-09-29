namespace mini_supermarket.DTO
{
    public class ChiTietSanPhamDTO
    {
        public int MaChiTietSanPham { get; set; }
        public int MaSanPham { get; set; }
        public string? ThuocTinh { get; set; }
        public decimal? GiaNhap { get; set; }
        public string? TrangThai { get; set; }

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
            MaChiTietSanPham = maChiTietSanPham;
            MaSanPham = maSanPham;
            ThuocTinh = thuocTinh;
            GiaNhap = giaNhap;
            TrangThai = trangThai;
        }
    }
}
