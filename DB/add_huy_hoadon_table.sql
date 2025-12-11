-- Script để thêm bảng lưu lịch sử hủy hóa đơn
-- Chạy script này để thêm bảng mới

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tbl_LichSuHuyHoaDon')
BEGIN
    CREATE TABLE Tbl_LichSuHuyHoaDon (
        MaLichSuHuy INT PRIMARY KEY IDENTITY(1,1),
        MaHoaDon INT NOT NULL,
        LyDoHuy NVARCHAR(MAX),
        MaNhanVienHuy INT NOT NULL,
        NgayHuy DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (MaHoaDon) REFERENCES Tbl_HoaDon(MaHoaDon),
        FOREIGN KEY (MaNhanVienHuy) REFERENCES Tbl_NhanVien(MaNhanVien)
    );
    
    PRINT 'Đã tạo bảng Tbl_LichSuHuyHoaDon thành công!';
END
ELSE
BEGIN
    PRINT 'Bảng Tbl_LichSuHuyHoaDon đã tồn tại.';
END
GO

