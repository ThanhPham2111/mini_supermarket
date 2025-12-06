using mini_supermarket.DAO;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.DTO;

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

        public IList<DoanhThuNgayDTO> GetDoanhThu7Ngay()
        {
            return trangChuDAO.GetDoanhThu7Ngay();
        }

        public IList<TopBanChayDTO> GetTop5BanChay()
        {
            return trangChuDAO.GetTop5BanChay();
        }

        public IList<SanPhamHetHanDTO> GetSanPhamSapHetHan()
        {
            return trangChuDAO.GetSanPhamSapHetHan();
        }

        public IList<KhachHangMuaNhieuDTO> GetKhachHangMuaNhieuNhat()
        {
            return trangChuDAO.GetKhachHangMuaNhieuNhat();
        }

        public IList<SanPhamHetHanDTO> GetSanPhamDaHetHan()
        {
            return trangChuDAO.GetSanPhamDaHetHan();
        }
    }
}
