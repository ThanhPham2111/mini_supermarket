using System;

namespace mini_supermarket.DTO
{
    public class TonKhoDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string? TenDonVi { get; set; }
        public string? TenLoai { get; set; }
        public string? TenThuongHieu { get; set; }
        public int? SoLuong { get; set; }
        public string? TrangThai { get; set; }
        public int? MaLoai { get; set; }
        public int? MaThuongHieu { get; set; }
        public decimal? GiaBan { get; set; }
        public DateTime? Hsd { get; set; }
        public decimal? GiaNhap { get; set; }
    }
}