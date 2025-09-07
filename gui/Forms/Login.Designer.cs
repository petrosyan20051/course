namespace gui.Forms {
    partial class Login {
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
            actionStrip = new MenuStrip();
            authorizeItem = new ToolStripMenuItem();
            registrateItem = new ToolStripMenuItem();
            actionStrip.SuspendLayout();
            SuspendLayout();
            // 
            // actionStrip
            // 
            actionStrip.BackColor = SystemColors.GradientInactiveCaption;
            actionStrip.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            actionStrip.Items.AddRange(new ToolStripItem[] { authorizeItem, registrateItem });
            actionStrip.Location = new Point(0, 0);
            actionStrip.Name = "actionStrip";
            actionStrip.RightToLeft = RightToLeft.No;
            actionStrip.Size = new Size(499, 33);
            actionStrip.TabIndex = 13;
            actionStrip.Text = "actionsStrip";
            actionStrip.ItemClicked += actionStrip_ItemClicked;
            // 
            // authorizeItem
            // 
            authorizeItem.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            authorizeItem.Name = "authorizeItem";
            authorizeItem.Size = new Size(147, 29);
            authorizeItem.Text = "Авторизация";
            // 
            // registrateItem
            // 
            registrateItem.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            registrateItem.ForeColor = SystemColors.ControlText;
            registrateItem.Name = "registrateItem";
            registrateItem.Size = new Size(140, 29);
            registrateItem.Text = "Регистрация";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(499, 443);
            Controls.Add(actionStrip);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = actionStrip;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вход в систему";
            actionStrip.ResumeLayout(false);
            actionStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox dbNameTxtBox;
        private MenuStrip actionStrip;
        private ToolStripMenuItem authorizeItem;
        private ToolStripMenuItem registrateItem;
    }
}