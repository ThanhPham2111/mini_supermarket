using System;

namespace mini_supermarket.DTO
{
    public class SanPhamHetHanDTO
    {
        private string _tenSanPham = string.Empty;
        private DateTime? _hsd;

        public string TenSanPham
        {
            get { return _tenSanPham; }
            set { _tenSanPham = value; }
        }

        public DateTime? HSD
        {
            get { return _hsd; }
            set { _hsd = value; }
        }
    }
}