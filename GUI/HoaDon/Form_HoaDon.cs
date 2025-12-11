#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using mini_supermarket.BUS;
using mini_supermarket.DTO;
using mini_supermarket.Common;

namespace mini_supermarket.GUI.HoaDon
{
    public partial class Form_HoaDon : Form
    {
        private readonly HoaDon_BUS _hoaDonBus = new();
        private readonly BindingSource _bindingSource = new();
        private BindingList<HoaDonDTO> _hoaDonList = new();
        private ContextMenuStrip _contextMenu;
        public Form_HoaDon()
        {
            InitializeComponent();
            Load += Form_HoaDon_Load;
            
            LoadHoaDonData();
        }

        private void Form_HoaDon_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            searchComboBox.SelectedIndex = 0;
            // Populate trạng thái combo box
            trangThaiComboBox.Items.Clear();
            trangThaiComboBox.Items.Add("Tất cả");
            trangThaiComboBox.Items.Add("Đã xuất");
            trangThaiComboBox.Items.Add("Đã hủy");
            trangThaiComboBox.SelectedIndex = 0; // Mặc định chọn "Tất cả"
            hoaDonDataGridView.AutoGenerateColumns = false;
            hoaDonDataGridView.DataSource = _bindingSource;

            // hoaDonDataGridView.SelectionChanged += hoaDonDataGridView_SelectionChanged;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(timKiemButton, "Tìm kiếm hóa đơn");
            toolTip.SetToolTip(xemChiTietButton, "Xem chi tiết hóa đơn đã chọn");
            toolTip.SetToolTip(lamMoiButton, "Làm mới danh sách");
            toolTip.SetToolTip(themFileButton, "Import Excel");
            toolTip.SetToolTip(xuatFileButton, "Export Excel");

            timKiemButton.Click += timKiemButton_Click;
            trangThaiComboBox.SelectedIndexChanged += trangThaiComboBox_SelectedIndexChanged;
            hoaDonDataGridView.CellFormatting += hoaDonDataGridView_CellFormatting;
            hoaDonDataGridView.MouseClick += hoaDonDataGridView_MouseClick;
            
            // Tạo ContextMenu
            _contextMenu = new ContextMenuStrip();
            var menuItemLyDoHuy = new ToolStripMenuItem("Xem lý do hủy");
            menuItemLyDoHuy.Click += MenuItemLyDoHuy_Click;
            _contextMenu.Items.Add(menuItemLyDoHuy);
            // xemChiTietButton.Click += xemChiTietButton_Click;
            // lamMoiButton.Click += lamMoiButton_Click;
            // themFileButton.Click += themFileButton_Click;
            // xuatFileButton.Click += xuatFileButton_Click;

            timKiemButton.Enabled = true;
            xemChiTietButton.Enabled = false;
            lamMoiButton.Enabled = true;
        }

