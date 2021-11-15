using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class MST_PAY_DET
    {
        public Int32 MPD_SEQ { get; set; }
        public String MPD_PAY_NO { get; set; }
        public String MPD_ACC_NO { get; set; }
        public Int32 MPD_AMT { get; set; }
        public Int32 MPD_STUS { get; set; }
        public String MPD_CRE_BY { get; set; }
        public DateTime MPD_CRE_DT { get; set; }
        public String MPD_MOD_BY { get; set; }
        public DateTime MPD_MOD_DT { get; set; }
        public Int32 MPD_ITM_LINE { get; set; }
        public static MST_PAY_DET Converter(DataRow row)
        {
            return new MST_PAY_DET
            {
                MPD_SEQ = row["MPD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPD_SEQ"].ToString()),
                MPD_PAY_NO = row["MPD_PAY_NO"] == DBNull.Value ? string.Empty : row["MPD_PAY_NO"].ToString(),
                MPD_ACC_NO = row["MPD_ACC_NO"] == DBNull.Value ? string.Empty : row["MPD_ACC_NO"].ToString(),
                MPD_AMT = row["MPD_AMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPD_AMT"].ToString()),
                MPD_STUS = row["MPD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPD_STUS"].ToString()),
                MPD_CRE_BY = row["MPD_CRE_BY"] == DBNull.Value ? string.Empty : row["MPD_CRE_BY"].ToString(),
                MPD_CRE_DT = row["MPD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPD_CRE_DT"].ToString()),
                MPD_MOD_BY = row["MPD_MOD_BY"] == DBNull.Value ? string.Empty : row["MPD_MOD_BY"].ToString(),
                MPD_MOD_DT = row["MPD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPD_MOD_DT"].ToString()),
                MPD_ITM_LINE = row["MPD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPD_ITM_LINE"].ToString())
            };
        }
    } 

}
