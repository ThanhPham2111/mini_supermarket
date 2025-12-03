using System;
using System.Windows.Forms;

namespace mini_supermarket.GUI.QuanLy
{
    public partial class Form_QuanLy : Form
    {
        public Form_QuanLy()
        {
            InitializeComponent();
            LoadTabs();
        }

        private void InitializeComponent()
        {
            this.tabControlQuanLy = new System.Windows.Forms.TabControl();
            this.tabPhanQuyen = new System.Windows.Forms.TabPage();
            this.tabLoiNhuan = new System.Windows.Forms.TabPage();
            this.tabQuyDoiDiem = new System.Windows.Forms.TabPage();
            this.tabControlQuanLy.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlQuanLy
            // 
            this.tabControlQuanLy.Controls.Add(this.tabPhanQuyen);
            this.tabControlQuanLy.Controls.Add(this.tabLoiNhuan);
            this.tabControlQuanLy.Controls.Add(this.tabQuyDoiDiem);
            this.tabControlQuanLy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlQuanLy.Location = new System.Drawing.Point(0, 0);
            this.tabControlQuanLy.Name = "tabControlQuanLy";
            this.tabControlQuanLy.SelectedIndex = 0;
            this.tabControlQuanLy.Size = new System.Drawing.Size(800, 450);
            this.tabControlQuanLy.TabIndex = 0;
            // 
            // tabPhanQuyen
            // 
            this.tabPhanQuyen.Location = new System.Drawing.Point(4, 24);
            this.tabPhanQuyen.Name = "tabPhanQuyen";
            this.tabPhanQuyen.Padding = new System.Windows.Forms.Padding(3);
            this.tabPhanQuyen.Size = new System.Drawing.Size(792, 422);
            this.tabPhanQuyen.TabIndex = 0;
            this.tabPhanQuyen.Text = "Phân quyền";
            this.tabPhanQuyen.UseVisualStyleBackColor = true;
            // 
            // tabLoiNhuan
            // 
            this.tabLoiNhuan.Location = new System.Drawing.Point(4, 24);
            this.tabLoiNhuan.Name = "tabLoiNhuan";
            this.tabLoiNhuan.Padding = new System.Windows.Forms.Padding(3);
            this.tabLoiNhuan.Size = new System.Drawing.Size(792, 422);
            this.tabLoiNhuan.TabIndex = 1;
            this.tabLoiNhuan.Text = "% Lợi nhuận";
            this.tabLoiNhuan.UseVisualStyleBackColor = true;
            // 
            // tabQuyDoiDiem
            // 
            this.tabQuyDoiDiem.Location = new System.Drawing.Point(4, 24);
            this.tabQuyDoiDiem.Name = "tabQuyDoiDiem";
            this.tabQuyDoiDiem.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuyDoiDiem.Size = new System.Drawing.Size(792, 422);
            this.tabQuyDoiDiem.TabIndex = 2;
            this.tabQuyDoiDiem.Text = "Quy đổi điểm KH";
            this.tabQuyDoiDiem.UseVisualStyleBackColor = true;
            // 
            // Form_QuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlQuanLy);
            this.Name = "Form_QuanLy";
            this.Text = "Quản lý hệ thống";
            this.tabControlQuanLy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControlQuanLy;
        private System.Windows.Forms.TabPage tabPhanQuyen;
        private System.Windows.Forms.TabPage tabLoiNhuan;
        private System.Windows.Forms.TabPage tabQuyDoiDiem;

        private void LoadTabs()
        {
            // Load UC_PhanQuyen into tabPhanQuyen
            var ucPhanQuyen = new UC_PhanQuyen();
            ucPhanQuyen.Dock = DockStyle.Fill;
            tabPhanQuyen.Controls.Add(ucPhanQuyen);

            // Load UC_LoiNhuan into tabLoiNhuan
            var ucLoiNhuan = new UC_LoiNhuan();
            ucLoiNhuan.Dock = DockStyle.Fill;
            tabLoiNhuan.Controls.Add(ucLoiNhuan);

            // Load UC_QuyDoiDiem into tabQuyDoiDiem
            var ucQuyDoiDiem = new UC_QuyDoiDiem();
            ucQuyDoiDiem.Dock = DockStyle.Fill;
            tabQuyDoiDiem.Controls.Add(ucQuyDoiDiem);
        }
    }
}
