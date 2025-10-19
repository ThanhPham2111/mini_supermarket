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
                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi")),
                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                });
            }

            return nhaCungCapList;
        }
    }
}