        private void LoadHoaDonData()
        {
            try
            {
                _hoaDonList.Clear();
                
                // Thêm items mới vào BindingList hiện tại để giữ kết nối với BindingSource
                var newData = _hoaDonBus.GetHoaDon();
                foreach (var item in newData)
                {
                    _hoaDonList.Add(item);
                }
                
                hoaDonDataGridView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể tải danh sách hóa đơn.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void hoaDonDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (hoaDonDataGridView.SelectedRows.Count > 0)
            {
                xemChiTietButton.Enabled = true;
            }
            else
            {
                xemChiTietButton.Enabled = false;
            }
        }

        private void lamMoiButton_Click(object sender, EventArgs e)
        {
            // Reload toàn bộ dữ liệu
            _bindingSource.DataSource = _hoaDonList;
            hoaDonDataGridView.ClearSelection();

            // Clear các controls tìm kiếm
            searchTextBox.Clear();
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker2.Value = DateTime.Today;
            dateTimePicker3.Value = DateTime.Today;
            trangThaiComboBox.SelectedIndex = 0; // Reset về "Tất cả"
        }

        private void timKiemButton_Click(object sender, EventArgs e)
        {
            // 1. Xử lý Mã hóa đơn, nhân viên, khách hàng
            if (searchComboBox.SelectedIndex == 0)
            {
                // Tìm kiếm theo Mã hóa đơn
                string maHoaDon = searchTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(maHoaDon))
                {
                    _bindingSource.DataSource = new BindingList<HoaDonDTO>(_hoaDonList.Where(h => h.MaHoaDon.ToString().Contains(maHoaDon)).ToList());
                }
                else
                {
                    _bindingSource.DataSource = _hoaDonList;
                }
            }
            else if (searchComboBox.SelectedIndex == 1)
            {
                // Tìm kiếm theo Nhân viên
                string nhanVien = searchTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(nhanVien))
                {
                    _bindingSource.DataSource = new BindingList<HoaDonDTO>(_hoaDonList.Where(h => h.NhanVien.ToString().ToLower().Contains(nhanVien.ToLower())).ToList());
                }
                else
                {
                    _bindingSource.DataSource = _hoaDonList;
                }
            }
            else if (searchComboBox.SelectedIndex == 2)
            {
                // Tìm kiếm theo Khách hàng
                string khachHang = searchTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(khachHang))
                {
                    _bindingSource.DataSource = new BindingList<HoaDonDTO>(_hoaDonList.Where(h => h.KhachHang.ToString().ToLower().Contains(khachHang.ToLower())).ToList());
                }
                else
                {
                    _bindingSource.DataSource = _hoaDonList;
                }
            }

            // 2. Xử lý thời gian (chỉ lọc khi cần thiết)
            DateTime fromDate = dateTimePicker2.Value.Date;
            DateTime toDate = dateTimePicker3.Value.Date;

            Console.WriteLine($"FromDate: {fromDate.Date}, ToDate: {toDate.Date}");
            // Lọc theo khoảng thời gian nếu fromDate <= toDate
            if (fromDate <= toDate)
            {
                var currentData = (BindingList<HoaDonDTO>)_bindingSource.DataSource;
                var filteredByDate = currentData.Where(h =>
                    h.NgayLap.HasValue &&
                    h.NgayLap.Value.Date >= fromDate &&
                    h.NgayLap.Value.Date <= toDate).ToList();
                _bindingSource.DataSource = new BindingList<HoaDonDTO>(filteredByDate);
            }

            // 3. Xử lý tổng tiền (chỉ lọc khi có giá trị trong cả 2 textbox)
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()) || !string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                decimal fromAmount = 0;
                decimal toAmount = decimal.MaxValue;

                // Parse từ textBox1 (từ)
                if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    if (!decimal.TryParse(textBox1.Text.Trim(), out fromAmount))
                        fromAmount = 0;
                }

                // Parse từ textBox2 (đến)
                if (!string.IsNullOrEmpty(textBox2.Text.Trim()))
                {
                    if (!decimal.TryParse(textBox2.Text.Trim(), out toAmount))
                        toAmount = decimal.MaxValue;
                }

                var currentData = (BindingList<HoaDonDTO>)_bindingSource.DataSource;

                // Debug: In thông tin lọc
                Console.WriteLine($"Lọc theo số tiền: {fromAmount:N0} - {toAmount:N0}");
                Console.WriteLine($"Tổng số hóa đơn hiện tại: {currentData.Count}");

                var filteredByAmount = currentData.Where(h =>
                    h.TongTien.HasValue &&
                    h.TongTien.Value >= fromAmount &&
                    h.TongTien.Value <= toAmount).ToList();

                Console.WriteLine($"Số hóa đơn sau khi lọc theo tiền: {filteredByAmount.Count}");

                // Debug: In một vài giá trị TongTien để kiểm tra
                foreach (var hd in currentData.Take(3))
                {
                    Console.WriteLine($"Hóa đơn {hd.MaHoaDon}: TongTien = {hd.TongTien}");
                }

                _bindingSource.DataSource = new BindingList<HoaDonDTO>(filteredByAmount);
            }

            // 4. Xử lý lọc theo trạng thái
            if (trangThaiComboBox.SelectedIndex > 0) // Nếu không chọn "Tất cả"
            {
                var currentData = (BindingList<HoaDonDTO>)_bindingSource.DataSource;
                string selectedTrangThai = trangThaiComboBox.SelectedItem?.ToString();
                
                if (!string.IsNullOrEmpty(selectedTrangThai))
                {
                    var filteredByTrangThai = currentData.Where(h => 
                        h.TrangThai != null && h.TrangThai.Equals(selectedTrangThai, StringComparison.OrdinalIgnoreCase)).ToList();
                    
                    Console.WriteLine($"Lọc theo trạng thái '{selectedTrangThai}': {filteredByTrangThai.Count} hóa đơn");
                    _bindingSource.DataSource = new BindingList<HoaDonDTO>(filteredByTrangThai);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                _bindingSource.DataSource = _hoaDonList;
            }
            else
            {
                // You can implement live search here if needed
                timKiemButton_Click(sender, e);
            }
        }

        private void searchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                _bindingSource.DataSource = _hoaDonList;
            }
            else
            {
                // You can implement live search here if needed
                timKiemButton_Click(sender, e);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            timKiemButton_Click(sender, e);
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            timKiemButton_Click(sender, e);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                _bindingSource.DataSource = _hoaDonList;
            }
            else
            {
                // You can implement live search here if needed
                timKiemButton_Click(sender, e);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                _bindingSource.DataSource = _hoaDonList;
            }
            else
            {
                // You can implement live search here if needed
                timKiemButton_Click(sender, e);
            }

        }

        private void xemChiTietButton_Click(object sender, EventArgs e)
        {
            var selectedHoaDon = hoaDonDataGridView.SelectedRows[0].DataBoundItem as HoaDonDTO;
            var dialog = new Form_HoaDonDialog(selectedHoaDon);
            dialog.ShowDialog(this);
        }

        private void exportFileButton_Click(object sender, EventArgs e)
        {
            // Implement export to Excel functionality here
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Lưu Excel"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            System.Data.DataTable dt = new System.Data.DataTable();

            // Lấy header
            foreach (DataGridViewColumn col in hoaDonDataGridView.Columns)
                dt.Columns.Add(col.HeaderText);

            // Lấy dữ liệu
            foreach (DataGridViewRow row in hoaDonDataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                var data = new object[row.Cells.Count];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].ValueType == typeof(DateTime))
                        data[i] = ((DateTime)row.Cells[i].Value).ToString("yyyy-MM-dd HH:mm");
                    else
                    data[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(data);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Hóa Đơn");
                wb.SaveAs(sfd.FileName);
            }

            MessageBox.Show("✅ Xuất Excel thành công!");
        }

        private void importFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Chọn file Excel"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                List<HoaDonDTO> importedHoaDons = new List<HoaDonDTO>();

                using (XLWorkbook wb = new XLWorkbook(ofd.FileName))
                {
                    var ws = wb.Worksheet(1);
                    var rows = ws.RowsUsed().Skip(1); // Bỏ qua header

                    foreach (var row in rows)
                    {
                        try
                        {
                            // Lấy giá trị từ các cell và debug
                            var maHoaDonStr = row.Cell(1).Value.ToString().Trim();
                            var ngayLapStr = row.Cell(2).Value.ToString().Trim();
                            var nhanVienStr = row.Cell(3).Value.ToString().Trim();
                            var khachHangStr = row.Cell(4).Value.ToString().Trim();
                            var tongTienStr = row.Cell(5).Value.ToString().Trim();

                            Console.WriteLine($"Dòng {row.RowNumber()}: MaHD='{maHoaDonStr}', NgayLap='{ngayLapStr}', NV='{nhanVienStr}', KH='{khachHangStr}', TongTien='{tongTienStr}'");

                            // Kiểm tra dòng có dữ liệu hợp lệ không
                            if (string.IsNullOrEmpty(maHoaDonStr) || maHoaDonStr == "0")
                            {
                                Console.WriteLine($"Bỏ qua dòng {row.RowNumber()}: MaHoaDon trống hoặc không hợp lệ");
                                continue;
                            }

                            // Parse an toàn
                            if (!int.TryParse(maHoaDonStr, out int maHoaDon) || maHoaDon <= 0)
                            {
                                Console.WriteLine($"Không thể parse MaHoaDon hoặc <= 0: '{maHoaDonStr}'");
                                continue;
                            }

                            // Kiểm tra xem MaHoaDon đã tồn tại chưa
                            if (_hoaDonList.Any(h => h.MaHoaDon == maHoaDon))
                            {
                                Console.WriteLine($"MaHoaDon {maHoaDon} đã tồn tại, bỏ qua");
                                continue;
                            }

                            var hoaDon = new HoaDonDTO
                            {
                                MaHoaDon = maHoaDon,
                                NgayLap = DateTime.TryParse(ngayLapStr, out DateTime ngayLap) ? ngayLap : (DateTime?)null,
                                NhanVien = nhanVienStr,
                                KhachHang = khachHangStr,
                                TongTien = decimal.TryParse(tongTienStr?.Replace(",", ""), out decimal tongTien) ? tongTien : (decimal?)null
                            };

                            importedHoaDons.Add(hoaDon);
                            Console.WriteLine($"✅ Đã thêm hóa đơn {hoaDon.MaHoaDon}");
                        }
                        catch (Exception rowEx)
                        {
                            Console.WriteLine($"❌ Lỗi khi đọc dòng {row.RowNumber()}: {rowEx.Message}");
                        }
                    }
                }

                // Thêm dữ liệu đã import vào danh sách hiện tại
                foreach (var hoaDon in importedHoaDons)
                {
                    _hoaDonList.Add(hoaDon);
                }

                // Refresh DataGridView
                _bindingSource.DataSource = _hoaDonList;

                MessageBox.Show($"✅ Nhập Excel thành công! Đã thêm {importedHoaDons.Count} hóa đơn.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi nhập Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trangThaiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gọi hàm tìm kiếm khi trạng thái thay đổi
            timKiemButton_Click(sender, e);
        }

        private void hoaDonDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (hoaDonDataGridView.Rows[e.RowIndex].DataBoundItem is HoaDonDTO hoaDon)
            {
                // Kiểm tra nếu trạng thái là "Đã hủy"
                if (hoaDon.TrangThai != null && hoaDon.TrangThai.Equals("Đã hủy", StringComparison.OrdinalIgnoreCase))
                {
                    // Đổi màu nền của cả hàng thành đỏ nhạt
                    hoaDonDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    hoaDonDataGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else
                {
                    // Reset về màu mặc định
                    hoaDonDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    hoaDonDataGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void hoaDonDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = hoaDonDataGridView.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0 && hitTest.RowIndex < hoaDonDataGridView.Rows.Count)
                {
                    hoaDonDataGridView.ClearSelection();
                    hoaDonDataGridView.Rows[hitTest.RowIndex].Selected = true;

                    var hoaDon = hoaDonDataGridView.Rows[hitTest.RowIndex].DataBoundItem as HoaDonDTO;
                    if (hoaDon != null && hoaDon.TrangThai == "Đã hủy")
                    {
                        // Chỉ hiển thị context menu nếu hóa đơn đã hủy
                        _contextMenu.Show(hoaDonDataGridView, e.Location);
                    }
                }
            }
        }

        private void MenuItemLyDoHuy_Click(object sender, EventArgs e)
        {
            if (hoaDonDataGridView.SelectedRows.Count == 0)
                return;

            var selectedHoaDon = hoaDonDataGridView.SelectedRows[0].DataBoundItem as HoaDonDTO;
            if (selectedHoaDon == null || selectedHoaDon.TrangThai != "Đã hủy")
                return;

            try
            {
                // Lấy thông tin hủy từ database
                var thongTinHuy = _hoaDonBus.GetThongTinHuyHoaDon(selectedHoaDon.MaHoaDon);
                
                // Lấy chi tiết hóa đơn
                var chiTietList = _hoaDonBus.GetChiTietHoaDon(selectedHoaDon.MaHoaDon.ToString());

                // Tính điểm hoàn lại (nếu có khách hàng)
                int? diemHoanLai = null;
                int? diemTichLuyBiTru = null;
                int? diemSuDungTraLai = null;
                
                if (selectedHoaDon.MaKhachHang.HasValue)
                {
                    // Tính điểm tích lũy đã cộng (tổng số lượng sản phẩm)
                    int diemTichLuy = chiTietList.Sum(ct => ct.SoLuong);
                    diemTichLuyBiTru = diemTichLuy;
                    
                    // Tính điểm đã sử dụng (nếu có)
                    var quyDoiDiemBUS = new BUS.QuyDoiDiem_BUS();
                    decimal giaTriMotDiem = quyDoiDiemBUS.GetGiaTriMotDiem();
                    decimal tongTienLyThuyet = chiTietList.Sum(ct => ct.GiaBan * ct.SoLuong);
                    decimal tongTienThucTe = selectedHoaDon.TongTien ?? 0;
                    decimal giamTuDiem = tongTienLyThuyet - tongTienThucTe;
                    int diemSuDung = giaTriMotDiem > 0 && giamTuDiem > 0 ? (int)Math.Floor(giamTuDiem / giaTriMotDiem) : 0;
                    diemSuDungTraLai = diemSuDung;
                    
                    // Điểm hoàn lại = điểm đã sử dụng - điểm tích lũy đã cộng
                    // (Vì khi hủy, ta trả lại điểm đã dùng và trừ điểm đã tích)
                    diemHoanLai = diemSuDung - diemTichLuy;
                }

                // Hiển thị dialog
                using (var dialog = new Dialog.Dialog_XemLyDoHuy(
                    selectedHoaDon,
                    chiTietList,
                    thongTinHuy.LyDoHuy,
                    thongTinHuy.NgayHuy,
                    thongTinHuy.TenNhanVienHuy,
                    diemHoanLai,
                    diemTichLuyBiTru,
                    diemSuDungTraLai))
                {
                    dialog.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"❌ Lỗi khi lấy thông tin hủy hóa đơn:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void huyButton_Click(object sender, EventArgs e)
        {
            if (hoaDonDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "Vui lòng chọn hóa đơn cần hủy!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedHoaDon = hoaDonDataGridView.SelectedRows[0].DataBoundItem as HoaDonDTO;
            if (selectedHoaDon == null)
            {
                return;
            }

            // Kiểm tra trạng thái
            if (selectedHoaDon.TrangThai == "Đã hủy")
            {
                MessageBox.Show(this, $"Hóa đơn #{selectedHoaDon.MaHoaDon} đã được hủy trước đó!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy thông tin chi tiết để hiển thị
            var chiTietList = _hoaDonBus.GetChiTietHoaDon(selectedHoaDon.MaHoaDon.ToString());
            string thongTin = $"Mã hóa đơn: HD{selectedHoaDon.MaHoaDon:D3}\n";
            thongTin += $"Khách hàng: {selectedHoaDon.KhachHang}\n";
            thongTin += $"Tổng tiền: {selectedHoaDon.TongTien:N0} đ\n";
            thongTin += $"Số sản phẩm: {chiTietList.Count} loại\n";
            thongTin += $"\n⚠️ Lưu ý: Hệ thống sẽ:\n";
            thongTin += $"  • Trả lại {chiTietList.Sum(ct => ct.SoLuong)} sản phẩm về kho\n";
            if (selectedHoaDon.MaKhachHang.HasValue)
            {
                int diemTichLuy = chiTietList.Sum(ct => ct.SoLuong);
                thongTin += $"  • Trừ {diemTichLuy} điểm tích lũy đã cộng\n";
                // Tính điểm đã sử dụng (nếu có)
                var quyDoiDiemBUS = new BUS.QuyDoiDiem_BUS();
                decimal giaTriMotDiem = quyDoiDiemBUS.GetGiaTriMotDiem();
                decimal tongTienLyThuyet = chiTietList.Sum(ct => ct.GiaBan * ct.SoLuong);
                decimal tongTienThucTe = selectedHoaDon.TongTien ?? 0;
                decimal giamTuDiem = tongTienLyThuyet - tongTienThucTe;
                int diemSuDung = giaTriMotDiem > 0 && giamTuDiem > 0 ? (int)Math.Floor(giamTuDiem / giaTriMotDiem) : 0;
                if (diemSuDung > 0)
                {
                    thongTin += $"  • Trả lại {diemSuDung} điểm đã sử dụng\n";
                }
            }

            // Hiển thị dialog xác nhận
            using (var dialog = new Dialog_HuyHoaDon(selectedHoaDon.MaHoaDon, thongTin))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        // Lấy mã nhân viên từ session
                        int maNhanVien = Common.SessionManager.CurrentMaNhanVien ?? 1;

                        // Gọi method hủy hóa đơn hoàn chỉnh
                        _hoaDonBus.HuyHoaDonHoanChinh(
                            selectedHoaDon.MaHoaDon,
                            maNhanVien,
                            dialog.LyDoHuy
                        );

                        MessageBox.Show(this, 
                            $"✅ Hủy hóa đơn #{selectedHoaDon.MaHoaDon} thành công!\n\n" +
                            $"Đã trả lại hàng về kho và điều chỉnh điểm khách hàng (nếu có).",
                            "Thành công", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);

                        // Reload dữ liệu
                        LoadHoaDonData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, 
                            $"❌ Lỗi khi hủy hóa đơn:\n\n{ex.Message}", 
                            "Lỗi", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}