using System;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class QuyDoiDiem_BUS
    {
        private readonly CauHinhQuyDoiDiem_DAO _cauHinhDao = new CauHinhQuyDoiDiem_DAO();

        // Lấy cấu hình quy đổi điểm mới nhất
        public CauHinhQuyDoiDiemDTO? GetCauHinh()
        {
            return _cauHinhDao.GetCauHinh();
        }

        // Cập nhật cấu hình quy đổi điểm
        public void UpdateCauHinh(int soDiem, decimal soTienTuongUng, int maNhanVien)
        {
            if (soDiem <= 0)
                throw new ArgumentException("Số điểm phải lớn hơn 0.");
            
            if (soTienTuongUng <= 0)
                throw new ArgumentException("Số tiền tương ứng phải lớn hơn 0.");

            _cauHinhDao.UpdateCauHinh(soDiem, soTienTuongUng, maNhanVien);
        }

        // Tính giá trị của 1 điểm (tiền tương ứng / số điểm)
        public decimal GetGiaTriMotDiem()
        {
            var cauHinh = _cauHinhDao.GetCauHinh();
            if (cauHinh == null)
            {
                // Nếu chưa có cấu hình, trả về giá trị mặc định tương thích với logic cũ
                // Logic cũ: 1 điểm = 100 đồng (GiaTriMotDiem = 100m)
                return 100m;
            }

            if (cauHinh.SoDiem <= 0)
                return 100m;

            // Tính giá trị 1 điểm = số tiền tương ứng / số điểm
            return cauHinh.SoTienTuongUng / cauHinh.SoDiem;
        }

        // Tính số điểm tích lũy từ tổng tiền
        // Ví dụ: nếu cấu hình là 100 điểm = 100 đồng, thì 1000 đồng = 1000 điểm
        public int TinhDiemTichLuyTuTien(decimal tongTien)
        {
            if (tongTien <= 0)
                return 0;

            var cauHinh = _cauHinhDao.GetCauHinh();
            if (cauHinh == null)
            {
                // Nếu chưa có cấu hình, dùng logic cũ: 1 điểm = 100 đồng
                // Logic cũ: TinhDiemTichLuy trả về tổng số lượng sản phẩm
                // Nhưng để tương thích, ta tính: tổng tiền / 100 (vì 1 điểm = 100 đồng)
                // Tuy nhiên, logic cũ thực tế là tích điểm theo số lượng, không theo tiền
                // Để đơn giản, ta dùng: tổng tiền / 100
                return (int)Math.Floor(tongTien / 100m);
            }

            if (cauHinh.SoDiem <= 0 || cauHinh.SoTienTuongUng <= 0)
                return 0;

            // Tính số điểm: (Tổng tiền / Số tiền tương ứng) * Số điểm
            // Ví dụ: 100 điểm = 100 đồng, thì 1000 đồng = (1000/100) * 100 = 1000 điểm
            decimal diem = (tongTien / cauHinh.SoTienTuongUng) * cauHinh.SoDiem;
            return (int)Math.Floor(diem);
        }
    }
}

