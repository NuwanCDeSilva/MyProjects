using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FF.WebERPClient
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
            DocNo=91,
            INV_DocNo=92,
            HpParaTp=93,
            ServiceAgent=94,
            AllSalesInvoice=95,
            Schema_category = 96,
            Schema_Type = 97,
            FixAssetRefNo=98
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
            GRAN
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
            ARQT006 // Revert Release
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
            DEBT
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








    }
}
