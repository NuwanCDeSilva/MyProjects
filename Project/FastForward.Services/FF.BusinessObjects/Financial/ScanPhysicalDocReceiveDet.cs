using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable] 
   public class ScanPhysicalDocReceiveDet
    {
        //#region Private Members
        //private string _grdd_com;
        //private string _grdd_cre_by;
        //private DateTime _grdd_cre_dt;
        //private string _grdd_deposit_bank;
        //private string _grdd_doc_bank;
        //private string _grdd_doc_bank_branch;
        //private string _grdd_doc_bank_cd;
        //private string _grdd_doc_desc;
        //private Boolean _grdd_doc_rcv;
        //private string _grdd_doc_ref;
        //private string _grdd_doc_tp;
        //private decimal _grdd_doc_val;
        //private DateTime _grdd_dt;
        //private Boolean _grdd_is_extra;
        //private Boolean _grdd_is_realized;
        //private DateTime _grdd_month;
        //private string _grdd_pc;
        //private string _grdd_rcv_by;
        //private DateTime _grdd_rcv_dt;
        //private DateTime _grdd_realized_dt;
        //private string _grdd_rmk;
        //private string _grdd_scan_by;
        //private DateTime _grdd_scan_dt;
        //private Boolean _grdd_scan_rcv;
        //private Int32 _grdd_seq;
        //private Boolean _grdd_sun_upload;
        //private string _grdd_sun_up_by;
        //private DateTime _grdd_sun_up_dt;
        //private decimal _grdd_sys_val;
        //private Int32 _grdd_week;
        //#endregion

        //public string Grdd_com { get { return _grdd_com; } set { _grdd_com = value; } }
        //public string Grdd_cre_by { get { return _grdd_cre_by; } set { _grdd_cre_by = value; } }
        //public DateTime Grdd_cre_dt { get { return _grdd_cre_dt; } set { _grdd_cre_dt = value; } }
        //public string Grdd_deposit_bank { get { return _grdd_deposit_bank; } set { _grdd_deposit_bank = value; } }
        //public string Grdd_doc_bank { get { return _grdd_doc_bank; } set { _grdd_doc_bank = value; } }
        //public string Grdd_doc_bank_branch { get { return _grdd_doc_bank_branch; } set { _grdd_doc_bank_branch = value; } }
        //public string Grdd_doc_bank_cd { get { return _grdd_doc_bank_cd; } set { _grdd_doc_bank_cd = value; } }
        //public string Grdd_doc_desc { get { return _grdd_doc_desc; } set { _grdd_doc_desc = value; } }
        //public Boolean Grdd_doc_rcv { get { return _grdd_doc_rcv; } set { _grdd_doc_rcv = value; } }
        //public string Grdd_doc_ref { get { return _grdd_doc_ref; } set { _grdd_doc_ref = value; } }
        //public string Grdd_doc_tp { get { return _grdd_doc_tp; } set { _grdd_doc_tp = value; } }
        //public decimal Grdd_doc_val { get { return _grdd_doc_val; } set { _grdd_doc_val = value; } }
        //public DateTime Grdd_dt { get { return _grdd_dt; } set { _grdd_dt = value; } }
        //public Boolean Grdd_is_extra { get { return _grdd_is_extra; } set { _grdd_is_extra = value; } }
        //public Boolean Grdd_is_realized { get { return _grdd_is_realized; } set { _grdd_is_realized = value; } }
        //public DateTime Grdd_month { get { return _grdd_month; } set { _grdd_month = value; } }
        //public string Grdd_pc { get { return _grdd_pc; } set { _grdd_pc = value; } }
        //public string Grdd_rcv_by { get { return _grdd_rcv_by; } set { _grdd_rcv_by = value; } }
        //public DateTime Grdd_rcv_dt { get { return _grdd_rcv_dt; } set { _grdd_rcv_dt = value; } }
        //public DateTime Grdd_realized_dt { get { return _grdd_realized_dt; } set { _grdd_realized_dt = value; } }
        //public string Grdd_rmk { get { return _grdd_rmk; } set { _grdd_rmk = value; } }
        //public string Grdd_scan_by { get { return _grdd_scan_by; } set { _grdd_scan_by = value; } }
        //public DateTime Grdd_scan_dt { get { return _grdd_scan_dt; } set { _grdd_scan_dt = value; } }
        //public Boolean Grdd_scan_rcv { get { return _grdd_scan_rcv; } set { _grdd_scan_rcv = value; } }
        //public Int32 Grdd_seq { get { return _grdd_seq; } set { _grdd_seq = value; } }
        //public Boolean Grdd_sun_upload { get { return _grdd_sun_upload; } set { _grdd_sun_upload = value; } }
        //public string Grdd_sun_up_by { get { return _grdd_sun_up_by; } set { _grdd_sun_up_by = value; } }
        //public DateTime Grdd_sun_up_dt { get { return _grdd_sun_up_dt; } set { _grdd_sun_up_dt = value; } }
        //public decimal Grdd_sys_val { get { return _grdd_sys_val; } set { _grdd_sys_val = value; } }
        //public Int32 Grdd_week { get { return _grdd_week; } set { _grdd_week = value; } }

        //public static ScanPhysicalDocReceiveDet Converter(DataRow row)
        //{
        //    return new ScanPhysicalDocReceiveDet
        //    {
        //        Grdd_com = row["GRDD_COM"] == DBNull.Value ? string.Empty : row["GRDD_COM"].ToString(),
        //        Grdd_cre_by = row["GRDD_CRE_BY"] == DBNull.Value ? string.Empty : row["GRDD_CRE_BY"].ToString(),
        //        Grdd_cre_dt = row["GRDD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_CRE_DT"]),
        //        Grdd_deposit_bank = row["GRDD_DEPOSIT_BANK"] == DBNull.Value ? string.Empty : row["GRDD_DEPOSIT_BANK"].ToString(),
        //        Grdd_doc_bank = row["GRDD_DOC_BANK"] == DBNull.Value ? string.Empty : row["GRDD_DOC_BANK"].ToString(),
        //        Grdd_doc_bank_branch = row["GRDD_DOC_BANK_BRANCH"] == DBNull.Value ? string.Empty : row["GRDD_DOC_BANK_BRANCH"].ToString(),
        //        Grdd_doc_bank_cd = row["GRDD_DOC_BANK_CD"] == DBNull.Value ? string.Empty : row["GRDD_DOC_BANK_CD"].ToString(),
        //        Grdd_doc_desc = row["GRDD_DOC_DESC"] == DBNull.Value ? string.Empty : row["GRDD_DOC_DESC"].ToString(),
        //        Grdd_doc_rcv = row["GRDD_DOC_RCV"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_DOC_RCV"]),
        //        Grdd_doc_ref = row["GRDD_DOC_REF"] == DBNull.Value ? string.Empty : row["GRDD_DOC_REF"].ToString(),
        //        Grdd_doc_tp = row["GRDD_DOC_TP"] == DBNull.Value ? string.Empty : row["GRDD_DOC_TP"].ToString(),
        //        Grdd_doc_val = row["GRDD_DOC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRDD_DOC_VAL"]),
        //        Grdd_dt = row["GRDD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_DT"]),
        //        Grdd_is_extra = row["GRDD_IS_EXTRA"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_IS_EXTRA"]),
        //        Grdd_is_realized = row["GRDD_IS_REALIZED"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_IS_REALIZED"]),
        //        Grdd_month = row["GRDD_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_MONTH"]),
        //        Grdd_pc = row["GRDD_PC"] == DBNull.Value ? string.Empty : row["GRDD_PC"].ToString(),
        //        Grdd_rcv_by = row["GRDD_RCV_BY"] == DBNull.Value ? string.Empty : row["GRDD_RCV_BY"].ToString(),
        //        Grdd_rcv_dt = row["GRDD_RCV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_RCV_DT"]),
        //        Grdd_realized_dt = row["GRDD_REALIZED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_REALIZED_DT"]),
        //        Grdd_rmk = row["GRDD_RMK"] == DBNull.Value ? string.Empty : row["GRDD_RMK"].ToString(),
        //        Grdd_scan_by = row["GRDD_SCAN_BY"] == DBNull.Value ? string.Empty : row["GRDD_SCAN_BY"].ToString(),
        //        Grdd_scan_dt = row["GRDD_SCAN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_SCAN_DT"]),
        //        Grdd_scan_rcv = row["GRDD_SCAN_RCV"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_SCAN_RCV"]),
        //        Grdd_seq = row["GRDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRDD_SEQ"]),
        //        Grdd_sun_upload = row["GRDD_SUN_UPLOAD"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_SUN_UPLOAD"]),
        //        Grdd_sun_up_by = row["GRDD_SUN_UP_BY"] == DBNull.Value ? string.Empty : row["GRDD_SUN_UP_BY"].ToString(),
        //        Grdd_sun_up_dt = row["GRDD_SUN_UP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_SUN_UP_DT"]),
        //        Grdd_sys_val = row["GRDD_SYS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRDD_SYS_VAL"]),
        //        Grdd_week = row["GRDD_WEEK"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRDD_WEEK"])

        //    };
        //}

        #region Private Members
        private string _grdd_com;
        private string _grdd_cre_by;
        private DateTime _grdd_cre_dt;
        private string _grdd_deposit_bank;
        private string _grdd_doc_bank;
        private string _grdd_doc_bank_branch;
        private string _grdd_doc_bank_cd;
        private string _grdd_doc_desc;
        private Boolean _grdd_doc_rcv;
        private string _grdd_doc_ref;
        private string _grdd_doc_tp;
        private decimal _grdd_doc_val;
        private DateTime _grdd_dt;
        private Boolean _grdd_is_extra;
        private Boolean _grdd_is_realized;
        private DateTime _grdd_month;
        private string _grdd_pc;
        private string _grdd_rcv_by;
        private DateTime _grdd_rcv_dt;
        private DateTime _grdd_realized_dt;
        private string _grdd_rmk;
        private string _grdd_scan_by;
        private DateTime _grdd_scan_dt;
        private Boolean _grdd_scan_rcv;
        private Int32 _grdd_seq;
        private Int32 _grdd_short_ref;
        private Boolean _grdd_sun_upload;
        private string _grdd_sun_up_by;
        private DateTime _grdd_sun_up_dt;
        private decimal _grdd_sys_val;
        private Int32 _grdd_week;
        private string _grdd_remarks;
        private Int32 _grdd_is_short_set;

        public string Grdd_remarks
        {
            get { return _grdd_remarks; }
            set { _grdd_remarks = value; }
        }
        #endregion

        public string Grdd_com { get { return _grdd_com; } set { _grdd_com = value; } }
        public string Grdd_cre_by { get { return _grdd_cre_by; } set { _grdd_cre_by = value; } }
        public DateTime Grdd_cre_dt { get { return _grdd_cre_dt; } set { _grdd_cre_dt = value; } }
        public string Grdd_deposit_bank { get { return _grdd_deposit_bank; } set { _grdd_deposit_bank = value; } }
        public string Grdd_doc_bank { get { return _grdd_doc_bank; } set { _grdd_doc_bank = value; } }
        public string Grdd_doc_bank_branch { get { return _grdd_doc_bank_branch; } set { _grdd_doc_bank_branch = value; } }
        public string Grdd_doc_bank_cd { get { return _grdd_doc_bank_cd; } set { _grdd_doc_bank_cd = value; } }
        public string Grdd_doc_desc { get { return _grdd_doc_desc; } set { _grdd_doc_desc = value; } }
        public Boolean Grdd_doc_rcv { get { return _grdd_doc_rcv; } set { _grdd_doc_rcv = value; } }
        public string Grdd_doc_ref { get { return _grdd_doc_ref; } set { _grdd_doc_ref = value; } }
        public string Grdd_doc_tp { get { return _grdd_doc_tp; } set { _grdd_doc_tp = value; } }
        public decimal Grdd_doc_val { get { return _grdd_doc_val; } set { _grdd_doc_val = value; } }
        public DateTime Grdd_dt { get { return _grdd_dt; } set { _grdd_dt = value; } }
        public Boolean Grdd_is_extra { get { return _grdd_is_extra; } set { _grdd_is_extra = value; } }
        public Boolean Grdd_is_realized { get { return _grdd_is_realized; } set { _grdd_is_realized = value; } }
        public DateTime Grdd_month { get { return _grdd_month; } set { _grdd_month = value; } }
        public string Grdd_pc { get { return _grdd_pc; } set { _grdd_pc = value; } }
        public string Grdd_rcv_by { get { return _grdd_rcv_by; } set { _grdd_rcv_by = value; } }
        public DateTime Grdd_rcv_dt { get { return _grdd_rcv_dt; } set { _grdd_rcv_dt = value; } }
        public DateTime Grdd_realized_dt { get { return _grdd_realized_dt; } set { _grdd_realized_dt = value; } }
        public string Grdd_rmk { get { return _grdd_rmk; } set { _grdd_rmk = value; } }
        public string Grdd_scan_by { get { return _grdd_scan_by; } set { _grdd_scan_by = value; } }
        public DateTime Grdd_scan_dt { get { return _grdd_scan_dt; } set { _grdd_scan_dt = value; } }
        public Boolean Grdd_scan_rcv { get { return _grdd_scan_rcv; } set { _grdd_scan_rcv = value; } }
        public Int32 Grdd_seq { get { return _grdd_seq; } set { _grdd_seq = value; } }
        public Int32 Grdd_short_ref { get { return _grdd_short_ref; } set { _grdd_short_ref = value; } }
        public Boolean Grdd_sun_upload { get { return _grdd_sun_upload; } set { _grdd_sun_upload = value; } }
        public string Grdd_sun_up_by { get { return _grdd_sun_up_by; } set { _grdd_sun_up_by = value; } }
        public DateTime Grdd_sun_up_dt { get { return _grdd_sun_up_dt; } set { _grdd_sun_up_dt = value; } }
        public decimal Grdd_sys_val { get { return _grdd_sys_val; } set { _grdd_sys_val = value; } }
        public Int32 Grdd_week { get { return _grdd_week; } set { _grdd_week = value; } }
        public Int32 grdd_is_short_set { get { return _grdd_is_short_set; } set { _grdd_is_short_set = value; } }

        //add by tharanga 2018/05/08
     
         public String bsta_com { get; set; }
         public String bsta_pc { get; set; }
         public DateTime bsta_dt { get; set; }
         public String bsta_accno { get; set; }
         public String bsta_adj_tp { get; set; }

         public Decimal bsta_amt { get; set; }
         public String bsta_refno { get; set; }
         public String bsta_rem { get; set; }
         public String bsta_mid { get; set; }
         public String bsta_cre_by { get; set; }
         public String bank_id { get; set; }
         public String BSTD_SUN_ACC { get; set; }
         public Decimal bsta_bnk_charge { get; set; }
        public static ScanPhysicalDocReceiveDet Converter(DataRow row)
        {
            return new ScanPhysicalDocReceiveDet
            {
                Grdd_com = row["GRDD_COM"] == DBNull.Value ? string.Empty : row["GRDD_COM"].ToString(),
                Grdd_cre_by = row["GRDD_CRE_BY"] == DBNull.Value ? string.Empty : row["GRDD_CRE_BY"].ToString(),
                Grdd_cre_dt = row["GRDD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_CRE_DT"]),
                Grdd_deposit_bank = row["GRDD_DEPOSIT_BANK"] == DBNull.Value ? string.Empty : row["GRDD_DEPOSIT_BANK"].ToString(),
                Grdd_doc_bank = row["GRDD_DOC_BANK"] == DBNull.Value ? string.Empty : row["GRDD_DOC_BANK"].ToString(),
                Grdd_doc_bank_branch = row["GRDD_DOC_BANK_BRANCH"] == DBNull.Value ? string.Empty : row["GRDD_DOC_BANK_BRANCH"].ToString(),
                Grdd_doc_bank_cd = row["GRDD_DOC_BANK_CD"] == DBNull.Value ? string.Empty : row["GRDD_DOC_BANK_CD"].ToString(),
                Grdd_doc_desc = row["GRDD_DOC_DESC"] == DBNull.Value ? string.Empty : row["GRDD_DOC_DESC"].ToString(),
                Grdd_doc_rcv = row["GRDD_DOC_RCV"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_DOC_RCV"]),
                Grdd_doc_ref = row["GRDD_DOC_REF"] == DBNull.Value ? string.Empty : row["GRDD_DOC_REF"].ToString(),
                Grdd_doc_tp = row["GRDD_DOC_TP"] == DBNull.Value ? string.Empty : row["GRDD_DOC_TP"].ToString(),
                Grdd_doc_val = row["GRDD_DOC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRDD_DOC_VAL"]),
                Grdd_dt = row["GRDD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_DT"]),
                Grdd_is_extra = row["GRDD_IS_EXTRA"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_IS_EXTRA"]),
                Grdd_is_realized = row["GRDD_IS_REALIZED"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_IS_REALIZED"]),
                Grdd_month = row["GRDD_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_MONTH"]),
                Grdd_pc = row["GRDD_PC"] == DBNull.Value ? string.Empty : row["GRDD_PC"].ToString(),
                Grdd_rcv_by = row["GRDD_RCV_BY"] == DBNull.Value ? string.Empty : row["GRDD_RCV_BY"].ToString(),
                Grdd_rcv_dt = row["GRDD_RCV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_RCV_DT"]),
                Grdd_realized_dt = row["GRDD_REALIZED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_REALIZED_DT"]),
                Grdd_rmk = row["GRDD_RMK"] == DBNull.Value ? string.Empty : row["GRDD_RMK"].ToString(),
                Grdd_scan_by = row["GRDD_SCAN_BY"] == DBNull.Value ? string.Empty : row["GRDD_SCAN_BY"].ToString(),
                Grdd_scan_dt = row["GRDD_SCAN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_SCAN_DT"]),
                Grdd_scan_rcv = row["GRDD_SCAN_RCV"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_SCAN_RCV"]),
                Grdd_seq = row["GRDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRDD_SEQ"]),
                Grdd_short_ref = row["GRDD_SHORT_REF"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRDD_SHORT_REF"]),
                Grdd_sun_upload = row["GRDD_SUN_UPLOAD"] == DBNull.Value ? false : Convert.ToBoolean(row["GRDD_SUN_UPLOAD"]),
                Grdd_sun_up_by = row["GRDD_SUN_UP_BY"] == DBNull.Value ? string.Empty : row["GRDD_SUN_UP_BY"].ToString(),
                Grdd_sun_up_dt = row["GRDD_SUN_UP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRDD_SUN_UP_DT"]),
                Grdd_sys_val = row["GRDD_SYS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRDD_SYS_VAL"]),
                Grdd_week = row["GRDD_WEEK"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRDD_WEEK"]),
                grdd_is_short_set = row["grdd_is_short_set"] == DBNull.Value ? 0 : Convert.ToInt32(row["grdd_is_short_set"]),
                Grdd_remarks = row["Grdd_remarks"] == DBNull.Value ? string.Empty : row["Grdd_remarks"].ToString()
            };
        }

    }
}
