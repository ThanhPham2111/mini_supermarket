using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using mini_supermarket.DAO;
using mini_supermarket.DTO;
using mini_supermarket.DB;

namespace mini_supermarket.BUS
{
    public class HoaDon_BUS
    {
        private readonly HoaDon_DAO _hoaDonDao = new();
        public int GetMaxMaHoaDon()
        {
            int maxID = _hoaDonDao.GetMaxMaHoaDon();
            return maxID + 1;
        }

        public IList<HoaDonDTO> GetHoaDon()
        {
            return _hoaDonDao.GetHoaDon();
        }

        public List<ChiTietHoaDonDTO> GetChiTietHoaDon(string maHoaDon)
        {
            return _hoaDonDao.GetChiTietHoaDon(maHoaDon);
        }

        public int CreateHoaDon(HoaDonDTO hoaDon, List<ChiTietHoaDonDTO> chiTietList)
        {
            if (hoaDon == null)
                throw new ArgumentNullException(nameof(hoaDon));

            if (chiTietList == null || chiTietList.Count == 0)
                throw new ArgumentException("Danh sách chi tiết hóa đơn không được rỗng.", nameof(chiTietList));

            // Insert hóa đơn
            int maHoaDon = _hoaDonDao.InsertHoaDon(hoaDon);

            // Insert chi tiết hóa đơn
            foreach (var chiTiet in chiTietList)
            {
                chiTiet.MaHoaDon = maHoaDon;
                _hoaDonDao.InsertChiTietHoaDon(chiTiet);
            }

            return maHoaDon;
        }

        public HoaDonDTO? GetHoaDonById(int maHoaDon)
        {
            return _hoaDonDao.GetHoaDonById(maHoaDon);
        }

        public int HuyHoaDon(HoaDonDTO hoaDon)
        {
            if (hoaDon == null)
                throw new ArgumentNullException(nameof(hoaDon));

            return _hoaDonDao.HuyHoaDon(hoaDon);
        }

        /// <summary>
        /// Hủy hóa đơn hoàn chỉnh: trả lại kho, trả lại điểm, cập nhật trạng thái
        /// </summary>
        public void HuyHoaDonHoanChinh(int maHoaDon, int maNhanVienHuy, string lyDoHuy)
        {
            if (maHoaDon <= 0)
                throw new ArgumentException("Mã hóa đơn không hợp lệ.", nameof(maHoaDon));

            if (string.IsNullOrWhiteSpace(lyDoHuy))
                throw new ArgumentException("Lý do hủy không được để trống.", nameof(lyDoHuy));

            // 1. Lấy thông tin hóa đơn
            var hoaDon = _hoaDonDao.GetHoaDonById(maHoaDon);
            if (hoaDon == null)
                throw new InvalidOperationException("Không tìm thấy hóa đơn.");

            // Kiểm tra trạng thái
            if (hoaDon.TrangThai == "Đã hủy")
                throw new InvalidOperationException("Hóa đơn này đã được hủy trước đó.");

            // 2. Lấy chi tiết hóa đơn
            var chiTietList = _hoaDonDao.GetChiTietHoaDon(maHoaDon.ToString());
            if (chiTietList == null || chiTietList.Count == 0)
                throw new InvalidOperationException("Hóa đơn không có chi tiết.");

            // 3. Tính điểm tích lũy từ hóa đơn (tổng số lượng sản phẩm)
            int diemTichLuy = chiTietList.Sum(ct => ct.SoLuong);

            // 4. Tính điểm đã sử dụng (nếu có)
            // Tính từ chênh lệch giữa tổng tiền lý thuyết và tổng tiền thực tế
            var quyDoiDiemBUS = new QuyDoiDiem_BUS();
            decimal giaTriMotDiem = quyDoiDiemBUS.GetGiaTriMotDiem();
            
            // Tính tổng tiền lý thuyết (không có giảm điểm)
            decimal tongTienLyThuyet = chiTietList.Sum(ct => ct.GiaBan * ct.SoLuong);
            decimal tongTienThucTe = hoaDon.TongTien ?? 0;
            decimal giamTuDiem = tongTienLyThuyet - tongTienThucTe;
            int diemSuDung = giaTriMotDiem > 0 && giamTuDiem > 0 ? (int)Math.Floor(giamTuDiem / giaTriMotDiem) : 0;

            // 5. Thực hiện các thao tác (mỗi method có transaction riêng, nhưng đảm bảo logic đúng)
            try
            {
                // 5.1. Trả lại số lượng kho cho từng sản phẩm
                var khoHangBUS = new KhoHangBUS();
                foreach (var chiTiet in chiTietList)
                {
                    // Tăng số lượng kho (trả lại hàng)
                    khoHangBUS.TangSoLuongKho(
                        chiTiet.MaSanPham,
                        chiTiet.SoLuong,
                        maNhanVienHuy,
                        "Trả hàng do hủy hóa đơn",
                        $"Hủy hóa đơn #{maHoaDon}: {lyDoHuy}"
                    );
                }

                // 5.2. Cập nhật điểm khách hàng (nếu có)
                if (hoaDon.MaKhachHang.HasValue)
                {
                    var khachHangBUS = new KhachHang_BUS();
                    var khachHangList = khachHangBUS.GetKhachHang();
                    var khachHang = khachHangList.FirstOrDefault(kh => kh.MaKhachHang == hoaDon.MaKhachHang.Value);
                    
                    if (khachHang != null)
                    {
                        int diemHienTai = khachHang.DiemTichLuy ?? 0;
                        // Trả lại điểm đã dùng, trừ điểm đã tích
                        int diemMoi = diemHienTai + diemSuDung - diemTichLuy;
                        if (diemMoi < 0) diemMoi = 0; // Đảm bảo không âm
                        
                        khachHangBUS.UpdateDiemTichLuy(hoaDon.MaKhachHang.Value, diemMoi);
                    }
                }

                // 5.3. Cập nhật trạng thái hóa đơn thành "Đã hủy" (thực hiện cuối cùng)
                _hoaDonDao.HuyHoaDon(hoaDon);

                // 5.4. Lưu lịch sử hủy hóa đơn
                _hoaDonDao.LuuLichSuHuyHoaDon(maHoaDon, lyDoHuy, maNhanVienHuy);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, throw để caller xử lý
                throw new InvalidOperationException($"Lỗi khi hủy hóa đơn: {ex.Message}", ex);
            }
        }

        public (string? LyDoHuy, int? MaNhanVienHuy, DateTime? NgayHuy, string? TenNhanVienHuy) GetThongTinHuyHoaDon(int maHoaDon)
        {
            return _hoaDonDao.GetThongTinHuyHoaDon(maHoaDon);
        }
    }
}