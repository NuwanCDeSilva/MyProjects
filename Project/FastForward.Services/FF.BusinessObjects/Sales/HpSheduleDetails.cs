using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//by Darshana 23/07/2012

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpSheduleDetails
    {
        #region Private Members
        private string _hts_acc_no;
        private string _hts_cre_by;
        private DateTime _hts_cre_dt;
        private DateTime _hts_due_dt;
        private decimal _hts_ins;
        private decimal _hts_intr;
        private Int32 _hts_rnt_no;
        private decimal _hts_rnt_val;
        private decimal _hts_sdt;
        private Int32 _hts_seq;
        private decimal _hts_ser;
        private decimal _hts_vat;
        private string _hts_mod_by;
        private DateTime _hts_mod_dt;
        private Boolean _hts_upload;
        private decimal _hts_veh_insu;
        private decimal _hts_tot_val;
        private decimal _hts_ins_vat;
        private decimal _hts_ins_comm;
        private decimal _hts_cap;
        #endregion

        public string Hts_acc_no
        {
            get { return _hts_acc_no; }
            set { _hts_acc_no = value; }
        }
        public string Hts_cre_by
        {
            get { return _hts_cre_by; }
            set { _hts_cre_by = value; }
        }
        public DateTime Hts_cre_dt
        {
            get { return _hts_cre_dt; }
            set { _hts_cre_dt = value; }
        }
        public DateTime Hts_due_dt
        {
            get { return _hts_due_dt; }
            set { _hts_due_dt = value; }
        }
        public decimal Hts_ins
        {
            get { return _hts_ins; }
            set { _hts_ins = value; }
        }
        public decimal Hts_intr
        {
            get { return _hts_intr; }
            set { _hts_intr = value; }
        }
        public Int32 Hts_rnt_no
        {
            get { return _hts_rnt_no; }
            set { _hts_rnt_no = value; }
        }
        public decimal Hts_rnt_val
        {
            get { return _hts_rnt_val; }
            set { _hts_rnt_val = value; }
        }
        public decimal Hts_sdt
        {
            get { return _hts_sdt; }
            set { _hts_sdt = value; }
        }
        public Int32 Hts_seq
        {
            get { return _hts_seq; }
            set { _hts_seq = value; }
        }
        public decimal Hts_ser
        {
            get { return _hts_ser; }
            set { _hts_ser = value; }
        }
        public decimal Hts_vat
        {
            get { return _hts_vat; }
            set { _hts_vat = value; }
        }

        public string Hts_mod_by
        {
            get { return _hts_mod_by; }
            set { _hts_mod_by = value; }
        }

        public DateTime Hts_mod_dt
        {
            get { return _hts_mod_dt; }
            set { _hts_mod_dt = value; }
        }

        public Boolean Hts_upload
        {
            get { return _hts_upload; }
            set { _hts_upload = value; }
        }

        public decimal Hts_veh_insu
        {
            get { return _hts_veh_insu; }
            set { _hts_veh_insu = value; }
        }

        public decimal Hts_tot_val
        {
            get { return _hts_tot_val; }
            set { _hts_tot_val = value; }
        }

        public decimal Hts_ins_vat
        {
            get { return _hts_ins_vat; }
            set { _hts_ins_vat = value; }
        }

        public decimal Hts_ins_comm
        {
            get { return _hts_ins_comm; }
            set { _hts_ins_comm = value; }
        }

        public decimal Hts_cap
        {
            get { return _hts_cap; }
            set { _hts_cap = value; }
        }

        public static HpSheduleDetails Converter(DataRow row)
        {
            return new HpSheduleDetails
            {
                Hts_acc_no = row["HTS_ACC_NO"] == DBNull.Value ? string.Empty : row["HTS_ACC_NO"].ToString(),
                Hts_cre_by = row["HTS_CRE_BY"] == DBNull.Value ? string.Empty : row["HTS_CRE_BY"].ToString(),
                Hts_cre_dt = row["HTS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HTS_CRE_DT"]),
                Hts_due_dt = row["HTS_DUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HTS_DUE_DT"]),
                Hts_ins = row["HTS_INS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_INS"]),
                Hts_intr = row["HTS_INTR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_INTR"]),
                Hts_rnt_no = row["HTS_RNT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["HTS_RNT_NO"]),
                Hts_rnt_val = row["HTS_RNT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_RNT_VAL"]),
                Hts_sdt = row["HTS_SDT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_SDT"]),
                Hts_seq = row["HTS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HTS_SEQ"]),
                Hts_ser = row["HTS_SER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_SER"]),
                Hts_vat = row["HTS_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_VAT"]),
                Hts_mod_by = row["HTS_MOD_BY"] == DBNull.Value ? string.Empty : row["HTS_MOD_BY"].ToString(),
                Hts_mod_dt = row["HTS_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HTS_MOD_DT"]),
                Hts_upload = row["HTS_UPLOAD"] == DBNull.Value ? false : Convert.ToBoolean(row["HTS_UPLOAD"]),
                Hts_veh_insu = row["HTS_VEH_INSU"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_VEH_INSU"]),
                Hts_tot_val = row["HTS_TOT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_TOT_VAL"]),
                Hts_ins_vat = row["HTS_INS_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_INS_VAT"]),
                Hts_ins_comm = row["HTS_INS_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_INS_COMM"]),
                Hts_cap = row["HTS_CAP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTS_CAP"])
            };
        }


    }
}
