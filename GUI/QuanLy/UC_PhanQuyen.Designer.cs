using System.Windows.Forms;

namespace mini_supermarket.GUI.QuanLy
{
    partial class UC_PhanQuyen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private SplitContainer splitContainer1;
        private GroupBox groupBoxRoles;
        private ListBox listBoxRoles;
        private Panel panelRoleActions;
        private Button btnAddRole;
        private Button btnDeleteRole;
        private GroupBox groupBoxPermissions;
        private DataGridView dgvPermissions;
        private Panel panelPermActions;
        private Button btnSavePermissions;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxRoles = new System.Windows.Forms.GroupBox();
            this.listBoxRoles = new System.Windows.Forms.ListBox();
            this.panelRoleActions = new System.Windows.Forms.Panel();
            this.btnAddRole = new System.Windows.Forms.Button();
            this.btnDeleteRole = new System.Windows.Forms.Button();
            this.groupBoxPermissions = new System.Windows.Forms.GroupBox();
            this.dgvPermissions = new System.Windows.Forms.DataGridView();
            this.panelPermActions = new System.Windows.Forms.Panel();
            this.btnSavePermissions = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxRoles.SuspendLayout();
            this.panelRoleActions.SuspendLayout();
            this.groupBoxPermissions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).BeginInit();
            this.panelPermActions.SuspendLayout();
            this.SuspendLayout();

            // 
            // UC_PhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245); // Light gray background like Form_NhanVien
            this.Controls.Add(this.splitContainer1);
            this.Name = "UC_PhanQuyen";
            this.Size = new System.Drawing.Size(1100, 700);
            this.Padding = new Padding(10);

            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 10);
            this.splitContainer1.Name = "splitContainer1";
            // Important: Set Size before SplitterDistance to avoid clamping
            this.splitContainer1.Size = new System.Drawing.Size(1080, 680);
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1; // Fix the left panel width
            this.splitContainer1.SplitterDistance = 130; // Reduced from 300 to give more space to permissions grid
            this.splitContainer1.IsSplitterFixed = true; // Then lock
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxRoles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxPermissions);

            // 
            // groupBoxRoles
            // 
            this.groupBoxRoles.Controls.Add(this.listBoxRoles);
            this.groupBoxRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRoles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.groupBoxRoles.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRoles.Name = "groupBoxRoles";
            this.groupBoxRoles.Padding = new System.Windows.Forms.Padding(10, 10, 10, 15); // Added bottom padding
            this.groupBoxRoles.Size = new System.Drawing.Size(220, 680); // Updated from 300
            this.groupBoxRoles.TabIndex = 0;
            this.groupBoxRoles.TabStop = false;
            this.groupBoxRoles.Text = "Vai trò"; // Shortened text

            // 
            // listBoxRoles
            // 
            this.listBoxRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxRoles.FormattingEnabled = true;
            this.listBoxRoles.ItemHeight = 25; // Taller items
            this.listBoxRoles.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.listBoxRoles.Location = new System.Drawing.Point(10, 28);
            this.listBoxRoles.Name = "listBoxRoles";
            this.listBoxRoles.IntegralHeight = false;
            this.listBoxRoles.SelectedIndexChanged += new System.EventHandler(this.listBoxRoles_SelectedIndexChanged);

            // 
            // groupBoxPermissions
            // 
            this.groupBoxPermissions.Controls.Add(this.dgvPermissions); // Add Grid LAST (Index 0) so it fills remaining space
            this.groupBoxPermissions.Controls.Add(this.panelPermActions); // Add Panel FIRST (Index 1) so it docks Top first
            
            this.groupBoxPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPermissions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.groupBoxPermissions.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPermissions.Name = "groupBoxPermissions";
            this.groupBoxPermissions.Padding = new System.Windows.Forms.Padding(10, 10, 10, 15); // Added bottom padding
            this.groupBoxPermissions.Size = new System.Drawing.Size(850, 680); // Updated from 790 (1080 - 220 - 10)
            this.groupBoxPermissions.TabIndex = 0;
            this.groupBoxPermissions.TabStop = false;
            this.groupBoxPermissions.Text = "Chi tiết Phân quyền";

            // 
            // dgvPermissions
            // 
            this.dgvPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPermissions.BackgroundColor = System.Drawing.Color.White;
            this.dgvPermissions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvPermissions.AllowUserToAddRows = false;
            this.dgvPermissions.AllowUserToDeleteRows = false;
            this.dgvPermissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPermissions.ColumnHeadersHeight = 40;
            this.dgvPermissions.RowTemplate.Height = 35;
            this.dgvPermissions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPermissions.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.dgvPermissions.Location = new System.Drawing.Point(10, 28);
            this.dgvPermissions.Name = "dgvPermissions";
            this.dgvPermissions.RowHeadersVisible = false;
            this.dgvPermissions.SelectionMode = DataGridViewSelectionMode.CellSelect;

            // 
            // panelPermActions
            // 
            this.panelPermActions.Controls.Add(this.btnAddRole);
            this.panelPermActions.Controls.Add(this.btnDeleteRole);
            this.panelPermActions.Controls.Add(this.btnSavePermissions);
            this.panelPermActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPermActions.Height = 50;
            this.panelPermActions.Padding = new Padding(0, 0, 0, 10);

            // 
            // btnAddRole
            // 
            this.btnAddRole.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRole.BackColor = System.Drawing.Color.FromArgb(0, 120, 215); // Blue
            this.btnAddRole.ForeColor = System.Drawing.Color.White;
            this.btnAddRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddRole.Location = new System.Drawing.Point(0, 0);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(100, 40);
            this.btnAddRole.Text = "Thêm vai trò";
            this.btnAddRole.Click += new System.EventHandler(this.btnAddRole_Click);

            // 
            // btnDeleteRole
            // 
            this.btnDeleteRole.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeleteRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteRole.BackColor = System.Drawing.Color.FromArgb(255, 77, 77); // Red
            this.btnDeleteRole.ForeColor = System.Drawing.Color.White;
            this.btnDeleteRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteRole.Location = new System.Drawing.Point(105, 0);
            this.btnDeleteRole.Margin = new Padding(5, 0, 0, 0);
            this.btnDeleteRole.Name = "btnDeleteRole";
            this.btnDeleteRole.Size = new System.Drawing.Size(100, 40);
            this.btnDeleteRole.Text = "Xóa vai trò";
            this.btnDeleteRole.Click += new System.EventHandler(this.btnDeleteRole_Click);

            // 
            // btnSavePermissions
            // 
            this.btnSavePermissions.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSavePermissions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePermissions.BackColor = System.Drawing.Color.FromArgb(0, 120, 215); // Blue
            this.btnSavePermissions.ForeColor = System.Drawing.Color.White;
            this.btnSavePermissions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSavePermissions.Location = new System.Drawing.Point(670, 0);
            this.btnSavePermissions.Name = "btnSavePermissions";
            this.btnSavePermissions.Size = new System.Drawing.Size(150, 40);
            this.btnSavePermissions.Text = "Lưu thay đổi";
            this.btnSavePermissions.Click += new System.EventHandler(this.btnSavePermissions_Click);

            // Re-arrange controls in Permissions GroupBox to put Actions at top
            this.groupBoxPermissions.Controls.Clear();
            this.groupBoxPermissions.Controls.Add(this.dgvPermissions); // Index 0 (Docked Last -> Fill)
            this.groupBoxPermissions.Controls.Add(this.panelPermActions); // Index 1 (Docked First -> Top)

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxRoles.ResumeLayout(false);
            this.panelRoleActions.ResumeLayout(false);
            this.groupBoxPermissions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).EndInit();
            this.panelPermActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
