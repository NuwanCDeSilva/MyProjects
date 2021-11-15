using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.UtilityClasses
{
    public class DataGridViewHelper
    {
        //Added by Udesh 12-Nov-2018
        public DataGridView CopyDataGridView(DataGridView dgv_org, DataTable _dataSource)
        {
            DataGridView dgv_copy = new DataGridView();
            try
            {
                if (dgv_copy.Columns.Count == 0)
                {
                    foreach (DataGridViewColumn dgvc in dgv_org.Columns)
                    {
                        dgv_copy.Columns.Add(dgvc.Clone() as DataGridViewColumn);
                    }
                }
                dgv_copy.AllowUserToAddRows = false;

                dgv_copy.DataSource = null;
                dgv_copy.AutoGenerateColumns = false;
                dgv_copy.DataSource = _dataSource;


                dgv_copy.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return dgv_copy;
        }
    }
}
