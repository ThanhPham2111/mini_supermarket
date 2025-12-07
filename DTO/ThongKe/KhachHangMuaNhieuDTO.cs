namespace mini_supermarket.DTO
{
    public class KhachHangMuaNhieuDTO
    {
        private string _tenKhachHang = string.Empty;
        private int _tongSoLuong;

        public string TenKhachHang
        {
            get { return _tenKhachHang; }
            set { _tenKhachHang = value; }
        }

        public int TongSoLuong
        {
            get { return _tongSoLuong; }
            set { _tongSoLuong = value; }
        }
    }
}