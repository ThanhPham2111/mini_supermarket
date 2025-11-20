using System;
using System.Data;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    /// <summary>
    /// BUS cho L?ch s? thay ??i kho: ki?m tra tham s? và ?i?u ph?i DAO.
    /// </summary>
    public class LichSuThayDoiKho_BUS
    {
        private readonly LichSuThayDoiKho_DAO _dao = new();

        public int GhiNhan(LichSuThayDoiKhoDTO log)
        {
            if (log == null) throw new ArgumentNullException(nameof(log));
            if (string.IsNullOrWhiteSpace(log.LoaiThayDoi)) throw new ArgumentException("Loai thay doi khong hop le.");
            if (log.MaSanPham <= 0) throw new ArgumentException("Ma san pham khong hop le.");
            if (log.SoLuongMoi < 0 || log.SoLuongCu < 0) throw new ArgumentException("So luong khong hop le.");
            if (log.ChenhLech != log.SoLuongMoi - log.SoLuongCu)
            {
                // ??m b?o nh?t quán
                log.ChenhLech = log.SoLuongMoi - log.SoLuongCu;
            }
            return _dao.Insert(log);
        }

        public DataTable LayTheoSanPham(int maSanPham, int top = 50)
        {
            if (maSanPham <= 0) throw new ArgumentException("Ma san pham khong hop le.");
            if (top <= 0) top = 50;
            return _dao.GetByProduct(maSanPham, top);
        }

        public DataTable LayGanDay(int top = 20)
        {
            if (top <= 0) top = 20;
            return _dao.GetRecent(top);
        }

        public DataTable TraCuu(DateTime? from, DateTime? to, string? loaiThayDoi, int? maNhanVien, int? maSanPham)
        {
            // chu?n hóa kho?ng th?i gian (to: end of day + epsilon)
            DateTime? toExclusive = to?.Date.AddDays(1);
            return _dao.GetByFilter(from?.Date, toExclusive, string.IsNullOrWhiteSpace(loaiThayDoi) ? null : loaiThayDoi.Trim(), maNhanVien, maSanPham);
        }
    }
}
