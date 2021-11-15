using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using FF.WindowsERPClient.Finance;

namespace FF.WindowsERPClient.Finance
{
    public partial class Scan_Physical_Docs_Settle : Base
    {
        static Int32 _ShortRef = 0;
        bool _isDecimalAllow = false;
        Int32 SelectWeek = 0;
        DateTime SelectMonthYear;
        string company = "";
        string profitCenter = "";
        public DateTime Todate;
        private static int _recordCNT = 0;

        private System.Windows.Forms.TextBox[] txtLine;
        private System.Windows.Forms.DateTimePicker[] dtDate;
        private System.Windows.Forms.TextBox[] txtDesc;
        private System.Windows.Forms.TextBox[] txtRef;
        private System.Windows.Forms.TextBox[] txtAmount;
        private System.Windows.Forms.CheckBox[] chkSettle;
        private System.Windows.Forms.CheckBox[] chkSUN;
        private System.Windows.Forms.TextBox[] txtSettleAmt;
        private System.Windows.Forms.TextBox[] txtRem;
        private System.Windows.Forms.TextBox[] txtShort;
        private System.Windows.Forms.DateTimePicker[] dtRlsDate;
        private System.Windows.Forms.TextBox[] txtAcc;

        private Scan_Physical_Docs ParentForm;

        private Scan_Physical_Docs parent_ScanPhysicalDocs = null;

        public Scan_Physical_Docs Parent_ScanPhysicalDocs
        {
            get { return Parent_ScanPhysicalDocs; }
            set { Parent_ScanPhysicalDocs = value; }
        }

        public Scan_Physical_Docs_Settle()
        {
            InitializeComponent();
            initialzeDefaultvalues();
            bind_Combo_ddlDocTypes();

        }

        private void bind_Combo_ddlDocTypes()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("ALL", "ALL");
            PartyTypes.Add("CHEQUE", "CHEQUE");
            PartyTypes.Add("CS_CHEQUE", "CS SETTLEMENT-CHEQUES");
            PartyTypes.Add("CS_CASH", "CS SETTLEMENT-CASH");
            PartyTypes.Add("CRCD", "CREDIT CARD");
            PartyTypes.Add("ADVAN", "ADVANCE RECEIPTS");
            PartyTypes.Add("DEPOSIT", "BANK DEPOSIT SLIP");
            PartyTypes.Add("COLL-BONUS", "COLLECTION BONUS");
            PartyTypes.Add("PROD-BONUS", "PRODUCT BONUS");//ZM-VOUCHER
            PartyTypes.Add("ZM-VOUCHER", "ZONE MANAGER VOUCHER");
            PartyTypes.Add("GV", "GIFT VOUCHER");
            PartyTypes.Add("GVO", "OTHER GIFT VOUCHER");
            PartyTypes.Add("SB", "SHORT BANKING");
            ddlDocTypes.DataSource = new BindingSource(PartyTypes, null);
            ddlDocTypes.DisplayMember = "Value";
            ddlDocTypes.ValueMember = "Key";
        }

        public Scan_Physical_Docs_Settle(Scan_Physical_Docs ParentForm_, DateTime SelectMonthYear_, Int32 SelectWeek_, string com, string pc)
        {
            InitializeComponent();
            ParentForm = ParentForm_;
            SelectWeek = SelectWeek_;
            SelectMonthYear = SelectMonthYear_;
            company = com;
            profitCenter = pc;

            txtPC.Text = profitCenter;
            txtweek.Text = SelectWeek.ToString();
            var month = new DateTime(SelectMonthYear_.Year, SelectMonthYear_.Month, 1);
            txtMonth.Text = month.ToString("MMM/yyyy");

            initialzeDefaultvalues();


        }
        private void initialzeDefaultvalues()
        {
            ddlDocTypes.SelectedIndex = 0;
            _recordCNT = 0;

        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion




        private void grvPopUpExtraDocs_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void txtExtraDocAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // IsDecimalAllow(_isDecimalAllow, sender, e);
        }

