using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class PhieuNhap_DAO
    {
        public IList<PhieuNhapDTO> GetPhieuNhap()
        {
            var phieuNhapList = new List<PhieuNhapDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaPhieuNhap, TongTien, NgayNhap, MaNhaCungCap
                                     FROM dbo.Tbl_PhieuNhap
                                     ORDER BY MaPhieuNhap";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                phieuNhapList.Add(new PhieuNhapDTO
                {
                    MaPhieuNhap = reader.GetInt32(reader.GetOrdinal("MaPhieuNhap")),
                    TongTien = reader.GetDecimal(reader.GetOrdinal("TongTien")),
                    NgayNhap = reader.GetDateTime(reader.GetOrdinal("NgayNhap")),
                    MaNhaCungCap = reader.GetInt32(reader.GetOrdinal("MaNhaCungCap"))
                });
            }

            return phieuNhapList;
        }

        public int InsertPhieuNhap(PhieuNhapDTO phieuNhap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_PhieuNhap (TongTien, NgayNhap, MaNhaCungCap)
                                     VALUES (@TongTien, @NgayNhap, @MaNhaCungCap);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

            AddPhieuNhapParameters(command, phieuNhap, includeKey: false);

            connection.Open();
            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Không thể tạo phiếu nhập mới.");
            }

            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void UpdatePhieuNhap(PhieuNhapDTO phieuNhap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_PhieuNhap
                                     SET TongTien = @TongTien,
                                         NgayNhap = @NgayNhap,
                                         MaNhaCungCap = @MaNhaCungCap
                                     WHERE MaPhieuNhap = @MaPhieuNhap";

            AddPhieuNhapParameters(command, phieuNhap, includeKey: true);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy phiếu nhập để cập nhật.");
            }
        }

        public void DeletePhieuNhap(int maPhieuNhap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM dbo.Tbl_PhieuNhap WHERE MaPhieuNhap = @MaPhieuNhap";
            command.Parameters.Add(new SqlParameter("@MaPhieuNhap", SqlDbType.Int)
            {
                Value = maPhieuNhap
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy phiếu nhập để xóa.");
            }
        }

        // public IList<PhieuNhapDTO> SearchPhieuNhap(string keyword)
        // {
        //     var list = new List<PhieuNhapDTO>();

        //     using (SqlConnection conn = new SqlConnection(connectionString))
        //     {
        //         conn.Open();

        //         // Câu truy vấn tìm kiếm: tìm theo mã phiếu nhập, nhà cung cấp hoặc ngày nhập (dạng chuỗi)
        //         string query = @"
        //             SELECT MaPhieuNhap, NgayNhap, NhaCungCap, TongTien
        //             FROM PhieuNhap
        //             WHERE 
        //                 CAST(MaPhieuNhap AS NVARCHAR) LIKE @keyword
        //                 OR NhaCungCap LIKE @keyword
        //                 OR CONVERT(NVARCHAR, NgayNhap, 23) LIKE @keyword
        //         ";

        //         using (SqlCommand cmd = new SqlCommand(query, conn))
        //         {
        //             // Thêm % để tìm kiếm tương tự LIKE %keyword%
        //             cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

        //             using (SqlDataReader reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     var phieuNhap = new PhieuNhapDTO
        //                     {
        //                         MaPhieuNhap = reader.GetInt32(0),
        //                         NgayNhap = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1),
        //                         MaNhaCungCap = reader.GetInt32(0),
        //                         TongTien = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3)
        //                     };

        //                     list.Add(phieuNhap);
        //                 }
        //             }
        //         }
        //     }

        //     return list;
        // }

        private static void AddPhieuNhapParameters(SqlCommand command, PhieuNhapDTO phieuNhap, bool includeKey)
        {
            command.Parameters.Clear();

            if (includeKey)
            {
                command.Parameters.Add(new SqlParameter("@MaPhieuNhap", SqlDbType.Int)
                {
                    Value = phieuNhap.MaPhieuNhap
                });
            }

            command.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal)
            {
                Value = phieuNhap.TongTien
            });

            command.Parameters.Add(new SqlParameter("@NgayNhap", SqlDbType.DateTime)
            {
                Value = phieuNhap.NgayNhap
            });

            if (includeKey)
            {
                command.Parameters.Add(new SqlParameter("@MaNhaCungCap", SqlDbType.Int)
                {
                    Value = phieuNhap.MaNhaCungCap
                });
            }
        }
    }
}
