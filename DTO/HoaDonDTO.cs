using System;

namespace mini_supermarket.DTO
{
    public class HoaDonDTO
    {
        // Instance Fields
        private int _maHoaDon;
        private string _maHoaDonCode;
        private DateTime? _ngayLap;
        private int _maNhanVien;
        private int? _maKhachHang;
        private decimal? _tongTien;
        private string _nhanVien;
        private string _khachHang;
        private string _trangThai;

        // Properties
        public int MaHoaDon 
        { 
            get => _maHoaDon; 
            set => _maHoaDon = value; 
        }

        public string MaHoaDonCode 
        { 
            get => _maHoaDonCode; 
            set => _maHoaDonCode = value; 
        }

        public DateTime? NgayLap 
        { 
            get => _ngayLap; 
            set => _ngayLap = value; 
        }

        public int MaNhanVien 
        { 
            get => _maNhanVien; 
            set => _maNhanVien = value; 
        }

        public int? MaKhachHang 
        { 
            get => _maKhachHang; 
            set => _maKhachHang = value; 
        }

        public decimal? TongTien 
        { 
            get => _tongTien; 
            set => _tongTien = value; 
        }

        // Properties để hiển thị tên nhân viên và khách hàng
        public string NhanVien 
        { 
            get => _nhanVien; 
            set => _nhanVien = value; 
        }

        public string KhachHang 
        { 
            get => _khachHang; 
            set => _khachHang = value; 
        }

        public decimal? ThanhTien => _tongTien;

        public string TrangThai 
        { 
            get => _trangThai; 
            set => _trangThai = value; 
        }

        // Constructors
        public HoaDonDTO()
        {
            _maHoaDonCode = string.Empty;
            _nhanVien = string.Empty;
            _khachHang = string.Empty;
            _trangThai = string.Empty;
        }

        public HoaDonDTO(
            int maHoaDon,
            string maHoaDonCode,
            DateTime? ngayLap,
            int maNhanVien,
            int? maKhachHang,
            decimal? tongTien,
            string trangThai)
        {
            _maHoaDon = maHoaDon;
            _maHoaDonCode = maHoaDonCode ?? string.Empty;
            _ngayLap = ngayLap;
            _maNhanVien = maNhanVien;
            _maKhachHang = maKhachHang;
            _tongTien = tongTien;
            _trangThai = trangThai ?? string.Empty;
            _nhanVien = string.Empty;
            _khachHang = string.Empty;
        }
    }
}
