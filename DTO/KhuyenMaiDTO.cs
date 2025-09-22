using System;

namespace mini_supermarket.DTO
{
    public class KhuyenMaiDTO
    {
        public int MaKhuyenMai { get; set; }
        public int? MaSanPham { get; set; }
        public string? TenKhuyenMai { get; set; }
        public decimal? PhanTramGiamGia { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? MoTa { get; set; }

        public KhuyenMaiDTO()
        {
        }

        public KhuyenMaiDTO(
            int maKhuyenMai,
            int? maSanPham,
            string? tenKhuyenMai,
            decimal? phanTramGiamGia,
            DateTime? ngayBatDau,
            DateTime? ngayKetThuc,
            string? moTa)
        {
            MaKhuyenMai = maKhuyenMai;
            MaSanPham = maSanPham;
            TenKhuyenMai = tenKhuyenMai;
            PhanTramGiamGia = phanTramGiamGia;
            NgayBatDau = ngayBatDau;
            NgayKetThuc = ngayKetThuc;
            MoTa = moTa;
        }
    }
}
