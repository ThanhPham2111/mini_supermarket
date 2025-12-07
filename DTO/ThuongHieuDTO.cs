using mini_supermarket.Common;

namespace mini_supermarket.DTO
{
    public class ThuongHieuDTO
    {
        private int _maThuongHieu;
        private string _tenThuongHieu = string.Empty;
        private string _trangThai = TrangThaiConstants.HoatDong;

        public ThuongHieuDTO()
        {
        }

        public ThuongHieuDTO(
            int maThuongHieu,
            string tenThuongHieu,
            string trangThai = TrangThaiConstants.HoatDong)
        {
            _maThuongHieu = maThuongHieu;
            _tenThuongHieu = tenThuongHieu;
            _trangThai = trangThai;
        }

        public int MaThuongHieu
        {
            get { return _maThuongHieu; }
            set { _maThuongHieu = value; }
        }

        public string TenThuongHieu
        {
            get { return _tenThuongHieu; }
            set { _tenThuongHieu = value; }
        }

        public string TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }
    }
}
