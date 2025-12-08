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
            var list = new List<TonKhoDTO>();
            string query = @"
                SELECT kh.MaSanPham, sp.TenSanPham, dv.TenDonVi, l.TenLoai, th.TenThuongHieu,
                       kh.SoLuong, kh.TrangThai, sp.MaLoai, sp.MaThuongHieu,
                       sp.GiaBan, sp.Hsd,
                       (SELECT TOP 1 ct.DonGiaNhap FROM Tbl_ChiTietPhieuNhap ct
                        INNER JOIN Tbl_PhieuNhap p ON ct.MaPhieuNhap = p.MaPhieuNhap
                        WHERE ct.MaSanPham = kh.MaSanPham AND p.TrangThai = N'Nhập thành công'
                        ORDER BY p.NgayNhap DESC, ct.MaChiTietPhieuNhap DESC) AS GiaNhapMoiNhat
                FROM Tbl_KhoHang kh
                INNER JOIN Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham
                LEFT JOIN Tbl_DonVi dv ON sp.MaDonVi = dv.MaDonVi
                LEFT JOIN Tbl_Loai l ON sp.MaLoai = l.MaLoai
                LEFT JOIN Tbl_ThuongHieu th ON sp.MaThuongHieu = th.MaThuongHieu";

            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 300; // Tăng timeout lên 5 phút
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new TonKhoDTO
                                {
                                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
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
                                    GiaNhap = reader.IsDBNull(reader.GetOrdinal("GiaNhapMoiNhat")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaNhapMoiNhat"))
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
                throw;
            }
            return list;
        }

        public IList<LoaiDTO> LayDanhSachLoai()
        {
            var list = new List<LoaiDTO>();
            string query = "SELECT MaLoai, TenLoai, MoTa, TrangThai FROM Tbl_Loai";

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
                                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString(reader.GetOrdinal("MoTa")),
                                    TrangThai = reader.GetString(reader.GetOrdinal("TrangThai"))
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
            string query = "SELECT MaThuongHieu, TenThuongHieu, TrangThai FROM Tbl_ThuongHieu";

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
                                    TrangThai = reader.GetString(reader.GetOrdinal("TrangThai"))
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

        public IList<SanPhamDTO> LayDanhSachSanPhamBanHang()
        {
            var list = new List<SanPhamDTO>();
            DateTime currentDate = DateTime.Now;
            string query = @"
                SELECT sp.MaSanPham, sp.TenSanPham, sp.GiaBan, kh.SoLuong,
                       ISNULL(km.TenKhuyenMai, '') AS KhuyenMai,
                       ISNULL(km.PhanTramGiamGia, 0) AS PhanTramGiam, sp.Hsd
                FROM Tbl_SanPham sp
                INNER JOIN Tbl_KhoHang kh ON sp.MaSanPham = kh.MaSanPham
                LEFT JOIN Tbl_KhuyenMai km ON sp.MaSanPham = km.MaSanPham
                    AND @CurrentDate BETWEEN km.NgayBatDau AND km.NgayKetThuc
                WHERE kh.SoLuong > 0 AND ISNULL(kh.TrangThaiDieuKien, 'Bán') = 'Bán'
                ORDER BY sp.TenSanPham";

            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CurrentDate", currentDate);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new SanPhamDTO
                                {
                                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                    GiaBan = reader.IsDBNull(reader.GetOrdinal("GiaBan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                    KhuyenMai = reader.GetString(reader.GetOrdinal("KhuyenMai")),
                                    PhanTramGiam = reader.GetDecimal(reader.GetOrdinal("PhanTramGiam")),
                                    Hsd = reader.IsDBNull(reader.GetOrdinal("Hsd")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Hsd"))
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
            string query = "SELECT COUNT(1) FROM Tbl_KhoHang WHERE MaSanPham = @MaSanPham";
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                        var result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi kiểm tra tồn tại sản phẩm: " + ex.Message);
                throw;
            }
        }

        public KhoHangDTO? GetByMaSanPham(int maSanPham)
        {
            string query = "SELECT MaSanPham, SoLuong, TrangThai, TrangThaiDieuKien FROM Tbl_KhoHang WHERE MaSanPham = @MaSanPham";
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
                            if (reader.Read())
                            {
                                return new KhoHangDTO
                                {
                                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                                    TrangThaiDieuKien = reader.IsDBNull(reader.GetOrdinal("TrangThaiDieuKien")) ? null : reader.GetString(reader.GetOrdinal("TrangThaiDieuKien"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy kho hàng theo mã sản phẩm: " + ex.Message);
                throw;
            }
            return null;
        }

        public void UpdateKhoHang(KhoHangDTO khoHang)
        {
            string query = "UPDATE Tbl_KhoHang SET SoLuong = @SoLuong, TrangThai = @TrangThai, TrangThaiDieuKien = @TrangThaiDieuKien WHERE MaSanPham = @MaSanPham";
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                        cmd.Parameters.AddWithValue("@SoLuong", khoHang.SoLuong ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", khoHang.TrangThai ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThaiDieuKien", khoHang.TrangThaiDieuKien ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật kho hàng: " + ex.Message);
                throw;
            }
        }

        public void InsertKhoHang(KhoHangDTO khoHang)
        {
            string query = "INSERT INTO Tbl_KhoHang (MaSanPham, SoLuong, TrangThai, TrangThaiDieuKien) VALUES (@MaSanPham, @SoLuong, @TrangThai, @TrangThaiDieuKien)";
            try
            {
                using (SqlConnection conn = DbConnectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                        cmd.Parameters.AddWithValue("@SoLuong", khoHang.SoLuong ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", khoHang.TrangThai ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThaiDieuKien", khoHang.TrangThaiDieuKien ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm kho hàng: " + ex.Message);
                throw;
            }
        }

        public bool CapNhatKhoVaGhiLog(KhoHangDTO khoHang, LichSuThayDoiKhoDTO lichSu)
        {
            // Validation: Không cho phép đặt trạng thái bán thành "Bán" nếu số lượng bằng 0
            if (khoHang.TrangThaiDieuKien == "Bán" && (khoHang.SoLuong ?? 0) == 0)
            {
                throw new InvalidOperationException("Không thể đặt trạng thái bán thành 'Bán' khi số lượng bằng 0.");
            }

            using (SqlConnection conn = DbConnectionFactory.CreateConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Tự động chuyển trạng thái bán thành "Không bán" khi số lượng = 0
                        string trangThaiDieuKien = khoHang.TrangThaiDieuKien;
                        if ((khoHang.SoLuong ?? 0) == 0)
                        {
                            trangThaiDieuKien = "Không bán";
                        }
                        else if (string.IsNullOrWhiteSpace(trangThaiDieuKien))
                        {
                            trangThaiDieuKien = "Bán";
                        }

                        // Update KhoHang
                        string updateKhoQuery = "UPDATE Tbl_KhoHang SET SoLuong = @SoLuong, TrangThai = @TrangThai, TrangThaiDieuKien = @TrangThaiDieuKien WHERE MaSanPham = @MaSanPham";
                        using (SqlCommand cmd = new SqlCommand(updateKhoQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                            cmd.Parameters.AddWithValue("@SoLuong", khoHang.SoLuong ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TrangThai", khoHang.TrangThai ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TrangThaiDieuKien", trangThaiDieuKien);
                            cmd.ExecuteNonQuery();
                        }

                        // Đồng bộ trạng thái qua Tbl_SanPham
                        string trangThaiSanPham = (khoHang.SoLuong.GetValueOrDefault() > 0) ? "Còn hàng" : "Hết hàng";
                        string updateSanPhamQuery = "UPDATE Tbl_SanPham SET TrangThai = @TrangThai WHERE MaSanPham = @MaSanPham";
                        using (SqlCommand cmd = new SqlCommand(updateSanPhamQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", trangThaiSanPham);
                            cmd.Parameters.AddWithValue("@MaSanPham", khoHang.MaSanPham);
                            cmd.ExecuteNonQuery();
                        }

                        // Ghi log lịch sử
                        string logQuery = @"INSERT INTO Tbl_LichSuThayDoiKho 
                            (MaSanPham, SoLuongCu, SoLuongMoi, ChenhLech, LoaiThayDoi, LyDo, GhiChu, MaNhanVien, NgayThayDoi)
                            VALUES (@MaSanPham, @SoLuongCu, @SoLuongMoi, @ChenhLech, @LoaiThayDoi, @LyDo, @GhiChu, @MaNhanVien, @NgayThayDoi)";
                        using (SqlCommand cmd = new SqlCommand(logQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaSanPham", lichSu.MaSanPham);
                            cmd.Parameters.AddWithValue("@SoLuongCu", lichSu.SoLuongCu);
                            cmd.Parameters.AddWithValue("@SoLuongMoi", lichSu.SoLuongMoi);
                            cmd.Parameters.AddWithValue("@ChenhLech", lichSu.ChenhLech);
                            cmd.Parameters.AddWithValue("@LoaiThayDoi", lichSu.LoaiThayDoi);
                            cmd.Parameters.AddWithValue("@LyDo", lichSu.LyDo ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@GhiChu", lichSu.GhiChu ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@MaNhanVien", lichSu.MaNhanVien);
                            cmd.Parameters.AddWithValue("@NgayThayDoi", lichSu.NgayThayDoi);
                            cmd.ExecuteNonQuery();
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
                                    MaSanPham = maSanPham,
                                    SoLuongCu = reader.GetInt32(reader.GetOrdinal("SoLuongCu")),
                                    SoLuongMoi = reader.GetInt32(reader.GetOrdinal("SoLuongMoi")),
                                    ChenhLech = reader.GetInt32(reader.GetOrdinal("ChenhLech")),
                                    LoaiThayDoi = reader.GetString(reader.GetOrdinal("LoaiThayDoi")),
                                    LyDo = reader.IsDBNull(reader.GetOrdinal("LyDo")) ? null : reader.GetString(reader.GetOrdinal("LyDo")),
                                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? null : reader.GetString(reader.GetOrdinal("GhiChu")),
                                    MaNhanVien = 0,
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

        public IList<SanPhamDTO> LayThongTinSanPhamChiTiet(int maSanPham)
        {
            string query = @"
                SELECT 
                    sp.MaSanPham,
                    sp.TenSanPham,
                    sp.GiaBan,
                    sp.HinhAnh,
                    kh.SoLuong,
                    ISNULL(km.TenKhuyenMai, N'') AS KhuyenMai,
                    ISNULL(km.PhanTramGiamGia, 0) AS PhanTramGiam,
                    dv.TenDonVi,
                    l.TenLoai
                FROM Tbl_SanPham sp
                INNER JOIN Tbl_KhoHang kh ON sp.MaSanPham = kh.MaSanPham
                LEFT JOIN Tbl_KhuyenMai km ON sp.MaSanPham = km.MaSanPham 
                    AND GETDATE() BETWEEN km.NgayBatDau AND km.NgayKetThuc
                LEFT JOIN Tbl_DonVi dv ON sp.MaDonVi = dv.MaDonVi
                LEFT JOIN Tbl_Loai l ON sp.MaLoai = l.MaLoai
                WHERE sp.MaSanPham = @MaSanPham;";

            var list = new List<SanPhamDTO>();
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
                                var item = new SanPhamDTO
                                {
                                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                    TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                    GiaBan = reader.IsDBNull(reader.GetOrdinal("GiaBan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                                    HinhAnh = reader.IsDBNull(reader.GetOrdinal("HinhAnh")) ? null : reader.GetString(reader.GetOrdinal("HinhAnh")),
                                    SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                    KhuyenMai = reader.GetString(reader.GetOrdinal("KhuyenMai")),
                                    PhanTramGiam = reader.GetDecimal(reader.GetOrdinal("PhanTramGiam")),
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

        // Lấy giá nhập mới nhất từ ChiTietPhieuNhap
        public decimal? GetGiaNhapMoiNhat(int maSanPham)
        {
            System.Diagnostics.Debug.WriteLine($"[GetGiaNhapMoiNhat] START - MaSanPham={maSanPham}");
            
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
                    
                    System.Diagnostics.Debug.WriteLine($"[GetGiaNhapMoiNhat] Query result: {result ?? "NULL"}");
                    
                    if (result != null && result != DBNull.Value)
                    {
                        decimal giaNhap = Convert.ToDecimal(result);
                        System.Diagnostics.Debug.WriteLine($"[GetGiaNhapMoiNhat] SUCCESS - MaSanPham={maSanPham}, GiaNhap={giaNhap}");
                        return giaNhap;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"[GetGiaNhapMoiNhat] No price found for MaSanPham={maSanPham}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[GetGiaNhapMoiNhat] ERROR - MaSanPham={maSanPham}, Exception={ex.Message}");
                Console.WriteLine($"[ERROR] GetGiaNhapMoiNhat: {ex.Message}");
                throw;
            }

            return null;
        }

        /// <summary>
        /// Giảm số lượng kho và ghi log trong cùng một transaction.
        /// </summary>
        public bool GiamSoLuongVaGhiLog(int maSanPham, int soLuongGiam, LichSuThayDoiKhoDTO lichSu)
        {
            using (SqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Bước 1: Giảm số lượng một cách an toàn
                        // Tự động chuyển trạng thái bán thành "Không bán" khi số lượng = 0
                        const string updateQuery = @"
                            UPDATE Tbl_KhoHang
                            SET SoLuong = SoLuong - @SoLuongGiam,
                                TrangThai = CASE 
                                    WHEN (SoLuong - @SoLuongGiam) <= 0 THEN N'Hết hàng'
                                    WHEN (SoLuong - @SoLuongGiam) <= 5 THEN N'Cảnh báo - Tần cận'
                                    WHEN (SoLuong - @SoLuongGiam) <= 10 THEN N'Cảnh báo - Sắp hết hàng'
                                    ELSE N'Cón hàng'
                                END,
                                TrangThaiDieuKien = CASE 
                                    WHEN (SoLuong - @SoLuongGiam) <= 0 THEN N'Không bán'
                                    ELSE TrangThaiDieuKien
                                END
                            WHERE MaSanPham = @MaSanPham AND SoLuong >= @SoLuongGiam";

                        int rowsAffected;
                        using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
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
                        using (SqlCommand cmdUpdateProduct = new SqlCommand(updateProductQuery, connection, transaction))
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

                        using (SqlCommand command = new SqlCommand(logQuery, connection, transaction))
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

        // Lấy danh sách kho hàng kèm giá nhập và giá bán
        public IList<KhoHangDTO> GetAllKhoHangWithPrice()
        {
            const string query = @"
                SELECT kh.MaSanPham, kh.SoLuong, kh.TrangThai, kh.TrangThaiDieuKien, sp.GiaBan, sp.TenSanPham,
                       (SELECT TOP 1 ct.DonGiaNhap FROM Tbl_ChiTietPhieuNhap ct INNER JOIN Tbl_PhieuNhap p ON ct.MaPhieuNhap = p.MaPhieuNhap WHERE ct.MaSanPham = kh.MaSanPham ORDER BY p.NgayNhap DESC) AS GiaNhap
                FROM Tbl_KhoHang kh
                JOIN Tbl_SanPham sp ON kh.MaSanPham = sp.MaSanPham";

            var list = new List<KhoHangDTO>();
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
                            var item = new KhoHangDTO()
                            {
                                MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                TenSanPham = reader.IsDBNull(reader.GetOrdinal("TenSanPham")) ? null : reader.GetString(reader.GetOrdinal("TenSanPham")),
                                SoLuong = reader.IsDBNull(reader.GetOrdinal("SoLuong")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                                TrangThaiDieuKien = reader.IsDBNull(reader.GetOrdinal("TrangThaiDieuKien")) ? "Bán" : reader.GetString(reader.GetOrdinal("TrangThaiDieuKien")),
                                GiaBan = reader.IsDBNull(reader.GetOrdinal("GiaBan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                                GiaNhap = reader.IsDBNull(reader.GetOrdinal("GiaNhap")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("GiaNhap"))
                            };
                            list.Add(item);
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
    }
}
