using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using FastForward.SCMWeb.UserControls;
using FF.BusinessObjects;
using System.Text;
using System.Configuration;

namespace FastForward.SCMWeb
{
    public class BasePage : System.Web.UI.Page
    {
        private ChannelOperator channelService = new ChannelOperator();

        //public const string GlbVersionNo = ConfigurationManager.AppSettings["VersionNo"].ToString();
        public const string GlbDummySerial1 = "_";

        //public uc_MsgInfo MasterMsgInfoUCtrl
        //{
        //    get { return (uc_MsgInfo)Master.FindControl("uc_MasterMsgInfo"); }
        //}

        //public uc_CommonSearch MasterCommonSearchUCtrl
        //{
        //    get { return (uc_CommonSearch)Master.FindControl("uc_CommonSearchMaster"); }
        //}

        public string MasterUserLocation
        {
            get { return ((TextBox)Master.FindControl("txtMasterUserLocation")).Text; }
            set { ((TextBox)Master.FindControl("txtMasterUserLocation")).Text = value; }
        }

        public string MasterUserProfitCenter
        {
            get { return ((TextBox)Master.FindControl("txtMasterProfitCenters")).Text; }
            set { ((TextBox)Master.FindControl("txtMasterProfitCenters")).Text = value; }
        }

        private string _glbUserName;
        private string _glbUserIP;
        private string _glbUserComputer;
        private string _glbUserComCode;
        private string _glbUserComDesc;
        private string _glbUserSessionID;
        private string _glbUserDefLoca;
        private string _glbUserDefProf;
        private bool _glbisLoging;

        public bool GlbisLoging
        {
            get { return _glbisLoging; }
            set { _glbisLoging = value; }
        }

        public string GlbUserName
        {
            get { return (Session["UserID"] != null) ? Session["UserID"].ToString() : string.Empty; }
        }

        public string GlbUserIP
        {
            get { return (Session["UserIP"] != null) ? Session["UserIP"].ToString() : string.Empty; }
        }

        public string Version
        {
            get { return (Session["Version"] != null) ? Session["Version"].ToString() : string.Empty; }
        }

        public string GlbUserComputer
        {
            get { return (Session["UserComputer"] != null) ? Session["UserComputer"].ToString() : string.Empty; }
        }

        public string GlbUserComCode
        {
            get { return (Session["UserCompanyCode"] != null) ? Session["UserCompanyCode"].ToString() : string.Empty; }
        }

        public string GlbUserComDesc
        {
            get { return (Session["UserCompanyName"] != null) ? Session["UserCompanyName"].ToString() : string.Empty; }
        }

        public string GlbUserSessionID
        {
            get { return (Session["SessionID"] != null) ? Session["SessionID"].ToString() : string.Empty; }
        }

        public string GlbUserDefLoca
        {
            get { return (Session["UserDefLoca"] != null) ? Session["UserDefLoca"].ToString() : string.Empty; }
        }

        public string GlbUserDefProf
        {
            get { return (Session["UserDefProf"] != null) ? Session["UserDefProf"].ToString() : string.Empty; }
        }

        #region REPORT PARAMETERS

        //darshana 15-11-2012 create new parameter set for view reprot seperatly
        public String GlbOthReportName
        {
            get { return Convert.ToString(Session["paraReportNameOth"]); }
            set { Session["paraReportNameOth"] = value; }
        }

        public String GlbOthReportPath
        {
            get { return Convert.ToString(Session["paraReportPathOth"]); }
            set { Session["paraReportPathOth"] = value; }
        }

        public String GlbOthReportMapPath
        {
            get { return Convert.ToString(Session["paraReportMapPathOth"]); }
            set { Session["paraReportMapPathOth"] = value; }
        }

        public String GlbOthDocNosList
        {
            get { return Convert.ToString(Session["paraDocNosListOth"]); }
            set { Session["paraDocNosListOth"] = value; }
        }

        //kapila 11/7/2012     REPORT PARAMETERS------------------------------------------------------------------------------------------------
        public String GlbReportName
        {
            get { return Convert.ToString(Session["paraReportName"]); }
            set { Session["paraReportName"] = value; }
        }

        public String GlbReportPath
        {
            get { return Convert.ToString(Session["paraReportPath"]); }
            set { Session["paraReportPath"] = value; }
        }

        public String GlbReportMapPath
        {
            get { return Convert.ToString(Session["paraReportMapPath"]); }
            set { Session["paraReportMapPath"] = value; }
        }

