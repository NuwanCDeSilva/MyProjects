using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterPartyHierachy
    {

        #region Private Members
        private string _msph_cate;
        private int _msph_lvl;
        private string _msph_tp;
        #endregion

        public string Msph_cate
        {
            get { return _msph_cate; }
            set { _msph_cate = value; }
        }
        public int Msph_lvl
        {
            get { return _msph_lvl; }
            set { _msph_lvl = value; }
        }
        public string Msph_tp
        {
            get { return _msph_tp; }
            set { _msph_tp = value; }
        }

        public static MasterPartyHierachy Converter(DataRow row)
        {
            return new MasterPartyHierachy
            {
                Msph_cate = row["MSPH_CATE"] == DBNull.Value ? string.Empty : row["MSPH_CATE"].ToString(),
                Msph_lvl = row["MSPH_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["MSPH_LVL"]),
                Msph_tp = row["MSPH_TP"] == DBNull.Value ? string.Empty : row["MSPH_TP"].ToString()

            };
        }
    }
}
