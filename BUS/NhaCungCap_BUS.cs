using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class NhaCungCap_BUS
    {
        private readonly NhaCungCap_DAO _dao = new();

        public const string StatusActive = "Hoạt động";
        public const string StatusInactive = "Khóa";

        private readonly List<string> _dsTrangThai = new()
        {
            StatusActive,
            StatusInactive
        };

        public List<string> GetDefaultStatuses()
        {
            return _dsTrangThai;
        }

        public List<NhaCungCapDTO> GetAll()
        {
            return _dao.GetNhaCungCap();
        }

        public List<NhaCungCapDTO> GetNhaCungCap(string? trangThai = null)
        {
            return _dao.GetNhaCungCap(trangThai);
        }

        public int GetNextMaNhaCungCap()
        {
            return _dao.GetMaxMaNhaCungCap() + 1;
        }

        public NhaCungCapDTO AddNhaCungCap(NhaCungCapDTO item)
        {
            Validate(item, false);
            item.MaNhaCungCap = _dao.InsertNhaCungCap(item);
            return item;
        }

        public void UpdateNhaCungCap(NhaCungCapDTO item)
        {
            Validate(item, true);
            _dao.UpdateNhaCungCap(item);
        }

        public void DeleteNhaCungCap(int ma)
        {
            _dao.DeleteNhaCungCap(ma);
        }

        // =============================
        // ✅ VALIDATE
        // =============================
        private void Validate(NhaCungCapDTO item, bool updating)
        {
            if (item == null)
                throw new Exception("Dữ liệu không hợp lệ");

            if (updating && item.MaNhaCungCap <= 0)
                throw new Exception("Mã không hợp lệ");

            if (string.IsNullOrWhiteSpace(item.TenNhaCungCap))
                throw new Exception("Tên không được để trống");

            if (string.IsNullOrWhiteSpace(item.DiaChi))
                throw new Exception("Địa chỉ không được để trống");

            if (string.IsNullOrWhiteSpace(item.SoDienThoai))
                throw new Exception("Số điện thoại không được để trống");

            if (item.SoDienThoai.Length != 10 || !item.SoDienThoai.All(char.IsDigit))
                throw new Exception("Số điện thoại không hợp lệ");

            if (string.IsNullOrWhiteSpace(item.Email))
            throw new Exception("Email không được để trống");


            // ✅ Trang thái
            if (string.IsNullOrWhiteSpace(item.TrangThai))
                item.TrangThai = StatusActive;

            if (!_dsTrangThai.Contains(item.TrangThai))
                throw new Exception("Trạng thái không hợp lệ");
        }

       
       
    }
}
