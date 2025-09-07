namespace gui.Controllers {
    partial class RegisterControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            passwordTxtBox = new TextBox();
            passwordLbl = new Label();
            userNameTxtBox = new TextBox();
            userNameLbl = new Label();
            secCheckBox = new ComboBox();
            secCheckLbl = new Label();
            SuspendLayout();
            // 
            // passwordTxtBox
            // 
            passwordTxtBox.Enabled = false;
            passwordTxtBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            passwordTxtBox.Location = new Point(125, 121);
            passwordTxtBox.Margin = new Padding(4);
            passwordTxtBox.Name = "passwordTxtBox";
            passwordTxtBox.Size = new Size(157, 24);
            passwordTxtBox.TabIndex = 21;
            // 
            // passwordLbl
            // 
            passwordLbl.AutoSize = true;
            passwordLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            passwordLbl.Location = new Point(47, 125);
            passwordLbl.Margin = new Padding(4, 0, 4, 0);
            passwordLbl.Name = "passwordLbl";
            passwordLbl.Size = new Size(65, 18);
            passwordLbl.TabIndex = 22;
            passwordLbl.Text = "Пароль:";
            // 
            // userNameTxtBox
            // 
            userNameTxtBox.Enabled = false;
            userNameTxtBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            userNameTxtBox.Location = new Point(125, 60);
            userNameTxtBox.Margin = new Padding(4);
            userNameTxtBox.Name = "userNameTxtBox";
            userNameTxtBox.Size = new Size(157, 24);
            userNameTxtBox.TabIndex = 19;
            // 
            // userNameLbl
            // 
            userNameLbl.AutoSize = true;
            userNameLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            userNameLbl.Location = new Point(47, 63);
            userNameLbl.Margin = new Padding(4, 0, 4, 0);
            userNameLbl.Name = "userNameLbl";
            userNameLbl.Size = new Size(54, 18);
            userNameLbl.TabIndex = 20;
            userNameLbl.Text = "Логин:";
            // 
            // secCheckBox
            // 
            secCheckBox.DropDownStyle = ComboBoxStyle.DropDownList;
            secCheckBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            secCheckBox.FormattingEnabled = true;
            secCheckBox.Items.AddRange(new object[] { "Базовый пользователь", "Редактор", "Администратор" });
            secCheckBox.Location = new Point(125, 168);
            secCheckBox.Margin = new Padding(4);
            secCheckBox.Name = "secCheckBox";
            secCheckBox.Size = new Size(157, 26);
            secCheckBox.TabIndex = 23;
            // 
            // secCheckLbl
            // 
            secCheckLbl.AutoSize = true;
            secCheckLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            secCheckLbl.Location = new Point(47, 171);
            secCheckLbl.Margin = new Padding(4, 0, 4, 0);
            secCheckLbl.Name = "secCheckLbl";
            secCheckLbl.Size = new Size(44, 18);
            secCheckLbl.TabIndex = 24;
            secCheckLbl.Text = "Роль";
            // 
            // RegisterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(secCheckBox);
            Controls.Add(secCheckLbl);
            Controls.Add(passwordTxtBox);
            Controls.Add(passwordLbl);
            Controls.Add(userNameTxtBox);
            Controls.Add(userNameLbl);
            Name = "RegisterControl";
            Size = new Size(313, 405);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox passwordTxtBox;
        private Label passwordLbl;
        private TextBox userNameTxtBox;
        private Label userNameLbl;
        private ComboBox secCheckBox;
        private Label secCheckLbl;
    }
}
