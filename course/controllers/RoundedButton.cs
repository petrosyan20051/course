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
        private int borderRadius = 10;

        public int BorderRadius {
            get { return borderRadius; }
            set {
                if (value >= 0)
                    borderRadius = value;
                this.Invalidate(); // Перерисовать кнопку при изменении радиуса
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            // Создаем путь для закругленных углов
            GraphicsPath path = new GraphicsPath();
            int width = this.Width;
            int height = this.Height;

            // Добавляем закругленный прямоугольник в путь
            path.AddArc(0, 0, borderRadius * 2, borderRadius * 2, 180, 90); // Левый верхний угол
            path.AddArc(width - borderRadius * 2, 0, borderRadius * 2, borderRadius * 2, 270, 90); // Правый верхний угол
            path.AddArc(width - borderRadius * 2, height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90); // Правый нижний угол
            path.AddArc(0, height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90); // Левый нижний угол
            path.CloseFigure();

            // Устанавливаем область отрисовки кнопки
            this.Region = new Region(path);

            // Отрисовываем текст кнопки
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
