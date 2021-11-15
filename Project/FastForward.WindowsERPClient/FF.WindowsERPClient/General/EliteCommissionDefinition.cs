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
    public partial class EliteCommissionDefinition : Base
    {
        #region properties

        DataTable PCList = null;
        List<EliteCommissionDetail> MgrList;
        List<EliteCommissionDetail> ExeList;
        List<EliteCommissionDetail> CashierList;
        List<EliteCommissionDetail> HelperList;
        List<EliteCommissionDetail> CoHeadList;
        List<EliteCommissionDetail> OtherList;
        List<EliteCommissionAdditional> AdditionalList;
        List<EliteCommissionIgnore> IgnoreList;
        List<CashCommissionDetailRef> ItemBrandCat_List;
        List<MasterInvoiceType> SalesType;
        int mgrLine = 0;
        int exeline = 0;
        int othLine = 0;
        #endregion


        public EliteCommissionDefinition()
        {
            InitializeComponent();
            MgrList = new List<EliteCommissionDetail>();
            ExeList = new List<EliteCommissionDetail>();
            OtherList = new List<EliteCommissionDetail>();
            AdditionalList = new List<EliteCommissionAdditional>();
            IgnoreList = new List<EliteCommissionIgnore>();
            CashierList = new List<EliteCommissionDetail>();
            HelperList = new List<EliteCommissionDetail>();
            CoHeadList = new List<EliteCommissionDetail>();
            ItemBrandCat_List=new List<CashCommissionDetailRef>();
            SalesType = new List<MasterInvoiceType>();
           
        }

        public void BindPartyType()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            //PartyTypes.Add("GPC", "GPC");
            //PartyTypes.Add("CHNL", "Channel");
            //PartyTypes.Add("SCHNL", "Sub Channel");
            //PartyTypes.Add("AREA", "Area");
            //PartyTypes.Add("REGION", "Region");
            //PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }

        private void EliteCommissionDefinition_Load(object sender, EventArgs e)
        {
            try
            {
                BindPartyType();
                BindSalesTypes();
                BindCategoryTypes();
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
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.EliteCircular:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator );
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeCate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeSubCategory:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
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
                if (PCList == null)
                    PCList = _result;
                else
                    PCList.Merge(_result);
                //PCList = _result;
                grvParty.DataSource = null;
                grvParty.AutoGenerateColumns = false;
                grvParty.DataSource = PCList;
                txtHierchCode.Text = "";
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

        private void Process()
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                    return;
                }

                if (string.IsNullOrEmpty(txtCircularNo.Text))
                {
                    MessageBox.Show("Circular Number can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MgrList == null || MgrList.Count <= 0 && OtherList.Count<=0) {
                    MessageBox.Show("Manager do not have definition", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ExeList == null || ExeList.Count <= 0 && OtherList.Count <= 0)
                {
                    MessageBox.Show("Executive do not have definition", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (CashierList == null || CashierList.Count <= 0) {
                //    MessageBox.Show("Cashier do not have definition", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //if (HelperList == null || HelperList.Count <= 0) {
                //    MessageBox.Show("Helper do not have definition", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //if (CoHeadList == null || CoHeadList.Count <= 0) {
                //    MessageBox.Show("Co-head do not have definition", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                if (grvParty.Rows.Count <= 0) {
                    MessageBox.Show("At least one profit center need to process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (SalesType == null || SalesType.Count <= 0) {
                    MessageBox.Show("At least one sales type need to process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //header fill
                FF.BusinessObjects.EliteCommissionDefinition _def = new BusinessObjects.EliteCommissionDefinition();
                _def.Saec_valid_from = dtFrom.Value.Date;
                _def.Saec_valid_to = dtTo.Value.Date;
                _def.Saec_cre_by = BaseCls.GlbUserID;
                _def.Saec_cre_dt = DateTime.Now;
                _def.Saec_mod_by = BaseCls.GlbUserID;
                _def.Saec_mod_dt = DateTime.Now;
                _def.Saec_stus = "A";
                _def.Eaec_com = BaseCls.GlbUserComCode;
                _def.Saec_no = txtCircularNo.Text;
                try
                {
                    _def.Saec_alw_discount = Convert.ToDecimal(textBox1.Text);
                }
                catch (Exception) {
                    MessageBox.Show("Discount rate has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //detail fill
                /*
                01.Manager
                02.Executive
                03.Cashier
                04.Helper
                05.Ch_Head
                 */
                List<EliteCommissionDetail> _detailList = new List<EliteCommissionDetail>();
                _detailList.AddRange(MgrList);
                _detailList.AddRange(ExeList);
                _detailList.AddRange(CashierList);
                _detailList.AddRange(HelperList);
                _detailList.AddRange(CoHeadList);
                _detailList.AddRange(OtherList);
                List<EliteCommissionPrty> _prtyList = new List<EliteCommissionPrty>();
                foreach (DataGridViewRow gvr in grvParty.Rows)
                {
                    EliteCommissionPrty _prty = new EliteCommissionPrty();
                    _prty.Saec_prt_tp = DropDownListPartyTypes.SelectedValue.ToString();
                    _prty.Saec_prt_cd = gvr.Cells[1].Value.ToString();
                    _prtyList.Add(_prty);
                }

                MasterAutoNumber _auto = new MasterAutoNumber();
                _auto.Aut_cate_tp = "PC";
                _auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _auto.Aut_direction = 0;
                _auto.Aut_moduleid = "COMM";
                _auto.Aut_start_char = "COMM";

                List<CashCommissionDetailRef> _itemList = new List<CashCommissionDetailRef>();
                //dataGridViewItem1.EndEdit();
                for (int i = 0; i < dataGridViewItem.Rows.Count; i++)
                {
                    dataGridViewItem.EndEdit();
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridViewItem.Rows[i].Cells[0];
                    if (cell.Value.ToString().ToUpper() == "TRUE")
                    {
                        _itemList.Add(ItemBrandCat_List[i]);
                    }
                }
                string _itemType = "";
                if (cmbSelectCat.SelectedValue != null)
                    _itemType = cmbSelectCat.SelectedValue.ToString();

                List<EliteCommissionSalesTypes> _salesType = new List<EliteCommissionSalesTypes>();
                foreach(MasterInvoiceType sal in  SalesType){
                    EliteCommissionSalesTypes _type = new EliteCommissionSalesTypes();
                    _type.Saec_sales_type = sal.Srtp_cd;
                    _salesType.Add(_type);
                }
                string err;
                bool type;
                if (rdoInclude.Checked)
                {
                    type = true;
                }
                else {
                    type = false;
                }

                int result = CHNLSVC.Sales.SaveEliteCommissionDefinition(_def, _detailList, _prtyList, AdditionalList, IgnoreList, _itemList, _itemType, _auto, _salesType,out err,type);
                if (string.IsNullOrEmpty( err))
                {
                    MessageBox.Show("Successfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Save Unsuccessful\n"+err, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                CHNLSVC.CloseChannel(); 
            }

        }

        private void btnMgrAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string type = "";
                if (rdoBelow.Checked)
                    type = "BE";
                if (rdoTarget.Checked)
                    type = "TA";
                if (rdoAfter.Checked)
                    type = "AF";

                EliteCommissionDetail _det = new EliteCommissionDetail();
                _det.Saec_emp_type = "SRMGR";
                _det.Saec_from = Convert.ToDecimal(txtMgrFrom.Text);
                _det.Saec_to = Convert.ToDecimal(txtMgrTo.Text);
                _det.Saec_type = type;
                _det.Saec_line_no = ++mgrLine;
                _det.Saec_value = Convert.ToDecimal(txtMgrValue.Text);
                _det.Saec_rate = Convert.ToDecimal(txtMgrRate.Text);

                MgrList.Add(_det);
                LoadMgrGrid();
                if (rdoAfter.Checked)
                {
                    txtMgrFrom.Text = (MgrList[MgrList.Count - 1].Saec_to + 1).ToString();
                    txtMgrTo.Text = (Convert.ToDecimal(txtMgrFrom.Text)-1+200000).ToString();
                    txtMgrRate.Text = "0";
                    txtMgrValue.Text = "0";
                }
                else
                {
                    txtMgrFrom.Text = "0";
                    txtMgrTo.Text = "0";
                    txtMgrRate.Text = "0";
                    txtMgrValue.Text = "0";
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                CHNLSVC.CloseChannel(); 
            }
        }

        private void LoadMgrGrid()
        {
            grvMgr.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = MgrList;
            grvMgr.DataSource = _source;
        }

        private void btnExeAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string type = "";
                if (rdoExeBelow.Checked)
                    type = "BE";
                if (rdoExeTarget.Checked)
                    type = "TA";
                if (rdoExeAfter.Checked)
                    type = "AF";

                EliteCommissionDetail _det = new EliteCommissionDetail();
                _det.Saec_emp_type = "SALEX";
                _det.Saec_from = Convert.ToDecimal(txtExeFrom.Text);
                _det.Saec_to = Convert.ToDecimal(txtExeTo.Text);
                _det.Saec_type = type;
                _det.Saec_line_no = ++exeline;
                _det.Saec_value = Convert.ToDecimal(txtExeValue.Text);
                _det.Saec_rate = Convert.ToDecimal(txtExeRate.Text);
                ExeList.Add(_det);
                LoadExeGrid();

                if (rdoExeAfter.Checked)
                {
                    txtExeFrom.Text = (ExeList[ExeList.Count - 1].Saec_to + 1).ToString();
                    txtExeTo.Text = (Convert.ToDecimal(txtExeFrom.Text) - 1 + 200000).ToString();
                    txtExeRate.Text =(ExeList[ExeList.Count - 1].Saec_rate).ToString();
                    txtExeValue.Text = (ExeList[ExeList.Count - 1].Saec_value).ToString();
                }
                else
                {
                    txtExeFrom.Text = "0";
                    txtExeTo.Text = "0";
                    txtExeRate.Text = "0";
                    txtExeValue.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CHNLSVC.CloseChannel(); 
            }
        }

        private void LoadExeGrid()
        {

            grvExe.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = ExeList;
            grvExe.DataSource = _source;

        }

        private void txtMgrRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMgrValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMgrRate_TextChanged(object sender, EventArgs e)
        {
            if (txtMgrRate.Text != "" && txtMgrRate.Text!="0") {
                txtMgrValue.Text = "0";
            }
        }

        private void txtMgrValue_TextChanged(object sender, EventArgs e)
        {
            if (txtMgrValue.Text != "" && txtMgrValue.Text!="0")
            {
                txtMgrRate.Text = "0";
            }
        }

        private void txtMgrFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMgrTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnAddtionalAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEmpType.SelectedItem == null)
                {
                    MessageBox.Show("Please select Employee type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbType.SelectedItem == null)
                {
                    MessageBox.Show("Please select Additional type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EliteCommissionAdditional _add = new EliteCommissionAdditional();
                _add.Saec_emp_type = cmbEmpType.SelectedItem.ToString();
                _add.Saec_tp = cmbType.SelectedItem.ToString();
                _add.Saec_desc = txtAdditionalDescription.Text;
                _add.Saec_val = Convert.ToDecimal(txtAddtionalAmt.Text);

                AdditionalList.Add(_add);
                LoadAddtional();
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

        private void LoadAddtional()
        {
            grvAdditional.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = AdditionalList;
            grvAdditional.DataSource = _source;
        }

        private void txtAddtionalAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void grvMgr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0) {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MgrList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = MgrList;
                    grvMgr.DataSource = _source;
                }
            }
        }

        private void grvExe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ExeList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = ExeList;
                    grvExe.DataSource = _source;
                }
            }
        }

        private void btnEmpCat_Click(object sender, EventArgs e)
        {
            if (txtEmpCate.Text == "" || txtEmpSubCate.Text == "") {
                MessageBox.Show("Please select employee category and sub category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EliteCommissionIgnore _ignore = new EliteCommissionIgnore();
            _ignore.Saec_emp_cate = txtEmpCate.Text;
            _ignore.Saec_emp_sub_cate = txtEmpSubCate.Text;
            IgnoreList.Add(_ignore);
            LoadIgnore();
            txtEmpCate.Text = "";
            txtEmpSubCate.Text = "";
        }

        private void LoadIgnore()
        {
            grvIgnore.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = IgnoreList;
            grvIgnore.DataSource = _source;
        }

        private void grvAdditional_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AdditionalList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = AdditionalList;
                    grvAdditional.DataSource = _source;
                }
            }
        }

        private void grvIgnore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    IgnoreList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = IgnoreList;
                    grvIgnore.DataSource = _source;
                }
            }
        }

        private void btnCashierAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCashierExtract.SelectedItem == null)
                {

                    MessageBox.Show("Please select Extract type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EliteCommissionDetail _det = new EliteCommissionDetail();
                _det.Saec_emp_type = "CASHIER";
                _det.Saec_extract_from = cmbCashierExtract.SelectedItem.ToString();
                _det.Saec_value = Convert.ToDecimal(txtCashireValue.Text);
                _det.Saec_rate = Convert.ToDecimal(txtCashierRate.Text);

                CashierList.Add(_det);
                LoadCashier();
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

        private void LoadCashier()
        {
            grvCashier.AutoGenerateColumns = false;
            BindingSource _sourec = new BindingSource();
            _sourec.DataSource = CashierList;
            grvCashier.DataSource = _sourec;
        }

        private void btnHelperAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbHelperExtract.SelectedItem == null)
                {

                    MessageBox.Show("Please select Extract type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EliteCommissionDetail _det = new EliteCommissionDetail();
                _det.Saec_emp_type = "HELPER";
                _det.Saec_extract_from = cmbHelperExtract.SelectedItem.ToString();
                _det.Saec_value = Convert.ToDecimal(txtHelperValue.Text);
                _det.Saec_rate = Convert.ToDecimal(txtHelperRate.Text);

                HelperList.Add(_det);
                LoadHelpergrid();
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

        private void LoadHelpergrid()
        {
            grvHelper.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = HelperList;
            grvHelper.DataSource = _source;
        }

        private void btnCoAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCoExtract.SelectedItem == null)
                {

                    MessageBox.Show("Please select Extract type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EliteCommissionDetail _det = new EliteCommissionDetail();
                _det.Saec_emp_type = "CH_HEAD";
                _det.Saec_extract_from = cmbCoExtract.SelectedItem.ToString();
                _det.Saec_value = Convert.ToDecimal(txtCoValue.Text);
                _det.Saec_rate = Convert.ToDecimal(txtCoRate.Text);

                CoHeadList.Add(_det);
                LoadCoHead();
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


        private void LoadCoHead()
        {
            grvCoHead.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = CoHeadList;
            grvCoHead.DataSource = _source;
        }

        private void grvCashier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CashierList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = CashierList;
                    grvCashier.DataSource = _source;
                }
            }
        }

        private void grvHelper_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    HelperList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = HelperList;
                    grvHelper.DataSource = _source;
                }
            }
        }

        private void grvCoHead_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CoHeadList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = CoHeadList;
                    grvCoHead.DataSource = _source;
                }
            }
        }

        private void btnBrand_Click_1(object sender, EventArgs e)
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
                _CommonSearch.ShowDialog();
                txtItemCD.Select();
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
                _CommonSearch.ReturnIndex = 0;
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

        private void BindCategoryTypes()
        {

            Dictionary<int, string> categoryType = new Dictionary<int, string>();
            categoryType.Add(3, "Item");
            categoryType.Add(5, "Brand & Cat");
            categoryType.Add(6, "Brand & main cat");
            categoryType.Add(7, "Brand");
            categoryType.Add(8, "Sub cat");
            categoryType.Add(9, "Cat");
            categoryType.Add(10, "Main cat");
            categoryType.Add(1, "Main cat & Cat");

            cmbSelectCat.DataSource = new BindingSource(categoryType, null);
            cmbSelectCat.DisplayMember = "Value";
            cmbSelectCat.ValueMember = "Key";

        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            try
            {

                /*
             
            Serial=1
            Promotion=2
            Item=3
            Brand & Sub cat=4
            Brand & Cat=5
            Brand & main cat=6
            Brand=7
            Sub cat=8
            Cat=9
            Main cat=10
           
             */

                if (cmbSelectCat.SelectedItem == null || cmbSelectCat.SelectedItem.ToString() == "")
                {
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
                if (cmbSelectCat.SelectedValue.ToString() == "10")
                {
                    selection = "CATE1";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "9")
                {
                    selection = "CATE2";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "7")
                {
                    selection = "BRAND";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "6")
                {
                    selection = "BRAND_CATE1";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "5")
                {
                    selection = "BRAND_CATE2";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "1")
                {
                    selection = "CAT1_CATE2";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "3")
                {
                    selection = "ITEM";
                }

                //ItemBrandCat_List = new List<CashCommissionDetailRef>();

                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), "", txtItemCD.Text.Trim(), txtSerial.Text.Trim(), "","");

                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem.Columns[1].HeaderText = cmbSelectCat.Text;
                }
                List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
                foreach (DataRow dr in dt.Rows)
                {
                    CashCommissionDetailRef obj = new CashCommissionDetailRef();
                    string code = dr["code"].ToString();
                    if (selection == "CAT1_CATE2")
                    {
                        obj.Sccd_main_cat = txtCate1.Text;
                    }
                    string brand = txtBrand.Text;
                   
                   
                    
                    if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE3")
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
                    if (_duplicate.Count() > 0)
                    {
                        MessageBox.Show("Duplicate record", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                if (addList == null || addList.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no data found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                dataGridViewItem.AutoGenerateColumns = false;
                ItemBrandCat_List.AddRange(addList);
                BindingSource source = new BindingSource();
                source.DataSource = ItemBrandCat_List;
                dataGridViewItem.AutoGenerateColumns = false;
                dataGridViewItem.DataSource = source;
                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem.Columns[1].HeaderText = cmbSelectCat.Text;
                }



                txtItemCD.Text = "";
                txtCate1.Text = "";
                txtCate2.Text = "";

                txtSerial.Text = "";
                txtPromotion.Text = "";

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

        private void txtCircularNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCircularNo.Text))
                {
                    return;
                }
                //validate circular no
                List<FF.BusinessObjects.EliteCommissionDefinition> _definition = CHNLSVC.Sales.GetEliteCommissionDefinition(txtCircularNo.Text);
                if (_definition != null)
                {
                    dtFrom.Value = _definition[0].Saec_valid_from;
                    dtTo.Value = _definition[0].Saec_valid_to;
                    textBox1.Text = _definition[0].Saec_alw_discount.ToString();
                }
                else {
                    return;
                }
                btnSave.Enabled = false;
                //load details
                List<EliteCommissionDetail> _detail = CHNLSVC.Sales.GetEliteCommissionDetailsByCircular(txtCircularNo.Text);

                //load mgr
                if (_detail != null && _detail.Count > 0)
                {
                    List<EliteCommissionDetail> _mgr = (from _res in _detail
                                                        where _res.Saec_emp_type == "SRMGR"
                                                        select _res).ToList<EliteCommissionDetail>();

                    grvMgr.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _mgr;
                    grvMgr.DataSource = _source;
                    //grvMgr.Enabled = false;


                    List<EliteCommissionDetail> _Exe = (from _res in _detail
                                                        where _res.Saec_emp_type == "SALEX"
                                                        select _res).ToList<EliteCommissionDetail>();

                    grvExe.AutoGenerateColumns = false;
                    BindingSource _source1 = new BindingSource();
                    _source1.DataSource = _Exe;
                    grvExe.DataSource = _source1;
                    //grvExe.Enabled = false;

                    //load cashier
                    List<EliteCommissionDetail> _Cashier = (from _res in _detail
                                                            where _res.Saec_emp_type == "CASHIER"
                                                            select _res).ToList<EliteCommissionDetail>();

                    grvCashier.AutoGenerateColumns = false;
                    BindingSource _source2 = new BindingSource();
                    _source2.DataSource = _Cashier;
                    grvCashier.DataSource = _source2;
                    //grvCashier.Enabled = false;

                    //load helper
                    List<EliteCommissionDetail> _Helper = (from _res in _detail
                                                           where _res.Saec_emp_type == "HELPER"
                                                           select _res).ToList<EliteCommissionDetail>();

                    grvHelper.AutoGenerateColumns = false;
                    BindingSource _source3 = new BindingSource();
                    _source3.DataSource = _Helper;
                    grvHelper.DataSource = _source3;
                    //grvHelper.Enabled = false;

                    //load co-head
                    List<EliteCommissionDetail> _Cohead = (from _res in _detail
                                                           where _res.Saec_emp_type == "CH_HEAD"
                                                           select _res).ToList<EliteCommissionDetail>();

                    grvCoHead.AutoGenerateColumns = false;
                    BindingSource _source4 = new BindingSource();
                    _source4.DataSource = _Cohead;
                    grvCoHead.DataSource = _source4;
                    //grvCoHead.Enabled = false;
                }

                //load pcs
                DataTable _dt = new DataTable();
                _dt.Columns.Add("Code");
                _dt.Columns.Add("Description");
                List<EliteCommissionPrty> _prty = CHNLSVC.Sales.GetEliteCommissionLocation(txtCircularNo.Text);
                foreach (EliteCommissionPrty _pty in _prty)
                {
                    DataRow dr = _dt.NewRow();
                    dr[0] = _pty.Saec_prt_cd;
                    _dt.Rows.Add(dr);
                }
                grvParty.AutoGenerateColumns = false;
                grvParty.DataSource = _dt;


                //load ignore
                List<EliteCommissionIgnore> _ignore = CHNLSVC.Sales.GetEliteCommissionIgnore(txtCircularNo.Text);
                grvIgnore.AutoGenerateColumns = false;
                BindingSource _source5 = new BindingSource();
                _source5.DataSource = _ignore;
                grvIgnore.DataSource = _source5;
                grvIgnore.Enabled = false;


                //load additional
                List<EliteCommissionAdditional> _additional = CHNLSVC.Sales.GetEliteCommissionAdditional(txtCircularNo.Text);
                grvAdditional.AutoGenerateColumns = false;
                BindingSource _source6 = new BindingSource();
                _source6.DataSource = _additional;
                grvAdditional.DataSource = _source6;
                grvAdditional.Enabled = false;


                List<EliteCommissionSalesTypes> _sales = CHNLSVC.Sales.GetEliteCommissionSalesType(txtCircularNo.Text);
                foreach (EliteCommissionSalesTypes _sal in _sales)
                {
                    MasterInvoiceType _invType = new MasterInvoiceType();
                    _invType.Srtp_cd = _sal.Saec_sales_type;
                    SalesType.Add(_invType);
                }
                grvSalesType.AutoGenerateColumns = false;
                BindingSource _source7 = new BindingSource();
                _source7.DataSource = SalesType;
                grvSalesType.DataSource = _source7;


                //load items
                List<CashCommissionDetailRef> _temItm = new List<CashCommissionDetailRef>();
                List<EliteCommissionItem> _item = CHNLSVC.Sales.GetEliteCommissionItem(txtCircularNo.Text);
                if (_item != null && _item.Count > 0)
                {
                    foreach (EliteCommissionItem _itm in _item)
                    {
                        CashCommissionDetailRef obj = new CashCommissionDetailRef();
                        if (_itm.Saec_brand != "")
                        {
                            obj.Sccd_itm = _itm.Saec_brand;
                        }
                        else if (_itm.Saec_cat1 != "")
                        {
                            obj.Sccd_itm = _itm.Saec_cat1;
                        }
                        else if (_itm.Saec_cat2 != "")
                        {
                            obj.Sccd_itm = _itm.Saec_cat2;
                        }
                        else if (_itm.Saec_itm != "")
                        {
                            obj.Sccd_itm = _itm.Saec_itm;
                        }
                        else if (_itm.Saec_pro != "")
                        {
                            obj.Sccd_itm = _itm.Saec_pro;
                        }
                        else if (_itm.Saec_serial != "")
                        {
                            obj.Sccd_itm = _itm.Saec_serial;
                        }
                        dataGridViewItem.AutoGenerateColumns = false;
                        BindingSource _source8 = new BindingSource();
                        _source8.DataSource = _temItm;
                        dataGridViewItem.DataSource = _source8;
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

        private void btnCircular_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EliteCircular);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchEliteCommCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircularNo;
                _CommonSearch.txtSearchbyword.Text = txtCircularNo.Text;
                _CommonSearch.ShowDialog();
                txtCircularNo.Focus();
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
            //variables
            MgrList = new List<EliteCommissionDetail>();
            ExeList = new List<EliteCommissionDetail>();
            AdditionalList = new List<EliteCommissionAdditional>();
            IgnoreList = new List<EliteCommissionIgnore>();
            CashierList = new List<EliteCommissionDetail>();
            HelperList = new List<EliteCommissionDetail>();
            CoHeadList = new List<EliteCommissionDetail>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            SalesType = new List<MasterInvoiceType>();
            mgrLine = 0;
            exeline = 0;
            othLine = 0;
            //controls
            this.Cursor = Cursors.WaitCursor;
            while (this.Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            InitializeComponent();
            BindPartyType();
            rdoValue.Checked = true;
            rdoQty.Checked = false;
            pnlValue.Visible = true;
            btnSave.Enabled = true;
            BindCategoryTypes();
            BindSalesTypes();
            this.Cursor = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //cancel definition hdr
            try
            {
                if (string.IsNullOrEmpty(txtCircularNo.Text)) {
                    MessageBox.Show("Circular Number can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int result = CHNLSVC.Sales.CancelEliteCommission(txtCircularNo.Text);
                if (result >= 1)
                {
                    MessageBox.Show("Successfully canceled", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cancel unsuccessful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnClear_Click(null, null);
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

        private void txtCashierRate_TextChanged(object sender, EventArgs e)
        {
            if (txtCashierRate.Text != "" && txtCashierRate.Text != "0")
            {
                txtCashireValue.Text = "0";
            }
        }

        private void txtCashireValue_TextChanged(object sender, EventArgs e)
        {
            if (txtCashireValue.Text != "" && txtCashireValue.Text != "0")
            {
                txtCashierRate.Text = "0";
            }
        }

        private void txtHelperRate_TextChanged(object sender, EventArgs e)
        {
            if (txtHelperRate.Text != "" && txtHelperRate.Text != "0")
            {
                txtHelperValue.Text = "0";
            }
        }

        private void txtHelperValue_TextChanged(object sender, EventArgs e)
        {
            if (txtHelperValue.Text != "" && txtHelperValue.Text != "0")
            {
                txtHelperRate.Text = "0";
            }
        }

        private void txtCoRate_TextChanged(object sender, EventArgs e)
        {
            if (txtCoRate.Text != "" && txtCoRate.Text != "0")
            {
                txtCoValue.Text = "0";
            }
        }

        private void txtCoValue_TextChanged(object sender, EventArgs e)
        {
            if (txtCoValue.Text != "" && txtCoValue.Text != "0")
            {
                txtCoRate.Text = "0";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BindSalesTypes()
        {
            cmbSalesType.DataSource = CHNLSVC.General.GetSalesTypes("", null, null);
            cmbSalesType.DisplayMember = "srtp_desc";
            cmbSalesType.ValueMember = "srtp_cd";
        }

        private void chkSalesAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSalesAll.Checked)
                {
                    DataTable _dt = CHNLSVC.General.GetSalesTypes("", null, null);
                    foreach (DataRow dr in _dt.Rows)
                    {
                        MasterInvoiceType _invoType = new MasterInvoiceType();
                        _invoType.Srtp_cd = dr["SRTP_CD"].ToString();
                        _invoType.Srtp_desc = dr["SRTP_DESC"].ToString();
                        SalesType.Add(_invoType);
                    }
                    grvSalesType.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = SalesType;
                    grvSalesType.DataSource = _source;
                }
                else
                {
                    SalesType = new List<MasterInvoiceType>();
                    grvSalesType.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = SalesType;
                    grvSalesType.DataSource = _source;
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

        private void btnAddSt_Click(object sender, EventArgs e)
        {
            try
            {
                string code = (cmbSalesType.SelectedValue != null) ? cmbSalesType.SelectedValue.ToString() : "";
                DataTable _dt = CHNLSVC.General.GetSalesTypes("", "SRTP_CD", code);
                if (_dt.Rows.Count > 0)
                {

                    MasterInvoiceType _duplicate = SalesType.Find(x => x.Srtp_cd == _dt.Rows[0]["Srtp_cd"].ToString());
                    if (_duplicate == null)
                    {
                        MasterInvoiceType _invType = new MasterInvoiceType();
                        _invType.Srtp_cd = _dt.Rows[0]["Srtp_cd"].ToString();
                        _invType.Srtp_desc = _dt.Rows[0]["SRTP_DESC"].ToString();
                        SalesType.Add(_invType);

                        grvSalesType.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = SalesType;
                        grvSalesType.DataSource = _source;
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

        private void grvSalesType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SalesType.RemoveAt(e.RowIndex);

                    grvSalesType.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = SalesType;
                    grvSalesType.DataSource = _source;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeCate);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_categories(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEmpCate;
                _CommonSearch.txtSearchbyword.Text = txtEmpCate.Text;
                _CommonSearch.ShowDialog();
                txtEmpCate.Focus();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeSubCategory);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_sub_categories(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEmpSubCate;
                _CommonSearch.txtSearchbyword.Text = txtEmpSubCate.Text;
                _CommonSearch.ShowDialog();
                txtEmpSubCate.Focus();
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

        private void rdoAfter_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAfter.Checked)
            {
                if (MgrList != null && MgrList.Count > 0)
                {
                    txtMgrFrom.Text = (MgrList[MgrList.Count - 1].Saec_to + 1).ToString();
                    txtMgrTo.Text = (Convert.ToDecimal(txtMgrFrom.Text) - 1 + 200000).ToString();
                    txtMgrRate.Text = "0";
                    txtMgrValue.Text = "0";
                }
            }
        }

        private void rdoExeAfter_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoExeAfter.Checked)
            {
                if (ExeList != null && ExeList.Count > 0)
                {
                    txtExeFrom.Text = (ExeList[ExeList.Count - 1].Saec_to + 1).ToString();
                    txtExeTo.Text = (Convert.ToDecimal(txtExeFrom.Text) - 1 + 200000).ToString();
                    txtExeRate.Text = "0";
                    txtExeValue.Text = "0";
                }
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvSalesType_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SalesType.RemoveAt(e.RowIndex);

                        grvSalesType.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = SalesType;
                        grvSalesType.DataSource = _source;
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

        private void cmbSelectCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCD.Text = "";
            txtCate1.Text = "";
            txtBrand.Text = "";
            txtCate2.Text = "";
            txtSerial.Text = "";
            txtPromotion.Text = "";
            txtSerial.Text = "";
            dataGridViewItem.DataSource = null;
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
        }

        private void btnOthCate_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeCate);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_categories(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthCate;
                _CommonSearch.txtSearchbyword.Text = txtOthCate.Text;
                _CommonSearch.ShowDialog();
                txtOthCate.Focus();
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

        private void rdoValue_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoValue.Checked)
            {
                pnlValue.Visible = true;
            }
            else {
                pnlValue.Visible = false;
            }
        }

        private void rdoQty_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoQty.Checked)
            {
                pnlValue.Visible = false;
            }
            else
            {
                pnlValue.Visible = true;
            }
        }

        private void btnAddOther_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOthCate.Text == "") {
                    MessageBox.Show("Please select Employee category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MasterUserCategory _user = CHNLSVC.General.GetUserCatByCode(txtOthCate.Text);
                if (_user == null) {
                    MessageBox.Show("Entered employee category invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //check previous records
                List<EliteCommissionDetail> _previous = (from _res in OtherList
                                                         where _res.Saec_emp_type == txtOthCate.Text
                                                         select _res).ToList();
                if (_previous != null && _previous.Count > 0) { 
                //type only can be value
                    if (!rdoValue.Checked) {
                        MessageBox.Show("If same employee type has multiple records type has to be Value Based","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    if (Convert.ToDecimal(txtOthFrom.Text) <= _previous[_previous.Count - 1].Saec_to) {
                        MessageBox.Show("Last record to value greater than this record from value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                
                }

                EliteCommissionDetail _det = new EliteCommissionDetail();
                _det.Saec_emp_type = txtOthCate.Text;
                _det.Saec_type = "OT";
                if (rdoValue.Checked)
                {
                    _det.Saec_from = Convert.ToDecimal(txtOthFrom.Text);
                    _det.Saec_to = Convert.ToDecimal(txtOthTo.Text);
                }
                else {
                    _det.Saec_from = 0;
                    _det.Saec_to = 0;
                }
                _det.Saec_line_no = ++othLine;
                _det.Saec_rate = Convert.ToDecimal(txtOthRate.Text);
                _det.Saec_value = Convert.ToDecimal(txtOthValue.Text);

                OtherList.Add(_det);
                LoadOther();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CHNLSVC.CloseChannel(); 
            }
        }

        private void LoadOther()
        {
            grvOther.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = OtherList;
            grvOther.DataSource = _source;
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListPartyTypes.SelectedIndex != -1) {
                PCList = null;
                grvParty.DataSource = null;
                grvParty.AutoGenerateColumns = false;
                grvParty.DataSource = PCList;
            }
        }


   }

}
