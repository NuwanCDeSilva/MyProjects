using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;


namespace FF.WebERPClient.Financial_Modules
{
    public partial class ReturnCheque : BasePage
    {

        #region properties

        //public DataTable GridData
        //{
        //    get { return (DataTable)ViewState["gridData"]; }
        //    set { ViewState["gridData"] = value; }
        //}

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                GridRowCount.Value = "0";
                CreateTable();
                GridViewDataBind();
                BindBanks(DropDownListBank, DropDownListReturnBank, DropDownListNBank);
                TextBoxLocation.Text = GlbUserDefLoca;
                DivNewCheque.Visible = false;
            }
            Panel1.Height = 300;
            TextBoxLocation.Attributes.Add("onKeyup", "return clickButton(event,'" + imgLocation.ClientID + "')");
        }

        protected void imgLocation_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxLocation.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void TextBoxChequeNumber_TextChanged(object sender, EventArgs e)
        {
            GridViewDataBind();
        }

        protected void DropDownListBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewDataBind();
        }

        protected void TextBoxLocation_TextChanged(object sender, EventArgs e)
        {
            GridViewDataBind();
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Financial_Modules/ReturnCheque.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        private string AddHtmlSpaces(int length) {
            string space = "";
            for (int i = 0; i < length; i++) {
                space = space + " &nbsp;";
            }
            return space;
        }

        private void BindBanks(DropDownList ddl1, DropDownList ddl2,DropDownList ddl3)
        {
            ddl1.Items.Add(new ListItem("--select--", "-1"));
            DataTable datasource = CHNLSVC.Financial.GetBanks();
            foreach (DataRow dr in datasource.Rows) {
                ddl1.Items.Add(new ListItem(dr["mbi_cd"].ToString()+HttpUtility.HtmlDecode(AddHtmlSpaces(7-dr["mbi_cd"].ToString().Length)) + "-" + dr["mbi_desc"].ToString(), dr["mbi_cd"].ToString()));
            }

            //ddl1.DataSource = CHNLSVC.Financial.GetBanks();
            //ddl1.DataTextField = "mbi_desc";
            //ddl1.DataValueField = "mbi_cd";
            //ddl1.DataBind();

            ddl2.Items.Add(new ListItem("--select--", "-1"));
            foreach (DataRow dr in datasource.Rows)
            {
                ddl2.Items.Add(new ListItem(dr["mbi_cd"].ToString()+HttpUtility.HtmlDecode(AddHtmlSpaces(7-dr["mbi_cd"].ToString().Length)) + "-" + dr["mbi_desc"].ToString(), dr["mbi_cd"].ToString()));
            }
            ddl3.Items.Add(new ListItem("--select--", "-1"));
            foreach (DataRow dr in datasource.Rows)
            {
                ddl3.Items.Add(new ListItem(dr["mbi_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["mbi_cd"].ToString().Length)) + "-" + dr["mbi_desc"].ToString(), dr["mbi_cd"].ToString()));
            }
            //ddl2.DataSource = CHNLSVC.Financial.GetBanks();
            //ddl2.DataTextField = "mbi_desc";
            //ddl2.DataValueField = "mbi_cd";
            //ddl2.DataBind();
        }

        private void GridViewDataBind()
        {
            //check return cheque table
            DataTable datatable = CHNLSVC.Financial.GetReturnChequeCount(TextBoxChequeNumber.Text, DropDownListBank.SelectedValue, TextBoxLocation.Text.ToUpper());
            if (Convert.ToInt32(datatable.Rows[0][0].ToString()) <= 0)
            {

                DataTable datasource = CHNLSVC.Financial.GetReturnCheques(TextBoxLocation.Text.ToUpper(), TextBoxChequeNumber.Text, DropDownListBank.SelectedValue);
                //foreach (DataRow dr in datasource.Rows)
                //{
                //    //get unique values
                //    DataRow[] dr1 = GridData.Select("SARD_REF_NO = '" + dr["SARD_REF_NO"] + "'   and SARD_CHQ_BANK_CD = '" + dr["SARD_CHQ_BANK_CD"] + "'   and sard_chq_branch=  '" + dr["sard_chq_branch"] + "' and sard_settle_amt='" + dr["sard_settle_amt"] + "' ");
                //    if (dr1.Length <= 0)
                //    {
                //        GridData.ImportRow(dr);
                //    }
                //}
                GridViewCheques.DataSource = datasource;
               
                GridViewCheques.DataBind();
                GridRowCount.Value = GridViewCheques.Rows.Count.ToString();
                decimal total=0;
                foreach (DataRow dr in datasource.Rows) {
                    total = total +Convert.ToDecimal(dr["sard_settle_amt"]);
                }
                LabelToSysAmo.Text = total.ToString();
                TextBoxReturnAmount.Text = total.ToString();
            }
            else
            {
                if(TextBoxChequeNumber.Text!="" )
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Entering Cheque is already available as Return Cheque");
            }
        
        }

        private void CreateTable()
        {
            //GridData = new DataTable();
            //DataTable datasource = CHNLSVC.Financial.GetReturnCheques(TextBoxLocation.Text, TextBoxChequeNumber.Text, DropDownListBank.SelectedValue);
            //GridData = datasource;
        }
        
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void imgSelect_Click(object sender, ImageClickEventArgs e)
        {
            if (DivNewCheque.Visible)
            {
                DivNewCheque.Visible = false;
                Panel1.Height = 300;
            }
            else
            {
                DivNewCheque.Visible = true;
                Panel1.Height = 250;
            }

        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {

            try
            {
                int result = CHNLSVC.Financial.UpdateReturnCheques(TextBoxNChequeNumber.Text, TextBoxChequeNumber.Text, DropDownListNBank.SelectedValue.ToString(), DropDownListBank.SelectedValue, TextBoxBranch.Text, Convert.ToDateTime(TextBoxChDate.Text));
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records updated sucessfully!');", true);

                    TextBoxBranch.Text = "";
                    TextBoxChDate.Text = "";
                    DropDownListNBank.SelectedIndex = 0;
                    DivNewCheque.Visible = false;
                    GridViewDataBind();
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured while processing!!");
                }
            }
            catch (Exception) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select return date");
            }
            }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            
            int seqNo = CHNLSVC.Inventory.GetSerialID();
            if (GridViewCheques.Rows.Count > 0 )
            {
                if (DropDownListReturnBank.SelectedValue.Equals("-1")) {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select return bank");
                    return;
                }
                if (TextBoxReturnDate.Text == "") {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select return date");
                    return;
                }


                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                //get reciept number
                _receiptAuto.Aut_cate_cd = GlbUserDefProf;
                _receiptAuto.Aut_cate_tp = "GPC";
                _receiptAuto.Aut_start_char = "RTCHQ";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "RTCHQ";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = 2012;
                string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                //insert reciept
                RecieptHeader recieptHeadder = new RecieptHeader();
                recieptHeadder.Sar_seq_no = seqNo;
                recieptHeadder.Sar_receipt_no = _cusNo;
                recieptHeadder.Sar_com_cd = GlbUserComCode;
                recieptHeadder.Sar_receipt_type = "RTCHQ";
                recieptHeadder.Sar_receipt_date = DateTime.Now;
                recieptHeadder.Sar_profit_center_cd = TextBoxLocation.Text.ToUpper();
                recieptHeadder.Sar_debtor_name = "CHEQUE";
                recieptHeadder.Sar_tot_settle_amt = Convert.ToDecimal(TextBoxReturnAmount.Text);
                recieptHeadder.Sar_direct = false;
                recieptHeadder.Sar_act = true;
                recieptHeadder.Sar_create_by = GlbUserName;
                recieptHeadder.Sar_create_when = DateTime.Now;
                CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder);

                //insert reciept item
                RecieptItem recieptItem = new RecieptItem();
                recieptItem.Sard_seq_no = seqNo;
                recieptItem.Sard_line_no = 1;
                recieptItem.Sard_receipt_no = _cusNo;
                recieptItem.Sard_chq_bank_cd = DropDownListBank.SelectedValue;
                recieptItem.Sard_pay_tp = "CASH";
                recieptItem.Sard_settle_amt = 0;
                CHNLSVC.Sales.SaveReceiptItem(recieptItem);
                //add return cheque record
                ChequeReturn chequeReturn = new ChequeReturn();
                chequeReturn.Seq = seqNo;
                chequeReturn.RefNo = _cusNo;
                chequeReturn.Bank = DropDownListBank.SelectedValue;
                chequeReturn.Pc = TextBoxLocation.Text;
                chequeReturn.Bank_type = Convert.ToInt32(DropDownListBankType.SelectedValue);
                chequeReturn.Returndate = Convert.ToDateTime(TextBoxReturnDate.Text);
                chequeReturn.Return_bank = DropDownListReturnBank.SelectedValue;
                chequeReturn.Cheque_no = TextBoxChequeNumber.Text;
                chequeReturn.Company = GlbUserComCode;
                chequeReturn.Create_by = GlbUserName;
                chequeReturn.Create_Date = DateTime.Now;
                chequeReturn.Act_value = Convert.ToDecimal(TextBoxReturnAmount.Text);
               // chequeReturn.Settle_val = Convert.ToDecimal(TextBoxReturnAmount.Text);
                CHNLSVC.Financial.SaveReturnCheque(chequeReturn);
                TextBoxChequeNumber.Text = "";
                GridViewDataBind();
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            DivNewCheque.Visible = false;
            Panel1.Height = 300;
        }  
    }
}