using mini_supermarket.DAO;
using mini_supermarket.DTO;
using System;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Linq;

namespace mini_supermarket.BUS
{
    public class KhoHangBUS
    {
        private KhoHangDAO khoHangDAO = new KhoHangDAO();

        // Hằng số định nghĩa trạng thái và ngưỡng cảnh báo
        private const int NGUONG_CANH_BAO = 10; // Ngưỡng cảnh báo hàng sắp hết
        private const int NGUONG_TIEM_CAN = 5;  // Ngưỡng cận - rất sắp hết

        public const string TRANG_THAI_CON_HANG = "Còn hàng";
        public const string TRANG_THAI_HET_HANG = "Hết hàng";
        public const string TRANG_THAI_SAP_HET = "Cảnh báo - Sắp hết hàng";
        public const string TRANG_THAI_TIEM_CAN = "Cảnh báo - Tiệm cận";

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

        // Phương thức helper: Xác định trạng thái kho hàng dựa trên số lượng
        private string XacDinhTrangThaiKho(int soLuong)
        {
            if (soLuong <= 0)
                return TRANG_THAI_HET_HANG;
            else if (soLuong <= NGUONG_TIEM_CAN)
                return TRANG_THAI_TIEM_CAN;
            else if (soLuong <= NGUONG_CANH_BAO)
                return TRANG_THAI_SAP_HET;
            else
                return TRANG_THAI_CON_HANG;
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

            // Tự động cập nhật trạng thái dựa trên số lượng
            khoHang.TrangThai = XacDinhTrangThaiKho(khoHang.SoLuong ?? 0);

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

            // Tự động xác định trạng thái
            khoHang.TrangThai = XacDinhTrangThaiKho(khoHang.SoLuong ?? 0);

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

            // Tự động xác định trạng thái dựa trên số lượng mới
            khoHang.TrangThai = XacDinhTrangThaiKho(khoHang.SoLuong ?? 0);

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

        // Kiểm tra tồn kho có đủ trước khi bán
        public bool KiemTraTonKhoDu(int maSanPham, int soLuongCan)
        {
            if (maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            if (soLuongCan <= 0)
                throw new ArgumentException("Số lượng cần phải lớn hơn 0");

            var khoHang = khoHangDAO.GetByMaSanPham(maSanPham);
            
            if (khoHang == null)
                return false;

            int soLuongHienTai = khoHang.SoLuong ?? 0;
            return soLuongHienTai >= soLuongCan;
        }

        // Giảm số lượng kho khi bán hàng - ĐÃ SỬA LẠI ĐỂ AN TOÀN HƠN
        public bool GiamSoLuongKho(int maSanPham, int soLuongGiam, int maNhanVien)
        {
            if (maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            if (soLuongGiam <= 0)
                throw new ArgumentException("Số lượng giảm phải lớn hơn 0");

            if (maNhanVien <= 0)
                throw new ArgumentException("Mã nhân viên không hợp lệ");

            // Lấy thông tin kho hiện tại để lấy số lượng cũ
            var khoHangHienTai = khoHangDAO.GetByMaSanPham(maSanPham);
            if (khoHangHienTai == null)
            {
                throw new InvalidOperationException("Sản phẩm không tồn tại trong kho.");
            }

            int soLuongCu = khoHangHienTai.SoLuong ?? 0;
            if (soLuongCu < soLuongGiam)
            {
                throw new InvalidOperationException($"Số lượng trong kho không đủ. Hiện có: {soLuongCu}, cần: {soLuongGiam}");
            }

            int soLuongMoi = soLuongCu - soLuongGiam;
            string trangThaiMoi = XacDinhTrangThaiKho(soLuongMoi);

            // Tạo lịch sử thay đổi
            LichSuThayDoiKhoDTO lichSu = new LichSuThayDoiKhoDTO
            {
                MaSanPham = maSanPham,
                SoLuongCu = soLuongCu,
                SoLuongMoi = soLuongMoi,
                ChenhLech = -soLuongGiam,
                LoaiThayDoi = "Bán hàng",
                LyDo = "Xuất kho do bán hàng",
                GhiChu = $"Giảm {soLuongGiam} sản phẩm. Trạng thái: {trangThaiMoi}",
                MaNhanVien = maNhanVien,
                NgayThayDoi = DateTime.Now
            };

            // Sử dụng transaction để đảm bảo tính toàn vẹn
            return khoHangDAO.GiamSoLuongVaGhiLog(maSanPham, soLuongGiam, lichSu);
        }

        // Tăng số lượng kho khi nhập hàng - GHI LOG CHI TIẾT
        public bool TangSoLuongKho(int maSanPham, int soLuongTang, int maNhanVien, 
            string lyDo = "Nhập kho từ nhà cung cấp", string ghiChu = "")
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
                TrangThai = XacDinhTrangThaiKho(soLuongMoi)
            };

            // Tạo lịch sử thay đổi - GHI LOG CHI TIẾT
            LichSuThayDoiKhoDTO lichSu = new LichSuThayDoiKhoDTO
            {
                MaSanPham = maSanPham,
                SoLuongCu = soLuongCu,
                SoLuongMoi = soLuongMoi,
                ChenhLech = soLuongTang,
                LoaiThayDoi = "Nhập hàng",
                LyDo = lyDo,
                GhiChu = $"Tăng {soLuongTang} sản phẩm. {ghiChu}. Trạng thái: {khoHangCapNhat.TrangThai}",
                MaNhanVien = maNhanVien,
                NgayThayDoi = DateTime.Now
            };

            // Nếu chưa có trong kho thì thêm mới, ngược lại cập nhật
            if (khoHangHienTai == null)
            {
                khoHangDAO.InsertKhoHang(khoHangCapNhat);
                // Vẫn ghi log cho lần thêm mới
                return khoHangDAO.CapNhatKhoVaGhiLog(khoHangCapNhat, lichSu);
            }
            else
            {
                return khoHangDAO.CapNhatKhoVaGhiLog(khoHangCapNhat, lichSu);
            }
        }

        // Lấy danh sách sản phẩm sắp hết hàng
        public DataTable LaySanPhamSapHetHang()
        {
            return khoHangDAO.LaySanPhamSapHetHang();
        }

        // Lấy danh sách sản phẩm hết hàng
        public DataTable LaySanPhamHetHang()
        {
            return khoHangDAO.LaySanPhamHetHang();
        }

        /// <summary>
        /// Nhập kho hàng loạt từ file Excel.
        /// </summary>
        public void NhapKhoTuExcel(string filePath, int maNhanVien)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Đường dẫn file không hợp lệ.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Không tìm thấy file.", filePath);

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new InvalidOperationException("File Excel không có worksheet nào.");

                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    // Bỏ qua nếu không có mã sản phẩm
                    if (worksheet.Cells[row, 1].Value == null || !int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out int maSp))
                        continue;

