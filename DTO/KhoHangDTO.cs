﻿namespace mini_supermarket.DTO
{
    public class KhoHangDTO
    {
        public int MaSanPham { get; set; }
        public string? TenSanPham { get; set; } // Tên sản phẩm để hiển thị
        public int? SoLuong { get; set; }
        public string? TrangThai { get; set; }

        public KhoHangDTO()
        {
        }

        public KhoHangDTO(int maSanPham, int? soLuong, string? trangThai)
        {
            MaSanPham = maSanPham;
            SoLuong = soLuong;
            TrangThai = trangThai;
        }
    }
}