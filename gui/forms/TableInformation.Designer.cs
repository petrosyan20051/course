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
            ((System.ComponentModel.ISupportInitialize)dbGrid).BeginInit();
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
            dbGrid.Size = new Size(933, 124);
            dbGrid.TabIndex = 0;
            // 
            // choosePnl
            // 
            choosePnl.Dock = DockStyle.Bottom;
            choosePnl.Location = new Point(0, 124);
            choosePnl.Margin = new Padding(4, 3, 4, 3);
            choosePnl.Name = "choosePnl";
            choosePnl.Size = new Size(933, 384);
            choosePnl.TabIndex = 1;
            // 
            // TableInformation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 508);
            Controls.Add(dbGrid);
            Controls.Add(choosePnl);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "TableInformation";
            Load += TableInformation_Load;
            ((System.ComponentModel.ISupportInitialize)dbGrid).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dbGrid;
        private System.Windows.Forms.Panel choosePnl;
    }
}