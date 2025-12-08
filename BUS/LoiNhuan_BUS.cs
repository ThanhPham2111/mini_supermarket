using System;
using System.Collections.Generic;
using System.Linq;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class LoiNhuan_BUS
    {
        private readonly CauHinhLoiNhuan_DAO _cauHinhDao = new CauHinhLoiNhuan_DAO();
        private readonly QuyTacLoiNhuan_DAO _quyTacDao = new QuyTacLoiNhuan_DAO();
        // GiaSanPham_DAO đã được xóa vì không còn cần thiết
        private readonly SanPham_BUS _sanPhamBus = new SanPham_BUS();
        private readonly KhoHangBUS _khoHangBus = new KhoHangBUS();

        // Lấy cấu hình lợi nhuận mặc định
        public CauHinhLoiNhuanDTO? GetCauHinh()
        {
            return _cauHinhDao.GetCauHinh();
        }

        // Cập nhật cấu hình lợi nhuận mặc định
        public void UpdateCauHinh(decimal phanTramLoiNhuan, int maNhanVien)
        {
            UpdateCauHinh(phanTramLoiNhuan, maNhanVien, out _);
        }

        // Overload trả về kết quả chi tiết
        public KetQuaApDungLoiNhuan UpdateCauHinh(decimal phanTramLoiNhuan, int maNhanVien, out KetQuaApDungLoiNhuan ketQua)
        {
            if (phanTramLoiNhuan < 0 || phanTramLoiNhuan > 100)
                throw new ArgumentException("Phần trăm lợi nhuận phải từ 0 đến 100.");

            // Cập nhật cấu hình vào database trước
            _cauHinhDao.UpdateCauHinh(phanTramLoiNhuan, maNhanVien);
            
            // Lưu ý: Không cần cập nhật giá bán khi % lợi nhuận thay đổi
            // Vì giá bán được tính động từ giá nhập + % lợi nhuận khi hiển thị
            // Chỉ cần đảm bảo giá nhập được cập nhật đúng
            ketQua = new KetQuaApDungLoiNhuan();
            return ketQua;
        }

        // Cập nhật cấu hình lợi nhuận mặc định và tất cả quy tắc riêng theo tỷ lệ
        // updateQuyTacRieng: true = cập nhật cả quy tắc riêng theo tỷ lệ, false = chỉ cập nhật % mặc định
        public void UpdateCauHinhVaQuyTac(decimal phanTramLoiNhuanMoi, int maNhanVien, bool updateQuyTacRieng = false)
        {
            if (phanTramLoiNhuanMoi < 0 || phanTramLoiNhuanMoi > 100)
                throw new ArgumentException("Phần trăm lợi nhuận phải từ 0 đến 100.");

            // 1. Lấy % mặc định hiện tại
            var cauHinhCu = _cauHinhDao.GetCauHinh();
            decimal phanTramCu = cauHinhCu?.PhanTramLoiNhuanMacDinh ?? 15.00m;

            // 2. Cập nhật % mặc định mới
            _cauHinhDao.UpdateCauHinh(phanTramLoiNhuanMoi, maNhanVien);

            // 3. Nếu cần, cập nhật TẤT CẢ quy tắc riêng theo tỷ lệ tương ứng
            if (updateQuyTacRieng && phanTramCu > 0)
            {
                decimal tyLe = phanTramLoiNhuanMoi / phanTramCu;
                
                var tatCaQuyTac = _quyTacDao.GetQuyTacLoiNhuan();
                foreach (var quyTac in tatCaQuyTac)
                {
                    // Chỉ cập nhật quy tắc TheoSanPham (không còn quy tắc "Chung" nữa)
                    if (quyTac.LoaiQuyTac == "TheoSanPham")
                    {
                        decimal phanTramMoi = Math.Round(quyTac.PhanTramLoiNhuan * tyLe, 2, MidpointRounding.AwayFromZero);
                        
                        // Đảm bảo % mới không vượt quá giới hạn
                        if (phanTramMoi > 100) phanTramMoi = 100;
                        if (phanTramMoi < 0) phanTramMoi = 0;

                        quyTac.PhanTramLoiNhuan = phanTramMoi;
                        quyTac.MaNhanVien = maNhanVien;
                        _quyTacDao.UpdateQuyTac(quyTac);
                    }
                }
            }

            // 4. Lưu ý: Không cần cập nhật giá bán khi % lợi nhuận thay đổi
            // Vì giá bán được tính động từ giá nhập + % lợi nhuận khi hiển thị
        }

        // Lấy danh sách quy tắc lợi nhuận
        public IList<QuyTacLoiNhuanDTO> GetQuyTacLoiNhuan(string? loaiQuyTac = null)
        {
            return _quyTacDao.GetQuyTacLoiNhuan(loaiQuyTac);
        }

        // Thêm quy tắc lợi nhuận
        public int AddQuyTac(QuyTacLoiNhuanDTO quyTac)
        {
            if (quyTac == null)
                throw new ArgumentNullException(nameof(quyTac));

            ValidateQuyTac(quyTac);

            // Chỉ cho phép TheoSanPham, không còn "Chung" nữa
            if (quyTac.LoaiQuyTac != "TheoSanPham")
                throw new ArgumentException("Chỉ hỗ trợ quy tắc 'TheoSanPham'. % mặc định lấy từ cấu hình chung.");

            // Set độ ưu tiên (không cần nữa nhưng giữ lại để tương thích)
            quyTac.UuTien = 1;

            quyTac.TrangThai = "Hoạt động";
            quyTac.NgayTao = DateTime.Now;
            quyTac.NgayCapNhat = DateTime.Now;

            return _quyTacDao.InsertQuyTac(quyTac);
        }

        // Cập nhật quy tắc lợi nhuận
        public void UpdateQuyTac(QuyTacLoiNhuanDTO quyTac)
        {
            if (quyTac == null)
                throw new ArgumentNullException(nameof(quyTac));

            ValidateQuyTac(quyTac);
            quyTac.NgayCapNhat = DateTime.Now;

            _quyTacDao.UpdateQuyTac(quyTac);
            
            // Tự động tính lại giá bán cho các sản phẩm bị ảnh hưởng
            switch (quyTac.LoaiQuyTac)
            {
                case "TheoSanPham":
                    if (quyTac.MaSanPham.HasValue)
                    {
                        decimal? giaNhap = _khoHangBus.GetGiaNhapMoiNhat(quyTac.MaSanPham.Value);
                        if (giaNhap.HasValue)
                            CapNhatGiaBanKhiGiaNhapThayDoi(quyTac.MaSanPham.Value, giaNhap.Value);
                    }
                    break;
                default:
                    throw new ArgumentException($"Không hỗ trợ loại quy tắc: {quyTac.LoaiQuyTac}. Chỉ hỗ trợ 'TheoSanPham'.");
            }
        }

        // Xóa quy tắc (soft delete)
        public void DeleteQuyTac(int maQuyTac)
        {
            _quyTacDao.DeleteQuyTac(maQuyTac);
        }

        // Lấy quy tắc áp dụng cho sản phẩm (theo thứ tự ưu tiên)
        // Chỉ tìm TheoSanPham hoặc Chung
        public QuyTacLoiNhuanDTO? GetQuyTacApDungChoSanPham(int maSanPham)
        {
            var sanPham = _sanPhamBus.GetSanPhamById(maSanPham);
            if (sanPham == null)
                return null;

            return _quyTacDao.GetQuyTacApDungChoSanPham(maSanPham);
        }

        // Tính giá bán dựa trên giá nhập và quy tắc lợi nhuận
        public decimal TinhGiaBan(decimal giaNhap, int maSanPham, int maLoai, int maThuongHieu, int maDonVi)
        {
            // Lấy quy tắc áp dụng (chỉ tìm TheoSanPham hoặc Chung)
            var quyTac = _quyTacDao.GetQuyTacApDungChoSanPham(maSanPham);
            
            decimal phanTramLoiNhuan;
            if (quyTac != null)
            {
                phanTramLoiNhuan = quyTac.PhanTramLoiNhuan;
            }
            else
            {
                // Dùng cấu hình mặc định
                var cauHinh = _cauHinhDao.GetCauHinh();
                phanTramLoiNhuan = cauHinh?.PhanTramLoiNhuanMacDinh ?? 10.00m;
            }

            // Tính giá bán = giá nhập * (1 + % lợi nhuận / 100)
            return giaNhap * (1 + phanTramLoiNhuan / 100);
        }

        // Áp dụng lợi nhuận cho toàn bộ kho hàng
        // Lưu ý: Giờ chỉ cập nhật giá nhập vào Tbl_SanPham.GiaBan, không tính giá bán nữa
        // Giá bán được tính động từ giá nhập + % lợi nhuận khi hiển thị
        // forceUpdate: true = luôn cập nhật giá nhập (khi % lợi nhuận thay đổi - không dùng nữa)
        //              false = chỉ cập nhật khi giá nhập tăng (khi nhập hàng)
        public void ApDungLoiNhuanChoToanBoKho(int maNhanVien, bool forceUpdate = false)
        {
            ApDungLoiNhuanChoToanBoKho(maNhanVien, forceUpdate, out _);
        }

        // Overload method trả về kết quả chi tiết
        // Lưu ý: Method này giờ chỉ cập nhật giá nhập vào Tbl_SanPham.GiaBan
        // Giá bán được tính động, không lưu vào DB
        public KetQuaApDungLoiNhuan ApDungLoiNhuanChoToanBoKho(int maNhanVien, bool forceUpdate, out KetQuaApDungLoiNhuan ketQua)
        {
            ketQua = new KetQuaApDungLoiNhuan();
            
            System.Diagnostics.Debug.WriteLine($"========== [ApDungLoiNhuanChoToanBoKho] START ==========");
            System.Diagnostics.Debug.WriteLine($"[ApDungLoiNhuanChoToanBoKho] forceUpdate={forceUpdate}, maNhanVien={maNhanVien}");
            
            var allSanPham = _sanPhamBus.GetAll();

            System.Diagnostics.Debug.WriteLine($"[ApDungLoiNhuanChoToanBoKho] Total products: {allSanPham.Count}");

            ketQua.TongSanPham = allSanPham.Count;
            int countUpdated = 0;
            int countSkipped = 0;

            foreach (var sanPham in allSanPham)
            {
                try
                {
                    // Lấy giá nhập mới nhất từ ChiTietPhieuNhap
                    decimal? giaNhapMoiNhat = _khoHangBus.GetGiaNhapMoiNhat(sanPham.MaSanPham);
                    
                    if (giaNhapMoiNhat.HasValue && giaNhapMoiNhat.Value > 0)
                    {
                        // Lấy giá nhập hiện tại (từ Tbl_SanPham.GiaBan - giờ là giá nhập)
                        decimal? giaNhapHienTai = sanPham.GiaBan;

                        // Logic: Nếu giá nhập mới >= giá nhập cũ thì cập nhật giá nhập mới
                        // Nếu giá nhập mới < giá nhập cũ thì giữ nguyên giá nhập cũ
                        bool shouldUpdate = !giaNhapHienTai.HasValue || giaNhapMoiNhat.Value >= giaNhapHienTai.Value;

                        if (shouldUpdate)
                        {
                            // Cập nhật giá nhập mới vào Tbl_SanPham.GiaBan
                            // (GiaBan giờ lưu giá nhập)
                            _sanPhamBus.UpdateGiaBan(sanPham.MaSanPham, giaNhapMoiNhat.Value);
                            countUpdated++;
                            
                            System.Diagnostics.Debug.WriteLine($"[Product {sanPham.MaSanPham}] Updated import price: {giaNhapMoiNhat.Value}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"[Product {sanPham.MaSanPham}] Skipped - New import price ({giaNhapMoiNhat.Value}) < current ({giaNhapHienTai ?? 0})");
                            countSkipped++;
                        }
                    }
                    else
                    {
                        // Không có giá nhập, bỏ qua
                        System.Diagnostics.Debug.WriteLine($"[Product {sanPham.MaSanPham}] Skipped - No import price");
                        countSkipped++;
                    }
                }
                catch (Exception ex)
                {
                    // Log lỗi nhưng tiếp tục với sản phẩm khác
                    System.Diagnostics.Debug.WriteLine($"[Product {sanPham.MaSanPham}] ERROR - {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"[Product {sanPham.MaSanPham}] Stack trace: {ex.StackTrace}");
                    countSkipped++;
                }
            }

            // Cập nhật kết quả
            ketQua.SanPhamDuocCapNhat = countUpdated;
            ketQua.SanPhamCoQuyTacRieng = 0; // Không còn phân biệt nữa
            ketQua.SanPhamDuocCapNhatVoiMacDinh = countUpdated;
            ketQua.SanPhamKhongCoGiaNhap = 0;
            ketQua.SanPhamDuocCapNhatBangFallback = 0;

            System.Diagnostics.Debug.WriteLine($"========== [ApDungLoiNhuanChoToanBoKho] SUMMARY ==========");
            System.Diagnostics.Debug.WriteLine($"Total products: {allSanPham.Count}");
            System.Diagnostics.Debug.WriteLine($"Updated (import price): {countUpdated}");
            System.Diagnostics.Debug.WriteLine($"Skipped: {countSkipped}");
            System.Diagnostics.Debug.WriteLine($"========== [ApDungLoiNhuanChoToanBoKho] END ==========");
            
            return ketQua;
        }

        // Cập nhật giá nhập khi nhập hàng mới
        // Được gọi khi xác nhận nhập kho
        // Lưu ý: Tbl_SanPham.GiaBan giờ lưu giá nhập, không phải giá bán
        public void CapNhatGiaBanKhiGiaNhapThayDoi(int maSanPham, decimal giaNhapMoi)
        {
            if (giaNhapMoi < 0)
                throw new ArgumentException("Giá nhập không được âm.");

            var sanPham = _sanPhamBus.GetSanPhamById(maSanPham);
            if (sanPham == null)
                throw new InvalidOperationException($"Không tìm thấy sản phẩm với mã {maSanPham}.");

            // Lấy giá nhập cũ (từ Tbl_SanPham.GiaBan - giờ là giá nhập)
            decimal? giaNhapCu = sanPham.GiaBan;

            // Logic: Nếu giá nhập mới >= giá nhập cũ thì cập nhật giá nhập mới
            // Nếu giá nhập mới < giá nhập cũ thì giữ nguyên giá nhập cũ
            if (!giaNhapCu.HasValue || giaNhapMoi >= giaNhapCu.Value)
            {
                // Cập nhật giá nhập mới vào Tbl_SanPham.GiaBan
                // (GiaBan giờ lưu giá nhập)
                _sanPhamBus.UpdateGiaBan(maSanPham, giaNhapMoi);
            }
            // Nếu giá nhập mới < giá nhập cũ, giữ nguyên giá nhập cũ (không làm gì)
        }

        // Method GetAllGiaSanPham đã được xóa vì bảng Tbl_GiaSanPham không còn tồn tại
        // Giá nhập lấy từ ChiTietPhieuNhap, giá bán lấy từ SanPham

        // Class để trả về kết quả áp dụng lợi nhuận cho toàn bộ kho
        public class KetQuaApDungLoiNhuan
        {
            public int TongSanPham { get; set; }
            public int SanPhamDuocCapNhat { get; set; } // Tổng số sản phẩm được cập nhật (bao gồm cả có và không có quy tắc riêng)
            public int SanPhamDuocCapNhatVoiMacDinh { get; set; } // Sản phẩm được cập nhật với % mặc định (không có quy tắc riêng)
            public int SanPhamCoQuyTacRieng { get; set; } // Sản phẩm có quy tắc riêng (không dùng % mặc định, nhưng vẫn được cập nhật)
            public int SanPhamKhongCoGiaNhap { get; set; }
            public int SanPhamDuocCapNhatBangFallback { get; set; }
        }

        // Lấy tất cả kho hàng với giá (cho GUI sử dụng)
        public IList<KhoHangDTO> GetAllKhoHangWithPrice()
        {
            return _khoHangBus.GetAllKhoHangWithPrice();
        }

        // Cập nhật giá bán cho một sản phẩm cụ thể (cho GUI sử dụng)
        // Lưu ý: Method này chỉ cập nhật giá bán vào Tbl_SanPham, không cập nhật giá nhập
        public void CapNhatGiaBanChoSanPham(int maSanPham, decimal giaBan)
        {
            _sanPhamBus.UpdateGiaBan(maSanPham, giaBan);
        }

        // Lấy giá nhập và giá bán cho một sản phẩm (tính giống như trong quản lý % lợi nhuận)
        // Trả về: (GiaNhap, GiaBan) - giá bán được tính từ giá nhập + % lợi nhuận
        public (decimal GiaNhap, decimal GiaBan) GetGiaNhapVaGiaBan(int maSanPham)
        {
            // Lấy dữ liệu từ GetAllKhoHangWithPrice (giống như quản lý % lợi nhuận)
            var khoHangList = _khoHangBus.GetAllKhoHangWithPrice();
            var khoHang = khoHangList.FirstOrDefault(kh => kh.MaSanPham == maSanPham);
            
            if (khoHang == null)
            {
                return (0, 0);
            }

            // Lấy % lợi nhuận
            var quyTac = GetQuyTacApDungChoSanPham(maSanPham);
            var cauHinh = _cauHinhDao.GetCauHinh();
            decimal phanTramMacDinh = cauHinh?.PhanTramLoiNhuanMacDinh ?? 15.00m;
            decimal phanTram = quyTac?.PhanTramLoiNhuan ?? phanTramMacDinh;

            decimal giaNhap = khoHang.GiaNhap ?? 0;
            decimal giaBan;

            if (giaNhap > 0)
            {
                // Có giá nhập → tính giá bán từ giá nhập và % lợi nhuận (làm tròn đến 2 chữ số thập phân)
                giaBan = Math.Round(giaNhap * (1 + phanTram / 100), 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                // Chưa có giá nhập, lấy từ Tbl_SanPham.GiaBan (giờ là giá nhập)
                var sanPham = _sanPhamBus.GetSanPhamById(maSanPham);
                if (sanPham != null && sanPham.GiaBan.HasValue && sanPham.GiaBan.Value > 0)
                {
                    giaNhap = sanPham.GiaBan.Value;
                    giaBan = Math.Round(giaNhap * (1 + phanTram / 100), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    giaBan = 0;
                }
            }

            return (giaNhap, giaBan);
        }

        private static void ValidateQuyTac(QuyTacLoiNhuanDTO quyTac)
        {
            if (string.IsNullOrWhiteSpace(quyTac.LoaiQuyTac))
                throw new ArgumentException("Loại quy tắc không được để trống.");

            if (quyTac.PhanTramLoiNhuan < 0 || quyTac.PhanTramLoiNhuan > 100)
                throw new ArgumentException("Phần trăm lợi nhuận phải từ 0 đến 100.");

            // Validate các trường tham chiếu dựa trên loại quy tắc
            switch (quyTac.LoaiQuyTac)
            {
                case "TheoSanPham":
                    if (!quyTac.MaSanPham.HasValue)
                        throw new ArgumentException("Mã sản phẩm không được để trống khi chọn quy tắc theo sản phẩm.");
                    break;
                default:
                    throw new ArgumentException($"Không hỗ trợ loại quy tắc: {quyTac.LoaiQuyTac}. Chỉ hỗ trợ 'TheoSanPham'. % mặc định lấy từ cấu hình chung.");
            }
        }
    }
}

