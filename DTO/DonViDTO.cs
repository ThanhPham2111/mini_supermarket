namespace mini_supermarket.DTO
{
    public class DonViDTO
    {
        public int MaDonVi { get; set; }
        public string TenDonVi { get; set; } = string.Empty;
        public string? MoTa { get; set; }

        public DonViDTO()
        {
        }

        public DonViDTO(int maDonVi, string tenDonVi, string? moTa)
        {
            MaDonVi = maDonVi;
            TenDonVi = tenDonVi;
            MoTa = moTa;
        }
    }
}
