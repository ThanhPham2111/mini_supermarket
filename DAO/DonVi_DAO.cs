using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using mini_supermarket.Common;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class DonVi_DAO
    {
        public IList<DonViDTO> GetAll(string? trangThai = null)
        {
            var result = new List<DonViDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaDonVi, TenDonVi, MoTa, TrangThai
                                   FROM dbo.Tbl_DonVi
                                   WHERE @TrangThai IS NULL OR TrangThai = @TrangThai
                                   ORDER BY MaDonVi ASC";

            var trangThaiParameter = new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = string.IsNullOrWhiteSpace(trangThai) ? DBNull.Value : trangThai.Trim()
            };
            command.Parameters.Add(trangThaiParameter);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new DonViDTO
                {
                    MaDonVi = reader.GetInt32(0),
                    TenDonVi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    MoTa = reader.IsDBNull(2) ? null : reader.GetString(2),
                    TrangThai = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                });
            }

            return result;
        }

        public DonViDTO Create(string tenDonVi, string? moTa, string trangThai)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_DonVi (TenDonVi, MoTa, TrangThai)
                                    OUTPUT INSERTED.MaDonVi, INSERTED.TenDonVi, INSERTED.MoTa, INSERTED.TrangThai
                                    VALUES (@TenDonVi, @MoTa, @TrangThai)";

            command.Parameters.Add(new SqlParameter("@TenDonVi", System.Data.SqlDbType.NVarChar, 100)
            {
                Value = tenDonVi
            });

            command.Parameters.Add(new SqlParameter("@MoTa", System.Data.SqlDbType.NVarChar, 255)
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
                throw new InvalidOperationException("Không thể tạo đơn vị.");
            }

            return new DonViDTO
            {
                MaDonVi = reader.GetInt32(0),
                TenDonVi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                MoTa = reader.IsDBNull(2) ? null : reader.GetString(2),
                TrangThai = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
            };
        }

        public DonViDTO Update(int maDonVi, string tenDonVi, string? moTa, string trangThai)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_DonVi
                                    SET TenDonVi = @TenDonVi, MoTa = @MoTa, TrangThai = @TrangThai
                                    OUTPUT INSERTED.MaDonVi, INSERTED.TenDonVi, INSERTED.MoTa, INSERTED.TrangThai
                                    WHERE MaDonVi = @MaDonVi";

            command.Parameters.Add(new SqlParameter("@MaDonVi", System.Data.SqlDbType.Int)
            {
                Value = maDonVi
            });

            command.Parameters.Add(new SqlParameter("@TenDonVi", System.Data.SqlDbType.NVarChar, 100)
            {
                Value = tenDonVi
            });

            command.Parameters.Add(new SqlParameter("@MoTa", System.Data.SqlDbType.NVarChar, 255)
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
                throw new InvalidOperationException("Không thể cập nhật đơn vị.");
            }

            return new DonViDTO
            {
                MaDonVi = reader.GetInt32(0),
                TenDonVi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                MoTa = reader.IsDBNull(2) ? null : reader.GetString(2),
                TrangThai = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
            };
        }

        public bool Delete(int maDonVi)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_DonVi
                                    SET TrangThai = @TrangThai
                                    WHERE MaDonVi = @MaDonVi AND TrangThai <> @TrangThai";

            command.Parameters.Add(new SqlParameter("@MaDonVi", System.Data.SqlDbType.Int)
            {
                Value = maDonVi
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
            command.CommandText = @"SELECT CAST(ISNULL(IDENT_CURRENT('dbo.Tbl_DonVi'), 0) AS INT) + CAST(IDENT_INCR('dbo.Tbl_DonVi') AS INT)";

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
