using System;

namespace mini_supermarket.DTO
{
    public class CauHinhQuyDoiDiemDTO
    {
        public int MaCauHinh { get; set; }
        public int SoDiem { get; set; }                    // Số điểm (ví dụ: 100)
        public decimal SoTienTuongUng { get; set; }       // Số tiền tương ứng (ví dụ: 100 đồng)
        public DateTime? NgayCapNhat { get; set; }
        public int? MaNhanVien { get; set; }
    }
}

