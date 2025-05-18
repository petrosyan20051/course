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
            this.gridPnl = new System.Windows.Forms.Panel();
            this.dbGrid = new System.Windows.Forms.DataGridView();
            this.choosePnl = new System.Windows.Forms.Panel();
            this.gridPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPnl
            // 
            this.gridPnl.Controls.Add(this.dbGrid);
            this.gridPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPnl.Location = new System.Drawing.Point(0, 0);
            this.gridPnl.Name = "gridPnl";
            this.gridPnl.Size = new System.Drawing.Size(800, 440);
            this.gridPnl.TabIndex = 0;
            // 
            // dbGrid
            // 
            this.dbGrid.AllowUserToAddRows = false;
            this.dbGrid.AllowUserToDeleteRows = false;
            this.dbGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbGrid.Location = new System.Drawing.Point(0, 0);
            this.dbGrid.Name = "dbGrid";
            this.dbGrid.ReadOnly = true;
            this.dbGrid.Size = new System.Drawing.Size(800, 440);
            this.dbGrid.TabIndex = 0;
            // 
            // choosePnl
            // 
            this.choosePnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.choosePnl.Location = new System.Drawing.Point(0, 107);
            this.choosePnl.Name = "choosePnl";
            this.choosePnl.Size = new System.Drawing.Size(800, 333);
            this.choosePnl.TabIndex = 1;
            // 
            // TableInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 440);
            this.Controls.Add(this.choosePnl);
            this.Controls.Add(this.gridPnl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TableInformation";
            this.Load += new System.EventHandler(this.TableInformation_Load);
            this.gridPnl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel gridPnl;
        private System.Windows.Forms.DataGridView dbGrid;
        private System.Windows.Forms.Panel choosePnl;
    }
}