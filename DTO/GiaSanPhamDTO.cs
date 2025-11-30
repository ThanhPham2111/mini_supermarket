using System;

namespace mini_supermarket.DTO
{
    public class GiaSanPhamDTO
    {
        public int MaGia { get; set; }
        public int MaSanPham { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaNhapGoc { get; set; }
        public decimal PhanTramLoiNhuanApDung { get; set; }
        public decimal GiaBan { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        
        // Thông tin sản phẩm (để hiển thị)
        public string? TenSanPham { get; set; }
        public string? TenLoai { get; set; }
        public string? TenThuongHieu { get; set; }
        public string? TenDonVi { get; set; }
    }
}

