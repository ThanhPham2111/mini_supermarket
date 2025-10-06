using System;
using System.Windows.Forms;

namespace mini_supermarket.GUI.PhieuNhap
{
    public partial class Form_PhieuNhap : Form
    {
    public Form_PhieuNhap()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Phiếu Nhập";
            this.Size = new System.Drawing.Size(1170, 750);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // Panel chính
            Panel mainPanel = new Panel
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(1150, 710),
                BackColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(mainPanel);

            // GroupBox thông tin phiếu nhập
            GroupBox inputGroupBox = new GroupBox
            {
                Text = "Thông tin phiếu nhập",
                Location = new System.Drawing.Point(15, 15),
                Size = new System.Drawing.Size(1130, 185),
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(33, 37, 41)
            };
            mainPanel.Controls.Add(inputGroupBox);

            // Layout inputs - 2 cột cân đối
            int labelX = 25, labelY = 35, labelW = 100, labelH = 28;
            int inputX = 135, inputW = 240, inputH = 30, rowH = 42;
            int col2X = 590, col2InputX = 700;
            
      
            inputGroupBox.Controls.Add(new Label { 
                Text = "Mã Phiếu:", 
                Location = new System.Drawing.Point(labelX, labelY), 
                Size = new System.Drawing.Size(labelW, labelH),
                Font = new System.Drawing.Font("Segoe UI", 11),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            });
            TextBox txtPhieuNhap = new TextBox { 
                Location = new System.Drawing.Point(inputX, labelY), 
                Size = new System.Drawing.Size(inputW, inputH),
                Font = new System.Drawing.Font("Segoe UI", 11)
            };
            inputGroupBox.Controls.Add(txtPhieuNhap);
            
            inputGroupBox.Controls.Add(new Label { 
                Text = "Tổng tiền:", 
                Location = new System.Drawing.Point(labelX, labelY + rowH), 
                Size = new System.Drawing.Size(labelW, labelH),
                Font = new System.Drawing.Font("Segoe UI", 11),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            });
            TextBox txtTongTien = new TextBox { 
                Location = new System.Drawing.Point(inputX, labelY + rowH), 
                Size = new System.Drawing.Size(inputW, inputH),
                Font = new System.Drawing.Font("Segoe UI", 11)
            };
            inputGroupBox.Controls.Add(txtTongTien);
            
            inputGroupBox.Controls.Add(new Label { 
                Text = "Trạng thái:", 
                Location = new System.Drawing.Point(labelX, labelY + 2 * rowH), 
                Size = new System.Drawing.Size(labelW, labelH),
                Font = new System.Drawing.Font("Segoe UI", 11),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            });
            TextBox txtTrangThai = new TextBox { 
                Location = new System.Drawing.Point(inputX, labelY + 2 * rowH), 
                Size = new System.Drawing.Size(inputW, inputH),
                Font = new System.Drawing.Font("Segoe UI", 11)
            };
            inputGroupBox.Controls.Add(txtTrangThai);
       
            // Cột 2
            inputGroupBox.Controls.Add(new Label { 
                Text = "Ngày nhập:", 
                Location = new System.Drawing.Point(col2X, labelY), 
                Size = new System.Drawing.Size(100, labelH),
                Font = new System.Drawing.Font("Segoe UI", 11),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            });
            DateTimePicker dateNhap = new DateTimePicker { 
                Location = new System.Drawing.Point(col2InputX, labelY), 
                Size = new System.Drawing.Size(inputW, inputH),
                Font = new System.Drawing.Font("Segoe UI", 11),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd     MM     yyyy"
            };
            inputGroupBox.Controls.Add(dateNhap);
            
