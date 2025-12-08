namespace mini_supermarket.DTO
{
    public class KhoHangDTO
    {
        public int MaSanPham { get; set; }
        public string? TenSanPham { get; set; }
        public int? SoLuong { get; set; }
        public string? TrangThai { get; set; }
        public string? TrangThaiDieuKien { get; set; } // Trạng thái bán: "Bán" hoặc "Không bán"
        public decimal? GiaNhap { get; set; } // Giá nhập
        public decimal? GiaBan { get; set; } // Giá bán

        public KhoHangDTO()
        {
        }

        public KhoHangDTO(int maSanPham, int? soLuong, string? trangThai, string? trangThaiDieuKien = "Bán", decimal? giaNhap = null, decimal? giaBan = null)
        {
            MaSanPham = maSanPham;
            SoLuong = soLuong;
            TrangThai = trangThai;
            TrangThaiDieuKien = trangThaiDieuKien;
            GiaNhap = giaNhap;
            GiaBan = giaBan;
        }
    }
}
