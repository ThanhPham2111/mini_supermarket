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

namespace mini_supermarket.GUI.HoaDon
{
    public partial class Form_HoaDon : Form
    {
        private readonly HoaDon_BUS _hoaDonBus = new();
        private readonly BindingSource _bindingSource = new();
        private BindingList<HoaDonDTO> _hoaDonList = new();
        public Form_HoaDon()
        {
            InitializeComponent();
            Load += Form_HoaDon_Load;
        }

        private void Form_HoaDon_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            searchComboBox.SelectedIndex = 0;
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
            // xemChiTietButton.Click += xemChiTietButton_Click;
            // lamMoiButton.Click += lamMoiButton_Click;
            // themFileButton.Click += themFileButton_Click;
            // xuatFileButton.Click += xuatFileButton_Click;

            timKiemButton.Enabled = true;
            xemChiTietButton.Enabled = false;
            lamMoiButton.Enabled = true;

            LoadHoaDonData();
        }

        private void LoadHoaDonData()
        {
            try
            {
                _hoaDonList.Clear();
                _hoaDonList = new BindingList<HoaDonDTO>(_hoaDonBus.GetHoaDon());
                _bindingSource.DataSource = _hoaDonList;
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
                string? selectedTrangThai = trangThaiComboBox.SelectedItem?.ToString();
                
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

        private void dateTimePicker2_ValueChanged(object? sender, EventArgs e)
        {
            timKiemButton_Click(sender, e);
        }

        private void dateTimePicker3_ValueChanged(object? sender, EventArgs e)
        {
            timKiemButton_Click(sender, e);
        }


        private void textBox1_TextChanged(object? sender, EventArgs e)
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

        private void textBox2_TextChanged(object? sender, EventArgs e)
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

        private void exportFileButton_Click(object? sender, EventArgs e)
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

        private void trangThaiComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Gọi hàm tìm kiếm khi trạng thái thay đổi
            timKiemButton_Click(sender, e);
        }

        private void hoaDonDataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
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

        private void huyButton_Click(object? sender, EventArgs e){
            var selectedHoaDon = hoaDonDataGridView.SelectedRows[0].DataBoundItem as HoaDonDTO;
            if(selectedHoaDon != null){
                var result = MessageBox.Show(this, $"Bạn có chắc chắn muốn hủy hóa đơn #{selectedHoaDon.MaHoaDon}?", "Xác nhận hủy hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result == DialogResult.Yes){
                    try{
                        int rowAffected = _hoaDonBus.HuyHoaDon(selectedHoaDon);
                        if(rowAffected > 0){
                            MessageBox.Show(this, $"✅ Hủy hóa đơn #{selectedHoaDon.MaHoaDon} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Cập nhật lại trạng thái trong danh sách hiện tại
                            selectedHoaDon.TrangThai = "Đã hủy";
                            hoaDonDataGridView.Refresh();
                        } else {
                            MessageBox.Show(this, $"❌ Hủy hóa đơn #{selectedHoaDon.MaHoaDon} thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } catch(Exception ex)
                    {
                        MessageBox.Show(this, $"❌ Lỗi khi hủy hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}