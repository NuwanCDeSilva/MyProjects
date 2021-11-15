using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Account
{
    public class ACCOUNT_TAX
    {
        public string TAX_CD { get; set; }
        public string TAX_DESC { get; set; }
        public decimal TAX_RATE { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static ACCOUNT_TAX Converter(DataRow row)
        {
            return new ACCOUNT_TAX
            {
                TAX_CD = row["TAX_CD"] == DBNull.Value ? string.Empty : row["TAX_CD"].ToString(),
                TAX_DESC = row["TAX_DESC"] == DBNull.Value ? string.Empty : row["TAX_DESC"].ToString(),
                TAX_RATE = row["TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TAX_RATE"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
