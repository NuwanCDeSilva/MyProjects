using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class ManegerCreation
    {
        #region Private Members
        private DateTime _hmfa_acc_dt;
        private Boolean _hmfa_act_stus;
        private DateTime _hmfa_bonus_st_dt;
        private string _hmfa_com;
        private string _hmfa_cre_by;
        private DateTime _hmfa_cre_dt;
        private string _hmfa_mgr_cd;
        private string _hmfa_mod_by;
        private DateTime _hmfa_mod_dt;
        private string _hmfa_pc;
        private Int32 _hmfa_seq;
        private DateTime _hmfa_sr_open_dt;
        #endregion

        public DateTime Hmfa_acc_dt
        {
            get { return _hmfa_acc_dt; }
            set { _hmfa_acc_dt = value; }
        }
        public Boolean Hmfa_act_stus
        {
            get { return _hmfa_act_stus; }
            set { _hmfa_act_stus = value; }
        }
        public DateTime Hmfa_bonus_st_dt
        {
            get { return _hmfa_bonus_st_dt; }
            set { _hmfa_bonus_st_dt = value; }
        }
        public string Hmfa_com
        {
            get { return _hmfa_com; }
            set { _hmfa_com = value; }
        }
        public string Hmfa_cre_by
        {
            get { return _hmfa_cre_by; }
            set { _hmfa_cre_by = value; }
        }
        public DateTime Hmfa_cre_dt
        {
            get { return _hmfa_cre_dt; }
            set { _hmfa_cre_dt = value; }
        }
        public string Hmfa_mgr_cd
        {
            get { return _hmfa_mgr_cd; }
            set { _hmfa_mgr_cd = value; }
        }
        public string Hmfa_mod_by
        {
            get { return _hmfa_mod_by; }
            set { _hmfa_mod_by = value; }
        }
        public DateTime Hmfa_mod_dt
        {
            get { return _hmfa_mod_dt; }
            set { _hmfa_mod_dt = value; }
        }
        public string Hmfa_pc
        {
            get { return _hmfa_pc; }
            set { _hmfa_pc = value; }
        }
        public Int32 Hmfa_seq
        {
            get { return _hmfa_seq; }
            set { _hmfa_seq = value; }
        }
        public DateTime Hmfa_sr_open_dt
        {
            get { return _hmfa_sr_open_dt; }
            set { _hmfa_sr_open_dt = value; }
        }

        public static ManegerCreation Converter(DataRow row)
        {
            return new ManegerCreation
            {
                Hmfa_acc_dt = row["HMFA_ACC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_ACC_DT"]),
                Hmfa_act_stus = row["HMFA_ACT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["HMFA_ACT_STUS"]),
                Hmfa_bonus_st_dt = row["HMFA_BONUS_ST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_BONUS_ST_DT"]),
                Hmfa_com = row["HMFA_COM"] == DBNull.Value ? string.Empty : row["HMFA_COM"].ToString(),
                Hmfa_cre_by = row["HMFA_CRE_BY"] == DBNull.Value ? string.Empty : row["HMFA_CRE_BY"].ToString(),
                Hmfa_cre_dt = row["HMFA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_CRE_DT"]),
                Hmfa_mgr_cd = row["HMFA_MGR_CD"] == DBNull.Value ? string.Empty : row["HMFA_MGR_CD"].ToString(),
                Hmfa_mod_by = row["HMFA_MOD_BY"] == DBNull.Value ? string.Empty : row["HMFA_MOD_BY"].ToString(),
                Hmfa_mod_dt = row["HMFA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_MOD_DT"]),
                Hmfa_pc = row["HMFA_PC"] == DBNull.Value ? string.Empty : row["HMFA_PC"].ToString(),
                Hmfa_seq = row["HMFA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HMFA_SEQ"]),
                Hmfa_sr_open_dt = row["HMFA_SR_OPEN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_SR_OPEN_DT"])

            };
        }
    }

 }
