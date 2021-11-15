using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterVehicalInsurancePolicy
    {
        #region members

        decimal seq;
        string policy_cd;
        string policy_des;
        string cre_by;
        DateTime cre_dt;


        #endregion

        #region properties

        public DateTime Cre_dt
        {
            get { return cre_dt; }
            set { cre_dt = value; }
        }

        public string Cre_by
        {
            get { return cre_by; }
            set { cre_by = value; }
        }

        public string Policy_des
        {
            get { return policy_des; }
            set { policy_des = value; }
        }

        public string Policy_cd
        {
            get { return policy_cd; }
            set { policy_cd = value; }
        }

        public decimal Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        #endregion

        public static MasterVehicalInsurancePolicy Converter(DataRow row)
        {
            return new MasterVehicalInsurancePolicy
            {
                Seq = ((row["SVIP_SEQ"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVIP_SEQ"].ToString())),
                Policy_cd = ((row["SVIP_POLC_CD"] == DBNull.Value) ? string.Empty : row["SVIP_POLC_CD"].ToString()),
                Policy_des = ((row["SVIP_POLC_DESC"] == DBNull.Value) ? string.Empty : row["SVIP_POLC_DESC"].ToString()),
                Cre_by = ((row["SVIP_CRE_BY"] == DBNull.Value) ? string.Empty : row["SVIP_CRE_BY"].ToString()),
                Cre_dt = ((row["SVIP_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVIP_CRE_DT"].ToString())),
            };
        }

    }
}
