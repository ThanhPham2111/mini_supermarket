using mini_supermarket.Common;

namespace mini_supermarket.DTO
{
    public class ThuongHieuDTO
    {
        public int MaThuongHieu { get; set; }
        public string TenThuongHieu { get; set; } = string.Empty;
        public string TrangThai { get; set; } = TrangThaiConstants.HoatDong;

        public ThuongHieuDTO()
        {
        }

        public ThuongHieuDTO(int maThuongHieu, string tenThuongHieu, string trangThai = TrangThaiConstants.HoatDong)
        {
            MaThuongHieu = maThuongHieu;
            TenThuongHieu = tenThuongHieu;
            TrangThai = trangThai;
        }
    }
}
