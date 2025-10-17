using System;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    public partial class SuaThuongHieuDialog : Form
    {
        private readonly ThuongHieu_BUS _bus = new();
        private readonly ThuongHieuDTO _thuongHieu;

        public ThuongHieuDTO? UpdatedThuongHieu { get; private set; }

        public SuaThuongHieuDialog(ThuongHieuDTO thuongHieu)
        {
            _thuongHieu = thuongHieu ?? throw new ArgumentNullException(nameof(thuongHieu));

            InitializeComponent();

            Load += SuaThuongHieuDialog_Load;
            confirmButton.Click += confirmButton_Click;
            closeButton.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        private void SuaThuongHieuDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            maThuongHieuTextBox.Text = _thuongHieu.MaThuongHieu.ToString();
            tenThuongHieuTextBox.Text = _thuongHieu.TenThuongHieu ?? string.Empty;
        }

        private void confirmButton_Click(object? sender, EventArgs e)
        {
            string tenThuongHieu = tenThuongHieuTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenThuongHieu))
            {
                MessageBox.Show(this,
                    "Tên thương hiệu không được để trống.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tenThuongHieuTextBox.Focus();
                return;
            }

            try
            {
                var updated = _bus.UpdateThuongHieu(_thuongHieu.MaThuongHieu, tenThuongHieu);
                UpdatedThuongHieu = updated;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Không thể cập nhật thương hiệu.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
