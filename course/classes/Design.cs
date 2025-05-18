using System.Drawing;

namespace course.classes {

    public class Design {

        #region General colors

        public static Color ButtonCloseEnterColor = Color.Red;
        public static Color ButtonExpandEnterColor = Color.MediumBlue;

        #endregion General colors

        #region Dark Theme

        public static int DarkTheme = 0;
        public static Color FormDarkDefaultColor = Color.FromArgb(45, 45, 45);

        public static Color ControlPanelDarkDefaultColor = Color.FromArgb(26, 26, 26);
        public static Color MenuPanelDarkDefaultColor = Color.FromArgb(35, 35, 35);
        public static Color SplitterDarkDefaultColor = Color.FromArgb(45, 45, 45);

        public static Color ButtonRollDarkEnterColor = Color.FromArgb(50, 50, 50);
        public static Color ButtonStyleDarkEnterColor = Color.FromArgb(40, 40, 40);

        public static Color DataGridViewDarkThemeColor = Color.FromArgb(70, 70, 70);

        public static Color DefaultDarkTextColor = Color.FromArgb(100, 100, 100); // text is not chosen
        public static Color ChosenDarkPanelColor = Color.FromArgb(200, 200, 200); // text is chosen
        public static Color onPanelDarkFocusedColor = Color.FromArgb(60, 60, 60); // panel is focused

        #endregion Dark Theme

        #region Light Theme

        public static int LightTheme = 1;
        public static Color FormLightDefaultColor = Color.FromArgb(248, 249, 250);

        public static Color ControlPanelLightDefaultColor = Color.FromArgb(170, 170, 170);
        public static Color MenuPanelLightDefaultColor = Color.FromArgb(220, 220, 220);
        public static Color SplitterLightDefaultColor = Color.FromArgb(245, 245, 245);

        public static Color ButtonStyleLightEnterColor = Color.FromArgb(220, 220, 220);

        public static Color DefaultLightTextColor = Color.FromArgb(180, 180, 180); // text is not chosen
        public static Color onEnterLightPanelColor = Color.FromArgb(52, 58, 64); // text is chosen
        public static Color onPanelLightFocusedColor = Color.FromArgb(190, 190, 190); // panel is focused

        #endregion Light Theme
    }
}