using course.classes;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace course.forms {

    public partial class MainForm {

        private void gridPnl_Click(object sender, EventArgs e) {
            style.UpdateMenuState(_gridPnl);
        }

        private void mainPnl_Click(object sender, EventArgs e) {
            style.UpdateMenuState(_mainPnl);
        }
    }
}