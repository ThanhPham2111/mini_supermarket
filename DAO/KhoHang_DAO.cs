using mini_supermarket.DB;
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
    }
}

