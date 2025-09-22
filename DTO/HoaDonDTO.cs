using System;

namespace mini_supermarket.DTO
{
    public class HoaDonDTO
    {
        public int MaHoaDon { get; set; }
        public DateTime? NgayLap { get; set; }
        public int? MaNhanVien { get; set; }
        public int? MaKhachHang { get; set; }
        public decimal? TongTien { get; set; }

        public HoaDonDTO()
        {
        }

        public HoaDonDTO(
            int maHoaDon,
            DateTime? ngayLap,
            int? maNhanVien,
            int? maKhachHang,
            decimal? tongTien)
        {
            MaHoaDon = maHoaDon;
            NgayLap = ngayLap;
            MaNhanVien = maNhanVien;
            MaKhachHang = maKhachHang;
            TongTien = tongTien;
        }
    }
}
