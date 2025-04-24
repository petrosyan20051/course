namespace course {
    partial class MainForm {
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
            this.controlPnl = new System.Windows.Forms.Panel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.expandBtn = new System.Windows.Forms.Button();
            this.rollBtn = new System.Windows.Forms.Button();
            this.controlPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlPnl
            // 
            this.controlPnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(16)))));
            this.controlPnl.Controls.Add(this.closeBtn);
            this.controlPnl.Controls.Add(this.expandBtn);
            this.controlPnl.Controls.Add(this.rollBtn);
            this.controlPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPnl.Location = new System.Drawing.Point(0, 0);
            this.controlPnl.Name = "controlPnl";
            this.controlPnl.Size = new System.Drawing.Size(989, 32);
            this.controlPnl.TabIndex = 0;
            this.controlPnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.controlPnl_MouseDown);
            this.controlPnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.controlPnl_MouseMove);
            this.controlPnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.controlPnl_MouseUp);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Black;
            this.closeBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(16)))));
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.closeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.closeBtn.Location = new System.Drawing.Point(924, 3);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(62, 26);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.Text = "X";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.closeBtn_MouseClick);
            this.closeBtn.MouseEnter += new System.EventHandler(this.closeBtn_MouseEnter);
            this.closeBtn.MouseLeave += new System.EventHandler(this.closeBtn_MouseLeave);
            // 
            // expandBtn
            // 
            this.expandBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(16)))));
            this.expandBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(16)))));
            this.expandBtn.FlatAppearance.BorderSize = 0;
            this.expandBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expandBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.expandBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.expandBtn.Location = new System.Drawing.Point(856, 3);
            this.expandBtn.Name = "expandBtn";
            this.expandBtn.Size = new System.Drawing.Size(62, 26);
            this.expandBtn.TabIndex = 1;
            this.expandBtn.Text = "▭";
            this.expandBtn.UseVisualStyleBackColor = false;
            this.expandBtn.MouseEnter += new System.EventHandler(this.expandBtn_MouseEnter);
            this.expandBtn.MouseLeave += new System.EventHandler(this.expandBtn_MouseLeave);
            // 
            // rollBtn
            // 
            this.rollBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(16)))));
            this.rollBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(16)))));
            this.rollBtn.FlatAppearance.BorderSize = 0;
            this.rollBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rollBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rollBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rollBtn.Location = new System.Drawing.Point(788, 3);
            this.rollBtn.Name = "rollBtn";
            this.rollBtn.Size = new System.Drawing.Size(62, 26);
            this.rollBtn.TabIndex = 2;
            this.rollBtn.Text = "-";
            this.rollBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rollBtn.UseVisualStyleBackColor = false;
            this.rollBtn.MouseEnter += new System.EventHandler(this.rollBtn_MouseEnter);
            this.rollBtn.MouseLeave += new System.EventHandler(this.rollBtn_MouseLeave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(989, 666);
            this.ControlBox = false;
            this.Controls.Add(this.controlPnl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Курсовая работа";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.controlPnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlPnl;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button expandBtn;
        private System.Windows.Forms.Button rollBtn;
    }
}

