using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class USR_DEF_TEMP
    {
        public string UPD_CD { get; set; }
        public string UPD_VAL { get; set; }
        public string UPD_USR_ID { get; set; }
        public string UPD_DES { get; set; }
        public string UPD_KEY { get; set; }
        public string UPD_TMP_NAME { get; set; }
        public string R__ { get; set; }
        public string RESULT_COUNT { get; set; }
        public static USR_DEF_TEMP Converter(DataRow row)
        {
            return new USR_DEF_TEMP
            {
                UPD_TMP_NAME = row["UPD_TMP_NAME"] == DBNull.Value ? string.Empty : row["UPD_TMP_NAME"].ToString(),
             //   UPD_CD = row["UPD_CD"] == DBNull.Value ? string.Empty : row["UPD_CD"].ToString(),
             //   UPD_VAL = row["UPD_VAL"] == DBNull.Value ? string.Empty : row["UPD_VAL"].ToString(),
             //   UPD_USR_ID = row["UPD_USR_ID"] == DBNull.Value ? string.Empty : row["UPD_USR_ID"].ToString(),
                UPD_DES = row["UPD_DES"] == DBNull.Value ? string.Empty : row["UPD_DES"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString()
              //  UPD_KEY = row["UPD_KEY"] == DBNull.Value ? string.Empty : row["UPD_KEY"].ToString()
                //CNT = row["CNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["CNT"].ToString())
            };
        }
    }
}
