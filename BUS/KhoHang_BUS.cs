using mini_supermarket.DAO;
using mini_supermarket.DTO;
using System;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace mini_supermarket.BUS
{
    public class KhoHangBUS
    {
        private KhoHangDAO khoHangDAO = new KhoHangDAO();
        private LichSuThayDoiKho_BUS lichSuBUS = new LichSuThayDoiKho_BUS();

        // Hằng số định nghĩa trạng thái và ngưỡng cảnh báo
        private const int NGUONG_CANH_BAO = 10; // Ngưỡng cảnh báo hàng sắp hết
        private const int NGUONG_TIEM_CAN = 5;  // Ngưỡng cận - rất sắp hết

        public const string TRANG_THAI_CON_HANG = "Còn hàng";
        public const string TRANG_THAI_HET_HANG = "Hết hàng";
        public const string TRANG_THAI_SAP_HET = "Cảnh báo - Sắp hết hàng";
        public const string TRANG_THAI_TIEM_CAN = "Cảnh báo - Tiệm cận";

        // Hằng số trạng thái điều kiện bán
        public const string TRANG_THAI_DIEU_KIEN_BAN = "Bán";
        public const string TRANG_THAI_DIEU_KIEN_KHONG_BAN = "Không bán";

        // Lấy danh sách tồn kho
        public IList<TonKhoDTO> LayDanhSachTonKho()
        {
            return khoHangDAO.LayDanhSachTonKho();
        }

        // Lấy danh sách loại sản phẩm
        public IList<LoaiDTO> LayDanhSachLoai()
        {
            return khoHangDAO.LayDanhSachLoai();
        }

        // Lấy danh sách thương hiệu
        public IList<ThuongHieuDTO> LayDanhSachThuongHieu()
        {
            return khoHangDAO.LayDanhSachThuongHieu();
        }

        // Lấy danh sách sản phẩm cho bán hàng
        public IList<SanPhamBanHangDTO> LayDanhSachSanPhamBanHang()
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

            // Tự động chuyển trạng thái bán thành "Không bán" khi số lượng = 0
            // Nếu số lượng > 0, giữ nguyên trạng thái điều kiện hiện tại (cho phép set "Không bán" thủ công)
            if ((khoHang.SoLuong ?? 0) == 0)
            {
                khoHang.TrangThaiDieuKien = TRANG_THAI_DIEU_KIEN_KHONG_BAN;
            }
            // Nếu số lượng > 0 và chưa có trạng thái điều kiện, mặc định là "Bán"
            else if (string.IsNullOrWhiteSpace(khoHang.TrangThaiDieuKien))
            {
                khoHang.TrangThaiDieuKien = TRANG_THAI_DIEU_KIEN_BAN;
            }

            return khoHangDAO.CapNhatKhoVaGhiLog(khoHang, lichSu);
        }

        // Lấy lịch sử thay đổi của sản phẩm
        public IList<LichSuThayDoiKhoDTO> LayLichSuThayDoi(int maSanPham)
        {
            if (maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            return lichSuBUS.LayTheoSanPham(maSanPham);
        }

        // Lấy thông tin chi tiết sản phẩm
        public IList<SanPhamChiTietDTO> LayThongTinSanPhamChiTiet(int maSanPham)
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
        public IList<SanPhamKhoDTO> LaySanPhamSapHetHang()
        {
            return khoHangDAO.LaySanPhamSapHetHang();
        }

        // Lấy danh sách sản phẩm hết hàng
        public IList<SanPhamKhoDTO> LaySanPhamHetHang()
        {
            return khoHangDAO.LaySanPhamHetHang();
        }

        // Cập nhật kho và ghi log (wrapper method cho DAO)
        public bool CapNhatKhoVaGhiLog(KhoHangDTO khoHang, LichSuThayDoiKhoDTO lichSu)
        {
            if (khoHang == null)
                throw new ArgumentNullException(nameof(khoHang));
            if (lichSu == null)
                throw new ArgumentNullException(nameof(lichSu));

            return khoHangDAO.CapNhatKhoVaGhiLog(khoHang, lichSu);
        }

        // Thêm mới kho hàng (wrapper method cho DAO)
        public void InsertKhoHang(KhoHangDTO khoHang)
        {
            if (khoHang == null)
                throw new ArgumentNullException(nameof(khoHang));

            khoHangDAO.InsertKhoHang(khoHang);
        }

        /// <summary>
        /// Nhập kho hàng loạt từ file Excel.
        /// Trả về kết quả nhập kho bao gồm trạng thái thành công, danh sách lỗi và cập nhật.
        /// </summary>
        public (bool HasUpdates, List<string> Errors, List<string> Updates) NhapKhoTuExcel(string filePath, int maNhanVien)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Đường dẫn file không hợp lệ.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Không tìm thấy file.", filePath);

            var errors = new List<string>();
            var updates = new List<string>();
            bool hasUpdates = false;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new InvalidOperationException("File Excel không có worksheet nào.");

                // Tìm vị trí cột theo header
                int colMaSP = -1, colSoLuong = -1;
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    var header = worksheet.Cells[1, col].Value?.ToString()?.Trim();
                    if (header == "Mã SP") colMaSP = col;
                    else if (header == "Số lượng") colSoLuong = col;
                }

                if (colMaSP == -1 || colSoLuong == -1)
                {
                    throw new InvalidOperationException("File Excel không có cột bắt buộc: 'Mã SP' hoặc 'Số lượng'.");
                }

                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    // Đọc Mã SP
                    string maSpText = worksheet.Cells[row, colMaSP].Value?.ToString()?.Trim() ?? "";
                    // Đọc Số lượng
                    string soLuongText = worksheet.Cells[row, colSoLuong].Value?.ToString()?.Trim() ?? "";

                    // Nếu cả hai đều trống, bỏ qua dòng mà không báo lỗi
                    if (string.IsNullOrEmpty(maSpText) && string.IsNullOrEmpty(soLuongText))
                    {
                        continue;
                    }

                    // Nếu mã SP trống nhưng số lượng có, báo lỗi
                    if (string.IsNullOrEmpty(maSpText) && !string.IsNullOrEmpty(soLuongText))
                    {
                        errors.Add($"Dòng {row}: Mã sản phẩm trống nhưng có số lượng.");
                        continue;
                    }

                    // Nếu mã SP có nhưng số lượng trống, bỏ qua
                    if (!string.IsNullOrEmpty(maSpText) && string.IsNullOrEmpty(soLuongText))
                    {
                        continue;
                    }

                    // Validate Mã SP
                    if (!int.TryParse(maSpText, out int maSp))
                    {
                        errors.Add($"Dòng {row}: Mã sản phẩm không phải là số nguyên ('{maSpText}').");
                        continue;
                    }

                    // Validate Số lượng
                    if (!int.TryParse(soLuongText, out int soLuongMoi))
                    {
                        errors.Add($"Dòng {row}: Số lượng không phải là số nguyên ('{soLuongText}').");
                        continue;
                    }
                    if (soLuongMoi < 0)
                    {
                        errors.Add($"Dòng {row}: Số lượng không được âm ({soLuongMoi}).");
                        continue;
                    }
                    if (soLuongMoi == 0)
                    {
                        errors.Add($"Dòng {row}: Số lượng phải lớn hơn 0 ({soLuongMoi}).");
                        continue;
                    }

                    var khoHienTai = khoHangDAO.GetByMaSanPham(maSp);
                    if (khoHienTai == null)
                    {
                        errors.Add($"Dòng {row}: Sản phẩm mã {maSp} không tồn tại.");
                        continue;
                    }

                    int soLuongCu = khoHienTai.SoLuong ?? 0;
                    if (soLuongMoi == soLuongCu)
                    {
                        // Không thay đổi, bỏ qua
                        continue;
                    }

                    int chenhLech = soLuongMoi - soLuongCu;

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
                        ChenhLech = chenhLech,
                        LoaiThayDoi = khoHienTai.SoLuong == null ? "Khởi tạo" : "Điều chỉnh",
                        LyDo = "Nhập từ file Excel",
                        GhiChu = $"Cập nhật hàng loạt từ Excel. Số lượng cũ: {soLuongCu}, mới: {soLuongMoi}, thay đổi: {chenhLech}",
                        MaNhanVien = maNhanVien,
                        NgayThayDoi = DateTime.Now
                    };

                    try
                    {
                        // Gọi phương thức BUS để cập nhật kho và ghi log
                        bool success = CapNhatSoLuongKho(khoUpdate, lichSu);
                        if (success)
                        {
                            updates.Add($"Sản phẩm {maSp}: {(chenhLech > 0 ? "+" : "")}{chenhLech} (cũ: {soLuongCu}, mới: {soLuongMoi})");
                            hasUpdates = true;
                        }
                        else
                        {
                            errors.Add($"Dòng {row}: Không thể cập nhật sản phẩm {maSp}");
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Dòng {row}: Lỗi cập nhật sản phẩm {maSp}: {ex.Message}");
                    }
                }
            }

            return (hasUpdates, errors, updates);
        }

        // Lấy giá nhập mới nhất của sản phẩm từ ChiTietPhieuNhap
        public decimal? GetGiaNhapMoiNhat(int maSanPham)
        {
            return khoHangDAO.GetGiaNhapMoiNhat(maSanPham);
        }

        // Lấy danh sách kho hàng kèm giá nhập và giá bán
        public IList<KhoHangDTO> GetAllKhoHangWithPrice()
        {
            return khoHangDAO.GetAllKhoHangWithPrice();
        }

        /// <summary>
        /// Xuất danh sách tồn kho ra file Excel.
        /// </summary>
        public void XuatDanhSachTonKhoRaExcel(IList<TonKhoDTO> data, string filePath)
        {
            if (data == null || data.Count == 0)
                throw new ArgumentException("Không có dữ liệu để xuất.");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TonKho");
                worksheet.Cells["A1"].LoadFromCollection(data, true);
                worksheet.Cells.AutoFitColumns();
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);
            }
        }

        /// <summary>
        /// Xuất file mẫu nhập kho (chỉ có Mã SP và Tên SP, Số lượng trống).
        /// </summary>
        public void XuatFileMauNhapKho(string filePath)
        {
            var allProducts = LayDanhSachTonKho();
            if (allProducts == null || allProducts.Count == 0)
                throw new InvalidOperationException("Không có sản phẩm nào trong kho để xuất mẫu.");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("MauNhapKho");

                // Header
                worksheet.Cells[1, 1].Value = "Mã SP";
                worksheet.Cells[1, 2].Value = "Tên sản phẩm";
                worksheet.Cells[1, 3].Value = "Số lượng"; // Để trống

                // Dữ liệu: Chỉ điền Mã và Tên, Số lượng để trống
                for (int i = 0; i < allProducts.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = allProducts[i].MaSanPham;
                    worksheet.Cells[i + 2, 2].Value = allProducts[i].TenSanPham;
                    // Cột 3 (Số lượng) để trống
                }

                worksheet.Cells.AutoFitColumns();
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);
            }
        }

        /// <summary>
        /// Cập nhật trạng thái điều kiện bán của sản phẩm (Bán hoặc Không bán).
        /// Phương thức này được sử dụng để ngừng bán một sản phẩm mà vẫn còn hàng.
        /// </summary>
        public bool CapNhatTrangThaiDieuKienBan(int maSanPham, string trangThaiDieuKien)
        {
            if (maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            if (string.IsNullOrWhiteSpace(trangThaiDieuKien))
                throw new ArgumentException("Trạng thái điều kiện không được để trống");

            if (trangThaiDieuKien != TRANG_THAI_DIEU_KIEN_BAN && trangThaiDieuKien != TRANG_THAI_DIEU_KIEN_KHONG_BAN)
                throw new ArgumentException($"Trạng thái điều kiện chỉ được là '{TRANG_THAI_DIEU_KIEN_BAN}' hoặc '{TRANG_THAI_DIEU_KIEN_KHONG_BAN}'");

            var khoHang = khoHangDAO.GetByMaSanPham(maSanPham);
            if (khoHang == null)
                throw new InvalidOperationException("Sản phẩm không tồn tại trong kho");

            khoHang.TrangThaiDieuKien = trangThaiDieuKien;
            khoHangDAO.UpdateKhoHang(khoHang);
            return true;
        }

        /// <summary>
        /// Lấy trạng thái điều kiện bán của sản phẩm.
        /// </summary>
        public string? GetTrangThaiDieuKienBan(int maSanPham)
        {
            if (maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ");

            var khoHang = khoHangDAO.GetByMaSanPham(maSanPham);
            return khoHang?.TrangThaiDieuKien;
        }
    }
}

