using mini_supermarket.DAO;
using System;
using System.Data;

namespace mini_supermarket.BUS
{
    public class TrangChuBUS
    {
        private TrangChuDAO trangChuDAO = new TrangChuDAO();
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

        /// <summary>
        /// L?y s? l??ng s?n ph?m s?p h?t hàng (1-10)
        /// </summary>
        public int GetSoLuongSanPhamSapHetHang()
        {
            try
            {
                DataTable dt = khoHangDAO.LaySanPhamSapHetHang();
                return dt?.Rows.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// L?y s? l??ng s?n ph?m ? tr?ng thái t?n c?n (1-5)
        /// </summary>
        public int GetSoLuongSanPhamTiemCan()
        {
            try
            {
                DataTable dt = khoHangDAO.LaySanPhamSapHetHang();
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

        /// <summary>
        /// L?y danh sách s?n ph?m s?p h?t hàng
        /// </summary>
        public DataTable GetSanPhamSapHetHang()
        {
            return khoHangDAO.LaySanPhamSapHetHang();
        }

        /// <summary>
        /// L?y danh sách s?n ph?m h?t hàng
        /// </summary>
        public DataTable GetSanPhamHetHang()
        {
            return khoHangDAO.LaySanPhamHetHang();
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
