using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class CollectionBonusAdjusment
    {
        #region Private Members
        private string _hcba_acc;
        private decimal _hcba_ars_amt;
        private decimal _hcba_ar_1mon;
        private DateTime _hcba_bonus_dt;
        private string _hcba_com;
        private string _hcba_cre_by;
        private DateTime _hcba_cre_dt;
        private decimal _hcba_dispute;
        private DateTime _hcba_grce_dt;
        private decimal _hcba_grce_sett;
        private decimal _hcba_insu;
        private decimal _hcba_lod;
        private decimal _hcba_net_ars;
        private decimal _hcba_other;
        private string _hcba_pc;
        private Int32 _hcba_seq;
        private decimal _hcba_service;
        private decimal _hcba_tot_adj;
        #endregion

        public string Hcba_acc
        {
            get { return _hcba_acc; }
            set { _hcba_acc = value; }
        }
        public decimal Hcba_ars_amt
        {
            get { return _hcba_ars_amt; }
            set { _hcba_ars_amt = value; }
        }
        public decimal Hcba_ar_1mon
        {
            get { return _hcba_ar_1mon; }
            set { _hcba_ar_1mon = value; }
        }
        public DateTime Hcba_bonus_dt
        {
            get { return _hcba_bonus_dt; }
            set { _hcba_bonus_dt = value; }
        }
        public string Hcba_com
        {
            get { return _hcba_com; }
            set { _hcba_com = value; }
        }
        public string Hcba_cre_by
        {
            get { return _hcba_cre_by; }
            set { _hcba_cre_by = value; }
        }
        public DateTime Hcba_cre_dt
        {
            get { return _hcba_cre_dt; }
            set { _hcba_cre_dt = value; }
        }
        public decimal Hcba_dispute
        {
            get { return _hcba_dispute; }
            set { _hcba_dispute = value; }
        }
        public DateTime Hcba_grce_dt
        {
            get { return _hcba_grce_dt; }
            set { _hcba_grce_dt = value; }
        }
        public decimal Hcba_grce_sett
        {
            get { return _hcba_grce_sett; }
            set { _hcba_grce_sett = value; }
        }
        public decimal Hcba_insu
        {
            get { return _hcba_insu; }
            set { _hcba_insu = value; }
        }
        public decimal Hcba_lod
        {
            get { return _hcba_lod; }
            set { _hcba_lod = value; }
        }
        public decimal Hcba_net_ars
        {
            get { return _hcba_net_ars; }
            set { _hcba_net_ars = value; }
        }
        public decimal Hcba_other
        {
            get { return _hcba_other; }
            set { _hcba_other = value; }
        }
        public string Hcba_pc
        {
            get { return _hcba_pc; }
            set { _hcba_pc = value; }
        }
        public Int32 Hcba_seq
        {
            get { return _hcba_seq; }
            set { _hcba_seq = value; }
        }
        public decimal Hcba_service
        {
            get { return _hcba_service; }
            set { _hcba_service = value; }
        }
        public decimal Hcba_tot_adj
        {
            get { return _hcba_tot_adj; }
            set { _hcba_tot_adj = value; }
        }

        public static CollectionBonusAdjusment Converter(DataRow row)
        {
            return new CollectionBonusAdjusment
            {
                Hcba_acc = row["HCBA_ACC"] == DBNull.Value ? string.Empty : row["HCBA_ACC"].ToString(),
                Hcba_ars_amt = row["HCBA_ARS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_ARS_AMT"]),
                Hcba_ar_1mon = row["HCBA_AR_1MON"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_AR_1MON"]),
                Hcba_bonus_dt = row["HCBA_BONUS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HCBA_BONUS_DT"]),
                Hcba_com = row["HCBA_COM"] == DBNull.Value ? string.Empty : row["HCBA_COM"].ToString(),
                Hcba_cre_by = row["HCBA_CRE_BY"] == DBNull.Value ? string.Empty : row["HCBA_CRE_BY"].ToString(),
                Hcba_cre_dt = row["HCBA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HCBA_CRE_DT"]),
                Hcba_dispute = row["HCBA_DISPUTE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_DISPUTE"]),
                Hcba_grce_dt = row["HCBA_GRCE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HCBA_GRCE_DT"]),
                Hcba_grce_sett = row["HCBA_GRCE_SETT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_GRCE_SETT"]),
                Hcba_insu = row["HCBA_INSU"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_INSU"]),
                Hcba_lod = row["HCBA_LOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_LOD"]),
                Hcba_net_ars = row["HCBA_NET_ARS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_NET_ARS"]),
                Hcba_other = row["HCBA_OTHER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_OTHER"]),
                Hcba_pc = row["HCBA_PC"] == DBNull.Value ? string.Empty : row["HCBA_PC"].ToString(),
                Hcba_seq = row["HCBA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HCBA_SEQ"]),
                Hcba_service = row["HCBA_SERVICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_SERVICE"]),
                Hcba_tot_adj = row["HCBA_TOT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBA_TOT_ADJ"])

            };
        }
    }
}
