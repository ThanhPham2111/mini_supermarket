using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D; // Thêm để hỗ trợ CustomButton
using mini_supermarket.GUI.Style; // Thêm namespace của CustomButton

namespace mini_supermarket.GUI.Form_BanHang
{
    partial class Form_banHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // ===== FORM =====
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1366, 768);
            MinimumSize = new Size(1280, 720);
            Name = "Form_banHang";
            Text = "Bán Hàng - Bách Hóa Xanh";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(245, 245, 245);
            DoubleBuffered = true;

            // ===== Header ===== (Removed - panel màu xanh lá đã được bỏ)
            // headerPanel đã được loại bỏ để đẩy giao diện lên cao hơn

            // ===== Main Layout =====
            mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Padding = new Padding(10);

            // ===== LEFT PANEL =====
            leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.Padding = new Padding(10);
            leftPanel.BackColor = Color.White;
            leftPanel.BorderStyle = BorderStyle.FixedSingle;

            // Search Panel
            searchPanel = new Panel();
            searchBox = new mini_supermarket.GUI.Style.SearchBoxControl();

            searchPanel.Dock = DockStyle.Top;
            searchPanel.Height = 55;
            searchPanel.Padding = new Padding(10, 10, 10, 10);
            searchPanel.BackColor = Color.White;

            searchBox.Location = new Point(10, 10);
            searchBox.Width = 350;
            searchBox.Height = 35;
            searchBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            searchPanel.Controls.Add(searchBox);

            // Product Header
            productHeaderPanel = new Panel();
            lblProductTitle = new Label();
            productHeaderPanel.Dock = DockStyle.Top;
            productHeaderPanel.Height = 40;
            productHeaderPanel.BackColor = Color.FromArgb(34, 139, 34);
            lblProductTitle.Dock = DockStyle.Fill;
            lblProductTitle.Text = "Danh sách sản phẩm";
            lblProductTitle.ForeColor = Color.White;
            lblProductTitle.Font = new Font("Roboto", 12F, FontStyle.Bold);
            lblProductTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblProductTitle.Padding = new Padding(10, 0, 0, 0);
            productHeaderPanel.Controls.Add(lblProductTitle);

            // Panel trung gian để chứa DataGridView (giới hạn chiều cao)
            // Không dùng Dock = Fill để tránh bị che bởi bottomLayout
            // Sẽ tính toán lại vị trí/kích thước trong event Layout
            Panel dgvContainerPanel = new Panel();
            dgvContainerPanel.Location = new Point(0, 0);
            dgvContainerPanel.Size = new Size(100, 100); // Sẽ được tính toán lại trong event Layout
            
            // DataGridView Products
            dgvProducts = new DataGridView();
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.RowTemplate.Height = 45;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(144, 238, 144);
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProducts.DefaultCellStyle.Font = new Font("Roboto", 8.5F);
            dgvProducts.DefaultCellStyle.Padding = new Padding(2);
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(34, 139, 34);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Roboto", 9F, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.Padding = new Padding(2);
            dgvProducts.ColumnHeadersHeight = 32;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProducts.GridColor = Color.FromArgb(230, 230, 230);
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

            productColumnMaSanPham = new DataGridViewTextBoxColumn();
            productColumnName = new DataGridViewTextBoxColumn();
            productColumnPrice = new DataGridViewTextBoxColumn();
            productColumnQuantity = new DataGridViewTextBoxColumn();
            productColumnHsd = new DataGridViewTextBoxColumn();
            productColumnPromotion = new DataGridViewTextBoxColumn();
            productColumnMaSanPham.HeaderText = "Mã SP";
            productColumnMaSanPham.FillWeight = 8;
            productColumnName.HeaderText = "Tên sản phẩm";
            productColumnName.FillWeight = 28;
            productColumnPrice.HeaderText = "Đơn giá";
            productColumnPrice.FillWeight = 14;
            productColumnQuantity.HeaderText = "SL";
            productColumnQuantity.FillWeight = 8;
            productColumnHsd.HeaderText = "HSD";
            productColumnHsd.FillWeight = 14;
            productColumnPromotion.HeaderText = "Khuyến mãi";
            productColumnPromotion.FillWeight = 28;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[]
            {
                productColumnMaSanPham, productColumnName, productColumnPrice, productColumnQuantity, productColumnHsd, productColumnPromotion
            });
            
            // Thêm dgvProducts vào container panel
            dgvContainerPanel.Controls.Add(dgvProducts);

            // Bottom Layout
            bottomLayout = new TableLayoutPanel();
            bottomLayout.Dock = DockStyle.Bottom;
            bottomLayout.Height = 210;
            bottomLayout.BringToFront(); // Đảm bảo bottomLayout luôn ở trên cùng
            bottomLayout.ColumnCount = 2;
            bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            bottomLayout.RowCount = 2;
            bottomLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            bottomLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            bottomLayout.Padding = new Padding(5);
            bottomLayout.BackColor = Color.White;

            productPreviewPanel = new Panel();
            productPreviewPanel.Dock = DockStyle.Fill;
            productPreviewPanel.BackColor = Color.FromArgb(248, 249, 250);
            productPreviewPanel.BorderStyle = BorderStyle.FixedSingle;
            productPreviewPanel.Padding = new Padding(10);
            productPreviewPanel.Margin = new Padding(5);

            // Product Image PictureBox
            picProductImage = new PictureBox();
            picProductImage.Dock = DockStyle.Fill;
            picProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            picProductImage.BackColor = Color.White;
            picProductImage.BorderStyle = BorderStyle.FixedSingle;
            productPreviewPanel.Controls.Add(picProductImage);

            // Product Actions
            productActionPanel = new FlowLayoutPanel();
            btnRefresh = new CustomButton();
            btnAddProduct = new CustomButton();
            productActionPanel.Dock = DockStyle.Fill;
            productActionPanel.FlowDirection = FlowDirection.LeftToRight;
            productActionPanel.Padding = new Padding(8, 14, 8, 14);
            productActionPanel.Margin = new Padding(5);
            productActionPanel.WrapContents = false;

            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Roboto", 9F, FontStyle.Bold);
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            ((CustomButton)btnRefresh).BorderRadius = 6;

            btnAddProduct.Text = "➕ Thêm vào giỏ";
            btnAddProduct.BackColor = Color.FromArgb(16, 137, 62);
            btnAddProduct.ForeColor = Color.White;
            btnAddProduct.FlatStyle = FlatStyle.Flat;
            btnAddProduct.Font = new Font("Roboto", 9F, FontStyle.Bold);
            btnAddProduct.Size = new Size(150, 40);
            btnAddProduct.FlatAppearance.MouseOverBackColor = Color.FromArgb(33, 136, 56);
            btnAddProduct.FlatAppearance.BorderSize = 0;
            btnAddProduct.Margin = new Padding(10, 0, 0, 0);
            btnAddProduct.Cursor = Cursors.Hand;
            btnAddProduct.Click += new EventHandler(btnAddProduct_Click);
            ((CustomButton)btnAddProduct).BorderRadius = 6;

            productActionPanel.Controls.Add(btnRefresh);
            productActionPanel.Controls.Add(btnAddProduct);

            // Product Detail
            productDetailLayout = new TableLayoutPanel();
            lblProductName = new Label();
            txtProductName = new TextBox();
            lblUnitPrice = new Label();
            txtUnitPrice = new TextBox();
            lblProductQuantity = new Label();
            txtQuantity = new TextBox();
            lblProductPromotion = new Label();
            txtPromotion = new TextBox();

            productDetailLayout.Dock = DockStyle.Fill;
            productDetailLayout.ColumnCount = 2;
            productDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            productDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            productDetailLayout.RowCount = 4;
            for (int i = 0; i < 4; i++)
                productDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            productDetailLayout.Padding = new Padding(10);
            productDetailLayout.Margin = new Padding(5);

            lblProductName.Text = "Tên SP:";
            lblUnitPrice.Text = "Đơn giá:";
            lblProductQuantity.Text = "Số lượng:";
            lblProductPromotion.Text = "Khuyến mãi:";

            foreach (Label l in new[] { lblProductName, lblUnitPrice, lblProductQuantity, lblProductPromotion })
            {
                l.Dock = DockStyle.Fill;
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.Font = new Font("Roboto", 8.5F, FontStyle.Bold);
                l.ForeColor = Color.FromArgb(52, 58, 64);
            }

            foreach (TextBox t in new[] { txtProductName, txtUnitPrice, txtPromotion })
            {
                t.Font = new Font("Roboto", 8.5F);
                t.Dock = DockStyle.Fill;
                t.BorderStyle = BorderStyle.FixedSingle;
                t.BackColor = Color.White;
                t.ReadOnly = true;
            }

            // txtQuantity is editable
            txtQuantity.Font = new Font("Roboto", 8.5F);
            txtQuantity.Dock = DockStyle.Fill;
            txtQuantity.BorderStyle = BorderStyle.FixedSingle;
            txtQuantity.BackColor = Color.White;
            txtQuantity.ReadOnly = false;

            productDetailLayout.Controls.Add(lblProductName, 0, 0);
            productDetailLayout.Controls.Add(txtProductName, 1, 0);
            productDetailLayout.Controls.Add(lblUnitPrice, 0, 1);
            productDetailLayout.Controls.Add(txtUnitPrice, 1, 1);
            productDetailLayout.Controls.Add(lblProductQuantity, 0, 2);
            productDetailLayout.Controls.Add(txtQuantity, 1, 2);
            productDetailLayout.Controls.Add(lblProductPromotion, 0, 3);
            productDetailLayout.Controls.Add(txtPromotion, 1, 3);

            bottomLayout.Controls.Add(productPreviewPanel, 0, 0);
            bottomLayout.SetRowSpan(productPreviewPanel, 2);
            bottomLayout.Controls.Add(productActionPanel, 1, 0);
            bottomLayout.Controls.Add(productDetailLayout, 1, 1);

            // Thứ tự add controls: add bottomLayout trước để nó chiếm phần bottom
            // Sau đó add dgvContainerPanel với Anchor để nó tự động điều chỉnh
            // Thứ tự add controls trong WinForms:
            // - Controls với Dock = Bottom được add trước sẽ chiếm phần bottom
            // - Controls với Anchor được add sau sẽ tự động điều chỉnh để không bị che
            leftPanel.Controls.Add(searchPanel);
            leftPanel.Controls.Add(productHeaderPanel);
            leftPanel.Controls.Add(bottomLayout);
            leftPanel.Controls.Add(dgvContainerPanel);
            
            // Đảm bảo bottomLayout luôn ở trên cùng về mặt z-order để không bị che
            bottomLayout.BringToFront();
            
            // Tính toán lại vị trí và kích thước của dgvContainerPanel sau khi add vào leftPanel
            // dgvContainerPanel sẽ nằm giữa productHeaderPanel và bottomLayout
            leftPanel.Layout += (sender, e) =>
            {
                if (dgvContainerPanel != null && bottomLayout != null && productHeaderPanel != null && searchPanel != null)
                {
                    int top = searchPanel.Bottom + productHeaderPanel.Height;
                    int bottom = bottomLayout.Top;
                    dgvContainerPanel.Location = new Point(0, top);
                    dgvContainerPanel.Height = bottom - top;
                    dgvContainerPanel.Width = leftPanel.ClientSize.Width;
                }
            };

            // ===== RIGHT PANEL =====
            rightPanel = new Panel();
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Padding = new Padding(15);
            rightPanel.BackColor = Color.White;
            rightPanel.BorderStyle = BorderStyle.FixedSingle;

            // Info Header
            infoHeaderPanel = new Panel();
            lblInfoTitle = new Label();
            infoHeaderPanel.Dock = DockStyle.Top;
            infoHeaderPanel.Height = 50;
            infoHeaderPanel.BackColor = Color.FromArgb(34, 139, 34);
            lblInfoTitle.Dock = DockStyle.Fill;
            lblInfoTitle.Text = "Thông tin đơn hàng";
            lblInfoTitle.ForeColor = Color.White;
            lblInfoTitle.Font = new Font("Roboto", 14F, FontStyle.Bold);
            lblInfoTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblInfoTitle.Padding = new Padding(10, 0, 0, 0);
            infoHeaderPanel.Controls.Add(lblInfoTitle);

            // Info Form
            infoFormLayout = new TableLayoutPanel();
            lblCustomer = new Label();
            txtCustomer = new TextBox();
            btnSelectCustomer = new CustomButton();
            lblAvailablePoints = new Label();
            txtAvailablePoints = new TextBox();
            lblEarnedPoints = new Label();
            txtEarnedPoints = new TextBox();
            lblUsePoints = new Label();
            txtUsePoints = new TextBox();

            infoFormLayout.Dock = DockStyle.Top;
            infoFormLayout.Height = 165;
            infoFormLayout.ColumnCount = 3;
            infoFormLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            infoFormLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            infoFormLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            infoFormLayout.RowCount = 4;
            for (int i = 0; i < 4; i++)
                infoFormLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            infoFormLayout.Padding = new Padding(15, 10, 15, 10);

            lblCustomer.Text = "Khách hàng:";
            lblAvailablePoints.Text = "Điểm hiện có:";
            lblEarnedPoints.Text = "Điểm tích lũy:";
            lblUsePoints.Text = "Dùng điểm:";

            foreach (Label l in new[] { lblCustomer, lblAvailablePoints, lblEarnedPoints, lblUsePoints })
            {
                l.Dock = DockStyle.Fill;
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.Font = new Font("Roboto", 8.5F, FontStyle.Bold);
                l.ForeColor = Color.FromArgb(52, 58, 64);
            }

            foreach (TextBox t in new[] { txtCustomer, txtAvailablePoints, txtEarnedPoints, txtUsePoints })
            {
                t.Font = new Font("Roboto", 8.5F);
                t.Dock = DockStyle.Fill;
                t.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                t.BorderStyle = BorderStyle.FixedSingle;
            }

            txtAvailablePoints.ReadOnly = true;
            txtAvailablePoints.BackColor = Color.FromArgb(248, 249, 250);
            txtEarnedPoints.ReadOnly = true;
            txtEarnedPoints.BackColor = Color.FromArgb(248, 249, 250);
            
            // Event handler cho txtUsePoints để validate điểm
            txtUsePoints.TextChanged += new EventHandler(txtUsePoints_TextChanged);

            btnSelectCustomer.Text = "Chọn KH";
            btnSelectCustomer.BackColor = Color.FromArgb(0, 123, 255);
            btnSelectCustomer.ForeColor = Color.White;
            btnSelectCustomer.FlatStyle = FlatStyle.Flat;
            btnSelectCustomer.Font = new Font("Roboto", 8.5F, FontStyle.Bold);
            btnSelectCustomer.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 217);
            btnSelectCustomer.FlatAppearance.BorderSize = 0;
            btnSelectCustomer.Dock = DockStyle.Fill;
            btnSelectCustomer.Margin = new Padding(5, 5, 0, 5);
            btnSelectCustomer.Cursor = Cursors.Hand;
            btnSelectCustomer.Click += new EventHandler(btnSelectCustomer_Click);
            ((CustomButton)btnSelectCustomer).BorderRadius = 5;

            infoFormLayout.Controls.Add(lblCustomer, 0, 0);
            infoFormLayout.Controls.Add(txtCustomer, 1, 0);
            infoFormLayout.Controls.Add(btnSelectCustomer, 2, 0);
            infoFormLayout.Controls.Add(lblAvailablePoints, 0, 1);
            infoFormLayout.Controls.Add(txtAvailablePoints, 1, 1);
            infoFormLayout.Controls.Add(lblEarnedPoints, 0, 2);
            infoFormLayout.Controls.Add(txtEarnedPoints, 1, 2);
            infoFormLayout.Controls.Add(lblUsePoints, 0, 3);
            infoFormLayout.Controls.Add(txtUsePoints, 1, 3);

            // Order Grid
            dgvOrder = new DataGridView();
            dgvOrder.Dock = DockStyle.Fill;
            dgvOrder.BackgroundColor = Color.White;
            dgvOrder.BorderStyle = BorderStyle.None;
            dgvOrder.RowHeadersVisible = false;
            dgvOrder.RowTemplate.Height = 45;
            dgvOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrder.MultiSelect = false;
            dgvOrder.AllowUserToAddRows = false;
            dgvOrder.AllowUserToDeleteRows = false;
            dgvOrder.AllowUserToResizeRows = false;
            dgvOrder.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvOrder.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 193, 7);
            dgvOrder.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvOrder.DefaultCellStyle.Font = new Font("Roboto", 8.5F);
            dgvOrder.DefaultCellStyle.Padding = new Padding(2);
            dgvOrder.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(34, 139, 34);
            dgvOrder.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrder.ColumnHeadersDefaultCellStyle.Font = new Font("Roboto", 9F, FontStyle.Bold);
            dgvOrder.ColumnHeadersDefaultCellStyle.Padding = new Padding(2);
            dgvOrder.ColumnHeadersHeight = 32;
            dgvOrder.EnableHeadersVisualStyles = false;
            dgvOrder.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvOrder.GridColor = Color.FromArgb(230, 230, 230);

            orderColumnName = new DataGridViewTextBoxColumn();
            orderColumnPrice = new DataGridViewTextBoxColumn();
            orderColumnQuantity = new DataGridViewTextBoxColumn();
            orderColumnPromotion = new DataGridViewTextBoxColumn();
            orderColumnName.HeaderText = "Tên sản phẩm";
            orderColumnName.FillWeight = 35;
            orderColumnPrice.HeaderText = "Đơn giá";
            orderColumnPrice.FillWeight = 20;
            orderColumnQuantity.HeaderText = "SL";
            orderColumnQuantity.FillWeight = 12;
            orderColumnPromotion.HeaderText = "Khuyến mãi";
            orderColumnPromotion.FillWeight = 33;
            dgvOrder.Columns.AddRange(new DataGridViewColumn[]
            {
                orderColumnName, orderColumnPrice, orderColumnQuantity, orderColumnPromotion
            });

            // Total Panel
            totalPanel = new TableLayoutPanel();
            lblTotal = new Label();
            txtTotal = new TextBox();
            btnRemove = new CustomButton();
            totalPanel.Dock = DockStyle.Bottom;
            totalPanel.Height = 70;
            totalPanel.ColumnCount = 3;
            totalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            totalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            totalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            totalPanel.Padding = new Padding(15, 10, 15, 10);
            totalPanel.BackColor = Color.FromArgb(248, 249, 250);

            lblTotal.Text = "TỔNG TIỀN:";
            lblTotal.Font = new Font("Roboto", 10F, FontStyle.Bold);
            lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            lblTotal.Dock = DockStyle.Fill;
            lblTotal.ForeColor = Color.FromArgb(220, 53, 69);

            txtTotal.Font = new Font("Roboto", 10F, FontStyle.Bold);
            txtTotal.Dock = DockStyle.Fill;
            txtTotal.TextAlign = HorizontalAlignment.Right;
            txtTotal.BorderStyle = BorderStyle.FixedSingle;
            txtTotal.ReadOnly = true;
            txtTotal.BackColor = Color.White;
            txtTotal.ForeColor = Color.FromArgb(220, 53, 69);
            txtTotal.Margin = new Padding(5, 10, 5, 10);

            btnRemove.Text = "🗑 Xóa";
            btnRemove.BackColor = Color.FromArgb(220, 53, 69);
            btnRemove.ForeColor = Color.White;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("Roboto", 9F, FontStyle.Bold);
            btnRemove.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 35, 51);
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Dock = DockStyle.Fill;
            btnRemove.Margin = new Padding(5, 10, 0, 10);
            btnRemove.Cursor = Cursors.Hand;
            btnRemove.Click += new EventHandler(btnRemove_Click);
            ((CustomButton)btnRemove).BorderRadius = 6;

            totalPanel.Controls.Add(lblTotal, 0, 0);
            totalPanel.Controls.Add(txtTotal, 1, 0);
            totalPanel.Controls.Add(btnRemove, 2, 0);

            // Action Panel
            actionPanel = new FlowLayoutPanel();
            btnCheckout = new CustomButton();
            btnCancel = new CustomButton();
            actionPanel.Dock = DockStyle.Bottom;
            actionPanel.Height = 65;
            actionPanel.FlowDirection = FlowDirection.RightToLeft;
            actionPanel.Padding = new Padding(12, 12, 12, 12);
            actionPanel.BackColor = Color.White;

            btnCheckout.Text = "💳 Thanh toán";
            btnCheckout.BackColor = Color.FromArgb(40, 167, 69);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Roboto", 9F, FontStyle.Bold);
            btnCheckout.Size = new Size(130, 40);
            btnCheckout.FlatAppearance.MouseOverBackColor = Color.FromArgb(33, 136, 56);
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.Margin = new Padding(0);
            btnCheckout.Cursor = Cursors.Hand;
            btnCheckout.Click += new EventHandler(btnCheckout_Click);
            ((CustomButton)btnCheckout).BorderRadius = 6;

            btnCancel.Text = "✖ Hủy đơn";
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Roboto", 9F, FontStyle.Bold);
            btnCancel.Size = new Size(110, 40);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Margin = new Padding(0, 0, 10, 0);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += new EventHandler(btnCancel_Click);
            ((CustomButton)btnCancel).BorderRadius = 6;

            actionPanel.Controls.Add(btnCheckout);
            actionPanel.Controls.Add(btnCancel);

            rightPanel.Controls.Add(dgvOrder);
            rightPanel.Controls.Add(infoFormLayout);
            rightPanel.Controls.Add(infoHeaderPanel);
            rightPanel.Controls.Add(totalPanel);
            rightPanel.Controls.Add(actionPanel);

            // ===== Add Panels =====
            mainLayout.Controls.Add(leftPanel, 0, 0);
            mainLayout.Controls.Add(rightPanel, 1, 0);

            Controls.Add(mainLayout);
            // headerPanel đã được loại bỏ

            // Thêm tooltip
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnAddProduct, "Thêm sản phẩm vào đơn hàng");
            toolTip.SetToolTip(btnRefresh, "Làm mới danh sách sản phẩm");
            toolTip.SetToolTip(btnCheckout, "Hoàn tất thanh toán");
            toolTip.SetToolTip(btnCancel, "Hủy đơn hàng hiện tại");
            toolTip.SetToolTip(btnRemove, "Xóa sản phẩm khỏi đơn hàng");
            toolTip.SetToolTip(btnSelectCustomer, "Chọn thông tin khách hàng");

            // Khởi tạo ErrorProvider cho txtUsePoints
            errorProviderUsePoints = new ErrorProvider();
            errorProviderUsePoints.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProviderUsePoints.SetIconAlignment(txtUsePoints, ErrorIconAlignment.MiddleRight);
            errorProviderUsePoints.SetIconPadding(txtUsePoints, -20);

            ResumeLayout(false);
        }

        // ===== FIELDS =====
        // Header panel đã được loại bỏ
        // private Panel headerPanel;
        // private Label lblRole, lblGreeting;
        private TableLayoutPanel mainLayout;
        private Panel leftPanel, rightPanel;
        private Panel searchPanel, productHeaderPanel;
        private Label lblProductTitle;
        private mini_supermarket.GUI.Style.SearchBoxControl searchBox;
        private DataGridView dgvProducts;
        private DataGridViewTextBoxColumn productColumnMaSanPham, productColumnName, productColumnPrice, productColumnQuantity, productColumnHsd, productColumnPromotion;
        private TableLayoutPanel bottomLayout, productDetailLayout;
        private Panel productPreviewPanel;
        private FlowLayoutPanel productActionPanel;
        private CustomButton btnRefresh, btnAddProduct;
        private Label lblProductName, lblUnitPrice, lblProductQuantity, lblProductPromotion;
        private TextBox txtProductName, txtUnitPrice, txtQuantity, txtPromotion;
        private Panel infoHeaderPanel;
        private Label lblInfoTitle;
        private TableLayoutPanel infoFormLayout, totalPanel;
        private Label lblCustomer, lblAvailablePoints, lblEarnedPoints, lblUsePoints, lblTotal;
        private TextBox txtCustomer, txtAvailablePoints, txtEarnedPoints, txtUsePoints, txtTotal;
        private CustomButton btnSelectCustomer, btnRemove;
        private DataGridView dgvOrder;
        private DataGridViewTextBoxColumn orderColumnName, orderColumnPrice, orderColumnQuantity, orderColumnPromotion;
        private FlowLayoutPanel actionPanel;
        private CustomButton btnCancel, btnCheckout;
        private PictureBox picProductImage;
        private int? selectedProductId = null;
        private int? selectedProductStock = null;
        private int? selectedKhachHangId = null; // Lưu mã khách hàng đã chọn
        private int? availablePoints = null; // Lưu điểm hiện có của khách hàng
        private ErrorProvider? errorProviderUsePoints; // ErrorProvider cho txtUsePoints
    }
}