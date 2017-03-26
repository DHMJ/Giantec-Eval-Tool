namespace GeneralRegConfigPlatform.MDGUI
{
    partial class MDRegisterView
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlRegViewTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnWriteSel = new System.Windows.Forms.Button();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnWriteAll = new System.Windows.Forms.Button();
            this.btn_SelectedRead = new System.Windows.Forms.Button();
            this.btnReadAll = new System.Windows.Forms.Button();
            this.txtDescriptions = new System.Windows.Forms.TextBox();
            this.columnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnBinary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlRegViewDVG = new System.Windows.Forms.Panel();
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mdDVG1 = new GeneralRegConfigPlatform.GUI.MDDataGridView();
            this.pnlRegViewTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlRegViewDVG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mdDVG1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRegViewTop
            // 
            this.pnlRegViewTop.Controls.Add(this.panel1);
            this.pnlRegViewTop.Controls.Add(this.txtDescriptions);
            this.pnlRegViewTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRegViewTop.Location = new System.Drawing.Point(0, 0);
            this.pnlRegViewTop.Name = "pnlRegViewTop";
            this.pnlRegViewTop.Size = new System.Drawing.Size(678, 71);
            this.pnlRegViewTop.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.btnWriteSel);
            this.panel1.Controls.Add(this.cbPortName);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.btnWriteAll);
            this.panel1.Controls.Add(this.btn_SelectedRead);
            this.panel1.Controls.Add(this.btnReadAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 71);
            this.panel1.TabIndex = 21;
            // 
            // btnWriteSel
            // 
            this.btnWriteSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWriteSel.Location = new System.Drawing.Point(213, 40);
            this.btnWriteSel.Name = "btnWriteSel";
            this.btnWriteSel.Size = new System.Drawing.Size(68, 23);
            this.btnWriteSel.TabIndex = 23;
            this.btnWriteSel.Text = "Write Sel";
            this.btnWriteSel.UseVisualStyleBackColor = true;
            this.btnWriteSel.Click += new System.EventHandler(this.btnWriteSel_Click);
            // 
            // cbPortName
            // 
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Items.AddRange(new object[] {
            "NULL"});
            this.cbPortName.Location = new System.Drawing.Point(21, 9);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(68, 20);
            this.cbPortName.TabIndex = 22;
            this.cbPortName.Visible = false;
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnect.Location = new System.Drawing.Point(21, 40);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(68, 23);
            this.btnConnect.TabIndex = 21;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Visible = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnWriteAll
            // 
            this.btnWriteAll.AutoSize = true;
            this.btnWriteAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWriteAll.Location = new System.Drawing.Point(117, 40);
            this.btnWriteAll.Name = "btnWriteAll";
            this.btnWriteAll.Size = new System.Drawing.Size(69, 23);
            this.btnWriteAll.TabIndex = 20;
            this.btnWriteAll.Text = "Write All";
            this.btnWriteAll.UseVisualStyleBackColor = true;
            this.btnWriteAll.Click += new System.EventHandler(this.btnWriteAll_Click);
            // 
            // btn_SelectedRead
            // 
            this.btn_SelectedRead.AutoSize = true;
            this.btn_SelectedRead.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SelectedRead.Location = new System.Drawing.Point(213, 9);
            this.btn_SelectedRead.Name = "btn_SelectedRead";
            this.btn_SelectedRead.Size = new System.Drawing.Size(68, 23);
            this.btn_SelectedRead.TabIndex = 19;
            this.btn_SelectedRead.Text = "Read Sel";
            this.btn_SelectedRead.UseVisualStyleBackColor = true;
            this.btn_SelectedRead.Click += new System.EventHandler(this.btn_SelectedRead_Click);
            // 
            // btnReadAll
            // 
            this.btnReadAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReadAll.Location = new System.Drawing.Point(118, 9);
            this.btnReadAll.Name = "btnReadAll";
            this.btnReadAll.Size = new System.Drawing.Size(68, 23);
            this.btnReadAll.TabIndex = 18;
            this.btnReadAll.Text = "Read All";
            this.btnReadAll.UseVisualStyleBackColor = true;
            this.btnReadAll.Click += new System.EventHandler(this.btnReadAll_Click);
            // 
            // txtDescriptions
            // 
            this.txtDescriptions.BackColor = System.Drawing.SystemColors.Window;
            this.txtDescriptions.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtDescriptions.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescriptions.Location = new System.Drawing.Point(372, 0);
            this.txtDescriptions.Multiline = true;
            this.txtDescriptions.Name = "txtDescriptions";
            this.txtDescriptions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescriptions.Size = new System.Drawing.Size(306, 71);
            this.txtDescriptions.TabIndex = 1;
            // 
            // columnAddress
            // 
            this.columnAddress.DataPropertyName = "Address";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.columnAddress.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnAddress.FillWeight = 1.401298E-45F;
            this.columnAddress.HeaderText = "Address";
            this.columnAddress.MinimumWidth = 60;
            this.columnAddress.Name = "columnAddress";
            this.columnAddress.ReadOnly = true;
            // 
            // columnName
            // 
            this.columnName.DataPropertyName = "Name";
            this.columnName.FillWeight = 300F;
            this.columnName.HeaderText = "Name";
            this.columnName.MinimumWidth = 300;
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            this.columnName.Width = 300;
            // 
            // columnBinary
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.columnBinary.DefaultCellStyle = dataGridViewCellStyle2;
            this.columnBinary.FillWeight = 1.401298E-45F;
            this.columnBinary.HeaderText = "Data (Binary)";
            this.columnBinary.MinimumWidth = 129;
            this.columnBinary.Name = "columnBinary";
            this.columnBinary.Width = 129;
            // 
            // columnHex
            // 
            this.columnHex.DataPropertyName = "Value";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.columnHex.DefaultCellStyle = dataGridViewCellStyle3;
            this.columnHex.FillWeight = 1.401298E-45F;
            this.columnHex.HeaderText = "Value";
            this.columnHex.MinimumWidth = 50;
            this.columnHex.Name = "columnHex";
            // 
            // pnlRegViewDVG
            // 
            this.pnlRegViewDVG.Controls.Add(this.mdDVG1);
            this.pnlRegViewDVG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRegViewDVG.Location = new System.Drawing.Point(0, 71);
            this.pnlRegViewDVG.Name = "pnlRegViewDVG";
            this.pnlRegViewDVG.Size = new System.Drawing.Size(678, 302);
            this.pnlRegViewDVG.TabIndex = 9;
            // 
            // rightClickMenu
            // 
            this.rightClickMenu.Name = "contextMenuStrip1";
            this.rightClickMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // mdDVG1
            // 
            this.mdDVG1.AllowUserToAddRows = false;
            this.mdDVG1.AllowUserToDeleteRows = false;
            this.mdDVG1.AllowUserToResizeColumns = false;
            this.mdDVG1.AllowUserToResizeRows = false;
            this.mdDVG1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.mdDVG1.ContextMenuStrip = this.rightClickMenu;
            this.mdDVG1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdDVG1.Location = new System.Drawing.Point(0, 0);
            this.mdDVG1.Name = "mdDVG1";
            this.mdDVG1.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.mdDVG1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.mdDVG1.RowTemplate.Height = 23;
            this.mdDVG1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mdDVG1.Size = new System.Drawing.Size(678, 302);
            this.mdDVG1.TabIndex = 0;
            this.mdDVG1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.mdDVG1_CellFormatting);
            this.mdDVG1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.mdDVG1_CellValidating);
            this.mdDVG1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.mdDVG1_CellValueChanged);
            this.mdDVG1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.mdDVG1_EditingControlShowing);
            this.mdDVG1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.mdDVG1_RowPrePaint);
            this.mdDVG1.SelectionChanged += new System.EventHandler(this.mdDVG1_SelectionChanged);
            this.mdDVG1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mdDVG1_KeyPress);
            // 
            // MDRegisterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlRegViewDVG);
            this.Controls.Add(this.pnlRegViewTop);
            this.Name = "MDRegisterView";
            this.Size = new System.Drawing.Size(678, 373);
            this.Load += new System.EventHandler(this.MDRegisterView_Load);
            this.pnlRegViewTop.ResumeLayout(false);
            this.pnlRegViewTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlRegViewDVG.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mdDVG1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRegViewTop;
        private System.Windows.Forms.Button btnReadAll;
        public System.Windows.Forms.DataGridViewTextBoxColumn columnAddress;
        public System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBinary;
        public System.Windows.Forms.DataGridViewTextBoxColumn columnHex;
        private System.Windows.Forms.Panel pnlRegViewDVG;
        private GUI.MDDataGridView mdDVG1;
        private System.Windows.Forms.TextBox txtDescriptions;
        private System.Windows.Forms.Button btn_SelectedRead;
        private System.Windows.Forms.Button btnWriteAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip rightClickMenu;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.Button btnWriteSel;
    }
}
