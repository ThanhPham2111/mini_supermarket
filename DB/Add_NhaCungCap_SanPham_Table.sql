-- =============================================
-- TẠO BẢNG Tbl_NhaCungCap_SanPham
-- Bảng liên kết nhiều-nhiều giữa Nhà cung cấp và Sản phẩm
-- =============================================

USE mini_sp01;
GO

-- Tạo bảng Tbl_NhaCungCap_SanPham
CREATE TABLE Tbl_NhaCungCap_SanPham (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaNhaCungCap INT NOT NULL,
    MaSanPham INT NOT NULL,
    FOREIGN KEY (MaNhaCungCap) REFERENCES Tbl_NhaCungCap(MaNhaCungCap),
    FOREIGN KEY (MaSanPham) REFERENCES Tbl_SanPham(MaSanPham),
    CONSTRAINT UK_NhaCungCap_SanPham UNIQUE (MaNhaCungCap, MaSanPham)
);
GO

-- =============================================
-- INSERT DỮ LIỆU GIẢ CHO Tbl_NhaCungCap_SanPham
-- Phân bổ sản phẩm cho nhà cung cấp dựa trên:
-- 1. Dữ liệu phiếu nhập hiện có
-- 2. Thương hiệu sản phẩm
-- 3. Loại sản phẩm
-- =============================================

-- Nhà cung cấp 1: Vinamilk (MaNhaCungCap = 1)
-- Sản phẩm: Sữa Vinamilk (SP 1, 4, 6)
INSERT INTO Tbl_NhaCungCap_SanPham (MaNhaCungCap, MaSanPham) VALUES 
(1, 1),  -- Sữa tươi Vinamilk 1L
(1, 4),  -- Sữa chua Vinamilk 100g
(1, 6);  -- Yomost dâu 110ml (có thể từ Vinamilk hoặc TH True Milk)

-- Nhà cung cấp 2: Coca Cola (MaNhaCungCap = 2)
-- Sản phẩm: Coca Cola, Pepsi, Nước suối (SP 2, 3, 5)
INSERT INTO Tbl_NhaCungCap_SanPham (MaNhaCungCap, MaSanPham) VALUES 
(2, 2),  -- Coca Cola 330ml
(2, 3),  -- Pepsi 330ml
(2, 5);  -- Nước suối Lavie 500ml

-- Nhà cung cấp 3: Acecook (MaNhaCungCap = 3)
-- Sản phẩm: Mì Hảo Hảo, Snack (SP 7, 8, 9)
INSERT INTO Tbl_NhaCungCap_SanPham (MaNhaCungCap, MaSanPham) VALUES 
(3, 7),  -- Mì Hảo Hảo Tôm Chua Cay
(3, 8),  -- Mì Hảo Hảo Chay
(3, 9);  -- Snack Oishi tôm cay

-- Nhà cung cấp 4: Kinh Đô (MaNhaCungCap = 4)
-- Sản phẩm: Bánh kẹo (SP 10, 11)
INSERT INTO Tbl_NhaCungCap_SanPham (MaNhaCungCap, MaSanPham) VALUES 
(4, 10), -- Bánh quy Kinh Đô bơ
(4, 11); -- Bánh quy Oreo (có thể từ Nestlé nhưng Kinh Đô cũng phân phối)

-- Nhà cung cấp 5: Unilever (MaNhaCungCap = 5)
-- Sản phẩm: Hóa mỹ phẩm, vệ sinh cá nhân (SP 16, 17, 18, 19, 20, 21, 22, 23)
INSERT INTO Tbl_NhaCungCap_SanPham (MaNhaCungCap, MaSanPham) VALUES 
(5, 16), -- Dầu gội Clear Men 400ml
(5, 17), -- Dầu gội Sunsilk 400ml
(5, 18), -- Sữa tắm Lifebuoy 400ml
(5, 19), -- Bột giặt Omo 1kg
(5, 20), -- Nước rửa chén Sunlight 500ml
(5, 21), -- Kem đánh răng P/S 150g
(5, 22), -- Bàn chải đánh răng P/S
(5, 23); -- Khăn giấy Pulppy 2 lớp

-- Nhà cung cấp 6: P&G (MaNhaCungCap = 6)
-- Sản phẩm: Vệ sinh cá nhân (SP 21, 22) - có thể từ P&G hoặc Unilever
-- (Đã thêm vào Unilever ở trên, có thể thêm vào P&G nếu muốn đa dạng)

-- Nhà cung cấp 7: TH True Milk (MaNhaCungCap = 7)
-- Sản phẩm: Sữa TH (SP 6 có thể từ TH True Milk)
INSERT INTO Tbl_NhaCungCap_SanPham (MaNhaCungCap, MaSanPham) VALUES 
(7, 6);  -- Yomost dâu 110ml (có thể từ TH True Milk)

-- Nhà cung cấp 8: Chinsu (MaNhaCungCap = 8)
-- Sản phẩm: Gia vị (SP 12)
INSERT INTO Tbl_NhaCungCap_SanPham (MaNhaCungCap, MaSanPham) VALUES 
(8, 12); -- Nước mắm Chinsu 500ml

-- Nhà cung cấp 9: Ajinomoto (MaNhaCungCap = 9)
-- Sản phẩm: Gia vị (SP 13, 14)
INSERT INTO Tbl_NhaCungCap_SanPham (MaNhaCungCap, MaSanPham) VALUES 
(9, 13), -- Hạt nêm Ajinomoto 450g
(9, 14), -- Bột canh Knorr 200g (có thể từ Unilever nhưng Ajinomoto cũng phân phối)
(9, 15); -- Đường trắng 1kg (có thể từ nhiều nhà cung cấp)

-- Nhà cung cấp 10: Đông lạnh (MaNhaCungCap = 10)
-- Sản phẩm: Đồ đông lạnh, thực phẩm tươi sống (SP 24, 25, 26, 27, 28, 29)
INSERT INTO Tbl_NhaCungCap_SanPham (MaNhaCungCap, MaSanPham) VALUES 
(10, 24), -- Thịt heo ba chỉ
(10, 25), -- Cá basa fillet
(10, 26), -- Rau cải xanh
(10, 27), -- Kem Wall's Vani
(10, 28), -- Kem Wall's Socola
(10, 29); -- Chả cá viên

-- Phân bổ thêm một số sản phẩm cho các nhà cung cấp khác để đa dạng
-- Một số sản phẩm có thể được cung cấp bởi nhiều nhà cung cấp

-- Thêm một số sản phẩm cho nhà cung cấp khác (tùy chọn)
-- Ví dụ: Bánh quy Oreo có thể từ Nestlé (nhưng Nestlé không có trong danh sách)
-- Nước suối có thể từ nhiều nhà cung cấp
-- Gia vị có thể từ nhiều nhà cung cấp

PRINT N'Hoàn tất! Đã tạo bảng Tbl_NhaCungCap_SanPham và insert dữ liệu mẫu.';
GO

