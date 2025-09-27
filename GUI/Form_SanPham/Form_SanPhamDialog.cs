using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_SanPham
{
    public partial class Form_SanPhamDialog : Form
    {
        private const string ImagePath = "C:/Users/T480s/Desktop/mini_supermarket/img/pexels-chevanon-312418.jpg";

        private readonly SanPhamDTO _sanPham;

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

            PopulateComboBox(donViComboBox, _sanPham.DonVi);

            maThuongHieuTextBox.Text = _sanPham.MaThuongHieu.ToString(CultureInfo.InvariantCulture);
            PopulateComboBox(maLoaiComboBox, _sanPham.MaLoai.ToString(CultureInfo.InvariantCulture));

            giaBanTextBox.Text = _sanPham.GiaBan.HasValue
                ? _sanPham.GiaBan.Value.ToString("N0", CultureInfo.CurrentCulture)
                : string.Empty;

            xuatXuTextBox.Text = _sanPham.XuatXu ?? string.Empty;
            hsdTextBox.Text = _sanPham.Hsd.HasValue
                ? _sanPham.Hsd.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)
                : string.Empty;

            PopulateComboBox(trangThaiComboBox, _sanPham.TrangThai);
            moTaTextBox.Text = _sanPham.MoTa ?? string.Empty;

            LoadProductImage();
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
            try
            {
                if (!File.Exists(ImagePath))
                {
                    productPictureBox.Image = null;
                    return;
                }

                using var image = Image.FromFile(ImagePath);
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