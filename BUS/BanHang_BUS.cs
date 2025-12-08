using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    /// <summary>
    /// Đóng gói toàn bộ nghiệp vụ cho luồng bán hàng.
    /// GUI chỉ cần gọi các hàm từ lớp này để thực hiện logic.
    /// </summary>
    public class BanHang_BUS
    {
        // Giữ lại hằng số này để tương thích ngược, nhưng sẽ dùng GetGiaTriMotDiem() thay thế
        [Obsolete("Sử dụng GetGiaTriMotDiem() thay vì GiaTriMotDiem để lấy giá trị động từ cấu hình")]
        public const decimal GiaTriMotDiem = 100m;

        private readonly KhoHangBUS khoHangBUS;
        private readonly HoaDon_BUS hoaDonBUS;
        private readonly KhachHang_BUS khachHangBUS;
        private readonly QuyDoiDiem_BUS quyDoiDiemBUS;

        public BanHang_BUS()
        {
            khoHangBUS = new KhoHangBUS();
            hoaDonBUS = new HoaDon_BUS();
            khachHangBUS = new KhachHang_BUS();
            quyDoiDiemBUS = new QuyDoiDiem_BUS();
        }

        // Lấy giá trị 1 điểm từ cấu hình động
        private decimal GetGiaTriMotDiem()
        {
            return quyDoiDiemBUS.GetGiaTriMotDiem();
        }

        public bool TestConnection(out int soLoaiSanPham, out string? errorMessage)
        {
            soLoaiSanPham = 0;
            errorMessage = null;
            try
            {
                var list = khoHangBUS.LayDanhSachLoai();
                soLoaiSanPham = list.Count;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public IList<SanPhamDTO> LayDanhSachSanPhamBanHang()
        {
            return khoHangBUS.LayDanhSachSanPhamBanHang().Cast<SanPhamDTO>().ToList();
        }

        public IList<SanPhamDTO> LayThongTinSanPhamChiTiet(int maSanPham)
        {
            return khoHangBUS.LayThongTinSanPhamChiTiet(maSanPham).Cast<SanPhamDTO>().ToList();
        }

        public bool ValidateAddProduct(int? productId, int? stock, string? quantityText, int currentCartQuantity,
            out int requestedQuantity, out string errorMessage)
        {
            requestedQuantity = 0;
            errorMessage = string.Empty;

            if (!productId.HasValue)
            {
                errorMessage = "Vui lòng chọn sản phẩm từ danh sách!";
                return false;
            }

            if (!stock.HasValue || stock.Value <= 0)
            {
                errorMessage = "Sản phẩm này đã hết hàng!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(quantityText))
            {
                errorMessage = "Vui lòng nhập số lượng!";
                return false;
            }

            if (!int.TryParse(quantityText.Trim(), out requestedQuantity) || requestedQuantity <= 0)
            {
                errorMessage = "Số lượng phải là số nguyên dương!";
                requestedQuantity = 0;
                return false;
            }

            int newQuantity = currentCartQuantity + requestedQuantity;
            if (newQuantity > stock.Value)
            {
                int remaining = Math.Max(stock.Value - currentCartQuantity, 0);
                errorMessage =
                    $"Số lượng sau khi cộng ({newQuantity}) vượt quá số lượng tồn kho ({stock.Value}).\n" +
                    $"Số lượng hiện tại trong giỏ: {currentCartQuantity}\n" +
                    $"Số lượng còn có thể thêm: {remaining}";
                requestedQuantity = 0;
                return false;
            }

            return true;
        }

        public bool ValidateUsePoints(
            int? availablePoints,
            string? usePointsText,
            out int usePoints,
            out string errorMessage,
            bool treatEmptyAsZero = true,
            decimal? maxAllowedAmount = null)
        {
            usePoints = 0;
            errorMessage = string.Empty;

            if (!availablePoints.HasValue)
            {
                if (string.IsNullOrWhiteSpace(usePointsText))
                    return true;

                errorMessage = "Vui lòng chọn khách hàng trước khi dùng điểm.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(usePointsText))
            {
                if (treatEmptyAsZero)
                    return true;

                errorMessage = "Vui lòng nhập số điểm muốn sử dụng.";
                return false;
            }

            if (!int.TryParse(usePointsText.Trim(), out usePoints))
            {
                errorMessage = "Điểm sử dụng phải là số nguyên.";
                return false;
            }

            if (usePoints < 0)
            {
                errorMessage = "Điểm sử dụng không được âm.";
                return false;
            }

            if (usePoints > availablePoints.Value)
            {
                errorMessage = "Điểm sử dụng vượt quá điểm hiện có.";
                return false;
            }

            if (maxAllowedAmount.HasValue)
            {
                decimal giaTriMotDiem = GetGiaTriMotDiem();
                decimal requiredAmount = usePoints * giaTriMotDiem;
                if (requiredAmount > maxAllowedAmount.Value)
                {
                    int maxPoints = (int)Math.Floor(maxAllowedAmount.Value / giaTriMotDiem);
                    errorMessage = maxPoints > 0
                        ? $"Tối đa chỉ được dùng {maxPoints} điểm cho đơn hàng hiện tại."
                        : "Đơn hàng hiện tại không đủ để sử dụng điểm.";
                    return false;
                }
            }

            return true;
        }

        public decimal TinhTongTien(IEnumerable<BanHangCartItemDTO> cartItems)
        {
            return cartItems.Sum(TinhThanhTien);
        }

        public decimal TinhThanhTien(BanHangCartItemDTO cartItem)
        {
            decimal giaBan = cartItem.GiaBan;
            if (cartItem.PhanTramGiam > 0)
            {
                giaBan -= giaBan * cartItem.PhanTramGiam / 100;
            }
            return giaBan * cartItem.SoLuong;
        }

        public int TinhDiemTichLuy(IEnumerable<BanHangCartItemDTO> cartItems)
        {
            // Tính điểm tích lũy theo số lượng sản phẩm: 1 sản phẩm = 1 điểm
            // Ví dụ: 10 sản phẩm = 10 điểm
            return cartItems.Sum(item => item.SoLuong);
        }

        public decimal TinhGiamTuDiem(decimal tongTien, int diemSuDung)
        {
            if (diemSuDung <= 0)
                return 0;

            decimal giaTriMotDiem = GetGiaTriMotDiem();
            decimal giam = diemSuDung * giaTriMotDiem;
            if (giam > tongTien)
                giam = tongTien;

            return giam;
        }

        public decimal TinhTongTienSauDiem(IEnumerable<BanHangCartItemDTO> cartItems, int diemSuDung)
        {
            decimal tongTien = TinhTongTien(cartItems);
            decimal giam = TinhGiamTuDiem(tongTien, diemSuDung);
            return tongTien - giam;
        }

        public BanHangPaymentResultDTO ProcessPayment(
            int? maKhachHang,
            int maNhanVien,
            IEnumerable<BanHangCartItemDTO> cartItems,
            int diemHienCo,
            int diemSuDung)
        {
            var items = cartItems?.ToList() ?? new List<BanHangCartItemDTO>();
            if (items.Count == 0)
            {
                throw new InvalidOperationException("Giỏ hàng đang trống.");
            }

            // Nếu không có khách hàng, không cho dùng điểm
            if (!maKhachHang.HasValue && diemSuDung > 0)
            {
                throw new InvalidOperationException("Khách lẻ không thể sử dụng điểm tích lũy.");
            }

            int diemTichLuy = maKhachHang.HasValue ? TinhDiemTichLuy(items) : 0;
            decimal tongTienTruocDiem = TinhTongTien(items);
            decimal giamTuDiem = TinhGiamTuDiem(tongTienTruocDiem, diemSuDung);
            decimal tongTienThanhToan = tongTienTruocDiem - giamTuDiem;

            if (maKhachHang.HasValue && diemSuDung > diemHienCo)
            {
                throw new InvalidOperationException("Điểm sử dụng vượt quá điểm hiện có.");
            }

            decimal giaTriMotDiem = GetGiaTriMotDiem();
            int diemSuDungThucTe = giaTriMotDiem > 0 ? (int)(giamTuDiem / giaTriMotDiem) : 0;
            int diemMoi = maKhachHang.HasValue ? (diemHienCo - diemSuDungThucTe) + diemTichLuy : 0;

            HoaDonDTO hoaDon = new HoaDonDTO
            {
                MaNhanVien = maNhanVien,
                MaKhachHang = maKhachHang,
                TongTien = tongTienThanhToan,
                NgayLap = DateTime.Now
            };

            List<ChiTietHoaDonDTO> chiTietList = items
                .Select(item => new ChiTietHoaDonDTO
                {
                    MaSanPham = item.MaSanPham,
                    SoLuong = item.SoLuong,
                    GiaBan = CalculateFinalPrice(item.GiaBan, item.PhanTramGiam)
                })
                .ToList();

            int maHoaDon = hoaDonBUS.CreateHoaDon(hoaDon, chiTietList);

            foreach (var item in items)
            {
                khoHangBUS.GiamSoLuongKho(item.MaSanPham, item.SoLuong, maNhanVien);
            }

            // Chỉ cập nhật điểm khi có khách hàng
            if (maKhachHang.HasValue)
            {
                khachHangBUS.UpdateDiemTichLuy(maKhachHang.Value, diemMoi);
            }

            return new BanHangPaymentResultDTO
            {
                MaHoaDon = maHoaDon,
                TongTienTruocDiem = tongTienTruocDiem,
                GiamTuDiem = giamTuDiem,
                TongTienThanhToan = tongTienThanhToan,
                DiemTichLuy = diemTichLuy,
                DiemSuDung = diemSuDungThucTe,
                DiemMoi = diemMoi
            };
        }

        private static decimal CalculateFinalPrice(decimal giaBan, decimal phanTramGiam)
        {
            if (phanTramGiam <= 0)
                return giaBan;

            return giaBan - (giaBan * phanTramGiam / 100);
        }
    }
}

