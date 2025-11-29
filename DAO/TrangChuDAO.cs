using System;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;

namespace mini_supermarket.DAO
{
    public class TrangChuDAO
    {
        public object? GetDoanhThuHomNay()
        {
            const string sql = "SELECT SUM(TongTien) FROM Tbl_HoaDon WHERE CONVERT(date, NgayLap) = CONVERT(date, GETDATE())";
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; // T?ng th?i gian ch? lên 60 giây
            conn.Open();
            return cmd.ExecuteScalar();
        }

        public int GetSoHoaDonHomNay()
        {
            const string sql = "SELECT COUNT(MaHoaDon) FROM Tbl_HoaDon WHERE CONVERT(date, NgayLap) = CONVERT(date, GETDATE())";
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; // T?ng th?i gian ch? lên 60 giây
            conn.Open();
            var result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        public int GetSoLuongSanPhamHetHang()
        {
            const string sql = "SELECT COUNT(MaSanPham) FROM Tbl_KhoHang WHERE SoLuong = 0";
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; // T?ng th?i gian ch? lên 60 giây
            conn.Open();
            var result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        public DataTable GetDoanhThu7Ngay()
        {
            const string sql = @"SELECT CONVERT(date, NgayLap) AS Ngay, SUM(TongTien) AS TongDoanhThu
                                  FROM Tbl_HoaDon
                                  WHERE NgayLap >= DATEADD(day, -7, GETDATE())
                                  GROUP BY CONVERT(date, NgayLap)
                                  ORDER BY Ngay ASC";
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; // T?ng th?i gian ch? lên 60 giây
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            return dt;
        }

        public DataTable GetTop5BanChay()
        {
            const string sql = @"SELECT TOP 5 sp.TenSanPham, SUM(ct.SoLuong) AS TongSoLuong
                                  FROM Tbl_ChiTietHoaDon ct
                                  JOIN Tbl_SanPham sp ON ct.MaSanPham = sp.MaSanPham
                                  JOIN Tbl_HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon
                                  WHERE hd.NgayLap >= DATEADD(day, -30, GETDATE())
                                  GROUP BY sp.TenSanPham
                                  ORDER BY TongSoLuong DESC";
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; // T?ng th?i gian ch? lên 60 giây
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            return dt;
        }

        public DataTable GetSanPhamSapHetHan()
        {
            const string sql = @"SELECT TenSanPham, HSD
                                  FROM Tbl_SanPham
                                  WHERE HSD BETWEEN GETDATE() AND DATEADD(day, 7, GETDATE())
                                  ORDER BY HSD ASC";
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; // T?ng th?i gian ch? lên 60 giây
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            return dt;
        }

        public DataTable GetKhachHangMuaNhieuNhat()
        {
            try
            {
                const string sql = @"SELECT kh.TenKhachHang, SUM(ct.SoLuong) AS TongSoLuong
                                      FROM Tbl_ChiTietHoaDon ct
                                      JOIN Tbl_HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon
                                      JOIN Tbl_KhachHang kh ON hd.MaKhachHang = kh.MaKhachHang
                                      WHERE hd.NgayLap >= DATEADD(day, -30, GETDATE())
                                      GROUP BY kh.TenKhachHang
                                      ORDER BY TongSoLuong DESC";
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 60; // T?ng th?i gian ch? lên 60 giây
                using var adapter = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("L?i khi l?y danh sách khách hàng mua nhi?u nh?t: " + ex.Message);
                throw;
            }
        }
    }
}
