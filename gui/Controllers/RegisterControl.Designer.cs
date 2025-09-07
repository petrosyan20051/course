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
            loginTxtBox = new TextBox();
            userNameLbl = new Label();
            roleComboBox = new ComboBox();
            secCheckLbl = new Label();
            enterBtn = new gui.controllers.RoundedButton();
            SuspendLayout();
            // 
            // passwordTxtBox
            // 
            passwordTxtBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            passwordTxtBox.Location = new Point(103, 114);
            passwordTxtBox.Margin = new Padding(4);
            passwordTxtBox.Name = "passwordTxtBox";
            passwordTxtBox.Size = new Size(389, 24);
            passwordTxtBox.TabIndex = 21;
            // 
            // passwordLbl
            // 
            passwordLbl.AutoSize = true;
            passwordLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            passwordLbl.Location = new Point(25, 117);
            passwordLbl.Margin = new Padding(4, 0, 4, 0);
            passwordLbl.Name = "passwordLbl";
            passwordLbl.Size = new Size(65, 18);
            passwordLbl.TabIndex = 22;
            passwordLbl.Text = "Пароль:";
            // 
            // loginTxtBox
            // 
            loginTxtBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            loginTxtBox.Location = new Point(103, 60);
            loginTxtBox.Margin = new Padding(4);
            loginTxtBox.Name = "loginTxtBox";
            loginTxtBox.Size = new Size(389, 24);
            loginTxtBox.TabIndex = 19;
            // 
            // userNameLbl
            // 
            userNameLbl.AutoSize = true;
            userNameLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            userNameLbl.Location = new Point(25, 63);
            userNameLbl.Margin = new Padding(4, 0, 4, 0);
            userNameLbl.Name = "userNameLbl";
            userNameLbl.Size = new Size(54, 18);
            userNameLbl.TabIndex = 20;
            userNameLbl.Text = "Логин:";
            // 
            // roleComboBox
            // 
            roleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            roleComboBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            roleComboBox.FormattingEnabled = true;
            roleComboBox.Items.AddRange(new object[] { "Базовый пользователь", "Редактор", "Администратор" });
            roleComboBox.Location = new Point(103, 168);
            roleComboBox.Margin = new Padding(4);
            roleComboBox.Name = "roleComboBox";
            roleComboBox.Size = new Size(389, 26);
            roleComboBox.TabIndex = 23;
            // 
            // secCheckLbl
            // 
            secCheckLbl.AutoSize = true;
            secCheckLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            secCheckLbl.Location = new Point(25, 171);
            secCheckLbl.Margin = new Padding(4, 0, 4, 0);
            secCheckLbl.Name = "secCheckLbl";
            secCheckLbl.Size = new Size(48, 18);
            secCheckLbl.TabIndex = 24;
            secCheckLbl.Text = "Роль:";
            // 
            // enterBtn
            // 
            enterBtn.BackColor = SystemColors.MenuHighlight;
            enterBtn.BorderRadius = 5;
            enterBtn.FlatAppearance.BorderSize = 0;
            enterBtn.FlatStyle = FlatStyle.Flat;
            enterBtn.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            enterBtn.ForeColor = Color.White;
            enterBtn.Location = new Point(25, 229);
            enterBtn.Margin = new Padding(4);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(467, 53);
            enterBtn.TabIndex = 25;
            enterBtn.Text = "Зарегистрировать";
            enterBtn.UseVisualStyleBackColor = false;
            enterBtn.Click += enterBtn_Click;
            // 
            // RegisterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(enterBtn);
            Controls.Add(roleComboBox);
            Controls.Add(secCheckLbl);
            Controls.Add(passwordTxtBox);
            Controls.Add(passwordLbl);
            Controls.Add(loginTxtBox);
            Controls.Add(userNameLbl);
            Name = "RegisterControl";
            Size = new Size(499, 315);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox passwordTxtBox;
        private Label passwordLbl;
        private TextBox loginTxtBox;
        private Label userNameLbl;
        private ComboBox roleComboBox;
        private Label secCheckLbl;
        private controllers.RoundedButton enterBtn;
    }
}
