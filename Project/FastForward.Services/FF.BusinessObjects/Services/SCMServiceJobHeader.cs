using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class SCMServiceJobHeader
    {
        #region Private Members
        private string _sjb_address1;
        private string _sjb_address2;
        private string _sjb_agreementno;
        private DateTime _sjb_aodissuedate;
        private string _sjb_aodissueloca;
        private string _sjb_aodissueno;
        private string _sjb_aodrecno;
        private string _sjb_bill_address1;
        private string _sjb_bill_address2;
        private string _sjb_bill_address3;
        private string _sjb_bill_cus_code;
        private string _sjb_bill_cus_name;
        private string _sjb_bill_cus_title;
        private string _sjb_bill_email;
        private string _sjb_bill_fax;
        private string _sjb_bill_id;
        private string _sjb_bill_idtype;
        private string _sjb_bill_mobileno;
        private string _sjb_bill_phone_no;
        private string _sjb_bill_town;
        private string _sjb_channel;
        private string _sjb_company;
        private string _sjb_cont_address1;
        private string _sjb_cont_address2;
        private string _sjb_cont_person;
        private string _sjb_cont_phone_no;
        private string _sjb_createby;
        private DateTime _sjb_createwhen;
        private string _sjb_cusagreementno;
        private DateTime _sjb_cusexpectdate;
        private string _sjb_custcode;
        private string _sjb_custname;
        private string _sjb_custtitle;
        private DateTime _sjb_date;
        private string _sjb_email;
        private DateTime _sjb_enddate;
        private string _sjb_faxno;
        private string _sjb_idno;
        private string _sjb_idtype;
        private string _sjb_inform_contact;
        private string _sjb_inform_location;
        private string _sjb_inform_person;
        private string _sjb_insucompany;
        private string _sjb_isagreement;
        private Boolean _sjb_isexternalitem;
        private Boolean _sjb_isprejob;
        private Boolean _sjb_issrn;
        private string _sjb_itemtype;
        private string _sjb_jobcategory;
        private string _sjb_jobclosecode;
        private string _sjb_jobclosedesc;
        private string _sjb_jobcloseremarks;
        private DateTime _sjb_jobconfdate;
        private string _sjb_jobno;
        private decimal _sjb_jobstage;
        private string _sjb_jobsubtype;
        private string _sjb_jobtype;
        private string _sjb_job_remarks;
        private string _sjb_lastmodifyby;
        private DateTime _sjb_lastmodifywhen;
        private string _sjb_lastprintby;
        private string _sjb_location;
        private string _sjb_mainjobno;
        private string _sjb_mainreqlocation;
        private string _sjb_mainreqno;
        private string _sjb_manualref;
        private string _sjb_mobino;
        private string _sjb_msn_no;
        private int _sjb_noofprint;
        private string _sjb_orderno;
        private string _sjb_otherref;
        private string _sjb_phoneno;
        private string _sjb_profitcenter;
        private string _sjb_prority;
        private string _sjb_quotation;
        private string _sjb_redojob;
        private string _sjb_remarks;
        private string _sjb_reqno;
        private DateTime _sjb_startdate;
        private string _sjb_status;
        private string _sjb_substage;
        private DateTime _sjb_techfinishdate;
        private DateTime _sjb_techstartdate;
        private string _sjb_tech_remarks;
        private string _sjb_town;
        private string _sjb_transferby;
        private DateTime _sjb_transferdate;
        #endregion

        public string Sjb_address1
        {
            get { return _sjb_address1; }
            set { _sjb_address1 = value; }
        }
        public string Sjb_address2
        {
            get { return _sjb_address2; }
            set { _sjb_address2 = value; }
        }
        public string Sjb_agreementno
        {
            get { return _sjb_agreementno; }
            set { _sjb_agreementno = value; }
        }
        public DateTime Sjb_aodissuedate
        {
            get { return _sjb_aodissuedate; }
            set { _sjb_aodissuedate = value; }
        }
        public string Sjb_aodissueloca
        {
            get { return _sjb_aodissueloca; }
            set { _sjb_aodissueloca = value; }
        }
        public string Sjb_aodissueno
        {
            get { return _sjb_aodissueno; }
            set { _sjb_aodissueno = value; }
        }
        public string Sjb_aodrecno
        {
            get { return _sjb_aodrecno; }
            set { _sjb_aodrecno = value; }
        }
        public string Sjb_bill_address1
        {
            get { return _sjb_bill_address1; }
            set { _sjb_bill_address1 = value; }
        }
        public string Sjb_bill_address2
        {
            get { return _sjb_bill_address2; }
            set { _sjb_bill_address2 = value; }
        }
        public string Sjb_bill_address3
        {
            get { return _sjb_bill_address3; }
            set { _sjb_bill_address3 = value; }
        }
        public string Sjb_bill_cus_code
        {
            get { return _sjb_bill_cus_code; }
            set { _sjb_bill_cus_code = value; }
        }
        public string Sjb_bill_cus_name
        {
            get { return _sjb_bill_cus_name; }
            set { _sjb_bill_cus_name = value; }
        }
        public string Sjb_bill_cus_title
        {
            get { return _sjb_bill_cus_title; }
            set { _sjb_bill_cus_title = value; }
        }
        public string Sjb_bill_email
        {
            get { return _sjb_bill_email; }
            set { _sjb_bill_email = value; }
        }
        public string Sjb_bill_fax
        {
            get { return _sjb_bill_fax; }
            set { _sjb_bill_fax = value; }
        }
        public string Sjb_bill_id
        {
            get { return _sjb_bill_id; }
            set { _sjb_bill_id = value; }
        }
        public string Sjb_bill_idtype
        {
            get { return _sjb_bill_idtype; }
            set { _sjb_bill_idtype = value; }
        }
        public string Sjb_bill_mobileno
        {
            get { return _sjb_bill_mobileno; }
            set { _sjb_bill_mobileno = value; }
        }
        public string Sjb_bill_phone_no
        {
            get { return _sjb_bill_phone_no; }
            set { _sjb_bill_phone_no = value; }
        }
        public string Sjb_bill_town
        {
            get { return _sjb_bill_town; }
            set { _sjb_bill_town = value; }
        }
        public string Sjb_channel
        {
            get { return _sjb_channel; }
            set { _sjb_channel = value; }
        }
        public string Sjb_company
        {
            get { return _sjb_company; }
            set { _sjb_company = value; }
        }
        public string Sjb_cont_address1
        {
            get { return _sjb_cont_address1; }
            set { _sjb_cont_address1 = value; }
        }
        public string Sjb_cont_address2
        {
            get { return _sjb_cont_address2; }
            set { _sjb_cont_address2 = value; }
        }
        public string Sjb_cont_person
        {
            get { return _sjb_cont_person; }
            set { _sjb_cont_person = value; }
        }
        public string Sjb_cont_phone_no
        {
            get { return _sjb_cont_phone_no; }
            set { _sjb_cont_phone_no = value; }
        }
        public string Sjb_createby
        {
            get { return _sjb_createby; }
            set { _sjb_createby = value; }
        }
        public DateTime Sjb_createwhen
        {
            get { return _sjb_createwhen; }
            set { _sjb_createwhen = value; }
        }
        public string Sjb_cusagreementno
        {
            get { return _sjb_cusagreementno; }
            set { _sjb_cusagreementno = value; }
        }
        public DateTime Sjb_cusexpectdate
        {
            get { return _sjb_cusexpectdate; }
            set { _sjb_cusexpectdate = value; }
        }
        public string Sjb_custcode
        {
            get { return _sjb_custcode; }
            set { _sjb_custcode = value; }
        }
        public string Sjb_custname
        {
            get { return _sjb_custname; }
            set { _sjb_custname = value; }
        }
        public string Sjb_custtitle
        {
            get { return _sjb_custtitle; }
            set { _sjb_custtitle = value; }
        }
        public DateTime Sjb_date
        {
            get { return _sjb_date; }
            set { _sjb_date = value; }
        }
        public string Sjb_email
        {
            get { return _sjb_email; }
            set { _sjb_email = value; }
        }
        public DateTime Sjb_enddate
        {
            get { return _sjb_enddate; }
            set { _sjb_enddate = value; }
        }
        public string Sjb_faxno
        {
            get { return _sjb_faxno; }
            set { _sjb_faxno = value; }
        }
        public string Sjb_idno
        {
            get { return _sjb_idno; }
            set { _sjb_idno = value; }
        }
        public string Sjb_idtype
        {
            get { return _sjb_idtype; }
            set { _sjb_idtype = value; }
        }
        public string Sjb_inform_contact
        {
            get { return _sjb_inform_contact; }
            set { _sjb_inform_contact = value; }
        }
        public string Sjb_inform_location
        {
            get { return _sjb_inform_location; }
            set { _sjb_inform_location = value; }
        }
        public string Sjb_inform_person
        {
            get { return _sjb_inform_person; }
            set { _sjb_inform_person = value; }
        }
        public string Sjb_insucompany
        {
            get { return _sjb_insucompany; }
            set { _sjb_insucompany = value; }
        }
        public string Sjb_isagreement
        {
            get { return _sjb_isagreement; }
            set { _sjb_isagreement = value; }
        }
        public Boolean Sjb_isexternalitem
        {
            get { return _sjb_isexternalitem; }
            set { _sjb_isexternalitem = value; }
        }
        public Boolean Sjb_isprejob
        {
            get { return _sjb_isprejob; }
            set { _sjb_isprejob = value; }
        }
        public Boolean Sjb_issrn
        {
            get { return _sjb_issrn; }
            set { _sjb_issrn = value; }
        }
        public string Sjb_itemtype
        {
            get { return _sjb_itemtype; }
            set { _sjb_itemtype = value; }
        }
        public string Sjb_jobcategory
        {
            get { return _sjb_jobcategory; }
            set { _sjb_jobcategory = value; }
        }
        public string Sjb_jobclosecode
        {
            get { return _sjb_jobclosecode; }
            set { _sjb_jobclosecode = value; }
        }
        public string Sjb_jobclosedesc
        {
            get { return _sjb_jobclosedesc; }
            set { _sjb_jobclosedesc = value; }
        }
        public string Sjb_jobcloseremarks
        {
            get { return _sjb_jobcloseremarks; }
            set { _sjb_jobcloseremarks = value; }
        }
        public DateTime Sjb_jobconfdate
        {
            get { return _sjb_jobconfdate; }
            set { _sjb_jobconfdate = value; }
        }
        public string Sjb_jobno
        {
            get { return _sjb_jobno; }
            set { _sjb_jobno = value; }
        }
        public decimal Sjb_jobstage
        {
            get { return _sjb_jobstage; }
            set { _sjb_jobstage = value; }
        }
        public string Sjb_jobsubtype
        {
            get { return _sjb_jobsubtype; }
            set { _sjb_jobsubtype = value; }
        }
        public string Sjb_jobtype
        {
            get { return _sjb_jobtype; }
            set { _sjb_jobtype = value; }
        }
        public string Sjb_job_remarks
        {
            get { return _sjb_job_remarks; }
            set { _sjb_job_remarks = value; }
        }
        public string Sjb_lastmodifyby
        {
            get { return _sjb_lastmodifyby; }
            set { _sjb_lastmodifyby = value; }
        }
        public DateTime Sjb_lastmodifywhen
        {
            get { return _sjb_lastmodifywhen; }
            set { _sjb_lastmodifywhen = value; }
        }
        public string Sjb_lastprintby
        {
            get { return _sjb_lastprintby; }
            set { _sjb_lastprintby = value; }
        }
        public string Sjb_location
        {
            get { return _sjb_location; }
            set { _sjb_location = value; }
        }
        public string Sjb_mainjobno
        {
            get { return _sjb_mainjobno; }
            set { _sjb_mainjobno = value; }
        }
        public string Sjb_mainreqlocation
        {
            get { return _sjb_mainreqlocation; }
            set { _sjb_mainreqlocation = value; }
        }
        public string Sjb_mainreqno
        {
            get { return _sjb_mainreqno; }
            set { _sjb_mainreqno = value; }
        }
        public string Sjb_manualref
        {
            get { return _sjb_manualref; }
            set { _sjb_manualref = value; }
        }
        public string Sjb_mobino
        {
            get { return _sjb_mobino; }
            set { _sjb_mobino = value; }
        }
        public string Sjb_msn_no
        {
            get { return _sjb_msn_no; }
            set { _sjb_msn_no = value; }
        }
        public int Sjb_noofprint
        {
            get { return _sjb_noofprint; }
            set { _sjb_noofprint = value; }
        }
        public string Sjb_orderno
        {
            get { return _sjb_orderno; }
            set { _sjb_orderno = value; }
        }
        public string Sjb_otherref
        {
            get { return _sjb_otherref; }
            set { _sjb_otherref = value; }
        }
        public string Sjb_phoneno
        {
            get { return _sjb_phoneno; }
            set { _sjb_phoneno = value; }
        }
        public string Sjb_profitcenter
        {
            get { return _sjb_profitcenter; }
            set { _sjb_profitcenter = value; }
        }
        public string Sjb_prority
        {
            get { return _sjb_prority; }
            set { _sjb_prority = value; }
        }
        public string Sjb_quotation
        {
            get { return _sjb_quotation; }
            set { _sjb_quotation = value; }
        }
        public string Sjb_redojob
        {
            get { return _sjb_redojob; }
            set { _sjb_redojob = value; }
        }
        public string Sjb_remarks
        {
            get { return _sjb_remarks; }
            set { _sjb_remarks = value; }
        }
        public string Sjb_reqno
        {
            get { return _sjb_reqno; }
            set { _sjb_reqno = value; }
        }
        public DateTime Sjb_startdate
        {
            get { return _sjb_startdate; }
            set { _sjb_startdate = value; }
        }
        public string Sjb_status
        {
            get { return _sjb_status; }
            set { _sjb_status = value; }
        }
        public string Sjb_substage
        {
            get { return _sjb_substage; }
            set { _sjb_substage = value; }
        }
        public DateTime Sjb_techfinishdate
        {
            get { return _sjb_techfinishdate; }
            set { _sjb_techfinishdate = value; }
        }
        public DateTime Sjb_techstartdate
        {
            get { return _sjb_techstartdate; }
            set { _sjb_techstartdate = value; }
        }
        public string Sjb_tech_remarks
        {
            get { return _sjb_tech_remarks; }
            set { _sjb_tech_remarks = value; }
        }
        public string Sjb_town
        {
            get { return _sjb_town; }
            set { _sjb_town = value; }
        }
        public string Sjb_transferby
        {
            get { return _sjb_transferby; }
            set { _sjb_transferby = value; }
        }
        public DateTime Sjb_transferdate
        {
            get { return _sjb_transferdate; }
            set { _sjb_transferdate = value; }
        }

        public static SCMServiceJobHeader Converter(DataRow row)
        {
            return new SCMServiceJobHeader
            {
                Sjb_address1 = row["SJB_ADDRESS1"] == DBNull.Value ? string.Empty : row["SJB_ADDRESS1"].ToString(),
                Sjb_address2 = row["SJB_ADDRESS2"] == DBNull.Value ? string.Empty : row["SJB_ADDRESS2"].ToString(),
                Sjb_agreementno = row["SJB_AGREEMENTNO"] == DBNull.Value ? string.Empty : row["SJB_AGREEMENTNO"].ToString(),
                Sjb_aodissuedate = row["SJB_AODISSUEDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_AODISSUEDATE"]),
                Sjb_aodissueloca = row["SJB_AODISSUELOCA"] == DBNull.Value ? string.Empty : row["SJB_AODISSUELOCA"].ToString(),
                Sjb_aodissueno = row["SJB_AODISSUENO"] == DBNull.Value ? string.Empty : row["SJB_AODISSUENO"].ToString(),
                Sjb_aodrecno = row["SJB_AODRECNO"] == DBNull.Value ? string.Empty : row["SJB_AODRECNO"].ToString(),
                Sjb_bill_address1 = row["SJB_BILL_ADDRESS1"] == DBNull.Value ? string.Empty : row["SJB_BILL_ADDRESS1"].ToString(),
                Sjb_bill_address2 = row["SJB_BILL_ADDRESS2"] == DBNull.Value ? string.Empty : row["SJB_BILL_ADDRESS2"].ToString(),
                Sjb_bill_address3 = row["SJB_BILL_ADDRESS3"] == DBNull.Value ? string.Empty : row["SJB_BILL_ADDRESS3"].ToString(),
                Sjb_bill_cus_code = row["SJB_BILL_CUS_CODE"] == DBNull.Value ? string.Empty : row["SJB_BILL_CUS_CODE"].ToString(),
                Sjb_bill_cus_name = row["SJB_BILL_CUS_NAME"] == DBNull.Value ? string.Empty : row["SJB_BILL_CUS_NAME"].ToString(),
                Sjb_bill_cus_title = row["SJB_BILL_CUS_TITLE"] == DBNull.Value ? string.Empty : row["SJB_BILL_CUS_TITLE"].ToString(),
                Sjb_bill_email = row["SJB_BILL_EMAIL"] == DBNull.Value ? string.Empty : row["SJB_BILL_EMAIL"].ToString(),
                Sjb_bill_fax = row["SJB_BILL_FAX"] == DBNull.Value ? string.Empty : row["SJB_BILL_FAX"].ToString(),
                Sjb_bill_id = row["SJB_BILL_ID"] == DBNull.Value ? string.Empty : row["SJB_BILL_ID"].ToString(),
                Sjb_bill_idtype = row["SJB_BILL_IDTYPE"] == DBNull.Value ? string.Empty : row["SJB_BILL_IDTYPE"].ToString(),
                Sjb_bill_mobileno = row["SJB_BILL_MOBILENO"] == DBNull.Value ? string.Empty : row["SJB_BILL_MOBILENO"].ToString(),
                Sjb_bill_phone_no = row["SJB_BILL_PHONE_NO"] == DBNull.Value ? string.Empty : row["SJB_BILL_PHONE_NO"].ToString(),
                Sjb_bill_town = row["SJB_BILL_TOWN"] == DBNull.Value ? string.Empty : row["SJB_BILL_TOWN"].ToString(),
                Sjb_channel = row["SJB_CHANNEL"] == DBNull.Value ? string.Empty : row["SJB_CHANNEL"].ToString(),
                Sjb_company = row["SJB_COMPANY"] == DBNull.Value ? string.Empty : row["SJB_COMPANY"].ToString(),
                Sjb_cont_address1 = row["SJB_CONT_ADDRESS1"] == DBNull.Value ? string.Empty : row["SJB_CONT_ADDRESS1"].ToString(),
                Sjb_cont_address2 = row["SJB_CONT_ADDRESS2"] == DBNull.Value ? string.Empty : row["SJB_CONT_ADDRESS2"].ToString(),
                Sjb_cont_person = row["SJB_CONT_PERSON"] == DBNull.Value ? string.Empty : row["SJB_CONT_PERSON"].ToString(),
                Sjb_cont_phone_no = row["SJB_CONT_PHONE_NO"] == DBNull.Value ? string.Empty : row["SJB_CONT_PHONE_NO"].ToString(),
                Sjb_createby = row["SJB_CREATEBY"] == DBNull.Value ? string.Empty : row["SJB_CREATEBY"].ToString(),
                Sjb_createwhen = row["SJB_CREATEWHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_CREATEWHEN"]),
                Sjb_cusagreementno = row["SJB_CUSAGREEMENTNO"] == DBNull.Value ? string.Empty : row["SJB_CUSAGREEMENTNO"].ToString(),
                Sjb_cusexpectdate = row["SJB_CUSEXPECTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_CUSEXPECTDATE"]),
                Sjb_custcode = row["SJB_CUSTCODE"] == DBNull.Value ? string.Empty : row["SJB_CUSTCODE"].ToString(),
                Sjb_custname = row["SJB_CUSTNAME"] == DBNull.Value ? string.Empty : row["SJB_CUSTNAME"].ToString(),
                Sjb_custtitle = row["SJB_CUSTTITLE"] == DBNull.Value ? string.Empty : row["SJB_CUSTTITLE"].ToString(),
                Sjb_date = row["SJB_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_DATE"]),
                Sjb_email = row["SJB_EMAIL"] == DBNull.Value ? string.Empty : row["SJB_EMAIL"].ToString(),
                Sjb_enddate = row["SJB_ENDDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_ENDDATE"]),
                Sjb_faxno = row["SJB_FAXNO"] == DBNull.Value ? string.Empty : row["SJB_FAXNO"].ToString(),
                Sjb_idno = row["SJB_IDNO"] == DBNull.Value ? string.Empty : row["SJB_IDNO"].ToString(),
                Sjb_idtype = row["SJB_IDTYPE"] == DBNull.Value ? string.Empty : row["SJB_IDTYPE"].ToString(),
                Sjb_inform_contact = row["SJB_INFORM_CONTACT"] == DBNull.Value ? string.Empty : row["SJB_INFORM_CONTACT"].ToString(),
                Sjb_inform_location = row["SJB_INFORM_LOCATION"] == DBNull.Value ? string.Empty : row["SJB_INFORM_LOCATION"].ToString(),
                Sjb_inform_person = row["SJB_INFORM_PERSON"] == DBNull.Value ? string.Empty : row["SJB_INFORM_PERSON"].ToString(),
                Sjb_insucompany = row["SJB_INSUCOMPANY"] == DBNull.Value ? string.Empty : row["SJB_INSUCOMPANY"].ToString(),
                Sjb_isagreement = row["SJB_ISAGREEMENT"] == DBNull.Value ? string.Empty : row["SJB_ISAGREEMENT"].ToString(),
                Sjb_isexternalitem = row["SJB_ISEXTERNALITEM"] == DBNull.Value ? false : Convert.ToBoolean(row["SJB_ISEXTERNALITEM"]),
                Sjb_isprejob = row["SJB_ISPREJOB"] == DBNull.Value ? false : Convert.ToBoolean(row["SJB_ISPREJOB"]),
                Sjb_issrn = row["SJB_ISSRN"] == DBNull.Value ? false : Convert.ToBoolean(row["SJB_ISSRN"]),
                Sjb_itemtype = row["SJB_ITEMTYPE"] == DBNull.Value ? string.Empty : row["SJB_ITEMTYPE"].ToString(),
                Sjb_jobcategory = row["SJB_JOBCATEGORY"] == DBNull.Value ? string.Empty : row["SJB_JOBCATEGORY"].ToString(),
                Sjb_jobclosecode = row["SJB_JOBCLOSECODE"] == DBNull.Value ? string.Empty : row["SJB_JOBCLOSECODE"].ToString(),
                Sjb_jobclosedesc = row["SJB_JOBCLOSEDESC"] == DBNull.Value ? string.Empty : row["SJB_JOBCLOSEDESC"].ToString(),
                Sjb_jobcloseremarks = row["SJB_JOBCLOSEREMARKS"] == DBNull.Value ? string.Empty : row["SJB_JOBCLOSEREMARKS"].ToString(),
                Sjb_jobconfdate = row["SJB_JOBCONFDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_JOBCONFDATE"]),
                Sjb_jobno = row["SJB_JOBNO"] == DBNull.Value ? string.Empty : row["SJB_JOBNO"].ToString(),
                Sjb_jobstage = row["SJB_JOBSTAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SJB_JOBSTAGE"]),
                Sjb_jobsubtype = row["SJB_JOBSUBTYPE"] == DBNull.Value ? string.Empty : row["SJB_JOBSUBTYPE"].ToString(),
                Sjb_jobtype = row["SJB_JOBTYPE"] == DBNull.Value ? string.Empty : row["SJB_JOBTYPE"].ToString(),
                Sjb_job_remarks = row["SJB_JOB_REMARKS"] == DBNull.Value ? string.Empty : row["SJB_JOB_REMARKS"].ToString(),
                Sjb_lastmodifyby = row["SJB_LASTMODIFYBY"] == DBNull.Value ? string.Empty : row["SJB_LASTMODIFYBY"].ToString(),
                Sjb_lastmodifywhen = row["SJB_LASTMODIFYWHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_LASTMODIFYWHEN"]),
                Sjb_lastprintby = row["SJB_LASTPRINTBY"] == DBNull.Value ? string.Empty : row["SJB_LASTPRINTBY"].ToString(),
                Sjb_location = row["SJB_LOCATION"] == DBNull.Value ? string.Empty : row["SJB_LOCATION"].ToString(),
                Sjb_mainjobno = row["SJB_MAINJOBNO"] == DBNull.Value ? string.Empty : row["SJB_MAINJOBNO"].ToString(),
                Sjb_mainreqlocation = row["SJB_MAINREQLOCATION"] == DBNull.Value ? string.Empty : row["SJB_MAINREQLOCATION"].ToString(),
                Sjb_mainreqno = row["SJB_MAINREQNO"] == DBNull.Value ? string.Empty : row["SJB_MAINREQNO"].ToString(),
                Sjb_manualref = row["SJB_MANUALREF"] == DBNull.Value ? string.Empty : row["SJB_MANUALREF"].ToString(),
                Sjb_mobino = row["SJB_MOBINO"] == DBNull.Value ? string.Empty : row["SJB_MOBINO"].ToString(),
                Sjb_msn_no = row["SJB_MSN_NO"] == DBNull.Value ? string.Empty : row["SJB_MSN_NO"].ToString(),
                Sjb_noofprint = row["SJB_NOOFPRINT"] == DBNull.Value ? 0 : Convert.ToInt16(row["SJB_NOOFPRINT"]),
                Sjb_orderno = row["SJB_ORDERNO"] == DBNull.Value ? string.Empty : row["SJB_ORDERNO"].ToString(),
                Sjb_otherref = row["SJB_OTHERREF"] == DBNull.Value ? string.Empty : row["SJB_OTHERREF"].ToString(),
                Sjb_phoneno = row["SJB_PHONENO"] == DBNull.Value ? string.Empty : row["SJB_PHONENO"].ToString(),
                Sjb_profitcenter = row["SJB_PROFITCENTER"] == DBNull.Value ? string.Empty : row["SJB_PROFITCENTER"].ToString(),
                Sjb_prority = row["SJB_PRORITY"] == DBNull.Value ? string.Empty : row["SJB_PRORITY"].ToString(),
                Sjb_quotation = row["SJB_QUOTATION"] == DBNull.Value ? string.Empty : row["SJB_QUOTATION"].ToString(),
                Sjb_redojob = row["SJB_REDOJOB"] == DBNull.Value ? string.Empty : row["SJB_REDOJOB"].ToString(),
                Sjb_remarks = row["SJB_REMARKS"] == DBNull.Value ? string.Empty : row["SJB_REMARKS"].ToString(),
                Sjb_reqno = row["SJB_REQNO"] == DBNull.Value ? string.Empty : row["SJB_REQNO"].ToString(),
                Sjb_startdate = row["SJB_STARTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_STARTDATE"]),
                Sjb_status = row["SJB_STATUS"] == DBNull.Value ? string.Empty : row["SJB_STATUS"].ToString(),
                Sjb_substage = row["SJB_SUBSTAGE"] == DBNull.Value ? string.Empty : row["SJB_SUBSTAGE"].ToString(),
                Sjb_techfinishdate = row["SJB_TECHFINISHDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_TECHFINISHDATE"]),
                Sjb_techstartdate = row["SJB_TECHSTARTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_TECHSTARTDATE"]),
                Sjb_tech_remarks = row["SJB_TECH_REMARKS"] == DBNull.Value ? string.Empty : row["SJB_TECH_REMARKS"].ToString(),
                Sjb_town = row["SJB_TOWN"] == DBNull.Value ? string.Empty : row["SJB_TOWN"].ToString(),
                Sjb_transferby = row["SJB_TRANSFERBY"] == DBNull.Value ? string.Empty : row["SJB_TRANSFERBY"].ToString(),
                Sjb_transferdate = row["SJB_TRANSFERDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_TRANSFERDATE"])

            };
        }
    }
}