        public DateTime? GlbFromDate
        {
            get { return Convert.ToDateTime(Session["paraFromDate"]); }
            set { Session["paraFromDate"] = value; }
        }

        public DateTime? GlbToDate
        {
            get { return Convert.ToDateTime(Session["paraToDate"]); }
            set { Session["paraToDate"] = value; }
        }

        //kapila
        public DateTime? GlbFromDate1
        {
            get { return Convert.ToDateTime(Session["paraFromDate1"]); }
            set { Session["paraFromDate1"] = value; }
        }

        //Nadeeka 12-07-2012
        /*
        public String GlbCompany
        {
            get { return Convert.ToString(Session["paraCompany"]); }
            set { Session["paraCompany"] = value; }
        }

        public String GlbChannel
        {
            get { return Convert.ToString(Session["paraChannel"]); }
            set { Session["paraChannel"] = value; }
        }
        */

        public String GlbArea
        {
            get { return Convert.ToString(Session["paraArea"]); }
            set { Session["paraArea"] = value; }
        }

        public String GlbRegion
        {
            get { return Convert.ToString(Session["paraRegion"]); }
            set { Session["paraRegion"] = value; }
        }

        public String GlbZone
        {
            get { return Convert.ToString(Session["paraZone"]); }
            set { Session["paraZone"] = value; }
        }

        public String GlbProfit
        {
            get { return Convert.ToString(Session["paraProfit"]); }
            set { Session["paraProfit"] = value; }
        }

        //Nadeeka 26-09-12
        public String GlbOtherSp
        {
            get { return Convert.ToString(Session["paraOthers"]); }
            set { Session["paraOthers"] = value; }
        }

        public String GlbIsCancelled
        {
            get { return Convert.ToString(Session["paraIsCancelled"]); }
            set { Session["paraIsCancelled"] = value; }
        }

        public String GlbReportHeading_1
        {
            get { return Convert.ToString(Session["paraReportHeading_1"]); }
            set { Session["paraReportHeading_1"] = value; }
        }

        public String GlbReportHeading_2
        {
            get { return Convert.ToString(Session["paraReportHeading_2"]); }
            set { Session["paraReportHeading_2"] = value; }
        }

        public String GlbMainPage
        {
            get { return Convert.ToString(Session["paraMainPage"]); }
            set { Session["paraMainPage"] = value; }
        }

        public String GlbDocNosList
        {
            get { return Convert.ToString(Session["paraDocNosList"]); }
            set { Session["paraDocNosList"] = value; }
        }

        public String GlbReportUser
        {
            get { return Convert.ToString(Session["paraReportUser"]); }
            set { Session["paraReportUser"] = value; }
        }

        //Sanjeewa 2012-07-21
        public String GlbProfitCenter
        {
            get { return Convert.ToString(Session["paraProfitCenter"]); }
            set { Session["paraProfitCenter"] = value; }
        }

        public String GlbPeriod
        {
            get { return Convert.ToString(Session["paraPeriod"]); }
            set { Session["paraPeriod"] = value; }
        }

        public String GlbDocType
        {
            get { return Convert.ToString(Session["paraSaleType"]); }
            set { Session["paraSaleType"] = value; }
        }

        public String GlbDocSubType
        {
            get { return Convert.ToString(Session["paraSaleSubType"]); }
            set { Session["paraSaleSubType"] = value; }
        }

        public String GlbSelectionFormula
        {
            get { return Convert.ToString(Session["paraSelFormula"]); }
            set { Session["paraSelFormula"] = value; }
        }

        //2012-09-22
        public String GlbCompany
        {
            get { return Convert.ToString(Session["paraCompany"]); }
            set { Session["paraCompany"] = value; }
        }

        public String GlbChannel
        {
            get { return Convert.ToString(Session["paraChannel"]); }
            set { Session["paraChannel"] = value; }
        }

        public String GlbLocation
        {
            get { return Convert.ToString(Session["paraLocation"]); }
            set { Session["paraLocation"] = value; }
        }

        public String GlbBrand
        {
            get { return Convert.ToString(Session["paraBrand"]); }
            set { Session["paraBrand"] = value; }
        }

        public String GlbModel
        {
            get { return Convert.ToString(Session["paraModel"]); }
            set { Session["paraModel"] = value; }
        }

