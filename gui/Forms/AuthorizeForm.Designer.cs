namespace gui.Forms {
    partial class AuthorizeForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            mainLbl = new Label();
            servNameLbl = new Label();
            userNameTxtBox = new TextBox();
            userNameLbl = new Label();
            passwordTxtBox = new TextBox();
            passwordLbl = new Label();
            dbNameLbl = new Label();
            servNameBox = new ComboBox();
            secCheckLbl = new Label();
            secCheckBox = new ComboBox();
            dbNameBox = new ComboBox();
            enterBtn = new gui.controllers.RoundedButton();
            SuspendLayout();
            // 
            // mainLbl
            // 
            mainLbl.AutoSize = true;
            mainLbl.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 204);
            mainLbl.Location = new Point(195, 19);
            mainLbl.Name = "mainLbl";
            mainLbl.Size = new Size(228, 45);
            mainLbl.TabIndex = 0;
            mainLbl.Text = "Авторизация";
            // 
            // servNameLbl
            // 
            servNameLbl.AutoSize = true;
            servNameLbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            servNameLbl.Location = new Point(20, 107);
            servNameLbl.Name = "servNameLbl";
            servNameLbl.Size = new Size(129, 25);
            servNameLbl.TabIndex = 1;
            servNameLbl.Text = "Имя сервера:";
            // 
            // userNameTxtBox
            // 
            userNameTxtBox.Enabled = false;
            userNameTxtBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            userNameTxtBox.Location = new Point(266, 205);
            userNameTxtBox.Name = "userNameTxtBox";
            userNameTxtBox.Size = new Size(340, 32);
            userNameTxtBox.TabIndex = 3;
            // 
            // userNameLbl
            // 
            userNameLbl.AutoSize = true;
            userNameLbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            userNameLbl.Location = new Point(20, 208);
            userNameLbl.Name = "userNameLbl";
            userNameLbl.Size = new Size(69, 25);
            userNameLbl.TabIndex = 3;
            userNameLbl.Text = "Логин:";
            // 
            // passwordTxtBox
            // 
            passwordTxtBox.Enabled = false;
            passwordTxtBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            passwordTxtBox.Location = new Point(266, 258);
            passwordTxtBox.Name = "passwordTxtBox";
            passwordTxtBox.Size = new Size(340, 32);
            passwordTxtBox.TabIndex = 4;
            // 
            // passwordLbl
            // 
            passwordLbl.AutoSize = true;
            passwordLbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            passwordLbl.Location = new Point(20, 261);
            passwordLbl.Name = "passwordLbl";
            passwordLbl.Size = new Size(82, 25);
            passwordLbl.TabIndex = 5;
            passwordLbl.Text = "Пароль:";
            // 
            // dbNameLbl
            // 
            dbNameLbl.AutoSize = true;
            dbNameLbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dbNameLbl.Location = new Point(25, 316);
            dbNameLbl.Name = "dbNameLbl";
            dbNameLbl.Size = new Size(124, 25);
            dbNameLbl.TabIndex = 8;
            dbNameLbl.Text = "База данных:";
            // 
            // servNameBox
            // 
            servNameBox.Font = new Font("Segoe UI", 14F);
            servNameBox.FormattingEnabled = true;
            servNameBox.Location = new Point(266, 107);
            servNameBox.Name = "servNameBox";
            servNameBox.Size = new Size(340, 33);
            servNameBox.TabIndex = 1;
            // 
            // secCheckLbl
            // 
            secCheckLbl.AutoSize = true;
            secCheckLbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            secCheckLbl.Location = new Point(20, 157);
            secCheckLbl.Name = "secCheckLbl";
            secCheckLbl.Size = new Size(221, 25);
            secCheckLbl.TabIndex = 10;
            secCheckLbl.Text = "Проверка подлинности:";
            // 
            // secCheckBox
            // 
            secCheckBox.DropDownStyle = ComboBoxStyle.DropDownList;
            secCheckBox.Font = new Font("Segoe UI", 14F);
            secCheckBox.FormattingEnabled = true;
            secCheckBox.Items.AddRange(new object[] { "Проверка подлинности Windows", "Проверка подлинности SQL Server" });
            secCheckBox.Location = new Point(266, 157);
            secCheckBox.Name = "secCheckBox";
            secCheckBox.Size = new Size(340, 33);
            secCheckBox.TabIndex = 2;
            secCheckBox.SelectedIndexChanged += secCheckBox_SelectedIndexChanged;
            // 
            // dbNameBox
            // 
            dbNameBox.Font = new Font("Segoe UI", 14F);
            dbNameBox.FormattingEnabled = true;
            dbNameBox.Location = new Point(266, 313);
            dbNameBox.Name = "dbNameBox";
            dbNameBox.Size = new Size(340, 33);
            dbNameBox.TabIndex = 5;
            // 
            // enterBtn
            // 
            enterBtn.BackColor = SystemColors.AppWorkspace;
            enterBtn.BorderRadius = 5;
            enterBtn.FlatAppearance.BorderSize = 0;
            enterBtn.FlatStyle = FlatStyle.Flat;
            enterBtn.Font = new Font("Segoe UI", 14F);
            enterBtn.Location = new Point(239, 376);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(141, 44);
            enterBtn.TabIndex = 6;
            enterBtn.Text = "Войти";
            enterBtn.UseVisualStyleBackColor = false;
            enterBtn.Click += enterBtn_Click;
            // 
            // AuthorizeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(618, 458);
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
            Controls.Add(mainLbl);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AuthorizeForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label mainLbl;
        private Label servNameLbl;
        private TextBox userNameTxtBox;
        private Label userNameLbl;
        private TextBox passwordTxtBox;
        private Label passwordLbl;
        private TextBox dbNameTxtBox;
        private Label dbNameLbl;
        private ComboBox servNameBox;
        private Label secCheckLbl;
        private ComboBox secCheckBox;
        private ComboBox dbNameBox;
        private controllers.RoundedButton enterBtn;
    }
}