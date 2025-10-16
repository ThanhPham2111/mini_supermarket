using System;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    public partial class SuaLoaiDialog : Form
    {
        private readonly LoaiSanPham_BUS _loaiBus = new();
        private readonly LoaiDTO _loaiToEdit;

        public LoaiDTO? UpdatedLoai { get; private set; }

        public SuaLoaiDialog(LoaiDTO loai)
        {
            _loaiToEdit = loai ?? throw new ArgumentNullException(nameof(loai));

            InitializeComponent();

            Load += SuaLoaiDialog_Load;
            confirmButton.Click += confirmButton_Click;
            closeButton.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        private void SuaLoaiDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            maLoaiTextBox.Text = _loaiToEdit.MaLoai.ToString();
            tenLoaiTextBox.Text = _loaiToEdit.TenLoai ?? string.Empty;
            moTaTextBox.Text = _loaiToEdit.MoTa ?? string.Empty;
        }

        private void confirmButton_Click(object? sender, EventArgs e)
        {
            string tenLoai = tenLoaiTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenLoai))
            {
                MessageBox.Show(this,
                    "Tên loại không được để trống.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tenLoaiTextBox.Focus();
                return;
            }

            string? moTa = string.IsNullOrWhiteSpace(moTaTextBox.Text) ? null : moTaTextBox.Text.Trim();

            try
            {
                var updated = _loaiBus.UpdateLoai(_loaiToEdit.MaLoai, tenLoai, moTa);
                UpdatedLoai = updated;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Không thể cập nhật loại.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

