USE mini_sp01;
GO

-- 1. Tbl_ThuongHieu
INSERT INTO Tbl_ThuongHieu (TenThuongHieu) VALUES 
(N'Vinamilk'),
(N'Coca Cola'),
(N'Pepsi'),
(N'Hảo Hảo'),
(N'Kinh Đô'),
(N'Unilever'),
(N'Nestlé');

-- 2. Tbl_Loai
INSERT INTO Tbl_Loai (TenLoai, MoTa, TrangThai) VALUES 
(N'Đồ uống', N'Các loại nước ngọt, bia, sữa', N'Hoạt động'),
(N'Thực phẩm khô', N'Mì gói, bánh kẹo, đồ hộp', N'Hoạt động'),
(N'Gia vị', N'Mắm, muối, đường, hạt nêm', N'Hoạt động'),
(N'Hóa mỹ phẩm', N'Dầu gội, sữa tắm, bột giặt', N'Hoạt động'),
(N'Thực phẩm tươi sống', N'Thịt, cá, rau củ', N'Hoạt động');

-- 3. Tbl_DonVi
INSERT INTO Tbl_DonVi (TenDonVi, MoTa) VALUES 
(N'Chai', N''),
(N'Lon', N''),
(N'Gói', N''),
(N'Thùng', N''),
(N'Hộp', N''),
(N'Kg', N'');

-- 4. Tbl_SanPham
-- Giả sử ID tự tăng bắt đầu từ 1
INSERT INTO Tbl_SanPham (TenSanPham, MaDonVi, MaThuongHieu, MaLoai, MoTa, GiaBan, HinhAnh, XuatXu, Hsd, TrangThai) VALUES 
(N'Sữa tươi Vinamilk 1L', 5, 1, 1, N'Sữa tươi tiệt trùng không đường', 32000, NULL, N'Việt Nam', '2025-12-31', N'Đang bán'),
(N'Coca Cola 330ml', 2, 2, 1, N'Nước giải khát có gas', 10000, NULL, N'Việt Nam', '2025-06-30', N'Đang bán'),
(N'Mì Hảo Hảo Tôm Chua Cay', 3, 4, 2, N'Mì ăn liền hương vị tôm chua cay', 4500, NULL, N'Việt Nam', '2024-12-31', N'Đang bán'),
(N'Dầu gội Clear Men', 1, 6, 4, N'Dầu gội sạch gàu cho nam', 65000, NULL, N'Việt Nam', '2026-01-01', N'Đang bán'),
(N'Nước mắm Nam Ngư', 1, 6, 3, N'Nước mắm chinsu nam ngư', 35000, NULL, N'Việt Nam', '2025-05-20', N'Đang bán');

-- 5. Tbl_NhanVien
INSERT INTO Tbl_NhanVien (TenNhanVien, GioiTinh, NgaySinh, SoDienThoai, VaiTro, TrangThai) VALUES 
(N'Nguyễn Văn Quản Lý', N'Nam', '1990-01-01', '0901234567', N'Quản lý', N'Đang làm việc'),
(N'Trần Thị Thu Ngân', N'Nữ', '1995-05-15', '0909876543', N'Thu ngân', N'Đang làm việc'),
(N'Lê Văn Kho', N'Nam', '1992-09-20', '0912345678', N'Thủ kho', N'Đang làm việc');

-- 6. Tbl_PhanQuyen
INSERT INTO Tbl_PhanQuyen (TenQuyen, MoTa) VALUES 
(N'Admin', N'Quản trị viên hệ thống, toàn quyền'),
(N'Quản lý', N'Quản lý cửa hàng'),
(N'Thu ngân', N'Nhân viên bán hàng và thu tiền'),
(N'Thủ kho', N'Nhân viên quản lý kho hàng');

-- 7. Tbl_TaiKhoan
-- Pass: 123456 (Lưu ý: Trong thực tế nên hash password)
INSERT INTO Tbl_TaiKhoan (TenDangNhap, MatKhau, MaNhanVien, MaQuyen, TrangThai) VALUES 
('admin', '123456', 1, 1, N'Hoạt động'),
('manager', '123456', 1, 2, N'Hoạt động'),
('thungan', '123456', 2, 3, N'Hoạt động'),
('thukho', '123456', 3, 4, N'Hoạt động');

-- 8. Tbl_KhachHang
INSERT INTO Tbl_KhachHang (TenKhachHang, SoDienThoai, DiaChi, Email, DiemTichLuy, TrangThai) VALUES 
(N'Khách lẻ', '0000000000', N'Tại quầy', NULL, 0, N'Hoạt động'),
(N'Nguyễn Văn A', '0987654321', N'123 Lê Lợi, TP.HCM', 'nguyenvana@email.com', 150, N'Hoạt động'),
(N'Trần Thị B', '0912345679', N'456 Nguyễn Huệ, TP.HCM', 'tranthib@email.com', 50, N'Hoạt động');

-- 9. Tbl_NhaCungCap
INSERT INTO Tbl_NhaCungCap (TenNhaCungCap, DiaChi, SoDienThoai, Email, TrangThai) VALUES 
(N'Công ty CP Vinamilk', N'Số 10, Tân Trào, Q7, TP.HCM', '02854155555', 'vinamilk@vinamilk.com.vn', N'Hợp tác'),
(N'Công ty TNHH Coca-Cola VN', N'Xa lộ Hà Nội, Thủ Đức', '02838966999', 'coca@cocacola.com', N'Hợp tác'),
(N'Acecook Việt Nam', N'KCN Tân Bình', '02838154064', 'info@acecook.com', N'Hợp tác');

