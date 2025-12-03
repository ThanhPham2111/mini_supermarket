USE mini_sp01;
GO

-- =============================================
-- FILE INSERT DỮ LIỆU MẪU CHO HỆ THỐNG BÁN HÀNG
-- Cập nhật theo schema mới: Giá nhập từ ChiTietPhieuNhap, Giá bán từ SanPham
-- =============================================

-- 1. Tbl_ThuongHieu (15 thương hiệu)
INSERT INTO Tbl_ThuongHieu (TenThuongHieu, TrangThai) VALUES 
(N'Vinamilk', N'Hoạt động'),
(N'Coca Cola', N'Hoạt động'),
(N'Pepsi', N'Hoạt động'),
(N'Hảo Hảo', N'Hoạt động'),
(N'Kinh Đô', N'Hoạt động'),
(N'Unilever', N'Hoạt động'),
(N'Nestlé', N'Hoạt động'),
(N'P&G', N'Hoạt động'),
(N'TH True Milk', N'Hoạt động'),
(N'Yomost', N'Hoạt động'),
(N'Chinsu', N'Hoạt động'),
(N'Ajinomoto', N'Hoạt động'),
(N'Maggi', N'Hoạt động'),
(N'Knorr', N'Hoạt động'),
(N'Thương hiệu ngưng hoạt động', N'Ngưng hoạt động');

-- 2. Tbl_Loai (8 loại sản phẩm)
INSERT INTO Tbl_Loai (TenLoai, MoTa, TrangThai) VALUES 
(N'Đồ uống', N'Các loại nước ngọt, bia, sữa, nước suối', N'Hoạt động'),
(N'Thực phẩm khô', N'Mì gói, bánh kẹo, đồ hộp, snack', N'Hoạt động'),
(N'Gia vị', N'Mắm, muối, đường, hạt nêm, bột canh', N'Hoạt động'),
(N'Hóa mỹ phẩm', N'Dầu gội, sữa tắm, bột giặt, nước rửa chén', N'Hoạt động'),
(N'Thực phẩm tươi sống', N'Thịt, cá, rau củ, trái cây', N'Hoạt động'),
(N'Bánh kẹo', N'Bánh quy, kẹo, chocolate', N'Hoạt động'),
(N'Đồ đông lạnh', N'Thực phẩm đông lạnh, kem', N'Hoạt động'),
(N'Vệ sinh cá nhân', N'Kem đánh răng, bàn chải, khăn giấy', N'Hoạt động');

-- 3. Tbl_DonVi (10 đơn vị)
INSERT INTO Tbl_DonVi (TenDonVi, MoTa, TrangThai) VALUES 
(N'Chai', N'', N'Hoạt động'),
(N'Lon', N'', N'Hoạt động'),
(N'Gói', N'', N'Hoạt động'),
(N'Thùng', N'', N'Hoạt động'),
(N'Hộp', N'', N'Hoạt động'),
(N'Kg', N'', N'Hoạt động'),
(N'Lít', N'', N'Hoạt động'),
(N'Gói lớn', N'', N'Hoạt động'),
(N'Túi', N'', N'Hoạt động'),
(N'Đơn vị ngưng hoạt động', N'Đơn vị đã ngưng sử dụng', N'Ngưng hoạt động');

