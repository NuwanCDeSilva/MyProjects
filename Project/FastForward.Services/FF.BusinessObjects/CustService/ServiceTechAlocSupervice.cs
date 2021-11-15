using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.CustService
{
    public class ServiceTechAlocSupervice
    {
        public Int32 Stas_Seq { get; set; }
        public string  Stas_Com_Cd { get; set; }
        public string Stas_Loc_Cd { get; set; }
        public string Stas_Job_No { get; set; }
        public string Stas_Emp_Cd { get; set; }
        public string Stas_Status { get; set; }
        public string Stas_Cre_By { get; set; }
        public DateTime? Stas_Cre_Dt { get; set; }
        public string Stas_Mod_By { get; set; }
        public DateTime? Stas_Mod_Dt { get; set; }
        public string Stas_Session_Id { get; set; }
        public string Stas_Supervice_Name { get; set; }

        public static ServiceTechAlocSupervice Converter (DataRow row)
        {
            return new ServiceTechAlocSupervice
            {
                Stas_Seq = row["stas_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["stas_seq"].ToString()),
                Stas_Com_Cd = row["stas_com_cd"] == DBNull.Value ? string.Empty : row["stas_com_cd"].ToString(),
                Stas_Loc_Cd = row["stas_loc_cd"] == DBNull.Value ? string.Empty : row["stas_loc_cd"].ToString(),
                Stas_Job_No = row["stas_job_no"] == DBNull.Value ? string.Empty : row["stas_job_no"].ToString(),
                Stas_Emp_Cd = row["stas_emp_cd"] == DBNull.Value ? string.Empty : row["stas_emp_cd"].ToString(),
                Stas_Status = row["stas_status"] == DBNull.Value ? string.Empty : row["stas_status"].ToString(),
                Stas_Cre_By = row["stas_cre_by"] == DBNull.Value ? string.Empty : row["stas_cre_by"].ToString(),
                Stas_Cre_Dt = row["stas_cre_dt"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["stas_cre_dt"].ToString()),
                Stas_Mod_By = row["stas_mod_by"] == DBNull.Value ? string.Empty : row["stas_mod_by"].ToString(),
                Stas_Mod_Dt = row["stas_mod_dt"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["stas_mod_dt"].ToString()),
                Stas_Session_Id = row["stas_session_id"] == DBNull.Value ? string.Empty : row["stas_session_id"].ToString()
            };
        }
    }
}
