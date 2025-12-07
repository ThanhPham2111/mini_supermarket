using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class NhanVien_DAO
    {
        public IList<NhanVienDTO> GetNhanVien(string? trangThaiFilter = null)
        {
            using var context = new NhanVienDbContext();
            
            var query = context.TblNhanVien.AsQueryable();

            if (!string.IsNullOrWhiteSpace(trangThaiFilter))
            {
                query = query.Where(nv => nv.TrangThai == trangThaiFilter);
            }

            return query
                .OrderBy(nv => nv.MaNhanVien)
                .Select(nv => new NhanVienDTO
                {
                    MaNhanVien = nv.MaNhanVien,
                    TenNhanVien = nv.TenNhanVien,
                    GioiTinh = nv.GioiTinh,
                    NgaySinh = nv.NgaySinh,
                    SoDienThoai = nv.SoDienThoai,
                    VaiTro = nv.VaiTro,
                    TrangThai = nv.TrangThai
                })
                .ToList();
        }

        public int GetMaxMaNhanVien()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ISNULL(MAX(MaNhanVien), 0) FROM dbo.Tbl_NhanVien";

            connection.Open();
            object? result = command.ExecuteScalar();
            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public int InsertNhanVien(NhanVienDTO nhanVien)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_NhanVien (TenNhanVien, GioiTinh, NgaySinh, SoDienThoai, VaiTro, TrangThai)
                                     VALUES (@TenNhanVien, @GioiTinh, @NgaySinh, @SoDienThoai, @VaiTro, @TrangThai);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

            AddNhanVienParameters(command, nhanVien, includeKey: false);

            connection.Open();
            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Không thể tạo nhân viên mới.");
            }

            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void UpdateNhanVien(NhanVienDTO nhanVien)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_NhanVien
                                     SET TenNhanVien = @TenNhanVien,
                                         GioiTinh = @GioiTinh,
                                         NgaySinh = @NgaySinh,
                                         SoDienThoai = @SoDienThoai,
                                         VaiTro = @VaiTro,
                                         TrangThai = @TrangThai
                                     WHERE MaNhanVien = @MaNhanVien";

            AddNhanVienParameters(command, nhanVien, includeKey: true);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy nhân viên để cập nhật.");
            }
        }

        public void DeleteNhanVien(int maNhanVien)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM dbo.Tbl_NhanVien WHERE MaNhanVien = @MaNhanVien";
            command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.Int)
            {
                Value = maNhanVien
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy nhân viên để xóa.");
            }
        }

        private static void AddNhanVienParameters(SqlCommand command, NhanVienDTO nhanVien, bool includeKey)
        {
            command.Parameters.Clear();

            if (includeKey)
            {
                command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.Int)
                {
                    Value = nhanVien.MaNhanVien
                });
            }

            command.Parameters.Add(new SqlParameter("@TenNhanVien", SqlDbType.NVarChar, 200)
            {
                Value = nhanVien.TenNhanVien
            });

            command.Parameters.Add(new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10)
            {
                Value = nhanVien.GioiTinh ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@NgaySinh", SqlDbType.Date)
            {
                Value = nhanVien.NgaySinh?.Date ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 20)
            {
                Value = nhanVien.SoDienThoai ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@VaiTro", SqlDbType.NVarChar, 100)
            {
                Value = nhanVien.VaiTro ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
            {
                Value = nhanVien.TrangThai ?? (object)DBNull.Value
            });
        }

        // Thêm hàm getNhanVienByID()
        public NhanVienDTO GetNhanVienByID(String maNV)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.Tbl_NhanVien";
            command.CommandText += " WHERE MaNhanVien = @MaNhanVien";
            command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.NVarChar, 50)
            {
                Value = maNV
            });

            connection.Open();
            using var reader = command.ExecuteReader();
            NhanVienDTO nv_tmp = new NhanVienDTO();
            if (reader.Read())
            {
                nv_tmp = new NhanVienDTO
                {
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    TenNhanVien = reader.GetString(reader.GetOrdinal("TenNhanVien")),
                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? null : reader.GetString(reader.GetOrdinal("GioiTinh")),
                    NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? null : reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    VaiTro = reader.IsDBNull(reader.GetOrdinal("VaiTro")) ? null : reader.GetString(reader.GetOrdinal("VaiTro")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                };
            }
            return nv_tmp;
        }

        public NhanVienDTO? GetNhanVienByID(int maNhanVien)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaNhanVien, TenNhanVien, GioiTinh, NgaySinh, SoDienThoai, VaiTro, TrangThai
                                     FROM dbo.Tbl_NhanVien
                                     WHERE MaNhanVien = @MaNhanVien";
            command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.Int)
            {
                Value = maNhanVien
            });

            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new NhanVienDTO
                {
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    TenNhanVien = reader.GetString(reader.GetOrdinal("TenNhanVien")),
                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? null : reader.GetString(reader.GetOrdinal("GioiTinh")),
                    NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? null : reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    VaiTro = reader.IsDBNull(reader.GetOrdinal("VaiTro")) ? null : reader.GetString(reader.GetOrdinal("VaiTro")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                };
            }
            return null;
        }
    }
}
