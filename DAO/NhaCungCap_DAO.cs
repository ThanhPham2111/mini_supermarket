using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class NhaCungCap_DAO
    {
        public IList<NhaCungCapDTO> GetNhaCungCap(string? trangThaiFilter = null)
        {
            var nhaCungCapList = new List<NhaCungCapDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaNhaCungCap, TenNhaCungCap, DiaChi, SoDienThoai, Email, TrangThai
                                     FROM dbo.Tbl_NhaCungCap";

            if (!string.IsNullOrWhiteSpace(trangThaiFilter))
            {
                command.CommandText += " WHERE TrangThai = @TrangThai";
                command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
                {
                    Value = trangThaiFilter
                });
            }

            command.CommandText += " ORDER BY MaNhaCungCap";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                nhaCungCapList.Add(new NhaCungCapDTO
                {
                    MaNhaCungCap = reader.GetInt32(reader.GetOrdinal("MaNhaCungCap")),
                    TenNhaCungCap = reader.GetString(reader.GetOrdinal("TenNhaCungCap")),
                    // GioiTinh = readeSoDienThoair.IsDBNull(reader.GetOrdinal("GioiTinh")) ? null : reader.GetString(reader.GetOrdinal("GioiTinh")),
                    // NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? null : reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                      DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                  
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    // VaiTro = reader.IsDBNull(reader.GetOrdinal("VaiTro")) ? null : reader.GetString(reader.GetOrdinal("VaiTro")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                });
            }

            return nhaCungCapList;
        }
        public int GetMaxMaNhaCungCap()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ISNULL(MAX(MaNhaCungCap), 0) FROM dbo.Tbl_NhaCungCap";

            connection.Open();
            object? result = command.ExecuteScalar();
            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }


        public int InsertNhaCungCap(NhaCungCapDTO nhaCungCap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_NhaCungCap (TenNhaCungCap,DiaChi,SoDienThoai, Email, TrangThai)
                                     VALUES (@TenNhaCungCap, @DiaChi, @SoDienThoai, @Email, @TrangThai);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

            AddNhaCungCapParameters(command, nhaCungCap, includeKey: false);

            connection.Open();
            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Khong the tao khach hang moi.");
            }

            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void UpdateNhaCungCap(NhaCungCapDTO nhaCungCap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_NhaCungCap
                                     SET TenNhaCungCap = @TenNhaCungCap, 
                                         DiaChi = @DiaChi,
                                         SoDienThoai = @SoDienThoai,
                                         Email = @Email,
                                         TrangThai = @TrangThai
                                     WHERE MaNhaCungCap = @MaNhaCungCap";

            AddNhaCungCapParameters(command, nhaCungCap, includeKey: true);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Khong tim thay khach hang de cap nhat.");
            }
        }

        public void DeleteNhaCungCap(int maNhaCungCap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM dbo.Tbl_NhaCungCap WHERE MaNhaCungCap = @MaNhaCungCap";
            command.Parameters.Add(new SqlParameter("@MaNhaCungCap", SqlDbType.Int)
            {
                Value = maNhaCungCap
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Khong tim thay khach hang de xoa.");
            }
        }

        private static void AddNhaCungCapParameters(SqlCommand command, NhaCungCapDTO nhaCungCap, bool includeKey)
        {
            command.Parameters.Clear();

            if (includeKey)
            {
                command.Parameters.Add(new SqlParameter("@MaNhaCungCap", SqlDbType.Int)
                {
                    Value = nhaCungCap.MaNhaCungCap
                });
            }

            command.Parameters.Add(new SqlParameter("@TenNhaCungCap", SqlDbType.NVarChar, 200)
            {
                Value = nhaCungCap.TenNhaCungCap
            });

            command.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200)
            {
                Value = nhaCungCap.DiaChi ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200)
            {
                Value = nhaCungCap.Email ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 20)
            {
                Value = nhaCungCap.SoDienThoai ?? (object)DBNull.Value
            });


            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
            {
                Value = nhaCungCap.TrangThai ?? (object)DBNull.Value
            });
        }
    }
}