-- 4. Tbl_SanPham (30 sản phẩm với giá bán đã tính sẵn)
-- Lưu ý: Giá bán sẽ được tính lại khi có phiếu nhập (dựa trên giá nhập và % lợi nhuận)
INSERT INTO Tbl_SanPham (TenSanPham, MaDonVi, MaThuongHieu, MaLoai, MoTa, GiaBan, HinhAnh, XuatXu, HSD, TrangThai) VALUES 
-- Đồ uống
(N'Sữa tươi Vinamilk 1L', 5, 1, 1, N'Sữa tươi tiệt trùng không đường', 32000, NULL, N'Việt Nam', '2025-12-31', N'Còn hàng'),
(N'Coca Cola 330ml', 2, 2, 1, N'Nước giải khát có gas', 10000, NULL, N'Việt Nam', '2025-06-30', N'Còn hàng'),
(N'Pepsi 330ml', 2, 3, 1, N'Nước giải khát có gas', 10000, NULL, N'Việt Nam', '2025-06-30', N'Còn hàng'),
(N'Sữa chua Vinamilk 100g', 5, 1, 1, N'Sữa chua uống vị dâu', 8000, NULL, N'Việt Nam', '2025-01-15', N'Còn hàng'),
(N'Nước suối Lavie 500ml', 1, 6, 1, N'Nước khoáng thiên nhiên', 5000, NULL, N'Việt Nam', '2026-12-31', N'Còn hàng'),
(N'Yomost dâu 110ml', 1, 10, 1, N'Sữa chua uống Yomost vị dâu', 7000, NULL, N'Việt Nam', '2025-02-28', N'Còn hàng'),
-- Thực phẩm khô
(N'Mì Hảo Hảo Tôm Chua Cay', 3, 4, 2, N'Mì ăn liền hương vị tôm chua cay', 4500, NULL, N'Việt Nam', '2025-12-31', N'Còn hàng'),
(N'Mì Hảo Hảo Chay', 3, 4, 2, N'Mì ăn liền chay', 4500, NULL, N'Việt Nam', '2025-12-31', N'Còn hàng'),
(N'Snack Oishi tôm cay', 3, 4, 2, N'Snack khoai tây vị tôm cay', 12000, NULL, N'Việt Nam', '2025-08-31', N'Còn hàng'),
(N'Bánh quy Kinh Đô bơ', 3, 5, 6, N'Bánh quy bơ Kinh Đô', 25000, NULL, N'Việt Nam', '2025-10-31', N'Còn hàng'),
(N'Bánh quy Oreo', 3, 7, 6, N'Bánh quy kem Oreo', 35000, NULL, N'Việt Nam', '2025-11-30', N'Còn hàng'),
-- Gia vị
(N'Nước mắm Chinsu 500ml', 1, 11, 3, N'Nước mắm Chinsu', 35000, NULL, N'Việt Nam', '2026-05-20', N'Còn hàng'),
(N'Hạt nêm Ajinomoto 450g', 3, 12, 3, N'Hạt nêm Ajinomoto', 28000, NULL, N'Việt Nam', '2026-03-31', N'Còn hàng'),
(N'Bột canh Knorr 200g', 3, 14, 3, N'Bột canh Knorr', 15000, NULL, N'Việt Nam', '2026-04-30', N'Còn hàng'),
(N'Đường trắng 1kg', 3, 6, 3, N'Đường trắng tinh luyện', 18000, NULL, N'Việt Nam', '2026-12-31', N'Còn hàng'),
-- Hóa mỹ phẩm
(N'Dầu gội Clear Men 400ml', 1, 6, 4, N'Dầu gội sạch gàu cho nam', 65000, NULL, N'Việt Nam', '2026-01-01', N'Còn hàng'),
(N'Dầu gội Sunsilk 400ml', 1, 6, 4, N'Dầu gội dưỡng tóc', 55000, NULL, N'Việt Nam', '2026-02-01', N'Còn hàng'),
(N'Sữa tắm Lifebuoy 400ml', 1, 6, 4, N'Sữa tắm diệt khuẩn', 45000, NULL, N'Việt Nam', '2026-03-01', N'Còn hàng'),
(N'Bột giặt Omo 1kg', 3, 6, 4, N'Bột giặt Omo', 85000, NULL, N'Việt Nam', '2026-12-31', N'Còn hàng'),
(N'Nước rửa chén Sunlight 500ml', 1, 6, 4, N'Nước rửa chén Sunlight', 25000, NULL, N'Việt Nam', '2026-06-30', N'Còn hàng'),
-- Vệ sinh cá nhân
(N'Kem đánh răng P/S 150g', 1, 8, 8, N'Kem đánh răng P/S', 35000, NULL, N'Việt Nam', '2026-08-31', N'Còn hàng'),
(N'Bàn chải đánh răng P/S', 1, 8, 8, N'Bàn chải đánh răng', 15000, NULL, N'Việt Nam', '2027-12-31', N'Còn hàng'),
(N'Khăn giấy Pulppy 2 lớp', 3, 6, 8, N'Khăn giấy ướt Pulppy', 20000, NULL, N'Việt Nam', '2026-12-31', N'Còn hàng'),
-- Thực phẩm tươi sống (giá bán sẽ được cập nhật khi nhập hàng)
(N'Thịt heo ba chỉ', 6, 1, 5, N'Thịt heo ba chỉ tươi', 120000, NULL, N'Việt Nam', NULL, N'Còn hàng'),
(N'Cá basa fillet', 6, 1, 5, N'Cá basa fillet đông lạnh', 80000, NULL, N'Việt Nam', NULL, N'Còn hàng'),
(N'Rau cải xanh', 6, 1, 5, N'Rau cải xanh tươi', 15000, NULL, N'Việt Nam', NULL, N'Còn hàng'),
-- Đồ đông lạnh
(N'Kem Wall''s Vani', 1, 6, 7, N'Kem Wall''s vị vani', 15000, NULL, N'Việt Nam', '2025-08-31', N'Còn hàng'),
(N'Kem Wall''s Socola', 1, 6, 7, N'Kem Wall''s vị socola', 15000, NULL, N'Việt Nam', '2025-08-31', N'Còn hàng'),
(N'Chả cá viên', 3, 1, 7, N'Chả cá viên đông lạnh', 45000, NULL, N'Việt Nam', '2025-09-30', N'Còn hàng');

