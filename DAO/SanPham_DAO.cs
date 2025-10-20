using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using mini_supermarket.DTO;

namespace mini_supermarket.DAO
{
    public class SanPham_DAO
    {
        public IList<SanPhamDTO> GetSanPham(string? trangThaiFilter = null)
        {
            var sanPhamList = new List<SanPhamDTO>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT sp.MaSanPham,
                                          sp.TenSanPham,
                                          sp.MaDonVi,
                                          dv.TenDonVi,
                                          sp.MaThuongHieu,
                                          sp.MaLoai,
                                          sp.MoTa,
                                          sp.GiaBan,
                                          sp.HinhAnh,
                                          sp.XuatXu,
                                          sp.Hsd,
                                          sp.TrangThai,
                                          l.TenLoai,
                                          th.TenThuongHieu,
                                          ISNULL(kh.SoLuong, 0) AS SoLuong
                                   FROM dbo.Tbl_SanPham sp
                                   LEFT JOIN dbo.Tbl_DonVi dv ON sp.MaDonVi = dv.MaDonVi
                                   LEFT JOIN dbo.Tbl_Loai l ON sp.MaLoai = l.MaLoai
                                   LEFT JOIN dbo.Tbl_ThuongHieu th ON sp.MaThuongHieu = th.MaThuongHieu
                                   LEFT JOIN dbo.Tbl_KhoHang kh ON sp.MaSanPham = kh.MaSanPham";

            if (!string.IsNullOrWhiteSpace(trangThaiFilter))
            {
                command.CommandText += " WHERE sp.TrangThai = @TrangThai";
                command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
                {
                    Value = trangThaiFilter
                });
            }

            command.CommandText += " ORDER BY sp.MaSanPham";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int maSanPhamIndex = reader.GetOrdinal("MaSanPham");
                int tenSanPhamIndex = reader.GetOrdinal("TenSanPham");
                int maDonViIndex = reader.GetOrdinal("MaDonVi");
                int tenDonViIndex = reader.GetOrdinal("TenDonVi");
                int maThuongHieuIndex = reader.GetOrdinal("MaThuongHieu");
                int maLoaiIndex = reader.GetOrdinal("MaLoai");
                int moTaIndex = reader.GetOrdinal("MoTa");
                int giaBanIndex = reader.GetOrdinal("GiaBan");
                int hinhAnhIndex = reader.GetOrdinal("HinhAnh");
                int xuatXuIndex = reader.GetOrdinal("XuatXu");
                int hsdIndex = reader.GetOrdinal("Hsd");
                int trangThaiIndex = reader.GetOrdinal("TrangThai");
                int tenLoaiIndex = reader.GetOrdinal("TenLoai");
                int tenThuongHieuIndex = reader.GetOrdinal("TenThuongHieu");
                int soLuongIndex = reader.GetOrdinal("SoLuong");

                var sanPham = new SanPhamDTO
                {
                    MaSanPham = reader.GetInt32(maSanPhamIndex),
                    TenSanPham = reader.IsDBNull(tenSanPhamIndex) ? string.Empty : reader.GetString(tenSanPhamIndex),
                    MaDonVi = reader.IsDBNull(maDonViIndex) ? 0 : reader.GetInt32(maDonViIndex),
                    TenDonVi = reader.IsDBNull(tenDonViIndex) ? null : reader.GetString(tenDonViIndex),
                    MaThuongHieu = reader.IsDBNull(maThuongHieuIndex) ? 0 : reader.GetInt32(maThuongHieuIndex),
                    MaLoai = reader.IsDBNull(maLoaiIndex) ? 0 : reader.GetInt32(maLoaiIndex),
                    MoTa = reader.IsDBNull(moTaIndex) ? null : reader.GetString(moTaIndex),
                    GiaBan = reader.IsDBNull(giaBanIndex) ? null : reader.GetDecimal(giaBanIndex),
                    HinhAnh = reader.IsDBNull(hinhAnhIndex) ? null : reader.GetString(hinhAnhIndex),
                    XuatXu = reader.IsDBNull(xuatXuIndex) ? null : reader.GetString(xuatXuIndex),
                    Hsd = reader.IsDBNull(hsdIndex) ? null : reader.GetDateTime(hsdIndex),
                    TrangThai = reader.IsDBNull(trangThaiIndex) ? null : reader.GetString(trangThaiIndex),
                    TenLoai = reader.IsDBNull(tenLoaiIndex) ? null : reader.GetString(tenLoaiIndex),
                    TenThuongHieu = reader.IsDBNull(tenThuongHieuIndex) ? null : reader.GetString(tenThuongHieuIndex),
                    SoLuong = reader.IsDBNull(soLuongIndex) ? 0 : reader.GetInt32(soLuongIndex)
                };

