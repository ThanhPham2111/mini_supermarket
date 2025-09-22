using System;

namespace mini_supermarket.DTO
{
    public class LichSuTichDiemDTO
    {
        public int MaLichSuTichDiem { get; set; }
        public int? MaKhachHang { get; set; }
        public int? MaHoaDon { get; set; }
        public int? DiemCong { get; set; }
        public int? DiemSuDung { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string? MoTa { get; set; }

        public LichSuTichDiemDTO()
        {
        }

        public LichSuTichDiemDTO(
            int maLichSuTichDiem,
            int? maKhachHang,
            int? maHoaDon,
            int? diemCong,
            int? diemSuDung,
            DateTime? ngayCapNhat,
            string? moTa)
        {
            MaLichSuTichDiem = maLichSuTichDiem;
            MaKhachHang = maKhachHang;
            MaHoaDon = maHoaDon;
            DiemCong = diemCong;
            DiemSuDung = diemSuDung;
            NgayCapNhat = ngayCapNhat;
            MoTa = moTa;
        }
    }
}
