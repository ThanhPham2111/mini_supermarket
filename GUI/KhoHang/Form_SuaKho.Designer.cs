namespace mini_supermarket.GUI.KhoHang
{
    partial class Form_SuaKho
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.txtSanPham = new System.Windows.Forms.TextBox();
            this.lblSoLuongHienTai = new System.Windows.Forms.Label();
            this.txtSoLuongHienTai = new System.Windows.Forms.TextBox();
            this.lblSoLuongMoi = new System.Windows.Forms.Label();
            this.numSoLuongMoi = new System.Windows.Forms.NumericUpDown();
            this.lblChenhLech = new System.Windows.Forms.Label();
            this.txtChenhLech = new System.Windows.Forms.TextBox();
            this.lblLoaiThayDoi = new System.Windows.Forms.Label();
            this.cboLoaiThayDoi = new System.Windows.Forms.ComboBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.cboLyDo = new System.Windows.Forms.ComboBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongMoi)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Location = new System.Drawing.Point(60, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(280, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐIỀU CHỈNH SỐ LƯỢNG TỒN KHO";
            // 
            // lblSanPham
            // 
            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSanPham.Location = new System.Drawing.Point(20, 60);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.Size = new System.Drawing.Size(74, 19);
            this.lblSanPham.TabIndex = 1;
            this.lblSanPham.Text = "Sản phẩm:";
            // 
            // txtSanPham
            // 
            this.txtSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSanPham.Location = new System.Drawing.Point(150, 57);
            this.txtSanPham.Name = "txtSanPham";
            this.txtSanPham.ReadOnly = true;
            this.txtSanPham.Size = new System.Drawing.Size(240, 25);
            this.txtSanPham.TabIndex = 2;
            this.txtSanPham.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // lblSoLuongHienTai
            // 
            this.lblSoLuongHienTai.AutoSize = true;
            this.lblSoLuongHienTai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoLuongHienTai.Location = new System.Drawing.Point(20, 95);
            this.lblSoLuongHienTai.Name = "lblSoLuongHienTai";
            this.lblSoLuongHienTai.Size = new System.Drawing.Size(120, 19);
            this.lblSoLuongHienTai.TabIndex = 3;
            this.lblSoLuongHienTai.Text = "Số lượng hiện tại:";
            // 
            // txtSoLuongHienTai
            // 
            this.txtSoLuongHienTai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtSoLuongHienTai.Location = new System.Drawing.Point(150, 92);
            this.txtSoLuongHienTai.Name = "txtSoLuongHienTai";
            this.txtSoLuongHienTai.ReadOnly = true;
            this.txtSoLuongHienTai.Size = new System.Drawing.Size(100, 25);
            this.txtSoLuongHienTai.TabIndex = 4;
            this.txtSoLuongHienTai.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSoLuongHienTai.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSoLuongMoi
            // 
            this.lblSoLuongMoi.AutoSize = true;
            this.lblSoLuongMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSoLuongMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblSoLuongMoi.Location = new System.Drawing.Point(20, 130);
            this.lblSoLuongMoi.Name = "lblSoLuongMoi";
            this.lblSoLuongMoi.Size = new System.Drawing.Size(107, 19);
            this.lblSoLuongMoi.TabIndex = 5;
            this.lblSoLuongMoi.Text = "Số lượng mới:";
            // 
            // numSoLuongMoi
            // 
            this.numSoLuongMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numSoLuongMoi.Location = new System.Drawing.Point(150, 127);
            this.numSoLuongMoi.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.numSoLuongMoi.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numSoLuongMoi.Name = "numSoLuongMoi";
            this.numSoLuongMoi.Size = new System.Drawing.Size(100, 25);
            this.numSoLuongMoi.TabIndex = 6;
            this.numSoLuongMoi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numSoLuongMoi.ValueChanged += new System.EventHandler(this.numSoLuongMoi_ValueChanged);
            // 
            // lblChenhLech
            // 
            this.lblChenhLech.AutoSize = true;
            this.lblChenhLech.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChenhLech.Location = new System.Drawing.Point(20, 165);
            this.lblChenhLech.Name = "lblChenhLech";
            this.lblChenhLech.Size = new System.Drawing.Size(79, 19);
            this.lblChenhLech.TabIndex = 7;
            this.lblChenhLech.Text = "Chênh lệch:";
            // 
            // txtChenhLech
            // 
            this.txtChenhLech.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtChenhLech.Location = new System.Drawing.Point(150, 162);
            this.txtChenhLech.Name = "txtChenhLech";
            this.txtChenhLech.ReadOnly = true;
            this.txtChenhLech.Size = new System.Drawing.Size(100, 25);
            this.txtChenhLech.TabIndex = 8;
            this.txtChenhLech.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtChenhLech.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblLoaiThayDoi
            // 
            this.lblLoaiThayDoi.AutoSize = true;
            this.lblLoaiThayDoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLoaiThayDoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));

            this.lblLoaiThayDoi.Location = new System.Drawing.Point(20, 200);
            this.lblLoaiThayDoi.Name = "lblLoaiThayDoi";
            this.lblLoaiThayDoi.Size = new System.Drawing.Size(99, 19);
            this.lblLoaiThayDoi.TabIndex = 9;
            this.lblLoaiThayDoi.Text = "Loại thay đổi:";
            // 
            // cboLoaiThayDoi
            // 
            this.cboLoaiThayDoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiThayDoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiThayDoi.FormattingEnabled = true;
            this.cboLoaiThayDoi.Items.AddRange(new object[] {
            "Kiểm kê",
            "Điều chỉnh",
            "Hủy hàng"});
            this.cboLoaiThayDoi.Location = new System.Drawing.Point(150, 197);
            this.cboLoaiThayDoi.Name = "cboLoaiThayDoi";
            this.cboLoaiThayDoi.Size = new System.Drawing.Size(240, 25);
            this.cboLoaiThayDoi.TabIndex = 10;
            this.cboLoaiThayDoi.SelectedIndexChanged += new System.EventHandler(this.cboLoaiThayDoi_SelectedIndexChanged);
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLyDo.Location = new System.Drawing.Point(20, 235);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(47, 19);
            this.lblLyDo.TabIndex = 11;
            this.lblLyDo.Text = "Lý do:";
            // 
            // cboLyDo
            // 
            this.cboLyDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLyDo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLyDo.FormattingEnabled = true;
            this.cboLyDo.Location = new System.Drawing.Point(150, 232);
            this.cboLyDo.Name = "cboLyDo";
            this.cboLyDo.Size = new System.Drawing.Size(240, 25);
            this.cboLyDo.TabIndex = 12;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChu.Location = new System.Drawing.Point(20, 270);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(59, 19);
            this.lblGhiChu.TabIndex = 13;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(150, 267);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(240, 50);
            this.txtGhiChu.TabIndex = 14;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(180, 340);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 35);
            this.btnHuy.TabIndex = 15;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(137)))), ((int)(((byte)(62)))));
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(290, 340);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 35);
            this.btnLuu.TabIndex = 16;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // Form_SuaKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(410, 390);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.cboLyDo);
            this.Controls.Add(this.lblLyDo);
            this.Controls.Add(this.cboLoaiThayDoi);
            this.Controls.Add(this.lblLoaiThayDoi);
            this.Controls.Add(this.txtChenhLech);
            this.Controls.Add(this.lblChenhLech);
            this.Controls.Add(this.numSoLuongMoi);
            this.Controls.Add(this.lblSoLuongMoi);
            this.Controls.Add(this.txtSoLuongHienTai);
            this.Controls.Add(this.lblSoLuongHienTai);
            this.Controls.Add(this.txtSanPham);
            this.Controls.Add(this.lblSanPham);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_SuaKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Điều Chỉnh Số Lượng Tồn Kho";
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongMoi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSanPham;
        private System.Windows.Forms.TextBox txtSanPham;
        private System.Windows.Forms.Label lblSoLuongHienTai;
        private System.Windows.Forms.TextBox txtSoLuongHienTai;
        private System.Windows.Forms.Label lblSoLuongMoi;
        private System.Windows.Forms.NumericUpDown numSoLuongMoi;
        private System.Windows.Forms.Label lblChenhLech;
        private System.Windows.Forms.TextBox txtChenhLech;
        private System.Windows.Forms.Label lblLoaiThayDoi;
        private System.Windows.Forms.ComboBox cboLoaiThayDoi;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.ComboBox cboLyDo;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuu;
    }
}
