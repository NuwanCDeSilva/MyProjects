using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD14  | User :- sahanj On 06-Jan-2016 10:04:09
//===========================================================================================================

public class RouteHeader 
{
public String FRH_CD   { get; set; } 
public String FRH_DESC   { get; set; } 
public String FRH_CAT   { get; set; } 
public Int32 FRH_ACT   { get; set; } 
public String FRH_CRE_BY   { get; set; } 
public DateTime FRH_CRE_DT   { get; set; } 
public String FRH_MOD_BY   { get; set; } 
public DateTime FRH_MOD_DT   { get; set; } 
public static RouteHeader Converter(DataRow row)  
{ 
return new RouteHeader 
{ 
FRH_CD = row["FRH_CD"] == DBNull.Value ? string.Empty : row["FRH_CD"].ToString(), 
FRH_DESC = row["FRH_DESC"] == DBNull.Value ? string.Empty : row["FRH_DESC"].ToString(), 
FRH_CAT = row["FRH_CAT"] == DBNull.Value ? string.Empty : row["FRH_CAT"].ToString(), 
FRH_ACT = row["FRH_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["FRH_ACT"].ToString()), 
FRH_CRE_BY = row["FRH_CRE_BY"] == DBNull.Value ? string.Empty : row["FRH_CRE_BY"].ToString(), 
FRH_CRE_DT = row["FRH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FRH_CRE_DT"].ToString()), 
FRH_MOD_BY = row["FRH_MOD_BY"] == DBNull.Value ? string.Empty : row["FRH_MOD_BY"].ToString(), 
FRH_MOD_DT = row["FRH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FRH_MOD_DT"].ToString())}; 
} 
} 
} 

