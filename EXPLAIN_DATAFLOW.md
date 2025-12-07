# 📊 Luồng Dữ Liệu: Database → DataGridView

## 🔄 Tổng quan luồng dữ liệu

```
Database (SQL Server)
    ↓
DAO Layer (NhanVien_DAO.cs)
    ↓
BUS Layer (NhanVien_BUS.cs)
    ↓
GUI Layer (Form_NhanVien.cs)
    ↓
BindingSource
    ↓
DataGridView (Hiển thị trên màn hình)
```

---

## 📝 Chi tiết từng bước

### **Bước 1: Database → DAO (Entity Framework)**

**File:** `DAO/NhanVien_DAO.cs`

```csharp
public IList<NhanVienDTO> GetNhanVien(string? trangThaiFilter = null)
{
    // 1. Tạo DbContext để kết nối database
    using var context = new NhanVienDbContext();
    
    // 2. Tạo query LINQ (chưa chạy SQL)
    var query = context.TblNhanVien.AsQueryable();

    // 3. Thêm điều kiện filter nếu có
    if (!string.IsNullOrWhiteSpace(trangThaiFilter))
    {
        query = query.Where(nv => nv.TrangThai == trangThaiFilter);
    }

    // 4. Sắp xếp và chuyển đổi sang DTO
    return query
        .OrderBy(nv => nv.MaNhanVien)
        .Select(nv => new NhanVienDTO  // ← Map từ Entity sang DTO
        {
            MaNhanVien = nv.MaNhanVien,
            TenNhanVien = nv.TenNhanVien,
            // ... các field khác
        })
        .ToList();  // ← THỰC SỰ chạy SQL và trả về List
}
```

**Điều gì xảy ra:**
- Entity Framework dịch LINQ query thành SQL
- Chạy SQL trên database: `SELECT * FROM Tbl_NhanVien ORDER BY MaNhanVien`
- Trả về `IList<NhanVienDTO>` (danh sách nhân viên)

---

### **Bước 2: DAO → BUS**

**File:** `BUS/NhanVien_BUS.cs`

```csharp
private readonly NhanVien_DAO _nhanVienDao = new();

public IList<NhanVienDTO> GetNhanVien(string? trangThaiFilter = null)
{
    // Gọi trực tiếp DAO, có thể thêm validation ở đây
    return _nhanVienDao.GetNhanVien(trangThaiFilter);
}
```

**Điều gì xảy ra:**
- BUS layer nhận dữ liệu từ DAO
- Có thể thêm business logic (validation, xử lý) nếu cần
- Trả về `IList<NhanVienDTO>` cho GUI

---

### **Bước 3: BUS → GUI (Form_NhanVien)**

**File:** `GUI/NhanVien/Form_NhanVien.cs`

#### **3.1. Khởi tạo BindingSource**

```csharp
private readonly BindingSource _bindingSource = new();
private readonly NhanVien_BUS _nhanVienBus = new();
private IList<NhanVienDTO> _currentNhanVien = Array.Empty<NhanVienDTO>();

// Trong Form_Load
nhanVienDataGridView.AutoGenerateColumns = false;  // Tắt tự động tạo cột
nhanVienDataGridView.DataSource = _bindingSource;  // Gán BindingSource
```

**BindingSource là gì?**
- Là lớp trung gian giữa DataGridView và dữ liệu
- Cho phép filter, sort, search mà không cần query lại database
- Tự động cập nhật DataGridView khi dữ liệu thay đổi

#### **3.2. Load dữ liệu từ database**

```csharp
private void LoadNhanVienData()
{
    try
    {
        // Gọi BUS để lấy dữ liệu từ database
        _currentNhanVien = _nhanVienBus.GetNhanVien();
        
        // Áp dụng filter và hiển thị
        ApplyStatusFilter();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Không thể tải danh sách nhân viên.\n\n{ex.Message}", 
            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

**Điều gì xảy ra:**
- Gọi `_nhanVienBus.GetNhanVien()` → trả về `IList<NhanVienDTO>`
- Lưu vào `_currentNhanVien`
- Gọi `ApplyStatusFilter()` để hiển thị

#### **3.3. Gán dữ liệu vào BindingSource**

```csharp
private void ApplyStatusFilter()
{
    string? selectedStatus = statusFilterComboBox.SelectedItem as string;

    if (string.IsNullOrWhiteSpace(selectedStatus) || 
        string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase))
    {
        // Hiển thị tất cả
        _bindingSource.DataSource = _currentNhanVien;  // ← Gán toàn bộ danh sách
    }
    else
    {
        // Filter theo trạng thái
        var filtered = new List<NhanVienDTO>();
        foreach (var nhanVien in _currentNhanVien)
        {
            if (string.Equals(nhanVien.TrangThai, selectedStatus, 
                StringComparison.OrdinalIgnoreCase))
            {
                filtered.Add(nhanVien);
            }
        }
        _bindingSource.DataSource = filtered;  // ← Gán danh sách đã filter
    }
}
```

**Điều gì xảy ra:**
- Gán `IList<NhanVienDTO>` vào `_bindingSource.DataSource`
- BindingSource tự động cập nhật DataGridView

---

### **Bước 4: BindingSource → DataGridView**

**File:** `GUI/NhanVien/Form_NhanVien.Designer.cs`

#### **4.1. Cấu hình DataGridView**

```csharp
// DataGridView đã được gán BindingSource
nhanVienDataGridView.DataSource = _bindingSource;

