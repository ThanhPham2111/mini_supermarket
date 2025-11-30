using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;
using OfficeOpenXml;
using System.Data;
using System.IO;

namespace mini_supermarket.GUI.NhaCungCap
{
    public partial class Form_NhaCungCap : Form
    {
        // Hằng hiển thị
        private const string StatusAll = "Tất cả";

        // Tầng nghiệp vụ
        private readonly NhaCungCap_BUS _bus = new();

        // Lưu trạng thái
        private List<string> _dsTrangThai = new();

        // Danh sách hiện tại hiển thị
        private List<NhaCungCapDTO> _dsNhaCungCap = new();

        public Form_NhaCungCap()
        {
            InitializeComponent();
            Load += Form_NhaCungCap_Load;
        }

        private void Form_NhaCungCap_Load(object sender, EventArgs e)
        {
            LoadDanhSachTrangThai();
            LoadDanhSachNhaCungCap();
 
            statusFilterComboBox.SelectedIndexChanged += (_, _) => LocTheoTrangThai();
            searchTextBox.TextChanged += (_, _) => TimKiem();
            themButton.Click += ThemButton_Click;
            suaButton.Click += SuaButton_Click;
            xoaButton.Click += XoaButton_Click;
            lamMoiButton.Click += (_, _) => LamMoi();
            nhaCungCapDataGridView.SelectionChanged += (_, _) => HienThiThongTin();
            exportExcelButton.Click += ExportExcelButton_Click;
            importExcelButton.Click += ImportExcelButton_Click;

        }
      

        // ==================== LOAD DỮ LIỆU ====================
       private void HienThiThongTin()
        {
            if (nhaCungCapDataGridView.SelectedRows.Count == 0)
                return;

            var item = nhaCungCapDataGridView.SelectedRows[0].DataBoundItem as NhaCungCapDTO;
            if (item == null) return;

            maNhaCungCapTextBox.Text = item.MaNhaCungCap.ToString();
            tenNhaCungCapTextBox.Text = item.TenNhaCungCap;
            diaChiTextBox.Text = item.DiaChi;
            soDienThoaiTextBox.Text = item.SoDienThoai;
            emailTextBox.Text = item.Email;
        }


        private void LoadDanhSachTrangThai()
        {
            _dsTrangThai = _bus.GetDefaultStatuses().ToList();

            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add(StatusAll);
            statusFilterComboBox.Items.AddRange(_dsTrangThai.ToArray());
            statusFilterComboBox.SelectedIndex = 0;
        }

        private void LoadDanhSachNhaCungCap()
        {
            _dsNhaCungCap = _bus.GetNhaCungCap();
            HienThiLenBang(_dsNhaCungCap);
        }

      
        private void HienThiLenBang(List<NhaCungCapDTO> ds)
        {
            nhaCungCapDataGridView.AutoGenerateColumns = false;
            nhaCungCapDataGridView.DataSource = ds;

            if (nhaCungCapDataGridView.Rows.Count > 0)
                nhaCungCapDataGridView.Rows[0].Selected = true;
        }




        // ==================== SỰ KIỆN ====================

        private void ThemButton_Click(object sender, EventArgs e)
        {
            using var dialog = new Form_NhaCungCapDialog(_dsTrangThai);

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                _bus.AddNhaCungCap(dialog.NhaCungCap);
                LoadDanhSachNhaCungCap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm.\n" + ex.Message);
            }
        }

