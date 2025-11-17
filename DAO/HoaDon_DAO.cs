using mini_supermarket.DTO;
using mini_supermarket.DB;

namespace mini_supermarket.DAO
{
    public class HoaDon_DAO
    {
        public IList<HoaDonDTO> GetHoaDon()
        {
            var hoaDonList = new List<HoaDonDTO>();
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT MaHoaDon, MaHoaDonCode, NgayLap, MaNhanVien, MaKhachHang, TongTien from Tbl_HoaDon";
            command.CommandText += "Order By MaHoaDon";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                hoaDonList.Add(new HoaDonDTO(
                    maHoaDon: reader.GetInt32(reader.GetOrdinal("MaHoaDon")),
                    maHoaDonCode: reader.GetString(reader.GetOrdinal("MaHoaDonCode")),
                    ngayLap: reader.IsDBNull(reader.GetOrdinal("NgayLap")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayLap")),
                    maNhanVien: reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    maKhachHang: reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    tongTien: reader.GetDecimal(reader.GetOrdinal("TongTien"))
                ));
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
    }
}