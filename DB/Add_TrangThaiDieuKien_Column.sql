-- =============================================
-- Script th�m c?t TrangThaiDieuKien v�o Tbl_KhoHang
-- M?c ?�ch: Qu?n l� tr?ng th�i b�n c?a s?n ph?m (B�n ho?c Kh�ng b�n)
-- Ng�y t?o: 2025
-- =============================================

-- S? d?ng database (s? d?ng ngo?c vu�ng ?? tr�nh l?i v?i d?u g?ch ngang)
USE [mini_sp01];
GO

-- Ki?m tra xem c?t ?� t?n t?i ch?a
IF NOT EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Tbl_KhoHang' 
      AND COLUMN_NAME = 'TrangThaiDieuKien'
      AND TABLE_SCHEMA = 'dbo'
)
BEGIN
    -- Th�m c?t v?i gi� tr? m?c ??nh "B�n"
    ALTER TABLE [dbo].[Tbl_KhoHang]
    ADD [TrangThaiDieuKien] NVARCHAR(20) NOT NULL DEFAULT N'Bán';
    
    PRINT N'? ?� th�m c?t TrangThaiDieuKien v�o Tbl_KhoHang';
END
ELSE
BEGIN
    PRINT N'? C?t TrangThaiDieuKien ?� t?n t?i trong Tbl_KhoHang';
END
GO

-- C?p nh?t c�c b?n ghi hi?n t?i n?u c?t t?n t?i nh?ng c� gi� tr? NULL
IF EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Tbl_KhoHang' 
      AND COLUMN_NAME = 'TrangThaiDieuKien'
      AND TABLE_SCHEMA = 'dbo'
)
BEGIN
    -- Ki?m tra xem c� b?n ghi n�o NULL kh�ng
    IF EXISTS (SELECT 1 FROM [dbo].[Tbl_KhoHang] WHERE [TrangThaiDieuKien] IS NULL)
    BEGIN
        UPDATE [dbo].[Tbl_KhoHang]
        SET [TrangThaiDieuKien] = N'Bán'
        WHERE [TrangThaiDieuKien] IS NULL;
        
        PRINT N'? ?� c?p nh?t gi� tr? m?c ??nh cho c�c b?n ghi hi?n t?i';
    END
    ELSE
    BEGIN
        PRINT N'? T?t c? b?n ghi ?� c� gi� tr? TrangThaiDieuKien';
    END
END
GO

PRINT N'? Ho�n t?t! C?t TrangThaiDieuKien ?� ???c th�m v� c?u h�nh.';
GO
