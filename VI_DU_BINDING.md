# VÍ DỤ CODE BINDING - TỪNG BƯỚC

## 📋 MỤC LỤC
1. [Ví dụ đơn giản nhất](#1-ví-dụ-đơn-giản-nhất)
2. [Ví dụ có Filter](#2-ví-dụ-có-filter)
3. [Ví dụ có Format giá trị](#3-ví-dụ-có-format-giá-trị)
4. [Ví dụ đầy đủ như Form_TaiKhoan](#4-ví-dụ-đầy-đủ-như-form_taikhoan)

---

## 1. VÍ DỤ ĐƠN GIẢN NHẤT

### Mục tiêu: Hiển thị danh sách tài khoản trong DataGridView

```csharp
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

public partial class Form_Example : Form
{
    // ========== BƯỚC 1: KHAI BÁO ==========
    private readonly BindingSource _bindingSource = new();
    private BindingList<TaiKhoanDTO> _currentTaiKhoan = new();
    private TaiKhoan_BUS _bus = new TaiKhoan_BUS();
    
    private DataGridView dgvTaiKhoan; // Giả sử đã có trong Designer
    
    public Form_Example()
    {
        InitializeComponent();
        Load += Form_Example_Load;
    }
    
    // ========== BƯỚC 2: SETUP ==========
    private void Form_Example_Load(object? sender, EventArgs e)
    {
        // Cấu hình DataGridView
        dgvTaiKhoan.AutoGenerateColumns = false;
        dgvTaiKhoan.DataSource = _bindingSource;  // ← Kết nối!
        
        // Load dữ liệu
        LoadData();
    }
    
    // ========== BƯỚC 3: LOAD DỮ LIỆU ==========
    private void LoadData()
    {
        // 1. Lấy dữ liệu từ BUS
        var list = _bus.GetTaiKhoan().ToList();
        
        // 2. Tạo BindingList
        _currentTaiKhoan = new BindingList<TaiKhoanDTO>(list);
        
        // 3. Gán vào BindingSource
        _bindingSource.DataSource = _currentTaiKhoan;
        
        // 4. DataGridView TỰ ĐỘNG hiển thị!
        // Không cần loop, không cần Rows.Add()
    }
}
```

**Kết quả:**
- DataGridView tự động hiển thị tất cả tài khoản
- Mỗi row = 1 TaiKhoanDTO
- Khi _currentTaiKhoan thay đổi → DataGridView tự động cập nhật

---

## 2. VÍ DỤ CÓ FILTER

### Mục tiêu: Filter theo trạng thái mà không làm mất dữ liệu gốc

```csharp
public partial class Form_Example : Form
{
    private readonly BindingSource _bindingSource = new();
    private BindingList<TaiKhoanDTO> _currentTaiKhoan = new(); // ← Dữ liệu GỐC
    private ComboBox statusComboBox; // ComboBox filter
    
    private void LoadData()
    {
        var list = _bus.GetTaiKhoan().ToList();
        _currentTaiKhoan = new BindingList<TaiKhoanDTO>(list); // Lưu TẤT CẢ
        
        // Ban đầu hiển thị tất cả
        _bindingSource.DataSource = _currentTaiKhoan;
    }
    
    // ========== FILTER ==========
    private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        string? selectedStatus = statusComboBox.SelectedItem as string;
        
        if (selectedStatus == "Tất cả")
        {
            // Hiển thị TẤT CẢ - dùng dữ liệu gốc
            _bindingSource.DataSource = _currentTaiKhoan;
        }
        else
        {
            // Filter - tạo BindingList MỚI
            var filtered = new BindingList<TaiKhoanDTO>();
            
            foreach (var tk in _currentTaiKhoan) // ← Duyệt dữ liệu GỐC
            {
                if (tk.TrangThai == selectedStatus)
                {
                    filtered.Add(tk);
                }
            }
            
            // Gán vào BindingSource
            _bindingSource.DataSource = filtered;
            // DataGridView TỰ ĐỘNG chỉ hiển thị filtered items
        }
        
        // Lưu ý: _currentTaiKhoan VẪN GIỮ NGUYÊN tất cả dữ liệu!
    }
}
```

**Điểm quan trọng:**
- ✅ `_currentTaiKhoan`: Giữ nguyên TẤT CẢ dữ liệu
- ✅ `filtered`: Chỉ chứa dữ liệu đã filter
- ✅ `_bindingSource.DataSource`: Có thể là gốc hoặc filtered
- ✅ Khi đổi `DataSource` → DataGridView tự động refresh

---

## 3. VÍ DỤ CÓ FORMAT GIÁ TRỊ

### Mục tiêu: Hiển thị tên nhân viên thay vì mã nhân viên

```csharp
public partial class Form_Example : Form
{
    private readonly BindingSource _bindingSource = new();
    private BindingList<TaiKhoanDTO> _currentTaiKhoan = new();
    private Dictionary<int, string> _nhanVienMap = new();
    
    private void Form_Example_Load(object? sender, EventArgs e)
    {
        // Load map nhân viên
        LoadNhanVienMap();
        
        // Setup DataGridView
        dgvTaiKhoan.AutoGenerateColumns = false;
        dgvTaiKhoan.DataSource = _bindingSource;
        
        // Đăng ký event để format giá trị
        dgvTaiKhoan.DataBindingComplete += DgvTaiKhoan_DataBindingComplete;
        
        LoadData();
    }
    
    private void LoadNhanVienMap()
    {
        var nhanVienList = _nhanVienBus.GetAll();
        _nhanVienMap = nhanVienList.ToDictionary(
            nv => nv.MaNhanVien, 
            nv => nv.TenNhanVien ?? $"NV{nv.MaNhanVien}"
        );
        // Kết quả: {1: "Nguyễn Văn A", 2: "Trần Thị B", ...}
    }
    
    // ========== FORMAT GIÁ TRỊ SAU KHI BINDING ==========
    private void DgvTaiKhoan_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        // Duyệt qua tất cả rows
        foreach (DataGridViewRow row in dgvTaiKhoan.Rows)
        {
            // Lấy object từ row
            if (row.DataBoundItem is TaiKhoanDTO tk)
            {
                // Cập nhật cột "Tên nhân viên" với giá trị từ map
                if (_nhanVienMap.TryGetValue(tk.MaNhanVien, out var tenNhanVien))
                {
                    row.Cells["tenNhanVienColumn"].Value = tenNhanVien;
                    // Hiển thị "Nguyễn Văn A" thay vì mã "1"
                }
            }
        }
    }
}
```

**Luồng xử lý:**
1. Binding hoàn tất → DataGridView có rows với dữ liệu gốc
2. `DataBindingComplete` event được gọi
3. Duyệt từng row → Lấy MaNhanVien từ DTO
4. Tra map → Lấy TenNhanVien
5. Cập nhật giá trị hiển thị

---

## 4. VÍ DỤ ĐẦY ĐỦ NHƯ FORM_TAIKHOAN

### Bao gồm: Load, Filter, Search, SelectionChanged

```csharp
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

public partial class Form_TaiKhoan : Form
{
    // ========== KHAI BÁO ==========
    private readonly BindingSource _bindingSource = new();
    private BindingList<TaiKhoanDTO> _currentTaiKhoan = new();
    private Dictionary<int, string> _nhanVienMap = new();
    private Dictionary<int, string> _quyenMap = new();
    
    private TaiKhoan_BUS _taiKhoanBus = new();
    private NhanVien_BUS _nhanVienBus = new();
    
    // Controls (giả sử đã có trong Designer)
    private DataGridView taiKhoanDataGridView;
    private ComboBox statusFilterComboBox;
    private TextBox searchTextBox;
    private TextBox maTaiKhoanTextBox, tenDangNhapTextBox;
    
    public Form_TaiKhoan()
    {
        InitializeComponent();
        Load += Form_TaiKhoan_Load;
    }
    
    // ========== SETUP ==========
    private void Form_TaiKhoan_Load(object? sender, EventArgs e)
    {
        // 1. Load maps
        LoadNhanVienMap();
        LoadQuyenMap();
        
        // 2. Setup ComboBox filter
        statusFilterComboBox.Items.Add("Tất cả");
        statusFilterComboBox.Items.Add("Hoạt động");
        statusFilterComboBox.Items.Add("Khóa");
        statusFilterComboBox.SelectedIndex = 0;
        statusFilterComboBox.SelectedIndexChanged += StatusFilterComboBox_SelectedIndexChanged;
        
        // 3. Setup DataGridView
        taiKhoanDataGridView.AutoGenerateColumns = false;
        taiKhoanDataGridView.DataSource = _bindingSource; // ← Kết nối!
        taiKhoanDataGridView.SelectionChanged += TaiKhoanDataGridView_SelectionChanged;
        taiKhoanDataGridView.DataBindingComplete += TaiKhoanDataGridView_DataBindingComplete;
        
        // 4. Setup Search
        searchTextBox.TextChanged += SearchTextBox_TextChanged;
        
        // 5. Load dữ liệu
        LoadTaiKhoanData();
    }
    
    // ========== LOAD MAPS ==========
    private void LoadNhanVienMap()
    {
        var nhanVienList = _nhanVienBus.GetAll();
        _nhanVienMap = nhanVienList.ToDictionary(
            nv => nv.MaNhanVien, 
            nv => nv.TenNhanVien ?? $"NV{nv.MaNhanVien}"
        );
    }
    
    private void LoadQuyenMap()
    {
        var quyenList = _taiKhoanBus.GetAllPhanQuyen();
        _quyenMap = quyenList.ToDictionary(q => q.MaQuyen, q => q.TenQuyen);
    }
    
    // ========== LOAD DỮ LIỆU ==========
    private void LoadTaiKhoanData()
    {
        // 1. Lấy từ BUS
        var list = _taiKhoanBus.GetTaiKhoan().ToList();
        
        // 2. Tạo BindingList
        _currentTaiKhoan = new BindingList<TaiKhoanDTO>(list);
        
        // 3. Áp dụng filter (sẽ set _bindingSource.DataSource)
        ApplyStatusFilter();
    }
    
    // ========== FILTER THEO TRẠNG THÁI ==========
    private void StatusFilterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ApplyStatusFilter();
    }
    
    private void ApplyStatusFilter()
    {
        string? selectedStatus = statusFilterComboBox.SelectedItem as string;
        
        if (selectedStatus == "Tất cả")
        {
            // Hiển thị tất cả
            _bindingSource.DataSource = _currentTaiKhoan;
        }
        else
        {
            // Filter
            var filtered = new BindingList<TaiKhoanDTO>();
            foreach (var tk in _currentTaiKhoan)
            {
                if (tk.TrangThai == selectedStatus)
                {
                    filtered.Add(tk);
                }
            }
            _bindingSource.DataSource = filtered;
        }
        
        // Nếu có search text, áp dụng search luôn
        if (!string.IsNullOrEmpty(searchTextBox.Text))
        {
            ApplySearchFilter();
        }
    }
    
    // ========== SEARCH ==========
    private void SearchTextBox_TextChanged(object? sender, EventArgs e)
    {
        ApplySearchFilter();
    }
    
    private void ApplySearchFilter()
    {
        string searchText = searchTextBox.Text.Trim().ToLower();
        string? selectedStatus = statusFilterComboBox.SelectedItem as string;
        
        var filtered = new BindingList<TaiKhoanDTO>();
        
        foreach (var tk in _currentTaiKhoan) // ← Duyệt dữ liệu GỐC
        {
            // Check status filter
            bool matchesStatus = selectedStatus == "Tất cả" || tk.TrangThai == selectedStatus;
            
            // Check search
            bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                tk.MaTaiKhoan.ToString().Contains(searchText) ||
                (tk.TenDangNhap?.ToLower().Contains(searchText) ?? false) ||
                (_nhanVienMap.TryGetValue(tk.MaNhanVien, out var tenNV) && 
                 tenNV.ToLower().Contains(searchText));
            
            if (matchesStatus && matchesSearch)
            {
                filtered.Add(tk);
            }
        }
        
        _bindingSource.DataSource = filtered;
    }
    
    // ========== USER CHỌN ROW ==========
    private void TaiKhoanDataGridView_SelectionChanged(object? sender, EventArgs e)
    {
        if (taiKhoanDataGridView.SelectedRows.Count > 0)
        {
            // Lấy object từ row được chọn
            var selectedTaiKhoan = (TaiKhoanDTO)taiKhoanDataGridView.SelectedRows[0].DataBoundItem;
            
            // Fill vào TextBox
            maTaiKhoanTextBox.Text = selectedTaiKhoan.MaTaiKhoan.ToString();
            tenDangNhapTextBox.Text = selectedTaiKhoan.TenDangNhap ?? string.Empty;
            
            // Hiển thị tên nhân viên từ map
            if (_nhanVienMap.TryGetValue(selectedTaiKhoan.MaNhanVien, out var tenNV))
            {
                // nhanVienTextBox.Text = tenNV; // Nếu có TextBox này
            }
        }
    }
    
    // ========== FORMAT DISPLAY VALUES ==========
    private void TaiKhoanDataGridView_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        foreach (DataGridViewRow row in taiKhoanDataGridView.Rows)
        {
            if (row.DataBoundItem is TaiKhoanDTO tk)
            {
                // Hiển thị tên nhân viên thay vì mã
                row.Cells["tenNhanVienColumn"].Value = 
                    _nhanVienMap.TryGetValue(tk.MaNhanVien, out var ten) ? ten : "";
                
                // Hiển thị tên quyền thay vì mã
                row.Cells["tenQuyenColumn"].Value = 
                    _quyenMap.TryGetValue(tk.MaQuyen, out var q) ? q : "";
            }
        }
    }
}
```

---

## 5. SO SÁNH: TRƯỚC VÀ SAU KHI DÙNG BINDING

### ❌ TRƯỚC (Không dùng Binding)

```csharp
private void LoadData()
{
    var list = _bus.GetTaiKhoan();
    
    // Phải tự clear
    dgvTaiKhoan.Rows.Clear();
    
    // Phải tự loop và add
    foreach (var tk in list)
    {
        dgvTaiKhoan.Rows.Add(
            tk.MaTaiKhoan,
            tk.TenDangNhap,
            tk.TrangThai
        );
    }
}

private void FilterData()
{
    var list = _bus.GetTaiKhoan();
    string status = statusComboBox.SelectedItem.ToString();
    
    // Phải clear lại
    dgvTaiKhoan.Rows.Clear();
    
    // Phải loop lại
    foreach (var tk in list)
    {
        if (tk.TrangThai == status)
        {
            dgvTaiKhoan.Rows.Add(...);
        }
    }
}
```

**Nhược điểm:**
- Code dài, lặp lại
- Phải tự quản lý rows
- Khó maintain

### ✅ SAU (Dùng Binding)

```csharp
private void LoadData()
{
    var list = _bus.GetTaiKhoan().ToList();
    _currentTaiKhoan = new BindingList<TaiKhoanDTO>(list);
    _bindingSource.DataSource = _currentTaiKhoan;
    // Xong! DataGridView tự động hiển thị
}

private void FilterData()
{
    string status = statusComboBox.SelectedItem.ToString();
    var filtered = new BindingList<TaiKhoanDTO>(
        _currentTaiKhoan.Where(tk => tk.TrangThai == status).ToList()
    );
    _bindingSource.DataSource = filtered;
    // Xong! DataGridView tự động cập nhật
}
```

**Ưu điểm:**
- Code ngắn gọn
- Tự động cập nhật UI
- Dễ maintain

---

## 6. CHECKLIST KHI DÙNG BINDING

### ✅ Setup cơ bản
- [ ] Khai báo `BindingSource _bindingSource = new()`
- [ ] Khai báo `BindingList<T> _currentData = new()`
- [ ] Set `DataGridView.AutoGenerateColumns = false`
- [ ] Set `DataGridView.DataSource = _bindingSource`

### ✅ Load dữ liệu
- [ ] Lấy dữ liệu từ BUS → `List<T>`
- [ ] Tạo `BindingList` từ List
- [ ] Gán vào `_bindingSource.DataSource`

### ✅ Filter/Search
- [ ] Giữ nguyên `_currentData` (dữ liệu gốc)
- [ ] Tạo `BindingList` mới từ filtered data
- [ ] Gán vào `_bindingSource.DataSource`

### ✅ Events
- [ ] `SelectionChanged`: Lấy `DataBoundItem` từ `SelectedRows[0]`
- [ ] `DataBindingComplete`: Format display values nếu cần

---

**Chúc bạn code vui vẻ! 🚀**

