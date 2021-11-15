using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   [Serializable]
   public class ServiceJobHeader
    {
        /// <summary>
        /// Written By Shani on 21/08/2012
        /// Table: sev_job_hdr
        /// </summary>
        #region Private Members
        private string _sjb_address1;
        private string _sjb_address2;
        private string _sjb_agreementno;
        private DateTime _sjb_aodissuedt;
        private string _sjb_aodissueloc;
        private string _sjb_aodissueno;
        private string _sjb_aodrecno;
        private string _sjb_bill_address1;
        private string _sjb_bill_address2;
        private string _sjb_bill_address3;
        private string _sjb_bill_cust_cd;
        private string _sjb_bill_cust_name;
        private string _sjb_bill_email;
        private string _sjb_bill_fax;
        private string _sjb_bill_id;
        private string _sjb_bill_idtype;
        private string _sjb_bill_mobino;
        private string _sjb_bill_phone_no;
        private string _sjb_bill_title;
        private string _sjb_bill_town;
        private string _sjb_chnl;
        private string _sjb_com;
        private string _sjb_cont_address1;
        private string _sjb_cont_address2;
        private string _sjb_cont_person;
        private string _sjb_cont_phone_no;
        private string _sjb_cre_by;
        private DateTime _sjb_cre_dt;
        private string _sjb_cusagreementno;
        private string _sjb_cust_cd;
        private DateTime _sjb_cust_expectdt;
        private string _sjb_cust_name;
        private DateTime _sjb_dt;
        private string _sjb_email;
        private DateTime _sjb_end_dt;
        private string _sjb_faxno;
        private string _sjb_idno;
        private string _sjb_id_tp;
        private string _sjb_inform_contact;
        private string _sjb_inform_location;
        private string _sjb_inform_person;
        private string _sjb_insu_com;
        private string _sjb_isagreement;
        private Boolean _sjb_isexternalitem;
        private Boolean _sjb_isprejob;
        private Boolean _sjb_issrn;
        private string _sjb_itm_tp;
        private string _sjb_jobclosecd;
        private string _sjb_jobclosedesc;
        private string _sjb_jobclosermk;
        private DateTime _sjb_jobconfdate;
        private string _sjb_jobno;
        private decimal _sjb_jobstage;
        private string _sjb_job_cat;
        private string _sjb_job_rmk;
        private string _sjb_job_sub_tp;
        private string _sjb_job_tp;
        private string _sjb_lastprintby;
        private string _sjb_loc;
        private string _sjb_mainjobno;
        private string _sjb_mainreqloc;
        private string _sjb_mainreqno;
        private string _sjb_manualref;
        private string _sjb_mobino;
        private string _sjb_mod_by;
        private DateTime _sjb_mod_dt;
        private string _sjb_msn_no;
        private int _sjb_noofprint;
        private string _sjb_orderno;
        private string _sjb_othref;
        private string _sjb_pc;
        private string _sjb_phoneno;
        private string _sjb_prority;
        private string _sjb_quotation;
        private string _sjb_redojob;
        private string _sjb_reqno;
        private string _sjb_rmk;
        private DateTime _sjb_start_dt;
        private string _sjb_stus;
        private string _sjb_substage;
        private DateTime _sjb_techfinishdate;
        private DateTime _sjb_techstartdate;
        private string _sjb_tech_rmk;
        private string _sjb_title;
        private string _sjb_town;
        private string _sjb_transferby;
        private DateTime _sjb_transferdt;
        #endregion

        public string Sjb_address1 { get { return _sjb_address1; } set { _sjb_address1 = value; } }
        public string Sjb_address2 { get { return _sjb_address2; } set { _sjb_address2 = value; } }
        public string Sjb_agreementno { get { return _sjb_agreementno; } set { _sjb_agreementno = value; } }
        public DateTime Sjb_aodissuedt { get { return _sjb_aodissuedt; } set { _sjb_aodissuedt = value; } }
        public string Sjb_aodissueloc { get { return _sjb_aodissueloc; } set { _sjb_aodissueloc = value; } }
        public string Sjb_aodissueno { get { return _sjb_aodissueno; } set { _sjb_aodissueno = value; } }
        public string Sjb_aodrecno { get { return _sjb_aodrecno; } set { _sjb_aodrecno = value; } }
        public string Sjb_bill_address1 { get { return _sjb_bill_address1; } set { _sjb_bill_address1 = value; } }
        public string Sjb_bill_address2 { get { return _sjb_bill_address2; } set { _sjb_bill_address2 = value; } }
        public string Sjb_bill_address3 { get { return _sjb_bill_address3; } set { _sjb_bill_address3 = value; } }
        public string Sjb_bill_cust_cd { get { return _sjb_bill_cust_cd; } set { _sjb_bill_cust_cd = value; } }
        public string Sjb_bill_cust_name { get { return _sjb_bill_cust_name; } set { _sjb_bill_cust_name = value; } }
        public string Sjb_bill_email { get { return _sjb_bill_email; } set { _sjb_bill_email = value; } }
        public string Sjb_bill_fax { get { return _sjb_bill_fax; } set { _sjb_bill_fax = value; } }
        public string Sjb_bill_id { get { return _sjb_bill_id; } set { _sjb_bill_id = value; } }
        public string Sjb_bill_idtype { get { return _sjb_bill_idtype; } set { _sjb_bill_idtype = value; } }
        public string Sjb_bill_mobino { get { return _sjb_bill_mobino; } set { _sjb_bill_mobino = value; } }
        public string Sjb_bill_phone_no { get { return _sjb_bill_phone_no; } set { _sjb_bill_phone_no = value; } }
        public string Sjb_bill_title { get { return _sjb_bill_title; } set { _sjb_bill_title = value; } }
        public string Sjb_bill_town { get { return _sjb_bill_town; } set { _sjb_bill_town = value; } }
        public string Sjb_chnl { get { return _sjb_chnl; } set { _sjb_chnl = value; } }
        public string Sjb_com { get { return _sjb_com; } set { _sjb_com = value; } }
        public string Sjb_cont_address1 { get { return _sjb_cont_address1; } set { _sjb_cont_address1 = value; } }
        public string Sjb_cont_address2 { get { return _sjb_cont_address2; } set { _sjb_cont_address2 = value; } }
        public string Sjb_cont_person { get { return _sjb_cont_person; } set { _sjb_cont_person = value; } }
        public string Sjb_cont_phone_no { get { return _sjb_cont_phone_no; } set { _sjb_cont_phone_no = value; } }
        public string Sjb_cre_by { get { return _sjb_cre_by; } set { _sjb_cre_by = value; } }
        public DateTime Sjb_cre_dt { get { return _sjb_cre_dt; } set { _sjb_cre_dt = value; } }
        public string Sjb_cusagreementno { get { return _sjb_cusagreementno; } set { _sjb_cusagreementno = value; } }
        public string Sjb_cust_cd { get { return _sjb_cust_cd; } set { _sjb_cust_cd = value; } }
        public DateTime Sjb_cust_expectdt { get { return _sjb_cust_expectdt; } set { _sjb_cust_expectdt = value; } }
        public string Sjb_cust_name { get { return _sjb_cust_name; } set { _sjb_cust_name = value; } }
        public DateTime Sjb_dt { get { return _sjb_dt; } set { _sjb_dt = value; } }
        public string Sjb_email { get { return _sjb_email; } set { _sjb_email = value; } }
        public DateTime Sjb_end_dt { get { return _sjb_end_dt; } set { _sjb_end_dt = value; } }
        public string Sjb_faxno { get { return _sjb_faxno; } set { _sjb_faxno = value; } }
        public string Sjb_idno { get { return _sjb_idno; } set { _sjb_idno = value; } }
        public string Sjb_id_tp { get { return _sjb_id_tp; } set { _sjb_id_tp = value; } }
        public string Sjb_inform_contact { get { return _sjb_inform_contact; } set { _sjb_inform_contact = value; } }
        public string Sjb_inform_location { get { return _sjb_inform_location; } set { _sjb_inform_location = value; } }
        public string Sjb_inform_person { get { return _sjb_inform_person; } set { _sjb_inform_person = value; } }
        public string Sjb_insu_com { get { return _sjb_insu_com; } set { _sjb_insu_com = value; } }
        public string Sjb_isagreement { get { return _sjb_isagreement; } set { _sjb_isagreement = value; } }
        public Boolean Sjb_isexternalitem { get { return _sjb_isexternalitem; } set { _sjb_isexternalitem = value; } }
        public Boolean Sjb_isprejob { get { return _sjb_isprejob; } set { _sjb_isprejob = value; } }
        public Boolean Sjb_issrn { get { return _sjb_issrn; } set { _sjb_issrn = value; } }
        public string Sjb_itm_tp { get { return _sjb_itm_tp; } set { _sjb_itm_tp = value; } }
        public string Sjb_jobclosecd { get { return _sjb_jobclosecd; } set { _sjb_jobclosecd = value; } }
        public string Sjb_jobclosedesc { get { return _sjb_jobclosedesc; } set { _sjb_jobclosedesc = value; } }
        public string Sjb_jobclosermk { get { return _sjb_jobclosermk; } set { _sjb_jobclosermk = value; } }
        public DateTime Sjb_jobconfdate { get { return _sjb_jobconfdate; } set { _sjb_jobconfdate = value; } }
        public string Sjb_jobno { get { return _sjb_jobno; } set { _sjb_jobno = value; } }
        public decimal Sjb_jobstage { get { return _sjb_jobstage; } set { _sjb_jobstage = value; } }
        public string Sjb_job_cat { get { return _sjb_job_cat; } set { _sjb_job_cat = value; } }
        public string Sjb_job_rmk { get { return _sjb_job_rmk; } set { _sjb_job_rmk = value; } }
        public string Sjb_job_sub_tp { get { return _sjb_job_sub_tp; } set { _sjb_job_sub_tp = value; } }
        public string Sjb_job_tp { get { return _sjb_job_tp; } set { _sjb_job_tp = value; } }
        public string Sjb_lastprintby { get { return _sjb_lastprintby; } set { _sjb_lastprintby = value; } }
        public string Sjb_loc { get { return _sjb_loc; } set { _sjb_loc = value; } }
        public string Sjb_mainjobno { get { return _sjb_mainjobno; } set { _sjb_mainjobno = value; } }
        public string Sjb_mainreqloc { get { return _sjb_mainreqloc; } set { _sjb_mainreqloc = value; } }
        public string Sjb_mainreqno { get { return _sjb_mainreqno; } set { _sjb_mainreqno = value; } }
        public string Sjb_manualref { get { return _sjb_manualref; } set { _sjb_manualref = value; } }
        public string Sjb_mobino { get { return _sjb_mobino; } set { _sjb_mobino = value; } }
        public string Sjb_mod_by { get { return _sjb_mod_by; } set { _sjb_mod_by = value; } }
        public DateTime Sjb_mod_dt { get { return _sjb_mod_dt; } set { _sjb_mod_dt = value; } }
        public string Sjb_msn_no { get { return _sjb_msn_no; } set { _sjb_msn_no = value; } }
        public int Sjb_noofprint { get { return _sjb_noofprint; } set { _sjb_noofprint = value; } }
        public string Sjb_orderno { get { return _sjb_orderno; } set { _sjb_orderno = value; } }
        public string Sjb_othref { get { return _sjb_othref; } set { _sjb_othref = value; } }
        public string Sjb_pc { get { return _sjb_pc; } set { _sjb_pc = value; } }
        public string Sjb_phoneno { get { return _sjb_phoneno; } set { _sjb_phoneno = value; } }
        public string Sjb_prority { get { return _sjb_prority; } set { _sjb_prority = value; } }
        public string Sjb_quotation { get { return _sjb_quotation; } set { _sjb_quotation = value; } }
        public string Sjb_redojob { get { return _sjb_redojob; } set { _sjb_redojob = value; } }
        public string Sjb_reqno { get { return _sjb_reqno; } set { _sjb_reqno = value; } }
        public string Sjb_rmk { get { return _sjb_rmk; } set { _sjb_rmk = value; } }
        public DateTime Sjb_start_dt { get { return _sjb_start_dt; } set { _sjb_start_dt = value; } }
        public string Sjb_stus { get { return _sjb_stus; } set { _sjb_stus = value; } }
        public string Sjb_substage { get { return _sjb_substage; } set { _sjb_substage = value; } }
        public DateTime Sjb_techfinishdate { get { return _sjb_techfinishdate; } set { _sjb_techfinishdate = value; } }
        public DateTime Sjb_techstartdate { get { return _sjb_techstartdate; } set { _sjb_techstartdate = value; } }
        public string Sjb_tech_rmk { get { return _sjb_tech_rmk; } set { _sjb_tech_rmk = value; } }
        public string Sjb_title { get { return _sjb_title; } set { _sjb_title = value; } }
        public string Sjb_town { get { return _sjb_town; } set { _sjb_town = value; } }
        public string Sjb_transferby { get { return _sjb_transferby; } set { _sjb_transferby = value; } }
        public DateTime Sjb_transferdt { get { return _sjb_transferdt; } set { _sjb_transferdt = value; } }
      
            
        public static ServiceJobHeader Converter(DataRow row)
        {
            return new ServiceJobHeader
            {
                Sjb_address1 = row["SJB_ADDRESS1"] == DBNull.Value ? string.Empty : row["SJB_ADDRESS1"].ToString(),
                Sjb_address2 = row["SJB_ADDRESS2"] == DBNull.Value ? string.Empty : row["SJB_ADDRESS2"].ToString(),
                Sjb_agreementno = row["SJB_AGREEMENTNO"] == DBNull.Value ? string.Empty : row["SJB_AGREEMENTNO"].ToString(),
                Sjb_aodissuedt = row["SJB_AODISSUEDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_AODISSUEDT"]),
                Sjb_aodissueloc = row["SJB_AODISSUELOC"] == DBNull.Value ? string.Empty : row["SJB_AODISSUELOC"].ToString(),
                Sjb_aodissueno = row["SJB_AODISSUENO"] == DBNull.Value ? string.Empty : row["SJB_AODISSUENO"].ToString(),
                Sjb_aodrecno = row["SJB_AODRECNO"] == DBNull.Value ? string.Empty : row["SJB_AODRECNO"].ToString(),
                Sjb_bill_address1 = row["SJB_BILL_ADDRESS1"] == DBNull.Value ? string.Empty : row["SJB_BILL_ADDRESS1"].ToString(),
                Sjb_bill_address2 = row["SJB_BILL_ADDRESS2"] == DBNull.Value ? string.Empty : row["SJB_BILL_ADDRESS2"].ToString(),
                Sjb_bill_address3 = row["SJB_BILL_ADDRESS3"] == DBNull.Value ? string.Empty : row["SJB_BILL_ADDRESS3"].ToString(),
                Sjb_bill_cust_cd = row["SJB_BILL_CUST_CD"] == DBNull.Value ? string.Empty : row["SJB_BILL_CUST_CD"].ToString(),
                Sjb_bill_cust_name = row["SJB_BILL_CUST_NAME"] == DBNull.Value ? string.Empty : row["SJB_BILL_CUST_NAME"].ToString(),
                Sjb_bill_email = row["SJB_BILL_EMAIL"] == DBNull.Value ? string.Empty : row["SJB_BILL_EMAIL"].ToString(),
                Sjb_bill_fax = row["SJB_BILL_FAX"] == DBNull.Value ? string.Empty : row["SJB_BILL_FAX"].ToString(),
                Sjb_bill_id = row["SJB_BILL_ID"] == DBNull.Value ? string.Empty : row["SJB_BILL_ID"].ToString(),
                Sjb_bill_idtype = row["SJB_BILL_IDTYPE"] == DBNull.Value ? string.Empty : row["SJB_BILL_IDTYPE"].ToString(),
                Sjb_bill_mobino = row["SJB_BILL_MOBINO"] == DBNull.Value ? string.Empty : row["SJB_BILL_MOBINO"].ToString(),
                Sjb_bill_phone_no = row["SJB_BILL_PHONE_NO"] == DBNull.Value ? string.Empty : row["SJB_BILL_PHONE_NO"].ToString(),
                Sjb_bill_title = row["SJB_BILL_TITLE"] == DBNull.Value ? string.Empty : row["SJB_BILL_TITLE"].ToString(),
                Sjb_bill_town = row["SJB_BILL_TOWN"] == DBNull.Value ? string.Empty : row["SJB_BILL_TOWN"].ToString(),
                Sjb_chnl = row["SJB_CHNL"] == DBNull.Value ? string.Empty : row["SJB_CHNL"].ToString(),
                Sjb_com = row["SJB_COM"] == DBNull.Value ? string.Empty : row["SJB_COM"].ToString(),
                Sjb_cont_address1 = row["SJB_CONT_ADDRESS1"] == DBNull.Value ? string.Empty : row["SJB_CONT_ADDRESS1"].ToString(),
                Sjb_cont_address2 = row["SJB_CONT_ADDRESS2"] == DBNull.Value ? string.Empty : row["SJB_CONT_ADDRESS2"].ToString(),
                Sjb_cont_person = row["SJB_CONT_PERSON"] == DBNull.Value ? string.Empty : row["SJB_CONT_PERSON"].ToString(),
                Sjb_cont_phone_no = row["SJB_CONT_PHONE_NO"] == DBNull.Value ? string.Empty : row["SJB_CONT_PHONE_NO"].ToString(),
                Sjb_cre_by = row["SJB_CRE_BY"] == DBNull.Value ? string.Empty : row["SJB_CRE_BY"].ToString(),
                Sjb_cre_dt = row["SJB_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_CRE_DT"]),
                Sjb_cusagreementno = row["SJB_CUSAGREEMENTNO"] == DBNull.Value ? string.Empty : row["SJB_CUSAGREEMENTNO"].ToString(),
                Sjb_cust_cd = row["SJB_CUST_CD"] == DBNull.Value ? string.Empty : row["SJB_CUST_CD"].ToString(),
                Sjb_cust_expectdt = row["SJB_CUST_EXPECTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_CUST_EXPECTDT"]),
                Sjb_cust_name = row["SJB_CUST_NAME"] == DBNull.Value ? string.Empty : row["SJB_CUST_NAME"].ToString(),
                Sjb_dt = row["SJB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_DT"]),
                Sjb_email = row["SJB_EMAIL"] == DBNull.Value ? string.Empty : row["SJB_EMAIL"].ToString(),
                Sjb_end_dt = row["SJB_END_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_END_DT"]),
                Sjb_faxno = row["SJB_FAXNO"] == DBNull.Value ? string.Empty : row["SJB_FAXNO"].ToString(),
                Sjb_idno = row["SJB_IDNO"] == DBNull.Value ? string.Empty : row["SJB_IDNO"].ToString(),
                Sjb_id_tp = row["SJB_ID_TP"] == DBNull.Value ? string.Empty : row["SJB_ID_TP"].ToString(),
                Sjb_inform_contact = row["SJB_INFORM_CONTACT"] == DBNull.Value ? string.Empty : row["SJB_INFORM_CONTACT"].ToString(),
                Sjb_inform_location = row["SJB_INFORM_LOCATION"] == DBNull.Value ? string.Empty : row["SJB_INFORM_LOCATION"].ToString(),
                Sjb_inform_person = row["SJB_INFORM_PERSON"] == DBNull.Value ? string.Empty : row["SJB_INFORM_PERSON"].ToString(),
                Sjb_insu_com = row["SJB_INSU_COM"] == DBNull.Value ? string.Empty : row["SJB_INSU_COM"].ToString(),
                Sjb_isagreement = row["SJB_ISAGREEMENT"] == DBNull.Value ? string.Empty : row["SJB_ISAGREEMENT"].ToString(),
                Sjb_isexternalitem = row["SJB_ISEXTERNALITEM"] == DBNull.Value ? false : Convert.ToBoolean(row["SJB_ISEXTERNALITEM"]),
                Sjb_isprejob = row["SJB_ISPREJOB"] == DBNull.Value ? false : Convert.ToBoolean(row["SJB_ISPREJOB"]),
                Sjb_issrn = row["SJB_ISSRN"] == DBNull.Value ? false : Convert.ToBoolean(row["SJB_ISSRN"]),
                Sjb_itm_tp = row["SJB_ITM_TP"] == DBNull.Value ? string.Empty : row["SJB_ITM_TP"].ToString(),
                Sjb_jobclosecd = row["SJB_JOBCLOSECD"] == DBNull.Value ? string.Empty : row["SJB_JOBCLOSECD"].ToString(),
                Sjb_jobclosedesc = row["SJB_JOBCLOSEDESC"] == DBNull.Value ? string.Empty : row["SJB_JOBCLOSEDESC"].ToString(),
                Sjb_jobclosermk = row["SJB_JOBCLOSERMK"] == DBNull.Value ? string.Empty : row["SJB_JOBCLOSERMK"].ToString(),
                Sjb_jobconfdate = row["SJB_JOBCONFDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_JOBCONFDATE"]),
                Sjb_jobno = row["SJB_JOBNO"] == DBNull.Value ? string.Empty : row["SJB_JOBNO"].ToString(),
                Sjb_jobstage = row["SJB_JOBSTAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SJB_JOBSTAGE"]),
                Sjb_job_cat = row["SJB_JOB_CAT"] == DBNull.Value ? string.Empty : row["SJB_JOB_CAT"].ToString(),
                Sjb_job_rmk = row["SJB_JOB_RMK"] == DBNull.Value ? string.Empty : row["SJB_JOB_RMK"].ToString(),
                Sjb_job_sub_tp = row["SJB_JOB_SUB_TP"] == DBNull.Value ? string.Empty : row["SJB_JOB_SUB_TP"].ToString(),
                Sjb_job_tp = row["SJB_JOB_TP"] == DBNull.Value ? string.Empty : row["SJB_JOB_TP"].ToString(),
                Sjb_lastprintby = row["SJB_LASTPRINTBY"] == DBNull.Value ? string.Empty : row["SJB_LASTPRINTBY"].ToString(),
                Sjb_loc = row["SJB_LOC"] == DBNull.Value ? string.Empty : row["SJB_LOC"].ToString(),
                Sjb_mainjobno = row["SJB_MAINJOBNO"] == DBNull.Value ? string.Empty : row["SJB_MAINJOBNO"].ToString(),
                Sjb_mainreqloc = row["SJB_MAINREQLOC"] == DBNull.Value ? string.Empty : row["SJB_MAINREQLOC"].ToString(),
                Sjb_mainreqno = row["SJB_MAINREQNO"] == DBNull.Value ? string.Empty : row["SJB_MAINREQNO"].ToString(),
                Sjb_manualref = row["SJB_MANUALREF"] == DBNull.Value ? string.Empty : row["SJB_MANUALREF"].ToString(),
                Sjb_mobino = row["SJB_MOBINO"] == DBNull.Value ? string.Empty : row["SJB_MOBINO"].ToString(),
                Sjb_mod_by = row["SJB_MOD_BY"] == DBNull.Value ? string.Empty : row["SJB_MOD_BY"].ToString(),
                Sjb_mod_dt = row["SJB_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_MOD_DT"]),
                Sjb_msn_no = row["SJB_MSN_NO"] == DBNull.Value ? string.Empty : row["SJB_MSN_NO"].ToString(),
                Sjb_noofprint = row["SJB_NOOFPRINT"] == DBNull.Value ? 0 : Convert.ToInt16(row["SJB_NOOFPRINT"]),
                Sjb_orderno = row["SJB_ORDERNO"] == DBNull.Value ? string.Empty : row["SJB_ORDERNO"].ToString(),
                Sjb_othref = row["SJB_OTHREF"] == DBNull.Value ? string.Empty : row["SJB_OTHREF"].ToString(),
                Sjb_pc = row["SJB_PC"] == DBNull.Value ? string.Empty : row["SJB_PC"].ToString(),
                Sjb_phoneno = row["SJB_PHONENO"] == DBNull.Value ? string.Empty : row["SJB_PHONENO"].ToString(),
                Sjb_prority = row["SJB_PRORITY"] == DBNull.Value ? string.Empty : row["SJB_PRORITY"].ToString(),
                Sjb_quotation = row["SJB_QUOTATION"] == DBNull.Value ? string.Empty : row["SJB_QUOTATION"].ToString(),
                Sjb_redojob = row["SJB_REDOJOB"] == DBNull.Value ? string.Empty : row["SJB_REDOJOB"].ToString(),
                Sjb_reqno = row["SJB_REQNO"] == DBNull.Value ? string.Empty : row["SJB_REQNO"].ToString(),
                Sjb_rmk = row["SJB_RMK"] == DBNull.Value ? string.Empty : row["SJB_RMK"].ToString(),
                Sjb_start_dt = row["SJB_START_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_START_DT"]),
                Sjb_stus = row["SJB_STUS"] == DBNull.Value ? string.Empty : row["SJB_STUS"].ToString(),
                Sjb_substage = row["SJB_SUBSTAGE"] == DBNull.Value ? string.Empty : row["SJB_SUBSTAGE"].ToString(),
                Sjb_techfinishdate = row["SJB_TECHFINISHDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_TECHFINISHDATE"]),
                Sjb_techstartdate = row["SJB_TECHSTARTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_TECHSTARTDATE"]),
                Sjb_tech_rmk = row["SJB_TECH_RMK"] == DBNull.Value ? string.Empty : row["SJB_TECH_RMK"].ToString(),
                Sjb_title = row["SJB_TITLE"] == DBNull.Value ? string.Empty : row["SJB_TITLE"].ToString(),
                Sjb_town = row["SJB_TOWN"] == DBNull.Value ? string.Empty : row["SJB_TOWN"].ToString(),
                Sjb_transferby = row["SJB_TRANSFERBY"] == DBNull.Value ? string.Empty : row["SJB_TRANSFERBY"].ToString(),
                Sjb_transferdt = row["SJB_TRANSFERDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_TRANSFERDT"])
              
             
            };
        }
    }
}
