using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class VehicalInsuranceClaim
    {
        #region members

        int seq_no;
        string reg_no;
        DateTime acc_date;
        bool polic_rep_sts;
        string dri_name;
        string dl_lic;
        bool doc_stus;
        string init_by;
        DateTime init_date;
        DateTime clamin_form_rec;
        DateTime dl_rec;
        DateTime repi_esti_rec;
        decimal repi_esti_val;
        DateTime police_rep_rec;
        DateTime app_forw;
        DateTime final_invo;
        string rec_by;
        DateTime rec_dt;
        decimal claim_val;
        decimal policy_excess;
        decimal other_dedection;
        decimal bal_val;
        string acc_no;
        string cheq_no;
        string cheq_bank;
        string cheq_branch;
        DateTime cheq_dt;
        decimal cheq_val;
        DateTime sett_dt;
        string set_by;
        DateTime set_dt;

        #endregion

        #region properties

           public int Seq_no
           {
               get { return seq_no; }
               set { seq_no = value; }
           }
           public string Reg_no
           {
               get { return reg_no; }
               set { reg_no = value; }
           }
           public DateTime Acc_date
           {
               get { return acc_date; }
               set { acc_date = value; }
           }
           public bool Polic_rep_sts
           {
               get { return polic_rep_sts; }
               set { polic_rep_sts = value; }
           }
           public string Dri_name
           {
               get { return dri_name; }
               set { dri_name = value; }
           }
           public string Dl_lic
           {
               get { return dl_lic; }
               set { dl_lic = value; }
           }
           public bool Doc_stus
           {
               get { return doc_stus; }
               set { doc_stus = value; }
           }
           public string Init_by
           {
               get { return init_by; }
               set { init_by = value; }
           }
           public DateTime Init_date
           {
               get { return init_date; }
               set { init_date = value; }
           }
           public DateTime Clamin_form_rec
           {
               get { return clamin_form_rec; }
               set { clamin_form_rec = value; }
           }
           public DateTime Dl_rec
           {
               get { return dl_rec; }
               set { dl_rec = value; }
           }
           public DateTime Repi_esti_rec
           {
               get { return repi_esti_rec; }
               set { repi_esti_rec = value; }
           }
           public decimal Repi_esti_val
           {
               get { return repi_esti_val; }
               set { repi_esti_val = value; }
           }
           public DateTime Police_rep_rec
           {
               get { return police_rep_rec; }
               set { police_rep_rec = value; }
           }
           public DateTime App_forw
           {
               get { return app_forw; }
               set { app_forw = value; }
           }
           public DateTime Final_invo
           {
               get { return final_invo; }
               set { final_invo = value; }
           }
           public string Rec_by
           {
               get { return rec_by; }
               set { rec_by = value; }
           }
           public DateTime Rec_dt
           {
               get { return rec_dt; }
               set { rec_dt = value; }
           }
           public decimal Claim_val
           {
               get { return claim_val; }
               set { claim_val = value; }
           }
           public decimal Policy_excess
           {
               get { return policy_excess; }
               set { policy_excess = value; }
           }
           public decimal Other_dedection
           {
               get { return other_dedection; }
               set { other_dedection = value; }
           }
           public decimal Bal_val
           {
               get { return bal_val; }
               set { bal_val = value; }
           }
           public string Acc_no
           {
               get { return acc_no; }
               set { acc_no = value; }
           }
           public string Cheq_no
           {
               get { return cheq_no; }
               set { cheq_no = value; }
           }

           public string Cheq_bank
           {
               get { return cheq_bank; }
               set { cheq_bank = value; }
           }
           public string Cheq_branch
           {
               get { return cheq_branch; }
               set { cheq_branch = value; }
           }
           public DateTime Cheq_dt
           {
               get { return cheq_dt; }
               set { cheq_dt = value; }
           }
           public decimal Cheq_val
           {
               get { return cheq_val; }
               set { cheq_val = value; }
           }
           public DateTime Sett_dt
           {
               get { return sett_dt; }
               set { sett_dt = value; }
           }
           public string Set_by
           {
               get { return set_by; }
               set { set_by = value; }
           }
           public DateTime Set_dt
           {
               get { return set_dt; }
               set { set_dt = value; }
           }
        
        #endregion

       public static VehicalInsuranceClaim Converter(DataRow row)
       {
           return new VehicalInsuranceClaim
           {
               Seq_no = ((row["SVICL_SEQ"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SVICL_SEQ"].ToString())),
               Reg_no = ((row["SVICL_VEH_REG_NO"] == DBNull.Value) ? string.Empty : row["SVICL_VEH_REG_NO"].ToString()),
               Acc_date = ((row["SVICL_ACDT_DT"] == DBNull.Value) ? DateTime.MinValue :Convert.ToDateTime(row["SVICL_ACDT_DT"].ToString())),
               Polic_rep_sts = ((row["SVICL_POLICE_RPT_STUS"] == DBNull.Value) ? false : Convert.ToBoolean(row["SVICL_POLICE_RPT_STUS"])),
               Dri_name = ((row["SVICL_DRV_NAME"] == DBNull.Value) ? string.Empty : row["SVICL_DRV_NAME"].ToString()),
               Dl_lic = ((row["SVICL_DL_NO"] == DBNull.Value) ? string.Empty : row["SVICL_DL_NO"].ToString()),
               Doc_stus = ((row["SVICL_DOC_STUS"] == DBNull.Value) ? false : Convert.ToBoolean(row["SVICL_DOC_STUS"])),
               Init_by = ((row["SVICL_INIT_BY"] == DBNull.Value) ? string.Empty : row["SVICL_INIT_BY"].ToString()),
               Init_date = ((row["SVICL_INIT_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_INIT_DT"].ToString())),
               Clamin_form_rec = ((row["SVICL_CLAIM_FORM_REC_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_CLAIM_FORM_REC_DT"].ToString())),
               Dl_rec = ((row["SVICL_DL_REC_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_DL_REC_DT"].ToString())),
               Repi_esti_rec = ((row["SVICL_REPAIR_ESTM_REC_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_REPAIR_ESTM_REC_DT"].ToString())),
               Repi_esti_val = ((row["SVICL_REPAIR_ESTM_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVICL_REPAIR_ESTM_VAL"].ToString())),
               Police_rep_rec = ((row["SVICL_POLICE_RPT_REC_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_POLICE_RPT_REC_DT"].ToString())),
               App_forw = ((row["SVICL_APP_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_APP_DT"].ToString())),
               Final_invo = ((row["SVICL_INV_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_INV_DT"].ToString())),
               Rec_by = ((row["SVICL_REC_BY"] == DBNull.Value) ? string.Empty : row["SVICL_REC_BY"].ToString()),
               Rec_dt = ((row["SVICL_REC_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_REC_DT"].ToString())),
               Claim_val = ((row["SVICL_CLAIM_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVICL_CLAIM_VAL"].ToString())),
               Policy_excess = ((row["SVICL_POLC_EXCESS"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVICL_POLC_EXCESS"].ToString())),
               Other_dedection = ((row["SVICL_OTH_DEDUCTION"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVICL_OTH_DEDUCTION"].ToString())),
               Bal_val = ((row["SVICL_BAL_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVICL_BAL_VAL"].ToString())),
               Acc_no = ((row["SVICL_ACC_NO"] == DBNull.Value) ? string.Empty : row["SVICL_ACC_NO"].ToString()),
               Cheq_no = ((row["SVICL_CHQ_NO"] == DBNull.Value) ? string.Empty : row["SVICL_CHQ_NO"].ToString()),
               Cheq_bank = ((row["SVICL_CHQ_BANK"] == DBNull.Value) ? string.Empty : row["SVICL_CHQ_BANK"].ToString()),
               Cheq_branch = ((row["SVICL_CHQ_BRANCH"] == DBNull.Value) ? string.Empty : row["SVICL_CHQ_BRANCH"].ToString()),
               Cheq_dt = ((row["SVICL_CHQ_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_CHQ_DT"].ToString())),
               Cheq_val = ((row["SVICL_CHQ_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVICL_CHQ_VAL"].ToString())),
               Sett_dt = ((row["SVICL_SETT_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_SETT_DT"].ToString())),
               Set_by = ((row["SVICL_SET_BY"] == DBNull.Value) ? string.Empty : row["SVICL_SET_BY"].ToString()),
               Set_dt = ((row["SVICL_SET_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVICL_SET_DT"].ToString())),
           };
       }
    }
}
