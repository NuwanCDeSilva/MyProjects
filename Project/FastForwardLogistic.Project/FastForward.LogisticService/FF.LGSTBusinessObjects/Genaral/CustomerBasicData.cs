using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class CustomerBasicData
    {
        public string MBE_CD { get; set; }
        public string MBE_NAME { get; set; }
        public string MBE_ADD1 { get; set; }
        public string MBE_ADD2 { get; set; }

        public static CustomerBasicData Converter(DataRow row)
        {
            return new CustomerBasicData
            {
                MBE_CD = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                MBE_NAME = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString()
            };
        }

        public static CustomerBasicData ConverterCusDetails(DataRow row)
        {
            return new CustomerBasicData
            {
                MBE_CD = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                MBE_NAME = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                MBE_ADD1 = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                MBE_ADD2 = row["MBE_ADD2"] == DBNull.Value ? string.Empty : row["MBE_ADD2"].ToString()
            };
        }
    }
}
