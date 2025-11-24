using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.QuanLy
{
    public partial class UC_PhanQuyen : UserControl
    {
        private PhanQuyen_DAO _dao;
        private List<ChucNangDTO> _chucNangs;
        private List<LoaiQuyenDTO> _loaiQuyens;
        private int _currentRoleId = -1;

        public UC_PhanQuyen()
        {
            InitializeComponent();
            _dao = new PhanQuyen_DAO();
            LoadData();
        }

        private void LoadData()
        {
            // Load Roles
            LoadRoles();

            // Load Metadata for Grid (Functions & Permission Types)
            _chucNangs = (List<ChucNangDTO>)_dao.GetAllChucNang();
            _loaiQuyens = (List<LoaiQuyenDTO>)_dao.GetAllLoaiQuyen();

            InitGridStructure();
        }

        private void LoadRoles()
        {
            var roles = _dao.GetAllRoles();
            listBoxRoles.DataSource = roles;
            listBoxRoles.DisplayMember = "TenQuyen";
            listBoxRoles.ValueMember = "MaQuyen";
        }

        private void InitGridStructure()
        {
            dgvPermissions.Columns.Clear();
            dgvPermissions.Rows.Clear();

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
                row.Cells["colChucNang"].Value = cn.TenChucNang;
                row.Tag = cn.MaChucNang; // Store ID in Tag
            }
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
                for (int i = 1; i < dgvPermissions.Columns.Count; i++)
                {
                    row.Cells[i].Value = false;
                }
            }

            // Fetch permissions
            var perms = _dao.GetChiTietQuyen(roleId);

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
                            _dao.SavePhanQuyen(_currentRoleId, maChucNang, lq.MaLoaiQuyen, isChecked);
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
            string roleName = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên Role mới:", "Thêm Role", "");
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                if (_dao.AddRole(roleName, ""))
                {
                    LoadRoles();
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm Role.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            if (listBoxRoles.SelectedItem is PhanQuyenDTO role)
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa role '{role.TenQuyen}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (_dao.DeleteRole(role.MaQuyen))
                    {
                        LoadRoles();
                        _currentRoleId = -1;
                        // Clear grid
                        foreach (DataGridViewRow row in dgvPermissions.Rows)
                        {
                            for (int i = 1; i < dgvPermissions.Columns.Count; i++)
                            {
                                row.Cells[i].Value = false;
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
    }
}
