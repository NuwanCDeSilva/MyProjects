using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public  class SCMServiceJobDetails
    {
        #region Private Members
        private Boolean _jbd_availabilty;
        private string _jbd_brand;
        private string _jbd_chg_warrremarks;
        private DateTime _jbd_chg_warrstdate;
        private string _jbd_customernotes;
        private DateTime _jbd_date_pur;
        private string _jbd_invoiceno;
        private Boolean _jbd_isinsurance;
        private Boolean _jbd_isstockupdate;
        private Boolean _jbd_issued;
        private Boolean _jbd_iswrn;
        private string _jbd_itemcode;
        private decimal _jbd_itemcost;
        private string _jbd_itemdesc;
        private string _jbd_itemmfc;
        private string _jbd_itemstatus;
        private string _jbd_itemtype;
        private Int32 _jbd_jobline;
        private string _jbd_jobno;
        private DateTime _jbd_lastwarrstdate;
        private string _jbd_mainitemcode;
        private string _jbd_mainitemmfc;
        private string _jbd_mainitemserial;
        private string _jbd_mainitemwarranty;
        private string _jbd_mainjobno;
        private string _jbd_mainreqlocation;
        private string _jbd_mainreqno;
        private Int32 _jbd_milage;
        private string _jbd_model;
        private string _jbd_msnno;
        private Boolean _jbd_needgatepass;
        private Boolean _jbd_onloan;
        private string _jbd_regno;
        private string _jbd_reqitemtype;
        private Int32 _jbd_reqline;
        private string _jbd_reqno;
        private string _jbd_req_type;
        private Boolean _jbd_sentwcn;
        private string _jbd_serial1;
        private string _jbd_serial2;
        private string _jbd_serlocachr;
        private int _jbd_ser_term;
        private string _jbd_usejob;
        private string _jbd_waraamd_by;
        private string _jbd_waraamd_seq;
        private DateTime _jbd_waraamd_when;
        private string _jbd_warranty;
        private Boolean _jbd_warranty_status;
        private Int32 _jbd_warrperiod;
        private Boolean _jbd_warrreplace;
        private DateTime _jbd_warrstartdate;
        #endregion

        public Boolean Jbd_availabilty
        {
            get { return _jbd_availabilty; }
            set { _jbd_availabilty = value; }
        }
        public string Jbd_brand
        {
            get { return _jbd_brand; }
            set { _jbd_brand = value; }
        }
        public string Jbd_chg_warrremarks
        {
            get { return _jbd_chg_warrremarks; }
            set { _jbd_chg_warrremarks = value; }
        }
        public DateTime Jbd_chg_warrstdate
        {
            get { return _jbd_chg_warrstdate; }
            set { _jbd_chg_warrstdate = value; }
        }
        public string Jbd_customernotes
        {
            get { return _jbd_customernotes; }
            set { _jbd_customernotes = value; }
        }
        public DateTime Jbd_date_pur
        {
            get { return _jbd_date_pur; }
            set { _jbd_date_pur = value; }
        }
        public string Jbd_invoiceno
        {
            get { return _jbd_invoiceno; }
            set { _jbd_invoiceno = value; }
        }
        public Boolean Jbd_isinsurance
        {
            get { return _jbd_isinsurance; }
            set { _jbd_isinsurance = value; }
        }
        public Boolean Jbd_isstockupdate
        {
            get { return _jbd_isstockupdate; }
            set { _jbd_isstockupdate = value; }
        }
        public Boolean Jbd_issued
        {
            get { return _jbd_issued; }
            set { _jbd_issued = value; }
        }
        public Boolean Jbd_iswrn
        {
            get { return _jbd_iswrn; }
            set { _jbd_iswrn = value; }
        }
        public string Jbd_itemcode
        {
            get { return _jbd_itemcode; }
            set { _jbd_itemcode = value; }
        }
        public decimal Jbd_itemcost
        {
            get { return _jbd_itemcost; }
            set { _jbd_itemcost = value; }
        }
        public string Jbd_itemdesc
        {
            get { return _jbd_itemdesc; }
            set { _jbd_itemdesc = value; }
        }
        public string Jbd_itemmfc
        {
            get { return _jbd_itemmfc; }
            set { _jbd_itemmfc = value; }
        }
        public string Jbd_itemstatus
        {
            get { return _jbd_itemstatus; }
            set { _jbd_itemstatus = value; }
        }
        public string Jbd_itemtype
        {
            get { return _jbd_itemtype; }
            set { _jbd_itemtype = value; }
        }
        public Int32 Jbd_jobline
        {
            get { return _jbd_jobline; }
            set { _jbd_jobline = value; }
        }
        public string Jbd_jobno
        {
            get { return _jbd_jobno; }
            set { _jbd_jobno = value; }
        }
        public DateTime Jbd_lastwarrstdate
        {
            get { return _jbd_lastwarrstdate; }
            set { _jbd_lastwarrstdate = value; }
        }
        public string Jbd_mainitemcode
        {
            get { return _jbd_mainitemcode; }
            set { _jbd_mainitemcode = value; }
        }
        public string Jbd_mainitemmfc
        {
            get { return _jbd_mainitemmfc; }
            set { _jbd_mainitemmfc = value; }
        }
        public string Jbd_mainitemserial
        {
            get { return _jbd_mainitemserial; }
            set { _jbd_mainitemserial = value; }
        }
        public string Jbd_mainitemwarranty
        {
            get { return _jbd_mainitemwarranty; }
            set { _jbd_mainitemwarranty = value; }
        }
        public string Jbd_mainjobno
        {
            get { return _jbd_mainjobno; }
            set { _jbd_mainjobno = value; }
        }
        public string Jbd_mainreqlocation
        {
            get { return _jbd_mainreqlocation; }
            set { _jbd_mainreqlocation = value; }
        }
        public string Jbd_mainreqno
        {
            get { return _jbd_mainreqno; }
            set { _jbd_mainreqno = value; }
        }
        public Int32 Jbd_milage
        {
            get { return _jbd_milage; }
            set { _jbd_milage = value; }
        }
        public string Jbd_model
        {
            get { return _jbd_model; }
            set { _jbd_model = value; }
        }
        public string Jbd_msnno
        {
            get { return _jbd_msnno; }
            set { _jbd_msnno = value; }
        }
        public Boolean Jbd_needgatepass
        {
            get { return _jbd_needgatepass; }
            set { _jbd_needgatepass = value; }
        }
        public Boolean Jbd_onloan
        {
            get { return _jbd_onloan; }
            set { _jbd_onloan = value; }
        }
        public string Jbd_regno
        {
            get { return _jbd_regno; }
            set { _jbd_regno = value; }
        }
        public string Jbd_reqitemtype
        {
            get { return _jbd_reqitemtype; }
            set { _jbd_reqitemtype = value; }
        }
        public Int32 Jbd_reqline
        {
            get { return _jbd_reqline; }
            set { _jbd_reqline = value; }
        }
        public string Jbd_reqno
        {
            get { return _jbd_reqno; }
            set { _jbd_reqno = value; }
        }
        public string Jbd_req_type
        {
            get { return _jbd_req_type; }
            set { _jbd_req_type = value; }
        }
        public Boolean Jbd_sentwcn
        {
            get { return _jbd_sentwcn; }
            set { _jbd_sentwcn = value; }
        }
        public string Jbd_serial1
        {
            get { return _jbd_serial1; }
            set { _jbd_serial1 = value; }
        }
        public string Jbd_serial2
        {
            get { return _jbd_serial2; }
            set { _jbd_serial2 = value; }
        }
        public string Jbd_serlocachr
        {
            get { return _jbd_serlocachr; }
            set { _jbd_serlocachr = value; }
        }
        public int Jbd_ser_term
        {
            get { return _jbd_ser_term; }
            set { _jbd_ser_term = value; }
        }
        public string Jbd_usejob
        {
            get { return _jbd_usejob; }
            set { _jbd_usejob = value; }
        }
        public string Jbd_waraamd_by
        {
            get { return _jbd_waraamd_by; }
            set { _jbd_waraamd_by = value; }
        }
        public string Jbd_waraamd_seq
        {
            get { return _jbd_waraamd_seq; }
            set { _jbd_waraamd_seq = value; }
        }
        public DateTime Jbd_waraamd_when
        {
            get { return _jbd_waraamd_when; }
            set { _jbd_waraamd_when = value; }
        }
        public string Jbd_warranty
        {
            get { return _jbd_warranty; }
            set { _jbd_warranty = value; }
        }
        public Boolean Jbd_warranty_status
        {
            get { return _jbd_warranty_status; }
            set { _jbd_warranty_status = value; }
        }
        public Int32 Jbd_warrperiod
        {
            get { return _jbd_warrperiod; }
            set { _jbd_warrperiod = value; }
        }
        public Boolean Jbd_warrreplace
        {
            get { return _jbd_warrreplace; }
            set { _jbd_warrreplace = value; }
        }
        public DateTime Jbd_warrstartdate
        {
            get { return _jbd_warrstartdate; }
            set { _jbd_warrstartdate = value; }
        }

        public static SCMServiceJobDetails Converter(DataRow row)
        {
            return new SCMServiceJobDetails
            {
                Jbd_availabilty = row["JBD_AVAILABILTY"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_AVAILABILTY"]),
                Jbd_brand = row["JBD_BRAND"] == DBNull.Value ? string.Empty : row["JBD_BRAND"].ToString(),
                Jbd_chg_warrremarks = row["JBD_CHG_WARRREMARKS"] == DBNull.Value ? string.Empty : row["JBD_CHG_WARRREMARKS"].ToString(),
                Jbd_chg_warrstdate = row["JBD_CHG_WARRSTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_CHG_WARRSTDATE"]),
                Jbd_customernotes = row["JBD_CUSTOMERNOTES"] == DBNull.Value ? string.Empty : row["JBD_CUSTOMERNOTES"].ToString(),
                Jbd_date_pur = row["JBD_DATE_PUR"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_DATE_PUR"]),
                Jbd_invoiceno = row["JBD_INVOICENO"] == DBNull.Value ? string.Empty : row["JBD_INVOICENO"].ToString(),
                Jbd_isinsurance = row["JBD_ISINSURANCE"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ISINSURANCE"]),
                Jbd_isstockupdate = row["JBD_ISSTOCKUPDATE"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ISSTOCKUPDATE"]),
                Jbd_issued = row["JBD_ISSUED"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ISSUED"]),
                Jbd_iswrn = row["JBD_ISWRN"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ISWRN"]),
                Jbd_itemcode = row["JBD_ITEMCODE"] == DBNull.Value ? string.Empty : row["JBD_ITEMCODE"].ToString(),
                Jbd_itemcost = row["JBD_ITEMCOST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_ITEMCOST"]),
                Jbd_itemdesc = row["JBD_ITEMDESC"] == DBNull.Value ? string.Empty : row["JBD_ITEMDESC"].ToString(),
                Jbd_itemmfc = row["JBD_ITEMMFC"] == DBNull.Value ? string.Empty : row["JBD_ITEMMFC"].ToString(),
                Jbd_itemstatus = row["JBD_ITEMSTATUS"] == DBNull.Value ? string.Empty : row["JBD_ITEMSTATUS"].ToString(),
                Jbd_itemtype = row["JBD_ITEMTYPE"] == DBNull.Value ? string.Empty : row["JBD_ITEMTYPE"].ToString(),
                Jbd_jobline = row["JBD_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_JOBLINE"]),
                Jbd_jobno = row["JBD_JOBNO"] == DBNull.Value ? string.Empty : row["JBD_JOBNO"].ToString(),
                Jbd_lastwarrstdate = row["JBD_LASTWARRSTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_LASTWARRSTDATE"]),
                Jbd_mainitemcode = row["JBD_MAINITEMCODE"] == DBNull.Value ? string.Empty : row["JBD_MAINITEMCODE"].ToString(),
                Jbd_mainitemmfc = row["JBD_MAINITEMMFC"] == DBNull.Value ? string.Empty : row["JBD_MAINITEMMFC"].ToString(),
                Jbd_mainitemserial = row["JBD_MAINITEMSERIAL"] == DBNull.Value ? string.Empty : row["JBD_MAINITEMSERIAL"].ToString(),
                Jbd_mainitemwarranty = row["JBD_MAINITEMWARRANTY"] == DBNull.Value ? string.Empty : row["JBD_MAINITEMWARRANTY"].ToString(),
                Jbd_mainjobno = row["JBD_MAINJOBNO"] == DBNull.Value ? string.Empty : row["JBD_MAINJOBNO"].ToString(),
                Jbd_mainreqlocation = row["JBD_MAINREQLOCATION"] == DBNull.Value ? string.Empty : row["JBD_MAINREQLOCATION"].ToString(),
                Jbd_mainreqno = row["JBD_MAINREQNO"] == DBNull.Value ? string.Empty : row["JBD_MAINREQNO"].ToString(),
                Jbd_milage = row["JBD_MILAGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_MILAGE"]),
                Jbd_model = row["JBD_MODEL"] == DBNull.Value ? string.Empty : row["JBD_MODEL"].ToString(),
                Jbd_msnno = row["JBD_MSNNO"] == DBNull.Value ? string.Empty : row["JBD_MSNNO"].ToString(),
                Jbd_needgatepass = row["JBD_NEEDGATEPASS"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_NEEDGATEPASS"]),
                Jbd_onloan = row["JBD_ONLOAN"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ONLOAN"]),
                Jbd_regno = row["JBD_REGNO"] == DBNull.Value ? string.Empty : row["JBD_REGNO"].ToString(),
                Jbd_reqitemtype = row["JBD_REQITEMTYPE"] == DBNull.Value ? string.Empty : row["JBD_REQITEMTYPE"].ToString(),
                Jbd_reqline = row["JBD_REQLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_REQLINE"]),
                Jbd_reqno = row["JBD_REQNO"] == DBNull.Value ? string.Empty : row["JBD_REQNO"].ToString(),
                Jbd_req_type = row["JBD_REQ_TYPE"] == DBNull.Value ? string.Empty : row["JBD_REQ_TYPE"].ToString(),
                Jbd_sentwcn = row["JBD_SENTWCN"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_SENTWCN"]),
                Jbd_serial1 = row["JBD_SERIAL1"] == DBNull.Value ? string.Empty : row["JBD_SERIAL1"].ToString(),
                Jbd_serial2 = row["JBD_SERIAL2"] == DBNull.Value ? string.Empty : row["JBD_SERIAL2"].ToString(),
                Jbd_serlocachr = row["JBD_SERLOCACHR"] == DBNull.Value ? string.Empty : row["JBD_SERLOCACHR"].ToString(),
                Jbd_ser_term = row["JBD_SER_TERM"] == DBNull.Value ? 0 : Convert.ToInt16(row["JBD_SER_TERM"]),
                Jbd_usejob = row["JBD_USEJOB"] == DBNull.Value ? string.Empty : row["JBD_USEJOB"].ToString(),
                Jbd_waraamd_by = row["JBD_WARAAMD_BY"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_BY"].ToString(),
                Jbd_waraamd_seq = row["JBD_WARAAMD_SEQ"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_SEQ"].ToString(),
                Jbd_waraamd_when = row["JBD_WARAAMD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARAAMD_WHEN"]),
                Jbd_warranty = row["JBD_WARRANTY"] == DBNull.Value ? string.Empty : row["JBD_WARRANTY"].ToString(),
                Jbd_warranty_status = row["JBD_WARRANTY_STATUS"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_WARRANTY_STATUS"]),
                Jbd_warrperiod = row["JBD_WARRPERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARRPERIOD"]),
                Jbd_warrreplace = row["JBD_WARRREPLACE"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_WARRREPLACE"]),
                Jbd_warrstartdate = row["JBD_WARRSTARTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARRSTARTDATE"])

            };
        }
    }
}
