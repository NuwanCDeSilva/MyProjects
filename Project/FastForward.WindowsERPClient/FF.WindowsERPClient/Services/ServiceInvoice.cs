using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceInvoice : Base
    {
        public ServiceInvoice()
        {
            InitializeComponent();
        }

        private void ServiceInvoice_Load(object sender, EventArgs e)
        {

        }

        private void btnViewPay_Click(object sender, EventArgs e)
        {
            ServicePayments frm = new ServicePayments(txtJobNo.Text, 0,txtCustomer.Text);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }
    }
}
