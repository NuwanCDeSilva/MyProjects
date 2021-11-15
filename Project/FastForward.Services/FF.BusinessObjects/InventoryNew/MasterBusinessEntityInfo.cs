using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterBusinessEntityInfo
    {
        #region Private Members
        private string _mbei_cd;
        private string _mbei_com;
        private string _mbei_tp;
        private string _mbei_val;
        private Int32 _mbei_available;
        #endregion

        public string Mbei_cd { get { return _mbei_cd; } set { _mbei_cd = value; } }
        public string Mbei_com { get { return _mbei_com; } set { _mbei_com = value; } }
        public string Mbei_tp { get { return _mbei_tp; } set { _mbei_tp = value; } }
        public string Mbei_val { get { return _mbei_val; } set { _mbei_val = value; } }
        public Int32 Mbei_available { get { return _mbei_available; } set { _mbei_available = value; } }
        public static MasterBusinessEntityInfo Converter(DataRow row)
        {
            return new MasterBusinessEntityInfo
            {
                Mbei_cd = row["MBEI_CD"] == DBNull.Value ? string.Empty : row["MBEI_CD"].ToString(),
                Mbei_com = row["MBEI_COM"] == DBNull.Value ? string.Empty : row["MBEI_COM"].ToString(),
                Mbei_tp = row["MBEI_TP"] == DBNull.Value ? string.Empty : row["MBEI_TP"].ToString(),
                Mbei_val = row["MBEI_VAL"] == DBNull.Value ? string.Empty : row["MBEI_VAL"].ToString(),
                Mbei_available = row["MBEI_AVAILABLE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBEI_AVAILABLE"])

            };
        }
    }
}
