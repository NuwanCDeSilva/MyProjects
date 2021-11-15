using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// Computer :- ITPD11  | User :- suneth On 30-Dec-2014 03:21:06
//===========================================================================================================

public class Service_WCN_Hdr 
{
public Int32 Swc_seq_no    { get; set; } 
public String Swc_doc_no    { get; set; } 
public DateTime Swc_dt    { get; set; } 
public Int32 Swc_tp    { get; set; } 
public String Swc_com    { get; set; } 
public String Swc_loc    { get; set; } 
public String Swc_supp    { get; set; } 
public String Swc_clm_supp    { get; set; } 
public String Swc_supp_tp    { get; set; } 
public String Swc_othdocno    { get; set; } 
public String Swc_rmks    { get; set; } 
public String Swc_air_bill_no    { get; set; } 
public DateTime Swc_bill_dt    { get; set; } 
public Int32 Swc_ispick    { get; set; } 
public String Swc_stus    { get; set; } 
public String Swc_cre_by    { get; set; } 
public DateTime Swc_cre_dt    { get; set; } 
public String Swc_mod_by    { get; set; } 
public DateTime Swc_mod_dt    { get; set; } 
public Int32 Swc_isemail    { get; set; }
public String Swc_order_no { get; set; } 
public String SWC_HOLD_REASON { get; set; }
public String SWC_JOB { get; set; }
public String SWC_REC_TYPE { get; set; }
public Int32 Swc_need_chk { get; set; }
public DateTime Swc_eta { get; set; } 

public static Service_WCN_Hdr Converter(DataRow row)  
{ 
return new Service_WCN_Hdr 
{ 
Swc_seq_no  = row["SWC_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWC_SEQ_NO"].ToString()), 
Swc_doc_no  = row["SWC_DOC_NO"] == DBNull.Value ? string.Empty : row["SWC_DOC_NO"].ToString(), 
Swc_dt  = row["SWC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWC_DT"].ToString()), 
Swc_tp  = row["SWC_TP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWC_TP"].ToString()), 
Swc_com  = row["SWC_COM"] == DBNull.Value ? string.Empty : row["SWC_COM"].ToString(), 
Swc_loc  = row["SWC_LOC"] == DBNull.Value ? string.Empty : row["SWC_LOC"].ToString(), 
Swc_supp  = row["SWC_SUPP"] == DBNull.Value ? string.Empty : row["SWC_SUPP"].ToString(), 
Swc_clm_supp  = row["SWC_CLM_SUPP"] == DBNull.Value ? string.Empty : row["SWC_CLM_SUPP"].ToString(), 
Swc_supp_tp  = row["SWC_SUPP_TP"] == DBNull.Value ? string.Empty : row["SWC_SUPP_TP"].ToString(), 
Swc_othdocno  = row["SWC_OTHDOCNO"] == DBNull.Value ? string.Empty : row["SWC_OTHDOCNO"].ToString(), 
Swc_rmks  = row["SWC_RMKS"] == DBNull.Value ? string.Empty : row["SWC_RMKS"].ToString(), 
Swc_air_bill_no  = row["SWC_AIR_BILL_NO"] == DBNull.Value ? string.Empty : row["SWC_AIR_BILL_NO"].ToString(), 
Swc_bill_dt  = row["SWC_BILL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWC_BILL_DT"].ToString()), 
Swc_ispick  = row["SWC_ISPICK"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWC_ISPICK"].ToString()), 
Swc_stus  = row["SWC_STUS"] == DBNull.Value ? string.Empty : row["SWC_STUS"].ToString(), 
Swc_isemail  = row["SWC_ISEMAIL"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWC_ISEMAIL"].ToString()),
Swc_order_no = row["SWC_ORDER_NO"] == DBNull.Value ? string.Empty : row["SWC_ORDER_NO"].ToString(),
SWC_HOLD_REASON = row["SWC_HOLD_REASON"] == DBNull.Value ? string.Empty : row["SWC_HOLD_REASON"].ToString(),
SWC_JOB = row["SWC_JOB"] == DBNull.Value ? string.Empty : row["SWC_JOB"].ToString(),
SWC_REC_TYPE = row["SWC_REC_TYPE"] == DBNull.Value ? string.Empty : row["SWC_REC_TYPE"].ToString(),
Swc_eta = row["SWC_ETA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWC_ETA"].ToString()),
};  
}

} 
} 
