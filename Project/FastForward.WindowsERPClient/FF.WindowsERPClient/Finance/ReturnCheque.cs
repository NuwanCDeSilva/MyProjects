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

namespace FF.WindowsERPClient.Finance
{
    public partial class ReturnCheque : Base
    {           
        //pkg_search.sp_search_cheque
        Boolean _mgrIssu = false;

        public ReturnCheque()
        {
            InitializeComponent();
           
            //BindBanks(DropDownListBank, DropDownListReturnBank, DropDownListNBank);
            BindBanks(DropDownListBank, DropDownListNBank);
            //listBoxBnkTp.SelectedIndex = 0;
            comboBnkTp.SelectedIndex = 0;
            TextBoxLocation.Text = BaseCls.GlbUserDefLoca;                 
            DivNewCheque.Enabled = false;
            TextBoxLocation.Focus();
            _mgrIssu = false;
           // MessageBox.Show(comboBnkTp.SelectedItem.ToString());
        }

        private void BindBanks(ComboBox ddl1, ComboBox ddl2)
        {
            try
            {
                DataTable datasource = CHNLSVC.Financial.GetBanks();
                ddl1.DataSource = datasource;
                ddl1.DisplayMember = "mbi_desc";
                ddl1.ValueMember = "mbi_cd";
                ddl1.SelectedIndex = -1;

                DataTable datasource2 = CHNLSVC.Financial.GetBanks();
                ddl2.DataSource = datasource2;
                ddl2.DisplayMember = "mbi_desc";
                ddl2.ValueMember = "mbi_cd";
                ddl2.SelectedIndex = -1;

                //DataTable datasource3 = CHNLSVC.Financial.GetBanks();
                //ddl3.DataSource = datasource3;
                //ddl3.DisplayMember = "mbi_desc";
                //ddl3.ValueMember = "mbi_cd";
                //ddl3.SelectedIndex = -1;
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
        private void myEvent(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void SalesReversal_Load(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void Clear_Data()
        {
            TextBoxLocation.Text = "";
        }


        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }             
            
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(DropDownListNBank.SelectedValue.ToString() + seperator);//DropDownListNBank
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Cheque:
                    {                      
                       //paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text.Trim().ToUpper() + seperator);                        
                       paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text.Trim().ToUpper() + seperator + DropDownListBank.SelectedValue.ToString() + seperator);                             
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
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


        private void txtProfitcenter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = TextBoxLocation;
                    _CommonSearch.ShowDialog();
                    TextBoxLocation.Select();
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

        private void txtProfitcenter_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextBoxLocation.Text))
                {
                    Int32 _isValidPC = 0;
                    TextBoxLocation.Text = TextBoxLocation.Text.Trim().ToUpper();
                    _isValidPC = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim().ToUpper());

                    if (_isValidPC == 0)
                    {
                        MessageBox.Show("Invalid or accsess denied Profitcenter.", "Return Cheque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TextBoxLocation.Text = "";
                        TextBoxLocation.Focus();
                    }
                    else
                    {

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

        private void btnProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxLocation;
                _CommonSearch.ShowDialog();
                TextBoxLocation.Select();
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


        private void btnBank_Click(object sender, EventArgs e)
        {            
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                //DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtBank;
                //_CommonSearch.ShowDialog();
                //txtBank.Select();            
        }

         private void txtChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                    DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = TextBoxChequeNumber;
                    _CommonSearch.ShowDialog();
                    TextBoxChequeNumber.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    TextBoxReturnDate.Focus();
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
         private void btnCheque_Click(object sender, EventArgs e)
         {
             try
             {
                 if (DropDownListBank.SelectedIndex == -1)
                 {
                     MessageBox.Show("Select Bank!");
                     return;
                 }
                 if (TextBoxLocation.Text.Trim() == "")
                 {
                     MessageBox.Show("Enter Profit center!");
                     return;
                 }
                 CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                 _CommonSearch.ReturnIndex = 0;
                 _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Cheque);
                 DataTable _result = CHNLSVC.CommonSearch.Get_Search_cheque(_CommonSearch.SearchParams, null, null);
                 _CommonSearch.dvResult.DataSource = _result;
                 _CommonSearch.BindUCtrlDDLData(_result);
                 _CommonSearch.obj_TragetTextBox = TextBoxChequeNumber;
                 _CommonSearch.ShowDialog();
                 TextBoxChequeNumber.Select();
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

         private void btnClear_Click(object sender, EventArgs e)
         {
             try
             {
                 ReturnCheque formnew = new ReturnCheque();
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

         private void GridViewDataBind()
         {
             try
             {
                 //add 19-03-2013
                 btnSave.Enabled = true;
                 LabelToSysAmo.Text = string.Format("{0:n2}", 0);
                 TextBoxReturnAmount.Text = string.Format("{0:n2}", 0);
                 string _type = "";
                 string _value = "";
                 Int32 _days = 0;
                 decimal _mgrChg = 0;
                 Boolean _mgrChgCal = false;
                 
                 //----------------------------------------------------------
                 //check return cheque table
                 DataTable datatable = CHNLSVC.Financial.GetReturnChequeCount(TextBoxChequeNumber.Text, DropDownListBank.SelectedValue.ToString(), TextBoxLocation.Text.ToUpper());
                 if (Convert.ToInt32(datatable.Rows[0][0].ToString()) <= 0)
                 {

                     DataTable datasource = CHNLSVC.Financial.GetReturnCheques(TextBoxLocation.Text.ToUpper(), TextBoxChequeNumber.Text, DropDownListBank.SelectedValue.ToString());
                     DataTable remsource = CHNLSVC.Financial.GetReturnChequesFromRemSum(TextBoxLocation.Text.ToUpper(), TextBoxChequeNumber.Text, DropDownListBank.SelectedValue.ToString());


                     datasource.Merge(remsource);

                     GridViewCheques.DataSource = null;
                     GridViewCheques.AutoGenerateColumns = false;
                     GridViewCheques.DataSource = datasource;

                     //GridRowCount.Value = GridViewCheques.Rows.Count.ToString();//TODO:
                     decimal total = 0;
                     

                     if (datasource != null)
                     {
                         if (datasource.Rows.Count > 0)
                         {

                             DropDownListNBank.SelectedValue = Convert.ToString(datasource.Rows[0]["SARD_CHQ_BANK_CD"]);//
                             TextBoxBranch.Text = Convert.ToString(datasource.Rows[0]["SARD_CHQ_BRANCH"]);
                             
                             try
                             {
                                 TextBoxChDate.Value = Convert.ToDateTime(datasource.Rows[0]["sard_chq_dt"]);
                             }
                             catch (Exception EX)
                             {

                             }

                         }
                         else
                         {
                             MessageBox.Show("Invalid Cheque Number!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         }
                     }
                     //sard_chq_branch
                     foreach (DataRow dr in datasource.Rows)
                     {
                         total = total + Convert.ToDecimal(dr["sard_settle_amt"]);

                     }


                     for (int x = 0; x < GridViewCheques.Rows.Count; x++)
                     {
                         if (Convert.ToInt16(GridViewCheques.Rows[x].Cells["sar_is_mgr_iss"].Value) == 1)
                         {
                             _mgrIssu = true;
                             goto L4;
                         }
                     }
                 L4:
                     { Int16 x = 0; }


                 if (_mgrIssu == true)
                 {
                     _days = Convert.ToInt32((Convert.ToDateTime(TextBoxReturnDate.Text).Date - Convert.ToDateTime(TextBoxChDate.Text).Date).TotalDays);
                     if (_days < 0)
                     {
                         _days = 0;
                     }
                     List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                     if (_hir2.Count > 0)
                     {
                         foreach (MasterSalesPriorityHierarchy _two in _hir2)
                         {
                             _type = _two.Mpi_cd;
                             _value = _two.Mpi_val;

                             List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceChargesNew(_type, _value, "RTNCHQMAN", Convert.ToDateTime(TextBoxReturnDate.Text).Date);
                             if (_ser != null)
                             {
                                 foreach (HpServiceCharges _ser1 in _ser)
                                 {
                                     if (_ser1.Hps_from_val <= _days && _ser1.Hps_to_val >= _days)
                                     {
                                         _mgrChg = Math.Round(((total * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                         _mgrChgCal = true;
                                         goto L3;
                                     }
                                 }
                             }
                         }
                     }
                     else
                     {
                         MessageBox.Show("Profit center hirarchy not set.", "Return Cheque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         btnSave.Enabled = false;
                         return;
                     }

                     if (_mgrChgCal == false)
                     {
                         MessageBox.Show("Manager issue cheque and cannot find manager charge definition.", "Return Cheque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         _mgrChg = 0;
                         lblMgrIssuChg.Text = string.Format("{0:n2}", _mgrChg);
                         btnSave.Enabled = false;
                         return;
                     }
                 }

                 L3: Int16 i = 0;

                     LabelToSysAmo.Text = string.Format("{0:n2}", total);//total.ToString();
                     TextBoxReturnAmount.Text = string.Format("{0:n2}", total);//total.ToString();
                     lblMgrIssuChg.Text = string.Format("{0:n2}", _mgrChg);//total.ToString();
                 }
                 else
                 {
                     if (TextBoxChequeNumber.Text != "")
                     {
                         //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Entering Cheque is already available as Return Cheque");
                         MessageBox.Show("Cheque already Returned!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         //  this.btnClear_Click(null, null);
                         try
                         {
                             DataTable datasource = CHNLSVC.Financial.GetReturnCheques(TextBoxLocation.Text.ToUpper(), TextBoxChequeNumber.Text, DropDownListBank.SelectedValue.ToString());

                             DataTable remsource = CHNLSVC.Financial.GetReturnChequesFromRemSum(TextBoxLocation.Text.ToUpper(), TextBoxChequeNumber.Text, DropDownListBank.SelectedValue.ToString());


                             datasource.Merge(remsource);

                             GridViewCheques.DataSource = null;
                             GridViewCheques.AutoGenerateColumns = false;
                             GridViewCheques.DataSource = datasource;
                             DataTable dt = CHNLSVC.Financial.get_rtn_chq_byBankChqNo(DropDownListBank.SelectedValue.ToString(), TextBoxChequeNumber.Text.Trim());

                             Decimal total = 0;
                             _mgrChg = 0;
                             if (dt != null)
                             {
                                 foreach (DataRow dr in dt.Rows)
                                 {
                                     total = total + Convert.ToDecimal(dr["srcq_act_val"]);
                                     _mgrChg = _mgrChg + Convert.ToDecimal(dr["srcq_mgr_chg"]);
                                 }
                                 LabelToSysAmo.Text = string.Format("{0:n2}", total);//total.ToString();
                                 TextBoxReturnAmount.Text = string.Format("{0:n2}", total);//total.ToString();
                                 lblMgrIssuChg.Text = string.Format("{0:n2}", _mgrChg);//total.ToString();
                             }
                             //LabelToSysAmo.Text = string.Format("{0:n2}", total);//total.ToString();
                             TextBoxReturnAmount.Text = string.Format("{0:n2}", total);//total.ToString();

                             total = 0;
                             foreach (DataRow dr in datasource.Rows)
                             {
                                 total = total + Convert.ToDecimal(dr["sard_settle_amt"]);
                             }
                             LabelToSysAmo.Text = string.Format("{0:n2}", total);//total.ToString();
                         }
                         catch (Exception ex) { }

                         btnSave.Enabled = false;
                     }
                     else
                     {
                         this.btnClear_Click(null, null);
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

         private void imgSelect_Click(object sender, EventArgs e)
         {
             try
             {
                 if (DropDownListBank.SelectedIndex == -1)
                 {
                     MessageBox.Show("Please select the Bank!");
                     return;
                 }
                 if (TextBoxChequeNumber.Text.Trim() == "")
                 {
                     MessageBox.Show("Please enter the cheque number!");
                     return;
                 }
                 DivNewCheque.Enabled = true;
                 TextBoxNChequeNumber.Text = TextBoxChequeNumber.Text;
                 GridViewDataBind();
                 if (GridViewCheques.Rows.Count < 1)
                 {
                     TextBoxNChequeNumber.Text = "";
                 }
                 //add 19-03-2013
                 #region
                 DataTable dt = CHNLSVC.Financial.get_rtn_chq_byBankChqNo(DropDownListBank.SelectedValue.ToString(), TextBoxChequeNumber.Text.Trim());
                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         if (Convert.ToString(dt.Rows[0]["srcq_pc"]) == TextBoxLocation.Text)
                         {
                             btnSave.Enabled = false;
                         }

                         

                         //MessageBox.Show("Already Returned!");
                         DataTable datasource = CHNLSVC.Financial.GetReturnCheques(TextBoxLocation.Text.ToUpper(), TextBoxChequeNumber.Text, DropDownListBank.SelectedValue.ToString());
                         DataTable remsource = CHNLSVC.Financial.GetReturnChequesFromRemSum(TextBoxLocation.Text.ToUpper(), TextBoxChequeNumber.Text, DropDownListBank.SelectedValue.ToString());


                         datasource.Merge(remsource);

                         GridViewCheques.DataSource = null;
                         GridViewCheques.AutoGenerateColumns = false;
                         GridViewCheques.DataSource = datasource;

                         Decimal total = 0;
                         decimal _mgrChg = 0;
                         if (dt != null)
                         {
                             foreach (DataRow dr in dt.Rows)
                             {
                                 if (Convert.ToString(dr["srcq_pc"]) == TextBoxLocation.Text)
                                 {
                                     total = total + Convert.ToDecimal(dr["srcq_act_val"]);
                                     _mgrChg = _mgrChg + Convert.ToDecimal(dr["srcq_mgr_chg"]);
                                 }
                             }
                             LabelToSysAmo.Text = string.Format("{0:n2}", total);//total.ToString();
                             TextBoxReturnAmount.Text = string.Format("{0:n2}", total);//total.ToString();
                         }
                         //LabelToSysAmo.Text = string.Format("{0:n2}", total);//total.ToString();
                         TextBoxReturnAmount.Text = string.Format("{0:n2}", total);//total.ToString();
                         

                         total = 0;
                         foreach (DataRow dr in datasource.Rows)
                         {
                             //if (Convert.ToString(dr["srcq_pc"]) == TextBoxLocation.Text)
                             //{
                                 total = total + Convert.ToDecimal(dr["sard_settle_amt"]);
                             //}
                         }
                         LabelToSysAmo.Text = string.Format("{0:n2}", total);//total.ToString();
                         lblMgrIssuChg.Text = string.Format("{0:n2}", _mgrChg);//total.ToString();
                     }

                 }

                 #endregion
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

         private void btnEdit_Click(object sender, EventArgs e)
         {
            
             try
             {
                 if (MessageBox.Show("Are you sure to edit?", "Confirm Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                 {
                     return;
                 }
                 string bankCd = DropDownListNBank.SelectedValue.ToString();
                 string branch = TextBoxBranch.Text.Trim();
                 int result = CHNLSVC.Financial.UpdateReturnCheques(TextBoxNChequeNumber.Text, TextBoxChequeNumber.Text,DropDownListNBank.SelectedValue.ToString(),DropDownListBank.SelectedValue.ToString(),TextBoxBranch.Text.Trim(), Convert.ToDateTime(TextBoxChDate.Value).Date,BaseCls.GlbUserComCode,TextBoxLocation.Text.Trim());
                 if (result > 0)
                 {
                     //ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records updated sucessfully!');", true);
                     MessageBox.Show("Updated Successfully!","",MessageBoxButtons.OK,MessageBoxIcon.Information); 
                     //TextBoxBranch.Text = "";
                     //TextBoxChDate.Text = "";
                     //DropDownListNBank.SelectedIndex = -1;
                     //DivNewCheque.Enabled = false;
                     //GridViewDataBind();
                     this.btnClear_Click(null, null);
                 }
                 else
                 {
                     //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured while processing!!");
                     MessageBox.Show("Not Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

         private void btn_Clear_Click(object sender, EventArgs e)
         {
             TextBoxBranch.Text = "";
             TextBoxChDate.Text = "";
             TextBoxNChequeNumber.Text = "";
             DropDownListNBank.SelectedIndex = -1;
             //DivNewCheque.Enabled = false;
             //GridViewDataBind();
         }

         private void TextBoxChequeNumber_KeyPress(object sender, KeyPressEventArgs e)
         {
             if(e.KeyChar==(char)Keys.Enter)
             {
                 this.imgSelect_Click(null, null);
             }
         }

        

         private void btnSave_Click(object sender, EventArgs e)
         {
             try
             {
                 if (GridViewCheques.Rows.Count > 0)
                 {
                     //if (DropDownListReturnBank.SelectedValue.Equals("-1"))
                     TextBoxLocation.Text = TextBoxLocation.Text.Trim().ToUpper();

                     if (string.IsNullOrEmpty(txtRtnBankAcc.Text))
                     {
                         // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select return bank");
                         MessageBox.Show("Please select return bank!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         txtRtnBankAcc.Focus();
                         return;
                     }
                     if (TextBoxReturnDate.Text == "")
                     {
                         //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select return date");
                         MessageBox.Show("Please select return date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                     }
                     try
                     {
                         Decimal retunVal = Convert.ToDecimal(TextBoxReturnAmount.Text.Trim());
                     }
                     catch (Exception EX)
                     {
                         MessageBox.Show("Please enter valid return amount!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         return;
                     }

                     if (MessageBox.Show("Are you sure to save?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                     {
                         return;
                     }
                     int seqNo = CHNLSVC.Inventory.GetSerialID();
                     MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                     //get reciept number
                     _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;//BaseCls.GlbUserDefProf;
                     _receiptAuto.Aut_cate_tp = BaseCls.GlbUserDefProf;
                     _receiptAuto.Aut_start_char = "RCHQ";
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
                     recieptHeadder.Sar_com_cd = BaseCls.GlbUserComCode;
                     recieptHeadder.Sar_receipt_type = "RTCHQ";
                     recieptHeadder.Sar_receipt_date = CHNLSVC.Security.GetServerDateTime().Date;
                     recieptHeadder.Sar_profit_center_cd = TextBoxLocation.Text.ToUpper();
                     recieptHeadder.Sar_debtor_name = "CHEQUE";
                     recieptHeadder.Sar_tot_settle_amt = Convert.ToDecimal(TextBoxReturnAmount.Text);
                     recieptHeadder.Sar_direct = false;
                     recieptHeadder.Sar_act = true;
                     recieptHeadder.Sar_create_by = BaseCls.GlbUserID;
                     recieptHeadder.Sar_session_id = BaseCls.GlbUserSessionID;
                     recieptHeadder.Sar_create_when = CHNLSVC.Security.GetServerDateTime().Date;

                     //  CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder); **

                     //insert reciept item
                     RecieptItem recieptItem = new RecieptItem();
                     recieptItem.Sard_seq_no = seqNo;
                     recieptItem.Sard_line_no = 1;
                     recieptItem.Sard_receipt_no = _cusNo;
                     recieptItem.Sard_chq_bank_cd = DropDownListBank.SelectedValue.ToString();
                     recieptItem.Sard_pay_tp = "CASH";
                     recieptItem.Sard_settle_amt = 0;

                     //CHNLSVC.Sales.SaveReceiptItem(recieptItem); **

                     //add return cheque record
                     ChequeReturn chequeReturn = new ChequeReturn();
                     chequeReturn.Seq = seqNo;
                     chequeReturn.RefNo = _cusNo;
                     chequeReturn.Bank = DropDownListBank.SelectedValue.ToString();
                     chequeReturn.Pc = TextBoxLocation.Text;
                     chequeReturn.Bank_type = comboBnkTp.SelectedItem.ToString() == "CASH" ? 1 : 0;// listBoxBnkTp.SelectedItem.ToString()=="CASH"?1:0;
                     chequeReturn.Returndate = Convert.ToDateTime(TextBoxReturnDate.Text);
                     chequeReturn.Return_bank = txtRtnBankAcc.Text.Trim();
                     chequeReturn.Cheque_no = TextBoxChequeNumber.Text;
                     chequeReturn.Company = BaseCls.GlbUserComCode;
                     chequeReturn.Create_by = BaseCls.GlbUserID;
                     chequeReturn.Create_Date = CHNLSVC.Security.GetServerDateTime().Date;
                     chequeReturn.Act_value = Convert.ToDecimal(TextBoxReturnAmount.Text);
                     chequeReturn.Sys_value = Convert.ToDecimal(LabelToSysAmo.Text);
                     chequeReturn.Srcq_mgr_chg = Convert.ToDecimal(lblMgrIssuChg.Text);
                     chequeReturn.Srcq_chq_branch = TextBoxBranch.Text.Trim();                     
                     // chequeReturn.Settle_val = Convert.ToDecimal(TextBoxReturnAmount.Text);

                     //Int32 eff= CHNLSVC.Financial.SaveReturnCheque(chequeReturn);//**
                     Int32 eff = 0;
                     try
                     {
                         eff = CHNLSVC.Financial.ChequeReturn(recieptHeadder, recieptItem, chequeReturn);
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         eff = 0;
                         return;

                     }

                     if (eff > 0)
                     {
                         MessageBox.Show("Successfully Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         this.btnClear_Click(null, null);
                     }
                     else
                     {
                         MessageBox.Show("Not Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     //GridViewDataBind();
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

         private void TextBoxLocation_KeyPress(object sender, KeyPressEventArgs e)
         {
             if(e.KeyChar==(char)Keys.Enter)
             {
                 txtProfitcenter_Leave(sender, e);
                 DropDownListBank.Focus();
                 DropDownListBank.DroppedDown=true;
             }
            
         }

         private void TextBoxChequeNumber_MouseDoubleClick(object sender, MouseEventArgs e)
         {
             this.btnCheque_Click(null, null);
         }

         private void TextBoxChequeNumber_KeyDown(object sender, KeyEventArgs e)
         {
             if(e.KeyCode==Keys.F2)
             {
                 this.btnCheque_Click(null, null);
             }
         }
         
         private void btnProfitCenter_Click_1(object sender, EventArgs e)
         {
             //TextBox txtBox = new TextBox();
             //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
             //_CommonSearch.ReturnIndex = 0;
             //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
             //DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
             //_CommonSearch.dvResult.DataSource = _result;
             //_CommonSearch.BindUCtrlDDLData(_result);
             //_CommonSearch.obj_TragetTextBox = TextBoxLocation; //txtBox;
             //_CommonSearch.ShowDialog();
            
           
         }

         private void TextBoxLocation_DoubleClick(object sender, EventArgs e)
         {
             this.btnProfitCenter_Click(null, null);
         }

         private void TextBoxLocation_KeyDown(object sender, KeyEventArgs e)
         {
             if (e.KeyCode == Keys.F2)
             {
                 this.btnProfitCenter_Click(null, null);
             }
         }

         private void TextBoxBranch_Leave(object sender, EventArgs e)
         {

         }

         private void TextBoxBranch_DoubleClick(object sender, EventArgs e)
         {
             try
             {
                 if (DropDownListNBank.SelectedIndex == -1)
                 {
                     MessageBox.Show("Select the new Bank!");
                     return;
                 }
                 CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                 _CommonSearch.ReturnIndex = 0;
                 _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                 DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                 _CommonSearch.dvResult.DataSource = _result;
                 _CommonSearch.BindUCtrlDDLData(_result);
                 _CommonSearch.obj_TragetTextBox = TextBoxBranch;
                 _CommonSearch.ShowDialog();
                 TextBoxBranch.Select();
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

         private void btnBranchSerarch_Click(object sender, EventArgs e)
         {
             try
             {
                 if (DropDownListNBank.SelectedIndex == -1)
                 {
                     MessageBox.Show("Select the new Bank!");
                     return;
                 }
                 CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                 _CommonSearch.ReturnIndex = 0;
                 _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                 DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                 _CommonSearch.dvResult.DataSource = _result;
                 _CommonSearch.BindUCtrlDDLData(_result);
                 _CommonSearch.obj_TragetTextBox = TextBoxBranch;
                 _CommonSearch.ShowDialog();
                 TextBoxBranch.Select();
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

         private void TextBoxBranch_KeyDown(object sender, KeyEventArgs e)
         {
             try
             {
                 if (e.KeyCode == Keys.F2)
                 {
                     if (DropDownListNBank.SelectedIndex == -1)
                     {
                         MessageBox.Show("Select the new Bank!");
                         return;
                     }
                     CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                     _CommonSearch.ReturnIndex = 0;
                     _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                     DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                     _CommonSearch.dvResult.DataSource = _result;
                     _CommonSearch.BindUCtrlDDLData(_result);
                     _CommonSearch.obj_TragetTextBox = TextBoxBranch;
                     _CommonSearch.ShowDialog();
                     TextBoxBranch.Select();
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

         private void TextBoxNChequeNumber_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar == (char)Keys.Enter)
             {
                 DropDownListNBank.Focus();               
                 DropDownListNBank.DroppedDown=true;
             }
         }

         private void TextBoxBranch_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar == (char)Keys.Enter)
             {
                 TextBoxChDate.Focus();
             }
         }

         private void DropDownListBank_SelectedIndexChanged(object sender, EventArgs e)
         {
             //if (DropDownListBank.SelectedIndex != -1)
             //{
             
             //}
             this.btn_Clear_Click(sender, e);
             TextBoxChequeNumber.Text = "";
         }

         private void DropDownListNBank_SelectedIndexChanged(object sender, EventArgs e)
         {
             TextBoxBranch.Text = "";
         }

         private void label18_Click(object sender, EventArgs e)
         {

         }

         private void label23_Click(object sender, EventArgs e)
         {

         }

         private void DropDownListBank_SelectionChangeCommitted(object sender, EventArgs e)
         {
             TextBoxChequeNumber.Focus();
         }

         private void TextBoxReturnDate_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar == (char)Keys.Enter)
             {
                 txtRtnBankAcc.Focus();
                 
             }
         }

         private void DropDownListReturnBank_SelectionChangeCommitted(object sender, EventArgs e)
         {
             comboBnkTp.Focus();
             comboBnkTp.DroppedDown = true;

         }

         private void TextBoxReturnAmount_TextChanged(object sender, EventArgs e)
         {
             try
             {
                 Convert.ToDecimal(TextBoxReturnAmount.Text);
             }
             catch (Exception ex)
             {
                 TextBoxReturnAmount.Text = "";
             }
         }

         private void TextBoxReturnAmount_Leave(object sender, EventArgs e)
         {
             if (!string.IsNullOrEmpty(TextBoxReturnAmount.Text))
             {
                 _mgrIssu = false;
                 decimal _mgrChg = 0;
                 Boolean _mgrChgCal = false;
                 Int32 _days = 0;
                 string _type = "";
                 string _value = "";

                 for (int x = 0; x < GridViewCheques.Rows.Count; x++)
                 {
                     if (Convert.ToInt16(GridViewCheques.Rows[x].Cells["sar_is_mgr_iss"].Value) == 1)
                     {
                         _mgrIssu = true;
                         goto L4;
                     }
                 }
             L4:
                 { Int16 x = 0; }

                 if (_mgrIssu == true)
                 {
                     //_days = Convert.ToInt32((Convert.ToDateTime(TextBoxChDate.Text).Date - Convert.ToDateTime(TextBoxReturnDate.Text).Date).TotalDays);
                     _days = Convert.ToInt32((Convert.ToDateTime(TextBoxReturnDate.Text).Date - Convert.ToDateTime(TextBoxChDate.Text).Date).TotalDays);
                     if (_days < 0)
                     {
                         _days = 0;
                     }
                     List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                     if (_hir2.Count > 0)
                     {
                         foreach (MasterSalesPriorityHierarchy _two in _hir2)
                         {
                             _type = _two.Mpi_cd;
                             _value = _two.Mpi_val;

                             List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceChargesNew(_type, _value, "RTNCHQMAN", Convert.ToDateTime(TextBoxReturnDate.Text).Date);
                             if (_ser != null)
                             {
                                 foreach (HpServiceCharges _ser1 in _ser)
                                 {
                                     if (_ser1.Hps_from_val <= _days && _ser1.Hps_to_val >= _days)
                                     {
                                         _mgrChg = Math.Round(((Convert.ToDecimal(TextBoxReturnAmount.Text) * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                         _mgrChgCal = true;
                                         btnSave.Enabled = true;
                                         goto L3;
                                     }
                                 }
                             }
                         }
                     }
                     else
                     {
                         MessageBox.Show("Profit center hirarchy not set.", "Return Cheque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         btnSave.Enabled = false;
                         return;
                     }

                     if (_mgrChgCal == false)
                     {
                         MessageBox.Show("Manager issue cheque and cannot find manager charge definition.", "Return Cheque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         _mgrChg = 0;
                         lblMgrIssuChg.Text = string.Format("{0:n2}", _mgrChg);
                         btnSave.Enabled = false;
                         return;
                     }
                 }

             L3: Int16 i = 0;

                 lblMgrIssuChg.Text = string.Format("{0:n2}", _mgrChg);//total.ToString();
             }
         }

         private void btnSearchBankAcc_Click(object sender, EventArgs e)
         {
             try
             {
                 CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                 _CommonSearch.ReturnIndex = 0;
                 _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllBankAccount);
                 DataTable _result = CHNLSVC.CommonSearch.GetAllBankAccounts(_CommonSearch.SearchParams, null, null);
                 _CommonSearch.IsSearchEnter = true;
                 _CommonSearch.dvResult.DataSource = _result;
                 _CommonSearch.BindUCtrlDDLData(_result);
                 _CommonSearch.obj_TragetTextBox = txtRtnBankAcc;
                 _CommonSearch.ShowDialog();
                 txtRtnBankAcc.Select();
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

         private void txtRtnBankAcc_KeyDown(object sender, KeyEventArgs e)
         {
             try
             {
                 if (e.KeyCode == Keys.Enter)
                 {
                     comboBnkTp.Focus();
                 }
                 else if (e.KeyCode == Keys.F2)
                 {
                     CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                     _CommonSearch.ReturnIndex = 0;
                     _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                     DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
                     _CommonSearch.IsSearchEnter = true;
                     _CommonSearch.dvResult.DataSource = _result;
                     _CommonSearch.BindUCtrlDDLData(_result);
                     _CommonSearch.obj_TragetTextBox = txtRtnBankAcc;
                     _CommonSearch.ShowDialog();
                     txtRtnBankAcc.Select();
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

         private void txtRtnBankAcc_DoubleClick(object sender, EventArgs e)
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
                 _CommonSearch.obj_TragetTextBox = txtRtnBankAcc;
                 _CommonSearch.ShowDialog();
                 txtRtnBankAcc.Select();
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
         private bool CheckBankAcc(string code)
         {
             MasterBankAccount account = CHNLSVC.Sales.GetBankDetails(null, null, code);
             if (account == null || account.Msba_com == null || account.Msba_com == "")
             {
                 return false;
             }
             else
                 return true;
         }

         private void txtRtnBankAcc_Leave(object sender, EventArgs e)
         {
             try
             {
                 if (!string.IsNullOrEmpty(txtRtnBankAcc.Text))
                 {
                     if (!CheckBankAcc(txtRtnBankAcc.Text))
                     {
                         MessageBox.Show("Invalid Deposit bank account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         txtRtnBankAcc.Text = "";
                     }
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

         private void TextBoxReturnDate_Leave(object sender, EventArgs e)
         {
             if (!string.IsNullOrEmpty(TextBoxReturnAmount.Text))
             {
                 _mgrIssu = false;
                 decimal _mgrChg = 0;
                 Boolean _mgrChgCal = false;
                 Int32 _days = 0;
                 string _type = "";
                 string _value = "";

                 for (int x = 0; x < GridViewCheques.Rows.Count; x++)
                 {
                     if (Convert.ToInt16(GridViewCheques.Rows[x].Cells["sar_is_mgr_iss"].Value) == 1)
                     {
                         _mgrIssu = true;
                         goto L4;
                     }
                 }
             L4:
                 { Int16 x = 0; }

                 if (_mgrIssu == true)
                 {
                     _days = Convert.ToInt32((Convert.ToDateTime(TextBoxReturnDate.Text).Date - Convert.ToDateTime(TextBoxChDate.Text).Date).TotalDays);
                     if (_days < 0)
                     {
                         _days = 0;
                     }
                     List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                     if (_hir2.Count > 0)
                     {
                         foreach (MasterSalesPriorityHierarchy _two in _hir2)
                         {
                             _type = _two.Mpi_cd;
                             _value = _two.Mpi_val;

                             List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceChargesNew(_type, _value, "RTNCHQMAN", Convert.ToDateTime(TextBoxReturnDate.Text).Date);
                             if (_ser != null)
                             {
                                 foreach (HpServiceCharges _ser1 in _ser)
                                 {
                                     if (_ser1.Hps_from_val <= _days && _ser1.Hps_to_val >= _days)
                                     {
                                         _mgrChg = Math.Round(((Convert.ToDecimal(TextBoxReturnAmount.Text) * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                         _mgrChgCal = true;
                                         btnSave.Enabled = true;
                                         goto L3;
                                     }
                                 }
                             }
                         }
                     }
                     else
                     {
                         MessageBox.Show("Profit center hirarchy not set.", "Return Cheque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         btnSave.Enabled = false;
                         return;
                     }

                     if (_mgrChgCal == false)
                     {
                         MessageBox.Show("Manager issue cheque and cannot find manager charge definition.", "Return Cheque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         _mgrChg = 0;
                         lblMgrIssuChg.Text = string.Format("{0:n2}", _mgrChg);
                         btnSave.Enabled = false;
                         return;
                     }
                 }

             L3: Int16 i = 0;

                 lblMgrIssuChg.Text = string.Format("{0:n2}", _mgrChg);//total.ToString();
             }
         }

    }
}
