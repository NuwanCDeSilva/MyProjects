using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.General
{
    public class mst_busentity_customer
    {
        public String MBE_CD { get; set; }
        public String MBE_NAME { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static mst_busentity_customer Converter(DataRow row)
        {
            return new mst_busentity_customer
            {
                MBE_CD = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                MBE_NAME = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }


    }
}
