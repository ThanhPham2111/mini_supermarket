using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using ClosedXML.Excel;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.PhieuNhap
{
    public partial class Form_PhieuNhap : Form
    {
        // Layout controls
        private Panel panelMain;
        private Panel panelHeader;
        private Panel panelFilters;
        private GroupBox groupBoxGrid;
        private TableLayoutPanel tblFilters;
        private FlowLayoutPanel headerActions;

        // Functional controls
        private DataGridView dgvPhieuNhap;
        private TextBox txtSearch;
        private ComboBox cboTimePeriod, cboSupplier, cboTrangThai;
        private Button btnAddImport, btnClear, btnImportExcel;

        // Data binding
        private readonly BindingSource _bindingSource = new();
        private BindingList<PhieuNhapDisplayDTO> _currentPhieuNhap = new();

        public Form_PhieuNhap()
        {
            InitializeComponent();
            Load += Form_PhieuNhap_Load;
        }

        private void InitializeComponent()
        {
            this.Text = "Chi tiết phiếu nhập";
            this.Size = new Size(1170, 750);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.WhiteSmoke; // Match FormKhoHang

            InitializeLayout();
        }

        private void Form_PhieuNhap_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            dgvPhieuNhap.AutoGenerateColumns = false;
            dgvPhieuNhap.DataSource = _bindingSource;

            LoadPhieuNhapData();
            _bindingSource.DataSource = _currentPhieuNhap;
        }

        private void InitializeLayout()
        {
            // 1. Main Panel
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(12)
            };
            this.Controls.Add(panelMain);

            // Thứ tự thêm controls với Dock rất quan trọng:
            // - Controls với Dock.Fill nên được thêm TRƯỚC
            // - Controls với Dock.Top được thêm SAU (theo thứ tự ngược từ dưới lên)
            
            // 1. Grid Section (Fill) - Thêm trước để fill phần còn lại
            InitializeGridSection();



            // 3. Header Section (Top) - Thêm cuối cùng, sẽ nằm trên cùng
            InitializeHeaderSection();
                        // 2. Filter Section (Top) - Thêm sau, sẽ nằm phía trên Grid
            InitializeFilterSection();
        }

        private void InitializeHeaderSection()
        {
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = Color.White,
                Padding = new Padding(12, 10, 12, 10)
            };
            panelMain.Controls.Add(panelHeader);

            // Actions Panel
            headerActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            panelHeader.Controls.Add(headerActions);

            // Buttons
            btnAddImport = CreateButton("➕ Thêm", Color.FromArgb(16, 137, 62)); // Green
            btnAddImport.Click += BtnAddImport_Click;
            
            btnClear = CreateButton("🔄 Làm mới", Color.FromArgb(0, 120, 215)); // Blue
            btnClear.Click += BtnClear_Click;

            btnImportExcel = CreateButton("📥 Nhập Excel", Color.FromArgb(0, 120, 215)); // Blue
            btnImportExcel.Click += BtnImportExcel_Click;

            headerActions.Controls.Add(btnAddImport);
            headerActions.Controls.Add(btnClear);
            headerActions.Controls.Add(btnImportExcel);
        }

        private void InitializeFilterSection()
        {
            panelFilters = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100, // Adjusted height
                BackColor = Color.White,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 12) // Spacing below
            };
            panelMain.Controls.Add(panelFilters);

            // Table Layout
            tblFilters = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 2,
                Padding = new Padding(0)
            };
            
            // Column Styles
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Label
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));   // Control
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Label
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));   // Control
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Label
            tblFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));   // Control

            // Row Styles
            tblFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

            panelFilters.Controls.Add(tblFilters);

            // 1. Time Period
            Label lblTime = new Label { Text = "Thời gian:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 7f) };
            cboTimePeriod = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cboTimePeriod.Items.AddRange(new[] { "Tất cả", "Hôm nay", "Tuần này", "Tháng này" });
            cboTimePeriod.SelectedIndex = 0;
            cboTimePeriod.SelectedIndexChanged += (s, e) => ApplyFilters();

            tblFilters.Controls.Add(lblTime, 0, 0);
            tblFilters.Controls.Add(cboTimePeriod, 1, 0);

            // 2. Supplier
            Label lblSupplier = new Label { Text = "NCC:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 7f) };
            cboSupplier = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            // Items will be loaded later
            cboSupplier.SelectedIndexChanged += (s, e) => ApplyFilters();

            tblFilters.Controls.Add(lblSupplier, 2, 0);
            tblFilters.Controls.Add(cboSupplier, 3, 0);

            // 3. Trạng thái
            Label lblTrangThai = new Label { Text = "Trạng thái:", Anchor = AnchorStyles.Left, AutoSize = true };
            cboTrangThai = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cboTrangThai.Items.AddRange(new[] { "Tất cả", "Nhập thành công", "Đang nhập", "Đã hủy" });
            cboTrangThai.SelectedIndex = 0;
            cboTrangThai.SelectedIndexChanged += (s, e) => ApplyFilters();

            tblFilters.Controls.Add(lblTrangThai, 4, 0);
            tblFilters.Controls.Add(cboTrangThai, 5, 0);

            // 4. Search
            Label lblSearch = new Label { Text = "Tìm kiếm:", Anchor = AnchorStyles.Left, AutoSize = true };
            txtSearch = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10), PlaceholderText = "Tìm kiếm theo mã phiếu..." };
            txtSearch.TextChanged += (s, e) => 
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text)) ApplyFilters();
                else PerformSearch();
            };

            tblFilters.Controls.Add(lblSearch, 0, 1);
            tblFilters.Controls.Add(txtSearch, 1, 1);
            tblFilters.SetColumnSpan(txtSearch, 5); // Span across remaining columns

            LoadNhaCungCapFilter();
        }

        private void InitializeGridSection()
        {
            groupBoxGrid = new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = "Danh sách phiếu nhập",
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White,
                Padding = new Padding(8)
            };
            panelMain.Controls.Add(groupBoxGrid);

            dgvPhieuNhap = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 35 }
            };

            ConfigureDataGridViewStyle();
            AddDataGridViewColumns();
            AddDataGridViewEvents();

            groupBoxGrid.Controls.Add(dgvPhieuNhap);
        }

        private Button CreateButton(string text, Color bgColor)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(120, 35),
                BackColor = bgColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 8, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }


        private void BtnAddImport_Click(object? sender, EventArgs e)
        {
            Form_ChiTietPhieuNhap formChiTiet = new Form_ChiTietPhieuNhap();
            DialogResult result = formChiTiet.ShowDialog();
            
            // Nếu thêm thành công, reload lại data
            if (result == DialogResult.OK)
            {
                LoadPhieuNhapData();
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cboTimePeriod.SelectedIndex = 0;
            cboSupplier.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            _bindingSource.DataSource = _currentPhieuNhap;
        }

        private void BtnImportExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                using var openDialog = new OpenFileDialog
                {
                    Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx",
                    Title = "Chọn file Excel để nhập",
                    Multiselect = false
                };

                if (openDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                ImportFromExcel(openDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nhập file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportFromExcel(string filePath)
        {
            try
            {
                using var workbook = new XLWorkbook(filePath);
                var ws = workbook.Worksheet(1);

                string nhaCungCap = string.Empty;
                DateTime? ngayNhap = null;
                var sanPhamList = new List<(string tenSP, string donVi, int soLuong, decimal donGia, decimal thanhTien)>();

                int headerRowIndex = -1;
                int colTenSP = -1, colDonVi = -1, colSoLuong = -1, colDonGia = -1, colThanhTien = -1;

                foreach (var row in ws.RowsUsed())
                {
                    var first = row.Cell(1).GetString().Trim();
                    var second = row.Cell(2).GetString().Trim();

                    if (first.Equals("NCC", StringComparison.OrdinalIgnoreCase))
                    {
                        nhaCungCap = second;
                        continue;
                    }

                    if (first.Equals("Ngày nhập", StringComparison.OrdinalIgnoreCase))
                    {
                        if (row.Cell(2).TryGetValue<DateTime>(out var parsedDate))
                        {
                            ngayNhap = parsedDate;
                        }
                        else if (DateTime.TryParse(second, out var parsedDate2))
                        {
                            ngayNhap = parsedDate2;
                        }
                        continue;
                    }

                    var titles = row.CellsUsed().ToDictionary(c => c.Address.ColumnNumber, c => c.GetString().Trim());
                    bool looksLikeHeader = titles.Values.Any(v => v.Contains("sản phẩm", StringComparison.OrdinalIgnoreCase))
                                          && titles.Values.Any(v => v.Contains("đơn", StringComparison.OrdinalIgnoreCase));

                    if (looksLikeHeader)
                    {
                        foreach (var kv in titles)
                        {
                            var title = kv.Value;
                            if (title.Contains("sản phẩm", StringComparison.OrdinalIgnoreCase)) colTenSP = kv.Key;
                            else if (title.Contains("đơn vị", StringComparison.OrdinalIgnoreCase)) colDonVi = kv.Key;
                            else if (title.Contains("số lượng", StringComparison.OrdinalIgnoreCase)) colSoLuong = kv.Key;
                            else if (title.Contains("đơn giá", StringComparison.OrdinalIgnoreCase)) colDonGia = kv.Key;
                            else if (title.Contains("thành tiền", StringComparison.OrdinalIgnoreCase)) colThanhTien = kv.Key;
                        }

                        headerRowIndex = row.RowNumber();

                        if (colTenSP < 0) colTenSP = 1;
                        if (colDonVi < 0) colDonVi = 2;
                        if (colSoLuong < 0) colSoLuong = 3;
                        if (colDonGia < 0) colDonGia = 4;
                        if (colThanhTien < 0) colThanhTien = 5;
                        break;
                    }
                }

                if (headerRowIndex == -1)
                {
                    MessageBox.Show("Không tìm thấy bảng sản phẩm trong file Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int currentRow = headerRowIndex + 1;
                while (true)
                {
                    var nameCell = ws.Cell(currentRow, colTenSP);
                    var unitCell = ws.Cell(currentRow, colDonVi);
                    var qtyCell = ws.Cell(currentRow, colSoLuong);
                    var priceCell = ws.Cell(currentRow, colDonGia);
                    var totalCell = ws.Cell(currentRow, colThanhTien);

                    string nameVal = nameCell.GetString().Trim();
                    string unitVal = unitCell.GetString().Trim();

                    bool isEmptyRow = string.IsNullOrWhiteSpace(nameVal) && string.IsNullOrWhiteSpace(unitVal) && qtyCell.IsEmpty();
                    bool isTotalRow = nameVal.Contains("tổng", StringComparison.OrdinalIgnoreCase) || priceCell.GetString().Trim().Contains("tổng", StringComparison.OrdinalIgnoreCase);

                    if (isEmptyRow || isTotalRow)
                    {
                        break;
                    }

                    int soLuong = qtyCell.TryGetValue<int>(out var qtyVal) ? qtyVal : ParseIntFallback(qtyCell.GetString());
                    decimal donGia = priceCell.TryGetValue<decimal>(out var priceVal) ? priceVal : ParseDecimalFallback(priceCell.GetString());
                    decimal thanhTien = totalCell.TryGetValue<decimal>(out var totalVal) ? totalVal : ParseDecimalFallback(totalCell.GetString());

                    if (thanhTien == 0 && donGia != 0 && soLuong != 0)
                    {
                        thanhTien = donGia * soLuong;
                    }

                    sanPhamList.Add((nameVal, unitVal, soLuong, donGia, thanhTien));

                    currentRow++;
                }

                if (sanPhamList.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu sản phẩm trong file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string message = $"Đã đọc được:\n" +
                               $"- NCC: {nhaCungCap}\n" +
                               $"- Số lượng sản phẩm: {sanPhamList.Count}\n\n" +
                               "Bạn có muốn nhập phiếu nhập này vào hệ thống?";

                if (MessageBox.Show(message, "Xác nhận nhập dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveImportedData(nhaCungCap, ngayNhap ?? DateTime.Now, sanPhamList);
                    LoadPhieuNhapData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ParseIntFallback(string input)
        {
            if (int.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
            {
                return val;
            }
            if (int.TryParse(input, NumberStyles.Any, CultureInfo.GetCultureInfo("vi-VN"), out var val2))
            {
                return val2;
            }
            return 0;
        }

        private decimal ParseDecimalFallback(string input)
        {
            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
            {
                return val;
            }
            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.GetCultureInfo("vi-VN"), out var val2))
            {
                return val2;
            }
            return 0m;
        }

        private void SaveImportedData(string tenNhaCungCap, DateTime ngayNhap,
            List<(string tenSP, string donVi, int soLuong, decimal donGia, decimal thanhTien)> sanPhamList)
        {
            try
            {
                var nhaCungCapBUS = new NhaCungCap_BUS();
                var sanPhamBUS = new SanPham_BUS();
                var phieuNhapBUS = new PhieuNhap_BUS();

                var nhaCungCapList = nhaCungCapBUS.GetAll();
                var nhaCungCap = nhaCungCapList.FirstOrDefault(ncc => ncc.TenNhaCungCap == tenNhaCungCap);

                if (nhaCungCap == null)
                {
                    MessageBox.Show($"Không tìm thấy nhà cung cấp '{tenNhaCungCap}' trong hệ thống!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var allSanPham = sanPhamBUS.GetAll();
                List<ChiTietPhieuNhapDTO> chiTietList = new List<ChiTietPhieuNhapDTO>();
                decimal tongTien = 0;

                foreach (var sp in sanPhamList)
                {
                    var sanPham = allSanPham.FirstOrDefault(s => s.TenSanPham == sp.tenSP);

                    if (sanPham != null)
                    {
                        chiTietList.Add(new ChiTietPhieuNhapDTO
                        {
                            MaSanPham = sanPham.MaSanPham,
                            SoLuong = sp.soLuong,
                            DonGiaNhap = sp.donGia,
                            ThanhTien = sp.thanhTien
                        });

                        tongTien += sp.thanhTien;
                    }
                    else
                    {
                        MessageBox.Show($"Sản phẩm '{sp.tenSP}' không tồn tại trong hệ thống!\nSẽ bỏ qua sản phẩm này.",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (chiTietList.Count == 0)
                {
                    MessageBox.Show("Không có sản phẩm hợp lệ để nhập!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                PhieuNhapDTO phieuNhap = new PhieuNhapDTO
                {
                    MaNhaCungCap = nhaCungCap.MaNhaCungCap,
                    NgayNhap = ngayNhap,
                    TongTien = tongTien,
                    ChiTietPhieuNhaps = chiTietList
                };

                var result = phieuNhapBUS.AddPhieuNhap(phieuNhap);

                if (result != null && result.MaPhieuNhap > 0)
                {
                    MessageBox.Show($"Nhập phiếu nhập thành công!\n" +
                                  $"- Mã phiếu: PN{result.MaPhieuNhap:D3}\n" +
                                  $"- Số sản phẩm: {chiTietList.Count}\n" +
                                  $"- Tổng tiền: {tongTien:N0} đ",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lỗi khi lưu phiếu nhập vào database!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPhieuNhapData()
        {
            try
            {
                var phieuNhapBUS = new PhieuNhap_BUS();
                var nhaCungCapBUS = new NhaCungCap_BUS();
                
                var phieuNhapList = phieuNhapBUS.GetPhieuNhap();
                var nhaCungCapList = nhaCungCapBUS.GetAll();

                var displayList = new List<PhieuNhapDisplayDTO>();
                
                foreach (var phieuNhap in phieuNhapList)
                {
                    var nhaCungCap = nhaCungCapList.FirstOrDefault(ncc => ncc.MaNhaCungCap == phieuNhap.MaNhaCungCap);
                    string tenNhaCungCap = nhaCungCap?.TenNhaCungCap ?? "N/A";
                    string trangThai = phieuNhap.TrangThai == "Hủy" ? "Đã hủy" : (phieuNhap.TrangThai ?? "Đang nhập");

                    displayList.Add(new PhieuNhapDisplayDTO
                    {
                        MaPhieuNhap = phieuNhap.MaPhieuNhap,
                        MaPhieu = $"PN{phieuNhap.MaPhieuNhap:D3}",
                        NgayNhap = phieuNhap.NgayNhap?.ToString("dd/MM/yyyy") ?? "N/A",
                        NhaCungCap = tenNhaCungCap,
                        TongTien = phieuNhap.TongTien ?? 0,
                        TrangThai = trangThai,
                        MaNhaCungCap = phieuNhap.MaNhaCungCap,
                        NgayNhapValue = phieuNhap.NgayNhap
                    });
                }

                _currentPhieuNhap = new BindingList<PhieuNhapDisplayDTO>(displayList);
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformSearch()
        {
            ApplyFilters();
        }

        private void LoadNhaCungCapFilter()
        {
            try
            {
                var nhaCungCapBUS = new NhaCungCap_BUS();
                var nhaCungCapList = nhaCungCapBUS.GetNhaCungCap(NhaCungCap_BUS.StatusActive);
                
                cboSupplier.Items.Clear();
                cboSupplier.Items.Add("🏢 Nhà cung cấp");
                cboSupplier.Items.Add("Tất cả");
                
                foreach (var ncc in nhaCungCapList)
                {
                    cboSupplier.Items.Add(ncc.TenNhaCungCap);
                }
                
                cboSupplier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải nhà cung cấp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            try
            {
                // Kiểm tra null
                if (cboTimePeriod == null || cboSupplier == null || dgvPhieuNhap == null || cboTrangThai == null)
                    return;

                string searchText = txtSearch?.Text.Trim().ToLower() ?? string.Empty;
                var filtered = new List<PhieuNhapDisplayDTO>();

                foreach (var phieuNhap in _currentPhieuNhap)
                {
                    bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                        phieuNhap.MaPhieu.ToLower().Contains(searchText) ||
                        (phieuNhap.NhaCungCap?.ToLower().Contains(searchText) ?? false) ||
                        (phieuNhap.NgayNhap?.ToLower().Contains(searchText) ?? false);

                    // Filter theo thời gian
                    bool matchesTime = true;
                    if (cboTimePeriod.SelectedIndex > 0 && phieuNhap.NgayNhapValue.HasValue)
                    {
                        DateTime today = DateTime.Now.Date;
                        DateTime phieuDate = phieuNhap.NgayNhapValue.Value.Date;

                        matchesTime = cboTimePeriod.SelectedIndex switch
                        {
                            1 => phieuDate == today,
                            2 => phieuDate >= today.AddDays(-(int)today.DayOfWeek) && phieuDate <= today,
                            3 => phieuDate.Month == today.Month && phieuDate.Year == today.Year,
                            _ => true
                        };
                    }

                    // Filter theo nhà cung cấp
                    bool matchesSupplier = true;
                    if (cboSupplier.SelectedIndex > 1)
                    {
                        string selectedSupplier = cboSupplier.SelectedItem?.ToString() ?? string.Empty;
                        matchesSupplier = phieuNhap.NhaCungCap == selectedSupplier;
                    }

                    // Filter theo trạng thái
                    bool matchesStatus = true;
                    if (cboTrangThai.SelectedIndex > 0)
                    {
                        string selectedStatus = cboTrangThai.SelectedItem?.ToString() ?? string.Empty;
                        matchesStatus = phieuNhap.TrangThai == selectedStatus;
                    }

                    if (matchesSearch && matchesTime && matchesSupplier && matchesStatus)
                    {
                        filtered.Add(phieuNhap);
                    }
                }

                _bindingSource.DataSource = filtered;
                dgvPhieuNhap.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridViewStyle()
        {
            // Column Headers Style
            dgvPhieuNhap.EnableHeadersVisualStyles = false;
            dgvPhieuNhap.ColumnHeadersHeight = 45;
            dgvPhieuNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 243, 245);
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 58, 64);
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);

            // Row Style
            dgvPhieuNhap.RowsDefaultCellStyle.BackColor = Color.White;
            dgvPhieuNhap.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvPhieuNhap.DefaultCellStyle.SelectionBackColor = Color.FromArgb(207, 226, 255);
            dgvPhieuNhap.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgvPhieuNhap.DefaultCellStyle.Padding = new Padding(5);
            dgvPhieuNhap.DefaultCellStyle.ForeColor = Color.FromArgb(73, 80, 87);
            dgvPhieuNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgvPhieuNhap.RowTemplate.Height = 35;
        }

        private void AddDataGridViewColumns()
        {
            // Mã phiếu
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPhieu",
                HeaderText = "Mã phiếu",
                DataPropertyName = "MaPhieu",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(13, 110, 253),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Ngày nhập
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayNhap",
                HeaderText = "Ngày nhập",
                DataPropertyName = "NgayNhap",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Nhà cung cấp
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NhaCungCap",
                HeaderText = "Nhà cung cấp",
                DataPropertyName = "NhaCungCap",
                Width = 400,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                }
            });

            // Tổng tiền
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TongTien",
                HeaderText = "Tổng tiền (VNĐ)",
                DataPropertyName = "TongTien",
                Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(220, 53, 69),
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Padding = new Padding(0, 0, 10, 0)
                }
            });

            // Trạng thái
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
        }

        private void AddDataGridViewEvents()
        {
            // Double click to view details
            dgvPhieuNhap.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    string maPhieuStr = dgvPhieuNhap.Rows[e.RowIndex].Cells["MaPhieu"].Value?.ToString() ?? "";
                    
                    // Parse mã phiếu nhập (ví dụ: "PN001" -> 1)
                    if (maPhieuStr.StartsWith("PN") && int.TryParse(maPhieuStr.Substring(2), out int maPhieuNhap))
                    {
                        Form_XemChiTietPhieuNhap formChiTiet = new Form_XemChiTietPhieuNhap(maPhieuNhap);
                        formChiTiet.ShowDialog();
                    }
                }
            };

            // Right click context menu
            dgvPhieuNhap.CellMouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    dgvPhieuNhap.ClearSelection();
                    dgvPhieuNhap.Rows[e.RowIndex].Selected = true;
                    
                    ContextMenuStrip menu = new ContextMenuStrip();
                    
                    // Lấy trạng thái của phiếu nhập
                    string trangThai = dgvPhieuNhap.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString() ?? "";
                    
                    // Chỉ hiển thị "Xác nhận nhập kho" nếu trạng thái là "Đang nhập"
                    if (trangThai == "Đang nhập")
                    {
                        ToolStripMenuItem xacNhanItem = new ToolStripMenuItem("✅ Xác nhận nhập kho");
                        xacNhanItem.Click += (sender, args) => XacNhanNhapKho_Click(e.RowIndex);
                        menu.Items.Add(xacNhanItem);
                        menu.Items.Add(new ToolStripSeparator());
                    }
                    
                    ToolStripMenuItem viewItem = new ToolStripMenuItem("👁️ Xem chi tiết");
                    viewItem.Click += (sender, args) => ViewDetail_Click(e.RowIndex);
                    menu.Items.Add(viewItem);
                    
                    // Nếu đã hủy, cho phép xem lý do hủy
                    if (trangThai == "Hủy" || trangThai == "Đã hủy")
                    {
                        menu.Items.Add(new ToolStripSeparator());
                        ToolStripMenuItem reasonItem = new ToolStripMenuItem("ℹ️ Lý do hủy");
                        reasonItem.Click += (sender, args) => XemLyDoHuy_Click(e.RowIndex);
                        menu.Items.Add(reasonItem);
                    }
                    
                    // Cho phép hủy nếu trạng thái là "Đang nhập" hoặc "Nhập thành công"
                    if (trangThai == "Đang nhập" || trangThai == "Nhập thành công")
                    {
                        menu.Items.Add(new ToolStripSeparator());
                        ToolStripMenuItem huyItem = new ToolStripMenuItem("❌ Hủy phiếu nhập");
                        huyItem.Click += (sender, args) => HuyPhieuNhap_Click(e.RowIndex);
                        menu.Items.Add(huyItem);
                    }
                    
                    menu.Show(dgvPhieuNhap, dgvPhieuNhap.PointToClient(Cursor.Position));
                }
            };

            // Cell formatting for status colors
            dgvPhieuNhap.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == dgvPhieuNhap.Columns["TrangThai"].Index && e.RowIndex >= 0)
                {
                    string trangThai = e.Value?.ToString() ?? "";
                    
                    if (trangThai == "Đang nhập")
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 243, 205); // Vàng nhạt
                        e.CellStyle.ForeColor = Color.FromArgb(133, 100, 4);   // Vàng đậm
                    }
                    else if (trangThai == "Nhập thành công")
                    {
                        e.CellStyle.BackColor = Color.FromArgb(209, 250, 229); // Xanh lá nhạt
                        e.CellStyle.ForeColor = Color.FromArgb(21, 128, 61);   // Xanh lá đậm
                    }
                    else if (trangThai == "Hủy" || trangThai == "Đã hủy")
                    {
                        e.CellStyle.BackColor = Color.FromArgb(248, 215, 218); // Đỏ nhạt
                        e.CellStyle.ForeColor = Color.FromArgb(114, 28, 36);   // Đỏ đậm
                    }
                }
            };
        }

        private void XacNhanNhapKho_Click(int rowIndex)
        {
            try
            {
                string maPhieuNhapStr = dgvPhieuNhap.Rows[rowIndex].Cells["MaPhieu"].Value?.ToString() ?? "";
                int maPhieuNhap = int.Parse(maPhieuNhapStr.Replace("PN", ""));
                
                DialogResult result = MessageBox.Show(
                    $"Xác nhận nhập kho cho phiếu {maPhieuNhapStr}?\n\n" +
                    "Sau khi xác nhận, số lượng sản phẩm sẽ được cập nhật vào kho và không thể hoàn tác!",
                    "Xác nhận nhập kho",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                
                if (result == DialogResult.Yes)
                {
                    var phieuNhapBUS = new PhieuNhap_BUS();
                    phieuNhapBUS.XacNhanNhapKho(maPhieuNhap);
                    
                    MessageBox.Show(
                        "Xác nhận nhập kho thành công!\nSố lượng sản phẩm đã được cập nhật vào kho.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    
                    LoadPhieuNhapData(); // Reload để cập nhật trạng thái
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi xác nhận nhập kho: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ViewDetail_Click(int rowIndex)
        {
            try
            {
                string maPhieuStr = dgvPhieuNhap.Rows[rowIndex].Cells["MaPhieu"].Value?.ToString() ?? "";
                
                if (maPhieuStr.StartsWith("PN") && int.TryParse(maPhieuStr.Substring(2), out int maPhieuNhap))
                {
                    Form_XemChiTietPhieuNhap formChiTiet = new Form_XemChiTietPhieuNhap(maPhieuNhap);
                    formChiTiet.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HuyPhieuNhap_Click(int rowIndex)
        {
            try
            {
                string maPhieuStr = dgvPhieuNhap.Rows[rowIndex].Cells["MaPhieu"].Value?.ToString() ?? "";
                string trangThai = dgvPhieuNhap.Rows[rowIndex].Cells["TrangThai"].Value?.ToString() ?? "";
                
                // Kiểm tra trạng thái
                if (trangThai == "Hủy" || trangThai == "Đã hủy")
                {
                    MessageBox.Show(
                        "Phiếu nhập này đã được hủy trước đó!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                
                if (maPhieuStr.StartsWith("PN") && 
                    int.TryParse(maPhieuStr.Substring(2), out int maPhieuNhap))
                {
                    // Hiển thị dialog nhập lý do
                    Dialog_HuyPhieuNhap dialog = new Dialog_HuyPhieuNhap(maPhieuStr);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var phieuNhapBUS = new PhieuNhap_BUS();
                        phieuNhapBUS.HuyPhieuNhap(maPhieuNhap, dialog.LyDoHuy);
                        
                        MessageBox.Show("Hủy phiếu nhập thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        ApplyFilters(); // Reload với filter hiện tại
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hủy phiếu nhập: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XemLyDoHuy_Click(int rowIndex)
        {
            try
            {
                string maPhieuStr = dgvPhieuNhap.Rows[rowIndex].Cells["MaPhieu"].Value?.ToString() ?? "";
                
                if (maPhieuStr.StartsWith("PN") &&
                    int.TryParse(maPhieuStr.Substring(2), out int maPhieuNhap))
                {
                    var phieuNhapBUS = new PhieuNhap_BUS();
                    var phieu = phieuNhapBUS.GetPhieuNhapById(maPhieuNhap);
                    string lyDo = phieu?.LyDoHuy ?? "Không có lý do hủy.";
                    
                    MessageBox.Show(lyDo, $"Lý do hủy {maPhieuStr}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem lý do hủy: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DTO for display in DataGridView
        private class PhieuNhapDisplayDTO
        {
            public int MaPhieuNhap { get; set; }
            public string MaPhieu { get; set; } = string.Empty;
            public string? NgayNhap { get; set; }
            public string? NhaCungCap { get; set; }
            public decimal TongTien { get; set; }
            public string? TrangThai { get; set; }
            public int MaNhaCungCap { get; set; }
            public DateTime? NgayNhapValue { get; set; }
        }
    }
}
