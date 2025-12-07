using System;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class CauHinhQuyDoiDiem_DAO
    {
        public CauHinhQuyDoiDiemDTO? GetCauHinh()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT TOP 1 MaCauHinh, SoDiem, SoTienTuongUng, NgayCapNhat, MaNhanVien
                                   FROM dbo.Tbl_CauHinhQuyDoiDiem
                                   ORDER BY NgayCapNhat DESC";

            connection.Open();
            using var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return new CauHinhQuyDoiDiemDTO
                {
                    MaCauHinh = reader.GetInt32(reader.GetOrdinal("MaCauHinh")),
                    SoDiem = reader.GetInt32(reader.GetOrdinal("SoDiem")),
                    SoTienTuongUng = reader.GetDecimal(reader.GetOrdinal("SoTienTuongUng")),
                    NgayCapNhat = reader.IsDBNull(reader.GetOrdinal("NgayCapNhat")) 
                        ? null : reader.GetDateTime(reader.GetOrdinal("NgayCapNhat")),
                    MaNhanVien = reader.IsDBNull(reader.GetOrdinal("MaNhanVien")) 
                        ? null : reader.GetInt32(reader.GetOrdinal("MaNhanVien"))
                };
            }

            return null;
        }

        public void UpdateCauHinh(int soDiem, decimal soTienTuongUng, int maNhanVien)
        {
            if (soDiem <= 0)
                throw new ArgumentException("Số điểm phải lớn hơn 0.");
            
            if (soTienTuongUng <= 0)
                throw new ArgumentException("Số tiền tương ứng phải lớn hơn 0.");

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_CauHinhQuyDoiDiem (SoDiem, SoTienTuongUng, MaNhanVien)
                                   VALUES (@SoDiem, @SoTienTuongUng, @MaNhanVien)";

            command.Parameters.Add(new SqlParameter("@SoDiem", SqlDbType.Int)
            {
                Value = soDiem
            });

            command.Parameters.Add(new SqlParameter("@SoTienTuongUng", SqlDbType.Decimal)
            {
                Precision = 18,
                Scale = 2,
                Value = soTienTuongUng
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

