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
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient.General
{
    public partial class ExchangeRateDefinition : Base
    {
        //sp_getValid_exratedetails  =NEW


        //-------------
        //get_BaseCurrToLKR =new
        //get_BaseCurrToLKR

        private List<MasterExchangeRate> ExchangeRate;
        public List<MasterExchangeRate> _ExchangeRate
        {
            get { return ExchangeRate; }
            set { ExchangeRate = value; }
        }
        private Int32 lineNo;

        public Int32 _lineNo
        {
            get { return lineNo; }
            set { lineNo = value; }
        }
       
        public ExchangeRateDefinition()
        {
            InitializeComponent();
            _ExchangeRate = new List<MasterExchangeRate>();
            _lineNo = 0;
            ClearData();
            txtTDate.Value = Convert.ToDateTime("31-Dec-2999").Date;
            txtPC.Text = BaseCls.GlbUserDefProf;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ExchangeRateDefinition formnew = new ExchangeRateDefinition();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }
        protected void BindCurrency(ComboBox _ddl)
        {
            _ddl.Items.Clear();
            List<MasterCurrency> _Currency = CHNLSVC.General.GetAllCurrency(string.Empty);
            if (_Currency != null)
            {
                _Currency = _Currency.OrderBy(items => items.Mcr_cd).ToList();
            }            
            _ddl.DataSource = _Currency;
           // _ddl.DisplayMember = "Mcr_cd";//mcr_desc
            _ddl.DisplayMember = "Mcr_cd";
            _ddl.ValueMember = "Mcr_cd";          
        }

        private void ClearData()
        {
            BindCurrency(ddlFromCur);
            BindCurrency(ddlToCur);
           // ddlFromCur.Text = "LKR"; 
            ddlFromCur.SelectedValue = "USD";
           // ddlToCur.Text = "LKR";
            ddlToCur.SelectedValue = "LKR";
            txtBankBuy.Text = "";
            txtBankSelling.Text = "";
            txtCustom.Text = "";
            _ExchangeRate = new List<MasterExchangeRate>();
            txtFDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            txtTDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());

            txtBankBuy.Text = string.Format("{0:n2}", 0);
            txtBankSelling.Text = string.Format("{0:n2}", 0);
            txtCustom.Text = string.Format("{0:n2}", 0);
           
            DataTable _table = new DataTable();
            gvExchange.DataSource = _table;         

            ddlFromCur.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty( txtPC.Text))
            {
                MessageBox.Show("Please select profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ddlFromCur.SelectedValue.ToString().Trim() == "-" || ddlToCur.SelectedValue.ToString().Trim() == "-")
            {
                MessageBox.Show("Please select valid curency!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDateTime(txtFDate.Value).Date > Convert.ToDateTime(txtTDate.Value).Date)
            {
                MessageBox.Show("'From Date' cannot be greater than 'To Date'", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtBankSelling.Text.Trim() == "" || txtBankBuy.Text.Trim() == "" || txtCustom.Text.Trim()=="")
            {
                MessageBox.Show("Please enter all rates!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                 Convert.ToDecimal(txtBankSelling.Text);
                 Convert.ToDecimal(txtBankBuy.Text);
                 Convert.ToDecimal(txtCustom.Text);
               
            }
            catch(Exception EX){
                MessageBox.Show("Enter valid rates!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Convert.ToDecimal(txtBankSelling.Text)==0)
            {
                 //Convert.ToDecimal(txtBankSelling.Text);
                 //Convert.ToDecimal(txtBankBuy.Text);
                 //Convert.ToDecimal(txtCustom.Text);
                MessageBox.Show("Enter bank selling rate!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Convert.ToDecimal(txtBankBuy.Text) == 0)
            {
                //Convert.ToDecimal(txtBankSelling.Text);
                //Convert.ToDecimal(txtBankBuy.Text);
                //Convert.ToDecimal(txtCustom.Text);
                MessageBox.Show("Enter bank buy rate!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtCustom.Text) == 0)
            {
                //Convert.ToDecimal(txtBankSelling.Text);
                //Convert.ToDecimal(txtBankBuy.Text);
                //Convert.ToDecimal(txtCustom.Text);
                MessageBox.Show("Enter Custom rate!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _lineNo += 1;
            _ExchangeRate.Add(AssignDataToObject());

            //--------------------------------------------------
            if (ddlFromCur.SelectedValue.ToString() != "USD" && chkFromUsd.Checked==true)
            {
                //TODO: 
                DataTable DT = CHNLSVC.Financial.GetFromCurr_to_ToCurr("USD", "LKR", DateTime.Now.Date,BaseCls.GlbUserComCode);
                if (DT == null)
                {
                    MessageBox.Show("Please save " + "USD" + "to LKR first!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (DT.Rows.Count < 1)
                {
                    MessageBox.Show("Please save " + "USD" + "to LKR first!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //---------------------------------------------------------
                DT = CHNLSVC.Financial.GetFromCurr_to_ToCurr("USD", "LKR", DateTime.Now.Date, BaseCls.GlbUserComCode);
                if (DT == null)
                {
                    MessageBox.Show("Please save "+ ddlFromCur.SelectedValue.ToString() +"to LKR first!" , "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if(DT.Rows.Count<1)
                {
                    MessageBox.Show("Please save " + ddlFromCur.SelectedValue.ToString() + "to LKR first!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Decimal usd_to_lkr_RATE1 = Convert.ToDecimal(DT.Rows[0]["MER_BNKSEL_RT"].ToString());//120
                Decimal other_to_lkr_RATE1 = Convert.ToDecimal(txtBankSelling.Text); //30
                Decimal usd_to_other_RATE1 = usd_to_lkr_RATE1 / other_to_lkr_RATE1;

                //--------------------------------------
                Decimal usd_to_lkr_RATE2 = Convert.ToDecimal(DT.Rows[0]["MER_BNKBUY_RT"].ToString());//120
                Decimal other_to_lkr_RATE2 = Convert.ToDecimal(txtBankSelling.Text); //30
                Decimal usd_to_other_RATE2 = usd_to_lkr_RATE2 / other_to_lkr_RATE2;
                //-----------------------------------------
                //--------------------------------------
                Decimal usd_to_lkr_RATE3 = Convert.ToDecimal(DT.Rows[0]["MER_BNKBUY_RT"].ToString());//120
                Decimal other_to_lkr_RATE3 = Convert.ToDecimal(txtBankSelling.Text); //30
                Decimal usd_to_other_RATE3 = usd_to_lkr_RATE3 / other_to_lkr_RATE3;
                //-----------------------------------------
                //USD/ERO= 120/30
                //USD   

                //USD :LKR OK  
                // 1  : 120
                //USD : EORO
                //ERO : LKR
                // 1  : 30
                MasterExchangeRate _tempItem = new MasterExchangeRate();

                _tempItem.Mer_id = _lineNo;
                _tempItem.Mer_com = BaseCls.GlbUserComCode;
                _tempItem.Mer_cur = "USD";//ddlFromCur.SelectedValue.ToString();
                _tempItem.Mer_vad_from = Convert.ToDateTime(txtFDate.Text);
                _tempItem.Mer_vad_to = Convert.ToDateTime(txtTDate.Text);
                _tempItem.Mer_bnksel_rt = Math.Round(usd_to_other_RATE1,6);//Convert.ToDecimal(txtBankSelling.Text);
                _tempItem.Mer_bnkbuy_rt =Math.Round( usd_to_other_RATE2,6);//Convert.ToDecimal(txtBankBuy.Text);
                _tempItem.Mer_cussel_rt =Math.Round( usd_to_other_RATE3,6);//Convert.ToDecimal(txtCustom.Text);
                _tempItem.Mer_buyvad_from = Convert.ToDateTime(txtFDate.Text);
                _tempItem.Mer_buyvad_to = Convert.ToDateTime(txtTDate.Text);
                _tempItem.Mer_act = true;
                _tempItem.Mer_cre_by = BaseCls.GlbUserID;
                _tempItem.Mer_cre_dt = Convert.ToDateTime(txtTDate.Text);
                _tempItem.Mer_mod_by = BaseCls.GlbUserID;
                _tempItem.Mer_mod_dt = Convert.ToDateTime(txtTDate.Text);
                //_tempItem.Mer_session_id =BaseCls.GlbUserSessionID;
                _tempItem.Mer_to_cur = ddlFromCur.SelectedValue.ToString();
                _tempItem.Mer_pc = txtPC.Text;
                _ExchangeRate.Add(_tempItem);

                //-------------------------------------------------
                 usd_to_other_RATE1 =   other_to_lkr_RATE1/usd_to_lkr_RATE1;
                 usd_to_other_RATE2 = other_to_lkr_RATE2 / usd_to_lkr_RATE2;
                 usd_to_other_RATE3 = other_to_lkr_RATE3 / usd_to_lkr_RATE3;

                MasterExchangeRate _tempItem2 = new MasterExchangeRate();

                _tempItem2.Mer_id = _lineNo;
                _tempItem2.Mer_com = BaseCls.GlbUserComCode;
                _tempItem2.Mer_cur = ddlFromCur.SelectedValue.ToString();
                _tempItem2.Mer_vad_from = Convert.ToDateTime(txtFDate.Text);
                _tempItem2.Mer_vad_to = Convert.ToDateTime(txtTDate.Text);
                _tempItem2.Mer_bnksel_rt =Math.Round( usd_to_other_RATE1,6);//Convert.ToDecimal(txtBankSelling.Text);
                _tempItem2.Mer_bnkbuy_rt =Math.Round( usd_to_other_RATE2,6);//Convert.ToDecimal(txtBankBuy.Text);
                _tempItem2.Mer_cussel_rt =Math.Round( usd_to_other_RATE3,6);//Convert.ToDecimal(txtCustom.Text);
                _tempItem2.Mer_buyvad_from = Convert.ToDateTime(txtFDate.Text);
                _tempItem2.Mer_buyvad_to = Convert.ToDateTime(txtTDate.Text);
                _tempItem2.Mer_act = true;
                _tempItem2.Mer_cre_by = BaseCls.GlbUserID;
                _tempItem2.Mer_cre_dt = Convert.ToDateTime(txtTDate.Text);
                _tempItem2.Mer_mod_by = BaseCls.GlbUserID;
                _tempItem2.Mer_mod_dt = Convert.ToDateTime(txtTDate.Text);
                //_tempItem.Mer_session_id =BaseCls.GlbUserSessionID;
                _tempItem2.Mer_to_cur = "USD";// ddlFromCur.SelectedValue.ToString();
                _tempItem2.Mer_pc = txtPC.Text;
                _ExchangeRate.Add(_tempItem2);
                
                //-----------------------------------------
            }
            
            BindAddItem();
            //ddlFromCur.Text = "LKR"; SelectedValue
            ddlFromCur.SelectedValue = "LKR";
            //ddlToCur.Text = "LKR";
            ddlToCur.SelectedValue = "LKR";

            //txtBankBuy.Text = "";
            //txtBankSelling.Text = "";
            //txtCustom.Text = "";
            txtBankBuy.Text = string.Format("{0:n2}", 0);
            txtBankSelling.Text = string.Format("{0:n2}", 0);
            txtCustom.Text = string.Format("{0:n2}", 0);

            txtTDate.Value = Convert.ToDateTime("31-Dec-2999").Date;
            ddlFromCur.Focus();
        }
        protected void BindAddItem()
        {
            gvExchange.DataSource = null;
            gvExchange.AutoGenerateColumns = false;
            gvExchange.DataSource = _ExchangeRate;
        
        }
        private MasterExchangeRate AssignDataToObject()
        {
            //string _Amt = "";
            MasterExchangeRate _tempItem = new MasterExchangeRate();

            _tempItem.Mer_id = _lineNo;
            _tempItem.Mer_com =BaseCls.GlbUserComCode;
            _tempItem.Mer_cur = ddlFromCur.SelectedValue.ToString();
            _tempItem.Mer_vad_from = Convert.ToDateTime(txtFDate.Text);
            _tempItem.Mer_vad_to = Convert.ToDateTime(txtTDate.Text);
            _tempItem.Mer_bnksel_rt = Convert.ToDecimal(txtBankSelling.Text);
            _tempItem.Mer_bnkbuy_rt = Convert.ToDecimal(txtBankBuy.Text);
            _tempItem.Mer_cussel_rt = Convert.ToDecimal(txtCustom.Text);
            _tempItem.Mer_buyvad_from = Convert.ToDateTime(txtFDate.Text);
            _tempItem.Mer_buyvad_to = Convert.ToDateTime(txtTDate.Text);
            _tempItem.Mer_act = true;
            _tempItem.Mer_cre_by = BaseCls.GlbUserID;
            _tempItem.Mer_cre_dt = Convert.ToDateTime(txtTDate.Text);
            _tempItem.Mer_mod_by = BaseCls.GlbUserID;
            _tempItem.Mer_mod_dt = Convert.ToDateTime(txtTDate.Text);
            //_tempItem.Mer_session_id =BaseCls.GlbUserSessionID;
            _tempItem.Mer_to_cur = ddlToCur.SelectedValue.ToString();
            _tempItem.Mer_pc = txtPC.Text;

            return _tempItem;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string _msg = "";
            Int32 _rowEffect = 0;
            if (gvExchange.Rows.Count <= 0)
            {
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Details not found.");
                MessageBox.Show("Add details to save!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure to save ?", "Confirm save", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Int32 line=1;
            foreach (MasterExchangeRate exr in _ExchangeRate)
            {
                exr.Mer_id = line;
                line = line + 1;

                if (exr.Mer_cur == "USD" && exr.Mer_to_cur == "LKR") {
                    if (exr.Mer_bnkbuy_rt == 1)
                    {
                        if (MessageBox.Show("You have set exchange rate between USD and LKR as 1\nAre you sure want to process?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            string _mail = "Exchange Rate between USD and LKR is set as 1.00 for ," + Environment.NewLine;
                            _mail += "Profit center - " + txtPC.Text.Trim() + ""+Environment.NewLine;
                            _mail += "User - " + BaseCls.GlbUserName + " (" + BaseCls.GlbUserID + ")";
                            CHNLSVC.CommonSearch.Send_SMTPMail("ab_dfree@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
                            CHNLSVC.CommonSearch.Send_SMTPMail("ab_dfs6d@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
                            CHNLSVC.CommonSearch.Send_SMTPMail("chamald@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
                            CHNLSVC.CommonSearch.Send_SMTPMail("chathuranga@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
                            CHNLSVC.CommonSearch.Send_SMTPMail("darshana@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
                            CHNLSVC.CommonSearch.Send_SMTPMail("sachith@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
                            CHNLSVC.CommonSearch.Send_SMTPMail("asanka@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
                            CHNLSVC.CommonSearch.Send_SMTPMail("kelum@abansgroup.com", "Exchange Rate between USD and LKR", _mail);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                
            }
            _rowEffect = (Int32)CHNLSVC.Sales.SaveExchangeRate(_ExchangeRate);

            if (_rowEffect == 1)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created");
                MessageBox.Show("Successfully created", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnClear_Click(null, null);//ClearData();
               // return;
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                    MessageBox.Show(_msg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                    MessageBox.Show("Creation Failed!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBankSelling_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtBankBuy.Focus();
            }
            else
            {
                if (Regex.IsMatch(txtBankSelling.Text.Trim(), "[A-Z]+"))// if (!char.IsDigit(e.KeyChar))//|| e.KeyChar.ToString()!="."
                {
                    txtBankSelling.Text = "";
                    MessageBox.Show("Enter valid rate", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  //  txtBankSelling.Text = "";
                    txtBankSelling.Focus();
                }              
                
            }
        }

        private void txtBankBuy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCustom.Focus();
            }
            else
            {
                if (Regex.IsMatch(txtBankBuy.Text.Trim(), "[A-Z]+"))//if (!char.IsDigit(e.KeyChar))
                {
                    txtBankBuy.Text = "";
                    MessageBox.Show("Enter valid rate", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //  txtBankSelling.Text = "";
                    txtBankBuy.Focus();
                }
            }
        }

        private void ddlFromCur_SelectionChangeCommitted(object sender, EventArgs e)
        {
             ddlToCur.Focus();
            // ddlToCur.DroppedDown = true;
        }

        private void txtCustom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAdd.Select();
            }
            if (Regex.IsMatch(txtCustom.Text.Trim(), "[A-Z]+"))// if (!char.IsDigit(e.KeyChar))
            {
                txtCustom.Text = "";
                MessageBox.Show("Enter valid rate", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //  txtBankSelling.Text = "";
                txtCustom.Focus();
            }
        }

        private void ddlToCur_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //txtBankSelling.Focus();
            txtFDate.Focus();
        }

        private void txtFDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtTDate.Focus();
            }           
        }

        private void txtTDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtBankSelling.Focus();
            }           
        }

        private void gvExchange_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewRow row = gvExchange.Rows[rowIndex];
                Int32 line = Convert.ToInt32(row.Cells["mer_id"].Value.ToString());
                _ExchangeRate.RemoveAll(x => x.Mer_id == line);
                BindAddItem();
           
            }
        }

        private void txtBankSelling_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtBankSelling.Text.Trim(), "[A-Z]+")) //should contain atleast one capital letter
            {
                MessageBox.Show("Enter valid rate", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankSelling.Text = "";
                txtBankSelling.Focus();
                return;
            }
        }

        private void txtBankBuy_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtBankBuy.Text.Trim(), "[A-Z]+")) //should contain atleast one capital letter
            {
                MessageBox.Show("Enter valid rate", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankBuy.Text = "";
                txtBankBuy.Focus();
                return;
            }
        }

        private void txtCustom_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtCustom.Text.Trim(), "[A-Z]+")) //should contain atleast one capital letter
            {
                MessageBox.Show("Enter valid rate", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustom.Text = "";
                txtCustom.Focus();
                return;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
          List<MasterExchangeRate> list=  CHNLSVC.Sales.GetValid_ExchangeRates(BaseCls.GlbUserComCode, ddlFromCur.SelectedValue.ToString(), ddlToCur.SelectedValue.ToString(), txtFDate.Value.Date);
          list = list.Where(x => x.Mer_pc == BaseCls.GlbUserDefProf).ToList();
          gvExchange.DataSource = null;
          gvExchange.AutoGenerateColumns = false;
          gvExchange.DataSource = list;
        }

        private void txtBankSelling_TextChanged(object sender, EventArgs e)
        {
            if (txtBankSelling.Text != "") {
                txtBankBuy.Text = txtBankSelling.Text;
                txtCustom.Text = txtBankSelling.Text;
            }
        }

        private void btn_Srch_PC_Click(object sender, EventArgs e)
        {

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutsideParty:
                    {
                        paramsText.Append("HP" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Prefix:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CircularByComp:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.PromoByComp:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "C" + seperator + "A");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashComCirc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
