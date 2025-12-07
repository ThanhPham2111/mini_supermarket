using System;

namespace mini_supermarket.DTO
{
    public class SanPhamDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public int MaDonVi { get; set; }
        public string? TenDonVi { get; set; }
        public int MaThuongHieu { get; set; }
        public int MaLoai { get; set; }
        public string? MoTa { get; set; }
        public decimal? GiaBan { get; set; }
        public string? HinhAnh { get; set; }
        public string? XuatXu { get; set; }
        public DateTime? Hsd { get; set; }
        public string? TrangThai { get; set; }
        public string? TenLoai { get; set; }
        public string? TenThuongHieu { get; set; }
        public int? SoLuong { get; set; }  // Số lượng tồn kho
        public decimal? PhanTramGiam { get; set; } // Phần trăm giảm giá
        public string? KhuyenMai { get; set; } // Tên khuyến mãi

        public SanPhamDTO()
        {
        }

        public SanPhamDTO(
            int maSanPham,
            string tenSanPham,
            int maDonVi,
            string? tenDonVi,
            int maThuongHieu,
            int maLoai,
            string? moTa,
            decimal? giaBan,
            string? hinhAnh,
            string? xuatXu,
            DateTime? hsd,
            string? trangThai,
            string? tenLoai,
            string? tenThuongHieu,
            decimal? phanTramGiam,
            string? khuyenMai)
        {
            MaSanPham = maSanPham;
            TenSanPham = tenSanPham;
            MaDonVi = maDonVi;
            TenDonVi = tenDonVi;
            MaThuongHieu = maThuongHieu;
            MaLoai = maLoai;
            MoTa = moTa;
            GiaBan = giaBan;
            HinhAnh = hinhAnh;
            XuatXu = xuatXu;
            Hsd = hsd;
            TrangThai = trangThai;
            TenLoai = tenLoai;
            TenThuongHieu = tenThuongHieu;
            PhanTramGiam = phanTramGiam;
            KhuyenMai = khuyenMai;
        }
    }
}
