using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class LoyaltyMemeber
    {//
        #region Private Members
        private string _salcm_app_by;
        private DateTime _salcm_app_dt;
        private decimal _salcm_bal_pt;
        private string _salcm_cd_ser;
        private decimal _salcm_col_pt;
        private string _salcm_contact;
        private string _salcm_cre_by;
        private DateTime _salcm_cre_dt;
        private string _salcm_cus_cd;
        private string _salcm_cus_spec;
        private decimal _salcm_dis_rt;
        private string _salcm_email;
        private decimal _salcm_exp_pt;
        private string _salcm_loty_tp;
        private string _salcm_no;
        private decimal _salcm_red_pt;
        private DateTime _salcm_val_frm;
        private DateTime _salcm_val_to;
        private Int32 _salcm_act;
        #endregion

        public Int32 Salcm_act
        {
            get { return _salcm_act; }
            set { _salcm_act = value; }
        }
        public string Salcm_app_by
        {
            get { return _salcm_app_by; }
            set { _salcm_app_by = value; }
        }
        public DateTime Salcm_app_dt
        {
            get { return _salcm_app_dt; }
            set { _salcm_app_dt = value; }
        }
        public decimal Salcm_bal_pt
        {
            get { return _salcm_bal_pt; }
            set { _salcm_bal_pt = value; }
        }
        public string Salcm_cd_ser
        {
            get { return _salcm_cd_ser; }
            set { _salcm_cd_ser = value; }
        }
        public decimal Salcm_col_pt
        {
            get { return _salcm_col_pt; }
            set { _salcm_col_pt = value; }
        }
        public string Salcm_contact
        {
            get { return _salcm_contact; }
            set { _salcm_contact = value; }
        }
        public string Salcm_cre_by
        {
            get { return _salcm_cre_by; }
            set { _salcm_cre_by = value; }
        }
        public DateTime Salcm_cre_dt
        {
            get { return _salcm_cre_dt; }
            set { _salcm_cre_dt = value; }
        }
        public string Salcm_cus_cd
        {
            get { return _salcm_cus_cd; }
            set { _salcm_cus_cd = value; }
        }
        public string Salcm_cus_spec
        {
            get { return _salcm_cus_spec; }
            set { _salcm_cus_spec = value; }
        }
        public decimal Salcm_dis_rt
        {
            get { return _salcm_dis_rt; }
            set { _salcm_dis_rt = value; }
        }
        public string Salcm_email
        {
            get { return _salcm_email; }
            set { _salcm_email = value; }
        }
        public decimal Salcm_exp_pt
        {
            get { return _salcm_exp_pt; }
            set { _salcm_exp_pt = value; }
        }
        public string Salcm_loty_tp
        {
            get { return _salcm_loty_tp; }
            set { _salcm_loty_tp = value; }
        }
        public string Salcm_no
        {
            get { return _salcm_no; }
            set { _salcm_no = value; }
        }
        public decimal Salcm_red_pt
        {
            get { return _salcm_red_pt; }
            set { _salcm_red_pt = value; }
        }
        public DateTime Salcm_val_frm
        {
            get { return _salcm_val_frm; }
            set { _salcm_val_frm = value; }
        }
        public DateTime Salcm_val_to
        {
            get { return _salcm_val_to; }
            set { _salcm_val_to = value; }
        }

        public static LoyaltyMemeber Converter(DataRow row)
        {
            return new LoyaltyMemeber
            {
                Salcm_app_by = row["SALCM_APP_BY"] == DBNull.Value ? string.Empty : row["SALCM_APP_BY"].ToString(),
                Salcm_app_dt = row["SALCM_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALCM_APP_DT"]),
                Salcm_bal_pt = row["SALCM_BAL_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALCM_BAL_PT"]),
                Salcm_cd_ser = row["SALCM_CD_SER"] == DBNull.Value ? string.Empty : row["SALCM_CD_SER"].ToString(),
                Salcm_col_pt = row["SALCM_COL_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALCM_COL_PT"]),
                Salcm_contact = row["SALCM_CONTACT"] == DBNull.Value ? string.Empty : row["SALCM_CONTACT"].ToString(),
                Salcm_cre_by = row["SALCM_CRE_BY"] == DBNull.Value ? string.Empty : row["SALCM_CRE_BY"].ToString(),
                Salcm_cre_dt = row["SALCM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALCM_CRE_DT"]),
                Salcm_cus_cd = row["SALCM_CUS_CD"] == DBNull.Value ? string.Empty : row["SALCM_CUS_CD"].ToString(),
                Salcm_cus_spec = row["SALCM_CUS_SPEC"] == DBNull.Value ? string.Empty : row["SALCM_CUS_SPEC"].ToString(),
                Salcm_dis_rt = row["SALCM_DIS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALCM_DIS_RT"]),
                Salcm_email = row["SALCM_EMAIL"] == DBNull.Value ? string.Empty : row["SALCM_EMAIL"].ToString(),
                Salcm_exp_pt = row["SALCM_EXP_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALCM_EXP_PT"]),
                Salcm_loty_tp = row["SALCM_LOTY_TP"] == DBNull.Value ? string.Empty : row["SALCM_LOTY_TP"].ToString(),
                Salcm_no = row["SALCM_NO"] == DBNull.Value ? string.Empty : row["SALCM_NO"].ToString(),
                Salcm_red_pt = row["SALCM_RED_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALCM_RED_PT"]),
                Salcm_val_frm = row["SALCM_VAL_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALCM_VAL_FRM"]),
                Salcm_val_to = row["SALCM_VAL_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALCM_VAL_TO"]),
                Salcm_act = row["Salcm_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["Salcm_act"])

            };
        }


    }
}

