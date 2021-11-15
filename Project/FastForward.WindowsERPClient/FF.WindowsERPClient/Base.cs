using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.Reports.Sales;
using FF.WindowsERPClient.Reports.Inventory;
using System.Globalization;
using System.ComponentModel;

namespace FF.WindowsERPClient
{
    public class Base : Form
    {
        public static readonly ChannelOperator channelService = new ChannelOperator();

        #region Public methods

        public ChannelOperator CHNLSVC
        {
            get
            {
                //if (!string.IsNullOrEmpty(BaseCls.GlbUserSessionID))
                //{
                //    if (BaseCls.GlbUserID != "ADMIN")
                //    {
                //        ChannelOperator channelServiceTemp = new ChannelOperator();
                //        if (channelServiceTemp.Security.IsSessionExpired(BaseCls.GlbUserSessionID, BaseCls.GlbUserID, BaseCls.GlbUserComCode) == true)
                //        {
                //            MessageBox.Show("Current Session is expired or has been closed by administrator!", "Fast Forward - SCM-II", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //            BaseCls.GlbIsExit = true;
                //            Application.Exit();
                //            GC.Collect();
                //        }
                //        channelServiceTemp.CloseAllChannels();
                //    }
                //}
                return channelService;
            }
        }

        //Add by Chamal 28-Aug-2013
        public bool CheckLocation(string _com, string _loc)
        {
            MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(_com, _loc);
            if (_mstLoc != null)
            {
                BaseCls.GlbIsManChkLoc = _mstLoc.Ml_is_chk_man_doc;
                _mstLoc = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Add by Chamal 28-Aug-2013
        public bool CheckProfitCenter(string _com, string _pc)
        {
            MasterProfitCenter _mstPc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            if (_mstPc != null)
            {
                BaseCls.GlbIsManChkPC = _mstPc.Mpc_is_chk_man_doc;
                _mstPc = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadProfitCenterDetail()
        {
            CacheLayer.Clear(CacheLayer.Key.ProfitCenter.ToString());
            CacheLayer.Clear(CacheLayer.Key.PriceDefinition.ToString());
            CacheLayer.Clear(CacheLayer.Key.ChannelDefinition.ToString());
            CacheLayer.Clear(CacheLayer.Key.IsSaleValueRoundUp.ToString());
            CacheLayer.Clear(CacheLayer.Key.CompanyInfor.ToString());

            //BaseCls.GlbMasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            //BaseCls.GlbPriceDefinitionRef = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(BaseCls.GlbUserComCode, string.Empty, string.Empty, string.Empty, BaseCls.GlbUserDefProf);
            MasterProfitCenter _ctn = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            CacheLayer.Add(_ctn, CacheLayer.Key.ProfitCenter.ToString());
            CacheLayer.Add(CHNLSVC.Inventory.GetChannelDetail(BaseCls.GlbUserComCode, _ctn.Mpc_chnl), CacheLayer.Key.ChannelDefinition.ToString());
            CacheLayer.Add(CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(BaseCls.GlbUserComCode, string.Empty, string.Empty, string.Empty, BaseCls.GlbUserDefProf), CacheLayer.Key.PriceDefinition.ToString());
            CacheLayer.Add(CHNLSVC.General.IsSaleFigureRoundUp(BaseCls.GlbUserComCode), CacheLayer.Key.IsSaleValueRoundUp.ToString());

            CacheLayer.Add(CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode), CacheLayer.Key.CompanyInfor.ToString());

            //BaseCls.GlbMasterProfitCenter = null;
            //BaseCls.GlbPriceDefinitionRef = null;      

            BaseCls.GlbDefChannel = string.Empty;
            BaseCls.GlbDefSubChannel = string.Empty;
            BaseCls.GlbDefArea = string.Empty;
            BaseCls.GlbDefRegion = string.Empty;
            BaseCls.GlbDefZone = string.Empty;

            DataTable _pcHierarchy = CHNLSVC.General.Get_PC_Hierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            if (_pcHierarchy.Rows.Count > 0)
            {
                foreach (DataRow r in _pcHierarchy.Rows)
                {
                    if (System.DBNull.Value != r["CHNL"]) BaseCls.GlbDefChannel = (string)r["CHNL"];
                    if (System.DBNull.Value != r["SCHNL"]) BaseCls.GlbDefSubChannel = (string)r["SCHNL"];
                    if (System.DBNull.Value != r["AREA"]) BaseCls.GlbDefArea = (string)r["AREA"];
                    if (System.DBNull.Value != r["REG"]) BaseCls.GlbDefRegion = (string)r["REG"];
                    if (System.DBNull.Value != r["ZONE_CD"]) BaseCls.GlbDefZone = (string)r["ZONE_CD"];
                }
            }
        }

        public void LoadLocationDetail()
        {
            BaseCls.GlbDefaultBin = CHNLSVC.Inventory.Get_default_binCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            CacheLayer.Clear(CacheLayer.Key.CompanyItemStatus.ToString());
            CacheLayer.Add(CHNLSVC.Inventory.GetAllCompanyStatus(BaseCls.GlbUserComCode), CacheLayer.Key.CompanyItemStatus.ToString());

            CacheLayer.Clear(CacheLayer.Key.ChannelParameter.ToString());

            MasterLocation oLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (oLocation.Ml_loc_tp.ToUpper() == "SERC".ToString())
            {
                Service_Chanal_parameter oChnlPara = CHNLSVC.General.GetChannelParamers(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (oChnlPara != null)
                {
                    CacheLayer.Add(oChnlPara, CacheLayer.Key.ChannelParameter.ToString());
                }
            }
        }

        //Added by Prabhath on 11/06/2013
        public void LoadCompanyDetail()
        {
            BaseCls.GlbIsSaleFigureRoundup = CHNLSVC.General.IsSaleFigureRoundUp(BaseCls.GlbUserComCode);
        }

        #endregion

        //Add by Chamal 22/03/2013
        private string _glbModuleName;
        public string GlbModuleName
        {
            get { return _glbModuleName; }
            set { _glbModuleName = value; }
        }

        public string GlbSelectData;
        //public string GlbUserName = string.Empty;
        //public string GlbUserDefLoca = string.Empty;
        //public string GlbUserDefProf = string.Empty;
        //public string GlbUserComCode = string.Empty;
        public string GlbUserSessionID = string.Empty;
        public string GlbSerial = string.Empty;
        public string GlbWarranty = string.Empty;
        public string GlbReportName = string.Empty;
        public string GlbReportDoc = string.Empty;
        public string GlbReportProfit = string.Empty;

        public string GlbDefaultBin = string.Empty;

        public string GlbReportHeading_1 = string.Empty;
        public string GlbReportUser = string.Empty;
        public string GlbPeriod = string.Empty;
        public string GlbDocType = string.Empty;
        public string GlbDocSubType = string.Empty;
        public string GlbProfitCenter = string.Empty;
        public string GlbReportPath = string.Empty;
        public string GlbSelectionFormula = string.Empty;
        public string GlbReportMapPath = string.Empty;


        /// <summary>
        /// Store Profit Center Detail
        /// Added By Prabhath on 31/12/2012
        /// </summary>
        public static MasterProfitCenter GlbMasterProfitCenter = null;
        /// <summary>
        /// Store Price Definition Detail
        /// Added By Prabhath on 31/12/2012
        /// </summary>
        public static List<PriceDefinitionRef> GlbPriceDefinitionRef = null;
        /// <summary>
        /// Store Default Bin
        /// Added By Prabhath on 31/12/2012
        /// </summary>
        //public static string GlbDefaultBin = string.Empty;



        //private string _glbUserComCode;
        //private string _glbUserDefProf;
        //private string _glbUserDefLoca;
        private string _glbUserName;


        public string GlbUserName1
        {
            get { return _glbUserName; }
            set { _glbUserName = value; }
        }

        //public string GlbUserDefLoca
        //{
        //    get { return _glbUserDefLoca; }
        //    set { _glbUserDefLoca = value; }
        //}

        //public string GlbUserDefProf
        //{
        //    get { return _glbUserDefProf; }
        //    set { _glbUserDefProf = value; }
        //}

        //public string GlbUserComCode
        //{
        //    get { return _glbUserComCode; }
        //    set { _glbUserComCode = value; }
        //}


        private bool glbReqIsApprovalNeed;
        public bool GlbReqIsApprovalNeed
        {
            get { return glbReqIsApprovalNeed; }
            set { glbReqIsApprovalNeed = value; }
        }

        private int glbReqRequestLevel;
        public int GlbReqRequestLevel
        {
            get { return glbReqRequestLevel; }
            set { glbReqRequestLevel = value; }
        }

        private int glbReqRequestApprovalLevel;
        public int GlbReqRequestApprovalLevel
        {
            get { return glbReqRequestApprovalLevel; }
            set { glbReqRequestApprovalLevel = value; }
        }

        private bool glbReqIsApprovalUser;
        public bool GlbReqIsApprovalUser
        {
            get { return glbReqIsApprovalUser; }
            set { glbReqIsApprovalUser = value; }
        }

        private bool glbReqIsFinalApprovalUser;
        public bool GlbReqIsFinalApprovalUser
        {
            get { return glbReqIsFinalApprovalUser; }
            set { glbReqIsFinalApprovalUser = value; }
        }

        private bool glbReqIsRequestGenerateUser;
        public bool GlbReqIsRequestGenerateUser
        {
            get { return glbReqIsRequestGenerateUser; }
            set { glbReqIsRequestGenerateUser = value; }
        }

        private int glbReqUserPermissionLevel;

        public int GlbReqUserPermissionLevel
        {
            get { return glbReqUserPermissionLevel; }
            set { glbReqUserPermissionLevel = value; }
        }


        public enum HirePurchasModuleApprovalCode
        {
            ARQT001,//ACCOUNT RESCHEDULE
            ARQT002,//MANAGER ISSUE REVERSAL
            ARQT004,//RETURN CHEQUE RECEIPT REVERSAL
            ARQT010,//Cash Conversion
            ARQT009,//HP ECD
            ARQT014,//CS Sale Reversal
            ARQT013,//HP Sale Reversal
            ARQT008, //HP Exchange
            ARQT015,//OTHER RECEIPT REVERSAL
            ARQT006, // Revert Release
            ARQT007,  //ADVAN Receipt Reverse
            ARQT011, //MANUAL DOC TRANSFER
            ARQT020,//VEHICLE REGISTRATION RECEIPT REFUND
            ARQT021,//VEHICLE INSURANCE RECEIPT REFUND
            ARQT019, //CONVERTED INVOICE VEH. INSURANCE REFUND
            ARQT018, //CONVERTED INVOICE REGISTRATION REFUND
            ARQT017,//REVESED INVOICE VEH. INSURANCE REFUND
            ARQT016,//REVESED INVOICE REGISTRATION REFUND

            HSRCREV,//hp receipt reverse - return cheque -ARQT004
            HSMIREV,//hp receipt reverse - manager issue -ARQT002
            HSRCTRV, //hp receipt reverse - other receipt -ARQT015

            VHREGRF, //VEHICLE REGISTRATION RECEIPT REFUND
            VHINSRF,//VEHICLE INSURANCE RECEIPT REFUND
            CCINSRF, //CONVERTED INVOICE VEH. INSURANCE REFUND -ARQT019
            CCREGRF,//CONVERTED INVOICE REGISTRATION REFUND -ARQT018
            CSINSRF,//REVESED INVOICE VEH. INSURANCE REFUND -ARQT017
            CSREGRF, //REVESED INVOICE REGISTRATION REFUND -ARQT016
            HSACRSC, //ACCOUNT RESCHEDULE
            CSINREV, //CASH / CREDIT SALES REVERSAL - ARQT014
            HSACREV, //HIRE SALES REVERSAL - ARQT013
            HSSPECC,//CASH CONVERTION
            INVMAN, //MANUAL DOC TRANSFER -ARQT011
            HSSPDIS, // HP ECD -ARQT009
            HSSEXCH, // EXCHANGE ARQT008
            HSRVTRL,//Revert Release
            ARQT034 //Credit Customer Creation
        }

        public enum SalesPriorityHierarchyCategory
        {
            LOC_PRIT_HIERARCHY,//Location 
            PC_PRIT_HIERARCHY,//Profit Center
            ITM_PRIT_HIERARCHY//Item
        }
        public enum SalesPriorityHierarchyType
        {
            PC,
            LOCATION,
            ITM
        }

        //public void RequestApprovalCycleDefinition(bool _isLocation, HirePurchasModuleApprovalCode _modulecode, DateTime _date, string _destinationcompany, string _destinationlocation, CommonUIDefiniton.SalesPriorityHierarchyCategory _category, CommonUIDefiniton.SalesPriorityHierarchyType _type)
        public void RequestApprovalCycleDefinition(bool _isLocation, HirePurchasModuleApprovalCode _modulecode, DateTime _date, string _destinationcompany, string _destinationlocation, SalesPriorityHierarchyCategory _category, SalesPriorityHierarchyType _type)
        {
            RequestApproveCycleDefinition _obj = null;
            UserRequestApprovePermission _obj_ = null;
            if (_isLocation)
                _obj = CHNLSVC.Security.GetApproveCycleDefinitionDataforLocation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToString(_modulecode), _date, _destinationcompany, _destinationlocation, Convert.ToString(_category), Convert.ToString(_type));
            else
                _obj = CHNLSVC.Security.GetApproveCycleDefinitionDataforProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToString(_modulecode), _date, _destinationcompany, _destinationlocation, Convert.ToString(_category), Convert.ToString(_type));


            if (_isLocation)
                _obj_ = CHNLSVC.Security.GetUserRequestApprovalPermissionDataForLocation(BaseCls.GlbUserID, Convert.ToString(_modulecode), BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToString(_category), Convert.ToString(_type));
            else
                _obj_ = CHNLSVC.Security.GetUserRequestApprovalPermissionDataForProfitCenter(BaseCls.GlbUserID, Convert.ToString(_modulecode), BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToString(_category), Convert.ToString(_type));
            if (_obj == null) BaseCls.GlbReqIsApprovalNeed = true;
            else if (_obj != null) BaseCls.GlbReqIsApprovalNeed = _obj.IsApprovalNeeded;
            else BaseCls.GlbReqIsApprovalNeed = false;

            if (_obj_ != null)
            {
                BaseCls.GlbReqRequestLevel = _obj_.RequestLevel;
                BaseCls.GlbReqRequestApprovalLevel = _obj_.RequestApproveLevel;
                BaseCls.GlbReqIsApprovalUser = _obj_.IsApprovalUser;
                BaseCls.GlbReqIsFinalApprovalUser = _obj_.IsFinalApprovalUser;
                BaseCls.GlbReqIsRequestGenerateUser = _obj_.IsRequestGenerateUser;
                BaseCls.GlbReqUserPermissionLevel = _obj_.UserPermissionLevel;

                GlbReqRequestLevel = _obj_.RequestLevel;
                GlbReqRequestApprovalLevel = _obj_.RequestApproveLevel;
                GlbReqIsApprovalUser = _obj_.IsApprovalUser;
                GlbReqIsFinalApprovalUser = _obj_.IsFinalApprovalUser;
                GlbReqIsRequestGenerateUser = _obj_.IsRequestGenerateUser;
                GlbReqUserPermissionLevel = _obj_.UserPermissionLevel;
            }
            else
            {
                BaseCls.GlbReqRequestLevel = -1;
                BaseCls.GlbReqRequestApprovalLevel = -1;
                BaseCls.GlbReqIsApprovalUser = false;
                BaseCls.GlbReqIsFinalApprovalUser = false;
                BaseCls.GlbReqIsRequestGenerateUser = false;
                BaseCls.GlbReqUserPermissionLevel = -1;

                GlbReqRequestLevel = -1;
                GlbReqRequestApprovalLevel = -1;
                GlbReqIsApprovalUser = false;
                GlbReqIsFinalApprovalUser = false;
                GlbReqIsRequestGenerateUser = false;
                GlbReqUserPermissionLevel = -1;
            }
        }
        public static string FormatToCurrency(string _value)
        {
            string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToCurrency"].ToString();
            return String.Format(_format, Convert.ToDecimal(_value));
        }
        public string FormatToQty(string _value)
        {
            string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToQty"].ToString();
            return String.Format(_format, Convert.ToDecimal(_value));
        }


        #region Regular Expressions


        public static bool IsValidEmail(string email)
        {
         //   string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            //kapila 23/4/2016
            string pattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool IsValidNIC(string nic)
        {
            string pattern="";
            if (nic.Length==10)     //kapila 14/1/2016
                 pattern = @"^[0-9]{9}[V,X]{1}$";
            else if (nic.Length==12)
                pattern = @"^[0-9]{12}$";
            else
                return false;

            System.Text.RegularExpressions.Match match = Regex.Match(nic.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool IsValidMobileOrLandNo(string mobile)
        {
            string pattern = @"^[0-9]{10}$";

            System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Base
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 628);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1016, 666);
            this.Name = "Base";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.Base_Load);
            this.ResumeLayout(false);

        }

        private void Base_Load(object sender, EventArgs e)
        {

        }

        public static Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }

        //Code by Chamal De Silva 10-Aug-2012
        protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, DateTimePicker _dtpcontrol, Label _lblcontrol, string _date, out bool _allowCurrentDate)
        {
            BackDates _bdt = new BackDates();
            _dtpcontrol.Enabled = false;
            _allowCurrentDate = false;

            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
            if (_isAllow == true)
            {
                _dtpcontrol.Enabled = true;
                //if (_bdt != null) _lblcontrol.Text = "This module is back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
                if (_bdt != null)
                {
                    _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
                    //_dtpcontrol.Value = _bdt.Gad_act_to_dt.Date; //TEMPORAY BLOCK - CHAMAL 06-05-2014 , BLOCK REMOVE BY CHAMAL  31-12-2014
                    //Again removeed because POSTING time its gone 31 st or 30 date
                    //again comented by kapila on 30/1/2017 (CRQ ID -3573)
                }
            }

            if (_bdt == null)
            {
                _allowCurrentDate = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentDate = true;
                }
            }

