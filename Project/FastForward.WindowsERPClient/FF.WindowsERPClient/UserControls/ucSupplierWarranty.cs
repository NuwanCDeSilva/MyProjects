using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.UserControls
{
    public partial class ucSupplierWarranty : UserControl
    {
        private Base _basePage;

        private String _JobNumer = string.Empty;
        private Int32 _JobLineNum = 0;
        public String GblJobNumber
        {
            get { return _JobNumer; }
            set { _JobNumer = value; }
        }

        public Int32 GblJobLine
        {
            get { return _JobLineNum; }
            set { _JobLineNum = value; }
        }

        public ucSupplierWarranty()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            _basePage = new Base();

            dgvWarrantyDetailsD23.DataSource = new List<Service_Enquiry_SupplierWrntyDetails>();
            dgvReceivedDetails.DataSource = new List<Service_Enquiry_SupplierWrntyDetails>();
            DataTable DtTemp ;
            if (optJob.Checked)
            {
                DtTemp = _basePage.CHNLSVC.CustService.GetSupplierWarrantyClaimRequestedItems(BaseCls.GlbUserComCode, _JobNumer, _JobLineNum);
            }
            else
            {
                DtTemp = _basePage.CHNLSVC.CustService.GET_SUP_WRNT_CLM_Requested_Serial(BaseCls.GlbUserComCode, _JobNumer, _JobLineNum);
            }

           


            if (DtTemp != null && DtTemp.Rows.Count > 0)
            {
                dgvRequestedItemsD16.AutoGenerateColumns = false;
                dgvRequestedItemsD16.DataSource = DtTemp;

                if (DtTemp.Rows.Count == 1)
                {
                    dgvRequestedItemsD16_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                }
            
            }
            else
            {
                MessageBox.Show("Selected Items has no records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             
                return;
            }
        }

        private void dgvRequestedItemsD16_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                String job = dgvRequestedItemsD16.Rows[e.RowIndex].Cells["JOBD16"].Value.ToString();
                Int32 LineNum = Convert.ToInt32(dgvRequestedItemsD16.Rows[e.RowIndex].Cells["JOBLINED16"].Value.ToString());
                Int32 SeqNUm = Convert.ToInt32(dgvRequestedItemsD16.Rows[e.RowIndex].Cells["SEQD16"].Value.ToString());
                String Type = dgvRequestedItemsD16.Rows[e.RowIndex].Cells["ItemStatusTextD16"].Value.ToString();

                lblStatusSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["sentwcnD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["sentwcnD16"].Value.ToString();
                lblReceiveStatusSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["recwncD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["recwncD16"].Value.ToString();
                lblSerialSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["SerialD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["SerialD16"].Value.ToString();
                lblQtySWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["QTYD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["QTYD16"].Value.ToString();
                lblCategorySWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["FROMTABLED16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["FROMTABLED16"].Value.ToString();
                if (  dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REJECT_DT"].Value.ToString () != "")
                {
                    lblRejectedDateSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REJECT_DT"].Value == null) ? string.Empty : Convert.ToDateTime(dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REJECT_DT"].Value.ToString()).Date.ToString("dd/MM/yyyy");
                }
                lblPartIDSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["PartIDD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["PartIDD16"].Value.ToString();
                lblOEMSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["OEMD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["OEMD16"].Value.ToString();
                lblCaseIdSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["CaseIDD16"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["CaseIDD16"].Value.ToString();
                lblRequestBySWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REQWCN_BY"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REQWCN_BY"].Value.ToString();
                lblReqeustDateSWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REQWCNDT"].Value == null) ? string.Empty : Convert.ToDateTime ( dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REQWCNDT"].Value.ToString()).Date.ToString("dd/MM/yyyy");
                lblRejectBySWC.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REJECT_BY"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["REJECT_BY"].Value.ToString();
                lblHoldReason.Text = (dgvRequestedItemsD16.Rows[e.RowIndex].Cells["COLRES"].Value == null) ? string.Empty : dgvRequestedItemsD16.Rows[e.RowIndex].Cells["COLRES"].Value.ToString();
                lblRemarks.Text = "";
                lblETADateSWC.Text = "";

                List<Service_Enquiry_SupplierWrntyDetails> oitems = _basePage.CHNLSVC.CustService.GET_SCV_SUPP_WRNTREQHDR_ENQ(job, LineNum, SeqNUm, Type);
                dgvWarrantyDetailsD23.AutoGenerateColumns = false;
                dgvWarrantyDetailsD23.DataSource = new List<Service_Enquiry_SupplierWrntyDetails>();
                if (oitems != null && oitems.Count > 0)
                {
                    for (int i = 0; i < oitems.Count; i++)
                    {
                        //if (oitems[i].SWC_STUS.ToString() == "H")
                        //{
                        //    lblHoldReason.Text = oitems[i].SWC_HOLD_REASON.ToString();                            
                        //}                        
                        lblRemarks.Text = oitems[i].SWC_RMKS.ToString();
                        if (Convert.ToDateTime(oitems[i].SWC_ETA.ToString()).Date != Convert.ToDateTime("01/JAN/0001").Date)
                        {
                            lblETADateSWC.Text = (oitems[i].SWC_ETA == null) ? string.Empty : Convert.ToDateTime(oitems[i].SWC_ETA.ToString()).Date.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            lblETADateSWC.Text = string.Empty;
                        }
                    }
                    if (oitems.FindAll(x => x.SWC_TP == "0").Count > 0)
                    {
                        dgvWarrantyDetailsD23.AutoGenerateColumns = false;
                        dgvWarrantyDetailsD23.DataSource = oitems.FindAll(x => x.SWC_TP == "0");

                        if (dgvWarrantyDetailsD23.Rows.Count == 1)
                        {
                            dgvWarrantyDetailsD23_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                        }
                    }
                    if (oitems.FindAll(x => x.SWC_TP == "1").Count > 0)
                    {
                        dgvReceivedDetails.AutoGenerateColumns = false;
                        dgvReceivedDetails.DataSource = oitems.FindAll(x => x.SWC_TP == "1");

                        if (dgvReceivedDetails.Rows.Count == 1)
                        {
                            dgvReceivedDetails_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                        }
                    }
                }
            }
        }

        private void btnCloseSerialPnl_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvRequestedItemsD16_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvWarrantyDetailsD23_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void clerSWCLables()
        {
            lblStatusSWC.Text = "";
            lblReceiveStatusSWC.Text = "";
            lblSerialSWC.Text = "";
            lblQtySWC.Text = "";
            lblCategorySWC.Text = "";
            lblRejectBySWC.Text = "";
            lblPartIDSWC.Text = "";
            lblOEMSWC.Text = "";
            lblCaseIdSWC.Text = "";
            lblRequestBySWC.Text = "";
            lblReqeustDateSWC.Text = "";
            lblRejectedDateSWC.Text = "";
            lblHoldReason.Text = "";

            lblSD2DocNumber.Text = "";
            lblRequestNumberasdasd.Text = "";
            lblItemCodeqwe.Text = "";
            lblSuppIteme3fg.Text = "";
            lblSerialrf43.Text = "";
            lblStatussad.Text = "";
            lblHoldReasonasd.Text = "";
            lblWarrantyNumasd.Text = "";
            lblSuppWarrntyNumaSd.Text = "";
            lblOEMSerialNumasd.Text = "";
            lblCaseID.Text = "";
            lblOtherDocasasd.Text = "";
            lblItemStatusasd.Text = "";
            lblDocNumasd.Text = "";
            lblDateasd.Text = "";
            lblTypeasd.Text = "";
            lblSuppilerasd.Text = "";
            lblClaimSupplierasd.Text = "";
            lblOtherDoc.Text = "";
            lblRemarkasd.Text = "";
            lblBillNumasd.Text = "";
            lblBillDate.Text = "";

            label136.Text = "";
            label162.Text = "";
            label138.Text = "";
            label165.Text = "";
            label147.Text = "";
            label169.Text = "";
            label140.Text = "";
            label167.Text = "";
            label149.Text = "";
            label171.Text = "";
            label137.Text = "";
            label164.Text = "";
            label139.Text = "";
            label166.Text = "";
            label148.Text = "";
            label170.Text = "";
            label146.Text = "";
            label168.Text = "";
            label150.Text = "";
            label172.Text = "";
            label173.Text = "";
            label174.Text = "";
        }
        private void dgvWarrantyDetailsD23_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                lblSD2DocNumber.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_DOC_NO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_DOC_NO"].Value.ToString();
                lblRequestNumberasdasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["swc_othdocnoD16"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["swc_othdocnoD16"].Value.ToString();
                lblItemCodeqwe.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_ITMCD"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_ITMCD"].Value.ToString();
                lblSuppIteme3fg.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SUPPITMCD"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SUPPITMCD"].Value.ToString();
                lblSerialrf43.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SER1"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SER1"].Value.ToString();
                lblStatussad.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_STUS"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_STUS"].Value.ToString();
                lblHoldReasonasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["swc_hold_reason"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["swc_hold_reason"].Value.ToString();
                lblWarrantyNumasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_WARRNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_WARRNO"].Value.ToString();
                lblSuppWarrntyNumaSd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SUPPWARRNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_SUPPWARRNO"].Value.ToString();
                lblOEMSerialNumasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_OEMSERNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_OEMSERNO"].Value.ToString();
                lblCaseID.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_CASEID"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_CASEID"].Value.ToString();
                lblOtherDocasasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_OTHDOCNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_OTHDOCNO"].Value.ToString();
                lblItemStatusasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_ITM_STUS"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWD_ITM_STUS"].Value.ToString();
                lblDocNumasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_DOC_NO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_DOC_NO"].Value.ToString();
                lblDateasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_DT"].Value == null) ? string.Empty : Convert.ToDateTime(dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_DT"].Value.ToString()).Date.ToString("dd/MM/yyyy");
                lblTypeasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_TP"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_TP"].Value.ToString();
                lblSuppilerasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_SUPP"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_SUPP"].Value.ToString();
                lblClaimSupplierasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_CLM_SUPP"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_CLM_SUPP"].Value.ToString();
                lblOtherDoc.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_OTHDOCNO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_OTHDOCNO"].Value.ToString();
                lblRemarkasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_RMKS"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_RMKS"].Value.ToString();
                lblBillNumasd.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_AIR_BILL_NO"].Value == null) ? string.Empty : dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_AIR_BILL_NO"].Value.ToString();
                lblBillDate.Text = (dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_BILL_DT"].Value == null) ? string.Empty : Convert.ToDateTime(dgvWarrantyDetailsD23.Rows[e.RowIndex].Cells["SWC_BILL_DT"].Value.ToString()).Date.ToString("dd/MM/yyyy");

 
            }
        }

        private void dgvReceivedDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                label136.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn1"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn1"].Value.ToString();
                label162.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].Value.ToString();
                label138.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn3"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn3"].Value.ToString();
                label165.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn4"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn4"].Value.ToString();
                label147.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn5"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn5"].Value.ToString();
                label169.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn6"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn6"].Value.ToString();
                label140.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn7"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn7"].Value.ToString();
                label167.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn8"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn8"].Value.ToString();
                label149.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn9"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn9"].Value.ToString();
                label171.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn10"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn10"].Value.ToString();
                label137.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn11"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn11"].Value.ToString();
                label164.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn12"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn12"].Value.ToString();
                label139.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn13"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn13"].Value.ToString();
                label166.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn14"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn14"].Value.ToString();
                label148.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn15"].Value == null) ? string.Empty : Convert.ToDateTime(dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn15"].Value.ToString()).Date.ToString("dd/MM/yyyy");
                label170.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn16"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn16"].Value.ToString();
                label146.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn17"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn17"].Value.ToString();
                label168.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn18"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn18"].Value.ToString();
                label150.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn19"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn19"].Value.ToString();
                label172.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn20"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn20"].Value.ToString();
                label173.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn21"].Value == null) ? string.Empty : dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn21"].Value.ToString();
                label174.Text = (dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn22"].Value == null) ? string.Empty : Convert.ToDateTime(dgvReceivedDetails.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn22"].Value.ToString()).Date.ToString("dd/MM/yyyy");
                
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void optSer_CheckedChanged(object sender, EventArgs e)
        {
            clerSWCLables();
        
            LoadData();
        }

        private void optJob_CheckedChanged(object sender, EventArgs e)
        {
            clerSWCLables();

            LoadData();
        }

        private void dgvReceivedDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pnlSupplieWarrantyClaim_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
