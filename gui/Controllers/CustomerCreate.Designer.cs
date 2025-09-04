namespace gui.Controllers {
    partial class CustomerCreate {
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
            noteTxtBox = new TextBox();
            noteLbl = new Label();
            emailTxtBox = new TextBox();
            emailLbl = new Label();
            phoneTxtBox = new TextBox();
            phoneNumberLbl = new Label();
            forenameTxtBox = new TextBox();
            forenameLbl = new Label();
            surnameTxtBox = new TextBox();
            surnameLbl = new Label();
            headerLbl = new Label();
            SuspendLayout();
            // 
            // enterBtn
            // 
            enterBtn.BackColor = SystemColors.AppWorkspace;
            enterBtn.BorderRadius = 5;
            enterBtn.FlatAppearance.BorderSize = 0;
            enterBtn.FlatStyle = FlatStyle.Flat;
            enterBtn.Font = new Font("Segoe UI", 16F);
            enterBtn.Location = new Point(124, 391);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(141, 44);
            enterBtn.TabIndex = 35;
            enterBtn.Text = "Добавить";
            enterBtn.UseVisualStyleBackColor = false;
            enterBtn.Click += enterBtn_Click;
            // 
            // noteTxtBox
            // 
            noteTxtBox.Font = new Font("Segoe UI", 12F);
            noteTxtBox.Location = new Point(28, 346);
            noteTxtBox.Name = "noteTxtBox";
            noteTxtBox.Size = new Size(333, 29);
            noteTxtBox.TabIndex = 34;
            // 
            // noteLbl
            // 
            noteLbl.AutoSize = true;
            noteLbl.Font = new Font("Segoe UI", 12F);
            noteLbl.Location = new Point(28, 322);
            noteLbl.Name = "noteLbl";
            noteLbl.Size = new Size(104, 21);
            noteLbl.TabIndex = 33;
            noteLbl.Text = "Примечание:";
            // 
            // emailTxtBox
            // 
            emailTxtBox.Font = new Font("Segoe UI", 12F);
            emailTxtBox.Location = new Point(28, 284);
            emailTxtBox.Name = "emailTxtBox";
            emailTxtBox.Size = new Size(333, 29);
            emailTxtBox.TabIndex = 32;
            // 
            // emailLbl
            // 
            emailLbl.AutoSize = true;
            emailLbl.Font = new Font("Segoe UI", 12F);
            emailLbl.Location = new Point(28, 260);
            emailLbl.Name = "emailLbl";
            emailLbl.Size = new Size(198, 21);
            emailLbl.TabIndex = 31;
            emailLbl.Text = "Адрес электронной почты";
            // 
            // phoneTxtBox
            // 
            phoneTxtBox.Font = new Font("Segoe UI", 12F);
            phoneTxtBox.Location = new Point(28, 221);
            phoneTxtBox.Name = "phoneTxtBox";
            phoneTxtBox.Size = new Size(333, 29);
            phoneTxtBox.TabIndex = 30;
            // 
            // phoneNumberLbl
            // 
            phoneNumberLbl.AutoSize = true;
            phoneNumberLbl.Font = new Font("Segoe UI", 12F);
            phoneNumberLbl.Location = new Point(28, 197);
            phoneNumberLbl.Name = "phoneNumberLbl";
            phoneNumberLbl.Size = new Size(130, 21);
            phoneNumberLbl.TabIndex = 29;
            phoneNumberLbl.Text = "Номер телефона";
            // 
            // forenameTxtBox
            // 
            forenameTxtBox.Font = new Font("Segoe UI", 12F);
            forenameTxtBox.Location = new Point(28, 155);
            forenameTxtBox.Name = "forenameTxtBox";
            forenameTxtBox.Size = new Size(333, 29);
            forenameTxtBox.TabIndex = 28;
            // 
            // forenameLbl
            // 
            forenameLbl.AutoSize = true;
            forenameLbl.Font = new Font("Segoe UI", 12F);
            forenameLbl.Location = new Point(28, 131);
            forenameLbl.Name = "forenameLbl";
            forenameLbl.Size = new Size(41, 21);
            forenameLbl.TabIndex = 27;
            forenameLbl.Text = "Имя";
            // 
            // surnameTxtBox
            // 
            surnameTxtBox.Font = new Font("Segoe UI", 12F);
            surnameTxtBox.Location = new Point(28, 92);
            surnameTxtBox.Name = "surnameTxtBox";
            surnameTxtBox.Size = new Size(333, 29);
            surnameTxtBox.TabIndex = 26;
            // 
            // surnameLbl
            // 
            surnameLbl.AutoSize = true;
            surnameLbl.Font = new Font("Segoe UI", 12F);
            surnameLbl.Location = new Point(28, 68);
            surnameLbl.Name = "surnameLbl";
            surnameLbl.Size = new Size(75, 21);
            surnameLbl.TabIndex = 25;
            surnameLbl.Text = "Фамилия";
            // 
            // headerLbl
            // 
            headerLbl.AutoSize = true;
            headerLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            headerLbl.Location = new Point(51, 13);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(287, 37);
            headerLbl.TabIndex = 24;
            headerLbl.Text = "Добавить заказчика";
            // 
            // CustomerCreate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(enterBtn);
            Controls.Add(noteTxtBox);
            Controls.Add(noteLbl);
            Controls.Add(emailTxtBox);
            Controls.Add(emailLbl);
            Controls.Add(phoneTxtBox);
            Controls.Add(phoneNumberLbl);
            Controls.Add(forenameTxtBox);
            Controls.Add(forenameLbl);
            Controls.Add(surnameTxtBox);
            Controls.Add(surnameLbl);
            Controls.Add(headerLbl);
            Name = "CustomerCreate";
            Size = new Size(388, 454);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private controllers.RoundedButton enterBtn;
        private TextBox noteTxtBox;
        private Label noteLbl;
        private TextBox emailTxtBox;
        private Label emailLbl;
        private TextBox phoneTxtBox;
        private Label phoneNumberLbl;
        private TextBox forenameTxtBox;
        private Label forenameLbl;
        private TextBox surnameTxtBox;
        private Label surnameLbl;
        private Label headerLbl;
    }
}
