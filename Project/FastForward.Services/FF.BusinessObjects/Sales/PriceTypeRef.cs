using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
  public  class PriceTypeRef
    {
        #region Private Members
        private string _sarpt_cd;
        private string _sarpt_desc;
        private int _sarpt_indi;
        private Boolean _sarpt_is_com;
        #endregion

        public string Sarpt_cd { get { return _sarpt_cd; } set { _sarpt_cd = value; } }
        public string Sarpt_desc { get { return _sarpt_desc; } set { _sarpt_desc = value; } }
        public int Sarpt_indi { get { return _sarpt_indi; } set { _sarpt_indi = value; } }
        public Boolean Sarpt_is_com { get { return _sarpt_is_com; } set { _sarpt_is_com = value; } }

        public static PriceTypeRef ConvertTotal(DataRow row)
        {
            return new PriceTypeRef
            {
                Sarpt_cd = row["SARPT_CD"] == DBNull.Value ? string.Empty : row["SARPT_CD"].ToString(),
                Sarpt_desc = row["SARPT_DESC"] == DBNull.Value ? string.Empty : row["SARPT_DESC"].ToString(),
                Sarpt_indi = row["SARPT_INDI"] == DBNull.Value ? 0 : Convert.ToInt16(row["SARPT_INDI"]),
                Sarpt_is_com = row["SARPT_IS_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["SARPT_IS_COM"])

            };
        }
    }
}

