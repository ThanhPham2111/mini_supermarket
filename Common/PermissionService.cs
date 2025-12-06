using System.Collections.Generic;
using System.Linq;
using mini_supermarket.BUS;
using mini_supermarket.Common;
using mini_supermarket.DTO;

namespace mini_supermarket.Common
{
    public class PermissionService
    {
        private readonly PhanQuyen_BUS _phanQuyenBus = new();
        private Dictionary<int, Dictionary<int, bool>>? _permissionCache; // MaChucNang -> MaLoaiQuyen -> bool

        // MaLoaiQuyen constants - cần map với database
        public const int LoaiQuyen_Xem = 1; // View
        public const int LoaiQuyen_Them = 2; // Create/Add
        public const int LoaiQuyen_Sua = 3; // Update/Edit
        public const int LoaiQuyen_Xoa = 4; // Delete

        public PermissionService()
        {
            // Không load ngay trong constructor vì session có thể chưa được set
            // Sẽ load khi cần thiết hoặc khi gọi ReloadPermissions()
        }

        private void LoadPermissions()
        {
            if (!SessionManager.IsLoggedIn || !SessionManager.CurrentMaQuyen.HasValue)
            {
                _permissionCache = null;
                return;
            }

            int maQuyen = SessionManager.CurrentMaQuyen.Value;
            
            try
            {
                var chiTietQuyen = _phanQuyenBus.GetChiTietQuyen(maQuyen);

                // Cache structure: MaChucNang -> MaLoaiQuyen -> bool
                _permissionCache = new Dictionary<int, Dictionary<int, bool>>();

                foreach (var quyen in chiTietQuyen)
                {
                    // Chỉ lưu các quyền được phép (DuocPhep = true)
                    // Nếu DuocPhep = false, không cần lưu vào cache (mặc định là false)
                    if (quyen.DuocPhep)
                    {
                        if (!_permissionCache.ContainsKey(quyen.MaChucNang))
                        {
                            _permissionCache[quyen.MaChucNang] = new Dictionary<int, bool>();
                        }

                        _permissionCache[quyen.MaChucNang][quyen.MaLoaiQuyen] = true;
                    }
                }
            }
            catch (Exception)
            {
                // Nếu có lỗi, set cache về null
                _permissionCache = null;
                throw;
            }
        }

        /// <summary>
        /// Kiểm tra quyền xem (View) cho một chức năng
        /// </summary>
        public bool HasViewPermission(int maChucNang)
        {
            return HasPermission(maChucNang, LoaiQuyen_Xem);
        }

        /// <summary>
        /// Kiểm tra quyền thêm (Create) cho một chức năng
        /// </summary>
        public bool HasCreatePermission(int maChucNang)
        {
            return HasPermission(maChucNang, LoaiQuyen_Them);
        }

        /// <summary>
        /// Kiểm tra quyền sửa (Update) cho một chức năng
        /// </summary>
        public bool HasUpdatePermission(int maChucNang)
        {
            return HasPermission(maChucNang, LoaiQuyen_Sua);
        }

        /// <summary>
        /// Kiểm tra quyền xóa (Delete) cho một chức năng
        /// </summary>
        public bool HasDeletePermission(int maChucNang)
        {
            return HasPermission(maChucNang, LoaiQuyen_Xoa);
        }

        /// <summary>
        /// Kiểm tra quyền cụ thể cho một chức năng
        /// </summary>
        public bool HasPermission(int maChucNang, int maLoaiQuyen)
        {
            if (!SessionManager.IsLoggedIn || !SessionManager.CurrentMaQuyen.HasValue)
            {
                return false;
            }

            // Admin luôn có quyền
            if (SessionManager.CurrentMaQuyen.Value == 1) // Admin
            {
                return true;
            }

            // Đảm bảo cache đã được load
            if (_permissionCache == null)
            {
                LoadPermissions();
            }

            // Nếu vẫn null sau khi load, có thể do không có quyền nào
            if (_permissionCache == null || !_permissionCache.ContainsKey(maChucNang))
            {
                return false;
            }

            if (!_permissionCache[maChucNang].ContainsKey(maLoaiQuyen))
            {
                return false;
            }

            return _permissionCache[maChucNang][maLoaiQuyen];
        }

        /// <summary>
        /// Kiểm tra quyền dựa trên tên chức năng (DuongDan)
        /// </summary>
        public bool HasPermissionByPath(string duongDan, int maLoaiQuyen)
        {
            if (string.IsNullOrWhiteSpace(duongDan))
            {
                return false;
            }

            // Admin luôn có quyền tất cả
            if (SessionManager.IsLoggedIn && SessionManager.CurrentMaQuyen.HasValue && SessionManager.CurrentMaQuyen.Value == 1)
            {
                return true;
            }

            var chucNangs = _phanQuyenBus.GetAllChucNang();
            var chucNang = chucNangs.FirstOrDefault(cn => 
                cn.DuongDan != null && cn.DuongDan.Equals(duongDan, System.StringComparison.OrdinalIgnoreCase));

            if (chucNang == null)
            {
                return false;
            }

            return HasPermission(chucNang.MaChucNang, maLoaiQuyen);
        }

        /// <summary>
        /// Lấy danh sách chức năng mà user có quyền xem
        /// </summary>
        public List<int> GetAccessibleChucNangs()
        {
            if (!SessionManager.IsLoggedIn || !SessionManager.CurrentMaQuyen.HasValue)
            {
                return new List<int>();
            }

            // Admin có quyền tất cả
            if (SessionManager.CurrentMaQuyen.Value == 1)
            {
                var allChucNangs = _phanQuyenBus.GetAllChucNang();
                return allChucNangs.Select(cn => cn.MaChucNang).ToList();
            }

            if (_permissionCache == null)
            {
                LoadPermissions();
            }

            if (_permissionCache == null)
            {
                return new List<int>();
            }

            var accessibleChucNangs = new List<int>();
            foreach (var chucNangId in _permissionCache.Keys)
            {
                if (HasViewPermission(chucNangId))
                {
                    accessibleChucNangs.Add(chucNangId);
                }
            }

            return accessibleChucNangs;
        }

        /// <summary>
        /// Reload permissions (khi có thay đổi phân quyền)
        /// </summary>
        public void ReloadPermissions()
        {
            LoadPermissions();
        }
    }
}

