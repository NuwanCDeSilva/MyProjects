using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD11  | User :- suneth On 03-Aug-2015 11:06:41
//===========================================================================================================
[Serializable]
public class mst_itm_tax_structure_det 
{
public Int32 Its_stuc_seq    { get; set; } 
public String Its_stuc_code    { get; set; } 
public String Its_com    { get; set; } 
public String Its_stus    { get; set; } 
public String Its_tax_cd    { get; set; } 
public String Its_taxrate_cd    { get; set; } 
public Decimal Its_tax_rate    { get; set; } 
public Boolean Its_act    { get; set; }
public DateTime Its_valid_from { get; set; }
public DateTime Its_valid_to { get; set; }

public String Its_stus_Des { get; set; } 
public static mst_itm_tax_structure_det Converter(DataRow row)  
{ 
return new mst_itm_tax_structure_det 
{ 
Its_stuc_seq  = row["ITS_STUC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_STUC_SEQ"].ToString()), 
Its_stuc_code  = row["ITS_STUC_CODE"] == DBNull.Value ? string.Empty : row["ITS_STUC_CODE"].ToString(), 
Its_com  = row["ITS_COM"] == DBNull.Value ? string.Empty : row["ITS_COM"].ToString(), 
Its_stus  = row["ITS_STUS"] == DBNull.Value ? string.Empty : row["ITS_STUS"].ToString(), 
Its_tax_cd  = row["ITS_TAX_CD"] == DBNull.Value ? string.Empty : row["ITS_TAX_CD"].ToString(), 
Its_taxrate_cd  = row["ITS_TAXRATE_CD"] == DBNull.Value ? string.Empty : row["ITS_TAXRATE_CD"].ToString(), 
Its_tax_rate  = row["ITS_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITS_TAX_RATE"].ToString()), 
Its_act  = row["ITS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["ITS_ACT"])}; 
}
public static mst_itm_tax_structure_det webConverter(DataRow row)
{
    return new mst_itm_tax_structure_det
    {
        Its_stuc_seq = row["ITS_STUC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITS_STUC_SEQ"].ToString()),
        Its_stuc_code = row["ITS_STUC_CODE"] == DBNull.Value ? string.Empty : row["ITS_STUC_CODE"].ToString(),
        Its_com = row["ITS_COM"] == DBNull.Value ? string.Empty : row["ITS_COM"].ToString(),
        Its_stus = row["ITS_STUS"] == DBNull.Value ? string.Empty : row["ITS_STUS"].ToString(),
        Its_tax_cd = row["ITS_TAX_CD"] == DBNull.Value ? string.Empty : row["ITS_TAX_CD"].ToString(),
        Its_taxrate_cd = row["ITS_TAXRATE_CD"] == DBNull.Value ? string.Empty : row["ITS_TAXRATE_CD"].ToString(),
        Its_tax_rate = row["ITS_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITS_TAX_RATE"].ToString()),
        Its_act = row["ITS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["ITS_ACT"]),
        Its_valid_from = row["ITS_VALID_FROM"] == DBNull.Value ? DateTime.MinValue :  Convert.ToDateTime(row["ITS_VALID_FROM"]),
        Its_valid_to = row["ITS_VALID_TO"] == DBNull.Value ? DateTime.MinValue :  Convert.ToDateTime(row["ITS_VALID_TO"])
    };
} 
} 
} 

