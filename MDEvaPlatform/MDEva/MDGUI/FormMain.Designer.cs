namespace GeneralRegConfigPlatform.GUI
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu_main = new System.Windows.Forms.MenuStrip();
            this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_ExitWithoutSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemView_List = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemView_Output = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemI2CMode = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemI2CMode_GPIO = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemI2CMode_HW = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBar_DeviceConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_FWVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_VID = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_PID = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_BoardType = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabCtrlRegView = new System.Windows.Forms.TabControl();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDongleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_main.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu_main
            // 
            this.menu_main.BackColor = System.Drawing.SystemColors.Menu;
            this.menu_main.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemView,
            this.MenuItemWindow,
            this.MenuItemI2CMode,
            this.toolsToolStripMenuItem,
            this.MenuItemHelp});
            this.menu_main.Location = new System.Drawing.Point(0, 0);
            this.menu_main.Name = "menu_main";
            this.menu_main.Size = new System.Drawing.Size(1081, 24);
            this.menu_main.TabIndex = 4;
            this.menu_main.Text = "menuStrip1";
            // 
            // MenuItemFile
            // 
            this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile_Open,
            this.MenuItemFile_Close,
            this.toolStripSeparator1,
            this.MenuItemFile_Import,
            this.MenuItemFile_Export,
            this.toolStripSeparator2,
            this.MenuItemFile_Exit,
            this.MenuItemFile_ExitWithoutSave});
            this.MenuItemFile.Name = "MenuItemFile";
            this.MenuItemFile.Size = new System.Drawing.Size(39, 20);
            this.MenuItemFile.Text = "&File";
            this.MenuItemFile.ToolTipText = "File";
            // 
            // MenuItemFile_Open
            // 
            this.MenuItemFile_Open.Name = "MenuItemFile_Open";
            this.MenuItemFile_Open.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_Open.Text = "&Open";
            this.MenuItemFile_Open.ToolTipText = "Add";
            this.MenuItemFile_Open.Click += new System.EventHandler(this.MenuItemFile_Open_Click);
            // 
            // MenuItemFile_Close
            // 
            this.MenuItemFile_Close.Name = "MenuItemFile_Close";
            this.MenuItemFile_Close.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_Close.Text = "&Close";
            this.MenuItemFile_Close.ToolTipText = "Close";
            this.MenuItemFile_Close.Click += new System.EventHandler(this.MenuItemFile_Close_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
            // 
            // MenuItemFile_Import
            // 
            this.MenuItemFile_Import.Name = "MenuItemFile_Import";
            this.MenuItemFile_Import.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_Import.Text = "&Import";
            this.MenuItemFile_Import.Click += new System.EventHandler(this.MenuItemFile_Import_Click);
            // 
            // MenuItemFile_Export
            // 
            this.MenuItemFile_Export.Name = "MenuItemFile_Export";
            this.MenuItemFile_Export.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_Export.Text = "&Export";
            this.MenuItemFile_Export.Click += new System.EventHandler(this.MenuItemFile_Export_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(174, 6);
            // 
            // MenuItemFile_Exit
            // 
            this.MenuItemFile_Exit.Name = "MenuItemFile_Exit";
            this.MenuItemFile_Exit.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_Exit.Text = "E&xit";
            this.MenuItemFile_Exit.ToolTipText = "Exit";
            this.MenuItemFile_Exit.Click += new System.EventHandler(this.MenuItemFile_Exit_Click);
            // 
            // MenuItemFile_ExitWithoutSave
            // 
            this.MenuItemFile_ExitWithoutSave.Name = "MenuItemFile_ExitWithoutSave";
            this.MenuItemFile_ExitWithoutSave.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_ExitWithoutSave.Text = "Exit &Without Saving";
            this.MenuItemFile_ExitWithoutSave.Click += new System.EventHandler(this.MenuItemFile_ExitWithoutSave_Click);
            // 
            // MenuItemView
            // 
            this.MenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemView_List,
            this.MenuItemView_Output});
            this.MenuItemView.Name = "MenuItemView";
            this.MenuItemView.Size = new System.Drawing.Size(45, 20);
            this.MenuItemView.Text = "&View";
            this.MenuItemView.ToolTipText = "View";
            this.MenuItemView.Visible = false;
            // 
            // MenuItemView_List
            // 
            this.MenuItemView_List.Name = "MenuItemView_List";
            this.MenuItemView_List.Size = new System.Drawing.Size(139, 22);
            this.MenuItemView_List.Text = "Product &List";
            this.MenuItemView_List.ToolTipText = "Product List";
            // 
            // MenuItemView_Output
            // 
            this.MenuItemView_Output.Name = "MenuItemView_Output";
            this.MenuItemView_Output.Size = new System.Drawing.Size(139, 22);
            this.MenuItemView_Output.Text = "&Output";
            this.MenuItemView_Output.ToolTipText = "Output Window";
            // 
            // MenuItemWindow
            // 
            this.MenuItemWindow.Name = "MenuItemWindow";
            this.MenuItemWindow.Size = new System.Drawing.Size(63, 20);
            this.MenuItemWindow.Text = "&Window";
            this.MenuItemWindow.ToolTipText = "Window";
            this.MenuItemWindow.Visible = false;
            // 
            // MenuItemI2CMode
            // 
            this.MenuItemI2CMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemI2CMode_GPIO,
            this.MenuItemI2CMode_HW});
            this.MenuItemI2CMode.Name = "MenuItemI2CMode";
            this.MenuItemI2CMode.Size = new System.Drawing.Size(71, 20);
            this.MenuItemI2CMode.Text = "I2C Mode";
            this.MenuItemI2CMode.Visible = false;
            // 
            // MenuItemI2CMode_GPIO
            // 
            this.MenuItemI2CMode_GPIO.Name = "MenuItemI2CMode_GPIO";
            this.MenuItemI2CMode_GPIO.Size = new System.Drawing.Size(162, 22);
            this.MenuItemI2CMode_GPIO.Text = "GPIO Simulated";
            // 
            // MenuItemI2CMode_HW
            // 
            this.MenuItemI2CMode_HW.Checked = true;
            this.MenuItemI2CMode_HW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItemI2CMode_HW.Name = "MenuItemI2CMode_HW";
            this.MenuItemI2CMode_HW.Size = new System.Drawing.Size(162, 22);
            this.MenuItemI2CMode_HW.Text = "I2C Hardware";
            // 
            // MenuItemHelp
            // 
            this.MenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemHelp_About});
            this.MenuItemHelp.Name = "MenuItemHelp";
            this.MenuItemHelp.Size = new System.Drawing.Size(45, 20);
            this.MenuItemHelp.Text = "&Help";
            this.MenuItemHelp.ToolTipText = "Help";
            // 
            // MenuItemHelp_About
            // 
            this.MenuItemHelp_About.Name = "MenuItemHelp_About";
            this.MenuItemHelp_About.Size = new System.Drawing.Size(152, 22);
            this.MenuItemHelp_About.Text = "&About";
            this.MenuItemHelp_About.ToolTipText = "About";
            // 
            // statusBar
            // 
            this.statusBar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar_DeviceConnected,
            this.statusBar_FWVersion,
            this.statusBar_VID,
            this.statusBar_PID,
            this.statusBar_BoardType});
            this.statusBar.Location = new System.Drawing.Point(0, 546);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1081, 24);
            this.statusBar.TabIndex = 7;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusBar_DeviceConnected
            // 
            this.statusBar_DeviceConnected.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusBar_DeviceConnected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statusBar_DeviceConnected.Name = "statusBar_DeviceConnected";
            this.statusBar_DeviceConnected.Size = new System.Drawing.Size(127, 19);
            this.statusBar_DeviceConnected.Text = "Device Disconnected";
            this.statusBar_DeviceConnected.ToolTipText = "Devices Disconnected or Connected?";
            // 
            // statusBar_FWVersion
            // 
            this.statusBar_FWVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusBar_FWVersion.Name = "statusBar_FWVersion";
            this.statusBar_FWVersion.Size = new System.Drawing.Size(76, 19);
            this.statusBar_FWVersion.Text = "FW Version:";
            this.statusBar_FWVersion.ToolTipText = "Firmware Version:";
            // 
            // statusBar_VID
            // 
            this.statusBar_VID.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusBar_VID.Name = "statusBar_VID";
            this.statusBar_VID.Size = new System.Drawing.Size(33, 19);
            this.statusBar_VID.Text = "VID:";
            this.statusBar_VID.ToolTipText = "VID";
            this.statusBar_VID.Visible = false;
            // 
            // statusBar_PID
            // 
            this.statusBar_PID.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusBar_PID.Name = "statusBar_PID";
            this.statusBar_PID.Size = new System.Drawing.Size(34, 19);
            this.statusBar_PID.Text = "PID:";
            this.statusBar_PID.ToolTipText = "PID";
            this.statusBar_PID.Visible = false;
            // 
            // statusBar_BoardType
            // 
            this.statusBar_BoardType.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusBar_BoardType.Name = "statusBar_BoardType";
            this.statusBar_BoardType.Size = new System.Drawing.Size(75, 19);
            this.statusBar_BoardType.Text = "Board Type:";
            // 
            // tabCtrlRegView
            // 
            this.tabCtrlRegView.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabCtrlRegView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlRegView.Location = new System.Drawing.Point(0, 24);
            this.tabCtrlRegView.MinimumSize = new System.Drawing.Size(890, 475);
            this.tabCtrlRegView.Name = "tabCtrlRegView";
            this.tabCtrlRegView.SelectedIndex = 0;
            this.tabCtrlRegView.Size = new System.Drawing.Size(1081, 522);
            this.tabCtrlRegView.TabIndex = 8;
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectDongleToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // selectDongleToolStripMenuItem
            // 
            this.selectDongleToolStripMenuItem.Name = "selectDongleToolStripMenuItem";
            this.selectDongleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectDongleToolStripMenuItem.Text = "Select Dongle";
            this.selectDongleToolStripMenuItem.Click += new System.EventHandler(this.selectDongleToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1081, 570);
            this.Controls.Add(this.tabCtrlRegView);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menu_main);
            this.Name = "FormMain";
            this.Text = "MainGUI";
            this.menu_main.ResumeLayout(false);
            this.menu_main.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu_main;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ExitWithoutSave;
        private System.Windows.Forms.ToolStripMenuItem MenuItemView;
        private System.Windows.Forms.ToolStripMenuItem MenuItemView_List;
        private System.Windows.Forms.ToolStripMenuItem MenuItemView_Output;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWindow;
        private System.Windows.Forms.ToolStripMenuItem MenuItemI2CMode;
        private System.Windows.Forms.ToolStripMenuItem MenuItemI2CMode_GPIO;
        private System.Windows.Forms.ToolStripMenuItem MenuItemI2CMode_HW;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp_About;
        private System.Windows.Forms.StatusStrip statusBar;
        public System.Windows.Forms.ToolStripStatusLabel statusBar_DeviceConnected;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_VID;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_PID;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_FWVersion;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_BoardType;
        private System.Windows.Forms.TabControl tabCtrlRegView;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Import;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Export;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectDongleToolStripMenuItem;
    }
}