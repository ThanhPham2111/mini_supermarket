namespace mini_supermarket.GUI.Form_LoaiSanPham
{
    partial class Form_DonVi
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
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.buttonsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.listHeaderLabel = new System.Windows.Forms.Label();
            this.donViDataGridView = new System.Windows.Forms.DataGridView();
            this.searchContainerPanel.SuspendLayout();
            this.buttonsFlowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.donViDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // searchContainerPanel
            // 
            this.searchContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
            this.searchContainerPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchContainerPanel.Controls.Add(this.searchButton);
            this.searchContainerPanel.Controls.Add(this.searchTextBox);
            this.searchContainerPanel.Location = new System.Drawing.Point(20, 20);
            this.searchContainerPanel.Name = "searchContainerPanel";
            this.searchContainerPanel.Padding = new System.Windows.Forms.Padding(8);
            this.searchContainerPanel.Size = new System.Drawing.Size(1052, 60);
            this.searchContainerPanel.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(12, 28);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(120, 30);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Tìm kiếm";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.searchButton.ForeColor = System.Drawing.Color.White;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(150, 28);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.PlaceholderText = "Tìm kiếm theo mã, đơn vị";
            this.searchTextBox.Size = new System.Drawing.Size(744, 23);
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
            this.addButton.Location = new System.Drawing.Point(8, 8);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(80, 30);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Thêm";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Margin = new System.Windows.Forms.Padding(8);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(104, 8);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(80, 30);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Sửa";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.editButton.ForeColor = System.Drawing.Color.White;
            this.editButton.Margin = new System.Windows.Forms.Padding(8);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(200, 8);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(80, 30);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Xóa";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(255, 77, 77);
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Margin = new System.Windows.Forms.Padding(8);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(296, 8);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(80, 30);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Làm mới";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(8);
            // 
            // listHeaderLabel
            // 
            this.listHeaderLabel.AutoSize = true;
            this.listHeaderLabel.Location = new System.Drawing.Point(20, 140);
            this.listHeaderLabel.Name = "listHeaderLabel";
            this.listHeaderLabel.Size = new System.Drawing.Size(87, 15);
            this.listHeaderLabel.TabIndex = 2;
            this.listHeaderLabel.Text = "Danh sách Đơn vị";
            // 
            // donViDataGridView
            // 
            this.donViDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
            this.donViDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.donViDataGridView.Location = new System.Drawing.Point(20, 160);
            this.donViDataGridView.Name = "donViDataGridView";
            this.donViDataGridView.RowTemplate.Height = 25;
            this.donViDataGridView.Size = new System.Drawing.Size(1052, 500);
            this.donViDataGridView.TabIndex = 3;
            this.donViDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.donViDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // Form_DonVi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.donViDataGridView);
            this.Controls.Add(this.listHeaderLabel);
            this.Controls.Add(this.buttonsFlowPanel);
            this.Controls.Add(this.searchContainerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_DonVi";
            this.Text = "Quản lý Đơn vị";
            this.searchContainerPanel.ResumeLayout(false);
            this.searchContainerPanel.PerformLayout();
            this.buttonsFlowPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.donViDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel searchContainerPanel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.FlowLayoutPanel buttonsFlowPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label listHeaderLabel;
        private System.Windows.Forms.DataGridView donViDataGridView;
    }
}


