using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PromotComParty
    {
        #region Private Members
        private string _hpcp_com;
        private Int32 _hpcp_line;
        private string _hpcp_pty_cd;
        private string _hpcp_pty_tp;
        private Int32 _hpcp_seq;
        #endregion

        public string Hpcp_com
        {
            get { return _hpcp_com; }
            set { _hpcp_com = value; }
        }
        public Int32 Hpcp_line
        {
            get { return _hpcp_line; }
            set { _hpcp_line = value; }
        }
        public string Hpcp_pty_cd
        {
            get { return _hpcp_pty_cd; }
            set { _hpcp_pty_cd = value; }
        }
        public string Hpcp_pty_tp
        {
            get { return _hpcp_pty_tp; }
            set { _hpcp_pty_tp = value; }
        }
        public Int32 Hpcp_seq
        {
            get { return _hpcp_seq; }
            set { _hpcp_seq = value; }
        }

        public static PromotComParty Converter(DataRow row)
        {
            return new PromotComParty
            {
                Hpcp_com = row["HPCP_COM"] == DBNull.Value ? string.Empty : row["HPCP_COM"].ToString(),
                Hpcp_line = row["HPCP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCP_LINE"]),
                Hpcp_pty_cd = row["HPCP_PTY_CD"] == DBNull.Value ? string.Empty : row["HPCP_PTY_CD"].ToString(),
                Hpcp_pty_tp = row["HPCP_PTY_TP"] == DBNull.Value ? string.Empty : row["HPCP_PTY_TP"].ToString(),
                Hpcp_seq = row["HPCP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCP_SEQ"])

            };
        }

    }
}
