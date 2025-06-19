namespace gui.Forms {
    partial class OrderForm {
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
            headerLbl = new Label();
            customerLbl = new Label();
            customerTxtBox = new TextBox();
            routeTxtBox = new TextBox();
            routeLbl = new Label();
            rateTxtBox = new TextBox();
            rateLbl = new Label();
            distanceTxtBox = new TextBox();
            distanceLbl = new Label();
            notetxtBox = new TextBox();
            noteLbl = new Label();
            enterBtn = new gui.controllers.RoundedButton();
            SuspendLayout();
            // 
            // headerLbl
            // 
            headerLbl.AutoSize = true;
            headerLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            headerLbl.Location = new Point(159, 22);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(224, 37);
            headerLbl.TabIndex = 0;
            headerLbl.Text = "Добавить заказ";
            // 
            // customerLbl
            // 
            customerLbl.AutoSize = true;
            customerLbl.Font = new Font("Segoe UI", 16F);
            customerLbl.Location = new Point(12, 84);
            customerLbl.Name = "customerLbl";
            customerLbl.Size = new Size(134, 30);
            customerLbl.TabIndex = 1;
            customerLbl.Text = "ID заказчик:";
            // 
            // customerTxtBox
            // 
            customerTxtBox.Font = new Font("Segoe UI", 16F);
            customerTxtBox.Location = new Point(177, 84);
            customerTxtBox.Name = "customerTxtBox";
            customerTxtBox.Size = new Size(333, 36);
            customerTxtBox.TabIndex = 2;
            // 
            // routeTxtBox
            // 
            routeTxtBox.Font = new Font("Segoe UI", 16F);
            routeTxtBox.Location = new Point(177, 145);
            routeTxtBox.Name = "routeTxtBox";
            routeTxtBox.Size = new Size(333, 36);
            routeTxtBox.TabIndex = 4;
            // 
            // routeLbl
            // 
            routeLbl.AutoSize = true;
            routeLbl.Font = new Font("Segoe UI", 16F);
            routeLbl.Location = new Point(12, 145);
            routeLbl.Name = "routeLbl";
            routeLbl.Size = new Size(135, 30);
            routeLbl.TabIndex = 3;
            routeLbl.Text = "ID маршрут:";
            // 
            // rateTxtBox
            // 
            rateTxtBox.Font = new Font("Segoe UI", 16F);
            rateTxtBox.Location = new Point(177, 207);
            rateTxtBox.Name = "rateTxtBox";
            rateTxtBox.Size = new Size(333, 36);
            rateTxtBox.TabIndex = 6;
            // 
            // rateLbl
            // 
            rateLbl.AutoSize = true;
            rateLbl.Font = new Font("Segoe UI", 16F);
            rateLbl.Location = new Point(12, 207);
            rateLbl.Name = "rateLbl";
            rateLbl.Size = new Size(106, 30);
            rateLbl.TabIndex = 5;
            rateLbl.Text = "ID тариф:";
            // 
            // distanceTxtBox
            // 
            distanceTxtBox.Font = new Font("Segoe UI", 16F);
            distanceTxtBox.Location = new Point(177, 262);
            distanceTxtBox.Name = "distanceTxtBox";
            distanceTxtBox.Size = new Size(333, 36);
            distanceTxtBox.TabIndex = 8;
            // 
            // distanceLbl
            // 
            distanceLbl.AutoSize = true;
            distanceLbl.Font = new Font("Segoe UI", 16F);
            distanceLbl.Location = new Point(12, 262);
            distanceLbl.Name = "distanceLbl";
            distanceLbl.Size = new Size(132, 30);
            distanceLbl.TabIndex = 7;
            distanceLbl.Text = "Расстояние:";
            // 
            // notetxtBox
            // 
            notetxtBox.Font = new Font("Segoe UI", 16F);
            notetxtBox.Location = new Point(177, 318);
            notetxtBox.Name = "notetxtBox";
            notetxtBox.Size = new Size(333, 36);
            notetxtBox.TabIndex = 10;
            // 
            // noteLbl
            // 
            noteLbl.AutoSize = true;
            noteLbl.Font = new Font("Segoe UI", 16F);
            noteLbl.Location = new Point(12, 318);
            noteLbl.Name = "noteLbl";
            noteLbl.Size = new Size(148, 30);
            noteLbl.TabIndex = 9;
            noteLbl.Text = "Примечание:";
            // 
            // enterBtn
            // 
            enterBtn.BackColor = SystemColors.AppWorkspace;
            enterBtn.BorderRadius = 5;
            enterBtn.FlatAppearance.BorderSize = 0;
            enterBtn.FlatStyle = FlatStyle.Flat;
            enterBtn.Font = new Font("Segoe UI", 16F);
            enterBtn.Location = new Point(201, 378);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(141, 44);
            enterBtn.TabIndex = 11;
            enterBtn.Text = "Войти";
            enterBtn.UseVisualStyleBackColor = false;
            enterBtn.Click += enterBtn_Click;
            // 
            // OrderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 450);
            Controls.Add(enterBtn);
            Controls.Add(notetxtBox);
            Controls.Add(noteLbl);
            Controls.Add(distanceTxtBox);
            Controls.Add(distanceLbl);
            Controls.Add(rateTxtBox);
            Controls.Add(rateLbl);
            Controls.Add(routeTxtBox);
            Controls.Add(routeLbl);
            Controls.Add(customerTxtBox);
            Controls.Add(customerLbl);
            Controls.Add(headerLbl);
            FormBorderStyle = FormBorderStyle.None;
            Name = "OrderForm";
            ShowIcon = false;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label headerLbl;
        private Label customerLbl;
        private TextBox customerTxtBox;
        private TextBox routeTxtBox;
        private Label routeLbl;
        private TextBox rateTxtBox;
        private Label rateLbl;
        private TextBox distanceTxtBox;
        private Label distanceLbl;
        private TextBox notetxtBox;
        private Label noteLbl;
        private controllers.RoundedButton enterBtn;
    }
}