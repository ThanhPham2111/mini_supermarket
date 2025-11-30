using System;
using System.Windows.Forms;

namespace mini_supermarket.GUI.PhieuNhap
{
    public partial class Dialog_HuyPhieuNhap : Form
    {
        private TextBox txtLyDo;
        private Button btnOK;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblLyDo;

        public string LyDoHuy { get; private set; } = string.Empty;

        public Dialog_HuyPhieuNhap(string maPhieuNhap)
        {
            InitializeComponent(maPhieuNhap);
        }

        private void InitializeComponent(string maPhieuNhap)
        {
            this.Text = "Hủy phiếu nhập";
            this.Size = new System.Drawing.Size(750, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;
            this.Padding = new Padding(10);

            // Title
            lblTitle = new Label
            {
                Text = $"Nhập lý do hủy phiếu nhập {maPhieuNhap}",
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Location = new System.Drawing.Point(20, 20)
            };
            this.Controls.Add(lblTitle);

            // Label Lý do
            lblLyDo = new Label
            {
                Text = "Lý do hủy:",
                Font = new System.Drawing.Font("Segoe UI", 10),
                AutoSize = true,
                Location = new System.Drawing.Point(20, 60)
            };
            this.Controls.Add(lblLyDo);

            // TextBox Lý do - tăng chiều cao để tận dụng không gian thêm
            txtLyDo = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new System.Drawing.Font("Segoe UI", 10),
                Location = new System.Drawing.Point(20, 85),
                Size = new System.Drawing.Size(690, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(txtLyDo);

            // Button OK - giữ nguyên vị trí
            btnOK = new Button
            {
                Text = "Xác nhận",
                DialogResult = DialogResult.OK,
                Size = new System.Drawing.Size(120, 35),
                Location = new System.Drawing.Point(490, 400),
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

            // Button Cancel - giữ nguyên vị trí
            btnCancel = new Button
            {
                Text = "Hủy",
                DialogResult = DialogResult.Cancel,
                Size = new System.Drawing.Size(120, 35),
                Location = new System.Drawing.Point(620, 400),
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
                MessageBox.Show("Vui lòng nhập lý do hủy phiếu nhập!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            LyDoHuy = txtLyDo.Text.Trim();
        }
    }
}

