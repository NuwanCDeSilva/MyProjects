using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
   public class QuotationHeader
   {  /// <summary>
       /// Written By Shani on 18/05/2012
       /// table = QUO_HDR
       /// </summary>

        #region Private Members
        private string _qh_add1;
        private string _qh_add2;
        private string _qh_anal_1;
        private string _qh_anal_2;
        private string _qh_anal_3;
        private string _qh_anal_4;
        private decimal _qh_anal_5;
        private decimal _qh_anal_6;
        private decimal _qh_anal_7;
        private DateTime _qh_anal_8;
        private string _qh_com;
        private string _qh_cre_by;
        private DateTime _qh_cre_when;
        private string _qh_cur_cd;
        private DateTime _qh_dt;
        private string _qh_email;
        private DateTime _qh_ex_dt;
        private decimal _qh_ex_rt;
        private DateTime _qh_frm_dt;
        private Boolean _qh_is_tax;
        private string _qh_jobno;
        private string _qh_mobi;
        private string _qh_mod_by;
        private DateTime _qh_mod_when;
        private string _qh_no;
        private string _qh_party_cd;
        private string _qh_party_name;
        private string _qh_pc;
        private string _qh_ref;
        private string _qh_remarks;
        private string _qh_sales_ex;
        private Int32 _qh_seq_no;
        private string _qh_session_id;
        private string _qh_stus;
        private string _qh_sub_tp;
        private string _qh_tel;
        private string _qh_tp;
        private string _qh_del_cuscd;
        private string _qh_del_cusname;
        private string _qh_del_cusadd1;
        private string _qh_del_cusadd2;
        private string _qh_del_custel;
        private string _qh_del_cusfax;
        private string _qh_del_cusid;
        private string _qh_del_cusvatreg;
        private string _qh_add_wararmk;
        private Int32 _Qh_quo_base; //kapila 11/3/2016
         /*Lakshan temp add 14 Mar 2016*/
        private string _qd_itm_cd;
        private string _qd_itm_des;
        private decimal _qd_frm_qty;
        private decimal _qd_to_qty;
        private decimal _qd_seq_log;
         
        #endregion

        public string Qh_add1 { get { return _qh_add1; } set { _qh_add1 = value; } }
        public string Qh_add2 { get { return _qh_add2; } set { _qh_add2 = value; } }
        public string Qh_anal_1 { get { return _qh_anal_1; } set { _qh_anal_1 = value; } }
        public string Qh_anal_2 { get { return _qh_anal_2; } set { _qh_anal_2 = value; } }
        public string Qh_anal_3 { get { return _qh_anal_3; } set { _qh_anal_3 = value; } }
        public string Qh_anal_4 { get { return _qh_anal_4; } set { _qh_anal_4 = value; } }
        public decimal Qh_anal_5 { get { return _qh_anal_5; } set { _qh_anal_5 = value; } }
        public decimal Qh_anal_6 { get { return _qh_anal_6; } set { _qh_anal_6 = value; } }
        public decimal Qh_anal_7 { get { return _qh_anal_7; } set { _qh_anal_7 = value; } }
        public DateTime Qh_anal_8 { get { return _qh_anal_8; } set { _qh_anal_8 = value; } }
        public string Qh_com { get { return _qh_com; } set { _qh_com = value; } }
        public string Qh_cre_by { get { return _qh_cre_by; } set { _qh_cre_by = value; } }
        public DateTime Qh_cre_when { get { return _qh_cre_when; } set { _qh_cre_when = value; } }
        public string Qh_cur_cd { get { return _qh_cur_cd; } set { _qh_cur_cd = value; } }
        public DateTime Qh_dt { get { return _qh_dt; } set { _qh_dt = value; } }
        public string Qh_email { get { return _qh_email; } set { _qh_email = value; } }
        public DateTime Qh_ex_dt { get { return _qh_ex_dt; } set { _qh_ex_dt = value; } }
        public decimal Qh_ex_rt { get { return _qh_ex_rt; } set { _qh_ex_rt = value; } }
        public DateTime Qh_frm_dt { get { return _qh_frm_dt; } set { _qh_frm_dt = value; } }
        public Boolean Qh_is_tax { get { return _qh_is_tax; } set { _qh_is_tax = value; } }
        public string Qh_jobno { get { return _qh_jobno; } set { _qh_jobno = value; } }
        public string Qh_mobi { get { return _qh_mobi; } set { _qh_mobi = value; } }
        public string Qh_mod_by { get { return _qh_mod_by; } set { _qh_mod_by = value; } }
        public DateTime Qh_mod_when { get { return _qh_mod_when; } set { _qh_mod_when = value; } }
        public string Qh_no { get { return _qh_no; } set { _qh_no = value; } }
        public string Qh_party_cd { get { return _qh_party_cd; } set { _qh_party_cd = value; } }
        public string Qh_party_name { get { return _qh_party_name; } set { _qh_party_name = value; } }
        public string Qh_pc { get { return _qh_pc; } set { _qh_pc = value; } }
        public string Qh_ref { get { return _qh_ref; } set { _qh_ref = value; } }
        public string Qh_remarks { get { return _qh_remarks; } set { _qh_remarks = value; } }
        public string Qh_sales_ex { get { return _qh_sales_ex; } set { _qh_sales_ex = value; } }
        public Int32 Qh_seq_no { get { return _qh_seq_no; } set { _qh_seq_no = value; } }
        public string Qh_session_id { get { return _qh_session_id; } set { _qh_session_id = value; } }
        public string Qh_stus { get { return _qh_stus; } set { _qh_stus = value; } }
        public string Qh_sub_tp { get { return _qh_sub_tp; } set { _qh_sub_tp = value; } }
        public string Qh_tel { get { return _qh_tel; } set { _qh_tel = value; } }
        public string Qh_tp { get { return _qh_tp; } set { _qh_tp = value; } }
        public string Qh_del_cuscd { get { return _qh_del_cuscd; } set { _qh_del_cuscd = value; } }
        public string Qh_del_cusname { get { return _qh_del_cusname; } set { _qh_del_cusname = value; } }
        public string Qh_del_cusadd1 { get { return _qh_del_cusadd1; } set { _qh_del_cusadd1 = value; } }
        public string Qh_del_cusadd2 { get { return _qh_del_cusadd2; } set { _qh_del_cusadd2 = value; } }
        public string Qh_del_custel { get { return _qh_del_custel; } set { _qh_del_custel = value; } }
        public string Qh_del_cusfax { get { return _qh_del_cusfax; } set { _qh_del_cusfax = value; } }
        public string Qh_del_cusid { get { return _qh_del_cusid; } set { _qh_del_cusid = value; } }
        public string Qh_del_cusvatreg { get { return _qh_del_cusvatreg; } set { _qh_del_cusvatreg = value; } }
        public string Qh_add_wararmk { get { return _qh_add_wararmk; } set { _qh_add_wararmk = value; } }
        public Int32 Qh_quo_base { get { return _Qh_quo_base; } set { _Qh_quo_base = value; } }   //kapila 11/3/2016
         //Lakshan 2016 Mar 14
        public string Qd_itm_cd { get { return _qd_itm_cd; } set { _qd_itm_cd = value; } }
        public string Qd_itm_des { get { return _qd_itm_des; } set { _qd_itm_des = value; } }
        public decimal Qd_frm_qty { get { return _qd_frm_qty; } set { _qd_frm_qty = value; } }
        public decimal Qd_to_qty { get { return _qd_to_qty; } set { _qd_to_qty = value; } }
        public string Tmp_req_no{ get; set; }
        public static QuotationHeader ConvertTotal(DataRow row)
        {
            return new QuotationHeader
            {
                Qh_add1 = row["QH_ADD1"] == DBNull.Value ? string.Empty : row["QH_ADD1"].ToString(),
                Qh_add2 = row["QH_ADD2"] == DBNull.Value ? string.Empty : row["QH_ADD2"].ToString(),
                Qh_anal_1 = row["QH_ANAL_1"] == DBNull.Value ? string.Empty : row["QH_ANAL_1"].ToString(),
                Qh_anal_2 = row["QH_ANAL_2"] == DBNull.Value ? string.Empty : row["QH_ANAL_2"].ToString(),
                Qh_anal_3 = row["QH_ANAL_3"] == DBNull.Value ? string.Empty : row["QH_ANAL_3"].ToString(),
                Qh_anal_4 = row["QH_ANAL_4"] == DBNull.Value ? string.Empty : row["QH_ANAL_4"].ToString(),
                Qh_anal_5 = row["QH_ANAL_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QH_ANAL_5"]),
                Qh_anal_6 = row["QH_ANAL_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QH_ANAL_6"]),
                Qh_anal_7 = row["QH_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QH_ANAL_7"]),
                Qh_anal_8 = row["QH_ANAL_8"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_ANAL_8"]),
                Qh_com = row["QH_COM"] == DBNull.Value ? string.Empty : row["QH_COM"].ToString(),
                Qh_cre_by = row["QH_CRE_BY"] == DBNull.Value ? string.Empty : row["QH_CRE_BY"].ToString(),
                Qh_cre_when = row["QH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_CRE_WHEN"]),
                Qh_cur_cd = row["QH_CUR_CD"] == DBNull.Value ? string.Empty : row["QH_CUR_CD"].ToString(),
                Qh_dt = row["QH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_DT"]),
                Qh_email = row["QH_EMAIL"] == DBNull.Value ? string.Empty : row["QH_EMAIL"].ToString(),
                Qh_ex_dt = row["QH_EX_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_EX_DT"]),
                Qh_ex_rt = row["QH_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QH_EX_RT"]),
                Qh_frm_dt = row["QH_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_FRM_DT"]),
                Qh_is_tax = row["QH_IS_TAX"] == DBNull.Value ? false : Convert.ToBoolean(row["QH_IS_TAX"]),
                Qh_jobno = row["QH_JOBNO"] == DBNull.Value ? string.Empty : row["QH_JOBNO"].ToString(),
                Qh_mobi = row["QH_MOBI"] == DBNull.Value ? string.Empty : row["QH_MOBI"].ToString(),
                Qh_mod_by = row["QH_MOD_BY"] == DBNull.Value ? string.Empty : row["QH_MOD_BY"].ToString(),
                Qh_mod_when = row["QH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_MOD_WHEN"]),
                Qh_no = row["QH_NO"] == DBNull.Value ? string.Empty : row["QH_NO"].ToString(),
                Qh_party_cd = row["QH_PARTY_CD"] == DBNull.Value ? string.Empty : row["QH_PARTY_CD"].ToString(),
                Qh_party_name = row["QH_PARTY_NAME"] == DBNull.Value ? string.Empty : row["QH_PARTY_NAME"].ToString(),
                Qh_pc = row["QH_PC"] == DBNull.Value ? string.Empty : row["QH_PC"].ToString(),
                Qh_ref = row["QH_REF"] == DBNull.Value ? string.Empty : row["QH_REF"].ToString(),
                Qh_remarks = row["QH_REMARKS"] == DBNull.Value ? string.Empty : row["QH_REMARKS"].ToString(),
                Qh_sales_ex = row["QH_SALES_EX"] == DBNull.Value ? string.Empty : row["QH_SALES_EX"].ToString(),
                Qh_seq_no = row["QH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QH_SEQ_NO"]),
                Qh_session_id = row["QH_SESSION_ID"] == DBNull.Value ? string.Empty : row["QH_SESSION_ID"].ToString(),
                Qh_stus = row["QH_STUS"] == DBNull.Value ? string.Empty : row["QH_STUS"].ToString(),
                Qh_sub_tp = row["QH_SUB_TP"] == DBNull.Value ? string.Empty : row["QH_SUB_TP"].ToString(),
                Qh_tel = row["QH_TEL"] == DBNull.Value ? string.Empty : row["QH_TEL"].ToString(),
                Qh_tp = row["QH_TP"] == DBNull.Value ? string.Empty : row["QH_TP"].ToString(),
                Qh_del_cuscd = row["QH_DEL_CUSCD"] == DBNull.Value ? string.Empty : row["QH_DEL_CUSCD"].ToString(),
                Qh_del_cusname = row["QH_DEL_CUSNAME"] == DBNull.Value ? string.Empty : row["QH_DEL_CUSNAME"].ToString(),
                Qh_del_cusadd1 = row["QH_DEL_CUSADD1"] == DBNull.Value ? string.Empty : row["QH_DEL_CUSADD1"].ToString(),
                Qh_del_cusadd2 = row["QH_DEL_CUSADD2"] == DBNull.Value ? string.Empty : row["QH_DEL_CUSADD2"].ToString(),
                Qh_del_custel = row["QH_DEL_CUSTEL"] == DBNull.Value ? string.Empty : row["QH_DEL_CUSTEL"].ToString(),
                Qh_del_cusfax = row["QH_DEL_CUSFAX"] == DBNull.Value ? string.Empty : row["QH_DEL_CUSFAX"].ToString(),
                Qh_del_cusid = row["QH_DEL_CUSID"] == DBNull.Value ? string.Empty : row["QH_DEL_CUSID"].ToString(),
                Qh_del_cusvatreg = row["QH_DEL_CUSVATREG"] == DBNull.Value ? string.Empty : row["QH_DEL_CUSVATREG"].ToString(),
                Qh_add_wararmk = row["QH_ADD_WARARMK"] == DBNull.Value ? string.Empty : row["QH_ADD_WARARMK"].ToString()
            };
        }


        public static QuotationHeader ConvertQuotation(DataRow row)
        {
            return new QuotationHeader
            {
                Qh_anal_5 = row["QH_ANAL_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QH_ANAL_5"]),
                Qh_ex_dt = row["QH_EX_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_EX_DT"]),
                Qh_frm_dt = row["QH_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_FRM_DT"]),
                Qh_no = row["QH_NO"] == DBNull.Value ? string.Empty : row["QH_NO"].ToString(),
                Qh_ref = row["QH_REF"] == DBNull.Value ? string.Empty : row["QH_REF"].ToString()
                
            };
        }
        public static QuotationHeader ConvertQuotation2(DataRow row)
        {
            return new QuotationHeader
            {
                Qh_anal_5 = row["QH_ANAL_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QH_ANAL_5"]),
                Qh_ex_dt = row["QH_EX_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_EX_DT"]),
                Qh_frm_dt = row["QH_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_FRM_DT"]),
                Qh_no = row["QH_NO"] == DBNull.Value ? string.Empty : row["QH_NO"].ToString(),
                Qh_ref = row["QH_REF"] == DBNull.Value ? string.Empty : row["QH_REF"].ToString(),
                Qh_party_cd = row["qh_party_cd"] == DBNull.Value ? string.Empty : row["qh_party_cd"].ToString(),
                Qh_party_name = row["QH_PARTY_NAME"] == DBNull.Value ? string.Empty : row["QH_PARTY_NAME"].ToString()
            };
        }
        public static QuotationHeader ConvertQuotData(DataRow row)
        {
            return new QuotationHeader
            {
                Qh_party_cd = row["Qh_party_cd"] == DBNull.Value ? string.Empty : row["Qh_party_cd"].ToString(),
                Qh_party_name = row["qh_party_name"] == DBNull.Value ? string.Empty : row["qh_party_name"].ToString(),
                Qd_itm_cd = row["qd_itm_cd"] == DBNull.Value ? string.Empty : row["qd_itm_cd"].ToString(),
                Qd_itm_des = row["qd_itm_desc"] == DBNull.Value ? string.Empty : row["qd_itm_desc"].ToString(),
                Qh_anal_5 = row["qd_unit_price"] == DBNull.Value ? 0 : Convert.ToDecimal(row["qd_unit_price"]),
                Qd_frm_qty = row["qd_frm_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["qd_frm_qty"]),
                Qd_to_qty = row["qd_to_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["qd_to_qty"]),
                Qh_ex_dt = row["QH_EX_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_EX_DT"]),
                Qh_frm_dt = row["QH_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QH_FRM_DT"]),
                Qh_no = row["QH_NO"] == DBNull.Value ? string.Empty : row["QH_NO"].ToString(),
                Qh_ref = row["QH_REF"] == DBNull.Value ? string.Empty : row["QH_REF"].ToString()
            };
        }
    }
}
