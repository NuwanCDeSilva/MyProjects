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
    public partial class Scan_Physical_Docs_Extra : Base
    {
        static Int32 _ShortRef=0;
        bool _isDecimalAllow = false;
        Int32 SelectWeek = 0;
        DateTime SelectMonthYear;
        string company = "";
        string profitCenter = "";

        private Scan_Physical_Docs ParentForm;

        private Scan_Physical_Docs parent_ScanPhysicalDocs=null;

        public Scan_Physical_Docs Parent_ScanPhysicalDocs
        {
            get { return Parent_ScanPhysicalDocs; }
            set { Parent_ScanPhysicalDocs = value; }
        }

        public Scan_Physical_Docs_Extra()
        {
            InitializeComponent();
            initialzeDefaultvalues();

        }
        public Scan_Physical_Docs_Extra(Scan_Physical_Docs ParentForm_, DateTime SelectMonthYear_, Int32 SelectWeek_,string com,string pc)
        {
            InitializeComponent();
            ParentForm = ParentForm_;
            SelectWeek = SelectWeek_;
            SelectMonthYear = SelectMonthYear_;
            company = com;
            profitCenter = pc;

            initialzeDefaultvalues();

            bind_Combo_ddlDocTypes();
            ddlPopUpDocTp.SelectedIndex = -1;

            PanelShortBanking.Enabled = false;
            

        }
        private void initialzeDefaultvalues()
        {
            grvPopUpExtraDocs.AutoGenerateColumns = false;

            cmbYear.Items.Add("2012");
            cmbYear.Items.Add("2013");
            cmbYear.Items.Add("2014");
            cmbYear.Items.Add("2015");
            cmbYear.Items.Add("2016");
            cmbYear.Items.Add("2017");
            cmbYear.Items.Add("2018");
            cmbMonth.SelectedIndex = -1;

            int _Year = DateTime.Now.Year;
            cmbYear.SelectedIndex = _Year % 2013 + 1;

            cmbMonth.Items.Add("January");
            cmbMonth.Items.Add("February");
            cmbMonth.Items.Add("March");
            cmbMonth.Items.Add("April");
            cmbMonth.Items.Add("May");
            cmbMonth.Items.Add("June");
            cmbMonth.Items.Add("July");
            cmbMonth.Items.Add("August");
            cmbMonth.Items.Add("September");
            cmbMonth.Items.Add("October");
            cmbMonth.Items.Add("November");
            cmbMonth.Items.Add("December");
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;

            ddlPopUpDocTp.Items.Clear();
            ddlPopUpDocTp.DataSource = null;
            ddlPopUpDocTp.DataSource = CHNLSVC.Financial.Get_GNR_RCV_DSK_DOC_Types();
            ddlPopUpDocTp.DisplayMember = "grdt_name";
            ddlPopUpDocTp.ValueMember = "grdt_tp";

            ddlPopUpDocTp.SelectedIndex = -1;
        }
        private void bind_Combo_ddlDocTypes()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            //PartyTypes.Add("ALL", "ALL");
            if (BaseCls.GlbUserComCode == "AAL" || BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD")
            {
                PartyTypes.Add("CHEQUE", "CHEQUE");
                PartyTypes.Add("CS_CHEQUE", "CS SETTLEMENT-CHEQUES");
                PartyTypes.Add("DEPOSIT", "BANK DEPOSIT SLIP");
            }
            PartyTypes.Add("CS_CASH", "CS SETTLEMENT-CASH");
            PartyTypes.Add("CRCD", "CREDIT CARD");
            PartyTypes.Add("ADVAN", "ADVANCE RECEIPTS");

            PartyTypes.Add("COLL-BONUS", "COLLECTION BONUS");
            PartyTypes.Add("PROD-BONUS", "PRODUCT BONUS");
            PartyTypes.Add("ZM-VOUCHER", "ZONE MANAGER VOUCHER");
            PartyTypes.Add("GV", "GIFT VOUCHER");
            PartyTypes.Add("GVO", "OTHER GIFT VOUCHER");
            PartyTypes.Add("SB", "SHORT BANK");
            ddlPopUpDocTp.DataSource = new BindingSource(PartyTypes, null);
            ddlPopUpDocTp.DisplayMember = "Value";
            ddlPopUpDocTp.ValueMember = "Key";
        }
        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
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

        private void btnExtraDocFind_Click(object sender, EventArgs e)
        {
            if (chekShortBS.Checked==false)
            {
                
            }
            if (chekShortBS.Checked)
            {
                PanelShortBanking.Enabled = true;
            }
            else
            {
                PanelShortBanking.Enabled = false;
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //cmbMonth.SelectedIndex = -1;
           // ddExtraDoclWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";
            grvPopUpExtraDocs.DataSource = null;
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbYear.Text))
            {
                int month = cmbMonth.SelectedIndex + 1;
                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
                lblMonth.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
            }

            ddExtraDoclWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";
            grvPopUpExtraDocs.DataSource = null;
        }

        private void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime _from;
                DateTime _to;
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(ddExtraDoclWeek.Text))
                {
                    DataTable _weekDef = CHNLSVC.General.GetWeekDefinition(Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(ddExtraDoclWeek.SelectedIndex + 1), out _from, out _to, BaseCls.GlbUserComCode, "");
                    if (_from != Convert.ToDateTime("31/Dec/9999"))
                    {
                        lblFrmdtWk.Text = _from.Date.ToString("dd/MMM/yyyy");
                        lblTodtWk.Text = _to.Date.ToString("dd/MMM/yyyy");
                    }
                    else
                    {
                        lblFrmdtWk.Text = string.Empty;
                        lblTodtWk.Text = string.Empty;
                    }
                }

                //TODO: LOAD THE GRIDVIEW.
                DateTime firstDayofMonth = Convert.ToDateTime(lblMonth.Text).Date;
                string SELECT_DOC_TP = Convert.ToString(ddlPopUpDocTp.Text);

                DataTable dt = new DataTable();
                //if (SELECT_DOC_TP == "ALL")
                //{
                //    dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(GlbUserComCode, txtPC.Text.Trim().ToUpper(), firstDayofMonth, Convert.ToInt32(ddlWeek.SelectedValue), null);
                //}
                //else {
                //    dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(GlbUserComCode, txtPC.Text.Trim().ToUpper(), firstDayofMonth, Convert.ToInt32(ddlWeek.SelectedValue), SELECT_DOC_TP);
                //}
                dt = CHNLSVC.Financial.Get_ShortBankDocs(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, firstDayofMonth, Convert.ToInt32(ddExtraDoclWeek.SelectedIndex + 1), null,null,0,DateTime.Now.Date,DateTime.Now.Date);
                grvPopUpExtraDocs.DataSource = null;
                grvPopUpExtraDocs.AutoGenerateColumns = false;
                grvPopUpExtraDocs.DataSource = dt;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnExtraDocAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO: new column fill GRDD_SHORT_REF
                //add to the table. call SP
                if (ddlPopUpDocTp.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select the check box for short banking!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ddlPopUpDocTp.SelectedValue.ToString() == "CS_CHEQUE" || ddlPopUpDocTp.SelectedValue.ToString() == "DEPOSIT" || ddlPopUpDocTp.SelectedValue.ToString() == "CRCD" || ddlPopUpDocTp.SelectedValue.ToString() == "CHEQUE")
                {
                    if (ddlPopUpBank.SelectedIndex == -1)
                    {
                        // need records in = <gnr_rcv_sun_acc>  table
                        MessageBox.Show("Please select bank!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                try
                {
                    Convert.ToDecimal(txtExtraDocAmt.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter valid amount!", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtExtraDocRef.Text.Trim() == "")
                {
                    MessageBox.Show("Enter reference number!", "Reference number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure to add?", "Confirm add", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Int32 eff = 0;
                ScanPhysicalDocReceiveDet DOC = new ScanPhysicalDocReceiveDet();
                if (ddlPopUpDocTp.SelectedValue.ToString() == "SHORT_BANK")
                {
                    _ShortRef = 0;
                }
                if (_ShortRef != 0)
                {
                    DOC = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC_on_Seq(_ShortRef);
                    DOC.Grdd_rcv_by = BaseCls.GlbUserID;
                    DOC.Grdd_rcv_dt = DateTime.Now.Date;
                    DOC.Grdd_doc_rcv = true;
                    DOC.Grdd_rmk = txtExtraDocRemks.Text.Trim();
                    DOC.Grdd_doc_val = Convert.ToDecimal(txtExtraDocAmt.Text);
                    DOC.Grdd_dt = SelectMonthYear; //*** DateTime.Now.Date;//Convert.ToDateTime(txtExtraDocDate.Text);
                    // DOC.Grdd_week = Convert.ToInt32(ddExtraDoclWeek.SelectedValue);
                    //***  DOC.Grdd_month = Convert.ToDateTime(lblMonth.Text);
                    //***  DOC.Grdd_week = Convert.ToInt32(ddExtraDoclWeek.SelectedIndex + 1);
                    DOC.Grdd_month = Convert.ToDateTime("01" + "/" + SelectMonthYear.Month + "/" + SelectMonthYear.Year).Date;//Convert.ToDateTime(lblMonth.Text); ;
                    DOC.Grdd_week = SelectWeek;//Convert.ToInt32(ddExtraDoclWeek.SelectedIndex + 1);

                    DOC.Grdd_is_extra = true;
                    DOC.Grdd_short_ref = _ShortRef;

                    if (ddlPopUpBank.SelectedItem != null)
                    {
                        DOC.Grdd_doc_bank = ddlPopUpBank.SelectedItem.ToString();
                    }
                    //if(ddlPopUpBank.SelectedIndex!=-1)
                    //{
                    //    DOC.Grdd_doc_bank = ddlPopUpBank.SelectedText.ToString();
                    //    if (ddlPopUpBank.SelectedValue!=null)
                    //        DOC.Grdd_doc_bank = ddlPopUpBank.SelectedValue.ToString();
                    //}
                    eff = CHNLSVC.Financial.saveExtraDoc(DOC, true);
                }
                else
                {
                    if (ddlPopUpDocTp.SelectedValue.ToString() == "")
                    {
                        MessageBox.Show("Select document type!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    DOC.Grdd_com = company;//BaseCls.GlbUserComCode;
                    DOC.Grdd_cre_by = BaseCls.GlbUserID;
                    DOC.Grdd_cre_dt = DateTime.Now.Date;
                    //DOC.Grdd_deposit_bank = null;
                    //DOC.Grdd_doc_bank
                    //DOC.Grdd_doc_bank_branch
                    //DOC.Grdd_doc_bank_cd                
                    DOC.Grdd_doc_rcv = true;
                    if (ddlPopUpDocTp.SelectedValue.ToString() == "SHORT_BANK")
                    {
                        DOC.Grdd_doc_rcv = false;

                    }
                    DOC.Grdd_doc_desc = Convert.ToString(ddlPopUpDocTp.Text.ToString());
                    DOC.Grdd_doc_ref = txtExtraDocRef.Text.Trim();
                    DOC.Grdd_doc_tp = Convert.ToString(ddlPopUpDocTp.SelectedValue.ToString());
                    DOC.Grdd_doc_val = Convert.ToDecimal(txtExtraDocAmt.Text.Trim());
                    DOC.Grdd_dt = SelectMonthYear; ;//*** DateTime.Now.Date;//Convert.ToDateTime(txtExtraDocDate.Text.Trim());
                    DOC.Grdd_is_extra = true;
                    //DOC.Grdd_is_realized

                    DOC.Grdd_pc = profitCenter;//BaseCls.GlbUserDefProf;
                    DOC.Grdd_rcv_by = BaseCls.GlbUserID;
                    DOC.Grdd_rcv_dt = DateTime.Now.Date;
                    DOC.Grdd_realized_dt = Convert.ToDateTime(DateTime.Now.Date);
                    DOC.Grdd_rmk = txtExtraDocRemks.Text;
                    // DOC.Grdd_scan_by =
                    //  DOC.Grdd_scan_dt;
                    // DOC.Grdd_scan_rcv;
                    DOC.Grdd_sys_val = Convert.ToDecimal(txtExtraDocAmt.Text.Trim());
                    //*** DOC.Grdd_month = Convert.ToDateTime(lblMonth.Text); ;
                    //*** DOC.Grdd_week = Convert.ToInt32(ddExtraDoclWeek.SelectedIndex + 1);
                    DOC.Grdd_month = Convert.ToDateTime("01" + "/" + SelectMonthYear.Month + "/" + SelectMonthYear.Year).Date;//Convert.ToDateTime(lblMonth.Text); ;
                    DOC.Grdd_week = SelectWeek;//Convert.ToInt32(ddExtraDoclWeek.SelectedIndex + 1);

                    if (ddlPopUpBank.SelectedItem != null)
                    {
                        DOC.Grdd_doc_bank = ddlPopUpBank.SelectedItem.ToString();
                    }
                    //if (ddlPopUpBank.SelectedIndex != -1)
                    //{
                    //    DOC.Grdd_doc_bank = ddlPopUpBank.SelectedText.ToString();
                    //    if (ddlPopUpBank.SelectedValue != null)
                    //     DOC.Grdd_doc_bank = ddlPopUpBank.SelectedValue.ToString();
                    //}

                    eff = CHNLSVC.Financial.saveExtraDoc(DOC, false);
                }
                if (eff > 0)
                {

                    MessageBox.Show("Successfully Added.", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (ParentForm != null)
                    {
                        ParentForm.btnView_Click(null, null);
                    }
                    if (ddExtraDoclWeek.SelectedIndex != -1)
                    {
                        try
                        {
                            this.ddlWeek_SelectedIndexChanged(null, null);
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
                else
                {

                    MessageBox.Show("Not Added!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                _ShortRef = 0;
                txtExtraDocAmt.Text = "";
                txtExtraDocRef.Text = "";
                txtExtraDocRemks.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ddlPopUpDocTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DT = null;
                string docType = Convert.ToString(ddlPopUpDocTp.SelectedValue);
                DT = CHNLSVC.Financial.GET_BANKS_of_PC_on_docType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, docType);

                //CHNLSVC.Financial.GET_BANKS_of_PC_on_docType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, docType);
                if (DT != null)
                {
                    ddlPopUpBank.Items.Clear();
                    if (!string.IsNullOrEmpty(docType))
                    {
                        foreach (DataRow drow in DT.Rows)
                        {
                            ddlPopUpBank.Items.Add(drow["grsa_bank_id"].ToString());
                            if (drow["grsa_is_default"].ToString() == "1")
                            {
                                ddlPopUpBank.SelectedValue = drow["grsa_bank_id"].ToString();

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvPopUpExtraDocs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    //GridViewRow row = grvPopUpExtraDocs.SelectedRow;
                    //Int32 SEQ = Convert.ToInt32(row.Cells[1].Text.Trim());
                    int _rowIndex = e.RowIndex;
                    _ShortRef = Convert.ToInt32(grvPopUpExtraDocs.Rows[_rowIndex].Cells["grdd_seq"].Value);
                    ScanPhysicalDocReceiveDet DOC = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC_on_Seq(_ShortRef);
                    txtExtraDocAmt.Text = DOC.Grdd_sys_val.ToString();
                    txtExtraDocRef.Text = DOC.Grdd_doc_ref;
                    txtExtraDocRemks.Text = "";
                    ddlPopUpDocTp.SelectedValue = DOC.Grdd_doc_tp;

                    txtExtraDocDate.Text = DateTime.Now.Date.ToShortDateString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
         
        }

        private void txtExtraDocAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
           // IsDecimalAllow(_isDecimalAllow, sender, e);
        }

        private void chekShortBS_CheckedChanged(object sender, EventArgs e)
        {
            if (chekShortBS.Checked == true)
            {
                PanelShortBanking.Enabled = true;
            }
            else
            {
                PanelShortBanking.Enabled = false;
                ddlPopUpDocTp.SelectedIndex = -1;
                ddlPopUpBank.SelectedIndex = -1;
                txtExtraDocAmt.Text = "";
                txtExtraDocRef.Text = "";
                txtExtraDocRemks.Text = "";
            }
        }
        


    }
}
