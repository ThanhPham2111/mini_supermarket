using System;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class CauHinhLoiNhuan_DAO
    {
        public CauHinhLoiNhuanDTO? GetCauHinh()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT TOP 1 MaCauHinh, PhanTramLoiNhuanMacDinh, NgayCapNhat, MaNhanVien
                                   FROM dbo.Tbl_CauHinhLoiNhuan
                                   ORDER BY NgayCapNhat DESC";

            connection.Open();
            using var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return new CauHinhLoiNhuanDTO
                {
                    MaCauHinh = reader.GetInt32(reader.GetOrdinal("MaCauHinh")),
                    PhanTramLoiNhuanMacDinh = reader.GetDecimal(reader.GetOrdinal("PhanTramLoiNhuanMacDinh")),
                    NgayCapNhat = reader.IsDBNull(reader.GetOrdinal("NgayCapNhat")) 
                        ? null : reader.GetDateTime(reader.GetOrdinal("NgayCapNhat")),
                    MaNhanVien = reader.IsDBNull(reader.GetOrdinal("MaNhanVien")) 
                        ? null : reader.GetInt32(reader.GetOrdinal("MaNhanVien"))
                };
            }

            return null;
        }

        public void UpdateCauHinh(decimal phanTramLoiNhuan, int maNhanVien)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_CauHinhLoiNhuan (PhanTramLoiNhuanMacDinh, MaNhanVien)
                                   VALUES (@PhanTramLoiNhuanMacDinh, @MaNhanVien)";

            command.Parameters.Add(new SqlParameter("@PhanTramLoiNhuanMacDinh", SqlDbType.Decimal)
            {
                Precision = 5,
                Scale = 2,
                Value = phanTramLoiNhuan
            });

            command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.Int)
            {
                Value = maNhanVien
            });

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}

