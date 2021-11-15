using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Linq;
using System.Globalization;
using System.Configuration;
using System.Data.OleDb;



using FF.BusinessObjects.General;
using FF.WindowsERPClient.UtilityClasses;
namespace FF.WindowsERPClient.General
{
    /*
     * Originally Written by Prabhath on 13/03/2013
     * Modification History
     * Name             Date                Code
     * 
     */

    public partial class OrganizationDefinition : FF.WindowsERPClient.Base
    {
        List<MasterSalesPriorityHierarchy> CompanyPcHierarchy = new List<MasterSalesPriorityHierarchy>();
        List<MasterLocationPriorityHierarchy> CompanyLocHierarchy = new List<MasterLocationPriorityHierarchy>();
        List<MasterProfitCenter> _adhocPc = null;
        List<MasterLocation> _adhocLoc = null;
        bool _isLoad = false;
        List<PaymentType> PaymentTxnList = null;
        List<MasterAdditionalProductBonus> _additionalProductBonus = null;
        DataTable ProfitCenterCharge;
        bool IsItemExcel = false;
        bool IsPromoExcel = false;

        bool isUpdate = false;
        bool isSubCh_update = false;
        Deposit_Bank_Pc_wise obj_channels;

        List<PriceBookLevelRef> PBList;
        List<PaymentTypeRef> PayType;
        List<CashCommissionDetailRef> ItemBrandCat_List;
        List<MasterInvoiceType> SalesType;
        DataTable PCList = null;
        string _searchType = "";
        private Boolean _is_hp_rec = false;
        private Boolean _is_GVO_sel = false;
        private int _excel_up_type = 0;     //kapila 23/12/2016 0-item , 1-promotions
        List<PcList> _list = new List<PcList>();
        int is_excel_upload = 0;
        private CommonSearch.CommonSearch _commonSearch = null;
        private MasterBusinessEntity _masterBusinessCompany = null;


        private DataTable dtBinGrid;//Added by Udesh 12-Nov-2018

        public OrganizationDefinition()
        {
            InitializeComponent();
            gvTransactionPayType.AutoGenerateColumns = false;
            gvAddBonus.AutoGenerateColumns = false;
            gvAddBonusDisplay.AutoGenerateColumns = false;
            gvPCCharge.AutoGenerateColumns = false;
            gvReceiptDivisionAll.AutoGenerateColumns = false;
            ProfitCenterCharge = new DataTable();
            _isLoad = true;
            gvSearchedPc.AutoGenerateColumns = false;
            ucProfitCenterCommonSearch.Company = BaseCls.GlbUserComCode;
            ucProfitCenterNewAssign.Company = BaseCls.GlbUserComCode;
            ucProfitCenterNewAssign.IsDisplayRawData = true;
            ucProfitCenterCommonSearch.ChangeCompanyReadonly(false);    //kapila 24/5/2014

            ucLoactionCommonSearch.Company = BaseCls.GlbUserComCode;
            ucLocationNewAssign.Company = BaseCls.GlbUserComCode;
            ucLocationNewAssign.IsDisplayRawData = true;
            if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
            {
                ucProfitCenterCommonSearch.IsAllProfitCenter = true;
            }
            if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
            {
                ucLoactionCommonSearch.IsAllLocation = true;
            }
            AddCheckBoxToGridHeader(true);
            _isLoad = false;
            LoadPermission();
            BindSalesTypes();

            PBList = new List<PriceBookLevelRef>();
            PayType = new List<PaymentTypeRef>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            SalesType = new List<MasterInvoiceType>();
            PCList = new DataTable();

            InitializeBinGridTable(); // Added by Udesh 12-Nov-2018
        }


        private void LoadPayType()
        {
            BindingSource _source = new BindingSource();
            List<PaymentTypeRef> _ref = CHNLSVC.Sales.GetAllPaymentType(ucProfitCenterCommonSearch.Company, string.Empty, string.Empty);
            _source.DataSource = _ref;
            chkLstPayType.DataSource = _source.DataSource;
            ((ListBox)chkLstPayType).DisplayMember = "Sapt_cd";
        }

        private void LoadGVItems()
        {
            DataTable _dt = CHNLSVC.Inventory.getItemByType("G");
            chkLstGV.DataSource = _dt;
            ((ListBox)chkLstGV).DisplayMember = "mi_cd";

        }

        private void LoadGVSchemes()
        {
            DataTable _dt = CHNLSVC.Sales.GetSchemes("ALL", null);
            chkLstScheme.DataSource = _dt;
            ((ListBox)chkLstScheme).DisplayMember = "hsd_cd";
        }

