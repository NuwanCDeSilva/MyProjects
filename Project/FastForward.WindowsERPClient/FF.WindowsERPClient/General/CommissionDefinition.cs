using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;
using System.Configuration;
using System.Data.OleDb;

namespace FF.WindowsERPClient.General
{
    public partial class CommissionDefinition : Base
    {
        List<PriceBookLevelRef> PBList;
        List<CashCommissionDetailRef> ExcecList;
        List<CashCommissionDetailRef> ItemBrandCat_List;
        List<string> ClonePcList;
        DataTable PCList = null;

        public CommissionDefinition()
        {
            InitializeComponent();
            

            PBList = new List<PriceBookLevelRef>();
            ExcecList = new List<CashCommissionDetailRef>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            ClonePcList = new List<string>();
            PCList = new DataTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
     
                if (pnlClone.Visible)
                {

                    txtCloneAddPc.Text = "";
                    txtClonePC.Text = "";
                    dataGridViewPCClone.DataSource = null;
                    ClonePcList = new List<string>();
                    pnlClone.Visible = false;
                    tabControl1.Enabled = true;
                    toolStrip1.Enabled = true;
                    return;
                }
                else
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region validation
                try
                {
                    Decimal r1 = Convert.ToDecimal(txtCashComRt.Text);
                    Decimal r2 = Convert.ToDecimal(txtCrCdComRt.Text);
                    Decimal r3 = Convert.ToDecimal(txtCrCdProComRt.Text);
                    Decimal r4 = Convert.ToDecimal(txtChqComRt.Text);
                    Decimal r5 = Convert.ToDecimal(txtGVComRt.Text);
                    Decimal r6 = Convert.ToDecimal(txtDBCComRt.Text);
                    Decimal r7 = Convert.ToDecimal(txtOthComRt.Text);

                    Decimal r8 = Convert.ToDecimal(txtExCashComRt.Text);
                    Decimal r9 = Convert.ToDecimal(txtExCrCdComRt.Text);
                    Decimal r10 = Convert.ToDecimal(txtExCrCdProComRt.Text);
                    Decimal r11 = Convert.ToDecimal(txtExChqComRt.Text);
                    Decimal r12 = Convert.ToDecimal(txtExGVComRt.Text);
                    Decimal r13 = Convert.ToDecimal(txtExDBCComRt.Text);
                    Decimal r14 = Convert.ToDecimal(txtExOthComRt.Text);

                    Decimal tot = r1 + r2 + r3 + r4 + r5 + r6 + r7 + r8 + r9 + r10 + r11 + r12 + r13 + r14;
                    if (tot > 1400)
                    {
                        MessageBox.Show("Invalid Rate found. Any rate should be less or equal to 100!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("All the Rate and Amount values must be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtCircularCode.Text == "")
                {
                    MessageBox.Show("Please enter Circular code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (PBList.Count <= 0)
                {
                    MessageBox.Show("Please add Price Book details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (grvParty.Rows.Count <= 0)
                {
                    MessageBox.Show("Please add Profit Center details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtType.Text == "")
                {
                    MessageBox.Show("Please select sales type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dateTimePickerFrom.Value.Date > dateTimePickerTo.Value.Date)
                {
                    MessageBox.Show("From date has to be grater than to date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion


                List<string> Selected_PC_List = new List<string>();
                //Selected_PC_List = GetSelectedPCList();


                List<PriceBookLevelRef> Selected_PBook_List = new List<PriceBookLevelRef>();
                Selected_PBook_List = GetSelectedPriceBookList();

                List<CashCommissionDetailRef> Selected_ExcecutiveList = new List<CashCommissionDetailRef>();
                try
                {
                    Selected_ExcecutiveList = GetSelectedExcecutiveList();
                }
                catch (Exception ex) {
                    MessageBox.Show("Error occured while processing!!!\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<CashCommissionDetailRef> Selected_BrandCatItmList = new List<CashCommissionDetailRef>();
                Selected_BrandCatItmList = GetSelected_BrandCateItemList();
                if (Selected_BrandCatItmList == null || Selected_BrandCatItmList.Count<=0)
                {
                    if (MessageBox.Show("Are you want to save circular without Items,\nCircular will save to Price book and level?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                        return;
                    }

                    //MessageBox.Show("Please clear Category details list and add again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;
                }
                string ItmSelectType = dataGridViewItem.Columns[0].HeaderText;
                if (cmbSelectCat.SelectedItem == null || cmbSelectCat.SelectedItem.ToString() == "")
                {
                    ItmSelectType = "";
                }
                else if (ItmSelectType.ToUpper() != "VALUE")
                {
                    if (cmbSelectCat.SelectedItem.ToString() == "MAIN CATEGORY")
                    {
                        ItmSelectType = "CATE1";
                    }
                    else if (cmbSelectCat.SelectedItem.ToString() == "CATEGORY")
                    {
                        ItmSelectType = "CATE2";
                    }
                    else if (cmbSelectCat.SelectedItem.ToString() == "BRAND & M.CATEGORY")
                    {
                        ItmSelectType = "BRAND_CATE1";
                    }
                    else if (cmbSelectCat.SelectedItem.ToString() == "BRAND & CATEGORY")
                    {
                        ItmSelectType = "BRAND_CATE2";
                    }
                    else
                    {
                        ItmSelectType = cmbSelectCat.SelectedItem.ToString();
                    }
                }

                Dictionary<string, Decimal> commission_values = getCommissionValues();
                DateTime frmDT = dateTimePickerFrom.Value.Date;
                DateTime toDT = dateTimePickerTo.Value.Date;

                //------------------------------------------------------------
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                // masterAuto.Aut_cate_cd = GlbUserName;
                //masterAuto.Aut_cate_tp = "PC";
                //masterAuto.Aut_direction = 1;
                // masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "COMMIS";
                masterAuto.Aut_number = 0;//what is Aut_number
                masterAuto.Aut_start_char = "COMM";

                CashCommissionHeaderRef commHeader = new CashCommissionHeaderRef();
                commHeader.Scch_cd = null;
                commHeader.Scch_circular = txtCircularCode.Text.Trim().ToUpper();
                commHeader.Scch_cre_by = BaseCls.GlbUserID;
                commHeader.Scch_cre_dt = DateTime.Now.Date;
                commHeader.Scch_desc = txtDescription.Text.Trim().ToUpper();
                commHeader.Scch_sale_tp = txtType.Text.Trim().ToUpper();

                List<string> PartyList = new List<string>();
                grvParty.EndEdit();
                foreach (DataGridViewRow gvr in grvParty.Rows)
                {

                    bool duplicate = PartyList.Contains(gvr.Cells[1].Value.ToString());
                    if (!duplicate)
                    {
                        PartyList.Add(gvr.Cells[1].Value.ToString());
                    }

                }

                string _cusType = cmbType.Text;
                decimal _from = Convert.ToDecimal(txtAdQtyFrom.Text);
                decimal _to = Convert.ToDecimal(txtAdQtyTo.Text);
                int _isComb = chkSame.Checked?1:0;
                int _isEpf = chkAdAlowEPF.Checked?1:0;
                decimal _adAmt = Convert.ToDecimal(txtAdAmount.Text);


                //commHeader.Scch_seq = 0;
                string comm_code = "";
                Int32 effect = CHNLSVC.Sales.saveTempTablesForCommision(commHeader, PartyList, Selected_PBook_List, Selected_ExcecutiveList, Selected_BrandCatItmList, ItmSelectType, BaseCls.GlbUserID, txtCircularCode.Text.Trim().ToUpper(), commission_values, frmDT, toDT, masterAuto, out comm_code, DropDownListPartyTypes.SelectedValue.ToString(), _cusType, _from, _to, _isComb, _isEpf, _adAmt);

                if (effect > 0)
                {
                    MessageBox.Show("Process Completed! Commission Code: " + comm_code, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PBList = new List<PriceBookLevelRef>();
                    ExcecList = new List<CashCommissionDetailRef>();
                    ItemBrandCat_List = new List<CashCommissionDetailRef>();
                    btnClear_Click(null, null);
                    return;
                }
                else
                {
                    MessageBox.Show("Process Incomplete. Please try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        protected Dictionary<string, Decimal> getCommissionValues()
        {
            Dictionary<string, Decimal> commissionValues = new Dictionary<string, decimal>();
            commissionValues.Add("cashRt", Convert.ToDecimal(txtCashComRt.Text.Trim()));
            commissionValues.Add("cashAmt", Convert.ToDecimal(txtCashComAmt.Text.Trim()));

            commissionValues.Add("credCardRt", Convert.ToDecimal(txtCrCdComRt.Text.Trim()));
            commissionValues.Add("credCardAmt", Convert.ToDecimal(txtCrCdComAmt.Text.Trim()));

            commissionValues.Add("credCardProRt", Convert.ToDecimal(txtCrCdProComRt.Text.Trim()));
            commissionValues.Add("credCardProAmt", Convert.ToDecimal(txtCrCdProComAmt.Text.Trim()));

            commissionValues.Add("chequeRt", Convert.ToDecimal(txtChqComRt.Text.Trim()));
            commissionValues.Add("chequeAmt", Convert.ToDecimal(txtChqComAmt.Text.Trim()));

            commissionValues.Add("gvRt", Convert.ToDecimal(txtGVComRt.Text.Trim()));
            commissionValues.Add("gvAmt", Convert.ToDecimal(txtGVComAmt.Text.Trim()));

            commissionValues.Add("dbcRt", Convert.ToDecimal(txtDBCComRt.Text.Trim()));
            commissionValues.Add("dbcAmt", Convert.ToDecimal(txtDBCComAmt.Text.Trim()));

            commissionValues.Add("otherRt", Convert.ToDecimal(txtOthComRt.Text.Trim()));
            commissionValues.Add("otherAmt", Convert.ToDecimal(txtOthComAmt.Text.Trim()));

            commissionValues.Add("amountRt", Convert.ToDecimal("0"));
            commissionValues.Add("amountAmt", Convert.ToDecimal("0"));
            //------------------------------------------------------------------------------------
            commissionValues.Add("EXcashRt", Convert.ToDecimal(txtExCashComRt.Text.Trim()));
            commissionValues.Add("EXcashAmt", Convert.ToDecimal(txtExCashComAmt.Text.Trim()));

            commissionValues.Add("EXcredCardRt", Convert.ToDecimal(txtExCrCdComRt.Text.Trim()));
            commissionValues.Add("EXcredCardAmt", Convert.ToDecimal(txtExCrCdComAmt.Text.Trim()));

            commissionValues.Add("EXcredCardProRt", Convert.ToDecimal(txtExCrCdProComRt.Text.Trim()));
            commissionValues.Add("EXcredCardProAmt", Convert.ToDecimal(txtExCrCdProComAmt.Text.Trim()));

            commissionValues.Add("EXchequeRt", Convert.ToDecimal(txtExChqComRt.Text.Trim()));
            commissionValues.Add("EXchequeAmt", Convert.ToDecimal(txtExChqComAmt.Text.Trim()));

            commissionValues.Add("EXgvRt", Convert.ToDecimal(txtExGVComRt.Text.Trim()));
            commissionValues.Add("EXgvAmt", Convert.ToDecimal(txtExGVComAmt.Text.Trim()));

            commissionValues.Add("EXdbcRt", Convert.ToDecimal(txtExDBCComRt.Text.Trim()));
            commissionValues.Add("EXdbcAmt", Convert.ToDecimal(txtExDBCComAmt.Text.Trim()));

            commissionValues.Add("EXotherRt", Convert.ToDecimal(txtExOthComRt.Text.Trim()));
            commissionValues.Add("EXotherAmt", Convert.ToDecimal(txtExOthComAmt.Text.Trim()));

            commissionValues.Add("EXamountRt", Convert.ToDecimal("0"));
            commissionValues.Add("EXamountAmt", Convert.ToDecimal("0"));

            commissionValues.Add("AdcashRt", Convert.ToDecimal(txtAdCashComRt.Text.Trim()));
            commissionValues.Add("AdcashChk", chkAlwCash.Checked?1:0);

            commissionValues.Add("AdcredCardRt", Convert.ToDecimal(txtAdCrCdComRt.Text.Trim()));
            commissionValues.Add("AdcredCardChk", chkAlwCC.Checked ? 1 : 0);

            commissionValues.Add("AdcredCardProRt", Convert.ToDecimal(txtAdCrCdProComRt.Text.Trim()));
            commissionValues.Add("AdcredCardProChk", chkAlwCCPro.Checked ? 1 : 0);

            commissionValues.Add("AdchequeRt", Convert.ToDecimal(txtAdChqComRt.Text.Trim()));
            commissionValues.Add("AdchequeChk", chkAlwChq.Checked ? 1 : 0);

            commissionValues.Add("AdgvRt", Convert.ToDecimal(txtAdGVComRt.Text.Trim()));
            commissionValues.Add("AdgvChk", chkAlwGV.Checked ? 1 : 0);

            commissionValues.Add("AddbcRt", Convert.ToDecimal(txtAdDBCComRt.Text.Trim()));
            commissionValues.Add("AddbcChk", chkAlwDBC.Checked ? 1 : 0);

            commissionValues.Add("AdotherRt", Convert.ToDecimal(txtAdOthComRt.Text.Trim()));
            commissionValues.Add("AdotherChk", chkAlwOth.Checked ? 1 : 0);

            return commissionValues;
        }
        private List<CashCommissionDetailRef> GetSelectedExcecutiveList()
        {
            return ExcecList;
        }
        private List<PriceBookLevelRef> GetSelectedPriceBookList()
        {
            // List<PriceBookLevelRef> list = new List<PriceBookLevelRef>();
            return PBList;
        }
        

        private List<CashCommissionDetailRef> GetSelected_BrandCateItemList()
        {
            try
            {
                List<string> list = new List<string>();
                List<CashCommissionDetailRef> _tempLis = new List<CashCommissionDetailRef>();
                foreach (CashCommissionDetailRef _ref in ItemBrandCat_List)
                {
                    _tempLis.Add(_ref);
                }
                if (dataGridViewItem.Rows.Count <= 0) {
                    return ItemBrandCat_List;
                }
               // dataGridViewItem.EndEdit();
                List<CashCommissionDetailRef> _removeList = new List<CashCommissionDetailRef>();
                dataGridViewItem.EndEdit();
                foreach (DataGridViewRow gvr in dataGridViewItem.Rows)
                {
                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells[0];
                    string ITM_CD = gvr.Cells[1].Value.ToString().Trim();
                    string brand_="";
                    if(gvr.Cells[2].Value!=null)
                     brand_ = gvr.Cells[2].Value.ToString().Trim();
                    //if (chkSelect.Checked)
                    //{

                    //    list.Add(gvr.Cells[1].Text);
                    //}
                    if (chkSelect.Value == null || chkSelect.Value.ToString() == "false")
                    {
                        if (brand_ != "")
                        {
                            _removeList.AddRange(_tempLis.Where(x => x.Sccd_itm == ITM_CD && x.Sccd_brd == brand_).ToList<CashCommissionDetailRef>());
                           // ItemBrandCat_List.RemoveAll(x => x.Sccd_itm == ITM_CD && x.Sccd_brd == brand_);
                        }
                        else {
                            _removeList.AddRange(_tempLis.Where(x => x.Sccd_itm == ITM_CD).ToList<CashCommissionDetailRef>());
                            //ItemBrandCat_List.RemoveAll(x => x.Sccd_itm == ITM_CD);
                        }
                    }
                }
                if (_removeList != null && _removeList.Count > 0) {
                    foreach (CashCommissionDetailRef _det in _removeList) {
                        _tempLis.Remove(_det);
                    }
                }
                //dataGridViewItem.DataSource = null;
                return _tempLis;

               
            }
            catch (Exception) { return null; }
            
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            toolStrip1.Enabled = false;
            pnlClone.Visible = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    Clear();
                }
                else
                {
                    ClearSearch();
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

        private void ClearSearch()
        {
            txtSearchCircular.Text = "";
            dataGridViewSearch.DataSource = null;
        }

        private void Clear()
        {
            txtCashComAmt.Text = "";
            txtCashComRt.Text = "";
            txtChqComAmt.Text = "";
            txtChqComRt.Text = "";
            txtCircularCode.Text = "";
            txtCrCdComAmt.Text = "";
            txtCrCdComRt.Text = "";
            txtCrCdProComAmt.Text = "";
            txtCrCdProComRt.Text = "";
            txtDBCComAmt.Text = "";
            txtDBCComRt.Text = "";
            txtDescription.Text = "";
            txtExCashComAmt.Text = "";
            txtExCashComRt.Text = "";
            txtExcecCd.Text = "";
            txtExcecType.Text = "";
            txtExChqComAmt.Text = "";
            txtExChqComRt.Text = "";
            txtExCrCdComAmt.Text = "";
            txtExCrCdComRt.Text = "";
            txtExCrCdProComAmt.Text = "";
            txtExCrCdProComRt.Text = "";
            txtExDBCComAmt.Text = "";
            txtExDBCComRt.Text = "";
            txtExGVComAmt.Text = "";
            txtExGVComRt.Text = "";
            txtExOthComAmt.Text = "";
            txtExOthComRt.Text = "";
            txtGVComAmt.Text = "";
            txtGVComRt.Text = "";
            txtLevel.Text = "";
            txtOthComAmt.Text = "";
            txtOthComRt.Text = "";
            txtPriceBook.Text = "";
            txtType.Text = "";
            txtBrand.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            txtItemCD.Text = "";
            txtCircular.Text = "";
            txtSerial.Text = "";
            txtPromotion.Text = "";
            txtHierchCode.Text = "";

            txtAdAmount.Text = "";
            txtAdCashComRt.Text = "";
            txtAdChqComRt.Text = "";
            txtAdCrCdComRt.Text = "";
            txtAdCrCdProComRt.Text = "";
            txtAdDBCComRt.Text = "";
            txtAdGVComRt.Text = "";
            txtAdOthComRt.Text = "";
            txtAdQtyFrom.Text = "";
            txtAdQtyTo.Text = "";
            chkAdAlowEPF.Checked = false;
            chkAlwCash.Checked = false;
            chkAlwCC.Checked = false;
            chkAlwCCPro.Checked = false;
            chkAlwChq.Checked= false;
            chkAlwDBC.Checked= false;
            chkAlwGV.Checked= false;
            chkAlwOth.Checked = false;
            chkSame.Checked = false;


            DateTime _date=CHNLSVC.Security.GetServerDateTime();
            dateTimePickerFrom.Value = _date;
            dateTimePickerTo.Value = _date;


            dataGridViewItem.DataSource = null;
            grvParty.DataSource = null;
            dataGridViewPriceBook.DataSource = null;
            dataGridViewExcecutive.DataSource = null;

            PBList = new List<PriceBookLevelRef>();
            ExcecList = new List<CashCommissionDetailRef>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            cmbSelectCat.SelectedIndex = 0;
            resetCommisionTextBoxes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region search events


        private void btnSearchType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable _result = CHNLSVC.General.GetSalesTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtType;
                _CommonSearch.txtSearchbyword.Text = txtType.Text;
                _CommonSearch.ShowDialog();
                txtType.Focus();
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

        private void btnSearchPB_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPriceBook;
                _CommonSearch.txtSearchbyword.Text = txtPriceBook.Text;
                _CommonSearch.ShowDialog();
                txtPriceBook.Focus();
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


        private void btnSearchPriceLvl_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLevel;
                _CommonSearch.txtSearchbyword.Text = txtLevel.Text;
                _CommonSearch.ShowDialog();
                txtLevel.Focus();
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

        private void btnSearchExeType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeCate);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_categories(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtExcecType;
                _CommonSearch.txtSearchbyword.Text = txtExcecType.Text;
                _CommonSearch.ShowDialog();
                txtExcecType.Focus();
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

        private void btnSearchExeCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeEPF);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_EPF(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtExcecCd;
                _CommonSearch.txtSearchbyword.Text = txtExcecCd.Text;
                _CommonSearch.ShowDialog();
                txtExcecCd.Focus();
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

        #region item search

        private void btnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;
                _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();
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

        private void btnMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate1;
                _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
                _CommonSearch.ShowDialog();
                txtCate1.Focus();
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

        private void btnCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate2;
                _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
                _CommonSearch.ShowDialog();
                txtCate2.Focus();
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

        private void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD;
                _CommonSearch.txtSearchbyword.Text = txtItemCD.Text;
                _CommonSearch.ShowDialog();
                txtItemCD.Focus();
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

        private void btnSerial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 6;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSerialSearchData(_CommonSearch.SearchParams);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial;
                _CommonSearch.txtSearchbyword.Text = txtSerial.Text;
                _CommonSearch.ShowDialog();
                txtSerial.Focus();
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

        private void btnCircular_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircular;
                _CommonSearch.txtSearchbyword.Text = txtCircular.Text;
                _CommonSearch.ShowDialog();
                txtCircular.Focus();
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

        private void btnPromation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotion;
                _CommonSearch.txtSearchbyword.Text = txtPromotion.Text;
                _CommonSearch.ShowDialog();
                txtPromotion.Focus();
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

        private void btnPCClone_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtClonePC;
                _CommonSearch.txtSearchbyword.Text = txtClonePC.Text;
                _CommonSearch.ShowDialog();
                txtClonePC.Focus();
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

        private void btnPCClone1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCloneAddPc;
                _CommonSearch.txtSearchbyword.Text = txtCloneAddPc.Text;
                _CommonSearch.ShowDialog();
                txtCloneAddPc.Focus();
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

        private void btnSearchCirculr_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashCommissionCircular);
                DataTable _result = CHNLSVC.CommonSearch.GetCashCommissionCircularNo(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSearchCircular;
                _CommonSearch.txtSearchbyword.Text = txtSearchCircular.Text;
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

        #endregion

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
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append("" + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(txtItemCD.Text + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append("" + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                       paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeCate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeEPF:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtExcecType.Text.Trim().ToUpper());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashCommissionCircular:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromotionalDiscount:
                    {
                        paramsText.Append(DateTime.Now.ToString("dd/MM/yyyy") + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion


        #region check number only

        private void txtCashComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCrCdComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCrCdProComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtChqComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtGVComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtDBCComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtOthComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCashComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCrCdComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCrCdProComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtChqComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtGVComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtDBCComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtOthComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExCashComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExCrCdComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExCrCdProComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExChqComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExGVComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExDBCComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExOthComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExCashComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExCrCdComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExCrCdProComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExChqComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExGVComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExDBCComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtExOthComAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        #endregion

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x0100 && (int)m.WParam == 13)
            {
                this.ProcessTabKey(true);
            }
            return base.ProcessKeyPreview(ref m);
        }

        private void btnAddPB_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPriceBook.AutoGenerateColumns = false;
                List<PriceBookLevelRef> pbLIST = new List<PriceBookLevelRef>();
                if (txtLevel.Text != "" && txtPriceBook.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPriceBook.Text && _res.Sapl_pb_lvl_cd == txtLevel.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book, level already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (txtPriceBook.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPriceBook.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                pbLIST = (CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, txtPriceBook.Text.Trim().ToUpper(), txtLevel.Text.Trim().ToUpper()));

                pbLIST.RemoveAll(x => x.Sapl_act == false);
                var distinctList = pbLIST.GroupBy(x => x.Sapl_pb_lvl_cd)
                             .Select(g => g.First())
                             .ToList();

                PBList.AddRange(distinctList);





                BindingSource source = new BindingSource();
                source.DataSource = PBList;
                dataGridViewPriceBook.DataSource = source;
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

        private void dataGridViewPriceBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PBList.RemoveAt(e.RowIndex);
                        BindingSource source = new BindingSource();
                        source.DataSource = PBList;
                        dataGridViewPriceBook.DataSource = source;
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

        private void btnAddExe_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewExcecutive.AutoGenerateColumns = false;
                CashCommissionDetailRef ccd = new CashCommissionDetailRef();
                if (txtExcecType.Text == "")
                {
                    MessageBox.Show("Please select Executive type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ccd.Sccd_exec_tp = txtExcecType.Text.Trim();
                ccd.Sccd_exec_cd = txtExcecCd.Text.Trim();
                //TODO: NEED VALIDATION?
                ExcecList.Add(ccd);
                BindingSource source = new BindingSource();
                source.DataSource = ExcecList;
                dataGridViewExcecutive.DataSource = source;
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



       

        

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSelectCat.SelectedItem == null || cmbSelectCat.SelectedItem.ToString() == "") {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2")
                {
                    if (txtBrand.Text == string.Empty)
                    {
                        MessageBox.Show("Specify brand also!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string selection = "";
                if (cmbSelectCat.SelectedItem.ToString() == "MAIN CATEGORY") {
                    selection = "CATE1";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "CATEGORY")
                {
                    selection = "CATE2";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "BRAND & M.CATEGORY")
                {
                    selection = "BRAND_CATE1";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "BRAND & CATEGORY")
                {
                    selection = "BRAND_CATE2";
                }
                else {
                    selection = cmbSelectCat.SelectedItem.ToString();
                }

                List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
               
                if (selection != "PROMOTIONL DISCOUNT")
                {

                    //ItemBrandCat_List = new List<CashCommissionDetailRef>();
                    DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), null, txtItemCD.Text.Trim(), txtSerial.Text.Trim(), txtCircular.Text.Trim(), txtPromotion.Text.Trim());

                    
                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewItem.Columns[1].HeaderText = cmbSelectCat.SelectedItem.ToString();
                    }
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        string code = dr["code"].ToString();
                        string brand = txtBrand.Text;
                        CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                        if (selection == "BRAND_CATE1" || selection == "BRAND_CATE2")
                        {
                            obj.Sccd_brd = brand;
                        }
                        else
                        {
                            obj.Sccd_brd = "N/A";
                        }

                        obj.Sccd_itm = code;
                        try
                        {
                            obj.Sccd_ser = dr["descript"].ToString();
                        }
                        catch (Exception)
                        {
                            obj.Sccd_ser = "";
                        }

                        var _duplicate = from _dup in ItemBrandCat_List
                                         where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                                         select _dup;
                        if (_duplicate.Count() == 0)
                        {
                            addList.Add(obj);
                        }

                    }
                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewItem.Columns[1].HeaderText = cmbSelectCat.SelectedItem.ToString();
                    }
                }
                //PROMOTIONL DISCOUNT
                else
                {
                    CashCommissionDetailRef obj = new CashCommissionDetailRef();
                    obj.Sccd_itm = txtPromotionalDiscount.Text;
                    var _duplicate = from _dup in ItemBrandCat_List
                                     where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                                     select _dup;
                    if (_duplicate.Count() == 0)
                    {
                        addList.Add(obj);
                    }
                }
                ItemBrandCat_List.AddRange(addList);
                BindingSource source = new BindingSource();
                source.DataSource = ItemBrandCat_List;
                dataGridViewItem.AutoGenerateColumns = false;
                dataGridViewItem.DataSource = source;

                foreach (DataGridViewRow gr in dataGridViewItem.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewItem.Rows[gr.Index].Cells[0];
                    chk.Value = "true";
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

        private void btnItemAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in dataGridViewItem.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewItem.Rows[gr.Index].Cells[0];
                chk.Value = "true";
            }
        }

        private void btnItemNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in dataGridViewItem.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewItem.Rows[gr.Index].Cells[0];
                chk.Value = "false";
            }
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            dataGridViewItem.DataSource = null;
        }
        private void resetCommisionTextBoxes()
        {
            txtCashComRt.Text = Convert.ToString(0);
            txtCashComAmt.Text = Convert.ToString(0);

            txtCrCdComRt.Text = Convert.ToString(0);
            txtCrCdComAmt.Text = Convert.ToString(0);

            txtCrCdProComRt.Text = Convert.ToString(0);
            txtCrCdProComAmt.Text = Convert.ToString(0);

            txtChqComRt.Text = Convert.ToString(0);
            txtChqComAmt.Text = Convert.ToString(0);

            txtGVComRt.Text = Convert.ToString(0);
            txtGVComAmt.Text = Convert.ToString(0);

            txtDBCComRt.Text = Convert.ToString(0);
            txtDBCComAmt.Text = Convert.ToString(0);

            txtOthComRt.Text = Convert.ToString(0);
            txtOthComAmt.Text = Convert.ToString(0);

            txtExCashComRt.Text = Convert.ToString(0);
            txtExCashComAmt.Text = Convert.ToString(0);

            txtExCrCdComRt.Text = Convert.ToString(0);
            txtExCrCdComAmt.Text = Convert.ToString(0);

            txtExCrCdProComRt.Text = Convert.ToString(0);
            txtExCrCdProComAmt.Text = Convert.ToString(0);

            txtExChqComRt.Text = Convert.ToString(0);
            txtExChqComAmt.Text = Convert.ToString(0);

            txtExGVComRt.Text = Convert.ToString(0);
            txtExGVComAmt.Text = Convert.ToString(0);

            txtExDBCComRt.Text = Convert.ToString(0);
            txtExDBCComAmt.Text = Convert.ToString(0);

            txtExOthComRt.Text = Convert.ToString(0);
            txtExOthComAmt.Text = Convert.ToString(0);

            txtAdCashComRt.Text = Convert.ToString(0);
            txtAdCrCdComRt.Text = Convert.ToString(0);
            txtAdCrCdProComRt.Text = Convert.ToString(0);
            txtAdChqComRt.Text = Convert.ToString(0);
            txtAdGVComRt.Text = Convert.ToString(0);
            txtAdDBCComRt.Text = Convert.ToString(0);
            txtAdOthComRt.Text = Convert.ToString(0);

            txtAdAmount.Text = Convert.ToString(0);
            txtAdQtyFrom.Text = Convert.ToString(0);
            txtAdQtyTo.Text = Convert.ToString(0);

        }

        private void CommissionDefinition_Load(object sender, EventArgs e)
        {
            try
            {
                resetCommisionTextBoxes();
                cmbSelectCat.SelectedIndex = 0;
                BindPartyType();
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

        public void BindPartyType()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }

        private void btnInvCombineSerClose_Click(object sender, EventArgs e)
        {
            pnlClone.Visible = false;
            tabControl1.Enabled = true;
            toolStrip1.Enabled = true;
            txtCloneAddPc.Text = "";
            txtClonePC.Text = "";
            dataGridViewPCClone.DataSource = null;
            ClonePcList = new List<string>();
        }

        private void linkLabelProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (ClonePcList.Count > 0)
                {
                    Int32 eff = CHNLSVC.Sales.Save_CloneCommissions(txtClonePC.Text.Trim().ToUpper(), ClonePcList, BaseCls.GlbUserID);
                    if (eff > 0)
                    {
                        MessageBox.Show("Cloning Completed Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlClone.Visible = false;
                        tabControl1.Enabled = true;
                        toolStrip1.Enabled = true;
                        txtCloneAddPc.Text = "";
                        txtClonePC.Text = "";
                        dataGridViewPCClone.DataSource = null;
                        ClonePcList = new List<string>();
                    }
                    else
                    {
                        MessageBox.Show("Sorry. Failed to complete. Please try again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please add profit centers to the cloning list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCloneAddPc.Text = "";
            txtClonePC.Text = "";
            dataGridViewPCClone.DataSource = null;
            ClonePcList = new List<string>();
        }

        private bool VaildateProfitCenter(string _code) {
            MasterProfitCenter pc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, _code);
            if (pc != null)
            {
                return true;
            }
            else
                return false;
        }

        private void btnAddClone_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPCClone.AutoGenerateColumns = false;
                //validate pc
                if (!VaildateProfitCenter(txtClonePC.Text))
                {
                    MessageBox.Show("Invalid Profit Center Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!VaildateProfitCenter(txtCloneAddPc.Text))
                {
                    MessageBox.Show("Invalid Clone Profit Center Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //check duplicates
                List<string> tem = (from _res in ClonePcList
                                    where _res == txtCloneAddPc.Text
                                    select _res).ToList<string>();
                if (tem != null && tem.Count > 0)
                {
                    MessageBox.Show("Profit Center " + txtCloneAddPc.Text + " Already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    ClonePcList.Add(txtCloneAddPc.Text.Trim().ToUpper());
                    var pcs = (from _res in ClonePcList
                               select new { PC = _res });

                    BindingSource source = new BindingSource();
                    source.DataSource = pcs.ToList();
                    dataGridViewPCClone.DataSource = source;
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

        private void dataGridViewPCClone_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0) {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    ClonePcList.RemoveAt(e.RowIndex);
                    var pcs = (from _res in ClonePcList
                               select new { PC = _res });

                    BindingSource source = new BindingSource();
                    source.DataSource = pcs.ToList();
                    dataGridViewPCClone.DataSource = source;              
                }
            }
        }

        private void ShowButtons() {
            btnSave.Visible = true;
            btnClone.Visible = true;

            toolStripSeparator2.Visible = true;
            toolStripSeparator3.Visible = true;
        }

        private void HideButtons() {
            btnSave.Visible = false;
            btnClone.Visible = false;

            toolStripSeparator2.Visible = false;
            toolStripSeparator3.Visible = false;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                ShowButtons();
            }
            else {
                HideButtons();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchCircular.Text == "")
                {
                    MessageBox.Show("Please enter circular code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                dataGridViewSearch.AutoGenerateColumns = false;
                dataGridViewSearch.DataSource = CHNLSVC.Sales.GetCashCommissionserach(txtSearchCircular.Text);
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

        private void btnBrItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadItems.Text = openFileDialog1.FileName;
        }

        private void btnUploadItem_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(txtUploadItems.Text))
            {
                MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Text = "";
                txtUploadItems.Focus();
                return;
            }

            if (cmbSelectCat.SelectedItem.ToString() == "MAIN CATEGORY")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedItem.ToString() == "CATEGORY")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedItem.ToString() == "BRAND & M.CATEGORY")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedItem.ToString() == "BRAND & M.CATEGORY")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedItem.ToString() == "BRAND")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadItems.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Focus();
            }

            string Extension = fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }


            conStr = String.Format(conStr, txtUploadItems.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in dt.Rows)
                {
                    //validation
                     if (cmbSelectCat.SelectedItem.ToString() == "ITEM")
                    {
                        if (string.IsNullOrEmpty(_dr[0].ToString())) {
                            continue;
                        }

                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString().Trim());
                        if (_item == null) {
                            MessageBox.Show("Invalid Item - " + _dr[0].ToString(), "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }
                    }

                     List<CashCommissionDetailRef> _duplicate = (from _res in ItemBrandCat_List
                                                           where _res.Sccd_itm == _dr[0].ToString()
                                                           select _res).ToList<CashCommissionDetailRef>();
                     if (_duplicate != null && _duplicate.Count > 0) {
                         MessageBox.Show("Item " + _dr[0].ToString() + " Duplicate in Excel", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         continue;
                     }

                    CashCommissionDetailRef _ref = new CashCommissionDetailRef();
                    _ref.Sccd_itm = _dr[0].ToString();
                    ItemBrandCat_List.Add(_ref);
                }
            }
            dataGridViewItem.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = ItemBrandCat_List;

            dataGridViewItem.DataSource = _source;
        }

        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();


                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }

                else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);

                        //if comm exe avialle or not
                        if (rdoCommExeAva.Checked)
                        {
                            //get pc com exe count
                            //count >0  add
                            //else remove
                            DataTable _tem = new DataTable();
                            _tem.TableName = "aa";
                            _tem.Columns.Add("Code");
                            _tem.Columns.Add("Description");
                            for (int I = 0; I < _result.Rows.Count; I++)
                            {
                                string _pc = _result.Rows[I]["Code"].ToString();
                                DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(BaseCls.GlbUserComCode, _pc);
                                if (_tblExecutive != null && _tblExecutive.Rows.Count > 0)
                                {
                                    var _ceCount = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") == "CE").ToList();
                                    if (_ceCount.Count > 0)
                                    {
                                        DataRow _dr = _tem.NewRow();
                                        _dr[0] = _result.Rows[I][0].ToString();
                                        _dr[1] = _result.Rows[I][1].ToString();
                                        _tem.Rows.Add(_dr);
                                    }
                                }
                            }
                            _result = new DataTable();
                            _result = _tem;
                        }
                        if (rdoCommExeNot.Checked)
                        {
                            //get pc com exe count
                            //count >0  remove
                            //else add
                            DataTable _tem = new DataTable();
                            _tem.TableName = "aa";
                            _tem.Columns.Add("Code");
                            _tem.Columns.Add("Description");
                            for (int I = 0; I < _result.Rows.Count; I++)
                            {
                                string _pc = _result.Rows[I]["Code"].ToString();
                                DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(BaseCls.GlbUserComCode, _pc);
                                if (_tblExecutive != null && _tblExecutive.Rows.Count > 0)
                                {
                                    var _ceCount = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") == "CE").ToList();
                                    if (_ceCount.Count <= 0)
                                    {
                                        DataRow _dr = _tem.NewRow();
                                        _dr[0] = _result.Rows[I][0].ToString();
                                        _dr[1] = _result.Rows[I][1].ToString();
                                        _tem.Rows.Add(_dr);
                                    }
                                }
                            }
                            _result = new DataTable();
                            _result = _tem;

                        }
                    }
                    else {
                        //if comm exe avialle or not
                        if (rdoCommExeAva.Checked)
                        {
                            //get pc com exe count
                            //count >0  add
                            //else remove
                            DataTable _tem = new DataTable();
                            _tem.TableName = "aa";
                            _tem.Columns.Add("Code");
                            _tem.Columns.Add("Description");
                            for (int I = 0; I < _result.Rows.Count; I++)
                            {
                                string _pc = _result.Rows[I]["Code"].ToString();
                                DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(BaseCls.GlbUserComCode, _pc);
                                if (_tblExecutive != null && _tblExecutive.Rows.Count > 0)
                                {
                                    var _ceCount = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") == "CE").ToList();
                                    if (_ceCount.Count > 0)
                                    {

                                        DataRow _dr = _tem.NewRow();
                                        _dr[0] = _result.Rows[I][0].ToString();
                                        _dr[1] = _result.Rows[I][1].ToString();
                                        _tem.Rows.Add(_dr);
                                    }
                                }
                            }
                            _result = new DataTable();
                            _result = _tem;
                        }
                        if (rdoCommExeNot.Checked)
                        {
                            //get pc com exe count
                            //count >0  remove
                            //else add
                            DataTable _tem = new DataTable();
                            _tem.TableName = "aa";
                            _tem.Columns.Add("Code");
                            _tem.Columns.Add("Description");
                            for (int I = 0; I < _result.Rows.Count; I++)
                            {
                                string _pc = _result.Rows[I]["Code"].ToString();
                                DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(BaseCls.GlbUserComCode, _pc);
                                if (_tblExecutive != null && _tblExecutive.Rows.Count > 0)
                                {
                                    var _ceCount = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") == "CE").ToList();
                                    if (_ceCount.Count <= 0)
                                    {
                                        DataRow _dr = _tem.NewRow();
                                        _dr[0] = _result.Rows[I][0].ToString();
                                        _dr[1] = _result.Rows[I][1].ToString();
                                        _tem.Rows.Add(_dr);
                                    }
                                }
                            }
                            _result = new DataTable();
                            _result = _tem;

                        }
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode.Text, "");
                    }
                }
                if (_result == null || _result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                PCList.Merge(_result);

                BindingSource _source = new BindingSource();
                _source.DataSource = PCList;

                grvParty.DataSource = null;
                grvParty.AutoGenerateColumns = false;
                grvParty.DataSource = _source;
                // chkPCAll.Checked = true;
                //AllClick();
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

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            PCList = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
            {
                PNLce.Visible = true;
            }
            else
                PNLce.Visible = false;
        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }

                else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode;
                _CommonSearch.ShowDialog();
                txtHierchCode.Focus();
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

        private void grvParty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PCList.Rows.RemoveAt(e.RowIndex);
                        grvParty.AutoGenerateColumns = false;
                        grvParty.DataSource = PCList;
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

        private void cmbSelectCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCD.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            
            txtSerial.Text = "";
            txtPromotion.Text = "";
            dataGridViewItem.DataSource = null;
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
        }

        private void btnPromotionlDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionalDiscount);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionlDiscountHeader(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotionalDiscount;
                _CommonSearch.txtSearchbyword.Text = txtPromotionalDiscount.Text;
                _CommonSearch.ShowDialog();
                txtPromotionalDiscount.Focus();
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

        private void txtAdRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdCashComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

        }

        private void txtAdCrCdComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdCrCdProComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdChqComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdGVComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdDBCComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdOthComRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdQtyFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAdQtyTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void CheckRate(TextBox _txt) {

            decimal _rate = 0;
            if (!decimal.TryParse(_txt.Text, out _rate)) {
                MessageBox.Show("Invalid Rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txt.Text="0";
                return;
            }
            if (_rate < 0 || _rate > 100) {
                MessageBox.Show("Rate shoud between 0 and 100", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txt.Text = "0";
                return;
            }
        
        }

        private void CheckAmount(TextBox _txt) {
            decimal _amount = 0;
            if (!decimal.TryParse(_txt.Text, out _amount))
            {
                MessageBox.Show("Invalid Rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txt.Text = "0";
                return;
            }
            if (_amount<0)
            {
                MessageBox.Show("Amount cannot be minus value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txt.Text = "0";
                return;
            }
        
        }

        private void txtAdCashComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtAdCashComRt);
        }

        private void txtAdCrCdComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtAdCrCdComRt);
        }

        private void txtAdCrCdProComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtAdCrCdProComRt);
        }

        private void txtAdChqComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtAdChqComRt);
        }

        private void txtAdGVComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtAdGVComRt);
        }

        private void txtAdDBCComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtAdDBCComRt);
        }

        private void txtAdOthComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtAdOthComRt);
        }

        private void txtCashComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtCashComRt);
        }

        private void txtCrCdComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtCrCdComRt);
        }

