using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;

namespace FF.WebERPClient.Financial_Modules
{
    public class RemDet {
        private string date;
        private Decimal prevExcessRem;
        private Decimal excessRem;
        private Decimal cashIH;
        private Decimal amtRemited;
        private Decimal difference;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public Decimal PrevExcessRem
        {
            get { return prevExcessRem; }
            set { prevExcessRem = value; }
        }       

        public Decimal ExcessRem
        {
            get { return excessRem; }
            set { excessRem = value; }
        }      

        public Decimal CashIH
        {
            get { return cashIH; }
            set { cashIH = value; }
        }      

        public Decimal AmtRemited
        {
            get { return amtRemited; }
            set { amtRemited = value; }
        }
        public Decimal Difference
        {
            get { return difference; }
            set { difference = value; }
        }
    }
    public partial class scan_physical_docs : BasePage
    {
            

        public Dictionary<string, Decimal> BindSumList
        {
            get { return (Dictionary<string, Decimal>)Session["BindSumList"]; }
            set { Session["BindSumList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btnProcess.Attributes.Add("onclick", "return ConfirmDelete('" + btnVeiw.ClientID + "')");
           
            if(!IsPostBack)
            {
                DataTable dt = new DataTable();
                grvDocDetails.DataSource = dt;
                grvDocDetails.DataBind();
                grvRem.DataSource = dt;
                grvRem.DataBind();
                grvSellectTool.DataSource = dt;
                grvSellectTool.DataBind();
                grvPopUpExtraDocs.DataSource = dt;
                grvPopUpExtraDocs.DataBind();

                BindSumList = new Dictionary<string, Decimal>();

                DataTable DT = CHNLSVC.Financial.Get_GNR_RCV_DSK_DOC_Types();
                if (DT!=null)
                {
                    ddlPopUpDocTp.Items.Add(new ListItem("", ""));
                    foreach (DataRow dr in DT.Rows)
                    {
                        ddlPopUpDocTp.Items.Add(new ListItem(dr["grdt_name"].ToString(), dr["grdt_tp"].ToString()));
                    }
                }

                txtPC.Text = GlbUserDefProf;
                btnExtraDocs.Enabled = false;
                
            }
        }

        protected void txtMonthYear_TextChanged(object sender, EventArgs e)
        {
            string monthYear = txtMonthYear_.Text.Trim();
            try {
                DateTime DTmonth_ = Convert.ToDateTime(monthYear);
            }
            catch(Exception ex){
                return;
            }
            DateTime DTmonth = Convert.ToDateTime(monthYear);
           //DataTable dataTb = CHNLSVC.Financial.GetWeeks_on_month(10, 2012, -1);
            DataTable dataTb = CHNLSVC.Financial.GetWeeks_on_month(DTmonth.Month, DTmonth.Year, -1);
            ddlWeek.Items.Clear();
            ddlWeek.Items.Add(new ListItem(string.Empty, string.Empty));
            foreach (DataRow dr in dataTb.Rows)
            {               
                ddlWeek.Items.Add(new ListItem( "week "+ dr["gw_week"].ToString(), dr["gw_week"].ToString()));
            }
        }
       
        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                string monthYear = txtMonthYear_.Text.Trim();
                DateTime DTmonth = Convert.ToDateTime(monthYear);

                DataTable dataTb = CHNLSVC.Financial.GetWeeks_on_month(DTmonth.Month, DTmonth.Year, Convert.ToInt32(ddlWeek.SelectedValue));
                if (dataTb != null)
                {
                    lblFrmdtWk.Text = Convert.ToDateTime(dataTb.Rows[0]["gw_from_dt"]).Date.ToShortDateString();
                    lblTodtWk.Text = Convert.ToDateTime(dataTb.Rows[0]["gw_to_dt"].ToString()).Date.ToShortDateString(); ;
                    //_wk = Convert.ToDecimal(dataTb.Rows[0]["gw_week"]);

                }
                else
                {
                    lblFrmdtWk.Text = string.Empty;
                    lblTodtWk.Text = string.Empty; 
                }
            }
            catch(Exception ex){
                lblFrmdtWk.Text = string.Empty;
                lblTodtWk.Text = string.Empty;
            }

            List<RemDet> remList = new List<RemDet>();
            try {
                DateTime START_DT = Convert.ToDateTime(lblFrmdtWk.Text);
                DateTime END_DT = Convert.ToDateTime(lblTodtWk.Text);
                Int32 COUNT_DAYS = (END_DT.Date - START_DT.Date).Days;
                for (int i = 0; i < COUNT_DAYS; i++)
                {

                    Dictionary<string, Decimal> dayOne = CHNLSVC.Financial.Get_RemDet(GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblFrmdtWk.Text).AddDays(i));
                    RemDet remdt = new RemDet();
                    remdt.Date = Convert.ToDateTime(lblFrmdtWk.Text).AddDays(i).ToShortDateString();
                    remdt.PrevExcessRem = dayOne["PrvExcess"];
                    remdt.ExcessRem = dayOne["ExcessRem"];
                    remdt.CashIH = dayOne["CashInHand"];
                    remdt.AmtRemited = dayOne["AmtRemited"];
                    remdt.Difference = dayOne["Defference"];
                    remList.Add(remdt);
                }
            
            }
            catch(Exception ex){
            
            }
           
