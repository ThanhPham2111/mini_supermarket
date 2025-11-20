-- T?o b?ng L?ch s? thay ??i kho
-- Ch?y script này trong database mini_sp
-- Script này IDEMPOTENT - có th? ch?y nhi?u l?n không l?i

USE mini_sp;
GO

-- Ki?m tra và xóa các INDEX n?u ?ã t?n t?i
IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IDX_LichSuThayDoiKho_MaSanPham' AND object_id = OBJECT_ID('Tbl_LichSuThayDoiKho'))
    DROP INDEX IDX_LichSuThayDoiKho_MaSanPham ON Tbl_LichSuThayDoiKho;
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IDX_LichSuThayDoiKho_NgayThayDoi' AND object_id = OBJECT_ID('Tbl_LichSuThayDoiKho'))
    DROP INDEX IDX_LichSuThayDoiKho_NgayThayDoi ON Tbl_LichSuThayDoiKho;
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IDX_LichSuThayDoiKho_MaNhanVien' AND object_id = OBJECT_ID('Tbl_LichSuThayDoiKho'))
    DROP INDEX IDX_LichSuThayDoiKho_MaNhanVien ON Tbl_LichSuThayDoiKho;
GO

-- Ki?m tra và xóa b?ng n?u ?ã t?n t?i
IF OBJECT_ID('Tbl_LichSuThayDoiKho', 'U') IS NOT NULL
BEGIN
    DROP TABLE Tbl_LichSuThayDoiKho;
    PRINT N'?ã xóa b?ng c? Tbl_LichSuThayDoiKho';
END
GO

-- T?o b?ng m?i
CREATE TABLE Tbl_LichSuThayDoiKho (
    MaLichSu INT PRIMARY KEY IDENTITY(1,1),
    MaSanPham INT NOT NULL,
    SoLuongCu INT NOT NULL,
    SoLuongMoi INT NOT NULL,
    ChenhLech INT NOT NULL,
    LoaiThayDoi NVARCHAR(50) COLLATE Vietnamese_CI_AS NOT NULL, -- 'Ki?m kê', '?i?u ch?nh', 'H?y hàng'
    LyDo NVARCHAR(255) COLLATE Vietnamese_CI_AS,
    GhiChu NVARCHAR(MAX) COLLATE Vietnamese_CI_AS,
    MaNhanVien INT NOT NULL,
    NgayThayDoi DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaSanPham) REFERENCES Tbl_SanPham(MaSanPham),
    FOREIGN KEY (MaNhanVien) REFERENCES Tbl_NhanVien(MaNhanVien)
);
PRINT N'?ã t?o b?ng Tbl_LichSuThayDoiKho';
GO

-- T?o các index ?? t?ng t?c truy v?n
CREATE INDEX IDX_LichSuThayDoiKho_MaSanPham ON Tbl_LichSuThayDoiKho(MaSanPham);
PRINT N'?ã t?o index: IDX_LichSuThayDoiKho_MaSanPham';
GO

CREATE INDEX IDX_LichSuThayDoiKho_NgayThayDoi ON Tbl_LichSuThayDoiKho(NgayThayDoi);
PRINT N'?ã t?o index: IDX_LichSuThayDoiKho_NgayThayDoi';
GO

CREATE INDEX IDX_LichSuThayDoiKho_MaNhanVien ON Tbl_LichSuThayDoiKho(MaNhanVien);
PRINT N'?ã t?o index: IDX_LichSuThayDoiKho_MaNhanVien';
GO

PRINT N'? Hoàn t?t! T?o b?ng Tbl_LichSuThayDoiKho thành công!';
GO
