using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PriceDefinitionUserRestriction
    {
    #region Private Members
	private string _spur_com;
	private string _spur_cre_by;
	private DateTime _spur_cre_dt;
	private DateTime _spur_from;
	private Int32 _spur_seq;
	private DateTime _spur_to;
	private string _spur_user;
    #endregion

	public string Spur_com 
	{
		get { return _spur_com; }
		set { _spur_com = value; }
	 }
	public string Spur_cre_by 
	{
		get { return _spur_cre_by; }
		set { _spur_cre_by = value; }
	 }
	public DateTime Spur_cre_dt 
	{
		get { return _spur_cre_dt; }
		set { _spur_cre_dt = value; }
	 }
	public DateTime Spur_from 
	{
		get { return _spur_from; }
		set { _spur_from = value; }
	 }
	public Int32 Spur_seq 
	{
		get { return _spur_seq; }
		set { _spur_seq = value; }
	 }
	public DateTime Spur_to 
	{
		get { return _spur_to; }
		set { _spur_to = value; }
	 }
	public string Spur_user 
	{
		get { return _spur_user; }
		set { _spur_user = value; }
	 }

    public static PriceDefinitionUserRestriction Converter(DataRow row) 
	{
        return new PriceDefinitionUserRestriction 
	 {
 	Spur_com = row["SPUR_COM"] == DBNull.Value ?   string.Empty : row["SPUR_COM"].ToString(),
 	Spur_cre_by = row["SPUR_CRE_BY"] == DBNull.Value ?   string.Empty : row["SPUR_CRE_BY"].ToString(),
 	Spur_cre_dt = row["SPUR_CRE_DT"] == DBNull.Value ?   DateTime.MinValue : Convert.ToDateTime(row["SPUR_CRE_DT"]),
 	Spur_from = row["SPUR_FROM"] == DBNull.Value ?   DateTime.MinValue : Convert.ToDateTime(row["SPUR_FROM"]),
 	Spur_seq = row["SPUR_SEQ"] == DBNull.Value ?   0 : Convert.ToInt32(row["SPUR_SEQ"]),
 	Spur_to = row["SPUR_TO"] == DBNull.Value ?   DateTime.MinValue : Convert.ToDateTime(row["SPUR_TO"]),
 	Spur_user = row["SPUR_USER"] == DBNull.Value ?   string.Empty : row["SPUR_USER"].ToString()

	};
}

    }
}
