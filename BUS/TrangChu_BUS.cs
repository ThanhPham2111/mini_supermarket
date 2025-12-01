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
        private KhoHangBUS khoHangBUS = new KhoHangBUS();
        private KhoHangDAO khoHangDAO = new KhoHangDAO();

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

        public int GetSoLuongSanPhamSapHetHang()
        {
            try
            {
                var list = khoHangDAO.LaySanPhamSapHetHang();
                return list?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        public int GetSoLuongSanPhamTiemCan()
        {
            try
            {
                var list = khoHangDAO.LaySanPhamSapHetHang();
                int count = 0;
                foreach (var item in list)
                {
                    int soLuong = item.SoLuong ?? 0;
                    if (soLuong >= 1 && soLuong <= 5)
                        count++;
                }
                return count;
            }
            catch
            {
                return 0;
            }
        }

        public IList<SanPhamKhoDTO> GetSanPhamSapHetHang()
        {
            return khoHangBUS.LaySanPhamSapHetHang();
        }

        public IList<SanPhamKhoDTO> GetSanPhamHetHang()
        {
            return khoHangBUS.LaySanPhamHetHang();
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
    }
}
