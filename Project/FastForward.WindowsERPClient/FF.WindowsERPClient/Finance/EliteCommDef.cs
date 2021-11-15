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
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.Finance;
using FF.WindowsERPClient.CommonSearch;


//Written By kapila on 26/12/2012
namespace FF.WindowsERPClient.Finance
{
    public partial class EliteCommDef : Base
    {
        static int RccStage;
        static string RccNo;
        static Int32 SeqNo;
        static Int32 ItmLine;
        static Int32 BatchLine;
        static Int32 SerLine;
        static string SerialNo;
        static string WarrantyNo;
        static string ItemCode;
        const string InvoiceBackDateName = "RCCENTRY";

        public EliteCommDef()
        {
            InitializeComponent();
           // InitializeValuesNDefaultValueSet();

        }




        public void setSearchValues(string _serial, string _warranty, string _itmCode, Int32 _seqno, Int32 _itemLine, Int32 _bachLine, Int32 _sLine)
        {
            SeqNo = _seqno;
            ItmLine = _itemLine;
            BatchLine = _bachLine;
            SerLine = _sLine;
            SerialNo = _serial;
            WarrantyNo = _warranty;
            ItemCode = _itmCode;

            //GetInvoiceDetails(_seqno, _itemLine, _bachLine, _sLine);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string com = txtComp.Text;
            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            lstPC.Clear();
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
            else
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        







    }
}


