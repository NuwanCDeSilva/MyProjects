using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class HpSchemeDefinitionProcess
    {
        #region Private Members
        private string _hpc_brd;
        private string _hpc_cat;
        private string _hpc_cir_no;
        private string _hpc_comm_cat;
        private string _hpc_cre_by;
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
        private string _hpc_ser;
        private DateTime _hpc_to_dt;
        private string _hpc_price_cir_no;

        private Boolean _hpc_is_rt;
        private decimal _hpc_fpay;
        private int _hsd_add_calwithvat;
        private string _Hpc_is_rt_typr;
        private Boolean _hpc_with_vat;
        #endregion

        public string Hpc_price_cir_no
        {
            get { return _hpc_price_cir_no; }
            set { _hpc_price_cir_no = value; }
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

        // Tharindu
        public Boolean Hpc_is_rt
        {
            get { return _hpc_is_rt; }
            set { _hpc_is_rt = value; }
        }

        public decimal Hpc_fpay
        {
            get { return _hpc_fpay; }
            set { _hpc_fpay = value; }
        }

        public int Hsd_add_calwithvat
        {
            get { return _hsd_add_calwithvat; }
            set { _hsd_add_calwithvat = value; }
        }
        public string Hpc_is_rt_typr
        {
            get { return _Hpc_is_rt_typr; }
            set { _Hpc_is_rt_typr = value; }
        }
        public Boolean Hpc_with_vat
        {
            get { return _hpc_with_vat; }
            set { _hpc_with_vat = value; }
        }
   

        public static HpSchemeDefinitionProcess Converter(DataRow row)
        {
            return new HpSchemeDefinitionProcess
            {
                Hpc_brd = row["HPC_BRD"] == DBNull.Value ? string.Empty : row["HPC_BRD"].ToString(),
                Hpc_cat = row["HPC_CAT"] == DBNull.Value ? string.Empty : row["HPC_CAT"].ToString(),
                Hpc_cir_no = row["HPC_CIR_NO"] == DBNull.Value ? string.Empty : row["HPC_CIR_NO"].ToString(),
                Hpc_comm_cat = row["HPC_COMM_CAT"] == DBNull.Value ? string.Empty : row["HPC_COMM_CAT"].ToString(),
                Hpc_cre_by = row["HPC_CRE_BY"] == DBNull.Value ? string.Empty : row["HPC_CRE_BY"].ToString(),
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
                Hpc_ser = row["HPC_SER"] == DBNull.Value ? string.Empty : row["HPC_SER"].ToString(),
                Hpc_to_dt = row["HPC_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPC_TO_DT"]),
                Hpc_price_cir_no = row["HPC_PRICE_CIR_NO"] == DBNull.Value ? string.Empty : row["HPC_PRICE_CIR_NO"].ToString(),

                Hpc_is_rt = row["Hpc_is_rt"] == DBNull.Value ? false : Convert.ToBoolean(row["Hpc_is_rt"]), // tHARINDU
                Hpc_fpay = row["Hpc_fpay"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Hpc_fpay"]),
                Hsd_add_calwithvat = row["Hsd_add_calwithvat"] == DBNull.Value ? 0 : Convert.ToInt32(row["Hsd_add_calwithvat"])
                
             
            };
        }

    }
}
