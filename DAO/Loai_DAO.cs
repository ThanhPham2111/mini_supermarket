using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using mini_supermarket.Common;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class Loai_DAO
    {
        public IList<LoaiDTO> GetAll(string? trangThai = null)
        {
            var result = new List<LoaiDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaLoai, TenLoai, MoTa, TrangThai
                                   FROM dbo.Tbl_Loai
                                   WHERE @TrangThai IS NULL OR TrangThai = @TrangThai
                                   ORDER BY MaLoai ASC";

            var trangThaiParameter = new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = string.IsNullOrWhiteSpace(trangThai) ? DBNull.Value : trangThai.Trim()
            };
            command.Parameters.Add(trangThaiParameter);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new LoaiDTO
                {
                    MaLoai = reader.GetInt32(0),
                    TenLoai = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    MoTa = reader.IsDBNull(2) ? null : reader.GetString(2),
                    TrangThai = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                });
            }

            return result;
        }

        public LoaiDTO Create(string tenLoai, string? moTa, string trangThai)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_Loai (TenLoai, MoTa, TrangThai)
                                    OUTPUT INSERTED.MaLoai, INSERTED.TenLoai, INSERTED.MoTa, INSERTED.TrangThai
                                    VALUES (@TenLoai, @MoTa, @TrangThai)";

            command.Parameters.Add(new SqlParameter("@TenLoai", System.Data.SqlDbType.NVarChar, 255)
            {
                Value = tenLoai
            });

            command.Parameters.Add(new SqlParameter("@MoTa", System.Data.SqlDbType.NVarChar, -1)
            {
                Value = (object?)moTa ?? DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = trangThai
            });

            connection.Open();
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new InvalidOperationException("Không thể tạo loại.");
            }

            return new LoaiDTO
            {
                MaLoai = reader.GetInt32(0),
                TenLoai = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                MoTa = reader.IsDBNull(2) ? null : reader.GetString(2),
                TrangThai = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
            };
        }

        public LoaiDTO Update(int maLoai, string tenLoai, string? moTa, string trangThai)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_Loai
                                    SET TenLoai = @TenLoai, MoTa = @MoTa, TrangThai = @TrangThai
                                    OUTPUT INSERTED.MaLoai, INSERTED.TenLoai, INSERTED.MoTa, INSERTED.TrangThai
                                    WHERE MaLoai = @MaLoai";

            command.Parameters.Add(new SqlParameter("@MaLoai", System.Data.SqlDbType.Int)
            {
                Value = maLoai
            });

            command.Parameters.Add(new SqlParameter("@TenLoai", System.Data.SqlDbType.NVarChar, 255)
            {
                Value = tenLoai
            });

            command.Parameters.Add(new SqlParameter("@MoTa", System.Data.SqlDbType.NVarChar, -1)
            {
                Value = (object?)moTa ?? DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = trangThai
            });

            connection.Open();
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new InvalidOperationException("Không thể cập nhật loại.");
            }

            return new LoaiDTO
            {
                MaLoai = reader.GetInt32(0),
                TenLoai = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                MoTa = reader.IsDBNull(2) ? null : reader.GetString(2),
                TrangThai = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
            };
        }

        public bool Delete(int maLoai)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_Loai
                                    SET TrangThai = @TrangThai
                                    WHERE MaLoai = @MaLoai AND TrangThai <> @TrangThai";

            command.Parameters.Add(new SqlParameter("@MaLoai", System.Data.SqlDbType.Int)
            {
                Value = maLoai
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = TrangThaiConstants.NgungHoatDong
            });

            connection.Open();
            int affected = command.ExecuteNonQuery();
            return affected > 0;
        }

        public int GetNextIdentityValue()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT CAST(ISNULL(IDENT_CURRENT('dbo.Tbl_Loai'), 0) AS INT) + CAST(IDENT_INCR('dbo.Tbl_Loai') AS INT)";

            connection.Open();
            var result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                return 1;
            }

            return result switch
            {
                decimal decimalValue => (int)decimalValue,
                long longValue => (int)longValue,
                int intValue => intValue,
                _ when int.TryParse(result.ToString(), out var parsed) => parsed,
                _ => 0
            };
        }
    }
}
