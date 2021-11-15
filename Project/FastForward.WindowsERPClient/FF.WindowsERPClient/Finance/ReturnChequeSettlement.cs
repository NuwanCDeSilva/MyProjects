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

namespace FF.WindowsERPClient.Finance
{
    //sp_get_returncheque_details
    //Cancel_retunChequeSettlement =NEW
    //sp_get_rtn_chq_DET =NEW
    public partial class ReturnChequeSettlement :Base
    {
        #region properties
        public string mouduleName = "m_Trans_Finance_ReturnChequeSettlement";

        private Decimal BalanceToFullSettle=0;
        List<ReturnChequeCalInterest> _newAddList = new List<ReturnChequeCalInterest>();

        private string chequeNo;
        public string ChequeNo
        {
            get { return chequeNo; }
            set { chequeNo = value; }
        }       
      
        private string ref_No;
        public string Ref_No
        {
            get { return ref_No; }
            set { ref_No = value; }
        }        
      
        private string bank;
        public string Bank
        {
            get { return bank; }
            set { bank = value; }
        }
     
        private List<RecieptItem> recieptItem;
        public List<RecieptItem> _recieptItem
        {
            get { return recieptItem; }
            set { recieptItem = value; }
        }       
        //---------------------------------------------------------------------
        #endregion

