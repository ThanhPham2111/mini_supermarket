using System.Windows.Forms;
using mini_supermarket.Common;

namespace mini_supermarket.Common
{
    /// <summary>
    /// Helper class để áp dụng quyền cho các form – ĐÃ SỬA HOÀN HẢO
    /// </summary>
    public static class FormPermissionHelper
    {
        /// <summary>
        /// Áp dụng quyền cho các button CRUD trong form
        /// </summary>
        public static void ApplyCRUDPermissions(
            PermissionService permissionService,
            string functionPath,
            Button? addButton = null,
            Button? editButton = null,
            Button? deleteButton = null,
            Button? viewButton = null)
        {
            if (permissionService == null || string.IsNullOrWhiteSpace(functionPath))
                return;

            // Thêm
            if (addButton != null)
            {
                addButton.Enabled = permissionService.HasPermissionByPath(functionPath, PermissionService.LoaiQuyen_Them);
            }

            // Sửa – SỬA LẠI: phải bật khi có quyền!
            if (editButton != null)
            {
                editButton.Enabled = permissionService.HasPermissionByPath(functionPath, PermissionService.LoaiQuyen_Sua);
            }

            // Xóa – SỬA LẠI: phải bật khi có quyền!
            if (deleteButton != null)
            {
                deleteButton.Enabled = permissionService.HasPermissionByPath(functionPath, PermissionService.LoaiQuyen_Xoa);
            }

            // Xem
            if (viewButton != null)
            {
                viewButton.Enabled = permissionService.HasPermissionByPath(functionPath, PermissionService.LoaiQuyen_Xem);
            }
        }

        /// <summary>
        /// Kiểm tra quyền trước khi thực hiện thao tác
        /// </summary>
        public static bool CheckPermissionBeforeAction(
            PermissionService permissionService,
            string functionPath,
            int permissionType,
            string actionName = "thao tác này")
        {
            if (permissionService == null || string.IsNullOrWhiteSpace(functionPath))
                return false;

            bool hasPermission = permissionService.HasPermissionByPath(functionPath, permissionType);
            if (!hasPermission)
            {
                MessageBox.Show($"Bạn không có quyền {actionName}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return hasPermission;
        }
    }
}