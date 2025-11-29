using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.QuanLy
{
    public partial class UC_LoiNhuan : UserControl
    {
        private LoiNhuan_BUS _loiNhuanBus;
        private SanPham_BUS _sanPhamBus;
        private LoaiSanPham_BUS _loaiBus;
        private ThuongHieu_BUS _thuongHieuBus;
        private DonVi_BUS _donViBus;

        // Controls
        private TabControl tabControlLoiNhuan;
        private TabPage tabCauHinhChung;
        private TabPage tabTheoLoai;
        private TabPage tabTheoThuongHieu;
        private TabPage tabTheoDonVi;
        private TabPage tabTheoSanPham;
        private TabPage tabXemTruoc;

        // Tab Cấu hình chung
        private NumericUpDown nudPhanTramMacDinh;
        private Button btnApDungToanBo;
        private Label lblPhanTramMacDinh;

        // DataGridViews cho các tab
        private DataGridView dgvTheoLoai;
        private DataGridView dgvTheoThuongHieu;
        private DataGridView dgvTheoDonVi;
        private DataGridView dgvTheoSanPham;
        private DataGridView dgvXemTruoc;

        public UC_LoiNhuan()
        {
            _loiNhuanBus = new LoiNhuan_BUS();
            _sanPhamBus = new SanPham_BUS();
            _loaiBus = new LoaiSanPham_BUS();
            _thuongHieuBus = new ThuongHieu_BUS();
            _donViBus = new DonVi_BUS();
            
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // TabControl chính
            tabControlLoiNhuan = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };

            // Tab 1: Cấu hình chung
            tabCauHinhChung = new TabPage
            {
                Text = "Cấu hình chung",
                Padding = new Padding(20)
            };
            InitializeTabCauHinhChung();
            tabControlLoiNhuan.TabPages.Add(tabCauHinhChung);

            // Tab 2: Theo Loại
            tabTheoLoai = new TabPage { Text = "Theo Loại", Padding = new Padding(10) };
            InitializeTabTheoLoai();
            tabControlLoiNhuan.TabPages.Add(tabTheoLoai);

            // Tab 3: Theo Thương hiệu
            tabTheoThuongHieu = new TabPage { Text = "Theo Thương hiệu", Padding = new Padding(10) };
            InitializeTabTheoThuongHieu();
            tabControlLoiNhuan.TabPages.Add(tabTheoThuongHieu);

            // Tab 4: Theo Đơn vị
            tabTheoDonVi = new TabPage { Text = "Theo Đơn vị", Padding = new Padding(10) };
            InitializeTabTheoDonVi();
            tabControlLoiNhuan.TabPages.Add(tabTheoDonVi);

            // Tab 5: Theo Sản phẩm
            tabTheoSanPham = new TabPage { Text = "Theo Sản phẩm", Padding = new Padding(10) };
            InitializeTabTheoSanPham();
            tabControlLoiNhuan.TabPages.Add(tabTheoSanPham);

            // Tab 6: Xem trước
            tabXemTruoc = new TabPage { Text = "Xem trước", Padding = new Padding(10) };
            InitializeTabXemTruoc();
            tabControlLoiNhuan.TabPages.Add(tabXemTruoc);

            this.Controls.Add(tabControlLoiNhuan);
        }

        private void InitializeTabCauHinhChung()
        {
            // Label
            lblPhanTramMacDinh = new Label
            {
                Text = "% Lợi nhuận mặc định:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 30)
            };
            tabCauHinhChung.Controls.Add(lblPhanTramMacDinh);

            // NumericUpDown
            nudPhanTramMacDinh = new NumericUpDown
            {
                Location = new Point(250, 28),
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 11),
                Minimum = 0,
                Maximum = 100,
                DecimalPlaces = 2,
                Increment = 0.5m
            };
            tabCauHinhChung.Controls.Add(nudPhanTramMacDinh);

            // Button
            btnApDungToanBo = new Button
            {
                Text = "Áp dụng cho toàn bộ kho",
                Size = new Size(200, 40),
                Location = new Point(20, 80),
                BackColor = Color.FromArgb(16, 137, 62),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnApDungToanBo.FlatAppearance.BorderSize = 0;
            btnApDungToanBo.Click += BtnApDungToanBo_Click;
            tabCauHinhChung.Controls.Add(btnApDungToanBo);
        }

        private void InitializeTabTheoLoai()
        {
            dgvTheoLoai = CreateDataGridView();
            dgvTheoLoai.Columns.Add("MaLoai", "Mã loại");
            dgvTheoLoai.Columns.Add("TenLoai", "Tên loại");
            dgvTheoLoai.Columns.Add("PhanTramLoiNhuan", "% Lợi nhuận");
            dgvTheoLoai.Columns["PhanTramLoiNhuan"].DefaultCellStyle.Format = "N2";
            dgvTheoLoai.CellEndEdit += (s, e) => DgvTheoLoai_CellEndEdit(s, e);
            tabTheoLoai.Controls.Add(dgvTheoLoai);
        }

        private void InitializeTabTheoThuongHieu()
        {
            dgvTheoThuongHieu = CreateDataGridView();
            dgvTheoThuongHieu.Columns.Add("MaThuongHieu", "Mã thương hiệu");
            dgvTheoThuongHieu.Columns.Add("TenThuongHieu", "Tên thương hiệu");
            dgvTheoThuongHieu.Columns.Add("PhanTramLoiNhuan", "% Lợi nhuận");
            dgvTheoThuongHieu.Columns["PhanTramLoiNhuan"].DefaultCellStyle.Format = "N2";
            dgvTheoThuongHieu.CellEndEdit += (s, e) => DgvTheoThuongHieu_CellEndEdit(s, e);
            tabTheoThuongHieu.Controls.Add(dgvTheoThuongHieu);
        }

        private void InitializeTabTheoDonVi()
        {
            dgvTheoDonVi = CreateDataGridView();
            dgvTheoDonVi.Columns.Add("MaDonVi", "Mã đơn vị");
            dgvTheoDonVi.Columns.Add("TenDonVi", "Tên đơn vị");
            dgvTheoDonVi.Columns.Add("PhanTramLoiNhuan", "% Lợi nhuận");
            dgvTheoDonVi.Columns["PhanTramLoiNhuan"].DefaultCellStyle.Format = "N2";
            dgvTheoDonVi.CellEndEdit += (s, e) => DgvTheoDonVi_CellEndEdit(s, e);
            tabTheoDonVi.Controls.Add(dgvTheoDonVi);
        }

        private void InitializeTabTheoSanPham()
        {
            dgvTheoSanPham = CreateDataGridView();
            dgvTheoSanPham.Columns.Add("MaSanPham", "Mã SP");
            dgvTheoSanPham.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvTheoSanPham.Columns.Add("GiaNhap", "Giá nhập");
            dgvTheoSanPham.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
            dgvTheoSanPham.Columns.Add("PhanTramLoiNhuan", "% Lợi nhuận");
            dgvTheoSanPham.Columns["PhanTramLoiNhuan"].DefaultCellStyle.Format = "N2";
            dgvTheoSanPham.Columns.Add("GiaBan", "Giá bán");
            dgvTheoSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            dgvTheoSanPham.CellEndEdit += (s, e) => DgvTheoSanPham_CellEndEdit(s, e);
            tabTheoSanPham.Controls.Add(dgvTheoSanPham);
        }

        private void InitializeTabXemTruoc()
        {
            dgvXemTruoc = CreateDataGridView();
            dgvXemTruoc.Columns.Add("MaSanPham", "Mã SP");
            dgvXemTruoc.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvXemTruoc.Columns.Add("Loai", "Loại");
            dgvXemTruoc.Columns.Add("ThuongHieu", "Thương hiệu");
            dgvXemTruoc.Columns.Add("DonVi", "Đơn vị");
            dgvXemTruoc.Columns.Add("GiaNhap", "Giá nhập");
            dgvXemTruoc.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
            dgvXemTruoc.Columns.Add("PhanTramLoiNhuan", "% Lợi nhuận");
            dgvXemTruoc.Columns["PhanTramLoiNhuan"].DefaultCellStyle.Format = "N2";
            dgvXemTruoc.Columns.Add("GiaBan", "Giá bán");
            dgvXemTruoc.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            dgvXemTruoc.Columns.Add("QuyTacApDung", "Quy tắc áp dụng");
            dgvXemTruoc.ReadOnly = true;
            tabXemTruoc.Controls.Add(dgvXemTruoc);
        }

        private DataGridView CreateDataGridView()
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = Color.White
            };
            return dgv;
        }

        private void LoadData()
        {
            LoadCauHinhChung();
            LoadTabTheoLoai();
            LoadTabTheoThuongHieu();
            LoadTabTheoDonVi();
            LoadTabTheoSanPham();
            LoadTabXemTruoc();
        }

        private void LoadCauHinhChung()
        {
            var cauHinh = _loiNhuanBus.GetCauHinh();
            if (cauHinh != null)
            {
                nudPhanTramMacDinh.Value = cauHinh.PhanTramLoiNhuanMacDinh;
            }
        }

        private void LoadTabTheoLoai()
        {
            dgvTheoLoai.Rows.Clear();
            var loaiList = _loaiBus.GetLoaiList();
            var quyTacList = _loiNhuanBus.GetQuyTacLoiNhuan("TheoLoai");
            
            // Lấy % lợi nhuận mặc định từ database (không dùng control)
            var cauHinh = _loiNhuanBus.GetCauHinh();
            decimal phanTramMacDinh = cauHinh?.PhanTramLoiNhuanMacDinh ?? 15.00m;

            foreach (var loai in loaiList)
            {
                var quyTac = quyTacList.FirstOrDefault(q => q.MaLoai == loai.MaLoai);
                dgvTheoLoai.Rows.Add(
                    loai.MaLoai,
                    loai.TenLoai,
                    quyTac?.PhanTramLoiNhuan ?? phanTramMacDinh
                );
            }
        }

        private void LoadTabTheoThuongHieu()
        {
            dgvTheoThuongHieu.Rows.Clear();
            var thuongHieuList = _thuongHieuBus.GetThuongHieuList();
            var quyTacList = _loiNhuanBus.GetQuyTacLoiNhuan("TheoThuongHieu");
            
            // Lấy % lợi nhuận mặc định từ database (không dùng control)
            var cauHinh = _loiNhuanBus.GetCauHinh();
            decimal phanTramMacDinh = cauHinh?.PhanTramLoiNhuanMacDinh ?? 15.00m;

            foreach (var th in thuongHieuList)
            {
                var quyTac = quyTacList.FirstOrDefault(q => q.MaThuongHieu == th.MaThuongHieu);
                dgvTheoThuongHieu.Rows.Add(
                    th.MaThuongHieu,
                    th.TenThuongHieu,
                    quyTac?.PhanTramLoiNhuan ?? phanTramMacDinh
                );
            }
        }

        private void LoadTabTheoDonVi()
        {
            dgvTheoDonVi.Rows.Clear();
            var donViList = _donViBus.GetDonViList();
            var quyTacList = _loiNhuanBus.GetQuyTacLoiNhuan("TheoDonVi");
            
            // Lấy % lợi nhuận mặc định từ database (không dùng control)
            var cauHinh = _loiNhuanBus.GetCauHinh();
            decimal phanTramMacDinh = cauHinh?.PhanTramLoiNhuanMacDinh ?? 15.00m;

            foreach (var dv in donViList)
            {
                var quyTac = quyTacList.FirstOrDefault(q => q.MaDonVi == dv.MaDonVi);
                dgvTheoDonVi.Rows.Add(
                    dv.MaDonVi,
                    dv.TenDonVi,
                    quyTac?.PhanTramLoiNhuan ?? phanTramMacDinh
                );
            }
        }

        private void LoadTabTheoSanPham()
        {
            dgvTheoSanPham.Rows.Clear();
            var khoHangList = _loiNhuanBus.GetAllKhoHangWithPrice();
            var quyTacList = _loiNhuanBus.GetQuyTacLoiNhuan("TheoSanPham");
            var sanPhamList = _sanPhamBus.GetAll();
            
            // Lấy % lợi nhuận mặc định từ database (không dùng control) - lấy một lần ở đầu
            var cauHinh = _loiNhuanBus.GetCauHinh();
            decimal phanTramMacDinh = cauHinh?.PhanTramLoiNhuanMacDinh ?? 15.00m;

            foreach (var kh in khoHangList)
            {
                var quyTac = quyTacList.FirstOrDefault(q => q.MaSanPham == kh.MaSanPham);
                var sp = sanPhamList.FirstOrDefault(s => s.MaSanPham == kh.MaSanPham);
                
                decimal giaNhap = kh.GiaNhap ?? 0;
                decimal phanTram = quyTac?.PhanTramLoiNhuan ?? phanTramMacDinh;
                
                // Nếu chưa có giá nhập (chưa có phiếu nhập), lấy giá bán từ Tbl_SanPham
                // Nếu có giá nhập, tính giá bán từ giá nhập và % lợi nhuận
                decimal giaBan;
                if (giaNhap > 0)
                {
                    // Có giá nhập → tính giá bán từ giá nhập (làm tròn đến 2 chữ số thập phân)
                    giaBan = Math.Round(giaNhap * (1 + phanTram / 100), 2, MidpointRounding.AwayFromZero);
                }
                else if (sp != null && sp.GiaBan.HasValue && sp.GiaBan.Value > 0)
                {
                    // Chưa có giá nhập nhưng có giá bán trong Tbl_SanPham → dùng giá bán đó
                    giaBan = sp.GiaBan.Value;
                    // Tính ngược lại giá nhập từ giá bán (để hiển thị, làm tròn đến 2 chữ số thập phân)
                    // GiaBan = GiaNhap * (1 + PhanTram/100) => GiaNhap = GiaBan / (1 + PhanTram/100)
                    giaNhap = Math.Round(giaBan / (1 + phanTram / 100), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    // Chưa có cả giá nhập và giá bán
                    giaBan = 0;
                }

                dgvTheoSanPham.Rows.Add(
                    kh.MaSanPham,
                    kh.TenSanPham ?? "",
                    giaNhap,
                    phanTram,
                    giaBan
                );
            }
        }

        private void LoadTabXemTruoc()
        {
            dgvXemTruoc.Rows.Clear();
            var khoHangList = _loiNhuanBus.GetAllKhoHangWithPrice();
            var sanPhamList = _sanPhamBus.GetAll();
            
            // Lấy % lợi nhuận mặc định từ database (không dùng control)
            var cauHinh = _loiNhuanBus.GetCauHinh();
            decimal phanTramMacDinh = cauHinh?.PhanTramLoiNhuanMacDinh ?? 15.00m;

            foreach (var kh in khoHangList)
            {
                var sp = sanPhamList.FirstOrDefault(s => s.MaSanPham == kh.MaSanPham);
                if (sp == null) continue;

                var quyTac = _loiNhuanBus.GetQuyTacApDungChoSanPham(kh.MaSanPham);

                decimal giaNhap = kh.GiaNhap ?? 0;
                decimal phanTram = quyTac?.PhanTramLoiNhuan ?? phanTramMacDinh;
                
                // LUÔN tính giá bán từ giá nhập và % lợi nhuận (không dùng giá bán cũ từ database)
                // Để đảm bảo hiển thị đúng giá bán mới nhất sau khi cập nhật % lợi nhuận
                decimal giaBan;
                if (giaNhap > 0)
                {
                    // Có giá nhập → tính giá bán từ giá nhập và % lợi nhuận mới nhất (làm tròn đến 2 chữ số thập phân)
                    giaBan = Math.Round(giaNhap * (1 + phanTram / 100), 2, MidpointRounding.AwayFromZero);
                }
                else if (sp.GiaBan.HasValue && sp.GiaBan.Value > 0)
                {
                    // Chưa có giá nhập nhưng có giá bán trong Tbl_SanPham → dùng giá bán đó
                    // (Giá bán này đã được cập nhật bởi ApDungLoiNhuanChoToanBoKho nếu có giá nhập)
                    giaBan = sp.GiaBan.Value;
                    // Tính ngược lại giá nhập từ giá bán (để hiển thị, làm tròn đến 2 chữ số thập phân)
                    // GiaBan = GiaNhap * (1 + PhanTram/100) => GiaNhap = GiaBan / (1 + PhanTram/100)
                    giaNhap = Math.Round(giaBan / (1 + phanTram / 100), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    // Chưa có cả giá nhập và giá bán
                    giaBan = 0;
                }
                
                string quyTacApDung = quyTac?.LoaiQuyTac ?? "Chung";

                dgvXemTruoc.Rows.Add(
                    kh.MaSanPham,
                    kh.TenSanPham ?? "",
                    sp.TenLoai ?? "",
                    sp.TenThuongHieu ?? "",
                    sp.TenDonVi ?? "",
                    giaNhap,
                    phanTram,
                    giaBan,
                    quyTacApDung
                );
            }
        }

        private void BtnApDungToanBo_Click(object? sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn áp dụng % lợi nhuận cho toàn bộ kho hàng?\n" +
                    "Hành động này sẽ cập nhật giá bán cho tất cả sản phẩm.",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // UpdateCauHinh() đã tự động gọi ApDungLoiNhuanChoToanBoKho() với forceUpdate=true
                        _loiNhuanBus.UpdateCauHinh(nudPhanTramMacDinh.Value, 1); // TODO: Lấy từ session

                        MessageBox.Show("Áp dụng lợi nhuận cho toàn bộ kho thành công!\n\n" +
                            "Giá bán đã được cập nhật cho tất cả sản phẩm có giá nhập.", 
                            "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reload lại tất cả dữ liệu
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}\n\nChi tiết: {ex.StackTrace}", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTheoLoai_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0) // Cột PhanTramLoiNhuan
            {
                try
                {
                    int maLoai = (int)dgvTheoLoai.Rows[e.RowIndex].Cells[0].Value;
                    decimal phanTram = Convert.ToDecimal(dgvTheoLoai.Rows[e.RowIndex].Cells[2].Value);

                    var quyTac = new QuyTacLoiNhuanDTO
                    {
                        LoaiQuyTac = "TheoLoai",
                        MaLoai = maLoai,
                        PhanTramLoiNhuan = phanTram,
                        MaNhanVien = 1 // TODO: Lấy từ session
                    };

                    var existing = _loiNhuanBus.GetQuyTacLoiNhuan("TheoLoai")
                        .FirstOrDefault(q => q.MaLoai == maLoai);

                    if (existing != null)
                    {
                        quyTac.MaQuyTac = existing.MaQuyTac;
                        _loiNhuanBus.UpdateQuyTac(quyTac);
                    }
                    else
                    {
                        _loiNhuanBus.AddQuyTac(quyTac);
                    }

                    // Cập nhật giá bán cho tất cả sản phẩm thuộc loại này
                    _loiNhuanBus.CapNhatGiaBanTheoLoai(maLoai);

                    LoadTabXemTruoc();
                    LoadTabTheoSanPham(); // Reload để hiển thị giá mới
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadTabTheoLoai();
                }
            }
        }

        private void DgvTheoThuongHieu_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                try
                {
                    int maThuongHieu = (int)dgvTheoThuongHieu.Rows[e.RowIndex].Cells[0].Value;
                    decimal phanTram = Convert.ToDecimal(dgvTheoThuongHieu.Rows[e.RowIndex].Cells[2].Value);

                    var quyTac = new QuyTacLoiNhuanDTO
                    {
                        LoaiQuyTac = "TheoThuongHieu",
                        MaThuongHieu = maThuongHieu,
                        PhanTramLoiNhuan = phanTram,
                        MaNhanVien = 1
                    };

                    var existing = _loiNhuanBus.GetQuyTacLoiNhuan("TheoThuongHieu")
                        .FirstOrDefault(q => q.MaThuongHieu == maThuongHieu);

                    if (existing != null)
                    {
                        quyTac.MaQuyTac = existing.MaQuyTac;
                        _loiNhuanBus.UpdateQuyTac(quyTac);
                    }
                    else
                    {
                        _loiNhuanBus.AddQuyTac(quyTac);
                    }

                    // Cập nhật giá bán cho tất cả sản phẩm thuộc thương hiệu này
                    _loiNhuanBus.CapNhatGiaBanTheoThuongHieu(maThuongHieu);

                    LoadTabXemTruoc();
                    LoadTabTheoSanPham(); // Reload để hiển thị giá mới
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadTabTheoThuongHieu();
                }
            }
        }

        private void DgvTheoDonVi_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                try
                {
                    int maDonVi = (int)dgvTheoDonVi.Rows[e.RowIndex].Cells[0].Value;
                    decimal phanTram = Convert.ToDecimal(dgvTheoDonVi.Rows[e.RowIndex].Cells[2].Value);

                    var quyTac = new QuyTacLoiNhuanDTO
                    {
                        LoaiQuyTac = "TheoDonVi",
                        MaDonVi = maDonVi,
                        PhanTramLoiNhuan = phanTram,
                        MaNhanVien = 1
                    };

                    var existing = _loiNhuanBus.GetQuyTacLoiNhuan("TheoDonVi")
                        .FirstOrDefault(q => q.MaDonVi == maDonVi);

                    if (existing != null)
                    {
                        quyTac.MaQuyTac = existing.MaQuyTac;
                        _loiNhuanBus.UpdateQuyTac(quyTac);
                    }
                    else
                    {
                        _loiNhuanBus.AddQuyTac(quyTac);
                    }

                    // Cập nhật giá bán cho tất cả sản phẩm thuộc đơn vị này
                    _loiNhuanBus.CapNhatGiaBanTheoDonVi(maDonVi);

                    LoadTabXemTruoc();
                    LoadTabTheoSanPham(); // Reload để hiển thị giá mới
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadTabTheoDonVi();
                }
            }
        }

        private void DgvTheoSanPham_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    int maSanPham = (int)dgvTheoSanPham.Rows[e.RowIndex].Cells[0].Value;

                    if (e.ColumnIndex == 2) // Cột Giá nhập
                    {
                        decimal giaNhapMoi = Convert.ToDecimal(dgvTheoSanPham.Rows[e.RowIndex].Cells[2].Value);
                        _loiNhuanBus.CapNhatGiaBanKhiGiaNhapThayDoi(maSanPham, giaNhapMoi);
                    }
                    else if (e.ColumnIndex == 3) // Cột PhanTramLoiNhuan
                    {
                        decimal phanTram = Convert.ToDecimal(dgvTheoSanPham.Rows[e.RowIndex].Cells[3].Value);
                        decimal giaNhap = Convert.ToDecimal(dgvTheoSanPham.Rows[e.RowIndex].Cells[2].Value);
                        decimal giaBan = giaNhap * (1 + phanTram / 100);

                        var quyTac = new QuyTacLoiNhuanDTO
                        {
                            LoaiQuyTac = "TheoSanPham",
                            MaSanPham = maSanPham,
                            PhanTramLoiNhuan = phanTram,
                            MaNhanVien = 1
                        };

                        var existing = _loiNhuanBus.GetQuyTacLoiNhuan("TheoSanPham")
                            .FirstOrDefault(q => q.MaSanPham == maSanPham);

                        if (existing != null)
                        {
                            quyTac.MaQuyTac = existing.MaQuyTac;
                            _loiNhuanBus.UpdateQuyTac(quyTac);
                        }
                        else
                        {
                            _loiNhuanBus.AddQuyTac(quyTac);
                        }

                        // Cập nhật giá bán trong Tbl_SanPham thông qua BUS
                        _loiNhuanBus.CapNhatGiaBanChoSanPham(maSanPham, giaBan);

                        // Cập nhật giá bán trong DataGridView
                        dgvTheoSanPham.Rows[e.RowIndex].Cells[4].Value = giaBan;
                    }

                    LoadTabXemTruoc();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadTabTheoSanPham();
                }
            }
        }
    }
}

