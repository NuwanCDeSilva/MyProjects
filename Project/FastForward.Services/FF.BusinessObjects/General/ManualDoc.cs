using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ManualDoc
    {
        #region Private Members

        private string _MDH_LOC = string.Empty;
        private DateTime _MDH_DT = DateTime.MinValue;
        private string _MDH_REF = string.Empty;
        private string _MDH_ISSUE_LOC = string.Empty;
        private string _MDH_STATUS = string.Empty;
        private string _MDH_REM = string.Empty;
        private DateTime _MDH_TIMESTAMP = DateTime.MinValue;
        private DateTime _MDH_RECEIVE_DT = DateTime.MinValue;
        private string _MDH_USER = string.Empty;

        #endregion

        #region Public Property Definition

        public string MDH_LOC
        {
            get { return _MDH_LOC; }
            set { _MDH_LOC = value; }
        }

        public DateTime MDH_DT
        {
            get { return _MDH_DT; }
            set { _MDH_DT = value; }
        }

        public string MDH_REF
        {
            get { return _MDH_REF; }
            set { _MDH_REF = value; }
        }
        public string MDH_ISSUE_LOC
        {
            get { return _MDH_ISSUE_LOC; }
            set { _MDH_ISSUE_LOC = value; }
        }
        public string MDH_STATUS
        {
            get { return _MDH_STATUS; }
            set { _MDH_STATUS = value; }
        }
        public string MDH_REM
        {
            get { return _MDH_REM; }
            set { _MDH_REM   = value; }
        }
        public DateTime MDH_TIMESTAMP
        {
            get { return _MDH_TIMESTAMP; }
            set { _MDH_TIMESTAMP = value; }
        }
        public DateTime MDH_RECEIVE_DT
        {
            get { return _MDH_RECEIVE_DT; }
            set { _MDH_RECEIVE_DT = value; }
        }
        public string MDH_USER
        {
            get { return _MDH_USER; }
            set { _MDH_USER = value; }
        }

        #endregion

        public static ManualDoc Converter(DataRow row)
        {
            return new ManualDoc
            {
                MDH_LOC = ((row["MDH_LOC"] == DBNull.Value) ? string.Empty : row["MDH_LOC"].ToString()),
                MDH_DT = ((row["MDH_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MDH_DT"])),
                MDH_REF = ((row["MDH_REF"] == DBNull.Value) ? string.Empty : row["MDH_REF"].ToString()),
                MDH_ISSUE_LOC = ((row["MDH_ISSUE_LOC"] == DBNull.Value) ? string.Empty : row["MDH_ISSUE_LOC"].ToString()),
                MDH_STATUS = ((row["MDH_STATUS"] == DBNull.Value) ? string.Empty : row["MDH_STATUS"].ToString()),
                MDH_REM = ((row["MDH_REM"] == DBNull.Value) ? string.Empty : row["MDH_REM"].ToString()),
                MDH_TIMESTAMP = ((row["MDH_TIMESTAMP"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MDH_TIMESTAMP"])),
                MDH_RECEIVE_DT = ((row["MDH_RECEIVE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MDH_RECEIVE_DT"])),
                MDH_USER = ((row["MDH_USER"] == DBNull.Value) ? string.Empty : row["MDH_USER"].ToString())

            };
        }
    }
}
