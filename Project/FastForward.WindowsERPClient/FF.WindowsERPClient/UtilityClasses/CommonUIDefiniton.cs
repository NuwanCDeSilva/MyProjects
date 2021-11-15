using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FF.WindowsERPClient
{
    /// <summary>
    /// This is a common class for common User Interface definition.
    /// Created By : Miginda Geeganage.
    /// Created On : 05/03/2012
    /// Modified By :
    /// Modified On :
    /// </summary>
    public static class CommonUIDefiniton
    {
        public enum MessageType
        {
            Error = 1,
            warning = 2,
            Information = 3,
            Critical = 4
        }

        public enum SearchUserControlType
        {
            Default = 0,
            Location = 1,
            Company = 2,
            Item = 3,
            AvailableSerial = 4,
            UserLocation = 5,
            UserProfitCenter = 6,
            SalesInvoice = 7,
            SalesOrder = 8,
            InvoiceType = 9,
            PriceBook = 10,
            PriceLevel = 11,
            PriceLevelItemStatus = 12,
            Customer = 13,
            Currency = 14,
            Employee_Executive = 15,
            Supplier = 16,
            ItemStatus = 17,
            PurchaseOrder = 18,
            IssuedSerial = 19,
            ReceiptType = 20,
            Bank = 21,
            BankAccount = 22,
            OutstandingInv = 23,
            Division = 24,
            Receipt = 25,
            OutsideParty = 26,
            Country = 27,
            AvailableNoneSerial = 28,
            Group_Sale = 29,
            Channel = 30,
            AllProfitCenters = 31,
            NIC = 32,
            Mobile = 33,
            InvSalesInvoice = 34,
            Loc_HIRC_Company = 35,
            Loc_HIRC_Channel = 36,
            Loc_HIRC_SubChannel = 37,
            Loc_HIRC_Area = 38,
            Loc_HIRC_Region = 39,
            Loc_HIRC_Zone = 40,
            Loc_HIRC_Location = 41,
            CAT_Main = 42,
            CAT_Sub1 = 43,
            CAT_Sub2 = 44,
            Inventory_Tracker = 45,
            PriceItem = 46,
            HireSalesInvoice = 47,
            HireSalesAccount = 48,
            Item_Documents = 49,
            Item_Serials = 50,
            Scheme = 51,
            PriceType = 52,
            WarraWarrantyNo = 53,
            WarraSerialNo = 54,
            GeneralRequest = 55,
            CashInvoice = 56,
            HireInvoice = 57,
            PriceBookByCompany = 58,
            AvailableSerialWithOth = 59,
            InvoiceItems = 60,
            PriceLevelByBook = 61,
            HpAccount = 62,
            InsuCom = 63,
            InsuPolicy = 64,
            UserID = 65,
            WHAREHOUSE = 66,
            Town = 67,
            OPE = 68,
            Pc_HIRC_Company = 69,
            Pc_HIRC_Channel = 70,
            Pc_HIRC_SubChannel = 71,
            Pc_HIRC_Area = 72,
            Pc_HIRC_Region = 73,
            Pc_HIRC_Zone = 74,
            Pc_HIRC_Location = 75,
            ItemAvailableSerial = 76,
            Sales_Type = 77,
            Model = 78,
            Insurance_Term = 79,
            DeliverdSerials = 80,
            InvoiceItemUnAssable = 81,
            BankBranch = 82,
            Circular = 83,
            Promotion = 84,
            Loyalty_Type = 85,
            EmployeeCate = 86,
            EmployeeEPF = 87,
            Sales_SubType = 88,
            Brand = 89,
            QuotationForInvoice = 90,
            DocNo = 91,
            INV_DocNo = 92,
            HpParaTp = 93,
            ServiceAgent = 94,
            AllSalesInvoice = 95,
            Schema_category = 96,
            Schema_Type = 97,
            FixAssetRefNo = 98,
            CustomerCommon = 99,
            DepositBankBranch = 100,
            InvDocs = 101,
            InvoiceByCus = 102,
            AcJobNo = 103,
            MRN = 104,
            SerialNonSerial = 105,
            CashInvoiceByCus = 106,
            GRNItem = 107,
            DINRequestNo = 108,
            HPInvoiceByCus = 109,
            HpInvoices = 110,
            SatReceipt = 111,
            InvSalesInvoiceForReversal = 112,
            InterTransferRequest =113,
            InterTransferReceipt=114,
            InterTransferInvoice=115,
            Circualr =116,
            HpAdjType = 117,
            RCC=118,
            ServiceInvoice=119,
            Cheque =120,
            WarrantyClaimInvoice=121,
            WarrantyClaimSerial = 122,
            DocSubType = 123,
            RegistrationDet = 124,
            VehicleInsuranceRef=125,
            CashCommissionCircular=126,
            InsuaranceDet =127,
            MovementDocDateSearch=128,
            VehicalInsuranceDebit=129,
            VehicalInsuranceRegNo=130,
            Hp_ActiveAccounts =131,
            RawPriceBook=132,
            RawPriceLevel=133,
            AvailableSerialWithTypes =134,
            GitDocDateSearch=135,
            GitDocWithLocDateSearch = 136,
            VehicalJobRegistrationNo=137,
            SchemeTypeByCate = 138,
            AllScheme = 139,
            InvoiceDet =140,
            PartyType=141,
            TransactionType = 142,
            PartyCode=143,
            ItemBrand=144,
            promoCode = 145,
            CircularByBook =146,
            VoucherNo=147,
            CircularForSerial=148,
            SerialForCircular = 149,
            AllProofDoc = 150,
            InvSalesInvoiceForReversalOth = 151,
            InternalVoucherExpense=152,
            GPC=153,
            HPInvoiceOth=154,
            MovementTypes =155,
            InventoryDirection = 156,
            GiftVoucher = 157,
            AvailableGiftVoucher=158,
            GetCompanyInvoice = 159,
            GetItmByType = 160,
            CreditNote=161,
            UserRole =162,
            CustomerId=163,
            BuyBackItem=164,
            SchByCir=165,
            InvoiceWithDate=166,
            SysOptGroups= 167,
            SearchLoyaltyCard=168,
            SearchGsByCus=169,
            searchCircular=170,
            LoyaltyCardNo=171,
            InvoiceExecutive=172,
            DocProInvoiceNo = 173,
            DocProEngine=174,
            DocProChassis=175,
            VehRegTxn=176,
            SystemRole= 177,
            SystemUser =178,
            SecUsrPermTp= 179,
            CustomerAll=180,
            MRN_AllLoc = 181,
            CustomerQuo=182,
            Designation=183,
            Department=184,
            Prefix=185,
            InvoiceItemUnAssableByModel=186,
            PromotionCode,
            WarrantExtendItem=188,
            AcServChgCode =189,
            EliteCircular=190,
            ApprovePermCode= 191,
            ApprovePermLevelCode =192,
            POrder =193,
            EmployeeSubCategory=194,
            PromotionalCircular=195,
            PritHierarchy=196,
            RCCType=197,
            RCCRepStus=198,
            RCCColMethod=199,
            RCCCloseTp=200,
            CircularByComp = 201,
            PromoByComp=202,
            IncentiveCirc=203,
            AdvancedReciept=204,
            GiftVoucherByPage=205,
            LoyaltyCustomer=206,
            CustomerCommonByNIC=207,
            RCCReq=208,
            InvTrcChnl=209,
            CashComCirc=210,
            ExchangeINDocument=211,
            ExchangeInvoice=212,
            HpInvoicesCancel=213,
            SearchReversal=214,
            SearchJobNo=215,
            SearchRevAcc=216,
            AuditCashVerify=217,
            AuditStockVerify=218,
            ExchangeJob=219,
            IncSaleTp=220,
            InventoryItem=221,
            GvCategory =222,
            District=223,
            Province=224,
            ReceiptDate=225,
            AccountDate=226,
            RccByCompleteStage =227,
            RccByRequestStage = 228,
            PromotionalDiscount = 229,
            PBVoucher=230,
            AccountChecklist=231,
            AccountChecklistPOD=232,
            SupplierFrmSerial=233,
            Deduction=234,
            Refund=235,
            IncentiveCircular=236,
            AdvanceRecForCus=237,
            HpAccountStus=238,
            LocationCat3=239,
            ToLocation = 240,
            ServiceAgentLoc=241,
            AllInactiveScheme=242,
            Area=243,
            Region=244,
            Zone=245,
            BankALL=246,
            DisVouTp=247,
            GenDiscount=248,
            ManIssRec=249,
            OutstandingInvOth = 250,
            ReturnCheque=251,
            AvailableSer4Itm=252,
            MgrIssueCheque=253,
            satReceiptByAnal3=254,
            PromotionVoucherCricular,
            DiscountCircularPending,
            PriceAsignApprovalPendingCricular,
            Promotor=258,
            BusDesig=259,
            BusDept=260,
            Towns=261,
            PayCircular=262,
            DepositBank=263,
            SerialNIC=264,
            JournalEntryAccount = 265,
            AdjType=266,
            Add_Inv_Tracker=267,
            ChequeVouchers = 268,
            Town_new=269,
            FundTransferVouchers=270,
            ServiceJobDetails = 271,
            EMP_ALL = 272,
            DefectTypes = 273,
            UtiliLocation=274,
            ServiceJobSearch = 275,
            ServiceEstimate= 276,
            ServiceSerial=277,
            ServiceWIPMRN_Loc = 278,
            Office=279,
            SerDef_Code=280,
            ChequeBook = 281,
            TechComments = 282,
            EstimateItems = 283,
            ConsumableItms = 284,
            Service_JobReqNum = 285,
            Service_conf_jobSearch = 286,
            Service_conf_customer = 287,
            EetimateByJob = 288,
            ServiceRequests = 289,
            SupplierCommon = 290,
            ServiceInvoiceSearch = 291,
            ServiceDetailSearials = 292,
            POByDate = 293,
            ServicePriority=294,
            ServiceTaskCate = 295,
            ServiceStageChages = 296,
            ServiceCustSatisfact=297,
            ServiceSupWCNno=298,
            ServiceClaimSupplier = 299,
            GetItmByTypeNew = 300,
            ServiceGatePass=301,
            ServiceJobSearchWIP = 302,
            ServiceJobStage=303,
            DO_InoiceNumber = 304,
            SerialNICAll=305,
            JobSerial = 306,
            ItemSearchComp = 307,
            PartCode = 308,
            SerialAvb = 309,
            EmployeeAll=310,
            ItemCate=311,
            MainItem=312,
            CustQuest=313,
            CustGrade=314,
            CustSatis=315,
            ConfByJob=316,
            MovementQuoDateSearch = 317,
            ItemSupplier=318,
            TourEnquiry = 319,
            TourFacCom = 320,
            HpAccountAdj = 321,
            AllBankAccount =322,
            ChequeNo=323,
            spromotor = 324,
            HpAcc4Act=325,
            AgrType=326,
            AgrClaimType=327,
            MasterType=328,
            ServiceJObSerarhEnquiry = 329,
            ServiceJobSearchWarrClaim = 330,
            masterItem =331,
            masterCat1 =332,
            masterCat2 =333,
            masterCat3 =334,
            masterCat4 = 335,
            masterCat5 = 336,
            masterUOM = 337,
            masterColor = 338,
            masterTax = 339,
            Agreement=340,
            masterContry =341,
            GRNNo=342,
            TaxCodes=343,
            CircularDef = 344,
            SupplierQuo=345,
            ModelMaster=346,
            InsPayAcc=347,
            InsReqInvoice=348,
            SignOnSeq=349,
            TempGRNNo=350,
            CreditNoteAud=351,
            Items_FA=352,
            Status_FA = 353,
            SignOnSeqbyDt=354,
            ACInsReq=355,
            InspecReq=356,
            ConsRecNo=357,
            QuoByCust=358,
            SerialNo=359,
            DCNItems=360,
            CAT_Sub3=361,
            CAT_Sub4 = 362, 
            CAT_Sub5=363,
            SubLoc=364,
            ServiceJobSearchF3 = 365,
            CustomerQuoInv = 366,
            CancelReq=367,
            SearchLoyaltyCardNo = 368,
            CancelSerialCirc=369,
            AllPBLevelByBook=370,
            CLS_ALW_LOC=371,
            Promot_Comm=372,
            AvailableSerialSCM=373,
            AdminTeam=374,
            PendADVNum=375,
            InvoiceVItemUnAssable=376,
            ItemByLocation = 377,
            SubReceiptTypes = 378,
            InvoiceWithoutReversal = 379,
            ServiceJobSearchByCustomer = 380,
            CreditNoteWithoutSRN = 381,
            Auditors = 382,
            BLHeader=383,
            TaxCode=384,
            TaxrateCodes=385,
            ExchangeRequest=386,
            ForwardInvoice=387,
            FixAssetRequest=388,
            ServiceJobsWithStage=389,
            JobRegNo=390,
            PreferLoc=391,
            ItemSerNo = 392,
            SubJob =393,
            EMP_ADVISOR = 394,
            POSupplier = 395, //updated by akila 2017/06/28
            ReservationNo = 396,//updated by akila 2017/08/14
            Route_cd=397, // add by tharanga 2017/08/29
            brandmngr = 398,
            Nationality, //Akila 2017/11/27
            Ref_code, //Tharindu 2017-12-07
          
            SearchSalesType, //by akila 2017/10/26
            Do_qua_serch,
            ServicePrioritybycust,
            ADJREQ,
            RegisterdEvents, //akila 2018/01/25
            DistrictCode, //akila 2018/03/29
            Sunaccount, //add by tharanga 2018/04/09
            MID_SERCH, //add by tharanga 2018/04/09
            vehregdet, //add by tharanga 2018/05/17
            tec_team_cd, //add by tharanga 2018/06/22
            Scv_loc,//add by tharanga 2018/07/13
            serch_pc_byloc,//add by tharanga 2018/07/13
            serch_service_loc,//add by tharanga 2018/07/13
             Scv_agent,//add by tharanga 2018/07/13
             DepositBank_cred,//add by tharanga 2018/09/06
             DocSubType_ADJ_REQ,//add by tharanga 2018/9/12
             AC_SVC_ALW_LOC, //add by tharanga 2018/10.15
        }

        public enum SearchUserControlCustomerType
        {
            MBE_CD,
            MBE_NIC,
            MBE_MOB

        }

        /// <summary>
        /// Assign for take part of the customer search in one common stored procedure
        /// Written By Prabhath on 27/04/2012
        /// </summary>
        #region Customer User Search Definition
        private static Dictionary<SearchUserControlCustomerType, string> SearchUserControlCustomerDescription = new Dictionary<SearchUserControlCustomerType, string> 
         {
            { SearchUserControlCustomerType.MBE_CD,"Code"},
            { SearchUserControlCustomerType.MBE_MOB,"Mobile No"},
            { SearchUserControlCustomerType.MBE_NIC,"NIC No"}
         };


        public static string ReturnCustomerSearchDisplay(SearchUserControlCustomerType _searchUserControlCustomerType)
        {
            string _display;
            SearchUserControlCustomerDescription.TryGetValue(_searchUserControlCustomerType, out _display);
            return _display;
        }
        #endregion

        public enum MasterTypeCategory
        {
            RCC,
            REQ,
            GRAN,
            PDA
        }

        //Written By Prabhath on 07/07/2012
        public enum SalesPriorityHierarchyType
        {
            PC,
            LOCATION,
            ITM
        }
        //Written By Prabhath on 07/07/2012
        public enum SalesPriorityHierarchyCategory
        {
            LOC_PRIT_HIERARCHY,//Location 
            PC_PRIT_HIERARCHY,//Profit Center
            ITM_PRIT_HIERARCHY//Item
        }

        //Written By Prabhath on 07/07/2012
        public enum HirePurchasCheckOn
        {
            UP,
            AF,
            HP
        }
        //Written By Prabhath on 07/07/2012
        public enum HirePurchasAdjustmentType
        {
            RCT
        }

        //Written By Prabhath on 07/07/2012
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
            ARQT007,// ADVAN Receipt Reverse
            ARQT011, //MANUAL DOC TRANSFER
            ARQT016, //WarrantyClaimCreditNote
            ARQT034 //Credit Customer Request

        }

        //Written By Prabhath on 27/07/2012
        public enum SalesType
        {
            SO,
            INV
        }

        //Written By Prabhath on 27/07/2012
        public enum InvoiceType
        {
            CS,
            HS
        }

        public enum ReturnRequestDocumentType
        {
            Cash,
            Hire,
            Request
        }


        public enum BusinessEntityType
        {
            BANK,
            HP,
            INS

        }


        public enum SMSDocumentType
        {
            DISCOUNT
        }


        public enum PayMode
        {
            CASH,
            ADVAN,
            CRNOTE,
            CHEQUE,
            CRCD,
            LORE,
            GVO,
            GVS,
            DEBT,
            STAR_PO,
            BANK_SLIP,
            TR_CHEQUE
        }
        public enum AdjustmentType
        {
            ADHOC
        }

        public enum CustomerMonitorSearchType
        {
            ACCOUNT,
            INVOICE,
            SSI,
            RECEIPT,
            VEHREGRECEIPT,
            VEHINSRECEIPT,
            VEHREGISTRATION,
            DELIVERYORDER,
            SALESRETURN,
            SERIALNO,
            COVERTNOTE,
            CONTACT
        }


        public enum UserPermissionType
        {
            RHP3,
            RSL2,
            RSL22,
            RSL16,
            INV6,
            INV1,
            HPPRM,
            RHP1,
            RSL5,
            RSL7,
            RSL9,
            RSL12,
            RSL18,
            RSL4,
            ACRES,
            INV5,
            RSL17,
            RSL19,
            SCMI,
            SCM2I,
            INV2,
            RSL10,
            ACJOB,
            INV3,
            RHP5,
            RSL3,
            RSL11,
            RSL15,
            RSL20,
            RSL21,
            SACAN,
            DOCRP,
            INV4,
            RHP2,
            RHP4,
            RSL1,
            RSL6,
            RSL23,
            DIROUT,
            INV7,
            WARCLC,
            PRICENQ
        }






    }
}
