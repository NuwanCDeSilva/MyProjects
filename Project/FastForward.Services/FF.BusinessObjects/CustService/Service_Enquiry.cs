using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Service_Enquiry
    {


    }

    public class Service_Enquiry_Job_Det
    {
        public Int32 JBD_SEQ_NO { get; set; }
        public String JBD_JOBNO { get; set; }
        public String SJB_REQNO { get; set; }
        public String SJB_CUST_CD { get; set; }
        public String SJB_CUST_NAME { get; set; }
        public String IswarrRep { get; set; }
        public static Service_Enquiry_Job_Det Converter(DataRow row)
        {
            return new Service_Enquiry_Job_Det
            {
                JBD_SEQ_NO = row["JBD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SEQ_NO"].ToString()),
                JBD_JOBNO = row["JBD_JOBNO"] == DBNull.Value ? string.Empty : row["JBD_JOBNO"].ToString(),
                SJB_REQNO = row["SJB_REQNO"] == DBNull.Value ? string.Empty : row["SJB_REQNO"].ToString(),
                SJB_CUST_CD = row["SJB_CUST_CD"] == DBNull.Value ? string.Empty : row["SJB_CUST_CD"].ToString(),
                SJB_CUST_NAME = row["SJB_CUST_NAME"] == DBNull.Value ? string.Empty : row["SJB_CUST_NAME"].ToString()
            };
        }
    }

    public class Service_Enquiry_Job_Hdr
    {
        public Int32 SJB_SEQ_NO { get; set; }
        public String SJB_JOBNO { get; set; }
        public DateTime SJB_DT { get; set; }
        public String SJB_REQNO { get; set; }
        public String SJB_JOBTP { get; set; }
        public String SC_DESC { get; set; }
        public String SERS_DESC { get; set; }
        public String SJB_ORDERNO { get; set; }
        public String SJB_CUST_CD { get; set; }
        public String SJB_CUST_NAME { get; set; }
        public String SJB_ADD1 { get; set; }
        public String SJB_ADD2 { get; set; }
        public String SJB_MOBINO { get; set; }
        public String SJB_CNT_PERSON { get; set; }
        public String SJB_B_CUST_CD { get; set; }
        public String SJB_B_CUST_NAME { get; set; }
        public String SJB_B_ADD1 { get; set; }
        public String SJB_B_ADD2 { get; set; }
        public String SJB_B_MOBINO { get; set; }

        public String SJB_TECH_RMK { get; set; }
        public String SJB_JOB_RMK { get; set; }
        public String SJB_CNT_PHNO { get; set; }

        public String SJB_PHNO { get; set; }
        public String SJB_B_PHNO { get; set; }

        public static Service_Enquiry_Job_Hdr Converter(DataRow row)
        {
            return new Service_Enquiry_Job_Hdr
            {
                SJB_SEQ_NO = row["SJB_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SJB_SEQ_NO"].ToString()),
                SJB_JOBNO = row["SJB_JOBNO"] == DBNull.Value ? string.Empty : row["SJB_JOBNO"].ToString(),
                SJB_DT = row["SJB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_DT"].ToString()),
                SJB_REQNO = row["SJB_REQNO"] == DBNull.Value ? string.Empty : row["SJB_REQNO"].ToString(),
                SJB_JOBTP = row["SJB_JOBTP"] == DBNull.Value ? string.Empty : row["SJB_JOBTP"].ToString(),
                SC_DESC = row["SC_DESC"] == DBNull.Value ? string.Empty : row["SC_DESC"].ToString(),
                SERS_DESC = row["SERS_DESC"] == DBNull.Value ? string.Empty : row["SERS_DESC"].ToString(),
                SJB_ORDERNO = row["SJB_ORDERNO"] == DBNull.Value ? string.Empty : row["SJB_ORDERNO"].ToString(),
                SJB_CUST_CD = row["SJB_CUST_CD"] == DBNull.Value ? string.Empty : row["SJB_CUST_CD"].ToString(),
                SJB_CUST_NAME = row["SJB_CUST_NAME"] == DBNull.Value ? string.Empty : row["SJB_CUST_NAME"].ToString(),
                SJB_ADD1 = row["SJB_ADD1"] == DBNull.Value ? string.Empty : row["SJB_ADD1"].ToString(),
                SJB_ADD2 = row["SJB_ADD2"] == DBNull.Value ? string.Empty : row["SJB_ADD2"].ToString(),
                SJB_MOBINO = row["SJB_MOBINO"] == DBNull.Value ? string.Empty : row["SJB_MOBINO"].ToString(),
                SJB_CNT_PERSON = row["SJB_CNT_PERSON"] == DBNull.Value ? string.Empty : row["SJB_CNT_PERSON"].ToString(),
                SJB_B_CUST_CD = row["SJB_B_CUST_CD"] == DBNull.Value ? string.Empty : row["SJB_B_CUST_CD"].ToString(),
                SJB_B_CUST_NAME = row["SJB_B_CUST_NAME"] == DBNull.Value ? string.Empty : row["SJB_B_CUST_NAME"].ToString(),
                SJB_B_ADD1 = row["SJB_B_ADD1"] == DBNull.Value ? string.Empty : row["SJB_B_ADD1"].ToString(),
                SJB_B_ADD2 = row["SJB_B_ADD2"] == DBNull.Value ? string.Empty : row["SJB_B_ADD2"].ToString(),
                SJB_B_MOBINO = row["SJB_B_MOBINO"] == DBNull.Value ? string.Empty : row["SJB_B_MOBINO"].ToString(),
                SJB_TECH_RMK = row["SJB_TECH_RMK"] == DBNull.Value ? string.Empty : row["SJB_TECH_RMK"].ToString(),
                SJB_JOB_RMK = row["SJB_JOB_RMK"] == DBNull.Value ? string.Empty : row["SJB_JOB_RMK"].ToString(),
                SJB_CNT_PHNO = row["SJB_CNT_PHNO"] == DBNull.Value ? string.Empty : row["SJB_CNT_PHNO"].ToString(),

                SJB_PHNO = row["SJB_PHNO"] == DBNull.Value ? string.Empty : row["SJB_PHNO"].ToString(),
                SJB_B_PHNO = row["SJB_B_PHNO"] == DBNull.Value ? string.Empty : row["SJB_B_PHNO"].ToString()

            };
        }
    }

    public class Service_Enquiry_TechAllo_Hdr
    {
        public String STH_EMP_CD { get; set; }
        public String ESEP_FIRST_NAME { get; set; }
        public String ESEP_MOBI_NO { get; set; }
        public String STH_CURR_STUS { get; set; }
        public String STH_STUS { get; set; }
        public String STH_LOC { get; set; }

        public static Service_Enquiry_TechAllo_Hdr Converter(DataRow row)
        {
            return new Service_Enquiry_TechAllo_Hdr
            {
                STH_EMP_CD = row["STH_EMP_CD"] == DBNull.Value ? string.Empty : row["STH_EMP_CD"].ToString(),
                ESEP_FIRST_NAME = row["ESEP_FIRST_NAME"] == DBNull.Value ? string.Empty : row["ESEP_FIRST_NAME"].ToString(),
                ESEP_MOBI_NO = row["ESEP_MOBI_NO"] == DBNull.Value ? string.Empty : row["ESEP_MOBI_NO"].ToString(),
                STH_CURR_STUS = row["STH_CURR_STUS"] == DBNull.Value ? string.Empty : row["STH_CURR_STUS"].ToString(),
                STH_STUS = row["STH_STUS"] == DBNull.Value ? string.Empty : row["STH_STUS"].ToString(),
                STH_LOC = row["STH_LOC"] == DBNull.Value ? string.Empty : row["STH_LOC"].ToString()
            };
        }
    }

    public class Service_Enquiry_Tech_Cmnt
    {
        public String stc_desc { get; set; }
        public String jtc_cmt_rmk { get; set; }
        public static Service_Enquiry_Tech_Cmnt Converter(DataRow row)
        {
            return new Service_Enquiry_Tech_Cmnt
            {
                stc_desc = row["STC_DESC"] == DBNull.Value ? string.Empty : row["STC_DESC"].ToString(),
                jtc_cmt_rmk = row["JTC_CMT_RMK"] == DBNull.Value ? string.Empty : row["JTC_CMT_RMK"].ToString()
            };
        }
    }

    public class Service_Enquiry_StandByItems
    {
        public String STI_DOC { get; set; }
        public String STI_ISSUEITMCD { get; set; }
        public String MI_LONGDESC { get; set; }
        public static Service_Enquiry_StandByItems Converter(DataRow row)
        {
            return new Service_Enquiry_StandByItems
            {
                STI_DOC = row["STI_DOC"] == DBNull.Value ? string.Empty : row["STI_DOC"].ToString(),
                STI_ISSUEITMCD = row["STI_ISSUEITMCD"] == DBNull.Value ? string.Empty : row["STI_ISSUEITMCD"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString()
            };
        }
    }

    public class Service_Enquiry_InventryItems
    {
        public String ITS_ITM_CD { get; set; }
        public String ITS_ITM_STUS { get; set; }
        public String ITS_SER_1 { get; set; }
        public String MI_LONGDESC { get; set; }
        public static Service_Enquiry_InventryItems Converter(DataRow row)
        {
            return new Service_Enquiry_InventryItems
            {
                ITS_ITM_CD = row["ITS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITS_ITM_CD"].ToString(),
                ITS_ITM_STUS = row["ITS_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITS_ITM_STUS"].ToString(),
                ITS_SER_1 = row["ITS_SER_1"] == DBNull.Value ? string.Empty : row["ITS_SER_1"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString()
            };
        }
    }

    public class Service_Enquiry_Estimate_Hdr
    {
        public Int32 ESH_SEQ_NO { get; set; }
        public String ESH_ESTNO { get; set; }
        public String ESH_TP { get; set; }
        public String ESH_DT { get; set; }
        public String RES_DESC { get; set; }
        public String EST_RMK { get; set; }
        public Decimal AMOUNT { get; set; }
        public Decimal DISCOUNT { get; set; }
        public Decimal TAX_AMOUNT { get; set; }
        public Decimal TOT_AMOUNT { get; set; }
        public static Service_Enquiry_Estimate_Hdr Converter(DataRow row)
        {
            return new Service_Enquiry_Estimate_Hdr
            {
                ESH_SEQ_NO = row["ESH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESH_SEQ_NO"].ToString()),
                ESH_ESTNO = row["ESH_ESTNO"] == DBNull.Value ? string.Empty : row["ESH_ESTNO"].ToString(),
                ESH_TP = row["ESH_TP"] == DBNull.Value ? string.Empty : row["ESH_TP"].ToString(),
                ESH_DT = row["ESH_DT"] == DBNull.Value ? string.Empty : row["ESH_DT"].ToString(),
                RES_DESC = row["RES_DESC"] == DBNull.Value ? string.Empty : row["RES_DESC"].ToString(),
                EST_RMK = row["EST_RMK"] == DBNull.Value ? string.Empty : row["EST_RMK"].ToString()
            };
        }
    }

    public class Service_Enquiry_Estimate_Items
    {
        public Int32 ESI_SEQ_NO { get; set; }
        public String ESI_ESTNO { get; set; }
        public String ESI_JOBNO { get; set; }
        public Int32 ESI_JOBLINE { get; set; }
        public Int32 ESI_LINE { get; set; }
        public String ESI_ITM_CD { get; set; }
        public String MI_LONGDESC { get; set; }
        public String MSTP_DESC { get; set; }
        public Decimal ESI_QTY { get; set; }
        public Decimal ESI_DISC_AMT { get; set; }
        public Decimal ESI_UNIT_RT { get; set; }
        public Decimal ESI_TAX_AMT { get; set; }
        public Decimal ESI_NET { get; set; }
        public static Service_Enquiry_Estimate_Items Converter(DataRow row)
        {
            return new Service_Enquiry_Estimate_Items
            {
                ESI_SEQ_NO = row["ESI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESI_SEQ_NO"].ToString()),
                ESI_ESTNO = row["ESI_ESTNO"] == DBNull.Value ? string.Empty : row["ESI_ESTNO"].ToString(),
                ESI_JOBNO = row["ESI_JOBNO"] == DBNull.Value ? string.Empty : row["ESI_JOBNO"].ToString(),
                ESI_JOBLINE = row["ESI_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESI_JOBLINE"].ToString()),
                ESI_LINE = row["ESI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ESI_LINE"].ToString()),
                ESI_ITM_CD = row["ESI_ITM_CD"] == DBNull.Value ? string.Empty : row["ESI_ITM_CD"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                MSTP_DESC = row["MSTP_DESC"] == DBNull.Value ? string.Empty : row["MSTP_DESC"].ToString(),
                ESI_QTY = row["ESI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESI_QTY"].ToString()),
                ESI_DISC_AMT = row["ESI_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESI_DISC_AMT"].ToString()),
                ESI_UNIT_RT = row["ESI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESI_UNIT_RT"].ToString()),
                ESI_TAX_AMT = row["ESI_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESI_TAX_AMT"].ToString()),
                ESI_NET = row["ESI_NET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESI_NET"].ToString())
            };
        }
    }

    public class Service_Enquiry_Estimate_TAX
    {
        public String ESV_ITM_CD { get; set; }
        public String ESV_TAX_TP { get; set; }
        public Decimal ESV_TAX_RT { get; set; }
        public Decimal ESV_TAX_AMT { get; set; }
        public static Service_Enquiry_Estimate_TAX Converter(DataRow row)
        {
            return new Service_Enquiry_Estimate_TAX
            {
                ESV_ITM_CD = row["ESV_ITM_CD"] == DBNull.Value ? string.Empty : row["ESV_ITM_CD"].ToString(),
                ESV_TAX_TP = row["ESV_TAX_TP"] == DBNull.Value ? string.Empty : row["ESV_TAX_TP"].ToString(),
                ESV_TAX_RT = row["ESV_TAX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESV_TAX_RT"].ToString()),
                ESV_TAX_AMT = row["ESV_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESV_TAX_AMT"].ToString())
            };
        }
    }

    public class Service_Enquiry_Invoice_Items
    {
        public String SAH_INV_NO { get; set; }
        public DateTime SAH_DT { get; set; }
        public String RSS_DESC { get; set; }
        public String SAD_ITM_CD { get; set; }
        public String MI_LONGDESC { get; set; }
        public String MSTP_DESC { get; set; }
        public Decimal SAD_QTY { get; set; }
        public Decimal SAD_UNIT_RT { get; set; }
        public Decimal SAD_TOT_AMT { get; set; }
        public Decimal SAD_ITM_TAX_AMT { get; set; }
        public Decimal SAD_DISC_AMT { get; set; }
        public Int32 SAD_ITM_LINE { get; set; }
        public String SAD_INV_NO { get; set; }

        public static Service_Enquiry_Invoice_Items Converter(DataRow row)
        {
            return new Service_Enquiry_Invoice_Items
            {
                SAH_INV_NO = row["SAH_INV_NO"] == DBNull.Value ? string.Empty : row["SAH_INV_NO"].ToString(),
                SAH_DT = row["SAH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_DT"].ToString()),
                RSS_DESC = row["RSS_DESC"] == DBNull.Value ? string.Empty : row["RSS_DESC"].ToString(),
                SAD_ITM_CD = row["SAD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ITM_CD"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                MSTP_DESC = row["MSTP_DESC"] == DBNull.Value ? string.Empty : row["MSTP_DESC"].ToString(),
                SAD_QTY = row["SAD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_QTY"].ToString()),
                SAD_UNIT_RT = row["SAD_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_RT"].ToString()),
                SAD_TOT_AMT = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"].ToString()),
                SAD_ITM_TAX_AMT = row["SAD_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_ITM_TAX_AMT"].ToString()),
                SAD_DISC_AMT = row["SAD_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_AMT"].ToString()),
                SAD_ITM_LINE = row["SAD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_LINE"].ToString()),
                SAD_INV_NO = row["SAD_INV_NO"] == DBNull.Value ? string.Empty : row["SAD_INV_NO"].ToString()
            };
        }
    }

    public class Service_Enquiry_Invoice_Header
    {
        public String InvoiceNum { get; set; }
        public DateTime Date { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Discount { get; set; }
        public Decimal TAXAmount { get; set; }
        public String Status { get; set; }
        public static Service_Enquiry_Invoice_Header Converter(DataRow row)
        {
            return new Service_Enquiry_Invoice_Header
            {
                InvoiceNum = row["INVOICENUM"] == DBNull.Value ? string.Empty : row["INVOICENUM"].ToString(),
                Date = row["DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DATE"].ToString()),
                Amount = row["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AMOUNT"].ToString()),
                Discount = row["DISCOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DISCOUNT"].ToString()),
                TAXAmount = row["TAXAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TAXAMOUNT"].ToString()),
                Status = row["STATUS"] == DBNull.Value ? string.Empty : row["STATUS"].ToString()
            };
        }
    }

    public class Service_Enquiry_Invoice_TAX
    {
        public String SATX_ITM_CD { get; set; }
        public String SATX_ITM_TAX_TP { get; set; }
        public Decimal SATX_ITM_TAX_RT { get; set; }
        public Decimal SATX_ITM_TAX_AMT { get; set; }
        public static Service_Enquiry_Invoice_TAX Converter(DataRow row)
        {
            return new Service_Enquiry_Invoice_TAX
            {
                SATX_ITM_CD = row["SATX_ITM_CD"] == DBNull.Value ? string.Empty : row["SATX_ITM_CD"].ToString(),
                SATX_ITM_TAX_TP = row["SATX_ITM_TAX_TP"] == DBNull.Value ? string.Empty : row["SATX_ITM_TAX_TP"].ToString(),
                SATX_ITM_TAX_RT = row["SATX_ITM_TAX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SATX_ITM_TAX_RT"].ToString()),
                SATX_ITM_TAX_AMT = row["SATX_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SATX_ITM_TAX_AMT"].ToString())
            };
        }
    }

    public class Service_Enquiry_PartTrasferd
    {
        public String ITH_DOC_NO { get; set; }
        public DateTime ITH_DOC_DATE { get; set; }
        public String ITS_ITM_CD { get; set; }
        public String ITS_ITM_STUS { get; set; }
        public String ITS_SER_1 { get; set; }
        public String MI_LONGDESC { get; set; }
        public Decimal ITB_QTY { get; set; }
        public String ITH_DIRECT { get; set; }
        public String ITH_LOC { get; set; }
        public String ITH_OTH_LOC { get; set; }

        public static Service_Enquiry_PartTrasferd Converter(DataRow row)
        {
            return new Service_Enquiry_PartTrasferd
            {
                ITH_DOC_NO = row["ITH_DOC_NO"] == DBNull.Value ? string.Empty : row["ITH_DOC_NO"].ToString(),
                ITH_DOC_DATE = row["ITH_DOC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_DOC_DATE"].ToString()),
                ITS_ITM_CD = row["ITS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITS_ITM_CD"].ToString(),
                ITS_ITM_STUS = row["ITS_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITS_ITM_STUS"].ToString(),
                ITS_SER_1 = row["ITS_SER_1"] == DBNull.Value ? string.Empty : row["ITS_SER_1"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                ITB_QTY = row["ITB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_QTY"].ToString()),
                ITH_DIRECT = row["ITH_DIRECT"] == DBNull.Value ? string.Empty : row["ITH_DIRECT"].ToString(),
                ITH_LOC = row["ITH_LOC"] == DBNull.Value ? string.Empty : row["ITH_LOC"].ToString()
            };
        }
        public static Service_Enquiry_PartTrasferd ConverterNew(DataRow row)
        {
            return new Service_Enquiry_PartTrasferd
            {
                ITH_DOC_NO = row["ITH_DOC_NO"] == DBNull.Value ? string.Empty : row["ITH_DOC_NO"].ToString(),
                ITH_DOC_DATE = row["ITH_DOC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_DOC_DATE"].ToString()),
                ITS_ITM_CD = row["ITS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITS_ITM_CD"].ToString(),
                ITS_ITM_STUS = row["ITS_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITS_ITM_STUS"].ToString(),
                ITS_SER_1 = row["ITS_SER_1"] == DBNull.Value ? string.Empty : row["ITS_SER_1"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                ITB_QTY = row["ITB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_QTY"].ToString()),
                ITH_DIRECT = row["ITH_DIRECT"] == DBNull.Value ? string.Empty : row["ITH_DIRECT"].ToString(),
                ITH_LOC = row["ITH_LOC"] == DBNull.Value ? string.Empty : row["ITH_LOC"].ToString(),
                ITH_OTH_LOC = row["ITH_OTH_LOC"] == DBNull.Value ? string.Empty : row["ITH_OTH_LOC"].ToString()
            };
        }
    }

    public class Service_Enquiry_SupplierWrntyClaim
    {
        public Int32 JBD_SEQ_NO { get; set; }
        public String JBD_JOBNO { get; set; }
        public Int32 JBD_JOBLINE { get; set; }
        public DateTime SJB_DT { get; set; }
        public String JBD_ITM_CD { get; set; }
        public String JBD_ITM_STUS { get; set; }
        public String JBD_SER1 { get; set; }
        public String TYPE { get; set; }
        public static Service_Enquiry_SupplierWrntyClaim Converter(DataRow row)
        {
            return new Service_Enquiry_SupplierWrntyClaim
            {
                JBD_SEQ_NO = row["JBD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_SEQ_NO"].ToString()),
                JBD_JOBNO = row["JBD_JOBNO"] == DBNull.Value ? string.Empty : row["JBD_JOBNO"].ToString(),
                JBD_JOBLINE = row["JBD_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBD_JOBLINE"].ToString()),
                SJB_DT = row["SJB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJB_DT"].ToString()),
                JBD_ITM_CD = row["JBD_ITM_CD"] == DBNull.Value ? string.Empty : row["JBD_ITM_CD"].ToString(),
                JBD_ITM_STUS = row["JBD_ITM_STUS"] == DBNull.Value ? string.Empty : row["JBD_ITM_STUS"].ToString(),
                JBD_SER1 = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),
                TYPE = row["TYPE"] == DBNull.Value ? string.Empty : row["TYPE"].ToString()
            };
        }
    }

    public class Service_Enquiry_SupplierWrntyDetails
    {
        public String SWD_DOC_NO { get; set; }
        public String SWD_ITMCD { get; set; }
        public String SWD_SUPPITMCD { get; set; }
        public String SWD_SER1 { get; set; }
        public String SWD_WARRNO { get; set; }
        public String SWD_SUPPWARRNO { get; set; }
        public String SWD_OEMSERNO { get; set; }
        public String SWD_CASEID { get; set; }
        public String SWD_OTHDOCNO { get; set; }
        public String SWD_ITM_STUS { get; set; }
        public String SWC_DOC_NO { get; set; }
        public DateTime SWC_DT { get; set; }
        public String SWC_TP { get; set; }
        public String SWC_SUPP { get; set; }
        public String SWC_CLM_SUPP { get; set; }
        public String SWC_OTHDOCNO { get; set; }
        public String SWC_RMKS { get; set; }
        public String SWC_AIR_BILL_NO { get; set; }
        public DateTime SWC_BILL_DT { get; set; }
        public String SWC_STUS { get; set; }


        public String SWC_HOLD_REASON { get; set; }
        public String SWC_REC_TYPE { get; set; }
        public DateTime SWC_ETA { get; set; }


        public static Service_Enquiry_SupplierWrntyDetails Converter(DataRow row)
        {
            return new Service_Enquiry_SupplierWrntyDetails
            {
                SWD_DOC_NO = row["SWD_DOC_NO"] == DBNull.Value ? string.Empty : row["SWD_DOC_NO"].ToString(),
                SWD_ITMCD = row["SWD_ITMCD"] == DBNull.Value ? string.Empty : row["SWD_ITMCD"].ToString(),
                SWD_SUPPITMCD = row["SWD_SUPPITMCD"] == DBNull.Value ? string.Empty : row["SWD_SUPPITMCD"].ToString(),
                SWD_SER1 = row["SWD_SER1"] == DBNull.Value ? string.Empty : row["SWD_SER1"].ToString(),
                SWD_WARRNO = row["SWD_WARRNO"] == DBNull.Value ? string.Empty : row["SWD_WARRNO"].ToString(),
                SWD_SUPPWARRNO = row["SWD_SUPPWARRNO"] == DBNull.Value ? string.Empty : row["SWD_SUPPWARRNO"].ToString(),
                SWD_OEMSERNO = row["SWD_OEMSERNO"] == DBNull.Value ? string.Empty : row["SWD_OEMSERNO"].ToString(),
                SWD_CASEID = row["SWD_CASEID"] == DBNull.Value ? string.Empty : row["SWD_CASEID"].ToString(),
                SWD_OTHDOCNO = row["SWD_OTHDOCNO"] == DBNull.Value ? string.Empty : row["SWD_OTHDOCNO"].ToString(),
                SWD_ITM_STUS = row["SWD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SWD_ITM_STUS"].ToString(),
                SWC_DOC_NO = row["SWC_DOC_NO"] == DBNull.Value ? string.Empty : row["SWC_DOC_NO"].ToString(),
                SWC_DT = row["SWC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWC_DT"].ToString()),
                SWC_TP = row["SWC_TP"] == DBNull.Value ? string.Empty : row["SWC_TP"].ToString(),
                SWC_SUPP = row["SWC_SUPP"] == DBNull.Value ? string.Empty : row["SWC_SUPP"].ToString(),
                SWC_CLM_SUPP = row["SWC_CLM_SUPP"] == DBNull.Value ? string.Empty : row["SWC_CLM_SUPP"].ToString(),
                SWC_OTHDOCNO = row["SWC_OTHDOCNO"] == DBNull.Value ? string.Empty : row["SWC_OTHDOCNO"].ToString(),
                SWC_RMKS = row["SWC_RMKS"] == DBNull.Value ? string.Empty : row["SWC_RMKS"].ToString(),
                SWC_AIR_BILL_NO = row["SWC_AIR_BILL_NO"] == DBNull.Value ? string.Empty : row["SWC_AIR_BILL_NO"].ToString(),
                SWC_BILL_DT = row["SWC_BILL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWC_BILL_DT"].ToString()),
                SWC_STUS = row["SWC_STUS"] == DBNull.Value ? string.Empty : row["SWC_STUS"].ToString(),
                SWC_HOLD_REASON = row["SWC_HOLD_REASON"] == DBNull.Value ? string.Empty : row["SWC_HOLD_REASON"].ToString(),
                SWC_REC_TYPE = row["SWC_REC_TYPE"] == DBNull.Value ? string.Empty : row["SWC_REC_TYPE"].ToString(),
                SWC_ETA = row["SWC_ETA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWC_ETA"].ToString()),
                
            };
        }
    }

    public class Service_Enquiry_ConfDetails
    {
        public Int32 JCD_SEQ { get; set; }
        public String JCD_NO { get; set; }
        public Int32 JCD_LINE { get; set; }
        public String JCD_JOBNO { get; set; }
        public Int32 JCD_JOBLINENO { get; set; }
        public String JCD_ITMCD { get; set; }
        public String JCD_ITMSTUS { get; set; }
        public String JCD_JOBITMSER { get; set; }
        public Decimal JCD_QTY { get; set; }
        public Decimal JCD_BALQTY { get; set; }
        public String JCD_PB { get; set; }
        public String JCD_PBLVL { get; set; }
        public Decimal JCD_UNITPRICE { get; set; }
        public Decimal JCD_AMT { get; set; }
        public static Service_Enquiry_ConfDetails Converter(DataRow row)
        {
            return new Service_Enquiry_ConfDetails
            {
                JCD_SEQ = row["JCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_SEQ"].ToString()),
                JCD_NO = row["JCD_NO"] == DBNull.Value ? string.Empty : row["JCD_NO"].ToString(),
                JCD_LINE = row["JCD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_LINE"].ToString()),
                JCD_JOBNO = row["JCD_JOBNO"] == DBNull.Value ? string.Empty : row["JCD_JOBNO"].ToString(),
                JCD_JOBLINENO = row["JCD_JOBLINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_JOBLINENO"].ToString()),
                JCD_ITMCD = row["JCD_ITMCD"] == DBNull.Value ? string.Empty : row["JCD_ITMCD"].ToString(),
                JCD_ITMSTUS = row["JCD_ITMSTUS"] == DBNull.Value ? string.Empty : row["JCD_ITMSTUS"].ToString(),
                JCD_JOBITMSER = row["JCD_JOBITMSER"] == DBNull.Value ? string.Empty : row["JCD_JOBITMSER"].ToString(),
                JCD_QTY = row["JCD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_QTY"].ToString()),
                JCD_BALQTY = row["JCD_BALQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_BALQTY"].ToString()),
                JCD_PB = row["JCD_PB"] == DBNull.Value ? string.Empty : row["JCD_PB"].ToString(),
                JCD_PBLVL = row["JCD_PBLVL"] == DBNull.Value ? string.Empty : row["JCD_PBLVL"].ToString(),
                JCD_UNITPRICE = row["JCD_UNITPRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_UNITPRICE"].ToString()),
                JCD_AMT = row["JCD_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_AMT"].ToString())
            };
        }
    }

    public class Service_Enquiry_CostSheet
    {
        public Int32 CS_SEQNO { get; set; }
        public Int32 CS_LINE { get; set; }
        public String CS_JOBNO { get; set; }
        public Int32 CS_JOBLINENO { get; set; }
        public String CS_JOBITMCD { get; set; }
        public String CS_JOBITMSER { get; set; }
        public String CS_ITMCD { get; set; }
        public String CS_ITMSTUS { get; set; }
        public String CS_ITMSER { get; set; }
        public Decimal CS_QTY { get; set; }
        public String CS_DIRECT { get; set; }
        public String CS_ITMDESC { get; set; }
        public static Service_Enquiry_CostSheet Converter(DataRow row)
        {
            return new Service_Enquiry_CostSheet
            {
                CS_SEQNO = row["CS_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["CS_SEQNO"].ToString()),
                CS_LINE = row["CS_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CS_LINE"].ToString()),
                CS_JOBNO = row["CS_JOBNO"] == DBNull.Value ? string.Empty : row["CS_JOBNO"].ToString(),
                CS_JOBLINENO = row["CS_JOBLINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["CS_JOBLINENO"].ToString()),
                CS_JOBITMCD = row["CS_JOBITMCD"] == DBNull.Value ? string.Empty : row["CS_JOBITMCD"].ToString(),
                CS_JOBITMSER = row["CS_JOBITMSER"] == DBNull.Value ? string.Empty : row["CS_JOBITMSER"].ToString(),
                CS_ITMCD = row["CS_ITMCD"] == DBNull.Value ? string.Empty : row["CS_ITMCD"].ToString(),
                CS_ITMSTUS = row["CS_ITMSTUS"] == DBNull.Value ? string.Empty : row["CS_ITMSTUS"].ToString(),
                CS_ITMSER = row["CS_ITMSER"] == DBNull.Value ? string.Empty : row["CS_ITMSER"].ToString(),
                CS_QTY = row["CS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CS_QTY"].ToString()),
                CS_DIRECT = row["CS_DIRECT"] == DBNull.Value ? string.Empty : row["CS_DIRECT"].ToString(),
                CS_ITMDESC = row["CS_ITMDESC"] == DBNull.Value ? string.Empty : row["CS_ITMDESC"].ToString()
            };
        }
    }

    public class Service_Enquiry_WarrtyReplacement
    {
        public String ITH_DOC_NO { get; set; }
        public DateTime ITH_DOC_DATE { get; set; }
        public String ITS_ITM_CD { get; set; }
        public String MIS_DESC { get; set; }
        public String ITS_SER_1 { get; set; }
        public String MI_LONGDESC { get; set; }
        public Decimal ITB_QTY { get; set; }
        public static Service_Enquiry_WarrtyReplacement Converter(DataRow row)
        {
            return new Service_Enquiry_WarrtyReplacement
            {
                ITH_DOC_NO = row["ITH_DOC_NO"] == DBNull.Value ? string.Empty : row["ITH_DOC_NO"].ToString(),
                ITH_DOC_DATE = row["ITH_DOC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_DOC_DATE"].ToString()),
                ITS_ITM_CD = row["ITS_ITM_CD"] == DBNull.Value ? string.Empty : row["ITS_ITM_CD"].ToString(),
                MIS_DESC = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString(),
                ITS_SER_1 = row["ITS_SER_1"] == DBNull.Value ? string.Empty : row["ITS_SER_1"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                ITB_QTY = row["ITB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_QTY"].ToString())
            };
        }
    }

    public class Service_Enquiry_CustCollectionData
    {
        public String SGP_DOC { get; set; }
        public DateTime SGP_DT { get; set; }
        public String SGP_RMK { get; set; }
        public String JBD_ITM_CD { get; set; }
        public String JBD_ITM_DESC { get; set; }
        public String MIS_DESC { get; set; }
        public String JBD_SER1 { get; set; }
        public Decimal QTY { get; set; }
        public static Service_Enquiry_CustCollectionData Converter(DataRow row)
        {
            return new Service_Enquiry_CustCollectionData
            {
                SGP_DOC = row["SGP_DOC"] == DBNull.Value ? string.Empty : row["SGP_DOC"].ToString(),
                SGP_DT = row["SGP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SGP_DT"].ToString()),
                SGP_RMK = row["SGP_RMK"] == DBNull.Value ? string.Empty : row["SGP_RMK"].ToString(),
                JBD_ITM_CD = row["JBD_ITM_CD"] == DBNull.Value ? string.Empty : row["JBD_ITM_CD"].ToString(),
                JBD_ITM_DESC = row["JBD_ITM_DESC"] == DBNull.Value ? string.Empty : row["JBD_ITM_DESC"].ToString(),
                MIS_DESC = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString(),
                JBD_SER1 = row["JBD_SER1"] == DBNull.Value ? string.Empty : row["JBD_SER1"].ToString(),
                QTY = row["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY"].ToString())
            };
        }
    }

    public class _Service_Enquiry_StageLog
    {
        public String JBS_DESC { get; set; }
        public DateTime SJL_CRE_DT { get; set; }
        public static _Service_Enquiry_StageLog Converter(DataRow row)
        {
            return new _Service_Enquiry_StageLog
            {
                JBS_DESC = row["JBS_DESC"] == DBNull.Value ? string.Empty : row["JBS_DESC"].ToString(),
                SJL_CRE_DT = row["SJL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJL_CRE_DT"].ToString())
            };
        }

       
    }

    public class _Service_Enquiry_StageLog_stage
    {
        public String JBS_DESC { get; set; }
        public Decimal SJL_JOBSTAGE { get; set; }
        public DateTime SJL_CRE_DT { get; set; }
        public String duration { get; set; }
        public String estimate_duration { get; set; }
        public static _Service_Enquiry_StageLog_stage Converter(DataRow row)
        {
            return new _Service_Enquiry_StageLog_stage
            {
                JBS_DESC = row["JBS_DESC"] == DBNull.Value ? string.Empty : row["JBS_DESC"].ToString(),
                SJL_CRE_DT = row["SJL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJL_CRE_DT"].ToString()),
                SJL_JOBSTAGE = row["SJL_JOBSTAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SJL_JOBSTAGE"].ToString())
            };
        }
    }

    public class Service_Enquiry_Inssuarance
    {
        public String SVIT_REF_NO { get; set; }
        public String SVIT_INS_VAL { get; set; }
        public DateTime SVIT_DT { get; set; }
        public String SVIT_PC { get; set; }
        public static Service_Enquiry_Inssuarance Converter(DataRow row)
        {
            return new Service_Enquiry_Inssuarance
            {
                SVIT_REF_NO = row["SVIT_REF_NO"] == DBNull.Value ? string.Empty : row["SVIT_REF_NO"].ToString(),
                SVIT_INS_VAL = row["SVIT_INS_VAL"] == DBNull.Value ? string.Empty : row["SVIT_INS_VAL"].ToString(),
                SVIT_DT = row["SVIT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_DT"].ToString()),
                SVIT_PC = row["SVIT_PC"] == DBNull.Value ? string.Empty : row["SVIT_PC"].ToString()
            };
        }
    }


    public class _Service_Enquiry_StageLog_stage_list
    {
        public String JBS_DESC { get; set; }
        public Decimal SJL_JOBSTAGE { get; set; }
        public DateTime SJL_CRE_DT { get; set; }
        public String duration { get; set; }
        public String estimate_duration { get; set; }
        public static _Service_Enquiry_StageLog_stage_list Converter(DataRow row)
        {
            return new _Service_Enquiry_StageLog_stage_list
            {
                JBS_DESC = row["JBS_DESC"] == DBNull.Value ? string.Empty : row["JBS_DESC"].ToString(),
                SJL_CRE_DT = row["SJL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJL_CRE_DT"].ToString()),
                SJL_JOBSTAGE = row["SJL_JOBSTAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SJL_JOBSTAGE"].ToString())
            };
        }
    }



}
