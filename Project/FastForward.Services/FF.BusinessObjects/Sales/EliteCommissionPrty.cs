using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public  class EliteCommissionPrty
   {
       #region Private Members
       private string _saec_circular;
       private string _saec_prt_cd;
       private string _saec_prt_tp;
       private Int32 _saec_seq;
       #endregion

       public string Saec_circular
       {
           get { return _saec_circular; }
           set { _saec_circular = value; }
       }
       public string Saec_prt_cd
       {
           get { return _saec_prt_cd; }
           set { _saec_prt_cd = value; }
       }
       public string Saec_prt_tp
       {
           get { return _saec_prt_tp; }
           set { _saec_prt_tp = value; }
       }
       public Int32 Saec_seq
       {
           get { return _saec_seq; }
           set { _saec_seq = value; }
       }

       public static EliteCommissionPrty Converter(DataRow row)
       {
           return new EliteCommissionPrty
           {
               Saec_circular = row["SAEC_CIRCULAR"] == DBNull.Value ? string.Empty : row["SAEC_CIRCULAR"].ToString(),
               Saec_prt_cd = row["SAEC_PRT_CD"] == DBNull.Value ? string.Empty : row["SAEC_PRT_CD"].ToString(),
               Saec_prt_tp = row["SAEC_PRT_TP"] == DBNull.Value ? string.Empty : row["SAEC_PRT_TP"].ToString(),
               Saec_seq = row["SAEC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SEQ"])

           };
       }


   }
}
