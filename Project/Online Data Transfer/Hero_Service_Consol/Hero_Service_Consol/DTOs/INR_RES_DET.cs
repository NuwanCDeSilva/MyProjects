﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hero_Service_Consol.DTOs
{
    public class INR_RES_DET
    {
        public Int32 IRD_SEQ { get; set; }
        public String IRD_RES_NO { get; set; }
        public Int32 IRD_LINE { get; set; }
        public String IRD_ITM_CD { get; set; }
        public String IRD_ITM_STUS { get; set; }
        public Decimal IRD_RES_QTY { get; set; }
        public Decimal IRD_RES_BQTY { get; set; }
        public Decimal IRD_RES_CQTY { get; set; }
        public Decimal IRD_REQ_QTY { get; set; }
        public Decimal IRD_REQ_BQTY { get; set; }
        public Decimal IRD_REQ_CQTY { get; set; }
        public Int32 IRD_ACT { get; set; }
        public String IRD_RESREQ_NO { get; set; }
        public Int32 IRD_RESREQ_LINE { get; set; }
        public String MI_MODEL { get; set; }
        public String MIS_DESC { get; set; }

        public String BL_NO { get; set; }//15/mar/2016
        public String LOC_CD { get; set; }//15/mar/2016
        public decimal Ird_so_mrn_bqty { get; set; }
        public decimal Ird_ava_bal_for_mrn { get; set; }
        public decimal IRD_MRN_AVA_BAL { get; set; }
        public static INR_RES_DET Converter(DataRow row)
        {
            return new INR_RES_DET
            {
                IRD_SEQ = row["IRD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_SEQ"].ToString()),
                IRD_RES_NO = row["IRD_RES_NO"] == DBNull.Value ? string.Empty : row["IRD_RES_NO"].ToString(),
                IRD_LINE = row["IRD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_LINE"].ToString()),
                IRD_ITM_CD = row["IRD_ITM_CD"] == DBNull.Value ? string.Empty : row["IRD_ITM_CD"].ToString(),
                IRD_ITM_STUS = row["IRD_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRD_ITM_STUS"].ToString(),
                IRD_RES_QTY = row["IRD_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_RES_QTY"].ToString()),
                IRD_RES_BQTY = row["IRD_RES_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_RES_BQTY"].ToString()),
                IRD_RES_CQTY = row["IRD_RES_CQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_RES_CQTY"].ToString()),
                IRD_REQ_QTY = row["IRD_REQ_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_REQ_QTY"].ToString()),
                IRD_REQ_BQTY = row["IRD_REQ_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_REQ_BQTY"].ToString()),
                IRD_REQ_CQTY = row["IRD_REQ_CQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_REQ_CQTY"].ToString()),
                IRD_ACT = row["IRD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_ACT"].ToString()),
                IRD_RESREQ_NO = row["IRD_RESREQ_NO"] == DBNull.Value ? string.Empty : row["IRD_RESREQ_NO"].ToString(),
                IRD_RESREQ_LINE = row["IRD_RESREQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_RESREQ_LINE"].ToString()),
                MI_MODEL = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                MIS_DESC = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString()
            };
        }

        public static INR_RES_DET ConverterNew(DataRow row)
        {
            return new INR_RES_DET
            {
                IRD_SEQ = row["IRD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_SEQ"].ToString()),
                IRD_RES_NO = row["IRD_RES_NO"] == DBNull.Value ? string.Empty : row["IRD_RES_NO"].ToString(),
                IRD_LINE = row["IRD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_LINE"].ToString()),
                IRD_ITM_CD = row["IRD_ITM_CD"] == DBNull.Value ? string.Empty : row["IRD_ITM_CD"].ToString(),
                IRD_ITM_STUS = row["IRD_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRD_ITM_STUS"].ToString(),
                IRD_RES_QTY = row["IRD_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_RES_QTY"].ToString()),
                IRD_RES_BQTY = row["IRD_RES_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_RES_BQTY"].ToString()),
                IRD_RES_CQTY = row["IRD_RES_CQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_RES_CQTY"].ToString()),
                IRD_REQ_QTY = row["IRD_REQ_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_REQ_QTY"].ToString()),
                IRD_REQ_BQTY = row["IRD_REQ_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_REQ_BQTY"].ToString()),
                IRD_REQ_CQTY = row["IRD_REQ_CQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_REQ_CQTY"].ToString()),
                IRD_ACT = row["IRD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_ACT"].ToString()),
                IRD_RESREQ_NO = row["IRD_RESREQ_NO"] == DBNull.Value ? string.Empty : row["IRD_RESREQ_NO"].ToString(),
                IRD_RESREQ_LINE = row["IRD_RESREQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_RESREQ_LINE"].ToString()),
                Ird_so_mrn_bqty = row["Ird_so_mrn_bqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Ird_so_mrn_bqty"].ToString()),
            };
        }
        public static INR_RES_DET Converter2(DataRow row)
        {
            return new INR_RES_DET
            {
                IRD_SEQ = row["IRD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_SEQ"].ToString()),
                IRD_RES_NO = row["IRD_RES_NO"] == DBNull.Value ? string.Empty : row["IRD_RES_NO"].ToString(),
                IRD_LINE = row["IRD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_LINE"].ToString()),
                IRD_ITM_CD = row["IRD_ITM_CD"] == DBNull.Value ? string.Empty : row["IRD_ITM_CD"].ToString(),
                IRD_ITM_STUS = row["IRD_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRD_ITM_STUS"].ToString(),
                IRD_RES_QTY = row["IRD_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_RES_QTY"].ToString()),
                IRD_RES_BQTY = row["IRD_RES_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_RES_BQTY"].ToString()),
                IRD_RES_CQTY = row["IRD_RES_CQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_RES_CQTY"].ToString()),
                IRD_REQ_QTY = row["IRD_REQ_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_REQ_QTY"].ToString()),
                IRD_REQ_BQTY = row["IRD_REQ_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_REQ_BQTY"].ToString()),
                Ird_so_mrn_bqty = row["Ird_so_mrn_bqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Ird_so_mrn_bqty"].ToString()),
                IRD_REQ_CQTY = row["IRD_REQ_CQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRD_REQ_CQTY"].ToString()),
                IRD_ACT = row["IRD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_ACT"].ToString()),
                IRD_RESREQ_NO = row["IRD_RESREQ_NO"] == DBNull.Value ? string.Empty : row["IRD_RESREQ_NO"].ToString(),
                IRD_RESREQ_LINE = row["IRD_RESREQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_RESREQ_LINE"].ToString())
            };
        }
    }
}
