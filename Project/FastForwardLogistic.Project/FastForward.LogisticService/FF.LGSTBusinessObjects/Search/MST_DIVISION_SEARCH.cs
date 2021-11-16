using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

// Class added by Chathura on 20-sep-2017
namespace FF.BusinessObjects.Search
{
    public class MST_DIVISION_SEARCH
    {
        public String MSRD_CD { get; set; }
        public String MSRD_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static MST_DIVISION_SEARCH Converter(DataRow row)
        {
            return new MST_DIVISION_SEARCH
            {
                MSRD_CD = row["MSRD_CD"] == DBNull.Value ? string.Empty : row["MSRD_CD"].ToString(),
                MSRD_DESC = row["MSRD_DESC"] == DBNull.Value ? string.Empty : row["MSRD_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
