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

        private Button _closeBtn, _minimizeBtn, _expandBtn, _styleBtn;
        private Panel _controlPnl, _menuPnl;
        private Splitter _menuSpl, _controlSpl;
        private PictureBox _mainImage, _gridImage;
        private Label _mainLabel, _gridLabel;
        private FormWindowState formWindowState = FormWindowState.Normal; // текущее состояние формы

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
            _styleBtn.Tag = 0; // текущая цветовая тема - темная
            this.Tag = 0x0; // битовая маска режима работы панели-меню. 0x0 - "Главная", 0x1 - "Таблицы"

            /*// Гибкая установка иконок PictureBox
            string appBaseDirectory = AppDomain.CurrentDomain.BaseDirectory; // путь к исполняемому файлу
            string imagePath = Path.Combine(appBaseDirectory, "..", "..", "icons"); // получаем доступ к каталогу icons
            _closeBtn.Image =
                Image.FromFile(Path.Combine(imagePath, "closeButton.png"));
            _minimizeBtn.Image =
                Image.FromFile(Path.Combine(imagePath, "minimizeButton.png"));
            _expandBtn.Image =
                Image.FromFile(Path.Combine(imagePath, "expandButton.png"));
            _mainImage.Image =
                Image.FromFile(Path.Combine(imagePath, "mainIconChosen.png"));
            _gridImage.Image =
                Image.FromFile(Path.Combine(imagePath, "gridIconDefault.png"));*/
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
            _btn.BackColor = Design.ControlPanelDarkDefaultColor;
        }

        private void rollBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ControlPanelDarkDefaultColor;
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
            _btn.BackColor = Design.ControlPanelDarkDefaultColor;
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

        private void switchBtn_Click(object sender, EventArgs e) {
            // Переключение дизайна интерфейса (темный или светлый стиль)
            string appBaseDirectory = AppDomain.CurrentDomain.BaseDirectory; // путь к исполняемому файлу
            string imagePath = Path.Combine(appBaseDirectory, "..", "..", "icons"); // получаем доступ к каталогу icons

            // Смена иконки кнопки. Tag == 0 -> в светлую тему, иначе - темную.
            int Tag = (int)_styleBtn.Tag;
            _styleBtn.Image = Image
                .FromFile(Path.Combine(
                    imagePath,
                    $"{(Tag == 0 ? "light" : "dark")}ModeButton.png"
                ));
            // Смена цветовой палитры
            switch (Tag) {
                case 0: // меняем на светлую
                    _controlPnl.BackColor = Design.ControlPanelLightDefaultColor;
                    _menuPnl.BackColor = Design.MenuPanelLightDefaultColor;

                    _closeBtn.BackColor = Design.ControlPanelLightDefaultColor;
                    _expandBtn.BackColor = Design.ControlPanelLightDefaultColor;
                    _minimizeBtn.BackColor = Design.ControlPanelLightDefaultColor;
                    _styleBtn.BackColor = Design.ControlPanelLightDefaultColor;

                    _menuSpl.BackColor = Design.SplitterLightDefaultColor;
                    _controlSpl.BackColor = Design.SplitterLightDefaultColor;

                    // Обновляем цвет для активных компонентов и неактивных компонентов панели-меню
                    /*foreach (Panel panels in _menuPnl.Controls) {
                        bool isActive = false; // текущий пункт меню не выбран по умолчанию
                        foreach (var control in panels.Controls) {
                            if (control is Label label) { // компонент - Label
                                                          // Смена цвета label. Зависит от того, является ли текущий пункт меню активным
                                label.BackColor = Design.MenuPanelLightDefaultColor;
                                label.ForeColor =
                                    label.BackColor == Design.DefaultDarkTextColor ?
                                    Design.DefaultLightTextColor : Design.onEnterLightPanelColor;
                                if (label.ForeColor == Design.onEnterLightPanelColor) {
                                    isActive = true; // данная панель содержит выбранный пункт меню
                                }
                            } else if (control is PictureBox pictureBox) {
                                 _styleBtn.Image = Image
                                    .FromFile(Path.Combine(
                                        imagePath,
                                        $"{(Tag == 0 ? "light" : "dark")}ModeButton.png"
                                    ));
                            }
                        }
                    }*/

                    this.BackColor = Design.FormLightDefaultColor;
                    break;

                case 1: // меняем на темную
                        // Установка цвета фона компонентов
                    _controlPnl.BackColor = Design.ControlPanelDarkDefaultColor;
                    _menuPnl.BackColor = Design.MenuPanelDarkDefaultColor;

                    _closeBtn.BackColor = Design.ControlPanelDarkDefaultColor;
                    _expandBtn.BackColor = Design.ControlPanelDarkDefaultColor;
                    _minimizeBtn.BackColor = Design.ControlPanelDarkDefaultColor;
                    _styleBtn.BackColor = Design.ControlPanelDarkDefaultColor;

                    _menuSpl.BackColor = Design.SplitterDarkDefaultColor;
                    _controlSpl.BackColor = Design.SplitterDarkDefaultColor;

                    //_mainLabel.BackColor = (int)this.Tag & 1 ==  ? Design.MenuPanelDarkDefaultColor;
                    _mainLabel.ForeColor = Design.onEnterDarkPanelColor;

                    _gridLabel.BackColor = Design.MenuPanelDarkDefaultColor;
                    _gridLabel.ForeColor = Design.DefaultDarkTextColor;

                    this.BackColor = Design.FormDarkDefaultColor;
                    break;
            }
            _styleBtn.Tag = (int)_styleBtn.Tag == 0 ? 1 : 0;
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