        private void btnExtraDocFind_Click(object sender, EventArgs e)
        {
            string SELECT_DOC_TP = ddlDocTypes.Text;
            Decimal _short = 0;
            Decimal _diff = 0;
            if (SELECT_DOC_TP == "")
            {
                MessageBox.Show("Select Document Type", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = CHNLSVC.Financial.Get_ShortBankDocs(BaseCls.GlbUserComCode, profitCenter, DateTime.Now.Date, 0, SELECT_DOC_TP, txtAccNo.Text, Convert.ToInt32(chk.Checked), dtFrom.Value.Date, dtTo.Value.Date);

            RemoveControls();
            pnlExecutive.Height = 31;
            pnlExecutive.Top = 4;
            _recordCNT = dt.Rows.Count;


            if (_recordCNT > 0)
            {
                LoadLine(_recordCNT);
                LoadDate(_recordCNT);
                LoadDesc(_recordCNT);
                LoadRef(_recordCNT);
                LoadAmount(_recordCNT);
                LoadSetAmount(_recordCNT);
                LoadSettle(_recordCNT);
                LoadRem(_recordCNT);
                LoadSun(_recordCNT);
                LoadShort(_recordCNT);
                LoadRlsDate(_recordCNT);
                LoadAcc(_recordCNT);
            }

            for (int i = 0; i < _recordCNT; i++)
            {
                txtLine[i + 1].Text = (dt.Rows[i]["Grdd_seq"]).ToString();
                dtDate[i + 1].Text = (dt.Rows[i]["grdd_dt"]).ToString();
                txtDesc[i + 1].Text = (dt.Rows[i]["grdd_doc_desc"]).ToString();
                if (txtDesc[i + 1].Text == "CHEQUE" && dt.Rows[i]["grdd_doc_ref"].ToString().Length == 13)   //7/5/2015
                    txtRef[i + 1].Text = dt.Rows[i]["grdd_doc_ref"].ToString().Substring(7, 6);
                else
                    txtRef[i + 1].Text = (dt.Rows[i]["grdd_doc_ref"]).ToString();
                txtAmount[i + 1].Text = (dt.Rows[i]["GRDD_SYS_VAL"]).ToString();

                _short = Convert.ToDecimal((dt.Rows[i]["GRDD_SYS_VAL"])) - Convert.ToDecimal((dt.Rows[i]["grdd_settle_val"]));
                txtShort[i + 1].Text = (_short).ToString();

                //18/8/2015
                _diff = Convert.ToDecimal((dt.Rows[i]["GRDD_SYS_VAL"])) - Convert.ToDecimal((dt.Rows[i]["GRDD_DOC_VAL"]));

                if (_diff != 0)
                    dtRlsDate[i + 1].Visible = false;
                else
                {
                    if (string.IsNullOrEmpty(dt.Rows[i]["grdd_realized_dt"].ToString()))
                        dtRlsDate[i + 1].Text = ("31/Dec/2999").ToString();
                    else
                        dtRlsDate[i + 1].Text = (dt.Rows[i]["grdd_realized_dt"]).ToString();

                    if (dtRlsDate[i + 1].Text.ToString() == ("31/Dec/2999").ToString())
                        dtRlsDate[i + 1].Visible = false;
                }

                txtAcc[i + 1].Text = (dt.Rows[i]["grdd_deposit_bank"]).ToString();


            }
            vScrollBar1.Maximum = pnlExecutive.Height - 6 * _recordCNT;

        }

        private void LoadShort(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtShort", _num);
            int n = 1;
            pnlBal.Height = 25;
            while (n < _num + 1)
            {
                txtShort[n].Tag = n;
                txtShort[n].Width = 75;
                txtShort[n].Height = 21;
                txtShort[n].BackColor = System.Drawing.SystemColors.Info;
                txtShort[n].TextAlign = HorizontalAlignment.Right;
                txtShort[n].Enabled = false;
                txtShort[n].Left = xPos;
                txtShort[n].Top = yPos;
                yPos = yPos + txtShort[n].Height + 4;
                pnlBal.Controls.Add(txtShort[n]);
                pnlBal.Height = pnlBal.Height + 25;
                n++;
            }
        }

        private void LoadSun(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("chkSun", _num);
            int n = 1;
            pnlSUN.Height = 25;
            while (n < _num + 1)
            {
                chkSUN[n].Tag = n;
                chkSUN[n].Width = 15;
                chkSUN[n].Height = 14;
                chkSUN[n].Left = xPos;
                chkSUN[n].Top = yPos;
                yPos = yPos + chkSUN[n].Height + 10;
                pnlSUN.Controls.Add(chkSUN[n]);
                pnlSUN.Height = pnlSUN.Height + 25;
                n++;
            }
        }

        private void LoadSettle(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("chkSet", _num);
            int n = 1;
            pnlSet.Height = 25;
            while (n < _num + 1)
            {
                chkSettle[n].Tag = n;
                chkSettle[n].Width = 15;
                chkSettle[n].Height = 14;
                chkSettle[n].Left = xPos;
                chkSettle[n].Top = yPos;
                yPos = yPos + chkSettle[n].Height + 10;
                pnlSet.Controls.Add(chkSettle[n]);
                pnlSet.Height = pnlSet.Height + 25;
                chkSettle[n].CheckedChanged += new System.EventHandler(chkSettle_CheckedChanged);
                n++;
            }
        }

        public void chkSettle_CheckedChanged(Object sender, System.EventArgs e)
        {
            Int32 _arrIndex = Convert.ToInt32(((System.Windows.Forms.CheckBox)sender).Tag);
            if (chkSettle[_arrIndex].Checked)
            {
                txtSettleAmt[_arrIndex].Enabled = true;
                txtRem[_arrIndex].Enabled = true;
            }
            else
            {
                txtSettleAmt[_arrIndex].Text = "";
                txtRem[_arrIndex].Text = "";
                txtSettleAmt[_arrIndex].Enabled = false;
                txtRem[_arrIndex].Enabled = false;
            }
            //textBox_TextChanged(null,null);
        }
        private void LoadSetAmount(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtSetAmount", _num);
            int n = 1;
            pnlSetAmt.Height = 25;
            while (n < _num + 1)
            {
                txtSettleAmt[n].Tag = n;
                txtSettleAmt[n].Width = 75;
                txtSettleAmt[n].Height = 21;
                txtSettleAmt[n].BackColor = System.Drawing.SystemColors.Info;
                txtSettleAmt[n].TextAlign = HorizontalAlignment.Right;
                txtSettleAmt[n].Enabled = false;
                txtSettleAmt[n].Left = xPos;
                txtSettleAmt[n].Top = yPos;
                yPos = yPos + txtSettleAmt[n].Height + 4;
                pnlSetAmt.Controls.Add(txtSettleAmt[n]);
                pnlSetAmt.Height = pnlSetAmt.Height + 25;
                txtSettleAmt[n].TextChanged += new System.EventHandler(textBox_TextChanged);
                n++;
            }
        }

        private void LoadRem(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtRem", _num);
            int n = 1;
            pnlRem.Height = 25;
            while (n < _num + 1)
            {
                txtRem[n].Tag = n;
                txtRem[n].Width = 165;
                txtRem[n].Height = 21;
                txtRem[n].BackColor = System.Drawing.SystemColors.Info;
                txtRem[n].TextAlign = HorizontalAlignment.Left;
                txtRem[n].Enabled = false;
                txtRem[n].Left = xPos;
                txtRem[n].Top = yPos;
                yPos = yPos + txtRem[n].Height + 4;
                pnlRem.Controls.Add(txtRem[n]);
                pnlRem.Height = pnlRem.Height + 25;
                n++;
            }
        }

        private void RemoveControls()
        {
            for (int i = 1; i < _recordCNT + 1; i++)
            {
                pnlDate.Controls.Remove(dtDate[i]);
                pnlLine.Controls.Remove(txtLine[i]);
                pnlRef.Controls.Remove(txtRef[i]);
                pnlAmount.Controls.Remove(txtAmount[i]);
                pnlDesc.Controls.Remove(txtDesc[i]);
                pnlSUN.Controls.Remove(chkSUN[i]);
                pnlRem.Controls.Remove(txtRem[i]);
                pnlSet.Controls.Remove(chkSettle[i]);
                pnlSetAmt.Controls.Remove(txtSettleAmt[i]);
                pnlBal.Controls.Remove(txtShort[i]);
                pnlRlsDate.Controls.Remove(dtRlsDate[i]);
                pnlAccNo.Controls.Remove(txtAcc[i]);
            }
        }

        private void LoadDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("dtDate", _num);
            int n = 1;
            pnlDate.Height = 25;
            while (n < _num + 1)
            {
                dtDate[n].Tag = n;
                dtDate[n].Width = 104;
                dtDate[n].Height = 21;
                dtDate[n].Left = xPos;
                dtDate[n].Top = yPos;
                yPos = yPos + dtDate[n].Height + 4;
                dtDate[n].Format = DateTimePickerFormat.Custom;
                dtDate[n].CustomFormat = "dd/MMM/yyyy";
                // dtDate[n].Enabled = false;
                pnlDate.Controls.Add(dtDate[n]);
                pnlDate.Height = pnlDate.Height + 25;
                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
        }

        private void LoadRlsDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("dtRlsDate", _num);
            int n = 1;
            pnlRlsDate.Height = 25;
            while (n < _num + 1)
            {
                dtRlsDate[n].Tag = n;
                dtRlsDate[n].Width = 104;
                dtRlsDate[n].Height = 21;
                dtRlsDate[n].Left = xPos;
                dtRlsDate[n].Top = yPos;
                yPos = yPos + dtRlsDate[n].Height + 4;
                dtRlsDate[n].Format = DateTimePickerFormat.Custom;
                dtRlsDate[n].CustomFormat = "dd/MMM/yyyy";
                dtRlsDate[n].Enabled = false;
                pnlRlsDate.Controls.Add(dtRlsDate[n]);
                pnlRlsDate.Height = pnlRlsDate.Height + 25;
                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
        }

        private void LoadAmount(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtAmount", _num);
            int n = 1;
            pnlAmount.Height = 25;
            while (n < _num + 1)
            {
                txtAmount[n].Tag = n;
                txtAmount[n].Width = 75;
                txtAmount[n].Height = 21;
                txtAmount[n].BackColor = System.Drawing.SystemColors.Info;
                txtAmount[n].TextAlign = HorizontalAlignment.Right;
                txtAmount[n].Enabled = false;
                txtAmount[n].Left = xPos;
                txtAmount[n].Top = yPos;
                yPos = yPos + txtAmount[n].Height + 4;
                pnlAmount.Controls.Add(txtAmount[n]);
                pnlAmount.Height = pnlAmount.Height + 25;
                n++;
            }
        }

        private void LoadLine(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtLine", _num);
            int n = 1;
            pnlLine.Height = 25;
            while (n < _num + 1)
            {
                pnlExecutive.Height = pnlExecutive.Height + 31;
                txtLine[n].Tag = n;
                txtLine[n].Width = 50;
                txtLine[n].Height = 21;
                txtLine[n].BackColor = System.Drawing.SystemColors.Info;
                txtLine[n].Left = xPos;
                txtLine[n].Top = yPos;
                yPos = yPos + txtLine[n].Height + 4;
                pnlLine.Controls.Add(txtLine[n]);
                pnlLine.Height = pnlLine.Height + 25;
                n++;
            }
        }

        private void LoadDesc(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtDesc", _num);
            int n = 1;
            pnlDesc.Height = 25;
            while (n < _num + 1)
            {
                txtDesc[n].Tag = n;
                txtDesc[n].Width = 235;
                txtDesc[n].Height = 21;
                txtDesc[n].BackColor = System.Drawing.SystemColors.Info;
                txtDesc[n].Enabled = false;
                txtDesc[n].Left = xPos;
                txtDesc[n].Top = yPos;
                yPos = yPos + txtDesc[n].Height + 4;
                pnlDesc.Controls.Add(txtDesc[n]);
                pnlDesc.Height = pnlDesc.Height + 25;
                n++;
            }
        }

        private void LoadRef(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtRef", _num);
            int n = 1;
            pnlRef.Height = 25;
            while (n < _num + 1)
            {
                txtRef[n].Tag = n;
                txtRef[n].Width = 113;
                txtRef[n].Height = 21;
                txtRef[n].BackColor = System.Drawing.SystemColors.Info;
                txtRef[n].Enabled = false;
                txtRef[n].Left = xPos;
                txtRef[n].Top = yPos;
                yPos = yPos + txtRef[n].Height + 4;
                pnlRef.Controls.Add(txtRef[n]);
                pnlRef.Height = pnlRef.Height + 25;
                n++;
            }
        }

        private void LoadAcc(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtAcc", _num);
            int n = 1;
            pnlAccNo.Height = 25;
            while (n < _num + 1)
            {
                txtAcc[n].Tag = n;
                txtAcc[n].Width = 113;
                txtAcc[n].Height = 21;
                txtAcc[n].BackColor = System.Drawing.SystemColors.Info;
                txtAcc[n].Enabled = false;
                txtAcc[n].Left = xPos;
                txtAcc[n].Top = yPos;
                yPos = yPos + txtAcc[n].Height + 4;
                pnlAccNo.Controls.Add(txtAcc[n]);
                pnlAccNo.Height = pnlAccNo.Height + 25;
                n++;
            }
        }

        private void AddControls(string anyControl, int cNumber)
        {
            switch (anyControl)
            {
                case "txtLine":
                    {
                        txtLine = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtLine[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "dtDate":
                    {
                        dtDate = new System.Windows.Forms.DateTimePicker[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            dtDate[i] = new System.Windows.Forms.DateTimePicker();
                        }
                        break;
                    }
                case "txtDesc":
                    {
                        txtDesc = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtDesc[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtRef":
                    {
                        txtRef = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRef[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }

                case "txtAmount":
                    {
                        txtAmount = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtAmount[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "chkSun":
                    {
                        chkSUN = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkSUN[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "chkSet":
                    {
                        chkSettle = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkSettle[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "txtSetAmount":
                    {
                        txtSettleAmt = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSettleAmt[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtRem":
                    {
                        txtRem = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRem[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtShort":
                    {
                        txtShort = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtShort[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }

                case "dtRlsDate":
                    {
                        dtRlsDate = new System.Windows.Forms.DateTimePicker[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            dtRlsDate[i] = new System.Windows.Forms.DateTimePicker();
                        }
                        break;
                    }
                case "txtAcc":
                    {
                        txtAcc = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtAcc[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
            }
        }


        private void btnUpd_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < _recordCNT + 1; i++)
            {
                if (chkSettle[i].Checked == true)
                {
                    if (string.IsNullOrEmpty(txtSettleAmt[i].Text))
                    {
                        MessageBox.Show("Please enter the settle amount", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            if (MessageBox.Show("Are You Sure?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            btnUpd.Enabled = false;
            for (int i = 1; i < _recordCNT + 1; i++)
            {
                if (chkSettle[i].Checked == true)
                {
                    DateTime month = new DateTime(SelectMonthYear.Year, SelectMonthYear.Month, 1);
                    Int32 X = CHNLSVC.Financial.UpdateShortSettlement(Convert.ToInt32(txtLine[i].Text), Convert.ToDecimal(txtSettleAmt[i].Text), Convert.ToInt32(chkSUN[i].Checked), txtRem[i].Text, month, Convert.ToInt32(txtweek.Text), BaseCls.GlbUserID, dtTo.Value.Date); //add by tharanga inform from Dilanda

                }
            }

            MessageBox.Show("Successfully Updated.", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void vScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            pnlExecutive.Top = -vScrollBar1.Value + 50;
        }


        protected void textBox_TextChanged(object sender, EventArgs e)
        {
            // Your code here
            try
            {
                txtSelTot.Text = "0.00";
                try
                {

                    for (int i = 1; i < _recordCNT + 1; i++)
                    {

                        if (chkSettle[i].Checked == true)
                        {
                            Decimal phyVal = 0;
                            if (!string.IsNullOrEmpty((txtSettleAmt[i].Text)))
                            {
                                phyVal = Convert.ToDecimal(txtSettleAmt[i].Text);
                            }

                            txtSelTot.Text = (Convert.ToDecimal(txtSelTot.Text) + phyVal).ToString("0.00");

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid amount!");

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_srch_accno_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccNo;
                _CommonSearch.ShowDialog();
                txtAccNo.Select();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (chk.Checked == true)
            {
                dtFrom.Enabled = true;
                dtTo.Enabled = true;
            }
            else
            {
                dtFrom.Enabled = false;
                dtTo.Enabled = false;
            }
        }

        private void Scan_Physical_Docs_Settle_Load(object sender, EventArgs e)
        {
            dtTo.Value =  Todate.Date;
        }



    }
}
