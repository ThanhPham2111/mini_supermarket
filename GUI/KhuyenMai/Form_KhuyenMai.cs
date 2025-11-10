using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.KhuyenMai
{
    public partial class Form_KhuyenMai : Form
    {
    private Panel? mainPanel;
    private DataGridView? dgv;
    private ComboBox? cboFilterProduct;
    private TextBox? txtSearch;
    private ComboBox? cboMaSanPham; // for input selection of product
    private TextBox? txtTenKhuyenMai;
    private NumericUpDown? nudPhanTram;
    private DateTimePicker? dtpBatDau;
    private DateTimePicker? dtpKetThuc;
    private TextBox? txtMoTa;
    private Button? btnAdd;
    private Button? btnUpdate;
    private Button? btnDelete;
    private Button? btnClear;

        private readonly KhuyenMai_BUS _bus = new();

        public Form_KhuyenMai()
        {
            InitializeComponent();
            LoadData();
        }
        private readonly SanPham_BUS _sanPhamBus = new();
        private List<SanPhamDTO>? _productList;

        private void InitializeComponent()
        {
            this.Text = "Quản lý Khuyến mãi";
            this.Size = new Size(1280, 820);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(248, 249, 250);

            mainPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1280, 820),
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            this.Controls.Add(mainPanel);

            // Title
            Label title = new Label
            {
                Text = "Danh sách Khuyến mãi",
                Location = new Point(20, 10),
                Size = new Size(500, 40),
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41)
            };
            mainPanel.Controls.Add(title);


            // Filter and search controls 
            var lblFilter = CreateLabel("Sản phẩm:", new Point(460, 30), new Size(70, 22));
            mainPanel.Controls.Add(lblFilter);

            cboFilterProduct = new ComboBox { Location = new Point(530, 28), Size = new Size(220, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            cboFilterProduct.SelectedIndexChanged += (s, e) => LoadData(GetSelectedFilterProductId(), txtSearch?.Text);
            mainPanel.Controls.Add(cboFilterProduct);

            var lblSearch = CreateLabel("Tìm kiếm:", new Point(760, 30), new Size(70, 22));
            mainPanel.Controls.Add(lblSearch);

            txtSearch = new TextBox { Location = new Point(830, 28), Size = new Size(230, 24) };
            txtSearch.TextChanged += (s, e) => LoadData(GetSelectedFilterProductId(), txtSearch.Text);
            mainPanel.Controls.Add(txtSearch);

            // Left panel: inputs
            Panel inputPanel = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(430, 600), // widened to the right
                BackColor = Color.White
            };
            mainPanel.Controls.Add(inputPanel);

            int lblX = 10, lblW = 120, ctrlX = 140, ctrlW = 260, y = 10, h = 28, gap = 42; // increased control width

            inputPanel.Controls.Add(CreateLabel("Sản phẩm:", new Point(lblX, y + 4), new Size(lblW, h)));
            cboMaSanPham = new ComboBox { Location = new Point(ctrlX, y), Size = new Size(ctrlW, h), DropDownStyle = ComboBoxStyle.DropDownList };
            inputPanel.Controls.Add(cboMaSanPham);

            y += gap;
            inputPanel.Controls.Add(CreateLabel("Tên khuyến mãi:", new Point(lblX, y + 4), new Size(lblW, h)));
            txtTenKhuyenMai = new TextBox { Location = new Point(ctrlX, y), Size = new Size(ctrlW, h) };
            inputPanel.Controls.Add(txtTenKhuyenMai);

            y += gap;
            inputPanel.Controls.Add(CreateLabel("% giảm giá:", new Point(lblX, y + 4), new Size(lblW, h)));
            nudPhanTram = new NumericUpDown { Location = new Point(ctrlX, y), Size = new Size(120, h) };
            nudPhanTram.DecimalPlaces = 2;
            nudPhanTram.Maximum = 100;
            nudPhanTram.Minimum = 0;
            nudPhanTram.Increment = 1;
            inputPanel.Controls.Add(nudPhanTram);

            y += gap;
            inputPanel.Controls.Add(CreateLabel("Ngày bắt đầu:", new Point(lblX, y + 4), new Size(lblW, h)));
            dtpBatDau = new DateTimePicker { Location = new Point(ctrlX, y), Size = new Size(200, h), Format = DateTimePickerFormat.Short }; 
            inputPanel.Controls.Add(dtpBatDau);

            y += gap;
            inputPanel.Controls.Add(CreateLabel("Ngày kết thúc:", new Point(lblX, y + 4), new Size(lblW, h)));
            dtpKetThuc = new DateTimePicker { Location = new Point(ctrlX, y), Size = new Size(200, h), Format = DateTimePickerFormat.Short };
            inputPanel.Controls.Add(dtpKetThuc);

            y += gap;
            inputPanel.Controls.Add(CreateLabel("Mô tả:", new Point(lblX, y + 4), new Size(lblW, h)));
            txtMoTa = new TextBox { Location = new Point(ctrlX, y), Size = new Size(ctrlW, 120), Multiline = true, ScrollBars = ScrollBars.Vertical };
            inputPanel.Controls.Add(txtMoTa);

            y += 140;
            btnClear = CreateButton("Làm mới", new Point(140, y), new Size(120, 36), Color.FromArgb(108, 117, 125));
            btnClear.Click += BtnClear_Click;
            inputPanel.Controls.Add(btnClear);

            // Place action buttons under the "Làm mới" button
            int btnY = y + 60;
            btnAdd = CreateButton("Thêm", new Point(40, btnY), new Size(100, 36), Color.FromArgb(25, 135, 84));
            btnAdd.Click += BtnAdd_Click;
            inputPanel.Controls.Add(btnAdd);

            btnUpdate = CreateButton("Sửa", new Point(150, btnY), new Size(100, 36), Color.FromArgb(13, 202, 240));
            btnUpdate.Click += BtnUpdate_Click;
            inputPanel.Controls.Add(btnUpdate);

            btnDelete = CreateButton("Xóa", new Point(260, btnY), new Size(100, 36), Color.FromArgb(220, 53, 69));
            btnDelete.Click += BtnDelete_Click;
            inputPanel.Controls.Add(btnDelete);

            // Right panel: grid
            dgv = new DataGridView
            {
                Location = new Point(450, 60),
                Size = new Size(750, 680),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false
            };

            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "MãKM", DataPropertyName = "MaKhuyenMai", Width = 60 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "MãSP", DataPropertyName = "MaSanPham", Width = 60 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên KM", DataPropertyName = "TenKhuyenMai", Width = 180 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "% giảm", DataPropertyName = "PhanTramGiamGia", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày bắt đầu", DataPropertyName = "NgayBatDau", Width = 110 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày kết thúc", DataPropertyName = "NgayKetThuc", Width = 110 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mô tả", DataPropertyName = "MoTa", Width = 200 });

            dgv.CellClick += Dgv_CellClick;
            mainPanel.Controls.Add(dgv);
        }

        private Button CreateButton(string text, Point location, Size size, Color back)
        {
            return new Button
            {
                Text = text,
                Location = location,
                Size = size,
                BackColor = back,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }

        private Label CreateLabel(string text, Point location, Size size)
        {
            return new Label
            {
                Text = text,
                Location = location,
                Size = size,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
        }

        private void LoadData()
        {
            // Load products for combo boxes then load grid
            LoadProducts();
            LoadData((int?)null, null);
        }

        private void LoadData(int? maSanPhamFilter, string? search)
        {
            try
            {
                IList<KhuyenMaiDTO> data = _bus.GetKhuyenMai(maSanPhamFilter);
                if (!string.IsNullOrWhiteSpace(search))
                {
                    string s = search.Trim();
                    data = new List<KhuyenMaiDTO>(data).FindAll(k => (k.TenKhuyenMai ?? string.Empty).IndexOf(s, StringComparison.CurrentCultureIgnoreCase) >= 0
                                                                              || (k.MoTa ?? string.Empty).IndexOf(s, StringComparison.CurrentCultureIgnoreCase) >= 0);
                }
                var binding = new BindingList<KhuyenMaiDTO>(data as List<KhuyenMaiDTO> ?? new List<KhuyenMaiDTO>(data));
                dgv!.DataSource = new BindingSource { DataSource = binding };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                _productList = _sanPhamBus.GetAll().ToList();

                // populate filter combobox with a "Tất cả" entry
                var filterList = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(0, "--Tất cả--") };
                foreach (var p in _productList)
                {
                    filterList.Add(new KeyValuePair<int, string>(p.MaSanPham, p.TenSanPham));
                }
                cboFilterProduct!.DataSource = filterList;
                cboFilterProduct.DisplayMember = "Value";
                cboFilterProduct.ValueMember = "Key";
                cboFilterProduct.SelectedIndex = 0;

                // populate input product combobox
                cboMaSanPham!.DataSource = _productList;
                cboMaSanPham.DisplayMember = "TenSanPham";
                cboMaSanPham.ValueMember = "MaSanPham";
                cboMaSanPham.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                // non-fatal: show message but allow the form to continue
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int? GetSelectedFilterProductId()
        {
            if (cboFilterProduct == null) return null;
            if (cboFilterProduct.SelectedItem is KeyValuePair<int, string> kv)
            {
                return kv.Key == 0 ? null : kv.Key;
            }
            // if DataSource is a list of integers or products, try ValueMember
            if (cboFilterProduct.SelectedValue is int v)
            {
                return v == 0 ? null : (int?)v;
            }
            return null;
        }

        private void Dgv_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgv!.Rows[e.RowIndex].DataBoundItem is KhuyenMaiDTO kh)
            {
                // select product in product combobox
                try { cboMaSanPham!.SelectedValue = kh.MaSanPham; } catch { /* ignore if not present */ }
                txtTenKhuyenMai!.Text = kh.TenKhuyenMai ?? string.Empty;
                nudPhanTram!.Value = kh.PhanTramGiamGia ?? 0;
                dtpBatDau!.Value = kh.NgayBatDau ?? DateTime.Now;
                dtpKetThuc!.Value = kh.NgayKetThuc ?? DateTime.Now;
                txtMoTa!.Text = kh.MoTa ?? string.Empty;
                // store selected id in Tag of grid for convenience
                dgv!.Tag = kh.MaKhuyenMai;
            }
        }

    private void BtnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                var kh = MapInputsToDto(isUpdate: false);
                var added = _bus.AddKhuyenMai(kh);
                MessageBox.Show("Thêm khuyến mãi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể thêm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgv!.Tag == null) { MessageBox.Show("Chọn khuyến mãi để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                var kh = MapInputsToDto(isUpdate: true);
                _bus.UpdateKhuyenMai(kh);
                MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgv!.Tag == null) { MessageBox.Show("Chọn khuyến mãi để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                int id = Convert.ToInt32(dgv!.Tag);
                if (MessageBox.Show("Bạn có chắc muốn xóa khuyến mãi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                _bus.DeleteKhuyenMai(id);
                MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgv!.Tag = null;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            cboMaSanPham!.SelectedIndex = -1;
            txtTenKhuyenMai!.Clear();
            nudPhanTram!.Value = 0;
            dtpBatDau!.Value = DateTime.Now;
            dtpKetThuc!.Value = DateTime.Now;
            txtMoTa!.Clear();
            dgv!.Tag = null;
        }

        private KhuyenMaiDTO MapInputsToDto(bool isUpdate)
        {
            int maSanPham;
            if (cboMaSanPham?.SelectedValue == null || !int.TryParse(cboMaSanPham.SelectedValue.ToString(), out maSanPham) || maSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm phải là số nguyên > 0.");

            var kh = new KhuyenMaiDTO
            {
                MaSanPham = maSanPham,
                TenKhuyenMai = string.IsNullOrWhiteSpace(txtTenKhuyenMai!.Text) ? null : txtTenKhuyenMai.Text.Trim(),
                PhanTramGiamGia = nudPhanTram!.Value,
                NgayBatDau = dtpBatDau!.Value.Date,
                NgayKetThuc = dtpKetThuc!.Value.Date,
                MoTa = string.IsNullOrWhiteSpace(txtMoTa!.Text) ? null : txtMoTa.Text.Trim()
            };

            if (isUpdate)
            {
                if (dgv!.Tag == null) throw new InvalidOperationException("Không có khuyến mãi được chọn để cập nhật.");
                kh.MaKhuyenMai = Convert.ToInt32(dgv!.Tag);
            }

            return kh;
        }
    }
}
