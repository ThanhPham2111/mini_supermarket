-- Script để insert dữ liệu chức năng và loại quyền
USE mini_sp01;
GO

-- 1. Insert Loại quyền (View, Create, Update, Delete)
IF NOT EXISTS (SELECT 1 FROM Tbl_LoaiQuyen WHERE MaLoaiQuyen = 1)
BEGIN
    INSERT INTO Tbl_LoaiQuyen (TenQuyen, MoTa) VALUES
    (N'Xem', N'Quyền xem dữ liệu'),
    (N'Thêm', N'Quyền thêm mới dữ liệu'),
    (N'Sửa', N'Quyền sửa dữ liệu'),
    (N'Xóa', N'Quyền xóa dữ liệu');
END
GO

-- 2. Insert Chức năng (mapping với sidebar)
IF NOT EXISTS (SELECT 1 FROM Tbl_ChucNang WHERE MaChucNang = 1)
BEGIN
    INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES
    -- Chức năng cha
    (N'Trang chủ', NULL, 'TrangChu', N'Trang chủ hệ thống'),
    (N'Bán hàng', NULL, 'BanHang', N'Chức năng bán hàng'),
    (N'Hóa đơn', NULL, 'HoaDon', N'Quản lý hóa đơn'),
    (N'Phiếu nhập', NULL, 'PhieuNhap', N'Quản lý phiếu nhập'),
    (N'Sản phẩm', NULL, 'SanPham', N'Quản lý sản phẩm'),
    (N'Kho hàng', NULL, 'KhoHang', N'Quản lý kho hàng'),
    (N'Loại sản phẩm', NULL, 'LoaiSanPham', N'Quản lý loại sản phẩm'),
    (N'Khuyến mãi', NULL, 'KhuyenMai', N'Quản lý khuyến mãi'),
    (N'Khách hàng', NULL, 'KhachHang', N'Quản lý khách hàng'),
    (N'Nhà cung cấp', NULL, 'NhaCungCap', N'Quản lý nhà cung cấp'),
    (N'Nhân viên', NULL, 'NhanVien', N'Quản lý nhân viên'),
    (N'Tài khoản', NULL, 'TaiKhoan', N'Quản lý tài khoản'),
    (N'Quản lý', NULL, 'QuanLy', N'Quản lý hệ thống');
END
GO

-- 3. Mặc định: Admin có quyền tất cả
-- (Admin không cần insert vào Tbl_PhanQuyenChiTiet vì code sẽ check MaQuyen = 1)

-- 4. Ví dụ: Cấp quyền cho "Thu ngân" (MaQuyen = 3)
-- Thu ngân có quyền: Xem Bán hàng, Xem Hóa đơn, Xem Khách hàng, Thêm/Sửa Bán hàng, Thêm Hóa đơn
IF NOT EXISTS (SELECT 1 FROM Tbl_PhanQuyenChiTiet WHERE MaQuyen = 3 AND MaChucNang = 2 AND MaLoaiQuyen = 1)
BEGIN
    -- Bán hàng - Xem, Thêm, Sửa
    INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep) VALUES
    (3, 2, 1, 1), -- Bán hàng - Xem
    (3, 2, 2, 1), -- Bán hàng - Thêm
    (3, 2, 3, 1), -- Bán hàng - Sửa
    
    -- Hóa đơn - Xem
    (3, 3, 1, 1), -- Hóa đơn - Xem
    
    -- Khách hàng - Xem
    (3, 9, 1, 1); -- Khách hàng - Xem
END
GO

-- 5. Ví dụ: Cấp quyền cho "Thủ kho" (MaQuyen = 4)
-- Thủ kho có quyền: Xem/Thêm/Sửa Phiếu nhập, Xem/Thêm/Sửa Kho hàng, Xem Sản phẩm
IF NOT EXISTS (SELECT 1 FROM Tbl_PhanQuyenChiTiet WHERE MaQuyen = 4 AND MaChucNang = 4 AND MaLoaiQuyen = 1)
BEGIN
    -- Phiếu nhập - Xem, Thêm, Sửa
    INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep) VALUES
    (4, 4, 1, 1), -- Phiếu nhập - Xem
    (4, 4, 2, 1), -- Phiếu nhập - Thêm
    (4, 4, 3, 1), -- Phiếu nhập - Sửa
    
    -- Kho hàng - Xem, Thêm, Sửa
    (4, 6, 1, 1), -- Kho hàng - Xem
    (4, 6, 2, 1), -- Kho hàng - Thêm
    (4, 6, 3, 1), -- Kho hàng - Sửa
    
    -- Sản phẩm - Xem
    (4, 5, 1, 1); -- Sản phẩm - Xem
END
GO

-- 6. Ví dụ: Cấp quyền cho "Quản lý" (MaQuyen = 2)
-- Quản lý có quyền hầu hết các chức năng (trừ Quản lý hệ thống)
IF NOT EXISTS (SELECT 1 FROM Tbl_PhanQuyenChiTiet WHERE MaQuyen = 2 AND MaChucNang = 2 AND MaLoaiQuyen = 1)
BEGIN
    -- Quản lý có quyền Xem, Thêm, Sửa cho hầu hết chức năng
    INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep) VALUES
    -- Bán hàng
    (2, 2, 1, 1), (2, 2, 2, 1), (2, 2, 3, 1),
    -- Hóa đơn
    (2, 3, 1, 1), (2, 3, 2, 1), (2, 3, 3, 1),
    -- Phiếu nhập
    (2, 4, 1, 1), (2, 4, 2, 1), (2, 4, 3, 1),
    -- Sản phẩm
    (2, 5, 1, 1), (2, 5, 2, 1), (2, 5, 3, 1),
    -- Kho hàng
    (2, 6, 1, 1), (2, 6, 2, 1), (2, 6, 3, 1),
    -- Loại sản phẩm
    (2, 7, 1, 1), (2, 7, 2, 1), (2, 7, 3, 1),
    -- Khuyến mãi
    (2, 8, 1, 1), (2, 8, 2, 1), (2, 8, 3, 1),
    -- Khách hàng
    (2, 9, 1, 1), (2, 9, 2, 1), (2, 9, 3, 1),
    -- Nhà cung cấp
    (2, 10, 1, 1), (2, 10, 2, 1), (2, 10, 3, 1),
    -- Nhân viên
    (2, 11, 1, 1), (2, 11, 2, 1), (2, 11, 3, 1),
    -- Tài khoản
    (2, 12, 1, 1), (2, 12, 2, 1), (2, 12, 3, 1);
END
GO

PRINT 'Đã hoàn tất insert dữ liệu chức năng và phân quyền!';
GO

