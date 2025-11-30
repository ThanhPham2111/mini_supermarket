namespace mini_supermarket.DTO
{
    public class SanPhamBanHangDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public decimal? GiaBan { get; set; }
        public int? SoLuong { get; set; }
        public string KhuyenMai { get; set; } = string.Empty;
        public decimal PhanTramGiam { get; set; }
    }
}