using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


public class CashSalesRev_History
{
    #region private members
    private string _item_code;
    private string _item_Name;
    private string _model;
    private string _status;
    private string _seriel;
    private Int32 _app_level;
    private string _grad_anal1;
    private string _apprval_status;
    private decimal _doc_seq_no;

    
   

   

    private string _brand;
    private string _gras_anal_4;
    private decimal _qty;
    private decimal _tot_amount;
    private string _mmct;
    private string _mis_desc;
    private decimal _grad_val1;


    private decimal _grad_val2;
    private string _drp_tp;
    private string _drp_doc_no;
    private string _drp_loc;
    private string _drp_req_dt;
    private string _drp_doc_dte;

   
    private string _reason;
    private string _Saur_Party;

    
   
    
   
   
   

    private DateTime _app_date;
    private string _gen_remark;
    private string _usr_name;
    private Int32 _app_lvl;
    private string _Outitem_code;
    private string _Outitem_Name;
    private string _Outmodel;
    private string _Outitem_brand;
    private Int32 _OutApp_lvl;
    private Int32 _CmntUsr_lvl;
    private string _Cmnt_assignto;
    private string _Cmnt_Cmnt;
    private decimal _grad_val4;
    private string _report_item;

    //for period_over_advance_receipt_refund-history
    private string _sar_anal_3;
    private DateTime _sar_recpDate;
    private decimal _sar_tot_amount;
    private string _sar_app_by;
    private string _se_usr_name;
   
   

    
   

    #endregion
    public decimal Grad_val2
    {
        get { return _grad_val2; }
        set { _grad_val2 = value; }
    }
    //reprint value
    public decimal Doc_seq_no
    {
        get { return _doc_seq_no; }
        set { _doc_seq_no = value; }
    }
    public string Drp_tp
    {
        get { return _drp_tp; }
        set { _drp_tp = value; }
    }
    public string Drp_doc_no
    {
        get { return _drp_doc_no; }
        set { _drp_doc_no = value; }
    }
    public string Drp_doc_dte
    {
        get { return _drp_doc_dte; }
        set { _drp_doc_dte = value; }
    }
    public string Drp_loc
    {
        get { return _drp_loc; }
        set { _drp_loc = value; }
    }
    public string Drp_req_dt
    {
        get { return _drp_req_dt; }
        set { _drp_req_dt = value; }
    }
    public string Reason
    {
        get { return _reason; }
        set { _reason = value; }
    }
    public string Saur_Party
    {
        get { return _Saur_Party; }
        set { _Saur_Party = value; }
    }
    //end
    public decimal Grad_val1
    {
        get { return _grad_val1; }
        set { _grad_val1 = value; }
    }
    public string Grad_anal1
    {
        get { return _grad_anal1; }
        set { _grad_anal1 = value; }
    }
    public Int32 App_level
    {
        get { return _app_level; }
        set { _app_level = value; }
    }
    public string Apprval_status
    {
        get { return _apprval_status; }
        set { _apprval_status = value; }
    }
    public string Item_Name
    {
        get { return _item_Name; }
        set { _item_Name = value; }
    }
    public string Report_item
    {
        get { return _report_item; }
        set { _report_item = value; }
    }
    public string Mis_desc
    {
        get { return _mis_desc; }
        set { _mis_desc = value; }
    }

    public string Item_code
    {
        get { return _item_code; }
        set { _item_code = value; }
    }
    public string Model
    {
        get { return _model; }
        set { _model = value; }
    }

    public decimal Grad_val4
    {
        get { return _grad_val4; }
        set { _grad_val4 = value; }
    }
    public string Status
    {
        get { return _status; }
        set { _status = value; }
    }
    public string Seriel
    {
        get { return _seriel; }
        set { _seriel = value; }
    }
    public DateTime App_date
    {
        get { return _app_date; }
        set { _app_date = value; }
    }
    public string Gen_remark
    {
        get { return _gen_remark; }
        set { _gen_remark = value; }
    }


    public string Mmct
    {
        get { return _mmct; }
        set { _mmct = value; }
    }

