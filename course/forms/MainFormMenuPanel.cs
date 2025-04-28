using course.classes;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace course.forms {

    public partial class MainForm {

        private void gridPnl_Click(object sender, EventArgs e) {
            UpdatePanels(
                new object[] { _gridImage, _gridLabel, _mainImage, _mainLabel },
                new string[] { "gridIconChosen.png", "mainIconDefault.png" }
            ); // обновление внешнего вида компонентов
        }

        private void mainPnl_Click(object sender, EventArgs e) {
            UpdatePanels(
                new object[] { _mainImage, _mainLabel, _gridImage, _gridLabel },
                new string[] { "mainIconChosen.png", "gridIconDefault.png" }
            ); // обновление внешнего вида компонентов
        }

        #region Пользовательские методы

        // Метод изменяющий состояния объектов при выборе какого-либо в панели "Меню"
        // Эл-ты массививов д.б симметричны по компонентам и идти попарно (PictureBox -> Label -> ...)
        // Первый элемент - тот, на который пользователь нажал
        private void UpdatePanels(object[] controls, string[] iconPaths) {
            if (controls is null || iconPaths is null) {
                return;
            } else if (controls.Length != iconPaths.Length * 2 || controls.Length % 2 == 1) {
                return; // эл-ты не симметричны
            }

            // Корневая папка иконок
            string appBaseDirectory = AppDomain.CurrentDomain.BaseDirectory; // путь к исполняемому файлу
            string imagePath = Path.Combine(appBaseDirectory, "..", "..", "icons"); // получаем доступ к каталогу icons

            // Изменяет внещний вид контроллеров, выбранных пользователем
            (controls[0] as PictureBox).Image =
                Image.FromFile(Path.Combine(imagePath, iconPaths[0]));
            (controls[1] as Label).ForeColor = Design.onEnterPanelColor;
            // Изменяем внешний вид оставшихся контроллеров
            foreach (var control in controls) {
                if (Array.IndexOf(controls, control) <= 1) {
                    continue;
                }

                if (control.GetType() == typeof(PictureBox)) {
                    // Меняем изображение
                    (control as PictureBox).Image =
                        Image.FromFile(Path.Combine(
                            imagePath, iconPaths[Array.IndexOf(controls, control) / 2]
                            ));
                } else if (control.GetType() == typeof(Label)) {
                    (control as Label).ForeColor = Design.DefaultTextColor;
                }
            }
        }

        #endregion Пользовательские методы
    }
}