using System; 
using System.Data; 

namespace FF.BusinessObjects 
{

//===========================================================================================================
// This code is generated by Code gen V.1 
// All rights reserved.
// Suneththaraka02@gmail.com 
// Computer :- ITPD11  | User :- suneth On 09-Feb-2015 04:10:45
//===========================================================================================================

public class whf_so_details 
{
public Int32 SO_LINE   { get; set; } 
public Int32 ITEM_LINE_NO   { get; set; } 
public String ITEM_CODE   { get; set; } 
public String DESCRIPTION   { get; set; } 
public String MODEL   { get; set; } 
public decimal QTY   { get; set; }
public decimal UNIT_RATE { get; set; }
public decimal AMOUNT { get; set; }
public decimal ITEM_TAX_AMOUNT { get; set; }
public decimal SO_BALANCE { get; set; }
public decimal DIS_RATE { get; set; }
public decimal DIS_AMOUNT { get; set; }
public decimal TOT_AMOUNT { get; set; } 
public String PRICE_BOOK   { get; set; } 
public String PRICE_LEVEL   { get; set; } 
public String UOM   { get; set; } 
public String STATUS   { get; set; } 
public String SO_NO   { get; set; } 
public String NEW_ITEM_CODE   { get; set; } 
public String NEW_ITEM_DESC   { get; set; } 
public Int32 PRICE_BOOK_PRICE   { get; set; } 
public String RES_NO   { get; set; } 
public String RES_REQ_NO   { get; set; } 
public Int32 RES_LINE_NO   { get; set; } 
public String RES_ITEM_CODE   { get; set; }
public decimal RES_QTY { get; set; }
public decimal RES_BAL_QTY { get; set; } 
public Int32 WARA_PERIOD   { get; set; } 
public Int32 CHECK_STK_TYPE   { get; set; } 
public Int32 PB_SEQ   { get; set; } 
public Int32 PB_ITEM_SEQ_NO   { get; set; } 
public String WARA_REMARKS   { get; set; } 
public String ITEM_TYPE   { get; set; } 
public Int32 PRICE_SHOW   { get; set; } 
public String MAIN_ITEM   { get; set; } 
public Int32 CANCEL_QTY   { get; set; } 
public Int32 ISSUE_AS_FREE   { get; set; } 
public String WITH_MAIN_ITEM   { get; set; } 
public Int32 MAIN_ITEM_FOR_FREE   { get; set; } 
public static whf_so_details Converter(DataRow row)  
{ 
return new whf_so_details 
{ 
SO_LINE = row["SO_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SO_LINE"].ToString()), 
ITEM_LINE_NO = row["ITEM_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITEM_LINE_NO"].ToString()), 
ITEM_CODE = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(), 
DESCRIPTION = row["DESCRIPTION"] == DBNull.Value ? string.Empty : row["DESCRIPTION"].ToString(), 
MODEL = row["MODEL"] == DBNull.Value ? string.Empty : row["MODEL"].ToString(), 
QTY = row["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY"].ToString()),
UNIT_RATE = row["UNIT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["UNIT_RATE"].ToString()),
AMOUNT = row["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AMOUNT"].ToString()),
ITEM_TAX_AMOUNT = row["ITEM_TAX_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITEM_TAX_AMOUNT"].ToString()),
SO_BALANCE = row["SO_BALANCE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SO_BALANCE"].ToString()),
DIS_RATE = row["DIS_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DIS_RATE"].ToString()),
DIS_AMOUNT = row["DIS_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DIS_AMOUNT"].ToString()),
TOT_AMOUNT = row["TOT_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOT_AMOUNT"].ToString()), 
PRICE_BOOK = row["PRICE_BOOK"] == DBNull.Value ? string.Empty : row["PRICE_BOOK"].ToString(), 
PRICE_LEVEL = row["PRICE_LEVEL"] == DBNull.Value ? string.Empty : row["PRICE_LEVEL"].ToString(), 
UOM = row["UOM"] == DBNull.Value ? string.Empty : row["UOM"].ToString(), 
STATUS = row["STATUS"] == DBNull.Value ? string.Empty : row["STATUS"].ToString(), 
SO_NO = row["SO_NO"] == DBNull.Value ? string.Empty : row["SO_NO"].ToString(), 
NEW_ITEM_CODE = row["NEW_ITEM_CODE"] == DBNull.Value ? string.Empty : row["NEW_ITEM_CODE"].ToString(), 
NEW_ITEM_DESC = row["NEW_ITEM_DESC"] == DBNull.Value ? string.Empty : row["NEW_ITEM_DESC"].ToString(), 
PRICE_BOOK_PRICE = row["PRICE_BOOK_PRICE"] == DBNull.Value ? 0 : Convert.ToInt32(row["PRICE_BOOK_PRICE"].ToString()), 
RES_NO = row["RES_NO"] == DBNull.Value ? string.Empty : row["RES_NO"].ToString(), 
RES_REQ_NO = row["RES_REQ_NO"] == DBNull.Value ? string.Empty : row["RES_REQ_NO"].ToString(), 
RES_LINE_NO = row["RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["RES_LINE_NO"].ToString()), 
RES_ITEM_CODE = row["RES_ITEM_CODE"] == DBNull.Value ? string.Empty : row["RES_ITEM_CODE"].ToString(), 
RES_QTY = row["RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RES_QTY"].ToString()),
RES_BAL_QTY = row["RES_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RES_BAL_QTY"].ToString()), 
WARA_PERIOD = row["WARA_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["WARA_PERIOD"].ToString()), 
CHECK_STK_TYPE = row["CHECK_STK_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CHECK_STK_TYPE"].ToString()), 
PB_SEQ = row["PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["PB_SEQ"].ToString()), 
PB_ITEM_SEQ_NO = row["PB_ITEM_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PB_ITEM_SEQ_NO"].ToString()), 
WARA_REMARKS = row["WARA_REMARKS"] == DBNull.Value ? string.Empty : row["WARA_REMARKS"].ToString(), 
ITEM_TYPE = row["ITEM_TYPE"] == DBNull.Value ? string.Empty : row["ITEM_TYPE"].ToString(), 
PRICE_SHOW = row["PRICE_SHOW"] == DBNull.Value ? 0 : Convert.ToInt32(row["PRICE_SHOW"].ToString()), 
MAIN_ITEM = row["MAIN_ITEM"] == DBNull.Value ? string.Empty : row["MAIN_ITEM"].ToString(), 
CANCEL_QTY = row["CANCEL_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["CANCEL_QTY"].ToString()), 
ISSUE_AS_FREE = row["ISSUE_AS_FREE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISSUE_AS_FREE"].ToString()), 
WITH_MAIN_ITEM = row["WITH_MAIN_ITEM"] == DBNull.Value ? string.Empty : row["WITH_MAIN_ITEM"].ToString(), 
MAIN_ITEM_FOR_FREE = row["MAIN_ITEM_FOR_FREE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MAIN_ITEM_FOR_FREE"].ToString())}; 
} 
} 
} 

