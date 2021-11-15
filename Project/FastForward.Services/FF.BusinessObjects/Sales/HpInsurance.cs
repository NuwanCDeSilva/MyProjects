using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpInsurance
    {

        /// <summary>
        /// Written By Prabhath on 05/07/2012
        /// Table: HPT_INSU (in EMS)
        /// </summary>

        #region Private Members
        private Boolean _hit_is_rvs;
        private string _hti_acc_num;
        private decimal _hti_comm_rt;
        private decimal _hti_comm_val;
        private string _hti_cre_by;
        private DateTime _hti_cre_dt;
        private DateTime _hti_dt;
        private decimal _hti_epf;
        private decimal _hti_esd;
        private decimal _hti_ins_val;
        private string _hti_mnl_num;
        private string _hti_pc;
        private string _hti_ref;
        private decimal _hti_vat_rt;
        private decimal _hti_vat_val;
        private decimal _hti_wht;
        private Int32 _hti_seq;
        private string _hti_com;

        #endregion

        public Boolean Hit_is_rvs { get { return _hit_is_rvs; } set { _hit_is_rvs = value; } }
        public string Hti_acc_num { get { return _hti_acc_num; } set { _hti_acc_num = value; } }
        public decimal Hti_comm_rt { get { return _hti_comm_rt; } set { _hti_comm_rt = value; } }
        public decimal Hti_comm_val { get { return _hti_comm_val; } set { _hti_comm_val = value; } }
        public string Hti_cre_by { get { return _hti_cre_by; } set { _hti_cre_by = value; } }
        public DateTime Hti_cre_dt { get { return _hti_cre_dt; } set { _hti_cre_dt = value; } }
        public DateTime Hti_dt { get { return _hti_dt; } set { _hti_dt = value; } }
        public decimal Hti_epf { get { return _hti_epf; } set { _hti_epf = value; } }
        public decimal Hti_esd { get { return _hti_esd; } set { _hti_esd = value; } }
        public decimal Hti_ins_val { get { return _hti_ins_val; } set { _hti_ins_val = value; } }
        public string Hti_mnl_num { get { return _hti_mnl_num; } set { _hti_mnl_num = value; } }
        public string Hti_pc { get { return _hti_pc; } set { _hti_pc = value; } }
        public string Hti_ref { get { return _hti_ref; } set { _hti_ref = value; } }
        public decimal Hti_vat_rt { get { return _hti_vat_rt; } set { _hti_vat_rt = value; } }
        public decimal Hti_vat_val { get { return _hti_vat_val; } set { _hti_vat_val = value; } }
        public decimal Hti_wht { get { return _hti_wht; } set { _hti_wht = value; } }
        public Int32 Hti_seq { get { return _hti_seq; } set { _hti_seq = value; } }
        public string Hti_com { get { return _hti_com; } set { _hti_com = value; } }

        public static HpInsurance Converter(DataRow row)
        {
            return new HpInsurance
            {
                Hit_is_rvs = row["HIT_IS_RVS"] == DBNull.Value ? false : Convert.ToBoolean(row["HIT_IS_RVS"]),
                Hti_acc_num = row["HTI_ACC_NUM"] == DBNull.Value ? string.Empty : row["HTI_ACC_NUM"].ToString(),
                Hti_comm_rt = row["HTI_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTI_COMM_RT"]),
                Hti_comm_val = row["HTI_COMM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTI_COMM_VAL"]),
                Hti_cre_by = row["HTI_CRE_BY"] == DBNull.Value ? string.Empty : row["HTI_CRE_BY"].ToString(),
                Hti_cre_dt = row["HTI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HTI_CRE_DT"]),
                Hti_dt = row["HTI_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HTI_DT"]),
                Hti_epf = row["HTI_EPF"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTI_EPF"]),
                Hti_esd = row["HTI_ESD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTI_ESD"]),
                Hti_ins_val = row["HTI_INS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTI_INS_VAL"]),
                Hti_mnl_num = row["HTI_MNL_NUM"] == DBNull.Value ? string.Empty : row["HTI_MNL_NUM"].ToString(),
                Hti_pc = row["HTI_PC"] == DBNull.Value ? string.Empty : row["HTI_PC"].ToString(),
                Hti_ref = row["HTI_REF"] == DBNull.Value ? string.Empty : row["HTI_REF"].ToString(),
                Hti_vat_rt = row["HTI_VAT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTI_VAT_RT"]),
                Hti_vat_val = row["HTI_VAT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTI_VAT_VAL"]),
                Hti_wht = row["HTI_WHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HTI_WHT"]),
                Hti_seq = row["HTI_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HTI_SEQ"]),
                Hti_com = row["HTI_COM"] == DBNull.Value ? string.Empty : row["HTI_COM"].ToString()
            };
        }
    }
}

