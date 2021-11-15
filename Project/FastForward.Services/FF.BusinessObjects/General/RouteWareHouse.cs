using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD14  | User :- sahanj On 06-Jan-2016 10:06:26
//===========================================================================================================

public class RouteWareHouse 
{
public String FRW_CD   { get; set; } 
public String FRW_WH_COM   { get; set; } 
public String FRW_WH_CD   { get; set; } 
public String FRW_COM_CD   { get; set; } 
public String FRW_LOC_CD   { get; set; } 
public Decimal FRW_ROUTE_DISTANCE   { get; set; } 
public String FRW_ROUTE_UOM   { get; set; } 
public Int32 FRW_ACT   { get; set; } 
public String FRW_CRE_BY   { get; set; } 
public DateTime FRW_CRE_DT   { get; set; } 
public String FRW_CRE_SESSION   { get; set; } 
public static RouteWareHouse Converter(DataRow row)  
{ 
return new RouteWareHouse 
{ 
FRW_CD = row["FRW_CD"] == DBNull.Value ? string.Empty : row["FRW_CD"].ToString(), 
FRW_WH_COM = row["FRW_WH_COM"] == DBNull.Value ? string.Empty : row["FRW_WH_COM"].ToString(), 
FRW_WH_CD = row["FRW_WH_CD"] == DBNull.Value ? string.Empty : row["FRW_WH_CD"].ToString(), 
FRW_COM_CD = row["FRW_COM_CD"] == DBNull.Value ? string.Empty : row["FRW_COM_CD"].ToString(), 
FRW_LOC_CD = row["FRW_LOC_CD"] == DBNull.Value ? string.Empty : row["FRW_LOC_CD"].ToString(), 
FRW_ROUTE_DISTANCE = row["FRW_ROUTE_DISTANCE"] == DBNull.Value ? 0 : Convert.ToInt32(row["FRW_ROUTE_DISTANCE"].ToString()), 
FRW_ROUTE_UOM = row["FRW_ROUTE_UOM"] == DBNull.Value ? string.Empty : row["FRW_ROUTE_UOM"].ToString(), 
FRW_ACT = row["FRW_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["FRW_ACT"].ToString()), 
FRW_CRE_BY = row["FRW_CRE_BY"] == DBNull.Value ? string.Empty : row["FRW_CRE_BY"].ToString(), 
FRW_CRE_DT = row["FRW_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FRW_CRE_DT"].ToString()), 
FRW_CRE_SESSION = row["FRW_CRE_SESSION"] == DBNull.Value ? string.Empty : row["FRW_CRE_SESSION"].ToString()}; 
} 
} 
} 
