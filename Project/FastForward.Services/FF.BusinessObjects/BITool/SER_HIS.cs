using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class SER_HIS
    {
      public string ITS_DOC_NO { get; set; }
      public DateTime ITS_DOC_DT { get; set; }
      public string ITS_COM { get; set; }
      public string ITS_LOC { get; set; }
      public string ITS_ITM_CD { get; set; }
      public string MI_SHORTDESC { get; set; }
      public string ITS_ITM_STUS { get; set; }
      public decimal ITS_UNIT_COST { get; set; }
      public string ITS_SER_1 { get; set; }
      public string ITS_ORIG_GRNNO { get; set; }
      public DateTime ITS_ORIG_GRNDT { get; set; }
      public string ITS_WARR_NO { get; set; }
      public Decimal ITS_WARR_PERIOD { get; set; }
      public string ITH_REMARKS { get; set; }
      
      public static SER_HIS Converter(DataRow row)
      {
          return new SER_HIS
          {
              ITS_DOC_NO = row["ITS_DOC_NO"] == DBNull.Value ? string.Empty : row["ITS_DOC_NO"].ToString(),
              ITH_REMARKS = row["ITH_REMARKS"] == DBNull.Value ? string.Empty : row["ITH_REMARKS"].ToString(),
              ITS_DOC_DT = row["ITS_DOC_DT"] == DBNull.Value ?DateTime.MinValue : Convert.ToDateTime(row["ITS_DOC_DT"].ToString()),
              ITS_COM = row["ITS_COM"] == DBNull.Value ? string.Empty : row["ITS_COM"].ToString(),
              ITS_LOC = row["ITS_LOC"] == DBNull.Value ? string.Empty : row["ITS_LOC"].ToString(),
              ITS_ITM_CD = row["ITS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITS_ITM_CD"].ToString(),
              MI_SHORTDESC = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString(),
              ITS_ITM_STUS = row["ITS_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITS_ITM_STUS"].ToString(),
              ITS_UNIT_COST = row["ITS_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITS_UNIT_COST"].ToString()),
              ITS_SER_1 = row["ITS_SER_1"] == DBNull.Value ? string.Empty : row["ITS_SER_1"].ToString(),
              ITS_ORIG_GRNNO = row["ITS_ORIG_GRNNO"] == DBNull.Value ? string.Empty : row["ITS_ORIG_GRNNO"].ToString(),
              ITS_ORIG_GRNDT = row["ITS_ORIG_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITS_ORIG_GRNDT"].ToString()),
              ITS_WARR_NO = row["ITS_WARR_NO"] == DBNull.Value ? string.Empty : row["ITS_WARR_NO"].ToString(),
              ITS_WARR_PERIOD = row["ITS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITS_WARR_PERIOD"].ToString())
          };
      }
    }
}
