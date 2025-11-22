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
                SELECT h.MaHoaDon, h.MaHoaDonCode, h.NgayLap, h.MaNhanVien, h.MaKhachHang, h.TongTien,
                       nv.TenNhanVien as NhanVien,
                       ISNULL(kh.TenKhachHang, 'Khách lẻ') as KhachHang
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
                    tongTien: reader.IsDBNull(reader.GetOrdinal("TongTien")) ? null : reader.GetDecimal(reader.GetOrdinal("TongTien"))
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

        public int InsertHoaDon(HoaDonDTO hoaDon)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
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

            connection.Open();
            object? result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Không thể tạo hóa đơn mới.");
            }

            return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void InsertChiTietHoaDon(ChiTietHoaDonDTO chiTiet)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
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

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}