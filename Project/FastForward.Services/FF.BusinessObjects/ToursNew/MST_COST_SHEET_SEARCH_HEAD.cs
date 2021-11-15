using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_COST_SHEET_SEARCH_HEAD
    {
        public string QCH_COST_NO { get; set; }
        public string QCH_REF { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_COST_SHEET_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_COST_SHEET_SEARCH_HEAD
            {
                QCH_COST_NO = row["QCH_COST_NO"] == DBNull.Value ? string.Empty : row["QCH_COST_NO"].ToString(),
                QCH_REF = row["QCH_REF"] == DBNull.Value ? string.Empty : row["QCH_REF"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
