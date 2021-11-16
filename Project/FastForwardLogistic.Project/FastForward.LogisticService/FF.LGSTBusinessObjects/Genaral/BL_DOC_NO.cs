using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
   public class BL_DOC_NO
    {
       public string bl_doc_no { get; set; }
       public static BL_DOC_NO Converter(DataRow row)
      {
          return new BL_DOC_NO
          {
              bl_doc_no = row["bl_doc_no"] == DBNull.Value ? string.Empty : row["bl_doc_no"].ToString(),
          };
      }
    }
}
