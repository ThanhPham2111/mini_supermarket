using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
using System;
using System.Data;

namespace mini_supermarket.DAO
{
    public class KhoHangDAO
    {
        // Lấy toàn bộ sản phẩm trong kho để hiển thị
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

            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] LayDanhSachTonKho: {ex.Message}");
            }
            return data;
        }

        // Lấy danh sách Loại sản phẩm để lọc
        public DataTable LayDanhSachLoai()
        {
            string query = "SELECT MaLoai, TenLoai FROM Tbl_Loai;";
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] LayDanhSachLoai: {ex.Message}");
            }
            return data;
        }

        // Lấy danh sách Thương hiệu để lọc
        public DataTable LayDanhSachThuongHieu()
        {
            string query = "SELECT MaThuongHieu, TenThuongHieu FROM Tbl_ThuongHieu;";
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] LayDanhSachThuongHieu: {ex.Message}");
            }
            return data;
        }

        // Lấy danh sách Nhà cung cấp để lọc 
        public DataTable LayDanhSachNhaCungCap()
        {
            string query = "SELECT MaNhaCungCap, TenNhaCungCap FROM Tbl_NhaCungCap;";
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] LayDanhSachNhaCungCap: {ex.Message}");
            }
            return data;
        }

        // Lấy chi tiết lịch sử nhập/xuất của một sản phẩm
        public DataTable LayLichSuNhapXuat(int maSanPham)
        {
            string query = @"
                SELECT 
                    pn.NgayNhap AS 'NgayGiaoDich',
                    N'Nhập kho' AS 'LoaiGiaoDich',
                    ctpn.SoLuong,
                    ctpn.DonGiaNhap AS 'DonGia',
                    pn.MaPhieuNhap AS 'MaPhieu'
                FROM Tbl_ChiTietPhieuNhap ctpn
                JOIN Tbl_PhieuNhap pn ON ctpn.MaPhieuNhap = pn.MaPhieuNhap
                WHERE ctpn.MaSanPham = @MaSanPham

                UNION ALL

                SELECT 
                    hd.NgayLap AS 'NgayGiaoDich',
                    N'Bán hàng' AS 'LoaiGiaoDich',
                    cthd.SoLuong,
                    cthd.GiaBan AS 'DonGia',
                    hd.MaHoaDon AS 'MaPhieu'
                FROM Tbl_ChiTietHoaDon cthd
                JOIN Tbl_HoaDon hd ON cthd.MaHoaDon = hd.MaHoaDon
                WHERE cthd.MaSanPham = @MaSanPham
                ORDER BY NgayGiaoDich DESC;";

            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] LayLichSuNhapXuat: {ex.Message}");
            }
            return data;
        }
    }
}

