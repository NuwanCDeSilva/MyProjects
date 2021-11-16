using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Security
{
    [Serializable]
    public class MasterProfitCenter
    {
        //Written By Prabhath on  02/05/2012
        //Profit Center Master 
        #region Private Members
        private Boolean _mpc_act;
        private string _mpc_add_1;
        private string _mpc_add_2;
        private int _mpc_add_hours;
        private string _mpc_cd;
        private Boolean _mpc_check_cm;
        private Boolean _mpc_check_pay;
        private Boolean _mpc_chk_credit;
        private string _mpc_chnl;
        private string _mpc_com;
        private string _mpc_def_customer;
        private string _mpc_def_dept;
        private decimal _mpc_def_dis_rate;
        private string _mpc_def_exrate;
        private string _mpc_def_loc;
        private string _mpc_def_pb;
        private string _mpc_desc;
        private Boolean _mpc_edit_price;
        private decimal _mpc_edit_rate;
        private string _mpc_email;
        private string _mpc_fax;
        private DateTime _mpc_fwd_sale_st;
        private Boolean _mpc_hp_sys_rec;
        private Boolean _mpc_inter_com;
        private Boolean _mpc_is_chk_man_doc;
        private Int32 _mpc_is_do_now;
        private string _mpc_man;
        private Int32 _mpc_max_fwdsale;
        private Boolean _mpc_multi_dept;
        private string _mpc_ope_cd;
        private Boolean _mpc_order_restric;
        private int _mpc_order_valid_pd;
        private string _mpc_oth_ref;
        private Boolean _mpc_print_dis;
        private Boolean _mpc_print_payment;
        private Boolean _mpc_print_wara_remarks;
        private Boolean _mpc_so_sms;
        private string _mpc_tel;
        private string _mpc_tp;
        private int _mpc_wara_extend;
        private Boolean _mpc_without_price;
        private Boolean _mpc_issp_tax;

        private string _MPC_DIST;
        private string _MPC_PROV;
        private string _MPC_MAN_NAME;
        private string _MPC_GRADE;
        private DateTime _MPC_OPN_DT;
        private DateTime _MPC_JOINED_DT;
        private DateTime _MPC_HOVR_DT;
        private Int32 _MPC_SQ_FT;
        private Int32 _MPC_NO_OF_STAFF;

        private Int32 _MPC_NUM_FWDSALE;

        private bool _MPC_CHK_AUTO_APP;
        private bool _MPC_IS_ALERT;
        private Int32 _mpc_cus_cre_man; //kapila 6/3/2017
        #endregion

        public Int32 Mpc_cus_cre_man
        {
            get { return _mpc_cus_cre_man; }
            set { _mpc_cus_cre_man = value; }
        }
        public Boolean Mpc_issp_tax
        {
            get { return _mpc_issp_tax; }
            set { _mpc_issp_tax = value; }
        }

        public Int32 MPC_NUM_FWDSALE
        {
            get { return _MPC_NUM_FWDSALE; }
            set { _MPC_NUM_FWDSALE = value; }
        }
        public Int32 MPC_SQ_FT
        {
            get { return _MPC_SQ_FT; }
            set { _MPC_SQ_FT = value; }
        }
        public Int32 MPC_NO_OF_STAFF
        {
            get { return _MPC_NO_OF_STAFF; }
            set { _MPC_NO_OF_STAFF = value; }
        }
        public DateTime MPC_HOVR_DT
        {
            get { return _MPC_HOVR_DT; }
            set { _MPC_HOVR_DT = value; }
        }
        public DateTime MPC_OPN_DT
        {
            get { return _MPC_OPN_DT; }
            set { _MPC_OPN_DT = value; }
        }
        public DateTime MPC_JOINED_DT
        {
            get { return _MPC_JOINED_DT; }
            set { _MPC_JOINED_DT = value; }
        }

        public string MPC_DIST
        {
            get { return _MPC_DIST; }
            set { _MPC_DIST = value; }
        }
        public string MPC_PROV
        {
            get { return _MPC_PROV; }
            set { _MPC_PROV = value; }
        }
        public string MPC_MAN_NAME
        {
            get { return _MPC_MAN_NAME; }
            set { _MPC_MAN_NAME = value; }
        }
        public string MPC_GRADE
        {
            get { return _MPC_GRADE; }
            set { _MPC_GRADE = value; }
        }

        public Boolean Mpc_act
        {
            get { return _mpc_act; }
            set { _mpc_act = value; }
        }
        public string Mpc_add_1
        {
            get { return _mpc_add_1; }
            set { _mpc_add_1 = value; }
        }
        public string Mpc_add_2
        {
            get { return _mpc_add_2; }
            set { _mpc_add_2 = value; }
        }
        public int Mpc_add_hours
        {
            get { return _mpc_add_hours; }
            set { _mpc_add_hours = value; }
        }
        public string Mpc_cd
        {
            get { return _mpc_cd; }
            set { _mpc_cd = value; }
        }
        public Boolean Mpc_check_cm
        {
            get { return _mpc_check_cm; }
            set { _mpc_check_cm = value; }
        }
        public Boolean Mpc_check_pay
        {
            get { return _mpc_check_pay; }
            set { _mpc_check_pay = value; }
        }
        public Boolean Mpc_chk_credit
        {
            get { return _mpc_chk_credit; }
            set { _mpc_chk_credit = value; }
        }
        public string Mpc_chnl
        {
            get { return _mpc_chnl; }
            set { _mpc_chnl = value; }
        }
        public string Mpc_com
        {
            get { return _mpc_com; }
            set { _mpc_com = value; }
        }
        public string Mpc_def_customer
        {
            get { return _mpc_def_customer; }
            set { _mpc_def_customer = value; }
        }
        public string Mpc_def_dept
        {
            get { return _mpc_def_dept; }
            set { _mpc_def_dept = value; }
        }
        public decimal Mpc_def_dis_rate
        {
            get { return _mpc_def_dis_rate; }
            set { _mpc_def_dis_rate = value; }
        }
        public string Mpc_def_exrate
        {
            get { return _mpc_def_exrate; }
            set { _mpc_def_exrate = value; }
        }
        public string Mpc_def_loc
        {
            get { return _mpc_def_loc; }
            set { _mpc_def_loc = value; }
        }
        public string Mpc_def_pb
        {
            get { return _mpc_def_pb; }
            set { _mpc_def_pb = value; }
        }
        public string Mpc_desc
        {
            get { return _mpc_desc; }
            set { _mpc_desc = value; }
        }
        public Boolean Mpc_edit_price
        {
            get { return _mpc_edit_price; }
            set { _mpc_edit_price = value; }
        }
        public decimal Mpc_edit_rate
        {
            get { return _mpc_edit_rate; }
            set { _mpc_edit_rate = value; }
        }
        public string Mpc_email
        {
            get { return _mpc_email; }
            set { _mpc_email = value; }
        }
        public string Mpc_fax
        {
            get { return _mpc_fax; }
            set { _mpc_fax = value; }
        }
        public DateTime Mpc_fwd_sale_st
        {
            get { return _mpc_fwd_sale_st; }
            set { _mpc_fwd_sale_st = value; }
        }
        public Boolean Mpc_hp_sys_rec
        {
            get { return _mpc_hp_sys_rec; }
            set { _mpc_hp_sys_rec = value; }
        }
        public Boolean Mpc_inter_com
        {
            get { return _mpc_inter_com; }
            set { _mpc_inter_com = value; }
        }
        public Boolean Mpc_is_chk_man_doc
        {
            get { return _mpc_is_chk_man_doc; }
            set { _mpc_is_chk_man_doc = value; }
        }
        public Int32 Mpc_is_do_now
        {
            get { return _mpc_is_do_now; }
            set { _mpc_is_do_now = value; }
        }
        public string Mpc_man
        {
            get { return _mpc_man; }
            set { _mpc_man = value; }
        }
        public Int32 Mpc_max_fwdsale
        {
            get { return _mpc_max_fwdsale; }
            set { _mpc_max_fwdsale = value; }
        }
        public Boolean Mpc_multi_dept
        {
            get { return _mpc_multi_dept; }
            set { _mpc_multi_dept = value; }
        }
        public string Mpc_ope_cd
        {
            get { return _mpc_ope_cd; }
            set { _mpc_ope_cd = value; }
        }
        public Boolean Mpc_order_restric
        {
            get { return _mpc_order_restric; }
            set { _mpc_order_restric = value; }
        }
        public int Mpc_order_valid_pd
        {
            get { return _mpc_order_valid_pd; }
            set { _mpc_order_valid_pd = value; }
        }
        public string Mpc_oth_ref
        {
            get { return _mpc_oth_ref; }
            set { _mpc_oth_ref = value; }
        }
        public Boolean Mpc_print_dis
        {
            get { return _mpc_print_dis; }
            set { _mpc_print_dis = value; }
        }
        public Boolean Mpc_print_payment
        {
            get { return _mpc_print_payment; }
            set { _mpc_print_payment = value; }
        }
        public Boolean Mpc_print_wara_remarks
        {
            get { return _mpc_print_wara_remarks; }
            set { _mpc_print_wara_remarks = value; }
        }
        public Boolean Mpc_so_sms
        {
            get { return _mpc_so_sms; }
            set { _mpc_so_sms = value; }
        }
        public string Mpc_tel
        {
            get { return _mpc_tel; }
            set { _mpc_tel = value; }
        }
        public string Mpc_tp
        {
            get { return _mpc_tp; }
            set { _mpc_tp = value; }
        }
        public int Mpc_wara_extend
        {
            get { return _mpc_wara_extend; }
            set { _mpc_wara_extend = value; }
        }
        public Boolean Mpc_without_price
        {
            get { return _mpc_without_price; }
            set { _mpc_without_price = value; }
        }
        public bool MPC_CHK_AUTO_APP
        {
            get { return _MPC_CHK_AUTO_APP; }
            set { _MPC_CHK_AUTO_APP = value; }
        }
        public bool MPC_IS_ALERT
        {
            get { return _MPC_IS_ALERT; }
            set { _MPC_IS_ALERT = value; }
        }

        public Int32 Mpc_mul_crnote { get; set; }
        public String Mpc_cate { get; set; }

        public String Mpc_cre_by { get; set; }
        public DateTime Mpc_cre_dt { get; set; }
        public String Mpc_mod_by { get; set; }
        public DateTime Mpc_mod_dt { get; set; }
        public Int32 Mpc_so_res { get; set; }
        public Int32 Mpc_so_rest_stk { get; set; }
        public Int32 Mpc_ord_alpbt { get; set; }

        public static MasterProfitCenter ConvertTotal(DataRow row)
        {
            return new MasterProfitCenter
            {
                Mpc_act = row["MPC_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_ACT"]),
                Mpc_add_1 = row["MPC_ADD_1"] == DBNull.Value ? string.Empty : row["MPC_ADD_1"].ToString(),
                Mpc_add_2 = row["MPC_ADD_2"] == DBNull.Value ? string.Empty : row["MPC_ADD_2"].ToString(),
                Mpc_add_hours = row["MPC_ADD_HOURS"] == DBNull.Value ? 0 : Convert.ToInt16(row["MPC_ADD_HOURS"]),
                Mpc_cd = row["MPC_CD"] == DBNull.Value ? string.Empty : row["MPC_CD"].ToString(),
                Mpc_check_cm = row["MPC_CHECK_CM"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_CHECK_CM"]),
                Mpc_check_pay = row["MPC_CHECK_PAY"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_CHECK_PAY"]),
                Mpc_chk_credit = row["MPC_CHK_CREDIT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_CHK_CREDIT"]),
                Mpc_chnl = row["MPC_CHNL"] == DBNull.Value ? string.Empty : row["MPC_CHNL"].ToString(),
                Mpc_com = row["MPC_COM"] == DBNull.Value ? string.Empty : row["MPC_COM"].ToString(),
                Mpc_def_customer = row["MPC_DEF_CUSTOMER"] == DBNull.Value ? string.Empty : row["MPC_DEF_CUSTOMER"].ToString(),
                Mpc_def_dept = row["MPC_DEF_DEPT"] == DBNull.Value ? string.Empty : row["MPC_DEF_DEPT"].ToString(),
                Mpc_def_dis_rate = row["MPC_DEF_DIS_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPC_DEF_DIS_RATE"]),
                Mpc_def_exrate = row["MPC_DEF_EXRATE"] == DBNull.Value ? string.Empty : row["MPC_DEF_EXRATE"].ToString(),
                Mpc_def_loc = row["MPC_DEF_LOC"] == DBNull.Value ? string.Empty : row["MPC_DEF_LOC"].ToString(),
                Mpc_def_pb = row["MPC_DEF_PB"] == DBNull.Value ? string.Empty : row["MPC_DEF_PB"].ToString(),
                Mpc_desc = row["MPC_DESC"] == DBNull.Value ? string.Empty : row["MPC_DESC"].ToString(),
                Mpc_edit_price = row["MPC_EDIT_PRICE"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_EDIT_PRICE"]),
                Mpc_edit_rate = row["MPC_EDIT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPC_EDIT_RATE"]),
                Mpc_email = row["MPC_EMAIL"] == DBNull.Value ? string.Empty : row["MPC_EMAIL"].ToString(),
                Mpc_fax = row["MPC_FAX"] == DBNull.Value ? string.Empty : row["MPC_FAX"].ToString(),
                Mpc_fwd_sale_st = row["MPC_FWD_SALE_ST"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_FWD_SALE_ST"]),
                Mpc_hp_sys_rec = row["MPC_HP_SYS_REC"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_HP_SYS_REC"]),
                Mpc_inter_com = row["MPC_INTER_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_INTER_COM"]),
                Mpc_is_chk_man_doc = row["MPC_IS_CHK_MAN_DOC"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_IS_CHK_MAN_DOC"]),
                Mpc_is_do_now = row["MPC_IS_DO_NOW"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_IS_DO_NOW"]),
                Mpc_man = row["MPC_MAN"] == DBNull.Value ? string.Empty : row["MPC_MAN"].ToString(),
                Mpc_max_fwdsale = row["MPC_MAX_FWDSALE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_MAX_FWDSALE"]),
                Mpc_multi_dept = row["MPC_MULTI_DEPT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_MULTI_DEPT"]),
                Mpc_ope_cd = row["MPC_OPE_CD"] == DBNull.Value ? string.Empty : row["MPC_OPE_CD"].ToString(),
                Mpc_order_restric = row["MPC_ORDER_RESTRIC"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_ORDER_RESTRIC"]),
                Mpc_order_valid_pd = row["MPC_ORDER_VALID_PD"] == DBNull.Value ? 0 : Convert.ToInt16(row["MPC_ORDER_VALID_PD"]),
                Mpc_oth_ref = row["MPC_OTH_REF"] == DBNull.Value ? string.Empty : row["MPC_OTH_REF"].ToString(),
                Mpc_print_dis = row["MPC_PRINT_DIS"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_PRINT_DIS"]),
                Mpc_print_payment = row["MPC_PRINT_PAYMENT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_PRINT_PAYMENT"]),
                Mpc_print_wara_remarks = row["MPC_PRINT_WARA_REMARKS"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_PRINT_WARA_REMARKS"]),
                Mpc_so_sms = row["MPC_SO_SMS"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_SO_SMS"]),
                Mpc_tel = row["MPC_TEL"] == DBNull.Value ? string.Empty : row["MPC_TEL"].ToString(),
                Mpc_tp = row["MPC_TP"] == DBNull.Value ? string.Empty : row["MPC_TP"].ToString(),
                Mpc_wara_extend = row["MPC_WARA_EXTEND"] == DBNull.Value ? 0 : Convert.ToInt16(row["MPC_WARA_EXTEND"]),
                Mpc_without_price = row["MPC_WITHOUT_PRICE"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_WITHOUT_PRICE"]),


                MPC_DIST = row["MPC_DIST"] == DBNull.Value ? string.Empty : row["MPC_DIST"].ToString(),
                MPC_PROV = row["MPC_PROV"] == DBNull.Value ? string.Empty : row["MPC_PROV"].ToString(),
                MPC_MAN_NAME = row["MPC_MAN_NAME"] == DBNull.Value ? string.Empty : row["MPC_MAN_NAME"].ToString(),
                MPC_GRADE = row["MPC_GRADE"] == DBNull.Value ? string.Empty : row["MPC_GRADE"].ToString(),
                MPC_SQ_FT = row["MPC_SQ_FT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_SQ_FT"]),
                MPC_NO_OF_STAFF = row["MPC_NO_OF_STAFF"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_NO_OF_STAFF"]),
                MPC_OPN_DT = row["MPC_OPN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_OPN_DT"]),
                MPC_JOINED_DT = row["MPC_JOINED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_JOINED_DT"]),
                MPC_HOVR_DT = row["MPC_HOVR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_HOVR_DT"]),
                MPC_NUM_FWDSALE = row["MPC_NUM_FWDSALE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_NUM_FWDSALE"]),
                Mpc_issp_tax = row["MPC_ISSP_TAX"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_ISSP_TAX"]),
                MPC_CHK_AUTO_APP = row["MPC_CHK_AUTO_APP"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_CHK_AUTO_APP"]),
            };

        }

        public static MasterProfitCenter ConverterNew(DataRow row)
        {
            return new MasterProfitCenter
            {
                Mpc_com = row["MPC_COM"] == DBNull.Value ? string.Empty : row["MPC_COM"].ToString(),
                Mpc_cd = row["MPC_CD"] == DBNull.Value ? string.Empty : row["MPC_CD"].ToString(),
                Mpc_desc = row["MPC_DESC"] == DBNull.Value ? string.Empty : row["MPC_DESC"].ToString(),
                Mpc_tp = row["MPC_TP"] == DBNull.Value ? string.Empty : row["MPC_TP"].ToString(),
                Mpc_oth_ref = row["MPC_OTH_REF"] == DBNull.Value ? string.Empty : row["MPC_OTH_REF"].ToString(),
                Mpc_add_1 = row["MPC_ADD_1"] == DBNull.Value ? string.Empty : row["MPC_ADD_1"].ToString(),
                Mpc_add_2 = row["MPC_ADD_2"] == DBNull.Value ? string.Empty : row["MPC_ADD_2"].ToString(),
                Mpc_tel = row["MPC_TEL"] == DBNull.Value ? string.Empty : row["MPC_TEL"].ToString(),
                Mpc_fax = row["MPC_FAX"] == DBNull.Value ? string.Empty : row["MPC_FAX"].ToString(),
                Mpc_act = row["MPC_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_ACT"].ToString()),
                Mpc_chnl = row["MPC_CHNL"] == DBNull.Value ? string.Empty : row["MPC_CHNL"].ToString(),
                Mpc_ope_cd = row["MPC_OPE_CD"] == DBNull.Value ? string.Empty : row["MPC_OPE_CD"].ToString(),
                Mpc_def_pb = row["MPC_DEF_PB"] == DBNull.Value ? string.Empty : row["MPC_DEF_PB"].ToString(),
                Mpc_edit_price = row["MPC_EDIT_PRICE"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_EDIT_PRICE"].ToString()),
                Mpc_chk_credit = row["MPC_CHK_CREDIT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_CHK_CREDIT"].ToString()),
                Mpc_edit_rate = row["MPC_EDIT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPC_EDIT_RATE"].ToString()),
                Mpc_def_dis_rate = row["MPC_DEF_DIS_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPC_DEF_DIS_RATE"].ToString()),
                Mpc_print_wara_remarks = row["MPC_PRINT_WARA_REMARKS"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_PRINT_WARA_REMARKS"].ToString()),
                Mpc_inter_com = row["MPC_INTER_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_INTER_COM"].ToString()),
                Mpc_print_dis = row["MPC_PRINT_DIS"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_PRINT_DIS"].ToString()),
                Mpc_print_payment = row["MPC_PRINT_PAYMENT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_PRINT_PAYMENT"].ToString()),
                Mpc_check_pay = row["MPC_CHECK_PAY"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_CHECK_PAY"].ToString()),
                Mpc_check_cm = row["MPC_CHECK_CM"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_CHECK_CM"].ToString()),
                Mpc_without_price = row["MPC_WITHOUT_PRICE"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_WITHOUT_PRICE"].ToString()),
                Mpc_order_valid_pd = row["MPC_ORDER_VALID_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_ORDER_VALID_PD"].ToString()),
                Mpc_order_restric = row["MPC_ORDER_RESTRIC"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_ORDER_RESTRIC"].ToString()),
                Mpc_wara_extend = row["MPC_WARA_EXTEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_WARA_EXTEND"].ToString()),
                Mpc_so_sms = row["MPC_SO_SMS"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_SO_SMS"].ToString()),
                Mpc_multi_dept = row["MPC_MULTI_DEPT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_MULTI_DEPT"].ToString()),
                Mpc_def_dept = row["MPC_DEF_DEPT"] == DBNull.Value ? string.Empty : row["MPC_DEF_DEPT"].ToString(),
                Mpc_def_loc = row["MPC_DEF_LOC"] == DBNull.Value ? string.Empty : row["MPC_DEF_LOC"].ToString(),
                Mpc_man = row["MPC_MAN"] == DBNull.Value ? string.Empty : row["MPC_MAN"].ToString(),
                Mpc_def_exrate = row["MPC_DEF_EXRATE"] == DBNull.Value ? string.Empty : row["MPC_DEF_EXRATE"].ToString(),
                Mpc_def_customer = row["MPC_DEF_CUSTOMER"] == DBNull.Value ? string.Empty : row["MPC_DEF_CUSTOMER"].ToString(),
                Mpc_add_hours = row["MPC_ADD_HOURS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_ADD_HOURS"].ToString()),
                Mpc_email = row["MPC_EMAIL"] == DBNull.Value ? string.Empty : row["MPC_EMAIL"].ToString(),
                Mpc_fwd_sale_st = row["MPC_FWD_SALE_ST"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_FWD_SALE_ST"].ToString()),
                Mpc_max_fwdsale = row["MPC_MAX_FWDSALE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_MAX_FWDSALE"].ToString()),
                Mpc_hp_sys_rec = row["MPC_HP_SYS_REC"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_HP_SYS_REC"].ToString()),
                Mpc_is_chk_man_doc = row["MPC_IS_CHK_MAN_DOC"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_IS_CHK_MAN_DOC"].ToString()),
                Mpc_is_do_now = row["MPC_IS_DO_NOW"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_IS_DO_NOW"].ToString()),
                MPC_DIST = row["MPC_DIST"] == DBNull.Value ? string.Empty : row["MPC_DIST"].ToString(),
                MPC_PROV = row["MPC_PROV"] == DBNull.Value ? string.Empty : row["MPC_PROV"].ToString(),
                MPC_OPN_DT = row["MPC_OPN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_OPN_DT"].ToString()),
                MPC_SQ_FT = row["MPC_SQ_FT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_SQ_FT"].ToString()),
                MPC_MAN_NAME = row["MPC_MAN_NAME"] == DBNull.Value ? string.Empty : row["MPC_MAN_NAME"].ToString(),
                MPC_JOINED_DT = row["MPC_JOINED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_JOINED_DT"].ToString()),
                MPC_HOVR_DT = row["MPC_HOVR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_HOVR_DT"].ToString()),
                MPC_NO_OF_STAFF = row["MPC_NO_OF_STAFF"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_NO_OF_STAFF"].ToString()),
                MPC_GRADE = row["MPC_GRADE"] == DBNull.Value ? string.Empty : row["MPC_GRADE"].ToString(),
                MPC_NUM_FWDSALE = row["MPC_NUM_FWDSALE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_NUM_FWDSALE"].ToString()),
                Mpc_mul_crnote = row["MPC_MUL_CRNOTE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_MUL_CRNOTE"].ToString()),
                Mpc_cate = row["MPC_CATE"] == DBNull.Value ? string.Empty : row["MPC_CATE"].ToString(),
                Mpc_issp_tax = row["MPC_ISSP_TAX"] == DBNull.Value ? false : Convert.ToBoolean(row["MPC_ISSP_TAX"].ToString()),
                Mpc_cre_by = row["MPC_CRE_BY"] == DBNull.Value ? string.Empty : row["MPC_CRE_BY"].ToString(),
                Mpc_cre_dt = row["MPC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_CRE_DT"].ToString()),
                Mpc_mod_by = row["MPC_MOD_BY"] == DBNull.Value ? string.Empty : row["MPC_MOD_BY"].ToString(),
                Mpc_mod_dt = row["MPC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPC_MOD_DT"].ToString()),
                Mpc_so_res = row["MPC_SO_RES"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_SO_RES"].ToString()),
                Mpc_so_rest_stk = row["MPC_SO_REST_STK"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_SO_REST_STK"].ToString()),
                Mpc_ord_alpbt = row["MPC_ORD_ALPBT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPC_ORD_ALPBT"].ToString())
            };
        }
    }
}
