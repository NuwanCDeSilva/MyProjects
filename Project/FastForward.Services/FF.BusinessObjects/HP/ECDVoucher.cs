using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
   public class ECDVoucher
    {
        #region Private Members
        private string _hvd_cre_by;
        private DateTime _hvd_cre_dt;
        private decimal _hvd_ecd_val;
        private decimal _hvd_from_bal;
        private DateTime _hvd_from_dt;
        private Boolean _hvd_is_prc;
        private Boolean _hvd_is_rt;
        private string _hvd_pc;
        private DateTime _hvd_prc_dt;
        private string _hvd_sch_cd;
        private decimal _hvd_to_bal;
        private DateTime _hvd_to_dt;
        private DateTime _hvd_acc_cr_from;
        private DateTime _hvd_acc_cr_to;

        #endregion

        public string Hvd_cre_by
        {
            get { return _hvd_cre_by; }
            set { _hvd_cre_by = value; }
        }
        public DateTime Hvd_cre_dt
        {
            get { return _hvd_cre_dt; }
            set { _hvd_cre_dt = value; }
        }
        public decimal Hvd_ecd_val
        {
            get { return _hvd_ecd_val; }
            set { _hvd_ecd_val = value; }
        }
        public decimal Hvd_from_bal
        {
            get { return _hvd_from_bal; }
            set { _hvd_from_bal = value; }
        }
        public DateTime Hvd_from_dt
        {
            get { return _hvd_from_dt; }
            set { _hvd_from_dt = value; }
        }
        public Boolean Hvd_is_prc
        {
            get { return _hvd_is_prc; }
            set { _hvd_is_prc = value; }
        }
        public Boolean Hvd_is_rt
        {
            get { return _hvd_is_rt; }
            set { _hvd_is_rt = value; }
        }
        public string Hvd_pc
        {
            get { return _hvd_pc; }
            set { _hvd_pc = value; }
        }
        public DateTime Hvd_prc_dt
        {
            get { return _hvd_prc_dt; }
            set { _hvd_prc_dt = value; }
        }
        public string Hvd_sch_cd
        {
            get { return _hvd_sch_cd; }
            set { _hvd_sch_cd = value; }
        }
        public decimal Hvd_to_bal
        {
            get { return _hvd_to_bal; }
            set { _hvd_to_bal = value; }
        }
        public DateTime Hvd_to_dt
        {
            get { return _hvd_to_dt; }
            set { _hvd_to_dt = value; }
        }
        public DateTime Hvd_acc_cr_to
        {
            get { return _hvd_acc_cr_to; }
            set { _hvd_acc_cr_to = value; }
        }
        public DateTime Hvd_acc_cr_from
        {
            get { return _hvd_acc_cr_from; }
            set { _hvd_acc_cr_from = value; }
        }

        public static ECDVoucher Converter(DataRow row)
        {
            return new ECDVoucher
            {
                Hvd_cre_by = row["HVD_CRE_BY"] == DBNull.Value ? string.Empty : row["HVD_CRE_BY"].ToString(),
                Hvd_cre_dt = row["HVD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HVD_CRE_DT"]),
                Hvd_ecd_val = row["HVD_ECD_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HVD_ECD_VAL"]),
                Hvd_from_bal = row["HVD_FROM_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HVD_FROM_BAL"]),
                Hvd_from_dt = row["HVD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HVD_FROM_DT"]),
                Hvd_is_prc = row["HVD_IS_PRC"] == DBNull.Value ? false : Convert.ToBoolean(row["HVD_IS_PRC"]),
                Hvd_is_rt = row["HVD_IS_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["HVD_IS_RT"]),
                Hvd_pc = row["HVD_PC"] == DBNull.Value ? string.Empty : row["HVD_PC"].ToString(),
                Hvd_prc_dt = row["HVD_PRC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HVD_PRC_DT"]),
                Hvd_sch_cd = row["HVD_SCH_CD"] == DBNull.Value ? string.Empty : row["HVD_SCH_CD"].ToString(),
                Hvd_to_bal = row["HVD_TO_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HVD_TO_BAL"]),
                Hvd_to_dt = row["HVD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HVD_TO_DT"]),
                Hvd_acc_cr_to = row["HVD_ACC_CR_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HVD_ACC_CR_TO"]),
                Hvd_acc_cr_from = row["HVD_ACC_CR_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HVD_ACC_CR_FROM"])

            };
        }

    }
}
