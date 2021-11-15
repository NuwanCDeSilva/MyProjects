using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class BankBinCodes
    {
        public string mbb_bin_cd { get; set; }
        public string mbb_ica { get; set; }
        public string mbb_bnk_cd { get; set; }
        public string mbb_crd_tp { get; set; }

        public static BankBinCodes Converter(DataRow row)
        {
            return new BankBinCodes
            {
                mbb_bin_cd = row["mbb_bin_cd"] == DBNull.Value ? string.Empty : row["mbb_bin_cd"].ToString(),
                mbb_ica = row["mbb_ica"] == DBNull.Value ? string.Empty : row["mbb_ica"].ToString(),
                mbb_bnk_cd = row["mbb_bnk_cd"] == DBNull.Value ? string.Empty : row["mbb_bnk_cd"].ToString(),
                mbb_crd_tp = row["mbb_crd_tp"] == DBNull.Value ? string.Empty : row["mbb_crd_tp"].ToString()
            };
        }
            
    }
}
