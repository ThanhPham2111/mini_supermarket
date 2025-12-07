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
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .AlignCenter()
                        .Text("HÓA ĐƠN BÁN HÀNG")
                        .FontSize(20)
                        .Bold()
                        .FontFamily(Fonts.Calibri);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(20);

                            // Thông tin cửa hàng và hóa đơn
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text("MINI SUPERMARKET").Bold().FontSize(14);
                                    col.Item().Text("Địa chỉ: 123 Đường ABC, Quận XYZ, TP.HCM");
                                    col.Item().Text("Điện thoại: 0123 456 789");
                                    col.Item().Text("Email: info@minisupermarket.com");
                                });

                                row.RelativeItem().AlignRight().Column(col =>
                                {
                                    col.Item().Text($"Mã HĐ: {hoaDon.MaHoaDonCode}").Bold();
                                    col.Item().Text($"Ngày: {hoaDon.NgayLap:dd/MM/yyyy HH:mm}");
                                    col.Item().Text($"Nhân viên: {tenNhanVien}");
                                });
                            });

                            column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                            // Thông tin khách hàng
                            column.Item().Column(col =>
                            {
                                col.Item().Text("THÔNG TIN KHÁCH HÀNG").Bold().FontSize(12);
                                col.Item().Text($"Tên khách hàng: {tenKhachHang}");
                            });

                            column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                            // Chi tiết sản phẩm
                            column.Item().Column(col =>
                            {
                                col.Item().Text("CHI TIẾT HÓA ĐƠN").Bold().FontSize(12);
                                col.Item().PaddingTop(10);

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
                                        header.Cell().Element(CellStyle).Text("Tên sản phẩm").Bold();
                                        header.Cell().Element(CellStyle).Text("ĐVT").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("SL").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("Đơn giá").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("Thành tiền").Bold();
                                    });

                                    foreach (var item in chiTietHoaDon)
                                    {
                                        table.Cell().Element(CellStyle).Text(item.TenSanPham);
                                        table.Cell().Element(CellStyle).Text(item.DonVi);
                                        table.Cell().Element(CellStyle).AlignRight().Text(item.SoLuong.ToString());
                                        table.Cell().Element(CellStyle).AlignRight().Text($"{item.GiaBan:N0} đ");
                                        table.Cell().Element(CellStyle).AlignRight().Text($"{item.ThanhTien:N0} đ");
                                    }
                                });
                            });

                            column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                            // Tổng tiền
                            column.Item().AlignRight().Column(col =>
                            {
                                col.Item().Row(row =>
                                {
                                    row.ConstantItem(100).Text("Tổng tiền:");
                                    row.RelativeItem().AlignRight().Text($"{tongTienTruocDiem:N0} đ");
                                });

                                if (diemSuDung > 0)
                                {
                                    col.Item().Row(row =>
                                    {
                                        row.ConstantItem(100).Text("Giảm từ điểm:");
                                        row.RelativeItem().AlignRight().Text($"-{giamTuDiem:N0} đ");
                                    });
                                }

                                col.Item().PaddingTop(5);
                                col.Item().Row(row =>
                                {
                                    row.ConstantItem(100).Text("TỔNG CỘNG:").Bold().FontSize(12);
                                    row.RelativeItem().AlignRight().Text($"{thanhTien:N0} đ").Bold().FontSize(12);
                                });
                            });

                            // Thông tin điểm tích lũy (nếu có)
                            if (diemTichLuy > 0 || diemSuDung > 0)
                            {
                                column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);
                                column.Item().Column(col =>
                                {
                                    col.Item().Text("THÔNG TIN ĐIỂM TÍCH LŨY").Bold().FontSize(12);
                                    if (diemSuDung > 0)
                                    {
                                        col.Item().Text($"Điểm đã sử dụng: {diemSuDung} điểm");
                                    }
                                    if (diemTichLuy > 0)
                                    {
                                        col.Item().Text($"Điểm tích lũy: +{diemTichLuy} điểm");
                                    }
                                });
                            }

                            // Footer
                            column.Item().PaddingTop(30);
                            column.Item().AlignCenter().Text("Cảm ơn quý khách đã mua hàng!").Italic();
                            column.Item().AlignCenter().Text("Hẹn gặp lại quý khách!").Italic();
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
                .PaddingVertical(5)
                .PaddingHorizontal(5);
        }
    }
}

