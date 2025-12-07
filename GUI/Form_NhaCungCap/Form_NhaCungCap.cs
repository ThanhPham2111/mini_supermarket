#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;
using OfficeOpenXml;                    // EPPlus
using OfficeOpenXml.Style;

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
            // BẮT BUỘC PHẢI CÓ DÒNG NÀY KHI DÙNG EPPPLUS >= 5
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Hoặc Commercial nếu đã mua

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

        // ==================== HIỂN THỊ THÔNG TIN ====================
        private void HienThiThongTin()
        {
            if (nhaCungCapDataGridView.SelectedRows.Count == 0) return;

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
            nhaCungCapDataGridView.DataSource = null;
            nhaCungCapDataGridView.DataSource = ds;

            if (nhaCungCapDataGridView.Rows.Count > 0)
                nhaCungCapDataGridView.Rows[0].Selected = true;
        }

        // ==================== SỰ KIỆN NÚT ====================
        private void ThemButton_Click(object sender, EventArgs e)
        {
            if (!_permissionService.HasPermissionByPath(FunctionPath, PermissionService.LoaiQuyen_Them))
            {
                MessageBox.Show("Bạn không có quyền thêm nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new Form_NhaCungCapDialog(_dsTrangThai);
            if (dialog.ShowDialog() != DialogResult.OK) return;

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
            if (dialog.ShowDialog() != DialogResult.OK) return;

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

            if (MessageBox.Show("Bạn có chắc chắn muốn khóa nhà cung cấp này?", "Xác nhận khóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
            var kq = trangThai == StatusAll
                ? _dsNhaCungCap
                : _dsNhaCungCap.Where(x => x.TrangThai == trangThai).ToList();

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

        private NhaCungCapDTO GetSelectedItem()
        {
            if (nhaCungCapDataGridView.SelectedRows.Count == 0) return null;
            return nhaCungCapDataGridView.SelectedRows[0].DataBoundItem as NhaCungCapDTO;
        }

        // ==================== EXPORT EXCEL - EPPlus ====================
        private void ExportExcelButton_Click(object sender, EventArgs e)
        {
            var list = nhaCungCapDataGridView.DataSource as List<NhaCungCapDTO>;
            if (list == null || list.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Xuất danh sách nhà cung cấp",
                FileName = $"NhaCungCap_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Danh sách nhà cung cấp");

                // Header
                string[] headers = { "Mã NCC", "Tên nhà cung cấp", "Địa chỉ", "Số điện thoại", "Email", "Trạng thái" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cells[1, i + 1].Value = headers[i];
                    ws.Cells[1, i + 1].Style.Font.Bold = true;
                    ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                    ws.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Data
                for (int i = 0; i < list.Count; i++)
                {
                    ws.Cells[i + 2, 1].Value = list[i].MaNhaCungCap;
                    ws.Cells[i + 2, 2].Value = list[i].TenNhaCungCap;
                    ws.Cells[i + 2, 3].Value = list[i].DiaChi;
                    ws.Cells[i + 2, 4].Value = list[i].SoDienThoai;
                    ws.Cells[i + 2, 5].Value = list[i].Email;
                    ws.Cells[i + 2, 6].Value = list[i].TrangThai;
                }

                // Tô viền + AutoFit
                var range = ws.Cells[1, 1, list.Count + 1, 6];
                range.AutoFitColumns();
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                // Lưu file
                File.WriteAllBytes(sfd.FileName, package.GetAsByteArray());

                MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (MessageBox.Show("Mở file vừa xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== IMPORT EXCEL - CHỈ THÊM MỚI, KHÔNG CẬP NHẬT DÒNG CŨ ====================
private void ImportExcelButton_Click(object sender, EventArgs e)
{
    using OpenFileDialog ofd = new OpenFileDialog
    {
        Filter = "Excel Workbook|*.xlsx",
        Title = "Chọn file Excel để nhập nhà cung cấp"
    };

    if (ofd.ShowDialog() != DialogResult.OK) return;

    try
    {
        var fileInfo = new FileInfo(ofd.FileName);
        using var package = new ExcelPackage(fileInfo);
        var ws = package.Workbook.Worksheets[0];
        int rowCount = ws.Dimension?.Rows ?? 0;

        if (rowCount < 2)
        {
            MessageBox.Show("File Excel không có dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Lấy danh sách nhà cung cấp hiện có trong CSDL để kiểm tra trùng
        var dsHienTai = _bus.GetNhaCungCap(); // Danh sách từ DB
        var tenHienTai = new HashSet<string>(dsHienTai.Select(x => x.TenNhaCungCap.Trim()), StringComparer.OrdinalIgnoreCase);
        var emailHienTai = new HashSet<string>(dsHienTai.Where(x => !string.IsNullOrEmpty(x.Email)).Select(x => x.Email.Trim()), StringComparer.OrdinalIgnoreCase);

        var importedList = new List<NhaCungCapDTO>();
        var danhSachMoi = new List<NhaCungCapDTO>();

        for (int row = 2; row <= rowCount; row++)
        {
            string ten = ws.Cells[row, 2].GetValue<string>()?.Trim();
            if (string.IsNullOrWhiteSpace(ten)) continue;

            string email = ws.Cells[row, 5].GetValue<string>()?.Trim() ?? "";

            // Kiểm tra trùng: theo Tên công ty HOẶC Email (nếu có email)
            bool daTonTai = tenHienTai.Contains(ten) || 
                           (!string.IsNullOrEmpty(email) && emailHienTai.Contains(email));

            if (daTonTai)
                continue; // Bỏ qua dòng này vì đã tồn tại

            var ncc = new NhaCungCapDTO
            {
                TenNhaCungCap = ten,
                DiaChi = ws.Cells[row, 3].GetValue<string>()?.Trim() ?? "",
                SoDienThoai = ws.Cells[row, 4].GetValue<string>()?.Trim() ?? "",
                Email = email,
                TrangThai = (ws.Cells[row, 6].GetValue<string>()?.Trim() == "Hoạt động") ? "Hoạt động" : "Khóa"
            };

            importedList.Add(ncc);
            danhSachMoi.Add(ncc); // Những cái sẽ được thêm mới
        }

        if (importedList.Count == 0)
        {
            MessageBox.Show("Tất cả nhà cung cấp trong file đã tồn tại trong hệ thống!\nKhông có dữ liệu mới để nhập.", 
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Hiển thị xem trước (chỉ những dòng mới)
        _dsNhaCungCap = importedList;
        HienThiLenBang(importedList);

        // Xác nhận nhập
        if (MessageBox.Show(
            $"Đã phát hiện {danhSachMoi.Count} nhà cung cấp mới (chưa tồn tại).\n\n" +
            $"Bạn có muốn thêm vào cơ sở dữ liệu không?",
            "Xác nhận nhập dữ liệu mới", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            int success = 0;
            foreach (var ncc in danhSachMoi)
            {
                try
                {
                    _bus.AddNhaCungCap(ncc);
                    success++;
                }
                catch (Exception ex)
                {
                    // Có thể ghi log nếu cần
                    Console.WriteLine($"Lỗi khi thêm {ncc.TenNhaCungCap}: {ex.Message}");
                }
            }

            LoadDanhSachNhaCungCap(); // Reload lại từ DB
            MessageBox.Show(
                $"Hoàn tất!\n" +
                $"Đã thêm thành công {success} nhà cung cấp mới.\n" +
                $"Bỏ qua {rowCount - 1 - danhSachMoi.Count} dòng đã tồn tại.",
                "Nhập Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi khi đọc file Excel:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
    }
}