using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using mini_supermarket.DAO;
using mini_supermarket.DTO;

namespace mini_supermarket.GUI.NhanVien
{
    public partial class Form_NhanVien : Form
    {
        private const string StatusAll = "Tất cả";
        private const string StatusWorking = "Đang làm";
        private const string StatusInactive = "Đã nghỉ";

        private static readonly CompareInfo VietnameseCompare = CultureInfo.GetCultureInfo("vi-VN").CompareInfo;

        private readonly NhanVien_DAO _nhanVienDao = new();
        private readonly BindingSource _bindingSource = new();
        private IList<NhanVienDTO> _currentNhanVien = Array.Empty<NhanVienDTO>();

        public Form_NhanVien()
        {
            InitializeComponent();
            Load += Form_NhanVien_Load;
        }

        private void Form_NhanVien_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.AddRange(new object[]
            {
                StatusAll,
                StatusWorking,
                StatusInactive
            });

            statusFilterComboBox.SelectedIndexChanged += statusFilterComboBox_SelectedIndexChanged;
            statusFilterComboBox.SelectedIndex = 0;

            nhanVienDataGridView.AutoGenerateColumns = false;
            nhanVienDataGridView.DataSource = _bindingSource;

            LoadNhanVienData();
        }

        private void statusFilterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyStatusFilter();
        }

        private void LoadNhanVienData()
        {
            try
            {
                _currentNhanVien = _nhanVienDao.GetNhanVien();
                ApplyStatusFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Không thể tải danh sách nhân viên.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStatusFilter()
        {
            string? selectedStatus = statusFilterComboBox.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(selectedStatus) || string.Equals(selectedStatus, StatusAll, StringComparison.OrdinalIgnoreCase))
            {
                _bindingSource.DataSource = _currentNhanVien;
                return;
            }

            var filtered = new List<NhanVienDTO>();

            foreach (var nhanVien in _currentNhanVien)
            {
                if (VietnameseCompare.Compare(nhanVien.TrangThai, selectedStatus, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) == 0)
                {
                    filtered.Add(nhanVien);
                }
            }

            _bindingSource.DataSource = filtered;
        }
    }
}


