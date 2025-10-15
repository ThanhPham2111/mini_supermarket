using System;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    public partial class ThemLoaiDialog : Form
    {
        private readonly LoaiSanPham_BUS _loaiBus = new();

        public LoaiDTO? CreatedLoai { get; private set; }

        public ThemLoaiDialog()
        {
            InitializeComponent();

            Load += ThemLoaiDialog_Load;
            confirmButton.Click += confirmButton_Click;
            closeButton.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        private void ThemLoaiDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            try
            {
                int nextId = _loaiBus.GetNextMaLoai();
                maLoaiTextBox.Text = nextId > 0 ? nextId.ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                maLoaiTextBox.Text = string.Empty;
                MessageBox.Show(this,
                    $"Không thể tải mã loại tiếp theo.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
                var created = _loaiBus.AddLoai(tenLoai, moTa);
                CreatedLoai = created;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Không thể thêm loại mới.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
