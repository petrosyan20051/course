using gui.classes;

namespace gui.forms {

    public partial class MainForm : Form {
        private bool isDragging { get; set; } = false; // форма находится в состоянии перемещения
        private bool isResizing { get; set; } = false; // форма находится в состоянии изменения размера
        private UserRights userRights { get; set; } = UserRights.Admin;

        private Point startPoint; // точка начала перемещения
        private FormWindowState formWindowState = FormWindowState.Normal; // текущее состояние формы

        private Button _closeBtn, _minimizeBtn, _expandBtn, _styleBtn, _userBtn;
        private Panel _controlPnl, _menuPnl, _mainMenuPnl, _gridMenuPnl, _mainGridPanel;
        private Splitter _menuSpl, _controlSpl;
        private PictureBox _mainImage, _gridImage;
        private Label _mainLabel, _gridLabel;

        private StyleManager style;

        public enum UserRights { Basic, Admin } // rights access for user

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
            _mainMenuPnl = this.mainPnl;
            _gridMenuPnl = this.gridPnl;
            _mainGridPanel = this.mainPnlGrid;
            _userBtn = this.userBtn;

            style = new StyleManager(
                Design.DarkTheme,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "icons")
            ); // Make style manager
            foreach (var control in _menuPnl.Controls) { // Add panels into manager
                if (control is Panel panel) {
                    style.AddPanel(panel, panel.Name);
                }
            }
            foreach (var control in this.Controls) { // add splitter into manager
                if (control is Splitter splitter) {
                    style.AddSplitter(splitter);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            this.InitVariables();

            // Add from about general information of tables
            TableInformation form = new TableInformation();
            style.AddDataGrid(form.Controls.OfType<DataGridView>().FirstOrDefault()); // add data grid into manager

            form.Height = _mainGridPanel.Height;
            form.Dock = DockStyle.Fill;
            _mainGridPanel.Controls.Add(form);
            form.Show();

            // Set components color
            _controlPnl.BackColor = Design.ControlPanelDarkDefaultColor;
            _menuPnl.BackColor = Design.MenuPanelDarkDefaultColor;

            _closeBtn.BackColor = Design.ControlPanelDarkDefaultColor;
            _expandBtn.BackColor = Design.ControlPanelDarkDefaultColor;
            _minimizeBtn.BackColor = Design.ControlPanelDarkDefaultColor;
            _styleBtn.BackColor = Design.ControlPanelDarkDefaultColor;

            _menuSpl.BackColor = Design.SplitterDarkDefaultColor;
            _controlSpl.BackColor = Design.SplitterDarkDefaultColor;

            _mainLabel.BackColor = Design.MenuPanelDarkDefaultColor;
            _mainLabel.ForeColor = Design.ChosenDarkPanelColor;
            _gridLabel.BackColor = Design.MenuPanelDarkDefaultColor;
            _gridLabel.ForeColor = Design.DefaultDarkTextColor;

            // Set picturebox location
            int startPointOfPictureBox = 15;
            int dx = 15;
            _mainImage.Location = new Point(startPointOfPictureBox, _mainMenuPnl.Height / 4);
            _gridImage.Location = new Point(startPointOfPictureBox, _gridMenuPnl.Height / 4);

            // Set label location
            _mainLabel.Location = new Point(_mainImage.Location.X + _mainImage.Width + dx, _mainMenuPnl.Height / 4 + 9);
            _gridLabel.Location = new Point(_gridImage.Location.X + _gridImage.Width + dx, _gridMenuPnl.Height / 4 + 6);

            // Set form color
            this.BackColor = Design.FormDarkDefaultColor;
            _styleBtn.Tag = Design.DarkTheme; // current theme - dark
            _menuPnl.Tag = _mainMenuPnl; // current chosen point

            // Add tooltips for components
            styleTip.SetToolTip(
                _styleBtn,
                $"Текущая тема: {((int)_styleBtn.Tag == Design.DarkTheme ?
                "тёмная" : "светлая")}"
            );
            minimizeTip.SetToolTip(_minimizeBtn, "Свернуть в трей");
            expandTip.SetToolTip(_expandBtn, "Растянуть на весь экран / вернуть в исходное положение");
            closeTip.SetToolTip(_closeBtn, "Завершение работы приложения");
            userTip.SetToolTip(
                _userBtn, $"Текущие права пользователя: " +
                $"{(userRights == UserRights.Admin ? "администратор" : "базовый пользователь")}");
        }

        private void closeBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ButtonCloseEnterColor;
        }

        private void closeBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = _controlPnl.BackColor;
        }

        private void minimizeBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = _controlPnl.BackColor;
        }

        private void minimizeBtn_MouseEnter(object sender, EventArgs e) {
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
            _btn.BackColor = (int)_styleBtn.Tag == Design.DarkTheme ? Design.ButtonStyleDarkEnterColor : Design.ButtonStyleLightEnterColor;
        }

        private void styleBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = _controlPnl.BackColor;
        }

        private void minimizeBtn_Click(object sender, EventArgs e) {
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

        private void mainPnl_MouseEnter(object sender, EventArgs e) {
            _mainMenuPnl.BackColor = (int)_styleBtn.Tag == Design.DarkTheme ?
                Design.onPanelDarkFocusedColor : Design.onPanelLightFocusedColor;
            _mainLabel.BackColor = (int)_styleBtn.Tag == Design.DarkTheme ?
                Design.onPanelDarkFocusedColor : Design.onPanelLightFocusedColor;
        }

        private void mainPnl_MouseLeave(object sender, EventArgs e) {
            _mainMenuPnl.BackColor = _menuPnl.BackColor;
            _mainLabel.BackColor = _menuPnl.BackColor;
        }

        private void gridPnl_MouseEnter(object sender, EventArgs e) {
            _gridMenuPnl.BackColor = (int)_styleBtn.Tag == Design.DarkTheme ?
                Design.onPanelDarkFocusedColor : Design.onPanelLightFocusedColor;
            _gridLabel.BackColor = (int)_styleBtn.Tag == Design.DarkTheme ?
                Design.onPanelDarkFocusedColor : Design.onPanelLightFocusedColor;
        }

        private void gridPnl_MouseLeave(object sender, EventArgs e) {
            _gridMenuPnl.BackColor = _menuPnl.BackColor;
            _gridLabel.BackColor = _menuPnl.BackColor;
        }

        private void closeBtn_Click(object sender, EventArgs e) {
            Application.Exit();
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
            (sender as Button).Image = StyleManager
                .LoadIcon(
                    Path.Combine(style.iconsPath, $"{(tag == Design.DarkTheme ? "light" : "dark")}_mode.png"),
                    style.iconsPath
                );

            ChangeDesign(tag == Design.DarkTheme ? Design.LightTheme : Design.DarkTheme);

            _styleBtn.Tag = (int)_styleBtn.Tag == Design.DarkTheme ? Design.LightTheme : Design.DarkTheme;
            styleTip.SetToolTip(
                _styleBtn,
                $"Текущая тема: {((int)_styleBtn.Tag == Design.DarkTheme ?
                "тёмная" : "светлая")}");
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
            _closeBtn.Location = new Point(this.Width - _closeBtn.Width, _closeBtn.Location.Y);
            _expandBtn.Location = new Point(_closeBtn.Location.X - _expandBtn.Width, _expandBtn.Location.Y);
            _minimizeBtn.Location = new Point(_expandBtn.Location.X - _minimizeBtn.Width, _minimizeBtn.Location.Y);
            _styleBtn.Location = new Point(_minimizeBtn.Location.X - _styleBtn.Width, _styleBtn.Location.Y);
            _userBtn.Location = new Point(_styleBtn.Location.X - _userBtn.Width, _userBtn.Location.Y);
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

        private void userBtn_Click(object sender, EventArgs e) {
            switch (userRights) {
                case UserRights.Basic:
                    userRights = UserRights.Admin;
                    userTip.SetToolTip(_userBtn, "Текущие права пользователя: администратор");

                    break;

                case UserRights.Admin:
                    userRights = UserRights.Basic;
                    userTip.SetToolTip(_userBtn, "Текущие права пользователя: базовый пользователь");
                    break;
            }
        }
    }
}