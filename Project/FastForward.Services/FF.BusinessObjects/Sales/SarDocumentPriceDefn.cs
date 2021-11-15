using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class SarDocumentPriceDefn
    {
 public String SADD_PC   { get; set; } 
public String SADD_DOC_TP   { get; set; } 
public String SADD_P_LVL   { get; set; } 
public Int32 SADD_IS_BANK_EX_RT   { get; set; } 
public Int32 SADD_IS_DISC   { get; set; } 
public Int32 SADD_DISC_RT   { get; set; } 
public String SADD_COM   { get; set; } 
public Int32 SADD_CHK_CREDIT_BAL   { get; set; } 
public String SADD_CRE_BY   { get; set; } 
public DateTime SADD_CRE_WHEN   { get; set; } 
public String SADD_MOD_BY   { get; set; } 
public DateTime SADD_MOD_WHEN   { get; set; } 
public String SADD_PB   { get; set; } 
public String SADD_PREFIX   { get; set; } 
public Int32 SADD_DEF   { get; set; } 
public String SADD_DEF_STUS   { get; set; } 
public Int32 SADD_DEF_PB   { get; set; } 
public Int32 SADD_IS_REP   { get; set; } 
public Int32 SADD_REP_ORDER   { get; set; } 
public Int32 SADD_IS_EDIT   { get; set; } 
public Int32 SADD_EDIT_RT   { get; set; }
public static SarDocumentPriceDefn Converter(DataRow row)
{
    return new SarDocumentPriceDefn
    {
        SADD_PC = row["SADD_PC"] == DBNull.Value ? string.Empty : row["SADD_PC"].ToString(),
        SADD_DOC_TP = row["SADD_DOC_TP"] == DBNull.Value ? string.Empty : row["SADD_DOC_TP"].ToString(),
        SADD_P_LVL = row["SADD_P_LVL"] == DBNull.Value ? string.Empty : row["SADD_P_LVL"].ToString(),
        SADD_IS_BANK_EX_RT = row["SADD_IS_BANK_EX_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_IS_BANK_EX_RT"].ToString()),
        SADD_IS_DISC = row["SADD_IS_DISC"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_IS_DISC"].ToString()),
        SADD_DISC_RT = row["SADD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_DISC_RT"].ToString()),
        SADD_COM = row["SADD_COM"] == DBNull.Value ? string.Empty : row["SADD_COM"].ToString(),
        SADD_CHK_CREDIT_BAL = row["SADD_CHK_CREDIT_BAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_CHK_CREDIT_BAL"].ToString()),
        SADD_CRE_BY = row["SADD_CRE_BY"] == DBNull.Value ? string.Empty : row["SADD_CRE_BY"].ToString(),
        SADD_CRE_WHEN = row["SADD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SADD_CRE_WHEN"].ToString()),
        SADD_MOD_BY = row["SADD_MOD_BY"] == DBNull.Value ? string.Empty : row["SADD_MOD_BY"].ToString(),
        SADD_MOD_WHEN = row["SADD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SADD_MOD_WHEN"].ToString()),
        SADD_PB = row["SADD_PB"] == DBNull.Value ? string.Empty : row["SADD_PB"].ToString(),
        SADD_PREFIX = row["SADD_PREFIX"] == DBNull.Value ? string.Empty : row["SADD_PREFIX"].ToString(),
        SADD_DEF = row["SADD_DEF"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_DEF"].ToString()),
        SADD_DEF_STUS = row["SADD_DEF_STUS"] == DBNull.Value ? string.Empty : row["SADD_DEF_STUS"].ToString(),
        SADD_DEF_PB = row["SADD_DEF_PB"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_DEF_PB"].ToString()),
        SADD_IS_REP = row["SADD_IS_REP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_IS_REP"].ToString()),
        SADD_REP_ORDER = row["SADD_REP_ORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_REP_ORDER"].ToString()),
        SADD_IS_EDIT = row["SADD_IS_EDIT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_IS_EDIT"].ToString()),
        SADD_EDIT_RT = row["SADD_EDIT_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_EDIT_RT"].ToString())
    };
}


    }
}
