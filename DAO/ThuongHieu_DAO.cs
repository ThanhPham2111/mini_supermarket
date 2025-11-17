using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using mini_supermarket.Common;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class ThuongHieu_DAO
    {
        public IList<ThuongHieuDTO> GetAll(string? trangThai = null)
        {
            var result = new List<ThuongHieuDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaThuongHieu, TenThuongHieu, TrangThai
                                   FROM dbo.Tbl_ThuongHieu
                                   WHERE @TrangThai IS NULL OR TrangThai = @TrangThai
                                   ORDER BY MaThuongHieu ASC";

            var trangThaiParameter = new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = string.IsNullOrWhiteSpace(trangThai) ? DBNull.Value : trangThai.Trim()
            };
            command.Parameters.Add(trangThaiParameter);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new ThuongHieuDTO
                {
                    MaThuongHieu = reader.GetInt32(0),
                    TenThuongHieu = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    TrangThai = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                });
            }

            return result;
        }

        public ThuongHieuDTO Create(string tenThuongHieu, string trangThai)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_ThuongHieu (TenThuongHieu, TrangThai)
                                    OUTPUT INSERTED.MaThuongHieu, INSERTED.TenThuongHieu, INSERTED.TrangThai
                                    VALUES (@TenThuongHieu, @TrangThai)";

            command.Parameters.Add(new SqlParameter("@TenThuongHieu", System.Data.SqlDbType.NVarChar, 255)
            {
                Value = tenThuongHieu
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = trangThai
            });

            connection.Open();
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new InvalidOperationException("Không thể tạo thương hiệu.");
            }

            return new ThuongHieuDTO
            {
                MaThuongHieu = reader.GetInt32(0),
                TenThuongHieu = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                TrangThai = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
            };
        }

        public ThuongHieuDTO Update(int maThuongHieu, string tenThuongHieu, string trangThai)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_ThuongHieu
                                    SET TenThuongHieu = @TenThuongHieu, TrangThai = @TrangThai
                                    OUTPUT INSERTED.MaThuongHieu, INSERTED.TenThuongHieu, INSERTED.TrangThai
                                    WHERE MaThuongHieu = @MaThuongHieu";

            command.Parameters.Add(new SqlParameter("@MaThuongHieu", System.Data.SqlDbType.Int)
            {
                Value = maThuongHieu
            });

            command.Parameters.Add(new SqlParameter("@TenThuongHieu", System.Data.SqlDbType.NVarChar, 255)
            {
                Value = tenThuongHieu
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = trangThai
            });

            connection.Open();
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new InvalidOperationException("Không thể cập nhật thương hiệu.");
            }

            return new ThuongHieuDTO
            {
                MaThuongHieu = reader.GetInt32(0),
                TenThuongHieu = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                TrangThai = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
            };
        }

        public bool Delete(int maThuongHieu)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_ThuongHieu
                                    SET TrangThai = @TrangThai
                                    WHERE MaThuongHieu = @MaThuongHieu AND TrangThai <> @TrangThai";

            command.Parameters.Add(new SqlParameter("@MaThuongHieu", System.Data.SqlDbType.Int)
            {
                Value = maThuongHieu
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
            command.CommandText = @"SELECT CAST(ISNULL(IDENT_CURRENT('dbo.Tbl_ThuongHieu'), 0) AS INT) + CAST(IDENT_INCR('dbo.Tbl_ThuongHieu') AS INT)";

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
