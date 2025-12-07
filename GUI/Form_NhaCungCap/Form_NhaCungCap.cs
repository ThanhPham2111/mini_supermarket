#nullable disable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;
using System.Data;
using System.IO;

namespace mini_supermarket.GUI.NhaCungCap
{
    public partial class Form_NhaCungCap : Form
    {
        // Hằng hiển thị
        private const string StatusAll = "Tất cả";
        private const string FunctionPath = "Form_NhaCungCap";

        // Tầng nghiệp vụ
        private readonly NhaCungCap_BUS _bus = new();
        private readonly PermissionService _permissionService = new();

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
            nhaCungCapDataGridView.SelectionChanged += (_, _) => { HienThiThongTin(); UpdateButtonsState(); };
            exportExcelButton.Click += ExportExcelButton_Click;
            importExcelButton.Click += ImportExcelButton_Click;

            ApplyPermissions();
        }

        private void ApplyPermissions()
        {
            bool canAdd = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them);
            bool canEdit = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
            bool canDelete = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa);

            themButton.Enabled = canAdd;
            suaButton.Enabled = canEdit && nhaCungCapDataGridView.SelectedRows.Count > 0;
            xoaButton.Enabled = canDelete && nhaCungCapDataGridView.SelectedRows.Count > 0;
        }

        private void UpdateButtonsState()
        {
            bool hasSelection = nhaCungCapDataGridView.SelectedRows.Count > 0;
            bool canEdit = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua);
            bool canDelete = _permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa);

            suaButton.Enabled = hasSelection && canEdit;
            xoaButton.Enabled = hasSelection && canDelete;
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
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them))
            {
                MessageBox.Show("Bạn không có quyền thêm nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Sua))
            {
                MessageBox.Show("Bạn không có quyền sửa nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Xoa))
            {
                MessageBox.Show("Bạn không có quyền khóa nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            UpdateButtonsState();
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
        // export excel
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

        try
        {
            _bus.XuatNhaCungCapRaExcel(list, sfd.FileName);
            MessageBox.Show("✅ Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    // import excel
    private void ImportExcelButton_Click(object sender, EventArgs e)
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
            var importedList = _bus.NhapNhaCungCapTuExcel(ofd.FileName);

            if (importedList == null || importedList.Count == 0)
            {
                MessageBox.Show("File Excel không có dữ liệu hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị lên bảng
            nhaCungCapDataGridView.DataSource = importedList;
            _dsNhaCungCap = importedList;

            MessageBox.Show($"✅ Nhập Excel thành công! Đã đọc được {importedList.Count} nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi nhập Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    }
}
