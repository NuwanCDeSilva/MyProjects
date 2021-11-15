using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class InventoryCostRate
    {
        #region Private Members
	private string _rcr_com;
	private int _rcr_from;
	private string _rcr_itm_stus;
	private decimal _rcr_rt;
	private int _rcr_to;
	private string _rcr_tp;
	private decimal _rcr_val;
        #endregion

	public string Rcr_com 
	{
		get { return _rcr_com; }
		set { _rcr_com = value; }
	 }
	public int Rcr_from 
	{
		get { return _rcr_from; }
		set { _rcr_from = value; }
	 }
	public string Rcr_itm_stus 
	{
		get { return _rcr_itm_stus; }
		set { _rcr_itm_stus = value; }
	 }
	public decimal Rcr_rt 
	{
		get { return _rcr_rt; }
		set { _rcr_rt = value; }
	 }
	public int Rcr_to 
	{
		get { return _rcr_to; }
		set { _rcr_to = value; }
	 }
	public string Rcr_tp 
	{
		get { return _rcr_tp; }
		set { _rcr_tp = value; }
	 }
	public decimal Rcr_val 
	{
		get { return _rcr_val; }
		set { _rcr_val = value; }
	 }

 public static InventoryCostRate Converter(DataRow row) 
	{
	 return new InventoryCostRate 
	 {
 	Rcr_com = row["RCR_COM"] == DBNull.Value ?   string.Empty : row["RCR_COM"].ToString(),
 	Rcr_from = row["RCR_FROM"] == DBNull.Value ?   0 : Convert.ToInt16(row["RCR_FROM"]),
 	Rcr_itm_stus = row["RCR_ITM_STUS"] == DBNull.Value ?   string.Empty : row["RCR_ITM_STUS"].ToString(),
 	Rcr_rt = row["RCR_RT"] == DBNull.Value ?   0 : Convert.ToDecimal(row["RCR_RT"]),
 	Rcr_to = row["RCR_TO"] == DBNull.Value ?   0 : Convert.ToInt16(row["RCR_TO"]),
 	Rcr_tp = row["RCR_TP"] == DBNull.Value ?   string.Empty : row["RCR_TP"].ToString(),
 	Rcr_val = row["RCR_VAL"] == DBNull.Value ?   0 : Convert.ToDecimal(row["RCR_VAL"])

	};
}

    }
}
