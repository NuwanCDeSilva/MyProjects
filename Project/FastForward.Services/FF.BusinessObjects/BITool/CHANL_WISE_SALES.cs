using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class CHANL_WISE_SALES
    {
        public string CHANNEL_CODE { get; set; }
        public string CHANNEL_DESCRIPTION { get; set; }
        public string PC_CODE { get; set; }
        public string PC_DESCRIPTION { get; set; }
        public string CATE_CD { get; set; }
        public string CATE_DESCRIPTION { get; set; }
        public decimal DELIVERY_SALES { get; set; }
        public decimal DELIVERY_SALES_PREY { get; set; }
        //public decimal INVOICE_SALES { get; set; }
        
        public static CHANL_WISE_SALES Converter(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                CHANNEL_DESCRIPTION = row["CHANNEL_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CHANNEL_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 :Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                //INVOICE_SALES = row["INVOICE_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INVOICE_SALES"].ToString())
            };
        }
        public static CHANL_WISE_SALES ConverterSub(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 :Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                //INVOICE_SALES = row["INVOICE_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INVOICE_SALES"].ToString())
            };
        }

        public static CHANL_WISE_SALES ConverterChnlDelivery(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                CHANNEL_DESCRIPTION = row["CHANNEL_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CHANNEL_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString())
            };
        }
        public static CHANL_WISE_SALES ConverterPcDelivery(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                PC_CODE = row["PC_CODE"] == DBNull.Value ? string.Empty : row["PC_CODE"].ToString(),
                PC_DESCRIPTION = row["PC_DESCRIPTION"] == DBNull.Value ? string.Empty : row["PC_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString())
            };
        }

        public static CHANL_WISE_SALES ConverterChnlDeliveryPre(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                CHANNEL_DESCRIPTION = row["CHANNEL_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CHANNEL_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                DELIVERY_SALES_PREY = row["DELIVERY_SALES_PREY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES_PREY"].ToString())

            };
        }

        public static CHANL_WISE_SALES ConverterPcDeliveryPre(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                PC_CODE = row["PC_CODE"] == DBNull.Value ? string.Empty : row["PC_CODE"].ToString(),
                PC_DESCRIPTION = row["PC_DESCRIPTION"] == DBNull.Value ? string.Empty : row["PC_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                DELIVERY_SALES_PREY = row["DELIVERY_SALES_PREY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES_PREY"].ToString())

            };
        }
        public static CHANL_WISE_SALES ConverterCatChnlDel(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                CHANNEL_DESCRIPTION = row["CHANNEL_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CHANNEL_DESCRIPTION"].ToString(),
                CATE_CD = row["CATE_CD"] == DBNull.Value ? string.Empty : row["CATE_CD"].ToString(),
                CATE_DESCRIPTION = row["CATE_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CATE_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString())
               

            };
        }
        public static CHANL_WISE_SALES ConverterCatPcDel(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                PC_CODE = row["PC_CODE"] == DBNull.Value ? string.Empty : row["PC_CODE"].ToString(),
                PC_DESCRIPTION = row["PC_DESCRIPTION"] == DBNull.Value ? string.Empty : row["PC_DESCRIPTION"].ToString(),
                CATE_CD = row["CATE_CD"] == DBNull.Value ? string.Empty : row["CATE_CD"].ToString(),
                CATE_DESCRIPTION = row["CATE_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CATE_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString())
               
            };
        }
        public static CHANL_WISE_SALES ConverterCatChnlDelpy(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                CHANNEL_DESCRIPTION = row["CHANNEL_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CHANNEL_DESCRIPTION"].ToString(),
                CATE_CD = row["CATE_CD"] == DBNull.Value ? string.Empty : row["CATE_CD"].ToString(),
                CATE_DESCRIPTION = row["CATE_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CATE_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                DELIVERY_SALES_PREY = row["DELIVERY_SALES_PREY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES_PREY"].ToString())


            };
        }
        public static CHANL_WISE_SALES ConverterCatPcDelPy(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                PC_CODE = row["PC_CODE"] == DBNull.Value ? string.Empty : row["PC_CODE"].ToString(),
                PC_DESCRIPTION = row["PC_DESCRIPTION"] == DBNull.Value ? string.Empty : row["PC_DESCRIPTION"].ToString(),
                CATE_CD = row["CATE_CD"] == DBNull.Value ? string.Empty : row["CATE_CD"].ToString(),
                CATE_DESCRIPTION = row["CATE_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CATE_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                DELIVERY_SALES_PREY = row["DELIVERY_SALES_PREY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES_PREY"].ToString())


            };
        }

        public static CHANL_WISE_SALES ConverterSp(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                CHANNEL_DESCRIPTION = row["CHANNEL_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CHANNEL_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                
            };
        }
        public static CHANL_WISE_SALES ConverterSpCate(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                CHANNEL_DESCRIPTION = row["CHANNEL_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CHANNEL_DESCRIPTION"].ToString(),
                CATE_CD = row["CATE_CD"] == DBNull.Value ? string.Empty : row["CATE_CD"].ToString(),
                CATE_DESCRIPTION = row["CATE_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CATE_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                
            };
        }
        public static CHANL_WISE_SALES ConverterSpPy(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                CHANNEL_DESCRIPTION = row["CHANNEL_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CHANNEL_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                DELIVERY_SALES_PREY = row["DELIVERY_SALES_PREY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES_PREY"].ToString())
            };
        }
        public static CHANL_WISE_SALES ConverterSpPyCate(DataRow row)
        {
            return new CHANL_WISE_SALES
            {
                CHANNEL_CODE = row["CHANNEL_CODE"] == DBNull.Value ? string.Empty : row["CHANNEL_CODE"].ToString(),
                CHANNEL_DESCRIPTION = row["CHANNEL_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CHANNEL_DESCRIPTION"].ToString(),
                DELIVERY_SALES = row["DELIVERY_SALES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES"].ToString()),
                CATE_CD = row["CATE_CD"] == DBNull.Value ? string.Empty : row["CATE_CD"].ToString(),
                CATE_DESCRIPTION = row["CATE_DESCRIPTION"] == DBNull.Value ? string.Empty : row["CATE_DESCRIPTION"].ToString(),
                DELIVERY_SALES_PREY = row["DELIVERY_SALES_PREY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DELIVERY_SALES_PREY"].ToString())
            };
        }
    }
   public  class CHANL_WISE_SALES_WITH_PY
    {
        public string CHANNEL_CODE { get; set; }
        public string CHANNEL_DESCRIPTION { get; set; }
        public decimal CY_DELIVERY_SALES { get; set; }
        public decimal CY_INVOICE_SALES { get; set; }
        public decimal PY_DELIVERY_SALES { get; set; }
        public decimal PY_INVOICE_SALES { get; set; }
    }
   public class CHANL_WISE_SALES_WITH_PY_PC
   {
       public string PC_CODE { get; set; }
       public string PC_DESCRIPTION { get; set; }
       public decimal CY_DELIVERY_SALES { get; set; }
       public decimal CY_INVOICE_SALES { get; set; }
       public decimal PY_DELIVERY_SALES { get; set; }
       public decimal PY_INVOICE_SALES { get; set; }
   }


}
