namespace mini_supermarket.DTO
{
    public class SanPhamKhoDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public int? SoLuong { get; set; }
        public decimal? GiaBan { get; set; }
        public string? TenDonVi { get; set; }
        public string? TenLoai { get; set; }
    }
}