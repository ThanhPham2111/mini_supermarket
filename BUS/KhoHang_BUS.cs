using mini_supermarket.DAO;
using mini_supermarket.DTO;
using System;
using System.Data;

namespace mini_supermarket.BUS
{
    public class KhoHangBUS
    {
        private KhoHangDAO khoHangDAO = new KhoHangDAO();

        // Lấy danh sách tồn kho
        public DataTable LayDanhSachTonKho()
        {
            return khoHangDAO.LayDanhSachTonKho();
        }

        // Lấy danh sách loại sản phẩm
        public DataTable LayDanhSachLoai()
        {
            return khoHangDAO.LayDanhSachLoai();
        }

        // Lấy danh sách thương hiệu
        public DataTable LayDanhSachThuongHieu()
        {
            return khoHangDAO.LayDanhSachThuongHieu();
        }

        // Lấy danh sách sản phẩm cho bán hàng
        public DataTable LayDanhSachSanPhamBanHang()
        {
            return khoHangDAO.LayDanhSachSanPhamBanHang();
        }

        // Kiểm tra sản phẩm có tồn tại trong kho không
        public bool KiemTraTonTai(int maSanPham)
        {
            return khoHangDAO.ExistsByMaSanPham(maSanPham);
        }

        // Lấy thông tin kho hàng theo mã sản phẩm
        public KhoHangDTO? GetByMaSanPham(int maSanPham)
        {
            return khoHangDAO.GetByMaSanPham(maSanPham);
        }

        // Cập nhật số lượng kho (không ghi log)
        public void CapNhatKhoHang(KhoHangDTO khoHang)
        {
            if (khoHang == null)
                throw new ArgumentNullException(nameof(khoHang));

            if (khoHang.MaSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            if (khoHang.SoLuong < 0)
                throw new ArgumentException("Số lượng không được âm");

            khoHangDAO.UpdateKhoHang(khoHang);
        }

        // Thêm mới sản phẩm vào kho
        public void ThemSanPhamVaoKho(KhoHangDTO khoHang)
        {
            if (khoHang == null)
                throw new ArgumentNullException(nameof(khoHang));

            if (khoHang.MaSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            if (khoHang.SoLuong < 0)
                throw new ArgumentException("Số lượng không được âm");

            khoHangDAO.InsertKhoHang(khoHang);
        }

        // Cập nhật số lượng kho có ghi log lịch sử
        public bool CapNhatSoLuongKho(KhoHangDTO khoHang, LichSuThayDoiKhoDTO lichSu)
        {
            if (khoHang == null)
                throw new ArgumentNullException(nameof(khoHang));

            if (lichSu == null)
                throw new ArgumentNullException(nameof(lichSu));

            if (khoHang.MaSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            if (khoHang.SoLuong < 0)
                throw new ArgumentException("Số lượng không được âm");

            if (string.IsNullOrWhiteSpace(lichSu.LoaiThayDoi))
                throw new ArgumentException("Loại thay đổi không được để trống");

            if (lichSu.MaNhanVien <= 0)
                throw new ArgumentException("Mã nhân viên không hợp lệ");

            return khoHangDAO.CapNhatKhoVaGhiLog(khoHang, lichSu);
        }

        // Lấy lịch sử thay đổi của sản phẩm
        public DataTable LayLichSuThayDoi(int maSanPham)
        {
            if (maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            return khoHangDAO.LayLichSuThayDoi(maSanPham);
        }

        // Lấy thông tin chi tiết sản phẩm
        public DataTable LayThongTinSanPhamChiTiet(int maSanPham)
        {
            if (maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            return khoHangDAO.LayThongTinSanPhamChiTiet(maSanPham);
        }

        // Giảm số lượng kho khi bán hàng
        public bool GiamSoLuongKho(int maSanPham, int soLuongGiam, int maNhanVien)
        {
            if (maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            if (soLuongGiam <= 0)
                throw new ArgumentException("Số lượng giảm phải lớn hơn 0");

            if (maNhanVien <= 0)
                throw new ArgumentException("Mã nhân viên không hợp lệ");

            // Lấy thông tin kho hiện tại
            var khoHangHienTai = khoHangDAO.GetByMaSanPham(maSanPham);
            
            if (khoHangHienTai == null)
                throw new Exception("Sản phẩm không tồn tại trong kho");

            int soLuongCu = khoHangHienTai.SoLuong ?? 0;
            
            if (soLuongCu < soLuongGiam)
                throw new Exception($"Số lượng trong kho không đủ. Hiện có: {soLuongCu}, cần: {soLuongGiam}");

            int soLuongMoi = soLuongCu - soLuongGiam;

            // Tạo DTO cập nhật kho
            KhoHangDTO khoHangCapNhat = new KhoHangDTO
            {
                MaSanPham = maSanPham,
                SoLuong = soLuongMoi,
                TrangThai = soLuongMoi > 0 ? "Còn hàng" : "Hết hàng"
            };

            // Tạo lịch sử thay đổi
            LichSuThayDoiKhoDTO lichSu = new LichSuThayDoiKhoDTO
            {
                MaSanPham = maSanPham,
                SoLuongCu = soLuongCu,
                SoLuongMoi = soLuongMoi,
                LoaiThayDoi = "Bán hàng",
                LyDo = "Xuất kho do bán hàng",
                GhiChu = $"Giảm {soLuongGiam} sản phẩm",
                MaNhanVien = maNhanVien
            };

            return khoHangDAO.CapNhatKhoVaGhiLog(khoHangCapNhat, lichSu);
        }

        // Tăng số lượng kho khi nhập hàng
        public bool TangSoLuongKho(int maSanPham, int soLuongTang, int maNhanVien)
        {
            if (maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            if (soLuongTang <= 0)
                throw new ArgumentException("Số lượng tăng phải lớn hơn 0");

            if (maNhanVien <= 0)
                throw new ArgumentException("Mã nhân viên không hợp lệ");

            // Lấy thông tin kho hiện tại
            var khoHangHienTai = khoHangDAO.GetByMaSanPham(maSanPham);
            
            int soLuongCu = khoHangHienTai?.SoLuong ?? 0;
            int soLuongMoi = soLuongCu + soLuongTang;

            // Tạo DTO cập nhật kho
            KhoHangDTO khoHangCapNhat = new KhoHangDTO
            {
                MaSanPham = maSanPham,
                SoLuong = soLuongMoi,
                TrangThai = soLuongMoi > 0 ? "Còn hàng" : "Hết hàng"
            };

            // Tạo lịch sử thay đổi
            LichSuThayDoiKhoDTO lichSu = new LichSuThayDoiKhoDTO
            {
                MaSanPham = maSanPham,
                SoLuongCu = soLuongCu,
                SoLuongMoi = soLuongMoi,
                LoaiThayDoi = "Nhập hàng",
                LyDo = "Nhập kho từ nhà cung cấp",
                GhiChu = $"Tăng {soLuongTang} sản phẩm",
                MaNhanVien = maNhanVien
            };

            // Nếu chưa có trong kho thì thêm mới, ngược lại cập nhật
            if (khoHangHienTai == null)
            {
                khoHangDAO.InsertKhoHang(khoHangCapNhat);
                return true;
            }
            else
            {
                return khoHangDAO.CapNhatKhoVaGhiLog(khoHangCapNhat, lichSu);
            }
        }
    }
}

