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
                    sp.MaThuongHieu,
                    sp.Hsd
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
                throw;
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
                throw;
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
                throw;
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
                WHERE kh.SoLuong > 0 -- Chỉ cần kiểm tra số lượng trong kho > 0
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
                throw;
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
                throw;
            }
        }

        public KhoHangDTO? GetByMaSanPham(int maSanPham)
        {
            const string query = @"SELECT MaSanPham, SoLuong, TrangThai 
                                   FROM Tbl_KhoHang WHERE MaSanPham = @MaSanPham";

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
                throw;
            }

            return null;
        }

        public void UpdateKhoHang(KhoHangDTO khoHang)
        {
            const string query = @"UPDATE Tbl_KhoHang 
                                   SET SoLuong = @SoLuong, 
                                       TrangThai = @TrangThai
                                   WHERE MaSanPham = @MaSanPham";

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
                throw;
            }
        }

        public void InsertKhoHang(KhoHangDTO khoHang)
        {
            const string query = @"INSERT INTO Tbl_KhoHang (MaSanPham, SoLuong, TrangThai) 
                                   VALUES (@MaSanPham, @SoLuong, @TrangThai)";

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
                throw;
            }
        }

        public IList<KhoHangDTO> GetAllKhoHangWithPrice()
        {
            var list = new List<KhoHangDTO>();
            const string query = @"
                SELECT 
                    kh.MaSanPham,
                    sp.TenSanPham,
                    kh.SoLuong,
                    kh.TrangThai,
                    -- Lấy giá nhập mới nhất từ ChiTietPhieuNhap
                    (SELECT TOP 1 ctpn.DonGiaNhap
                     FROM Tbl_ChiTietPhieuNhap ctpn
                     INNER JOIN Tbl_PhieuNhap pn ON ctpn.MaPhieuNhap = pn.MaPhieuNhap
                     WHERE ctpn.MaSanPham = kh.MaSanPham
                       AND pn.TrangThai = N'Nhập thành công'
                     ORDER BY pn.NgayNhap DESC, ctpn.MaChiTietPhieuNhap DESC) AS GiaNhap,
                    -- Lấy giá bán từ Tbl_SanPham
                    sp.GiaBan AS GiaBan
                FROM Tbl_KhoHang kh
                INNER JOIN Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham
                ORDER BY sp.TenSanPham";

            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new KhoHangDTO
                            {
                                MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                TenSanPham = reader.IsDBNull(reader.GetOrdinal("TenSanPham")) ? null : reader.GetString(reader.GetOrdinal("TenSanPham")),
                                SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                                GiaNhap = reader.IsDBNull(reader.GetOrdinal("GiaNhap")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaNhap")),
                                GiaBan = reader.IsDBNull(reader.GetOrdinal("GiaBan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaBan"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetAllKhoHangWithPrice: {ex.Message}");
                throw;
            }

            return list;
        }

        // PHƯƠNG ÁN 2: Cập nhật kho + Ghi log lịch sử 
        public bool CapNhatKhoVaGhiLog(KhoHangDTO khoHang, LichSuThayDoiKhoDTO lichSu)
        {
            string queryUpdateKho = @"UPDATE Tbl_KhoHang 
                                      SET SoLuong = @SoLuongMoi, 
                                          TrangThai = @TrangThai
                                      WHERE MaSanPham = @MaSanPham";

            string queryInsertKho = @"INSERT INTO Tbl_KhoHang (MaSanPham, SoLuong, TrangThai)
                                      VALUES (@MaSanPham, @SoLuongMoi, @TrangThai)";

            string queryUpdateSanPham = @"UPDATE Tbl_SanPham SET TrangThai = @TrangThaiSanPham WHERE MaSanPham = @MaSanPham";

            string queryInsertLog = @"INSERT INTO Tbl_LichSuThayDoiKho 
                                      (MaSanPham, SoLuongCu, SoLuongMoi, ChenhLech, LoaiThayDoi, LyDo, GhiChu, MaNhanVien, NgayThayDoi)
                                      VALUES (@MaSanPham, @SoLuongCu, @SoLuongMoi, @ChenhLech, @LoaiThayDoi, @LyDo, @GhiChu, @MaNhanVien, @NgayThayDoi)";

            using (SqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected;
                        // 1. Cố gắng cập nhật số lượng kho
                        using (SqlCommand cmdUpdate = new SqlCommand(queryUpdateKho, connection, transaction))
                        {
                            cmdUpdate.Parameters.AddWithValue("@SoLuongMoi", khoHang.SoLuong ?? (object)DBNull.Value);
                            cmdUpdate.Parameters.AddWithValue("@TrangThai", khoHang.TrangThai ?? (object)DBNull.Value);
                            cmdUpdate.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                            rowsAffected = cmdUpdate.ExecuteNonQuery();
                        }

                        // Nếu không có dòng nào được cập nhật (sản phẩm chưa có trong kho), thì thêm mới
                        if (rowsAffected == 0)
                        {
                            using (SqlCommand cmdInsertKho = new SqlCommand(queryInsertKho, connection, transaction))
                            {
                                cmdInsertKho.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                                cmdInsertKho.Parameters.AddWithValue("@SoLuongMoi", khoHang.SoLuong ?? (object)DBNull.Value);
                                cmdInsertKho.Parameters.AddWithValue("@TrangThai", khoHang.TrangThai ?? (object)DBNull.Value);
                                cmdInsertKho.ExecuteNonQuery();
                            }
                        }

                        // 2. Đồng bộ trạng thái qua Tbl_SanPham
                        string trangThaiSanPham = (khoHang.SoLuong.GetValueOrDefault() > 0) ? "Còn hàng" : "Hết hàng";
                        using (SqlCommand cmdUpdateSP = new SqlCommand(queryUpdateSanPham, connection, transaction))
                        {
                            cmdUpdateSP.Parameters.AddWithValue("@TrangThaiSanPham", trangThaiSanPham);
                            cmdUpdateSP.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                            cmdUpdateSP.ExecuteNonQuery();
                        }

                        // 3. Ghi log lịch sử
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
                        throw; // Ném lại lỗi để các lớp trên xử lý
                    }
                }
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
                throw;
            }
            return dataTable;
        }

        public DataTable LayThongTinSanPhamChiTiet(int maSanPham)
        {
            string query = @"
                SELECT 
                    sp.MaSanPham,
                    sp.TenSanPham,
                    sp.GiaBan,
                    sp.HinhAnh,
                    kh.SoLuong,
                    ISNULL(km.TenKhuyenMai, N'') AS KhuyenMai,
                    ISNULL(km.PhanTramGiamGia, 0) AS PhanTramGiam
                FROM Tbl_SanPham sp
                INNER JOIN Tbl_KhoHang kh ON sp.MaSanPham = kh.MaSanPham
                LEFT JOIN Tbl_KhuyenMai km ON sp.MaSanPham = km.MaSanPham 
                    AND GETDATE() BETWEEN km.NgayBatDau AND km.NgayKetThuc
                WHERE sp.MaSanPham = @MaSanPham;";

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
                Console.WriteLine("Lỗi khi lấy thông tin sản phẩm chi tiết: " + ex.Message);
                throw;
            }
            return dataTable;
        }

        // Lấy danh sách sản phẩm sắp hết hàng (từ 1 đến 10)
        public DataTable LaySanPhamSapHetHang()
        {
            string query = @"
                SELECT 
                    kh.MaSanPham,
                    sp.TenSanPham,
                    kh.SoLuong,
                    sp.GiaBan,
                    dv.TenDonVi,
                    l.TenLoai
                FROM Tbl_KhoHang kh
                JOIN Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham
                LEFT JOIN Tbl_DonVi dv ON sp.MaDonVi = dv.MaDonVi
                LEFT JOIN Tbl_Loai l ON sp.MaLoai = l.MaLoai
                WHERE kh.SoLuong > 0 AND kh.SoLuong <= 10
                ORDER BY kh.SoLuong ASC, sp.TenSanPham ASC;";

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
                Console.WriteLine("Lỗi khi lấy danh sách sắp hết hàng: " + ex.Message);
                throw;
            }
            return dataTable;
        }

        // Lấy danh sách sản phẩm hết hàng hoàn toàn
        public DataTable LaySanPhamHetHang()
        {
            string query = @"
                SELECT 
                    kh.MaSanPham,
                    sp.TenSanPham,
                    kh.SoLuong,
                    sp.GiaBan,
                    dv.TenDonVi,
                    l.TenLoai
                FROM Tbl_KhoHang kh
                JOIN Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham
                LEFT JOIN Tbl_DonVi dv ON sp.MaDonVi = dv.MaDonVi
                LEFT JOIN Tbl_Loai l ON sp.MaLoai = l.MaLoai
                WHERE kh.SoLuong = 0
                ORDER BY sp.TenSanPham ASC;";

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
                Console.WriteLine("Lỗi khi lấy danh sách hết hàng: " + ex.Message);
                throw;
            }
            return dataTable;
        }

        // Lấy giá nhập mới nhất từ ChiTietPhieuNhap
        public decimal? GetGiaNhapMoiNhat(int maSanPham)
        {
            const string query = @"
                SELECT TOP 1 ctpn.DonGiaNhap
                FROM Tbl_ChiTietPhieuNhap ctpn
                INNER JOIN Tbl_PhieuNhap pn ON ctpn.MaPhieuNhap = pn.MaPhieuNhap
                WHERE ctpn.MaSanPham = @MaSanPham
                  AND pn.TrangThai = N'Nhập thành công'
                ORDER BY pn.NgayNhap DESC, ctpn.MaChiTietPhieuNhap DESC";

            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetGiaNhapMoiNhat: {ex.Message}");
                throw;
            }

            return null;
        }

        public bool GiamSoLuongVaGhiLog(int maSanPham, int soLuongGiam, LichSuThayDoiKhoDTO lichSu)
        {
            using (var connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Bước 1: Giảm số lượng một cách an toàn
                        const string updateQuery = @"
                            UPDATE Tbl_KhoHang
                            SET SoLuong = SoLuong - @SoLuongGiam,
                                TrangThai = CASE 
                                    WHEN (SoLuong - @SoLuongGiam) <= 0 THEN N'Hết hàng'
                                    WHEN (SoLuong - @SoLuongGiam) <= 5 THEN N'Cảnh báo - Tần cận'
                                    WHEN (SoLuong - @SoLuongGiam) <= 10 THEN N'Cảnh báo - Sắp hết hàng'
                                    ELSE N'Còn hàng'
                                END
                            WHERE MaSanPham = @MaSanPham AND SoLuong >= @SoLuongGiam";

                        int rowsAffected;
                        using (var command = new SqlCommand(updateQuery, connection, transaction))
                        {
                            command.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = maSanPham;
                            command.Parameters.Add("@SoLuongGiam", SqlDbType.Int).Value = soLuongGiam;
                            rowsAffected = command.ExecuteNonQuery();
                        }

                        // Nếu không có dòng nào được cập nhật, nghĩa là không đủ hàng
                        if (rowsAffected == 0)
                        {
                            transaction.Rollback();
                            return false; // Hoặc ném exception để báo lỗi không đủ hàng
                        }

                        // Bước 2: Đồng bộ trạng thái qua Tbl_SanPham
                        string trangThaiSanPham = (lichSu.SoLuongMoi > 0) ? "Còn hàng" : "Hết hàng";
                        const string updateProductQuery = "UPDATE Tbl_SanPham SET TrangThai = @TrangThai WHERE MaSanPham = @MaSanPham";
                        using (var cmdUpdateProduct = new SqlCommand(updateProductQuery, connection, transaction))
                        {
                            cmdUpdateProduct.Parameters.AddWithValue("@TrangThai", trangThaiSanPham);
                            cmdUpdateProduct.Parameters.AddWithValue("@MaSanPham", maSanPham);
                            cmdUpdateProduct.ExecuteNonQuery();
                        }

                        // Bước 3: Ghi log lịch sử
                        const string logQuery = @"
                            INSERT INTO Tbl_LichSuThayDoiKho 
                            (MaSanPham, SoLuongCu, SoLuongMoi, ChenhLech, LoaiThayDoi, LyDo, GhiChu, MaNhanVien, NgayThayDoi)
                            VALUES (@MaSanPham, @SoLuongCu, @SoLuongMoi, @ChenhLech, @LoaiThayDoi, @LyDo, @GhiChu, @MaNhanVien, @NgayThayDoi)";

                        using (var command = new SqlCommand(logQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@MaSanPham", lichSu.MaSanPham);
                            command.Parameters.AddWithValue("@SoLuongCu", lichSu.SoLuongCu);
                            command.Parameters.AddWithValue("@SoLuongMoi", lichSu.SoLuongMoi);
                            command.Parameters.AddWithValue("@ChenhLech", lichSu.ChenhLech);
                            command.Parameters.AddWithValue("@LoaiThayDoi", lichSu.LoaiThayDoi);
                            command.Parameters.AddWithValue("@LyDo", lichSu.LyDo ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@GhiChu", lichSu.GhiChu ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@MaNhanVien", lichSu.MaNhanVien);
                            command.Parameters.AddWithValue("@NgayThayDoi", lichSu.NgayThayDoi);
                            command.ExecuteNonQuery();
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
    }
}
