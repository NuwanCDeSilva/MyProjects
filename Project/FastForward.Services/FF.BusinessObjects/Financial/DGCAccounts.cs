using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD14  | User :- sahanj On 09-Dec-2015 09:17:03
//===========================================================================================================

public class DGCAccounts 
{
public String MDA_COM   { get; set; } 
public String MDA_ACC_NO   { get; set; } 
public String MDA_ACC_NAME   { get; set; } 
public Decimal MDA_FAC_AMT   { get; set; } 
public Decimal MDA_USED_AMT   { get; set; } 
public Int32 MDA_ACT   { get; set; } 
public static DGCAccounts Converter(DataRow row)  
{ 
return new DGCAccounts 
{ 
MDA_COM = row["MDA_COM"] == DBNull.Value ? string.Empty : row["MDA_COM"].ToString(), 
MDA_ACC_NO = row["MDA_ACC_NO"] == DBNull.Value ? string.Empty : row["MDA_ACC_NO"].ToString(), 
MDA_ACC_NAME = row["MDA_ACC_NAME"] == DBNull.Value ? string.Empty : row["MDA_ACC_NAME"].ToString(), 
MDA_FAC_AMT = row["MDA_FAC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MDA_FAC_AMT"].ToString()), 
MDA_USED_AMT = row["MDA_USED_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MDA_USED_AMT"].ToString()), 
MDA_ACT = row["MDA_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MDA_ACT"].ToString())}; 
} 
} 
} 
