using System;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    public partial class ThemThuongHieuDialog : Form
    {
        private readonly ThuongHieu_BUS _bus = new();

        public ThuongHieuDTO? CreatedThuongHieu { get; private set; }

        public ThemThuongHieuDialog()
        {
            InitializeComponent();

            Load += ThemThuongHieuDialog_Load;
            confirmButton.Click += confirmButton_Click;
            closeButton.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        private void ThemThuongHieuDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            statusComboBox.Items.Clear();
            statusComboBox.Items.Add(TrangThaiConstants.HoatDong);
            statusComboBox.Items.Add(TrangThaiConstants.NgungHoatDong);
            statusComboBox.SelectedIndex = 0;

            try
            {
                int nextId = _bus.GetNextMaThuongHieu();
                maTextBox.Text = nextId > 0 ? nextId.ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                maTextBox.Text = string.Empty;
                MessageBox.Show(this,
                    $"Không thể lấy mã thương hiệu tiếp theo.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void confirmButton_Click(object? sender, EventArgs e)
        {
            string tenThuongHieu = tenTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenThuongHieu))
            {
                MessageBox.Show(this,
                    "Tên thương hiệu không được để trống.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tenTextBox.Focus();
                return;
            }

            string trangThai = statusComboBox.SelectedItem as string ?? TrangThaiConstants.HoatDong;

            try
            {
                var created = _bus.AddThuongHieu(tenThuongHieu, trangThai);
                CreatedThuongHieu = created;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Không thể thêm thương hiệu mới.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
