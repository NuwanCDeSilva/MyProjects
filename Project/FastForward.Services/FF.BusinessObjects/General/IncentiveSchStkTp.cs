using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class IncentiveSchStkTp
    {
        #region Private Members
        private string _incs_circ;
        private Int32 _incs_dt_line;
        private Int32 _incs_line;
        private string _incs_ref;
        private string _incs_stk_tp;
        #endregion


        public string Incs_circ
        {
            get { return _incs_circ; }
            set { _incs_circ = value; }
        }
        public Int32 Incs_dt_line
        {
            get { return _incs_dt_line; }
            set { _incs_dt_line = value; }
        }
        public Int32 Incs_line
        {
            get { return _incs_line; }
            set { _incs_line = value; }
        }
        public string Incs_ref
        {
            get { return _incs_ref; }
            set { _incs_ref = value; }
        }
        public string Incs_stk_tp
        {
            get { return _incs_stk_tp; }
            set { _incs_stk_tp = value; }
        }

        public static IncentiveSchStkTp Converter(DataRow row)
        {
            return new IncentiveSchStkTp
            {
                Incs_circ = row["INCS_CIRC"] == DBNull.Value ? string.Empty : row["INCS_CIRC"].ToString(),
                Incs_dt_line = row["INCS_DT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCS_DT_LINE"]),
                Incs_line = row["INCS_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCS_LINE"]),
                Incs_ref = row["INCS_REF"] == DBNull.Value ? string.Empty : row["INCS_REF"].ToString(),
                Incs_stk_tp = row["INCS_STK_TP"] == DBNull.Value ? string.Empty : row["INCS_STK_TP"].ToString()
            };
        }

    }
}
