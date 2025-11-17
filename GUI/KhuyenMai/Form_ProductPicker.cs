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
    public class Form_ProductPicker : Form
    {
        private readonly SanPham_BUS _sanPhamBus = new();
        private List<SanPhamDTO> _items = new();
        private DataGridView dgvProducts;
        private TextBox txtSearch;
        private Button btnOk;
        private Button btnCancel;

        public int? SelectedProductId { get; private set; }

        public Form_ProductPicker()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void InitializeComponent()
        {
            this.Text = "Chọn sản phẩm";
            this.Size = new Size(640, 460);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            txtSearch = new TextBox { Location = new Point(12, 12), Size = new Size(440, 26), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            txtSearch.PlaceholderText = "Tìm theo mã hoặc tên...";
            txtSearch.TextChanged += (s, e) => ApplyFilter();
            this.Controls.Add(txtSearch);

            btnOk = new Button { Text = "Chọn", Location = new Point(460, 10), Size = new Size(80, 28), BackColor = Color.FromArgb(25,135,84), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnOk.Click += BtnOk_Click;
            this.Controls.Add(btnOk);

            btnCancel = new Button { Text = "Đóng", Location = new Point(548, 10), Size = new Size(80, 28), BackColor = Color.FromArgb(108,117,125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);

            dgvProducts = new DataGridView
            {
                Location = new Point(12, 48),
                Size = new Size(616, 360),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã SP", DataPropertyName = "MaSanPham", Width = 60 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên sản phẩm", DataPropertyName = "TenSanPham", Width = 280 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đơn vị", DataPropertyName = "TenDonVi", Width = 120 });
            var giaCol = new DataGridViewTextBoxColumn { HeaderText = "Giá bán", DataPropertyName = "GiaBan", Width = 156 };
            dgvProducts.Columns.Add(giaCol);

            dgvProducts.CellDoubleClick += DgvProducts_CellDoubleClick;
            this.Controls.Add(dgvProducts);
        }

        private void LoadProducts()
        {
            try
            {
                _items = _sanPhamBus.GetAll().ToList();
                dgvProducts.DataSource = new BindingList<SanPhamDTO>(_items);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            string q = txtSearch.Text?.Trim() ?? string.Empty;
            IEnumerable<SanPhamDTO> filtered = _items;
            if (!string.IsNullOrEmpty(q))
            {
                filtered = _items.Where(p => (p.TenSanPham ?? string.Empty).IndexOf(q, StringComparison.CurrentCultureIgnoreCase) >= 0
                                              || p.MaSanPham.ToString().IndexOf(q, StringComparison.CurrentCultureIgnoreCase) >= 0);
            }
            dgvProducts.DataSource = new BindingList<SanPhamDTO>(filtered.ToList());
        }

        private void DgvProducts_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvProducts.Rows[e.RowIndex].DataBoundItem is SanPhamDTO sp)
            {
                SelectedProductId = sp.MaSanPham;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is SanPhamDTO sp)
            {
                SelectedProductId = sp.MaSanPham;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
