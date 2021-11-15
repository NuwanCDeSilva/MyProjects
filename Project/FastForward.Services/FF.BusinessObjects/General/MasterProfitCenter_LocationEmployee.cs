using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    //Kelum : Profit Center Assign and Location Assign for Employee : 2016-April-23

    [Serializable]
    public class MasterProfitCenter_LocationEmployee
    {
        public String _mpce_epf { get; set; }
        public String _mpce_com { get; set; }
        public String _mpce_pc { get; set; }
        public DateTime _mpce_assn_dt { get; set; }
        public String _mpce_rep_cd { get; set; }
        public String _mpce_anal_1 { get; set; }
        public String _mpce_anal_2 { get; set; }
        public String _mpce_anal_3 { get; set; }      
        public Int32 _mpce_act { get; set; }
        public Decimal _mpce_max_stk_val { get; set; }
        public String _mpce_mgr { get; set; }
        public Int32 _mpce_is_rest { get; set; }

        //grid poperties

        public String companyname { get; set; }
        public String profitcenterorlocation { get; set; }
        public String manager { get; set; }

        public static MasterProfitCenter_LocationEmployee Converter(DataRow row)
        {
            return new MasterProfitCenter_LocationEmployee
            {
                _mpce_epf = row["MPCE_EPF"] == DBNull.Value ? string.Empty : row["MPCE_EPF"].ToString(),
                _mpce_com = row["MPCE_COM"] == DBNull.Value ? string.Empty : row["MPCE_COM"].ToString(),
                _mpce_pc = row["MPCE_PC"] == DBNull.Value ? string.Empty : row["MPCE_PC"].ToString(),
                _mpce_assn_dt = row["MPCE_ASSN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPCE_ASSN_DT"].ToString()),
                _mpce_rep_cd = row["MPCE_REP_CD"] == DBNull.Value ? string.Empty : row["MPCE_REP_CD"].ToString(),
                _mpce_anal_1 = row["MPCE_ANAL_1"] == DBNull.Value ? string.Empty : row["MPCE_ANAL_1"].ToString(),
                _mpce_anal_2 = row["MPCE_ANAL_2"] == DBNull.Value ? string.Empty : row["MPCE_ANAL_2"].ToString(),
                _mpce_anal_3 = row["MPCE_ANAL_3"] == DBNull.Value ? string.Empty : row["MPCE_ANAL_3"].ToString(),
                _mpce_act = row["MPCE_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPCE_ACT"].ToString()),
                _mpce_max_stk_val = row["MPCE_MAX_STK_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPCE_MAX_STK_VAL"].ToString()),
                _mpce_mgr = row["MPCE_MGR"] == DBNull.Value ? string.Empty : row["MPCE_MGR"].ToString(),
                _mpce_is_rest = row["MPCE_IS_REST"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPCE_IS_REST"].ToString()),
                companyname = row["companyname"] == DBNull.Value ? string.Empty : row["companyname"].ToString(),
                profitcenterorlocation = row["profitcenterorlocation"] == DBNull.Value ? string.Empty : row["profitcenterorlocation"].ToString(),
                manager = row["manager"] == DBNull.Value ? string.Empty : row["manager"].ToString()
              

            };
        } 
    }
}
