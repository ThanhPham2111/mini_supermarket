using System;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class KhoHang_DAO
    {
        /// <summary>
        /// Cập nhật số lượng sản phẩm trong kho
        /// Nếu sản phẩm chưa có trong kho thì INSERT, nếu có rồi thì UPDATE
        /// </summary>
        public void UpdateSoLuong(int maSanPham, int soLuongThem)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            
            // Kiểm tra sản phẩm đã tồn tại trong kho chưa
            command.CommandText = @"SELECT SoLuong FROM dbo.Tbl_KhoHang WHERE MaSanPham = @MaSanPham";
            command.Parameters.Add(new SqlParameter("@MaSanPham", maSanPham));
            
            connection.Open();
            var result = command.ExecuteScalar();
            
            if (result == null || result == DBNull.Value)
            {
                // Chưa có trong kho -> INSERT
                command.CommandText = @"INSERT INTO dbo.Tbl_KhoHang (MaSanPham, SoLuong, TrangThai)
                                       VALUES (@MaSanPham, @SoLuong, N'Còn hàng')";
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@MaSanPham", maSanPham));
                command.Parameters.Add(new SqlParameter("@SoLuong", soLuongThem));
                command.ExecuteNonQuery();
            }
            else
            {
                // Đã có trong kho -> UPDATE (cộng thêm số lượng)
                int soLuongHienTai = Convert.ToInt32(result);
                int soLuongMoi = soLuongHienTai + soLuongThem;
                
                command.CommandText = @"UPDATE dbo.Tbl_KhoHang 
                                       SET SoLuong = @SoLuong,
                                           TrangThai = CASE 
                                               WHEN @SoLuong > 0 THEN N'Còn hàng' 
                                               ELSE N'Hết hàng' 
                                           END
                                       WHERE MaSanPham = @MaSanPham";
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@MaSanPham", maSanPham));
                command.Parameters.Add(new SqlParameter("@SoLuong", soLuongMoi));
                command.ExecuteNonQuery();
            }
        }
        
        /// <summary>
        /// Lấy thông tin kho hàng theo mã sản phẩm
        /// </summary>
        public KhoHangDTO? GetByMaSanPham(int maSanPham)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            
            command.CommandText = @"SELECT MaSanPham, SoLuong, TrangThai 
                                   FROM dbo.Tbl_KhoHang 
                                   WHERE MaSanPham = @MaSanPham";
            command.Parameters.Add(new SqlParameter("@MaSanPham", maSanPham));
            
            connection.Open();
            using var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return new KhoHangDTO
                {
                    MaSanPham = reader.GetInt32(0),
                    SoLuong = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                    TrangThai = reader.IsDBNull(2) ? null : reader.GetString(2)
                };
            }
            
            return null;
        }
    }
}