-- 5. Tbl_NhanVien (8 nhân viên)
INSERT INTO Tbl_NhanVien (TenNhanVien, GioiTinh, NgaySinh, SoDienThoai, VaiTro, TrangThai) VALUES 
(N'Nguyễn Văn Quản Lý', N'Nam', '1990-01-01', '0901234567', N'Quản lý', N'Đang làm việc'),
(N'Trần Thị Thu Ngân', N'Nữ', '1995-05-15', '0909876543', N'Thu ngân', N'Đang làm việc'),
(N'Lê Văn Kho', N'Nam', '1992-09-20', '0912345678', N'Thủ kho', N'Đang làm việc'),
(N'Phạm Thị Hoa', N'Nữ', '1996-03-10', '0923456789', N'Thu ngân', N'Đang làm việc'),
(N'Hoàng Văn Nam', N'Nam', '1993-07-25', '0934567890', N'Thu ngân', N'Đang làm việc'),
(N'Võ Thị Lan', N'Nữ', '1994-11-05', '0945678901', N'Thủ kho', N'Đang làm việc'),
(N'Đỗ Văn Minh', N'Nam', '1991-02-14', '0956789012', N'Thu ngân', N'Đang làm việc'),
(N'Bùi Thị Mai', N'Nữ', '1997-08-30', '0967890123', N'Thu ngân', N'Đang làm việc');

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
('thungan1', '123456', 2, 3, N'Hoạt động'),
('thukho1', '123456', 3, 4, N'Hoạt động'),
('thungan2', '123456', 4, 3, N'Hoạt động'),
('thungan3', '123456', 5, 3, N'Hoạt động'),
('thukho2', '123456', 6, 4, N'Hoạt động'),
('thungan4', '123456', 8, 3, N'Hoạt động');

-- 8. Tbl_KhachHang (15 khách hàng)
INSERT INTO Tbl_KhachHang (TenKhachHang, SoDienThoai, DiaChi, Email, DiemTichLuy, TrangThai) VALUES 
(N'Khách lẻ', '0000000000', N'Tại quầy', NULL, 0, N'Hoạt động'),
(N'Nguyễn Văn A', '0987654321', N'123 Lê Lợi, Q1, TP.HCM', 'nguyenvana@email.com', 150, N'Hoạt động'),
(N'Trần Thị B', '0912345679', N'456 Nguyễn Huệ, Q1, TP.HCM', 'tranthib@email.com', 50, N'Hoạt động'),
(N'Lê Văn C', '0923456780', N'789 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', 'levanc@email.com', 200, N'Hoạt động'),
(N'Phạm Thị D', '0934567891', N'321 Võ Văn Tần, Q3, TP.HCM', 'phamthid@email.com', 80, N'Hoạt động'),
(N'Hoàng Văn E', '0945678902', N'654 Cách Mạng Tháng 8, Q10, TP.HCM', 'hoangvane@email.com', 300, N'Hoạt động'),
(N'Võ Thị F', '0956789013', N'987 Lý Thường Kiệt, Q11, TP.HCM', 'vothif@email.com', 120, N'Hoạt động'),
(N'Đỗ Văn G', '0967890124', N'147 Trường Chinh, Q.Tân Phú, TP.HCM', 'dovang@email.com', 90, N'Hoạt động'),
(N'Bùi Thị H', '0978901235', N'258 Tân Hương, Q.Tân Phú, TP.HCM', 'buithih@email.com', 250, N'Hoạt động'),
(N'Ngô Văn I', '0989012346', N'369 Phạm Văn Đồng, Q.Thủ Đức, TP.HCM', 'ngovani@email.com', 180, N'Hoạt động'),
(N'Đinh Thị K', '0990123457', N'741 Xa Lộ Hà Nội, Q.Thủ Đức, TP.HCM', 'dinhthik@email.com', 100, N'Hoạt động'),
(N'Trương Văn L', '0901234568', N'852 Nguyễn Duy Trinh, Q.Thủ Đức, TP.HCM', 'truongvanl@email.com', 220, N'Hoạt động'),
(N'Lý Thị M', '0912345670', N'963 Quốc Lộ 1A, Q.Thủ Đức, TP.HCM', 'lythim@email.com', 70, N'Hoạt động'),
(N'Vương Văn N', '0923456781', N'159 Lê Văn Việt, Q.Thủ Đức, TP.HCM', 'vuongvann@email.com', 160, N'Hoạt động'),
(N'Lưu Thị O', '0934567892', N'357 Võ Văn Ngân, Q.Thủ Đức, TP.HCM', 'luuthio@email.com', 140, N'Hoạt động');

