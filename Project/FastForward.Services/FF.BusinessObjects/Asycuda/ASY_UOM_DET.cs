using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_UOM_DET
    {
        public string MSUC_CD { get; set; }
        public string MSU_DESC { get; set; }
         public static ASY_UOM_DET Converter(DataRow row)
         {
             return new ASY_UOM_DET
             {
                 MSUC_CD = row["MSUC_CD"] == DBNull.Value ? string.Empty : row["MSUC_CD"].ToString(),
                 MSU_DESC = row["MSU_DESC"] == DBNull.Value ? string.Empty : row["MSU_DESC"].ToString(),
             };
         }
    }
}
