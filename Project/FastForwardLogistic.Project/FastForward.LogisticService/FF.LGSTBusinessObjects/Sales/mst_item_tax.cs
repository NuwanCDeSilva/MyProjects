using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class mst_item_tax
    {
        public string CHA_CODE { get; set; }
        public string TAX_TYPE_CODE { get; set; }
        public string TAX_RATE_CODE { get; set; }
        public decimal TAX_RATE{ get; set; }
        public int ACTIVE { get; set; }
        public decimal BASED_ON { get; set; }
        public DateTime FROM_DATE { get; set; }
        public DateTime TO_DATE { get; set; }
        public string TOC { get; set; }
        public string COMPANY_CODE { get; set; }

        public static mst_item_tax Converter(DataRow row)
        {
            return new mst_item_tax
            {
                CHA_CODE = row["CHA_CODE"] == DBNull.Value ? string.Empty : row["CHA_CODE"].ToString(),
                TAX_TYPE_CODE = row["TAX_TYPE_CODE"] == DBNull.Value ? string.Empty : row["TAX_TYPE_CODE"].ToString(),
                TAX_RATE_CODE = row["TAX_RATE_CODE"] == DBNull.Value ? string.Empty : row["TAX_RATE_CODE"].ToString(),
                TAX_RATE = row["TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TAX_RATE"].ToString()),
                ACTIVE = row["ACTIVE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACTIVE"].ToString()),
                BASED_ON = row["BASED_ON"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BASED_ON"].ToString()),
                FROM_DATE = row["FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FROM_DATE"].ToString()),
                TO_DATE = row["TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TO_DATE"].ToString()),
                TOC = row["TOC"] == DBNull.Value ? string.Empty : row["TOC"].ToString(),
                COMPANY_CODE = row["COMPANY_CODE"] == DBNull.Value ? string.Empty : row["COMPANY_CODE"].ToString()
            };
        }

    }
}
