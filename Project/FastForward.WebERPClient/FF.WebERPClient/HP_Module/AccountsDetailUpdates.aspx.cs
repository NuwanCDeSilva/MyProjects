using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;

namespace FF.WebERPClient.HP_Module
{
    public partial class AccountsDetailUpdates : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uc_CustomerCreation1.UserControlButtonClicked += new
                  EventHandler(txtHiddenCustCD_TextChanged);

            //lost forcust fire
            txtCustCode.Attributes.Add("onblur", "return onblurFire(event,'" + btnGetCust.ClientID + "')");
            //F2 press
            txtCustCode.Attributes.Add("onKeyup", "return clickButton(event,'" + ImgBtnCust.ClientID + "')");
            txtAccNo.Attributes.Add("onKeyup", "return clickButton(event,'" + ImgBtnAcc.ClientID + "')");
           
            //enter key function          
            txtCustCode.Attributes.Add("onkeydown", "return fun1(event,'" + btnGetCust.ClientID + "')");
            txtAccNo.Attributes.Add("onkeydown", "return clickButton(event,'" + btnAccOk_2.ClientID + "')");
            txtAccNo.Attributes.Add("onkeydown", "return clickButton(event,'" + btnAccOk.ClientID + "')");
            

            if(!IsPostBack)
            {
                hideDives();
                txtCurrentDt.Text = DateTime.Now.ToShortDateString();
                txtFrmDt.Text = txtCurrentDt.Text;
                txtToDt.Text = txtCurrentDt.Text;
                DataTable DT = new DataTable();
                grvAccounts.DataSource = DT;
                grvAccounts.DataBind();

                DataTable datasource1 = CHNLSVC.Sales.GetHp_flag_bank_onType("BANK");//CHNLSVC.General.GetSalesTypes("", null, null);
                foreach (DataRow dr in datasource1.Rows)
                {

                    ddlMortgageCd.Items.Add(new ListItem(dr["code"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(10 - dr["code"].ToString().Length)) + "-" + dr["description"].ToString(), dr["code"].ToString()));
                }


                DataTable datasource2 = CHNLSVC.Sales.GetHp_flag_bank_onType("FLAG");
                foreach (DataRow dr in datasource2.Rows)
                {

                    ddlCategoCd.Items.Add(new ListItem(dr["code"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(10 - dr["code"].ToString().Length)) + "-" + dr["description"].ToString(), dr["code"].ToString()));
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
        private void hideDives()
        {
            divCurDate.Visible = false;
            divMortgage.Visible = false;
            divCategorize.Visible = false;
            divAccTrans.Visible = false;
            divCustTrans.Visible = false;
            divCustDetChange.Visible = false;

            //------------
            //divPhn.Visible = false;
            //divProvinceCd.Visible = false;
            //divCountryCd.Visible = false;
            //divPostalcd.Visible = false;
            //------------
        }
       
        protected void rdoMortgage_CheckedChanged(object sender, EventArgs e)
        {
            hideDives();
            divMortgage.Visible = true;

            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP1"))
            {
                btnConfMorg.Enabled = true;
            }
            else
            {
                btnConfMorg.Enabled = false;
            }

        }

        protected void rdoCategorize_CheckedChanged(object sender, EventArgs e)
        {
            hideDives();
            divCategorize.Visible = true;
            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP2"))
            {
                
                btnConfCatego.Enabled = true;
            }
            else
            {
                btnConfCatego.Enabled = false;
            }
        }

        protected void rdoAccTransfer_CheckedChanged(object sender, EventArgs e)
        {
            hideDives();
            divAccTrans.Visible = true;

            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP3"))
            {
                btnConfAccTr.Enabled = true;
            }
            else
            {
                btnConfAccTr.Enabled = false;
            }
        }

        protected void rdoCustTransfer_CheckedChanged(object sender, EventArgs e)
        {
            hideDives();
            divCustTrans.Visible = true;
            txtCustCode.Text = string.Empty;
            divCustDetClose.Visible = false;

            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP4"))
            {
                btnConfCustTr.Enabled = true;
            }
            else
            {
                btnConfCustTr.Enabled = false;
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                //UserProfitCenter              
                default:
                    break;
            }

            return paramsText.ToString();
        }
        
        protected void ImgBtnPC_Click1(object sender, ImageClickEventArgs e)
        {
            //MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            //DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            //MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            //MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //MasterCommonSearchUCtrl.ReturnResultControl = txtPC.ClientID;
            //MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

            /////////////////////////////
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPC.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

            /////////////////////////////

        }

        protected void ImgBtnAcc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetHpAccountSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtAccNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnAccOk_Click(object sender, EventArgs e)
        {
            
            DateTime currDate=Convert.ToDateTime(txtCurrentDt.Text.Trim());
            DateTime fromDt;
            DateTime toDt;
            try
            {
               fromDt = Convert.ToDateTime(txtFrmDt.Text.Trim());
               toDt = Convert.ToDateTime(txtToDt.Text.Trim()); 
            }
            catch(Exception ex){
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid dates!");
                return;
            }
            DataTable dt = new DataTable();
            dt = CHNLSVC.Sales.GetHp_ActiveAccounts(GlbUserComCode, txtPC.Text.Trim().ToUpper(),currDate,fromDt,toDt,null,null);
            grvAccounts.DataSource = dt;
            grvAccounts.DataBind();
        }

        protected void btnAccOk_2_Click(object sender, EventArgs e)
        {
            DateTime currDate = Convert.ToDateTime(txtCurrentDt.Text.Trim());
            string AccNumber = txtAccNo.Text.Trim();

            DataTable dt = new DataTable();
            dt = CHNLSVC.Sales.GetHp_ActiveAccounts(GlbUserComCode, txtPC.Text.Trim().ToUpper(), currDate, DateTime.MaxValue, DateTime.MaxValue, AccNumber, null);
            grvAccounts.DataSource = dt;
            grvAccounts.DataBind();
            if (grvAccounts.Rows.Count > 0)
            {               
                foreach (GridViewRow gvr in grvAccounts.Rows)
                {
                    Label pc = (Label)gvr.Cells[9].FindControl("lblAccPC");
                    txtPC.Text = pc.Text;
                }
            }
        }

        protected void ImgBtnNewPC_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTrPC.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnConfMorg_Click(object sender, EventArgs e)
        {
            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP1"))
            {
                btnConfMorg.Enabled = true;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Permission Granted!");
                string Msg = "<script>alert('No Permission Granted!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
            List<string> accountsList = new List<string>();
            foreach (GridViewRow gvr in grvAccounts.Rows) 
            {
                CheckBox chekbox = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
               
                if (chekbox.Checked)
                {
                    Label pc = (Label)gvr.Cells[9].FindControl("lblAccPC");
                    string pc_ = pc.Text;
                    if (pc_ != txtPC.Text.Trim())
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Profit center is changed after adding accounts!");
                        txtPC.Focus();
                        accountsList.Clear();
                        return;

                    }

                    string acc_no = gvr.Cells[1].Text;                    
                    accountsList.Add(acc_no);
                }                               
            }
            if (accountsList.Count<1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add accounts first!");
                return;
            }
            Int32 eff = CHNLSVC.Sales.Update_Flag_Bank("BANK", ddlMortgageCd.SelectedValue, accountsList);
            if (eff > 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Succsessfully Updated!");

            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error in Updation! Records are not updated.");
                return;
            }            
        }

        protected void btnConfCatego_Click(object sender, EventArgs e)
        {
            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP2"))
            {

                btnConfCatego.Enabled = true;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Permission Granted!");
                string Msg = "<script>alert('No Permission Granted!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
            List<string> accountsList = new List<string>();
            foreach (GridViewRow gvr in grvAccounts.Rows)
            {                
                CheckBox chekbox = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                
                if (chekbox.Checked)
                {
                    Label pc = (Label)gvr.Cells[9].FindControl("lblAccPC");
                    string pc_ = pc.Text;
                    if (pc_ != txtPC.Text.Trim())
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Profit center is changed after adding accounts!");
                        txtPC.Focus();
                        accountsList.Clear();
                        return;

                    }

                    string acc_no = gvr.Cells[1].Text;
                    accountsList.Add(acc_no);
                }               
            }
            if (accountsList.Count < 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add accounts first!");
                return;
            }
            Int32 eff = CHNLSVC.Sales.Update_Flag_Bank("FLAG", ddlCategoCd.SelectedValue, accountsList);
            if (eff >0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Succsessfully Updated!");               
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error in Updation! Records are not updated.");
                return;
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvAccounts.Rows)
            {
                CheckBox chekbox = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                chekbox.Checked = true;
            }
        }

        protected void btnNone_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvAccounts.Rows)
            {
                CheckBox chekbox = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                chekbox.Checked = false;
            }
        }

