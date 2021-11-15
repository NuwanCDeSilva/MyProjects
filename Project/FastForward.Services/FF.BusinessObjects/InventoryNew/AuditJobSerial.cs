using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class AuditJobSerial
    {
        public int Audjs_Seq { get; set; }
        public string Audjs_JobNo { get; set; }
        public string Audjs_ItemCode { get; set; }
        public string Audjs_SerialNo { get; set; }
        public string Audjs_ItemStatus { get; set; }
        public string Audjs_InDocNo { get; set; }
        public DateTime? Audjs_DocInDate { get; set; }
        public string Audjs_WarrantyNo { get; set; }
        public string Audjs_RefStatus { get; set; }
        public string Audjs_RptType { get; set; }
        public string Audjs_Type { get; set; }
        public int Audjs_SerialId { get; set; }
        public string Audjs_Remark { get; set; }
        public DateTime? Audjs_OriginelInDate { get; set; }
        public string Audjs_PhysicallyAvailableSerial { get; set; }
        public string Audjs_CreatedBy { get; set; }
        public DateTime Audjs_CreatedDate { get; set; }

        //Akila 2017/04/28
        public string Audjs_SessionId { get; set; }
        public DateTime? Audjs_ModDate { get; set; }
        public string Audjs_ModBy { get; set; }
        public Decimal  Aud_charges { get; set; }
        public Decimal AUDJS_CHARGES_USER { get; set; }
        public Int32 _IS_USER_charge_update { get; set; }
        public Int32 audjs_charges_processed { get; set; }
        public string Audjs_Charges_resoon { get; set; }
        public static AuditJobSerial Converter(DataRow row)
        {
            return new AuditJobSerial
            {
                Audjs_CreatedBy = row["AUDJS_CRE_BY"] == DBNull.Value ? string.Empty : row["AUDJS_CRE_BY"].ToString(),
                Audjs_CreatedDate = row["AUDJS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUDJS_CRE_DT"]),
                Audjs_InDocNo = row["AUDJS_IN_DOC"] == DBNull.Value ? string.Empty : row["AUDJS_IN_DOC"].ToString(),
                Audjs_DocInDate = row["AUDJS_IN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUDJS_IN_DT"]),
                Audjs_ItemCode = row["AUDJS_ITEM"] == DBNull.Value ? string.Empty : row["AUDJS_ITEM"].ToString(),
                Audjs_ItemStatus = row["AUDJS_ITM_STUS"] == DBNull.Value ? string.Empty : row["AUDJS_ITM_STUS"].ToString(),
                Audjs_JobNo = row["AUDJS_JOB"] == DBNull.Value ? string.Empty : row["AUDJS_JOB"].ToString(),
                Audjs_RefStatus = row["AUDJS_REF_STUS"] == DBNull.Value ? string.Empty : row["AUDJS_REF_STUS"].ToString(),
                Audjs_RptType = row["AUDJS_RPT_TYPE"] == DBNull.Value ? string.Empty : row["AUDJS_RPT_TYPE"].ToString(),
                Audjs_Seq = row["AUDJS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUDJS_SEQ"]),
                Audjs_SerialNo = row["AUDJS_SERIAL"] == DBNull.Value ? string.Empty : row["AUDJS_SERIAL"].ToString(),
                Audjs_WarrantyNo = row["AUDJS_WARRANTY"] == DBNull.Value ? string.Empty : row["AUDJS_WARRANTY"].ToString(),
                Audjs_Type = row["AUDJS_TYPE"] == DBNull.Value ? string.Empty : (row["AUDJS_TYPE"]).ToString(),
                Audjs_SerialId = row["AUDJS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUDJS_SER_ID"]),
                Audjs_OriginelInDate = row["AUDJS_ORG_IN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUDJS_ORG_IN_DT"]),
                Audjs_Remark = row["AUDJS_RMK"] == DBNull.Value ? string.Empty : (row["AUDJS_RMK"]).ToString(),
                Audjs_PhysicallyAvailableSerial = row["AUDJS_PHY_SERIAL"] == DBNull.Value ? string.Empty : (row["AUDJS_PHY_SERIAL"]).ToString(),
                Aud_charges = row["audjs_charges"] == DBNull.Value ? 0 : Convert.ToDecimal(row["audjs_charges"]),
                AUDJS_CHARGES_USER = row["AUDJS_CHARGES_USER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUDJS_CHARGES_USER"]),
                audjs_charges_processed = row["audjs_charges_processed"] == DBNull.Value ? 0 : Convert.ToInt32(row["audjs_charges_processed"]),
               
            };
        }
        public static AuditJobSerial Converternew(DataRow row)
        {
            return new AuditJobSerial
            {
                Audjs_CreatedBy = row["AUDJS_CRE_BY"] == DBNull.Value ? string.Empty : row["AUDJS_CRE_BY"].ToString(),
                Audjs_CreatedDate = row["AUDJS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUDJS_CRE_DT"]),
                Audjs_InDocNo = row["AUDJS_IN_DOC"] == DBNull.Value ? string.Empty : row["AUDJS_IN_DOC"].ToString(),
                Audjs_DocInDate = row["AUDJS_IN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUDJS_IN_DT"]),
                Audjs_ItemCode = row["AUDJS_ITEM"] == DBNull.Value ? string.Empty : row["AUDJS_ITEM"].ToString(),
                Audjs_ItemStatus = row["AUDJS_ITM_STUS"] == DBNull.Value ? string.Empty : row["AUDJS_ITM_STUS"].ToString(),
                Audjs_JobNo = row["AUDJS_JOB"] == DBNull.Value ? string.Empty : row["AUDJS_JOB"].ToString(),
                Audjs_RefStatus = row["AUDJS_REF_STUS"] == DBNull.Value ? string.Empty : row["AUDJS_REF_STUS"].ToString(),
                Audjs_RptType = row["AUDJS_RPT_TYPE"] == DBNull.Value ? string.Empty : row["AUDJS_RPT_TYPE"].ToString(),
                Audjs_Seq = row["AUDJS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUDJS_SEQ"]),
                Audjs_SerialNo = row["AUDJS_SERIAL"] == DBNull.Value ? string.Empty : row["AUDJS_SERIAL"].ToString(),
                Audjs_WarrantyNo = row["AUDJS_WARRANTY"] == DBNull.Value ? string.Empty : row["AUDJS_WARRANTY"].ToString(),
                Audjs_Type = row["AUDJS_TYPE"] == DBNull.Value ? string.Empty : (row["AUDJS_TYPE"]).ToString(),
                Audjs_SerialId = row["AUDJS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUDJS_SER_ID"]),
                Audjs_OriginelInDate = row["AUDJS_ORG_IN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUDJS_ORG_IN_DT"]),
                Audjs_Remark = row["AUDJS_RMK"] == DBNull.Value ? string.Empty : (row["AUDJS_RMK"]).ToString(),
                Audjs_PhysicallyAvailableSerial = row["AUDJS_PHY_SERIAL"] == DBNull.Value ? string.Empty : (row["AUDJS_PHY_SERIAL"]).ToString(),
                Aud_charges = row["audjs_charges"] == DBNull.Value ? 0 : Convert.ToDecimal(row["audjs_charges"]),
                AUDJS_CHARGES_USER = row["AUDJS_CHARGES_USER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUDJS_CHARGES_USER"]),
                audjs_charges_processed = row["audjs_charges_processed"] == DBNull.Value ? 0 : Convert.ToInt32(row["audjs_charges_processed"]),
                Audjs_Charges_resoon = row["aurs_desc"] == DBNull.Value ? string.Empty : (row["aurs_desc"]).ToString()
            };
        }
    }
}
