using System;
using System.Data;

namespace FF.BusinessObjects
{

    public class Service_Charge
    {
        private Boolean _scg_act;
        private string _scg_com;
        private decimal _scg_cost;
        private string _scg_cre_by;
        private DateTime _scg_cre_dt;
        private DateTime _scg_from_dt;
        private decimal _scg_from_val;
        private Boolean _scg_is_mand;
        private string _scg_itm_cd;
        private decimal _scg_js;
        private string _scg_mod_by;
        private DateTime _scg_mod_dt;
        private string _scg_pty_cd;
        private string _scg_pty_tp;
        private decimal _scg_rate;
        private Boolean _scg_read_sub;
        private Int32 _scg_seq;
        private string _scg_task_loc;
        private DateTime _scg_to_dt;
        private decimal _scg_to_val;
        private string _scg_uom_val;

        private string _mi_shortdesc;

        public string Mi_shortdesc
        {
            get { return _mi_shortdesc; }
            set { _mi_shortdesc = value; }
        }

        public Boolean Scg_act
        {
            get { return _scg_act; }
            set { _scg_act = value; }
        }
        public string Scg_com
        {
            get { return _scg_com; }
            set { _scg_com = value; }
        }
        public decimal Scg_cost
        {
            get { return _scg_cost; }
            set { _scg_cost = value; }
        }
        public string Scg_cre_by
        {
            get { return _scg_cre_by; }
            set { _scg_cre_by = value; }
        }
        public DateTime Scg_cre_dt
        {
            get { return _scg_cre_dt; }
            set { _scg_cre_dt = value; }
        }
        public DateTime Scg_from_dt
        {
            get { return _scg_from_dt; }
            set { _scg_from_dt = value; }
        }
        public decimal Scg_from_val
        {
            get { return _scg_from_val; }
            set { _scg_from_val = value; }
        }
        public Boolean Scg_is_mand
        {
            get { return _scg_is_mand; }
            set { _scg_is_mand = value; }
        }
        public string Scg_itm_cd
        {
            get { return _scg_itm_cd; }
            set { _scg_itm_cd = value; }
        }
        public decimal Scg_js
        {
            get { return _scg_js; }
            set { _scg_js = value; }
        }
        public string Scg_mod_by
        {
            get { return _scg_mod_by; }
            set { _scg_mod_by = value; }
        }
        public DateTime Scg_mod_dt
        {
            get { return _scg_mod_dt; }
            set { _scg_mod_dt = value; }
        }
        public string Scg_pty_cd
        {
            get { return _scg_pty_cd; }
            set { _scg_pty_cd = value; }
        }
        public string Scg_pty_tp
        {
            get { return _scg_pty_tp; }
            set { _scg_pty_tp = value; }
        }
        public decimal Scg_rate
        {
            get { return _scg_rate; }
            set { _scg_rate = value; }
        }
        public Boolean Scg_read_sub
        {
            get { return _scg_read_sub; }
            set { _scg_read_sub = value; }
        }
        public Int32 Scg_seq
        {
            get { return _scg_seq; }
            set { _scg_seq = value; }
        }
        public string Scg_task_loc
        {
            get { return _scg_task_loc; }
            set { _scg_task_loc = value; }
        }
        public DateTime Scg_to_dt
        {
            get { return _scg_to_dt; }
            set { _scg_to_dt = value; }
        }
        public decimal Scg_to_val
        {
            get { return _scg_to_val; }
            set { _scg_to_val = value; }
        }
        public string Scg_uom_val
        {
            get { return _scg_uom_val; }
            set { _scg_uom_val = value; }
        }

        public static Service_Charge Converter_1(DataRow row)
        {
            return new Service_Charge
            {
                Scg_rate = row["SCG_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCG_RATE"]),
                Scg_is_mand = row["SCG_IS_MAND"] == DBNull.Value ? false : Convert.ToBoolean(row["SCG_IS_MAND"]),
                Mi_shortdesc = row["Mi_shortdesc"] == DBNull.Value ? string.Empty : row["Mi_shortdesc"].ToString()
            };
        }

        public static Service_Charge Converter(DataRow row)
        {
            return new Service_Charge
            {
                Scg_act = row["SCG_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SCG_ACT"]),
                Scg_com = row["SCG_COM"] == DBNull.Value ? string.Empty : row["SCG_COM"].ToString(),
                Scg_cost = row["SCG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCG_COST"]),
                Scg_cre_by = row["SCG_CRE_BY"] == DBNull.Value ? string.Empty : row["SCG_CRE_BY"].ToString(),
                Scg_cre_dt = row["SCG_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCG_CRE_DT"]),
                Scg_from_dt = row["SCG_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCG_FROM_DT"]),
                Scg_from_val = row["SCG_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCG_FROM_VAL"]),
                Scg_is_mand = row["SCG_IS_MAND"] == DBNull.Value ? false : Convert.ToBoolean(row["SCG_IS_MAND"]),
                Scg_itm_cd = row["SCG_ITM_CD"] == DBNull.Value ? string.Empty : row["SCG_ITM_CD"].ToString(),
                Scg_js = row["SCG_JS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCG_JS"]),
                Scg_mod_by = row["SCG_MOD_BY"] == DBNull.Value ? string.Empty : row["SCG_MOD_BY"].ToString(),
                Scg_mod_dt = row["SCG_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCG_MOD_DT"]),
                Scg_pty_cd = row["SCG_PTY_CD"] == DBNull.Value ? string.Empty : row["SCG_PTY_CD"].ToString(),
                Scg_pty_tp = row["SCG_PTY_TP"] == DBNull.Value ? string.Empty : row["SCG_PTY_TP"].ToString(),
                Scg_rate = row["SCG_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCG_RATE"]),
                Scg_read_sub = row["SCG_READ_SUB"] == DBNull.Value ? false : Convert.ToBoolean(row["SCG_READ_SUB"]),
                Scg_seq = row["SCG_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCG_SEQ"]),
                Scg_task_loc = row["SCG_TASK_LOC"] == DBNull.Value ? string.Empty : row["SCG_TASK_LOC"].ToString(),
                Scg_to_dt = row["SCG_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCG_TO_DT"]),
                Scg_to_val = row["SCG_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCG_TO_VAL"]),
                Scg_uom_val = row["SCG_UOM_VAL"] == DBNull.Value ? string.Empty : row["SCG_UOM_VAL"].ToString()

            };
        }

    }


}