                    // Lấy số lượng mới, nếu không có thì bỏ qua dòng này
                    if (worksheet.Cells[row, 6].Value == null || !int.TryParse(worksheet.Cells[row, 6].Value.ToString(), out int soLuongMoi))
                        continue;

                    var khoHienTai = khoHangDAO.GetByMaSanPham(maSp);
                    int soLuongCu = khoHienTai?.SoLuong ?? 0;

                    // Tạo DTO để cập nhật
                    KhoHangDTO khoUpdate = new KhoHangDTO
                    {
                        MaSanPham = maSp,
                        SoLuong = soLuongMoi,
                        TrangThai = XacDinhTrangThaiKho(soLuongMoi)
                    };

                    // Tạo DTO cho lịch sử
                    LichSuThayDoiKhoDTO lichSu = new LichSuThayDoiKhoDTO
                    {
                        MaSanPham = maSp,
                        SoLuongCu = soLuongCu,
                        SoLuongMoi = soLuongMoi,
                        ChenhLech = soLuongMoi - soLuongCu,
                        LoaiThayDoi = khoHienTai == null ? "Khởi tạo" : "Điều chỉnh",
                        LyDo = "Nhập từ file Excel",
                        GhiChu = "Cập nhật hàng loạt từ Excel.",
                        MaNhanVien = maNhanVien,
                        NgayThayDoi = DateTime.Now
                    };

                    // Gọi phương thức upsert an toàn
                    khoHangDAO.CapNhatKhoVaGhiLog(khoUpdate, lichSu);
                }
            }
        }
    }
}

