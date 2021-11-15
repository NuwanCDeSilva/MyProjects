using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class CashGeneralDicountDef
    {
        //Written By Prabhath on 04/08/2012
        //Table : SAR_GEN_DISC_DEFN

        #region Private Members
        private Boolean _sgdd_alw_pro;
        private Boolean _sgdd_alw_ser;
        private string _sgdd_circular;
        private string _sgdd_com;
        private string _sgdd_cre_by;
        private DateTime _sgdd_cre_dt;
        private string _sgdd_cust_cd;
        private decimal _sgdd_disc_rt;
        private decimal _sgdd_disc_val;
        private DateTime _sgdd_from_dt;
        private string _sgdd_itm;
        private string _sgdd_mod_by;
        private DateTime _sgdd_mod_dt;
        private Int32 _sgdd_no_of_times;
        private Int32 _sgdd_no_of_used_times;
        private string _sgdd_pb;
        private string _sgdd_pb_lvl;
        private string _sgdd_pc;
        private string _sgdd_req_ref;
        private string _sgdd_sale_tp;
        private Int32 _sgdd_seq;
        private Boolean _sgdd_stus;
        private DateTime _sgdd_to_dt;

        //added by shani 07/08/2012
        //these fields are not in the table
        private string _brand;
        private string _cate1;
        private string _cate2;
        private string _cate3;

        #endregion

        public Boolean Sgdd_alw_pro { get { return _sgdd_alw_pro; } set { _sgdd_alw_pro = value; } }
        public Boolean Sgdd_alw_ser { get { return _sgdd_alw_ser; } set { _sgdd_alw_ser = value; } }
        public string Sgdd_circular { get { return _sgdd_circular; } set { _sgdd_circular = value; } }
        public string Sgdd_com { get { return _sgdd_com; } set { _sgdd_com = value; } }
        public string Sgdd_cre_by { get { return _sgdd_cre_by; } set { _sgdd_cre_by = value; } }
        public DateTime Sgdd_cre_dt { get { return _sgdd_cre_dt; } set { _sgdd_cre_dt = value; } }
        public string Sgdd_cust_cd { get { return _sgdd_cust_cd; } set { _sgdd_cust_cd = value; } }
        public decimal Sgdd_disc_rt { get { return _sgdd_disc_rt; } set { _sgdd_disc_rt = value; } }
        public decimal Sgdd_disc_val { get { return _sgdd_disc_val; } set { _sgdd_disc_val = value; } }
        public DateTime Sgdd_from_dt { get { return _sgdd_from_dt; } set { _sgdd_from_dt = value; } }
        public string Sgdd_itm { get { return _sgdd_itm; } set { _sgdd_itm = value; } }
        public string Sgdd_mod_by { get { return _sgdd_mod_by; } set { _sgdd_mod_by = value; } }
        public DateTime Sgdd_mod_dt { get { return _sgdd_mod_dt; } set { _sgdd_mod_dt = value; } }
        public Int32 Sgdd_no_of_times { get { return _sgdd_no_of_times; } set { _sgdd_no_of_times = value; } }
        public Int32 Sgdd_no_of_used_times { get { return _sgdd_no_of_used_times; } set { _sgdd_no_of_used_times = value; } }
        public string Sgdd_pb { get { return _sgdd_pb; } set { _sgdd_pb = value; } }
        public string Sgdd_pb_lvl { get { return _sgdd_pb_lvl; } set { _sgdd_pb_lvl = value; } }
        public string Sgdd_pc { get { return _sgdd_pc; } set { _sgdd_pc = value; } }
        public string Sgdd_req_ref { get { return _sgdd_req_ref; } set { _sgdd_req_ref = value; } }
        public string Sgdd_sale_tp { get { return _sgdd_sale_tp; } set { _sgdd_sale_tp = value; } }
        public Int32 Sgdd_seq { get { return _sgdd_seq; } set { _sgdd_seq = value; } }
        public Boolean Sgdd_stus { get { return _sgdd_stus; } set { _sgdd_stus = value; } }
        public DateTime Sgdd_to_dt { get { return _sgdd_to_dt; } set { _sgdd_to_dt = value; } }

        //added by shani 07/08/2012 

        public string Sgdd_brand { get { return _brand; } set { _brand = value; } }
        public string Sgdd_cate1 { get { return _cate1; } set { _cate1 = value; } }
        public string Sgdd_cate2 { get { return _cate2; } set { _cate2 = value; } }
        public string Sgdd_cate3 { get { return _cate3; } set { _cate3 = value; } }

        public static CashGeneralDicountDef Converter(DataRow row)
        {
            return new CashGeneralDicountDef
            {
                Sgdd_alw_pro = row["SGDD_ALW_PRO"] == DBNull.Value ? false : Convert.ToBoolean(row["SGDD_ALW_PRO"]),
                Sgdd_alw_ser = row["SGDD_ALW_SER"] == DBNull.Value ? false : Convert.ToBoolean(row["SGDD_ALW_SER"]),
                Sgdd_circular = row["SGDD_CIRCULAR"] == DBNull.Value ? string.Empty : row["SGDD_CIRCULAR"].ToString(),
                Sgdd_com = row["SGDD_COM"] == DBNull.Value ? string.Empty : row["SGDD_COM"].ToString(),
                Sgdd_cre_by = row["SGDD_CRE_BY"] == DBNull.Value ? string.Empty : row["SGDD_CRE_BY"].ToString(),
                Sgdd_cre_dt = row["SGDD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SGDD_CRE_DT"]),
                Sgdd_cust_cd = row["SGDD_CUST_CD"] == DBNull.Value ? string.Empty : row["SGDD_CUST_CD"].ToString(),
                Sgdd_disc_rt = row["SGDD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SGDD_DISC_RT"]),
                Sgdd_disc_val = row["SGDD_DISC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SGDD_DISC_VAL"]),
                Sgdd_from_dt = row["SGDD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SGDD_FROM_DT"]),
                Sgdd_itm = row["SGDD_ITM"] == DBNull.Value ? string.Empty : row["SGDD_ITM"].ToString(),
                Sgdd_mod_by = row["SGDD_MOD_BY"] == DBNull.Value ? string.Empty : row["SGDD_MOD_BY"].ToString(),
                Sgdd_mod_dt = row["SGDD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SGDD_MOD_DT"]),
                Sgdd_no_of_times = row["SGDD_NO_OF_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SGDD_NO_OF_TIMES"]),
                Sgdd_no_of_used_times = row["SGDD_NO_OF_USED_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SGDD_NO_OF_USED_TIMES"]),
                Sgdd_pb = row["SGDD_PB"] == DBNull.Value ? string.Empty : row["SGDD_PB"].ToString(),
                Sgdd_pb_lvl = row["SGDD_PB_LVL"] == DBNull.Value ? string.Empty : row["SGDD_PB_LVL"].ToString(),
                Sgdd_pc = row["SGDD_PC"] == DBNull.Value ? string.Empty : row["SGDD_PC"].ToString(),
                Sgdd_req_ref = row["SGDD_REQ_REF"] == DBNull.Value ? string.Empty : row["SGDD_REQ_REF"].ToString(),
                Sgdd_sale_tp = row["SGDD_SALE_TP"] == DBNull.Value ? string.Empty : row["SGDD_SALE_TP"].ToString(),
                Sgdd_seq = row["SGDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SGDD_SEQ"]),
                Sgdd_stus = row["SGDD_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SGDD_STUS"]),
                Sgdd_to_dt = row["SGDD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SGDD_TO_DT"])

            };
        }
    }
}


