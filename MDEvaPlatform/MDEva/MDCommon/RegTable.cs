using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MD.MDCommon
{
    public partial class RegTable : UserControl
    {
        public RegTable()
        {
            InitializeComponent();
        }

        public RegTable(BindingList<RegisterProperty> blist)
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSize = true;
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);

            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.DataPropertyName = "Index";
            indexColumn.HeaderText = " ";
            indexColumn.ReadOnly = true;
            indexColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            indexColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            indexColumn.Selected = false;
            indexColumn.DefaultCellStyle.ForeColor = SystemColors.ControlDarkDark;
            indexColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn regAddrColumn = new DataGridViewTextBoxColumn();
            regAddrColumn.DataPropertyName = "regAddr";
            regAddrColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            regAddrColumn.HeaderText = "Addr(H2)";
            regAddrColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            regAddrColumn.DefaultCellStyle.Format = "X2";
            regAddrColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            regAddrColumn.ReadOnly = true;
            regAddrColumn.Selected = false;

            DataGridViewTextBoxColumn regDataColumn = new DataGridViewTextBoxColumn();
            regDataColumn.DataPropertyName = "regData";
            regDataColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            regDataColumn.HeaderText = "Data(H2)";
            regDataColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            regDataColumn.DefaultCellStyle.Format = "X2";
            regDataColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //regDataColumn.ReadOnly = true;
            regDataColumn.Selected = false;
            

            DataGridViewComboBoxColumn rwColumn = new DataGridViewComboBoxColumn();
            rwColumn.DataPropertyName = "rw";
            rwColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //rwColumn.Width = 100;
            rwColumn.HeaderText = "RW";
            rwColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            rwColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            rwColumn.Items.AddRange(new object[] {
            RWProperty.RW,
            RWProperty.R,
            RWProperty.W});
            rwColumn.Selected = false;
            rwColumn.ReadOnly = true;
            rwColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

            DataGridViewCheckBoxColumn ifReadColumn = new DataGridViewCheckBoxColumn();
            ifReadColumn.DataPropertyName = "ifRead";
            ifReadColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            ifReadColumn.HeaderText = "If Read";
            ifReadColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            ifReadColumn.Selected = false;

            DataGridViewCheckBoxColumn ifWriteColumn = new DataGridViewCheckBoxColumn();
            ifWriteColumn.DataPropertyName = "ifWrite";
            ifWriteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            ifWriteColumn.HeaderText = "If Write";
            ifWriteColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            ifWriteColumn.Selected = false;

            DataGridViewButtonColumn test = new DataGridViewButtonColumn();
            test.DataPropertyName = "test";
            test.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            test.HeaderText = "Test R";
            test.SortMode = DataGridViewColumnSortMode.NotSortable;
            test.Selected = false;
            test.Text = "asfds";
            test.Name = "fasdfsa";
            


            dataGridView1.Columns.Add(indexColumn);
            dataGridView1.Columns.Add(regAddrColumn);
            dataGridView1.Columns.Add(regDataColumn);
            dataGridView1.Columns.Add(rwColumn);
            dataGridView1.Columns.Add(ifReadColumn);
            dataGridView1.Columns.Add(ifWriteColumn);
            //dataGridView1.Columns.Add(test);

            dataGridView1.DataSource = blist;
        }

        void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void BindingDataSource(BindingList<RegisterProperty> regList)
        {
            dataGridView1.DataSource = regList;
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                TextBox tb = e.Control as TextBox;
                tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
                if (this.dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                }
            }
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b' || !"abcdefABCDEF".Contains(e.KeyChar.ToString()) || e.KeyChar != '-' || e.KeyChar != '+') //allow the backspace and minus
                {
                    e.Handled = true;
                }
            }
        }
    }
}
