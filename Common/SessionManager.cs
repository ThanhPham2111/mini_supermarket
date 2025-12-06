using mini_supermarket.DTO;

namespace mini_supermarket.Common
{
    public static class SessionManager
    {
        private static TaiKhoanDTO? _currentUser;
        private static NhanVienDTO? _currentNhanVien;

        public static TaiKhoanDTO? CurrentUser
        {
            get => _currentUser;
            private set => _currentUser = value;
        }

        public static NhanVienDTO? CurrentNhanVien
        {
            get => _currentNhanVien;
            private set => _currentNhanVien = value;
        }

        public static int? CurrentMaQuyen => CurrentUser?.MaQuyen;
        public static int? CurrentMaNhanVien => CurrentUser?.MaNhanVien;
        public static string? CurrentTenDangNhap => CurrentUser?.TenDangNhap;

        public static void SetCurrentUser(TaiKhoanDTO taiKhoan, NhanVienDTO? nhanVien = null)
        {
            CurrentUser = taiKhoan;
            CurrentNhanVien = nhanVien;
        }

        public static void ClearSession()
        {
            CurrentUser = null;
            CurrentNhanVien = null;
        }

        public static bool IsLoggedIn => CurrentUser != null;
    }
}

