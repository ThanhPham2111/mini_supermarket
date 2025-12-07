namespace mini_supermarket.DTO
{
    /// <summary>
    /// Kết quả trả về sau khi xử lý thanh toán bán hàng.
    /// </summary>
    public class BanHangPaymentResultDTO
    {
        public int MaHoaDon { get; set; }
        public decimal TongTienTruocDiem { get; set; }
        public decimal GiamTuDiem { get; set; }
        public decimal TongTienThanhToan { get; set; }
        public int DiemTichLuy { get; set; }
        public int DiemSuDung { get; set; }
        public int DiemMoi { get; set; }
    }
}

