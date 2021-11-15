using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD11  | User :- suneth On 07-Jul-2015 04:13:43
//===========================================================================================================
[Serializable]
public class mst_itm_com_reorder 
{
public String Icr_com_code    { get; set; } 
public String Icr_itm_code    { get; set; } 
public String Icr_itm_sts    { get; set; } 
public Decimal Icr_qih    { get; set; } 
public Decimal Icr_fqty    { get; set; } 
public Decimal Icr_reqty    { get; set; } 
public Decimal Icr_iss_qty    { get; set; } 
public Decimal Icr_re_order_lvl    { get; set; } 
public Decimal Icr_re_order_qty    { get; set; } 
public String Icr_created_by    { get; set; } 
public DateTime Icr_create_when    { get; set; } 
public String Icr_last_modify_by    { get; set; } 
public DateTime Icr_last_modify_when    { get; set; } 
public Decimal Icr_avg_cost    { get; set; }
public String Icr_class { get; set; } 
public Decimal Icr_safety_qty    { get; set; } 
public Decimal Icr_min_cost    { get; set; } 
public Decimal Icr_max_cost    { get; set; } 
public Decimal Icr_curr_cost    { get; set; }

public String Icr_Status_Des { get; set; } 

public static mst_itm_com_reorder Converter(DataRow row)  
{ 
return new mst_itm_com_reorder 
{ 
Icr_com_code  = row["ICR_COM_CODE"] == DBNull.Value ? string.Empty : row["ICR_COM_CODE"].ToString(), 
Icr_itm_code  = row["ICR_ITM_CODE"] == DBNull.Value ? string.Empty : row["ICR_ITM_CODE"].ToString(), 
Icr_itm_sts  = row["ICR_ITM_STS"] == DBNull.Value ? string.Empty : row["ICR_ITM_STS"].ToString(), 
Icr_qih  = row["ICR_QIH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_QIH"].ToString()), 
Icr_fqty  = row["ICR_FQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_FQTY"].ToString()), 
Icr_reqty  = row["ICR_REQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_REQTY"].ToString()), 
Icr_iss_qty  = row["ICR_ISS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_ISS_QTY"].ToString()), 
Icr_re_order_lvl  = row["ICR_RE_ORDER_LVL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_RE_ORDER_LVL"].ToString()), 
Icr_re_order_qty  = row["ICR_RE_ORDER_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_RE_ORDER_QTY"].ToString()), 
Icr_created_by  = row["ICR_CREATED_BY"] == DBNull.Value ? string.Empty : row["ICR_CREATED_BY"].ToString(), 
Icr_create_when  = row["ICR_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICR_CREATE_WHEN"].ToString()), 
Icr_last_modify_by  = row["ICR_LAST_MODIFY_BY"] == DBNull.Value ? string.Empty : row["ICR_LAST_MODIFY_BY"].ToString(), 
Icr_last_modify_when  = row["ICR_LAST_MODIFY_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICR_LAST_MODIFY_WHEN"].ToString()), 
Icr_avg_cost  = row["ICR_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_AVG_COST"].ToString()),
Icr_class = row["Icr_class"] == DBNull.Value ? string.Empty : row["Icr_class"].ToString(),
Icr_safety_qty = row["ICR_SAFETY_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_SAFETY_QTY"].ToString()),
Icr_min_cost = row["ICR_MIN_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_MIN_COST"].ToString()),
Icr_max_cost = row["ICR_MAX_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_MAX_COST"].ToString()),
Icr_curr_cost = row["ICR_CURR_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_CURR_COST"].ToString()),
}; 
} 
} 
} 

