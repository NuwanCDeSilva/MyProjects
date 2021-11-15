using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Ref_Title
    {
        public String RTIT_CD { get; set; }
        public String RTIT_DISP { get; set; }
        public static Ref_Title Converter(DataRow row)
        {
            return new Ref_Title
            {
                RTIT_CD = row["RTIT_CD"] == DBNull.Value ? string.Empty : row["RTIT_CD"].ToString(),
                RTIT_DISP = row["RTIT_DISP"] == DBNull.Value ? string.Empty : row["RTIT_DISP"].ToString()
            };
        }
    }
}
