using System;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
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

            statusComboBox.Items.Clear();
            statusComboBox.Items.Add(TrangThaiConstants.HoatDong);
            statusComboBox.Items.Add(TrangThaiConstants.NgungHoatDong);

            maTextBox.Text = _thuongHieu.MaThuongHieu.ToString();
            tenTextBox.Text = _thuongHieu.TenThuongHieu ?? string.Empty;

            string currentStatus = string.IsNullOrWhiteSpace(_thuongHieu.TrangThai)
                ? TrangThaiConstants.HoatDong
                : _thuongHieu.TrangThai;
            int idx = statusComboBox.Items.IndexOf(currentStatus);
            statusComboBox.SelectedIndex = idx >= 0 ? idx : 0;
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
                var updated = _bus.UpdateThuongHieu(_thuongHieu.MaThuongHieu, tenThuongHieu, trangThai);
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
