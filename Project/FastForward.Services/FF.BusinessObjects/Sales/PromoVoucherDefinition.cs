using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
//By Chamal 19-Jun-2014
namespace FF.BusinessObjects
{
    [Serializable]
    public class PromoVoucherDefinition
    {
        #region Private Members
        private string _spd_brd;
        private string _spd_cat;
        private string _spd_circular_no;
        private string _spd_com;
        private string _spd_cre_by;
        private DateTime _spd_cre_dt;
        private decimal _spd_disc;
        private Boolean _spd_disc_isrt;
        private DateTime _spd_from_dt;
        private string _spd_itm;
        private string _spd_main_cat;
        private string _spd_mod_by;
        private DateTime _spd_mod_dt;
        private string _spd_pb;
        private string _spd_pb_lvl;
        private string _spd_pty_cd;
        private string _spd_pty_tp;
        private string _spd_sale_tp;
        private Int32 _spd_seq;
        private Boolean _spd_stus;
        private DateTime _spd_to_dt;
        private string _spd_vou_cd;
        private Int32 _spd_period;
        private string _spd_rdm_com;
        private string _spd_rdm_pb;
        private string _spd_rdm_pb_lvl;
        private Int32 _sPD_BATCH_SEQ;

        #endregion

        public string Spd_brd
        {
            get { return _spd_brd; }
            set { _spd_brd = value; }
        }
        public string Spd_cat
        {
            get { return _spd_cat; }
            set { _spd_cat = value; }
        }
        public string Spd_circular_no
        {
            get { return _spd_circular_no; }
            set { _spd_circular_no = value; }
        }
        public string Spd_com
        {
            get { return _spd_com; }
            set { _spd_com = value; }
        }
        public string Spd_cre_by
        {
            get { return _spd_cre_by; }
            set { _spd_cre_by = value; }
        }
        public DateTime Spd_cre_dt
        {
            get { return _spd_cre_dt; }
            set { _spd_cre_dt = value; }
        }
        public decimal Spd_disc
        {
            get { return _spd_disc; }
            set { _spd_disc = value; }
        }
        public Boolean Spd_disc_isrt
        {
            get { return _spd_disc_isrt; }
            set { _spd_disc_isrt = value; }
        }
        public DateTime Spd_from_dt
        {
            get { return _spd_from_dt; }
            set { _spd_from_dt = value; }
        }
        public string Spd_itm
        {
            get { return _spd_itm; }
            set { _spd_itm = value; }
        }
        public string Spd_main_cat
        {
            get { return _spd_main_cat; }
            set { _spd_main_cat = value; }
        }
        public string Spd_mod_by
        {
            get { return _spd_mod_by; }
            set { _spd_mod_by = value; }
        }
        public DateTime Spd_mod_dt
        {
            get { return _spd_mod_dt; }
            set { _spd_mod_dt = value; }
        }
        public string Spd_pb
        {
            get { return _spd_pb; }
            set { _spd_pb = value; }
        }
        public string Spd_pb_lvl
        {
            get { return _spd_pb_lvl; }
            set { _spd_pb_lvl = value; }
        }
        public string Spd_pty_cd
        {
            get { return _spd_pty_cd; }
            set { _spd_pty_cd = value; }
        }
        public string Spd_pty_tp
        {
            get { return _spd_pty_tp; }
            set { _spd_pty_tp = value; }
        }
        public string Spd_sale_tp
        {
            get { return _spd_sale_tp; }
            set { _spd_sale_tp = value; }
        }
        public Int32 Spd_seq
        {
            get { return _spd_seq; }
            set { _spd_seq = value; }
        }
        public Boolean Spd_stus
        {
            get { return _spd_stus; }
            set { _spd_stus = value; }
        }
        public DateTime Spd_to_dt
        {
            get { return _spd_to_dt; }
            set { _spd_to_dt = value; }
        }
        public string Spd_vou_cd
        {
            get { return _spd_vou_cd; }
            set { _spd_vou_cd = value; }
        }
        public Int32 Spd_period
        {
            get { return _spd_period; }
            set { _spd_period = value; }
        }
        public string Spd_rdm_com
        {
            get { return _spd_rdm_com; }
            set { _spd_rdm_com = value; }
        }
        public string Spd_rdm_pb
        {
            get { return _spd_rdm_pb; }
            set { _spd_rdm_pb = value; }
        }
        public string Spd_rdm_pb_lvl
        {
            get { return _spd_rdm_pb_lvl; }
            set { _spd_rdm_pb_lvl = value; }
        }
        public Int32 SPD_BATCH_SEQ
        {
            get { return _sPD_BATCH_SEQ; }
            set { _sPD_BATCH_SEQ = value; }
        }



