using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Class for changing style of app. Dark Theme, Light Theme

namespace course.classes {

    public class StyleManager {
        private int theme { set; get; } // Current theme. Light or dark

        public string iconsPath { private set; get; } =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "icons"); // Путь к папке с иконками

        public List<Panel> panels { private set; get; }
        public List<Splitter> splitters { private set; get; }

        public StyleManager(int theme, string? iconsPath) {
            this.theme = theme;
            this.iconsPath = iconsPath;
            this.panels = [];
            this.splitters = [];
        }

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

        public void UpdateIconFolderPath(string path) {
            if (Directory.Exists(path)) {
                this.iconsPath = path;
            }
        }

        // Update menu state
        public void UpdateMenuState(Panel clickedPanel) {
            foreach (var panel in panels) {
                var panelName = panel.Name;
                var label = panel.Controls.OfType<Label>().FirstOrDefault();
                var pictureBox = panel.Controls.OfType<PictureBox>().FirstOrDefault();

                if (label is null || pictureBox is null) continue; // panel or label is not found

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

                // Change color for menu panels
                panel.BackColor = theme == Design.DarkTheme ? Design.MenuPanelDarkDefaultColor : Design.MenuPanelLightDefaultColor;
            }
            foreach (var splitter in splitters) {
                splitter.BackColor = theme == Design.DarkTheme ? Design.SplitterDarkDefaultColor : Design.SplitterLightDefaultColor;
            }
        }

        // Get icon from file
        private Image LoadIcon(string iconName) {
            string iconPath = Path.Combine(iconsPath, iconName);
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

        // Change theme
        public void ChangeMenuPanelTheme(int newTheme, Panel clickedPanel) {
            theme = newTheme; // update theme var
            UpdateMenuState(clickedPanel); // update all points for theme
        }
    }
}