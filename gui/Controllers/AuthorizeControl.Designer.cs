namespace gui.Controllers {
    partial class AuthorizeControl {
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
            enterBtn = new gui.controllers.RoundedButton();
            dbNameBox = new ComboBox();
            secCheckBox = new ComboBox();
            secCheckLbl = new Label();
            servNameBox = new ComboBox();
            dbNameLbl = new Label();
            passwordTxtBox = new TextBox();
            passwordLbl = new Label();
            userNameTxtBox = new TextBox();
            userNameLbl = new Label();
            servNameLbl = new Label();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // enterBtn
            // 
            enterBtn.BackColor = SystemColors.MenuHighlight;
            enterBtn.BorderRadius = 5;
            enterBtn.FlatAppearance.BorderSize = 0;
            enterBtn.FlatStyle = FlatStyle.Flat;
            enterBtn.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            enterBtn.ForeColor = Color.White;
            enterBtn.Location = new Point(17, 358);
            enterBtn.Margin = new Padding(4);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(475, 53);
            enterBtn.TabIndex = 19;
            enterBtn.Text = "Войти";
            enterBtn.UseVisualStyleBackColor = false;
            enterBtn.Click += enterBtn_Click;
            // 
            // dbNameBox
            // 
            dbNameBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            dbNameBox.FormattingEnabled = true;
            dbNameBox.Location = new Point(209, 295);
            dbNameBox.Margin = new Padding(4);
            dbNameBox.Name = "dbNameBox";
            dbNameBox.Size = new Size(283, 26);
            dbNameBox.TabIndex = 17;
            // 
            // secCheckBox
            // 
            secCheckBox.DropDownStyle = ComboBoxStyle.DropDownList;
            secCheckBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            secCheckBox.FormattingEnabled = true;
            secCheckBox.Items.AddRange(new object[] { "Проверка подлинности Windows", "Проверка подлинности SQL Server" });
            secCheckBox.Location = new Point(209, 110);
            secCheckBox.Margin = new Padding(4);
            secCheckBox.Name = "secCheckBox";
            secCheckBox.Size = new Size(283, 26);
            secCheckBox.TabIndex = 13;
            secCheckBox.SelectedIndexChanged += secCheckBox_SelectedIndexChanged;
            // 
            // secCheckLbl
            // 
            secCheckLbl.AutoSize = true;
            secCheckLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            secCheckLbl.Location = new Point(17, 112);
            secCheckLbl.Margin = new Padding(4, 0, 4, 0);
            secCheckLbl.Name = "secCheckLbl";
            secCheckLbl.Size = new Size(175, 18);
            secCheckLbl.TabIndex = 21;
            secCheckLbl.Text = "Проверка подлинности:";
            // 
            // servNameBox
            // 
            servNameBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            servNameBox.FormattingEnabled = true;
            servNameBox.Location = new Point(209, 47);
            servNameBox.Margin = new Padding(4);
            servNameBox.Name = "servNameBox";
            servNameBox.Size = new Size(283, 26);
            servNameBox.TabIndex = 11;
            servNameBox.Text = "localhost";
            // 
            // dbNameLbl
            // 
            dbNameLbl.AutoSize = true;
            dbNameLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            dbNameLbl.Location = new Point(17, 298);
            dbNameLbl.Margin = new Padding(4, 0, 4, 0);
            dbNameLbl.Name = "dbNameLbl";
            dbNameLbl.Size = new Size(101, 18);
            dbNameLbl.TabIndex = 20;
            dbNameLbl.Text = "База данных:";
            // 
            // passwordTxtBox
            // 
            passwordTxtBox.Enabled = false;
            passwordTxtBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            passwordTxtBox.Location = new Point(209, 234);
            passwordTxtBox.Margin = new Padding(4);
            passwordTxtBox.Name = "passwordTxtBox";
            passwordTxtBox.Size = new Size(283, 24);
            passwordTxtBox.TabIndex = 16;
            // 
            // passwordLbl
            // 
            passwordLbl.AutoSize = true;
            passwordLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            passwordLbl.Location = new Point(17, 236);
            passwordLbl.Margin = new Padding(4, 0, 4, 0);
            passwordLbl.Name = "passwordLbl";
            passwordLbl.Size = new Size(65, 18);
            passwordLbl.TabIndex = 18;
            passwordLbl.Text = "Пароль:";
            // 
            // userNameTxtBox
            // 
            userNameTxtBox.Enabled = false;
            userNameTxtBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            userNameTxtBox.Location = new Point(209, 173);
            userNameTxtBox.Margin = new Padding(4);
            userNameTxtBox.Name = "userNameTxtBox";
            userNameTxtBox.Size = new Size(283, 24);
            userNameTxtBox.TabIndex = 14;
            // 
            // userNameLbl
            // 
            userNameLbl.AutoSize = true;
            userNameLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            userNameLbl.Location = new Point(17, 174);
            userNameLbl.Margin = new Padding(4, 0, 4, 0);
            userNameLbl.Name = "userNameLbl";
            userNameLbl.Size = new Size(54, 18);
            userNameLbl.TabIndex = 15;
            userNameLbl.Text = "Логин:";
            // 
            // servNameLbl
            // 
            servNameLbl.AutoSize = true;
            servNameLbl.Font = new Font("Microsoft Sans Serif", 11.25F);
            servNameLbl.Location = new Point(16, 50);
            servNameLbl.Margin = new Padding(4, 0, 4, 0);
            servNameLbl.Name = "servNameLbl";
            servNameLbl.Size = new Size(102, 18);
            servNameLbl.TabIndex = 12;
            servNameLbl.Text = "Имя сервера:";
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Location = new Point(0, 420);
            progressBar.Margin = new Padding(4);
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(508, 26);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 22;
            progressBar.Visible = false;
            // 
            // AuthorizeControl
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(progressBar);
            Controls.Add(enterBtn);
            Controls.Add(dbNameBox);
            Controls.Add(secCheckBox);
            Controls.Add(secCheckLbl);
            Controls.Add(servNameBox);
            Controls.Add(dbNameLbl);
            Controls.Add(passwordTxtBox);
            Controls.Add(passwordLbl);
            Controls.Add(userNameTxtBox);
            Controls.Add(userNameLbl);
            Controls.Add(servNameLbl);
            Font = new Font("Microsoft Sans Serif", 11.25F);
            Margin = new Padding(4);
            Name = "AuthorizeControl";
            Size = new Size(508, 446);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private controllers.RoundedButton enterBtn;
        private ComboBox dbNameBox;
        private ComboBox secCheckBox;
        private Label secCheckLbl;
        private ComboBox servNameBox;
        private Label dbNameLbl;
        private TextBox passwordTxtBox;
        private Label passwordLbl;
        private TextBox userNameTxtBox;
        private Label userNameLbl;
        private Label servNameLbl;
        private ProgressBar progressBar;
    }
}
