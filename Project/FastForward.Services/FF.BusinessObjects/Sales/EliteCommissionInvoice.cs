using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class EliteCommissionInvoice
    {
        #region Private Members
	private decimal _saec_discount;
	private string _saec_emp_code;
	private string _saec_emp_epf;
	private string _saec_emp_type;
	private string _saec_inv_no;
	private Boolean _saec_is_rev;
	private string _saec_pc;
	private decimal _saec_price;
	private decimal _saec_vat;
    private string _saec_circular;
    private int _saec_year;
    private int _saec_month;
    private DateTime _saec_from;
    private DateTime _saec_to;
    private string _saec_itm;

        #endregion


    public string Saec_circular
    {
        get { return _saec_circular; }
        set { _saec_circular = value; }
    }
	public decimal Saec_discount 
	{
		get { return _saec_discount; }
		set { _saec_discount = value; }
	 }
	public string Saec_emp_code 
	{
		get { return _saec_emp_code; }
		set { _saec_emp_code = value; }
	 }
	public string Saec_emp_epf 
	{
		get { return _saec_emp_epf; }
		set { _saec_emp_epf = value; }
	 }
	public string Saec_emp_type 
	{
		get { return _saec_emp_type; }
		set { _saec_emp_type = value; }
	 }
	public string Saec_inv_no 
	{
		get { return _saec_inv_no; }
		set { _saec_inv_no = value; }
	 }
	public Boolean Saec_is_rev 
	{
		get { return _saec_is_rev; }
		set { _saec_is_rev = value; }
	 }
	public string Saec_pc 
	{
		get { return _saec_pc; }
		set { _saec_pc = value; }
	 }
	public decimal Saec_price 
	{
		get { return _saec_price; }
		set { _saec_price = value; }
	 }
	public decimal Saec_vat 
	{
		get { return _saec_vat; }
		set { _saec_vat = value; }
	 }

    public int Saec_year
    {
        get { return _saec_year; }
        set { _saec_year = value; }
    }

    public int Saec_month
    {
        get { return _saec_month; }
        set { _saec_month = value; }
    }
    public DateTime Saec_from
    {
        get { return _saec_from; }
        set { _saec_from = value; }
    }
    public DateTime Saec_to
    {
        get { return _saec_to; }
        set { _saec_to = value; }
    }

    public string Saec_itm
    {
        get { return _saec_itm; }
        set { _saec_itm = value; }
    }


    public static EliteCommissionInvoice Converter(DataRow row)
    {
        return new EliteCommissionInvoice
        {
            Saec_discount = row["SAEC_DISCOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_DISCOUNT"]),
            Saec_emp_code = row["SAEC_EMP_CODE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_CODE"].ToString(),
            Saec_emp_epf = row["SAEC_EMP_EPF"] == DBNull.Value ? string.Empty : row["SAEC_EMP_EPF"].ToString(),
            Saec_emp_type = row["SAEC_EMP_TYPE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_TYPE"].ToString(),
            Saec_inv_no = row["SAEC_INV_NO"] == DBNull.Value ? string.Empty : row["SAEC_INV_NO"].ToString(),
            Saec_is_rev = row["SAEC_IS_REV"] == DBNull.Value ? false : Convert.ToBoolean(row["SAEC_IS_REV"]),
            Saec_pc = row["SAEC_PC"] == DBNull.Value ? string.Empty : row["SAEC_PC"].ToString(),
            Saec_price = row["SAEC_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_PRICE"]),
            Saec_vat = row["SAEC_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_VAT"]),
            Saec_circular = row["SAEC_CIRCULAR"] == DBNull.Value ? string.Empty : (row["SAEC_VAT"]).ToString(),
            Saec_year = row["SAEC_YEAR"] == DBNull.Value ?  0 : Convert.ToInt32(row["SAEC_YEAR"]),
            Saec_month = row["SAEC_MONTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_MONTH"]),
            Saec_from = row["SAEC_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_FROM"]),
            Saec_to = row["SAEC_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_TO"]),
            Saec_itm = row["SAEC_ITM"] == DBNull.Value ? string.Empty : row["SAEC_ITM"].ToString(),
        };
    }
    }
}
