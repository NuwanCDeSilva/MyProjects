using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class ref_comm_collect_ovrt
    {
       public Int32 rcco_seq { get; set; }
       public String rcco_docno { get; set; }
       public String rcco_emp_cd { get; set; }
       public String rcco_emp_cat { get; set; }
       public String rcco_inv_tp { get; set; }
       public decimal rcco_st_val { get; set; }
       public decimal rcco_end_val { get; set; }
       public decimal rcco_rate { get; set; }
       public Int32 rcco_stl_st_dt { get; set; }
       public Int32 rcco_stl_end_dt { get; set; }
       public String rcco_anal1 { get; set; }
       public String rcco_anal2 { get; set; }
       public static ref_comm_collect_ovrt Converter(DataRow row)
       {
           return new ref_comm_collect_ovrt
           {
               rcco_seq = row["rcco_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["rcco_seq"].ToString()),
               rcco_docno = row["rcco_docno"] == DBNull.Value ? string.Empty : row["rcco_docno"].ToString(),
               rcco_emp_cd = row["rcco_emp_cd"] == DBNull.Value ? string.Empty : row["rcco_emp_cd"].ToString(),
               rcco_emp_cat = row["rcco_emp_cat"] == DBNull.Value ? string.Empty : row["rcco_emp_cat"].ToString(),
               rcco_inv_tp = row["rcco_inv_tp"] == DBNull.Value ? string.Empty : row["rcco_inv_tp"].ToString(),
               rcco_st_val = row["rcco_st_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcco_st_val"].ToString()),
               rcco_end_val = row["rcco_end_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcco_end_val"].ToString()),
               rcco_rate = row["rcco_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcco_rate"].ToString()),
               rcco_stl_st_dt = row["rcco_stl_st_dt"] == DBNull.Value ? 0 : Convert.ToInt32(row["rcco_stl_st_dt"].ToString()),
               rcco_stl_end_dt = row["rcco_stl_end_dt"] == DBNull.Value ? 0 : Convert.ToInt32(row["rcco_stl_end_dt"].ToString()),
               rcco_anal1 = row["rcco_anal1"] == DBNull.Value ? string.Empty : row["rcco_anal1"].ToString(),
               rcco_anal2 = row["rcco_anal2"] == DBNull.Value ? string.Empty : row["rcco_anal2"].ToString(),

           };
       }
    }
}
