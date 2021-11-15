using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.General
{
    public partial class LoyaltyMembership : Base
    {
        public LoyaltyMembership()
        {
            InitializeComponent();
        }

        #region search
        private void btnLoyaltyType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true; 
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
                DataTable _result = CHNLSVC.CommonSearch.GetLoyaltyTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoyaltyType;
                _CommonSearch.txtSearchbyword.Text = txtLoyaltyType.Text;
                _CommonSearch.ShowDialog();

                txtLoyaltyType.Focus();
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

        private void btnCusCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true; 
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoyaltyCustomer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCodeLoyalty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.ShowDialog();
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

        private void btnCardNo_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoyaltyCardNo);
            //DataTable _result = CHNLSVC.CommonSearch.GetLoyaltyCardNos(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtCardNo;
            //_CommonSearch.ShowDialog();
            //txtCardNo.Select();
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true; 
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCardNo;
                _CommonSearch.ShowDialog();
                txtCardNo.Select();
                LoadMember(txtRenCusCode.Text, txtCardNo.Text);
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

        private void btnTrnCusCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true; 
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoyaltyCustomer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCodeLoyalty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTrnCusCode;
                _CommonSearch.ShowDialog();
                txtTrnCusCode.Select();
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

        private void btnTrnCardNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true; 
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTrnCardNo;
                _CommonSearch.ShowDialog();
                txtTrnCardNo.Select();
                LoadMember(txtTrnCusCode.Text, txtTrnCardNo.Text);
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

        private void btnEnqCus_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true; 
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoyaltyCustomer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCodeLoyalty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEnqCusCode;
                _CommonSearch.ShowDialog();
                txtEnqCusCode.Select();
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

        private void btnEnqCard_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true; 
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEnqCardNo;
                _CommonSearch.ShowDialog();
                txtEnqCardNo.Select();
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


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.LoyaltyCustomer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loyalty_Type:
                    {
                        paramsText.Append(BaseCls.GlbUserDefProf + seperator + DateTime.Now.ToString("dd/MMM/yyyy") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LoyaltyCardNo: {
                    paramsText.Append(seperator);
                    break;
                }
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {

                        DateTime _date = CHNLSVC.Security.GetServerDateTime();
                        if (tabControl1.SelectedIndex == 2)
                        {
                            paramsText.Append(txtTrnCusCode.Text + seperator + _date.Date.ToString("d") + seperator);
                            break;
                        }
                        if (tabControl1.SelectedIndex == 3) {
                            paramsText.Append(txtEnqCusCode.Text + seperator + _date.Date.ToString("d") + seperator);
                            break;
                        }
                        if (tabControl1.SelectedIndex == 1) {
                            paramsText.Append(txtRenCusCode.Text + seperator + _date.Date.ToString("d") + seperator);
                            break;
                        }
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void LoadMember(string _cusCode,string _card)
        {
            if (_cusCode != "" && _card != "")
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                List<LoyaltyMemeber> _member = CHNLSVC.Sales.GetCustomerLoyality(_cusCode, _card, _date.Date);
                if (_member != null && _member.Count > 0)
                {
                    if (tabControl1.SelectedIndex == 2)
                    {

                        lblTrnCusCode.Text = _member[0].Salcm_cus_cd;
                        lblTrnCusSpec.Text = _member[0].Salcm_cus_spec;
                        lblTrnBalance.Text = _member[0].Salcm_bal_pt.ToString();
                        lblTrnCollect.Text = _member[0].Salcm_col_pt.ToString();
                        lblTrnDisRate.Text = _member[0].Salcm_dis_rt.ToString();
                        lblTrnLoyaltyType.Text = _member[0].Salcm_loty_tp;
                        lblTrnRedeem.Text = _member[0].Salcm_red_pt.ToString();

                    }
                    if (tabControl1.SelectedIndex == 3)
                    {

                        lblCusCode.Text = _member[0].Salcm_cus_cd;
                        lblEnqCusSpec.Text = _member[0].Salcm_cus_spec;
                        lblEnqBalPt.Text = _member[0].Salcm_bal_pt.ToString();
                        lblEnqColPt.Text = _member[0].Salcm_col_pt.ToString();
                        lblEnqDisRt.Text = _member[0].Salcm_dis_rt.ToString();
                        lblEnqLotyType.Text = _member[0].Salcm_loty_tp;
                        lblEnqRedPt.Text = _member[0].Salcm_red_pt.ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Invalid customer code/ card number combination", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        #region main buttons

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                //tab 01
                txtLoyaltyType.Text = "";
                txtCardSerial.Text = "";
                txtCusCode.Text = "";
                txtDiscountRate.Text = "0";
                txtEmail.Text = "";
                txtPoints.Text = "0";
                txtMembershipChg.Text = "0";
                txtContact.Text = "";
                dtFrom.Value = _date;
                dtTo.Value = _date;
                lblCusCode.Text = "";
                lblCusName.Text = "";
                lblAddress.Text = "";


                //tab 02
                txtCardNo.Text = "";
                txtRenewalChg.Text = "";
                dtRenFrom.Value = _date;
                dtRenTo.Value = _date;
                lblCusCode.Text = "";
                lblBalance.Text = "";
                lblCollectedPoints.Text = "";
                lblCusSpec.Text = "";
                lblDiscount.Text = "";
                lblLoyaltyType.Text = "";
                lblRedeem.Text = "";
                txtRenCusCode.Text = "";

                lblRenCusAdd.Text = "";
                lblRenCusName.Text = "";




                //tab 03
                txtTrnCardNo.Text = "";
                txtTrnCardSerial.Text = "";
                txtTrnCusCode.Text = "";
                lblTrnBalance.Text = "";
                lblTrnCollect.Text = "";
                lblTrnCusCode.Text = "";
                lblTrnCusSpec.Text = "";
                lblTrnDisRate.Text = "";
                lblTrnLoyaltyType.Text = "";
                lblTrnRedeem.Text = "";
                ucPayModes1.ClearControls();
                ucPayModes1.InvoiceType = "LOYALTY";
                ucPayModes1.TotalAmount = 0;
                ucPayModes2.ClearControls();
                ucPayModes2.InvoiceType = "LOYALTY";
                ucPayModes2.TotalAmount = 0;

                lblTrnCusAdd.Text = "";
                lblYrnCusName.Text = "";

                //tab 03
                txtEnqCusCode.Text = "";
                txtEnqCardNo.Text = "";
                lblEnqBalPt.Text = "";
                lblEnqColPt.Text = "";
                lblEnqCus.Text = "";
                lblEnqCusSpec.Text = "";
                lblEnqDisRt.Text = "";
                lblEnqLotyType.Text = "";
                lblEnqRedPt.Text = "";

                lblEnqCusName.Text = "";
                lblEnqCusAdd.Text = "";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Process();
                this.Cursor = Cursors.Default;
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

        #endregion

        private void Process()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {

                    #region validation

                    
                    if (dtFrom.Value.Date > dtTo.Value.Date)
                    {
                        MessageBox.Show("To date has to be greater than From date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    txtLoyaltyType_Leave(null, null);
                    if (txtLoyaltyType.Text == "")
                    {
                        MessageBox.Show("Please select Loyalty Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //if (txtCardSerial.Text == "")
                    //{
                    //    MessageBox.Show("Please enter Card Serial", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    if (txtCusCode.Text == "")
                    {
                        MessageBox.Show("Please select Customer Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (ucPayModes1.Balance > 0)
                    {
                        MessageBox.Show("You have to pay membership charge before save", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    LoyaltyMemeber _tmember = CHNLSVC.Sales.ValidateLoyaltyMember(txtCardSerial.Text, txtCusCode.Text, txtLoyaltyType.Text);
                    if (_tmember != null) {
                        MessageBox.Show("This customer already has this card serial", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //generate message
                    List<LoyaltyMemeber> _loy = CHNLSVC.Sales.GetCustomerLoyality(txtCusCode.Text, null, DateTime.Now);
                    if (_loy != null && _loy.Count > 0) {
                        //string st = _loy.Select(x => x.Salcm_no);
                        List<LoyaltyMemeber> _mem = (from res in _loy
                                                     where res.Salcm_loty_tp == txtLoyaltyType.Text
                                                     select res).ToList<LoyaltyMemeber>();
                        if (_mem != null && _mem.Count > 0)
                        {
                            MessageBox.Show(lblCusName.Text + " already has loyalty card.\nCard No : " + _mem[0].Salcm_no + "\nLoyalty Type : " + txtLoyaltyType.Text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    #endregion

                    MasterAutoNumber _cardAuto = new MasterAutoNumber();
                    _cardAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                    _cardAuto.Aut_cate_tp = "COM";
                    _cardAuto.Aut_start_char = "LO";
                    _cardAuto.Aut_direction = 0;
                    _cardAuto.Aut_modify_dt = null;
                    _cardAuto.Aut_moduleid = "LOTY";
                    _cardAuto.Aut_number = 0;
                    _cardAuto.Aut_year = null;

                    MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                    _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _receiptAuto.Aut_cate_tp = "PC";
                    _receiptAuto.Aut_direction = 1;
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto.Aut_moduleid = "RECEIPT";
                    _receiptAuto.Aut_number = 0;
                    _receiptAuto.Aut_start_char = "LOYA";
                    _receiptAuto.Aut_year = null;

                    LoyaltyMemeber _member = new LoyaltyMemeber();
                    _member.Salcm_loty_tp = txtLoyaltyType.Text;
                    _member.Salcm_email = txtEmail.Text;
                    _member.Salcm_contact = txtContact.Text;
                    _member.Salcm_cus_cd = txtCusCode.Text;
                    _member.Salcm_dis_rt = Convert.ToDecimal(txtDiscountRate.Text);
                    _member.Salcm_val_frm = dtFrom.Value.Date;
                    _member.Salcm_val_to = dtTo.Value.Date;
                    _member.Salcm_cre_by = BaseCls.GlbUserID;
                    _member.Salcm_cre_dt = _date;
                    _member.Salcm_app_by = BaseCls.GlbUserID;
                    _member.Salcm_app_dt = _date;
                    _member.Salcm_bal_pt = Convert.ToDecimal(txtPoints.Text);
                    _member.Salcm_cd_ser = txtCardSerial.Text;
                    _member.Salcm_cus_spec = "CLASSIC";

                    RecieptHeader _reciept = new RecieptHeader();
                    _reciept.Sar_receipt_type = "";
                    _reciept.Sar_act = true;
                    _reciept.Sar_receipt_type = "LOYA";

                    //get customer details
                    MasterBusinessEntity _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCusCode.Text, null, null, "C");
                    if (_entity.Mbe_cd != null)
                    {
                        _reciept.Sar_debtor_cd = _entity.Mbe_acc_cd;
                        _reciept.Sar_debtor_name = _entity.Mbe_name;
                        _reciept.Sar_debtor_add_1 = _entity.Mbe_add1;
                        _reciept.Sar_debtor_add_2 = _entity.Mbe_add2;
                    }
                    _reciept.Sar_receipt_date = _date.Date;
                    _reciept.Sar_create_by = BaseCls.GlbUserID;
                    _reciept.Sar_create_when = _date;
                    _reciept.Sar_direct = true;
                    _reciept.Sar_tot_settle_amt = Convert.ToDecimal(txtMembershipChg.Text);

                    string cardNo = "";
                    string recieptNo = "";
                    string serialNo = "";
                    int effect = CHNLSVC.Sales.SaveLoyaltyMembership(_member, _cardAuto, _receiptAuto, ucPayModes1.RecieptItemList, _reciept, out cardNo, out recieptNo,out serialNo,BaseCls.GlbUserDefProf);

                    MessageBox.Show("Loyalty Membership details saved successfully.\nCard No - " + cardNo+"\nCare Serial - "+serialNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //print receipt
                    if (!string.IsNullOrEmpty(recieptNo))
                    {
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        BaseCls.GlbReportTp = "REC";
                        _view.GlbReportName = "ReceiptPrints.rpt";
                        BaseCls.GlbReportName = "ReceiptPrints.rpt";
                        _view.GlbReportDoc = recieptNo;
                        _view.Show();
                        _view = null;
                    }
                    btnClear_Click(null, null);
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    #region validate
                    if (dtRenFrom.Value.Date > dtRenTo.Value.Date)
                    {
                        MessageBox.Show("To date has to be greater than From date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    txtCardNo_Leave(null, null);
                    if (txtCardNo.Text == "") {
                        MessageBox.Show("Please select loyalty Card", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (ucPayModes2.Balance > 0)
                    {
                        MessageBox.Show("You have to pay Renewal charge before save", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    #endregion

                    LoyaltyMemeber _member = CHNLSVC.Sales.GetLoyaltyMemberByCard(txtCardNo.Text);
                    if (_member != null)
                    {

                        _member.Salcm_val_to = dtRenTo.Value.Date;

                        MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                        _receiptAuto.Aut_cate_tp = "PC";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "RECEIPT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "LOYA";
                        _receiptAuto.Aut_year = null;

                        RecieptHeader _reciept = new RecieptHeader();
                        _reciept.Sar_act = true;
                        _reciept.Sar_receipt_type = "LOYA";

                        //get customer details
                        MasterBusinessEntity _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCusCode.Text, null, null, "C");
                        if (_entity.Mbe_cd != null)
                        {
                            _reciept.Sar_debtor_cd = _entity.Mbe_acc_cd;
                            _reciept.Sar_debtor_name = _entity.Mbe_name;
                            _reciept.Sar_debtor_add_1 = _entity.Mbe_add1;
                            _reciept.Sar_debtor_add_2 = _entity.Mbe_add2;
                        }
                        _reciept.Sar_receipt_date = _date.Date;
                        _reciept.Sar_create_by = BaseCls.GlbUserID;
                        _reciept.Sar_create_when = _date;
                        _reciept.Sar_direct = true;
                        _reciept.Sar_tot_settle_amt = Convert.ToDecimal(txtRenewalChg.Text);

                        string recieptno = "";
                        int effect = CHNLSVC.Sales.SaveLoyaltyRenewal(_member, _receiptAuto, _reciept, ucPayModes2.RecieptItemList, out recieptno);
                        MessageBox.Show("Loyalty Membership details updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //print receipt
                        if (!string.IsNullOrEmpty(recieptno))
                        {
                            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                            BaseCls.GlbReportTp = "REC";
                            _view.GlbReportName = "ReceiptPrints.rpt";
                            BaseCls.GlbReportName = "ReceiptPrints.rpt";
                            _view.GlbReportDoc = recieptno;
                            _view.Show();
                            _view = null;
                        }
                        btnClear_Click(null, null);
                    }
                    else {
                        MessageBox.Show("Unexpected error occurred.\nLoyalty Member not load", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if(tabControl1.SelectedIndex==2)
                {
                    #region validation
                    if (txtTrnCardNo.Text == "") {
                        MessageBox.Show("Please select card no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtTrnCardSerial.Text == "") {
                        MessageBox.Show("Please enter card serial no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtTrnCusCode.Text == "") {
                        MessageBox.Show("Please select customer code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    LoyaltyMemeber _temember = CHNLSVC.Sales.GetLoyaltyMemberByCard(txtTrnCardNo.Text);
                    if (_temember != null)
                    {
                        if (_temember.Salcm_cus_cd != txtTrnCusCode.Text) {
                            MessageBox.Show("Customer code and card number mismatch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else {
                        MessageBox.Show("Unexpected error occurred.\nLoyalty Member not load", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    #endregion

                    LoyaltyMemeber _member = CHNLSVC.Sales.GetLoyaltyMemberByCard(txtTrnCardNo.Text);
                    if (_member != null)
                    {
                        MasterAutoNumber _auto = new MasterAutoNumber();
                        _auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                        _auto.Aut_cate_tp = "PC";
                        _auto.Aut_start_char = "LO";
                        _auto.Aut_direction = 0;
                        _auto.Aut_modify_dt = null;
                        _auto.Aut_moduleid = "LOTY";
                        _auto.Aut_number = 0;
                        _auto.Aut_year = null;
     
                        

                        LoyaltyMemberLog _loyalLog = new LoyaltyMemberLog();
                        _loyalLog.Sacml_bal_pt = _member.Salcm_bal_pt;
                        //get old card serial
                        _loyalLog.Sacml_cd_ser = _member.Salcm_cd_ser;
                        _loyalLog.Sacml_col_pt = _member.Salcm_col_pt;
                        _loyalLog.Sacml_cus_cd = _member.Salcm_cus_cd.ToUpper();
                        _loyalLog.Sacml_cus_spec = _member.Salcm_cus_spec.ToUpper();
                        _loyalLog.Sacml_dis_rt = _member.Salcm_dis_rt;
                        _loyalLog.Sacml_exp_pt = _member.Salcm_exp_pt;
                        _loyalLog.Sacml_loty_tp = _member.Salcm_loty_tp;
                        _loyalLog.Sacml_no = _member.Salcm_no;
                        _loyalLog.Sacml_red_pt = _member.Salcm_red_pt;
                        _loyalLog.Sacml_cre_by = BaseCls.GlbUserID;
                        _loyalLog.Sacml_cre_dt = _date;

                        _member.Salcm_cd_ser = txtTrnCardSerial.Text;

                        bool result = CHNLSVC.Sales.UpdateLoyaltyMembership(_member, _auto, _loyalLog);

                        MessageBox.Show("Loyalty Member history transfer successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else {
                        MessageBox.Show("Unexpected error occurred.\nLoyalty Member not load", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error occurred while processing.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }
        
        #region tab 01 key down events
        private void txtLoyaltyType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCusCode.Focus();
            if (e.KeyCode == Keys.F2)
                btnLoyaltyType_Click(null, null);
        }

        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCardSerial.Focus();
            if (e.KeyCode == Keys.F2)
                btnCusCode_Click(null, null);
        }

        private void txtCardSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtFrom.Focus();
        }

        private void dtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtContact.Focus();
        }

        private void txtContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtEmail.Focus();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPoints.Focus();
        }

        private void txtPoints_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDiscountRate.Focus();
        }

        private void txtDiscountRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ucPayModes1.PayModeCombo.Focus();
        } 
        #endregion

        #region tab 02 key down event
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtRenFrom.Focus();
            if (e.KeyCode == Keys.F2)
                btnCardNo_Click(null, null);
        }

        private void dtRenFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtRenTo.Focus();
        }

        private void dtRenTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ucPayModes2.PayModeCombo.Focus();
        } 
        #endregion

        #region tab 03 key down event
        private void txtTrnCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTrnCardNo.Focus();
            if (e.KeyCode == Keys.F2)
                btnTrnCusCode_Click(null, null);
        }

        private void txtTrnCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTrnCardSerial.Focus();
            if (e.KeyCode == Keys.F2)
                btnTrnCardNo_Click(null, null);
        }

        private void txtTrnCardSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        } 
        #endregion

        #region mouse double click

        private void txtLoyaltyType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnLoyaltyType_Click(null, null);
        }

        private void txtCusCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCusCode_Click(null, null);
        }

        private void txtCardNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCardNo_Click(null, null);
        }

        private void txtTrnCusCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnTrnCusCode_Click(null, null);
        }

        private void txtTrnCardNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnTrnCardNo_Click(null, null);
        } 


        #endregion

        #region validation
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            try
            {
                //email validation
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    Boolean _isValid = IsValidEmail(txtEmail.Text.Trim());

                    if (_isValid == false)
                    {
                        MessageBox.Show("Invalid email address.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmail.Text = "";
                        txtEmail.Focus();
                        return;
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

        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            try
            {
                //validate cus code
                if (txtCusCode.Text != "")
                {

                    MasterBusinessEntity _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCusCode.Text, null, null, "C");
                    if (string.IsNullOrEmpty(_entity.Mbe_cd))
                    {
                        MessageBox.Show("Invalid customer code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCusCode.Text = "";
                        return;
                    }
                    else
                    {
                        txtContact.Text = _entity.Mbe_contact;
                        txtEmail.Text = _entity.Mbe_email;

                        lblCustomerCode.Text = _entity.Mbe_cd;
                        lblCusName.Text = _entity.Mbe_name;
                        lblAddress.Text = _entity.Mbe_add1 + " " + _entity.Mbe_add2;

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

        private void txtLoyaltyType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtLoyaltyType.Text != "")
                {
                    LoyaltyType _loyalty = CHNLSVC.Sales.GetLoyaltyType(txtLoyaltyType.Text);
                    if (_loyalty != null)
                    {
                        dtTo.Value = dtFrom.Value.AddDays(_loyalty.Salt_valid);
                        txtMembershipChg.Text = _loyalty.Salt_memb_chg.ToString();
                        if (ucPayModes1.TotalAmount != _loyalty.Salt_memb_chg)
                        {
                            ucPayModes1.ClearControls();
                            ucPayModes1.InvoiceType = "LOYALTY";
                            ucPayModes1.TotalAmount = _loyalty.Salt_memb_chg;
                            ucPayModes1.LoadData();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid loyalty type code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLoyaltyType.Text = "";
                        return;
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

        private void txtCardNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCardNo.Text != "")
                {
                    LoyaltyMemeber _member = CHNLSVC.Sales.GetLoyaltyMemberByCard(txtCardNo.Text);
                    if (_member != null)
                    {
                        lblCusCode.Text = _member.Salcm_cus_cd;
                        lblCusSpec.Text = _member.Salcm_cus_spec;
                        lblBalance.Text = _member.Salcm_bal_pt.ToString();
                        lblCollectedPoints.Text = _member.Salcm_col_pt.ToString();
                        lblDiscount.Text = _member.Salcm_dis_rt.ToString();
                        lblLoyaltyType.Text = _member.Salcm_loty_tp;
                        lblRedeem.Text = _member.Salcm_red_pt.ToString();

                        //customer details
                        MasterBusinessEntity _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _member.Salcm_cus_cd, null, null, "C");
                        if (string.IsNullOrEmpty(_entity.Mbe_cd))
                        {
                            MessageBox.Show("Invalid customer code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {


                            lblRenCusName.Text = _entity.Mbe_name;
                            lblRenCusAdd.Text = _entity.Mbe_add1 + " " + _entity.Mbe_add2;

                        }


                        dtRenFrom.Value = _member.Salcm_val_to;


                        //get renewal chg
                        LoyaltyType _loyal = CHNLSVC.Sales.GetLoyaltyType(_member.Salcm_loty_tp);
                        if (_loyal != null)
                        {
                            txtRenewalChg.Text = _loyal.Salt_renew_chg.ToString();
                            dtRenTo.Value = dtRenFrom.Value.AddDays(_loyal.Salt_valid);
                            //pay mode
                            if (ucPayModes2.TotalAmount != _loyal.Salt_renew_chg)
                            {
                                ucPayModes2.ClearControls();
                                ucPayModes2.InvoiceType = "LOYALTY";
                                ucPayModes2.TotalAmount = _loyal.Salt_renew_chg;
                                ucPayModes2.LoadData();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Loyalty type not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid loyalty card number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
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
        #endregion

        #region key press events
        private void txtPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDiscountRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        } 
        #endregion

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEnqCusCode.Text) || string.IsNullOrEmpty(txtEnqCardNo.Text))
                {
                    return;
                }

                LoyaltyMemeber _temember = CHNLSVC.Sales.GetLoyaltyMemberByCard(txtEnqCardNo.Text);
                if (_temember != null)
                {
                    if (_temember.Salcm_cus_cd != txtEnqCusCode.Text)
                    {
                        MessageBox.Show("Customer code and card number mismatch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Loyalty Member not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                LoadMember(txtEnqCusCode.Text, txtEnqCardNo.Text);
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

        private void lnkCus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCusCode;
                _CusCre.ShowDialog();
                txtCusCode.Select();
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

        private void txtTrnCusCode_Leave(object sender, EventArgs e)
        {
            try
            {
                MasterBusinessEntity _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtTrnCusCode.Text, null, null, "C");
                if (string.IsNullOrEmpty(_entity.Mbe_cd))
                {
                    MessageBox.Show("Invalid customer code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {


                    lblYrnCusName.Text = _entity.Mbe_name;
                    lblTrnCusAdd.Text = _entity.Mbe_add1 + " " + _entity.Mbe_add2;

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

        private void txtEnqCusCode_Leave(object sender, EventArgs e)
        {
            try
            {
                MasterBusinessEntity _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtEnqCusCode.Text, null, null, "C");
                if (string.IsNullOrEmpty(_entity.Mbe_cd))
                {
                    MessageBox.Show("Invalid customer code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {


                    lblEnqCusName.Text = _entity.Mbe_name;
                    lblEnqCusAdd.Text = _entity.Mbe_add1 + " " + _entity.Mbe_add2;

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

        private void btnRenCusCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true; 
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoyaltyCustomer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCodeLoyalty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRenCusCode;
                _CommonSearch.ShowDialog();
               
                txtRenCusCode.Select();
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

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtLoyaltyType.Text != "")
                {
                    LoyaltyType _loyalty = CHNLSVC.Sales.GetLoyaltyType(txtLoyaltyType.Text);
                    if (_loyalty != null)
                    {
                        dtTo.Value = dtFrom.Value.AddDays(_loyalty.Salt_valid);
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

        private void dtRenFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCardNo.Text != "")
                {
                    LoyaltyMemeber _member = CHNLSVC.Sales.GetLoyaltyMemberByCard(txtCardNo.Text);
                    if (_member != null)
                    {

                        LoyaltyType _loyalty = CHNLSVC.Sales.GetLoyaltyType(_member.Salcm_loty_tp);
                        if (_loyalty != null)
                        {
                            dtRenTo.Value = dtRenFrom.Value.AddDays(_loyalty.Salt_valid);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //check permission
                if (tabControl1.SelectedIndex == 0)
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10007))
                    {
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }

                }
                if (tabControl1.SelectedIndex == 1)
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10008))
                    {
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }

                }
                if (tabControl1.SelectedIndex == 2)
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10009))
                    {
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }

                }
                if (tabControl1.SelectedIndex == 3)
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10010))
                    {
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
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

        private void LoyaltyMembership_Load(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(null, null);
        }

      


    }

        
}
