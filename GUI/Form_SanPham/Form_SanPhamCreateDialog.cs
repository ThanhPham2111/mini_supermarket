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
    public partial class Form_SanPhamCreateDialog : Form
    {
        private readonly SanPham_BUS _sanPhamBus = new();
        private readonly NhaCungCap_BUS _nhaCungCapBus = new();
        private IList<DonViDTO> _donViList = Array.Empty<DonViDTO>();
        private IList<LoaiDTO> _loaiList = Array.Empty<LoaiDTO>();
        private IList<ThuongHieuDTO> _thuongHieuList = Array.Empty<ThuongHieuDTO>();
        private IList<NhaCungCapDTO> _nhaCungCapList = Array.Empty<NhaCungCapDTO>();
        private string? _selectedImagePath;

        public SanPhamDTO? CreatedSanPham { get; private set; }

        public Form_SanPhamCreateDialog()
        {
            InitializeComponent();
            Load += Form_SanPhamCreateDialog_Load;
            saveButton.Click += saveButton_Click;
            cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
            chonAnhButton.Click += chonAnhButton_Click;
        }

        private void Form_SanPhamCreateDialog_Load(object? sender, EventArgs e)
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
                _nhaCungCapList = _nhaCungCapBus.GetNhaCungCap(NhaCungCap_BUS.StatusActive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể tải dữ liệu tham chiếu.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            BindComboBox(donViComboBox, _donViList, nameof(DonViDTO.TenDonVi), nameof(DonViDTO.MaDonVi));
            BindComboBox(loaiComboBox, _loaiList, nameof(LoaiDTO.TenLoai), nameof(LoaiDTO.MaLoai));
            BindComboBox(thuongHieuComboBox, _thuongHieuList, nameof(ThuongHieuDTO.TenThuongHieu), nameof(ThuongHieuDTO.MaThuongHieu));
            BindComboBox(nhaCungCapComboBox, _nhaCungCapList, nameof(NhaCungCapDTO.TenNhaCungCap), nameof(NhaCungCapDTO.MaNhaCungCap));

            hsdDateTimePicker.Format = DateTimePickerFormat.Custom;
            hsdDateTimePicker.CustomFormat = "dd/MM/yyyy";
            hsdDateTimePicker.ShowCheckBox = true;
            hsdDateTimePicker.Checked = false;
        }

        private static void BindComboBox<T>(ComboBox comboBox, IList<T> items, string displayMember, string valueMember)
        {
            comboBox.DataSource = items;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.SelectedIndex = items.Count > 0 ? 0 : -1;
        }

        private void chonAnhButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Chọn hình ảnh",
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
                MessageBox.Show(this, "Tên sản phẩm không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tenSanPhamTextBox.Focus();
                return;
            }

            if (donViComboBox.SelectedValue is not int maDonVi || maDonVi <= 0)
            {
                MessageBox.Show(this, "Vui lòng chọn đơn vị.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                donViComboBox.Focus();
                return;
            }

            if (loaiComboBox.SelectedValue is not int maLoai || maLoai <= 0)
            {
                MessageBox.Show(this, "Vui lòng chọn loại sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                loaiComboBox.Focus();
                return;
            }

            if (thuongHieuComboBox.SelectedValue is not int maThuongHieu || maThuongHieu <= 0)
            {
                MessageBox.Show(this, "Vui lòng chọn thương hiệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                thuongHieuComboBox.Focus();
                return;
            }

            decimal? giaBan = null;
            if (!string.IsNullOrWhiteSpace(giaBanTextBox.Text))
            {
                if (!decimal.TryParse(giaBanTextBox.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out var parsedGiaBan) || parsedGiaBan < 0)
                {
                    MessageBox.Show(this, "Giá bán không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    giaBanTextBox.Focus();
                    return;
                }

                giaBan = parsedGiaBan;
            }

            // Trạng thái luôn mặc định là "Còn hàng"
            string trangThai = SanPham_BUS.StatusConHang;

            DateTime? hsd = hsdDateTimePicker.Checked ? hsdDateTimePicker.Value.Date : null;
            string? xuatXu = string.IsNullOrWhiteSpace(xuatXuTextBox.Text) ? null : xuatXuTextBox.Text.Trim();
            string? moTa = string.IsNullOrWhiteSpace(moTaTextBox.Text) ? null : moTaTextBox.Text.Trim();

            (string Relative, string Full)? copiedImage = null;
            try
            {
                copiedImage = CopyImageIfNeeded();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể sao chép hình ảnh.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sanPham = new SanPhamDTO
            {
                TenSanPham = tenSanPham,
                MaDonVi = maDonVi,
                MaLoai = maLoai,
                MaThuongHieu = maThuongHieu,
                GiaBan = giaBan,
                TrangThai = trangThai,
                Hsd = hsd,
                XuatXu = xuatXu,
                MoTa = moTa,
                HinhAnh = copiedImage?.Relative
            };

            try
            {
                var created = _sanPhamBus.AddSanPham(sanPham);
                CreatedSanPham = created;
                if (copiedImage?.Relative != null)
                {
                    CreatedSanPham.HinhAnh = copiedImage.Value.Relative;
                }

                // Lưu liên kết nhà cung cấp - sản phẩm nếu đã chọn nhà cung cấp
                if (nhaCungCapComboBox.SelectedValue is int maNhaCungCap && maNhaCungCap > 0)
                {
                    try
                    {
                        _nhaCungCapBus.LinkSanPhamToNhaCungCap(maNhaCungCap, created.MaSanPham);
                    }
                    catch (Exception ex)
                    {
                        // Log lỗi nhưng không chặn việc tạo sản phẩm
                        MessageBox.Show(this, $"Đã tạo sản phẩm nhưng không thể liên kết với nhà cung cấp.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                if (copiedImage?.Full != null && File.Exists(copiedImage.Value.Full))
                {
                    try
                    {
                        File.Delete(copiedImage.Value.Full);
                    }
                    catch
                    {
                        // ignore cleanup failure
                    }
                }

                MessageBox.Show(this, $"Không thể thêm sản phẩm.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private (string Relative, string Full)? CopyImageIfNeeded()
        {
            if (string.IsNullOrWhiteSpace(_selectedImagePath))
            {
                return null;
            }

            string sourcePath = _selectedImagePath;
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException("Không tìm thấy tệp hình ảnh đã chọn.", sourcePath);
            }

            string imageDirectory = Path.Combine(AppContext.BaseDirectory, "img");
            Directory.CreateDirectory(imageDirectory);

            string extension = Path.GetExtension(sourcePath);
            if (string.IsNullOrWhiteSpace(extension))
            {
                extension = ".jpg";
            }

            string fileName = $"{Guid.NewGuid():N}{extension}";
            string destinationPath = Path.Combine(imageDirectory, fileName);
            File.Copy(sourcePath, destinationPath, overwrite: true);

            string relativePath = Path.Combine("img", fileName);
            return (relativePath, destinationPath);
        }
    }
}