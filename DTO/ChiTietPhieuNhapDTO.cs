namespace mini_supermarket.DTO
{
    public class ChiTietPhieuNhapDTO
    {
        public int MaChiTietPhieuNhap { get; set; }
        public int? MaSanPham { get; set; }
        public int? MaPhieuNhap { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGiaNhap { get; set; }
        public decimal? ThanhTien { get; set; }

        public ChiTietPhieuNhapDTO()
        {
        }

        public ChiTietPhieuNhapDTO(
            int maChiTietPhieuNhap,
            int? maSanPham,
            int? maPhieuNhap,
            int? soLuong,
            decimal? donGiaNhap,
            decimal? thanhTien)
        {
            MaChiTietPhieuNhap = maChiTietPhieuNhap;
            MaSanPham = maSanPham;
            MaPhieuNhap = maPhieuNhap;
            SoLuong = soLuong;
            DonGiaNhap = donGiaNhap;
            ThanhTien = thanhTien;
        }
    }
}
