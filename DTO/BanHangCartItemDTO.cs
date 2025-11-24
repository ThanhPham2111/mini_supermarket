namespace mini_supermarket.DTO
{
    /// <summary>
    /// Item trong giỏ hàng, bao gồm số lượng để xử lý nghiệp vụ bên BUS.
    /// </summary>
    public class BanHangCartItemDTO : BanHangItemDTO
    {
        public int SoLuong { get; set; }
    }
}

