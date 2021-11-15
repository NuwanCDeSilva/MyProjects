using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class SCMServiceRequestDetails
    {
        #region Private Members
        private decimal _sri_availabilty;
        private string _sri_brand;
        private string _sri_chg_warrremarks;
        private DateTime _sri_chg_warrstdate;
        private string _sri_customernotes;
        private Boolean _sri_isstockupdate;
        private string _sri_issuedocno;
        private string _sri_itemcode;
        private decimal _sri_itemcost;
        private string _sri_itemdesc;
        private string _sri_itemmfc;
        private string _sri_itemstatus;
        private string _sri_itemtype;
        private string _sri_jobno;
        private DateTime _sri_lastwarrstdate;
        private string _sri_mainitemcode;
        private string _sri_mainitemmfc;
        private string _sri_mainitemserial;
        private string _sri_mainitemwarranty;
        private string _sri_mainjobno;
        private string _sri_mainreqlocation;
        private string _sri_mainreqno;
        private Int32 _sri_milage;
        private string _sri_model;
        private string _sri_msnno;
        private Boolean _sri_onloan;
        private string _sri_regno;
        private string _sri_reqitemtype;
        private Int32 _sri_req_line;
        private string _sri_req_no;
        private string _sri_req_type;
        private Boolean _sri_sentwcn;
        private string _sri_serial1;
        private string _sri_serial2;
        private string _sri_serlocachr;
        private Boolean _sri_usejob;
        private string _sri_warranty;
        private Boolean _sri_warranty_status;
        private Int32 _sri_warrperiod;
        private DateTime _sri_warrstartdate;
        #endregion

        public decimal Sri_availabilty
        {
            get { return _sri_availabilty; }
            set { _sri_availabilty = value; }
        }
        public string Sri_brand
        {
            get { return _sri_brand; }
            set { _sri_brand = value; }
        }
        public string Sri_chg_warrremarks
        {
            get { return _sri_chg_warrremarks; }
            set { _sri_chg_warrremarks = value; }
        }
        public DateTime Sri_chg_warrstdate
        {
            get { return _sri_chg_warrstdate; }
            set { _sri_chg_warrstdate = value; }
        }
        public string Sri_customernotes
        {
            get { return _sri_customernotes; }
            set { _sri_customernotes = value; }
        }
        public Boolean Sri_isstockupdate
        {
            get { return _sri_isstockupdate; }
            set { _sri_isstockupdate = value; }
        }
        public string Sri_issuedocno
        {
            get { return _sri_issuedocno; }
            set { _sri_issuedocno = value; }
        }
        public string Sri_itemcode
        {
            get { return _sri_itemcode; }
            set { _sri_itemcode = value; }
        }
        public decimal Sri_itemcost
        {
            get { return _sri_itemcost; }
            set { _sri_itemcost = value; }
        }
        public string Sri_itemdesc
        {
            get { return _sri_itemdesc; }
            set { _sri_itemdesc = value; }
        }
        public string Sri_itemmfc
        {
            get { return _sri_itemmfc; }
            set { _sri_itemmfc = value; }
        }
        public string Sri_itemstatus
        {
            get { return _sri_itemstatus; }
            set { _sri_itemstatus = value; }
        }
        public string Sri_itemtype
        {
            get { return _sri_itemtype; }
            set { _sri_itemtype = value; }
        }
        public string Sri_jobno
        {
            get { return _sri_jobno; }
            set { _sri_jobno = value; }
        }
        public DateTime Sri_lastwarrstdate
        {
            get { return _sri_lastwarrstdate; }
            set { _sri_lastwarrstdate = value; }
        }
        public string Sri_mainitemcode
        {
            get { return _sri_mainitemcode; }
            set { _sri_mainitemcode = value; }
        }
        public string Sri_mainitemmfc
        {
            get { return _sri_mainitemmfc; }
            set { _sri_mainitemmfc = value; }
        }
        public string Sri_mainitemserial
        {
            get { return _sri_mainitemserial; }
            set { _sri_mainitemserial = value; }
        }
        public string Sri_mainitemwarranty
        {
            get { return _sri_mainitemwarranty; }
            set { _sri_mainitemwarranty = value; }
        }
        public string Sri_mainjobno
        {
            get { return _sri_mainjobno; }
            set { _sri_mainjobno = value; }
        }
        public string Sri_mainreqlocation
        {
            get { return _sri_mainreqlocation; }
            set { _sri_mainreqlocation = value; }
        }
        public string Sri_mainreqno
        {
            get { return _sri_mainreqno; }
            set { _sri_mainreqno = value; }
        }
        public Int32 Sri_milage
        {
            get { return _sri_milage; }
            set { _sri_milage = value; }
        }
        public string Sri_model
        {
            get { return _sri_model; }
            set { _sri_model = value; }
        }
        public string Sri_msnno
        {
            get { return _sri_msnno; }
            set { _sri_msnno = value; }
        }
        public Boolean Sri_onloan
        {
            get { return _sri_onloan; }
            set { _sri_onloan = value; }
        }
        public string Sri_regno
        {
            get { return _sri_regno; }
            set { _sri_regno = value; }
        }
        public string Sri_reqitemtype
        {
            get { return _sri_reqitemtype; }
            set { _sri_reqitemtype = value; }
        }
        public Int32 Sri_req_line
        {
            get { return _sri_req_line; }
            set { _sri_req_line = value; }
        }
        public string Sri_req_no
        {
            get { return _sri_req_no; }
            set { _sri_req_no = value; }
        }
        public string Sri_req_type
        {
            get { return _sri_req_type; }
            set { _sri_req_type = value; }
        }
        public Boolean Sri_sentwcn
        {
            get { return _sri_sentwcn; }
            set { _sri_sentwcn = value; }
        }
        public string Sri_serial1
        {
            get { return _sri_serial1; }
            set { _sri_serial1 = value; }
        }
        public string Sri_serial2
        {
            get { return _sri_serial2; }
            set { _sri_serial2 = value; }
        }
        public string Sri_serlocachr
        {
            get { return _sri_serlocachr; }
            set { _sri_serlocachr = value; }
        }
        public Boolean Sri_usejob
        {
            get { return _sri_usejob; }
            set { _sri_usejob = value; }
        }
        public string Sri_warranty
        {
            get { return _sri_warranty; }
            set { _sri_warranty = value; }
        }
        public Boolean Sri_warranty_status
        {
            get { return _sri_warranty_status; }
            set { _sri_warranty_status = value; }
        }
        public Int32 Sri_warrperiod
        {
            get { return _sri_warrperiod; }
            set { _sri_warrperiod = value; }
        }
        public DateTime Sri_warrstartdate
        {
            get { return _sri_warrstartdate; }
            set { _sri_warrstartdate = value; }
        }

        public static SCMServiceRequestDetails Converter(DataRow row)
        {
            return new SCMServiceRequestDetails
            {
                Sri_availabilty = row["SRI_AVAILABILTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRI_AVAILABILTY"]),
                Sri_brand = row["SRI_BRAND"] == DBNull.Value ? string.Empty : row["SRI_BRAND"].ToString(),
                Sri_chg_warrremarks = row["SRI_CHG_WARRREMARKS"] == DBNull.Value ? string.Empty : row["SRI_CHG_WARRREMARKS"].ToString(),
                Sri_chg_warrstdate = row["SRI_CHG_WARRSTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRI_CHG_WARRSTDATE"]),
                Sri_customernotes = row["SRI_CUSTOMERNOTES"] == DBNull.Value ? string.Empty : row["SRI_CUSTOMERNOTES"].ToString(),
                Sri_isstockupdate = row["SRI_ISSTOCKUPDATE"] == DBNull.Value ? false : Convert.ToBoolean(row["SRI_ISSTOCKUPDATE"]),
                Sri_issuedocno = row["SRI_ISSUEDOCNO"] == DBNull.Value ? string.Empty : row["SRI_ISSUEDOCNO"].ToString(),
                Sri_itemcode = row["SRI_ITEMCODE"] == DBNull.Value ? string.Empty : row["SRI_ITEMCODE"].ToString(),
                Sri_itemcost = row["SRI_ITEMCOST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRI_ITEMCOST"]),
                Sri_itemdesc = row["SRI_ITEMDESC"] == DBNull.Value ? string.Empty : row["SRI_ITEMDESC"].ToString(),
                Sri_itemmfc = row["SRI_ITEMMFC"] == DBNull.Value ? string.Empty : row["SRI_ITEMMFC"].ToString(),
                Sri_itemstatus = row["SRI_ITEMSTATUS"] == DBNull.Value ? string.Empty : row["SRI_ITEMSTATUS"].ToString(),
                Sri_itemtype = row["SRI_ITEMTYPE"] == DBNull.Value ? string.Empty : row["SRI_ITEMTYPE"].ToString(),
                Sri_jobno = row["SRI_JOBNO"] == DBNull.Value ? string.Empty : row["SRI_JOBNO"].ToString(),
                Sri_lastwarrstdate = row["SRI_LASTWARRSTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRI_LASTWARRSTDATE"]),
                Sri_mainitemcode = row["SRI_MAINITEMCODE"] == DBNull.Value ? string.Empty : row["SRI_MAINITEMCODE"].ToString(),
                Sri_mainitemmfc = row["SRI_MAINITEMMFC"] == DBNull.Value ? string.Empty : row["SRI_MAINITEMMFC"].ToString(),
                Sri_mainitemserial = row["SRI_MAINITEMSERIAL"] == DBNull.Value ? string.Empty : row["SRI_MAINITEMSERIAL"].ToString(),
                Sri_mainitemwarranty = row["SRI_MAINITEMWARRANTY"] == DBNull.Value ? string.Empty : row["SRI_MAINITEMWARRANTY"].ToString(),
                Sri_mainjobno = row["SRI_MAINJOBNO"] == DBNull.Value ? string.Empty : row["SRI_MAINJOBNO"].ToString(),
                Sri_mainreqlocation = row["SRI_MAINREQLOCATION"] == DBNull.Value ? string.Empty : row["SRI_MAINREQLOCATION"].ToString(),
                Sri_mainreqno = row["SRI_MAINREQNO"] == DBNull.Value ? string.Empty : row["SRI_MAINREQNO"].ToString(),
                Sri_milage = row["SRI_MILAGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRI_MILAGE"]),
                Sri_model = row["SRI_MODEL"] == DBNull.Value ? string.Empty : row["SRI_MODEL"].ToString(),
                Sri_msnno = row["SRI_MSNNO"] == DBNull.Value ? string.Empty : row["SRI_MSNNO"].ToString(),
                Sri_onloan = row["SRI_ONLOAN"] == DBNull.Value ? false : Convert.ToBoolean(row["SRI_ONLOAN"]),
                Sri_regno = row["SRI_REGNO"] == DBNull.Value ? string.Empty : row["SRI_REGNO"].ToString(),
                Sri_reqitemtype = row["SRI_REQITEMTYPE"] == DBNull.Value ? string.Empty : row["SRI_REQITEMTYPE"].ToString(),
                Sri_req_line = row["SRI_REQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRI_REQ_LINE"]),
                Sri_req_no = row["SRI_REQ_NO"] == DBNull.Value ? string.Empty : row["SRI_REQ_NO"].ToString(),
                Sri_req_type = row["SRI_REQ_TYPE"] == DBNull.Value ? string.Empty : row["SRI_REQ_TYPE"].ToString(),
                Sri_sentwcn = row["SRI_SENTWCN"] == DBNull.Value ? false : Convert.ToBoolean(row["SRI_SENTWCN"]),
                Sri_serial1 = row["SRI_SERIAL1"] == DBNull.Value ? string.Empty : row["SRI_SERIAL1"].ToString(),
                Sri_serial2 = row["SRI_SERIAL2"] == DBNull.Value ? string.Empty : row["SRI_SERIAL2"].ToString(),
                Sri_serlocachr = row["SRI_SERLOCACHR"] == DBNull.Value ? string.Empty : row["SRI_SERLOCACHR"].ToString(),
                Sri_usejob = row["SRI_USEJOB"] == DBNull.Value ? false : Convert.ToBoolean(row["SRI_USEJOB"]),
                Sri_warranty = row["SRI_WARRANTY"] == DBNull.Value ? string.Empty : row["SRI_WARRANTY"].ToString(),
                Sri_warranty_status = row["SRI_WARRANTY_STATUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SRI_WARRANTY_STATUS"]),
                Sri_warrperiod = row["SRI_WARRPERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRI_WARRPERIOD"]),
                Sri_warrstartdate = row["SRI_WARRSTARTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRI_WARRSTARTDATE"])

            };
        }
    }
}
