using System;
using System.Windows.Forms;
using mini_supermarket.BUS;
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

            maDonViTextBox.Text = _donVi.MaDonVi.ToString();
            tenDonViTextBox.Text = _donVi.TenDonVi ?? string.Empty;
            moTaTextBox.Text = _donVi.MoTa ?? string.Empty;
        }

        private void confirmButton_Click(object? sender, EventArgs e)
        {
            string tenDonVi = tenDonViTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenDonVi))
            {
                MessageBox.Show(this,
                    "Tên đơn vị không được để trống.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tenDonViTextBox.Focus();
                return;
            }

            string? moTa = string.IsNullOrWhiteSpace(moTaTextBox.Text) ? null : moTaTextBox.Text.Trim();

            try
            {
                var updated = _bus.UpdateDonVi(_donVi.MaDonVi, tenDonVi, moTa);
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
