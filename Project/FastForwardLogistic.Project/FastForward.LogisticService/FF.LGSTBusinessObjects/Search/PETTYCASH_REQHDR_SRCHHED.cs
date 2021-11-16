using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class PETTYCASH_REQHDR_SRCHHED
    {
            public String TPRH_REQ_NO { get; set; }
            public String TPRH_MANUAL_REF { get; set; }
            public DateTime TPRH_REQ_DT { get; set; }
            public string RESULT_COUNT { get; set; }
            public string R__ { get; set; }

            public static PETTYCASH_REQHDR_SRCHHED Converter(DataRow row)
            {
                return new PETTYCASH_REQHDR_SRCHHED
                {                    
                    TPRH_REQ_NO = row["TPRH_REQ_NO"] == DBNull.Value ? string.Empty : row["TPRH_REQ_NO"].ToString(),
                    TPRH_MANUAL_REF = row["TPRH_MANUAL_REF"] == DBNull.Value ? string.Empty : row["TPRH_MANUAL_REF"].ToString(),
                    TPRH_REQ_DT = row["TPRH_REQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_REQ_DT"].ToString()),
                    RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                    R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
                };
            }
        

    }
}
