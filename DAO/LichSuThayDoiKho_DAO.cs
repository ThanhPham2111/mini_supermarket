using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    /// <summary>
    /// DAO chuyên x? lý truy v?n b?ng Tbl_LichSuThayDoiKho.
    /// Ch? ch?a truy v?n, không ki?m tra nghi?p v?.
    /// </summary>
    public class LichSuThayDoiKho_DAO
    {
        public int Insert(LichSuThayDoiKhoDTO log)
        {
            using var conn = DbConnectionFactory.CreateConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Tbl_LichSuThayDoiKho (MaSanPham, SoLuongCu, SoLuongMoi, ChenhLech, LoaiThayDoi, LyDo, GhiChu, MaNhanVien, NgayThayDoi)\n                                 VALUES (@MaSanPham, @SoLuongCu, @SoLuongMoi, @ChenhLech, @LoaiThayDoi, @LyDo, @GhiChu, @MaNhanVien, @NgayThayDoi);\n                                 SELECT CAST(SCOPE_IDENTITY() AS INT);";
            AddParams(cmd, log);
            var result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        /// <summary>
        /// Dùng chung trong transaction ngoài (KhoHang c?p nh?t + ghi log).
        /// </summary>
        public int InsertWithConnection(SqlConnection conn, SqlTransaction tran, LichSuThayDoiKhoDTO log)
        {
            using var cmd = conn.CreateCommand();
            cmd.Transaction = tran;
            cmd.CommandText = @"INSERT INTO Tbl_LichSuThayDoiKho (MaSanPham, SoLuongCu, SoLuongMoi, ChenhLech, LoaiThayDoi, LyDo, GhiChu, MaNhanVien, NgayThayDoi)\n                                 VALUES (@MaSanPham, @SoLuongCu, @SoLuongMoi, @ChenhLech, @LoaiThayDoi, @LyDo, @GhiChu, @MaNhanVien, @NgayThayDoi);";
            AddParams(cmd, log);
            return cmd.ExecuteNonQuery();
        }

        public DataTable GetByProduct(int maSanPham, int top = 50)
        {
            var dt = new DataTable();
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT TOP(@Top) ls.MaLichSu, ls.MaSanPham, sp.TenSanPham, ls.SoLuongCu, ls.SoLuongMoi, ls.ChenhLech, ls.LoaiThayDoi, ls.LyDo, ls.GhiChu, ls.MaNhanVien, nv.TenNhanVien, ls.NgayThayDoi\n                                FROM Tbl_LichSuThayDoiKho ls\n                                JOIN Tbl_SanPham sp ON sp.MaSanPham = ls.MaSanPham\n                                JOIN Tbl_NhanVien nv ON nv.MaNhanVien = ls.MaNhanVien\n                                WHERE ls.MaSanPham = @MaSP\n                                ORDER BY ls.NgayThayDoi DESC";
            cmd.Parameters.Add(new SqlParameter("@Top", top));
            cmd.Parameters.Add(new SqlParameter("@MaSP", maSanPham));
            conn.Open();
            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable GetRecent(int top = 20)
        {
            var dt = new DataTable();
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT TOP(@Top) ls.MaLichSu, ls.MaSanPham, sp.TenSanPham, ls.SoLuongCu, ls.SoLuongMoi, ls.ChenhLech, ls.LoaiThayDoi, ls.LyDo, ls.GhiChu, ls.MaNhanVien, nv.TenNhanVien, ls.NgayThayDoi\n                                FROM Tbl_LichSuThayDoiKho ls\n                                JOIN Tbl_SanPham sp ON sp.MaSanPham = ls.MaSanPham\n                                JOIN Tbl_NhanVien nv ON nv.MaNhanVien = ls.MaNhanVien\n                                ORDER BY ls.NgayThayDoi DESC";
            cmd.Parameters.Add(new SqlParameter("@Top", top));
            conn.Open();
            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable GetByFilter(DateTime? from, DateTime? to, string? loaiThayDoi, int? maNhanVien, int? maSanPham)
        {
            var dt = new DataTable();
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = conn.CreateCommand();
            var sql = @"SELECT ls.MaLichSu, ls.MaSanPham, sp.TenSanPham, ls.SoLuongCu, ls.SoLuongMoi, ls.ChenhLech, ls.LoaiThayDoi, ls.LyDo, ls.GhiChu, ls.MaNhanVien, nv.TenNhanVien, ls.NgayThayDoi\n                        FROM Tbl_LichSuThayDoiKho ls\n                        JOIN Tbl_SanPham sp ON sp.MaSanPham = ls.MaSanPham\n                        JOIN Tbl_NhanVien nv ON nv.MaNhanVien = ls.MaNhanVien\n                        WHERE 1=1";
            if (from.HasValue)
            {
                sql += " AND ls.NgayThayDoi >= @From";
                cmd.Parameters.Add(new SqlParameter("@From", from.Value));
            }
            if (to.HasValue)
            {
                sql += " AND ls.NgayThayDoi < @To"; // exclusive end
                cmd.Parameters.Add(new SqlParameter("@To", to.Value));
            }
            if (!string.IsNullOrWhiteSpace(loaiThayDoi))
            {
                sql += " AND ls.LoaiThayDoi = @Loai";
                cmd.Parameters.Add(new SqlParameter("@Loai", loaiThayDoi));
            }
            if (maNhanVien.HasValue)
            {
                sql += " AND ls.MaNhanVien = @MaNV";
                cmd.Parameters.Add(new SqlParameter("@MaNV", maNhanVien.Value));
            }
            if (maSanPham.HasValue)
            {
                sql += " AND ls.MaSanPham = @MaSP";
                cmd.Parameters.Add(new SqlParameter("@MaSP", maSanPham.Value));
            }
            sql += " ORDER BY ls.NgayThayDoi DESC";
            cmd.CommandText = sql;
            conn.Open();
            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        private static void AddParams(SqlCommand cmd, LichSuThayDoiKhoDTO log)
        {
            cmd.Parameters.AddWithValue("@MaSanPham", log.MaSanPham);
            cmd.Parameters.AddWithValue("@SoLuongCu", log.SoLuongCu);
            cmd.Parameters.AddWithValue("@SoLuongMoi", log.SoLuongMoi);
            cmd.Parameters.AddWithValue("@ChenhLech", log.ChenhLech);
            cmd.Parameters.AddWithValue("@LoaiThayDoi", log.LoaiThayDoi);
            cmd.Parameters.AddWithValue("@LyDo", (object?)log.LyDo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GhiChu", (object?)log.GhiChu ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNhanVien", log.MaNhanVien);
            cmd.Parameters.AddWithValue("@NgayThayDoi", log.NgayThayDoi);
        }
    }
}
