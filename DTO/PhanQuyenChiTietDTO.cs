using System;

namespace mini_supermarket.DTO
{
    public class PhanQuyenChiTietDTO
    {
        private int _maPhanQuyenChiTiet;
        private int _maQuyen;
        private int _maChucNang;
        private int _maLoaiQuyen;
        private bool _duocPhep;

        public PhanQuyenChiTietDTO()
        {
        }

        public PhanQuyenChiTietDTO(
            int maPhanQuyenChiTiet,
            int maQuyen,
            int maChucNang,
            int maLoaiQuyen,
            bool duocPhep)
        {
            _maPhanQuyenChiTiet = maPhanQuyenChiTiet;
            _maQuyen = maQuyen;
            _maChucNang = maChucNang;
            _maLoaiQuyen = maLoaiQuyen;
            _duocPhep = duocPhep;
        }

        public int MaPhanQuyenChiTiet
        {
            get { return _maPhanQuyenChiTiet; }
            set { _maPhanQuyenChiTiet = value; }
        }

        public int MaQuyen
        {
            get { return _maQuyen; }
            set { _maQuyen = value; }
        }

        public int MaChucNang
        {
            get { return _maChucNang; }
            set { _maChucNang = value; }
        }

        public int MaLoaiQuyen
        {
            get { return _maLoaiQuyen; }
            set { _maLoaiQuyen = value; }
        }

        public bool DuocPhep
        {
            get { return _duocPhep; }
            set { _duocPhep = value; }
        }
    }
}