        public static PromoVoucherDefinition Converter(DataRow row)
        {
            return new PromoVoucherDefinition
            {
                Spd_brd = row["SPD_BRD"] == DBNull.Value ? string.Empty : row["SPD_BRD"].ToString(),
                Spd_cat = row["SPD_CAT"] == DBNull.Value ? string.Empty : row["SPD_CAT"].ToString(),
                Spd_circular_no = row["SPD_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SPD_CIRCULAR_NO"].ToString(),
                Spd_com = row["SPD_COM"] == DBNull.Value ? string.Empty : row["SPD_COM"].ToString(),
                Spd_cre_by = row["SPD_CRE_BY"] == DBNull.Value ? string.Empty : row["SPD_CRE_BY"].ToString(),
                Spd_cre_dt = row["SPD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPD_CRE_DT"]),
                Spd_disc = row["SPD_DISC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_DISC"]),
                Spd_disc_isrt = row["SPD_DISC_ISRT"] == DBNull.Value ? false : Convert.ToBoolean(row["SPD_DISC_ISRT"]),
                Spd_from_dt = row["SPD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPD_FROM_DT"]),
                Spd_itm = row["SPD_ITM"] == DBNull.Value ? string.Empty : row["SPD_ITM"].ToString(),
                Spd_main_cat = row["SPD_MAIN_CAT"] == DBNull.Value ? string.Empty : row["SPD_MAIN_CAT"].ToString(),
                Spd_mod_by = row["SPD_MOD_BY"] == DBNull.Value ? string.Empty : row["SPD_MOD_BY"].ToString(),
                Spd_mod_dt = row["SPD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPD_MOD_DT"]),
                Spd_pb = row["SPD_PB"] == DBNull.Value ? string.Empty : row["SPD_PB"].ToString(),
                Spd_pb_lvl = row["SPD_PB_LVL"] == DBNull.Value ? string.Empty : row["SPD_PB_LVL"].ToString(),
                Spd_pty_cd = row["SPD_PTY_CD"] == DBNull.Value ? string.Empty : row["SPD_PTY_CD"].ToString(),
                Spd_pty_tp = row["SPD_PTY_TP"] == DBNull.Value ? string.Empty : row["SPD_PTY_TP"].ToString(),
                Spd_sale_tp = row["SPD_SALE_TP"] == DBNull.Value ? string.Empty : row["SPD_SALE_TP"].ToString(),
                Spd_seq = row["SPD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_SEQ"]),
                Spd_stus = row["SPD_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SPD_STUS"]),
                Spd_to_dt = row["SPD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPD_TO_DT"]),
                Spd_vou_cd = row["SPD_VOU_CD"] == DBNull.Value ? string.Empty : row["SPD_VOU_CD"].ToString(),
                Spd_period = row["SPD_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_PERIOD"]),
                Spd_rdm_com = row["SPD_RDM_COM"] == DBNull.Value ? string.Empty : row["SPD_RDM_COM"].ToString(),
                Spd_rdm_pb = row["SPD_RDM_PB"] == DBNull.Value ? string.Empty : row["SPD_RDM_PB"].ToString(),
                Spd_rdm_pb_lvl = row["SPD_RDM_PB_LVL"] == DBNull.Value ? string.Empty : row["SPD_RDM_PB_LVL"].ToString(),
                SPD_BATCH_SEQ = row["SPD_BATCH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_BATCH_SEQ"])
            };
        }
    }
}

