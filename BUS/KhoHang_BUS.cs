using System;
using System.Collections.Generic;
using mini_supermarket.DTO;
using mini_supermarket.DAO;

namespace mini_supermarket.BUS
{
    public class KhoHang_BUS
    {
        private readonly KhoHang_DAO _dao = new();

        // --- Lấy danh sách kho hàng ---
        public IList<KhoHangDTO> GetKhoHang(string? trangThaiFilter = null)
        {
            return _dao.GetKhoHang(trangThaiFilter);
        }

        // --- Thêm kho hàng ---
        public void AddKhoHang(KhoHangDTO kho)
        {
            if (kho == null) throw new ArgumentNullException(nameof(kho));
            if (kho.MaSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ.");

            // Tự xác định trạng thái dựa trên số lượng
            kho.TrangThai = GetStatusByQuantity(kho.SoLuong ?? 0);

            // Kiểm tra xem sản phẩm đã tồn tại trong kho chưa
            var existing = _dao.GetKhoHang();
            var existingItem = existing.FirstOrDefault(x => x.MaSanPham == kho.MaSanPham);
            if (existingItem != null)
            {
                // Nếu đã tồn tại, cập nhật số lượng
                kho.SoLuong = (existingItem.SoLuong ?? 0) + (kho.SoLuong ?? 0);
                _dao.UpdateKhoHang(kho);
            }
            else
            {
                // Nếu chưa tồn tại, thêm mới
                _dao.InsertKhoHang(kho);
            }
        }


        // --- Cập nhật kho hàng ---
        public void UpdateKhoHang(KhoHangDTO kho)
        {
            if (kho == null) throw new ArgumentNullException(nameof(kho));
            if (kho.MaSanPham <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ.");

            // Cập nhật trạng thái dựa trên số lượng
            kho.TrangThai = GetStatusByQuantity(kho.SoLuong ?? 0);

            _dao.UpdateKhoHang(kho);
        }

        // --- Xóa kho hàng ---
        public void DeleteKhoHang(int maSanPham)
        {
            if (maSanPham <= 0) throw new ArgumentException("Mã sản phẩm không hợp lệ.");
            _dao.DeleteKhoHang(maSanPham);
        }

        // --- Lấy trạng thái sẵn có ---
        public IList<string> GetAvailableStatuses()
        {
            return _dao.GetDistinctTrangThai();
        }

        // --- Lấy danh sách tất cả sản phẩm (để chọn khi thêm/sửa) ---
        public IList<SanPhamDTO> GetAllProducts()
        {
            return _dao.GetAllProducts();
        }

        // --- Xác định trạng thái tự động dựa trên số lượng ---
        public string GetStatusByQuantity(int soLuong)
        {
            if (soLuong <= 0) return "Hết hàng";
            if (soLuong < 10) return "Sắp hết";
            return "Còn hàng";
        }

        // --- Tìm kiếm kho hàng theo tên hoặc mã sản phẩm ---
        public IList<KhoHangDTO> SearchKhoHang(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetKhoHang();

            keyword = keyword.Trim().ToLower();
            var all = GetKhoHang();
            var result = new List<KhoHangDTO>();

            foreach (var item in all)
            {
                var spName = item.TenSanPham?.ToLower() ?? "";
                var spId = item.MaSanPham.ToString();
                if (spName.Contains(keyword) || spId.Contains(keyword))
                    result.Add(item);
            }

            return result;
        }
    }
}