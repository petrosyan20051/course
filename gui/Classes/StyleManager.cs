// Class for changing style of app. Dark Theme, Light Theme

namespace gui.Classes {

    public class StyleManager {
        private int theme { set; get; } // Current theme. Light or dark

        public string IconsPath { private set; get; } // path with icons

        public List<Panel> panels { private set; get; }
        public List<Splitter> splitters { private set; get; }
        public List<DataGridView> datagrids { private set; get; }
        public List<Button> buttons { private set; get; }
        public List<Form> forms { private set; get; }

        public StyleManager(int theme, string iconsPath) {
            this.theme = theme;
            this.IconsPath = iconsPath;
            this.panels = new List<Panel>();
            this.splitters = new List<Splitter>();
            this.datagrids = new List<DataGridView>();
            this.buttons = new List<Button>();
            this.forms = new List<Form>();
        }

        #region Add Controls

        // Add panel into manager
        public void AddPanel(Panel? panel, string? panelName) {
            if (!panels.Contains(panel)) {
                panels.Add(panel);
            }
        }

        // Add splitter into manager
        public void AddSplitter(Splitter? splitter) {
            if (!splitter.Contains(splitter)) {
                splitters.Add(splitter);
            }
        }

        // Add grid into manager
        public void AddDataGrid(DataGridView? grid) {
            if (!datagrids.Contains(grid)) {
                datagrids.Add(grid);
            }
        }

        public void AddButton(Button? button) {
            if (!buttons.Contains(button)) {
                buttons.Add(button);
            }
        }

        public void AddForm(Form? form) {
            if (!forms.Contains(form)) {
                forms.Add(form);
            }
        }

        #endregion

        #region Delete Controls

        public void DeletePanel(Panel? panel) {
            if (panels.Contains(panel)) {
                panels.Remove(panel);
            }
        }

        public void DeletePanel(Splitter? splitter) {
            if (splitters.Contains(splitter)) {
                splitters.Remove(splitter);
            }
        }

        public void DeleteDataGrid(DataGridView? grid) {
            if (datagrids.Contains(grid)) {
                datagrids.Remove(grid);
            }
        }

        public void DeleteButton(Button? button) {
            if (buttons.Contains(button)) {
                buttons.Remove(button);
            }
        }

        public void DeleteForm(Form? form) {
            if (forms.Contains(form)) {
                forms.Remove(form);
            }
        }

        public void UpdateIconFolderPath(string path) {
            if (Directory.Exists(path)) {
                this.IconsPath = path;
            }
        }

        #endregion

        // Update menu state
        public void UpdateMenuState(Panel clickedPanel) {
            foreach (var panel in panels) {
                var panelName = panel.Name;
                var label = panel.Controls.OfType<Label>().FirstOrDefault();
                var pictureBox = panel.Controls.OfType<PictureBox>().FirstOrDefault();

                if (label is null || pictureBox is null) {  // panel or label is not found
                    // Change color for menu and control panels
                    if (panel.Name.Contains("control")) { // controlPanel
                        panel.BackColor = theme == Design.LightTheme ? Design.ControlPanelLightDefaultColor : Design.ControlPanelDarkDefaultColor;
                        foreach (var button in buttons) {
                            button.BackColor = panel.BackColor;
                        }
                    } else { // menuPanel
                        panel.BackColor = theme == Design.LightTheme ? Design.MenuPanelLightDefaultColor : Design.MenuPanelDarkDefaultColor;
                    }
                    continue;
                }


                label.BackColor = theme == Design.DarkTheme ?
                        Design.MenuPanelDarkDefaultColor : Design.MenuPanelLightDefaultColor;

                if (panel.Equals(clickedPanel)) { // change icon for clicked panel
                    label.ForeColor = theme == Design.DarkTheme ?
                        Design.ChosenDarkPanelColor : Design.onEnterLightPanelColor;
                    pictureBox.Image = LoadIcon($"{panelName}_on_{(theme == Design.DarkTheme ? "dark" : "bright")}_theme.png");
                } else { // otherwise
                    label.ForeColor = theme == Design.DarkTheme ?
                        Design.DefaultDarkTextColor : Design.DefaultLightTextColor;
                    pictureBox.Image = LoadIcon($"{panelName}_off_{(theme == Design.DarkTheme ? "dark" : "bright")}_theme.png");
                }

                panel.BackColor = theme == Design.DarkTheme ? Design.MenuPanelDarkDefaultColor : Design.MenuPanelLightDefaultColor;

            }
            foreach (var splitter in splitters) {
                splitter?.BackColor = theme == Design.DarkTheme ? Design.SplitterDarkDefaultColor : Design.SplitterLightDefaultColor;
            }
            foreach (var grid in datagrids) {
                grid?.BackgroundColor = theme == Design.DarkTheme ? Design.DataGridViewDarkThemeColor : Design.DataGridViewLightThemeColor;
            }
        }

        // Get icon from file
        private Image LoadIcon(string iconName) {
            string iconPath = Path.Combine(IconsPath, iconName);
            if (File.Exists(iconPath)) {
                return Image.FromFile(iconPath);
            }
            throw new FileNotFoundException($"Icon is not found: {iconPath}");
        }

        public static Image LoadIcon(string iconName, string iconsPath) {
            string iconPath = Path.Combine(iconsPath, iconName);
            if (File.Exists(iconPath)) {
                return Image.FromFile(iconPath);
            }
            throw new FileNotFoundException($"Icon is not found: {iconPath}");
        }

        // Change menu panel theme theme
        public void ChangeMenuPanelTheme(int newTheme, Panel clickedPanel) {
            if (newTheme != Design.DarkTheme && newTheme != Design.LightTheme) { // incorrect input
                return;
            }

            theme = newTheme; // update theme var
            UpdateMenuState(clickedPanel); // update all points for theme
        }

        // Change theme of all controls
        public void ChangeFullTheme(int newTheme, Panel clickedPanel) {
            if (newTheme != Design.DarkTheme && newTheme != Design.LightTheme) { // incorrect input
                return;
            }
            theme = newTheme;

            foreach (var form in forms) { // changed forms backcolor
                form.BackColor = newTheme == Design.LightTheme ? Design.FormLightDefaultColor : Design.FormDarkDefaultColor;
            }

            ChangeMenuPanelTheme(newTheme, clickedPanel);
        }
    }
}