// Cấu hình các cột
nhanVienDataGridView.AutoGenerateColumns = false;  // Tắt tự động
nhanVienDataGridView.ReadOnly = true;  // Chỉ đọc
nhanVienDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
```

#### **4.2. Định nghĩa các cột**

```csharp
// Cột Mã NV
maNhanVienColumn.DataPropertyName = "MaNhanVien";  // ← Map với property của DTO
maNhanVienColumn.HeaderText = "Mã NV";
maNhanVienColumn.Name = "maNhanVienColumn";

// Cột Họ tên
hoTenColumn.DataPropertyName = "TenNhanVien";  // ← Map với property TenNhanVien
hoTenColumn.HeaderText = "Họ tên";

// Cột Ngày sinh
ngaySinhColumn.DataPropertyName = "NgaySinh";
ngaySinhColumn.DefaultCellStyle.Format = "dd/MM/yyyy";  // Format ngày tháng

// ... các cột khác
```

**DataPropertyName là gì?**
- Là tên property trong DTO class (`NhanVienDTO`)
- DataGridView tự động lấy giá trị từ property này để hiển thị
- Ví dụ: `DataPropertyName = "TenNhanVien"` → lấy `nhanVien.TenNhanVien`

---

## 🎯 Tóm tắt luồng hoàn chỉnh

```
1. User mở Form_NhanVien
   ↓
2. Form_Load() được gọi
   ↓
3. LoadNhanVienData() được gọi
   ↓
4. _nhanVienBus.GetNhanVien()
   ↓
5. _nhanVienDao.GetNhanVien()
   ↓
6. Entity Framework chạy SQL: SELECT * FROM Tbl_NhanVien
   ↓
7. Trả về IList<NhanVienDTO>
   ↓
8. Lưu vào _currentNhanVien
   ↓
9. ApplyStatusFilter()
   ↓
10. _bindingSource.DataSource = _currentNhanVien
   ↓
11. DataGridView tự động hiển thị dữ liệu
    (vì đã gán DataSource = _bindingSource)
```

---

## 🔍 Cách DataGridView tự động bind

Khi bạn gán `_bindingSource.DataSource = _currentNhanVien`:

1. **DataGridView** nhận biết BindingSource đã có dữ liệu mới
2. **Duyệt qua từng item** trong `_currentNhanVien` (mỗi item là `NhanVienDTO`)
3. **Với mỗi cột**, lấy giá trị từ property tương ứng:
   - Cột `maNhanVienColumn` → lấy `nhanVien.MaNhanVien`
   - Cột `hoTenColumn` → lấy `nhanVien.TenNhanVien`
   - Cột `ngaySinhColumn` → lấy `nhanVien.NgaySinh` và format `dd/MM/yyyy`
4. **Hiển thị** từng dòng trong DataGridView

---

## 💡 Ví dụ minh họa

Giả sử database có 3 nhân viên:

| MaNhanVien | TenNhanVien | GioiTinh | NgaySinh | SoDienThoai | VaiTro | TrangThai |
|------------|-------------|----------|----------|-------------|--------|-----------|
| 1 | Nguyễn Văn A | Nam | 1990-01-01 | 0123456789 | Thu ngân | Đang làm |
| 2 | Trần Thị B | Nữ | 1995-05-15 | 0987654321 | Thủ kho | Đang làm |
| 3 | Lê Văn C | Nam | 1988-12-20 | 0111222333 | Quản lý | Đã nghỉ |

**Luồng dữ liệu:**

1. **EF chạy SQL** → Trả về 3 records
2. **Map sang DTO** → `List<NhanVienDTO>` có 3 items
3. **Gán vào BindingSource** → `_bindingSource.DataSource = list`
4. **DataGridView hiển thị:**

```
| Mã NV | Chức vụ | Họ tên      | Ngày sinh | Giới tính | SĐT       | Trạng thái |
|-------|---------|-------------|-----------|----------|-----------|------------|
| 1     | Thu ngân| Nguyễn Văn A| 01/01/1990| Nam      | 0123456789| Đang làm   |
| 2     | Thủ kho | Trần Thị B  | 15/05/1995| Nữ      | 0987654321| Đang làm   |
| 3     | Quản lý | Lê Văn C    | 20/12/1988| Nam      | 0111222333| Đã nghỉ    |
```

---

## ✅ Tóm tắt

1. **Database** → Dữ liệu thô (SQL Server)
2. **DAO** → Entity Framework chạy SQL, map sang DTO
3. **BUS** → Nhận DTO, có thể thêm business logic
4. **GUI** → Gọi BUS, lưu vào `_currentNhanVien`
5. **BindingSource** → Trung gian giữa GUI và dữ liệu
6. **DataGridView** → Tự động bind và hiển thị

**BindingSource.DataSource** là chìa khóa - khi gán dữ liệu vào đây, DataGridView tự động cập nhật!

