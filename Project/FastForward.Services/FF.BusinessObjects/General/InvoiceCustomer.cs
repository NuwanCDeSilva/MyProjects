using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class InvoiceCustomer
    {
        public string  SAH_INV_NO{get;set;}
        public string  SAH_CUS_NAME{get;set;}
        public string  SAH_CUS_ADD1{get;set;}
        public DateTime  SAH_DT{get;set;}
        public string  SAH_PC{get;set;}
        public string SAH_PC_DESC { get; set; }
        public string  SAH_INV_TP{get;set;}
        public string  SAH_CUS_CD{get;set;}
        public string  SAH_CUS_ADD2{get;set;}
        public string  SAD_ITM_CD{get;set;}
        public decimal  SAD_QTY{get;set;}
        public decimal  SAD_TOT_AMT{get;set;}
        public decimal  SAD_UNIT_AMT{get;set;}
        public decimal  SAD_DISC_AMT{get;set;}
        public decimal  SAD_ITM_TAX_AMT{get;set;}
        public string  SAD_PROMO_CD{get;set;}
        public string  SAH_ACC_NO{get;set;}
        public string  SAD_PBOOK{get;set;}
        public string  SAD_PB_LVL{get;set;}
        public string  SAD_ITM_STUS { get; set; }
        public static InvoiceCustomer Converter(DataRow row)
        {
            return new InvoiceCustomer
            {
                SAH_INV_NO = row["SAH_INV_NO"] == DBNull.Value ? String.Empty :  row["SAH_INV_NO"].ToString(),
                SAH_CUS_NAME = row["SAH_CUS_NAME"] == DBNull.Value ? String.Empty : row["SAH_CUS_NAME"].ToString(),
                SAH_CUS_ADD1 = row["SAH_CUS_ADD1"] == DBNull.Value ? String.Empty : row["SAH_CUS_ADD1"].ToString(),
                SAH_DT = row["SAH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_DT"].ToString()),
                SAH_PC = row["SAH_PC"] == DBNull.Value ? String.Empty : row["SAH_PC"].ToString(),
                SAH_INV_TP = row["SAH_INV_TP"] == DBNull.Value ? String.Empty : row["SAH_INV_TP"].ToString(),
                SAH_CUS_CD = row["SAH_CUS_CD"] == DBNull.Value ? String.Empty : row["SAH_CUS_CD"].ToString(),
                SAH_CUS_ADD2 = row["SAH_CUS_ADD2"] == DBNull.Value ? String.Empty : row["SAH_CUS_ADD2"].ToString(),
                SAD_ITM_CD = row["SAD_ITM_CD"] == DBNull.Value ? String.Empty : row["SAD_ITM_CD"].ToString(),
                SAD_QTY = row["SAD_QTY"] == DBNull.Value ? 0 :Convert.ToDecimal(row["SAD_QTY"].ToString()),
                SAD_TOT_AMT = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"].ToString()),
                SAD_UNIT_AMT = row["SAD_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_AMT"].ToString()),
                SAD_DISC_AMT = row["SAD_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_AMT"].ToString()),
                SAD_ITM_TAX_AMT = row["SAD_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_ITM_TAX_AMT"].ToString()),
                SAD_PROMO_CD = row["SAD_PROMO_CD"] == DBNull.Value ? String.Empty : row["SAD_PROMO_CD"].ToString(),
                SAH_ACC_NO = row["SAH_ACC_NO"] == DBNull.Value ? String.Empty : row["SAH_ACC_NO"].ToString(),
                SAD_PBOOK = row["SAD_PBOOK"] == DBNull.Value ? String.Empty : row["SAD_PBOOK"].ToString(),
                SAD_PB_LVL = row["SAD_PB_LVL"] == DBNull.Value ? String.Empty : row["SAD_PB_LVL"].ToString(),
                SAD_ITM_STUS = row["SAD_ITM_STUS"] == DBNull.Value ? String.Empty : row["SAD_ITM_STUS"].ToString(),
                SAH_PC_DESC = row["SAH_PC_DESC"] == DBNull.Value ? String.Empty : row["SAH_PC_DESC"].ToString()
            };
        }
    }
}
