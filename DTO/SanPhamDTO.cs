using System;

namespace mini_supermarket.DTO
{
    public class SanPhamDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string? DonVi { get; set; }
        public int MaThuongHieu { get; set; }
        public int MaLoai { get; set; }
        public string? MoTa { get; set; }
        public decimal? GiaBan { get; set; }
        public string? HinhAnh { get; set; }
        public string? XuatXu { get; set; }
        public DateTime? Hsd { get; set; }
        public string? TrangThai { get; set; }
        public string? TenLoai { get; set; } // New property for Category Name
        public string? TenThuongHieu { get; set; } // New property for Brand Name

        public SanPhamDTO()
        {
        }

        public SanPhamDTO(
            int maSanPham,
            string tenSanPham,
            string? donVi,
            int maThuongHieu,
            int maLoai,
            string? moTa,
            decimal? giaBan,
            string? hinhAnh,
            string? xuatXu,
            DateTime? hsd,
            string? trangThai,
            string? tenLoai,
            string? tenThuongHieu)
        {
            MaSanPham = maSanPham;
            TenSanPham = tenSanPham;
            DonVi = donVi;
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
        }
    }
}