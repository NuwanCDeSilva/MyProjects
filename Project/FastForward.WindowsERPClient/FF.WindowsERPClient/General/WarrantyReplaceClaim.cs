using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.IO;

namespace FF.WindowsERPClient.General
{
    public partial class WarrantyReplaceClaim : Base
    {
        public WarrantyReplaceClaim()
        {
            InitializeComponent();
        }

        private void btnGetInvoiceDet_Click(object sender, EventArgs e)
        {
            //GetInvoiceHeaderDetails
            List<InvoiceHeader> INV = new List<InvoiceHeader>();
            InvoiceHeader invoice= CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoice.Text.Trim());
            INV.Add(invoice);
            grv_invoice.DataSource = null;
            grv_invoice.AutoGenerateColumns = false;
            grv_invoice.DataSource = INV;

            DataTable dt=  CHNLSVC.Sales.GetInvoiceWithSerial(txtInvoice.Text.Trim());
            grvInvoicSerials.DataSource = null;
            //grvInvoicSerials.AutoGenerateColumns = false;
            grvInvoicSerials.DataSource = dt;
           
            //invoice.Sah_com;
            //invoice.Sah_inv_no;
            //invoice.Sah_inv_sub_tp;
            //invoice.Sah_inv_tp;
            //invoice.Sah_pc;
            //invoice.Sah_remarks;
            //invoice.Sah_ref_doc;
            //invoice.Sah_seq_no;
            //invoice.Sah_stus;
            //invoice.Sah_tp;

        }
        private void bindApproved_RequestList()
        {
            //Approved list.
            List<RequestApprovalHeader> _lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT021", string.Empty, string.Empty);//CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVehInsRec1.Text.Trim(), HirePurchasModuleApprovalCode.ARQT021.ToString(), 1, GlbReqUserPermissionLevel);
            if (_lst == null)
            {
                _lst = new List<RequestApprovalHeader>();
            }
           
            if (_lst != null)
            {
                var _duplicate = from _dup in _lst
                                 where _dup.Grah_app_stus == "A"
                                 select _dup;
                if (_duplicate.Count() > 0)
                {
                    _lst = _duplicate.ToList();
                    grvAppInsReq.DataSource = null;
                    grvAppInsReq.AutoGenerateColumns = false;
                    grvAppInsReq.DataSource = _lst;
                }
            }
        
        }

        private void btnSendClaimReq_Click(object sender, EventArgs e)
        {           
        
           // RequestApprovalCycleDefinition(false, CD, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
        
            //REQUEST HEADER

            //REQUEST DETAILS

            //REQUEST LOG HEADER

            //REQUEST LOG DETAILS
        }

        private void grvPendingInsReq_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //GridViewRow row = grvInvItems.SelectedRow;
                DataGridViewRow row = grvPendingInsReq.Rows[rowIndex];

                string Req_no = row.Cells["pend_Grah_ref"].Value.ToString();
                //ddlPendingVehIns_req.SelectedItem = Req_no;
                txtPendingReqNo.Text = Req_no;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
        }
    }
}
