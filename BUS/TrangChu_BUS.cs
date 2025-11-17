using mini_supermarket.DAO;
using System;
using System.Data;

namespace mini_supermarket.BUS
{
    public class TrangChuBUS
    {
        private TrangChuDAO trangChuDAO = new TrangChuDAO();

        public decimal GetDoanhThuHomNay()
        {
            var result = trangChuDAO.GetDoanhThuHomNay();
            
            if (result == null || result == DBNull.Value)
            {
                return 0;
            }
            
            return Convert.ToDecimal(result);
        }

        public int GetSoHoaDonHomNay()
        {
            return trangChuDAO.GetSoHoaDonHomNay();
        }

        public int GetSoLuongSanPhamHetHang()
        {
            return trangChuDAO.GetSoLuongSanPhamHetHang();
        }

        public DataTable GetDoanhThu7Ngay()
        {
            return trangChuDAO.GetDoanhThu7Ngay();
        }

        public DataTable GetTop5BanChay()
        {
            return trangChuDAO.GetTop5BanChay();
        }

        public DataTable GetSanPhamSapHetHan()
        {
            return trangChuDAO.GetSanPhamSapHetHan();
        }
    }
}
