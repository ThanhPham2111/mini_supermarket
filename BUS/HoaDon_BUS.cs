using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class HoaDon_BUS
    {
        private readonly HoaDon_DAO _hoaDonDao = new();
        public int GetMaxMaHoaDon()
        {
            int maxID = _hoaDonDao.GetMaxMaHoaDon();
            return maxID + 1;
        }

        public IList<HoaDonDTO> GetHoaDon()
        {
            return _hoaDonDao.GetHoaDon();
        }

        public List<ChiTietHoaDonDTO> GetChiTietHoaDon(string maHoaDon)
        {
            return _hoaDonDao.GetChiTietHoaDon(maHoaDon);
        }

        public int CreateHoaDon(HoaDonDTO hoaDon, List<ChiTietHoaDonDTO> chiTietList)
        {
            if (hoaDon == null)
                throw new ArgumentNullException(nameof(hoaDon));

            if (chiTietList == null || chiTietList.Count == 0)
                throw new ArgumentException("Danh sách chi tiết hóa đơn không được rỗng.", nameof(chiTietList));

            // Insert hóa đơn
            int maHoaDon = _hoaDonDao.InsertHoaDon(hoaDon);

            // Insert chi tiết hóa đơn
            foreach (var chiTiet in chiTietList)
            {
                chiTiet.MaHoaDon = maHoaDon;
                _hoaDonDao.InsertChiTietHoaDon(chiTiet);
            }

            return maHoaDon;
        }

        public int HuyHoaDon(HoaDonDTO hoaDon)
        {
            if (hoaDon == null)
                throw new ArgumentNullException(nameof(hoaDon));

            return _hoaDonDao.HuyHoaDon(hoaDon);
        }
    }
}