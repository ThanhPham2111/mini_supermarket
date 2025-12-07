namespace mini_supermarket.DTO
{
    public class ChiTietHoaDonDTO
    {
        public int MaChiTietHoaDon { get; set; }
        public int MaHoaDon { get; set; }
        public int MaSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }

        // Properties for display
        public string TenSanPham { get; set; } = string.Empty;
        public string DonVi { get; set; } = string.Empty;
        public decimal ThanhTien => SoLuong * GiaBan;

        public ChiTietHoaDonDTO()
        {
        }

        public ChiTietHoaDonDTO(
            int maChiTietHoaDon,
            int maHoaDon,
            int maSanPham,
            int soLuong,
            decimal giaBan)
        {
            MaChiTietHoaDon = maChiTietHoaDon;
            MaHoaDon = maHoaDon;
            MaSanPham = maSanPham;
            SoLuong = soLuong;
            GiaBan = giaBan;
        }
    }
}