        protected void btnConfAccTr_Click(object sender, EventArgs e)
        {
            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP3"))
            {
                btnConfAccTr.Enabled = true;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Permission Granted!");
                string Msg = "<script>alert('No Permission Granted!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
            List<string> accountsList = new List<string>();
            foreach (GridViewRow gvr in grvAccounts.Rows)
            {
                CheckBox chekbox = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                if (chekbox.Checked)
                {
                    Label pc = (Label)gvr.Cells[9].FindControl("lblAccPC");
                    string pc_ = pc.Text;
                    if (pc_ != txtPC.Text.Trim())
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Profit center is changed after adding accounts!");
                        txtPC.Focus();
                        accountsList.Clear();
                        return;

                    }

                     string acc_no = gvr.Cells[1].Text;
                     accountsList.Add(acc_no);
                }             
            }
            if (accountsList.Count < 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add accounts first!");
                return;
            }
            DateTime curDate= Convert.ToDateTime(txtCurrentDt.Text.Trim());
            Int32 eff = CHNLSVC.Sales.Transfer_accounts(txtTrPC.Text.Trim().ToUpper(), curDate, accountsList);

            if (eff > 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Succsessfully Transfered!");
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error Occured. Failed Transfer!");
            }
        }

        protected void btnConfCustTr_Click(object sender, EventArgs e)
        {
            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP4"))
            {
                btnConfCustTr.Enabled = true;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Permission Granted!");
                string Msg = "<script>alert('No Permission Granted!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
            List<string> accountsList = new List<string>();
            foreach (GridViewRow gvr in grvAccounts.Rows)
            {
                CheckBox chekbox = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                if (chekbox.Checked)
                {
                    Label pc = (Label)gvr.Cells[9].FindControl("lblAccPC");
                    string pc_ = pc.Text;
                    if (pc_ != txtPC.Text.Trim())
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Profit center is changed after adding accounts!");
                        txtPC.Focus();
                        accountsList.Clear();
                        return;

                    }

                    string acc_no = gvr.Cells[1].Text;
                    accountsList.Add(acc_no);
                }
            }
            if (accountsList.Count < 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add accounts first!");
                return;
            }
            DateTime curDate = Convert.ToDateTime(txtCurrentDt.Text.Trim());
            HpCustomer _HpAccountCus = new HpCustomer();
            _HpAccountCus.Htc_adr_01 = txtAddresline1.Text; //+ txtAddresline2.Text;
            _HpAccountCus.Htc_adr_02 = txtAddresline2.Text;
            _HpAccountCus.Htc_adr_03 = txtAddresline3.Text;
            _HpAccountCus.Htc_adr_tp = 3;
            _HpAccountCus.Htc_cre_by = GlbUserName;
            _HpAccountCus.Htc_cre_dt = curDate;
            _HpAccountCus.Htc_cust_cd = txtCustCode.Text.Trim();
            _HpAccountCus.Htc_cust_tp = "C";
            if (accountsList.Count<1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select Accounts transfer!");
                return;
            }
            
           // HpCustomer customer = new HpCustomer();
           // customer = CHNLSVC.Sales.Get_HpAccCustomer("C", txtCustCode.Text.Trim(), 3, null);
            HpCustomer customer = getCustomer(txtCustCode.Text.Trim(), 3, null);
            if (customer != null)
            {
               // txtAddresline1.Text = customer.Htc_adr_01;
               // txtAddresline2.Text = customer.Htc_adr_02;
               // txtAddresline3.Text = customer.Htc_adr_03;
            }
            else
            {
                //customer = CHNLSVC.Sales.Get_HpAccCustomer("C", txtCustCode.Text.Trim(), 1, null);
                customer = getCustomer(txtCustCode.Text.Trim(), 1, null);
                if (customer==null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cusomer does not exists in our records!");//this customer number is not in HPT_CUST table
                    
                    return;
                }
               
            }
            Int32 eff = 0;
            try
            {
                eff = CHNLSVC.Sales.Update_AccCustomer(_HpAccountCus, accountsList, txtCustCode.Text.Trim());
                eff = CHNLSVC.Sales.Update_Account_Ownership(txtCustCode.Text.Trim(), accountsList, curDate);
                if (eff > 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Succsessfully Changed the ownership!");
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error Occured. Failed to Change!");
                }
            }
            catch(Exception ex)
            {
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error Occured.!");
                //return;
            }
             
            
        }
        private HpCustomer getCustomer(string custCode,Int32 addressType,string accNo)
        {
            HpCustomer customer = new HpCustomer();
            customer = CHNLSVC.Sales.Get_HpAccCustomer("C", custCode, addressType, accNo);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                txtAddresline1.Text = string.Empty;
                txtAddresline2.Text = string.Empty;
                txtAddresline3.Text = string.Empty;
                return null;
            }
               
           
        }
        protected void rdoCustDetChange_CheckedChanged(object sender, EventArgs e)
        {
            hideDives();
            divCustDetChange.Visible = true;
            uc_CustomerCreation1.EnableMainButtons(false);
            divCustUcButtons.Visible = true;
        }

        protected void txtHiddenCustCD_TextChanged(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyCustCD(uc_CustomerCreation1.CustCode);
            //ddlHO_status.SelectedValue = custProf.Mbe_ho_stus;
            //ddlSH_status.SelectedValue = custProf.Mbe_pc_stus;

            uc_CustCreationExternalDet1.SetExtraValues(custProf);

            CustomerAccountRef custAccRef = CHNLSVC.Sales.GetCustomerAccount(GlbUserComCode, uc_CustomerCreation1.CustCode);
           // txtCredLimit.Text = string.Format("{0:n2}", custAccRef.Saca_crdt_lmt);

            if (uc_CustomerCreation1.CustCode == "")
            {
                btn_CREATE.Enabled = true;
                
                //divCustDetClose.Visible = true;
            }
            else
            {
                btn_CREATE.Enabled = false;
                txtCustCode.Text = uc_CustomerCreation1.CustCode;

                HpCustomer customer = new HpCustomer();
                customer= CHNLSVC.Sales.Get_HpAccCustomer("C", uc_CustomerCreation1.CustCode, 3, null);
                if (customer!=null)
                {
                    txtAddresline1.Text = customer.Htc_adr_01;
                    txtAddresline2.Text = customer.Htc_adr_02;
                    txtAddresline3.Text = customer.Htc_adr_03;
                }
                
                //divCustDetClose.Visible = false;
            }
            uc_CustCreationExternalDet1.EnableAddressPanel(true);
                      
        }
        protected MasterBusinessEntity FinalMasterBusinessEntity(MasterBusinessEntity custPart1, MasterBusinessEntity custPart2)
        {
            MasterBusinessEntity customer = new MasterBusinessEntity();
            customer = custPart2;
            customer.Mbe_com = custPart1.Mbe_com;
            customer.Mbe_act = custPart1.Mbe_act;
            customer.Mbe_name = custPart1.Mbe_name;
            customer.Mbe_nic = custPart1.Mbe_nic;
            customer.Mbe_sub_tp = custPart1.Mbe_sub_tp;
            customer.Mbe_mob = custPart1.Mbe_mob;
            customer.Mbe_tp = custPart1.Mbe_tp;
            customer.Mbe_pp_no = custPart1.Mbe_pp_no;
            customer.Mbe_sex = custPart1.Mbe_sex;
            customer.Mbe_cate = custPart1.Mbe_cate;
            customer.Mbe_cre_dt = custPart1.Mbe_cre_dt;
            customer.Mbe_dl_no = custPart1.Mbe_dl_no;
            customer.Mbe_dob = custPart1.Mbe_dob;

            customer.Mbe_agre_send_sms = custPart1.Mbe_agre_send_sms;
            customer.Mbe_br_no = custPart1.Mbe_br_no;
            //customer.Mbe_ho_stus = ddlHO_status.SelectedValue;
            //customer.Mbe_pc_stus = ddlSH_status.SelectedValue;

            //--------------------------------------

            return customer;
        }
        protected void btn_CREATE_Click(object sender, EventArgs e)
        {
            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP4"))
            {
                // btn_UPDATE.Enabled = true;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Permission Granted!");
                string Msg = "<script>alert('No Permission Granted!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
            try
            {
                if (uc_CustomerCreation1.CustType == "Individual")
                {
                    if (uc_CustomerCreation1.DOB.Trim() == string.Empty)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Birthday!");
                        return;
                    }
                    Convert.ToDateTime(uc_CustomerCreation1.DOB.Trim());

                    if (uc_CustomerCreation1.NIC.Trim() == "" && uc_CustomerCreation1.PPNo.Trim() == "" && uc_CustomerCreation1.DL.Trim() == "")
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Need to fill either NIC or Passport or DL number!");
                        return;
                    }
                    else if (uc_CustomerCreation1.NIC.Trim() != "")
                    {
                        CustomerCreationUC CUST_ = new CustomerCreationUC();
                        Boolean isValid = CUST_.IsValidNIC(uc_CustomerCreation1.NIC.ToUpper());
                        if (isValid == false)
                        {
                            string Msg = "<script>alert('Invalid NIC number!' );</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                            return;
                        }

                    }
                }

            }
            catch (Exception ex)
            {

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid Birthday!");
                return;
            }

            if (uc_CustCreationExternalDet1.Addressline1.Trim() == "" && uc_CustCreationExternalDet1.Addressline2 == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Need to fill address!");
                return;
            }

            if (uc_CustomerCreation1.CustType != "Individual")
            {
                if (uc_CustomerCreation1.BrNo.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter BR number!");
                    return;
                }
                //TODO: enter Br no.
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Fill all Mandatory fields in the customer segmentation section!");
                //return;
            }

         
            MasterMsgInfoUCtrl.Clear();

            MasterBusinessEntity custPart1 = new MasterBusinessEntity();
            custPart1 = uc_CustomerCreation1.GetMainCustInfor();
            //----------------------------------------------------------
            MasterBusinessEntity custPart2 = new MasterBusinessEntity();
            custPart2 = uc_CustCreationExternalDet1.GetExtraCustDet(); //uc_CustomerCreation1.GetExtraCustDet();
            //----------------------------------------------------------
            MasterBusinessEntity finalCust = new MasterBusinessEntity();
            finalCust = FinalMasterBusinessEntity(custPart1, custPart2);
            //----------------------------------------------------------
            CustomerAccountRef _account = new CustomerAccountRef();
           
            _account.Saca_com_cd = GlbUserComCode;
            //try
            //{
            //    if (txtCredLimit.Text.Trim() != string.Empty)
            //    {
            //        _account.Saca_crdt_lmt = Convert.ToDecimal(txtCredLimit.Text.Trim());
            //    }
            //    else
            //    {
            //        _account.Saca_crdt_lmt = 0;
            //    }
            //    //if (_account.Saca_crdt_lmt < 0)
            //    //{
            //    //    txtCredLimit.Focus();
            //    //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid credit limit!");
            //    //    return;
            //    //}

            //}
            //catch (Exception ex)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid credit limit!");
            //    return;
            //}
            _account.Saca_cre_by = GlbUserName;
            _account.Saca_cre_when = DateTime.Now.Date;
            // _account.Saca_cust_cd = _invoiceHeader.Sah_cus_cd;
            _account.Saca_mod_by = GlbUserName;
            _account.Saca_mod_when = DateTime.Now.Date;
            _account.Saca_ord_bal = 0;
            _account.Saca_session_id = GlbUserSessionID;

            //----------------------------------------------------------
            List<MasterBusinessEntityInfo> bisInfoList = new List<MasterBusinessEntityInfo>();
            //foreach (GridViewRow gvr in this.grvCustSegmentation.Rows)
            //{
            //    MasterBusinessEntityInfo bisInfo = new MasterBusinessEntityInfo();
            //    //bisInfo.Mbei_cd
            //    bisInfo.Mbei_com = GlbUserComCode;
            //    Label type = (Label)gvr.Cells[2].FindControl("lblgrvTypeName");
            //    bisInfo.Mbei_tp = type.Text;
            //    DropDownList ddlVal = (DropDownList)gvr.Cells[3].FindControl("ddlgrvTypeVal");
            //    bisInfo.Mbei_val = ddlVal.SelectedValue;
            //    if (!(ddlVal.SelectedValue == string.Empty))
            //    {
            //        bisInfoList.Add(bisInfo);
            //    }


            //}
            CustomerCreationUC CUST = new CustomerCreationUC();
            string custCD = CUST.SaveCustomer(finalCust, _account, bisInfoList);

            uc_CustomerCreation1.CustCode = custCD;
            if (custCD != string.Empty)
            {
               // txtComCode.Text = GlbUserComCode;
                btn_CREATE.Enabled = false;
                string Msg = "<script>alert('Profile Created! Customer Code: " + custCD + "' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                string Msg = "<script>alert('Profile Not Created! Error occured due to wrong data.' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }


            #region not need
            //  string Ms = "<script>alert('TESTING MESSAGE' );</script>";
            //  ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Ms, false);


            //List<CashGeneralDicountDef> saveDiscountList = new List<CashGeneralDicountDef>();
            //foreach (CashGeneralDicountDef disc in DiscountList)
            //{
            //    //if (disc.Sgdd_pc == "All")
            //    //{
            //    //    // CHNLSVC.Sales.GetAllProfCenters(GlbUserComCode);
            //    //    //CHNLSVC.Sales.GetAllProfCenters(txtComCode.Text.Trim());

            //    //}
            //    //else
            //    //{
            //    //    saveDiscountList.Add(disc);
            //    //}
            //    saveDiscountList.Add(disc);
            //}

            //try
            //{
            //    if (DiscountList.Count > 0)
            //    {
            //        Int32 effect = 0;
            //        using (TransactionScope _tr = new TransactionScope())
            //        {
            //            foreach (CashGeneralDicountDef disc in saveDiscountList)
            //            {
            //                disc.Sgdd_cust_cd = custCD;
            //                if (disc.Sgdd_pc == "All")
            //                {
            //                    List<string> pclist = new List<string>();
            //                    pclist = CHNLSVC.Sales.GetAllProfCenters(disc.Sgdd_com);
            //                    foreach (string pc in pclist)
            //                    {
            //                        List<string> pclist_ = new List<string>();
            //                        pclist_.Add(pc);
            //                        effect = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist_);
            //                    }

            //                }
            //                else
            //                {
            //                    List<string> pclist = new List<string>();
            //                    pclist.Add(disc.Sgdd_pc);

            //                    effect = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist);
            //                }

            //            }
            //            _tr.Complete();
            //        }
            //        if (effect < 1)
            //        {
            //            string Msgg = "<script>alert('No entries gone to discount table' );</script>";
            //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
            //            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No entries gone to discount table!");
            //            return;

            //        }
            //        else
            //        {
            //            string Msg_ = "<script>alert('Discounts inserted successfully' );</script>";
            //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg_, false);
            //        }
            //        //Int32 effect = CHNLSVC.Sales.SaveBusinessEntityDiscount(DiscountList[0], txtDiscPC.Text.Trim());
            //    }

            //}
            //catch (Exception ex)
            //{
            //    string Msgg = "<script>alert('no entries gone to discount table' );</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Discounts were not added!");
            //    return;
            //}
            #endregion
            

        }
        protected void btn_UPDATE_Click(object sender, EventArgs e)
        {
            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "HP4"))
            {
               // btn_UPDATE.Enabled = true;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Permission Granted!");
                string Msg = "<script>alert('No Permission Granted!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);               
                return;
            }
            MasterBusinessEntity custPart1 = new MasterBusinessEntity();
            custPart1 = uc_CustomerCreation1.GetMainCustInfor();

            MasterBusinessEntity custPart2 = new MasterBusinessEntity();
            custPart2 = uc_CustCreationExternalDet1.GetExtraCustDet(); //uc_CustomerCreation1.GetExtraCustDet();

            MasterBusinessEntity finalCust = new MasterBusinessEntity();
            finalCust = FinalMasterBusinessEntity(custPart1, custPart2);
            finalCust.Mbe_cd = uc_CustomerCreation1.CustCode;
            finalCust.Mbe_com = GlbUserComCode;

            List<MasterBusinessEntityInfo> bisInfoList = new List<MasterBusinessEntityInfo>();
            //foreach (GridViewRow gvr in this.grvCustSegmentation.Rows)
            //{
            //    MasterBusinessEntityInfo bisInfo = new MasterBusinessEntityInfo();
            //    //bisInfo.Mbei_cd
            //    bisInfo.Mbei_com = GlbUserComCode;
            //    Label type = (Label)gvr.Cells[2].FindControl("lblgrvTypeName");
            //    bisInfo.Mbei_tp = type.Text;
            //    DropDownList ddlVal = (DropDownList)gvr.Cells[3].FindControl("ddlgrvTypeVal");
            //    bisInfo.Mbei_val = ddlVal.SelectedValue;
            //    if (!(ddlVal.SelectedValue == string.Empty))
            //    {
            //        bisInfoList.Add(bisInfo);
            //    }


            //}
           // CustomerCreationUC CUST = new CustomerCreationUC();
            Int32 effect = CHNLSVC.Sales.UpdateBizEntity_OnPermission(finalCust); //CUST.UpdateCustomer(finalCust, 0, bisInfoList);

            if (effect >= 0)
            {

                string Msg = "<script>alert('Profile Updated! (Customer Code:" + finalCust.Mbe_cd + ")' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                #region no need
                               
                //List<CashGeneralDicountDef> saveDiscountList = new List<CashGeneralDicountDef>();
                //foreach (CashGeneralDicountDef disc in DiscountList)
                //{
                //    saveDiscountList.Add(disc);
                //}

                //try
                //{
                //    if (DiscountList.Count > 0)
                //    {
                //        Int32 effect_ = 0;
                //        using (TransactionScope _tr = new TransactionScope())
                //        {
                //            foreach (CashGeneralDicountDef disc in saveDiscountList)
                //            {
                //                disc.Sgdd_cust_cd = uc_CustomerCreation1.CustCode;//custCD;
                //                if (disc.Sgdd_pc == "All")
                //                {
                //                    List<string> pclist = new List<string>();
                //                    pclist = CHNLSVC.Sales.GetAllProfCenters(disc.Sgdd_com);
                //                    foreach (string pc in pclist)
                //                    {
                //                        List<string> pclist_ = new List<string>();
                //                        pclist_.Add(pc);
                //                        effect_ = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist_);
                //                    }

                //                }
                //                else
                //                {
                //                    List<string> pclist = new List<string>();
                //                    pclist.Add(disc.Sgdd_pc);

                //                    effect_ = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist);
                //                }

                //            }
                //            _tr.Complete();
                //        }
                //        if (effect_ < 1)
                //        {
                //            string Msgg = "<script>alert('No entries gone to discount table' );</script>";
                //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                //            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No entries gone to discount table!");
                //            return;

                //        }
                //        else
                //        {
                //            string Msg_ = "<script>alert('Discounts inserted successfully' );</script>";
                //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg_, false);
                //        }

                //    }

                //}
                //catch (Exception ex)
                //{
                //    string Msgg = "<script>alert('no entries gone to discount table' );</script>";
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Discounts were not added!");
                //    return;
                //}
                #endregion
            }
            else
            {
                string Msg = "<script>alert('Failed To Update!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }

        protected void btn_CLEAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/HpCollection.aspx");
        }

        protected void ImgBtnCust_Click(object sender, ImageClickEventArgs e)
        {
            divCustUcButtons.Visible = false;
            divCustDetChange.Visible = true;
            divCustDetClose.Visible = true;
        }

        protected void ImgBtnAccUC_Click(object sender, ImageClickEventArgs e)
        {
           // string AccNo=string.Empty;
           // HpAccount Acc = new HpAccount();
           // Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
           //// uc_HpAccountSummary1.set_all_values(
           // ModalPopupExtSearch.Show();
        }

        protected void grvAccounts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;
            string AccNo = (string)grvAccounts.DataKeys[row_id][0];
            //decimal _settleAmount = (decimal)grvAccounts.DataKeys[row_id][1];

            DateTime curDate = Convert.ToDateTime(txtCurrentDt.Text.Trim());
            
            HpAccount Acc = new HpAccount();
            Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
            ModalPopupExtSearch.Show();
            uc_HpAccountSummary1.set_all_values(Acc, GlbUserDefProf, curDate, txtPC.Text.Trim());
           
            
        }

        protected void imgBtnTownSearch_Click(object sender, ImageClickEventArgs e)
        {
           

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTrPC.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnGetCust_Click(object sender, EventArgs e)
        {
            HpCustomer cust= getCustomer(txtCustCode.Text.Trim(),3,null);
            if (cust== null)
            {
                cust = getCustomer(txtCustCode.Text.Trim(), 1, null);
            }
            if (cust != null)
            {
                txtAddresline1.Text = cust.Htc_adr_01;
                txtAddresline2.Text = cust.Htc_adr_02;
                txtAddresline3.Text = cust.Htc_adr_03;
            }
            else
            {
                txtAddresline1.Text = string.Empty;
                txtAddresline2.Text = string.Empty;
                txtAddresline3.Text = string.Empty;
            }
            
        }

        protected void btnFB_Create_Click(object sender, EventArgs e)
        {
            HPR_FlagBank fb=new HPR_FlagBank();
            fb.Hfb_cd = txt_fbCode.Text.Trim().ToUpper();
            fb.Hfb_tp = ddlFB_type.SelectedValue;
            fb.Hpf_desc = txt_fbDescript.Text.Trim().ToUpper();
            fb.Hpf_cre_by = GlbUserName;
            fb.Hpf_cre_dt = DateTime.Now.Date;
            Int32 eff= CHNLSVC.Sales.Save_FlagBank(fb);
            if (eff > 0)
            {
                lblFlagBankSave.Text = "Successfully Created!";
                lblFlagBankSave.ForeColor = System.Drawing.Color.Green;
                txt_fbCode.Text = "";
                txt_fbDescript.Text = "";
            }
            else
            {
                lblFlagBankSave.Text = "Not Created!";
                lblFlagBankSave.ForeColor = System.Drawing.Color.Red;
                txt_fbCode.Text = "";
                txt_fbDescript.Text = "";
            }

            ModalPopupExtender_FB.Show();
        }

        protected void btnNewMortg_Click(object sender, EventArgs e)
        {
            string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _masterLocation, "FB"))
            {
                txt_fbCode.Text = "";
                txt_fbDescript.Text = "";
                lblFlagBankSave.Text = "";
                ddlFB_type.SelectedValue = "BANK";
                ddlFB_type.Enabled = false;
                ModalPopupExtender_FB.Show();
            }
            else
            {
                string Msg = "<script>alert('No Permission!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            
        }

        protected void btnNewFlag_Click(object sender, EventArgs e)
        {
             
             string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
             if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _masterLocation, "FB"))
             {
                 txt_fbCode.Text = "";
                 txt_fbDescript.Text = "";
                 lblFlagBankSave.Text = "";
                 ddlFB_type.SelectedValue = "FLAG";
                 ddlFB_type.Enabled = false;
                 ModalPopupExtender_FB.Show();
             }
             else
             {
                 string Msg = "<script>alert('No Permission!' );</script>";
                 ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
             }
           
        }

        protected void ImgBtnClose_Click(object sender, ImageClickEventArgs e)
        {
            divCustDetChange.Visible = false;
        }

        protected void btnNewCust_Click(object sender, EventArgs e)
        {
            divCustUcButtons.Visible = false;
            divCustDetChange.Visible = true;
            divCustDetClose.Visible = true;
        }

        protected void grvAccounts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Acc_no = e.Row.Cells[1].Text;
                DateTime currDate = Convert.ToDateTime(txtCurrentDt.Text.Trim());
                Decimal accBal = CHNLSVC.Sales.Get_AccountBalance(currDate, Acc_no);
                e.Row.Cells[6].Text = accBal.ToString();

                string custName = CHNLSVC.Sales.GetHpCustomerName(Acc_no);
                e.Row.Cells[3].Text = custName.ToString();
                Label pc = (Label)e.Row.Cells[9].FindControl("lblAccPC");

                DateTime ars_dt;
                DateTime sup_dt;
                processForGettingArrears(pc.Text,out ars_dt,out sup_dt);
                //Decimal arrears= CHNLSVC.Financial.Get_Arrears(pc.Text, Acc_no, currDate, currDate);
               
                DateTime arrDt=new DateTime();
                DateTime supDt=new DateTime();
                Hp_AccountSummary.get_ArearsDate_SupDate(pc.Text, currDate, out arrDt, out supDt);
                 Hp_AccountSummary SUMM=new Hp_AccountSummary();
                Decimal ARREARS= Hp_AccountSummary.getArears(Acc_no, SUMM, pc.Text, currDate, arrDt, sup_dt);
                e.Row.Cells[8].Text = ARREARS.ToString();               
                
            }
            
        }
        protected void processForGettingArrears(string pc ,out DateTime ars_dt, out DateTime sup_dt)
        {
            ars_dt =DateTime.MinValue.Date;
            sup_dt = DateTime.MinValue.Date;
            Hp_AccountSummary SUMMARY = new Hp_AccountSummary();
            DataTable hierchy_tbl = new DataTable();
            hierchy_tbl = SUMMARY.getHP_Hierachy(pc);//call sp_get_hp_hierachy
            if (hierchy_tbl.Rows.Count > 0)
            {
                foreach (DataRow da in hierchy_tbl.Rows)
                {
                    string party_tp = Convert.ToString(da["MPI_CD"]);
                    string party_cd = Convert.ToString(da["MPI_VAL"]);
                    //----------------------------------------------------
                    DataTable info_tbl = new DataTable();
                    info_tbl = SUMMARY.getArrearsInfo(party_tp, party_cd, DateTime.Now.Date);//returns one row
                    if (info_tbl.Rows.Count > 0)
                    {
                        DataRow DrECD = info_tbl.Rows[0];
                        DateTime HADD_ARS_DT = Convert.ToDateTime(info_tbl.Rows[0]["HADD_ARS_DT"]);//hadd_ars_dt
                        DateTime HADD_SUP_DT = Convert.ToDateTime(info_tbl.Rows[0]["HADD_SUP_DT"]);//hadd_sup_dt
                        ars_dt = HADD_ARS_DT;
                        sup_dt = HADD_SUP_DT;
                       
                        return;
                    }
                    else
                    {
                       // Arrears = 0;
                        // return Arrears;
                    }
                }
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/AccountsDetailUpdates.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/AccountsDetailUpdates.aspx");
        }
        //protected void btnTown_Click(object sender, EventArgs e)
        //{
        //    txtDistrict.Text = "";
        //    txtProvince.Text = "";
        //    txtPostalCode.Text = "";
        //    txtCountryCD.Text = "";

        //    CustomerCreationUC CUST = new CustomerCreationUC();
        //    DataTable dt = new DataTable();
        //    dt = CUST.Get_DetBy_town(txtTown.Text.Trim());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            string district = dt.Rows[0]["DISTRICT"].ToString();
        //            string province = dt.Rows[0]["PROVINCE"].ToString();
        //            string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
        //            string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();
        //            txtDistrict.Text = district;
        //            txtProvince.Text = province;
        //            txtPostalCode.Text = postalCD;
        //            txtCountryCD.Text = countryCD;
        //        }

        //    }
        //}
    }
}