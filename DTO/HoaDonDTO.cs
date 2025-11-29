using System;

namespace mini_supermarket.DTO
{
    public class HoaDonDTO
    {
        public int MaHoaDon { get; set; }
        public string MaHoaDonCode { get; set; } = string.Empty;
        public DateTime? NgayLap { get; set; }
        public int MaNhanVien { get; set; }
        public int? MaKhachHang { get; set; }
        public decimal? TongTien { get; set; }

        // Properties for display
        public string NhanVien { get; set; } = string.Empty;
        public string KhachHang { get; set; } = string.Empty;
        public decimal? ThanhTien => TongTien;
        public string TrangThai { get; set; } = string.Empty;

        public HoaDonDTO()
        {
        }

        public HoaDonDTO(
            int maHoaDon,
            string maHoaDonCode,
            DateTime? ngayLap,
            int maNhanVien,
            int? maKhachHang,
            decimal? tongTien,
            string trangThai)
        {
            MaHoaDon = maHoaDon;
            MaHoaDonCode = maHoaDonCode;
            NgayLap = ngayLap;
            MaNhanVien = maNhanVien;
            MaKhachHang = maKhachHang;
            TongTien = tongTien;
            TrangThai = trangThai;
        }
    }
}
