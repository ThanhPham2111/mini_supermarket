using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class DonVi_DAO
    {
        public IList<DonViDTO> GetAll()
        {
            var result = new List<DonViDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaDonVi, TenDonVi, MoTa
                                   FROM dbo.Tbl_DonVi
                                   ORDER BY TenDonVi";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new DonViDTO
                {
                    MaDonVi = reader.GetInt32(0),
                    TenDonVi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    MoTa = reader.IsDBNull(2) ? null : reader.GetString(2)
                });
            }

            return result;
        }
    }
}
