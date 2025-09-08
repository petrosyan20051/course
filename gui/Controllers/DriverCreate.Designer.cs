namespace gui.Controllers {
    partial class DriverCreate {
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
            licenceSeriesTxtBox = new TextBox();
            licenseSeriesLbl = new Label();
            phoneNumberTxtBox = new TextBox();
            phoneNumberLbl = new Label();
            forenameTxtBox = new TextBox();
            forenameLbl = new Label();
            surnameTxtBox = new TextBox();
            surnameLbl = new Label();
            headerLbl = new Label();
            licenceNumberTxtBox = new TextBox();
            licenceNumberLbl = new Label();
            cancelBtn = new gui.controllers.RoundedButton();
            SuspendLayout();
            // 
            // enterBtn
            // 
            enterBtn.BackColor = SystemColors.AppWorkspace;
            enterBtn.BorderRadius = 5;
            enterBtn.FlatAppearance.BorderSize = 0;
            enterBtn.FlatStyle = FlatStyle.Flat;
            enterBtn.Font = new Font("Segoe UI", 16F);
            enterBtn.Location = new Point(44, 477);
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
            noteTxtBox.Location = new Point(24, 425);
            noteTxtBox.Name = "noteTxtBox";
            noteTxtBox.Size = new Size(333, 29);
            noteTxtBox.TabIndex = 6;
            // 
            // noteLbl
            // 
            noteLbl.AutoSize = true;
            noteLbl.Font = new Font("Segoe UI", 12F);
            noteLbl.Location = new Point(24, 401);
            noteLbl.Name = "noteLbl";
            noteLbl.Size = new Size(101, 21);
            noteLbl.TabIndex = 33;
            noteLbl.Text = "Примечание";
            // 
            // licenceSeriesTxtBox
            // 
            licenceSeriesTxtBox.Font = new Font("Segoe UI", 12F);
            licenceSeriesTxtBox.Location = new Point(24, 288);
            licenceSeriesTxtBox.Name = "licenceSeriesTxtBox";
            licenceSeriesTxtBox.Size = new Size(333, 29);
            licenceSeriesTxtBox.TabIndex = 4;
            // 
            // licenseSeriesLbl
            // 
            licenseSeriesLbl.AutoSize = true;
            licenseSeriesLbl.Font = new Font("Segoe UI", 12F);
            licenseSeriesLbl.Location = new Point(24, 264);
            licenseSeriesLbl.Name = "licenseSeriesLbl";
            licenseSeriesLbl.Size = new Size(193, 21);
            licenseSeriesLbl.TabIndex = 31;
            licenseSeriesLbl.Text = "Серия водительских прав";
            // 
            // phoneNumberTxtBox
            // 
            phoneNumberTxtBox.Font = new Font("Segoe UI", 12F);
            phoneNumberTxtBox.Location = new Point(24, 225);
            phoneNumberTxtBox.Name = "phoneNumberTxtBox";
            phoneNumberTxtBox.Size = new Size(333, 29);
            phoneNumberTxtBox.TabIndex = 3;
            // 
            // phoneNumberLbl
            // 
            phoneNumberLbl.AutoSize = true;
            phoneNumberLbl.Font = new Font("Segoe UI", 12F);
            phoneNumberLbl.Location = new Point(24, 201);
            phoneNumberLbl.Name = "phoneNumberLbl";
            phoneNumberLbl.Size = new Size(130, 21);
            phoneNumberLbl.TabIndex = 29;
            phoneNumberLbl.Text = "Номер телефона";
            // 
            // forenameTxtBox
            // 
            forenameTxtBox.Font = new Font("Segoe UI", 12F);
            forenameTxtBox.Location = new Point(24, 159);
            forenameTxtBox.Name = "forenameTxtBox";
            forenameTxtBox.Size = new Size(333, 29);
            forenameTxtBox.TabIndex = 2;
            // 
            // forenameLbl
            // 
            forenameLbl.AutoSize = true;
            forenameLbl.Font = new Font("Segoe UI", 12F);
            forenameLbl.Location = new Point(24, 135);
            forenameLbl.Name = "forenameLbl";
            forenameLbl.Size = new Size(41, 21);
            forenameLbl.TabIndex = 27;
            forenameLbl.Text = "Имя";
            // 
            // surnameTxtBox
            // 
            surnameTxtBox.Font = new Font("Segoe UI", 12F);
            surnameTxtBox.Location = new Point(24, 96);
            surnameTxtBox.Name = "surnameTxtBox";
            surnameTxtBox.Size = new Size(333, 29);
            surnameTxtBox.TabIndex = 1;
            // 
            // surnameLbl
            // 
            surnameLbl.AutoSize = true;
            surnameLbl.Font = new Font("Segoe UI", 12F);
            surnameLbl.Location = new Point(24, 72);
            surnameLbl.Name = "surnameLbl";
            surnameLbl.Size = new Size(75, 21);
            surnameLbl.TabIndex = 25;
            surnameLbl.Text = "Фамилия";
            // 
            // headerLbl
            // 
            headerLbl.AutoSize = true;
            headerLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            headerLbl.Location = new Point(57, 17);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(278, 37);
            headerLbl.TabIndex = 24;
            headerLbl.Text = "Добавить водителя";
            // 
            // licenceNumberTxtBox
            // 
            licenceNumberTxtBox.Font = new Font("Segoe UI", 12F);
            licenceNumberTxtBox.Location = new Point(24, 359);
            licenceNumberTxtBox.Name = "licenceNumberTxtBox";
            licenceNumberTxtBox.Size = new Size(333, 29);
            licenceNumberTxtBox.TabIndex = 5;
            // 
            // licenceNumberLbl
            // 
            licenceNumberLbl.AutoSize = true;
            licenceNumberLbl.Font = new Font("Segoe UI", 12F);
            licenceNumberLbl.Location = new Point(24, 335);
            licenceNumberLbl.Name = "licenceNumberLbl";
            licenceNumberLbl.Size = new Size(197, 21);
            licenceNumberLbl.TabIndex = 36;
            licenceNumberLbl.Text = "Номер водительских прав";
            // 
            // cancelBtn
            // 
            cancelBtn.BackColor = SystemColors.AppWorkspace;
            cancelBtn.BorderRadius = 5;
            cancelBtn.FlatAppearance.BorderSize = 0;
            cancelBtn.FlatStyle = FlatStyle.Flat;
            cancelBtn.Font = new Font("Segoe UI", 16F);
            cancelBtn.Location = new Point(204, 477);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(141, 44);
            cancelBtn.TabIndex = 37;
            cancelBtn.Text = "Отмена";
            cancelBtn.UseVisualStyleBackColor = false;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // DriverCreate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMargin = new Size(0, 10);
            Controls.Add(cancelBtn);
            Controls.Add(licenceNumberTxtBox);
            Controls.Add(licenceNumberLbl);
            Controls.Add(enterBtn);
            Controls.Add(noteTxtBox);
            Controls.Add(noteLbl);
            Controls.Add(licenceSeriesTxtBox);
            Controls.Add(licenseSeriesLbl);
            Controls.Add(phoneNumberTxtBox);
            Controls.Add(phoneNumberLbl);
            Controls.Add(forenameTxtBox);
            Controls.Add(forenameLbl);
            Controls.Add(surnameTxtBox);
            Controls.Add(surnameLbl);
            Controls.Add(headerLbl);
            Name = "DriverCreate";
            Size = new Size(392, 557);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private controllers.RoundedButton enterBtn;
        private TextBox noteTxtBox;
        private Label noteLbl;
        private TextBox licenceSeriesTxtBox;
        private Label licenseSeriesLbl;
        private TextBox phoneNumberTxtBox;
        private Label phoneNumberLbl;
        private TextBox forenameTxtBox;
        private Label forenameLbl;
        private TextBox surnameTxtBox;
        private Label surnameLbl;
        private Label headerLbl;
        private TextBox licenceNumberTxtBox;
        private Label licenceNumberLbl;
        private controllers.RoundedButton cancelBtn;
    }
}
