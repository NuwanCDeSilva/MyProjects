using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Text.RegularExpressions;
using System.Data;
namespace FF.WindowsERPClient
{
    //Code add by Chamal De Silva 01-Jan-2013
    public static class BaseCls
    {
        //1:0:0:119 26-Nov-2013 with transferMode="Streamed"
        public static string GlbVersionNo = "1:0:0:196";
        //public static string GlbVersionNo = "1:0:0:199.1";
        public static string GlbSelectData = string.Empty;
        public static string GlbUserID = string.Empty;
        public static string GlbUserCategory = string.Empty;
        public static string GlbUserName = string.Empty;
        public static string GlbUserIP = string.Empty;
        public static string GlbHostName = string.Empty;
        public static string GlbUserDeptID = string.Empty;
        public static string GlbUserDeptName = string.Empty;
        public static string GlbUserDefLoca = string.Empty;
        public static string GlbUserDefProf = string.Empty;
        public static string GlbUserComCode = string.Empty;
        public static string GlbUserSessionID = string.Empty;
        public static string GlbSerial = string.Empty;
        public static string GlbWarranty = string.Empty;
        public static string GlbDefaultBin = string.Empty;
        public static string GlbDefChannel = string.Empty;//Add by Chamal 19-03-2013
        public static string GlbDefSubChannel = string.Empty;//Add by Chamal 19-03-2013
        public static string GlbDefArea = string.Empty;//Add by Chamal 19-03-2013
        public static string GlbDefRegion = string.Empty;//Add by Chamal 19-03-2013
        public static string GlbDefZone = string.Empty;//Add by Chamal 19-03-2013
        public static MasterProfitCenter GlbMasterProfitCenter = null;
        public static List<PriceDefinitionRef> GlbPriceDefinitionRef = null;
        public const string GlbCommonErrorMessage = "There is a connection problem with the server. Please try again!";
        public static bool GlbIsSaleFigureRoundup = false;//Added by Prabhath on 11/06/2013
        public static bool GlbIsExit = false; //Add by Chamal 26-06-2013 
        public static bool GlbIsManChkPC = true; //Add by Darshana 27-08-2013 
        public static bool GlbIsManChkLoc = true;  //Add by Darshana 27-08-2013 

        #region Report parameters

        public static string GlbReportName = string.Empty;
        public static string GlbReportDoc = string.Empty;
        public static string GlbReportProfit = string.Empty;
        public static string GlbReportChannel = string.Empty;
        public static string GlbReportComp = string.Empty;
        public static string GlbReportCompCode = string.Empty;
        public static string GlbReportCompAddr = string.Empty;
        public static string GlbReportScheme = string.Empty;
        public static string GlbReportGroupBy = string.Empty;
        public static string GlbStatus = string.Empty;
        public static string GlbReportDocType = string.Empty;
        public static string GlbReportDocSubType = string.Empty;
        public static string GlbReportExecCode = string.Empty;
        public static string GlbReportItemCode = string.Empty;
        public static string GlbReportItemStatus = string.Empty;
        public static string GlbReportBrand = string.Empty;
        public static string GlbReportModel = string.Empty;
        public static string GlbReportItemCat1 = string.Empty;
        public static string GlbReportItemCat2 = string.Empty;
        public static string GlbReportItemCat3 = string.Empty;
        public static string GlbReportItemCatType = string.Empty;
        public static string GlbReportHeading = string.Empty;
        public static string GlbReportType = string.Empty;
        public static Int32 GlbReportDirection = 0;
        public static string GlbReportCusId = string.Empty;
        public static string GlbReportPriceBook = string.Empty;
        public static string GlbReportPBLevel = string.Empty;
        public static string GlbReportSupplier = string.Empty;
        public static string GlbReportFilePath = string.Empty;
        public static string GlbReportPurchaseOrder = string.Empty;
        public static string GlbReportStrStatus = string.Empty;
        public static string GlbReportDocMismatch = string.Empty;
        public static string GlbReportJobNo = string.Empty;
        public static string GlbReportUser = string.Empty;
        public static string GlbReportRole = string.Empty;
        public static string GlbReportDepartment = string.Empty;
        public static string GlbReportAppBy = string.Empty;
        public static Decimal GlbReportDiscRate = 0;
        public static Int16 GlbReportDiscTp = 0;
        public static Int16 GlbReportIsDelivered = 0;
        public static Int16 GlbReportBook = 0;
        public static int GlbReportFromPage = 0;
        public static int GlbReportToPage = 0;
        public static string GlbReportExeType = string.Empty;
        public static string GlbReportDoc1 = string.Empty;
        public static string GlbReportDoc2 = string.Empty;
        public static string GlbReportPromotor = string.Empty;
        public static Int16 GlbReportAllowCommision = 0;
        public static string GlbReportTechncian = string.Empty;
        public static string GlbReportJobCat = string.Empty;
        public static string GlbReportItemType = string.Empty;
        public static string GlbReportJobStatus = string.Empty;
        public static string GlbReportWarrStatus = string.Empty;

