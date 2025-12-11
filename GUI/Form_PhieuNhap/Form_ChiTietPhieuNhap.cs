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
        private Button btnAdd = null!, btnCancel = null!;
        private Label lblTongTien = null!;

        // Product row dimensions
        private const int COL0_WIDTH = 80;   // Mã SP
        private const int COL1_WIDTH = 300;  // Sản phẩm (giảm để nhường chỗ cho HSD)
        private const int COL2_WIDTH = 100;  // Số lượng
        private const int COL3_WIDTH = 130;  // Đơn giá
        private const int COL4_WIDTH = 120;  // HSD (mới)
        private const int COL5_WIDTH = 140;  // Thành tiền
        private const int COL6_WIDTH = 100;   // Xóa
        private const int ROW_HEIGHT = 38;
        private const int ROW_MARGIN = 5;

        private int productRowCount = 0;

        // Cache for product data
        private IList<SanPhamDTO>? sanPhamCache = null;

        // Modern color scheme
        private readonly Color primaryColor = Color.FromArgb(0, 120, 215);      // Standard Blue
        private readonly Color primaryDarkColor = Color.FromArgb(0, 90, 158);   // Darker Blue
        private readonly Color successColor = Color.FromArgb(16, 137, 62);      // Standard Green
        private readonly Color cancelColor = Color.FromArgb(108, 117, 125);     // Gray
        private readonly Color backgroundColor = Color.WhiteSmoke;              // Light Gray
        private readonly Color cardColor = Color.White;
        private readonly Color borderColor = Color.FromArgb(224, 224, 224);
        private readonly Color textPrimaryColor = Color.FromArgb(33, 33, 33);
        private readonly Color textSecondaryColor = Color.FromArgb(117, 117, 117);

        public Form_ChiTietPhieuNhap()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadNhaCungCapData();
            // Load sản phẩm sau khi đã load nhà cung cấp
            LoadSanPhamData();
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
                cboNhaCungCap.SelectedIndexChanged += CboNhaCungCap_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboNhaCungCap_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Khi chọn nhà cung cấp, lọc lại danh sách sản phẩm
            LoadSanPhamData();
            // Cập nhật lại tất cả các combobox sản phẩm trong các dòng đã thêm
            RefreshAllProductComboBoxes();
        }

        private void LoadSanPhamData()
        {
            try
            {
                var sanPhamBUS = new SanPham_BUS();
                int? maNhaCungCap = null;

                // Lấy mã nhà cung cấp đã chọn (chỉ khi combobox đã được khởi tạo và đã chọn một nhà cung cấp hợp lệ)
                if (cboNhaCungCap != null && cboNhaCungCap.SelectedIndex > 0 && cboNhaCungCap.SelectedItem != null)
                {
                    try
                    {
                        var selectedNCC = cboNhaCungCap.SelectedItem;
                        int selectedMaNCC = (int)selectedNCC.GetType().GetProperty("MaNhaCungCap")!.GetValue(selectedNCC)!;
                        if (selectedMaNCC > 0)
                        {
                            maNhaCungCap = selectedMaNCC;
                        }
                    }
                    catch
                    {
                        // Nếu không lấy được giá trị, để maNhaCungCap = null
                    }
                }

                if (!maNhaCungCap.HasValue || maNhaCungCap.Value <= 0)
                {
                    // Nếu chưa chọn nhà cung cấp, để sanPhamCache rỗng
                    sanPhamCache = new List<SanPhamDTO>();
                    return;
                }

                // Chỉ lấy sản phẩm đang ở trạng thái "Còn hàng" và thuộc nhà cung cấp đã chọn
                var allSanPham = sanPhamBUS.GetSanPham(SanPham_BUS.StatusConHang, maNhaCungCap);
                
                // Lọc bỏ sản phẩm có trạng thái bán = "Không bán"
                var khoHangBUS = new KhoHangBUS();
                sanPhamCache = allSanPham.Where(sp =>
                {
                    var trangThaiBan = khoHangBUS.GetTrangThaiDieuKienBan(sp.MaSanPham);
                    // Chỉ lấy sản phẩm có trạng thái bán = "Bán" hoặc null (mặc định là "Bán")
                    return trangThaiBan != KhoHangBUS.TRANG_THAI_DIEU_KIEN_KHONG_BAN;
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshAllProductComboBoxes()
        {
            // Cập nhật lại tất cả các combobox sản phẩm trong các dòng đã thêm
            foreach (Control control in productRowsContainerPanel.Controls)
            {
                if (control is Panel rowPanel)
                {
                    foreach (Control ctrl in rowPanel.Controls)
                    {
                        if (ctrl is ComboBox comboBox && comboBox.Name == "productComboBox")
                        {
                            LoadProductComboBox(comboBox);
                            break;
                        }
                    }
                }
            }
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
                // Tìm TextBox tên sản phẩm (có Tag chứa MaSanPham, ở vị trí COL0_WIDTH + 5)
                if (ctrl is TextBox txt && txt.ReadOnly && ctrl.Location.X == COL0_WIDTH + 5 && txt.Tag != null)
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
            // Kiểm tra xem nhà cung cấp đã được chọn chưa
            if (cboNhaCungCap == null || cboNhaCungCap.SelectedIndex <= 0 || cboNhaCungCap.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp trước khi thêm sản phẩm!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã nhà cung cấp từ SelectedItem (vì dùng anonymous objects)
            var selectedNCC = cboNhaCungCap.SelectedItem;
            int maNhaCungCap = 0;
            try
            {
                maNhaCungCap = (int)selectedNCC.GetType().GetProperty("MaNhaCungCap")!.GetValue(selectedNCC)!;
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp trước khi thêm sản phẩm!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (maNhaCungCap <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp trước khi thêm sản phẩm!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đảm bảo sanPhamCache đã được cập nhật với sản phẩm của nhà cung cấp đã chọn
            LoadSanPhamData();

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
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                TextAlign = ContentAlignment.MiddleLeft
            };
            popup.Controls.Add(lblSearch);

            // TextBox tìm kiếm
            TextBox txtSearch = new TextBox
            {
                Location = new Point(130, 20),
                Size = new Size(280, 35),
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
                Location = new Point(420, 20),
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
                Location = new Point(530, 20),
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
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Hsd",
                HeaderText = "HSD",
                Width = 120,
                DataPropertyName = "Hsd",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
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
                Location = new Point(610, 560),
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
                        DateTime? hsd = sanPham.Hsd;
                        AddProductRowWithData(sanPham.MaSanPham, sanPham.TenSanPham ?? "", giaBan, hsd);
                        
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
                Location = new Point(770, 560),
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
            InitializeFooterSection(); // Footer first (Dock Bottom)
            InitializeProductSection(); // Fill remaining space
        }

        private void InitializeMainPanel()
        {
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = backgroundColor,
                Padding = new Padding(20)
            };
            this.Controls.Add(mainPanel);
        }

        private void InitializeHeader()
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = cardColor,
                Padding = new Padding(15, 10, 15, 10)
            };
            mainPanel.Controls.Add(headerPanel);

            // Title
            Label lblTitle = new Label
            {
                Text = "THÊM PHIẾU NHẬP HÀNG",
                Dock = DockStyle.Left,
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = primaryColor,
                TextAlign = ContentAlignment.MiddleLeft
            };
            headerPanel.Controls.Add(lblTitle);
        }

        private void InitializeInfoSection()
        {
            infoSectionPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = cardColor,
                Padding = new Padding(20),
                Margin = new Padding(0, 20, 0, 20)
            };
            mainPanel.Controls.Add(infoSectionPanel);
            infoSectionPanel.BringToFront(); // Ensure order

            TableLayoutPanel tblInfo = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 2,
                Padding = new Padding(0)
            };
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            
            infoSectionPanel.Controls.Add(tblInfo);

            // Row 1: Ngày nhập & Nhà cung cấp
            Label lblNgayNhap = new Label { Text = "Ngày nhập:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 9f) };
            dtpNgayNhap = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10), MinDate = DateTime.Today };
            dtpNgayNhap.ValueChanged += (s, e) =>
            {
                if (dtpNgayNhap.Value.Date < DateTime.Today)
                {
                    MessageBox.Show("Ngày nhập không được ở quá khứ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayNhap.Value = DateTime.Today;
                }
            };

            Label lblNhaCungCap = new Label { Text = "NCC:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 9f) };
            cboNhaCungCap = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };

            tblInfo.Controls.Add(lblNgayNhap, 0, 0);
            tblInfo.Controls.Add(dtpNgayNhap, 1, 0);
            tblInfo.Controls.Add(lblNhaCungCap, 2, 0);
            tblInfo.Controls.Add(cboNhaCungCap, 3, 0);
        }

        private void InitializeFooterSection()
        {
            Panel footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = cardColor,
                Padding = new Padding(20)
            };
            mainPanel.Controls.Add(footerPanel);

            // Total Label Text
            Label lblTotalText = new Label
            {
                Text = "Tổng tiền:",
                Dock = DockStyle.Right,
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 15, 15, 0)
            };
            footerPanel.Controls.Add(lblTotalText);

            // Total Amount
            lblTongTien = new Label
            {
                Text = "0 đ",
                Dock = DockStyle.Right,
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 10, 10, 0)
            };
            footerPanel.Controls.Add(lblTongTien);

            // Buttons
            FlowLayoutPanel flowButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight
            };
            footerPanel.Controls.Add(flowButtons);

            btnAdd = new Button
            {
                Text = "Lưu phiếu nhập",
                Size = new Size(150, 40),
                BackColor = primaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;

            btnCancel = new Button
            {
                Text = "Hủy",
                Size = new Size(100, 40),
                BackColor = cancelColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand,
                Margin = new Padding(10, 0, 0, 0)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            flowButtons.Controls.Add(btnAdd);
            flowButtons.Controls.Add(btnCancel);
        }

        private void InitializeProductSection()
        {
            productSectionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = cardColor,
                Padding = new Padding(20),
                Margin = new Padding(0, 20, 0, 0)
            };
            mainPanel.Controls.Add(productSectionPanel);
            productSectionPanel.BringToFront();

            // Header for products
            Panel pnlProductHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,  // Tăng height để tạo khoảng cách với table header bên dưới
                Padding = new Padding(0, 0, 0, 15) // Thêm padding bottom
            };
            productSectionPanel.Controls.Add(pnlProductHeader);

            Label lblProductTitle = new Label
            {
                Text = "Chi tiết sản phẩm",
                Dock = DockStyle.Left,
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlProductHeader.Controls.Add(lblProductTitle);

            Button btnAddProduct = new Button
            {
                Text = "➕ Thêm sản phẩm",
                Dock = DockStyle.Right,
                Size = new Size(170, 38),
                BackColor = Color.FromArgb(16, 137, 62),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAddProduct.FlatAppearance.BorderSize = 0;
            btnAddProduct.Click += (s, e) => AddProductRow();
            pnlProductHeader.Controls.Add(btnAddProduct);

            // Table Header
            Panel pnlTableHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = primaryColor,
                Margin = new Padding(0, 10, 0, 0)
            };
            productSectionPanel.Controls.Add(pnlTableHeader);
            pnlTableHeader.BringToFront();

            // Helper to add header labels
            void AddHeader(string text, int width, DockStyle dock)
            {
                Label lbl = new Label
                {
                    Text = text,
                    Width = width,
                    Dock = dock,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlTableHeader.Controls.Add(lbl);
                lbl.BringToFront(); // Dock order matters
            }

            // Dock Right to Left
            AddHeader("", COL6_WIDTH, DockStyle.Right); // Delete
            AddHeader("Thành tiền", COL5_WIDTH, DockStyle.Right);
            AddHeader("HSD", COL4_WIDTH, DockStyle.Right);
            AddHeader("Đơn giá", COL3_WIDTH, DockStyle.Right);
            AddHeader("Số lượng", COL2_WIDTH, DockStyle.Right);
            AddHeader("Sản phẩm", COL1_WIDTH, DockStyle.Fill); // Fill the rest
            AddHeader("Mã SP", COL0_WIDTH, DockStyle.Left);

            // Product Rows Container
            productRowsContainerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White
            };
            productSectionPanel.Controls.Add(productRowsContainerPanel);
            productRowsContainerPanel.BringToFront();
        }


        private void AddProductRow()
        {
            // Mở popup chọn sản phẩm trước khi tạo row
            ShowProductSelectionPopupForNewRow();
        }

        private void AddProductRowWithData(int maSanPham, string tenSanPham, decimal giaBan, DateTime? hsd = null)
        {
            int rowY = productRowCount * (ROW_HEIGHT + ROW_MARGIN);

            // Lấy thông tin HSD từ sản phẩm nếu không được truyền vào
            if (!hsd.HasValue)
            {
                var sanPhamBUS = new SanPham_BUS();
                var sanPham = sanPhamBUS.GetSanPhamById(maSanPham);
                hsd = sanPham?.Hsd;
            }

            // TextBox hiển thị mã sản phẩm
            TextBox txtMaSanPham = new TextBox
            {
                Location = new Point(0, rowY),
                Size = new Size(COL0_WIDTH - 5, ROW_HEIGHT),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = textPrimaryColor,
                ReadOnly = true,
                Text = maSanPham.ToString(),
                TextAlign = HorizontalAlignment.Center
            };
            productRowsContainerPanel.Controls.Add(txtMaSanPham);

            // TextBox hiển thị sản phẩm đã chọn
            TextBox txtProduct = new TextBox
            {
                Location = new Point(COL0_WIDTH + 5, rowY),
                Size = new Size(COL1_WIDTH - 5, ROW_HEIGHT),
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
                Location = new Point(COL0_WIDTH + COL1_WIDTH + 5, rowY),
                Size = new Size(COL2_WIDTH - 5, ROW_HEIGHT),
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
                Location = new Point(COL0_WIDTH + COL1_WIDTH + COL2_WIDTH, rowY),
                Size = new Size(COL3_WIDTH - 5, ROW_HEIGHT),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = textPrimaryColor,
                TextAlign = HorizontalAlignment.Right,
                Text = giaBan.ToString("N0")
            };
            productRowsContainerPanel.Controls.Add(txtPrice);

            // TextBox HSD (ReadOnly)
            TextBox txtHSD = new TextBox
            {
                Location = new Point(COL0_WIDTH + COL1_WIDTH + COL2_WIDTH + COL3_WIDTH, rowY),
                Size = new Size(COL4_WIDTH - 5, ROW_HEIGHT),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                Enabled = false,
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = textPrimaryColor,
                TextAlign = HorizontalAlignment.Center,
                Text = hsd?.ToString("dd/MM/yyyy") ?? "N/A",
                Tag = hsd // Lưu giá trị DateTime để dùng sau
            };
            productRowsContainerPanel.Controls.Add(txtHSD);

            // TextBox thành tiền (read-only/disabled)
            TextBox txtTotal = new TextBox
            {
                Location = new Point(COL0_WIDTH + COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + COL4_WIDTH, rowY),
                Size = new Size(COL5_WIDTH - 5, ROW_HEIGHT),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                Enabled = false,
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = primaryColor,
                TextAlign = HorizontalAlignment.Right,
                Text = giaBan.ToString("N0")
            };
            productRowsContainerPanel.Controls.Add(txtTotal);

            // Button Xóa
            Button btnDelete = new Button
            {
                Text = "✕",
                Location = new Point(COL0_WIDTH + COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + COL4_WIDTH + COL5_WIDTH + 5, rowY + 4),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 245, 245),
                ForeColor = Color.FromArgb(244, 67, 54),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "btnDelete" // Đặt tên để nhận diện
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
            btnDelete.Click += BtnDelete_Click;
            productRowsContainerPanel.Controls.Add(btnDelete);

            // Update thành tiền when quantity or price changes
            nudQty.ValueChanged += (s, e) => UpdateRowTotal(nudQty, txtPrice, txtTotal);
            txtPrice.TextChanged += (s, e) => UpdateRowTotal(nudQty, txtPrice, txtTotal);

            productRowCount++;

            // Update container height
            productRowsContainerPanel.Height = productRowCount * (ROW_HEIGHT + ROW_MARGIN);

            // Cập nhật thành tiền ban đầu ngay sau khi thêm hàng
            UpdateRowTotal(nudQty, txtPrice, txtTotal);

            // Cập nhật trạng thái ComboBox nhà cung cấp sau khi tăng productRowCount
            UpdateNhaCungCapComboBoxState();

            // Scroll to bottom
            productSectionPanel.AutoScrollPosition = new Point(0, productRowsContainerPanel.Height);
        }

        private void RemoveProductRow(int rowY)
        {
            // Tìm index của hàng cần xóa dựa vào rowY
            int rowIndex = rowY / (ROW_HEIGHT + ROW_MARGIN);
            
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

            productRowCount--;

            // Dịch chuyển các hàng phía dưới lên
            foreach (Control ctrl in productRowsContainerPanel.Controls)
            {
                if (ctrl.Top > rowY)
                {
                    ctrl.Top -= (ROW_HEIGHT + ROW_MARGIN);
                }
            }

            // Update container height
            productRowsContainerPanel.Height = Math.Max(0, productRowCount * (ROW_HEIGHT + ROW_MARGIN));

            // Cập nhật tổng tiền
            UpdateGrandTotal();
            
            // Cập nhật trạng thái ComboBox nhà cung cấp
            UpdateNhaCungCapComboBoxState();
            
            // Force refresh panel
            productRowsContainerPanel.Refresh();
        }
        
        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // Tìm hàng chứa button này dựa vào vị trí Top
                int rowY = btn.Top - 4; // Trừ offset đã cộng khi tạo button
                RemoveProductRow(rowY);
            }
        }

        /// <summary>
        /// Cập nhật trạng thái enable/disable của ComboBox nhà cung cấp
        /// - Enable: Khi không có sản phẩm nào trong danh sách
        /// - Disable: Khi có ít nhất 1 sản phẩm trong danh sách
        /// </summary>
        private void UpdateNhaCungCapComboBoxState()
        {
            if (cboNhaCungCap != null)
            {
                // Kiểm tra số lượng sản phẩm trong danh sách
                bool hasProducts = productRowCount > 0;
                
                // Khóa combobox nếu có sản phẩm, mở khóa nếu không có sản phẩm
                cboNhaCungCap.Enabled = !hasProducts;
                
                // Đổi màu để người dùng dễ nhận biết
                if (hasProducts)
                {
                    cboNhaCungCap.BackColor = Color.FromArgb(240, 240, 240);
                    cboNhaCungCap.ForeColor = textSecondaryColor;
                }
                else
                {
                    cboNhaCungCap.BackColor = Color.White;
                    cboNhaCungCap.ForeColor = textPrimaryColor;
                }
            }
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

            // Duyệt qua từng hàng dựa vào productRowCount
            for (int i = 0; i < productRowCount; i++)
            {
                int expectedRowY = i * (ROW_HEIGHT + ROW_MARGIN);
                
                // Tìm TextBox thành tiền của hàng này
                foreach (Control ctrl in productRowsContainerPanel.Controls)
                {
                    if (ctrl.Top == expectedRowY &&
                        ctrl is TextBox txt && 
                        txt.ReadOnly && 
                        txt.Enabled == false &&
                        ctrl.Location.X == COL0_WIDTH + COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + COL4_WIDTH &&
                        !string.IsNullOrWhiteSpace(txt.Text))
                    {
                        // Parse the text, removing thousand separators
                        string cleanText = txt.Text.Replace(",", "").Replace(".", "").Trim();
                        if (decimal.TryParse(cleanText, out decimal rowTotal))
                        {
                            grandTotal += rowTotal;
                        }
                        break; // Tìm thấy TextBox thành tiền của hàng này rồi, chuyển sang hàng tiếp theo
                    }
                }
            }

            if (lblTongTien != null)
            {
                lblTongTien.Text = grandTotal.ToString("N0") + " đ";
            }
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
                            // txtProduct có ReadOnly = true và Location.X = COL0_WIDTH + 5 (sau cột mã SP)
                            if (txt.ReadOnly && ctrl.Location.X == COL0_WIDTH + 5 && txt.Tag != null)
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
                NgayNhap = dtpNgayNhap.Value.Date,
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
                TextBox? txtHSD = null;

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
                            // txtProduct: ReadOnly, Location.X = COL0_WIDTH + 5 (sau cột mã SP)
                            if (txt.ReadOnly && ctrl.Location.X == COL0_WIDTH + 5 && txt.Tag != null)
                                txtProduct = txt;
                            // txtPrice: không ReadOnly
                            else if (!txt.ReadOnly && txt.Enabled)
                                txtPrice = txt;
                            // txtHSD: ReadOnly và Enabled = false, ở vị trí cột 4
                            else if (txt.ReadOnly && !txt.Enabled && 
                                     ctrl.Location.X == COL0_WIDTH + COL1_WIDTH + COL2_WIDTH + COL3_WIDTH)
                                txtHSD = txt;
                            // txtTotal: ReadOnly và Enabled = false, ở vị trí cột 5 (sau HSD)
                            else if (txt.ReadOnly && !txt.Enabled && 
                                     ctrl.Location.X == COL0_WIDTH + COL1_WIDTH + COL2_WIDTH + COL3_WIDTH + COL4_WIDTH)
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

                    // Lấy HSD từ TextBox (đã lưu trong Tag)
                    DateTime? hsd = txtHSD?.Tag as DateTime?;

                    // Cập nhật HSD cho sản phẩm nếu có và khác với HSD hiện tại
                    if (hsd.HasValue)
                    {
                        var sanPhamBUS = new SanPham_BUS();
                        var sanPham = sanPhamBUS.GetSanPhamById(maSanPham);
                        if (sanPham != null && (!sanPham.Hsd.HasValue || sanPham.Hsd.Value != hsd.Value))
                        {
                            sanPham.Hsd = hsd.Value;
                            sanPhamBUS.UpdateSanPham(sanPham);
                        }
                    }

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