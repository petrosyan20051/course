using course.classes;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace course.forms {

    public partial class MainForm : Form {
        private bool isDragging = false; // форма находится в состоянии перемещения
        private bool isResizing = false; // форма находится в состоянии изменения размера

        private Point startPoint; // точка начала перемещения
        private FormWindowState formWindowState = FormWindowState.Normal; // текущее состояние формы

        private Button _closeBtn, _minimizeBtn, _expandBtn, _styleBtn;
        private Panel _controlPnl, _menuPnl, _mainPnl, _gridPnl;
        private Splitter _menuSpl, _controlSpl;
        private PictureBox _mainImage, _gridImage;
        private Label _mainLabel, _gridLabel;

        private StyleManager style;
        public MainForm() {
            InitializeComponent();
        }

        private void InitVariables() {
            _closeBtn = this.closeBtn;
            _minimizeBtn = this.minimizeBtn;
            _expandBtn = this.expandBtn;
            _controlPnl = this.controlPnl;
            _menuPnl = this.menuPnl;
            _menuSpl = this.menuSpl;
            _controlSpl = this.controlSpl;
            _mainImage = this.mainImage;
            _mainLabel = this.mainLbl;
            _gridImage = this.gridImage;
            _gridLabel = this.gridLbl;
            _styleBtn = this.styleBtn;
            _mainPnl = this.mainPnl;
            _gridPnl = this.gridPnl;
            
            style = new StyleManager(
                Design.DarkTheme,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "icons")
            ); // Make style manager
            foreach (Panel panel in _menuPnl.Controls) { // Add panels into panel
                style.AddPanel(panel, panel.Name);
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            this.InitVariables();

            // Установка цвета фона компонентов
            _controlPnl.BackColor = Design.ControlPanelDarkDefaultColor;
            _menuPnl.BackColor = Design.MenuPanelDarkDefaultColor;

            _closeBtn.BackColor = Design.ControlPanelDarkDefaultColor;
            _expandBtn.BackColor = Design.ControlPanelDarkDefaultColor;
            _minimizeBtn.BackColor = Design.ControlPanelDarkDefaultColor;
            _styleBtn.BackColor = Design.ControlPanelDarkDefaultColor;

            _menuSpl.BackColor = Design.SplitterDarkDefaultColor;
            _controlSpl.BackColor = Design.SplitterDarkDefaultColor;

            _mainLabel.BackColor = Design.MenuPanelDarkDefaultColor;
            _mainLabel.ForeColor = Design.onEnterDarkPanelColor;
            // Установка расположения label
            _mainLabel.Location = new Point(
                    _mainLabel.Location.X,
                    (_mainImage.Location.Y + _mainLabel.Height) / 2 + 9
                );

            _gridLabel.BackColor = Design.MenuPanelDarkDefaultColor;
            _gridLabel.ForeColor = Design.DefaultDarkTextColor;
            _gridLabel.Location = new Point(
                    _gridLabel.Location.X,
                    (_gridImage.Location.Y + _gridLabel.Height) / 2 + 9
                );

            // Установка цвета формы фона
            (sender as Form)?.BackColor = Design.FormDarkDefaultColor;
            _styleBtn.Tag = Design.DarkTheme; // current theme - dark
            _menuPnl.Tag = _mainPnl; // current chosen point
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
            _btn.BackColor = _controlPnl.BackColor;
        }

        private void rollBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = _controlPnl.BackColor;
        }

        private void rollBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ButtonRollDarkEnterColor;
        }

        private void expandBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ButtonExpandEnterColor;
        }

        private void expandBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = _controlPnl.BackColor;
        }

        private void styleBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn?.BackColor = (int)_styleBtn.Tag == Design.DarkTheme ? Design.ButtonStyleDarkEnterColor : Design.ButtonStyleLightEnterColor;
        }

        private void styleBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn?.BackColor = _controlPnl.BackColor;
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

        private void styleBtn_Click(object sender, EventArgs e) {

            // Change theme
            int tag = (int)_styleBtn.Tag;
            style.ChangeMenuPanelTheme(tag == Design.DarkTheme ? Design.LightTheme : Design.DarkTheme, (Panel)_menuPnl.Tag);
            (sender as Button)?.Image = StyleManager
                .LoadIcon(
                    Path.Combine(style.iconsPath, $"{(tag == Design.DarkTheme ? "light" : "dark")}_mode.png"), 
                    style.iconsPath
                );            
            
            ChangeDesign(tag == Design.DarkTheme ? Design.LightTheme : Design.DarkTheme);
            
            _styleBtn.Tag = (int)_styleBtn.Tag == Design.DarkTheme ? Design.LightTheme : Design.DarkTheme;
            
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
                _minimizeBtn.Location = new Point(this.Width - 222, _minimizeBtn.Location.Y);
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
            _minimizeBtn.Location = new Point(this.Width - 222, _minimizeBtn.Location.Y);
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