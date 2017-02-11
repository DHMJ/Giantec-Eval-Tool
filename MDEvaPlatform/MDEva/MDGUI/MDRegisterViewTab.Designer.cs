namespace GeneralRegConfigPlatform.MDGUI
{
    partial class MDRegisterViewTab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSearch = new System.Windows.Forms.DataGridView();
            this.panel_RevervedSpace = new System.Windows.Forms.Panel();
            this.pnlSearchTop = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.pnlSearchTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Panel1.Controls.Add(this.dgvSearch);
            this.splitContainer1.Panel1.Controls.Add(this.panel_RevervedSpace);
            this.splitContainer1.Panel1.Controls.Add(this.pnlSearchTop);
            this.splitContainer1.Panel1MinSize = 260;
            this.splitContainer1.Size = new System.Drawing.Size(612, 280);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvSearch
            // 
            this.dgvSearch.AllowUserToAddRows = false;
            this.dgvSearch.AllowUserToDeleteRows = false;
            this.dgvSearch.AllowUserToResizeColumns = false;
            this.dgvSearch.AllowUserToResizeRows = false;
            this.dgvSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearch.Location = new System.Drawing.Point(24, 72);
            this.dgvSearch.MultiSelect = false;
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.ReadOnly = true;
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSearch.Size = new System.Drawing.Size(236, 208);
            this.dgvSearch.TabIndex = 23;
            this.dgvSearch.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSearch_CellFormatting);
            this.dgvSearch.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvSearch_RowPrePaint);
            this.dgvSearch.SelectionChanged += new System.EventHandler(this.dgvSearch_SelectionChanged);
            // 
            // panel_RevervedSpace
            // 
            this.panel_RevervedSpace.BackColor = System.Drawing.SystemColors.Window;
            this.panel_RevervedSpace.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_RevervedSpace.Location = new System.Drawing.Point(0, 72);
            this.panel_RevervedSpace.Name = "panel_RevervedSpace";
            this.panel_RevervedSpace.Size = new System.Drawing.Size(24, 208);
            this.panel_RevervedSpace.TabIndex = 25;
            // 
            // pnlSearchTop
            // 
            this.pnlSearchTop.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSearchTop.Controls.Add(this.btnClear);
            this.pnlSearchTop.Controls.Add(this.label1);
            this.pnlSearchTop.Controls.Add(this.tbSearch);
            this.pnlSearchTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchTop.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchTop.Name = "pnlSearchTop";
            this.pnlSearchTop.Size = new System.Drawing.Size(260, 72);
            this.pnlSearchTop.TabIndex = 24;
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(182, 29);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(68, 23);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "Search:";
            // 
            // tbSearch
            // 
            this.tbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearch.Location = new System.Drawing.Point(22, 31);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(154, 21);
            this.tbSearch.TabIndex = 13;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // MDRegisterViewTab
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MDRegisterViewTab";
            this.Size = new System.Drawing.Size(612, 280);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.pnlSearchTop.ResumeLayout(false);
            this.pnlSearchTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSearch;
        private System.Windows.Forms.Panel pnlSearchTop;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Panel panel_RevervedSpace;
    }
}
