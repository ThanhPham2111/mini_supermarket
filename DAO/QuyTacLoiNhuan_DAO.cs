using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class QuyTacLoiNhuan_DAO
    {
        public IList<QuyTacLoiNhuanDTO> GetQuyTacLoiNhuan(string? loaiQuyTac = null)
        {
            var list = new List<QuyTacLoiNhuanDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            
            string query = @"SELECT q.MaQuyTac, q.LoaiQuyTac, q.MaLoai, q.MaThuongHieu, q.MaDonVi, q.MaSanPham,
                                   q.PhanTramLoiNhuan, q.UuTien, q.TrangThai, q.NgayTao, q.NgayCapNhat, q.MaNhanVien,
                                   l.TenLoai, th.TenThuongHieu, dv.TenDonVi, sp.TenSanPham
                            FROM dbo.Tbl_QuyTacLoiNhuan q
                            LEFT JOIN dbo.Tbl_Loai l ON q.MaLoai = l.MaLoai
                            LEFT JOIN dbo.Tbl_ThuongHieu th ON q.MaThuongHieu = th.MaThuongHieu
                            LEFT JOIN dbo.Tbl_DonVi dv ON q.MaDonVi = dv.MaDonVi
                            LEFT JOIN dbo.Tbl_SanPham sp ON q.MaSanPham = sp.MaSanPham
                            WHERE q.TrangThai = N'Hoạt động'";

            if (!string.IsNullOrWhiteSpace(loaiQuyTac))
            {
                query += " AND q.LoaiQuyTac = @LoaiQuyTac";
                command.Parameters.Add(new SqlParameter("@LoaiQuyTac", SqlDbType.NVarChar, 50)
                {
                    Value = loaiQuyTac
                });
            }

            query += " ORDER BY q.UuTien DESC, q.NgayCapNhat DESC";

            command.CommandText = query;
            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new QuyTacLoiNhuanDTO
                {
                    MaQuyTac = reader.GetInt32(reader.GetOrdinal("MaQuyTac")),
                    LoaiQuyTac = reader.GetString(reader.GetOrdinal("LoaiQuyTac")),
                    MaLoai = reader.IsDBNull(reader.GetOrdinal("MaLoai")) ? null : reader.GetInt32(reader.GetOrdinal("MaLoai")),
                    MaThuongHieu = reader.IsDBNull(reader.GetOrdinal("MaThuongHieu")) ? null : reader.GetInt32(reader.GetOrdinal("MaThuongHieu")),
                    MaDonVi = reader.IsDBNull(reader.GetOrdinal("MaDonVi")) ? null : reader.GetInt32(reader.GetOrdinal("MaDonVi")),
                    MaSanPham = reader.IsDBNull(reader.GetOrdinal("MaSanPham")) ? null : reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    PhanTramLoiNhuan = reader.GetDecimal(reader.GetOrdinal("PhanTramLoiNhuan")),
                    UuTien = reader.GetInt32(reader.GetOrdinal("UuTien")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                    NgayTao = reader.IsDBNull(reader.GetOrdinal("NgayTao")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayTao")),
                    NgayCapNhat = reader.IsDBNull(reader.GetOrdinal("NgayCapNhat")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayCapNhat")),
                    MaNhanVien = reader.IsDBNull(reader.GetOrdinal("MaNhanVien")) ? null : reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    TenLoai = reader.IsDBNull(reader.GetOrdinal("TenLoai")) ? null : reader.GetString(reader.GetOrdinal("TenLoai")),
                    TenThuongHieu = reader.IsDBNull(reader.GetOrdinal("TenThuongHieu")) ? null : reader.GetString(reader.GetOrdinal("TenThuongHieu")),
                    TenDonVi = reader.IsDBNull(reader.GetOrdinal("TenDonVi")) ? null : reader.GetString(reader.GetOrdinal("TenDonVi")),
                    TenSanPham = reader.IsDBNull(reader.GetOrdinal("TenSanPham")) ? null : reader.GetString(reader.GetOrdinal("TenSanPham"))
                });
            }

            return list;
        }

        public QuyTacLoiNhuanDTO? GetQuyTacLoiNhuanById(int maQuyTac)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT q.MaQuyTac, q.LoaiQuyTac, q.MaLoai, q.MaThuongHieu, q.MaDonVi, q.MaSanPham,
                                           q.PhanTramLoiNhuan, q.UuTien, q.TrangThai, q.NgayTao, q.NgayCapNhat, q.MaNhanVien,
                                           l.TenLoai, th.TenThuongHieu, dv.TenDonVi, sp.TenSanPham
                                    FROM dbo.Tbl_QuyTacLoiNhuan q
                                    LEFT JOIN dbo.Tbl_Loai l ON q.MaLoai = l.MaLoai
                                    LEFT JOIN dbo.Tbl_ThuongHieu th ON q.MaThuongHieu = th.MaThuongHieu
                                    LEFT JOIN dbo.Tbl_DonVi dv ON q.MaDonVi = dv.MaDonVi
                                    LEFT JOIN dbo.Tbl_SanPham sp ON q.MaSanPham = sp.MaSanPham
                                    WHERE q.MaQuyTac = @MaQuyTac";

            command.Parameters.Add(new SqlParameter("@MaQuyTac", SqlDbType.Int)
            {
                Value = maQuyTac
            });

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new QuyTacLoiNhuanDTO
                {
                    MaQuyTac = reader.GetInt32(reader.GetOrdinal("MaQuyTac")),
                    LoaiQuyTac = reader.GetString(reader.GetOrdinal("LoaiQuyTac")),
                    MaLoai = reader.IsDBNull(reader.GetOrdinal("MaLoai")) ? null : reader.GetInt32(reader.GetOrdinal("MaLoai")),
                    MaThuongHieu = reader.IsDBNull(reader.GetOrdinal("MaThuongHieu")) ? null : reader.GetInt32(reader.GetOrdinal("MaThuongHieu")),
                    MaDonVi = reader.IsDBNull(reader.GetOrdinal("MaDonVi")) ? null : reader.GetInt32(reader.GetOrdinal("MaDonVi")),
                    MaSanPham = reader.IsDBNull(reader.GetOrdinal("MaSanPham")) ? null : reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    PhanTramLoiNhuan = reader.GetDecimal(reader.GetOrdinal("PhanTramLoiNhuan")),
                    UuTien = reader.GetInt32(reader.GetOrdinal("UuTien")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                    NgayTao = reader.IsDBNull(reader.GetOrdinal("NgayTao")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayTao")),
                    NgayCapNhat = reader.IsDBNull(reader.GetOrdinal("NgayCapNhat")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayCapNhat")),
                    MaNhanVien = reader.IsDBNull(reader.GetOrdinal("MaNhanVien")) ? null : reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    TenLoai = reader.IsDBNull(reader.GetOrdinal("TenLoai")) ? null : reader.GetString(reader.GetOrdinal("TenLoai")),
                    TenThuongHieu = reader.IsDBNull(reader.GetOrdinal("TenThuongHieu")) ? null : reader.GetString(reader.GetOrdinal("TenThuongHieu")),
                    TenDonVi = reader.IsDBNull(reader.GetOrdinal("TenDonVi")) ? null : reader.GetString(reader.GetOrdinal("TenDonVi")),
                    TenSanPham = reader.IsDBNull(reader.GetOrdinal("TenSanPham")) ? null : reader.GetString(reader.GetOrdinal("TenSanPham"))
                };
            }

            return null;
        }

        public int InsertQuyTac(QuyTacLoiNhuanDTO quyTac)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_QuyTacLoiNhuan 
                                   (LoaiQuyTac, MaLoai, MaThuongHieu, MaDonVi, MaSanPham, PhanTramLoiNhuan, UuTien, TrangThai, MaNhanVien)
                                   VALUES (@LoaiQuyTac, @MaLoai, @MaThuongHieu, @MaDonVi, @MaSanPham, @PhanTramLoiNhuan, @UuTien, @TrangThai, @MaNhanVien);
                                   SELECT CAST(SCOPE_IDENTITY() AS INT);";

            AddParameters(command, quyTac, includeKey: false);

            connection.Open();
            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Không thể tạo quy tắc lợi nhuận mới.");
            }

            return Convert.ToInt32(result);
        }

        public void UpdateQuyTac(QuyTacLoiNhuanDTO quyTac)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_QuyTacLoiNhuan
                                   SET PhanTramLoiNhuan = @PhanTramLoiNhuan,
                                       UuTien = @UuTien,
                                       TrangThai = @TrangThai,
                                       NgayCapNhat = GETDATE(),
                                       MaNhanVien = @MaNhanVien
                                   WHERE MaQuyTac = @MaQuyTac";

            AddParameters(command, quyTac, includeKey: true);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy quy tắc lợi nhuận để cập nhật.");
            }
        }

        public void DeleteQuyTac(int maQuyTac)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_QuyTacLoiNhuan
                                   SET TrangThai = N'Ngưng hoạt động'
                                   WHERE MaQuyTac = @MaQuyTac";

            command.Parameters.Add(new SqlParameter("@MaQuyTac", SqlDbType.Int)
            {
                Value = maQuyTac
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy quy tắc lợi nhuận để xóa.");
            }
        }

        public QuyTacLoiNhuanDTO? GetQuyTacApDungChoSanPham(int maSanPham)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            // Chỉ tìm quy tắc TheoSanPham, KHÔNG tìm "Chung" nữa
            // Nếu không có quy tắc TheoSanPham, sẽ dùng % mặc định từ Tbl_CauHinhLoiNhuan
            command.CommandText = @"SELECT TOP 1 q.MaQuyTac, q.LoaiQuyTac, q.MaLoai, q.MaThuongHieu, q.MaDonVi, q.MaSanPham,
                                           q.PhanTramLoiNhuan, q.UuTien, q.TrangThai, q.NgayTao, q.NgayCapNhat, q.MaNhanVien
                                    FROM dbo.Tbl_QuyTacLoiNhuan q
                                    WHERE q.TrangThai = N'Hoạt động'
                                    AND q.LoaiQuyTac = N'TheoSanPham'
                                    AND q.MaSanPham = @MaSanPham
                                    ORDER BY q.NgayCapNhat DESC";

            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = maSanPham });

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new QuyTacLoiNhuanDTO
                {
                    MaQuyTac = reader.GetInt32(reader.GetOrdinal("MaQuyTac")),
                    LoaiQuyTac = reader.GetString(reader.GetOrdinal("LoaiQuyTac")),
                    MaLoai = reader.IsDBNull(reader.GetOrdinal("MaLoai")) ? null : reader.GetInt32(reader.GetOrdinal("MaLoai")),
                    MaThuongHieu = reader.IsDBNull(reader.GetOrdinal("MaThuongHieu")) ? null : reader.GetInt32(reader.GetOrdinal("MaThuongHieu")),
                    MaDonVi = reader.IsDBNull(reader.GetOrdinal("MaDonVi")) ? null : reader.GetInt32(reader.GetOrdinal("MaDonVi")),
                    MaSanPham = reader.IsDBNull(reader.GetOrdinal("MaSanPham")) ? null : reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    PhanTramLoiNhuan = reader.GetDecimal(reader.GetOrdinal("PhanTramLoiNhuan")),
                    UuTien = reader.GetInt32(reader.GetOrdinal("UuTien")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                    NgayTao = reader.IsDBNull(reader.GetOrdinal("NgayTao")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayTao")),
                    NgayCapNhat = reader.IsDBNull(reader.GetOrdinal("NgayCapNhat")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayCapNhat")),
                    MaNhanVien = reader.IsDBNull(reader.GetOrdinal("MaNhanVien")) ? null : reader.GetInt32(reader.GetOrdinal("MaNhanVien"))
                };
            }

            return null;
        }

        private static void AddParameters(SqlCommand command, QuyTacLoiNhuanDTO quyTac, bool includeKey)
        {
            command.Parameters.Clear();

            if (includeKey)
            {
                command.Parameters.Add(new SqlParameter("@MaQuyTac", SqlDbType.Int)
                {
                    Value = quyTac.MaQuyTac
                });
            }

            command.Parameters.Add(new SqlParameter("@LoaiQuyTac", SqlDbType.NVarChar, 50)
            {
                Value = quyTac.LoaiQuyTac
            });

            command.Parameters.Add(new SqlParameter("@MaLoai", SqlDbType.Int)
            {
                Value = quyTac.MaLoai ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@MaThuongHieu", SqlDbType.Int)
            {
                Value = quyTac.MaThuongHieu ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@MaDonVi", SqlDbType.Int)
            {
                Value = quyTac.MaDonVi ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int)
            {
                Value = quyTac.MaSanPham ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@PhanTramLoiNhuan", SqlDbType.Decimal)
            {
                Precision = 5,
                Scale = 2,
                Value = quyTac.PhanTramLoiNhuan
            });

            command.Parameters.Add(new SqlParameter("@UuTien", SqlDbType.Int)
            {
                Value = quyTac.UuTien
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
            {
                Value = quyTac.TrangThai ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.Int)
            {
                Value = quyTac.MaNhanVien ?? (object)DBNull.Value
            });
        }
    }
}

