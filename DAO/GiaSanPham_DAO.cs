using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class GiaSanPham_DAO
    {
        public GiaSanPhamDTO? GetGiaByMaSanPham(int maSanPham)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT g.MaGia, g.MaSanPham, g.GiaNhap, g.GiaNhapGoc, g.PhanTramLoiNhuanApDung, 
                                           g.GiaBan, g.NgayCapNhat,
                                           sp.TenSanPham, l.TenLoai, th.TenThuongHieu, dv.TenDonVi
                                    FROM dbo.Tbl_GiaSanPham g
                                    INNER JOIN dbo.Tbl_SanPham sp ON g.MaSanPham = sp.MaSanPham
                                    LEFT JOIN dbo.Tbl_Loai l ON sp.MaLoai = l.MaLoai
                                    LEFT JOIN dbo.Tbl_ThuongHieu th ON sp.MaThuongHieu = th.MaThuongHieu
                                    LEFT JOIN dbo.Tbl_DonVi dv ON sp.MaDonVi = dv.MaDonVi
                                    WHERE g.MaSanPham = @MaSanPham";

            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int)
            {
                Value = maSanPham
            });

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new GiaSanPhamDTO
                {
                    MaGia = reader.GetInt32(reader.GetOrdinal("MaGia")),
                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    GiaNhap = reader.GetDecimal(reader.GetOrdinal("GiaNhap")),
                    GiaNhapGoc = reader.GetDecimal(reader.GetOrdinal("GiaNhapGoc")),
                    PhanTramLoiNhuanApDung = reader.GetDecimal(reader.GetOrdinal("PhanTramLoiNhuanApDung")),
                    GiaBan = reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                    NgayCapNhat = reader.IsDBNull(reader.GetOrdinal("NgayCapNhat")) 
                        ? null : reader.GetDateTime(reader.GetOrdinal("NgayCapNhat")),
                    TenSanPham = reader.IsDBNull(reader.GetOrdinal("TenSanPham")) 
                        ? null : reader.GetString(reader.GetOrdinal("TenSanPham")),
                    TenLoai = reader.IsDBNull(reader.GetOrdinal("TenLoai")) 
                        ? null : reader.GetString(reader.GetOrdinal("TenLoai")),
                    TenThuongHieu = reader.IsDBNull(reader.GetOrdinal("TenThuongHieu")) 
                        ? null : reader.GetString(reader.GetOrdinal("TenThuongHieu")),
                    TenDonVi = reader.IsDBNull(reader.GetOrdinal("TenDonVi")) 
                        ? null : reader.GetString(reader.GetOrdinal("TenDonVi"))
                };
            }

            return null;
        }

        public IList<GiaSanPhamDTO> GetAllGiaSanPham()
        {
            var list = new List<GiaSanPhamDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT g.MaGia, g.MaSanPham, g.GiaNhap, g.GiaNhapGoc, g.PhanTramLoiNhuanApDung, 
                                           g.GiaBan, g.NgayCapNhat,
                                           sp.TenSanPham, l.TenLoai, th.TenThuongHieu, dv.TenDonVi
                                    FROM dbo.Tbl_GiaSanPham g
                                    INNER JOIN dbo.Tbl_SanPham sp ON g.MaSanPham = sp.MaSanPham
                                    LEFT JOIN dbo.Tbl_Loai l ON sp.MaLoai = l.MaLoai
                                    LEFT JOIN dbo.Tbl_ThuongHieu th ON sp.MaThuongHieu = th.MaThuongHieu
                                    LEFT JOIN dbo.Tbl_DonVi dv ON sp.MaDonVi = dv.MaDonVi
                                    ORDER BY sp.TenSanPham";

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new GiaSanPhamDTO
                {
                    MaGia = reader.GetInt32(reader.GetOrdinal("MaGia")),
                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    GiaNhap = reader.GetDecimal(reader.GetOrdinal("GiaNhap")),
                    GiaNhapGoc = reader.GetDecimal(reader.GetOrdinal("GiaNhapGoc")),
                    PhanTramLoiNhuanApDung = reader.GetDecimal(reader.GetOrdinal("PhanTramLoiNhuanApDung")),
                    GiaBan = reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                    NgayCapNhat = reader.IsDBNull(reader.GetOrdinal("NgayCapNhat")) 
                        ? null : reader.GetDateTime(reader.GetOrdinal("NgayCapNhat")),
                    TenSanPham = reader.IsDBNull(reader.GetOrdinal("TenSanPham")) 
                        ? null : reader.GetString(reader.GetOrdinal("TenSanPham")),
                    TenLoai = reader.IsDBNull(reader.GetOrdinal("TenLoai")) 
                        ? null : reader.GetString(reader.GetOrdinal("TenLoai")),
                    TenThuongHieu = reader.IsDBNull(reader.GetOrdinal("TenThuongHieu")) 
                        ? null : reader.GetString(reader.GetOrdinal("TenThuongHieu")),
                    TenDonVi = reader.IsDBNull(reader.GetOrdinal("TenDonVi")) 
                        ? null : reader.GetString(reader.GetOrdinal("TenDonVi"))
                });
            }

            return list;
        }

        public void InsertOrUpdateGiaSanPham(GiaSanPhamDTO giaSanPham)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"IF EXISTS (SELECT 1 FROM dbo.Tbl_GiaSanPham WHERE MaSanPham = @MaSanPham)
                                   BEGIN
                                       UPDATE dbo.Tbl_GiaSanPham
                                       SET GiaNhap = @GiaNhap,
                                           GiaNhapGoc = @GiaNhapGoc,
                                           PhanTramLoiNhuanApDung = @PhanTramLoiNhuanApDung,
                                           GiaBan = @GiaBan,
                                           NgayCapNhat = GETDATE()
                                       WHERE MaSanPham = @MaSanPham
                                   END
                                   ELSE
                                   BEGIN
                                       INSERT INTO dbo.Tbl_GiaSanPham 
                                       (MaSanPham, GiaNhap, GiaNhapGoc, PhanTramLoiNhuanApDung, GiaBan)
                                       VALUES (@MaSanPham, @GiaNhap, @GiaNhapGoc, @PhanTramLoiNhuanApDung, @GiaBan)
                                   END";

            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int)
            {
                Value = giaSanPham.MaSanPham
            });

            command.Parameters.Add(new SqlParameter("@GiaNhap", SqlDbType.Decimal)
            {
                Precision = 18,
                Scale = 2,
                Value = giaSanPham.GiaNhap
            });

            command.Parameters.Add(new SqlParameter("@GiaNhapGoc", SqlDbType.Decimal)
            {
                Precision = 18,
                Scale = 2,
                Value = giaSanPham.GiaNhapGoc
            });

            command.Parameters.Add(new SqlParameter("@PhanTramLoiNhuanApDung", SqlDbType.Decimal)
            {
                Precision = 5,
                Scale = 2,
                Value = giaSanPham.PhanTramLoiNhuanApDung
            });

            command.Parameters.Add(new SqlParameter("@GiaBan", SqlDbType.Decimal)
            {
                Precision = 18,
                Scale = 2,
                Value = giaSanPham.GiaBan
            });

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void CapNhatGiaNhap(int maSanPham, decimal giaNhapMoi)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_GiaSanPham
                                   SET GiaNhap = @GiaNhapMoi,
                                       -- Logic: Nếu giá nhập mới >= giá nhập gốc thì cập nhật giá nhập gốc và tính lại giá bán
                                       GiaNhapGoc = CASE 
                                           WHEN @GiaNhapMoi >= GiaNhapGoc THEN @GiaNhapMoi
                                           ELSE GiaNhapGoc
                                       END,
                                       GiaBan = CASE 
                                           WHEN @GiaNhapMoi >= GiaNhapGoc THEN @GiaNhapMoi * (1 + PhanTramLoiNhuanApDung / 100)
                                           ELSE GiaBan
                                       END,
                                       NgayCapNhat = GETDATE()
                                   WHERE MaSanPham = @MaSanPham";

            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int)
            {
                Value = maSanPham
            });

            command.Parameters.Add(new SqlParameter("@GiaNhapMoi", SqlDbType.Decimal)
            {
                Precision = 18,
                Scale = 2,
                Value = giaNhapMoi
            });

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}

