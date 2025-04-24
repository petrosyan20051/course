using System;
using System.Drawing;
using System.Windows.Forms;
using course.classes;

namespace course {
    public partial class MainForm : Form {
        private bool isDragging = false; // форма находится в состоянии перемещения
        private bool isResizing = false; // форма находится в состоянии изменения размера

        private Point startPoint; // точка начала перемещения
        private Point resizeStartPoint; // точка начала изменения размера

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            controlPnl.BackColor = Design.PanelDefaultColor;
            closeBtn.BackColor = Design.PanelDefaultColor;
            expandBtn.BackColor = Design.PanelDefaultColor;
            rollBtn.BackColor = Design.PanelDefaultColor;

            var _form = sender as Form;
            _form.BackColor = Design.FormDefaultColor;

        }

        private void closeBtn_MouseClick(object sender, MouseEventArgs e) {
            Application.Exit();
        }

        private void closeBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ButtonCloseEnterColor;
        }

        private void closeBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.PanelDefaultColor;
        }

        private void rollBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.PanelDefaultColor;
        }

        private void rollBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ButtonRollEnterColor;
        }

        private void expandBtn_MouseEnter(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.ButtonExpandEnterColor;
        }

        private void expandBtn_MouseLeave(object sender, EventArgs e) {
            var _btn = sender as Button;
            _btn.BackColor = Design.PanelDefaultColor;
        }

        private void controlPnl_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                isDragging = true;
                startPoint = new Point(e.X, e.Y);
            }
        }

        private void controlPnl_MouseUp(object sender, MouseEventArgs e) {
            isDragging = false;
        }

        private void controlPnl_MouseMove(object sender, MouseEventArgs e) {
            if (isDragging) {
                Point newPoint = this.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(-startPoint.X, -startPoint.Y);
                this.Location = newPoint;
            }
        }
    }
}
