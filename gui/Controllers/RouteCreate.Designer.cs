namespace gui.Controllers {
    partial class RouteCreate {
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
            dropAddressTxtBox = new TextBox();
            dropAddressLbl = new Label();
            boardingAddressTxtBox = new TextBox();
            boardingAddressLbl = new Label();
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
            enterBtn.Location = new Point(126, 281);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(141, 44);
            enterBtn.TabIndex = 4;
            enterBtn.Text = "Добавить";
            enterBtn.UseVisualStyleBackColor = false;
            enterBtn.Click += enterBtn_Click;
            // 
            // noteTxtBox
            // 
            noteTxtBox.Font = new Font("Segoe UI", 12F);
            noteTxtBox.Location = new Point(30, 229);
            noteTxtBox.Name = "noteTxtBox";
            noteTxtBox.Size = new Size(333, 29);
            noteTxtBox.TabIndex = 3;
            // 
            // noteLbl
            // 
            noteLbl.AutoSize = true;
            noteLbl.Font = new Font("Segoe UI", 12F);
            noteLbl.Location = new Point(30, 205);
            noteLbl.Name = "noteLbl";
            noteLbl.Size = new Size(101, 21);
            noteLbl.TabIndex = 33;
            noteLbl.Text = "Примечание";
            // 
            // dropAddressTxtBox
            // 
            dropAddressTxtBox.Font = new Font("Segoe UI", 12F);
            dropAddressTxtBox.Location = new Point(30, 159);
            dropAddressTxtBox.Name = "dropAddressTxtBox";
            dropAddressTxtBox.Size = new Size(333, 29);
            dropAddressTxtBox.TabIndex = 2;
            // 
            // dropAddressLbl
            // 
            dropAddressLbl.AutoSize = true;
            dropAddressLbl.Font = new Font("Segoe UI", 12F);
            dropAddressLbl.Location = new Point(30, 135);
            dropAddressLbl.Name = "dropAddressLbl";
            dropAddressLbl.Size = new Size(117, 21);
            dropAddressLbl.TabIndex = 27;
            dropAddressLbl.Text = "Адрес высадки";
            // 
            // boardingAddressTxtBox
            // 
            boardingAddressTxtBox.Font = new Font("Segoe UI", 12F);
            boardingAddressTxtBox.Location = new Point(30, 96);
            boardingAddressTxtBox.Name = "boardingAddressTxtBox";
            boardingAddressTxtBox.Size = new Size(333, 29);
            boardingAddressTxtBox.TabIndex = 1;
            // 
            // boardingAddressLbl
            // 
            boardingAddressLbl.AutoSize = true;
            boardingAddressLbl.Font = new Font("Segoe UI", 12F);
            boardingAddressLbl.Location = new Point(30, 72);
            boardingAddressLbl.Name = "boardingAddressLbl";
            boardingAddressLbl.Size = new Size(116, 21);
            boardingAddressLbl.TabIndex = 25;
            boardingAddressLbl.Text = "Адрес посадки";
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
            // RouteCreate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(enterBtn);
            Controls.Add(noteTxtBox);
            Controls.Add(noteLbl);
            Controls.Add(dropAddressTxtBox);
            Controls.Add(dropAddressLbl);
            Controls.Add(boardingAddressTxtBox);
            Controls.Add(boardingAddressLbl);
            Controls.Add(headerLbl);
            Name = "RouteCreate";
            Size = new Size(392, 343);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private controllers.RoundedButton enterBtn;
        private TextBox noteTxtBox;
        private Label noteLbl;
        private TextBox dropAddressTxtBox;
        private Label dropAddressLbl;
        private TextBox boardingAddressTxtBox;
        private Label boardingAddressLbl;
        private Label headerLbl;
    }
}
