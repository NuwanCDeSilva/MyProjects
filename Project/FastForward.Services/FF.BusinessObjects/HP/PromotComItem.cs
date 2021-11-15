using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PromotComItem
    {
        #region Private Members
        private string _hpci_cd;
        private Int32 _hpci_line;
        private Int32 _hpci_seq;
        private string _hpci_tp;
        private string _hpci_brnd;
        #endregion

        public string Hpci_brnd
        {
            get { return _hpci_brnd; }
            set { _hpci_brnd = value; }
        }
        public string Hpci_cd
        {
            get { return _hpci_cd; }
            set { _hpci_cd = value; }
        }
        public Int32 Hpci_line
        {
            get { return _hpci_line; }
            set { _hpci_line = value; }
        }
        public Int32 Hpci_seq
        {
            get { return _hpci_seq; }
            set { _hpci_seq = value; }
        }
        public string Hpci_tp
        {
            get { return _hpci_tp; }
            set { _hpci_tp = value; }
        }


        public static PromotComItem Converter(DataRow row)
        {
            return new PromotComItem
            {
                Hpci_cd = row["HPCI_CD"] == DBNull.Value ? string.Empty : row["HPCI_CD"].ToString(),
                Hpci_line = row["HPCI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCI_LINE"]),
                Hpci_seq = row["HPCI_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCI_SEQ"]),
                Hpci_tp = row["HPCI_TP"] == DBNull.Value ? string.Empty : row["HPCI_TP"].ToString(),
                Hpci_brnd = row["HPCI_BRND"] == DBNull.Value ? string.Empty : row["HPCI_BRND"].ToString()

            };
        }

    }
}
