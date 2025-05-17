namespace course.forms {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.controlPnl = new System.Windows.Forms.Panel();
            this.closeBtn = new course.controllers.RoundedButton();
            this.expandBtn = new course.controllers.RoundedButton();
            this.minimizeBtn = new course.controllers.RoundedButton();
            this.styleBtn = new System.Windows.Forms.Button();
            this.menuPnl = new System.Windows.Forms.Panel();
            this.gridPnl = new System.Windows.Forms.Panel();
            this.gridLbl = new System.Windows.Forms.Label();
            this.gridImage = new System.Windows.Forms.PictureBox();
            this.mainPnl = new System.Windows.Forms.Panel();
            this.mainLbl = new System.Windows.Forms.Label();
            this.mainImage = new System.Windows.Forms.PictureBox();
            this.menuSpl = new System.Windows.Forms.Splitter();
            this.controlSpl = new System.Windows.Forms.Splitter();
            this.mainPnlGrid = new System.Windows.Forms.Panel();
            this.controlPnl.SuspendLayout();
            this.menuPnl.SuspendLayout();
            this.gridPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridImage)).BeginInit();
            this.mainPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainImage)).BeginInit();
            this.SuspendLayout();
            // 
            // controlPnl
            // 
            this.controlPnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(16)))));
            this.controlPnl.Controls.Add(this.closeBtn);
            this.controlPnl.Controls.Add(this.expandBtn);
            this.controlPnl.Controls.Add(this.minimizeBtn);
            this.controlPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPnl.Location = new System.Drawing.Point(0, 0);
            this.controlPnl.Name = "controlPnl";
            this.controlPnl.Size = new System.Drawing.Size(1000, 32);
            this.controlPnl.TabIndex = 0;
            this.controlPnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.controlPnl_MouseDown);
            this.controlPnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.controlPnl_MouseMove);
            this.controlPnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.controlPnl_MouseUp);
            // 
            // closeBtn
            // 
            this.closeBtn.BorderRadius = 5;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Image = ((System.Drawing.Image)(resources.GetObject("closeBtn.Image")));
            this.closeBtn.Location = new System.Drawing.Point(935, 3);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(62, 26);
            this.closeBtn.TabIndex = 6;
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            this.closeBtn.MouseEnter += new System.EventHandler(this.closeBtn_MouseEnter);
            this.closeBtn.MouseLeave += new System.EventHandler(this.closeBtn_MouseLeave);
            // 
            // expandBtn
            // 
            this.expandBtn.BorderRadius = 5;
            this.expandBtn.FlatAppearance.BorderSize = 0;
            this.expandBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expandBtn.Image = ((System.Drawing.Image)(resources.GetObject("expandBtn.Image")));
            this.expandBtn.Location = new System.Drawing.Point(873, 3);
            this.expandBtn.Name = "expandBtn";
            this.expandBtn.Size = new System.Drawing.Size(62, 26);
            this.expandBtn.TabIndex = 5;
            this.expandBtn.UseVisualStyleBackColor = true;
            this.expandBtn.Click += new System.EventHandler(this.expandBtn_Click);
            this.expandBtn.MouseEnter += new System.EventHandler(this.expandBtn_MouseEnter);
            this.expandBtn.MouseLeave += new System.EventHandler(this.expandBtn_MouseLeave);
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.BorderRadius = 5;
            this.minimizeBtn.FlatAppearance.BorderSize = 0;
            this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeBtn.Image = ((System.Drawing.Image)(resources.GetObject("minimizeBtn.Image")));
            this.minimizeBtn.Location = new System.Drawing.Point(811, 3);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(62, 26);
            this.minimizeBtn.TabIndex = 4;
            this.minimizeBtn.UseVisualStyleBackColor = true;
            this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
            this.minimizeBtn.MouseEnter += new System.EventHandler(this.minimizeBtn_MouseEnter);
            this.minimizeBtn.MouseLeave += new System.EventHandler(this.minimizeBtn_MouseLeave);
            // 
            // styleBtn
            // 
            this.styleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(16)))));
            this.styleBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(16)))));
            this.styleBtn.FlatAppearance.BorderSize = 0;
            this.styleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.styleBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.styleBtn.Image = ((System.Drawing.Image)(resources.GetObject("styleBtn.Image")));
            this.styleBtn.Location = new System.Drawing.Point(749, 3);
            this.styleBtn.Name = "styleBtn";
            this.styleBtn.Size = new System.Drawing.Size(62, 26);
            this.styleBtn.TabIndex = 3;
            this.styleBtn.Tag = "";
            this.styleBtn.UseVisualStyleBackColor = false;
            this.styleBtn.Click += new System.EventHandler(this.styleBtn_Click);
            this.styleBtn.MouseEnter += new System.EventHandler(this.styleBtn_MouseEnter);
            this.styleBtn.MouseLeave += new System.EventHandler(this.styleBtn_MouseLeave);
            // 
            // menuPnl
            // 
            this.menuPnl.Controls.Add(this.gridPnl);
            this.menuPnl.Controls.Add(this.mainPnl);
            this.menuPnl.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPnl.Location = new System.Drawing.Point(0, 32);
            this.menuPnl.Name = "menuPnl";
            this.menuPnl.Size = new System.Drawing.Size(174, 634);
            this.menuPnl.TabIndex = 1;
            // 
            // gridPnl
            // 
            this.gridPnl.BackColor = System.Drawing.Color.Transparent;
            this.gridPnl.Controls.Add(this.gridLbl);
            this.gridPnl.Controls.Add(this.gridImage);
            this.gridPnl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridPnl.Location = new System.Drawing.Point(-1, 144);
            this.gridPnl.Name = "gridPnl";
            this.gridPnl.Size = new System.Drawing.Size(176, 79);
            this.gridPnl.TabIndex = 1;
            this.gridPnl.Click += new System.EventHandler(this.gridPnl_Click);
            // 
            // gridLbl
            // 
            this.gridLbl.AutoSize = true;
            this.gridLbl.BackColor = System.Drawing.SystemColors.Menu;
            this.gridLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridLbl.Location = new System.Drawing.Point(70, 20);
            this.gridLbl.Name = "gridLbl";
            this.gridLbl.Size = new System.Drawing.Size(82, 20);
            this.gridLbl.TabIndex = 1;
            this.gridLbl.Text = "Таблицы";
            this.gridLbl.Click += new System.EventHandler(this.gridPnl_Click);
            // 
            // gridImage
            // 
            this.gridImage.Image = ((System.Drawing.Image)(resources.GetObject("gridImage.Image")));
            this.gridImage.Location = new System.Drawing.Point(24, 20);
            this.gridImage.Name = "gridImage";
            this.gridImage.Size = new System.Drawing.Size(32, 32);
            this.gridImage.TabIndex = 0;
            this.gridImage.TabStop = false;
            this.gridImage.Click += new System.EventHandler(this.gridPnl_Click);
            // 
            // mainPnl
            // 
            this.mainPnl.BackColor = System.Drawing.Color.Transparent;
            this.mainPnl.Controls.Add(this.mainLbl);
            this.mainPnl.Controls.Add(this.mainImage);
            this.mainPnl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mainPnl.Location = new System.Drawing.Point(0, 55);
            this.mainPnl.Name = "mainPnl";
            this.mainPnl.Size = new System.Drawing.Size(176, 89);
            this.mainPnl.TabIndex = 0;
            this.mainPnl.Click += new System.EventHandler(this.mainPnl_Click);
            // 
            // mainLbl
            // 
            this.mainLbl.AutoSize = true;
            this.mainLbl.BackColor = System.Drawing.SystemColors.Menu;
            this.mainLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainLbl.Location = new System.Drawing.Point(70, 20);
            this.mainLbl.Name = "mainLbl";
            this.mainLbl.Size = new System.Drawing.Size(80, 20);
            this.mainLbl.TabIndex = 1;
            this.mainLbl.Text = "Главная";
            this.mainLbl.Click += new System.EventHandler(this.mainPnl_Click);
            // 
            // mainImage
            // 
            this.mainImage.Image = ((System.Drawing.Image)(resources.GetObject("mainImage.Image")));
            this.mainImage.Location = new System.Drawing.Point(24, 20);
            this.mainImage.Name = "mainImage";
            this.mainImage.Size = new System.Drawing.Size(32, 32);
            this.mainImage.TabIndex = 0;
            this.mainImage.TabStop = false;
            this.mainImage.Click += new System.EventHandler(this.mainPnl_Click);
            // 
            // menuSpl
            // 
            this.menuSpl.BackColor = System.Drawing.Color.IndianRed;
            this.menuSpl.Cursor = System.Windows.Forms.Cursors.Default;
            this.menuSpl.Enabled = false;
            this.menuSpl.Location = new System.Drawing.Point(174, 32);
            this.menuSpl.Name = "menuSpl";
            this.menuSpl.Size = new System.Drawing.Size(2, 634);
            this.menuSpl.TabIndex = 2;
            this.menuSpl.TabStop = false;
            // 
            // controlSpl
            // 
            this.controlSpl.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlSpl.Enabled = false;
            this.controlSpl.Location = new System.Drawing.Point(176, 32);
            this.controlSpl.Name = "controlSpl";
            this.controlSpl.Size = new System.Drawing.Size(824, 2);
            this.controlSpl.TabIndex = 3;
            this.controlSpl.TabStop = false;
            // 
            // mainPnlGrid
            // 
            this.mainPnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPnlGrid.Location = new System.Drawing.Point(176, 34);
            this.mainPnlGrid.Name = "mainPnlGrid";
            this.mainPnlGrid.Size = new System.Drawing.Size(824, 632);
            this.mainPnlGrid.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(1000, 666);
            this.ControlBox = false;
            this.Controls.Add(this.mainPnlGrid);
            this.Controls.Add(this.styleBtn);
            this.Controls.Add(this.controlSpl);
            this.Controls.Add(this.menuSpl);
            this.Controls.Add(this.menuPnl);
            this.Controls.Add(this.controlPnl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Курсовая работа";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.controlPnl.ResumeLayout(false);
            this.menuPnl.ResumeLayout(false);
            this.gridPnl.ResumeLayout(false);
            this.gridPnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridImage)).EndInit();
            this.mainPnl.ResumeLayout(false);
            this.mainPnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlPnl;
        private System.Windows.Forms.Panel menuPnl;
        private System.Windows.Forms.Splitter menuSpl;
        private System.Windows.Forms.Splitter controlSpl;
        private System.Windows.Forms.Panel mainPnl;
        private System.Windows.Forms.PictureBox mainImage;
        private System.Windows.Forms.Label mainLbl;
        private System.Windows.Forms.Panel mainPnlGrid;
        private System.Windows.Forms.Panel gridPnl;
        private System.Windows.Forms.Label gridLbl;
        private System.Windows.Forms.PictureBox gridImage;
        private System.Windows.Forms.Button styleBtn;
        private controllers.RoundedButton minimizeBtn;
        private controllers.RoundedButton expandBtn;
        private controllers.RoundedButton closeBtn;
    }
}

