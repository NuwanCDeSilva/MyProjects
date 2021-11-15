using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
  public  class PriceBookRef
    {
        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// </summary>

        #region Private Members
        private Boolean _sapb_act;
        private string _sapb_com;
        private string _sapb_cre_by;
        private DateTime _sapb_cre_when;
        private string _sapb_desc;
        private int _sapb_hierachy_lvl;
        private string _sapb_mod_by;
        private DateTime _sapb_mod_when;
        private string _sapb_pb;
        #endregion

        public Boolean Sapb_act { get { return _sapb_act; } set { _sapb_act = value; } }
        public string Sapb_com { get { return _sapb_com; } set { _sapb_com = value; } }
        public string Sapb_cre_by { get { return _sapb_cre_by; } set { _sapb_cre_by = value; } }
        public DateTime Sapb_cre_when { get { return _sapb_cre_when; } set { _sapb_cre_when = value; } }
        public string Sapb_desc { get { return _sapb_desc; } set { _sapb_desc = value; } }
        public int Sapb_hierachy_lvl { get { return _sapb_hierachy_lvl; } set { _sapb_hierachy_lvl = value; } }
        public string Sapb_mod_by { get { return _sapb_mod_by; } set { _sapb_mod_by = value; } }
        public DateTime Sapb_mod_when { get { return _sapb_mod_when; } set { _sapb_mod_when = value; } }
        public string Sapb_pb { get { return _sapb_pb; } set { _sapb_pb = value; } }

        public static PriceBookRef ConvertTotal(DataRow row)
        {
            return new PriceBookRef
            {
                Sapb_act = row["SAPB_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPB_ACT"]),
                Sapb_com = row["SAPB_COM"] == DBNull.Value ? string.Empty : row["SAPB_COM"].ToString(),
                Sapb_cre_by = row["SAPB_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPB_CRE_BY"].ToString(),
                Sapb_cre_when = row["SAPB_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPB_CRE_WHEN"]),
                Sapb_desc = row["SAPB_DESC"] == DBNull.Value ? string.Empty : row["SAPB_DESC"].ToString(),
                Sapb_hierachy_lvl = row["SAPB_HIERACHY_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["SAPB_HIERACHY_LVL"]),
                Sapb_mod_by = row["SAPB_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPB_MOD_BY"].ToString(),
                Sapb_mod_when = row["SAPB_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPB_MOD_WHEN"]),
                Sapb_pb = row["SAPB_PB"] == DBNull.Value ? string.Empty : row["SAPB_PB"].ToString()

            };
        }
    }
}

