using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
    public class MasterInvoiceType
    {
        #region Private Members
        private string _srtp_cd;
        private string _srtp_desc;
        private string _srtp_main_tp;
        #endregion

        public string Srtp_cd { get { return _srtp_cd; } set { _srtp_cd = value; } }
        public string Srtp_desc { get { return _srtp_desc; } set { _srtp_desc = value; } }

        public string Srtp_main_tp { get { return _srtp_main_tp; } set { _srtp_main_tp = value; } }

        //updated by akila 2017/10/26
        public DateTime? Srtp_valid_from_dt { get; set; }
        public DateTime? Srtp_valid_to_dt { get; set; }

        public static MasterInvoiceType ConvertTotal(DataRow row)
        {
            return new MasterInvoiceType
            {
                Srtp_cd = row["SRTP_CD"] == DBNull.Value ? string.Empty : row["SRTP_CD"].ToString(),
                Srtp_desc = row["SRTP_DESC"] == DBNull.Value ? string.Empty : row["SRTP_DESC"].ToString(),
                Srtp_main_tp = row["SRTP_MAIN_TP"] == DBNull.Value ? string.Empty : row["SRTP_MAIN_TP"].ToString()

            };
        }
        public static MasterInvoiceType ConverterCustomerWeb(DataRow row)
        {
            return new MasterInvoiceType
            {
                Srtp_cd = row["SRTP_CD"] == DBNull.Value ? string.Empty : row["SRTP_CD"].ToString(),
                Srtp_desc = row["SRTP_DESC"] == DBNull.Value ? string.Empty : row["SRTP_DESC"].ToString(),
                Srtp_main_tp = row["SRTP_MAIN_TP"] == DBNull.Value ? string.Empty : row["SRTP_MAIN_TP"].ToString(),
                Srtp_valid_from_dt = row["MBSA_VALID_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBSA_VALID_FRM_DT"].ToString()),
                Srtp_valid_to_dt = row["MBSA_VALID_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBSA_VALID_TO_DT"].ToString())

            };
        }
    }
}

