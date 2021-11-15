using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FF.BusinessObjects.Tours
{
    public enum ToursStatus
    {
        [Description("Approved")]
        Approved = 1,

        [Description("Pending")]
        Pending = 2,

        [Description("Cancel")]
        Cancel = 0,

        [Description("POGenarated")]
        POGenarated = 3,
    }

    public enum EnquiryStages
    {
        [Description("Cancelled")]
        Cancelled = 0,
        [Description("Pending")]
        Pending = 1,
        [Description("Quotation prepared")]
        Quotation_prepaird = 2,
        [Description("Send Quotation to customer")]
        Send_Quotation_to_customer = 3,
        [Description("Customer acceptance for quotation")]
        Customer_acceptance_for_quotation = 4,
        [Description("Invoiced")]
        Invoiced = 5,
        [Description("Customer paid")]
        Customer_paid = 6,
        [Description("Submit to customer")]
        Submit_to_customer = 7,
        [Description("Quotation Approved")]
        Quotation_Approved = 8,
    }
}
