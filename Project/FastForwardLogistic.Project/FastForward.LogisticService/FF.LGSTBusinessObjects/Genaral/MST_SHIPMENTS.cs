using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class MST_SHIPMENTS
    {
        public string msc_com { get; set; }

        public string msc_cd { get; set; }

        public string msc_desc { get; set; }

        public string msc_act { get; set; }


        public static MST_SHIPMENTS Converter(DataRow row)
        {
            return new MST_SHIPMENTS
            {
                msc_com = row["msc_com"] == DBNull.Value ? string.Empty : row["msc_com"].ToString(),
                msc_cd = row["msc_cd"] == DBNull.Value ? string.Empty : row["msc_cd"].ToString(),
                msc_desc = row["msc_desc"] == DBNull.Value ? string.Empty : row["msc_desc"].ToString(),
                msc_act = row["msc_act"] == DBNull.Value ? string.Empty : row["msc_act"].ToString()

            };
        }
    }
}
