namespace gui.Controllers {
    partial class OrderCreate {
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
            distanceTxtBox = new TextBox();
            distanceLbl = new Label();
            rateTxtBox = new TextBox();
            rateLbl = new Label();
            routeTxtBox = new TextBox();
            routeLbl = new Label();
            customerTxtBox = new TextBox();
            customerLbl = new Label();
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
            enterBtn.Location = new Point(206, 369);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(141, 44);
            enterBtn.TabIndex = 23;
            enterBtn.Text = "Войти";
            enterBtn.UseVisualStyleBackColor = false;
            enterBtn.Click += enterBtn_Click;
            // 
            // noteTxtBox
            // 
            noteTxtBox.Font = new Font("Segoe UI", 16F);
            noteTxtBox.Location = new Point(182, 309);
            noteTxtBox.Name = "noteTxtBox";
            noteTxtBox.Size = new Size(333, 36);
            noteTxtBox.TabIndex = 22;
            // 
            // noteLbl
            // 
            noteLbl.AutoSize = true;
            noteLbl.Font = new Font("Segoe UI", 16F);
            noteLbl.Location = new Point(17, 309);
            noteLbl.Name = "noteLbl";
            noteLbl.Size = new Size(148, 30);
            noteLbl.TabIndex = 21;
            noteLbl.Text = "Примечание:";
            // 
            // distanceTxtBox
            // 
            distanceTxtBox.Font = new Font("Segoe UI", 16F);
            distanceTxtBox.Location = new Point(182, 253);
            distanceTxtBox.Name = "distanceTxtBox";
            distanceTxtBox.Size = new Size(333, 36);
            distanceTxtBox.TabIndex = 20;
            // 
            // distanceLbl
            // 
            distanceLbl.AutoSize = true;
            distanceLbl.Font = new Font("Segoe UI", 16F);
            distanceLbl.Location = new Point(17, 253);
            distanceLbl.Name = "distanceLbl";
            distanceLbl.Size = new Size(132, 30);
            distanceLbl.TabIndex = 19;
            distanceLbl.Text = "Расстояние:";
            // 
            // rateTxtBox
            // 
            rateTxtBox.Font = new Font("Segoe UI", 16F);
            rateTxtBox.Location = new Point(182, 198);
            rateTxtBox.Name = "rateTxtBox";
            rateTxtBox.Size = new Size(333, 36);
            rateTxtBox.TabIndex = 18;
            // 
            // rateLbl
            // 
            rateLbl.AutoSize = true;
            rateLbl.Font = new Font("Segoe UI", 16F);
            rateLbl.Location = new Point(17, 198);
            rateLbl.Name = "rateLbl";
            rateLbl.Size = new Size(106, 30);
            rateLbl.TabIndex = 17;
            rateLbl.Text = "ID тариф:";
            // 
            // routeTxtBox
            // 
            routeTxtBox.Font = new Font("Segoe UI", 16F);
            routeTxtBox.Location = new Point(182, 136);
            routeTxtBox.Name = "routeTxtBox";
            routeTxtBox.Size = new Size(333, 36);
            routeTxtBox.TabIndex = 16;
            // 
            // routeLbl
            // 
            routeLbl.AutoSize = true;
            routeLbl.Font = new Font("Segoe UI", 16F);
            routeLbl.Location = new Point(17, 136);
            routeLbl.Name = "routeLbl";
            routeLbl.Size = new Size(135, 30);
            routeLbl.TabIndex = 15;
            routeLbl.Text = "ID маршрут:";
            // 
            // customerTxtBox
            // 
            customerTxtBox.Font = new Font("Segoe UI", 16F);
            customerTxtBox.Location = new Point(182, 75);
            customerTxtBox.Name = "customerTxtBox";
            customerTxtBox.Size = new Size(333, 36);
            customerTxtBox.TabIndex = 14;
            // 
            // customerLbl
            // 
            customerLbl.AutoSize = true;
            customerLbl.Font = new Font("Segoe UI", 16F);
            customerLbl.Location = new Point(17, 75);
            customerLbl.Name = "customerLbl";
            customerLbl.Size = new Size(134, 30);
            customerLbl.TabIndex = 13;
            customerLbl.Text = "ID заказчик:";
            // 
            // headerLbl
            // 
            headerLbl.AutoSize = true;
            headerLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            headerLbl.Location = new Point(164, 13);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(224, 37);
            headerLbl.TabIndex = 12;
            headerLbl.Text = "Добавить заказ";
            // 
            // OrderCreate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(enterBtn);
            Controls.Add(noteTxtBox);
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
            Name = "OrderCreate";
            Size = new Size(532, 445);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private controllers.RoundedButton enterBtn;
        private TextBox noteTxtBox;
        private Label noteLbl;
        private TextBox distanceTxtBox;
        private Label distanceLbl;
        private TextBox rateTxtBox;
        private Label rateLbl;
        private TextBox routeTxtBox;
        private Label routeLbl;
        private TextBox customerTxtBox;
        private Label customerLbl;
        private Label headerLbl;
    }
}
