using mini_supermarket.DB;
using mini_supermarket.DTO;
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace mini_supermarket.DAO
{
    public class KhoHangDAO
    {
        public DataTable LayDanhSachTonKho()
        {
            string query = @"
                SELECT 
                    kh.MaSanPham AS MaSP,
                    sp.TenSanPham,
                    dv.TenDonVi,
                    l.TenLoai,
                    th.TenThuongHieu,
                    kh.SoLuong,
                    kh.TrangThai,
                    sp.MaLoai,
                    sp.MaThuongHieu
                FROM Tbl_KhoHang kh
                JOIN Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham
                LEFT JOIN Tbl_DonVi dv ON sp.MaDonVi = dv.MaDonVi
                LEFT JOIN Tbl_Loai l ON sp.MaLoai = l.MaLoai
                LEFT JOIN Tbl_ThuongHieu th ON sp.MaThuongHieu = th.MaThuongHieu;";

            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách tồn kho: " + ex.Message);
            }
            return dataTable;
        }

        public DataTable LayDanhSachLoai()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT MaLoai, TenLoai FROM Tbl_Loai;";
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách loại: " + ex.Message);
            }
            return dataTable;
        }

        public DataTable LayDanhSachThuongHieu()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT MaThuongHieu, TenThuongHieu FROM Tbl_ThuongHieu;";
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách thương hiệu: " + ex.Message);
            }
            return dataTable;
        }

        public DataTable LayDanhSachSanPhamBanHang()
        {
            string query = @"
                SELECT 
                    sp.MaSanPham,
                    sp.TenSanPham,
                    sp.GiaBan,
                    kh.SoLuong,
                    ISNULL(km.TenKhuyenMai, N'') AS KhuyenMai,
                    ISNULL(km.PhanTramGiamGia, 0) AS PhanTramGiam
                FROM Tbl_SanPham sp
                INNER JOIN Tbl_KhoHang kh ON sp.MaSanPham = kh.MaSanPham
                LEFT JOIN Tbl_KhuyenMai km ON sp.MaSanPham = km.MaSanPham 
                    AND GETDATE() BETWEEN km.NgayBatDau AND km.NgayKetThuc
                WHERE sp.TrangThai = N'Còn hàng' 
                    AND kh.SoLuong > 0
                ORDER BY sp.TenSanPham;";

            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách sản phẩm bán hàng: " + ex.Message);
            }
            return dataTable;
        }

        public bool ExistsByMaSanPham(int maSanPham)
        {
            const string query = "SELECT COUNT(1) FROM Tbl_KhoHang WHERE MaSanPham = @MaSanPham";

            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    return result != null && Convert.ToInt32(result) > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] ExistsByMaSanPham: {ex.Message}");
                return false;
            }
        }

        public KhoHangDTO? GetByMaSanPham(int maSanPham)
        {
            const string query = @"SELECT MaSanPham, SoLuong, TrangThai FROM Tbl_KhoHang WHERE MaSanPham = @MaSanPham";

            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new KhoHangDTO
                            {
                                MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetByMaSanPham: {ex.Message}");
            }

            return null;
        }

        public void UpdateKhoHang(KhoHangDTO khoHang)
        {
            const string query = @"UPDATE Tbl_KhoHang SET SoLuong = @SoLuong, TrangThai = @TrangThai WHERE MaSanPham = @MaSanPham";

            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                    command.Parameters.AddWithValue("@SoLuong", khoHang.SoLuong ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TrangThai", khoHang.TrangThai ?? (object)DBNull.Value);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UpdateKhoHang: {ex.Message}");
            }
        }

        public void InsertKhoHang(KhoHangDTO khoHang)
        {
            const string query = @"INSERT INTO Tbl_KhoHang (MaSanPham, SoLuong, TrangThai) VALUES (@MaSanPham, @SoLuong, @TrangThai)";

            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                    command.Parameters.AddWithValue("@SoLuong", khoHang.SoLuong ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TrangThai", khoHang.TrangThai ?? (object)DBNull.Value);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] InsertKhoHang: {ex.Message}");
            }
        }

        // PHƯƠNG ÁN 2: Cập nhật kho + Ghi log lịch sử
        public bool CapNhatKhoVaGhiLog(KhoHangDTO khoHang, LichSuThayDoiKhoDTO lichSu)
        {
            string queryUpdateKho = @"UPDATE Tbl_KhoHang 
                                      SET SoLuong = @SoLuongMoi, 
                                          TrangThai = @TrangThai
                                      WHERE MaSanPham = @MaSanPham";

            string queryInsertLog = @"INSERT INTO Tbl_LichSuThayDoiKho 
                                      (MaSanPham, SoLuongCu, SoLuongMoi, ChenhLech, LoaiThayDoi, LyDo, GhiChu, MaNhanVien, NgayThayDoi)
                                      VALUES (@MaSanPham, @SoLuongCu, @SoLuongMoi, @ChenhLech, @LoaiThayDoi, @LyDo, @GhiChu, @MaNhanVien, @NgayThayDoi)";

            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Cập nhật số lượng kho
                            using (SqlCommand cmdUpdate = new SqlCommand(queryUpdateKho, connection, transaction))
                            {
                                cmdUpdate.Parameters.AddWithValue("@SoLuongMoi", khoHang.SoLuong ?? (object)DBNull.Value);
                                cmdUpdate.Parameters.AddWithValue("@TrangThai", khoHang.TrangThai ?? (object)DBNull.Value);
                                cmdUpdate.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                                cmdUpdate.ExecuteNonQuery();
                            }

                            // 2. Ghi log lịch sử
                            using (SqlCommand cmdInsert = new SqlCommand(queryInsertLog, connection, transaction))
                            {
                                cmdInsert.Parameters.AddWithValue("@MaSanPham", lichSu.MaSanPham);
                                cmdInsert.Parameters.AddWithValue("@SoLuongCu", lichSu.SoLuongCu);
                                cmdInsert.Parameters.AddWithValue("@SoLuongMoi", lichSu.SoLuongMoi);
                                cmdInsert.Parameters.AddWithValue("@ChenhLech", lichSu.ChenhLech);
                                cmdInsert.Parameters.AddWithValue("@LoaiThayDoi", lichSu.LoaiThayDoi);
                                cmdInsert.Parameters.AddWithValue("@LyDo", lichSu.LyDo ?? (object)DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@GhiChu", lichSu.GhiChu ?? (object)DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@MaNhanVien", lichSu.MaNhanVien);
                                cmdInsert.Parameters.AddWithValue("@NgayThayDoi", lichSu.NgayThayDoi);
                                cmdInsert.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] CapNhatKhoVaGhiLog: {ex.Message}");
                return false;
            }
        }

        // Lấy lịch sử thay đổi của 1 sản phẩm
        public DataTable LayLichSuThayDoi(int maSanPham)
        {
            string query = @"
                SELECT 
                    ls.MaLichSu,
                    ls.SoLuongCu,
                    ls.SoLuongMoi,
                    ls.ChenhLech,
                    ls.LoaiThayDoi,
                    ls.LyDo,
                    ls.GhiChu,
                    nv.TenNhanVien,
                    ls.NgayThayDoi
                FROM Tbl_LichSuThayDoiKho ls
                JOIN Tbl_NhanVien nv ON ls.MaNhanVien = nv.MaNhanVien
                WHERE ls.MaSanPham = @MaSanPham
                ORDER BY ls.NgayThayDoi DESC";

            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy lịch sử thay đổi: " + ex.Message);
            }
            return dataTable;
        }
    }
}
