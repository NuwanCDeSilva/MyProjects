using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //create by = shani on 25-07-2013
    //table = HPT_ACCOUNTS_AGREEMENT
    [Serializable]
    public class AccountAgreement
    {
        #region Private Members
        private string _agr_account;
        private Boolean _agr_closure;
        private DateTime _agr_closuredate;
        private string _agr_closuretype;
        private DateTime _agr_datereturned;
        private DateTime _agr_date_received;
        private string _agr_handover;
        private string _agr_handoverid;
        private string _agr_handovername;
        private string _agr_location;
        private DateTime _agr_modifieddate;
        private string _agr_sessionid;
        private string _agr_slipno;
        private decimal _agr_timereturned;
        private DateTime _agr_timestamp;
        private DateTime _agr_writtenoff;
        private string _agr_stus;
        private DateTime _agr_closedt;
        private string _agr_comcd;
        #endregion

        public string Agr_account { get { return _agr_account; } set { _agr_account = value; } }
        public Boolean Agr_closure { get { return _agr_closure; } set { _agr_closure = value; } }
        public DateTime Agr_closuredate { get { return _agr_closuredate; } set { _agr_closuredate = value; } }
        public string Agr_closuretype { get { return _agr_closuretype; } set { _agr_closuretype = value; } }
        public DateTime Agr_datereturned { get { return _agr_datereturned; } set { _agr_datereturned = value; } }
        public DateTime Agr_date_received { get { return _agr_date_received; } set { _agr_date_received = value; } }
        public string Agr_handover { get { return _agr_handover; } set { _agr_handover = value; } }
        public string Agr_handoverid { get { return _agr_handoverid; } set { _agr_handoverid = value; } }
        public string Agr_handovername { get { return _agr_handovername; } set { _agr_handovername = value; } }
        public string Agr_location { get { return _agr_location; } set { _agr_location = value; } }
        public DateTime Agr_modifieddate { get { return _agr_modifieddate; } set { _agr_modifieddate = value; } }
        public string Agr_sessionid { get { return _agr_sessionid; } set { _agr_sessionid = value; } }
        public string Agr_slipno { get { return _agr_slipno; } set { _agr_slipno = value; } }
        public decimal Agr_timereturned { get { return _agr_timereturned; } set { _agr_timereturned = value; } }
        public DateTime Agr_timestamp { get { return _agr_timestamp; } set { _agr_timestamp = value; } }
        public DateTime Agr_writtenoff { get { return _agr_writtenoff; } set { _agr_writtenoff = value; } }
        public string Agr_stus { get { return _agr_stus; } set { _agr_stus = value; } }
        public DateTime Agr_closedt { get { return _agr_closedt; } set { _agr_closedt = value; } }
        public string Agr_comcd { get { return _agr_comcd; } set { _agr_comcd = value; } }

        public static AccountAgreement Converter(DataRow row)
        {
            return new AccountAgreement
            {
                Agr_account = row["AGR_ACCOUNT"] == DBNull.Value ? string.Empty : row["AGR_ACCOUNT"].ToString(),
                Agr_closure = row["AGR_CLOSURE"] == DBNull.Value ? false : Convert.ToBoolean(row["AGR_CLOSURE"]),
                Agr_closuredate = row["AGR_CLOSUREDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGR_CLOSUREDATE"]),
                Agr_closuretype = row["AGR_CLOSURETYPE"] == DBNull.Value ? string.Empty : row["AGR_CLOSURETYPE"].ToString(),
                Agr_datereturned = row["AGR_DATERETURNED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGR_DATERETURNED"]),
                Agr_date_received = row["AGR_DATE_RECEIVED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGR_DATE_RECEIVED"]),
                Agr_handover = row["AGR_HANDOVER"] == DBNull.Value ? string.Empty : row["AGR_HANDOVER"].ToString(),
                Agr_handoverid = row["AGR_HANDOVERID"] == DBNull.Value ? string.Empty : row["AGR_HANDOVERID"].ToString(),
                Agr_handovername = row["AGR_HANDOVERNAME"] == DBNull.Value ? string.Empty : row["AGR_HANDOVERNAME"].ToString(),
                Agr_location = row["AGR_LOCATION"] == DBNull.Value ? string.Empty : row["AGR_LOCATION"].ToString(),
                Agr_modifieddate = row["AGR_MODIFIEDDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGR_MODIFIEDDATE"]),
                Agr_sessionid = row["AGR_SESSIONID"] == DBNull.Value ? string.Empty : row["AGR_SESSIONID"].ToString(),
                Agr_slipno = row["AGR_SLIPNO"] == DBNull.Value ? string.Empty : row["AGR_SLIPNO"].ToString(),
                Agr_timereturned = row["AGR_TIMERETURNED"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGR_TIMERETURNED"]),
                Agr_timestamp = row["AGR_TIMESTAMP"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGR_TIMESTAMP"]),
                Agr_writtenoff = row["AGR_WRITTENOFF"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGR_WRITTENOFF"]),
                Agr_stus = row["AGR_STUS"] == DBNull.Value ? string.Empty : row["AGR_STUS"].ToString(),
                Agr_closedt = row["AGR_CLOSEDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGR_CLOSEDT"]),
                Agr_comcd = row["AGR_COMCD"] == DBNull.Value ? string.Empty : row["AGR_COMCD"].ToString(),

            };
        }

    }
}
