using System.ComponentModel;
using System.Reflection;

namespace FF.BusinessObjects
{
    // Tharaka 2015-01-13

    public static class GetEnumDesc
    {
        public static string GetDescription(object enumValue, string defDesc)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return defDesc;
        }

        public static string GetEnumDescription(System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }

    public enum NotificationTypes
    {
        GIT = 1,
        AccountReminders = 2,
        DayEndPending = 3,
        ManagerReminders = 4,
        FDSales = 5,
        SalesFigure = 6,
        SalesFigureItem = 7,
        LastDayendprocessedDate = 8,
        AccountStatus = 9,
        AccountStatusItem = 10,
        AllowedRcc = 11,
        OtherShopCollection = 12,
        LastLogOnDate=13
    }

    public enum ThoughtStatus
    {
        Close = 0,
        Current = -1
    }

    internal class CommonEnums
    {
    }

    public enum CommonEnum
    {
        [Description("Job cancel")]
        Job_Cancel = 1,

        [Description("Update job items as under warranty")]
        Update_job_items_as_under_warranty = 2,

        [Description("Job confirmation cancel")]
        Job_confirmation_cancel = 3,

        [Description("Job FOC approval")]
        Job_FOC_approval = 4,

        [Description("Job hold and re-open")]
        Job_hold_and_re_open = 5,

        [Description("Job estimate approval")]
        Job_estimate_approval = 6,

        [Description("Customer warranty claim request approval")]
        Customer_warranty_claim_request_approve = 7,

        [Description("Cancel approved customer warranty claim request")]
        Cancel_appvoed_customer_warranty_clain_request = 8,

        [Description("MRN approval")]
        MRN_Approve = 9,

        [Description("Approved MRN cancel")]
        Cancel_approved_mrn = 10,

        [Description("Discount approval separately for each job")]
        Discount_approval_separately_for_each_job = 11,

        [Description("Service agreement approval")]
        Service_agreement_approcal = 12,

        //Add by akila 2017/07/14 add new enum type 
        [Description("Pending Jobs")]
        Pending_Jobs = 13
    }

    public enum ServiceJobStages
    {
        [Description("REQUEST_ONLY")]
        REQUEST_ONLY = 1,

        [Description("NEW JOB OPEN")]
        NEW_JOB_OPEN = 2,

        //[Description("NEW JOB OPEN - INSPECTION  ")]
        //NEW_JOB_OPEN_INSPECTION = 1.1,

        [Description("TECHNICIAN ALLOCATED")]
        TECHNICIAN_ALLOCATED = 3,

        [Description("JOB STARTED - TECHNICIAN")]
        JOB_STARTED_TECHNICIAN = 4,

        [Description("JOB REOPENED - TECHNICIAN")]
        JOB_REOPENED_TECHNICIAN = 5,

        [Description("JOB COMMENTED - TECHNICIAN")]
        JOB_COMMENTED_TECHNICIAN = 6,

        [Description("JOB COMPLETED")]
        JOB_COMPLETED = 7,

        [Description("JOB COMPLETED - INVOICED")]
        JOB_COMPLETED_INVOICED = 8,

        [Description("ITEM(S) DELEVERED")]
        ITEM_DELEVERED = 11
    }
}