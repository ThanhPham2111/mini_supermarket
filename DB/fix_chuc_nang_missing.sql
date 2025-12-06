USE mini_sp01;
GO

-- Script để kiểm tra và thêm các chức năng còn thiếu vào Tbl_ChucNang
-- Đảm bảo có đủ 13 chức năng với DuongDan có prefix "Form_"

-- Kiểm tra và thêm các chức năng còn thiếu
IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_TrangChu')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Trang Chủ', NULL, 'Form_TrangChu', N'Trang chủ hệ thống');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_BanHang')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Bán Hàng', NULL, 'Form_BanHang', N'Chức năng bán hàng');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_HoaDon')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Hóa Đơn', NULL, 'Form_HoaDon', N'Quản lý hóa đơn');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_PhieuNhap')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Phiếu Nhập', NULL, 'Form_PhieuNhap', N'Quản lý phiếu nhập');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_SanPham')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Sản Phẩm', NULL, 'Form_SanPham', N'Quản lý sản phẩm');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_KhoHang')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Kho Hàng', NULL, 'Form_KhoHang', N'Quản lý kho hàng');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_LoaiSanPham')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Loại Sản Phẩm', NULL, 'Form_LoaiSanPham', N'Quản lý loại sản phẩm');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_KhuyenMai')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Khuyến Mãi', NULL, 'Form_KhuyenMai', N'Quản lý khuyến mãi');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_KhachHang')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Khách Hàng', NULL, 'Form_KhachHang', N'Quản lý khách hàng');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_NhaCungCap')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Nhà Cung Cấp', NULL, 'Form_NhaCungCap', N'Quản lý nhà cung cấp');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_NhanVien')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Nhân Viên', NULL, 'Form_NhanVien', N'Quản lý nhân viên');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_TaiKhoan')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Tài Khoản', NULL, 'Form_TaiKhoan', N'Quản lý tài khoản');
END
GO

IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE DuongDan = 'Form_QuanLy')
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    (N'Quản Lý', NULL, 'Form_QuanLy', N'Quản lý hệ thống');
END
GO

-- Cập nhật DuongDan cho các chức năng đã có nhưng thiếu prefix "Form_"
UPDATE Tbl_ChucNang SET DuongDan = 'Form_TrangChu' WHERE DuongDan = 'TrangChu' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Trang Chủ%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_BanHang' WHERE DuongDan = 'BanHang' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Bán Hàng%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_HoaDon' WHERE DuongDan = 'HoaDon' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Hóa Đơn%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_PhieuNhap' WHERE DuongDan = 'PhieuNhap' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Phiếu Nhập%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_SanPham' WHERE DuongDan = 'SanPham' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Sản Phẩm%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_KhoHang' WHERE DuongDan = 'KhoHang' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Kho Hàng%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_LoaiSanPham' WHERE DuongDan = 'LoaiSanPham' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Loại Sản Phẩm%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_KhuyenMai' WHERE DuongDan = 'KhuyenMai' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Khuyến Mãi%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_KhachHang' WHERE DuongDan = 'KhachHang' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Khách Hàng%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_NhaCungCap' WHERE DuongDan = 'NhaCungCap' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Nhà Cung Cấp%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_NhanVien' WHERE DuongDan = 'NhanVien' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Nhân Viên%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_TaiKhoan' WHERE DuongDan = 'TaiKhoan' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Tài Khoản%');
UPDATE Tbl_ChucNang SET DuongDan = 'Form_QuanLy' WHERE DuongDan = 'QuanLy' OR (DuongDan IS NULL AND TenChucNang LIKE N'%Quản Lý%');
GO

-- Kiểm tra kết quả
SELECT COUNT(*) AS SoChucNang, 
       STRING_AGG(TenChucNang, ', ') AS DanhSachChucNang
FROM Tbl_ChucNang;
GO

PRINT N'Đã hoàn tất: Kiểm tra và thêm các chức năng còn thiếu.';
GO

