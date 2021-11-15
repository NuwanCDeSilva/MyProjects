using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{

    public class REF_OBJ_LIST_DET
    {
        public Int32 ROLD_ID { get; set; }
        public Int32 ROLD_REFOB_ID { get; set; }
        public String ROLD_NAME { get; set; }
        public String ROLD_CODE { get; set; }
        public Int32 ROLD_STUS { get; set; }
        public String ROLD_CRE_BY { get; set; }
        public DateTime ROLD_CRE_DT { get; set; }
        public String ROLD_MOD_BY { get; set; }
        public DateTime ROLD_MOD_DT { get; set; }
        public static REF_OBJ_LIST_DET Converter(DataRow row)
        {
            return new REF_OBJ_LIST_DET
            {
                ROLD_ID = row["ROLD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ROLD_ID"].ToString()),
                ROLD_REFOB_ID = row["ROLD_REFOB_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ROLD_REFOB_ID"].ToString()),
                ROLD_NAME = row["ROLD_NAME"] == DBNull.Value ? string.Empty : row["ROLD_NAME"].ToString(),
                ROLD_CODE = row["ROLD_CODE"] == DBNull.Value ? string.Empty : row["ROLD_CODE"].ToString(),
                ROLD_STUS = row["ROLD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["ROLD_STUS"].ToString()),
                ROLD_CRE_BY = row["ROLD_CRE_BY"] == DBNull.Value ? string.Empty : row["ROLD_CRE_BY"].ToString(),
                ROLD_CRE_DT = row["ROLD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ROLD_CRE_DT"].ToString()),
                ROLD_MOD_BY = row["ROLD_MOD_BY"] == DBNull.Value ? string.Empty : row["ROLD_MOD_BY"].ToString(),
                ROLD_MOD_DT = row["ROLD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ROLD_MOD_DT"].ToString())
            };
        }
    } 

}
