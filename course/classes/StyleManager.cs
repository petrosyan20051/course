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
        private bool theme { set; get; } // Current theme. Light or dark
        private string iconsPath { set; get; } =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "icons"); // Путь к папке с иконками

        private List<Panel> panels;

        public MenuManager(bool theme, string iconsPath) {
            this.theme = theme;
            this.iconsPath = iconsPath;
            this.panels = new List<Panel>();
        }

        // Добавление панели в менеджер
        public void AddPanel(Panel? panel, string? panelName) {
            if (!panels.Contains(panel)) {
                panels.Add(panel);
            }
        }

        // Обновление состояния меню
        public void UpdateMenuState(Panel clickedPanel) {
            foreach (var panel in panels) {
                var panelName = panel.Name;
                var label = panel.Controls.OfType<Label>().FirstOrDefault();
                var pictureBox = panel.Controls.OfType<PictureBox>().FirstOrDefault();

                if (label is null || pictureBox is null) continue;

                if (panel == clickedPanel) {
                    // Active for dark and light theme
                    label.ForeColor = !theme ? Design.onEnterDarkPanelColor : Design.onEnterLightPanelColor;
                    pictureBox.Image = LoadIcon($"{panelName}_colorful.png");
                } else {
                    // Inactive for dark and light theme
                    label.ForeColor = theme ? Design.onEnterLightPanelColor : Design.onEnterDarkPanelColor;
                    pictureBox.Image = LoadIcon($"{panelName}_pale.png");
                }
            }
        }

        // Загрузка иконки из файла
        private Image LoadIcon(string iconName) {
            string iconPath = Path.Combine(iconsPath, iconName);
            if (File.Exists(iconPath)) {
                return Image.FromFile(iconPath);
            }
            throw new FileNotFoundException($"Иконка не найдена: {iconPath}");
        }

        /*// Смена темы
        public void ChangeTheme(bool newTheme) {
            theme = newTheme;
            UpdateMenuState(null); // Обновляем все пункты меню для новой темы
        }*/
    }
}