using mini_supermarket.Common;

namespace mini_supermarket.DTO
{
    public class LoaiDTO
    {
        private int _maLoai;
        private string _tenLoai = string.Empty;
        private string? _moTa;
        private string _trangThai = TrangThaiConstants.HoatDong;

        public LoaiDTO()
        {
        }

        public LoaiDTO(
            int maLoai,
            string tenLoai,
            string? moTa,
            string trangThai = TrangThaiConstants.HoatDong)
        {
            _maLoai = maLoai;
            _tenLoai = tenLoai;
            _moTa = moTa;
            _trangThai = trangThai;
        }

        public int MaLoai
        {
            get { return _maLoai; }
            set { _maLoai = value; }
        }

        public string TenLoai
        {
            get { return _tenLoai; }
            set { _tenLoai = value; }
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
