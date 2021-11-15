using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hero_Service_Consol.DTOs
{
    public class INR_RES_LOG
    {
        public Int32 IRL_SEQ { get; set; }
        public String IRL_RES_NO { get; set; }
        public Int32 IRL_RES_LINE { get; set; }
        public Int32 IRL_LINE { get; set; }
        public String IRL_ITM_CD { get; set; }
        public String IRL_ITM_STUS { get; set; }
        public Decimal IRL_RES_QTY { get; set; }
        public Decimal IRL_RES_BQTY { get; set; }
        public Decimal TMP_IRL_RES_BQTY { get; set; }
        public Decimal IRL_RES_IQTY { get; set; }
        public String IRL_ORIG_DOC_TP { get; set; }
        public String IRL_ORIG_DOC_NO { get; set; }
        public DateTime IRL_ORIG_DOC_DT { get; set; }
        public Int32 IRL_ORIG_ITM_LINE { get; set; }
        public Int32 IRL_ORIG_BATCH_LINE { get; set; }
        public String IRL_ORIG_COM { get; set; }
        public String IRL_ORIG_LOC { get; set; }
        public String IRL_CURT_DOC_TP { get; set; }
        public String IRL_CURT_DOC_NO { get; set; }
        public DateTime IRL_CURT_DOC_DT { get; set; }
        public Int32 IRL_CURT_ITM_LINE { get; set; }
        public Int32 IRL_CURT_BATCH_LINE { get; set; }
        public String IRL_CURT_COM { get; set; }
        public String IRL_CURT_LOC { get; set; }
        public Int32 IRL_BASE_LINE { get; set; }
        public Int32 IRL_ACT { get; set; }
        public String IRL_CRE_BY { get; set; }
        public DateTime IRL_CRE_DT { get; set; }
        public String IRL_CRE_SESSION { get; set; }
        public DateTime IRL_MOD_BY { get; set; }

        public DateTime IRL_MOD_DT { get; set; }
        public String IRL_MOD_SESSION { get; set; }

        public Decimal IRL_CAN_QTY { get; set; } //rukshan

        public String BL_NO { get; set; }
        public String LOC_CD { get; set; }//15/mar/2016

        public string IRL_MOD_BY_NEW { get; set; }
        public Int32 IRL_RES_WP { get; set; } // Add by Lakshan 04 Nov 2016

        public string Temp_IRL_MOD_BY { get; set; }//Add by Lakshan

        public Decimal IRL_RES_CQTY { get; set; } // Add by Randima
        public string IRL_REQ_NO { get; set; }  // Add by Randima
        public string IRL_STUS_DESC { get; set; } //Add by Randima
        public string TMP_AOD_IN_LOC { get; set; }
        public string TMP_AOD_IN_COM { get; set; }
        public static INR_RES_LOG Converter(DataRow row)
        {
            return new INR_RES_LOG
            {
                IRL_SEQ = row["IRL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_SEQ"].ToString()),
                IRL_RES_NO = row["IRL_RES_NO"] == DBNull.Value ? string.Empty : row["IRL_RES_NO"].ToString(),
                IRL_RES_LINE = row["IRL_RES_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_RES_LINE"].ToString()),
                IRL_LINE = row["IRL_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_LINE"].ToString()),
                IRL_ITM_CD = row["IRL_ITM_CD"] == DBNull.Value ? string.Empty : row["IRL_ITM_CD"].ToString(),
                IRL_ITM_STUS = row["IRL_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRL_ITM_STUS"].ToString(),
                IRL_RES_QTY = row["IRL_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRL_RES_QTY"].ToString()),
                IRL_RES_BQTY = row["IRL_RES_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRL_RES_BQTY"].ToString()),
                IRL_RES_IQTY = row["IRL_RES_IQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRL_RES_IQTY"].ToString()),
                IRL_ORIG_DOC_TP = row["IRL_ORIG_DOC_TP"] == DBNull.Value ? string.Empty : row["IRL_ORIG_DOC_TP"].ToString(),
                IRL_ORIG_DOC_NO = row["IRL_ORIG_DOC_NO"] == DBNull.Value ? string.Empty : row["IRL_ORIG_DOC_NO"].ToString(),
                IRL_ORIG_DOC_DT = row["IRL_ORIG_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_ORIG_DOC_DT"].ToString()),
                IRL_ORIG_ITM_LINE = row["IRL_ORIG_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_ORIG_ITM_LINE"].ToString()),
                IRL_ORIG_BATCH_LINE = row["IRL_ORIG_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_ORIG_BATCH_LINE"].ToString()),
                IRL_ORIG_COM = row["IRL_ORIG_COM"] == DBNull.Value ? string.Empty : row["IRL_ORIG_COM"].ToString(),
                IRL_ORIG_LOC = row["IRL_ORIG_LOC"] == DBNull.Value ? string.Empty : row["IRL_ORIG_LOC"].ToString(),
                IRL_CURT_DOC_TP = row["IRL_CURT_DOC_TP"] == DBNull.Value ? string.Empty : row["IRL_CURT_DOC_TP"].ToString(),
                IRL_CURT_DOC_NO = row["IRL_CURT_DOC_NO"] == DBNull.Value ? string.Empty : row["IRL_CURT_DOC_NO"].ToString(),
                IRL_CURT_DOC_DT = row["IRL_CURT_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_CURT_DOC_DT"].ToString()),
                IRL_CURT_ITM_LINE = row["IRL_CURT_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_CURT_ITM_LINE"].ToString()),
                IRL_CURT_BATCH_LINE = row["IRL_CURT_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_CURT_BATCH_LINE"].ToString()),
                IRL_CURT_COM = row["IRL_CURT_COM"] == DBNull.Value ? string.Empty : row["IRL_CURT_COM"].ToString(),
                IRL_CURT_LOC = row["IRL_CURT_LOC"] == DBNull.Value ? string.Empty : row["IRL_CURT_LOC"].ToString(),
                IRL_BASE_LINE = row["IRL_BASE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_BASE_LINE"].ToString()),
                IRL_ACT = row["IRL_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_ACT"].ToString()),
                IRL_CRE_BY = row["IRL_CRE_BY"] == DBNull.Value ? string.Empty : row["IRL_CRE_BY"].ToString(),
                IRL_CRE_DT = row["IRL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_CRE_DT"].ToString()),
                IRL_CRE_SESSION = row["IRL_CRE_SESSION"] == DBNull.Value ? string.Empty : row["IRL_CRE_SESSION"].ToString(),
                IRL_MOD_BY = row["IRL_MOD_BY"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_MOD_BY"].ToString()),
                IRL_MOD_DT = row["IRL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_MOD_DT"].ToString()),
                IRL_MOD_SESSION = row["IRL_MOD_SESSION"] == DBNull.Value ? string.Empty : row["IRL_MOD_SESSION"].ToString()
            };
        }
        //Add by Lakshan 20 oct 2016
        public static INR_RES_LOG ConverterNew(DataRow row)
        {
            return new INR_RES_LOG
            {
                IRL_SEQ = row["IRL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_SEQ"].ToString()),
                IRL_RES_NO = row["IRL_RES_NO"] == DBNull.Value ? string.Empty : row["IRL_RES_NO"].ToString(),
                IRL_RES_LINE = row["IRL_RES_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_RES_LINE"].ToString()),
                IRL_LINE = row["IRL_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_LINE"].ToString()),
                IRL_ITM_CD = row["IRL_ITM_CD"] == DBNull.Value ? string.Empty : row["IRL_ITM_CD"].ToString(),
                IRL_ITM_STUS = row["IRL_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRL_ITM_STUS"].ToString(),
                IRL_RES_QTY = row["IRL_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRL_RES_QTY"].ToString()),
                IRL_RES_BQTY = row["IRL_RES_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRL_RES_BQTY"].ToString()),
                IRL_RES_IQTY = row["IRL_RES_IQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRL_RES_IQTY"].ToString()),
                IRL_ORIG_DOC_TP = row["IRL_ORIG_DOC_TP"] == DBNull.Value ? string.Empty : row["IRL_ORIG_DOC_TP"].ToString(),
                IRL_ORIG_DOC_NO = row["IRL_ORIG_DOC_NO"] == DBNull.Value ? string.Empty : row["IRL_ORIG_DOC_NO"].ToString(),
                IRL_ORIG_DOC_DT = row["IRL_ORIG_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_ORIG_DOC_DT"].ToString()),
                IRL_ORIG_ITM_LINE = row["IRL_ORIG_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_ORIG_ITM_LINE"].ToString()),
                IRL_ORIG_BATCH_LINE = row["IRL_ORIG_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_ORIG_BATCH_LINE"].ToString()),
                IRL_ORIG_COM = row["IRL_ORIG_COM"] == DBNull.Value ? string.Empty : row["IRL_ORIG_COM"].ToString(),
                IRL_ORIG_LOC = row["IRL_ORIG_LOC"] == DBNull.Value ? string.Empty : row["IRL_ORIG_LOC"].ToString(),
                IRL_CURT_DOC_TP = row["IRL_CURT_DOC_TP"] == DBNull.Value ? string.Empty : row["IRL_CURT_DOC_TP"].ToString(),
                IRL_CURT_DOC_NO = row["IRL_CURT_DOC_NO"] == DBNull.Value ? string.Empty : row["IRL_CURT_DOC_NO"].ToString(),
                IRL_CURT_DOC_DT = row["IRL_CURT_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_CURT_DOC_DT"].ToString()),
                IRL_CURT_ITM_LINE = row["IRL_CURT_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_CURT_ITM_LINE"].ToString()),
                IRL_CURT_BATCH_LINE = row["IRL_CURT_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_CURT_BATCH_LINE"].ToString()),
                IRL_CURT_COM = row["IRL_CURT_COM"] == DBNull.Value ? string.Empty : row["IRL_CURT_COM"].ToString(),
                IRL_CURT_LOC = row["IRL_CURT_LOC"] == DBNull.Value ? string.Empty : row["IRL_CURT_LOC"].ToString(),
                IRL_BASE_LINE = row["IRL_BASE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_BASE_LINE"].ToString()),
                IRL_ACT = row["IRL_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_ACT"].ToString()),
                IRL_CRE_BY = row["IRL_CRE_BY"] == DBNull.Value ? string.Empty : row["IRL_CRE_BY"].ToString(),
                IRL_CRE_DT = row["IRL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_CRE_DT"].ToString()),
                IRL_CRE_SESSION = row["IRL_CRE_SESSION"] == DBNull.Value ? string.Empty : row["IRL_CRE_SESSION"].ToString(),
                IRL_MOD_BY_NEW = row["IRL_MOD_BY"] == DBNull.Value ? string.Empty : Convert.ToString(row["IRL_MOD_BY"].ToString()),
                IRL_MOD_DT = row["IRL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_MOD_DT"].ToString()),
                IRL_MOD_SESSION = row["IRL_MOD_SESSION"] == DBNull.Value ? string.Empty : row["IRL_MOD_SESSION"].ToString()
            };
        }
        //Add by Lakshan 20 oct 2016
        public static INR_RES_LOG ConvertAllData(DataRow row)
        {
            return new INR_RES_LOG
            {
                IRL_SEQ = row["IRL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_SEQ"].ToString()),
                IRL_RES_NO = row["IRL_RES_NO"] == DBNull.Value ? string.Empty : row["IRL_RES_NO"].ToString(),
                IRL_RES_LINE = row["IRL_RES_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_RES_LINE"].ToString()),
                IRL_LINE = row["IRL_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_LINE"].ToString()),
                IRL_ITM_CD = row["IRL_ITM_CD"] == DBNull.Value ? string.Empty : row["IRL_ITM_CD"].ToString(),
                IRL_ITM_STUS = row["IRL_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRL_ITM_STUS"].ToString(),
                IRL_RES_QTY = row["IRL_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRL_RES_QTY"].ToString()),
                IRL_RES_BQTY = row["IRL_RES_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRL_RES_BQTY"].ToString()),
                IRL_RES_IQTY = row["IRL_RES_IQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRL_RES_IQTY"].ToString()),
                IRL_ORIG_DOC_TP = row["IRL_ORIG_DOC_TP"] == DBNull.Value ? string.Empty : row["IRL_ORIG_DOC_TP"].ToString(),
                IRL_ORIG_DOC_NO = row["IRL_ORIG_DOC_NO"] == DBNull.Value ? string.Empty : row["IRL_ORIG_DOC_NO"].ToString(),
                IRL_ORIG_DOC_DT = row["IRL_ORIG_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_ORIG_DOC_DT"].ToString()),
                IRL_ORIG_ITM_LINE = row["IRL_ORIG_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_ORIG_ITM_LINE"].ToString()),
                IRL_ORIG_BATCH_LINE = row["IRL_ORIG_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_ORIG_BATCH_LINE"].ToString()),
                IRL_ORIG_COM = row["IRL_ORIG_COM"] == DBNull.Value ? string.Empty : row["IRL_ORIG_COM"].ToString(),
                IRL_ORIG_LOC = row["IRL_ORIG_LOC"] == DBNull.Value ? string.Empty : row["IRL_ORIG_LOC"].ToString(),
                IRL_CURT_DOC_TP = row["IRL_CURT_DOC_TP"] == DBNull.Value ? string.Empty : row["IRL_CURT_DOC_TP"].ToString(),
                IRL_CURT_DOC_NO = row["IRL_CURT_DOC_NO"] == DBNull.Value ? string.Empty : row["IRL_CURT_DOC_NO"].ToString(),
                IRL_CURT_DOC_DT = row["IRL_CURT_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_CURT_DOC_DT"].ToString()),
                IRL_CURT_ITM_LINE = row["IRL_CURT_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_CURT_ITM_LINE"].ToString()),
                IRL_CURT_BATCH_LINE = row["IRL_CURT_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_CURT_BATCH_LINE"].ToString()),
                IRL_CURT_COM = row["IRL_CURT_COM"] == DBNull.Value ? string.Empty : row["IRL_CURT_COM"].ToString(),
                IRL_CURT_LOC = row["IRL_CURT_LOC"] == DBNull.Value ? string.Empty : row["IRL_CURT_LOC"].ToString(),
                IRL_BASE_LINE = row["IRL_BASE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_BASE_LINE"].ToString()),
                IRL_ACT = row["IRL_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_ACT"].ToString()),
                IRL_CRE_BY = row["IRL_CRE_BY"] == DBNull.Value ? string.Empty : row["IRL_CRE_BY"].ToString(),
                IRL_CRE_DT = row["IRL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_CRE_DT"].ToString()),
                IRL_CRE_SESSION = row["IRL_CRE_SESSION"] == DBNull.Value ? string.Empty : row["IRL_CRE_SESSION"].ToString(),
                IRL_MOD_BY_NEW = row["IRL_MOD_BY"] == DBNull.Value ? string.Empty : Convert.ToString(row["IRL_MOD_BY"].ToString()),
                IRL_MOD_DT = row["IRL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRL_MOD_DT"].ToString()),
                IRL_MOD_SESSION = row["IRL_MOD_SESSION"] == DBNull.Value ? string.Empty : row["IRL_MOD_SESSION"].ToString(),
                IRL_RES_WP = row["IRL_RES_WP"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRL_RES_WP"].ToString())
            };
        }
        public static INR_RES_LOG CreateNewObject(INR_RES_LOG _obj)
        {
            INR_RES_LOG _newObj = new INR_RES_LOG();
            _newObj.IRL_SEQ = _obj.IRL_SEQ;
            _newObj.IRL_RES_NO = _obj.IRL_RES_NO;
            _newObj.IRL_RES_LINE = _obj.IRL_RES_LINE;
            _newObj.IRL_LINE = _obj.IRL_LINE;
            _newObj.IRL_ITM_CD = _obj.IRL_ITM_CD;
            _newObj.IRL_ITM_STUS = _obj.IRL_ITM_STUS;
            _newObj.IRL_RES_QTY = _obj.IRL_RES_QTY;
            _newObj.IRL_RES_BQTY = _obj.IRL_RES_BQTY;
            _newObj.TMP_IRL_RES_BQTY = _obj.TMP_IRL_RES_BQTY;
            _newObj.IRL_RES_IQTY = _obj.IRL_RES_IQTY;
            _newObj.IRL_ORIG_DOC_TP = _obj.IRL_ORIG_DOC_TP;
            _newObj.IRL_ORIG_DOC_NO = _obj.IRL_ORIG_DOC_NO;
            _newObj.IRL_ORIG_DOC_DT = _obj.IRL_ORIG_DOC_DT;
            _newObj.IRL_ORIG_ITM_LINE = _obj.IRL_ORIG_ITM_LINE;
            _newObj.IRL_ORIG_BATCH_LINE = _obj.IRL_ORIG_BATCH_LINE;
            _newObj.IRL_ORIG_COM = _obj.IRL_ORIG_COM;
            _newObj.IRL_ORIG_LOC = _obj.IRL_ORIG_LOC;
            _newObj.IRL_CURT_DOC_TP = _obj.IRL_CURT_DOC_TP;
            _newObj.IRL_CURT_DOC_NO = _obj.IRL_CURT_DOC_NO;
            _newObj.IRL_CURT_DOC_DT = _obj.IRL_CURT_DOC_DT;
            _newObj.IRL_CURT_ITM_LINE = _obj.IRL_CURT_ITM_LINE;
            _newObj.IRL_CURT_BATCH_LINE = _obj.IRL_CURT_BATCH_LINE;
            _newObj.IRL_CURT_COM = _obj.IRL_CURT_COM;
            _newObj.IRL_CURT_LOC = _obj.IRL_CURT_LOC;
            _newObj.IRL_BASE_LINE = _obj.IRL_BASE_LINE;
            _newObj.IRL_ACT = _obj.IRL_ACT;
            _newObj.IRL_CRE_BY = _obj.IRL_CRE_BY;
            _newObj.IRL_CRE_DT = _obj.IRL_CRE_DT;
            _newObj.IRL_CRE_SESSION = _obj.IRL_CRE_SESSION;
            _newObj.IRL_MOD_BY = _obj.IRL_MOD_BY;
            _newObj.IRL_MOD_DT = _obj.IRL_MOD_DT;
            _newObj.IRL_MOD_SESSION = _obj.IRL_MOD_SESSION;
            _newObj.IRL_CAN_QTY = _obj.IRL_CAN_QTY;
            _newObj.BL_NO = _obj.BL_NO;
            _newObj.LOC_CD = _obj.LOC_CD;
            _newObj.IRL_MOD_BY_NEW = _obj.IRL_MOD_BY_NEW;
            _newObj.IRL_RES_WP = _obj.IRL_RES_WP;
            _newObj.Temp_IRL_MOD_BY = _obj.Temp_IRL_MOD_BY;
            _newObj.IRL_RES_CQTY = _obj.IRL_RES_CQTY;
            _newObj.IRL_REQ_NO = _obj.IRL_REQ_NO;
            _newObj.IRL_STUS_DESC = _obj.IRL_STUS_DESC;
            _newObj.TMP_AOD_IN_LOC = _obj.TMP_AOD_IN_LOC;
            _newObj.TMP_AOD_IN_COM = _obj.TMP_AOD_IN_COM;
            return _newObj;
        }
    }
}
