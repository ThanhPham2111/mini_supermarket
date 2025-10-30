using System;
using System.Data;
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
        }
    }
}