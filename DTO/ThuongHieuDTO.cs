namespace mini_supermarket.DTO
{
    public class ThuongHieuDTO
    {
        public int MaThuongHieu { get; set; }
        public string TenThuongHieu { get; set; } = string.Empty;

        public ThuongHieuDTO()
        {
        }

        public ThuongHieuDTO(int maThuongHieu, string tenThuongHieu)
        {
            MaThuongHieu = maThuongHieu;
            TenThuongHieu = tenThuongHieu;
        }
    }
}
