using System;
using System.Collections.Generic;

namespace mini_supermarket.DTO
{
    public class PhieuNhapDTO
    {
        private int _maPhieuNhap;
        private DateTime? _ngayNhap;
        private int _maNhaCungCap;
        private decimal? _tongTien;
        private string? _trangThai;
        private string? _lyDoHuy;
        private ICollection<ChiTietPhieuNhapDTO> _chiTietPhieuNhaps = new List<ChiTietPhieuNhapDTO>();

        public int MaPhieuNhap
        {
            get { return _maPhieuNhap; }
            set { _maPhieuNhap = value; }
        }

        public DateTime? NgayNhap
        {
            get { return _ngayNhap; }
            set { _ngayNhap = value; }
        }

        public int MaNhaCungCap
        {
            get { return _maNhaCungCap; }
            set { _maNhaCungCap = value; }
        }

        public decimal? TongTien
        {
            get { return _tongTien; }
            set { _tongTien = value; }
        }

        public string? TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }

        public string? LyDoHuy
        {
            get { return _lyDoHuy; }
            set { _lyDoHuy = value; }
        }

        public ICollection<ChiTietPhieuNhapDTO> ChiTietPhieuNhaps
        {
            get { return _chiTietPhieuNhaps; }
            set { _chiTietPhieuNhaps = value; }
        }

        public PhieuNhapDTO()
        {
        }

        public PhieuNhapDTO(
            int maPhieuNhap,
            DateTime? ngayNhap,
            int maNhaCungCap,
            decimal? tongTien,
            string? trangThai = null)
        {
            _maPhieuNhap = maPhieuNhap;
            _ngayNhap = ngayNhap;
            _maNhaCungCap = maNhaCungCap;
            _tongTien = tongTien;
            _trangThai = trangThai;
        }
    }
}
