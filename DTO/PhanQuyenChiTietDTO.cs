using System;

namespace mini_supermarket.DTO
{
    public class PhanQuyenChiTietDTO
    {
        public int MaPhanQuyenChiTiet { get; set; }
        public int MaQuyen { get; set; }
        public int MaChucNang { get; set; }
        public int MaLoaiQuyen { get; set; }
        public bool DuocPhep { get; set; }

        public PhanQuyenChiTietDTO() { }

        public PhanQuyenChiTietDTO(int maPhanQuyenChiTiet, int maQuyen, int maChucNang, int maLoaiQuyen, bool duocPhep)
        {
            MaPhanQuyenChiTiet = maPhanQuyenChiTiet;
            MaQuyen = maQuyen;
            MaChucNang = maChucNang;
            MaLoaiQuyen = maLoaiQuyen;
            DuocPhep = duocPhep;
        }
    }
}
