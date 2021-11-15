using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class AuditMemebers
    {
        public string Ajm_Job_no { get; set; }
        public string Ajm_Mem_Id { get; set; }
        public string Ajm_Mem_Nic { get; set; }
        public string Ajm_Mem_Name { get; set; }
        public string Ajm_Cre_by { get; set; }
        public DateTime Ajm_Cre_Dt { get; set; }
        public string Ajm_Mem_type { get; set; }
        public Int32 AJM_RECORD_COUNT { get; set; }
        public static AuditMemebers Converter(DataRow _row)
        {
            return new AuditMemebers 
            {
                Ajm_Job_no = _row["AJM_JOB_NO"] == DBNull.Value ? string.Empty : _row["AJM_JOB_NO"].ToString(),
                Ajm_Mem_Id = _row["AJM_MEM_ID"] == DBNull.Value ? string.Empty : _row["AJM_MEM_ID"].ToString(),
                Ajm_Mem_Nic = _row["AJM_MEM_NIC"] == DBNull.Value ? string.Empty : _row["AJM_MEM_NIC"].ToString(),
                Ajm_Mem_Name = _row["AJM_MEM_NAME"] == DBNull.Value ? string.Empty : _row["AJM_MEM_NAME"].ToString(),
                Ajm_Mem_type = _row["AJM_MEM_TYPE"] == DBNull.Value ? "MEMBER" : _row["AJM_MEM_TYPE"].ToString(),
                Ajm_Cre_by = _row["AJM_CRE_BY"] == DBNull.Value ? string.Empty : _row["AJM_CRE_BY"].ToString(),
                Ajm_Cre_Dt = _row["AJM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime( _row["AJM_CRE_DT"].ToString())
            };
        }
    }
}
