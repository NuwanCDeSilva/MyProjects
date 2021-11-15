using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PhysicalStockVerificationSerial
    {
        #region Private Members
        private string _auss_cre_by;
        private DateTime _auss_cre_dt;
        private string _auss_in_doc;
        private DateTime _auss_in_dt;
        private string _auss_item;
        private string _auss_itm_stus;
        private string _auss_job;
        private string _auss_ref_stus;
        private string _auss_rpt_type;
        private Int32 _auss_seq;
        private string _auss_serial;
        private string _auss_warranty;
        private string _auss_type;
        private int _auss_ser_id;
        private string _auss_rmk;
        private DateTime _AUSS_ORG_IN_DT;

        #endregion
        public DateTime AUSS_ORG_IN_DT
        {
            get { return _AUSS_ORG_IN_DT; }
            set { _AUSS_ORG_IN_DT = value; }
        }
        public string Auss_cre_by
        {
            get { return _auss_cre_by; }
            set { _auss_cre_by = value; }
        }
        public DateTime Auss_cre_dt
        {
            get { return _auss_cre_dt; }
            set { _auss_cre_dt = value; }
        }
        public string Auss_in_doc
        {
            get { return _auss_in_doc; }
            set { _auss_in_doc = value; }
        }
        public DateTime Auss_in_dt
        {
            get { return _auss_in_dt; }
            set { _auss_in_dt = value; }
        }
        public string Auss_item
        {
            get { return _auss_item; }
            set { _auss_item = value; }
        }
        public string Auss_itm_stus
        {
            get { return _auss_itm_stus; }
            set { _auss_itm_stus = value; }
        }
        public string Auss_job
        {
            get { return _auss_job; }
            set { _auss_job = value; }
        }
        public string Auss_ref_stus
        {
            get { return _auss_ref_stus; }
            set { _auss_ref_stus = value; }
        }
        public string Auss_rpt_type
        {
            get { return _auss_rpt_type; }
            set { _auss_rpt_type = value; }
        }
        public Int32 Auss_seq
        {
            get { return _auss_seq; }
            set { _auss_seq = value; }
        }
        public string Auss_serial
        {
            get { return _auss_serial; }
            set { _auss_serial = value; }
        }
        public string Auss_warranty
        {
            get { return _auss_warranty; }
            set { _auss_warranty = value; }
        }
        public string Auss_type
        {
            get { return _auss_type; }
            set { _auss_type = value; }
        }
        public int Auss_ser_id
        {
            get { return _auss_ser_id; }
            set { _auss_ser_id = value; }
        }
        public string Auss_rmk
        {
            get { return _auss_rmk; }
            set { _auss_rmk = value; }
        }
        public static PhysicalStockVerificationSerial Converter(DataRow row)
        {
            return new PhysicalStockVerificationSerial
            {
                Auss_cre_by = row["AUSS_CRE_BY"] == DBNull.Value ? string.Empty : row["AUSS_CRE_BY"].ToString(),
                Auss_cre_dt = row["AUSS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSS_CRE_DT"]),
                Auss_in_doc = row["AUSS_IN_DOC"] == DBNull.Value ? string.Empty : row["AUSS_IN_DOC"].ToString(),
                Auss_in_dt = row["AUSS_IN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSS_IN_DT"]),
                Auss_item = row["AUSS_ITEM"] == DBNull.Value ? string.Empty : row["AUSS_ITEM"].ToString(),
                Auss_itm_stus = row["AUSS_ITM_STUS"] == DBNull.Value ? string.Empty : row["AUSS_ITM_STUS"].ToString(),
                Auss_job = row["AUSS_JOB"] == DBNull.Value ? string.Empty : row["AUSS_JOB"].ToString(),
                Auss_ref_stus = row["AUSS_REF_STUS"] == DBNull.Value ? string.Empty : row["AUSS_REF_STUS"].ToString(),
                Auss_rpt_type = row["AUSS_RPT_TYPE"] == DBNull.Value ? string.Empty : row["AUSS_RPT_TYPE"].ToString(),
                Auss_seq = row["AUSS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSS_SEQ"]),
                Auss_serial = row["AUSS_SERIAL"] == DBNull.Value ? string.Empty : row["AUSS_SERIAL"].ToString(),
                Auss_warranty = row["AUSS_WARRANTY"] == DBNull.Value ? string.Empty : row["AUSS_WARRANTY"].ToString(),
                Auss_type = row["AUSS_TYPE"] == DBNull.Value ? string.Empty : (row["AUSS_TYPE"]).ToString(),
                Auss_ser_id = row["AUSS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSS_SER_ID"]),
                AUSS_ORG_IN_DT = row["AUSS_ORG_IN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSS_ORG_IN_DT"]),
                Auss_rmk = row["AUSS_RMK"] == DBNull.Value ? string.Empty : (row["AUSS_RMK"]).ToString()
            };
        }

    }
}
