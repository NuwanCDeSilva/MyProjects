using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hero_Service_Consol.DTOs
{
    public class MstAlertCriteria
    {
        public string Alc_Com_code { get; set; }
        public string Alc_User { get; set; }
        public string Alc_Module { get; set; }
        public string Alc_Criteria_Type { get; set; }
        public string Alc_Code { get; set; }
        public string Alc_User_Email { get; set; }
        public int Alc_Late_Noof_Dt { get; set; }

        public static MstAlertCriteria Converter(DataRow _row)
        {
            return new MstAlertCriteria
            {
                Alc_Com_code = _row["alc_com"] == DBNull.Value ? string.Empty : _row["alc_com"].ToString(),
                Alc_User = _row["alc_user"] == DBNull.Value ? string.Empty : _row["alc_user"].ToString(),
                Alc_Module = _row["alc_module"] == DBNull.Value ? string.Empty : _row["alc_module"].ToString(),
                Alc_Criteria_Type = _row["alc_criteria_type"] == DBNull.Value ? string.Empty : _row["alc_criteria_type"].ToString(),
                Alc_Code = _row["alc_code"] == DBNull.Value ? string.Empty : _row["alc_code"].ToString(),
                Alc_User_Email = _row["alc_user_email"] == DBNull.Value ? string.Empty : _row["alc_user_email"].ToString(),
                Alc_Late_Noof_Dt = _row["alc_late_noof_dt"] == DBNull.Value ? 0 : Convert.ToInt32(_row["alc_late_noof_dt"])
            };            
        }
    }
}
