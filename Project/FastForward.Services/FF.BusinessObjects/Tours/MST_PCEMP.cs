using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MST_PCEMP
    {
        public String MPE_EPF { get; set; }
        public String MPE_COM { get; set; }
        public String MPE_PC { get; set; }
        public DateTime MPE_ASSN_DT { get; set; }
        public String MPE_REP_CD { get; set; }
        public String MPE_ANAL_1 { get; set; }
        public String MPE_ANAL_2 { get; set; }
        public String MPE_ANAL_3 { get; set; }
        public Int32 MPE_ACT { get; set; }
        public static MST_PCEMP Converter(DataRow row)
        {
            return new MST_PCEMP
            {
                MPE_EPF = row["MPE_EPF"] == DBNull.Value ? string.Empty : row["MPE_EPF"].ToString(),
                MPE_COM = row["MPE_COM"] == DBNull.Value ? string.Empty : row["MPE_COM"].ToString(),
                MPE_PC = row["MPE_PC"] == DBNull.Value ? string.Empty : row["MPE_PC"].ToString(),
                MPE_ASSN_DT = row["MPE_ASSN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPE_ASSN_DT"].ToString()),
                MPE_REP_CD = row["MPE_REP_CD"] == DBNull.Value ? string.Empty : row["MPE_REP_CD"].ToString(),
                MPE_ANAL_1 = row["MPE_ANAL_1"] == DBNull.Value ? string.Empty : row["MPE_ANAL_1"].ToString(),
                MPE_ANAL_2 = row["MPE_ANAL_2"] == DBNull.Value ? string.Empty : row["MPE_ANAL_2"].ToString(),
                MPE_ANAL_3 = row["MPE_ANAL_3"] == DBNull.Value ? string.Empty : row["MPE_ANAL_3"].ToString(),
                MPE_ACT = row["MPE_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPE_ACT"].ToString())
            };
        }
    }
}
