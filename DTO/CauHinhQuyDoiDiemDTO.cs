using System;

namespace mini_supermarket.DTO
{
    public class CauHinhQuyDoiDiemDTO
    {
        private int _maCauHinh;
        private int _soDiem;
        private decimal _soTienTuongUng;
        private DateTime? _ngayCapNhat;
        private int? _maNhanVien;

        public CauHinhQuyDoiDiemDTO()
        {
        }

        public CauHinhQuyDoiDiemDTO(
            int maCauHinh,
            int soDiem,
            decimal soTienTuongUng,
            DateTime? ngayCapNhat,
            int? maNhanVien)
        {
            _maCauHinh = maCauHinh;
            _soDiem = soDiem;
            _soTienTuongUng = soTienTuongUng;
            _ngayCapNhat = ngayCapNhat;
            _maNhanVien = maNhanVien;
        }

        public int MaCauHinh
        {
            get { return _maCauHinh; }
            set { _maCauHinh = value; }
        }

        public int SoDiem
        {
            get { return _soDiem; }
            set { _soDiem = value; }
        }

        public decimal SoTienTuongUng
        {
            get { return _soTienTuongUng; }
            set { _soTienTuongUng = value; }
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
    }
}
