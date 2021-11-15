using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{

    public class IncentiveSchSerial
    {
        #region Private Members
        private string _incr_circ;
        private Int32 _incr_dt_line;
        private Int32 _incr_line;
        private string _incr_ref;
        private string _incr_ser;
        #endregion

        public string Incr_circ
        {
            get { return _incr_circ; }
            set { _incr_circ = value; }
        }
        public Int32 Incr_dt_line
        {
            get { return _incr_dt_line; }
            set { _incr_dt_line = value; }
        }
        public Int32 Incr_line
        {
            get { return _incr_line; }
            set { _incr_line = value; }
        }
        public string Incr_ref
        {
            get { return _incr_ref; }
            set { _incr_ref = value; }
        }
        public string Incr_ser
        {
            get { return _incr_ser; }
            set { _incr_ser = value; }
        }

        public static IncentiveSchSerial Converter(DataRow row)
        {
            return new IncentiveSchSerial
            {
                Incr_circ = row["INCR_CIRC"] == DBNull.Value ? string.Empty : row["INCR_CIRC"].ToString(),
                Incr_dt_line = row["INCR_DT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCR_DT_LINE"]),
                Incr_line = row["INCR_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCR_LINE"]),
                Incr_ref = row["INCR_REF"] == DBNull.Value ? string.Empty : row["INCR_REF"].ToString(),
                Incr_ser = row["INCR_SER"] == DBNull.Value ? string.Empty : row["INCR_SER"].ToString()
            };
        }

    }
}
