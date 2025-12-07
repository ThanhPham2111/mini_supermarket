namespace mini_supermarket.DTO
{
    public class TopBanChayDTO
    {
        private string _tenSanPham = string.Empty;
        private int _tongSoLuong;

        public string TenSanPham
        {
            get { return _tenSanPham; }
            set { _tenSanPham = value; }
        }

        public int TongSoLuong
        {
            get { return _tongSoLuong; }
            set { _tongSoLuong = value; }
        }
    }
}