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

    internal class StyleManager {
        public bool theme { set; get; } // Текущая тема ("light" или "dark")

        public string iconsPath { set; get; } =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "icons"); // Путь к папке с иконками

        private Dictionary<Panel, string> panelNames; // Словарь: панель -> имя пункта меню

        public MenuManager(bool theme, string iconsPath) {
            this.theme = theme;
            this.iconsPath = iconsPath;
            this.panelNames = new Dictionary<Panel, string>();
        }

        // Добавление панели в менеджер
        public void AddPanel(Panel? panel, string? panelName) {
            if (!panelNames.ContainsKey(panel)) {
                panelNames[panel] = panelName;
                panel.Click += Panel_Click; // Подписываемся на событие клика
            }
        }

        // Обработчик клика по панели
        private void Panel_Click(object sender, EventArgs e) {
            var clickedPanel = sender as Panel;
            UpdateMenuState(clickedPanel);
        }

        // Обновление состояния меню
        public void UpdateMenuState(Panel clickedPanel) {
            foreach (var panel in panelNames.Keys) {
                var panelName = panelNames[panel];
                var label = panel.Controls.OfType<Label>().FirstOrDefault();
                var pictureBox = panel.Controls.OfType<PictureBox>().FirstOrDefault();

                if (label == null || pictureBox == null) continue;

                if (panel == clickedPanel) {
                    // Активный пункт меню
                    label.ForeColor = !theme ? Color.Black : Color.White;
                    pictureBox.Image = LoadIcon($"{panelName}_active_{theme}.png");
                } else {
                    // Неактивный пункт меню
                    label.ForeColor = theme ? Color.Gray : Color.DarkGray;
                    pictureBox.Image = LoadIcon($"{panelName}_inactive_{theme}.png");
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

        // Смена темы
        public void ChangeTheme(string newTheme) {
            theme = newTheme;
            UpdateMenuState(null); // Обновляем все пункты меню для новой темы
        }
    }
}