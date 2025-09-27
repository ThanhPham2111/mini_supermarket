using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class NhanVien_DAO
    {
        public IList<NhanVienDTO> GetNhanVien(string? trangThaiFilter = null)
        {
            var nhanVienList = new List<NhanVienDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaNhanVien, TenNhanVien, GioiTinh, NgaySinh, SoDienThoai, VaiTro, TrangThai
                                     FROM dbo.Tbl_NhanVien";

            if (!string.IsNullOrWhiteSpace(trangThaiFilter))
            {
                command.CommandText += " WHERE TrangThai = @TrangThai";
                command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
                {
                    Value = trangThaiFilter
                });
            }

            command.CommandText += " ORDER BY MaNhanVien";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                nhanVienList.Add(new NhanVienDTO
                {
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    TenNhanVien = reader.GetString(reader.GetOrdinal("TenNhanVien")),
                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? null : reader.GetString(reader.GetOrdinal("GioiTinh")),
                    NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? null : reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    VaiTro = reader.IsDBNull(reader.GetOrdinal("VaiTro")) ? null : reader.GetString(reader.GetOrdinal("VaiTro")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                });
            }

            return nhanVienList;
        }
        public int GetMaxMaNhanVien()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ISNULL(MAX(MaNhanVien), 0) FROM dbo.Tbl_NhanVien";

            connection.Open();
            object? result = command.ExecuteScalar();
            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }


        public int InsertNhanVien(NhanVienDTO nhanVien)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_NhanVien (TenNhanVien, GioiTinh, NgaySinh, SoDienThoai, VaiTro, TrangThai)
                                     VALUES (@TenNhanVien, @GioiTinh, @NgaySinh, @SoDienThoai, @VaiTro, @TrangThai);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

            AddNhanVienParameters(command, nhanVien, includeKey: false);

            connection.Open();
            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Khong the tao nhan vien moi.");
            }

            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void UpdateNhanVien(NhanVienDTO nhanVien)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_NhanVien
                                     SET TenNhanVien = @TenNhanVien,
                                         GioiTinh = @GioiTinh,
                                         NgaySinh = @NgaySinh,
                                         SoDienThoai = @SoDienThoai,
                                         VaiTro = @VaiTro,
                                         TrangThai = @TrangThai
                                     WHERE MaNhanVien = @MaNhanVien";

            AddNhanVienParameters(command, nhanVien, includeKey: true);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Khong tim thay nhan vien de cap nhat.");
            }
        }

        public void DeleteNhanVien(int maNhanVien)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM dbo.Tbl_NhanVien WHERE MaNhanVien = @MaNhanVien";
            command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.Int)
            {
                Value = maNhanVien
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Khong tim thay nhan vien de xoa.");
            }
        }

        private static void AddNhanVienParameters(SqlCommand command, NhanVienDTO nhanVien, bool includeKey)
        {
            command.Parameters.Clear();

            if (includeKey)
            {
                command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.Int)
                {
                    Value = nhanVien.MaNhanVien
                });
            }

            command.Parameters.Add(new SqlParameter("@TenNhanVien", SqlDbType.NVarChar, 200)
            {
                Value = nhanVien.TenNhanVien
            });

            command.Parameters.Add(new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10)
            {
                Value = nhanVien.GioiTinh ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@NgaySinh", SqlDbType.Date)
            {
                Value = nhanVien.NgaySinh?.Date ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 20)
            {
                Value = nhanVien.SoDienThoai ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@VaiTro", SqlDbType.NVarChar, 100)
            {
                Value = nhanVien.VaiTro ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
            {
                Value = nhanVien.TrangThai ?? (object)DBNull.Value
            });
        }
    }
}
