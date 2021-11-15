using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{

    public class IncentiveSchPB
    {
        #region Private Members
        private string _inpbl_circ;
        private Int32 _inpbl_dt_line;
        private Int32 _inpbl_line;
        private string _inpbl_pb;
        private string _inpbl_pb_lvl;
        private string _inpbl_ref;
        #endregion

        public string Inpbl_circ
        {
            get { return _inpbl_circ; }
            set { _inpbl_circ = value; }
        }
        public Int32 Inpbl_dt_line
        {
            get { return _inpbl_dt_line; }
            set { _inpbl_dt_line = value; }
        }
        public Int32 Inpbl_line
        {
            get { return _inpbl_line; }
            set { _inpbl_line = value; }
        }
        public string Inpbl_pb
        {
            get { return _inpbl_pb; }
            set { _inpbl_pb = value; }
        }
        public string Inpbl_pb_lvl
        {
            get { return _inpbl_pb_lvl; }
            set { _inpbl_pb_lvl = value; }
        }
        public string Inpbl_ref
        {
            get { return _inpbl_ref; }
            set { _inpbl_ref = value; }
        }

        public static IncentiveSchPB Converter(DataRow row)
        {
            return new IncentiveSchPB
            {
                Inpbl_circ = row["INPBL_CIRC"] == DBNull.Value ? string.Empty : row["INPBL_CIRC"].ToString(),
                Inpbl_dt_line = row["INPBL_DT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INPBL_DT_LINE"]),
                Inpbl_line = row["INPBL_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INPBL_LINE"]),
                Inpbl_pb = row["INPBL_PB"] == DBNull.Value ? string.Empty : row["INPBL_PB"].ToString(),
                Inpbl_pb_lvl = row["INPBL_PB_LVL"] == DBNull.Value ? string.Empty : row["INPBL_PB_LVL"].ToString(),
                Inpbl_ref = row["INPBL_REF"] == DBNull.Value ? string.Empty : row["INPBL_REF"].ToString()
            };
        }

    }
}