        public String GlbItemCode
        {
            get { return Convert.ToString(Session["paraItemCode"]); }
            set { Session["paraItemCode"] = value; }
        }

        public String GlbItemStatus
        {
            get { return Convert.ToString(Session["paraItemStatus"]); }
            set { Session["paraItemStatus"] = value; }
        }

        public String GlbItemCat1
        {
            get { return Convert.ToString(Session["paraItemCat1"]); }
            set { Session["paraItemCat1"] = value; }
        }

        public String GlbItemCat2
        {
            get { return Convert.ToString(Session["paraItemCat2"]); }
            set { Session["paraItemCat2"] = value; }
        }

        public String GlbItemCat3
        {
            get { return Convert.ToString(Session["paraItemCat3"]); }
            set { Session["paraItemCat3"] = value; }
        }

        // Parameters for Stock Balance Report
        public String GlbWithCost
        {
            get { return Convert.ToString(Session["paraWithCost"]); }
            set { Session["paraWithCost"] = value; }
        }

        public String GlbGroupLoc
        {
            get { return Convert.ToString(Session["paraGroupLoc"]); }
            set { Session["paraGroupLoc"] = value; }
        }

        public String GlbGroupChannel
        {
            get { return Convert.ToString(Session["paraGroupChannel"]); }
            set { Session["paraGroupChannel"] = value; }
        }

        public String GlbGroupItemStatus
        {
            get { return Convert.ToString(Session["paraGroupItemStatus"]); }
            set { Session["paraGroupItemStatus"] = value; }
        }

        public String GlbGroupICat1
        {
            get { return Convert.ToString(Session["paraGroupICat1"]); }
            set { Session["paraGroupICat1"] = value; }
        }

        public DateTime GlbAsatDate
        {
            get { return Convert.ToDateTime(Session["paraAsatDate"]); }
            set { Session["paraAsatDate"] = value; }
        }

        public String GlbIsAsAtReport
        {
            get { return Convert.ToString(Session["paraIsAsAtReport"]); }
            set { Session["paraIsAsAtReport"] = value; }
        }

        public String GlbIsSerialReport
        {
            get { return Convert.ToString(Session["paraIsSerialReport"]); }
            set { Session["paraIsSerialReport"] = value; }
        }

        public Int32 GlbFixAssetStatus
        {
            get { return Convert.ToInt32(Session["GlbFixAssetStatus"]); }
            set { Session["GlbFixAssetStatus"] = value; }
        }

        public String GlbFixAsset_RefNo
        {
            get { return Convert.ToString(Session["GlbFixAsset_RefNo"]); }
            set { Session["GlbFixAsset_RefNo"] = value; }
        }

        // END Parameters for Stock Balance Report

        //2012-09-22
        //public String GlbCompany
        //{
        //    get { return Convert.ToString(Session["paraCompany"]); }
        //    set { Session["paraCompany"] = value; }
        //}

        //public String GlbLocation
        //{
        //    get { return Convert.ToString(Session["paraLocation"]); }
        //    set { Session["paraLocation"] = value; }
        //}

        //public String GlbBrand
        //{
        //    get { return Convert.ToString(Session["paraBrand"]); }
        //    set { Session["paraBrand"] = value; }
        //}

        //public String GlbModel
        //{
        //    get { return Convert.ToString(Session["paraModel"]); }
        //    set { Session["paraModel"] = value; }
        //}

        //public String GlbItemCode
        //{
        //    get { return Convert.ToString(Session["paraItemCode"]); }
        //    set { Session["paraItemCode"] = value; }
        //}

        //public String GlbItemStatus
        //{
        //    get { return Convert.ToString(Session["paraItemStatus"]); }
        //    set { Session["paraItemStatus"] = value; }
        //}

        //public String GlbItemCat1
        //{
        //    get { return Convert.ToString(Session["paraItemCat1"]); }
        //    set { Session["paraItemCat1"] = value; }
        //}

        //public String GlbItemCat2
        //{
        //    get { return Convert.ToString(Session["paraItemCat2"]); }
        //    set { Session["paraItemCat2"] = value; }
        //}

        //public String GlbItemCat3
        //{
        //    get { return Convert.ToString(Session["paraItemCat3"]); }
        //    set { Session["paraItemCat3"] = value; }
        //}

        //// Parameters for Stock Balance Report
        //public String GlbWithCost
        //{
        //    get { return Convert.ToString(Session["paraWithCost"]); }
        //    set { Session["paraWithCost"] = value; }
        //}

