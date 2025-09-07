using gui.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui.Controllers {
    public partial class RegisterControl : UserControl {
        public RegisterControl() {
            InitializeComponent();
            InitVariables();
        }

        #region Пользовательские методы

        private void InitVariables() {
            this.Tag = Login.ActionType.Register;
        }

        #endregion
    }
}
