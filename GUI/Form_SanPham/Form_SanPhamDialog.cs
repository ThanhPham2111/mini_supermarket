using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_SanPham
{
    public partial class Form_SanPhamDialog : Form
    {
        private readonly SanPhamDTO _sanPham;
        private readonly NhaCungCap_BUS _nhaCungCapBus = new();

        public Form_SanPhamDialog(SanPhamDTO sanPham)
        {
            _sanPham = sanPham ?? throw new ArgumentNullException(nameof(sanPham));

            InitializeComponent();
            Load += Form_SanPhamDialog_Load;
        }

        private void Form_SanPhamDialog_Load(object? sender, EventArgs e)
        {
            maSanPhamTextBox.Text = _sanPham.MaSanPham.ToString(CultureInfo.InvariantCulture);
            tenSanPhamTextBox.Text = _sanPham.TenSanPham ?? string.Empty;

            PopulateComboBox(donViComboBox, _sanPham.TenDonVi);

            maThuongHieuTextBox.Text = _sanPham.TenThuongHieu ?? string.Empty; // Hien thi ten thuong hieu
            PopulateComboBox(maLoaiComboBox, _sanPham.TenLoai ?? string.Empty); // Hien thi ten loai

            // Lấy giá nhập mới nhất từ ChiTietPhieuNhap (giống như quản lý % lợi nhuận)
            var khoHangBus = new KhoHangBUS();
            decimal? giaNhap = khoHangBus.GetGiaNhapMoiNhat(_sanPham.MaSanPham);
            giaBanTextBox.Text = giaNhap.HasValue && giaNhap.Value > 0
                ? giaNhap.Value.ToString("N0", CultureInfo.CurrentCulture)
                : (_sanPham.GiaBan.HasValue ? _sanPham.GiaBan.Value.ToString("N0", CultureInfo.CurrentCulture) : string.Empty);

            xuatXuTextBox.Text = _sanPham.XuatXu ?? string.Empty;
            hsdTextBox.Text = _sanPham.Hsd.HasValue
                ? _sanPham.Hsd.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)
                : string.Empty;

            // Load nhà cung cấp thay vì trạng thái
            LoadNhaCungCap();
            moTaTextBox.Text = _sanPham.MoTa ?? string.Empty;

            LoadProductImage();
        }

        private void LoadNhaCungCap()
        {
            try
            {
                int? maNhaCungCap = _nhaCungCapBus.GetMaNhaCungCapBySanPham(_sanPham.MaSanPham);
                if (maNhaCungCap.HasValue && maNhaCungCap.Value > 0)
                {
                    var nhaCungCapList = _nhaCungCapBus.GetNhaCungCap(NhaCungCap_BUS.StatusActive);
                    var nhaCungCap = nhaCungCapList.FirstOrDefault(ncc => ncc.MaNhaCungCap == maNhaCungCap.Value);
                    if (nhaCungCap != null)
                    {
                        PopulateComboBox(nhaCungCapComboBox, nhaCungCap.TenNhaCungCap);
                    }
                    else
                    {
                        PopulateComboBox(nhaCungCapComboBox, null);
                    }
                }
                else
                {
                    PopulateComboBox(nhaCungCapComboBox, null);
                }
            }
            catch
            {
                PopulateComboBox(nhaCungCapComboBox, null);
            }
        }

        private static void PopulateComboBox(ComboBox comboBox, string? value)
        {
            comboBox.Items.Clear();
            comboBox.Enabled = false;

            if (!string.IsNullOrWhiteSpace(value))
            {
                comboBox.Items.Add(value);
                comboBox.SelectedIndex = 0;
            }
            else
            {
                comboBox.Items.Add(string.Empty);
                comboBox.SelectedIndex = 0;
            }
        }

        private void LoadProductImage()
        {
            string? imagePath = _sanPham.HinhAnh;

            if (!string.IsNullOrWhiteSpace(imagePath) && !Path.IsPathRooted(imagePath))
            {
                imagePath = Path.Combine(AppContext.BaseDirectory, imagePath);
            }

            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                productPictureBox.Image = null;
                return;
            }

            try
            {
                using var image = Image.FromFile(imagePath);
                productPictureBox.Image = new Bitmap(image);
            }
            catch
            {
                productPictureBox.Image = null;
            }
        }

        private void closeButton_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
