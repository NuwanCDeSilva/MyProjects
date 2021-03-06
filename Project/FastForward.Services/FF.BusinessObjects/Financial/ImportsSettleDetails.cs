using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD14  | User :- sahanj On 09-Dec-2015 09:21:29
//===========================================================================================================

public class ImportsSettleDetails 
{
public Int32 ISDT_SEQ_NO   { get; set; } 
public String ISDT_DOC_NO   { get; set; } 
public Int32 ISDT_LINE   { get; set; } 
public String ISDT_AST_NO   { get; set; } 
public Decimal ISDT_AST_AMT   { get; set; } 
public static ImportsSettleDetails Converter(DataRow row)  
{ 
return new ImportsSettleDetails 
{ 
ISDT_SEQ_NO = row["ISDT_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISDT_SEQ_NO"].ToString()), 
ISDT_DOC_NO = row["ISDT_DOC_NO"] == DBNull.Value ? string.Empty : row["ISDT_DOC_NO"].ToString(), 
ISDT_LINE = row["ISDT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISDT_LINE"].ToString()), 
ISDT_AST_NO = row["ISDT_AST_NO"] == DBNull.Value ? string.Empty : row["ISDT_AST_NO"].ToString(), 
ISDT_AST_AMT = row["ISDT_AST_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISDT_AST_AMT"].ToString())}; 
} 
} 
} 

