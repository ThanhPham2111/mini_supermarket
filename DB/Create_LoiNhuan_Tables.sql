-- =============================================
-- Script tạo các bảng quản lý lợi nhuận
-- Chạy script này trên database mini_sp01 đã có sẵn
-- =============================================

USE mini_sp01;
GO

-- 1. Bảng cấu hình lợi nhuận mặc định (chung cho toàn hệ thống)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tbl_CauHinhLoiNhuan')
BEGIN
    CREATE TABLE Tbl_CauHinhLoiNhuan (
        MaCauHinh INT PRIMARY KEY IDENTITY(1,1),
        PhanTramLoiNhuanMacDinh DECIMAL(5,2) DEFAULT 10.00, -- % lợi nhuận mặc định
        NgayCapNhat DATETIME DEFAULT GETDATE(),
        MaNhanVien INT, -- Người cập nhật
        FOREIGN KEY (MaNhanVien) REFERENCES Tbl_NhanVien(MaNhanVien)
    );
    
    -- Insert giá trị mặc định (chỉ nếu bảng mới tạo và chưa có dữ liệu)
    IF NOT EXISTS (SELECT * FROM Tbl_CauHinhLoiNhuan)
    BEGIN
        INSERT INTO Tbl_CauHinhLoiNhuan (PhanTramLoiNhuanMacDinh, MaNhanVien) 
        VALUES (10.00, 1);
    END
    
    PRINT N'Đã tạo bảng Tbl_CauHinhLoiNhuan';
END
ELSE
BEGIN
    PRINT N'Bảng Tbl_CauHinhLoiNhuan đã tồn tại';
END
GO

-- 2. Bảng quy tắc lợi nhuận (hỗ trợ nhiều cấp độ)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tbl_QuyTacLoiNhuan')
BEGIN
    CREATE TABLE Tbl_QuyTacLoiNhuan (
        MaQuyTac INT PRIMARY KEY IDENTITY(1,1),
        LoaiQuyTac NVARCHAR(50) NOT NULL, -- 'Chung', 'TheoLoai', 'TheoThuongHieu', 'TheoDonVi', 'TheoSanPham'
        MaLoai INT NULL, -- Nếu LoaiQuyTac = 'TheoLoai'
        MaThuongHieu INT NULL, -- Nếu LoaiQuyTac = 'TheoThuongHieu'
        MaDonVi INT NULL, -- Nếu LoaiQuyTac = 'TheoDonVi'
        MaSanPham INT NULL, -- Nếu LoaiQuyTac = 'TheoSanPham'
        PhanTramLoiNhuan DECIMAL(5,2) NOT NULL, -- % lợi nhuận
        UuTien INT DEFAULT 0, -- Độ ưu tiên: SanPham(4) > DonVi(3) > ThuongHieu(2) > Loai(1) > Chung(0)
        TrangThai NVARCHAR(50) DEFAULT N'Hoạt động',
        NgayTao DATETIME DEFAULT GETDATE(),
        NgayCapNhat DATETIME DEFAULT GETDATE(),
        MaNhanVien INT,
        FOREIGN KEY (MaLoai) REFERENCES Tbl_Loai(MaLoai),
        FOREIGN KEY (MaThuongHieu) REFERENCES Tbl_ThuongHieu(MaThuongHieu),
        FOREIGN KEY (MaDonVi) REFERENCES Tbl_DonVi(MaDonVi),
        FOREIGN KEY (MaSanPham) REFERENCES Tbl_SanPham(MaSanPham),
        FOREIGN KEY (MaNhanVien) REFERENCES Tbl_NhanVien(MaNhanVien)
    );
    
    -- Tạo index để tăng tốc truy vấn
    CREATE INDEX IDX_QuyTacLoiNhuan_LoaiQuyTac ON Tbl_QuyTacLoiNhuan(LoaiQuyTac, TrangThai);
    CREATE INDEX IDX_QuyTacLoiNhuan_MaSanPham ON Tbl_QuyTacLoiNhuan(MaSanPham) WHERE MaSanPham IS NOT NULL;
    CREATE INDEX IDX_QuyTacLoiNhuan_MaLoai ON Tbl_QuyTacLoiNhuan(MaLoai) WHERE MaLoai IS NOT NULL;
    CREATE INDEX IDX_QuyTacLoiNhuan_MaThuongHieu ON Tbl_QuyTacLoiNhuan(MaThuongHieu) WHERE MaThuongHieu IS NOT NULL;
    CREATE INDEX IDX_QuyTacLoiNhuan_MaDonVi ON Tbl_QuyTacLoiNhuan(MaDonVi) WHERE MaDonVi IS NOT NULL;
    
    PRINT N'Đã tạo bảng Tbl_QuyTacLoiNhuan';
END
ELSE
BEGIN
    PRINT N'Bảng Tbl_QuyTacLoiNhuan đã tồn tại';
END
GO

-- 3. Bảng lưu giá nhập và giá bán thực tế của từng sản phẩm
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tbl_GiaSanPham')
BEGIN
    CREATE TABLE Tbl_GiaSanPham (
        MaGia INT PRIMARY KEY IDENTITY(1,1),
        MaSanPham INT NOT NULL UNIQUE,
        GiaNhap DECIMAL(18, 2) NOT NULL, -- Giá nhập hiện tại
        GiaNhapGoc DECIMAL(18, 2) NOT NULL, -- Giá nhập gốc khi set lợi nhuận (để so sánh)
        PhanTramLoiNhuanApDung DECIMAL(5,2) NOT NULL, -- % lợi nhuận đang áp dụng
        GiaBan DECIMAL(18, 2) NOT NULL, -- Giá bán = GiaNhapGoc * (1 + PhanTramLoiNhuanApDung/100)
        NgayCapNhat DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (MaSanPham) REFERENCES Tbl_SanPham(MaSanPham)
    );
    
    CREATE INDEX IDX_GiaSanPham_MaSanPham ON Tbl_GiaSanPham(MaSanPham);
    
    PRINT N'Đã tạo bảng Tbl_GiaSanPham';
END
ELSE
BEGIN
    PRINT N'Bảng Tbl_GiaSanPham đã tồn tại';
END
GO

PRINT N'Hoàn tất! Đã tạo tất cả các bảng quản lý lợi nhuận.';
GO

