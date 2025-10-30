using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace mini_supermarket.GUI.Style
{
    public class SearchBoxControl : Panel
    {
        private PictureBox icon;
        private TextBox textBox;
        private string iconPath;
        private Image loadedIcon;

        public TextBox InnerTextBox => textBox;
        public string IconPath
        {
            get => iconPath;
            set
            {
                iconPath = value;
                RefreshIcon();
            }
        }

        public SearchBoxControl()
        {
            // 📏 Kích thước & bố cục tổng thể
            this.Height = 35;
            this.Width = 300;
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            icon = new PictureBox
            {
                Size = new Size(20, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(8, (this.Height - 20) / 2),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            this.Controls.Add(icon);

            // 🧾 Ô nhập văn bản
            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Roboto", 11F),
                Location = new Point(35, 8),
                Width = this.Width - 45,
                PlaceholderText = "Nhập tên hoặc mã sản phẩm..."
            };
            this.Controls.Add(textBox);

            // 🌈 Hiệu ứng hover (bao gồm cả icon + textbox)
            EventHandler handleEnter = (_, __) => this.BackColor = Color.FromArgb(245, 245, 245);
            EventHandler handleLeave = (_, __) => this.BackColor = Color.White;

            this.MouseEnter += handleEnter;
            this.MouseLeave += handleLeave;
            icon.MouseEnter += handleEnter;
            icon.MouseLeave += handleLeave;
            textBox.MouseEnter += handleEnter;
            textBox.MouseLeave += handleLeave;

            RefreshIcon();
        }

        private void RefreshIcon()
        {
            loadedIcon?.Dispose();
            loadedIcon = LoadIconImage();
            icon.Image = loadedIcon;
        }

        private Image LoadIconImage()
        {
            foreach (string candidate in GetCandidateIconPaths())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(candidate) && File.Exists(candidate))
                    {
                        using FileStream fs = new FileStream(candidate, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        return Image.FromStream(fs);
                    }
                }
                catch (IOException)
                {
                    // Bỏ qua và thử đường dẫn khác
                }
            }

            return CreateFallbackIcon();
        }

        private IEnumerable<string> GetCandidateIconPaths()
        {
            if (!string.IsNullOrWhiteSpace(iconPath))
                yield return iconPath;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            yield return Path.Combine(baseDir, "img", "search_icon.png");

            DirectoryInfo? current = new DirectoryInfo(baseDir);
            for (int i = 0; i < 5 && current?.Parent != null; i++)
            {
                current = current.Parent;
                string candidate = Path.Combine(current.FullName, "img", "search_icon.png");
                yield return candidate;
            }
        }

        private static Image CreateFallbackIcon()
        {
            Bitmap bitmap = new Bitmap(20, 20);

            using (Graphics g = Graphics.FromImage(bitmap))
            using (Pen pen = new Pen(Color.Gray, 2f))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle circle = new Rectangle(2, 2, 12, 12);
                g.DrawEllipse(pen, circle);
                g.DrawLine(pen, new Point(12, 12), new Point(18, 18));
            }

            return bitmap;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                loadedIcon?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
