using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace course.controllers {

    public partial class RoundedButton : Button {

        // Радиус закругления углов
        private int borderRadius { set; get; } = 5;

        public int BorderRadius {
            get { return borderRadius; }
            set {
                if (value >= 0)
                    borderRadius = value;
                this.Invalidate(); // Перерисовать кнопку при изменении радиуса
            }
        }

        public RoundedButton() {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }

        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.Clear(this.BackColor);

            // Make path for rounded angles
            GraphicsPath path = new GraphicsPath();
            int width = this.Width;
            int height = this.Height;

            // Add rounded rectangle into path
            path.AddArc(0, 0, borderRadius * 2, borderRadius * 2, 180, 90); // Left-up corner
            path.AddArc(width - borderRadius * 2, 0, borderRadius * 2, borderRadius * 2, 270, 90); // Right-up corner
            path.AddArc(width - borderRadius * 2, height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90); // Right-down corner
            path.AddArc(0, height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90); // Left-down corner
            path.CloseFigure();

            // Make area for path
            this.Region = new Region(path);

            // Draw button text
            using (SolidBrush brush = new SolidBrush(this.ForeColor)) {
                StringFormat format = new StringFormat {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                e.Graphics.DrawString(this.Text, this.Font, brush, this.ClientRectangle, format);
            }

            base.OnPaint(e);
        }
    }
}