using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
  public  class PriceCategoryRef
    {
        #region Private Members
        private string _sarpc_cd;
        private string _sarpc_desc;
        private Boolean _sarpc_is_type;
        #endregion

        public string Sarpc_cd { get { return _sarpc_cd; } set { _sarpc_cd = value; } }
        public string Sarpc_desc { get { return _sarpc_desc; } set { _sarpc_desc = value; } }
        public Boolean Sarpc_is_type { get { return _sarpc_is_type; } set { _sarpc_is_type = value; } }

        public static PriceCategoryRef ConvertTotal(DataRow row)
        {
            return new PriceCategoryRef
            {
                Sarpc_cd = row["SARPC_CD"] == DBNull.Value ? string.Empty : row["SARPC_CD"].ToString(),
                Sarpc_desc = row["SARPC_DESC"] == DBNull.Value ? string.Empty : row["SARPC_DESC"].ToString(),
                Sarpc_is_type = row["SARPC_IS_TYPE"] == DBNull.Value ? false : Convert.ToBoolean(row["SARPC_IS_TYPE"])

            };
        }
    }
}

