using System;

namespace mini_supermarket.DTO
{
    public class NhanVienDTO
    {
        private int _maNhanVien;
        private string _tenNhanVien = string.Empty;
        private string? _gioiTinh;
        private DateTime? _ngaySinh;
        private string? _soDienThoai;
        private string? _vaiTro;
        private string? _trangThai;

        public NhanVienDTO()
        {
        }

        public NhanVienDTO(
            int maNhanVien,
            string tenNhanVien,
            string? gioiTinh,
            DateTime? ngaySinh,
            string? soDienThoai,
            string? vaiTro,
            string? trangThai)
        {
            _maNhanVien = maNhanVien;
            _tenNhanVien = tenNhanVien;
            _gioiTinh = gioiTinh;
            _ngaySinh = ngaySinh;
            _soDienThoai = soDienThoai;
            _vaiTro = vaiTro;
            _trangThai = trangThai;
        }

        public int MaNhanVien
        {
            get { return _maNhanVien; }
            set { _maNhanVien = value; }
        }

        public string TenNhanVien
        {
            get { return _tenNhanVien; }
            set { _tenNhanVien = value; }
        }

        public string? GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; }
        }

        public DateTime? NgaySinh
        {
            get { return _ngaySinh; }
            set { _ngaySinh = value; }
        }

        public string? SoDienThoai
        {
            get { return _soDienThoai; }
            set { _soDienThoai = value; }
        }

        public string? VaiTro
        {
            get { return _vaiTro; }
            set { _vaiTro = value; }
        }

        public string? TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }
    }
}
