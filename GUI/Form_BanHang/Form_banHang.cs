using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_BanHang
{
    // Class để lưu thông tin sản phẩm trong giỏ hàng
    public class ProductInfo
    {
        public int MaSanPham { get; set; }
        public decimal GiaBan { get; set; }
        public decimal PhanTramGiam { get; set; }
    }

    public partial class Form_banHang : Form
    {
        private KhoHangBUS? khoHangBUS;
        private DataTable? allProductsData; // Lưu danh sách sản phẩm gốc để filter

        public Form_banHang()
        {
            try
            {
                InitializeComponent();
                khoHangBUS = new KhoHangBUS();

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
                if (khoHangBUS == null)
                {
                    MessageBox.Show("KhoHangBUS chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var testDt = khoHangBUS.LayDanhSachLoai();
                Console.WriteLine($"Test connection OK. Số loại: {testDt.Rows.Count}");
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

                if (khoHangBUS == null)
                {
                    MessageBox.Show("KhoHangBUS chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine("Bắt đầu load sản phẩm...");

                DataTable dt = khoHangBUS.LayDanhSachSanPhamBanHang();
                Console.WriteLine($"Đã query xong. Số dòng: {dt?.Rows.Count ?? 0}");

                // Lưu danh sách sản phẩm gốc để filter
                allProductsData = dt?.Copy();

                // Debug: Kiểm tra số lượng dữ liệu
                if (dt == null || dt.Rows.Count == 0)
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
                LoadProductsFromDataTable(dt);
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
                if (khoHangBUS == null)
                {
                    MessageBox.Show("KhoHangBUS chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable dt = khoHangBUS.LayThongTinSanPhamChiTiet(maSanPham);
                if (dt == null || dt.Rows.Count == 0)
                {
                    ClearProductDetails();
                    return;
                }

                DataRow row = dt.Rows[0];
                
                // Lưu thông tin sản phẩm được chọn
                selectedProductId = maSanPham;
                selectedProductStock = row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : 0;

                // Lưu thông tin để thêm vào giỏ
                currentProductName = row["TenSanPham"]?.ToString() ?? "";
                currentProductPrice = row["GiaBan"] != DBNull.Value ? Convert.ToDecimal(row["GiaBan"]) : 0;
                currentProductDiscount = row["PhanTramGiam"] != DBNull.Value ? Convert.ToDecimal(row["PhanTramGiam"]) : 0;
                
                string khuyenMai = row["KhuyenMai"]?.ToString() ?? "";
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
                string? hinhAnh = row["HinhAnh"]?.ToString();
                LoadProductImage(hinhAnh);
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
                // Kiểm tra đã chọn sản phẩm chưa
                if (!selectedProductId.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm từ danh sách!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Kiểm tra số lượng tồn kho
                if (!selectedProductStock.HasValue || selectedProductStock.Value <= 0)
                {
                    MessageBox.Show("Sản phẩm này đã hết hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate số lượng nhập
                if (string.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int soLuongNhap) || soLuongNhap <= 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                    txtQuantity.SelectAll();
                    return;
                }

                // Kiểm tra số lượng nhập <= số lượng tồn kho
                if (soLuongNhap > selectedProductStock.Value)
                {
                    MessageBox.Show($"Số lượng nhập ({soLuongNhap}) vượt quá số lượng tồn kho ({selectedProductStock.Value})!\n\nVui lòng nhập số lượng nhỏ hơn hoặc bằng {selectedProductStock.Value}.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                    txtQuantity.SelectAll();
                    return;
                }

                // Thêm sản phẩm vào giỏ hàng (dgvOrder)
                if (dgvOrder == null || currentProductName == null || !currentProductPrice.HasValue)
                {
                    MessageBox.Show("Lỗi: Không thể thêm sản phẩm vào giỏ hàng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra sản phẩm đã có trong giỏ chưa
                bool productExists = false;
                DataGridViewRow? existingRow = null;
                
                foreach (DataGridViewRow row in dgvOrder.Rows)
                {
                    if (row.Tag is ProductInfo info && info.MaSanPham == selectedProductId.Value)
                    {
                        productExists = true;
                        existingRow = row;
                        break;
                    }
                }

                if (productExists && existingRow != null)
                {
                    // Sản phẩm đã có trong giỏ, cộng số lượng
                    int currentQuantity = 0;
                    if (existingRow.Cells[2].Value != null && int.TryParse(existingRow.Cells[2].Value.ToString(), out int qty))
                    {
                        currentQuantity = qty;
                    }

                    int newQuantity = currentQuantity + soLuongNhap;
                    
                    // Kiểm tra số lượng mới không vượt quá tồn kho
                    if (newQuantity > selectedProductStock.Value)
                    {
                        MessageBox.Show($"Số lượng sau khi cộng ({newQuantity}) vượt quá số lượng tồn kho ({selectedProductStock.Value})!\n\nSố lượng hiện tại trong giỏ: {currentQuantity}\nSố lượng muốn thêm: {soLuongNhap}\nSố lượng tồn kho còn lại: {selectedProductStock.Value - currentQuantity}",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtQuantity.Focus();
                        txtQuantity.SelectAll();
                        return;
                    }

                    // Cập nhật số lượng
                    existingRow.Cells[2].Value = newQuantity;
                }
                else
                {
                    // Sản phẩm chưa có trong giỏ, thêm mới
                    // Format hiển thị
                    string giaBanStr = currentProductPrice.Value.ToString("N0") + " đ";
                    string khuyenMaiStr = currentProductPromotion ?? "";

                    // Thêm row vào dgvOrder
                    int rowIndex = dgvOrder.Rows.Add(currentProductName, giaBanStr, soLuongNhap, khuyenMaiStr);
                    
                    // Lưu thông tin vào Tag để tính tổng tiền
                    var productInfo = new ProductInfo
                    {
                        MaSanPham = selectedProductId.Value,
                        GiaBan = currentProductPrice.Value,
                        PhanTramGiam = currentProductDiscount ?? 0
                    };
                    dgvOrder.Rows[rowIndex].Tag = productInfo;
                }

                // Cập nhật tổng tiền và điểm tích lũy
                UpdateTotal();
                UpdateEarnedPoints();

                // Reset số lượng về 1 sau khi thêm
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
                if (dgvOrder == null || txtTotal == null)
                    return;

                decimal total = 0;

                foreach (DataGridViewRow row in dgvOrder.Rows)
                {
                    if (row.Tag == null)
                        continue;

                    try
                    {
                        // Lấy thông tin từ Tag
                        if (row.Tag is ProductInfo productInfo)
                        {
                            // Lấy số lượng từ cột
                            if (row.Cells[2].Value != null && int.TryParse(row.Cells[2].Value.ToString(), out int soLuong))
                            {
                                // Tính tiền: (GiaBan * SoLuong) - (GiaBan * SoLuong * PhanTramGiam / 100)
                                decimal thanhTien = productInfo.GiaBan * soLuong;
                                if (productInfo.PhanTramGiam > 0)
                                {
                                    thanhTien = thanhTien - (thanhTien * productInfo.PhanTramGiam / 100);
                                }
                                total += thanhTien;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi tính tổng tiền cho row: {ex.Message}");
                    }
                }

                // Hiển thị tổng tiền
                txtTotal.Text = total.ToString("N0") + " đ";
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
                if (dgvOrder == null || txtEarnedPoints == null)
                    return;

                int totalPoints = 0;

                foreach (DataGridViewRow row in dgvOrder.Rows)
                {
                    // Lấy số lượng từ cột
                    if (row.Cells[2].Value != null && int.TryParse(row.Cells[2].Value.ToString(), out int soLuong))
                    {
                        // 1 sản phẩm = 1 điểm (theo số lượng)
                        totalPoints += soLuong;
                    }
                }

                // Hiển thị điểm tích lũy
                txtEarnedPoints.Text = totalPoints.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật điểm tích lũy: {ex.Message}");
                if (txtEarnedPoints != null)
                    txtEarnedPoints.Text = "0";
            }
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
                    LoadProductsFromDataTable(allProductsData);
                }
                else
                {
                    // Filter dữ liệu
                    string searchLower = searchText.ToLower();
                    DataTable filteredData = allProductsData.Clone();

                    foreach (DataRow row in allProductsData.Rows)
                    {
                        string tenSanPham = row["TenSanPham"]?.ToString()?.ToLower() ?? "";
                        string maSanPham = row["MaSanPham"]?.ToString() ?? "";

                        if (tenSanPham.Contains(searchLower) || maSanPham.Contains(searchText))
                        {
                            filteredData.ImportRow(row);
                        }
                    }

                    LoadProductsFromDataTable(filteredData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tìm kiếm: {ex.Message}");
            }
        }

        private void LoadProductsFromDataTable(DataTable dt)
        {
            try
            {
                if (dgvProducts == null || dt == null)
                    return;

                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        string tenSanPham = row["TenSanPham"]?.ToString() ?? "";
                        decimal giaBan = row["GiaBan"] != DBNull.Value ? Convert.ToDecimal(row["GiaBan"]) : 0;
                        int soLuong = row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : 0;
                        string khuyenMai = row["KhuyenMai"]?.ToString() ?? "";
                        decimal phanTramGiam = row["PhanTramGiam"] != DBNull.Value ? Convert.ToDecimal(row["PhanTramGiam"]) : 0;

                        // Format hiển thị
                        string giaBanStr = giaBan.ToString("N0") + " đ";
                        string khuyenMaiStr = string.IsNullOrEmpty(khuyenMai) ? "" : $"{khuyenMai} (-{phanTramGiam}%)";

                        // Thêm row vào DataGridView
                        dgvProducts.Rows.Add(tenSanPham, giaBanStr, soLuong, khuyenMaiStr);

                        // Lưu MaSanPham vào Tag của row để dùng sau
                        dgvProducts.Rows[dgvProducts.Rows.Count - 1].Tag = row["MaSanPham"];
                        count++;
                    }
                    catch (Exception rowEx)
                    {
                        Console.WriteLine($"Lỗi khi thêm row {count}: {rowEx.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load sản phẩm từ DataTable: {ex.Message}");
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
                        errorProviderUsePoints.SetError(txtUsePoints, "");
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
                            errorProviderUsePoints.SetError(txtUsePoints, "");
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
                // Kiểm tra đã chọn khách hàng chưa
                if (!selectedKhachHangId.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng trước khi thanh toán!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra có sản phẩm trong giỏ hàng không
                if (dgvOrder == null || dgvOrder.Rows.Count == 0)
                {
                    MessageBox.Show("Giỏ hàng đang trống. Vui lòng thêm sản phẩm vào giỏ hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra điểm sử dụng có vượt quá điểm hiện có không
                if (!ValidateUsePoints())
                {
                    MessageBox.Show("Số điểm sử dụng vượt quá điểm hiện có của khách hàng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (txtUsePoints != null)
                    {
                        txtUsePoints.Focus();
                        txtUsePoints.SelectAll();
                    }
                    return;
                }

                // TODO: Thực hiện thanh toán ở đây
                MessageBox.Show("Chức năng thanh toán đang được phát triển...", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán:\n\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsePoints_TextChanged(object? sender, EventArgs e)
        {
            ValidateUsePointsRealtime();
        }

        private void ValidateUsePointsRealtime()
        {
            if (txtUsePoints == null || errorProviderUsePoints == null)
                return;

            // Nếu chưa chọn khách hàng, không validate
            if (!availablePoints.HasValue)
            {
                errorProviderUsePoints.SetError(txtUsePoints, "");
                return;
            }

            string usePointsText = txtUsePoints.Text.Trim();

            // Nếu để trống, không hiển thị lỗi
            if (string.IsNullOrWhiteSpace(usePointsText))
            {
                errorProviderUsePoints.SetError(txtUsePoints, "");
                return;
            }

            // Kiểm tra có phải số không
            if (!int.TryParse(usePointsText, out int usePoints))
            {
                errorProviderUsePoints.SetError(txtUsePoints, "!");
                return;
            }

            // Kiểm tra số âm
            if (usePoints < 0)
            {
                errorProviderUsePoints.SetError(txtUsePoints, "!");
                return;
            }

            // Kiểm tra vượt quá điểm hiện có
            if (usePoints > availablePoints.Value)
            {
                errorProviderUsePoints.SetError(txtUsePoints, "!");
            }
            else
            {
                errorProviderUsePoints.SetError(txtUsePoints, "");
            }
        }

        private bool ValidateUsePoints()
        {
            if (txtUsePoints == null)
                return true; // Nếu không có ô nhập, coi như hợp lệ

            string usePointsText = txtUsePoints.Text.Trim();

            // Nếu để trống, coi như không dùng điểm (hợp lệ)
            if (string.IsNullOrWhiteSpace(usePointsText))
                return true;

            // Nếu chưa chọn khách hàng, không validate
            if (!availablePoints.HasValue)
                return true;

            // Kiểm tra có phải số không
            if (!int.TryParse(usePointsText, out int usePoints))
                return false;

            // Kiểm tra số âm
            if (usePoints < 0)
                return false;

            // Kiểm tra vượt quá điểm hiện có
            if (usePoints > availablePoints.Value)
                return false;

            return true;
        }
    }
}