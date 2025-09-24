namespace mini_supermarket.DTO
{
    public class KhoHangDTO
    {
        public int MaSanPham { get; set; }
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
