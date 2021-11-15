using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.Finance;
using System.Globalization;
using FF.WindowsERPClient.Reports.Sales;
using FF.BusinessObjects.Financial;

//Written By kapila on 26/12/2012
namespace FF.WindowsERPClient.Finance
{
    public partial class DayEndProcess : Base
    {
        public DayEndProcess()
        {
            try
            {
                InitializeComponent();
                InitializeEnv();
                loadInit();
                BindData();

                //BindGridView();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Day End Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void BackDatePermission()
        {
            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty);
            //txtDate.Enabled = true;

            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            txtDate.Enabled = true;

        }

        private void BindGridView()
        {
            DataTable _remdet = CHNLSVC.Financial.GetRemitanceSumDet(Convert.ToDateTime(txtDate.Value).Date, BaseCls.GlbUserDefProf);
            gvRemLimit.DataSource = _remdet;
        }

        private void BindData()
        {
            ddlSecDef.Items.Clear();
            ddlSecDef.DataSource = null;
            ddlSecDef.DataSource = CHNLSVC.Financial.GetSection();
            ddlSecDef.DisplayMember = "RSS_DESC";
            ddlSecDef.ValueMember = "RSS_CD";

            LoadRemitTypes(Convert.ToString(ddlSecDef.SelectedValue));
            ddlRemTp.SelectedValue = "001";
            pnlBank.Visible = true;
        }

        protected void loadInit()
        {
            Decimal _CIH = 0;
            Decimal _Comm = 0;
            int X = CHNLSVC.Financial.GetDayEndInit(Convert.ToDateTime(txtDate.Value).Date, Convert.ToDateTime(txtDate.Value).Date, "03", "012", "CIH", BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, out  _Comm, out _CIH);
            txtColBonus.Text = _Comm.ToString("0.00", CultureInfo.InvariantCulture);
            txtCIH.Text = _CIH.ToString("0.00", CultureInfo.InvariantCulture);
            clearAll();

            if (CHNLSVC.Financial.IsDayEndDone_win(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Value).Date, Convert.ToDateTime(txtDate.Value).Date) == true)
            {
                txtCIH.Enabled = false;
                txtColBonus.Enabled = false;
            }
            else
            {
                txtCIH.Enabled = true;
                txtColBonus.Enabled = true;
            }

            //load default deposit
            DataTable _DT = CHNLSVC.Sales.get_Def_dep_Bank(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "BANK_SLIP");
            if (_DT.Rows.Count > 0)
            {
                txtBank.Text = _DT.Rows[0]["mpb_sun_ac"].ToString();
                lblBank.Text = _DT.Rows[0]["mpb_bank_name"].ToString();
            }

            DataTable _DT1 = CHNLSVC.Sales.get_Def_dep_Bank(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CHEQUE");
            if (_DT1.Rows.Count > 0)
                textBoxChqDepBank.Text = _DT1.Rows[0]["mpb_sun_ac"].ToString();
        }

        private void InitializeEnv()
        {
            txtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            dtp_Month.Value = Convert.ToDateTime("01" + "/" + dtp_Month.Value.Month + "/" + dtp_Month.Value.Year).Date;

            gvRec.AutoGenerateColumns = false;
            gvDisb.AutoGenerateColumns = false;
            gvSum.AutoGenerateColumns = false;
            gvLess.AutoGenerateColumns = false;
            gvRemLimit.AutoGenerateColumns = false;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCIH.Text))  //25/7/2016
                {
                    MessageBox.Show("Please enter Cash in Hand amount", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCIH.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtColBonus.Text))  //25/7/2016
                {
                    MessageBox.Show("Please enter commission withdrawn amount", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtColBonus.Focus();
                    return;
                }
                if (Convert.ToDateTime(txtDate.Text).Date > DateTime.Now.Date)      //22/8/2013
                {
                    MessageBox.Show("Cannot Process. Check the Date.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (CHNLSVC.Financial.IsDayEndDone_win(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date) == true)
                {
                    MessageBox.Show("You cannot process. You have already processed.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (BaseCls.GlbUserID == string.Empty)
                {
                    MessageBox.Show("Cannot process. Session is expired", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //if (IsAllowBackDateForModule(BaseCls.BaseCls.GlbUserComCode, GlbUserDefLoca, string.Empty, Page, txtDate.Text, imgFromDate, lblDispalyInfor) == true)
                //{
                //    throw new UIValidationException("Back date not allow for selected date!");
                //}
                //check whether session variable are empty


                DateTime _invalidDT;
                if (CHNLSVC.Financial.IsValidDayEndDate(Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserDefProf, out _invalidDT) == false)
                {
                    if (_invalidDT != Convert.ToDateTime("31/Dec/9999"))
                    {
                        MessageBox.Show("Cannot process." + "\n" + "You have to Day End on " + _invalidDT.ToString("dd/MMM/yyyy"), "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Cannot process.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
                #region check bill payement add by tharanga 2018/09/05
                //MasterProfitCenter _mstPcN = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                //if (_mstPcN.Mpc_ce_stus == 1)
                //{
                //    List<Ref_Bill_Collet> Ref_Bill_ColletLIST = new List<Ref_Bill_Collet>();
                //    Ref_Bill_ColletLIST = CHNLSVC.Financial.GetRef_Bill_Collet_dayend(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);
                //    if (Ref_Bill_ColletLIST.Count <= 0)
                //    {
                //        Cursor.Current = Cursors.Default;
                //        MessageBox.Show("Bill record not found", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        return;
                //    }
                //    else
                //    {
                //        foreach (Ref_Bill_Collet _list in Ref_Bill_ColletLIST)
                //        {
                //            if (_list.RBC_CONF_STUS == 0)
                //            {


                //                Decimal tot = _list.RBC_1HDOV + _list.RBC_2HDOV + _list.RBC_3HDOV;
                //                if (MessageBox.Show("Bill record found. Amout is " + tot.ToString("0.00") + ". Do you Need to add ? ", "Day End", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //                {
                //                    Int32 eff = CHNLSVC.Financial.ref_bill_collect_conform(_list.RBC_COM, _list.RBC_COL_LOC, _list.RBC_DT, BaseCls.GlbUserID, 1, _list.RBC_SEQ);

                //                }
                //                else
                //                {
                //                    Int32 eff = CHNLSVC.Financial.ref_bill_collect_conform(_list.RBC_COM, _list.RBC_COL_LOC, _list.RBC_DT, BaseCls.GlbUserID, 2, _list.RBC_SEQ);

                //                }
                //            }
                //        }
                //    }


                //}
                #endregion
                #region check bill payement add by tharanga 2018/09/05
                MasterProfitCenter _mstPcN = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                if (_mstPcN.Mpc_ce_stus == 1)
                {
                    List<Ref_Bill_Collet> Ref_Bill_ColletLIST = new List<Ref_Bill_Collet>();
                    Ref_Bill_ColletLIST = CHNLSVC.Financial.GetRef_Bill_Collet_dayend(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);
                    if (Ref_Bill_ColletLIST.Count <= 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Bill record not found", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {


                        foreach (Ref_Bill_Collet _list in Ref_Bill_ColletLIST)
                        {
                            if (_list.RBC_CONF_STUS == 0)
                            {
                                if (CHNLSVC.Financial.IsDayEndDone_win(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date) == true)
                                {
                                    MessageBox.Show("New bill collection record is found. You cannot add it to dayend process, because dayend is already finalized.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                Decimal tot = _list.RBC_1HDOV + _list.RBC_2HDOV + _list.RBC_3HDOV;
                                string _amount = amount(tot);
                                if (MessageBox.Show("Bill collection record is found. Amout is " + _amount + ". Do you need to add ? ", "Day End", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Int32 eff = CHNLSVC.Financial.ref_bill_collect_conform(_list.RBC_COM, _list.RBC_COL_LOC, _list.RBC_DT, BaseCls.GlbUserID, 1, _list.RBC_SEQ);

                                }
                                else
                                {
                                    Int32 eff = CHNLSVC.Financial.ref_bill_collect_conform(_list.RBC_COM, _list.RBC_COL_LOC, _list.RBC_DT, BaseCls.GlbUserID, 2, _list.RBC_SEQ);

                                }
                            }
                        }
                    }


                }
                #endregion
                //check whether day end is not run for the previous day with having transactions
                DateTime _curDay = Convert.ToDateTime(txtDate.Text).Date;
                DateTime start = _curDay.AddDays(-1);
                DateTime _lastDay;
                int G = CHNLSVC.Financial.GetLastDayEndDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _curDay, out _lastDay);
                DateTime finish = _lastDay.AddDays(-1);

                for (DateTime x = start; x >= finish; x = x.AddDays(-1))
                {
                    if (CHNLSVC.Financial.IsDayEndDone(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, x, x) == false)
                    {
                        //check whether invoices/receipts found
                        if (CHNLSVC.Financial.IsPrvDayTxnsFound(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, x) == true)
                        {
                            MessageBox.Show("Cannot process. Day end is not done on " + x.ToString("dd/MMM/yyyy"), "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }


                //DateTime _prevDay = _curDay.AddDays(-1);
                //if (CHNLSVC.Financial.IsDayEndDone(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _prevDay, _prevDay) == false)
                //{
                //    //check whether invoices/receipts found
                //    if (CHNLSVC.Financial.IsPrvDayTxnsFound(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _prevDay) == true)
                //    {
                //        MessageBox.Show("Cannot process. Day end is not done for the previous day", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }

                //}

                //check whether txns found after process day end for the same day -(13/3/2013)
                if (CHNLSVC.Financial.IsTxnFoundAfterDayEnd(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _curDay, "SALE") == true)
                {
                    MessageBox.Show("Cannot process. Transactions found after process last day end." + "\n" + "Request a backdate from Accounts Dept.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (CHNLSVC.Financial.IsTxnFoundAfterDayEnd(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _curDay, "REC") == true)
                    {
                        MessageBox.Show("Cannot process. Transactions found after process last day end." + "\n" + "Request a backdate from Accounts Dept.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (CHNLSVC.Financial.IsTxnFoundAfterDayEnd(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _curDay, "RVT") == true)
                        {
                            MessageBox.Show("Cannot process. Transactions found after process last day end." + "\n" + "Request a backdate from Accounts Dept.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                }

                Decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtDate.Text).Date, out _wkNo, BaseCls.GlbUserComCode);

                if (_wkNo == 0)
                {
                    MessageBox.Show("Week Definition not found. Contact Accounts dept.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //22/5/2014- check registration receipts not found------------------------------------------------------------------------
                Boolean _isRegistrationMandatory = false;
                DataTable MasterChannel = new DataTable();
                MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
                _isRegistrationMandatory = (MasterChannel.Rows[0]["msc_is_registration"].ToString()) == "1" ? true : false;

                if (_isRegistrationMandatory == true)
                {
                    //HpAccount _letAcc = new HpAccount();
                    //_letAcc = CHNLSVC.Sales.GetLatestHPAccount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                    //if (_letAcc != null)
                    //{
                    List<InvoiceItem> _regAllowItm = new List<InvoiceItem>();
                    _regAllowItm = CHNLSVC.Sales.GetVehRegAllowInv(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDate.Value.Date);

                    if (_regAllowItm != null)
                    {
                        foreach (InvoiceItem _r in _regAllowItm)
                        {
                            //check invoice is reversed  17/6/2014
                            Boolean _isInvRevsed = CHNLSVC.Financial.IsInvReversed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _r.Sad_inv_no);
                            if (_isInvRevsed == false)
                            {
                                List<VehicalRegistration> _tmpReg = new List<VehicalRegistration>();
                                _tmpReg = CHNLSVC.Sales.CheckVehRegTxn(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _r.Sad_inv_no, _r.Sad_itm_cd);

                                if (_tmpReg == null)
                                {
                                    DataTable _dtInvH = CHNLSVC.Sales.GetSalesHdr(_r.Sad_inv_no);        //kapila 1/2/2016
                                    _tmpReg = CHNLSVC.Sales.CheckVehRegTxn(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _dtInvH.Rows[0]["sah_structure_seq"].ToString(), _r.Sad_itm_cd);

                                    if (_tmpReg == null)
                                    {
                                        //bool _isNotAllow = false;
                                        //DataTable _dtSer = CHNLSVC.Sales.getSerialByInvLine(_r.Sad_inv_no, _r.Sad_itm_line);
                                        //foreach (DataRow row2 in _dtSer.Rows)
                                        //{
                                        //    if (CHNLSVC.Sales.IsRegInsuAllowed(BaseCls.GlbUserComCode, "VHREG", _dtSer.Rows[0]["itb_itm_stus"].ToString(), "") == false)
                                        //    {
                                        //        _isNotAllow = true;
                                        //        break;
                                        //    }
                                        //}

                                        //if (_isNotAllow == true)
                                        //{
                                        MessageBox.Show("Registration not completed for invoice : " + _r.Sad_inv_no + " Item : " + _r.Sad_itm_cd, "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    //}
                                }
                            }
                        }
                    }
                    //}
                }

                //16/6/2015- check insurance receipts not found------------------------------------------------------------------------
                Boolean _isInsuMandatory = false;   //re activated 1/12/2015
                DataTable MasterChnl = new DataTable();
                MasterChnl = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
                _isInsuMandatory = (MasterChnl.Rows[0]["msc_is_veh_ins"].ToString()) == "1" ? true : false;

                if (_isInsuMandatory == true)
                {
                    List<InvoiceItem> _insuAllowItm = new List<InvoiceItem>();
                    List<InvoiceItem> _insuAlwCredItm_ = new List<InvoiceItem>();   //kapila 6/1/2016

                    _insuAllowItm = CHNLSVC.Sales.GetVehInsuAllowInv(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDate.Value.Date);
                    _insuAlwCredItm_ = CHNLSVC.Sales.GetVehInsuAllowCredInv(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDate.Value.Date);

                    var _allInvoices = _insuAllowItm;
                    if (_insuAlwCredItm_ != null)
                        if (_insuAllowItm != null)
                            _allInvoices = _insuAllowItm.Concat(_insuAlwCredItm_).ToList();


                    if (_allInvoices != null)
                    {
                        foreach (InvoiceItem _r in _allInvoices)
                        {
                            //check invoice is reversed  17/6/2014
                            Boolean _isInvRevsed = CHNLSVC.Financial.IsInvReversed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _r.Sad_inv_no);
                            if (_isInvRevsed == false)
                            {
                                List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_r.Sad_inv_no);
                                if (_insurance == null)
                                {
                                    DataTable _dtInvH = CHNLSVC.Sales.GetSalesHdr(_r.Sad_inv_no);        //kapila 2/2/2016
                                    _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_dtInvH.Rows[0]["sah_structure_seq"].ToString());
                                    if (_insurance == null)
                                    {
                                        //bool _isNotAllow = false;
                                        //DataTable _dtSer = CHNLSVC.Sales.getSerialByInvLine(_r.Sad_inv_no, _r.Sad_itm_line);
                                        //foreach (DataRow row2 in _dtSer.Rows)
                                        //{
                                        //    if (CHNLSVC.Sales.IsRegInsuAllowed(BaseCls.GlbUserComCode, "VHINS", _dtSer.Rows[0]["itb_itm_stus"].ToString(), "") == false)
                                        //    {
                                        //        _isNotAllow = true;
                                        //        break;
                                        //    }
                                        //}

                                        //if (_isNotAllow == true)
                                        //{
                                        MessageBox.Show("Vehicle Insurance is not completed for the invoice : " + _r.Sad_inv_no + " Item : " + _r.Sad_itm_cd, "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    //}
                                }
                            }
                        }
                    }

                    //kapila 6/1/2016
                    DataTable _dtDCNItms = CHNLSVC.Sales.GetVehInsuAllowDCNItems(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtDate.Value.Date);
                    foreach (DataRow r in _dtDCNItms.Rows)
                    {
                        List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(r["qh_no"].ToString());
                        if (_insurance == null)
                        {
                            _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(r["ith_oth_docno"].ToString());  //kapila 
                            if (_insurance == null)
                            {
                                MessageBox.Show("Vehicle Insurance is not completed for the quotation : " + r["qh_no"].ToString() + " Item : " + r["iti_itm_cd"].ToString(), "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                        }
                        //kapila 1/2/2016
                        List<VehicalRegistration> _tmpReg = new List<VehicalRegistration>();
                        _tmpReg = CHNLSVC.Sales.CheckVehRegTxn(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, r["ith_oth_docno"].ToString(), r["iti_itm_cd"].ToString());

                        if (_tmpReg == null)
                        {
                            DataTable _dtInvH = CHNLSVC.Sales.GetSalesHdr(r["ith_oth_docno"].ToString());        //kapila 1/2/2016
                            _tmpReg = CHNLSVC.Sales.CheckVehRegTxn(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, r["qh_no"].ToString(), r["iti_itm_cd"].ToString());

                            if (_tmpReg == null)    //kapila 10/2/2016
                            {
                                MessageBox.Show("Vehicle registration is not completed for the quotation : " + r["qh_no"].ToString() + " Item : " + r["iti_itm_cd"].ToString(), "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                }
                //kapila 11/1/2016
                DataTable _dtDCNCust = CHNLSVC.Sales.get_DCN_DP_Cust(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtDate.Value.Date);
                foreach (DataRow r in _dtDCNCust.Rows)
                {
                    DataTable _dtDCNRec = CHNLSVC.Sales.get_DCN_receipt(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, (r["qh_no"].ToString()));
                    if (_dtDCNRec.Rows.Count == 0)
                    {
                        MessageBox.Show("Advance receipt not found for the quotation : " + r["qh_no"].ToString(), "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }
                }

                //-----------------------------------------------------------------------------------

                if (MessageBox.Show("This is the Final Day End Process. You cannot process again." + "\n" + "Are You Sure?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                DateTime _st = DateTime.Now;
                int _processSeq = CHNLSVC.CommonSearch.StartTimeModule("DAY END - FINAL", "", DateTime.Now, BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, BaseCls.GlbUserID, txtDate.Value.Date);


                //process commission
                string _outErr = "";
                int _effComm = CHNLSVC.Sales.Process_DayEnd_Commission(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, out _outErr);
                if (_effComm == 0)      //2/7/2014
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Process halted." + "\n" + "Reason: commission definition not found for " + _outErr, "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                MasterProfitCenter _mstPc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                if (_mstPc.Mpc_ce_stus == 1)
                {
                    List<Ref_Bill_Collet> Ref_Bill_ColletLIST = new List<Ref_Bill_Collet>();
                    Ref_Bill_ColletLIST = CHNLSVC.Financial.GetRef_Bill_Collet_dayend(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);
                    foreach (Ref_Bill_Collet _list in Ref_Bill_ColletLIST)
                    {
                        
                    }
                } 

                //kapila 28/6/2016 introducer commission
                DataTable _dtIntrComm = null;
                if (BaseCls.GlbUserComCode == "AAL")
                    _dtIntrComm = CHNLSVC.Financial.processIntroduComm(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserID);

                MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                DataTable DT = CHNLSVC.Financial.ProcessDayEnd(Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(_wkNo), BaseCls.GlbUserID, mst_com.Mc_anal24);

                //process other location sale commission   13/8/2013    
                Int32 R = CHNLSVC.Financial.Process_OtherLocation_Selling_Comm(Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, Convert.ToInt32(_wkNo), BaseCls.GlbUserID);

                //kapila 16/11/2013 group sale comm
                Int32 T = CHNLSVC.Financial.Process_group_sale_Selling_Comm(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);

                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();

                DataTable dtESD_EPF_WHT = new DataTable();
                dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);

                Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
                if (dtESD_EPF_WHT.Rows.Count > 0)
                {
                    ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                    EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                    WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

                }

                _remSumDet.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet.Rem_pc = BaseCls.GlbUserDefProf;
                _remSumDet.Rem_dt = Convert.ToDateTime(txtDate.Text).Date;
                _remSumDet.Rem_sec = "03";
                _remSumDet.Rem_cd = "012";
                _remSumDet.Rem_sh_desc = "Commission Withdrawn";
                _remSumDet.Rem_lg_desc = "Commission Withdrawn";
                _remSumDet.Rem_val = Convert.ToDecimal(txtColBonus.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtColBonus.Text);
                _remSumDet.Rem_week = (_wkNo + "S").ToString();
                _remSumDet.Rem_ref_no = "";
                _remSumDet.Rem_rmk = "";
                _remSumDet.Rem_cr_acc = "";
                _remSumDet.Rem_db_acc = "";
                _remSumDet.Rem_cre_by = BaseCls.GlbUserID;
                _remSumDet.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumDet.Rem_is_sos = false;
                _remSumDet.Rem_is_dayend = true;
                _remSumDet.Rem_is_sun = false;
                _remSumDet.Rem_del_alw = true;
                _remSumDet.Rem_is_rem_sum = false;
                _remSumDet.Rem_cat = 12;
                _remSumDet.Rem_add = 0;
                _remSumDet.Rem_ded = 0;
                _remSumDet.Rem_net = 0;
                _remSumDet.Rem_epf = EPF_rt;
                _remSumDet.Rem_esd = ESD_rt;
                _remSumDet.Rem_wht = WHT_rt;
                _remSumDet.Rem_add_fin = 0;
                _remSumDet.Rem_ded_fin = 0;
                _remSumDet.Rem_net_fin = 0;
                _remSumDet.Rem_rmk_fin = "";
                _remSumDet.Rem_bnk_cd = "";
                _remSumDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
                //8/8/2013
                _remSumDet.REM_CHQNO = "";
                _remSumDet.REM_CHQ_BANK_CD = "";
                _remSumDet.REM_CHQ_BRANCH = "";
                _remSumDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                _remSumDet.REM_DEPOSIT_BANK_CD = "";
                _remSumDet.REM_DEPOSIT_BRANCH = "";

                //save commission
                int row_aff = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet);

                RemitanceSummaryDetail _remSumDet1 = new RemitanceSummaryDetail();

                _remSumDet1.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet1.Rem_pc = BaseCls.GlbUserDefProf;
                _remSumDet1.Rem_dt = Convert.ToDateTime(txtDate.Text).Date;
                _remSumDet1.Rem_sec = "06";
                _remSumDet1.Rem_cd = "001";
                _remSumDet1.Rem_sh_desc = "Cash In Hand";
                _remSumDet1.Rem_lg_desc = "Cash In Hand";
                _remSumDet1.Rem_val = Convert.ToDecimal(txtCIH.Text);
                _remSumDet1.Rem_val_final = Convert.ToDecimal(txtCIH.Text);
                _remSumDet1.Rem_week = (_wkNo + "S").ToString();
                _remSumDet1.Rem_ref_no = "";
                _remSumDet1.Rem_rmk = "";
                _remSumDet1.Rem_cr_acc = "";
                _remSumDet1.Rem_db_acc = "";
                _remSumDet1.Rem_cre_by = BaseCls.GlbUserID;
                _remSumDet1.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumDet1.Rem_is_sos = false;
                _remSumDet1.Rem_is_dayend = true;
                _remSumDet1.Rem_is_sun = false;
                _remSumDet1.Rem_del_alw = true;
                _remSumDet1.Rem_is_rem_sum = false;
                _remSumDet1.Rem_cat = 12;
                _remSumDet1.Rem_add = 0;
                _remSumDet1.Rem_ded = 0;
                _remSumDet1.Rem_net = 0;
                _remSumDet1.Rem_epf = EPF_rt;
                _remSumDet1.Rem_esd = ESD_rt;
                _remSumDet1.Rem_wht = WHT_rt;
                _remSumDet1.Rem_add_fin = 0;
                _remSumDet1.Rem_ded_fin = 0;
                _remSumDet1.Rem_net_fin = 0;
                _remSumDet1.Rem_rmk_fin = "";
                _remSumDet1.Rem_bnk_cd = "";
                _remSumDet1.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
                //8/8/2013
                _remSumDet1.REM_CHQNO = "";
                _remSumDet1.REM_CHQ_BANK_CD = "";
                _remSumDet1.REM_CHQ_BRANCH = "";
                _remSumDet1.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                _remSumDet1.REM_DEPOSIT_BANK_CD = "";
                _remSumDet1.REM_DEPOSIT_BRANCH = "";

                //save cash in hand
                int row_aff1 = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet1);

                RemitanceSummaryDetail _remSumDet2 = new RemitanceSummaryDetail();
                //DateTime _curDay = Convert.ToDateTime(txtDate.Text);
                //DateTime _nextDay = _curDay.AddDays(1);
                Decimal _prvDayCIH = 0;
                int XX = CHNLSVC.Financial.Get_prv_day_CIH(Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, out _prvDayCIH);

                _remSumDet2.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet2.Rem_pc = BaseCls.GlbUserDefProf;
                _remSumDet2.Rem_dt = Convert.ToDateTime(txtDate.Text).Date;
                _remSumDet2.Rem_sec = "03";
                _remSumDet2.Rem_cd = "008";
                _remSumDet2.Rem_sh_desc = "Prv. Day Cash in Hand";
                _remSumDet2.Rem_lg_desc = "Prv. Day Cash in Hand";
                _remSumDet2.Rem_val = _prvDayCIH;
                _remSumDet2.Rem_val_final = _prvDayCIH;
                _remSumDet2.Rem_week = (_wkNo + "S").ToString();
                _remSumDet2.Rem_ref_no = "";
                _remSumDet2.Rem_rmk = "";
                _remSumDet2.Rem_cr_acc = "";
                _remSumDet2.Rem_db_acc = "";
                _remSumDet2.Rem_del_alw = false;
                _remSumDet2.Rem_cre_by = BaseCls.GlbUserID;
                _remSumDet2.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumDet2.Rem_is_sos = false;
                _remSumDet2.Rem_is_dayend = true;
                _remSumDet2.Rem_is_sun = false;
                _remSumDet2.Rem_is_rem_sum = true;
                _remSumDet2.Rem_cat = 12;
                _remSumDet2.Rem_add = 0;
                _remSumDet2.Rem_ded = 0;
                _remSumDet2.Rem_net = 0;
                _remSumDet2.Rem_epf = EPF_rt;
                _remSumDet2.Rem_esd = ESD_rt;
                _remSumDet2.Rem_wht = WHT_rt;
                _remSumDet2.Rem_add_fin = 0;
                _remSumDet2.Rem_ded_fin = 0;
                _remSumDet2.Rem_net_fin = 0;
                _remSumDet2.Rem_rmk_fin = "";
                _remSumDet2.Rem_bnk_cd = "";
                _remSumDet2.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");

                //8/8/2013
                _remSumDet2.REM_CHQNO = "";
                _remSumDet2.REM_CHQ_BANK_CD = "";
                _remSumDet2.REM_CHQ_BRANCH = "";
                _remSumDet2.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                _remSumDet2.REM_DEPOSIT_BANK_CD = "";
                _remSumDet2.REM_DEPOSIT_BRANCH = "";

                //save prv. day cash in hand
                int row_aff2 = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet2);

                BindReceiptGridData(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);
                BindDisbursementGridData(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);
                BindSummaryGridData(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);
                BindLessGridData(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);

                //Boolean _isOK = calc();
                //BindSummaryGridData(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date , Convert.ToDateTime(txtDate.Text));

                ////btnConfirm.Enabled = true;
                //DayEnd _dayEnd = new DayEnd();
                //_dayEnd.Upd_com = BaseCls.GlbUserComCode;
                //_dayEnd.Upd_pc = BaseCls.GlbUserDefProf;
                //_dayEnd.Upd_dt = Convert.ToDateTime(txtDate.Text);
                //_dayEnd.Upd_cre_dt = DateTime.Now;
                //_dayEnd.Upd_cre_by = BaseCls.GlbUserID;
                //_dayEnd.Upd_ov_wrt = false;

                //int X = CHNLSVC.Financial.SaveDayEnd(_dayEnd);

                Boolean _isOK = calc();
                if (_isOK == true)
                {
                    BindSummaryGridData(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);

                    //btnConfirm.Enabled = true;
                    DayEnd _dayEnd = new DayEnd();
                    _dayEnd.Upd_com = BaseCls.GlbUserComCode;
                    _dayEnd.Upd_pc = BaseCls.GlbUserDefProf;
                    _dayEnd.Upd_dt = Convert.ToDateTime(txtDate.Text).Date;
                    _dayEnd.Upd_cre_dt = DateTime.Now;
                    _dayEnd.Upd_cre_by = BaseCls.GlbUserID;
                    _dayEnd.Upd_ov_wrt = false;

                    int X = CHNLSVC.Financial.SaveDayEnd(_dayEnd);

                    Int32 K = CHNLSVC.Sales.UpdateDayEnd(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);
                }
                else
                {
                    clearAll();
                    DateTime _ed1 = DateTime.Now;
                    TimeSpan _diff1 = new TimeSpan();
                    _diff1 = _ed1 - _st;
                    SendMail(_processSeq, BaseCls.GlbUserDefProf + " - Day End - FINAL SHORT", _st, _ed1, _diff1);

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Cannot proceed. Remitance is short !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime _ed = DateTime.Now;
                TimeSpan _diff = new TimeSpan();
                _diff = _ed - _st;
                Cursor.Current = Cursors.Default;
                SendMail(_processSeq, BaseCls.GlbUserDefProf + " - Day End - FINAL", _st, _ed, _diff);
                MessageBox.Show("Completed", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //imgFromDate.Visible = false;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Day End Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        public void SendMail(int _proSeq, string _subject, DateTime _st, DateTime _ed, TimeSpan _diffTimeSpan)
        {
            CHNLSVC.CommonSearch.EndTimeModule(_proSeq, DateTime.Now, _diffTimeSpan);
            //string _emails = CHNLSVC.CommonSearch.GetModuleWiseEmail("DAY END");

            //string[] to = _emails.Split(';');

            //foreach (string emailAdd in to)
            //{
            //    if (!string.IsNullOrEmpty(emailAdd))
            //        CHNLSVC.CommonSearch.Send_SMTPMail(emailAdd, _subject, "Start Time - " + _st + " End Time - " + _ed + " Duration - " + _diffTimeSpan);
            //}
        }

        private void clearAll()
        {
            gvRec.DataSource = null;

            gvSum.DataSource = null;

            gvDisb.DataSource = null;

            gvLess.DataSource = null;

            lbl_banked.Text = "0.00";
            lbl_CIH.Text = "0.00";
            lbl_comm_wdr.Text = "0.00";
            lbl_diff.Text = "0.00";
            lbl_TotRem.Text = "0.00";

            txtDisbTot.Text = "0.00";
            txtRecTot.Text = "0.00";
            txtLessTot.Text = "0.00";
            txtSumTot.Text = "0.00";

        }

        private void BindDisbursementGridData(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_Win(BaseCls.GlbUserComCode, _pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "02", out _total, out _totalFinal);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val = dr["rem_val"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val = item.Key.rem_val,

                   };

                gvDisb.DataSource = _tblItems;
                txtDisbTot.Text = "0.00";
            }
            else
            {
                gvDisb.DataSource = CHNLSVC.Financial.GetRemSummaryReport_Win(BaseCls.GlbUserComCode, _pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "02", out _total, out _totalFinal);
                txtDisbTot.Text = _total.ToString("0,0.00", CultureInfo.InvariantCulture);
            }
        }

        private void BindDisbursementGridData_view(string _pc, DateTime _fromdate, DateTime _todate)
        {

            Decimal _val = 0;
            //DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_Win(_pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "02", out _total, out _totalFinal);
            MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            DataTable _tbl = CHNLSVC.Financial.get_Rem_Sum_Rep_View(Convert.ToDateTime(_fromdate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "02", Convert.ToDecimal(txtColBonus.Text), mst_com.Mc_anal24);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val = dr["rem_val"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val = item.Key.rem_val,

                   };

                gvDisb.DataSource = _tblItems;
                txtDisbTot.Text = "0.00";
            }
            else
            {
                gvDisb.DataSource = _tbl;
                for (int i = 0; i < _tbl.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(_tbl.Rows[i]["rem_val"].ToString()))
                    {
                        _val = _val + Convert.ToDecimal(_tbl.Rows[i]["rem_val"]);
                    }

                }

                //gvDisb.DataSource = CHNLSVC.Financial.get_Rem_Sum_Rep_View(Convert.ToDateTime(_fromdate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "02");
                txtDisbTot.Text = _val.ToString("0,0.00", CultureInfo.InvariantCulture);
            }


        }

        private void BindSummaryGridData(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_without_comm_withdraw_win(BaseCls.GlbUserComCode, _pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "03", out _total, out _totalFinal, 0);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val = dr["rem_val"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val = item.Key.rem_val,

                   };

                gvSum.DataSource = _tblItems;
                txtSumTot.Text = "0.00";
            }
            else
            {
                gvSum.DataSource = CHNLSVC.Financial.GetRemSummaryReport_without_comm_withdraw_win(BaseCls.GlbUserComCode, _pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "03", out _total, out _totalFinal, 0);
                txtSumTot.Text = _total.ToString("0,0.00", CultureInfo.InvariantCulture);
            }


        }

        decimal Fineval = 0;
        private void BindSummaryGridData_view(string _pc, DateTime _fromdate, DateTime _todate)
        {

            Decimal _val = 0;
            //DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_without_comm_withdraw_win(_pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "03", out _total, out _totalFinal, 0);
            MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            DataTable _tbl = CHNLSVC.Financial.get_Rem_Sum_Rep_View(Convert.ToDateTime(_fromdate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "03", Convert.ToDecimal(txtColBonus.Text), mst_com.Mc_anal24);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val = dr["rem_val"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val = item.Key.rem_val,

                   };

                gvSum.DataSource = _tblItems;
                txtSumTot.Text = "0.00";
            }
            else
            {
                gvSum.DataSource = _tbl;
                for (int i = 0; i < _tbl.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(_tbl.Rows[i]["rem_val"].ToString()))
                    {
                        _val = _val + Convert.ToDecimal(_tbl.Rows[i]["rem_val"]);
                    }
                    //Fineval = Convert.ToDecimal(_tblItems.Where(y => (int)y.rem_cd == 27));
                }

                var _tblItemsFine =
                  from dr in _tbl.AsEnumerable()
                  group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val = dr["rem_val"] } into item
                  select new
                  {
                      rem_cd = item.Key.rem_cd,
                      rem_sh_desc = item.Key.rem_sh_desc,
                      rem_val = item.Key.rem_val,

                  };
                
               // Fineval = Convert.ToDecimal(_tblItemsFine.Where(y => (int)y.rem_cd == 005));
           //     var Fineval1 = _tblItemsFine.Where(y => y.rem_cd == "027").SingleOrDefault().rem_val;

                //gvSum.DataSource = CHNLSVC.Financial.GetRemSummaryReport_without_comm_withdraw(_pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "03", out _total, out _totalFinal, 0);
                txtSumTot.Text = _val.ToString("0,0.00", CultureInfo.InvariantCulture);

                
            }


        }

        private void BindLessGridData(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_Win(BaseCls.GlbUserComCode, _pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "04", out _total, out _totalFinal);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val = dr["rem_val"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val = item.Key.rem_val,

                   };

                gvLess.DataSource = _tblItems;
                txtLessTot.Text = "0.00";
            }
            else
            {
                gvLess.DataSource = CHNLSVC.Financial.GetRemSummaryReport_Win(BaseCls.GlbUserComCode, _pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "04", out _total, out _totalFinal);
                txtLessTot.Text = _total.ToString("0,0.00", CultureInfo.InvariantCulture);
            }


        }

        private void BindLessGridData_view(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            Decimal _val = 0;
            //DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_Win(_pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "04", out _total, out _totalFinal);
            MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            DataTable _tbl = CHNLSVC.Financial.get_Rem_Sum_Rep_View(Convert.ToDateTime(_fromdate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "04", Convert.ToDecimal(txtColBonus.Text), mst_com.Mc_anal24);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val = dr["rem_val"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val = item.Key.rem_val,

                   };

                gvLess.DataSource = _tblItems;
                txtLessTot.Text = "0.00";

                decimal val = Convert.ToDecimal(_tblItems.Where(y => (int)y.rem_cd == 002));
    
            }
            else
            {
                gvLess.DataSource = _tbl;
                for (int i = 0; i < _tbl.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(_tbl.Rows[i]["rem_val"].ToString()))
                    {
                        _val = _val + Convert.ToDecimal(_tbl.Rows[i]["rem_val"]);
                    }

                }
                //gvLess.DataSource = CHNLSVC.Financial.get_Rem_Sum_Rep_View(Convert.ToDateTime(_fromdate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "04");
                txtLessTot.Text = _val.ToString("0,0.00", CultureInfo.InvariantCulture);

               
            }


        }

        private void BindReceiptGridData_View(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            Decimal _val = 0;
            //DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport(_pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "01", out _total, out _totalFinal);
            MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            DataTable _tbl = CHNLSVC.Financial.get_Rem_Sum_Rep_View(Convert.ToDateTime(_fromdate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "01", Convert.ToDecimal(txtColBonus.Text), mst_com.Mc_anal24);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val = dr["rem_val"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val = item.Key.rem_val,

                   };

                gvRec.DataSource = _tblItems;
                txtRecTot.Text = "0.00";
            }
            else
            {
                gvRec.DataSource = _tbl;
                for (int i = 0; i < _tbl.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(_tbl.Rows[i]["rem_val"].ToString()))
                    {
                        _val = _val + Convert.ToDecimal(_tbl.Rows[i]["rem_val"]);
                    }

                }
                //gvRec.DataSource = CHNLSVC.Financial.get_Rem_Sum_Rep_View(Convert.ToDateTime(_fromdate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "01");;
                txtRecTot.Text = _val.ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            //gvRec.DataBind();

        }

        private void BindReceiptGridData(string _pc, DateTime _fromdate, DateTime _todate)
        {
            Decimal _total = 0;
            Decimal _totalFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_Win(BaseCls.GlbUserComCode, _pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "01", out _total, out _totalFinal);

            if (_tbl.Rows.Count <= 0)
            {

                var _tblItems =
                   from dr in _tbl.AsEnumerable()
                   group dr by new { rem_cd = dr["rem_cd"], rem_sh_desc = dr["rem_sh_desc"], rem_val = dr["rem_val"] } into item
                   select new
                   {
                       rem_cd = item.Key.rem_cd,
                       rem_sh_desc = item.Key.rem_sh_desc,
                       rem_val = item.Key.rem_val,

                   };

                gvRec.DataSource = _tblItems;
                txtRecTot.Text = "0.00";
            }
            else
            {
                gvRec.DataSource = CHNLSVC.Financial.GetRemSummaryReport_Win(BaseCls.GlbUserComCode, _pc, Convert.ToDateTime(_fromdate).Date, Convert.ToDateTime(_todate).Date, "01", out _total, out _totalFinal);
                txtRecTot.Text = _total.ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            //gvRec.DataBind();

        }

        private Boolean calc()
        {
            //prv day excess
            Boolean _isSuccess = true;
            Decimal _prvDayExcess = 0;
            Decimal _excessRem = 0;
            int Y = CHNLSVC.Financial.GetPrvDayExcess(Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserDefProf,BaseCls.GlbUserComCode, out _prvDayExcess);

            //total deductions (sum of section 03 without gross remitance and Commission Withdrawn)
            Decimal _totDeduct = 0;
            int Z = CHNLSVC.Financial.GetTotDeductions(Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserDefProf, out _totDeduct);

            //excess remitance for the day (tot remitance + prev. day excess - tot disbursement + commision withdrawn)
            Decimal _totDisb = 0;
            int K = CHNLSVC.Financial.GetTotDisbForCalc_Excess(Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserDefProf, out _totDisb);
            _excessRem = _totDisb + _prvDayExcess - _totDeduct;

            //get net value of collection bonus
            //Decimal _NetColBonus = 0;
            //int J = CHNLSVC.Financial.GetTotCollBonus(Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date,"02","013",BaseCls.GlbUserDefProf,out _NetColBonus);

            //_excessRem = _totDisb + _prvDayExcess - _totDeduct - Convert.ToDecimal(txtColBonus.Text);
            _excessRem = _totDisb + _prvDayExcess - _totDeduct;

            //check whether comm withdrawn is exceeds the excess remitance
            if (_excessRem > 0)
            {
                if (_excessRem * 1000 > Convert.ToDecimal(3))
                {
                    if (Convert.ToDecimal(txtColBonus.Text) > _excessRem)
                    {
                        if (Convert.ToDecimal(txtColBonus.Text) - _excessRem > 1)
                        {
                            _isSuccess = false;
                        }
                        else
                        {
                            _isSuccess = true;
                            _excessRem = _totDisb + _prvDayExcess - _totDeduct - Convert.ToDecimal(txtColBonus.Text);
                        }
                    }
                    else
                    {
                        _isSuccess = true;
                        _excessRem = _totDisb + _prvDayExcess - _totDeduct - Convert.ToDecimal(txtColBonus.Text);
                    }
                }
            }
            //remitance to be banked
            Decimal _totSum = Convert.ToDecimal(txtSumTot.Text) + _excessRem;
            Decimal _totLess = Convert.ToDecimal(txtLessTot.Text);
            lbl_banked.Text = (_totSum - _totLess).ToString("0,0.00", CultureInfo.InvariantCulture);

            //cash in hand
            Decimal _CIH = Convert.ToDecimal(txtCIH.Text);
            lbl_CIH.Text = _CIH.ToString("0,0.00", CultureInfo.InvariantCulture);

            //total remitance entered by manually 
            Decimal _totRemManual = 0;
            Decimal _totRemManualFinal = 0;
            DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_Win(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date, "05", out _totRemManual, out _totRemManualFinal);

            //total remitance
            Decimal _totRem = 0;
            int X = CHNLSVC.Financial.GetTotRemitance(BaseCls.GlbUserComCode, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserDefProf, out _totRem);
            _totRem = _totRem + _totRemManual;
            lbl_TotRem.Text = (_totRem).ToString("0,0.00", CultureInfo.InvariantCulture);

            Decimal _weekNo = 0;
            decimal _wkno = 0;
            _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtDate.Text).Date, out _wkno, BaseCls.GlbUserComCode);

            DataTable dtESD_EPF_WHT = new DataTable();
            dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);

            Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
            if (dtESD_EPF_WHT.Rows.Count > 0)
            {
                ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

            }

            //add total remitance into gnt_rem_sum table (sec-99 , code-001) use by shani
            RemitanceSummaryDetail _remSumDet001 = new RemitanceSummaryDetail();

            _remSumDet001.Rem_com = BaseCls.GlbUserComCode;
            _remSumDet001.Rem_pc = BaseCls.GlbUserDefProf;
            _remSumDet001.Rem_dt = Convert.ToDateTime(txtDate.Text).Date;
            _remSumDet001.Rem_sec = "99";
            _remSumDet001.Rem_cd = "001";
            _remSumDet001.Rem_sh_desc = "Total Remmitance";
            _remSumDet001.Rem_lg_desc = "Total Remmitance";
            _remSumDet001.Rem_val = _totRem;
            _remSumDet001.Rem_val_final = _totRem;
            _remSumDet001.Rem_week = (_wkno + "S").ToString();
            _remSumDet001.Rem_ref_no = "";
            _remSumDet001.Rem_rmk = "";
            _remSumDet001.Rem_cr_acc = "";
            _remSumDet001.Rem_db_acc = "";
            _remSumDet001.Rem_del_alw = false;
            _remSumDet001.Rem_cre_by = BaseCls.GlbUserID;
            _remSumDet001.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _remSumDet001.Rem_is_sos = false;
            _remSumDet001.Rem_is_dayend = true;
            _remSumDet001.Rem_is_sun = false;
            _remSumDet001.Rem_is_rem_sum = true;
            _remSumDet001.Rem_cat = 12;
            _remSumDet001.Rem_add = 0;
            _remSumDet001.Rem_ded = 0;
            _remSumDet001.Rem_net = 0;
            _remSumDet001.Rem_epf = EPF_rt;
            _remSumDet001.Rem_esd = ESD_rt;
            _remSumDet001.Rem_wht = WHT_rt;
            _remSumDet001.Rem_add_fin = 0;
            _remSumDet001.Rem_ded_fin = 0;
            _remSumDet001.Rem_net_fin = 0;
            _remSumDet001.Rem_rmk_fin = "";
            _remSumDet001.Rem_bnk_cd = "";
            _remSumDet001.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
            //8/8/2013
            _remSumDet001.REM_CHQNO = "";
            _remSumDet001.REM_CHQ_BANK_CD = "";
            _remSumDet001.REM_CHQ_BRANCH = "";
            _remSumDet001.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
            _remSumDet001.REM_DEPOSIT_BANK_CD = "";
            _remSumDet001.REM_DEPOSIT_BRANCH = "";

            //save total remmitance
            int row_aff2 = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet001);

            //difference
            lbl_diff.Text = (_totSum - _totLess - _CIH - _totRem).ToString("0,0.00", CultureInfo.InvariantCulture);

            //add difference into gnt_rem_sum table (sec-99 , code-002)  use by shani
            RemitanceSummaryDetail _remSumDet002 = new RemitanceSummaryDetail();

            _remSumDet001.Rem_com = BaseCls.GlbUserComCode;
            _remSumDet001.Rem_pc = BaseCls.GlbUserDefProf;
            _remSumDet001.Rem_dt = Convert.ToDateTime(txtDate.Text).Date;
            _remSumDet001.Rem_sec = "99";
            _remSumDet001.Rem_cd = "002";
            _remSumDet001.Rem_sh_desc = "Difference";
            _remSumDet001.Rem_lg_desc = "Difference";
            _remSumDet001.Rem_val = Convert.ToDecimal(lbl_diff.Text);
            _remSumDet001.Rem_val_final = Convert.ToDecimal(lbl_diff.Text);
            _remSumDet001.Rem_week = (_wkno + "S").ToString();
            _remSumDet001.Rem_ref_no = "";
            _remSumDet001.Rem_rmk = "";
            _remSumDet001.Rem_cr_acc = "";
            _remSumDet001.Rem_db_acc = "";
            _remSumDet001.Rem_del_alw = false;
            _remSumDet001.Rem_cre_by = BaseCls.GlbUserID;
            _remSumDet001.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _remSumDet001.Rem_is_sos = false;
            _remSumDet001.Rem_is_dayend = true;
            _remSumDet001.Rem_is_sun = false;
            _remSumDet001.Rem_is_rem_sum = true;
            _remSumDet001.Rem_cat = 12;
            _remSumDet001.Rem_add = 0;
            _remSumDet001.Rem_ded = 0;
            _remSumDet001.Rem_net = 0;
            _remSumDet001.Rem_epf = EPF_rt;
            _remSumDet001.Rem_esd = ESD_rt;
            _remSumDet001.Rem_wht = WHT_rt;
            _remSumDet001.Rem_add_fin = 0;
            _remSumDet001.Rem_ded_fin = 0;
            _remSumDet001.Rem_net_fin = 0;
            _remSumDet001.Rem_rmk_fin = "";
            _remSumDet001.Rem_bnk_cd = "";
            _remSumDet001.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
            //8/8/2013
            _remSumDet001.REM_CHQNO = "";
            _remSumDet001.REM_CHQ_BANK_CD = "";
            _remSumDet001.REM_CHQ_BRANCH = "";
            _remSumDet001.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
            _remSumDet001.REM_DEPOSIT_BANK_CD = "";
            _remSumDet001.REM_DEPOSIT_BRANCH = "";

            //save difference
            int row_aff3 = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet001);

            //add remitance to be banked into gnt_rem_sum table (sec-99 , code-003)  use by sanjeewa  15/7/2014
            RemitanceSummaryDetail _remSumDet003 = new RemitanceSummaryDetail();

            _remSumDet003.Rem_com = BaseCls.GlbUserComCode;
            _remSumDet003.Rem_pc = BaseCls.GlbUserDefProf;
            _remSumDet003.Rem_dt = Convert.ToDateTime(txtDate.Text).Date;
            _remSumDet003.Rem_sec = "99";
            _remSumDet003.Rem_cd = "003";
            _remSumDet003.Rem_sh_desc = "Remitance to be Banked";
            _remSumDet003.Rem_lg_desc = "Remitance to be Banked";
            _remSumDet003.Rem_val = Convert.ToDecimal(lbl_banked.Text);
            _remSumDet003.Rem_val_final = Convert.ToDecimal(lbl_banked.Text);
            _remSumDet003.Rem_week = (_wkno + "S").ToString();
            _remSumDet003.Rem_ref_no = "";
            _remSumDet003.Rem_rmk = "";
            _remSumDet003.Rem_cr_acc = "";
            _remSumDet003.Rem_db_acc = "";
            _remSumDet003.Rem_del_alw = false;
            _remSumDet003.Rem_cre_by = BaseCls.GlbUserID;
            _remSumDet003.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _remSumDet003.Rem_is_sos = false;
            _remSumDet003.Rem_is_dayend = true;
            _remSumDet003.Rem_is_sun = false;
            _remSumDet003.Rem_is_rem_sum = true;
            _remSumDet003.Rem_cat = 12;
            _remSumDet003.Rem_add = 0;
            _remSumDet003.Rem_ded = 0;
            _remSumDet003.Rem_net = 0;
            _remSumDet003.Rem_epf = EPF_rt;
            _remSumDet003.Rem_esd = ESD_rt;
            _remSumDet003.Rem_wht = WHT_rt;
            _remSumDet003.Rem_add_fin = 0;
            _remSumDet003.Rem_ded_fin = 0;
            _remSumDet003.Rem_net_fin = 0;
            _remSumDet003.Rem_rmk_fin = "";
            _remSumDet003.Rem_bnk_cd = "";
            _remSumDet003.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
            //8/8/2013
            _remSumDet003.REM_CHQNO = "";
            _remSumDet003.REM_CHQ_BANK_CD = "";
            _remSumDet003.REM_CHQ_BRANCH = "";
            _remSumDet003.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
            _remSumDet003.REM_DEPOSIT_BANK_CD = "";
            _remSumDet003.REM_DEPOSIT_BRANCH = "";

            //save remitance to be banked
            int row_aff4 = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet003);

            //commission withdrawn
            Decimal _commWdr = Convert.ToDecimal(txtColBonus.Text);
            lbl_comm_wdr.Text = _commWdr.ToString("0,0.00", CultureInfo.InvariantCulture);

            //save excess remitance----------------------------------------------------------------------------------------------------
            RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();

            _remSumDet.Rem_com = BaseCls.GlbUserComCode;
            _remSumDet.Rem_pc = BaseCls.GlbUserDefProf;
            _remSumDet.Rem_dt = Convert.ToDateTime(txtDate.Text).Date;
            _remSumDet.Rem_sec = "03";
            _remSumDet.Rem_cd = "011";
            _remSumDet.Rem_sh_desc = "Excess Remitance";
            _remSumDet.Rem_lg_desc = "Excess Remitance";
            _remSumDet.Rem_val = _excessRem;
            _remSumDet.Rem_val_final = _excessRem;
            _remSumDet.Rem_week = (_wkno + "S").ToString();
            _remSumDet.Rem_ref_no = "";
            _remSumDet.Rem_rmk = "";
            _remSumDet.Rem_cr_acc = "";
            _remSumDet.Rem_db_acc = "";
            _remSumDet.Rem_cre_by = BaseCls.GlbUserID;
            _remSumDet.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _remSumDet.Rem_is_sos = false;
            _remSumDet.Rem_is_dayend = true;
            _remSumDet.Rem_is_sun = false;
            _remSumDet.Rem_del_alw = true;
            _remSumDet.Rem_is_rem_sum = false;
            _remSumDet.Rem_cat = 12;
            _remSumDet.Rem_add = 0;
            _remSumDet.Rem_ded = 0;
            _remSumDet.Rem_net = 0;
            _remSumDet.Rem_epf = EPF_rt;
            _remSumDet.Rem_esd = ESD_rt;
            _remSumDet.Rem_wht = WHT_rt;
            _remSumDet.Rem_add_fin = 0;
            _remSumDet.Rem_ded_fin = 0;
            _remSumDet.Rem_net_fin = 0;
            _remSumDet.Rem_rmk_fin = "";
            _remSumDet.Rem_bnk_cd = "";
            _remSumDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
            //8/8/2013
            _remSumDet.REM_CHQNO = "";
            _remSumDet.REM_CHQ_BANK_CD = "";
            _remSumDet.REM_CHQ_BRANCH = "";
            _remSumDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
            _remSumDet.REM_DEPOSIT_BANK_CD = "";
            _remSumDet.REM_DEPOSIT_BRANCH = "";

            //save excess remitance
            int row_aff = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet);


            return _isSuccess;
        }

        private void ddlSecDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRemitTypes(Convert.ToString(ddlSecDef.SelectedValue));
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Day End Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadRemitTypes(string _sec)
        {

            ddlRemTp.DataSource = null;
            var source = new BindingSource();
            source.DataSource = CHNLSVC.Financial.get_rem_type_by_sec(_sec, 0);
            if (source.DataSource != null)
            {
                ddlRemTp.DataSource = source;
                ddlRemTp.DisplayMember = "RSD_DESC";
                ddlRemTp.ValueMember = "RSD_CD";
            }

        }

        private void ddlRemTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "003") { pnlCheque.Visible = true; } else { pnlCheque.Visible = false; }

                if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "001") { pnlBank.Visible = true; } else { pnlBank.Visible = false; }

                if (Convert.ToString(ddlSecDef.SelectedValue) == "02" && Convert.ToString(ddlRemTp.SelectedValue) == "013")
                {
                    //txtGross.Enabled = true;
                    txtAdd.Enabled = true;
                    txtDeduct.Enabled = true;
                    txtNet.Enabled = true;
                    txtVoucher.Enabled = true;
                }
                else
                {
                    //txtGross.Enabled = false;
                    txtAdd.Text = "0";
                    txtDeduct.Text = "0";
                    txtNet.Text = "0";
                    txtGross.Text = "0";
                    txtVoucher.Text = "";
                    txtAdd.Enabled = false;
                    txtDeduct.Enabled = false;
                    txtNet.Enabled = false;
                    txtVoucher.Enabled = false;

                    //check whether this rem type is a once per month
                    Boolean _isOnce = CHNLSVC.Financial.IsOnceRemType(Convert.ToString(ddlSecDef.SelectedValue), Convert.ToString(ddlRemTp.SelectedValue));
                    if (_isOnce == true)
                    {
                        dtp_Month.Visible = true;
                        lblMnth.Visible = true;
                    }
                    else
                    {
                        dtp_Month.Visible = false;
                        lblMnth.Visible = false;
                    }
                }
                if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "002")
                {
                    txtVoucher.Enabled = true;
                }
                if (Convert.ToString(ddlSecDef.SelectedValue) == "02" && Convert.ToString(ddlRemTp.SelectedValue) == "022")
                {
                    txtVoucher.Enabled = true;
                }
                txtVal.Enabled = true;
                txtAdd.Enabled = true;
                txtDeduct.Enabled = true;
                txtNet.Enabled = true;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Day End Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                //kapila 27/8/2014
                Boolean _isDepBanAccMan = false;

                DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel);
                if (_dtDepBank.Rows.Count > 0)
                    if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                        _isDepBanAccMan = true;

                if (ddlSecDef.SelectedValue.Equals("03") && ddlRemTp.SelectedValue.Equals("027"))
                {
                    MessageBox.Show("You cannot enter fine charges !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //kapila 27/11/2013
                if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "001" && string.IsNullOrEmpty(txtBank.Text))
                {
                    if (_isDepBanAccMan == true)
                    {
                        MessageBox.Show("Select the bank code !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtBank.Focus();
                        return;
                    }
                }
                //kapila 27/10/2014
                if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "001" && string.IsNullOrEmpty(txtRem.Text))
                {
                    MessageBox.Show("Enter the remarks !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRem.Focus();
                    return;
                }

                DateTime _invalidDT = Convert.ToDateTime("31/Dec/9999");
                if (CHNLSVC.Financial.IsValidDayEndDate(Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserDefProf, out _invalidDT) == false)
                {
                    if (_invalidDT != Convert.ToDateTime("31/Dec/9999"))
                    {
                        MessageBox.Show("Cannot process." + "\n" + "You have to Day End on " + _invalidDT.ToString("dd/MMM/yyyy"), "Day End", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cannot process.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }
                if (ddlRemTp.SelectedValue.Equals("-1"))
                {
                    MessageBox.Show("Please select Remitance", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (ddlSecDef.SelectedValue.Equals("-1"))
                {
                    MessageBox.Show("Please select Section", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //19/12/2014
                if (Convert.ToString(ddlSecDef.SelectedValue) == "02" && Convert.ToString(ddlRemTp.SelectedValue) == "013")
                {
                    if (string.IsNullOrEmpty(txtVoucher.Text))
                    {
                        MessageBox.Show("Enter Voucher #", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (ddlSecDef.SelectedValue.Equals("02") && ddlRemTp.SelectedValue.Equals("013"))
                {
                    if (string.IsNullOrEmpty(txtGross.Text) || string.IsNullOrEmpty(txtAdd.Text) || string.IsNullOrEmpty(txtDeduct.Text) || string.IsNullOrEmpty(txtNet.Text))
                    {
                        MessageBox.Show("Please Collection bonus amount", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                string _curDate = DateTime.Today.Date.ToString("dd/MMM/yyyy");
                if ((txtDate.Text).ToString() != _curDate)
                {
                    BackDates _bdt = new BackDates();

                    bool _isAllow = CHNLSVC.General.IsBackDateFound(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, this.GlbModuleName);
                    if (_isAllow == false)
                    {

                        MessageBox.Show("You cannot add entry for back date !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }
                }

                //08/08/2013 receive cheques
                if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "003")
                {
                    if (string.IsNullOrEmpty(textBoxChequeNo.Text)) { MessageBox.Show("Please enter the cheque no"); textBoxChequeNo.Focus(); return; }
                    if (string.IsNullOrEmpty(textBoxChqBank.Text)) { MessageBox.Show("Please enter the valid bank"); textBoxChqBank.Focus(); return; }

                    if (textBoxChequeNo.Text.Length != 6)  //3/11/2014
                    {
                        MessageBox.Show("Please enter correct cheque number. [Cheque number should be 6 numbers.]", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        textBoxChequeNo.Focus();
                        return;
                    }

                    if (!CheckBank(textBoxChqBank.Text, lblChqBank))
                    {
                        MessageBox.Show("Invalid Bank Code");
                        textBoxChqBank.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(textBoxChqBranch.Text))
                    {
                        MessageBox.Show("Please enter cheque branch");
                        textBoxChqBranch.Focus();
                        return;
                    }

                    if (_isDepBanAccMan == true)
                    {
                        DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CHEQUE", textBoxChqDepBank.Text);
                        if (BankName.Rows.Count == 0)
                        {
                            MessageBox.Show("Invalid deposit bank account !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            textBoxChqDepBank.Focus();
                            return;
                        }
                    }

                    #region Check retrn CHEQUE date count
                    DateTime Date = Convert.ToDateTime(txtDate.Text).Date;
                    HpSystemParameters _getSystemParameter = new HpSystemParameters();
                    _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "CRDC", Date);
                    if (string.IsNullOrEmpty(_getSystemParameter.Hsy_cd))
                    {
                        _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "CRDC", Date);
                    }
                    if (!string.IsNullOrEmpty(_getSystemParameter.Hsy_cd))
                    {
                        List<ChequeReturn> Getreturn_cheq_cout_data = new List<ChequeReturn>();
                        Getreturn_cheq_cout_data = CHNLSVC.Financial.Getreturn_cheq_cout_data(BaseCls.GlbUserDefProf, Date, BaseCls.GlbUserComCode, Convert.ToInt16(_getSystemParameter.Hsy_val));
                        if (Getreturn_cheq_cout_data != null)
                        {


                            if (Getreturn_cheq_cout_data.Count > 0)
                            {
                                MessageBox.Show("You are not allowed to collect cheque payments. Following return cheques are not settle within " + Convert.ToInt16(_getSystemParameter.Hsy_val) + " Days", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                grdreyrncheq.AutoGenerateColumns = false;
                                grdreyrncheq.DataSource = new List<ChequeReturn>();
                                grdreyrncheq.DataSource = Getreturn_cheq_cout_data;
                                reyrncheq.Visible = true;
                                return;
                            }
                        }

                    }

                    #endregion
                    //if (textBoxChqBranch.Text != "" && !CheckBankBranch(textBoxChqBank.Text, textBoxChqBranch.Text))
                    //{
                    //    MessageBox.Show("Cheque Bank and Branch not match");
                    //    return;
                    //}
                }

                //check whether already entered is_once type rem type
                DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtp_Month.Value.Month + "/" + dtp_Month.Value.Year).Date;
                DateTime _fromDate = Convert.ToDateTime("01" + "/" + txtDate.Value.Date.Month + "/" + txtDate.Value.Date.Year).Date;
                DateTime _dateLimit = Convert.ToDateTime(txtDate.Text).Date;

                if (dtp_Month.Visible == true)
                {
                    _dateLimit = _tmpMonth;
                    _fromDate = _tmpMonth;

                    Boolean _IsExist = CHNLSVC.Financial.IsOnceRemDataExist(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToString(ddlSecDef.SelectedValue), Convert.ToString(ddlRemTp.SelectedValue), _tmpMonth);
                    if (_IsExist == true)
                    {
                        MessageBox.Show("Already exist for this month !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                //check whether rem limitation is available
                Boolean _isRemLimit = false;

                DateTime firstOfNextMonth = new DateTime(_fromDate.Year, _fromDate.Month, 1).AddMonths(1);
                DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);

                Decimal _Limit = 0;

                DataTable _dtLimitAloc = CHNLSVC.General.GetExpenseLimitAloc(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToString(ddlRemTp.SelectedValue), _dateLimit);
                if(_dtLimitAloc.Rows.Count>0)
                {
                    _isRemLimit = true;
                    _Limit = Convert.ToDecimal(_dtLimitAloc.Rows[0]["exla_val"]);
                }

                //check whether limit is exceeded
                if (_isRemLimit == true && _Limit > 0)
                {
                    Decimal _val = 0;
                    Decimal _finVal = 0;
                    int D = CHNLSVC.Financial.GetRemSumDetWOCurDt(BaseCls.GlbUserComCode, _fromDate, lastOfThisMonth, Convert.ToDateTime(txtDate.Text).Date, Convert.ToString(ddlSecDef.SelectedValue), Convert.ToString(ddlRemTp.SelectedValue), BaseCls.GlbUserDefProf, out _val, out _finVal);
                    if (Convert.ToDecimal(txtVal.Text) > _Limit - _val)
                    {
                        MessageBox.Show("Allowed limit is exceeded !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (Convert.ToString(ddlSecDef.SelectedValue) == "02" && Convert.ToString(ddlRemTp.SelectedValue) == "022")
                {
                    if (string.IsNullOrEmpty(txtVoucher.Text))
                    {
                        MessageBox.Show("Enter Voucher #", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "002")
                {
                    if (string.IsNullOrEmpty(txtVoucher.Text))
                    {
                        MessageBox.Show("Enter Voucher #", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        //update gnt_intr_vou_hdr 

                    }
                }

                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();
                Decimal _weekNo = 0;
                _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtDate.Text).Date, out _weekNo, BaseCls.GlbUserComCode);

                DataTable dtESD_EPF_WHT = new DataTable();
                dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);

                Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
                if (dtESD_EPF_WHT.Rows.Count > 0)
                {
                    ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                    EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                    WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

                }

                _remSumDet.Rem_com = BaseCls.GlbUserComCode;
                _remSumDet.Rem_pc = BaseCls.GlbUserDefProf;
                _remSumDet.Rem_dt = Convert.ToDateTime(txtDate.Text).Date;
                _remSumDet.Rem_sec = Convert.ToString(ddlSecDef.SelectedValue);
                _remSumDet.Rem_cd = Convert.ToString(ddlRemTp.SelectedValue);
                if (ddlSecDef.SelectedValue == "02" && ddlRemTp.SelectedValue == "013")  //col bonus
                {
                    _remSumDet.Rem_sh_desc = (ddlRemTp.Text + "-" + txtVoucher.Text).ToString();
                    _remSumDet.Rem_lg_desc = (ddlRemTp.Text + "-" + txtVoucher.Text).ToString().ToUpper();
                    _remSumDet.Rem_val = Convert.ToDecimal(txtNet.Text);
                    _remSumDet.Rem_val_final = Convert.ToDecimal(txtNet.Text);
                }
                else
                {
                    if (dtp_Month.Visible == true)
                    {
                        string s = dtp_Month.Value.ToString("MMM", CultureInfo.InvariantCulture) + '/' + dtp_Month.Value.Year.ToString();
                        _remSumDet.Rem_sh_desc = ddlRemTp.Text + '(' + s + ')';
                    }
                    else
                    {
                        _remSumDet.Rem_sh_desc = ddlRemTp.Text;
                    }
                    _remSumDet.Rem_lg_desc = ddlRemTp.Text.ToUpper();
                    _remSumDet.Rem_val = Convert.ToDecimal(txtVal.Text);
                    _remSumDet.Rem_val_final = Convert.ToDecimal(txtVal.Text);
                }


                _remSumDet.Rem_week = (_weekNo + "S").ToString();
                _remSumDet.Rem_ref_no = txtVoucher.Text;
                if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "003")
                    _remSumDet.Rem_rmk = txtRem.Text + ' ' + textBoxChequeNo.Text;
                else
                    _remSumDet.Rem_rmk = txtRem.Text;
                _remSumDet.Rem_cr_acc = "";
                _remSumDet.Rem_db_acc = "";
                _remSumDet.Rem_del_alw = false;
                _remSumDet.Rem_cre_by = BaseCls.GlbUserID;
                _remSumDet.Rem_cre_dt = DateTime.Now;
                if (ddlSecDef.SelectedValue == "05" && ddlRemTp.SelectedValue == "001")       //slip
                {
                    _remSumDet.Rem_is_sos = false;
                }
                else
                {
                    _remSumDet.Rem_is_sos = true;
                }

                _remSumDet.Rem_is_dayend = true;
                _remSumDet.Rem_is_sun = true;
                _remSumDet.Rem_cat = 17;
                _remSumDet.Rem_add = Convert.ToDecimal(txtAdd.Text);
                _remSumDet.Rem_ded = Convert.ToDecimal(txtDeduct.Text);
                _remSumDet.Rem_net = Convert.ToDecimal(txtNet.Text);
                _remSumDet.Rem_epf = EPF_rt;
                _remSumDet.Rem_esd = ESD_rt;
                _remSumDet.Rem_wht = WHT_rt;
                _remSumDet.Rem_add_fin = Convert.ToDecimal(txtAdd.Text);
                _remSumDet.Rem_ded_fin = Convert.ToDecimal(txtDeduct.Text);
                _remSumDet.Rem_net_fin = Convert.ToDecimal(txtNet.Text);
                _remSumDet.Rem_rmk_fin = txtRem.Text;
                _remSumDet.Rem_bnk_cd = "";
                _remSumDet.Rem_is_rem_sum = true;


                if (dtp_Month.Visible == true)
                {
                    _remSumDet.REM_REM_MONTH = _tmpMonth;
                }
                else
                {
                    _remSumDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");
                }
                if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "003")
                {
                    _remSumDet.REM_CHQNO = textBoxChequeNo.Text;
                    _remSumDet.REM_CHQ_BANK_CD = textBoxChqBank.Text;
                    _remSumDet.REM_CHQ_BRANCH = textBoxChqBranch.Text;
                    _remSumDet.REM_CHQ_DT = Convert.ToDateTime(dateTimePickerExpire.Text).Date;
                    _remSumDet.REM_DEPOSIT_BANK_CD = textBoxChqDepBank.Text;
                    _remSumDet.REM_DEPOSIT_BRANCH = textBoxChqDepBranch.Text;
                }
                else
                {
                    _remSumDet.REM_CHQNO = "";
                    _remSumDet.REM_CHQ_BANK_CD = "";
                    _remSumDet.REM_CHQ_BRANCH = "";
                    _remSumDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                    _remSumDet.REM_DEPOSIT_BANK_CD = txtBank.Text;
                    _remSumDet.REM_DEPOSIT_BRANCH = "";
                }
                _remSumDet.Rem_bnk_cd = lblDepBank.Text;

                int row_aff = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet);
                BindGridView();

                MessageBox.Show("Successfully Updated", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //clear
                ddlSecDef.SelectedValue = "-1";
                ddlRemTp.SelectedValue = "-1";
                txtVal.Text = "";
                txtGross.Text = "0";
                txtDeduct.Text = "0";
                txtAdd.Text = "0";
                txtNet.Text = "0";
                txtRem.Text = "";
                txtVoucher.Text = "";
                txtBank.Text = "";
                lblBank.Text = "";

                txtVal.Enabled = true;
                txtAdd.Enabled = true;
                txtDeduct.Enabled = true;
                txtNet.Enabled = true;
                pnlCheque.Visible = false;
            }

            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Day End Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private bool CheckBankBranch(string bank, string branch)
        {
            if (!string.IsNullOrEmpty(branch))
            {
                bool valid = CHNLSVC.Sales.validateBank_and_Branch(bank, branch, "BANK");
                //MessageBox.Show("Bank and Branch code mismatch");
                return valid;
            }
            else
            {
                return false;
            }
        }

        private bool CheckBank(string bank, Label lbl)
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(bank))
            {
                _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetails(bank, "BANK");

                if (_bankAccounts.Mbi_cd != null)
                {
                    //List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
                    //var _promo = (from _prom in _paymentTypeRef
                    //              where _prom.Stp_pay_tp == comboBoxPayModes.SelectedValue.ToString()
                    //              select _prom).ToList();

                    //foreach (PaymentType _type in _promo)
                    //{
                    //    if (_type.Stp_pd != null && _type.Stp_pd > 0 && _type.Stp_bank == textBoxCCBank.Text && _type.Stp_from_dt.Date <= DateTime.Now.Date && _type.Stp_to_dt.Date >= DateTime.Now.Date)
                    //    {
                    //        panelPermotion.Visible = true;
                    //        chkIsPromo.Checked = false;

                    //    }

                    //}
                    lbl.Text = _bankAccounts.Mbi_desc;
                    return true;
                }
                else
                {
                    MessageBox.Show("Please select the valid bank.");
                    return false;
                }
            }
            return false;

        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {

            loadInit();
            gvRemLimit.DataSource = null;
            //BindGridView();
        }

        protected void CalBonusNet(object sender, EventArgs e)
        {
            txtNet.Text = (Convert.ToDecimal(txtVal.Text) + Convert.ToDecimal(txtAdd.Text) - Convert.ToDecimal(txtDeduct.Text)).ToString();
        }

        protected void LoadGrossBonus(object sender, EventArgs e)
        {
            try
            {
                if (ddlSecDef.SelectedValue.Equals("02") && ddlRemTp.SelectedValue.Equals("013"))
                {
                    txtGross.Text = txtVal.Text;
                    CalBonusNet(null, null);
                }
                else
                {
                    txtGross.Text = (0).ToString();
                    txtGross.Text = "0";
                    txtDeduct.Text = "0";
                    txtAdd.Text = "0";
                    txtNet.Text = "0";
                }
            }
            catch
            {

            }

        }

        private void txtAdd_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(ddlSecDef.SelectedValue) == "02" && Convert.ToString(ddlRemTp.SelectedValue) == "013")
            {
                if (!string.IsNullOrEmpty(txtAdd.Text))
                    CalBonusNet(null, null);
            }
        }

        private void txtDeduct_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(ddlSecDef.SelectedValue) == "02" && Convert.ToString(ddlRemTp.SelectedValue) == "013")
            {
                if (!string.IsNullOrEmpty(txtDeduct.Text))
                    CalBonusNet(null, null);
            }
        }

        private void txtVal_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVal.Text))
                LoadGrossBonus(null, null);
        }

        private void btn_View_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtCIH.Text))
                {
                    MessageBox.Show("Please enter Cash in Hand amount", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCIH.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtColBonus.Text))  //25/7/2016
                {
                    MessageBox.Show("Please enter commission withdrawn amount", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtColBonus.Focus();
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;

                RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();

                Decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtDate.Text).Date, out _wkNo, BaseCls.GlbUserComCode);

                if (_wkNo == 0)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Week Definition not found. Contact Accounts dept.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DateTime _st = DateTime.Now;
                int _processSeq = CHNLSVC.CommonSearch.StartTimeModule("DAY END - VIEW", "", DateTime.Now, BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, BaseCls.GlbUserID, txtDate.Value.Date);

                //process commission
                string _outErr = "";
                int _effComm = CHNLSVC.Sales.Process_DayEnd_Commission(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, out _outErr);
                if (_effComm == 0)  //2/7/2014
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Process halted." + "\n" + "Reason: commission definition not found for " + _outErr, "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //kapila 3/9/2016 - update item taxt
                // int _effitmtax = CHNLSVC.Sales.Update_Item_Tax(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);
                #region check bill payement add by tharanga 2018/09/05
                MasterProfitCenter _mstPcN = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                if (_mstPcN.Mpc_ce_stus == 1)
                {
                    List<Ref_Bill_Collet> Ref_Bill_ColletLIST = new List<Ref_Bill_Collet>();
                    Ref_Bill_ColletLIST = CHNLSVC.Financial.GetRef_Bill_Collet_dayend(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);
                    if (Ref_Bill_ColletLIST.Count <= 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Bill record not found", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                      

                        foreach (Ref_Bill_Collet _list in Ref_Bill_ColletLIST)
                        {
                            if (_list.RBC_CONF_STUS == 0)
                            {
                                if (CHNLSVC.Financial.IsDayEndDone_win(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date) == true)
                                {
                                    MessageBox.Show("New bill collection record is found. You cannot add it to dayend process, because dayend is already finalized.", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                Decimal tot = _list.RBC_1HDOV + _list.RBC_2HDOV + _list.RBC_3HDOV;
                                string _amount = amount(tot);
                                if (MessageBox.Show("Bill collection record is found. Amout is " + _amount + ". Do you need to add ? ", "Day End", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Int32 eff = CHNLSVC.Financial.ref_bill_collect_conform(_list.RBC_COM, _list.RBC_COL_LOC, _list.RBC_DT, BaseCls.GlbUserID, 1, _list.RBC_SEQ);

                                }
                                else
                                {
                                    Int32 eff = CHNLSVC.Financial.ref_bill_collect_conform(_list.RBC_COM, _list.RBC_COL_LOC, _list.RBC_DT, BaseCls.GlbUserID, 2, _list.RBC_SEQ);

                                }
                            }
                        }
                    }


                }
                #endregion
                //kapila 28/6/2016 introducer commission
                DataTable _dtIntrComm = null;
                if (BaseCls.GlbUserComCode == "AAL")
                    _dtIntrComm = CHNLSVC.Financial.processIntroduComm(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserID);


                //kapila 30/11/2015
                if (!CHNLSVC.Financial.IsDayEndDone_win(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date) == true)
                {
                    //process other location sale commission   13/8/2013    
                    Int32 R = CHNLSVC.Financial.Process_OtherLocation_Selling_Comm(Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, Convert.ToInt32(_wkNo), BaseCls.GlbUserID);

                    //kapila 16/11/2013 group sale comm
                    Int32 T = CHNLSVC.Financial.Process_group_sale_Selling_Comm(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);

                    //kapila 30/11/2015 additional product comm
                    Int32 P = CHNLSVC.Financial.CalcAddiProdComm(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);
                }
                DateTime _fromDate;
                int D = CHNLSVC.Financial.GetFirstDayEndDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Value).Date, Convert.ToDateTime(txtDate.Value).Date, out _fromDate);

                Decimal _RemBanked = 0;

                //cash in hand
                Decimal _CIH = Convert.ToDecimal(txtCIH.Text);
                lbl_CIH.Text = _CIH.ToString("0,0.00", CultureInfo.InvariantCulture);

                //commission withdrawn
                Decimal _commWdr = Convert.ToDecimal(txtColBonus.Text);
                lbl_comm_wdr.Text = _commWdr.ToString("0,0.00", CultureInfo.InvariantCulture);

                Decimal _totRemManual = 0;
                Decimal _totRemManualFinal = 0;
                DataTable _tblRem = CHNLSVC.Financial.GetRemSummaryReport_Win(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(_fromDate).Date, Convert.ToDateTime(txtDate.Value).Date, "05", out _totRemManual, out _totRemManualFinal);

                //total remitance
                Decimal _tmptotRem = 0;
                Decimal _totRem = 0;
                int Y = CHNLSVC.Financial.GetTotRemitance(BaseCls.GlbUserComCode, Convert.ToDateTime(_fromDate).Date, Convert.ToDateTime(txtDate.Value).Date, BaseCls.GlbUserDefProf, out _tmptotRem);
                _totRem = _tmptotRem + _totRemManual;

                Decimal previousexessval = 0;

                //Decimal _excessRem = 0;
                BindReceiptGridData_View(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);
                BindDisbursementGridData_view(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);
                BindSummaryGridData_view(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);
                BindLessGridData_view(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);

                //remitance to be banked
                _RemBanked = Convert.ToDecimal(txtSumTot.Text) - Convert.ToDecimal(txtLessTot.Text);
                lbl_banked.Text = (_RemBanked).ToString("0,0.00", CultureInfo.InvariantCulture);

                //total remitance
                lbl_TotRem.Text = (_totRem).ToString("0,0.00", CultureInfo.InvariantCulture);

                //difference
                lbl_diff.Text = (Convert.ToDecimal(txtSumTot.Text) - Convert.ToDecimal(txtLessTot.Text) - Convert.ToDecimal(txtCIH.Text) - _totRem).ToString("0,0.00", CultureInfo.InvariantCulture);

                //Tharindu 2018-04-08 remittance auto process
                decimal remval = Convert.ToDecimal(txtLessTot.Text);

                if (remval > 0)
                {
                    string _msg = string.Empty;
                    decimal mincashval = (remval / 100) * (40);
                    DateTime credt = DateTime.Now;
                    DataTable dt = new DataTable();
                    List<FineCharges> _curfinechg = new List<FineCharges>();
                    int i = 0;
                    decimal chargesum = 0;
                    decimal balval = 0;


                    dt = CHNLSVC.Financial.getcurrfinecharges(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date);

                    _curfinechg = (from DataRow row in dt.Rows

                           select new FineCharges
                           {
                               seqno = Convert.ToInt32(row["gfc_seq_no"].ToString()),
                               finecode = row["gfc_fine_code"].ToString(),
                               amount = Convert.ToDecimal(row["gfc_fine_amt"].ToString()),
                               balance = Convert.ToDecimal(row["gfc_fine_bal"].ToString()),
                               issetoff = i,
                               description = row["gfc_fine_des"].ToString(),
                               finedate = Convert.ToDateTime(row["gfc_fine_dt"].ToString())

                           }).ToList();


                    if(_curfinechg != null)
                    {
                      chargesum = _curfinechg.Sum(x => x.amount);
                    }


                    foreach (var item in _curfinechg)
                    {
                        if (chargesum <= 0 || mincashval <= 0)
                        {
                            return;
                        }
                        else
                        {
                            chargesum = chargesum - item.amount;
                            mincashval = mincashval - item.amount;

                            if(mincashval < 0)
                            {
                                balval = item.amount + mincashval;
                                item.amount = balval;
                            }

                              //insert new record
                            RemitanceSummaryDetail _remDet = new RemitanceSummaryDetail();
                            _remDet.Rem_dt = Convert.ToDateTime(item.finedate).Date;
                            decimal week = 0;
                            CHNLSVC.General.GetWeek(Convert.ToDateTime(item.finedate).Date, out week, BaseCls.GlbUserComCode);
                            _remDet.Rem_week = week.ToString();
                            _remDet.Rem_com = BaseCls.GlbUserComCode;
                            _remDet.Rem_pc = BaseCls.GlbUserDefProf;
                            _remDet.Rem_cre_by = BaseCls.GlbUserID;
                            _remDet.Rem_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                            _remDet.Rem_rmk = "";
                            _remDet.Rem_rmk_fin ="";
                            _remDet.Rem_val = item.amount;
                            _remDet.Rem_val_final =  item.amount;
                            //TODO: Need section and type codes
                            _remDet.Rem_sec = "03";
                            _remDet.Rem_cd = "027";
                            _remDet.Rem_sh_desc = item.description; //DropDownListRemitType.SelectedText.ToString();//DropDownListRemitType.SelectedItem.Text;
                            _remDet.Rem_lg_desc =item.description;//DropDownListRemitType.SelectedText.ToString();//DropDownListRemitType.SelectedItem.Text;
                            _remDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");    //kapila 14/3/2013

                            //kapila 28/5/2013
                            _remDet.Rem_is_dayend = true;
                            _remDet.Rem_is_rem_sum = true;
                            _remDet.Rem_is_sos = true;
                            _remDet.Rem_is_sun = true;

                            //8/8/2013
                            _remDet.REM_CHQNO = "";
                            _remDet.REM_CHQ_BANK_CD = "";
                            _remDet.REM_CHQ_BRANCH = "";
                            _remDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
                            _remDet.REM_DEPOSIT_BANK_CD = "";
                            _remDet.REM_DEPOSIT_BRANCH = "";


                            int effect1 = CHNLSVC.Financial.SaveRemSummaryDetails(_remDet);
                            int effect = CHNLSVC.Financial.SaveFineSetOff(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(credt).Date, 0, BaseCls.GlbUserID, Convert.ToDateTime(credt).Date, item.amount,item.seqno, out _msg);
                        }
                    }

                   

                    //if(Convert.ToDecimal(txtCIH.Text) > mincashval)
                    //{
                    //    int effect = CHNLSVC.Financial.SaveFineSetOff(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(credt).Date, 0, BaseCls.GlbUserID, Convert.ToDateTime(credt).Date, mincashval, out _msg);
                    //}
                }

               // BindSummaryGridData_view(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);

                BindGridView();

                DateTime _ed = DateTime.Now;
                TimeSpan _diff = new TimeSpan();
                _diff = _ed - _st;

                Cursor.Current = Cursors.Default;
                SendMail(_processSeq, BaseCls.GlbUserDefProf + " - Day End - View", _st, _ed, _diff);
                MessageBox.Show("View Completed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                #region Remove Lines 2 - Chamal 09-07-2013
                //BaseCls.GlbCommWithdrawn = _CommWthdr;
                //BaseCls.GlbCommWithdrawnFinal = _CommWthdr_Final;

                //BaseCls.GlbReportFromDate = Convert.ToDateTime(_fromDate).Date;
                //BaseCls.GlbReportToDate = Convert.ToDateTime(txtDate.Value).Date;

                //BaseCls.GlbReportProfit = BaseCls.GlbUserDefProf;
                //GetCompanyDet(out _repComp,out  _repComp_Addr);
                //BaseCls.GlbReportComp = _repComp;
                //BaseCls.GlbReportCompAddr = _repComp_Addr;

                //ReportViewer _view = new ReportViewer();
                //if (CHNLSVC.Financial.IsDayEndDone(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date , Convert.ToDateTime(txtDate.Text).Date) == true)
                //{
                //    //day end is done
                //    _view.GlbReportName = "Remitance_Sum.rpt";
                //    BaseCls.GlbReportName = "Remitance_Sum.rpt";
                //}
                //else
                //{
                //    _view.GlbReportName = "Remitance_Sum_view.rpt";
                //    BaseCls.GlbReportName = "Remitance_Sum_view.rpt";
                //}
                //_view.Show();
                //_view = null;
                #endregion
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Day End Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void GetCompanyDet(out string _comp, out string _addr)
        {

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp != null)
            {
                _comp = _masterComp.Mc_desc;
                _addr = _masterComp.Mc_add1 + _masterComp.Mc_add2;
            }
            else
            {
                _comp = "";
                _addr = "";
            }
        }

        private void gvRemLimit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string _curDate = DateTime.Today.Date.ToString("dd/MMM/yyyy");
            if ((txtDate.Text).ToString() != _curDate)
            {
                BackDates _bdt = new BackDates();

                bool _isAllow = CHNLSVC.General.IsBackDateFound(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, this.GlbModuleName);
                if (_isAllow == false)
                {

                    MessageBox.Show("You cannot add entry for back date !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
            }

            if (gvRemLimit.RowCount > 0)
            {
                int _rowIndex = e.RowIndex;
                int _colIndex = e.ColumnIndex;

                if (_rowIndex != -1)
                {


                    if (gvRemLimit.Columns[_colIndex].Name == "rem_remove")
                    {
                        if (MessageBox.Show("Are you sure ?", "Day End", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                        OnRemoveFromItemGrid(_rowIndex, _colIndex);


                        return;
                    }
                }
            }
        }

        protected void OnRemoveFromItemGrid(int _rowIndex, int _colIndex)
        {
            try
            {
                int row_id = _rowIndex;

                string _sec = Convert.ToString(gvRemLimit.Rows[row_id].Cells["rem_sec"].Value);
                string _code = Convert.ToString(gvRemLimit.Rows[row_id].Cells["rem_cd"].Value);
                DateTime _date = Convert.ToDateTime(gvRemLimit.Rows[row_id].Cells["rem_dt"].Value);
                string _refNo = Convert.ToString(gvRemLimit.Rows[row_id].Cells["rem_ref_no"].Value);

                //group sale comm cannot be removed.this is calculated automatically
                if (_sec == "02" && _code == "005")
                {
                    MessageBox.Show("You cannot remove group sale commission !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (_sec == "03" && _code == "027")
                {
                    MessageBox.Show("You cannot remove fine charges !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //remove from the table
                Int32 X = CHNLSVC.Financial.DeleteRemSum(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _date, _sec, _code, _refNo);
                BindGridView();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSecDef.SelectedValue = "-1";
                ddlRemTp.SelectedValue = "-1";
                txtVal.Text = "";
                txtGross.Text = "0";
                txtDeduct.Text = "0";
                txtAdd.Text = "0";
                txtNet.Text = "0";
                txtRem.Text = "";
                txtVoucher.Text = "";
                gvRemLimit.DataSource = null;
                gvDisb.DataSource = null;
                gvLess.DataSource = null;
                gvRec.DataSource = null;
                gvSum.DataSource = null;
                txtDisbTot.Text = "0.00";
                txtRecTot.Text = "0.00";
                txtLessTot.Text = "0.00";
                txtSumTot.Text = "0.00";

                lbl_banked.Text = "0.00";
                lbl_CIH.Text = "0.00";
                lbl_comm_wdr.Text = "0.00";
                lbl_diff.Text = "0.00";
                lbl_TotRem.Text = "0.00";

                txtCIH.Text = "0";
                txtColBonus.Text = "0";
                txtBank.Text = "";
                lblBank.Text = "";
                txtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Day End Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
                GC.Collect();
            }
        }

        private void DayEndProcess_Load(object sender, EventArgs e)
        {
            BackDatePermission();
        }

        private void ddlSecDef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ddlRemTp.Focus();
        }

        private void ddlRemTp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtVal.Focus();
        }

        private void txtVal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtVoucher.Focus();
        }

        private void txtVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRem.Focus();
        }

        private void txtRem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtGross.Focus();
        }

        private void txtGross_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAdd.Focus();
        }

        private void txtAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDeduct.Focus();
        }

        private void txtDeduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAdd.Focus();
        }

        private void txtCIH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtColBonus.Focus();
        }

        private void buttonChqBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 2;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxChqBank;
                _CommonSearch.ShowDialog();
                textBoxChqBank.Select();
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.AdvancedReciept:
                    {

                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBank:
                    {
                        if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "001")
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "BANK_SLIP" + seperator);
                        else
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "CHEQUE" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {

                        paramsText.Append(textBoxChqBank.Text.Trim() + seperator);


                        //paramsText.Append(textBoxCCDepBank.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBankBranch:
                    {


                        paramsText.Append(textBoxChqDepBank.Text.Trim() + seperator);



                        //paramsText.Append(textBoxCCDepBank.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GiftVoucher:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + 0 + seperator);
                        break;
                    }

                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void buttonChqBranchSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxChqBranch;
                _CommonSearch.ShowDialog();
                textBoxChqBranch.Select();
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

        private void buttonChqDepBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 3;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable _result = CHNLSVC.CommonSearch.searchDepositBankCode(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxChqDepBank;
                _CommonSearch.ShowDialog();
                textBoxChqDepBank.Select();

                getBank();

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

        private void txtVoucher_Leave(object sender, EventArgs e)
        {
            if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "002")
            {
                if (!string.IsNullOrEmpty(txtVoucher.Text))
                {
                    VoucherHeader _voucher = CHNLSVC.Financial.GetValidVoucher(Convert.ToDateTime(txtDate.Value).Date, BaseCls.GlbUserComCode, txtVoucher.Text);
                    if (_voucher == null)
                    {
                        MessageBox.Show("This is not a Valid Voucher Number", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtVoucher.Text = "";
                        txtVoucher.Focus();
                        return;
                    }
                    else
                    {
                        txtVal.Text = (_voucher.Givh_val).ToString();
                        txtRem.Text = _voucher.Givd_emp_name;
                        txtVal.Enabled = false;
                    }
                }
            }
            if (Convert.ToString(ddlSecDef.SelectedValue) == "02" && Convert.ToString(ddlRemTp.SelectedValue) == "022")
            {
                DataTable _Dtvou = CHNLSVC.Financial.GetValidPBVoucher(Convert.ToDateTime(txtDate.Value).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVoucher.Text.ToString());
                if (_Dtvou.Rows.Count > 0)
                {
                    txtVal.Text = _Dtvou.Rows[0]["pbih_net"].ToString();
                    txtVal.Enabled = false;
                }
                else
                {
                    txtVal.Enabled = true;
                    if (MessageBox.Show("This is not a Valid Voucher Number." + "\n" + "Are You Sure?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        txtVoucher.Focus();
                    }
                }
            }
            //kapila 5/9/2014 collection bonus
            if (Convert.ToString(ddlSecDef.SelectedValue) == "02" && Convert.ToString(ddlRemTp.SelectedValue) == "013")
            {
                if (!string.IsNullOrEmpty(txtVoucher.Text))
                {
                    DataTable _colBVou = CHNLSVC.Financial.GetValidColBonusVoucher(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtVoucher.Text.ToString());
                    if (_colBVou.Rows.Count > 0)
                    {
                        txtVal.Text = _colBVou.Rows[0]["hpbv_gross_bonus"].ToString();
                        txtGross.Text = _colBVou.Rows[0]["hpbv_gross_bonus"].ToString();
                        txtAdd.Text = _colBVou.Rows[0]["hpbv_refund"].ToString();
                        txtDeduct.Text = _colBVou.Rows[0]["hpbv_deduct"].ToString();
                        txtNet.Text = _colBVou.Rows[0]["hpbv_net_bonus"].ToString();

                        txtVal.Enabled = false;
                        txtAdd.Enabled = false;
                        txtDeduct.Enabled = false;
                        txtNet.Enabled = false;

                    }
                    else
                    {
                        txtVal.Enabled = true;
                        txtAdd.Enabled = true;
                        txtDeduct.Enabled = true;
                        txtNet.Enabled = true;

                        //if (MessageBox.Show("This is not a Valid Voucher Number." + "\n" + "Are You Sure?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        //{
                        //    txtVoucher.Focus();
                        //}
                        //19/12/2014
                        MessageBox.Show("This is not a Valid Voucher Number", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtVoucher.Focus();
                        return;
                    }
                }
            }
        }

        private void btnBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 3;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable _result = CHNLSVC.CommonSearch.searchDepositBankCode(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBank;
                _CommonSearch.ShowDialog();
                txtBank.Select();

                getBank();

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

        private void getBank()
        {
            lblBank.Text = "";
            lblDepBank.Text = "";
            DataTable BankName = null;

            if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "001")
                BankName = CHNLSVC.Sales.get_Dep_Bank_Name(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "BANK_SLIP", txtBank.Text);
            if (Convert.ToString(ddlSecDef.SelectedValue) == "05" && Convert.ToString(ddlRemTp.SelectedValue) == "003")
                BankName = CHNLSVC.Sales.get_Dep_Bank_Name(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CHEQUE", textBoxChqDepBank.Text);

            if (BankName.Rows.Count == 0)
            {
                MessageBox.Show("Invalid deposit bank account !", "Day End", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBank.Focus();
                return;
            }
            else
            {
                foreach (DataRow row2 in BankName.Rows)
                {
                    lblBank.Text = row2["MPB_SUN_DESC"].ToString();
                    lblDepBank.Text = row2["mpb_bank_cd"].ToString();
                }
            }

        }

        private void txtBank_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBank.Text))
                getBank();
        }

        private bool CheckBankAcc(string code)
        {
            MasterBankAccount account = CHNLSVC.Sales.GetBankDetails(BaseCls.GlbUserComCode, null, code);
            if (account == null || account.Msba_com == null || account.Msba_com == "")
            {
                return false;
            }
            else
                return true;
        }

        private void txtBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnBankSearch_Click(null, null);
            }
        }

        private void txtBank_DoubleClick(object sender, EventArgs e)
        {
            btnBankSearch_Click(null, null);
        }

        private void txtCIH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtColBonus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtCIH_TextChanged(object sender, EventArgs e)
        {

        }
        private string amount(decimal _amout)
        {
            string _tempamout = String.Format("{0:C}", _amout);
            return _tempamout.Replace("$", "Rs.");
        }

        private void btnCloseretrnceq_Click(object sender, EventArgs e)
        {
            reyrncheq.Visible = false;
        }
    }
}




