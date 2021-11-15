using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using FF.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Imports.OrderFinancing
{
    public partial class OrderFinancing : BasePage
    {
        Order_Financing _Order = new Order_Financing();
        PIOrderFinancing _PIOrderFinancing = new PIOrderFinancing();
        OrderFinancingAmd _OrderFinancingAmd = new OrderFinancingAmd();
        OrderFinancingcost _OrderFinancingcost = new OrderFinancingcost();
        List<PIOrderFinancing>  _PIOrder =new List<PIOrderFinancing>();

        ImportFINPay _ImportFINPay = new ImportFINPay();
        DataTable _result;
        List<ImportPIDetails> _ImportPIDetails;
        List<ImportPIKit> _ImportPIKit;
        DataTable _resultPI;
        DataTable _PayType = new DataTable();
        DataTable _PICost = new DataTable();
        DataTable _PICostOther = new DataTable();
        decimal total = 0;
        decimal CostTotal = 0;
        decimal CostTotalLKR = 0;
        decimal CostPayTotal = 0;
        bool costtablenew = false;
        SystemUser _systemaduser = new SystemUser();
        string searchparam;
        int LineNo = 0;
        bool ISFinancialAmend = false;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    clear();
                    ddlPaymentTermsDataPopulate();
                    VisiblefalseButton();
                    if (Session["UserSBU"].ToString() == "")
                    {
                        lblMssg.Text = "SBU (Strategic Business) is not allocate for your login ID.";
                        lblMssg1.Text = "There is not setup default SBU (Sttre Buds Unit) for your login ID.";
                        SbuPopup.Show();
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

            }
            else if (IsPostBack)
            {
                txtPIDate.Text = Request[txtPIDate.UniqueID];
                txtEDate.Text = Request[txtEDate.UniqueID];
                txtADate.Text = Request[txtADate.UniqueID];
                txtIpolicyDate.Text = Request[txtIpolicyDate.UniqueID];
               // txtFLimit.Text = Request[txtFLimit.UniqueID];
                if (!(ViewState["SPIITEM_"] == null))
                {
                    ItemPopup.Show();
                    ViewState["SPIITEM_"] = null;
                }
                if (!(ViewState["SPIKitITEM_"] == null))
                {
                    KitItemPopup.Show();
                    ViewState["SPIKitITEM_"] = null;
                }
                if (!(ViewState["PaySettle"] == null))
                {
                    paypopup.Show();
                    ViewState["PaySettle"] = null;
                }
            }

        }
        #region
        private void clearMsg()
        {
            WarningOF.Visible = false;
            SuccessOF.Visible = false;
        }

        private void clear()
        {
            txtShipDate.Text = "";
            txtPiNo.Text = string.Empty;
            grdPid.DataSource = new int[] { };
            grdPid.DataBind();
            grdADetails.DataSource = new int[] { };
            grdADetails.DataBind();
            grdCDetails.DataSource = new int[] { };
            grdCDetails.DataBind();
            grdPayAmount.DataSource = new int[] { };
            grdPayAmount.DataBind();
            DateTime orddate = DateTime.Now;
            txtPIDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtEDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtIpolicyDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtADate.Text = orddate.ToString("dd/MMM/yyyy");
            lblAmdNo.Text = "0";
            lblStatus.Text = "Default";
            txtCPeriod.Text = string.Empty;
            txtDocNo.Text = string.Empty;
            txtUtilityV.Text = string.Empty;
            txtBankRefno.Text = string.Empty;
            txtfileNo.Text = string.Empty;
            txtManualRefNo.Text = string.Empty;
            txtBank.Text = string.Empty;
            txtAccountNo.Text = string.Empty;
            txtFLimit.Text = "0";
            txtTFAmount.Text = string.Empty;
            txtPiNo.Text = string.Empty;
            txtPTotal.Text = string.Empty;
            txtAamount.Text = string.Empty;
            txtICompany.Text = string.Empty;
            txtIsumInterest.Text = string.Empty;
            txtIPloicyNo.Text = string.Empty;
            txtIPremium.Text = string.Empty;
            lblSCurrancy.Text = string.Empty;
            CompanyCurrency();
            txtCost.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtLAmount.Text = string.Empty;
            txtERate.Text = string.Empty;
            txtcTotal.Text = string.Empty;
            txtCLKRTotal.Text = string.Empty;
            txtPayAmount.Text = string.Empty;
            txtPayAmountLKR.Text = string.Empty;
            lblCAmount.Text = "0";
            lblCPAmount.Text = "0";
            txtRate.Text = string.Empty;
            Session["FLimit"] = "";

        }
        private void SPITemplate()
        {
            DataTable ItemTbl = new DataTable();
            DataColumn dc = new DataColumn("Item Name", typeof(String));
            ItemTbl.Columns.Add(dc);
            dc = new DataColumn("Qty", typeof(String));
            ItemTbl.Columns.Add(dc);
            ViewState["PIITEM"] = ItemTbl;
            ViewState["SPIITEM"] = ItemTbl;
        }
        private void SPIKitTemplate()
        {
            DataTable ItemTbl = new DataTable();
            DataColumn dc = new DataColumn("Item Name", typeof(String));
            ItemTbl.Columns.Add(dc);
            dc = new DataColumn("Qty", typeof(String));
            ItemTbl.Columns.Add(dc);
            ViewState["PIKitITEM"] = ItemTbl;
            ViewState["SPIKitITEM"] = ItemTbl;
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.PINO:
                    {
                        paramsText.Append(BaseCls.GlbPINo + seperator + Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PaymentTerms:
                    {
                        paramsText.Append("IPM" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("BANK" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtBank.Text + seperator + ddlPaymentTerms.Text);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FacilityAmout:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlPaymentTerms.Text);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.costtype:
                    {
                        paramsText.Append(ddlPaymentTerms.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Financial:
                    {
                        paramsText.Append(searchparam + seperator + Session["UserCompanyCode"].ToString());
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void FilterData()
        {
            _result = (DataTable)ViewState["SEARCH"];
            DataView dv = new DataView(_result);
            string searchParameter = ddlSearchbykey.Text;
            dv.RowFilter = "" + ddlSearchbykey.Text + " LIKE '%" + txtSearchbyword.Text + "%'";
            // dv.RowFilter = "REFERENCESNO = '" + txtSearchbyword.Text + "' ";
            if (dv.Count > 0)
            {
                _result = dv.ToTable();
            }
          
            grdResult.DataSource = _result;
            grdResult.DataBind();
            UserPopoup.Show();
        }
        private bool checksup(string SCODE,string TTerm)
        {

            if ((txtsuppcode.Text == SCODE) && (txtTradeT.Text == TTerm))
            {
                return true;
            }
            if ((txtsuppcode.Text == null))
            {
                return true;
            }
            DisplayMessage("Can't select different supplier", 2);
            ////lblWOF.Text = "Can't select different Supplier";
            ////WarningOF.Visible = true;
            return false;
        }
        private bool checkPIalreadyadd(string PINO)
        {
            for (int i = 0; i < grdPid.Rows.Count; i++)
            {
                GridViewRow row = grdPid.Rows[i];
                string str = grdPid.DataKeys[row.RowIndex]["IP_PI_NO"].ToString();
                if (str == PINO)
                {
                    DisplayMessage("Already add PI number", 2);
                    //lblWOF.Text = "Already add PI Number";
                    //WarningOF.Visible = true;
                    return false;
                }
            }
            return true;
        }
        private void TmpCostDatatable()
        {
            DataRow dr = null;
            if (costtablenew == false)
            {
                return;
            }
            if (ViewState["costTable"] == null)
            {

                //define the columns
                _PICost.Columns.Add(new DataColumn("CType", typeof(string)));
                _PICost.Columns.Add(new DataColumn("Amount", typeof(decimal)));
                _PICost.Columns.Add(new DataColumn("AmountL", typeof(decimal)));
                dr = _PICost.NewRow();
                dr["CType"] = "COST";
                dr["Amount"] = txtPTotal.Text;
                decimal Total = decimal.Parse(txtERate.Text) * decimal.Parse(txtPTotal.Text);
                decimal ReduceTotal4degit = Math.Round(Total, 4);
                dr["AmountL"] = ReduceTotal4degit.ToString();
                _PICost.Rows.Add(dr);
                grdCDetails.DataSource = _PICost;
                grdCDetails.DataBind();
                ViewState["costTable"] = _PICost;
            }
            else
            {
                MergCostDatatable();
                return;
            }

            //ViewState["costTable"] = grdCDetails.DataSource;
            foreach (DataRow dre in _PICost.Rows)
            {
                CostTotal += Convert.ToDecimal(dr["Amount"]);
            }
            foreach (DataRow dre in _PICost.Rows)
            {
                CostTotalLKR += Convert.ToDecimal(dr["AmountL"]);
            }
            decimal currntFacilityam = Convert.ToDecimal(txtFLimit.Text);
            if (currntFacilityam < CostTotalLKR) {
                DisplayMessage("Facility limit not enough", 2);
               // ErrorAlert("Facility Limit not enough");
                Session["FLimit"] = "false";
                return;
            }


           // txtCLKRTotal.Text = CostTotalLKR.ToString();
            txtCLKRTotal.Text = Convert.ToDecimal(CostTotalLKR).ToString("#,##0.00");
           // txtcTotal.Text = CostTotal.ToString();
            txtcTotal.Text = Convert.ToDecimal(CostTotal).ToString("#,##0.00");
            lblSamount.Text = txtCLKRTotal.Text;
        }
        protected void lbtnWarningCostCategoryClose_Click(object sender, EventArgs e)
        {
            clearMsg();
        }
        private void PayTypeDatatTable()
        {
            _PayType.Columns.Add(new DataColumn("PType", typeof(string)));
            _PayType.Columns.Add(new DataColumn("AmountType", typeof(string)));
            _PayType.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            _PayType.Columns.Add(new DataColumn("IFY_AMT_DEAL", typeof(decimal)));
            _PayType.Columns.Add(new DataColumn("IFY_ANAL_2", typeof(string)));
            DataRow dr = null;
            // dr = _PayType.NewRow();
            // _PayType.Rows.Add(dr);
            ViewState["Pay"] = _PayType;
        }
        private void PayTypeAdd()
        {
            lblerror.Visible = false;
            if (ViewState["Pay"] == null)
            {
                PayTypeDatatTable();
            }
            DataTable dtCurrentTable = (DataTable)ViewState["Pay"];

            bool resultpay = false;
            if (ddlAmountType.SelectedValue == "1")
            {
                lblCAmount.Visible = true;
                lblCPAmount.Visible = false;
                decimal ratecalculation = (Convert.ToDecimal(lblSamount.Text) * Convert.ToDecimal(txtRate.Text)) / 100;
                txtPayAmountLKR.Text = ratecalculation.ToString();
                decimal amount = decimal.Parse(txtPayAmountLKR.Text);
                decimal Total = amount / decimal.Parse(txtERate.Text);
                decimal ReduceTotal4degit = Math.Round(Total, 4);
                txtPayAmount.Text = ReduceTotal4degit.ToString();
                decimal checkAmount = Convert.ToDecimal(lblCAmount.Text) + ratecalculation;

                decimal balnce = Convert.ToDecimal(lblSamount.Text) - Convert.ToDecimal(lblCAmount.Text);
                lblAamount.Text = balnce.ToString();
                if (checkAmount <= Convert.ToDecimal(lblSamount.Text))
                {
                    if ((dtCurrentTable != null) && (dtCurrentTable.Rows.Count > 0))
                    {
                        string type = dtCurrentTable.Rows[0]["AmountType"].ToString();
                        if (type == "Rate")
                        {
                            bool result = false;
                            foreach (DataRow row in dtCurrentTable.Rows)
                            {
                                if (row["PType"].ToString() == ddlPayType.SelectedItem.Text)
                                {
                                    decimal newvalue = Convert.ToDecimal(row[2].ToString());
                                    decimal newvalue2 = Convert.ToDecimal(row[3].ToString());
                                    row.SetField("Amount", newvalue + Convert.ToDecimal(txtPayAmount.Text));
                                    row.SetField("IFY_AMT_DEAL", newvalue2 + Convert.ToDecimal(txtPayAmountLKR.Text));
                                    row.SetField("IFY_ANAL_2", newvalue2 + Convert.ToDecimal(txtRate.Text));
                                    result = true;
                                }

                            }
                            if (result == false)
                            {
                                dtCurrentTable.Rows.Add(ddlPayType.SelectedItem.Text, ddlAmountType.SelectedItem.Text, decimal.Parse(txtPayAmount.Text), decimal.Parse(txtPayAmountLKR.Text), "");

                            }
                        }
                        else
                        {
                            lblerror.Visible = true;
                            lblerror.Text = "Can't add Rate type";
                        }
                    }
                    else
                    {
                        dtCurrentTable.Rows.Add(ddlPayType.SelectedItem.Text, ddlAmountType.SelectedItem.Text, decimal.Parse(txtPayAmount.Text), decimal.Parse(txtPayAmountLKR.Text), "");
                    }
                }
                else
                {

                    lblerror.Visible = true;
                    lblerror.Text = "Can't add amount ";
                }

            }
            if (ddlAmountType.SelectedValue == "2")
            {
                lblCAmount.Visible = false;
                lblCPAmount.Visible = true;
                decimal currentrate = Convert.ToDecimal(lblCPAmount.Text);
                decimal newrate = Convert.ToDecimal(txtPayAmount.Text);
                decimal newratet_PayAmountLKR = Convert.ToDecimal(txtPayAmountLKR.Text);
                decimal totalrate = currentrate + newratet_PayAmountLKR;
                decimal balnce = Convert.ToDecimal(lblSamount.Text) - Convert.ToDecimal(lblCPAmount.Text);
                lblAamount.Text = balnce.ToString();

                if ((totalrate <= Convert.ToDecimal(lblSamount.Text)) && (currentrate <= Convert.ToDecimal(lblSamount.Text)))
                {
                    if ((dtCurrentTable != null) && (dtCurrentTable.Rows.Count > 0))
                    {
                        string type = dtCurrentTable.Rows[0]["AmountType"].ToString();
                        if (type == "Amount")
                        {
                            bool result = false;
                            foreach (DataRow row in dtCurrentTable.Rows)
                            {
                                if (row["PType"].ToString() == ddlPayType.SelectedItem.Text)
                                {
                                    decimal newvalue = Convert.ToDecimal(row[2].ToString());
                                    decimal newvalue2 = Convert.ToDecimal(row[3].ToString());
                                    row.SetField("Amount", newvalue + Convert.ToDecimal(txtPayAmount.Text));
                                    row.SetField("IFY_AMT_DEAL", newvalue2 + Convert.ToDecimal(txtPayAmountLKR.Text));
                                    result = true;
                                }
                            }
                            if (result == false)
                            {
                                dtCurrentTable.Rows.Add(ddlPayType.SelectedItem.Text, ddlAmountType.SelectedItem.Text, decimal.Parse(txtPayAmount.Text), decimal.Parse(txtPayAmountLKR.Text), "");

                            }
                        }
                        else
                        {
                            lblerror.Visible = true;
                            lblerror.Text = "Can't add Amount type";
                        }
                    }
                    else
                    {
                        dtCurrentTable.Rows.Add(ddlPayType.SelectedItem.Text, ddlAmountType.SelectedItem.Text, decimal.Parse(txtPayAmount.Text), decimal.Parse(txtPayAmountLKR.Text), "");
                    }


                }
                else
                {
                    lblerror.Visible = true;
                    lblerror.Text = "Can't add amount ";
                }
            }

            foreach (DataRow row in dtCurrentTable.Rows)
            {
                CostPayTotal += Convert.ToDecimal(row["IFY_AMT_DEAL"]);
                if (ddlAmountType.SelectedItem.Text == "Rate")
                {
                    lblCPAmount.Text = "0";
                    lblCAmount.Text = CostPayTotal.ToString();
                    decimal balnc = Convert.ToDecimal(lblSamount.Text) - Convert.ToDecimal(lblCAmount.Text);
                    lblAamount.Text = balnc.ToString();
                }
                if (ddlAmountType.SelectedItem.Text == "Amount")
                {
                    lblCAmount.Text = "0";
                    lblCPAmount.Text = CostPayTotal.ToString();
                    decimal balnc = Convert.ToDecimal(lblSamount.Text) - Convert.ToDecimal(lblCPAmount.Text);
                    lblAamount.Text = balnc.ToString();
                }
                // resultpay = true;
            }

            grdPayAmount.DataSource = dtCurrentTable;
            grdPayAmount.DataBind();
            ViewState["Pay"] = dtCurrentTable;
            //if (resultpay == false)
            //{

            //    dtCurrentTable.Rows.Add(ddlPayType.SelectedItem.Text, ddlAmountType.SelectedItem.Text, decimal.Parse(txtPayAmount.Text));
            //    if (ddlAmountType.SelectedItem.Text == "Rate")
            //    {
            //        lblCAmount.Text = "";
            //        lblCAmount.Text = CostPayTotal.ToString();
            //    }
            //    if (ddlAmountType.SelectedItem.Text == "Amount")
            //    {
            //        lblCAmount.Text = "";
            //        lblCPAmount.Text = CostPayTotal.ToString();
            //    }
            //    ViewState["Pay"] = dtCurrentTable;
            //}

            // _PayType = ViewState["Pay"] as DataTable;
            // if (ViewState["PITable"] != null)
            // {
            //     PayTypeDatatTable();
            // }

            //// _PayType.Rows.Add(ddlPayType.SelectedItem.Text, ddlAmountType.SelectedItem.Text, Convert.ToDecimal(txtPayAmount.Text));
            // DataRow dr = null;
            // dr = _PayType.NewRow();
            // dr["PType"] = ddlPayType.SelectedItem.Text;
            // dr["AmountType"] = ddlAmountType.SelectedItem.Text;
            // decimal Total = decimal.Parse(txtPayAmount.Text);
            // dr["Amount"] = Total;
            // _PayType.Rows.Add(dr);
            // ViewState["Pay"] = _PayType;
            // lbltype.Text = ddlAmountType.SelectedItem.Text;
            // if (lbltype.Text == ddlAmountType.SelectedItem.Text)
            // {
            //     grdPayAmount.DataSource = _PayType;
            //     grdPayAmount.DataBind();
            //     foreach (DataRow dtr in _PayType.Rows)
            //     {
            //         CostPayTotal += Convert.ToDecimal(dtr["Amount"]);
            //         if (ddlAmountType.SelectedItem.Text == "Rate")
            //         {
            //             lblCAmount.Text = "";
            //             lblCAmount.Text = CostPayTotal.ToString();
            //         }
            //         if (ddlAmountType.SelectedItem.Text == "Amount")
            //         {
            //             lblCAmount.Text = "";
            //             lblCPAmount.Text = CostPayTotal.ToString();
            //         }
            //     }

            // }
            //  else
            //  {
            //         //ErrorAlert("Please select the ame type!");
            //  }



            paypopup.Show();
        }
        private void MergCostDatatable()
        {
            DataTable dtCurrentTable = (DataTable)ViewState["costTable"];
            bool result = false;
            foreach (DataRow row in dtCurrentTable.Rows)
            {
                if (row["CType"].ToString() == txtCost.Text)
                {
                    if (txtCost.Text == "COST")
                    {
                        decimal Total = decimal.Parse(txtERate.Text) * decimal.Parse(txtPTotal.Text);
                        decimal ReduceTotal4degit = Math.Round(Total, 4);
                        row.SetField("CType", "COST");
                        row.SetField("Amount", txtPTotal.Text);
                        row.SetField("AmountL", ReduceTotal4degit);
                        result = true;
                        row.AcceptChanges();
                        ViewState["costTable"] = dtCurrentTable;

                    }
                    else
                    {
                        decimal newvalue = Convert.ToDecimal(row[1].ToString());
                        decimal newvalue2 = Convert.ToDecimal(row[2].ToString());
                        row.SetField("CType", txtCost.Text);
                        row.SetField("Amount", newvalue + Convert.ToDecimal(txtAmount.Text));
                        row.SetField("AmountL", newvalue2 + Convert.ToDecimal(txtLAmount.Text));
                        result = true;
                        row.AcceptChanges();
                        ViewState["costTable"] = dtCurrentTable;
                    }
                   
                }
                //if (row["CType"].ToString() == "COST")
                //{
                //    decimal Total = decimal.Parse(txtERate.Text) * decimal.Parse(txtPTotal.Text);
                //    decimal ReduceTotal4degit = Math.Round(Total, 4);

                //    decimal newvalue = Convert.ToDecimal(row[1].ToString());
                //    decimal newvalue2 = Convert.ToDecimal(row[2].ToString());
                //    row.SetField("CType", "COST");
                //    row.SetField("Amount", txtPTotal.Text);
                //    row.SetField("AmountL", ReduceTotal4degit);
                //    result = true;
                //    row.AcceptChanges();
                //    ViewState["costTable"] = dtCurrentTable;
                //}

            }
            if (result == false)
            {

                dtCurrentTable.Rows.Add(txtCost.Text, Convert.ToDecimal(txtAmount.Text), Convert.ToDecimal(txtLAmount.Text));

            }
            //DataRow drw = null;
            ////define the columns
            //_PICostOther.Columns.Add(new DataColumn("CType", typeof(string)));
            //_PICostOther.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            //_PICostOther.Columns.Add(new DataColumn("AmountL", typeof(decimal)));
            //drw = _PICostOther.NewRow();
            //drw["CType"] = txtCost.Text;
            //drw["Amount"] = txtAmount.Text;
            //drw["AmountL"] = txtLAmount.Text;
            //_PICostOther.Rows.Add(drw);

            //ViewState["costTable"] = dtCurrentTable + "~" + _PICostOther;

            //dtCurrentTable.Merge(_PICostOther);
            // grdCDetails.DataSource = dtCurrentTable;
            //DataTable dtCurrentTable1 = (DataTable)ViewState["costTable"];
            dtCurrentTable.AcceptChanges();
            TotalCostcount();
            decimal currntFacilityam = Convert.ToDecimal(txtFLimit.Text);
            if (currntFacilityam < CostTotalLKR)
            {
                DisplayMessage("Facility limit not enough", 2);
                //ErrorAlert("Facility Limit not enough");
                return;
            }

            grdCDetails.DataSource = dtCurrentTable;
            grdCDetails.DataBind();          
            ViewState["costTable"] = dtCurrentTable;
           
        }
        private void PIDetailLoad(string _PID)
        {

            //ImportPI _ImportPI = null;
            //_ImportPI = CHNLSVC.Financial.GetPIByPIID(_PID);
            DataTable tbl = CHNLSVC.Financial.GetDPIByPIID(_PID);
           
            if (tbl.Rows.Count > 0)
            {

                string supliercode = tbl.Rows[0]["IP_SUPP"].ToString();
                string TTream = tbl.Rows[0]["IP_TOP"].ToString();

                DataTable _tblname = CHNLSVC.CustService.GetSupplierDetails(supliercode, "ABL");
                if (_tblname.Rows.Count > 0)
                {
                    txtSName.Text = _tblname.Rows[0][5].ToString();
                }

                string Bank = tbl.Rows[0]["IP_BANK_CD"].ToString();
                string Account = tbl.Rows[0]["IP_BANK_ACC_NO"].ToString();
                if (txtBank.Text==string.Empty)
                {
                    txtBank.Text = Bank;
                    txtAccountNo.Text = Account;
                }

                txtPiNo.Text = tbl.Rows[0]["IP_PI_NO"].ToString();
                decimal cost = Convert.ToDecimal(tbl.Rows[0]["IP_TOT_AMT"].ToString());
               
                if (ViewState["PITable"] != null)
                {
                    if (checksup(supliercode, TTream))
                    {
                        if (checkPIalreadyadd(txtPiNo.Text))
                        {
                            txtTradeT.Text=TTream;
                            txtsuppcode.Text = supliercode;
                            DataTable dtCurrentTable = (DataTable)ViewState["PITable"];
                            //DataRow drCurrentRow = dtCurrentTable.NewRow();
                            tbl.Merge(dtCurrentTable);


                            foreach (DataRow dr in tbl.Rows)
                            {
                                total += Convert.ToDecimal(dr["IP_TOT_AMT"]);
                            }


                            // _PICost.Rows.Clear();
                            //`   TmpCostDatatable();
                            DataTable dtCurrentTable1 = (DataTable)ViewState["costTable"];

                            Decimal LKRvalue = cost * decimal.Parse(txtERate.Text);
                            decimal totalNEW = LKRvalue + Convert.ToDecimal(txtCLKRTotal.Text);
                            decimal currntFacilityam = Convert.ToDecimal(txtFLimit.Text);
                            if (currntFacilityam < totalNEW) {
                                DisplayMessage("Facility limit not enough", 2);
                               // ErrorAlert("Facility Limit not enough");
                                Session["FLimit"] = "false";
                                return;
                            }
                            Session["FLimit"] = "true";
                            foreach (DataRow row in dtCurrentTable1.Rows)
                            {
                                if (row["CType"].ToString() == "COST")
                                {
                                    decimal currentcost = Convert.ToDecimal(row[1].ToString());
                                    decimal currentLKRvalue = Convert.ToDecimal(row[2].ToString());
                                    row.SetField("CType", "COST");
                                    row.SetField("Amount", cost + currentcost);
                                    row.SetField("AmountL", LKRvalue + currentLKRvalue);
                                }

                            }
                            grdPid.DataSource = tbl;
                            ViewState["PITable"] = tbl;
                            grdPid.DataBind();
                           // txtPTotal.Text = total.ToString("{0:n}");

                            txtPTotal.Text = Convert.ToDecimal(total).ToString("#,##0.00");

                            ViewState["costTable"] = dtCurrentTable1;


                            grdCDetails.DataSource = dtCurrentTable1;
                            grdCDetails.DataBind();
                            costtablenew = false;
                            //dtCurrentTable1.Rows.Find("COST");
                            txtCost.Text = "COST";
                        }
                    }
                }

                else
                {
                    txtsuppcode.Text = supliercode;
                    txtTradeT.Text = TTream;
                    GetCompanyandSupplierCurrancy();
                    bool result = false;
                    ExchangeRate(out result);
                    if (result == false)
                    {

                        DisplayMessage("Please set exchange rate", 2);
                        return;
                    }
                    decimal cost1 = Convert.ToDecimal(tbl.Rows[0]["IP_TOT_AMT"].ToString());
                    Decimal LKRvalue = cost1 * decimal.Parse(txtERate.Text);
                   
                    decimal currntFacilityam = Convert.ToDecimal(txtFLimit.Text);
                    if (currntFacilityam < LKRvalue)
                    {
                        DisplayMessage("Facility limit is not enough", 2);
                        //ErrorAlert("Facility Limit not enough");
                        Session["FLimit"] = "false";
                        return;
                    }
                    Session["FLimit"] = "true";
                    grdPid.DataSource = tbl;
                    ViewState["PITable"] = tbl;
                    grdPid.DataBind();
                    foreach (DataRow dr in tbl.Rows)
                    {
                        total += Convert.ToDecimal(dr["IP_TOT_AMT"]);
                    }
//txtPTotal.Text = total.ToString("{0:n}");
                    txtPTotal.Text = Convert.ToDecimal(total).ToString("#,##0.00");
                    _PICost.Rows.Clear();
                    costtablenew = true;

                }


            }

        }

        private void VisiblefalseButton()
        {
            lbtnUpdate.Enabled = false;
            lbtnUpdate.OnClientClick = "return Enable();";
            lbtnUpdate.CssClass = "buttoncolor";
            lbtnCancel.Enabled = false;
            lbtnCancel.OnClientClick = "return Enable();";
            lbtnCancel.CssClass = "buttoncolor";
            lbtnApproval.Enabled = false;
            lbtnApproval.OnClientClick = "return Enable();";
            lbtnApproval.CssClass = "buttoncolor";
        }
        private void VisibleButton()
        {
            lbtnUpdate.Enabled = true;
            lbtnUpdate.OnClientClick = "UpdateConfirm();";
            lbtnUpdate.CssClass = "buttonUndocolor";
            lbtnCancel.Enabled = true;
            lbtnCancel.OnClientClick = "CancelConfirm();";
            lbtnCancel.CssClass = "buttonUndocolor";
            lbtnApproval.Enabled = true;
            lbtnApproval.OnClientClick = "ApprovalConfirm();";
            lbtnApproval.CssClass = "buttonUndocolor";
        }

        private void TotalCostcount()
        {
            
            DataTable tbl = (DataTable)ViewState["costTable"];
            foreach (DataRow dr in tbl.Rows)
            {
              CostTotal += Convert.ToDecimal(dr["Amount"]);
            }
            foreach (DataRow dr in tbl.Rows)
            {
                CostTotalLKR += Convert.ToDecimal(dr["AmountL"]);
            }
            decimal currntFacilityam = Convert.ToDecimal(txtFLimit.Text);
            if (currntFacilityam < CostTotalLKR)
            {
                DisplayMessage("Facility limit not enough", 2);
                //ErrorAlert("Facility Limit not enough");
                return;
            }
            //txtCLKRTotal.Text = CostTotalLKR.ToString();
             txtCLKRTotal.Text = Convert.ToDecimal(CostTotalLKR).ToString("#,##0.00");
            lblSamount.Text = txtCLKRTotal.Text;
           // txtcTotal.Text = CostTotal.ToString();
            txtcTotal.Text = Convert.ToDecimal(CostTotal).ToString("#,##0.00");
            txtTFAmount.Text = txtCLKRTotal.Text;

            txtAamount.Text = txtTFAmount.Text;
            txtAamount.Text = Convert.ToDecimal(txtAamount.Text).ToString("#,##0.00");
            CostTotal = 0;
            CostTotalLKR = 0;
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int index = Convert.ToInt32(e.RowIndex);
            //DataTable old = ViewState["costTable"] as DataTable;

            //DataTable tbl = new DataTable();
            //tbl.Columns.Add(new DataColumn("Avtive", typeof(bool)));
            //tbl.Columns.Add(new DataColumn("CType", typeof(string)));
            //DataRow dr = tbl.Rows[index];
            //tbl.Rows.Add(old.Rows[index]["CType"]);
            //dr["Avtive"] = true;

            // DataTable dt = ViewState["costTable"] as DataTable;


            //// DataTable sd = new DataTable();
            // old.Columns.Add(new DataColumn("Avtive", typeof(bool)));
            // DataRow dr = old.Rows[index];
            // dr["Avtive"] = true;
            //// tbl = ViewState["costTable"] as DataTable;
            ////// old.Rows.Add(old.Rows[index]["Avtive"]);
            // ViewState["OldcostTable"] = old;
            // tbl = ViewState["OldcostTable"] as DataTable;

            // dt.Rows[index].Delete();
            // dt.AcceptChanges();
            // grdCDetails.DataSource = dt;
            // grdCDetails.DataBind();

            // DataTable newtbl = Session["OldcostTable"] as DataTable;
            // ViewState["OldcostTable"] = null;
            // ViewState["costTable"] = newtbl;


        }
        private void ddlPaymentTermsDataPopulate()
        {
            clearMsg();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PaymentTerms);
            DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams, null, null);
            ddlPaymentTerms.DataSource = result;
            ddlPaymentTerms.DataTextField = "CODE";
            ddlPaymentTerms.DataValueField = "CODE";
            ddlPaymentTerms.DataBind();
           // ddlPaymentTerms.ToolTip = ddlPaymentTerms.SelectedValue;
            string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PaymentTerms);
            DataTable result2 = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams2, "Code", ddlPaymentTerms.Text);

            if (result2 != null)
            {
                ddlPaymentTerms.ToolTip = result2.Rows[0][1].ToString();
            }
           // ddlPaymentTerms.ToolTip = ddlPaymentTerms.SelectedValue;
        }
        private void ddlSubpaymentTernDataPopulate(string PCAD, string PCD)
        {
            clearMsg();
            DataTable result = CHNLSVC.CommonSearch.GetPaymentSubTerm(PCAD, PCD);
            ddlSubpaymentTerms.DataSource = result;
            ddlSubpaymentTerms.DataTextField = "mcas_tp";
            ddlSubpaymentTerms.DataValueField = "mcas_tp";
            ddlSubpaymentTerms.DataBind();
           // ddlSubpaymentTerms.ToolTip = ddlSubpaymentTerms.SelectedValue ;
        }
        private void ExchangeRate(out bool value)
        {
            DataTable ERateTbl = CHNLSVC.Financial.GetExchangeRate(Session["UserCompanyCode"].ToString(), lblSCurrancy.Text, lblCompanyCurrancy.Text);
            if (ERateTbl != null)
            {
                if (ERateTbl.Rows.Count > 0)
                {
                    txtERate.Text = ERateTbl.Rows[0][5].ToString();
                    value = true;
                }
                else
                {
                    value = false;
                }
            }
            else
            {
                value = false;
            }
        }

        private void CompanyCurrency()
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable CompanyCurrancytbl = CHNLSVC.CommonSearch.SearchCompanyCurrancy(SearchParams);
            if (CompanyCurrancytbl.Rows.Count > 0)
            {
                lblCompanyCurrancy.Text = CompanyCurrancytbl.Rows[0]["CURRENCY"].ToString();
            }
        }
        private void GetCompanyandSupplierCurrancy()
        {

            List<MasterBusinessEntity> supplierlist = CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), txtsuppcode.Text, string.Empty, string.Empty, "S");

            if (supplierlist != null || supplierlist.Count > 1)
            {
                foreach (var _nicCust in supplierlist)
                {
                    Session["SUPPLER_CURRENCY"] = _nicCust.Mbe_cur_cd;
                    lblSCurrancy.Text = _nicCust.Mbe_cur_cd;
                }
            }

        }
        private void SaveOrderFinancing()
        {
            try
            {
                Tuple<int, string> _effect2;
                //Int32 row_aff = 0;
                string SBU_Character = Session["UserSBU"].ToString();
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString() + "(" + SBU_Character + ")";
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = null;
                mastAutoNo.Aut_modify_dt = null;
                mastAutoNo.Aut_moduleid = ddlPaymentTerms.Text; //DP, DA
                mastAutoNo.Aut_start_char = ddlPaymentTerms.Text;
                mastAutoNo.Aut_year = 2015;
                DataTable dt = ViewState["PITable"] as DataTable;

                DataTable costTableTbl = ViewState["costTable"] as DataTable;
                #region Order_Financing
                _Order.If_com = Session["UserCompanyCode"].ToString();
                _Order.If_sbu = Session["UserSBU"].ToString();
                _Order.If_tp = ddlPaymentTerms.Text;
                if (ddlSubpaymentTerms.Text != "")
                {
                    _Order.If_sub_tp = ddlSubpaymentTerms.Text.ToUpper();
                }
               
                _Order.If_ref_no = txtBankRefno.Text;
                _Order.If_file_no = txtfileNo.Text;
                _Order.If_othdoc_no = txtManualRefNo.Text;
                _Order.If_doc_dt = Convert.ToDateTime(txtPIDate.Text);
                _Order.If_exp_dt = Convert.ToDateTime(txtEDate.Text);
                //_Order.If_rmk = "";

                _Order.If_supp = txtsuppcode.Text;
                _Order.If_top_cat = "TOT";
                _Order.If_top = txtTradeT.Text;
                _Order.If_cur = lblSCurrancy.Text;
                _Order.If_ex_rt = decimal.Parse(txtERate.Text);
                _Order.If_tot_amt = decimal.Parse(txtcTotal.Text);
                _Order.If_tot_amt_deal = decimal.Parse(txtCLKRTotal.Text);
                _Order.If_set_amt_deal = 0;
                _Order.If_bank_cd = txtBank.Text;
                _Order.If_bank_acc_no = txtAccountNo.Text;
                _Order.If_fac_lmt = decimal.Parse(txtFLimit.Text); //Nedd ask again
                _Order.If_uti_lmt = decimal.Parse(txtUtilityV.Text);
                _Order.If_insu_com = txtICompany.Text;
                _Order.If_insu_poli_no = txtIPloicyNo.Text;
                _Order.If_insu_poli_dt = Convert.ToDateTime(txtIpolicyDate.Text);
                _Order.If_insu_amt = decimal.Parse(txtIsumInterest.Text);
                _Order.If_prem_amt = decimal.Parse(txtIPremium.Text);
                 bool value = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16001);
                 if (value == true)
                 {
                     _Order.If_stus = "A";
                 }
                 else
                 {
                     _Order.If_stus = "S";
                 }
               

                _Order.If_si_stus = false;
                _Order.Ip_is_kit = false;//false //Need clarify again
                _Order.If_amd_seq = 0;
                _Order.If_anal_1 = txtbranch.Text;
                //_Order.If_anal_2 = "";
                //_Order.If_anal_3 = "";
                //_Order.If_anal_4 = "";
                if (txtCPeriod.Text == "")
                {
                    txtCPeriod.Text = "0";
                }
                _Order.If_crdt_pd = Convert.ToInt32(txtCPeriod.Text);
                _Order.If_cre_by = Session["UserID"].ToString();
                _Order.If_cre_dt = System.DateTime.Now;
                //_Order.If_mod_by = "";
                //_Order.If_mod_dt = "";
                _Order.If_session_id = Session["SessionID"].ToString();
                if (ddlPaymentTerms.SelectedItem.Text== "LC")
                {
                    _Order.If_latest_sh_dt = Convert.ToDateTime(txtShipDate.Text.Trim());
                }
                //SESSION ID
                #endregion
                #region OrderFinancingPI
                // _PIOrderFinancing.Ifp_tot_amt = Convert.ToDecimal(txtPTotal.Text);
                _PIOrderFinancing.Ifp_ex_rt = Convert.ToDecimal(txtERate.Text);
                _PIOrderFinancing.Ifp_cre_by = Session["UserID"].ToString();
                _PIOrderFinancing.Ifp_cre_dt = System.DateTime.Now;
                _PIOrderFinancing.Ifp_session_id = Session["SessionID"].ToString();
                #endregion
                #region OrderFinancingAmd

                _OrderFinancingAmd.Ifa_line = 0;
                _OrderFinancingAmd.Ifa_amd_dt = Convert.ToDateTime(txtADate.Text);
                _OrderFinancingAmd.Ifa_amt = Convert.ToDecimal(txtAamount.Text);
                _OrderFinancingAmd.Ifa_amt_deal = Convert.ToDecimal(txtAamount.Text);
                _OrderFinancingAmd.Ifa_ex_rt = Convert.ToDecimal(txtERate.Text);
                _OrderFinancingAmd.Ifa_cre_by = Session["UserID"].ToString();
                _OrderFinancingAmd.Ifa_cre_dt = System.DateTime.Now;
                _OrderFinancingAmd.Ifa_session_id = Session["SessionID"].ToString();
                #endregion

                #region OrderFinancingcost
                _OrderFinancingcost.Ifc_ele_cat = "IPM";
                _OrderFinancingcost.Ifc_ele_tp = ddlPaymentTerms.Text;
                _OrderFinancingcost.Ifc_ex_rt = Convert.ToDecimal(txtERate.Text);
                // _OrderFinancingcost.Ifc_act = 1;
                _OrderFinancingcost.Ifc_cre_by = Session["UserID"].ToString();
                _OrderFinancingcost.Ifc_cre_dt = System.DateTime.Now;
                _OrderFinancingcost.Ifc_session_id = Session["SessionID"].ToString();
                #endregion

                #region OrderFinancingPayment
                _ImportFINPay.Ify_doc_no = txtDocNo.Text;
                _ImportFINPay.Ify_cre_by = Session["UserID"].ToString();
                _ImportFINPay.Ify_cre_dt = System.DateTime.Now;
                _ImportFINPay.Ify_session_id = Session["SessionID"].ToString();
                _ImportFINPay.Ify_ex_rt = Convert.ToDecimal(txtERate.Text);
                DataTable _ImportFINPayTbl = ViewState["Pay"] as DataTable;
                #endregion
                // CHNLSVC.Financial.UpdateAutoNumber(mastAutoNo);
                _effect2 = CHNLSVC.Financial.SaveNewOrderFinancing(_Order, mastAutoNo, _PIOrderFinancing, dt, _OrderFinancingAmd, _OrderFinancingcost, costTableTbl, _ImportFINPay, _ImportFINPayTbl);
                if (_effect2.Item1 > 0)
                {
                    string _msg = "Succesfully saved   "  + _effect2.Item2;
                    DisplayMessage(_msg, 3);
                   // SuccessAlert("Succesfully Save.. !" + _effect2.Item2);
                    lblStatus.Text = "Saved";
                }
                else
                {
                    DisplayMessage(_effect2.Item2, 2);
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
            }

        }

        private void UpdateOrderFinancing()
        {
            try
            {
                Int32 row_aff = 0;
                #region Order_Financing
                _Order.If_doc_no = txtDocNo.Text;
                _Order.If_com = Session["UserCompanyCode"].ToString();
                _Order.If_sbu = Session["UserSBU"].ToString();
                _Order.If_tp = ddlPaymentTerms.Text;
                if (ddlSubpaymentTerms.Text != "")
                {
                    _Order.If_sub_tp = ddlSubpaymentTerms.Text;
                }
                
                _Order.If_ref_no = txtBankRefno.Text;
                _Order.If_file_no = txtfileNo.Text;
                _Order.If_othdoc_no = txtManualRefNo.Text;
                _Order.If_doc_dt = Convert.ToDateTime(txtPIDate.Text);
                _Order.If_exp_dt = Convert.ToDateTime(txtEDate.Text);
                //_Order.If_rmk = "";
                _Order.If_supp = txtsuppcode.Text;
                _Order.If_top_cat = "TOT";
                _Order.If_top = txtTradeT.Text;
                _Order.If_cur = lblSCurrancy.Text;
                _Order.If_ex_rt = decimal.Parse(txtERate.Text);
                _Order.If_tot_amt = decimal.Parse(txtcTotal.Text);
                _Order.If_tot_amt_deal = decimal.Parse(txtCLKRTotal.Text);
                _Order.If_set_amt_deal = 0;
                _Order.If_bank_cd = txtBank.Text;
                _Order.If_bank_acc_no = txtAccountNo.Text;
                _Order.If_fac_lmt = decimal.Parse(txtFLimit.Text); //Nedd ask again
                _Order.If_uti_lmt = decimal.Parse(txtUtilityV.Text);
                _Order.If_insu_com = txtICompany.Text;
                _Order.If_insu_poli_no = txtIPloicyNo.Text;
                _Order.If_insu_poli_dt = Convert.ToDateTime(txtIpolicyDate.Text);
                _Order.If_insu_amt = decimal.Parse(txtIsumInterest.Text);
                _Order.If_prem_amt = decimal.Parse(txtIPremium.Text);
                if (Session["Status"].ToString() == "A")
                {
                     bool value = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16001);
                     if (value == true)
                     {
                         _Order.If_stus = "A";
                     }
                     //else
                     //{
                     //    _Order.If_stus = "S";
                     //}
                }
                else
                {
                    _Order.If_stus = "S";
                }

                _Order.If_si_stus = false;
                _Order.Ip_is_kit = false;//false //Need clarify again
                _Order.If_amd_seq = 0;
                //_Order.If_anal_1 = "";
                //_Order.If_anal_2 = "";
                //_Order.If_anal_3 = "";
                //_Order.If_anal_4 = "";
                _Order.If_crdt_pd = Convert.ToInt32(txtCPeriod.Text);
                // _Order.If_cre_by = Session["UserID"].ToString();
                // _Order.If_cre_dt = System.DateTime.Now;
                _Order.If_mod_by = Session["UserID"].ToString();
                _Order.If_mod_dt = System.DateTime.Now;
                _Order.If_session_id = Session["SessionID"].ToString();
                if (ddlPaymentTerms.SelectedItem.Text == "LC")
                {
                    _Order.If_latest_sh_dt = Convert.ToDateTime(txtShipDate.Text.Trim());
                }
                //SESSION ID
                #endregion
                #region OrderFinancingPI
                _PIOrderFinancing.Ifp_seq_no = Convert.ToInt32(lblSeqno.Text);
                DataTable dt = ViewState["OLDPITable"] as DataTable;
                if (dt == null)
                {
                    dt = ViewState["PITable"] as DataTable;

                }
                DataTable NewAmendPI = ViewState["NewAmendPI"] as DataTable;
                DataTable costTableTbl = ViewState["costTable"] as DataTable;
                _PIOrderFinancing.Ifp_ex_rt = Convert.ToDecimal(txtERate.Text);
                _PIOrderFinancing.Ifp_cre_by = Session["UserID"].ToString();
                _PIOrderFinancing.Ifp_cre_dt = System.DateTime.Now;
                _PIOrderFinancing.Ifp_session_id = Session["SessionID"].ToString();
                _PIOrderFinancing.Ifp_mod_by = Session["UserID"].ToString();
                _PIOrderFinancing.Ifp_mod_dt = System.DateTime.Now;
                //foreach (DataRow dr in dt.Rows)
                //{
                //    object value = dr[7];
                //    if (value == DBNull.Value)
                //    {

                //    }
                //    else
                //    {
                //        int accountLevel = Convert.ToInt32(dr[7].ToString());
                //        LineNo = Math.Max(LineNo, accountLevel);
                //        _PIOrderFinancing.Ifp_line = LineNo + 1;
                //    }

                //}
                _PIOrderFinancing.Ifp_doc_no = txtDocNo.Text;

                #endregion
                #region OrderFinancingAmd
                _OrderFinancingAmd.Ifa_seq_no = Convert.ToInt32(lblSeqno.Text);
                _OrderFinancingAmd.Ifa_doc_no = txtDocNo.Text;
                int x;
                //int.TryParse(lblAmdNo.Text.ToString(), out x);

                _OrderFinancingAmd.Ifa_line = Convert.ToInt32(lblAmdNo.Text);
                _OrderFinancingAmd.Ifa_amd_dt = Convert.ToDateTime(txtADate.Text);
                _OrderFinancingAmd.Ifa_amt = Convert.ToDecimal(txtAamount.Text);
                _OrderFinancingAmd.Ifa_amt_deal = Convert.ToDecimal(txtAamount.Text);
                _OrderFinancingAmd.Ifa_ex_rt = Convert.ToDecimal(txtERate.Text);
                _OrderFinancingAmd.Ifa_cre_by = Session["UserID"].ToString();
                _OrderFinancingAmd.Ifa_cre_dt = System.DateTime.Now;
                _OrderFinancingAmd.Ifa_session_id = Session["SessionID"].ToString();
                #endregion
                #region OrderFinancingcost
                _OrderFinancingcost.Ifc_seq_no = Convert.ToInt32(lblSeqno.Text);
                _OrderFinancingcost.Ifc_line = int.Parse(lblCostLineNo.Text);
                _OrderFinancingcost.Ifc_ele_cat = "IPM";
                _OrderFinancingcost.Ifc_ele_tp = ddlPaymentTerms.Text;
                _OrderFinancingcost.Ifc_doc_no = txtDocNo.Text;
                _OrderFinancingcost.Ifc_ex_rt = Convert.ToDecimal(txtERate.Text);
                // _OrderFinancingcost.Ifc_act = 1;
                _OrderFinancingcost.Ifc_cre_by = Session["UserID"].ToString();
                _OrderFinancingcost.Ifc_cre_dt = System.DateTime.Now;
                _OrderFinancingcost.Ifc_session_id = Session["SessionID"].ToString();
                #endregion
                #region OrderFinancingPayment
                _ImportFINPay.Ify_doc_no = txtDocNo.Text;
                _ImportFINPay.Ify_cre_by = Session["UserID"].ToString();
                _ImportFINPay.Ify_cre_dt = System.DateTime.Now;
                _ImportFINPay.Ify_session_id = Session["SessionID"].ToString();
                _ImportFINPay.Ify_ex_rt = Convert.ToDecimal(txtERate.Text);
                _ImportFINPay.Ify_seq_no = Convert.ToInt32(lblSeqno.Text);
                _ImportFINPay.Ify_line = Convert.ToInt32(lblPaylineno.Text);
                DataTable _ImportFINPayTbl = ViewState["Pay"] as DataTable;
                #endregion

                DataTable _DelPI = ViewState["DeletePI"] as DataTable;
                DataTable _DelCost = ViewState["DeleteCost"] as DataTable;
                if (Session["TFacilityAmount"].ToString() != txtTFAmount.Text) { ISFinancialAmend = true; }
                row_aff = CHNLSVC.Financial.UpdateNewOrderFinancing(_Order,_DelPI, _PIOrderFinancing, dt, NewAmendPI, _OrderFinancingAmd, _OrderFinancingcost, costTableTbl,_DelCost, _ImportFINPay, _ImportFINPayTbl, ISFinancialAmend);

                if (row_aff > 0)
                {
                    if (ISFinancialAmend == false)
                    {
                        DisplayMessage("Succesfully Updated !", 3);
                        //SuccessAlert("Succesfully Update.. !");
                    }
                    else
                    {
                        DisplayMessage("Amendment Details successfully updated", 3);
                        //SuccessAlert("Amendment Details Successfully Updated .. !!");
                    }
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
            }
        }
        private void SuccessAlert(string msg)
        {
            lblSOF.Text = msg;
            SuccessOF.Visible = true;
        }
        private void ErrorAlert(string msg)
        {
            lblWOF.Text = msg;
            WarningOF.Visible = true;
        }
        private bool validation()
        {
            clearMsg();
            string value = txtPIDate.Text;
            if (string.IsNullOrEmpty(Session["UserCompanyCode"].ToString()))
            {
                DisplayMessage("Please select the comapny", 2);
                //ErrorAlert("Please select the Comapny!");
                return false;
            }
            //if (string.IsNullOrEmpty(Session["UserSBU"].ToString()))
            //{
            //    ErrorAlert("Please select the SBU!");
            //    return false;
            //}
            if (string.IsNullOrEmpty(ddlPaymentTerms.Text))
            {
                DisplayMessage("Please select the payment term", 2);
                //ErrorAlert("Please select the Payment Term!");
                ddlPaymentTerms.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(ddlSubpaymentTerms.Text))
            //{
            //    ErrorAlert("Please select the Sub Payment Term!");
            //    txtDocNo.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtBankRefno.Text))
            {
                DisplayMessage("Please select bank reference number", 2);
                //ErrorAlert("Please select Bank Ref # !");
                txtBankRefno.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtfileNo.Text))
            {
                DisplayMessage("Please select file number", 2);
               // ErrorAlert("Please select File # !");
                txtfileNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtManualRefNo.Text))
            {
                DisplayMessage("Please select file number", 2);
                //ErrorAlert("Please select File # !");
                txtManualRefNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPIDate.Text))
            {
                DisplayMessage("Please select date", 2);
                //ErrorAlert("Please select Date !");
                txtPIDate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtEDate.Text))
            {
                DisplayMessage("Please select expir date", 2);
                //ErrorAlert("Please select Expir Date !");
                txtEDate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtBank.Text))
            {
                DisplayMessage("Please select the bank", 2);
                //ErrorAlert("Please select the Bank!");
                txtBank.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text))
            {
                DisplayMessage("Please select the bank account number", 2);
                //ErrorAlert("Please select the Bank Account #!");
                txtAccountNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtsuppcode.Text))
            {
                DisplayMessage("Please select supplier code", 2);
//ErrorAlert("Please select Supplier code !");
                txtsuppcode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTradeT.Text))
            {
                DisplayMessage("Please select trade term", 2);
                //ErrorAlert("Please select Trade Term !");
                txtTradeT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lblSCurrancy.Text))
            {
                DisplayMessage("Currancy code empty", 2);
                //ErrorAlert("Currancy code Empty !");
                return false;
            }
            if (string.IsNullOrEmpty(txtERate.Text))
            {
                DisplayMessage("Exchange rate empty", 2);
                //ErrorAlert("Exchange Rate Empty !");
                return false;
            }
            if (string.IsNullOrEmpty(txtTFAmount.Text))
            {
                DisplayMessage("Supplier amount empty", 2);
                //ErrorAlert("Supplier Amount Empty!");
                txtTFAmount.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCLKRTotal.Text))
            {
                DisplayMessage("Total LC amount by dealing currency empty", 2);
                //ErrorAlert("Total LC Amount by Dealing Currency Empty!");
                txtCLKRTotal.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtBank.Text))
            {
                DisplayMessage("Please select bank", 2);
                //ErrorAlert("Please select Bank !");
                txtBank.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text))
            {
                DisplayMessage("Please select bank account no", 2);
                //ErrorAlert("Please select Bank account no !");
                txtAccountNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtUtilityV.Text))
            {
                DisplayMessage("Please fill utility value", 2);
                //ErrorAlert("Please Fill Utility Value !");
                txtUtilityV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtICompany.Text))
            {
                DisplayMessage("Please select insurance company", 2);
                //ErrorAlert("Please select Insurance Company !");
                txtICompany.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtIPloicyNo.Text))
            {
                DisplayMessage("Please select insurance policy number ", 2);
                //ErrorAlert("Please select Insurance Policy # !");
                txtIPloicyNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtIpolicyDate.Text))
            {
                DisplayMessage("Please select insurance policy date ", 2);
                //ErrorAlert("Please select Insurance Policy Date !");
                txtIpolicyDate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtIsumInterest.Text))
            {
                DisplayMessage("Insurance amount require ", 2);
                //ErrorAlert("Insurance Amount Require !");
                txtIsumInterest.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtIPremium.Text))
            {
                DisplayMessage("Insurance premium require", 2);
                //ErrorAlert("Insurance Premium Require!");
                txtIPremium.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtADate.Text))
            {
                DisplayMessage("Please select amendmentm date", 2);
                //ErrorAlert("Please select Amendmentm Date !");
                txtADate.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtCPeriod.Text))
            //{
            //    DisplayMessage("Creadit period require", 2);
            //    //ErrorAlert("Creadit Period Require !");
            //    txtCPeriod.Focus();
            //    return false;
            //}
            //if (!IsNumeric(txtIPloicyNo.Text.Trim(), NumberStyles.Float))
            //{
            //    DisplayMessage("Please enter valid policy number", 2);
            //    txtIPloicyNo.Focus();
            //    return false;
            //}
            if (ddlPaymentTerms.SelectedItem.Text=="LC")
            {
                if (string.IsNullOrEmpty(txtShipDate.Text))
                {
                    DisplayMessage("Please select the Latest Shipment Date", 2);
                    //ErrorAlert("Please select the Payment Term!");
                    txtShipDate.Focus();
                    return false;
                }
            }
            return true;
        }

        private void GetIMP_FIN_HDR(string _doc, out bool _cancel)
        {
            txtEDate.Text = "";
            _cancel = true;
            searchparam = "HDR";
            string _subpayterm = "";
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Financial);
            _result = CHNLSVC.CommonSearch.SEARCH_FIN_ByID(SearchParams, _doc);
            if (_result.Rows.Count > 0)
            {
                txtBankRefno.Text = _result.Rows[0][6].ToString();
                // txt.Text = _result.Rows[0][6].ToString();
                txtfileNo.Text = _result.Rows[0][7].ToString();
                txtManualRefNo.Text = _result.Rows[0][8].ToString();
                string Status = _result.Rows[0][29].ToString();
                if (Status == "C")
                {
                    _cancel = false;
                    return;
                }
                lblSeqno.Text = _result.Rows[0][0].ToString();
                txtDocNo.Text = _result.Rows[0][1].ToString();
                ddlPaymentTerms.Text = _result.Rows[0][4].ToString();
                ddlSubpaymentTernDataPopulate("IPM", ddlPaymentTerms.Text);

                _subpayterm = _result.Rows[0][5].ToString();
                if (!string.IsNullOrEmpty(_subpayterm))
                {
                    ddlSubpaymentTerms.Text = _result.Rows[0][5].ToString();
                }
                else
                {
                    
                }
             
                txtPIDate.Text = Convert.ToDateTime(_result.Rows[0][9].ToString()).ToShortDateString();
                txtEDate.Text = Convert.ToDateTime(_result.Rows[0][10].ToString()).ToShortDateString();
                DateTime _dtTmp = new DateTime(),_dtDate =DateTime.MinValue;
                _dtDate = DateTime.TryParse(_result.Rows[0]["IF_LATEST_SH_DT"].ToString(), out _dtTmp) ? Convert.ToDateTime(_result.Rows[0]["IF_LATEST_SH_DT"].ToString()) : DateTime.MinValue;
                if (_dtDate.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                {
                    txtShipDate.Text = Convert.ToDateTime(_result.Rows[0]["IF_LATEST_SH_DT"].ToString()).ToShortDateString();
                }
                txtsuppcode.Text = _result.Rows[0][12].ToString();
                DataTable _tblname = CHNLSVC.CustService.GetSupplierDetails(txtsuppcode.Text, "ABL");
                if (_tblname.Rows.Count > 0)
                {
                    txtSName.Text = _tblname.Rows[0][5].ToString();
                }

                txtTradeT.Text = _result.Rows[0][14].ToString();
                lblSCurrancy.Text = _result.Rows[0][15].ToString();
                txtERate.Text = _result.Rows[0][16].ToString();
                txtcTotal.Text = _result.Rows[0][17].ToString();
                txtcTotal.Text = Convert.ToDecimal(txtcTotal.Text).ToString("#,##0.00");
                txtCLKRTotal.Text = _result.Rows[0][18].ToString();
                txtCLKRTotal.Text = Convert.ToDecimal(txtCLKRTotal.Text).ToString("#,##0.00");
                lblSamount.Text = _result.Rows[0][18].ToString();
                txtTFAmount.Text = _result.Rows[0][18].ToString();
                txtTFAmount.Text = Convert.ToDecimal(txtTFAmount.Text).ToString("#,##0.00");
                txtAamount.Text = _result.Rows[0][18].ToString();
                txtAamount.Text = Convert.ToDecimal(txtAamount.Text).ToString("#,##0.00");
                // txtCPeriod.Text = _result.Rows[0][18].ToString();
                Session["TFacilityAmount"] = txtTFAmount.Text;

                txtBank.Text = _result.Rows[0][20].ToString();
                txtAccountNo.Text = _result.Rows[0][21].ToString();
                string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);

                DataTable _resultF = CHNLSVC.CommonSearch.GetBankAccountFacility(SearchParams2, txtAccountNo.Text);
                if (_resultF.Rows.Count > 0)
                {
                    txtFLimit.Text = _resultF.Rows[0]["FacilityAmount"].ToString();
                    txtFLimit.Text = Convert.ToDecimal(txtFLimit.Text).ToString("#,##0.00");
                }
                txtUtilityV.Text = _result.Rows[0][23].ToString();
                txtICompany.Text = _result.Rows[0][24].ToString();
                txtIPloicyNo.Text = _result.Rows[0][25].ToString();
                txtIpolicyDate.Text = Convert.ToDateTime(_result.Rows[0][26].ToString()).ToShortDateString();
                txtIsumInterest.Text = _result.Rows[0][27].ToString();
                txtIsumInterest.Text = Convert.ToDecimal(txtIsumInterest.Text).ToString("#,##0.00");
                txtIPremium.Text = _result.Rows[0][28].ToString();
                txtIPremium.Text = Convert.ToDecimal(txtIPremium.Text).ToString("#,##0.00");
                txtbranch.Text = _result.Rows[0][33].ToString();
               

                Session["Status"] = Status;
                lblStatus.Text = string.Empty;
                if (Status == "S")
                {
                    bool value = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16001);
                    if (value == true)
                    {
                        lbtnUpdate.Enabled = true;
                        lbtnUpdate.OnClientClick = "UpdateConfirm();";
                        lbtnUpdate.CssClass = "buttonUndocolor";

                        lbtnApproval.Enabled = true;
                        lbtnApproval.OnClientClick = "ApprovalConfirm();";
                        lbtnApproval.CssClass = "buttonUndocolor";

                        lbtnCancel.Enabled = true;
                        lbtnCancel.OnClientClick = "CancelConfirm();";
                        lbtnCancel.CssClass = "buttonUndocolor";

                        lbtnAdd.Enabled = false;
                        lbtnAdd.OnClientClick = "return Enable();";
                        lbtnAdd.CssClass = "buttoncolor";
                    }
                    else
                    {
                        lbtnUpdate.Enabled = true;
                        lbtnUpdate.OnClientClick = "UpdateConfirm();";
                        lbtnUpdate.CssClass = "buttonUndocolor";


                        lbtnCancel.Enabled = false;
                        lbtnCancel.OnClientClick = "return Enable();";
                        lbtnCancel.CssClass = "buttoncolor";

                        lbtnAdd.Enabled = false;
                        lbtnAdd.OnClientClick = "return Enable();";
                        lbtnAdd.CssClass = "buttoncolor";
                    }
                   // VisibleButton();
                    lblStatus.Text = "Saved";
                }
                if (Status == "A")
                {
                    bool value = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16001);
                    if (value == true)
                    {
                        //lbtnUpdate.Enabled = true;
                        //lbtnUpdate.OnClientClick = "return Enable();;";
                        //lbtnUpdate.CssClass = "buttoncolor";

                        lbtnCancel.Enabled = true;
                        lbtnCancel.OnClientClick = "CancelConfirm();";
                        lbtnCancel.CssClass = "buttonUndocolor";

                        lbtnAdd.Enabled = false;
                        lbtnAdd.OnClientClick = "return Enable();";
                        lbtnAdd.CssClass = "buttoncolor";

                        lbtnUpdate.Enabled = true;
                        lbtnUpdate.OnClientClick = "UpdateConfirm();";
                        lbtnUpdate.CssClass = "buttonUndocolor";

                    }
                    else
                    {
                        lbtnUpdate.Enabled = false;
                        lbtnUpdate.OnClientClick = "return Enable();";
                        lbtnUpdate.CssClass = "buttoncolor";

                        lbtnCancel.Enabled = false;
                        lbtnCancel.OnClientClick = "return Enable();";
                        lbtnCancel.CssClass = "buttoncolor";

                        lbtnAdd.Enabled = false;
                        lbtnAdd.OnClientClick = "return Enable();";
                        lbtnAdd.CssClass = "buttoncolor";
                    }
                    //VisiblefalseButton();






                    lblStatus.Text = "Approved";
                }
                if (Status == "C")
                {
                    VisiblefalseButton();
                    lbtnAdd.Enabled = false;
                    lbtnAdd.OnClientClick = "return Enable();";
                    lbtnAdd.CssClass = "buttoncolor";
                    lblStatus.Text = "Cancelled";
                }
                txtCPeriod.Text = _result.Rows[0][37].ToString();

                string SearchParams1 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable CompanyCurrancytbl = CHNLSVC.CommonSearch.SearchCompanyCurrancy(SearchParams1);
                if (CompanyCurrancytbl.Rows.Count > 0)
                {
                    lblCompanyCurrancy.Text = CompanyCurrancytbl.Rows[0]["CURRENCY"].ToString();
                }
            }
            UserPopoup.Hide();
            searchparam = string.Empty;
        }
        private void GetIMP_FIN_PI(string _doc)
        {
            searchparam = "PI";
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Financial);
            _result = CHNLSVC.CommonSearch.SEARCH_FIN_ByID(SearchParams, _doc);



            grdPid.DataSource = _result;
            grdPid.DataBind();
            //DataTable NewAmdPI = new DataTable();

            //for (int i = 0; i < _result.Rows.Count; i++)
            //{
            //    int value = Convert.ToInt32(_result.Rows[i]["ifp_is_pi_amd"]);
            //    string _PI = _result.Rows[i]["IP_PI_NO"].ToString();
            //    if (value == 1)
            //    {
            //        DataTable tbl = CHNLSVC.Financial.GetDPIByPIID(_PI);
            //        NewAmdPI = tbl.Clone();
            //        foreach (DataRow dr in tbl.Rows)
            //        {
            //            NewAmdPI.Rows.Add(dr.ItemArray);
            //            _result.Rows.Remove(_result.Rows[i]);
            //        }

            //    }

            //}   

            //foreach (GridViewRow row in grdPid.Rows)
            //{
            //    Label MyLabel = (Label)row.FindControl("ifp_is_pi_amd");
            //    if (MyLabel.Text == "1")
            //    {
            //        DataRow dr = _result.NewRow();
            //        //dr = (row.DataBind as DataRowView).Row;
            //        //dr.SetParentRow();
            //        dr["IP_PI_NO"] = row.Cells[0].Text;
            //        //dr["ip_pi_dt"] =Convert.ToDateTime(row.Cells[1].Text);
            //        _result.Rows.Add(dr);
            //    }
            //}

            UserPopoup.Hide();

            // txtPTotal.Text = _result.Rows[0][5].ToString();
            searchparam = string.Empty;
            ViewState["PITable"] = _result;
            ViewState["OldPITable"] = _result;
            totalPIAmountcalcualte();
            //foreach (DataRow dr in _result.Rows)
            //{
            //    string PID = dr[0].ToString();
            //    PIItem(PID);
            //    PIKitItem(PID);
            //}
           // PIItem(PID);
           // PIKitItem(PID);

        }
        private void totalPIAmountcalcualte()
        {
            total = 0;
            DataTable dt = ViewState["PITable"] as DataTable;
            grdPid.DataBind();
            foreach (DataRow dr in dt.Rows)
            {
                total += Convert.ToDecimal(dr["IP_TOT_AMT"]);
            }
           // txtPTotal.Text = total.ToString();
            txtPTotal.Text = Convert.ToDecimal(total).ToString("#,##0.00");

        }
        private void GetIMP_FIN_AMD(string _doc)
        {
            searchparam = "AMD";
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Financial);
            _result = CHNLSVC.CommonSearch.SEARCH_FIN_ByID(SearchParams, _doc);
            grdADetails.DataSource = _result;
            grdADetails.DataBind();
            UserPopoup.Hide();
            searchparam = string.Empty;
            foreach (DataRow dr in _result.Rows)
            {
                object value = dr[0];
                if (value == DBNull.Value)
                { }
                else
                {
                    int accountLevel = Convert.ToInt32(dr[0].ToString());
                    int AmdNo = Math.Max(LineNo, accountLevel);
                    lblAmdNo.Text = (AmdNo + 1).ToString();
                }

            }
        }
        private void GetIMP_FIN_COST(string _doc)
        {
            searchparam = "COST";
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Financial);
            _result = CHNLSVC.CommonSearch.SEARCH_FIN_ByID(SearchParams, _doc);
            grdCDetails.DataSource = _result;
            grdCDetails.DataBind();
            UserPopoup.Hide();
            searchparam = string.Empty;
            ViewState["costTable"] = _result;
            ViewState["OldcostTable"] = _result;
            foreach (DataRow dr in _result.Rows)
            {
                object value = dr[3];
                if (value == DBNull.Value)
                { }
                else
                {
                    int accountLevel = Convert.ToInt32(dr[3].ToString());
                    int costNo = Math.Max(LineNo, accountLevel);
                    lblCostLineNo.Text = (costNo + 1).ToString();
                }
            }

        }

        private void GetIMP_FIN_PAY(string _doc)
        {
            searchparam = "PAY";
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Financial);
            _result = CHNLSVC.CommonSearch.SEARCH_FIN_ByID(SearchParams, _doc);
            grdPayAmount.DataSource = _result;
            grdPayAmount.DataBind();
            UserPopoup.Hide();
            ViewState["PayAmount"] = _result;
            searchparam = string.Empty;
            ViewState["Pay"] = _result;
            if ((_result != null) && (_result.Rows.Count > 0))
            {
                lblPaylineno.Text = _result.Rows[0]["ify_line"].ToString();
            }


        }
        private void CommenSearch()
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            _result = CHNLSVC.CommonSearch.SEARCH_FIN_HDR(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();

            BindUCtrlDDLData(_result);
            lblvalue.Text = "Commen";
            UserPopoup.Show();
        }

        private void PIItem(string _PID)
        {
            if (ViewState["PIITEM"] == null)
            {
                SPITemplate();
            }
            DataTable Itemtbl = ViewState["PIITEM"] as DataTable;
            //  DataTable Itemtbl = new DataTable();

            _ImportPIDetails = CHNLSVC.Financial.GET_IMP_PIITEM(_PID);
            for (int i = 0; i < _ImportPIDetails.Count; i++)
            {
                DataRow dr = Itemtbl.NewRow();
                dr[0] = _ImportPIDetails[i].IPI_ITM_CD;
                dr[1] = _ImportPIDetails[i].IPI_QTY;
                Itemtbl.Rows.InsertAt(dr, i);
            }

            grdItemResult.DataSource = Itemtbl;
            grdItemResult.DataBind();
            ViewState["PIITEM"] = Itemtbl;
        }

        private void PIKitItem(string _PID)
        {
            if (ViewState["PIKitITEM"] == null)
            {
                SPIKitTemplate();  
            }
           
          
            DataTable Itemtbl = ViewState["PIKitITEM"] as DataTable;
            //  DataTable Itemtbl = new DataTable();

            _ImportPIKit = CHNLSVC.Financial.GET_IMP_PIKITITEM(_PID);
            for (int i = 0; i < _ImportPIKit.Count; i++)
            {
                DataRow dr = Itemtbl.NewRow();
                dr[0] = _ImportPIKit[i].IPK_ITM_CD;
                dr[1] = _ImportPIKit[i].IPK_QTY;
                Itemtbl.Rows.InsertAt(dr, i);
            }
            ViewState["PIKitITEM"] = Itemtbl;
          
        }
        #endregion

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }

        protected void lbtnPiNo_Click(object sender, EventArgs e)
        {
            if (txtFLimit.Text == "0")
            {
                DisplayMessage("Please select Account number", 2);
                //ErrorAlert("Please select Facility Limit");
                return;
            }
            clearMsg();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            // string company=Session["UserCompanyCode"].ToString();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PINO);
            _result = CHNLSVC.CommonSearch.Get_All_PINo(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "400";
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        #region ModalPopup
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Commen")
            {
                CommenSearch();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "400")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PINO);
                _result = CHNLSVC.CommonSearch.Get_All_PINo(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "63")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
                _result = CHNLSVC.CommonSearch.SearchBank(SearchParams);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                lblvalue.Text = "63";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "405")
            {
                string company = Session["UserCompanyCode"].ToString();
                _result = CHNLSVC.Financial.GetCost(company, "IPM", ddlPaymentTerms.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                lblvalue.Text = "405";
                UserPopoup.Show();
                return;
            }
            else if (lblvalue.Text == "21")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                _result = CHNLSVC.CommonSearch.SearchBank(SearchParams);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
           // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            //clear();
            string PID = grdResult.SelectedRow.Cells[1].Text;
            string Name = grdResult.SelectedRow.Cells[2].Text;
            if (lblvalue.Text == "400")
            {
               
                txtCost.Text = "COST";
                PIDetailLoad(PID);
                if (Session["FLimit"].ToString() == "false")
                {
                    DisplayMessage("Facility limit not enough", 2);
                    return;
                }
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                GetCompanyandSupplierCurrancy();
                bool reult = false;
                ExchangeRate(out reult);
                if (reult == false)
                {
                    DisplayMessage("Please set exchange rate ", 2);
                    return;
                }
                costtablenew = true;
                // costtablenew = true;
                TmpCostDatatable();
               
                decimal Total = decimal.Parse(txtERate.Text) * decimal.Parse(txtcTotal.Text);
                decimal ReduceTotal4degit = Math.Round(Total, 4);
               // txtCLKRTotal.Text = ReduceTotal4degit.ToString();
                txtCLKRTotal.Text = Convert.ToDecimal(ReduceTotal4degit).ToString("#,##0.00");
                txtTFAmount.Text = txtCLKRTotal.Text;
                txtAamount.Text = txtPTotal.Text;
                txtAamount.Text = Convert.ToDecimal(txtPTotal.Text).ToString("#,##0.00");
                lblSamount.Text = txtCLKRTotal.Text;
               // PIItem(PID);
               // PIKitItem(PID);
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "405")
            {
                if (PID == "COST")
                {
                   // DisplayMessage("cant select Cost", 2);
                    //ErrorAlert("can't select Cost");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected code is already added');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    return;
                }
                else
                {
                    txtCost.Text = PID;
                    lblvalue.Text = "";
                }
                
            }
            else if (lblvalue.Text == "63")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                lblvalue.Text = "";
                txtICompany.Text = PID;
            }
            else if (lblvalue.Text == "Commen")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                lbtnAdd.Enabled = false;
                lbtnAdd.OnClientClick = "return Enable();";
                lbtnAdd.CssClass = "buttoncolor";
                bool _cost=true;
                GetIMP_FIN_HDR(PID, out _cost);
                if (_cost == true)
                {
                    GetIMP_FIN_PI(PID);
                    GetIMP_FIN_AMD(PID);
                    GetIMP_FIN_COST(PID);
                    GetIMP_FIN_PAY(PID);
                    // VisibleButton();
                    lblvalue.Text = "";
                    Session["TotalAmount"] = txtTFAmount.Text;
                }
            }
            else if (lblvalue.Text == "21")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                txtBank.Text = PID;
                txtBank.ToolTip = Name;
                bankDetails();
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "22")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                txtAccountNo.Text = PID;
                string balance = grdResult.SelectedRow.Cells[4].Text;
                txtFLimit.Text = balance;
                txtFLimit.Text = Convert.ToDecimal(txtFLimit.Text).ToString("#,##0.00");
                string Uvalue = grdResult.SelectedRow.Cells[3].Text;
                txtUtilityV.Text = Uvalue;
                txtbranch.Text = grdResult.SelectedRow.Cells[6].Text;
                //GetFacilityAmout(txtAccountNo.Text);
                lblvalue.Text = "";
            }

        }

        private void bankDetails(){
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetBankAccountFacility(SearchParams, "");
            string expression;
            expression = "Default=1";
            DataRow[] foundRows;

            // Use the Select method to find all rows matching the filter.
            foundRows = _result.Select(expression);

            // Print column 0 of each returned row.
            for (int i = 0; i < foundRows.Length; i++)
            {
                txtAccountNo.Text = foundRows[i][0].ToString();
                txtbranch.Text = foundRows[i][6].ToString();
                txtFLimit.Text = foundRows[i][3].ToString();
                txtUtilityV.Text =foundRows[i][2].ToString();
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "400")
            {
                FilterData();
                //string name = dataView[index]["Name"] as string;
                //  string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PINO);
                //  _result = CHNLSVC.CommonSearch.Get_All_PINo(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());                           
            }
            else if (lblvalue.Text == "405")
            {
                FilterData();
            }
            else if (lblvalue.Text == "Commen")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                _result = CHNLSVC.CommonSearch.SEARCH_FIN_HDR(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "63")
            {
                FilterData();
            }
            else if (lblvalue.Text == "21")
            {
                FilterData();
            }
            else if (lblvalue.Text == "22")
            {
                FilterData();
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "400")
            {
                FilterData();
            }
            else if (lblvalue.Text == "405")
            {
                FilterData();
            }
            else if (lblvalue.Text == "Commen")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                _result = CHNLSVC.CommonSearch.SEARCH_FIN_HDR(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "63")
            {
                FilterData();
            }
            else if (lblvalue.Text == "21")
            {
                FilterData();
            }
            else if (lblvalue.Text == "22")
            {
                FilterData();
            }
        }

        #endregion

       
        #region menu
        protected void lbtnSDetails_Click(object sender, EventArgs e)
        {
            SupplierPopoup.Show();
        }
        #endregion


        protected void lbtnInSearch_Click(object sender, EventArgs e)
        {
            clearMsg();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            // string company=Session["UserCompanyCode"].ToString();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
            _result = CHNLSVC.CommonSearch.SearchBank(SearchParams);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "63";
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtncostSearch_Click(object sender, EventArgs e)
        {
            clearMsg();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string company = Session["UserCompanyCode"].ToString();
            // string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.costtype);
            _result = CHNLSVC.Financial.GetCost(company, "IPM", ddlPaymentTerms.Text);
            // _result = CHNLSVC.CommonSearch.SearchCostType(SearchParams);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "405";
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnCAdd_Click(object sender, EventArgs e)
        {
            clearMsg();
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16041))
            {
                string Msg = "You dont have permission to Cost Involvement Details .Permission code : 16041";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
                return;
            }
            if (!(grdPid.Rows.Count > 0))
            {
                 DisplayMessage("Please add PI details", 2);
                //ErrorAlert("Please Add PI Details");
                return;
            }
            if (txtFLimit.Text == string.Empty)
            {
                DisplayMessage("Please select facility amount", 2);
                //ErrorAlert("Please select Facility Amount");
                return;
            }
            if (txtCost.Text == string.Empty)
            {
                DisplayMessage("Please select cost type", 2);
                //ErrorAlert("Please Select Cost Type");
                return;
            }
            if (txtLAmount.Text == string.Empty)
            {
                DisplayMessage("Please type cost amount", 2);
                //ErrorAlert("Please Type Cost Amount");
                return;
            }
            if (txtCost.Text == "COST")
            {
                DisplayMessage("Please select different cost type", 2);
                //ErrorAlert("Please Select different Cost Type");
                return;
            }
            if (!IsNumeric(txtAmount.Text.Trim(), NumberStyles.Float))
            {
                DisplayMessage("Please enter valid  amount ", 2);
                txtAmount.Focus();
                return;
            }
            MergCostDatatable();
            txtLAmount.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtCost.Text = string.Empty;
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
           

            if (validation())
            {
                if (txtSavelconformmessageValue.Value == "Yes")
                {
                    decimal totalNEW = Convert.ToDecimal(txtTFAmount.Text);
                    decimal currntFacilityam = Convert.ToDecimal(txtFLimit.Text);
                    if (currntFacilityam < totalNEW) {
                        DisplayMessage("Facility Limit not enough", 2);
                        //ErrorAlert("Facility Limit not enough"); 
                        return; }

                    SaveOrderFinancing();
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                }
                txtSavelconformmessageValue.Value = null;
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtERate.Text))
            {
                DisplayMessage("Please select PI", 2);
                //ErrorAlert("Please select PI");
                return;
            }
            if (!IsNumeric(txtAmount.Text.Trim(), NumberStyles.Float))
            {
                DisplayMessage("Please enter valid number for qty", 2);
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid number for qty !!!')", true);
                txtAmount.Focus();
                return ;
            }
            decimal amount = decimal.Parse(txtAmount.Text);
            decimal Total = decimal.Parse(txtERate.Text) * amount;
            decimal ReduceTotal4degit = Math.Round(Total, 4);
            txtLAmount.Text = ReduceTotal4degit.ToString();
            txtAmount.Text = amount.ToString();
        }

        protected void lbtnDocSearch_Click(object sender, EventArgs e)
        {
            CommenSearch();
            ddlSearchbykey.SelectedIndex = 0;
        }

        protected void lbtnBankRefSearch_Click(object sender, EventArgs e)
        {
            CommenSearch();
            ddlSearchbykey.SelectedIndex = 4;
        }

        protected void lbtnFileSearch_Click(object sender, EventArgs e)
        {
            CommenSearch();
            ddlSearchbykey.SelectedIndex = 5;
        }

        protected void lbtnMRefSearch_Click(object sender, EventArgs e)
        {
            CommenSearch();
            ddlSearchbykey.SelectedIndex = 6;
        }

        protected void grdPid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "lbtnPDelete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                grdPid.DeleteRow(id);
            }
        }

        protected void grdPid_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {



        }

        protected void grdPid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            //if (Session["TotalAmount"] == txtTFAmount.Text)
            //{
            if (validation())
            {
                if (txtUpdateconformmessageValue.Value == "Yes")
                {
                    decimal totalNEW =  Convert.ToDecimal(txtTFAmount.Text);
                    decimal currntFacilityam = Convert.ToDecimal(txtFLimit.Text);
                    if (currntFacilityam < totalNEW) {
                        DisplayMessage("Facility Limit not enough", 2);
                        //ErrorAlert("Facility Limit not enough");
                        return; }
                    UpdateOrderFinancing();

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                }
                txtUpdateconformmessageValue.Value = null;
            }
            //}
            //else
            //{
            //    ErrorAlert("You not change ");
            //}

        }

        protected void grdPid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ifp_is_pi_amd")) == "1")
                {
                    if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "IFP_ACT")) == "1")
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                        lbtnAmendPI.Visible = true;
                        lbtnUpdate.Enabled = false;
                        lbtnUpdate.OnClientClick = "return Enable();";
                        lbtnUpdate.CssClass = "buttoncolor";

                    }
                }



            }
        }

        protected void lbtnAmendPI_Click(object sender, EventArgs e)
        {
            DataTable NewAmdPI = new DataTable();
            // DataTable oldPI = ViewState["PITable"] as DataTable;
            ViewState["OLDPITable"] = ViewState["PITable"];
            DataTable dt = ViewState["PITable"] as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int value = Convert.ToInt32(dt.Rows[i]["ifp_is_pi_amd"]);
                string _PI = dt.Rows[i]["IP_PI_NO"].ToString();
                if (value == 1)
                {
                    // PIDetailLoad(_PI);
                    DataTable tbl = CHNLSVC.Financial.GetDPIByPIID(_PI);
                    NewAmdPI = tbl.Clone();
                    foreach (DataRow dr in tbl.Rows)
                    {

                        NewAmdPI.Rows.Add(dr.ItemArray);
                        dt.Rows.Remove(dt.Rows[i]);
                    }

                    dt.Merge(NewAmdPI);
                    grdPid.DataSource = dt;
                    grdPid.DataBind();

                    lbtnUpdate.Enabled = true;
                    lbtnUpdate.OnClientClick = "UpdateConfirm();";
                    lbtnUpdate.CssClass = "buttonUndocolor";
                    ViewState["NewAmendPI"] = NewAmdPI;
                }

            }

        }

        protected void ddlPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubpaymentTernDataPopulate("IPM", ddlPaymentTerms.Text);
            string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PaymentTerms);
            DataTable result2 = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams2, "Code", ddlPaymentTerms.Text);

            if (result2 != null)
            {
                ddlPaymentTerms.ToolTip = result2.Rows[0][1].ToString();
            }

            if((ddlPaymentTerms.Text=="DA") || (ddlPaymentTerms.Text=="OA"))
            {
                txtCPeriod.ReadOnly = false;
                return;
            }
            txtCPeriod.ReadOnly=true;
        }

        protected void lbtnBankSearch_Click(object sender, EventArgs e)
        {

            clearMsg();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            // string company=Session["UserCompanyCode"].ToString();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            _result = CHNLSVC.CommonSearch.SearchBank(SearchParams);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "21";
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnAccountSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBank.Text))
            {
                DisplayMessage("Please select the bank", 2);
                //ErrorAlert("Please select the Bank !");
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            _result = CHNLSVC.CommonSearch.GetBankAccountFacility(SearchParams,"");
            _result.Columns.Remove("Default");
            grdResult.DataSource = _result;
            grdResult.DataBind();
           // _result.Columns.Remove("Default");
            _result.Columns.Remove("Utility Value");
            _result.Columns.Remove("Rate %");
            _result.Columns.Remove("Branch");
            lblvalue.Text = "22";
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
            ViewState["SEARCH"] = _result;
        }

        protected void lbtnVItem_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtPiItem.ClientID + "').value = '';", true);
            ViewState["PIITEM"] = null;
            _result = ViewState["PITable"] as DataTable;
            if (_result == null)
            {
                DisplayMessage("Please select PI", 2);
                //ErrorAlert("Please Select PI");
                return;
            }
            foreach (DataRow dr in _result.Rows)
            {
                string PID = dr[0].ToString();
                PIItem(PID);
            }
            DataTable Itemtbl = ViewState["PIITEM"] as DataTable;
            grdItemResult.DataSource = Itemtbl;
            grdItemResult.DataBind();           
            ItemPopup.Show();
        }

        protected void txtPiItem_TextChanged(object sender, EventArgs e)
        {
            if (!(txtPiItem.Text == ""))
            {
                ViewState["PIITEM"] = null;
                PIItem(txtPiItem.Text);
            }
            DataTable Itemtbl = ViewState["PIITEM"] as DataTable;
            grdItemResult.DataSource = Itemtbl;
            grdItemResult.DataBind();
            ItemPopup.Show();
        }
        protected void lbtnPiItemSearch_Click(object sender, EventArgs e)
        {
            if (!(txtPiItem.Text == ""))
            {
                ViewState["PIITEM"] = null;
                PIItem(txtPiItem.Text);
            }
            DataTable Itemtbl = ViewState["PIITEM"] as DataTable;
            grdItemResult.DataSource = Itemtbl;
            grdItemResult.DataBind();
            ItemPopup.Show();
        }
     

        protected void lbtnApproval_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16001))
            {
                // MessageBox.Show("Sorry, You have no permission to approve this document!\n( Advice: Required permission code : 16001)");
                return;
            }
            decimal totalNEW = Convert.ToDecimal(txtTFAmount.Text);
            decimal currntFacilityam = Convert.ToDecimal(txtFLimit.Text);
            if (currntFacilityam < totalNEW) {
                //ErrorAlert("Facility Limit not enough");
                DisplayMessage("Facility Limit not enough", 2);
                return;
            }

            if (txtApprovalconformmessageValue.Value == "Yes")
            {
                string err = string.Empty;
                txtSavelconformmessageValue.Value = null;
                Int32 row_aff = 0;
                _Order.If_doc_no = txtDocNo.Text;
                _Order.If_stus = "A";
                _Order.If_com = Session["UserCompanyCode"].ToString();
                _Order.If_bank_cd = txtBank.Text;
                _Order.If_bank_acc_no = txtAccountNo.Text;
                _Order.If_tot_amt_deal =Convert.ToDecimal(txtTFAmount.Text);
                row_aff = CHNLSVC.Financial.Update_Approval_Cancel(_Order,out err);
                if (row_aff > 0)
                {
                    DisplayMessage("Succesfully approved", 3);
                   // SuccessAlert("Succesfully Approval.. !");
                    lblStatus.Text = "Approved";
                }
                else
                {
                    DisplayMessage(err, 4);
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }

        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16002))
            {
                // MessageBox.Show("Sorry, You have no permission to approve this document!\n( Advice: Required permission code : 16001)");
                return;
            }
            try
            {
                if (txtCancelconformmessageValue.Value == "Yes")
                {
                    string err = string.Empty;
                    Int32 row_aff = 0;
                    _Order.If_doc_no = txtDocNo.Text;
                    _Order.If_stus = "C";
                    row_aff = CHNLSVC.Financial.Update_Approval_Cancel(_Order, out err);
                    if (row_aff > 0)
                    {
                        DisplayMessage("Succesfully cancelled", 3);
                        //SuccessAlert("Succesfully Canceled.. !");
                        lblStatus.Text = "Cancelled";
                    }
                    else
                    {
                        DisplayMessage(err, 4);
                    }
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                }


            }
            catch (Exception ex)
            {

            }


        }

        protected void lbtnVKitItem_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtKitItemSearch.ClientID + "').value = '';", true);
            ViewState["PIKitITEM"] = null;
            _result = ViewState["PITable"]  as DataTable;
            if (_result == null)
            {
                DisplayMessage("Please select PI", 2);
               // ErrorAlert("Please Select PI..!");
                return;
            }
            foreach (DataRow dr in _result.Rows)
            {
                string PID = dr[0].ToString();
                PIKitItem(PID);
            }
            DataTable Itemtbl = ViewState["PIKitITEM"] as DataTable;
            grdKitItem.DataSource = Itemtbl;
            grdKitItem.DataBind();
            KitItemPopup.Show();
        }

        protected void txtKitItemSearch_TextChanged(object sender, EventArgs e)
        {
            if (!(txtKitItemSearch.Text == ""))
            {
                ViewState["PIKitITEM"] = null;
                PIKitItem(txtKitItemSearch.Text);
            }
            DataTable Itemtbl = ViewState["PIKitITEM"] as DataTable;
            grdKitItem.DataSource = Itemtbl;
            grdKitItem.DataBind();
            KitItemPopup.Show();
        }

        protected void lbtnKitSearch_Click(object sender, EventArgs e)
        {
            if (!(txtKitItemSearch.Text == ""))
            {
                ViewState["PIKitITEM"] = null;
                PIKitItem(txtKitItemSearch.Text);
            }
            DataTable Itemtbl = ViewState["PIKitITEM"] as DataTable;
            grdKitItem.DataSource = Itemtbl;
            grdKitItem.DataBind();
            KitItemPopup.Show();
        }

        protected void txtCPeriod_TextChanged(object sender, EventArgs e)
        {
            int distance;
            if (int.TryParse(txtCPeriod.Text, out distance))
            {
                return;
            }
            clearMsg();
            DisplayMessage("Please enter a valid credit period", 2);
           // ErrorAlert("only allow Numeric Value!");
        }

        protected void txtUtilityV_TextChanged(object sender, EventArgs e)
        {
            int distance;
            if (int.TryParse(txtUtilityV.Text, out distance))
            {            
                return;
            }
            clearMsg();
           // ErrorAlert("only allow Numeric Value!");
            DisplayMessage("only allow numeric value", 2);
        }

        protected void txtIsumInterest_TextChanged(object sender, EventArgs e)
        {
            int distance;
            if (int.TryParse(txtIsumInterest.Text, out distance))
            {              
                return;
            }
            clearMsg();
           // ErrorAlert("only allow Numeric Value!");
            DisplayMessage("only allow numeric value", 2);
        }

        protected void lbtnFLimit_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FacilityAmout);
            _result = CHNLSVC.CommonSearch.SearchFacilityLimit(SearchParams);
            grdFLimit.DataSource = _result;
            grdFLimit.DataBind();

            facilitypopup.Show();
        }

        protected void lbtnsettlepayment_Click(object sender, EventArgs e)
        {
            if (lblSamount.Text == "")
            {
                DisplayMessage("Please select PI", 2);
                //ErrorAlert("Please select PI..!");
                return;
            }
            DataTable _res = ViewState["PayAmount"] as DataTable;
            decimal sum = 0;
            if (_res != null)
            {
                foreach (DataRow r in _res.Rows)
                {
                    sum += Convert.ToDecimal(r[3].ToString());
                }
            }
                       
            decimal av = Convert.ToDecimal(lblSamount.Text) - sum;
            lblAamount.Text = av.ToString();

        

            paypopup.Show();
        }

        private void DeleteCoste(string ctype)
        {
            //_OrderFinancingcost.Ifc_act = false;
            //_OrderFinancingcost.Ifc_doc_no = txtDocNo.Text;
            //_OrderFinancingcost.Ifc_ele_cd = ctype;
            //int row_arr = CHNLSVC.Financial.Update_Cost_Inactive(_OrderFinancingcost);
            //if (row_arr > 0)
            //{
            //    GetIMP_FIN_COST(txtDocNo.Text);

            //}
            //GetIMP_FIN_COST(txtDocNo.Text);
            DataTable tbl = (DataTable)ViewState["costTable"];
            bool _IsoldCost= false;
            for (int i = tbl.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = tbl.Rows[i];
                if (dr["CType"].ToString() == ctype)
                {
                    dr.Delete();
                    dr.AcceptChanges();
                }
            }
            tbl.AcceptChanges();
            grdCDetails.DataSource = tbl;
            grdCDetails.DataBind();
            ViewState["costTable"] = tbl;
           // MergCostDatatable();
            TotalCostcount();

            DataTable _oldTbl = ViewState["OldcostTable"] as DataTable;
            if (_oldTbl != null)
            {
                for (int i = 0; _oldTbl.Rows.Count > i; i++)
                {
                    DataRow dr = _oldTbl.Rows[i];
                    if (dr["CType"].ToString() == ctype)
                    {
                        _IsoldCost = true;
                    }

                }
            }
           
            if (_IsoldCost == true)
            {
                DataTable _DetPI = new DataTable();
                if (ViewState["DeleteCost"] == null)
                {

                    DataRow drD = null;
                    _DetPI.Columns.Add(new DataColumn("Ifc_doc_no", typeof(string)));
                    _DetPI.Columns.Add(new DataColumn("Ifc_ele_cd", typeof(string)));
                    drD = _DetPI.NewRow();
                    drD["Ifc_doc_no"] = txtDocNo.Text;
                    drD["Ifc_ele_cd"] = ctype;
                    _DetPI.Rows.Add(drD);
                }
                else
                {
                    _DetPI = ViewState["DeleteCost"] as DataTable;
                    _DetPI.Rows.Add(txtDocNo.Text, ctype);
                }
                ViewState["DeleteCost"] = _DetPI;

                // 


            }
        }
        protected void lbtnCDelete_Click(object sender, EventArgs e)
        {
            clearMsg();
            bool IsCostCheck = false;
            foreach (GridViewRow dgvr in grdCDetails.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("SEC_DEF");
                string CType = (dgvr.FindControl("CType") as Label).Text;
                if (chk != null & chk.Checked)
                {
                    if (CType == "COST")
                    {
                        DisplayMessage("Cant delete cost type please untick the COST", 2);
                        //ErrorAlert("Can't Delete Cost Type Please untick the COST!");
                        return;
                    }
                    //string confirmValue = Request.Form["DeleteConfirm_value"];
                    if (txtDeleteconformmessageValue.Value == "Yes")
                    {
                        DeleteCoste(CType);
                        // LoadUserRole();
                        IsCostCheck = true;
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Delete!')", true);
                    }
                    //txtDeleteconformmessageValue.Value = "";
                }
                
            }
            if (!(grdCDetails.Rows.Count > 0))
            {
                DisplayMessage("No data found to delete", 2);
                //ErrorAlert("No Data to Found Delete..!");
                return;
            }
            if (IsCostCheck == false)
            {
                DisplayMessage("Please select cost type to delete", 2);
                  //  ErrorAlert("Please select Cost Type to Delete!");
                return;
            }
        }

        private void DeletePI(string ctype)
        {
            DataTable _PItbl = ViewState["PITable"] as DataTable;
            bool _IsnewPI = false;
            int lineNo = 0;
            for (int i = _PItbl.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = _PItbl.Rows[i];
                if (dr["IP_PI_NO"].ToString() == ctype)
                {
                    dr.Delete();
                    dr.AcceptChanges();
                }                  
            }
            _PayType.AcceptChanges();
            grdPid.DataSource = _PItbl;
            grdPid.DataBind();
            ViewState["PITable"] = _PItbl;

            totalPIAmountcalcualte();
            txtCost.Text = "COST";
            MergCostDatatable();
            TotalCostcount();
            DataTable _oldTbl = ViewState["OldPITable"] as DataTable;
            if (_oldTbl != null)
            {
                for (int i = 0; _oldTbl.Rows.Count > i; i++)
                {
                    DataRow dr = _oldTbl.Rows[i];
                    if (dr["IP_PI_NO"].ToString() == ctype)
                    {
                        lineNo = Convert.ToInt32(dr["IFP_LINE"].ToString());
                        _IsnewPI = true;
                    }

                }
            }
           
            if (_IsnewPI== true)
            {
                DataTable _DetPI = new DataTable();
                if (ViewState["DeletePI"] == null)
                {
                    
                    DataRow drD = null;
                    _DetPI.Columns.Add(new DataColumn("Ifp_line", typeof(Int32)));
                    _DetPI.Columns.Add(new DataColumn("Ifp_doc_no", typeof(string)));
                    _DetPI.Columns.Add(new DataColumn("Ifp_pi_no", typeof(string)));
                    drD = _DetPI.NewRow();
                    drD["Ifp_line"] = lineNo;
                    drD["Ifp_doc_no"] = txtDocNo.Text;
                    drD["Ifp_pi_no"] = ctype;
                    _DetPI.Rows.Add(drD);
                }
                else
                {
                    _DetPI = ViewState["DeletePI"] as DataTable;
                    _DetPI.Rows.Add(lineNo, txtDocNo.Text, ctype);
                }
                ViewState["DeletePI"] = _DetPI;

               // 
                
               
            }
           
        }
        protected void lbtnPDelete_Click(object sender, EventArgs e)
        {
            clearMsg();
            bool IsPICheck = false;
            foreach (GridViewRow dgvr in grdPid.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("SEC_DE_PI");
                string CType = (dgvr.FindControl("IP_PI_NO") as Label).Text;
                if (chk != null & chk.Checked)
                {
                    if (txtDeleteconformmessageValue.Value == "Yes")
                    {
                        DeletePI(CType);
                        IsPICheck = true;
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Delete!')", true);
                    }
                }
                
            }
            if (!(grdPid.Rows.Count > 0))
            {
                if (IsPICheck == true)
                {
                  
                    return;
                }
                DisplayMessage("No data found to delete..", 2);
               // ErrorAlert("No Data to Found Delete..!");
                return;
            }
            if (IsPICheck == false)
            {
                DisplayMessage("Please select PI to delete..", 2);
                //ErrorAlert("Please select PI to Delete!");
                return;
            }
        }

        protected void lbtnAddPayAmount_Click(object sender, EventArgs e)
        {
            lblerror.Text = "";
            if (ddlAmountType.SelectedValue == "1")
            {
                if (txtRate.Text == "")
                {
                    lblerror.Text = "Please type Rate";
                    return;
                }
                PayTypeAdd();
                txtPayAmount.Text = "0";
                txtPayAmountLKR.Text = "0";
            }
            else if (ddlAmountType.SelectedValue == "2")
            {
                if (txtPayAmountLKR.Text == "")
                {
                    lblerror.Text = "Please type Amount";
                    return;
                }
                PayTypeAdd();
                txtPayAmount.Text = "0";
                txtPayAmountLKR.Text = "0";
            }

        }

        protected void txtPayAmount_TextChanged(object sender, EventArgs e)
        {
            decimal amount = decimal.Parse(txtPayAmount.Text);
            decimal Total = decimal.Parse(txtERate.Text) * amount;
            decimal ReduceTotal4degit = Math.Round(Total, 4);
            txtPayAmountLKR.Text = ReduceTotal4degit.ToString();
            ViewState["PaySettle"] = "t";
            paypopup.Show();
        }

        protected void ddlAmountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmountType.SelectedValue == "1")
            {
                txtRate.Visible = true;
                txtPayAmount.Text = "0";
                txtPayAmountLKR.Text = "0";
                txtPayAmount.Enabled = false;
                txtPayAmountLKR.Enabled = false;
                paypopup.Show();
                return;
            }
            txtPayAmount.Text = "0";
            txtPayAmountLKR.Text = "0";
            txtPayAmount.Enabled = true;
            txtPayAmountLKR.Enabled = true;
            txtRate.Visible = false;
            paypopup.Show();
        }

        protected void lbtnPayDelete_Click(object sender, EventArgs e)
        {
            clearMsg();
            foreach (GridViewRow dgvr in grdPayAmount.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("SEC_DEPayment");
                string CType = (dgvr.FindControl("PType") as Label).Text;
                if (chk != null & chk.Checked)
                {
                    if (txtDeleteconformmessageValue.Value == "Yes")
                    {
                        DeletePayment(CType);
                        paypopup.Show();
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Delete!')", true);
                    }
                }
                else
                {
                    lblerror.Text="Please select PI no to Delete!";
                    paypopup.Show();
                }
            }

        }

        private void DeletePayment(string Ctype)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["Pay"];
            _ImportFINPay.Ify_doc_no = txtDocNo.Text;
            _ImportFINPay.Ify_tp = Ctype;
            int row_arr = CHNLSVC.Financial.DeleteFinPay(_ImportFINPay);
            if (row_arr > 0)
            {
                GetIMP_FIN_PAY(txtDocNo.Text);

            }
            else
            {

                DataRow[] result = dtCurrentTable.Select("PType = '" + Ctype + "'");
                foreach (DataRow row in result)
                {
                    //if (row["PType"].ToString().Trim().ToUpper().Contains("MM"))
                        dtCurrentTable.Rows.Remove(row);
                }
                grdPayAmount.DataSource = dtCurrentTable;
                grdPayAmount.DataBind();
                ViewState["Pay"] = dtCurrentTable;
                ViewState["PayAmount"] = dtCurrentTable;

                decimal sum = 0;
                decimal sum2 = 0;
                if (dtCurrentTable != null)
                {
                    foreach (DataRow r in dtCurrentTable.Rows)
                    {
                        sum += Convert.ToDecimal(r[3].ToString());
                    }
                }
               
                decimal av = Convert.ToDecimal(lblSamount.Text) - sum;
                lblAamount.Text = av.ToString();
                if (ddlAmountType.SelectedValue == "1")
                {
                    lblCAmount.Text=sum.ToString();
                }
                else
                {
                    lblCPAmount.Text = sum.ToString();
                }
                //lblCAmount,lblCPAmount
            }
           // GetIMP_FIN_PAY(txtDocNo.Text);
        }

        protected void txtAccountNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSbu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ADMIN/Home.aspx");
        }


        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }

        protected void txtPTotal_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void ddlSubpaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubpaymentTerms.Text == "U")
            {
                txtCPeriod.ReadOnly = false;
                return;
            }
            txtCPeriod.ReadOnly = true;
        }

        protected void txtBank_TextChanged(object sender, EventArgs e)
        {
            MasterOutsideParty _MasterOutsideParty = new MasterOutsideParty();
            _MasterOutsideParty = CHNLSVC.Sales.GetOutSidePartyDetails(txtBank.Text,null);
            if (_MasterOutsideParty.Mbi_cd==null)
           {
               txtBank.Text = string.Empty;
               DisplayMessage("Please enter valid  Bank ", 2);
               txtBank.Focus();
               return;
           }
            bankDetails();
        }

        protected void txtBankRefno_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            _result = CHNLSVC.CommonSearch.SEARCH_FIN_HDR(SearchParams, "REFERENCE", txtBankRefno.Text);
            if(_result!=null)
            {
                if (_result.Rows.Count > 0)
                {
                    string PID = _result.Rows[0][0].ToString();
                    bool _cost = true;
                    GetIMP_FIN_HDR(PID, out _cost);
                    if (_cost)
                    {
                        GetIMP_FIN_PI(PID);
                        GetIMP_FIN_AMD(PID);
                        GetIMP_FIN_COST(PID);
                        GetIMP_FIN_PAY(PID);
                        // VisibleButton();
                        lblvalue.Text = "";
                        Session["TotalAmount"] = txtTFAmount.Text;
                    }
                }
            }
           
        }



        protected void txtfileNo_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            _result = CHNLSVC.CommonSearch.SEARCH_FIN_HDR(SearchParams, "FILE NO", txtfileNo.Text);
            if (_result != null)
            {
                if (_result.Rows.Count > 0)
                {
                    string PID = _result.Rows[0][0].ToString();
                    bool _cost = true;
                    GetIMP_FIN_HDR(PID, out _cost);
                    if (_cost)
                    {
                        GetIMP_FIN_PI(PID);
                        GetIMP_FIN_AMD(PID);
                        GetIMP_FIN_COST(PID);
                        GetIMP_FIN_PAY(PID);
                        // VisibleButton();
                        lblvalue.Text = "";
                        Session["TotalAmount"] = txtTFAmount.Text;
                    }
                }
            }

        }

        protected void lbtnShClr_Click(object sender, EventArgs e)
        {
            txtShipDate.Text = "";
        }



    }
}


