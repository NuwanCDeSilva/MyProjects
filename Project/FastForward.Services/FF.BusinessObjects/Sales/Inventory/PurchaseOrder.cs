using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //Written By P.Wijetunge on 07/04/2012
    /// <summary>
    /// Purchase Order - m a p - PUR_HDR
    /// </summary>
    public class PurchaseOrder
    {
        #region Private Members
        private string _poh_com;
        private Boolean _poh_contain_kit;
        private string _poh_cre_period;
        private string _poh_cur_cd;
        private decimal _poh_dis_amt;
        private decimal _poh_dis_rt;
        private string _poh_doc_no;
        private DateTime _poh_dt;
        private decimal _poh_ex_rt;
        private int _poh_frm_mon;
        private Int32 _poh_frm_yer;
        private string _poh_job_no;
        private string _poh_ope;
        private decimal _poh_oth_tot;
        private string _poh_pay_term;
        private string _poh_port_of_orig;
        private DateTime _poh_preferd_eta;
        private string _poh_profit_cd;
        private string _poh_ref;
        private string _poh_remarks;
        private Boolean _poh_reprint;
        private string _poh_sent_add;
        private string _poh_sent_by;
        private Boolean _poh_sent_to_vendor;
        private string _poh_sent_via;
        private Int32 _poh_seq_no;
        private string _poh_stus;
        private decimal _poh_sub_tot;
        private string _poh_sub_tp;
        private string _poh_supp;
        private Boolean _poh_tax_chg;
        private decimal _poh_tax_tot;
        private decimal _poh_tot;
        private int _poh_to_mon;
        private Int32 _poh_to_yer;
        private string _poh_tp;
        private string _poh_trans_term;
        private Int32 _poh_is_conspo;

        //UI specific properties
        private string _fromDate = string.Empty;
        private string _toDate = string.Empty;

        MasterBusinessEntity _masterBusinessEntity = null;
        MasterLocation _masterLocation = null;

        List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = null;
        List<PurchaseOrderDetail> _PurchaseOrderDetailList = null;     

        #endregion

        public string Poh_com { get { return _poh_com; } set { _poh_com = value; } }
        public Boolean Poh_contain_kit { get { return _poh_contain_kit; } set { _poh_contain_kit = value; } }
        public string Poh_cre_period { get { return _poh_cre_period; } set { _poh_cre_period = value; } }
        public string Poh_cur_cd { get { return _poh_cur_cd; } set { _poh_cur_cd = value; } }
        public decimal Poh_dis_amt { get { return _poh_dis_amt; } set { _poh_dis_amt = value; } }
        public decimal Poh_dis_rt { get { return _poh_dis_rt; } set { _poh_dis_rt = value; } }
        public string Poh_doc_no { get { return _poh_doc_no; } set { _poh_doc_no = value; } }
        public DateTime Poh_dt { get { return _poh_dt; } set { _poh_dt = value; } }
        public decimal Poh_ex_rt { get { return _poh_ex_rt; } set { _poh_ex_rt = value; } }
        public int Poh_frm_mon { get { return _poh_frm_mon; } set { _poh_frm_mon = value; } }
        public Int32 Poh_frm_yer { get { return _poh_frm_yer; } set { _poh_frm_yer = value; } }
        public string Poh_job_no { get { return _poh_job_no; } set { _poh_job_no = value; } }
        public string Poh_ope { get { return _poh_ope; } set { _poh_ope = value; } }
        public decimal Poh_oth_tot { get { return _poh_oth_tot; } set { _poh_oth_tot = value; } }
        public string Poh_pay_term { get { return _poh_pay_term; } set { _poh_pay_term = value; } }
        public string Poh_port_of_orig { get { return _poh_port_of_orig; } set { _poh_port_of_orig = value; } }
        public DateTime Poh_preferd_eta { get { return _poh_preferd_eta; } set { _poh_preferd_eta = value; } }
        public string Poh_profit_cd { get { return _poh_profit_cd; } set { _poh_profit_cd = value; } }
        public string Poh_ref { get { return _poh_ref; } set { _poh_ref = value; } }
        public string Poh_remarks { get { return _poh_remarks; } set { _poh_remarks = value; } }
        public Boolean Poh_reprint { get { return _poh_reprint; } set { _poh_reprint = value; } }
        public string Poh_sent_add { get { return _poh_sent_add; } set { _poh_sent_add = value; } }
        public string Poh_sent_by { get { return _poh_sent_by; } set { _poh_sent_by = value; } }
        public Boolean Poh_sent_to_vendor { get { return _poh_sent_to_vendor; } set { _poh_sent_to_vendor = value; } }
        public string Poh_sent_via { get { return _poh_sent_via; } set { _poh_sent_via = value; } }
        public Int32 Poh_seq_no { get { return _poh_seq_no; } set { _poh_seq_no = value; } }
        public string Poh_stus { get { return _poh_stus; } set { _poh_stus = value; } }
        public decimal Poh_sub_tot { get { return _poh_sub_tot; } set { _poh_sub_tot = value; } }
        public string Poh_sub_tp { get { return _poh_sub_tp; } set { _poh_sub_tp = value; } }
        public string Poh_supp { get { return _poh_supp; } set { _poh_supp = value; } }
        public Boolean Poh_tax_chg { get { return _poh_tax_chg; } set { _poh_tax_chg = value; } }
        public decimal Poh_tax_tot { get { return _poh_tax_tot; } set { _poh_tax_tot = value; } }
        public decimal Poh_tot { get { return _poh_tot; } set { _poh_tot = value; } }
        public int Poh_to_mon { get { return _poh_to_mon; } set { _poh_to_mon = value; } }
        public Int32 Poh_to_yer { get { return _poh_to_yer; } set { _poh_to_yer = value; } }
        public string Poh_tp { get { return _poh_tp; } set { _poh_tp = value; } }
        public string Poh_trans_term { get { return _poh_trans_term; } set { _poh_trans_term = value; } }
        public Int32 poh_is_conspo { get { return _poh_is_conspo; } set { _poh_is_conspo = value; } }

        public string FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        public string ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }

        public MasterBusinessEntity MasterBusinessEntity
        {
            get { return _masterBusinessEntity; }
            set { _masterBusinessEntity = value; }
        }

        public MasterLocation MasterLocation
        {
            get { return _masterLocation; }
            set { _masterLocation = value; }
        }

        public List<PurchaseOrderDelivery> PurchaseOrderDeliveryList
        {
            get { return _purchaseOrderDeliveryList; }
            set { _purchaseOrderDeliveryList = value; }
        }

        public List<PurchaseOrderDetail> PurchaseOrderDetailList
        {
            get { return _PurchaseOrderDetailList; }
            set { _PurchaseOrderDetailList = value; }
        }


        public static PurchaseOrder ConvertTotal(DataRow row)
        {
            return new PurchaseOrder
            {
                Poh_com = row["POH_COM"] == DBNull.Value ? string.Empty : row["POH_COM"].ToString(),
                Poh_contain_kit = row["POH_CONTAIN_KIT"] == DBNull.Value ? false : Convert.ToBoolean(row["POH_CONTAIN_KIT"]),
                Poh_cre_period = row["POH_CRE_PERIOD"] == DBNull.Value ? string.Empty : row["POH_CRE_PERIOD"].ToString(),
                Poh_cur_cd = row["POH_CUR_CD"] == DBNull.Value ? string.Empty : row["POH_CUR_CD"].ToString(),
                Poh_dis_amt = row["POH_DIS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POH_DIS_AMT"]),
                Poh_dis_rt = row["POH_DIS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POH_DIS_RT"]),
                Poh_doc_no = row["POH_DOC_NO"] == DBNull.Value ? string.Empty : row["POH_DOC_NO"].ToString(),
                Poh_dt = row["POH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["POH_DT"]),
                Poh_ex_rt = row["POH_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POH_EX_RT"]),
                Poh_frm_mon = row["POH_FRM_MON"] == DBNull.Value ? 0 : Convert.ToInt16(row["POH_FRM_MON"]),
                Poh_frm_yer = row["POH_FRM_YER"] == DBNull.Value ? 0 : Convert.ToInt32(row["POH_FRM_YER"]),
                Poh_job_no = row["POH_JOB_NO"] == DBNull.Value ? string.Empty : row["POH_JOB_NO"].ToString(),
                Poh_ope = row["POH_OPE"] == DBNull.Value ? string.Empty : row["POH_OPE"].ToString(),
                Poh_oth_tot = row["POH_OTH_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POH_OTH_TOT"]),
                Poh_pay_term = row["POH_PAY_TERM"] == DBNull.Value ? string.Empty : row["POH_PAY_TERM"].ToString(),
                Poh_port_of_orig = row["POH_PORT_OF_ORIG"] == DBNull.Value ? string.Empty : row["POH_PORT_OF_ORIG"].ToString(),
                Poh_preferd_eta = row["POH_PREFERD_ETA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["POH_PREFERD_ETA"]),
                Poh_profit_cd = row["POH_PROFIT_CD"] == DBNull.Value ? string.Empty : row["POH_PROFIT_CD"].ToString(),
                Poh_ref = row["POH_REF"] == DBNull.Value ? string.Empty : row["POH_REF"].ToString(),
                Poh_remarks = row["POH_REMARKS"] == DBNull.Value ? string.Empty : row["POH_REMARKS"].ToString(),
                Poh_reprint = row["POH_REPRINT"] == DBNull.Value ? false : Convert.ToBoolean(row["POH_REPRINT"]),
                Poh_sent_add = row["POH_SENT_ADD"] == DBNull.Value ? string.Empty : row["POH_SENT_ADD"].ToString(),
                Poh_sent_by = row["POH_SENT_BY"] == DBNull.Value ? string.Empty : row["POH_SENT_BY"].ToString(),
                Poh_sent_to_vendor = row["POH_SENT_TO_VENDOR"] == DBNull.Value ? false : Convert.ToBoolean(row["POH_SENT_TO_VENDOR"]),
                Poh_sent_via = row["POH_SENT_VIA"] == DBNull.Value ? string.Empty : row["POH_SENT_VIA"].ToString(),
                Poh_seq_no = row["POH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["POH_SEQ_NO"]),
                Poh_stus = row["POH_STUS"] == DBNull.Value ? string.Empty : row["POH_STUS"].ToString(),
                Poh_sub_tot = row["POH_SUB_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POH_SUB_TOT"]),
                Poh_sub_tp = row["POH_SUB_TP"] == DBNull.Value ? string.Empty : row["POH_SUB_TP"].ToString(),
                Poh_supp = row["POH_SUPP"] == DBNull.Value ? string.Empty : row["POH_SUPP"].ToString(),
                Poh_tax_chg = row["POH_TAX_CHG"] == DBNull.Value ? false : Convert.ToBoolean(row["POH_TAX_CHG"]),
                Poh_tax_tot = row["POH_TAX_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POH_TAX_TOT"]),
                Poh_tot = row["POH_TOT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["POH_TOT"]),
                Poh_to_mon = row["POH_TO_MON"] == DBNull.Value ? 0 : Convert.ToInt16(row["POH_TO_MON"]),
                Poh_to_yer = row["POH_TO_YER"] == DBNull.Value ? 0 : Convert.ToInt32(row["POH_TO_YER"]),
                Poh_tp = row["POH_TP"] == DBNull.Value ? string.Empty : row["POH_TP"].ToString(),
                Poh_trans_term = row["POH_TRANS_TERM"] == DBNull.Value ? string.Empty : row["POH_TRANS_TERM"].ToString(),
                poh_is_conspo = row["POH_IS_CONSPO"] == DBNull.Value ? 0 : Convert.ToInt32(row["POH_IS_CONSPO"])

            };
        }
    }
}

