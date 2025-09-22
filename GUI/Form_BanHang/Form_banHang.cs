using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mini_supermarket.GUI.Form_BanHang
{
    public partial class Form_banHang : Form
    {
        public Form_banHang()
        {
            InitializeComponent();
            LoadAccountList();
        }

        void LoadAccountList()
        {
            try
            {
                string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=project_c;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Ưu tiên đúng bảng theo DB của bạn
                    string[] queries = new[]
                    {
                        "SELECT MaTaiKhoan, TenDangNhap, MatKhau, MaNhanVien, MaQuyen, TrangThai FROM dbo.Tbl_TaiKhoan",
                        "SELECT * FROM TaiKhoan",

                    };

                    foreach (string query in queries)
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand(query, connection))
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable data = new DataTable();
                                adapter.Fill(data);
                                if (data.Rows.Count > 0 || query.Contains("Tbl_TaiKhoan"))
                                {
                                    dataGridView1.DataSource = data;
                                    return;
                                }
                            }
                        }
                        catch (SqlException)
                        {
                            // Thử query tiếp theo
                        }
                    }

                    throw new Exception("Không tìm thấy bảng tài khoản: hãy kiểm tra dbo.Tbl_TaiKhoan/TaiKhoan/Account.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối CSDL: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
