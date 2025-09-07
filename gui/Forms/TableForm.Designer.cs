namespace gui.Forms {
    partial class TableForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableForm));
            dbGrid = new DataGridView();
            choosePnl = new Panel();
            tablesLbl = new Label();
            tableLst = new ComboBox();
            menuStrip1 = new MenuStrip();
            setAddStrip = new ToolStripMenuItem();
            setDeleteStrip = new ToolStripMenuItem();
            setRecoverStrip = new ToolStripMenuItem();
            dataPnl = new Panel();
            ((System.ComponentModel.ISupportInitialize)dbGrid).BeginInit();
            choosePnl.SuspendLayout();
            menuStrip1.SuspendLayout();
            dataPnl.SuspendLayout();
            SuspendLayout();
            // 
            // dbGrid
            // 
            dbGrid.AllowUserToDeleteRows = false;
            dbGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dbGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dbGrid.Dock = DockStyle.Fill;
            dbGrid.Location = new Point(0, 0);
            dbGrid.Margin = new Padding(4, 3, 4, 3);
            dbGrid.Name = "dbGrid";
            dbGrid.ReadOnly = true;
            dbGrid.RowHeadersVisible = false;
            dbGrid.RowHeadersWidth = 92;
            dbGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dbGrid.Size = new Size(900, 361);
            dbGrid.TabIndex = 0;
            dbGrid.CellEndEdit += dbGrid_CellEndEdit;
            dbGrid.CellEnter += dbGrid_CellEnter;
            dbGrid.CellPainting += dbGrid_CellPainting;
            dbGrid.CellParsing += dbGrid_CellParsing;
            dbGrid.DataError += dbGrid_DataError;
            dbGrid.KeyDown += dbGrid_KeyDown;
            // 
            // choosePnl
            // 
            choosePnl.Controls.Add(tablesLbl);
            choosePnl.Controls.Add(tableLst);
            choosePnl.Dock = DockStyle.Bottom;
            choosePnl.Location = new Point(0, 385);
            choosePnl.Margin = new Padding(4, 3, 4, 3);
            choosePnl.Name = "choosePnl";
            choosePnl.Size = new Size(900, 61);
            choosePnl.TabIndex = 1;
            // 
            // tablesLbl
            // 
            tablesLbl.AutoSize = true;
            tablesLbl.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tablesLbl.Location = new Point(288, 19);
            tablesLbl.Name = "tablesLbl";
            tablesLbl.Size = new Size(88, 25);
            tablesLbl.TabIndex = 1;
            tablesLbl.Text = "Таблица";
            // 
            // tableLst
            // 
            tableLst.DropDownStyle = ComboBoxStyle.DropDownList;
            tableLst.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tableLst.FormattingEnabled = true;
            tableLst.Location = new Point(382, 16);
            tableLst.Name = "tableLst";
            tableLst.Size = new Size(160, 33);
            tableLst.TabIndex = 0;
            tableLst.SelectedIndexChanged += tableLst_SelectedIndexChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { setAddStrip, setDeleteStrip, setRecoverStrip });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.System;
            menuStrip1.Size = new Size(900, 24);
            menuStrip1.Stretch = false;
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // setAddStrip
            // 
            setAddStrip.Image = (Image)resources.GetObject("setAddStrip.Image");
            setAddStrip.Name = "setAddStrip";
            setAddStrip.Size = new Size(124, 20);
            setAddStrip.Text = "Добавить набор";
            setAddStrip.Click += addSetStrip_Click;
            // 
            // setDeleteStrip
            // 
            setDeleteStrip.Image = (Image)resources.GetObject("setDeleteStrip.Image");
            setDeleteStrip.Name = "setDeleteStrip";
            setDeleteStrip.Size = new Size(138, 20);
            setDeleteStrip.Text = "Удалить набор(-ы)";
            setDeleteStrip.Click += setDeleteStrip_Click;
            // 
            // setRecoverStrip
            // 
            setRecoverStrip.Image = (Image)resources.GetObject("setRecoverStrip.Image");
            setRecoverStrip.Name = "setRecoverStrip";
            setRecoverStrip.Size = new Size(169, 20);
            setRecoverStrip.Text = "Восстановить набор(-ы)";
            setRecoverStrip.Click += setRecoverStrip_Click;
            // 
            // dataPnl
            // 
            dataPnl.Controls.Add(dbGrid);
            dataPnl.Dock = DockStyle.Fill;
            dataPnl.Location = new Point(0, 24);
            dataPnl.Name = "dataPnl";
            dataPnl.Size = new Size(900, 361);
            dataPnl.TabIndex = 2;
            // 
            // TableForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 446);
            Controls.Add(dataPnl);
            Controls.Add(choosePnl);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            Name = "TableForm";
            Load += TableInformation_Load;
            ((System.ComponentModel.ISupportInitialize)dbGrid).EndInit();
            choosePnl.ResumeLayout(false);
            choosePnl.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            dataPnl.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dbGrid;
        private System.Windows.Forms.Panel choosePnl;
        private ComboBox tableLst;
        private Label tablesLbl;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem setAddStrip;
        private ToolStripMenuItem setDeleteStrip;
        private ToolStripMenuItem setRecoverStrip;
        private Panel dataPnl;
    }
}