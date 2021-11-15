using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class SalesInventoryAge
    {
        public String USER_ID { get; set; }
        public DateTime USER_DT { get; set; }
        public String ITEM_CAT1 { get; set; }
        public String ITEM_CODE { get; set; }
        public String ITEM_DESC { get; set; }
        public Decimal JAN_QTY1 { get; set; }
        public Decimal JAN_AMT1 { get; set; }
        public Decimal FEB_QTY2 { get; set; }
        public Decimal FEB_AMT2 { get; set; }
        public Decimal MAR_QTY3 { get; set; }
        public Decimal MAR_AMT3 { get; set; }
        public Decimal APR_QTY4 { get; set; }
        public Decimal APR_AMT4 { get; set; }
        public Decimal MAY_QTY5 { get; set; }
        public Decimal MAY_AMT5 { get; set; }
        public Decimal JUN_QTY6 { get; set; }
        public Decimal JUN_AMT6 { get; set; }
        public Decimal JUL_QTY7 { get; set; }
        public Decimal JUL_AMT7 { get; set; }
        public Decimal AUG_QTY8 { get; set; }
        public Decimal AUG_AMT8 { get; set; }
        public Decimal SEP_QTY9 { get; set; }
        public Decimal SEP_AMT9 { get; set; }
        public Decimal OCT_QTY10 { get; set; }
        public Decimal OCT_AMT10 { get; set; }
        public Decimal NOV_QTY11 { get; set; }
        public Decimal NOV_AMT11 { get; set; }
        public Decimal DEC_QTY12 { get; set; }
        public Decimal DEC_AMT12 { get; set; }
        public Decimal TOT_SQTY { get; set; }
        public Decimal TOT_SAMT { get; set; }
        public Decimal QTY_90 { get; set; }
        public Decimal VAL_90 { get; set; }
        public Decimal QTY_120 { get; set; }
        public Decimal VAL_120 { get; set; }
        public Decimal QTY_180 { get; set; }
        public Decimal VAL_180 { get; set; }
        public Decimal QTY_270 { get; set; }
        public Decimal VAL_270 { get; set; }
        public Decimal QTY_360 { get; set; }
        public Decimal VAL_360 { get; set; }
        public Decimal QTY_361 { get; set; }
        public Decimal VAL_361 { get; set; }
        public Decimal QTY_TOT { get; set; }
        public Decimal VAL_TOT { get; set; }

        public static SalesInventoryAge Converter(DataRow row)
        {
            return new SalesInventoryAge
            {
                USER_ID = row["USER_ID"] == DBNull.Value ? string.Empty : row["USER_ID"].ToString(),
                USER_DT = row["USER_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["USER_DT"].ToString()),
                ITEM_CAT1 = row["ITEM_CAT1"] == DBNull.Value ? string.Empty : row["ITEM_CAT1"].ToString(),
                ITEM_CODE = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                ITEM_DESC = row["ITEM_DESC"] == DBNull.Value ? string.Empty : row["ITEM_DESC"].ToString(),
                JAN_QTY1 = row["JAN_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JAN_QTY1"].ToString()),
                JAN_AMT1 = row["JAN_AMT1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JAN_AMT1"].ToString()),
                FEB_QTY2 = row["FEB_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FEB_QTY2"].ToString()),
                FEB_AMT2 = row["FEB_AMT2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FEB_AMT2"].ToString()),
                MAR_QTY3 = row["MAR_QTY3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MAR_QTY3"].ToString()),
                MAR_AMT3 = row["MAR_AMT3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MAR_AMT3"].ToString()),
                APR_QTY4 = row["APR_QTY4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["APR_QTY4"].ToString()),
                APR_AMT4 = row["APR_AMT4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["APR_AMT4"].ToString()),
                MAY_QTY5 = row["MAY_QTY5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MAY_QTY5"].ToString()),
                MAY_AMT5 = row["MAY_AMT5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MAY_AMT5"].ToString()),
                JUN_QTY6 = row["JUN_QTY6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JUN_QTY6"].ToString()),
                JUN_AMT6 = row["JUN_AMT6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JUN_AMT6"].ToString()),
                JUL_QTY7 = row["JUL_QTY7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JUL_QTY7"].ToString()),
                JUL_AMT7 = row["JUL_AMT7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JUL_AMT7"].ToString()),
                AUG_QTY8 = row["AUG_QTY8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUG_QTY8"].ToString()),
                AUG_AMT8 = row["AUG_AMT8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUG_AMT8"].ToString()),
                SEP_QTY9 = row["SEP_QTY9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SEP_QTY9"].ToString()),
                SEP_AMT9 = row["SEP_AMT9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SEP_AMT9"].ToString()),
                OCT_QTY10 = row["OCT_QTY10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["OCT_QTY10"].ToString()),
                OCT_AMT10 = row["OCT_AMT10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["OCT_AMT10"].ToString()),
                NOV_QTY11 = row["NOV_QTY11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["NOV_QTY11"].ToString()),
                NOV_AMT11 = row["NOV_AMT11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["NOV_AMT11"].ToString()),
                DEC_QTY12 = row["DEC_QTY12"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEC_QTY12"].ToString()),
                DEC_AMT12 = row["DEC_AMT12"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DEC_AMT12"].ToString()),
                TOT_SQTY = row["TOT_SQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOT_SQTY"].ToString()),
                TOT_SAMT = row["TOT_SAMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOT_SAMT"].ToString()),
                QTY_90 = row["QTY_90"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY_90"].ToString()),
                VAL_90 = row["VAL_90"] == DBNull.Value ? 0 : Convert.ToDecimal(row["VAL_90"].ToString()),
                QTY_120 = row["QTY_120"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY_120"].ToString()),
                VAL_120 = row["VAL_120"] == DBNull.Value ? 0 : Convert.ToDecimal(row["VAL_120"].ToString()),
                QTY_180 = row["QTY_180"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY_180"].ToString()),
                VAL_180 = row["VAL_180"] == DBNull.Value ? 0 : Convert.ToDecimal(row["VAL_180"].ToString()),
                QTY_270 = row["QTY_270"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY_270"].ToString()),
                VAL_270 = row["VAL_270"] == DBNull.Value ? 0 : Convert.ToDecimal(row["VAL_270"].ToString()),
                QTY_360 = row["QTY_360"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY_360"].ToString()),
                VAL_360 = row["VAL_360"] == DBNull.Value ? 0 : Convert.ToDecimal(row["VAL_360"].ToString()),
                QTY_361 = row["QTY_361"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY_361"].ToString()),
                VAL_361 = row["VAL_361"] == DBNull.Value ? 0 : Convert.ToDecimal(row["VAL_361"].ToString()),
                QTY_TOT = row["QTY_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY_TOT"].ToString()),
                VAL_TOT = row["VAL_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["VAL_TOT"].ToString())
            };
        }
    }
}