    public string Brand
    {
        get { return _brand; }
        set { _brand = value; }
    }

    public decimal Tot_amount
    {
        get { return _tot_amount; }
        set { _tot_amount = value; }
    }
    public decimal Qty
    {
        get { return _qty; }
        set { _qty = value; }
    }
    public string Gras_anal_4
    {
        get { return _gras_anal_4; }
        set { _gras_anal_4 = value; }
    }



    //for period_over_advance_receipt_refund-history
    public string Sar_anal_3
    {
        get { return _sar_anal_3; }
        set { _sar_anal_3 = value; }
    }
    public DateTime Sar_recpDate
    {
        get { return _sar_recpDate; }
        set { _sar_recpDate = value; }
    }
    public decimal Sar_tot_amount
    {
        get { return _sar_tot_amount; }
        set { _sar_tot_amount = value; }
    }
    public string Sar_app_by
    {
        get { return _sar_app_by; }
        set { _sar_app_by = value; }
    }
    public string Se_usr_name
    {
        get { return _se_usr_name; }
        set { _se_usr_name = value; }
    }
    //for period_over_advance_receipt_refund-history END..

    public string Usr_name
    {
        get { return _usr_name; }
        set { _usr_name = value; }
    }
    public Int32 App_lvl
    {
        get { return _app_lvl; }
        set { _app_lvl = value; }
    }
    public string Outitem_code
    {
        get { return _Outitem_code; }
        set { _Outitem_code = value; }
    }
    public string Outitem_Name
    {
        get { return _Outitem_Name; }
        set { _Outitem_Name = value; }
    }
    public string Outmodel
    {
        get { return _Outmodel; }
        set { _Outmodel = value; }
    }
    public string Outitem_brand
    {
        get { return _Outitem_brand; }
        set { _Outitem_brand = value; }
    }
    public Int32 OutApp_lvl
    {
        get { return _OutApp_lvl; }
        set { _OutApp_lvl = value; }
    }
    public Int32 CmntUsr_lvl
    {
        get { return _CmntUsr_lvl; }
        set { _CmntUsr_lvl = value; }
    }
    public string Cmnt_assignto
    {
        get { return _Cmnt_assignto; }
        set { _Cmnt_assignto = value; }
    }
    public string Cmnt_Cmnt
    {
        get { return _Cmnt_Cmnt; }
        set { _Cmnt_Cmnt = value; }
    }
    #region Converter
    public static CashSalesRev_History Converter(DataRow row)
    {

        return new CashSalesRev_History
        {
            Item_code = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
            Item_Name = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
            Model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
            Status = row["SAD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SAD_ITM_STUS"].ToString(),
            Seriel = row["GRAS_ANAL3"] == DBNull.Value ? string.Empty : row["GRAS_ANAL3"].ToString(),
            App_level = row["GRAD_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_LVL"]),
            Mis_desc = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString(),
            Grad_val4 = row["GRAD_VAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL4"]),
            Brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
            Gras_anal_4 = row["GRAS_ANAL4"] == DBNull.Value ? string.Empty : row["GRAS_ANAL4"].ToString(),
            Gen_remark = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
            Report_item = row["REREPORT_ITEM"] == DBNull.Value ? string.Empty : row["REREPORT_ITEM"].ToString(),
            Qty = row["SAD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_QTY"]),
            Tot_amount = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"]),
            Mmct = row["MMCT_SDESC"] == DBNull.Value ? string.Empty : row["MMCT_SDESC"].ToString(),
            Apprval_status = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
        };
    }
    #endregion

    #region ConverterInvoiceDetails
    public static CashSalesRev_History ConverterInvoiceDetails(DataRow row)
    {

        return new CashSalesRev_History
        {
            App_date = row["GRAH_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_APP_DT"]),
            Gen_remark = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
            Usr_name = row["SE_USR_NAME"] == DBNull.Value ? string.Empty : row["SE_USR_NAME"].ToString(),
            App_lvl = row["GRAH_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_APP_LVL"]),
        };
    }
    #endregion
    #region ConverterOutItemDetails
    public static CashSalesRev_History ConverterOutItemDetails(DataRow row)
    {
        return new CashSalesRev_History
        {
            Outitem_code = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
            Outitem_Name = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString(),
            Outmodel = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
            Outitem_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
            OutApp_lvl = row["GRAD_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_LVL"]),
        };
    }
    #endregion
    #region ConverterComments
    public static CashSalesRev_History ConverterComments(DataRow row)
    {
        return new CashSalesRev_History
        {

            Cmnt_Cmnt = row["GRCL_RMK"] == DBNull.Value ? string.Empty : row["GRCL_RMK"].ToString(),
            Cmnt_assignto = row["GRCL_DEPT"] == DBNull.Value ? string.Empty : row["GRCL_DEPT"].ToString(),
            CmntUsr_lvl = row["GRCL_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRCL_LVL"]),
        };
    }
    #endregion

    #region Convert Advanced_Recept
    public static CashSalesRev_History ConverterAdvanceRecpt(DataRow row)
    {
        return new CashSalesRev_History
        {
         
            Sar_anal_3 = row["SAR_ANAL_3"] == DBNull.Value ? string.Empty : row["SAR_ANAL_3"].ToString(),
            Sar_recpDate = row["SAR_RECEIPT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAR_RECEIPT_DATE"]),
            Sar_tot_amount = row["SAR_TOT_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_TOT_SETTLE_AMT"]),
            Gen_remark = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
            //Mis_desc = row["mis_desc"] == DBNull.Value ? string.Empty : row["mis_desc"].ToString(),
            Status = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
            App_lvl = row["GRAH_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_APP_LVL"]),
            Sar_app_by = row["GRAH_APP_BY"] == DBNull.Value ? string.Empty : row["GRAH_APP_BY"].ToString(),
            App_date = row["Approve_Date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Approve_Date"]),
            Se_usr_name = row["SE_USR_NAME"] == DBNull.Value ? string.Empty : row["SE_USR_NAME"].ToString(),
        };
    }



    #endregion
    #region ConverterCashRefund
    public static CashSalesRev_History ConverterCashRefund(DataRow row)
    {
        return new CashSalesRev_History
        {

            Sar_anal_3 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
            Grad_anal1 = row["GRAD_ANAL1"] == DBNull.Value ? string.Empty : row["GRAD_ANAL1"].ToString(),
            Grad_val1 = row["GRAD_VAL1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL1"]),
            Grad_val2 = row["GRAD_VAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL2"]),
            App_lvl = row["GRAH_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_APP_LVL"]),
            Sar_app_by = row["GRAH_CRE_BY"] == DBNull.Value ? string.Empty : row["GRAH_CRE_BY"].ToString(),
            App_date = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"]),
            Status = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
        
        };
    }



    #endregion

    #region ConverterReprint
    public static CashSalesRev_History ConverterReprint(DataRow row)
    {
        return new CashSalesRev_History
        {

            //Doc_seq_no = row["DRP_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DRP_SEQ_NO"]),
            Drp_tp = row["DRP_TP"] == DBNull.Value ? string.Empty : row["DRP_TP"].ToString(),
            Drp_doc_no = row["DRP_DOC_NO"] == DBNull.Value ? string.Empty : row["DRP_DOC_NO"].ToString(),
            Drp_loc = row["DRP_LOC"] == DBNull.Value ? string.Empty : row["DRP_LOC"].ToString(),
            Drp_req_dt = row["DRP_REQ_DT"] == DBNull.Value ? string.Empty : row["DRP_REQ_DT"].ToString(),
            Drp_doc_dte = row["DRP_DOC_DT"] == DBNull.Value ? string.Empty : row["DRP_DOC_DT"].ToString(),
            Reason = row["DRP_REASON"] == DBNull.Value ? string.Empty : row["DRP_REASON"].ToString(),
            Saur_Party = row["SEUR_PARTY"] == DBNull.Value ? string.Empty : row["SEUR_PARTY"].ToString(),



        };
    }



    #endregion
}

