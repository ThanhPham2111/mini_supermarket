using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini_supermarket.DB
{
    // DbContext là bắt buộc khi dùng Entity Framework Core
    // Đây là cách đơn giản nhất có thể
    public class NhanVienDbContext : DbContext
    {
        private const string ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=mini_sp01;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        public DbSet<NhanVienEntity> TblNhanVien { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }

    // Entity class - map với bảng Tbl_NhanVien
    [Table("Tbl_NhanVien")]
    public class NhanVienEntity
    {
        [Key]
        [Column("MaNhanVien")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhanVien { get; set; }

        [Column("TenNhanVien")]
        public string TenNhanVien { get; set; } = string.Empty;

        [Column("GioiTinh")]
        public string? GioiTinh { get; set; }

        [Column("NgaySinh", TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [Column("SoDienThoai")]
        public string? SoDienThoai { get; set; }

        [Column("VaiTro")]
        public string? VaiTro { get; set; }

        [Column("TrangThai")]
        public string? TrangThai { get; set; }
    }
}

