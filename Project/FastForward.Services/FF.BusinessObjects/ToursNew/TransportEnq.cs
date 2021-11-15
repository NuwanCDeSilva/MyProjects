using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class TransportEnq
    {
        public string GCE_ENQ_ID { get; set; }
        public string GCE_REF { get; set; }
        public string GCE_ENQ { get; set; }
        public string GCE_BILL_CUSNAME { get; set; }
        public string GCE_FLY_NO { get; set; }
        public string GCE_FRM_TN { get; set; }
        public DateTime GCE_FLY_DATE { get; set; }
        public string GCE_TO_TN { get; set; }
        public string GCE_DEPOSIT_CHG { get; set; }
        public string Sad_alt_itm_desc { get; set; }
        public string Sad_itm_cd { get; set; }
        public decimal Sad_tot_amt { get; set; }
        public DateTime gce_ret_dt { get; set; }
        public DateTime gce_expect_dt { get; set; }
        public string gce_veh_tp { get; set; }
        public string gce_driver { get; set; }
        public string gce_fleet { get; set; }
         public string gce_cont_per { get; set; }
         public string gce_cont_mob { get; set; }

         public decimal START_MILEAGE { get; set; }
         public decimal END_MILEAGE { get; set; }
         public string TripType { get; set; }

         public string UserLocation { get; set; }
         public string GCE_NAME { get; set; }
        
        





    }
}
