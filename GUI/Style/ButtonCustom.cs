using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace mini_supermarket.GUI.Style
{
    public class CustomButton : Button
    {
        private int borderRadius = 10;

        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                borderRadius = value;
                Invalidate();
            }
        }

        public CustomButton()
        {
            // Đặt con trỏ thành bàn tay (pointer) khi hover
            this.Cursor = Cursors.Hand;

            // Đảm bảo FlatStyle là Flat để hiệu ứng hover hoạt động mượt mà
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
                int radius = borderRadius * 2;
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                Region = new Region(path);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Region = new Region(CreateRoundRect());
        }

        private GraphicsPath CreateRoundRect()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int radius = borderRadius * 2;
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}