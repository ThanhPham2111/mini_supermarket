using mini_supermarket.DTO;
using mini_supermarket.DB;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms.VisualStyles;

namespace mini_supermarket.DAO
{
    public class HoaDon_DAO
    {
        public IList<HoaDonDTO> GetHoaDon()
        {
            var hoaDonList = new List<HoaDonDTO>();
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT h.MaHoaDon, h.MaHoaDonCode, h.NgayLap, h.MaNhanVien, h.MaKhachHang, h.TongTien, h.TrangThai,
                       nv.TenNhanVien as NhanVien,
                       ISNULL(kh.TenKhachHang, N'Khách lẻ') as KhachHang
                FROM Tbl_HoaDon h
                LEFT JOIN Tbl_NhanVien nv ON h.MaNhanVien = nv.MaNhanVien
                LEFT JOIN Tbl_KhachHang kh ON h.MaKhachHang = kh.MaKhachHang
                ORDER BY h.MaHoaDon";
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                hoaDonList.Add(new HoaDonDTO(
                    maHoaDon: reader.GetInt32(reader.GetOrdinal("MaHoaDon")),
                    maHoaDonCode: reader.GetString(reader.GetOrdinal("MaHoaDonCode")),
                    ngayLap: reader.IsDBNull(reader.GetOrdinal("NgayLap")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayLap")),
                    maNhanVien: reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    maKhachHang: reader.IsDBNull(reader.GetOrdinal("MaKhachHang")) ? null : reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    tongTien: reader.IsDBNull(reader.GetOrdinal("TongTien")) ? null : reader.GetDecimal(reader.GetOrdinal("TongTien")),
                    trangThai: reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? string.Empty : reader.GetString(reader.GetOrdinal("TrangThai"))
                )
                {
                    NhanVien = reader.GetString(reader.GetOrdinal("NhanVien")),
                    KhachHang = reader.GetString(reader.GetOrdinal("KhachHang"))
                });
            }
            return hoaDonList;
        }

        public int GetMaxMaHoaDon()
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ISNULL(MAX(MaHoaDon), 0) FROM dbo.Tbl_HoaDon";
            connection.Open();
            object? result = command.ExecuteScalar();
            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public HoaDonDTO? GetHoaDonById(int maHoaDon)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT h.MaHoaDon, h.MaHoaDonCode, h.NgayLap, h.MaNhanVien, h.MaKhachHang, h.TongTien, h.TrangThai,
                       nv.TenNhanVien as NhanVien,
                       ISNULL(kh.TenKhachHang, N'Khách lẻ') as KhachHang
                FROM Tbl_HoaDon h
                LEFT JOIN Tbl_NhanVien nv ON h.MaNhanVien = nv.MaNhanVien
                LEFT JOIN Tbl_KhachHang kh ON h.MaKhachHang = kh.MaKhachHang
                WHERE h.MaHoaDon = @MaHoaDon";
            command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
            connection.Open();
            
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new HoaDonDTO(
                    maHoaDon: reader.GetInt32(reader.GetOrdinal("MaHoaDon")),
                    maHoaDonCode: reader.GetString(reader.GetOrdinal("MaHoaDonCode")),
                    ngayLap: reader.IsDBNull(reader.GetOrdinal("NgayLap")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayLap")),
                    maNhanVien: reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    maKhachHang: reader.IsDBNull(reader.GetOrdinal("MaKhachHang")) ? null : reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    tongTien: reader.IsDBNull(reader.GetOrdinal("TongTien")) ? null : reader.GetDecimal(reader.GetOrdinal("TongTien")),
                    trangThai: reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? string.Empty : reader.GetString(reader.GetOrdinal("TrangThai"))
                )
                {
                    NhanVien = reader.GetString(reader.GetOrdinal("NhanVien")),
                    KhachHang = reader.GetString(reader.GetOrdinal("KhachHang"))
                };
            }
            return null;
        }

