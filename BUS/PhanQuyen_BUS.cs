using System;
using System.Collections.Generic;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.BUS
{
    public class PhanQuyen_BUS
    {
        private readonly PhanQuyen_DAO _dao = new PhanQuyen_DAO();

        // Lấy danh sách tất cả các Role (Nhóm quyền)
        public IList<PhanQuyenDTO> GetAllRoles()
        {
            return _dao.GetAllRoles();
        }

        // Lấy danh sách tất cả Chức năng
        public IList<ChucNangDTO> GetAllChucNang()
        {
            return _dao.GetAllChucNang();
        }

        // Lấy danh sách tất cả Loại quyền (View, Add, Edit, Delete...)
        public IList<LoaiQuyenDTO> GetAllLoaiQuyen()
        {
            return _dao.GetAllLoaiQuyen();
        }

        // Lấy chi tiết phân quyền cho 1 Role cụ thể
        public IList<PhanQuyenChiTietDTO> GetChiTietQuyen(int maQuyen)
        {
            if (maQuyen <= 0)
                throw new ArgumentException("Mã quyền không hợp lệ.");

            return _dao.GetChiTietQuyen(maQuyen);
        }

        // Lưu (Cập nhật hoặc Thêm mới) quyền
        public void SavePhanQuyen(int maQuyen, int maChucNang, int maLoaiQuyen, bool duocPhep)
        {
            if (maQuyen <= 0)
                throw new ArgumentException("Mã quyền không hợp lệ.");
            if (maChucNang <= 0)
                throw new ArgumentException("Mã chức năng không hợp lệ.");
            if (maLoaiQuyen <= 0)
                throw new ArgumentException("Mã loại quyền không hợp lệ.");

            _dao.SavePhanQuyen(maQuyen, maChucNang, maLoaiQuyen, duocPhep);
        }

        // Thêm Role mới
        public bool AddRole(string tenQuyen, string moTa)
        {
            if (string.IsNullOrWhiteSpace(tenQuyen))
                throw new ArgumentException("Tên quyền không được để trống.");

            return _dao.AddRole(tenQuyen, moTa);
        }

        // Xóa Role
        public bool DeleteRole(int maQuyen)
        {
            if (maQuyen <= 0)
                throw new ArgumentException("Mã quyền không hợp lệ.");

            return _dao.DeleteRole(maQuyen);
        }
    }
}

