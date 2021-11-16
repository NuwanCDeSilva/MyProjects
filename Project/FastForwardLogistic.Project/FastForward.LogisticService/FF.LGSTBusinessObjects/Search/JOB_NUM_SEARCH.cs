using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class JOB_NUM_SEARCH
    {
       public string JB_JB_NO { get; set; }
       public string JB_POUCH_NO { get; set; }
       public DateTime JB_JB_DT { get; set; }
       public string JB_STUS { get; set; }
       public string RESULT_COUNT { get; set; }
       public string MBE_CD { get; set; }
       public string MBE_NAME { get; set; }
       public string R__ { get; set; }

       public static JOB_NUM_SEARCH Converter(DataRow row)
       {
           return new JOB_NUM_SEARCH
           {
               JB_JB_NO = row["JB_JB_NO"] == DBNull.Value ? string.Empty : row["JB_JB_NO"].ToString(),
               JB_POUCH_NO = row["JB_POUCH_NO"] == DBNull.Value ? string.Empty : row["JB_POUCH_NO"].ToString(),
               JB_STUS = row["JB_STUS"] == DBNull.Value ? string.Empty : row["JB_STUS"].ToString(),
               RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
               JB_JB_DT = row["JB_JB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JB_JB_DT"].ToString()),
               R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
           };
       }
       public static JOB_NUM_SEARCH ConverterJob(DataRow row)
       {
           return new JOB_NUM_SEARCH
           {
               JB_JB_NO = row["JB_JB_NO"] == DBNull.Value ? string.Empty : row["JB_JB_NO"].ToString(),
               JB_POUCH_NO = row["JB_POUCH_NO"] == DBNull.Value ? string.Empty : row["JB_POUCH_NO"].ToString(),
               RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
               JB_JB_DT = row["JB_JB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JB_JB_DT"].ToString()),
               R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
           };
       }
       public static JOB_NUM_SEARCH ConverterCost(DataRow row)
       {
           return new JOB_NUM_SEARCH
           {
               JB_JB_NO = row["JB_JB_NO"] == DBNull.Value ? string.Empty : row["JB_JB_NO"].ToString(),
               JB_POUCH_NO = row["JB_POUCH_NO"] == DBNull.Value ? string.Empty : row["JB_POUCH_NO"].ToString(),
               JB_STUS = row["JB_STUS"] == DBNull.Value ? string.Empty : row["JB_STUS"].ToString(),
               RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
               JB_JB_DT = row["JB_JB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JB_JB_DT"].ToString()),
               R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
               MBE_CD = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
               MBE_NAME = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
           };
       }
    }
}
