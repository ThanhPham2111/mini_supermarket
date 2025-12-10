#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;               // Thêm cho BindingList
using System.IO;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace mini_supermarket.GUI.NhaCungCap
{
    public partial class Form_NhaCungCap : Form
    {
        // Hằng hiển thị
        private const string StatusAll = "Tất cả";
        private const string FunctionPath = "Form_NhaCungCap";

        // Tầng nghiệp vụ
        private readonly NhaCungCap_BUS _nhaCungCapBus = new();
        // Thay vì dùng trực tiếp BindingList, ta dùng BindingSource làm "trung gian"
        private readonly BindingSource _bindingSource = new();
        private readonly PermissionService _permissionService = new();
        // Danh sách trạng thái (Hoạt động / Khóa)
        private List<string> _dsTrangThai = new();
        // Danh sách GỐC từ CSDL (không thay đổi khi lọc/tìm kiếm)
        private List<NhaCungCapDTO> _allNhaCungCap = new();

        // Danh sách HIỆN THỊ trên grid – dùng BindingList để tự động cập nhật UI
        private BindingList<NhaCungCapDTO> _dsNhaCungCap = new();

        public Form_NhaCungCap()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            InitializeComponent();
            Load += Form_NhaCungCap_Load;
        }
        /// Sự kiện Load form - khởi tạo tất cả
        private void Form_NhaCungCap_Load(object sender, EventArgs e)
        {
            LoadDanhSachTrangThai();
            LoadDanhSachNhaCungCap(); // Load dữ liệu vào grid lần đầu
            // Gắn sự kiện
            statusFilterComboBox.SelectedIndexChanged += (_, _) => LocTheoTrangThai();
            searchTextBox.TextChanged += (_, _) => TimKiem();
            themButton.Click += ThemButton_Click;
            suaButton.Click += SuaButton_Click;
            xoaButton.Click += XoaButton_Click;
            lamMoiButton.Click += (_, _) => LamMoi();
            nhaCungCapDataGridView.SelectionChanged += (_, _) => 
            { 
                HienThiThongTin(); 
                UpdateButtonsState(); 
            };
            exportExcelButton.Click += ExportExcelButton_Click;
            importExcelButton.Click += ImportExcelButton_Click;

            ApplyPermissions();
        }

        #region Phân quyền

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

        #endregion

        #region Hiển thị thông tin chi tiết

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

        #endregion

        #region Load dữ liệu
        // Load danh sách trạng thái vào ComboBox lọc
        private void LoadDanhSachTrangThai()
        {
            _dsTrangThai = _nhaCungCapBus.GetDefaultStatuses().ToList();

            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add(StatusAll);
            statusFilterComboBox.Items.AddRange(_dsTrangThai.ToArray());
            statusFilterComboBox.SelectedIndex = 0; // "tat ca"
        }
        // Load toàn bộ nhà cung cấp từ DB và gán vào BindingSource
        private void LoadDanhSachNhaCungCap()
        {
            _allNhaCungCap = _nhaCungCapBus.GetNhaCungCap(); // Lấy toàn bộ từ DB
           // Quan trọng: Gán BindingList vào BindingSource
            _bindingSource.DataSource = new BindingList<NhaCungCapDTO>(_allNhaCungCap);
            
           // Gán BindingSource làm nguồn dữ liệu cho grid
            nhaCungCapDataGridView.DataSource = _bindingSource;
        }

        private void HienThiLenBang(BindingList<NhaCungCapDTO> ds)
        {
            nhaCungCapDataGridView.AutoGenerateColumns = false;
            nhaCungCapDataGridView.DataSource = null; // Reset để tránh lỗi binding
            nhaCungCapDataGridView.DataSource = ds;

            if (nhaCungCapDataGridView.Rows.Count > 0)
                nhaCungCapDataGridView.Rows[0].Selected = true;
        }

        #endregion

        private void LamMoi()
        {
            searchTextBox.Clear();
            statusFilterComboBox.SelectedIndex = 0;
            _dsNhaCungCap = new BindingList<NhaCungCapDTO>(_allNhaCungCap);
            HienThiLenBang(_dsNhaCungCap);
        }

        #region Lọc & Tìm kiếm

        private void LocTheoTrangThai()
        {
            string trangThai = statusFilterComboBox.SelectedItem?.ToString() ?? StatusAll;

            if (trangThai == StatusAll)
            {
                _bindingSource.DataSource = new BindingList<NhaCungCapDTO>(_allNhaCungCap);
            }
            else
            {
                var filtered = _allNhaCungCap.Where(x => x.TrangThai == trangThai).ToList();
                _bindingSource.DataSource = new BindingList<NhaCungCapDTO>(filtered);
            }

            UpdateButtonsState();
        }

        private void TimKiem()
        {
            string tuKhoa = searchTextBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                _dsNhaCungCap = new BindingList<NhaCungCapDTO>(_allNhaCungCap);
            }
            else
            {
                var ketQua = _allNhaCungCap.Where(x =>
                    x.MaNhaCungCap.ToString().Contains(tuKhoa) ||
                    (x.TenNhaCungCap?.ToLower().Contains(tuKhoa) ?? false) ||
                    (x.SoDienThoai?.ToLower().Contains(tuKhoa) ?? false)
                ).ToList();

                _dsNhaCungCap = new BindingList<NhaCungCapDTO>(ketQua);
            }

            HienThiLenBang(_dsNhaCungCap);
        }
        
        //         private void TimKiem()
        // {
        //     string tuKhoa = searchTextBox.Text.Trim();
        //     if (string.IsNullOrEmpty(tuKhoa))
        //     {
        //         _bindingSource.RemoveFilter();
        //     }
        //     else
        //     {
        //         tuKhoa = tuKhoa.ToLower();
        //         _bindingSource.Filter = $@"MaNhaCungCap.ToString().Contains('{tuKhoa}') = true OR 
        //                                 Convert(TenNhaCungCap, 'System.String').ToLower().Contains('{tuKhoa}') = true OR
        //                                 Convert(SoDienThoai, 'System.String').ToLower().Contains('{tuKhoa}') = true";
        //     }
        // }
        #endregion

        #region Thao tác Thêm / Sửa / Khóa

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
                _nhaCungCapBus.AddNhaCungCap(dialog.NhaCungCap);
                ReloadDataFromDatabase();
                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _nhaCungCapBus.UpdateNhaCungCap(dialog.NhaCungCap);
                ReloadDataFromDatabase();
                MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể sửa.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _nhaCungCapBus.UpdateNhaCungCap(item);
                ReloadDataFromDatabase();
                MessageBox.Show("Đã khóa nhà cung cấp.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể khóa.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private NhaCungCapDTO GetSelectedItem()
        {
            // if (nhaCungCapDataGridView.SelectedRows.Count == 0) return null;
            // return nhaCungCapDataGridView.SelectedRows[0].DataBoundItem as NhaCungCapDTO;
            return _bindingSource.Current as NhaCungCapDTO;
        }

        /// <summary>
        /// Tải lại dữ liệu từ CSDL và cập nhật BindingList (dùng chung cho thêm/sửa/khóa/import)
        /// </summary>
        private void ReloadDataFromDatabase()
        {
            _allNhaCungCap = _nhaCungCapBus.GetNhaCungCap();
           // Làm mới DataSource của BindingSource
            _bindingSource.DataSource = new BindingList<NhaCungCapDTO>(_allNhaCungCap);

            // Tự động áp dụng lại filter nếu có
            if (statusFilterComboBox.SelectedIndex > 0)
                LocTheoTrangThai();
            if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
                TimKiem();
                }

        #endregion

        #region Xuất Excel

        private void ExportExcelButton_Click(object sender, EventArgs e)
        {
            var list = _dsNhaCungCap?.ToList(); // Lấy danh sách đang hiển thị
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

                string[] headers = { "Mã NCC", "Tên nhà cung cấp", "Địa chỉ", "Số điện thoại", "Email", "Trạng thái" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cells[1, i + 1].Value = headers[i];
                    ws.Cells[1, i + 1].Style.Font.Bold = true;
                    ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                    ws.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ws.Cells[i + 2, 1].Value = list[i].MaNhaCungCap;
                    ws.Cells[i + 2, 2].Value = list[i].TenNhaCungCap;
                    ws.Cells[i + 2, 3].Value = list[i].DiaChi;
                    ws.Cells[i + 2, 4].Value = list[i].SoDienThoai;
                    ws.Cells[i + 2, 5].Value = list[i].Email;
                    ws.Cells[i + 2, 6].Value = list[i].TrangThai;
                }

                var range = ws.Cells[1, 1, list.Count + 1, 6];
                range.AutoFitColumns();
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);

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

        #endregion

        #region Nhập Excel

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
                var ws = package.Workbook.Worksheets.FirstOrDefault();

                if (ws?.Dimension == null)
                {
                    MessageBox.Show("File Excel không hợp lệ hoặc trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int rowCount = ws.Dimension.Rows;
                if (rowCount < 2)
                {
                    MessageBox.Show("File Excel không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Danh sách hiện có trong CSDL để kiểm tra trùng tên
                var dsHienTai = _nhaCungCapBus.GetNhaCungCap();
                var tenTonTai = new HashSet<string>(
                    dsHienTai.Select(x => x.TenNhaCungCap?.Trim()).Where(t => !string.IsNullOrEmpty(t)),
                    StringComparer.OrdinalIgnoreCase);

                var danhSachMoi = new List<NhaCungCapDTO>();
                var danhSachLoi = new List<string>();

                for (int row = 2; row <= rowCount; row++)
                {
                    string ten = ws.Cells[row, 2].GetValue<string>()?.Trim() ?? "";

                    if (string.IsNullOrWhiteSpace(ten))
                    {
                        danhSachLoi.Add($"Dòng {row}: Thiếu tên nhà cung cấp");
                        continue;
                    }

                    if (tenTonTai.Contains(ten))
                    {
                        danhSachLoi.Add($"Dòng {row}: Tên '{ten}' đã tồn tại");
                        continue;
                    }

                    string diaChi = ws.Cells[row, 3].GetValue<string>()?.Trim() ?? "";
                    string sdt = ws.Cells[row, 4].GetValue<string>()?.Trim() ?? "";
                    string email = ws.Cells[row, 5].GetValue<string>()?.Trim() ?? "";
                    string trangThai = (ws.Cells[row, 6].GetValue<string>()?.Trim() == "Khóa") ? "Khóa" : "Hoạt động";

                    var ncc = new NhaCungCapDTO
                    {
                        MaNhaCungCap = 0,
                        TenNhaCungCap = ten,
                        DiaChi = diaChi,
                        SoDienThoai = sdt,
                        Email = email,
                        TrangThai = trangThai
                    };

                    danhSachMoi.Add(ncc);
                    tenTonTai.Add(ten);
                }

                if (danhSachMoi.Count == 0)
                {
                    MessageBox.Show("Không có nhà cung cấp nào hợp lệ để thêm!\n\n" +
                        (danhSachLoi.Count > 0 ? string.Join("\n", danhSachLoi.Take(10)) : ""),
                        "Import thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xem trước trên grid
                _dsNhaCungCap = new BindingList<NhaCungCapDTO>(danhSachMoi);
                HienThiLenBang(_dsNhaCungCap);

                string thongBao = $"Sẵn sàng thêm {danhSachMoi.Count} nhà cung cấp mới.\n";
                if (danhSachLoi.Count > 0)
                    thongBao += $"Đã bỏ qua {danhSachLoi.Count} dòng lỗi/trùng.\n";

                if (MessageBox.Show(thongBao + "\nXác nhận nhập vào hệ thống?", "Xác nhận nhập", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int success = 0;
                    foreach (var ncc in danhSachMoi)
                    {
                        try
                        {
                            _nhaCungCapBus.AddNhaCungCap(ncc);
                            success++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi thêm {ncc.TenNhaCungCap}: {ex.Message}");
                        }
                    }

                    ReloadDataFromDatabase();

                    MessageBox.Show($"HOÀN TẤT!\nThành công: {success}\nBỏ qua: {danhSachLoi.Count + (danhSachMoi.Count - success)}",
                        "Nhập Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file Excel:\n" + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}