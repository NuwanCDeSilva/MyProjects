using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.General
{
    public class INT_HDR
    {
        public Int32 ITH_SEQ_NO { get; set; }
        public String ITH_COM { get; set; }
        public String ITH_SBU { get; set; }
        public String ITH_CHANNEL { get; set; }
        public String ITH_LOC { get; set; }
        public String ITH_DOC_NO { get; set; }
        public DateTime ITH_DOC_DATE { get; set; }
        public Int32 ITH_DOC_YEAR { get; set; }
        public Int32 ITH_DIRECT { get; set; }
        public String ITH_DOC_TP { get; set; }
        public String ITH_CATE_TP { get; set; }
        public String ITH_SUB_TP { get; set; }
        public String ITH_ENTRY_TP { get; set; }
        public String ITH_LOADING_USER { get; set; }
        public String ITH_LOADING_POINT { get; set; }
        public String ITH_BUS_ENTITY { get; set; }
        public String ITH_OTH_LOC { get; set; }
        public String ITH_JOB_NO { get; set; }
        public String ITH_ENTRY_NO { get; set; }
        public String ITH_ACC_NO { get; set; }
        public Int32 ITH_IS_MANUAL { get; set; }
        public String ITH_MANUAL_REF { get; set; }
        public String ITH_COM_DOCNO { get; set; }
        public String ITH_REMARKS { get; set; }
        public String ITH_VEHI_NO { get; set; }
        public String ITH_DEL_CD { get; set; }
        public String ITH_DEL_ADD1 { get; set; }
        public String ITH_DEL_ADD2 { get; set; }
        public String ITH_DEL_PARTY { get; set; }
        public String ITH_DEL_TOWN { get; set; }
        public Int32 ITH_GIT_CLOSE { get; set; }
        public DateTime ITH_GIT_CLOSE_DATE { get; set; }
        public String ITH_GIT_CLOSE_DOC { get; set; }
        public Int32 ITH_ISPRINTED { get; set; }
        public Int32 ITH_NOOFCOPIES { get; set; }
        public String ITH_STUS { get; set; }
        public String ITH_CRE_BY { get; set; }
        public DateTime ITH_CRE_WHEN { get; set; }
        public String ITH_MOD_BY { get; set; }
        public DateTime ITH_MOD_WHEN { get; set; }
        public String ITH_SESSION_ID { get; set; }
        public String ITH_ANAL_1 { get; set; }
        public String ITH_ANAL_2 { get; set; }
        public String ITH_ANAL_3 { get; set; }
        public String ITH_ANAL_4 { get; set; }
        public String ITH_ANAL_5 { get; set; }
        public Int32 ITH_ANAL_6 { get; set; }
        public Int32 ITH_ANAL_7 { get; set; }
        public DateTime ITH_ANAL_8 { get; set; }
        public DateTime ITH_ANAL_9 { get; set; }
        public Int32 ITH_ANAL_10 { get; set; }
        public Int32 ITH_ANAL_11 { get; set; }
        public Int32 ITH_ANAL_12 { get; set; }
        public String ITH_SUB_DOCNO { get; set; }
        public String ITH_OTH_DOCNO { get; set; }
        public String ITH_PC { get; set; }
        public String ITH_OTH_COM { get; set; }
        public Int32 ITH_ISJOBBASE { get; set; }
        public String ITH_BK_NO { get; set; }
        public static INT_HDR Converter(DataRow row)
        {
            return new INT_HDR
            {
                ITH_SEQ_NO = row["ITH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_SEQ_NO"].ToString()),
                ITH_COM = row["ITH_COM"] == DBNull.Value ? string.Empty : row["ITH_COM"].ToString(),
                ITH_SBU = row["ITH_SBU"] == DBNull.Value ? string.Empty : row["ITH_SBU"].ToString(),
                ITH_CHANNEL = row["ITH_CHANNEL"] == DBNull.Value ? string.Empty : row["ITH_CHANNEL"].ToString(),
                ITH_LOC = row["ITH_LOC"] == DBNull.Value ? string.Empty : row["ITH_LOC"].ToString(),
                ITH_DOC_NO = row["ITH_DOC_NO"] == DBNull.Value ? string.Empty : row["ITH_DOC_NO"].ToString(),
                ITH_DOC_DATE = row["ITH_DOC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_DOC_DATE"].ToString()),
                ITH_DOC_YEAR = row["ITH_DOC_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_DOC_YEAR"].ToString()),
                ITH_DIRECT = row["ITH_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_DIRECT"].ToString()),
                ITH_DOC_TP = row["ITH_DOC_TP"] == DBNull.Value ? string.Empty : row["ITH_DOC_TP"].ToString(),
                ITH_CATE_TP = row["ITH_CATE_TP"] == DBNull.Value ? string.Empty : row["ITH_CATE_TP"].ToString(),
                ITH_SUB_TP = row["ITH_SUB_TP"] == DBNull.Value ? string.Empty : row["ITH_SUB_TP"].ToString(),
                ITH_ENTRY_TP = row["ITH_ENTRY_TP"] == DBNull.Value ? string.Empty : row["ITH_ENTRY_TP"].ToString(),
                ITH_LOADING_USER = row["ITH_LOADING_USER"] == DBNull.Value ? string.Empty : row["ITH_LOADING_USER"].ToString(),
                ITH_LOADING_POINT = row["ITH_LOADING_POINT"] == DBNull.Value ? string.Empty : row["ITH_LOADING_POINT"].ToString(),
                ITH_BUS_ENTITY = row["ITH_BUS_ENTITY"] == DBNull.Value ? string.Empty : row["ITH_BUS_ENTITY"].ToString(),
                ITH_OTH_LOC = row["ITH_OTH_LOC"] == DBNull.Value ? string.Empty : row["ITH_OTH_LOC"].ToString(),
                ITH_JOB_NO = row["ITH_JOB_NO"] == DBNull.Value ? string.Empty : row["ITH_JOB_NO"].ToString(),
                ITH_ENTRY_NO = row["ITH_ENTRY_NO"] == DBNull.Value ? string.Empty : row["ITH_ENTRY_NO"].ToString(),
                ITH_ACC_NO = row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                ITH_IS_MANUAL = row["ITH_IS_MANUAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_IS_MANUAL"].ToString()),
                ITH_MANUAL_REF = row["ITH_MANUAL_REF"] == DBNull.Value ? string.Empty : row["ITH_MANUAL_REF"].ToString(),
                ITH_COM_DOCNO = row["ITH_COM_DOCNO"] == DBNull.Value ? string.Empty : row["ITH_COM_DOCNO"].ToString(),
                ITH_REMARKS = row["ITH_REMARKS"] == DBNull.Value ? string.Empty : row["ITH_REMARKS"].ToString(),
                ITH_VEHI_NO = row["ITH_VEHI_NO"] == DBNull.Value ? string.Empty : row["ITH_VEHI_NO"].ToString(),
                ITH_DEL_CD = row["ITH_DEL_CD"] == DBNull.Value ? string.Empty : row["ITH_DEL_CD"].ToString(),
                ITH_DEL_ADD1 = row["ITH_DEL_ADD1"] == DBNull.Value ? string.Empty : row["ITH_DEL_ADD1"].ToString(),
                ITH_DEL_ADD2 = row["ITH_DEL_ADD2"] == DBNull.Value ? string.Empty : row["ITH_DEL_ADD2"].ToString(),
                ITH_DEL_PARTY = row["ITH_DEL_PARTY"] == DBNull.Value ? string.Empty : row["ITH_DEL_PARTY"].ToString(),
                ITH_DEL_TOWN = row["ITH_DEL_TOWN"] == DBNull.Value ? string.Empty : row["ITH_DEL_TOWN"].ToString(),
                ITH_GIT_CLOSE = row["ITH_GIT_CLOSE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_GIT_CLOSE"].ToString()),
                ITH_GIT_CLOSE_DATE = row["ITH_GIT_CLOSE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_GIT_CLOSE_DATE"].ToString()),
                ITH_GIT_CLOSE_DOC = row["ITH_GIT_CLOSE_DOC"] == DBNull.Value ? string.Empty : row["ITH_GIT_CLOSE_DOC"].ToString(),
                ITH_ISPRINTED = row["ITH_ISPRINTED"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_ISPRINTED"].ToString()),
                ITH_NOOFCOPIES = row["ITH_NOOFCOPIES"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_NOOFCOPIES"].ToString()),
                ITH_STUS = row["ITH_STUS"] == DBNull.Value ? string.Empty : row["ITH_STUS"].ToString(),
                ITH_CRE_BY = row["ITH_CRE_BY"] == DBNull.Value ? string.Empty : row["ITH_CRE_BY"].ToString(),
                ITH_CRE_WHEN = row["ITH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_CRE_WHEN"].ToString()),
                ITH_MOD_BY = row["ITH_MOD_BY"] == DBNull.Value ? string.Empty : row["ITH_MOD_BY"].ToString(),
                ITH_MOD_WHEN = row["ITH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_MOD_WHEN"].ToString()),
                ITH_SESSION_ID = row["ITH_SESSION_ID"] == DBNull.Value ? string.Empty : row["ITH_SESSION_ID"].ToString(),
                ITH_ANAL_1 = row["ITH_ANAL_1"] == DBNull.Value ? string.Empty : row["ITH_ANAL_1"].ToString(),
                ITH_ANAL_2 = row["ITH_ANAL_2"] == DBNull.Value ? string.Empty : row["ITH_ANAL_2"].ToString(),
                ITH_ANAL_3 = row["ITH_ANAL_3"] == DBNull.Value ? string.Empty : row["ITH_ANAL_3"].ToString(),
                ITH_ANAL_4 = row["ITH_ANAL_4"] == DBNull.Value ? string.Empty : row["ITH_ANAL_4"].ToString(),
                ITH_ANAL_5 = row["ITH_ANAL_5"] == DBNull.Value ? string.Empty : row["ITH_ANAL_5"].ToString(),
                ITH_ANAL_6 = row["ITH_ANAL_6"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_ANAL_6"].ToString()),
                ITH_ANAL_7 = row["ITH_ANAL_7"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_ANAL_7"].ToString()),
                ITH_ANAL_8 = row["ITH_ANAL_8"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_ANAL_8"].ToString()),
                ITH_ANAL_9 = row["ITH_ANAL_9"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_ANAL_9"].ToString()),
                ITH_ANAL_10 = row["ITH_ANAL_10"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_ANAL_10"].ToString()),
                ITH_ANAL_11 = row["ITH_ANAL_11"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_ANAL_11"].ToString()),
                ITH_ANAL_12 = row["ITH_ANAL_12"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_ANAL_12"].ToString()),
                ITH_SUB_DOCNO = row["ITH_SUB_DOCNO"] == DBNull.Value ? string.Empty : row["ITH_SUB_DOCNO"].ToString(),
                ITH_OTH_DOCNO = row["ITH_OTH_DOCNO"] == DBNull.Value ? string.Empty : row["ITH_OTH_DOCNO"].ToString(),
                ITH_PC = row["ITH_PC"] == DBNull.Value ? string.Empty : row["ITH_PC"].ToString(),
                ITH_OTH_COM = row["ITH_OTH_COM"] == DBNull.Value ? string.Empty : row["ITH_OTH_COM"].ToString(),
                ITH_ISJOBBASE = row["ITH_ISJOBBASE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_ISJOBBASE"].ToString()),
                ITH_BK_NO = row["ITH_BK_NO"] == DBNull.Value ? string.Empty : row["ITH_BK_NO"].ToString()
            };
        }
    }

}
