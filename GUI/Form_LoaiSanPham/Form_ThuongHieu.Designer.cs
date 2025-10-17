namespace mini_supermarket.GUI.Form_LoaiSanPham
{
    partial class Form_ThuongHieu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.searchContainerPanel = new System.Windows.Forms.Panel();
            this.statusFilterComboBox = new System.Windows.Forms.ComboBox();
            this.statusFilterLabel = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.buttonsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.listHeaderLabel = new System.Windows.Forms.Label();
            this.thuongHieuDataGridView = new System.Windows.Forms.DataGridView();
            this.searchContainerPanel.SuspendLayout();
            this.buttonsFlowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thuongHieuDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // searchContainerPanel
            // 
            this.searchContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
            this.searchContainerPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchContainerPanel.Controls.Add(this.statusFilterComboBox);
            this.searchContainerPanel.Controls.Add(this.statusFilterLabel);
            this.searchContainerPanel.Controls.Add(this.searchButton);
            this.searchContainerPanel.Controls.Add(this.searchTextBox);
            this.searchContainerPanel.Location = new System.Drawing.Point(20, 20);
            this.searchContainerPanel.Name = "searchContainerPanel";
            this.searchContainerPanel.Padding = new System.Windows.Forms.Padding(8);
            this.searchContainerPanel.Size = new System.Drawing.Size(1052, 60);
            this.searchContainerPanel.TabIndex = 0;
            // 
            // statusFilterComboBox
            // 
            this.statusFilterComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusFilterComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.statusFilterComboBox.Location = new System.Drawing.Point(700, 28);
            this.statusFilterComboBox.Name = "statusFilterComboBox";
            this.statusFilterComboBox.Size = new System.Drawing.Size(200, 25);
            this.statusFilterComboBox.TabIndex = 3;
            // 
            // statusFilterLabel
            // 
            this.statusFilterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusFilterLabel.AutoSize = true;
            this.statusFilterLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusFilterLabel.Location = new System.Drawing.Point(700, 8);
            this.statusFilterLabel.Name = "statusFilterLabel";
            this.statusFilterLabel.Size = new System.Drawing.Size(86, 15);
            this.statusFilterLabel.TabIndex = 2;
            this.statusFilterLabel.Text = "Trạng thái lọc";
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(12, 28);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(120, 30);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Tìm kiếm";
            this.searchButton.UseVisualStyleBackColor = false;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.searchTextBox.Location = new System.Drawing.Point(150, 28);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.PlaceholderText = "Tìm kiếm theo mã, thương hiệu";
            this.searchTextBox.Size = new System.Drawing.Size(430, 25);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // buttonsFlowPanel
            // 
            this.buttonsFlowPanel.AutoSize = true;
            this.buttonsFlowPanel.Controls.Add(this.addButton);
            this.buttonsFlowPanel.Controls.Add(this.editButton);
            this.buttonsFlowPanel.Controls.Add(this.deleteButton);
            this.buttonsFlowPanel.Controls.Add(this.refreshButton);
            this.buttonsFlowPanel.Location = new System.Drawing.Point(20, 90);
            this.buttonsFlowPanel.Name = "buttonsFlowPanel";
            this.buttonsFlowPanel.Padding = new System.Windows.Forms.Padding(8);
            this.buttonsFlowPanel.Size = new System.Drawing.Size(420, 46);
            this.buttonsFlowPanel.TabIndex = 1;
            this.buttonsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(8, 8);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(80, 30);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Thêm";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Margin = new System.Windows.Forms.Padding(8);
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.ForeColor = System.Drawing.Color.White;
            this.editButton.Location = new System.Drawing.Point(104, 8);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(80, 30);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Sửa";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Margin = new System.Windows.Forms.Padding(8);
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(255, 77, 77);
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(200, 8);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(80, 30);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Ngưng";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Margin = new System.Windows.Forms.Padding(8);
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.Location = new System.Drawing.Point(296, 8);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(80, 30);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Làm mới";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Margin = new System.Windows.Forms.Padding(8);
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // listHeaderLabel
            // 
            this.listHeaderLabel.AutoSize = true;
            this.listHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.listHeaderLabel.Location = new System.Drawing.Point(20, 140);
            this.listHeaderLabel.Name = "listHeaderLabel";
            this.listHeaderLabel.Size = new System.Drawing.Size(136, 19);
            this.listHeaderLabel.TabIndex = 2;
            this.listHeaderLabel.Text = "Danh sách thương hiệu";
            // 
            // thuongHieuDataGridView
            // 
            this.thuongHieuDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
            this.thuongHieuDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.thuongHieuDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.thuongHieuDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.thuongHieuDataGridView.Location = new System.Drawing.Point(20, 160);
            this.thuongHieuDataGridView.Name = "thuongHieuDataGridView";
            this.thuongHieuDataGridView.RowTemplate.Height = 25;
            this.thuongHieuDataGridView.Size = new System.Drawing.Size(1052, 500);
            this.thuongHieuDataGridView.TabIndex = 3;
            // 
            // Form_ThuongHieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.thuongHieuDataGridView);
            this.Controls.Add(this.listHeaderLabel);
            this.Controls.Add(this.buttonsFlowPanel);
            this.Controls.Add(this.searchContainerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_ThuongHieu";
            this.Text = "Quản lý Thương hiệu";
            this.searchContainerPanel.ResumeLayout(false);
            this.searchContainerPanel.PerformLayout();
            this.buttonsFlowPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.thuongHieuDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel searchContainerPanel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.ComboBox statusFilterComboBox;
        private System.Windows.Forms.Label statusFilterLabel;
        private System.Windows.Forms.FlowLayoutPanel buttonsFlowPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label listHeaderLabel;
        private System.Windows.Forms.DataGridView thuongHieuDataGridView;
    }
}