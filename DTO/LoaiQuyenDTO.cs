using System;

namespace mini_supermarket.DTO
{
    public class LoaiQuyenDTO
    {
        public int MaLoaiQuyen { get; set; }
        public string TenQuyen { get; set; } = string.Empty;
        public string? MoTa { get; set; }

        public LoaiQuyenDTO() { }

        public LoaiQuyenDTO(int maLoaiQuyen, string tenQuyen, string? moTa)
        {
            MaLoaiQuyen = maLoaiQuyen;
            TenQuyen = tenQuyen;
            MoTa = moTa;
        }
    }
}
