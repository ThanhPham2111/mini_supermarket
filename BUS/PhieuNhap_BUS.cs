using System;
using System.Collections.Generic;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class PhieuNhap_BUS
    {
        private readonly PhieuNhap_DAO _phieuNhapDao = new PhieuNhap_DAO();
        private readonly KhoHangBUS _khoHangBus = new KhoHangBUS();

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
                // Đặt trạng thái mặc định là "Đang nhập"
                phieuNhap.TrangThai = "Đang nhập";
                
                // Thêm phiếu nhập vào database
                int newId = _phieuNhapDao.InsertPhieuNhap(phieuNhap);
                phieuNhap.MaPhieuNhap = newId;
                
                // QUAN TRỌNG: KHÔNG cập nhật kho ở đây
                // Chỉ cập nhật kho khi gọi XacNhanNhapKho()
                
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

        public void XacNhanNhapKho(int maPhieuNhap)
        {
            if (maPhieuNhap <= 0)
                throw new ArgumentException("Mã phiếu nhập không hợp lệ.", nameof(maPhieuNhap));

            try
            {
                // 1. Lấy thông tin phiếu nhập
                var phieuNhap = _phieuNhapDao.GetPhieuNhapById(maPhieuNhap);
                
                if (phieuNhap == null)
                    throw new InvalidOperationException("Không tìm thấy phiếu nhập.");
                
                // Kiểm tra trạng thái hiện tại
                if (phieuNhap.TrangThai == "Nhập thành công")
                    throw new InvalidOperationException("Phiếu nhập đã được xác nhận trước đó.");
                
                // 2. Cập nhật trạng thái phiếu nhập
                _phieuNhapDao.UpdateTrangThaiPhieuNhap(maPhieuNhap, "Nhập thành công");
                
                // 3. Cập nhật số lượng vào kho hàng và giá bán vào Tbl_SanPham
                var loiNhuanBus = new LoiNhuan_BUS();
                
                if (phieuNhap.ChiTietPhieuNhaps != null && phieuNhap.ChiTietPhieuNhaps.Count > 0)
                {
                    foreach (var chiTiet in phieuNhap.ChiTietPhieuNhaps)
                    {
                        // Sử dụng KhoHang_BUS.TangSoLuongKho để đảm bảo logic nghiệp vụ
                        bool success = _khoHangBus.TangSoLuongKho(
                            chiTiet.MaSanPham,
                            chiTiet.SoLuong,
                            1, // TODO: Lấy từ session đăng nhập
                            string.Format("Nhập hàng từ phiếu nhập PN{0:D3}", maPhieuNhap),
                            string.Format("Xác nhận nhập kho - Phiếu nhập PN{0:D3}", maPhieuNhap)
                        );
                        
                        if (!success)
                        {
                            throw new InvalidOperationException($"Không thể cập nhật kho cho sản phẩm {chiTiet.MaSanPham}");
                        }
                        
                        // 4. QUAN TRỌNG: Cập nhật giá bán vào Tbl_SanPham với logic giá nhập mới
                        // Giá nhập mới = DonGiaNhap từ chi tiết phiếu nhập
                        loiNhuanBus.CapNhatGiaBanKhiGiaNhapThayDoi(chiTiet.MaSanPham, chiTiet.DonGiaNhap);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xác nhận nhập kho: {ex.Message}", ex);
            }
        }

        public void DeletePhieuNhap(int maPhieuNhap)
        {
            if (maPhieuNhap <= 0)
                throw new ArgumentException("Mã phiếu nhập không hợp lệ.", nameof(maPhieuNhap));

            _phieuNhapDao.DeletePhieuNhap(maPhieuNhap);
        }

        public void HuyPhieuNhap(int maPhieuNhap, string lyDoHuy)
        {
            if (maPhieuNhap <= 0)
                throw new ArgumentException("Mã phiếu nhập không hợp lệ.", nameof(maPhieuNhap));

            if (string.IsNullOrWhiteSpace(lyDoHuy))
                throw new ArgumentException("Lý do hủy không được để trống.", nameof(lyDoHuy));

            try
            {
                // 1. Lấy thông tin phiếu nhập
                var phieuNhap = _phieuNhapDao.GetPhieuNhapById(maPhieuNhap);
                
                if (phieuNhap == null)
                    throw new InvalidOperationException("Không tìm thấy phiếu nhập.");

                // 2. Nếu phiếu nhập đã được xác nhận (Nhập thành công), cần trừ lại số lượng trong kho
                if (phieuNhap.TrangThai == "Nhập thành công")
                {
                    if (phieuNhap.ChiTietPhieuNhaps != null && phieuNhap.ChiTietPhieuNhaps.Count > 0)
                    {
                        foreach (var chiTiet in phieuNhap.ChiTietPhieuNhaps)
                        {
                            // Sử dụng KhoHang_BUS.GiamSoLuongKho để đảm bảo logic nghiệp vụ
                            bool success = _khoHangBus.GiamSoLuongKho(
                                chiTiet.MaSanPham,
                                chiTiet.SoLuong,
                                1 // TODO: Lấy từ session đăng nhập
                            );
                            
                            if (!success)
                            {
                                throw new InvalidOperationException($"Không thể giảm kho cho sản phẩm {chiTiet.MaSanPham}");
                            }
                        }
                    }
                }

                // 3. Cập nhật trạng thái phiếu nhập thành "Hủy"
                _phieuNhapDao.HuyPhieuNhap(maPhieuNhap, lyDoHuy);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi hủy phiếu nhập: {ex.Message}", ex);
            }
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