-- 9. Tbl_NhaCungCap (10 nhà cung cấp)
INSERT INTO Tbl_NhaCungCap (TenNhaCungCap, DiaChi, SoDienThoai, Email, TrangThai) VALUES 
(N'Công ty CP Vinamilk', N'Số 10, Tân Trào, Q7, TP.HCM', '02854155555', 'vinamilk@vinamilk.com.vn', N'Hoạt động'),
(N'Công ty TNHH Coca-Cola VN', N'Xa lộ Hà Nội, Thủ Đức, TP.HCM', '02838966999', 'coca@cocacola.com', N'Hoạt động'),
(N'Acecook Việt Nam', N'KCN Tân Bình, TP.HCM', '02838154064', 'info@acecook.com', N'Hoạt động'),
(N'Công ty CP Kinh Đô', N'KCN Tân Thuận, Q7, TP.HCM', '02838730000', 'kinhdo@kinhdo.com.vn', N'Hoạt động'),
(N'Unilever Việt Nam', N'KCN Tân Thuận, Q7, TP.HCM', '02837730000', 'unilever@unilever.com', N'Hoạt động'),
(N'P&G Việt Nam', N'KCN Tân Thuận, Q7, TP.HCM', '02837740000', 'pg@pg.com', N'Hoạt động'),
(N'Công ty CP Sữa TH', N'KCN Bình Dương', '02743888888', 'th@thmilk.com', N'Hoạt động'),
(N'Công ty CP Chinsu', N'KCN Tân Bình, TP.HCM', '02838123456', 'chinsu@chinsu.com', N'Hoạt động'),
(N'Ajinomoto Việt Nam', N'KCN Tân Bình, TP.HCM', '02838123457', 'ajinomoto@ajinomoto.com', N'Hoạt động'),
(N'Công ty CP Thực phẩm Đông lạnh', N'KCN Tân Bình, TP.HCM', '02838123458', 'donglanh@donglanh.com', N'Hoạt động');

-- 10. Tbl_CauHinhLoiNhuan (Cấu hình % lợi nhuận mặc định)
INSERT INTO Tbl_CauHinhLoiNhuan (PhanTramLoiNhuanMacDinh, NgayCapNhat, MaNhanVien) VALUES 
(15.00, GETDATE(), 1);

<<<<<<< HEAD
-- 11. Tbl_QuyTacLoiNhuan (Chỉ có quy tắc TheoSanPham, không còn "Chung" nữa)
-- % mặc định lấy từ Tbl_CauHinhLoiNhuan, không cần quy tắc "Chung"
INSERT INTO Tbl_QuyTacLoiNhuan (LoaiQuyTac, MaLoai, MaThuongHieu, MaDonVi, MaSanPham, PhanTramLoiNhuan, UuTien, TrangThai, NgayTao, NgayCapNhat, MaNhanVien) VALUES 
(N'TheoSanPham', NULL, NULL, NULL, 1, 20.00, 1, N'Hoạt động', GETDATE(), GETDATE(), 1); -- Sữa Vinamilk (SP 1): 20%

