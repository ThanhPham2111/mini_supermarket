using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using mini_supermarket.BUS;

namespace mini_supermarket.GUI.Form_BanHang
{
    public partial class Form_banHang : Form
    {
        private KhoHangBUS? khoHangBUS;

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

                // Thêm dữ liệu mới
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

                // Log thành công
                Console.WriteLine($"Đã tải {count} sản phẩm thành công!");
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

                // Hiển thị thông tin sản phẩm
                txtProductName.Text = row["TenSanPham"]?.ToString() ?? "";
                
                decimal giaBan = row["GiaBan"] != DBNull.Value ? Convert.ToDecimal(row["GiaBan"]) : 0;
                txtUnitPrice.Text = giaBan.ToString("N0") + " đ";

                // Số lượng mặc định là 1
                txtQuantity.Text = "1";

                // Khuyến mãi
                string khuyenMai = row["KhuyenMai"]?.ToString() ?? "";
                decimal phanTramGiam = row["PhanTramGiam"] != DBNull.Value ? Convert.ToDecimal(row["PhanTramGiam"]) : 0;
                if (!string.IsNullOrEmpty(khuyenMai) && phanTramGiam > 0)
                {
                    txtPromotion.Text = $"{khuyenMai} (-{phanTramGiam}%)";
                }
                else
                {
                    txtPromotion.Text = "";
                }

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

                // TODO: Thêm sản phẩm vào giỏ hàng (dgvOrder)
                // Tạm thời chỉ hiển thị thông báo
                MessageBox.Show($"Đã thêm {soLuongNhap} sản phẩm vào giỏ hàng!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset số lượng về 1 sau khi thêm
                txtQuantity.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm vào giỏ:\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}