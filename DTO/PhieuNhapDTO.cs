using System;
using System.Collections.Generic;

namespace mini_supermarket.DTO
{
    public class PhieuNhapDTO
    {
        public int MaPhieuNhap { get; set; }
        public DateTime? NgayNhap { get; set; }
        public int MaNhaCungCap { get; set; }
        public decimal? TongTien { get; set; }
        public ICollection<ChiTietPhieuNhapDTO> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhapDTO>();

        public PhieuNhapDTO()
        {
        }

        public PhieuNhapDTO(
            int maPhieuNhap,
            DateTime? ngayNhap,
            int maNhaCungCap,
            decimal? tongTien)
        {
            MaPhieuNhap = maPhieuNhap;
            NgayNhap = ngayNhap;
            MaNhaCungCap = maNhaCungCap;
            TongTien = tongTien;
        }
    }
}
