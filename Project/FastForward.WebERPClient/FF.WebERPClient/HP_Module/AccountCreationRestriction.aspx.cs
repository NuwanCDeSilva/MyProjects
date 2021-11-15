using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;
using System.Drawing;
using System.Text;

namespace FF.WebERPClient.HP_Module
{
    public partial class AccountCreationRestriction : BasePage
    {
        //private List<AccountRestriction> accRestrList = null;//=new List<AccountRestriction>();
        //private Int32 restrSEQ = 0;

        public Int32 RestrSEQ
        {
            get { return Convert.ToInt32(ViewState["RestrSEQ"]); }
            set { ViewState["RestrSEQ"] = value; }
        }
        public List<AccountRestriction> AccRestrList
        {
            get { return (List<AccountRestriction>)ViewState["AccRestrList"]; }
            set { ViewState["AccRestrList"] = value; }
        }
        
        //******
        public List<Hpr_SysParameter> Hpr_ParaList
        {
            get { return (List<Hpr_SysParameter>)ViewState["Hpr_ParaList"]; }
            set { ViewState["Hpr_ParaList"] = value; }
        }
        public List<Hpr_SysParameter> Show_Hpr_ParaList
        {
            get { return (List<Hpr_SysParameter>)ViewState["Show_Hpr_ParaList"]; }
            set { ViewState["Show_Hpr_ParaList"] = value; }
        }
        public List<string> ClonePcList
        {
            get { return (List<string>)Session["ClonePcList"]; }
            set { Session["ClonePcList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtParaCode.Attributes.Add("onblur", "return onblurFire(event,'" + btnHidn.ClientID + "')");
            
            txtParaCodePopUp.Attributes.Add("onblur", "return onblurFire(event,'" + btnModPopBlur1.ClientID + "')");
            txtCloneParaCode.Attributes.Add("onblur", "return onblurFire(event,'" + btnModPopBlur2.ClientID + "')");

            if (!IsPostBack)
            {
                divNoOfMonths.Visible = false;
                AccRestrList = new List<AccountRestriction>();
                RestrSEQ = 0;
                DataTable dt = new DataTable();
                grvResctrict.DataSource = dt;
                grvResctrict.DataBind();
                clearScreen();

                //********
                Hpr_ParaList = new List<Hpr_SysParameter>();
                Show_Hpr_ParaList = new List<Hpr_SysParameter>();
                ClonePcList = new List<string>();
                grvClonePc.DataSource = ClonePcList;
                grvClonePc.DataBind();

                DataTable dt2 = new DataTable();
                grvParameters.DataSource = dt2;
                grvParameters.DataBind();
            }
        }
        private void clearScreen()
        {

            AccRestrList.Clear();
            DataTable dt = new DataTable();
            grvResctrict.DataSource = dt;
            grvResctrict.DataBind();

            MasterMsgInfoUCtrl.Clear();
            uc_ProfitCenterSearch1.Clear();

            txtApprNoOfAcc.Text = "";
            txtFromDate.Text = "";
            txtNoOfMonths.Text = "";
            txtSalesVal.Text = "";
            txtToDate.Text = "";

            uc_ProfitCenterSearch1.Company = GlbUserComCode;
            uc_ProfitCenterSearch1.CompanyDes = GlbUserComDesc;
            //rdoAnual.Checked = true;
            txtFromDate.Text = DateTime.Now.Date.ToShortDateString();
            clearAddScreen();

        }
        private void clearScreen_para()
        {
            Hpr_ParaList = new List<Hpr_SysParameter>();
            Show_Hpr_ParaList = new List<Hpr_SysParameter>();

            DataTable dt = new DataTable();
            grvParameters.DataSource = dt;
            grvParameters.DataBind();

            MasterMsgInfoUCtrl.Clear();
            uc_ProfitCenterSearch2.Clear();
            uc_ProfitCenterSearch2.Company = GlbUserComCode;
            uc_ProfitCenterSearch2.CompanyDes = GlbUserComDesc;

            txtParaCode.Text = string.Empty;
            txtFromDt_pty.Text = string.Empty;
            txtToDt_pty.Text = string.Empty;
            txtValue_pty.Text = string.Empty;

        }
        protected void btn_SAVE_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, string.Empty, "ACRES") == false)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                return;
            }
            if (AccRestrList.Count == 0)
            {
                return;
            }
            Int32 effect = 0;
            effect = CHNLSVC.Sales.SaveAccRestriction(AccRestrList);
            if (effect > 0)
            {
                clearScreen();

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved!");

                string Msg = "<script>alert('Successfully Saved!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                clearScreen();

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Could not save!");
                return;
            
            }
        }

        protected void btn_CLEAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/AccountCreationRestriction.aspx");
        }

        protected void btn_CLOSE_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void rdoAnual_CheckedChanged(object sender, EventArgs e)
        {
            clearAddScreen();
            divToDate.Visible = true;
            divNoOfMonths.Visible = false;
            
        }

        protected void rdoMonthly_CheckedChanged(object sender, EventArgs e)
        {
            clearAddScreen();
            divToDate.Visible = false;
            divNoOfMonths.Visible = true;
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

        private void AddToRestrictionList()
        {
            //  List<AccountRestriction> list = new List<AccountRestriction>();
            MasterMsgInfoUCtrl.Clear();
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                
                if (AccRestrList == null)
                {
                    AccRestrList = new List<AccountRestriction>();
                }

                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                if (chkSelect.Checked)
                {
                    string profCenter = gvr.Cells[0].Text.Trim();

                    AccountRestriction AccRestr = new AccountRestriction();
                    AccRestr.Hrs_cre_by = GlbUserName;
                    AccRestr.Hrs_cre_dt = DateTime.Now.Date;

                    AccRestr.Hrs_no_ac = Convert.ToInt64(txtApprNoOfAcc.Text.Trim());
                    AccRestr.Hrs_pc = profCenter;
                    AccRestr.Hrs_seq = RestrSEQ++; //this is generated automatically by DB Sequence when inserting
                    AccRestr.Hrs_tot_val = Convert.ToDecimal(txtSalesVal.Text.Trim());
                    try
                    {
                        AccRestr.Hrs_from_dt = Convert.ToDateTime(txtFromDate.Text.Trim());
                        if (txtToDate.Text.Trim() != "")
                        {
                            AccRestr.Hrs_to_dt = Convert.ToDateTime(txtToDate.Text.Trim());
                        }
                        
                    }
                    catch (Exception ex)
                    {

                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter valid Date!");
                        return;
                    }
                    if (rdoAnual.Checked)
                    {
                        AccRestr.Hrs_tp = 2;

                        if (txtToDate.Text.Trim() == "")
                        {
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter 'To Date'.");
                            return;
                        }
                        AccRestr.Hrs_to_dt = Convert.ToDateTime(txtToDate.Text.Trim());                    
                        
                    }
                    else
                    {                        
                        AccRestr.Hrs_to_dt = AccRestr.Hrs_from_dt.AddMonths(1);
                        AccRestr.Hrs_to_dt = AccRestr.Hrs_to_dt.AddDays(-1);                       
                        AccRestr.Hrs_tp = 1;                       
                        
                    }
                    AccRestrList.Add(AccRestr);
                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try {
                Convert.ToInt64(txtApprNoOfAcc.Text.Trim());
            }
            catch(Exception ex){
                txtApprNoOfAcc.Focus();
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Exceeds max length.");
                return;
            }
            try
            {
                Convert.ToDecimal(txtSalesVal.Text.Trim());
            }
            catch (Exception ex)
            {
                txtSalesVal.Focus();
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Exceeds max length.");
                return;
            }
           
            if (grvProfCents.Rows.Count > 0)
            {
                if(rdoAnual.Checked)
                {
                    AddToRestrictionList();
                }
                else if (rdoMonthly.Checked)
                {
                    Int32 NoOfMonths=0;
                    try
                    {
                        NoOfMonths = Convert.ToInt32(txtNoOfMonths.Text.Trim());
                    }
                    catch(Exception ex){
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please valid # of months!");
                        return;
                    }
                    DateTime orgFromDt= Convert.ToDateTime(txtFromDate.Text.Trim());
                    for (int i =0; i < NoOfMonths; i++ )
                    {
                        AddToRestrictionList();
                        txtFromDate.Text = (Convert.ToDateTime(txtFromDate.Text.Trim()).AddMonths(1)).ToString();
                    }
                    txtFromDate.Text = orgFromDt.ToShortDateString();
                }
                grvResctrict.DataSource = AccRestrList;
                grvResctrict.DataBind();
                grvProfCents.DataSource = null;
                grvProfCents.DataBind();
                clearAddScreen();

            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select Profit centers!");
                return;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            grvProfCents.DataSource = null;
            grvProfCents.DataBind();
        }

        protected void grvResctrict_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;//lblSEQno
            //  Int32 SEQ= Convert.ToInt32(grvResctrict.Rows[rowIndex].Cells[6].Text);
            // Label SEQ = (Label)e.Row.FindControl("chekPc");
            Label lblSEQ = (Label)(grvResctrict.Rows[rowIndex].FindControl("lblSEQno"));
            Int64 SEQ = Convert.ToInt64(lblSEQ.Text);
            AccRestrList.RemoveAll(x => x.Hrs_seq == SEQ);
            grvResctrict.DataSource = AccRestrList;
            grvResctrict.DataBind();
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void btnNone_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }
        }

        protected void btnClearSaveList_Click(object sender, EventArgs e)
        {
            AccRestrList.Clear();
            grvResctrict.DataSource = AccRestrList;
            grvResctrict.DataBind();
        }

        protected void clearAddScreen()
        {
            txtFromDate.Text = DateTime.Now.Date.ToShortDateString();
            txtToDate.Text = "";
            txtApprNoOfAcc.Text = "";
            txtSalesVal.Text = "";
            txtNoOfMonths.Text = "";

        }

        protected void ImgBtnAdd_pty_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearch2.Company;
            string chanel = uc_ProfitCenterSearch2.Channel;
            string subChanel = uc_ProfitCenterSearch2.SubChannel;
            string area = uc_ProfitCenterSearch2.Area;
            string region = uc_ProfitCenterSearch2.Region;
            string zone = uc_ProfitCenterSearch2.Zone;
            string pc = uc_ProfitCenterSearch2.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);           
            grvPC_pty.DataSource = dt;
            grvPC_pty.DataBind();

         
        }

       
        //********************************************************************************

        private void AddTo_ParaList()
        {            
            MasterMsgInfoUCtrl.Clear();
            string codeDescript = string.Empty;
            DataTable dt = CHNLSVC.Sales.Get_get_hpr_para_types(txtParaCode.Text.Trim().ToUpper());
            if (dt == null)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Code!");
                txtParaCode.Focus();
                return;
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    codeDescript = dt.Rows[0]["pt_desc"].ToString();
                    lblCodeDesc.Text = codeDescript;
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Code!");
                    txtParaCode.Focus();
                    return;
                }
            }
            if (Hpr_ParaList == null)
            {
                Hpr_ParaList = new List<Hpr_SysParameter>();
            }
            if (Show_Hpr_ParaList == null)
            {
                Show_Hpr_ParaList = new List<Hpr_SysParameter>();
            }
            foreach (GridViewRow gvr in grvPC_pty.Rows) 
            {
                //if (Hpr_ParaList == null)
                //{
                //    Hpr_ParaList = new List<Hpr_SysParameter>();
                //}

                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekPc_para");
                if (chkSelect.Checked)
                {
                    string profCenter = gvr.Cells[1].Text.Trim();

                    Hpr_SysParameter hpr_para = new Hpr_SysParameter();
                    hpr_para.Hsy_seq = -1;

                    hpr_para.Hsy_cd = txtParaCode.Text.Trim().ToUpper();
                    hpr_para.Hsy_cre_by = GlbUserName;
                    hpr_para.Hsy_cre_dt = DateTime.Now.Date;
                    hpr_para.Hsy_desc = codeDescript;                   
                    hpr_para.Hsy_pty_cd = profCenter;
                    hpr_para.Hsy_pty_tp = "PC";                                        
                    hpr_para.Hsy_val = Convert.ToDecimal(txtValue_pty.Text.Trim());
                    try
                    {
                        hpr_para.Hsy_from_dt = Convert.ToDateTime(txtFromDt_pty.Text.Trim());
                        hpr_para.Hsy_to_dt = Convert.ToDateTime(txtToDt_pty.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter valid dates!");
                        return;
                    }
                    //-------------------------------------------
                    var _duplicate = from _dup in Show_Hpr_ParaList
                                     where _dup.Hsy_cd == hpr_para.Hsy_cd && _dup.Hsy_pty_cd == profCenter
                                     select _dup;
                    if (_duplicate.Count() > 0)
                    {
                        //string Msg = "<script>alert('already added!');</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);                                               
                    }
                    else
                    {
                        Hpr_ParaList.Add(hpr_para);
                        Show_Hpr_ParaList.Add(hpr_para);
                    } //-------------------------------------------                   
                }

                grvParameters.DataSource = Show_Hpr_ParaList;
                grvParameters.DataBind();
            }
        }

        protected void btnSavePty_Click(object sender, EventArgs e)
        {
            
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, string.Empty, "HPPRM")==false)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                return;
            }
            
            //---------------------------------------------------------
            if (Hpr_ParaList.Count==0)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Add new definitions to the list!"); 
                return;
            }
            Int32 effect = 0;
            try {
                 effect = CHNLSVC.Sales.Save_hpr_sys_para(Hpr_ParaList);
            }
            catch(Exception EX){
                string Msg = "<script>alert('System error!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
            
            if (effect > 0)
            {
                clearScreen_para();
                string Msg = "<script>alert('Successfully saved!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully saved!");                
            }
            else
            {
                string Msg = "<script>alert('Failed to save. Try again!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to save!");
            }
        }
        protected void btnAddPara_Click(object sender, EventArgs e)
        {
            if (txtFromDt_pty.Text.Trim() == "" || txtToDt_pty.Text.Trim() == "" || txtValue_pty.Text.Trim() == "") 
            {
                return;
            }
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, string.Empty, "HPPRM") == false)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                return;
            }
            if (grvPC_pty.Rows.Count > 0)
            {
                    if (Show_Hpr_ParaList.Count < 1)
                    {
                        Show_Hpr_ParaList = CHNLSVC.Sales.GetAll_hpr_Para(txtParaCode.Text.Trim().ToUpper(), "PC", string.Empty);
                        grvParameters.DataSource = Show_Hpr_ParaList;
                        grvParameters.DataBind();
                    }
               
                AddTo_ParaList(); //Add new Parameter to PCs
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add Profit Centers!");
                return;
            }
            
        }

        protected void grvPC_pty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chekMand = (CheckBox)e.Row.FindControl("chekPc_para");
                if (!chekMand.Checked)
                {
                    chekMand.Checked = true;
                }
            }
        }

        protected void grvParameters_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label seq = (Label)e.Row.FindControl("lblParaSeq");
                if (seq.Text.Trim()== "-1")
                {
                    e.Row.BackColor = Color.Khaki;
                }
                grvParameters.Focus();

            }
        }

        protected void grvParameters_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            Label SEQ = (Label)grvParameters.Rows[rowIndex].FindControl("lblParaSeq");
             string para_CD = grvParameters.Rows[rowIndex].Cells[2].Text;
             string pc = grvParameters.Rows[rowIndex].Cells[4].Text;
             if (SEQ.Text == "-1")
             {
                 Show_Hpr_ParaList.RemoveAll(x => x.Hsy_cd == para_CD && x.Hsy_pty_cd == pc);
                 Hpr_ParaList.RemoveAll(x => x.Hsy_cd == para_CD && x.Hsy_pty_cd == pc);                 
             }
             else {
                 string Msg = "<script>alert('Cannot delete existing records in the database!');</script>";
                 ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
               
             }
             grvParameters.DataSource = Show_Hpr_ParaList;
             grvParameters.DataBind();
        }

        protected void btnAll_pty_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvPC_pty.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekPc_para");
                chkSelect.Checked = true;
            }            
        }

        protected void btnNone_pty_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvPC_pty.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekPc_para");
                chkSelect.Checked = false;
            }
        }

        protected void btnClear_pty_Click(object sender, EventArgs e)
        {           
            grvPC_pty.DataSource = null;
            grvPC_pty.DataBind();
        }

        protected void btnVeiwPara_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, string.Empty, "HPPRM") == false)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                return;
            }
            txtParaPc_PopUp.Text = string.Empty;
            txtParaCodePopUp.Text = string.Empty;
            grvVeiwParaPopUp.DataSource = null;
            grvVeiwParaPopUp.DataBind();
            ModalPopupVeiwPara.Show();
        }

        protected void btnHidn_Click(object sender, EventArgs e)
        {
            string codeDescript = string.Empty;
            string code= txtParaCode.Text.Trim().ToUpper();

            if (code == "" || code == string.Empty)
            {
                code = " ";
            }
            DataTable dt = CHNLSVC.Sales.Get_get_hpr_para_types(code);
            if (dt == null)
            {               
                lblCodeDesc.Text = string.Empty;
                return;
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    codeDescript = dt.Rows[0]["pt_desc"].ToString();
                    lblCodeDesc.Text = codeDescript;
                }
               
            }
        }

        protected void btnCloneParam_Click(object sender, EventArgs e)
        {
            //CHECK FOR PERMISSION
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, string.Empty, "HPPRM") == false)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                return;
            }
            txtCloneAddPc.Text = string.Empty;
            txtClonePC.Text = string.Empty;
            ClonePcList = new List<string>();
            grvClonePc.DataSource = ClonePcList;
            grvClonePc.DataBind();
            
            ModalPopupClonePara.Show();   
        }

        protected void ImgBtnCloneAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClonePcList.Add(txtCloneAddPc.Text.Trim().ToUpper());
            grvClonePc.DataSource = ClonePcList;
            grvClonePc.DataBind();

            ModalPopupClonePara.Show();
                     
        }

        protected void btnProcessClone_Click(object sender, EventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();
            if (ClonePcList.Count > 0)
            {
                Int32 eff = 0;
               
               eff = CHNLSVC.Sales.Clone_hpr_para_types(txtCloneParaCode.Text.Trim().ToUpper(), txtClonePC.Text.Trim().ToUpper(), ClonePcList, GlbUserName, DateTime.Now.Date);
                if (eff > 0)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cloning Compleated Successfully!");
                    string Msg = "<script>alert('Cloning Compleated Successfully!');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Sorry. Failed to compleate. Please try again!");
                }
            }
            else
            {
                string Msg = "<script>alert('Please add profit centers to the cloning list.');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
        }

        protected void btnCloneClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnVeiwPopUp_Click(object sender, EventArgs e)
        {
            string code = txtParaCodePopUp.Text.Trim().ToUpper();

            if (code == "" || code == string.Empty)
            {
                code = " ";
            }
            List<Hpr_SysParameter> veiw_list = new List<Hpr_SysParameter>();
            if (checkAllCodesPopUp.Checked == true)
            {
                veiw_list = CHNLSVC.Sales.GetAll_hpr_Para(string.Empty, "PC", txtParaPc_PopUp.Text.Trim().ToUpper());
            }
            else
            {
                veiw_list = CHNLSVC.Sales.GetAll_hpr_Para(code, "PC", txtParaPc_PopUp.Text.Trim().ToUpper());
            }
          

            grvVeiwParaPopUp.DataSource = veiw_list;
            grvVeiwParaPopUp.DataBind();
            
            ModalPopupVeiwPara.Show();
        }

        protected void btnClearPopUp_Click(object sender, EventArgs e)
        {
            txtParaPc_PopUp.Text = string.Empty;
            txtParaCodePopUp.Text = string.Empty;
            
            grvVeiwParaPopUp.DataSource = null;
            grvVeiwParaPopUp.DataBind();
            
            ModalPopupVeiwPara.Show();
        }
        #region Searchin
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.HpParaTp:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
             
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
        protected void ImgCodeSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpParaTp);
            DataTable dataSource1 = CHNLSVC.CommonSearch.Get_hp_parameterTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtParaCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void txtParaCode_TextChanged(object sender, EventArgs e)
        {                     
           
            txtFromDt_pty.Text = string.Empty;
            txtToDt_pty.Text = string.Empty;
            txtValue_pty.Text = string.Empty;

            Hpr_ParaList = new List<Hpr_SysParameter>();
            Show_Hpr_ParaList = new List<Hpr_SysParameter>();

                Show_Hpr_ParaList = CHNLSVC.Sales.GetAll_hpr_Para(txtParaCode.Text.Trim().ToUpper(), "PC", string.Empty);
                grvParameters.DataSource = Show_Hpr_ParaList;
                grvParameters.DataBind();
            
           
        }

        protected void btnVeiwAcRestr_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, string.Empty, "ACRES") == false)
            {//HP ACCOUNT RESTRICTION
                DataTable dt = CHNLSVC.General.Get_All_User_paramTypes("ACRES");
                string desc = string.Empty;
                if (dt!=null)
                {
                    if (dt.Rows.Count>0)
                    {
                        try {
                           desc= "(No Permission for '"+ dt.Rows[0]["seup_usr_permdesc"].ToString()+"')";
                        }
                        catch(Exception ex){
                            desc = "(No Permission for 'ACRES')";
                        }
                      
                    }
                }
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied! " + desc);
                return;
            }
            if(rdoVeiwMonthly.Checked)
            {
                List<HpAccRestriction> list=CHNLSVC.Sales.GetAll_SavedAccountRestrictons(txtVeiwPcAcRestr.Text.Trim().ToUpper(),1);
                grvVeiwAcRestr.DataSource = list;
                grvVeiwAcRestr.DataBind();
            }
            if(rdoVeiwAnnually.Checked)
            {
                List<HpAccRestriction> list = CHNLSVC.Sales.GetAll_SavedAccountRestrictons(txtVeiwPcAcRestr.Text.Trim().ToUpper(), 2);
                grvVeiwAcRestr.DataSource = list;
                grvVeiwAcRestr.DataBind();
            }
            ModalPopupVeiwAcRestr.Show();
        }

   
        protected void btnAccRestrVeiw_Click(object sender, EventArgs e)
        {
            txtVeiwPcAcRestr.Text = string.Empty;
            grvVeiwAcRestr.DataSource = null;
            grvVeiwAcRestr.DataBind();
            ModalPopupVeiwAcRestr.Show();
        }

        protected void ImgBtnCloneCode_Click(object sender, ImageClickEventArgs e)
        {           
            ModalPopupClonePara.BackgroundCssClass = "";           
        
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpParaTp);
            DataTable dataSource1 = CHNLSVC.CommonSearch.Get_hp_parameterTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCloneParaCode.ClientID;
                      
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

            ModalPopupClonePara.Show();
        }

        protected void ImgVeiwParamCd_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupVeiwPara.BackgroundCssClass = "";       

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpParaTp);
            DataTable dataSource1 = CHNLSVC.CommonSearch.Get_hp_parameterTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtParaCodePopUp.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

            ModalPopupVeiwPara.Show();
        }

        protected void btnModPopBlur1_Click(object sender, EventArgs e)
        {
            ModalPopupVeiwPara.Show();
            try 
            {              
                ModalPopupVeiwPara.BackgroundCssClass = "modalBackground";               
            }
            catch(Exception ex){}            

        }

        protected void btnModPopBlur2_Click(object sender, EventArgs e)
        {
            ModalPopupClonePara.Show();
            try
            {
                //ModalPopupClonePara.BackgroundCssClass = "modalBackground";   
                ModalPopupClonePara.BackgroundCssClass = "modalBackground";              
            }
            catch (Exception ex) { }
        }
          
        
    }
}