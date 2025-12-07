using System;

namespace mini_supermarket.DTO
{
    public class GiaSanPhamDTO
    {
        private int _maGia;
        private int _maSanPham;
        private decimal _giaNhap;
        private decimal _giaNhapGoc;
        private decimal _phanTramLoiNhuanApDung;
        private decimal _giaBan;
        private DateTime? _ngayCapNhat;
        private string? _tenSanPham;
        private string? _tenLoai;
        private string? _tenThuongHieu;
        private string? _tenDonVi;

        public GiaSanPhamDTO()
        {
        }

        public GiaSanPhamDTO(
            int maGia,
            int maSanPham,
            decimal giaNhap,
            decimal giaNhapGoc,
            decimal phanTramLoiNhuanApDung,
            decimal giaBan,
            DateTime? ngayCapNhat,
            string? tenSanPham,
            string? tenLoai,
            string? tenThuongHieu,
            string? tenDonVi)
        {
            _maGia = maGia;
            _maSanPham = maSanPham;
            _giaNhap = giaNhap;
            _giaNhapGoc = giaNhapGoc;
            _phanTramLoiNhuanApDung = phanTramLoiNhuanApDung;
            _giaBan = giaBan;
            _ngayCapNhat = ngayCapNhat;
            _tenSanPham = tenSanPham;
            _tenLoai = tenLoai;
            _tenThuongHieu = tenThuongHieu;
            _tenDonVi = tenDonVi;
        }

        public int MaGia
        {
            get { return _maGia; }
            set { _maGia = value; }
        }

        public int MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }

        public decimal GiaNhap
        {
            get { return _giaNhap; }
            set { _giaNhap = value; }
        }

        public decimal GiaNhapGoc
        {
            get { return _giaNhapGoc; }
            set { _giaNhapGoc = value; }
        }

        public decimal PhanTramLoiNhuanApDung
        {
            get { return _phanTramLoiNhuanApDung; }
            set { _phanTramLoiNhuanApDung = value; }
        }

        public decimal GiaBan
        {
            get { return _giaBan; }
            set { _giaBan = value; }
        }

        public DateTime? NgayCapNhat
        {
            get { return _ngayCapNhat; }
            set { _ngayCapNhat = value; }
        }

        public string? TenSanPham
        {
            get { return _tenSanPham; }
            set { _tenSanPham = value; }
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
    }
}
