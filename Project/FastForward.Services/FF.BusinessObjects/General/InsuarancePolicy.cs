using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//by darshana 15/08/2012
namespace FF.BusinessObjects
{
    [Serializable]
    public class InsuarancePolicy
    {
        #region Private Members
        private string _svip_cre_by;
        private DateTime _svip_cre_dt;
        private string _svip_polc_cd;
        private string _svip_polc_desc;
        private Int32 _svip_seq;
        #endregion

        public string Svip_cre_by
        {
            get { return _svip_cre_by; }
            set { _svip_cre_by = value; }
        }
        public DateTime Svip_cre_dt
        {
            get { return _svip_cre_dt; }
            set { _svip_cre_dt = value; }
        }
        public string Svip_polc_cd
        {
            get { return _svip_polc_cd; }
            set { _svip_polc_cd = value; }
        }
        public string Svip_polc_desc
        {
            get { return _svip_polc_desc; }
            set { _svip_polc_desc = value; }
        }
        public Int32 Svip_seq
        {
            get { return _svip_seq; }
            set { _svip_seq = value; }
        }

        public static InsuarancePolicy Converter(DataRow row)
        {
            return new InsuarancePolicy
            {
                Svip_cre_by = row["SVIP_CRE_BY"] == DBNull.Value ? string.Empty : row["SVIP_CRE_BY"].ToString(),
                Svip_cre_dt = row["SVIP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIP_CRE_DT"]),
                Svip_polc_cd = row["SVIP_POLC_CD"] == DBNull.Value ? string.Empty : row["SVIP_POLC_CD"].ToString(),
                Svip_polc_desc = row["SVIP_POLC_DESC"] == DBNull.Value ? string.Empty : row["SVIP_POLC_DESC"].ToString(),
                Svip_seq = row["SVIP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVIP_SEQ"])

            };
        }

    }
}
