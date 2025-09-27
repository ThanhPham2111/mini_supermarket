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
            command.CommandText = @"SELECT sp.MaSanPham, sp.TenSanPham, sp.DonVi, sp.MaThuongHieu, sp.MaLoai, sp.MoTa, sp.GiaBan, sp.XuatXu, sp.Hsd, sp.TrangThai,
                                          l.TenLoai, th.TenThuongHieu
                                   FROM dbo.Tbl_SanPham sp
                                   LEFT JOIN dbo.Tbl_Loai l ON sp.MaLoai = l.MaLoai
                                   LEFT JOIN dbo.Tbl_ThuongHieu th ON sp.MaThuongHieu = th.MaThuongHieu";

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
                int donViIndex = reader.GetOrdinal("DonVi");
                int maThuongHieuIndex = reader.GetOrdinal("MaThuongHieu");
                int maLoaiIndex = reader.GetOrdinal("MaLoai");
                int moTaIndex = reader.GetOrdinal("MoTa");
                int giaBanIndex = reader.GetOrdinal("GiaBan");
                int xuatXuIndex = reader.GetOrdinal("XuatXu");
                int hsdIndex = reader.GetOrdinal("Hsd");
                int trangThaiIndex = reader.GetOrdinal("TrangThai");
                int tenLoaiIndex = reader.GetOrdinal("TenLoai");
                int tenThuongHieuIndex = reader.GetOrdinal("TenThuongHieu");

                var sanPham = new SanPhamDTO
                {
                    MaSanPham = reader.GetInt32(maSanPhamIndex),
                    TenSanPham = reader.IsDBNull(tenSanPhamIndex) ? string.Empty : reader.GetString(tenSanPhamIndex),
                    DonVi = reader.IsDBNull(donViIndex) ? null : reader.GetString(donViIndex),
                    MaThuongHieu = reader.IsDBNull(maThuongHieuIndex) ? 0 : reader.GetInt32(maThuongHieuIndex),
                    MaLoai = reader.IsDBNull(maLoaiIndex) ? 0 : reader.GetInt32(maLoaiIndex),
                    MoTa = reader.IsDBNull(moTaIndex) ? null : reader.GetString(moTaIndex),
                    GiaBan = reader.IsDBNull(giaBanIndex) ? null : reader.GetDecimal(giaBanIndex),
                    XuatXu = reader.IsDBNull(xuatXuIndex) ? null : reader.GetString(xuatXuIndex),
                    Hsd = reader.IsDBNull(hsdIndex) ? null : reader.GetDateTime(hsdIndex),
                    TrangThai = reader.IsDBNull(trangThaiIndex) ? null : reader.GetString(trangThaiIndex),
                    TenLoai = reader.IsDBNull(tenLoaiIndex) ? null : reader.GetString(tenLoaiIndex),
                    TenThuongHieu = reader.IsDBNull(tenThuongHieuIndex) ? null : reader.GetString(tenThuongHieuIndex)
                };

                sanPhamList.Add(sanPham);
            }

            return sanPhamList;
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