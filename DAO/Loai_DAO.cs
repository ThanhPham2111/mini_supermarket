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
    }
}
