using System;

namespace mini_supermarket.DTO
{
    public class NhanVienDTO
    {
        public int MaNhanVien { get; set; }
        public string? TenNhanVien { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? VaiTro { get; set; }
        public string? TrangThai { get; set; }

        public NhanVienDTO()
        {
        }

        public NhanVienDTO(
            int maNhanVien,
            string? tenNhanVien,
            string? gioiTinh,
            DateTime? ngaySinh,
            string? soDienThoai,
            string? vaiTro,
            string? trangThai)
        {
            MaNhanVien = maNhanVien;
            TenNhanVien = tenNhanVien;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            SoDienThoai = soDienThoai;
            VaiTro = vaiTro;
            TrangThai = trangThai;
        }
    }
}
