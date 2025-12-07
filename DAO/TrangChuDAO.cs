using System;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using System.Collections.Generic;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class TrangChuDAO
    {
        public object? GetDoanhThuHomNay()
        {
            const string sql = "SELECT SUM(TongTien) FROM Tbl_HoaDon WHERE CONVERT(date, NgayLap) = CONVERT(date, GETDATE())";
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60;
            conn.Open();
            return cmd.ExecuteScalar();
        }

        public int GetSoHoaDonHomNay()
        {
            const string sql = "SELECT COUNT(MaHoaDon) FROM Tbl_HoaDon WHERE CONVERT(date, NgayLap) = CONVERT(date, GETDATE())";
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; 
            conn.Open();
            var result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        public int GetSoLuongSanPhamHetHang()
        {
            const string sql = "SELECT COUNT(MaSanPham) FROM Tbl_KhoHang WHERE SoLuong = 0";
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; 
            conn.Open();
            var result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        public IList<DoanhThuNgayDTO> GetDoanhThu7Ngay()
        {
            const string sql = @"SELECT CONVERT(date, NgayLap) AS Ngay, SUM(TongTien) AS TongDoanhThu
                                  FROM Tbl_HoaDon
                                  WHERE NgayLap >= DATEADD(day, -7, GETDATE())
                                  GROUP BY CONVERT(date, NgayLap)
                                  ORDER BY Ngay ASC";
            var list = new List<DoanhThuNgayDTO>();
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; 
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new DoanhThuNgayDTO
                {
                    Ngay = reader.GetDateTime(reader.GetOrdinal("Ngay")),
                    TongDoanhThu = reader.IsDBNull(reader.GetOrdinal("TongDoanhThu")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TongDoanhThu"))
                };
                list.Add(item);
            }
            return list;
        }

        public IList<TopBanChayDTO> GetTop5BanChay()
        {
            const string sql = @"SELECT TOP 5 sp.TenSanPham, SUM(ct.SoLuong) AS TongSoLuong
                                  FROM Tbl_ChiTietHoaDon ct
                                  JOIN Tbl_SanPham sp ON ct.MaSanPham = sp.MaSanPham
                                  JOIN Tbl_HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon
                                  WHERE hd.NgayLap >= DATEADD(day, -30, GETDATE())
                                  GROUP BY sp.TenSanPham
                                  ORDER BY TongSoLuong DESC";
            var list = new List<TopBanChayDTO>();
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; 
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new TopBanChayDTO
                {
                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                    TongSoLuong = reader.GetInt32(reader.GetOrdinal("TongSoLuong"))
                };
                list.Add(item);
            }
            return list;
        }

        public IList<SanPhamHetHanDTO> GetSanPhamSapHetHan()
        {
            const string sql = @"SELECT TenSanPham, HSD
                                  FROM Tbl_SanPham
                                  WHERE HSD BETWEEN GETDATE() AND DATEADD(day, 7, GETDATE())
                                  ORDER BY HSD ASC";
            var list = new List<SanPhamHetHanDTO>();
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; 
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new SanPhamHetHanDTO
                {
                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                    HSD = reader.IsDBNull(reader.GetOrdinal("HSD")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("HSD"))
                };
                list.Add(item);
            }
            return list;
        }

        public IList<KhachHangMuaNhieuDTO> GetKhachHangMuaNhieuNhat()
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
                var list = new List<KhachHangMuaNhieuDTO>();
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 60; 
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new KhachHangMuaNhieuDTO
                    {
                        TenKhachHang = reader.GetString(reader.GetOrdinal("TenKhachHang")),
                        TongSoLuong = reader.GetInt32(reader.GetOrdinal("TongSoLuong"))
                    };
                    list.Add(item);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách khách hàng mua nhiều nhất: " + ex.Message);
                throw;
            }
        }

        public IList<SanPhamHetHanDTO> GetSanPhamDaHetHan()
        {
            const string sql = @"SELECT TenSanPham, HSD
                                  FROM Tbl_SanPham
                                  WHERE HSD < GETDATE()
                                  ORDER BY HSD ASC";
            var list = new List<SanPhamHetHanDTO>();
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 60; 
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new SanPhamHetHanDTO
                {
                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                    HSD = reader.IsDBNull(reader.GetOrdinal("HSD")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("HSD"))
                };
                list.Add(item);
            }
            return list;
        }
    }
}
