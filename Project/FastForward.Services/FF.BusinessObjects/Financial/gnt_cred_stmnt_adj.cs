using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //table: hpr_ars_dt_defn
    //created by :Shani 21-11-2012
    [Serializable]
    public class gnt_cred_stmnt_adj
    {

        public Int32 cred_seq { get; set; }
        public Int32 cred_sta_seq_line { get; set; }
        public string cred_sta_com {get; set;}
        public string cred_sta_pc {get; set;}
        public DateTime cred_sta_dt{get; set;}
        public string cred_sta_mid {get; set;}
        public string cred_sta_accno {get; set;}
         public string cred_sta_adj_tp {get; set;}

        public Decimal cred_sta_amt {get; set;}
       
        public string cred_sta_refno {get; set;}
        public string cred_sta_rem {get; set;}
        public string cred_sta_cre_by {get; set;}
      
        



    }
}