            //----
            grvRem.DataSource = remList;
            grvRem.DataBind();
            //TODO: fill the grvRem
            if (ddlWeek.SelectedValue == "" || ddlWeek.SelectedValue == string.Empty)
            {
                btnExtraDocs.Enabled = false;
            }
            else
            {
                btnExtraDocs.Enabled = true;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            BindSumList.Clear();
            grvSellectTool.DataSource = BindSumList;
            grvSellectTool.DataBind();

            grvDocDetails.DataSource = new DataTable();
            grvDocDetails.DataBind();

            DateTime DTmonth = Convert.ToDateTime(txtMonthYear_.Text.Trim());
            Int32 mon = DTmonth.Month;
            Int32 yr = DTmonth.Year;
            Int32 Week = Convert.ToInt32(ddlWeek.SelectedValue);
            
           Int32 del_eff= CHNLSVC.Financial.Delete_GNT_RCV_DSK_DOC(GlbUserComCode, txtPC.Text.ToUpper(), DTmonth, Week);
           Int32 eff = CHNLSVC.Financial.Save_GNT_RCV_DSK_DOC(GlbUserName, DateTime.Now.Date, GlbUserComCode, txtPC.Text.ToUpper(), DTmonth.Month, DTmonth.Year, Week, DTmonth);
           if (eff > 0)
           {
               MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Processed.");
           }
           else
           {
               MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Processed!");
           }
        }