        //public String GlbGroupLoc
        //{
        //    get { return Convert.ToString(Session["paraGroupLoc"]); }
        //    set { Session["paraGroupLoc"] = value; }
        //}

        //public String GlbGroupChannel
        //{
        //    get { return Convert.ToString(Session["paraGroupChannel"]); }
        //    set { Session["paraGroupChannel"] = value; }
        //}

        //public String GlbGroupItemStatus
        //{
        //    get { return Convert.ToString(Session["paraGroupItemStatus"]); }
        //    set { Session["paraGroupItemStatus"] = value; }
        //}

        //public String GlbGroupICat1
        //{
        //    get { return Convert.ToString(Session["paraGroupICat1"]); }
        //    set { Session["paraGroupICat1"] = value; }
        //}

        //public String GlbAsatDate
        //{
        //    get { return Convert.ToString(Session["paraAsatDate"]); }
        //    set { Session["paraAsatDate"] = value; }
        //}

        //public String GlbIsAsAtReport
        //{
        //    get { return Convert.ToString(Session["paraIsAsAtReport"]); }
        //    set { Session["paraIsAsAtReport"] = value; }
        //}

        //public String GlbIsSerialReport
        //{
        //    get { return Convert.ToString(Session["paraIsSerialReport"]); }
        //    set { Session["paraIsSerialReport"] = value; }
        //}

        //Sanjeewa

        //kapila 27-8-2012
        public Decimal GlbRemToBeBanked
        {
            get { return Convert.ToDecimal(Session["paraRemToBeBanked"]); }
            set { Session["paraRemToBeBanked"] = value; }
        }

        public Decimal GlbCIH
        {
            get { return Convert.ToDecimal(Session["paraCIH"]); }
            set { Session["paraCIH"] = value; }
        }

        public Decimal GlbTotRemitance
        {
            get { return Convert.ToDecimal(Session["paraTotRemitance"]); }
            set { Session["paraTotRemitance"] = value; }
        }

        public Decimal GlbCommWithdrawn
        {
            get { return Convert.ToDecimal(Session["paraCommWithdrawn"]); }
            set { Session["paraCommWithdrawn"] = value; }
        }

        public Decimal GlbRemToBeBankedFinal
        {
            get { return Convert.ToDecimal(Session["paraRemToBeBankedFinal"]); }
            set { Session["paraRemToBeBankedFinal"] = value; }
        }

        public Decimal GlbCIHFinal
        {
            get { return Convert.ToDecimal(Session["paraCIHFinal"]); }
            set { Session["paraCIHFinal"] = value; }
        }

        public Decimal GlbTotRemitanceFinal
        {
            get { return Convert.ToDecimal(Session["paraTotRemitanceFinal"]); }
            set { Session["paraTotRemitanceFinal"] = value; }
        }

        public Decimal GlbCommWithdrawnFinal
        {
            get { return Convert.ToDecimal(Session["paraCommWithdrawnFinal"]); }
            set { Session["paraCommWithdrawnFinal"] = value; }
        }

        public DateTime GlbFromDateSub
        {
            get { return Convert.ToDateTime(Session["paraFromDateSub"]); }
            set { Session["paraFromDateSub"] = value; }
        }

        public DateTime GlbToDateSub
        {
            get { return Convert.ToDateTime(Session["paraToDateSub"]); }
            set { Session["paraToDateSub"] = value; }
        }

        public String GlbReportSerialList
        {
            get { return Convert.ToString(Session["GlbReportSerialList"]); }
            set { Session["GlbReportSerialList"] = value; }
        }

        public String GlbReportWarrantyList
        {
            get { return Convert.ToString(Session["GlbReportWarrantyList"]); }
            set { Session["GlbReportWarrantyList"] = value; }
        }

        public String GlbRecNo
        {
            get { return Convert.ToString(Session["paraRecNo"]); }
            set { Session["paraRecNo"] = value; }
        }

        public Int32 GlbRepDB
        {
            get { return Convert.ToInt32(Session["GlbRepDB"]); }
            set { Session["GlbRepDB"] = value; }
        }

        public Decimal GlbPrevBal
        {
            get { return Convert.ToDecimal(Session["paraPrevBal"]); }
            set { Session["paraPrevBal"] = value; }
        }

