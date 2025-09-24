namespace mini_supermarket.DTO
{
    public class PhanQuyenDTO
    {
        public int MaQuyen { get; set; }
        public string TenQuyen { get; set; } = string.Empty;
        public string? MoTa { get; set; }

        public PhanQuyenDTO()
        {
        }

        public PhanQuyenDTO(int maQuyen, string tenQuyen, string? moTa)
        {
            MaQuyen = maQuyen;
            TenQuyen = tenQuyen;
            MoTa = moTa;
        }
    }
}
