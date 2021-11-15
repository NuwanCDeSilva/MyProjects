using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace FF.WindowsERPClient.UserControls
{
    [ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
    public partial class MultiColumnCombo : UserControl
    {

        //Written By Prabhath on 17/12/2012
        //For replace single column combo box

        #region Private Variable
        private Size _userCtrlSize = new Size(383, 171);
        private Size _textBoxSize = new Size(101, 22);
        private Size _textBoxDefaultSize = new Size(101, 22);
        private DataTable _dataSource = new DataTable();

        private Int16 _selectedColoumn = 0;
        private string _plusString = string.Empty;
        bool _isGridVisible = false;
        #endregion
        #region Public Variables
        /// <summary>
        /// Define the user control size.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always), Description("Define the user control size. If not set, it will size the control according to the display content")]
        public Size UserCtrlSize
        {
            get { return _userCtrlSize; }
            set { _userCtrlSize = value; }
        }
        /// <summary>
        /// Define the textbox control size.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always), Description("Define the textbox control size. If not set, it will size for the default textbox size (100,21)")]
        public Size TextBoxSize
        {
            get { return _textBoxSize; }
            set { _textBoxSize = value; }
        }
        /// <summary>
        /// Define the data source.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always), Description("Define the data source. Data source should be in data table type")]
        public DataTable DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }
        #endregion

        public MultiColumnCombo()
        {
            InitializeComponent();
        }

        private void ResizeControl()
        {
            if (DataSource != null)
                if (DataSource.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataGridViewColumn c in gvDataGrid.Columns)
                        i += c.Width;
                    gvDataGrid.Width = i + gvDataGrid.RowHeadersWidth + 1;
                    this.Size = new Size(gvDataGrid.Width, gvDataGrid.Height);
                    UserCtrlSize = new Size(gvDataGrid.Width, gvDataGrid.Height+10);
                }
            //if (_textBoxSize != null) txtValue.Size = TextBoxSize; else txtValue.Size = _textBoxDefaultSize;
        }

        public object _queryObject = new object();
        
  

        private void SetDataSource()
        {
            gvDataGrid.DataSource = _queryObject;

        }

        private void txtValue_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isGridVisible)
            {
                this.Size = new Size(101, 22);
                _isGridVisible = false;
            }
            else
            {
                SetDataSource();
                ResizeControl();
                this.Size = new Size(383, 171);
                _isGridVisible = true;
            }
        }

        private void gvDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _selectedColoumn = (Int16)e.ColumnIndex;
            _plusString = string.Empty;
            txtValue.Focus();
        }

        private void gvDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Size = new Size(101, 22);
                _isGridVisible = false;
                txtValue.Text = gvDataGrid.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void gvDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                this.Size = new Size(101, 22);
                _isGridVisible = false;
                txtValue.Text = gvDataGrid.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            if (_isGridVisible == false) return;
            if (string.IsNullOrEmpty(txtValue.Text))
            {
                _plusString = string.Empty;
                gvDataGrid.DataSource = _queryObject;
            }
            else
            {
                _plusString = txtValue.Text;
                gvDataGrid.DataSource = null;

                var _h = _queryObject as ArrayList;

                var _query = (from _p in DataSource.AsEnumerable()
                                where _p.Field<string>(_selectedColoumn).Contains(_plusString)
                                select new
                                {
                                    Code = _p.Field<string>(0),
                                    Description = _p.Field<string>(1)

                                }).ToList();

                var lst = _query;

                gvDataGrid.DataSource = lst;
            }
        }



    }
}
