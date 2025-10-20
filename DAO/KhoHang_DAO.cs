using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class KhoHang_DAO
    {
        // --- Lấy danh sách kho hàng, có thể lọc theo trạng thái ---
        public IList<KhoHangDTO> GetKhoHang(string? trangThaiFilter = null)
        {
            var khoHangList = new List<KhoHangDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();

            // JOIN để lấy TenSanPham
            command.CommandText = @"
                SELECT kh.MaSanPham,
                       sp.TenSanPham,
                       kh.SoLuong,
                       kh.TrangThai
                FROM dbo.Tbl_KhoHang kh
                INNER JOIN dbo.Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham";

            if (!string.IsNullOrWhiteSpace(trangThaiFilter))
            {
                command.CommandText += " WHERE kh.TrangThai = @TrangThai";
                command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = trangThaiFilter });
            }

            // ORDER BY chỉ các cột trong SELECT
            command.CommandText += " ORDER BY sp.TenSanPham";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                khoHangList.Add(new KhoHangDTO
                {
                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")).Trim(),
                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))?.Trim()
                });
            }

            return khoHangList;
        }

        // --- Thêm kho hàng ---
        public int InsertKhoHang(KhoHangDTO kho)
        {
            if (kho == null) throw new ArgumentNullException(nameof(kho));

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO dbo.Tbl_KhoHang (MaSanPham, SoLuong, TrangThai)
                VALUES (@MaSanPham, @SoLuong, @TrangThai)";

            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = kho.MaSanPham });
            command.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int) { Value = (object?)kho.SoLuong ?? DBNull.Value });
            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = (object?)kho.TrangThai ?? DBNull.Value });

            connection.Open();
            return command.ExecuteNonQuery();
        }

        // --- Cập nhật kho hàng ---
        public int UpdateKhoHang(KhoHangDTO kho)
        {
            if (kho == null) throw new ArgumentNullException(nameof(kho));

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE dbo.Tbl_KhoHang
                SET SoLuong = @SoLuong,
                    TrangThai = @TrangThai
                WHERE MaSanPham = @MaSanPham";

            command.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int) { Value = (object?)kho.SoLuong ?? DBNull.Value });
            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = (object?)kho.TrangThai ?? DBNull.Value });
            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = kho.MaSanPham });

            connection.Open();
            return command.ExecuteNonQuery();
        }

        // --- Xóa kho hàng ---
        public int DeleteKhoHang(int maSanPham)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM dbo.Tbl_KhoHang WHERE MaSanPham = @MaSanPham";
            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = maSanPham });

            connection.Open();
            return command.ExecuteNonQuery();
        }

        // --- Kiểm tra sản phẩm đã tồn tại trong kho hàng chưa ---
        public bool ExistsByMaSanPham(int maSanPham)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(1) FROM dbo.Tbl_KhoHang WHERE MaSanPham = @MaSanPham";
            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = maSanPham });

            connection.Open();
            var result = command.ExecuteScalar();
            return result != null && Convert.ToInt32(result) > 0;
        }

        // --- Lấy thông tin kho hàng theo mã sản phẩm ---
        public KhoHangDTO? GetByMaSanPham(int maSanPham)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaSanPham, SoLuong, TrangThai 
                                   FROM dbo.Tbl_KhoHang 
                                   WHERE MaSanPham = @MaSanPham";
            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = maSanPham });

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new KhoHangDTO
                {
                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))?.Trim()
                };
            }

            return null;
        }

        // --- Lấy danh sách trạng thái có trong kho ---
        public IList<string> GetDistinctTrangThai()
        {
            var statuses = new List<string>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT DISTINCT TrangThai
                FROM dbo.Tbl_KhoHang
                WHERE TrangThai IS NOT NULL AND LTRIM(RTRIM(TrangThai)) <> ''
                ORDER BY TrangThai"; // ORDER BY hợp lệ vì cột xuất hiện trong SELECT

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                statuses.Add(reader.GetString(0).Trim());
            }

            return statuses;
        }

        // --- Lấy danh sách tất cả sản phẩm (để chọn khi thêm/sửa) ---
        public IList<SanPhamDTO> GetAllProducts()
        {
            var list = new List<SanPhamDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT MaSanPham, TenSanPham
                FROM dbo.Tbl_SanPham
                ORDER BY TenSanPham";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SanPhamDTO
                {
                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")).Trim()
                });
            }

            return list;
        }
    }
}
