using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class HpScheduleDetailLog
    {
        //Written By Prabhath on 29/08/2012
        //Table : hpt_shed_his


        #region Private Members
        private string _hsh_acc_no;
        private string _hsh_cre_by;
        private DateTime _hsh_cre_dt;
        private DateTime _hsh_dt;
        private DateTime _hsh_due_dt;
        private decimal _hsh_ins;
        private decimal _hsh_intr;
        private Int32 _hsh_rnt_no;
        private decimal _hsh_rnt_val;
        private decimal _hsh_sdt;
        private Int32 _hsh_seq;
        private decimal _hsh_ser;
        private decimal _hsh_vat;
        #endregion

        public string Hsh_acc_no { get { return _hsh_acc_no; } set { _hsh_acc_no = value; } }
        public string Hsh_cre_by { get { return _hsh_cre_by; } set { _hsh_cre_by = value; } }
        public DateTime Hsh_cre_dt { get { return _hsh_cre_dt; } set { _hsh_cre_dt = value; } }
        public DateTime Hsh_dt { get { return _hsh_dt; } set { _hsh_dt = value; } }
        public DateTime Hsh_due_dt { get { return _hsh_due_dt; } set { _hsh_due_dt = value; } }
        public decimal Hsh_ins { get { return _hsh_ins; } set { _hsh_ins = value; } }
        public decimal Hsh_intr { get { return _hsh_intr; } set { _hsh_intr = value; } }
        public Int32 Hsh_rnt_no { get { return _hsh_rnt_no; } set { _hsh_rnt_no = value; } }
        public decimal Hsh_rnt_val { get { return _hsh_rnt_val; } set { _hsh_rnt_val = value; } }
        public decimal Hsh_sdt { get { return _hsh_sdt; } set { _hsh_sdt = value; } }
        public Int32 Hsh_seq { get { return _hsh_seq; } set { _hsh_seq = value; } }
        public decimal Hsh_ser { get { return _hsh_ser; } set { _hsh_ser = value; } }
        public decimal Hsh_vat { get { return _hsh_vat; } set { _hsh_vat = value; } }

        public static HpScheduleDetailLog Converter(DataRow row)
        {
            return new HpScheduleDetailLog
            {
                Hsh_acc_no = row["HSH_ACC_NO"] == DBNull.Value ? string.Empty : row["HSH_ACC_NO"].ToString(),
                Hsh_cre_by = row["HSH_CRE_BY"] == DBNull.Value ? string.Empty : row["HSH_CRE_BY"].ToString(),
                Hsh_cre_dt = row["HSH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSH_CRE_DT"]),
                Hsh_dt = row["HSH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSH_DT"]),
                Hsh_due_dt = row["HSH_DUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSH_DUE_DT"]),
                Hsh_ins = row["HSH_INS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSH_INS"]),
                Hsh_intr = row["HSH_INTR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSH_INTR"]),
                Hsh_rnt_no = row["HSH_RNT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSH_RNT_NO"]),
                Hsh_rnt_val = row["HSH_RNT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSH_RNT_VAL"]),
                Hsh_sdt = row["HSH_SDT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSH_SDT"]),
                Hsh_seq = row["HSH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSH_SEQ"]),
                Hsh_ser = row["HSH_SER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSH_SER"]),
                Hsh_vat = row["HSH_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSH_VAT"])

            };
        }
    }
}
