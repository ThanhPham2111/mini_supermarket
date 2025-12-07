using System;

namespace mini_supermarket.DTO
{
    /// <summary>
    /// Thông tin sản phẩm được sử dụng trong luồng bán hàng (giá bán, khuyến mãi...).
    /// </summary>
    public class BanHangItemDTO
    {
        public int MaSanPham { get; set; }
        public decimal GiaBan { get; set; }
        public decimal PhanTramGiam { get; set; }
    }
}

