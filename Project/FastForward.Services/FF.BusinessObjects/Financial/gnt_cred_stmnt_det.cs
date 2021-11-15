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
    public class gnt_cred_stmnt_det
    {  
         public Int32 cred_seq { get; set; }
         public Int32 cred_seq_line { get; set; }
         public string cred_std_com {get; set;}
         public string cred_std_pc {get; set;}
          public DateTime cred_sth_dt {get; set;} 
         public string cred_std_bank {get; set;}
         public string cred_std_mid {get; set;}
         public string cred_std_accno {get; set;}
         public string cred_std_doc_tp {get; set;}
         public string cred_std_doc_desc {get; set;}
         public string cred_std_doc_ref {get; set;}
         public Decimal cred_std_sys_val {get; set;}
         public Int32 cred_std_is_realized { get; set; }
          public DateTime cred_std_realized_dt {get; set;} 
         public Decimal cred_std_doc_val {get; set;}
         public string cred_std_doc_bank {get; set;}
         public string cred_std_rmk {get; set;}
         public string cred_std_deposit_bank {get; set;}
         public string cred_std_doc_bank_cd {get; set;}
         public string cred_std_doc_bank_branch {get; set;}
         public Int32 cred_std_is_no_sun { get; set; }
         public Int32 cred_std_is_no_state { get; set; }
         public Int32 cred_std_is_scan { get; set; }
          public string cred_std_cre_by {get; set;}
           public DateTime cred_std_cre_dt {get; set;} 
         public Int32 cred_std_is_new { get; set; }
         public Int32 cred_std_is_extra { get; set; }
         public Decimal cred_std_net {get; set;}
         public Decimal cred_std_bnk_chg {get; set;}
         public Int32 cred_std_sar_seq_no { get; set; }
         public Int32 cred_std_sar_seq_line { get; set; }
         public Int32 cred_std_rec_rpt { get; set; }
          public DateTime cred_std_realized_dt_enterd {get; set;}
          public Decimal cred_settlement_amt { get; set; }
        
    }
}
