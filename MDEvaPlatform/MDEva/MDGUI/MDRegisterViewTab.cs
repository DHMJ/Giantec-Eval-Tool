using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MD.MDCommon;
using DMCommunication;

namespace GeneralRegConfigPlatform.MDGUI
{
    public partial class MDRegisterViewTab : UserControl
    {
        DataTable dt_reg;
        MDRegisterView mdRegView;
        List<string> bfNameList = new List<string> { };
        List<string> RegNameList = new List<string> { };
        DataTable dtSearch = new DataTable("dtSearch");
        public MDRegisterViewTab()
        {
            InitializeComponent();

            // fix the two panel size
            this.splitContainer1.Panel1MinSize = 260;
            this.splitContainer1.Panel2MinSize = this.Width - 260;

        }

        public void UpdateAllGUI_Tab()
        {
            mdRegView.UpdateGUIAll();
            UpdateSearchedItems("");
        }

        public MDRegisterViewTab(DataTable _dt, DataTable _dtCustomer, RegisterMap _regmap, DMDongle _dongle)
        {
            InitializeComponent();
            
            dt_reg = _dt;
            if (dt_reg.TableName == "Customer")
            {
                dt_reg.RowChanged += new DataRowChangeEventHandler(dt_reg_RowChanged);
                dt_reg.TableCleared += new DataTableClearEventHandler(dt_reg_TableCleared);
            }

            // fix the two panel size
            this.splitContainer1.Panel1MinSize = 260;
            this.splitContainer1.Panel2MinSize = this.Width - 260;

            // Add MDRegisterView control
            mdRegView = new MDRegisterView(dt_reg,_dtCustomer, _regmap, _dongle);
            this.splitContainer1.Panel2.Controls.Add(mdRegView);
            mdRegView.Dock = DockStyle.Left;

            // create data table for search dgv
            dtSearch.Columns.Clear();
            dtSearch.Columns.Add("Items");
            CollectDisplayNames(dt_reg);
            UpdateSearchedItems(this.tbSearch.Text);
            this.dgvSearch.DataSource = dtSearch;

            // Added Row Selected Change Event
            mdRegView.RowSelectedChangeEvent += new MDRegisterView.RowSelectedChangeEventHandler(mdRegView_RowSelectedChangeEvent);
        }

        void dt_reg_TableCleared(object sender, DataTableClearEventArgs e)
        {
            dtSearch.Columns.Clear();
        }

        void dt_reg_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                dtSearch.Columns.Clear();
                dtSearch.Columns.Add("Items");
                CollectDisplayNames(dt_reg);
                UpdateSearchedItems(this.tbSearch.Text);
                this.dgvSearch.DataSource = dtSearch;
            }
        }

        void mdRegView_RowSelectedChangeEvent(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dvgRow in dgvSearch.Rows)
            {
                if (dvgRow.Cells[0].Value.ToString() == sender.ToString())
                {
                    // delete event binding to avoid infinite jump
                    this.dgvSearch.SelectionChanged -= new System.EventHandler(this.dgvSearch_SelectionChanged);
                    dgvSearch.ClearSelection();
                    dvgRow.Selected = true;
                    // Made the selected row displayed in the middle of the list if possible
                    int rowCounts = dgvSearch.Height / dgvSearch.Rows[0].Height;
                    dgvSearch.FirstDisplayedScrollingRowIndex = (dgvSearch.Rows.IndexOf(dvgRow) - rowCounts / 2) > 0 ?
                        (dgvSearch.Rows.IndexOf(dvgRow) - rowCounts / 2) : 0;

                    this.dgvSearch.SelectionChanged += new System.EventHandler(this.dgvSearch_SelectionChanged);
                }
            }
        }

        private void CollectDisplayNames(DataTable _dt)
        {
            bfNameList.Clear();
            RegNameList.Clear();
            foreach (DataRow dr in _dt.Rows)
            {
                if(dr[2].ToString() != "")
                    bfNameList.Add(dr[2].ToString());
                if (dr[0].ToString() != "")
                    RegNameList.Add(dr[2].ToString());
            }
        }

        public void UpdateSearchedItems(string keyWords)
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
            if (dgvSearch.SelectedCells.Count > 0)
            {
                // unbinding event to avoid unexpected changed fist display row when choose from left
                mdRegView.RowSelectedChangeEvent -= new MDRegisterView.RowSelectedChangeEventHandler(mdRegView_RowSelectedChangeEvent);
                mdRegView.JumpToSelected(dgvSearch.SelectedCells[0].Value.ToString());
                mdRegView.RowSelectedChangeEvent += new MDRegisterView.RowSelectedChangeEventHandler(mdRegView_RowSelectedChangeEvent);

            }
        }

        private void dgvSearch_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.Value = "  " + e.Value.ToString().TrimStart();
        }

        private void dgvSearch_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //if (dgvSearch.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Reg"))
            if (RegNameList.Contains(dgvSearch.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                dgvSearch.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightGray;
                dgvSearch.Rows[e.RowIndex].Cells[0].Style.Font = new System.Drawing.Font("Calibri", 12F,
                    System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }
    }
}
