namespace gui.Controllers {
    partial class RateCreate {
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
            movePriceTxtBox = new TextBox();
            movePriceLbl = new Label();
            vehicleIdTxtBox = new TextBox();
            vehicleIdLbl = new Label();
            driverIdTxtBox = new TextBox();
            driverIdLbl = new Label();
            forenameTxtBox = new TextBox();
            forenameLbl = new Label();
            headerLbl = new Label();
            idlePriceTxtBox = new TextBox();
            idlePriceLbl = new Label();
            roundedButton1 = new gui.controllers.RoundedButton();
            SuspendLayout();
            // 
            // enterBtn
            // 
            enterBtn.BackColor = SystemColors.AppWorkspace;
            enterBtn.BorderRadius = 5;
            enterBtn.FlatAppearance.BorderSize = 0;
            enterBtn.FlatStyle = FlatStyle.Flat;
            enterBtn.Font = new Font("Segoe UI", 16F);
            enterBtn.Location = new Point(56, 473);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(141, 44);
            enterBtn.TabIndex = 35;
            enterBtn.Text = "Добавить";
            enterBtn.UseVisualStyleBackColor = false;
            // 
            // noteTxtBox
            // 
            noteTxtBox.Font = new Font("Segoe UI", 12F);
            noteTxtBox.Location = new Point(30, 421);
            noteTxtBox.Name = "noteTxtBox";
            noteTxtBox.Size = new Size(333, 29);
            noteTxtBox.TabIndex = 34;
            // 
            // noteLbl
            // 
            noteLbl.AutoSize = true;
            noteLbl.Font = new Font("Segoe UI", 12F);
            noteLbl.Location = new Point(30, 397);
            noteLbl.Name = "noteLbl";
            noteLbl.Size = new Size(101, 21);
            noteLbl.TabIndex = 33;
            noteLbl.Text = "Примечание";
            // 
            // movePriceTxtBox
            // 
            movePriceTxtBox.Font = new Font("Segoe UI", 12F);
            movePriceTxtBox.Location = new Point(30, 288);
            movePriceTxtBox.Name = "movePriceTxtBox";
            movePriceTxtBox.Size = new Size(333, 29);
            movePriceTxtBox.TabIndex = 32;
            // 
            // movePriceLbl
            // 
            movePriceLbl.AutoSize = true;
            movePriceLbl.Font = new Font("Segoe UI", 12F);
            movePriceLbl.Location = new Point(30, 264);
            movePriceLbl.Name = "movePriceLbl";
            movePriceLbl.Size = new Size(191, 21);
            movePriceLbl.TabIndex = 31;
            movePriceLbl.Text = "Цена поездки (руб./мин.)";
            // 
            // vehicleIdTxtBox
            // 
            vehicleIdTxtBox.Font = new Font("Segoe UI", 12F);
            vehicleIdTxtBox.Location = new Point(30, 225);
            vehicleIdTxtBox.Name = "vehicleIdTxtBox";
            vehicleIdTxtBox.Size = new Size(333, 29);
            vehicleIdTxtBox.TabIndex = 30;
            // 
            // vehicleIdLbl
            // 
            vehicleIdLbl.AutoSize = true;
            vehicleIdLbl.Font = new Font("Segoe UI", 12F);
            vehicleIdLbl.Location = new Point(30, 201);
            vehicleIdLbl.Name = "vehicleIdLbl";
            vehicleIdLbl.Size = new Size(197, 21);
            vehicleIdLbl.TabIndex = 29;
            vehicleIdLbl.Text = "ID транспортное средство";
            // 
            // driverIdTxtBox
            // 
            driverIdTxtBox.Font = new Font("Segoe UI", 12F);
            driverIdTxtBox.Location = new Point(30, 159);
            driverIdTxtBox.Name = "driverIdTxtBox";
            driverIdTxtBox.Size = new Size(333, 29);
            driverIdTxtBox.TabIndex = 28;
            // 
            // driverIdLbl
            // 
            driverIdLbl.AutoSize = true;
            driverIdLbl.Font = new Font("Segoe UI", 12F);
            driverIdLbl.Location = new Point(30, 135);
            driverIdLbl.Name = "driverIdLbl";
            driverIdLbl.Size = new Size(95, 21);
            driverIdLbl.TabIndex = 27;
            driverIdLbl.Text = "ID водитель";
            // 
            // forenameTxtBox
            // 
            forenameTxtBox.Font = new Font("Segoe UI", 12F);
            forenameTxtBox.Location = new Point(30, 96);
            forenameTxtBox.Name = "forenameTxtBox";
            forenameTxtBox.Size = new Size(333, 29);
            forenameTxtBox.TabIndex = 26;
            // 
            // forenameLbl
            // 
            forenameLbl.AutoSize = true;
            forenameLbl.Font = new Font("Segoe UI", 12F);
            forenameLbl.Location = new Point(30, 72);
            forenameLbl.Name = "forenameLbl";
            forenameLbl.Size = new Size(78, 21);
            forenameLbl.TabIndex = 25;
            forenameLbl.Text = "Название";
            // 
            // headerLbl
            // 
            headerLbl.AutoSize = true;
            headerLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            headerLbl.Location = new Point(60, 17);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(273, 37);
            headerLbl.TabIndex = 24;
            headerLbl.Text = "Добавить маршрут";
            // 
            // idlePriceTxtBox
            // 
            idlePriceTxtBox.Font = new Font("Segoe UI", 12F);
            idlePriceTxtBox.Location = new Point(30, 353);
            idlePriceTxtBox.Name = "idlePriceTxtBox";
            idlePriceTxtBox.Size = new Size(333, 29);
            idlePriceTxtBox.TabIndex = 37;
            // 
            // idlePriceLbl
            // 
            idlePriceLbl.AutoSize = true;
            idlePriceLbl.Font = new Font("Segoe UI", 12F);
            idlePriceLbl.Location = new Point(30, 329);
            idlePriceLbl.Name = "idlePriceLbl";
            idlePriceLbl.Size = new Size(262, 21);
            idlePriceLbl.TabIndex = 36;
            idlePriceLbl.Text = "Цена ожидания подачи (руб./мин.)";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = SystemColors.AppWorkspace;
            roundedButton1.BorderRadius = 5;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Segoe UI", 16F);
            roundedButton1.Location = new Point(212, 473);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(141, 44);
            roundedButton1.TabIndex = 38;
            roundedButton1.Text = "Отмена";
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // RateCreate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMargin = new Size(0, 10);
            Controls.Add(roundedButton1);
            Controls.Add(idlePriceTxtBox);
            Controls.Add(idlePriceLbl);
            Controls.Add(enterBtn);
            Controls.Add(noteTxtBox);
            Controls.Add(noteLbl);
            Controls.Add(movePriceTxtBox);
            Controls.Add(movePriceLbl);
            Controls.Add(vehicleIdTxtBox);
            Controls.Add(vehicleIdLbl);
            Controls.Add(driverIdTxtBox);
            Controls.Add(driverIdLbl);
            Controls.Add(forenameTxtBox);
            Controls.Add(forenameLbl);
            Controls.Add(headerLbl);
            Name = "RateCreate";
            Size = new Size(392, 548);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private controllers.RoundedButton enterBtn;
        private TextBox noteTxtBox;
        private Label noteLbl;
        private TextBox movePriceTxtBox;
        private Label movePriceLbl;
        private TextBox vehicleIdTxtBox;
        private Label vehicleIdLbl;
        private TextBox driverIdTxtBox;
        private Label driverIdLbl;
        private TextBox forenameTxtBox;
        private Label forenameLbl;
        private Label headerLbl;
        private TextBox idlePriceTxtBox;
        private Label idlePriceLbl;
        private controllers.RoundedButton roundedButton1;
    }
}
