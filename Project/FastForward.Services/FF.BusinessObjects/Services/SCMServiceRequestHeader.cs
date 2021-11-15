using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SCMServiceRequestHeader
    {
        #region Private Members
        private string _srh_address1;
        private string _srh_address2;
        private string _srh_agreementno;
        private Boolean _srh_allowedit;
        private DateTime _srh_aodissuedate;
        private string _srh_aodissueloca;
        private string _srh_aodissueno;
        private string _srh_aodrecno;
        private string _srh_bill_address1;
        private string _srh_bill_address2;
        private string _srh_bill_address3;
        private string _srh_bill_cus_code;
        private string _srh_bill_cus_name;
        private string _srh_bill_cus_title;
        private string _srh_bill_email;
        private string _srh_bill_fax;
        private string _srh_bill_id;
        private string _srh_bill_idtype;
        private string _srh_bill_phone_no;
        private string _srh_bill_town;
        private string _srh_channel;
        private DateTime _srh_commencementdate;
        private string _srh_company;
        private string _srh_cont_address;
        private string _srh_cont_person;
        private string _srh_cont_phone_no;
        private string _srh_createby;
        private DateTime _srh_createwhen;
        private string _srh_cusagreementno;
        private DateTime _srh_cusexpectdate;
        private string _srh_custcode;
        private string _srh_custname;
        private string _srh_custtitle;
        private DateTime _srh_date;
        private string _srh_email;
        private DateTime _srh_expiredate;
        private string _srh_idno;
        private string _srh_idtype;
        private string _srh_inform_contact;
        private string _srh_inform_location;
        private string _srh_inform_person;
        private string _srh_insucompany;
        private Boolean _srh_isagreement;
        private Boolean _srh_isexternalitem;
        private string _srh_lastmodifyby;
        private DateTime _srh_lastmodifywhen;
        private string _srh_location;
        private string _srh_mainreqlocation;
        private string _srh_mainreqno;
        private string _srh_manualref;
        private string _srh_otherref;
        private string _srh_phoneno;
        private string _srh_priority;
        private string _srh_profitcenter;
        private string _srh_remarks;
        private DateTime _srh_renewaldate;
        private string _srh_reqcategory;
        private DateTime _srh_reqtime;
        private string _srh_reqtype;
        private string _srh_req_no;
        private string _srh_req_remarks;
        private string _srh_req_sub_type;
        private string _srh_status;
        private string _srh_tech_remarks;
        private string _srh_town;
        #endregion

        public string Srh_address1
        {
            get { return _srh_address1; }
            set { _srh_address1 = value; }
        }
        public string Srh_address2
        {
            get { return _srh_address2; }
            set { _srh_address2 = value; }
        }
        public string Srh_agreementno
        {
            get { return _srh_agreementno; }
            set { _srh_agreementno = value; }
        }
        public Boolean Srh_allowedit
        {
            get { return _srh_allowedit; }
            set { _srh_allowedit = value; }
        }
        public DateTime Srh_aodissuedate
        {
            get { return _srh_aodissuedate; }
            set { _srh_aodissuedate = value; }
        }
        public string Srh_aodissueloca
        {
            get { return _srh_aodissueloca; }
            set { _srh_aodissueloca = value; }
        }
        public string Srh_aodissueno
        {
            get { return _srh_aodissueno; }
            set { _srh_aodissueno = value; }
        }
        public string Srh_aodrecno
        {
            get { return _srh_aodrecno; }
            set { _srh_aodrecno = value; }
        }
        public string Srh_bill_address1
        {
            get { return _srh_bill_address1; }
            set { _srh_bill_address1 = value; }
        }
        public string Srh_bill_address2
        {
            get { return _srh_bill_address2; }
            set { _srh_bill_address2 = value; }
        }
        public string Srh_bill_address3
        {
            get { return _srh_bill_address3; }
            set { _srh_bill_address3 = value; }
        }
        public string Srh_bill_cus_code
        {
            get { return _srh_bill_cus_code; }
            set { _srh_bill_cus_code = value; }
        }
        public string Srh_bill_cus_name
        {
            get { return _srh_bill_cus_name; }
            set { _srh_bill_cus_name = value; }
        }
        public string Srh_bill_cus_title
        {
            get { return _srh_bill_cus_title; }
            set { _srh_bill_cus_title = value; }
        }
        public string Srh_bill_email
        {
            get { return _srh_bill_email; }
            set { _srh_bill_email = value; }
        }
        public string Srh_bill_fax
        {
            get { return _srh_bill_fax; }
            set { _srh_bill_fax = value; }
        }
        public string Srh_bill_id
        {
            get { return _srh_bill_id; }
            set { _srh_bill_id = value; }
        }
        public string Srh_bill_idtype
        {
            get { return _srh_bill_idtype; }
            set { _srh_bill_idtype = value; }
        }
        public string Srh_bill_phone_no
        {
            get { return _srh_bill_phone_no; }
            set { _srh_bill_phone_no = value; }
        }
        public string Srh_bill_town
        {
            get { return _srh_bill_town; }
            set { _srh_bill_town = value; }
        }
        public string Srh_channel
        {
            get { return _srh_channel; }
            set { _srh_channel = value; }
        }
        public DateTime Srh_commencementdate
        {
            get { return _srh_commencementdate; }
            set { _srh_commencementdate = value; }
        }
        public string Srh_company
        {
            get { return _srh_company; }
            set { _srh_company = value; }
        }
        public string Srh_cont_address
        {
            get { return _srh_cont_address; }
            set { _srh_cont_address = value; }
        }
        public string Srh_cont_person
        {
            get { return _srh_cont_person; }
            set { _srh_cont_person = value; }
        }
        public string Srh_cont_phone_no
        {
            get { return _srh_cont_phone_no; }
            set { _srh_cont_phone_no = value; }
        }
        public string Srh_createby
        {
            get { return _srh_createby; }
            set { _srh_createby = value; }
        }
        public DateTime Srh_createwhen
        {
            get { return _srh_createwhen; }
            set { _srh_createwhen = value; }
        }
        public string Srh_cusagreementno
        {
            get { return _srh_cusagreementno; }
            set { _srh_cusagreementno = value; }
        }
        public DateTime Srh_cusexpectdate
        {
            get { return _srh_cusexpectdate; }
            set { _srh_cusexpectdate = value; }
        }
        public string Srh_custcode
        {
            get { return _srh_custcode; }
            set { _srh_custcode = value; }
        }
        public string Srh_custname
        {
            get { return _srh_custname; }
            set { _srh_custname = value; }
        }
        public string Srh_custtitle
        {
            get { return _srh_custtitle; }
            set { _srh_custtitle = value; }
        }
        public DateTime Srh_date
        {
            get { return _srh_date; }
            set { _srh_date = value; }
        }
        public string Srh_email
        {
            get { return _srh_email; }
            set { _srh_email = value; }
        }
        public DateTime Srh_expiredate
        {
            get { return _srh_expiredate; }
            set { _srh_expiredate = value; }
        }
        public string Srh_idno
        {
            get { return _srh_idno; }
            set { _srh_idno = value; }
        }
        public string Srh_idtype
        {
            get { return _srh_idtype; }
            set { _srh_idtype = value; }
        }
        public string Srh_inform_contact
        {
            get { return _srh_inform_contact; }
            set { _srh_inform_contact = value; }
        }
        public string Srh_inform_location
        {
            get { return _srh_inform_location; }
            set { _srh_inform_location = value; }
        }
        public string Srh_inform_person
        {
            get { return _srh_inform_person; }
            set { _srh_inform_person = value; }
        }
        public string Srh_insucompany
        {
            get { return _srh_insucompany; }
            set { _srh_insucompany = value; }
        }
        public Boolean Srh_isagreement
        {
            get { return _srh_isagreement; }
            set { _srh_isagreement = value; }
        }
        public Boolean Srh_isexternalitem
        {
            get { return _srh_isexternalitem; }
            set { _srh_isexternalitem = value; }
        }
        public string Srh_lastmodifyby
        {
            get { return _srh_lastmodifyby; }
            set { _srh_lastmodifyby = value; }
        }
        public DateTime Srh_lastmodifywhen
        {
            get { return _srh_lastmodifywhen; }
            set { _srh_lastmodifywhen = value; }
        }
        public string Srh_location
        {
            get { return _srh_location; }
            set { _srh_location = value; }
        }
        public string Srh_mainreqlocation
        {
            get { return _srh_mainreqlocation; }
            set { _srh_mainreqlocation = value; }
        }
        public string Srh_mainreqno
        {
            get { return _srh_mainreqno; }
            set { _srh_mainreqno = value; }
        }
        public string Srh_manualref
        {
            get { return _srh_manualref; }
            set { _srh_manualref = value; }
        }
        public string Srh_otherref
        {
            get { return _srh_otherref; }
            set { _srh_otherref = value; }
        }
        public string Srh_phoneno
        {
            get { return _srh_phoneno; }
            set { _srh_phoneno = value; }
        }
        public string Srh_priority
        {
            get { return _srh_priority; }
            set { _srh_priority = value; }
        }
        public string Srh_profitcenter
        {
            get { return _srh_profitcenter; }
            set { _srh_profitcenter = value; }
        }
        public string Srh_remarks
        {
            get { return _srh_remarks; }
            set { _srh_remarks = value; }
        }
        public DateTime Srh_renewaldate
        {
            get { return _srh_renewaldate; }
            set { _srh_renewaldate = value; }
        }
        public string Srh_reqcategory
        {
            get { return _srh_reqcategory; }
            set { _srh_reqcategory = value; }
        }
        public DateTime Srh_reqtime
        {
            get { return _srh_reqtime; }
            set { _srh_reqtime = value; }
        }
        public string Srh_reqtype
        {
            get { return _srh_reqtype; }
            set { _srh_reqtype = value; }
        }
        public string Srh_req_no
        {
            get { return _srh_req_no; }
            set { _srh_req_no = value; }
        }
        public string Srh_req_remarks
        {
            get { return _srh_req_remarks; }
            set { _srh_req_remarks = value; }
        }
        public string Srh_req_sub_type
        {
            get { return _srh_req_sub_type; }
            set { _srh_req_sub_type = value; }
        }
        public string Srh_status
        {
            get { return _srh_status; }
            set { _srh_status = value; }
        }
        public string Srh_tech_remarks
        {
            get { return _srh_tech_remarks; }
            set { _srh_tech_remarks = value; }
        }
        public string Srh_town
        {
            get { return _srh_town; }
            set { _srh_town = value; }
        }
       
        public static SCMServiceRequestHeader Converter(DataRow row)
        {
            return new SCMServiceRequestHeader
            {
                Srh_address1 = row["SRH_ADDRESS1"] == DBNull.Value ? string.Empty : row["SRH_ADDRESS1"].ToString(),
                Srh_address2 = row["SRH_ADDRESS2"] == DBNull.Value ? string.Empty : row["SRH_ADDRESS2"].ToString(),
                Srh_agreementno = row["SRH_AGREEMENTNO"] == DBNull.Value ? string.Empty : row["SRH_AGREEMENTNO"].ToString(),
                Srh_allowedit = row["SRH_ALLOWEDIT"] == DBNull.Value ? false : Convert.ToBoolean(row["SRH_ALLOWEDIT"]),
                Srh_aodissuedate = row["SRH_AODISSUEDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRH_AODISSUEDATE"]),
                Srh_aodissueloca = row["SRH_AODISSUELOCA"] == DBNull.Value ? string.Empty : row["SRH_AODISSUELOCA"].ToString(),
                Srh_aodissueno = row["SRH_AODISSUENO"] == DBNull.Value ? string.Empty : row["SRH_AODISSUENO"].ToString(),
                Srh_aodrecno = row["SRH_AODRECNO"] == DBNull.Value ? string.Empty : row["SRH_AODRECNO"].ToString(),
                Srh_bill_address1 = row["SRH_BILL_ADDRESS1"] == DBNull.Value ? string.Empty : row["SRH_BILL_ADDRESS1"].ToString(),
                Srh_bill_address2 = row["SRH_BILL_ADDRESS2"] == DBNull.Value ? string.Empty : row["SRH_BILL_ADDRESS2"].ToString(),
                Srh_bill_address3 = row["SRH_BILL_ADDRESS3"] == DBNull.Value ? string.Empty : row["SRH_BILL_ADDRESS3"].ToString(),
                Srh_bill_cus_code = row["SRH_BILL_CUS_CODE"] == DBNull.Value ? string.Empty : row["SRH_BILL_CUS_CODE"].ToString(),
                Srh_bill_cus_name = row["SRH_BILL_CUS_NAME"] == DBNull.Value ? string.Empty : row["SRH_BILL_CUS_NAME"].ToString(),
                Srh_bill_cus_title = row["SRH_BILL_CUS_TITLE"] == DBNull.Value ? string.Empty : row["SRH_BILL_CUS_TITLE"].ToString(),
                Srh_bill_email = row["SRH_BILL_EMAIL"] == DBNull.Value ? string.Empty : row["SRH_BILL_EMAIL"].ToString(),
                Srh_bill_fax = row["SRH_BILL_FAX"] == DBNull.Value ? string.Empty : row["SRH_BILL_FAX"].ToString(),
                Srh_bill_id = row["SRH_BILL_ID"] == DBNull.Value ? string.Empty : row["SRH_BILL_ID"].ToString(),
                Srh_bill_idtype = row["SRH_BILL_IDTYPE"] == DBNull.Value ? string.Empty : row["SRH_BILL_IDTYPE"].ToString(),
                Srh_bill_phone_no = row["SRH_BILL_PHONE_NO"] == DBNull.Value ? string.Empty : row["SRH_BILL_PHONE_NO"].ToString(),
                Srh_bill_town = row["SRH_BILL_TOWN"] == DBNull.Value ? string.Empty : row["SRH_BILL_TOWN"].ToString(),
                Srh_channel = row["SRH_CHANNEL"] == DBNull.Value ? string.Empty : row["SRH_CHANNEL"].ToString(),
                Srh_commencementdate = row["SRH_COMMENCEMENTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRH_COMMENCEMENTDATE"]),
                Srh_company = row["SRH_COMPANY"] == DBNull.Value ? string.Empty : row["SRH_COMPANY"].ToString(),
                Srh_cont_address = row["SRH_CONT_ADDRESS"] == DBNull.Value ? string.Empty : row["SRH_CONT_ADDRESS"].ToString(),
                Srh_cont_person = row["SRH_CONT_PERSON"] == DBNull.Value ? string.Empty : row["SRH_CONT_PERSON"].ToString(),
                Srh_cont_phone_no = row["SRH_CONT_PHONE_NO"] == DBNull.Value ? string.Empty : row["SRH_CONT_PHONE_NO"].ToString(),
                Srh_createby = row["SRH_CREATEBY"] == DBNull.Value ? string.Empty : row["SRH_CREATEBY"].ToString(),
                Srh_createwhen = row["SRH_CREATEWHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRH_CREATEWHEN"]),
                Srh_cusagreementno = row["SRH_CUSAGREEMENTNO"] == DBNull.Value ? string.Empty : row["SRH_CUSAGREEMENTNO"].ToString(),
                Srh_cusexpectdate = row["SRH_CUSEXPECTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRH_CUSEXPECTDATE"]),
                Srh_custcode = row["SRH_CUSTCODE"] == DBNull.Value ? string.Empty : row["SRH_CUSTCODE"].ToString(),
                Srh_custname = row["SRH_CUSTNAME"] == DBNull.Value ? string.Empty : row["SRH_CUSTNAME"].ToString(),
                Srh_custtitle = row["SRH_CUSTTITLE"] == DBNull.Value ? string.Empty : row["SRH_CUSTTITLE"].ToString(),
                Srh_date = row["SRH_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRH_DATE"]),
                Srh_email = row["SRH_EMAIL"] == DBNull.Value ? string.Empty : row["SRH_EMAIL"].ToString(),
                Srh_expiredate = row["SRH_EXPIREDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRH_EXPIREDATE"]),
                Srh_idno = row["SRH_IDNO"] == DBNull.Value ? string.Empty : row["SRH_IDNO"].ToString(),
                Srh_idtype = row["SRH_IDTYPE"] == DBNull.Value ? string.Empty : row["SRH_IDTYPE"].ToString(),
                Srh_inform_contact = row["SRH_INFORM_CONTACT"] == DBNull.Value ? string.Empty : row["SRH_INFORM_CONTACT"].ToString(),
                Srh_inform_location = row["SRH_INFORM_LOCATION"] == DBNull.Value ? string.Empty : row["SRH_INFORM_LOCATION"].ToString(),
                Srh_inform_person = row["SRH_INFORM_PERSON"] == DBNull.Value ? string.Empty : row["SRH_INFORM_PERSON"].ToString(),
                Srh_insucompany = row["SRH_INSUCOMPANY"] == DBNull.Value ? string.Empty : row["SRH_INSUCOMPANY"].ToString(),
                Srh_isagreement = row["SRH_ISAGREEMENT"] == DBNull.Value ? false : Convert.ToBoolean(row["SRH_ISAGREEMENT"]),
                Srh_isexternalitem = row["SRH_ISEXTERNALITEM"] == DBNull.Value ? false : Convert.ToBoolean(row["SRH_ISEXTERNALITEM"]),
                Srh_lastmodifyby = row["SRH_LASTMODIFYBY"] == DBNull.Value ? string.Empty : row["SRH_LASTMODIFYBY"].ToString(),
                Srh_lastmodifywhen = row["SRH_LASTMODIFYWHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRH_LASTMODIFYWHEN"]),
                Srh_location = row["SRH_LOCATION"] == DBNull.Value ? string.Empty : row["SRH_LOCATION"].ToString(),
                Srh_mainreqlocation = row["SRH_MAINREQLOCATION"] == DBNull.Value ? string.Empty : row["SRH_MAINREQLOCATION"].ToString(),
                Srh_mainreqno = row["SRH_MAINREQNO"] == DBNull.Value ? string.Empty : row["SRH_MAINREQNO"].ToString(),
                Srh_manualref = row["SRH_MANUALREF"] == DBNull.Value ? string.Empty : row["SRH_MANUALREF"].ToString(),
                Srh_otherref = row["SRH_OTHERREF"] == DBNull.Value ? string.Empty : row["SRH_OTHERREF"].ToString(),
                Srh_phoneno = row["SRH_PHONENO"] == DBNull.Value ? string.Empty : row["SRH_PHONENO"].ToString(),
                Srh_priority = row["SRH_PRIORITY"] == DBNull.Value ? string.Empty : row["SRH_PRIORITY"].ToString(),
                Srh_profitcenter = row["SRH_PROFITCENTER"] == DBNull.Value ? string.Empty : row["SRH_PROFITCENTER"].ToString(),
                Srh_remarks = row["SRH_REMARKS"] == DBNull.Value ? string.Empty : row["SRH_REMARKS"].ToString(),
                Srh_renewaldate = row["SRH_RENEWALDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRH_RENEWALDATE"]),
                Srh_reqcategory = row["SRH_REQCATEGORY"] == DBNull.Value ? string.Empty : row["SRH_REQCATEGORY"].ToString(),
                Srh_reqtime = row["SRH_REQTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRH_REQTIME"]),
                Srh_reqtype = row["SRH_REQTYPE"] == DBNull.Value ? string.Empty : row["SRH_REQTYPE"].ToString(),
                Srh_req_no = row["SRH_REQ_NO"] == DBNull.Value ? string.Empty : row["SRH_REQ_NO"].ToString(),
                Srh_req_remarks = row["SRH_REQ_REMARKS"] == DBNull.Value ? string.Empty : row["SRH_REQ_REMARKS"].ToString(),
                Srh_req_sub_type = row["SRH_REQ_SUB_TYPE"] == DBNull.Value ? string.Empty : row["SRH_REQ_SUB_TYPE"].ToString(),
                Srh_status = row["SRH_STATUS"] == DBNull.Value ? string.Empty : row["SRH_STATUS"].ToString(),
                Srh_tech_remarks = row["SRH_TECH_REMARKS"] == DBNull.Value ? string.Empty : row["SRH_TECH_REMARKS"].ToString(),
                Srh_town = row["SRH_TOWN"] == DBNull.Value ? string.Empty : row["SRH_TOWN"].ToString()

            };
        }

    }
}
