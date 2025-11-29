-- =============================================
-- Script cập nhật giá nhập và giá bán cho Tbl_KhoHang
-- Lấy giá nhập từ phiếu nhập gần nhất, tính giá bán dựa trên % lợi nhuận
-- =============================================

USE mini_sp01;
GO

-- Bước 1: Cập nhật giá nhập từ phiếu nhập gần nhất (trạng thái "Nhập thành công")
UPDATE kh
SET kh.GiaNhap = ISNULL((
    SELECT TOP 1 ctpn.DonGiaNhap
    FROM dbo.Tbl_ChiTietPhieuNhap ctpn
    INNER JOIN dbo.Tbl_PhieuNhap pn ON ctpn.MaPhieuNhap = pn.MaPhieuNhap
    WHERE ctpn.MaSanPham = kh.MaSanPham
        AND pn.TrangThai = N'Nhập thành công'
    ORDER BY pn.NgayNhap DESC, ctpn.MaChiTietPhieuNhap DESC
), 0)
FROM dbo.Tbl_KhoHang kh;

PRINT N'Đã cập nhật giá nhập cho ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' sản phẩm.';
GO

-- Bước 2: Tính và cập nhật giá bán dựa trên giá nhập và % lợi nhuận
-- Lấy % lợi nhuận từ quy tắc (ưu tiên: Sản phẩm > Đơn vị > Thương hiệu > Loại > Chung)
UPDATE kh
SET kh.GiaBan = CASE 
    WHEN kh.GiaNhap > 0 THEN
        kh.GiaNhap * (1 + ISNULL((
            -- Tìm quy tắc áp dụng (theo thứ tự ưu tiên)
            SELECT TOP 1 q.PhanTramLoiNhuan
            FROM dbo.Tbl_QuyTacLoiNhuan q
            INNER JOIN dbo.Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham
            WHERE q.TrangThai = N'Hoạt động'
            AND (
                (q.LoaiQuyTac = N'TheoSanPham' AND q.MaSanPham = sp.MaSanPham)
                OR (q.LoaiQuyTac = N'TheoDonVi' AND q.MaDonVi = sp.MaDonVi)
                OR (q.LoaiQuyTac = N'TheoThuongHieu' AND q.MaThuongHieu = sp.MaThuongHieu)
                OR (q.LoaiQuyTac = N'TheoLoai' AND q.MaLoai = sp.MaLoai)
                OR (q.LoaiQuyTac = N'Chung')
            )
            ORDER BY 
                CASE q.LoaiQuyTac
                    WHEN N'TheoSanPham' THEN 4
                    WHEN N'TheoDonVi' THEN 3
                    WHEN N'TheoThuongHieu' THEN 2
                    WHEN N'TheoLoai' THEN 1
                    WHEN N'Chung' THEN 0
                    ELSE 0
                END DESC,
                q.NgayCapNhat DESC
        ), ISNULL((
            -- Nếu không có quy tắc, lấy từ cấu hình mặc định
            SELECT TOP 1 PhanTramLoiNhuanMacDinh
            FROM dbo.Tbl_CauHinhLoiNhuan
            ORDER BY NgayCapNhat DESC
        ), 10.00)) / 100)
    ELSE 
        ISNULL(sp.GiaBan, 0) -- Nếu chưa có giá nhập, giữ giá bán hiện tại từ Tbl_SanPham
    END
FROM dbo.Tbl_KhoHang kh
INNER JOIN dbo.Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham;

PRINT N'Đã cập nhật giá bán cho ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' sản phẩm.';
GO

-- Bước 3: Không cần đồng bộ giá bán về Tbl_SanPham
-- Giá nhập và giá bán chỉ lưu trong Tbl_KhoHang

-- Hiển thị kết quả
SELECT 
    kh.MaSanPham,
    sp.TenSanPham,
    kh.GiaNhap,
    kh.GiaBan,
    CASE 
        WHEN kh.GiaNhap > 0 THEN 
            CAST(((kh.GiaBan - kh.GiaNhap) / kh.GiaNhap * 100) AS DECIMAL(5,2))
        ELSE 0
    END AS PhanTramLoiNhuan
FROM dbo.Tbl_KhoHang kh
INNER JOIN dbo.Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham
ORDER BY kh.MaSanPham;

PRINT N'Hoàn tất! Đã cập nhật giá nhập và giá bán cho tất cả sản phẩm trong kho hàng.';
GO

