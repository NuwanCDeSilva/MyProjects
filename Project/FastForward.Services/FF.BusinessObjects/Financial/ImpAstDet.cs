using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD14  | User :- sahanj On 01-Dec-2015 09:29:06
//===========================================================================================================

    public class ImpAstDet 
{
public Int32 ISTD_SEQ_NO   { get; set; } 
public Int32 ISTD_LINE_NO   { get; set; } 
public String ISTD_ENTRY_NO   { get; set; } 
public String ISTD_COST_CAT   { get; set; } 
public String ISTD_COST_TP   { get; set; } 
public String ISTD_COST_ELE   { get; set; } 
public Decimal ISTD_COST_ELE_AMT   { get; set; }
public Decimal ISTD_COST_CLAIM_AMT { get; set; }
public Decimal ISTD_COST_UNCLAIM_AMT { get; set; }
public Decimal ISTD_COST_STL_AMT { get; set; }
public Decimal ISTD_DIFF_AMT { get; set; } 
public String ISTD_ASSESS_NO   { get; set; } 
public DateTime ISTD_ASSESS_DT   { get; set; } 
public String ISTD_STUS   { get; set; } 
public String ISTD_MOD_BY   { get; set; } 
public DateTime ISTD_MOD_DT   { get; set; } 
public String ISTD_MOD_SESSION   { get; set; } 
public String ISTD_CNCL_BY   { get; set; } 
public DateTime ISTD_CNCL_DT   { get; set; } 
public String ISTD_CNCL_SESSION   { get; set; }
public static ImpAstDet Converter(DataRow row)  
{
    return new ImpAstDet 
{ 
ISTD_SEQ_NO = row["ISTD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISTD_SEQ_NO"].ToString()), 
ISTD_LINE_NO = row["ISTD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISTD_LINE_NO"].ToString()), 
ISTD_ENTRY_NO = row["ISTD_ENTRY_NO"] == DBNull.Value ? string.Empty : row["ISTD_ENTRY_NO"].ToString(), 
ISTD_COST_CAT = row["ISTD_COST_CAT"] == DBNull.Value ? string.Empty : row["ISTD_COST_CAT"].ToString(), 
ISTD_COST_TP = row["ISTD_COST_TP"] == DBNull.Value ? string.Empty : row["ISTD_COST_TP"].ToString(), 
ISTD_COST_ELE = row["ISTD_COST_ELE"] == DBNull.Value ? string.Empty : row["ISTD_COST_ELE"].ToString(), 
ISTD_COST_ELE_AMT = row["ISTD_COST_ELE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISTD_COST_ELE_AMT"].ToString()),
ISTD_COST_CLAIM_AMT = row["ISTD_COST_CLAIM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISTD_COST_CLAIM_AMT"].ToString()),
ISTD_COST_UNCLAIM_AMT = row["ISTD_COST_UNCLAIM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISTD_COST_UNCLAIM_AMT"].ToString()),
ISTD_COST_STL_AMT = row["ISTD_COST_STL_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISTD_COST_STL_AMT"].ToString()),
ISTD_DIFF_AMT = row["ISTD_DIFF_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISTD_DIFF_AMT"].ToString()), 
ISTD_ASSESS_NO = row["ISTD_ASSESS_NO"] == DBNull.Value ? string.Empty : row["ISTD_ASSESS_NO"].ToString(), 
ISTD_ASSESS_DT = row["ISTD_ASSESS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISTD_ASSESS_DT"].ToString()), 
ISTD_STUS = row["ISTD_STUS"] == DBNull.Value ? string.Empty : row["ISTD_STUS"].ToString(), 
ISTD_MOD_BY = row["ISTD_MOD_BY"] == DBNull.Value ? string.Empty : row["ISTD_MOD_BY"].ToString(), 
ISTD_MOD_DT = row["ISTD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISTD_MOD_DT"].ToString()), 
ISTD_MOD_SESSION = row["ISTD_MOD_SESSION"] == DBNull.Value ? string.Empty : row["ISTD_MOD_SESSION"].ToString(), 
ISTD_CNCL_BY = row["ISTD_CNCL_BY"] == DBNull.Value ? string.Empty : row["ISTD_CNCL_BY"].ToString(), 
ISTD_CNCL_DT = row["ISTD_CNCL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISTD_CNCL_DT"].ToString()), 
ISTD_CNCL_SESSION = row["ISTD_CNCL_SESSION"] == DBNull.Value ? string.Empty : row["ISTD_CNCL_SESSION"].ToString()}; 
} 
} 
} 

