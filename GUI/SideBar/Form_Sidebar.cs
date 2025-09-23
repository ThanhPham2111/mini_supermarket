using System;
using System.Drawing;
using System.Windows.Forms;
using mini_supermarket.GUI.Form_BanHang;

namespace mini_supermarket.GUI.SideBar
{
    public partial class Form_Sidebar : Form
    {
        private Button? _activeButton;
        private Form? _activeForm;

        public Form_Sidebar()
        {
            InitializeComponent();
            InitializeLayout();
            ShowBanHang();
        }

        private void InitializeLayout()
        {
            ClientSize = new Size(1440, 900);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void navButton1_Click(object sender, EventArgs e)
        {
            ShowBanHang();
        }

        private void navButton2_Click(object sender, EventArgs e)
        {
            ShowPlaceholder("Chuc nang dang cap nhat.", navButton2);
        }

        private void navButton3_Click(object sender, EventArgs e)
        {
            ShowPlaceholder("Bao cao se hien thi tai day.", navButton3);
        }

        private void ShowBanHang()
        {
            SetActiveButton(navButton1);
            mainTitleLabel.Text = navButton1.Text;

            CloseActiveForm();

            var banHangForm = new Form_banHang
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            _activeForm = banHangForm;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(banHangForm);
            banHangForm.Show();
        }

        private void ShowPlaceholder(string message, Button sourceButton)
        {
            SetActiveButton(sourceButton);
            mainTitleLabel.Text = sourceButton.Text;

            CloseActiveForm();
            contentHostPanel.Controls.Clear();

            var placeholderPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var messageLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = message,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(52, 58, 64),
                Padding = new Padding(16),
                TextAlign = ContentAlignment.MiddleCenter
            };

            placeholderPanel.Controls.Add(messageLabel);
            contentHostPanel.Controls.Add(placeholderPanel);
        }

        private void SetActiveButton(Button button)
        {
            if (_activeButton != null)
            {
                _activeButton.BackColor = Color.FromArgb(52, 58, 64);
            }

            _activeButton = button;
            _activeButton.BackColor = Color.FromArgb(73, 80, 87);
        }

        private void CloseActiveForm()
        {
            if (_activeForm == null)
            {
                return;
            }

            if (contentHostPanel.Controls.Contains(_activeForm))
            {
                contentHostPanel.Controls.Remove(_activeForm);
            }

            _activeForm.Close();
            _activeForm.Dispose();
            _activeForm = null;
        }
    }
}
