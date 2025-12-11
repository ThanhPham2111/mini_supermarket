using System;
using System.Windows.Forms;

namespace mini_supermarket.GUI.HoaDon
{
    public partial class Dialog_HuyHoaDon : Form
    {
        private TextBox txtLyDo;
        private Button btnOK;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblLyDo;
        private Label lblThongBao;

        public string LyDoHuy { get; private set; } = string.Empty;

        public Dialog_HuyHoaDon(int maHoaDon, string thongTinHoaDon)
        {
            InitializeComponent(maHoaDon, thongTinHoaDon);
        }

        private void InitializeComponent(int maHoaDon, string thongTinHoaDon)
        {
            this.Text = "Hủy hóa đơn";
            this.Size = new System.Drawing.Size(750, 550);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;
            this.Padding = new Padding(10);

            // Title
            lblTitle = new Label
            {
                Text = $"Xác nhận hủy hóa đơn #{maHoaDon}",
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Location = new System.Drawing.Point(20, 20),
                ForeColor = System.Drawing.Color.FromArgb(220, 53, 69)
            };
            this.Controls.Add(lblTitle);

            // Thông báo
            lblThongBao = new Label
            {
                Text = thongTinHoaDon,
                Font = new System.Drawing.Font("Segoe UI", 9),
                AutoSize = false,
                Location = new System.Drawing.Point(20, 55),
                Size = new System.Drawing.Size(690, 60),
                ForeColor = System.Drawing.Color.FromArgb(108, 117, 125)
            };
            this.Controls.Add(lblThongBao);

            // Label Lý do
            lblLyDo = new Label
            {
                Text = "Lý do hủy:",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Location = new System.Drawing.Point(20, 130)
            };
            this.Controls.Add(lblLyDo);

            // TextBox Lý do
            txtLyDo = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new System.Drawing.Font("Segoe UI", 10),
                Location = new System.Drawing.Point(20, 155),
                Size = new System.Drawing.Size(690, 280),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            this.Controls.Add(txtLyDo);

            // Button OK
            btnOK = new Button
            {
                Text = "Xác nhận hủy",
                DialogResult = DialogResult.OK,
                Size = new System.Drawing.Size(140, 35),
                Location = new System.Drawing.Point(470, 450),
                BackColor = System.Drawing.Color.FromArgb(220, 53, 69),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.Click += BtnOK_Click;
            this.Controls.Add(btnOK);

            // Button Cancel
            btnCancel = new Button
            {
                Text = "Hủy bỏ",
                DialogResult = DialogResult.Cancel,
                Size = new System.Drawing.Size(120, 35),
                Location = new System.Drawing.Point(600, 450),
                BackColor = System.Drawing.Color.FromArgb(108, 117, 125),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnCancel);

            // Set AcceptButton và CancelButton
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void BtnOK_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLyDo.Text))
            {
                MessageBox.Show("Vui lòng nhập lý do hủy hóa đơn!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            LyDoHuy = txtLyDo.Text.Trim();
        }
    }
}

