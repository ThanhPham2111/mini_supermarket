namespace mini_supermarket.DTO
{
    public class PhanQuyenDTO
    {
        private int _maQuyen;
        private string _tenQuyen = string.Empty;
        private string? _moTa;

        public PhanQuyenDTO()
        {
        }

        public PhanQuyenDTO(
            int maQuyen,
            string tenQuyen,
            string? moTa)
        {
            _maQuyen = maQuyen;
            _tenQuyen = tenQuyen;
            _moTa = moTa;
        }

        public int MaQuyen
        {
            get { return _maQuyen; }
            set { _maQuyen = value; }
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
