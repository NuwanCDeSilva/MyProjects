using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.Services
{
   public class ServiceAreaS
    {
        private string _SSA_COM;
        private string _SSA_SER_LOC;
        private string _SSA_TOWN_CD;
        private int _SSA_ACT;
        private string _SSA_CRE_BY;
        private DateTime _SSA_CRE_DT;
        private string _SSA_MOD_BY;
        private DateTime _SSA_MOD_DT;
        private Int32 _SSA_DEF;
        public string SSA_COM { get { return _SSA_COM; } set { _SSA_COM = value; } }
        public string SSA_SER_LOC { get { return _SSA_SER_LOC; } set { _SSA_SER_LOC = value; } }
        public string SSA_TOWN_CD { get { return _SSA_TOWN_CD; } set { _SSA_TOWN_CD = value; } }
        public int SSA_ACT { get { return _SSA_ACT; } set { _SSA_ACT = value; } }
        public string SSA_CRE_BY { get { return _SSA_CRE_BY; } set { _SSA_CRE_BY = value; } }
        public DateTime SSA_CRE_DT { get { return _SSA_CRE_DT; } set { _SSA_CRE_DT = value; } }
        public string SSA_MOD_BY { get { return _SSA_MOD_BY; } set { _SSA_MOD_BY = value; } }
        public DateTime SSA_MOD_DT { get { return _SSA_MOD_DT; } set { _SSA_MOD_DT = value; } }
        public Int32 SSA_DEF { get { return _SSA_DEF; } set { _SSA_DEF = value; } }


        public static ServiceAreaS Converter(DataRow row)
        {
            return new ServiceAreaS
            {
                SSA_COM = row["SSA_COM"] == DBNull.Value ? string.Empty : row["SSA_COM"].ToString(),
                SSA_SER_LOC = row["SSA_SER_LOC"] == DBNull.Value ? string.Empty : row["SSA_SER_LOC"].ToString(),
                SSA_TOWN_CD = row["SSA_TOWN_CD"] == DBNull.Value ? string.Empty : (row["SSA_TOWN_CD"].ToString()),
                SSA_ACT = row["SSA_ACT"] == DBNull.Value ? 0 : Convert.ToInt32 (row["SSA_ACT"].ToString()),
              
                SSA_DEF = row["ssa_def_loc"] == DBNull.Value ? 0 : Convert.ToInt32(row["ssa_def_loc"].ToString()),
               
            };
        }
    }
}
