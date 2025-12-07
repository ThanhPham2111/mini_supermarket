using mini_supermarket.Common;

namespace mini_supermarket.DTO
{
    public class DonViDTO
    {
        private int _maDonVi;
        private string _tenDonVi = string.Empty;
        private string? _moTa;
        private string _trangThai = TrangThaiConstants.HoatDong;

        public DonViDTO()
        {
        }

        public DonViDTO(
            int maDonVi,
            string tenDonVi,
            string? moTa,
            string trangThai = TrangThaiConstants.HoatDong)
        {
            _maDonVi = maDonVi;
            _tenDonVi = tenDonVi;
            _moTa = moTa;
            _trangThai = trangThai;
        }

        public int MaDonVi
        {
            get { return _maDonVi; }
            set { _maDonVi = value; }
        }

        public string TenDonVi
        {
            get { return _tenDonVi; }
            set { _tenDonVi = value; }
        }

        public string? MoTa
        {
            get { return _moTa; }
            set { _moTa = value; }
        }

        public string TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }
    }
}
