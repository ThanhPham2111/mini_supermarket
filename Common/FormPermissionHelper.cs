using System.Windows.Forms;
using mini_supermarket.Common;

namespace mini_supermarket.Common
{
    /// <summary>
    /// Helper class để áp dụng quyền cho các form
    /// </summary>
    public static class FormPermissionHelper
    {
        /// <summary>
        /// Áp dụng quyền cho các button CRUD trong form
        /// </summary>
        /// <param name="permissionService">PermissionService instance</param>
        /// <param name="functionPath">Đường dẫn chức năng (ví dụ: "Form_SanPham")</param>
        /// <param name="addButton">Button Thêm (có thể null)</param>
        /// <param name="editButton">Button Sửa (có thể null)</param>
        /// <param name="deleteButton">Button Xóa (có thể null)</param>
        /// <param name="viewButton">Button Xem (có thể null)</param>
        public static void ApplyCRUDPermissions(
            PermissionService permissionService,
            string functionPath,
            Button? addButton = null,
            Button? editButton = null,
            Button? deleteButton = null,
            Button? viewButton = null)
        {
            if (permissionService == null || string.IsNullOrWhiteSpace(functionPath))
            {
                return;
            }

            // Áp dụng quyền Thêm
            if (addButton != null)
            {
                bool canAdd = permissionService.HasPermissionByPath(functionPath, PermissionService.LoaiQuyen_Them);
                addButton.Enabled = canAdd;
            }

            // Áp dụng quyền Sửa
            if (editButton != null)
            {
                bool canEdit = permissionService.HasPermissionByPath(functionPath, PermissionService.LoaiQuyen_Sua);
                // Chỉ disable nếu không có quyền, nhưng vẫn có thể enable khi có selection
                if (!canEdit)
                {
                    editButton.Enabled = false;
                }
            }

            // Áp dụng quyền Xóa
            if (deleteButton != null)
            {
                bool canDelete = permissionService.HasPermissionByPath(functionPath, PermissionService.LoaiQuyen_Xoa);
                if (!canDelete)
                {
                    deleteButton.Enabled = false;
                }
            }

            // Áp dụng quyền Xem
            if (viewButton != null)
            {
                bool canView = permissionService.HasPermissionByPath(functionPath, PermissionService.LoaiQuyen_Xem);
                if (!canView)
                {
                    viewButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Kiểm tra quyền trước khi thực hiện thao tác
        /// </summary>
        /// <param name="permissionService">PermissionService instance</param>
        /// <param name="functionPath">Đường dẫn chức năng</param>
        /// <param name="permissionType">Loại quyền (LoaiQuyen_Xem, LoaiQuyen_Them, etc.)</param>
        /// <param name="actionName">Tên hành động để hiển thị trong thông báo</param>
        /// <returns>True nếu có quyền, false nếu không</returns>
        public static bool CheckPermissionBeforeAction(
            PermissionService permissionService,
            string functionPath,
            int permissionType,
            string actionName)
        {
            if (permissionService == null || string.IsNullOrWhiteSpace(functionPath))
            {
                return false;
            }

            bool hasPermission = permissionService.HasPermissionByPath(functionPath, permissionType);
            if (!hasPermission)
            {
                MessageBox.Show($"Bạn không có quyền {actionName}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return hasPermission;
        }
    }
}