        public Decimal GlbCurBal
        {
            get { return Convert.ToDecimal(Session["paraCurBal"]); }
            set { Session["paraCurBal"] = value; }
        }

        public String GlbManager
        {
            get { return Convert.ToString(Session["paraManager"]); }
            set { Session["paraManager"] = value; }
        }

        public String GlbStatus
        {
            get { return Convert.ToString(Session["paraStatus"]); }
            set { Session["paraStatus"] = value; }
        }

        //sachith 2012/08/30
        //Insurance Cover Note print
        public string GlbCoverNoteNo
        {
            get { return Convert.ToString(Session["covNo"]); }
            set { Session["covNo"] = value; }
        }

        //sachith
        public string HPCustType
        {
            get { return Convert.ToString(Session["HPCustType"]); }
            set { Session["HPCustType"] = value; }
        }

        public String GlbVehRegRecNo
        {
            get { return Convert.ToString(Session["GlbVehRegRecNo"]); }
            set { Session["GlbVehRegRecNo"] = value; }
        }

        public String GlbRecType
        {
            get { return Convert.ToString(Session["paraRecType"]); }
            set { Session["paraRecType"] = value; }
        }

        public String GlbReportCompany
        {
            get { return Convert.ToString(Session["paraReportCompany"]); }
            set { Session["paraReportCompany"] = value; }
        }

        public String GlbReportCompanyAddr
        {
            get { return Convert.ToString(Session["paraReportCompanyAddr"]); }
            set { Session["paraReportCompanyAddr"] = value; }
        }

        public String GlbReportDeliveryOrderNo
        {
            get { return Convert.ToString(Session["GlbReportDeliveryOrderNo"]); }
            set { Session["GlbReportDeliveryOrderNo"] = value; }
        }

        public String GlbReportDeliveryOrderPath
        {
            get { return Convert.ToString(Session["GlbReportDeliveryOrderPath"]); }
            set { Session["GlbReportDeliveryOrderPath"] = value; }
        }

        public String GlbReportDeliveryOrderMapPath
        {
            get { return Convert.ToString(Session["GlbReportDeliveryOrderMapPath"]); }
            set { Session["GlbReportDeliveryOrderMapPath"] = value; }
        }

        public String GlbReportDeliveryOrderMainPage
        {
            get { return Convert.ToString(Session["GlbReportDeliveryOrderMainPage"]); }
            set { Session["GlbReportDeliveryOrderMainPage"] = value; }
        }

        //kapila 22/11/2012
        public Int32 GlbReportAccStatus
        {
            get { return Convert.ToInt32(Session["paraReportAccStatus"]); }
            set { Session["paraReportAccStatus"] = value; }
        }

        public void clearReportParameters()
        {
            GlbReportDeliveryOrderPath = "";    //kapila 22/11/2012
            GlbReportDeliveryOrderMapPath = "";  //kapila 22/11/2012
            GlbReportAccStatus = 0;             //kapila 22/11/2012
            GlbRegion = "";
            GlbZone = "";
            GlbProfit = "";
            GlbMainPage = "";
            GlbCommWithdrawn = 0;
            GlbReportSerialList = "";
            GlbReportWarrantyList = "";
            GlbRecNo = "";
            GlbVehRegRecNo = "";
            GlbReportHeading_1 = "";
            GlbReportHeading_2 = "";
            GlbReportUser = "";
            GlbDocNosList = "";
            GlbReportPath = "";
            GlbReportMapPath = "";
            GlbReportName = "";
            GlbFromDate = Convert.ToDateTime("01/01/0001 00:00:00");
            GlbFromDate1 = Convert.ToDateTime("01/01/0001 00:00:00");
            GlbToDate = Convert.ToDateTime("01/01/0001 00:00:00");
            GlbProfitCenter = "";
            GlbPeriod = "";
            GlbDocType = "";
            GlbDocSubType = "";
            GlbSelectionFormula = "";
            GlbCIH = 0;
            GlbCoverNoteNo = "";
            GlbRemToBeBanked = 0;
            GlbToDateSub = Convert.ToDateTime("01/01/0001 00:00:00");
            GlbFromDateSub = Convert.ToDateTime("01/01/0001 00:00:00");
            GlbTotRemitance = 0;
            GlbChannel = "";
            GlbArea = "";
            GlbRecType = "";        //kapila

            //add by darshana on behalf sanjeewa
            GlbRepDB = 0;
            GlbCompany = "";
            GlbLocation = "";
            GlbBrand = "";
            GlbModel = "";
            GlbItemCode = "";
            GlbItemStatus = "";
            GlbItemCat1 = "";
            GlbItemCat2 = "";
            GlbItemCat3 = "";
            GlbWithCost = "";
            GlbGroupLoc = "";
            GlbGroupChannel = "";
            GlbGroupItemStatus = "";
            GlbGroupICat1 = "";
            GlbAsatDate = Convert.ToDateTime("01/01/0001 00:00:00");
            GlbIsAsAtReport = "";
            GlbIsSerialReport = "";
            GlbOthDocNosList = "";
            GlbOthReportMapPath = "";
            GlbOthReportName = "";
            GlbOthReportPath = "";

            GlbCLS_BAL_Pc = null;
            GlbCLS_BAL_SchCode = null;
            GlbCLS_BAL_DueDate = Convert.ToDateTime("01/01/0001 00:00:00");
            GlbCLS_BAL_UserName = null;

            GlbCurBal = 0;
            GlbPrevBal = 0;
        }

