using System.Drawing;

namespace course.classes {

    public class Design {

        // Dark Theme
        public static Color FormDarkDefaultColor = Color.FromArgb(45, 45, 45);

        public static Color ControlPanelDarkDefaultColor = Color.FromArgb(26, 26, 26);
        public static Color MenuPanelDarkDefaultColor = Color.FromArgb(35, 35, 35);
        public static Color SplitterDarkDefaultColor = Color.FromArgb(45, 45, 45);

        public static Color DefaultDarkTextColor = Color.FromArgb(100, 100, 100); // text is not chosen
        public static Color onEnterDarkPanelColor = Color.FromArgb(200, 200, 200); // panel is focused

        // Light Theme
        public static Color FormLightDefaultColor = Color.FromArgb(248, 249, 250);

        public static Color ControlPanelLightDefaultColor = Color.FromArgb(26, 26, 26);
        public static Color MenuPanelLightDefaultColor = Color.FromArgb(233, 236, 239);
        public static Color SplitterLightDefaultColor = Color.FromArgb(45, 45, 45);

        public static Color ButtonCloseEnterColor = Color.Red;
        public static Color ButtonExpandEnterColor = Color.MediumBlue;
        public static Color ButtonRollEnterColor = Color.Gray;

        public static Color DefaultLightTextColor = Color.FromArgb(200, 200, 200); // text is not chosen
        public static Color onEnterLightPanelColor = Color.FromArgb(52, 58, 64); // panel is focused
    }
}