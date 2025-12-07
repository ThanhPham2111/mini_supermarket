using System;

namespace mini_supermarket.DTO
{
    public class LoaiQuyenDTO
    {
        private int _maLoaiQuyen;
        private string _tenQuyen = string.Empty;
        private string? _moTa;

        public LoaiQuyenDTO()
        {
        }

        public LoaiQuyenDTO(
            int maLoaiQuyen,
            string tenQuyen,
            string? moTa)
        {
            _maLoaiQuyen = maLoaiQuyen;
            _tenQuyen = tenQuyen;
            _moTa = moTa;
        }

        public int MaLoaiQuyen
        {
            get { return _maLoaiQuyen; }
            set { _maLoaiQuyen = value; }
        }

        public string TenQuyen
        {
            get { return _tenQuyen; }
            set { _tenQuyen = value; }
        }

        public string? MoTa
        {
            get { return _moTa; }
            set { _moTa = value; }
        }
    }
}
