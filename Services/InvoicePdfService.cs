using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using mini_supermarket.DTO;

namespace mini_supermarket.Services
{
    public class InvoicePdfService
    {
        public void GenerateInvoicePdf(
            HoaDonDTO hoaDon,
            List<ChiTietHoaDonDTO> chiTietHoaDon,
            string tenNhanVien,
            string tenKhachHang,
            decimal tongTienTruocDiem,
            decimal giamTuDiem,
            int diemTichLuy,
            int diemSuDung)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            string fileName = $"HoaDon_{hoaDon.MaHoaDonCode}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HoaDon");
            
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);

            decimal tongTien = hoaDon.TongTien ?? 0;
            decimal thanhTien = tongTien;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(1.2f, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(8));

                    page.Content()
                        .PaddingVertical(0.5f, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(12);

                            // Header chỉ hiển thị một lần ở đầu (không lặp lại)
                            column.Item()
                                .PaddingBottom(8)
                                .AlignCenter()
                                .Text("HÓA ĐƠN BÁN HÀNG")
                                .FontSize(16)
                                .Bold()
                                .FontFamily(Fonts.Calibri);

                            // Thông tin cửa hàng và hóa đơn
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text("MINI SUPERMARKET").Bold().FontSize(11);
                                    col.Item().Text("Địa chỉ: 123 Đường ABC, Quận XYZ, TP.HCM").FontSize(8);
                                    col.Item().Text("Điện thoại: 0123 456 789").FontSize(8);
                                    col.Item().Text("Email: info@minisupermarket.com").FontSize(8);
                                });

                                row.RelativeItem().AlignRight().Column(col =>
                                {
                                    col.Item().Text($"Mã HĐ: {hoaDon.MaHoaDonCode}").Bold().FontSize(8);
                                    col.Item().Text($"Ngày: {hoaDon.NgayLap:dd/MM/yyyy HH:mm}").FontSize(8);
                                    col.Item().Text($"Nhân viên: {tenNhanVien}").FontSize(8);
                                });
                            });

                            column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                            // Thông tin khách hàng
                            column.Item().Column(col =>
                            {
                                col.Item().Text("THÔNG TIN KHÁCH HÀNG").Bold().FontSize(10);
                                col.Item().Text($"Tên khách hàng: {tenKhachHang}").FontSize(8);
                            });

                            column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                            // Chi tiết sản phẩm
                            column.Item().Column(col =>
                            {
                                col.Item().Text("CHI TIẾT HÓA ĐƠN").Bold().FontSize(10);
                                col.Item().PaddingTop(5);

                                // Header bảng
                                col.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(3); // Tên sản phẩm
                                        columns.RelativeColumn(1); // Đơn vị
                                        columns.RelativeColumn(1); // Số lượng
                                        columns.RelativeColumn(1.5f); // Đơn giá
                                        columns.RelativeColumn(1.5f); // Thành tiền
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Element(CellStyle).Text("Tên sản phẩm").Bold().FontSize(8);
                                        header.Cell().Element(CellStyle).Text("ĐVT").Bold().FontSize(8);
                                        header.Cell().Element(CellStyle).AlignRight().Text("SL").Bold().FontSize(8);
                                        header.Cell().Element(CellStyle).AlignRight().Text("Đơn giá").Bold().FontSize(8);
                                        header.Cell().Element(CellStyle).AlignRight().Text("Thành tiền").Bold().FontSize(8);
                                    });

                                    foreach (var item in chiTietHoaDon)
                                    {
                                        table.Cell().Element(CellStyle).Text(item.TenSanPham).FontSize(8);
                                        table.Cell().Element(CellStyle).Text(item.DonVi).FontSize(8);
                                        table.Cell().Element(CellStyle).AlignRight().Text(item.SoLuong.ToString()).FontSize(8);
                                        table.Cell().Element(CellStyle).AlignRight().Text($"{item.GiaBan:N0} đ").FontSize(8);
                                        table.Cell().Element(CellStyle).AlignRight().Text($"{item.ThanhTien:N0} đ").FontSize(8);
                                    }
                                });
                            });

                            column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                            // Tổng tiền
                            column.Item().AlignRight().Column(col =>
                            {
                                col.Item().Row(row =>
                                {
                                    row.ConstantItem(80).Text("Tổng tiền:").FontSize(8);
                                    row.RelativeItem().AlignRight().Text($"{tongTienTruocDiem:N0} đ").FontSize(8);
                                });

                                if (diemSuDung > 0)
                                {
                                    col.Item().Row(row =>
                                    {
                                        row.ConstantItem(80).Text("Giảm từ điểm:").FontSize(8);
                                        row.RelativeItem().AlignRight().Text($"-{giamTuDiem:N0} đ").FontSize(8);
                                    });
                                }

                                col.Item().PaddingTop(3);
                                col.Item().Row(row =>
                                {
                                    row.ConstantItem(80).Text("TỔNG CỘNG:").Bold().FontSize(10);
                                    row.RelativeItem().AlignRight().Text($"{thanhTien:N0} đ").Bold().FontSize(10);
                                });
                            });

                            // Footer
                            column.Item().PaddingTop(15);
                            column.Item().AlignCenter().Text("Cảm ơn quý khách đã mua hàng!").Italic().FontSize(8);
                            column.Item().AlignCenter().Text("Hẹn gặp lại quý khách!").Italic().FontSize(8);
                        });
                });
            })
            .GeneratePdf(filePath);

            // Mở file PDF sau khi tạo
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }

        private IContainer CellStyle(IContainer container)
        {
            return container
                .BorderBottom(0.5f)
                .BorderColor(Colors.Grey.Lighten2)
                .PaddingVertical(3)
                .PaddingHorizontal(4);
        }
    }
}