-- 10. Tbl_KhoHang
INSERT INTO Tbl_KhoHang (MaSanPham, SoLuong, TrangThai) VALUES 
(1, 100, N'Còn hàng'),
(2, 200, N'Còn hàng'),
(3, 500, N'Còn hàng'),
(4, 50, N'Còn hàng'),
(5, 80, N'Còn hàng');

-- 11. Tbl_ChucNang (Dựa trên Sidebar)
INSERT INTO Tbl_ChucNang (TenChucNang, MaCha, DuongDan, MoTa) VALUES 
(N'Trang Chủ', NULL, 'Form_TrangChu', N'Màn hình dashboard'),
(N'Bán Hàng', NULL, 'Form_BanHang', N'Màn hình POS'),
(N'Hóa Đơn', NULL, 'Form_HoaDon', N'Quản lý hóa đơn'),
(N'Phiếu Nhập', NULL, 'Form_PhieuNhap', N'Quản lý nhập kho'),
(N'Sản Phẩm', NULL, 'Form_SanPham', N'Quản lý danh sách sản phẩm'),
(N'Kho Hàng', NULL, 'Form_KhoHang', N'Quản lý tồn kho'),
(N'Loại Sản Phẩm', NULL, 'Form_LoaiSanPham', N'Quản lý danh mục loại'),
(N'Khuyến Mãi', NULL, 'Form_KhuyenMai', N'Chương trình khuyến mãi'),
(N'Khách Hàng', NULL, 'Form_KhachHang', N'Quản lý khách hàng'),
(N'Nhân Viên', NULL, 'Form_NhanVien', N'Quản lý nhân viên'),
(N'Nhà Cung Cấp', NULL, 'Form_NhaCungCap', N'Quản lý nhà cung cấp'),
(N'Tài Khoản', NULL, 'Form_TaiKhoan', N'Quản lý tài khoản hệ thống'),
(N'Quản Lý', NULL, 'Form_QuanLy', N'Phân quyền và cấu hình hệ thống');

-- 12. Tbl_LoaiQuyen
INSERT INTO Tbl_LoaiQuyen (TenQuyen, MoTa) VALUES 
(N'Xem', N'Quyền xem dữ liệu'),
(N'Thêm', N'Quyền thêm mới dữ liệu'),
(N'Sửa', N'Quyền cập nhật dữ liệu'),
(N'Xóa', N'Quyền xóa dữ liệu'),
(N'In', N'Quyền in ấn (hóa đơn, báo cáo)'),
(N'Xuất Excel', N'Quyền xuất dữ liệu ra file');

-- 13. Tbl_PhanQuyenChiTiet
-- Cấp quyền Full cho Admin (MaQuyen = 1) cho tất cả chức năng và loại quyền
-- (Sử dụng cursor hoặc insert select để tạo nhanh)
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 1, cn.MaChucNang, lq.MaLoaiQuyen, 1
FROM Tbl_ChucNang cn, Tbl_LoaiQuyen lq;

-- Cấp quyền cho Thu Ngân (MaQuyen = 3): Chỉ Bán hàng, Xem sản phẩm, Xem khách hàng
-- 1. Bán hàng (MaChucNang = 2): Full quyền
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 3, 2, MaLoaiQuyen, 1 FROM Tbl_LoaiQuyen;

-- 2. Sản phẩm (MaChucNang = 5): Chỉ Xem
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 3, 5, MaLoaiQuyen, 1 FROM Tbl_LoaiQuyen WHERE TenQuyen = N'Xem';

-- 3. Khách hàng (MaChucNang = 9): Xem, Thêm
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 3, 9, MaLoaiQuyen, 1 FROM Tbl_LoaiQuyen WHERE TenQuyen IN (N'Xem', N'Thêm');

-- 14. Tbl_HoaDon (Vài hóa đơn mẫu)
INSERT INTO Tbl_HoaDon (MaNhanVien, MaKhachHang, TongTien, NgayLap) VALUES 
(2, 1, 42000, GETDATE()),
(2, 2, 65000, DATEADD(day, -1, GETDATE()));

-- 15. Tbl_ChiTietHoaDon
-- Hóa đơn 1: 1 Sữa (32k) + 1 Coca (10k) = 42k
INSERT INTO Tbl_ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan) VALUES 
(1, 1, 1, 32000),
(1, 2, 1, 10000);

-- Hóa đơn 2: 1 Dầu gội (65k)
INSERT INTO Tbl_ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan) VALUES 
(2, 4, 1, 65000);

-- 16. Tbl_PhieuNhap
INSERT INTO Tbl_PhieuNhap (NgayNhap, MaNhaCungCap, TongTien) VALUES 
(DATEADD(month, -1, GETDATE()), 1, 3200000);

-- 17. Tbl_ChiTietPhieuNhap
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(1, 1, 100, 30000, 3000000); -- Nhập 100 chai sữa giá 30k

-- 18. Tbl_KhuyenMai
INSERT INTO Tbl_KhuyenMai (MaSanPham, TenKhuyenMai, PhanTramGiamGia, NgayBatDau, NgayKetThuc, MoTa) VALUES 
(2, N'Mua hè sảng khoái', 10.00, '2025-06-01', '2025-08-31', N'Giảm giá nước ngọt');

-- 19. Tbl_LichSuThayDoiKho
INSERT INTO Tbl_LichSuThayDoiKho (MaSanPham, SoLuongCu, SoLuongMoi, ChenhLech, LoaiThayDoi, LyDo, MaNhanVien, NgayThayDoi) VALUES 
(1, 0, 100, 100, N'Nhập hàng', N'Nhập hàng đầu kỳ', 3, DATEADD(month, -1, GETDATE()));
