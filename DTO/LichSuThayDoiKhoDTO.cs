namespace mini_supermarket.DTO
{
    public class LichSuThayDoiKhoDTO
    {
        public int MaLichSu { get; set; }
        public int MaSanPham { get; set; }
        public string? TenSanPham { get; set; }
        public int SoLuongCu { get; set; }
        public int SoLuongMoi { get; set; }
        public int ChenhLech { get; set; }
        public string LoaiThayDoi { get; set; } = string.Empty; // Ki?m kê, ?i?u ch?nh, H?y hàng
        public string? LyDo { get; set; }
        public string? GhiChu { get; set; }
        public int MaNhanVien { get; set; }
        public string? TenNhanVien { get; set; }
        public DateTime NgayThayDoi { get; set; }

        public LichSuThayDoiKhoDTO()
        {
            NgayThayDoi = DateTime.Now;
        }

        public LichSuThayDoiKhoDTO(int maSanPham, int soLuongCu, int soLuongMoi, string loaiThayDoi, string? lyDo, string? ghiChu, int maNhanVien)
        {
            MaSanPham = maSanPham;
            SoLuongCu = soLuongCu;
            SoLuongMoi = soLuongMoi;
            ChenhLech = soLuongMoi - soLuongCu;
            LoaiThayDoi = loaiThayDoi;
            LyDo = lyDo;
            GhiChu = ghiChu;
            MaNhanVien = maNhanVien;
            NgayThayDoi = DateTime.Now;
        }
    }
}