        public static Int16 GlbReportStatus = 0;
        public static int GlbReportIsCostPrmission = 0;

        public static int GlbReportGroupProfit = 0;
        public static int GlbReportGroupDOLoc = 0;
        public static int GlbReportGroupDocType = 0;
        public static int GlbReportGroupCustomerCode = 0;
        public static int GlbReportGroupExecCode = 0;
        public static int GlbReportGroupItemCode = 0;
        public static int GlbReportGroupBrand = 0;
        public static int GlbReportGroupModel = 0;
        public static int GlbReportGroupItemCat1 = 0;
        public static int GlbReportGroupItemCat2 = 0;
        public static int GlbReportGroupItemCat3 = 0;
        public static int GlbReportGroupItemStatus = 0;
        public static int GlbReportGroupInvoiceNo = 0;
        public static int GlbReportGroupLastGroup = 0;
        public static int GlbReportGroupPromotor = 0;
        public static string GlbReportGroupLastGroupCat = "";

        public static int GlbReportOutsOnly = 0;

        public static Int16 GlbReportWithCost = 0;
        public static Int16 GlbReportWithSerial = 0;
        public static Int16 GlbReportforAllCompany = 0;
        public static Int16 GlbReportIsFast = 0;
        public static Int16 GlbReportWithStatus = 0;
        public static int GlbReportIsExport = 0;

        public static string GlbRecType = string.Empty;
        public static string GlbPrefix = string.Empty;
        public static string GlbPayType = string.Empty;
        public static string GlbReportTp = string.Empty;
        public static Int16 ShowComName = 0;
        public static string GlbReportPaperSize = string.Empty;

        public static string GlbRccType = string.Empty;
        public static string GlbRccAgent = string.Empty;
        public static string GlbRccColMethod = string.Empty;
        public static string GlbRccCloseTp = string.Empty;

        public static Double GlbReportCusAccBal = 0;
        public static Decimal GlbRemToBeBanked = 0;
        public static Decimal GlbRemToBeBankedFinal = 0;
        public static Decimal GlbCIH = 0;
        public static Decimal GlbCIHFinal = 0;
        public static Decimal GlbTotRemitance = 0;
        public static Decimal GlbTotRemitanceFinal = 0;
        public static Decimal GlbCommWithdrawn = 0;
        public static Decimal GlbCommWithdrawnFinal = 0;
        public static Decimal GlbwithClaims = 0;
        public static Decimal GlbwithSettle = 0;
        public static DateTime GlbReportFromDate = Convert.ToDateTime("01/01/0001 00:00:00");
        public static DateTime GlbReportToDate = Convert.ToDateTime("01/01/0001 00:00:00");
        public static DateTime GlbReportAsAtDate = Convert.ToDateTime("01/01/0001 00:00:00");
        public static string GlbReportCustomerCode = string.Empty;

        public static Decimal GlbReportPrvBal = 0;
        public static Decimal GlbReportCurBal = 0;

        public static Int32 GlbReportWeek = 0;
        public static Int32 GlbReportYear = 0;
        public static Int32 GlbReportMonth = 0;
        public static Decimal GlbReportOpBal = 0;
        public static Decimal GlbReportCloseBal = 0;
        public static Int32 GlbReportnoofDays = 0;
        public static bool GlbReportViewPC = false;
        public static DataTable GlbReportDataTable = null;
        public static Int16 GlbReportIsAsAt = 0;
        public static DateTime GlbReportStartOn = Convert.ToDateTime("01/01/0001 00:00:00");
        public static Int32 GlbReportParaLine1 = 0;
        public static Int32 GlbReportParaLine2 = 0;

        public static string GlbReportInsComp = string.Empty;
        public static string GlbReportParty = string.Empty;
        public static string GlbReportPartyCode = string.Empty;
        public static string GlbReportPolCode = string.Empty;
        public static string GlbReportWithRCC = string.Empty;

        #endregion


        #region Approval Cycle

        private static bool glbReqIsApprovalNeed;
        public static bool GlbReqIsApprovalNeed
        {
            get { return glbReqIsApprovalNeed; }
            set { glbReqIsApprovalNeed = value; }
        }

