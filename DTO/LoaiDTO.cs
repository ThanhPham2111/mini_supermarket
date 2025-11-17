using mini_supermarket.Common;

namespace mini_supermarket.DTO
{
    public class LoaiDTO
    {
        public int MaLoai { get; set; }
        public string TenLoai { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public string TrangThai { get; set; } = TrangThaiConstants.HoatDong;

        public LoaiDTO()
        {
        }

        public LoaiDTO(int maLoai, string tenLoai, string? moTa, string trangThai = TrangThaiConstants.HoatDong)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
            MoTa = moTa;
            TrangThai = trangThai;
        }
    }
}
