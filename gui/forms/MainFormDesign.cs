using gui.Classes;

namespace gui.Forms {

    public partial class MainForm {

        private void gridPnl_Click(object sender, EventArgs e) {
            styler.UpdateMenuState(_gridMenuPnl);
            _menuPnl.Tag = _gridMenuPnl; // update which point of menu is chosen
        }

        private void mainPnl_Click(object sender, EventArgs e) {
            styler.UpdateMenuState(_mainMenuPnl);
            _menuPnl.Tag = _mainMenuPnl; // update which point of menu is chosen
        }

        #region Пользовательские методы



        #endregion Пользовательские методы

        #region Deprecated

        // Method to change full design of app between light and dark mode
        private void ChangeDesign(int modeToChange) {
            if (modeToChange != Design.DarkTheme && modeToChange != Design.LightTheme) { // incorrect input
                return;
            }

            this.BackColor = modeToChange == Design.LightTheme ? Design.FormLightDefaultColor : Design.FormDarkDefaultColor;
            _menuPnl.BackColor = modeToChange == Design.LightTheme ? Design.MenuPanelLightDefaultColor : Design.MenuPanelDarkDefaultColor;
            _controlPnl.BackColor = modeToChange == Design.LightTheme ? Design.ControlPanelLightDefaultColor : Design.ControlPanelDarkDefaultColor;
            foreach (Button button in _controlPnl.Controls) { // change background color for control panel buttons
                button.BackColor = _controlPnl.BackColor;
            }
            _menuSpl.BackColor = modeToChange == Design.LightTheme ? Design.SplitterLightDefaultColor : Design.SplitterDarkDefaultColor;
            _controlSpl.BackColor = modeToChange == Design.LightTheme ? Design.SplitterLightDefaultColor : Design.SplitterDarkDefaultColor;
        }

        #endregion
    }
}