        protected void ddlDocTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnVeiw_Click(object sender, EventArgs e)
        {
            BindSumList.Clear();
            grvSellectTool.DataSource = BindSumList;
            grvSellectTool.DataBind();
            DateTime firstDayofMonth = Convert.ToDateTime(txtMonthYear_.Text.Trim());
            string SELECT_DOC_TP = ddlDocTypes.SelectedValue;

            DataTable dt = new DataTable();
            if (SELECT_DOC_TP == "ALL")
            {
                dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(GlbUserComCode, txtPC.Text.Trim().ToUpper(), firstDayofMonth, Convert.ToInt32(ddlWeek.SelectedValue), null);
            }
            else {
                dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(GlbUserComCode, txtPC.Text.Trim().ToUpper(), firstDayofMonth, Convert.ToInt32(ddlWeek.SelectedValue), SELECT_DOC_TP);
            }

            grvDocDetails.DataSource = dt;
            grvDocDetails.DataBind();
          
            Dictionary<string ,Decimal> bindSumList=new Dictionary<string,decimal>();

            foreach(DataRow dr in  dt.Rows)
            {
               string docTp= dr["GRDD_DOC_DESC"].ToString();
               string isCheked= dr["GRDD_DOC_RCV"].ToString();
              // Decimal phyVal = Convert.ToDecimal(dr["GRDD_DOC_VAL"]);
               Decimal phyVal = 0;
               //TextBox PH_VAL = (TextBox)grvDocDetails.FindControl("txtgrvPhyVal");
               //Decimal phyVal = Convert.ToDecimal(PH_VAL.Text);//GRDD_DOC_VAL
               if (isCheked == "1")
               {
                   phyVal = Convert.ToDecimal(dr["GRDD_DOC_VAL"]);
                   int count = BindSumList.Count(D => D.Key.StartsWith(docTp));
                   if (count==0)
                   {
                       BindSumList.Add(docTp, phyVal);
                   }
                   else if (count>0)
                   {
                       Decimal val = BindSumList[docTp];
                       val = val+ phyVal;
                       //bindSumList[docTp] = val;
                       BindSumList[docTp] = val;
                   }
               }
            }
            // grvSellectTool.DataSource = bindSumList;//BindSumList
            grvSellectTool.DataSource = BindSumList;
            grvSellectTool.DataBind();
            
        }
        public static DateTime GetFirstDayOfNextMonth(DateTime startDate)
        {
            if (startDate.Month == 12) // its end of year , we need to add another year to new date:
            {
                startDate = new DateTime((startDate.Year + 1), 1, 1);
            }
            else
            {
                startDate = new DateTime(startDate.Year, (startDate.Month + 1), 1);
            }
            
            return startDate;
        }

        protected void grvDocDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    TextBox TX = (TextBox)e.Row.Cells[0].FindControl("txtgrvDate");
                    TX.Text = (Convert.ToDateTime(TX.Text).Date).ToShortDateString();
                }
                catch (Exception ex) { }
               
                try
                {
                    TextBox TX_RLZ = (TextBox)e.Row.Cells[0].FindControl("txtgrvRealizDt");
                    TX_RLZ.Text = (Convert.ToDateTime(TX_RLZ.Text).Date).ToShortDateString();
                }
                catch (Exception ex)
                {  }
                //--------------------------------
                Label lblDocTp = (Label)e.Row.FindControl("lblDocType");
                
                DataTable DT = CHNLSVC.Financial.GET_BANKS_of_PC_on_docType(GlbUserComCode, txtPC.Text.Trim().ToUpper(), lblDocTp.Text);
                DropDownList ddlDepositBank = (DropDownList)e.Row.FindControl("ddlDepBnkCd");
                ddlDepositBank.Items.Clear();
                ddlDepositBank.Items.Add("");
                if (DT != null)
                {

                    foreach (DataRow drow in DT.Rows)
                    {
                        ddlDepositBank.Items.Add(drow["grsa_bank_id"].ToString());
                        if (drow["grsa_is_default"].ToString() == "1")
                        {                           
                            ddlDepositBank.SelectedValue = drow["grsa_bank_id"].ToString();
                        }

                    }
                }

