using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//by darshana
namespace FF.BusinessObjects
{
    [Serializable]
    public class HpSchemeDefinition
    {
        #region Private Members
        private string _hpc_brd;
        private string _hpc_cat;
        private string _hpc_cir_no;
        private string _hpc_comm_cat;
        private string _hpc_cre_by;
        private DateTime _hpc_cre_dt; 
        private string _hpc_cust_cd;
        private decimal _hpc_disc;
        private Boolean _hpc_disc_isrt;
        private decimal _hpc_dp_comm;
        private DateTime _hpc_from_dt;
        private decimal _hpc_inst_comm;
        private Boolean _hpc_is_alw;
        private string _hpc_itm;
        private string _hpc_main_cat;
        private string _hpc_pb;
        private string _hpc_pb_lvl;
        private string _hpc_pro;
        private string _hpc_pty_cd;
        private string _hpc_pty_tp;
        private string _hpc_sch_cd;
        private Int32 _hpc_seq;
        private string _hpc_ser;
        private DateTime _hpc_to_dt;
        private string _hpc_mod_by;
        private DateTime _hpc_mod_dt;
        private string _hpc_stus;
        private string _hpc_price_cir_no;

        private Boolean hpc_is_rt;
        private decimal hpc_fpay;
        private int hpc_fpay_withvat;

       

        #endregion

        public string Hpc_price_cir_no
        {
            get { return _hpc_price_cir_no; }
            set { _hpc_price_cir_no = value; }
        }
        public string Hpc_stus
        {
            get { return _hpc_stus; }
            set { _hpc_stus = value; }
        }

        public string Hpc_mod_by
        {
            get { return _hpc_mod_by; }
            set { _hpc_mod_by = value; }
        }

        public DateTime Hpc_mod_dt
        {
            get { return _hpc_mod_dt; }
            set { _hpc_mod_dt = value; }
        }

        public string Hpc_brd
        {
            get { return _hpc_brd; }
            set { _hpc_brd = value; }
        }
        public string Hpc_cat
        {
            get { return _hpc_cat; }
            set { _hpc_cat = value; }
        }
        public string Hpc_cir_no
        {
            get { return _hpc_cir_no; }
            set { _hpc_cir_no = value; }
        }
        public string Hpc_comm_cat
        {
            get { return _hpc_comm_cat; }
            set { _hpc_comm_cat = value; }
        }
        public string Hpc_cre_by
        {
            get { return _hpc_cre_by; }
            set { _hpc_cre_by = value; }
        }
        public DateTime Hpc_cre_dt
        {
            get { return _hpc_cre_dt; }
            set { _hpc_cre_dt = value; }
        }
        public string Hpc_cust_cd
        {
            get { return _hpc_cust_cd; }
            set { _hpc_cust_cd = value; }
        }
        public decimal Hpc_disc
        {
            get { return _hpc_disc; }
            set { _hpc_disc = value; }
        }
        public Boolean Hpc_disc_isrt
        {
            get { return _hpc_disc_isrt; }
            set { _hpc_disc_isrt = value; }
        }
        public decimal Hpc_dp_comm
        {
            get { return _hpc_dp_comm; }
            set { _hpc_dp_comm = value; }
        }
        public DateTime Hpc_from_dt
        {
            get { return _hpc_from_dt; }
            set { _hpc_from_dt = value; }
        }
        public decimal Hpc_inst_comm
        {
            get { return _hpc_inst_comm; }
            set { _hpc_inst_comm = value; }
        }
        public Boolean Hpc_is_alw
        {
            get { return _hpc_is_alw; }
            set { _hpc_is_alw = value; }
        }
        public string Hpc_itm
        {
            get { return _hpc_itm; }
            set { _hpc_itm = value; }
        }
        public string Hpc_main_cat
        {
            get { return _hpc_main_cat; }
            set { _hpc_main_cat = value; }
        }
        public string Hpc_pb
        {
            get { return _hpc_pb; }
            set { _hpc_pb = value; }
        }
        public string Hpc_pb_lvl
        {
            get { return _hpc_pb_lvl; }
            set { _hpc_pb_lvl = value; }
        }
        public string Hpc_pro
        {
            get { return _hpc_pro; }
            set { _hpc_pro = value; }
        }
        public string Hpc_pty_cd
        {
            get { return _hpc_pty_cd; }
            set { _hpc_pty_cd = value; }
        }
        public string Hpc_pty_tp
        {
            get { return _hpc_pty_tp; }
            set { _hpc_pty_tp = value; }
        }
        public string Hpc_sch_cd
        {
            get { return _hpc_sch_cd; }
            set { _hpc_sch_cd = value; }
        }
        public Int32 Hpc_seq
        {
            get { return _hpc_seq; }
            set { _hpc_seq = value; }
        }
        public string Hpc_ser
        {
            get { return _hpc_ser; }
            set { _hpc_ser = value; }
        }
        public DateTime Hpc_to_dt
        {
            get { return _hpc_to_dt; }
            set { _hpc_to_dt = value; }
        }

        // tAHRINDU 
        public Boolean Hpc_is_rt
        {
            get { return hpc_is_rt; }
            set { hpc_is_rt = value; }
        }

        public decimal Hpc_fpay
        {
            get { return hpc_fpay; }
            set { hpc_fpay = value; }
        }

