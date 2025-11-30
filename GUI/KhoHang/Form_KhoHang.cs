#nullable enable

using mini_supermarket.BUS;
using mini_supermarket.GUI.Form_SanPham;
using mini_supermarket.DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using System.Collections.Generic;

namespace mini_supermarket.GUI.KhoHang
{
    public partial class Form_KhoHang : Form
    {
        private KhoHangBUS khoHangBUS = new KhoHangBUS();
        private IList<TonKhoDTO>? dtProducts = null;
        private const int NGUONG_CANH_BAO = 10; // Ngưỡng cảnh báo hàng sắp hết
        private ToolTip toolTipTenSP = new ToolTip(); // ToolTip để hiển thị tên sản phẩm đầy đủ

        public Form_KhoHang()
        {
            InitializeComponent();
        }

        private void Form_KhoHang_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadDataGridView();
            
            // Đăng ký sự kiện ToolTip
            dgvKhoHang.CellMouseEnter += dgvKhoHang_CellMouseEnter;
            dgvKhoHang.CellMouseLeave += dgvKhoHang_CellMouseLeave;

            // Cho phép sắp xếp cột và hàng
            dgvKhoHang.AllowUserToOrderColumns = true;
            foreach (DataGridViewColumn column in dgvKhoHang.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void LoadComboBoxes()
        {
            // Load Loại sản phẩm
            var listLoai = khoHangBUS.LayDanhSachLoai();
            var comboListLoai = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(-1, "Tất cả loại") };
            foreach (var item in listLoai)
            {
                comboListLoai.Add(new KeyValuePair<int, string>(item.MaLoai, item.TenLoai));
            }
            cboLoaiSP.DataSource = comboListLoai;
            cboLoaiSP.DisplayMember = "Value";
            cboLoaiSP.ValueMember = "Key";

            // Load Thương hiệu
            var listThuongHieu = khoHangBUS.LayDanhSachThuongHieu();
            var comboListThuongHieu = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(-1, "Tất cả thương hiệu") };
            foreach (var item in listThuongHieu)
            {
                comboListThuongHieu.Add(new KeyValuePair<int, string>(item.MaThuongHieu, item.TenThuongHieu));
            }
            cboThuongHieu.DataSource = comboListThuongHieu;
            cboThuongHieu.DisplayMember = "Value";
            cboThuongHieu.ValueMember = "Key";