        #endregion REPORT PARAMETERS

        //Shani 2012-09-16
        // HP Closing Balance Summary
        public String GlbCLS_BAL_Pc
        {
            get { return Convert.ToString(Session["GlbCLS_BAL_Pc"]); }
            set { Session["GlbCLS_BAL_Pc"] = value; }
        }

        public String GlbCLS_BAL_SchCode
        {
            get { return Convert.ToString(Session["GlbCLS_BAL_SchCode"]); }
            set { Session["GlbCLS_BAL_SchCode"] = value; }
        }

        public DateTime GlbCLS_BAL_DueDate
        {
            get { return Convert.ToDateTime(Session["GlbCLS_BAL_DueDate"]); }
            set { Session["GlbCLS_BAL_DueDate"] = value; }
        }

        public String GlbCLS_BAL_UserName
        {
            get { return Convert.ToString(Session["GlbCLS_BAL_UserName"]); }
            set { Session["GlbCLS_BAL_UserName"] = value; }
        }

        protected override void OnUnload(EventArgs e)
        {
            try
            {
                channelService.CloseAllChannels();
                base.OnUnload(e);
            }
            catch (Exception ex)
            {
            }
        }

        #region Serial Scan

        public string GlbSerialScanRequestNo
        {
            get { return string.IsNullOrEmpty(Convert.ToString(Session["paraRequestNo"])) ? "-1" : Convert.ToString(Session["paraRequestNo"]); }
            set { Session["paraRequestNo"] = value; }
        }

        public string GlbSerialScanReturnUrl
        {
            get { return string.IsNullOrEmpty(Session["paraReturnUrl"].ToString()) ? "-1" : Session["paraReturnUrl"].ToString(); }
            set { Session["paraReturnUrl"] = value; }
        }

        public Int32 GlbSerialScanUserSeqNo
        {
            get { return Convert.ToInt32(Session["paraUserSeqNo"]); }
            set { Session["paraUserSeqNo"] = value; }
        }

        public string GlbSerialScanDocumentType
        {
            get
            {
                if (Session["paraDocumentType"] == null)
                    return "-1";
                else if (string.IsNullOrEmpty(Session["paraDocumentType"].ToString()))
                    return "-1";
                else
                    return Session["paraDocumentType"].ToString();
            }
            set { Session["paraDocumentType"] = value; }
        }

        public Int32 GlbSerialScanDirection
        {
            get { return Convert.ToInt32(Session["paraDirection"]); }
            set { Session["paraDirection"] = value; }
        }

        public String GlbBusinessEntity
        {
            get { return Convert.ToString(Session["paraBusinessEntity"]); }
            set { Session["paraBusinessEntity"] = value; }
        }

        public String GlbSerialBusinessEntity
        {
            get { return Convert.ToString(Session["paraBusinessEntity"]); }
            set { Session["paraBusinessEntity"] = value; }
        }

        #endregion Serial Scan

        #region Invoice Call DO

        public string CallDObyInvoice
        {
            set { Session["CallDObyInvoice"] = value; }
            get { return (string)Session["CallDObyInvoice"]; }
        }

        public string CallDobyInvoiceManual
        {
            set { Session["CallDobyInvoiceManual"] = value; }
            get { return (string)Session["CallDobyInvoiceManual"]; }
        }

        #endregion Invoice Call DO

        #region Public methods

