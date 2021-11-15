using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//darshana 18-07-2012
namespace FF.BusinessObjects
{
    [Serializable]
    public class HpInsuranceDefinition
    {
        #region Private Members
        private string _hpi_cal_on;
        private string _hpi_chk_on;
        private decimal _hpi_comm;
        private Boolean _hpi_comm_isrt;
        private string _hpi_cre_by;
        private DateTime _hpi_cre_dt;
        private DateTime _hpi_from_dt;
        private decimal _hpi_from_val;
        private Boolean _hpi_ins_isrt;
        private decimal _hpi_ins_val;
        private string _hpi_pty_cd;
        private string _hpi_pty_tp;
        private string _hpi_sch_cd;
        private Int32 _hpi_seq;
        private DateTime _hpi_to_dt;
        private decimal _hpi_to_val;
        private decimal _hpi_vat_rt;
        private bool _hpi_is_comp;
        #endregion

        public string Hpi_cal_on
        {
            get { return _hpi_cal_on; }
            set { _hpi_cal_on = value; }
        }
        public string Hpi_chk_on
        {
            get { return _hpi_chk_on; }
            set { _hpi_chk_on = value; }
        }
        public decimal Hpi_comm
        {
            get { return _hpi_comm; }
            set { _hpi_comm = value; }
        }
        public Boolean Hpi_comm_isrt
        {
            get { return _hpi_comm_isrt; }
            set { _hpi_comm_isrt = value; }
        }
        public string Hpi_cre_by
        {
            get { return _hpi_cre_by; }
            set { _hpi_cre_by = value; }
        }
        public DateTime Hpi_cre_dt
        {
            get { return _hpi_cre_dt; }
            set { _hpi_cre_dt = value; }
        }
        public DateTime Hpi_from_dt
        {
            get { return _hpi_from_dt; }
            set { _hpi_from_dt = value; }
        }
        public decimal Hpi_from_val
        {
            get { return _hpi_from_val; }
            set { _hpi_from_val = value; }
        }
        public Boolean Hpi_ins_isrt
        {
            get { return _hpi_ins_isrt; }
            set { _hpi_ins_isrt = value; }
        }
        public decimal Hpi_ins_val
        {
            get { return _hpi_ins_val; }
            set { _hpi_ins_val = value; }
        }
        public string Hpi_pty_cd
        {
            get { return _hpi_pty_cd; }
            set { _hpi_pty_cd = value; }
        }
        public string Hpi_pty_tp
        {
            get { return _hpi_pty_tp; }
            set { _hpi_pty_tp = value; }
        }
        public string Hpi_sch_cd
        {
            get { return _hpi_sch_cd; }
            set { _hpi_sch_cd = value; }
        }
        public Int32 Hpi_seq
        {
            get { return _hpi_seq; }
            set { _hpi_seq = value; }
        }
        public DateTime Hpi_to_dt
        {
            get { return _hpi_to_dt; }
            set { _hpi_to_dt = value; }
        }
        public decimal Hpi_to_val
        {
            get { return _hpi_to_val; }
            set { _hpi_to_val = value; }
        }
        public decimal Hpi_vat_rt
        {
            get { return _hpi_vat_rt; }
            set { _hpi_vat_rt = value; }
        }

        public bool Hpi_is_comp
        {
            get { return _hpi_is_comp; }
            set { _hpi_is_comp = value; }
        }

        public static HpInsuranceDefinition Converter(DataRow row)
        {
            return new HpInsuranceDefinition
            {
                Hpi_cal_on = row["HPI_CAL_ON"] == DBNull.Value ? string.Empty : row["HPI_CAL_ON"].ToString(),
                Hpi_chk_on = row["HPI_CHK_ON"] == DBNull.Value ? string.Empty : row["HPI_CHK_ON"].ToString(),
                Hpi_comm = row["HPI_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPI_COMM"]),
                Hpi_comm_isrt = row["HPI_COMM_ISRT"] == DBNull.Value ? false : Convert.ToBoolean(row["HPI_COMM_ISRT"]),
                Hpi_cre_by = row["HPI_CRE_BY"] == DBNull.Value ? string.Empty : row["HPI_CRE_BY"].ToString(),
                Hpi_cre_dt = row["HPI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPI_CRE_DT"]),
                Hpi_from_dt = row["HPI_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPI_FROM_DT"]),
                Hpi_from_val = row["HPI_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPI_FROM_VAL"]),
                Hpi_ins_isrt = row["HPI_INS_ISRT"] == DBNull.Value ? false : Convert.ToBoolean(row["HPI_INS_ISRT"]),
                Hpi_ins_val = row["HPI_INS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPI_INS_VAL"]),
                Hpi_pty_cd = row["HPI_PTY_CD"] == DBNull.Value ? string.Empty : row["HPI_PTY_CD"].ToString(),
                Hpi_pty_tp = row["HPI_PTY_TP"] == DBNull.Value ? string.Empty : row["HPI_PTY_TP"].ToString(),
                Hpi_sch_cd = row["HPI_SCH_CD"] == DBNull.Value ? string.Empty : row["HPI_SCH_CD"].ToString(),
                Hpi_seq = row["HPI_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPI_SEQ"]),
                Hpi_to_dt = row["HPI_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPI_TO_DT"]),
                Hpi_to_val = row["HPI_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPI_TO_VAL"]),
                Hpi_vat_rt = row["HPI_VAT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPI_VAT_RT"]),
                Hpi_is_comp = row["HPI_IS_COMP"] == DBNull.Value ? false : Convert.ToBoolean(row["HPI_IS_COMP"])

            };
        }
    }
}
