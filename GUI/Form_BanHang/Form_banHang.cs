using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using mini_supermarket.DB;
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
                using (SqlConnection connection = DbConnectionFactory.CreateConnection())
                {
                    connection.Open();
                    // Uu tiên dúng b?ng theo DB c?a b?n
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
                            // Th? query ti?p theo
                        }
                    }

                    throw new Exception("Không tìm th?y b?ng tài kho?n: hãy ki?m tra dbo.Tbl_TaiKhoan/TaiKhoan/Account.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i k?t n?i CSDL: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


