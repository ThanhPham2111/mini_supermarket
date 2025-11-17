using mini_supermarket.DAO;
using mini_supermarket.DTO;
using System.Data;

namespace mini_supermarket.BUS
{
    public class KhoHangBUS
    {
        private KhoHangDAO khoHangDAO = new KhoHangDAO();

        public DataTable LayDanhSachTonKho()
        {
            return khoHangDAO.LayDanhSachTonKho();
        }

        public DataTable LayDanhSachLoai()
        {
            return khoHangDAO.LayDanhSachLoai();
        }

        public DataTable LayDanhSachThuongHieu()
        {
            return khoHangDAO.LayDanhSachThuongHieu();
        }

        public DataTable LayDanhSachSanPhamBanHang()
        {
            return khoHangDAO.LayDanhSachSanPhamBanHang();
        }

        public KhoHangDTO? GetByMaSanPham(int maSanPham)
        {
            return khoHangDAO.GetByMaSanPham(maSanPham);
        }

        // PHƯƠNG ÁN 2: Cập nhật kho có ghi log
        public bool CapNhatSoLuongKho(KhoHangDTO khoHang, LichSuThayDoiKhoDTO lichSu)
        {
            return khoHangDAO.CapNhatKhoVaGhiLog(khoHang, lichSu);
        }

        // Lấy lịch sử thay đổi
        public DataTable LayLichSuThayDoi(int maSanPham)
        {
            return khoHangDAO.LayLichSuThayDoi(maSanPham);
        }
    }
}