        private void SuaButton_Click(object sender, EventArgs e)
        {
            var item = GetSelectedItem();
            if (item == null) return;

            using var dialog = new Form_NhaCungCapDialog(_dsTrangThai, item);

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                _bus.UpdateNhaCungCap(dialog.NhaCungCap);
                LoadDanhSachNhaCungCap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể sửa.\n" + ex.Message);
            }
        }

        private void XoaButton_Click(object sender, EventArgs e)
        {
            var item = GetSelectedItem();
            if (item == null) return;

            if (MessageBox.Show("Khóa nhà cung cấp?", "Hỏi", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                item.TrangThai = NhaCungCap_BUS.StatusInactive;
                _bus.UpdateNhaCungCap(item);
                LoadDanhSachNhaCungCap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể khóa.\n" + ex.Message);
            }
        }

        private void LamMoi()
        {
            searchTextBox.Text = "";
            statusFilterComboBox.SelectedIndex = 0;
            LoadDanhSachNhaCungCap();
        }

              
        // ==================== LỌC + TÌM KIẾM ====================

        private void LocTheoTrangThai()
        {
            string trangThai = statusFilterComboBox.SelectedItem?.ToString() ?? StatusAll;

            List<NhaCungCapDTO> kq;

            if (trangThai == StatusAll)
                kq = _dsNhaCungCap;
            else
                kq = _dsNhaCungCap.Where(x => x.TrangThai == trangThai).ToList();

            HienThiLenBang(kq);
        }

        private void TimKiem()
        {
            string tuKhoa = searchTextBox.Text.Trim().ToLower();

            var kq = _dsNhaCungCap.Where(x =>
                x.MaNhaCungCap.ToString().Contains(tuKhoa) ||
                (x.TenNhaCungCap?.ToLower().Contains(tuKhoa) ?? false) ||
                (x.SoDienThoai?.ToLower().Contains(tuKhoa) ?? false)
            ).ToList();

            HienThiLenBang(kq);
        }

        // ==================== HỖ TRỢ ====================

       private NhaCungCapDTO GetSelectedItem()
        {
            if (nhaCungCapDataGridView.SelectedRows.Count == 0)
                return null;

            return nhaCungCapDataGridView.SelectedRows[0].DataBoundItem as NhaCungCapDTO;
        }
        // export excel và import excel
      

    private void ExportExcelButton_Click(object sender, EventArgs e)
    {
    SaveFileDialog sfd = new SaveFileDialog
    {
        Filter = "Excel Workbook|*.xlsx",
        Title = "Lưu Excel"
    };

    if (sfd.ShowDialog() != DialogResult.OK)
        return;

    var list = nhaCungCapDataGridView.DataSource as List<NhaCungCapDTO>;
    if (list == null || list.Count == 0)
    {
        MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }

    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    using (ExcelPackage package = new ExcelPackage())
    {
        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Nhà Cung Cấp");
        worksheet.Cells["A1"].LoadFromCollection(list, true);
        worksheet.Cells.AutoFitColumns();
        package.SaveAs(sfd.FileName);
    }

    MessageBox.Show("✅ Xuất Excel thành công!");
    }
    private void ImportExcelButton_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog
        {
            Filter = "Excel Workbook|*.xlsx",
            Title = "Chọn file Excel"
        };

        if (ofd.ShowDialog() != DialogResult.OK)
            return;

        var importedList = new List<NhaCungCapDTO>();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (ExcelPackage package = new ExcelPackage(new FileInfo(ofd.FileName)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;

            for (int row = 2; row <= rowCount; row++) // start from row 2 to skip header
            {
                if (colCount >= 5)
                {
                    var item = new NhaCungCapDTO
                    {
                        MaNhaCungCap = int.TryParse(worksheet.Cells[row, 1].Text, out var ma) ? ma : 0,
                        TenNhaCungCap = worksheet.Cells[row, 2].Text ?? "",
                        DiaChi = worksheet.Cells[row, 3].Text ?? "",
                        SoDienThoai = worksheet.Cells[row, 4].Text ?? "",
                        Email = worksheet.Cells[row, 5].Text ?? "",
                        TrangThai = colCount > 5 ? worksheet.Cells[row, 6].Text ?? "" : ""
                    };
                    importedList.Add(item);
                }
            }
        }

        // hiển thị lên bảng
        nhaCungCapDataGridView.DataSource = importedList;

        MessageBox.Show("✅ Nhập Excel thành công!");
    }


    }
}
