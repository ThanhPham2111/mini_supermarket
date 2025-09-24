using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class NhanVien_DAO
    {
        public IList<NhanVienDTO> GetNhanVien(string? trangThaiFilter = null)
        {
            var nhanVienList = new List<NhanVienDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaNhanVien, TenNhanVien, GioiTinh, NgaySinh, SoDienThoai, VaiTro, TrangThai
                                     FROM dbo.Tbl_NhanVien";

            if (!string.IsNullOrWhiteSpace(trangThaiFilter))
            {
                command.CommandText += " WHERE TrangThai = @TrangThai";
                command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
                {
                    Value = trangThaiFilter
                });
            }

            command.CommandText += " ORDER BY MaNhanVien";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                nhanVienList.Add(new NhanVienDTO
                {
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    TenNhanVien = reader.GetString(reader.GetOrdinal("TenNhanVien")),
                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? null : reader.GetString(reader.GetOrdinal("GioiTinh")),
                    NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? null : reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    VaiTro = reader.IsDBNull(reader.GetOrdinal("VaiTro")) ? null : reader.GetString(reader.GetOrdinal("VaiTro")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                });
            }

            return nhanVienList;
        }
    }
}
