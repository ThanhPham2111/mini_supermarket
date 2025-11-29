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
            command.CommandText = @"SELECT MaPhieuNhap, TongTien, NgayNhap, MaNhaCungCap, TrangThai, LyDoHuy
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
                    MaNhaCungCap = reader.GetInt32(reader.GetOrdinal("MaNhaCungCap")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                    LyDoHuy = reader.IsDBNull(reader.GetOrdinal("LyDoHuy")) ? null : reader.GetString(reader.GetOrdinal("LyDoHuy"))
                });
            }

            return phieuNhapList;
        }

        public PhieuNhapDTO? GetPhieuNhapById(int maPhieuNhap)
        {
            PhieuNhapDTO? phieuNhap = null;

            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            // 1. Lấy thông tin phiếu nhập
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT MaPhieuNhap, TongTien, NgayNhap, MaNhaCungCap, TrangThai, LyDoHuy
                                       FROM dbo.Tbl_PhieuNhap
                                       WHERE MaPhieuNhap = @MaPhieuNhap";

                command.Parameters.Add(new SqlParameter("@MaPhieuNhap", SqlDbType.Int)
                {
                    Value = maPhieuNhap
                });

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    phieuNhap = new PhieuNhapDTO
                    {
                        MaPhieuNhap = reader.GetInt32(reader.GetOrdinal("MaPhieuNhap")),
                        TongTien = reader.GetDecimal(reader.GetOrdinal("TongTien")),
                        NgayNhap = reader.GetDateTime(reader.GetOrdinal("NgayNhap")),
                        MaNhaCungCap = reader.GetInt32(reader.GetOrdinal("MaNhaCungCap")),
                        TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                        LyDoHuy = reader.IsDBNull(reader.GetOrdinal("LyDoHuy")) ? null : reader.GetString(reader.GetOrdinal("LyDoHuy"))
                    };
                }
            }

            // 2. Nếu tìm thấy phiếu nhập, lấy chi tiết
            if (phieuNhap != null)
            {
                using var command = connection.CreateCommand();
                command.CommandText = @"SELECT MaChiTietPhieuNhap, MaSanPham, MaPhieuNhap, SoLuong, DonGiaNhap, ThanhTien
                                       FROM dbo.Tbl_ChiTietPhieuNhap
                                       WHERE MaPhieuNhap = @MaPhieuNhap";

                command.Parameters.Add(new SqlParameter("@MaPhieuNhap", SqlDbType.Int)
                {
                    Value = maPhieuNhap
                });

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var chiTiet = new ChiTietPhieuNhapDTO
                    {
                        MaChiTietPhieuNhap = reader.GetInt32(reader.GetOrdinal("MaChiTietPhieuNhap")),
                        MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                        MaPhieuNhap = reader.GetInt32(reader.GetOrdinal("MaPhieuNhap")),
                        SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                        DonGiaNhap = reader.GetDecimal(reader.GetOrdinal("DonGiaNhap")),
                        ThanhTien = reader.GetDecimal(reader.GetOrdinal("ThanhTien"))
                    };
                    phieuNhap.ChiTietPhieuNhaps.Add(chiTiet);
                }
            }

            return phieuNhap;
        }

        public int InsertPhieuNhap(PhieuNhapDTO phieuNhap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();
            
            using var transaction = connection.BeginTransaction();
            
            try
            {
                int maPhieuNhap;
                
                // 1. Insert Phiếu Nhập
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = @"INSERT INTO dbo.Tbl_PhieuNhap (TongTien, NgayNhap, MaNhaCungCap, TrangThai, LyDoHuy)
                                           VALUES (@TongTien, @NgayNhap, @MaNhaCungCap, @TrangThai, @LyDoHuy);
                                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    command.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal)
                    {
                        Value = phieuNhap.TongTien ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new SqlParameter("@NgayNhap", SqlDbType.DateTime)
                    {
                        Value = phieuNhap.NgayNhap ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new SqlParameter("@MaNhaCungCap", SqlDbType.Int)
                    {
                        Value = phieuNhap.MaNhaCungCap
                    });

                    command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
                    {
                        Value = phieuNhap.TrangThai ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new SqlParameter("@LyDoHuy", SqlDbType.NVarChar, -1)
                    {
                        Value = phieuNhap.LyDoHuy ?? (object)DBNull.Value
                    });

                    object? result = command.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        throw new InvalidOperationException("Không thể tạo phiếu nhập mới.");
                    }

                    maPhieuNhap = Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
                }

                // 2. Insert Chi Tiết Phiếu Nhập
                if (phieuNhap.ChiTietPhieuNhaps != null && phieuNhap.ChiTietPhieuNhaps.Count > 0)
                {
                    foreach (var chiTiet in phieuNhap.ChiTietPhieuNhaps)
                    {
                        using var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = @"INSERT INTO dbo.Tbl_ChiTietPhieuNhap 
                                               (MaPhieuNhap, MaSanPham, SoLuong, DonGiaNhap, ThanhTien)
                                               VALUES (@MaPhieuNhap, @MaSanPham, @SoLuong, @DonGiaNhap, @ThanhTien)";

                        command.Parameters.Add(new SqlParameter("@MaPhieuNhap", SqlDbType.Int)
                        {
                            Value = maPhieuNhap
                        });

                        command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int)
                        {
                            Value = chiTiet.MaSanPham
                        });

                        command.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int)
                        {
                            Value = chiTiet.SoLuong
                        });

                        command.Parameters.Add(new SqlParameter("@DonGiaNhap", SqlDbType.Decimal)
                        {
                            Value = chiTiet.DonGiaNhap
                        });

                        command.Parameters.Add(new SqlParameter("@ThanhTien", SqlDbType.Decimal)
                        {
                            Value = chiTiet.ThanhTien
                        });

                        command.ExecuteNonQuery();
                    }
                }

                // Commit transaction
                transaction.Commit();
                return maPhieuNhap;
            }
            catch (Exception)
            {
                // Rollback nếu có lỗi
                transaction.Rollback();
                throw;
            }
        }

        public void UpdatePhieuNhap(PhieuNhapDTO phieuNhap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_PhieuNhap
                                     SET TongTien = @TongTien,
                                         NgayNhap = @NgayNhap,
                                         MaNhaCungCap = @MaNhaCungCap,
                                         TrangThai = @TrangThai,
                                         LyDoHuy = @LyDoHuy
                                     WHERE MaPhieuNhap = @MaPhieuNhap";

            AddPhieuNhapParameters(command, phieuNhap, includeKey: true);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy phiếu nhập để cập nhật.");
            }
        }

        public void UpdateTrangThaiPhieuNhap(int maPhieuNhap, string trangThai)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_PhieuNhap
                                     SET TrangThai = @TrangThai
                                     WHERE MaPhieuNhap = @MaPhieuNhap";

            command.Parameters.Add(new SqlParameter("@MaPhieuNhap", SqlDbType.Int)
            {
                Value = maPhieuNhap
            });

            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
            {
                Value = trangThai
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy phiếu nhập để cập nhật.");
            }
        }

        public void HuyPhieuNhap(int maPhieuNhap, string lyDoHuy)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE dbo.Tbl_PhieuNhap
                                     SET TrangThai = N'Hủy',
                                         LyDoHuy = @LyDoHuy
                                     WHERE MaPhieuNhap = @MaPhieuNhap";

            command.Parameters.Add(new SqlParameter("@MaPhieuNhap", SqlDbType.Int)
            {
                Value = maPhieuNhap
            });

            command.Parameters.Add(new SqlParameter("@LyDoHuy", SqlDbType.NVarChar, -1)
            {
                Value = lyDoHuy ?? (object)DBNull.Value
            });

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Không tìm thấy phiếu nhập để hủy.");
            }
        }

        public void DeletePhieuNhap(int maPhieuNhap)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();
            
            using var transaction = connection.BeginTransaction();
            
            try
            {
                // 1. Xóa chi tiết phiếu nhập trước
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = @"DELETE FROM dbo.Tbl_ChiTietPhieuNhap WHERE MaPhieuNhap = @MaPhieuNhap";
                    command.Parameters.Add(new SqlParameter("@MaPhieuNhap", SqlDbType.Int)
                    {
                        Value = maPhieuNhap
                    });
                    command.ExecuteNonQuery();
                }
                
                // 2. Xóa phiếu nhập
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = @"DELETE FROM dbo.Tbl_PhieuNhap WHERE MaPhieuNhap = @MaPhieuNhap";
                    command.Parameters.Add(new SqlParameter("@MaPhieuNhap", SqlDbType.Int)
                    {
                        Value = maPhieuNhap
                    });
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Không tìm thấy phiếu nhập để xóa.");
                    }
                }
                
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
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

            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
            {
                Value = phieuNhap.TrangThai ?? (object)DBNull.Value
            });

            command.Parameters.Add(new SqlParameter("@LyDoHuy", SqlDbType.NVarChar, -1)
            {
                Value = phieuNhap.LyDoHuy ?? (object)DBNull.Value
            });
        }
    }
}