            // Load Trạng thái
            var comboListTrangThai = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("", "Tất cả trạng thái"),
                new KeyValuePair<string, string>("Còn hàng", "Còn hàng"),
                new KeyValuePair<string, string>("Hết hàng", "Hết hàng")
            };
            cboTrangThai.DataSource = comboListTrangThai;
            cboTrangThai.DisplayMember = "Value";
            cboTrangThai.ValueMember = "Key";
        }

        private void LoadDataGridView()
        {
            dtProducts = khoHangBUS.LayDanhSachTonKho();

            if (dtProducts != null)
            {
                // Không cần CaseSensitive cho IList
            }

            dgvKhoHang.DataSource = dtProducts;
            SetupColumnHeaders();
        }

        private void SetupColumnHeaders()
        {
            if (dgvKhoHang.Columns["MaLoai"] != null) dgvKhoHang.Columns["MaLoai"].Visible = false;
            if (dgvKhoHang.Columns["MaThuongHieu"] != null) dgvKhoHang.Columns["MaThuongHieu"].Visible = false;
            if (dgvKhoHang.Columns["MaSanPham"] != null) dgvKhoHang.Columns["MaSanPham"].HeaderText = "Mã sản phẩm";
            if (dgvKhoHang.Columns["TenSanPham"] != null) dgvKhoHang.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            if (dgvKhoHang.Columns["TenDonVi"] != null) dgvKhoHang.Columns["TenDonVi"].HeaderText = "Đơn vị";
            if (dgvKhoHang.Columns["TenLoai"] != null) dgvKhoHang.Columns["TenLoai"].HeaderText = "Loại";
            if (dgvKhoHang.Columns["TenThuongHieu"] != null) dgvKhoHang.Columns["TenThuongHieu"].HeaderText = "Thương hiệu";
            if (dgvKhoHang.Columns["SoLuong"] != null) dgvKhoHang.Columns["SoLuong"].HeaderText = "Số lượng";
            if (dgvKhoHang.Columns["TrangThai"] != null) dgvKhoHang.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvKhoHang.Columns["GiaBan"] != null) 
            {
                dgvKhoHang.Columns["GiaBan"].HeaderText = "Giá bán";
                dgvKhoHang.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                dgvKhoHang.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvKhoHang.Columns["Hsd"] != null) dgvKhoHang.Columns["Hsd"].HeaderText = "Hạn sử dụng";
            if (dgvKhoHang.Columns["GiaNhap"] != null) 
            {
                dgvKhoHang.Columns["GiaNhap"].HeaderText = "Giá nhập";
                dgvKhoHang.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                dgvKhoHang.Columns["GiaNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            foreach (DataGridViewColumn column in dgvKhoHang.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Set lại alignment cho cột giá (căn giữa)
            if (dgvKhoHang.Columns["GiaBan"] != null) 
                dgvKhoHang.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvKhoHang.Columns["GiaNhap"] != null) 
                dgvKhoHang.Columns["GiaNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Sắp xếp lại thứ tự cột
            if (dgvKhoHang.Columns.Contains("TenThuongHieu")) dgvKhoHang.Columns["TenThuongHieu"].DisplayIndex = 4;
            if (dgvKhoHang.Columns.Contains("Hsd")) dgvKhoHang.Columns["Hsd"].DisplayIndex = 5;
            if (dgvKhoHang.Columns.Contains("SoLuong")) dgvKhoHang.Columns["SoLuong"].DisplayIndex = 6;
        }

        // Highlight cảnh báo hàng tồn kho thấp
        private void dgvKhoHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKhoHang.Rows.Count) return;
            if (dgvKhoHang.Rows[e.RowIndex].DataBoundItem == null) return;
            TonKhoDTO item = (TonKhoDTO)dgvKhoHang.Rows[e.RowIndex].DataBoundItem;
            int soLuong = item.SoLuong ?? 0;
            if (soLuong == 0)
            {
                dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 220, 220);
                dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
            }
            else if (soLuong < NGUONG_CANH_BAO)
            {
                dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 220);
                dgvKhoHang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.DarkOrange;
            }
        }

        private void ApplyFilters()
        {
            if (dtProducts == null) return;

            var filtered = dtProducts.AsEnumerable();

            string tuKhoa = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                filtered = filtered.Where(item => item.TenSanPham.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) || item.MaSanPham.ToString().Contains(tuKhoa));
            }

            if (cboLoaiSP.SelectedValue != null && (int)cboLoaiSP.SelectedValue != -1)
            {
                filtered = filtered.Where(item => item.MaLoai == (int)cboLoaiSP.SelectedValue);
            }

            if (cboThuongHieu.SelectedValue != null && (int)cboThuongHieu.SelectedValue != -1)
            {
                filtered = filtered.Where(item => item.MaThuongHieu == (int)cboThuongHieu.SelectedValue);
            }

            if (cboTrangThai.SelectedValue != null && !string.IsNullOrEmpty(cboTrangThai.SelectedValue.ToString()))
            {
                filtered = filtered.Where(item => item.TrangThai == cboTrangThai.SelectedValue.ToString());
            }

            dgvKhoHang.DataSource = filtered.ToList();
        }

        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e) { ApplyFilters(); }
        private void cboThuongHieu_SelectedIndexChanged(object sender, EventArgs e) { ApplyFilters(); }
        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e) { ApplyFilters(); }
        private void txtTimKiem_TextChanged(object sender, EventArgs e) { ApplyFilters(); }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLoaiSP.SelectedValue = -1;
            cboThuongHieu.SelectedValue = -1;
            cboTrangThai.SelectedValue = "";
            LoadDataGridView();
        }

        // Nút Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhoHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần điều chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TonKhoDTO item = (TonKhoDTO)dgvKhoHang.SelectedRows[0].DataBoundItem;

            int maSanPham = item.MaSanPham;
            string tenSanPham = item.TenSanPham;
            int soLuong = item.SoLuong ?? 0;

            // TODO: Lấy MaNhanVien từ session/login thực tế
            // Hiện tại dùng giá trị mặc định 1
            int maNhanVien = 1;

            Form_SuaKho formSua = new Form_SuaKho(maSanPham, tenSanPham, soLuong, maNhanVien);
            formSua.ShowDialog();

            if (formSua.IsUpdated)
            {
                LoadDataGridView(); // Reload lại dữ liệu
            }
        }

        // Nút Xuất Excel (xuất danh sách hiện đang hiển thị)
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvKhoHang.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var list = (IList<TonKhoDTO>)dgvKhoHang.DataSource;

            if (list == null || list.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"DanhSachTonKho_{DateTime.Now:yyyyMMdd}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    khoHangBUS.XuatDanhSachTonKhoRaExcel(list, saveFileDialog.FileName);
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi có muốn mở file không
                    DialogResult result = MessageBox.Show("Bạn có muốn mở file Excel vừa xuất không?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu file Excel.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Chọn file Excel nhập kho"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            // TODO: Lấy MaNhanVien từ session/login thực tế
            int maNhanVien = 1;

            try
            {
                var (hasUpdates, errors, updates) = khoHangBUS.NhapKhoTuExcel(ofd.FileName, maNhanVien);

                // Hiển thị kết quả
                string message = "";
                if (errors.Any())
                {
                    message += "Có lỗi trong quá trình nhập:\n" + string.Join("\n", errors) + "\n\n";
                }
                if (updates.Any())
                {
                    message += "Cập nhật thành công:\n" + string.Join("\n", updates);
                }
                if (!errors.Any() && !updates.Any())
                {
                    message = "Không có dữ liệu hợp lệ để cập nhật.";
                }

                MessageBox.Show(message, hasUpdates ? "Kết quả nhập Excel" : "Thông báo", MessageBoxButtons.OK, hasUpdates ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (hasUpdates)
                {
                    LoadDataGridView(); // Reload dữ liệu sau khi cập nhật
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nhập file Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatFileMau_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"MauNhapKho_{DateTime.Now:yyyyMMdd}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    khoHangBUS.XuatFileMauNhapKho(saveFileDialog.FileName);
                    MessageBox.Show("Xuất file mẫu Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi có muốn mở file không
                    DialogResult result = MessageBox.Show("Bạn có muốn mở file mẫu Excel vừa xuất không?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi lưu file mẫu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvKhoHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Không thực hiện hành động nào
        }

        /// <summary>
        /// Hiển thị ToolTip với tên sản phẩm đầy đủ khi di chuột vào cột TenSanPham
        /// </summary>
        private void dgvKhoHang_CellMouseEnter(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                DataGridViewColumn column = dgvKhoHang.Columns[e.ColumnIndex];
                if (column.Name != "TenSanPham")
                    return;

                DataGridViewCell cell = dgvKhoHang.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value == null)
                    return;

                string tenSanPham = cell.Value.ToString() ?? "";

                if (tenSanPham.EndsWith("..."))
                {
                    if (dtProducts != null && e.RowIndex < dtProducts.Count)
                    {
                        string tenDayDu = dtProducts[e.RowIndex].TenSanPham;
                        if (!string.IsNullOrEmpty(tenDayDu) && tenDayDu != tenSanPham)
                        {
                            toolTipTenSP.SetToolTip(dgvKhoHang, $"📦 {tenDayDu}");
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Lỗi không quan trọng, có thể bỏ qua
            }
        }

        private void dgvKhoHang_CellMouseLeave(object? sender, DataGridViewCellEventArgs e)
        {
            toolTipTenSP.SetToolTip(dgvKhoHang, "");
        }

        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            if (dgvKhoHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xem lịch sử!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TonKhoDTO item = (TonKhoDTO)dgvKhoHang.SelectedRows[0].DataBoundItem;
            int maSanPham = item.MaSanPham;
            string tenSanPham = item.TenSanPham;

            Form_LichSuKhoHang formLichSu = new Form_LichSuKhoHang(maSanPham, tenSanPham);
            formLichSu.ShowDialog();
        }
    }
}

