using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class ThuongHieu_DAO
    {
        public IList<ThuongHieuDTO> GetAll()
        {
            var result = new List<ThuongHieuDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaThuongHieu, TenThuongHieu
                                   FROM dbo.Tbl_ThuongHieu
                                   ORDER BY MaThuongHieu ASC";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new ThuongHieuDTO
                {
                    MaThuongHieu = reader.GetInt32(0),
                    TenThuongHieu = reader.IsDBNull(1) ? string.Empty : reader.GetString(1)
                });
            }

            return result;
        }
    }
}
