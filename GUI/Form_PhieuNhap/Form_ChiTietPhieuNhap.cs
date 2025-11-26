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
                var nhaCungCapList = nhaCungCapBUS.GetNhaCungCap(NhaCungCap_BUS.StatusActive);

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

        private HashSet<int> GetAddedProductIds()
        {
            HashSet<int> addedIds = new HashSet<int>();
            
            foreach (Control ctrl in productRowsContainerPanel.Controls)
            {
                if (ctrl is TextBox txt && txt.ReadOnly && ctrl.Location.X == 0 && txt.Tag != null)
                {
                    int maSanPham = (int)txt.Tag;
                    if (maSanPham > 0)
                    {
                        addedIds.Add(maSanPham);
                    }
                }
            }
            
            return addedIds;
        }

        private void ShowProductSelectionPopupForNewRow()
        {
            // Lấy danh sách ID sản phẩm đã thêm
            HashSet<int> addedProductIds = GetAddedProductIds();

            // Tạo form popup
            Form popup = new Form
            {
                Text = "Chọn sản phẩm",
                Size = new Size(900, 650),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = backgroundColor
            };

            // Label tìm kiếm
            Label lblSearch = new Label
            {
                Text = "🔍 Tìm kiếm:",
                Location = new Point(20, 25),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                TextAlign = ContentAlignment.MiddleLeft
            };
            popup.Controls.Add(lblSearch);

            // TextBox tìm kiếm
            TextBox txtSearch = new TextBox
            {
                Location = new Point(125, 20),
                Size = new Size(300, 35),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Nhập tên sản phẩm, thương hiệu, loại..."
            };
            popup.Controls.Add(txtSearch);
            
            // Focus vào textbox search khi mở popup
            popup.Shown += (s, e) => txtSearch.Focus();

            // Button tìm kiếm
            Button btnSearch = new Button
            {
                Text = "Tìm kiếm",
                Location = new Point(435, 20),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = primaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            popup.Controls.Add(btnSearch);

            // Button reset
            Button btnReset = new Button
            {
                Text = "Reset",
                Location = new Point(545, 20),
                Size = new Size(80, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnReset.FlatAppearance.BorderSize = 0;
            popup.Controls.Add(btnReset);

            // DataGridView hiển thị danh sách sản phẩm
            DataGridView dgvProducts = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(840, 480),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10),
                AutoGenerateColumns = false  // Tắt tự động tạo cột
            };

            // Thêm columns
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaSanPham",
                HeaderText = "Mã SP",
                Width = 20,
                DataPropertyName = "MaSanPham"
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenSanPham",
                HeaderText = "Tên sản phẩm",
                Width = 250,
                DataPropertyName = "TenSanPham"
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenThuongHieu",
                HeaderText = "Thương hiệu",
                Width = 120,
                DataPropertyName = "TenThuongHieu"
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenLoai",
                HeaderText = "Loại",
                Width = 100,
                DataPropertyName = "TenLoai"
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDonVi",
                HeaderText = "Đơn vị",
                Width = 100,
                DataPropertyName = "TenDonVi"
            });

            // Load dữ liệu sản phẩm, lọc bỏ các sản phẩm đã thêm
            List<SanPhamDTO> originalData = sanPhamCache?
                .Where(sp => !addedProductIds.Contains(sp.MaSanPham))
                .ToList() ?? new List<SanPhamDTO>();
            
            if (originalData.Count == 0)
            {
                MessageBox.Show("Tất cả sản phẩm đã được thêm vào phiếu nhập!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                popup.Dispose();
                return;
            }
            
            dgvProducts.DataSource = new BindingSource { DataSource = originalData };

            // Label hiển thị số lượng kết quả
            Label lblResultCount = new Label
            {
                Text = $"Tìm thấy {originalData.Count} sản phẩm",
                Location = new Point(640, 27),
                Size = new Size(200, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = textSecondaryColor,
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.Transparent
            };
            popup.Controls.Add(lblResultCount);

            // Logic tìm kiếm
            Action performSearch = () =>
            {
                try
                {
                    string searchText = txtSearch.Text.Trim().ToLower();
                    
                    if (string.IsNullOrEmpty(searchText))
                    {
                        // Nếu không có từ khóa tìm kiếm, hiển thị toàn bộ dữ liệu
                        dgvProducts.DataSource = new BindingSource { DataSource = originalData };
                        lblResultCount.Text = $"Tìm thấy {originalData.Count} sản phẩm";
                        lblResultCount.ForeColor = textSecondaryColor;
                    }
                    else
                    {
                        // Lọc dữ liệu theo nhiều tiêu chí
                        var filteredData = originalData.Where(sp =>
                        {
                            // Tìm theo tên sản phẩm
                            bool matchName = sp.TenSanPham?.ToLower().Contains(searchText) ?? false;
                            
                            // Tìm theo thương hiệu
                            bool matchBrand = sp.TenThuongHieu?.ToLower().Contains(searchText) ?? false;
                            
                            // Tìm theo loại
                            bool matchCategory = sp.TenLoai?.ToLower().Contains(searchText) ?? false;
                            
                            // Tìm theo mã sản phẩm
                            bool matchId = sp.MaSanPham.ToString().Contains(searchText);
                            
                            // Trả về true nếu khớp với bất kỳ tiêu chí nào
                            return matchName || matchBrand || matchCategory || matchId;
                        }).ToList();
                        
                        // Cập nhật DataGridView với dữ liệu đã lọc
                        dgvProducts.DataSource = new BindingSource { DataSource = filteredData };
                        
                        // Cập nhật label số lượng kết quả
                        if (filteredData.Count == 0)
                        {
                            lblResultCount.Text = "Không tìm thấy kết quả";
                            lblResultCount.ForeColor = Color.FromArgb(244, 67, 54); // Red color
                        }
                        else
                        {
                            lblResultCount.Text = $"Tìm thấy {filteredData.Count} sản phẩm";
                            lblResultCount.ForeColor = successColor; // Green color
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblResultCount.Text = "Lỗi tìm kiếm";
                    lblResultCount.ForeColor = Color.FromArgb(244, 67, 54);
                }
            };

            // Event cho button tìm kiếm
            btnSearch.Click += (s, e) => performSearch();
            
            // Event cho textbox - tìm kiếm khi nhấn Enter
            txtSearch.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    performSearch();
                    e.Handled = true;
                }
            };
            
            // Tìm kiếm tự động khi nhập (tùy chọn - có thể bỏ comment nếu muốn)
            /*
            txtSearch.TextChanged += (s, e) =>
            {
                // Chỉ tự động tìm kiếm nếu đã nhập ít nhất 2 ký tự
                if (txtSearch.Text.Length >= 2 || txtSearch.Text.Length == 0)
                {
                    performSearch();
                }
            };
            */
            
            // Event cho button reset
            btnReset.Click += (s, e) =>
            {
                txtSearch.Text = "";
                txtSearch.Focus();
                dgvProducts.DataSource = new BindingSource { DataSource = originalData };
                lblResultCount.Text = $"Tìm thấy {originalData.Count} sản phẩm";
                lblResultCount.ForeColor = textSecondaryColor;
            };

            // Style cho header
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.ColumnHeadersHeight = 40;
            dgvProducts.EnableHeadersVisualStyles = false;

            // Style cho rows
            dgvProducts.RowTemplate.Height = 35;
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 250);

            popup.Controls.Add(dgvProducts);

            // Button chọn
            Button btnSelect = new Button
            {
                Text = "Chọn sản phẩm",
                Location = new Point(610, 570),
                Size = new Size(150, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = primaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSelect.FlatAppearance.BorderSize = 0;
            btnSelect.Click += (s, e) =>
            {
                if (dgvProducts.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvProducts.SelectedRows[0];
                    var sanPham = selectedRow.DataBoundItem as SanPhamDTO;
                    
                    if (sanPham != null)
                    {
                        // Thêm sản phẩm vào bảng
                        decimal giaBan = sanPham.GiaBan ?? 0;
                        AddProductRowWithData(sanPham.MaSanPham, sanPham.TenSanPham ?? "", giaBan);
                        
                        popup.DialogResult = DialogResult.OK;
                        popup.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một sản phẩm!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            popup.Controls.Add(btnSelect);

            // Button hủy
            Button btnCancelPopup = new Button
            {
                Text = "Hủy",
                Location = new Point(770, 570),
                Size = new Size(90, 40),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelPopup.FlatAppearance.BorderSize = 0;
            btnCancelPopup.Click += (s, e) =>
            {
                popup.DialogResult = DialogResult.Cancel;
                popup.Close();
            };
            popup.Controls.Add(btnCancelPopup);

            // Double click để chọn nhanh
            dgvProducts.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    btnSelect.PerformClick();
                }
            };

            popup.ShowDialog();
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

            // Không tự động thêm hàng đầu tiên nữa
            // AddProductRow();
        }

        private void AddProductRow()
        {
            // Mở popup chọn sản phẩm trước khi tạo row
            ShowProductSelectionPopupForNewRow();
        }

        private void AddProductRowWithData(int maSanPham, string tenSanPham, decimal giaBan)
        {
            int rowY = productRowCount * (ROW_HEIGHT + ROW_MARGIN);

            // TextBox hiển thị sản phẩm đã chọn
            TextBox txtProduct = new TextBox
            {
                Location = new Point(0, rowY),
                Size = new Size(COL1_WIDTH, ROW_HEIGHT),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = textPrimaryColor,
                ReadOnly = true,
                Text = tenSanPham,
                Tag = maSanPham // Store MaSanPham
            };
            productRowsContainerPanel.Controls.Add(txtProduct);

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
                Padding = new Padding(5, 0, 5, 0),
                Text = giaBan.ToString("N0")
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
                Padding = new Padding(5, 0, 5, 0),
                Text = giaBan.ToString("N0")
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
            foreach (Control ctrl in productRowsContainerPanel.Controls)
            {
                // Chỉ lấy TextBox thành tiền (ở vị trí cột 4)
                if (ctrl is TextBox txt && 
                    txt.ReadOnly && 
                    txt.Enabled == false &&
                    ctrl.Location.X == COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + 5 &&
                    !string.IsNullOrWhiteSpace(txt.Text))
                {
                    // Parse the text, removing thousand separators
                    string cleanText = txt.Text.Replace(",", "").Replace(".", "").Trim();
                    if (decimal.TryParse(cleanText, out decimal rowTotal))
                    {
                        grandTotal += rowTotal;
                    }
                }
            }

            if (lblTongTien != null)
            {
                lblTongTien.Text = grandTotal.ToString("N0") + " đ";
            }
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
            if (productRowCount == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra từng hàng sản phẩm
            for (int i = 0; i < productRowCount; i++)
            {
                // Get controls for this row
                TextBox? txtProduct = null;
                NumericUpDown? nudQty = null;
                TextBox? txtPrice = null;

                foreach (Control ctrl in productRowsContainerPanel.Controls)
                {
                    if (ctrl.Top == i * (ROW_HEIGHT + ROW_MARGIN))
                    {
                        if (ctrl is TextBox txt)
                        {
                            // txtProduct có ReadOnly = true và Location.X = 0
                            if (txt.ReadOnly && ctrl.Location.X == 0)
                                txtProduct = txt;
                            // txtPrice không có ReadOnly và Enabled = true
                            else if (!txt.ReadOnly && txt.Enabled)
                                txtPrice = txt;
                        }
                        if (ctrl is NumericUpDown nud) nudQty = nud;
                    }
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
                TextBox? txtProduct = null;
                NumericUpDown? nudQty = null;
                TextBox? txtPrice = null;
                TextBox? txtTotal = null;

                foreach (Control ctrl in productRowsContainerPanel.Controls)
                {
                    if (ctrl.Top == i * (ROW_HEIGHT + ROW_MARGIN))
                    {
                        if (ctrl is NumericUpDown nud) 
                        {
                            nudQty = nud;
                        }
                        else if (ctrl is TextBox txt)
                        {
                            // txtProduct: ReadOnly, Location.X = 0
                            if (txt.ReadOnly && ctrl.Location.X == 0)
                                txtProduct = txt;
                            // txtPrice: không ReadOnly
                            else if (!txt.ReadOnly && txt.Enabled)
                                txtPrice = txt;
                            // txtTotal: ReadOnly và Enabled = false, ở vị trí cột 4
                            else if (txt.ReadOnly && !txt.Enabled && 
                                     ctrl.Location.X == COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + 5)
                                txtTotal = txt;
                        }
                    }
                }

                if (txtProduct != null && txtProduct.Tag != null && 
                    nudQty != null && txtPrice != null && txtTotal != null)
                {
                    // Lấy mã sản phẩm từ Tag
                    int maSanPham = (int)txtProduct.Tag;
                    
                    // Skip nếu chưa chọn sản phẩm
                    if (maSanPham == 0) continue;

                    // Parse đơn giá và thành tiền
                    decimal donGia = decimal.Parse(txtPrice.Text.Replace(",", "").Replace(".", "").Trim());
                    decimal thanhTien = decimal.Parse(txtTotal.Text.Replace(",", "").Replace(".", "").Trim());

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