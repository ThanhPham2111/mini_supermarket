using System;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    public partial class SuaDonViDialog : Form
    {
        private readonly DonVi_BUS _bus = new();
        private readonly DonViDTO _donVi;

        public DonViDTO? UpdatedDonVi { get; private set; }

        public SuaDonViDialog(DonViDTO donVi)
        {
            _donVi = donVi ?? throw new ArgumentNullException(nameof(donVi));

            InitializeComponent();

            Load += SuaDonViDialog_Load;
            confirmButton.Click += confirmButton_Click;
            closeButton.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        private void SuaDonViDialog_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            statusComboBox.Items.Clear();
            statusComboBox.Items.Add(TrangThaiConstants.HoatDong);
            statusComboBox.Items.Add(TrangThaiConstants.NgungHoatDong);

            maTextBox.Text = _donVi.MaDonVi.ToString();
            tenTextBox.Text = _donVi.TenDonVi ?? string.Empty;
            moTaTextBox.Text = _donVi.MoTa ?? string.Empty;

            string currentStatus = string.IsNullOrWhiteSpace(_donVi.TrangThai)
                ? TrangThaiConstants.HoatDong
                : _donVi.TrangThai;
            int idx = statusComboBox.Items.IndexOf(currentStatus);
            statusComboBox.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private void confirmButton_Click(object? sender, EventArgs e)
        {
            string tenDonVi = tenTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenDonVi))
            {
                MessageBox.Show(this,
                    "Tên đơn vị không được để trống.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tenTextBox.Focus();
                return;
            }

            string? moTa = string.IsNullOrWhiteSpace(moTaTextBox.Text) ? null : moTaTextBox.Text.Trim();
            string trangThai = statusComboBox.SelectedItem as string ?? TrangThaiConstants.HoatDong;

            try
            {
                var updated = _bus.UpdateDonVi(_donVi.MaDonVi, tenDonVi, moTa, trangThai);
                UpdatedDonVi = updated;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Không thể cập nhật đơn vị.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
