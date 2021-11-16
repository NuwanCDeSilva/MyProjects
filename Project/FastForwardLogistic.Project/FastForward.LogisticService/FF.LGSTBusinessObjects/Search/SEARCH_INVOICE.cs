using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class SEARCH_INVOICE
    {
       public string TIH_INV_NO { get; set; }
       public DateTime TIH_INV_DT { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public string JB_JB_NO { get; set; }

        public static SEARCH_INVOICE Converter(DataRow row)
        {
            return new SEARCH_INVOICE
            {
                TIH_INV_NO = row["TIH_INV_NO"] == DBNull.Value ? string.Empty : row["TIH_INV_NO"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                TIH_INV_DT = row["TIH_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TIH_INV_DT"].ToString()),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }

        public static SEARCH_INVOICE ConverterInvRef(DataRow row)
        {
            return new SEARCH_INVOICE
            {
                TIH_INV_NO = row["TIH_INV_NO"] == DBNull.Value ? string.Empty : row["TIH_INV_NO"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                TIH_INV_DT = row["TIH_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TIH_INV_DT"].ToString()),
                JB_JB_NO = row["JB_JB_NO"] == DBNull.Value ? string.Empty : row["JB_JB_NO"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
