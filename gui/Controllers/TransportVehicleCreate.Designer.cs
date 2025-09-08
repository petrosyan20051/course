namespace gui.Controllers {
    partial class TransportVehicleCreate {
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
            registrationCodeTxtBox = new TextBox();
            registrationCodeLbl = new Label();
            seriesTxtBox = new TextBox();
            seriesLbl = new Label();
            numberTxtBox = new TextBox();
            numberLbl = new Label();
            driverIdTxtBox = new TextBox();
            driverIdLbl = new Label();
            headerLbl = new Label();
            modelTxtBox = new TextBox();
            modelLbl = new Label();
            colorTxtBox = new TextBox();
            colorLbl = new Label();
            releaseYearTxtBox = new TextBox();
            releaseYearLbl = new Label();
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
            enterBtn.Location = new Point(132, 600);
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
            noteTxtBox.Location = new Point(30, 558);
            noteTxtBox.Name = "noteTxtBox";
            noteTxtBox.Size = new Size(514, 29);
            noteTxtBox.TabIndex = 34;
            // 
            // noteLbl
            // 
            noteLbl.AutoSize = true;
            noteLbl.Font = new Font("Segoe UI", 12F);
            noteLbl.Location = new Point(30, 534);
            noteLbl.Name = "noteLbl";
            noteLbl.Size = new Size(101, 21);
            noteLbl.TabIndex = 33;
            noteLbl.Text = "Примечание";
            // 
            // registrationCodeTxtBox
            // 
            registrationCodeTxtBox.Font = new Font("Segoe UI", 12F);
            registrationCodeTxtBox.Location = new Point(30, 288);
            registrationCodeTxtBox.Name = "registrationCodeTxtBox";
            registrationCodeTxtBox.Size = new Size(514, 29);
            registrationCodeTxtBox.TabIndex = 4;
            // 
            // registrationCodeLbl
            // 
            registrationCodeLbl.AutoSize = true;
            registrationCodeLbl.Font = new Font("Segoe UI", 12F);
            registrationCodeLbl.Location = new Point(30, 264);
            registrationCodeLbl.Name = "registrationCodeLbl";
            registrationCodeLbl.Size = new Size(132, 21);
            registrationCodeLbl.TabIndex = 31;
            registrationCodeLbl.Text = "Код регистрации";
            // 
            // seriesTxtBox
            // 
            seriesTxtBox.Font = new Font("Segoe UI", 12F);
            seriesTxtBox.Location = new Point(30, 225);
            seriesTxtBox.Name = "seriesTxtBox";
            seriesTxtBox.Size = new Size(514, 29);
            seriesTxtBox.TabIndex = 3;
            // 
            // seriesLbl
            // 
            seriesLbl.AutoSize = true;
            seriesLbl.Font = new Font("Segoe UI", 12F);
            seriesLbl.Location = new Point(30, 201);
            seriesLbl.Name = "seriesLbl";
            seriesLbl.Size = new Size(54, 21);
            seriesLbl.TabIndex = 29;
            seriesLbl.Text = "Серия";
            // 
            // numberTxtBox
            // 
            numberTxtBox.Font = new Font("Segoe UI", 12F);
            numberTxtBox.Location = new Point(30, 159);
            numberTxtBox.Name = "numberTxtBox";
            numberTxtBox.Size = new Size(514, 29);
            numberTxtBox.TabIndex = 2;
            // 
            // numberLbl
            // 
            numberLbl.AutoSize = true;
            numberLbl.Font = new Font("Segoe UI", 12F);
            numberLbl.Location = new Point(30, 135);
            numberLbl.Name = "numberLbl";
            numberLbl.Size = new Size(236, 21);
            numberLbl.TabIndex = 27;
            numberLbl.Text = "Номер транспортного средства";
            // 
            // driverIdTxtBox
            // 
            driverIdTxtBox.Font = new Font("Segoe UI", 12F);
            driverIdTxtBox.Location = new Point(30, 96);
            driverIdTxtBox.Name = "driverIdTxtBox";
            driverIdTxtBox.Size = new Size(514, 29);
            driverIdTxtBox.TabIndex = 1;
            // 
            // driverIdLbl
            // 
            driverIdLbl.AutoSize = true;
            driverIdLbl.Font = new Font("Segoe UI", 12F);
            driverIdLbl.Location = new Point(30, 72);
            driverIdLbl.Name = "driverIdLbl";
            driverIdLbl.Size = new Size(95, 21);
            driverIdLbl.TabIndex = 25;
            driverIdLbl.Text = "ID водитель";
            // 
            // headerLbl
            // 
            headerLbl.AutoSize = true;
            headerLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            headerLbl.Location = new Point(58, 17);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(466, 37);
            headerLbl.TabIndex = 24;
            headerLbl.Text = "Добавить транспортное средство";
            // 
            // modelTxtBox
            // 
            modelTxtBox.Font = new Font("Segoe UI", 12F);
            modelTxtBox.Location = new Point(30, 357);
            modelTxtBox.Name = "modelTxtBox";
            modelTxtBox.Size = new Size(514, 29);
            modelTxtBox.TabIndex = 5;
            // 
            // modelLbl
            // 
            modelLbl.AutoSize = true;
            modelLbl.Font = new Font("Segoe UI", 12F);
            modelLbl.Location = new Point(30, 333);
            modelLbl.Name = "modelLbl";
            modelLbl.Size = new Size(66, 21);
            modelLbl.TabIndex = 36;
            modelLbl.Text = "Модель";
            // 
            // colorTxtBox
            // 
            colorTxtBox.Font = new Font("Segoe UI", 12F);
            colorTxtBox.Location = new Point(30, 425);
            colorTxtBox.Name = "colorTxtBox";
            colorTxtBox.Size = new Size(514, 29);
            colorTxtBox.TabIndex = 37;
            // 
            // colorLbl
            // 
            colorLbl.AutoSize = true;
            colorLbl.Font = new Font("Segoe UI", 12F);
            colorLbl.Location = new Point(30, 401);
            colorLbl.Name = "colorLbl";
            colorLbl.Size = new Size(45, 21);
            colorLbl.TabIndex = 38;
            colorLbl.Text = "Цвет";
            // 
            // releaseYearTxtBox
            // 
            releaseYearTxtBox.Font = new Font("Segoe UI", 12F);
            releaseYearTxtBox.Location = new Point(30, 491);
            releaseYearTxtBox.Name = "releaseYearTxtBox";
            releaseYearTxtBox.Size = new Size(514, 29);
            releaseYearTxtBox.TabIndex = 39;
            // 
            // releaseYearLbl
            // 
            releaseYearLbl.AutoSize = true;
            releaseYearLbl.Font = new Font("Segoe UI", 12F);
            releaseYearLbl.Location = new Point(30, 467);
            releaseYearLbl.Name = "releaseYearLbl";
            releaseYearLbl.Size = new Size(99, 21);
            releaseYearLbl.TabIndex = 40;
            releaseYearLbl.Text = "Год выпуска";
            // 
            // cancelBtn
            // 
            cancelBtn.BackColor = SystemColors.AppWorkspace;
            cancelBtn.BorderRadius = 5;
            cancelBtn.FlatAppearance.BorderSize = 0;
            cancelBtn.FlatStyle = FlatStyle.Flat;
            cancelBtn.Font = new Font("Segoe UI", 16F);
            cancelBtn.Location = new Point(306, 600);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(141, 44);
            cancelBtn.TabIndex = 41;
            cancelBtn.Text = "Отмена";
            cancelBtn.UseVisualStyleBackColor = false;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // TransportVehicleCreate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMargin = new Size(0, 10);
            Controls.Add(cancelBtn);
            Controls.Add(releaseYearTxtBox);
            Controls.Add(releaseYearLbl);
            Controls.Add(colorTxtBox);
            Controls.Add(colorLbl);
            Controls.Add(modelTxtBox);
            Controls.Add(modelLbl);
            Controls.Add(enterBtn);
            Controls.Add(noteTxtBox);
            Controls.Add(noteLbl);
            Controls.Add(registrationCodeTxtBox);
            Controls.Add(registrationCodeLbl);
            Controls.Add(seriesTxtBox);
            Controls.Add(seriesLbl);
            Controls.Add(numberTxtBox);
            Controls.Add(numberLbl);
            Controls.Add(driverIdTxtBox);
            Controls.Add(driverIdLbl);
            Controls.Add(headerLbl);
            Name = "TransportVehicleCreate";
            Size = new Size(582, 678);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private controllers.RoundedButton enterBtn;
        private TextBox noteTxtBox;
        private Label noteLbl;
        private TextBox registrationCodeTxtBox;
        private Label registrationCodeLbl;
        private TextBox seriesTxtBox;
        private Label seriesLbl;
        private TextBox numberTxtBox;
        private Label numberLbl;
        private TextBox driverIdTxtBox;
        private Label driverIdLbl;
        private Label headerLbl;
        private TextBox modelTxtBox;
        private Label modelLbl;
        private TextBox colorTxtBox;
        private Label colorLbl;
        private TextBox releaseYearTxtBox;
        private Label releaseYearLbl;
        private controllers.RoundedButton cancelBtn;
    }
}
