using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class Sms_Ref_Log
    {
        public Int32 Rml_id { get; set; }//Reminder ID
        public String Rml_com { get; set; }//Company
        public String Rml_pc { get; set; }//Profit Center
        public String Rml_loc { get; set; }//Location
        public String Rml_bdoc { get; set; }//job no
        public Int32 Rml_bdoc_line { get; set; }//job line no   
        public String Rml_rdoc { get; set; }//estimate number
        public String Rml_sms { get; set; }//SMS Text
        public String Rml_email { get; set; }//Email Text
        public Int32 Rml_smsseq { get; set; }//SMS Out Seq
        public Int32 Rml_sm_stus { get; set; }
        public Int32 Rml_em_stus { get; set; }
        public String Rml_cre_by { get; set; }
        public DateTime Rml_cre_dt { get; set; } 
    }
}
