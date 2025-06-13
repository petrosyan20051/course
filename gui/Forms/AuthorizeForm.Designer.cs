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
            label1 = new Label();
            ipTxtBox = new TextBox();
            userNameTxtBox = new TextBox();
            userNameLbl = new Label();
            passwordTxtBox = new TextBox();
            passwordLbl = new Label();
            enterBtn = new gui.controllers.RoundedButton();
            dbNameTxtBox = new TextBox();
            dbNameLbl = new Label();
            SuspendLayout();
            // 
            // mainLbl
            // 
            mainLbl.AutoSize = true;
            mainLbl.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 204);
            mainLbl.Location = new Point(131, 20);
            mainLbl.Name = "mainLbl";
            mainLbl.Size = new Size(228, 45);
            mainLbl.TabIndex = 0;
            mainLbl.Text = "Авторизация";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(59, 104);
            label1.Name = "label1";
            label1.Size = new Size(90, 25);
            label1.TabIndex = 1;
            label1.Text = "IP-адрес:";
            // 
            // ipTxtBox
            // 
            ipTxtBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ipTxtBox.Location = new Point(155, 104);
            ipTxtBox.Name = "ipTxtBox";
            ipTxtBox.Size = new Size(233, 32);
            ipTxtBox.TabIndex = 2;
            // 
            // userNameTxtBox
            // 
            userNameTxtBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            userNameTxtBox.Location = new Point(155, 216);
            userNameTxtBox.Name = "userNameTxtBox";
            userNameTxtBox.Size = new Size(233, 32);
            userNameTxtBox.TabIndex = 4;
            // 
            // userNameLbl
            // 
            userNameLbl.AutoSize = true;
            userNameLbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            userNameLbl.Location = new Point(80, 219);
            userNameLbl.Name = "userNameLbl";
            userNameLbl.Size = new Size(69, 25);
            userNameLbl.TabIndex = 3;
            userNameLbl.Text = "Логин:";
            // 
            // passwordTxtBox
            // 
            passwordTxtBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            passwordTxtBox.Location = new Point(155, 269);
            passwordTxtBox.Name = "passwordTxtBox";
            passwordTxtBox.Size = new Size(233, 32);
            passwordTxtBox.TabIndex = 5;
            // 
            // passwordLbl
            // 
            passwordLbl.AutoSize = true;
            passwordLbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            passwordLbl.Location = new Point(67, 272);
            passwordLbl.Name = "passwordLbl";
            passwordLbl.Size = new Size(82, 25);
            passwordLbl.TabIndex = 5;
            passwordLbl.Text = "Пароль:";
            // 
            // enterBtn
            // 
            enterBtn.BackColor = SystemColors.ButtonShadow;
            enterBtn.BorderRadius = 5;
            enterBtn.FlatAppearance.BorderSize = 0;
            enterBtn.FlatStyle = FlatStyle.Flat;
            enterBtn.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            enterBtn.Location = new Point(169, 323);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(153, 57);
            enterBtn.TabIndex = 7;
            enterBtn.Text = "Войти";
            enterBtn.UseVisualStyleBackColor = false;
            enterBtn.Click += enterBtn_Click;
            // 
            // dbNameTxtBox
            // 
            dbNameTxtBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dbNameTxtBox.Location = new Point(155, 160);
            dbNameTxtBox.Name = "dbNameTxtBox";
            dbNameTxtBox.Size = new Size(233, 32);
            dbNameTxtBox.TabIndex = 3;
            // 
            // dbNameLbl
            // 
            dbNameLbl.AutoSize = true;
            dbNameLbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dbNameLbl.Location = new Point(25, 163);
            dbNameLbl.Name = "dbNameLbl";
            dbNameLbl.Size = new Size(124, 25);
            dbNameLbl.TabIndex = 8;
            dbNameLbl.Text = "База данных:";
            // 
            // AuthorizeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 418);
            Controls.Add(dbNameTxtBox);
            Controls.Add(dbNameLbl);
            Controls.Add(enterBtn);
            Controls.Add(passwordTxtBox);
            Controls.Add(passwordLbl);
            Controls.Add(userNameTxtBox);
            Controls.Add(userNameLbl);
            Controls.Add(ipTxtBox);
            Controls.Add(label1);
            Controls.Add(mainLbl);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AuthorizeForm";
            Text = "Авторизация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label mainLbl;
        private Label label1;
        private TextBox ipTxtBox;
        private TextBox userNameTxtBox;
        private Label userNameLbl;
        private TextBox passwordTxtBox;
        private Label passwordLbl;
        private controllers.RoundedButton enterBtn;
        private TextBox dbNameTxtBox;
        private Label dbNameLbl;
    }
}