using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD14  | User :- sahanj On 02-Dec-2015 02:47:25
//===========================================================================================================
[Serializable]
public class ImpCusdecDutySum 
{
public Int32 CUDS_SEQ_NO   { get; set; } 
public String CUDS_DOC_NO   { get; set; } 
public String CUDS_COST_CAT   { get; set; } 
public String CUDS_COST_TP   { get; set; } 
public String CUDS_COST_ELE   { get; set; } 
public Decimal CUDS_COST_ELE_AMT   { get; set; }
public Decimal CUDS_COST_CLAIM_AMT { get; set; }
public Decimal CUDS_COST_UNCLAIM_AMT { get; set; }
public Decimal CUDS_COST_STL_AMT { get; set; } 
public static ImpCusdecDutySum Converter(DataRow row)  
{ 
return new ImpCusdecDutySum 
{ 
CUDS_SEQ_NO = row["CUDS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUDS_SEQ_NO"].ToString()), 
CUDS_DOC_NO = row["CUDS_DOC_NO"] == DBNull.Value ? string.Empty : row["CUDS_DOC_NO"].ToString(), 
CUDS_COST_CAT = row["CUDS_COST_CAT"] == DBNull.Value ? string.Empty : row["CUDS_COST_CAT"].ToString(), 
CUDS_COST_TP = row["CUDS_COST_TP"] == DBNull.Value ? string.Empty : row["CUDS_COST_TP"].ToString(), 
CUDS_COST_ELE = row["CUDS_COST_ELE"] == DBNull.Value ? string.Empty : row["CUDS_COST_ELE"].ToString(),
CUDS_COST_ELE_AMT = row["CUDS_COST_ELE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUDS_COST_ELE_AMT"].ToString()),
CUDS_COST_CLAIM_AMT = row["CUDS_COST_CLAIM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUDS_COST_CLAIM_AMT"].ToString()),
CUDS_COST_UNCLAIM_AMT = row["CUDS_COST_UNCLAIM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUDS_COST_UNCLAIM_AMT"].ToString()),
CUDS_COST_STL_AMT = row["CUDS_COST_STL_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUDS_COST_STL_AMT"].ToString())
}; 
} 
} 
} 