            CheckSessionIsExpired();
            return _isAllow;
        }

        //protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, DateTimePicker _dtpcontrol, Label _lblcontrol, string _date)
        //{
        //    BackDates _bdt = new BackDates();
        //    _dtpcontrol.Enabled = false;


        //    bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
        //    if (_isAllow == true)
        //    {
        //        _dtpcontrol.Enabled = true;
        //        //if (_bdt != null) _lblcontrol.Text = "This module is back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
        //        if (_bdt != null)
        //        {
        //            _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
        //           }
        //    }

        //    CheckSessionIsExpired();
        //    return _isAllow;
        //}
        //Re-arranged by Prabhath on 23-01-2013
        protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, DateTimePicker _dtpcontrol, ToolStripLabel _lblcontrol, string _date, out bool _allowCurrentDate)
        {
            BackDates _bdt = new BackDates();
            _dtpcontrol.Enabled = false;
            _allowCurrentDate = false;

            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
            if (_isAllow == true)
            {
                _dtpcontrol.Enabled = true;
                if (_bdt != null)
                {
                    _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. \r\n Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
                    //comment by darshana 05-08-2014
                     //_dtpcontrol.Value = _bdt.Gad_act_to_dt.Date;//BLOCK REMOVE BY CHAMAL  31-12-2014
                    //again comented by kapila on 30/1/2017 (CRQ ID -3573)
                }
            }

            if (_bdt == null)
            {
                _allowCurrentDate = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentDate = true;
                }
            }
            CheckSessionIsExpired();
            return _isAllow;
        }

        //protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, DateTimePicker _dtpcontrol, ToolStripLabel _lblcontrol, string _date)
        //{
        //    BackDates _bdt = new BackDates();
        //    _dtpcontrol.Enabled = false;

        //    bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
        //    if (_isAllow == true)
        //    {
        //        _dtpcontrol.Enabled = true;
        //        if (_bdt != null)
        //        {
        //            _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. \r\n Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
        //        }
        //    }
        //    CheckSessionIsExpired();
        //    return _isAllow;
        //}
        //Written By Prabhath on 21/01/2013
        public void IsDecimalAllow(bool _isDecimalAllow, object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, But not allow decimal
            if (_isDecimalAllow == false)
            {
                if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 || e.KeyChar == 46))
                {
                    e.Handled = true;
                    return;
                }
            }
            // allows 0-9, backspace, and decimal
            else if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        public string ExtractTitleByCustomerName(string _customerName)
        {
            string[] _splits = _customerName.Split('.');
            StringBuilder _actualTitle = new StringBuilder(string.Empty);
            if (_splits.Length > 1)
            {
                string _title = _splits[0];
                if (!string.IsNullOrEmpty(_title))
                {
                    _actualTitle.Append(_title.Substring(0, 1)); _actualTitle.Append(_title.Substring(1, _title.Length - 1).ToLower()); _actualTitle.Append(".");
                }
            }
            return _actualTitle.ToString();

        }

        //written by prabhath on 31/05/2012          
        public bool IsDate(string val, System.Globalization.DateTimeStyles DateTimeStyle)
        {
            DateTime result;
            return DateTime.TryParse(val, System.Globalization.CultureInfo.CurrentCulture, DateTimeStyle, out result);
        }

        //written by Prabhath on 9/02/2013        
        public bool IsReferancedDocDateAppropriate(List<ReptPickSerials> _reptPrickSerialList, DateTime _processDate)
        {
            bool _appropriate = true;
            if (_reptPrickSerialList != null)
            {
                var _isInAppropriate = _reptPrickSerialList.Where(x => x.Tus_doc_dt.Date > _processDate.Date).ToList();
                if (_isInAppropriate == null || _isInAppropriate.Count <= 0) _appropriate = true;
                else _appropriate = false;
                if (_appropriate == false)
                {
                    StringBuilder _documents = new StringBuilder();
                    foreach (ReptPickSerials _one in _isInAppropriate)
                    {
                        if (string.IsNullOrEmpty(_documents.ToString()))
                            _documents.Append(_one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                        else
                            _documents.Append(" and " + _one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                    }
                    MessageBox.Show("The Inward documents " + _documents.ToString() + " equal or grater than to a this Outward document " + _processDate.Date.ToShortDateString() + " date!", "Reference Document with Out Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            return _appropriate;
        }

        //Written By Prabhath on 19/02/2013 according to Chamal function
        public bool CheckServerDateTime()
        {
            //function provided by Chamal on 19/2/2013
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show("Your system date is not equal to server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MessageBox.Show("Your machine date conflict with the server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show("Your system time zone is not equal to server time zone! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MessageBox.Show("Your machine time zone conflict with the server time zone! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        //Written By Prabhath on 15/03/2013 - Formulate Table String For Display Purpose
        public string FormulateDisplayText(string _text)
        {
            if (string.IsNullOrEmpty(_text)) return string.Empty;
            StringBuilder _lastName = new StringBuilder();
            string[] split = _text.Trim().Split(' ');
            for (int i = 0; i < split.Length; i++)
            {
                if (string.IsNullOrEmpty(split[i])) continue;
                string _fCh = split[i].Substring(0, 1).ToUpper();
                string _remain = split[i].Substring(1, split[i].Length - 1).ToLower();
                _lastName.Append(_fCh + _remain);
                if (split.Length > 1)
                    _lastName.Append(" ");
            }
            return _lastName.ToString();
        }

        //Code by Chamal 01-Jul-2013
        public void CheckSessionIsExpired()
        {
            if (BaseCls.GlbUserID != "ADMIN")
            {
                string _expMsg = "Current Session is expired or has been closed by administrator!";
                if (CHNLSVC.Security.IsSessionExpired(BaseCls.GlbUserSessionID, BaseCls.GlbUserID, BaseCls.GlbUserComCode, out _expMsg) == true)
                {
                    MessageBox.Show(_expMsg, "Fast Forward - SCM-II", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    BaseCls.GlbIsExit = true;
                    Application.Exit();
                    GC.Collect();
                }
            }
        }

        //Copied by Prabhath from http://stackoverflow.com/questions/7075201/rounding-up-to-2-decimal-places-in-c-sharp on 13/08/2013
        public static decimal RoundUpForPlace(decimal input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * Convert.ToDecimal(multiplier)) / Convert.ToDecimal(multiplier);
        }

        public void SetLoginCacheLayer(LoginUser _loginUSER, string _winLogonname, string _winUser)
        {
            CacheLayer.Clear(CacheLayer.Key.ProfitCenter.ToString());
            CacheLayer.Clear(CacheLayer.Key.PriceDefinition.ToString());
            CacheLayer.Clear(CacheLayer.Key.ChannelDefinition.ToString());
            CacheLayer.Clear(CacheLayer.Key.IsSaleValueRoundUp.ToString());
            CacheLayer.Clear(CacheLayer.Key.CompanyItemStatus.ToString());
            CacheLayer.Clear(CacheLayer.Key.CompanyInfor.ToString());
            CacheLayer.Clear(CacheLayer.Key.ChannelParameter.ToString());


            if (_loginUSER.Mst_pc != null) CacheLayer.Add(_loginUSER.Mst_pc, CacheLayer.Key.ProfitCenter.ToString());
            if (_loginUSER.Chnl_defn != null) CacheLayer.Add(_loginUSER.Chnl_defn, CacheLayer.Key.ChannelDefinition.ToString());
            if (_loginUSER.Price_defn != null) CacheLayer.Add(_loginUSER.Price_defn, CacheLayer.Key.PriceDefinition.ToString());
            CacheLayer.Add(_loginUSER.Is_sale_roundup, CacheLayer.Key.IsSaleValueRoundUp.ToString());
            CacheLayer.Add(_loginUSER.Com_itm_status, CacheLayer.Key.CompanyItemStatus.ToString());
            CacheLayer.Add(_loginUSER.Mst_com, CacheLayer.Key.CompanyInfor.ToString());

            if (!string.IsNullOrEmpty(_loginUSER.User_def_loc))
            {
                MasterLocation oLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, _loginUSER.User_def_loc);
                if (oLocation.Ml_loc_tp.ToUpper() == "SERC".ToString())
                {
                    Service_Chanal_parameter oChnlPara = CHNLSVC.General.GetChannelParamers(BaseCls.GlbUserComCode, _loginUSER.User_def_loc);
                    if (oChnlPara != null)
                    {
                        _loginUSER.Chl_para = oChnlPara;
                        CacheLayer.Add(_loginUSER.Chl_para, CacheLayer.Key.ChannelParameter.ToString());
                    }
                }
            }

            //_loginUSER.Chl_para = CHNLSVC.General.GetChannelParamers(BaseCls.GlbUserComCode, _loginUSER.User_def_loc);
            //CacheLayer.Add(_loginUSER.Chl_para, CacheLayer.Key.ChannelParameter.ToString());


            BaseCls.GlbDefChannel = _loginUSER.Def_chnl;
            BaseCls.GlbDefSubChannel = _loginUSER.Def_schnl;
            BaseCls.GlbDefArea = _loginUSER.Def_area;
            BaseCls.GlbDefRegion = _loginUSER.Def_Region;
            BaseCls.GlbDefZone = _loginUSER.Def_zone;

            BaseCls.GlbDefaultBin = _loginUSER.User_def_bin;
            BaseCls.GlbIsSaleFigureRoundup = _loginUSER.Is_sale_roundup;

            BaseCls.GlbUserName = _loginUSER.User_name;
            BaseCls.GlbUserDeptID = _loginUSER.User_dept_id;
            BaseCls.GlbUserDeptName = _loginUSER.User_dept_name;
            BaseCls.GlbUserCategory = _loginUSER.User_category;
            BaseCls.GlbUserDefLoca = _loginUSER.User_def_loc;
            BaseCls.GlbUserDefProf = _loginUSER.User_def_pc;

            BaseCls.GlbIsManChkLoc = _loginUSER.Is_man_chk_loc;
            BaseCls.GlbIsManChkPC = _loginUSER.Is_man_chk_pc;

            _loginUSER.User_session_id = Convert.ToString(CHNLSVC.Security.SaveLoginSession(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserIP, BaseCls.GlbHostName, _winLogonname, _winUser));

            BaseCls.GlbUserSessionID = _loginUSER.User_session_id;
        }

        //Written by Prabhath on 11/10/2013
        public bool IsPendingOrApprovedRequestAvailable(string _company, string _location, string _type, string _document)
        {
            bool _isK = false;
            if (CHNLSVC.Inventory.IsPendingOrApprovedRequestAvailable(_company, _location, _type, _document))
            {
                //MessageBox.Show("There are several pending/approved request available for the document " + _document + ". You can not process this request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Cannot proceed ! \nReason : Found another request for this transaction " + _document + ".", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isK = true;
            }
            else
            { _isK = false; }
            return _isK;
        }

        //Written by Prabhath on 04/12/2013
        public bool IsUserProcessed(Int32 _seqno, string _document)
        {
            bool _isUserProcessed = false;
            bool _isProcess = CHNLSVC.Inventory.CheckCompanyMulti(BaseCls.GlbUserComCode);
            if (_isProcess == false) { _isUserProcessed = false; return _isUserProcessed; }

            string _error = string.Empty;
            DataTable _tbl = CHNLSVC.Inventory.GetProcessUser(_seqno, _document, BaseCls.GlbUserComCode);
            if (_tbl == null || _tbl.Rows.Count <= 0) return _isUserProcessed;
            string _user = _tbl.Rows[0].Field<string>("tuh_pro_user");
            if (string.IsNullOrEmpty(_user))
            {
                _isUserProcessed = false;
                try
                {
                    Int32 _effect = CHNLSVC.Inventory.UpdateProcessUser(BaseCls.GlbUserID, _seqno, _document, BaseCls.GlbUserComCode, out _error);
                    if (_effect == -1)
                    {
                        _isUserProcessed = true;
                        MessageBox.Show("Error generated while processing. " + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    _isUserProcessed = true;
                    _error = ex.Message;
                    MessageBox.Show("Error generated while processing. " + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (_user == BaseCls.GlbUserID) _isUserProcessed = false;
                else
                {
                    DataTable _us = CHNLSVC.Inventory.GetUserNameByUserID(_user);
                    string _name = string.Empty;
                    if (_us != null && _us.Rows.Count > 0)
                    {
                        _name = _us.Rows[0].Field<string>("SE_USR_NAME");
                    }
                    _isUserProcessed = true;
                    MessageBox.Show("This document is processing by the user " + _user + "-" + _name + ". Process terminated!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return _isUserProcessed;
        }

        //Code by Chamal 20-Jan-2013
        public static string ConvertTo_ProperCase(string text)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            return myTI.ToTitleCase(text.ToLower());
        }

        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