        public int Hpc_fpay_withvat
        {
            get { return hpc_fpay_withvat; }
            set { hpc_fpay_withvat = value; }
        }

        public static HpSchemeDefinition Converter(DataRow row)
        {
            return new HpSchemeDefinition
            {
                Hpc_brd = row["HPC_BRD"] == DBNull.Value ? string.Empty : row["HPC_BRD"].ToString(),
                Hpc_cat = row["HPC_CAT"] == DBNull.Value ? string.Empty : row["HPC_CAT"].ToString(),
                Hpc_cir_no = row["HPC_CIR_NO"] == DBNull.Value ? string.Empty : row["HPC_CIR_NO"].ToString(),
                Hpc_comm_cat = row["HPC_COMM_CAT"] == DBNull.Value ? string.Empty : row["HPC_COMM_CAT"].ToString(),
                Hpc_cre_by = row["HPC_CRE_BY"] == DBNull.Value ? string.Empty : row["HPC_CRE_BY"].ToString(),
                Hpc_cre_dt = row["HPC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPC_CRE_DT"]),
                Hpc_cust_cd = row["HPC_CUST_CD"] == DBNull.Value ? string.Empty : row["HPC_CUST_CD"].ToString(),
                Hpc_disc = row["HPC_DISC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPC_DISC"]),
                Hpc_disc_isrt = row["HPC_DISC_ISRT"] == DBNull.Value ? false : Convert.ToBoolean(row["HPC_DISC_ISRT"]),
                Hpc_dp_comm = row["HPC_DP_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPC_DP_COMM"]),
                Hpc_from_dt = row["HPC_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPC_FROM_DT"]),
                Hpc_inst_comm = row["HPC_INST_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPC_INST_COMM"]),
                Hpc_is_alw = row["HPC_IS_ALW"] == DBNull.Value ? false : Convert.ToBoolean(row["HPC_IS_ALW"]),
                Hpc_itm = row["HPC_ITM"] == DBNull.Value ? string.Empty : row["HPC_ITM"].ToString(),
                Hpc_main_cat = row["HPC_MAIN_CAT"] == DBNull.Value ? string.Empty : row["HPC_MAIN_CAT"].ToString(),
                Hpc_pb = row["HPC_PB"] == DBNull.Value ? string.Empty : row["HPC_PB"].ToString(),
                Hpc_pb_lvl = row["HPC_PB_LVL"] == DBNull.Value ? string.Empty : row["HPC_PB_LVL"].ToString(),
                Hpc_pro = row["HPC_PRO"] == DBNull.Value ? string.Empty : row["HPC_PRO"].ToString(),
                Hpc_pty_cd = row["HPC_PTY_CD"] == DBNull.Value ? string.Empty : row["HPC_PTY_CD"].ToString(),
                Hpc_pty_tp = row["HPC_PTY_TP"] == DBNull.Value ? string.Empty : row["HPC_PTY_TP"].ToString(),
                Hpc_sch_cd = row["HPC_SCH_CD"] == DBNull.Value ? string.Empty : row["HPC_SCH_CD"].ToString(),
                Hpc_seq = row["HPC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPC_SEQ"]),
                Hpc_ser = row["HPC_SER"] == DBNull.Value ? string.Empty : row["HPC_SER"].ToString(),
                Hpc_to_dt = row["HPC_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPC_TO_DT"]),
                Hpc_mod_dt = row["HPC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPC_MOD_DT"]),
                Hpc_mod_by = row["HPC_MOD_BY"] == DBNull.Value ? string.Empty : row["HPC_MOD_BY"].ToString(),
                Hpc_stus = row["HPC_STUS"] == DBNull.Value ? string.Empty : row["HPC_STUS"].ToString(),
                Hpc_price_cir_no = row["HPC_PRICE_CIR_NO"] == DBNull.Value ? string.Empty : row["HPC_PRICE_CIR_NO"].ToString(),

                Hpc_fpay = row["Hpc_fpay"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Hpc_fpay"]), // tHARINDU
                Hpc_is_rt = row["Hpc_is_rt"] == DBNull.Value ? false : Convert.ToBoolean(row["Hpc_is_rt"]), // tHARINDU
                Hpc_fpay_withvat = row["Hpc_fpay_withvat"] == DBNull.Value ? 0 : Convert.ToInt32(row["Hpc_fpay_withvat"])
            };
        }

        public static HpSchemeDefinition ConverterSpecial(DataRow row)
        {
            return new HpSchemeDefinition
            {
                Hpc_cir_no = row["HPC_CIR_NO"] == DBNull.Value ? string.Empty : row["HPC_CIR_NO"].ToString(),
                Hpc_disc = row["HPC_DISC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPC_DISC"]),
                Hpc_dp_comm = row["HPC_DP_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPC_DP_COMM"]),
                Hpc_from_dt = row["HPC_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPC_FROM_DT"]),
                Hpc_inst_comm = row["HPC_INST_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPC_INST_COMM"]),
                Hpc_is_alw = row["HPC_IS_ALW"] == DBNull.Value ? false : Convert.ToBoolean(row["HPC_IS_ALW"]),
                Hpc_to_dt = row["HPC_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPC_TO_DT"])
                
            };
        }

    }
}
