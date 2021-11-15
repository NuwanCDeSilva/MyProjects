using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD11  | User :- suneth On 21-Jan-2015 12:21:57
//===========================================================================================================

public class Service_Replacement_Hstry 
{
public Int32 Rrh_seq    { get; set; } 
public DateTime Rrh_repl_dt    { get; set; } 
public String Rrh_sold_itm_cd    { get; set; } 
public String Rrh_sold_ser1    { get; set; } 
public String Rrh_sold_ser2    { get; set; } 
public String Rrh_sold_warr    { get; set; } 
public Int32 Rrh_sold_serid    { get; set; } 
public String Rrh_repl_itm_cd    { get; set; } 
public String Rrh_repl_ser1    { get; set; } 
public String Rrh_repl_ser2    { get; set; } 
public String Rrh_repl_warr    { get; set; } 
public Int32 Rrh_repl_serid    { get; set; } 
public String Rrh_job_no    { get; set; } 
public Int32 Rrh_job_line    { get; set; } 
public String Rrh_cre_by    { get; set; } 
public DateTime Rrh_cre_dt    { get; set; } 
public String Rrh_mod_by    { get; set; } 
public DateTime Rrh_mod_dt    { get; set; } 
public static Service_Replacement_Hstry Converter(DataRow row)  
{ 
return new Service_Replacement_Hstry 
{ 
Rrh_seq  = row["RRH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RRH_SEQ"].ToString()), 
Rrh_repl_dt  = row["RRH_REPL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RRH_REPL_DT"].ToString()), 
Rrh_sold_itm_cd  = row["RRH_SOLD_ITM_CD"] == DBNull.Value ? string.Empty : row["RRH_SOLD_ITM_CD"].ToString(), 
Rrh_sold_ser1  = row["RRH_SOLD_SER1"] == DBNull.Value ? string.Empty : row["RRH_SOLD_SER1"].ToString(), 
Rrh_sold_ser2  = row["RRH_SOLD_SER2"] == DBNull.Value ? string.Empty : row["RRH_SOLD_SER2"].ToString(), 
Rrh_sold_warr  = row["RRH_SOLD_WARR"] == DBNull.Value ? string.Empty : row["RRH_SOLD_WARR"].ToString(), 
Rrh_sold_serid  = row["RRH_SOLD_SERID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RRH_SOLD_SERID"].ToString()), 
Rrh_repl_itm_cd  = row["RRH_REPL_ITM_CD"] == DBNull.Value ? string.Empty : row["RRH_REPL_ITM_CD"].ToString(), 
Rrh_repl_ser1  = row["RRH_REPL_SER1"] == DBNull.Value ? string.Empty : row["RRH_REPL_SER1"].ToString(), 
Rrh_repl_ser2  = row["RRH_REPL_SER2"] == DBNull.Value ? string.Empty : row["RRH_REPL_SER2"].ToString(), 
Rrh_repl_warr  = row["RRH_REPL_WARR"] == DBNull.Value ? string.Empty : row["RRH_REPL_WARR"].ToString(), 
Rrh_repl_serid  = row["RRH_REPL_SERID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RRH_REPL_SERID"].ToString()), 
Rrh_job_no  = row["RRH_JOB_NO"] == DBNull.Value ? string.Empty : row["RRH_JOB_NO"].ToString(), 
Rrh_job_line  = row["RRH_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["RRH_JOB_LINE"].ToString()), 
Rrh_cre_by  = row["RRH_CRE_BY"] == DBNull.Value ? string.Empty : row["RRH_CRE_BY"].ToString(), 
Rrh_cre_dt  = row["RRH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RRH_CRE_DT"].ToString()), 
Rrh_mod_by  = row["RRH_MOD_BY"] == DBNull.Value ? string.Empty : row["RRH_MOD_BY"].ToString(), 
Rrh_mod_dt  = row["RRH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RRH_MOD_DT"].ToString())}; 
} 
} 
} 

