using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_SanPham
{
    public partial class Form_SanPhamUpdate : Form
    {
        private readonly SanPham_BUS _sanPhamBus = new();
        private readonly SanPhamDTO _original;
        private IList<DonViDTO> _donViList = Array.Empty<DonViDTO>();
        private IList<LoaiDTO> _loaiList = Array.Empty<LoaiDTO>();
        private IList<ThuongHieuDTO> _thuongHieuList = Array.Empty<ThuongHieuDTO>();
        private string? _selectedImagePath; // absolute path selected by user
        private string? _existingImageRelative; // current relative image path in DB

        public SanPhamDTO? UpdatedSanPham { get; private set; }

        public Form_SanPhamUpdate(SanPhamDTO sanPham)
        {
            _original = sanPham ?? throw new ArgumentNullException(nameof(sanPham));

            InitializeComponent();

            Load += Form_SanPhamUpdate_Load;
            saveButton.Click += saveButton_Click;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
            chonAnhButton.Click += chonAnhButton_Click;
        }

        private void Form_SanPhamUpdate_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            try
            {
                _donViList = _sanPhamBus.GetDonViList();
                _loaiList = _sanPhamBus.GetLoaiList();
                _thuongHieuList = _sanPhamBus.GetThuongHieuList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Khong the tai du lieu tham chieu.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            BindComboBox(donViComboBox, _donViList, nameof(DonViDTO.TenDonVi), nameof(DonViDTO.MaDonVi));
            BindComboBox(loaiComboBox, _loaiList, nameof(LoaiDTO.TenLoai), nameof(LoaiDTO.MaLoai));
            BindComboBox(thuongHieuComboBox, _thuongHieuList, nameof(ThuongHieuDTO.TenThuongHieu), nameof(ThuongHieuDTO.MaThuongHieu));

            trangThaiComboBox.Items.Clear();
            trangThaiComboBox.Items.Add(SanPham_BUS.StatusConHang);
            trangThaiComboBox.Items.Add(SanPham_BUS.StatusHetHang);

            hsdDateTimePicker.Format = DateTimePickerFormat.Custom;
            hsdDateTimePicker.CustomFormat = "dd/MM/yyyy";
            hsdDateTimePicker.ShowCheckBox = true;

            // Populate data from original
            maSanPhamTextBox.Text = _original.MaSanPham.ToString(CultureInfo.InvariantCulture);
            tenSanPhamTextBox.Text = _original.TenSanPham ?? string.Empty;

            SelectComboValue(donViComboBox, _original.MaDonVi);
            SelectComboValue(loaiComboBox, _original.MaLoai);
            SelectComboValue(thuongHieuComboBox, _original.MaThuongHieu);

            giaBanTextBox.Text = _original.GiaBan.HasValue ? _original.GiaBan.Value.ToString("N0", CultureInfo.CurrentCulture) : string.Empty;
            xuatXuTextBox.Text = _original.XuatXu ?? string.Empty;

            if (_original.Hsd.HasValue)
            {
                hsdDateTimePicker.Value = _original.Hsd.Value.Date;
                hsdDateTimePicker.Checked = true;
            }
            else
            {
                hsdDateTimePicker.Checked = false;
            }

            string trangThai = string.IsNullOrWhiteSpace(_original.TrangThai) ? SanPham_BUS.StatusConHang : _original.TrangThai!;
            int idx = trangThaiComboBox.Items.IndexOf(trangThai);
            trangThaiComboBox.SelectedIndex = idx >= 0 ? idx : 0;

            moTaTextBox.Text = _original.MoTa ?? string.Empty;

            _existingImageRelative = _original.HinhAnh;
            if (!string.IsNullOrWhiteSpace(_existingImageRelative))
            {
                hinhAnhTextBox.Text = _existingImageRelative;
                // Resolve to full path for preview
                string full = _existingImageRelative;
                if (!Path.IsPathRooted(full))
                {
                    full = Path.Combine(AppContext.BaseDirectory, full);
                }
                LoadImagePreview(File.Exists(full) ? full : null);
            }
        }

        private static void BindComboBox<T>(ComboBox comboBox, IList<T> items, string displayMember, string valueMember)
        {
            comboBox.DataSource = items;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.SelectedIndex = items.Count > 0 ? 0 : -1;
        }

        private static void SelectComboValue(ComboBox comboBox, int value)
        {
            if (value <= 0)
            {
                comboBox.SelectedIndex = -1;
                return;
            }

            comboBox.SelectedValue = value;
        }

        private void chonAnhButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Chon hinh anh",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*",
                Multiselect = false
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            _selectedImagePath = dialog.FileName;
            hinhAnhTextBox.Text = _selectedImagePath;
            LoadImagePreview(_selectedImagePath);
        }

        private void LoadImagePreview(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                if (productPictureBox.Image != null)
                {
                    var old = productPictureBox.Image;
                    productPictureBox.Image = null;
                    old.Dispose();
                }
                return;
            }

            try
            {
                using var image = Image.FromFile(path);
                var clone = new Bitmap(image);
                if (productPictureBox.Image != null)
                {
                    var old = productPictureBox.Image;
                    productPictureBox.Image = null;
                    old.Dispose();
                }

                productPictureBox.Image = clone;
            }
            catch
            {
                if (productPictureBox.Image != null)
                {
                    var old = productPictureBox.Image;
                    productPictureBox.Image = null;
                    old.Dispose();
                }
            }
        }

        private void saveButton_Click(object? sender, EventArgs e)
        {
            string tenSanPham = tenSanPhamTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenSanPham))
            {
                MessageBox.Show(this, "Vui long nhap ten san pham.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tenSanPhamTextBox.Focus();
                return;
            }

            if (donViComboBox.SelectedValue is not int maDonVi || maDonVi <= 0)
            {
                MessageBox.Show(this, "Vui long chon don vi.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                donViComboBox.Focus();
                return;
            }

            if (loaiComboBox.SelectedValue is not int maLoai || maLoai <= 0)
            {
                MessageBox.Show(this, "Vui long chon loai san pham.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                loaiComboBox.Focus();
                return;
            }

            if (thuongHieuComboBox.SelectedValue is not int maThuongHieu || maThuongHieu <= 0)
            {
                MessageBox.Show(this, "Vui long chon thuong hieu.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                thuongHieuComboBox.Focus();
                return;
            }

            decimal? giaBan = null;
            if (!string.IsNullOrWhiteSpace(giaBanTextBox.Text))
            {
                if (!decimal.TryParse(giaBanTextBox.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out var parsedGiaBan) || parsedGiaBan < 0)
                {
                    MessageBox.Show(this, "Gia ban khong hop le.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    giaBanTextBox.Focus();
                    return;
                }

                giaBan = parsedGiaBan;
            }

            string? trangThai = trangThaiComboBox.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(trangThai))
            {
                MessageBox.Show(this, "Vui long chon trang thai.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                trangThaiComboBox.Focus();
                return;
            }

            DateTime? hsd = hsdDateTimePicker.Checked ? hsdDateTimePicker.Value.Date : (DateTime?)null;
            string? xuatXu = string.IsNullOrWhiteSpace(xuatXuTextBox.Text) ? null : xuatXuTextBox.Text.Trim();
            string? moTa = string.IsNullOrWhiteSpace(moTaTextBox.Text) ? null : moTaTextBox.Text.Trim();

            string? hinhAnhToSave = _existingImageRelative;
            (string Relative, string Full)? copied = null;
            if (!string.IsNullOrWhiteSpace(_selectedImagePath))
            {
                try
                {
                    copied = CopyImageToAppFolder(_selectedImagePath!);
                    hinhAnhToSave = copied.Value.Relative;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Khong the sao chep hinh anh.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            var updated = new SanPhamDTO
            {
                MaSanPham = _original.MaSanPham,
                TenSanPham = tenSanPham,
                MaDonVi = maDonVi,
                MaLoai = maLoai,
                MaThuongHieu = maThuongHieu,
                GiaBan = giaBan,
                TrangThai = trangThai,
                Hsd = hsd,
                XuatXu = xuatXu,
                MoTa = moTa,
                HinhAnh = hinhAnhToSave
            };

            try
            {
                _sanPhamBus.UpdateSanPham(updated);
                UpdatedSanPham = updated;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                // Cleanup newly copied image on failure
                if (copied?.Full != null && File.Exists(copied.Value.Full))
                {
                    try { File.Delete(copied.Value.Full); } catch { /* ignore */ }
                }

                MessageBox.Show(this, $"Khong the cap nhat san pham.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static (string Relative, string Full) CopyImageToAppFolder(string sourcePath)
        {
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException("Khong tim thay tep hinh anh.", sourcePath);
            }

            string imageDirectory = Path.Combine(AppContext.BaseDirectory, "img");
            Directory.CreateDirectory(imageDirectory);

            string extension = Path.GetExtension(sourcePath);
            if (string.IsNullOrWhiteSpace(extension)) extension = ".jpg";

            string fileName = $"{Guid.NewGuid():N}{extension}";
            string destinationPath = Path.Combine(imageDirectory, fileName);
            File.Copy(sourcePath, destinationPath, overwrite: true);

            string relativePath = Path.Combine("img", fileName);
            return (relativePath, destinationPath);
        }
    }
}
