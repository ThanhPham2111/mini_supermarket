SET NOCOUNT ON;
BEGIN TRY
    BEGIN TRAN;

    -- Lấy 1 nhân viên và TOP 5 sản phẩm
    DECLARE @MaNV INT = (SELECT TOP 1 MaNhanVien FROM Tbl_NhanVien ORDER BY MaNhanVien);
    DECLARE @Products TABLE (MaSanPham INT, GiaBan DECIMAL(18,2));
    INSERT INTO @Products(MaSanPham, GiaBan)
    SELECT TOP 5 MaSanPham, ISNULL(GiaBan, 10000) FROM Tbl_SanPham ORDER BY MaSanPham;

    IF NOT EXISTS (SELECT 1 FROM @Products) THROW 50000, N'Không có sản phẩm trong Tbl_SanPham.', 1;

    -- Tạo hóa đơn 7 ngày (không chèn MaHoaDonCode)
    DECLARE @i INT = 0;
    WHILE (@i < 7)
    BEGIN
        DECLARE @Ngay DATE = DATEADD(DAY, -@i, CAST(GETDATE() AS DATE));
        DECLARE @NewHD INT;

        INSERT INTO Tbl_HoaDon (NgayLap, MaNhanVien, TongTien)
        VALUES (@Ngay, @MaNV, 0);
        SET @NewHD = SCOPE_IDENTITY();

        -- Thêm 3 chi tiết
        INSERT INTO Tbl_ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan)
        SELECT TOP 3 @NewHD, p.MaSanPham, (ABS(CHECKSUM(NEWID())) % 5) + 1, p.GiaBan
        FROM @Products p ORDER BY NEWID();

        -- Cập nhật tổng tiền
        UPDATE hd SET TongTien = x.SumTien
        FROM Tbl_HoaDon hd
        JOIN (SELECT MaHoaDon, SUM(SoLuong * GiaBan) AS SumTien FROM Tbl_ChiTietHoaDon WHERE MaHoaDon = @NewHD GROUP BY MaHoaDon) x 
        ON x.MaHoaDon = hd.MaHoaDon
        WHERE hd.MaHoaDon = @NewHD;

        SET @i += 1;
    END

    -- Cập nhật HSD 3 sản phẩm vào 7 ngày tới
    IF COL_LENGTH('Tbl_SanPham','HSD') IS NOT NULL
    BEGIN
        UPDATE TOP (3) sp SET HSD = DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 7) + 1, CAST(GETDATE() AS date))
        FROM Tbl_SanPham sp WHERE sp.HSD IS NULL OR sp.HSD > DATEADD(DAY, 7, GETDATE());
    END

    COMMIT TRAN;
    PRINT N'ĐÃ TẠO 7 HÓA ĐƠN DEMO + CẬP NHẬT HSD.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;