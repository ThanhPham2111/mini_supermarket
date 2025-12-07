using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class KhachHang_DAO
    {
        public IList<KhachHangDTO> GetKhachHang(string? trangThaiFilter = null)
        {
            var khachHangList = new List<KhachHangDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaKhachHang, TenKhachHang, SoDienThoai, DiaChi, Email, DiemTichLuy, TrangThai
                                     FROM dbo.Tbl_KhachHang";

            if (!string.IsNullOrWhiteSpace(trangThaiFilter))
            {
                command.CommandText += " WHERE TrangThai = @TrangThai";
                command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
                {
                    Value = trangThaiFilter
                });
            }

            command.CommandText += " ORDER BY MaKhachHang";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                khachHangList.Add(new KhachHangDTO
                {
                    MaKhachHang = reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    TenKhachHang = reader.GetString(reader.GetOrdinal("TenKhachHang")),
                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi")),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                    DiemTichLuy = reader.GetInt32(reader.GetOrdinal("DiemTichLuy")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                });
            }

            return khachHangList;
        }
        
        public int GetMaxMaKhachHang()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ISNULL(MAX(MaKhachHang), 0) FROM dbo.Tbl_KhachHang";

            connection.Open();
            object? result = command.ExecuteScalar();
            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }


        public int InsertKhachHang(KhachHangDTO khachHang)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_KhachHang (TenKhachHang, SoDienThoai, DiaChi, Email, DiemTichLuy, TrangThai)
                                     VALUES (@TenKhachHang, @SoDienThoai, @DiaChi, @Email, @DiemTichLuy, @TrangThai);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

            AddKhachHangParameters(command, khachHang, includeKey: false);

            connection.Open();
            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Khong the tao khach hang moi.");
            }

            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void UpdateKhachHang(KhachHangDTO khachHang)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_KhachHang
                                     SET TenKhachHang = @TenKhachHang,
                                         SoDienThoai = @SoDienThoai,
                                         DiaChi = @DiaChi,
                                         Email = @Email,
                                         DiemTichLuy = @DiemTichLuy,
                                         TrangThai = @TrangThai
                                     WHERE MaKhachHang = @MaKhachHang";

            AddKhachHangParameters(command, khachHang, includeKey: true);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Khong tim thay khach hang de cap nhat.");
            }
        }

        public void DeleteKhachHang(int maKhachHang)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM dbo.Tbl_KhachHang WHERE MaKhachHang = @MaKhachHang";
            command.Parameters.Add(new SqlParameter("@MaKhachHang", SqlDbType.Int)
            {
                Value = maKhachHang
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Khong tim thay khach hang de xoa.");
            }
        }

        private static void AddKhachHangParameters(SqlCommand command, KhachHangDTO khachHang, bool includeKey)
        {
            command.Parameters.Clear();

            if (includeKey)
            {
                command.Parameters.Add(new SqlParameter("@MaKhachHang", SqlDbType.Int)
                {
                    Value = khachHang.MaKhachHang
                });
            }

            command.Parameters.Add(new SqlParameter("@TenKhachHang", SqlDbType.NVarChar, 200)
            {
                Value = khachHang.TenKhachHang
            });

            command.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200)
            {
                Value = khachHang.DiaChi ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200)
            {
                Value = khachHang.Email ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 20)
            {
                Value = khachHang.SoDienThoai ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@DiemTichLuy", SqlDbType.Int)
            {
                Value = khachHang.DiemTichLuy ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
            {
                Value = khachHang.TrangThai ?? (object)DBNull.Value
            });
        }

        public void UpdateDiemTichLuy(int maKhachHang, int diemMoi)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE dbo.Tbl_KhachHang
                SET DiemTichLuy = @DiemTichLuy
                WHERE MaKhachHang = @MaKhachHang";

            command.Parameters.Add(new SqlParameter("@MaKhachHang", SqlDbType.Int)
            {
                Value = maKhachHang
            });

            command.Parameters.Add(new SqlParameter("@DiemTichLuy", SqlDbType.Int)
            {
                Value = diemMoi
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy khách hàng để cập nhật điểm.");
            }
        }
    }
}