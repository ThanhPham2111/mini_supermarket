using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    }
}