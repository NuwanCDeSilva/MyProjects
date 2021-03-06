using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD11  | User :- suneth On 03-Aug-2015 11:06:23
//===========================================================================================================

public class mst_itm_tax_structure_hdr 
{
public Int32 Ish_stuc_seq    { get; set; } 
public String Ish_stuc_code    { get; set; } 
public String Ish_com    { get; set; } 
public String Ish_des    { get; set; } 
public DateTime Ish_date    { get; set; } 
public String Ish_cre_by    { get; set; } 
public DateTime Ish_cre_when    { get; set; } 
public String Ish_mod_by    { get; set; } 
public DateTime Ish_mod_when    { get; set; } 
public Int32 Ish_act    { get; set; } 
public static mst_itm_tax_structure_hdr Converter(DataRow row)  
{ 
return new mst_itm_tax_structure_hdr 
{ 
Ish_stuc_seq  = row["ISH_STUC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISH_STUC_SEQ"].ToString()), 
Ish_stuc_code  = row["ISH_STUC_CODE"] == DBNull.Value ? string.Empty : row["ISH_STUC_CODE"].ToString(), 
Ish_com  = row["ISH_COM"] == DBNull.Value ? string.Empty : row["ISH_COM"].ToString(), 
Ish_des  = row["ISH_DES"] == DBNull.Value ? string.Empty : row["ISH_DES"].ToString(), 
Ish_date  = row["ISH_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISH_DATE"].ToString()), 
Ish_cre_by  = row["ISH_CRE_BY"] == DBNull.Value ? string.Empty : row["ISH_CRE_BY"].ToString(), 
Ish_cre_when  = row["ISH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISH_CRE_WHEN"].ToString()), 
Ish_mod_by  = row["ISH_MOD_BY"] == DBNull.Value ? string.Empty : row["ISH_MOD_BY"].ToString(), 
Ish_mod_when  = row["ISH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISH_MOD_WHEN"].ToString()), 
Ish_act  = row["ISH_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISH_ACT"].ToString())}; 
} 
} 
} 

