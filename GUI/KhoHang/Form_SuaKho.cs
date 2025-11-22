using mini_supermarket.BUS;
using mini_supermarket.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace mini_supermarket.GUI.KhoHang
{
    public partial class Form_SuaKho : Form
    {
        private KhoHangBUS khoHangBUS = new KhoHangBUS();
        private int maSanPham;
        private int soLuongHienTai;
        private int maNhanVien;
        public bool IsUpdated { get; private set; } = false;

        // Dictionary để mapping loại thay đổi với lý do tương ứng
        private Dictionary<string, string[]> lyDoTheoLoai = new Dictionary<string, string[]>
        {
            { "Kiểm kê", new[] { "Kiểm kê định kỳ", "Kiểm kê đột xuất", "Kiểm kê cuối năm" } },
            { "Điều chỉnh", new[] { "Sai sót nhập liệu", "Điều chỉnh số liệu", "Cập nhật thực tế" } },
            { "Hủy hàng", new[] { "Hàng hư hỏng", "Hàng hết hạn", "Hàng mất mát", "Hàng bị lỗi" } }
        };

        public Form_SuaKho(int maSanPham, string tenSanPham, int soLuong, int maNhanVien)
        {
            InitializeComponent();
            
            // SET TEXT TIẾNG VIỆT TẠI ĐÂY - Fix encoding issue
            this.Text = "Điều Chỉnh Số Lượng Tồn Kho";
            lblTitle.Text = "ĐIỀU CHỈNH SỐ LƯỢNG TỒN KHO";
            lblSanPham.Text = "Sản phẩm:";
            lblSoLuongHienTai.Text = "Số lượng hiện tại:";
            lblSoLuongMoi.Text = "Số lượng mới:";
            lblChenhLech.Text = "Chênh lệch:";
            lblLoaiThayDoi.Text = "Loại thay đổi:";
            lblLyDo.Text = "Lý do:";
            lblGhiChu.Text = "Ghi chú:";
            btnHuy.Text = "Hủy";
            btnLuu.Text = "Lưu";
            
            this.maSanPham = maSanPham;
            this.soLuongHienTai = soLuong;
            this.maNhanVien = maNhanVien;

            txtSanPham.Text = tenSanPham;
            txtSoLuongHienTai.Text = soLuong.ToString();
            numSoLuongMoi.Value = soLuong;
            
            // Mặc định chọn Kiểm kê
            cboLoaiThayDoi.SelectedIndex = 0;
            
            TinhChenhLech();
            CapNhatGioiHanSoLuongTheoLoai();
        }

        private void cboLoaiThayDoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load lý do theo loại thay đổi
            string loaiThayDoi = cboLoaiThayDoi.SelectedItem?.ToString() ?? "Kiểm kê";
            
            cboLyDo.Items.Clear();
            if (lyDoTheoLoai.ContainsKey(loaiThayDoi))
            {
                cboLyDo.Items.AddRange(lyDoTheoLoai[loaiThayDoi]);
                if (cboLyDo.Items.Count > 0)
                {
                    cboLyDo.SelectedIndex = 0;
                }
            }

            // Điều chỉnh giới hạn số lượng theo loại thay đổi
            CapNhatGioiHanSoLuongTheoLoai();
        }

        private void CapNhatGioiHanSoLuongTheoLoai()
        {
            string loaiThayDoi = cboLoaiThayDoi.SelectedItem?.ToString() ?? "Kiểm kê";
            if (loaiThayDoi == "Hủy hàng")
            {
                // Hủy hàng: chỉ được giảm số lượng, giá trị mới không vượt quá hiện tại
                numSoLuongMoi.Maximum = soLuongHienTai;
                if (numSoLuongMoi.Value > soLuongHienTai)
                {
                    numSoLuongMoi.Value = soLuongHienTai;
                }
            }
            else
            {
                // Các loại khác: cho phép tăng/giảm tự do trong khoảng hợp lý
                numSoLuongMoi.Maximum = 999999;
            }
        }

        private void numSoLuongMoi_ValueChanged(object sender, EventArgs e)
        {
            TinhChenhLech();
        }

        private void TinhChenhLech()
        {
            int chenhLech = (int)numSoLuongMoi.Value - soLuongHienTai;
            txtChenhLech.Text = chenhLech.ToString();

            if (chenhLech > 0)
            {
                txtChenhLech.ForeColor = Color.Green;
                txtChenhLech.Text = "+" + chenhLech;
            }
            else if (chenhLech < 0)
            {
                txtChenhLech.ForeColor = Color.Red;
            }
            else
            {
                txtChenhLech.ForeColor = Color.Black;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validation cơ bản
            if (numSoLuongMoi.Value == soLuongHienTai)
            {
                MessageBox.Show("Số lượng mới giống số lượng hiện tại!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(cboLoaiThayDoi.Text))
            {
                MessageBox.Show("Vui lòng chọn loại thay đổi!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiThayDoi.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cboLyDo.Text))
            {
                MessageBox.Show("Vui lòng chọn lý do!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLyDo.Focus();
                return;
            }

            // Validation nghiệp vụ theo loại thay đổi
            int chenhLech = (int)numSoLuongMoi.Value - soLuongHienTai;
            string loaiThayDoi = cboLoaiThayDoi.Text;

            if (loaiThayDoi == "Hủy hàng" && chenhLech > 0)
            {
                MessageBox.Show("Loại thay đổi 'Hủy hàng' chỉ áp dụng khi giảm số lượng.", "Không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận
            string message = $"Bạn có chắc chắn muốn điều chỉnh số lượng?\n\n" +
                           $"Sản phẩm: {txtSanPham.Text}\n" +
                           $"Số lượng: {soLuongHienTai} → {numSoLuongMoi.Value}\n" +
                           $"Chênh lệch: {(chenhLech > 0 ? "+" : "")}{chenhLech}\n" +
                           $"Loại: {loaiThayDoi}\n" +
                           $"Lý do: {cboLyDo.Text}";

            DialogResult result = MessageBox.Show(message, "Xác nhận điều chỉnh", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Tạo DTO KhoHang
                    KhoHangDTO khoHang = new KhoHangDTO
                    {
                        MaSanPham = maSanPham,
                        SoLuong = (int)numSoLuongMoi.Value,
                        TrangThai = (int)numSoLuongMoi.Value > 0 ? "Còn hàng" : "Hết hàng"
                    };

                    // Tạo DTO Lịch sử
                    LichSuThayDoiKhoDTO lichSu = new LichSuThayDoiKhoDTO(
                        maSanPham,
                        soLuongHienTai,
                        (int)numSoLuongMoi.Value,
                        loaiThayDoi,
                        cboLyDo.Text,
                        txtGhiChu.Text,
                        maNhanVien
                    );

                    // Cập nhật + Ghi log
                    bool success = khoHangBUS.CapNhatSoLuongKho(khoHang, lichSu);

                    if (success)
                    {
                        MessageBox.Show("Điều chỉnh số lượng tồn kho thành công!\nĐã ghi nhận lịch sử thay đổi.", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        IsUpdated = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi điều chỉnh số lượng!", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
