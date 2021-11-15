using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;

namespace FF.WebERPClient.General_Modules
{
    public partial class ProfitCenterDefinition : BasePage
    {
        public List<MasterProfitCenter> PcHieraList
        {
            get { return (List<MasterProfitCenter>)ViewState["PcHieraList"]; }
            set { ViewState["PcHieraList"] = value; }
        }
       
        public List<MasterReceiptDivision> recDivList
        {
            get { return (List<MasterReceiptDivision>)ViewState["recDivList"]; }
            set { ViewState["recDivList"] = value; }
        }
        public List<PaymentType> PaymentTxnList
        {
            get { return (List<PaymentType>)ViewState["PaymentTxnList"]; }
            set { ViewState["PaymentTxnList"] = value; }
        }
        //PaymentType
        protected void Page_Load(object sender, EventArgs e)
        {          
            if(!IsPostBack)
            {
                txtPcCode.Attributes.Add("onkeypress", "return fun1(event,'" + btnHidnGetPcDet.ClientID + "')");
                 List<MasterCompany> Companies =  CHNLSVC.General.GetALLMasterCompaniesData();
                 foreach (MasterCompany com in Companies)
                 {
                     ddlComPcHiera.Items.Add(new ListItem(com.Mc_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(10 - com.Mc_cd.Length))+"--"  + com.Mc_desc, com.Mc_cd));
                     ddlVeiwHieraCom.Items.Add(new ListItem(com.Mc_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(10 - com.Mc_cd.Length)) + "--" + com.Mc_desc, com.Mc_cd));
                    
                 }
               