                sanPhamList.Add(sanPham);
            }

            return sanPhamList;
        }

        public int InsertSanPham(SanPhamDTO sanPham)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Tbl_SanPham
                                        (TenSanPham, MaDonVi, MaThuongHieu, MaLoai, MoTa, GiaBan, HinhAnh, XuatXu, Hsd, TrangThai)
                                    VALUES
                                        (@TenSanPham, @MaDonVi, @MaThuongHieu, @MaLoai, @MoTa, @GiaBan, @HinhAnh, @XuatXu, @Hsd, @TrangThai);
                                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

            command.Parameters.Add(new SqlParameter("@TenSanPham", SqlDbType.NVarChar, 255)
            {
                Value = sanPham.TenSanPham
            });
            command.Parameters.Add(new SqlParameter("@MaDonVi", SqlDbType.Int)
            {
                Value = sanPham.MaDonVi
            });
            command.Parameters.Add(new SqlParameter("@MaThuongHieu", SqlDbType.Int)
            {
                Value = sanPham.MaThuongHieu
            });
            command.Parameters.Add(new SqlParameter("@MaLoai", SqlDbType.Int)
            {
                Value = sanPham.MaLoai
            });
            command.Parameters.Add(new SqlParameter("@MoTa", SqlDbType.NVarChar, -1)
            {
                Value = (object?)sanPham.MoTa ?? DBNull.Value
            });
            var giaBanParameter = new SqlParameter("@GiaBan", SqlDbType.Decimal)
            {
                Precision = 18,
                Scale = 2,
                Value = sanPham.GiaBan.HasValue ? sanPham.GiaBan.Value : DBNull.Value
            };
            command.Parameters.Add(giaBanParameter);
            command.Parameters.Add(new SqlParameter("@HinhAnh", SqlDbType.NVarChar, -1)
            {
                Value = (object?)sanPham.HinhAnh ?? DBNull.Value
            });
            command.Parameters.Add(new SqlParameter("@XuatXu", SqlDbType.NVarChar, 100)
            {
                Value = (object?)sanPham.XuatXu ?? DBNull.Value
            });
            command.Parameters.Add(new SqlParameter("@Hsd", SqlDbType.Date)
            {
                Value = sanPham.Hsd.HasValue ? sanPham.Hsd.Value.Date : DBNull.Value
            });
            command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
            {
                Value = sanPham.TrangThai ?? (object)DBNull.Value
            });

            connection.Open();
            var result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Failed to insert product.");
            }

            return Convert.ToInt32(result);
        }

        public IList<string> GetDistinctTrangThai()
        {
            var statuses = new List<string>();

            using var connection = DbConnectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT DISTINCT TrangThai
                                     FROM dbo.Tbl_SanPham
                                     WHERE TrangThai IS NOT NULL AND LTRIM(RTRIM(TrangThai)) <> ''
                                     ORDER BY TrangThai";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string status = reader.GetString(0).Trim();
                if (!string.IsNullOrEmpty(status))
                {
                    statuses.Add(status);
                }
            }

            return statuses;
        }
    }
}
