using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    public class DocCheckList
    {
        #region Private Members
        private string _dcl_com;
        private string _dcl_cre_by;
        private DateTime _dcl_cre_dt;
        private string _dcl_desc;
        private Int32 _dcl_line;
        private DateTime _dcl_month;
        private string _dcl_pc;
        private Int32 _dcl_rt;
        private Int32 _dcl_se;
        private Int32 _dcl_sr;
        private Int32 _dcl_week;

        #endregion

        public string Dcl_com
        {
            get { return _dcl_com; }
            set { _dcl_com = value; }
        }
        public string Dcl_cre_by
        {
            get { return _dcl_cre_by; }
            set { _dcl_cre_by = value; }
        }
        public DateTime Dcl_cre_dt
        {
            get { return _dcl_cre_dt; }
            set { _dcl_cre_dt = value; }
        }
        public string Dcl_desc
        {
            get { return _dcl_desc; }
            set { _dcl_desc = value; }
        }
        public Int32 Dcl_line
        {
            get { return _dcl_line; }
            set { _dcl_line = value; }
        }
        public DateTime Dcl_month
        {
            get { return _dcl_month; }
            set { _dcl_month = value; }
        }
        public string Dcl_pc
        {
            get { return _dcl_pc; }
            set { _dcl_pc = value; }
        }
        public Int32 Dcl_rt
        {
            get { return _dcl_rt; }
            set { _dcl_rt = value; }
        }
        public Int32 Dcl_se
        {
            get { return _dcl_se; }
            set { _dcl_se = value; }
        }
        public Int32 Dcl_sr
        {
            get { return _dcl_sr; }
            set { _dcl_sr = value; }
        }
        public Int32 Dcl_week
        {
            get { return _dcl_week; }
            set { _dcl_week = value; }
        }

        public static DocCheckList Converter(DataRow row)
        {
            return new DocCheckList
            {
                Dcl_com = row["DCL_COM"] == DBNull.Value ? string.Empty : row["DCL_COM"].ToString(),
                Dcl_cre_by = row["DCL_CRE_BY"] == DBNull.Value ? string.Empty : row["DCL_CRE_BY"].ToString(),
                Dcl_cre_dt = row["DCL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DCL_CRE_DT"]),
                Dcl_desc = row["DCL_DESC"] == DBNull.Value ? string.Empty : row["DCL_DESC"].ToString(),
                Dcl_line = row["DCL_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["DCL_LINE"]),
                Dcl_month = row["DCL_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DCL_MONTH"]),
                Dcl_pc = row["DCL_PC"] == DBNull.Value ? string.Empty : row["DCL_PC"].ToString(),
                Dcl_rt = row["DCL_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["DCL_RT"]),
                Dcl_se = row["DCL_SE"] == DBNull.Value ? 0 : Convert.ToInt32(row["DCL_SE"]),
                Dcl_sr = row["DCL_SR"] == DBNull.Value ? 0 : Convert.ToInt32(row["DCL_SR"]),
                Dcl_week = row["DCL_WEEK"] == DBNull.Value ? 0 : Convert.ToInt32(row["DCL_WEEK"])

            };
        }

    }
}
