using System;
using System.Text.RegularExpressions;

namespace mini_supermarket.GUI.KhachHang;

public class Validation_Component
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Biểu thức chính quy chuẩn (tương đối) để kiểm tra định dạng email cơ bản
        // Lưu ý: Không có regex nào có thể kiểm tra *tuyệt đối* tất cả các email hợp lệ
        // theo chuẩn RFC, nhưng đây là một mẫu được sử dụng phổ biến và an toàn.
        string regexPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        try
        {
            return Regex.IsMatch(email, regexPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            // Xử lý khi quá thời gian kiểm tra (hiếm gặp, nhưng cần thiết để phòng tránh DoS)
            return false;
        }
    }

    public static bool IsValidNumberPhone(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Chuẩn hóa chuỗi (loại bỏ khoảng trắng, dấu gạch ngang, dấu ngoặc)
        // để kiểm tra regex chính xác hơn.
        string normalizedNumber = Regex.Replace(phoneNumber, @"[\s\-\(\)]", "");

        // Regex cơ bản: 0[3|5|7|8|9](\d{8,9})$
        // Hoặc regex bao quát hơn (chấp nhận 10-11 chữ số, bắt đầu bằng 0)
        string regexPattern = @"^0[23789]\d{8,9}$";

        try
        {
            // Kiểm tra sau khi đã chuẩn hóa chuỗi
            return Regex.IsMatch(normalizedNumber, regexPattern);
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool IsValidNumber(string num_str)
    {
        try
        {
            int n = int.Parse(num_str);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
        return true;
    }
}