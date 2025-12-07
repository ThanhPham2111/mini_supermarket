using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class KhuyenMai_DAO
    {
        public IList<KhuyenMaiDTO> GetKhuyenMai(int? maSanPhamFilter = null)
        {
            var list = new List<KhuyenMaiDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaKhuyenMai, MaSanPham, TenKhuyenMai, PhanTramGiamGia, NgayBatDau, NgayKetThuc, MoTa
                                        FROM dbo.Tbl_KhuyenMai";

            if (maSanPhamFilter.HasValue)
            {
                command.CommandText += " WHERE MaSanPham = @MaSanPham";
                command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = maSanPhamFilter.Value });
            }

            command.CommandText += " ORDER BY MaKhuyenMai";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new KhuyenMaiDTO
                {
                    MaKhuyenMai = reader.GetInt32(reader.GetOrdinal("MaKhuyenMai")),
                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    TenKhuyenMai = reader.IsDBNull(reader.GetOrdinal("TenKhuyenMai")) ? null : reader.GetString(reader.GetOrdinal("TenKhuyenMai")),
                    PhanTramGiamGia = reader.IsDBNull(reader.GetOrdinal("PhanTramGiamGia")) ? null : reader.GetDecimal(reader.GetOrdinal("PhanTramGiamGia")),
                    NgayBatDau = reader.IsDBNull(reader.GetOrdinal("NgayBatDau")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                    NgayKetThuc = reader.IsDBNull(reader.GetOrdinal("NgayKetThuc")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayKetThuc")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString(reader.GetOrdinal("MoTa"))
                });
            }

            return list;
        }

        public int GetMaxMaKhuyenMai()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ISNULL(MAX(MaKhuyenMai), 0) FROM dbo.Tbl_KhuyenMai";

            connection.Open();
            object? result = command.ExecuteScalar();
            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public int InsertKhuyenMai(KhuyenMaiDTO kh)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_KhuyenMai (MaSanPham, TenKhuyenMai, PhanTramGiamGia, NgayBatDau, NgayKetThuc, MoTa)
                                     VALUES (@MaSanPham, @TenKhuyenMai, @PhanTramGiamGia, @NgayBatDau, @NgayKetThuc, @MoTa);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

            AddParameters(command, kh, includeKey: false);

            connection.Open();
            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Không thể tạo khuyến mãi mới.");
            }

            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void UpdateKhuyenMai(KhuyenMaiDTO kh)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_KhuyenMai
                                     SET MaSanPham = @MaSanPham,
                                         TenKhuyenMai = @TenKhuyenMai,
                                         PhanTramGiamGia = @PhanTramGiamGia,
                                         NgayBatDau = @NgayBatDau,
                                         NgayKetThuc = @NgayKetThuc,
                                         MoTa = @MoTa
                                     WHERE MaKhuyenMai = @MaKhuyenMai";

            AddParameters(command, kh, includeKey: true);

            connection.Open();
            int rows = command.ExecuteNonQuery();
            if (rows == 0)
            {
                throw new InvalidOperationException("Không tìm thấy khuyến mãi để cập nhật.");
            }
        }

        public void DeleteKhuyenMai(int maKhuyenMai)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM dbo.Tbl_KhuyenMai WHERE MaKhuyenMai = @MaKhuyenMai";
            command.Parameters.Add(new SqlParameter("@MaKhuyenMai", SqlDbType.Int) { Value = maKhuyenMai });

            connection.Open();
            int rows = command.ExecuteNonQuery();
            if (rows == 0)
            {
                throw new InvalidOperationException("Không tìm thấy khuyến mãi để xóa.");
            }
        }

        public bool ExistsKhuyenMaiForProduct(int maSanPham, int? excludeMaKhuyenMai = null)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(1) FROM dbo.Tbl_KhuyenMai WHERE MaSanPham = @MaSanPham";
            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = maSanPham });

            if (excludeMaKhuyenMai.HasValue)
            {
                command.CommandText += " AND MaKhuyenMai <> @MaKhuyenMai";
                command.Parameters.Add(new SqlParameter("@MaKhuyenMai", SqlDbType.Int) { Value = excludeMaKhuyenMai.Value });
            }

            connection.Open();
            object? result = command.ExecuteScalar();
            int count = Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
            return count > 0;
        }

        private static void AddParameters(SqlCommand command, KhuyenMaiDTO kh, bool includeKey)
        {
            command.Parameters.Clear();

            if (includeKey)
            {
                command.Parameters.Add(new SqlParameter("@MaKhuyenMai", SqlDbType.Int) { Value = kh.MaKhuyenMai });
            }

            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = kh.MaSanPham });

            command.Parameters.Add(new SqlParameter("@TenKhuyenMai", SqlDbType.NVarChar, 255)
            {
                Value = kh.TenKhuyenMai ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@PhanTramGiamGia", SqlDbType.Decimal)
            {
                Precision = 5,
                Scale = 2,
                Value = kh.PhanTramGiamGia ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@NgayBatDau", SqlDbType.DateTime)
            {
                Value = kh.NgayBatDau ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@NgayKetThuc", SqlDbType.DateTime)
            {
                Value = kh.NgayKetThuc ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@MoTa", SqlDbType.NVarChar, -1)
            {
                Value = kh.MoTa ?? (object)DBNull.Value
            });
        }
    }
}
