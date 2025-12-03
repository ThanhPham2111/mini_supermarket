-- =============================================
-- Script thêm c?t TrangThaiDieuKien vào Tbl_KhoHang
-- M?c ?ích: Qu?n lý tr?ng thái bán c?a s?n ph?m (Bán ho?c Không bán)
-- Ngày t?o: 2025
-- =============================================

-- S? d?ng database (s? d?ng ngo?c vuông ?? tránh l?i v?i d?u g?ch ngang)
USE [mini_sp01];
GO

-- Ki?m tra xem c?t ?ã t?n t?i ch?a
IF NOT EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Tbl_KhoHang' 
      AND COLUMN_NAME = 'TrangThaiDieuKien'
      AND TABLE_SCHEMA = 'dbo'
)
BEGIN
    -- Thêm c?t v?i giá tr? m?c ??nh "Bán"
    ALTER TABLE [dbo].[Tbl_KhoHang]
    ADD [TrangThaiDieuKien] NVARCHAR(20) NOT NULL DEFAULT N'Bán';
    
    PRINT N'? ?ã thêm c?t TrangThaiDieuKien vào Tbl_KhoHang';
END
ELSE
BEGIN
    PRINT N'? C?t TrangThaiDieuKien ?ã t?n t?i trong Tbl_KhoHang';
END
GO

-- C?p nh?t các b?n ghi hi?n t?i n?u c?t t?n t?i nh?ng có giá tr? NULL
IF EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Tbl_KhoHang' 
      AND COLUMN_NAME = 'TrangThaiDieuKien'
      AND TABLE_SCHEMA = 'dbo'
)
BEGIN
    -- Ki?m tra xem có b?n ghi nào NULL không
    IF EXISTS (SELECT 1 FROM [dbo].[Tbl_KhoHang] WHERE [TrangThaiDieuKien] IS NULL)
    BEGIN
        UPDATE [dbo].[Tbl_KhoHang]
        SET [TrangThaiDieuKien] = N'Bán'
        WHERE [TrangThaiDieuKien] IS NULL;
        
        PRINT N'? ?ã c?p nh?t giá tr? m?c ??nh cho các b?n ghi hi?n t?i';
    END
    ELSE
    BEGIN
        PRINT N'? T?t c? b?n ghi ?ã có giá tr? TrangThaiDieuKien';
    END
END
GO

PRINT N'? Hoàn t?t! C?t TrangThaiDieuKien ?ã ???c thêm và c?u hình.';
GO
