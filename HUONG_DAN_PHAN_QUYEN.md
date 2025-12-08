# HƯỚNG DẪN HỆ THỐNG PHÂN QUYỀN ĐỘNG

## 📋 MỤC LỤC
1. [Quản lý Session](#1-quản-lý-session)
2. [Hệ thống Phân quyền Động](#2-hệ-thống-phân-quyền-động)
3. [Cách Sidebar Ẩn/Hiện Chức năng](#3-cách-sidebar-ẩnhiện-chức-năng)
4. [Cách Ẩn Chức năng Con trong Form](#4-cách-ẩn-chức-năng-con-trong-form)

---

## 1. QUẢN LÝ SESSION

### 1.1. SessionManager - Lưu trữ thông tin đăng nhập

**File:** `Common/SessionManager.cs`

```csharp
public static class SessionManager
{
    private static TaiKhoanDTO? _currentUser;      // Tài khoản hiện tại
    private static NhanVienDTO? _currentNhanVien;  // Nhân viên tương ứng
    
    // Properties để truy cập từ bất kỳ đâu
    public static TaiKhoanDTO? CurrentUser => _currentUser;
    public static NhanVienDTO? CurrentNhanVien => _currentNhanVien;
    public static int? CurrentMaQuyen => CurrentUser?.MaQuyen;  // Mã quyền của user
    public static int? CurrentMaNhanVien => CurrentUser?.MaNhanVien;
    public static string? CurrentTenDangNhap => CurrentUser?.TenDangNhap;
}
```

### 1.2. Quy trình đăng nhập và lưu session

**File:** `GUI/Form_Login.cs` (dòng 102-120)

```csharp
// 1. Xác thực tài khoản
var taiKhoan = taiKhoanBus.Authenticate(tenDangNhap, matKhau);

// 2. Lấy thông tin nhân viên từ MaNhanVien trong tài khoản
var nhanVienBus = new NhanVien_BUS();
var nhanVien = nhanVienBus.GetNhanVienByID(taiKhoan.MaNhanVien);

// 3. LƯU SESSION - Đây là bước quan trọng!
SessionManager.SetCurrentUser(taiKhoan, nhanVien);
```

**Giải thích:**
- Khi user đăng nhập thành công, hệ thống lấy `TaiKhoanDTO` từ database
- Từ `taiKhoan.MaNhanVien`, hệ thống lấy thông tin `NhanVienDTO` tương ứng
- Cả hai được lưu vào `SessionManager` dưới dạng **static fields**
- Vì là static, có thể truy cập từ **bất kỳ form/class nào** trong ứng dụng

### 1.3. Nhận biết nhân viên ứng với tài khoản

**Cách hoạt động:**
1. **Bảng Tbl_TaiKhoan** có cột `MaNhanVien` → liên kết với `Tbl_NhanVien`
2. Khi đăng nhập, `SessionManager` lưu cả `TaiKhoanDTO` và `NhanVienDTO`
3. Để lấy tên nhân viên: `SessionManager.CurrentNhanVien.TenNhanVien`
4. Để lấy mã quyền: `SessionManager.CurrentMaQuyen` (từ `CurrentUser.MaQuyen`)

**Ví dụ sử dụng:**
```csharp
// Hiển thị tên nhân viên trên sidebar
if (SessionManager.CurrentNhanVien != null)
{
    userNameLabel.Text = SessionManager.CurrentNhanVien.TenNhanVien;
}

// Kiểm tra quyền
if (SessionManager.CurrentMaQuyen == 1) // Admin
{
    // Admin có toàn quyền
}
```

### 1.4. Xóa session khi đăng xuất

**File:** `GUI/SideBar/Form_Sidebar.cs` (dòng 723-737)

```csharp
private void logoutButton_Click(object sender, EventArgs e)
{
    // Xác nhận đăng xuất
    var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", ...);
    
    if (result == DialogResult.Yes)
    {
        // XÓA SESSION
        SessionManager.ClearSession();  // Set _currentUser và _currentNhanVien = null
        this.Close();
    }
}
```

---

## 2. HỆ THỐNG PHÂN QUYỀN ĐỘNG

### 2.1. PermissionService - Kiểm tra quyền

**File:** `Common/PermissionService.cs`

**Cấu trúc:**
- **Cache quyền:** `Dictionary<int, Dictionary<int, bool>>` 
  - Key 1: `MaChucNang` (mã chức năng)
  - Key 2: `MaLoaiQuyen` (mã loại quyền: 1=Xem, 2=Thêm, 3=Sửa, 4=Xóa)
  - Value: `true` nếu được phép, `false` nếu không

### 2.2. Quy trình load quyền

**Bước 1:** Lấy `MaQuyen` từ session
```csharp
int maQuyen = SessionManager.CurrentMaQuyen.Value;
```

**Bước 2:** Kiểm tra Admin (MaQuyen = 1)
```csharp
if (maQuyen == 1) // Admin
{
    _permissionCache = null; // Admin không cần cache, luôn có quyền
    return;
}
```

**Bước 3:** Load quyền từ database
```csharp
var chiTietQuyen = _phanQuyenBus.GetChiTietQuyen(maQuyen);
// Trả về danh sách PhanQuyenChiTietDTO với:
// - MaChucNang: Mã chức năng (Form_BanHang, Form_SanPham, ...)
// - MaLoaiQuyen: Loại quyền (1=Xem, 2=Thêm, 3=Sửa, 4=Xóa)
// - DuocPhep: true/false
```

**Bước 4:** Cache vào memory
```csharp
_permissionCache = new Dictionary<int, Dictionary<int, bool>>();

foreach (var quyen in chiTietQuyen)
{
    if (quyen.DuocPhep) // Chỉ lưu quyền được phép
    {
        if (!_permissionCache.ContainsKey(quyen.MaChucNang))
        {
            _permissionCache[quyen.MaChucNang] = new Dictionary<int, bool>();
        }
        _permissionCache[quyen.MaChucNang][quyen.MaLoaiQuyen] = true;
    }
}
```

### 2.3. Kiểm tra quyền

**Method:** `HasPermission(int maChucNang, int maLoaiQuyen)`

```csharp
public bool HasPermission(int maChucNang, int maLoaiQuyen)
{
    // 1. Kiểm tra đã đăng nhập chưa
    if (!SessionManager.IsLoggedIn || !SessionManager.CurrentMaQuyen.HasValue)
        return false;

    // 2. Admin luôn có quyền
    if (SessionManager.CurrentMaQuyen.Value == 1)
        return true;

    // 3. Load cache nếu chưa có
    if (_permissionCache == null)
        LoadPermissions();

    // 4. Kiểm tra trong cache
    if (_permissionCache == null || !_permissionCache.ContainsKey(maChucNang))
        return false;

    if (!_permissionCache[maChucNang].ContainsKey(maLoaiQuyen))
        return false;

    return _permissionCache[maChucNang][maLoaiQuyen];
}
```

**Method:** `HasPermissionByPath(string duongDan, int maLoaiQuyen)`
- Tìm `MaChucNang` từ `DuongDan` (ví dụ: "Form_BanHang")
- Sau đó gọi `HasPermission(maChucNang, maLoaiQuyen)`

---

## 3. CÁCH SIDEBAR ẨN/HIỆN CHỨC NĂNG

### 3.1. Mapping Button với DuongDan

**File:** `GUI/SideBar/Form_Sidebar.cs` (dòng 55-77)

```csharp
private Dictionary<Button, string> _buttonPathMapping = new();

private void InitializePermissions()
{
    _permissionService = new PermissionService();
    
    // Mapping: Button → DuongDan (tên trong database)
    _buttonPathMapping = new Dictionary<Button, string>
    {
        { navTrangChuButton, "Form_TrangChu" },
        { navBanHangButton, "Form_BanHang" },
        { navHoaDonButton, "Form_HoaDon" },
        { navPhieuNhapButton, "Form_PhieuNhap" },
        { navSanPhamButton, "Form_SanPham" },
        { navKhoHangButton, "Form_KhoHang" },
        { navLoaiSanPhamButton, "Form_LoaiSanPham" },
        { navKhuyenMaiButton, "Form_KhuyenMai" },
        { navKhachHangButton, "Form_KhachHang" },
        { navNhaCungCapButton, "Form_NhaCungCap" },
        { navNhanVienButton, "Form_NhanVien" },
        { navTaiKhoanButton, "Form_TaiKhoan" },
        { navQuanLyButton, "Form_QuanLy" }
    };
}
```

### 3.2. ApplyPermissions() - Ẩn/Hiện button

**File:** `GUI/SideBar/Form_Sidebar.cs` (dòng 79-195)

**Quy trình:**

**Bước 1:** Kiểm tra đăng nhập
```csharp
if (!SessionManager.IsLoggedIn || _permissionService == null)
{
    // Ẩn tất cả trừ Trang Chủ
    foreach (var button in _buttonPathMapping.Keys)
    {
        if (button != navTrangChuButton)
            button.Visible = false;
    }
    return;
}
```

**Bước 2:** Admin hiển thị tất cả
```csharp
if (SessionManager.CurrentMaQuyen == 1) // Admin
{
    foreach (var button in _buttonPathMapping.Keys)
    {
        button.Visible = true;
    }
    return;
}
```

**Bước 3:** Kiểm tra quyền cho từng button
```csharp
foreach (var kvp in _buttonPathMapping)
{
    var button = kvp.Key;
    var duongDan = kvp.Value; // "Form_BanHang", "Form_SanPham", ...
    
    // Trang chủ luôn hiển thị
    if (button == navTrangChuButton)
    {
        button.Visible = true;
        continue;
    }
    
    // Tìm chức năng trong database
    var chucNang = allChucNangs.FirstOrDefault(cn => 
        cn.DuongDan.Equals(duongDan, StringComparison.OrdinalIgnoreCase));
    
    if (chucNang != null)
    {
        // Kiểm tra quyền XEM (LoaiQuyen_Xem = 1)
        bool hasViewPermission = _permissionService.HasViewPermission(chucNang.MaChucNang);
        button.Visible = hasViewPermission; // Ẩn nếu không có quyền
    }
    else
    {
        button.Visible = false; // Không tìm thấy → ẩn
    }
}
```

### 3.3. Kiểm tra quyền khi click button

**File:** `GUI/SideBar/Form_Sidebar.cs` (dòng 271-277, 624-643)

```csharp
private void navBanHangButton_Click(object sender, EventArgs e)
{
    // Kiểm tra quyền TRƯỚC KHI mở form
    if (!CheckPermission("Form_BanHang", PermissionService.LoaiQuyen_Xem))
    {
        return; // Không có quyền → không mở form
    }
    ShowBanHang();
}

private bool CheckPermission(string duongDan, int maLoaiQuyen)
{
    if (_permissionService == null)
        return false;

    // Admin luôn có quyền
    if (SessionManager.CurrentMaQuyen == 1)
        return true;

    bool hasPermission = _permissionService.HasPermissionByPath(duongDan, maLoaiQuyen);
    if (!hasPermission)
    {
        MessageBox.Show("Bạn không có quyền truy cập chức năng này!", ...);
    }
    return hasPermission;
}
```

---

## 4. CÁCH ẨN CHỨC NĂNG CON TRONG FORM

### 4.1. Form_QuanLy - Ví dụ với TabControl

**File:** `GUI/QuanLy/Form_QuanLy.cs`

Hiện tại, `Form_QuanLy` có 3 tab:
- `tabPhanQuyen` (Phân quyền)
- `tabLoiNhuan` (% Lợi nhuận)
- `tabQuyDoiDiem` (Quy đổi điểm KH)

**Cách ẩn tab dựa trên quyền:**

```csharp
public Form_QuanLy()
{
    InitializeComponent();
    LoadTabs();
    ApplyTabPermissions(); // THÊM METHOD NÀY
}

private void ApplyTabPermissions()
{
    var permissionService = new PermissionService();
    
    // Ẩn tab "Phân quyền" nếu không có quyền
    // Giả sử có chức năng "Form_QuanLy_PhanQuyen" trong DB
    bool hasPhanQuyenPermission = permissionService.HasPermissionByPath(
        "Form_QuanLy_PhanQuyen", 
        PermissionService.LoaiQuyen_Xem
    );
    tabPhanQuyen.Visible = hasPhanQuyenPermission;
    
    // Tương tự cho các tab khác
    bool hasLoiNhuanPermission = permissionService.HasPermissionByPath(
        "Form_QuanLy_LoiNhuan", 
        PermissionService.LoaiQuyen_Xem
    );
    tabLoiNhuan.Visible = hasLoiNhuanPermission;
    
    bool hasQuyDoiDiemPermission = permissionService.HasPermissionByPath(
        "Form_QuanLy_QuyDoiDiem", 
        PermissionService.LoaiQuyen_Xem
    );
    tabQuyDoiDiem.Visible = hasQuyDoiDiemPermission;
}
```

### 4.2. Ẩn Button CRUD trong Form

**File:** `Common/FormPermissionHelper.cs`

**Sử dụng helper:**

```csharp
public partial class Form_NhanVien : Form
{
    private PermissionService _permissionService;
    
    public Form_NhanVien()
    {
        InitializeComponent();
        _permissionService = new PermissionService();
        ApplyPermissions(); // Áp dụng quyền khi form load
    }
    
    private void ApplyPermissions()
    {
        // Ẩn/Disable các button dựa trên quyền
        FormPermissionHelper.ApplyCRUDPermissions(
            _permissionService,
            "Form_NhanVien", // DuongDan trong database
            addButton: btnThem,      // Button Thêm
            editButton: btnSua,      // Button Sửa
            deleteButton: btnXoa,    // Button Xóa
            viewButton: btnXem        // Button Xem (nếu có)
        );
    }
}
```

**Cách hoạt động:**
- `ApplyCRUDPermissions()` kiểm tra quyền cho từng loại (Thêm, Sửa, Xóa, Xem)
- Nếu không có quyền → `button.Enabled = false` (hoặc `button.Visible = false`)

### 4.3. Kiểm tra quyền trước khi thực hiện thao tác

```csharp
private void btnThem_Click(object sender, EventArgs e)
{
    // Kiểm tra quyền TRƯỚC KHI thêm
    if (!FormPermissionHelper.CheckPermissionBeforeAction(
        _permissionService,
        "Form_NhanVien",
        PermissionService.LoaiQuyen_Them,
        "thêm nhân viên"
    ))
    {
        return; // Không có quyền → dừng lại
    }
    
    // Có quyền → thực hiện thêm
    // ... code thêm nhân viên
}
```

---

## 📊 SƠ ĐỒ LUỒNG HOẠT ĐỘNG

```
┌─────────────────┐
│  User Đăng Nhập │
└────────┬────────┘
         │
         ▼
┌─────────────────────────┐
│ Form_Login.Authenticate │
└────────┬────────────────┘
         │
         ▼
┌──────────────────────────────┐
│ SessionManager.SetCurrentUser │
│ - Lưu TaiKhoanDTO            │
│ - Lưu NhanVienDTO            │
└────────┬─────────────────────┘
         │
         ▼
┌─────────────────────────┐
│ Form_Sidebar được mở    │
└────────┬────────────────┘
         │
         ▼
┌──────────────────────────────┐
│ PermissionService.Reload      │
│ - Lấy MaQuyen từ Session      │
│ - Load quyền từ DB           │
│ - Cache vào memory            │
└────────┬─────────────────────┘
         │
         ▼
┌──────────────────────────────┐
│ Form_Sidebar.ApplyPermissions│
│ - Duyệt từng button           │
│ - Kiểm tra quyền Xem          │
│ - Ẩn button nếu không có Q   │
└────────┬─────────────────────┘
         │
         ▼
┌──────────────────────────────┐
│ User click button            │
│ - CheckPermission()           │
│ - Nếu có quyền → mở form     │
│ - Nếu không → hiện thông báo │
└──────────────────────────────┘
```

---

## 🔑 CÁC ĐIỂM QUAN TRỌNG

### 1. Admin (MaQuyen = 1)
- **Luôn có toàn quyền**, không cần kiểm tra database
- `PermissionService.HasPermission()` trả về `true` ngay lập tức

### 2. Cache Quyền
- Quyền được cache trong memory để tăng hiệu suất
- Khi phân quyền thay đổi, cần gọi `ReloadPermissions()`

### 3. DuongDan trong Database
- Phải khớp với mapping trong `Form_Sidebar._buttonPathMapping`
- Ví dụ: "Form_BanHang", "Form_SanPham", "Form_QuanLy"

### 4. Loại Quyền
- `LoaiQuyen_Xem = 1` (View) - Quyền xem/chạm vào form
- `LoaiQuyen_Them = 2` (Create) - Quyền thêm mới
- `LoaiQuyen_Sua = 3` (Update) - Quyền sửa
- `LoaiQuyen_Xoa = 4` (Delete) - Quyền xóa

### 5. Session là Static
- `SessionManager` dùng static fields → có thể truy cập từ bất kỳ đâu
- Chỉ có 1 session tại 1 thời điểm (single user)

---

## 💡 VÍ DỤ THỰC TẾ

### Ví dụ 1: User có quyền xem Bán hàng nhưng không có quyền sửa
```csharp
// Trong Form_BanHang
private void ApplyPermissions()
{
    var ps = new PermissionService();
    
    // Có quyền xem → form hiển thị
    bool canView = ps.HasPermissionByPath("Form_BanHang", PermissionService.LoaiQuyen_Xem);
    
    // Không có quyền sửa → button Sửa bị disable
    btnSua.Enabled = ps.HasPermissionByPath("Form_BanHang", PermissionService.LoaiQuyen_Sua);
}
```

### Ví dụ 2: Ẩn tab trong Form_QuanLy
```csharp
// Trong Form_QuanLy
private void ApplyTabPermissions()
{
    var ps = new PermissionService();
    
    // Chỉ hiển thị tab "Phân quyền" nếu có quyền
    tabPhanQuyen.Visible = ps.HasPermissionByPath(
        "Form_QuanLy_PhanQuyen", 
        PermissionService.LoaiQuyen_Xem
    );
}
```

---

## ❓ CÂU HỎI THƯỜNG GẶP

**Q: Làm sao để thêm chức năng mới vào hệ thống phân quyền?**
A: 
1. Thêm record vào bảng `Tbl_ChucNang` với `DuongDan` (ví dụ: "Form_NewFeature")
2. Thêm mapping trong `Form_Sidebar._buttonPathMapping`
3. Phân quyền cho các role trong `Tbl_PhanQuyenChiTiet`

**Q: Tại sao button vẫn hiển thị dù không có quyền?**
A: Kiểm tra:
- `ApplyPermissions()` đã được gọi chưa?
- `DuongDan` trong DB có khớp với mapping không?
- `MaQuyen` của user có đúng không?

**Q: Làm sao để reload quyền sau khi thay đổi phân quyền?**
A: Gọi `_permissionService.ReloadPermissions()` và `ApplyPermissions()` lại

---

**Tài liệu này giải thích toàn bộ hệ thống phân quyền động của bạn. Nếu có thắc mắc, hãy xem lại code trong các file đã được đề cập!**
