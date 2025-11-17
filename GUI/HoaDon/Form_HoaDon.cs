// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
// using System.Drawing;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Forms;
// using mini_supermarket.BUS;
// using mini_supermarket.DTO;
// using System.Globalization;
// using mini_supermarket.DAO;

// namespace mini_supermarket.GUI.HoaDon
// {
//     public partial class Form_HoaDon : Form
//     {
//         private readonly HoaDon_BUS _hoaDonBus = new();
//         private readonly BindingSource _bindingSource = new();
//         // private readonly List<string> _roles;
//         private IList<HoaDonDTO> _currentHoaDon = Array.Empty<HoaDonDTO>();
//         public Form_HoaDon()
//         {
//             InitializeComponent();
//             Load += Form_HoaDon_Load;
//         }

//         private void hoaDonDataGridView_SelectionChanged(object? sender, EventArgs e)
//         {

//         }

//         private void Form_HoaDon_Load(object? sender, EventArgs e)
//         {
//             if (DesignMode)
//             {
//                 return;
//             }

//             hoaDonDataGridView.AutoGenerateColumns = false;
//             hoaDonDataGridView.DataSource = _bindingSource;
//             hoaDonDataGridView.SelectionChanged += hoaDonDataGridView_SelectionChanged;

//             var toolTip = new ToolTip();
//             toolTip.SetToolTip(themButton, "Thêm hóa đơn mới");
//             toolTip.SetToolTip(suaButton, "Sửa thông tin hóa đơn đã chọn");
//             toolTip.SetToolTip(xoaButton, "Khóa hóa đơn đã chọn"); // Updated: "Khóa" instead of "Xóa"
//             toolTip.SetToolTip(lamMoiButton, "Làm mới danh sách");
//             toolTip.SetToolTip(xemChiTietButton, "Xem chi tiết hóa đơn đã chọn");
//             toolTip.SetToolTip(thongKeButton, "Thống kê danh sách");

//             themButton.Click += themButton_Click;
//             suaButton.Click += suaButton_Click;
//             xoaButton.Click += xoaButton_Click;
//             lamMoiButton.Click += lamMoiButton_Click;
//             xemChiTietButton.Click += xemChiTietButton_Click;
//             thongKeButton.Click += (_, _) => ApplySearchFilter();


//             themButton.Enabled = true;
//             lamMoiButton.Enabled = true;
//             suaButton.Enabled = false;
//             xemChiTietButton.Enabled = false;
//             xoaButton.Enabled = false;

//             SetInputFieldsEnabled(false);

//             LoadHoaDonData();
//         }

//         private void SetInputFieldsEnabled(bool enabled)
//         {
//             idSearchTextBox.Enabled = enabled;
//             customerSearchTextBox.Enabled = enabled;
//             employeeSearchTextBox.Enabled = enabled;
//             thanhTien1TextBox.Enabled = enabled;
//             thanhTien2TextBox.Enabled = enabled;
//             ngayDateTimePicker.Enabled = enabled;
//             khoangThoiGian1DateTimePicker.Enabled = enabled;
//             khoangThoiGian2DateTimePicker.Enabled = enabled;
//         }

//         private void themButton_Click(object sender, EventArgs e)
//         {

//         }
//         private void suaButton_Click(object sender, EventArgs e)
//         {

//         }
//         private void xoaButton_Click(object sender, EventArgs e)
//         {

//         }
//         private void lamMoiButton_Click(object sender, EventArgs e)
//         {

//         }
//         private void thongKeButton_Click(object sender, EventArgs e)
//         {

//         }
//         private void xemChiTietButton_Click(object sender, EventArgs e)
//         {

//         }

//         private void LoadHoaDonData()
//         {
//             try
//             {
//                 _currentHoaDon = _hoaDonBus.GetHoaDon();
//                 ApplyStatusFilter();
//             }
//             catch (Exception ex)
//             {
//                 MessageBox.Show(this, $"Không thể tải danh sách hóa đơn.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//             }
//         }

//         // private void ApplyStatusFilter()
//         // {

//         // }

//         // private void ApplySearchFilter()
//         // {
            
//         // }
        
//         private void ApplyStatusFilter()
//         {
//             var filtered = new List<HoaDonDTO>();
//             foreach (var hoaDon in _currentHoaDon)
//             {
//                 filtered.Add(hoaDon);
//             }
//             _bindingSource.DataSource = filtered;

//             hoaDonDataGridView.ClearSelection();
//         }

//         private void ApplySearchFilter()
//         {
//             string idSearchText = idSearchTextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));
//             string customerSearchText = customerSearchTextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));
//             string employeeSearchText = employeeSearchTextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));
//             string thanhTien1Text = thanhTien1TextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));
//             string thanhTien2Text = thanhTien2TextBox.Text.Trim().ToLower(CultureInfo.GetCultureInfo("vi-VN"));

//             var filtered = new List<HoaDonDTO>();

//             foreach (var hoaDon in _currentHoaDon)
//             {
//                 NhanVien_BUS nv_BUS = new NhanVien_BUS();
//                 string tenNhanVien = nv_BUS.GetNhanVienByID(hoaDon.MaHoaDon.ToString());
//                 string tenKhachHang = customerSearchText;
//                 bool matchesMaHoaDon = string.IsNullOrEmpty(idSearchText) ||
//                     hoaDon.MaHoaDon.ToString().ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(idSearchText);

//                 bool matchesCustomer = string.IsNullOrEmpty(customerSearchText) ||
//                     hoaDon.MaNhanVien.ToString().ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(customerSearchText) ||
//                     (hoaDon.?.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(customerSearchText) ?? false) ||
//                     (hoaDon.SoDienThoai?.ToLower(CultureInfo.GetCultureInfo("vi-VN")).Contains(customerSearchText) ?? false);

//                 if (matchesSearch)
//                 {
//                     filtered.Add(hoaDon);
//                 }
//             }

//             _bindingSource.DataSource = filtered;
//             hoaDonDataGridView.ClearSelection();
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mini_supermarket.GUI.HoaDon
{
    public partial class Form_HoaDon : Form
    {
        public Form_HoaDon()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void khachHangDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lamMoiButton_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
