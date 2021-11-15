using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.General
{
    public class GetPCCategory
    {
        public string MSPH_TP { get; set; }

        public static GetPCCategory webConverter(DataRow row)
        {
            return new GetPCCategory
            {
                MSPH_TP = row["MSPH_TP"] == DBNull.Value ? string.Empty : row["MSPH_TP"].ToString()

            };
        }
    }
}
