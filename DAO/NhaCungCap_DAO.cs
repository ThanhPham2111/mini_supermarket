using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class NhaCungCap_DAO {
    public List<NhaCungCapDTO> GetNhaCungCap(string? trangThaiFilter = null)
    {
        var list = new List<NhaCungCapDTO>();

        using var connection = DbConnectionFactory.CreateConnection();
        using var command = connection.CreateCommand();

        // sql cơ bản
        command.CommandText = @"
        SELECT MaNhaCungCap, TenNhaCungCap, DiaChi, SoDienThoai, Email, TrangThai
        FROM dbo.Tbl_NhaCungCap
    ";

        // Nếu có filter trạng thái → thêm WHERE
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
            list.Add(ReadNhaCungCap(reader));
        }

        return list;
    }

    // Lấy mã nhà cung cấp lớn nhất trong DB
    public int GetMaxMaNhaCungCap()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ISNULL(MAX(MaNhaCungCap), 0) FROM dbo.Tbl_NhaCungCap";

            connection.Open();
            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        // Thêm mới nhà cung cấp → trả về id vừa tạo
        public int InsertNhaCungCap(NhaCungCapDTO nhaCungCap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"INSERT INTO dbo.Tbl_NhaCungCap (TenNhaCungCap,DiaChi,SoDienThoai, Email, TrangThai)
                                     VALUES (@TenNhaCungCap, @DiaChi, @SoDienThoai, @Email, @TrangThai);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

            AddParams(command, nhaCungCap, includeId: false);

            connection.Open();
            object? result = command.ExecuteScalar();

            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Không thể tạo nhà cung cấp mới.");
            }

            return Convert.ToInt32(result);
        }

        // Cập nhật thông tin nhà cung cấp
        public void UpdateNhaCungCap(NhaCungCapDTO nhaCungCap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE dbo.Tbl_NhaCungCap
                                     SET TenNhaCungCap = @TenNhaCungCap, 
                                         DiaChi = @DiaChi,
                                         SoDienThoai = @SoDienThoai,
                                         Email = @Email,
                                         TrangThai = @TrangThai
                                     WHERE MaNhaCungCap = @MaNhaCungCap";

            AddParams(command, nhaCungCap, includeId: true);

            connection.Open();
            int rows = command.ExecuteNonQuery();
            if (rows == 0)
                throw new InvalidOperationException("Không tìm thấy nhà cung cấp để cập nhật.");
        }

        // Xóa nhà cung cấp theo mã
        public void DeleteNhaCungCap(int maNhaCungCap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"DELETE FROM dbo.Tbl_NhaCungCap WHERE MaNhaCungCap = @MaNhaCungCap";
            command.Parameters.Add(new SqlParameter("@MaNhaCungCap", SqlDbType.Int)
            {
                Value = maNhaCungCap
            });

            connection.Open();
            int rows = command.ExecuteNonQuery();
            if (rows == 0)
                throw new InvalidOperationException("Không tìm thấy nhà cung cấp để xóa.");
        }

        // Helper: đọc từ DataReader → DTO
        private static NhaCungCapDTO ReadNhaCungCap(SqlDataReader reader)
        {
            return new NhaCungCapDTO
            {
                MaNhaCungCap = reader.GetInt32(reader.GetOrdinal("MaNhaCungCap")),
                TenNhaCungCap = reader.GetString(reader.GetOrdinal("TenNhaCungCap")),
                DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai"))
                                ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai"))
                                ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
            };
        }
        // Helper: thêm parameter
        private static void AddParams(SqlCommand cmd, NhaCungCapDTO dto, bool includeId)
        {
            cmd.Parameters.Clear();

            if (includeId)
            {
                cmd.Parameters.Add(new SqlParameter("@MaNhaCungCap", SqlDbType.Int) { Value = dto.MaNhaCungCap });
            }

            cmd.Parameters.Add(new SqlParameter("@TenNhaCungCap", SqlDbType.NVarChar, 200) { Value = dto.TenNhaCungCap });
            cmd.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = dto.DiaChi ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200) { Value = dto.Email ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 20) { Value = dto.SoDienThoai ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = dto.TrangThai ?? (object)DBNull.Value });
        }
    }
}
