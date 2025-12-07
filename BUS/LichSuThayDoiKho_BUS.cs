using System;
using System.Collections.Generic;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class LichSuThayDoiKho_BUS
    {
        private readonly LichSuThayDoiKho_DAO _dao = new();

        public IList<LichSuThayDoiKhoDTO> LayTheoSanPham(int maSanPham, int top = 50)
        {
            if (maSanPham <= 0) throw new ArgumentException("Ma san pham khong hop le.");
            if (top <= 0) top = 50;
            return _dao.GetByProduct(maSanPham, top);
        }
    }
}
