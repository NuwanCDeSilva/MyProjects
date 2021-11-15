using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hero_Service_Consol.DTOs
{
    public class MstModuleConf
    {
        public string Mmc_Mod_Cd { get; set; }
        public string Mmc_Run_Time_Unit { get; set; }
        public Int32 Mmc_Run_Time{ get; set; }
        public DateTime Mmc_Last_Run_Dt { get; set; }
        public Int32 Mmc_Minimum_Send_Dt { get; set; }

        public MstModuleConf(DataRow _row)
        {
            Mmc_Mod_Cd = _row["mmc_mod_cd"] == DBNull.Value ? string.Empty : _row["mmc_mod_cd"].ToString();
            Mmc_Run_Time_Unit = _row["mmc_run_time_unit"] == DBNull.Value ? string.Empty : _row["mmc_run_time_unit"].ToString();
            Mmc_Run_Time = _row["mmc_run_time"] == DBNull.Value ? 0 : Convert.ToInt32(_row["mmc_run_time"]);
            Mmc_Last_Run_Dt = _row["mmc_last_run_dt"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(_row["mmc_last_run_dt"]);
            Mmc_Minimum_Send_Dt = _row["mmc_minimum_send_dt"] == DBNull.Value ? 0 : Convert.ToInt32(_row["mmc_minimum_send_dt"]);
        }
    }
}
