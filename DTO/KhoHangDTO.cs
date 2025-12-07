namespace mini_supermarket.DTO
{
    public class KhoHangDTO
    {
        private int _maSanPham;
        private string? _tenSanPham;
        private int? _soLuong;
        private string? _trangThai;
        private string? _trangThaiDieuKien;
        private decimal? _giaNhap;
        private decimal? _giaBan;

        public int MaSanPham
        {
            get { return _maSanPham; }
            set { _maSanPham = value; }
        }

        public string? TenSanPham
        {
            get { return _tenSanPham; }
            set { _tenSanPham = value; }
        }

        public int? SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; }
        }

        public string? TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; }
        }

        public string? TrangThaiDieuKien
        {
            get { return _trangThaiDieuKien; }
            set { _trangThaiDieuKien = value; }
        }

        public decimal? GiaNhap
        {
            get { return _giaNhap; }
            set { _giaNhap = value; }
        }

        public decimal? GiaBan
        {
            get { return _giaBan; }
            set { _giaBan = value; }
        }

        public KhoHangDTO()
        {
        }

        public KhoHangDTO(int maSanPham, int? soLuong, string? trangThai, string? trangThaiDieuKien = "Bán", decimal? giaNhap = null, decimal? giaBan = null)
        {
            _maSanPham = maSanPham;
            _soLuong = soLuong;
            _trangThai = trangThai;
            _trangThaiDieuKien = trangThaiDieuKien;
            _giaNhap = giaNhap;
            _giaBan = giaBan;
        }
    }
}
