using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class SalesTarget
    {
        #region private members
        private string _executiveCode;
        private decimal _amount;
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _prerange;
        private decimal _excommission;
        private decimal _mngrcommssion;

        private decimal _rctc_target;
        private decimal _rctc_st_per;
        private decimal _rctc_end_per;
        private string _rctc_exec;
        private decimal _rctc_exc_rate;
        private string _rctc_mngr;
        private decimal _rctc_mngr_rate;

        #endregion

        public string ExecutiveCode { get { return _executiveCode; } set { _executiveCode = value; } }
        public decimal Amount { get { return _amount; } set { _amount = value; } }
        public DateTime FromDate { get { return _fromDate; } set { _fromDate = value; } }
        public DateTime ToDate { get { return _toDate; } set { _toDate = value; } }
        public string prerange { get { return _prerange; } set { _prerange = value; } }
        public decimal excommission { get { return _excommission; } set { _excommission = value; } }
        public decimal mngrcommssion { get { return _mngrcommssion; } set { _mngrcommssion = value; } }

        public decimal Target { get { return _rctc_target; } set { _rctc_target = value; } }
        public decimal StrPer { get { return _rctc_st_per; } set { _rctc_st_per = value; } }
        public decimal EndPer { get { return _rctc_end_per; } set { _rctc_end_per = value; } }
        public string ExeCode { get { return _rctc_exec; } set { _rctc_exec = value; } }
        public decimal ExeRate { get { return _rctc_exc_rate; } set { _rctc_exc_rate = value; } }
        public string ManCode { get { return _rctc_mngr; } set { _rctc_mngr = value; } }
        public decimal ManRate { get { return _rctc_mngr_rate; } set { _rctc_mngr_rate = value; } }


        public static SalesTarget Converter(DataRow row)
        {
            return new SalesTarget
            {
                ExecutiveCode = row["sfd_exc"] == DBNull.Value ? string.Empty : row["sfd_exc"].ToString(),
                Amount = row["sfd_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sfd_val"].ToString()),
                prerange = row["prerange"] == DBNull.Value ? string.Empty : row["prerange"].ToString(),
                excommission = row["excommission"] == DBNull.Value ? 0 : Convert.ToDecimal(row["excommission"].ToString()),
                mngrcommssion = row["mngrcommssion"] == DBNull.Value ? 0 : Convert.ToDecimal(row["mngrcommssion"].ToString()),
            };
        }
        public static SalesTarget ConvertTarget(DataRow row)
        {
            return new SalesTarget
            {
                //ExecutiveCode = row["sfd_exc"] == DBNull.Value ? string.Empty : row["sfd_exc"].ToString(),
                //Amount = row["sfd_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sfd_val"].ToString()),
                Target = row["rctc_target"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_target"].ToString()),
                StrPer = row["rctc_st_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_st_per"].ToString()),
                EndPer = row["rctc_end_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_end_per"].ToString()),
                ExeCode = row["rctc_exec"] == DBNull.Value ? string.Empty : row["rctc_exec"].ToString(),
                ExeRate = row["rctc_exc_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_exc_rate"].ToString()),
                ManCode = row["rctc_mngr"] == DBNull.Value ? string.Empty : row["rctc_mngr"].ToString(),
                ManRate = row["rctc_mngr_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_mngr_rate"].ToString())
            };
        }
    }
}