        private void txtCrCdProComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtCrCdProComRt);
        }

        private void txtChqComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtChqComRt);
        }

        private void txtGVComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtGVComRt);
        }

        private void txtDBCComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtDBCComRt);
        }

        private void txtOthComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtOthComRt);
        }

        private void txtExCashComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtExCashComRt);
        }

        private void txtExCrCdComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtExCrCdComRt);
        }

        private void txtExCrCdProComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtExCrCdProComRt);
        }

        private void txtExChqComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtExChqComRt);
        }

        private void txtExGVComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtExGVComRt);
        }

        private void txtExDBCComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtExDBCComRt);
        }

        private void txtExOthComRt_Leave(object sender, EventArgs e)
        {
            CheckRate(txtExOthComRt);
        }

        private void txtCashComAmt_Leave(object sender, EventArgs e)
        {
            CheckAmount(txtCashComAmt);
        }

        private void txtExCashComAmt_Leave(object sender, EventArgs e)
        {
            CheckAmount(txtExCashComAmt);
        }

        private void txtAdAmount_Leave(object sender, EventArgs e)
        {
            CheckAmount(txtAdAmount);
        }

        private void txtAdQtyTo_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtAdQtyFrom.Text) > Convert.ToDecimal(txtAdQtyTo.Text)) {
                MessageBox.Show("From qty can not be grater than to qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdQtyFrom.Text = "0";
                txtAdQtyTo.Text = "0";
                return;
            
            }
        }

        private void btn_srch_edit_circ_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashCommissionCircular);
                DataTable _result = CHNLSVC.CommonSearch.GetCashCommissionCircularNo(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircularEdit;
                _CommonSearch.txtSearchbyword.Text = txtCircularEdit.Text;
                _CommonSearch.ShowDialog();

                DataTable _dtComDet = CHNLSVC.Sales.GetCashCommissionserach(txtCircularEdit.Text);
                if (_dtComDet.Rows.Count != 0)
                {
                    dtFromEdit.Value = Convert.ToDateTime(_dtComDet.Rows[0]["sccd_from_dt"]);
                    dtToEdit.Value = Convert.ToDateTime(_dtComDet.Rows[0]["sccd_to_dt"]);
                    txtComCd.Text = _dtComDet.Rows[0]["sccd_cd"].ToString();
                }
                else
                {
                    txtComCd.Text = "";
                    MessageBox.Show("Data not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtCircularEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_edit_circ_Click(null, null);
        }

        private void btnEditDt_Click(object sender, EventArgs e)
        {
            if (dtToEdit.Value.Date.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("Invalid To Date !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            Int32 _eff = CHNLSVC.General.UpdateCashComEndDate(txtComCd.Text, dtToEdit.Value.Date,BaseCls.GlbUserID);
            MessageBox.Show("Successfully Updated !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



       




    }
}
