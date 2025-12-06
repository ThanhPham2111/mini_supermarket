using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.QuanLy.PhanQuyen
{
    public partial class RoleSelectionDialog : Form
    {
        private ComboBox? _roleComboBox;
        public PhanQuyenDTO? SelectedRole { get; private set; }

        public RoleSelectionDialog(IList<PhanQuyenDTO> roles)
        {
            InitializeComponent(roles);
        }

        private void InitializeComponent(IList<PhanQuyenDTO> roles)
        {
            this.Text = "Chọn Role Mới";
            this.Size = new System.Drawing.Size(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            var label = new Label
            {
                Text = "Chọn role mới để chuyển tất cả tài khoản:",
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(350, 30),
                AutoSize = false
            };
            this.Controls.Add(label);

            _roleComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(20, 50),
                Size = new System.Drawing.Size(340, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            // Chỉ set DataSource nếu có roles
            if (roles != null && roles.Count > 0)
            {
                _roleComboBox.DataSource = roles;
                _roleComboBox.DisplayMember = "TenQuyen";
                _roleComboBox.ValueMember = "MaQuyen";
                // SelectedIndex sẽ tự động được set về 0 khi set DataSource
            }
            else
            {
                // Nếu không có roles, disable comboBox
                _roleComboBox.Enabled = false;
            }
            
            this.Controls.Add(_roleComboBox);

            var btnOK = new Button
            {
                Text = "Xác nhận",
                Location = new System.Drawing.Point(200, 100),
                Size = new System.Drawing.Size(80, 30),
                DialogResult = DialogResult.OK
            };
            btnOK.Click += (s, e) =>
            {
                if (_roleComboBox == null || _roleComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn một role.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                if (_roleComboBox.SelectedItem is PhanQuyenDTO selected)
                {
                    SelectedRole = selected;
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một role hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                }
            };
            this.Controls.Add(btnOK);

            var btnCancel = new Button
            {
                Text = "Hủy",
                Location = new System.Drawing.Point(285, 100),
                Size = new System.Drawing.Size(80, 30),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
    }
}

