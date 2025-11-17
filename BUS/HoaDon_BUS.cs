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
    }
}