                clearPcHieraTab();
                clearRecDevTab();
                clearPcChargesTab();
                List<PaymentTypeRef> PaymentType = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, string.Empty, string.Empty);
                if (PaymentType!=null)
                {
                    foreach (PaymentTypeRef pt in PaymentType)
                    {
                        ddlPayTypes.Items.Add(new ListItem(pt.Sapt_cd, pt.Sapt_cd));
                    }
                }

            }
        }
        private string AddHtmlSpaces(int length)
        {
            string space = "";
            for (int i = 0; i < length; i++)
            {
                space = space + " &nbsp;";
            }
            return space;
        }
        private void clearPcHieraTab()
        {
            //txtPcHieraCom.Text = string.Empty;
            txtPcHiera.Text = string.Empty;
            PcHieraList = new List<MasterProfitCenter>();
            DataTable DT = new DataTable();
            grvViewPcHiera.DataSource = DT;
            grvViewPcHiera.DataBind();
     
        }
        private void clearRecDevTab()
        {
            recDivList = new List<MasterReceiptDivision>();
            HiddenField_ComRecDiv.Value = string.Empty;
             DataTable DT = new DataTable();
             grvRec.DataSource = DT;
             grvRec.DataBind();
             grvPC_RcptCat.DataSource = DT;
             grvPC_RcptCat.DataBind();
        }
        private void clearPcChargesTab()
        {
            HiddenField_pcChg.Value = string.Empty; 
            DataTable DT = new DataTable();
            grvPc_Chg.DataSource = DT;
            grvPc_Chg.DataBind();

           txtChgEPF.Text= string.Empty;
           txtChgESD.Text=string.Empty;
           txtChgWHT.Text=string.Empty;

           txtChgFromDt.Text=string.Empty;
           txtChgToDt.Text = string.Empty;
           grv_PCchgs.DataSource = DT;
           grv_PCchgs.DataBind();
        }
        
        protected void btnCLEAR_cre_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/General_Modules/ProfitCenterDefinition.aspx");
        }

        protected void btnCLOSE_cre_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnSAVE_cre_Click(object sender, EventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();

            string _com = txtPc_Com.Text.Trim().ToUpper();
            string _pc = txtPcCode.Text.Trim().ToUpper();
            MasterProfitCenter Prof_center = CHNLSVC.General.GetPCByPCCode(string.Empty, _pc);
            if (Prof_center != null)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Profit Center code already exist!");
                return;
            }

            if (chekIsMultiDept.Checked && txtDefDepartment.Text.Trim()=="")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Default Department code!");
                txtDefDepartment.Focus();
                return;
            }

            MasterProfitCenter Prof = new MasterProfitCenter();
            try { 
            
                Prof.Mpc_act=checkActivePc.Checked;
                Prof.Mpc_add_1=txtAddress_1.Text.ToUpper();
                Prof.Mpc_add_2=txtAddress_2.Text.ToUpper();
                Prof.Mpc_add_hours=Convert.ToInt32(txtAddHours.Text.Trim());
                Prof.Mpc_cd=txtPcCode.Text.Trim().ToUpper();
                Prof.Mpc_check_cm=check_manCashMemo.Checked;
                Prof.Mpc_check_pay=check_pay.Checked;
                Prof.Mpc_chk_credit=check_credit.Checked;
                Prof.Mpc_chnl=txtPcChanel.Text.Trim().ToUpper();
                Prof.Mpc_com=txtPc_Com.Text.Trim().ToUpper();
                Prof.Mpc_def_customer=txtDefCustomer.Text.Trim().ToUpper();
                Prof.Mpc_def_dept=txtDefDepartment.Text.Trim().ToUpper();
                if (txtDefDiscountRt.Text.Trim()!="")
                { Prof.Mpc_def_dis_rate = Convert.ToDecimal(txtDefDiscountRt.Text.Trim()); }
                
                Prof.Mpc_def_exrate=txtDefDiscountRt.Text.Trim(); //TOUPPER?
                Prof.Mpc_def_loc=txtDefLoc.Text.Trim().ToUpper();
                Prof.Mpc_def_pb= txtDefPB.Text.Trim().ToUpper();
                Prof.Mpc_desc=txtPcDescript.Text.Trim().ToUpper();
                Prof.Mpc_edit_price=checkEditPrice.Checked;
                if (txtAllowRtForEdit.Text.Trim() != "")
                { Prof.Mpc_edit_rate = Convert.ToDecimal(txtAllowRtForEdit.Text.Trim()); }
               
                Prof.Mpc_fax= txtPcFax.Text.Trim();
                Prof.Mpc_inter_com=checkInterCom.Checked;
                Prof.Mpc_man=txtManagerCd.Text.Trim(); //TOUPPER?
                Prof.Mpc_multi_dept= chekIsMultiDept.Checked;
                Prof.Mpc_ope_cd= txtOPEcd.Text.Trim(); //TOUPPER?
                Prof.Mpc_order_restric= checkOrderRestr.Checked;
                if (txtOrderValidPeriod.Text.Trim() != "")
                { Prof.Mpc_order_valid_pd = Convert.ToInt32(txtOrderValidPeriod.Text.Trim()); }
                
                Prof.Mpc_oth_ref= txtOherRef.Text.Trim();  //TOUPPER?
                Prof.Mpc_print_dis= checkPrintDisc.Checked;
                Prof.Mpc_print_payment= checkPrintPaymnt.Checked;
                Prof.Mpc_print_wara_remarks= checkPrintWarrRemk.Checked;
                Prof.Mpc_so_sms= checkSOsms.Checked;
                Prof.Mpc_tel= txtPcTele.Text.Trim();
                Prof.Mpc_tp= ddlPcType.SelectedValue;
                if (txtExtWarrPeriod.Text.Trim() != "")
                { Prof.Mpc_wara_extend = Convert.ToInt32(txtExtWarrPeriod.Text.Trim()); }
                
                Prof.Mpc_without_price = checkEnterPriceMan.Checked;
            }
            catch(Exception EX){
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid data has been entered. Please double check!");
                return;
            }
            try{
                 Int32 eff = CHNLSVC.General.Save_profit_center(Prof);
                 if (eff > 0)
                 {
                     string Msg = "<script>alert('Successfully Created!');</script>";
                     ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                 }
                 else
                 {
                     this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Profit Center not Created!");
                     return;
                 }
            }
            catch(Exception ex){
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured.Please double check entered values and re-try!");
                return;
            }           
        }

        private void Load_PC_Details(string _com,string _code)
        {
            //TODO:Load PC
            MasterProfitCenter Prof = CHNLSVC.General.GetPCByPCCode(_com, _code);
            if (Prof==null)
            {
                return;
            }
            checkActivePc.Checked =Prof.Mpc_act ;
            txtAddress_1.Text = Prof.Mpc_add_1;
            txtAddress_2.Text = Prof.Mpc_add_2;
            txtAddHours.Text = Prof.Mpc_add_hours.ToString();
            txtPcCode.Text = Prof.Mpc_cd;
            check_manCashMemo.Checked = Prof.Mpc_check_cm;
            check_pay.Checked = Prof.Mpc_check_pay;
            check_credit.Checked = Prof.Mpc_chk_credit;
            txtPcChanel.Text = Prof.Mpc_chnl;
            txtPc_Com.Text = Prof.Mpc_com;
            txtDefCustomer.Text = Prof.Mpc_def_customer;
            txtDefDepartment.Text = Prof.Mpc_def_dept;
            txtDefDiscountRt.Text = Prof.Mpc_def_dis_rate.ToString();

            txtDefDiscountRt.Text = Prof.Mpc_def_exrate;
            txtDefLoc.Text = Prof.Mpc_def_loc;
            txtDefPB.Text = Prof.Mpc_def_pb;
            txtPcDescript.Text = Prof.Mpc_desc;
            checkEditPrice.Checked = Prof.Mpc_edit_price;
            txtAllowRtForEdit.Text = Prof.Mpc_edit_rate.ToString();
            txtPcFax.Text = Prof.Mpc_fax;
            checkInterCom.Checked = Prof.Mpc_inter_com;
            txtManagerCd.Text = Prof.Mpc_man;
            chekIsMultiDept.Checked = Prof.Mpc_multi_dept;
            txtOPEcd.Text = Prof.Mpc_ope_cd;

            checkOrderRestr.Checked = Prof.Mpc_order_restric;
            txtOrderValidPeriod.Text = Prof.Mpc_order_valid_pd.ToString();
            txtOherRef.Text = Prof.Mpc_oth_ref;
            checkPrintDisc.Checked = Prof.Mpc_print_dis;
            checkPrintPaymnt.Checked = Prof.Mpc_print_payment;
            checkPrintWarrRemk.Checked = Prof.Mpc_print_wara_remarks;
            checkSOsms.Checked = Prof.Mpc_so_sms;
            txtPcTele.Text = Prof.Mpc_tel;

            txtExtWarrPeriod.Text = Prof.Mpc_wara_extend.ToString();
            checkEnterPriceMan.Checked = Prof.Mpc_without_price;
            try {
                ddlPcType.SelectedValue = Prof.Mpc_tp;
            }
            catch(Exception ex){
                return;
            }
            
           
        }

        protected void btnHidnGetPcDet_Click(object sender, EventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();
            string com = txtPc_Com.Text.Trim().ToUpper();
            string pc= txtPcCode.Text.Trim().ToUpper();
            if (com=="")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter company of the profit center!");
                txtPc_Com.Focus();
                return;
            }
            if (pc == "")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter profit center code!");
                txtPcCode.Focus();
                return;
            }
            Load_PC_Details(com,pc);
        }

        protected void btnUPDATE_cre_Click(object sender, EventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();
           
            string _com = txtPc_Com.Text.Trim().ToUpper();
            string _pc = txtPcCode.Text.Trim().ToUpper();
            MasterProfitCenter Prof_center = CHNLSVC.General.GetPCByPCCode(_com, _pc);
            if (Prof_center == null)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Profit Center code!");
                return;
            }
            if (chekIsMultiDept.Checked && txtDefDepartment.Text.Trim() == "")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Default Department code!");
                txtDefDepartment.Focus();
                return;
            }
            MasterProfitCenter Prof = new MasterProfitCenter();
            try {
                Prof.Mpc_act = checkActivePc.Checked;
                Prof.Mpc_add_1 = txtAddress_1.Text.ToUpper();
                Prof.Mpc_add_2 = txtAddress_2.Text.ToUpper();
                Prof.Mpc_add_hours = Convert.ToInt32(txtAddHours.Text.Trim());
                Prof.Mpc_cd = txtPcCode.Text.Trim().ToUpper();
                Prof.Mpc_check_cm = check_manCashMemo.Checked;
                Prof.Mpc_check_pay = check_pay.Checked;
                Prof.Mpc_chk_credit = check_credit.Checked;
                Prof.Mpc_chnl = txtPcChanel.Text.Trim().ToUpper();
                Prof.Mpc_com = txtPc_Com.Text.Trim().ToUpper();
                Prof.Mpc_def_customer = txtDefCustomer.Text.Trim().ToUpper();
                Prof.Mpc_def_dept = txtDefDepartment.Text.Trim().ToUpper();
                if (txtDefDiscountRt.Text.Trim() != "")
                { Prof.Mpc_def_dis_rate = Convert.ToDecimal(txtDefDiscountRt.Text.Trim()); }

                Prof.Mpc_def_exrate = txtDefDiscountRt.Text.Trim(); //TOUPPER?
                Prof.Mpc_def_loc = txtDefLoc.Text.Trim().ToUpper();
                Prof.Mpc_def_pb = txtDefPB.Text.Trim().ToUpper();
                Prof.Mpc_desc = txtPcDescript.Text.Trim().ToUpper();
                Prof.Mpc_edit_price = checkEditPrice.Checked;
                if (txtAllowRtForEdit.Text.Trim() != "")
                { Prof.Mpc_edit_rate = Convert.ToDecimal(txtAllowRtForEdit.Text.Trim()); }

                Prof.Mpc_fax = txtPcFax.Text.Trim();
                Prof.Mpc_inter_com = checkInterCom.Checked;
                Prof.Mpc_man = txtManagerCd.Text.Trim(); //TOUPPER?
                Prof.Mpc_multi_dept = chekIsMultiDept.Checked;
                Prof.Mpc_ope_cd = txtOPEcd.Text.Trim(); //TOUPPER?
                Prof.Mpc_order_restric = checkOrderRestr.Checked;
                if (txtOrderValidPeriod.Text.Trim() != "")
                { Prof.Mpc_order_valid_pd = Convert.ToInt32(txtOrderValidPeriod.Text.Trim()); }

                Prof.Mpc_oth_ref = txtOherRef.Text.Trim();  //TOUPPER?
                Prof.Mpc_print_dis = checkPrintDisc.Checked;
                Prof.Mpc_print_payment = checkPrintPaymnt.Checked;
                Prof.Mpc_print_wara_remarks = checkPrintWarrRemk.Checked;
                Prof.Mpc_so_sms = checkSOsms.Checked;
                Prof.Mpc_tel = txtPcTele.Text.Trim();
                Prof.Mpc_tp = ddlPcType.SelectedValue;
                if (txtExtWarrPeriod.Text.Trim() != "")
                { Prof.Mpc_wara_extend = Convert.ToInt32(txtExtWarrPeriod.Text.Trim()); }

                Prof.Mpc_without_price = checkEnterPriceMan.Checked;
            }
            catch(Exception ex){
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid data has been entered. Please double check!");
                return;
            }          
            //----------------------------------------------------------------------------------
            try
            {
                Int32 eff = CHNLSVC.General.Update_profit_center(Prof);
                if (eff > 0)
                {
                    string Msg = "<script>alert('Successfully Updated!');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Profit Center Details Not Updated!");
                    return;
                }
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured.Please double check entered values and re-try!");
                return;
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

                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtTxnMainCat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;

                    }
                //case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                //    {

                //        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(GlbUserComCode + seperator + txtTxnPB.Text.Trim() + seperator);
                        // paramsText.Append(GlbUserComCode + seperator + "%" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        //paramsText.Append(txtCircular.Text.Trim().ToUpper() + seperator + "Promotion" + seperator);
                        paramsText.Append(string.Empty + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
        protected void ImgBtnSearchCom_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable dataSource1 = CHNLSVC.CommonSearch.GetCompanySearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPc_Com.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }


        ///////////////////////TAB PANEL 2///////////////////////////////////////////////////////////
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
            //foreach (GridViewRow gvr in grvProfCents.Rows)
            //{
            //    CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
            //    chkSelect.Checked = false;
            //}
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            PcHieraList.Clear();
            grvProfCents.DataSource = PcHieraList;
            grvProfCents.DataBind();
        }
        protected void grvProfCents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    CheckBox chekMand = (CheckBox)e.Row.FindControl("chekPc");
            //    if (!chekMand.Checked)
            //    {
            //        chekMand.Checked = true;
            //    }
            //}
        }
        protected void btnAddPcHiera_Click(object sender, EventArgs e)
        {
            //TODO: VALIDATE PC
            MasterProfitCenter PC = new MasterProfitCenter();
            PC.Mpc_com = ddlComPcHiera.SelectedValue;
            PC.Mpc_cd = txtPcHiera.Text.Trim().ToUpper();
            PcHieraList.Add(PC);
            grvProfCents.DataSource = PcHieraList;
            grvProfCents.DataBind();
        }

        protected void grvProfCents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            string PC_code = grvProfCents.Rows[rowIndex].Cells[1].Text;
            
            PcHieraList.RemoveAll(x => x.Mpc_com == ddlComPcHiera.SelectedValue && x.Mpc_cd == PC_code);
            grvProfCents.DataSource = PcHieraList;
            grvProfCents.DataBind();
           
        }

        protected void btnSaveHiera_Click(object sender, EventArgs e)
        {
            List<MasterSalesPriorityHierarchy> _pcInfoHeaders = new List<MasterSalesPriorityHierarchy>();
            foreach (MasterProfitCenter pc in PcHieraList)
            {
                MasterSalesPriorityHierarchy info = new MasterSalesPriorityHierarchy();
                info.Mpi_act = true;                
                info.Mpi_pc_cd = pc.Mpc_cd;
                info.Mpi_com_cd = pc.Mpc_com;
                info.Mpi_tp="PC_PRIT_HIERARCHY";
                //info.Mpi_val=;
                //info.Mpi_cd= 
                _pcInfoHeaders.Add(info);
            }

            Dictionary<string, string> code_and_value = new Dictionary<string, string>();            
            code_and_value.Add("ZONE", txtHieraZONE.Text.Trim().ToUpper());
            code_and_value.Add("REGION", txtHieraREGION.Text.Trim().ToUpper());
            code_and_value.Add("AREA", txtHieraAREA.Text.Trim().ToUpper());
            code_and_value.Add("SCHNL", txtHieraSUBCHANEL.Text.Trim().ToUpper());            
            code_and_value.Add("CHNL", txtHieraCHANEL.Text.Trim().ToUpper());
            code_and_value.Add("COM", ddlComPcHiera.SelectedValue);
            code_and_value.Add("GPC", txtHieraCPG.Text.Trim().ToUpper());

            Int32 eff = CHNLSVC.General.Save_MST_PC_INFO(_pcInfoHeaders, code_and_value);
            if (eff > 0)
            {
                string Msg = "<script>alert('Successfully Saved!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Saved. Try again!");
                return;
            }
            //Save_MST_PC_INFO
        }

        protected void btnViewHiera_Click(object sender, EventArgs e)
        {
            List<MasterSalesPriorityHierarchy> AllPc_info= CHNLSVC.General.Get_AllPc_info(ddlVeiwHieraCom.SelectedValue, txtViewHieraPc.Text.Trim().ToUpper(),string.Empty,string.Empty);
            grvViewPcHiera.DataSource = AllPc_info;
            grvViewPcHiera.DataBind();
        }

        protected void ImgBtnLoadHiera_Click(object sender, ImageClickEventArgs e)
        {
            List<MasterSalesPriorityHierarchy> AllPc_info = CHNLSVC.General.Get_AllPc_info(ddlUpdtPcHiera.SelectedValue, txtUpdtPcHiera.Text.Trim().ToUpper(), string.Empty, "PC_PRIT_HIERARCHY");
            if (AllPc_info!=null)
            {
                foreach (MasterSalesPriorityHierarchy info in AllPc_info)
                {
                    switch (info.Mpi_cd)
                    {
                        case "ZONE":
                            {
                                txtUpdteZONE.Text = info.Mpi_val;
                                break;
                            }
                        case "REGION":
                            {
                                txtUpdteREGION.Text = info.Mpi_val; 
                                break;
                            }
                        case "AREA":
                            {
                                txtUpdteAREA.Text = info.Mpi_val; 
                                break; 
                            }
                        case "SCHNL":
                            {
                                txtUpdteSUBCHNL.Text = info.Mpi_val; 
                                break;
                            }
                        case "CHNL":
                            {
                                txtUpdteCHNL.Text = info.Mpi_val; 
                                break;
                            }
                        case "GPC":
                            {
                                txtUpdteCGP.Text = info.Mpi_val; 
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }

        protected void btnUpdateHiera_Click(object sender, EventArgs e)
        {
            List<MasterSalesPriorityHierarchy> _pcInfoHeaders = new List<MasterSalesPriorityHierarchy>();
           
            MasterSalesPriorityHierarchy info = new MasterSalesPriorityHierarchy();
            info.Mpi_act = true;
            info.Mpi_pc_cd = txtUpdtPcHiera.Text.Trim().ToUpper();
            info.Mpi_com_cd = ddlUpdtPcHiera.SelectedValue;
            info.Mpi_tp = "PC_PRIT_HIERARCHY";
            //info.Mpi_val=;
            //info.Mpi_cd= 
            _pcInfoHeaders.Add(info);
           

            Dictionary<string, string> code_and_value = new Dictionary<string, string>();
            code_and_value.Add("ZONE", txtUpdteZONE.Text.Trim().ToUpper()); 
            code_and_value.Add("REGION", txtUpdteREGION.Text.Trim().ToUpper());
            code_and_value.Add("AREA", txtUpdteAREA.Text.Trim().ToUpper());
            code_and_value.Add("SCHNL", txtUpdteSUBCHNL.Text.Trim().ToUpper());
            code_and_value.Add("CHNL", txtUpdteCHNL.Text.Trim().ToUpper());
            code_and_value.Add("COM", ddlUpdtPcHiera.SelectedValue);
            code_and_value.Add("GPC", txtUpdteCGP.Text.Trim().ToUpper());

            Int32 eff = CHNLSVC.General.Update_MST_PC_INFO(info, code_and_value);
            if (eff > 0)
            {
                string Msg = "<script>alert('Successfully Updated!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Updated. Try again!");
                return;
            }
        }

        ////////////////////////////////////TAB PANEL 03/////////////////////////////////////////////////////////

        protected void grvPC_RcptCat_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void btnAll_pty_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvPC_RcptCat.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekPc_para");
                chkSelect.Checked = true;
            }
        }

        protected void btnNone_pty_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvPC_RcptCat.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekPc_para");
                chkSelect.Checked = false;
            }
        }

        protected void btnClear_pty_Click(object sender, EventArgs e)
        {
            grvPC_RcptCat.DataSource = null;
            grvPC_RcptCat.DataBind();
        }

        protected void ImgBtnAddPcRcpt_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearch1.Company;
            string chanel = uc_ProfitCenterSearch1.Channel;
            string subChanel = uc_ProfitCenterSearch1.SubChannel;
            string area = uc_ProfitCenterSearch1.Area;
            string region = uc_ProfitCenterSearch1.Region;
            string zone = uc_ProfitCenterSearch1.Zone;
            string pc = uc_ProfitCenterSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            grvPC_RcptCat.DataSource = dt;
            grvPC_RcptCat.DataBind();

            HiddenField_ComRecDiv.Value = com;
        }

        protected void btnAddRec_Click(object sender, EventArgs e)
        {
            MasterReceiptDivision recDiv = new MasterReceiptDivision();
            recDiv.Msrd_cd= txtRecCode.Text.Trim().ToUpper();
            recDiv.Msrd_com = HiddenField_ComRecDiv.Value;
            recDiv.Msrd_cre_by=GlbUserName;;
            recDiv.Msrd_cre_dt=DateTime.Now.Date;
            recDiv.Msrd_desc= txtRecDesc.Text.Trim().ToUpper();
            recDiv.Msrd_div_tp="INTERNAL";
            recDiv.Msrd_inv_tp="CRED2";
            recDiv.Msrd_is_def=checkRecDefault.Checked;
            recDiv.Msrd_is_sales=true;
            recDiv.Msrd_is_ser=false;
            recDiv.Msrd_mod_by=GlbUserName;
            recDiv.Msrd_mod_dt=DateTime.Now.Date;;
           // recDiv.Msrd_pc=;
            recDiv.Msrd_stus=true;
            recDivList.Add(recDiv);

            grvRec.DataSource = recDivList;
            grvRec.DataBind();
            checkRecDefault.Checked = false;
            txtRecCode.Text = string.Empty;
            txtRecDesc.Text = string.Empty;
        }

        protected void btnRecSave_Click(object sender, EventArgs e)
        {
            List<string> pcList = new List<string>();
            foreach (GridViewRow gvr in grvPC_RcptCat.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chekPc_para");
                if (chkSelect.Checked)
                {
                    string profCenter = gvr.Cells[1].Text.Trim();
                    pcList.Add(profCenter);
                }
            }

            if (pcList.Count<1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select profit center(s)!");
                return;
            }
            if (recDivList.Count<1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add code(s)!");
                return;
            }

            Int32 eff = CHNLSVC.General.Save_MST_REC_DIV(recDivList, pcList);
            if (eff > 0)
            {
                clearRecDevTab();
                string Msg = "<script>alert('Successfully Saved!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Saved. Try again!");
                return;
            }
        }

        protected void grvPc_Chg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chekMand = (CheckBox)e.Row.FindControl("chekPcChg");
                if (!chekMand.Checked)
                {
                    chekMand.Checked = true;
                }
            }
        }

        protected void btnAllChg_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvPc_Chg.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekPcChg");
                chkSelect.Checked = true;
            }
        }

        protected void btnNoneChg_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvPc_Chg.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekPcChg");
                chkSelect.Checked = false;
            }
        }

        protected void btnClearChg_Click(object sender, EventArgs e)
        {
            grvPc_Chg.DataSource = null;
            grvPc_Chg.DataBind();
        }

        protected void ImgBtnPcChgAdd_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearch2.Company;
            string chanel = uc_ProfitCenterSearch2.Channel;
            string subChanel = uc_ProfitCenterSearch2.SubChannel;
            string area = uc_ProfitCenterSearch2.Area;
            string region = uc_ProfitCenterSearch2.Region;
            string zone = uc_ProfitCenterSearch2.Zone;
            string pc = uc_ProfitCenterSearch2.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            grvPc_Chg.DataSource = dt;
            grvPc_Chg.DataBind();

            HiddenField_pcChg.Value = com;
        }

        protected void btnSavePcChg_Click(object sender, EventArgs e)
        {
            Decimal EPF = 0;
            Decimal ESD = 0;
            Decimal WHT = 0;
            try {
                EPF = Convert.ToDecimal(txtChgEPF.Text.Trim());
                ESD = Convert.ToDecimal(txtChgESD.Text.Trim());
                WHT = Convert.ToDecimal(txtChgWHT.Text.Trim());
            }
            catch(Exception EX){
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Rate(s)!");
                return;
            }
            if (EPF > 100 || ESD > 100 || WHT>100)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Rates cannot be grater than 100!");
                return;
            }
            DateTime fromDt;
            DateTime toDt;
            try
            {
                fromDt = Convert.ToDateTime (txtChgFromDt.Text.Trim());
                toDt = Convert.ToDateTime(txtChgToDt.Text.Trim());
                if (toDt<fromDt)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "'To Date' should be greater than 'From Date'!");
                    return;
                }
              
            }
            catch (Exception EX)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Date(s)!");
                return;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<string> pcList = new List<string>();
            foreach (GridViewRow gvr in grvPc_Chg.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chekPcChg");
                if (chkSelect.Checked)
                {
                    string profCenter = gvr.Cells[1].Text.Trim();
                    pcList.Add(profCenter);
                }
            }

            if (pcList.Count < 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select profit center(s)!");
                return;
            }
            //Int32 eff= CHNLSVC.General.Save_MST_PC_CHG(
            //HiddenField_pcChg
            //MasterProfitCenterCharges 
            List<MasterProfitCenterCharges> pc_chg_List = new List<MasterProfitCenterCharges>();
            foreach (string pc in pcList)
            {
                MasterProfitCenterCharges chg = new MasterProfitCenterCharges();
                chg.Mpch_com = HiddenField_pcChg.Value;
                chg.Mpch_cre_by = GlbUserName;
                chg.Mpch_cre_dt = DateTime.Now.Date;
                chg.Mpch_epf = EPF;
                chg.Mpch_esd = ESD;
                chg.Mpch_from_dt = fromDt;
                chg.Mpch_pc = pc;
                chg.Mpch_to_dt = toDt;
                chg.Mpch_wht = WHT;
                //chg.Mpch_seq
                pc_chg_List.Add(chg);
            }

            Int32 eff = CHNLSVC.General.Save_MST_PC_CHG(pc_chg_List);
            if (eff > 0)
            {
                clearPcChargesTab();
                string Msg = "<script>alert('Successfully Saved!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Saved. Try again!");
                return;
            }
        }

        protected void btnAllTxn_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvPc_Txn.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekPcTxn");
                chkSelect.Checked = true;
            }
        }

        protected void btnNoneTxn_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvPc_Txn.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekPcTxn");
                chkSelect.Checked = false;
            }
        }

        protected void btnClearTxn_Click(object sender, EventArgs e)
        {
            grvPc_Txn.DataSource = null;
            grvPc_Txn.DataBind();
        }

        protected void ImgBtnAddTxn_Click(object sender, ImageClickEventArgs e)
        {
            HiddenField_TxnSeq.Value = (Convert.ToInt32(HiddenField_TxnSeq.Value)+1).ToString();
            //PaymentTxnList
            PaymentType txn = new PaymentType();
            txn.Stp_act = true;
            txn.Stp_bank = txtTxnBank.Text.Trim();
            txn.Stp_bank_chg_rt = Convert.ToDecimal(txtTxnBnkCgRt.Text.Trim());
            txn.Stp_bank_chg_val = Convert.ToDecimal(txtTxnBnkCgVal.Text.Trim());
            txn.Stp_brd = txtTxnBrand.Text.Trim().ToUpper();
            txn.Stp_cat = txtTxnMainCat.Text.Trim().ToUpper();
            txn.Stp_cre_by = GlbUserName;
            txn.Stp_cre_dt = DateTime.Now.Date;
            txn.Stp_def = checkDefTxn.Checked;
            txn.Stp_from_dt=Convert.ToDateTime(txtTxnFrom.Text.Trim());
            txn.Stp_itm = txtTxnItemCd.Text.Trim().ToUpper();
           
            txn.Stp_main_cat = txtTxnMainCat.Text.Trim().ToUpper();
            txn.Stp_pay_tp = ddlPayTypes.SelectedValue;//txtTxnPayType.Text.Trim();
            txn.Stp_pb = txtTxnPB.Text.Trim();
            txn.Stp_pb_lvl = txtTxnPBL.Text.Trim();
            txn.Stp_pd = Convert.ToInt32(txtTxnPD.Text.Trim());
            txn.Stp_pro = txtTxnPromo.Text.Trim();
          
            txn.Stp_ser = txtTxnSerialNo.Text.Trim();
            txn.Stp_to_dt=Convert.ToDateTime(txtTxnTo.Text.Trim());
            txn.Stp_txn_tp = txtTxnType.Text;
            //txn.Stp_loc=
            txn.Stp_seq = Convert.ToInt32(HiddenField_TxnSeq.Value);//for delete purpose
            if (PaymentTxnList==null)
            {
                PaymentTxnList = new List<PaymentType>();
            }
            PaymentTxnList.Add(txn);
            grvTxnList.DataSource = PaymentTxnList;
            grvTxnList.DataBind();

            checkDefTxn.Checked=false;
        }

        protected void grvTxnList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;//lblSEQno
            //  Int32 SEQ= Convert.ToInt32(grvResctrict.Rows[rowIndex].Cells[6].Text);
            // Label SEQ = (Label)e.Row.FindControl("chekPc");
            Label lblSEQ = (Label)(grvTxnList.Rows[rowIndex].FindControl("lblTxnSeq"));
            Int64 SEQ = Convert.ToInt64(lblSEQ.Text);
            PaymentTxnList.RemoveAll(x => x.Stp_seq == SEQ);
            grvTxnList.DataSource = PaymentTxnList;
            grvTxnList.DataBind();
            
        }

        protected void btnSaveTxnTp_Click(object sender, EventArgs e)
        {
             List<string> pcList = new List<string>();
            foreach (GridViewRow gvr in grvPc_Txn.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chekPcTxn");
                if (chkSelect.Checked)
                {
                    string profCenter = gvr.Cells[1].Text.Trim();
                    pcList.Add(profCenter);
                }
            }
           
            if (pcList.Count < 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select profit center(s)!");
                return;
            }
            
            Int32 eff= CHNLSVC.General.Save_SAR_TXN_PAY_TP(PaymentTxnList, pcList);
                      
            if (eff > 0)
            {
                clearRecDevTab();
                string Msg = "<script>alert('Successfully Saved!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Saved. Try again!");
                return;
            }

        }

        protected void ImgBtnAddPc_Txn_Click(object sender, ImageClickEventArgs e)
        {

            string com = uc_ProfitCenterSearch3.Company;
            string chanel = uc_ProfitCenterSearch3.Channel;
            string subChanel = uc_ProfitCenterSearch3.SubChannel;
            string area = uc_ProfitCenterSearch3.Area;
            string region = uc_ProfitCenterSearch3.Region;
            string zone = uc_ProfitCenterSearch3.Zone;
            string pc = uc_ProfitCenterSearch3.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            grvPc_Txn.DataSource = dt;
            grvPc_Txn.DataBind();

            //HiddenField_pcChg.Value = com;
        }

        protected void imgBtnHieraPc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPcHiera.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnPCHieraDisp_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtViewHieraPc.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnImgHieraUpdatePc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtUpdtPcHiera.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTxnItemCd.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnMainCat_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTxnMainCat.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnSubCat_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTxnSubCat.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnPB_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(MasterCommonSearchUCtrl.SearchParams, null, null);

            //MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
            //DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTxnPB.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnPbLevel_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelByBookData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTxnPBL.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnBankCd_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTxnBank.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnBrand_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTxnBrand.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnPromo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
            DataTable dataSource1 = CHNLSVC.CommonSearch.GetPromotionSearch(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTxnPromo.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnViewLatestChg_Click(object sender, EventArgs e)
        {
            grv_PCchgs.DataSource= CHNLSVC.General.Get_latest_PcCharges(string.Empty, string.Empty);
            grv_PCchgs.DataBind();
        }
                
       
    }
}