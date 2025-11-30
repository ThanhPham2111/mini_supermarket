-- =============================================
-- Script xóa các cột GiaNhap và GiaBan khỏi bảng Tbl_KhoHang
-- Chỉ giữ lại MaSanPham, SoLuong, TrangThai
-- =============================================

USE mini_sp01;
GO

-- Kiểm tra và xóa cột GiaNhap nếu tồn tại
IF EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Tbl_KhoHang' 
    AND COLUMN_NAME = 'GiaNhap'
)
BEGIN
    ALTER TABLE Tbl_KhoHang DROP COLUMN GiaNhap;
    PRINT N'Đã xóa cột GiaNhap khỏi Tbl_KhoHang.';
END
ELSE
BEGIN
    PRINT N'Cột GiaNhap không tồn tại trong Tbl_KhoHang.';
END
GO

-- Kiểm tra và xóa cột GiaBan nếu tồn tại
IF EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Tbl_KhoHang' 
    AND COLUMN_NAME = 'GiaBan'
)
BEGIN
    ALTER TABLE Tbl_KhoHang DROP COLUMN GiaBan;
    PRINT N'Đã xóa cột GiaBan khỏi Tbl_KhoHang.';
END
ELSE
BEGIN
    PRINT N'Cột GiaBan không tồn tại trong Tbl_KhoHang.';
END
GO

PRINT N'Hoàn tất: Bảng Tbl_KhoHang chỉ còn các cột: MaSanPham, SoLuong, TrangThai';
GO

