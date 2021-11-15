using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace FF.BusinessObjects
{

    //table : gnr_week
    //created by :Shani 19-11-2012
    [Serializable]
    public class GnrWeek
    {
        #region Private Members
        private string _gw_cre_by;
        private DateTime _gw_cre_dt;
        private DateTime _gw_from_dt;
        private Int32 _gw_month;
        private DateTime _gw_to_dt;
        private Int32 _gw_week;
        private Int32 _gw_year;
        #endregion

        public string Gw_cre_by { get { return _gw_cre_by; } set { _gw_cre_by = value; } }
        public DateTime Gw_cre_dt { get { return _gw_cre_dt; } set { _gw_cre_dt = value; } }
        public DateTime Gw_from_dt { get { return _gw_from_dt; } set { _gw_from_dt = value; } }
        public Int32 Gw_month { get { return _gw_month; } set { _gw_month = value; } }
        public DateTime Gw_to_dt { get { return _gw_to_dt; } set { _gw_to_dt = value; } }
        public Int32 Gw_week { get { return _gw_week; } set { _gw_week = value; } }
        public Int32 Gw_year { get { return _gw_year; } set { _gw_year = value; } }

        public static GnrWeek Converter(DataRow row)
        {
            return new GnrWeek
            {
                Gw_cre_by = row["GW_CRE_BY"] == DBNull.Value ? string.Empty : row["GW_CRE_BY"].ToString(),
                Gw_cre_dt = row["GW_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GW_CRE_DT"]),
                Gw_from_dt = row["GW_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GW_FROM_DT"]),
                Gw_month = row["GW_MONTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["GW_MONTH"]),
                Gw_to_dt = row["GW_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GW_TO_DT"]),
                Gw_week = row["GW_WEEK"] == DBNull.Value ? 0 : Convert.ToInt32(row["GW_WEEK"]),
                Gw_year = row["GW_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["GW_YEAR"])

            };
        }


    }
}