-- 12. Tbl_KhoHang (30 sản phẩm - số lượng ban đầu)
INSERT INTO Tbl_KhoHang (MaSanPham, SoLuong, TrangThai) VALUES 
(1, 100, N'Còn hàng'),
(2, 200, N'Còn hàng'),
(3, 150, N'Còn hàng'),
(4, 80, N'Còn hàng'),
(5, 300, N'Còn hàng'),
(6, 120, N'Còn hàng'),
(7, 500, N'Còn hàng'),
(8, 450, N'Còn hàng'),
(9, 200, N'Còn hàng'),
(10, 100, N'Còn hàng'),
(11, 80, N'Còn hàng'),
(12, 60, N'Còn hàng'),
(13, 150, N'Còn hàng'),
(14, 200, N'Còn hàng'),
(15, 180, N'Còn hàng'),
(16, 50, N'Còn hàng'),
(17, 60, N'Còn hàng'),
(18, 70, N'Còn hàng'),
(19, 40, N'Còn hàng'),
(20, 90, N'Còn hàng'),
(21, 50, N'Còn hàng'),
(22, 100, N'Còn hàng'),
(23, 80, N'Còn hàng'),
(24, 30, N'Còn hàng'),
(25, 25, N'Còn hàng'),
(26, 40, N'Còn hàng'),
(27, 60, N'Còn hàng'),
(28, 50, N'Còn hàng'),
(29, 20, N'Còn hàng');

-- 13. Tbl_PhieuNhap (10 phiếu nhập - đã xác nhận thành công)
-- Lưu ý: Giá nhập được lưu trong ChiTietPhieuNhap, giá bán sẽ được tính tự động
INSERT INTO Tbl_PhieuNhap (NgayNhap, MaNhaCungCap, TongTien, TrangThai, LyDoHuy) VALUES 
(DATEADD(month, -2, GETDATE()), 1, 3200000, N'Nhập thành công', NULL), -- PN1: Vinamilk
(DATEADD(month, -1, GETDATE()), 2, 2000000, N'Nhập thành công', NULL), -- PN2: Coca Cola
(DATEADD(month, -1, GETDATE()), 3, 2250000, N'Nhập thành công', NULL), -- PN3: Acecook
(DATEADD(day, -20, GETDATE()), 5, 5000000, N'Nhập thành công', NULL), -- PN4: Unilever
(DATEADD(day, -15, GETDATE()), 1, 640000, N'Nhập thành công', NULL), -- PN5: Vinamilk
(DATEADD(day, -10, GETDATE()), 2, 1500000, N'Nhập thành công', NULL), -- PN6: Coca Cola
(DATEADD(day, -7, GETDATE()), 4, 2500000, N'Nhập thành công', NULL), -- PN7: Kinh Đô
(DATEADD(day, -5, GETDATE()), 8, 2100000, N'Nhập thành công', NULL), -- PN8: Chinsu
(DATEADD(day, -3, GETDATE()), 9, 1680000, N'Nhập thành công', NULL), -- PN9: Ajinomoto
(DATEADD(day, -1, GETDATE()), 10, 1800000, N'Nhập thành công', NULL); -- PN10: Đông lạnh

-- 14. Tbl_ChiTietPhieuNhap (Chi tiết các phiếu nhập với giá nhập)
-- PN1: Vinamilk - Sữa tươi (giá nhập 30k, giá bán sẽ = 30k * 1.20 = 36k)
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(1, 1, 100, 30000, 3000000);

-- PN2: Coca Cola
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(2, 2, 200, 9000, 1800000),
(3, 2, 150, 9000, 1350000);

-- PN3: Acecook - Mì Hảo Hảo
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(7, 3, 500, 4000, 2000000),
(8, 3, 450, 4000, 1800000),
(9, 3, 200, 11000, 2200000);

-- PN4: Unilever - Hóa mỹ phẩm
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(16, 4, 50, 55000, 2750000),
(17, 4, 60, 48000, 2880000),
(18, 4, 70, 38000, 2660000),
(19, 4, 40, 75000, 3000000),
(20, 4, 90, 22000, 1980000);

-- PN5: Vinamilk - Sữa chua
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(4, 5, 80, 7000, 560000),
(6, 5, 120, 6000, 720000);

-- PN6: Coca Cola - Nước suối
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(5, 6, 300, 4500, 1350000);

-- PN7: Kinh Đô - Bánh kẹo
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(10, 7, 100, 22000, 2200000),
(11, 7, 80, 30000, 2400000);

-- PN8: Chinsu - Gia vị
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(12, 8, 60, 30000, 1800000),
(13, 8, 150, 25000, 3750000),
(14, 8, 200, 13000, 2600000),
(15, 8, 180, 16000, 2880000);

-- PN9: Ajinomoto - Gia vị
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(13, 9, 150, 25000, 3750000);

-- PN10: Đông lạnh
INSERT INTO Tbl_ChiTietPhieuNhap (MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien) VALUES 
(27, 10, 60, 40000, 2400000),
(28, 10, 50, 13000, 650000),
(29, 10, 20, 100000, 2000000);

