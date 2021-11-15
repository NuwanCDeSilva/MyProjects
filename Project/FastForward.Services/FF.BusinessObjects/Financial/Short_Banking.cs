using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Short_Banking
    {
        #region Private Members
        private string _SB_COM;
        private string _SB_PC;
        private DateTime _SB_DT;
        private Decimal _SB_PRV_EXS_REM;
        private Decimal _SB_EXS_REM;
        private Decimal _SB_CIH;
        private Decimal _SB_REMITED;
        private Decimal _SB_DIFF;
        private DateTime _SB_MONTH;
        private Int32 _SB_WEEK;

        #endregion

        #region Public Property Definition
        public string SB_COM
        {
            get { return _SB_COM; }
            set { _SB_COM = value; }
        }
        public string SB_PC
        {
            get { return _SB_PC; }
            set { _SB_PC = value; }
        }
        public DateTime SB_DT
        {
            get { return _SB_DT; }
            set { _SB_DT = value; }
        }
        public Decimal SB_PRV_EXS_REM
        {
            get { return _SB_PRV_EXS_REM; }
            set { _SB_PRV_EXS_REM = value; }
        }
        public Decimal SB_EXS_REM
        {
            get { return _SB_EXS_REM; }
            set { _SB_EXS_REM = value; }
        }
        public Decimal SB_CIH
        {
            get { return _SB_CIH; }
            set { _SB_CIH = value; }
        }
        public Decimal SB_REMITED
        {
            get { return _SB_REMITED; }
            set { _SB_REMITED = value; }
        }
        public Decimal SB_DIFF
        {
            get { return _SB_DIFF; }
            set { _SB_DIFF = value; }
        }
        public DateTime SB_MONTH
        {
            get { return _SB_MONTH; }
            set { _SB_MONTH = value; }
        }
        public Int32 SB_WEEK
        {
            get { return _SB_WEEK; }
            set { _SB_WEEK = value; }
        }
        #endregion

        #region Converters
        public static Short_Banking Converter(DataRow row)
        {
            return new Short_Banking
            {
                SB_COM = row["SB_COM"] == DBNull.Value ? string.Empty : row["SB_COM"].ToString(),
                SB_PC = row["SB_PC"] == DBNull.Value ? string.Empty : row["SB_PC"].ToString(),
                SB_DT = row["SB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SB_DT"]),
                SB_PRV_EXS_REM = row["SB_PRV_EXS_REM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SB_PRV_EXS_REM"]),
                SB_EXS_REM = row["SB_EXS_REM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SB_EXS_REM"]),
                SB_CIH = row["SB_CIH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SB_CIH"]),
                SB_REMITED = row["SB_REMITED"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SB_REMITED"]),
                SB_DIFF = row["SB_DIFF"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SB_DIFF"]),
                SB_MONTH = row["SB_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SB_MONTH"]),
                SB_WEEK = row["SB_WEEK"] == DBNull.Value ? 0 : Convert.ToInt32(row["SB_WEEK"]),
            };
        }
        #endregion
    }
}

