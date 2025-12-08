using System;

namespace mini_supermarket.DTO
{
    public class LichSuThayDoiKhoDTO
    {
        private int _maLichSu;
        private int _maSanPham;
        private string? _tenSanPham;
        private int _soLuongCu;
        private int _soLuongMoi;
        private int _chenhLech;
        private string _loaiThayDoi = string.Empty;
        private string? _lyDo;
        private string? _ghiChu;
        private int _maNhanVien;
        private string? _tenNhanVien;
        private DateTime _ngayThayDoi;

        public int MaLichSu
        {
            get => _maLichSu;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Mã lịch sử phải lớn hơn 0.", nameof(MaLichSu));
                _maLichSu = value;
            }
        }

        public int MaSanPham
        {
            get => _maSanPham;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Mã sản phẩm phải lớn hơn 0.", nameof(MaSanPham));
                _maSanPham = value;
            }
        }

        public string? TenSanPham
        {
            get => _tenSanPham;
            set => _tenSanPham = value;
        }

        public int SoLuongCu
        {
            get => _soLuongCu;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Số lượng không được âm.", nameof(SoLuongCu));
                _soLuongCu = value;
            }
        }

        public int SoLuongMoi
        {
            get => _soLuongMoi;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Số lượng mới không được âm.", nameof(SoLuongMoi));
                _soLuongMoi = value;
                // T? ??ng tính ChenhLech
                _chenhLech = value - _soLuongCu;
            }
        }

        public int ChenhLech
        {
            get => _chenhLech;
            set
            {
                // Validation: ChenhLech ph?i b?ng SoLuongMoi - SoLuongCu
                if (value != _soLuongMoi - _soLuongCu)
                    throw new ArgumentException("Chênh lệch phải bằng số lượng mới trừ số lượng cũ.", nameof(ChenhLech));
                _chenhLech = value;
            }
        }

        public string LoaiThayDoi
        {
            get => _loaiThayDoi;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Loại thay đổi không được trùng.", nameof(LoaiThayDoi));
                _loaiThayDoi = value;
            }
        }

        public string? LyDo
        {
            get => _lyDo;
            set => _lyDo = value;
        }

        public string? GhiChu
        {
            get => _ghiChu;
            set => _ghiChu = value;
        }

        public int MaNhanVien
        {
            get => _maNhanVien;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Mã nhân viên phải lớn hơn 0.", nameof(MaNhanVien));
                _maNhanVien = value;
            }
        }

        public string? TenNhanVien
        {
            get => _tenNhanVien;
            set => _tenNhanVien = value;
        }

        public DateTime NgayThayDoi
        {
            get => _ngayThayDoi;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Ngày thay đổi không được trong tương lai.", nameof(NgayThayDoi));
                _ngayThayDoi = value;
            }
        }

        public LichSuThayDoiKhoDTO()
        {
            _ngayThayDoi = DateTime.Now;
        }

        public LichSuThayDoiKhoDTO(int maSanPham, int soLuongCu, int soLuongMoi, string loaiThayDoi, string? lyDo, string? ghiChu, int maNhanVien)
        {
            MaSanPham = maSanPham;
            SoLuongCu = soLuongCu;
            SoLuongMoi = soLuongMoi;
            ChenhLech = soLuongMoi - soLuongCu;
            LoaiThayDoi = loaiThayDoi;
            LyDo = lyDo;
            GhiChu = ghiChu;
            MaNhanVien = maNhanVien;
            NgayThayDoi = DateTime.Now;
        }
    }
}