        private void ReturnChequeSettlement_Load(object sender, EventArgs e)
        {

            //added 20-03-2013-----------------
            string mouduleName = this.GlbModuleName;
            try
            {
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, mouduleName, txtCurrentDt, lblBackDate, string.Empty, out _allowCurrentTrans);            
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
          
            //---------------------------------            
        }
        public ReturnChequeSettlement()
        {
            try
            {
                InitializeComponent();
                BalanceToFullSettle = 0;
                txtCurrentDt.Value = CHNLSVC.Security.GetServerDateTime().Date;
                DataTable datasource = CHNLSVC.Financial.GetReturnChequeDetails();
                GridViewCheques.AutoGenerateColumns = false;
                //datasource.AsEnumerable().Where(a => Convert.ToString(a["SRCQ_COM"]) == BaseCls.GlbUserComCode && Convert.ToString(a["srcq_pc"]) == BaseCls.GlbUserDefProf).CopyToDataTable();
                if (datasource != null)
                {
                    if (datasource.Rows.Count > 0)
                    {
                        var query = from p in datasource.AsEnumerable()
                                    where p.Field<string>("SRCQ_COM").Trim() == BaseCls.GlbUserComCode && p.Field<string>("srcq_pc").Trim() == BaseCls.GlbUserDefProf
                                    select p;
                        if (query.Count<DataRow>() > 0)
                        {
                            //Creating a table from the query                        
                            // GridViewCheques.DataSource = query.CopyToDataTable<DataRow>();  ******
                            //-----------------------------------------------
                            DataTable DATA_SOURSE = query.CopyToDataTable<DataRow>();//(DataTable)(GridViewCheques.DataSource);
                            DATA_SOURSE.Columns.Add("RETUN_BNK");
                            DATA_SOURSE.Columns.Add("CHK_BNK");
                            DataTable dt = CHNLSVC.Financial.GetBanks();
                            foreach (DataRow row in DATA_SOURSE.Rows)
                            {
                                string chqBank = row["SRCQ_BANK"].ToString();
                                string chqRetBank = row["SRCQ_RTN_BANK"].ToString();
                                DateTime chqRetDate = Convert.ToDateTime(row["srcq_rtn_dt"]).Date;

                                MasterBankAccount _tmpBankAcc = new MasterBankAccount();
                                _tmpBankAcc = CHNLSVC.Sales.GetBankDetails(BaseCls.GlbUserComCode, null, chqRetBank);

                                string retBnk = "";
                                if (_tmpBankAcc != null)
                                {
                                    retBnk = _tmpBankAcc.Msba_acc_desc;
                                }
                                else
                                {
                                    retBnk = "";
                                }

                                //string retBnk = (from r in dt.AsEnumerable()
                                //                 where (r.Field<string>("mbi_cd")) == chqRetBank
                                //                 select r.Field<string>("mbi_desc")).First<string>();
                                row.SetField("RETUN_BNK", retBnk);
                                //-----------
                                string chqBnk_ = (from r in dt.AsEnumerable()
                                                  where (r.Field<string>("mbi_cd")) == chqBank
                                                  select r.Field<string>("mbi_desc")).First<string>();
                                row.SetField("CHK_BNK", chqBnk_);

                            }



                            GridViewCheques.DataSource = DATA_SOURSE;

                        }
                    }
                }

                ChequeNo = "";
                ucPayModes1.Date = Convert.ToDateTime(txtCurrentDt.Value).Date;
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
        

        private bool IsBackDateOk()
        {
            try
            {
                bool _isOK = true;
                string selectDate = txtCurrentDt.Value.Date.ToShortDateString();//Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToShortDateString();
                //if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, mouduleName, txtCurrentDt, lblBackDate, selectDate) == false)
                //{
                //    if (txtCurrentDt.Value.Date != DateTime.Now.Date)
                //    {
                //        txtCurrentDt.Enabled = true;
                //        MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtCurrentDt.Focus();
                //        _isOK = false;
                //        return _isOK;
                //    }
                //}


                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, mouduleName, txtCurrentDt, lblBackDate, selectDate, out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (txtCurrentDt.Value.Date != DateTime.Now.Date)
                        {
                            txtCurrentDt.Enabled = true;
                            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCurrentDt.Focus();
                            _isOK = false;
                            return _isOK;
                        }
                    }
                    else
                    {
                        txtCurrentDt.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCurrentDt.Focus();
                        _isOK = false;
                        return _isOK;
                    }
                }

                return _isOK;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
                return false;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }     
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ReturnChequeSettlement formnew = new ReturnChequeSettlement();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
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

        private void GridViewCheques_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    Int32 rowIndex = e.RowIndex;
                    _newAddList = new List<ReturnChequeCalInterest>();
                    DataGridViewRow grv = GridViewCheques.Rows[rowIndex];
                    string chequeNo = grv.Cells["SRCQ_CHQ"].Value.ToString();

                    // decimal amount = Convert.ToDecimal(GridViewCheques.Rows[GridViewCheques.SelectedIndex].Cells[2].Text);
                    decimal amount = Convert.ToDecimal(grv.Cells["SRCQ_ACT_VAL"].Value);//SRCQ_ACT_VAL                
                    // decimal settleAmoun = Convert.ToDecimal(GridViewCheques.Rows[GridViewCheques.SelectedIndex].Cells[4].Text);
                    decimal settleAmoun = Convert.ToDecimal(grv.Cells["SRCQ_SET_VAL"].Value);//SRCQ_SET_VAL
                    // string pc = GridViewCheques.DataKeys[GridViewCheques.SelectedIndex].Values[0].ToString();
                    string pc = grv.Cells["srcq_pc"].Value.ToString();//srcq_pc
                    //ChequeNo = GridViewCheques.Rows[GridViewCheques.SelectedIndex].Cells[0].Text;
                    ChequeNo = grv.Cells["SRCQ_CHQ"].Value.ToString();//SRCQ_CHQ                
                    //Ref_No = GridViewCheques.DataKeys[GridViewCheques.SelectedIndex].Values[1].ToString();
                    Ref_No = grv.Cells["SRCQ_REF"].Value.ToString();
                    //Bank = GridViewCheques.Rows[GridViewCheques.SelectedIndex].Cells[1].Text;
                    Bank = grv.Cells["SRCQ_BANK"].Value.ToString();

                    string _rtnBank = grv.Cells["SRCQ_RTN_BANK"].Value.ToString();

                    //settlement for capital
                    decimal capitalSettleAmount = Convert.ToDecimal(grv.Cells["srcq_cap_set"].Value);//srcq_cap_set
                    decimal intrestSettleAmount = Convert.ToDecimal(grv.Cells["srcq_intr_set"].Value);//srcq_intr_set
                    decimal intrestAmount = Convert.ToDecimal(grv.Cells["srcq_intr"].Value);//srcq_intr
                    decimal newIntrestAmount = 0;
                    DateTime _chqRtnDt = Convert.ToDateTime(grv.Cells["srcq_rtn_dt"].Value);


                    //lblAmtBalCapital.Text = string.Format("{0:n2}", (amount - capitalSettleAmount));
                    //TextBoxSAmo.Text = string.Format("{0:n2}", ((amount + intrestAmount) - settleAmoun));
                    //BalanceToFullSettle = ((amount + intrestAmount) - settleAmoun);
                    //txtSelectChqNo.Text = ChequeNo;

                    lblAmtBalCapital.Text = string.Format("{0:n2}", (amount - capitalSettleAmount));
                    //check and calculate intrest amount
                    string _type = "";
                    string _value = "";
                    decimal _intVal = 0;
                    Int32 _dueDays = Convert.ToInt32((Convert.ToDateTime(txtCurrentDt.Value).Date - Convert.ToDateTime(_chqRtnDt).Date).TotalDays);

                    List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                    if (_hir2.Count > 0)
                    {
                        foreach (MasterSalesPriorityHierarchy _two in _hir2)
                        {
                            _type = _two.Mpi_cd;
                            _value = _two.Mpi_val;

                            List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceChargesNew(_type, _value, "RTNCHQCUS", Convert.ToDateTime(txtCurrentDt.Value).Date);
                            if (_ser != null)
                            {
                                foreach (HpServiceCharges _ser1 in _ser)
                                {
                                    if (_ser1.Hps_from_val <= _dueDays)
                                    {

                                        //check whether already cal interst for this
                                        List<ReturnChequeCalInterest> _tmpCalInt = new List<ReturnChequeCalInterest>();
                                        _tmpCalInt = CHNLSVC.Financial.get_calrtn_int(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, chequeNo, Bank, Convert.ToInt32(_ser1.Hps_from_val), Convert.ToInt32(_ser1.Hps_to_val), Convert.ToDateTime(txtCurrentDt.Value).Date  );

                                        if (_tmpCalInt == null)
                                        {
                                            _intVal = _intVal + ((Convert.ToDecimal(lblAmtBalCapital.Text) * _ser1.Hps_rt / 100) + _ser1.Hps_chg);
                                            decimal _slabIntVal = ((Convert.ToDecimal(lblAmtBalCapital.Text) * _ser1.Hps_rt / 100) + _ser1.Hps_chg);
                                            ReturnChequeCalInterest _tmpCal = new ReturnChequeCalInterest();
                                            _tmpCal.Src_active = true;
                                            _tmpCal.Src_cal_cap = Convert.ToDecimal(lblAmtBalCapital.Text);
                                            _tmpCal.Src_cal_from = Convert.ToInt32(_ser1.Hps_from_val);
                                            _tmpCal.Src_cal_to = Convert.ToInt32(_ser1.Hps_to_val);
                                            _tmpCal.Src_chq_bank = Bank;
                                            _tmpCal.Src_com = BaseCls.GlbUserComCode;
                                            _tmpCal.Src_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
                                            _tmpCal.Src_due_cal_dt = Convert.ToDateTime(txtCurrentDt.Value).Date;
                                            _tmpCal.Src_due_days = _dueDays;
                                            _tmpCal.Src_int = _slabIntVal;
                                            _tmpCal.Src_pc = BaseCls.GlbUserDefProf;
                                            _tmpCal.Src_ref_no = chequeNo;
                                            _tmpCal.Src_rtn_bank = _rtnBank;
                                            _tmpCal.Src_usr = BaseCls.GlbUserID;
                                            _newAddList.Add(_tmpCal);

                                        }
                                        //_mgrChg = Math.Round(((Convert.ToDecimal(TextBoxReturnAmount.Text) * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                        
                                        //goto L3;
                                    }
                                }
                            }
                        }
                    }

                    intrestAmount = (intrestAmount + _intVal);
                    newIntrestAmount = (intrestAmount) - intrestSettleAmount;

                    lblTotNewInt.Text = string.Format("{0:n2}", (newIntrestAmount));
                    TextBoxSAmo.Text = string.Format("{0:n2}", ((amount + intrestAmount) - settleAmoun));
                    BalanceToFullSettle = ((amount + intrestAmount) - settleAmoun);
                    txtSelectChqNo.Text = ChequeNo;

                    CalBalanceAmount();

                    ucPayModes1.ClearControls();
                    ucPayModes1.InvoiceType = "RCHQS";
                    ucPayModes1.TotalAmount = Math.Round(Convert.ToDecimal(TextBoxSAmo.Text), 2);   //((amount + intrestAmount) - settleAmoun);
                    //ucPayModes1.Amount.Text = string.Format("{0:n2}", (amount - settleAmoun));   
                    ucPayModes1.LoadData();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // bool IsBD_Ok= IsBackDateOk();
                if (IsBackDateOk() == false)
                {
                    return;
                }

                if (ChequeNo == "")
                {
                    MessageBox.Show("Select a cheque to settle!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Label paidAmt = ucPayModes1.PaidAmountLabel;
                decimal paidAmount = Convert.ToDecimal(paidAmt.Text.Trim());
                //if (paidAmount < Convert.ToDecimal(TextBoxSAmo.Text.Trim()))
                if (paidAmount <= 0)
                {
                    MessageBox.Show("Please enter payments.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure to save?", "Confirm Settlement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //TODO: SAVE
                _recieptItem = ucPayModes1.RecieptItemList;
                //get seq no          
                //int seqNo = CHNLSVC.Inventory.GetSerialID();

                //get reciept no
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = "HO";//BaseCls.GlbUserDefLoca;
                _receiptAuto.Aut_cate_tp = "GPC";//"PC";//BaseCls.GlbUserDefLoca;
                _receiptAuto.Aut_start_char = "RCHQS";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "RCHQS";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = null;//2012;                      

                string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                //insert reciept
                RecieptHeader recieptHeadder = new RecieptHeader();
                int seqNo = CHNLSVC.Inventory.GetSerialID();
                recieptHeadder.Sar_seq_no = seqNo;
                recieptHeadder.Sar_receipt_no = _cusNo;
                recieptHeadder.Sar_com_cd = BaseCls.GlbUserComCode;
                recieptHeadder.Sar_manual_ref_no = Ref_No;
                recieptHeadder.Sar_receipt_type = "RCHQS";
                recieptHeadder.Sar_receipt_date = Convert.ToDateTime(txtCurrentDt.Value.ToString());//DateTime.Now;
                recieptHeadder.Sar_profit_center_cd = BaseCls.GlbUserDefProf; //GlbUserDefLoca.ToUpper();
                recieptHeadder.Sar_debtor_name = "CHEQUE";
                recieptHeadder.Sar_tot_settle_amt = paidAmount;//Convert.ToDecimal(TextBoxSAmo.Text);
                recieptHeadder.Sar_direct = true;
                recieptHeadder.Sar_act = true;
                recieptHeadder.Sar_create_when = Convert.ToDateTime(txtCurrentDt.Value.ToString());// DateTime.Now; 
                recieptHeadder.Sar_create_by = BaseCls.GlbUserID;//GlbUserName;

                Int32 eff = 0;
                //save reciept items           
                eff = CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder);
                foreach (RecieptItem ri in _recieptItem)
                {
                    ri.Sard_seq_no = seqNo;
                    ri.Sard_receipt_no = _cusNo;
                    eff = CHNLSVC.Sales.SaveReceiptItem(ri);
                }
                //save reciept


                //return cheque update
                ChequeReturn chequeReturn = new ChequeReturn();
                chequeReturn.Seq = seqNo;
                chequeReturn.Pc = BaseCls.GlbUserDefProf; //GlbUserDefLoca;
                chequeReturn.Cheque_no = ChequeNo;
                chequeReturn.Company = BaseCls.GlbUserComCode; //GlbUserComCode;
                chequeReturn.Create_by = BaseCls.GlbUserID;//GlbUserName;
                chequeReturn.Bank = Bank;
                //if (Convert.ToDecimal(lblPayBalance.Text) == 0)
                if (ucPayModes1.Balance == 0)
                    chequeReturn.Is_set = true;
                chequeReturn.Create_Date = Convert.ToDateTime(txtCurrentDt.Value.ToString());//DateTime.Now;
                chequeReturn.Settle_val = paidAmount;//Convert.ToDecimal(lblPayPaid.Text);

                eff = CHNLSVC.Financial.SaveReturnCheque(chequeReturn);
                if (eff > 0)
                {
                    MessageBox.Show("Updated Successfully!\n\nReceipt No. =" + _cusNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.btnClear_Click(null, null);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                panel_cancel.Visible = true;
                //TODO:CHECK PERMISSION
                grv_cancelSettle.DataSource = null;
                grv_cancelSettle.AutoGenerateColumns = false;
                grv_cancelSettle.DataSource = CHNLSVC.Financial.get_Rtn_Chq_DET(BaseCls.GlbUserDefProf, Convert.ToDateTime(txtCurrentDt.Value.ToString()), Convert.ToDateTime(txtCurrentDt.Value.ToString()));

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
            panel_cancel.Visible = false;
            grv_cancelSettle.DataSource = null;
        }

        private void grv_cancelSettle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 rowIndex = e.RowIndex;
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    //GridViewRow row = grvInvItems.SelectedRow;
                    DataGridViewRow row = grv_cancelSettle.Rows[rowIndex];
                    Int32 Seq_no = Convert.ToInt32(row.Cells["c_SAR_SEQ_NO"].Value.ToString());
                    Decimal receptAmt = Convert.ToDecimal(row.Cells["c_SAR_TOT_SETTLE_AMT"].Value.ToString());//c_SAR_TOT_SETTLE_AMT
                    string referenceNo = (row.Cells["c_srcq_ref"].Value.ToString());

                    if (MessageBox.Show("Are you sure to cancel settlement?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    Int32 eff = CHNLSVC.Financial.Cancel_ReturnChequesSettlement(referenceNo, Seq_no, receptAmt);
                    if (eff > 0)
                    {
                        MessageBox.Show("Settlement Cancelled Successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.btnClear_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Settlement Not Cancelled!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtCurrentDt_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, mouduleName, txtCurrentDt, lblBackDate, string.Empty, out _allowCurrentTrans) == false)
                {
                    if (Convert.ToDateTime(txtCurrentDt.Value.Date).Date != CHNLSVC.Security.GetServerDateTime().Date)
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Back date not allow for selected date!");
                        MessageBox.Show("Back date not allowed for the selected date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCurrentDt.Value = CHNLSVC.Security.GetServerDateTime().Date;
                        // return;
                    }
                    else
                    {

                    }
                    //  txtReceiptDate.Text = pickedDate.Value.ToShortDateString();

                    if (_allowCurrentTrans == true)
                    {
                        if (txtCurrentDt.Value.Date != CHNLSVC.Security.GetServerDateTime().Date)
                        {
                            txtCurrentDt.Enabled = true;
                            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCurrentDt.Focus(); 
                            return;
                        }
                    }
                    else
                    {
                        txtCurrentDt.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCurrentDt.Focus();
                        txtCurrentDt.Focus();
                        return;
                    }
                }
                else
                {
                    // txtReceiptDate.Text = pickedDate.Value.ToShortDateString();
                    txtCurrentDt.Enabled = true;
                }
                //txtCurrentDt.Enabled = true;
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

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            try
            {
                // bool IsBD_Ok= IsBackDateOk();
                if (IsBackDateOk() == false)
                {
                    return;
                }

                if (ChequeNo == "")
                {
                    MessageBox.Show("Select a cheque to settle!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Label paidAmt = ucPayModes1.PaidAmountLabel;
                decimal paidAmount = Convert.ToDecimal(paidAmt.Text.Trim());
                //if (paidAmount < Convert.ToDecimal(TextBoxSAmo.Text.Trim()))
                if (paidAmount <= 0)
                {
                    MessageBox.Show("Please enter payments.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ucPayModes1.Balance != 0)
                {
                    MessageBox.Show("Payment not completed.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure to save?", "Confirm Settlement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //TODO: SAVE
                _recieptItem = ucPayModes1.RecieptItemList;
                //get seq no          
                //int seqNo = CHNLSVC.Inventory.GetSerialID();

                //get reciept no
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;//BaseCls.GlbUserDefLoca;
                _receiptAuto.Aut_cate_tp = BaseCls.GlbUserDefProf;//"PC";//BaseCls.GlbUserDefLoca;
                _receiptAuto.Aut_start_char = "RCHQS";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "RCHQS";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = null;//2012;                      

                decimal _totIntTobeSet = 0;
                decimal _totNewInt = 0;
                decimal _totPaidInt = 0;

                _totIntTobeSet = Convert.ToDecimal(lblTotNewInt.Text);
                _totNewInt = Convert.ToDecimal(lblBalInt.Text);
                _totPaidInt = _totIntTobeSet - _totNewInt;

                string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                //insert reciept
                RecieptHeader recieptHeadder = new RecieptHeader();
                int seqNo = CHNLSVC.Inventory.GetSerialID();
                recieptHeadder.Sar_seq_no = seqNo;
                recieptHeadder.Sar_receipt_no = _cusNo;
                recieptHeadder.Sar_com_cd = BaseCls.GlbUserComCode;
                recieptHeadder.Sar_manual_ref_no = Ref_No;
                recieptHeadder.Sar_ref_doc = ref_No;
                recieptHeadder.Sar_receipt_type = "RCHQS";
                recieptHeadder.Sar_receipt_date = Convert.ToDateTime(txtCurrentDt.Value.ToString());//DateTime.Now;
                recieptHeadder.Sar_profit_center_cd = BaseCls.GlbUserDefProf; //GlbUserDefLoca.ToUpper();
                recieptHeadder.Sar_debtor_name = "CHEQUE";
                recieptHeadder.Sar_tot_settle_amt = paidAmount;//Convert.ToDecimal(TextBoxSAmo.Text);
                recieptHeadder.Sar_direct = true;
                recieptHeadder.Sar_act = true;
                recieptHeadder.Sar_create_when = Convert.ToDateTime(txtCurrentDt.Value.ToString());// DateTime.Now; 
                recieptHeadder.Sar_create_by = BaseCls.GlbUserID;//GlbUserName;
                recieptHeadder.Sar_comm_amt = _totPaidInt;
                recieptHeadder.Sar_session_id = BaseCls.GlbUserSessionID;

                Int32 eff = 0;
                //save reciept items           
                //eff = CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder);
                //foreach (RecieptItem ri in _recieptItem)
                //{
                //    ri.Sard_seq_no = seqNo;
                //    ri.Sard_receipt_no = _cusNo;
                //eff = CHNLSVC.Sales.SaveReceiptItem(ri);
                //}

                //return cheque update
                ChequeReturn chequeReturn = new ChequeReturn();
                chequeReturn.Seq = seqNo;
                chequeReturn.Pc = BaseCls.GlbUserDefProf;
                chequeReturn.Cheque_no = ChequeNo;
                chequeReturn.Company = BaseCls.GlbUserComCode;
                chequeReturn.Create_by = BaseCls.GlbUserID;
                chequeReturn.Bank = Bank;

                //if (ucPayModes1.Balance == 0)
                //chequeReturn.Is_set = true;
                //-----------------------------------------------------------
                Decimal bal = (BalanceToFullSettle - paidAmount);
                if (bal <= 0)
                    chequeReturn.Is_set = true;
                //-----------------------------------------------------------

                chequeReturn.Create_Date = Convert.ToDateTime(txtCurrentDt.Value.ToString());
                chequeReturn.Settle_val = paidAmount;//Convert.ToDecimal(lblPayPaid.Text);

                decimal _newCalInt = 0;
                foreach (ReturnChequeCalInterest _tmpList in _newAddList)
                {
                    _newCalInt = _newCalInt + _tmpList.Src_int;
                }

                chequeReturn.Intrest = _newCalInt;//Convert.ToDecimal(lblTotNewInt.Text);
                chequeReturn.Srcq_cap_set = (Convert.ToDecimal(lblAmtBalCapital.Text) - Convert.ToDecimal(lblCapitalBalance.Text));
                chequeReturn.Srcq_intr_set = (Convert.ToDecimal(lblTotNewInt.Text) - Convert.ToDecimal(lblBalInt.Text));

                string RtnReceipt = "";

                eff = CHNLSVC.Financial.SaveReturnCheque_NEW(recieptHeadder, null, _recieptItem, chequeReturn, _newAddList,null, out RtnReceipt);

                //eff = CHNLSVC.Financial.SaveReturnCheque(chequeReturn);
                if (eff > 0)
                {
                    MessageBox.Show("Updated Successfully!\n\nReceipt No. =" + _cusNo, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.btnClear_Click(null, null);
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

        private void TextBoxSAmo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (string.IsNullOrEmpty(TextBoxSAmo.Text))
                    {
                        MessageBox.Show("Please enter settle amount.", "Cheque settlement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TextBoxSAmo.Focus();
                        return;
                    }

                    if (!IsNumeric(TextBoxSAmo.Text))
                    {
                        MessageBox.Show("Please enter valid settle amount.", "Cheque settlement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TextBoxSAmo.Text = "";
                        return;

                    }

                    if (Convert.ToDecimal(TextBoxSAmo.Text) > (Convert.ToDecimal(lblAmtBalCapital.Text) + Convert.ToDecimal(lblTotNewInt.Text)))
                    {
                        MessageBox.Show("Settle amount cannot exceed actual amount to be settle.", "Cheque settlement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TextBoxSAmo.Text = "";
                        return;
                    }

                    CalBalanceAmount();

                    ucPayModes1.ClearControls();
                    ucPayModes1.InvoiceType = "RCHQS";
                    // ucPayModes1.TotalAmount = (amount - settleAmoun);
                    ucPayModes1.TotalAmount = Math.Round(Convert.ToDecimal(TextBoxSAmo.Text.Trim()), 2);
                    //ucPayModes1.Amount.Text = string.Format("{0:n2}", (amount - settleAmoun));   
                    ucPayModes1.LoadData();
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

        private void TextBoxSAmo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxSAmo.Text))
            {
                if (!IsNumeric(TextBoxSAmo.Text))
                {
                    MessageBox.Show("Please enter valid settle amount.", "Cheque settlement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxSAmo.Text = "";
                    return;

                }

                if (Convert.ToDecimal(TextBoxSAmo.Text) > (Convert.ToDecimal(lblAmtBalCapital.Text) + Convert.ToDecimal(lblTotNewInt.Text)))
                {
                    MessageBox.Show("Settle amount cannot exceed actual amount to be settle.", "Cheque settlement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxSAmo.Text = "";
                    return;
                }

                CalBalanceAmount();

                ucPayModes1.ClearControls();
                ucPayModes1.InvoiceType = "RCHQS";
                // ucPayModes1.TotalAmount = (amount - settleAmoun);
                ucPayModes1.TotalAmount = Math.Round(Convert.ToDecimal(TextBoxSAmo.Text.Trim()), 2);
                //ucPayModes1.Amount.Text = string.Format("{0:n2}", (amount - settleAmoun));   
                ucPayModes1.LoadData();
            }
        }

        private void CalBalanceAmount()
        {
            decimal _SetAmt = 0;
            decimal _BalCapital = 0;
            decimal _balaInt = 0;
            decimal _newBalCap = 0;
            decimal _newBalInt = 0;
            decimal _newSetAmt = 0;

            _SetAmt = Convert.ToDecimal(TextBoxSAmo.Text);
            _BalCapital = Convert.ToDecimal(lblAmtBalCapital.Text);
            _balaInt = Convert.ToDecimal(lblTotNewInt.Text);

            if (_SetAmt >= _balaInt)
            {
                _newBalInt = 0;
                _newSetAmt = _SetAmt - _balaInt;
                _newBalCap = _BalCapital - _newSetAmt;

            }
            else
            {
                _newBalInt = _balaInt - _SetAmt;
                _newSetAmt = 0;
                _newBalCap = _BalCapital - _newSetAmt;
            }

            lblCapitalBalance.Text = string.Format("{0:n2}", (_newBalCap));
            lblBalInt.Text = string.Format("{0:n2}", (_newBalInt));
        }

        private void grv_cancelSettle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ucPayModes1_Load(object sender, EventArgs e)
        {

        }


    }
}
