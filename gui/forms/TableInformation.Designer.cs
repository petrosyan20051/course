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
            tablesLbl = new Label();
            tableLst = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dbGrid).BeginInit();
            choosePnl.SuspendLayout();
            SuspendLayout();
            // 
            // dbGrid
            // 
            dbGrid.AllowUserToAddRows = false;
            dbGrid.AllowUserToDeleteRows = false;
            dbGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dbGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dbGrid.Dock = DockStyle.Fill;
            dbGrid.Location = new Point(0, 0);
            dbGrid.Margin = new Padding(4, 3, 4, 3);
            dbGrid.Name = "dbGrid";
            dbGrid.ReadOnly = true;
            dbGrid.Size = new Size(900, 447);
            dbGrid.TabIndex = 0;
            // 
            // choosePnl
            // 
            choosePnl.Controls.Add(tablesLbl);
            choosePnl.Controls.Add(tableLst);
            choosePnl.Dock = DockStyle.Bottom;
            choosePnl.Location = new Point(0, 447);
            choosePnl.Margin = new Padding(4, 3, 4, 3);
            choosePnl.Name = "choosePnl";
            choosePnl.Size = new Size(900, 61);
            choosePnl.TabIndex = 1;
            // 
            // tablesLbl
            // 
            tablesLbl.AutoSize = true;
            tablesLbl.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tablesLbl.Location = new Point(294, 19);
            tablesLbl.Name = "tablesLbl";
            tablesLbl.Size = new Size(93, 25);
            tablesLbl.TabIndex = 1;
            tablesLbl.Text = "Таблицы";
            // 
            // tableLst
            // 
            tableLst.DropDownStyle = ComboBoxStyle.DropDownList;
            tableLst.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tableLst.FormattingEnabled = true;
            tableLst.Location = new Point(393, 16);
            tableLst.Name = "tableLst";
            tableLst.Size = new Size(160, 33);
            tableLst.TabIndex = 0;
            tableLst.SelectedIndexChanged += tableLst_SelectedIndexChanged;
            // 
            // TableInformation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 508);
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
    }
}