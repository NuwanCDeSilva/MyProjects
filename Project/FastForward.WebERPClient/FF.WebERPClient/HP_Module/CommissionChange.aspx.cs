using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;

namespace FF.WebERPClient.HP_Module
{
    public partial class CommissionChange : BasePage
    {

        public List<InvoiceHeader> ChangedInvoicesList
        {
            get { return (List<InvoiceHeader>)Session["ChangedInvoicesList"]; }
            set { Session["ChangedInvoicesList"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtInvoiceNo.Attributes.Add("onkeypress", "return fun1(event,'" + ImgBtnAddInvoice.ClientID + "')");
            
            //txtVehInsurNew.Attributes.Add("onblur", "return onblurFire(event,'" + btnCollCal.ClientID + "')");
            if(!IsPostBack)
            {
                DataTable dt = new DataTable();
                grvInvItems.DataSource=dt;
                grvInvItems.DataBind();
                grvPaymodeComm.DataSource=dt;
                grvPaymodeComm.DataBind();

                ChangedInvoicesList = new List<InvoiceHeader>();
                grvChangedInvoices.DataSource = dt;
                grvChangedInvoices.DataBind();

            }
        }

        #region Searchin
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Promotion:
                //    {
                //        paramsText.Append(txtCircular.Text.Trim().ToUpper() + seperator + "Promotion" + seperator);
                //        break;
                //    }
                //Sales_SubType
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_SubType:
                    {
                        paramsText.Append(txtType.Text.Trim().ToUpper() + seperator );
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + txtType.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                       // paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "INV" + seperator);
                          paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "INV" + seperator);  
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
        protected void ImgBtnType_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
            DataTable dataSource1 = CHNLSVC.General.GetSalesTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtType.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnSubType_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_SubType);
            DataTable dataSource1 = CHNLSVC.CommonSearch.Get_sales_subtypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSubType.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnInvoiceNo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesInvoice);
           // DataTable dataSource1 = CHNLSVC.CommonSearch.GetInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            DataTable dataSource1 = CHNLSVC.CommonSearch.GetInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
           
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInvoiceNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnAddInvoice_Click(object sender, ImageClickEventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();
           
           DataTable dt= CHNLSVC.Sales.Get_invoiceItemsForCommis(txtInvoiceNo.Text.Trim().ToUpper(), null);
           if (dt.Rows.Count > 0)
           {
               InvoiceHeader invoiceHdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
               if (invoiceHdr != null)
               {

                   if (GlbUserDefProf != invoiceHdr.Sah_pc)
                   {

                       string Msg = "<script>alert('Invoice belongs to another profit center!');</script>";
                       ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                       return;
                   }
                   else
                   {
                       Boolean isPeriodClose = CHNLSVC.Financial.IsPeriodClosed(GlbUserComCode, GlbUserDefProf, "FIN_REM", invoiceHdr.Sah_dt);
                       if (isPeriodClose == true)
                       {
                           this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Period Closed!");
                           return;
                       }

                       lblInvoiceDate.Text = invoiceHdr.Sah_dt.ToShortDateString();
                       grvInvItems.DataSource = dt;
                       grvInvItems.DataBind();
                   }

               }
               else
               {
                   string Msg = "<script>alert('Invoice could not be found!');</script>";
                   ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                   return;
               }                          
              
           }
           else
           {
               
               DataTable emptyDt = new DataTable();
               grvInvItems.DataSource = emptyDt;
               grvInvItems.DataBind();
           }
        }

        protected void grvInvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvInvItems.SelectedRow;
            string ItemCode = row.Cells[1].Text.Trim();
            string Descrtipt = row.Cells[2].Text.Trim();
            txtItmCode.Text = ItemCode;
            txtDescript.Text = Descrtipt;           
          //  txtCommRt.Text = row.Cells[5].Text.Trim();
            txtComAmt.Text = row.Cells[5].Text.Trim();
            txtNetVal.Text = row.Cells[6].Text.Trim();

            DataTable dt = CHNLSVC.Sales.Get_Paymodes_ofItemsForCommis(txtInvoiceNo.Text.Trim().ToUpper(), ItemCode);
            if (dt.Rows.Count > 0)
            {
                grvPaymodeComm.DataSource = dt;
                grvPaymodeComm.DataBind();
            }
            else
            {
                DataTable emptyDt = new DataTable();
                grvPaymodeComm.DataSource = emptyDt;
                grvPaymodeComm.DataBind();
            }
           
        }

