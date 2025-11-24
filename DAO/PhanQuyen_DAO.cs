using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class PhanQuyen_DAO
    {
        // Lấy danh sách tất cả các Role (Nhóm quyền)
        public IList<PhanQuyenDTO> GetAllRoles()
        {
            var roles = new List<PhanQuyenDTO>();
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT MaQuyen, TenQuyen, MoTa FROM Tbl_PhanQuyen";
            
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new PhanQuyenDTO
                {
                    MaQuyen = reader.GetInt32(0),
                    TenQuyen = reader.GetString(1),
                    MoTa = reader.IsDBNull(2) ? null : reader.GetString(2)
                });
            }
            return roles;
        }

        // Lấy danh sách tất cả Chức năng
        public IList<ChucNangDTO> GetAllChucNang()
        {
            var list = new List<ChucNangDTO>();
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT MaChucNang, TenChucNang, MaCha, DuongDan, MoTa FROM Tbl_ChucNang ORDER BY MaChucNang";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ChucNangDTO
                {
                    MaChucNang = reader.GetInt32(0),
                    TenChucNang = reader.GetString(1),
                    MaCha = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                    DuongDan = reader.IsDBNull(3) ? null : reader.GetString(3),
                    MoTa = reader.IsDBNull(4) ? null : reader.GetString(4)
                });
            }
            return list;
        }

        // Lấy danh sách tất cả Loại quyền (View, Add, Edit, Delete...)
        public IList<LoaiQuyenDTO> GetAllLoaiQuyen()
        {
            var list = new List<LoaiQuyenDTO>();
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT MaLoaiQuyen, TenQuyen, MoTa FROM Tbl_LoaiQuyen ORDER BY MaLoaiQuyen";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new LoaiQuyenDTO
                {
                    MaLoaiQuyen = reader.GetInt32(0),
                    TenQuyen = reader.GetString(1),
                    MoTa = reader.IsDBNull(2) ? null : reader.GetString(2)
                });
            }
            return list;
        }

        // Lấy chi tiết phân quyền cho 1 Role cụ thể
        public IList<PhanQuyenChiTietDTO> GetChiTietQuyen(int maQuyen)
        {
            var list = new List<PhanQuyenChiTietDTO>();
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaPhanQuyenChiTiet, MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep 
                                    FROM Tbl_PhanQuyenChiTiet 
                                    WHERE MaQuyen = @MaQuyen";
            command.Parameters.AddWithValue("@MaQuyen", maQuyen);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new PhanQuyenChiTietDTO
                {
                    MaPhanQuyenChiTiet = reader.GetInt32(0),
                    MaQuyen = reader.GetInt32(1),
                    MaChucNang = reader.GetInt32(2),
                    MaLoaiQuyen = reader.GetInt32(3),
                    DuocPhep = reader.GetBoolean(4)
                });
            }
            return list;
        }

        // Lưu (Cập nhật hoặc Thêm mới) quyền
        public void SavePhanQuyen(int maQuyen, int maChucNang, int maLoaiQuyen, bool duocPhep)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            
            // Kiểm tra xem đã có record chưa
            command.CommandText = @"SELECT COUNT(1) FROM Tbl_PhanQuyenChiTiet 
                                    WHERE MaQuyen = @MaQuyen AND MaChucNang = @MaChucNang AND MaLoaiQuyen = @MaLoaiQuyen";
            
            command.Parameters.AddWithValue("@MaQuyen", maQuyen);
            command.Parameters.AddWithValue("@MaChucNang", maChucNang);
            command.Parameters.AddWithValue("@MaLoaiQuyen", maLoaiQuyen);
            
            connection.Open();
            int count = (int)command.ExecuteScalar();

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@MaQuyen", maQuyen);
            command.Parameters.AddWithValue("@MaChucNang", maChucNang);
            command.Parameters.AddWithValue("@MaLoaiQuyen", maLoaiQuyen);
            command.Parameters.AddWithValue("@DuocPhep", duocPhep);

            if (count > 0)
            {
                // Update
                command.CommandText = @"UPDATE Tbl_PhanQuyenChiTiet 
                                        SET DuocPhep = @DuocPhep 
                                        WHERE MaQuyen = @MaQuyen AND MaChucNang = @MaChucNang AND MaLoaiQuyen = @MaLoaiQuyen";
            }
            else
            {
                // Insert
                command.CommandText = @"INSERT INTO Tbl_PhanQuyenChiTiet (MaQuyen, MaChucNang, MaLoaiQuyen, DuocPhep) 
                                        VALUES (@MaQuyen, @MaChucNang, @MaLoaiQuyen, @DuocPhep)";
            }

            command.ExecuteNonQuery();
        }

        // Thêm Role mới
        public bool AddRole(string tenQuyen, string moTa)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Tbl_PhanQuyen (TenQuyen, MoTa) VALUES (@TenQuyen, @MoTa)";
            command.Parameters.AddWithValue("@TenQuyen", tenQuyen);
            command.Parameters.AddWithValue("@MoTa", moTa ?? (object)DBNull.Value);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        // Xóa Role
        public bool DeleteRole(int maQuyen)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            
            try
            {
                // Xóa chi tiết trước
                using var cmd1 = connection.CreateCommand();
                cmd1.Transaction = transaction;
                cmd1.CommandText = "DELETE FROM Tbl_PhanQuyenChiTiet WHERE MaQuyen = @MaQuyen";
                cmd1.Parameters.AddWithValue("@MaQuyen", maQuyen);
                cmd1.ExecuteNonQuery();

                // Xóa Role
                using var cmd2 = connection.CreateCommand();
                cmd2.Transaction = transaction;
                cmd2.CommandText = "DELETE FROM Tbl_PhanQuyen WHERE MaQuyen = @MaQuyen";
                cmd2.Parameters.AddWithValue("@MaQuyen", maQuyen);
                cmd2.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
