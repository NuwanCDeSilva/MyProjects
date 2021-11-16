using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class InvoiceCom
    {
        public int HAS_DATE { get; set; }
        public int NUM_OF_BKDATES { get; set; }
        public DateTime INV_DATE { get; set; }

        public static InvoiceCom Converter(DataRow row)
        {
            return new InvoiceCom
            {
                HAS_DATE = row["HAS_DATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAS_DATE"].ToString()),
                NUM_OF_BKDATES = row["NUM_OF_BKDATES"] == DBNull.Value ? 0 : Convert.ToInt32(row["NUM_OF_BKDATES"].ToString())
            };
        }
        public static InvoiceCom ConverterToHasDate(DataRow row)
        {
            return new InvoiceCom
            {
                HAS_DATE = row["HAS_DATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAS_DATE"].ToString())
            };
        }
        public static InvoiceCom ConverterToBackDates(DataRow row)
        {
            return new InvoiceCom
            {
                NUM_OF_BKDATES = row["NUM_OF_BKDATES"] == DBNull.Value ? 0 : Convert.ToInt32(row["NUM_OF_BKDATES"].ToString())
            };
        }

        public static InvoiceCom ConverterToInvDate(DataRow row)
        {
            return new InvoiceCom
            {
                INV_DATE = row["INV_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INV_DATE"].ToString())
            };
        }

    }
}
