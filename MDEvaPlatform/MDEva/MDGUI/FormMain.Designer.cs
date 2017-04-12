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
            this.MenuItemFile_Open_Excel = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Open_proj = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_ExitWithoutSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDongleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptWriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.panel_GPIO = new System.Windows.Forms.Panel();
            this.btn_UpdateGPIO = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.richTextBox_Reg_Window = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_Reg04_Read = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_Reg_StartAddr = new System.Windows.Forms.TextBox();
            this.textBox_Reg_Length = new System.Windows.Forms.TextBox();
            this.btn_Reg04_Write = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Reg03_Read = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Reg03_Addr = new System.Windows.Forms.TextBox();
            this.textBox_Reg03_Value = new System.Windows.Forms.TextBox();
            this.btn_Reg03_Write = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Reg01_Read = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Reg01_Addr = new System.Windows.Forms.TextBox();
            this.textBox_Reg01_Value = new System.Windows.Forms.TextBox();
            this.btn_Reg01_Write = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Reg02_Read = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_Reg02_Addr = new System.Windows.Forms.TextBox();
            this.textBox_Reg02_Value = new System.Windows.Forms.TextBox();
            this.btn_Reg02_Write = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Reg00_Read = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Reg00_Addr = new System.Windows.Forms.TextBox();
            this.textBox_Reg00_Value = new System.Windows.Forms.TextBox();
            this.btn_Reg00_Write = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbt_Valid_off = new System.Windows.Forms.RadioButton();
            this.rbt_Valid_on = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbt_RSTB_Off = new System.Windows.Forms.RadioButton();
            this.rbt_RSTB_On = new System.Windows.Forms.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabCtrlRegView = new System.Windows.Forms.TabControl();
            this.menu_main.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.panel_GPIO.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu_main
            // 
            this.menu_main.BackColor = System.Drawing.SystemColors.Menu;
            this.menu_main.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.toolsToolStripMenuItem,
            this.MenuItemView,
            this.MenuItemWindow,
            this.MenuItemI2CMode,
            this.MenuItemHelp});
            this.menu_main.Location = new System.Drawing.Point(0, 0);
            this.menu_main.Name = "menu_main";
            this.menu_main.Size = new System.Drawing.Size(1261, 24);
            this.menu_main.TabIndex = 4;
            this.menu_main.Text = "menuStrip1";
            // 
            // MenuItemFile
            // 
            this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile_Open,
            this.MenuItemFile_Save,
            this.MenuItemFile_SaveAs,
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
            this.MenuItemFile_Open.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile_Open_Excel,
            this.MenuItemFile_Open_proj});
            this.MenuItemFile_Open.Name = "MenuItemFile_Open";
            this.MenuItemFile_Open.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_Open.Text = "&Open";
            this.MenuItemFile_Open.ToolTipText = "Add";
            // 
            // MenuItemFile_Open_Excel
            // 
            this.MenuItemFile_Open_Excel.Name = "MenuItemFile_Open_Excel";
            this.MenuItemFile_Open_Excel.Size = new System.Drawing.Size(133, 22);
            this.MenuItemFile_Open_Excel.Text = "Excel";
            this.MenuItemFile_Open_Excel.Click += new System.EventHandler(this.MenuItemFile_Open_Excel_Click);
            // 
            // MenuItemFile_Open_proj
            // 
            this.MenuItemFile_Open_proj.Name = "MenuItemFile_Open_proj";
            this.MenuItemFile_Open_proj.Size = new System.Drawing.Size(133, 22);
            this.MenuItemFile_Open_proj.Text = "MD Project";
            this.MenuItemFile_Open_proj.Click += new System.EventHandler(this.MenuItemFile_Open_proj_Click);
            // 
            // MenuItemFile_Save
            // 
            this.MenuItemFile_Save.Name = "MenuItemFile_Save";
            this.MenuItemFile_Save.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_Save.Text = "&Save";
            this.MenuItemFile_Save.Click += new System.EventHandler(this.MenuItemFile_Save_Click);
            // 
            // MenuItemFile_SaveAs
            // 
            this.MenuItemFile_SaveAs.Name = "MenuItemFile_SaveAs";
            this.MenuItemFile_SaveAs.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_SaveAs.Text = "S&ave as..";
            this.MenuItemFile_SaveAs.Click += new System.EventHandler(this.MenuItemFile_Save_As_Click);
            // 
            // MenuItemFile_Close
            // 
            this.MenuItemFile_Close.Name = "MenuItemFile_Close";
            this.MenuItemFile_Close.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_Close.Text = "&Close";
            this.MenuItemFile_Close.ToolTipText = "Close current project";
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
            this.MenuItemFile_Import.ToolTipText = "Import register configurations from *.mdcfg file";
            this.MenuItemFile_Import.Click += new System.EventHandler(this.MenuItemFile_Import_Click);
            // 
            // MenuItemFile_Export
            // 
            this.MenuItemFile_Export.Name = "MenuItemFile_Export";
            this.MenuItemFile_Export.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_Export.Text = "&Export";
            this.MenuItemFile_Export.ToolTipText = "Export register configurations to *.mdcfg file";
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
            this.MenuItemFile_Exit.ToolTipText = "Exit and save form register settings to project file";
            this.MenuItemFile_Exit.Click += new System.EventHandler(this.MenuItemFile_Exit_Click);
            // 
            // MenuItemFile_ExitWithoutSave
            // 
            this.MenuItemFile_ExitWithoutSave.Name = "MenuItemFile_ExitWithoutSave";
            this.MenuItemFile_ExitWithoutSave.Size = new System.Drawing.Size(177, 22);
            this.MenuItemFile_ExitWithoutSave.Text = "Exit &Without Saving";
            this.MenuItemFile_ExitWithoutSave.Click += new System.EventHandler(this.MenuItemFile_ExitWithoutSave_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectDongleToolStripMenuItem,
            this.scriptWriteToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // selectDongleToolStripMenuItem
            // 
            this.selectDongleToolStripMenuItem.Name = "selectDongleToolStripMenuItem";
            this.selectDongleToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.selectDongleToolStripMenuItem.Text = "Select Dongle";
            this.selectDongleToolStripMenuItem.Click += new System.EventHandler(this.selectDongleToolStripMenuItem_Click);
            this.selectDongleToolStripMenuItem.MouseEnter += new System.EventHandler(this.selectDongleToolStripMenuItem_MouseEnter);
            // 
            // scriptWriteToolStripMenuItem
            // 
            this.scriptWriteToolStripMenuItem.Name = "scriptWriteToolStripMenuItem";
            this.scriptWriteToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.scriptWriteToolStripMenuItem.Text = "Script Write";
            this.scriptWriteToolStripMenuItem.Click += new System.EventHandler(this.scriptWriteToolStripMenuItem_Click);
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
            this.MenuItemHelp_About.Size = new System.Drawing.Size(105, 22);
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
            this.statusBar.Location = new System.Drawing.Point(0, 713);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1261, 24);
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
            this.statusBar_FWVersion.Size = new System.Drawing.Size(96, 19);
            this.statusBar_FWVersion.Text = "FW Version: 1.0";
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
            this.statusBar_BoardType.Size = new System.Drawing.Size(124, 19);
            this.statusBar_BoardType.Text = "Board Type: CDA001";
            // 
            // panel_GPIO
            // 
            this.panel_GPIO.BackColor = System.Drawing.SystemColors.Control;
            this.panel_GPIO.Controls.Add(this.btn_UpdateGPIO);
            this.panel_GPIO.Controls.Add(this.groupBox7);
            this.panel_GPIO.Controls.Add(this.groupBox5);
            this.panel_GPIO.Controls.Add(this.groupBox4);
            this.panel_GPIO.Controls.Add(this.groupBox6);
            this.panel_GPIO.Controls.Add(this.groupBox3);
            this.panel_GPIO.Controls.Add(this.groupBox2);
            this.panel_GPIO.Controls.Add(this.groupBox1);
            this.panel_GPIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_GPIO.Location = new System.Drawing.Point(0, 0);
            this.panel_GPIO.Name = "panel_GPIO";
            this.panel_GPIO.Size = new System.Drawing.Size(278, 689);
            this.panel_GPIO.TabIndex = 9;
            // 
            // btn_UpdateGPIO
            // 
            this.btn_UpdateGPIO.Location = new System.Drawing.Point(99, 92);
            this.btn_UpdateGPIO.Name = "btn_UpdateGPIO";
            this.btn_UpdateGPIO.Size = new System.Drawing.Size(75, 25);
            this.btn_UpdateGPIO.TabIndex = 13;
            this.btn_UpdateGPIO.Text = "Update";
            this.btn_UpdateGPIO.UseVisualStyleBackColor = true;
            this.btn_UpdateGPIO.Click += new System.EventHandler(this.btn_UpdateGPIO_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.richTextBox_Reg_Window);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.btn_Reg04_Read);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.textBox_Reg_StartAddr);
            this.groupBox7.Controls.Add(this.textBox_Reg_Length);
            this.groupBox7.Controls.Add(this.btn_Reg04_Write);
            this.groupBox7.Location = new System.Drawing.Point(18, 496);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(245, 190);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Register";
            // 
            // richTextBox_Reg_Window
            // 
            this.richTextBox_Reg_Window.Location = new System.Drawing.Point(5, 75);
            this.richTextBox_Reg_Window.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox_Reg_Window.Name = "richTextBox_Reg_Window";
            this.richTextBox_Reg_Window.Size = new System.Drawing.Size(235, 110);
            this.richTextBox_Reg_Window.TabIndex = 8;
            this.richTextBox_Reg_Window.Text = "00-11-22-11-11-22-22-22-2-2-2-2-2-2-2-2-2-2-2-2-2-2-2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "StartAddr(Hex)";
            // 
            // btn_Reg04_Read
            // 
            this.btn_Reg04_Read.Location = new System.Drawing.Point(151, 19);
            this.btn_Reg04_Read.Name = "btn_Reg04_Read";
            this.btn_Reg04_Read.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg04_Read.TabIndex = 6;
            this.btn_Reg04_Read.Text = "Read";
            this.btn_Reg04_Read.UseVisualStyleBackColor = true;
            this.btn_Reg04_Read.Click += new System.EventHandler(this.btn_Reg04_Read_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Length(Hex)";
            // 
            // textBox_Reg_StartAddr
            // 
            this.textBox_Reg_StartAddr.Location = new System.Drawing.Point(84, 22);
            this.textBox_Reg_StartAddr.Name = "textBox_Reg_StartAddr";
            this.textBox_Reg_StartAddr.Size = new System.Drawing.Size(47, 20);
            this.textBox_Reg_StartAddr.TabIndex = 4;
            this.textBox_Reg_StartAddr.Text = "00";
            this.textBox_Reg_StartAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Reg_Length
            // 
            this.textBox_Reg_Length.Location = new System.Drawing.Point(84, 50);
            this.textBox_Reg_Length.Name = "textBox_Reg_Length";
            this.textBox_Reg_Length.Size = new System.Drawing.Size(47, 20);
            this.textBox_Reg_Length.TabIndex = 5;
            this.textBox_Reg_Length.Text = "02";
            this.textBox_Reg_Length.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Reg04_Write
            // 
            this.btn_Reg04_Write.Location = new System.Drawing.Point(151, 47);
            this.btn_Reg04_Write.Name = "btn_Reg04_Write";
            this.btn_Reg04_Write.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg04_Write.TabIndex = 7;
            this.btn_Reg04_Write.Text = "Write";
            this.btn_Reg04_Write.UseVisualStyleBackColor = true;
            this.btn_Reg04_Write.Click += new System.EventHandler(this.btn_Reg04_Write_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.btn_Reg03_Read);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.textBox_Reg03_Addr);
            this.groupBox5.Controls.Add(this.textBox_Reg03_Value);
            this.groupBox5.Controls.Add(this.btn_Reg03_Write);
            this.groupBox5.Location = new System.Drawing.Point(18, 404);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(245, 86);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Register";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Addr(Hex)";
            // 
            // btn_Reg03_Read
            // 
            this.btn_Reg03_Read.Location = new System.Drawing.Point(151, 23);
            this.btn_Reg03_Read.Name = "btn_Reg03_Read";
            this.btn_Reg03_Read.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg03_Read.TabIndex = 6;
            this.btn_Reg03_Read.Text = "Read";
            this.btn_Reg03_Read.UseVisualStyleBackColor = true;
            this.btn_Reg03_Read.Click += new System.EventHandler(this.btn_Reg03_Read_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Value(Hex)";
            // 
            // textBox_Reg03_Addr
            // 
            this.textBox_Reg03_Addr.Location = new System.Drawing.Point(69, 26);
            this.textBox_Reg03_Addr.Name = "textBox_Reg03_Addr";
            this.textBox_Reg03_Addr.Size = new System.Drawing.Size(53, 20);
            this.textBox_Reg03_Addr.TabIndex = 4;
            this.textBox_Reg03_Addr.Text = "00";
            this.textBox_Reg03_Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Reg03_Value
            // 
            this.textBox_Reg03_Value.Location = new System.Drawing.Point(69, 53);
            this.textBox_Reg03_Value.Name = "textBox_Reg03_Value";
            this.textBox_Reg03_Value.Size = new System.Drawing.Size(53, 20);
            this.textBox_Reg03_Value.TabIndex = 5;
            this.textBox_Reg03_Value.Text = "00";
            this.textBox_Reg03_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Reg03_Write
            // 
            this.btn_Reg03_Write.Location = new System.Drawing.Point(151, 52);
            this.btn_Reg03_Write.Name = "btn_Reg03_Write";
            this.btn_Reg03_Write.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg03_Write.TabIndex = 7;
            this.btn_Reg03_Write.Text = "Write";
            this.btn_Reg03_Write.UseVisualStyleBackColor = true;
            this.btn_Reg03_Write.Click += new System.EventHandler(this.btn_Reg03_Write_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.btn_Reg01_Read);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.textBox_Reg01_Addr);
            this.groupBox4.Controls.Add(this.textBox_Reg01_Value);
            this.groupBox4.Controls.Add(this.btn_Reg01_Write);
            this.groupBox4.Location = new System.Drawing.Point(18, 221);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(245, 86);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Register";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Addr(Hex)";
            // 
            // btn_Reg01_Read
            // 
            this.btn_Reg01_Read.Location = new System.Drawing.Point(151, 23);
            this.btn_Reg01_Read.Name = "btn_Reg01_Read";
            this.btn_Reg01_Read.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg01_Read.TabIndex = 6;
            this.btn_Reg01_Read.Text = "Read";
            this.btn_Reg01_Read.UseVisualStyleBackColor = true;
            this.btn_Reg01_Read.Click += new System.EventHandler(this.btn_Reg01_Read_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Value(Hex)";
            // 
            // textBox_Reg01_Addr
            // 
            this.textBox_Reg01_Addr.Location = new System.Drawing.Point(69, 26);
            this.textBox_Reg01_Addr.Name = "textBox_Reg01_Addr";
            this.textBox_Reg01_Addr.Size = new System.Drawing.Size(53, 20);
            this.textBox_Reg01_Addr.TabIndex = 4;
            this.textBox_Reg01_Addr.Text = "00";
            this.textBox_Reg01_Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Reg01_Value
            // 
            this.textBox_Reg01_Value.Location = new System.Drawing.Point(69, 52);
            this.textBox_Reg01_Value.Name = "textBox_Reg01_Value";
            this.textBox_Reg01_Value.Size = new System.Drawing.Size(53, 20);
            this.textBox_Reg01_Value.TabIndex = 5;
            this.textBox_Reg01_Value.Text = "00";
            this.textBox_Reg01_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Reg01_Write
            // 
            this.btn_Reg01_Write.Location = new System.Drawing.Point(151, 52);
            this.btn_Reg01_Write.Name = "btn_Reg01_Write";
            this.btn_Reg01_Write.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg01_Write.TabIndex = 7;
            this.btn_Reg01_Write.Text = "Write";
            this.btn_Reg01_Write.UseVisualStyleBackColor = true;
            this.btn_Reg01_Write.Click += new System.EventHandler(this.btn_Reg01_Write_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.btn_Reg02_Read);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.textBox_Reg02_Addr);
            this.groupBox6.Controls.Add(this.textBox_Reg02_Value);
            this.groupBox6.Controls.Add(this.btn_Reg02_Write);
            this.groupBox6.Location = new System.Drawing.Point(18, 313);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(245, 86);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Register";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Addr(Hex)";
            // 
            // btn_Reg02_Read
            // 
            this.btn_Reg02_Read.Location = new System.Drawing.Point(151, 24);
            this.btn_Reg02_Read.Name = "btn_Reg02_Read";
            this.btn_Reg02_Read.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg02_Read.TabIndex = 6;
            this.btn_Reg02_Read.Text = "Read";
            this.btn_Reg02_Read.UseVisualStyleBackColor = true;
            this.btn_Reg02_Read.Click += new System.EventHandler(this.btn_Reg02_Read_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Value(Hex)";
            // 
            // textBox_Reg02_Addr
            // 
            this.textBox_Reg02_Addr.Location = new System.Drawing.Point(69, 26);
            this.textBox_Reg02_Addr.Name = "textBox_Reg02_Addr";
            this.textBox_Reg02_Addr.Size = new System.Drawing.Size(53, 20);
            this.textBox_Reg02_Addr.TabIndex = 4;
            this.textBox_Reg02_Addr.Text = "00";
            this.textBox_Reg02_Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Reg02_Value
            // 
            this.textBox_Reg02_Value.Location = new System.Drawing.Point(69, 54);
            this.textBox_Reg02_Value.Name = "textBox_Reg02_Value";
            this.textBox_Reg02_Value.Size = new System.Drawing.Size(53, 20);
            this.textBox_Reg02_Value.TabIndex = 5;
            this.textBox_Reg02_Value.Text = "00";
            this.textBox_Reg02_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Reg02_Write
            // 
            this.btn_Reg02_Write.Location = new System.Drawing.Point(151, 52);
            this.btn_Reg02_Write.Name = "btn_Reg02_Write";
            this.btn_Reg02_Write.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg02_Write.TabIndex = 7;
            this.btn_Reg02_Write.Text = "Write";
            this.btn_Reg02_Write.UseVisualStyleBackColor = true;
            this.btn_Reg02_Write.Click += new System.EventHandler(this.btn_Reg02_Write_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btn_Reg00_Read);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBox_Reg00_Addr);
            this.groupBox3.Controls.Add(this.textBox_Reg00_Value);
            this.groupBox3.Controls.Add(this.btn_Reg00_Write);
            this.groupBox3.Location = new System.Drawing.Point(18, 129);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(245, 86);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Register";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Addr(Hex)";
            // 
            // btn_Reg00_Read
            // 
            this.btn_Reg00_Read.Location = new System.Drawing.Point(151, 23);
            this.btn_Reg00_Read.Name = "btn_Reg00_Read";
            this.btn_Reg00_Read.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg00_Read.TabIndex = 6;
            this.btn_Reg00_Read.Text = "Read";
            this.btn_Reg00_Read.UseVisualStyleBackColor = true;
            this.btn_Reg00_Read.Click += new System.EventHandler(this.btn_Reg00_Read_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Value(Hex)";
            // 
            // textBox_Reg00_Addr
            // 
            this.textBox_Reg00_Addr.Location = new System.Drawing.Point(69, 26);
            this.textBox_Reg00_Addr.Name = "textBox_Reg00_Addr";
            this.textBox_Reg00_Addr.Size = new System.Drawing.Size(53, 20);
            this.textBox_Reg00_Addr.TabIndex = 4;
            this.textBox_Reg00_Addr.Text = "00";
            this.textBox_Reg00_Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Reg00_Value
            // 
            this.textBox_Reg00_Value.Location = new System.Drawing.Point(69, 52);
            this.textBox_Reg00_Value.Name = "textBox_Reg00_Value";
            this.textBox_Reg00_Value.Size = new System.Drawing.Size(53, 20);
            this.textBox_Reg00_Value.TabIndex = 5;
            this.textBox_Reg00_Value.Text = "00";
            this.textBox_Reg00_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Reg00_Write
            // 
            this.btn_Reg00_Write.Location = new System.Drawing.Point(151, 52);
            this.btn_Reg00_Write.Name = "btn_Reg00_Write";
            this.btn_Reg00_Write.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg00_Write.TabIndex = 7;
            this.btn_Reg00_Write.Text = "Write";
            this.btn_Reg00_Write.UseVisualStyleBackColor = true;
            this.btn_Reg00_Write.Click += new System.EventHandler(this.btn_Reg00_Write_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(150, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(113, 96);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "VALID";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbt_Valid_off);
            this.panel1.Controls.Add(this.rbt_Valid_on);
            this.panel1.Location = new System.Drawing.Point(19, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(71, 51);
            this.panel1.TabIndex = 2;
            // 
            // rbt_Valid_off
            // 
            this.rbt_Valid_off.AutoSize = true;
            this.rbt_Valid_off.Checked = true;
            this.rbt_Valid_off.Location = new System.Drawing.Point(3, 27);
            this.rbt_Valid_off.Name = "rbt_Valid_off";
            this.rbt_Valid_off.Size = new System.Drawing.Size(45, 17);
            this.rbt_Valid_off.TabIndex = 1;
            this.rbt_Valid_off.TabStop = true;
            this.rbt_Valid_off.Text = "Low";
            this.rbt_Valid_off.UseVisualStyleBackColor = true;
            // 
            // rbt_Valid_on
            // 
            this.rbt_Valid_on.AutoSize = true;
            this.rbt_Valid_on.Location = new System.Drawing.Point(3, 3);
            this.rbt_Valid_on.Name = "rbt_Valid_on";
            this.rbt_Valid_on.Size = new System.Drawing.Size(47, 17);
            this.rbt_Valid_on.TabIndex = 0;
            this.rbt_Valid_on.Text = "High";
            this.rbt_Valid_on.UseVisualStyleBackColor = true;
            this.rbt_Valid_on.CheckedChanged += new System.EventHandler(this.rbt_Valid_on_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Location = new System.Drawing.Point(18, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 94);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RSTB";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbt_RSTB_Off);
            this.panel2.Controls.Add(this.rbt_RSTB_On);
            this.panel2.Location = new System.Drawing.Point(20, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(71, 51);
            this.panel2.TabIndex = 4;
            // 
            // rbt_RSTB_Off
            // 
            this.rbt_RSTB_Off.AutoSize = true;
            this.rbt_RSTB_Off.Checked = true;
            this.rbt_RSTB_Off.Location = new System.Drawing.Point(3, 27);
            this.rbt_RSTB_Off.Name = "rbt_RSTB_Off";
            this.rbt_RSTB_Off.Size = new System.Drawing.Size(45, 17);
            this.rbt_RSTB_Off.TabIndex = 1;
            this.rbt_RSTB_Off.TabStop = true;
            this.rbt_RSTB_Off.Text = "Low";
            this.rbt_RSTB_Off.UseVisualStyleBackColor = true;
            // 
            // rbt_RSTB_On
            // 
            this.rbt_RSTB_On.AutoSize = true;
            this.rbt_RSTB_On.Location = new System.Drawing.Point(3, 3);
            this.rbt_RSTB_On.Name = "rbt_RSTB_On";
            this.rbt_RSTB_On.Size = new System.Drawing.Size(47, 17);
            this.rbt_RSTB_On.TabIndex = 0;
            this.rbt_RSTB_On.Text = "High";
            this.rbt_RSTB_On.UseVisualStyleBackColor = true;
            this.rbt_RSTB_On.CheckedChanged += new System.EventHandler(this.rbt_RSTB_On_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabCtrlRegView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel_GPIO);
            this.splitContainer1.Size = new System.Drawing.Size(1261, 689);
            this.splitContainer1.SplitterDistance = 979;
            this.splitContainer1.TabIndex = 10;
            // 
            // tabCtrlRegView
            // 
            this.tabCtrlRegView.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabCtrlRegView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlRegView.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlRegView.Name = "tabCtrlRegView";
            this.tabCtrlRegView.SelectedIndex = 0;
            this.tabCtrlRegView.Size = new System.Drawing.Size(979, 689);
            this.tabCtrlRegView.TabIndex = 8;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1261, 737);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menu_main);
            this.Name = "FormMain";
            this.Text = "MainGUI v1.1";
            this.menu_main.ResumeLayout(false);
            this.menu_main.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.panel_GPIO.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Import;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Export;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.Panel panel_GPIO;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbt_RSTB_Off;
        private System.Windows.Forms.RadioButton rbt_RSTB_On;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbt_Valid_off;
        private System.Windows.Forms.RadioButton rbt_Valid_on;
        private System.Windows.Forms.ToolStripMenuItem scriptWriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectDongleToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_Reg04_Read;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_Reg_StartAddr;
        private System.Windows.Forms.TextBox textBox_Reg_Length;
        private System.Windows.Forms.Button btn_Reg04_Write;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Reg03_Read;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Reg03_Addr;
        private System.Windows.Forms.TextBox textBox_Reg03_Value;
        private System.Windows.Forms.Button btn_Reg03_Write;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Reg01_Read;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Reg01_Addr;
        private System.Windows.Forms.TextBox textBox_Reg01_Value;
        private System.Windows.Forms.Button btn_Reg01_Write;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_Reg02_Read;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_Reg02_Addr;
        private System.Windows.Forms.TextBox textBox_Reg02_Value;
        private System.Windows.Forms.Button btn_Reg02_Write;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Reg00_Read;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Reg00_Addr;
        private System.Windows.Forms.TextBox textBox_Reg00_Value;
        private System.Windows.Forms.Button btn_Reg00_Write;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabCtrlRegView;
        private System.Windows.Forms.Button btn_UpdateGPIO;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Save;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Open_Excel;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Open_proj;
        private System.Windows.Forms.RichTextBox richTextBox_Reg_Window;
    }
}