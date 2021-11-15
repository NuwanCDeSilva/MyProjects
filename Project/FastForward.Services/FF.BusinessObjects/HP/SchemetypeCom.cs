using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SchemetypeCom
    {
        
   
        #region Private Members
        private string _HSAC_SCH_TP;
         private int _HSAC_LINE;
         private string _HSAC_SCH_COM;
         private int _HSAC_ACT_STUS;
         private DateTime _HSAC_CRE_DT;
         private string _HSAC_CRE_BY;
         private DateTime _HSAC_MOD_DT;
         private string _HSAC_MOD_BY;
        #endregion

         public string HSAC_SCH_TP
        {
            get { return _HSAC_SCH_TP; }
            set { _HSAC_SCH_TP = value; }
        }
         public int HSAC_LINE
         {
             get { return _HSAC_LINE; }
             set { _HSAC_LINE = value; }
         }
         public string HSAC_SCH_COM
         {
             get { return _HSAC_SCH_COM; }
             set { _HSAC_SCH_COM = value; }
         }
         public int HSAC_ACT_STUS
         {
             get { return _HSAC_ACT_STUS; }
             set { _HSAC_ACT_STUS = value; }
         }
         public DateTime HSAC_CRE_DT
         {
             get { return _HSAC_CRE_DT; }
             set { _HSAC_CRE_DT = value; }
         }
         public string HSAC_CRE_BY
         {
             get { return _HSAC_CRE_BY; }
             set { _HSAC_CRE_BY = value; }
         }
         public DateTime HSAC_MOD_DT
         {
             get { return _HSAC_MOD_DT; }
             set { _HSAC_MOD_DT = value; }
         }
         public string HSAC_MOD_BY
         {
             get { return _HSAC_MOD_BY; }
             set { _HSAC_MOD_BY = value; }
         }


         public static SchemetypeCom Converter(DataRow row)
         {
             return new SchemetypeCom
             {
                 HSAC_SCH_TP = row["HSAC_SCH_TP"] == DBNull.Value ? string.Empty : row["HSAC_SCH_TP"].ToString(),
                 HSAC_LINE = row["HSAC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSAC_LINE"]),
                 HSAC_SCH_COM = row["HSAC_SCH_COM"] == DBNull.Value ? string.Empty : row["HSAC_SCH_COM"].ToString(),
                 HSAC_ACT_STUS = row["HSAC_ACT_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSAC_ACT_STUS"]),
                 HSAC_CRE_DT = row["HSAC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSAC_CRE_DT"]),
                 HSAC_CRE_BY = row["HSAC_CRE_BY"] == DBNull.Value ? string.Empty : row["HSAC_CRE_BY"].ToString(),
                 HSAC_MOD_DT = row["HSAC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSAC_MOD_DT"]),
                 HSAC_MOD_BY = row["HSAC_MOD_BY"] == DBNull.Value ? string.Empty : row["HSAC_MOD_BY"].ToString()

             };
         }

    }
}