        private void LoadBook()
        {
            DataTable _book = CHNLSVC.Sales.GetPriceBookTable(ucProfitCenterCommonSearch.Company, string.Empty);
            _book.Rows.Add("");
            BindingSource _sourceBook = new BindingSource();
            _sourceBook.DataSource = _book;
            cmbtpt_Book.DataSource = _sourceBook.DataSource;
            cmbtpt_Book.DisplayMember = "SAPB_PB";
            cmbtpt_Book.Text = string.Empty;
        }
        private void LoadProfitCenterCharge()
        {
            ProfitCenterCharge = new DataTable();
            var _r = from DataGridViewRow _rs in gvSearchedPc.Rows
                     where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true
                     select _rs;

            if (_r == null || _r.Count() <= 0)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = ProfitCenterCharge;
                gvPCCharge.DataSource = _source;
            }
            else
            {
                foreach (var _row in _r)
                {
                    string _profCenter = Convert.ToString(_row.Cells["p_pccode"].Value);
                    DataTable _pcCharge = CHNLSVC.General.GetProfitCenterCharge(ucProfitCenterCommonSearch.Company, _profCenter);
                    ProfitCenterCharge.Merge(_pcCharge);
                }
                BindingSource _source = new BindingSource();
                _source.DataSource = ProfitCenterCharge;
                gvPCCharge.DataSource = _source;
            }
        }
        private void LoadReceiptDivision()
        {
            if (gvSearchedPc.RowCount <= 0) return;
            List<string> _lst = (from DataGridViewRow _r in gvSearchedPc.Rows
                                 where Convert.ToBoolean(_r.Cells["p_select"].Value) == true
                                 select Convert.ToString(_r.Cells["p_pccode"].Value)).ToList();

            DataTable _tbl = CHNLSVC.General.GetReceiptDivision("AAL", _lst);
            gvReceiptDivisionAll.DataSource = _tbl;
        }
        private void LoadLevel(string _book)
        {
            DataTable _level = CHNLSVC.Sales.GetPriceLevelTable(ucProfitCenterCommonSearch.Company, _book, string.Empty);
            if (_level.Rows.Count > 0)
            {
                _level.Rows.Add("");
                var level = _level.AsEnumerable().ToList().Select(X => X.Field<string>("SAPL_PB_LVL_CD")).Distinct().ToList();
                BindingSource _sourceLevel = new BindingSource();
                _sourceLevel.DataSource = level;

                cmbtpt_Level.DataSource = _sourceLevel.DataSource;
                cmbtpt_Level.DisplayMember = "SAPL_PB_LVL_CD";
                cmbtpt_Level.Text = string.Empty;
            }

        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.EmployeeAll:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(lblBankID.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TransactionType:
                    {
                        paramsText.Append(ucProfitCenterCommonSearch.Company + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(ucProfitCenterCommonSearch.Company + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txttpt_maincat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(string.Empty + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PayCircular:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankALL:
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.GenDiscount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ucProfitCenterCommonSearch.Company) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.Channel) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.SubChannel) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.Area) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.Regien) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.Zone) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.ProfitCenter))
            {
                MessageBox.Show("Please select one of Hierarchy filter criteria", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (tabControl1.SelectedTab.Name != tpLocHierarchy.Name)
            {
                if (string.IsNullOrEmpty(ucProfitCenterCommonSearch.ProfitCenter))
                {
                    MessageBox.Show("Please select the profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string _company = ucProfitCenterCommonSearch.Company;
                string _pc = ucProfitCenterCommonSearch.ProfitCenter;
                BindingSource _source = new BindingSource();
                _source.DataSource = _adhocPc;

                if (gvSearchedPc.Columns[1].DataPropertyName != "Mpc_cd")
                {
                    gvSearchedPc.Columns[1].DataPropertyName = "Mpc_cd";
                    gvSearchedPc.Columns[2].DataPropertyName = "Mpc_desc";
                    gvSearchedPc.DataSource = _source;

                }

                List<MasterProfitCenter> _lst = CHNLSVC.Sales.GetProfitCenterList(_company.Trim(), _pc.Trim());
                if (_adhocPc == null)
                {
                    _adhocPc = new List<MasterProfitCenter>();

                }
                else
                {
                    var _dup = _adhocPc.Where(X => X.Mpc_cd == _pc);
                    if (_dup != null)
                        if (_dup.Count() > 0)
                        {

                            CompanyPcHierarchy = CHNLSVC.Sales.GetSalesPriorityHierarchyWithDescription(ucProfitCenterCommonSearch.Company, string.Empty);
                            List<MasterSalesPriorityHierarchy> _oS = new List<MasterSalesPriorityHierarchy>();
                            foreach (DataGridViewRow _r in gvSearchedPc.Rows)
                            {
                                _r.Cells[0].Value = true;
                                if (CompanyPcHierarchy != null)
                                    if (CompanyPcHierarchy.Count > 0)
                                    {
                                        var _l = (from _n in CompanyPcHierarchy
                                                  where _n.Mpi_pc_cd == Convert.ToString(_r.Cells["p_pccode"].Value)
                                                  select _n).ToList();

                                        _oS.AddRange(_l);
                                    }
                            }
                            CompanyPcHierarchy = new List<MasterSalesPriorityHierarchy>();
                            CompanyPcHierarchy = _oS;
                            DisplayValueToHierarchy(true);
                            _oS = null;
                            return;
                        }

                }
                if (_lst == null || _lst.Count <= 0) { MessageBox.Show("There is no such profit center available.", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                _adhocPc.AddRange(_lst);
                _source.DataSource = _adhocPc;
                gvSearchedPc.DataSource = _source;

                CompanyPcHierarchy = CHNLSVC.Sales.GetSalesPriorityHierarchyWithDescription(ucProfitCenterCommonSearch.Company, string.Empty);
                List<MasterSalesPriorityHierarchy> _o = new List<MasterSalesPriorityHierarchy>();
                foreach (DataGridViewRow _r in gvSearchedPc.Rows)
                {
                    _r.Cells[0].Value = true;
                    if (CompanyPcHierarchy != null)
                        if (CompanyPcHierarchy.Count > 0)
                        {
                            var _l = (from _n in CompanyPcHierarchy
                                      where _n.Mpi_pc_cd == Convert.ToString(_r.Cells["p_pccode"].Value)
                                      select _n).ToList();

                            _o.AddRange(_l);
                        }
                }
                CompanyPcHierarchy = new List<MasterSalesPriorityHierarchy>();
                CompanyPcHierarchy = _o;
                DisplayValueToHierarchy(true);

                if (_o != null && _o.Count > 0)
                    if (_o.Count == 6)
                    {
                        foreach (MasterSalesPriorityHierarchy _h in _o.Where(x => x.Mpi_cd == "CHNL" || x.Mpi_cd == "SCHNL").ToList())
                        {
                            if (_h.Mpi_cd == "CHNL")
                                ucProfitCenterNewAssign.Channel = _h.Mpi_val;
                            if (_h.Mpi_cd == "SCHNL")
                                ucProfitCenterNewAssign.SubChannel = _h.Mpi_val;
                        }
                    }

                _o = null;

            }





            if (tabControl1.SelectedTab.Name == tpLocHierarchy.Name)
            {
                if (string.IsNullOrEmpty(ucLoactionCommonSearch.ProfitCenter))
                {
                    MessageBox.Show("Please select the location", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string _company = ucLoactionCommonSearch.Company;
                string _loc = ucLoactionCommonSearch.ProfitCenter;
                BindingSource _source = new BindingSource();
                _source.DataSource = _adhocLoc;

                if (gvSearchedPc.Columns[1].DataPropertyName != "Ml_loc_cd")
                {
                    gvSearchedPc.Columns[1].DataPropertyName = "Ml_loc_cd";
                    gvSearchedPc.Columns[2].DataPropertyName = "Ml_loc_desc";
                    gvSearchedPc.DataSource = _source;
                }

                List<MasterLocation> _lst = null;
                MasterLocation _locs = null;
                if (!string.IsNullOrEmpty(_loc))
                {
                    _locs = CHNLSVC.General.GetLocationByLocCode(_company.Trim(), _loc);
                    if (_lst == null) _lst = new List<MasterLocation>();
                    _lst.Add(_locs);
                }
                else
                {
                    _lst = CHNLSVC.General.GetLocationByCompany(_company.Trim());
                }

                // List<MasterLocation> _lst = CHNLSVC.General.GetLocationByCompany(_company.Trim());
                if (_adhocLoc == null)
                {
                    _adhocLoc = new List<MasterLocation>();

                }
                else
                {
                    var _dup = _adhocLoc.Where(X => X.Ml_loc_cd == _loc);
                    if (_dup != null)
                        if (_dup.Count() > 0)
                        {
                            CompanyLocHierarchy = CHNLSVC.Sales.GetLocationPriorityHierarchyWithDescription(ucLoactionCommonSearch.Company, string.Empty);
                            List<MasterLocationPriorityHierarchy> _oS = new List<MasterLocationPriorityHierarchy>();
                            foreach (DataGridViewRow _r in gvSearchedPc.Rows)
                            {
                                _r.Cells[0].Value = true;
                                if (CompanyLocHierarchy != null)
                                    if (CompanyLocHierarchy.Count > 0)
                                    {
                                        var _l = (from _n in CompanyLocHierarchy
                                                  where _n.Mli_loc_cd == Convert.ToString(_r.Cells["p_pccode"].Value)
                                                  select _n).ToList();

                                        _oS.AddRange(_l);
                                    }
                            }
                            CompanyLocHierarchy = new List<MasterLocationPriorityHierarchy>();
                            CompanyLocHierarchy = _oS;
                            DisplayValueToHierarchy(false);
                            _oS = null;
                            return;
                        }
                }
                if (_lst == null || _lst.Count <= 0) { MessageBox.Show("There is no such location available.", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                var _single = _lst.Where(x => x.Ml_loc_cd == _loc).ToList();
                _adhocLoc.AddRange(_single);
                _source.DataSource = _adhocLoc;
                gvSearchedPc.DataSource = _source;

                CompanyLocHierarchy = CHNLSVC.Sales.GetLocationPriorityHierarchyWithDescription(ucLoactionCommonSearch.Company, string.Empty);
                List<MasterLocationPriorityHierarchy> _o = new List<MasterLocationPriorityHierarchy>();
                foreach (DataGridViewRow _r in gvSearchedPc.Rows)
                {
                    _r.Cells[0].Value = true;
                    if (CompanyLocHierarchy != null)
                        if (CompanyLocHierarchy.Count > 0)
                        {
                            var _l = (from _n in CompanyLocHierarchy
                                      where _n.Mli_loc_cd == Convert.ToString(_r.Cells["p_pccode"].Value)
                                      select _n).ToList();

                            _o.AddRange(_l);
                        }
                }
                CompanyLocHierarchy = new List<MasterLocationPriorityHierarchy>();
                CompanyLocHierarchy = _o;
                DisplayValueToHierarchy(false);
                _o = null;

            }

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPcHierarchySave.Text = "Save";
            lblNoPaymode.Text = string.Empty;
            lblNoPc.Text = string.Empty;

            ClearPcHierarchyDefinitionScreen();
            ClearLocationHierarchyDefinitionScreen();
            pnlLoc.Visible = false;
            if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
            {
                ucProfitCenterCommonSearch.IsAllProfitCenter = true;
            }
            else
            {
                ucProfitCenterCommonSearch.IsAllProfitCenter = false;
            }

            if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
            {
                ucProfitCenterCommonSearch.Visible = true;
                ucLoactionCommonSearch.Visible = false;
            }
            else if (tabControl1.SelectedTab.Name == tpLocHierarchy.Name)
            {
                ucProfitCenterCommonSearch.Visible = false;
                ucLoactionCommonSearch.Visible = true;
            }
            else
            {
                ucProfitCenterCommonSearch.Visible = true;
                ucLoactionCommonSearch.Visible = false;
            }

            if (tabControl1.SelectedTab.Name == tpPcAdditionalBonus.Name)
            {
                List<MasterAdditionalProductBonus> _lst = CHNLSVC.Sales.GetAllProductBonusSetup(ucProfitCenterCommonSearch.Company);
                BindingSource _source = new BindingSource();
                _source.DataSource = _lst;
                gvAddBonusDisplay.DataSource = _source;
            }
            if (tabControl1.SelectedTab.Name == tpPcTransactionPayTypes.Name)
            {
                LoadPayType();
                LoadBook();
                LoadGVItems();
                LoadGVSchemes();
                pnlLoc.Visible = true;
            }
            //if (tabControl1.SelectedTab.Name == tpPcCharges.Name)
            //{
            //    ProfitCenterCharge = new DataTable();
            //    LoadProfitCenterCharge();
            //}
            //if (tabControl1.SelectedTab.Name == tpPcReceiptCategory.Name)
            //{
            //    LoadReceiptDivision();
            //}


        }
        private string SetCommonSearchInitialParametersForPc(CommonUIDefiniton.SearchUserControlType _type)
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
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(ucProfitCenterCommonSearch.Company + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(ucProfitCenterCommonSearch.Company + seperator + ucProfitCenterCommonSearch.Channel + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(ucProfitCenterCommonSearch.Company + seperator + ucProfitCenterCommonSearch.Channel + seperator + ucProfitCenterCommonSearch.SubChannel + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(ucProfitCenterCommonSearch.Company + seperator + ucProfitCenterCommonSearch.Channel + seperator + ucProfitCenterCommonSearch.SubChannel + seperator + ucProfitCenterCommonSearch.Area + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(ucProfitCenterCommonSearch.Company + seperator + ucProfitCenterCommonSearch.Channel + seperator + ucProfitCenterCommonSearch.SubChannel + seperator + ucProfitCenterCommonSearch.Area + seperator + ucProfitCenterCommonSearch.Regien + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(ucProfitCenterCommonSearch.Company + seperator + ucProfitCenterCommonSearch.Channel + seperator + ucProfitCenterCommonSearch.SubChannel + seperator + ucProfitCenterCommonSearch.Area + seperator + ucProfitCenterCommonSearch.Regien + seperator + ucProfitCenterCommonSearch.Zone + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(ucLoactionCommonSearch.Company + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append(ucLoactionCommonSearch.Company + seperator + ucLoactionCommonSearch.Channel + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append(ucLoactionCommonSearch.Company + seperator + ucLoactionCommonSearch.Channel + seperator + ucLoactionCommonSearch.SubChannel + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        paramsText.Append(ucLoactionCommonSearch.Company + seperator + ucLoactionCommonSearch.Channel + seperator + ucLoactionCommonSearch.SubChannel + seperator + ucLoactionCommonSearch.Area + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        paramsText.Append(ucLoactionCommonSearch.Company + seperator + ucLoactionCommonSearch.Channel + seperator + ucLoactionCommonSearch.SubChannel + seperator + ucLoactionCommonSearch.Area + seperator + ucLoactionCommonSearch.Regien + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(ucLoactionCommonSearch.Company + seperator + ucLoactionCommonSearch.Channel + seperator + ucLoactionCommonSearch.SubChannel + seperator + ucLoactionCommonSearch.Area + seperator + ucLoactionCommonSearch.Regien + seperator + ucLoactionCommonSearch.Zone + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void LoadCurrentAssignDetail(DataGridViewRow _row, bool _isPc)
        {
            if (_row == null)
            {
                ClearCurrentAssignment(_isPc);
                return;
            }
            if (_isPc)
            {
                lblPcCur_area.Text = Convert.ToString(_row.Cells["ps_area"].Value);
                lblPcCur_channel.Text = Convert.ToString(_row.Cells["ps_channel"].Value);
                lblPcCur_region.Text = Convert.ToString(_row.Cells["ps_region"].Value);
                lblPcCur_subchannel.Text = Convert.ToString(_row.Cells["ps_subchannel"].Value);
                lblPcCur_zone.Text = Convert.ToString(_row.Cells["ps_zone"].Value);
            }
            else
            {
                lblLocCur_area.Text = Convert.ToString(_row.Cells["pl_area"].Value);
                lblLocCur_channel.Text = Convert.ToString(_row.Cells["pl_channel"].Value);
                lblLocCur_region.Text = Convert.ToString(_row.Cells["pl_region"].Value);
                lblLocCur_subchannel.Text = Convert.ToString(_row.Cells["pl_subchannel"].Value);
                lblLocCur_zone.Text = Convert.ToString(_row.Cells["pl_zone"].Value);
            }

        }
        private void ucProfitCenterCommonSearch_TextBoxLostFocus(object sender, EventArgs e)
        {
            string _company = ucProfitCenterCommonSearch.Company;
            string _channel = ucProfitCenterCommonSearch.Channel;
            string _subchannel = ucProfitCenterCommonSearch.SubChannel;
            string _area = ucProfitCenterCommonSearch.Area;
            string _region = ucProfitCenterCommonSearch.Regien;
            string _zone = ucProfitCenterCommonSearch.Zone;
            string _pc = ucProfitCenterCommonSearch.ProfitCenter;

            if (!string.IsNullOrEmpty(ucProfitCenterCommonSearch.Company) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.Channel) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.SubChannel) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.Area) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.Regien) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.Zone) && string.IsNullOrEmpty(ucProfitCenterCommonSearch.ProfitCenter))
            {
                MessageBox.Show("Please select one of Hierarchy filter criteria", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            TextBox _sender = (TextBox)sender;

            if (_sender.Name == "TextBoxChannel" || _sender.Name == "TextBoxSubChannel" || _sender.Name == "TextBoxArea" || _sender.Name == "TextBoxRegion" || _sender.Name == "TextBoxZone")
            {
                if (string.IsNullOrEmpty(_sender.Text)) return;
                //get it from hierarchy
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParametersForPc(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                gvSearchedPc.Columns[1].DataPropertyName = "Code";
                gvSearchedPc.Columns[2].DataPropertyName = "Description";

                BindingSource _source = new BindingSource();
                _source.DataSource = _result;
                gvSearchedPc.DataSource = _source;

                _adhocPc = null;
            }

            if ((_sender.Name == "TextBoxCompany" || _sender.Name == "TextBoxLocation") && ucProfitCenterCommonSearch.Visible && _adhocPc == null)
            {

                gvSearchedPc.Columns[1].DataPropertyName = "Mpc_cd";
                gvSearchedPc.Columns[2].DataPropertyName = "Mpc_desc";

                List<MasterProfitCenter> _lst = CHNLSVC.Sales.GetProfitCenterList(_company.Trim(), _pc.Trim());

                BindingSource _source = new BindingSource();
                _source.DataSource = _lst;
                gvSearchedPc.DataSource = _source;
            }

            if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
                CompanyPcHierarchy = CHNLSVC.Sales.GetSalesPriorityHierarchyWithDescription(ucProfitCenterCommonSearch.Company, ucProfitCenterCommonSearch.ProfitCenter);

            if (_sender.Name != "TextBoxCompany")
            {
                if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
                {
                    List<MasterSalesPriorityHierarchy> _o = new List<MasterSalesPriorityHierarchy>();
                    foreach (DataGridViewRow _r in gvSearchedPc.Rows)
                    {
                        _r.Cells[0].Value = true;
                        if (CompanyPcHierarchy != null)
                            if (CompanyPcHierarchy.Count > 0)
                            {
                                var _l = (from _n in CompanyPcHierarchy
                                          where _n.Mpi_pc_cd == Convert.ToString(_r.Cells["p_pccode"].Value)
                                          select _n).ToList();

                                _o.AddRange(_l);
                            }
                    }
                    CompanyPcHierarchy = new List<MasterSalesPriorityHierarchy>();
                    CompanyPcHierarchy = _o;
                    DisplayValueToHierarchy(true);
                    _o = null;
                }
            }
            else
            {
                if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
                    DisplayValueToHierarchy(true);
                foreach (DataGridViewRow _r in gvSearchedPc.Rows)
                    _r.Cells[0].Value = true;
            }

            LoadPayType();
            if (tabControl1.SelectedTab.Name == tpPcCharges.Name)
                LoadProfitCenterCharge();

            if (gvPcHierarchy.RowCount == 1 && tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
            {
                DataGridViewRow _row = gvPcHierarchy.Rows[0];
                LoadCurrentAssignDetail(_row, true);
            }
            AddCheckBoxToGridHeader(false);
        }
        private void ucLoactionCommonSearch_TextBoxLostFocus(object sender, EventArgs e)
        {
            string _company = ucLoactionCommonSearch.Company;
            string _channel = ucLoactionCommonSearch.Channel;
            string _subchannel = ucLoactionCommonSearch.SubChannel;
            string _area = ucLoactionCommonSearch.Area;
            string _region = ucLoactionCommonSearch.Regien;
            string _zone = ucLoactionCommonSearch.Zone;
            string _loc = ucLoactionCommonSearch.ProfitCenter;

            TextBox _sender = (TextBox)sender;

            if (tabControl1.SelectedTab.Name == tpLocHierarchy.Name)
            {
                if (_sender.Name == "TextBoxChannel" || _sender.Name == "TextBoxSubChannel" || _sender.Name == "TextBoxArea" || _sender.Name == "TextBoxRegion" || _sender.Name == "TextBoxZone")
                {
                    if (string.IsNullOrEmpty(_sender.Text)) return;
                    //get it from hierarchy
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.SearchParams = SetCommonSearchInitialParametersForPc(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                    gvSearchedPc.Columns[1].DataPropertyName = "Code";
                    gvSearchedPc.Columns[2].DataPropertyName = "Description";

                    BindingSource _source = new BindingSource();
                    _source.DataSource = _result;
                    gvSearchedPc.DataSource = _source;


                }

                if ((_sender.Name == "TextBoxCompany" || _sender.Name == "TextBoxLocation") && ucLoactionCommonSearch.Visible && _adhocLoc == null)
                {
                    gvSearchedPc.Columns[1].DataPropertyName = "Ml_loc_cd";
                    gvSearchedPc.Columns[2].DataPropertyName = "Ml_loc_desc";
                    List<MasterLocation> _lst = null;
                    MasterLocation _locs = null;
                    if (_sender.Name == "TextBoxLocation" && !string.IsNullOrEmpty(_loc))
                    {
                        _locs = CHNLSVC.General.GetLocationByLocCode(_company.Trim(), _loc);
                        if (_lst == null) _lst = new List<MasterLocation>();
                        _lst.Add(_locs);
                    }
                    else
                    {
                        _lst = CHNLSVC.General.GetLocationByCompany(_company.Trim());
                    }
                    BindingSource _source = new BindingSource();
                    if (!string.IsNullOrEmpty(_loc))
                    {
                        var _single = _lst.Where(x => x.Ml_loc_cd == _loc).ToList();
                        _source.DataSource = _single;
                    }
                    else
                        _source.DataSource = _lst;

                    gvSearchedPc.DataSource = _source;
                }

                if (_sender.Name == "TextBoxLocation")
                    CompanyLocHierarchy = CHNLSVC.Sales.GetLocationPriorityHierarchyWithDescription(ucLoactionCommonSearch.Company, ucLoactionCommonSearch.ProfitCenter);
                else
                    CompanyLocHierarchy = CHNLSVC.Sales.GetLocationPriorityHierarchyWithDescription(ucLoactionCommonSearch.Company, string.Empty);

                if (_sender.Name != "TextBoxCompany")
                {
                    List<MasterLocationPriorityHierarchy> _o = new List<MasterLocationPriorityHierarchy>();
                    foreach (DataGridViewRow _r in gvSearchedPc.Rows)
                    {
                        _r.Cells[0].Value = true;
                        if (CompanyLocHierarchy != null)
                            if (CompanyLocHierarchy.Count > 0)
                            {
                                var _l = (from _n in CompanyLocHierarchy
                                          where _n.Mli_loc_cd == Convert.ToString(_r.Cells["p_pccode"].Value)
                                          select _n).ToList();

                                _o.AddRange(_l);
                            }
                    }
                    CompanyLocHierarchy = new List<MasterLocationPriorityHierarchy>();
                    CompanyLocHierarchy = _o;
                    DisplayValueToHierarchy(false);
                    _o = null;
                }
                else
                {
                    DisplayValueToHierarchy(false);
                    foreach (DataGridViewRow _r in gvSearchedPc.Rows)
                        _r.Cells[0].Value = true;
                }
            }
            else
            {
                List<MasterLocation> _lst = new List<MasterLocation>();
                BindingSource _source = new BindingSource();
                _source.DataSource = _lst;
                gvSearchedPc.DataSource = _source;
            }

            if (gvLocHierarchy.RowCount == 1)
            {
                DataGridViewRow _row = gvLocHierarchy.Rows[0];
                LoadCurrentAssignDetail(_row, false);
            }
            AddCheckBoxToGridHeader(false);
        }
        private void gvSearchedPc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvSearchedPc.RowCount > 0)
                if (e.RowIndex != -1)
                {


                }
        }
        string _serText = string.Empty;
        private void gvSearchedPc_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool _isFound = false;
            _serText += e.KeyChar.ToString();
            //if (Char.IsLetter(e.KeyChar))
            //{
            for (int i = 0; i < (gvSearchedPc.Rows.Count); i++)
            {
                if (gvSearchedPc.Rows[i].Cells["p_pccode"].Value.ToString().StartsWith(_serText, true, CultureInfo.InvariantCulture))
                {
                    _isFound = true;
                    gvSearchedPc.Rows[i].Cells["p_pccode"].Selected = true;
                    gvSearchedPc.FirstDisplayedScrollingRowIndex = i;
                    gvSearchedPc.Update();
                    timer1.Enabled = true;
                    return; // stop looping
                }
            }


            //}

            if (_isFound == false && _serText.Length >= 3) timer1.Enabled = true;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            _serText = string.Empty;
            timer1.Enabled = false;
        }
        private void ClearCurrentAssignment(bool _isPc)
        {
            if (_isPc)
            {
                lblPcCur_area.Text = string.Empty;
                lblPcCur_channel.Text = string.Empty;
                lblPcCur_region.Text = string.Empty;
                lblPcCur_subchannel.Text = string.Empty;
                lblPcCur_zone.Text = string.Empty;
            }
            else
            {
                lblLocCur_area.Text = string.Empty;
                lblLocCur_channel.Text = string.Empty;
                lblLocCur_region.Text = string.Empty;
                lblLocCur_subchannel.Text = string.Empty;
                lblLocCur_zone.Text = string.Empty;
            }
        }
        private void gvPcHierarchy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvPcHierarchy.RowCount > 0)
                if (e.RowIndex != -1)
                {
                    DataGridViewRow _row = gvPcHierarchy.Rows[e.RowIndex];
                    LoadCurrentAssignDetail(_row, true);
                }
        }
        private void gvPcHierarchy_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvPcHierarchy.RowCount > 0)
                    if (gvPcHierarchy.SelectedRows[0].Index != -1)
                    {
                        DataGridViewRow _row = gvPcHierarchy.Rows[gvPcHierarchy.SelectedRows[0].Index];
                        LoadCurrentAssignDetail(_row, true);
                    }
            }
            catch
            {

            }
        }
        private void gvLocHierarchy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvLocHierarchy.RowCount > 0)
                if (e.RowIndex != -1)
                {
                    DataGridViewRow _row = gvLocHierarchy.Rows[e.RowIndex];
                    LoadCurrentAssignDetail(_row, false);
                }
        }
        private void gvLocHierarchy_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvLocHierarchy.RowCount > 0)
                    if (gvLocHierarchy.SelectedRows[0].Index != -1)
                    {
                        DataGridViewRow _row = gvLocHierarchy.Rows[gvLocHierarchy.SelectedRows[0].Index];
                        LoadCurrentAssignDetail(_row, false);
                    }
            }
            catch
            {

            }
        }
        private void DisplayValueToHierarchy(bool _isPc)
        {
            if (_isPc)
            {
                gvPcHierarchy.Rows.Clear();
                if (CompanyPcHierarchy == null) return;
                var _pcLst = CompanyPcHierarchy.Select(x => x.Mpi_pc_cd).Distinct();
                foreach (string _pc in _pcLst)
                {
                    int _count = 1;
                    object[] obj = new object[7];
                    var Lst = CompanyPcHierarchy.Where(x => x.Mpi_pc_cd == _pc && x.Mpi_cd != "COM").ToList();
                    if (Lst != null) if (Lst.Count > 0)
                            foreach (MasterSalesPriorityHierarchy _one in Lst)
                            {

                                string _type = _one.Mpi_cd;
                                string _value = _one.Mpi_val + " - " + FormulateDisplayText(_one.Description);
                                int _index = 0;

                                switch (_type)
                                {
                                    case "CHNL":
                                        _index = 4;
                                        _count += 1;
                                        break;
                                    case "SCHNL":
                                        _index = 5;
                                        _count += 1;
                                        break;
                                    case "AREA":
                                        _index = 3;
                                        _count += 1;
                                        break;
                                    case "REGION":
                                        _index = 2;
                                        break;
                                    case "ZONE":
                                        _index = 1;
                                        _count += 1;
                                        break;
                                    case "PC":
                                        _index = 0;
                                        _count += 1;
                                        break;
                                }

                                obj.SetValue(_value, _index);
                                if (_count == 6)
                                {
                                    gvPcHierarchy.AllowUserToAddRows = true;
                                    gvPcHierarchy.Rows.Insert(gvPcHierarchy.NewRowIndex, obj);
                                    gvPcHierarchy.AllowUserToAddRows = false;
                                    _count = 1;
                                }
                            }
                }
            }
            else
            {
                gvLocHierarchy.Rows.Clear();
                if (CompanyLocHierarchy == null) return;
                var _pcLst = CompanyLocHierarchy.Select(x => x.Mli_loc_cd).Distinct();
                foreach (string _pc in _pcLst)
                {
                    int _count = 1;
                    object[] obj = new object[7];
                    var Lst = CompanyLocHierarchy.Where(x => x.Mli_loc_cd == _pc && x.Mli_cd != "COM").ToList();
                    if (Lst != null) if (Lst.Count > 0)
                            foreach (MasterLocationPriorityHierarchy _one in Lst)
                            {

                                string _type = _one.Mli_cd;
                                string _value = _one.Mli_val + " - " + FormulateDisplayText(_one.Description);
                                int _index = 0;

                                switch (_type)
                                {
                                    case "CHNL":
                                        _index = 4;
                                        _count += 1;
                                        break;
                                    case "SCHNL":
                                        _index = 5;
                                        _count += 1;
                                        break;
                                    case "AREA":
                                        _index = 3;
                                        _count += 1;
                                        break;
                                    case "REGION":
                                        _index = 2;
                                        break;
                                    case "ZONE":
                                        _index = 1;
                                        _count += 1;
                                        break;
                                    case "LOC":
                                        _index = 0;
                                        _count += 1;
                                        break;
                                }

                                obj.SetValue(_value, _index);
                                if (_count == 6)
                                {
                                    gvLocHierarchy.AllowUserToAddRows = true;
                                    gvLocHierarchy.Rows.Insert(gvLocHierarchy.NewRowIndex, obj);
                                    gvLocHierarchy.AllowUserToAddRows = false;
                                    _count = 1;
                                }
                            }
                }
            }

        }
        CheckBox _checkboxHeader = new CheckBox();
        private void AddCheckBoxToGridHeader(bool _isInitial)
        {
            Rectangle rect = gvSearchedPc.GetCellDisplayRectangle(0, -1, false);
            int _height = 0; int _width = 0;
            if (_isInitial)
            {
                CheckBox _checkboxHeader = new CheckBox();
                _width = 20;
                _height = rect.Height;

                _height = (_height - 11) / 2;
                _width = (_width - 11) / 2;

                rect.Y = _height;
                rect.X = _width;

                _checkboxHeader.Name = "checkboxHeader";
                _checkboxHeader.FlatStyle = FlatStyle.Flat;
                _checkboxHeader.Size = new Size(11, 11);
                _checkboxHeader.Location = rect.Location;
                _checkboxHeader.Checked = true;
                _checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
                _checkboxHeader.BackColor = Color.Silver;
                gvSearchedPc.Controls.Add(_checkboxHeader);

            }
            else
            {
                _width = rect.Width;
                _height = rect.Height;

                _height = (_height - 11) / 2;
                _width = (_width - 11) / 2;

                rect.Y = _height;
                rect.X = _width;

                _checkboxHeader.Location = rect.Location;
                gvSearchedPc.Refresh();

            }

        }
        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            int _selectedRowIndex = gvSearchedPc.SelectedRows[0].Index;

            CheckBox headerBox = ((CheckBox)gvSearchedPc.Controls.Find("checkboxHeader", true)[0]);
            gvSearchedPc.Rows[0].Cells[0].Value = headerBox.Checked;
            for (int i = 0; i < gvSearchedPc.RowCount; i++)
            {
                gvSearchedPc.Rows[i].Cells[0].Value = headerBox.Checked;
            }
            if (gvSearchedPc.RowCount > 1)
            {
                gvSearchedPc.Rows[_selectedRowIndex + 1].Cells[0].Selected = true;
                gvSearchedPc.Rows[_selectedRowIndex].Cells[0].Selected = true;
            }
            else
            {
                gvSearchedPc.Rows[0].Cells[0].ReadOnly = true;
                gvSearchedPc.Rows[0].Cells[0].Value = headerBox.Checked;
                gvSearchedPc.Rows[0].Cells[0].ReadOnly = false;
            }
        }
        private void gvSearchedPc_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.Index == 0 && _isLoad == false)
            {
                AddCheckBoxToGridHeader(false);
                this.Refresh();
            }
        }
        private void gvSearchedPc_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                DataGridView _view = (DataGridView)sender;
                bool _bol = _view.Columns[0].Displayed;
                _checkboxHeader.Visible = _bol;
                Rectangle rectFA = gvSearchedPc.GetCellDisplayRectangle(0, -1, false);
                int _height = 0; int _width = 0;
                _width = rectFA.Width;
                _height = rectFA.Height;
                _height = (_height - 11) / 2;
                _width = (11 - e.NewValue);
                rectFA.Y = _height;
                rectFA.X = _width;
                _checkboxHeader.Size = new Size(rectFA.X, 11);
                gvSearchedPc.Refresh();
            }
        }
        private void ClearPcHierarchyDefinitionScreen()
        {
            ucProfitCenterCommonSearch.ClearScreen(BaseCls.GlbUserComCode);

            BindingSource _source = new BindingSource();
            _source.DataSource = null;
            gvSearchedPc.DataSource = _source;
            gvPcHierarchy.Rows.Clear();

            lblPcCur_area.Text = string.Empty;
            lblPcCur_channel.Text = string.Empty;
            lblPcCur_region.Text = string.Empty;
            lblPcCur_subchannel.Text = string.Empty;
            lblPcCur_zone.Text = string.Empty;

            ucProfitCenterNewAssign.ClearScreen(BaseCls.GlbUserComCode);

            _adhocPc = null;

        }
        private void ClearLocationHierarchyDefinitionScreen()
        {
            ucLoactionCommonSearch.Company = string.Empty;
            ucLoactionCommonSearch.Regien = string.Empty;
            ucLoactionCommonSearch.Area = string.Empty;
            ucLoactionCommonSearch.Zone = string.Empty;
            ucLoactionCommonSearch.SubChannel = string.Empty;
            ucLoactionCommonSearch.Channel = string.Empty;
            ucLoactionCommonSearch.Company = BaseCls.GlbUserComCode;

            BindingSource _source = new BindingSource();
            _source.DataSource = null;
            gvSearchedPc.DataSource = _source;
            gvLocHierarchy.Rows.Clear();

            lblLocCur_area.Text = string.Empty;
            lblLocCur_channel.Text = string.Empty;
            lblLocCur_region.Text = string.Empty;
            lblLocCur_subchannel.Text = string.Empty;
            lblLocCur_zone.Text = string.Empty;

            ucLocationNewAssign.Company = string.Empty;
            ucLocationNewAssign.Regien = string.Empty;
            ucLocationNewAssign.Area = string.Empty;
            ucLocationNewAssign.Zone = string.Empty;
            ucLocationNewAssign.SubChannel = string.Empty;
            ucLocationNewAssign.Channel = string.Empty;
            ucLocationNewAssign.Company = BaseCls.GlbUserComCode;

            _adhocLoc = null;

        }
        private void ClearReceiptTextBox()
        {
            txtRece_Code.Clear();
            txtRece_Description.Clear();
            txtRece_Code.Focus();
        }
        private void ClearProfitCenterCharges()
        {
            txtpcc_EPFRate.Clear();
            txtpcc_ESDRate.Clear();
            txtpcc_WHDTaxRate.Clear();

        }
        private void ClearTransactionPayType()
        {
            txttpt_brand.Clear();
            txttpt_ChargeRate.Clear();
            txttpt_ChargeValue.Clear();
            txttpt_Code.Clear();
            txttpt_Installment.Clear();
            txttpt_item.Clear();
            txttpt_maincat.Clear();
            txttpt_Promotion.Clear();
            txttpt_serial.Clear();
            txttpt_subcat.Clear();
            txttpt_TxnType.Clear();
            //txtCirc.Clear();

            cmbtpt_Book.Text = string.Empty;
            //cmbPayType.Text = string.Empty;
            for (int i = 0; i < chkLstPayType.Items.Count; i++)
            {
                chkLstPayType.SetItemChecked(i, false);
            }
            for (int i = 0; i < chkLstGV.Items.Count; i++)
            {
                chkLstGV.SetItemChecked(i, false);
            }
            for (int i = 0; i < chkLstScheme.Items.Count; i++)
            {
                chkLstScheme.SetItemChecked(i, false);
            }


        }
        private void ClearAdditionalProductBonus()
        {
            txtpab_appitem.Clear();
            txtpab_appvalue.Clear();
            txtpab_brand.Clear();
            txtpab_maincat.Clear();
            txtpab_subcat.Clear();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Do you need to clear the screen?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int _name = tabControl1.SelectedIndex;
            _adhocPc = new List<MasterProfitCenter>();
            while (this.Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            InitializeComponent();
            gvTransactionPayType.AutoGenerateColumns = false;
            gvAddBonus.AutoGenerateColumns = false;
            gvAddBonusDisplay.AutoGenerateColumns = false;
            gvPCCharge.AutoGenerateColumns = false;
            ProfitCenterCharge = new DataTable();
            _isLoad = true;
            gvSearchedPc.AutoGenerateColumns = false;
            ucProfitCenterCommonSearch.Company = BaseCls.GlbUserComCode;
            ucProfitCenterNewAssign.Company = BaseCls.GlbUserComCode;
            ucProfitCenterNewAssign.IsDisplayRawData = true;

            ucLoactionCommonSearch.Company = BaseCls.GlbUserComCode;
            ucLocationNewAssign.Company = BaseCls.GlbUserComCode;
            ucLocationNewAssign.IsDisplayRawData = true;
            if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
            {
                ucProfitCenterCommonSearch.IsAllProfitCenter = true;
            }
            if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
            {
                ucLoactionCommonSearch.IsAllLocation = true;
            }
            AddCheckBoxToGridHeader(true);
            _isLoad = false;

            tabControl1.SelectedIndex = _name;
            lblNoPaymode.Text = string.Empty;
            lblNoPc.Text = string.Empty;
            lstPC.Clear();
            txtCirc.Enabled = true;
            PBList = null;
            txtDiscount.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            BindSalesTypes();
            SalesType = null;
            txtPriceBook.Text = string.Empty;
            txtLevel.Text = string.Empty;
        }
        private void btnRece_Add_Click(object sender, EventArgs e)
        {
            if (gvSearchedPc.RowCount <= 0) { MessageBox.Show("Please select the profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtRece_Code.Text)) { MessageBox.Show("Please select the code", "Code", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRece_Code.Clear(); txtRece_Code.Focus(); return; }
            if (string.IsNullOrEmpty(txtRece_Description.Text)) { MessageBox.Show("Please select the description", "Description", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRece_Description.Clear(); txtRece_Description.Focus(); return; }

            var _duplicate = from DataGridViewRow _r in gvReceiptDivision.Rows where Convert.ToString(_r.Cells["rec_code"].Value) == txtRece_Code.Text.Trim() select _r;
            if (_duplicate != null) if (_duplicate.Count() > 0) { MessageBox.Show("This code already added", "Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (chkSetDefault.Checked) { var _duplicateDefault = from DataGridViewRow _r in gvReceiptDivision.Rows where Convert.ToString(_r.Cells["rec_isDefault"].Value) == "1" select _r; if (_duplicateDefault != null)   if (_duplicateDefault.Count() > 0) { MessageBox.Show("You can not add more than one default code", "Default Code", MessageBoxButtons.OK, MessageBoxIcon.Information); return; } }

            if (txtRece_Code.Text.ToString().ToUpper() == "ADVAN") { MessageBox.Show("Cannot define Receipt division as ADVAN", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            object[] obj = new object[4];

            obj.SetValue(txtRece_Code.Text.Trim(), 1);
            obj.SetValue(txtRece_Description.Text.Trim(), 2);
            obj.SetValue(chkSetDefault.Checked ? "1" : "0", 3);

            gvReceiptDivision.AllowUserToAddRows = true;
            int _newRowIndex = gvReceiptDivision.NewRowIndex;
            gvReceiptDivision.Rows.Insert(_newRowIndex, obj);
            gvReceiptDivision.AllowUserToAddRows = false;

            if (chkSetDefault.Checked)
            {
                gvReceiptDivision.Rows[_newRowIndex].DefaultCellStyle.BackColor = Color.Gold;
            }
            ClearReceiptTextBox();
        }
        private bool IsCodeAlreadyExist(string _code, string _profitcenter)
        {
            DataTable _table = CHNLSVC.General.GetAllReceiptDivision(ucProfitCenterCommonSearch.Company, _profitcenter);
            bool _yes = false;

            var _duplicate = _table.AsEnumerable().Where(x => x.Field<string>("msrd_cd") == _code).ToList();
            if (_duplicate != null)
                if (_duplicate.Count > 0)
                    _yes = true;

            return _yes;

        }
        private bool IsDefaultValueAvailable(string _company, string _profitcenter)
        {
            MasterReceiptDivision _defDivision = CHNLSVC.Sales.GetDefRecDivision(_company, _profitcenter);
            if (!string.IsNullOrEmpty(_defDivision.Msrd_cd))
                return true;
            else
                return false;
        }
        private void btnPcHierarchySave_Click(object sender, EventArgs e)
        {
            if (is_excel_upload == 1)
            {
                if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
                {
                    if (txtFrom.Value.Date == txtTo.Value.Date)
                    { MessageBox.Show("Please select the valid date period.", "Profit Center Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (txtFrom.Value.Date < CHNLSVC.Security.GetServerDateTime().Date)
                    {
                        DataTable _tbl = CHNLSVC.General.GetInforBackDate(ucProfitCenterCommonSearch.Company, "PC_INFO"); if (_tbl == null || _tbl.Rows.Count <= 0) { MessageBox.Show("Please select the valid date range.", "Profit Center Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        if (_tbl != null && _tbl.Rows.Count > 0)
                        {
                            int _day = _tbl.Rows[0].Field<Int16>("infbk_alw_days");
                            double _curdiff = (txtFrom.Value.Date - CHNLSVC.Security.GetServerDateTime().Date).TotalDays;
                            if (Convert.ToInt32(_curdiff) > _day)
                            { MessageBox.Show("Please select the valid date period.", "Profit Center Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        }
                    }
                    if (MessageBox.Show("Do you need to save this profit center hierarchy?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;


                    List<MasterSalesPriorityHierarchyLog> _pcInfoHeaders = new List<MasterSalesPriorityHierarchyLog>();
                    List<MasterSalesPriorityHierarchy> _pcInfoHeader = new List<MasterSalesPriorityHierarchy>();
                    var _r = from DataGridViewRow _rs in gvSearchedPc.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true select _rs;
                    if (_r == null || _r.Count() <= 0)
                    { MessageBox.Show("Please select the profit center which you have to assign hierarchy.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    foreach (var _row in _r)
                    {
                        MasterSalesPriorityHierarchyLog infos = new MasterSalesPriorityHierarchyLog();
                        MasterSalesPriorityHierarchy info = new MasterSalesPriorityHierarchy();
                        infos.Mpil_act = true; infos.Mpil_pc_cd = Convert.ToString(_row.Cells["p_pccode"].Value);
                        infos.Mpil_com_cd = ucProfitCenterCommonSearch.Company; infos.Mpil_tp = "PC_PRIT_HIERARCHY";
                        infos.Mpil_cr_by = BaseCls.GlbUserID; infos.Mpil_mod_by = BaseCls.GlbUserID;
                        infos.Mpil_frm_dt = txtFrom.Value.Date; infos.Mpil_to_dt = txtTo.Value.Date;
                        _pcInfoHeaders.Add(infos);
                        info.Mpi_act = true; info.Mpi_pc_cd = Convert.ToString(_row.Cells["p_pccode"].Value);
                        info.Mpi_com_cd = ucProfitCenterCommonSearch.Company; info.Mpi_tp = "PC_PRIT_HIERARCHY";
                        _pcInfoHeader.Add(info);

                    }

                    string _msg = string.Empty;
                    Int32 eff = CHNLSVC.General.Save_MST_PC_INFO_LOG_Excle_upload(_pcInfoHeaders, _list, out _msg);
                    if (eff > 0)
                    {
                        MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); ClearPcHierarchyDefinitionScreen();
                        is_excel_upload = 0;
                        txtexcle.Text = "";
                        label100.Text = "";
                        lblexclestates.Visible = false;
                        return;
                    }
                    else
                    { MessageBox.Show("Please try again." + _msg, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


                }

                if (tabControl1.SelectedTab.Name == tpLocHierarchy.Name)
                {
                    if (txtFrom.Value.Date < CHNLSVC.Security.GetServerDateTime().Date)
                    {
                        DataTable _tbl = CHNLSVC.General.GetInforBackDate(ucProfitCenterCommonSearch.Company, "LOC_INFO"); if (_tbl == null || _tbl.Rows.Count <= 0) { MessageBox.Show("Please select the valid date range.", "Location Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        if (_tbl != null && _tbl.Rows.Count > 0)
                        {
                            int _day = _tbl.Rows[0].Field<Int16>("infbk_alw_days");
                            double _curdiff = (txtTo.Value.Date - txtFrom.Value.Date).TotalDays;
                            if (Convert.ToInt32(_curdiff) > _day)
                            { MessageBox.Show("Please select the valid date period.", "Location Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        }
                    }
                    if (txtFrom.Value.Date == txtTo.Value.Date)
                    { MessageBox.Show("Please select the valid date period.", "Location Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (MessageBox.Show("Do you need to save this location hierarchy?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    List<MasterLocationPriorityHierarchyLog> _locInfoHeaders = new List<MasterLocationPriorityHierarchyLog>();
                    var _r = from DataGridViewRow _rs in gvSearchedPc.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true select _rs;
                    if (_r == null || _r.Count() <= 0)
                    { MessageBox.Show("Please select the profit center which you have to assign hierarchy.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    foreach (var _row in _r)
                    {
                        MasterLocationPriorityHierarchyLog info = new MasterLocationPriorityHierarchyLog();
                        info.Mlil_act = true; info.Mlil_loc_cd = Convert.ToString(_row.Cells["p_pccode"].Value);
                        info.Mlil_com_cd = ucProfitCenterCommonSearch.Company; info.Mlil_tp = "LOC_PRIT_HIERARCHY";
                        info.Mlil_cr_by = BaseCls.GlbUserID; info.Mlil_mod_by = BaseCls.GlbUserID;
                        info.Mlil_frm_dt = txtFrom.Value.Date; info.Mlil_to_dt = txtTo.Value.Date;
                        _locInfoHeaders.Add(info);
                    }

                    string _msg = string.Empty;
                    Int32 eff = CHNLSVC.General.save_mst_loc_info_log_exle(_locInfoHeaders, _list, out _msg);
                    if (eff > 0)
                    {
                        MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); ClearPcHierarchyDefinitionScreen();
                        lblexclestates.Visible = false;
                        txtexle.Text = "";
                        label103.Text = "";
                        return;
                    }
                    else
                    { MessageBox.Show("Please try again." + _msg, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


                }


            }

            #region Normal save
            else
            {
                if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
                {
                    if (txtFrom.Value.Date == txtTo.Value.Date)
                    { MessageBox.Show("Please select the valid date period.", "Profit Center Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (txtFrom.Value.Date < CHNLSVC.Security.GetServerDateTime().Date)
                    {
                        DataTable _tbl = CHNLSVC.General.GetInforBackDate(ucProfitCenterCommonSearch.Company, "PC_INFO"); if (_tbl == null || _tbl.Rows.Count <= 0) { MessageBox.Show("Please select the valid date range.", "Profit Center Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        if (_tbl != null && _tbl.Rows.Count > 0)
                        {
                            int _day = _tbl.Rows[0].Field<Int16>("infbk_alw_days");
                            double _curdiff = (txtFrom.Value.Date - CHNLSVC.Security.GetServerDateTime().Date).TotalDays;
                            if (Convert.ToInt32(_curdiff) > _day)
                            { MessageBox.Show("Please select the valid date period.", "Profit Center Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        }
                    }
                    if (MessageBox.Show("Do you need to save this profit center hierarchy?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Zone))
                    { MessageBox.Show("Please select the zone.", "Zone", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Regien))
                    { MessageBox.Show("Please select the region.", "Region", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Area))
                    { MessageBox.Show("Please select the area.", "Area", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (string.IsNullOrEmpty(ucProfitCenterNewAssign.SubChannel))
                    { MessageBox.Show("Please select the sub channel.", "Sub Channel", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                    if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Channel))
                    { MessageBox.Show("Please select the channel.", "Channel", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Company))
                    { MessageBox.Show("Please select the company.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                    // Nadeeka  21-05-2015
                    //if (CHNLSVC.Sales.CheckAssignChannel( ucProfitCenterNewAssign.SubChannel,ucProfitCenterNewAssign.Company) == 0)
                    //{
                    //    if (MessageBox.Show("Default Price defintion not setup to selected sub channel, Are you sure you want to continue?", "Definition", MessageBoxButtons.YesNo) == DialogResult.No) return;

                    //}


                    List<MasterSalesPriorityHierarchyLog> _pcInfoHeaders = new List<MasterSalesPriorityHierarchyLog>();
                    List<MasterSalesPriorityHierarchy> _pcInfoHeader = new List<MasterSalesPriorityHierarchy>();
                    var _r = from DataGridViewRow _rs in gvSearchedPc.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true select _rs;
                    if (_r == null || _r.Count() <= 0)
                    { MessageBox.Show("Please select the profit center which you have to assign hierarchy.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    foreach (var _row in _r)
                    {
                        MasterSalesPriorityHierarchyLog infos = new MasterSalesPriorityHierarchyLog();
                        MasterSalesPriorityHierarchy info = new MasterSalesPriorityHierarchy();
                        infos.Mpil_act = true; infos.Mpil_pc_cd = Convert.ToString(_row.Cells["p_pccode"].Value);
                        infos.Mpil_com_cd = ucProfitCenterCommonSearch.Company; infos.Mpil_tp = "PC_PRIT_HIERARCHY";
                        infos.Mpil_cr_by = BaseCls.GlbUserID; infos.Mpil_mod_by = BaseCls.GlbUserID;
                        infos.Mpil_frm_dt = txtFrom.Value.Date; infos.Mpil_to_dt = txtTo.Value.Date;
                        _pcInfoHeaders.Add(infos);
                        info.Mpi_act = true; info.Mpi_pc_cd = Convert.ToString(_row.Cells["p_pccode"].Value);
                        info.Mpi_com_cd = ucProfitCenterCommonSearch.Company; info.Mpi_tp = "PC_PRIT_HIERARCHY";
                        _pcInfoHeader.Add(info);

                    }
                    Dictionary<string, string> code_and_value = new Dictionary<string, string>();
                    code_and_value.Add("ZONE", ucProfitCenterNewAssign.Zone.Trim().ToUpper());
                    code_and_value.Add("REGION", ucProfitCenterNewAssign.Regien.Trim().ToUpper());
                    code_and_value.Add("AREA", ucProfitCenterNewAssign.Area.Trim().ToUpper());
                    code_and_value.Add("SCHNL", ucProfitCenterNewAssign.SubChannel.Trim().ToUpper());
                    code_and_value.Add("CHNL", ucProfitCenterNewAssign.Channel.Trim().ToUpper());
                    code_and_value.Add("COM", ucProfitCenterCommonSearch.Company.Trim().ToUpper());
                    code_and_value.Add("PC", string.Empty);
                    code_and_value.Add("GPC", "GRUP01");
                    string _msg = string.Empty;
                    Int32 eff = CHNLSVC.General.Save_MST_PC_INFO_LOG(_pcInfoHeaders, code_and_value, out _msg);
                    if (eff > 0)
                    {
                        MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); ClearPcHierarchyDefinitionScreen(); return;
                    }
                    else
                    { MessageBox.Show("Please try again." + _msg, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                }
                if (tabControl1.SelectedTab.Name == tpLocHierarchy.Name)
                {
                    if (txtFrom.Value.Date < CHNLSVC.Security.GetServerDateTime().Date)
                    {
                        DataTable _tbl = CHNLSVC.General.GetInforBackDate(ucProfitCenterCommonSearch.Company, "LOC_INFO"); if (_tbl == null || _tbl.Rows.Count <= 0) { MessageBox.Show("Please select the valid date range.", "Location Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        if (_tbl != null && _tbl.Rows.Count > 0)
                        {
                            int _day = _tbl.Rows[0].Field<Int16>("infbk_alw_days");
                            double _curdiff = (txtTo.Value.Date - txtFrom.Value.Date).TotalDays;
                            if (Convert.ToInt32(_curdiff) > _day)
                            { MessageBox.Show("Please select the valid date period.", "Location Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        }
                    }
                    if (txtFrom.Value.Date == txtTo.Value.Date)
                    { MessageBox.Show("Please select the valid date period.", "Location Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (MessageBox.Show("Do you need to save this location hierarchy?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    List<MasterLocationPriorityHierarchyLog> _locInfoHeaders = new List<MasterLocationPriorityHierarchyLog>();
                    var _r = from DataGridViewRow _rs in gvSearchedPc.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true select _rs;
                    if (_r == null || _r.Count() <= 0)
                    { MessageBox.Show("Please select the profit center which you have to assign hierarchy.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    foreach (var _row in _r)
                    {
                        MasterLocationPriorityHierarchyLog info = new MasterLocationPriorityHierarchyLog();
                        info.Mlil_act = true; info.Mlil_loc_cd = Convert.ToString(_row.Cells["p_pccode"].Value);
                        info.Mlil_com_cd = ucProfitCenterCommonSearch.Company; info.Mlil_tp = "LOC_PRIT_HIERARCHY";
                        info.Mlil_cr_by = BaseCls.GlbUserID; info.Mlil_mod_by = BaseCls.GlbUserID;
                        info.Mlil_frm_dt = txtFrom.Value.Date; info.Mlil_to_dt = txtTo.Value.Date;
                        _locInfoHeaders.Add(info);
                    }
                    Dictionary<string, string> code_and_value = new Dictionary<string, string>();
                    code_and_value.Add("ZONE", ucLocationNewAssign.Zone.Trim().ToUpper());
                    code_and_value.Add("REGION", ucLocationNewAssign.Regien.Trim().ToUpper());
                    code_and_value.Add("AREA", ucLocationNewAssign.Area.Trim().ToUpper());
                    code_and_value.Add("SCHNL", ucLocationNewAssign.SubChannel.Trim().ToUpper());
                    code_and_value.Add("CHNL", ucLocationNewAssign.Channel.Trim().ToUpper());
                    code_and_value.Add("COM", ucLocationNewAssign.Company.Trim().ToUpper());
                    code_and_value.Add("LOC", string.Empty);
                    code_and_value.Add("GPC", "GRUP01");
                    string _msg = string.Empty;
                    Int32 eff = CHNLSVC.General.save_mst_loc_info_log(_locInfoHeaders, code_and_value, out _msg);
                    if (eff > 0)
                    { MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); ClearPcHierarchyDefinitionScreen(); return; }
                    else
                    { MessageBox.Show("Please try again." + _msg, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                }
                if (tabControl1.SelectedTab.Name == tpPcReceiptCategory.Name)
                {
                    if (MessageBox.Show("Do you need to save this receipt division?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    var _r = from DataGridViewRow _rs in gvSearchedPc.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true select _rs;
                    if (_r == null || _r.Count() <= 0)
                    { MessageBox.Show("Please select the profit center which you have to assign.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (gvReceiptDivision.RowCount <= 0)
                    { MessageBox.Show("Please select the codes that you need to assign for the profit center", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    List<string> pcList = new List<string>();
                    foreach (var _row in _r)
                    { string _profCenter = Convert.ToString(_row.Cells["p_pccode"].Value); pcList.Add(_profCenter); }
                    List<MasterReceiptDivision> _recDivList = new List<MasterReceiptDivision>();
                    foreach (DataGridViewRow _row in gvReceiptDivision.Rows)
                    {
                        MasterReceiptDivision _one = new MasterReceiptDivision();
                        _one.Msrd_cd = Convert.ToString(_row.Cells["rec_code"].Value);
                        _one.Msrd_com = ucProfitCenterCommonSearch.Company; _one.Msrd_cre_by = BaseCls.GlbUserID;
                        _one.Msrd_desc = Convert.ToString(_row.Cells["rec_description"].Value); _one.Msrd_div_tp = "INTERNAL";
                        _one.Msrd_inv_tp = "CRED2"; _one.Msrd_is_def = Convert.ToBoolean(Convert.ToString(_row.Cells["rec_isDefault"].Value) == "1" ? true : false);
                        _one.Msrd_is_sales = true; _one.Msrd_is_ser = false;
                        _one.Msrd_mod_by = BaseCls.GlbUserID; _one.Msrd_stus = true;
                        _recDivList.Add(_one);
                    }
                    foreach (string _one in pcList)
                    {
                        var _duplicate = _recDivList.Where(x => IsCodeAlreadyExist(x.Msrd_cd, _one)).ToList();
                        if (_duplicate != null)
                            if (_duplicate.Count > 0)
                            { MessageBox.Show(_duplicate[0].Msrd_cd + " is already available in the previous collection for the profit center " + _duplicate[0].Msrd_com, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    }
                    foreach (string _pc in pcList)
                    {
                        bool _isDefExist = IsDefaultValueAvailable(ucProfitCenterCommonSearch.Company, _pc);
                        var _IsCollectionHvDefault = _recDivList.Where(x => x.Msrd_com == Convert.ToString(ucProfitCenterCommonSearch.Company) && x.Msrd_pc == Convert.ToString(_pc) && x.Msrd_is_def).ToList();
                        if (_isDefExist && _IsCollectionHvDefault != null && _IsCollectionHvDefault.Count > 0)
                        { MessageBox.Show(_pc + " profit center already available default value.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    }
                    Int32 eff = CHNLSVC.General.Save_MST_REC_DIV(_recDivList, pcList);
                    if (eff > 0)
                    {
                        gvReceiptDivision.Rows.Clear();
                        MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearReceiptTextBox(); gvReceiptDivision.DataSource = null;
                        gvReceiptDivisionAll.DataSource = null; ClearPcHierarchyDefinitionScreen();
                    }
                    else
                    { MessageBox.Show("Not Saved. Try again!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                }
                if (tabControl1.SelectedTab.Name == tpPcCharges.Name)
                {
                    if (MessageBox.Show("Do you need to save this profit center charges?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    var _r = from DataGridViewRow _rs in gvSearchedPc.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true select _rs;
                    if (_r == null || _r.Count() <= 0)
                    { MessageBox.Show("Please select the profit center which you have to assign.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (dtppcc_Fromdate.Value.Date > dtppcc_Todate.Value.Date)
                    { MessageBox.Show("Please select the valid date range", "Date Range", MessageBoxButtons.OK, MessageBoxIcon.Information); dtppcc_Fromdate.Focus(); return; }
                    if (string.IsNullOrEmpty(txtpcc_EPFRate.Text))
                    { MessageBox.Show("Please select the EPF rate", "EPF Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); txtpcc_EPFRate.Focus(); return; }
                    else if (Convert.ToDecimal(txtpcc_EPFRate.Text) > 100)
                    { MessageBox.Show("Please select the valid EPF rate", "EPF Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); txtpcc_EPFRate.Focus(); return; }
                    if (string.IsNullOrEmpty(txtpcc_ESDRate.Text))
                    { MessageBox.Show("Please select the ESD rate", "ESD Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); txtpcc_ESDRate.Focus(); return; }
                    else if (Convert.ToDecimal(txtpcc_ESDRate.Text) > 100)
                    { MessageBox.Show("Please select the valid ESD rate", "ESD Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); txtpcc_ESDRate.Focus(); return; }
                    if (string.IsNullOrEmpty(txtpcc_WHDTaxRate.Text))
                    { MessageBox.Show("Please select the WHD tax rate", "WHD Tax Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); txtpcc_WHDTaxRate.Focus(); return; }
                    else if (Convert.ToDecimal(txtpcc_WHDTaxRate.Text) > 100)
                    { MessageBox.Show("Please select the valid WHD tax rate", "WHD Tax Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); txtpcc_WHDTaxRate.Focus(); return; }
                    List<string> pcList = new List<string>();
                    foreach (var _row in _r)
                    { string _profCenter = Convert.ToString(_row.Cells["p_pccode"].Value); pcList.Add(_profCenter); }
                    List<MasterProfitCenterCharges> pc_chg_List = new List<MasterProfitCenterCharges>();
                    foreach (string pc in pcList)
                    {
                        MasterProfitCenterCharges chg = new MasterProfitCenterCharges();
                        chg.Mpch_com = ucProfitCenterCommonSearch.Company; chg.Mpch_cre_by = BaseCls.GlbUserID;
                        chg.Mpch_cre_dt = DateTime.Now; chg.Mpch_epf = Convert.ToDecimal(txtpcc_EPFRate.Text);
                        chg.Mpch_esd = Convert.ToDecimal(txtpcc_ESDRate.Text); chg.Mpch_from_dt = dtppcc_Fromdate.Value.Date;
                        chg.Mpch_pc = pc; chg.Mpch_to_dt = dtppcc_Todate.Value.Date;
                        chg.Mpch_wht = Convert.ToDecimal(txtpcc_WHDTaxRate.Text);
                        pc_chg_List.Add(chg);
                    }
                    Int32 eff = CHNLSVC.General.Save_MST_PC_CHG(pc_chg_List);
                    if (eff > 0)
                    { MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); ClearProfitCenterCharges(); ClearPcHierarchyDefinitionScreen(); gvPCCharge.DataSource = null; }
                    else
                    { MessageBox.Show("Not Saved. Try again!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                }
                //modified by kapila
                if (tabControl1.SelectedTab.Name == tpPcTransactionPayTypes.Name)
                {
                    if (MessageBox.Show("Do you need to save this profit center transaction type?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                    if (gvTransactionPayType.Rows.Count <= 0)
                    {
                        MessageBox.Show("Please select the transaction type parameter", "Transaction Parameter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    Boolean _is_new = false;
                    if (btnPcHierarchySave.Text == "Save")
                        _is_new = true;
                    else
                        _is_new = false;

                    List<string> pcList = new List<string>();
                    foreach (ListViewItem Item in lstPC.Items)  //kapila 5/8/2014
                    {
                        string pc = Item.Text;

                        if (Item.Checked == true)
                            pcList.Add(pc);
                    }
                    if (pcList.Count == 0)
                    {
                        MessageBox.Show("Please select the Sub channel/profit center which you have to assign.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Int32 eff = 0;
                    List<PaymentType> _tp = new List<PaymentType>();
                    int _count = 0;
                    bool _isSaved = false;

                    if (PaymentTxnList.Count > 1000)
                        foreach (PaymentType t in PaymentTxnList)
                        {
                            _tp.Add(t);
                            _count += 1;
                            if (_count == 250)
                            {

                                eff += CHNLSVC.General.Save_SAR_TXN_PAY_TP(_tp, pcList, _is_new);
                                _count = 0;
                                _tp = new List<PaymentType>();
                                _isSaved = true;
                            }

                        }
                    if (!_isSaved)
                        eff += CHNLSVC.General.Save_SAR_TXN_PAY_TP(PaymentTxnList, pcList, _is_new);

                    if (eff != 0 || eff > 0)
                    { ClearTransactionPayType(); txtCirc.Clear(); lstPC.Clear(); PaymentTxnList = new List<PaymentType>(); BindingSource _source = new BindingSource(); _source.DataSource = PaymentTxnList; gvTransactionPayType.DataSource = _source; MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else
                    { MessageBox.Show("Not Saved. Try again!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                }
                if (tabControl1.SelectedTab.Name == tpPcAdditionalBonus.Name)
                {
                    if (MessageBox.Show("Do you need to save this profit center product bonus?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    var _r = from DataGridViewRow _rs in gvSearchedPc.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true select _rs;
                    if (_r == null || _r.Count() <= 0)
                    { MessageBox.Show("Please select the profit center which you have to assign.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (gvAddBonus.Rows.Count <= 0)
                    { MessageBox.Show("Please select the product bonus parameters", "Bonus Parameter", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    List<string> pcList = new List<string>();
                    foreach (var _row in _r)
                    { string _profCenter = Convert.ToString(_row.Cells["p_pccode"].Value); pcList.Add(_profCenter); }
                    Int32 eff = CHNLSVC.Sales.SaveAdditionalProductBonus(_additionalProductBonus, pcList);
                    if (eff > 0)
                    {
                        _additionalProductBonus = new List<MasterAdditionalProductBonus>();
                        BindingSource _source = new BindingSource(); _source.DataSource = _additionalProductBonus;
                        gvAddBonus.DataSource = _source; ClearAdditionalProductBonus();
                        MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        List<MasterAdditionalProductBonus> _lst = CHNLSVC.Sales.GetAllProductBonusSetup(ucProfitCenterCommonSearch.Company);
                        BindingSource _source1 = new BindingSource(); _source1.DataSource = _lst;
                        gvAddBonusDisplay.DataSource = _source1;
                    }
                    else
                    { MessageBox.Show("Not Saved. Try again!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                }
            }
            #endregion
        }
        private void gvReceiptDivision_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvReceiptDivision.RowCount > 0)
                if (e.RowIndex != -1)
                    if (gvReceiptDivision.Columns[e.ColumnIndex].Name == "rec_delete")
                    {
                        if (MessageBox.Show("Do you need to remove this record?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            return;
                        gvReceiptDivision.Rows.RemoveAt(e.RowIndex);
                    }

        }
        private void txtpcc_ESDRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(true, sender, e);
        }
        private void txtpcc_EPFRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(true, sender, e);
        }
        private void txtpcc_WHDTaxRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(true, sender, e);
        }
        private void btnSearch_tptTxnType_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransactionType);
            DataTable _result = CHNLSVC.CommonSearch.SearchTransactionType(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txttpt_TxnType;
            _CommonSearch.ShowDialog();
            txttpt_TxnType.Select();
            getTransType();
        }

        private void getTransType()
        {
            DataTable _dt = CHNLSVC.Sales.GetReceiptType(txttpt_TxnType.Text);
            if (txttpt_TxnType.Text != "CS")
            {
                if (_dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(_dt.Rows[0]["msrt_is_hp_rec"]) == 0)
                    {
                        _is_hp_rec = false;
                        chkLstScheme.Visible = false;
                        for (int i = 0; i < chkLstScheme.Items.Count; i++)
                        {
                            chkLstScheme.SetItemChecked(i, false);
                        }
                    }
                    else
                    {
                        _is_hp_rec = true;

                    }
                }
                else
                {
                    _is_hp_rec = false;
                    chkLstScheme.Visible = false;
                    for (int i = 0; i < chkLstScheme.Items.Count; i++)
                    {
                        chkLstScheme.SetItemChecked(i, false);
                    }
                }
            }
            else
            {
                _is_hp_rec = false;
                chkLstScheme.Visible = false;
                for (int i = 0; i < chkLstScheme.Items.Count; i++)
                {
                    chkLstScheme.SetItemChecked(i, false);
                }
            }
        }

        private void btnSearch_tptItem_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txttpt_item;
            _CommonSearch.ShowDialog();
            txttpt_item.Select();
        }
        private void btnSearch_tptBrand_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txttpt_brand;
            _CommonSearch.ShowDialog();
            txttpt_brand.Select();
        }
        private void btnSearch_tptMainCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txttpt_maincat;
            _CommonSearch.ShowDialog();
            txttpt_maincat.Select();
        }
        private void btnSearch_tptSubCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txttpt_subcat;
            _CommonSearch.ShowDialog();
            txttpt_subcat.Select();
        }
        private void btnSearch_tptPromotion_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
            DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txttpt_Promotion;
            _CommonSearch.ShowDialog();
            txttpt_Promotion.Select();
        }
        private void btnSearch_tptBank_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txttpt_Code;
            _CommonSearch.ShowDialog();
            txttpt_Code.Select();
        }
        private void txttpt_TxnType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_tptTxnType_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                chkLstPayType.Focus();  //cmbPayType.Focus();
        }
        private void cmbPayType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtptpt_fromdate.Focus();
        }
        private void dtptpt_fromdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtptpt_todate.Focus();
        }
        private void dtptpt_todate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                chktpt_SetAsDefault.Focus();
        }
        private void chktpt_SetAsDefault_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txttpt_item.Focus();
        }
        private void txttpt_item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txttpt_brand.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_tptItem_Click(null, null);
        }
        private void txttpt_TxnType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_tptTxnType_Click(null, null);
        }
        private void txttpt_item_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_tptItem_Click(null, null);
        }
        private void txttpt_brand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txttpt_maincat.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_tptBrand_Click(null, null);
        }
        private void txttpt_maincat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txttpt_subcat.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_tptMainCat_Click(null, null);
        }
        private void txttpt_brand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_tptBrand_Click(null, null);
        }
        private void txttpt_maincat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_tptMainCat_Click(null, null);
        }
        private void txttpt_subcat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_tptSubCat_Click(null, null);
        }
        private void txttpt_subcat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txttpt_serial.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_tptSubCat_Click(null, null);
        }
        private void txttpt_serial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbtpt_Book.Focus();
        }
        private void cmbtpt_Book_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbtpt_Level.Focus();
        }
        private void cmbtpt_Level_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txttpt_Promotion.Focus();
        }
        private void txttpt_Promotion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_tptPromotion_Click(null, null);
        }
        private void txttpt_Promotion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txttpt_Code.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_tptPromotion_Click(null, null);
        }
        private void txttpt_Code_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_tptBank_Click(null, null);
        }
        private void txttpt_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txttpt_Installment.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_tptBank_Click(null, null);
        }
        private void txttpt_Installment_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(true, sender, e);
        }
        private void txttpt_ChargeRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(true, sender, e);
        }
        private void txttpt_ChargeValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(true, sender, e);
        }
        private void txttpt_Installment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txttpt_ChargeRate.Focus();
        }
        private void txttpt_ChargeRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txttpt_ChargeValue.Focus();

        }
        private void txttpt_ChargeValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btntpt_Add.Focus();
        }
        private void cmbtpt_Book_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbtpt_Book.Text)) return;
            LoadLevel(cmbtpt_Book.Text);
        }
        private void txttpt_TxnType_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttpt_TxnType.Text)) return;
        }
        private void txttpt_item_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttpt_item.Text)) return;
        }
        private void txttpt_brand_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttpt_brand.Text)) return;
        }
        private void txttpt_maincat_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttpt_maincat.Text)) return;
        }
        private void txttpt_subcat_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttpt_subcat.Text)) return;
            if (string.IsNullOrEmpty(txttpt_maincat.Text)) { MessageBox.Show("Please select the main category", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information); txttpt_subcat.Clear(); txttpt_maincat.Focus(); return; }
        }
        private void txttpt_Promotion_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttpt_Promotion.Text)) return;
        }
        private void txttpt_Code_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttpt_Code.Text)) return;
        }
        private void txttpt_serial_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttpt_serial.Text)) return;
            if (string.IsNullOrEmpty(txttpt_item.Text)) { MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); txttpt_serial.Clear(); txttpt_item.Focus(); return; }
        }
        private void cmbtpt_Level_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbtpt_Level.Text)) return;
            if (string.IsNullOrEmpty(cmbtpt_Book.Text)) { MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); cmbtpt_Level.Text = string.Empty; cmbtpt_Book.Focus(); return; }
        }
        static int counter = 1;


        private void btntpt_Add_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(cmbtpt_Level.Text) && string.IsNullOrEmpty(cmbtpt_Book.Text))
            {
                MessageBox.Show("You have to select price level with a price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime fromDt = Convert.ToDateTime(dtptpt_fromdate.Value.Date);
            DateTime toDt = Convert.ToDateTime(dtptpt_todate.Value.Date);
            if (toDt < fromDt)
            {
                MessageBox.Show("'To Date' should be greater than 'From Date'!", "Date Range", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (chkLstPayType.CheckedItems.Count <= 0)
            {
                MessageBox.Show("Please select the pay type", "Pay Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtCirc.Text))     //kapila 5/8/2014
            {
                MessageBox.Show("Please enter circular number", "Pay Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ////////////////////

            // Nadeeka 06-06-2015
            string _pb = string.Empty;
            string _lvl = string.Empty;
            string _item = string.Empty;
            string _serial = string.Empty;
            string _promo = string.Empty;
            string _brand = string.Empty;
            string _subcate = string.Empty;
            string _mcate = string.Empty;



            _pb = cmbtpt_Book.Text;
            _lvl = cmbtpt_Level.Text;
            _item = txttpt_item.Text;
            _serial = txttpt_serial.Text;
            _promo = txttpt_Promotion.Text;
            _brand = txttpt_brand.Text;
            _subcate = txttpt_subcat.Text;
            _mcate = txttpt_maincat.Text;


            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_item) && !string.IsNullOrEmpty(_serial) && !string.IsNullOrEmpty(_promo) && !string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_subcate) && !string.IsNullOrEmpty(_mcate))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 4 Combinations
            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_item) && !string.IsNullOrEmpty(_serial) && (!string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_brand) || !string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_mcate)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_subcate) && (!string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_serial) || !string.IsNullOrEmpty(_mcate)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_mcate) && (!string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_serial) || !string.IsNullOrEmpty(_subcate)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3 Combinations
            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_item) && !string.IsNullOrEmpty(_serial) && string.IsNullOrEmpty(_lvl) && (!string.IsNullOrEmpty(_mcate) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_brand) || !string.IsNullOrEmpty(_subcate)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_item) && string.IsNullOrEmpty(_serial) && !string.IsNullOrEmpty(_lvl) && (!string.IsNullOrEmpty(_mcate) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_brand) || !string.IsNullOrEmpty(_subcate)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_brand) && string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_subcate) && (!string.IsNullOrEmpty(_mcate) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_serial)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (!string.IsNullOrEmpty(_pb) && string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_subcate) && (!string.IsNullOrEmpty(_mcate) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_serial)))
            //{
            //    MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_brand) && string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_mcate) && (!string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_serial)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //if (!string.IsNullOrEmpty(_pb) && string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_mcate) && (!string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_serial)))
            //{
            //    MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_lvl) && string.IsNullOrEmpty(_mcate) && (!string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_serial)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // 2

            if (string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_serial) && string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_item) && (!string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_mcate) || !string.IsNullOrEmpty(_brand)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_pb) && string.IsNullOrEmpty(_serial) && string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_item) && (!string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_mcate) || !string.IsNullOrEmpty(_brand)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_pb) && string.IsNullOrEmpty(_brand) && string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_subcate) && (!string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_mcate) || !string.IsNullOrEmpty(_serial)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(_pb) && string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_mcate) && (!string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_serial)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_pb) && string.IsNullOrEmpty(_brand) && string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_mcate) && (!string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_serial)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_brand) && string.IsNullOrEmpty(_lvl) && string.IsNullOrEmpty(_mcate) && (!string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_promo) || !string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_serial)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //1

            if (!string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_item) && string.IsNullOrEmpty(_serial) && !string.IsNullOrEmpty(_promo) && !string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_subcate) && !string.IsNullOrEmpty(_mcate))
            {
                MessageBox.Show("Can't define only for serial", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_pb) && !string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_item) && !string.IsNullOrEmpty(_serial) && !string.IsNullOrEmpty(_promo) && !string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_subcate) && !string.IsNullOrEmpty(_mcate))
            {
                MessageBox.Show("Can't define only for Price Book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_pb) && string.IsNullOrEmpty(_lvl) && !string.IsNullOrEmpty(_item) && !string.IsNullOrEmpty(_serial) && !string.IsNullOrEmpty(_promo) && !string.IsNullOrEmpty(_brand) && !string.IsNullOrEmpty(_subcate) && !string.IsNullOrEmpty(_mcate))
            {
                MessageBox.Show("Can't define only for Price Level", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Promotion
            if (!string.IsNullOrEmpty(_promo) && (!string.IsNullOrEmpty(_lvl) || !string.IsNullOrEmpty(_item) || !string.IsNullOrEmpty(_serial) || !string.IsNullOrEmpty(_pb) || !string.IsNullOrEmpty(_brand) || !string.IsNullOrEmpty(_subcate) || !string.IsNullOrEmpty(_mcate)))
            {
                MessageBox.Show("Incorrect combination", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
            }



            /////////////////



            if (!string.IsNullOrEmpty(txttpt_item.Text))
            {
                if (!IsItemExcel)
                {
                    ItemList _one = new ItemList();
                    _one.Item = txttpt_item.Text.Trim();
                    _one.Serial = txttpt_serial.Text.Trim();
                    if (_itemLst == null || _itemLst.Count <= 0) _itemLst = new List<ItemList>();
                    _itemLst.Add(_one);
                }

                else if (IsItemExcel)
                {
                    ItemList _one = new ItemList();
                    _one.Item = txttpt_item.Text.Trim();
                    _one.Serial = string.Empty;
                    if (_itemLst == null || _itemLst.Count <= 0) _itemLst = new List<ItemList>();
                    _itemLst.Add(_one);
                }
            }

            //kapila 4/6/2015 select only GVO
            if (_is_GVO_sel == true && chkLstGV.CheckedItems.Count > 0)
            {
                //if (chkLstGV.CheckedItems.Count > 0)
                //{
                if (chkLstScheme.CheckedItems.Count > 0)
                {
                    foreach (object GVChecked in chkLstGV.CheckedItems)
                    {
                        DataRowView castedItem = GVChecked as DataRowView;
                        foreach (object SchChecked in chkLstScheme.CheckedItems)
                        {
                            DataRowView castedSch = SchChecked as DataRowView;
                            for (int i = 0; i < chkLstPayType.CheckedItems.Count; i++)
                            {
                                if (_itemLst == null || _itemLst.Count <= 0)
                                    addPaymentList(i, txttpt_item.Text.Trim().ToUpper(), txttpt_serial.Text.Trim(), castedItem["mi_cd"].ToString(), castedSch["hsd_cd"].ToString());
                                else
                                {
                                    foreach (ItemList _itm in _itemLst)
                                    {
                                        txttpt_item.Text = _itm.Item;
                                        txttpt_serial.Text = _itm.Serial;
                                        addPaymentList(i, txttpt_item.Text.Trim().ToUpper(), txttpt_serial.Text.Trim(), castedItem["mi_cd"].ToString(), castedSch["hsd_cd"].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (object GVChecked in chkLstGV.CheckedItems)
                    {
                        DataRowView castedItem = GVChecked as DataRowView;
                        for (int i = 0; i < chkLstPayType.CheckedItems.Count; i++)
                        {
                            if (_itemLst == null || _itemLst.Count <= 0)
                                addPaymentList(i, txttpt_item.Text.Trim().ToUpper(), txttpt_serial.Text.Trim(), castedItem["mi_cd"].ToString(), "");
                            else
                            {
                                foreach (ItemList _itm in _itemLst)
                                {
                                    txttpt_item.Text = _itm.Item;
                                    txttpt_serial.Text = _itm.Serial;
                                    addPaymentList(i, txttpt_item.Text.Trim().ToUpper(), txttpt_serial.Text.Trim(), castedItem["mi_cd"].ToString(), "");
                                }
                            }
                        }
                    }
                }
                //}

            }
            else
            {
                if (_itemLst == null || _itemLst.Count <= 0)
                {
                    //kapila 23/12/2016
                    if (_promoLst == null || _promoLst.Count <= 0)
                    {
                        for (int i = 0; i < chkLstPayType.CheckedItems.Count; i++)
                        {
                            object itemChecked = chkLstPayType.CheckedItems[i];
                            PaymentType txn = new PaymentType();
                            txn.Stp_act = true;
                            txn.Stp_bank = txttpt_Code.Text.Trim();
                            if (!string.IsNullOrEmpty(txttpt_ChargeRate.Text.Trim()))
                                txn.Stp_bank_chg_rt = Convert.ToDecimal(txttpt_ChargeRate.Text.Trim());
                            if (!string.IsNullOrEmpty(txttpt_ChargeValue.Text.Trim()))
                                txn.Stp_bank_chg_val = Convert.ToDecimal(txttpt_ChargeValue.Text.Trim());
                            txn.Stp_brd = txttpt_brand.Text.Trim().ToUpper();
                            txn.Stp_cat = txttpt_subcat.Text.Trim().ToUpper();
                            txn.Stp_cre_by = BaseCls.GlbUserID;
                            txn.Stp_cre_dt = DateTime.Now;
                            txn.Stp_def = chktpt_SetAsDefault.Checked;
                            txn.Stp_from_dt = Convert.ToDateTime(dtptpt_fromdate.Value.Date);
                            txn.Stp_itm = txttpt_item.Text.Trim().ToUpper();
                            txn.Stp_main_cat = txttpt_maincat.Text.Trim().ToUpper();
                            txn.Stp_pay_tp = ((PaymentTypeRef)itemChecked).Sapt_cd;
                            txn.Stp_pb = cmbtpt_Book.Text.Trim();
                            txn.Stp_pb_lvl = cmbtpt_Level.Text.Trim();
                            if (!string.IsNullOrEmpty(txttpt_Installment.Text.Trim()))
                                txn.Stp_pd = Convert.ToInt32(txttpt_Installment.Text.Trim());
                            txn.Stp_pro = txttpt_Promotion.Text.Trim();
                            txn.Stp_ser = txttpt_serial.Text.Trim();
                            txn.Stp_to_dt = Convert.ToDateTime(dtptpt_todate.Value.Date);
                            txn.Stp_txn_tp = txttpt_TxnType.Text;
                            txn.Stp_seq = counter++;
                            txn.Stp_pty_tp = cmbCommDef.SelectedIndex == 0 ? "SCHNL" : "PC";        //kapila 5/8/2014
                            txn.Stp_circ = txtCirc.Text;
                            txn.Stp_com = BaseCls.GlbUserComCode;
                            if (PaymentTxnList == null) PaymentTxnList = new List<PaymentType>();


                            PaymentTxnList.Add(txn);

                        }
                    }
                    else
                    {
                        foreach (PromoList _itm in _promoLst)
                        {
                            txttpt_Promotion.Text = _itm.PromoCode;

                            for (int i = 0; i < chkLstPayType.CheckedItems.Count; i++)
                            {
                                object itemChecked = chkLstPayType.CheckedItems[i];
                                PaymentType txn = new PaymentType();
                                txn.Stp_act = true;
                                txn.Stp_bank = txttpt_Code.Text.Trim();
                                if (!string.IsNullOrEmpty(txttpt_ChargeRate.Text.Trim()))
                                    txn.Stp_bank_chg_rt = Convert.ToDecimal(txttpt_ChargeRate.Text.Trim());
                                if (!string.IsNullOrEmpty(txttpt_ChargeValue.Text.Trim()))
                                    txn.Stp_bank_chg_val = Convert.ToDecimal(txttpt_ChargeValue.Text.Trim());
                                txn.Stp_brd = txttpt_brand.Text.Trim().ToUpper();
                                txn.Stp_cat = txttpt_subcat.Text.Trim().ToUpper();
                                txn.Stp_cre_by = BaseCls.GlbUserID;
                                txn.Stp_cre_dt = DateTime.Now;
                                txn.Stp_def = chktpt_SetAsDefault.Checked;
                                txn.Stp_from_dt = Convert.ToDateTime(dtptpt_fromdate.Value.Date);
                                txn.Stp_itm = txttpt_item.Text.Trim().ToUpper();
                                txn.Stp_main_cat = txttpt_maincat.Text.Trim().ToUpper();
                                txn.Stp_pay_tp = ((PaymentTypeRef)itemChecked).Sapt_cd;
                                txn.Stp_pb = cmbtpt_Book.Text.Trim();
                                txn.Stp_pb_lvl = cmbtpt_Level.Text.Trim();
                                if (!string.IsNullOrEmpty(txttpt_Installment.Text.Trim()))
                                    txn.Stp_pd = Convert.ToInt32(txttpt_Installment.Text.Trim());
                                txn.Stp_pro = txttpt_Promotion.Text.Trim();
                                txn.Stp_ser = txttpt_serial.Text.Trim();
                                txn.Stp_to_dt = Convert.ToDateTime(dtptpt_todate.Value.Date);
                                txn.Stp_txn_tp = txttpt_TxnType.Text;
                                txn.Stp_seq = counter++;
                                txn.Stp_pty_tp = cmbCommDef.SelectedIndex == 0 ? "SCHNL" : "PC";        //kapila 5/8/2014
                                txn.Stp_circ = txtCirc.Text;
                                txn.Stp_com = BaseCls.GlbUserComCode;
                                if (PaymentTxnList == null) PaymentTxnList = new List<PaymentType>();


                                PaymentTxnList.Add(txn);

                            }
                        }
                    }
                }
                else
                {
                    foreach (ItemList _itm in _itemLst)
                    {
                        txttpt_item.Text = _itm.Item;
                        txttpt_serial.Text = _itm.Serial;
                        for (int i = 0; i < chkLstPayType.CheckedItems.Count; i++)
                        {
                            object itemChecked = chkLstPayType.CheckedItems[i];
                            PaymentType txn = new PaymentType();
                            txn.Stp_act = true;
                            txn.Stp_bank = txttpt_Code.Text.Trim();
                            if (!string.IsNullOrEmpty(txttpt_ChargeRate.Text.Trim()))
                                txn.Stp_bank_chg_rt = Convert.ToDecimal(txttpt_ChargeRate.Text.Trim());
                            if (!string.IsNullOrEmpty(txttpt_ChargeValue.Text.Trim()))
                                txn.Stp_bank_chg_val = Convert.ToDecimal(txttpt_ChargeValue.Text.Trim());
                            txn.Stp_brd = txttpt_brand.Text.Trim().ToUpper();
                            txn.Stp_cat = txttpt_subcat.Text.Trim().ToUpper();
                            txn.Stp_cre_by = BaseCls.GlbUserID;
                            txn.Stp_cre_dt = DateTime.Now;
                            txn.Stp_def = chktpt_SetAsDefault.Checked;
                            txn.Stp_from_dt = Convert.ToDateTime(dtptpt_fromdate.Value.Date);
                            txn.Stp_itm = txttpt_item.Text.Trim().ToUpper();
                            txn.Stp_main_cat = txttpt_maincat.Text.Trim().ToUpper();
                            txn.Stp_pay_tp = ((PaymentTypeRef)itemChecked).Sapt_cd;
                            txn.Stp_pb = cmbtpt_Book.Text.Trim();
                            txn.Stp_pb_lvl = cmbtpt_Level.Text.Trim();
                            if (!string.IsNullOrEmpty(txttpt_Installment.Text.Trim()))
                                txn.Stp_pd = Convert.ToInt32(txttpt_Installment.Text.Trim());
                            txn.Stp_pro = txttpt_Promotion.Text.Trim();
                            txn.Stp_ser = txttpt_serial.Text.Trim();
                            txn.Stp_to_dt = Convert.ToDateTime(dtptpt_todate.Value.Date);
                            txn.Stp_txn_tp = txttpt_TxnType.Text;
                            txn.Stp_seq = counter++;
                            txn.Stp_pty_tp = cmbCommDef.SelectedIndex == 0 ? "SCHNL" : "PC";    //kapila 5/8/2014
                            txn.Stp_circ = txtCirc.Text;
                            txn.Stp_com = BaseCls.GlbUserComCode;
                            if (PaymentTxnList == null) PaymentTxnList = new List<PaymentType>();

                            PaymentTxnList.Add(txn);

                        }
                    }

                }
            }

            BindingSource _source = new BindingSource();
            _source.DataSource = PaymentTxnList;
            gvTransactionPayType.DataSource = _source;
            chktpt_SetAsDefault.Checked = false;
            ClearTransactionPayType();

            txtCirc.Enabled = false;

            var _r = from DataGridViewRow _rs in gvSearchedPc.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true select _rs;
            if (_r == null || _r.Count() <= 0)
                lblNoPc.Text = "0";
            else lblNoPc.Text = _r.Count().ToString();
            if (PaymentTxnList == null || PaymentTxnList.Count <= 0) lblNoPaymode.Text = "0"; else lblNoPaymode.Text = PaymentTxnList.Count.ToString();

        }

        private void addPaymentList(Int32 _payLine, string _item, string _serial, string _gvcode, string _sch)
        {
            object itemChecked = chkLstPayType.CheckedItems[_payLine];
            PaymentType txn = new PaymentType();
            txn.Stp_act = true;
            txn.Stp_bank = txttpt_Code.Text.Trim();
            if (!string.IsNullOrEmpty(txttpt_ChargeRate.Text.Trim()))
                txn.Stp_bank_chg_rt = Convert.ToDecimal(txttpt_ChargeRate.Text.Trim());
            if (!string.IsNullOrEmpty(txttpt_ChargeValue.Text.Trim()))
                txn.Stp_bank_chg_val = Convert.ToDecimal(txttpt_ChargeValue.Text.Trim());
            txn.Stp_brd = txttpt_brand.Text.Trim().ToUpper();
            txn.Stp_cat = txttpt_subcat.Text.Trim().ToUpper();
            txn.Stp_cre_by = BaseCls.GlbUserID;
            txn.Stp_cre_dt = DateTime.Now;
            txn.Stp_def = chktpt_SetAsDefault.Checked;
            txn.Stp_from_dt = Convert.ToDateTime(dtptpt_fromdate.Value.Date);
            txn.Stp_itm = txttpt_item.Text.Trim().ToUpper();
            txn.Stp_main_cat = txttpt_maincat.Text.Trim().ToUpper();
            txn.Stp_pay_tp = ((PaymentTypeRef)itemChecked).Sapt_cd;
            txn.Stp_pb = cmbtpt_Book.Text.Trim();
            txn.Stp_pb_lvl = cmbtpt_Level.Text.Trim();
            if (!string.IsNullOrEmpty(txttpt_Installment.Text.Trim()))
                txn.Stp_pd = Convert.ToInt32(txttpt_Installment.Text.Trim());
            txn.Stp_pro = txttpt_Promotion.Text.Trim();
            txn.Stp_ser = txttpt_serial.Text.Trim();
            txn.Stp_to_dt = Convert.ToDateTime(dtptpt_todate.Value.Date);
            txn.Stp_txn_tp = txttpt_TxnType.Text;
            txn.Stp_seq = counter++;
            txn.Stp_pty_tp = cmbCommDef.SelectedIndex == 0 ? "SCHNL" : "PC";    //kapila 5/8/2014
            txn.Stp_circ = txtCirc.Text;
            txn.Stp_com = BaseCls.GlbUserComCode;
            if (PaymentTxnList == null) PaymentTxnList = new List<PaymentType>();
            txn.Stp_vou_cd = _gvcode;
            txn.Stp_sch_cd = _sch;

            PaymentTxnList.Add(txn);
        }

        private void gvTransactionPayType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvTransactionPayType.RowCount > 0)
                if (e.RowIndex != -1)
                {
                    if (gvTransactionPayType.Columns[e.ColumnIndex].Name == "tpt_delete")
                    {
                        if (MessageBox.Show("Do you need to remove this record?.", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;

                        int _seq = Convert.ToInt32(gvTransactionPayType.Rows[e.RowIndex].Cells["tpt_seqno"].Value);
                        PaymentTxnList.RemoveAll(x => x.Stp_seq == _seq);
                        BindingSource _source = new BindingSource();
                        _source.DataSource = PaymentTxnList;
                        gvTransactionPayType.DataSource = _source;

                    }
                }
        }
        private void dtpab_fromdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpab_todate.Focus();
        }
        private void dtpab_todate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtpab_brand.Focus();
        }
        private void txtpab_brand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtpab_maincat.Focus();
            if (e.KeyCode == Keys.F2)
                btSearch_abBrand_Click(null, null);
        }
        private void txtpab_brand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btSearch_abBrand_Click(null, null);
        }
        private void txtpab_brand_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpab_brand.Text)) return;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
            if (_result.Rows.Count > 0)
            {
                var available = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtpab_brand.Text.Trim()).ToList();
                if (available == null || available.Count <= 0)
                {
                    MessageBox.Show("Please select valid brand", "Brand", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtpab_brand.Clear();
                    return;
                }

            }

        }
        private void btSearch_abBrand_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtpab_brand;
            _CommonSearch.ShowDialog();
            txtpab_brand.Select();
        }
        private void txtpab_maincat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtpab_subcat.Focus();
            if (e.KeyCode == Keys.F2)
                btSearch_abMainCat_Click(null, null);
        }
        private void txtpab_maincat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btSearch_abMainCat_Click(null, null);
        }
        private void txtpab_maincat_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpab_maincat.Text)) return;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            if (_result.Rows.Count > 0)
            {
                var available = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtpab_maincat.Text.Trim()).ToList();
                if (available == null || available.Count <= 0)
                {
                    MessageBox.Show("Please select valid main category", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtpab_maincat.Clear();
                    return;
                }

            }

        }
        private void btSearch_abMainCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtpab_maincat;
            _CommonSearch.ShowDialog();
            txtpab_maincat.Select();
        }
        private void txtpab_subcat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtpab_appitem.Focus();
            if (e.KeyCode == Keys.F2)
                btSearch_abSubCat_Click(null, null);
        }
        private void txtpab_subcat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btSearch_abSubCat_Click(null, null);
        }
        private void txtpab_subcat_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpab_subcat.Text)) return;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            if (_result.Rows.Count > 0)
            {
                var available = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtpab_subcat.Text.Trim()).ToList();
                if (available == null || available.Count <= 0)
                {
                    MessageBox.Show("Please select valid sub category", "Sub Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtpab_subcat.Clear();
                    return;
                }

            }

        }
        private void btSearch_abSubCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtpab_subcat;
            _CommonSearch.ShowDialog();
            txtpab_subcat.Select();
        }
        private void txtpab_appitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtpab_appvalue.Focus();
        }
        private void txtpab_appitem_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(false, sender, e);
        }
        private void txtpab_appvalue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnab_Add.Focus();
        }
        private void txtpab_appvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(true, sender, e);
        }
        private void btnab_Add_Click(object sender, EventArgs e)
        {
            DateTime fromDt = Convert.ToDateTime(dtpab_fromdate.Value.Date);
            DateTime toDt = Convert.ToDateTime(dtpab_todate.Value.Date);
            if (toDt < fromDt)
            {
                MessageBox.Show("'To Date' should be greater than 'From Date'!", "Date Range", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtpab_appitem.Text))
            {
                MessageBox.Show("Please select the approve item qty", "Approve Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpab_appitem.Clear();
                txtpab_appitem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtpab_appvalue.Text))
            {
                MessageBox.Show("Please select the approve value", "Approve Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpab_appvalue.Clear();
                txtpab_appvalue.Focus();
                return;
            }

            MasterAdditionalProductBonus _one = new MasterAdditionalProductBonus();
            _one.Itc_act_stus = true;
            _one.Itc_app_itms = Convert.ToInt32(txtpab_appitem.Text.Trim());
            _one.Itc_app_val = Convert.ToDecimal(txtpab_appvalue.Text.Trim());
            _one.Itc_brnd = txtpab_brand.Text.Trim();
            _one.Itc_cat_1 = txtpab_maincat.Text.Trim();
            _one.Itc_cat_2 = txtpab_subcat.Text.Trim();
            _one.Itc_com = ucProfitCenterCommonSearch.Company;
            _one.Itc_cre_by = BaseCls.GlbUserID;
            _one.Itc_cre_dt = DateTime.Now;
            _one.Itc_frm_dt = Convert.ToDateTime(dtpab_fromdate.Value.Date).Date;
            _one.Itc_pc = string.Empty;
            _one.Itc_seq_no = counter++;
            _one.Itc_to_dt = Convert.ToDateTime(dtpab_todate.Value.Date);

            if (_additionalProductBonus == null)
                _additionalProductBonus = new List<MasterAdditionalProductBonus>();
            _additionalProductBonus.Add(_one);

            BindingSource _source = new BindingSource();
            _source.DataSource = _additionalProductBonus;
            gvAddBonus.DataSource = _source;
            ClearAdditionalProductBonus();
        }
        private void gvAddBonus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvAddBonus.RowCount > 0) if (e.RowIndex != -1) if (gvAddBonus.Columns[e.ColumnIndex].Name == "ab_delete")
                    {
                        if (MessageBox.Show("Do you need to remove this record?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        int _seq = Convert.ToInt32(gvAddBonus.Rows[e.RowIndex].Cells["ab_seqno"].Value);
                        _additionalProductBonus.RemoveAll(x => x.Itc_seq_no == _seq);
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _additionalProductBonus;
                        gvAddBonus.DataSource = _source;

                    }
        }
        private void txtRece_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRece_Description.Focus();
        }
        private void txtRece_Description_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnRece_Add.Focus();
        }
        private void btnReceiptDiviView_Click(object sender, EventArgs e)
        {
            LoadReceiptDivision();
        }
        private void btnPcChargeView_Click(object sender, EventArgs e)
        {
            ProfitCenterCharge = new DataTable();
            LoadProfitCenterCharge();
        }
        private void chktpt_SetAsDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLstPayType.CheckedItems.Count > 1 && chktpt_SetAsDefault.Checked)
            {
                MessageBox.Show("You can only select one default pay type.", "Many Pay Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chktpt_SetAsDefault.Checked = false;
            }
        }
        private void LoadPermission()
        {
            //ODPC    - ORGANIZATION DEFINITION - PROFIT CENTER
            //ODLOC   - ORGANIZATION DEFINITION - LOCATION
            //ODRD    - ORGANIZATION DEFINITION - RECEIPT DIVISION
            //ODPCC   - ORGANIZATION DEFINITION - PROFIT CENTER CHARGERS
            //ODTPT   - ORGANIZATION DEFINITION - PROFIT CENTER TRANSACTION PAY TYPE
            //ODAB    - ORGANIZATION DEFINITION - PROFIT CENTER ADDITIONAL BONUS

            //10049	ORGANIZATION DEFINITION - PROFIT CENTER          
            //10050	ORGANIZATION DEFINITION - LOCATION               
            //10051	ORGANIZATION DEFINITION - RECEIPT DIVISION       
            //10052	ORGANIZATION DEFINITION - PROFIT CENTER CHARGERS 
            //10053	ORGANIZATION DEFINITION - PC/TRANSACTION PAY TYPE
            //10054	ORGANIZATION DEFINITION - PC/ADDITIONAL BONUS

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10049))
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ODPC"))
                ((Control)this.tpPcHierarchy).Enabled = true;
            else ((Control)this.tpPcHierarchy).Enabled = false;

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10050))
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ODLOC"))
                ((Control)this.tpLocHierarchy).Enabled = true;
            else ((Control)this.tpLocHierarchy).Enabled = false;

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10051))
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ODRD"))
                ((Control)this.tpPcReceiptCategory).Enabled = true;
            else ((Control)this.tpPcReceiptCategory).Enabled = false;

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10052))
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ODPCC"))
                ((Control)this.tpPcCharges).Enabled = true;
            else ((Control)this.tpPcCharges).Enabled = false;

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10053))
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ODTPT"))
                ((Control)this.tpPcTransactionPayTypes).Enabled = true;
            else ((Control)this.tpPcTransactionPayTypes).Enabled = false;

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10054))
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ODAB"))
                ((Control)this.tpPcAdditionalBonus).Enabled = true;
            else ((Control)this.tpPcAdditionalBonus).Enabled = false;

        }
        private void BackUpCode()
        {
            #region Profit Center Hierarchy
            if (tabControl1.SelectedTab.Name == tpPcHierarchy.Name)
            {
                if (MessageBox.Show("Do you need to save this profit center hierarchy?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Zone))
                {
                    MessageBox.Show("Please select the zone.", "Zone", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Regien))
                {
                    MessageBox.Show("Please select the region.", "Region", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Area))
                {
                    MessageBox.Show("Please select the area.", "Area", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(ucProfitCenterNewAssign.SubChannel))
                {
                    MessageBox.Show("Please select the sub channel.", "Sub Channel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Channel))
                {
                    MessageBox.Show("Please select the channel.", "Channel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(ucProfitCenterNewAssign.Company))
                {
                    MessageBox.Show("Please select the company.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<MasterSalesPriorityHierarchy> _pcInfoHeaders = new List<MasterSalesPriorityHierarchy>();
                var _r = from DataGridViewRow _rs in gvSearchedPc.Rows
                         where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true
                         select _rs;

                if (_r == null || _r.Count() <= 0)
                {
                    MessageBox.Show("Please select the profit center which you have to assign hierarchy.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var _row in _r)
                {
                    MasterSalesPriorityHierarchy info = new MasterSalesPriorityHierarchy();
                    info.Mpi_act = true;
                    info.Mpi_pc_cd = Convert.ToString(_row.Cells["p_pccode"].Value);
                    info.Mpi_com_cd = ucProfitCenterCommonSearch.Company;
                    info.Mpi_tp = "PC_PRIT_HIERARCHY";
                    _pcInfoHeaders.Add(info);
                }

                Dictionary<string, string> code_and_value = new Dictionary<string, string>();
                code_and_value.Add("ZONE", ucProfitCenterNewAssign.Zone.Trim().ToUpper());
                code_and_value.Add("REGION", ucProfitCenterNewAssign.Regien.Trim().ToUpper());
                code_and_value.Add("AREA", ucProfitCenterNewAssign.Area.Trim().ToUpper());
                code_and_value.Add("SCHNL", ucProfitCenterNewAssign.SubChannel.Trim().ToUpper());
                code_and_value.Add("CHNL", ucProfitCenterNewAssign.Channel.Trim().ToUpper());
                code_and_value.Add("COM", ucProfitCenterCommonSearch.Company.Trim().ToUpper());
                code_and_value.Add("PC", string.Empty);
                code_and_value.Add("GPC", "GRUP01");

                Int32 eff = CHNLSVC.General.Save_MST_PC_INFO(_pcInfoHeaders, code_and_value);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearPcHierarchyDefinitionScreen();
                    return;
                }
                else
                {
                    MessageBox.Show("Please try again.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            #endregion

            #region Location Hierarchy
            if (tabControl1.SelectedTab.Name == tpLocHierarchy.Name)
            {
                if (MessageBox.Show("Do you need to save this location hierarchy?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                List<MasterLocationPriorityHierarchy> _locInfoHeaders = new List<MasterLocationPriorityHierarchy>();
                var _r = from DataGridViewRow _rs in gvSearchedPc.Rows
                         where Convert.ToBoolean(((DataGridViewCheckBoxCell)_rs.Cells["p_select"]).Value) == true
                         select _rs;

                if (_r == null || _r.Count() <= 0)
                {
                    MessageBox.Show("Please select the profit center which you have to assign hierarchy.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var _row in _r)
                {
                    MasterLocationPriorityHierarchy info = new MasterLocationPriorityHierarchy();
                    info.Mli_act = true;
                    info.Mli_loc_cd = Convert.ToString(_row.Cells["p_pccode"].Value);
                    info.Mli_com_cd = ucProfitCenterCommonSearch.Company;
                    info.Mli_tp = "LOC_PRIT_HIERARCHY";
                    _locInfoHeaders.Add(info);
                }

                Dictionary<string, string> code_and_value = new Dictionary<string, string>();
                code_and_value.Add("ZONE", ucLocationNewAssign.Zone.Trim().ToUpper());
                code_and_value.Add("REGION", ucLocationNewAssign.Regien.Trim().ToUpper());
                code_and_value.Add("AREA", ucLocationNewAssign.Area.Trim().ToUpper());
                code_and_value.Add("SCHNL", ucLocationNewAssign.SubChannel.Trim().ToUpper());
                code_and_value.Add("CHNL", ucLocationNewAssign.Channel.Trim().ToUpper());
                code_and_value.Add("COM", ucLocationNewAssign.Company.Trim().ToUpper());
                code_and_value.Add("LOC", string.Empty);
                code_and_value.Add("GPC", "GRUP01");

                Int32 eff = CHNLSVC.General.save_mst_loc_info(_locInfoHeaders, code_and_value);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearPcHierarchyDefinitionScreen();
                    return;
                }
                else
                {
                    MessageBox.Show("Please try again.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            #endregion
        }
        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            lblFilePath.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName.Text = _obj[_obj.Length - 1].ToString();
            lblFilePath.Text = openFileDialog1.FileName;
        }
        private List<ItemList> _itemLst = null;
        private class ItemList
        {
            string _item;
            string _serial;

            public string Item
            {
                get { return _item; }
                set { _item = value; }
            }
            public string Serial
            {
                get { return _serial; }
                set { _serial = value; }
            }

        }
        private List<PromoList> _promoLst = null;
        private class PromoList
        {
            string _promoCode;

            public string PromoCode
            {
                get { return _promoCode; }
                set { _promoCode = value; }
            }


        }
        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Clear();
                lblFilePath.Text = string.Empty;
                txtFileName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(lblFilePath.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Clear();
                lblFilePath.Text = string.Empty;
                txtFileName.Focus();
                return;
            }

            System.IO.FileInfo _fileObj = new System.IO.FileInfo(lblFilePath.Text);

            if (_fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Focus();
            }

            string _extension = _fileObj.Extension;
            string _conStr = string.Empty;

            if (_extension.ToUpper() == ".XLS") _conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"].ConnectionString;
            else if (_extension.ToUpper() == ".XLSX") _conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"].ConnectionString;

            _conStr = String.Format(_conStr, lblFilePath.Text, "NO");
            OleDbConnection _connExcel = new OleDbConnection(_conStr);
            OleDbCommand _cmdExcel = new OleDbCommand();
            OleDbDataAdapter _oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            _cmdExcel.Connection = _connExcel;

            //Get the name of First Sheet
            _connExcel.Open();
            DataTable _dtExcelSchema;
            _dtExcelSchema = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string _sheetName = _dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            _connExcel.Close();

            _connExcel.Open();
            _cmdExcel.CommandText = "SELECT * From [" + _sheetName + "]";
            _oda.SelectCommand = _cmdExcel;
            _oda.Fill(_dt);
            _connExcel.Close();
            _itemLst = new List<ItemList>();
            _promoLst = new List<PromoList>();
            StringBuilder _errorLst = new StringBuilder();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (_dt.Rows.Count > 0)
            {
                if (_excel_up_type == 0)    //item excel
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        if (!IsItemExcel)
                        {
                            try
                            {
                                string _serial = Convert.ToString(_dr[1]);
                            }
                            catch
                            {
                                MessageBox.Show("There is no serial available in the excel file. Please check.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(_dr[0].ToString())) continue;
                        if (!IsItemExcel) if (!string.IsNullOrEmpty(_dr[0].ToString()) && string.IsNullOrEmpty(_dr[1].ToString())) if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("serial not fount in item - " + _dr[0].ToString()); else _errorLst.Append(" and serial not fount in item - " + _dr[0].ToString());

                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString().Trim());
                        if (_item == null)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid item - " + _dr[0].ToString());
                            else _errorLst.Append(" and invalid item - " + _dr[0].ToString());
                            continue;
                        }

                        if (IsItemExcel)
                        {
                            var _dup = _itemLst.Where(x => x.Item == _dr[0].ToString()).ToList();
                            if (_dup != null && _dup.Count > 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("item " + _dr[0].ToString() + " duplicate");
                                else _errorLst.Append(" and item " + _dr[0].ToString() + " duplicate");
                                continue;
                            }
                        }
                        else
                        {
                            var _dup = _itemLst.Where(x => x.Item == _dr[0].ToString() && x.Serial == _dr[1].ToString()).ToList();
                            if (_dup != null && _dup.Count > 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("item and serial are " + _dr[0].ToString() + " duplicate");
                                else _errorLst.Append(" and item and serial are " + _dr[0].ToString() + " duplicate");
                                continue;
                            }
                        }

                        ItemList _lst = new ItemList();
                        if (!IsItemExcel)
                        {
                            _lst.Item = _dr[0].ToString().Trim();
                            _lst.Serial = _dr[1].ToString().Trim();
                        }
                        else
                        {
                            _lst.Item = _dr[0].ToString().Trim();
                            _lst.Serial = string.Empty;
                        }
                        _itemLst.Add(_lst);

                    }

                    if (!string.IsNullOrEmpty(_errorLst.ToString()))
                    {
                        if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            _itemLst = new List<ItemList>();
                            pnlExcel.Visible = true;
                        }
                    }
                }
                else //promotion excel
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {

                        if (string.IsNullOrEmpty(_dr[0].ToString())) continue;

                        List<PriceDetailRef> _pdet = CHNLSVC.Sales.GetPriceByPromoCD(_dr[0].ToString().Trim());
                        if (_pdet == null)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid promotion - " + _dr[0].ToString());
                            else _errorLst.Append(" and invalid promotion - " + _dr[0].ToString());
                            continue;
                        }


                        var _dup = _promoLst.Where(x => x.PromoCode == _dr[0].ToString()).ToList();
                        if (_dup != null && _dup.Count > 0)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("promotion " + _dr[0].ToString() + " duplicate");
                            else _errorLst.Append(" and promotion " + _dr[0].ToString() + " duplicate");
                            continue;
                        }


                        PromoList _lst = new PromoList();

                        _lst.PromoCode = _dr[0].ToString().Trim();

                        _promoLst.Add(_lst);

                    }

                    if (!string.IsNullOrEmpty(_errorLst.ToString()))
                    {
                        if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            _promoLst = new List<PromoList>();
                            pnlExcel.Visible = true;
                        }
                    }
                }
            }

            pnlExcel.Visible = false;

        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (_itemLst != null && _itemLst.Count > 0)
            {
                string _noun = string.Empty;
                if (IsItemExcel) _noun = "item";
                else _noun = "item/serial";
                if (MessageBox.Show("Currently you have select a list of " + _noun + ".If you continue, the present excel data will be lost. Do you need to continue anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    _itemLst = new List<ItemList>();
                else
                    return;
            }

            if (pnlExcel.Visible)
                pnlExcel.Visible = false;
            else
            {
                pnlExcel.Location = new Point(293, 303);
                pnlExcel.Visible = true;
            }

            IsItemExcel = true;
            _excel_up_type = 0;
        }
        private void btnSerialExcel_Click(object sender, EventArgs e)
        {
            if (_itemLst != null && _itemLst.Count > 0)
            {
                string _noun = string.Empty;
                if (IsItemExcel) _noun = "item";
                else _noun = "item/serial";
                if (MessageBox.Show("Currently you have select a list of " + _noun + ".If you continue, the present excel data will be lost. Do you need to continue anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    _itemLst = new List<ItemList>();
                else
                    return;
            }

            if (pnlExcel.Visible) pnlExcel.Visible = false;
            else pnlExcel.Visible = true;
            IsItemExcel = false;
        }

        private void btnClose_new_Click(object sender, EventArgs e)
        {
            txtCodeNew.Text = "";
            txtDesnNew.Text = "";
            pnlCreateNew.Visible = false;
        }



        private void btn_srch_new_Click(object sender, EventArgs e)
        {
            DataTable _result = null;
            DataTable LocDes = null;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            if (lblDesnNew.Text == "Area")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Area);
                _result = CHNLSVC.CommonSearch.Getareasearchdata(_CommonSearch.SearchParams, null, null);
            }
            if (lblDesnNew.Text == "Region")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Region);
                _result = CHNLSVC.CommonSearch.Getregionsearchdata(_CommonSearch.SearchParams, null, null);
            }
            if (lblDesnNew.Text == "Zone")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Zone);
                _result = CHNLSVC.CommonSearch.Getzonesearchdata(_CommonSearch.SearchParams, null, null);
            }
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCodeNew;
            _CommonSearch.ShowDialog();
            txtCodeNew.Select();

            if (lblDesnNew.Text == "Area")
            {
                LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", txtCodeNew.Text);
            }
            if (lblDesnNew.Text == "Region")
            {
                LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", txtCodeNew.Text);
            }
            if (lblDesnNew.Text == "Zone")
            {
                LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", txtCodeNew.Text);
            }
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtDesnNew.Text = row2["descp"].ToString();
                txtEmpNew.Text = row2["mgr_cd"].ToString();
            }
        }

        private void btnRegion_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Top = 360;
            pnlCreateNew.Left = 507;
            pnlCreateNew.Visible = true;
            label71.Text = "New Region Creation";
            lblDesnNew.Text = "Region";
        }

        private void btnZone_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Top = 360;
            pnlCreateNew.Left = 507;
            pnlCreateNew.Visible = true;
            label71.Text = "New Zone Creation";
            lblDesnNew.Text = "Zone";
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Top = 360;
            pnlCreateNew.Left = 507;
            pnlCreateNew.Visible = true;
            label71.Text = "New Area Creation";
            lblDesnNew.Text = "Area";
        }

        private void btnClearNew_Click(object sender, EventArgs e)
        {
            txtCodeNew.Text = "";
            txtDesnNew.Text = "";
            txtEmpNew.Text = "";
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            string _newCode = "";
            if (string.IsNullOrEmpty(txtEmpNew.Text))
            {
                MessageBox.Show("Please enter manager code", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Definition", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (lblDesnNew.Text == "Area")
            {
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_moduleid = "AREA";
                masterAuto.Aut_start_char = "A";

                MasterArea _mstArea = new MasterArea();
                _mstArea.Msar_act = chkAct.Checked;
                _mstArea.Msar_cd = txtCodeNew.Text;
                _mstArea.Msar_com = BaseCls.GlbUserComCode;
                _mstArea.Msar_cre_by = BaseCls.GlbUserID;
                _mstArea.Msar_desc = txtDesnNew.Text;
                _mstArea.Msar_mod_by = BaseCls.GlbUserID;
                _mstArea.Msar_session_id = BaseCls.GlbUserSessionID;
                _mstArea.Msar_mgr_cd = txtEmpNew.Text;

                int row_aff = CHNLSVC.General.update_area(_mstArea, masterAuto, out _newCode);
                if (row_aff != -99 && row_aff >= 0)
                {
                    if (string.IsNullOrEmpty(txtCodeNew.Text))
                    {
                        MessageBox.Show("Successfully Saved. Code number - " + _newCode, "Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Successfully Updated", "Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(_newCode, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (lblDesnNew.Text == "Region")
            {
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_moduleid = "REGION";
                masterAuto.Aut_start_char = "R";

                MasterRegion _mstReg = new MasterRegion();
                _mstReg.Msrg_act = chkAct.Checked;
                _mstReg.Msrg_cd = txtCodeNew.Text;
                _mstReg.Msrg_com = BaseCls.GlbUserComCode;
                _mstReg.Msrg_cre_by = BaseCls.GlbUserID;
                _mstReg.Msrg_desc = txtDesnNew.Text;
                _mstReg.Msrg_mod_by = BaseCls.GlbUserID;
                _mstReg.Msrg_session_id = BaseCls.GlbUserSessionID;
                _mstReg.Msrg_mgr_cd = txtEmpNew.Text;

                int row_aff = CHNLSVC.General.update_region(_mstReg, masterAuto, out _newCode);
                if (row_aff != -99 && row_aff >= 0)
                {
                    if (string.IsNullOrEmpty(txtCodeNew.Text))
                    {
                        MessageBox.Show("Successfully Saved. Code number - " + _newCode, "Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Successfully Updated", "Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(_newCode, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (lblDesnNew.Text == "Zone")
            {
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_moduleid = "ZONE";
                masterAuto.Aut_start_char = "Z";

                MasterZone _mstZone = new MasterZone();
                _mstZone.Mszn_act = chkAct.Checked;
                _mstZone.Mszn_cd = txtCodeNew.Text;
                _mstZone.Mszn_com = BaseCls.GlbUserComCode;
                _mstZone.Mszn_cre_by = BaseCls.GlbUserID;
                _mstZone.Mszn_desc = txtDesnNew.Text;
                _mstZone.Mszn_mod_by = BaseCls.GlbUserID;
                _mstZone.Mszn_session_id = BaseCls.GlbUserSessionID;
                _mstZone.Mszn_mgr_cd = txtEmpNew.Text;

                int row_aff = CHNLSVC.General.update_zone(_mstZone, masterAuto, out _newCode);
                if (row_aff != -99 && row_aff >= 0)
                {
                    if (string.IsNullOrEmpty(txtCodeNew.Text))
                    {
                        MessageBox.Show("Successfully Saved. Code number - " + _newCode, "Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Successfully Updated", "Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(_newCode, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnArea1_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Top = 360;
            pnlCreateNew.Left = 507;
            pnlCreateNew.Visible = true;
            label71.Text = "New Area Creation";
            lblDesnNew.Text = "Area";
        }

        private void btnZone1_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Top = 360;
            pnlCreateNew.Left = 507;
            pnlCreateNew.Visible = true;
            label71.Text = "New Zone Creation";
            lblDesnNew.Text = "Zone";
        }

        private void btnRegion1_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Top = 360;
            pnlCreateNew.Left = 507;
            pnlCreateNew.Visible = true;
            label71.Text = "New Region Creation";
            lblDesnNew.Text = "Region";
        }

        private void btnClearBank_Click(object sender, EventArgs e)
        {
            txtBankCode.Text = "";
            txtBank.Text = "";
            txtSUNCode.Text = "";
            txtBankID.Text = "";
        }

        private void btnClearBranch_Click(object sender, EventArgs e)
        {
            txtBranchCode.Text = "";
            txtBranch.Text = "";
        }

        private void btnSaveBank_Click(object sender, EventArgs e)
        {
            string _CompCode = "";
            if (txtBankCode.Text == "")
            {
                MessageBox.Show("Please enter bank code", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtBank.Text == "")
            {
                MessageBox.Show("Please enter bank name", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtSUNCode.Text == "")
            {
                MessageBox.Show("Please enter SUN code", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtBankID.Text == "")
            {
                MessageBox.Show("Please enter bank ID", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Bank", MessageBoxButtons.YesNo) == DialogResult.No) return;

            MasterOutsideParty _outsideParty = new MasterOutsideParty();

            _outsideParty.Mbi_cd = txtBankCode.Text;
            _outsideParty.Mbi_country_cd = "LK"; //Corrected LKR to LK Chamal 07-02-2016
            _outsideParty.Mbi_desc = txtBank.Text;
            _outsideParty.Mbi_tp = "BANK";
            _outsideParty.Mbi_id = txtBankID.Text;
            _outsideParty.Mbi_act = chkBank.Checked;
            _outsideParty.Mbi_sun_bank = txtSUNCode.Text;
            _outsideParty.Mbi_add1 = "";
            _outsideParty.Mbi_add2 = "";
            _outsideParty.Mbi_tel = "";
            _outsideParty.Mbi_fax = "";
            _outsideParty.Mbi_email = "";
            _outsideParty.Mbi_web = "";
            _outsideParty.Mbi_town_cd = "";
            _outsideParty.Mbi_tax1 = "";
            _outsideParty.Mbi_tax2 = "";
            _outsideParty.Mbi_tax3 = "";
            _outsideParty.Mbi_cre_by = BaseCls.GlbUserID;
            _outsideParty.Mbi_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
            _outsideParty.Mbi_mod_by = BaseCls.GlbUserID;
            _outsideParty.Mbi_mod_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _outsideParty.Mbi_session_id = BaseCls.GlbUserSessionID;

            int row_aff = CHNLSVC.General.SaveOutsideParty(_outsideParty, null, out _CompCode);
            if (row_aff != -99 && row_aff >= 0)
            {
                MessageBox.Show("Successfully Updated", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(_CompCode, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_srch_bank_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankALL);
            DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompanyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBankID;
            _CommonSearch.ShowDialog();
            txtBankID.Select();

            load_bank_det();
        }

        private void load_bank_det()
        {
            txtBank.Text = "";
            txtSUNCode.Text = "";
            txtBankCode.Text = "";
            chkBank.Checked = false;

            DataTable BankName = CHNLSVC.Sales.get_Bank_Name(txtBankID.Text);
            foreach (DataRow row2 in BankName.Rows)
            {
                txtBank.Text = row2["mbi_desc"].ToString();
                txtSUNCode.Text = row2["mbi_sun_bank"].ToString();
                txtBankCode.Text = row2["mbi_cd"].ToString();
                chkBank.Checked = Convert.ToBoolean(row2["mbi_act"]);
            }
        }

        private void load_branch_bank_det()
        {
            lblBankName.Text = "";
            lblBankID.Text = "";
            txtBranchCode.Text = "";
            txtBranch.Text = "";

            DataTable BankName = CHNLSVC.Sales.get_Bank_Name(txtBranchBankCode.Text);
            foreach (DataRow row2 in BankName.Rows)
            {
                lblBankName.Text = row2["mbi_desc"].ToString();
                lblBankID.Text = row2["mbi_cd"].ToString();
            }
        }

        //Added by Udesh 12-Nov-2018
        private void load_bin_bank_det()
        {
            lblBinBankName.ResetText();
            txtBinNumber.ResetText();

            DataTable BankName = CHNLSVC.Sales.get_Bank_Name(txtBinBankCode.Text);
            foreach (DataRow row2 in BankName.Rows)
            {
                lblBinBankName.Text = row2["mbi_desc"].ToString();
            }

            LoadCardType(null);
        }

        //Added by Udesh 12-Nov-2018
        private void LoadCardType(string _bankCode)
        {
            DataTable _dtCardType = CHNLSVC.Sales.GetBankCC(_bankCode);
            _dtCardType.Rows.InsertAt(_dtCardType.NewRow(), 0);
            if (_dtCardType.Rows.Count > 0)
            {
                cmbBinCardType.DataSource = _dtCardType;
                cmbBinCardType.DisplayMember = "mbct_cc_tp";
                cmbBinCardType.ValueMember = "mbct_cc_tp";
            }
            else
            {
                cmbBinCardType.DataSource = null;
            }
        }

        private void txtBankID_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBankID.Text))
                load_bank_det();
        }

        private void btn_srch_branch_bank_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankALL);
            DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompanyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBranchBankCode;
            _CommonSearch.ShowDialog();
            txtBranchBankCode.Select();

            load_branch_bank_det();
        }

        private void txtBankCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBranchCode.Text))
            {
                if (IsNumeric(txtBankCode.Text) == false)
                {
                    MessageBox.Show("Invalid bank code", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBankCode.Focus();
                    return;
                }
            }
        }

        private void btn_srch_branch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
            DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBranchCode;
            _CommonSearch.ShowDialog();
            txtBranchCode.Select();

            load_branch_det();
        }

        private void load_branch_det()
        {
            txtBranch.Text = "";

            DataTable BankName = CHNLSVC.Sales.get_Branch_Name(lblBankID.Text, txtBranchCode.Text);
            foreach (DataRow row2 in BankName.Rows)
            {
                txtBranch.Text = row2["mbb_desc"].ToString();
                chkBranch.Checked = Convert.ToBoolean(row2["mbb_active"]);
            }
        }

        private void txtBranchCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBranchCode.Text))
            {
                if (IsNumeric(txtBranchCode.Text) == false)
                {
                    MessageBox.Show("Invalid branch code", "Branch", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBranchCode.Focus();
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtBranchCode.Text))
                load_branch_det();
        }

        private void btnSaveBrance_Click(object sender, EventArgs e)
        {
            string _errr = "";
            if (txtBranchCode.Text == "")
            {
                MessageBox.Show("Please enter branch code", "Branch", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtBranch.Text == "")
            {
                MessageBox.Show("Please enter branch", "Branch", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Branch", MessageBoxButtons.YesNo) == DialogResult.No) return;

            MasterBankBranch _bankBranch = new MasterBankBranch();
            _bankBranch.Mbb_country_cd = "LK";
            _bankBranch.Mbb_bus_cd = lblBankID.Text;
            _bankBranch.Mbb_cd = txtBranchCode.Text;
            _bankBranch.Mbb_desc = txtBranch.Text;
            _bankBranch.Mbb_active = Convert.ToBoolean(chkBranch.Checked);
            _bankBranch.Mbb_cre_by = BaseCls.GlbUserID;
            _bankBranch.Mbb_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
            _bankBranch.Mbb_mod_by = BaseCls.GlbUserID;
            _bankBranch.Mbb_mod_when = Convert.ToDateTime(DateTime.Now.Date).Date;
            _bankBranch.Mbb_session_id = BaseCls.GlbUserSessionID;

            int row_aff = CHNLSVC.General.SaveBankBranch(_bankBranch, out _errr);
            if (row_aff != -99 && row_aff >= 0)
            {
                MessageBox.Show("Successfully Updated", "Branch", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(_errr, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Base _basePage = new Base();
            //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();


            //    DataTable dt = new DataTable();
            //    DataTable _result = new DataTable();
            //    if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
            //    {
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            //        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //        if (txtHierchCode.Text != "")
            //        {
            //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
            //        }
            //    }
            //    else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            //    {
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            //        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //        if (txtHierchCode.Text != "")
            //        {
            //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
            //        }
            //    }
            //    else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            //    {
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            //        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //        if (txtHierchCode.Text != "")
            //        {
            //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
            //        }
            //    }

            //    else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
            //    {
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            //        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //        if (txtHierchCode.Text != "")
            //        {
            //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
            //        }
            //    }
            //    else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
            //    {
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            //        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //        if (txtHierchCode.Text != "")
            //        {
            //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
            //        }
            //    }
            //    else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
            //    {
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            //        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //        if (txtHierchCode.Text != "")
            //        {
            //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
            //        }
            //    }
            //    else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
            //    {
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            //        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            //        if (txtHierchCode.Text != "")
            //        {
            //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
            //        }
            //    }
            //    else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
            //    {
            //        // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
            //        //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
            //        _result = CHNLSVC.General.Get_GET_GPC("", "");
            //        if (txtHierchCode.Text != "")
            //        {
            //            _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode.Text, "");
            //        }
            //    }
            //    if (_result == null || _result.Rows.Count <= 0)
            //    {
            //        MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    PCList.Merge(_result);
            //    grvParty.DataSource = null;
            //    grvParty.AutoGenerateColumns = false;
            //    grvParty.DataSource = PCList;
            //    // chkPCAll.Checked = true;
            //    //AllClick();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
                if (distinctList == null || distinctList.Count <= 0)
                {
                    MessageBox.Show("Invalid Price book or Level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PBList.AddRange(distinctList);

                BindingSource source = new BindingSource();
                source.DataSource = PBList;
                dataGridViewPriceBook.DataSource = source;
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvSalesType_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_save_disc_Click(object sender, EventArgs e)
        {
            //kapila 16/3/2017
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10157))
            {
                MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10157", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            List<CashGeneralDicountDef> _itemList = new List<CashGeneralDicountDef>();
            if (txtCircular.Text == "")
            {
                MessageBox.Show("Please select circular number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtDiscount.Text == "")
            {
                MessageBox.Show("Please enter discount amount/value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal val;
            if (!decimal.TryParse(txtDiscount.Text, out val))
            {
                MessageBox.Show("Please enter discount amount/value in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (val < 0)
            {
                MessageBox.Show("Please enter discount amount/value can not be minus", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (PBList.Count <= 0)
            {
                MessageBox.Show("Please select price book details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (gvSearchedPc.Rows.Count <= 0)
            {
                MessageBox.Show("Please select location details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (SalesType.Count <= 0)
            {
                MessageBox.Show("Please select sales type details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtDiscount.Text) > 100)
            {
                MessageBox.Show("Invalid discount rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiscount.Focus();
                return;
            }
            if (Convert.ToDecimal(txtNoTimes.Text) < 0)
            {
                MessageBox.Show("Invalid value for the no of times", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoTimes.Focus();
                return;
            }
            if (Convert.ToDateTime(dateTimePickerFrom.Text) > Convert.ToDateTime(dateTimePickerTo.Text))
            {
                MessageBox.Show("From date has to be smller than to date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Definition", MessageBoxButtons.YesNo) == DialogResult.No) return;


            try
            {
                for (int i = 0; i < (gvSearchedPc.Rows.Count); i++)
                {
                    if (gvSearchedPc.Rows[i].Cells["p_pccode"].Selected == true)
                    {
                        foreach (DataGridViewRow gvrS in grvSalesType.Rows)
                        {
                            foreach (DataGridViewRow gvrPB in dataGridViewPriceBook.Rows)
                            {
                                CashGeneralDicountDef _genDisc = new CashGeneralDicountDef();
                                _genDisc.Sgdd_alw_pro = chkPro.Checked;
                                _genDisc.Sgdd_alw_ser = chkSer.Checked;
                                _genDisc.Sgdd_circular = txtCircular.Text;
                                _genDisc.Sgdd_com = BaseCls.GlbUserComCode;
                                _genDisc.Sgdd_cre_by = BaseCls.GlbUserID;
                                _genDisc.Sgdd_cre_dt = DateTime.Now;
                                _genDisc.Sgdd_cust_cd = "";
                                if (rdoAmo.Checked)
                                    _genDisc.Sgdd_disc_val = Convert.ToDecimal(txtDiscount.Text);
                                else
                                    _genDisc.Sgdd_disc_rt = Convert.ToDecimal(txtDiscount.Text);

                                _genDisc.Sgdd_from_dt = dateTimePickerFrom.Value.Date;
                                _genDisc.Sgdd_itm = "";
                                _genDisc.Sgdd_mod_by = BaseCls.GlbUserID;
                                _genDisc.Sgdd_mod_dt = DateTime.Now;
                                _genDisc.Sgdd_no_of_times = Convert.ToInt32(txtNoTimes.Text);
                                _genDisc.Sgdd_no_of_used_times = 0;
                                _genDisc.Sgdd_pb = gvrPB.Cells[1].Value.ToString();
                                _genDisc.Sgdd_pb_lvl = gvrPB.Cells[2].Value.ToString();
                                _genDisc.Sgdd_pc = gvSearchedPc.Rows[i].Cells["p_pccode"].Value.ToString();  // Item.Text;
                                _genDisc.Sgdd_req_ref = "";
                                _genDisc.Sgdd_sale_tp = gvrS.Cells[1].Value.ToString();
                                _genDisc.Sgdd_to_dt = dateTimePickerTo.Value.Date;
                                _genDisc.Sgdd_stus = true;
                                _genDisc.Sgdd_cust_cd = txtCustomer.Text.ToString();

                                _itemList.Add(_genDisc);
                            }
                        }
                    }
                }
                string _error = "";
                int row_aff = CHNLSVC.Sales.SaveGeneralDiscDef(_itemList, txtCircular.Text, out _error);
                if (row_aff != -99 && row_aff >= 0)
                {
                    MessageBox.Show("Successfully Updated", "Discount Def", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_disc();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_error, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_clear_disc_Click(object sender, EventArgs e)
        {
            clear_disc();
            txtCircular.Focus();
        }

        private void clear_disc()
        {
            chkPro.Checked = false;
            chkSer.Checked = false;
            txtDiscount.Text = "0";
            txtNoTimes.Text = "9999";
            dataGridViewPriceBook.DataSource = null;
            grvSalesType.DataSource = null;
            txtCircular.Text = "";
            dateTimePickerFrom.Enabled = true;
            txtCirc.Enabled = true;
            dateTimePickerFrom.ResetText();
            dateTimePickerTo.ResetText();
            PBList = new List<PriceBookLevelRef>();
            SalesType = new List<MasterInvoiceType>();
            txtDiscount.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            BindSalesTypes();

            txtPriceBook.Text = string.Empty;
            txtLevel.Text = string.Empty;
        }

        private void btn_srch_circ_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GenDiscount);
            DataTable _result = CHNLSVC.CommonSearch.GetGenDiscSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCircular;
            _CommonSearch.ShowDialog();
            txtCircular.Select();

            load_gen_disc_det();
        }

        private void load_gen_disc_det()
        {
            DataTable _dt = CHNLSVC.Sales.GetGenDiscByCirc(txtCircular.Text);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                dateTimePickerFrom.Value = Convert.ToDateTime(_dt.Rows[0]["SGDD_FROM_DT"]);
                dateTimePickerTo.Value = Convert.ToDateTime(_dt.Rows[0]["SGDD_TO_DT"]);

                dateTimePickerFrom.Enabled = false;

            }
        }

        private void txtCircular_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCircular.Text))
                load_gen_disc_det();
        }

        private void btnSearchChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChanel;
                _CommonSearch.ShowDialog();
                txtChanel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChanel.Text))
                {
                    MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChanel.Text = "";
                    txtChanel.Focus();
                    return;
                }
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSChanel;
                _CommonSearch.ShowDialog();
                txtSChanel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchPC_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Boolean _isFound = false;
            try
            {

                Base _basePage = new Base();

                if (cmbCommDef.Text == "Profit Center")
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, null, null, null, txtPC.Text);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            if (Item.Text == drow["PROFIT_CENTER"].ToString())
                                _isFound = true;
                        }
                        if (_isFound == false)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                        _isFound = false;
                    }
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _searchType = "";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (!string.IsNullOrEmpty(txtSChanel.Text))
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtSChanel.Text);
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                }
            A:
                foreach (ListViewItem Item in lstPC.Items)
                {
                    Item.Checked = true;
                }
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtPC.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnPcClear_Click(object sender, EventArgs e)
        {
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            lstPC.Clear();
            txtChanel.Focus();
        }

        private void cmbCommDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCommDef.Text == "Profit Center")
            {
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtChanel.Enabled = true;
                txtPC.Enabled = true;
                txtSChanel.Enabled = true;
                btnSearchChannel.Enabled = true;
                btnSearchPC.Enabled = true;
                btnSearchSubChannel.Enabled = true;
                lstPC.Clear();
                txtChanel.Focus();

            }
            else
            {
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtChanel.Enabled = true;
                txtPC.Enabled = false;
                txtSChanel.Enabled = true;
                btnSearchChannel.Enabled = true;
                btnSearchPC.Enabled = false;
                btnSearchSubChannel.Enabled = true;
                lstPC.Clear();
                txtChanel.Focus();
            }
            ClearTransactionPayType();
            txtCirc.Clear();
            gvTransactionPayType.DataSource = null;
            PaymentTxnList = new List<PaymentType>();
        }

        private void btnSrchCirc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PayCircular);
            DataTable _result = CHNLSVC.CommonSearch.searchPayCircularData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCirc;
            _CommonSearch.ShowDialog();
            txtCirc.Select();
        }

        private void txtCirc_Leave(object sender, EventArgs e)
        {
            load_pay_circ_det();
        }

        private void load_pay_circ_det()
        {
            DataTable _dt = CHNLSVC.General.GetPayCircByCode(BaseCls.GlbUserComCode, txtCirc.Text);
            if (_dt.Rows.Count > 0)
            {
                dtptpt_fromdate.Enabled = false;
                panel9.Enabled = false;
                panel8.Enabled = false;
                panel10.Enabled = false;
                btntpt_Add.Enabled = false;
                btnPcHierarchySave.Text = "Update";
            }
            else
            {
                dtptpt_fromdate.Enabled = true;
                panel9.Enabled = true;
                panel8.Enabled = true;
                panel10.Enabled = true;
                btntpt_Add.Enabled = true;
                btnPcHierarchySave.Text = "Save";
            }
        }

        private void OrganizationDefinition_Load(object sender, EventArgs e)
        {

        }

        private void btnChannel_Click(object sender, EventArgs e)
        {
            pnlNewChanel.Visible = true;
            label90.Text = "Channel";
            lblCreationNew.Text = "New Channel Creation";
            btnNewSubChan.Enabled = false;

        }

        private void btnClearChannel_Click(object sender, EventArgs e)
        {
            chkActiveNew.Text = "Active";
            btnSaveChannel.Text = "Save";
            isUpdate = false;
            txtChanCode.Text = "";
            txtNewChanel.Text = "";
            txtChanCode.ReadOnly = false;
            txtNewChanel.ReadOnly = false;
            txtEmp.Text = "";

        }


        private Deposit_Bank_Pc_wise filltoChanel()
        {
            obj_channels = new Deposit_Bank_Pc_wise();
            obj_channels.Company = BaseCls.GlbUserComCode;
            obj_channels.BankCode = txtChanCode.Text.Trim();
            obj_channels.Desc = txtNewChanel.Text.Trim();
            obj_channels.Create_by = BaseCls.GlbUserID;
            obj_channels.Modifyby = BaseCls.GlbUserID;
            obj_channels.ManagerCode = txtEmp.Text;

            return obj_channels;
        }


        private void btnSaveChannel_Click(object sender, EventArgs e)
        {
            #region validation

            if (txtChanCode.Text == "")
            {
                MessageBox.Show("Please enter code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNewChanel.Text.Trim() == "")
            {
                MessageBox.Show("Please enter channel", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtEmp.Text.Trim() == "")
            {
                MessageBox.Show("Please enter manager code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion

            try
            {

                if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                if (label90.Text == "Channel")
                {

                    if (isUpdate == false)
                    {
                        bool chek = CHNLSVC.General.check_avl_chn(BaseCls.GlbUserComCode, txtChanCode.Text.Trim());
                        if (chek == true)
                        {
                            MessageBox.Show("Thease Records are already inserted!!!..Try Again..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtChanCode.Text = "";
                            txtNewChanel.Text = "";
                            return;
                        }
                        string _error = "";
                        int result = CHNLSVC.General.Insert_to_chanelDets(filltoChanel(), out _error);
                        if (result == -1)
                        {
                            MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            chkActiveNew.Text = "Active";
                            btnSaveChannel.Text = "Save";
                            isUpdate = false;
                            txtChanCode.Text = "";
                            txtNewChanel.Text = "";
                            pnlNewChanel.Visible = false;

                        }


                    }
                    else
                    {
                        string _error = "";
                        int result = CHNLSVC.General.Update_to_chanelDets(filltoChanel(), out _error);
                        if (result == -1)
                        {
                            MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Records Updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            chkActiveNew.Text = "Active";
                            btnSaveChannel.Text = "Save";
                            isUpdate = false;
                            txtChanCode.ReadOnly = false;
                            txtNewChanel.ReadOnly = false;
                            txtChanCode.Text = "";
                            txtNewChanel.Text = "";
                            pnlNewChanel.Visible = false;

                        }
                    }


                }

                if (label90.Text == "Sub Channel")
                {

                    if (isUpdate == false)
                    {
                        DataTable chek = CHNLSVC.General.get_sub_chnl_stus(BaseCls.GlbUserComCode, txtChanCode.Text.Trim());
                        if (chek.Rows.Count > 0)
                        {
                            MessageBox.Show("Thease Records are already inserted!!!..Try Again..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtChanCode.Text = "";
                            txtNewChanel.Text = "";
                            return;
                        }
                        string _error = "";
                        int result = CHNLSVC.General.Insert_to_subchanelDets(filltoChanel(), out _error);
                        if (result == -1)
                        {
                            MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            chkActiveNew.Text = "Active";
                            btnSaveChannel.Text = "Save";
                            isUpdate = false;
                            txtChanCode.Text = "";
                            txtNewChanel.Text = "";
                            pnlNewChanel.Visible = false;

                        }


                    }
                    else
                    {
                        string _error = "";
                        int result = CHNLSVC.General.Update_to_subchanelDets(filltoChanel(), out _error);
                        if (result == -1)
                        {
                            MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Records Updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            chkActiveNew.Text = "Active";
                            btnSaveChannel.Text = "Save";
                            isUpdate = false;
                            txtChanCode.ReadOnly = false;
                            txtNewChanel.ReadOnly = false;
                            txtChanCode.Text = "";
                            txtNewChanel.Text = "";
                            pnlNewChanel.Visible = false;

                        }
                    }



                }


            }
            catch (Exception ex)
            {

                //throw ex;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
        }

        private void btnSearchChanel_Click(object sender, EventArgs e)
        {
            DataTable _result = null;
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;

                if (label90.Text == "Channel")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _CommonSearch.IsRawData = true;
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);

                }
                if (label90.Text == "Sub Channel")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _CommonSearch.IsRawData = true;
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChanCode;
                _CommonSearch.ShowDialog();
                txtChanCode.Select();
                if (txtChanCode.Text != "")
                {
                    DataRow[] foundRows;
                    foundRows = _result.Select("Code LIKE '" + txtChanCode.Text + "%'");
                    txtNewChanel.Text = foundRows[0][1].ToString();

                    if (label90.Text == "Channel")
                    {
                        DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "CHNL", txtChanCode.Text);
                        foreach (DataRow row2 in LocDes.Rows)
                        {
                            txtEmpNew.Text = row2["mgr_cd"].ToString();
                        }
                        DataTable dt_stus = CHNLSVC.General.get_stus_chnl(BaseCls.GlbUserComCode, txtChanCode.Text.Trim());
                        int status = Convert.ToInt32(dt_stus.Rows[0]["MSC_ACT"]);
                        if (status == 1)
                        {
                            btnSaveChannel.Text = "Edit";
                            chkActiveNew.Text = "Inactive";
                            isUpdate = true;
                            txtChanCode.ReadOnly = true;
                            txtNewChanel.ReadOnly = true;

                        }
                        else
                        {

                            btnSaveChannel.Text = "Edit";
                            chkActiveNew.Text = "Active";
                            isUpdate = true;
                            txtChanCode.ReadOnly = true;
                            txtNewChanel.ReadOnly = true;
                        }
                    }

                    if (label90.Text == "Sub Channel")
                    {
                        DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtChanCode.Text);
                        foreach (DataRow row2 in LocDes.Rows)
                        {
                            txtEmpNew.Text = row2["mgr_cd"].ToString();
                        }
                        DataTable dt_stus = CHNLSVC.General.get_sub_chnl_stus(BaseCls.GlbUserComCode, txtChanCode.Text.Trim());
                        int status = Convert.ToInt32(dt_stus.Rows[0]["MSSC_ACT"]);
                        if (status == 1)
                        {
                            btnSaveChannel.Text = "Edit";
                            chkActiveNew.Text = "Inactive";
                            isUpdate = true;
                            txtChanCode.ReadOnly = true;
                            txtNewChanel.ReadOnly = true;

                        }
                        else
                        {

                            btnSaveChannel.Text = "Edit";
                            chkActiveNew.Text = "Active";
                            isUpdate = true;
                            txtChanCode.ReadOnly = true;
                            txtNewChanel.ReadOnly = true;
                        }


                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCloseChanel_Click(object sender, EventArgs e)
        {
            chkActiveNew.Text = "Active";
            btnSaveChannel.Text = "Save";
            isUpdate = false;
            txtChanCode.Text = "";
            txtNewChanel.Text = "";
            txtNewChanel.ReadOnly = false;
            txtChanCode.ReadOnly = false;
            pnlNewChanel.Visible = false;
            btnNewSubChan.Enabled = true;
            btnChannel.Enabled = true;
        }

        private void btnNewSubChan_Click(object sender, EventArgs e)
        {
            pnlNewChanel.Visible = true;
            label90.Text = "Sub Channel";
            lblCreationNew.Text = "New Sub Channel Creation";
            btnChannel.Enabled = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (label90.Text == "Channel")
            {
                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                clsSalesRep objsales = new clsSalesRep();
                BaseCls.GlbReportName = "Channel_det_Report.rpt";
                BaseCls.GlbReportHeading = "Channel Details";

                string _cha = txtChanCode.Text.Trim();
                //string _desc = txtNewChanel.Text.Trim();
                objsales.get_chan_dets(_cha);
                _view.Show();
                _view = null;
                //BaseCls.GlbReportDataTable.Clear();
            }
            if (label90.Text == "Sub Channel")
            {
                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                clsSalesRep objsales = new clsSalesRep();
                BaseCls.GlbReportName = "Sub_channel_det_Report.rpt";
                BaseCls.GlbReportHeading = "Sub Channel Details";

                string _cha = txtChanCode.Text.Trim();
                objsales.get_sub_chan_dets(_cha);
                _view.Show();
                _view = null;
                BaseCls.GlbReportDataTable.Clear();
            }




        }

        private void btnNewPrnt_Click(object sender, EventArgs e)
        {
            if (lblDesnNew.Text == "Area")
            {
                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                clsSalesRep objsales = new clsSalesRep();
                BaseCls.GlbReportName = "Area_det_Report.rpt";
                BaseCls.GlbReportHeading = "Area Details";

                string _cha = txtCodeNew.Text.Trim();
                //string _desc = txtNewChanel.Text.Trim();
                objsales.get_area_dets(_cha);
                _view.Show();
                _view = null;
            }


            if (lblDesnNew.Text == "Region")
            {
                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                clsSalesRep objsales = new clsSalesRep();
                BaseCls.GlbReportName = "Region_det_Report.rpt";
                BaseCls.GlbReportHeading = "Region Details";

                string _cha = txtCodeNew.Text.Trim();
                //string _desc = txtNewChanel.Text.Trim();
                objsales.get_region_dets(_cha);
                _view.Show();
                _view = null;
            }


            if (lblDesnNew.Text == "Zone")
            {
                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                clsSalesRep objsales = new clsSalesRep();
                BaseCls.GlbReportName = "Zone_det_Report.rpt";
                BaseCls.GlbReportHeading = "Zone Details";

                string _cha = txtCodeNew.Text.Trim();
                //string _desc = txtNewChanel.Text.Trim();
                objsales.get_zone_dets(_cha);
                _view.Show();
                _view = null;
            }


        }

        private void txtChanCode_Leave(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;

            if (label90.Text == "Channel")
            {

                if (string.IsNullOrEmpty(txtChanCode.Text)) return;

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _CommonSearch.IsRawData = true;
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtChanCode.Text.Trim()).ToList();
                if (_validate != null && _validate.Count > 0)
                {
                    txtNewChanel.Text = _validate[0][1].ToString();

                }
            }


            if (label90.Text == "Sub Channel")
            {
                if (string.IsNullOrEmpty(txtChanCode.Text)) return;

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _CommonSearch.IsRawData = true;
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtChanCode.Text.Trim()).ToList();
                if (_validate.Count > 0)
                {
                    txtNewChanel.Text = _validate[0][1].ToString();

                }

            }
        }

        private void txtChanCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNewChanel.Focus();
            }
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            chkLstGV.Visible = false;
            chkLstScheme.Visible = false;
            _is_GVO_sel = false;

            if (!string.IsNullOrEmpty(txttpt_TxnType.Text))
            {
                if (chkLstPayType.CheckedItems.Count == 1)
                {
                    for (int i = 0; i < chkLstPayType.CheckedItems.Count; i++)
                    {
                        object itemChecked = chkLstPayType.CheckedItems[i];
                        if (((PaymentTypeRef)itemChecked).Sapt_cd == "GVO")
                        {
                            getTransType();
                            chkLstGV.Visible = true;
                            _is_GVO_sel = true;
                            if (_is_hp_rec == true)
                                chkLstScheme.Visible = true;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the transaction type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void chkLstPayType_Click(object sender, EventArgs e)
        {
            chkLstGV.Visible = false;
            chkLstScheme.Visible = false;
            _is_GVO_sel = false;

            for (int i = 0; i < chkLstGV.Items.Count; i++)
            {
                chkLstGV.SetItemChecked(i, false);
            }
            for (int i = 0; i < chkLstScheme.Items.Count; i++)
            {
                chkLstScheme.SetItemChecked(i, false);
            }

        }

        private void btn_srch_emp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeAll);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_All(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEmp;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtEmp.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEmp_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmp.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtEmp.Text);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid employee code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmp.Text = "";
                    txtEmp.Focus();
                    return;
                }
            }
        }

        private void btn_srch_empnew_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeAll);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_All(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEmpNew;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtEmpNew.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEmp_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_emp_Click(null, null);
        }

        private void txtEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_emp_Click(null, null);
        }

        private void txtEmpNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_empnew_Click(null, null);
        }

        private void txtEmpNew_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_empnew_Click(null, null);
        }

        private void btnPromoExcel_Click(object sender, EventArgs e)
        {
            if (_promoLst != null && _promoLst.Count > 0)
            {
                if (MessageBox.Show("Currently you have select a list of promotions .If you continue, the present excel data will be lost. Do you need to continue anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    _promoLst = new List<PromoList>();
                else
                    return;
            }

            if (pnlExcel.Visible)
                pnlExcel.Visible = false;
            else
            {
                pnlExcel.Location = new Point(464, 335);
                pnlExcel.Visible = true;
            }

            IsPromoExcel = true;
            _excel_up_type = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pnlExle_loc.Visible = false;
        }

        private void btnuploadpc_Click(object sender, EventArgs e)
        {
            label100.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtexcle.Text = _obj[_obj.Length - 1].ToString();
            label100.Text = openFileDialog1.FileName;
        }

        private void btnUploadFile_exle_Click(object sender, EventArgs e)
        {
            excleupload();
        }

        private void excleupload() //Tharanga 2017/07/03
        {
            _list.Clear();
            Base _basePage;
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(txtexcle.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtexcle.Clear();
                label100.Text = string.Empty;
                txtexcle.Focus();
                return;
            }

            if (string.IsNullOrEmpty(label100.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtexcle.Clear();
                label100.Text = string.Empty;
                txtexcle.Focus();
                return;
            }


            System.IO.FileInfo _fileObj = new System.IO.FileInfo(label100.Text);




            if (_fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Focus();
            }

            string Extension = _fileObj.Extension;

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
            else
            {
                MessageBox.Show("Invalid upload file Format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }


            string _excelConnectionString = ConfigurationManager.ConnectionStrings[Extension == ".XLS" ? "ConStringExcel03" : "ConStringExcel07"].ConnectionString;
            _excelConnectionString = String.Format(conStr, label100.Text, "YES");
            OleDbConnection _connExcel = new OleDbConnection(_excelConnectionString);
            OleDbCommand _cmdExcel = new OleDbCommand();
            OleDbDataAdapter _oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            _cmdExcel.Connection = _connExcel;
            int count = 0;
            try
            {
                _connExcel.Open();
            }
            catch (Exception)
            {

                MessageBox.Show("Excel file cannot be open during upload ! Please close it and process !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Get the name of First Sheet


            DataTable _dtExcelSchema;
            _dtExcelSchema = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string _sheetName = _dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            _connExcel.Close();

            _connExcel.Open();
            _cmdExcel.CommandText = "SELECT * From [" + _sheetName + "]";
            _oda.SelectCommand = _cmdExcel;
            _oda.Fill(_dt);
            _connExcel.Close();
            _itemLst = new List<ItemList>();
            _promoLst = new List<PromoList>();



            gvPcHierarchy.Rows.Clear();
            StringBuilder _errorLst = new StringBuilder();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (_dt.Rows.Count > 0)
            {
                if (_dt.Columns.Count != 4)
                {
                    MessageBox.Show("Data mismatch or not available! [Com / Loc / Code / Value  ]", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (_excel_up_type == 0)    //item excel
                {
                    _basePage = new Base();
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        string vale = _dr[3].ToString().ToUpper().Trim().ToString();
                        string type = _dr[2].ToString().ToUpper().Trim().ToString();
                        string pc = _dr[1].ToString().ToUpper().Trim().ToString();
                        string com = _dr[0].ToString().ToUpper().Trim().ToString();


                        if (type != "ZONE" || type != "SCHNL" || type != "REGION" || type != "PC" || type != "GPC" || type != "COM" || type != "CHNL" || type != "AREA")
                        {
                            MessageBox.Show("Invalid Code" + " " + type, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (com == "")
                        {
                            MessageBox.Show("Please check the company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (BaseCls.GlbUserComCode != com)
                        {
                            MessageBox.Show("Please check the company" + " " + com, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (!_basePage.CHNLSVC.General.CheckProfitCenter(com, pc))
                        {
                            MessageBox.Show("Please check the Profit Center." + " " + pc, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        DataTable _locationDetails = new DataTable();
                        //_locationDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, Loc);
                        //if (_locationDetails.Rows.Count <= 0)
                        //{
                        //    MessageBox.Show("Please check the Location." + " " + Loc, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "ZONE")
                        {

                            _basePage = new Base();
                            if (!_basePage.CHNLSVC.General.CheckZone(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the Zone." + " " + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "SCHNL")
                        {
                            _basePage = new Base();

                            if (!_basePage.CHNLSVC.General.CheckSubChannel(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the Sub Channel." + " " + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "REGION")
                        {
                            _basePage = new Base();
                            if (!_basePage.CHNLSVC.General.CheckRegion(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the channel." + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "PC")
                        {
                            _basePage = new Base();
                            if (!_basePage.CHNLSVC.General.CheckProfitCenter(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the PC." + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (pc != vale)
                            {
                                MessageBox.Show("PC mismatch.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "GPC")
                        {
                            _basePage = new Base();
                            if (vale != "GRUP01")
                            {
                                MessageBox.Show("Please check the GPC." + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "COM")
                        {
                            if (BaseCls.GlbUserComCode != vale)
                            {
                                MessageBox.Show("Please check the company" + " " + com, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "CHNL")
                        {
                            _basePage = new Base();

                            if (!_basePage.CHNLSVC.General.CheckChannel(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the Channel." + " " + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "AREA")
                        {
                            _basePage = new Base();

                            if (!_basePage.CHNLSVC.General.CheckArea(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the Area." + " " + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }



                        PcList _pclist = new PcList();
                        _pclist.com_cd = BaseCls.GlbUserComCode.ToString();
                        _pclist.pc_cd = (_dr[1].ToString().ToUpper().Trim()).ToString();
                        _pclist.Type = (_dr[2].ToString().ToUpper().Trim()).ToString();
                        _pclist.Type_value = (_dr[3].ToString().ToUpper().Trim()).ToString();
                        _pclist.active = "1";
                        _list.Add(_pclist);

                    }
                    lblexclestates.Visible = true;
                    pnlExleupload.Visible = false;
                    foreach (var item in _list)
                    {
                        //  gvPcHierarchy.Rows.Add();
                        //  gvPcHierarchy.Rows[count].Cells["ps_pc"].Value = item.pc_cd.ToString();
                        ////  gvPcHierarchy.Rows[count].Cells["ps_zone"].Value = item.Zone.ToString();


                        count++;

                    }



                    if (!string.IsNullOrEmpty(_errorLst.ToString()))
                    {
                        if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            _itemLst = new List<ItemList>();
                            pnlExleupload.Visible = true;
                            label103.Text = "";
                        }
                    }
                }

            }

            pnlExleupload.Visible = false;
            pnlExle_loc.Visible = false;
            is_excel_upload = 1;

        }

        private void btn_exleUpload_Click(object sender, EventArgs e)
        {
            pnlExleupload.Visible = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            pnlExle_loc.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label103.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtexle.Text = _obj[_obj.Length - 1].ToString();
            label103.Text = openFileDialog1.FileName;
        }


        //private void imgChaSearch_Click(object sender, EventArgs e)
        //{
        //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //    DataTable _result = null;
        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
        //    _CommonSearch.IsRawData = false;
        //    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

        //    _CommonSearch.dvResult.DataSource = _result;
        //    _CommonSearch.BindUCtrlDDLData(_result);
        //    _CommonSearch.obj_TragetTextBox = TextBoxChannel;
        //    _CommonSearch.ShowDialog();

        //    TextBoxChannel.Focus();
        //}

        //private void imgSubChaSearch_Click(object sender, EventArgs e)
        //{
        //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //    DataTable _result = null;
        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
        //    _CommonSearch.IsRawData = false;
        //    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
        //    _CommonSearch.dvResult.DataSource = _result;
        //    _CommonSearch.BindUCtrlDDLData(_result);
        //    _CommonSearch.obj_TragetTextBox = TextBoxSubChannel;
        //    _CommonSearch.ShowDialog();

        //    TextBoxSubChannel.Focus();
        //}

        //private void imgAreaSearch_Click(object sender, EventArgs e)
        //{
        //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //    DataTable _result = null;

        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
        //    _CommonSearch.IsRawData = false;
        //    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

        //    _CommonSearch.dvResult.DataSource = _result;
        //    _CommonSearch.BindUCtrlDDLData(_result);
        //    _CommonSearch.obj_TragetTextBox = TextBoxArea;
        //    _CommonSearch.ShowDialog();

        //    TextBoxArea.Focus();
        //}

        //private void imgRegionSearch_Click(object sender, EventArgs e)
        //{
        //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //    DataTable _result = null;

        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
        //    _CommonSearch.IsRawData = false;
        //    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

        //    _CommonSearch.dvResult.DataSource = _result;
        //    _CommonSearch.BindUCtrlDDLData(_result);
        //    _CommonSearch.obj_TragetTextBox = TextBoxRegion;
        //    _CommonSearch.ShowDialog();

        //    TextBoxRegion.Focus();
        //}

        //private void imgZoneSearch_Click(object sender, EventArgs e)
        //{
        //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //    DataTable _result = null;

        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
        //    _CommonSearch.IsRawData = false;
        //    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

        //    _CommonSearch.dvResult.DataSource = _result;
        //    _CommonSearch.BindUCtrlDDLData(_result);
        //    _CommonSearch.obj_TragetTextBox = TextBoxZone;
        //    _CommonSearch.ShowDialog();

        //    TextBoxZone.Focus();
        //}

        //private void imgProCeSearch_Click(object sender, EventArgs e)
        //{
        //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
        //    DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
        //    _CommonSearch.dvResult.DataSource = _result;
        //    _CommonSearch.BindUCtrlDDLData(_result);

        //    _CommonSearch.obj_TragetTextBox = TextBoxLocation;
        //    _CommonSearch.ShowDialog();
        //    TextBoxLocation.Focus();
        //}

        private void excleupload_loc() //Tharanga 2017/07/03
        {
            _list.Clear();
            Base _basePage;
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(txtexle.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtexcle.Clear();
                label100.Text = string.Empty;
                txtexcle.Focus();
                return;
            }

            if (string.IsNullOrEmpty(label103.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtexcle.Clear();
                label100.Text = string.Empty;
                txtexcle.Focus();
                return;
            }


            System.IO.FileInfo _fileObj = new System.IO.FileInfo(label103.Text);




            if (_fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Focus();
            }

            string Extension = _fileObj.Extension;

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
            else
            {
                MessageBox.Show("Invalid upload file Format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }


            string _excelConnectionString = ConfigurationManager.ConnectionStrings[Extension == ".XLS" ? "ConStringExcel03" : "ConStringExcel07"].ConnectionString;
            _excelConnectionString = String.Format(conStr, label103.Text, "YES");
            OleDbConnection _connExcel = new OleDbConnection(_excelConnectionString);
            OleDbCommand _cmdExcel = new OleDbCommand();
            OleDbDataAdapter _oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            _cmdExcel.Connection = _connExcel;
            int count = 0;

            //Get the name of First Sheet
            try
            {
                _connExcel.Open();
            }
            catch (Exception)
            {

                MessageBox.Show("Excel file cannot be open during upload ! Please close it and process !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable _dtExcelSchema;
            _dtExcelSchema = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string _sheetName = _dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            _connExcel.Close();

            _connExcel.Open();
            _cmdExcel.CommandText = "SELECT * From [" + _sheetName + "]";
            _oda.SelectCommand = _cmdExcel;
            _oda.Fill(_dt);
            _connExcel.Close();
            _itemLst = new List<ItemList>();
            _promoLst = new List<PromoList>();



            gvPcHierarchy.Rows.Clear();
            StringBuilder _errorLst = new StringBuilder();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (_dt.Rows.Count > 0)
            {
                if (_dt.Columns.Count != 4)
                {
                    MessageBox.Show("Data mismatch or not available! [Com / Loc / Code / Value ]", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (_excel_up_type == 0)    //item excel
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        string vale = _dr[3].ToString().ToUpper().Trim().ToString();
                        string type = _dr[2].ToString().ToUpper().Trim().ToString();
                        string Loc = _dr[1].ToString().ToUpper().Trim().ToString();
                        string com = _dr[0].ToString().ToUpper().Trim().ToString();









                        if (type != "ZONE" || type != "SCHNL" || type != "REGION" || type != "LOC" || type != "GPC" || type != "COM" || type != "CHNL" || type != "AREA")
                        {
                            MessageBox.Show("Invalid Code" + " " + type, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (com == "")
                        {
                            MessageBox.Show("Please check the company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (BaseCls.GlbUserComCode != com)
                        {
                            MessageBox.Show("Please check the company" + " " + com, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        DataTable _locationDetails = new DataTable();
                        _locationDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, Loc);
                        if (_locationDetails.Rows.Count <= 0)
                        {
                            MessageBox.Show("Please check the Location." + " " + Loc, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "ZONE")
                        {
                            _basePage = new Base();
                            if (!_basePage.CHNLSVC.General.CheckZone(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the Zone." + " " + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "SCHNL")
                        {
                            _basePage = new Base();

                            if (!_basePage.CHNLSVC.General.CheckSubChannel(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the Sub Channel." + " " + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "REGION")
                        {
                            _basePage = new Base();
                            if (!_basePage.CHNLSVC.General.CheckRegion(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the channel." + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "LOC")
                        {
                            _basePage = new Base();
                            DataTable odt = _basePage.CHNLSVC.General.LoadLocationDetailsByCode(BaseCls.GlbUserComCode, vale);
                            if (odt.Rows.Count <= 0)
                            {
                                MessageBox.Show("Please check the Location." + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (Loc != vale)
                            {
                                MessageBox.Show("location mismatch." + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //if (!_basePage.CHNLSVC.General.LoadLocationDetailsByCode((BaseCls.GlbUserComCode, vale))
                            //{
                            //    MessageBox.Show("Please check the Location." + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "GPC")
                        {
                            _basePage = new Base();
                            if (vale != "GRUP01")
                            {
                                MessageBox.Show("Please check the GPC." + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "COM")
                        {
                            if (BaseCls.GlbUserComCode != vale)
                            {
                                MessageBox.Show("Please check the company" + " " + com, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "CHNL")
                        {
                            _basePage = new Base();

                            if (!_basePage.CHNLSVC.General.CheckChannel(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the Channel." + " " + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        if ((_dr[2].ToString().ToUpper().Trim()).ToString() == "AREA")
                        {
                            _basePage = new Base();

                            if (!_basePage.CHNLSVC.General.CheckArea(BaseCls.GlbUserComCode, vale))
                            {
                                MessageBox.Show("Please check the Area." + " " + (_dr[3].ToString().ToUpper().Trim()).ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }







                        PcList _pclist = new PcList();
                        _pclist.com_cd = BaseCls.GlbUserComCode.ToString();
                        _pclist.pc_cd = (_dr[1].ToString().ToUpper().Trim()).ToString();
                        _pclist.Type = (_dr[2].ToString().ToUpper().Trim()).ToString();
                        _pclist.Type_value = (_dr[3].ToString().ToUpper().Trim()).ToString();
                        _pclist.active = "1";
                        _list.Add(_pclist);

                    }
                    lblexclestates.Visible = true;
                    is_excel_upload = 1;

                    foreach (var item in _list)
                    {
                        //  gvPcHierarchy.Rows.Add();
                        //  gvPcHierarchy.Rows[count].Cells["ps_pc"].Value = item.pc_cd.ToString();
                        ////  gvPcHierarchy.Rows[count].Cells["ps_zone"].Value = item.Zone.ToString();


                        count++;

                    }



                    if (!string.IsNullOrEmpty(_errorLst.ToString()))
                    {
                        if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            _itemLst = new List<ItemList>();
                            pnlExleupload.Visible = true;
                        }
                    }
                }

            }

            pnlExleupload.Visible = false;
            pnlExle_loc.Visible = false;
            is_excel_upload = 1;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            excleupload_loc();
        }

        private void pnlExleupload_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParametersnew(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtCustomer;
                _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCustomer.Select();
            }
            catch (Exception ex)
            {
                txtCustomer.Clear();
                this.Cursor = Cursors.Default;

            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Customer_Click(null, null);
        }

        private void txtCustomer_Leave(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtCustomer.Text))
            //_masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, BaseCls.GlbUserComCode);
            if (_masterBusinessCompany.Mbe_cd != null)
            {


                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        MessageBox.Show("This customer already inactive. Please contact Accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCustomer.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomer.Text = "";
                    txtCustomer.Focus();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(txtCustomer.Text))
                {
                    MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomer.Text = "";
                    txtCustomer.Focus();
                }
            }

        }

        private string SetCommonSearchInitialParametersnew(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        //Added by Udesh 12-Nov-2018
        private void btnSearchBinBankID_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankALL);
            DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompanyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBinBankCode;
            _CommonSearch.ShowDialog();
            txtBinBankCode.Select();

            load_bin_bank_det();
        }

        //Added by Udesh 12-Nov-2018
        private void InitializeBinGridTable()
        {
            dtBinGrid = new DataTable();
            dtBinGrid.Columns.Add("RBA_BNK_ID");
            dtBinGrid.Columns.Add("RBA_TP");
            dtBinGrid.Columns.Add("RBA_TO_DT");// Item To Status
            dtBinGrid.Columns.Add("RBA_FRM_DT");// Item From Status
            dtBinGrid.Columns.Add("RBA_BIN_NO");
            dgvBin.AutoGenerateColumns = false;
        }

        //Added by Udesh 12-Nov-2018
        private void btnAddBinDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBinBankCode.Text))
                {
                    MessageBox.Show("Please select bank code", "Bin Assignment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(cmbBinCardType.Text))
                {
                    MessageBox.Show("Please select card type", "Bin Assignment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (dtpBinFromDate.Value.Date > dtpBinToDate.Value.Date)
                {
                    MessageBox.Show("Please enter valid date range", "Bin Assignment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(txtBinNumber.Text))
                {
                    MessageBox.Show("Please enter bin number", "Bin Assignment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtBinNumber.Text.Length > 10)
                {
                    MessageBox.Show("Bin number should be less than 10 digits", "Bin Assignment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DataTable _dtTempBinGrid = dtBinGrid.Clone();
                DataRow _newRow = _dtTempBinGrid.NewRow();

                _newRow["RBA_BNK_ID"] = txtBinBankCode.Text;
                _newRow["RBA_TP"] = cmbBinCardType.SelectedValue.ToString();
                _newRow["RBA_FRM_DT"] = dtpBinFromDate.Value.Date.ToShortDateString();
                _newRow["RBA_TO_DT"] = dtpBinToDate.Value.Date.ToShortDateString();
                _newRow["RBA_BIN_NO"] = txtBinNumber.Text;

                _dtTempBinGrid.Rows.Add(_newRow);

                dtBinGrid.Merge(_dtTempBinGrid);

                dgvBin.DataSource = null;
                dgvBin.AutoGenerateColumns = false;
                dgvBin.DataSource = dtBinGrid;

                ClearBinAssignment(_isClearAll: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Added by Udesh 12-Nov-2018
        private void txtBinNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Added by Udesh 12-Nov-2018
        private void btnClearBin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear bin assignment data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ClearBinAssignment(_isClearAll: true);
            }
        }

        //Added by Udesh 12-Nov-2018
        private void ClearBinAssignment(bool _isClearAll)
        {
            try
            {
                txtBinBankCode.ResetText();
                lblBinBankName.ResetText();
                txtBinNumber.ResetText(); 
                cmbBinCardType.DataSource = null;
                dtpBinFromDate.Value = CHNLSVC.Security.GetServerDateTime();
                dtpBinToDate.Value = CHNLSVC.Security.GetServerDateTime();

                if (_isClearAll)
                {
                    dtBinGrid.Rows.Clear();
                    dgvBin.DataSource = null;
                    dgvBin.AutoGenerateColumns = false;

                    btnSaveBin.Enabled = true;
                    btnBinExcelUpload.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //Added by Udesh 12-Nov-2018
        private bool BinAssignmentTableValidation()
        {
            StringBuilder _errorLst = new StringBuilder();

            if (dtBinGrid.Rows.Count < 1)
            {
                MessageBox.Show("No data to save", "Bin Assignment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            for (int _count = 0; _count < dtBinGrid.Rows.Count; _count++)
            {
                if (dtBinGrid.Rows[_count]["RBA_BNK_ID"] == null || string.IsNullOrEmpty(dtBinGrid.Rows[_count]["RBA_BNK_ID"].ToString()))
                {
                    _errorLst.Append(string.Format("Empty bank code in line number {0}", _count++));
                }

                if (dtBinGrid.Rows[_count]["RBA_TP"] == null || string.IsNullOrEmpty(dtBinGrid.Rows[_count]["RBA_TP"].ToString()))
                {
                    _errorLst.Append(string.Format("Empty card type in line number {0}", _count++));
                }

                if (dtBinGrid.Rows[_count]["RBA_FRM_DT"] == null || string.IsNullOrEmpty(dtBinGrid.Rows[_count]["RBA_FRM_DT"].ToString()))
                {
                    _errorLst.Append(string.Format("Empty From Date in line number {0}", _count++));
                }

                if (dtBinGrid.Rows[_count]["RBA_TO_DT"] == null || string.IsNullOrEmpty(dtBinGrid.Rows[_count]["RBA_TO_DT"].ToString()))
                {
                    _errorLst.Append(string.Format("Empty To Date in line number {0}", _count++));
                }

                if (dtBinGrid.Rows[_count]["RBA_BIN_NO"] == null || string.IsNullOrEmpty(dtBinGrid.Rows[_count]["RBA_BIN_NO"].ToString()))
                {
                    _errorLst.Append(string.Format("Empty bin number in line number {0}", _count++));
                }

            }



            if (!string.IsNullOrEmpty(_errorLst.ToString()))
            {
                MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString(), "Bin Assignment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Clipboard.SetText(_errorLst.ToString());
                return false;
            }

            return true;
        }

        //Added by Udesh 12-Nov-2018
        private void btnSaveBin_Click(object sender, EventArgs e)
        {
            try
            {
                string _errr = string.Empty;

                if (MessageBox.Show("Are you sure to save?", "Bin Assignment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                if (BinAssignmentTableValidation())
                {
                    List<REF_BIN_ASSIGN> _binAssignList = new List<REF_BIN_ASSIGN>();

                    foreach (DataGridViewRow _row in dgvBin.Rows)
                    {
                        REF_BIN_ASSIGN _binAssign = new REF_BIN_ASSIGN()
                            {
                                RBA_BNK_ID = _row.Cells["RBA_BNK_ID"].Value.ToString(),
                                RBA_TP = _row.Cells["RBA_TP"].Value.ToString(),
                                RBA_FRM_DT = DateTime.Parse(_row.Cells["RBA_FRM_DT"].Value.ToString()),
                                RBA_TO_DT = DateTime.Parse(_row.Cells["RBA_TO_DT"].Value.ToString()),
                                RBA_BIN_NO = _row.Cells["RBA_BIN_NO"].Value.ToString(),
                                RBA_ACT = true,
                                RBA_SESSION_ID = BaseCls.GlbUserSessionID,
                                RBA_CRE_BY = BaseCls.GlbUserID,
                                RBA_CRE_DT = CHNLSVC.Security.GetServerDateTime()
                            };
                        _binAssignList.Add(_binAssign);
                    }

                    int row_aff = CHNLSVC.General.SaveBinAssignment(_binAssignList, out _errr);
                    if (row_aff >= 0)
                    {
                        MessageBox.Show("Successfully Updated", "Bin Assignment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearBinAssignment(true);
                    }
                    else
                    {
                        MessageBox.Show(_errr, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Added by Udesh 12-Nov-2018
        private void btnBinViewNumbers_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBinBankCode.Text))
                {
                    MessageBox.Show("Please select bank code", "Bin Assignment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                dtBinGrid.Rows.Clear();

                REF_BIN_ASSIGN _binAssign = new REF_BIN_ASSIGN()
                           {
                               RBA_BNK_ID = txtBinBankCode.Text
                           };

                dtBinGrid = CHNLSVC.General.GetBinAssignmentDetailsByBankCode(_binAssign);

                dgvBin.DataSource = null;
                dgvBin.AutoGenerateColumns = false;
                dgvBin.DataSource = dtBinGrid;

                btnSaveBin.Enabled = false;
                btnBinExcelUpload.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Added by Udesh 12-Nov-2018
        private void btnBinExpand_Click(object sender, EventArgs e)
        {
            using (Form form = new Form())
            {
                form.Text = "Bin Assignment Details";
                form.ShowIcon = false;
                form.ClientSize = new System.Drawing.Size(550, 400);
                this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

                System.Windows.Forms.DataGridView dgvBinExpand = new DataGridViewHelper().CopyDataGridView(this.dgvBin, dtBinGrid);
                form.Controls.Add(dgvBinExpand);
                dgvBinExpand.Dock = System.Windows.Forms.DockStyle.Fill;
                form.ShowDialog();
            }
        }

        //Added by Udesh 12-Nov-2018
        private void btnBinExcelUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string _filePath = openFileDialog1.FileName;
                UploadBinExcel(_filePath);
            }
        }
        private void UploadBinExcel(string _filePath)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dtBinGrid.Rows.Clear();
                DataTable _dtTempBinGrid = dtBinGrid.Clone();

                string _msg = string.Empty;
                StringBuilder _errorLst = new StringBuilder();
                DataTable _dtTempParty = dtBinGrid.Clone();

                if (string.IsNullOrEmpty(_filePath))
                {
                    MessageBox.Show("Please select upload file path.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(_filePath);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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


                conStr = String.Format(conStr, _filePath, "NO");
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

                if (dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(0);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        CommonSearch.CommonSearch _bankCodeSearch = new CommonSearch.CommonSearch();
                        DataTable _dtBankCode = new DataTable();
                        DataTable _dtCardType = new DataTable();

                        #region Prepare Master Data

                        _bankCodeSearch.ReturnIndex = 0;
                        _bankCodeSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankALL);
                        _dtBankCode = CHNLSVC.CommonSearch.GetBusinessCompanyData(_bankCodeSearch.SearchParams, null, null);

                        _dtCardType = CHNLSVC.Sales.GetBankCC(null);

                        #endregion

                        int _rowCount = 0;
                        foreach (DataRow _dr in dt.Rows)
                        {
                            _rowCount++;

                            #region Bank Code
                            string _bankCode = _dr[0].ToString().ToUpper().Trim();

                            if (string.IsNullOrEmpty(_bankCode))
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty bank code contain in row number {0}", _rowCount));
                                else _errorLst.Append(string.Format(" and Empty bank code contain in row number {0}", _rowCount));
                                continue;
                            }

                            var _resultBankCode = _dtBankCode.AsEnumerable().Where(r => r.Field<string>("Code") == _bankCode).FirstOrDefault();
                            if (_resultBankCode == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Invalid bank code - " + _bankCode);
                                else _errorLst.Append(" and Invalid bank code - " + _bankCode);
                                continue;
                            }

                            #endregion

                            #region Card Type
                            string _cardType = _dr[1].ToString().ToUpper().Trim();

                            if (string.IsNullOrEmpty(_cardType))
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty card type contain in row number {0}", _rowCount));
                                else _errorLst.Append(string.Format(" and Empty card type contain in row number {0}", _rowCount));
                                continue;
                            }

                            var _resultCardType = _dtCardType.AsEnumerable().Where(r => r.Field<string>("mbct_cc_tp") == _cardType).FirstOrDefault();
                            if (_resultCardType == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Invalid card type - " + _cardType);
                                else _errorLst.Append(" and Invalid card type - " + _cardType);
                                continue;
                            }

                            #endregion

                            #region Date
                            string _fromValue = _dr[2].ToString().Trim();
                            if (string.IsNullOrEmpty(_fromValue))
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty from date contain in row number {0}", _rowCount));
                                else _errorLst.Append(string.Format(" and Empty from date contain in row number {0}", _rowCount));
                                continue;
                            }

                            string _toValue = _dr[3].ToString().Trim();
                            if (string.IsNullOrEmpty(_toValue))
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty to date contain in row number {0}", _rowCount));
                                else _errorLst.Append(string.Format(" and Empty to date contain in row number {0}", _rowCount));
                                continue;
                            }

                            DateTime _fromDate = new DateTime();
                            DateTime _toDate = new DateTime();

                            if (!DateTime.TryParse(_fromValue, out _fromDate))
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("From date must be date value - " + _fromDate);
                                else _errorLst.Append(" and from date must be date value - " + _fromDate);
                                continue;
                            }
                            if (!DateTime.TryParse(_toValue, out _toDate))
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("To date must be date value - " + _toValue);
                                else _errorLst.Append(" and to date must be date value - " + _toValue);
                                continue;
                            }

                            if (_fromDate > _toDate)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Invalid date range contain in row number {0}", _rowCount));
                                else _errorLst.Append(string.Format(" and Invalid date range contain in row number {0}", _rowCount));
                                continue;
                            }


                            #endregion

                            #region Bin Number

                            string _binNumber = _dr[4].ToString().Trim();
                            decimal _value = 0;

                            if (string.IsNullOrEmpty(_binNumber))
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty bin number contain in row number {0}", _rowCount));
                                else _errorLst.Append(string.Format(" and Empty bin number contain in row number {0}", _rowCount));
                                continue;
                            }

                            if (!decimal.TryParse(_binNumber, out _value))
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Bin number must be numeric value - " + _binNumber);
                                else _errorLst.Append(" and bin number must be numeric value - " + _binNumber);
                                continue;
                            }

                            if (_binNumber.Length>10)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Bin number should be less than 10 digits in row number {0}", _rowCount));
                                else _errorLst.Append(string.Format(" and Bin number should be less than 10 digits in row number {0}", _rowCount));
                                continue;
                            }

                            #endregion

                            DataRow _newRow = _dtTempBinGrid.NewRow();

                            _newRow["RBA_BNK_ID"] = _bankCode;
                            _newRow["RBA_TP"] = _cardType;
                            _newRow["RBA_FRM_DT"] = _fromDate.ToString("dd/MMM/yyyy");
                            _newRow["RBA_TO_DT"] = _toDate.ToString("dd/MMM/yyyy");
                            _newRow["RBA_BIN_NO"] = _binNumber;

                            _dtTempBinGrid.Rows.Add(_newRow);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(_errorLst.ToString()))
                {
                    MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString(), "Discrepancies", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Clipboard.SetText(_errorLst.ToString());
                    return;
                }

                dtBinGrid.Merge(_dtTempBinGrid);
                dgvBin.DataSource = null;
                dgvBin.AutoGenerateColumns = false;
                dgvBin.DataSource = dtBinGrid;

            }


            catch (Exception err)
            {
                if (err.Message.Contains("already opened"))
                {
                    MessageBox.Show("The Microsoft Office is already opened exclusively by another user, or you need permission to view and write its data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Unable to upload. please select the correct file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }


    }
}