        public ChannelOperator CHNLSVC
        {
            get
            {
                return channelService;
            }
        }

        #endregion Public methods

        //written by prabhath on 31/05/2012
        public Int32 GetNewAutoNumber(string _module, Int16 _direction, string _startChar, string _catType, string _catCode, DateTime _modifyDate, Int32 _year)
        {
            FF.BusinessObjects.MasterAutoNumber _masterAutoNumber = CHNLSVC.Inventory.GetAutoNumber(_module, _direction, _startChar, _catType, _catCode, _modifyDate, _year);
            return _masterAutoNumber.Aut_number;
        }

        //written by prabhath on 31/05/2012
        public Int16 UpdateAutoNumber(FF.BusinessObjects.MasterAutoNumber _masterAutoNumber)
        {
            return CHNLSVC.Inventory.UpdateAutoNumber(_masterAutoNumber);
        }

        //written by prabhath on 31/05/2012
        protected DataTable SetEmptyRow(DataTable _table)
        {
            DataTable _dtTable = _table;
            DataRow _row = _table.NewRow();
            _dtTable.Rows.Add(_row);
            return _dtTable;
        }

        //written by prabhath on 31/05/2012
        protected bool IsNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            decimal result;
            return decimal.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        //written by prabhath on 31/05/2012
        protected bool IsDate(string val, System.Globalization.DateTimeStyles DateTimeStyle)
        {
            DateTime result;
            return DateTime.TryParse(val, System.Globalization.CultureInfo.CurrentCulture, DateTimeStyle, out result);
        }

        #region Regular Expressions