        public List<ChiTietHoaDonDTO> GetChiTietHoaDon(string maHoaDon){
            List<ChiTietHoaDonDTO> chiTietHoaDonList = new List<ChiTietHoaDonDTO>();
            int.TryParse(maHoaDon, out int numMaHoaDon);
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT ct.MaChiTietHoaDon, ct.MaHoaDon, ct.MaSanPham, ct.GiaBan, ct.SoLuong,
                       sp.TenSanPham, dv.TenDonVi as DonVi
                FROM dbo.Tbl_ChiTietHoaDon ct
                LEFT JOIN dbo.Tbl_SanPham sp ON ct.MaSanPham = sp.MaSanPham
                LEFT JOIN dbo.Tbl_DonVi dv ON sp.MaDonVi = dv.MaDonVi
                WHERE ct.maHoaDon = @maHoaDon";
            command.Parameters.AddWithValue("@maHoaDon", numMaHoaDon);
            connection.Open();

            using var reader = command.ExecuteReader();

            while(reader.Read()){
                chiTietHoaDonList.Add(new ChiTietHoaDonDTO
                {
                    MaChiTietHoaDon = reader.GetInt32(reader.GetOrdinal("MaChiTietHoaDon")),
                    MaHoaDon = reader.GetInt32(reader.GetOrdinal("MaHoaDon")),
                    MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                    GiaBan = reader.GetDecimal(reader.GetOrdinal("GiaBan")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    TenSanPham = reader.IsDBNull(reader.GetOrdinal("TenSanPham")) ? "N/A" : reader.GetString(reader.GetOrdinal("TenSanPham")),
                    DonVi = reader.IsDBNull(reader.GetOrdinal("DonVi")) ? "N/A" : reader.GetString(reader.GetOrdinal("DonVi"))
                });
                Console.WriteLine($"Loaded: {chiTietHoaDonList.Count} items");
            }

            return chiTietHoaDonList;
        }

        public int InsertHoaDon(HoaDonDTO hoaDon, SqlConnection? connection = null, SqlTransaction? transaction = null)
        {
            bool shouldCloseConnection = connection == null;
            if (connection == null)
            {
                connection = DbConnectionFactory.CreateConnection();
                connection.Open();
            }

            using var command = connection.CreateCommand();
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            command.CommandText = @"
                INSERT INTO dbo.Tbl_HoaDon (MaNhanVien, MaKhachHang, TongTien, NgayLap)
                VALUES (@MaNhanVien, @MaKhachHang, @TongTien, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            command.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.Int)
            {
                Value = hoaDon.MaNhanVien
            });

            if (hoaDon.MaKhachHang.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@MaKhachHang", SqlDbType.Int)
                {
                    Value = hoaDon.MaKhachHang.Value
                });
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@MaKhachHang", SqlDbType.Int)
                {
                    Value = DBNull.Value
                });
            }

            if (hoaDon.TongTien.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal)
                {
                    Value = hoaDon.TongTien.Value
                });
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal)
                {
                    Value = DBNull.Value
                });
            }

            if (shouldCloseConnection && connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Không thể tạo hóa đơn mới.");
            }

            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void InsertChiTietHoaDon(ChiTietHoaDonDTO chiTiet, SqlConnection? connection = null, SqlTransaction? transaction = null)
        {
            bool shouldCloseConnection = connection == null;
            if (connection == null)
            {
                connection = DbConnectionFactory.CreateConnection();
                connection.Open();
            }

            using var command = connection.CreateCommand();
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            command.CommandText = @"
                INSERT INTO dbo.Tbl_ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan)
                VALUES (@MaHoaDon, @MaSanPham, @SoLuong, @GiaBan);";

            command.Parameters.Add(new SqlParameter("@MaHoaDon", SqlDbType.Int)
            {
                Value = chiTiet.MaHoaDon
            });

            command.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int)
            {
                Value = chiTiet.MaSanPham
            });

            command.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int)
            {
                Value = chiTiet.SoLuong
            });

            command.Parameters.Add(new SqlParameter("@GiaBan", SqlDbType.Decimal)
            {
                Value = chiTiet.GiaBan
            });

            if (shouldCloseConnection && connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            command.ExecuteNonQuery();
        }
    
        public int HuyHoaDon(HoaDonDTO hoaDon){
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
            UPDATE dbo.Tbl_HoaDon
            Set TrangThai = N'Đã hủy'
            WHERE MaHoaDon = @MaHoaDon;";
            command.Parameters.AddWithValue("@MaHoaDon", hoaDon.MaHoaDon);
            connection.Open();
            int rs = command.ExecuteNonQuery();
            return rs;
        }

        public void LuuLichSuHuyHoaDon(int maHoaDon, string lyDoHuy, int maNhanVienHuy)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Tbl_LichSuHuyHoaDon')
                BEGIN
                    INSERT INTO dbo.Tbl_LichSuHuyHoaDon (MaHoaDon, LyDoHuy, MaNhanVienHuy, NgayHuy)
                    VALUES (@MaHoaDon, @LyDoHuy, @MaNhanVienHuy, GETDATE());
                END";
            command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
            command.Parameters.AddWithValue("@LyDoHuy", lyDoHuy ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@MaNhanVienHuy", maNhanVienHuy);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public (string? LyDoHuy, int? MaNhanVienHuy, DateTime? NgayHuy, string? TenNhanVienHuy) GetThongTinHuyHoaDon(int maHoaDon)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Tbl_LichSuHuyHoaDon')
                BEGIN
                    SELECT lh.LyDoHuy, lh.MaNhanVienHuy, lh.NgayHuy, nv.TenNhanVien as TenNhanVienHuy
                    FROM dbo.Tbl_LichSuHuyHoaDon lh
                    LEFT JOIN dbo.Tbl_NhanVien nv ON lh.MaNhanVienHuy = nv.MaNhanVien
                    WHERE lh.MaHoaDon = @MaHoaDon
                    ORDER BY lh.NgayHuy DESC;
                END";
            command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
            connection.Open();
            
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return (
                    reader.IsDBNull(reader.GetOrdinal("LyDoHuy")) ? null : reader.GetString(reader.GetOrdinal("LyDoHuy")),
                    reader.IsDBNull(reader.GetOrdinal("MaNhanVienHuy")) ? null : reader.GetInt32(reader.GetOrdinal("MaNhanVienHuy")),
                    reader.IsDBNull(reader.GetOrdinal("NgayHuy")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayHuy")),
                    reader.IsDBNull(reader.GetOrdinal("TenNhanVienHuy")) ? null : reader.GetString(reader.GetOrdinal("TenNhanVienHuy"))
                );
            }
            return (null, null, null, null);
        }
    }
}