                //----------------permission allocation---------------------------------------------
                if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, string.Empty, "SCAN") == false)
                {
                    CheckBox chekgrvScnRecive = (CheckBox)e.Row.FindControl("chekgrvScnRecive");
                    chekgrvScnRecive.Enabled = false;

                    TextBox txtgrvRefNo = (TextBox)e.Row.FindControl("txtgrvRefNo");
                    txtgrvRefNo.Enabled = false;
                }                
                if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, string.Empty, "RCV") == false)
                {
                    CheckBox chekPhyReceive = (CheckBox)e.Row.FindControl("chekPhyReceive");
                    chekPhyReceive.Enabled = false;

                    TextBox txtgrvRemk = (TextBox)e.Row.FindControl("txtgrvRemk");
                    txtgrvRemk.Enabled = false;

                    TextBox txtgrvBranch = (TextBox)e.Row.FindControl("txtgrvBranch");
                    txtgrvBranch.Enabled = false;

                    TextBox txtgrvBankCd = (TextBox)e.Row.FindControl("txtgrvBankCd");
                    txtgrvBankCd.Enabled = false;

                    DropDownList ddlDepBnkCd = (DropDownList)e.Row.FindControl("ddlDepBnkCd");
                    ddlDepBnkCd.Enabled = false;

                    TextBox txtgrvPhyVal = (TextBox)e.Row.FindControl("txtgrvPhyVal");
                    txtgrvPhyVal.Enabled = false;
                    //---------------------------------------------------
                    TextBox txtgrvRealizDt = (TextBox)e.Row.FindControl("txtgrvRealizDt");
                    txtgrvRealizDt.Enabled = false;

                    CheckBox chekgrvIsRealiz = (CheckBox)e.Row.FindControl("chekgrvIsRealiz");
                    chekgrvIsRealiz.Enabled = false;
                }
                if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, string.Empty, "SUN") == false)
                {
                    CheckBox chekgrvSunUp = (CheckBox)e.Row.FindControl("chekgrvSunUp");
                    chekgrvSunUp.Enabled = false;
                }
                ////TextBox txtgrvRemk = (TextBox)e.Row.FindControl("txtgrvRemk");
                ////txtgrvRemk.Enabled = false;

                ////TextBox txtgrvBranch = (TextBox)e.Row.FindControl("txtgrvBranch");
                ////txtgrvBranch.Enabled = false;

                ////TextBox txtgrvBankCd = (TextBox)e.Row.FindControl("txtgrvBankCd");
                ////txtgrvBankCd.Enabled = false;

                ////DropDownList ddlDepBnkCd = (DropDownList)e.Row.FindControl("ddlDepBnkCd");
                ////ddlDepBnkCd.Enabled = false;

                ////TextBox txtgrvPhyVal = (TextBox)e.Row.FindControl("txtgrvPhyVal");
                ////txtgrvPhyVal.Enabled = false;

                ////CheckBox chekPhyReceive = (CheckBox)e.Row.FindControl("chekPhyReceive");
                ////chekPhyReceive.Enabled = false;

                ////TextBox txtgrvRealizDt = (TextBox)e.Row.FindControl("txtgrvRealizDt");
                ////txtgrvRealizDt.Enabled = false;

                ////CheckBox chekgrvIsRealiz = (CheckBox)e.Row.FindControl("chekgrvIsRealiz");
                ////chekgrvIsRealiz.Enabled = false;

                ////CheckBox chekgrvSunUp = (CheckBox)e.Row.FindControl("chekgrvSunUp");
                ////chekgrvSunUp.Enabled = false;

                ////CheckBox chekgrvScnRecive = (CheckBox)e.Row.FindControl("chekgrvScnRecive");
                ////chekgrvScnRecive.Enabled = false;

                ////TextBox txtgrvRefNo = (TextBox)e.Row.FindControl("txtgrvRefNo");
                ////txtgrvRefNo.Enabled = false;
               
                 
                //----------------------------------------------------------------------------------
            }

            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<ScanPhysicalDocReceiveDet> UpdateDocList = new List<ScanPhysicalDocReceiveDet>();
            foreach (GridViewRow gvr in grvDocDetails.Rows)
            {
                ScanPhysicalDocReceiveDet dicObj = new ScanPhysicalDocReceiveDet();
                dicObj.Grdd_com = GlbUserComCode;
                dicObj.Grdd_cre_by = GlbUserName;
                dicObj.Grdd_cre_dt = DateTime.Now.Date;

                //dicObj.Grdd_deposit_bank = ((TextBox)gvr.FindControl("txtgrvDepBankCd")).Text; //GRDD_DEPOSIT_BANK
                DropDownList ddlDepBank = (DropDownList)gvr.FindControl("ddlDepBnkCd");
                dicObj.Grdd_deposit_bank = ddlDepBank.SelectedValue;
               // dicObj.Grdd_doc_bank = gvr.Cells[3].Text; ;
                dicObj.Grdd_doc_bank_branch = ((TextBox)gvr.FindControl("txtgrvBranch")).Text;
                dicObj.Grdd_doc_bank_cd = ((TextBox)gvr.FindControl("txtgrvBankCd")).Text;
               // dicObj.Grdd_doc_desc = gvr.Cells[1].Text;
                dicObj.Grdd_doc_rcv = ((CheckBox)gvr.FindControl("chekPhyReceive")).Checked;
                dicObj.Grdd_doc_ref = ((TextBox)gvr.FindControl("txtgrvRefNo")).Text;
                //dicObj.Grdd_doc_tp = ;//NO NEED to assign
                dicObj.Grdd_doc_val = Convert.ToDecimal(((TextBox)gvr.FindControl("txtgrvPhyVal")).Text);
                dicObj.Grdd_dt = Convert.ToDateTime(((TextBox)gvr.FindControl("txtgrvDate")).Text).Date;
                //dicObj.Grdd_is_extra =;
                dicObj.Grdd_is_realized = ((CheckBox)gvr.FindControl("chekgrvIsRealiz")).Checked; 
                //dicObj.Grdd_month = ;
                //dicObj.Grdd_pc =;
                dicObj.Grdd_rcv_by = GlbUserName;
                dicObj.Grdd_rcv_dt = DateTime.Now.Date; ;
                dicObj.Grdd_realized_dt = Convert.ToDateTime(((TextBox)gvr.FindControl("txtgrvRealizDt")).Text).Date;
                dicObj.Grdd_rmk = ((TextBox)gvr.FindControl("txtgrvRemk")).Text; 
                dicObj.Grdd_scan_by = GlbUserName;
                dicObj.Grdd_scan_dt = DateTime.Now.Date;
                dicObj.Grdd_scan_rcv = ((CheckBox)gvr.FindControl("chekgrvScnRecive")).Checked;
                dicObj.Grdd_seq = Convert.ToInt32(((Label)gvr.FindControl("lblSeqNo")).Text);//**
                dicObj.Grdd_sun_upload = ((CheckBox)gvr.FindControl("chekgrvSunUp")).Checked;
                dicObj.Grdd_sun_up_by = GlbUserName;
                dicObj.Grdd_sun_up_dt = DateTime.Now.Date; ;
                dicObj.Grdd_sys_val = Convert.ToDecimal(gvr.Cells[4].Text);
                //dicObj.Grdd_week = ;
                UpdateDocList.Add(dicObj);
            }

            Int32 IS_SCAN = ((CheckBox)grvDocDetails.Rows[0].FindControl("chekgrvScnRecive")).Enabled == true ? 1 : 0;
            Int32 IS_SUN = ((CheckBox)grvDocDetails.Rows[0].FindControl("chekgrvSunUp")).Enabled == true ? 1 : 0;
            Int32 IS_RECEIVE = ((CheckBox)grvDocDetails.Rows[0].FindControl("chekPhyReceive")).Enabled == true ? 1 : 0;
            
            Int32 eff = 0;           
            eff = CHNLSVC.Financial.Update_GNT_RCV_DSK_DOC(UpdateDocList, IS_SCAN, IS_SUN, IS_RECEIVE);//update upon permissions
        
            if (eff > 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Updated.");
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Updated!");
            }
           
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            grvDocDetails.DataSource = new DataTable();
            grvDocDetails.DataBind();

            grvSellectTool.DataSource = new DataTable();
            grvSellectTool.DataBind();
        }

        protected void chekPhyReceive_CheckedChanged(object sender, EventArgs e)
        {
            Decimal phyVal = 0;
            CheckBox cb = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)cb.NamingContainer;
            if (gr != null)
            {
                int i = gr.RowIndex;
               // phyVal = Convert.ToDecimal(grvDocDetails.DataKeys[i].Value);//GRDD_DOC_VAL
                TextBox PH_VAL= (TextBox)grvDocDetails.Rows[i].FindControl("txtgrvPhyVal");
               
                phyVal = Convert.ToDecimal(PH_VAL.Text);//GRDD_DOC_VAL
                if (phyVal <= 0)
                {
                    PH_VAL.Text = (0).ToString();
                    cb.Checked = false;
                    return;
                }
                string docTp = grvDocDetails.Rows[i].Cells[1].Text;

                //------------------------------------
               // Dictionary<string, Decimal> bindSumList = (Dictionary<string, Decimal>)grvSellectTool.DataSource;
               
                int count = BindSumList.Count(D => D.Key.StartsWith(docTp));
                if (count == 0)
                {
                    BindSumList.Add(docTp, phyVal);
                }
                else if (count > 0)
                {
                    Decimal val = BindSumList[docTp];
                    if (cb.Checked)
                    {
                        val = val + phyVal;
                    }
                    else {
                        val = val - phyVal;
                    }
                    
                    //bindSumList[docTp] = val;
                    BindSumList[docTp] = val;
                }

                //--------------
                //if (isCheked == "1")
                //{
                //    int count = BindSumList.Count(D => D.Key.StartsWith(docTp));
                //    if (count == 0)
                //    {
                //        BindSumList.Add(docTp, phyVal);
                //    }
                //    else if (count > 0)
                //    {
                //        Decimal val = BindSumList[docTp];
                //        val = val + phyVal;
                //        //bindSumList[docTp] = val;
                //        BindSumList[docTp] = val;
                //    }
                //}
                //*****************

                
            }
            grvSellectTool.DataSource = BindSumList;
            grvSellectTool.DataBind();
            
            
        }

        protected void btnExtraDocs_Click(object sender, EventArgs e)
        {
            ModalPopupExtItem.Show();
        }

        protected void ddExtraDoclWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 Month= Convert.ToDateTime(txtGridExtraDocMonYr.Text).Month;
                Int32 Year = Convert.ToDateTime(txtGridExtraDocMonYr.Text).Year;
                DataTable dataTb = CHNLSVC.Financial.GetWeeks_on_month(Month, Year, Convert.ToInt32(ddExtraDoclWeek.SelectedValue));
                if (dataTb != null)
                {
                    lblExtraFromDtWk.Text = Convert.ToDateTime(dataTb.Rows[0]["gw_from_dt"]).Date.ToShortDateString();
                    lblExtraToDtWk.Text = Convert.ToDateTime(dataTb.Rows[0]["gw_to_dt"].ToString()).Date.ToShortDateString(); 
                    //_wk = Convert.ToDecimal(dataTb.Rows[0]["gw_week"]);

                }
                else
                {
                    lblExtraFromDtWk.Text = string.Empty;
                    lblExtraToDtWk.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblExtraFromDtWk.Text = string.Empty;
                lblExtraToDtWk.Text = string.Empty;
            }

            //TODO: LOAD THE GRIDVIEW.
            DateTime firstDayofMonth = Convert.ToDateTime(txtGridExtraDocMonYr.Text.Trim());
            string SELECT_DOC_TP = ddlPopUpDocTp.SelectedValue; 

            DataTable dt = new DataTable();
            //if (SELECT_DOC_TP == "ALL")
            //{
            //    dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(GlbUserComCode, txtPC.Text.Trim().ToUpper(), firstDayofMonth, Convert.ToInt32(ddlWeek.SelectedValue), null);
            //}
            //else {
            //    dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(GlbUserComCode, txtPC.Text.Trim().ToUpper(), firstDayofMonth, Convert.ToInt32(ddlWeek.SelectedValue), SELECT_DOC_TP);
            //}
           dt= CHNLSVC.Financial.Get_ShortBankDocs(GlbUserComCode, txtPC.Text.Trim().ToUpper(), firstDayofMonth, Convert.ToInt32(ddExtraDoclWeek.SelectedValue), null);

           grvPopUpExtraDocs.DataSource = dt;
           grvPopUpExtraDocs.DataBind();
            
           ModalPopupExtItem.Show();
        }

        protected void txtGridExtraDocMonYr_TextChanged(object sender, EventArgs e)
        {
            DateTime DTmonth = Convert.ToDateTime(txtGridExtraDocMonYr.Text.Trim());
            //DataTable dataTb = CHNLSVC.Financial.GetWeeks_on_month(10, 2012, -1);
            DataTable dataTb = CHNLSVC.Financial.GetWeeks_on_month(DTmonth.Month, DTmonth.Year, -1);
            ddExtraDoclWeek.Items.Clear();
            ddExtraDoclWeek.Items.Add(new ListItem(string.Empty, string.Empty));
            foreach (DataRow dr in dataTb.Rows)
            {
                ddExtraDoclWeek.Items.Add(new ListItem("week " + dr["gw_week"].ToString(), dr["gw_week"].ToString()));
            }
            ModalPopupExtItem.Show();
        }

        protected void txtPC_TextChanged(object sender, EventArgs e)
        {
           // txtMonthYear_.Text = string.Empty;
          //  lblFrmdtWk.Text = string.Empty;
          //  lblTodtWk.Text = string.Empty;
            //grvDocDetails.DataSource = new DataTable();
            //grvDocDetails.DataBind();
          
            //grvRem.DataSource = new DataTable();
            //grvRem.DataBind();

            //grvSellectTool.DataSource = new DataTable();
            //grvSellectTool.DataBind();
            
        }

        protected void btnExtraDocFind_Click(object sender, EventArgs e)
        {
            if (chekShortBS.Checked)
            {
                PanelShortBanking.Enabled = true;
            }
            else
            {
                PanelShortBanking.Enabled = false;
            }
            ModalPopupExtItem.Show();
        }

        protected void grvPopUpExtraDocs_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvPopUpExtraDocs.SelectedRow;
            Int32 SEQ = Convert.ToInt32(row.Cells[1].Text.Trim());
            HiddenField_seq.Value = SEQ.ToString();
            ScanPhysicalDocReceiveDet DOC= CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC_on_Seq(SEQ);
            txtExtraDocAmt.Text = DOC.Grdd_sys_val.ToString();
            txtExtraDocRef.Text = DOC.Grdd_doc_ref;
            txtExtraDocRemks.Text = "";

            txtExtraDocDate.Text = DateTime.Now.Date.ToShortDateString();
            try
                {
                    ddlPopUpDocTp.SelectedValue = DOC.Grdd_doc_tp;
                }
            catch(Exception ex){
                ddlPopUpDocTp.SelectedValue = "";
            }
            ModalPopupExtItem.Show();
        }

        protected void btnExtraDocAdd_Click(object sender, EventArgs e)
        {
            
            //TODO: new column fill GRDD_SHORT_REF
             //add to the table. call SP
            Int32 eff = 0;
            ScanPhysicalDocReceiveDet DOC = new ScanPhysicalDocReceiveDet();
            if (ddlPopUpDocTp.SelectedValue == "SHORT_BANK")
            {
                HiddenField_seq.Value = string.Empty;
            }
            if (HiddenField_seq.Value != string.Empty)
            {
                DOC = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC_on_Seq(Convert.ToInt32(HiddenField_seq.Value));
                DOC.Grdd_rcv_by = GlbUserName;
                DOC.Grdd_rcv_dt = DateTime.Now.Date;
                DOC.Grdd_doc_rcv = true;
                DOC.Grdd_rmk = txtExtraDocRemks.Text.Trim();
                DOC.Grdd_doc_val = Convert.ToDecimal(txtExtraDocAmt.Text);
                DOC.Grdd_dt = DateTime.Now.Date;//Convert.ToDateTime(txtExtraDocDate.Text);
               // DOC.Grdd_week = Convert.ToInt32(ddExtraDoclWeek.SelectedValue);
                DOC.Grdd_month = Convert.ToDateTime(txtMonthYear_.Text.Trim());
                DOC.Grdd_week = Convert.ToInt32(ddlWeek.SelectedValue);
               
                DOC.Grdd_is_extra = true;
                DOC.Grdd_short_ref = Convert.ToInt32(HiddenField_seq.Value);
                 eff = CHNLSVC.Financial.saveExtraDoc(DOC, true);
            }
            else
            {
                if (ddlPopUpDocTp.SelectedValue == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select document type!");
                    return;
                }
               
                DOC.Grdd_com = GlbUserComCode;
                DOC.Grdd_cre_by = GlbUserName;
                DOC.Grdd_cre_dt = DateTime.Now.Date;
                //DOC.Grdd_deposit_bank = null;
                //DOC.Grdd_doc_bank
                //DOC.Grdd_doc_bank_branch
                //DOC.Grdd_doc_bank_cd                
                DOC.Grdd_doc_rcv = true;
                if (ddlPopUpDocTp.SelectedValue == "SHORT_BANK")
                {
                    DOC.Grdd_doc_rcv = false;
                    
                }
                DOC.Grdd_doc_desc = ddlPopUpDocTp.SelectedItem.Text;
                DOC.Grdd_doc_ref = txtExtraDocRef.Text.Trim();
                DOC.Grdd_doc_tp = ddlPopUpDocTp.SelectedValue;
                DOC.Grdd_doc_val = Convert.ToDecimal(txtExtraDocAmt.Text.Trim());
                DOC.Grdd_dt = DateTime.Now.Date;//Convert.ToDateTime(txtExtraDocDate.Text.Trim());
                DOC.Grdd_is_extra = true;               
                //DOC.Grdd_is_realized
                
                DOC.Grdd_pc = txtPC.Text.Trim().ToUpper();
                DOC.Grdd_rcv_by = GlbUserName;
                DOC.Grdd_rcv_dt = DateTime.Now.Date;
                //  DOC.Grdd_realized_dt=
                DOC.Grdd_rmk=txtExtraDocRemks.Text;
               // DOC.Grdd_scan_by =
               //  DOC.Grdd_scan_dt;
               // DOC.Grdd_scan_rcv;
               //  DOC.Grdd_sys_val =
                DOC.Grdd_month = Convert.ToDateTime(txtMonthYear_.Text.Trim());
                DOC.Grdd_week = Convert.ToInt32(ddlWeek.SelectedValue);
                eff = CHNLSVC.Financial.saveExtraDoc(DOC, false);
            }
            if (eff > 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Added.");
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Added!");
            }
            HiddenField_seq.Value = string.Empty;
            txtExtraDocAmt.Text = "";
            txtExtraDocRef.Text = "";
            txtExtraDocRemks.Text = "";
            
        }

        protected void ddlPopUpDocTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string docType = ddlPopUpDocTp.SelectedValue;
            DataTable DT = CHNLSVC.Financial.GET_BANKS_of_PC_on_docType(GlbUserComCode, txtPC.Text.Trim().ToUpper(), docType);
            if (DT != null)
            {
                ddlPopUpBank.Items.Clear();
                foreach (DataRow drow in DT.Rows)
                {
                    ddlPopUpBank.Items.Add(drow["grsa_bank_id"].ToString());
                    if (drow["grsa_is_default"].ToString() == "1")
                    {
                        ddlPopUpBank.SelectedValue = drow["grsa_bank_id"].ToString();
                        
                    }

                }
            }
            ModalPopupExtItem.Show();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            BasePage _basePage = new BasePage();
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
               
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + _basePage.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void imgbtnSearchProfitCenter_Click(object sender, ImageClickEventArgs e)
        {
            BasePage _basePage = new BasePage();

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPC.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
    }
}