using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class REF_TMPLT_ITM_VALUE
    {
        public Int32 RTIV_ID { get; set; }
        public Int32 RTIV_HED_ID { get; set; }
        public Int32 RTIV_DET_ID { get; set; }
        public String RTIV_UNQ_CD { get; set; }
        public String RTIV_COM { get; set; }
        public String RTIV_LOC { get; set; }
        public String RTIV_PC { get; set; }
        public String RTIV_ANAL1 { get; set; }
        public String RTIV_ANAL2 { get; set; }
        public Int32 RTIV_ANAL3 { get; set; }
        public Int32 RTIV_ANAL4 { get; set; }
        public Int32 RTIV_STUS { get; set; }
        public Int32 RTIV_SEQ { get; set; }
        public String RTIV_VALUE { get; set; }
        public String RTIV_CRE_BY { get; set; }
        public DateTime RTIV_CRE_DT { get; set; }
        public String RTIV_MOD_BY { get; set; }
        public DateTime RTIV_MOD_DT { get; set; }
        public string RTIV_MODULE { get; set; }
        public Int32 RTIV_DIRECT { get; set; }
        public static REF_TMPLT_ITM_VALUE Converter(DataRow row)
        {
            return new REF_TMPLT_ITM_VALUE
            {
                RTIV_ID = row["RTIV_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_ID"].ToString()),
                RTIV_HED_ID = row["RTIV_HED_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_HED_ID"].ToString()),
                RTIV_DET_ID = row["RTIV_DET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_DET_ID"].ToString()),
                RTIV_UNQ_CD = row["RTIV_UNQ_CD"] == DBNull.Value ? string.Empty : row["RTIV_UNQ_CD"].ToString(),
                RTIV_COM = row["RTIV_COM"] == DBNull.Value ? string.Empty : row["RTIV_COM"].ToString(),
                RTIV_LOC = row["RTIV_LOC"] == DBNull.Value ? string.Empty : row["RTIV_LOC"].ToString(),
                RTIV_PC = row["RTIV_PC"] == DBNull.Value ? string.Empty : row["RTIV_PC"].ToString(),
                RTIV_ANAL1 = row["RTIV_ANAL1"] == DBNull.Value ? string.Empty : row["RTIV_ANAL1"].ToString(),
                RTIV_ANAL2 = row["RTIV_ANAL2"] == DBNull.Value ? string.Empty : row["RTIV_ANAL2"].ToString(),
                RTIV_ANAL3 = row["RTIV_ANAL3"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_ANAL3"].ToString()),
                RTIV_ANAL4 = row["RTIV_ANAL4"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_ANAL4"].ToString()),
                RTIV_STUS = row["RTIV_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_STUS"].ToString()),
                RTIV_SEQ = row["RTIV_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_SEQ"].ToString()),
                RTIV_VALUE = row["RTIV_VALUE"] == DBNull.Value ? string.Empty : row["RTIV_VALUE"].ToString(),
                RTIV_CRE_BY = row["RTIV_CRE_BY"] == DBNull.Value ? string.Empty : row["RTIV_CRE_BY"].ToString(),
                RTIV_CRE_DT = row["RTIV_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTIV_CRE_DT"].ToString()),
                RTIV_MOD_BY = row["RTIV_MOD_BY"] == DBNull.Value ? string.Empty : row["RTIV_MOD_BY"].ToString(),
                RTIV_MOD_DT = row["RTIV_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTIV_MOD_DT"].ToString()),
                RTIV_MODULE = row["RTIV_MODULE"] == DBNull.Value ? string.Empty : row["RTIV_MODULE"].ToString(),
                RTIV_DIRECT = row["RTIV_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_DIRECT"].ToString())
            };
        }


    }
    public class REF_TMPLT_ITM_VALUE_TABLE
    {
        public Int32 RTIV_ID { get; set; }
        public Int32 RTIV_HED_ID { get; set; }
        public Int32 RTIV_DET_ID { get; set; }
        public String RTIV_UNQ_CD { get; set; }
        public String RTIV_COM { get; set; }
        public Int32 RTIV_SEQ { get; set; }
        public String RTIV_VALUE { get; set; }
        public string RTIV_MODULE { get; set; }
        public Int32 RTIV_STUS { get; set; }
        public string RO_TYPE { get; set; }
        public Int32 DEF_VAL_FLD { get; set; }
        public Int32 RTIV_DIRECT { get; set; }
        public static REF_TMPLT_ITM_VALUE_TABLE Converter(DataRow row)
        {
            return new REF_TMPLT_ITM_VALUE_TABLE
            {
                RTIV_ID = row["RTIV_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_ID"].ToString()),
                RTIV_HED_ID = row["RTIV_HED_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_HED_ID"].ToString()),
                RTIV_DET_ID = row["RTIV_DET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_DET_ID"].ToString()),
                RTIV_UNQ_CD = row["RTIV_UNQ_CD"] == DBNull.Value ? string.Empty : row["RTIV_UNQ_CD"].ToString(),
                RTIV_COM = row["RTIV_COM"] == DBNull.Value ? string.Empty : row["RTIV_COM"].ToString(),
                RTIV_SEQ = row["RTIV_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_SEQ"].ToString()),
                RTIV_VALUE = row["RTIV_VALUE"] == DBNull.Value ? string.Empty : row["RTIV_VALUE"].ToString(),
                RTIV_MODULE = row["RTIV_MODULE"] == DBNull.Value ? string.Empty : row["RTIV_MODULE"].ToString(),
                RTIV_STUS = row["RTIV_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_STUS"].ToString()),
                RO_TYPE = row["RO_TYPE"] == DBNull.Value ? string.Empty : row["RO_TYPE"].ToString(),
                DEF_VAL_FLD = row["RTD_IS_VALUE"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_IS_VALUE"].ToString()),
                RTIV_DIRECT = row["RTIV_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTIV_DIRECT"].ToString())
            };
        }
    }
    public class VALUE_ITM_LIST
    {
        public Int32 RTIV_SEQ { get; set; }
        public Int32 STUS { get; set; }
        public string UNQ_VAL { get; set; }
        public Int32 IS_UPDATED { get; set; }
        public Int32 NEW_ADDED { get; set; }
        public Int32 RTIV_DIRECT { get; set; }
        public List<REF_TMPLT_ITM_VALUE_TABLE> ITEMS { get; set; }
    }
    public class TABLE_HED {
        public string HEADER { get; set; }
        public string TYPE { get; set; }
        public static TABLE_HED Converter(DataRow row)
        {
            return new TABLE_HED
            {
                HEADER = row["HEADER"] == DBNull.Value ? string.Empty :  row["HEADER"].ToString(),
                TYPE = row["TYPE"] == DBNull.Value ? string.Empty : row["TYPE"].ToString()
            };
        }
    }
}
