using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.General
{
    public partial class LocationNProfitCenterCreation : FF.WindowsERPClient.Base
    {
        //Written By Prabhath on 16/04/2013

        #region Variable
        private List<SystemUserCompany> _company = null;
        List<string> DirectOperation = new List<string>() { string.Empty, "SCM", "SCM2", "POS" };
        List<string> YesNoSelection = new List<string>() { string.Empty, "Yes", "No" };
        List<string> CenterType = new List<string>() { string.Empty, "Cost", "Profit" };
        List<string> ActiveInactiveStatus = new List<string>() { string.Empty, "Active", "Inactive" };
        DataRow _rows = null;
        #endregion

        public LocationNProfitCenterCreation()
        {
            InitializeComponent();
            LocationNProfitCenterInitializing();
        }
        private void LocationNProfitCenterCreation_Shown(object sender, EventArgs e)
        {
            lcmbCompany_SelectedIndexChanged(null, null);
        }

        #region Common Implementation
        private void LocationNProfitCenterInitializing()
        {
            LoadCompany();
            LoadDirectOperation();
            LoadOnlineSystem();
            LoadLocationType();
            LoadMainCategory();
            LoadSub2Category();
            LoadCountry();
            LoadProvince();

            LoadCenterType();
            LoadBankCurrency();
        }
        #endregion
        #region Form Initialization - Location
        private void LoadCompany()
        {
            if (_company == null) _company = new List<SystemUserCompany>();
            _company = CHNLSVC.Security.GetUserCompany(BaseCls.GlbUserID);
            BindingSource _source = new BindingSource();
            _source.DataSource = _company;
            lcmbCompany.DataSource = _source.DataSource;
            lcmbCompany.DisplayMember = "SEC_COM_CD";

            pcmbCompany.DataSource = _source.DataSource;
            pcmbCompany.DisplayMember = "SEC_COM_CD";

            lcmbCompany.SelectedText = BaseCls.GlbUserComCode;

        }
        private void LoadDirectOperation()
        {
            lcmbDirectOperation.DataSource = DirectOperation;

        }
        private void LoadOperationAdminTeam(string _company)
        {
            DataTable _adminTeam = CHNLSVC.General.GetOperationAdminTeam(_company);
            _rows = _adminTeam.NewRow();
            _adminTeam.Rows.InsertAt(_rows, 0);
            lcmbOperationAdminTeam.DataSource = _adminTeam;
            pcmbOperationAdminTeam.DataSource = _adminTeam;
            lcmbOperationAdminTeam.DisplayMember = "mso_cd";
            pcmbOperationAdminTeam.DisplayMember = "mso_cd";
            lcmbOperationAdminTeam.Text = "";
            pcmbOperationAdminTeam.Text = "";
        }
        private void LoadOnlineSystem()
        {
            lcmbOnline.DataSource = YesNoSelection;
        }
        private void LoadLocationType()
        {
            DataTable _loctype = CHNLSVC.General.GetLocationType();
            _loctype.AsEnumerable().ToList().ForEach(x => x.SetField("rlt_desc", FormulateDisplayText(x.Field<string>("rlt_desc"))));
            int _maxlength = 18;
            if (_loctype != null)
                if (_loctype.Rows.Count > 0)
                    _maxlength = _loctype.AsEnumerable().ToList().Max(x => string.IsNullOrEmpty(Convert.ToString(x.Field<string>("rlt_desc"))) ? 20 : x.Field<string>("rlt_desc").Length);
            _rows = _loctype.NewRow();
            _loctype.Rows.InsertAt(_rows, 0);
            lcmbLocationType.DataSource = _loctype;
            lcmbLocationType.DisplayMember = "rlt_desc";
            lcmbLocationType.ValueMember = "rlt_cd";
            lcmbLocationType.Text = string.Empty;
            lcmbMainCategory.DropDownWidth = Convert.ToInt32(_maxlength * 5.5);
        }
        private void LoadGrade(string _company, string _loctype)
        {
            DataTable _grade = CHNLSVC.General.GetLocationGrade(_company, _loctype);
            _grade.Rows.Add(string.Empty);
            lcmbGrade.DataSource = _grade;
            lcmbGrade.DisplayMember = "lgrd_grd_cd";
            lcmbGrade.Text = string.Empty;

        }
        private void LoadMainCategory()
        {
            DataTable _main = CHNLSVC.General.GetLocationCategory1();
            _main.AsEnumerable().ToList().ForEach(x => x.SetField("rlc_desc", FormulateDisplayText(x.Field<string>("rlc_desc"))));
            int _maxlength = 18;
            if (_main != null)
                if (_main.Rows.Count > 0)
                    _maxlength = _main.AsEnumerable().ToList().Max(x => string.IsNullOrEmpty(Convert.ToString(x.Field<string>("rlc_desc"))) ? 20 : x.Field<string>("rlc_desc").Length);
            _rows = _main.NewRow();
            _main.Rows.InsertAt(_rows, 0);
            lcmbMainCategory.DataSource = _main;
            lcmbMainCategory.DisplayMember = "rlc_desc";
            lcmbMainCategory.ValueMember = "rlc_cd";
            lcmbMainCategory.Text = string.Empty;
            lcmbMainCategory.DropDownWidth = Convert.ToInt32(_maxlength * 5.5);

        }
        private void LoadSub1Category(string _company)
        {
            DataTable _sub1 = CHNLSVC.General.GetLocationCategory2(_company);
            _sub1.AsEnumerable().ToList().ForEach(x => x.SetField("msc_desc", FormulateDisplayText(x.Field<string>("msc_desc"))));
            int _maxlength = 18;
            if (_sub1 != null)
                if (_sub1.Rows.Count > 0)
                    _maxlength = _sub1.AsEnumerable().ToList().Max(x => string.IsNullOrEmpty(Convert.ToString(x.Field<string>("msc_desc"))) ? 20 : x.Field<string>("msc_desc").Length);
            _rows = _sub1.NewRow();
            _sub1.Rows.InsertAt(_rows, 0);
            lcmbSub1Category.DataSource = _sub1;
            lcmbSub1Category.DisplayMember = "msc_desc";
            lcmbSub1Category.ValueMember = "msc_cd";
            lcmbSub1Category.Text = string.Empty;
            lcmbSub1Category.DropDownWidth = Convert.ToInt32(_maxlength * 5.5);
        }
        private void LoadSub2Category()
        {
            DataTable _sub2 = CHNLSVC.General.GetLocationCategory3();
            _sub2.AsEnumerable().ToList().ForEach(x => x.SetField("lpct_desc", FormulateDisplayText(x.Field<string>("lpct_desc"))));
            int _maxlength = 18;
            if (_sub2 != null)
                if (_sub2.Rows.Count > 0)
                    _maxlength = _sub2.AsEnumerable().ToList().Max(x => string.IsNullOrEmpty(Convert.ToString(x.Field<string>("lpct_desc"))) ? 20 : x.Field<string>("lpct_desc").Length);
            _rows = _sub2.NewRow();
            _sub2.Rows.InsertAt(_rows, 0);
            lcmbSub2Category.DataSource = _sub2;
            lcmbSub2Category.DisplayMember = "lpct_desc";
            lcmbSub2Category.ValueMember = "lpct_cate_cd";
            lcmbSub2Category.Text = string.Empty;
            lcmbSub2Category.DropDownWidth = Convert.ToInt32(_maxlength * 5.5);
        }
        private void LoadCountry()
        {
            DataTable _country = CHNLSVC.General.GetCountry();
            _country.AsEnumerable().ToList().ForEach(x => x.SetField("mcu_desc", FormulateDisplayText(x.Field<string>("mcu_desc"))));
            int _maxlength = 18;
            if (_country != null)
                if (_country.Rows.Count > 0)
                    _maxlength = _country.AsEnumerable().ToList().Max(x => string.IsNullOrEmpty(Convert.ToString(x.Field<string>("mcu_desc"))) ? 20 : x.Field<string>("mcu_desc").Length);
            _rows = _country.NewRow();
            _country.Rows.InsertAt(_rows, 0);
            lcmbCountry.DataSource = _country;
            lcmbCountry.DisplayMember = "mcu_desc";
            lcmbCountry.ValueMember = "mcu_cd";
            lcmbCountry.Text = string.Empty;
            lcmbCountry.DropDownWidth = Convert.ToInt32(_maxlength * 5.5);
        }
        private void LoadProvince()
        {
            DataTable _province = CHNLSVC.General.GetProvince();
            _province.AsEnumerable().ToList().ForEach(x => x.SetField("mpro_desc", FormulateDisplayText(x.Field<string>("mpro_desc"))));
            int _maxlength = 18;
            if (_province != null)
                if (_province.Rows.Count > 0)
                    _maxlength = _province.AsEnumerable().ToList().Max(x => string.IsNullOrEmpty(Convert.ToString(x.Field<string>("mpro_desc"))) ? 20 : x.Field<string>("mpro_desc").Length);
            _rows = _province.NewRow();
            _province.Rows.InsertAt(_rows, 0);
            lcmbProvince.DataSource = _province;
            lcmbProvince.DisplayMember = "mpro_desc";
            lcmbProvince.ValueMember = "mpro_cd";
            lcmbProvince.Text = string.Empty;
            lcmbProvince.DropDownWidth = Convert.ToInt32(_maxlength * 5.5);
        }
        private void LoadDistrict(string _province)
        {
            DataTable _district = CHNLSVC.General.GetDistrict(_province);
            _district.AsEnumerable().ToList().ForEach(x => x.SetField("mds_district", FormulateDisplayText(x.Field<string>("mds_district"))));
            int _maxlength = 18;
            if (_district != null)
                if (_district.Rows.Count > 0)
                    _maxlength = _district.AsEnumerable().ToList().Max(x => string.IsNullOrEmpty(Convert.ToString(x.Field<string>("mds_district"))) ? 20 : x.Field<string>("mds_district").Length);
            _rows = _district.NewRow();
            _district.Rows.InsertAt(_rows, 0);
            lcmbDistrict.DataSource = _district;
            lcmbDistrict.DisplayMember = "mds_district";
            lcmbDistrict.ValueMember = "mds_dist_cd";
            lcmbDistrict.Text = string.Empty;
            lcmbDistrict.DropDownWidth = Convert.ToInt32(_maxlength * 5.5);
        }
        private void LoadTown(string _province, string _district)
        {
            DataTable _town = CHNLSVC.General.GetTown(_province, _district);
            _town.AsEnumerable().ToList().ForEach(x => x.SetField("mt_desc", FormulateDisplayText(x.Field<string>("mt_desc"))));
            int _maxlength = 18;
            if (_town != null)
                if (_town.Rows.Count > 0)
                    _maxlength = _town.AsEnumerable().ToList().Max(x => string.IsNullOrEmpty(Convert.ToString(x.Field<string>("mt_desc"))) ? 20 : x.Field<string>("mt_desc").Length);
            _rows = _town.NewRow();
            _town.Rows.InsertAt(_rows, 0);
            lcmbTown.DataSource = _town;
            lcmbTown.DisplayMember = "mt_desc";
            lcmbTown.ValueMember = "mt_cd";
            lcmbTown.Text = string.Empty;
            lcmbTown.DropDownWidth = Convert.ToInt32(_maxlength * 5.5);
        }


        private void LoadEmployeeType()
        {
            DataTable _empcategory = CHNLSVC.General.GetEmployeeCategory();
            _empcategory.AsEnumerable().ToList().ForEach(x => x.SetField("mec_desc", FormulateDisplayText(x.Field<string>("mec_desc"))));
            int _maxlength = 18;
            if (_empcategory != null)
                if (_empcategory.Rows.Count > 0)
                    _maxlength = _empcategory.AsEnumerable().ToList().Max(x => string.IsNullOrEmpty(Convert.ToString(x.Field<string>("mec_desc"))) ? 20 : x.Field<string>("mec_desc").Length);
            _rows = _empcategory.NewRow();
            _empcategory.Rows.InsertAt(_rows, 0);
            lcmbEmployeeType.DataSource = _empcategory;
            lcmbEmployeeType.DisplayMember = "mec_desc";
            lcmbEmployeeType.ValueMember = "mec_cat";
            lcmbEmployeeType.Text = string.Empty;
            lcmbEmployeeType.DropDownWidth = Convert.ToInt32(_maxlength * 5.5);
        }
        private void LoadEmployeeStatus()
        {
            lcmbEmployeeStatus.DataSource = ActiveInactiveStatus;
        }
        #endregion
        #region Form Initialization - Profit Center
        private void LoadCenterType()
        {
            pcmbCenterType.DataSource = CenterType;
            pcmbCenterType.Text = string.Empty;
        }
        private void LoadPriceBook(string _company)
        {
            DataTable _book = CHNLSVC.Sales.GetPriceBookTable(_company, string.Empty);
            _rows = _book.NewRow();
            _book.Rows.InsertAt(_rows, 0);
            pcmbSlPriceBook.DataSource = _book;
            pcmbSlPriceBook.DisplayMember = "sapb_pb";
            pcmbSlPriceBook.Text = string.Empty;
        }
        private void LoadPriceLevel(string _company, string _book)
        {
            DataTable _level = CHNLSVC.Sales.GetPriceLevelTable(_company, _book, string.Empty);
            if (_level.Rows.Count > 0)
            {
                var level = _level.AsEnumerable().ToList().Select(X => X.Field<string>("SAPL_PB_LVL_CD")).Distinct().ToList();
                level.Insert(0, string.Empty);
                BindingSource _sourceLevel = new BindingSource();
                _sourceLevel.DataSource = level;

                pcmbSlPriceLevel.DataSource = _sourceLevel.DataSource;
                pcmbSlPriceLevel.DisplayMember = "SAPL_PB_LVL_CD";
                pcmbSlPriceLevel.Text = string.Empty;
            }

        }
        private void LoadBankCurrency()
        {
            List<MasterCurrency> _currency = CHNLSVC.General.GetAllCurrency(string.Empty);
            if (_currency == null || _currency.Count <= 0) { pcmbDefExchCode.DataSource = _currency; return; }
            MasterCurrency _one = new MasterCurrency() { Mcr_cd = string.Empty, Mcr_desc = string.Empty };
            _currency.Insert(0, _one);
            BindingSource _source = new BindingSource();
            _currency.OrderBy(X => X.Mcr_cd);
            _source.DataSource = _currency;
            pcmbDefExchCode.DataSource = _source.DataSource;
            pcmbDefExchCode.DisplayMember = "Mcr_cd";
            pcmbDefExchCode.Text = string.Empty;

        }
        private void LoadOutletDepartment(string _company)
        {
            DataTable _dept = CHNLSVC.General.GetOutletDepartment(_company);
            _rows = _dept.NewRow();
            _dept.Rows.InsertAt(_rows, 0);
            pcmbDefDept.DataSource = _dept;
            pcmbDefDept.DisplayMember = "msod_cd";
            pcmbDefDept.Text = string.Empty;
        }
        private void LoadDefaultStatus()
        {

        }
        #endregion
        #region Tab Change Event
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Name == tabPgLocation.Name)
                ChangesWhenCompanyChange(lcmbCompany.Text.Trim());
            if (tabControl.SelectedTab.Name == tabPgProfitCenter.Name)
                ChangesWhenCompanyChange(pcmbCompany.Text.Trim());
        }
        #endregion
        #region Common Function
        private void ChangesWhenCompanyChange(string _company)
        {
            //If Both Active
            LoadOperationAdminTeam(_company);


            //If Location Active
            if (tabControl.SelectedTab.Name == tabPgLocation.Name)
            {
                LoadGrade(_company, lcmbGrade.Text.Trim());
                LoadSub1Category(_company);
                LoadEmployeeType();
                LoadEmployeeStatus();
            }


            //If Profit Center Active
            if (tabControl.SelectedTab.Name == tabPgProfitCenter.Name)
            {
                if (pchkAllowMultiDept.Checked) LoadOutletDepartment(_company); else LoadOutletDepartment(string.Empty);
                LoadPriceBook(_company);
            }
        }
        private void SetLocationStatus()
        {
            if (lchkStatus.Checked)
                llblStatus.Text = "Active";
            else
                llblStatus.Text = "Inactive";
        }
        private void SetLocationSuspendStatus()
        {
            if (lchkSuspend.Checked)
                llblSuspend.Text = "Suspended";
            else
                llblSuspend.Text = "Keep Up";
        }
        private void SetProfitCenterStatus()
        {
            if (pchkStatus.Checked)
                plblStatus.Text = "Active";
            else
                plblStatus.Text = "Inactive";
        }
        #endregion
        #region Check Box Event
        private void lchkStatus_CheckedChanged(object sender, EventArgs e)
        {
            SetLocationStatus();
        }
        private void lchkSuspend_CheckedChanged(object sender, EventArgs e)
        {
            SetLocationSuspendStatus();
        }
        private void lchkMaintainSubLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (lchkMaintainSubLocation.Checked)
            {
                ltxtMainLocation.ReadOnly = false;
                ltxtMainLocation.Focus();
            }
            else
            {
                ltxtMainLocation.ReadOnly = true;
                ltxtMainLocation.Clear();
            }

        }
        private void lchkForwardSale_CheckedChanged(object sender, EventArgs e)
        {
            if (lchkForwardSale.Checked)
            {
                ltxtMaxLimit.ReadOnly = false;
                ltxtMaxLimit.Focus();
            }
            else
            {
                ltxtMaxLimit.ReadOnly = true;
                ltxtMaxLimit.Clear();
            }
        }
        private void lchkInsuredCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (lchkInsuredCompany.Checked)
            {
                ltxtInsuredValue.ReadOnly = false;
                ltxtInsuredValue.Focus();
            }
            else
            {
                ltxtInsuredValue.ReadOnly = true;
                ltxtInsuredValue.Clear();
            }
        }
        private void lchkBankGuarantyCompulsory_CheckedChanged(object sender, EventArgs e)
        {
            if (lchkBankGuarantyCompulsory.Checked)
            {
                ltxtGuarantValue.ReadOnly = false;
                ltxtGuarantValue.Focus();
            }
            else
            {
                ltxtGuarantValue.ReadOnly = true;
                ltxtGuarantValue.Clear();
            }
        }
        private void lchkCustomerCodeforManager_CheckedChanged(object sender, EventArgs e)
        {
            if (lchkCustomerCodeforManager.Checked)
            {
                ltxtCustomerCodeforManager.ReadOnly = false;
                ltxtCustomerCodeforManager.Focus();
            }
            else
            {
                ltxtCustomerCodeforManager.ReadOnly = true;
                ltxtCustomerCodeforManager.Clear();
            }
        }
        private void pchkStatus_CheckedChanged(object sender, EventArgs e)
        {
            SetProfitCenterStatus();
        }
        private void pchkAllowMultiDept_CheckedChanged(object sender, EventArgs e)
        {
            if (pchkAllowMultiDept.Checked)
            {
                if (string.IsNullOrEmpty(Convert.ToString(pcmbCompany.Text))) { pcmbDefDept.DataSource = new DataTable(); return; }
                LoadOutletDepartment(pcmbCompany.Text.Trim());
            }
            else
            {
                pcmbDefDept.DataSource = new DataTable();
            }
        }
        #endregion
        #region Combo Box Events
        private void lcmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lcmbCompany.Text)) { llblCompanyName.Text = string.Empty; return; }
            var _name = _company.Where(x => x.SEC_COM_CD == lcmbCompany.Text).Select(x => x.MasterComp.Mc_desc).ToList();
            if (_name == null || _name.Count <= 0) { llblCompanyName.Text = string.Empty; return; }
            llblCompanyName.Text = FormulateDisplayText(Convert.ToString(_name[0]));
            plblCompanyName.Text = FormulateDisplayText(Convert.ToString(_name[0]));
            ChangesWhenCompanyChange(lcmbCompany.Text);
        }
        private void lcmbLocationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lcmbLocationType.Text)) return;
            LoadGrade(lcmbCompany.Text.Trim(), lcmbLocationType.Text.Trim().ToUpper());
        }
        private void lcmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lcmbProvince.Text)) { LoadDistrict(string.Empty); LoadTown(string.Empty, string.Empty); return; }
            LoadDistrict(Convert.ToString(lcmbProvince.SelectedValue));
            LoadTown(Convert.ToString(lcmbProvince.SelectedValue), string.Empty);
        }
        private void lcmbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lcmbDistrict.Text)) { LoadTown(string.Empty, string.Empty); return; }
            LoadTown(Convert.ToString(lcmbProvince.SelectedValue), Convert.ToString(lcmbDistrict.SelectedValue));
        }
        private void pcmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pcmbCompany.Text)) { plblCompanyName.Text = string.Empty; return; }
            var _name = _company.Where(x => x.SEC_COM_CD == pcmbCompany.Text).Select(x => x.MasterComp.Mc_desc).ToList();
            if (_name == null || _name.Count <= 0) { plblCompanyName.Text = string.Empty; return; }
            plblCompanyName.Text = FormulateDisplayText(Convert.ToString(_name[0]));
            llblCompanyName.Text = FormulateDisplayText(Convert.ToString(_name[0]));
            ChangesWhenCompanyChange(pcmbCompany.Text);
        }
        private void pcmbSlPriceBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pcmbSlPriceBook.Text)) { LoadPriceLevel(pcmbCompany.Text.Trim(), string.Empty); return; }
            LoadPriceLevel(pcmbCompany.Text.Trim(), pcmbSlPriceBook.Text.Trim());
        }
        #endregion

    }
}
