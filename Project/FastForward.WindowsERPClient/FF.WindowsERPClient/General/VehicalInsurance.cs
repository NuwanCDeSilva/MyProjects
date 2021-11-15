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
    public partial class VehicalInsurance : Base
    {
        string type = "";
        decimal DebitTotal = 0;
        string VehicalNo = "";
        DateTime AccidentDate = DateTime.Now;
        string invtype = "";
        public VehicalInsurance()
        {
            InitializeComponent();
        }

        private void VehicalInsurance_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPreviousClaims.AutoGenerateColumns = false;
                dataGridViewSearch.AutoGenerateColumns = false;
                type = "Cover";
                BindDistrict(cmbDistrict);
                cmbDistrict_SelectionChangeCommitted(null, null);
                LoadRecievDocCombo();
                LoadCustomerSettlementCombo();
                cmbRegNo_SelectionChangeCommitted(null, null);
                txtCompany.Text = BaseCls.GlbUserComCode;
                // txtPC.Text = BaseCls.GlbUserDefProf;
                txtDebitCompany.Text = BaseCls.GlbUserComCode;
                //txtDebitPC.Text = BaseCls.GlbUserDefProf;
                txtRecieptNo.Select();
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                dateTimePickerFrom.Value = _date.AddMonths(-1);
                dateTimePickerDebitFrom.Value = _date.AddMonths(-1);

                //date range set
                txtNoOfDays_Leave(null, null);
                dateTimePickerExpireNo.Value = _date.AddYears(1);

                ucPayModes1.InvoiceType = "CS";
                ucPayModes1.Allow_Plus_balance = true;
                ucPayModes1.TotalAmount = 0;
                ucPayModes1.LoadData();
                ucPayModes1.IsZeroAllow = true;
                dateTimePickerFrom.Value = DateTime.Now.AddYears(-5);
                dateTimePickerDebitFrom.Value = DateTime.Now.AddYears(-5);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #region main button events

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Nadeeeka 04-03-2015 (Permission checking added)
                //if (invtype == "HS")  9/6/2016 COMENTED AS PER CHAMINDA AND ZEENIA
                //{
                //    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10103))
                //    {
                //        MessageBox.Show("Sorry, You have no permission to print cover note for Hire sales !\n( Advice: Required permission code :10103)");
                //        return;
                //    }
                //}


                //kapila 20/5/2016 check allow limit for DO without approval is exceeded





                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;


                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                string _insComp = "";
                if (tabControl1.SelectedIndex == 0)
                {
                    if (tabControl2.SelectedIndex == 0)
                    {
                        List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssInsCovNot", txtRecieptNo.Text);
                        if (_vehins != null)
                        {
                            txtNoOfDays_Leave(null, null);
                            int tem;
                            decimal temVal;
                            if (!int.TryParse(txtNoOfDays.Text, out tem))
                            {
                                MessageBox.Show("No of days required and has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (txtCoverNoteNo.Text == "")
                            {
                                MessageBox.Show("Please enter cover note no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!Decimal.TryParse(txtPremValue.Text, out temVal))
                            {
                                MessageBox.Show("Insurance value is required and has to be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }


                            //kapila 20/5/2016 check allow limit for DO without approval is exceeded
                            decimal _maxDO = 0;
                            decimal _maxDoDays = 0;

                            HpSystemParameters _System_Para = new HpSystemParameters();
                            _System_Para = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "HSMAXDO", Convert.ToDateTime(DateTime.Now.Date).Date);
                            if (_System_Para.Hsy_cd != null)
                            {
                                _maxDoDays = _System_Para.Hsy_val;
                            }
                            if (_System_Para.Hsy_cd == null)
                            {
                                _System_Para = CHNLSVC.Sales.GetSystemParameter("SCHNL", BaseCls.GlbDefSubChannel, "HSMAXDO", Convert.ToDateTime(DateTime.Now.Date).Date);
                                if (_System_Para.Hsy_cd != null)
                                {
                                    _maxDoDays = _System_Para.Hsy_val;
                                }
                            }
                            if (_System_Para.Hsy_cd == null)
                            {
                                _System_Para = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "HSMAXDO", Convert.ToDateTime(DateTime.Now.Date).Date);
                                if (_System_Para.Hsy_cd != null)
                                {
                                    _maxDoDays = _System_Para.Hsy_val;
                                }
                            }
                            if (_maxDoDays > 0)
                            {
                                DataTable _dt = CHNLSVC.Financial.IsDoDaysExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(_maxDoDays));
                                if (_dt.Rows.Count > 0)
                                {
                                    MessageBox.Show("Cannot create A/C.Exceed the allowed no of days from registration for " + _dt.Rows[0]["sah_inv_no"].ToString() + ". Please contact Registration Department.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }


                            _System_Para = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "HSDO", Convert.ToDateTime(DateTime.Now.Date).Date);
                            if (_System_Para.Hsy_cd != null)
                            {
                                _maxDO = _System_Para.Hsy_val;
                            }
                            if (_System_Para.Hsy_cd == null)
                            {
                                _System_Para = CHNLSVC.Sales.GetSystemParameter("SCHNL", BaseCls.GlbDefSubChannel, "HSDO", Convert.ToDateTime(DateTime.Now.Date).Date);
                                if (_System_Para.Hsy_cd != null)
                                {
                                    _maxDO = _System_Para.Hsy_val;
                                }
                            }
                            if (_System_Para.Hsy_cd == null)
                            {
                                _System_Para = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "HSDO", Convert.ToDateTime(DateTime.Now.Date).Date);
                                if (_System_Para.Hsy_cd != null)
                                {
                                    _maxDO = _System_Para.Hsy_val;
                                }
                            }
                            if (_maxDO > 0)
                            {
                                decimal _totsaleqty = 0;
                                int _effc = CHNLSVC.Financial.GetTotSadQty(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, out _totsaleqty);
                                if (_maxDO <= _totsaleqty)
                                {
                                    MessageBox.Show("Exceed the allowed no of DCNs without having registration approval. Please contact Registration Department.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            //if (_vehins[0].Svit_cvnt_issue == 2) {
                            //    MessageBox.Show("Canceled Receipt can not add cove note", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}
                            try
                            {
                                _vehins[0].Svit_ins_com = txtInsCompany.Text;
                                _vehins[0].Svit_ins_polc = txtInsPolicy.Text;

                                _vehins[0].Svit_cust_cd = txtCusCode.Text;
                                _vehins[0].Svit_cust_title = cmbCustomerTitle.SelectedItem.ToString();
                                _vehins[0].Svit_last_name = txtLastName.Text;
                                _vehins[0].Svit_full_name = txtFullName.Text;
                                _vehins[0].Svit_initial = txtInitials.Text;
                                _vehins[0].Svit_add01 = txtAdd1.Text;
                                _vehins[0].Svit_add02 = txtAdd2.Text;
                                _vehins[0].Svit_city = txtCity.Text;
                                _vehins[0].Svit_district = cmbDistrict.SelectedValue.ToString();
                                _vehins[0].Svit_province = txtProvince.Text;
                                _vehins[0].Svit_contact = txtContact.Text;
                                //TextBoxMake.Text=_vehins.m
                                _vehins[0].Svit_model = txtModal.Text;
                                _vehins[0].Svit_capacity = txtCapacity.Text;

                                _vehins[0].Svit_cvnt_no = txtCoverNoteNo.Text;
                                _vehins[0].Svit_ins_val = Convert.ToDecimal(txtPremValue.Text);
                                _vehins[0].Svit_cvnt_issue = 1;
                                _vehins[0].Svit_cvnt_by = BaseCls.GlbUserID;
                                _vehins[0].Svit_cvnt_days = Convert.ToInt32(txtNoOfDays.Text);
                                _vehins[0].Svit_cvnt_from_dt = dateTimePickerCoverNoteFrom.Value;
                                _vehins[0].Svit_cvnt_to_dt = dateTimePickerCoverNoteTo.Value;
                                _vehins[0].Svit_cvnt_dt = DateTime.Now;
                                _vehins[0].Svit_no_of_prnt = _vehins[0].Svit_no_of_prnt + 1;
                                List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);

                                MasterAutoNumber _receiptAuto_1 = new MasterAutoNumber();

                                _receiptAuto_1.Aut_cate_cd = txtInsCompany.Text; //kapila 11/3/2014
                                _receiptAuto_1.Aut_cate_tp = "COVNT";
                                _receiptAuto_1.Aut_start_char = "ABN";
                                _receiptAuto_1.Aut_direction = 0;
                                _receiptAuto_1.Aut_moduleid = "COVNT";
                                _receiptAuto_1.Aut_number = 0;
                                _receiptAuto_1.Aut_year = DateTime.Now.Date.Year;

                                //_vehins[0].Svit_ext_no=ECN
                                CHNLSVC.General.SaveVehicalInsurance(_vehins[0], _receiptAuto_1);

                                //update status
                                if (list != null)
                                    CHNLSVC.General.UpdateCoverNote(_vehins[0].Svit_inv_no, list[0].Sad_itm_cd, BaseCls.GlbUserComCode);
                                else
                                {
                                    List<QoutationDetails> _recallList = new List<QoutationDetails>();  //kapila 25/1/2016
                                    _recallList = CHNLSVC.Sales.Get_all_linesForQoutation(_vehins[0].Svit_inv_no);
                                    if (_recallList != null)
                                        CHNLSVC.General.UpdateCoverNote(_vehins[0].Svit_inv_no, _recallList[0].Qd_itm_cd, BaseCls.GlbUserComCode);
                                }
                                _insComp = txtInsCompany.Text;
                                ClearCustomerAndVehicalDetails();
                                ClearIssueCoverNote();
                                ClearSearchDetails();
                                MessageBox.Show("Record updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //report
                                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                                if (_insComp == "CN001")
                                    BaseCls.GlbReportName = "InsuranceCoverNote.rpt";

                                if (_insComp == "MBSL")
                                    BaseCls.GlbReportName = "InsuranceCoverNoteMBSL.rpt";

                                if (_insComp == "UAL01")
                                    BaseCls.GlbReportName = "InsuranceCoverNoteUMS.rpt";

                                if (_insComp == "JS001")
                                    BaseCls.GlbReportName = "InsuranceCoverNoteJS.rpt";

                                if (_insComp == "AIA")
                                    BaseCls.GlbReportName = "InsuranceCoverNoteAIA.rpt";

                                BaseCls.GlbReportDoc = _vehins[0].Svit_ref_no;
                                _view.Show();
                                _view = null;


                            }
                            catch (Exception er)
                            {
                                MessageBox.Show("Error occurred while processing!!\n" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CHNLSVC.CloseChannel();
                            }
                        }
                    }
                    else if (tabControl2.SelectedIndex == 1)
                    {
                        List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("ExtCovNot", txtRecieptNo.Text);
                        if (_vehins != null)
                        {
                            //validation
                            int tem;
                            if (!int.TryParse(txtExtendDays.Text, out tem))
                            {
                                MessageBox.Show("No of days required and has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            try
                            {
                                dateTimePickerExtendTo.Value = dateTimePickerExtendFrom.Value.AddDays(Convert.ToInt32(txtExtendDays.Text));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Unexpected error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (_vehins[0].Svit_cvnt_issue == 2)
                            {
                                MessageBox.Show("Canceled Receipt can not extend cove note", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            try
                            {

                                _vehins[0].Svit_ext_issue = true;
                                _vehins[0].Svit_ext_by = BaseCls.GlbUserID;
                                _vehins[0].Svit_ext_days = Convert.ToInt32(txtExtendDays.Text);
                                _vehins[0].Svit_ext_from_dt = dateTimePickerExtendFrom.Value;
                                _vehins[0].Svit_ext_to_dt = dateTimePickerExtendTo.Value;
                                _vehins[0].Svit_ext_no = txtExtCoverNote.Text;
                                _vehins[0].Svit_ext_dt = DateTime.Now;
                                //_vehins[0].Svit_ext_no=ECN
                                CHNLSVC.General.SaveVehicalInsurance(_vehins[0], null);
                                List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);
                                CHNLSVC.General.UpdateCoverNote(_vehins[0].Svit_inv_no, list[0].Sad_itm_cd, BaseCls.GlbUserComCode);

                                _insComp = txtInsCompany.Text;
                                ClearCustomerAndVehicalDetails();
                                ClearExtend();
                                ClearSearchDetails();

                                MessageBox.Show("Record updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //report
                                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                                //BaseCls.GlbReportName = "InsuranceCoverNote.rpt";
                                if (_insComp == "CN001")
                                    BaseCls.GlbReportName = "InsuranceCoverNote.rpt";

                                if (_insComp == "MBSL")
                                    BaseCls.GlbReportName = "InsuranceCoverNoteMBSL.rpt";

                                if (_insComp == "UAL01")
                                    BaseCls.GlbReportName = "InsuranceCoverNoteUMS.rpt";

                                if (_insComp == "JS001")
                                    BaseCls.GlbReportName = "InsuranceCoverNoteJS.rpt";

                                if (_insComp == "AIA")
                                    BaseCls.GlbReportName = "InsuranceCoverNoteAIA.rpt";

                                BaseCls.GlbReportDoc = _vehins[0].Svit_ref_no;
                                _view.Show();
                                _view = null;
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show("Error occurred while processing!!\n" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CHNLSVC.CloseChannel();
                            }
                        }
                    }
                    else
                    {
                        List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssCer", txtRecieptNo.Text);
                        if (_vehins != null)
                        {
                            decimal temVal;
                            if (txtPolicyNo.Text == "")
                            {
                                MessageBox.Show("Please enter policy no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (txtDebitNoteNo.Text == "")
                            {
                                MessageBox.Show("Please enter debit no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!Decimal.TryParse(txtNetPremium.Text, out temVal))
                            {
                                MessageBox.Show("Net Premium is required and has to be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!Decimal.TryParse(txtSRCCPre.Text, out temVal))
                            {
                                MessageBox.Show("SRCC Premium is required and has to be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!Decimal.TryParse(txtOther.Text, out temVal))
                            {
                                MessageBox.Show("Other value is required and has to be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (_vehins[0].Svit_cvnt_issue == 2)
                            {
                                MessageBox.Show("Canceled Receipt can not issue certificate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (lblVehNo.Text == "")
                            {
                                MessageBox.Show("Can not issue certificate without vehicle registration no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            //if (TextBoxRegDate.Text == "") {
                            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter reg date");
                            //    return;
                            //}
                            try
                            {
                                _vehins[0].Svit_net_prem = Convert.ToDecimal(txtNetPremium.Text);
                                _vehins[0].Svit_oth_val = Convert.ToDecimal(txtOther.Text);
                                _vehins[0].Svit_srcc_prem = Convert.ToDecimal(txtSRCCPre.Text);
                                _vehins[0].Svit_tot_val = Convert.ToDecimal(txtTotal.Text);
                                _vehins[0].Svit_polc_by = BaseCls.GlbUserID;
                                _vehins[0].Svit_polc_dt = _date;
                                _vehins[0].Svit_polc_no = txtPolicyNo.Text;
                                _vehins[0].Svit_expi_dt = dateTimePickerExpireNo.Value;
                                _vehins[0].Svit_eff_dt = dateTimePickerEffectiveDate.Value;
                                _vehins[0].Svit_file_no = txtFileNo.Text;
                                _vehins[0].Svit_dbt_no = txtDebitNoteNo.Text;
                                _vehins[0].Svit_polc_stus = true;
                                _vehins[0].Svit_reg_dt = Convert.ToDateTime(lblRegDate.Text);
                                _vehins[0].Svit_veh_reg_no = lblVehNo.Text;
                                CHNLSVC.General.SaveVehicalInsurance(_vehins[0], null);
                                ClearCustomerAndVehicalDetails();
                                ClearIssueCertificate();
                                ClearSearchDetails();
                                MessageBox.Show("Record updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show("Error occurred while processing!!\n" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CHNLSVC.CloseChannel();

                            }
                        }
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {

                    if (ucPayModes1.TotalAmount <= 0)
                    {
                        MessageBox.Show("Please select receipt", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //if (ucPayModes1.Balance > 0)
                    //{
                    //    MessageBox.Show("You have to pay full amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    try
                    {
                        MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = "HO";
                        _receiptAuto.Aut_cate_tp = "HO";
                        _receiptAuto.Aut_start_char = "DBSET";
                        _receiptAuto.Aut_direction = 0;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "DBSET";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_year = null;
                        string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                        //ins pay table
                        int cou = 0;
                        foreach (RecieptItem ri in ucPayModes1.RecieptItemList)
                        {
                            cou++;
                            VehicalInsurancePay vehInsPay = new VehicalInsurancePay();
                            vehInsPay.Pay_ref_no = _cusNo;
                            vehInsPay.Ref_line = cou;
                            vehInsPay.Cre_by = BaseCls.GlbUserID;
                            vehInsPay.Value = ri.Sard_settle_amt;
                            vehInsPay.Pay_tp = ri.Sard_pay_tp;
                            if (ri.Sard_pay_tp == "CHEQUE")
                            {
                                vehInsPay.Ref_no = ri.Sard_ref_no;
                                vehInsPay.Bank = ri.Sard_chq_bank_cd;
                                vehInsPay.Bank_branch = ri.Sard_chq_branch;
                            }
                            else if (ri.Sard_pay_tp == "CRCD")
                            {
                                vehInsPay.Ref_no = ri.Sard_ref_no;
                                vehInsPay.Bank = ri.Sard_credit_card_bank;
                                vehInsPay.Bank_branch = ri.Sard_chq_branch;
                            }
                            CHNLSVC.General.SaveInsurancePay(vehInsPay);
                        }
                        for (int i = 0; i < listBoxDebitNos.Items.Count; i++)
                        {
                            List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("SetDebNot", listBoxDebitNos.Items[i].ToString());
                            if (_vehins != null)
                            {

                                _vehins[0].Svit_dbt_set_stus = true;
                                _vehins[0].Svit_dbt_by = BaseCls.GlbUserID;
                                _vehins[0].Svit_dbt_dt = DateTime.Now;
                                _vehins[0].Pay_ref_no = _cusNo;
                                CHNLSVC.General.SaveVehicalInsurance(_vehins[0], null);
                            }
                        }
                        listBoxDebitNos.Items.Clear();
                        txtDebitNo.Text = "";
                        ucPayModes1.ClearControls();
                        txtDebitCompany.Text = "";
                        txtDebitAccountNo.Text = "";
                        txtDebitPC.Text = "";
                        dataGridViewDebitSearch.DataSource = null;
                        txtDebitCompany.Text = BaseCls.GlbUserComCode;
                        txtDebitPC.Text = BaseCls.GlbUserDefProf;
                        DebitTotal = 0;
                        MessageBox.Show("Record updated successfully!", "Information");
                    }
                    catch (Exception er)
                    {

                        MessageBox.Show("Error occurred while processing!!\n" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CHNLSVC.CloseChannel();
                    }
                }
                else
                {

                    if (tabControl3.SelectedIndex == 0)
                    {
                        try
                        {
                            List<VehicalInsuranceClaim> vehClList = CHNLSVC.General.GetClaimDetails("RecieveDocumentSpecific", VehicalNo, AccidentDate);
                            if (vehClList != null)
                            {

                                vehClList[0].Reg_no = VehicalNo;
                                vehClList[0].Polic_rep_sts = chkPoliceReport.Checked;
                                vehClList[0].Dri_name = txtDriverName.Text;
                                vehClList[0].Dl_lic = txtLicenceNo.Text;
                                vehClList[0].Init_by = BaseCls.GlbUserID;
                                vehClList[0].Init_date = _date;
                                CHNLSVC.General.SaveInsuranceClaim(vehClList[0]);
                                ClearClaimCustomerDetails();
                                ClearClaimIntimated();
                                MessageBox.Show("Record updated successfully!", "Information");
                            }
                            else
                            {
                                VehicalInsuranceClaim _vehinsClaim = new VehicalInsuranceClaim();
                                _vehinsClaim.Reg_no = txtRegNo.Text.ToUpper();
                                _vehinsClaim.Acc_date = dateTimePickerAccidentDate.Value;
                                _vehinsClaim.Polic_rep_sts = chkPoliceReport.Checked;
                                _vehinsClaim.Dri_name = txtDriverName.Text;
                                _vehinsClaim.Dl_lic = txtLicenceNo.Text;
                                _vehinsClaim.Init_by = BaseCls.GlbUserID;
                                _vehinsClaim.Init_date = _date;
                                CHNLSVC.General.SaveInsuranceClaim(_vehinsClaim);

                                ClearClaimCustomerDetails();
                                ClearClaimIntimated();
                                MessageBox.Show("Record added successfully!", "Information");
                            }
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("Error occurred while processing!!\n" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            CHNLSVC.CloseChannel();
                        }
                    }
                    else if (tabControl3.SelectedIndex == 1)
                    {
                        List<VehicalInsuranceClaim> vehClList = CHNLSVC.General.GetClaimDetails("RecieveDocumentSpecific", VehicalNo, AccidentDate);
                        if (vehClList != null)
                        {
                            decimal val;
                            if (!decimal.TryParse(txtRepairEstimate.Text, out val))
                            {
                                MessageBox.Show("Repair estimate has to be number", "Warning");
                                return;
                            }

                            try
                            {

                                vehClList[0].Clamin_form_rec = dateTimePickerClaimFormRecieve.Value;
                                vehClList[0].Dl_rec = dateTimePickerLicenceRecieve.Value;
                                vehClList[0].Repi_esti_rec = dateTimePickerRepairEstimateRecieve.Value;
                                vehClList[0].Police_rep_rec = dateTimePickerPoliceReportRecieve.Value;
                                vehClList[0].App_forw = dateTimePickerForwadingApproval.Value;
                                vehClList[0].Final_invo = dateTimePickerFinalInvoice.Value;
                                vehClList[0].Repi_esti_val = Convert.ToDecimal(txtRepairEstimate.Text);
                                vehClList[0].Doc_stus = true;
                                vehClList[0].Rec_by = BaseCls.GlbUserID;
                                vehClList[0].Rec_dt = DateTime.Now;
                                CHNLSVC.General.SaveInsuranceClaim(vehClList[0]);
                                ClearClaimCustomerDetails();
                                ClearRecieveDocument();
                                MessageBox.Show("Record updated successfully!", "Information");
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show("Error occurred while processing!!\n" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CHNLSVC.CloseChannel();

                            }
                        }
                        else
                        {
                            decimal val;
                            if (!decimal.TryParse(txtRepairEstimate.Text, out val))
                            {
                                MessageBox.Show("Repair estimate has to be number", "Warning");
                                return;
                            }

                            try
                            {
                                VehicalInsuranceClaim _vehinsClaim = new VehicalInsuranceClaim();
                                _vehinsClaim.Clamin_form_rec = dateTimePickerClaimFormRecieve.Value;
                                _vehinsClaim.Dl_rec = dateTimePickerLicenceRecieve.Value;
                                _vehinsClaim.Repi_esti_rec = dateTimePickerRepairEstimateRecieve.Value;
                                _vehinsClaim.Police_rep_rec = dateTimePickerPoliceReportRecieve.Value;
                                _vehinsClaim.App_forw = dateTimePickerForwadingApproval.Value;
                                _vehinsClaim.Final_invo = dateTimePickerFinalInvoice.Value;
                                _vehinsClaim.Repi_esti_val = Convert.ToDecimal(txtRepairEstimate.Text);
                                _vehinsClaim.Doc_stus = true;
                                _vehinsClaim.Rec_by = BaseCls.GlbUserID;
                                _vehinsClaim.Rec_dt = DateTime.Now;
                                CHNLSVC.General.SaveInsuranceClaim(_vehinsClaim);
                                ClearClaimCustomerDetails();
                                ClearRecieveDocument();
                                MessageBox.Show("Record updated successfully!", "Information");
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show("Error occurred while processing!!\n" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CHNLSVC.CloseChannel();

                            }


                        }
                    }
                    else
                    {
                        List<VehicalInsuranceClaim> vehClList = CHNLSVC.General.GetClaimDetails("CustomerSettlementSpecific", VehicalNo, AccidentDate);
                        if (vehClList != null)
                        {
                            decimal trnVal;
                            DateTime temDate;
                            if (!decimal.TryParse(txtClaimAmount.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter Claim amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!decimal.TryParse(txtPolicyExcess.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter policy excess amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!decimal.TryParse(txtOtherDeduction.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter other deduction amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!decimal.TryParse(txtBalanceValue.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter balance amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!decimal.TryParse(txtChequeValue.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter checque amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (txtClaimAccountNo.Text == "")
                            {
                                MessageBox.Show("Please enter account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (txtChecqueNo.Text == "")
                            {
                                MessageBox.Show("Please enter cheque no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (txtChequeBank.Text == "")
                            {
                                MessageBox.Show("Please enter cheque bank", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (txtChequeBranch.Text == "")
                            {
                                MessageBox.Show("Please enter cheque branch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            try
                            {
                                vehClList[0].Claim_val = Convert.ToDecimal(txtClaimAmount.Text);
                                vehClList[0].Policy_excess = Convert.ToDecimal(txtPolicyExcess.Text);
                                vehClList[0].Other_dedection = Convert.ToDecimal(txtOtherDeduction.Text);
                                vehClList[0].Bal_val = Convert.ToDecimal(txtBalanceValue.Text);
                                vehClList[0].Acc_no = txtClaimAccountNo.Text;
                                vehClList[0].Cheq_no = txtChecqueNo.Text;
                                vehClList[0].Cheq_bank = txtChequeBank.Text;
                                vehClList[0].Cheq_branch = txtChequeBranch.Text;
                                vehClList[0].Cheq_dt = dateTimePickerChequeDate.Value;
                                vehClList[0].Cheq_val = Convert.ToDecimal(txtChequeValue.Text);
                                vehClList[0].Sett_dt = _date;
                                vehClList[0].Set_by = BaseCls.GlbUserID;
                                vehClList[0].Set_dt = _date;
                                CHNLSVC.General.SaveInsuranceClaim(vehClList[0]);
                                ClearClaimCustomerDetails();
                                ClearCustomerSettlement();
                                MessageBox.Show("Record save successfully!", "Information");
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show("Error occurred while processing!!\n" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CHNLSVC.CloseChannel();
                            }
                        }
                        else
                        {
                            decimal trnVal;
                            DateTime temDate;
                            if (!decimal.TryParse(txtClaimAmount.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter Claim amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!decimal.TryParse(txtPolicyExcess.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter policy excess amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!decimal.TryParse(txtOtherDeduction.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter other deduction amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!decimal.TryParse(txtBalanceValue.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter balance amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!decimal.TryParse(txtChequeValue.Text, out trnVal))
                            {
                                MessageBox.Show("Please enter checque amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (txtClaimAccountNo.Text == "")
                            {
                                MessageBox.Show("Please enter account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (txtChecqueNo.Text == "")
                            {
                                MessageBox.Show("Please enter cheque no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (txtChequeBank.Text == "")
                            {
                                MessageBox.Show("Please enter cheque bank", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (txtChequeBranch.Text == "")
                            {
                                MessageBox.Show("Please enter cheque branch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            try
                            {
                                VehicalInsuranceClaim _vehinsClaim = new VehicalInsuranceClaim();
                                _vehinsClaim.Claim_val = Convert.ToDecimal(txtClaimAmount.Text);
                                _vehinsClaim.Policy_excess = Convert.ToDecimal(txtPolicyExcess.Text);
                                _vehinsClaim.Other_dedection = Convert.ToDecimal(txtOtherDeduction.Text);
                                _vehinsClaim.Bal_val = Convert.ToDecimal(txtBalanceValue.Text);
                                _vehinsClaim.Acc_no = txtClaimAccountNo.Text;
                                _vehinsClaim.Cheq_no = txtChecqueNo.Text;
                                _vehinsClaim.Cheq_bank = txtChequeBank.Text;
                                _vehinsClaim.Cheq_branch = txtChequeBranch.Text;
                                _vehinsClaim.Cheq_dt = dateTimePickerChequeDate.Value;
                                _vehinsClaim.Cheq_val = Convert.ToDecimal(txtChequeValue.Text);
                                _vehinsClaim.Sett_dt = _date;
                                _vehinsClaim.Set_by = BaseCls.GlbUserID;
                                _vehinsClaim.Set_dt = _date;
                                CHNLSVC.General.SaveInsuranceClaim(_vehinsClaim);
                                ClearClaimCustomerDetails();
                                ClearCustomerSettlement();
                                MessageBox.Show("Record save successfully!", "Information");
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show("Error occurred while processing!!\n" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CHNLSVC.CloseChannel();
                            }
                        }
                    }
                }
                DebitTotal = 0;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    if (tabControl2.SelectedIndex == 0)
                    {
                        ClearSearchDetails();
                        ClearIssueCoverNote();
                        ClearCustomerAndVehicalDetails();
                    }
                    else if (tabControl2.SelectedIndex == 1)
                    {
                        ClearSearchDetails();
                        ClearExtend();
                        ClearCustomerAndVehicalDetails();
                    }
                    else
                    {

                        ClearSearchDetails();
                        ClearIssueCertificate();
                        ClearCustomerAndVehicalDetails();
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    listBoxDebitNos.Items.Clear();
                    txtDebitNo.Text = "";
                    ucPayModes1.ClearControls();
                    txtDebitCompany.Text = "";
                    txtDebitAccountNo.Text = "";
                    txtDebitPC.Text = "";
                    DebitTotal = 0;
                    dataGridViewDebitSearch.DataSource = null;
                    txtDebitCompany.Text = BaseCls.GlbUserComCode;
                    txtDebitPC.Text = BaseCls.GlbUserDefProf;
                }
                else
                {
                    if (tabControl3.SelectedIndex == 0)
                    {
                        ClearClaimCustomerDetails();
                        ClearClaimIntimated();
                    }
                    else if (tabControl3.SelectedIndex == 1)
                    {
                        ClearClaimCustomerDetails();
                        ClearRecieveDocument();
                    }
                    else
                    {
                        ClearClaimCustomerDetails();
                        ClearCustomerSettlement();
                    }
                }
                DebitTotal = 0;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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

        #endregion

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewSearch.AutoGenerateColumns = false;

                string recent = "";
                DateTime from = DateTime.MinValue;
                DateTime to = DateTime.MaxValue;
                if (chkMostRecent.Checked)
                {
                    recent = "Recent";
                }
                else
                {
                    recent = "NRecent";
                }


                from = dateTimePickerFrom.Value;
                to = dateTimePickerTo.Value;
                dataGridViewSearch.DataSource = CHNLSVC.General.GetInsuranceInvoice(txtCompany.Text, txtPC.Text, from, to, recent + type, txtVehNo.Text, txtAccount.Text.ToUpper(), txtInv.Text, txtSearchEngine.Text, txtSearchChassis.Text);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void CheckRecieptAvailability()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            if (tabControl2.SelectedIndex == 0)
            {
                List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssInsCovNot", txtRecieptNo.Text);

                //load details
                if (_vehins != null)
                {
                    if (_vehins[0].Svit_cvnt_issue == 2)
                    {
                        MessageBox.Show("This receipt is canceled. Can not issue cover note", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (_vehins[0].Svit_rec_tp != "VHINS")
                    {
                        MessageBox.Show("This receipt is not allow to issue cover note", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtRecieptNo.Text = "";
                        txtRecieptNo.Focus();
                        return;
                    }

                    invtype = _vehins[0].Svit_sales_tp;
                    txtInsCompany.Text = _vehins[0].Svit_ins_com;
                    txtInsPolicy.Text = _vehins[0].Svit_ins_polc;

                    txtCusCode.Text = _vehins[0].Svit_cust_cd;
                    cmbCustomerTitle.SelectedItem = _vehins[0].Svit_cust_title;
                    txtLastName.Text = _vehins[0].Svit_last_name;
                    txtFullName.Text = _vehins[0].Svit_full_name;
                    txtInitials.Text = _vehins[0].Svit_initial;
                    txtAdd1.Text = _vehins[0].Svit_add01;
                    txtAdd2.Text = _vehins[0].Svit_add02;
                    txtCity.Text = _vehins[0].Svit_city;
                    cmbDistrict.SelectedValue = _vehins[0].Svit_district;
                    txtProvince.Text = _vehins[0].Svit_province;
                    txtContact.Text = _vehins[0].Svit_contact;
                    //TextBoxMake.Text=_vehins.m
                    txtModal.Text = _vehins[0].Svit_model;
                    txtCapacity.Text = _vehins[0].Svit_capacity;
                    txtEngine.Text = _vehins[0].Svit_engine;
                    txtChassis.Text = _vehins[0].Svit_chassis;
                    txtPremValue.Text = _vehins[0].Svit_ins_val.ToString();
                    List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);
                    if (list != null)
                    {
                        List<InvoiceItem> _select = (from _lst in list
                                                     where _lst.Sad_unit_rt > 0
                                                     select _lst).ToList();

                        list = new List<InvoiceItem>();
                        list = _select;
                        txtSalesPrice.Text = list[0].Sad_unit_amt.ToString();
                    }
                    else
                    {
                        List<QoutationDetails> _recallList = new List<QoutationDetails>();  //kapila 25/1/2016
                        _recallList = CHNLSVC.Sales.Get_all_linesForQoutation(_vehins[0].Svit_inv_no);
                        if (_recallList != null)
                            txtSalesPrice.Text = (_recallList[0].Qd_tot_amt / _recallList[0].Qd_to_qty).ToString();

                    }
                    DataTable dt = CHNLSVC.General.GetHpSch(_vehins[0].Svit_inv_no);
                    if (dt.Rows.Count > 0)
                    {

                        txtSchemeCode.Text = dt.Rows[0]["HPA_SCH_CD"].ToString();
                        txtAccount.Text = dt.Rows[0]["HPA_ACC_NO"].ToString();
                    }
                    else
                    {
                        txtSchemeCode.Text = "";
                        txtAccount.Text = "";
                    }

                    dateTimePickerCoverNoteFrom.Value = _date;
                    string _cusNo = "";
                    lock (this)
                    {
                        MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                        //_receiptAuto.Aut_cate_cd ="COVNT";
                        _receiptAuto.Aut_cate_cd = txtInsCompany.Text; //kapila 11/3/2014
                        _receiptAuto.Aut_cate_tp = "COVNT";
                        //_receiptAuto.Aut_start_char = "ABN";
                        if (txtInsCompany.Text == "CN001")   //kapila 11/3/2014
                        {
                            _receiptAuto.Aut_start_char = "ABN";
                        }
                        if (txtInsCompany.Text == "UAL01")
                        {
                            _receiptAuto.Aut_start_char = "CR/AA";
                        }
                        if (txtInsCompany.Text == "MBSL")
                        {
                            _receiptAuto.Aut_start_char = "MTABN";
                        }
                        if (txtInsCompany.Text == "JS001")
                        {
                            _receiptAuto.Aut_start_char = "ABN2014";
                        }
                        if (txtInsCompany.Text == "AIA")
                        {
                            _receiptAuto.Aut_start_char = "A";
                        }
                        _receiptAuto.Aut_direction = 0;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "COVNT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_year = 2012;
                        _cusNo = CHNLSVC.General.GetCoverNoteNo(_receiptAuto, "Cover");
                    }
                    txtCoverNoteNo.Text = _cusNo;

                }
                else
                {
                    MessageBox.Show("This receipt can not issue cover note. Already cover note issued.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearCustomerAndVehicalDetails();
                    ClearIssueCoverNote();
                    return;
                }
            }
            else if (tabControl2.SelectedIndex == 1)
            {
                List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("ExtCovNot", txtRecieptNo.Text);
                if (_vehins != null)
                {
                    if (_vehins[0].Svit_cvnt_issue == 2)
                    {
                        MessageBox.Show("This receipt is canceled. Can not issue cover note", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    txtInsCompany.Text = _vehins[0].Svit_ins_com;
                    txtInsPolicy.Text = _vehins[0].Svit_ins_polc;

                    txtCusCode.Text = _vehins[0].Svit_cust_cd;
                    cmbCustomerTitle.SelectedItem = _vehins[0].Svit_cust_title;
                    txtLastName.Text = _vehins[0].Svit_last_name;
                    txtFullName.Text = _vehins[0].Svit_full_name;
                    txtInitials.Text = _vehins[0].Svit_initial;
                    txtAdd1.Text = _vehins[0].Svit_add01;
                    txtAdd2.Text = _vehins[0].Svit_add02;
                    txtCity.Text = _vehins[0].Svit_city;
                    cmbDistrict.SelectedValue = _vehins[0].Svit_district;
                    txtProvince.Text = _vehins[0].Svit_province;
                    txtContact.Text = _vehins[0].Svit_contact;
                    //TextBoxMake.Text=_vehins.m
                    txtModal.Text = _vehins[0].Svit_model;
                    txtCapacity.Text = _vehins[0].Svit_capacity;
                    txtEngine.Text = _vehins[0].Svit_engine;
                    txtChassis.Text = _vehins[0].Svit_chassis;
                    txtPremValue.Text = _vehins[0].Svit_ins_val.ToString();
                    List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);
                    DataTable dt = CHNLSVC.General.GetHpSch(_vehins[0].Svit_inv_no);
                    if (dt.Rows.Count > 0)
                    {

                        txtSchemeCode.Text = dt.Rows[0]["HPA_SCH_CD"].ToString();
                        txtAccount.Text = dt.Rows[0]["HPA_ACC_NO"].ToString();
                    }
                    else
                    {
                        txtSchemeCode.Text = "";
                        txtAccount.Text = "";
                    }
                    txtSalesPrice.Text = list[0].Sad_unit_amt.ToString();

                    string _cusNo = "";
                    lock (this)
                    {
                        MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = "COVNT";
                        _receiptAuto.Aut_cate_tp = "COVNT";
                        _receiptAuto.Aut_start_char = "ABN";
                        _receiptAuto.Aut_direction = 0;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "COVNT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_year = 2012;
                        _cusNo = CHNLSVC.General.GetCoverNoteNo(_receiptAuto, "Cover");
                    }
                    txtExtCoverNote.Text = _cusNo;


                    if (_vehins[0].Svit_ext_to_dt.Year == 2999)
                        dateTimePickerExtendFrom.Value = _vehins[0].Svit_cvnt_to_dt;
                    else
                    {
                        dateTimePickerExtendFrom.Value = _vehins[0].Svit_ext_to_dt;
                        dateTimePickerExtendTo.Value = dateTimePickerExtendFrom.Value.AddDays(1);
                    }
                }
                else
                {
                    MessageBox.Show("This receipt can not extend cover note.The receipt do not have cover note yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearCustomerAndVehicalDetails();
                    ClearExtend();
                    return;
                }
            }
            else
            {
                List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssCer", txtRecieptNo.Text);
                //load details
                if (_vehins != null)
                {
                    if (_vehins[0].Svit_cvnt_issue == 2)
                    {
                        MessageBox.Show("This receipt is canceled. Can not issue cover note", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    txtInsCompany.Text = _vehins[0].Svit_ins_com;
                    txtInsPolicy.Text = _vehins[0].Svit_ins_polc;

                    txtCusCode.Text = _vehins[0].Svit_cust_cd;
                    cmbCustomerTitle.SelectedItem = _vehins[0].Svit_cust_title;
                    txtLastName.Text = _vehins[0].Svit_last_name;
                    txtFullName.Text = _vehins[0].Svit_full_name;
                    txtInitials.Text = _vehins[0].Svit_initial;
                    txtAdd1.Text = _vehins[0].Svit_add01;
                    txtAdd2.Text = _vehins[0].Svit_add02;
                    txtCity.Text = _vehins[0].Svit_city;
                    cmbDistrict.SelectedValue = _vehins[0].Svit_district;
                    txtProvince.Text = _vehins[0].Svit_province;
                    txtContact.Text = _vehins[0].Svit_contact;
                    //TextBoxMake.Text=_vehins.m
                    txtModal.Text = _vehins[0].Svit_model;
                    txtCapacity.Text = _vehins[0].Svit_capacity;
                    txtEngine.Text = _vehins[0].Svit_engine;
                    txtChassis.Text = _vehins[0].Svit_chassis;
                    txtPremValue.Text = _vehins[0].Svit_ins_val.ToString();
                    List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);
                    DataTable dt = CHNLSVC.General.GetHpSch(_vehins[0].Svit_inv_no);
                    if (dt.Rows.Count > 0)
                    {

                        txtSchemeCode.Text = dt.Rows[0]["HPA_SCH_CD"].ToString();
                        txtAccount.Text = dt.Rows[0]["HPA_ACC_NO"].ToString();
                    }
                    else
                    {
                        txtSchemeCode.Text = "";
                        txtAccount.Text = "";
                    }
                    txtTotal.Text = _vehins[0].Svit_ins_val.ToString();
                    txtSalesPrice.Text = list[0].Sad_unit_amt.ToString();
                    lblVehNo.Text = _vehins[0].Svit_veh_reg_no;
                    //TextBoxVehicalNo.Text = _vehins[0].Svit_veh_reg_no;
                    if (_vehins[0].Svit_reg_dt.Year == 2999)
                    {
                        //TextBoxRegDate.Text = "";
                        lblRegDate.Text = "";
                    }
                    else
                    {
                        //TextBoxRegDate.Text = _vehins[0].Svit_reg_dt.ToShortDateString();
                        lblRegDate.Text = _vehins[0].Svit_reg_dt.ToShortDateString();
                    }

                    txtPolicyNo.Text = _vehins[0].Svit_polc_no;
                    dateTimePickerExpireNo.Value = _vehins[0].Svit_expi_dt;
                    txtDebitNoteNo.Text = _vehins[0].Svit_dbt_no;
                    txtNetPremium.Text = _vehins[0].Svit_net_prem.ToString();
                    txtSRCCPre.Text = _vehins[0].Svit_srcc_prem.ToString();
                    txtFileNo.Text = _vehins[0].Svit_file_no;
                    CalcTotalPremium();
                }
                else
                {
                    MessageBox.Show("This receipt can not issue certificate.The receipt do not have cover note yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearCustomerAndVehicalDetails();
                    ClearIssueCertificate();
                    return;
                }
            }
        }

        private void CalcTotalPremium()
        {
            try
            {
                decimal net = Convert.ToDecimal(txtNetPremium.Text);
                decimal srcc = Convert.ToDecimal(txtSRCCPre.Text);
                decimal total = Convert.ToDecimal(txtTotal.Text);

                txtOther.Text = (total - (net + srcc)).ToString();
            }
            catch (Exception) { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetVehicalInsuarance(null, txtDebitNo.Text);
                if (_vehins != null)
                {
                    bool b = listBoxDebitNos.Items.Contains(txtDebitNo.Text);
                    if (!b)
                    {
                        txtDebitNo.Text = "";
                        LoadDebitNote(_vehins[0].Svit_ref_no);
                    }
                }
                else
                {
                    MessageBox.Show("This debit note is already settled or invalid. Please recheck and try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxDebitNos.SelectedItem != null)
                {
                    List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("SetDebNot", listBoxDebitNos.SelectedItem.ToString());
                    //load details
                    if (_vehins != null)
                    {
                        DebitTotal = DebitTotal - _vehins[0].Svit_tot_val;
                        listBoxDebitNos.Items.RemoveAt(listBoxDebitNos.SelectedIndex);
                        ucPayModes1.InvoiceType = "CS";
                        ucPayModes1.Allow_Plus_balance = true;
                        ucPayModes1.TotalAmount = DebitTotal;
                        ucPayModes1.LoadData();
                        ucPayModes1.IsZeroAllow = true;
                    }
                }
                else
                {
                    MessageBox.Show("Please select debit no to remove", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnDebitSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewDebitSearch.AutoGenerateColumns = false;

                string recent = "";
                DateTime from = DateTime.MinValue;
                DateTime to = DateTime.MaxValue;
                if (chkDebitRecent.Checked)
                {
                    recent = "RecentDE";
                }
                else
                {
                    recent = "NRecentDE";
                }


                from = dateTimePickerDebitFrom.Value;
                to = dateTimePickerDebitTo.Value;

                dataGridViewDebitSearch.DataSource = CHNLSVC.General.GetInsuranceInvoice(txtDebitCompany.Text, txtDebitPC.Text, from, to, recent, txtDebitVehNo.Text, txtDebitAccountNo.Text.ToUpper(), txtDebitInvoice.Text, txtDebitEngine.Text, txtDebitChassis.Text);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtExtendDays_Leave(object sender, EventArgs e)
        {
            if (txtExtendDays.Text != "")
            {
                int val;
                if (!int.TryParse(txtExtendDays.Text, out val))
                {
                    MessageBox.Show("Extended days has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    dateTimePickerExtendTo.Value = dateTimePickerExtendFrom.Value.AddDays(Convert.ToInt32(txtExtendDays.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x0100 && (int)m.WParam == 13)
            {
                this.ProcessTabKey(true);
            }
            return base.ProcessKeyPreview(ref m);
        }

        #region combo box selection change

        private void cmbDistrict_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbDistrict.SelectedValue == null) return;
                if (string.IsNullOrEmpty(cmbDistrict.SelectedValue.ToString())) return;
                DistrictProvince _type = CHNLSVC.Sales.GetDistrict(cmbDistrict.SelectedValue.ToString())[0];
                if (_type.Mds_district == null) { MessageBox.Show("Province information not available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                txtProvince.Text = _type.Mds_province;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbRegNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (tabControl3.SelectedIndex == 0)
                {
                    ClearClaimCustomerDetails();
                    ClearClaimIntimated();
                    ClearRecieveDocument();
                    ClearCustomerSettlement();

                }
                if (tabControl3.SelectedIndex == 1)
                {
                    ClearClaimCustomerDetails();
                    ClearClaimIntimated();
                    ClearRecieveDocument();
                    ClearCustomerSettlement();

                    if (cmbRegNo.SelectedItem != null)
                        LoadClaim(cmbRegNo.SelectedItem.ToString(), "RecieveDocument");
                }
                else
                {
                    ClearClaimCustomerDetails();
                    ClearClaimIntimated();
                    ClearRecieveDocument();
                    ClearCustomerSettlement();

                    if (cmbRegNo.SelectedItem != null)
                        LoadClaim(cmbRegNo.SelectedItem.ToString(), "CustomerSettlementGrid");
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region grid view cell content click

        private void dataGridViewSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    txtRecieptNo.Text = dataGridViewSearch.Rows[e.RowIndex].Cells[1].Value.ToString();
                    CheckRecieptAvailability();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dataGridViewDebitSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                txtDebitNo.Text = dataGridViewDebitSearch.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void dataGridViewPreviousClaims_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    VehicalNo = dataGridViewPreviousClaims.Rows[e.RowIndex].Cells[2].Value.ToString();
                    AccidentDate = Convert.ToDateTime(dataGridViewPreviousClaims.Rows[e.RowIndex].Cells[1].Value);
                    LoadPreviousClaimDetails();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region clear methods

        private void ClearCustomerAndVehicalDetails()
        {
            txtCusCode.Text = "";
            cmbCustomerTitle.SelectedIndex = 0;
            txtLastName.Text = "";
            txtFullName.Text = "";
            txtInitials.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtCity.Text = "";
            cmbDistrict.SelectedIndex = 0;
            txtProvince.Text = "";
            txtContact.Text = "";
            //TextBoxMake.Text=_vehins.m
            txtModal.Text = "";
            txtCapacity.Text = "";
            txtEngine.Text = "";
            txtChassis.Text = "";
            txtPremValue.Text = "";
            txtSchemeCode.Text = "";
            txtAccount.Text = "";
            txtSalesPrice.Text = "";
            txtInsCompany.Text = "";
            txtInsPolicy.Text = "";
            cmbDistrict_SelectionChangeCommitted(null, null);
        }

        private void ClearSearchDetails()
        {


            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            txtRecieptNo.Text = "";
            txtCompany.Text = "";
            txtPC.Text = "";
            txtAccountNo.Text = "";
            dateTimePickerFrom.Value = _date.AddMonths(-1);
            dateTimePickerTo.Value = _date;

            dataGridViewSearch.DataSource = null;
            txtCompany.Text = BaseCls.GlbUserComCode;
            txtPC.Text = BaseCls.GlbUserDefProf;
        }

        private void ClearIssueCoverNote()
        {
            invtype = "";
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            txtCoverNoteNo.Text = "";
            txtPremValue.Text = "";
            dateTimePickerCoverNoteFrom.Value = _date;
            dateTimePickerCoverNoteTo.Value = _date;
            txtNoOfDays.Text = "30";
            txtNoOfDays_Leave(null, null);
        }

        private void ClearExtend()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            txtExtendDays.Text = "";
            dateTimePickerExtendFrom.Value = _date;
            dateTimePickerExtendTo.Value = _date;
        }

        private void ClearIssueCertificate()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            txtPolicyNo.Text = "";
            txtDebitNoteNo.Text = "";
            txtNetPremium.Text = "";
            txtSRCCPre.Text = "";
            txtOther.Text = "";
            txtTotal.Text = "";
            dateTimePickerEffectiveDate.Value = _date;
            dateTimePickerExpireNo.Value = _date.AddYears(1);
            txtFileNo.Text = "";
        }

        private void ClearClaimCustomerDetails()
        {
            lblContact.Text = "";
            lblAdd1.Text = "";
            lblAdd2.Text = "";
            lblCity.Text = "";
            lblDistrict.Text = "";
            lblFullName.Text = "";
            lblInitials.Text = "";
            lblLastName.Text = "";
            lblProvince.Text = "";
            lblTitle.Text = "";
            txtRegNo.Text = "";
            dataGridViewPreviousClaims.DataSource = null;
        }

        private void ClearClaimIntimated()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            txtDriverName.Text = "";
            txtLicenceNo.Text = "";
            dateTimePickerAccidentDate.Value = _date;
            chkPoliceReport.Checked = false;
        }

        private void ClearRecieveDocument()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            dateTimePickerClaimFormRecieve.Value = _date;
            dateTimePickerLicenceRecieve.Value = _date;
            dateTimePickerRepairEstimateRecieve.Value = _date;
            dateTimePickerPoliceReportRecieve.Value = _date;
            dateTimePickerForwadingApproval.Value = _date;
            dateTimePickerFinalInvoice.Value = _date;
            txtRepairEstimate.Text = "";
        }

        private void ClearCustomerSettlement()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            txtClaimAmount.Text = "";
            txtPolicyExcess.Text = "";
            txtOtherDeduction.Text = "";
            txtClaimAccountNo.Text = "";
            txtChequeValue.Text = "";
            txtChequeBank.Text = "";
            txtChequeBranch.Text = "";
            txtBalanceValue.Text = "";
            txtChecqueNo.Text = "";
            dateTimePickerChequeDate.Value = _date;

        }


        #endregion

        #region data load events

        private void LoadRecievDocCombo()
        {
            cmbRegNo.Items.Clear();
            List<VehicalInsuranceClaim> claLi = CHNLSVC.General.GetClaimDetails("RecieveDocumentDestinct", null, DateTime.MinValue);
            if (claLi != null)
            {
                foreach (VehicalInsuranceClaim clain in claLi)
                {
                    cmbRegNo.Items.Add(clain.Reg_no);
                }
            }
        }

        private void LoadCustomerSettlementCombo()
        {
            cmbRegNo.Items.Clear();
            List<VehicalInsuranceClaim> claLi = CHNLSVC.General.GetClaimDetails("CustomerSettlement", null, DateTime.MinValue);
            if (claLi != null)
            {
                foreach (VehicalInsuranceClaim clain in claLi)
                {
                    cmbRegNo.Items.Add(clain.Reg_no);
                }
            }
        }

        private void LoadPreviousClaimDetails()
        {
            if (tabControl3.SelectedIndex == 0)
            {
                List<VehicalInsuranceClaim> claimList = CHNLSVC.General.GetClaimDetails("RecieveDocument", VehicalNo, AccidentDate);
                if (claimList != null)
                {
                    txtDriverName.Text = claimList[0].Dri_name;
                    txtLicenceNo.Text = claimList[0].Dl_lic;
                    dateTimePickerAccidentDate.Value = claimList[0].Acc_date;
                }
            }
            else if (tabControl3.SelectedIndex == 1)
            {
                List<VehicalInsuranceClaim> claimList = CHNLSVC.General.GetClaimDetails("RecieveDocumentSpecific", VehicalNo, AccidentDate);
                if (claimList != null)
                {


                }
            }
            else
            {
                List<VehicalInsuranceClaim> claimList = CHNLSVC.General.GetClaimDetails("CustomerSettlementSpecific", VehicalNo, AccidentDate);
                if (claimList != null)
                {

                }
            }
        }

        private void BindDistrict(ComboBox cmbDistrict)
        {
            List<DistrictProvince> _district = CHNLSVC.Sales.GetDistrict("");
            var source = new BindingSource();
            source.DataSource = _district.OrderBy(items => items.Mds_district);
            cmbDistrict.DataSource = source;
            cmbDistrict.DisplayMember = "Mds_district";
            cmbDistrict.ValueMember = "Mds_district";
        }

        private void LoadDebitNote(string st)
        {
            List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("SetDebNot", st);
            //load details
            if (_vehins != null)
            {

                listBoxDebitNos.Items.Add(st);
                DebitTotal = DebitTotal + _vehins[0].Svit_tot_val;
                ucPayModes1.InvoiceType = "CS";
                ucPayModes1.Allow_Plus_balance = true;
                ucPayModes1.TotalAmount = DebitTotal;
                ucPayModes1.LoadData();
                ucPayModes1.IsZeroAllow = true;
            }
            else
            {
                MessageBox.Show("Invalid reciept", "Warning");
            }
        }

        private void LoadClaim(string st, string type)
        {
            dataGridViewPreviousClaims.AutoGenerateColumns = false;
            List<FF.BusinessObjects.VehicleInsuarance> _vehinsList = CHNLSVC.General.GetClaimCusDetails(st.ToUpper());
            if (_vehinsList != null)
            {
                lblTitle.Text = _vehinsList[0].Svit_cust_title;
                lblFullName.Text = _vehinsList[0].Svit_full_name;
                lblLastName.Text = _vehinsList[0].Svit_last_name;
                lblInitials.Text = _vehinsList[0].Svit_initial;
                lblAdd1.Text = _vehinsList[0].Svit_add01;
                lblAdd2.Text = _vehinsList[0].Svit_add02;
                lblDistrict.Text = _vehinsList[0].Svit_district;
                lblCity.Text = _vehinsList[0].Svit_city;
                lblProvince.Text = _vehinsList[0].Svit_province;
                lblContact.Text = _vehinsList[0].Svit_contact;

                dataGridViewPreviousClaims.DataSource = CHNLSVC.General.GetClaimDetails(type, st, DateTime.MinValue);
            }
            else
            {
                dataGridViewPreviousClaims.DataSource = null;

            }
        }

        #endregion

        #region common search

        private void btnCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCompany;
                _CommonSearch.txtSearchbyword.Text = txtCompany.Text;
                _CommonSearch.ShowDialog();
                txtCompany.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                txtPC.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccountNo;
                _CommonSearch.txtSearchbyword.Text = txtAccountNo.Text;
                _CommonSearch.ShowDialog();
                txtAccountNo.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnDebitCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDebitCompany;
                _CommonSearch.txtSearchbyword.Text = txtDebitCompany.Text;
                _CommonSearch.ShowDialog();
                txtDebitCompany.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnDebitPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDebitPC;
                _CommonSearch.txtSearchbyword.Text = txtDebitPC.Text;
                _CommonSearch.ShowDialog();
                txtDebitPC.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnDebitAccount_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDebitAccountNo;
                _CommonSearch.txtSearchbyword.Text = txtDebitAccountNo.Text;
                _CommonSearch.ShowDialog();
                txtDebitAccountNo.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnReciept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef);
                DataTable _result = CHNLSVC.CommonSearch.GetVehicalInsuranceRef(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRecieptNo;
                _CommonSearch.txtSearchbyword.Text = txtRecieptNo.Text;
                _CommonSearch.ShowDialog();
                txtRecieptNo.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnDebit_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehicalInsuranceDebit);
                DataTable _result = CHNLSVC.CommonSearch.GetVehicalInsuranceDebitNo(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDebitNo;
                _CommonSearch.txtSearchbyword.Text = txtDebitNo.Text;
                _CommonSearch.ShowDialog();
                txtDebitNo.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehicalInsuranceRegNo);
                DataTable _result = CHNLSVC.CommonSearch.GetvehicalInsuranceRegistrationNUmber(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRegNo;
                _CommonSearch.ShowDialog();
                txtRegNo.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCustomerCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        if (tabControl1.SelectedIndex == 0)
                        {
                            paramsText.Append(BaseCls.GlbUserID + seperator + txtCompany.Text + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 1)
                        {
                            paramsText.Append(BaseCls.GlbUserID + seperator + txtDebitCompany.Text + seperator);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        if (tabControl1.SelectedIndex == 0)
                        {
                            paramsText.Append(txtCompany.Text + seperator + txtPC.Text + seperator + "A" + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 1)
                        {
                            paramsText.Append(txtDebitCompany.Text + seperator + txtDebitPC.Text + seperator + "A" + seperator);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        if (tabControl1.SelectedIndex == 0)
                        {
                            paramsText.Append(txtCompany.Text + seperator);
                            break;
                        }
                        else if (tabControl1.SelectedIndex == 1)
                        {
                            paramsText.Append(txtDebitCompany.Text + seperator);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehicalInsuranceDebit:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehicalInsuranceRegNo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
            }
            return paramsText.ToString();
        }

        #endregion

        #region tab selected index change

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnPrint.Visible = false;
            if (tabControl1.SelectedIndex == 0)
            {
                txtRecieptNo.Focus();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                txtDebitCompany.Focus();
            }
            else
            {
                txtRegNo.Focus();
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl2.SelectedIndex == 0)
                {
                    type = "Cover";
                    //pnlCustomer.Enabled = true;
                    //pnlVehicalRegistration.Enabled = true;
                    txtLastName.ReadOnly = false;
                    cmbCustomerTitle.Enabled = true;
                    txtFullName.ReadOnly = false;
                    txtInitials.ReadOnly = false;
                    txtCusCode.ReadOnly = false;
                    txtAdd1.ReadOnly = false;
                    txtAdd2.ReadOnly = false;
                    txtContact.ReadOnly = false;
                    btnCustomerCode.Enabled = true;
                    txtCity.ReadOnly = false;
                    cmbDistrict.Enabled = true;
                    txtProvince.ReadOnly = false;
                    txtModal.ReadOnly = false;
                    txtEngine.ReadOnly = false;
                    txtAccount.ReadOnly = false;
                    txtCapacity.ReadOnly = false;
                    txtChassis.ReadOnly = false;
                    txtSchemeCode.ReadOnly = false;
                    txtSalesPrice.ReadOnly = false;


                    lblVeh.Visible = false;
                    lblVehNo.Visible = false;
                    lblReg.Visible = false;
                    lblRegDate.Visible = false;

                    lblVehNo.Text = "";
                    lblRegDate.Text = "";

                    ClearCustomerAndVehicalDetails();
                    ClearSearchDetails();
                    ClearExtend();
                    ClearIssueCertificate();
                    ClearIssueCoverNote();
                    //btnPrint.Visible = false;
                }
                else if (tabControl2.SelectedIndex == 1)
                {
                    type = "Extend";
                    //pnlCustomer.Enabled = false;
                    //pnlVehicalRegistration.Enabled = false;

                    txtLastName.ReadOnly = true;
                    cmbCustomerTitle.Enabled = false;
                    txtFullName.ReadOnly = true;
                    txtInitials.ReadOnly = true;
                    txtCusCode.ReadOnly = true;
                    txtAdd1.ReadOnly = true;
                    txtAdd2.ReadOnly = true;
                    txtContact.ReadOnly = true;
                    btnCustomerCode.Enabled = false;
                    txtCity.ReadOnly = true;
                    cmbDistrict.Enabled = false;
                    txtProvince.ReadOnly = true;
                    txtModal.ReadOnly = true;
                    txtEngine.ReadOnly = true;
                    txtAccount.ReadOnly = true;
                    txtCapacity.ReadOnly = true;
                    txtChassis.ReadOnly = true;
                    txtSchemeCode.ReadOnly = true;
                    txtSalesPrice.ReadOnly = true;

                    lblVeh.Visible = false;
                    lblVehNo.Visible = false;
                    lblReg.Visible = false;
                    lblRegDate.Visible = false;
                    lblVehNo.Text = "";
                    lblRegDate.Text = "";
                    ClearCustomerAndVehicalDetails();
                    ClearSearchDetails();
                    ClearExtend();
                    ClearIssueCertificate();
                    ClearIssueCoverNote();
                    //btnPrint.Visible = false;
                }
                else
                {
                    type = "Cer";
                    //pnlCustomer.Enabled = false;
                    //pnlVehicalRegistration.Enabled = false;

                    txtLastName.ReadOnly = true;
                    cmbCustomerTitle.Enabled = false;
                    txtFullName.ReadOnly = true;
                    txtInitials.ReadOnly = true;
                    txtCusCode.ReadOnly = true;
                    txtAdd1.ReadOnly = true;
                    txtAdd2.ReadOnly = true;
                    txtContact.ReadOnly = true;
                    btnCustomerCode.Enabled = false;
                    txtCity.ReadOnly = true;
                    cmbDistrict.Enabled = false;
                    txtProvince.ReadOnly = true;
                    txtModal.ReadOnly = true;
                    txtEngine.ReadOnly = true;
                    txtAccount.ReadOnly = true;
                    txtCapacity.ReadOnly = true;
                    txtChassis.ReadOnly = true;
                    txtSchemeCode.ReadOnly = true;
                    txtSalesPrice.ReadOnly = true;

                    lblVeh.Visible = true;
                    lblVehNo.Visible = true;
                    lblReg.Visible = true;
                    lblRegDate.Visible = true;
                    lblVehNo.Text = "";
                    lblRegDate.Text = "";
                    ClearCustomerAndVehicalDetails();
                    ClearSearchDetails();
                    ClearExtend();
                    ClearIssueCertificate();
                    ClearIssueCoverNote();
                    //btnPrint.Visible = true;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }


        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl3.SelectedIndex == 0)
                {
                    pnlText.Visible = true;
                    pnlCombo.Visible = false;
                    ClearClaimCustomerDetails();
                    ClearClaimIntimated();
                    ClearCustomerSettlement();
                    ClearRecieveDocument();
                    dataGridViewPreviousClaims.Visible = true;
                }
                else if (tabControl3.SelectedIndex == 1)
                {
                    pnlText.Visible = false;
                    pnlCombo.Visible = true;
                    ClearClaimCustomerDetails();
                    ClearClaimIntimated();
                    ClearCustomerSettlement();
                    ClearRecieveDocument();
                    LoadRecievDocCombo();
                    cmbRegNo_SelectionChangeCommitted(null, null);
                    dataGridViewPreviousClaims.Visible = false;
                }
                else
                {
                    pnlText.Visible = false;
                    pnlCombo.Visible = true;
                    ClearClaimCustomerDetails();
                    ClearClaimIntimated();
                    ClearCustomerSettlement();
                    ClearRecieveDocument();
                    LoadCustomerSettlementCombo();
                    cmbRegNo_SelectionChangeCommitted(null, null);
                    dataGridViewPreviousClaims.Visible = false;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region text box leave

        private void txtRecieptNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtRecieptNo.Text != "")
                {
                    CheckRecieptAvailability();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRegNo_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadClaim(txtRegNo.Text, "REGISTRATION");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtNetPremium_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalcTotalPremium();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSRCCPre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalcTotalPremium();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region textbox key down

        private void txtCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnCompany_Click(null, null);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnPC_Click(null, null);
            }
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnAccount_Click(null, null);
            }
        }

        private void txtDebitCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnDebitCompany_Click(null, null);
            }
        }

        private void txtDebitPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnDebitPC_Click(null, null);
            }
        }

        private void txtDebitAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnDebitAccount_Click(null, null);
            }
        }

        private void txtDebitNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnDebit_Click(null, null);
        }

        private void txtRecieptNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnReciept_Click(null, null);
        }

        #endregion

        #region text mouse double click

        private void txtRecieptNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnReciept_Click(null, null);
        }

        private void txtCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCompany_Click(null, null);
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPC_Click(null, null);
        }

        private void txtAccountNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnAccount_Click(null, null);
        }

        private void txtDebitCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnDebitCompany_Click(null, null);
        }

        private void txtDebitPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnDebitPC_Click(null, null);
        }

        private void txtDebitAccountNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnDebitAccount_Click(null, null);
        }

        private void txtDebitNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnDebit_Click(null, null);
        }

        #endregion


        private void txtNoOfDays_Leave(object sender, EventArgs e)
        {
            if (txtNoOfDays.Text != "")
            {
                int val;
                if (!int.TryParse(txtNoOfDays.Text, out val))
                {
                    MessageBox.Show("Extended days has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
                try
                {
                    dateTimePickerCoverNoteTo.Value = dateTimePickerCoverNoteFrom.Value.AddDays(Convert.ToInt32(txtNoOfDays.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected error occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void txtRecieptNo_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