-- 15. Cập nhật giá bán trong Tbl_SanPham dựa trên giá nhập mới nhất và % lợi nhuận
-- Sử dụng công thức: GiaBan = GiaNhap * (1 + LoiNhuan/100)
-- Lưu ý: Chỉ cập nhật nếu giá nhập mới >= giá nhập cũ (theo logic nghiệp vụ)
UPDATE sp
SET sp.GiaBan = CASE 
    WHEN ctpn.DonGiaNhap IS NOT NULL THEN
        -- Chỉ tìm quy tắc TheoSanPham, nếu không có thì dùng % mặc định từ CauHinh
        ROUND(ctpn.DonGiaNhap * (1 + ISNULL((
            SELECT TOP 1 q.PhanTramLoiNhuan
            FROM Tbl_QuyTacLoiNhuan q
            WHERE q.TrangThai = N'Hoạt động'
            AND q.LoaiQuyTac = N'TheoSanPham'
            AND q.MaSanPham = sp.MaSanPham
            ORDER BY q.NgayCapNhat DESC
        ), (SELECT TOP 1 PhanTramLoiNhuanMacDinh FROM Tbl_CauHinhLoiNhuan ORDER BY NgayCapNhat DESC)) / 100), 2)
    ELSE sp.GiaBan
END
FROM Tbl_SanPham sp
OUTER APPLY (
    SELECT TOP 1 ctpn.DonGiaNhap
    FROM Tbl_ChiTietPhieuNhap ctpn
    INNER JOIN Tbl_PhieuNhap pn ON ctpn.MaPhieuNhap = pn.MaPhieuNhap
    WHERE ctpn.MaSanPham = sp.MaSanPham
      AND pn.TrangThai = N'Nhập thành công'
    ORDER BY pn.NgayNhap DESC, ctpn.MaChiTietPhieuNhap DESC
) AS ctpn;

-- 16. Tbl_ChucNang
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

-- 17. Tbl_LoaiQuyen
INSERT INTO Tbl_LoaiQuyen (TenQuyen, MoTa) VALUES 
(N'Xem', N'Quyền xem dữ liệu'),
(N'Thêm', N'Quyền thêm mới dữ liệu'),
(N'Sửa', N'Quyền cập nhật dữ liệu'),
(N'Xóa', N'Quyền xóa dữ liệu'),
(N'In', N'Quyền in ấn (hóa đơn, báo cáo)'),
(N'Xuất Excel', N'Quyền xuất dữ liệu ra file');

-- 18. Tbl_PhanQuyenChiTiet
-- Cấp quyền Full cho Admin (MaQuyen = 1)
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 1, cn.MaChucNang, lq.MaLoaiQuyen, 1
FROM Tbl_ChucNang cn, Tbl_LoaiQuyen lq;

-- Cấp quyền cho Quản lý (MaQuyen = 2): Full quyền trừ Admin
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 2, cn.MaChucNang, lq.MaLoaiQuyen, 1
FROM Tbl_ChucNang cn, Tbl_LoaiQuyen lq
WHERE cn.MaChucNang != 13; -- Trừ Quản Lý

-- Cấp quyền cho Thu Ngân (MaQuyen = 3)
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 3, 2, MaLoaiQuyen, 1 FROM Tbl_LoaiQuyen; -- Bán hàng: Full
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 3, 5, MaLoaiQuyen, 1 FROM Tbl_LoaiQuyen WHERE TenQuyen = N'Xem'; -- Sản phẩm: Xem
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 3, 9, MaLoaiQuyen, 1 FROM Tbl_LoaiQuyen WHERE TenQuyen IN (N'Xem', N'Thêm'); -- Khách hàng: Xem, Thêm

-- Cấp quyền cho Thủ kho (MaQuyen = 4)
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 4, 4, MaLoaiQuyen, 1 FROM Tbl_LoaiQuyen; -- Phiếu nhập: Full
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 4, 6, MaLoaiQuyen, 1 FROM Tbl_LoaiQuyen; -- Kho hàng: Full
INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep)
SELECT 4, 5, MaLoaiQuyen, 1 FROM Tbl_LoaiQuyen WHERE TenQuyen = N'Xem'; -- Sản phẩm: Xem

