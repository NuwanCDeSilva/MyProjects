using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    [Serializable]
    public class ServiceJobDetail
    {
        /// <summary>
        /// Written By Shani on 21/08/2012
        /// Table: sev_job_det
        /// </summary>
        /// 
        #region Private Members
        private Boolean _jbd_availabilty;
        private string _jbd_brand;
        private string _jbd_chg_warrrmk;
        private DateTime _jbd_chg_warrstdt;
        private string _jbd_cust_notes;
        private DateTime _jbd_dt_pur;
        private string _jbd_invoiceno;
        private Boolean _jbd_isinsurance;
        private Boolean _jbd_isstockupdate;
        private Boolean _jbd_issued;
        private Boolean _jbd_iswrn;
        private string _jbd_item_tp;
        private string _jbd_itm_cd;
        private decimal _jbd_itm_cost;
        private string _jbd_itm_desc;
        private string _jbd_itm_mfc;
        private string _jbd_itm_stus;
        private Int32 _jbd_jobline;
        private string _jbd_jobno;
        private DateTime _jbd_lastwarrstdt;
        private string _jbd_mainitmcd;
        private string _jbd_mainitmser;
        private string _jbd_mainitmwarr;
        private string _jbd_mainitm_mfc;
        private string _jbd_mainjobno;
        private string _jbd_mainreqloc;
        private string _jbd_mainreqno;
        private Decimal _jbd_milage;
        private string _jbd_model;
        private string _jbd_msnno;
        private Boolean _jbd_needgatepass;
        private Boolean _jbd_onloan;
        private string _jbd_regno;
        private string _jbd_reqitmtp;
        private Int32 _jbd_reqline;
        private string _jbd_reqno;
        private string _jbd_req_tp;
        private Boolean _jbd_sentwcn;
        private string _jbd_ser1;
        private string _jbd_ser2;
        private string _jbd_ser3;
        private string _jbd_ser4;
        private string _jbd_sevlocachr;
        private int _jbd_sev_term;
        private string _jbd_usejob;
        private string _jbd_waraamd_by;
        private DateTime _jbd_waraamd_dt;
        private string _jbd_waraamd_seq;
        private string _jbd_warr;
        private Int32 _jbd_warrperiod;
        private Boolean _jbd_warrreplace;
        private DateTime _jbd_warrstartdt;
        private Boolean _jbd_warr_stus;
        #endregion

        public Boolean Jbd_availabilty { get { return _jbd_availabilty; } set { _jbd_availabilty = value; } }
        public string Jbd_brand { get { return _jbd_brand; } set { _jbd_brand = value; } }
        public string Jbd_chg_warrrmk { get { return _jbd_chg_warrrmk; } set { _jbd_chg_warrrmk = value; } }
        public DateTime Jbd_chg_warrstdt { get { return _jbd_chg_warrstdt; } set { _jbd_chg_warrstdt = value; } }
        public string Jbd_cust_notes { get { return _jbd_cust_notes; } set { _jbd_cust_notes = value; } }
        public DateTime Jbd_dt_pur { get { return _jbd_dt_pur; } set { _jbd_dt_pur = value; } }
        public string Jbd_invoiceno { get { return _jbd_invoiceno; } set { _jbd_invoiceno = value; } }
        public Boolean Jbd_isinsurance { get { return _jbd_isinsurance; } set { _jbd_isinsurance = value; } }
        public Boolean Jbd_isstockupdate { get { return _jbd_isstockupdate; } set { _jbd_isstockupdate = value; } }
        public Boolean Jbd_issued { get { return _jbd_issued; } set { _jbd_issued = value; } }
        public Boolean Jbd_iswrn { get { return _jbd_iswrn; } set { _jbd_iswrn = value; } }
        public string Jbd_item_tp { get { return _jbd_item_tp; } set { _jbd_item_tp = value; } }
        public string Jbd_itm_cd { get { return _jbd_itm_cd; } set { _jbd_itm_cd = value; } }
        public decimal Jbd_itm_cost { get { return _jbd_itm_cost; } set { _jbd_itm_cost = value; } }
        public string Jbd_itm_desc { get { return _jbd_itm_desc; } set { _jbd_itm_desc = value; } }
        public string Jbd_itm_mfc { get { return _jbd_itm_mfc; } set { _jbd_itm_mfc = value; } }
        public string Jbd_itm_stus { get { return _jbd_itm_stus; } set { _jbd_itm_stus = value; } }
        public Int32 Jbd_jobline { get { return _jbd_jobline; } set { _jbd_jobline = value; } }
        public string Jbd_jobno { get { return _jbd_jobno; } set { _jbd_jobno = value; } }
        public DateTime Jbd_lastwarrstdt { get { return _jbd_lastwarrstdt; } set { _jbd_lastwarrstdt = value; } }
        public string Jbd_mainitmcd { get { return _jbd_mainitmcd; } set { _jbd_mainitmcd = value; } }
        public string Jbd_mainitmser { get { return _jbd_mainitmser; } set { _jbd_mainitmser = value; } }
        public string Jbd_mainitmwarr { get { return _jbd_mainitmwarr; } set { _jbd_mainitmwarr = value; } }
        public string Jbd_mainitm_mfc { get { return _jbd_mainitm_mfc; } set { _jbd_mainitm_mfc = value; } }
        public string Jbd_mainjobno { get { return _jbd_mainjobno; } set { _jbd_mainjobno = value; } }
        public string Jbd_mainreqloc { get { return _jbd_mainreqloc; } set { _jbd_mainreqloc = value; } }
        public string Jbd_mainreqno { get { return _jbd_mainreqno; } set { _jbd_mainreqno = value; } }
        public Decimal Jbd_milage { get { return _jbd_milage; } set { _jbd_milage = value; } }
        public string Jbd_model { get { return _jbd_model; } set { _jbd_model = value; } }
        public string Jbd_msnno { get { return _jbd_msnno; } set { _jbd_msnno = value; } }
        public Boolean Jbd_needgatepass { get { return _jbd_needgatepass; } set { _jbd_needgatepass = value; } }
        public Boolean Jbd_onloan { get { return _jbd_onloan; } set { _jbd_onloan = value; } }
        public string Jbd_regno { get { return _jbd_regno; } set { _jbd_regno = value; } }
        public string Jbd_reqitmtp { get { return _jbd_reqitmtp; } set { _jbd_reqitmtp = value; } }
        public Int32 Jbd_reqline { get { return _jbd_reqline; } set { _jbd_reqline = value; } }
        public string Jbd_reqno { get { return _jbd_reqno; } set { _jbd_reqno = value; } }
        public string Jbd_req_tp { get { return _jbd_req_tp; } set { _jbd_req_tp = value; } }
        public Boolean Jbd_sentwcn { get { return _jbd_sentwcn; } set { _jbd_sentwcn = value; } }
        public string Jbd_ser1 { get { return _jbd_ser1; } set { _jbd_ser1 = value; } }
        public string Jbd_ser2 { get { return _jbd_ser2; } set { _jbd_ser2 = value; } }
        public string Jbd_ser3 { get { return _jbd_ser3; } set { _jbd_ser3 = value; } }
        public string Jbd_ser4 { get { return _jbd_ser4; } set { _jbd_ser4 = value; } }
        public string Jbd_sevlocachr { get { return _jbd_sevlocachr; } set { _jbd_sevlocachr = value; } }
        public int Jbd_sev_term { get { return _jbd_sev_term; } set { _jbd_sev_term = value; } }
        public string Jbd_usejob { get { return _jbd_usejob; } set { _jbd_usejob = value; } }
        public string Jbd_waraamd_by { get { return _jbd_waraamd_by; } set { _jbd_waraamd_by = value; } }
        public DateTime Jbd_waraamd_dt { get { return _jbd_waraamd_dt; } set { _jbd_waraamd_dt = value; } }
        public string Jbd_waraamd_seq { get { return _jbd_waraamd_seq; } set { _jbd_waraamd_seq = value; } }
        public string Jbd_warr { get { return _jbd_warr; } set { _jbd_warr = value; } }
        public Int32 Jbd_warrperiod { get { return _jbd_warrperiod; } set { _jbd_warrperiod = value; } }
        public Boolean Jbd_warrreplace { get { return _jbd_warrreplace; } set { _jbd_warrreplace = value; } }
        public DateTime Jbd_warrstartdt { get { return _jbd_warrstartdt; } set { _jbd_warrstartdt = value; } }
        public Boolean Jbd_warr_stus { get { return _jbd_warr_stus; } set { _jbd_warr_stus = value; } }

        public static ServiceJobDetail Converter(DataRow row)
        {
            return new ServiceJobDetail
            {
                Jbd_availabilty = row["JBD_AVAILABILTY"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_AVAILABILTY"]),
                Jbd_brand = row["JBD_BRAND"] == DBNull.Value ? string.Empty : row["JBD_BRAND"].ToString(),
                Jbd_chg_warrrmk = row["JBD_CHG_WARRRMK"] == DBNull.Value ? string.Empty : row["JBD_CHG_WARRRMK"].ToString(),
                Jbd_chg_warrstdt = row["JBD_CHG_WARRSTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_CHG_WARRSTDT"]),
                Jbd_cust_notes = row["JBD_CUST_NOTES"] == DBNull.Value ? string.Empty : row["JBD_CUST_NOTES"].ToString(),
                Jbd_dt_pur = row["JBD_DT_PUR"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_DT_PUR"]),
                Jbd_invoiceno = row["JBD_INVOICENO"] == DBNull.Value ? string.Empty : row["JBD_INVOICENO"].ToString(),
                Jbd_isinsurance = row["JBD_ISINSURANCE"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ISINSURANCE"]),
                Jbd_isstockupdate = row["JBD_ISSTOCKUPDATE"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ISSTOCKUPDATE"]),
                Jbd_issued = row["JBD_ISSUED"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ISSUED"]),
                Jbd_iswrn = row["JBD_ISWRN"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ISWRN"]),
                Jbd_item_tp = row["JBD_ITEM_TP"] == DBNull.Value ? string.Empty : row["JBD_ITEM_TP"].ToString(),
                Jbd_itm_cd = row["JBD_ITM_CD"] == DBNull.Value ? string.Empty : row["JBD_ITM_CD"].ToString(),
                Jbd_itm_cost = row["JBD_ITM_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_ITM_COST"]),
                Jbd_itm_desc = row["JBD_ITM_DESC"] == DBNull.Value ? string.Empty : row["JBD_ITM_DESC"].ToString(),
                Jbd_itm_mfc = row["JBD_ITM_MFC"] == DBNull.Value ? string.Empty : row["JBD_ITM_MFC"].ToString(),
                Jbd_itm_stus = row["JBD_ITM_STUS"] == DBNull.Value ? string.Empty : row["JBD_ITM_STUS"].ToString(),
                Jbd_jobline = row["JBD_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_JOBLINE"]),
                Jbd_jobno = row["JBD_JOBNO"] == DBNull.Value ? string.Empty : row["JBD_JOBNO"].ToString(),
                Jbd_lastwarrstdt = row["JBD_LASTWARRSTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_LASTWARRSTDT"]),
                Jbd_mainitmcd = row["JBD_MAINITMCD"] == DBNull.Value ? string.Empty : row["JBD_MAINITMCD"].ToString(),
                Jbd_mainitmser = row["JBD_MAINITMSER"] == DBNull.Value ? string.Empty : row["JBD_MAINITMSER"].ToString(),
                Jbd_mainitmwarr = row["JBD_MAINITMWARR"] == DBNull.Value ? string.Empty : row["JBD_MAINITMWARR"].ToString(),
                Jbd_mainitm_mfc = row["JBD_MAINITM_MFC"] == DBNull.Value ? string.Empty : row["JBD_MAINITM_MFC"].ToString(),
                Jbd_mainjobno = row["JBD_MAINJOBNO"] == DBNull.Value ? string.Empty : row["JBD_MAINJOBNO"].ToString(),
                Jbd_mainreqloc = row["JBD_MAINREQLOC"] == DBNull.Value ? string.Empty : row["JBD_MAINREQLOC"].ToString(),
                Jbd_mainreqno = row["JBD_MAINREQNO"] == DBNull.Value ? string.Empty : row["JBD_MAINREQNO"].ToString(),
                Jbd_milage = row["JBD_MILAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JBD_MILAGE"]),
                Jbd_model = row["JBD_MODEL"] == DBNull.Value ? string.Empty : row["JBD_MODEL"].ToString(),
                Jbd_msnno = row["JBD_MSNNO"] == DBNull.Value ? string.Empty : row["JBD_MSNNO"].ToString(),
                Jbd_needgatepass = row["JBD_NEEDGATEPASS"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_NEEDGATEPASS"]),
                Jbd_onloan = row["JBD_ONLOAN"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_ONLOAN"]),
                Jbd_regno = row["JBD_REGNO"] == DBNull.Value ? string.Empty : row["JBD_REGNO"].ToString(),
                Jbd_reqitmtp = row["JBD_REQITMTP"] == DBNull.Value ? string.Empty : row["JBD_REQITMTP"].ToString(),
                Jbd_reqline = row["JBD_REQLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_REQLINE"]),
                Jbd_reqno = row["JBD_REQNO"] == DBNull.Value ? string.Empty : row["JBD_REQNO"].ToString(),
                Jbd_req_tp = row["JBD_REQ_TP"] == DBNull.Value ? string.Empty : row["JBD_REQ_TP"].ToString(),
                Jbd_sentwcn = row["JBD_SENTWCN"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_SENTWCN"]),
                Jbd_ser1 = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),
                Jbd_ser2 = row["JBD_SER2"] == DBNull.Value ? string.Empty : row["JBD_SER2"].ToString(),
                Jbd_ser3 = row["JBD_SER3"] == DBNull.Value ? string.Empty : row["JBD_SER3"].ToString(),
                Jbd_ser4 = row["JBD_SER4"] == DBNull.Value ? string.Empty : row["JBD_SER4"].ToString(),
                Jbd_sevlocachr = row["JBD_SEVLOCACHR"] == DBNull.Value ? string.Empty : row["JBD_SEVLOCACHR"].ToString(),
                Jbd_sev_term = row["JBD_SEV_TERM"] == DBNull.Value ? 0 : Convert.ToInt16(row["JBD_SEV_TERM"]),
                Jbd_usejob = row["JBD_USEJOB"] == DBNull.Value ? string.Empty : row["JBD_USEJOB"].ToString(),
                Jbd_waraamd_by = row["JBD_WARAAMD_BY"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_BY"].ToString(),
                Jbd_waraamd_dt = row["JBD_WARAAMD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARAAMD_DT"]),
                Jbd_waraamd_seq = row["JBD_WARAAMD_SEQ"] == DBNull.Value ? string.Empty : row["JBD_WARAAMD_SEQ"].ToString(),
                Jbd_warr = row["JBD_WARR"] == DBNull.Value ? string.Empty : row["JBD_WARR"].ToString(),
                Jbd_warrperiod = row["JBD_WARRPERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_WARRPERIOD"]),
                Jbd_warrreplace = row["JBD_WARRREPLACE"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_WARRREPLACE"]),
                Jbd_warrstartdt = row["JBD_WARRSTARTDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JBD_WARRSTARTDT"]),
                Jbd_warr_stus = row["JBD_WARR_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["JBD_WARR_STUS"])

            };
        }

    }
}
