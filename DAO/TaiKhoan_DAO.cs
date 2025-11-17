using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class TaiKhoan_DAO
    {
        public IList<TaiKhoanDTO> GetTaiKhoan(string? trangThaiFilter = null)
        {
            var taiKhoanList = new List<TaiKhoanDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT tk.MaTaiKhoan, tk.TenDangNhap, tk.MatKhau, tk.MaNhanVien, 
                                           tk.MaQuyen, tk.TrangThai,
                                           nv.TenNhanVien, pq.TenQuyen
                                    FROM dbo.Tbl_TaiKhoan tk
                                    INNER JOIN dbo.Tbl_NhanVien nv ON tk.MaNhanVien = nv.MaNhanVien
                                    INNER JOIN dbo.Tbl_PhanQuyen pq ON tk.MaQuyen = pq.MaQuyen";

            if (!string.IsNullOrWhiteSpace(trangThaiFilter))
            {
                command.CommandText += " WHERE tk.TrangThai = @TrangThai";
                command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
                {
                    Value = trangThaiFilter
                });
            }

            command.CommandText += " ORDER BY tk.MaTaiKhoan";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                taiKhoanList.Add(new TaiKhoanDTO
                {
                    MaTaiKhoan = reader.GetInt32(reader.GetOrdinal("MaTaiKhoan")),
                    TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap")),
                    MatKhau = reader.GetString(reader.GetOrdinal("MatKhau")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaQuyen = reader.GetInt32(reader.GetOrdinal("MaQuyen")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                });
            }

            return taiKhoanList;
        }

        public int GetMaxMaTaiKhoan()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ISNULL(MAX(MaTaiKhoan), 0) FROM dbo.Tbl_TaiKhoan";

            connection.Open();
            object? result = command.ExecuteScalar();
            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public bool IsTenDangNhapExists(string tenDangNhap, int? excludeMaTaiKhoan = null)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM dbo.Tbl_TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            
            if (excludeMaTaiKhoan.HasValue)
            {
                command.CommandText += " AND MaTaiKhoan != @MaTaiKhoan";
                command.Parameters.Add(new SqlParameter("@MaTaiKhoan", SqlDbType.Int)
                {
                    Value = excludeMaTaiKhoan.Value
                });
            }

            command.Parameters.Add(new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 255)
            {
                Value = tenDangNhap
            });

            connection.Open();
            object? result = command.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
        }

        public int InsertTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_TaiKhoan (TenDangNhap, MatKhau, MaNhanVien, MaQuyen, TrangThai)
                                     VALUES (@TenDangNhap, @MatKhau, @MaNhanVien, @MaQuyen, @TrangThai);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

            AddTaiKhoanParameters(command, taiKhoan, includeKey: false);

            connection.Open();
            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Không thể tạo tài khoản mới.");
            }

            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void UpdateTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_TaiKhoan
                                     SET TenDangNhap = @TenDangNhap,
                                         MatKhau = @MatKhau,
                                         MaNhanVien = @MaNhanVien,
                                         MaQuyen = @MaQuyen,
                                         TrangThai = @TrangThai
                                     WHERE MaTaiKhoan = @MaTaiKhoan";

            AddTaiKhoanParameters(command, taiKhoan, includeKey: true);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy tài khoản để cập nhật.");
            }
        }

        public void DeleteTaiKhoan(int maTaiKhoan)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM dbo.Tbl_TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan";
            command.Parameters.Add(new SqlParameter("@MaTaiKhoan", SqlDbType.Int)
            {
                Value = maTaiKhoan
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy tài khoản để xóa.");
            }
        }

        public IList<PhanQuyenDTO> GetAllPhanQuyen()
        {
            var result = new List<PhanQuyenDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT MaQuyen, TenQuyen, MoTa FROM dbo.Tbl_PhanQuyen ORDER BY MaQuyen";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new PhanQuyenDTO
                {
                    MaQuyen = reader.GetInt32(reader.GetOrdinal("MaQuyen")),
                    TenQuyen = reader.GetString(reader.GetOrdinal("TenQuyen")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString(reader.GetOrdinal("MoTa"))
                });
            }

            return result;
        }

        private static void AddTaiKhoanParameters(SqlCommand command, TaiKhoanDTO taiKhoan, bool includeKey)
        {
            command.Parameters.Clear();

            if (includeKey)
            {
                command.Parameters.Add(new SqlParameter("@MaTaiKhoan", SqlDbType.Int)
                {
                    Value = taiKhoan.MaTaiKhoan
                });
            }

            command.Parameters.Add(new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 255)
            {
                Value = taiKhoan.TenDangNhap
            });

            command.Parameters.Add(new SqlParameter("@MatKhau", SqlDbType.VarChar, 255)
            {
                Value = taiKhoan.MatKhau
            });

            command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.Int)
            {
                Value = taiKhoan.MaNhanVien
            });

            command.Parameters.Add(new SqlParameter("@MaQuyen", SqlDbType.Int)
            {
                Value = taiKhoan.MaQuyen
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
            {
                Value = taiKhoan.TrangThai ?? (object)DBNull.Value
            });
        }
    }
}

