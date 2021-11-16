using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class SunAccountall
    {
        public string accnt_code { get; set; }
        public Int32 period { get; set; }
        public Int32 trans_date { get; set; }
        public Int32 jrnal_no { get; set; }
        public Int32 jrnal_line { get; set; }
        public decimal amount { get; set; }
        public string d_c { get; set; }
        public string allocation { get; set; }
        public string jrnal_type { get; set; }
        public string jrnal_srce { get; set; }
        public string treference { get; set; }
        public string descriptn { get; set; }
        public Int32 entry_date { get; set; }
        public Int32 entry_prd { get; set; }
        public Int32 due_date { get; set; }
        public Int32 alloc_ref { get; set; }
        public Int32 alloc_date { get; set; }
        public Int32 alloc_period { get; set; }
        public string asset_ind { get; set; }
        public string asset_code { get; set; }
        public string asset_sub { get; set; }
        public string conv_code { get; set; }
        public decimal conv_rate { get; set; }
        public decimal other_amt { get; set; }
        public string other_dp { get; set; }
        public string cleardown { get; set; }
        public string reversal { get; set; }
        public string loss_gain { get; set; }
        public string rough_flag { get; set; }
        public string in_use_flag { get; set; }
        public string anal_t0 { get; set; }
        public string anal_t1 { get; set; }
        public string anal_t2 { get; set; }
        public string anal_t3 { get; set; }
        public string anal_t4 { get; set; }
        public string anal_t5 { get; set; }
        public string anal_t6 { get; set; }
        public string anal_t7 { get; set; }
        public string anal_t8 { get; set; }
        public string anal_t9 { get; set; }
        public Int32 posting_date { get; set; }
        public string alloc_in_progress { get; set; }
        public Int32 hold_ref { get; set; }
        public string hold_op_id { get; set; }
        public string last_change_user_id { get; set; }
        public Int32 last_change_date { get; set; }
        public string originator_id { get; set; }

        public string Com { get; set; }
        public string doc { get; set; }
        public string ActGRNNo { get; set; }
    }
}
