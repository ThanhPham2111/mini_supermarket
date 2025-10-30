using mini_supermarket.DAO;
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
    }
}

