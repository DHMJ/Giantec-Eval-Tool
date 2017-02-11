namespace GeneralRegConfigPlatform.MDGUI
{
    partial class FormScript
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.richtxt_ScriptView = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.text_Srcipt_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Script_Excute = new System.Windows.Forms.Button();
            this.btn_Script_Save = new System.Windows.Forms.Button();
            this.btn_Script_load = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richtxt_ScriptView
            // 
            this.richtxt_ScriptView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richtxt_ScriptView.Location = new System.Drawing.Point(0, 0);
            this.richtxt_ScriptView.Name = "richtxt_ScriptView";
            this.richtxt_ScriptView.Size = new System.Drawing.Size(476, 365);
            this.richtxt_ScriptView.TabIndex = 0;
            this.richtxt_ScriptView.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richtxt_ScriptView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.text_Srcipt_Name);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Script_Excute);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Script_Save);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Script_load);
            this.splitContainer1.Size = new System.Drawing.Size(738, 365);
            this.splitContainer1.SplitterDistance = 476;
            this.splitContainer1.TabIndex = 1;
            // 
            // text_Srcipt_Name
            // 
            this.text_Srcipt_Name.Location = new System.Drawing.Point(15, 46);
            this.text_Srcipt_Name.Name = "text_Srcipt_Name";
            this.text_Srcipt_Name.Size = new System.Drawing.Size(225, 21);
            this.text_Srcipt_Name.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Script Name:";
            // 
            // btn_Script_Excute
            // 
            this.btn_Script_Excute.Location = new System.Drawing.Point(42, 125);
            this.btn_Script_Excute.Name = "btn_Script_Excute";
            this.btn_Script_Excute.Size = new System.Drawing.Size(176, 35);
            this.btn_Script_Excute.TabIndex = 2;
            this.btn_Script_Excute.Text = "Excute";
            this.btn_Script_Excute.UseVisualStyleBackColor = true;
            this.btn_Script_Excute.Click += new System.EventHandler(this.btn_Script_Excute_Click);
            // 
            // btn_Script_Save
            // 
            this.btn_Script_Save.Location = new System.Drawing.Point(143, 89);
            this.btn_Script_Save.Name = "btn_Script_Save";
            this.btn_Script_Save.Size = new System.Drawing.Size(75, 21);
            this.btn_Script_Save.TabIndex = 1;
            this.btn_Script_Save.Text = "Save";
            this.btn_Script_Save.UseVisualStyleBackColor = true;
            this.btn_Script_Save.Click += new System.EventHandler(this.btn_Script_Save_Click);
            // 
            // btn_Script_load
            // 
            this.btn_Script_load.Location = new System.Drawing.Point(42, 89);
            this.btn_Script_load.Name = "btn_Script_load";
            this.btn_Script_load.Size = new System.Drawing.Size(75, 21);
            this.btn_Script_load.TabIndex = 0;
            this.btn_Script_load.Text = "Load";
            this.btn_Script_load.UseVisualStyleBackColor = true;
            this.btn_Script_load.Click += new System.EventHandler(this.btn_Script_load_Click);
            // 
            // FormScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormScript";
            this.Size = new System.Drawing.Size(738, 365);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richtxt_ScriptView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_Script_Excute;
        private System.Windows.Forms.Button btn_Script_Save;
        private System.Windows.Forms.Button btn_Script_load;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_Srcipt_Name;
    }
}
