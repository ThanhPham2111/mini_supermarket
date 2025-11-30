using mini_supermarket.DB;
using mini_supermarket.DTO;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace mini_supermarket.DAO
{
    public class KhoHangDAO
    {
        public IList<TonKhoDTO> LayDanhSachTonKho()
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

            var list = new List<TonKhoDTO>();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new TonKhoDTO
                                {
                                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSP")),
                                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                    TenDonVi = reader.IsDBNull(reader.GetOrdinal("TenDonVi")) ? null : reader.GetString(reader.GetOrdinal("TenDonVi")),
                                    TenLoai = reader.IsDBNull(reader.GetOrdinal("TenLoai")) ? null : reader.GetString(reader.GetOrdinal("TenLoai")),
                                    TenThuongHieu = reader.IsDBNull(reader.GetOrdinal("TenThuongHieu")) ? null : reader.GetString(reader.GetOrdinal("TenThuongHieu")),
                                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                                    MaLoai = reader.IsDBNull(reader.GetOrdinal("MaLoai")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("MaLoai")),
                                    MaThuongHieu = reader.IsDBNull(reader.GetOrdinal("MaThuongHieu")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("MaThuongHieu")),
                                    GiaBan = reader.IsDBNull(reader.GetOrdinal("GiaBan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                                    Hsd = reader.IsDBNull(reader.GetOrdinal("Hsd")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Hsd")),
                                    GiaNhap = reader.IsDBNull(reader.GetOrdinal("GiaNhap")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaNhap"))
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách tồn kho: " + ex.Message);
                throw; // Ném lại lỗi để lớp BUS xử lý
            }
            return list;
        }

        public IList<LoaiDTO> LayDanhSachLoai()
        {
            var list = new List<LoaiDTO>();
            string query = "SELECT MaLoai, TenLoai FROM Tbl_Loai;";
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new LoaiDTO
                                {
                                    MaLoai = reader.GetInt32(reader.GetOrdinal("MaLoai")),
                                    TenLoai = reader.GetString(reader.GetOrdinal("TenLoai")),
                                    MoTa = null,
                                    TrangThai = "HoatDong"
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách loại: " + ex.Message);
                throw;
            }
            return list;
        }

        public IList<ThuongHieuDTO> LayDanhSachThuongHieu()
        {
            var list = new List<ThuongHieuDTO>();
            string query = "SELECT MaThuongHieu, TenThuongHieu FROM Tbl_ThuongHieu;";
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ThuongHieuDTO
                                {
                                    MaThuongHieu = reader.GetInt32(reader.GetOrdinal("MaThuongHieu")),
                                    TenThuongHieu = reader.GetString(reader.GetOrdinal("TenThuongHieu")),
                                    TrangThai = "HoatDong"
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách thương hiệu: " + ex.Message);
                throw;
            }
            return list;
        }

        public IList<SanPhamBanHangDTO> LayDanhSachSanPhamBanHang()
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

            var list = new List<SanPhamBanHangDTO>();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new SanPhamBanHangDTO
                                {
                                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                    GiaBan = reader.IsDBNull(reader.GetOrdinal("GiaBan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                    KhuyenMai = reader.GetString(reader.GetOrdinal("KhuyenMai")),
                                    PhanTramGiam = reader.GetDecimal(reader.GetOrdinal("PhanTramGiam"))
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách sản phẩm bán hàng: " + ex.Message);
                throw;
            }
            return list;
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
        public IList<LichSuThayDoiKhoDTO> LayLichSuThayDoi(int maSanPham)
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

            var list = new List<LichSuThayDoiKhoDTO>();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new LichSuThayDoiKhoDTO
                                {
                                    MaLichSu = reader.GetInt32(reader.GetOrdinal("MaLichSu")),
                                    MaSanPham = maSanPham, // từ parameter
                                    SoLuongCu = reader.GetInt32(reader.GetOrdinal("SoLuongCu")),
                                    SoLuongMoi = reader.GetInt32(reader.GetOrdinal("SoLuongMoi")),
                                    ChenhLech = reader.GetInt32(reader.GetOrdinal("ChenhLech")),
                                    LoaiThayDoi = reader.GetString(reader.GetOrdinal("LoaiThayDoi")),
                                    LyDo = reader.IsDBNull(reader.GetOrdinal("LyDo")) ? null : reader.GetString(reader.GetOrdinal("LyDo")),
                                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? null : reader.GetString(reader.GetOrdinal("GhiChu")),
                                    MaNhanVien = 0, // không select, set default
                                    TenNhanVien = reader.GetString(reader.GetOrdinal("TenNhanVien")),
                                    NgayThayDoi = reader.GetDateTime(reader.GetOrdinal("NgayThayDoi"))
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy lịch sử thay đổi: " + ex.Message);
                throw;
            }
            return list;
        }

        public IList<SanPhamChiTietDTO> LayThongTinSanPhamChiTiet(int maSanPham)
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

            var list = new List<SanPhamChiTietDTO>();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new SanPhamChiTietDTO
                                {
                                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                    GiaBan = reader.IsDBNull(reader.GetOrdinal("GiaBan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                                    HinhAnh = reader.IsDBNull(reader.GetOrdinal("HinhAnh")) ? null : reader.GetString(reader.GetOrdinal("HinhAnh")),
                                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                    KhuyenMai = reader.GetString(reader.GetOrdinal("KhuyenMai")),
                                    PhanTramGiam = reader.GetDecimal(reader.GetOrdinal("PhanTramGiam"))
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin sản phẩm chi tiết: " + ex.Message);
                throw;
            }
            return list;
        }

        // Lấy danh sách sản phẩm sắp hết hàng (từ 1 đến 10)
        public IList<SanPhamKhoDTO> LaySanPhamSapHetHang()
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

            var list = new List<SanPhamKhoDTO>();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new SanPhamKhoDTO
                                {
                                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                    GiaBan = reader.IsDBNull(reader.GetOrdinal("GiaBan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                                    TenDonVi = reader.IsDBNull(reader.GetOrdinal("TenDonVi")) ? null : reader.GetString(reader.GetOrdinal("TenDonVi")),
                                    TenLoai = reader.IsDBNull(reader.GetOrdinal("TenLoai")) ? null : reader.GetString(reader.GetOrdinal("TenLoai"))
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách sắp hết hàng: " + ex.Message);
                throw;
            }
            return list;
        }

        // Lấy danh sách sản phẩm hết hàng hoàn toàn
        public IList<SanPhamKhoDTO> LaySanPhamHetHang()
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

            var list = new List<SanPhamKhoDTO>();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new SanPhamKhoDTO
                                {
                                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                    GiaBan = reader.IsDBNull(reader.GetOrdinal("GiaBan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                                    TenDonVi = reader.IsDBNull(reader.GetOrdinal("TenDonVi")) ? null : reader.GetString(reader.GetOrdinal("TenDonVi")),
                                    TenLoai = reader.IsDBNull(reader.GetOrdinal("TenLoai")) ? null : reader.GetString(reader.GetOrdinal("TenLoai"))
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách hết hàng: " + ex.Message);
                throw;
            }
            return list;
        }

        /// <summary>
        /// Giảm số lượng kho và ghi log trong cùng một transaction.
        /// </summary>
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
