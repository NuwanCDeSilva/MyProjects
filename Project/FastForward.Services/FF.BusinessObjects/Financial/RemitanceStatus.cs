using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public class RemitanceStatus
    {
        #region Private Members
        private string _gpac_com;
        private string _gpac_cre_by;
        private DateTime _gpac_cre_dt;
        private DateTime _gpac_op_dt;
        private string _gpac_pc;
        private Int32 _gpac_seq;
        private Boolean _gpac_stus;
        private string _gpac_tp;

        #endregion

        public string Gpac_com
        {
            get { return _gpac_com; }
            set { _gpac_com = value; }
        }
        public string Gpac_cre_by
        {
            get { return _gpac_cre_by; }
            set { _gpac_cre_by = value; }
        }
        public DateTime Gpac_cre_dt
        {
            get { return _gpac_cre_dt; }
            set { _gpac_cre_dt = value; }
        }
        public DateTime Gpac_op_dt
        {
            get { return _gpac_op_dt; }
            set { _gpac_op_dt = value; }
        }
        public string Gpac_pc
        {
            get { return _gpac_pc; }
            set { _gpac_pc = value; }
        }
        public Int32 Gpac_seq
        {
            get { return _gpac_seq; }
            set { _gpac_seq = value; }
        }
        public Boolean Gpac_stus
        {
            get { return _gpac_stus; }
            set { _gpac_stus = value; }
        }
        public string Gpac_tp
        {
            get { return _gpac_tp; }
            set { _gpac_tp = value; }
        }


        public static RemitanceStatus Converter(DataRow row)
        {
            return new RemitanceStatus
            {
                Gpac_com = row["GPAC_COM"] == DBNull.Value ? string.Empty : row["GPAC_COM"].ToString(),
                Gpac_cre_by = row["GPAC_CRE_BY"] == DBNull.Value ? string.Empty : row["GPAC_CRE_BY"].ToString(),
                Gpac_cre_dt = row["GPAC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GPAC_CRE_DT"]),
                Gpac_op_dt = row["GPAC_OP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GPAC_OP_DT"]),
                Gpac_pc = row["GPAC_PC"] == DBNull.Value ? string.Empty : row["GPAC_PC"].ToString(),
                Gpac_seq = row["GPAC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GPAC_SEQ"]),
                Gpac_stus = row["GPAC_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["GPAC_STUS"]),
                Gpac_tp = row["GPAC_TP"] == DBNull.Value ?   string.Empty : row["GPAC_TP"].ToString()

            };
        }

    }
}
