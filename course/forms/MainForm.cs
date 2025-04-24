using System;
using System.Drawing;
using System.Windows.Forms;
using course.classes;

namespace course {

    public partial class MainForm : Form {
        private bool isDragging = false; // форма находится в состоянии перемещения
        private bool isResizing = false; // форма находится в состоянии изменения размера

        private Point startPoint; // точка начала перемещения

        private Button _closeBtn, _rollBtn, _expandBtn;
        private Panel _controlPnl, _menuPnl;
        private Splitter _menuSpl, _controlSpl;
        private FormWindowState formWindowState = FormWindowState.Normal; // текущее состояние формы

        public MainForm() {
            InitializeComponent();
        }

        private void InitVariables() {
            _closeBtn = this.closeBtn;
            _rollBtn = this.rollBtn;
            _expandBtn = this.expandBtn;
            _controlPnl = this.controlPnl;
            _menuPnl = this.menuPnl;
            _menuSpl = this.menuSpl;
            _controlSpl = this.controlSpl;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            this.InitVariables();

            _controlPnl.BackColor = Design.ControlPanelDefaultColor;
            _menuPnl.BackColor = Design.MenuPanelDefaultColor;

            _closeBtn.BackColor = Design.ControlPanelDefaultColor;
            _expandBtn.BackColor = Design.ControlPanelDefaultColor;
            _rollBtn.BackColor = Design.ControlPanelDefaultColor;

            _menuSpl.BackColor = Design.SplitterDefaulColor;
            _controlSpl.BackColor = Design.SplitterDefaulColor;

            (sender as Form).BackColor = Design.FormDefaultColor;
        }

        private void closeBtn_MouseClick(object sender, MouseEventArgs e) {
            Application.Exit();
        }

        private void closeBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ButtonCloseEnterColor;
        }

        private void closeBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ControlPanelDefaultColor;
        }

        private void rollBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ControlPanelDefaultColor;
        }

        private void rollBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ButtonRollEnterColor;
        }

        private void expandBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ButtonExpandEnterColor;
        }

        private void expandBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ControlPanelDefaultColor;
        }

        private void controlPnl_MouseDown(object sender, MouseEventArgs e) {
            // Щелчок ЛКМ по панели порождает перемещение формы
            if (e.Button == MouseButtons.Left) {
                isDragging = true; // начинаем перемещение
                startPoint = new Point(e.X, e.Y);
            }
        }

        private void controlPnl_MouseUp(object sender, MouseEventArgs e) {
            isDragging = false; // Отпускание ЛКМ останавливает перемещение формы
        }

        private void controlPnl_MouseMove(object sender, MouseEventArgs e) {
            if (isDragging) { // Перемещаем форму
                Point newPoint = this.PointToScreen(new Point(e.X, e.Y)); // новая точка, куда перемещаем
                newPoint.Offset(-startPoint.X, -startPoint.Y); // смещение новой точки относительно старой
                this.Location = newPoint;
            }

            // Переход с формы на панель в крайнем правом и левом положении должен сбрасывать иконку курсора
            if (e.X >= this.Width - 5 || e.X <= 5) {
                this.Cursor = Cursors.Default;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e) {
            // Изменение значка курсора при масштабировании
            if (isResizing) {
                if (this.Cursor == Cursors.SizeWE) { 
                    this.Size = new Size(
                        Math.Max(this.MinimumSize.Width, e.X),
                        this.Size.Height
                    );
                }
                
                

                // Обновляем позиции кнопок управления
                _closeBtn.Location = new Point(this.Width - 65, _closeBtn.Location.Y);
                _expandBtn.Location = new Point(this.Width - 144, _expandBtn.Location.Y);
                _rollBtn.Location = new Point(this.Width - 222, _rollBtn.Location.Y);
            } else {
                if ((e.X >= this.ClientSize.Width - 10 || e.X <= 10) &&
                    e.Y <= this.ClientSize.Height - _controlPnl.Height - 5) {
                    this.Cursor = Cursors.SizeWE;
                } else {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void expandBtn_Click(object sender, EventArgs e) {
            this.WindowState = this.WindowState == FormWindowState.Maximized ?
                FormWindowState.Normal :
                FormWindowState.Maximized;
            // Обновляем позиции кнопок управления
            _closeBtn.Location = new Point(this.Width - 65, _closeBtn.Location.Y);
            _expandBtn.Location = new Point(this.Width - 144, _expandBtn.Location.Y);
            _rollBtn.Location = new Point(this.Width - 222, _rollBtn.Location.Y);
        }

        private void rollBtn_Click(object sender, EventArgs e) {
            switch (this.WindowState) {
                case FormWindowState.Maximized:
                case FormWindowState.Normal:
                    this.WindowState = FormWindowState.Minimized; // минимизируем
                    formWindowState = this.WindowState; // запоминаем предыдущее состояние
                    break;

                default:
                    this.WindowState = formWindowState; // возвращаем состояние как было
                    break;
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                isResizing = false;
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left && (
                Cursor.Current == Cursors.SizeWE ||
                Cursor.Current == Cursors.SizeNWSE)) {
                isResizing = true;
            }
        }
    }
}