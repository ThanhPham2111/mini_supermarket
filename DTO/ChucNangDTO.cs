using System;

namespace mini_supermarket.DTO
{
    public class ChucNangDTO
    {
        public int MaChucNang { get; set; }
        public string TenChucNang { get; set; } = string.Empty;
        public int? MaCha { get; set; }
        public string? DuongDan { get; set; }
        public string? MoTa { get; set; }

        public ChucNangDTO() { }

        public ChucNangDTO(int maChucNang, string tenChucNang, int? maCha, string? duongDan, string? moTa)
        {
            MaChucNang = maChucNang;
            TenChucNang = tenChucNang;
            MaCha = maCha;
            DuongDan = duongDan;
            MoTa = moTa;
        }
    }
}
