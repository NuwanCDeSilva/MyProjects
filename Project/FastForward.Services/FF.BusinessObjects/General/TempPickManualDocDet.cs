using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
    public class TempPickManualDocDet
    {
         //used for hire purchase collection(Manual receipts). 
        #region Private Members

        private string _MDD_PREFIX = string.Empty;
        private string _MDD_BK_NO = string.Empty;
        private int _MDD_FIRST = 0;
        private int _MDD_LAST = 0;
        private int _MDD_CUR = 0;
        private string _MDD_USER = string.Empty;
        private string _MDD_LOC = string.Empty;
        #endregion

        #region Public Property Definition

        public string MDD_PREFIX
        {
            get { return _MDD_PREFIX; }
            set { _MDD_PREFIX = value; }
        }
        public string MDD_BK_NO
        {
            get { return _MDD_BK_NO; }
            set { _MDD_BK_NO = value; }
        }


        public int MDD_FIRST
        {
            get { return _MDD_FIRST; }
            set { _MDD_FIRST = value; }
        }
        public int MDD_LAST
        {
            get { return _MDD_LAST; }
            set { _MDD_LAST = value; }
        }
        public int MDD_CUR
        {
            get { return _MDD_CUR; }
            set { _MDD_CUR = value; }
        }

        public string MDD_USER
        {
            get { return _MDD_USER; }
            set { _MDD_USER = value; }
        }
        public string MDD_LOC
        {
            get { return _MDD_LOC; }
            set { _MDD_LOC = value; }
        }

        #endregion

        public static TempPickManualDocDet Converter(DataRow row)
        {
            return new TempPickManualDocDet
            {
                MDD_PREFIX = ((row["MDD_PREFIX"] == DBNull.Value) ? string.Empty : row["MDD_PREFIX"].ToString()),
                MDD_BK_NO = ((row["MDD_BK_NO"] == DBNull.Value) ? string.Empty : row["MDD_BK_NO"].ToString()),
                MDD_FIRST = ((row["MDD_FIRST"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MDD_FIRST"])),
                MDD_LAST = ((row["MDD_LAST"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MDD_LAST"])),
                MDD_CUR = ((row["MDD_CUR"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MDD_CUR"])),
                MDD_USER = ((row["MDD_USER"] == DBNull.Value) ? string.Empty : row["MDD_USER"].ToString()),
                MDD_LOC = ((row["MDD_LOC"] == DBNull.Value) ? string.Empty : row["MDD_LOC"].ToString())

            };
        }
    }
}
