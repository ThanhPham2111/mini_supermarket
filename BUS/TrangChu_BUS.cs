using mini_supermarket.DAO;
using System;
using System.Data;

namespace mini_supermarket.BUS
{
    public class TrangChuBUS
    {
        private TrangChuDAO trangChuDAO = new TrangChuDAO();
        private KhoHangBUS khoHangBUS = new KhoHangBUS();

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
                DataTable dt = khoHangBUS.LaySanPhamSapHetHang();
                return dt?.Rows.Count ?? 0;
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
                DataTable dt = khoHangBUS.LaySanPhamSapHetHang();
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
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

        public DataTable GetSanPhamSapHetHang()
        {
            return khoHangBUS.LaySanPhamSapHetHang();
        }

        public DataTable GetSanPhamHetHang()
        {
            return khoHangBUS.LaySanPhamHetHang();
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

        public DataTable GetKhachHangMuaNhieuNhat()
        {
            return trangChuDAO.GetKhachHangMuaNhieuNhat();
        }
    }
}
