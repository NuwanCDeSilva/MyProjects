using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpSchemeDetails
    {
        #region Private Members
        private Boolean _hsd_act;
        private Boolean _hsd_add_calwithvat;
        private Boolean _hsd_add_is_rt;
        private decimal _hsd_add_rnt;
        private Boolean _hsd_alw_gs;
        private string _hsd_cd;
        private string _hsd_cre_by;
        private DateTime _hsd_cre_dt;
        private decimal _hsd_def_intr;
        private string _hsd_desc;
        private decimal _hsd_dis;
        private Boolean _hsd_dis_isrt;
        private decimal _hsd_fpay;
        private Boolean _hsd_fpay_calwithvat;
        private Boolean _hsd_fpay_withvat;
        private Boolean _hsd_has_insu;
        private Boolean _hsd_init_insu;
        private Boolean _hsd_init_sduty;
        private Boolean _hsd_init_serchg;
        private decimal _hsd_intr_rt;
        private Boolean _hsd_is_rt;
        private string _hsd_pty_cd;
        private string _hsd_pty_tp;
        private string _hsd_sch_tp;
        private Int32 _hsd_term;
        private Int32 _hsd_noof_addrnt;
        private Int32 _hsd_insu_term;
        private Int32 _hsd_veh_insu_term;
        private Boolean _hsd_comm_on_vat;
        private Int32 _hsd_veh_insu_col_term;
        private Boolean _hsd_alw_vou;
        private Boolean _hsd_alw_cus;
        private Boolean _hsd_vou_man;
        private Boolean _hsd_is_red;
        private Int32 _hsd_is_rvt; // Nadeeka
        private Int32 _hsd_spc_vou; //Sanjeewa 2016-06-01
        private Int32 _hsd_vou_gen; //Sanjeewa 2016-06-01

        public Int32 _hsd_is_com_ser_chg; // tAHRINDU
        #endregion

        public Boolean Hsd_is_red
        {
            get { return _hsd_is_red; }
            set { _hsd_is_red = value; }
        }
        public Boolean Hsd_vou_man
        {
            get { return _hsd_vou_man; }
            set { _hsd_vou_man = value; }
        }

        public Boolean Hsd_alw_vou
        {
            get { return _hsd_alw_vou; }
            set { _hsd_alw_vou = value; }
        }

        public Boolean Hsd_alw_cus
        {
            get { return _hsd_alw_cus; }
            set { _hsd_alw_cus = value; }
        }

        public Boolean Hsd_act
        {
            get { return _hsd_act; }
            set { _hsd_act = value; }
        }
        public Boolean Hsd_add_calwithvat
        {
            get { return _hsd_add_calwithvat; }
            set { _hsd_add_calwithvat = value; }
        }
        public Boolean Hsd_add_is_rt
        {
            get { return _hsd_add_is_rt; }
            set { _hsd_add_is_rt = value; }
        }
        public decimal Hsd_add_rnt
        {
            get { return _hsd_add_rnt; }
            set { _hsd_add_rnt = value; }
        }
        public Boolean Hsd_alw_gs
        {
            get { return _hsd_alw_gs; }
            set { _hsd_alw_gs = value; }
        }
        public string Hsd_cd
        {
            get { return _hsd_cd; }
            set { _hsd_cd = value; }
        }
        public string Hsd_cre_by
        {
            get { return _hsd_cre_by; }
            set { _hsd_cre_by = value; }
        }
        public DateTime Hsd_cre_dt
        {
            get { return _hsd_cre_dt; }
            set { _hsd_cre_dt = value; }
        }
        public decimal Hsd_def_intr
        {
            get { return _hsd_def_intr; }
            set { _hsd_def_intr = value; }
        }
        public string Hsd_desc
        {
            get { return _hsd_desc; }
            set { _hsd_desc = value; }
        }
        public decimal Hsd_dis
        {
            get { return _hsd_dis; }
            set { _hsd_dis = value; }
        }
        public Boolean Hsd_dis_isrt
        {
            get { return _hsd_dis_isrt; }
            set { _hsd_dis_isrt = value; }
        }
        public decimal Hsd_fpay
        {
            get { return _hsd_fpay; }
            set { _hsd_fpay = value; }
        }
        public Boolean Hsd_fpay_calwithvat
        {
            get { return _hsd_fpay_calwithvat; }
            set { _hsd_fpay_calwithvat = value; }
        }
        public Boolean Hsd_fpay_withvat
        {
            get { return _hsd_fpay_withvat; }
            set { _hsd_fpay_withvat = value; }
        }
        public Boolean Hsd_has_insu
        {
            get { return _hsd_has_insu; }
            set { _hsd_has_insu = value; }
        }
        public Boolean Hsd_init_insu
        {
            get { return _hsd_init_insu; }
            set { _hsd_init_insu = value; }
        }
        public Boolean Hsd_init_sduty
        {
            get { return _hsd_init_sduty; }
            set { _hsd_init_sduty = value; }
        }
        public Boolean Hsd_init_serchg
        {
            get { return _hsd_init_serchg; }
            set { _hsd_init_serchg = value; }
        }
        public decimal Hsd_intr_rt
        {
            get { return _hsd_intr_rt; }
            set { _hsd_intr_rt = value; }
        }
        public Boolean Hsd_is_rt
        {
            get { return _hsd_is_rt; }
            set { _hsd_is_rt = value; }
        }
        public string Hsd_pty_cd
        {
            get { return _hsd_pty_cd; }
            set { _hsd_pty_cd = value; }
        }
        public string Hsd_pty_tp
        {
            get { return _hsd_pty_tp; }
            set { _hsd_pty_tp = value; }
        }
        public string Hsd_sch_tp
        {
            get { return _hsd_sch_tp; }
            set { _hsd_sch_tp = value; }
        }
        public Int32 Hsd_term
        {
            get { return _hsd_term; }
            set { _hsd_term = value; }
        }
        public Int32 Hsd_noof_addrnt
        {
            get { return _hsd_noof_addrnt; }
            set { _hsd_noof_addrnt = value; }
        }

        public Int32 Hsd_insu_term
        {
            get { return _hsd_insu_term; }
            set { _hsd_insu_term = value; }
        }

        public Int32 Hsd_veh_insu_term
        {
            get { return _hsd_veh_insu_term; }
            set { _hsd_veh_insu_term = value; }
        }

        public Boolean Hsd_comm_on_vat
        {
            get { return _hsd_comm_on_vat; }
            set { _hsd_comm_on_vat = value; }
        }

        public Int32 Hsd_veh_insu_col_term
        {
            get { return _hsd_veh_insu_col_term; }
            set { _hsd_veh_insu_col_term = value; }
        }

        public Int32 Hsd_is_rvt
        {
            get { return _hsd_is_rvt; }
            set { _hsd_is_rvt = value; }
        }
        public Int32 Hsd_spc_vou
        {
            get { return _hsd_spc_vou; }
            set { _hsd_spc_vou = value; }
        }
        public Int32 Hsd_vou_gen
        {
            get { return _hsd_vou_gen; }
            set { _hsd_vou_gen = value; }
        }

        public static HpSchemeDetails Converter(DataRow row)
        {
            return new HpSchemeDetails
            {
                Hsd_act = row["HSD_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_ACT"]),
                Hsd_add_calwithvat = row["HSD_ADD_CALWITHVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_ADD_CALWITHVAT"]),
                Hsd_add_is_rt = row["HSD_ADD_IS_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_ADD_IS_RT"]),
                Hsd_add_rnt = row["HSD_ADD_RNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSD_ADD_RNT"]),
                Hsd_alw_gs = row["HSD_ALW_GS"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_ALW_GS"]),
                Hsd_cd = row["HSD_CD"] == DBNull.Value ? string.Empty : row["HSD_CD"].ToString(),
                Hsd_cre_by = row["HSD_CRE_BY"] == DBNull.Value ? string.Empty : row["HSD_CRE_BY"].ToString(),
                Hsd_cre_dt = row["HSD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSD_CRE_DT"]),
                Hsd_def_intr = row["HSD_DEF_INTR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSD_DEF_INTR"]),
                Hsd_desc = row["HSD_DESC"] == DBNull.Value ? string.Empty : row["HSD_DESC"].ToString(),
                Hsd_dis = row["HSD_DIS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSD_DIS"]),
                Hsd_dis_isrt = row["HSD_DIS_ISRT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_DIS_ISRT"]),
                Hsd_fpay = row["HSD_FPAY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSD_FPAY"]),
                Hsd_fpay_calwithvat = row["HSD_FPAY_CALWITHVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_FPAY_CALWITHVAT"]),
                Hsd_fpay_withvat = row["HSD_FPAY_WITHVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_FPAY_WITHVAT"]),
                Hsd_has_insu = row["HSD_HAS_INSU"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_HAS_INSU"]),
                Hsd_init_insu = row["HSD_INIT_INSU"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_INIT_INSU"]),
                Hsd_init_sduty = row["HSD_INIT_SDUTY"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_INIT_SDUTY"]),
                Hsd_init_serchg = row["HSD_INIT_SERCHG"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_INIT_SERCHG"]),
                Hsd_intr_rt = row["HSD_INTR_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSD_INTR_RT"]),
                Hsd_is_rt = row["HSD_IS_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_IS_RT"]),
                Hsd_pty_cd = row["HSD_PTY_CD"] == DBNull.Value ? string.Empty : row["HSD_PTY_CD"].ToString(),
                Hsd_pty_tp = row["HSD_PTY_TP"] == DBNull.Value ? string.Empty : row["HSD_PTY_TP"].ToString(),
                Hsd_sch_tp = row["HSD_SCH_TP"] == DBNull.Value ? string.Empty : row["HSD_SCH_TP"].ToString(),
                Hsd_term = row["HSD_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSD_TERM"]),
                Hsd_noof_addrnt = row["HSD_NOOF_ADDRNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSD_NOOF_ADDRNT"]),
                Hsd_insu_term = row["HSD_INSU_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSD_INSU_TERM"]),
                Hsd_veh_insu_term = row["HSD_VEH_INSU_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSD_VEH_INSU_TERM"]),
                Hsd_comm_on_vat = row["HSD_COMM_ON_VAT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_COMM_ON_VAT"]),
                Hsd_veh_insu_col_term = row["HSD_VEH_INSU_COL_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSD_VEH_INSU_COL_TERM"]),
                Hsd_alw_vou =  row["HSD_ALW_VOU"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_ALW_VOU"]),
                Hsd_alw_cus = row["HSD_ALW_CUS"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_ALW_CUS"]),
                Hsd_vou_man = row["HSD_VOU_MAN"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_VOU_MAN"]),
                Hsd_is_red = row["HSD_IS_RED"] == DBNull.Value ? false : Convert.ToBoolean(row["HSD_IS_RED"]),
                Hsd_is_rvt = row["HSD_IS_RVT"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSD_IS_RVT"]),
                Hsd_spc_vou = row["HSD_SPC_VOU"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSD_SPC_VOU"]),
                Hsd_vou_gen = row["HSD_VOU_GEN"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSD_VOU_GEN"]),

                _hsd_is_com_ser_chg = row["hsd_comm_on_serchg"] == DBNull.Value ? 0 : Convert.ToInt32(row["hsd_comm_on_serchg"])
                
            };
        }
    }

}
