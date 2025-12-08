using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class LichSuThayDoiKho_DAO
    {
       public IList<LichSuThayDoiKhoDTO> GetByProduct(int maSanPham, int top = 50)
        {
            var list = new List<LichSuThayDoiKhoDTO>();
            using var conn = DbConnectionFactory.CreateConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT TOP(@Top) ls.MaLichSu, ls.MaSanPham, ISNULL(sp.TenSanPham, 'Không xác định') AS TenSanPham, ls.SoLuongCu, ls.SoLuongMoi, ls.ChenhLech, ls.LoaiThayDoi, ls.LyDo, ls.GhiChu, ls.MaNhanVien, nv.TenNhanVien, ls.NgayThayDoi
                                FROM Tbl_LichSuThayDoiKho ls
                                LEFT JOIN Tbl_SanPham sp ON sp.MaSanPham = ls.MaSanPham
                                LEFT JOIN Tbl_NhanVien nv ON nv.MaNhanVien = ls.MaNhanVien
                                WHERE ls.MaSanPham = @MaSP
                                ORDER BY ls.NgayThayDoi DESC";
            cmd.Parameters.Add(new SqlParameter("@Top", top));
            cmd.Parameters.Add(new SqlParameter("@MaSP", maSanPham));
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var dto = new LichSuThayDoiKhoDTO
                {
                    MaLichSu = reader.GetInt32(0),
                    MaSanPham = reader.GetInt32(1),
                    TenSanPham = reader.GetString(2),
                    SoLuongCu = reader.GetInt32(3),
                    SoLuongMoi = reader.GetInt32(4),
                    ChenhLech = reader.GetInt32(5),
                    LoaiThayDoi = reader.GetString(6),
                    LyDo = reader.IsDBNull(7) ? null : reader.GetString(7),
                    GhiChu = reader.IsDBNull(8) ? null : reader.GetString(8),
                    MaNhanVien = reader.GetInt32(9),
                    TenNhanVien = reader.GetString(10),
                    NgayThayDoi = reader.GetDateTime(11)
                };
                list.Add(dto);
            }
            return list;
        }
    }
}
