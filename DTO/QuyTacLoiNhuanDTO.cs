using System;

namespace mini_supermarket.DTO
{
    public class QuyTacLoiNhuanDTO
    {
        private int _maQuyTac;
        private string _loaiQuyTac = string.Empty;
        private int? _maLoai;
        private int? _maThuongHieu;
        private int? _maDonVi;
        private int? _maSanPham;
        private decimal _phanTramLoiNhuan;
        private int _uuTien;
        private string? _trangThai;
        private DateTime? _ngayTao;
        private DateTime? _ngayCapNhat;
        private int? _maNhanVien;
        private string? _tenLoai;
        private string? _tenThuongHieu;
        private string? _tenDonVi;
        private string? _tenSanPham;

        public QuyTacLoiNhuanDTO()
        {
        }

        public QuyTacLoiNhuanDTO(
            int maQuyTac,
            string loaiQuyTac,
            int? maLoai,
            int? maThuongHieu,
            int? maDonVi,
            int? maSanPham,
            decimal phanTramLoiNhuan,
            int uuTien,
            string? trangThai,
            DateTime? ngayTao,
            DateTime? ngayCapNhat,
            int? maNhanVien,
            string? tenLoai,
            string? tenThuongHieu,
            string? tenDonVi,
            string? tenSanPham)
        {
            _maQuyTac = maQuyTac;
            _loaiQuyTac = loaiQuyTac;
            _maLoai = maLoai;
            _maThuongHieu = maThuongHieu;
            _maDonVi = maDonVi;
            _maSanPham = maSanPham;
            _phanTramLoiNhuan = phanTramLoiNhuan;
            _uuTien = uuTien;
            _trangThai = trangThai;
            _ngayTao = ngayTao;
            _ngayCapNhat = ngayCapNhat;
            _maNhanVien = maNhanVien;
            _tenLoai = tenLoai;
            _tenThuongHieu = tenThuongHieu;
            _tenDonVi = tenDonVi;
            _tenSanPham = tenSanPham;
        }

        public int MaQuyTac
        {
            get { return _maQuyTac; }
            set { _maQuyTac = value; }
        }

        public string LoaiQuyTac
        {
            get { return _loaiQuyTac; }
            set { _loaiQuyTac = value; }
        }

        public int? MaLoai
        {
            get { return _maLoai; }
            set { _maLoai = value; }
        }

        public int? MaThuongHieu
        {
            get { return _maThuongHieu; }
            set { _maThuongHieu = value; }
        }

        public int? MaDonVi
        {
            get { return _maDonVi; }
            set { _maDonVi = value; }
        }

        public int? MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }

        public decimal PhanTramLoiNhuan
        {
            get { return _phanTramLoiNhuan; }
            set { _phanTramLoiNhuan = value; }
        }

        public int UuTien
        {
            get { return _uuTien; }
            set { _uuTien = value; }
        }

        public string? TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }

        public DateTime? NgayTao
        {
            get { return _ngayTao; }
            set { _ngayTao = value; }
        }

        public DateTime? NgayCapNhat
        {
            get { return _ngayCapNhat; }
            set { _ngayCapNhat = value; }
        }

        public int? MaNhanVien
        {
            get { return _maNhanVien; }
            set { _maNhanVien = value; }
        }

        public string? TenLoai
        {
            get { return _tenLoai; }
            set { _tenLoai = value; }
        }

        public string? TenThuongHieu
        {
            get { return _tenThuongHieu; }
            set { _tenThuongHieu = value; }
        }

        public string? TenDonVi
        {
            get { return _tenDonVi; }
            set { _tenDonVi = value; }
        }

        public string? TenSanPham
        {
            get { return _tenSanPham; }
            set { _tenSanPham = value; }
        }
    }
}
