using System.Windows.Forms;

namespace mini_supermarket.GUI.Form_LoaiSanPham
{
    partial class Form_LoaiSanPham
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabLoai = new System.Windows.Forms.TabPage();
            this.loaiDataGridView = new System.Windows.Forms.DataGridView();
            this.listHeaderLabel = new System.Windows.Forms.Label();
            this.buttonsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addLoaiButton = new System.Windows.Forms.Button();
            this.editLoaiButton = new System.Windows.Forms.Button();
            this.deleteLoaiButton = new System.Windows.Forms.Button();
            this.refreshLoaiButton = new System.Windows.Forms.Button();
            this.searchContainerPanel = new System.Windows.Forms.Panel();
            this.loaiStatusFilterComboBox = new System.Windows.Forms.ComboBox();
            this.loaiStatusFilterLabel = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.tabThuongHieu = new System.Windows.Forms.TabPage();
            this.tabDonVi = new System.Windows.Forms.TabPage();
            this.mainTabControl.SuspendLayout();
            this.tabLoai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loaiDataGridView)).BeginInit();
            this.buttonsFlowPanel.SuspendLayout();
            this.searchContainerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tabLoai);
            this.mainTabControl.Controls.Add(this.tabThuongHieu);
            this.mainTabControl.Controls.Add(this.tabDonVi);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1100, 700);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            // 
            // tabLoai
            // 
            this.tabLoai.Controls.Add(this.loaiDataGridView);
            this.tabLoai.Controls.Add(this.listHeaderLabel);
            this.tabLoai.Controls.Add(this.buttonsFlowPanel);
            this.tabLoai.Controls.Add(this.searchContainerPanel);
            this.tabLoai.Location = new System.Drawing.Point(4, 26);
            this.tabLoai.Name = "tabLoai";
            this.tabLoai.Padding = new System.Windows.Forms.Padding(12);
            this.tabLoai.Size = new System.Drawing.Size(1092, 670);
            this.tabLoai.TabIndex = 0;
            this.tabLoai.Text = "Loại";
            this.tabLoai.UseVisualStyleBackColor = false;
            this.tabLoai.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            // 
            // loaiDataGridView
            // 
            this.loaiDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loaiDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.loaiDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loaiDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.loaiDataGridView.Location = new System.Drawing.Point(20, 200);
            this.loaiDataGridView.MultiSelect = false;
            this.loaiDataGridView.Name = "loaiDataGridView";
            this.loaiDataGridView.RowTemplate.Height = 25;
            this.loaiDataGridView.Size = new System.Drawing.Size(1052, 440);
            this.loaiDataGridView.TabIndex = 3;
            // 
            // listHeaderLabel
            // 
            this.listHeaderLabel.AutoSize = true;
            this.listHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.listHeaderLabel.Location = new System.Drawing.Point(20, 170);
            this.listHeaderLabel.Name = "listHeaderLabel";
            this.listHeaderLabel.Size = new System.Drawing.Size(106, 19);
            this.listHeaderLabel.TabIndex = 2;
            this.listHeaderLabel.Text = "Danh sách loại";
            // 
            // buttonsFlowPanel
            // 
            this.buttonsFlowPanel.AutoSize = true;
            this.buttonsFlowPanel.Controls.Add(this.addLoaiButton);
            this.buttonsFlowPanel.Controls.Add(this.editLoaiButton);
            this.buttonsFlowPanel.Controls.Add(this.deleteLoaiButton);
            this.buttonsFlowPanel.Controls.Add(this.refreshLoaiButton);
            this.buttonsFlowPanel.Location = new System.Drawing.Point(20, 120);
            this.buttonsFlowPanel.Name = "buttonsFlowPanel";
            this.buttonsFlowPanel.Padding = new System.Windows.Forms.Padding(8);
            this.buttonsFlowPanel.Size = new System.Drawing.Size(420, 46);
            this.buttonsFlowPanel.TabIndex = 1;
            // 
            // addLoaiButton
            // 
            this.addLoaiButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.addLoaiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addLoaiButton.ForeColor = System.Drawing.Color.White;
            this.addLoaiButton.Location = new System.Drawing.Point(8, 8);
            this.addLoaiButton.Margin = new System.Windows.Forms.Padding(8);
            this.addLoaiButton.Name = "addLoaiButton";
            this.addLoaiButton.Size = new System.Drawing.Size(80, 30);
            this.addLoaiButton.TabIndex = 0;
            this.addLoaiButton.Text = "Thêm";
            this.addLoaiButton.UseVisualStyleBackColor = false;
            this.addLoaiButton.Click += new System.EventHandler(this.addLoaiButton_Click);
            // 
            // editLoaiButton
            // 
            this.editLoaiButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.editLoaiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editLoaiButton.ForeColor = System.Drawing.Color.White;
            this.editLoaiButton.Location = new System.Drawing.Point(104, 8);
            this.editLoaiButton.Margin = new System.Windows.Forms.Padding(8);
            this.editLoaiButton.Name = "editLoaiButton";
            this.editLoaiButton.Size = new System.Drawing.Size(80, 30);
            this.editLoaiButton.TabIndex = 1;
            this.editLoaiButton.Text = "Sửa";
            this.editLoaiButton.UseVisualStyleBackColor = false;
            this.editLoaiButton.Click += new System.EventHandler(this.editLoaiButton_Click);
            // 
            // deleteLoaiButton
            // 
            this.deleteLoaiButton.BackColor = System.Drawing.Color.FromArgb(255, 77, 77);
            this.deleteLoaiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteLoaiButton.ForeColor = System.Drawing.Color.White;
            this.deleteLoaiButton.Location = new System.Drawing.Point(200, 8);
            this.deleteLoaiButton.Margin = new System.Windows.Forms.Padding(8);
            this.deleteLoaiButton.Name = "deleteLoaiButton";
            this.deleteLoaiButton.Size = new System.Drawing.Size(80, 30);
            this.deleteLoaiButton.TabIndex = 2;
            this.deleteLoaiButton.Text = "Xóa";
            this.deleteLoaiButton.UseVisualStyleBackColor = false;
            this.deleteLoaiButton.Click += new System.EventHandler(this.deleteLoaiButton_Click);
            // 
            // refreshLoaiButton
            // 
            this.refreshLoaiButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.refreshLoaiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshLoaiButton.ForeColor = System.Drawing.Color.White;
            this.refreshLoaiButton.Location = new System.Drawing.Point(296, 8);
            this.refreshLoaiButton.Name = "refreshLoaiButton";
            this.refreshLoaiButton.Size = new System.Drawing.Size(80, 30);
            this.refreshLoaiButton.TabIndex = 3;
            this.refreshLoaiButton.Text = "Làm mới";
            this.refreshLoaiButton.UseVisualStyleBackColor = false;
            this.refreshLoaiButton.Margin = new System.Windows.Forms.Padding(8);
            this.refreshLoaiButton.Click += new System.EventHandler(this.refreshLoaiButton_Click);
            // 
            // searchContainerPanel
            // 
            this.searchContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchContainerPanel.Controls.Add(this.loaiStatusFilterComboBox);
            this.searchContainerPanel.Controls.Add(this.loaiStatusFilterLabel);
            this.searchContainerPanel.Controls.Add(this.searchButton);
            this.searchContainerPanel.Controls.Add(this.searchTextBox);
            this.searchContainerPanel.Location = new System.Drawing.Point(20, 20);
            this.searchContainerPanel.Name = "searchContainerPanel";
            this.searchContainerPanel.Size = new System.Drawing.Size(1052, 80);
            this.searchContainerPanel.TabIndex = 0;
            // 
            // loaiStatusFilterComboBox
            // 
            this.loaiStatusFilterComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loaiStatusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loaiStatusFilterComboBox.Location = new System.Drawing.Point(760, 38);
            this.loaiStatusFilterComboBox.Name = "loaiStatusFilterComboBox";
            this.loaiStatusFilterComboBox.Size = new System.Drawing.Size(260, 25);
            this.loaiStatusFilterComboBox.TabIndex = 3;
            // 
            // loaiStatusFilterLabel
            // 
            this.loaiStatusFilterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loaiStatusFilterLabel.AutoSize = true;
            this.loaiStatusFilterLabel.Location = new System.Drawing.Point(760, 15);
            this.loaiStatusFilterLabel.Name = "loaiStatusFilterLabel";
            this.loaiStatusFilterLabel.Size = new System.Drawing.Size(95, 19);
            this.loaiStatusFilterLabel.TabIndex = 2;
            this.loaiStatusFilterLabel.Text = "Trạng thái lọc";
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(0, 34);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(120, 30);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Tìm kiếm";
            this.searchButton.UseVisualStyleBackColor = false;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(140, 36);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.PlaceholderText = "Tìm kiếm theo mã, loại";
            this.searchTextBox.Size = new System.Drawing.Size(480, 25);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // tabThuongHieu
            // 
            this.tabThuongHieu.Location = new System.Drawing.Point(4, 26);
            this.tabThuongHieu.Name = "tabThuongHieu";
            this.tabThuongHieu.Padding = new System.Windows.Forms.Padding(12);
            this.tabThuongHieu.Size = new System.Drawing.Size(1092, 670);
            this.tabThuongHieu.TabIndex = 1;
            this.tabThuongHieu.Text = "Thương hiệu";
            this.tabThuongHieu.UseVisualStyleBackColor = true;
            // 
            // tabDonVi
            // 
            this.tabDonVi.Location = new System.Drawing.Point(4, 26);
            this.tabDonVi.Name = "tabDonVi";
            this.tabDonVi.Padding = new System.Windows.Forms.Padding(12);
            this.tabDonVi.Size = new System.Drawing.Size(1092, 670);
            this.tabDonVi.TabIndex = 2;
            this.tabDonVi.Text = "Đơn vị";
            this.tabDonVi.UseVisualStyleBackColor = true;
            // 
            // Form_LoaiSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.mainTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_LoaiSanPham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Loại Sản phẩm";
            this.mainTabControl.ResumeLayout(false);
            this.tabLoai.ResumeLayout(false);
            this.tabLoai.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loaiDataGridView)).EndInit();
            this.buttonsFlowPanel.ResumeLayout(false);
            this.searchContainerPanel.ResumeLayout(false);
            this.searchContainerPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabLoai;
        private System.Windows.Forms.TabPage tabThuongHieu;
        private System.Windows.Forms.TabPage tabDonVi;
        private System.Windows.Forms.Panel searchContainerPanel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.ComboBox loaiStatusFilterComboBox;
        private System.Windows.Forms.Label loaiStatusFilterLabel;
        private System.Windows.Forms.FlowLayoutPanel buttonsFlowPanel;
        private System.Windows.Forms.Button addLoaiButton;
        private System.Windows.Forms.Button editLoaiButton;
        private System.Windows.Forms.Button deleteLoaiButton;
        private System.Windows.Forms.Button refreshLoaiButton;
        private System.Windows.Forms.Label listHeaderLabel;
        private System.Windows.Forms.DataGridView loaiDataGridView;
    }
}
