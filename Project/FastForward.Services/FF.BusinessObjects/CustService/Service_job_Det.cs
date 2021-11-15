using System;
using System.Data;

namespace FF.BusinessObjects
{
    public class Service_job_Det
    {
        public Int32 Jbd_seq_no { get; set; }
        public string Jbd_jobno { get; set; }
        public Int32 Jbd_jobline { get; set; }
        public string Jbd_sjobno { get; set; }
        public string Jbd_loc { get; set; }
        public string Jbd_pc { get; set; }
        public string Jbd_itm_cd { get; set; }
        public string Jbd_itm_stus { get; set; }
        public string Jbd_itm_desc { get; set; }
        public string Jbd_brand { get; set; }
        public string Jbd_model { get; set; }
        public Decimal Jbd_itm_cost { get; set; }
        public string Jbd_ser1 { get; set; }
        public string Jbd_ser2 { get; set; }
        public string Jbd_warr { get; set; }
        public string Jbd_regno { get; set; }
        public Decimal Jbd_milage { get; set; }
        public Int32 Jbd_warr_stus { get; set; }
        public Int32 Jbd_onloan { get; set; }
        public DateTime Jbd_chg_warr_stdt { get; set; }
        public string Jbd_chg_warr_rmk { get; set; }
        public Int32 Jbd_isinsurance { get; set; }
        public string Jbd_cate1 { get; set; }
        public Int32 Jbd_ser_term { get; set; }
        public DateTime Jbd_lastwarr_stdt { get; set; }
        public Int32 Jbd_issued { get; set; }
        public string Jbd_mainitmcd { get; set; }
        public string Jbd_mainitmser { get; set; }
        public string Jbd_mainitmwarr { get; set; }
        public string Jbd_itmmfc { get; set; }
        public string Jbd_mainitmmfc { get; set; }
        public Int32 Jbd_availabilty { get; set; }
        public string Jbd_usejob { get; set; }
        public string Jbd_msnno { get; set; }
        public string Jbd_itmtp { get; set; }
        public string Jbd_serlocchr { get; set; }
        public string Jbd_custnotes { get; set; }
        public string Jbd_mainreqno { get; set; }
        public string Jbd_mainreqloc { get; set; }
        public string Jbd_mainjobno { get; set; }
        public string Jbd_reqitmtp { get; set; }
        public string Jbd_reqno { get; set; }
        public Int32 Jbd_reqline { get; set; }
        public Int32 Jbd_isstockupdate { get; set; }
        public Int32 Jbd_isgatepass { get; set; }
        public Int32 Jbd_iswrn { get; set; }
        public Int32 Jbd_warrperiod { get; set; }
        public string Jbd_warrrmk { get; set; }
        public DateTime Jbd_warrstartdt { get; set; }
        public Int32 Jbd_warrreplace { get; set; }
        public DateTime Jbd_date_pur { get; set; }
        public string Jbd_invc_no { get; set; }
        public string Jbd_waraamd_seq { get; set; }
        public string Jbd_waraamd_by { get; set; }
        public DateTime Jbd_waraamd_dt { get; set; }
        public string Jbd_invc_showroom { get; set; }
        public string Jbd_aodissueloc { get; set; }
        public DateTime Jbd_aodissuedt { get; set; }
        public string Jbd_aodissueno { get; set; }
        public string Jbd_aodrecno { get; set; }
        public DateTime Jbd_techst_dt { get; set; }
        public DateTime Jbd_techfin_dt { get; set; }
        public string Jbd_msn_no { get; set; }
        public Int32 Jbd_isexternalitm { get; set; }
        public DateTime Jbd_conf_dt { get; set; }
        public string Jbd_conf_cd { get; set; }
        public string Jbd_conf_desc { get; set; }
        public string Jbd_conf_rmk { get; set; }
        public string Jbd_tranf_by { get; set; }
        public DateTime Jbd_tranf_dt { get; set; }
        public Int32 Jbd_do_invoice { get; set; }
        public string Jbd_insu_com { get; set; }
        public string Jbd_agreeno { get; set; }
        public Int32 Jbd_issrn { get; set; }
        public string Jbd_isagreement { get; set; }
        public string Jbd_cust_agreeno { get; set; }
        public string Jbd_quo_no { get; set; }
        public Decimal Jbd_stage { get; set; }
        public string Jbd_com { get; set; }
        public string Jbd_ser_id { get; set; }
        public DateTime Jbd_techst_dt_man { get; set; }
        public DateTime Jbd_techfin_dt_man { get; set; }
        public Int32 Jbd_reqwcn { get; set; }
        public DateTime Jbd_reqwcndt { get; set; }
        public DateTime Jbd_reqwcnsysdt { get; set; }
        public Int32 Jbd_sentwcn { get; set; }
        public Int32 Jbd_recwcn { get; set; }
        public Int32 Jbd_takewcn { get; set; }
        public DateTime Jbd_takewcndt { get; set; }
        public DateTime Jbd_takewcnsysdt { get; set; }
        public string Jbd_supp_cd { get; set; }
        public string Jbd_part_cd { get; set; }
        public string Jbd_oem_no { get; set; }
        public string Jbd_case_id { get; set; }
        public Int32 Jbd_act { get; set; }
        public Int32 Jbd_oldjobline { get; set; }
        public string Jbd_tech_rmk { get; set; }
        public string Jbd_tech_custrmk { get; set; }
        public string StageText { get; set; }
        public string Jbd_tech_cls_tp { get; set; }
        public Int32 Jbd_isfocapp { get; set; }

        //--Additional columns
        public Boolean Jbd_select { get; set; }
        public string Jbd_warr_stus_text { get; set; }

        public DateTime Sjb_dt { get; set; }
        public string Sjb_jobstp { get; set; }
        public Int32 Jbd_invc_line { get; set; }
        public Int32 Jbd_is_service { get; set; }
        public Int32 Jbd_is_updatesch { get; set; }


        public Int32 Jbd_swarr_stus { get; set; }
        public Int32 Jbd_swarrperiod { get; set; }
        public string Jbd_swarrrmk { get; set; }
        public DateTime Jbd_swarrstartdt { get; set; }
        public string Jbd_serold { get; set; }
        public Int32 Isold_part { get; set; }
        public Int32 jbd_isstockupdate { get; set; }

        public string Jbd_tech_cls_rmk { get; set; }
        public string JBS_DESC { get; set; }// Nadeeka 09-05-2015
        public Int32 Jbd_ser_repeat { get; set; }

        //Tharaka 2015-08-11
        public string JBD_REJECT_BY { get; set; }
        public DateTime JBD_REJECT_DT { get; set; }

        public Int32 Jbd_is_fgap { get; set; }

