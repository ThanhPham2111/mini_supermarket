using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.PhieuNhap
{
    public partial class Form_ChiTietPhieuNhap : Form
    {
        // WinAPI for shadow effect
        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private Panel mainPanel = null!;
        private Panel headerPanel = null!;
        private Panel infoSectionPanel = null!;
        private Panel productSectionPanel = null!;
        private Panel productRowsContainerPanel = null!;
        private ComboBox cboNhaCungCap = null!;
        private DateTimePicker dtpNgayNhap = null!;
        private Button btnAdd = null!, btnCancel = null!, btnClose = null!;
        private Label lblTongTien = null!;

        // Product row dimensions
        private const int COL1_WIDTH = 450;  // Sản phẩm
        private const int COL2_WIDTH = 100;  // Số lượng
        private const int COL3_WIDTH = 130;  // Đơn giá
        private const int COL4_WIDTH = 140;  // Thành tiền
        private const int COL5_WIDTH = 50;   // Xóa
        private const int ROW_HEIGHT = 38;
        private const int ROW_MARGIN = 5;

        private int productRowCount = 0;

        // Cache for product data
        private IList<SanPhamDTO>? sanPhamCache = null;

        // Modern color scheme
        private readonly Color primaryColor = Color.FromArgb(33, 150, 243);      // Modern Blue
        private readonly Color primaryDarkColor = Color.FromArgb(25, 118, 210);  // Darker Blue
        private readonly Color successColor = Color.FromArgb(76, 175, 80);       // Green
        private readonly Color cancelColor = Color.FromArgb(158, 158, 158);      // Gray
        private readonly Color backgroundColor = Color.FromArgb(250, 251, 252);  // Light Gray
        private readonly Color cardColor = Color.White;
        private readonly Color borderColor = Color.FromArgb(224, 224, 224);
        private readonly Color textPrimaryColor = Color.FromArgb(33, 33, 33);
        private readonly Color textSecondaryColor = Color.FromArgb(117, 117, 117);

        public Form_ChiTietPhieuNhap()
        {
            // Load data BEFORE InitializeComponent() to ensure sanPhamCache is ready
            LoadSanPhamDataToCache();
            
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadNhaCungCapData();
        }

        private void LoadNhaCungCapData()
        {
            try
            {
                var nhaCungCapBUS = new NhaCungCap_BUS();
                var nhaCungCapList = nhaCungCapBUS.GetAll();

                cboNhaCungCap.Items.Clear();
                cboNhaCungCap.Items.Add(new { MaNhaCungCap = 0, TenNhaCungCap = "-- Chọn nhà cung cấp --" });

                foreach (var nhaCungCap in nhaCungCapList)
                {
                    cboNhaCungCap.Items.Add(new { MaNhaCungCap = nhaCungCap.MaNhaCungCap, TenNhaCungCap = nhaCungCap.TenNhaCungCap });
                }

                cboNhaCungCap.DisplayMember = "TenNhaCungCap";
                cboNhaCungCap.ValueMember = "MaNhaCungCap";
                cboNhaCungCap.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSanPhamData()
        {
            try
            {
                var sanPhamBUS = new SanPham_BUS();
                sanPhamCache = sanPhamBUS.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSanPhamDataToCache()
        {
            LoadSanPhamData();
        }

        private void LoadProductComboBox(ComboBox comboBox)
        {
            try
            {
                comboBox.Items.Clear();
                comboBox.Items.Add(new { MaSanPham = 0, TenSanPham = "-- Chọn sản phẩm --" });

                if (sanPhamCache != null)
                {
                    foreach (var sanPham in sanPhamCache)
                    {
                        comboBox.Items.Add(new { MaSanPham = sanPham.MaSanPham, TenSanPham = sanPham.TenSanPham });
                    }
                }

                comboBox.DisplayMember = "TenSanPham";
                comboBox.ValueMember = "MaSanPham";
                comboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Thêm phiếu nhập hàng";
            this.Size = new Size(1010, 710);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = backgroundColor;

            InitializeMainPanel();
            InitializeHeader();
            InitializeInfoSection();
            InitializeProductSection();
            InitializeTotalSection();
            InitializeActionButtons();
        }

        private void InitializeMainPanel()
        {
            mainPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1000, 710),
                BackColor = cardColor,
                Padding = new Padding(0),
                AutoScroll = true
            };
            this.Controls.Add(mainPanel);
        }

        private void InitializeHeader()
        {
            headerPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1000, 80),
                BackColor = cardColor
            };
            mainPanel.Controls.Add(headerPanel);

            // Icon and Title
            Label lblIcon = new Label
            {
                Text = "📦",
                Location = new Point(35, 20),
                Size = new Size(40, 40),
                Font = new Font("Segoe UI", 20),
                BackColor = Color.Transparent
            };
            headerPanel.Controls.Add(lblIcon);

            Label lblTitle = new Label
            {
                Text = "Thêm phiếu nhập hàng",
                Location = new Point(80, 20),
                Size = new Size(450, 40),
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                BackColor = Color.Transparent,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            headerPanel.Controls.Add(lblTitle);
            // Bottom border
            Panel line = new Panel
            {
                Location = new Point(0, 79),
                Size = new Size(1000, 1),
                BackColor = borderColor
            };
            headerPanel.Controls.Add(line);
        }

        private void InitializeInfoSection()
        {
            infoSectionPanel = new Panel
            {
                Location = new Point(35, 100),
                Size = new Size(930, 100),
                BackColor = backgroundColor,
                Padding = new Padding(20)
            };
            mainPanel.Controls.Add(infoSectionPanel);

            // Section title with icon
            Label lblSectionIcon = new Label
            {
                Text = "ℹ️",
                Location = new Point(0, 0),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 14),
                BackColor = Color.Transparent
            };
            infoSectionPanel.Controls.Add(lblSectionIcon);

            Label lblSection = new Label
            {
                Text = "Thông tin chung",
                Location = new Point(35, 0),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                BackColor = Color.Transparent
            };
            infoSectionPanel.Controls.Add(lblSection);

            int startY = 50;
            int labelW = 130;
            int fieldW = 280;
            int fieldH = 38;
            int gapX = 50;
            int col1X = 0;
            int col2X = col1X + labelW + fieldW + gapX;

            // Row 1: Ngày nhập & Nhà cung cấp
            CreateStyledLabel("Ngày nhập", col1X, startY, labelW, infoSectionPanel);
            dtpNgayNhap = CreateStyledDateTimePicker(col1X + labelW, startY, fieldW, fieldH, infoSectionPanel);

            CreateStyledLabel("Nhà cung cấp", col2X, startY, labelW, infoSectionPanel);
            cboNhaCungCap = CreateStyledComboBox(col2X + labelW, startY, fieldW, fieldH, infoSectionPanel);
        }

        private void InitializeProductSection()
        {
            // Header section with title and button (outside the main panel)
            int headerY = 200;
            
            // Section title with icon
            Label lblSectionIcon = new Label
            {
                Text = "📋",
                Location = new Point(35, headerY),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 14),
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblSectionIcon);

            Label lblSection = new Label
            {
                Text = "Chi tiết sản phẩm",
                Location = new Point(70, headerY),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                BackColor = Color.Transparent
            };
            mainPanel.Controls.Add(lblSection);

            // Add button to add new product (next to label)
            Button btnAddProduct = new Button
            {
                Text = "+ Thêm sản phẩm",
                Location = new Point(764, headerY + 2),
                Size = new Size(140, 28),
                BackColor = Color.FromArgb(240, 245, 250),
                ForeColor = primaryColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnAddProduct.FlatAppearance.BorderColor = primaryColor;
            btnAddProduct.FlatAppearance.BorderSize = 1;
            btnAddProduct.MouseEnter += (s, e) => {
                btnAddProduct.BackColor = primaryColor;
                btnAddProduct.ForeColor = Color.White;
            };
            btnAddProduct.MouseLeave += (s, e) => {
                btnAddProduct.BackColor = Color.FromArgb(240, 245, 250);
                btnAddProduct.ForeColor = primaryColor;
            };
            btnAddProduct.Click += (s, e) => AddProductRow();
            mainPanel.Controls.Add(btnAddProduct);

            // Product data panel
            productSectionPanel = new Panel
            {
                Location = new Point(35, 240),
                Size = new Size(930, 300),
                BackColor = backgroundColor,
                Padding = new Padding(20),
                AutoScroll = true
            };
            mainPanel.Controls.Add(productSectionPanel);

            // Create header row
            int tableHeaderY = 0;
            int headerH = 35;

            CreateTableHeader("Sản phẩm", 0, tableHeaderY, COL1_WIDTH, headerH, productSectionPanel);
            CreateTableHeader("Số lượng", COL1_WIDTH, tableHeaderY, COL2_WIDTH, headerH, productSectionPanel);
            CreateTableHeader("Đơn giá", COL1_WIDTH + COL2_WIDTH, tableHeaderY, COL3_WIDTH, headerH, productSectionPanel);
            CreateTableHeader("Thành tiền", COL1_WIDTH + COL2_WIDTH + COL3_WIDTH, tableHeaderY, COL4_WIDTH, headerH, productSectionPanel);
            CreateTableHeader("", COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + COL4_WIDTH, tableHeaderY, COL5_WIDTH, headerH, productSectionPanel);

            // Create container panel for product rows
            productRowsContainerPanel = new Panel
            {
                Location = new Point(0, tableHeaderY + headerH + 10),
                Size = new Size(COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + COL4_WIDTH + COL5_WIDTH, 200),
                BackColor = Color.White,
                AutoSize = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            productSectionPanel.Controls.Add(productRowsContainerPanel);

            // Add first product row
            AddProductRow();
        }

        private void AddProductRow()
        {
            int rowY = productRowCount * (ROW_HEIGHT + ROW_MARGIN);

            // ComboBox sản phẩm
            ComboBox cboProduct = new ComboBox
            {
                Location = new Point(0, rowY),
                Size = new Size(COL1_WIDTH - 5, ROW_HEIGHT),
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                ForeColor = textPrimaryColor
            };
            
            // Load sản phẩm vào ComboBox
            LoadProductComboBox(cboProduct);
            
            productRowsContainerPanel.Controls.Add(cboProduct);

            // NumericUpDown số lượng
            NumericUpDown nudQty = new NumericUpDown
            {
                Location = new Point(COL1_WIDTH + 5, rowY),
                Size = new Size(COL2_WIDTH - 10, ROW_HEIGHT),
                Font = new Font("Segoe UI", 11),
                Minimum = 1,
                Maximum = 10000,
                Value = 1,
                TextAlign = HorizontalAlignment.Center,
                BorderStyle = BorderStyle.FixedSingle
            };
            productRowsContainerPanel.Controls.Add(nudQty);

            // TextBox đơn giá
            TextBox txtPrice = new TextBox
            {
                Location = new Point(COL1_WIDTH + COL2_WIDTH + 5, rowY),
                Size = new Size(COL3_WIDTH - 10, ROW_HEIGHT),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = textPrimaryColor,
                Padding = new Padding(5, 0, 5, 0)
            };
            productRowsContainerPanel.Controls.Add(txtPrice);

            // TextBox thành tiền (read-only/disabled)
            TextBox txtTotal = new TextBox
            {
                Location = new Point(COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + 5, rowY),
                Size = new Size(COL4_WIDTH - 10, ROW_HEIGHT),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                Enabled = false,
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = primaryColor,
                Padding = new Padding(5, 0, 5, 0)
            };
            productRowsContainerPanel.Controls.Add(txtTotal);

            // Button Xóa
            Button btnDelete = new Button
            {
                Text = "✕",
                Location = new Point(COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + COL4_WIDTH + 8, rowY-3),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 245, 245),
                ForeColor = Color.FromArgb(244, 67, 54),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = rowY // Store row position for identification
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.MouseEnter += (s, e) => {
                btnDelete.BackColor = Color.FromArgb(244, 67, 54);
                btnDelete.ForeColor = Color.White;
            };
            btnDelete.MouseLeave += (s, e) => {
                btnDelete.BackColor = Color.FromArgb(255, 245, 245);
                btnDelete.ForeColor = Color.FromArgb(244, 67, 54);
            };
            btnDelete.Click += (s, e) => RemoveProductRow(rowY);
            productRowsContainerPanel.Controls.Add(btnDelete);

            // Update đơn giá và thành tiền khi chọn sản phẩm
            cboProduct.SelectedIndexChanged += (s, e) =>
            {
                if (cboProduct.SelectedItem != null)
                {
                    var selectedItem = cboProduct.SelectedItem;
                    var maSanPham = (int)selectedItem.GetType().GetProperty("MaSanPham")!.GetValue(selectedItem)!;

                    if (maSanPham > 0 && sanPhamCache != null)
                    {
                        var sanPham = sanPhamCache.FirstOrDefault(sp => sp.MaSanPham == maSanPham);
                        if (sanPham != null && sanPham.GiaBan.HasValue)
                        {
                            txtPrice.Text = sanPham.GiaBan.Value.ToString("N0");
                        }
                    }
                    else
                    {
                        txtPrice.Text = "";
                        txtTotal.Text = "";
                    }
                }
            };

            // Update thành tiền when quantity or price changes
            nudQty.ValueChanged += (s, e) => UpdateRowTotal(nudQty, txtPrice, txtTotal);
            txtPrice.TextChanged += (s, e) => UpdateRowTotal(nudQty, txtPrice, txtTotal);

            productRowCount++;

            // Update container height
            productRowsContainerPanel.Height = productRowCount * (ROW_HEIGHT + ROW_MARGIN);

            // Scroll to bottom
            productSectionPanel.AutoScrollPosition = new Point(0, productRowsContainerPanel.Height);
        }

        private void RemoveProductRow(int rowY)
        {
            // Không cho xóa nếu chỉ còn 1 hàng
            if (productRowCount <= 1)
            {
                MessageBox.Show("Phải có ít nhất một sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xóa tất cả controls trong hàng này
            List<Control> controlsToRemove = new List<Control>();
            foreach (Control ctrl in productRowsContainerPanel.Controls)
            {
                if (ctrl.Top == rowY)
                {
                    controlsToRemove.Add(ctrl);
                }
            }

            foreach (Control ctrl in controlsToRemove)
            {
                productRowsContainerPanel.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            // Dịch chuyển các hàng phía dưới lên
            foreach (Control ctrl in productRowsContainerPanel.Controls)
            {
                if (ctrl.Top > rowY)
                {
                    ctrl.Top -= (ROW_HEIGHT + ROW_MARGIN);
                    
                    // Update Tag for delete buttons
                    if (ctrl is Button btn && btn.Text == "✕")
                    {
                        btn.Tag = ctrl.Top;
                        // Update click event
                        btn.Click -= (s, e) => RemoveProductRow((int)btn.Tag);
                        int newRowY = ctrl.Top;
                        btn.Click += (s, e) => RemoveProductRow(newRowY);
                    }
                }
            }

            productRowCount--;

            // Update container height
            productRowsContainerPanel.Height = productRowCount * (ROW_HEIGHT + ROW_MARGIN);

            // Cập nhật tổng tiền
            UpdateGrandTotal();
        }

        private void UpdateRowTotal(NumericUpDown qty, TextBox price, TextBox total)
        {
            if (decimal.TryParse(price.Text, out decimal donGia))
            {
                int soLuong = (int)qty.Value;
                total.Text = (soLuong * donGia).ToString("N0");
            }
            else
            {
                total.Text = "";
            }

            // Cập nhật tổng tiền
            UpdateGrandTotal();
        }

        private void UpdateGrandTotal()
        {
            decimal grandTotal = 0;

            // Tính tổng từ tất cả các hàng sản phẩm
            for (int i = 0; i < productRowCount; i++)
            {
                foreach (Control ctrl in productRowsContainerPanel.Controls)
                {
                    if (ctrl.Top == i * (ROW_HEIGHT + ROW_MARGIN) && 
                        ctrl is TextBox txt && 
                        txt.ReadOnly && 
                        !string.IsNullOrWhiteSpace(txt.Text))
                    {
                        // Parse the text, removing thousand separators
                        string cleanText = txt.Text.Replace(",", "").Replace(".", "");
                        if (decimal.TryParse(cleanText, out decimal rowTotal))
                        {
                            grandTotal += rowTotal;
                        }
                    }
                }
            }

            lblTongTien.Text = grandTotal.ToString("N0") + " đ";
        }

        private void CreateTableHeader(string text, int x, int y, int width, int height, Panel parent)
        {
            Label header = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = primaryColor,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };
            parent.Controls.Add(header);
        }

        private void InitializeTotalSection()
        {
            // Panel chứa tổng tiền
            Panel totalPanel = new Panel
            {
                Location = new Point(35, 545),
                Size = new Size(930, 50),
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            mainPanel.Controls.Add(totalPanel);

            // Label "Tổng tiền:"
            Label lblTongTienText = new Label
            {
                Text = "Tổng tiền:",
                Location = new Point(570, 12),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.Transparent
            };
            totalPanel.Controls.Add(lblTongTienText);

            // Label hiển thị số tiền
            lblTongTien = new Label
            {
                Text = "0 đ",
                Location = new Point(730, 12),
                Size = new Size(180, 25),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = primaryColor,
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.Transparent
            };
            totalPanel.Controls.Add(lblTongTien);
        }

        private void InitializeActionButtons()
        {
            // Add button with modern styling
            btnAdd = new Button
            {
                Text = "✓  Thêm phiếu nhập",
                Location = new Point(590, 610),
                Size = new Size(260, 45),
                BackColor = primaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.MouseEnter += (s, e) => btnAdd.BackColor = primaryDarkColor;
            btnAdd.MouseLeave += (s, e) => btnAdd.BackColor = primaryColor;
            btnAdd.Click += BtnAdd_Click;
            mainPanel.Controls.Add(btnAdd);

            // Cancel button
            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(870, 610),
                Size = new Size(95, 45),
                BackColor = Color.White,
                ForeColor = textSecondaryColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderColor = borderColor;
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.MouseEnter += (s, e) => {
                btnCancel.BackColor = Color.FromArgb(245, 245, 245);
                btnCancel.ForeColor = textPrimaryColor;
            };
            btnCancel.MouseLeave += (s, e) => {
                btnCancel.BackColor = Color.White;
                btnCancel.ForeColor = textSecondaryColor;
            };
            btnCancel.Click += (s, e) => this.Close();
            mainPanel.Controls.Add(btnCancel);
        }

        // Helper methods for creating styled controls
        private Label CreateStyledLabel(string text, int x, int y, int width, Panel parent)
        {
            Label lbl = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 28),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = textSecondaryColor,
                BackColor = Color.Transparent
            };
            parent.Controls.Add(lbl);
            return lbl;
        }

        private TextBox CreateStyledTextBox(int x, int y, int width, int height, Panel parent, bool readOnly = false)
        {
            TextBox txt = new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = readOnly,
                BackColor = readOnly ? Color.FromArgb(248, 249, 250) : Color.White,
                ForeColor = textPrimaryColor,
                Padding = new Padding(5, 0, 5, 0)
            };
            parent.Controls.Add(txt);
            return txt;
        }

        private ComboBox CreateStyledComboBox(int x, int y, int width, int height, Panel parent)
        {
            ComboBox cbo = new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                ForeColor = textPrimaryColor
            };
            parent.Controls.Add(cbo);
            return cbo;
        }

        private DateTimePicker CreateStyledDateTimePicker(int x, int y, int width, int height, Panel parent)
        {
            DateTimePicker dtp = new DateTimePicker
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                Font = new Font("Segoe UI", 11),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };
            parent.Controls.Add(dtp);
            return dtp;
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            // Validate thông tin chung
            if (cboNhaCungCap.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate ngày nhập không được ở quá khứ
            if (dtpNgayNhap.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày nhập không được ở quá khứ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate chi tiết sản phẩm
            if (productRowsContainerPanel.Controls.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra từng hàng sản phẩm
            for (int i = 0; i < productRowCount; i++)
            {
                // Get controls for this row
                ComboBox? cboProduct = null;
                NumericUpDown? nudQty = null;
                TextBox? txtPrice = null;

                foreach (Control ctrl in productRowsContainerPanel.Controls)
                {
                    if (ctrl.Top == i * (ROW_HEIGHT + ROW_MARGIN))
                    {
                        if (ctrl is ComboBox cbo) cboProduct = cbo;
                        if (ctrl is NumericUpDown nud) nudQty = nud;
                        if (ctrl is TextBox txt && !txt.ReadOnly) txtPrice = txt;
                    }
                }

                if (cboProduct != null && cboProduct.SelectedIndex == 0)
                {
                    MessageBox.Show($"Vui lòng chọn sản phẩm ở hàng {i + 1}!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPrice != null && (string.IsNullOrWhiteSpace(txtPrice.Text) || 
                    !decimal.TryParse(txtPrice.Text.Replace(",", "").Replace(".", ""), out decimal price) || price <= 0))
                {
                    MessageBox.Show($"Vui lòng nhập đơn giá hợp lệ cho hàng {i + 1}!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nudQty != null && nudQty.Value <= 0)
                {
                    MessageBox.Show($"Số lượng phải lớn hơn 0 ở hàng {i + 1}!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Build PhieuNhapDTO
            PhieuNhapDTO phieuNhap = BuildPhieuNhapDTO();

            try
            {
                // Gọi BUS để lưu vào database
                var phieuNhapBUS = new PhieuNhap_BUS();
                phieuNhapBUS.AddPhieuNhap(phieuNhap);

                MessageBox.Show("✓ Thêm phiếu nhập thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm phiếu nhập:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PhieuNhapDTO BuildPhieuNhapDTO()
        {
            // Lấy mã nhà cung cấp từ ComboBox
            var selectedNCC = cboNhaCungCap.SelectedItem;
            int maNhaCungCap = selectedNCC != null 
                ? (int)selectedNCC.GetType().GetProperty("MaNhaCungCap")!.GetValue(selectedNCC)!
                : 0;

            // Tạo PhieuNhapDTO
            var phieuNhap = new PhieuNhapDTO
            {
                NgayNhap = dtpNgayNhap.Value,
                MaNhaCungCap = maNhaCungCap,
                TongTien = 0
            };

            // Duyệt qua từng hàng sản phẩm để tạo ChiTietPhieuNhapDTO
            for (int i = 0; i < productRowCount; i++)
            {
                ComboBox? cboProduct = null;
                NumericUpDown? nudQty = null;
                TextBox? txtPrice = null;
                TextBox? txtTotal = null;

                foreach (Control ctrl in productRowsContainerPanel.Controls)
                {
                    if (ctrl.Top == i * (ROW_HEIGHT + ROW_MARGIN))
                    {
                        if (ctrl is ComboBox cbo) cboProduct = cbo;
                        if (ctrl is NumericUpDown nud) nudQty = nud;
                        if (ctrl is TextBox txt)
                        {
                            if (!txt.ReadOnly)
                                txtPrice = txt;
                            else
                                txtTotal = txt;
                        }
                    }
                }

                if (cboProduct != null && cboProduct.SelectedItem != null && 
                    nudQty != null && txtPrice != null && txtTotal != null)
                {
                    // Lấy mã sản phẩm
                    var selectedProduct = cboProduct.SelectedItem;
                    int maSanPham = (int)selectedProduct.GetType().GetProperty("MaSanPham")!.GetValue(selectedProduct)!;

                    // Parse đơn giá và thành tiền
                    decimal donGia = decimal.Parse(txtPrice.Text.Replace(",", "").Replace(".", ""));
                    decimal thanhTien = decimal.Parse(txtTotal.Text.Replace(",", "").Replace(".", ""));

                    // Tạo chi tiết
                    var chiTiet = new ChiTietPhieuNhapDTO
                    {
                        MaSanPham = maSanPham,
                        SoLuong = (int)nudQty.Value,
                        DonGiaNhap = donGia,
                        ThanhTien = thanhTien
                    };

                    phieuNhap.ChiTietPhieuNhaps.Add(chiTiet);
                    phieuNhap.TongTien += thanhTien;
                }
            }

            return phieuNhap;
        }
    }
}