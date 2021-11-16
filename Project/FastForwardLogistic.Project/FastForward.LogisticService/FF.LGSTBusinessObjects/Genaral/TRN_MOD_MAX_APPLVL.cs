using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class TRN_MOD_MAX_APPLVL
    {
        public Int32 TMAL_SEQ { get; set; }
        public String TMAL_MODULE { get; set; }
        public Int32 TMAL_MAX_APPLVL { get; set; }
        public String TMAL_CRE_BY { get; set; }
        public String TMAL_MOD_BY { get; set; }
        public DateTime TMAL_MOD_DT { get; set; }
        public DateTime TMAL_CRE_DT { get; set; }
        public String TMAL_COM { get; set; }
        public String TMAL_PC { get; set; }
        public static TRN_MOD_MAX_APPLVL Converter(DataRow row)
        {
            return new TRN_MOD_MAX_APPLVL
            {
                TMAL_SEQ = row["TMAL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["TMAL_SEQ"].ToString()),
                TMAL_MODULE = row["TMAL_MODULE"] == DBNull.Value ? string.Empty : row["TMAL_MODULE"].ToString(),
                TMAL_MAX_APPLVL = row["TMAL_MAX_APPLVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["TMAL_MAX_APPLVL"].ToString()),
                TMAL_CRE_BY = row["TMAL_CRE_BY"] == DBNull.Value ? string.Empty : row["TMAL_CRE_BY"].ToString(),
                TMAL_MOD_BY = row["TMAL_MOD_BY"] == DBNull.Value ? string.Empty : row["TMAL_MOD_BY"].ToString(),
                TMAL_MOD_DT = row["TMAL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TMAL_MOD_DT"]),
                TMAL_CRE_DT = row["TMAL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TMAL_CRE_DT"]),
                TMAL_COM = row["TMAL_COM"] == DBNull.Value ? string.Empty : row["TMAL_COM"].ToString(),
                TMAL_PC = row["TMAL_PC"] == DBNull.Value ? string.Empty : row["TMAL_PC"].ToString()
              };
        }
    }
}