        [System.Web.Services.WebMethod]
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        [System.Web.Services.WebMethod]
        public static bool IsValidNIC(string nic)
        {
            string pattern = @"^[0-9]{9}[V]{1}$";

            System.Text.RegularExpressions.Match match = Regex.Match(nic.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        [System.Web.Services.WebMethod]
        public static bool IsValidMobileOrLandNo(string mobile)
        {
            string pattern = @"^[0-9]{10}$";

            System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        #endregion Regular Expressions

        #region App Setting

        //Chamal 17/07/2012
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string FormatToCurrency(string _value)
        {
            string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToCurrency"].ToString();
            return String.Format(_format, Convert.ToDecimal(_value));
        }
        public static string FormatInvoiceDiscount(string _value)
        {
            string _format = System.Configuration.ConfigurationManager.AppSettings["FormatInvoiceDiscount"].ToString();
            return String.Format(_format, Convert.ToDecimal(_value));
        }

        public string FormatToQty(string _value)
        {
            string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToQty"].ToString();
            return String.Format(_format, Convert.ToDecimal(_value));
        }

        public string FormatToDate(string _value)
        {
            string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToDate"].ToString();
            return String.Format(_format, _value);
        }

        #endregion App Setting

        //Written By Prabhath on 18/07/2012, modify Miginda's code for the location and profit center wise

        #region User Approval and Permission

        public bool GlbReqIsApprovalNeed
        {
            get { return Convert.ToBoolean(Session["GlbReqIsApprovalNeed"]); }
            set { Session["GlbReqIsApprovalNeed"] = value; }
        }

        public int GlbReqRequestLevel
        {
            get { return Convert.ToInt32(Session["GlbReqRequestLevel"]); }
            set { Session["GlbReqRequestLevel"] = value; }
        }

        public int GlbReqRequestApprovalLevel
        {
            get { return Convert.ToInt32(Session["GlbReqRequestApprovalLevel"]); }
            set { Session["GlbReqRequestApprovalLevel"] = value; }
        }

        public int GlbReqUserPermissionLevel
        {
            get { return Convert.ToInt32(Session["GlbReqUserPermissionLevel"]); }
            set { Session["GlbReqUserPermissionLevel"] = value; }
        }

        public bool GlbReqIsApprovalUser
        {
            get { return Convert.ToBoolean(Session["GlbReqIsApprovalUser"]); }
            set { Session["GlbReqIsApprovalUser"] = value; }
        }

        public bool GlbReqIsFinalApprovalUser
        {
            get { return Convert.ToBoolean(Session["GlbReqIsFinalApprovalUser"]); }
            set { Session["GlbReqIsFinalApprovalUser"] = value; }
        }

        public bool GlbReqIsRequestGenerateUser
        {
            get { return Convert.ToBoolean(Session["GlbReqIsRequestGenerateUser"]); }
            set { Session["GlbReqIsRequestGenerateUser"] = value; }
        }

        public void RequestApprovalCycleDefinition(bool _isLocation, CommonUIDefiniton.HirePurchasModuleApprovalCode _modulecode, DateTime _date, string _destinationcompany, string _destinationlocation, CommonUIDefiniton.SalesPriorityHierarchyCategory _category, CommonUIDefiniton.SalesPriorityHierarchyType _type)
        {
            RequestApproveCycleDefinition _obj = null;
            UserRequestApprovePermission _obj_ = null;
            if (_isLocation)
                _obj = CHNLSVC.Security.GetApproveCycleDefinitionDataforLocation(GlbUserComCode, GlbUserDefLoca, Convert.ToString(_modulecode), _date, _destinationcompany, _destinationlocation, Convert.ToString(_category), Convert.ToString(_type));
            else
                _obj = CHNLSVC.Security.GetApproveCycleDefinitionDataforProfitCenter(GlbUserComCode, GlbUserDefProf, Convert.ToString(_modulecode), _date, _destinationcompany, _destinationlocation, Convert.ToString(_category), Convert.ToString(_type));

            if (_isLocation)
                _obj_ = CHNLSVC.Security.GetUserRequestApprovalPermissionDataForLocation(GlbUserName, Convert.ToString(_modulecode), GlbUserComCode, GlbUserDefLoca, Convert.ToString(_category), Convert.ToString(_type));
            else
                _obj_ = CHNLSVC.Security.GetUserRequestApprovalPermissionDataForProfitCenter(GlbUserName, Convert.ToString(_modulecode), GlbUserComCode, GlbUserDefProf, Convert.ToString(_category), Convert.ToString(_type));
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

        #endregion User Approval and Permission

        //Code by Chamal De Silva 10-Aug-2012
        protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, System.Web.UI.Page _page, string _backdate, Image _imgcontrol, Label _lblcontrol)
        {
            string _outmsg = "";
            BackDates _bdt = new BackDates();
            _imgcontrol.Visible = false;
            string _filename = _page.AppRelativeVirtualPath.Substring(_page.AppRelativeVirtualPath.LastIndexOf("/") + 1).Replace(".aspx", "").ToUpper();
            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename, _backdate, out _bdt);
            if (_isAllow == true)
            {
                _imgcontrol.Visible = true;
                if (_bdt != null) _outmsg = "This module back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
            }

            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _outmsg);
            _lblcontrol.Text = _outmsg;

            return _isAllow;
        }

        public string[] GlbSearchResult
        {
            get { return Convert.ToString(Session["GlbSearchResult"]).Split('|'); }
            set { Session["GlbSearchResult"] = value; }
        }

        //kapila 30/10/2012
        public String GlbExcsShortID
        {
            get { return Convert.ToString(Session["GlbExcsShortID"]); }
            set { Session["GlbExcsShortID"] = value; }
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

        //Tharaka 2015-09-10
        protected bool IsAllowBackDateForModuleNew(string _com, string _loc, string _pc, System.Web.UI.Page _page, string _backdate, LinkButton _imgcontrol, Label _lblcontrol, String moduleName, out bool _allowCurrentTrans)
        {
            string _outmsg = "";
            BackDates _bdt = new BackDates();
            _imgcontrol.Visible = false;
            _allowCurrentTrans = false;
            string _filename = string.Empty;

            if (String.IsNullOrEmpty(moduleName))
            {
                _filename = _page.AppRelativeVirtualPath.Substring(_page.AppRelativeVirtualPath.LastIndexOf("/") + 1).Replace(".aspx", "").ToUpper();
            }
            else
            {
                _filename = moduleName;
            }
            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename, _backdate, out _bdt);
            if (_isAllow == true)
            {
                _imgcontrol.Visible = true;
                if (_bdt != null) _outmsg = "This module back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
            }

            if (_bdt == null)
            {
                _allowCurrentTrans = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentTrans = true;
                }
            }

            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _outmsg);
            _lblcontrol.Text = _outmsg;

            return _isAllow;
        }


        public bool CheckServerDateTime(out string msg)
        {
            msg = string.Empty;
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                msg = "Your machine date conflict with the server date! \nPlease contact system administrator....";
                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                msg = "Your machine time zone conflict with the server time zone! \nPlease contact system administrator....";
                return false;
            }

            return true;
        }
    }
}