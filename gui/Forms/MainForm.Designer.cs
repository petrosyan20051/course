namespace gui.Forms {
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            controlPnl = new Panel();
            userBtn = new gui.controllers.RoundedButton();
            styleBtn = new gui.controllers.RoundedButton();
            closeBtn = new gui.controllers.RoundedButton();
            expandBtn = new gui.controllers.RoundedButton();
            minimizeBtn = new gui.controllers.RoundedButton();
            menuPnl = new Panel();
            gridPnl = new Panel();
            gridLbl = new Label();
            gridImage = new PictureBox();
            mainPnl = new Panel();
            mainLbl = new Label();
            mainImage = new PictureBox();
            menuSpl = new Splitter();
            controlSpl = new Splitter();
            mainPnlGrid = new Panel();
            styleTip = new ToolTip(components);
            minimizeTip = new ToolTip(components);
            expandTip = new ToolTip(components);
            closeTip = new ToolTip(components);
            userTip = new ToolTip(components);
            controlPnl.SuspendLayout();
            menuPnl.SuspendLayout();
            gridPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridImage).BeginInit();
            mainPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainImage).BeginInit();
            SuspendLayout();
            // 
            // controlPnl
            // 
            controlPnl.BackColor = Color.FromArgb(12, 12, 16);
            controlPnl.Controls.Add(userBtn);
            controlPnl.Controls.Add(styleBtn);
            controlPnl.Controls.Add(minimizeBtn);
            controlPnl.Controls.Add(expandBtn);
            controlPnl.Controls.Add(closeBtn);
            controlPnl.Dock = DockStyle.Top;
            controlPnl.Location = new Point(0, 0);
            controlPnl.Name = "controlPnl";
            controlPnl.Size = new Size(1000, 32);
            controlPnl.TabIndex = 0;
            controlPnl.MouseDown += controlPnl_MouseDown;
            controlPnl.MouseMove += controlPnl_MouseMove;
            controlPnl.MouseUp += controlPnl_MouseUp;
            // 
            // userBtn
            // 
            userBtn.BorderRadius = 5;
            userBtn.Dock = DockStyle.Right;
            userBtn.FlatAppearance.BorderSize = 0;
            userBtn.FlatStyle = FlatStyle.Flat;
            userBtn.Image = (Image)resources.GetObject("userBtn.Image");
            userBtn.Location = new Point(690, 0);
            userBtn.Name = "userBtn";
            userBtn.Size = new Size(62, 32);
            userBtn.TabIndex = 8;
            userBtn.UseVisualStyleBackColor = true;
            userBtn.Click += userBtn_Click;
            userBtn.MouseEnter += styleBtn_MouseEnter;
            userBtn.MouseLeave += styleBtn_MouseLeave;
            // 
            // styleBtn
            // 
            styleBtn.BorderRadius = 5;
            styleBtn.Dock = DockStyle.Right;
            styleBtn.FlatAppearance.BorderSize = 0;
            styleBtn.FlatStyle = FlatStyle.Flat;
            styleBtn.Image = (Image)resources.GetObject("styleBtn.Image");
            styleBtn.Location = new Point(752, 0);
            styleBtn.Name = "styleBtn";
            styleBtn.Size = new Size(62, 32);
            styleBtn.TabIndex = 7;
            styleBtn.UseVisualStyleBackColor = true;
            styleBtn.Click += styleBtn_Click;
            styleBtn.MouseEnter += styleBtn_MouseEnter;
            styleBtn.MouseLeave += styleBtn_MouseLeave;
            // 
            // closeBtn
            // 
            closeBtn.BorderRadius = 5;
            closeBtn.Dock = DockStyle.Right;
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.FlatStyle = FlatStyle.Flat;
            closeBtn.Image = (Image)resources.GetObject("closeBtn.Image");
            closeBtn.Location = new Point(938, 0);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(62, 32);
            closeBtn.TabIndex = 6;
            closeBtn.UseVisualStyleBackColor = true;
            closeBtn.Click += closeBtn_Click;
            closeBtn.MouseEnter += closeBtn_MouseEnter;
            closeBtn.MouseLeave += closeBtn_MouseLeave;
            // 
            // expandBtn
            // 
            expandBtn.BorderRadius = 5;
            expandBtn.Dock = DockStyle.Right;
            expandBtn.FlatAppearance.BorderSize = 0;
            expandBtn.FlatStyle = FlatStyle.Flat;
            expandBtn.Image = (Image)resources.GetObject("expandBtn.Image");
            expandBtn.Location = new Point(876, 0);
            expandBtn.Name = "expandBtn";
            expandBtn.Size = new Size(62, 32);
            expandBtn.TabIndex = 5;
            expandBtn.UseVisualStyleBackColor = true;
            expandBtn.Click += expandBtn_Click;
            expandBtn.MouseEnter += expandBtn_MouseEnter;
            expandBtn.MouseLeave += expandBtn_MouseLeave;
            // 
            // minimizeBtn
            // 
            minimizeBtn.BorderRadius = 5;
            minimizeBtn.Dock = DockStyle.Right;
            minimizeBtn.FlatAppearance.BorderSize = 0;
            minimizeBtn.FlatStyle = FlatStyle.Flat;
            minimizeBtn.Image = (Image)resources.GetObject("minimizeBtn.Image");
            minimizeBtn.Location = new Point(814, 0);
            minimizeBtn.Name = "minimizeBtn";
            minimizeBtn.Size = new Size(62, 32);
            minimizeBtn.TabIndex = 4;
            minimizeBtn.UseVisualStyleBackColor = true;
            minimizeBtn.Click += minimizeBtn_Click;
            minimizeBtn.MouseEnter += minimizeBtn_MouseEnter;
            minimizeBtn.MouseLeave += minimizeBtn_MouseLeave;
            // 
            // menuPnl
            // 
            menuPnl.Controls.Add(gridPnl);
            menuPnl.Controls.Add(mainPnl);
            menuPnl.Dock = DockStyle.Left;
            menuPnl.Location = new Point(0, 32);
            menuPnl.Name = "menuPnl";
            menuPnl.Size = new Size(174, 634);
            menuPnl.TabIndex = 1;
            // 
            // gridPnl
            // 
            gridPnl.BackColor = Color.Transparent;
            gridPnl.Controls.Add(gridLbl);
            gridPnl.Controls.Add(gridImage);
            gridPnl.ForeColor = SystemColors.ControlText;
            gridPnl.Location = new Point(-1, 127);
            gridPnl.Name = "gridPnl";
            gridPnl.Size = new Size(176, 70);
            gridPnl.TabIndex = 1;
            gridPnl.Click += gridPnl_Click;
            gridPnl.MouseEnter += gridPnl_MouseEnter;
            gridPnl.MouseLeave += gridPnl_MouseLeave;
            // 
            // gridLbl
            // 
            gridLbl.AutoSize = true;
            gridLbl.BackColor = SystemColors.Menu;
            gridLbl.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            gridLbl.Location = new Point(70, 20);
            gridLbl.Name = "gridLbl";
            gridLbl.Size = new Size(82, 20);
            gridLbl.TabIndex = 1;
            gridLbl.Text = "Таблицы";
            gridLbl.Click += gridPnl_Click;
            gridLbl.MouseEnter += gridPnl_MouseEnter;
            // 
            // gridImage
            // 
            gridImage.Image = (Image)resources.GetObject("gridImage.Image");
            gridImage.Location = new Point(24, 20);
            gridImage.Name = "gridImage";
            gridImage.Size = new Size(32, 32);
            gridImage.TabIndex = 0;
            gridImage.TabStop = false;
            gridImage.Click += gridPnl_Click;
            gridImage.MouseEnter += gridPnl_MouseEnter;
            // 
            // mainPnl
            // 
            mainPnl.BackColor = Color.Transparent;
            mainPnl.Controls.Add(mainLbl);
            mainPnl.Controls.Add(mainImage);
            mainPnl.ForeColor = SystemColors.ControlText;
            mainPnl.Location = new Point(0, 55);
            mainPnl.Name = "mainPnl";
            mainPnl.Size = new Size(176, 66);
            mainPnl.TabIndex = 0;
            mainPnl.Click += mainPnl_Click;
            mainPnl.MouseEnter += mainPnl_MouseEnter;
            mainPnl.MouseLeave += mainPnl_MouseLeave;
            // 
            // mainLbl
            // 
            mainLbl.AutoSize = true;
            mainLbl.BackColor = SystemColors.Menu;
            mainLbl.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            mainLbl.Location = new Point(70, 20);
            mainLbl.Name = "mainLbl";
            mainLbl.Size = new Size(80, 20);
            mainLbl.TabIndex = 1;
            mainLbl.Text = "Главная";
            mainLbl.Click += mainPnl_Click;
            mainLbl.MouseEnter += mainPnl_MouseEnter;
            // 
            // mainImage
            // 
            mainImage.Image = (Image)resources.GetObject("mainImage.Image");
            mainImage.Location = new Point(24, 20);
            mainImage.Name = "mainImage";
            mainImage.Size = new Size(32, 32);
            mainImage.TabIndex = 0;
            mainImage.TabStop = false;
            mainImage.Click += mainPnl_Click;
            mainImage.MouseEnter += mainPnl_MouseEnter;
            // 
            // menuSpl
            // 
            menuSpl.BackColor = Color.IndianRed;
            menuSpl.Enabled = false;
            menuSpl.Location = new Point(174, 32);
            menuSpl.Name = "menuSpl";
            menuSpl.Size = new Size(2, 634);
            menuSpl.TabIndex = 2;
            menuSpl.TabStop = false;
            // 
            // controlSpl
            // 
            controlSpl.Dock = DockStyle.Top;
            controlSpl.Enabled = false;
            controlSpl.Location = new Point(176, 32);
            controlSpl.Name = "controlSpl";
            controlSpl.Size = new Size(824, 2);
            controlSpl.TabIndex = 3;
            controlSpl.TabStop = false;
            // 
            // mainPnlGrid
            // 
            mainPnlGrid.Dock = DockStyle.Fill;
            mainPnlGrid.Location = new Point(176, 34);
            mainPnlGrid.Name = "mainPnlGrid";
            mainPnlGrid.Size = new Size(824, 632);
            mainPnlGrid.TabIndex = 4;
            // 
            // styleTip
            // 
            styleTip.ToolTipTitle = "Смена темы";
            // 
            // minimizeTip
            // 
            minimizeTip.ToolTipTitle = "Свернуть";
            // 
            // expandTip
            // 
            expandTip.ToolTipTitle = "Расширить";
            // 
            // closeTip
            // 
            closeTip.ToolTipTitle = "Закрыть";
            // 
            // userTip
            // 
            userTip.ToolTipTitle = "Режим прав пользователя";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 26, 26);
            ClientSize = new Size(1000, 666);
            ControlBox = false;
            Controls.Add(mainPnlGrid);
            Controls.Add(controlSpl);
            Controls.Add(menuSpl);
            Controls.Add(menuPnl);
            Controls.Add(controlPnl);
            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5, 4, 5, 4);
            MinimumSize = new Size(400, 300);
            Name = "MainForm";
            ShowIcon = false;
            Text = "Курсовая работа";
            Load += MainForm_Load;
            MouseDown += MainForm_MouseDown;
            MouseMove += MainForm_MouseMove;
            MouseUp += MainForm_MouseUp;
            controlPnl.ResumeLayout(false);
            menuPnl.ResumeLayout(false);
            gridPnl.ResumeLayout(false);
            gridPnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridImage).EndInit();
            mainPnl.ResumeLayout(false);
            mainPnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainImage).EndInit();
            ResumeLayout(false);

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
        private controllers.RoundedButton minimizeBtn;
        private controllers.RoundedButton expandBtn;
        private controllers.RoundedButton closeBtn;
        private controllers.RoundedButton styleBtn;
        private System.Windows.Forms.ToolTip styleTip;
        private System.Windows.Forms.ToolTip minimizeTip;
        private System.Windows.Forms.ToolTip expandTip;
        private System.Windows.Forms.ToolTip closeTip;
        private controllers.RoundedButton userBtn;
        private ToolTip userTip;
    }
}