-- 19. Tbl_HoaDon (20 hóa đơn mẫu)
INSERT INTO Tbl_HoaDon (MaNhanVien, MaKhachHang, TongTien, NgayLap, TrangThai) VALUES 
(2, 1, 42000, DATEADD(day, -30, GETDATE()), N'Đã xuất'),
(2, 2, 65000, DATEADD(day, -29, GETDATE()), N'Đã xuất'),
(4, 3, 85000, DATEADD(day, -28, GETDATE()), N'Đã xuất'),
(2, 4, 120000, DATEADD(day, -27, GETDATE()), N'Đã xuất'),
(5, 5, 45000, DATEADD(day, -26, GETDATE()), N'Đã xuất'),
(4, 6, 95000, DATEADD(day, -25, GETDATE()), N'Đã xuất'),
(2, 7, 55000, DATEADD(day, -24, GETDATE()), N'Đã xuất'),
(5, 8, 78000, DATEADD(day, -23, GETDATE()), N'Đã xuất'),
(4, 9, 110000, DATEADD(day, -22, GETDATE()), N'Đã xuất'),
(2, 10, 68000, DATEADD(day, -21, GETDATE()), N'Đã xuất'),
(5, 11, 52000, DATEADD(day, -20, GETDATE()), N'Đã xuất'),
(4, 12, 88000, DATEADD(day, -19, GETDATE()), N'Đã xuất'),
(2, 13, 72000, DATEADD(day, -18, GETDATE()), N'Đã xuất'),
(5, 14, 105000, DATEADD(day, -17, GETDATE()), N'Đã xuất'),
(4, 15, 48000, DATEADD(day, -16, GETDATE()), N'Đã xuất'),
(2, 1, 62000, DATEADD(day, -15, GETDATE()), N'Đã xuất'),
(5, 2, 92000, DATEADD(day, -14, GETDATE()), N'Đã xuất'),
(4, 3, 58000, DATEADD(day, -13, GETDATE()), N'Đã xuất'),
(2, 4, 115000, DATEADD(day, -12, GETDATE()), N'Đã xuất'),
(5, 5, 75000, DATEADD(day, -11, GETDATE()), N'Đã xuất');

-- 20. Tbl_ChiTietHoaDon (Chi tiết các hóa đơn)
-- Hóa đơn 1-5
INSERT INTO Tbl_ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan) VALUES 
(1, 1, 1, 36000), (1, 2, 1, 10800),
(2, 4, 1, 8400), (2, 16, 1, 68750),
(3, 7, 2, 4720), (3, 8, 1, 4720), (3, 12, 1, 42000),
(4, 1, 2, 36000), (4, 5, 2, 5400), (4, 6, 2, 7200),
(5, 9, 1, 12980), (5, 10, 1, 25950);

-- Hóa đơn 6-10
INSERT INTO Tbl_ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan) VALUES 
(6, 11, 1, 35400), (6, 13, 1, 29500), (6, 14, 1, 15340),
(7, 15, 1, 18400), (7, 17, 1, 60000), (7, 20, 1, 27500),
(8, 2, 3, 10800), (8, 3, 2, 10800), (8, 5, 2, 5400),
(9, 16, 1, 68750), (9, 18, 1, 47500), (9, 19, 1, 93750),
(10, 7, 5, 4720), (10, 8, 3, 4720), (10, 9, 2, 12980);

-- Hóa đơn 11-15
INSERT INTO Tbl_ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan) VALUES 
(11, 12, 1, 42000), (11, 13, 1, 29500),
(12, 1, 1, 36000), (12, 4, 2, 8400), (12, 6, 3, 7200),
(13, 10, 1, 25950), (13, 11, 1, 35400),
(14, 16, 1, 68750), (14, 17, 1, 60000), (14, 21, 1, 43750),
(15, 2, 2, 10800), (15, 3, 2, 10800), (15, 5, 2, 5400);

-- Hóa đơn 16-20
INSERT INTO Tbl_ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan) VALUES 
(16, 7, 3, 4720), (16, 8, 2, 4720), (16, 9, 1, 12980), (16, 12, 1, 42000),
(17, 1, 1, 36000), (17, 4, 1, 8400), (17, 6, 2, 7200), (17, 10, 1, 25950),
(18, 13, 1, 29500), (18, 14, 1, 15340), (18, 15, 1, 18400),
(19, 16, 1, 68750), (19, 17, 1, 60000), (19, 18, 1, 47500),
(20, 2, 3, 10800), (20, 3, 2, 10800), (20, 5, 2, 5400), (20, 6, 2, 7200);

