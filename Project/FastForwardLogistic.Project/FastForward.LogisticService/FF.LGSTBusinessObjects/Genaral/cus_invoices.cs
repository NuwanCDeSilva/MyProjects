using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class cus_invoices
    {
        public String TIH_INV_NO { get; set; }
        public String TIH_MAN_REF_NO { get; set; }
        public Decimal TIH_TOT_AMT { get; set; }
        public DateTime TIH_INV_DT { get; set; }

        public static cus_invoices Converter(DataRow row)
        {
            return new cus_invoices
            {

                TIH_INV_NO = row["TIH_INV_NO"] == DBNull.Value ? string.Empty : row["TIH_INV_NO"].ToString(),
                TIH_MAN_REF_NO = row["TIH_MAN_REF_NO"] == DBNull.Value ? string.Empty : row["TIH_MAN_REF_NO"].ToString(),
                TIH_TOT_AMT = row["TIH_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TIH_TOT_AMT"].ToString()),
                TIH_INV_DT = row["TIH_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TIH_INV_DT"].ToString())
            };
        }
    }
}
