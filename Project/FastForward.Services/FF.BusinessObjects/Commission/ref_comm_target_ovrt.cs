using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class ref_comm_target_ovrt
    {
       public Int32 rcto_seq { get; set; }
       public String rcto_docno { get; set; }
       public String rcto_emp_cd { get; set; }
       public String rcto_emp_cat { get; set; }
       public decimal rcto_st_val { get; set; }
       public decimal rcto_end_val { get; set; }
       public decimal rcto_rate { get; set; }
       public string rcto_anal1 { get; set; }
       public string rcto_anal2 { get; set; }
       public static ref_comm_target_ovrt Converter(DataRow row)
       {
           return new ref_comm_target_ovrt
           {
               rcto_seq = row["rcto_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["rcto_seq"].ToString()),
               rcto_docno = row["rcto_docno"] == DBNull.Value ? string.Empty : row["rcto_docno"].ToString(),
               rcto_emp_cd = row["rcto_emp_cd"] == DBNull.Value ? string.Empty : row["rcto_emp_cd"].ToString(),
               rcto_emp_cat = row["rcto_emp_cat"] == DBNull.Value ? string.Empty : row["rcto_emp_cat"].ToString(),
               rcto_st_val = row["rcto_st_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcto_st_val"].ToString()),
               rcto_end_val = row["rcto_end_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcto_end_val"].ToString()),
               rcto_rate = row["rcto_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcto_rate"].ToString()),
               rcto_anal1 = row["rcto_anal1"] == DBNull.Value ? string.Empty : row["rcto_anal1"].ToString(),
               rcto_anal2 = row["rcto_anal2"] == DBNull.Value ? string.Empty : row["rcto_anal2"].ToString(),

           };
       }
    }
}
