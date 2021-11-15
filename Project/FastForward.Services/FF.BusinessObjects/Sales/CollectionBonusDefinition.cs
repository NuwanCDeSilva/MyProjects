using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class CollectionBonusDefinition
    {
        #region Private Members
        private decimal _hcbd_ars_per_from;
        private decimal _hcbd_ars_per_to;
        private Int32 _hcbd_avg_acc;
        private decimal _hcbd_bon_add_per;
        private decimal _hcbd_bon_per;
        private Int32 _hcbd_contu_month;
        private string _hcbd_cre_by;
        private DateTime _hcbd_cre_dt;
        private string _hcbd_mod_by;
        private DateTime _hcbd_mod_dt;
        private Int32 _hcbd_seq;
        private Int32 _hcbd_year_from;
        private Int32 _hcbd_year_to;
        #endregion

        public decimal Hcbd_ars_per_from
        {
            get { return _hcbd_ars_per_from; }
            set { _hcbd_ars_per_from = value; }
        }
        public decimal Hcbd_ars_per_to
        {
            get { return _hcbd_ars_per_to; }
            set { _hcbd_ars_per_to = value; }
        }
        public Int32 Hcbd_avg_acc
        {
            get { return _hcbd_avg_acc; }
            set { _hcbd_avg_acc = value; }
        }
        public decimal Hcbd_bon_add_per
        {
            get { return _hcbd_bon_add_per; }
            set { _hcbd_bon_add_per = value; }
        }
        public decimal Hcbd_bon_per
        {
            get { return _hcbd_bon_per; }
            set { _hcbd_bon_per = value; }
        }
        public Int32 Hcbd_contu_month
        {
            get { return _hcbd_contu_month; }
            set { _hcbd_contu_month = value; }
        }
        public string Hcbd_cre_by
        {
            get { return _hcbd_cre_by; }
            set { _hcbd_cre_by = value; }
        }
        public DateTime Hcbd_cre_dt
        {
            get { return _hcbd_cre_dt; }
            set { _hcbd_cre_dt = value; }
        }
        public string Hcbd_mod_by
        {
            get { return _hcbd_mod_by; }
            set { _hcbd_mod_by = value; }
        }
        public DateTime Hcbd_mod_dt
        {
            get { return _hcbd_mod_dt; }
            set { _hcbd_mod_dt = value; }
        }
        public Int32 Hcbd_seq
        {
            get { return _hcbd_seq; }
            set { _hcbd_seq = value; }
        }
        public Int32 Hcbd_year_from
        {
            get { return _hcbd_year_from; }
            set { _hcbd_year_from = value; }
        }
        public Int32 Hcbd_year_to
        {
            get { return _hcbd_year_to; }
            set { _hcbd_year_to = value; }
        }

        public static CollectionBonusDefinition Converter(DataRow row)
        {
            return new CollectionBonusDefinition
            {
                Hcbd_ars_per_from = row["HCBD_ARS_PER_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBD_ARS_PER_FROM"]),
                Hcbd_ars_per_to = row["HCBD_ARS_PER_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBD_ARS_PER_TO"]),
                Hcbd_avg_acc = row["HCBD_AVG_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HCBD_AVG_ACC"]),
                Hcbd_bon_add_per = row["HCBD_BON_ADD_PER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBD_BON_ADD_PER"]),
                Hcbd_bon_per = row["HCBD_BON_PER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HCBD_BON_PER"]),
                Hcbd_contu_month = row["HCBD_CONTU_MONTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["HCBD_CONTU_MONTH"]),
                Hcbd_cre_by = row["HCBD_CRE_BY"] == DBNull.Value ? string.Empty : row["HCBD_CRE_BY"].ToString(),
                Hcbd_cre_dt = row["HCBD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HCBD_CRE_DT"]),
                Hcbd_mod_by = row["HCBD_MOD_BY"] == DBNull.Value ? string.Empty : row["HCBD_MOD_BY"].ToString(),
                Hcbd_mod_dt = row["HCBD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HCBD_MOD_DT"]),
                Hcbd_seq = row["HCBD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HCBD_SEQ"]),
                Hcbd_year_from = row["HCBD_YEAR_FROM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HCBD_YEAR_FROM"]),
                Hcbd_year_to = row["HCBD_YEAR_TO"] == DBNull.Value ? 0 : Convert.ToInt32(row["HCBD_YEAR_TO"])

            };
        }
    }

}
