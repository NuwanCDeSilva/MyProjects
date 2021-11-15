using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD11  | User :- suneth On 23-Jun-2015 04:09:13
//===========================================================================================================

public class SCV_SUPP_CLAIM_REC 
{
public Int32 Scc_seq    { get; set; } 
public Int32 Scc_line    { get; set; } 
public String Scc_code    { get; set; } 
public Decimal Scc_amt    { get; set; } 
public DateTime Scc_fromdate    { get; set; } 
public DateTime Scc_todate    { get; set; } 
public Boolean Scc_active    { get; set; } 
public static SCV_SUPP_CLAIM_REC Converter(DataRow row)  
{ 
return new SCV_SUPP_CLAIM_REC 
{ 
Scc_seq  = row["SCC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCC_SEQ"].ToString()), 
Scc_line  = row["SCC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCC_LINE"].ToString()), 
Scc_code  = row["SCC_CODE"] == DBNull.Value ? string.Empty : row["SCC_CODE"].ToString(), 
Scc_amt  = row["SCC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCC_AMT"].ToString()), 
Scc_fromdate  = row["SCC_FROMDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCC_FROMDATE"].ToString()), 
Scc_todate  = row["SCC_TODATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCC_TODATE"].ToString()), 
Scc_active  = row["SCC_ACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(row["SCC_ACTIVE"])}; 
} 
} 
} 

