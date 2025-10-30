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

            // ===== Header =====
            headerPanel = new Panel();
            lblRole = new Label();
            lblGreeting = new Label();
            PictureBox logo = new PictureBox();

            headerPanel.BackColor = Color.FromArgb(34, 139, 34);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 50;
            headerPanel.Padding = new Padding(15, 5, 15, 5);

            // logo.Image = Properties.Resources.BachHoaXanhLogo; // Bỏ comment và đảm bảo file logo tồn tại
            logo.Size = new Size(120, 40);
            logo.SizeMode = PictureBoxSizeMode.StretchImage;
            logo.Location = new Point(15, 5);

            lblGreeting.Font = new Font("Roboto", 12F, FontStyle.Regular);
            lblGreeting.ForeColor = Color.White;
            lblGreeting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblGreeting.Text = "Xin chào,";
            lblGreeting.AutoSize = true;
            lblGreeting.Location = new Point(1200, 10);

            lblRole.Font = new Font("Roboto", 10F);
            lblRole.ForeColor = Color.White;
            lblRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRole.AutoSize = true;
            lblRole.Location = new Point(1200, 30);
            lblRole.Text = "(Nhân viên bán hàng)";

            headerPanel.Controls.Add(logo);
            headerPanel.Controls.Add(lblRole);
            headerPanel.Controls.Add(lblGreeting);

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
            leftPanel.Padding = new Padding(15);
            leftPanel.BackColor = Color.White;
            leftPanel.BorderStyle = BorderStyle.FixedSingle;

            // Sales Header
            salesHeaderPanel = new Panel();
            lblSalesTitle = new Label();
            salesHeaderPanel.Dock = DockStyle.Top;
            salesHeaderPanel.Height = 50;
            salesHeaderPanel.BackColor = Color.FromArgb(34, 139, 34);
            lblSalesTitle.Dock = DockStyle.Fill;
            lblSalesTitle.Text = "Bán Hàng";
            lblSalesTitle.ForeColor = Color.White;
            lblSalesTitle.Font = new Font("Roboto", 16F, FontStyle.Bold);
            lblSalesTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblSalesTitle.Padding = new Padding(10, 0, 0, 0);
            salesHeaderPanel.Controls.Add(lblSalesTitle);

            // Search
            searchPanel = new Panel();
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Height = 80;
            searchPanel.Padding = new Padding(10);
            searchPanel.BackColor = Color.FromArgb(250, 250, 250);

            // 🔍 Sử dụng custom SearchBoxControl
            var searchBox = new mini_supermarket.GUI.Style.SearchBoxControl();
            searchBox.Location = new Point(15, 20);   // vị trí trong panel
            searchBox.Width = 320;                    // độ rộng
            searchPanel.Controls.Add(searchBox);

            // Thêm panel vào layout
            this.Controls.Add(searchPanel);

            // Product Header (phần tiếp theo của bạn)


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

            // DataGridView Products
            dgvProducts = new DataGridView();
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.RowTemplate.Height = 40;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(34, 139, 34);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.EnableHeadersVisualStyles = false;

            productColumnName = new DataGridViewTextBoxColumn();
            productColumnPrice = new DataGridViewTextBoxColumn();
            productColumnQuantity = new DataGridViewTextBoxColumn();
            productColumnPromotion = new DataGridViewTextBoxColumn();
            productColumnName.HeaderText = "Tên sản phẩm";
            productColumnPrice.HeaderText = "Đơn giá";
            productColumnQuantity.HeaderText = "Số lượng";
            productColumnPromotion.HeaderText = "Khuyến mãi";
            dgvProducts.Columns.AddRange(new DataGridViewColumn[]
            {
                productColumnName, productColumnPrice, productColumnQuantity, productColumnPromotion
            });

            // Bottom Layout
            bottomLayout = new TableLayoutPanel();
            bottomLayout.Dock = DockStyle.Bottom;
            bottomLayout.Height = 250;
            bottomLayout.ColumnCount = 2;
            bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            bottomLayout.RowCount = 2;
            bottomLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            bottomLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            productPreviewPanel = new Panel();
            productPreviewPanel.Dock = DockStyle.Fill;
            productPreviewPanel.BackColor = Color.FromArgb(245, 245, 245);
            productPreviewPanel.BorderStyle = BorderStyle.FixedSingle;
            productPreviewPanel.Padding = new Padding(5);

            // Product Actions
            productActionPanel = new FlowLayoutPanel();
            btnRefresh = new CustomButton();
            btnAddProduct = new CustomButton();
            productActionPanel.Dock = DockStyle.Fill;
            productActionPanel.FlowDirection = FlowDirection.LeftToRight;
            productActionPanel.Padding = new Padding(0, 10, 0, 10);

            btnRefresh.Text = "Làm mới";
            btnRefresh.BackColor = Color.FromArgb(169, 169, 169);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Roboto", 12F, FontStyle.Bold);
            btnRefresh.Size = new Size(140, 45);
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(149, 149, 149);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Padding = new Padding(10, 5, 10, 5);
            ((CustomButton)btnRefresh).BorderRadius = 8;

            btnAddProduct.Text = "Thêm sản phẩm";
            btnAddProduct.BackColor = Color.FromArgb(34, 139, 34);
            btnAddProduct.ForeColor = Color.White;
            btnAddProduct.FlatStyle = FlatStyle.Flat;
            btnAddProduct.Font = new Font("Roboto", 12F, FontStyle.Bold);
            btnAddProduct.Size = new Size(160, 45);
            btnAddProduct.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 205, 50);
            btnAddProduct.FlatAppearance.BorderSize = 0;
            btnAddProduct.Padding = new Padding(10, 5, 10, 5);
            ((CustomButton)btnAddProduct).BorderRadius = 8;

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
            productDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            productDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            productDetailLayout.RowCount = 4;
            for (int i = 0; i < 4; i++)
                productDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            productDetailLayout.Padding = new Padding(10);

            lblProductName.Text = "Tên sản phẩm";
            lblUnitPrice.Text = "Đơn giá";
            lblProductQuantity.Text = "Số lượng";
            lblProductPromotion.Text = "Khuyến mãi";

            foreach (Label l in new[] { lblProductName, lblUnitPrice, lblProductQuantity, lblProductPromotion })
            {
                l.Dock = DockStyle.Fill;
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.Font = new Font("Roboto", 12F);
            }

            foreach (TextBox t in new[] { txtProductName, txtUnitPrice, txtQuantity, txtPromotion })
            {
                t.Font = new Font("Roboto", 12F);
                t.Dock = DockStyle.Fill;
            }

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

            leftPanel.Controls.Add(bottomLayout);
            leftPanel.Controls.Add(dgvProducts);
            leftPanel.Controls.Add(productHeaderPanel);
            leftPanel.Controls.Add(searchPanel);
            leftPanel.Controls.Add(salesHeaderPanel);

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
            chkUsePoints = new CheckBox();

            infoFormLayout.Dock = DockStyle.Top;
            infoFormLayout.Height = 180;
            infoFormLayout.ColumnCount = 3;
            infoFormLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            infoFormLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            infoFormLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            infoFormLayout.RowCount = 4;
            for (int i = 0; i < 4; i++)
                infoFormLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            infoFormLayout.Padding = new Padding(10);

            lblCustomer.Text = "Khách hàng";
            lblAvailablePoints.Text = "Điểm hiện có";
            lblEarnedPoints.Text = "Điểm tích lũy";
            lblUsePoints.Text = "Sử dụng điểm";

            foreach (Label l in new[] { lblCustomer, lblAvailablePoints, lblEarnedPoints, lblUsePoints })
            {
                l.Dock = DockStyle.Fill;
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.Font = new Font("Roboto", 12F);
            }

            foreach (TextBox t in new[] { txtCustomer, txtAvailablePoints, txtEarnedPoints, txtUsePoints })
            {
                t.Font = new Font("Roboto", 12F);
                t.Dock = DockStyle.Fill;
            }

            txtCustomer.PlaceholderText = "Mã KH hoặc SĐT...";

            btnSelectCustomer.Text = "Chọn";
            btnSelectCustomer.BackColor = Color.FromArgb(34, 139, 34);
            btnSelectCustomer.ForeColor = Color.White;
            btnSelectCustomer.FlatStyle = FlatStyle.Flat;
            btnSelectCustomer.Font = new Font("Roboto", 12F, FontStyle.Bold);
            btnSelectCustomer.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 205, 50);
            btnSelectCustomer.FlatAppearance.BorderSize = 0;
            btnSelectCustomer.Padding = new Padding(10, 5, 20, 5);
            btnSelectCustomer.Size = new Size(100, 40);
            ((CustomButton)btnSelectCustomer).BorderRadius = 14;

            chkUsePoints.Text = "Áp dụng điểm";
            chkUsePoints.Font = new Font("Roboto", 12F);
            chkUsePoints.AutoSize = true;
            chkUsePoints.TextAlign = ContentAlignment.MiddleLeft;

            infoFormLayout.Controls.Add(lblCustomer, 0, 0);
            infoFormLayout.Controls.Add(txtCustomer, 1, 0);
            infoFormLayout.Controls.Add(btnSelectCustomer, 2, 0);
            infoFormLayout.Controls.Add(lblAvailablePoints, 0, 1);
            infoFormLayout.Controls.Add(txtAvailablePoints, 1, 1);
            infoFormLayout.Controls.Add(lblEarnedPoints, 0, 2);
            infoFormLayout.Controls.Add(txtEarnedPoints, 1, 2);
            infoFormLayout.Controls.Add(lblUsePoints, 0, 3);
            infoFormLayout.Controls.Add(txtUsePoints, 1, 3);
            infoFormLayout.Controls.Add(chkUsePoints, 2, 3);

            // Order Grid
            dgvOrder = new DataGridView();
            dgvOrder.Dock = DockStyle.Fill;
            dgvOrder.BackgroundColor = Color.White;
            dgvOrder.BorderStyle = BorderStyle.None;
            dgvOrder.RowHeadersVisible = false;
            dgvOrder.RowTemplate.Height = 40;
            dgvOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrder.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvOrder.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(34, 139, 34);
            dgvOrder.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrder.EnableHeadersVisualStyles = false;

            orderColumnName = new DataGridViewTextBoxColumn();
            orderColumnPrice = new DataGridViewTextBoxColumn();
            orderColumnQuantity = new DataGridViewTextBoxColumn();
            orderColumnPromotion = new DataGridViewTextBoxColumn();
            orderColumnName.HeaderText = "Tên sản phẩm";
            orderColumnPrice.HeaderText = "Đơn giá";
            orderColumnQuantity.HeaderText = "Số lượng";
            orderColumnPromotion.HeaderText = "Khuyến mãi";
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
            totalPanel.Height = 60;
            totalPanel.ColumnCount = 3;
            totalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            totalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            totalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            totalPanel.Padding = new Padding(10);

            lblTotal.Text = "Tổng tiền";
            lblTotal.Font = new Font("Roboto", 12F, FontStyle.Bold);
            lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            lblTotal.Dock = DockStyle.Fill;

            txtTotal.Font = new Font("Roboto", 12F);
            txtTotal.Dock = DockStyle.Fill;

            btnRemove.Text = "Xóa";
            btnRemove.BackColor = Color.FromArgb(255, 77, 77);
            btnRemove.ForeColor = Color.White;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("Roboto", 12F, FontStyle.Bold);
            btnRemove.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 99, 99);
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Padding = new Padding(10, 5, 10, 5);
            btnRemove.Size = new Size(100, 40);
            ((CustomButton)btnRemove).BorderRadius = 8;

            totalPanel.Controls.Add(lblTotal, 0, 0);
            totalPanel.Controls.Add(txtTotal, 1, 0);
            totalPanel.Controls.Add(btnRemove, 2, 0);

            // Action Panel
            actionPanel = new FlowLayoutPanel();
            btnCheckout = new CustomButton();
            btnCancel = new CustomButton();
            actionPanel.Dock = DockStyle.Bottom;
            actionPanel.Height = 60;
            actionPanel.FlowDirection = FlowDirection.RightToLeft;
            actionPanel.Padding = new Padding(10);

            btnCheckout.Text = "Thanh toán";
            btnCheckout.BackColor = Color.FromArgb(34, 139, 34);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Roboto", 12F, FontStyle.Bold);
            btnCheckout.Size = new Size(140, 45);
            btnCheckout.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 205, 50);
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.Padding = new Padding(10, 5, 10, 5);
            ((CustomButton)btnCheckout).BorderRadius = 8;

            btnCancel.Text = "Hủy";
            btnCancel.BackColor = Color.FromArgb(169, 169, 169);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Roboto", 12F, FontStyle.Bold);
            btnCancel.Size = new Size(120, 45);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(149, 149, 149);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Padding = new Padding(10, 5, 10, 5);
            ((CustomButton)btnCancel).BorderRadius = 8;

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
            Controls.Add(headerPanel);

            // Thêm tooltip
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnAddProduct, "Thêm sản phẩm vào đơn hàng");
            toolTip.SetToolTip(btnRefresh, "Làm mới danh sách sản phẩm");
            toolTip.SetToolTip(btnCheckout, "Hoàn tất thanh toán");
            toolTip.SetToolTip(btnCancel, "Hủy đơn hàng hiện tại");
            toolTip.SetToolTip(btnRemove, "Xóa sản phẩm khỏi đơn hàng");
            toolTip.SetToolTip(btnSelectCustomer, "Chọn thông tin khách hàng");

            ResumeLayout(false);
        }

        // ===== FIELDS =====
        private Panel headerPanel;
        private Label lblRole, lblGreeting;
        private TableLayoutPanel mainLayout;
        private Panel leftPanel, rightPanel;
        private Panel salesHeaderPanel, searchPanel, productHeaderPanel;
        private Label lblSalesTitle, lblSearch, lblProductTitle;
        private TextBox txtSearch;
        private DataGridView dgvProducts;
        private DataGridViewTextBoxColumn productColumnName, productColumnPrice, productColumnQuantity, productColumnPromotion;
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
        private CheckBox chkUsePoints;
        private DataGridView dgvOrder;
        private DataGridViewTextBoxColumn orderColumnName, orderColumnPrice, orderColumnQuantity, orderColumnPromotion;
        private FlowLayoutPanel actionPanel;
        private CustomButton btnCancel, btnCheckout;
    }
}