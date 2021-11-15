using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class HpAccountChecklistDet
    {
        #region Private Members
        private string _agrd_acc_no;
        private string _agrd_cre_by;
        private DateTime _agrd_cre_dt;
        private string _agrd_doc_no;
        private Boolean _agrd_ho_rec;
        private string _agrd_ho_rec_by;
        private DateTime _agrd_ho_rec_dt;
        private string _agrd_mod_by;
        private DateTime _agrd_mod_dt;
        private Int32 _agrd_seq;
        private Boolean _agrd_sw_rec;
        private string _agrd_sw_rec_by;
        private DateTime _agrd_sw_rec_dt;
        #endregion

        public string Agrd_acc_no
        {
            get { return _agrd_acc_no; }
            set { _agrd_acc_no = value; }
        }
        public string Agrd_cre_by
        {
            get { return _agrd_cre_by; }
            set { _agrd_cre_by = value; }
        }
        public DateTime Agrd_cre_dt
        {
            get { return _agrd_cre_dt; }
            set { _agrd_cre_dt = value; }
        }
        public string Agrd_doc_no
        {
            get { return _agrd_doc_no; }
            set { _agrd_doc_no = value; }
        }
        public Boolean Agrd_ho_rec
        {
            get { return _agrd_ho_rec; }
            set { _agrd_ho_rec = value; }
        }
        public string Agrd_ho_rec_by
        {
            get { return _agrd_ho_rec_by; }
            set { _agrd_ho_rec_by = value; }
        }
        public DateTime Agrd_ho_rec_dt
        {
            get { return _agrd_ho_rec_dt; }
            set { _agrd_ho_rec_dt = value; }
        }
        public string Agrd_mod_by
        {
            get { return _agrd_mod_by; }
            set { _agrd_mod_by = value; }
        }
        public DateTime Agrd_mod_dt
        {
            get { return _agrd_mod_dt; }
            set { _agrd_mod_dt = value; }
        }
        public Int32 Agrd_seq
        {
            get { return _agrd_seq; }
            set { _agrd_seq = value; }
        }
        public Boolean Agrd_sw_rec
        {
            get { return _agrd_sw_rec; }
            set { _agrd_sw_rec = value; }
        }
        public string Agrd_sw_rec_by
        {
            get { return _agrd_sw_rec_by; }
            set { _agrd_sw_rec_by = value; }
        }
        public DateTime Agrd_sw_rec_dt
        {
            get { return _agrd_sw_rec_dt; }
            set { _agrd_sw_rec_dt = value; }
        }

        public static HpAccountChecklistDet Converter(DataRow row)
        {
            return new HpAccountChecklistDet
            {
                Agrd_acc_no = row["AGRD_ACC_NO"] == DBNull.Value ? string.Empty : row["AGRD_ACC_NO"].ToString(),
                Agrd_cre_by = row["AGRD_CRE_BY"] == DBNull.Value ? string.Empty : row["AGRD_CRE_BY"].ToString(),
                Agrd_cre_dt = row["AGRD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRD_CRE_DT"]),
                Agrd_doc_no = row["AGRD_DOC_NO"] == DBNull.Value ? string.Empty : row["AGRD_DOC_NO"].ToString(),
                Agrd_ho_rec = row["AGRD_HO_REC"] == DBNull.Value ? false : Convert.ToBoolean(row["AGRD_HO_REC"]),
                Agrd_ho_rec_by = row["AGRD_HO_REC_BY"] == DBNull.Value ? string.Empty : row["AGRD_HO_REC_BY"].ToString(),
                Agrd_ho_rec_dt = row["AGRD_HO_REC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRD_HO_REC_DT"]),
                Agrd_mod_by = row["AGRD_MOD_BY"] == DBNull.Value ? string.Empty : row["AGRD_MOD_BY"].ToString(),
                Agrd_mod_dt = row["AGRD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRD_MOD_DT"]),
                Agrd_seq = row["AGRD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AGRD_SEQ"]),
                Agrd_sw_rec = row["AGRD_SW_REC"] == DBNull.Value ? false : Convert.ToBoolean(row["AGRD_SW_REC"]),
                Agrd_sw_rec_by = row["AGRD_SW_REC_BY"] == DBNull.Value ? string.Empty : row["AGRD_SW_REC_BY"].ToString(),
                Agrd_sw_rec_dt = row["AGRD_SW_REC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AGRD_SW_REC_DT"])

            };
        }


    }
}
