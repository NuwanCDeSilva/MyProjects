using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD11  | User :- suneth On 07-Jul-2015 04:14:36
//===========================================================================================================
[Serializable]
public class mst_itm_redeem_com 
{
public String Red_item_code    { get; set; } 
public String Red_com_code    { get; set; } 
public Int32 Red_active    { get; set; } 
public String Red_create_by    { get; set; } 
public DateTime Red_create_when    { get; set; }
public String Red_com_des { get; set; }
public string Red_active_status { get; set; }//rukshan 15/Dec/2015
public static mst_itm_redeem_com Converter(DataRow row)  
{ 
return new mst_itm_redeem_com 
{ 
Red_item_code  = row["RED_ITEM_CODE"] == DBNull.Value ? string.Empty : row["RED_ITEM_CODE"].ToString(), 
Red_com_code  = row["RED_COM_CODE"] == DBNull.Value ? string.Empty : row["RED_COM_CODE"].ToString(), 
Red_active  = row["RED_ACTIVE"] == DBNull.Value ? 0 : Convert.ToInt32(row["RED_ACTIVE"].ToString()), 
Red_create_by  = row["RED_CREATE_BY"] == DBNull.Value ? string.Empty : row["RED_CREATE_BY"].ToString(), 
Red_create_when  = row["RED_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RED_CREATE_WHEN"].ToString()),
Red_com_des = row["Red_com_des"] == DBNull.Value ? string.Empty : row["Red_com_des"].ToString()
}; 
} 
} 
} 

