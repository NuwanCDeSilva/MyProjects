using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class PhysicalStockVerificationItem
    {
       #region Private Members
	private string _ausi_cre_by;
	private DateTime _ausi_cre_dt;
	private decimal _ausi_db_qty;
	private string _ausi_itm;
	private string _ausi_job;
	private decimal _ausi_ledger_qty;
	private decimal _ausi_physical_qty;
	private Int32 _ausi_seq;
	private string _ausi_stus;
    private string _ausi_desc;

    
    #endregion

	public string Ausi_cre_by 
	{
		get { return _ausi_cre_by; }
		set { _ausi_cre_by = value; }
	 }
	public DateTime Ausi_cre_dt 
	{
		get { return _ausi_cre_dt; }
		set { _ausi_cre_dt = value; }
	 }
	public decimal Ausi_db_qty 
	{
		get { return _ausi_db_qty; }
		set { _ausi_db_qty = value; }
	 }
	public string Ausi_itm 
	{
		get { return _ausi_itm; }
		set { _ausi_itm = value; }
	 }
	public string Ausi_job 
	{
		get { return _ausi_job; }
		set { _ausi_job = value; }
	 }
	public decimal Ausi_ledger_qty 
	{
		get { return _ausi_ledger_qty; }
		set { _ausi_ledger_qty = value; }
	 }
	public decimal Ausi_physical_qty 
	{
		get { return _ausi_physical_qty; }
		set { _ausi_physical_qty = value; }
	 }
	public Int32 Ausi_seq 
	{
		get { return _ausi_seq; }
		set { _ausi_seq = value; }
	 }
	public string Ausi_stus 
	{
		get { return _ausi_stus; }
		set { _ausi_stus = value; }
	 }
    public string Ausi_desc
    {
        get { return _ausi_desc; }
        set { _ausi_desc = value; }
    }

 public static PhysicalStockVerificationItem Converter(DataRow row) 
	{
	 return new PhysicalStockVerificationItem 
	 {
 	Ausi_cre_by = row["AUSI_CRE_BY"] == DBNull.Value ?   string.Empty : row["AUSI_CRE_BY"].ToString(),
 	Ausi_cre_dt = row["AUSI_CRE_DT"] == DBNull.Value ?   DateTime.MinValue : Convert.ToDateTime(row["AUSI_CRE_DT"]),
 	Ausi_db_qty = row["AUSI_DB_QTY"] == DBNull.Value ?   0 : Convert.ToDecimal(row["AUSI_DB_QTY"]),
 	Ausi_itm = row["AUSI_ITM"] == DBNull.Value ?   string.Empty : row["AUSI_ITM"].ToString(),
 	Ausi_job = row["AUSI_JOB"] == DBNull.Value ?   string.Empty : row["AUSI_JOB"].ToString(),
 	Ausi_ledger_qty = row["AUSI_LEDGER_QTY"] == DBNull.Value ?   0 : Convert.ToDecimal(row["AUSI_LEDGER_QTY"]),
 	Ausi_physical_qty = row["AUSI_PHYSICAL_QTY"] == DBNull.Value ?   0 : Convert.ToDecimal(row["AUSI_PHYSICAL_QTY"]),
 	Ausi_seq = row["AUSI_SEQ"] == DBNull.Value ?   0 : Convert.ToInt32(row["AUSI_SEQ"]),
 	Ausi_stus = row["AUSI_STUS"] == DBNull.Value ?   string.Empty : row["AUSI_STUS"].ToString(),
    Ausi_desc = row["AUSI_DESC"] == DBNull.Value ? string.Empty : row["AUSI_DESC"].ToString()
	};
}


   }
}
