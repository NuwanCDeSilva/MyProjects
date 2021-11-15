using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class HPAccountChecklistHdr
    {

        #region Private Members
        private string _agrh_com;
        private string _agrh_cre_by;
        private DateTime _agrh_cre_dt;
        private string _agrh_doc_no;
        private Boolean _agrh_ho_confirm;
        private string _agrh_mod_by;
        private DateTime _agrh_mod_dt;
        private string _agrh_pc;
        private string _agrh_pod_no;
        private Int32 _agrh_seq;
        private Boolean _agrh_sw_confirm;
        private DateTime _agrh_dt;


        #endregion

        public string Agrh_com
        {
            get { return _agrh_com; }
            set { _agrh_com = value; }
        }
        public string Agrh_cre_by
        {
            get { return _agrh_cre_by; }
            set { _agrh_cre_by = value; }
        }
        public DateTime Agrh_cre_dt
        {
            get { return _agrh_cre_dt; }
            set { _agrh_cre_dt = value; }
        }
        public string Agrh_doc_no
        {
            get { return _agrh_doc_no; }
            set { _agrh_doc_no = value; }
        }
        public Boolean Agrh_ho_confirm
        {
            get { return _agrh_ho_confirm; }
            set { _agrh_ho_confirm = value; }
        }
        public string Agrh_mod_by
        {
            get { return _agrh_mod_by; }
            set { _agrh_mod_by = value; }
        }
        public DateTime Agrh_mod_dt
        {
            get { return _agrh_mod_dt; }
            set { _agrh_mod_dt = value; }
        }
        public string Agrh_pc
        {
            get { return _agrh_pc; }
            set { _agrh_pc = value; }
        }
        public string Agrh_pod_no
        {
            get { return _agrh_pod_no; }
            set { _agrh_pod_no = value; }
        }
        public Int32 Agrh_seq
        {
            get { return _agrh_seq; }
            set { _agrh_seq = value; }
        }
        public Boolean Agrh_sw_confirm
        {
            get { return _agrh_sw_confirm; }
            set { _agrh_sw_confirm = value; }
        }
        public DateTime Agrh_dt
        {
            get { return _agrh_dt; }
            set { _agrh_dt = value; }
        }

        public static HPAccountChecklistHdr Converter(DataRow row)
        {
            return new HPAccountChecklistHdr
            {
                Agrh_com = row["AGRH_COM"] == DBNull.Value ? string.Empty : row["AGRH_COM"].ToString(),
                Agrh_cre_by = row["AGRH_CRE_BY"] == DBNull.Value ? string.Empty : row["AGRH_CRE_BY"].ToString(),
                Agrh_cre_dt = row["AGRH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRH_CRE_DT"]),
                Agrh_doc_no = row["AGRH_DOC_NO"] == DBNull.Value ? string.Empty : row["AGRH_DOC_NO"].ToString(),
                Agrh_ho_confirm = row["AGRH_HO_CONFIRM"] == DBNull.Value ? false : Convert.ToBoolean(row["AGRH_HO_CONFIRM"]),
                Agrh_mod_by = row["AGRH_MOD_BY"] == DBNull.Value ? string.Empty : row["AGRH_MOD_BY"].ToString(),
                Agrh_mod_dt = row["AGRH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRH_MOD_DT"]),
                Agrh_pc = row["AGRH_PC"] == DBNull.Value ? string.Empty : row["AGRH_PC"].ToString(),
                Agrh_pod_no = row["AGRH_POD_NO"] == DBNull.Value ? string.Empty : row["AGRH_POD_NO"].ToString(),
                Agrh_seq = row["AGRH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AGRH_SEQ"]),
                Agrh_sw_confirm = row["AGRH_SW_CONFIRM"] == DBNull.Value ? false : Convert.ToBoolean(row["AGRH_SW_CONFIRM"]),
                Agrh_dt = row["ARGH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ARGH_DT"]),

            };
        }
    }
}
