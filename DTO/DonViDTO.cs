using mini_supermarket.Common;

namespace mini_supermarket.DTO
{
    public class DonViDTO
    {
        public int MaDonVi { get; set; }
        public string TenDonVi { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public string TrangThai { get; set; } = TrangThaiConstants.HoatDong;

        public DonViDTO()
        {
        }

        public DonViDTO(int maDonVi, string tenDonVi, string? moTa, string trangThai = TrangThaiConstants.HoatDong)
        {
            MaDonVi = maDonVi;
            TenDonVi = tenDonVi;
            MoTa = moTa;
            TrangThai = trangThai;
        }
    }
}