        protected void grvPaymodeComm_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = grvInvItems.SelectedRow;
            string ItemCode = row.Cells[1].Text.Trim();
        }

        protected void grvPaymodeComm_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvPaymodeComm.SelectedRow;
            TextBox finalcommRt = (TextBox)row.Cells[6].FindControl("txtFinCommRt");
            TextBox finalcommAmt = (TextBox)row.Cells[7].FindControl("txtFinCommAmt");
           
        }

        protected void grvPaymodeComm_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex = e.RowIndex; 
            TextBox finalcommRt = (TextBox)grvPaymodeComm.Rows[rowIndex].FindControl("txtFinCommRt");
             Decimal finComm_Rt=Convert.ToDecimal(finalcommRt.Text);
             if (finComm_Rt > 100)
             {
                 this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid rate!");
                 finalcommRt.Focus();
                 return;
             }

            TextBox finalcommAmt = (TextBox)grvPaymodeComm.Rows[rowIndex].FindControl("txtFinCommAmt");
            Decimal calOn = Convert.ToDecimal(grvPaymodeComm.Rows[rowIndex].Cells[3].Text);
            Decimal finComm_Amt = calOn * (finComm_Rt / 100);
            finComm_Amt = Math.Round(finComm_Amt, 2);
            finalcommAmt.Text = finComm_Amt.ToString();
           // Decimal finComm_Amt=Convert.ToDecimal(finalcommAmt.Text);
            //TextBox finalcommRt = (TextBox)row.Cells[6].FindControl("txtFinCommRt");
           // TextBox finalcommAmt = (TextBox)row.Cells[7].FindControl("txtFinCommAmt");
            
            Label commLine = (Label)grvPaymodeComm.Rows[rowIndex].FindControl("lblCommLine");
            Label InvoiceNo = (Label)grvPaymodeComm.Rows[rowIndex].FindControl("lblInvoiceNo");
            Label ItemLine = (Label)grvPaymodeComm.Rows[rowIndex].FindControl("lblItemLine");
            
            string itemCode = Convert.ToString(grvPaymodeComm.Rows[rowIndex].Cells[1].Text);
            Int32 comm_line = Convert.ToInt32(commLine.Text);
            string invoiceNo = InvoiceNo.Text;
            Int32 itm_line = Convert.ToInt32(ItemLine.Text);

            Int32 eff = CHNLSVC.Sales.UpdateCommissionLine(invoiceNo, itemCode,itm_line, comm_line, finComm_Rt, finComm_Amt);

            if (eff>0)
            {
                try {

                    InvoiceHeader invoiceHdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(invoiceNo);                   
                    ChangedInvoicesList.Add(invoiceHdr);
                    grvChangedInvoices.DataSource = ChangedInvoicesList;
                    grvChangedInvoices.DataBind();
                }
                catch (Exception ex)
                { 
                
                }
                string Msg = "<script>alert('Commission Changed!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
            //Int32 eff2 = CHNLSVC.Financial.ProcessFinalizeDayEnd(Convert.ToDateTime(lblInvoiceDate.Text), GlbUserComCode, GlbUserDefProf);
        }
        protected void grvProfCents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chekMand = (CheckBox)e.Row.FindControl("chekPc");
                if (!chekMand.Checked)
                {
                    chekMand.Checked = true;
                }
            }
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void btnNone_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chekPc");
                chkSelect.Checked = false;
            }
        }

        protected void btnClearPcList_Click(object sender, EventArgs e)
        {
            grvProfCents.DataSource = null;
            grvProfCents.DataBind();
        }

        protected void btnAddToPC_list_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearch1.Company;
            string chanel = uc_ProfitCenterSearch1.Channel;
            string subChanel = uc_ProfitCenterSearch1.SubChannel;
            string area = uc_ProfitCenterSearch1.Area;
            string region = uc_ProfitCenterSearch1.Region;
            string zone = uc_ProfitCenterSearch1.Zone;
            string pc = uc_ProfitCenterSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            grvProfCents.DataSource = dt;
            grvProfCents.DataBind();
        }

        protected void btnProcessCirc_Click(object sender, EventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();
            List<string> pc_list=GetSelectedPCList();
            if (pc_list.Count<1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter profit center(s)!");
                return;
            }
            //Int32 eff= CHNLSVC.Sales.circularWise_Commission_change(GlbUserComCode, pc_list, DateTime.Now.Date, txtCircularName.Text.Trim().ToUpper(), null);
            DateTime dtfrm=Convert.ToDateTime(txtFrm_dt.Text.Trim()).Date;
            DateTime dtTo=Convert.ToDateTime(txtTo_dt.Text.Trim()).Date;
            
            Double no_ofDays=(dtTo - dtfrm).TotalDays; 
             List<DateTime> dates=new List<DateTime>();
             dates.Add(dtfrm);
            for(int i=0; i< no_ofDays; i++) 
            {
                dates.Add(dtfrm.AddDays(1));
            }
           
            //DateTime DT = DateTime.Now.Date.AddDays(-4);
            Int32 eff = CHNLSVC.Sales.Process_Commission_change_at_PC(GlbUserComCode, pc_list, dates);
            if (eff>0)
            {               
                
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Succussfylly changed!");
                string Msg = "<script>alert('Commission Changed!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
        
        }
        private List<string> GetSelectedPCList()
        {
            List<string> list = new List<string>();
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chekPc");
                if (chkSelect.Checked)
                {
                    list.Add(gvr.Cells[1].Text);
                }
            }
            return list;
        }

        protected void grvInvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            

        }

        protected void btnHiddenView_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvPaymodeComm.Rows)
            {
                TextBox finComRt = (TextBox)gvr.Cells[6].FindControl("txtFinCommRt");
                if (finComRt.Text.Trim()!=string.Empty||finComRt.Text.Trim()!="")
                {
                    TextBox finComAmt = (TextBox)gvr.Cells[6].FindControl("txtFinCommAmt");
                    Decimal fin_rt = Convert.ToDecimal(finComRt.Text);
                    //Decimal fin_amt = Convert.ToDecimal(finComRt.Text);
                    Decimal calOn_ = Convert.ToDecimal(gvr.Cells[3].Text);
                    finComAmt.Text = (calOn_ * (fin_rt / 100)).ToString();
                }
                               
            }
        }

        protected void grvPaymodeComm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (grvPaymodeComm.Rows.Count > 0)
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        TextBox txt = (TextBox)e.Row.FindControl("txtFinCommRt");
            //        // txt.Attributes.Add("onBlur", "JvfunonBlur();");
            //        txt.Attributes.Add("onblur", "return fun1(event,'" + btnHiddenView.ClientID + "')");

            //    }
            //}
        }

        protected void ImgCalc_Click(object sender, ImageClickEventArgs e)
        {
          
            foreach (GridViewRow gvr in grvPaymodeComm.Rows)
            {
                TextBox finComRt = (TextBox)gvr.Cells[6].FindControl("txtFinCommRt");
                TextBox finComAmt = (TextBox)gvr.Cells[6].FindControl("txtFinCommAmt");
                Decimal fin_rt = Convert.ToDecimal(finComRt.Text);
                if (fin_rt>100)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid rate!");
                    finComRt.Focus();
                    return;
                }
                //Decimal fin_amt = Convert.ToDecimal(finComRt.Text);
                Decimal calOn_ = Convert.ToDecimal(gvr.Cells[3].Text);                
                Decimal fin_amt = calOn_ * (fin_rt / 100);
                fin_amt = Math.Round(fin_amt, 2);
                finComAmt.Text = fin_amt.ToString();

            }
        }

        protected void ImgBtnCircular_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnFinalSave_Click(object sender, EventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();         
            List<string> dayList = new List<string>();
            foreach (InvoiceHeader inv in ChangedInvoicesList)
            {
                string date= inv.Sah_dt.ToShortDateString();                              

                if (dayList.Contains(date) == false)
                {
                    dayList.Add(date);
                }

            }

            //-----------
            Int32 eff2 = 0;
            foreach (string date in dayList)
            {
                 eff2 = CHNLSVC.Financial.ProcessFinalizeDayEnd(Convert.ToDateTime(date), GlbUserComCode, GlbUserDefProf);               
            }
            if (eff2 > 0)
            {
               // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Succussfylly Finalized!");
                string Msg = "<script>alert('Succussfylly Finalized!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
               // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Not Processed!");
                string Msg = "<script>alert('Failed to Finalize!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }

        protected void btnFinalize_Click(object sender, EventArgs e)
        {
            
            this.MasterMsgInfoUCtrl.Clear();
            try
            {
                DateTime dtfrm_ = Convert.ToDateTime(txtFrm_dt.Text.Trim()).Date;
                DateTime dtTo_ = Convert.ToDateTime(txtTo_dt.Text.Trim()).Date;
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid dates!");
                return;
            }
            DateTime dtfrm = Convert.ToDateTime(txtFrm_dt.Text.Trim()).Date;
            DateTime dtTo = Convert.ToDateTime(txtTo_dt.Text.Trim()).Date;

            Double no_ofDays = (dtTo - dtfrm).TotalDays;
            List<DateTime> dates = new List<DateTime>();
            dates.Add(dtfrm);
            for (int i = 0; i < no_ofDays; i++)
            {
                dates.Add(dtfrm.AddDays(1));
            }
            List<string> list = new List<string>();
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chekPc");
                if (chkSelect.Checked)
                {
                    list.Add(gvr.Cells[1].Text);
                }
            }
            //-------------------------------------------
            string company= uc_ProfitCenterSearch1.Company;
            try { 
                    foreach (DateTime dt in dates)
                    {
                        foreach(string pc in list)
                        {
                            Int32 eff = CHNLSVC.Financial.ProcessFinalizeDayEnd(dt, company, pc); 
                        }
            
                    }
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Succussfylly Finalized!");
                    string Msg = "<script>alert('Succussfylly Finalized!');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            catch(Exception ex){
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Failed to Finalize!");
                string Msg = "<script>alert('Failed to Finalize!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            
            }
            
            
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/CommissionChange.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
    }
}