            inputGroupBox.Controls.Add(new Label { 
                Text = "Nhà cung:", 
                Location = new System.Drawing.Point(col2X, labelY + rowH), 
                Size = new System.Drawing.Size(100, labelH),
                Font = new System.Drawing.Font("Segoe UI", 11),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            });
            ComboBox cbNhaCungCap = new ComboBox { 
                Location = new System.Drawing.Point(col2InputX, labelY + rowH), 
                Size = new System.Drawing.Size(inputW, inputH),
                Font = new System.Drawing.Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            inputGroupBox.Controls.Add(cbNhaCungCap);

            // Buttons - căn giữa và đẹp hơn
            int btnY = 135, btnW = 95, btnH = 32, btnGap = 15, btnStartX = 480;
            
            Button btnAdd = new Button { 
                Text = "Thêm", 
                Location = new System.Drawing.Point(btnStartX, btnY), 
                Size = new System.Drawing.Size(btnW, btnH), 
                BackColor = System.Drawing.Color.FromArgb(0, 123, 255), 
                ForeColor = System.Drawing.Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            
            Button btnEdit = new Button { 
                Text = "Sửa", 
                Location = new System.Drawing.Point(btnStartX + btnW + btnGap, btnY), 
                Size = new System.Drawing.Size(btnW, btnH), 
                BackColor = System.Drawing.Color.FromArgb(108, 117, 125), 
                ForeColor = System.Drawing.Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            
            Button btnDelete = new Button { 
                Text = "Xóa", 
                Location = new System.Drawing.Point(btnStartX + 2 * (btnW + btnGap), btnY), 
                Size = new System.Drawing.Size(btnW, btnH), 
                BackColor = System.Drawing.Color.FromArgb(220, 53, 69), 
                ForeColor = System.Drawing.Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            
            Button btnRefresh = new Button { 
                Text = "Làm mới", 
                Location = new System.Drawing.Point(btnStartX + 3 * (btnW + btnGap), btnY), 
                Size = new System.Drawing.Size(btnW, btnH), 
                BackColor = System.Drawing.Color.FromArgb(40, 167, 69), 
                ForeColor = System.Drawing.Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            
            inputGroupBox.Controls.Add(btnAdd);
            inputGroupBox.Controls.Add(btnEdit);
            inputGroupBox.Controls.Add(btnDelete);
            inputGroupBox.Controls.Add(btnRefresh);

            // Tìm kiếm
            Label lblSearch = new Label { 
                Text = "Tìm", 
                Location = new System.Drawing.Point(20, 220), 
                Size = new System.Drawing.Size(50, 32), 
                Font = new System.Drawing.Font("Segoe UI", 10),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblSearch);
            
            TextBox txtSearch = new TextBox { 
                Location = new System.Drawing.Point(70, 220), 
                Size = new System.Drawing.Size(500, 32), 
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            mainPanel.Controls.Add(txtSearch);
            
            Button btnSearch = new Button { 
                Text = "Tìm", 
                Location = new System.Drawing.Point(580, 220), 
                Size = new System.Drawing.Size(85, 32), 
                BackColor = System.Drawing.Color.FromArgb(0, 123, 255), 
                ForeColor = System.Drawing.Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            mainPanel.Controls.Add(btnSearch);

            // DataGridView - hiển thị dữ liệu
            DataGridView dgvPhieuNhap = new DataGridView
            {
                Location = new System.Drawing.Point(20, 270),
                Size = new System.Drawing.Size(1120, 360),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 },
                Font = new System.Drawing.Font("Segoe UI", 9),
                GridColor = System.Drawing.Color.FromArgb(220, 220, 220),
                EnableHeadersVisualStyles = false
            };
            
          
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
          
            dgvPhieuNhap.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            dgvPhieuNhap.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            dgvPhieuNhap.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            
            dgvPhieuNhap.Columns.Add("MaCTPN", "Mã chi tiết PN");
            dgvPhieuNhap.Columns.Add("MaSP", "Mã sản phẩm");
            dgvPhieuNhap.Columns.Add("SoLuong", "Số lượng");
            dgvPhieuNhap.Columns.Add("DonGiaNhap", "Đơn giá nhập");
            dgvPhieuNhap.Columns.Add("ThanhTien", "Thành tiền");
            mainPanel.Controls.Add(dgvPhieuNhap);
        }
    }
}
