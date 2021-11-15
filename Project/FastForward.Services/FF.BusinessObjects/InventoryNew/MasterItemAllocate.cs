using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
     [Serializable]
    public class MasterItemAllocate
    {
        #region Private Members
        private Boolean _isa_act;
        private decimal _isa_aloc_bqty;
        private decimal _isa_aloc_qty;
        private string _isa_chnl;
        private string _isa_com;
        private string _isa_cre_by;
        private DateTime _isa_cre_dt;
        private DateTime _isa_doc_dt;
        private string _isa_doc_no;
        private string _isa_doc_tp;
        private DateTime _isa_dt;
        private string _isa_itm_cd;
        private Boolean _isa_itm_changed;
        private string _isa_itm_stus;
        private string _isa_loc;
        private string _isa_ref_no;
        private decimal _isa_req_bqty;
        private decimal _isa_req_qty;
        private Int32 _isa_seq;
        private string _isa_tp;
        private string _isa_session_id;
        private string _isa_cnl_by;
        private DateTime _isa_cnl_dt;
        private string _isa_cnl_session_id;
        #endregion

        public Boolean Isa_act
        {
            get { return _isa_act; }
            set { _isa_act = value; }
        }
        public decimal Isa_aloc_bqty
        {
            get { return _isa_aloc_bqty; }
            set { _isa_aloc_bqty = value; }
        }
        public decimal Isa_aloc_qty
        {
            get { return _isa_aloc_qty; }
            set { _isa_aloc_qty = value; }
        }
        public string Isa_chnl
        {
            get { return _isa_chnl; }
            set { _isa_chnl = value; }
        }
        public string Isa_com
        {
            get { return _isa_com; }
            set { _isa_com = value; }
        }
        public string Isa_cre_by
        {
            get { return _isa_cre_by; }
            set { _isa_cre_by = value; }
        }
        public DateTime Isa_cre_dt
        {
            get { return _isa_cre_dt; }
            set { _isa_cre_dt = value; }
        }
        public DateTime Isa_doc_dt
        {
            get { return _isa_doc_dt; }
            set { _isa_doc_dt = value; }
        }
        public string Isa_doc_no
        {
            get { return _isa_doc_no; }
            set { _isa_doc_no = value; }
        }
        public string Isa_doc_tp
        {
            get { return _isa_doc_tp; }
            set { _isa_doc_tp = value; }
        }
        public DateTime Isa_dt
        {
            get { return _isa_dt; }
            set { _isa_dt = value; }
        }
        public string Isa_itm_cd
        {
            get { return _isa_itm_cd; }
            set { _isa_itm_cd = value; }
        }
        public Boolean Isa_itm_changed
        {
            get { return _isa_itm_changed; }
            set { _isa_itm_changed = value; }
        }
        public string Isa_itm_stus
        {
            get { return _isa_itm_stus; }
            set { _isa_itm_stus = value; }
        }
        public string Isa_loc
        {
            get { return _isa_loc; }
            set { _isa_loc = value; }
        }
        public string Isa_ref_no
        {
            get { return _isa_ref_no; }
            set { _isa_ref_no = value; }
        }
        public decimal Isa_req_bqty
        {
            get { return _isa_req_bqty; }
            set { _isa_req_bqty = value; }
        }
        public decimal Isa_req_qty
        {
            get { return _isa_req_qty; }
            set { _isa_req_qty = value; }
        }
        public Int32 Isa_seq
        {
            get { return _isa_seq; }
            set { _isa_seq = value; }
        }
        public string Isa_tp
        {
            get { return _isa_tp; }
            set { _isa_tp = value; }
        }
        public string Isa_session_id
        {
            get { return _isa_session_id; }
            set { _isa_session_id = value; }
        }
        public string Isa_cnl_by
        {
            get { return _isa_cnl_by; }
            set { _isa_cnl_by = value; }
        }
        public DateTime Isa_cnl_dt
        {
            get { return _isa_cnl_dt; }
            set { _isa_cnl_dt = value; }
        }
        public string Isa_cnl_session_id
        {
            get { return _isa_cnl_session_id; }
            set { _isa_cnl_session_id = value; }
        }
        public Int32 Tmp_doc_no { get; set; }

        public static MasterItemAllocate Converter(DataRow row)
        {
            return new MasterItemAllocate
            {
                Isa_act = row["ISA_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["ISA_ACT"]),
                Isa_aloc_bqty = row["ISA_ALOC_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISA_ALOC_BQTY"]),
                Isa_aloc_qty = row["ISA_ALOC_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISA_ALOC_QTY"]),
                Isa_chnl = row["ISA_CHNL"] == DBNull.Value ? string.Empty : row["ISA_CHNL"].ToString(),
                Isa_com = row["ISA_COM"] == DBNull.Value ? string.Empty : row["ISA_COM"].ToString(),
                Isa_cre_by = row["ISA_CRE_BY"] == DBNull.Value ? string.Empty : row["ISA_CRE_BY"].ToString(),
                Isa_cre_dt = row["ISA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISA_CRE_DT"]),
                Isa_doc_dt = row["ISA_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISA_DOC_DT"]),
                Isa_doc_no = row["ISA_DOC_NO"] == DBNull.Value ? string.Empty : row["ISA_DOC_NO"].ToString(),
                Isa_doc_tp = row["ISA_DOC_TP"] == DBNull.Value ? string.Empty : row["ISA_DOC_TP"].ToString(),
                Isa_dt = row["ISA_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISA_DT"]),
                Isa_itm_cd = row["ISA_ITM_CD"] == DBNull.Value ? string.Empty : row["ISA_ITM_CD"].ToString(),
                Isa_itm_changed = row["ISA_ITM_CHANGED"] == DBNull.Value ? false : Convert.ToBoolean(row["ISA_ITM_CHANGED"]),
                Isa_itm_stus = row["ISA_ITM_STUS"] == DBNull.Value ? string.Empty : row["ISA_ITM_STUS"].ToString(),
                Isa_loc = row["ISA_LOC"] == DBNull.Value ? string.Empty : row["ISA_LOC"].ToString(),
                Isa_ref_no = row["ISA_REF_NO"] == DBNull.Value ? string.Empty : row["ISA_REF_NO"].ToString(),
                Isa_req_bqty = row["ISA_REQ_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISA_REQ_BQTY"]),
                Isa_req_qty = row["ISA_REQ_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISA_REQ_QTY"]),
                Isa_seq = row["ISA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISA_SEQ"]),
                Isa_tp = row["ISA_TP"] == DBNull.Value ? string.Empty : row["ISA_TP"].ToString(),
                Isa_session_id = row["ISA_SESSION_ID"] == DBNull.Value ? string.Empty : row["ISA_SESSION_ID"].ToString(),
                Isa_cnl_by = row["ISA_CNL_BY"] == DBNull.Value ? string.Empty : row["ISA_CNL_BY"].ToString(),
                Isa_cnl_dt = row["ISA_CNL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISA_CNL_DT"]),
                Isa_cnl_session_id = row["ISA_CNL_SESSION_ID"] == DBNull.Value ? string.Empty : row["ISA_CNL_SESSION_ID"].ToString()
               
            };
        }
    }
 }
