using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.BUS;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.QuanLy
{
    public partial class UC_PhanQuyen : UserControl
    {
        private PhanQuyen_BUS _bus;
        private List<ChucNangDTO> _chucNangs;
        private List<LoaiQuyenDTO> _loaiQuyens;
        private int _currentRoleId = -1;

        public UC_PhanQuyen()
        {
            InitializeComponent();
            _bus = new PhanQuyen_BUS();
            LoadData();
        }

        private void LoadData()
        {
            // Load Roles
            LoadRoles();

            // Load Metadata for Grid (Functions & Permission Types)
            _chucNangs = (List<ChucNangDTO>)_bus.GetAllChucNang();
            _loaiQuyens = (List<LoaiQuyenDTO>)_bus.GetAllLoaiQuyen();

            InitGridStructure();
        }

        private void LoadRoles()
        {
            var roles = _bus.GetAllRoles();
            listBoxRoles.DataSource = roles;
            listBoxRoles.DisplayMember = "TenQuyen";
            listBoxRoles.ValueMember = "MaQuyen";
        }

        private void InitGridStructure()
        {
            dgvPermissions.Columns.Clear();
            dgvPermissions.Rows.Clear();

            // Column 0: Select All checkbox for this function
            var colSelectAll = new DataGridViewCheckBoxColumn();
            colSelectAll.HeaderText = "Tất cả";
            colSelectAll.Name = "colSelectAll";
            colSelectAll.Width = 80; // Increased from 60
            colSelectAll.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSelectAll.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPermissions.Columns.Add(colSelectAll);

            // Column 1: Function Name (Read-only)
            var colFunc = new DataGridViewTextBoxColumn();
            colFunc.HeaderText = "Chức năng";
            colFunc.Name = "colChucNang";
            colFunc.ReadOnly = true;
            colFunc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Fill remaining space
            dgvPermissions.Columns.Add(colFunc);

            // Dynamic Columns: Permission Types (Checkbox)
            foreach (var lq in _loaiQuyens)
            {
                var col = new DataGridViewCheckBoxColumn();
                col.HeaderText = lq.TenQuyen;
                col.Name = "col_" + lq.MaLoaiQuyen;
                col.Width = 60; // Fixed width
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Fixed
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPermissions.Columns.Add(col);
            }

            // Populate Rows (Functions)
            foreach (var cn in _chucNangs)
            {
                var rowIndex = dgvPermissions.Rows.Add();
                var row = dgvPermissions.Rows[rowIndex];
                row.Cells["colSelectAll"].Value = false;
                row.Cells["colChucNang"].Value = cn.TenChucNang;
                row.Tag = cn.MaChucNang; // Store ID in Tag
            }

            // Add event handler for checkbox changes
            dgvPermissions.CellValueChanged += dgvPermissions_CellValueChanged;
            dgvPermissions.CurrentCellDirtyStateChanged += dgvPermissions_CurrentCellDirtyStateChanged;
        }

        private void listBoxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxRoles.SelectedItem is PhanQuyenDTO role)
            {
                _currentRoleId = role.MaQuyen;
                LoadPermissionsForRole(_currentRoleId);
            }
        }

        private void LoadPermissionsForRole(int roleId)
        {
            // Reset all checkboxes
            foreach (DataGridViewRow row in dgvPermissions.Rows)
            {
                row.Cells["colSelectAll"].Value = false;
                for (int i = 2; i < dgvPermissions.Columns.Count; i++) // Start from 2 (skip SelectAll and ChucNang)
                {
                    row.Cells[i].Value = false;
                }
            }

            // Fetch permissions
            var perms = _bus.GetChiTietQuyen(roleId);

            // Map to Grid
            foreach (var p in perms)
            {
                // Find row by MaChucNang
                foreach (DataGridViewRow row in dgvPermissions.Rows)
                {
                    if ((int)row.Tag == p.MaChucNang)
                    {
                        // Find column by MaLoaiQuyen
                        string colName = "col_" + p.MaLoaiQuyen;
                        if (dgvPermissions.Columns.Contains(colName))
                        {
                            row.Cells[colName].Value = p.DuocPhep;
                        }
                        break;
                    }
                }
            }

            // Update "Select All" checkboxes based on current state
            UpdateSelectAllCheckboxes();
        }

        private void btnSavePermissions_Click(object sender, EventArgs e)
        {
            if (_currentRoleId == -1) return;

            try
            {
                foreach (DataGridViewRow row in dgvPermissions.Rows)
                {
                    int maChucNang = (int)row.Tag;

                    // Iterate dynamic columns
                    foreach (var lq in _loaiQuyens)
                    {
                        string colName = "col_" + lq.MaLoaiQuyen;
                        if (dgvPermissions.Columns.Contains(colName))
                        {
                            bool isChecked = Convert.ToBoolean(row.Cells[colName].Value);
                            _bus.SavePhanQuyen(_currentRoleId, maChucNang, lq.MaLoaiQuyen, isChecked);
                        }
                    }
                }
                MessageBox.Show("Lưu phân quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            using (var dialog = new Dialog_ThemVaiTro())
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string roleName = dialog.TenVaiTro;
                    string moTa = dialog.MoTa;

                    if (_bus.AddRole(roleName, moTa))
                    {
                        MessageBox.Show("Thêm vai trò thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRoles();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi thêm vai trò.", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            if (listBoxRoles.SelectedItem is PhanQuyenDTO role)
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa role '{role.TenQuyen}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (_bus.DeleteRole(role.MaQuyen))
                    {
                        LoadRoles();
                        _currentRoleId = -1;
                        // Clear grid
                        foreach (DataGridViewRow row in dgvPermissions.Rows)
                        {
                            for (int i = 0; i < dgvPermissions.Columns.Count; i++) // Start from 0 to include colSelectAll
                            {
                                if (i != dgvPermissions.Columns["colChucNang"].Index) // Skip function name column
                                {
                                    row.Cells[i].Value = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi xóa Role.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool _isUpdating = false; // Flag to prevent recursive updates

        // Event handler to commit checkbox changes immediately
        private void dgvPermissions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPermissions.IsCurrentCellDirty && dgvPermissions.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvPermissions.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Event handler for checkbox value changes
        private void dgvPermissions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _isUpdating) return;

            var row = dgvPermissions.Rows[e.RowIndex];
            
            // If "Select All" checkbox was changed
            if (e.ColumnIndex == dgvPermissions.Columns["colSelectAll"].Index)
            {
                _isUpdating = true;
                try
                {
                    bool selectAll = Convert.ToBoolean(row.Cells["colSelectAll"].Value);
                    
                    // Set ALL permission checkboxes in this row (including "Xem")
                    for (int i = 2; i < dgvPermissions.Columns.Count; i++) // Skip colSelectAll and colChucNang
                    {
                        row.Cells[i].Value = selectAll;
                    }
                }
                finally
                {
                    _isUpdating = false;
                }
            }
            // If any permission checkbox was changed, update "Select All" checkbox
            else if (e.ColumnIndex >= 2) // Permission columns start from index 2
            {
                UpdateSelectAllForRow(row);
            }
        }

        // Update "Select All" checkbox for a specific row based on its permission checkboxes
        private void UpdateSelectAllForRow(DataGridViewRow row)
        {
            bool allChecked = true;
            bool anyChecked = false;

            for (int i = 2; i < dgvPermissions.Columns.Count; i++)
            {
                bool isChecked = Convert.ToBoolean(row.Cells[i].Value);
                if (isChecked)
                {
                    anyChecked = true;
                }
                else
                {
                    allChecked = false;
                }
            }

            // Set "Select All" to true only if ALL permissions are checked
            // Use _isUpdating flag to prevent triggering CellValueChanged event
            // which would cause all child checkboxes to be unchecked
            _isUpdating = true;
            try
            {
                row.Cells["colSelectAll"].Value = allChecked && anyChecked;
            }
            finally
            {
                _isUpdating = false;
            }
        }

        // Update all "Select All" checkboxes based on current permission states
        private void UpdateSelectAllCheckboxes()
        {
            foreach (DataGridViewRow row in dgvPermissions.Rows)
            {
                UpdateSelectAllForRow(row);
            }
        }
    }
}
