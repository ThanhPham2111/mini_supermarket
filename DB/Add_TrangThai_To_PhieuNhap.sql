USE mini_sp01;
GO

-- Thêm cột TrangThai vào bảng Tbl_PhieuNhap
ALTER TABLE Tbl_PhieuNhap
ADD TrangThai NVARCHAR(50) DEFAULT N'Đang nhập';
GO

-- Cập nhật trạng thái cho các phiếu nhập hiện có
UPDATE Tbl_PhieuNhap
SET TrangThai = N'Nhập thành công'
WHERE TrangThai IS NULL;
GO

PRINT 'Migration completed: Added TrangThai column to Tbl_PhieuNhap';
