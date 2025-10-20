using System;
using System.Collections.Generic;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class PhieuNhap_BUS
    {
        private readonly PhieuNhap_DAO _phieuNhapDao = new();
        private readonly KhoHang_DAO _khoHangDao = new();

        /*  public int GetNextMaPhieuNhap()
         {
             int maxId = _phieuNhapDao.GetMaxMaPhieuNhap();
             return maxId + 1;
         } */

        public IList<PhieuNhapDTO> GetPhieuNhap()
        {
            return _phieuNhapDao.GetPhieuNhap();
        }

        public PhieuNhapDTO? GetPhieuNhapById(int maPhieuNhap)
        {
            if (maPhieuNhap <= 0)
                throw new ArgumentException("Mã phiếu nhập không hợp lệ.", nameof(maPhieuNhap));

            return _phieuNhapDao.GetPhieuNhapById(maPhieuNhap);
        }

        public PhieuNhapDTO AddPhieuNhap(PhieuNhapDTO phieuNhap)
        {
            if (phieuNhap == null)
                throw new ArgumentNullException(nameof(phieuNhap));

            ValidatePhieuNhap(phieuNhap, isUpdate: false);

            try
            {
                // 1. Thêm phiếu nhập vào database
                int newId = _phieuNhapDao.InsertPhieuNhap(phieuNhap);
                phieuNhap.MaPhieuNhap = newId;
                
                // 2. Cập nhật số lượng trong kho hàng cho từng chi tiết phiếu nhập
                if (phieuNhap.ChiTietPhieuNhaps != null && phieuNhap.ChiTietPhieuNhaps.Count > 0)
                {
                    foreach (var chiTiet in phieuNhap.ChiTietPhieuNhaps)
                    {
                        // Kiểm tra sản phẩm đã có trong kho hàng chưa
                        bool exists = _khoHangDao.ExistsByMaSanPham(chiTiet.MaSanPham);
                        
                        if (exists)
                        {
                            // Đã có trong kho -> Lấy thông tin hiện tại và cộng thêm số lượng
                            var khoHangHienTai = _khoHangDao.GetByMaSanPham(chiTiet.MaSanPham);
                            if (khoHangHienTai != null)
                            {
                                int soLuongMoi = (khoHangHienTai.SoLuong ?? 0) + chiTiet.SoLuong;
                                
                                KhoHangDTO khoHangUpdate = new KhoHangDTO
                                {
                                    MaSanPham = chiTiet.MaSanPham,
                                    SoLuong = soLuongMoi,
                                    TrangThai = soLuongMoi > 0 ? "Còn hàng" : "Hết hàng"
                                };
                                _khoHangDao.UpdateKhoHang(khoHangUpdate);
                            }
                        }
                        else
                        {
                            // Chưa có trong kho -> Thêm mới
                            KhoHangDTO khoHangNew = new KhoHangDTO
                            {
                                MaSanPham = chiTiet.MaSanPham,
                                SoLuong = chiTiet.SoLuong,
                                TrangThai = chiTiet.SoLuong > 0 ? "Còn hàng" : "Hết hàng"
                            };
                            _khoHangDao.InsertKhoHang(khoHangNew);
                        }
                    }
                }
                
                return phieuNhap;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm phiếu nhập: {ex.Message}", ex);
            }
        }

        public void UpdatePhieuNhap(PhieuNhapDTO phieuNhap)
        {
            if (phieuNhap == null)
                throw new ArgumentNullException(nameof(phieuNhap));

            ValidatePhieuNhap(phieuNhap, isUpdate: true);
            _phieuNhapDao.UpdatePhieuNhap(phieuNhap);
        }

        public void DeletePhieuNhap(int maPhieuNhap)
        {
            if (maPhieuNhap <= 0)
                throw new ArgumentException("Mã phiếu nhập không hợp lệ.", nameof(maPhieuNhap));

            _phieuNhapDao.DeletePhieuNhap(maPhieuNhap);
        }

        // public IList<PhieuNhapDTO> SearchPhieuNhap(string keyword)
        // {
        //     return _phieuNhapDao.SearchPhieuNhap(keyword?.Trim() ?? string.Empty);
        // }

        private static void ValidatePhieuNhap(PhieuNhapDTO phieuNhap, bool isUpdate)
        {
            if (isUpdate && phieuNhap.MaPhieuNhap <= 0)
                throw new ArgumentException("Mã phiếu nhập không hợp lệ.", nameof(phieuNhap.MaPhieuNhap));

            if (string.IsNullOrEmpty(phieuNhap.MaNhaCungCap.ToString()))
            {
                throw new ArgumentException("Mã nhà cung cấp không được để trống.");
            }
            if (phieuNhap.MaNhaCungCap <= 0)
                throw new ArgumentException("Nhà cung cấp không được để trống.", nameof(phieuNhap.MaNhaCungCap));

            if (phieuNhap.NgayNhap.HasValue && phieuNhap.NgayNhap.Value.Date > DateTime.Today)
                throw new ArgumentException("Ngày nhập không hợp lệ.", nameof(phieuNhap.NgayNhap));

            if (phieuNhap.TongTien < 0)
                throw new ArgumentException("Tổng tiền không được nhỏ hơn 0.", nameof(phieuNhap.TongTien));
        }
    }
}
