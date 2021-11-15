using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.WindowsERPClient.General;

namespace FF.WindowsERPClient.Sales
{
    public partial class CusmobileNo : Form
    {
        public string cusmobno = string.Empty;
        DataTable dt = new DataTable();
     
        public CusmobileNo()
        {
            InitializeComponent();
        }

        //public CusmobileNo(DataTable dt)
        //{
        //    dgvMobilenos.DataSource = dt;
        //}

        public void GetMobileNos (DataTable dt)
        {
             dgvMobilenos.DataSource = dt;
        }

        private void dgvMobilenos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cusmobno = dgvMobilenos.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.Close();
        }
    }
}
