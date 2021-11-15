using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
[Serializable]
    public class VehicalRegistration
    {
        #region private memebers

        Boolean p_srvt_isapp;

        decimal p_seq;
        string p_svrt_com;
        string p_svrt_pc;
        DateTime p_svrt_dt;
        string p_svrt_inv_no;
        string p_svrt_sales_tp;
        decimal p_svrt_reg_val;
        decimal p_svrt_claim_val;
        string p_svrt_id_tp;
        string p_svrt_id_ref;
        string p_svrt_cust_cd;
        string p_svrt_cust_title;
        string p_svrt_last_name;
        string p_svrt_full_name;
        string p_svrt_initial;
        string p_svrt_add01;
        string p_svrt_add02;
        string p_svrt_city;
        string p_svrt_district;
        string p_svrt_province;
        string p_svrt_contact;
        string p_svrt_model;
        string p_svrt_brd;
        string p_svrt_chassis;
        string p_svrt_engine;
        string p_svrt_color;
        string p_svrt_fuel;
        decimal p_svrt_capacity;
        string p_svrt_unit;
        decimal p_svrt_horse_power;
        decimal p_svrt_wheel_base;
        string p_svrt_tire_front;
        string p_svrt_tire_rear;
        decimal p_svrt_weight;
        decimal p_svrt_man_year;
        string p_svrt_import;
        string p_svrt_authority;
        string p_svrt_country;
        DateTime p_svrt_custom_dt;
        DateTime p_svrt_clear_dt;
        DateTime p_svrt_declear_dt;
        string p_svrt_importer;
        string p_svrt_importer_add01;
        string p_svrt_importer_add02;
        string p_svrt_cre_bt;
        DateTime p_svrt_cre_dt;
        decimal p_svrt_prnt_stus;
        string p_svrt_prnt_by;
        DateTime p_svrt_prnt_dt;
        decimal p_srvt_rmv_stus;
        string p_srvt_rmv_by;
        DateTime p_srvt_rmv_dt;
        string p_svrt_veh_reg_no;
        string p_svrt_reg_by;
        DateTime p_svrt_reg_dt;
        string p_svrt_image;
        decimal p_srvt_cust_stus;
        string p_srvt_cust_by;
        DateTime p_srvt_cust_dt;
        decimal p_srvt_cls_stus;
        string p_srvt_cls_by;
        DateTime p_srvt_cls_dt;
        string p_srvt_insu_ref;
        string p_srvt_ref_no;
        string p_srvt_itm_cd;
        private string _srvt_cr_pod_ref;
        private DateTime _srvt_no_plt_dt;
        private string _srvt_no_plt_pod_ref;
        //added by kapila 4/11/2014
         DateTime _SRVT_RMV_TIME;
         DateTime _SVRT_REG_TIME;
         DateTime _SRVT_CUST_TIME;
         DateTime _SRVT_CLS_TIME;
         DateTime _SRVT_NO_PLT_TIME;
         string _SRVT_PLT_REC_BY;
         DateTime _SRVT_PLT_REC_DT;
         DateTime _SRVT_PLT_REC_TIME;
         string _SRVT_PLT_REC_REM;
        //kapila
         private Int32 _SRVT_IS_RESEND;
         private DateTime _SRVT_RESEND_DT;
         private string _SRVT_RESEND_REM;
         private Int32 _SRVT_IS_REREC;
         private DateTime _SRVT_REREC_DT;

         private string _srvt_cour_to;
         private Int32 _SRVT_IS_RET_HO;
         private DateTime _SRVT_RET_HO_DT;
         private string _SRVT_RET_HO_REM;
         private DateTime _SRVT_CR_COUR_DT;

    //kapila 27/5/2016
         private string _SRVT_IS_TX_REM;
         private Int32 _SRVT_IS_TX_RP;
         private DateTime _SRVT_IS_TX_DT;
         private string _SRVT_IS_REC_REM;
         private Int32 _SRVT_IS_REC_RP;
         private DateTime _SRVT_IS_REC_DT;


        #endregion

        #region public roperties
         public Int32 SRVT_IS_TX_RP
         {
             get { return _SRVT_IS_TX_RP; }
             set { _SRVT_IS_TX_RP = value; }
         }
         public DateTime SRVT_IS_TX_DT
         {
             get { return _SRVT_IS_TX_DT; }
             set { _SRVT_IS_TX_DT = value; }
         }
         public string SRVT_IS_TX_REM
         {
             get { return _SRVT_IS_TX_REM; }
             set { _SRVT_IS_TX_REM = value; }
         }
         public Int32 SRVT_IS_REC_RP
         {
             get { return _SRVT_IS_REC_RP; }
             set { _SRVT_IS_REC_RP = value; }
         }
         public DateTime SRVT_IS_REC_DT
         {
             get { return _SRVT_IS_REC_DT; }
             set { _SRVT_IS_REC_DT = value; }
         }
         public string SRVT_IS_REC_REM
         {
             get { return _SRVT_IS_REC_REM; }
             set { _SRVT_IS_REC_REM = value; }
         }
    //-----------------
         public DateTime SRVT_CR_COUR_DT
         {
             get { return _SRVT_CR_COUR_DT; }
             set { _SRVT_CR_COUR_DT = value; }
         }
         public Int32 SRVT_IS_RET_HO
         {
             get { return _SRVT_IS_RET_HO; }
             set { _SRVT_IS_RET_HO = value; }
         }
         public DateTime SRVT_RET_HO_DT
         {
             get { return _SRVT_RET_HO_DT; }
             set { _SRVT_RET_HO_DT = value; }
         }
         public string SRVT_RET_HO_REM
         {
             get { return _SRVT_RET_HO_REM; }
             set { _SRVT_RET_HO_REM = value; }
         }


         public string srvt_cour_to
         {
             get { return _srvt_cour_to; }
             set { _srvt_cour_to = value; }
         }
         public DateTime SRVT_REREC_DT
         {
             get { return _SRVT_REREC_DT; }
             set { _SRVT_REREC_DT = value; }
         }
         public Int32 SRVT_IS_REREC
         {
             get { return _SRVT_IS_REREC; }
             set { _SRVT_IS_REREC = value; }
         }
         public string SRVT_RESEND_REM
         {
             get { return _SRVT_RESEND_REM; }
             set { _SRVT_RESEND_REM = value; }
         }
         public Int32 SRVT_IS_RESEND
         {
             get { return _SRVT_IS_RESEND; }
             set { _SRVT_IS_RESEND = value; }
         }
         public DateTime SRVT_RESEND_DT
         {
             get { return _SRVT_RESEND_DT; }
             set { _SRVT_RESEND_DT = value; }
         }
         public string SRVT_PLT_REC_REM
         {
             get { return _SRVT_PLT_REC_REM; }
             set { _SRVT_PLT_REC_REM = value; }
         }
        public DateTime SVRT_REG_TIME
        {
            get { return _SVRT_REG_TIME; }
            set {  _SVRT_REG_TIME= value; }
        }
        public DateTime SRVT_CUST_TIME
        {
            get { return _SRVT_CUST_TIME; }
            set { _SRVT_CUST_TIME = value; }
        }
        public DateTime SRVT_CLS_TIME
        {
            get { return _SRVT_CLS_TIME; }
            set {  _SRVT_CLS_TIME= value; }
        }
        public DateTime SRVT_NO_PLT_TIME
        {
            get { return _SRVT_NO_PLT_TIME; }
            set {  _SRVT_NO_PLT_TIME= value; }
        }
        public string SRVT_PLT_REC_BY 
        {
            get { return _SRVT_PLT_REC_BY ; }
            set { _SRVT_PLT_REC_BY = value; }
        }
        public DateTime SRVT_PLT_REC_DT
        {
            get { return _SRVT_PLT_REC_DT; }
            set {  _SRVT_PLT_REC_DT= value; }
        }
        public DateTime SRVT_PLT_REC_TIME
        {
            get { return _SRVT_PLT_REC_TIME ; }
            set { _SRVT_PLT_REC_TIME = value; }
        }
        public DateTime SRVT_RMV_TIME
        {
            get { return _SRVT_RMV_TIME; }
            set { _SRVT_RMV_TIME = value; }
        }
        public Boolean P_srvt_isapp 
        { 
            get { return p_srvt_isapp; } 
            set { p_srvt_isapp = value; } 
        }

        public decimal P_seq
        {
            get { return p_seq; }
            set { p_seq = value; }
        }

        public string P_svrt_com
        {
            get { return p_svrt_com; }
            set { p_svrt_com = value; }
        }

        public string P_svrt_pc
        {
            get { return p_svrt_pc; }
            set { p_svrt_pc = value; }
        }

        public DateTime P_svrt_dt
        {
            get { return p_svrt_dt; }
            set { p_svrt_dt = value; }
        }

        public string P_svrt_inv_no
        {
            get { return p_svrt_inv_no; }
            set { p_svrt_inv_no = value; }
        }

        public string P_svrt_sales_tp
        {
            get { return p_svrt_sales_tp; }
            set { p_svrt_sales_tp = value; }
        }

        public decimal P_svrt_reg_val
        {
            get { return p_svrt_reg_val; }
            set { p_svrt_reg_val = value; }
        }

        public decimal P_svrt_claim_val
        {
            get { return p_svrt_claim_val; }
            set { p_svrt_claim_val = value; }
        }

        public string P_svrt_id_tp
        {
            get { return p_svrt_id_tp; }
            set { p_svrt_id_tp = value; }
        }

        public string P_svrt_id_ref
        {
            get { return p_svrt_id_ref; }
            set { p_svrt_id_ref = value; }
        }

        public string P_svrt_cust_cd
        {
            get { return p_svrt_cust_cd; }
            set { p_svrt_cust_cd = value; }
        }

        public string P_svrt_cust_title
        {
            get { return p_svrt_cust_title; }
            set { p_svrt_cust_title = value; }
        }

        public string P_svrt_last_name
        {
            get { return p_svrt_last_name; }
            set { p_svrt_last_name = value; }
        }

        public string P_svrt_full_name
        {
            get { return p_svrt_full_name; }
            set { p_svrt_full_name = value; }
        }

        public string P_svrt_initial
        {
            get { return p_svrt_initial; }
            set { p_svrt_initial = value; }
        }

        public string P_svrt_add01
        {
            get { return p_svrt_add01; }
            set { p_svrt_add01 = value; }
        }

        public string P_svrt_add02
        {
            get { return p_svrt_add02; }
            set { p_svrt_add02 = value; }
        }

        public string P_svrt_city
        {
            get { return p_svrt_city; }
            set { p_svrt_city = value; }
        }

        public string P_svrt_district
        {
            get { return p_svrt_district; }
            set { p_svrt_district = value; }
        }

        public string P_svrt_province
        {
            get { return p_svrt_province; }
            set { p_svrt_province = value; }
        }

        public string P_svrt_contact
        {
            get { return p_svrt_contact; }
            set { p_svrt_contact = value; }
        }

        public string P_svrt_model
        {
            get { return p_svrt_model; }
            set { p_svrt_model = value; }
        }

        public string P_svrt_brd
        {
            get { return p_svrt_brd; }
            set { p_svrt_brd = value; }
        }

        public string P_svrt_chassis
        {
            get { return p_svrt_chassis; }
            set { p_svrt_chassis = value; }
        }

        public string P_svrt_engine
        {
            get { return p_svrt_engine; }
            set { p_svrt_engine = value; }
        }

        public string P_svrt_color
        {
            get { return p_svrt_color; }
            set { p_svrt_color = value; }
        }

        public string P_svrt_fuel
        {
            get { return p_svrt_fuel; }
            set { p_svrt_fuel = value; }
        }

        public decimal P_svrt_capacity
        {
            get { return p_svrt_capacity; }
            set { p_svrt_capacity = value; }
        }

        public string P_svrt_unit
        {
            get { return p_svrt_unit; }
            set { p_svrt_unit = value; }
        }

        public decimal P_svrt_horse_power
        {
            get { return p_svrt_horse_power; }
            set { p_svrt_horse_power = value; }
        }

        public decimal P_svrt_wheel_base
        {
            get { return p_svrt_wheel_base; }
            set { p_svrt_wheel_base = value; }
        }

        public string P_svrt_tire_front
        {
            get { return p_svrt_tire_front; }
            set { p_svrt_tire_front = value; }
        }

        public string P_svrt_tire_rear
        {
            get { return p_svrt_tire_rear; }
            set { p_svrt_tire_rear = value; }
        }

        public decimal P_svrt_weight
        {
            get { return p_svrt_weight; }
            set { p_svrt_weight = value; }
        }

        public decimal P_svrt_man_year
        {
            get { return p_svrt_man_year; }
            set { p_svrt_man_year = value; }
        }

        public string P_svrt_import
        {
            get { return p_svrt_import; }
            set { p_svrt_import = value; }
        }

        public string P_svrt_authority
        {
            get { return p_svrt_authority; }
            set { p_svrt_authority = value; }
        }

        public string P_svrt_country
        {
            get { return p_svrt_country; }
            set { p_svrt_country = value; }
        }

        public DateTime P_svrt_custom_dt
        {
            get { return p_svrt_custom_dt; }
            set { p_svrt_custom_dt = value; }
        }

        public DateTime P_svrt_clear_dt
        {
            get { return p_svrt_clear_dt; }
            set { p_svrt_clear_dt = value; }
        }

        public DateTime P_svrt_declear_dt
        {
            get { return p_svrt_declear_dt; }
            set { p_svrt_declear_dt = value; }
        }

        public string P_svrt_importer
        {
            get { return p_svrt_importer; }
            set { p_svrt_importer = value; }
        }

        public string P_svrt_importer_add01
        {
            get { return p_svrt_importer_add01; }
            set { p_svrt_importer_add01 = value; }
        }

        public string P_svrt_importer_add02
        {
            get { return p_svrt_importer_add02; }
            set { p_svrt_importer_add02 = value; }
        }

        public string P_svrt_cre_bt
        {
            get { return p_svrt_cre_bt; }
            set { p_svrt_cre_bt = value; }
        }

        public DateTime P_svrt_cre_dt
        {
            get { return p_svrt_cre_dt; }
            set { p_svrt_cre_dt = value; }
        }

        public decimal P_svrt_prnt_stus
        {
            get { return p_svrt_prnt_stus; }
            set { p_svrt_prnt_stus = value; }
        }

        public string P_svrt_prnt_by
        {
            get { return p_svrt_prnt_by; }
            set { p_svrt_prnt_by = value; }
        }

        public DateTime P_svrt_prnt_dt
        {
            get { return p_svrt_prnt_dt; }
            set { p_svrt_prnt_dt = value; }
        }

        public string P_srvt_rmv_by
        {
            get { return p_srvt_rmv_by; }
            set { p_srvt_rmv_by = value; }
        }

        public DateTime P_srvt_rmv_dt
        {
            get { return p_srvt_rmv_dt; }
            set { p_srvt_rmv_dt = value; }
        }

        public decimal P_srvt_rmv_stus
        {
            get { return p_srvt_rmv_stus; }
            set { p_srvt_rmv_stus = value; }
        }

        public string P_svrt_veh_reg_no
        {
            get { return p_svrt_veh_reg_no; }
            set { p_svrt_veh_reg_no = value; }
        }

        public string P_svrt_reg_by
        {
            get { return p_svrt_reg_by; }
            set { p_svrt_reg_by = value; }
        }

        public DateTime P_svrt_reg_dt
        {
            get { return p_svrt_reg_dt; }
            set { p_svrt_reg_dt = value; }
        }

        public string P_svrt_image
        {
            get { return p_svrt_image; }
            set { p_svrt_image = value; }
        }

        public decimal P_srvt_cust_stus
        {
            get { return p_srvt_cust_stus; }
            set { p_srvt_cust_stus = value; }
        }

        public string P_srvt_cust_by
        {
            get { return p_srvt_cust_by; }
            set { p_srvt_cust_by = value; }
        }

        public DateTime P_srvt_cust_dt
        {
            get { return p_srvt_cust_dt; }
            set { p_srvt_cust_dt = value; }
        }

        public decimal P_srvt_cls_stus
        {
            get { return p_srvt_cls_stus; }
            set { p_srvt_cls_stus = value; }
        }

        public string P_srvt_cls_by
        {
            get { return p_srvt_cls_by; }
            set { p_srvt_cls_by = value; }
        }

        public DateTime P_srvt_cls_dt
        {
            get { return p_srvt_cls_dt; }
            set { p_srvt_cls_dt = value; }
        }

        public string P_srvt_insu_ref
        {
            get { return p_srvt_insu_ref; }
            set { p_srvt_insu_ref = value; }
        }

        public string P_srvt_ref_no
        {
            get { return p_srvt_ref_no; }
            set { p_srvt_ref_no = value; }
        }

        public string P_srvt_itm_cd
        {
            get { return p_srvt_itm_cd; }
            set { p_srvt_itm_cd = value; }
        }


        public string Srvt_cr_pod_ref
        {
            get { return _srvt_cr_pod_ref; }
            set { _srvt_cr_pod_ref = value; }
        }
        public DateTime Srvt_no_plt_dt
        {
            get { return _srvt_no_plt_dt; }
            set { _srvt_no_plt_dt = value; }
        }
        public string Srvt_no_plt_pod_ref
        {
            get { return _srvt_no_plt_pod_ref; }
            set { _srvt_no_plt_pod_ref = value; }
        }

        #endregion

        public static VehicalRegistration Converter(DataRow row)
        {
            return new VehicalRegistration
            {
                P_srvt_isapp = row["SRVT_ISAPP"] == DBNull.Value ? false : Convert.ToBoolean(row["SRVT_ISAPP"]),

                P_svrt_com = ((row["SVRT_COM"] == DBNull.Value) ? string.Empty : row["SVRT_COM"].ToString()),
                P_seq = ((row["SVRT_SEQ"] == DBNull.Value) ? 0 :Convert.ToDecimal( row["SVRT_SEQ"].ToString())),
                P_svrt_pc = ((row["SVRT_PC"] == DBNull.Value) ? string.Empty : row["SVRT_PC"].ToString()),
                P_svrt_dt = ((row["SVRT_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVRT_DT"])),
                P_svrt_inv_no = ((row["SVRT_INV_NO"] == DBNull.Value) ? string.Empty : row["SVRT_INV_NO"].ToString()),
                P_svrt_sales_tp = ((row["SVRT_SALES_TP"] == DBNull.Value) ? string.Empty : row["SVRT_SALES_TP"].ToString()),
                P_svrt_reg_val = ((row["SVRT_REG_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal( row["SVRT_REG_VAL"].ToString())),
                P_svrt_claim_val = ((row["SVRT_CLAIM_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVRT_CLAIM_VAL"].ToString())),
                P_svrt_id_tp = ((row["SVRT_ID_TP"] == DBNull.Value) ? string.Empty : row["SVRT_ID_TP"].ToString()),
                P_svrt_id_ref = ((row["SVRT_ID_REF"] == DBNull.Value) ? string.Empty : row["SVRT_ID_REF"].ToString()),
                P_svrt_cust_cd = ((row["SVRT_CUST_CD"] == DBNull.Value) ? string.Empty : row["SVRT_CUST_CD"].ToString()),
                P_svrt_cust_title = ((row["SVRT_CUST_TITLE"] == DBNull.Value) ? string.Empty : row["SVRT_CUST_TITLE"].ToString()),
                P_svrt_last_name = ((row["SVRT_LAST_NAME"] == DBNull.Value) ? string.Empty : row["SVRT_LAST_NAME"].ToString()),
                P_svrt_full_name = ((row["SVRT_FULL_NAME"] == DBNull.Value) ? string.Empty : row["SVRT_FULL_NAME"].ToString()),
                P_svrt_initial = ((row["SVRT_INITIAL"] == DBNull.Value) ? string.Empty : row["SVRT_INITIAL"].ToString()),
                P_svrt_add01 = ((row["SVRT_ADD01"] == DBNull.Value) ? string.Empty : row["SVRT_ADD01"].ToString()),
                P_svrt_add02 = ((row["SVRT_ADD02"] == DBNull.Value) ? string.Empty : row["SVRT_ADD02"].ToString()),
                P_svrt_city = ((row["SVRT_CITY"] == DBNull.Value) ? string.Empty : row["SVRT_CITY"].ToString()),
                P_svrt_district = ((row["SVRT_DISTRICT"] == DBNull.Value) ? string.Empty : row["SVRT_DISTRICT"].ToString()),
                P_svrt_province = ((row["SVRT_PROVINCE"] == DBNull.Value) ? string.Empty : row["SVRT_PROVINCE"].ToString()),
                P_svrt_contact = ((row["SVRT_CONTACT"] == DBNull.Value) ? string.Empty : row["SVRT_CONTACT"].ToString()),
                P_svrt_model = ((row["SVRT_MODEL"] == DBNull.Value) ? string.Empty : row["SVRT_MODEL"].ToString()),
                P_svrt_brd = ((row["SVRT_BRD"] == DBNull.Value) ? string.Empty : row["SVRT_BRD"].ToString()),
                P_svrt_chassis = ((row["SVRT_CHASSIS"] == DBNull.Value) ? string.Empty : row["SVRT_CHASSIS"].ToString()),
                P_svrt_engine = ((row["SVRT_ENGINE"] == DBNull.Value) ? string.Empty : row["SVRT_ENGINE"].ToString()),
                P_svrt_color = ((row["SVRT_COLOR"] == DBNull.Value) ? string.Empty : row["SVRT_COLOR"].ToString()),
                P_svrt_fuel = ((row["SVRT_FUEL"] == DBNull.Value) ? string.Empty : row["SVRT_FUEL"].ToString()),
                P_svrt_capacity = ((row["SVRT_CAPACITY"] == DBNull.Value) ? 0 :Convert.ToDecimal( row["SVRT_CAPACITY"].ToString())),
                P_svrt_unit = ((row["SVRT_UNIT"] == DBNull.Value) ? string.Empty : row["SVRT_UNIT"].ToString()),
                P_svrt_horse_power = ((row["SVRT_HORSE_POWER"] == DBNull.Value) ?0 : Convert.ToDecimal(row["SVRT_HORSE_POWER"].ToString())),
                P_svrt_wheel_base = ((row["SVRT_WHEEL_BASE"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVRT_WHEEL_BASE"].ToString())),
                P_svrt_tire_front = ((row["SVRT_TIRE_FRONT"] == DBNull.Value) ? string.Empty : row["SVRT_TIRE_FRONT"].ToString()),
                P_svrt_tire_rear = ((row["SVRT_TIRE_REAR"] == DBNull.Value) ? string.Empty : row["SVRT_TIRE_REAR"].ToString()),
                P_svrt_weight = ((row["SVRT_WEIGHT"] == DBNull.Value) ? 0 :Convert.ToDecimal( row["SVRT_WEIGHT"].ToString())),
                P_svrt_man_year = ((row["SVRT_MAN_YEAR"] == DBNull.Value) ? 0 :Convert.ToDecimal( row["SVRT_MAN_YEAR"].ToString())),
                P_svrt_import = ((row["SVRT_IMPORT"] == DBNull.Value) ? string.Empty : row["SVRT_IMPORT"].ToString()),
                P_svrt_authority = ((row["SVRT_AUTHORITY"] == DBNull.Value) ? string.Empty : row["SVRT_AUTHORITY"].ToString()),
                P_svrt_country = ((row["SVRT_COUNTRY"] == DBNull.Value) ? string.Empty : row["SVRT_COUNTRY"].ToString()),
                P_svrt_custom_dt = ((row["SVRT_CUSTOM_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime( row["SVRT_CUSTOM_DT"].ToString())),
                P_svrt_clear_dt = ((row["SVRT_CLEAR_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime( row["SVRT_CLEAR_DT"].ToString())),
                P_svrt_declear_dt = ((row["SVRT_DECLEAR_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime( row["SVRT_DECLEAR_DT"].ToString())),
                P_svrt_importer = ((row["SVRT_IMPORTER"] == DBNull.Value) ? string.Empty : row["SVRT_IMPORTER"].ToString()),
                P_svrt_importer_add01 = ((row["SVRT_IMPORTER_ADD01"] == DBNull.Value) ? string.Empty : row["SVRT_IMPORTER_ADD01"].ToString()),
                P_svrt_importer_add02 = ((row["SVRT_IMPORTER_ADD02"] == DBNull.Value) ? string.Empty : row["SVRT_IMPORTER_ADD02"].ToString()),
                P_svrt_cre_bt = ((row["SVRT_CRE_BT"] == DBNull.Value) ? string.Empty : row["SVRT_CRE_BT"].ToString()),
                P_svrt_cre_dt = ((row["SVRT_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVRT_CRE_DT"].ToString())),
                P_svrt_prnt_stus = ((row["SVRT_PRNT_STUS"] == DBNull.Value) ?0 :Convert.ToDecimal( row["SVRT_PRNT_STUS"].ToString())),
                P_svrt_prnt_by = ((row["SVRT_PRNT_BY"] == DBNull.Value) ? string.Empty : row["SVRT_PRNT_BY"].ToString()),
                P_svrt_prnt_dt = ((row["SVRT_PRNT_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVRT_PRNT_DT"].ToString())),
                P_srvt_rmv_by = ((row["SRVT_RMV_BY"] == DBNull.Value) ? string.Empty : row["SRVT_RMV_BY"].ToString()),
                P_srvt_rmv_dt = ((row["SRVT_RMV_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_RMV_DT"].ToString())),
                P_srvt_rmv_stus = ((row["SRVT_RMV_STUS"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SRVT_RMV_STUS"].ToString())),
                P_svrt_veh_reg_no = ((row["SVRT_VEH_REG_NO"] == DBNull.Value) ? string.Empty : row["SVRT_VEH_REG_NO"].ToString()),
                P_svrt_reg_by = ((row["SVRT_REG_BY"] == DBNull.Value) ? string.Empty : row["SVRT_REG_BY"].ToString()),
                P_svrt_reg_dt = ((row["SVRT_REG_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVRT_REG_DT"].ToString())),
                P_svrt_image = ((row["SVRT_IMAGE"] == DBNull.Value) ? string.Empty : row["SVRT_IMAGE"].ToString()),
                P_srvt_cust_stus = ((row["SRVT_CUST_STUS"] == DBNull.Value) ? 0 :Convert.ToDecimal( row["SRVT_CUST_STUS"].ToString())),
                P_srvt_cust_by = ((row["SRVT_CUST_BY"] == DBNull.Value) ? string.Empty : row["SRVT_CUST_BY"].ToString()),
                P_srvt_cust_dt = ((row["SRVT_CUST_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_CUST_DT"].ToString())),
                P_srvt_cls_stus = ((row["SRVT_CLS_STUS"] == DBNull.Value) ? 0 :Convert.ToDecimal(row["SRVT_CLS_STUS"].ToString())),
                P_srvt_cls_by = ((row["SRVT_CLS_BY"] == DBNull.Value) ? string.Empty : row["SRVT_CLS_BY"].ToString()),
                P_srvt_cls_dt = ((row["SRVT_CLS_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_CLS_DT"].ToString())),
                P_srvt_insu_ref = ((row["SRVT_INSU_REF"] == DBNull.Value) ? string.Empty : row["SRVT_INSU_REF"].ToString()),
                P_srvt_ref_no = ((row["SVRT_REF_NO"] == DBNull.Value) ? string.Empty : row["SVRT_REF_NO"].ToString()),
                P_srvt_itm_cd = ((row["SRVT_ITM_CD"] == DBNull.Value) ? string.Empty : row["SRVT_ITM_CD"].ToString()),
                Srvt_cr_pod_ref = row["SRVT_CR_POD_REF"] == DBNull.Value ? string.Empty : row["SRVT_CR_POD_REF"].ToString(),
                Srvt_no_plt_dt = row["SRVT_NO_PLT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_NO_PLT_DT"]),
                SRVT_RMV_TIME = row["SRVT_RMV_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_RMV_TIME"]),
                SVRT_REG_TIME = row["SVRT_REG_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVRT_REG_TIME"]),
                SRVT_CUST_TIME = row["SRVT_CUST_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_CUST_TIME"]),
                SRVT_CLS_TIME = row["SRVT_CLS_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_CLS_TIME"]),
                SRVT_NO_PLT_TIME = row["SRVT_NO_PLT_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_NO_PLT_TIME"]),
                SRVT_PLT_REC_BY = row["SRVT_PLT_REC_BY"] == DBNull.Value ? string.Empty : row["SRVT_PLT_REC_BY"].ToString(),
                SRVT_PLT_REC_DT = row["SRVT_PLT_REC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_PLT_REC_DT"]),
                SRVT_PLT_REC_TIME = row["SRVT_PLT_REC_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_PLT_REC_TIME"]),
                SRVT_PLT_REC_REM = row["SRVT_PLT_REC_REM"] == DBNull.Value ? string.Empty : row["SRVT_PLT_REC_REM"].ToString(),
                Srvt_no_plt_pod_ref = row["SRVT_NO_PLT_POD_REF"] == DBNull.Value ? string.Empty : row["SRVT_NO_PLT_POD_REF"].ToString(),
                SRVT_IS_RESEND = row["SRVT_IS_RESEND"] == DBNull.Value ? 0 :  Convert.ToInt32(row["SRVT_IS_RESEND"]),
                SRVT_RESEND_DT = row["SRVT_RESEND_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_RESEND_DT"]),
                SRVT_RESEND_REM = row["SRVT_RESEND_REM"] == DBNull.Value ? string.Empty : row["SRVT_RESEND_REM"].ToString(),
                SRVT_IS_REREC = row["SRVT_IS_REREC"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRVT_IS_REREC"]),
                SRVT_REREC_DT = row["SRVT_REREC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_REREC_DT"]),
                SRVT_CR_COUR_DT = row["SRVT_CR_COUR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_CR_COUR_DT"]),

                SRVT_IS_TX_REM = row["SRVT_IS_TX_REM"] == DBNull.Value ? string.Empty : row["SRVT_IS_TX_REM"].ToString(),
                SRVT_IS_TX_RP = row["SRVT_IS_TX_RP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRVT_IS_TX_RP"]),
                SRVT_IS_TX_DT = row["SRVT_IS_TX_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_IS_TX_DT"]),

                SRVT_IS_REC_REM = row["SRVT_IS_REC_REM"] == DBNull.Value ? string.Empty : row["SRVT_IS_REC_REM"].ToString(),
                SRVT_IS_REC_RP = row["SRVT_IS_REC_RP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRVT_IS_REC_RP"]),
                SRVT_IS_REC_DT = row["SRVT_IS_REC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRVT_IS_REC_DT"])
                

            };
        }

    }
}
