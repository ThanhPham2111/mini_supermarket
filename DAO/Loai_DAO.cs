using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class Loai_DAO
    {
        public IList<LoaiDTO> GetAll()
        {
            var result = new List<LoaiDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaLoai, TenLoai, MoTa
                                   FROM dbo.Tbl_Loai
                                   ORDER BY TenLoai";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new LoaiDTO
                {
                    MaLoai = reader.GetInt32(0),
                    TenLoai = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    MoTa = reader.IsDBNull(2) ? null : reader.GetString(2)
                });
            }

            return result;
        }

        public LoaiDTO Create(string tenLoai, string? moTa)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_Loai (TenLoai, MoTa)
                                    OUTPUT INSERTED.MaLoai, INSERTED.TenLoai, INSERTED.MoTa
                                    VALUES (@TenLoai, @MoTa)";

            var tenLoaiParameter = new SqlParameter("@TenLoai", System.Data.SqlDbType.NVarChar, 255)
            {
                Value = tenLoai
            };
            command.Parameters.Add(tenLoaiParameter);

            var moTaParameter = new SqlParameter("@MoTa", System.Data.SqlDbType.NVarChar, -1)
            {
                Value = (object?)moTa ?? System.DBNull.Value
            };
            command.Parameters.Add(moTaParameter);

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
                MoTa = reader.IsDBNull(2) ? null : reader.GetString(2)
            };
        }

        public int GetNextIdentityValue()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT CAST(ISNULL(IDENT_CURRENT('dbo.Tbl_Loai'), 0) AS INT) + CAST(IDENT_INCR('dbo.Tbl_Loai') AS INT)";

            connection.Open();
            var result = command.ExecuteScalar();
            if (result == null || result == System.DBNull.Value)
            {
                return 1;
            }

            if (result is decimal decimalValue)
            {
                return (int)decimalValue;
            }

            if (result is long longValue)
            {
                return (int)longValue;
            }

            if (result is int intValue)
            {
                return intValue;
            }

            if (int.TryParse(result.ToString(), out var parsed))
            {
                return parsed;
            }

            return 0;
        }
    }
}
