using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class SMS_OBJ
    {
        public string MOBILE { get; set; }
        public string NAME { get; set; }
    }
    public class CUSTOMER_SALES
    {
        public string INVOICE_NO { get; set; }
        public decimal AMOUNT { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public string PC { get; set; }
        public string PC_TOWN { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public string MODEL { get; set; }
        //public string MI_SHORTDESC { get; set; }
        public string BRAND { get; set; }
        public string CATEGORY_1 { get; set; }
        public string CATEGORY_2 { get; set; }
        public string CATEGORY_3 { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string TOWN { get; set; }
        public string NATIONALTY { get; set; }
        public string DISTRICT { get; set; }
        public string PROVINCE { get; set; }

        public static CUSTOMER_SALES ConverterAll(DataRow row)
        {
            return new CUSTOMER_SALES
            {
                INVOICE_NO = row["INVOICE_NO"] == DBNull.Value ? string.Empty : row["INVOICE_NO"].ToString(),
                AMOUNT = row["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AMOUNT"].ToString()),
                INVOICE_DATE = row["INVOICE_DATE"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime(row["INVOICE_DATE"].ToString()),
                PC = row["PC"] == DBNull.Value ? string.Empty : row["PC"].ToString(),
                PC_TOWN = row["PC_TOWN"] == DBNull.Value ? string.Empty : row["PC_TOWN"].ToString(),
                ITEM_CODE = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                ITEM_DESCRIPTION = row["ITEM_DESCRIPTION"] == DBNull.Value ? string.Empty : row["ITEM_DESCRIPTION"].ToString(),
                MODEL = row["MODEL"] == DBNull.Value ? string.Empty : row["MODEL"].ToString(),
                BRAND = row["BRAND"] == DBNull.Value ? string.Empty : row["BRAND"].ToString(),
                CATEGORY_1 = row["CATEGORY_1"] == DBNull.Value ? string.Empty : row["CATEGORY_1"].ToString(),
                CATEGORY_2 = row["CATEGORY_2"] == DBNull.Value ? string.Empty : row["CATEGORY_2"].ToString(),
                CATEGORY_3 = row["CATEGORY_3"] == DBNull.Value ? string.Empty : row["CATEGORY_3"].ToString(),
                CUSTOMER_CODE = row["CUSTOMER_CODE"] == DBNull.Value ? string.Empty : row["CUSTOMER_CODE"].ToString(),
                NAME = row["NAME"] == DBNull.Value ? string.Empty : row["NAME"].ToString(),
                ADDRESS = row["ADDRESS"] == DBNull.Value ? string.Empty : row["ADDRESS"].ToString(),
                MOBILE = row["MOBILE"] == DBNull.Value ? string.Empty : row["MOBILE"].ToString(),
                EMAIL = row["EMAIL"] == DBNull.Value ? string.Empty : row["EMAIL"].ToString(),
                TOWN = row["TOWN"] == DBNull.Value ? string.Empty : row["TOWN"].ToString(),
                NATIONALTY = row["NATIONALTY"] == DBNull.Value ? string.Empty : row["NATIONALTY"].ToString()
              //  DISTRICT = row["DISTRICT"] == DBNull.Value ? string.Empty : row["DISTRICT"].ToString(),
                //PROVINCE = row["PROVINCE"] == DBNull.Value ? string.Empty : row["PROVINCE"].ToString()

                //SAD_INV_NO = row["SAD_INV_NO"] == DBNull.Value ? string.Empty : row["SAD_INV_NO"].ToString(),
                //AMOUNT = row["AMOUNT"] == DBNull.Value ? 0: Convert.ToDecimal(row["AMOUNT"].ToString()),
                //MBE_CD = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                //MBE_NAME = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                //MBE_ADD1 = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                //MBE_MOB = row["MBE_MOB"] == DBNull.Value ? string.Empty : row["MBE_MOB"].ToString(),
                //MBE_EMAIL = row["MBE_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_EMAIL"].ToString(),
                //SAD_ITM_CD = row["SAD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ITM_CD"].ToString(),
                //MI_SHORTDESC = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString()

            };
        }
        public static CUSTOMER_SALES ConverterSub(DataRow row)
        {
            return new CUSTOMER_SALES
            {
                INVOICE_NO = row["INVOICE_NO"] == DBNull.Value ? string.Empty : row["INVOICE_NO"].ToString(),
                AMOUNT = row["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AMOUNT"].ToString()),
                INVOICE_DATE = row["INVOICE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INVOICE_DATE"].ToString()),
                PC = row["PC"] == DBNull.Value ? string.Empty : row["PC"].ToString(),
                PC_TOWN = row["PC_TOWN"] == DBNull.Value ? string.Empty : row["PC_TOWN"].ToString(),
                CUSTOMER_CODE = row["CUSTOMER_CODE"] == DBNull.Value ? string.Empty : row["CUSTOMER_CODE"].ToString(),
                NAME = row["NAME"] == DBNull.Value ? string.Empty : row["NAME"].ToString(),
                ADDRESS = row["ADDRESS"] == DBNull.Value ? string.Empty : row["ADDRESS"].ToString(),
                MOBILE = row["MOBILE"] == DBNull.Value ? string.Empty : row["MOBILE"].ToString(),
                EMAIL = row["EMAIL"] == DBNull.Value ? string.Empty : row["EMAIL"].ToString(),
                TOWN = row["TOWN"] == DBNull.Value ? string.Empty : row["TOWN"].ToString(),
                NATIONALTY = row["NATIONALTY"] == DBNull.Value ? string.Empty : row["NATIONALTY"].ToString(),
                DISTRICT = row["DISTRICT"] == DBNull.Value ? string.Empty : row["DISTRICT"].ToString(),
                PROVINCE = row["PROVINCE"] == DBNull.Value ? string.Empty : row["PROVINCE"].ToString()
            };
        }
        public static CUSTOMER_SALES CustomerDetails(DataRow row)
        {
            return new CUSTOMER_SALES
            {
                //INVOICE_NO = row["INVOICE_NO"] == DBNull.Value ? string.Empty : row["INVOICE_NO"].ToString(),
                AMOUNT = row["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AMOUNT"].ToString()),
                INVOICE_DATE = row["INVOICE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INVOICE_DATE"].ToString()),
                //PC = row["PC"] == DBNull.Value ? string.Empty : row["PC"].ToString(),
                //PC_TOWN = row["PC_TOWN"] == DBNull.Value ? string.Empty : row["PC_TOWN"].ToString(),
                CUSTOMER_CODE = row["CUSTOMER_CODE"] == DBNull.Value ? string.Empty : row["CUSTOMER_CODE"].ToString(),
                NAME = row["NAME"] == DBNull.Value ? string.Empty : row["NAME"].ToString(),
                ADDRESS = row["ADDRESS"] == DBNull.Value ? string.Empty : row["ADDRESS"].ToString(),
                MOBILE = row["MOBILE"] == DBNull.Value ? string.Empty : row["MOBILE"].ToString(),
                EMAIL = row["EMAIL"] == DBNull.Value ? string.Empty : row["EMAIL"].ToString(),
                TOWN = row["TOWN"] == DBNull.Value ? string.Empty : row["TOWN"].ToString(),
                NATIONALTY = row["NATIONALTY"] == DBNull.Value ? string.Empty : row["NATIONALTY"].ToString()
             //   DISTRICT = row["DISTRICT"] == DBNull.Value ? string.Empty : row["DISTRICT"].ToString(),
               // PROVINCE = row["PROVINCE"] == DBNull.Value ? string.Empty : row["PROVINCE"].ToString()
            };
        } 
    }
}
