using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class BOIProc
    {
        public string duty { get; set; }
        public Int32 mp { get; set; }
        public Int32 act { get; set; }
        public string procd { get; set; }

        public static BOIProc Converter(DataRow row)
        {
            return new BOIProc
            {
                duty = row["duty"] == DBNull.Value ? string.Empty : row["duty"].ToString(),
                mp = row["mp"] == DBNull.Value ? 0 : Convert.ToInt32(row["mp"].ToString()),
                act = row["act"] == DBNull.Value ? 0 : Convert.ToInt32(row["act"].ToString()),

            };
        }
    }
}
