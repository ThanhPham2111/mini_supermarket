using System;

namespace mini_supermarket.DTO
{
    public class LichSuTichDiemDTO
    {
        private int _maLichSuTichDiem;
        private int _maKhachHang;
        private int? _maHoaDon;
        private int? _diemCong;
        private int? _diemSuDung;
        private DateTime? _ngayCapNhat;
        private string? _moTa;

        public LichSuTichDiemDTO()
        {
        }

        public LichSuTichDiemDTO(
            int maLichSuTichDiem,
            int maKhachHang,
            int? maHoaDon,
            int? diemCong,
            int? diemSuDung,
            DateTime? ngayCapNhat,
            string? moTa)
        {
            _maLichSuTichDiem = maLichSuTichDiem;
            _maKhachHang = maKhachHang;
            _maHoaDon = maHoaDon;
            _diemCong = diemCong;
            _diemSuDung = diemSuDung;
            _ngayCapNhat = ngayCapNhat;
            _moTa = moTa;
        }

        public int MaLichSuTichDiem
        {
            get { return _maLichSuTichDiem; }
            set { _maLichSuTichDiem = value; }
        }

        public int MaKhachHang
        {
            get { return _maKhachHang; }
            set { _maKhachHang = value; }
        }

        public int? MaHoaDon
        {
            get { return _maHoaDon; }
            set { _maHoaDon = value; }
        }

        public int? DiemCong
        {
            get { return _diemCong; }
            set { _diemCong = value; }
        }

        public int? DiemSuDung
        {
            get { return _diemSuDung; }
            set { _diemSuDung = value; }
        }

        public DateTime? NgayCapNhat
        {
            get { return _ngayCapNhat; }
            set { _ngayCapNhat = value; }
        }

        public string? MoTa
        {
            get { return _moTa; }
            set { _moTa = value; }
        }
    }
}
