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
            dbGrid.Margin = new Padding(9, 7, 9, 7);
            dbGrid.Name = "dbGrid";
            dbGrid.ReadOnly = true;
            dbGrid.RowHeadersWidth = 92;
            dbGrid.Size = new Size(1929, 1103);
            dbGrid.TabIndex = 0;
            // 
            // choosePnl
            // 
            choosePnl.Controls.Add(addSetBtn);
            choosePnl.Controls.Add(tablesLbl);
            choosePnl.Controls.Add(tableLst);
            choosePnl.Dock = DockStyle.Bottom;
            choosePnl.Location = new Point(0, 1103);
            choosePnl.Margin = new Padding(9, 7, 9, 7);
            choosePnl.Name = "choosePnl";
            choosePnl.Size = new Size(1929, 150);
            choosePnl.TabIndex = 1;
            // 
            // addSetBtn
            // 
            addSetBtn.BackColor = SystemColors.ButtonShadow;
            addSetBtn.BorderRadius = 10;
            addSetBtn.FlatAppearance.BorderSize = 0;
            addSetBtn.FlatStyle = FlatStyle.Flat;
            addSetBtn.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            addSetBtn.Location = new Point(895, 40);
            addSetBtn.Name = "addSetBtn";
            addSetBtn.Size = new Size(351, 79);
            addSetBtn.TabIndex = 2;
            addSetBtn.Text = "Добавить набор";
            addSetBtn.UseVisualStyleBackColor = false;
            addSetBtn.Click += addSetBtn_Click;
            // 
            // tablesLbl
            // 
            tablesLbl.AutoSize = true;
            tablesLbl.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tablesLbl.Location = new Point(41, 47);
            tablesLbl.Margin = new Padding(6, 0, 6, 0);
            tablesLbl.Name = "tablesLbl";
            tablesLbl.Size = new Size(199, 59);
            tablesLbl.TabIndex = 1;
            tablesLbl.Text = "Таблица";
            // 
            // tableLst
            // 
            tableLst.DropDownStyle = ComboBoxStyle.DropDownList;
            tableLst.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tableLst.FormattingEnabled = true;
            tableLst.Location = new Point(252, 47);
            tableLst.Margin = new Padding(6, 7, 6, 7);
            tableLst.Name = "tableLst";
            tableLst.Size = new Size(338, 67);
            tableLst.TabIndex = 0;
            tableLst.SelectedIndexChanged += tableLst_SelectedIndexChanged;
            // 
            // TableInformation
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1929, 1253);
            Controls.Add(dbGrid);
            Controls.Add(choosePnl);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(9, 7, 9, 7);
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