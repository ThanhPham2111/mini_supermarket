using System;

namespace mini_supermarket.DTO
{
    public class CauHinhLoiNhuanDTO
    {
        public int MaCauHinh { get; set; }
        public decimal PhanTramLoiNhuanMacDinh { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public int? MaNhanVien { get; set; }
    }
}

