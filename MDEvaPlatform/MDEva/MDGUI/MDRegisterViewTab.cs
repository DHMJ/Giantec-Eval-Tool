using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MD.MDCommon;

namespace GeneralRegConfigPlatform.MDGUI
{
    public partial class MDRegisterViewTab : UserControl
    {
        DataTable dt_reg;
        MDRegisterView mdRegView;
        List<string> bfNameList = new List<string> { };
        DataTable dtSearch = new DataTable("dtSearch");
        public MDRegisterViewTab()
        {
            InitializeComponent();

            // fix the two panel size
            this.splitContainer1.Panel1MinSize = 260;
            this.splitContainer1.Panel2MinSize = this.Width - 260;
        }

        public MDRegisterViewTab(DataTable _dt, RegisterMap _regmap, DMDongle.comm _sp)
        {
            InitializeComponent();
            
            dt_reg = _dt;

            // fix the two panel size
            this.splitContainer1.Panel1MinSize = 260;
            this.splitContainer1.Panel2MinSize = this.Width - 260;

            // Add MDRegisterView control
            mdRegView = new MDRegisterView(dt_reg, _regmap, _sp);
            this.splitContainer1.Panel2.Controls.Add(mdRegView);
            mdRegView.Dock = DockStyle.Left;

            // create data table for search dgv
            dtSearch.Columns.Add("Items");
            CollectBFNames(dt_reg);
            UpdateSearchedItems(this.tbSearch.Text);
            this.dgvSearch.DataSource = dtSearch;
        }

        private void CollectBFNames(DataTable _dt)
        {
            bfNameList.Clear();
            foreach (DataRow dr in _dt.Rows)
            {
                if(dr[2].ToString() != "")
                    bfNameList.Add(dr[2].ToString());
            }
        }

        private void UpdateSearchedItems(string keyWords)
        {
            dtSearch.Rows.Clear();
            foreach (string str in bfNameList)
            {
                if (str.ToUpper().Contains(keyWords.ToUpper()))
                {
                    dtSearch.Rows.Add(new object[] { str });
                }
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateSearchedItems(this.tbSearch.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.tbSearch.Text = "";
        }

        private void dgvSearch_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvSearch.SelectedCells.Count >0)
                mdRegView.JumpToSelected(dgvSearch.SelectedCells[0].Value.ToString());
        }

        private void dgvSearch_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.Value = "  " + e.Value.ToString().TrimStart();
        }
    }
}