        private static int glbReqRequestLevel;
        public static int GlbReqRequestLevel
        {
            get { return glbReqRequestLevel; }
            set { glbReqRequestLevel = value; }
        }

        private static int glbReqRequestApprovalLevel;
        public static int GlbReqRequestApprovalLevel
        {
            get { return glbReqRequestApprovalLevel; }
            set { glbReqRequestApprovalLevel = value; }
        }

        private static bool glbReqIsApprovalUser;
        public static bool GlbReqIsApprovalUser
        {
            get { return glbReqIsApprovalUser; }
            set { glbReqIsApprovalUser = value; }
        }

        private static bool glbReqIsFinalApprovalUser;
        public static bool GlbReqIsFinalApprovalUser
        {
            get { return glbReqIsFinalApprovalUser; }
            set { glbReqIsFinalApprovalUser = value; }
        }

        private static bool glbReqIsRequestGenerateUser;
        public static bool GlbReqIsRequestGenerateUser
        {
            get { return glbReqIsRequestGenerateUser; }
            set { glbReqIsRequestGenerateUser = value; }
        }

        private static int glbReqUserPermissionLevel;

        public static int GlbReqUserPermissionLevel
        {
            get { return glbReqUserPermissionLevel; }
            set { glbReqUserPermissionLevel = value; }
        }
        public enum HirePurchasModuleApprovalCode
        {
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
            ARQT011 //MANUAL DOC TRANSFER
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
        public static void RequestApprovalCycleDefinition(bool _isLocation, HirePurchasModuleApprovalCode _modulecode, DateTime _date, string _destinationcompany, string _destinationlocation, SalesPriorityHierarchyCategory _category, SalesPriorityHierarchyType _type)
        {
            RequestApproveCycleDefinition _obj = null;
            UserRequestApprovePermission _obj_ = null;
            if (_isLocation)
                //TODO: UNCOMMENT
                // _obj = CHNLSVC.Security.GetApproveCycleDefinitionDataforLocation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToString(_modulecode), _date, _destinationcompany, _destinationlocation, Convert.ToString(_category), Convert.ToString(_type));
                ;
            else
                //TODO: UNCOMMENT
                // _obj = CHNLSVC.Security.GetApproveCycleDefinitionDataforProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToString(_modulecode), _date, _destinationcompany, _destinationlocation, Convert.ToString(_category), Convert.ToString(_type));


                if (_isLocation)
                    //TODO: UNCOMMENT
                    // _obj_ = CHNLSVC.Security.GetUserRequestApprovalPermissionDataForLocation(BaseCls.GlbUserName, Convert.ToString(_modulecode), Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToString(_category), Convert.ToString(_type));
                    ;
                else
                    //TODO: UNCOMMENT
                    // _obj_ = CHNLSVC.Security.GetUserRequestApprovalPermissionDataForProfitCenter(BaseCls.GlbUserName, Convert.ToString(_modulecode), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToString(_category), Convert.ToString(_type));
                    if (_obj != null) GlbReqIsApprovalNeed = _obj.IsApprovalNeeded;
                    else GlbReqIsApprovalNeed = false;

            if (_obj_ != null)
            {
                GlbReqRequestLevel = _obj_.RequestLevel;
                GlbReqRequestApprovalLevel = _obj_.RequestApproveLevel;
                GlbReqIsApprovalUser = _obj_.IsApprovalUser;
                GlbReqIsFinalApprovalUser = _obj_.IsFinalApprovalUser;
                GlbReqIsRequestGenerateUser = _obj_.IsRequestGenerateUser;
                GlbReqUserPermissionLevel = _obj_.UserPermissionLevel;
            }
            else
            {
                GlbReqRequestLevel = -1;
                GlbReqRequestApprovalLevel = -1;
                GlbReqIsApprovalUser = false;
                GlbReqIsFinalApprovalUser = false;
                GlbReqIsRequestGenerateUser = false;
                GlbReqUserPermissionLevel = -1;
            }
        }



        #endregion

        public static string FormatToCurrency(string _value)
        {
            string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToCurrency"].ToString();
            return String.Format(_format, Convert.ToDecimal(_value));
        }
        public static string FormatToQty(string _value)
        {
            string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToQty"].ToString();
            return String.Format(_format, Convert.ToDecimal(_value));
        }
        #region Regular Expressions


        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool IsValidNIC(string nic)
        {
            string pattern = @"^[0-9]{9}[V]{1}$";

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
        public static Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }

        #endregion

    }
}
