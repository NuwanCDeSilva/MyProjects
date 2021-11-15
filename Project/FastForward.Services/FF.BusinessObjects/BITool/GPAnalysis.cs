using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class GPAnalysis
    {
        public String COM_CD { get; set; }
        public String CHNL_CD { get; set; }
        public String CHNL_DESC { get; set; }
        public String PC_CD { get; set; }
        public String PC_DESC { get; set; }
        public Int32 YEAR_ { get; set; }
        public Int32 MONTH_ { get; set; }
        public String ITEM_CAT1 { get; set; }
        public String ITEM_CAT1DESC { get; set; }
        public String ITEM_CD { get; set; }
        public String ITEM_DESC { get; set; }
        public String BRAND { get; set; }
        public String ITEM_MODEL { get; set; }
        public Decimal CASH_QTY { get; set; }
        public Decimal CASH_AMOUNT { get; set; }
        public Decimal CREDIT_CARD_QTY { get; set; }
        public Decimal CREDIT_CARD_AMOUNT { get; set; }
        public Decimal CREDIT_QTY { get; set; }
        public Decimal CREDIT_AMOUNT { get; set; }
        public Decimal HIRE_SALE_QTY { get; set; }
        public Decimal HIRE_SALE_AMOUNT { get; set; }
        public Decimal DEBIT_NOTE_QTY { get; set; }
        public Decimal DEBIT_NOTE_AMOUNT { get; set; }
        public Decimal DEALER_SALE_QTY { get; set; }
        public Decimal DEALER_SALE_AMOUNT { get; set; }
        public Decimal TOTAL_QTY { get; set; }
        public Decimal TOTAL_AMOUNT { get; set; }
        public Decimal COST { get; set; }
        public Decimal CASH_GP { get; set; }
        public Decimal CREDIT_CARD_GP { get; set; }
        public Decimal CREDIT_GP { get; set; }
        public Decimal HIRE_SALE_GP { get; set; }
        public Decimal DEBIT_NOTE_GP { get; set; }
        public Decimal DEALER_SALE_GP { get; set; }
        public Decimal TOTAL_GP { get; set; }
        public static GPAnalysis Converter(DataRow row)
        {
            return new GPAnalysis
            {
                COM_CD = row["COM_CD"] == DBNull.Value ? string.Empty : row["COM_CD"].ToString(),
                CHNL_CD = row["CHNL_CD"] == DBNull.Value ? string.Empty : row["CHNL_CD"].ToString(),
                CHNL_DESC = row["CHNL_DESC"] == DBNull.Value ? string.Empty : row["CHNL_DESC"].ToString(),
                PC_CD = row["PC_CD"] == DBNull.Value ? string.Empty : row["PC_CD"].ToString(),
                PC_DESC = row["PC_DESC"] == DBNull.Value ? string.Empty : row["PC_DESC"].ToString(),
                YEAR_ = row["YEAR_"] == DBNull.Value ? 0 : Convert.ToInt32(row["YEAR_"].ToString()),
                MONTH_ = row["MONTH_"] == DBNull.Value ? 0 : Convert.ToInt32(row["MONTH_"].ToString()),
                ITEM_CAT1 = row["ITEM_CAT1"] == DBNull.Value ? string.Empty : row["ITEM_CAT1"].ToString(),
                ITEM_CAT1DESC = row["ITEM_CAT1DESC"] == DBNull.Value ? string.Empty : row["ITEM_CAT1DESC"].ToString(),
                ITEM_CD = row["ITEM_CD"] == DBNull.Value ? string.Empty : row["ITEM_CD"].ToString(),
                ITEM_DESC = row["ITEM_DESC"] == DBNull.Value ? string.Empty : row["ITEM_DESC"].ToString(),
                BRAND = row["BRAND"] == DBNull.Value ? string.Empty : row["BRAND"].ToString(),
                ITEM_MODEL = row["ITEM_MODEL"] == DBNull.Value ? string.Empty : row["ITEM_MODEL"].ToString(),
                CASH_QTY = row["CASH_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CASH_QTY"].ToString()),
                CASH_AMOUNT = row["CASH_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CASH_AMOUNT"].ToString()),
                CREDIT_CARD_QTY = row["CREDIT_CARD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CREDIT_CARD_QTY"].ToString()),
                CREDIT_CARD_AMOUNT = row["CREDIT_CARD_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CREDIT_CARD_AMOUNT"].ToString()),
                CREDIT_QTY = row["CREDIT_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CREDIT_QTY"].ToString()),
                CREDIT_AMOUNT = row["CREDIT_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CREDIT_AMOUNT"].ToString()),
                HIRE_SALE_QTY = row["HIRE_SALE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HIRE_SALE_QTY"].ToString()),
                HIRE_SALE_AMOUNT = row["HIRE_SALE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HIRE_SALE_AMOUNT"].ToString()),
                DEBIT_NOTE_QTY = row["DEBIT_NOTE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEBIT_NOTE_QTY"].ToString()),
                DEBIT_NOTE_AMOUNT = row["DEBIT_NOTE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEBIT_NOTE_AMOUNT"].ToString()),
                DEALER_SALE_QTY = row["DEALER_SALE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEALER_SALE_QTY"].ToString()),
                DEALER_SALE_AMOUNT = row["DEALER_SALE_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEALER_SALE_AMOUNT"].ToString()),
                TOTAL_QTY = row["TOTAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOTAL_QTY"].ToString()),
                TOTAL_AMOUNT = row["TOTAL_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOTAL_AMOUNT"].ToString()),
                COST = row["COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["COST"].ToString()),
                CASH_GP = row["CASH_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CASH_GP"].ToString()),
                CREDIT_CARD_GP = row["CREDIT_CARD_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CREDIT_CARD_GP"].ToString()),
                CREDIT_GP = row["CREDIT_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CREDIT_GP"].ToString()),
                HIRE_SALE_GP = row["HIRE_SALE_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HIRE_SALE_GP"].ToString()),
                DEBIT_NOTE_GP = row["DEBIT_NOTE_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEBIT_NOTE_GP"].ToString()),
                DEALER_SALE_GP = row["DEALER_SALE_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEALER_SALE_GP"].ToString()),
                TOTAL_GP = row["TOTAL_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOTAL_GP"].ToString())
            };
        }
    }
}

