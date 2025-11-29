using System;

namespace mini_supermarket.DTO
{
    public class QuyTacLoiNhuanDTO
    {
        public int MaQuyTac { get; set; }
        public string LoaiQuyTac { get; set; } = string.Empty; // 'Chung', 'TheoLoai', 'TheoThuongHieu', 'TheoDonVi', 'TheoSanPham'
        public int? MaLoai { get; set; }
        public int? MaThuongHieu { get; set; }
        public int? MaDonVi { get; set; }
        public int? MaSanPham { get; set; }
        public decimal PhanTramLoiNhuan { get; set; }
        public int UuTien { get; set; }
        public string? TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public int? MaNhanVien { get; set; }
        
        // Thông tin tham chiếu (để hiển thị)
        public string? TenLoai { get; set; }
        public string? TenThuongHieu { get; set; }
        public string? TenDonVi { get; set; }
        public string? TenSanPham { get; set; }
    }
}

