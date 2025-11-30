using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_BanHang
{
    public partial class Form_banHang : Form
    {
        private BanHang_BUS? banHangBUS;
        private IList<SanPhamBanHangDTO>? allProductsData; // Lưu danh sách sản phẩm gốc để filter

        public Form_banHang()
        {
            try
            {
                InitializeComponent();
                banHangBUS = new BanHang_BUS();

                // Test database connection trước
                TestConnection();

                // Load sau khi form đã hiển thị
                this.Load += Form_banHang_Load;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form:\n\n" + ex.Message + "\n\n" + ex.StackTrace,
                    "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_banHang_Load(object? sender, EventArgs e)
        {
            LoadSanPham();
            // Khởi tạo tổng tiền
            if (txtTotal != null)
            {
                txtTotal.Text = "0 đ";
            }
            // Khởi tạo điểm tích lũy
            if (txtEarnedPoints != null)
            {
                txtEarnedPoints.Text = "0";
            }
            // Thêm event handler cho tìm kiếm realtime
            if (searchBox != null && searchBox.InnerTextBox != null)
            {
                searchBox.InnerTextBox.TextChanged += SearchBox_TextChanged;
            }
        }

        private void TestConnection()
        {
            try
            {
                if (banHangBUS == null)
                {
                    MessageBox.Show("BanHang_BUS chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (banHangBUS.TestConnection(out int soLoai, out string? errorMessage))
                {
                    Console.WriteLine($"Test connection OK. Số loại: {soLoai}");
                }
                else
                {
                    MessageBox.Show("Lỗi kết nối database:\n\n" + errorMessage,
                        "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối database:\n\n" + ex.Message,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSanPham()
        {
            try
            {
                // Kiểm tra dgvProducts đã được khởi tạo chưa
                if (dgvProducts == null)
                {
                    MessageBox.Show("DataGridView chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (banHangBUS == null)
                {
                    MessageBox.Show("BanHang_BUS chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine("Bắt đầu load sản phẩm...");

                var list = banHangBUS.LayDanhSachSanPhamBanHang();

                // Lưu danh sách sản phẩm gốc để filter
                allProductsData = list;

                // Debug: Kiểm tra số lượng dữ liệu
                if (list == null || list.Count == 0)
                {
                    MessageBox.Show("Không có sản phẩm nào trong kho!\n\nVui lòng kiểm tra:\n" +
                        "1. Đã có dữ liệu trong bảng Tbl_SanPham chưa?\n" +
                        "2. Trạng thái sản phẩm phải là 'Còn hàng'\n" +
                        "3. Sản phẩm phải có số lượng > 0 trong Tbl_KhoHang",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Xóa dữ liệu cũ
                dgvProducts.Rows.Clear();
                Console.WriteLine("Đã clear rows");

                // Load sản phẩm từ DataTable
                LoadProductsFromList(allProductsData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm:\n\n" +
                    "Message: " + ex.Message + "\n\n" +
                    "Source: " + ex.Source + "\n\n" +
                    "Stack trace:\n" + ex.StackTrace,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSanPham();
            ClearProductDetails();
            // Clear search box
            if (searchBox?.InnerTextBox != null)
            {
                searchBox.InnerTextBox.Text = "";
            }
        }

        private void DgvProducts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                ClearProductDetails();
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dgvProducts.SelectedRows[0];
                if (selectedRow.Tag == null)
                {
                    ClearProductDetails();
                    return;
                }

                int maSanPham = Convert.ToInt32(selectedRow.Tag);
                LoadProductDetails(maSanPham);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin sản phẩm:\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearProductDetails();
            }
        }

        // Lưu thông tin sản phẩm hiện tại để thêm vào giỏ
        private decimal? currentProductPrice = null;
        private decimal? currentProductDiscount = null;
        private string? currentProductName = null;
        private string? currentProductPromotion = null;

        private void LoadProductDetails(int maSanPham)
        {
            try
            {
                if (banHangBUS == null)
                {
                    MessageBox.Show("BanHang_BUS chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var list = banHangBUS.LayThongTinSanPhamChiTiet(maSanPham);
                if (list == null || list.Count == 0)
                {
                    ClearProductDetails();
                    return;
                }

                var item = list[0];
                
                // Lưu thông tin sản phẩm được chọn
                selectedProductId = maSanPham;
                selectedProductStock = item.SoLuong ?? 0;

                // Lưu thông tin để thêm vào giỏ
                currentProductName = item.TenSanPham;
                currentProductPrice = item.GiaBan;
                currentProductDiscount = item.PhanTramGiam;
                
                string khuyenMai = item.KhuyenMai ?? "";
                if (!string.IsNullOrEmpty(khuyenMai) && currentProductDiscount > 0)
                {
                    currentProductPromotion = $"{khuyenMai} (-{currentProductDiscount}%)";
                }
                else
                {
                    currentProductPromotion = "";
                }

                // Hiển thị thông tin sản phẩm
                txtProductName.Text = currentProductName;
                txtUnitPrice.Text = currentProductPrice.Value.ToString("N0") + " đ";

                // Số lượng mặc định là 1
                txtQuantity.Text = "1";
                txtPromotion.Text = currentProductPromotion;

                // Load ảnh sản phẩm
                LoadProductImage(item.HinhAnh);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin sản phẩm:\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearProductDetails();
            }
        }

        private void LoadProductImage(string? imagePath)
        {
            try
            {
                if (picProductImage == null)
                    return;

                if (string.IsNullOrWhiteSpace(imagePath))
                {
                    picProductImage.Image = null;
                    return;
                }

                // Nếu đường dẫn không phải absolute path, thêm base directory
                if (!Path.IsPathRooted(imagePath))
                {
                    imagePath = Path.Combine(AppContext.BaseDirectory, imagePath);
                }

                if (!File.Exists(imagePath))
                {
                    picProductImage.Image = null;
                    return;
                }

                using (var image = Image.FromFile(imagePath))
                {
                    picProductImage.Image = new Bitmap(image);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi load ảnh sản phẩm: " + ex.Message);
                if (picProductImage != null)
                    picProductImage.Image = null;
            }
        }

        private void ClearProductDetails()
        {
            selectedProductId = null;
            selectedProductStock = null;
            currentProductPrice = null;
            currentProductDiscount = null;
            currentProductName = null;
            currentProductPromotion = null;
            txtProductName.Text = "";
            txtUnitPrice.Text = "";
            txtQuantity.Text = "";
            txtPromotion.Text = "";
            if (picProductImage != null)
                picProductImage.Image = null;
        }

        private void btnAddProduct_Click(object? sender, EventArgs e)
        {
            try
            {
                if (banHangBUS == null)
                {
                    MessageBox.Show("BanHang_BUS chưa được khởi tạo!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dgvOrder == null || currentProductName == null || !currentProductPrice.HasValue)
                {
                    MessageBox.Show("Lỗi: Không thể thêm sản phẩm vào giỏ hàng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataGridViewRow? existingRow = null;
                int currentQuantity = 0;

                foreach (DataGridViewRow row in dgvOrder.Rows)
                {
                    if (row.Tag is BanHangItemDTO info && selectedProductId.HasValue &&
                        info.MaSanPham == selectedProductId.Value)
                    {
                        existingRow = row;
                        if (row.Cells[2].Value != null && int.TryParse(row.Cells[2].Value.ToString(), out int qty))
                        {
                            currentQuantity = qty;
                        }
                        break;
                    }
                }

                if (!banHangBUS.ValidateAddProduct(selectedProductId, selectedProductStock, txtQuantity.Text,
                        currentQuantity, out int soLuongNhap, out string errorMessage))
                {
                    MessageBox.Show(errorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (txtQuantity != null)
                    {
                        txtQuantity.Focus();
                        txtQuantity.SelectAll();
                    }
                    return;
                }

                if (existingRow != null)
                {
                    int newQuantity = currentQuantity + soLuongNhap;
                    existingRow.Cells[2].Value = newQuantity;
                }
                else
                {
                    string giaBanStr = currentProductPrice.Value.ToString("N0") + " đ";
                    string khuyenMaiStr = currentProductPromotion ?? "";

                    int rowIndex = dgvOrder.Rows.Add(currentProductName, giaBanStr, soLuongNhap, khuyenMaiStr);

                    var productInfo = new BanHangItemDTO
                    {
                        MaSanPham = selectedProductId!.Value,
                        GiaBan = currentProductPrice.Value,
                        PhanTramGiam = currentProductDiscount ?? 0
                    };
                    dgvOrder.Rows[rowIndex].Tag = productInfo;
                }

                UpdateTotal();
                UpdateEarnedPoints();

                txtQuantity.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm vào giỏ:\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotal()
        {
            try
            {
                if (dgvOrder == null || txtTotal == null || banHangBUS == null)
                    return;

                var cartItems = GetCartItems();
                decimal total = banHangBUS.TinhTongTien(cartItems);

                int usePoints = 0;
                bool canApplyPoints = TryGetUsePoints(out usePoints, false, total);
                decimal discount = (canApplyPoints && usePoints > 0)
                    ? banHangBUS.TinhGiamTuDiem(total, usePoints)
                    : 0;

                decimal payable = total - discount;
                txtTotal.Text = payable.ToString("N0") + " đ";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật tổng tiền: {ex.Message}");
                if (txtTotal != null)
                    txtTotal.Text = "0 đ";
            }
        }

        private void btnRemove_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvOrder == null)
                    return;

                if (dgvOrder.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa trong giỏ hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Xóa các row được chọn
                foreach (DataGridViewRow row in dgvOrder.SelectedRows)
                {
                    dgvOrder.Rows.Remove(row);
                }

                // Cập nhật tổng tiền và điểm tích lũy
                UpdateTotal();
                UpdateEarnedPoints();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sản phẩm:\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateEarnedPoints()
        {
            try
            {
                if (dgvOrder == null || txtEarnedPoints == null || banHangBUS == null)
                    return;

                var cartItems = GetCartItems();
                int totalPoints = banHangBUS.TinhDiemTichLuy(cartItems);
                txtEarnedPoints.Text = totalPoints.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật điểm tích lũy: {ex.Message}");
                if (txtEarnedPoints != null)
                    txtEarnedPoints.Text = "0";
            }
        }

        private List<BanHangCartItemDTO> GetCartItems()
        {
            List<BanHangCartItemDTO> cartItems = new List<BanHangCartItemDTO>();

            if (dgvOrder == null)
                return cartItems;

            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (row.Tag is BanHangItemDTO productInfo &&
                    row.Cells[2].Value != null &&
                    int.TryParse(row.Cells[2].Value.ToString(), out int soLuong))
                {
                    cartItems.Add(new BanHangCartItemDTO
                    {
                        MaSanPham = productInfo.MaSanPham,
                        GiaBan = productInfo.GiaBan,
                        PhanTramGiam = productInfo.PhanTramGiam,
                        SoLuong = soLuong
                    });
                }
            }

            return cartItems;
        }

        private void SearchBox_TextChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dgvProducts == null || allProductsData == null)
                    return;

                string searchText = searchBox?.InnerTextBox?.Text?.Trim() ?? "";

                // Xóa dữ liệu cũ
                dgvProducts.Rows.Clear();

                if (string.IsNullOrEmpty(searchText))
                {
                    // Nếu không có từ khóa, hiển thị tất cả
                    LoadProductsFromList(allProductsData);
                }
                else
                {
                    // Filter dữ liệu
                    string searchLower = searchText.ToLower();
                    var filtered = allProductsData.Where(item =>
                    {
                        string tenSanPham = item.TenSanPham.ToLower();
                        string maSanPham = item.MaSanPham.ToString();

                        return tenSanPham.Contains(searchLower) || maSanPham.Contains(searchText);
                    }).ToList();

                    LoadProductsFromList(filtered);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tìm kiếm: {ex.Message}");
            }
        }

        private void LoadProductsFromList(IList<SanPhamBanHangDTO> list)
        {
            try
            {
                if (dgvProducts == null || list == null)
                    return;

                foreach (var item in list)
                {
                    string tenSanPham = item.TenSanPham ?? "";
                    decimal giaBan = item.GiaBan ?? 0m;
                    int soLuong = item.SoLuong ?? 0;
                    string khuyenMai = item.KhuyenMai ?? "";
                    decimal phanTramGiam = item.PhanTramGiam;

                    string giaBanStr = giaBan.ToString("N0") + " đ";
                    string khuyenMaiStr = string.IsNullOrEmpty(khuyenMai) ? "" : $"{khuyenMai} (-{phanTramGiam}%)";

                    dgvProducts.Rows.Add(tenSanPham, giaBanStr, soLuong, khuyenMaiStr);

                    dgvProducts.Rows[dgvProducts.Rows.Count - 1].Tag = item.MaSanPham;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load sản phẩm từ list: {ex.Message}");
            }
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            try
            {
                // Xác nhận hủy đơn
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy đơn hàng này?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Reset giỏ hàng
                    if (dgvOrder != null)
                    {
                        dgvOrder.Rows.Clear();
                    }

                    // Reset tổng tiền
                    if (txtTotal != null)
                    {
                        txtTotal.Text = "0 đ";
                    }

                    // Reset điểm tích lũy
                    if (txtEarnedPoints != null)
                    {
                        txtEarnedPoints.Text = "0";
                    }

                    // Reset các trường thông tin sản phẩm
                    ClearProductDetails();

                    // Reset các trường khách hàng (nếu cần)
                    if (txtCustomer != null)
                        txtCustomer.Text = "";
                    if (txtAvailablePoints != null)
                        txtAvailablePoints.Text = "";
                    if (txtEarnedPoints != null)
                        txtEarnedPoints.Text = "";
                    if (txtUsePoints != null)
                        txtUsePoints.Text = "";
                    
                    // Reset mã khách hàng đã chọn và điểm
                    selectedKhachHangId = null;
                    availablePoints = null;
                    
                    // Clear error
                    if (errorProviderUsePoints != null && txtUsePoints != null)
                    {
                        errorProviderUsePoints.SetError(txtUsePoints!, "");
                    }

                    // Bỏ chọn sản phẩm trong danh sách
                    if (dgvProducts != null)
                    {
                        dgvProducts.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy đơn:\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectCustomer_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var dialog = new Dialog_ChonKhachHang())
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK && dialog.SelectedKhachHang != null)
                    {
                        var kh = dialog.SelectedKhachHang;
                        selectedKhachHangId = kh.MaKhachHang;

                        // Hiển thị thông tin khách hàng
                        if (txtCustomer != null)
                        {
                            txtCustomer.Text = kh.TenKhachHang ?? "";
                        }

                        if (txtAvailablePoints != null)
                        {
                            availablePoints = kh.DiemTichLuy ?? 0;
                            txtAvailablePoints.Text = availablePoints.Value.ToString();
                        }
                        
                        // Reset ô dùng điểm khi chọn khách hàng mới
                        if (txtUsePoints != null)
                        {
                            txtUsePoints.Text = "";
                        }
                        
                        // Clear error khi chọn khách hàng mới
                        if (errorProviderUsePoints != null)
                        {
                            errorProviderUsePoints.SetError(txtUsePoints!, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn khách hàng:\n\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckout_Click(object? sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có sản phẩm trong giỏ hàng không
                if (dgvOrder == null || dgvOrder.Rows.Count == 0)
                {
                    MessageBox.Show("Giỏ hàng đang trống. Vui lòng thêm sản phẩm vào giỏ hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cartItems = GetCartItems();
                decimal orderTotal = banHangBUS != null ? banHangBUS.TinhTongTien(cartItems) : 0;

                // Kiểm tra điểm sử dụng có hợp lệ không (chỉ khi có khách hàng)
                if (selectedKhachHangId.HasValue)
                {
                    if (!TryGetUsePoints(out _, true, orderTotal))
                    {
                        if (txtUsePoints != null)
                        {
                            txtUsePoints.Focus();
                            txtUsePoints.SelectAll();
                        }
                        return;
                    }
                }
                else
                {
                    // Nếu không có khách hàng, đảm bảo không dùng điểm
                    if (txtUsePoints != null && !string.IsNullOrWhiteSpace(txtUsePoints.Text))
                    {
                        MessageBox.Show("Khách lẻ không thể sử dụng điểm tích lũy. Vui lòng xóa điểm sử dụng!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (txtUsePoints != null)
                        {
                            txtUsePoints.Focus();
                            txtUsePoints.SelectAll();
                        }
                        return;
                    }
                }

                // Xác nhận thanh toán
                // DialogResult confirmResult = MessageBox.Show(
                //     "Bạn có chắc chắn muốn thanh toán đơn hàng này?",
                //     "Xác nhận thanh toán",
                //     MessageBoxButtons.YesNo,
                //     MessageBoxIcon.Question);

                // if (confirmResult != DialogResult.Yes)
                // {
                //     return;
                // }

                // Thực hiện thanh toán
                ProcessPayment();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán:\n\n{ex.Message}\n\n{ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessPayment()
        {
            try
            {
                if (banHangBUS == null)
                {
                    throw new InvalidOperationException("BanHang_BUS chưa được khởi tạo.");
                }

                var cartItems = GetCartItems();
                if (cartItems.Count == 0)
                {
                    throw new InvalidOperationException("Giỏ hàng đang trống.");
                }

                decimal orderTotal = banHangBUS.TinhTongTien(cartItems);
                int diemSuDung = 0;
                int diemHienCo = 0;

                // Chỉ xử lý điểm khi có khách hàng
                if (selectedKhachHangId.HasValue)
                {
                    if (!TryGetUsePoints(out diemSuDung, false, orderTotal))
                    {
                        throw new InvalidOperationException("Số điểm sử dụng không hợp lệ.");
                    }
                    diemHienCo = availablePoints ?? 0;
                }

                int maNhanVien = GetCurrentEmployeeId();

                BanHangPaymentResultDTO result = banHangBUS.ProcessPayment(
                    selectedKhachHangId,
                    maNhanVien,
                    cartItems,
                    diemHienCo,
                    diemSuDung);

                // Tạo thông báo thành công
                string message = $"Thanh toán thành công!\n\n" +
                    $"Mã hóa đơn: HD{result.MaHoaDon:D3}\n" +
                    $"Tổng tiền trước điểm: {result.TongTienTruocDiem:N0} đ\n";
                
                if (result.GiamTuDiem > 0)
                {
                    message += $"Giảm giá bằng điểm: -{result.GiamTuDiem:N0} đ\n";
                }
                
                message += $"Tổng cần thanh toán: {result.TongTienThanhToan:N0} đ\n";
                
                if (selectedKhachHangId.HasValue)
                {
                    message += $"Điểm tích lũy: +{result.DiemTichLuy}\n" +
                        $"Điểm sử dụng: {(result.DiemSuDung > 0 ? result.DiemSuDung.ToString() : "0")}\n" +
                        $"Điểm mới của khách hàng: {result.DiemMoi}";
                }
                else
                {
                    message += "Khách hàng: Khách lẻ";
                }

                MessageBox.Show(
                    message,
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ResetFormAfterPayment();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xử lý thanh toán: {ex.Message}", ex);
            }
        }

        private bool TryGetUsePoints(out int usePoints, bool showMessageOnError = false, decimal? currentOrderTotal = null)
        {
            usePoints = 0;

            if (banHangBUS == null)
                return true;

            decimal? maxAmount = currentOrderTotal;
            if (!maxAmount.HasValue)
            {
                var snapshot = GetCartItems();
                if (snapshot.Count > 0)
                {
                    maxAmount = banHangBUS.TinhTongTien(snapshot);
                }
                else
                {
                    maxAmount = 0;
                }
            }

            bool isValid = banHangBUS.ValidateUsePoints(
                availablePoints,
                txtUsePoints?.Text,
                out usePoints,
                out string errorMessage,
                treatEmptyAsZero: true,
                maxAllowedAmount: maxAmount);

            if (!isValid)
            {
                usePoints = 0;
                if (showMessageOnError && !string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return isValid;
        }

        private int GetCurrentEmployeeId()
        {
            // TODO: Lấy từ session/login, tạm thời dùng 1
            // Có thể tạo một class SessionManager để lưu thông tin nhân viên đăng nhập
            return 1;
        }

        private void ResetFormAfterPayment()
        {
            // Xóa giỏ hàng
            if (dgvOrder != null)
            {
                dgvOrder.Rows.Clear();
            }

            // Reset tổng tiền
            if (txtTotal != null)
            {
                txtTotal.Text = "0 đ";
            }

            // Reset điểm tích lũy
            if (txtEarnedPoints != null)
            {
                txtEarnedPoints.Text = "0";
            }

            // Reset các trường thông tin sản phẩm
            ClearProductDetails();

            // Reset các trường khách hàng
            if (txtCustomer != null)
                txtCustomer.Text = "";
            if (txtAvailablePoints != null)
                txtAvailablePoints.Text = "";
            if (txtUsePoints != null)
                txtUsePoints.Text = "";

            // Reset mã khách hàng đã chọn và điểm
            selectedKhachHangId = null;
            availablePoints = null;

            // Clear error
            if (errorProviderUsePoints != null && txtUsePoints != null)
            {
                errorProviderUsePoints.SetError(txtUsePoints!, "");
            }

            // Bỏ chọn sản phẩm trong danh sách
            if (dgvProducts != null)
            {
                dgvProducts.ClearSelection();
            }
        }

        private void txtUsePoints_TextChanged(object? sender, EventArgs e)
        {
            ValidateUsePointsRealtime();
            UpdateTotal();
        }

        private void ValidateUsePointsRealtime()
        {
            if (txtUsePoints == null || errorProviderUsePoints == null || banHangBUS == null)
                return;

            if (!availablePoints.HasValue)
            {
                errorProviderUsePoints.SetError(txtUsePoints!, "");
                return;
            }

            var cartItems = GetCartItems();
            decimal orderTotal = banHangBUS.TinhTongTien(cartItems);
            bool isValid = banHangBUS.ValidateUsePoints(
                availablePoints,
                txtUsePoints.Text,
                out _,
                out _,
                treatEmptyAsZero: true,
                maxAllowedAmount: orderTotal);

            errorProviderUsePoints.SetError(txtUsePoints!, isValid ? "" : "!");
        }
    }
}