        public Int32 Jbd_is_closed { get; set; }
        public Decimal Jbd_rep_perc { get; set; }
        //Sanjeewa 2016-01-29
        public Int32 jbd_sw_stus { get; set; }
        public string jbd_pb { get; set; }
        public string jbd_pblvl { get; set; }
        public DateTime jbd_del_sale_dt { get; set; }        
        public Decimal Jbd_need_chk { get; set; }
        public string sjb_creby { get; set; }
        public Decimal Jbd_itm_qty { get; set; }
        public Int32 Swd_Line { get; set; }//Akila 2018/02/21
        public string sjb_req_no { get; set; }

        #region Converters

        public static Service_job_Det Converter(DataRow row)
        {
            return new Service_job_Det
            {
                Jbd_seq_no = row["JBD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SEQ_NO"].ToString()),
                Jbd_jobno = row["JBD_JOBNO"] == DBNull.Value ? string.Empty : row["JBD_JOBNO"].ToString(),
                Jbd_jobline = row["JBD_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_JOBLINE"].ToString()),
                Jbd_sjobno = row["JBD_SJOBNO"] == DBNull.Value ? string.Empty : row["JBD_SJOBNO"].ToString(),
                Jbd_loc = row["JBD_LOC"] == DBNull.Value ? string.Empty : row["JBD_LOC"].ToString(),
                Jbd_pc = row["JBD_PC"] == DBNull.Value ? string.Empty : row["JBD_PC"].ToString(),
                Jbd_itm_cd = row["JBD_ITM_CD"] == DBNull.Value ? string.Empty : row["JBD_ITM_CD"].ToString(),
                Jbd_itm_stus = row["JBD_ITM_STUS"] == DBNull.Value ? string.Empty : row["JBD_ITM_STUS"].ToString(),
                Jbd_itm_desc = row["JBD_ITM_DESC"] == DBNull.Value ? string.Empty : row["JBD_ITM_DESC"].ToString(),
                Jbd_brand = row["JBD_BRAND"] == DBNull.Value ? string.Empty : row["JBD_BRAND"].ToString(),
                Jbd_model = row["JBD_MODEL"] == DBNull.Value ? string.Empty : row["JBD_MODEL"].ToString(),
                Jbd_itm_cost = row["JBD_ITM_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_ITM_COST"].ToString()),
                Jbd_ser1 = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),
                Jbd_ser2 = row["JBD_SER2"] == DBNull.Value ? string.Empty : row["JBD_SER2"].ToString(),
                Jbd_warr = row["JBD_WARR"] == DBNull.Value ? string.Empty : row["JBD_WARR"].ToString(),
                Jbd_regno = row["JBD_REGNO"] == DBNull.Value ? string.Empty : row["JBD_REGNO"].ToString(),
                Jbd_milage = row["JBD_MILAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_MILAGE"].ToString()),
                Jbd_warr_stus = row["JBD_WARR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARR_STUS"].ToString()),
                Jbd_onloan = row["JBD_ONLOAN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ONLOAN"].ToString()),
                Jbd_chg_warr_stdt = row["JBD_CHG_WARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_CHG_WARR_STDT"].ToString()),
                Jbd_chg_warr_rmk = row["JBD_CHG_WARR_RMK"] == DBNull.Value ? string.Empty : row["JBD_CHG_WARR_RMK"].ToString(),
                Jbd_isinsurance = row["JBD_ISINSURANCE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISINSURANCE"].ToString()),
                Jbd_cate1 = row["JBD_CATE1"] == DBNull.Value ? string.Empty : row["JBD_CATE1"].ToString(),
                Jbd_ser_term = row["JBD_SER_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SER_TERM"].ToString()),
                Jbd_lastwarr_stdt = row["JBD_LASTWARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_LASTWARR_STDT"].ToString()),
                Jbd_issued = row["JBD_ISSUED"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISSUED"].ToString()),
                Jbd_mainitmcd = row["JBD_MAINITMCD"] == DBNull.Value ? string.Empty : row["JBD_MAINITMCD"].ToString(),
                Jbd_mainitmser = row["JBD_MAINITMSER"] == DBNull.Value ? string.Empty : row["JBD_MAINITMSER"].ToString(),
                Jbd_mainitmwarr = row["JBD_MAINITMWARR"] == DBNull.Value ? string.Empty : row["JBD_MAINITMWARR"].ToString(),
                Jbd_itmmfc = row["JBD_ITMMFC"] == DBNull.Value ? string.Empty : row["JBD_ITMMFC"].ToString(),
                Jbd_mainitmmfc = row["JBD_MAINITMMFC"] == DBNull.Value ? string.Empty : row["JBD_MAINITMMFC"].ToString(),
                Jbd_availabilty = row["JBD_AVAILABILTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_AVAILABILTY"].ToString()),
                Jbd_usejob = row["JBD_USEJOB"] == DBNull.Value ? string.Empty : row["JBD_USEJOB"].ToString(),
                Jbd_msnno = row["JBD_MSNNO"] == DBNull.Value ? string.Empty : row["JBD_MSNNO"].ToString(),
                Jbd_itmtp = row["JBD_ITMTP"] == DBNull.Value ? string.Empty : row["JBD_ITMTP"].ToString(),
                Jbd_serlocchr = row["JBD_SERLOCCHR"] == DBNull.Value ? string.Empty : row["JBD_SERLOCCHR"].ToString(),
                Jbd_custnotes = row["JBD_CUSTNOTES"] == DBNull.Value ? string.Empty : row["JBD_CUSTNOTES"].ToString(),
                Jbd_mainreqno = row["JBD_MAINREQNO"] == DBNull.Value ? string.Empty : row["JBD_MAINREQNO"].ToString(),
                Jbd_mainreqloc = row["JBD_MAINREQLOC"] == DBNull.Value ? string.Empty : row["JBD_MAINREQLOC"].ToString(),
                Jbd_mainjobno = row["JBD_MAINJOBNO"] == DBNull.Value ? string.Empty : row["JBD_MAINJOBNO"].ToString(),
                Jbd_reqitmtp = row["JBD_REQITMTP"] == DBNull.Value ? string.Empty : row["JBD_REQITMTP"].ToString(),
                Jbd_reqno = row["JBD_REQNO"] == DBNull.Value ? string.Empty : row["JBD_REQNO"].ToString(),
                Jbd_reqline = row["JBD_REQLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_REQLINE"].ToString()),
                Jbd_isstockupdate = row["JBD_ISSTOCKUPDATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISSTOCKUPDATE"].ToString()),
                Jbd_isgatepass = row["JBD_ISGATEPASS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISGATEPASS"].ToString()),
                Jbd_iswrn = row["JBD_ISWRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISWRN"].ToString()),
                Jbd_warrperiod = row["JBD_WARRPERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARRPERIOD"].ToString()),
                Jbd_warrrmk = row["JBD_WARRRMK"] == DBNull.Value ? string.Empty : row["JBD_WARRRMK"].ToString(),
                Jbd_warrstartdt = row["JBD_WARRSTARTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARRSTARTDT"].ToString()),
                Jbd_warrreplace = row["JBD_WARRREPLACE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARRREPLACE"].ToString()),
                Jbd_date_pur = row["JBD_DATE_PUR"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_DATE_PUR"].ToString()),
                Jbd_invc_no = row["JBD_INVC_NO"] == DBNull.Value ? string.Empty : row["JBD_INVC_NO"].ToString(),
                Jbd_waraamd_seq = row["JBD_WARAAMD_SEQ"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_SEQ"].ToString(),
                Jbd_waraamd_by = row["JBD_WARAAMD_BY"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_BY"].ToString(),
                Jbd_waraamd_dt = row["JBD_WARAAMD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARAAMD_DT"].ToString()),
                Jbd_invc_showroom = row["JBD_INVC_SHOWROOM"] == DBNull.Value ? string.Empty : row["JBD_INVC_SHOWROOM"].ToString(),
                Jbd_aodissueloc = row["JBD_AODISSUELOC"] == DBNull.Value ? string.Empty : row["JBD_AODISSUELOC"].ToString(),
                Jbd_aodissuedt = row["JBD_AODISSUEDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_AODISSUEDT"].ToString()),
                Jbd_aodissueno = row["JBD_AODISSUENO"] == DBNull.Value ? string.Empty : row["JBD_AODISSUENO"].ToString(),
                Jbd_aodrecno = row["JBD_AODRECNO"] == DBNull.Value ? string.Empty : row["JBD_AODRECNO"].ToString(),
                Jbd_techst_dt = row["JBD_TECHST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHST_DT"].ToString()),
                Jbd_techfin_dt = row["JBD_TECHFIN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHFIN_DT"].ToString()),
                Jbd_msn_no = row["JBD_MSN_NO"] == DBNull.Value ? string.Empty : row["JBD_MSN_NO"].ToString(),
                Jbd_isexternalitm = row["JBD_ISEXTERNALITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISEXTERNALITM"].ToString()),
                Jbd_conf_dt = row["JBD_CONF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_CONF_DT"].ToString()),
                Jbd_conf_cd = row["JBD_CONF_CD"] == DBNull.Value ? string.Empty : row["JBD_CONF_CD"].ToString(),
                Jbd_conf_desc = row["JBD_CONF_DESC"] == DBNull.Value ? string.Empty : row["JBD_CONF_DESC"].ToString(),
                Jbd_conf_rmk = row["JBD_CONF_RMK"] == DBNull.Value ? string.Empty : row["JBD_CONF_RMK"].ToString(),
                Jbd_tranf_by = row["JBD_TRANF_BY"] == DBNull.Value ? string.Empty : row["JBD_TRANF_BY"].ToString(),
                Jbd_tranf_dt = row["JBD_TRANF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TRANF_DT"].ToString()),
                Jbd_do_invoice = row["JBD_DO_INVOICE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_DO_INVOICE"].ToString()),
                Jbd_insu_com = row["JBD_INSU_COM"] == DBNull.Value ? string.Empty : row["JBD_INSU_COM"].ToString(),
                Jbd_agreeno = row["JBD_AGREENO"] == DBNull.Value ? string.Empty : row["JBD_AGREENO"].ToString(),
                Jbd_issrn = row["JBD_ISSRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISSRN"].ToString()),
                Jbd_isagreement = row["JBD_ISAGREEMENT"] == DBNull.Value ? string.Empty : row["JBD_ISAGREEMENT"].ToString(),
                Jbd_cust_agreeno = row["JBD_CUST_AGREENO"] == DBNull.Value ? string.Empty : row["JBD_CUST_AGREENO"].ToString(),
                Jbd_quo_no = row["JBD_QUO_NO"] == DBNull.Value ? string.Empty : row["JBD_QUO_NO"].ToString(),
                Jbd_stage = row["JBD_STAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_STAGE"].ToString()),
                Jbd_com = row["JBD_COM"] == DBNull.Value ? string.Empty : row["JBD_COM"].ToString(),
                Jbd_ser_id = row["JBD_SER_ID"] == DBNull.Value ? string.Empty : row["JBD_SER_ID"].ToString(),
                Jbd_techst_dt_man = row["JBD_TECHST_DT_MAN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHST_DT_MAN"].ToString()),
                Jbd_techfin_dt_man = row["JBD_TECHFIN_DT_MAN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHFIN_DT_MAN"].ToString()),
                Jbd_reqwcn = row["JBD_REQWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_REQWCN"].ToString()),
                Jbd_reqwcndt = row["JBD_REQWCNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_REQWCNDT"].ToString()),
                Jbd_reqwcnsysdt = row["JBD_REQWCNSYSDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_REQWCNSYSDT"].ToString()),
                Jbd_sentwcn = row["JBD_SENTWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SENTWCN"].ToString()),
                Jbd_recwcn = row["JBD_RECWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_RECWCN"].ToString()),
                Jbd_takewcn = row["JBD_TAKEWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_TAKEWCN"].ToString()),
                Jbd_takewcndt = row["JBD_TAKEWCNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TAKEWCNDT"].ToString()),
                Jbd_takewcnsysdt = row["JBD_TAKEWCNSYSDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TAKEWCNSYSDT"].ToString()),
                Jbd_supp_cd = row["JBD_SUPP_CD"] == DBNull.Value ? string.Empty : row["JBD_SUPP_CD"].ToString(),
                Jbd_part_cd = row["JBD_PART_CD"] == DBNull.Value ? string.Empty : row["JBD_PART_CD"].ToString(),
                Jbd_oem_no = row["JBD_OEM_NO"] == DBNull.Value ? string.Empty : row["JBD_OEM_NO"].ToString(),
                Jbd_case_id = row["JBD_CASE_ID"] == DBNull.Value ? string.Empty : row["JBD_CASE_ID"].ToString(),
                Jbd_act = row["JBD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ACT"].ToString()),
                Jbd_oldjobline = row["JBD_OLDJOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_OLDJOBLINE"].ToString()),
                Jbd_tech_rmk = row["JBD_TECH_RMK"] == DBNull.Value ? string.Empty : row["JBD_TECH_RMK"].ToString(),
                Jbd_tech_custrmk = row["JBD_TECH_CUSTRMK"] == DBNull.Value ? string.Empty : row["JBD_TECH_CUSTRMK"].ToString(),
                StageText = row["JBS_DESC"] == DBNull.Value ? string.Empty : row["JBS_DESC"].ToString(),
                Jbd_tech_cls_tp = row["JBD_TECH_CLS_TP"] == DBNull.Value ? string.Empty : row["JBD_TECH_CLS_TP"].ToString(),
                Jbd_isfocapp = row["JBD_ISFOCAPP"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISFOCAPP"].ToString()),

                Jbd_swarr_stus = row["JBD_SWARR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SWARR_STUS"].ToString()),
                Jbd_swarrperiod = row["JBD_SWARRPERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SWARRPERIOD"].ToString()),
                Jbd_swarrrmk = row["JBD_SWARRRMK"] == DBNull.Value ? string.Empty : row["JBD_SWARRRMK"].ToString(),
                Jbd_swarrstartdt = row["JBD_SWARRSTARTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_SWARRSTARTDT"].ToString()),
                Jbd_tech_cls_rmk = row["Jbd_tech_cls_rmk"] == DBNull.Value ? string.Empty : row["Jbd_tech_cls_rmk"].ToString(),
                Jbd_is_fgap = row["JBD_IS_FGAP"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_IS_FGAP"].ToString()),
                Jbd_is_closed = row["JBD_IS_CLOSED"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_IS_CLOSED"].ToString()),
                Jbd_rep_perc = row["Jbd_rep_perc"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Jbd_rep_perc"].ToString()),
                jbd_sw_stus = row["jbd_sw_stus"] == DBNull.Value ? 0 : Convert.ToInt32(row["jbd_sw_stus"].ToString()), 
                jbd_pb = row["jbd_pb"] == DBNull.Value ? string.Empty : row["jbd_pb"].ToString(),
                jbd_pblvl = row["jbd_pblvl"] == DBNull.Value ? string.Empty : row["jbd_pblvl"].ToString(),
                jbd_del_sale_dt = row["jbd_del_sale_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["jbd_del_sale_dt"].ToString()),
                sjb_creby = row["jbd_pblvl"] == DBNull.Value ? string.Empty : row["jbd_pblvl"].ToString()
            };
        }      
               
              

        public static Service_job_Det ConverterClaimItems(DataRow row)
        {
            return new Service_job_Det
            {
                Jbd_seq_no = row["JBD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SEQ_NO"].ToString()),
                Jbd_jobno = row["JBD_JOBNO"] == DBNull.Value ? string.Empty : row["JBD_JOBNO"].ToString(),
                Jbd_itm_cd = row["JBD_ITM_CD"] == DBNull.Value ? string.Empty : row["JBD_ITM_CD"].ToString(),
                Jbd_itm_desc = row["JBD_ITM_DESC"] == DBNull.Value ? string.Empty : row["JBD_ITM_DESC"].ToString(),
                Jbd_ser1 = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),
                Jbd_reqwcn = row["JBD_REQWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_REQWCN"].ToString()),
                Jbd_sentwcn = row["JBD_SENTWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SENTWCN"].ToString()),
                Jbd_recwcn = row["JBD_RECWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_RECWCN"].ToString()),
                Jbd_takewcn = row["JBD_TAKEWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_TAKEWCN"].ToString()),
                Jbd_supp_cd = row["JBD_SUPP_CD"] == DBNull.Value ? string.Empty : row["JBD_SUPP_CD"].ToString(),
                Jbd_part_cd = row["JBD_PART_CD"] == DBNull.Value ? string.Empty : row["JBD_PART_CD"].ToString(),
                Jbd_oem_no = row["JBD_OEM_NO"] == DBNull.Value ? string.Empty : row["JBD_OEM_NO"].ToString(),
                Jbd_case_id = row["JBD_CASE_ID"] == DBNull.Value ? string.Empty : row["JBD_CASE_ID"].ToString(),
                Sjb_dt = row["SJB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_DT"].ToString()),
                Jbd_itm_stus = row["JBD_ITM_STUS"] == DBNull.Value ? string.Empty : row["JBD_ITM_STUS"].ToString(),
                Jbd_warr = row["JBD_WARR"] == DBNull.Value ? string.Empty : row["JBD_WARR"].ToString(),
                Jbd_jobline = row["JBD_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_JOBLINE"].ToString()),
                Jbd_ser_id = row["JBD_SER_ID"] == DBNull.Value ? string.Empty : row["JBD_SER_ID"].ToString(),
                Jbd_serold = row["Jbd_serold"] == DBNull.Value ? string.Empty : row["Jbd_serold"].ToString(),
                Isold_part = row["Isold_part"] == DBNull.Value ? 0 : Convert.ToInt32(row["Isold_part"].ToString()),
                jbd_isstockupdate = row["jbd_isstockupdate"] == DBNull.Value ? 0 : Convert.ToInt32(row["jbd_isstockupdate"].ToString()),
                Jbd_brand = row["jbd_brand"] == DBNull.Value ? string.Empty : row["jbd_brand"].ToString(),
                Jbd_reqwcndt = row["JBD_REQWCNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_REQWCNDT"].ToString())
            };
        }

        public static Service_job_Det ConverterClaimDetails(DataRow row)
        {
            return new Service_job_Det
            {

                Jbd_jobno = row["JBD_JOBNO"] == DBNull.Value ? string.Empty : row["JBD_JOBNO"].ToString(),
                Jbd_itm_cd = row["JBD_ITM_CD"] == DBNull.Value ? string.Empty : row["JBD_ITM_CD"].ToString(),
                Jbd_itm_desc = row["JBD_ITM_DESC"] == DBNull.Value ? string.Empty : row["JBD_ITM_DESC"].ToString(),
                Jbd_ser1 = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),
                Jbd_part_cd = row["JBD_PART_CD"] == DBNull.Value ? string.Empty : row["JBD_PART_CD"].ToString(),

                Jbd_oem_no = row["JBD_OEM_NO"] == DBNull.Value ? string.Empty : row["JBD_OEM_NO"].ToString(),
                Jbd_case_id = row["JBD_CASE_ID"] == DBNull.Value ? string.Empty : row["JBD_CASE_ID"].ToString(),
                Sjb_dt = row["SJB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_DT"].ToString()),
                Jbd_itm_stus = row["JBD_ITM_STUS"] == DBNull.Value ? string.Empty : row["JBD_ITM_STUS"].ToString(),
                Jbd_warr = row["JBD_WARR"] == DBNull.Value ? string.Empty : row["JBD_WARR"].ToString(),
                Jbd_jobline = row["JBD_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_JOBLINE"].ToString()),
                Jbd_ser_id = row["JBD_SER_ID"] == DBNull.Value ? string.Empty : row["JBD_SER_ID"].ToString(),
                Jbd_serold = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),
                Isold_part = row["Isold_part"] == DBNull.Value ? 0 : Convert.ToInt32(row["Isold_part"].ToString()),
                jbd_isstockupdate = row["jbd_isstockupdate"] == DBNull.Value ? 0 : Convert.ToInt32(row["jbd_isstockupdate"].ToString()),
                Jbd_supp_cd = row["JBD_SUPP_CD"] == DBNull.Value ? string.Empty : row["JBD_SUPP_CD"].ToString(),//296-06-2015 Nadeeka
                Jbd_brand = row["jbd_brand"] == DBNull.Value ? string.Empty : row["jbd_brand"].ToString(),
                Jbd_reqwcndt = row["JBD_REQWCNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_REQWCNDT"].ToString()),
                Jbd_need_chk = row["Jbd_need_chk"] == DBNull.Value ? 0 : Convert.ToInt32(row["Jbd_need_chk"].ToString()),
                Swd_Line = row["swd_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["swd_line"].ToString()) //Add by akila 2018/02/21
            };
        }

        public static Service_job_Det ConverterPureAll(DataRow row)
        {
            return new Service_job_Det
            {
                Jbd_seq_no = row["JBD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SEQ_NO"].ToString()),
                Jbd_jobno = row["JBD_JOBNO"] == DBNull.Value ? string.Empty : row["JBD_JOBNO"].ToString(),
                Jbd_jobline = row["JBD_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_JOBLINE"].ToString()),
                Jbd_sjobno = row["JBD_SJOBNO"] == DBNull.Value ? string.Empty : row["JBD_SJOBNO"].ToString(),
                Jbd_loc = row["JBD_LOC"] == DBNull.Value ? string.Empty : row["JBD_LOC"].ToString(),
                Jbd_pc = row["JBD_PC"] == DBNull.Value ? string.Empty : row["JBD_PC"].ToString(),
                Jbd_itm_cd = row["JBD_ITM_CD"] == DBNull.Value ? string.Empty : row["JBD_ITM_CD"].ToString(),
                Jbd_itm_stus = row["JBD_ITM_STUS"] == DBNull.Value ? string.Empty : row["JBD_ITM_STUS"].ToString(),
                Jbd_itm_desc = row["JBD_ITM_DESC"] == DBNull.Value ? string.Empty : row["JBD_ITM_DESC"].ToString(),
                Jbd_brand = row["JBD_BRAND"] == DBNull.Value ? string.Empty : row["JBD_BRAND"].ToString(),
                Jbd_model = row["JBD_MODEL"] == DBNull.Value ? string.Empty : row["JBD_MODEL"].ToString(),
                Jbd_itm_cost = row["JBD_ITM_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_ITM_COST"].ToString()),
                Jbd_ser1 = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),
                Jbd_ser2 = row["JBD_SER2"] == DBNull.Value ? string.Empty : row["JBD_SER2"].ToString(),
                Jbd_warr = row["JBD_WARR"] == DBNull.Value ? string.Empty : row["JBD_WARR"].ToString(),
                Jbd_regno = row["JBD_REGNO"] == DBNull.Value ? string.Empty : row["JBD_REGNO"].ToString(),
                Jbd_milage = row["JBD_MILAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_MILAGE"].ToString()),
                Jbd_warr_stus = row["JBD_WARR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARR_STUS"].ToString()),
                Jbd_onloan = row["JBD_ONLOAN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ONLOAN"].ToString()),
                Jbd_chg_warr_stdt = row["JBD_CHG_WARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_CHG_WARR_STDT"].ToString()),
                Jbd_chg_warr_rmk = row["JBD_CHG_WARR_RMK"] == DBNull.Value ? string.Empty : row["JBD_CHG_WARR_RMK"].ToString(),
                Jbd_isinsurance = row["JBD_ISINSURANCE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISINSURANCE"].ToString()),
                Jbd_cate1 = row["JBD_CATE1"] == DBNull.Value ? string.Empty : row["JBD_CATE1"].ToString(),
                Jbd_ser_term = row["JBD_SER_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SER_TERM"].ToString()),
                Jbd_lastwarr_stdt = row["JBD_LASTWARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_LASTWARR_STDT"].ToString()),
                Jbd_issued = row["JBD_ISSUED"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISSUED"].ToString()),
                Jbd_mainitmcd = row["JBD_MAINITMCD"] == DBNull.Value ? string.Empty : row["JBD_MAINITMCD"].ToString(),
                Jbd_mainitmser = row["JBD_MAINITMSER"] == DBNull.Value ? string.Empty : row["JBD_MAINITMSER"].ToString(),
                Jbd_mainitmwarr = row["JBD_MAINITMWARR"] == DBNull.Value ? string.Empty : row["JBD_MAINITMWARR"].ToString(),
                Jbd_itmmfc = row["JBD_ITMMFC"] == DBNull.Value ? string.Empty : row["JBD_ITMMFC"].ToString(),
                Jbd_mainitmmfc = row["JBD_MAINITMMFC"] == DBNull.Value ? string.Empty : row["JBD_MAINITMMFC"].ToString(),
                Jbd_availabilty = row["JBD_AVAILABILTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_AVAILABILTY"].ToString()),
                Jbd_usejob = row["JBD_USEJOB"] == DBNull.Value ? string.Empty : row["JBD_USEJOB"].ToString(),
                Jbd_msnno = row["JBD_MSNNO"] == DBNull.Value ? string.Empty : row["JBD_MSNNO"].ToString(),
                Jbd_itmtp = row["JBD_ITMTP"] == DBNull.Value ? string.Empty : row["JBD_ITMTP"].ToString(),
                Jbd_serlocchr = row["JBD_SERLOCCHR"] == DBNull.Value ? string.Empty : row["JBD_SERLOCCHR"].ToString(),
                Jbd_custnotes = row["JBD_CUSTNOTES"] == DBNull.Value ? string.Empty : row["JBD_CUSTNOTES"].ToString(),
                Jbd_mainreqno = row["JBD_MAINREQNO"] == DBNull.Value ? string.Empty : row["JBD_MAINREQNO"].ToString(),
                Jbd_mainreqloc = row["JBD_MAINREQLOC"] == DBNull.Value ? string.Empty : row["JBD_MAINREQLOC"].ToString(),
                Jbd_mainjobno = row["JBD_MAINJOBNO"] == DBNull.Value ? string.Empty : row["JBD_MAINJOBNO"].ToString(),
                Jbd_reqitmtp = row["JBD_REQITMTP"] == DBNull.Value ? string.Empty : row["JBD_REQITMTP"].ToString(),
                Jbd_reqno = row["JBD_REQNO"] == DBNull.Value ? string.Empty : row["JBD_REQNO"].ToString(),
                Jbd_reqline = row["JBD_REQLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_REQLINE"].ToString()),
                Jbd_isstockupdate = row["JBD_ISSTOCKUPDATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISSTOCKUPDATE"].ToString()),
                Jbd_isgatepass = row["JBD_ISGATEPASS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISGATEPASS"].ToString()),
                Jbd_iswrn = row["JBD_ISWRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISWRN"].ToString()),
                Jbd_warrperiod = row["JBD_WARRPERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARRPERIOD"].ToString()),
                Jbd_warrrmk = row["JBD_WARRRMK"] == DBNull.Value ? string.Empty : row["JBD_WARRRMK"].ToString(),
                Jbd_warrstartdt = row["JBD_WARRSTARTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARRSTARTDT"].ToString()),
                Jbd_warrreplace = row["JBD_WARRREPLACE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARRREPLACE"].ToString()),
                Jbd_date_pur = row["JBD_DATE_PUR"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_DATE_PUR"].ToString()),
                Jbd_invc_no = row["JBD_INVC_NO"] == DBNull.Value ? string.Empty : row["JBD_INVC_NO"].ToString(),
                Jbd_waraamd_seq = row["JBD_WARAAMD_SEQ"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_SEQ"].ToString(),
                Jbd_waraamd_by = row["JBD_WARAAMD_BY"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_BY"].ToString(),
                Jbd_waraamd_dt = row["JBD_WARAAMD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARAAMD_DT"].ToString()),
                Jbd_invc_showroom = row["JBD_INVC_SHOWROOM"] == DBNull.Value ? string.Empty : row["JBD_INVC_SHOWROOM"].ToString(),
                Jbd_aodissueloc = row["JBD_AODISSUELOC"] == DBNull.Value ? string.Empty : row["JBD_AODISSUELOC"].ToString(),
                Jbd_aodissuedt = row["JBD_AODISSUEDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_AODISSUEDT"].ToString()),
                Jbd_aodissueno = row["JBD_AODISSUENO"] == DBNull.Value ? string.Empty : row["JBD_AODISSUENO"].ToString(),
                Jbd_aodrecno = row["JBD_AODRECNO"] == DBNull.Value ? string.Empty : row["JBD_AODRECNO"].ToString(),
                Jbd_techst_dt = row["JBD_TECHST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHST_DT"].ToString()),
                Jbd_techfin_dt = row["JBD_TECHFIN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHFIN_DT"].ToString()),
                Jbd_msn_no = row["JBD_MSN_NO"] == DBNull.Value ? string.Empty : row["JBD_MSN_NO"].ToString(),
                Jbd_isexternalitm = row["JBD_ISEXTERNALITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISEXTERNALITM"].ToString()),
                Jbd_conf_dt = row["JBD_CONF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_CONF_DT"].ToString()),
                Jbd_conf_cd = row["JBD_CONF_CD"] == DBNull.Value ? string.Empty : row["JBD_CONF_CD"].ToString(),
                Jbd_conf_desc = row["JBD_CONF_DESC"] == DBNull.Value ? string.Empty : row["JBD_CONF_DESC"].ToString(),
                Jbd_conf_rmk = row["JBD_CONF_RMK"] == DBNull.Value ? string.Empty : row["JBD_CONF_RMK"].ToString(),
                Jbd_tranf_by = row["JBD_TRANF_BY"] == DBNull.Value ? string.Empty : row["JBD_TRANF_BY"].ToString(),
                Jbd_tranf_dt = row["JBD_TRANF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TRANF_DT"].ToString()),
                Jbd_do_invoice = row["JBD_DO_INVOICE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_DO_INVOICE"].ToString()),
                Jbd_insu_com = row["JBD_INSU_COM"] == DBNull.Value ? string.Empty : row["JBD_INSU_COM"].ToString(),
                Jbd_agreeno = row["JBD_AGREENO"] == DBNull.Value ? string.Empty : row["JBD_AGREENO"].ToString(),
                Jbd_issrn = row["JBD_ISSRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISSRN"].ToString()),
                Jbd_isagreement = row["JBD_ISAGREEMENT"] == DBNull.Value ? string.Empty : row["JBD_ISAGREEMENT"].ToString(),
                Jbd_cust_agreeno = row["JBD_CUST_AGREENO"] == DBNull.Value ? string.Empty : row["JBD_CUST_AGREENO"].ToString(),
                Jbd_quo_no = row["JBD_QUO_NO"] == DBNull.Value ? string.Empty : row["JBD_QUO_NO"].ToString(),
                Jbd_stage = row["JBD_STAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_STAGE"].ToString()),
                Jbd_com = row["JBD_COM"] == DBNull.Value ? string.Empty : row["JBD_COM"].ToString(),
                Jbd_ser_id = row["JBD_SER_ID"] == DBNull.Value ? string.Empty : row["JBD_SER_ID"].ToString(),
                Jbd_techst_dt_man = row["JBD_TECHST_DT_MAN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHST_DT_MAN"].ToString()),
                Jbd_techfin_dt_man = row["JBD_TECHFIN_DT_MAN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHFIN_DT_MAN"].ToString()),
                Jbd_reqwcn = row["JBD_REQWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_REQWCN"].ToString()),
                Jbd_reqwcndt = row["JBD_REQWCNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_REQWCNDT"].ToString()),
                Jbd_reqwcnsysdt = row["JBD_REQWCNSYSDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_REQWCNSYSDT"].ToString()),
                Jbd_sentwcn = row["JBD_SENTWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SENTWCN"].ToString()),
                Jbd_recwcn = row["JBD_RECWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_RECWCN"].ToString()),
                Jbd_takewcn = row["JBD_TAKEWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_TAKEWCN"].ToString()),
                Jbd_takewcndt = row["JBD_TAKEWCNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TAKEWCNDT"].ToString()),
                Jbd_takewcnsysdt = row["JBD_TAKEWCNSYSDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TAKEWCNSYSDT"].ToString()),
                Jbd_supp_cd = row["JBD_SUPP_CD"] == DBNull.Value ? string.Empty : row["JBD_SUPP_CD"].ToString(),
                Jbd_part_cd = row["JBD_PART_CD"] == DBNull.Value ? string.Empty : row["JBD_PART_CD"].ToString(),
                Jbd_oem_no = row["JBD_OEM_NO"] == DBNull.Value ? string.Empty : row["JBD_OEM_NO"].ToString(),
                Jbd_case_id = row["JBD_CASE_ID"] == DBNull.Value ? string.Empty : row["JBD_CASE_ID"].ToString(),
                Jbd_act = row["JBD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ACT"].ToString()),
                Jbd_oldjobline = row["JBD_OLDJOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_OLDJOBLINE"].ToString()),
                Jbd_tech_rmk = row["JBD_TECH_RMK"] == DBNull.Value ? string.Empty : row["JBD_TECH_RMK"].ToString(),
                Jbd_tech_custrmk = row["JBD_TECH_CUSTRMK"] == DBNull.Value ? string.Empty : row["JBD_TECH_CUSTRMK"].ToString(),
                Jbd_tech_cls_tp = row["JBD_TECH_CLS_TP"] == DBNull.Value ? string.Empty : row["JBD_TECH_CLS_TP"].ToString(),
                Jbd_isfocapp = row["JBD_ISFOCAPP"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISFOCAPP"].ToString()),
                Jbd_swarr_stus = row["JBD_SWARR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SWARR_STUS"].ToString()),
                Jbd_swarrperiod = row["JBD_SWARRPERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SWARRPERIOD"].ToString()),
                Jbd_swarrrmk = row["JBD_SWARRRMK"] == DBNull.Value ? string.Empty : row["JBD_SWARRRMK"].ToString(),
                Jbd_swarrstartdt = row["JBD_SWARRSTARTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_SWARRSTARTDT"].ToString()),
                Jbd_tech_cls_rmk = row["Jbd_tech_cls_rmk"] == DBNull.Value ? string.Empty : row["Jbd_tech_cls_rmk"].ToString()
            };
        }

        public static Service_job_Det ConverterDet(DataRow row)
        { // Nadeeka 09-05-2015
            return new Service_job_Det
            {
                Jbd_seq_no = row["JBD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SEQ_NO"].ToString()),
                Jbd_jobno = row["JBD_JOBNO"] == DBNull.Value ? string.Empty : row["JBD_JOBNO"].ToString(),
                Jbd_jobline = row["JBD_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_JOBLINE"].ToString()),
                Jbd_sjobno = row["JBD_SJOBNO"] == DBNull.Value ? string.Empty : row["JBD_SJOBNO"].ToString(),
                Jbd_loc = row["JBD_LOC"] == DBNull.Value ? string.Empty : row["JBD_LOC"].ToString(),
                Jbd_pc = row["JBD_PC"] == DBNull.Value ? string.Empty : row["JBD_PC"].ToString(),
                Jbd_itm_cd = row["JBD_ITM_CD"] == DBNull.Value ? string.Empty : row["JBD_ITM_CD"].ToString(),
                Jbd_itm_stus = row["JBD_ITM_STUS"] == DBNull.Value ? string.Empty : row["JBD_ITM_STUS"].ToString(),
                Jbd_itm_desc = row["JBD_ITM_DESC"] == DBNull.Value ? string.Empty : row["JBD_ITM_DESC"].ToString(),
                Jbd_brand = row["JBD_BRAND"] == DBNull.Value ? string.Empty : row["JBD_BRAND"].ToString(),
                Jbd_model = row["JBD_MODEL"] == DBNull.Value ? string.Empty : row["JBD_MODEL"].ToString(),
                Jbd_itm_cost = row["JBD_ITM_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_ITM_COST"].ToString()),
                Jbd_ser1 = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),
                Jbd_ser2 = row["JBD_SER2"] == DBNull.Value ? string.Empty : row["JBD_SER2"].ToString(),
                Jbd_warr = row["JBD_WARR"] == DBNull.Value ? string.Empty : row["JBD_WARR"].ToString(),
                Jbd_regno = row["JBD_REGNO"] == DBNull.Value ? string.Empty : row["JBD_REGNO"].ToString(),
                Jbd_milage = row["JBD_MILAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_MILAGE"].ToString()),
                Jbd_warr_stus = row["JBD_WARR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARR_STUS"].ToString()),
                Jbd_onloan = row["JBD_ONLOAN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ONLOAN"].ToString()),
                Jbd_chg_warr_stdt = row["JBD_CHG_WARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_CHG_WARR_STDT"].ToString()),
                Jbd_chg_warr_rmk = row["JBD_CHG_WARR_RMK"] == DBNull.Value ? string.Empty : row["JBD_CHG_WARR_RMK"].ToString(),
                Jbd_isinsurance = row["JBD_ISINSURANCE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISINSURANCE"].ToString()),
                Jbd_cate1 = row["JBD_CATE1"] == DBNull.Value ? string.Empty : row["JBD_CATE1"].ToString(),
                Jbd_ser_term = row["JBD_SER_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SER_TERM"].ToString()),
                Jbd_lastwarr_stdt = row["JBD_LASTWARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_LASTWARR_STDT"].ToString()),
                Jbd_issued = row["JBD_ISSUED"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISSUED"].ToString()),
                Jbd_mainitmcd = row["JBD_MAINITMCD"] == DBNull.Value ? string.Empty : row["JBD_MAINITMCD"].ToString(),
                Jbd_mainitmser = row["JBD_MAINITMSER"] == DBNull.Value ? string.Empty : row["JBD_MAINITMSER"].ToString(),
                Jbd_mainitmwarr = row["JBD_MAINITMWARR"] == DBNull.Value ? string.Empty : row["JBD_MAINITMWARR"].ToString(),
                Jbd_itmmfc = row["JBD_ITMMFC"] == DBNull.Value ? string.Empty : row["JBD_ITMMFC"].ToString(),
                Jbd_mainitmmfc = row["JBD_MAINITMMFC"] == DBNull.Value ? string.Empty : row["JBD_MAINITMMFC"].ToString(),
                Jbd_availabilty = row["JBD_AVAILABILTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_AVAILABILTY"].ToString()),
                Jbd_usejob = row["JBD_USEJOB"] == DBNull.Value ? string.Empty : row["JBD_USEJOB"].ToString(),
                Jbd_msnno = row["JBD_MSNNO"] == DBNull.Value ? string.Empty : row["JBD_MSNNO"].ToString(),
                Jbd_itmtp = row["JBD_ITMTP"] == DBNull.Value ? string.Empty : row["JBD_ITMTP"].ToString(),
                Jbd_serlocchr = row["JBD_SERLOCCHR"] == DBNull.Value ? string.Empty : row["JBD_SERLOCCHR"].ToString(),
                Jbd_custnotes = row["JBD_CUSTNOTES"] == DBNull.Value ? string.Empty : row["JBD_CUSTNOTES"].ToString(),
                Jbd_mainreqno = row["JBD_MAINREQNO"] == DBNull.Value ? string.Empty : row["JBD_MAINREQNO"].ToString(),
                Jbd_mainreqloc = row["JBD_MAINREQLOC"] == DBNull.Value ? string.Empty : row["JBD_MAINREQLOC"].ToString(),
                Jbd_mainjobno = row["JBD_MAINJOBNO"] == DBNull.Value ? string.Empty : row["JBD_MAINJOBNO"].ToString(),
                Jbd_reqitmtp = row["JBD_REQITMTP"] == DBNull.Value ? string.Empty : row["JBD_REQITMTP"].ToString(),
                Jbd_reqno = row["JBD_REQNO"] == DBNull.Value ? string.Empty : row["JBD_REQNO"].ToString(),
                Jbd_reqline = row["JBD_REQLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_REQLINE"].ToString()),
                Jbd_isstockupdate = row["JBD_ISSTOCKUPDATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISSTOCKUPDATE"].ToString()),
                Jbd_isgatepass = row["JBD_ISGATEPASS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISGATEPASS"].ToString()),
                Jbd_iswrn = row["JBD_ISWRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISWRN"].ToString()),
                Jbd_warrperiod = row["JBD_WARRPERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARRPERIOD"].ToString()),
                Jbd_warrrmk = row["JBD_WARRRMK"] == DBNull.Value ? string.Empty : row["JBD_WARRRMK"].ToString(),
                Jbd_warrstartdt = row["JBD_WARRSTARTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARRSTARTDT"].ToString()),
                Jbd_warrreplace = row["JBD_WARRREPLACE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARRREPLACE"].ToString()),
                Jbd_date_pur = row["JBD_DATE_PUR"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_DATE_PUR"].ToString()),
                Jbd_invc_no = row["JBD_INVC_NO"] == DBNull.Value ? string.Empty : row["JBD_INVC_NO"].ToString(),
                Jbd_waraamd_seq = row["JBD_WARAAMD_SEQ"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_SEQ"].ToString(),
                Jbd_waraamd_by = row["JBD_WARAAMD_BY"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_BY"].ToString(),
                Jbd_waraamd_dt = row["JBD_WARAAMD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARAAMD_DT"].ToString()),
                Jbd_invc_showroom = row["JBD_INVC_SHOWROOM"] == DBNull.Value ? string.Empty : row["JBD_INVC_SHOWROOM"].ToString(),
                Jbd_aodissueloc = row["JBD_AODISSUELOC"] == DBNull.Value ? string.Empty : row["JBD_AODISSUELOC"].ToString(),
                Jbd_aodissuedt = row["JBD_AODISSUEDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_AODISSUEDT"].ToString()),
                Jbd_aodissueno = row["JBD_AODISSUENO"] == DBNull.Value ? string.Empty : row["JBD_AODISSUENO"].ToString(),
                Jbd_aodrecno = row["JBD_AODRECNO"] == DBNull.Value ? string.Empty : row["JBD_AODRECNO"].ToString(),
                Jbd_techst_dt = row["JBD_TECHST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHST_DT"].ToString()),
                Jbd_techfin_dt = row["JBD_TECHFIN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHFIN_DT"].ToString()),
                Jbd_msn_no = row["JBD_MSN_NO"] == DBNull.Value ? string.Empty : row["JBD_MSN_NO"].ToString(),
                Jbd_isexternalitm = row["JBD_ISEXTERNALITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISEXTERNALITM"].ToString()),
                Jbd_conf_dt = row["JBD_CONF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_CONF_DT"].ToString()),
                Jbd_conf_cd = row["JBD_CONF_CD"] == DBNull.Value ? string.Empty : row["JBD_CONF_CD"].ToString(),
                Jbd_conf_desc = row["JBD_CONF_DESC"] == DBNull.Value ? string.Empty : row["JBD_CONF_DESC"].ToString(),
                Jbd_conf_rmk = row["JBD_CONF_RMK"] == DBNull.Value ? string.Empty : row["JBD_CONF_RMK"].ToString(),
                Jbd_tranf_by = row["JBD_TRANF_BY"] == DBNull.Value ? string.Empty : row["JBD_TRANF_BY"].ToString(),
                Jbd_tranf_dt = row["JBD_TRANF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TRANF_DT"].ToString()),
                Jbd_do_invoice = row["JBD_DO_INVOICE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_DO_INVOICE"].ToString()),
                Jbd_insu_com = row["JBD_INSU_COM"] == DBNull.Value ? string.Empty : row["JBD_INSU_COM"].ToString(),
                Jbd_agreeno = row["JBD_AGREENO"] == DBNull.Value ? string.Empty : row["JBD_AGREENO"].ToString(),
                Jbd_issrn = row["JBD_ISSRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISSRN"].ToString()),
                Jbd_isagreement = row["JBD_ISAGREEMENT"] == DBNull.Value ? string.Empty : row["JBD_ISAGREEMENT"].ToString(),
                Jbd_cust_agreeno = row["JBD_CUST_AGREENO"] == DBNull.Value ? string.Empty : row["JBD_CUST_AGREENO"].ToString(),
                Jbd_quo_no = row["JBD_QUO_NO"] == DBNull.Value ? string.Empty : row["JBD_QUO_NO"].ToString(),
                Jbd_stage = row["JBD_STAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_STAGE"].ToString()),
                Jbd_com = row["JBD_COM"] == DBNull.Value ? string.Empty : row["JBD_COM"].ToString(),
                Jbd_ser_id = row["JBD_SER_ID"] == DBNull.Value ? string.Empty : row["JBD_SER_ID"].ToString(),
                Jbd_techst_dt_man = row["JBD_TECHST_DT_MAN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHST_DT_MAN"].ToString()),
                Jbd_techfin_dt_man = row["JBD_TECHFIN_DT_MAN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TECHFIN_DT_MAN"].ToString()),
                Jbd_reqwcn = row["JBD_REQWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_REQWCN"].ToString()),
                Jbd_reqwcndt = row["JBD_REQWCNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_REQWCNDT"].ToString()),
                Jbd_reqwcnsysdt = row["JBD_REQWCNSYSDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_REQWCNSYSDT"].ToString()),
                Jbd_sentwcn = row["JBD_SENTWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SENTWCN"].ToString()),
                Jbd_recwcn = row["JBD_RECWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_RECWCN"].ToString()),
                Jbd_takewcn = row["JBD_TAKEWCN"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_TAKEWCN"].ToString()),
                Jbd_takewcndt = row["JBD_TAKEWCNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TAKEWCNDT"].ToString()),
                Jbd_takewcnsysdt = row["JBD_TAKEWCNSYSDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_TAKEWCNSYSDT"].ToString()),
                Jbd_supp_cd = row["JBD_SUPP_CD"] == DBNull.Value ? string.Empty : row["JBD_SUPP_CD"].ToString(),
                Jbd_part_cd = row["JBD_PART_CD"] == DBNull.Value ? string.Empty : row["JBD_PART_CD"].ToString(),
                Jbd_oem_no = row["JBD_OEM_NO"] == DBNull.Value ? string.Empty : row["JBD_OEM_NO"].ToString(),
                Jbd_case_id = row["JBD_CASE_ID"] == DBNull.Value ? string.Empty : row["JBD_CASE_ID"].ToString(),
                Jbd_act = row["JBD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ACT"].ToString()),
                Jbd_oldjobline = row["JBD_OLDJOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_OLDJOBLINE"].ToString()),
                Jbd_tech_rmk = row["JBD_TECH_RMK"] == DBNull.Value ? string.Empty : row["JBD_TECH_RMK"].ToString(),
                Jbd_tech_custrmk = row["JBD_TECH_CUSTRMK"] == DBNull.Value ? string.Empty : row["JBD_TECH_CUSTRMK"].ToString(),
                StageText = row["JBS_DESC"] == DBNull.Value ? string.Empty : row["JBS_DESC"].ToString(),
                Jbd_tech_cls_tp = row["JBD_TECH_CLS_TP"] == DBNull.Value ? string.Empty : row["JBD_TECH_CLS_TP"].ToString(),
                Jbd_isfocapp = row["JBD_ISFOCAPP"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_ISFOCAPP"].ToString()),

                Jbd_swarr_stus = row["JBD_SWARR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SWARR_STUS"].ToString()),
                Jbd_swarrperiod = row["JBD_SWARRPERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SWARRPERIOD"].ToString()),
                Jbd_swarrrmk = row["JBD_SWARRRMK"] == DBNull.Value ? string.Empty : row["JBD_SWARRRMK"].ToString(),
                Jbd_swarrstartdt = row["JBD_SWARRSTARTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_SWARRSTARTDT"].ToString()),
                Jbd_tech_cls_rmk = row["Jbd_tech_cls_rmk"] == DBNull.Value ? string.Empty : row["Jbd_tech_cls_rmk"].ToString(),
                JBS_DESC = row["JBS_DESC"] == DBNull.Value ? string.Empty : row["JBS_DESC"].ToString()
            };
        }

        #endregion Converters
    }

    public class Service_free_det
    {
        public string Description { get; set; }
        public DateTime Servicedates { get; set; }
        public int Noofservices { get; set; }
    }


}
