namespace gui.forms {
    partial class TableInformation {
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
            dbGrid = new DataGridView();
            choosePnl = new Panel();
            addSetBtn = new gui.controllers.RoundedButton();
            tablesLbl = new Label();
            tableLst = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dbGrid).BeginInit();
            choosePnl.SuspendLayout();
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
            dbGrid.RowHeadersWidth = 92;
            dbGrid.Size = new Size(900, 385);
            dbGrid.TabIndex = 0;
            dbGrid.CellPainting += dbGrid_CellPainting;
            dbGrid.CellParsing += dbGrid_CellParsing;
            dbGrid.DataError += dbGrid_DataError;
            // 
            // choosePnl
            // 
            choosePnl.Controls.Add(addSetBtn);
            choosePnl.Controls.Add(tablesLbl);
            choosePnl.Controls.Add(tableLst);
            choosePnl.Dock = DockStyle.Bottom;
            choosePnl.Location = new Point(0, 385);
            choosePnl.Margin = new Padding(4, 3, 4, 3);
            choosePnl.Name = "choosePnl";
            choosePnl.Size = new Size(900, 61);
            choosePnl.TabIndex = 1;
            // 
            // addSetBtn
            // 
            addSetBtn.BackColor = SystemColors.ButtonShadow;
            addSetBtn.BorderRadius = 10;
            addSetBtn.FlatAppearance.BorderSize = 0;
            addSetBtn.FlatStyle = FlatStyle.Flat;
            addSetBtn.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            addSetBtn.Location = new Point(418, 16);
            addSetBtn.Margin = new Padding(1);
            addSetBtn.Name = "addSetBtn";
            addSetBtn.Size = new Size(164, 32);
            addSetBtn.TabIndex = 2;
            addSetBtn.Text = "Добавить набор";
            addSetBtn.UseVisualStyleBackColor = false;
            addSetBtn.Click += addSetBtn_Click;
            // 
            // tablesLbl
            // 
            tablesLbl.AutoSize = true;
            tablesLbl.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tablesLbl.Location = new Point(19, 19);
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
            tableLst.Location = new Point(118, 19);
            tableLst.Name = "tableLst";
            tableLst.Size = new Size(160, 33);
            tableLst.TabIndex = 0;
            tableLst.SelectedIndexChanged += tableLst_SelectedIndexChanged;
            // 
            // TableInformation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 446);
            Controls.Add(dbGrid);
            Controls.Add(choosePnl);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "TableInformation";
            Load += TableInformation_Load;
            ((System.ComponentModel.ISupportInitialize)dbGrid).EndInit();
            choosePnl.ResumeLayout(false);
            choosePnl.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dbGrid;
        private System.Windows.Forms.Panel choosePnl;
        private ComboBox tableLst;
        private Label tablesLbl;
        private controllers.RoundedButton addSetBtn;
    }
}