-- 21. Tbl_KhuyenMai (5 chương trình khuyến mãi)
INSERT INTO Tbl_KhuyenMai (MaSanPham, TenKhuyenMai, PhanTramGiamGia, NgayBatDau, NgayKetThuc, MoTa) VALUES 
(2, N'Mua hè sảng khoái', 10.00, '2025-06-01', '2025-08-31', N'Giảm giá nước ngọt Coca Cola'),
(3, N'Pepsi mùa hè', 10.00, '2025-06-01', '2025-08-31', N'Giảm giá nước ngọt Pepsi'),
(7, N'Combo mì Hảo Hảo', 15.00, '2025-01-01', '2025-12-31', N'Giảm giá mì Hảo Hảo'),
(16, N'Dầu gội Clear giảm giá', 20.00, '2025-01-01', '2025-03-31', N'Giảm giá dầu gội Clear'),
(1, N'Sữa Vinamilk khuyến mãi', 5.00, '2025-01-01', '2025-06-30', N'Giảm giá sữa Vinamilk');

-- 22. Tbl_LichSuTichDiem (Lịch sử tích điểm của khách hàng)
INSERT INTO Tbl_LichSuTichDiem (MaKhachHang, MaHoaDon, DiemCong, DiemSuDung, NgayCapNhat, MoTa) VALUES 
(2, 1, 4, 0, DATEADD(day, -30, GETDATE()), N'Tích điểm từ hóa đơn 1'),
(2, 17, 9, 0, DATEADD(day, -14, GETDATE()), N'Tích điểm từ hóa đơn 17'),
(3, 3, 8, 0, DATEADD(day, -28, GETDATE()), N'Tích điểm từ hóa đơn 3'),
(4, 4, 12, 0, DATEADD(day, -27, GETDATE()), N'Tích điểm từ hóa đơn 4'),
(5, 5, 4, 0, DATEADD(day, -26, GETDATE()), N'Tích điểm từ hóa đơn 5'),
(6, 6, 9, 0, DATEADD(day, -25, GETDATE()), N'Tích điểm từ hóa đơn 6'),
(7, 7, 5, 0, DATEADD(day, -24, GETDATE()), N'Tích điểm từ hóa đơn 7'),
(8, 8, 7, 0, DATEADD(day, -23, GETDATE()), N'Tích điểm từ hóa đơn 8'),
(9, 9, 11, 0, DATEADD(day, -22, GETDATE()), N'Tích điểm từ hóa đơn 9'),
(10, 10, 6, 0, DATEADD(day, -21, GETDATE()), N'Tích điểm từ hóa đơn 10');

-- 23. Tbl_LichSuThayDoiKho (Lịch sử thay đổi kho)
INSERT INTO Tbl_LichSuThayDoiKho (MaSanPham, SoLuongCu, SoLuongMoi, ChenhLech, LoaiThayDoi, LyDo, GhiChu, MaNhanVien, NgayThayDoi) VALUES 
(1, 0, 100, 100, N'Nhập hàng', N'Nhập hàng đầu kỳ', N'Nhập hàng từ nhà cung cấp Vinamilk', 3, DATEADD(month, -2, GETDATE())),
(2, 0, 200, 200, N'Nhập hàng', N'Nhập hàng đầu kỳ', N'Nhập hàng từ nhà cung cấp Coca Cola', 3, DATEADD(month, -1, GETDATE())),
(3, 0, 150, 150, N'Nhập hàng', N'Nhập hàng đầu kỳ', N'Nhập hàng từ nhà cung cấp Coca Cola', 3, DATEADD(month, -1, GETDATE())),
(7, 0, 500, 500, N'Nhập hàng', N'Nhập hàng đầu kỳ', N'Nhập hàng từ nhà cung cấp Acecook', 3, DATEADD(month, -1, GETDATE())),
(16, 0, 50, 50, N'Nhập hàng', N'Nhập hàng đầu kỳ', N'Nhập hàng từ nhà cung cấp Unilever', 3, DATEADD(day, -20, GETDATE())),
(1, 100, 80, -20, N'Bán hàng', N'Xuất kho do bán hàng', N'Bán 20 sản phẩm', 2, DATEADD(day, -15, GETDATE())),
(2, 200, 195, -5, N'Bán hàng', N'Xuất kho do bán hàng', N'Bán 5 sản phẩm', 2, DATEADD(day, -10, GETDATE()));

PRINT N'Hoàn tất! Đã insert dữ liệu mẫu cho hệ thống.';
GO
