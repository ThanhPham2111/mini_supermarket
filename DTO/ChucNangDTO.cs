using System;

namespace mini_supermarket.DTO
{
    public class ChucNangDTO
    {
        private int _maChucNang;
        private string _tenChucNang = string.Empty;
        private int? _maCha;
        private string? _duongDan;
        private string? _moTa;

        public ChucNangDTO()
        {
        }

        public ChucNangDTO(
            int maChucNang,
            string tenChucNang,
            int? maCha,
            string? duongDan,
            string? moTa)
        {
            _maChucNang = maChucNang;
            _tenChucNang = tenChucNang;
            _maCha = maCha;
            _duongDan = duongDan;
            _moTa = moTa;
        }

        public int MaChucNang
        {
            get { return _maChucNang; }
            set { _maChucNang = value; }
        }

        public string TenChucNang
        {
            get { return _tenChucNang; }
            set { _tenChucNang = value; }
        }

        public int? MaCha
        {
            get { return _maCha; }
            set { _maCha = value; }
        }

        public string? DuongDan
        {
            get { return _duongDan; }
            set { _duongDan = value; }
        }

        public string? MoTa
        {
            get { return _moTa; }
            set { _moTa = value; }
        }
    }
}
