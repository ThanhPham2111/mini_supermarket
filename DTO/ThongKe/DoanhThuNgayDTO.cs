using System;

namespace mini_supermarket.DTO
{
    public class DoanhThuNgayDTO
    {
        private DateTime _ngay;
        private decimal _tongDoanhThu;

        public DateTime Ngay
        {
            get { return _ngay; }
            set { _ngay = value; }
        }

        public decimal TongDoanhThu
        {
            get { return _tongDoanhThu; }
            set { _tongDoanhThu = value; }
        }
    }
}