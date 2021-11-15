using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{//
    public class LoyaltyPriorityCode
    {
        #region Private Members
        private string _sarcd_com;
        private int _sarcd_ord;
        private string _sarcd_tp;
        #endregion

        public string Sarcd_com { get { return _sarcd_com; } set { _sarcd_com = value; } }
        public int Sarcd_ord { get { return _sarcd_ord; } set { _sarcd_ord = value; } }
        public string Sarcd_tp { get { return _sarcd_tp; } set { _sarcd_tp = value; } }

        public static LoyaltyPriorityCode Converter(DataRow row)
        {
            return new LoyaltyPriorityCode
            {
                Sarcd_com = row["SARCD_COM"] == DBNull.Value ? string.Empty : row["SARCD_COM"].ToString(),
                Sarcd_ord = row["SARCD_ORD"] == DBNull.Value ? 0 : Convert.ToInt16(row["SARCD_ORD"]),
                Sarcd_tp = row["SARCD_TP"] == DBNull.Value ? string.Empty : row["SARCD_TP"].ToString()

            };
        }
    }
}
