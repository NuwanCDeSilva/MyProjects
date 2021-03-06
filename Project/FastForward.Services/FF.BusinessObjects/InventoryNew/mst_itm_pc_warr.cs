using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD11  | User :- suneth On 09-Jul-2015 08:39:27
//===========================================================================================================
[Serializable]
public class mst_itm_pc_warr 
{
public String Pc_com    { get; set; } 
public String Pc_code    { get; set; } 
public String Pc_item_code    { get; set; } 
public String Pc_item_stus    { get; set; } 
public Int32 Pc_wara_period    { get; set; } 
public String Pc_wara_rmk    { get; set; } 
public DateTime Pc_valid_from    { get; set; } 
public DateTime Pc_valid_to    { get; set; } 
public DateTime Pc_create_when    { get; set; } 
public String Pc_create_by    { get; set; }
public string Pc_item_st_des { get; set; }//rukshan 15/Dec/2015
public string Pc_type { get; set; }//rukshan 15/Dec/2015
public static mst_itm_pc_warr Converter(DataRow row)  
{ 
return new mst_itm_pc_warr 
{ 
Pc_com  = row["PC_COM"] == DBNull.Value ? string.Empty : row["PC_COM"].ToString(), 
Pc_code  = row["PC_CODE"] == DBNull.Value ? string.Empty : row["PC_CODE"].ToString(), 
Pc_item_code  = row["PC_ITEM_CODE"] == DBNull.Value ? string.Empty : row["PC_ITEM_CODE"].ToString(), 
Pc_item_stus  = row["PC_ITEM_STUS"] == DBNull.Value ? string.Empty : row["PC_ITEM_STUS"].ToString(), 
Pc_wara_period  = row["PC_WARA_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_WARA_PERIOD"].ToString()), 
Pc_wara_rmk  = row["PC_WARA_RMK"] == DBNull.Value ? string.Empty : row["PC_WARA_RMK"].ToString(), 
Pc_valid_from  = row["PC_VALID_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PC_VALID_FROM"].ToString()), 
Pc_valid_to  = row["PC_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PC_VALID_TO"].ToString()), 
Pc_create_when  = row["PC_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PC_CREATE_WHEN"].ToString()), 
Pc_create_by  = row["PC_CREATE_BY"] == DBNull.Value ? string.Empty : row["PC_CREATE_BY"].ToString(),
Pc_item_st_des = row["mis_desc"] == DBNull.Value ? string.Empty : row["mis_desc"].ToString(),
Pc_type = row["pc_type"] == DBNull.Value ? string.Empty : row["pc_type"].ToString()
}; 
} 
} 
} 

