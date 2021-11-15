using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;

namespace FF.WebERPClient.HP_Module
{
    public partial class SMSReminders : BasePage
    {

        #region proprties
        
        public string GeneralText {
            get { return (string)ViewState["GeneralText"]; }
            set { ViewState["GeneralText"] = value; }
        }

        public string ArrearsAccounts {
            get { return (string)ViewState["ArrearsAccounts"]; }
            set { ViewState["ArrearsAccounts"] = value; }
        }

        public string MonthlyDue {
            get { return (string)ViewState["MonthlyDue"]; }
            set { ViewState["MonthlyDue"] = value; }
        }

        public string HPCustomers {
            get { return (string)ViewState["HPCustomers"]; }
            set { ViewState["HPCustomers"] = value; }
        }

        public int GvRowCount {
            get { return (int)ViewState["GvRowCount"]; }
            set { ViewState["GvRowCount"] = value; }
        }

        public int GvAllCount {
            get { return (int)ViewState["GvAllCount"]; }
            set { ViewState["GvAllCount"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GvRowCount = 0;
                GvAllCount = 0;
                LoadPcs();
                HPCustomers = "Dear  [cusName] , thank u for buying [product] on Hire purchase [loc] S/R on [date]  (A/c [accNo]).Tot value [total] & diriya [diriya]-[com]-0112565293";
                MonthlyDue = "Dear customer, your next due date of HP Acc. No [accNo] is on [date] .-[com]-";
                ArrearsAccounts = "Dear Customer, Plz make arrangements to settle the arrears amount of Rs. [amo]  in HP Acc. No [accNo] Immediately.-[com]-";
                GeneralText = TextBoxMessage.Text + "[com]";
                DataTable dt = new DataTable();
                DataTable dt1=new DataTable();
                LoadFail(dt1);
                LoadGrid(dt);
                RadioButtonListMessageType_SelectedIndexChanged(null, null);
            }
            SendCount.Text = GvRowCount.ToString();
            LabelAllCount.Text = GvAllCount.ToString();
        }

        #region grid data bind

        private void LoadFail(DataTable dt1)
        {
            GridViewFailCust.DataSource = dt1;
            GridViewFailCust.DataBind();
        }

        private void LoadGrid(DataTable dt)
        {
            //check sms table
            //remove main grid add fail grid
            DataTable dt1=dt.Clone();
            List<DataRow> _dupList = new List<DataRow>();
            foreach (DataRow dr in dt.Rows) {
                HPReminderSMS _sms = CHNLSVC.Sales.GetSMSReminder(GlbUserComCode, GlbUserDefProf, dr["HPA_ACC_NO"].ToString(), DateTime.Now.Date);
                if (_sms != null) {

                    _dupList.Add(dr);
                }
            }
            foreach (DataRow dr in _dupList) {
                dt1.ImportRow(dr);
            }
            foreach (DataRow dr in _dupList)
            {
                dt.Rows.Remove(dr);
            }


            GvAllCount = dt.Rows.Count;
            GvRowCount = dt.Rows.Count;
            GridViewCust.DataSource = dt;
            GridViewCust.DataBind();
            SendCount.Text = GvRowCount.ToString();
            LabelAllCount.Text = GvAllCount.ToString();

            if (RadioButtonListMessageType.SelectedIndex == 1) {
                GridViewCust.Columns[4].Visible = true;
            }
            if (RadioButtonListMessageType.SelectedIndex == 2)
            {
                GridViewCust.Columns[5].Visible = true;
            }
            if (RadioButtonListMessageType.SelectedIndex != 2)
            {
                GridViewCust.Columns[5].Visible = false;
            }
            if (RadioButtonListMessageType.SelectedIndex != 1)
            {
                GridViewCust.Columns[4].Visible = false;
            }
            GridViewFailCust.DataSource = dt1;
            GridViewFailCust.DataBind();
        }

        private void BindEmpty() {
            DataTable dt = new DataTable();
            GridViewCust.DataSource = dt;
            GridViewCust.DataBind();

            GridViewFailCust.DataSource = dt;
            GridViewFailCust.DataBind();
        }

        #endregion


        private string GenarateMessage(string text,string _cusName,string _product,string _loc,DateTime _date,string _accNo,string _com,string _diriya,decimal _arrears) {

            if (text.Contains("[CustomerName]")) {
               text= text.Replace("[CustomerName]", _cusName);
            }
            if (text.Contains("[product]"))
            {
                text=text.Replace("[product]", _product);
            }
            if (text.Contains("[location]"))
            {
               text= text.Replace("[location]", _loc);
            }
            if (text.Contains("[date]"))
            {
                text = text.Replace("[date]", _date.ToShortDateString());
            }
            if (text.Contains("[accNo]"))
            {
                text = text.Replace("[accNo]", _accNo);
            }
            if (text.Contains("[com]")) {
                text = text.Replace("[com]",_com);
            }
            if (text.Contains("[diriya]"))
            {
                text = text.Replace("[diriya]", _diriya);
            }
            if (text.Contains("[amo]"))
            {
                text = text.Replace("[amo]", _arrears.ToString());
            }
            return text;
        }

        private void LoadPcs()
        {            
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            GridViewPC.DataSource = dataSource;
            GridViewPC.DataBind();
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HP_Module/SMSReminders.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void RadioButtonListMessageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (RadioButtonListMessageType.SelectedIndex == 0) {
                LabelMessage.Text = GeneralText;
                LabelMessage.Visible = false;
                TextBoxMessage.Visible = true;
                DivArrears.Visible = false;
                DivHpCus.Visible = false;
                DivMonthlyDue.Visible = false;
                RadioButtonGuarantor.Visible = false;
                SendCount.Text = "0";
                LabelAllCount.Text = "0";
            }
            else if (RadioButtonListMessageType.SelectedIndex == 1) {
                LabelMessage.Text = ArrearsAccounts;
                LabelMessage.Visible = true;
                TextBoxMessage.Visible = false;
                DivArrears.Visible = true;
                DivHpCus.Visible = false;
                RadioButtonGuarantor.Visible = false;
                DivMonthlyDue.Visible = false;
                TextBoxAsAtDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("dd/MMM/yyyy");
                SendCount.Text = "0";
                LabelAllCount.Text = "0";
            }
            else if (RadioButtonListMessageType.SelectedIndex == 2) {
                LabelMessage.Text = MonthlyDue;
                LabelMessage.Visible = true;
                TextBoxMessage.Visible = false;
                DivArrears.Visible = false;
                DivHpCus.Visible = false;
                DivMonthlyDue.Visible = true;
                RadioButtonGuarantor.Visible = false;
                DateTime today = DateTime.Today;
                TextBoxDueDt.Text = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month)).ToString("dd/MMM/yyyy");
                SendCount.Text = "0";
                LabelAllCount.Text = "0";
            }
            else if (RadioButtonListMessageType.SelectedIndex == 3) {
                LabelMessage.Text = HPCustomers;
                LabelMessage.Visible = true;
                TextBoxMessage.Visible = false;
                DivArrears.Visible = false;
                DivHpCus.Visible = true;
                DivMonthlyDue.Visible = false;
                RadioButtonGuarantor.Visible = true;
                SendCount.Text = "0";
                LabelAllCount.Text = "0";
            }
            BindEmpty();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (TextBoxDateFrom.Text == "" && RadioButtonListMessageType.SelectedIndex==3) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select from date");
                return;
            }
            if (TextBoxDateTo.Text == "" && RadioButtonListMessageType.SelectedIndex==3) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select to date");
                return;
            }
            if (RadioButtonListMessageType.SelectedIndex==3 && Convert.ToDateTime(TextBoxDateFrom.Text) > Convert.ToDateTime(TextBoxDateTo.Text)) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has to be smaller than to date");
                return;
            }
            string sendType = "";
            if (RadioButtonCustomer.Checked && RadioButtonListMessageType.SelectedIndex == 3)
            {
                sendType = "Customer_HP";
            }
            else if (RadioButtonGuarantor.Checked && RadioButtonListMessageType.SelectedIndex == 3)
            {
                sendType = "Guarantor_HP";
            }
            else if (RadioButtonListMessageType.SelectedIndex == 2)
                sendType = "Due";
            else if (RadioButtonListMessageType.SelectedIndex == 1)
                sendType = "Ars";
            else
                sendType = "Genaral";


            DataTable dt=new DataTable();
            foreach (GridViewRow gr in GridViewPC.Rows) {
                CheckBox chk = (CheckBox)gr.FindControl("chekPc");
            if (chk.Checked) {
                if(sendType=="Due")
                    dt.Merge(CHNLSVC.Sales.GetHPCustomer(GlbUserComCode, gr.Cells[1].Text, DateTime.MinValue, DateTime.MaxValue, sendType, null,Convert.ToDateTime(TextBoxDueDt.Text),DateTime.Now,DateTime.Now));
                else if(sendType=="Genaral")
                    dt.Merge(CHNLSVC.Sales.GetHPCustomer(GlbUserComCode, gr.Cells[1].Text, DateTime.MinValue,DateTime.MaxValue, sendType, null,DateTime.Now,DateTime.Now,DateTime.Now));
                else if (sendType == "Ars")
                {
                    Hp_AccountSummary summary = new Hp_AccountSummary();
                    DateTime ars_date;
                    DateTime sup_date;
                    Hp_AccountSummary.get_ArearsDate_SupDate(gr.Cells[1].Text, Convert.ToDateTime(TextBoxAsAtDate.Text), out ars_date, out sup_date);
                    dt.Merge(CHNLSVC.Sales.GetHPCustomer(GlbUserComCode, gr.Cells[1].Text, DateTime.Now,DateTime.Now, sendType, null, Convert.ToDateTime(TextBoxAsAtDate.Text),ars_date,sup_date));
                }
                else
                    dt.Merge(CHNLSVC.Sales.GetHPCustomer(GlbUserComCode, gr.Cells[1].Text, Convert.ToDateTime(TextBoxDateFrom.Text), Convert.ToDateTime(TextBoxDateTo.Text), sendType, null, DateTime.Now,DateTime.Now,DateTime.Now));
            }
            }
            
            //remove rows
            //arrears and due

            
            //due
            if (RadioButtonListMessageType.SelectedIndex == 2) {
                //DateTime ars_dt = DateTime.MinValue.Date;
                //DateTime sup_dt = DateTime.MinValue.Date;
                //processForGettingArrears(GlbUserDefProf, out ars_dt, out sup_dt, Convert.ToDateTime(TextBoxDueDt.Text));
                //if (ars_dt == DateTime.MinValue.Date)
                //{
                //    ars_dt = Convert.ToDateTime(TextBoxDueDt.Text);
                //}
                //if (sup_dt == DateTime.MinValue.Date)
                //{
                //    sup_dt = Convert.ToDateTime(TextBoxDueDt.Text);
                //}
                //foreach (DataRow dr in dt.Rows)
                //{

                //    //Decimal arrears = CHNLSVC.Sales.Get_hp_TotalDue( dr["HPA_ACC_NO"].ToString(), ars_dt.Date);
                //    decimal _monRen = CHNLSVC.Sales.Get_MonthlyRental(Convert.ToDateTime(TextBoxDueDt.Text), dr["HPA_ACC_NO"].ToString());
                //    if ( _monRen<=0)
                //    {
                //        dr.Delete();
                //    }
                //}
                //dt.AcceptChanges();
            }

            LoadGrid(dt);
        }

        

        protected void ButtonAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewPC.Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("chekPc");
                chk.Checked = true;
            }
        }

        protected void ButtonNone_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewPC.Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("chekPc");
                chk.Checked = false;
            }
        }

        protected void CheckBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxSelectAll.Checked)
            {
                foreach (GridViewRow gr in GridViewCust.Rows)
                {
                    CheckBox chk = (CheckBox)gr.FindControl("chekPc");
                    chk.Checked = true;
                    GvRowCount++;
                }
            }
            else {
                foreach (GridViewRow gr in GridViewCust.Rows)
                {
                    CheckBox chk = (CheckBox)gr.FindControl("chekPc");
                    chk.Checked = false;
                    GvRowCount--;
                }
            }
            SendCount.Text = GvRowCount.ToString();
            LabelAllCount.Text = GvAllCount.ToString();
        }

        private bool ValidateMobileNo(string num) {
            int intNum=0;
            //check only contain degits
            if (!int.TryParse(num, out intNum))
            {
                return false;
            }
            //check for length
            else
            {
                if (num.Length < 10)
                {
                    return false;
                }
                //check for first three chars
                else
                {
                    string firstChar = num.Substring(0, 3);
                    if (firstChar != "071" && firstChar != "077" && firstChar != "078" && firstChar != "072" && firstChar != "075")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            DateTime ars_dt=DateTime.MinValue.Date;
            DateTime sup_dt=DateTime.MinValue.Date;
            
            string message="";
            if (RadioButtonListMessageType.SelectedIndex == 0) {
                message = TextBoxMessage.Text;
            }
            else if (RadioButtonListMessageType.SelectedIndex == 1)
            {
                message = ArrearsAccounts;
                //processForGettingArrears(GlbUserDefProf, out ars_dt, out sup_dt, Convert.ToDateTime(TextBoxAsAtDate.Text));
                //if (ars_dt == DateTime.MinValue.Date)
                //{
                //    ars_dt = Convert.ToDateTime(TextBoxAsAtDate.Text).Date;
                //}
                //if (sup_dt == DateTime.MinValue.Date)
                //{
                //    sup_dt = Convert.ToDateTime(TextBoxAsAtDate.Text).Date; ;
                //}
            }
            else if (RadioButtonListMessageType.SelectedIndex == 2)
            {
                message = MonthlyDue;
                //processForGettingArrears(GlbUserDefProf, out ars_dt, out sup_dt,Convert.ToDateTime(TextBoxDueDt.Text));
                //if (ars_dt == DateTime.MinValue.Date)
                //{
                //    ars_dt = Convert.ToDateTime(TextBoxDueDt.Text).Date;
                //}
                //if (sup_dt == DateTime.MinValue.Date)
                //{
                //    sup_dt = Convert.ToDateTime(TextBoxDueDt.Text).Date; 
                //}
            }
            else if (RadioButtonListMessageType.SelectedIndex == 3)
            {
                message = HPCustomers;
            }
            
            int result = 0;
            //save process
            foreach (GridViewRow gr in GridViewCust.Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("chekPc");

                if (chk.Checked)
                {
                    string mobilNo = gr.Cells[7].Text;
                    string name = gr.Cells[2].Text;
                    string accNo=gr.Cells[1].Text;
                    string product = gr.Cells[3].Text;
                    string arrears = ((TextBox)gr.FindControl("TextBoxArrears")).Text;
                    bool isValid = ValidateMobileNo(mobilNo);
                    if (RadioButtonListMessageType.SelectedIndex == 1)
                    {
                        
                        
                        //Decimal arrears = CHNLSVC.Financial.Get_Arrears(GlbUserDefProf, accNo, ars_dt, sup_dt);
                        //decimal arrears = Convert.ToDecimal(gr.Cells[4].Text);
                        message = GenarateMessage(message, name, product, GlbUserDefLoca, DateTime.Now, accNo, GlbCompany, "",Convert.ToDecimal(arrears));
                    }
                    else if (RadioButtonListMessageType.SelectedIndex == 3)
                    {
                        string location = GlbUserDefProf;
                        string acc_seq = accNo;
                        List<HpAccount> accList = new List<HpAccount>();
                        accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
                        message = GenarateMessage(message, name, product, GlbUserDefLoca,DateTime.Now , accNo, GlbCompany, (accList[0].Hpa_init_ins + accList[0].Hpa_init_stm).ToString(), 0);
                    }
                    else if (RadioButtonListMessageType.SelectedIndex == 2)
                    {
                        DateTime _due;
                        if (DateTime.TryParse(((Label)gr.FindControl("LabelDue")).Text, out _due))
                        {
                            _due = Convert.ToDateTime(((Label)gr.FindControl("LabelDue")).Text);
                            message = GenarateMessage(message, name, product, GlbUserDefLoca, _due, accNo, GlbCompany, "", 0);
                        }
                        else
                            isValid = false;
                    }
                    else
                        message = GenarateMessage(message, name, product, GlbUserDefLoca, DateTime.Now, accNo, GlbCompany, "", 0);
                    if (isValid)
                    {
                        //send message
                        OutSMS _sms = new OutSMS();
                        _sms.Msgtype = "RMD";
                        if (message != "")
                            _sms.Msg = message;
                        else
                            _sms.Msg = " ";
                        _sms.Receiverphno = mobilNo;
                        _sms.Receiver = "";
                        _sms.Createtime = DateTime.Now;
                        _sms.Refdocno = accNo;
                        //TODO: save SMS
                        

                        HPReminderSMS _rmd = new HPReminderSMS();
                        _rmd.Hsrm_acc = accNo;
                        _rmd.Hsrm_com = GlbUserComCode;
                        _rmd.Hsrm_contact = mobilNo;
                        _rmd.Hsrm_cre_by = GlbUserName;
                        _rmd.Hsrm_cre_dt = DateTime.Now;
                        _rmd.Hsrm_pc = GlbUserDefProf;
                        _rmd.Hsrm_rmd_dt = DateTime.Now.Date;
                        _rmd.Hsrm_tp = "";
                        _rmd.Hsrm_val = 0;
                        result = CHNLSVC.Sales.SaveReminderSMS(_rmd,_sms);
                       
                    }
                }
            }
            
            if (result > 0)
            {
                string Msg = "<script>alert('Record insert Sucessfully!!');window.location = 'SMSReminders.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }

        }

        protected void chekPc_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk= (CheckBox)sender;
            if (chk.Checked)
            {
                GvRowCount++;
            }
            else
                GvRowCount--;
            SendCount.Text = GvRowCount.ToString();
            LabelAllCount.Text = GvAllCount.ToString();
        }

        protected void RadioButtonCustomer_CheckedChanged(object sender, EventArgs e)
        {
            ButtonSearch_Click(null, null);
        }

        protected void RadioButtonGuarantor_CheckedChanged(object sender, EventArgs e)
        {
            ButtonSearch_Click(null, null);
        }

        protected void processForGettingArrears(string pc, out DateTime ars_dt, out DateTime sup_dt,DateTime _date)
        {
            ars_dt = DateTime.MinValue.Date;
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
                    info_tbl = SUMMARY.getArrearsInfo(party_tp, party_cd, Convert.ToDateTime(_date));//returns one row
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
    }
}