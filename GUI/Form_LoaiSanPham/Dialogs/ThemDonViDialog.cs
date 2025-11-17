using System;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.Form_LoaiSanPham.Dialogs
{
    public partial class ThemDonViDialog : Form
    {
        private readonly DonVi_BUS _bus = new();

        public DonViDTO? CreatedDonVi { get; private set; }

        public ThemDonViDialog()
        {
            InitializeComponent();

            Load += ThemDonViDialog_Load;
            confirmButton.Click += confirmButton_Click;
            closeButton.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        private void ThemDonViDialog_Load(object? sender, EventArgs e)
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
                int nextId = _bus.GetNextMaDonVi();
                maTextBox.Text = nextId > 0 ? nextId.ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                maTextBox.Text = string.Empty;
                MessageBox.Show(this,
                    $"Không thể lấy mã đơn vị tiếp theo.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
                var created = _bus.AddDonVi(tenDonVi, moTa, trangThai);
                CreatedDonVi = created;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Không thể thêm đơn vị mới.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
