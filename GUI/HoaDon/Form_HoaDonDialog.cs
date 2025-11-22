using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mini_supermarket.DTO;
using mini_supermarket.BUS;
using DocumentFormat.OpenXml.Drawing.Charts;
using ClosedXML.Excel;

namespace mini_supermarket.GUI.HoaDon
{
    public partial class Form_HoaDonDialog : Form
    {
        HoaDon_BUS _hoaDonBus = new HoaDon_BUS();
        BindingList<ChiTietHoaDonDTO> _chiTietHoaDonList = new BindingList<ChiTietHoaDonDTO>();
        BindingSource _bindingSource = new();
        public Form_HoaDonDialog(HoaDonDTO? selectedHoaDon) 
        {
            InitializeComponent();
            Load += Form_Hoa_Don_Dialog_Load;
            Load_Form(selectedHoaDon);
        }

        private void Form_Hoa_Don_Dialog_Load(object? sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _bindingSource;
        }

        private void Load_Form(HoaDonDTO? selectedHoaDon){
            if (selectedHoaDon != null)
            {
                // Hiển thị thông tin hóa đơn lên các control
                maHoaDonText.Text = selectedHoaDon.MaHoaDon.ToString();
                ngayText.Text = selectedHoaDon.NgayLap?.ToString("dd/MM/yyyy HH:mm");
                khachHangText.Text = selectedHoaDon.KhachHang;
            }

            _chiTietHoaDonList = new BindingList<ChiTietHoaDonDTO>(_hoaDonBus.GetChiTietHoaDon(maHoaDonText.Text));
            _bindingSource.DataSource = _chiTietHoaDonList;
            dataGridView1.ClearSelection();
        }

        private void maHoaDonLabel_Click(object sender, EventArgs e)
        {

        }

        private void ngayLabel_Click(object sender, EventArgs e)
        {

        }

        private void khachHangLabel_Click(object sender, EventArgs e)
        {

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
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                dt.Columns.Add(col.HeaderText);

            // Lấy dữ liệu
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var data = new object[row.Cells.Count];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    data[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(data);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Nhà Cung Cấp");
                wb.SaveAs(sfd.FileName);
            }

            MessageBox.Show("✅ Xuất Excel thành công!");
        }
    }
}
