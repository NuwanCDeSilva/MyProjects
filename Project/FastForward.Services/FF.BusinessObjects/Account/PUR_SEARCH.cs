using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Account
{
    public class PUR_SEARCH
    {
        public string PURNO { get; set; }
        public decimal COST { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static PUR_SEARCH Converter(DataRow row)
        {
            return new PUR_SEARCH
            {
                PURNO = row["PURNO"] == DBNull.Value ? string.Empty : row["PURNO"].ToString(),
                COST = row["COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["COST"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
       
    }
    public class PUR_SELECTED
    {
        public string PURNO { get; set; }
        public decimal COST { get; set; }
        public decimal TAX { get; set; }
    }
}
